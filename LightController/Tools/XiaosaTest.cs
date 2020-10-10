using DMX512;
using LightController.Ast;
using LightController.Entity;
using LightController.PeripheralDevice;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public void BigDataTest(IList<DB_Value> values)
        {
            Dictionary<int, Dictionary<int, Dictionary<int, DB_Value>>> C_SceneData = new Dictionary<int, Dictionary<int, Dictionary<int, DB_Value>>>();
            Dictionary<int, Dictionary<int, Dictionary<int, DB_Value>>> M_SceneData = new Dictionary<int, Dictionary<int, Dictionary<int, DB_Value>>>();
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("开始整理");
            stopwatch.Start();
            foreach (DB_Value item in values)
            {
                if (item.PK.Mode == Constant.MODE_C)
                {
                    if (!C_SceneData.ContainsKey(item.PK.Frame))
                    {
                        C_SceneData.Add(item.PK.Frame, new Dictionary<int, Dictionary<int, DB_Value>>());
                    }
                    if (!C_SceneData[item.PK.Frame].ContainsKey(item.PK.LightID))
                    {
                        C_SceneData[item.PK.Frame].Add(item.PK.LightID, new Dictionary<int, DB_Value>());
                    }
                    C_SceneData[item.PK.Frame][item.PK.LightID][item.PK.Step] = item;
                }
                else
                {
                    if (!M_SceneData.ContainsKey(item.PK.Frame))
                    {
                        M_SceneData.Add(item.PK.Frame, new Dictionary<int, Dictionary<int, DB_Value>>());
                    }
                    if (!M_SceneData[item.PK.Frame].ContainsKey(item.PK.LightID))
                    {
                        M_SceneData[item.PK.Frame].Add(item.PK.LightID, new Dictionary<int, DB_Value>());
                    }
                    if (item.ChangeMode == Constant.MODE_M_JUMP)
                    {
                        M_SceneData[item.PK.Frame][item.PK.LightID][item.PK.Step] = item;
                    }
                }
            }
            Console.WriteLine("完成,耗时：" + stopwatch.ElapsedMilliseconds + "毫秒");
            stopwatch.Stop();
        }
        bool flag;
        public void Old2NewTest()
        {
            flag = true;
            BasicSceneConfig config = new BasicSceneConfig(false, 300, 10);
            OldFileToNewFileUtils.GetInstance().ReadOldFile(@"C:\Users\99729\Desktop\test\传世界原512控制器 （视易系统）\传世界原512控制器 （视易系统）\1.BIN", @"C:\Users\99729\Desktop\result", @"C1", config, FileKind.Record_Basic, this.Completed1, this.Error);
         
        }
        private void Completed1()
        {
            MusicSceneConfig musicconfig = new MusicSceneConfig(@"C:\Users\99729\Desktop\test\传世界原512控制器 （视易系统）\传世界原512控制器 （视易系统）\1.BIN");
            musicconfig.MusicIntervalTime = 300;
            musicconfig.MusicStepTime = 30;
            musicconfig.MusitStepList = new int[] { 2, 1, 3 };
            OldFileToNewFileUtils.GetInstance().ReadOldFile(@"C:\Users\99729\Desktop\test\传世界原512控制器 （视易系统）\传世界原512控制器 （视易系统）\M1.BIN", @"C:\Users\99729\Desktop\result", @"M1", musicconfig, FileKind.Record_Music, this.Completed2, this.Error);
        }

        private void Completed2()
        {
            Console.WriteLine("转换完成");
            flag = false;
        }

        private void Error(string msg)
        {
            Console.WriteLine("转换失败：" + msg);
            flag = false;
        }
    }
}
