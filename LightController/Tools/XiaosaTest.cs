using LightController.Entity;
using LightController.PeripheralDevice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class XiaosaTest
    {
        private static XiaosaTest Instance { get; set; }
        private SerialConnect SerialConnect { get; set; }

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

        public void OpenSerialPort()
        {
          
            //if (SerialConnect == null)
            //{
            //    SerialConnect = new SerialConnect();
            //}
            //SerialConnect.OpenSerialPort("COM14");
        }

        public void Download(CCEntity entity)
        {
            //SerialConnect.CenterControlDownload(entity,CCDCompleted,CCDError);
        }

        private void CCDCompleted(Object obj)
        {
            Console.WriteLine("中控设备下载成功");
        }

        private void CCDError()
        {
            Console.WriteLine("中控设备下载失败");
        }
    }
}
