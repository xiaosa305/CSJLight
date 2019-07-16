using LightController.Ast;
using System;
using System.Collections.Generic;
using System.IO;
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
        private IList<DMX_C_File> C_Files { get; set; }
        private IList<DMX_M_File> M_Files { get; set; }
        private DBWrapper DBWrapper;
        private static int i = 0;
        public Test(DBWrapper dBWrapper)
        {
            C_Files = DMXTools.GetInstance().Get_C_Files(FormatTools.GetInstance().GetC_SenceDatas(dBWrapper), @"C:\Temp\LightProject\Test1\global.ini");
            M_Files = DMXTools.GetInstance().Get_M_Files(FormatTools.GetInstance().GetM_SenceDatas(dBWrapper), @"C:\Temp\LightProject\Test1\global.ini");
            this.DBWrapper = dBWrapper;
        }


        public void Start()
        {
            //Testapplication();
            PreViewTest();

        }

        public void WriteToFile()
        {
            foreach (DMX_C_File file in C_Files)
            {
                file.WriteFile(@"C:\Temp\");
            }
            foreach (DMX_M_File file in M_Files)
            {
                if (file.Data.Datas.Count() != 0)
                {
                    file.WriteFile(@"C:\Temp\");
                }
            }
            Console.WriteLine("Connect Test");
        }

        public void PreViewTest()
        {
            PlayTools.GetInstance().MusicControl();
        }

        public void Testapplication()
        {
            if (i == 0)
            {
                string localIP = string.Empty;
                foreach (IPAddress iPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (iPAddress.AddressFamily.ToString() == "InterNetwork")
                    {
                        localIP = iPAddress.ToString();
                    }
                }
                ConnectTools.GetInstance().Start(localIP);
            }
            ConnectTools.GetInstance().SearchDevice();
            if (i > 0)
            {
                IList<string> iplist = new List<string>();
                foreach (string item in ConnectTools.GetInstance().GetDevicesIp())
                {
                    if (item.Length > 0)
                    {
                        iplist.Add(item);
                    }
                }
                ConnectTools.GetInstance().Download(iplist.ToArray(), DBWrapper, @"C:\Temp\LightProject\Test1\global.ini",new DownloadCallBack());
            }
            i++;
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
