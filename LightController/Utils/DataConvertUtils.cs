using DMX512;
using LightController.Ast;
using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;
using System.Diagnostics;
using LightController.MyForm;

namespace LightController.Utils
{
    public class DataConvertUtils
    {
        private static Dictionary<int, Dictionary<int, bool>> C_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
        private static Dictionary<int, bool> C_DMXSceneState = new Dictionary<int, bool>();
        private static Dictionary<int, Dictionary<int, bool>> M_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
        private static Dictionary<int, bool> M_DMXSceneState = new Dictionary<int, bool>();
        private static Dictionary<int, bool> C_PreviewDataState = new Dictionary<int, bool>();
        private static Dictionary<int, bool> M_PreviewDataState = new Dictionary<int, bool>();
        private static CSJ_SceneData C_PreviewSceneData { get; set; }
        private static CSJ_SceneData M_PreviewSceneData { get; set; }
        public static readonly int MODE_PREVIEW = FileUtils.MODE_PREVIEW;
        public static readonly int MODE_MAKEFILE = FileUtils.MODE_MAKEFILE;
        private static int BuildMode { get; set; }
        private const int WriteBufferSize = 1024 * 50;
        private static ISaveProjectCallBack CallBack { get; set; }

        public static bool Flag = false;

        public static void InitThreadPool()
        {
            ThreadPool.SetMaxThreads(100, 10);
        }

