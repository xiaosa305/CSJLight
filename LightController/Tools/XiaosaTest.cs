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

        private XiaosaTest()
        {

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
            this.OnlineTest();
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

        private void Completed2(Object obj,string msg)
        {
            Console.WriteLine("2: " + msg);
            List<OnlineDeviceInfo> devices = this.Connect.DeviceInfos;
            this.Connect.BindDevice(devices[0], Completed3, Error3);
        }

        private void Error2(string msg)
        {
            Console.WriteLine("2: " +msg);
        }

        private void Completed3(Object obj, string msg)
        {
            Console.WriteLine("3: " + msg);
        }

        private void Error3(string msg)
        {
            Console.WriteLine("3: " + msg);
        }
    }
}
