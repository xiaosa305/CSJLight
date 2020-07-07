using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace LightDog.tools
{
    public class SerialPortTool : Conmunicator
    {
        private static SerialPortTool Instant { get; set; }
        private SerialPort SerialPortDevice { get; set; }
        private bool ReceiveStatus { get; set; }
        private System.Timers.Timer OpenSerialPortTimer { get; set; }

        private SerialPortTool()
        {
            this.InitParam();
            this.SerialPortDevice = new SerialPort()
            {
                BaudRate = 115200,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One
            };
            this.ReceiveStatus = false;
            this.SerialPortDevice.DataReceived += new SerialDataReceivedEventHandler(ReceiveAction);
            this.OpenSerialPortTimer = new System.Timers.Timer()
            {
                AutoReset = false
            };
            this.OpenSerialPortTimer.Elapsed += new ElapsedEventHandler((s, e) => OpenSerialPortStart(s, e));

        }
        public static SerialPortTool GetInstant()
        {
            if (Instant == null)
            {
                Instant = new SerialPortTool();
            }
            return Instant;
        }


        public void OpenSerialPort(Completed completed,Error error)
        {
            if (this.OpenSerialPortTimer.Enabled)
            {
                return;
            }
            else
            {
                this.Completed_Event = completed;
                this.Error_Event = error;
                this.OpenSerialPortTimer.Start();
            }
        }

        private void OpenSerialPortStart(Object obj, ElapsedEventArgs e)
        {
            string[] portName = SerialPort.GetPortNames();
            foreach (string name in portName)
            {
                try
                {
                    if (this.SerialPortDevice.IsOpen)
                    {
                        this.ReceiveStatus = false;
                        this.SerialPortDevice.Close();
                    }
                    this.SerialPortDevice.PortName = name;
                    Console.WriteLine("搜索串口：" + name);
                    this.ReceiveStatus = true;
                    this.SerialPortDevice.Open();
                    this.Flag = false;
                    this.CheckDevice();
                    Thread.Sleep(200);
                    if (this.Flag)
                    {
                        Console.WriteLine("搜到设备");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
               
            }
            if (this.Flag)
            {
                this.Completed_Event(null, "搜索设备成功");
            }
            else
            {
                this.Error_Event(null, "搜索设备失败");
            }
        }

        public bool CloseSerialPort()
        {
            try
            {
                if (this.SerialPortDevice.IsOpen)
                {
                    this.SerialPortDevice.Close();
                }
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void ReceiveAction(Object sender,SerialDataReceivedEventArgs s)
        {
            while (this.ReceiveStatus)
            {
                try
                {
                    this.RxBuff.Add(Convert.ToByte(this.SerialPortDevice.ReadByte()));
                    if (this.RxBuff.Count >= 8)
                    {
                        if (this.RxBuff.Count == 13)
                        {
                            Console.WriteLine("");
                        }
                        Console.WriteLine(Encoding.Default.GetString(RxBuff.ToArray()));
                        if (this.CheckData())
                        {
                            lock (this.Queue)
                            {
                                this.Queue.Enqueue(this.RemovePackageHead());
                            }
                            RxBuff.Clear();
                        }
                    }
                }
                catch (Exception)
                {
                    this.ReceiveStatus = false;
                    break;
                }
            }
            this.RxBuff.Clear();
        }

        public override void Send(byte[] data,int offset,int count)
        {
            try
            {
                foreach (byte item in data)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine("");
                this.SerialPortDevice.Write(data, offset, count);
                this.SendCompleted(null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
