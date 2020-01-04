using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace LightController.PeripheralDevice
{
    public abstract class BaseCommunication
    {
        private const string BEGIN_CONFIG = "BeginConfig";
        private const string END_CONFIG = "EndConfig";
        protected const int UDPADDR = 255;
        private const int PACKHEADLENGTH = 8;//协议头大小
        private const int PACKSIZE = 512;//通信分包大小
        private const byte PACKFLAG1 = 0xAA;//协议标记1
        private const byte PACKFLAG2 = 0xBB;//协议标记2
        private const byte PARAM_SEPARATOR = 0x20;//通信命令参数分割符
        private const byte PARAM_TERMINATOR = 0x00;//通信命令参数分割符
        private const byte PLACEHOLDER = 0x00;//占位符
        protected int DeviceAddr { get; set; }//设备地址
        protected static List<byte> ReadBuff = new List<byte>();//接收缓存
        private Order SecondOrder { get; set; }//二级命令
        private string MainOrder { get; set; }//主命令
        protected bool IsAck { get; set; }//回复确认标记
        private bool IsSending { get; set; }//发送进行中标记
        private bool IsStartCopy { get; set; }
        protected System.Timers.Timer TimeOutTimer { get; set; }//超时定时器
        private const double TIMEOUT = 4000;//超时等待时长
        //private bool ThreadStatus { get; set; }//线程状态标记
        private bool IsStopThread { get; set; }//终止线程继续执行标记
        private byte[] Data { get; set; }//数据
        private int PackCount { get; set; }//通信分包总数
        private int PackIndex { get; set; }//当前发送分包编号

        public delegate void Completed(Object obj);
        public delegate void Error();
        public event Completed Completed_Event;
        public event Error Error_Event;
        //初始化
        protected void Init()
        {
            this.IsStopThread = false;
            this.IsSending = false;
            this.IsStartCopy = false;
        }
        protected bool IsReceiveAck(byte[] data)
        {
            bool result = false;
            result = data[0] == 0x61 && data[1] == 0x63 && data[2] == 0x6B && data[3] == 0x0D && data[4] == 0x0A;
            return result;
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        protected abstract void Send(byte[] data);
        /// <summary>
        /// 搜索设备
        /// </summary>
        public abstract void AutoSearchDevice();
        /// <summary>
        /// 链接设备
        /// </summary>
        public abstract void ConnectDevice();
        /// <summary>
        /// 启动透传模式
        /// </summary>
        public void BeginConfig()
        {
            List<byte> data = new List<byte>();
            int packLength = Encoding.Default.GetBytes(BEGIN_CONFIG).Length;
            data.Add(0xAA);
            data.Add(0xBB);
            data.Add(0xFF);
            data.Add(Convert.ToByte(packLength & 0xFF));
            data.Add(Convert.ToByte((packLength >> 8) & 0xFF));
            data.Add(Convert.ToByte("00000001", 2));
            data.Add(0x00);
            data.Add(0x00);
            data.AddRange(Encoding.Default.GetBytes(BEGIN_CONFIG));
            byte[] crc = CRCTools.GetInstance().GetCRC(data.ToArray());
            data[6] = crc[0];
            data[7] = crc[1];
            this.Send(data.ToArray());
        }
        /// <summary>
        /// 关闭透传模式
        /// </summary>
        public void EndConfig()
        {
            List<byte> data = new List<byte>();
            int packLength = Encoding.Default.GetBytes(END_CONFIG).Length;
            data.Add(0xAA);
            data.Add(0xBB);
            data.Add(0xFF);
            data.Add(Convert.ToByte(packLength & 0xFF));
            data.Add(Convert.ToByte((packLength >> 8) & 0xFF));
            data.Add(Convert.ToByte("00000001", 2));
            data.Add(0x00);
            data.Add(0x00);
            data.AddRange(Encoding.Default.GetBytes(END_CONFIG));
            byte[] crc = CRCTools.GetInstance().GetCRC(data.ToArray());
            data[6] = crc[0];
            data[7] = crc[1];
            this.Send(data.ToArray());
        }
        /// <summary>
        /// 发送数据完成
        /// </summary>
        protected void SendDataCompleted()
        {
            this.StartTimeOut();
            Console.WriteLine("发送成功");
        }
        /// <summary>
        /// 定时器执行语句
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendTimeOut(object sender, ElapsedEventArgs e)
        {
            this.IsStopThread = true;
            this.IsSending = false;
            Console.WriteLine("发送超时");
            this.Error_Event();
        }
        /// <summary>
        /// 启动定时器计时
        /// </summary>
        private void StartTimeOut()
        {
            if (this.TimeOutTimer == null)
            {
                this.TimeOutTimer = new System.Timers.Timer(TIMEOUT);
                this.TimeOutTimer.Elapsed += SendTimeOut;
                this.TimeOutTimer.Enabled = true;
                this.TimeOutTimer.AutoReset = false;
            }
            Console.WriteLine("启动超时处理定时器");
            this.TimeOutTimer.Start();
        }
        /// <summary>
        /// 停止定时器计时
        /// </summary>
        private void StopTimeOut()
        {
            if (TimeOutTimer != null)
            {
                TimeOutTimer.Stop();
                Console.WriteLine("停止超时定时器");
            }
        }
        /// <summary>
        /// 获取数据包标记位
        /// </summary>
        /// <returns></returns>
        private byte GetDataMark()
        {
            byte result = 0x00;
            if (this.PackIndex == this.PackCount)
            {
                result = 0x02;
            }
            else
            {
                result = 0x06;
            }
            return result;
        }
        /// <summary>
        /// 获取命令包标记位
        /// </summary>
        /// <returns>标记位</returns>
        private Byte GetOrderMark()
        {
            byte result = 0x00;
            switch (this.MainOrder)
            {
                case Constant.NEW_DEVICE_LIGHTCONTROL:
                    result = this.GetLightControlMark();
                    break;
                case Constant.NEW_DEVICE_CENTRALCONTROL:
                    result = this.GetCenTralControlMark();
                    break;
            }
            return result;
        }
        /// <summary>
        /// 获取灯控设备操作命令对应标记位
        /// </summary>
        /// <returns></returns>
        private Byte GetLightControlMark()
        {
            byte result = 0x00;
            switch (this.SecondOrder)
            {
                case Order.ZG:
                case Order.RG:
                case Order.YG:
                case Order.DG:
                    result = 0x05;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 获取中控设备操作命令对应标记位
        /// </summary>
        /// <returns></returns>
        private Byte GetCenTralControlMark()
        {
            byte result = 0x00;
            switch (this.SecondOrder)
            {
                case Order.LK:
                case Order.DL:
                case Order.CP:
                case Order.XP:
                    result = 0x05;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 发送命令通信包
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="order">命令</param>
        /// <param name="paramList">参数</param>
        private void SendOrder(byte[] data, string order, string[] paramList)
        {
            this.Data = data;
            this.MainOrder = order;
            if (this.Data != null)
            {
                this.PackCount = this.Data.Length / PACKSIZE;
                this.PackCount += (this.Data.Length % PACKSIZE == 0) ? 0 : 1;
                this.PackIndex = 0;
            }
            List<byte> pack = new List<byte>();
            List<byte> packHead = new List<byte>();
            List<byte> packData = new List<byte>();
            packData.AddRange(Encoding.Default.GetBytes(this.MainOrder));//添加命令
            if (paramList != null)
            {
                for (int i = 0; i < paramList.Length; i++)
                {
                    packData.Add(PARAM_SEPARATOR);//添加参数分隔符
                    packData.AddRange(Encoding.Default.GetBytes(paramList[i]));//添加参数
                }
                packData.Add(PARAM_TERMINATOR);
            }
            packHead.Add(PACKFLAG1);//添加标记位1
            packHead.Add(PACKFLAG2);//添加标记位2
            packHead.Add(Convert.ToByte(this.DeviceAddr));//添加地址位
            packHead.Add(Convert.ToByte(packData.Count & 0xFF));//添加数据包长度前8位
            packHead.Add(Convert.ToByte((packData.Count >> 8) & 0xFF));//添加数据包长度后8位
            packHead.Add(this.GetOrderMark());//添加标记位
            packHead.Add(PLACEHOLDER);//添加通信包CRC前8位占位符
            packHead.Add(PLACEHOLDER);//添加通信包CRC后8位占位符
            pack.AddRange(packHead);//通信包添加包头
            pack.AddRange(packData);//通信包添加包体
            byte[] packCRC = CRCTools.GetInstance().GetCRC(pack.ToArray());//获取通信包16位CRC校验码
            pack[6] = packCRC[0];//添加通信包CRC前8位
            pack[7] = packCRC[1];//添加通信包CRC后8位
            Console.WriteLine("发送数据为:" + Encoding.Default.GetString(packData.ToArray()));
            this.Send(pack.ToArray());
        }
        /// <summary>
        /// 发送命令通信包，命令带额外数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="order"></param>
        /// <param name="orderData"></param>
        /// <param name="paramList"></param>
        private void SendOrder(byte[] data, string order, byte[] param)
        {
            this.Data = data;
            this.MainOrder = order;
            if (this.Data != null)
            {
                this.PackCount = this.Data.Length / PACKSIZE;
                this.PackCount += (this.Data.Length % PACKSIZE == 0) ? 0 : 1;
                this.PackIndex = 0;
            }
            List<byte> pack = new List<byte>();
            List<byte> packHead = new List<byte>();
            List<byte> packData = new List<byte>();
            packData.AddRange(Encoding.Default.GetBytes(this.MainOrder));//添加命令
            if (param != null)
            {
                packData.AddRange(param);
            }
            packHead.Add(PACKFLAG1);//添加标记位1
            packHead.Add(PACKFLAG2);//添加标记位2
            packHead.Add(Convert.ToByte(this.DeviceAddr));//添加地址位
            packHead.Add(Convert.ToByte(packData.Count & 0xFF));//添加数据包长度前8位
            packHead.Add(Convert.ToByte((packData.Count >> 8) & 0xFF));//添加数据包长度后8位
            packHead.Add(this.GetOrderMark());//添加标记位
            packHead.Add(PLACEHOLDER);//添加通信包CRC前8位占位符
            packHead.Add(PLACEHOLDER);//添加通信包CRC后8位占位符
            pack.AddRange(packHead);//通信包添加包头
            pack.AddRange(packData);//通信包添加包体
            byte[] packCRC = CRCTools.GetInstance().GetCRC(pack.ToArray());//获取通信包16位CRC校验码
            pack[6] = packCRC[0];//添加通信包CRC前8位
            pack[7] = packCRC[1];//添加通信包CRC后8位
            this.Send(pack.ToArray());
        }
        /// <summary>
        /// 发送数据包
        /// </summary>
        private void SendData()
        {
            this.PackIndex++;
            List<byte> pack = new List<byte>();
            List<byte> packHead = new List<byte>();
            List<byte> packData = new List<byte>();
            if (this.PackIndex == this.PackCount)
            {
                for (int i = 0; i < this.Data.Length - (this.PackIndex - 1) * PACKSIZE; i++)
                {
                    packData.Add(this.Data[(this.PackIndex - 1) * PACKSIZE + i]);//添加最后一包数据包数据
                }
            }
            else
            {
                for (int i = 0; i < PACKSIZE; i++)
                {
                    packData.Add(this.Data[(this.PackIndex - 1) * PACKSIZE + i]);//添加非尾包数据包数据
                }
            }
            packHead.Add(PACKFLAG1);//添加标记位1
            packHead.Add(PACKFLAG2);//添加标记位2
            packHead.Add(Convert.ToByte(this.DeviceAddr));//添加地址位
            packHead.Add(Convert.ToByte(packData.Count & 0xFF));//添加数据包长度前8位
            packHead.Add(Convert.ToByte((packData.Count >> 8) & 0xFF));//添加数据包长度后8位
            packHead.Add(this.GetDataMark());//添加标记位
            packHead.Add(PLACEHOLDER);//添加通信包CRC前8位占位符
            packHead.Add(PLACEHOLDER);//添加通信包CRC后8位占位符
            pack.AddRange(packHead);//通信包添加包头
            pack.AddRange(packData);//通信包添加包体
            byte[] packCRC = CRCTools.GetInstance().GetCRC(pack.ToArray());//获取通信包16位CRC校验码
            pack[6] = packCRC[0];//添加通信包CRC前8位
            pack[7] = packCRC[1];//添加通信包CRC后8位
            //TODO 与占位下载进度显示部分
            string str = "";
            for (int i = 0; i < pack.Count; i++)
            {

            }
            this.Send(pack.ToArray());
        }
        /// <summary>
        /// 接收数据处理
        /// </summary>
        protected void Receive()
        {
            if (ReadBuff.Count >= PACKHEADLENGTH)
            {
                if (ReadBuff[0] == 0xAA && ReadBuff[1] == 0xBB && ReadBuff[2] == 0x00 && ReadBuff[5] == 2)
                {
                    int packDataSize = (ReadBuff[3] & 0xFF) | ((ReadBuff[4] << 8) & 0xFF);
                    if (ReadBuff.Count == packDataSize + PACKHEADLENGTH)
                    {
                        byte[] packCRC = new byte[] { ReadBuff[6], ReadBuff[7] };
                        ReadBuff[6] = 0x00;
                        ReadBuff[7] = 0x00;
                        byte[] CaluPackCRC = CRCTools.GetInstance().GetCRC(ReadBuff.ToArray());
                        if (packCRC[0] == CaluPackCRC[0] && packCRC[1] == CaluPackCRC[1])
                        {
                            List<byte> data =  ReadBuff.Skip(PACKHEADLENGTH).ToList();
                            Console.WriteLine(Encoding.Default.GetString(data.ToArray()));
                            this.ReceiveManege(data);
                            ReadBuff.Clear();
                        }
                    }
                }
                else
                {
                    ReadBuff.Clear();
                }
            }
        }
        /// <summary>
        /// 接收数据事务管理
        /// </summary>
        /// <param name="data"></param>
        private void ReceiveManege(List<byte> data)
        {
            switch (MainOrder)
            {
                case Constant.NEW_DEVICE_LIGHTCONTROL:
                    this.NewDeviceLightControlReceive(data);
                    break;
                case Constant.NEW_DEVICE_CENTRALCONTROL:
                    this.NewDeviceCentralControlReceive(data);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 灯光控制继电器通信接收回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void NewDeviceLightControlReceive(List<byte> data)
        {
            switch (SecondOrder)
            {
                case Order.ZG:
                    this. LightControlConnectReceive(data);
                    break;
                case Order.RG:
                    this.LightControlReadReceive(data);
                    break;
                case Order.DG:
                    this.LightControlDownloadReceive(data);
                    break;
                case Order.YG:
                    this.LightControlDebugReceive(data);
                    break;
            }
        }
        /// <summary>
        /// 灯控连接回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void LightControlConnectReceive(List<byte> data)
        {
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.StopTimeOut();
                this.SendData();

            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals("ack\r\n"))
            {
                this.StopTimeOut();
                this.IsSending = false;
                this.Completed_Event(null);

            }
        }
        /// <summary>
        /// 灯控读取配置数据回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void LightControlReadReceive(List<byte> data)
        {
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.StopTimeOut();
                this.SendData();
            }
            else
            {
                byte[] crcBuff = CRCTools.GetInstance().GetLightControlCRC(data.Take(data.Count - 2).ToArray());
                if (crcBuff[0] == data[data.Count - 2] && crcBuff[1] == data[data.Count - 1])
                {
                    this.StopTimeOut();
                    this.IsSending = false;
                    this.Completed_Event(new LightControlData(data));
                }
                else
                {
                    this.StopTimeOut();
                    this.IsSending = false;
                    this.Error_Event();
                }
            }
        }
        /// <summary>
        /// 灯控下载配置数据回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void LightControlDownloadReceive(List<byte> data)
        {
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.StopTimeOut();
                Thread.Sleep(200);
                this.SendData();
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_ACK))
            {
                this.StopTimeOut();
                this.IsAck = true;
            }
            else
            {
                Console.WriteLine("灯控下载配置数据接收到其他回复消息" + Encoding.Default.GetString(data.ToArray()));
            }
        }
        /// <summary>
        /// 灯控调试数据回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void LightControlDebugReceive(List<byte> data)
        {
            //TODO-LightControlDebugReceive-目前暂无操作
        }
        /// <summary>
        /// 中控通信接收回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void NewDeviceCentralControlReceive(List<byte> data)
        {
            switch (this.SecondOrder)
            {
                case Order.LK:
                    this.CentralControlConnectReceive(data);
                    break;
                case Order.DL:
                    break;
                case Order.CP:
                    this.CentralControlStartCopyReceive(data);
                    break;
                case Order.XP:
                    this.CentralControlStopCopyReceive(data);
                    break;
            }
        }
        /// <summary>
        /// 中控设备链接回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void CentralControlConnectReceive(List<byte> data)
        {
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.StopTimeOut();
                this.SendData();

            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals("ack\r\n"))
            {
                this.StopTimeOut();
                this.IsSending = false;
                this.Completed_Event(null);
            }
        }
        /// <summary>
        /// 中控设备开启解码回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void CentralControlStartCopyReceive(List<byte> data)
        {
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.StopTimeOut();
                this.SendData();
                this.StopTimeOut();
                this.IsStartCopy = true;
                this.IsSending = false;
            }
            else
            {
                if (this.IsStartCopy)
                {
                    this.Completed_Event(data);
                }
                else
                {
                    this.Error_Event();
                }
            }
        }
        /// <summary>
        /// 中控设备关闭解码回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void CentralControlStopCopyReceive(List<byte> data)
        {
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.StopTimeOut();
                this.SendData();
                this.StopTimeOut();
                this.IsStartCopy = false;
                this.IsSending = false;
                this.Completed_Event(null);
            }
            else
            {
                this.Error_Event();
            }
        }
        //灯控设备配置
        /// <summary>
        /// 灯控设备链接
        /// </summary>
        public void LightControlConnect(Completed completed,Error error)
        {
            if (!this.IsSending)
            {
                Console.WriteLine("开始连接灯控设备");
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(LightControlConnectStart), null);
                this.LightControlConnectStart(null);
            }
        }
        /// <summary>
        /// 灯控设备链接执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void LightControlConnectStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.ZG;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_LIGHTCONTROL_CONNECT);
                this.SendOrder(data, Constant.NEW_DEVICE_LIGHTCONTROL, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_CONNECT, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        /// <summary>
        /// 灯控设备读取
        /// </summary>
        public void LightControlRead(Completed completed,Error error)
        {
            if (!this.IsSending)
            {
                Console.WriteLine("开始读取灯控设备配置数据");
                this.StartTimeOut();
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(LightControlReadStart, null);
                this.LightControlReadStart(null);
            }
        }
        /// <summary>
        /// 灯控设备读取执行线程
        /// </summary>
        private void LightControlReadStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.RG;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_LIGHTCONTROL_READ);
                this.SendOrder(data, Constant.NEW_DEVICE_LIGHTCONTROL, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_READ ,data.Length.ToString()});
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        /// <summary>
        /// 灯控设备下载
        /// </summary>
        public void LightControlDownload(LightControlData data,Completed completed,Error error)
        {
            if (!this.IsSending)
            {
                Console.WriteLine("开始下载灯控设备配置数据");
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(LightControlDownloadStart, data);
                this.LightControlDownloadStart(data);
            }
        }
        /// <summary>
        /// 灯控设备下载数据执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void LightControlDownloadStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.DG;
                List<byte> data = new List<byte>();
                data.AddRange(Encoding.Default.GetBytes(Constant.OLD_DEVICE_LIGHTCONTROL_DOWNLOAD));
                this.IsAck = false;
                this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_LIGHTCONTROL, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_DOWNLOAD, data.ToArray().Length.ToString() });
                while (true)
                {
                    if (this.IsAck)
                    {
                        data.Clear();
                        data.AddRange((obj as LightControlData).GetData());
                        this.IsAck = false;
                        this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_LIGHTCONTROL, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_DOWNLOAD, data.ToArray().Length.ToString() });
                        break;
                    }
                    if (IsStopThread)
                    {
                        return;
                    }
                }
                while (true)
                {
                    if (this.IsAck)
                    {
                        this.IsAck = false;
                        this.IsSending = false;
                        this.StopTimeOut();
                        this.Completed_Event(null);
                        break;
                    }
                    if (IsStopThread)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        /// <summary>
        /// 灯控设备调试
        /// </summary>
        public void LightControlDebug(byte[] data ,Error error)
        {
            if (!this.IsSending)
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Error_Event = error;
                ThreadPool.QueueUserWorkItem(LightControlDebugStart, data);
            }
        }
        /// <summary>
        /// 灯控设备调试线程
        /// </summary>
        /// <param name="obj"></param>
        private void LightControlDebugStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.YG;
                List<byte> data = new List<byte>();
                data.AddRange(Encoding.Default.GetBytes(Constant.OLD_DEVICE_LIGHTCONTROL_DEBUG));
                data.AddRange(obj as byte[]);
                this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_LIGHTCONTROL, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_DEBUG, data.ToArray().Length.ToString() });
                Thread.Sleep(500);
                this.SendData();
                this.IsSending = false;
                this.TimeOutTimer.Stop();
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        //中控设备配置
        /// <summary>
        /// 中控设备连接
        /// </summary>
        /// <param name="completed"></param>
        /// <param name="error"></param>
        public void CentralControlConnect(Completed completed,Error error)
        {
            if (!this.IsSending)
            {
                Console.WriteLine("开始连接中控设备");
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(CentralControlConnectStart), null);
                this.CentralControlConnectStart(null);
            }
        }
        /// <summary>
        /// 中控设备链接执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void CentralControlConnectStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.LK;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_CENTRALCONTROL_CONNECT);
                this.SendOrder(data, Constant.NEW_DEVICE_CENTRALCONTROL, new string[] { Constant.OLD_DEVICE_CENTRALCONTROL_CONNECT, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        /// <summary>
        /// 中控设备开启解码
        /// </summary>
        public void CentralControlStartCopy(Completed completed,Error error)
        {
            if (!this.IsSending)
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(CentralControlStartCopyStart), null);
                CentralControlStartCopyStart(null);
            }
        }
        /// <summary>
        /// 中控设备开启解码执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void CentralControlStartCopyStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.CP;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_CENTRALCONTROL_START_STUDY);
                this.SendOrder(data, Constant.NEW_DEVICE_CENTRALCONTROL, new string[] { Constant.OLD_DEVICE_CENTRALCONTROL_START_STUDY, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        /// <summary>
        /// 中控设备关闭解码
        /// </summary>
        /// <param name="completed"></param>
        /// <param name="error"></param>
        public void CentralControlStopCopy(Completed completed,Error error)
        {
            if (!this.IsSending)
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(CentralControlStopCopyStart), null);
                this.CentralControlStopCopyStart(null);
            }
        }
        /// <summary>
        /// 中控设备关闭解码执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void CentralControlStopCopyStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.XP;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_CENTRALCONTROL_STOP_STUDY);
                this.SendOrder(data, Constant.NEW_DEVICE_CENTRALCONTROL, new string[] { Constant.OLD_DEVICE_CENTRALCONTROL_STOP_STUDY, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = true;
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        //墙板设备配置
    }
    enum Order
    {
        ZG,RG,DG,YG,ZC,RC,DC,LK,DL,CP,XP
    }
}
