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
        private static Dictionary<int, Dictionary<int, bool>> C_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
        private static Dictionary<int, bool> C_DMXSceneState = new Dictionary<int, bool>();
        private static Dictionary<int, Dictionary<int, bool>> M_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
        private static Dictionary<int, bool> M_DMXSceneState = new Dictionary<int, bool>();
        private const int WriteBufferSize = 1024 * 50;
        public static bool GeneratedSceneData(CSJ_SceneData sceneData, DBWrapper wrapper, string configPath, int mode)
        {
            switch (mode)
            {
                case Constant.MODE_C:
                    return GeneratedC_SceneData(sceneData, wrapper, configPath);
                case Constant.MODE_M:
                    return GeneratedM_SceneData(sceneData, wrapper, configPath);
            }
            return false;
        }
        public static void GeneratedSceneData(Object obj)
        {
            SceneDBData data = obj as SceneDBData;
            int mode = data.Mode;
            switch (mode)
            {
                case Constant.MODE_C:
                    GeneratedC_SceneData(data.SceneData, data.Wrapper, data.ConfigPath);
                    break;
                case Constant.MODE_M:
                    GeneratedM_SceneData(data.SceneData, data.Wrapper, data.ConfigPath);
                    break;
            }
        }
        private static bool GeneratedC_SceneData(CSJ_SceneData sceneData, DBWrapper wrapper, string configPath)
        {
            string sceneFileName = "C" + (sceneData.SceneNo + 1) + ".bin";
            StreamReader reader;
            int sceneNo = Constant.GetNumber(sceneData.SceneNo);
            int channelCount = sceneData.ChannelCount;
            int micSensor = 0;
            int senseFreq = 0;
            int runTime = 0;
            int flag = 0;
            int mainIndex = 0;
            float rate = 255;
            bool result = true;
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
            data.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            data.Add(Convert.ToByte(micSensor));
            data.Add(Convert.ToByte(((senseFreq) * 60) & 0xFF));
            data.Add(Convert.ToByte((((senseFreq) * 60) >> 8) & 0xFF));
            data.Add(Convert.ToByte((runTime) & 0xFF));
            data.Add(Convert.ToByte(((runTime) >> 8) & 0xFF));
            data.Add(Convert.ToByte((channelCount) & 0xFF));
            data.Add(Convert.ToByte(((channelCount) >> 8) & 0xFF));
            FileUtils.Write(data.ToArray(),data.Count, sceneFileName, true, false);
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
                Console.WriteLine("启动线程开始计算场景" + (sceneNo + 1) + "通道" +Constant.GetNumber(cSJ_ChannelData.ChannelNo) + "数据");
                result = result & CSJThreadManager.QueueChannelUserWorkItem(new WaitCallback(ConvertC_DataWaitCallback), new C_ChannelThreadDataInfo(currentChannelData, Constant.GetNumber(cSJ_ChannelData.ChannelNo), flag, sceneNo, rate), 200000);
            }
            return result;
        }
        private static void ConvertC_DataWaitCallback(Object obj)
        {
            float value = 0;
            int intValue = 0;

            C_ChannelThreadDataInfo dataInfo = (C_ChannelThreadDataInfo)obj;
            CSJ_ChannelData channelData = dataInfo.ChannelData;
            int flag = dataInfo.Flag;
            int sceneNo = Constant.GetNumber(dataInfo.SceneNo);
            float rate = dataInfo.Rate;
            string fileName = "C" + Constant.GetNumber(sceneNo + 1) + "-" + Constant.GetNumber(dataInfo.ChannelNo) + ".bin";
            int stepTime;
            int isGradualChange;
            int stepValue;
            float inc = 0;
            List<byte> WriteBuffer = new List<byte>();
            int startValue = channelData.StepValues[0];
            FileUtils.Write(((flag == 2) ? Convert.ToByte(0) : Convert.ToByte(startValue)), fileName, true, true);
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
                        FileUtils.Write(WriteBuffer.ToArray(),WriteBuffer.Count, fileName, false, true);
                        WriteBuffer.Clear();
                        break;
                    }
                    else
                    {
                        if (isGradualChange == Constant.MODE_C_GRADUAL)
                        {
                            value = startValue + inc * (fram + 1);
                            if (inc < 0)
                            {
                                value = value < 0 ? 0 : value;
                            }
                            else
                            {
                                value = value > 255 ? 255 : value;
                            }
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
                        if (WriteBufferSize == WriteBuffer.Count)
                        {
                            FileUtils.Write(WriteBuffer.ToArray(),WriteBuffer.Count, fileName, false, true);
                            WriteBuffer.Clear();
                        }
                    }
                }
                startValue = stepValue;
            }
            DataCacheWriteCompleted(sceneNo, dataInfo.ChannelNo,Constant.MODE_C);
        }
        private static void DataCacheWriteCompleted(int sceneNo, int channelNo,int mode)
        {
            switch (mode)
            {
                case Constant.MODE_C:
                    C_DMXSceneChannelData[sceneNo][channelNo] = true;
                    Console.WriteLine("场景" + (sceneNo + 1) + "-通道" + channelNo + "数据计算完成");
                    if (!C_DMXSceneChannelData[sceneNo].ContainsValue(false))
                    {
                        C_DMXSceneState[sceneNo] = true;
                        Console.WriteLine("场景" + (sceneNo + 1) + "数据计算完成并生成文件成功");
                        FileUtils.MergeFile(Constant.GetNumber(sceneNo), mode);
                    }
                    if (!C_DMXSceneState.ContainsValue(false))
                    {
                        Console.WriteLine("所有场景数据计算完成");
                        C_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
                        C_DMXSceneState = new Dictionary<int, bool>();
                    }
                    break;
                case Constant.MODE_M:
                    M_DMXSceneChannelData[sceneNo][channelNo] = true;
                    Console.WriteLine("场景" + (sceneNo + 1) + "-通道" + channelNo + "数据计算完成");
                    if (!M_DMXSceneChannelData[sceneNo].ContainsValue(false))
                    {
                        M_DMXSceneState[sceneNo] = true;
                        Console.WriteLine("场景" + (sceneNo + 1) + "数据计算完成并生成文件成功");
                        FileUtils.MergeFile(Constant.GetNumber(sceneNo), mode);
                    }
                    if (!M_DMXSceneState.ContainsValue(false))
                    {
                        Console.WriteLine("所有场景数据计算完成");
                        M_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
                        M_DMXSceneState = new Dictionary<int, bool>();
                    }
                    break;
            }
        }

        private static bool GeneratedM_SceneData(CSJ_SceneData sceneData, DBWrapper wrapper, string configPath)
        {
            string sceneFileName = "M" + (sceneData.SceneNo + 1) + ".bin";
            StreamReader reader;
            List<int> stepList = new List<int>();
            bool result = true;
            int channelCount = sceneData.ChannelCount;
            int sceneNo = Constant.GetNumber(sceneData.SceneNo);
            int frameTime = 0;
            int musicIntervalTime = 0;
            int mainIndex = 0;
            float rate = 255;
            int flag;
            using (reader = new StreamReader(configPath))
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
                            string sceneNumber = "";
                            if (lineStr.Split('=')[0].Length > 3)
                            {
                                sceneNumber = lineStr[0].ToString() + lineStr[1].ToString();
                            }
                            else
                            {
                                sceneNumber = lineStr[0].ToString();
                            }
                            if (sceneNumber.Equals(sceneData.SceneNo.ToString()))
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
                                frameTime = intValue;
                                lineStr = reader.ReadLine();
                                strValue = lineStr.Split('=')[1];
                                intValue = int.Parse(strValue.ToString());
                                musicIntervalTime = intValue;
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
            List<byte> data = new List<byte>();
            data.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            data.Add(Convert.ToByte(frameTime));
            data.Add(Convert.ToByte(musicIntervalTime & 0xFF));
            data.Add(Convert.ToByte((musicIntervalTime >> 8) & 0xFF));
            for (int i = 0; i < stepList.Count; i++)
            {
                data.Add(Convert.ToByte(stepList[i]));
            }
            for (int i = stepList.Count; i < Constant.STEPLISTMAX; i++)
            {
                data.Add(Convert.ToByte(0x00));
            }
            data.Add(Convert.ToByte(channelCount & 0xFF));
            data.Add(Convert.ToByte((channelCount >> 8 ) & 0xFF));
            FileUtils.Write(data.ToArray(), data.Count, sceneFileName, true, false);
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
                Console.WriteLine("启动线程开始计算场景" + (sceneNo + 1) + "通道" + Constant.GetNumber(cSJ_ChannelData.ChannelNo) + "数据");
                result = result & CSJThreadManager.QueueChannelUserWorkItem(new WaitCallback(ConvertM_DataWaitCallback), new M_ChannelThreadDataInfo(currentChannelData, Constant.GetNumber(cSJ_ChannelData.ChannelNo), flag, sceneNo, rate,frameTime), 200000);
            }
            return result;
        }
        private static void ConvertM_DataWaitCallback(Object obj)
        {
            M_ChannelThreadDataInfo dataInfo = obj as M_ChannelThreadDataInfo;
            CSJ_ChannelData channelData = dataInfo.ChannelData;
            float value = 0;
            int intValue = 0;
            int flag = dataInfo.Flag;
            int sceneNo = Constant.GetNumber(dataInfo.SceneNo);
            float rate = dataInfo.Rate;
            string fileName = "M" + Constant.GetNumber(sceneNo + 1) + "-" + Constant.GetNumber(dataInfo.ChannelNo) + ".bin";
            int stepTime;
            int isGradualChange;
            int stepValue;
            int framTime = dataInfo.FramTime;
            float inc = 0;
            List<byte> WriteBuffer = new List<byte>();
            int startValue = channelData.StepValues[0];
            FileUtils.Write(((flag == 2) ? Convert.ToByte(0) : Convert.ToByte(startValue)), fileName, true, true);
            for (int step = 1; step < channelData.StepCount + 1; step++)
            {
                stepValue = (step == channelData.StepCount) ? channelData.StepValues[0] : channelData.StepValues[step];
                stepTime = (step == channelData.StepCount) ? channelData.StepTimes[0] : channelData.StepTimes[step];
                isGradualChange = (step == channelData.StepCount) ? channelData.IsGradualChange[0] : channelData.IsGradualChange[step];
                inc = (stepValue - startValue) / (float)stepTime;
                for (int fram = 0; fram < framTime; fram++)
                {
                    if (step == channelData.StepCount && fram == stepTime - 1)
                    {
                        FileUtils.Write(WriteBuffer.ToArray(), WriteBuffer.Count, fileName, false, true);
                        WriteBuffer.Clear();
                        break;
                    }
                    else
                    {
                        if (isGradualChange == Constant.MODE_M_GRADUAL)
                        {
                            value = startValue + inc * (fram + 1);
                            if (inc < 0)
                            {
                                value = value < 0 ? 0 : value;
                            }
                            else
                            {
                                value = value > 255 ? 255 : value;
                            }
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
                            if (step == channelData.StepCount)
                            {
                                FileUtils.Write(WriteBuffer.ToArray(), WriteBuffer.Count, fileName, false, true);
                                WriteBuffer.Clear();
                                break;
                            }
                            else
                            {
                                WriteBuffer.Add(Convert.ToByte(stepValue));
                                FileUtils.Write(WriteBuffer.ToArray(), WriteBuffer.Count, fileName, false, true);
                                WriteBuffer.Clear();
                                break;
                            }
                        }
                    }
                }
                startValue = stepValue;
            }
            DataCacheWriteCompleted(Constant.GetNumber(sceneNo), Constant.GetNumber(dataInfo.ChannelNo), Constant.MODE_M);
        }

        public static void SaveProjectFile(DBWrapper wrapper, ValueDAO valueDAO, string configPath)
        {
            FileUtils.ClearCacheData();
            FileUtils.ClearProjectData();
            Thread thread = new Thread(GeneratedDBSceneData) { IsBackground = true };
            thread.Start(new DBData(wrapper, valueDAO, configPath));
        }
        private static void GeneratedDBSceneData(Object obj)
        {
            DBData data = obj as DBData;
            DBWrapper wrapper = data.Wrapper;
            string configPath = data.ConfigPath;
            ValueDAO valueDAO = data.ValueDAO;
            List<int> sceneNos = new List<int>();
            foreach (DB_StepCount item in data.Wrapper.stepCountList)
            {
                if (!sceneNos.Contains(item.PK.Frame))
                {
                    sceneNos.Add(item.PK.Frame);
                }
            }
            foreach (int sceneNo in sceneNos)
            {
                C_DMXSceneChannelData.Add(sceneNo, new Dictionary<int, bool>());
                C_DMXSceneState.Add(sceneNo, false);
                M_DMXSceneChannelData.Add(sceneNo, new Dictionary<int, bool>());
                M_DMXSceneState.Add(sceneNo, false);
                //GetSceneDataWaitCallback(new SceneThreadDataInfo(sceneNo, wrapper, valueDAO, Constant.MODE_C, configPath));
                //GetSceneDataWaitCallback(new SceneThreadDataInfo(sceneNo, wrapper, valueDAO, Constant.MODE_M, configPath));

                //------存在BUG，待修复
                CSJThreadManager.QueueSceneUserWorkItem(new WaitCallback(GetSceneDataWaitCallback), new SceneThreadDataInfo(Constant.GetNumber(sceneNo), wrapper, valueDAO, Constant.MODE_C, configPath), 200000);
                //CSJThreadManager.QueueSceneUserWorkItem(new WaitCallback(GetSceneDataWaitCallback), new SceneThreadDataInfo(Constant.GetNumber(sceneNo), wrapper, valueDAO, Constant.MODE_M, configPath), 200000);
            }
        }
        private static void GetSceneDataWaitCallback(Object obj)
        {
            SceneThreadDataInfo data = obj as SceneThreadDataInfo;
            IList<CSJ_ChannelData> channelDatas = new List<CSJ_ChannelData>();
            foreach (DB_Light light in data.Wrapper.lightList)
            {
                for (int i = 0; i < light.Count; i++)
                {
                    if (light.StartID + i > 512)
                    {
                        break;
                    }
                    else
                    {
                        IList<DB_Value> values = data.ValueDAO.GetPKList(new DB_ValuePK()
                        {
                            Frame = data.SceneNo,
                            Mode = data.Mode,
                            LightID = light.StartID + i,
                            LightIndex = light.LightNo
                        });
                        if (values.Count > 0)
                        {
                            List<int> isGradualChange = new List<int>();
                            List<int> stepTimes = new List<int>();
                            List<int> stepValues = new List<int>();
                            int stepNumber = 0;
                            foreach (DB_Value value in values)
                            {
                                if (data.Mode == Constant.MODE_C)
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
                            CSJ_ChannelData channelData = new CSJ_ChannelData()
                            {
                                ChannelNo = light.StartID + i,
                                IsGradualChange = isGradualChange,
                                StepTimes = stepTimes,
                                StepValues = stepValues,
                                StepCount = stepNumber
                            };
                            channelDatas.Add(channelData);
                            if (data.Mode == Constant.MODE_C)
                            {
                                C_DMXSceneChannelData[data.SceneNo].Add(channelData.ChannelNo, false);
                            }
                            else
                            {
                                M_DMXSceneChannelData[data.SceneNo].Add(channelData.ChannelNo, false);
                            }
                            Console.WriteLine("场景" + (data.SceneNo + 1) + "通道" + (light.StartID + i) + "数据获取完成");
                        }
                    }
                }
            }
            CSJ_SceneData sceneData = new CSJ_SceneData()
            {
                SceneNo = data.SceneNo,
                ChannelCount = channelDatas.Count,
                ChannelDatas = channelDatas
            };
            switch (data.Mode)
            {
                case Constant.MODE_C:
                    Console.WriteLine("基础场景" + (data.SceneNo + 1) + "数据获取完成");
                    break;
                case Constant.MODE_M:
                    Console.WriteLine("音频场景" + (data.SceneNo + 1) + "数据获取完成");
                    break;
            }
            //项目进度阻塞中，暂时优先制作安卓软件。
            CSJThreadManager.QueueSceneUserWorkItem(new WaitCallback(GeneratedSceneData),new SceneDBData(sceneData,data.Wrapper,data.ConfigPath,data.Mode),2000000);
        }
        private class C_ChannelThreadDataInfo
        {
            public CSJ_ChannelData ChannelData { get; set; }
            public int Flag { get; set; }
            public int SceneNo { get; set; }
            public float Rate { get; set; }
            public int ChannelNo { get; set; }

            public C_ChannelThreadDataInfo(CSJ_ChannelData channelData, int channelNo, int flag, int sceneNo, float rate)
            {
                this.ChannelData = channelData;
                this.Flag = flag;
                this.SceneNo = sceneNo;
                this.Rate = rate;
                this.ChannelNo = channelNo;
            }
        }
        private class M_ChannelThreadDataInfo
        {
            public CSJ_ChannelData ChannelData { get; set; }
            public int Flag { get; set; }
            public int SceneNo { get; set; }
            public float Rate { get; set; }
            public int ChannelNo { get; set; }
            public int FramTime { get; set; }

            public M_ChannelThreadDataInfo(CSJ_ChannelData channelData, int channelNo, int flag, int sceneNo, float rate,int framTime)
            {
                this.ChannelData = channelData;
                this.Flag = flag;
                this.SceneNo = sceneNo;
                this.Rate = rate;
                this.ChannelNo = channelNo;
                this.FramTime = framTime;
            }
        }
        private class SceneThreadDataInfo
        {
            public DBWrapper Wrapper { get; set; }
            public int Mode { get; set; }
            public string ConfigPath { get; set; }
            public ValueDAO ValueDAO { get; set; }
            public int SceneNo { get; set; }
            public SceneThreadDataInfo(int sceneNo, DBWrapper wrapper, ValueDAO valueDAO, int mode, string configPath)
            {
                this.Wrapper = wrapper;
                this.Mode = mode;
                this.ConfigPath = configPath;
                this.ValueDAO = valueDAO;
                this.SceneNo = sceneNo;
            }
        }
        private class DBData
        {
            public DBWrapper Wrapper { get; set; }
            public string ConfigPath { get; set; }
            public ValueDAO ValueDAO { get; set; }
            public DBData(DBWrapper wrapper, ValueDAO valueDAO, string configPath)
            {
                this.Wrapper = wrapper;
                this.ConfigPath = configPath;
                this.ValueDAO = valueDAO;
            }
        }
        private class SceneDBData
        {
            public CSJ_SceneData SceneData { get; set; }
            public DBWrapper Wrapper { get; set; }
            public string ConfigPath { get; set; }
            public int Mode { get; set; }
            public SceneDBData(CSJ_SceneData sceneData, DBWrapper wrapper, string configPath, int mode)
            {
                this.SceneData = sceneData;
                this.Wrapper = wrapper;
                this.ConfigPath = configPath;
                this.Mode = mode;
            }
        }
        private delegate void Completed();
        private delegate void Failed();
    }
}
