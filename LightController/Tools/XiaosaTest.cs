using DMX512;
using LightController.Ast;
using LightController.Entity;
using LightController.PeripheralDevice;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using LightController.Utils.LightConfig;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
    public class XiaosaTest
    {
        private static XiaosaTest Instance { get; set; }
        private SerialConnect SerialConnect { get; set; }
        private OnlineConnect Connect { get; set; }
        private bool Status { get; set; }

        private XiaosaTest()
        {
            this.Status = false;
        }
        public static XiaosaTest GetInstance()
        {
            if (Instance == null)
            {
                Instance = new XiaosaTest();
            }
            return Instance;
        }

        public void Test()
        {
            Console.WriteLine("小撒的测试");
            this.DMXTest();
        }
        private void OnlineTest()
        {
            this.Connect = new OnlineConnect("admin");
            if (this.Connect.Connect(null))
            {
                Thread.Sleep(1000);
                this.Connect.SetSessionId(Completed1, Error1);
            }
        }
        private void Completed1(Object obj, string msg)
        {
            Console.WriteLine("1: " + msg);
            this.Connect.GetOnlineDevices(Completed2, Error2);
        }
        private void Error1(string msg)
        {
            Console.WriteLine("1: " + msg);
        }
        private void Completed2(Object obj, string msg)
        {
            Console.WriteLine("2: " + msg);
            List<OnlineDeviceInfo> devices = this.Connect.DeviceInfos;
            this.Connect.BindDevice(devices[0], Completed3, Error3);
        }
        private void Error2(string msg)
        {
            Console.WriteLine("2: " + msg);
        }
        private void Completed3(Object obj, string msg)
        {
            Console.WriteLine("3: " + msg);
        }
        private void Error3(string msg)
        {
            Console.WriteLine("3: " + msg);
        }

        private void DMXTest()
        {
            if (this.Status ==false)
            {
                this.Status = true;
                Thread thread = new Thread(Task) { IsBackground = true };
                thread.Start();
            }
            else
            {
                this.Status = false;
            }
        }

        private void Task(Object obj)
        {
            SerialPort com = new SerialPort();
            com.BaudRate = 256000;
            com.StopBits = StopBits.Two;
            com.DataBits = 8;
            com.Parity = Parity.None;
            string[] names = SerialPort.GetPortNames();
            com.PortName = "COM8";
            byte[] dmx = Enumerable.Repeat(Convert.ToByte(0x00), 513).ToArray();
            dmx[1] = 0x64;
            dmx[2] = 0x80;
            dmx[5] = 0xFF;
            dmx[6] = 0xFF;
            dmx[7] = 0xFF;
            dmx[8] = 0xFF;
            Console.WriteLine("dmx 开启");
            com.Open();
            while (this.Status)
            {
                try
                {
                    com.BreakState = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                Thread.Sleep(0);
                try
                {
                    com.BreakState = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                Thread.Sleep(0);
                //com.DiscardOutBuffer();
                com.Write(dmx, 0, dmx.Length);
                try
                {
                    com.BreakState = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                Thread.Sleep(30);
            }
            com.Close();
            Console.WriteLine("dmx 关闭");
        }
    }
}
