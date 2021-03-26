using LBDConfigTool.utils.record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBDConfigTool.utils.test
{
    public class RecordTest
    {
        private static RecordTest Instance { get; set; }
        private RecordTest()
        {

        }
        public static RecordTest GetInstance()
        {
            if (Instance == null)
            {
                Instance = new RecordTest();
            }
            return Instance;
        }
        public void Test()
        {
            DMXManager manager = new DMXManager(null);
            string filePath = @"C:\Users\99729\Desktop\test\record";
            manager.StartRecord(filePath + @"\SC000.bin", filePath + @"\csj.scu",Count);
        }
        private void Count(int value)
        {
            Console.WriteLine(value);
        }
    }
}
