using DMX512;
using LightController.Ast;
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

        /// <summary>
        /// 未完工程
        /// </summary>
        /// <returns></returns>
        public ICSJFile GetHardware(string filePath)
        {
            return new CSJ_Hardware(filePath);
        }

        public ICSJFile GetHardware(byte[] fileBuff)
        {
            return new CSJ_Hardware(fileBuff);
        }

        public CSJ_Project GetCSJProjectFiles(DBWrapper wrapper,string configPath)
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
                        for (int i = 0; i < 24; i++)
                        {
                            lineStr = reader.ReadLine();
                            if (lineStr.StartsWith(sceneData.SceneNo + "CK"))
                            {
                                strValue = lineStr.Split('=')[1];
                                int.TryParse(strValue, out intValue);
                                file.MICSensor = intValue;
                            }
                            if (lineStr.StartsWith(sceneData.SceneNo + "JG"))
                            {
                                strValue = lineStr.Split('=')[1];
                                int.TryParse(strValue, out intValue);
                                file.SenseFreq = intValue;
                            }
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
                ChannelData channelData = new ChannelData()
                {
                    ChannelNo = item.ChannelNo
                };
                List<int> datas = new List<int>();
                int startValue = item.StepValues[0];
                datas.Add(item.StepValues[0]);
                for (int step = 1; step < item.StepCount + 1; step++)
                {
                    int stepTime;
                    int stepValue;
                    int isGradualChange;
                    if (step == (item.StepCount))
                    {
                        stepTime = item.StepTimes[0];
                        stepValue = item.StepValues[0];
                        isGradualChange = item.IsGradualChange[0];
                    }
                    else
                    {
                        stepTime = item.StepTimes[step];
                        stepValue = item.StepValues[step];
                        isGradualChange = item.IsGradualChange[step];
                    }
                    if (isGradualChange == Constant.MODE_GRADUAL)
                    {
                        double inc = (stepValue - startValue) / (stepTime * 1.0);
                        for (int fram = 0; fram < stepTime; fram++)
                        {
                            if (step == item.StepCount && fram == (stepTime - 1))
                            {
                                break;
                            }
                            datas.Add((int)Math.Floor(startValue + inc * (fram + 1)));
                        }
                    }
                    else
                    {
                        for (int fram = 0; fram < stepTime; fram++)
                        {
                            if (step == item.StepCount && fram == (stepTime - 1))
                            {
                                break;
                            }
                            datas.Add(stepValue);
                        }
                    }
                    startValue = stepValue;
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
            List<ChannelData> channelDatas = new List<ChannelData>();
            file = new CSJ_M()
            {
                SceneNo = sceneData.SceneNo,
                ChannelCount = sceneData.ChannelCount,
            };
            /**************配置音频步时间(占位)**************/
            file.FrameTime = sceneData.ChannelDatas[0].StepTimes[0];
            /**************************************************/

            /**************配置音频叠加后间隔时间(占位)**************/
            file.MusicIntervalTime = 1000;
            /**************************************************/

            /**************配置音频步数列表(模拟)**************/
            List<int> stepList = new List<int>();
            stepList.Add(2);
            stepList.Add(4);
            file.StepList = stepList;
            file.StepListCount = stepList.Count;
            /**************************************************/
            foreach (CSJ_ChannelData item in sceneData.ChannelDatas)
            {
                ChannelData channelData = new ChannelData()
                {
                    ChannelNo = item.ChannelNo,
                };
                channelData.Datas = item.StepValues.ToList();
                channelData.DataSize = channelData.Datas.Count;
                channelDatas.Add(channelData);
            }
            file.ChannelDatas = channelDatas;
            return file;
        }

        private List<int> GetSceneNos()
        {
            List<int> sceneNos = new List<int>();
            for (int i = 0; i < Constant.SCENECOUNT; i++)
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

        private CSJ_SceneData GetSceneData(int sceneNo,int mode)
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
                    CSJ_ChannelData channelData = GetChannelData(sceneNo, light.StartID + i, mode,light);
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

        private CSJ_ChannelData GetChannelData(int sceneNo,int channelNo,int mode,DB_Light light)
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
                        if (value.ChangeMode != Constant.HIDDEN)
                        {
                            if (mode == Constant.MODE_M)
                            {
                                if (value.ChangeMode == Constant.MUSIC_CONTROL_OFF)
                                {
                                    continue;
                                }
                            }
                            isGradualChange.Add(value.ChangeMode);
                            stepTimes.Add(value.StepTime);
                            stepValues.Add(value.ScrollValue);
                            stepCount++;
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
      
        private int GetStepCount(int lightNo,int sceneNo,int mode)
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