        /// <summary>
        ///生成全场景数据入口
        /// </summary>
        /// <param name="wrapper"></param>
        /// <param name="valueDAO"></param>
        /// <param name="configPath"></param>
        public static void SaveProjectFile(DBWrapper wrapper, MainFormInterface mainForm, string configPath,ISaveProjectCallBack callBack)
        {
            InitThreadPool();
            CallBack = callBack;
            BuildMode = MODE_MAKEFILE;
            FileUtils.ClearCacheData();
            FileUtils.ClearProjectData();
            FileUtils.CreateConfig(new CSJ_Config(wrapper, configPath));
            //初始化状态存储器
            C_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
            C_DMXSceneState = new Dictionary<int, bool>();
            M_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
            M_DMXSceneState = new Dictionary<int, bool>();
            //启动线程开始执行数据生成及数据导出文件
            ThreadPool.QueueUserWorkItem(new WaitCallback(GeneratedDBSceneData), new DBData(wrapper, mainForm, configPath));
        }
        /// <summary>
        /// 检索数据库获取数据
        /// </summary>
        /// <param name="obj"></param>
        private static void GeneratedDBSceneData(Object obj)
        {
            DBData data = obj as DBData;
            DBWrapper wrapper = data.Wrapper;
            string configPath = data.ConfigPath;
            MainFormInterface mainForm = data.MianForm;
            List<int> c_SceneNos = new List<int>();
            List<int> m_SceneNos = new List<int>();
            foreach (DB_StepCount item in data.Wrapper.stepCountList)
            {
                if (!c_SceneNos.Contains(item.PK.Frame) && item.PK.Mode == Constant.MODE_C)
                {
                    c_SceneNos.Add(item.PK.Frame);
                }
            }
            foreach (DB_StepCount item in data.Wrapper.stepCountList)
            {
                if (!m_SceneNos.Contains(item.PK.Frame) && item.PK.Mode == Constant.MODE_M)
                {
                    m_SceneNos.Add(item.PK.Frame);
                }
            }
            //基础场景数据生成
            foreach (int sceneNo in c_SceneNos)
            {
                C_DMXSceneChannelData.Add(sceneNo, new Dictionary<int, bool>());
                C_DMXSceneState.Add(sceneNo, false);
            }
            foreach (int sceneNo in m_SceneNos)
            {
                M_DMXSceneChannelData.Add(sceneNo, new Dictionary<int, bool>());
                M_DMXSceneState.Add(sceneNo, false);
            }
            foreach (int sceneNo in c_SceneNos)
            {
                Flag = false;
                GetSceneDataWaitCallback(new SceneThreadDataInfo(sceneNo, wrapper, mainForm, Constant.MODE_C, configPath));
                //TODO 测试
                while (true)
                {
                    if (Flag)
                    {
                        break;
                    }
                }
            }
            //音频场景数据生成
            foreach (int sceneNo in m_SceneNos)
            {
                Flag = false;
                GetSceneDataWaitCallback(new SceneThreadDataInfo(sceneNo, wrapper, mainForm, Constant.MODE_M, configPath));
                //TODO 测试
                while (true)
                {
                    if (Flag)
                    {
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 生成场景数据库数据
        /// </summary>
        /// <param name="obj"></param>
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
                        
                        IList<TongdaoWrapper> values = data.MainForm.GetFMTDList(new DB_ValuePK()
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
                            foreach (TongdaoWrapper value in values)
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
                            if (channelData.StepValues.Count > 0)
                            {
                                channelDatas.Add(channelData);
                                if (data.Mode == Constant.MODE_C)
                                {
                                    lock (C_DMXSceneChannelData)
                                    {
                                        C_DMXSceneChannelData[data.SceneNo].Add(channelData.ChannelNo, false);
                                    }

                                }
                                else
                                {
                                    lock (M_DMXSceneChannelData)
                                    {
                                        M_DMXSceneChannelData[data.SceneNo].Add(channelData.ChannelNo, false);
                                    }
                                }
                            }
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
            if (sceneData.ChannelCount == 0)
            {
                switch (data.Mode)
                {
                    case Constant.MODE_M:
                        M_DMXSceneChannelData.Remove(data.SceneNo);
                        M_DMXSceneState.Remove(data.SceneNo);
                        break;
                    case Constant.MODE_C:
                        C_DMXSceneChannelData.Remove(data.SceneNo);
                        C_DMXSceneState.Remove(data.SceneNo);
                        break;
                }
                if (!C_DMXSceneState.ContainsValue(false) && !M_DMXSceneState.ContainsValue(false))
                {
                    M_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
                    M_DMXSceneState = new Dictionary<int, bool>();
                    C_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
                    C_DMXSceneState = new Dictionary<int, bool>();
                    FileUtils.CreateGradientData();
                    CallBack.Completed();
                }
                else
                {
                    Flag = true;
                }
            }
            else
            {
                //TODO 测试同步语句效率
                GeneratedSceneData(new SceneDBData(sceneData, data.Wrapper, data.ConfigPath, data.Mode));
                //ThreadPool.QueueUserWorkItem(new WaitCallback(GeneratedSceneData), new SceneDBData(sceneData, data.Wrapper, data.ConfigPath, data.Mode));
            }
        }
        private static void GeneratedSceneData(Object obj)
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
        /// <summary>
        /// 基础场景数据处理
        /// </summary>
        /// <param name="sceneData"></param>
        /// <param name="wrapper"></param>
        /// <param name="configPath"></param>
        /// <returns></returns>
        private static void GeneratedC_SceneData(CSJ_SceneData sceneData, DBWrapper wrapper, string configPath)
        {
            sceneData.ChannelCount = sceneData.ChannelDatas.Count;
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
            if (sceneData.ChannelCount == 0)
            {
                return;
            }
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
            FileUtils.Write(data.ToArray(),data.Count, sceneFileName, BuildMode == MODE_MAKEFILE, true, false);
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
                C_ChannelThreadDataInfo dataInfo = new C_ChannelThreadDataInfo(currentChannelData, Constant.GetNumber(cSJ_ChannelData.ChannelNo), flag, sceneNo, rate);
                dataInfo.SetName("C1-" + Constant.GetNumber(cSJ_ChannelData.ChannelNo) + ".bin");
                //TODO 测试同步语句效率-基础场景
                ConvertC_DataWaitCallback(dataInfo);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(ConvertC_DataWaitCallback), dataInfo);
            }
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
            try
            {
                FileUtils.Write(((flag == 2) ? Convert.ToByte(0) : Convert.ToByte(startValue)), fileName, BuildMode == MODE_MAKEFILE, true, true);
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
                            FileUtils.Write(WriteBuffer.ToArray(), WriteBuffer.Count, fileName, BuildMode == MODE_MAKEFILE, false, true);
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
                                FileUtils.Write(WriteBuffer.ToArray(), WriteBuffer.Count, fileName, BuildMode == MODE_MAKEFILE, false, true);
                                WriteBuffer.Clear();
                            }
                        }
                    }
                    startValue = stepValue;
                }
                DataCacheWriteCompleted(Constant.GetNumber(sceneNo), Constant.GetNumber(dataInfo.ChannelNo), Constant.MODE_C);
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "计算生成常规文件数据出错-" + fileName,ex);
            }
        }
        /// <summary>
        /// 音频场景数据处理
        /// </summary>
        /// <param name="sceneData"></param>
        /// <param name="wrapper"></param>
        /// <param name="configPath"></param>
        /// <returns></returns>
        private static void GeneratedM_SceneData(CSJ_SceneData scenedata, DBWrapper wrapperdata, string configpath)
        {
            try
            {
                CSJ_SceneData sceneData = scenedata;
                DBWrapper wrapper = wrapperdata;
                string configPath = configpath;
                string sceneFileName = "M" + (sceneData.SceneNo + 1) + ".bin";
                StreamReader reader;
                List<int> stepList = new List<int>();
                int channelCount = sceneData.ChannelCount;
                int sceneNo = Constant.GetNumber(sceneData.SceneNo);
                int frameTime = 0;
                int musicIntervalTime = 0;
                int mainIndex = 0;
                float rate = 255;
                int flag;
                if (scenedata.ChannelCount == 0)
                {
                    return;
                }
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
                data.Add(Convert.ToByte(stepList.Count));
                for (int i = 0; i < stepList.Count; i++)
                {
                    data.Add(Convert.ToByte(stepList[i]));
                }
                for (int i = stepList.Count; i < Constant.STEPLISTMAX; i++)
                {
                    data.Add(Convert.ToByte(0x00));
                }
                data.Add(Convert.ToByte(channelCount & 0xFF));
                data.Add(Convert.ToByte((channelCount >> 8) & 0xFF));
                FileUtils.Write(data.ToArray(), data.Count, sceneFileName, BuildMode == MODE_MAKEFILE, true, false);
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
                    M_ChannelThreadDataInfo dataInfo = new M_ChannelThreadDataInfo(currentChannelData, Constant.GetNumber(cSJ_ChannelData.ChannelNo), flag, Constant.GetNumber(sceneNo), rate, frameTime);
                    //TODO 测试同步语句效率-音频场景
                    ConvertM_DataWaitCallback(dataInfo);
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(ConvertM_DataWaitCallback), dataInfo);
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "音频场景数据处理出现异常", ex);
            }
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
            try
            {
                FileUtils.Write(((flag == 2) ? Convert.ToByte(0) : Convert.ToByte(startValue)), fileName, BuildMode == MODE_MAKEFILE, true, true);
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
                            FileUtils.Write(WriteBuffer.ToArray(), WriteBuffer.Count, fileName, BuildMode == MODE_MAKEFILE, false, true);
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
                                    FileUtils.Write(WriteBuffer.ToArray(), WriteBuffer.Count, fileName, BuildMode == MODE_MAKEFILE, false, true);
                                    WriteBuffer.Clear();
                                    break;
                                }
                                else
                                {
                                    WriteBuffer.Add(Convert.ToByte(stepValue));
                                    FileUtils.Write(WriteBuffer.ToArray(), WriteBuffer.Count, fileName, BuildMode == MODE_MAKEFILE, false, true);
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
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "计算生成音频数据出错-" + fileName, ex);
            }
           
        }
        /// <summary>
        /// 数据处理完成
        /// </summary>
        /// <param name="sceneNo"></param>
        /// <param name="channelNo"></param>
        /// <param name="mode"></param>
        private static void DataCacheWriteCompleted(int sceneNo, int channelNo, int mode)
        {
            switch (mode)
            {
                case Constant.MODE_C:
                    if (CallBack != null)
                    {
                        CallBack.UpdateProgress("基础场景" + (sceneNo + 1) + "-" + channelNo + "完成");
                    }
                    lock (C_DMXSceneChannelData)
                    {
                        C_DMXSceneChannelData[sceneNo][channelNo] = true;
                    }
                    if (!C_DMXSceneChannelData[sceneNo].ContainsValue(false))
                    {
                        lock (C_DMXSceneState)
                        {
                            C_DMXSceneState[sceneNo] = true;
                        }
                        //TODO 测试
                        FileUtils.MergeFile(Constant.GetNumber(sceneNo), mode,  BuildMode == MODE_MAKEFILE, (!C_DMXSceneState.ContainsValue(false)) && (!M_DMXSceneState.ContainsValue(false)), CallBack);
                    }
                    if (!C_DMXSceneState.ContainsValue(false))
                    {
                        C_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
                        C_DMXSceneState = new Dictionary<int, bool>();
                    }
                    break;
                case Constant.MODE_M:
                    if (CallBack != null)
                    {
                        CallBack.UpdateProgress("音频场景" + (sceneNo + 1) + "-" + channelNo + "完成");
                    }
                    lock (M_DMXSceneChannelData)
                    {
                        M_DMXSceneChannelData[sceneNo][channelNo] = true;
                    }
                    if (!M_DMXSceneChannelData[sceneNo].ContainsValue(false))
                    {
                        lock (M_DMXSceneState)
                        {
                            M_DMXSceneState[sceneNo] = true;
                        }
                        //TODO 测试
                        FileUtils.MergeFile(Constant.GetNumber(sceneNo), mode,  BuildMode == MODE_MAKEFILE, (!C_DMXSceneState.ContainsValue(false)) && (!M_DMXSceneState.ContainsValue(false)), CallBack);
                    }
                    if (!M_DMXSceneState.ContainsValue(false))
                    {
                        M_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
                        M_DMXSceneState = new Dictionary<int, bool>();
                    }
                    break;
            }
        }
        /// <summary>
        ///预览数据生成入口
        /// </summary>
        public static void SaveProjectFileByPreviewData(DBWrapper wrapper, string configPath,int sceneNo,ISaveProjectCallBack callBack)
        {
            CallBack = callBack;
            FileUtils.ClearPreviewCacheData();
            FileUtils.ClearPreviewProjectData();
            BuildMode = MODE_PREVIEW;
            C_PreviewDataState = new Dictionary<int, bool>();
            M_PreviewDataState = new Dictionary<int, bool>();
            C_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
            C_DMXSceneState = new Dictionary<int, bool>();
            M_DMXSceneChannelData = new Dictionary<int, Dictionary<int, bool>>();
            M_DMXSceneState = new Dictionary<int, bool>();
            C_PreviewSceneData = null;
            M_PreviewSceneData = null;
            //TODO 测试同步语句效率-整理预览数据
            GeneratedPreviewSceneData(new PreviewData(wrapper, configPath, Constant.GetNumber(sceneNo)));
            //ThreadPool.QueueUserWorkItem(new WaitCallback(GeneratedPreviewSceneData), new PreviewData(wrapper, configPath, Constant.GetNumber(sceneNo)));

        }
        /// <summary>
        /// 整理生成预览场景数据
        /// </summary>
        /// <param name="obj"></param>
        private static void GeneratedPreviewSceneData(Object obj)
        {
            DBWrapper wrapper = (obj as PreviewData).Wrapper;
            string configPath = (obj as PreviewData).ConfigPath;
            int sceneNo = (obj as PreviewData).SceneNo;
            int c_StepCount = 0;
            int m_StepCount = 0;
            Dictionary<int, List<DB_Value>> c_ValuePairs = new Dictionary<int, List<DB_Value>>();
            Dictionary<int, List<DB_Value>> m_ValuePairs = new Dictionary<int, List<DB_Value>>();
            CSJ_SceneData c_SceneData = new CSJ_SceneData()
            {
                SceneNo = sceneNo,
                ChannelCount = 0,
                ChannelDatas = new List<CSJ_ChannelData>()
            };
            CSJ_SceneData m_SceneData = new CSJ_SceneData()
            {
                SceneNo = sceneNo,
                ChannelCount = 0,
                ChannelDatas = new List<CSJ_ChannelData>()
            };
            foreach (DB_StepCount item in wrapper.stepCountList)
            {
                if (item.PK.Frame == sceneNo && item.PK.Mode == Constant.MODE_C)
                {
                    c_StepCount = item.StepCount;
                }
                if (item.PK.Frame == sceneNo && item.PK.Mode == Constant.MODE_M)
                {
                    m_StepCount = item.StepCount;
                }
            }
            if (c_StepCount > 0)
            {
                C_DMXSceneChannelData.Add(0, new Dictionary<int, bool>());
                C_DMXSceneState.Add(0, false);
            }
            if (m_StepCount > 0)
            {
                M_DMXSceneChannelData.Add(0, new Dictionary<int, bool>());
                M_DMXSceneState.Add(0, false);
            }
            foreach (DB_Value item in wrapper.valueList)
            {
                switch (item.PK.Mode)
                {
                    case Constant.MODE_C:
                        if (!c_ValuePairs.ContainsKey(item.PK.LightID))
                        {
                            c_ValuePairs.Add(item.PK.LightID, new List<DB_Value>());
                        }
                        if (item.ChangeMode != Constant.MODE_C_HIDDEN)
                        {
                            c_ValuePairs[item.PK.LightID].Add(item);
                        }
                        break;
                    case Constant.MODE_M:
                        if (!m_ValuePairs.ContainsKey(item.PK.LightID))
                        {
                            m_ValuePairs.Add((item.PK.LightID), new List<DB_Value>());
                        }
                        if (item.ChangeMode == Constant.MODE_M_JUMP)
                        {
                            m_ValuePairs[item.PK.LightID].Add(item);
                        }
                        break;
                    default:
                        break;
                }
            }
            foreach (int channelNo in c_ValuePairs.Keys)
            {
                List<int> isGradualChange = new List<int>();
                List<int> stepValues = new List<int>();
                List<int> stepTimes = new List<int>();
                foreach (DB_Value value in (c_ValuePairs[channelNo]).OrderBy(m => m.PK.Step).ToList())
                {
                    isGradualChange.Add(value.ChangeMode);
                    stepValues.Add(value.ScrollValue);
                    stepTimes.Add(value.StepTime);
                }
                CSJ_ChannelData channelData = new CSJ_ChannelData
                {
                    ChannelNo = channelNo,
                    IsGradualChange = isGradualChange,
                    StepCount = stepValues.Count,
                    StepTimes = stepTimes,
                    StepValues = stepValues
                };
                if (channelData.StepCount > 0)
                {
                    c_SceneData.ChannelDatas.Add(channelData);
                    C_DMXSceneChannelData[0].Add(channelNo, false);
                }
            }
            foreach (int channelNo in m_ValuePairs.Keys)
            {
                List<int> isGradualChange = new List<int>();
                List<int> stepValues = new List<int>();
                List<int> stepTimes = new List<int>();
                foreach (DB_Value value in (m_ValuePairs[channelNo]).OrderBy(m => m.PK.Step).ToList())
                {
                    isGradualChange.Add(value.ChangeMode);
                    stepValues.Add(value.ScrollValue);
                    stepTimes.Add(value.StepTime);
                }
                CSJ_ChannelData channelData = new CSJ_ChannelData
                {
                    ChannelNo = channelNo,
                    IsGradualChange = isGradualChange,
                    StepCount = stepValues.Count,
                    StepTimes = stepTimes,
                    StepValues = stepValues
                };
                if (channelData.StepCount > 0)
                {
                    m_SceneData.ChannelDatas.Add(channelData);
                    M_DMXSceneChannelData[0].Add(channelNo, false);
                }
            }
            if (c_StepCount > 0)
            {
                c_SceneData.ChannelCount = c_SceneData.ChannelDatas.Count();
                c_SceneData.SceneNo = 0;
                if (C_DMXSceneChannelData[0].Count == 0)
                {
                    C_DMXSceneState.Remove(0);
                }
                else
                {
                    //TODO 测试同步语句效率-预览基础场景数据
                    GeneratedSceneData(new SceneDBData(c_SceneData, wrapper, configPath, Constant.MODE_C));
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(GeneratedSceneData), new SceneDBData(c_SceneData, wrapper, configPath, Constant.MODE_C));
                }
            }
            if (m_StepCount > 0)
            {
                m_SceneData.ChannelCount = m_SceneData.ChannelDatas.Count();
                m_SceneData.SceneNo = 0;
                if (M_DMXSceneChannelData[0].Count == 0)
                {
                    M_DMXSceneState.Remove(0);
                }
                else
                {
                    //TODO 测试同步语句效率-预览基础场景数据
                    GeneratedSceneData(new SceneDBData(m_SceneData, wrapper, configPath, Constant.MODE_M));
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(GeneratedSceneData), new SceneDBData(m_SceneData, wrapper, configPath, Constant.MODE_M));
                }
            }
        }
        private class C_ChannelThreadDataInfo : BaseThreadDataInfo
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
        private class M_ChannelThreadDataInfo : BaseThreadDataInfo
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
            public MainFormInterface MainForm { get; set; }
            public int SceneNo { get; set; }
            public SceneThreadDataInfo(int sceneNo, DBWrapper wrapper, MainFormInterface mainForm, int mode, string configPath)
            {
                this.Wrapper = wrapper;
                this.Mode = mode;
                this.ConfigPath = configPath;
                this.MainForm = mainForm;
                this.SceneNo = sceneNo;
            }
        }
        private class DBData
        {
            public DBWrapper Wrapper { get; set; }
            public string ConfigPath { get; set; }
            public MainFormInterface MianForm { get; set; }
            public DBData(DBWrapper wrapper, MainFormInterface mianForm, string configPath)
            {
                this.Wrapper = wrapper;
                this.ConfigPath = configPath;
                this.MianForm = mianForm;
            }
        }
        private class PreviewData
        {
            public DBWrapper Wrapper { get; set; }
            public string ConfigPath { get; set; }
            public int SceneNo { get; set; }
            public PreviewData(DBWrapper wrapper,string configPath,int sceneNo)
            {
                this.Wrapper = wrapper;
                this.ConfigPath = configPath;
                this.SceneNo = sceneNo;
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
        private class PreViewDataInfo
        {
            public DBWrapper Wrapper { get; set; }
            public int Mode { get; set; }
            public int SceneNo { get; set; }
            public int ChannelNo { get; set; }
            public int StepCount { get; set; }
            public int LightIndex { get; set; }
            public string ConfigPath { get; set; }
            public PreViewDataInfo(DBWrapper wrapper, string configPath , int mode,int sceneNo,int channelNo,int stepCount,int lightIndex)
            {
                this.Mode = mode;
                this.SceneNo = sceneNo;
                this.ChannelNo = channelNo;
                this.StepCount = stepCount;
                this.LightIndex = lightIndex;
                this.Wrapper = wrapper;
                this.ConfigPath = configPath;
            }
        }
        public class BaseThreadDataInfo
        {
            string Name { get; set; }
            public string GetName()
            {
                return this.Name;
            }
            public void SetName(string name)
            {
                this.Name = name;
            }
        }
        private delegate void Completed();
        private delegate void Failed();
    }
}
