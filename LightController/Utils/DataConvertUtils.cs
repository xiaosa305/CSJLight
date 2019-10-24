using DMX512;
using LightController.Ast;
using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightController.Utils
{
    public class DataConvertUtils
    {
        private static Dictionary<int, Dictionary<int, bool>> C_DMXSceneDictionary = new Dictionary<int, Dictionary<int, bool>>();
        private static Dictionary<int, Dictionary<int, bool>> C_SceneStateDictionary = new Dictionary<int, Dictionary<int, bool>>();
        private static Dictionary<int, List<CSJ_ChannelData>> C_SceneChannelDatas = new Dictionary<int, List<CSJ_ChannelData>>();

        private static bool MainThreadState { get; set; }

        private const int WriteBufferSize = 1024*50;
        public static bool GeneratedSceneData(CSJ_SceneData sceneData, DBWrapper wrapper, string configPath ,int mode)
        {
            switch (mode)
            {
                case Constant.MODE_C:
                    return GeneratedC_SceneData(sceneData, wrapper,configPath);
                case Constant.MODE_M:
                    return GeneratedM_SceneData(sceneData, wrapper,configPath);
            }
            return false;
        }
        private static bool GeneratedC_SceneData(CSJ_SceneData sceneData, DBWrapper wrapper, string configPath)
        {
            string sceneFileName = "C" + (sceneData.SceneNo + 1) + ".bin";
            StreamReader reader;
            int sceneNo = sceneData.SceneNo;
            int channelCount = sceneData.ChannelCount;
            int micSensor = 0;
            int senseFreq = 0;
            int runTime = 0;
            int fileSize = 0;
            int flag = 0;
            int mainIndex = 0;
            float rate = 255;


            //TODO 配置文件头信息并且写入场景文件中
            using (reader = new StreamReader(configPath))
            {
                string lineStr = "";
                string strValue = "";
                int intValue = 0;
                while ((lineStr = reader.ReadLine()) != null)
                {
                    if (lineStr.Equals("[YM]"))
                    {
                        for (int i = 0; i < Constant.SCENECOUNTMAX; i++)
                        {
                            lineStr = reader.ReadLine();
                            if (lineStr.StartsWith(sceneData.SceneNo + "CK"))
                            {
                                strValue = lineStr.Split('=')[1];
                                int.TryParse(strValue, out intValue);
                                micSensor = intValue;
                            }
                            lineStr = reader.ReadLine();
                            if (lineStr.StartsWith(sceneData.SceneNo + "JG"))
                            {
                                strValue = lineStr.Split('=')[1];
                                int.TryParse(strValue, out intValue);
                                senseFreq = intValue;
                            }
                            lineStr = reader.ReadLine();
                            if (lineStr.StartsWith(sceneData.SceneNo + "ZX"))
                            {
                                strValue = lineStr.Split('=')[1];
                                int.TryParse(strValue, out intValue);
                                runTime = intValue;
                            }
                        }
                        break;
                    }
                }
            }
            List<byte> data = new List<byte>();
            //摇麦开关
            data.Add(Convert.ToByte(micSensor));
            //要嘛感应时间
            data.Add(Convert.ToByte(((senseFreq) * 60) & 0xFF));
            data.Add(Convert.ToByte((((senseFreq) * 60) >> 8) & 0xFF));
            //摇麦执行时间
            data.Add(Convert.ToByte((runTime) & 0xFF));
            data.Add(Convert.ToByte(((runTime) >> 8) & 0xFF));
            //通道总数
            data.Add(Convert.ToByte((channelCount) & 0xFF));
            data.Add(Convert.ToByte(((channelCount) >> 8) & 0xFF));
            //写入文件
            FileUtils.Write(data.ToArray(), sceneFileName, true, false);

            Dictionary<int, bool> channelDictionary = new Dictionary<int, bool>();
            //TODO WriteChannelData
            foreach (CSJ_ChannelData cSJ_ChannelData in sceneData.ChannelDatas)
            {
                flag = 0;
                CSJ_ChannelData currentChannelData = cSJ_ChannelData;
                if (null != wrapper.fineTuneList)
                {
                    foreach (DB_FineTune fineTune in wrapper.fineTuneList)
                    {

                        if (cSJ_ChannelData.ChannelNo == fineTune.FineTuneIndex)
                        {
                            flag = 2;
                            mainIndex = fineTune.MainIndex;
                            rate = fineTune.MaxValue;
                            if (0 == rate)
                            {
                                rate = 255;
                            }
                            break;
                        }
                    }
                    if (2 == flag)
                    {
                        foreach (CSJ_ChannelData item in sceneData.ChannelDatas)
                        {
                            if (mainIndex == item.ChannelNo)
                            {
                                currentChannelData = item;
                                break;
                            }
                        }
                    }
                }
                //修改文件写入系统，分多文件，一个通道一个文件，全部写完再合并为CSJ的场景工程文件
                channelDictionary.Add(cSJ_ChannelData.ChannelNo, false);
                CSJThreadManager.QueueChannelUserWorkItem(new WaitCallback(ConvertC_DataWaitCallback), new ChannelThreadDataInfo(currentChannelData,flag,sceneNo,rate), 2000);
            }
            C_DMXSceneDictionary.Add(sceneData.SceneNo,channelDictionary);
            return false;
        }
        private static void ConvertC_DataWaitCallback(Object obj)
        {
            float value = 0;
            int intValue = 0;

            ChannelThreadDataInfo dataInfo = (ChannelThreadDataInfo)obj;
            CSJ_ChannelData channelData =dataInfo.ChannelData;
            Console.WriteLine("开始计算通道" + dataInfo.ChannelData.ChannelNo + "的数据");
            int flag = dataInfo.Flag;
            int sceneNo = dataInfo.SceneNo;
            float rate = dataInfo.Rate;
            string fileName = "C" + (sceneNo + 1) + "-" + channelData.ChannelNo + ".bin";
            //生成数据并写入文件
            int stepTime;
            int isGradualChange;
            int stepValue;
            float inc = 0;
            List<byte> WriteBuffer = new List<byte>();
            int startValue = channelData.StepValues[0];
            Console.WriteLine(fileName);
            FileUtils.Write((flag == 2) ? Convert.ToByte(0) : Convert.ToByte(startValue), fileName, true, true);
            for (int step = 1; step < channelData.StepCount + 1; step++)
            {
                stepValue = (step == channelData.StepCount) ? channelData.StepValues[0] : channelData.StepValues[step];
                stepTime = (step == channelData.StepCount) ? channelData.StepTimes[0] : channelData.StepTimes[step];
                isGradualChange = (step == channelData.StepCount) ? channelData.IsGradualChange[0] : channelData.IsGradualChange[step];
                inc = (stepValue - startValue) / (float)stepTime;
                for (int fram = 0; fram < stepTime; fram++)
                {
                    if (step == channelData.StepCount && fram == stepTime - 1)
                    {
                        break;
                    }
                    else
                    {
                        if (isGradualChange == Constant.MODE_C_GRADUAL)
                        {
                            value = startValue + inc * (fram + 1);
                            intValue = (int)Math.Floor(value * 256);
                            if (flag == 2)
                            {
                                intValue = (int)((intValue & 0xFF) / (255.0 / rate));
                            }
                            else
                            {
                                intValue = (int)((intValue >> 8) & 0xFF);
                            }
                            WriteBuffer.Add(Convert.ToByte(intValue));
                        }
                        else
                        {
                            if (2 == flag)
                            {
                                WriteBuffer.Add(Convert.ToByte(0));
                            }
                            else
                            {
                                WriteBuffer.Add(Convert.ToByte(stepValue));
                            }
                        }
                        if (WriteBufferSize == WriteBuffer.Count || (step == channelData.StepCount && fram == stepTime - 2))
                        {
                            FileUtils.Write(WriteBuffer.ToArray(), fileName, false, true);
                            WriteBuffer.Clear();
                        }
                    }
                }
                startValue = stepValue;
            }
            //通道数据写入完成执行方法
            DataCacheWriteCompleted(sceneNo,channelData.ChannelNo);
        }
        private static void DataCacheWriteCompleted(int sceneNo,int channelNo)
        {
            Console.WriteLine("场景" + (sceneNo + 1) + "-通道" + channelNo + "数据计算完成");
        }
        private static bool GeneratedM_SceneData(CSJ_SceneData sceneData, DBWrapper wrapper, string configPath)
        {
            string fileName = "M" + (sceneData.SceneNo + 1) + ".bin";
            //TODO WriteHeadData

            //TODO WriteChannelData
            return false;
        }
        public static void SaveProjectFile(DBWrapper wrapper,string configPath)
        {
            Thread thread = new Thread(Test) { IsBackground = true};
            thread.Start(new DBData(wrapper, configPath));
        }

        private static void Test(Object obj)

        {
            DBData data = obj as DBData;
            DBWrapper wrapper = data.Wrapper;
            string configPath = data.ConfigPath;
            List<int> sceneNos = new List<int>();
            foreach (DB_StepCount item in wrapper.stepCountList)
            {
                if (!sceneNos.Contains(item.PK.Frame))
                {
                    sceneNos.Add(item.PK.Frame);
                }
            }
            foreach (int sceneNo in sceneNos)
            {
                MainThreadState = false;
                CSJThreadManager.QueueSceneUserWorkItem(new WaitCallback(GetSceneDataWaitCallback), new SceneThreadDataInfo(sceneNo, wrapper, Constant.MODE_C, configPath), 2000);
                //CSJThreadManager.QueueSceneUserWorkItem(new WaitCallback(GetSceneDataWaitCallback), new SceneThreadDataInfo(sceneNo, wrapper, Constant.MODE_M, configPath), 2000);
                C_SceneChannelDatas.Add(sceneNo, new List<CSJ_ChannelData>());
                C_SceneStateDictionary.Add(sceneNo, new Dictionary<int, bool>());
                while (!MainThreadState)
                {
                    Thread.Sleep(1000);
                }
            }
        }
        private static void GetSceneDataWaitCallback(Object obj)
        {
            SceneThreadDataInfo dataInfo = (SceneThreadDataInfo)obj;
            int sceneNo = dataInfo.SceneNo;
            int mode = dataInfo.Mode;
            DBWrapper wrapper = dataInfo.Wrapper;
            string configPath = dataInfo.ConfigPath;
            foreach (DB_Light light in wrapper.lightList)
            {
                for (int i = 0; i < light.Count; i++)
                {
                    if (light.StartID + i > 512)
                    {
                        break;
                    }
                    else
                    {
                        C_SceneStateDictionary[sceneNo].Add(light.StartID + i, false);
                        //↓开启线程处理各个通道数据↓
                        CSJThreadManager.QueueChannelUserWorkItem(new WaitCallback(SceneChannelDataConvert), new SceneChannelThreadDataInfo(light.StartID + i, dataInfo,light), 2000);
                        //↑开启线程处理各个通道数据↑
                    }
                }
            }
        }
        private static void SceneChannelDataConvert(Object obj)
        {
            SceneChannelThreadDataInfo dataInfo = obj as SceneChannelThreadDataInfo;
            DBWrapper wrapper = dataInfo.SceneThreadData.Wrapper;
            Console.WriteLine("开启线程" + Thread.CurrentThread.ManagedThreadId + "处理场景" + dataInfo.SceneThreadData.SceneNo + "通道" +dataInfo.ChannelNo + "数据");
            int sceneNo = dataInfo.SceneThreadData.SceneNo;
            int mode = dataInfo.SceneThreadData.Mode;
            DB_Light light = dataInfo.Light;
            List<int> isGradualChange = new List<int>();
            List<int> stepTimes = new List<int>();
            List<int> stepValues = new List<int>();
            int stepCount = 0;
            int stepNumber = 0;
            foreach (DB_StepCount item in wrapper.stepCountList)
            {
                if (light.LightNo == item.PK.LightIndex && sceneNo == item.PK.Frame && mode == item.PK.Mode)
                {
                    stepCount = item.StepCount;
                }
            }
            for (int step = 0; step < stepCount; step++)
            {
                foreach (DB_Value value in wrapper.valueList)
                {
                    if (light.LightNo == value.PK.LightIndex && mode == value.PK.Mode && (step + 1) == value.PK.Step && sceneNo == value.PK.Frame && value.PK.LightID == dataInfo.ChannelNo)
                    {
                        if (mode == Constant.MODE_C)
                        {
                            if (value.ChangeMode != Constant.MODE_C_HIDDEN)
                            {
                                isGradualChange.Add(value.ChangeMode);
                                stepTimes.Add(value.StepTime);
                                stepValues.Add(value.ScrollValue);
                                stepNumber++;
                            }
                        }
                        else
                        {
                            if (value.ChangeMode == Constant.MODE_M_JUMP)
                            {
                                isGradualChange.Add(value.ChangeMode);
                                stepTimes.Add(value.StepTime);
                                stepValues.Add(value.ScrollValue);
                                stepNumber++;
                            }
                        }
                    }
                }
            }
            CSJ_ChannelData channelData = new CSJ_ChannelData()
            {
                ChannelNo = dataInfo.ChannelNo,
                StepCount = stepNumber,
                IsGradualChange = isGradualChange,
                StepTimes = stepTimes,
                StepValues = stepValues
            };
            if (channelData.StepCount > 0)
            {
                SceneChannelDataGenerationSuccessful(sceneNo, channelData.ChannelNo, channelData, dataInfo);
            }
        }
        private static void SceneChannelDataGenerationSuccessful(int sceneNo,int channelNo,CSJ_ChannelData channelData, SceneChannelThreadDataInfo dataInfo)
        {
            if (channelData != null)
            {
                C_SceneChannelDatas[sceneNo].Add(channelData);
            }
            C_SceneStateDictionary[sceneNo][channelData.ChannelNo] = true;
            Console.WriteLine("-------场景" + (sceneNo + 1) + "通道" + channelNo + "数据处理完成");
            if (!C_SceneStateDictionary[sceneNo].ContainsValue(false))
            {
                CSJ_SceneData sceneData = new CSJ_SceneData()
                {
                    SceneNo = sceneNo,
                    ChannelCount = C_SceneChannelDatas[sceneNo].Count,
                    ChannelDatas = C_SceneChannelDatas[sceneNo]
                };
                Console.WriteLine("-------场景" + sceneNo + "数据处理完成");
                GeneratedSceneData(sceneData, dataInfo.SceneThreadData.Wrapper, dataInfo.SceneThreadData.ConfigPath, dataInfo.SceneThreadData.Mode);
                C_SceneChannelDatas.Remove(sceneNo);
                C_SceneStateDictionary.Remove(sceneNo);
                MainThreadState = true;
            }
        }
        private class ChannelThreadDataInfo
        {
            public CSJ_ChannelData ChannelData { get; set; }
            public int Flag { get; set; }
            public int SceneNo { get; set; }
            public float Rate { get; set; }

            public ChannelThreadDataInfo(CSJ_ChannelData channelData,int flag,int sceneNo,float rate)
            {
                this.ChannelData = channelData;
                this.Flag = flag;
                this.SceneNo = sceneNo;
                this.Rate = rate;
            }
        }
        private class SceneThreadDataInfo
        {
            public int SceneNo { get; set; }
            public DBWrapper Wrapper { get; set; }
            public int Mode { get; set; }
            public string ConfigPath { get; set; }
            public SceneThreadDataInfo(int sceneNo,DBWrapper wrapper,int mode,string configPath)
            {
                this.SceneNo = sceneNo;
                this.Wrapper = wrapper;
                this.Mode = mode;
                this.ConfigPath = configPath;
            }
        }
        private class SceneChannelThreadDataInfo
        {
            public int ChannelNo { get; set; }
            public DB_Light Light { get; set; }
            public SceneThreadDataInfo SceneThreadData { get; set; }
            public SceneChannelThreadDataInfo(int channelNo, SceneThreadDataInfo dataInfo, DB_Light light)
            {
                this.ChannelNo = channelNo;
                this.SceneThreadData = dataInfo;
                this.Light = light;
            }
        }
        private class DBData
        {
            public DBWrapper Wrapper { get; set; }
            public string ConfigPath { get; set; }
            public DBData(DBWrapper wrapper,string configPath)
            {
                this.Wrapper = wrapper;
                this.ConfigPath = configPath;
            }
        }
        private delegate void Completed();
        private delegate void Failed();
    }
}
