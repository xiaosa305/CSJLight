using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    class SerialPortTools
    {
        private static SerialPortTools Instance { get; set; }
        private string PortName { get; set; }
        private int BaudRate { get; set; }
        private StopBits StopBits { get; set; }
        private Parity Parity { get; set; }
        public int DataBits { get; set; }
        private SerialPort ComDevice { get; set; }

        private SerialPortTools()
        {
            ComDevice = new SerialPort();
        }

        public static SerialPortTools GetInstance()
        {
            if(Instance == null)
            {
                Instance = new SerialPortTools();
            }
            return Instance;
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
            BaudRate = 250000;
            StopBits = StopBits.Two;
            Parity = Parity.None;
            DataBits = 8;
            SetSerialPort();
            ComDevice.Open();
            return ComDevice.IsOpen;
        }

        public void Download()
        {
            if (ComDevice.IsOpen)
            {

            }
        }
    }
}
