using LightController.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
    public class Conn
    {
        //接收缓冲区大小
        private const int BUFFER_SIZE = 2048;
        //socket
        public Socket Socket { get; set; }
        //是否被使用
        public bool IsUse { get; set; }
        //Buff
        public byte[] ReadBuff { get; set; }
        //连接ip地址
        public string Ip { get; set; }
        //连接端口号
        public int Port { get; set; }
        //接收数据偏移量
        public int BuffCount { get; set; }
        //数据包总包数
        private int Pakege_Count { get; set; }
        //当前数据包编号数据包
        private int Pakege_No { get; set; }
        //待发送数据
        private byte[] Data { get; set; }
        //发送数据等待超时处理线程
        private Thread TimeOutThread { get; set; }
        //发送命令
        private string Order { get; set; }
        //接收命令
        private RECEIVE MReceive { get; set; }
        //备注信息
        private string[] Strs { get; set; }
        //发送缓冲区大小
        private int PakegeSize { get; set; }
        //发送数据完成状态
        /// <summary>
        /// 接收数据状态
        /// </summary>
        private bool IsReceive { get; set; }
        private bool IsTiomeOutThreadStart { get; set; }
        private int TimeOutCount { get; set; }
        private bool DownloadState { get; set; }
        private Thread DownloadThread { get; set; }
        private DBWrapper DBWrapper { get; set; }
        private string ConfigPath { get; set; }
        private IReceiveCallBack callBack { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Conn()
        {
            ReadBuff = new byte[BUFFER_SIZE];
            IsUse = false;
            PakegeSize = 2040;
            Ip = "";
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="socket"></param>
        public void Init(Socket socket)
        {
            this.Socket = socket;
            DownloadState = true;
            IsUse = true;
            IsReceive = false;
            IsTiomeOutThreadStart = false;
            BuffCount = 0;
            TimeOutCount = 0;
            TimeOutThread = new Thread(new ThreadStart(TimeOut));
            TimeOutThread.IsBackground = true;
            string addr = Socket.RemoteEndPoint.ToString();
            Ip = addr.Split(':')[0];
            int.TryParse(addr.Split(':')[1], out int connPort);
            Port = connPort;
            TimeOutThread.Start();
        }
        /// <summary>
        /// 设定发送数据缓冲区大小
        /// </summary>
        /// <param name="size"></param>
        public void SetPakegeSize(int size)
        {
            this.PakegeSize = size;
        }
        /// <summary>
        /// 接收缓冲区大小
        /// </summary>
        /// <returns></returns>
        public int BuffRemain()
        {
            return BUFFER_SIZE - BuffCount;
        }
        /// <summary>
        /// 获取客户端地址
        /// </summary>
        /// <returns></returns>
        public string GetAddress()
        {
            if (!IsUse) return "获取地址失败";
            return Socket.RemoteEndPoint.ToString();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (!IsUse) return;
            Console.WriteLine(GetAddress() + "断开连接");
            DownloadState = false;
            DownloadThread.Abort();
            callBack.SendError(Ip, Order);
            Ip = "";
            Socket.Close();
            IsUse = false;
        }
        /// <summary>
        /// 发送命令事务管理
        /// </summary>
        /// <param name="data">数据主体</param>
        /// <param name="order">命令</param>
        /// <param name="strarray">备注信息</param>
        private void SendData(byte[] data, string order, string[] strarray)
        {
            Data = data;
            Order = order;
            Strs = strarray;
            switch (order)
            {
                case Constant.ORDER_PUT:
                case Constant.ORDER_BEGIN_SEND:
                case Constant.ORDER_END_SEND:
                default:
                    SendOrderPakege();
                    break;
            }

        }
        /// <summary>
        /// 返回消息事务管理
        /// </summary>
        /// <param name="receive">返回消息类型</param>
        private void Receive(RECEIVE receive)
        {
            MReceive = receive;
            switch (receive)
            {
                case RECEIVE.Send:
                    Console.WriteLine("发送第" + (Pakege_No + 1) + "包数据，包总数为" + Pakege_Count);
                    SendDataPakege();
                    break;
                default:
                    break;
            }
            
        }
        /// <summary>
        /// 发送命令包
        /// </summary>
        private void SendOrderPakege()
        {
            if (Data != null)
            {
                Pakege_Count = Data.Length / PakegeSize;
                Pakege_Count = (Data.Length % PakegeSize == 0) ? Pakege_Count : Pakege_Count + 1;
                Pakege_No = 0;
            }
            List<byte> pakege = new List<byte>();
            List<byte> pakegeData = new List<byte>();
            byte[] pakegeOrder = Encoding.Default.GetBytes(Order);
            byte[] space = Encoding.Default.GetBytes(" ");
            pakegeData.AddRange(pakegeOrder);
            if (Strs != null)
            {
                pakegeData.AddRange(space);
                for (int i = 0; i < Strs.Length; i++)
                {
                    pakegeData.AddRange(Encoding.Default.GetBytes(Strs[i]));
                    if (i != Strs.Length - 1)
                    {
                        pakegeData.AddRange(space);
                    }
                }
            }
            byte[] pakegeDataLength = new byte[] { Convert.ToByte(pakegeData.Count & 0xFF), Convert.ToByte((pakegeData.Count >> 8) & 0xFF) };
            byte pakegeMark = GetOrderMark();
            byte[] pakegeHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), pakegeDataLength[0], pakegeDataLength[1], pakegeMark, Convert.ToByte(0x00), Convert.ToByte(0x00) };
            pakege.AddRange(pakegeHead);
            pakege.AddRange(pakegeData);
            byte[] pakegeCRC = CRCTools.GetInstance().GetCRC(pakege.ToArray());
            pakege[6] = pakegeCRC[0];
            pakege[7] = pakegeCRC[1];

            Console.Write("Send:    " + Order + " ---");
            foreach (byte value in pakege)
            {
                Console.Write(Convert.ToString(value,16) + " ");
            }

            Socket.BeginSend(pakege.ToArray(), 0, pakege.ToArray().Length, SocketFlags.None, SendCb,this);
        }
        /// <summary>
        /// 获取协议头标记位
        /// </summary>
        /// <returns></returns>
        private byte GetOrderMark()
        {
            byte result;
            switch (Order)
            {
                case Constant.ORDER_PUT:
                    result = Convert.ToByte(Constant.MARK_ORDER_TAKE_DATA, 2);
                    break;

                default:
                    result = Convert.ToByte(Constant.MARK_ORDER_NO_TAKE_DATA, 2);
                    break;
            }
            return result;
        }
        /// <summary>
        /// 获取数据包标记位
        /// </summary>
        /// <returns></returns>
        private byte GetDataMark()
        {
            byte result;
            if (Pakege_No == Pakege_Count)
            {
                result = Convert.ToByte(Constant.MARK_DATA_END, 2);
            }
            else
            {
                result = Convert.ToByte(Constant.MARK_DATA_NO_END, 2);
            }
            return result;
        }
        /// <summary>
        /// 发送数据包
        /// </summary>
        /// <param name="receive"></param>
        private void SendDataPakege()
        {
            Pakege_No++;
            byte[] pakegeData;
            List<byte> pakege = new List<byte>();
            if (Pakege_No != Pakege_Count)
            {
                pakegeData = new byte[PakegeSize];
                for (int i = 0; i < PakegeSize; i++)
                {
                    pakegeData[i] = Data[(Pakege_No - 1) * PakegeSize + i];
                }
            }
            else
            {
                pakegeData = new byte[Data.Length - (Pakege_No - 1) * PakegeSize];
                for (int i = 0; i < pakegeData.Length - (Pakege_No - 1) * PakegeSize; i++)
                {
                    pakegeData[i] = Data[(Pakege_No - 1) * PakegeSize + i];
                }
            }
            int len = pakegeData.Length;
            byte[] pakegeDataSize = new byte[] { Convert.ToByte(pakegeData.Length & 0xFF), Convert.ToByte((pakegeData.Length >> 8) & 0xFF) };
            byte pakegeMark = GetDataMark();
            byte[] pakegeHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), pakegeDataSize[0], pakegeDataSize[1], pakegeMark, Convert.ToByte(0x00), Convert.ToByte(0x00) };
            pakege.AddRange(pakegeHead);
            pakege.AddRange(pakegeData);
            byte[] pakegeCRC = CRCTools.GetInstance().GetCRC(pakege.ToArray());
            pakege[6] = pakegeCRC[0];
            pakege[7] = pakegeCRC[1];

            Console.Write("SendFile:    " + Order + ",pakegeNo: " + Pakege_No + ",pakegeCount: " + Pakege_Count + " ---");
            foreach (byte value in pakege)
            {
                Console.Write("0x" + Convert.ToString(value, 16) + ",");
            }

            Socket.BeginSend(pakege.ToArray(), 0, pakege.ToArray().Length, SocketFlags.None, SendCb, this);
        }
        /// <summary>
        /// 超时处理
        /// </summary>
        private void TimeOut()
        {
            while (true)
            {
                if (IsTiomeOutThreadStart)
                {
                    for (int i = 0; i < 5000; i++)
                    {
                        Thread.Sleep(1);
                        if (IsReceive)
                        {
                            break;
                        }
                    }
                    IsTiomeOutThreadStart = false;
                    if (!IsReceive)
                    {
                        if (TimeOutCount > 5)
                        {
                            try
                            {
                                DownloadState = false;
                                DownloadThread.Abort();
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("超时" + TimeOutCount + "次,结束发送");
                            }
                            finally
                            {
                                TimeOutCount = 0;
                                callBack.SendError(Ip,Order);
                            }
                        }
                        else
                        {
                            TimeOutCount++;
                            try
                            {
                                DownloadState = false;
                                DownloadThread.Abort();
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("超时" + TimeOutCount + "次");
                            }
                            finally
                            {
                                DownloadFile(DBWrapper, ConfigPath,callBack);
                            }    
                        }
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }
        /// <summary>
        /// 绑定数据接收监听
        /// </summary>
        public void BeginReceive()
        {
            Socket.BeginReceive(ReadBuff, BuffCount, BuffRemain(), SocketFlags.None, ReceiveCb,this);
        }
        /// <summary>
        /// Recevie回调函数
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ReceiveCb(IAsyncResult asyncResult)
        {
            IsTiomeOutThreadStart = false;
            IsReceive = true;
            //获取接受对象
            Conn conn = (Conn)asyncResult.AsyncState;
            try
            {
                int count = conn.Socket.EndReceive(asyncResult);
                //关闭信号
                if (count <= 0)
                {
                    Console.WriteLine("收到 [" + conn.GetAddress() + "] 断开连接");
                    conn.Close();
                    return;
                }
                //数据处理
                string receiveStr = Encoding.UTF8.GetString(conn.ReadBuff, 0, count);
                Console.WriteLine("Receive Data : " + receiveStr);
                switch (Order)
                {
                    case Constant.ORDER_PUT:
                        switch (receiveStr)
                        {
                            case Constant.RECEIVE_ORDER_SENDNEXT:
                                Receive(RECEIVE.Send);
                                break;
                            case Constant.RECEIVE_ORDER_PUT:
                                Receive(RECEIVE.Send);
                                break;
                            case Constant.RECEIVE_ORDER_DONE:
                                Console.WriteLine("下载完成");
                                DownloadState = true;
                                break;
                            default:
                                DownloadThread.Abort();
                                DownloadState = false;
                                callBack.SendError(Ip,Order);
                                break;
                        }
                        break;
                    case Constant.ORDER_BEGIN_SEND:
                        switch (receiveStr.Split(':')[0])
                        {
                            case Constant.RECEIVE_ORDER_BEGIN_OK:
                                DownloadState = true;
                                break;
                            case Constant.RECEIVE_ORDER_END_ERROR:
                                DownloadThread.Abort();
                                DownloadState = false;
                                callBack.SendError(Ip,Order);
                                break;
                            default:
                                break;
                        }
                        break;
                    case Constant.ORDER_END_SEND:
                        switch (receiveStr.Split(':')[0])
                        {
                            case Constant.RECEIVE_ORDER_END_OK:
                                //执行发送完成命令
                                callBack.SendCompleted(Ip,Order);
                                break;
                            case Constant.RECEIVE_ORDER_END_ERROR:
                                DownloadThread.Abort();
                                DownloadState = false;
                                callBack.SendError(Ip,Order);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        switch (receiveStr.Split(':')[0])
                        {
                            case Constant.RECEIVE_ORDER_OTHER_OK:
                                callBack.SendCompleted(Ip,Order);
                                break;
                            case Constant.RECEIVE_ORDER_OTHER_ERROR:
                                callBack.SendError(Ip,Order);
                                break;
                            default:
                                break;
                        }
                        break;
                }
                //继续接收消息
                conn.Socket.BeginReceive(conn.ReadBuff, conn.BuffCount, conn.BuffRemain(), SocketFlags.None, ReceiveCb, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[" + conn.GetAddress() + "] 断开连接" + "Exception :" + ex.Message);
                conn.Close();
            }
        }
        /// <summary>
        /// Send回调函数
        /// </summary>
        /// <param name="asyncResult"></param>
        private void SendCb(IAsyncResult asyncResult)
        {
            IsReceive = false;
            IsTiomeOutThreadStart = true;
            Console.WriteLine("发送完成");
            
        }
        /// <summary>
        /// 下载所有文件数据
        /// </summary>
        public void DownloadFile(DBWrapper dBWrapper,string configPath,IReceiveCallBack receiveCallBack)
        {
            DBWrapper = dBWrapper;
            ConfigPath = configPath;
            callBack = receiveCallBack;
            DownloadThread = new Thread(new ThreadStart(DownloadStart));
            DownloadThread.Start();
        }

        private void DownloadStart()
        {
            IList<DMX_C_File> c_Files = DataTools.GetInstance().GetC_Files(DBWrapper,ConfigPath);
            IList<DMX_M_File> m_Files = DataTools.GetInstance().GetM_Files(DBWrapper,ConfigPath);
            DMXConfigData configData = DataTools.GetInstance().GetConfigData(DBWrapper, ConfigPath);
            DownloadState = false;
            SendData(null, Constant.ORDER_BEGIN_SEND, null);
            string fileName = "";
            string fileSize = "";
            string fileCRC = "";
            byte[] crc;
            foreach (DMX_C_File item in c_Files)
            {
                if (item.SenceNo < 10)
                {
                    fileName = "C0" + (item.SenceNo +1) + ".bin";
                }
                else
                {
                    fileName = "C" + (item.SenceNo + 1) + ".bin";
                }
                fileSize = item.GetByteData().Length.ToString();
                crc = CRCTools.GetInstance().GetCRC(item.GetByteData());
                fileCRC = crc[0].ToString() + crc[1].ToString();
                while (true)
                {
                    if (DownloadState)
                    {
                        SendData(item.GetByteData(),Constant.ORDER_PUT,new string[] {fileName,fileSize,fileCRC});
                        DownloadState = false;
                        break;
                    }
                }
            }
            foreach (DMX_M_File item in m_Files)
            {
                if (item.SenceNo < 10)
                {
                    fileName = "M0" + (item.SenceNo + 1) + ".bin";
                }
                else
                {
                    fileName = "M" + (item.SenceNo + 1) + ".bin";
                }
                fileSize = item.GetByteData().Length.ToString();
                crc = CRCTools.GetInstance().GetCRC(item.GetByteData());
                fileCRC = crc[0].ToString() + crc[1].ToString();
                while (true)
                {
                    if (DownloadState)
                    {
                        SendData(item.GetByteData(), Constant.ORDER_PUT, new string[] { fileName, fileSize, fileCRC });
                        DownloadState = false;
                        break;
                    }
                }
            }
            fileName = "Config.bin";
            byte[] configFileData = configData.GetConfigData();
            configData.WriteToFile(@"C:\Temp");
            fileSize = configFileData.Length.ToString();
            crc = CRCTools.GetInstance().GetCRC(configData.GetConfigData());
            fileCRC = crc[0].ToString() + crc[1].ToString();
            while (true)
            {
                if (DownloadState)
                {
                    SendData(configFileData, Constant.ORDER_PUT, new string[] { fileName, fileSize, fileCRC });
                    DownloadState = false;
                    break;
                }
            }
            while (true)
            {
                if (DownloadState)
                {
                    SendData(null, Constant.ORDER_END_SEND, null);
                    DownloadState = false;
                    break;
                }
            }
        }
        /// <summary>
        /// 发送控制命令
        /// </summary>
        /// <param name="order"></param>
        /// <param name="array"></param>
        public void SendOrder(string order,string[] array,IReceiveCallBack receiveCallBack)
        {
            callBack = receiveCallBack;
            SendData(null, order, array);
        }
    }
}
