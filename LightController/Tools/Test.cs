using LightController.Ast;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
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
        public Test(DBWrapper dBWrapper, ValueDAO valueDAO, string configPath)
        {
            this.DBWrapper = dBWrapper;
            this.ValueDAO = valueDAO;
            this.ConfigPath = configPath;
            DataConvertUtils.InitThreadPool();
        }
        public void Start(int index)
        {
            string[] ports = SerialPortTools.GetInstance().GetSerialPortNameList();
            switch (index)
            {
                case 1:
                    DataConvertUtils.SaveProjectFile(DBWrapper, ValueDAO, ConfigPath);
                    break;
                case 2:
                    DataConvertUtils.SaveProjectFileByPreviewData(DBWrapper, ConfigPath, 0);
                    break;
                case 3:
                    FileUtils.CreateGradientData();
                    break;
                case 4:
                    break;
                default:
                    break;
            }
        }

        private Test(DBWrapper wrapper)
        {

        }
    }
}
