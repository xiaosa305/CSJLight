using FTD2XX_NET;
using LightController.Ast;
using LightController.Tools.CSJ;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
    class SerialPortTools : ICommunicator
    {
        private static SerialPortTools Instance { get; set; }
        private string PortName { get; set; }
        private int BaudRate { get; set; }
        private StopBits StopBits { get; set; }
        private Parity Parity { get; set; }
        private int DataBits { get; set; }
        private SerialPort ComDevice { get; set; }
        private int ResendCount { get; set; }
        private List<byte> RxBuff { get; set; }

        private SerialPortTools()
        {
            this.ComDevice = new SerialPort();
            this.InitParameters();
            this.SetDefaultSerialPort();
            this.RxBuff = new List<byte>();
            this.PackageSize = Constant.PACKAGE_SIZE_DEFAULT;
            this.Addr = Constant.UDPADDR;
            this.ResendCount = 0;
            this.TimeOutThread = new Thread(new ThreadStart(this.TimeOut))
            {
                IsBackground = true
            };
            this.ComDevice.DataReceived += new SerialDataReceivedEventHandler(this.Recive);
            this.ComDevice.WriteBufferSize = this.PackageSize + 8;
            this.TimeOutThread.Start();
            CSJLogs.GetInstance().DebugLog("Init Completed");
        }
        public static SerialPortTools GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SerialPortTools();
            }
            return Instance;
        }
        private void SetDefaultSerialPort()
        {
            this.PortName = this.GetSerialPortNameList()[0];
            this.BaudRate = 115200;
            this.StopBits = StopBits.One;
            this.Parity = Parity.None;
            this.DataBits = 8;
        }
        public string[] GetSerialPortNameList()
        {
            string[] ports = SerialPort.GetPortNames();
            bool flag;
            List<string> dmx512names = new List<string>();
            List<string> result = new List<string>();
            UInt32 deviceCount = 0;
            FTDI.FT_STATUS status = FTDI.FT_STATUS.FT_OK;
            FTDI dmx512Device = new FTDI();
            status = dmx512Device.GetNumberOfDevices(ref deviceCount);
            if (status == FTDI.FT_STATUS.FT_OK)
            {
                if (deviceCount > 0)
                {
                    FTDI.FT_DEVICE_INFO_NODE[] deviceList = new FTDI.FT_DEVICE_INFO_NODE[deviceCount];
                    status = dmx512Device.GetDeviceList(deviceList);
                    if (status == FTDI.FT_STATUS.FT_OK)
                    {
                        for (int i = 0; i < deviceList.Length; i++)
                        {
                            status = dmx512Device.OpenBySerialNumber(deviceList[i].SerialNumber);
                            if (status == FTDI.FT_STATUS.FT_OK)
                            {
                                string portName;
                                dmx512Device.GetCOMPort(out portName);
                                if (portName != null && portName != "")
                                {
                                    dmx512names.Add(portName);
                                    dmx512Device.Close();
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < ports.Length; i++)
            {
                flag = true;
                foreach (string item in dmx512names)
                {
                    if (item.Equals(ports[i]))
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    result.Add(ports[i]);
                }
            }
            return result.ToArray();
        }
        public string[] GetDMX512DeviceList()
        {
            CSJLogs.GetInstance().DebugLog("1");
            return null;
            //try
            //{
            //    List<string> deviceNameList = new List<string>();
            //    CSJLogs.GetInstance().DebugLog("1");
            //    UInt32 deviceCount = 0;
            //    CSJLogs.GetInstance().DebugLog("2");

            //    FTDI.FT_STATUS status = FTDI.FT_STATUS.FT_OK;
            //    CSJLogs.GetInstance().DebugLog("3");

            //    FTDI dmx512Device = new FTDI();
            //    CSJLogs.GetInstance().DebugLog("4");

            //    status = dmx512Device.GetNumberOfDevices(ref deviceCount);
            //    CSJLogs.GetInstance().DebugLog("5");

            //    if (status == FTDI.FT_STATUS.FT_OK)
            //    {
            //        if (deviceCount > 0)
            //        {
            //            FTDI.FT_DEVICE_INFO_NODE[] deviceList = new FTDI.FT_DEVICE_INFO_NODE[deviceCount];
            //            CSJLogs.GetInstance().DebugLog("6");

            //            status = dmx512Device.GetDeviceList(deviceList);
            //            CSJLogs.GetInstance().DebugLog("7");

            //            if (status == FTDI.FT_STATUS.FT_OK)
            //            {
            //                for (int i = 0; i < deviceList.Length; i++)
            //                {
            //                    status = dmx512Device.OpenBySerialNumber(deviceList[i].SerialNumber);
            //                    CSJLogs.GetInstance().DebugLog("8");

            //                    if (status == FTDI.FT_STATUS.FT_OK)
            //                    {
            //                        string portName;
            //                        dmx512Device.GetCOMPort(out portName);
            //                        CSJLogs.GetInstance().DebugLog("9");

            //                        if (portName != null && portName != "")
            //                        {
            //                            deviceNameList.Add(portName);
            //                            CSJLogs.GetInstance().DebugLog("10");

            //                            dmx512Device.Close();
            //                            CSJLogs.GetInstance().DebugLog("11");

            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    return deviceNameList.ToArray();
            //}
            //catch (Exception ex)
            //{
            //    CSJLogs.GetInstance().ErrorLog(ex);
            //    return null;
            //}
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
            if (ComDevice.IsOpen)
            {
                ComDevice.Close();
            }
            SetSerialPort();
            ComDevice.Open();
            Console.WriteLine("串口" + PortName + "已打开");
            return ComDevice.IsOpen;
        }
        protected override void Send(byte[] txBuff)
        {
            if (ComDevice.IsOpen)
            {
                ComDevice.DiscardInBuffer();
                ComDevice.DiscardOutBuffer();
                RxBuff.Clear();
                ComDevice.Write(txBuff, 0, txBuff.Length);
                SendComplected();
            }
        }
        protected void Recive(object sender, SerialDataReceivedEventArgs s)
        {
            int packageDataSize = 1;
            byte[] packageHead = new byte[Constant.PACKAGEHEAD_SIZE];
            while (true)
            {
                try
                {
                    RxBuff.Add(Convert.ToByte(ComDevice.ReadByte()));
                    if (RxBuff.Count == Constant.PACKAGEHEAD_SIZE)
                    {
                        for (int i = 0; i < Constant.PACKAGEHEAD_SIZE; i++)
                        {
                            packageHead[i] = RxBuff[i];
                        }
                        if (packageHead[0] != Convert.ToByte(0xAA) || packageHead[1] != Convert.ToByte(0xBB) || packageHead[2] != Convert.ToByte(0x00) || packageHead[5] != Convert.ToByte(Constant.MARK_DATA_END, 2))
                        {
                            CloseDevice();
                            break;
                        }
                        else
                        {
                            packageDataSize = (packageHead[3] & 0xFF) | ((packageHead[4] << 8) & 0xFF);
                        }
                    }
                    else if (RxBuff.Count == packageDataSize + Constant.PACKAGEHEAD_SIZE)
                    {
                        RxBuff[6] = Convert.ToByte(0x00);
                        RxBuff[7] = Convert.ToByte(0x00);
                        byte[] packageCRC = CRCTools.GetInstance().GetCRC(RxBuff.ToArray());
                        if (packageCRC[0] != packageHead[6] || packageCRC[1] != packageHead[7])
                        {
                            CloseDevice();
                            break;
                        }
                        else
                        {
                            byte[] packageData = new byte[packageDataSize];
                            Array.Copy(RxBuff.ToArray(), 8, packageData, 0, packageDataSize);
                            ReceiveMessageManage(packageData, packageDataSize);
                            RxBuff.Clear();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error :" + ex.Message);
                }
            }
        }
        public override void CloseDevice()
        {
            this.DownloadStatus = false;
            try
            {
                switch (this.Order)
                {
                    case Constant.ORDER_PUT_PARAM:
                        break;
                    case Constant.ORDER_GET_PARAM:
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
                InitParameters();
            }
        }
    }
}
