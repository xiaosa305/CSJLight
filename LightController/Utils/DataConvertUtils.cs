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
        private static Dictionary<int, Dictionary<int, bool>> SceneDictionary = new Dictionary<int, Dictionary<int, bool>>();
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
                CSJThreadManager.QueueUserWorkItem(new WaitCallback(ConvertC_Data), new ChannelThreadDataInfo(currentChannelData,flag,sceneNo,rate), 2000);
            }
            SceneDictionary.Add(sceneData.SceneNo,channelDictionary);
            return false;
        }

        private static void ConvertC_Data(Object obj)
        {
            float value = 0;
            int intValue = 0;

            ChannelThreadDataInfo dataInfo = (ChannelThreadDataInfo)obj;
            CSJ_ChannelData channelData =dataInfo.ChannelData;
            int flag = dataInfo.Flag;
            int sceneNo = dataInfo.SceneNo;
            float rate = dataInfo.Rate;
            string fileName = "C" + (sceneNo + 1) + "-" + channelData.ChannelNo + ".bin";
            //生成数据并写入文件
            int stepTime;
            int isGradualChange;
            int stepValue;
            float inc = 0;
            int startValue = channelData.StepValues[0];
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
                            FileUtils.Write(Convert.ToByte(intValue), fileName, false, true);
                        }
                        else
                        {
                            if (2 == flag)
                            {
                                FileUtils.Write(Convert.ToByte(0), fileName, false, true);
                            }
                            else
                            {
                                FileUtils.Write(Convert.ToByte(stepValue), fileName, false, true);
                            }
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
            Console.WriteLine("场景" + (sceneNo + 1) + "-通道" + channelNo + "完成");
        }

        private static bool GeneratedM_SceneData(CSJ_SceneData sceneData, DBWrapper wrapper, string configPath)
        {
            string fileName = "M" + (sceneData.SceneNo + 1) + ".bin";
            //TODO WriteHeadData

            //TODO WriteChannelData
            return false;
        }
        private class ChannelThreadDataInfo
        {
            public CSJ_ChannelData ChannelData { get; set; }
            public int Flag { get; set; }
            public int SceneNo { get; set; }
            public float Rate { get; set; }
            //public Completed Completed { get; set; }
            //public Failed Failed { get; set; }

            public ChannelThreadDataInfo(CSJ_ChannelData channelData,int flag,int sceneNo,float rate)
            {
                this.ChannelData = channelData;
                this.Flag = flag;
                this.SceneNo = sceneNo;
                this.Rate = rate;
            }
        }
        private delegate void Completed();
        private delegate void Failed();
    }
}
