using LightController.Ast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
            FormatTools formatTools = new FormatTools(dBWrapper.lightList, dBWrapper.stepCountList, dBWrapper.valueList);
            C_Files = DMXTools.GetInstance().Get_C_Files(formatTools.GetC_SenceDatas());
            M_Files = DMXTools.GetInstance().Get_M_Files(formatTools.GetM_SenceDatas());
            this.DBWrapper = dBWrapper;
        }


		public void Start()
        {
           ReadFromFile();
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
            configData.Test();
        }
    }
}
