using DMX512;
using LightController.Ast;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
    public class DmxDataConvert
    {
        private static DmxDataConvert Instance { get; set; }
        private DBWrapper Wrapper { get; set; }
        private string ConfigPath { get; set; }
        private DmxDataConvert()
        {
        }
        public static DmxDataConvert GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DmxDataConvert();
            }
            return Instance;
        }
        public ICSJFile GetHardware(string filePath)
        {
            return new CSJ_Hardware(filePath);
        }
        public ICSJFile GetHardware(byte[] fileBuff)
        {
            CSJ_Hardware file = new CSJ_Hardware();
            try
            {
                file = new CSJ_Hardware(fileBuff);
            }
            catch (Exception)
            {
                CSJLogs.GetInstance().DebugLog("设备未初始化");
            }
            return file;
        }
        public CSJ_Project GetCSJProjectFiles(DBWrapper wrapper, string configPath)
        {
            ConfigPath = configPath;
            Wrapper = wrapper;
            CSJ_Project project = new CSJ_Project()
            {
                ConfigFile = new CSJ_Config(wrapper, configPath),
                CFiles = null,
                MFiles = null
            };
            project.CFiles = GetCFiles();
            project.MFiles = GetMFiles();
            return project;
        }
        private List<ICSJFile> GetCFiles()
        {
            List<ICSJFile> cs = new List<ICSJFile>();
            List<int> sceneNos = GetSceneNos();
            foreach (int sceneNo in sceneNos)
            {
                ICSJFile file = GetCSJFile(sceneNo, Constant.MODE_C);
                if (file != null)
                {
                    cs.Add(file);
                }
            }
            if (cs.Count > 0)
            {
                return cs;
            }
            return null;
        }
        private List<ICSJFile> GetMFiles()
        {
            List<ICSJFile> ms = new List<ICSJFile>();
            List<int> sceneNos = GetSceneNos();
            foreach (int sceneNo in sceneNos)
            {
                ICSJFile file = GetCSJFile(sceneNo, Constant.MODE_M);
                if (file != null)
                {
                    ms.Add(file);
                }
            }
            if (ms.Count > 0)
            {
                return ms;
            }
            return null;
        }
        private ICSJFile GetCSJFile(int sceneNo, int mode)
        {
            ICSJFile file = null;
            CSJ_SceneData sceneData = GetSceneData(sceneNo, mode);
            if (sceneData != null)
            {
                switch (mode)
                {
                    case Constant.MODE_C:
                        file = GetCSJCFile(sceneData);
                        break;
                    case Constant.MODE_M:
                        file = GetCSJMFile(sceneData);
                        break;
                    default:
                        file = null;
                        break;
                }
                return file;
            }
            return null;
        }
        private ICSJFile GetCSJCFile(CSJ_SceneData sceneData)
        {
            //---Test---

            //DataConvertUtils.GeneratedSceneData(sceneData, Wrapper, ConfigPath, Constant.MODE_C);
            //return null;
            //---Test---
            CSJ_C file = null;
            StreamReader reader;
            List<ChannelData> channelDatas = new List<ChannelData>();
            file = new CSJ_C()
            {
                SceneNo = sceneData.SceneNo,
                ChannelCount = sceneData.ChannelCount,
            };
            using (reader = new StreamReader(ConfigPath))
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
                                file.MICSensor = intValue;
                            }
                            lineStr = reader.ReadLine();
                            if (lineStr.StartsWith(sceneData.SceneNo + "JG"))
                            {
                                strValue = lineStr.Split('=')[1];
                                int.TryParse(strValue, out intValue);
                                file.SenseFreq = intValue;
                            }
                            lineStr = reader.ReadLine();
                            if (lineStr.StartsWith(sceneData.SceneNo + "ZX"))
                            {
                                strValue = lineStr.Split('=')[1];
                                int.TryParse(strValue, out intValue);
                                file.RunTime = intValue;
                            }
                        }
                    }
                }
            }
            foreach (CSJ_ChannelData item in sceneData.ChannelDatas)
            {
                int flag = 0;
                int mainIndex = 0;
                List<int> datas = new List<int>();
                int stepValue;
                int stepTime;
                int isGradualChange;
                int startValue;
                int rate = 255;
                ChannelData channelData = new ChannelData()
                {
                    ChannelNo = item.ChannelNo
                };
                if (null != Wrapper.fineTuneList)
                {
                    foreach (DB_FineTune fineTune in Wrapper.fineTuneList)
                    {
                        if (fineTune.MainIndex == channelData.ChannelNo)
                        {
                            flag = 1;
                        }
                        else if (fineTune.FineTuneIndex == channelData.ChannelNo)
                        {
                            flag = 2;
                            rate = fineTune.MaxValue;
                            if (rate == 0)
                            {
                                rate = 255;
                            }
                        }
                    }
                }
                if (2 == flag)
                {
                    foreach (DB_FineTune fineTune in Wrapper.fineTuneList)
                    {
                        if (fineTune.FineTuneIndex == channelData.ChannelNo)
                        {
                            mainIndex = fineTune.MainIndex;
                        }
                    }
                    if (mainIndex != 0)
                    {
                        foreach (CSJ_ChannelData cSJ_Channel in sceneData.ChannelDatas)
                        {
                            if (mainIndex == cSJ_Channel.ChannelNo)
                            {
                                datas.Add(0);
                                startValue = cSJ_Channel.StepValues[0];
                                for (int step = 1; step < cSJ_Channel.StepCount + 1; step++)
                                {
                                    if (step == cSJ_Channel.StepCount)
                                    {
                                        stepValue = cSJ_Channel.StepValues[0];
                                        stepTime = cSJ_Channel.StepTimes[0];
                                        isGradualChange = cSJ_Channel.IsGradualChange[0];
                                    }
                                    else
                                    {
                                        stepValue = cSJ_Channel.StepValues[step];
                                        stepTime = cSJ_Channel.StepTimes[step];
                                        isGradualChange = cSJ_Channel.IsGradualChange[step];
                                    }
                                    float inc = (stepValue - startValue) / (float)stepTime;
                                    for (int fram = 0; fram < stepTime; fram++)
                                    {
                                        if (step == cSJ_Channel.StepCount && fram == stepTime - 1)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (isGradualChange == Constant.MODE_C_GRADUAL)
                                            {
                                                float value = startValue + inc * (fram + 1);
                                                int intValue = (int)Math.Floor(value * 256);
                                                intValue = (int)((intValue & 0xFF) / (255.0 / rate));
                                                datas.Add(intValue);
                                            }
                                            else
                                            {
                                                datas.Add(0);
                                            }
                                        }
                                    }
                                    startValue = stepValue;
                                }
                            }
                        }
                    }
                }
                else
                {
                    startValue = item.StepValues[0];
                    datas.Add(startValue);
                    for (int step = 1; step < item.StepCount + 1; step++)
                    {
                        if (step == item.StepCount)
                        {
                            stepValue = item.StepValues[0];
                            stepTime = item.StepTimes[0];
                            isGradualChange = item.IsGradualChange[0];
                        }
                        else
                        {
                            stepValue = item.StepValues[step];
                            stepTime = item.StepTimes[step];
                            isGradualChange = item.IsGradualChange[step];
                        }
                        float inc = (stepValue - startValue) / (float)stepTime;
                        for (int fram = 0; fram < stepTime; fram++)
                        {
                            if (step == item.StepCount && fram == stepTime - 1)
                            {
                                break;
                            }
                            else
                            {
                                if (isGradualChange == Constant.MODE_C_GRADUAL)
                                {
                                    float value = startValue + inc * (fram + 1);
                                    int intValue = (int)Math.Floor(value * 256);
                                    datas.Add((intValue >> 8) & 0xFF);
                                }
                                else
                                {
                                    datas.Add(stepValue);
                                }
                            }
                        }
                        startValue = stepValue;
                    }
                }
                channelData.Datas = datas;
                channelData.DataSize = channelData.Datas.Count;
                channelDatas.Add(channelData);
            }
            file.ChannelDatas = channelDatas;
            return file;
        }
        private ICSJFile GetCSJMFile(CSJ_SceneData sceneData)
        {
            CSJ_M file = null;
            StreamReader reader;
            List<int> stepList = new List<int>();
            List<ChannelData> channelDatas = new List<ChannelData>();
            file = new CSJ_M()
            {
                SceneNo = sceneData.SceneNo,
                ChannelCount = sceneData.ChannelCount,
            };
            using (reader = new StreamReader(ConfigPath))
            {
                string lineStr;
                string strValue = string.Empty;
                int intValue;
                while (true)
                {
                    lineStr = reader.ReadLine();
                    if (lineStr.Equals("[SK]"))
                    {
                        for (int i = 0; i < Constant.SCENECOUNTMAX; i++)
                        {
                            lineStr = reader.ReadLine();
                            string sceneNo = "";
                            if (lineStr.Split('=')[0].Length > 3)
                            {
                                sceneNo = lineStr[0].ToString() + lineStr[1].ToString();
                            }
                            else
                            {
                                sceneNo = lineStr[0].ToString();
                            }
                            if (sceneNo.Equals(sceneData.SceneNo.ToString()))
                            {
                                strValue = lineStr.Split('=')[1];
                                for (int strIndex = 0; strIndex < strValue.Length; strIndex++)
                                {
                                    intValue = int.Parse(strValue[strIndex].ToString());
                                    if (intValue != 0)
                                    {
                                        stepList.Add(intValue);
                                    }
                                }
                                lineStr = reader.ReadLine();
                                strValue = lineStr.Split('=')[1];
                                intValue = int.Parse(strValue.ToString());
                                file.FrameTime = intValue;
                                lineStr = reader.ReadLine();
                                strValue = lineStr.Split('=')[1];
                                intValue = int.Parse(strValue.ToString());
                                file.MusicIntervalTime = intValue;
                            }
                            else
                            {
                                lineStr = reader.ReadLine();
                                lineStr = reader.ReadLine();
                            }
                        }
                        break;
                    }
                }
            }
            file.StepList = stepList;
            file.StepListCount = file.StepList.Count;
            foreach (CSJ_ChannelData item in sceneData.ChannelDatas)
            {
                int flag = 0;
                int mainIndex = 0;
                List<int> datas = new List<int>();
                int stepValue;
                int isGradualChange;
                int startValue;
                int rate = 255;
                ChannelData channelData = new ChannelData()
                {
                    ChannelNo = item.ChannelNo
                };
                if (null != Wrapper.fineTuneList)
                {
                    foreach (DB_FineTune fineTune in Wrapper.fineTuneList)
                    {
                        if (fineTune.MainIndex == channelData.ChannelNo)
                        {
                            flag = 1;
                        }
                        else if (fineTune.FineTuneIndex == channelData.ChannelNo)
                        {
                            flag = 2;
                            rate = fineTune.MaxValue;
                            if (rate == 0)
                            {
                                rate = 255;
                            }
                        }
                    }
                }
                if (2 == flag)
                {
                    foreach (DB_FineTune fineTune in Wrapper.fineTuneList)
                    {
                        if (fineTune.FineTuneIndex == channelData.ChannelNo)
                        {
                            mainIndex = fineTune.MainIndex;
                        }
                    }
                    if (mainIndex != 0)
                    {
                        foreach (CSJ_ChannelData cSJ_Channel in sceneData.ChannelDatas)
                        {
                            if (mainIndex == cSJ_Channel.ChannelNo)
                            {
                                datas.Add(0);
                                startValue = cSJ_Channel.StepValues[0];
                                for (int step = 1; step < cSJ_Channel.StepCount + 1; step++)
                                {
                                    if (step == cSJ_Channel.StepCount)
                                    {
                                        stepValue = cSJ_Channel.StepValues[0];
                                        isGradualChange = cSJ_Channel.IsGradualChange[0];
                                    }
                                    else
                                    {
                                        stepValue = cSJ_Channel.StepValues[step];
                                        isGradualChange = cSJ_Channel.IsGradualChange[step];
                                    }
                                    for (int fram = 0; fram < file.FrameTime; fram++)
                                    {
                                        if (step == cSJ_Channel.StepCount && fram == file.FrameTime - 1)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (isGradualChange == Constant.MODE_M_GRADUAL)
                                            {
                                                float inc = (stepValue - startValue) / (float)file.FrameTime;
                                                float value = startValue + inc * (fram + 1);
                                                int intValue = (int)Math.Floor(value * 256);
                                                if (rate == 1)
                                                {
                                                    intValue = (int)((intValue & 0xFF) / (255.0 / rate));
                                                    datas.Add(intValue);
                                                }
                                                else
                                                {
                                                    datas.Add(intValue & 0xFF);

                                                }
                                            }
                                            else
                                            {
                                                if (step == cSJ_Channel.StepCount)
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    datas.Add(0);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    startValue = stepValue;
                                }
                            }
                        }
                    }
                }
                else
                {
                    startValue = item.StepValues[0];
                    datas.Add(startValue);
                    for (int step = 1; step < item.StepCount + 1; step++)
                    {
                        if (step == item.StepCount)
                        {
                            stepValue = item.StepValues[0];
                            isGradualChange = item.IsGradualChange[0];
                        }
                        else
                        {
                            stepValue = item.StepValues[step];
                            isGradualChange = item.IsGradualChange[step];
                        }
                        for (int fram = 0; fram < file.FrameTime; fram++)
                        {
                            if (step == item.StepCount && fram == file.FrameTime - 1)
                            {
                                break;
                            }
                            else
                            {
                                if (isGradualChange == Constant.MODE_M_GRADUAL)
                                {
                                    float inc = (stepValue - startValue) / (float)file.FrameTime;
                                    float value = startValue + inc * (fram + 1);
                                    int intValue = (int)Math.Floor(value * 256);
                                    datas.Add((intValue >> 8) & 0xFF);
                                }
                                else
                                {
                                    if (step == item.StepCount)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        datas.Add(stepValue);
                                        break;
                                    }
                                }
                            }
                        }
                        startValue = stepValue;
                    }
                }
                channelData.Datas = datas;
                channelData.DataSize = channelData.Datas.Count;
                channelDatas.Add(channelData);
            }
            file.ChannelDatas = channelDatas;
            return file;
        }
        private List<int> GetSceneNos()
        {
            List<int> sceneNos = new List<int>();
            for (int i = 0; i < Constant.SCENECOUNTMAX; i++)
            {
                foreach (DB_StepCount item in Wrapper.stepCountList)
                {
                    if (item.PK.Frame == i)
                    {
                        sceneNos.Add(i);
                        break;
                    }
                }
            }
            return sceneNos;
        }
        private CSJ_SceneData GetSceneData(int sceneNo, int mode)
        {
            CSJ_SceneData sceneData = new CSJ_SceneData()
            {
                SceneNo = sceneNo,
            };
            List<CSJ_ChannelData> channelDatas = new List<CSJ_ChannelData>();
            foreach (DB_Light light in Wrapper.lightList)
            {
                for (int i = 0; i < light.Count; i++)
                {
                    CSJ_ChannelData channelData = GetChannelData(sceneNo, light.StartID + i, mode, light);
                    if (channelData != null)
                    {
                        channelDatas.Add(channelData);
                    }
                }
            }
            sceneData.ChannelDatas = channelDatas;
            sceneData.ChannelCount = channelDatas.Count;
            if (sceneData.ChannelCount > 0)
            {
                return sceneData;
            }
            return null;
        }
        private CSJ_ChannelData GetChannelData(int sceneNo, int channelNo, int mode, DB_Light light)
        {
            if (channelNo > 512)
            {
                return null;
            }
            List<int> isGradualChange = new List<int>();
            List<int> stepTimes = new List<int>();
            List<int> stepValues = new List<int>();
            int stepCount = 0;
            for (int step = 0; step < GetStepCount(light.LightNo, sceneNo, mode); step++)
            {
                foreach (DB_Value value in Wrapper.valueList)
                {
                    if (light.LightNo == value.PK.LightIndex && mode == value.PK.Mode && (step + 1) == value.PK.Step && sceneNo == value.PK.Frame && value.PK.LightID == channelNo)
                    {
                        if (mode == Constant.MODE_C)
                        {
                            if (value.ChangeMode != Constant.MODE_C_HIDDEN)
                            {
                                isGradualChange.Add(value.ChangeMode);
                                stepTimes.Add(value.StepTime);
                                stepValues.Add(value.ScrollValue);
                                stepCount++;
                            }
                        }
                        else
                        {
                            if (value.ChangeMode == Constant.MODE_M_JUMP)
                            {
                                isGradualChange.Add(value.ChangeMode);
                                stepTimes.Add(value.StepTime);
                                stepValues.Add(value.ScrollValue);
                                stepCount++;
                            }
                        }
                    }
                }
            }
            CSJ_ChannelData channelData = new CSJ_ChannelData()
            {
                ChannelNo = channelNo,
                StepCount = stepCount,
                IsGradualChange = isGradualChange,
                StepTimes = stepTimes,
                StepValues = stepValues
            };
            if (channelData.StepCount > 0)
            {
                return channelData;
            }
            return null;
        }
        private int GetStepCount(int lightNo, int sceneNo, int mode)
        {
            int stepCount = 0;
            foreach (DB_StepCount item in Wrapper.stepCountList)
            {
                if (lightNo == item.PK.LightIndex && sceneNo == item.PK.Frame && mode == item.PK.Mode)
                {
                    stepCount = item.StepCount;
                }
            }
            return stepCount;
        }
    }

}
