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
        private Thread DownloadThread { get; set; }
        private bool DownloadStatus { get; set; }
        private byte[] Data { get; set; }
        private string Order { get; set; }
        private string[] Strs { get; set; }
        private RECEIVE MReceive { get; set; }
        private int Package_No { get; set; }
        private int Package_Count { get; set; }
        private int PackageSize { get; set; }
        private List<byte> ReadList { get; set; }


        private SerialPortTools()
        {
            ComDevice = new SerialPort();
            DownloadStatus = true;
            PackageSize = 1016;
            ComDevice.DataReceived += new SerialDataReceivedEventHandler(ReceiveData);
            ComDevice.WriteBufferSize = PackageSize + 8;
            ReadList = new List<byte>();
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

        public bool OpenCom(string portName)
        {
            PortName = portName;
            BaudRate = 9600;
            StopBits = StopBits.Two;
            Parity = Parity.None;
            DataBits = 8;
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
            switch (order)
            {
                case Constant.ORDER_PUT:
                case Constant.ORDER_BEGIN_SEND:
                case Constant.ORDER_END_SEND:
                default:
                    SendOrderPackage();
                    break;
            }
        }

        private void Receive(RECEIVE receive)
        {
            MReceive = receive;
            switch (MReceive)
            {
                case RECEIVE.Send:
                    Console.WriteLine("发送第" + (Package_No + 1) + "包数据，包总数为" + Package_Count);
                    SendDataPackage();
                    break;
                default:
                    break;
            }
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

            Console.Write("Send:    " + Order + " ---");
            foreach (byte value in package)
            {
                Console.Write(Convert.ToString(value, 16) + " ");
            }
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

            Console.Write("SendFile:    " + Order + ",packageNo: " + Package_No + ",packageCount: " + Package_Count + " ---");
            foreach (byte value in package)
            {
                Console.Write("0x" + Convert.ToString(value, 16) + ",");
            }
            ComDevice.Write(package.ToArray(), 0, package.ToArray().Length);
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
            string receiveStr = "";
            int count = ComDevice.BytesToRead;
            byte[] readBuff = new byte[count];
            ComDevice.Read(readBuff, 0, count);
            ReadList.AddRange(readBuff);
            if (ReadList.Count > 2)
            {
                receiveStr = Encoding.Default.GetString(ReadList.ToArray());
                Console.WriteLine("Decoding ====>" + receiveStr);
            }
            Console.WriteLine("==>Received<==");
        }

        public void Download(DBWrapper wrapper,string configPath,IReceiveCallBack receiveCallBack)
        {
            if (ComDevice.IsOpen)
            {
                DBWrapper = wrapper;
                ConfigPath = configPath;
                CallBack = receiveCallBack;
                DownloadThread = new Thread(new ThreadStart(DownloadThreadStart))
                {
                    IsBackground = true
                };
                DownloadThread.Start();
            }
        }

        private void DownloadThreadStart()
        {
            IList<DMX_C_File> c_Files = DataTools.GetInstance().GetC_Files(DBWrapper, ConfigPath);
            IList<DMX_M_File> m_Files = DataTools.GetInstance().GetM_Files(DBWrapper, ConfigPath);
            DMXConfigData configData = DataTools.GetInstance().GetConfigData(DBWrapper, ConfigPath);
            DownloadStatus = false;
            Send(data: null, order: Constant.ORDER_BEGIN_SEND, strarray: null);
            string fileName = "";
            string fileSize = "";
            string fileCRC = "";
            byte[] crc;
            foreach (DMX_C_File item in c_Files)
            {
                if (item.SceneNo < 10)
                {
                    fileName = "C0" + (item.SceneNo + 1) + ".bin";
                }
                else
                {
                    fileName = "C" + (item.SceneNo + 1) + ".bin";
                }
                fileSize = item.GetByteData().Length.ToString();
                crc = CRCTools.GetInstance().GetCRC(item.GetByteData());
                fileCRC = crc[0].ToString() + crc[1].ToString();
                while (true)
                {
                    if (DownloadStatus)
                    {
                        Send(data: item.GetByteData(), order: Constant.ORDER_PUT, strarray: new string[] { fileName, fileSize, fileCRC });
                        DownloadStatus = false;
                        break;
                    }
                }
            }
            foreach (DMX_M_File item in m_Files)
            {
                if (item.SceneNo < 10)
                {
                    fileName = "M0" + (item.SceneNo + 1) + ".bin";
                }
                else
                {
                    fileName = "M" + (item.SceneNo + 1) + ".bin";
                }
                fileSize = item.GetByteData().Length.ToString();
                crc = CRCTools.GetInstance().GetCRC(item.GetByteData());
                fileCRC = crc[0].ToString() + crc[1].ToString();
                while (true)
                {
                    if (DownloadStatus)
                    {
                        Send(data: item.GetByteData(), order: Constant.ORDER_PUT, strarray: new string[] { fileName, fileSize, fileCRC });
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
    }
}
