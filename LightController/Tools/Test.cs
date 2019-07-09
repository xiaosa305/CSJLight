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
            C_Files = DMXTools.GetInstance().Get_C_Files(FormatTools.GetInstance().GetC_SenceDatas(dBWrapper));
            M_Files = DMXTools.GetInstance().Get_M_Files(FormatTools.GetInstance().GetM_SenceDatas(dBWrapper));
            this.DBWrapper = dBWrapper;
        }


		public void Start()
        {
            //ReadFromFile();
            Testapplication();
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

        public void ReadFromFile()
        {
            DMXConfigData configData = new DMXConfigData(this.DBWrapper);
            //configData.Test();
            //ConnectTools.GetInstance().DownLoadData(configData.GetConfigData());
            if (i == 0)
            {
                ConnectTools.GetInstance().SerchDevice();
                i++;
            }
            else
            {
                ConnectTools.GetInstance().Send("192.168.31.236", configData.GetConfigData(), ORDER.Put, new string[] { "Config.bin" });
                i = 0;
            }
        }

        public void Testapplication()
        {
            DMXConfigData configData = new DMXConfigData(this.DBWrapper);
            if (i == 0)
            {

                //ConnectTools.GetInstance().SerchDevice("192.168.31.235",7070);
                SocketTools.GetInstance().Start(new IPEndPoint(IPAddress.Parse("192.168.31.235"), 7070));
                SocketTools.GetInstance().AddConnect("192.168.31.15", 7060);
                //SockTools.GetInstance().AddConnect("192.168.31.236", 7060);

                i++;
            }
            else
            {
                byte[] data = C_Files[0].GetByteData(); ;
                C_Files[0].WriteFile(@"C:\Temp");
                string fileName = "C01.bin";
                byte[] testData = Encoding.Default.GetBytes("Hallo World");
                SocketTools.GetInstance().Send("192.168.31.15", data, ORDER.Put, new string[] { fileName });
                //SockTools.GetInstance().SendData("192.168.31.236", configData.GetConfigData(), ORDER.Put, new string[] { "Config.bin" });
            }
        }
    }
}
