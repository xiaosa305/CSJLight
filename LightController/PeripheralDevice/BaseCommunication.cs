using LightController.Ast;
using LightController.Common;
using LightController.Entity;
using LightController.Tools;
using LightController.Tools.CSJ;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace LightController.PeripheralDevice
{
    public abstract class BaseCommunication
    {
        protected const int DEFAULT_PACKSIZE = 512;
        protected const double TIMEOUT = 4000;//超时等待时长
        protected const int UDPADDR = 255;
        protected const int PACKHEADLENGTH = 8;//协议头大小
        protected const byte PACKFLAG1 = 0xAA;//协议标记1
        protected const byte PACKFLAG2 = 0xBB;//协议标记2
        protected const byte PARAM_SEPARATOR = 0x20;//通信命令参数分割符
        protected const byte PARAM_TERMINATOR = 0x00;//通信命令参数分割符
        protected const byte PLACEHOLDER = 0x00;//占位符
        protected int PackSize { get; set; } //通信分包大小
        protected int DeviceAddr { get; set; }//设备地址
        protected static List<byte> ReadBuff = new List<byte>();//接收缓存
        protected Order SecondOrder { get; set; }//二级命令
        protected string MainOrder { get; set; }//主命令
        protected bool IsAck { get; set; }//回复确认标记
        protected bool IsSending { get; set; }//发送进行中标记
        protected bool IsStartCopy { get; set; }
        protected System.Timers.Timer TimeOutTimer { get; set; }//超时定时器
        protected bool IsStopThread { get; set; }//终止线程继续执行标记
        protected byte[] Data { get; set; }//数据
        protected int PackCount { get; set; }//通信分包总数
        protected int PackIndex { get; set; }//当前发送分包编号
        protected bool IsCenterControlDownload { get; set; }
        protected bool IsDone { get; set; }

        public delegate void Completed(Object obj,string message);
        public delegate void Error(string message);
        public delegate void KeyPressClick(Object obj);
        public delegate void CopyListener(Object obj);
        public event Completed Completed_Event;
        public event Error Error_Event;
        protected event KeyPressClick KeyPressClick_Event;
        protected event CopyListener CopyListener_Event;


        //910灯控新增参数
        protected const int PACKAGESIZE = 512;//数据包大小
        protected System.Timers.Timer TransactionTimer { get; set; }//灯控操作执行定时器
        public delegate void Progress(string filename, int progress);//进度更新事件委托
        protected event Progress ProgressEvent;//进度更新事件
        protected long DownloadFileToTalSize { get; set; }//工程项目文件总大小
        protected long CurrentDownloadCompletedSize { get; set; }//当前文件大小
        protected string CurrentFileName { get; set; }//当前下载文件名称
        protected bool DownloadProjectStatus { get; set; }//下载状态标记




        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        protected abstract void Send(byte[] data);
        /// <summary>
        /// 断开连接
        /// </summary>
        public abstract void DisConnect();
        /// <summary>
        /// 功能：获取连接状态
        /// </summary>
        /// <returns></returns>
        public abstract bool IsConnected();

        public abstract bool Connect(NetworkDeviceInfo deviceInfo);

        public abstract bool OpenSerialPort(string portName);

        /// <summary>
        /// 初始化
        /// </summary>
        protected void Init()
        {
            this.IsStopThread = false;
            this.IsSending = false;
            this.IsStartCopy = false;
            this.PackSize = DEFAULT_PACKSIZE;
            this.IsCenterControlDownload = false;
            this.DownloadProjectStatus = false;
        }
        /// <summary>
        /// 发送数据完成
        /// </summary>
        protected void SendDataCompleted()
        {
            this.StartTimeOut();
            if (this.MainOrder != null)
            {
                if (this.MainOrder.Equals(Constant.ORDER_PUT) || this.MainOrder.Equals(Constant.ORDER_UPDATE))
                {
                    int progress = Convert.ToInt32(this.CurrentDownloadCompletedSize / (this.DownloadFileToTalSize * 1.0) * 100);
                    this.ProgressEvent(this.CurrentFileName, progress);
                }
            }
        }
        /// <summary>
        /// 定时器执行语句
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendTimeOut(object sender, ElapsedEventArgs e)
        {
           
            if (this.SecondOrder == Order.STOP_INTENT_PREVIEW)
            {

            }
            //else if (this.SecondOrder == Order.SERVER_SET_SESSION_ID || this.SecondOrder == Order.SERVER_BIND_DEVICE || this.SecondOrder == Order.SERVER_CHANGE_BIND_DEVICE || this.SecondOrder == Order.SERVER_UNBIND_DEVICE || this.SecondOrder == Order.SERVER_GET_DEVICES)
            //{
            //    ;
            //}
            else
            {
                this.IsStopThread = true;
                this.IsSending = false;
                LogTools.Debug(Constant.TAG_XIAOSA, "操作命令超时,主命令：" + this.MainOrder + ",副命令：" + this.SecondOrder);
                this.CommandFailed("通信超时");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 启动定时器计时
        /// </summary>
        protected void StartTimeOut()
        {
            //LogTools.Debug(Constant.TAG_XIAOSA, "启动超时处理定时器");
            if (this.TimeOutTimer == null)
            {
                this.TimeOutTimer = new System.Timers.Timer(TIMEOUT);
                this.TimeOutTimer.Elapsed += SendTimeOut;
                this.TimeOutTimer.AutoReset = false;
            }
            this.TimeOutTimer.Start();
        }
        /// <summary>
        /// 停止定时器计时
        /// </summary>
        protected void StopTimeOut()
        {
            if (TimeOutTimer != null)
            {
                Console.WriteLine("关闭超时");
                //LogTools.Debug(Constant.TAG_XIAOSA, "关闭超时定时器");
                TimeOutTimer.Stop();
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
                case Constant.NEW_DEVICE_PASSTHROUGH:
                    result = this.GetPassThroughMark();
                    break;
                case Constant.ORDER_BEGIN_SEND:
                    result = this.GetOrderForBeginSendMark();
                    break;
                case Constant.ORDER_PUT:
                    result = this.GetOrderForPutMark();
                    break;
                case Constant.ORDER_END_SEND:
                    result = this.GetOrderForEndSendMark();
                    break;
                case Constant.ORDER_PUT_PARAM:
                    result = this.GetOrderForPutParamMark();
                    break;
                case Constant.ORDER_GET_PARAM:
                    result = this.GetOrderForGetParamMark();
                    break;
                case Constant.ORDER_UPDATE:
                    result = this.GetOrderForUpdateDeviceSystemMark();
                    break;
                case Constant.ORDER_START_DEBUG:
                    result = this.GetOrderForStartIntentPreviewMark();
                    break;
                case Constant.ORDER_END_DEBUG:
                    result = this.GetOrderForStopIntentPreviewMark();
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
                case Order.DK:
                case Order.CP:
                case Order.XP:
                    result = 0x05;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 获取透传模式操作命令对应标记位
        /// </summary>
        /// <returns></returns>
        private Byte GetPassThroughMark()
        {
            byte result = 0x00;
            switch (SecondOrder)
            {
                case Order.ZG:
                case Order.RG:
                case Order.DG:
                case Order.YG:
                case Order.ZC:
                case Order.RC:
                case Order.DC:
                case Order.LK:
                case Order.DK:
                case Order.CP:
                case Order.XP:
                    result = 0x05;
                    break;
            }
            return result;
        }

        //获取灯控功能操作命令对应标记位
        /// <summary>
        /// 功能：获取BeginSend标记位
        /// </summary>
        /// <returns></returns>
        private Byte GetOrderForBeginSendMark()
        {
            return Convert.ToByte(Constant.MARK_ORDER_NO_TAKE_DATA, 2);
        }
        /// <summary>
        /// 功能：获取Put标记位
        /// </summary>
        /// <returns></returns>
        private Byte GetOrderForPutMark()
        {
            return Convert.ToByte(Constant.MARK_ORDER_TAKE_DATA, 2);
        }
        /// <summary>
        /// 功能：获取EndSend标记位
        /// </summary>
        /// <returns></returns>
        private Byte GetOrderForEndSendMark()
        {
            return Convert.ToByte(Constant.MARK_ORDER_NO_TAKE_DATA, 2);
        }
        /// <summary>
        /// 功能：获取PutParam标记位
        /// </summary>
        /// <returns></returns>
        private Byte GetOrderForPutParamMark()
        {
            return Convert.ToByte(Constant.MARK_ORDER_TAKE_DATA, 2);

        }
        /// <summary>
        /// 功能：获取GetParam标记位
        /// </summary>
        /// <returns></returns>
        private Byte GetOrderForGetParamMark()
        {
            return Convert.ToByte(Constant.MARK_ORDER_NO_TAKE_DATA, 2);
        }
        /// <summary>
        /// 功能：获取UpdateDeviceSystem标记位
        /// </summary>
        /// <returns></returns>
        private Byte GetOrderForUpdateDeviceSystemMark()
        {
            return Convert.ToByte(Constant.MARK_ORDER_TAKE_DATA, 2);

        }
        /// <summary>
        /// 功能：获取StartIntentPreview标记位
        /// </summary>
        /// <returns></returns>
        private Byte GetOrderForStartIntentPreviewMark()
        {
            return Convert.ToByte(Constant.MARK_ORDER_NO_TAKE_DATA, 2);
        }
        /// <summary>
        /// 功能：获取StopIntentPreview标记位
        /// </summary>
        /// <returns></returns>
        private Byte GetOrderForStopIntentPreviewMark()
        {
            return Convert.ToByte(Constant.MARK_ORDER_NO_TAKE_DATA, 2);
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
                this.PackCount = this.Data.Length / PackSize;
                this.PackCount += (this.Data.Length % PackSize == 0) ? 0 : 1;
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
            CommandLogUtils.GetInstance().Enqueue("ORDER ::::  MainOrder: " + this.MainOrder + ";   SecondOrder:" + this.SecondOrder + ";   PackageIndex：" + this.PackIndex + ";    PackageCount" + this.PackCount + "\n");
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
                for (int i = 0; i < this.Data.Length - (this.PackIndex - 1) * this.PackSize; i++)
                {
                    packData.Add(this.Data[(this.PackIndex - 1) * this.PackSize + i]);//添加最后一包数据包数据
                }
            }
            else
            {
                for (int i = 0; i < this.PackSize; i++)
                {
                    packData.Add(this.Data[(this.PackIndex - 1) * this.PackSize + i]);//添加非尾包数据包数据
                }
            }
            //中控协议下载分包处理
            if (this.IsCenterControlDownload)
            {
                byte[] crc = CRCTools.GetInstance().GetLightControlCRC(packData.ToArray());
                packData.AddRange(crc);
                //LogTools.Debug(Constant.TAG_XIAOSA, "CurrentPack:" + this.PackIndex + ",PackCount:" + this.PackCount + "，CurrentPackSize:" + packData.Count);
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

            //TODO 下载进度显示部分
            if (this.MainOrder.Equals(Constant.ORDER_PUT) || this.MainOrder.Equals(Constant.ORDER_UPDATE))
            {
                this.CurrentDownloadCompletedSize += packData.Count();
            }
            CommandLogUtils.GetInstance().Enqueue("DATA ::::  MainOrder: " + this.MainOrder + ";   SecondOrder:" + this.SecondOrder + ";   PackageIndex：" + this.PackIndex + ";    PackageCount" + this.PackCount + "\n");
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
                            List<byte> data = ReadBuff.Skip(PACKHEADLENGTH).ToList();
                            //LogTools.Debug(Constant.TAG_XIAOSA, "Receive Data:" + Encoding.Default.GetString(data.ToArray()));
                            this.ReceiveManege(data);
                            ReadBuff.Clear();
                        }
                    }
                }
                //else if (ReadBuff[0] == 0xBB && ReadBuff[1] == 0xAA && ReadBuff.Count >= 4)
                //{
                //    byte[] data = new byte[ReadBuff.Count];
                //    ReadBuff.CopyTo(data);
                //    this.ServerReceiveManager(data);
                //}
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
                case Constant.NEW_DEVICE_PASSTHROUGH:
                    this.PassThroughReceive(data);
                    break;
                case Constant.ORDER_BEGIN_SEND:
                case Constant.ORDER_PUT:
                case Constant.ORDER_END_SEND:
                    this.DownloadProjectReceiveManager(data);
                    break;
                case Constant.ORDER_PUT_PARAM:
                    this.PutParamReceiveManager(data);
                    break;
                case Constant.ORDER_GET_PARAM:
                    this.GetParamReceiveManager(data);
                    break;
                case Constant.ORDER_UPDATE:
                    this.UpdateDeviceSystemReceiveManager(data);
                    break;
                case Constant.ORDER_START_DEBUG:
                    this.StartIntentPreviewReceiveManager(data);
                    break;
                case Constant.ORDER_END_DEBUG:
                    this.StopIntentPreviewReceiveManager(data);
                    break;
            }
        }

        protected void CommandSuccessed(Object obj,string msg)
        {
            this.SecondOrder = Order.NULL;
            this.IsSending = false;
            this.Completed_Event(obj,msg);
        }

        protected void CommandFailed(string msg)
        {
            this.SecondOrder = Order.NULL;
            this.IsSending = false;
            this.Error_Event(msg);
        }

        protected void SetCompletedEvent(Completed completed)
        {
            this.Completed_Event = completed;
        }

        protected void SetErrordEvent(Error error)
        {
            this.Error_Event = error;
        }

        //透传回复管理
        private void PassThroughReceive(List<byte> data)
        {
            if (data.Count == 2 && data[0] + data[1] == 0xFF)
            {
                this.KeyPressClickListenerReceive(data);
            }
            else
            {
                switch (this.SecondOrder)
                {
                    case Order.ZG:
                        this.LightControlConnectReceive(data);
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
                    case Order.ZC:
                        this.KeyPressConnectReceive(data);
                        break;
                    case Order.RC:
                        this.KeyPressReadReceive(data);
                        break;
                    case Order.DC:
                        this.KeyPressDownloadReceive(data);
                        break;
                    case Order.LK:
                        this.CenterControlConnectReceive(data);
                        break;
                    case Order.DK:
                        this.CenterControlDownloadReceive(data);
                        break;
                    case Order.CP:
                        this.CenterControlStartCopyReceive(data);
                        break;
                    case Order.XP:
                        this.CenterControlStopCopyReceive(data);
                        break;
                }
            }
        }

        //灯控回复管理
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
                this.IsAck = true;
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
            {
                this.IsDone = true;
            }

            if (this.IsAck &&this.IsDone)
            {
                this.StopTimeOut();
                this.IsSending = false;
                this.Completed_Event(null,"灯控连接成功");
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
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
            {
                this.IsDone = true;
            }
            else
            {
                if (this.IsDone == true)
                {
                    byte[] crcBuff = CRCTools.GetInstance().GetLightControlCRC(data.Take(data.Count - 2).ToArray());
                    if (crcBuff[0] == data[data.Count - 2] && crcBuff[1] == data[data.Count - 1])
                    {
                        this.StopTimeOut();
                        this.IsSending = false;
                        LightControlData value = new LightControlData(data);
                        this.Completed_Event(value,"灯控读取配置数据成功");
                    }
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
                this.SendData();
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_ACK))
            {
                if (this.IsDone)
                {
                    this.StopTimeOut();
                }
                this.IsAck = true;
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
            {
                if (this.IsAck)
                {
                    this.StopTimeOut();
                }
                this.IsDone = true;
            }
        }
        /// <summary>
        /// 灯控调试数据回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void LightControlDebugReceive(List<byte> data)
        {
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.StopTimeOut();
                this.SendData();
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
            {
                this.StopTimeOut();
                this.IsSending = false;
                this.Completed_Event(null,"灯控调试数据发送成功");
            }
        }

        //中控回复管理
        /// <summary>
        /// 中控通信接收回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void NewDeviceCentralControlReceive(List<byte> data)
        {
            switch (this.SecondOrder)
            {
                case Order.LK:
                    this.CenterControlConnectReceive(data);
                    break;
                case Order.DK:
                    this.CenterControlDownloadReceive(data);
                    break;
                case Order.CP:
                    this.CenterControlStartCopyReceive(data);
                    break;
                case Order.XP:
                    this.CenterControlStopCopyReceive(data);
                    break;
            }
        }
        /// <summary>
        /// 中控设备链接回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void CenterControlConnectReceive(List<byte> data)
        {
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.StopTimeOut();
                this.SendData();
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals("ack\r\n"))
            {
                this.IsAck = true;
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
            {
                this.IsDone = true;
            }
            if (this.IsDone && this.IsAck)
            {
                this.StopTimeOut();
                this.IsAck = false;
                this.IsDone = false;
                this.IsSending = false;
                this.Completed_Event(null,"中控设备连接成功");
            }
        }
        /// <summary>
        /// 中控设备开启解码回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void CenterControlStartCopyReceive(List<byte> data)
        {
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.StopTimeOut();
                this.SendData();
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
            {
                this.IsDone = true;
            }
            else
            {
                if (this.IsStartCopy)
                {
                    this.CopyListener_Event(data);
                }
                else
                {
                    this.Error_Event("中控设备未启动解码模式");
                }
            }
            if (this.IsDone)
            {
                this.StopTimeOut();
                this.IsStartCopy = true;
                this.IsSending = false;
                this.IsDone = false;
                this.Completed_Event(null,"中控设备启动解码模式成功");
            }
        }
        /// <summary>
        /// 中控设备关闭解码回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void CenterControlStopCopyReceive(List<byte> data)
        {
            this.StopTimeOut();
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.SendData();
                LogTools.Debug(Constant.TAG_XIAOSA, Constant.RECEIVE_ORDER_PUT);

            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
            {
                //this.IsDone = true;
                this.IsStartCopy = false;
                this.IsSending = false;
                this.IsDone = false;
                this.Completed_Event(null, "中控设备关闭解码模式成功");
                //LogTools.Debug(Constant.TAG_XIAOSA, "中控设备关闭解码模式成功");
            }
            else
            {
                this.Error_Event("中控设备关闭解码模式失败");
                LogTools.Debug(Constant.TAG_XIAOSA, "中控设备关闭解码模式失败");
            }
        }
        /// <summary>
        /// 中控设备下载回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void CenterControlDownloadReceive(List<byte> data)
        {
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.StopTimeOut();
                this.SendData();
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
            {
                if (this.IsAck)
                {
                    this.StopTimeOut();
                }
                this.IsDone = true;
                //if (this.PackIndex > 0 && this.PackIndex == this.PackCount)
                //{
                //    this.IsAck = true;
                //    this.StopTimeOut();
                //}
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_SENDNEXT))
            {
                if (this.IsAck)
                {
                    this.StopTimeOut();
                }
                this.IsDone = true;
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_ACK))
            {
                if (this.IsDone)
                {
                    this.StopTimeOut();
                }
                this.IsAck = true;
                //if (this.PackIndex > 0 && this.PackIndex == this.PackCount)
                //{
                //    //this.IsAck = true;
                //    this.StopTimeOut();
                //}
                //else
                //{
                //    this.IsAck = true;
                //}
            }
        }

        //墙板回复管理
        /// <summary>
        /// 墙板设备连接回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void KeyPressConnectReceive(List<byte> data)
        {
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.StopTimeOut();
                this.SendData();
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals("ack\r\n"))
            {
                this.IsAck = true;
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
            {
                this.IsDone = true;
            }
            if (IsAck && IsDone)
            {
                this.StopTimeOut();
                this.IsSending = false;
                this.Completed_Event(null,"墙板设备连接成功");
            }
        }
        /// <summary>
        /// 墙板设备读取配置数据回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void KeyPressReadReceive(List<byte> data)
        {
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.StopTimeOut();
                this.SendData();
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
            {
                this.IsAck = true;
                this.IsDone = true;
            }
            else
            {
                byte[] crcBuff = CRCTools.GetInstance().GetLightControlCRC(data.Take(data.Count - 2).ToArray());
                if (crcBuff[0] == data[data.Count - 2] && crcBuff[1] == data[data.Count - 1])
                {
                    this.StopTimeOut();
                    this.IsSending = false;
                    this.Completed_Event(new KeyEntity(data),"读取墙壁配置数据成功");
                }
            }
        }
        /// <summary>
        /// 墙板设备下载配置数据回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void KeyPressDownloadReceive(List<byte> data)
        {
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT))
            {
                this.StopTimeOut();
                this.SendData();
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_ACK))
            {
                if (this.IsDone)
                {
                    this.StopTimeOut();
                }
                this.IsAck = true;
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
            {
                if (this.IsAck)
                {
                    this.StopTimeOut();

                }
                this.IsDone = true;
            }
        }
        /// <summary>
        /// 墙板设备监听点击事件回复消息处理
        /// </summary>
        /// <param name="data"></param>
        private void KeyPressClickListenerReceive(List<byte> data)
        {
            if (this.KeyPressClick_Event != null)
            {
                this.KeyPressClick_Event(data);
            }
        }

        //灯控设备配置
        /// <summary>
        /// 灯控设备链接
        /// </summary>
        public void LightControlConnect(Completed completed,Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
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
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data, Constant.NEW_DEVICE_LIGHTCONTROL, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_CONNECT, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                LogTools.Error(Constant.TAG_XIAOSA, "灯控设备链接失败", ex);
            }
        }
        /// <summary>
        /// 灯控设备读取
        /// </summary>
        public void LightControlRead(Completed completed,Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                //this.StartTimeOut();
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
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data, Constant.NEW_DEVICE_LIGHTCONTROL, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_READ ,data.Length.ToString()});
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                LogTools.Error(Constant.TAG_XIAOSA, "灯控设备读取数据失败", ex);
            }
        }
        /// <summary>
        /// 灯控设备下载
        /// </summary>
        public void LightControlDownload(LightControlData data,Completed completed,Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
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
                this.IsDone = false;
                this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_LIGHTCONTROL, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_DOWNLOAD, data.ToArray().Length.ToString() });
                while (true)
                {
                    if (this.IsAck && this.IsDone)
                    {
                        data.Clear();
                        data.AddRange((obj as LightControlData).GetData());
                        this.IsAck = false;
                        this.IsDone = false;
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
                    if (this.IsAck && this.IsDone)
                    {
                        this.IsAck = false;
                        this.IsDone = false;
                        this.IsSending = false;
                        this.StopTimeOut();
                        this.Completed_Event(null,"灯控设备下载数据成功");
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
                LogTools.Error(Constant.TAG_XIAOSA, "下载数据到灯控设备失败", ex);
            }
        }
        /// <summary>
        /// 灯控设备调试
        /// </summary>
        public void LightControlDebug(byte[] data ,Completed completed , Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Error_Event = error;
                this.Completed_Event = completed;
                //ThreadPool.QueueUserWorkItem(LightControlDebugStart, data);
                this.LightControlDebugStart(data);
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
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_LIGHTCONTROL, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_DEBUG, data.ToArray().Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                LogTools.Error(Constant.TAG_XIAOSA, "发送调试数据到灯控设备失败", ex);
            }
        }

        //中控设备配置
        /// <summary>
        /// 中控设备连接
        /// </summary>
        /// <param name="completed"></param>
        /// <param name="error"></param>
        public void CenterControlConnect(Completed completed,Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(CentralControlConnectStart), null);
                this.CenterControlConnectStart(null);
            }
        }
        /// <summary>
        /// 中控设备链接执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void CenterControlConnectStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.LK;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_CENTRALCONTROL_CONNECT);
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data, Constant.NEW_DEVICE_CENTRALCONTROL, new string[] { Constant.OLD_DEVICE_CENTRALCONTROL_CONNECT, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                LogTools.Error(Constant.TAG_XIAOSA, "连接中控设备失败", ex);
            }
        }
        /// <summary>
        /// 中控设备开启解码
        /// </summary>
        public void CenterControlStartCopy(Completed completed,Error error,CopyListener copyListener)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                this.CopyListener_Event = copyListener;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(CentralControlStartCopyStart), null);
                CenterControlStartCopyStart(null);
            }
        }
        /// <summary>
        /// 中控设备开启解码执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void CenterControlStartCopyStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.CP;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_CENTRALCONTROL_START_STUDY);
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data, Constant.NEW_DEVICE_CENTRALCONTROL, new string[] { Constant.OLD_DEVICE_CENTRALCONTROL_START_STUDY, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                LogTools.Error(Constant.TAG_XIAOSA, "中控设备开启解码失败", ex);
            }
        }
        /// <summary>
        /// 中控设备关闭解码
        /// </summary>
        /// <param name="completed"></param>
        /// <param name="error"></param>
        public void CenterControlStopCopy(Completed completed,Error error)
        {
            //TODO XIAOSA:添加测试
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(CentralControlStopCopyStart), null);
                this.CenterControlStopCopyStart(null);
            }
        }
        /// <summary>
        /// 中控设备关闭解码执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void CenterControlStopCopyStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.XP;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_CENTRALCONTROL_STOP_STUDY);
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data, Constant.NEW_DEVICE_CENTRALCONTROL, new string[] { Constant.OLD_DEVICE_CENTRALCONTROL_STOP_STUDY, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = true;
                LogTools.Error(Constant.TAG_XIAOSA, "中控设备关闭解码失败", ex);
            }
        }
        /// <summary>
        /// 中控设备下载协议数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="completed"></param>
        /// <param name="error"></param>
        public void CenterControlDownload(CCEntity entity,Completed completed,Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                this.CenterControlDownloadStart(entity);
            }
        }
        /// <summary>
        /// 中控设备下载协议数据执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void CenterControlDownloadStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.DK;
                List<byte> data = new List<byte>();
                data.AddRange(Encoding.Default.GetBytes(Constant.OLD_DEVICE_CENTRALCONTROL_DOWNLOAD));
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_CENTRALCONTROL, new string[] { Constant.OLD_DEVICE_CENTRALCONTROL_DOWNLOAD, data.ToArray().Length.ToString() });
                while (true)
                {
                    if (this.IsAck && this.IsDone)
                    {
                        data.Clear();
                        data.AddRange((obj as CCEntity).GetData());
                        this.IsAck = false;
                        this.IsDone = false;
                        int dataLength = data.ToArray().Length + (2 * 32);
                        this.IsCenterControlDownload = true;
                        this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_CENTRALCONTROL, new string[] { Constant.OLD_DEVICE_CENTRALCONTROL_DOWNLOAD, dataLength.ToString() });
                        break;
                    }
                    if (IsStopThread)
                    {
                        return;
                    }
                }
                while (true)
                {
                    if (this.IsAck && this.IsDone)
                    {
                        this.IsAck = false;
                        this.IsDone = false;
                        Console.WriteLine("包序：" + PackIndex);
                        if (this.PackIndex == this.PackCount)
                        {
                            this.IsSending = false;
                            this.IsCenterControlDownload = false;
                            this.Completed_Event(null,"中控设备下载协议数据成功");
                            break;
                        }
                        else if (this.PackIndex < this.PackCount)
                        {
                            this.SendData();
                        }
                    }
                    if (this.IsStopThread)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "下载协议数据到中控设备失败", ex);
            }
        }

        //透传模式墙板设备配置
        /// <summary>
        /// 透传模式墙板设备连接
        /// </summary>
        /// <param name="completed"></param>
        /// <param name="error"></param>
        public void PassThroughKeyPressConnect(Completed completed,Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(PassThroughKeyPressConnectStart), null);
                this.PassThroughKeyPressConnectStart(null);
            }
        }
        /// <summary>
        /// 透传模式墙板设备连接执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void PassThroughKeyPressConnectStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.ZC;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_KEYPRESS_CONNECT);
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data, Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_KEYPRESS_CONNECT, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                LogTools.Error(Constant.TAG_XIAOSA, "透传模式链接墙板设备失败", ex);
            }
        }
        /// <summary>
        /// 透传模式墙板设备读取配置数据
        /// </summary>
        /// <param name="completed"></param>
        /// <param name="error"></param>
        public void PassThroughKeyPressRead(Completed completed,Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(PassThroughKeyPressReadStart), null);
                this.PassThroughKeyPressReadStart(null);
            }
        }
        /// <summary>
        /// 透传模式墙板设备读取配置数据执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void PassThroughKeyPressReadStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.RC;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_KEYPRESS_READ);
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data, Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_KEYPRESS_READ, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                LogTools.Error(Constant.TAG_XIAOSA, "透传模式读取墙板设备数据失败", ex);
            }
        }
        /// <summary>
        /// 透传模式墙板设备下载配置数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="completed"></param>
        /// <param name="error"></param>
        public void PassThroughKeyPressDownload(KeyEntity entity,Completed completed,Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(PassThroughKeyPressDownloadStart), entity);
                this.PassThroughKeyPressDownloadStart(entity);
            }
        }
        /// <summary>
        /// 透传模式墙板设备下载配置数据执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void PassThroughKeyPressDownloadStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.DC;
                List<byte> data = new List<byte>();
                data.AddRange(Encoding.Default.GetBytes(Constant.OLD_DEVICE_KEYPRESS_DOWNLOAD));
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_KEYPRESS_DOWNLOAD, data.ToArray().Length.ToString() });
                while (true)
                {
                    if (this.IsStopThread)
                    {
                        return;
                    }
                    if (this.IsAck && this.IsDone)
                    {
                        this.IsAck = false;
                        this.IsDone = false;
                        data.Clear();
                        data.AddRange((obj as KeyEntity).GetData());
                        Thread.Sleep(75);
                        this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_KEYPRESS_DOWNLOAD, data.ToArray().Length.ToString() });
                        break;
                    }
                }
                while (true)
                {
                    if (this.IsStopThread)
                    {
                        return;
                    }
                    if (this.IsAck && this.IsDone)
                    {
                        this.IsAck = false;
                        this.IsDone = false;
                        this.IsSending = false;
                        this.StopTimeOut();
                        this.Completed_Event(null,"透传模式墙板设备下载配置数据成功");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                LogTools.Error(Constant.TAG_XIAOSA, "透传模式下载配置数据到墙板设备失败", ex);
            }
        }
        /// <summary>
        /// 设置墙板按键点击事件监听
        /// </summary>
        /// <param name="click"></param>
        public void PassThroughKeyPressSetClickListener(KeyPressClick click)
        {
            this.KeyPressClick_Event = click;
        }
        /// <summary>
        /// 关闭墙板按键点击事件监听
        /// </summary>
        public void PassThroughKeyPressCloseClickListener()
        {
            this.KeyPressClick_Event = null;
        }

        //透传模式灯控设备配置
        /// <summary>
        /// 透传模式灯控设备链接
        /// </summary>
        public void PassThroughLightControlConnect(Completed completed, Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(PassThroughLightControlConnectStart), null);
                this.PassThroughLightControlConnectStart(null);
            }
        }
        /// <summary>
        /// 透传模式灯控设备链接执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void PassThroughLightControlConnectStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.ZG;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_LIGHTCONTROL_CONNECT);
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data, Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_CONNECT, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                LogTools.Error(Constant.TAG_XIAOSA, "透传模式链接灯控设备失败", ex);
            }
        }
        /// <summary>
        /// 透传模式灯控设备读取
        /// </summary>
        public void PassThroughLightControlRead(Completed completed, Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                //this.StartTimeOut();
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(PassThroughLightControlReadStart, null);
                this.PassThroughLightControlReadStart(null);
            }
        }
        /// <summary>
        /// 透传模式灯控设备读取执行线程
        /// </summary>
        private void PassThroughLightControlReadStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.RG;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_LIGHTCONTROL_READ);
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data, Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_READ, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                LogTools.Error(Constant.TAG_XIAOSA, "透传模式读取灯控设备数据失败", ex);
            }
        }
        /// <summary>
        /// 透传模式灯控设备下载
        /// </summary>
        public void PassThroughLightControlDownload(LightControlData data, Completed completed, Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(PassThroughLightControlDownloadStart, data);
                this.PassThroughLightControlDownloadStart(data);
            }
        }
        /// <summary>
        /// 透传模式灯控设备下载数据执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void PassThroughLightControlDownloadStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.DG;
                List<byte> data = new List<byte>();
                data.AddRange(Encoding.Default.GetBytes(Constant.OLD_DEVICE_LIGHTCONTROL_DOWNLOAD));
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_DOWNLOAD, data.ToArray().Length.ToString() });
                while (true)
                {
                    if (this.IsAck && this.IsDone)
                    {
                        data.Clear();
                        data.AddRange((obj as LightControlData).GetData());
                        this.IsAck = false;
                        this.IsDone = false;
                        this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_DOWNLOAD, data.ToArray().Length.ToString() });
                        break;
                    }
                    if (this.IsStopThread)
                    {
                        return;
                    }
                }
                while (true)
                {
                    if (this.IsAck && this.IsDone)
                    {
                        this.IsAck = false;
                        this.IsSending = false;
                        this.StopTimeOut();
                        this.Completed_Event(null,"透传模式灯控设备下载数据成功");
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
                LogTools.Error(Constant.TAG_XIAOSA, "透传模式下载配置数据到灯控设备失败", ex);
            }
        }
        /// <summary>
        /// 透传模式灯控设备调试
        /// </summary>
        public void PassThroughLightControlDebug(byte[] data,Completed completed, Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Error_Event = error;
                this.Completed_Event = completed;
                //ThreadPool.QueueUserWorkItem(PassThroughLightControlDebugStart, data);
                this.PassThroughLightControlDebugStart(data);
            }
        }
        /// <summary>
        /// 透传模式灯控设备调试线程
        /// </summary>
        /// <param name="obj"></param>
        private void PassThroughLightControlDebugStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.YG;
                List<byte> data = new List<byte>();
                data.AddRange(Encoding.Default.GetBytes(Constant.OLD_DEVICE_LIGHTCONTROL_DEBUG));
                data.AddRange(obj as byte[]);
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_LIGHTCONTROL_DEBUG, data.ToArray().Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                LogTools.Error(Constant.TAG_XIAOSA, "透传模式发送调试数据到灯控设备失败", ex);
            }
        }

        //透传模式中控设备配置
        /// <summary>
        /// 透传模式中控设备连接
        /// </summary>
        /// <param name="completed"></param>
        /// <param name="error"></param>
        public void PassThroughCenterControlConnect(Completed completed, Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(PassThroughCenterControlConnectStart), null);
                this.PassThroughCenterControlConnectStart(null);
            }
        }
        /// <summary>
        /// 透传模式中控设备链接执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void PassThroughCenterControlConnectStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.LK;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_CENTRALCONTROL_CONNECT);
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data, Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_CENTRALCONTROL_CONNECT, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                LogTools.Error(Constant.TAG_XIAOSA, "透传模式链接中控设备失败", ex);
            }
        }
        /// <summary>
        /// 透传模式中控设备开启解码
        /// </summary>
        public void PassThroughCenterControlStartCopy(Completed completed, Error error, CopyListener copyListener)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                this.CopyListener_Event = copyListener;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(PassThroughCenterControlStartCopyStart), null);
                this.PassThroughCenterControlStartCopyStart(null);
            }
        }
        /// <summary>
        /// 透传模式中控设备开启解码执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void PassThroughCenterControlStartCopyStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.CP;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_CENTRALCONTROL_START_STUDY);
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data, Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_CENTRALCONTROL_START_STUDY, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                LogTools.Error(Constant.TAG_XIAOSA, "透传模式中控设备开启解码模式失败", ex);
            }
        }
        /// <summary>
        /// 透传模式中控设备关闭解码
        /// </summary>
        /// <param name="completed"></param>
        /// <param name="error"></param>
        public void PassThroughCenterControlStopCopy(Completed completed, Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(PassThroughCenterControlStopCopyStart), null);
                this.PassThroughCenterControlStopCopyStart(null);
            }
        }
        /// <summary>
        /// 透传模式中控设备关闭解码执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void PassThroughCenterControlStopCopyStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.XP;
                byte[] data = Encoding.Default.GetBytes(Constant.OLD_DEVICE_CENTRALCONTROL_STOP_STUDY);
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data, Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_CENTRALCONTROL_STOP_STUDY, data.Length.ToString() });
            }
            catch (Exception ex)
            {
                this.IsSending = true;
                LogTools.Error(Constant.TAG_XIAOSA, "透传模式中控设备关闭解码模式失败", ex);
            }
        }
        /// <summary>
        /// 透传模式中控设备下载协议数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="completed"></param>
        /// <param name="error"></param>
        public void PassThroughCenterControlDownload(CCEntity entity, Completed completed, Error error)
        {
            if ((!this.IsSending) && this.IsConnected())
            {
                this.IsSending = true;
                this.IsStopThread = false;
                this.Completed_Event = completed;
                this.Error_Event = error;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(PassThroughCenterControlDownloadStart), null);
                this.PassThroughCenterControlDownloadStart(entity);
            }
        }
        /// <summary>
        /// 透传模式中控设备下载协议数据执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void PassThroughCenterControlDownloadStart(Object obj)
        {
            try
            {
                this.SecondOrder = Order.DK;
                List<byte> data = new List<byte>();
                data.AddRange(Encoding.Default.GetBytes(Constant.OLD_DEVICE_CENTRALCONTROL_DOWNLOAD));
                this.IsAck = false;
                this.IsDone = false;
                this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_CENTRALCONTROL_DOWNLOAD, data.ToArray().Length.ToString() });
                while (true)
                {
                    if (this.IsAck && this.IsDone)
                    {
                        data.Clear();
                        data.AddRange((obj as CCEntity).GetData());
                        this.IsAck = false;
                        this.IsDone = false;
                        int dataLength = data.ToArray().Length + (2 * 32);
                        this.IsCenterControlDownload = true;
                        this.SendOrder(data.ToArray(), Constant.NEW_DEVICE_PASSTHROUGH, new string[] { Constant.OLD_DEVICE_CENTRALCONTROL_DOWNLOAD, dataLength.ToString() });
                        break;
                    }
                    if (IsStopThread)
                    {
                        return;
                    }
                }
                while (true)
                {
                    if (this.IsAck && this.IsDone)
                    {
                        this.IsAck = false;
                        this.IsDone = false;
                        if (this.PackIndex == this.PackCount)
                        {
                            this.IsSending = false;
                            this.IsCenterControlDownload = false;
                            this.Completed_Event(null,"透传模式中控设备下载协议数据成功");
                            break;
                        }
                        else if (this.PackIndex < this.PackCount)
                        {
                            this.SendData();
                        }
                    }
                    if (this.IsStopThread)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "透传模式下载协议数据到中控设备失败", ex);
            }
        }

        //910灯控功能整合到新的通信模块
        /// <summary>
        /// 功能：灯光工厂下载更新专属发包处理模块
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataLengtgh"></param>
        /// <param name="isLastPackage"></param>
        private void SendDataPackageForDownload(byte[] data,int dataLengtgh,bool isLastPackage)
        {
            try
            {
                this.PackIndex++;
                List<byte> package = new List<byte>();
                List<byte> packageData = new List<byte>();
                for (int i = 0; i < dataLengtgh; i++)
                {
                    packageData.Add(data[i]);
                }
                byte[] packageDataSize = new byte[]
                {
                    Convert.ToByte(dataLengtgh & 0xFF),
                    Convert.ToByte((dataLengtgh >> 8) & 0xFF)
                };
                byte packageMark = isLastPackage ? Convert.ToByte(Constant.MARK_DATA_END, 2) : Convert.ToByte(Constant.MARK_DATA_NO_END, 2);
                byte[] packageHead = new byte[]
                {
                    Convert.ToByte(0xAA),
                    Convert.ToByte(0xBB),
                    Convert.ToByte(this.DeviceAddr),
                    packageDataSize[0],
                    packageDataSize[1],
                    packageMark,
                    Convert.ToByte(0x00),
                    Convert.ToByte(0x00)
                };
                package.AddRange(packageHead);
                package.AddRange(packageData);
                byte[] packageCRC = CRCTools.GetInstance().GetCRC(package.ToArray());
                package[6] = packageCRC[0];
                package[7] = packageCRC[1];
                if (this.MainOrder.Equals(Constant.ORDER_PUT) || this.MainOrder.Equals(Constant.ORDER_UPDATE))
                {
                    this.CurrentDownloadCompletedSize += packageData.Count();
                }
                this.Send(package.ToArray());
            }
            catch (Exception ex) 
            {
                LogTools.Error(Constant.TAG_XIAOSA, "灯光工程下载数据发送出现异常", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.Error_Event("灯光工程下载更新数据包发送中止");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：灯光工程下载更新
        /// </summary>
        /// <param name="wrapper">数据库数据</param>
        /// <param name="configPath">全局配置文件路径</param>
        /// <param name="completed">成功事件委托</param>
        /// <param name="error">失败事件委托</param>
        public void DownloadProject(Completed completed,Error error,Progress progress)
        {
            try
            {
                if ((!this.IsSending) && this.IsConnected())
                {
                    this.IsSending = true;
                    this.Completed_Event = completed;
                    this.ProgressEvent = progress;
                    this.Error_Event = error;
                    this.CloseTransactionTimer();
                    this.TransactionTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TransactionTimer.Elapsed += new ElapsedEventHandler((s, e) => DownloadProjectStart(s, e));
                    this.TransactionTimer.Start();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "灯光工程下载更新任务启动失败", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.Error_Event("灯光工程下载更新任务启动失败");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：灯光工程下载更新执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void DownloadProjectStart(Object obj, ElapsedEventArgs e)
        {
            this.SecondOrder = Order.DOWNLOAD_PROJECT;
            string projectDirPath = Application.StartupPath + @"\DataCache\Download\CSJ";
            this.CurrentDownloadCompletedSize = 0;
            this.DownloadFileToTalSize = 0;
            string fileName = string.Empty;
            string fileSize = string.Empty;
            string fileCRC = string.Empty;
            byte[] readBuff = new byte[PACKAGESIZE];
            int readSize = 0;
            this.DownloadProjectStatus = false;
            try
            {
                //读取所有文件大小
                if (Directory.Exists(projectDirPath))
                {
                    foreach (string filePath in Directory.GetFileSystemEntries(projectDirPath))
                    {
                        this.DownloadFileToTalSize += (new FileInfo(filePath)).Length;
                    }
                    if (this.DownloadFileToTalSize == 0)
                    {
                        this.IsSending = false;
                        this.Error_Event("灯光工程下载更新失败，没有工程文件");
                        return;
                    }
                    //发送灯光工程更新启动命令
                    this.SendOrder(null, Constant.ORDER_BEGIN_SEND, null);
                    while (true)
                    {
                        if (this.DownloadProjectStatus)
                        {
                            this.DownloadProjectStatus = false;
                            break;
                        }
                        Thread.Sleep(0);
                    }
                    foreach (string filePath in Directory.GetFileSystemEntries(projectDirPath))
                    {
                        FileInfo info = new FileInfo(filePath);
                        fileName = info.Name;
                        fileSize = info.Length.ToString();
                        byte[] crcBuff = CRCTools.GetInstance().GetCRC(filePath);
                        fileCRC = Convert.ToInt32((crcBuff[0] & 0xFF) | ((crcBuff[1] & 0xFF) << 8)) + "";
                        this.CurrentFileName = fileName;
                        this.SendOrder(null, Constant.ORDER_PUT, new string[] { fileName, fileSize, fileCRC });
                        while (true)
                        {
                            if (this.DownloadProjectStatus)
                            {
                                this.DownloadProjectStatus = false;
                                break;
                            }
                            Thread.Sleep(0);
                        }
                        using (FileStream fileStream = info.OpenRead())
                        {
                            while ((readSize = fileStream.Read(readBuff, 0, readBuff.Length)) > 0)
                            {
                                if (readSize < PACKAGESIZE)
                                {
                                    SendDataPackageForDownload(readBuff, readSize, true);
                                }
                                else
                                {
                                    SendDataPackageForDownload(readBuff, readSize, false);
                                }
                                while (true)
                                {
                                    if (this.DownloadProjectStatus)
                                    {
                                        this.DownloadProjectStatus = false;
                                        break;
                                    }
                                    Thread.Sleep(0);
                                }
                            }
                        }
                    }
                    this.SendOrder(null, Constant.ORDER_END_SEND, null);
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "灯光工程下载更新已中止", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.Error_Event("灯光工程下载更新失败");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：更新硬件配置
        /// </summary>
        /// <param name="filePath">硬件配置文件路径</param>
        /// <param name="completed">成功事件委托</param>
        /// <param name="error">失败事件委托</param>
        public void PutParam(string filePath,Completed completed,Error error)
        {
            try
            {
                if ((!this.IsSending) && this.IsConnected())
                {
                    this.IsSending = true;
                    this.Completed_Event = completed;
                    this.Error_Event = error;
                    this.CloseTransactionTimer();
                    this.TransactionTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TransactionTimer.Elapsed += new ElapsedEventHandler((s, e) => PutParamStart(s, e, new PutParamData(filePath)));
                    this.TransactionTimer.Start();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "更新硬件配置任务启动失败", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.Error_Event("更新硬件配置任务启动失败");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：更新硬件配置执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void PutParamStart(Object obj, ElapsedEventArgs e, PutParamData putParamData)
        {
            try
            {
                this.SecondOrder = Order.PUT_PARAM;
                ICSJFile hardWareFile = new CSJ_Hardware(putParamData.FilePath);
                byte[] data = hardWareFile.GetData();
                string fileName = @"Hardware.bin";
                string fileSize = data.Length.ToString();
                byte[] crcBuff = CRCTools.GetInstance().GetCRC(data);
                string fileCrc = Convert.ToInt32((crcBuff[0] & 0xFF) | ((crcBuff[1] & 0xFF) << 8)) + "";
                this.SendOrder(data, Constant.ORDER_PUT_PARAM, new string[] { fileName, fileSize, fileCrc });
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA,"更新硬件配置信息失败",ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.Error_Event("更新硬件配置信息失败");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：读取硬件配置信息
        /// </summary>
        /// <param name="completed">成功事件委托</param>
        /// <param name="error">失败事件委托</param>
        public void GetParam(Completed completed,Error error)
        {
            try
            {
                if ((!this.IsSending) && this.IsConnected())
                {
                    this.IsSending = true;
                    this.Completed_Event = completed;
                    this.Error_Event = error;
                    this.CloseTransactionTimer();
                    this.TransactionTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TransactionTimer.Elapsed += new ElapsedEventHandler((s, e) => GetParamStart(s, e));
                    this.TransactionTimer.Start();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "读取硬件配置任务启动失败", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.Error_Event("读取硬件配置信息任务启动失败");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：读取硬件配置信息执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void GetParamStart(Object obj, ElapsedEventArgs e)
        {
            try
            {
                this.SecondOrder = Order.GET_PARAM;
                this.SendOrder(null, Constant.ORDER_GET_PARAM, null);
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "读取硬件配置信息失败",ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.Error_Event("读取硬件配置信息失败");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：升级硬件系统
        /// </summary>
        /// <param name="completed">成功事件委托</param>
        /// <param name="error">失败事件委托</param>
        public void UpdateDeviceSystem(string filePath, Completed completed,Error error,Progress progress)
        {
            try
            {
                if ((!this.IsSending) && this.IsConnected())
                {
                    this.IsSending = true;
                    this.Completed_Event = completed;
                    this.ProgressEvent = progress;
                    this.Error_Event = error;
                    this.CloseTransactionTimer();
                    this.TransactionTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TransactionTimer.Elapsed += new ElapsedEventHandler((s, e) => UpdateDeviceSystemStart(s, e,new UpdateDeviceSystemData(filePath)));
                    this.TransactionTimer.Start();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "升级硬件系统任务启动失败", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.Error_Event("升级硬件系统任务启动失败");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：升级硬件系统执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateDeviceSystemStart(Object obj, ElapsedEventArgs e,UpdateDeviceSystemData updateDeviceSystemData)
        {
            try
            {
                this.SecondOrder = Order.UPDATE_DEVICE_SYSTEM;
                if (File.Exists(updateDeviceSystemData.FilePath))
                {
                    FileStream fileStream = File.OpenRead(updateDeviceSystemData.FilePath);
                    this.DownloadFileToTalSize = 0;
                    this.CurrentDownloadCompletedSize = 0;
                    byte[] data = new byte[fileStream.Length];
                    fileStream.Read(data, 0, data.Length);
                    string fileSize = data.Length.ToString();
                    this.DownloadFileToTalSize = data.Length;
                    string fileName = "Update.xbin";
                    this.CurrentFileName = fileName;
                    byte[] crc = CRCTools.GetInstance().GetCRC(data);
                    string fileCrc = Convert.ToInt32((crc[0] & 0xFF) | ((crc[1] & 0xFF) << 8)) + "";
                    this.SendOrder(data, Constant.ORDER_UPDATE, new string[] { fileName, fileSize, fileCrc });
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA,"升级硬件系统失败",ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.Error_Event("升级硬件系统失败");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：启动网络调试模式
        /// </summary>
        /// <param name="completed">成功事件委托</param>
        /// <param name="error">失败事件委托</param>
        public void StartIntentPreview(int timeFactory, Completed completed,Error error)
        {
            try
            {
                if ((!this.IsSending) && this.IsConnected())
                {
                    this.IsSending = true;
                    this.Completed_Event = completed;
                    this.Error_Event = error;
                    this.CloseTransactionTimer();
                    this.TransactionTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TransactionTimer.Elapsed += new ElapsedEventHandler((s, e) => StartIntentPreviewStart(s, e,new StartIntentPreviewData(timeFactory)));
                    this.TransactionTimer.Start();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "启动网络调试模式任务启动失败", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.Error_Event("启动网络调试模式任务启动失败");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：启动网络调试模式执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void StartIntentPreviewStart(Object obj, ElapsedEventArgs e,StartIntentPreviewData startIntentPreviewData)
        {
            try
            {
                this.SecondOrder = Order.START_INTENT_PREVIEW;
                this.SendOrder(null, Constant.ORDER_START_DEBUG, new string[]{ Convert.ToString(startIntentPreviewData.TimeFactory)});
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "启动网络调试模式失败", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.Error_Event("启动网络调试模式失败");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：关闭网络调试模式
        /// </summary>
        /// <param name="completed">成功事件委托</param>
        /// <param name="error">失败事件委托</param>
        public void StopIntentPreview(Completed completed, Error error)
        {
            try
            {
                if ((!this.IsSending) && this.IsConnected())
                {
                    this.IsSending = true;
                    this.Completed_Event = completed;
                    this.Error_Event = error;
                    this.CloseTransactionTimer();
                    this.TransactionTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TransactionTimer.Elapsed += new ElapsedEventHandler((s, e) => StopIntentPreviewStart(s, e));
                    this.TransactionTimer.Start();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "关闭网络调试模式任务启动失败", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.Error_Event("关闭网络调试模式任务启动失败");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：关闭网络调试模式执行线程
        /// </summary>
        /// <param name="obj"></param>
        private void StopIntentPreviewStart(Object obj, ElapsedEventArgs e)
        {
            try
            {
                this.SecondOrder = Order.STOP_INTENT_PREVIEW;
                this.SendOrder(null, Constant.ORDER_END_DEBUG, null);
                this.StopTimeOut();
                this.Completed_Event(null,"关闭网络调试");
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "关闭网络调试模式任务失败", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.Error_Event("关闭网络调试模式任务失败");
                this.CloseTransactionTimer();
            }
        }

        //910灯控功能回复管理模块
        /// <summary>
        /// 功能：灯光工程下载回复消息管理
        /// </summary>
        /// <param name="data">回复消息数据</param>
        private void DownloadProjectReceiveManager(List<byte> data)
        {
            this.StopTimeOut();
            switch (this.MainOrder)
            {
                case Constant.ORDER_BEGIN_SEND:
                    if (Encoding.Default.GetString(data.ToArray()).Split(':')[0].Equals(Constant.RECEIVE_ORDER_BEGIN_OK))
                    {
                        string value = Encoding.Default.GetString(data.ToArray()).Split(':')[1].Split(' ')[1];
                        long.TryParse(value, out long intValue);
                        long DiskUsableSize = intValue * 1024 * 1024;
                        if (DiskUsableSize >= this.DownloadFileToTalSize)
                        {
                            this.DownloadProjectStatus = true;
                        }
                        else
                        {
                            LogTools.Debug(Constant.TAG_XIAOSA,"SD卡容量不足");
                            this.IsSending = false;
                            this.Error_Event("SD卡容量不足");
                            this.CloseTransactionTimer();
                        }
                    }
                    else if (Encoding.Default.GetString(data.ToArray()).Split(':')[0].Equals(Constant.RECEIVE_ORDER_BEGIN_ERROR))
                    {
                        string errorMessage = "";
                        if (Encoding.Default.GetString(data.ToArray()).Split(':')[1].Equals(Constant.RECEIVE_ORDER_BEGIN_ERROR_DISK))
                        {
                            errorMessage = "未插入SD卡";
                        }
                        else
                        {
                            errorMessage = "灯光工程下载失败";
                        }
                        this.Error_Event(errorMessage);
                        this.IsSending = false;
                        this.CloseTransactionTimer();
                    }
                    break;
                case Constant.ORDER_PUT:
                    if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT) || Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_SENDNEXT) || Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
                    {
                        this.DownloadProjectStatus = true;
                    }
                    else
                    {
                        this.IsSending = false;
                        this.Error_Event("灯光工程下载失败");
                        this.CloseTransactionTimer();
                    }
                    break;
                case Constant.ORDER_END_SEND:
                    if (Encoding.Default.GetString(data.ToArray()).Split(':')[0].Equals(Constant.RECEIVE_ORDER_ENDSEND_OK))
                    {
                        this.DownloadProjectStatus = true;
                        this.Completed_Event(null,"灯光工程下载成功");
                    }
                    else if (Encoding.Default.GetString(data.ToArray()).Split(':')[0].Equals(Constant.RECEIVE_ORDER_ENDSEND_ERROR))
                    {
                        this.Error_Event("灯光工程下载失败");
                    }
                    this.IsSending = false;
                    this.CloseTransactionTimer();
                    break;
            }
        }
        /// <summary>
        /// 功能：更新硬件配置回复消息管理
        /// </summary>
        /// <param name="data">回复消息数据</param>
        private void PutParamReceiveManager(List<byte> data)
        {
            this.StopTimeOut();
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_PUT_PARAM))
            {
                this.SendData();
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
            {
                this.IsSending = false;
                this.Completed_Event(null, "更新硬件配置信息成功");
                this.CloseTransactionTimer();
                LogTools.Debug(Constant.TAG_XIAOSA, "更新硬件配置信息成功");
            }
            else
            {
                //更新硬件失败
                this.IsSending = false;
                this.Error_Event("更新硬件配置信息失败");
                LogTools.Debug(Constant.TAG_XIAOSA, "更新硬件配置信息失败");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：读取硬件配置信息回复消息管理
        /// </summary>
        /// <param name="data">回复消息数据</param>
        private void GetParamReceiveManager(List<byte> data)
        {
            this.StopTimeOut();
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_GET_PARAM))
            {
                //读取硬件配置信息失败
                LogTools.Debug(Constant.TAG_XIAOSA, "读取硬件配置信息失败");
                this.Error_Event("读取硬件配置信息失败");
            }
            else
            {
                CSJ_Hardware hardware = null;
                hardware = new CSJ_Hardware(data.ToArray());
                this.Completed_Event(hardware, "读取硬件配置信息成功");
            }
            this.IsSending = false;
            this.CloseTransactionTimer();
        }
        /// <summary>
        /// 功能：升级硬件系统回复消息管理
        /// </summary>
        /// <param name="data"></param>
        private void UpdateDeviceSystemReceiveManager(List<byte> data)
        {
            this.StopTimeOut();
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_UPDATE_OK) || Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_SENDNEXT))
            {
                this.SendData();
            }
            else if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_DONE))
            {
                this.IsSending = false;
                this.Completed_Event(null, "升级硬件系统成功");
                this.CloseTransactionTimer();
            }
            else
            {
                LogTools.Debug(Constant.TAG_XIAOSA, "升级硬件系统失败");
                this.IsSending = false;
                this.Error_Event("升级硬件系统失败");
                this.CloseTransactionTimer();
            }
        }
        /// <summary>
        /// 功能：启动网络调试模拟回复消息管理
        /// </summary>
        /// <param name="data"></param>
        private void StartIntentPreviewReceiveManager(List<byte> data)
        {
            this.StopTimeOut();
            if (Encoding.Default.GetString(data.ToArray()).Equals(Constant.RECEIVE_ORDER_START_DEBUG_OK))
            {
                this.Completed_Event(null, "启动网络模拟调试成功");
            }
            else
            {
                LogTools.Debug(Constant.TAG_XIAOSA, "启动网络模拟调试失败");
                this.Error_Event("启动网络模拟调试失败");
            }
            this.IsSending = false;
            this.CloseTransactionTimer();
        }
        /// <summary>
        /// 功能：关闭网络调试模拟回复消息管理
        /// </summary>
        /// <param name="data"></param>
        private void StopIntentPreviewReceiveManager(List<byte> data)
        {
            this.StopTimeOut();
            this.Completed_Event(null,"关闭网络调试模拟成功");
            this.IsSending = false;
            this.CloseTransactionTimer();
        }
        /// <summary>
        /// 功能：关闭灯光控制事务定时器
        /// </summary>
        protected void CloseTransactionTimer()
        {
            if (this.TransactionTimer != null)
            {
                this.TransactionTimer.Stop();
                this.TransactionTimer = null;
            }
        }


        // 服务器功能模块
        //private void SetSessionId(String sessionId,Completed completed,Error error)
        //{
        //    try
        //    {
        //        if ((!this.IsSending) && this.IsConnected())
        //        {
        //            this.IsSending = true;
        //            this.Completed_Event = completed;
        //            this.Error_Event = error;
        //            this.CloseTransactionTimer();
        //            this.TransactionTimer = new System.Timers.Timer { AutoReset = false };
        //            this.TransactionTimer.Elapsed += new ElapsedEventHandler((s,e) => SetSessionIdStart(s, e,sessionId));
        //            this.TransactionTimer.Start();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogTools.Error(Constant.TAG_XIAOSA, "灯光工程下载更新任务启动失败", ex);
        //        this.StopTimeOut();
        //        this.IsSending = false;
        //        this.Error_Event("灯光工程下载更新任务启动失败");
        //        this.CloseTransactionTimer();
        //    }
        //}

        //private void SetSessionIdStart(Object obj, ElapsedEventArgs e,string sessionId)
        //{
        //    this.SecondOrder = Order.SERVER_SET_SESSION_ID;
        //    List<byte> data = new List<byte>();
        //    data.Add(0xBB);
        //    data.Add(0xAA);
        //    data.Add(0x04);
        //    data.Add(0x00);
        //    data.AddRange(Encoding.Default.GetBytes(sessionId));
        //    this.Send(data.ToArray());
        //}


        //服务器功能回复管理模块
        //private void ServerReceiveManager(byte[] data)
        //{
        //    switch (data[2])
        //    {
        //        case 0x00:
        //        case 0x01:
        //        case 0x02:
        //        case 0x03:
        //        case 0x04:
        //            this.ServerSetSessionIdReceiveManager(data);
        //            break;
        //    }
        //}
        //private void ServerSetSessionIdReceiveManager(byte[] data)
        //{
        //    switch (data[3])
        //    {
        //        case 0x00:
        //            //失败
        //        case 0x01:
        //            //成功
        //            break;
        //    }
        //}
    }

    //事件传递数据结构
    /// <summary>
    /// 灯光工程下载事件执行传递数据结构
    /// </summary>
    public class DownloadProjectData
    {
        public DBWrapper Wrapper { get; set; }
        public string ConfigPath { get; set; }
        public DownloadProjectData(DBWrapper wrapper,string configPath)
        {
            this.Wrapper = wrapper;
            this.ConfigPath = configPath;
        }
    }
    public class PutParamData
    {
        public string FilePath { get; set; } 
        public PutParamData(string filePath)
        {
            this.FilePath = filePath;
        }
    }
    public class UpdateDeviceSystemData
    {
        public string FilePath { get; set; }
        public UpdateDeviceSystemData(string filePath)
        {
            this.FilePath = filePath;
        }
    }
    public class StartIntentPreviewData
    {
        public int TimeFactory { get; set; }
        public StartIntentPreviewData(int timeFactory)
        {
            this.TimeFactory = timeFactory;
        }
    }

    public enum Order
    {
        NULL,
        ZG,RG,DG,YG,ZC,RC,DC,LK,DK,CP,XP,
        DOWNLOAD_PROJECT,PUT_PARAM,GET_PARAM,UPDATE_DEVICE_SYSTEM,
        START_INTENT_PREVIEW,STOP_INTENT_PREVIEW,
        SERVER_SET_SESSION_ID, SERVER_BIND_DEVICE, SERVER_CHANGE_BIND_DEVICE, SERVER_UNBIND_DEVICE, SERVER_GET_DEVICES
    }
}
