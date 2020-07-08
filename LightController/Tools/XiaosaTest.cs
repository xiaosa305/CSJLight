using LightController.Ast;
using LightController.Entity;
using LightController.PeripheralDevice;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
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
        private NetworkConnect Connect { get; set; }

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

        public void NewConnectTest(DBWrapper wrapper,string configPath)
        {
            
        }

        public void OpenSerialPort()
        {
          
        }
        public void Download(CCEntity entity)
        {
            
        }

        public void BigDataTest(List<TongdaoWrapper> values)
        {

        }
    }
}
