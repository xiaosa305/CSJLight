using LightController.Ast;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
    /// <summary>
    /// 测试用
    /// </summary>
    class Test
    {
        private DBWrapper DBWrapper;
        public Test(DBWrapper dBWrapper,ValueDAO valueDAO,string configPath)
        {
            this.DBWrapper = dBWrapper;
        }
        public void Start(int index)
        {
            string[] ports = SerialPortTools.GetInstance().GetSerialPortNameList();
            switch (index)
            {
                case 1:
                    SerialPortTools.GetInstance().OpenCom("COM3");
                    SerialPortTools.GetInstance().SetPackageSize(SerialPortTools.PacketSize.BYTE_1024);
                    break;
                case 2:
                    break;
                case 3:
                    SerialPortTools.GetInstance().SendOrder("Reset", null, new DownloadCallBack());
                    break;
                case 4:
                    BigDataTest();
                    break;
                default:
                    break;
            }
        }

        private void BigDataTest()
        {
            List<List<long>> group = new List<List<long>>();
            for (long j = 0; j < 512; j++)
            {
                List<long> data = new List<long>();
                for (long i = 0; i < 1000000; i++)
                {
                    data.Add(i);
                }
                group.Add(data);
            }
            Console.WriteLine(group.Count);
        }
        private void GetParamTest(CSJ_Hardware hardware)
        {
            Console.WriteLine("test Complected");
        }
        private void SeralPortTest()
        {
            string[] list  = SerialPortTools.GetInstance().GetSerialPortNameList();
        }
        public void Testapplication()
        {
                IList<string> iplist = new List<string>();
            foreach (string item in ConnectTools.GetInstance().GetDevicesIp())
            {
                if (item.Length > 0)
                {
                    iplist.Add(item);
                }
            }
            try
            {

                ConnectTools.GetInstance().Download(iplist.ToArray(), DBWrapper, @"C:\Temp\LightProject\Test1\global.ini", new DownloadCallBack(),new DownloadProgressDelegate(DownloadProgress));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DownloadProgress(string fileName,int progress)
        {
            Console.WriteLine("===========Download  " + fileName + " : " + progress + "%===========");
        }
    }
    public class DownloadCallBack : IReceiveCallBack
    {
        public void SendCompleted(string ip, string order)
        {
            Console.WriteLine(ip + "===>" + order + ": 下载完成");
        }

        public void SendError(string ip, string order)
        {
            Console.WriteLine(ip + "===>" + order + ": 下载失败");
        }
    }
    public class OrderCallBack : IReceiveCallBack
    {
        public void SendCompleted(string ip, string order)
        {
            Console.WriteLine(ip + "===>" + order + ": 发送成功");
        }

        public void SendError(string ip, string order)
        {
            Console.WriteLine(ip + "===>" + order + ": 发送失败");
        }
    }
}
