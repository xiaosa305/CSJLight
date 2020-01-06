using LightController.Entity;
using LightController.PeripheralDevice;
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

            if (SerialConnect == null)
            {
                SerialConnect = new SerialConnect();
            }
            SerialConnect.OpenSerialPort("COM16");
        }

        public void Download(CCEntity entity)
        {
            //SerialConnect.CenterControlDownload(entity,CCDCompleted,CCDError);
            //SerialConnect.KeyPressConnect(KPCConpleted, KPCError);
            SerialConnect.KeyPressRead(KPRCompleted, KPRError);
            //KeyEntity test = KeyEntity.GetTest();
            //SerialConnect.KeyPressDownload(test, KPDCompleted, KPDError);
        }

        private void CCDCompleted(Object obj)
        {
            Console.WriteLine("中控设备下载成功");
        }

        private void KPCConpleted(Object obj)
        {
            Console.WriteLine("墙板设备连接成功");
            Thread.Sleep(500);
        }

        private void KPRCompleted(Object obj)
        {
            KeyEntity entity = obj as KeyEntity;
            Console.WriteLine("墙板设备读取成功");
            //SerialConnect.KeyPressDownload(entity, KPDCompleted, KPDError);
        }

        private void CCDError()
        {
            Console.WriteLine("中控设备下载失败");
        }
        private void KPCError()
        {
            Console.WriteLine("墙板设备连接失败");
        }
        private void KPRError()
        {
            Console.WriteLine("墙板设备读取失败");
        }

        private void KPDCompleted(Object obj)
        {
            Console.WriteLine("墙板设备下载数据成功");
        }

        private void KPDError()
        {
            Console.WriteLine("墙板设备下载数据失败");
        }
    }
}
