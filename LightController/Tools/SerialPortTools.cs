using LightController.Ast;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
    class SerialPortTools
    {
        private static SerialPortTools Instance { get; set; }
        private string PortName { get; set; }
        private int BaudRate { get; set; }
        private StopBits StopBits { get; set; }
        private Parity Parity { get; set; }
        private int DataBits { get; set; }
        private SerialPort ComDevice { get; set; }
        private DBWrapper DBWrapper { get; set; }
        private string ConfigPath { get; set; }
        private IReceiveCallBack CallBack { get; set; }
        public string HardwarePath { get; private set; }
        private Thread DownloadThread { get; set; }
        private bool DownloadStatus { get; set; }
        public bool IsSending { get; private set; }
        private byte[] Data { get; set; }
        private string Order { get; set; }
        private string[] Strs { get; set; }
        private int Package_No { get; set; }
        private int Package_Count { get; set; }
        private int PackageSize { get; set; }
        private DownloadProgressDelegate DownloadProgressDelegate { get; set; }
        private GetParamDelegate GetParamDelegate { get; set; }
        private string DeviceName { get; set; }
        private int Addr { get; set; }
        public int DownloadFileToTalSize { get; private set; }
        public bool IsTiomeOutThreadStart { get; private set; }
        public bool IsReceive { get; private set; }
        public int TimeOutCount { get; private set; }
        public Thread TimeOutThread { get; }
        public double CurrentDownloadCompletedSize { get; private set; }
        public string CurrentFileName { get; private set; }

        private SerialPortTools()
        {
            ComDevice = new SerialPort();
            DownloadStatus = true;
            PackageSize = Constant.PACKAGE_SIZE_1K;
            Addr = Constant.UDPADDR;
            IsSending = false;
            IsReceive = false;
            TimeOutCount = 0;
            TimeOutThread = new Thread(new ThreadStart(TimeOut))
            {
                IsBackground = true
            };
            ComDevice.DataReceived += new SerialDataReceivedEventHandler(ReceiveData);
            ComDevice.WriteBufferSize = PackageSize + 8;
            TimeOutThread.Start();
        }

        public static SerialPortTools GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SerialPortTools();
            }
            return Instance;
        }

        public string[] GetSerialPortNameList()
        {
            return SerialPort.GetPortNames();
        }

        public void SetPackageSize(int size)
        {
            this.PackageSize = size;
        }

        private void SetSerialPort()
        {
            ComDevice.BaudRate = BaudRate;
            ComDevice.StopBits = StopBits;
            ComDevice.Parity = Parity;
            ComDevice.DataBits = DataBits;
            ComDevice.PortName = PortName;
        }

        public bool OpenCom(string portName,int baud)
        {
            PortName = portName;
            BaudRate = baud;
            StopBits = StopBits.Two;
            Parity = Parity.None;
            DataBits = 8;
            if (ComDevice.IsOpen)
            {
                ComDevice.Close();
            }
            SetSerialPort();
            ComDevice.Open();
            return ComDevice.IsOpen;
        }

        public bool CloseCom()
        {
            ComDevice.Close();
            return !ComDevice.IsOpen;
        }

        private void Send(byte[] data, string order, string[] strarray)
        {
            Data = data;
            Order = order;
            Strs = strarray;
            SendOrderPackage();
        }

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
            byte[] packageHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), packageDataLength[0], packageDataLength[1], packageMark, Convert.ToByte(0x00), Convert.ToByte(0x00) };
            package.AddRange(packageHead);
            package.AddRange(packageData);
            byte[] packageCRC = CRCTools.GetInstance().GetCRC(package.ToArray());
            package[6] = packageCRC[0];
            package[7] = packageCRC[1];
            IsReceive = false;
            IsTiomeOutThreadStart = true;
            ComDevice.Write(package.ToArray(), 0, package.ToArray().Length);
        }

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
            byte[] packageHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), packageDataSize[0], packageDataSize[1], packageMark, Convert.ToByte(0x00), Convert.ToByte(0x00) };
            package.AddRange(packageHead);
            package.AddRange(packageData);
            byte[] packageCRC = CRCTools.GetInstance().GetCRC(package.ToArray());
            package[6] = packageCRC[0];
            package[7] = packageCRC[1];
            IsReceive = false;
            IsTiomeOutThreadStart = true;
            if (Order.Equals(Constant.ORDER_PUT))
            {
                CurrentDownloadCompletedSize += packageData.Count();
                int progress = Convert.ToInt16(CurrentDownloadCompletedSize / (DownloadFileToTalSize * 1.0) * 100);
                DownloadProgressDelegate(CurrentFileName, progress);
            }
            ComDevice.Write(package.ToArray(), 0, package.ToArray().Length);
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
                    for (int i = 0; i < 2000; i++)
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
                                        IsSending = false;
                                        string deviceName = DeviceName;
                                        CallBack.SendError(deviceName, Order);
                                    }
                                    else
                                    {
                                        TimeOutCount++;
                                        Download(DBWrapper, ConfigPath, CallBack, DownloadProgressDelegate);
                                    }
                                }
                                break;
                            case Constant.ORDER_PUT_PARAM:
                                if (TimeOutCount > 5)
                                {
                                    TimeOutCount = 0;
                                    IsSending = false;
                                    string deviceName = DeviceName;
                                    CallBack.SendError(deviceName, Order);
                                }
                                else
                                {
                                    TimeOutCount++;
                                    PutParam(HardwarePath, CallBack);
                                }
                                break;
                            case Constant.ORDER_GET_PARAM:
                                if (TimeOutCount > 5)
                                {
                                    TimeOutCount = 0;
                                    IsSending = false;
                                    string deviceName = DeviceName;
                                    CallBack.SendError(deviceName, Order);
                                }
                                else
                                {
                                    TimeOutCount++;
                                    GetParam(GetParamDelegate, CallBack);
                                }
                                break;
                            default:
                                if (TimeOutCount > 5)
                                {
                                    TimeOutCount = 0;
                                    IsSending = false;
                                    string deviceName = DeviceName;
                                    CallBack.SendError(deviceName, Order);
                                }
                                else
                                {
                                    TimeOutCount++;
                                    Send(null, Constant.ORDER_GET_PARAM, null);
                                }
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
        /// 接收消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="s"></param>
        private void ReceiveData(object sender, SerialDataReceivedEventArgs s)
        {
            IsTiomeOutThreadStart = false;
            IsReceive = true;
            string receiveStr = "";
            string deviceName = DeviceName;
            switch (Order)
            {
                case Constant.ORDER_BEGIN_SEND:
                    Thread.Sleep(100);
                    break;
                case Constant.ORDER_PUT:
                    Thread.Sleep(50);
                    break;
                case Constant.ORDER_END_SEND:
                    Thread.Sleep(50);
                    break;
                case Constant.ORDER_PUT_PARAM:
                    Thread.Sleep(50);
                    break;
                case Constant.ORDER_GET_PARAM:
                    Thread.Sleep(500);
                    break;
                default:
                    Thread.Sleep(50);
                    break;
            }
            int count = ComDevice.BytesToRead;
            byte[] readBuff = new byte[count];
            ComDevice.Read(readBuff, 0, count);
            receiveStr = Encoding.Default.GetString(readBuff);
            switch (Order)
            {
                case Constant.ORDER_BEGIN_SEND:
                    switch (receiveStr.Split(':')[0])
                    {
                        case Constant.RECEIVE_ORDER_BEGIN_OK:
                            DownloadStatus = true;
                            break;
                        case Constant.RECEIVE_ORDER_BEGIN_ERROR:
                            break;
                        default:
                            try
                            {
                                DownloadThread.Abort();
                            }
                            finally
                            {
                                DownloadStatus = false;
                                IsSending = false;
                                DownloadProgressDelegate("", 0);
                                CallBack.SendError(deviceName, Order);
                            }
                            break;
                    }
                    break;
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
                                IsSending = false;
                                DownloadProgressDelegate("", 0);
                                CallBack.SendError(deviceName, Order);
                            }
                            break;
                    }
                    break;
                case Constant.ORDER_END_SEND:
                    switch (receiveStr.Split(':')[0])
                    {
                        case Constant.RECEIVE_ORDER_END_OK:
                            DownloadStatus = true;
                            IsSending = false;
                            DownloadProgressDelegate("", 0);
                            CallBack.SendCompleted(deviceName, Order);
                            break;
                        case Constant.RECEIVE_ORDER_END_ERROR:
                        default:
                            try
                            {
                                DownloadThread.Abort();
                            }
                            finally
                            {
                                DownloadStatus = false;
                                IsSending = false;
                                DownloadProgressDelegate("", 0);
                                CallBack.SendError(deviceName, Order);
                            }
                            break;
                    }
                    break;
                case Constant.ORDER_PUT_PARAM:
                    switch (receiveStr)
                    {
                        case Constant.RECEIVE_ORDER_PUT_PARA:
                            SendDataPackage();
                            break;
                        case Constant.RECEIVE_ORDER_DONE:
                            Console.WriteLine("下载完成");
                            IsSending = false;
                            CallBack.SendCompleted(deviceName, Order);
                            break;
                        default:
                            IsSending = false;
                            CallBack.SendError(deviceName, Order);
                            break;
                    }
                    break;
                case Constant.ORDER_GET_PARAM:
                    GetParamDelegate(new DMXHardware(readBuff));
                    break;
                default:
                    switch (receiveStr.Split(':')[0])
                    {
                        case Constant.RECEIVE_ORDER_OTHER_OK:
                            IsSending = false;
                            CallBack.SendCompleted(deviceName, Order);
                            break;
                        case Constant.RECEIVE_ORDER_OTHER_ERROR:
                        default:
                            IsSending = false;
                            CallBack.SendError(deviceName, Order);
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 下载所有文件数据
        /// </summary>
        public void Download(DBWrapper wrapper,string configPath,IReceiveCallBack receiveCallBack,DownloadProgressDelegate download)
        {
            if (ComDevice.IsOpen && !IsSending)
            {
                DBWrapper = wrapper;
                ConfigPath = configPath;
                CallBack = receiveCallBack;
                DownloadProgressDelegate = download;
                IsSending = true;
                DownloadThread = new Thread(new ThreadStart(DownloadThreadStart))
                {
                    IsBackground = true
                };
                DownloadThread.Start();
            }
        }

        /// <summary>
        /// 下载数据线程
        /// </summary>
        private void DownloadThreadStart()
        {
            CurrentDownloadCompletedSize = 0;
            IList<DMX_C_File> c_Files = DataTools.GetInstance().GetC_Files(DBWrapper, ConfigPath);
            IList<DMX_M_File> m_Files = DataTools.GetInstance().GetM_Files(DBWrapper, ConfigPath);
            DMXConfigData configData = DataTools.GetInstance().GetConfigData(DBWrapper, ConfigPath);
            byte[] configFileData = configData.GetConfigData();
            foreach (DMX_C_File item in c_Files)
            {
                DownloadFileToTalSize += item.GetByteData().Length;
            }
            foreach (DMX_M_File item in m_Files)
            {
                if (item.Data.Datas.Count > 0)
                {
                    DownloadFileToTalSize += item.GetByteData().Length;
                }
            }
            DownloadFileToTalSize += configFileData.Length;
            DownloadStatus = false;
            Send(data: null, order: Constant.ORDER_BEGIN_SEND, strarray: null);
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
                        CurrentFileName = fileName;
                        Send(data: item.GetByteData(), order: Constant.ORDER_PUT, strarray: new string[] { fileName, fileSize, fileCRC });
                        DownloadStatus = false;
                        break;
                    }
                }
            }
            foreach (DMX_M_File item in m_Files)
            {
                if (item.Data.Datas.Count > 0)
                {
                    fileName = "M" + (item.SceneNo + 1) + ".bin";
                    fileSize = item.GetByteData().Length.ToString();
                    crc = CRCTools.GetInstance().GetCRC(item.GetByteData());
                    fileCRC = crc[0].ToString() + crc[1].ToString();
                    while (true)
                    {
                        if (DownloadStatus)
                        {
                            CurrentFileName = fileName;
                            Send(data: item.GetByteData(), order: Constant.ORDER_PUT, strarray: new string[] { fileName, fileSize, fileCRC });
                            DownloadStatus = false;
                            break;
                        }
                    }
                }
            }
            fileName = "Config.bin";
            fileSize = configFileData.Length.ToString();
            crc = CRCTools.GetInstance().GetCRC(configData.GetConfigData());
            fileCRC = crc[0].ToString() + crc[1].ToString();
            while (true)
            {
                if (DownloadStatus)
                {
                    CurrentFileName = fileName;
                    Send(data: configFileData, order: Constant.ORDER_PUT, strarray: new string[] { fileName, fileSize, fileCRC });
                    DownloadStatus = false;
                    break;
                }
            }
            while (true)
            {
                if (DownloadStatus)
                {
                    Send(data: null, order: Constant.ORDER_END_SEND, strarray: null);
                    DownloadStatus = false;
                    break;
                }
            }
        }

        /// <summary>
        /// 发送配置文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="receiveCallBack"></param>
        public void PutParam(string filePath, IReceiveCallBack receiveCallBack)
        {
            if (!IsSending && ComDevice.IsOpen)
            {
                CallBack = receiveCallBack;
                HardwarePath = filePath;
                IsSending = true;
                DMXHardware hardware = new DMXHardware(HardwarePath);
                byte[] data = hardware.GetHardware();
                string fileName = "Hardware.bin";
                string fileSize = data.Length.ToString();
                byte[] crc = CRCTools.GetInstance().GetCRC(data);
                string fileCRC = crc[0].ToString() + crc[1].ToString();
                Send(data, Constant.ORDER_PUT_PARAM, new string[] { fileName, fileSize, fileCRC });
            }
        }

        /// <summary>
        /// 读取硬件配置参数
        /// </summary>
        /// <param name="getParam"></param>
        public void GetParam(GetParamDelegate getParam, IReceiveCallBack receiveCall)
        {
            if (!IsSending)
            {
                CallBack = receiveCall;
                this.GetParamDelegate = getParam;
                IsSending = true;
                Send(null, Constant.ORDER_GET_PARAM, null);
            }
        }
    }
}
