﻿using FTD2XX_NET;
using LightController.Ast;
using LightController.PeripheralDevice;
using LightController.Tools.CSJ;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
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
        private bool IsSearchDevice { get; set; }
        private bool IsOpenComdevice { get; set; }
        private SerialPortTools()
        {
            try
            {
                this.ComDevice = new SerialPort();
                this.InitParameters();
                this.IsOpenComdevice = false;
                this.IsSearchDevice = false;
                this.SetDefaultSerialPort();
                this.RxBuff = new List<byte>();
                this.PackageSize = 512;
                this.Addr = Constant.UDPADDR;
                this.ResendCount = 0;
                this.TimeOutThread = new Thread(new ThreadStart(this.TimeOut))
                {
                    IsBackground = true
                };
                this.ComDevice.DataReceived += new SerialDataReceivedEventHandler(this.Recive);
                this.ComDevice.WriteBufferSize = this.PackageSize + 8;
                this.TimeOutThread.Start();
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "初始化串口工具失败", ex);
            }
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
            try
            {
                this.BaudRate = 115200;
                this.StopBits = StopBits.One;
                this.Parity = Parity.None;
                this.DataBits = 8;
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "设置串口默认配置失败", ex);
            }
        }
        public string[] GetSerialPortNameList()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                return ports;
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "获取串口列表失败", ex);
                return null;
            }
        }
        public string[] GetDMX512DeviceList()
        {
            try
            {
                List<string> deviceNameList = new List<string>();
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
                                        deviceNameList.Add(portName);
                                        dmx512Device.Close();
                                    }
                                }
                            }
                        }
                    }
                }
                return deviceNameList.ToArray();
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "获取DMX串口列表失败", ex);
                return null;
            }
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
            try
            {
                IsOpenComdevice = false;
                PortName = portName;
                if (ComDevice.IsOpen)
                {
                    ComDevice.Close();
                }
                SetSerialPort();
                //TODO
                this.ComDevice.DataReceived += new SerialDataReceivedEventHandler(this.Recive);
                ComDevice.Open();
                this.IsOpenComdevice = true;
                //LogTools.Debug(Constant.TAG_XIAOSA, "串口" + PortName + "打开成功");
                return ComDevice.IsOpen;
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "打开串口失败", ex);
                return false;
            }
        }
        public void SearchDevice(ICommunicatorCallBack receiveCallBack)
        {
            try
            {
                this.Addr = Constant.UDPADDR;
                IsSearchDevice = true;
                SendOrder(Constant.ORDER_SEARCH, null, receiveCallBack);
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "搜索设备失败", ex);
            }
            
        }
        protected override void Send(byte[] txBuff)
        {
            if (ComDevice.IsOpen)
            {
                //ComDevice.DiscardInBuffer();
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
            while (this.IsOpenComdevice)
            {
                try
                {
                    this.RxBuff.Add(Convert.ToByte(ComDevice.ReadByte()));
                    if (this.RxBuff.Count == Constant.PACKAGEHEAD_SIZE)
                    {
                        for (int i = 0; i < Constant.PACKAGEHEAD_SIZE; i++)
                        {                                                                                                                                                                                                                                                                                                                                                                                                                                                       
                            packageHead[i] = this.RxBuff[i];
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
                    else if (this.RxBuff.Count == packageDataSize + Constant.PACKAGEHEAD_SIZE)
                    {
                        this.RxBuff[6] = Convert.ToByte(0x00);
                        this.RxBuff[7] = Convert.ToByte(0x00);
                        byte[] packageCRC = CRCTools.GetInstance().GetCRC(this.RxBuff.ToArray());
                        if (packageCRC[0] != packageHead[6] || packageCRC[1] != packageHead[7])
                        {
                            CloseDevice();
                            break;
                        }
                        else
                        {
                            byte[] packageData = new byte[packageDataSize];
                            Array.Copy(this.RxBuff.ToArray(), 8, packageData, 0, packageDataSize);
                            if (IsSearchDevice)
                            {
                                string rxStr = Encoding.Default.GetString(packageData, 0, packageDataSize);
                                string[] rxStrArray = rxStr.Split(' ');
                                this.Addr = int.Parse(rxStrArray[0]);
                                this.DeviceName = rxStrArray[1];
                                CallBack.Completed(DeviceName);
                            }
                            else
                            {
                                ReceiveMessageManage(packageData, packageDataSize);
                            }
                            this.RxBuff.Clear();
                            break;
                        }
                    }
                    IsSearchDevice = false;
                }
                catch (Exception ex)
                {
                    this.IsOpenComdevice = false;
                    LogTools.Debug(Constant.TAG_XIAOSA, "串口已关闭");
                }
            }
            this.RxBuff.Clear();
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
            catch(Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "关闭串口失败", ex);
                CallBack.Error(this.DeviceName, "");
            }
            finally
            {
                if (this.ComDevice.IsOpen)
                {
                    this.ComDevice.Close();
                }
                InitParameters();
            }
        }
    }
}
