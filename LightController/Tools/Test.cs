using LightController.Ast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            FormatTools formatTools = new FormatTools(dBWrapper.lightList, dBWrapper.stepCountList, dBWrapper.valueList);
            C_Files = DMXTools.GetInstance().Get_C_Files(formatTools.GetC_SenceDatas());
            M_Files = DMXTools.GetInstance().Get_M_Files(formatTools.GetM_SenceDatas());
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
                ConnectTools.GetInstance().SendData("192.168.31.236", configData.GetConfigData(), ORDER.Put, new string[] { "Config.bin" });
                i = 0;
            }
        }

        public void Testapplication()
        {
            DMXConfigData configData = new DMXConfigData(this.DBWrapper);
            if (i == 0)
            {
                ConnectTools.GetInstance().SerchDevice();
                SockTools.GetInstance().AddConnect("192.168.31.15", 7060);
                //SockTools.GetInstance().AddConnect("192.168.31.236", 7060);

                i++;
            }
            else
            {
                byte[] data = C_Files[0].GetByteData(); ;
                C_Files[0].WriteFile(@"C:\Temp\");
                string fileName = "C01.bin";
                SockTools.GetInstance().SendData("192.168.31.15", data, ORDER.Put, new string[] { fileName });
                //SockTools.GetInstance().SendData("192.168.31.236", configData.GetConfigData(), ORDER.Put, new string[] { "Config.bin" });
            }
        }
    }
}
