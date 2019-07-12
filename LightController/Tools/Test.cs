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
        }

        public void Testapplication()
        {
            if (i == 0)
            {
                ConnectTools.GetInstance().Start("192.168.31.235");
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
                ConnectTools.GetInstance().Download(iplist.ToArray(), DBWrapper, @"C:\Temp\LightProject\Test4\global.ini");
            }
            i++;
        }
    }
}
