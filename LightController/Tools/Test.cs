using LightController.Ast;
using LightController.MyForm;
using LightController.PeripheralDevice;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
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
        private DBWrapper DBWrapper { get; set; }
        private ValueDAO ValueDAO { get; set; }
        private string ConfigPath { get; set; }
        private MainFormInterface MainForm { get; set; }
        private static SerialConnect SerialConnect { get; set; }
        public Test(DBWrapper dBWrapper, MainFormInterface mainForm, string configPath)
        {
            this.DBWrapper = dBWrapper;
            this.MainForm = mainForm;
            this.ConfigPath = configPath;
            DataConvertUtils.InitThreadPool();
            if (SerialConnect == null)
            {
                SerialConnect = new SerialConnect();
                Console.WriteLine("new了一个Test");
            }
        }
        public void Start(int index)
        {
            switch (index)
            {
                case 1:
                    //SerialPortTools.GetInstance().OpenCom("COM15");
                    SerialConnect.OpenSerialPort("COM16");
                    //SerialConnect.OpenSerialPort("COM15");
                    break;
                case 2:
                    //SerialPortTools.GetInstance().NewLightControlConnect(new CallbackTest());
                    SerialConnect.LightControlConnect(LCCCompleted, LCCError);
                    break;
                case 3:
                    //SerialPortTools.GetInstance().NewLightControlRead(new CallbackTest());
                    SerialConnect.LightControlRead(LCRCompleted, LCRError);
                    break;
                case 4:
                    //SerialPortTools.GetInstance().NewLightControlDownload(LightControlData.GetTestData(), new CallbackTest());
                    //SerialConnect.LightControlDownload(LightControlData.GetTestData(), LCDCompleted, LCDError);
                    SerialConnect.LightControlDebug(new byte[] { 0xFF,0x0F }, LCDDebugError);
                    break;
                default:
                    break;
            }
        }
        private void LCCCompleted(Object obj)
        {
            Console.WriteLine("设备链接成功，下一步进行读取配置信息");
            SerialConnect.LightControlRead(LCRCompleted,LCRError);
        }
        private void LCCError()
        {
            Console.WriteLine("链接设备失败");
        }
        private void LCRCompleted(Object obj)
        {
            Console.WriteLine("设备读取配置信息成功");
            LightControlData data = obj as LightControlData;
        }
        private void LCRError()
        {
            Console.WriteLine("设备读取配置失败");
        }
        private void LCDCompleted(Object obj)
        {
            Console.WriteLine("设备下载配置信息成功");
        }
        private void LCDError()
        {
            Console.WriteLine("设备下载配置信息失败");
        }

        private void LCDDebugError()
        {
            Console.WriteLine("设备调试失败");
        }
    }

    class CallbackTest : ICommunicatorCallBack
    {
        public void Completed(string deviceTag)
        {
            Console.WriteLine("完成");
        }

        public void Error(string deviceTag, string errorMessage)
        {
            Console.WriteLine("失败");
        }

        public void GetParam(CSJ_Hardware hardware)
        {

        }

        public void UpdateProgress(string deviceTag, string fileName, int newProgress)
        {

        }
    }
}
