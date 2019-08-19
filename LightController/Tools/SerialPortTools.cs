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
    class SerialPortTools:ICommunicator
    {
        private static SerialPortTools Instance { get; set; }
        private string PortName { get; set; }
        private int BaudRate { get; set; }
        private StopBits StopBits { get; set; }
        private Parity Parity { get; set; }
        private int DataBits { get; set; }
        private SerialPort ComDevice { get; set; }
        private int ResendCount { get; set; }
        private SerialPortTools()
        {
            this.ComDevice = new SerialPort();
            this.InitParameters();
            this.SetDefaultSerialPort();
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
            return SerialPort.GetPortNames();
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
            return ComDevice.IsOpen;
        }
        protected override void Send(byte[] txBuff)
        {
            if (ComDevice.IsOpen)
            {
                ComDevice.Write(txBuff, 0, txBuff.Length);
                SendComplected();
            }
        }

        protected void Recive(object sender, SerialDataReceivedEventArgs s)
        {
           
        }

        public override void CloseDevice()
        {
            ComDevice.Close();
        }
    }
}
