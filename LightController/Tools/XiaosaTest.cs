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
            if (this.Connect == null)
            {
                this.Connect = new NetworkConnect(new NetworkDeviceInfo() { DeviceAddr = 1, DeviceIp = "192.168.31.153", DeviceName = "大房101" });
            }
            if (this.Connect.IsConnected())
            {
                //this.Connect.DownloadProject(wrapper, configPath, DownloadCompleted, DownloadError, DownloadProgress);
                //this.Connect.PutParam(@"C:\Users\99729\Dev\Gitee\CSJLight\LightController\HardwareSet.ini", DownloadCompleted, DownloadError);
                //this.Connect.GetParam(DownloadCompleted, DownloadError);
            }
        }

        public void DownloadCompleted(Object obj)
        {
            CSJ_Hardware hardware = obj as CSJ_Hardware;
            Console.WriteLine("下载成功");
        }
        public void DownloadError()
        {
            Console.WriteLine("下载失败");
        }
        public  void DownloadProgress(string fileName,int progress)
        {
            Console.WriteLine(fileName + "下载进度：" + progress);
        }
















        public void OpenSerialPort()
        {
            NetworkDeviceInfo info = new NetworkDeviceInfo();
            info.DeviceIp = "192.168.31.16";
            info.DeviceName = "惊艳PLUS";
            info.DeviceAddr = 1;
            NetworkConnect connect = new NetworkConnect(info);
            Thread.Sleep(300);
            Console.WriteLine("设备连接状态 : " + connect.IsConnected());
            connect.LightControlConnect(LCCCompleted, LCCError);
        }
        public void Download(CCEntity entity)
        {
            
        }
        private void KeyPressClickListener(Object obj)
        {
            List<byte> data = obj as List<byte>;
        }
        private void LCCCompleted(Object obj)
        {
            Console.WriteLine("灯控设备连接成功");
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
        private void LCCError()
        {
            Console.WriteLine("灯控设备连接失败");
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
