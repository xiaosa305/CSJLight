using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Timers;

namespace LightController.Tools
{
    public class Dmx512Test
    {
        private const int T = 50;
        private const int SenDT = 21;
        private SerialPort Com { get; set; }
        private List<byte> DataBuff { get; set; }
        private Timer Timer { get; set; }
        private int Index { get; set; }

        public Dmx512Test()
        {
            Com = new SerialPort();
            Com.StopBits = StopBits.Two;
            Com.Parity = Parity.None;
            Com.DataBits = 8;
            Com.BaudRate = 250000;
            Com.DataReceived += new SerialDataReceivedEventHandler(Receive);
            DataBuff = Enumerable.Repeat(Convert.ToByte(0x00), 513).ToList();
            Timer = new Timer(T - SenDT);
            Timer.Elapsed += TimerAction;
            Timer.AutoReset = true;
            Index = 0;
            DataBuff[3] = 0x64;
        }

        public void Open()
        {
            if (Com.IsOpen)
            {
                Com.Close();
            }
            Com.PortName = "COM4";
            Com.Open();
        }

        public void Start()
        {
            Timer.Enabled = true;
            Timer.Start();
        }

        private void TimerAction(object sender, ElapsedEventArgs e)
        {
            DataBuff[1] = Convert.ToByte(Index);
            Com.Write(DataBuff.ToArray(), 0, DataBuff.ToArray().Length);
            Index++;
            if (Index > 255)
            {
                Index = 0;
            }
        }

        private void Receive(Object sender,SerialDataReceivedEventArgs s)
        {

        }
    }
}
