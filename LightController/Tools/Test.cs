using LightController.Ast;
using LightController.MyForm;
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
        public Test(DBWrapper dBWrapper, MainFormInterface mainForm, string configPath)
        {
            this.DBWrapper = dBWrapper;
            this.MainForm = mainForm;
            this.ConfigPath = configPath;
            DataConvertUtils.InitThreadPool();
        }
        public void Start(int index)
        {
            string[] ports = SerialPortTools.GetInstance().GetSerialPortNameList();
            switch (index)
            {
                case 1:
                    PlayTools.GetInstance().TestOpen();
                    break;
                case 2:
                    PlayTools.GetInstance().SetTest();
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    break;
            }
        }

        private class aaa : ISaveProjectCallBack
        {
            public void Completed()
            {
            }

            public void Error()
            {

            }

            public void UpdateProgress(string name)
            {
            }
        }

        private Test(DBWrapper wrapper)
        {

        }
    }
}
