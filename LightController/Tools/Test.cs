using LightController.Ast;
using LightController.Common;
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
                    //SerialConnect.OpenSerialPort("COM14");
                    //SerialConnect.OpenSerialPort("COM16");
                    string aa = "";
                    string[] bb = aa.Split(' ');
                    Console.WriteLine();
                    break;
                case 2:
                    SerialConnect.CenterControlConnect(CCCCompleted,CCCError);
                    break;
                case 3:
                    SerialConnect.CenterControlStartCopy(CCCpCompleted, CCCpError);
                    break;
                case 4:
                    SerialConnect.CenterControlStopCopy(CCXpCompleted, CCXpError);
                    break;
                default:
                    break;
            }
        }
        private void CCCCompleted(Object obj)
        {
            Console.WriteLine("设备连接成功");
        }
        private void CCCError()
        {
            Console.WriteLine("设备连接失败");
        }

        private void CCCpCompleted(Object obj)
        {
            List<byte> data = obj as List<byte>;
            string str = "";
            for (int i = 0; i < data.Count; i++)
            {
                str = str + " " + StringHelper.DecimalStringToHex(Convert.ToInt16(data[i]).ToString());
            }
            Console.WriteLine("回读到红外码值:" + str);
        }
        private void CCCpError()
        {
            Console.WriteLine("开启解码失败");
        }
        private void CCXpCompleted(Object obj)
        {
            Console.WriteLine("关闭解码成功");
        }
        private void CCXpError()
        {
            Console.WriteLine("关闭解码失败");
        }


        private void LCCCompleted(Object obj)
        {
            Console.WriteLine("设备链接成功，下一步进行读取配置信息");
            //SerialConnect.LightControlRead(LCRCompleted,LCRError);
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
}
