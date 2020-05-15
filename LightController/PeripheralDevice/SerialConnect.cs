using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightController.PeripheralDevice
{
    public class SerialConnect : BaseCommunication
    {
        private const int DEFAULT_BAUDRATE = 115200;
        private SerialPort SerialPortDevice { get; set; }
        private int CurrentBaudRate { get; set; }
        private bool IsDeviceOpen { get; set; }
        private Order Order { get; set; }
        public SerialConnect()
        {
            this.CurrentBaudRate = DEFAULT_BAUDRATE;
            this.Init();
            this.DeviceAddr = UDPADDR;
        }
        public List<string> GetSerialPortNames()
        {
            return SerialPort.GetPortNames().ToList();
        }
        public override void OpenSerialPort(string portName)
        {
            this.IsDeviceOpen = false;
            this.CurrentBaudRate = DEFAULT_BAUDRATE;
            if (this.SerialPortDevice == null)
            {
                this.SerialPortDevice = new SerialPort();
                this.SetSerialPort();
            }
            if (this.SerialPortDevice.IsOpen)
            {
                this.SerialPortDevice.Close();
            }
            this.SerialPortDevice.DataReceived += new SerialDataReceivedEventHandler(ReceiveData);
            this.SerialPortDevice.PortName = portName;
            Thread.Sleep(100);
            this.IsDeviceOpen = true;
            this.SerialPortDevice.Open();
        }
        private void SetSerialPort()
        {
            this.SerialPortDevice.BaudRate = this.CurrentBaudRate;
            this.SerialPortDevice.StopBits = StopBits.One;
            this.SerialPortDevice.DataBits = 8;
            this.SerialPortDevice.Parity = Parity.None;
        }
        protected void ReceiveData(object sender, SerialDataReceivedEventArgs s)
        {
            try
            {
                while (this.IsDeviceOpen)
                {
                    ReadBuff.Add(Convert.ToByte(SerialPortDevice.ReadByte()));
                    this.Receive();
                }
                ReadBuff.Clear();
            }
            catch (Exception ex)
            {
                this.IsDeviceOpen = false;
                LogTools.Debug(Constant.TAG_XIAOSA, "串口已关闭或串口接收模块发生异常");
            }
            finally
            {
                ReadBuff.Clear();
            }
        }
        protected override void Send(byte[] data)
        {
            this.SerialPortDevice.Write(data, 0, data.Length);
            this.SendDataCompleted();
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public override void DisConnect()
        {
            if (this.SerialPortDevice != null)
            {
                if (this.SerialPortDevice.IsOpen)
                {
                    this.SerialPortDevice.Close();
                }
            }
        }

        public override bool IsConnected()
        {
            if (this.SerialPortDevice != null)
            {
                return this.SerialPortDevice.IsOpen;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 串口模式下不操作
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        public override bool Connect(NetworkDeviceInfo deviceInfo)
        {
            return false;
        }
    }
}
