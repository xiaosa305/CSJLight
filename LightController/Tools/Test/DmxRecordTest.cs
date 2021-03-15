using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace LightController.Tools.Test
{
    public class DmxRecordTest
    {
        private static DmxRecordTest Instance { get; set; }
        private bool IsDeviceOpen { get; set; }
        private SerialPort SerialPortDevice { get; set; }
        private long DataTimeNow { get; set; }

        private DmxRecordTest()
        {
            this.DataTimeNow = -1;
        }
        public static DmxRecordTest GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DmxRecordTest();
            }
            return Instance;
        }

        public void Start()
        {
            this.IsDeviceOpen = false;
            var names = SerialPort.GetPortNames();
            this.SerialPortDevice = new SerialPort();
            this.SetSerialPort();
            this.SerialPortDevice.DataReceived += new SerialDataReceivedEventHandler(ReceiveData);
            this.IsDeviceOpen = true;
            //this.SerialPortDevice.PortName = names[7];
            this.SerialPortDevice.Open();
            Console.WriteLine("串口打开成功");
        }

        private void SetSerialPort()
        {
            this.SerialPortDevice.PortName = "COM7";
            this.SerialPortDevice.BaudRate = 256000;
            this.SerialPortDevice.StopBits = StopBits.Two;
            this.SerialPortDevice.DataBits = 8;
            this.SerialPortDevice.Parity = Parity.None;
        }

        protected void ReceiveData(object sender, SerialDataReceivedEventArgs s)
        {
            try
            {
                while (this.IsDeviceOpen)
                {
                    SerialPortDevice.ReadByte();
                    if (this.DataTimeNow != -1)
                    {
                        long time = DateTime.Now.Ticks;
                        Console.WriteLine(time - this.DataTimeNow);
                        this.DataTimeNow = time;
                    }
                    else
                    {
                        this.DataTimeNow = DateTime.Now.Ticks;
                    }
                }
            }
            catch (Exception)
            {
                this.IsDeviceOpen = false;
            }
        }
    }
}
