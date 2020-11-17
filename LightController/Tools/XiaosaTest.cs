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
            this.Connect.Connect(null);
            Thread.Sleep(2000);
            this.Connect.SetSessionId(Completed2,Error);
            //this.Connect.GetOnlineDevices(Completed2,Error);
        }

        private void Completed2(Object obj,string msg)
        {
            Console.WriteLine(msg);
        }

        private void Error(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
