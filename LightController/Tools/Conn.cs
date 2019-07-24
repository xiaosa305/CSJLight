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
        
        private const int BUFFER_SIZE = 2048;//接收缓冲区大小
        public Socket Socket { get; set; }//socket
        public bool IsUse { get; set; } //是否被使用
        public byte[] ReadBuff { get; set; } //Buff
        public string Ip { get; set; }//连接ip地址
        public int Port { get; set; } //连接端口号
        public int BuffCount { get; set; } //接收数据偏移量
        private int Package_Count { get; set; }//数据包总包数
        private int Package_No { get; set; }//当前数据包编号数据包
        private byte[] Data { get; set; }//待发送数据
        private Thread TimeOutThread { get; set; }//发送数据等待超时处理线程
        private string Order { get; set; }//发送命令
        private string[] Strs { get; set; }//备注信息
        private int PackageSize { get; set; }//发送缓冲区大小
        private bool IsReceive { get; set; } // 接收数据状态
        private bool IsTiomeOutThreadStart { get; set; }//超时线程是否启动
        private int TimeOutCount { get; set; }//超时重发计数
        private bool DownloadStatus { get; set; }//下载数据通信状态标记
        private Thread DownloadThread { get; set; }//下载数据线程
        private Thread PutParamThread { get; set; }//发送配置文件线程
        private DBWrapper DBWrapper { get; set; }//数据库数据
        private string ConfigPath { get; set; }//全局配置文件路径
        private string HardwarePath { get; set; }//硬件配置文件路径
        private IReceiveCallBack CallBack { get; set; }//命令完成或错误回调方法
        public int Addr { get; set; }//硬件地址
        public string DeviceName { get; set; }//硬件标识

        /// <summary>
        /// 构造函数
        /// </summary>
        public Conn()
        {
            ReadBuff = new byte[BUFFER_SIZE];
            IsUse = false;
            PackageSize = 2040;
            Ip = "";
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="socket"></param>
        public void Init(Socket socket)
        {
            this.Socket = socket;
            DownloadStatus = true;
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
        public void SetPackageSize(int size)
        {
            this.PackageSize = size;
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
            DownloadStatus = false;
            try
            {
                switch (Order)
                {
                    case Constant.ORDER_PUT_PARA:
                        PutParamThread.Abort();
                        break;
                    case Constant.ORDER_PUT:
                    case Constant.ORDER_BEGIN_SEND:
                    case Constant.ORDER_END_SEND:
                        DownloadThread.Abort();
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                try
                {
                    TimeOutThread.Abort();
                }
                finally
                {
                    CallBack.SendError(Ip, Order);
                    Ip = "";
                    Socket.Close();
                    IsUse = false;
                }
            }
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
                case Constant.ORDER_PUT_PARA:
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
            if (Package_No == Package_Count)
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
            SendOrderPackage();
        }

        /// <summary>
        /// 发送命令包
        /// </summary>
        private void SendOrderPackage()
        {
            if (Data != null)
            {
                Package_Count = Data.Length / PackageSize;
                Package_Count = (Data.Length % PackageSize == 0) ? Package_Count : Package_Count + 1;
                Package_No = 0;
            }
            List<byte> package = new List<byte>();
            List<byte> packageData = new List<byte>();
            byte[] packageOrder = Encoding.Default.GetBytes(Order);
            byte[] space = Encoding.Default.GetBytes(" ");
            packageData.AddRange(packageOrder);
            if (Strs != null)
            {
                packageData.AddRange(space);
                for (int i = 0; i < Strs.Length; i++)
                {
                    packageData.AddRange(Encoding.Default.GetBytes(Strs[i]));
                    if (i != Strs.Length - 1)
                    {
                        packageData.AddRange(space);
                    }
                }
            }
            byte[] packageDataLength = new byte[] { Convert.ToByte(packageData.Count & 0xFF), Convert.ToByte((packageData.Count >> 8) & 0xFF) };
            byte packageMark = GetOrderMark();
            byte[] packageHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(Addr), packageDataLength[0], packageDataLength[1], packageMark, Convert.ToByte(0x00), Convert.ToByte(0x00) };
            package.AddRange(packageHead);
            package.AddRange(packageData);
            byte[] packageCRC = CRCTools.GetInstance().GetCRC(package.ToArray());
            package[6] = packageCRC[0];
            package[7] = packageCRC[1];

            Console.Write("Send:    " + Order + " ---");
            foreach (byte value in package)
            {
                Console.Write(Convert.ToString(value,16) + " ");
            }

            Socket.BeginSend(package.ToArray(), 0, package.ToArray().Length, SocketFlags.None, SendCb,this);
        }

        /// <summary>
        /// 发送数据包
        /// </summary>
        /// <param name="receive"></param>
        private void SendDataPackage()
        {
            Package_No++;
            byte[] packageData;
            List<byte> package = new List<byte>();
            if (Package_No != Package_Count)
            {
                packageData = new byte[PackageSize];
                for (int i = 0; i < PackageSize; i++)
                {
                    packageData[i] = Data[(Package_No - 1) * PackageSize + i];
                }
            }
            else
            {
                packageData = new byte[Data.Length - (Package_No - 1) * PackageSize];
                for (int i = 0; i < packageData.Length - (Package_No - 1) * PackageSize; i++)
                {
                    packageData[i] = Data[(Package_No - 1) * PackageSize + i];
                }
            }
            int len = packageData.Length;
            byte[] packageDataSize = new byte[] { Convert.ToByte(packageData.Length & 0xFF), Convert.ToByte((packageData.Length >> 8) & 0xFF) };
            byte packageMark = GetDataMark();
            byte[] packageHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(Addr), packageDataSize[0], packageDataSize[1], packageMark, Convert.ToByte(0x00), Convert.ToByte(0x00) };
            package.AddRange(packageHead);
            package.AddRange(packageData);
            byte[] packageCRC = CRCTools.GetInstance().GetCRC(package.ToArray());
            package[6] = packageCRC[0];
            package[7] = packageCRC[1];

            Console.Write("SendFile:    " + Order + ",packageNo: " + Package_No + ",packageCount: " + Package_Count + " ---");
            foreach (byte value in package)
            {
                Console.Write("0x" + Convert.ToString(value, 16) + ",");
            }

            Socket.BeginSend(package.ToArray(), 0, package.ToArray().Length, SocketFlags.None, SendCb, this);
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
                        switch (Order)
                        {
                            case Constant.ORDER_BEGIN_SEND:
                            case Constant.ORDER_END_SEND:
                            case Constant.ORDER_PUT:
                                try
                                {
                                    DownloadStatus = false;
                                    DownloadThread.Abort();
                                }
                                finally
                                {
                                    if (TimeOutCount > 5)
                                    {
                                        TimeOutCount = 0;
                                        CallBack.SendError(Ip, Order);
                                    }
                                    else
                                    {
                                        TimeOutCount++;
                                        DownloadFile(DBWrapper, ConfigPath, CallBack);
                                    }
                                }
                                break;
                            case Constant.ORDER_PUT_PARA:
                                try
                                {
                                    PutParamThread.Abort();
                                }
                                finally
                                {
                                    if (TimeOutCount > 5)
                                    {
                                        TimeOutCount = 0;
                                        CallBack.SendError(Ip, Order);
                                    }
                                    else
                                    {
                                        TimeOutCount++;
                                        PutPara(HardwarePath, CallBack);
                                    }
                                }
                                break;
                            default:
                                break;
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
            Conn conn = (Conn)asyncResult.AsyncState;
            try
            {
                int count = conn.Socket.EndReceive(asyncResult);
                if (count <= 0)
                {
                    Console.WriteLine("收到 [" + conn.GetAddress() + "] 断开连接");
                    conn.Close();
                    return;
                }
                string receiveStr = Encoding.UTF8.GetString(conn.ReadBuff, 0, count);
                Console.WriteLine("Receive Data : " + receiveStr);
                switch (Order)
                {
                    case Constant.ORDER_PUT:
                        switch (receiveStr)
                        {
                            case Constant.RECEIVE_ORDER_SENDNEXT:
                                SendDataPackage();
                                break;
                            case Constant.RECEIVE_ORDER_PUT:
                                SendDataPackage();
                                break;
                            case Constant.RECEIVE_ORDER_DONE:
                                Console.WriteLine("下载完成");
                                DownloadStatus = true;
                                break;
                            default:
                                try
                                {
                                    DownloadThread.Abort();
                                }
                                finally
                                {
                                    DownloadStatus = false;
                                    CallBack.SendError(Ip, Order);
                                }
                                break;
                        }
                        break;
                    case Constant.ORDER_BEGIN_SEND:
                        switch (receiveStr.Split(':')[0])
                        {
                            case Constant.RECEIVE_ORDER_BEGIN_OK:
                                DownloadStatus = true;
                                break;
                            case Constant.RECEIVE_ORDER_END_ERROR:
                                try
                                {
                                    DownloadThread.Abort();
                                }
                                finally
                                {
                                    DownloadStatus = false;
                                    CallBack.SendError(Ip, Order);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case Constant.ORDER_END_SEND:
                        switch (receiveStr.Split(':')[0])
                        {
                            case Constant.RECEIVE_ORDER_END_OK:
                                DownloadStatus = true;
                                CallBack.SendCompleted(Ip,Order);
                                break;
                            case Constant.RECEIVE_ORDER_END_ERROR:
                                try
                                {
                                    DownloadThread.Abort();
                                }
                                finally
                                {
                                    DownloadStatus = false;
                                    CallBack.SendError(Ip, Order);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case Constant.ORDER_PUT_PARA:
                        switch (receiveStr)
                        {
                            case Constant.RECEIVE_ORDER_PUT_PARA:
                                SendDataPackage();
                                break;
                            case Constant.RECEIVE_ORDER_DONE:
                                Console.WriteLine("下载完成");
                                CallBack.SendCompleted(Ip,Order);
                                break;
                            default:
                                try
                                {
                                    PutParamThread.Abort();
                                }
                                finally
                                {
                                    CallBack.SendError(Ip, Order);
                                }
                                break;
                        }
                        break;
                    default:
                        break;
                }
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
            CallBack = receiveCallBack;
            DownloadThread = new Thread(new ThreadStart(DownloadStart));
            DownloadThread.Start();
        }

        /// <summary>
        /// 下载数据线程
        /// </summary>
        private void DownloadStart()
        {
            IList<DMX_C_File> c_Files = DataTools.GetInstance().GetC_Files(DBWrapper,ConfigPath);
            IList<DMX_M_File> m_Files = DataTools.GetInstance().GetM_Files(DBWrapper,ConfigPath);
            DMXConfigData configData = DataTools.GetInstance().GetConfigData(DBWrapper, ConfigPath);
            DownloadStatus = false;
            SendData(null, Constant.ORDER_BEGIN_SEND, null);
            string fileName = "";
            string fileSize = "";
            string fileCRC = "";
            byte[] crc;
            foreach (DMX_C_File item in c_Files)
            {
                fileName = "C" + (item.SceneNo + 1) + ".bin";
                fileSize = item.GetByteData().Length.ToString();
                crc = CRCTools.GetInstance().GetCRC(item.GetByteData());
                fileCRC = crc[0].ToString() + crc[1].ToString();
                while (true)
                {
                    if (DownloadStatus)
                    {
                        SendData(item.GetByteData(),Constant.ORDER_PUT,new string[] {fileName,fileSize,fileCRC});
                        DownloadStatus = false;
                        break;
                    }
                }
            }
            foreach (DMX_M_File item in m_Files)
            {
                fileName = "M" + (item.SceneNo + 1) + ".bin";
                fileSize = item.GetByteData().Length.ToString();
                crc = CRCTools.GetInstance().GetCRC(item.GetByteData());
                fileCRC = crc[0].ToString() + crc[1].ToString();
                while (true)
                {
                    if (DownloadStatus)
                    {
                        SendData(item.GetByteData(), Constant.ORDER_PUT, new string[] { fileName, fileSize, fileCRC });
                        DownloadStatus = false;
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
                if (DownloadStatus)
                {
                    SendData(configFileData, Constant.ORDER_PUT, new string[] { fileName, fileSize, fileCRC });
                    DownloadStatus = false;
                    break;
                }
            }
            while (true)
            {
                if (DownloadStatus)
                {
                    SendData(null, Constant.ORDER_END_SEND, null);
                    DownloadStatus = false;
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
            CallBack = receiveCallBack;
            SendData(null, order, array);
        }

        /// <summary>
        /// 发送配置文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="receiveCallBack"></param>
        public void PutPara(string filePath,IReceiveCallBack receiveCallBack)
        {
            CallBack = receiveCallBack;
            HardwarePath = filePath;
            PutParamThread = new Thread(new ThreadStart(PutParamThreadStart))
            {
                IsBackground = true
            };
            PutParamThread.Start();
        }

        /// <summary>
        /// 发送配置文件线程
        /// </summary>
        private void PutParamThreadStart()
        {
            DMXHardware hardware = new DMXHardware(HardwarePath);
            byte[] data = hardware.GetHardware();
            string fileName = "Hardware.bin";
            string fileSize = data.Length.ToString();
            byte[] crc = CRCTools.GetInstance().GetCRC(data);
            string fileCRC = crc[0].ToString() + crc[1].ToString();
            SendData(data, Constant.ORDER_PUT_PARA, new string[] { fileName, fileSize, fileCRC });
        }
    }
}
