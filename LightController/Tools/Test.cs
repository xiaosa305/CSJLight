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
        public Test(DBWrapper dBWrapper)
        {
            C_Files = DMXTools.GetInstance().Get_C_Files(FormatTools.GetInstance().GetC_SceneDatas(dBWrapper), @"C:\Temp\LightProject\Test1\global.ini");
            M_Files = DMXTools.GetInstance().Get_M_Files(FormatTools.GetInstance().GetM_SceneDatas(dBWrapper), @"C:\Temp\LightProject\Test1\global.ini");
            this.DBWrapper = dBWrapper;
        }


        public void Start(int index)
        {
            switch (index)
            {
                case 1:
                    string localIP = string.Empty;
                    foreach (IPAddress iPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                    {
                        if (iPAddress.AddressFamily.ToString() == "InterNetwork")
                        {
                            localIP = iPAddress.ToString();
                        }
                    }
                    ConnectTools.GetInstance().Start(localIP);
                    break;
                case 2:
                    ConnectTools.GetInstance().SearchDevice();
                    break;
                case 3:
                    Testapplication();
                    break;
                case 4:
                    //SeralPortTest();
                    break;
                default:
                    break;
            }
        }

        private void SeralPortTest()
        {
            string[] list  = SerialPortTools.GetInstance().GetSerialPortNameList();
            SerialPortTools.GetInstance().OpenCom("COM3", 115200);
            SerialPortTools.GetInstance().Download(DBWrapper, @"C:\Temp\LightProject\Test1\global.ini", new DownloadCallBack(), new DownloadProgressDelegate(DownloadProgress));
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
            PlayTools.GetInstance().ReConnectDevice();
            //DMX512Player.GetInstance().Preview(DBWrapper, 0);
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
