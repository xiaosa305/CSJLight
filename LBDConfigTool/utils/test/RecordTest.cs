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
            manager.Start();
        }
    }
}
