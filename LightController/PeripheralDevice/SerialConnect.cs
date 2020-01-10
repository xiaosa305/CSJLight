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
        private SerialPort SerialPortDevice { get; set; }
        private int CurrentBaudRate { get; set; }
        private bool IsDeviceOpen { get; set; }
        private Order Order { get; set; }
        public SerialConnect()
        {
            this.CurrentBaudRate = 115200;
            this.Init();
            this.DeviceAddr = UDPADDR;
        }
        public List<string> GetSerialPortNames()
        {
            return SerialPort.GetPortNames().ToList();
        }
        public void OpenSerialPort(string portName)
        {
            this.IsDeviceOpen = false;
            this.CurrentBaudRate = 115200;
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
                    //Console.WriteLine("TAG-XIAOSA-Class:SerialConnect-Method:ReceiveData=>" + "串口开启状态:" + this.SerialPortDevice.IsOpen + ",串口开启状态标记位:" + this.IsDeviceOpen + "串口读取缓存区大小：" + ReadBuff.Count);
                    this.Receive();
                }
                ReadBuff.Clear();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("XIAOSA-ERROR：" + ex.Message);
                //Console.WriteLine("XIAOSA-ERROR：" + ex.StackTrace);
                //Console.WriteLine("关闭串口");
            }
            finally
            {
                ReadBuff.Clear();
            }
        }
        protected override void Send(byte[] data)
        {
            this.SerialPortDevice.DiscardOutBuffer();
            this.SerialPortDevice.DiscardInBuffer();
            this.SerialPortDevice.Write(data, 0, data.Length);
            this.SendDataCompleted();
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public override void DisConnect()
        {
            if (this.SerialPortDevice.IsOpen)
            {
                this.SerialPortDevice.Close();
            }
        }
    }
}
