using CCWin.Win32.Const;
using DMX512;
using LightController.Ast;
using LightController.Tools;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using static LightController.Xiaosa.Entity.CallBackFunction;

namespace LightController.Utils
{
    public class DataGenerationProgram
    {
        private const int MODE_PREVIEW = 11;
        private const int MODE_MAKEFILE = 12;
        private const int WRITE_BUFFER_SIZE = 1024 * 50;
      
        private static DataGenerationProgram Instance { get; set; }
        private string ConfigPath { get; set; }
        private DBWrapper Wrapper { get; set; }
        private int BuildMode { get; set; }

        private Dictionary<int, bool> cSceneStatus { get; set; }
        private Dictionary<int, bool> mSceneStatus { get; set; }
        private Completed Complet_Event { get; set; }
        private Error Error_Event { get; set; }
        private DataGenerationProgram()
        {
            this.InitParam();
        }
        public static DataGenerationProgram GetInstance()
        {
            if (Instance == null) { Instance = new DataGenerationProgram(); }
            return Instance;
        }
        private void InitParam()
        {
            this.cSceneStatus = new Dictionary<int, bool>();
            this.mSceneStatus = new Dictionary<int, bool>();
        }
        private void ClearCache()
        {
            this.cSceneStatus.Clear();
            this.mSceneStatus.Clear();
        }
        public void BuildProjectFile(DBWrapper wrapper,string configPath,Completed complet,Error error)
        {
            this.Wrapper = wrapper;
            this.ConfigPath = configPath;
            this.Complet_Event += complet;
            this.Error_Event += error;
        }
        private void SceneDataGenerating(IList<DB_Value> values, int sceneNo)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Dictionary<int, Dictionary<int, DB_Value>> cChanelDatas = new Dictionary<int, Dictionary<int, DB_Value>>();
            Dictionary<int, Dictionary<int, DB_Value>> mChanelDatas = new Dictionary<int, Dictionary<int, DB_Value>>();
            Dictionary<int, int> fineTuneIndex = new Dictionary<int, int>();
            Dictionary<int, int> fineTuneMaxValue = new Dictionary<int, int>();
            foreach (DB_Value value in values)
            {
                if (value.PK.Mode == Constant.MODE_C)
                {
                    if (!cChanelDatas.ContainsKey(value.PK.LightID))
                    {
                        cChanelDatas[value.PK.LightID] = new Dictionary<int, DB_Value>();
                    }
                    cChanelDatas[value.PK.LightID].Add(value.PK.Step, value);
                }
                else
                {
                    if (!mChanelDatas.ContainsKey(value.PK.LightID))
                    {
                        mChanelDatas[value.PK.LightID] = new Dictionary<int, DB_Value>();
                    }
                    if (value.ChangeMode == Constant.MODE_M_JUMP)
                    {
                        mChanelDatas[value.PK.LightID].Add(value.PK.Step, value);
                    }
                }
            }
            if (cChanelDatas.Values.Count == 0)
            {
                return;
            }
            else
            {
                this.cSceneStatus.Add(sceneNo, false);
            }
            if (mChanelDatas.Values.Count != 0)
            {
                this.mSceneStatus.Add(sceneNo, false);
            }
            if (null != this.Wrapper.fineTuneList)
            {
                foreach (DB_FineTune item in this.Wrapper.fineTuneList)
                {
                    fineTuneIndex.Add(item.FineTuneIndex, item.MainIndex);
                    fineTuneMaxValue.Add(item.FineTuneIndex, item.MaxValue);
                }
            }
            Console.WriteLine("场景" + sceneNo + "整理数据耗时" + stopwatch.ElapsedMilliseconds + "毫秒");
            stopwatch.Stop();
            if (this.cSceneStatus.ContainsKey(sceneNo))
            {
                this.GeneratedC_SceneData(cChanelDatas, sceneNo, fineTuneIndex, fineTuneMaxValue);
            }
            if (this.mSceneStatus.ContainsKey(sceneNo))
            {
                this.GeneratedM_SceneData(mChanelDatas, sceneNo, fineTuneIndex, fineTuneMaxValue);
            }
        }
        private void GeneratedC_SceneData(Dictionary<int, Dictionary<int, DB_Value>> values, int sceneNo, Dictionary<int, int> fineTuneIndex, Dictionary<int, int> fineTuneMaxValue)
        {
            StreamReader reader;
            int channelCount = values.Keys.Count;
            string sceneFileName = "C" + (sceneNo + 1) + ".bin";
            List<byte> writeBuff = new List<byte>();
            int micSensor = 0;
            int senseFreq = 0;
            int runTime = 0;
            if (channelCount == 0)
            {
                return;
            }
            using (reader = new StreamReader(this.ConfigPath))
            {
                string lineStr = string.Empty;
                string strValue = string.Empty;
                int intValue = -1;
                while ((lineStr = reader.ReadLine()) != null)
                {
                    if (lineStr.Equals("[YM]"))
                    {
                        for (int index = 0; index < Constant.SCENECOUNTMAX; index++)
                        {
                            lineStr = reader.ReadLine();
                            if (lineStr.StartsWith(sceneNo + "CK"))
                            {
                                strValue = lineStr.Split('=')[1];
                                int.TryParse(strValue, out intValue);
                                micSensor = intValue;
                            }
                            lineStr = reader.ReadLine();
                            if (lineStr.StartsWith(sceneNo + "JG"))
                            {
                                strValue = lineStr.Split('=')[1];
                                int.TryParse(strValue, out intValue);
                                senseFreq = intValue;
                            }
                            lineStr = reader.ReadLine();
                            if (lineStr.StartsWith(sceneNo + "ZX"))
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
            FileUtils.Write(data.ToArray(), data.Count, sceneFileName, BuildMode == MODE_MAKEFILE, true, false);
            var channelDatas = from objDic in values orderby objDic.Key descending select objDic;
            foreach (KeyValuePair<int, Dictionary<int, DB_Value>> item in channelDatas)
            {
                bool isFirst = true;
                int firstStep = -1;
                int channelNo = item.Key;
                bool isFineTune = fineTuneIndex.ContainsKey(channelNo);
                string fileName = "C" + (sceneNo + 1) + "-" + channelNo + ".bin";
                int startValue = 0;
                var channedata = from objDic in item.Value orderby objDic.Key descending select objDic;
                foreach (KeyValuePair<int, DB_Value> dbValue in channedata)
                {
                    if (isFirst)
                    {
                        startValue = dbValue.Value.ScrollValue;
                        firstStep = dbValue.Key;
                        isFirst = false;
                        byte firstData;
                        if (isFineTune)
                        {
                            firstData = 0x00;
                        }
                        else
                        {
                            firstData = Convert.ToByte(startValue);
                        }
                        FileUtils.Write(firstData, fileName, BuildMode == MODE_MAKEFILE, true, true);
                    }
                    else
                    {
                        float inc = (dbValue.Value.ScrollValue - startValue) / (float)dbValue.Value.StepTime;
                        for (int fram = 0; fram < dbValue.Value.StepTime; fram++)
                        {
                            if (dbValue.Value.ChangeMode == Constant.MODE_C_GRADUAL)
                            {
                                float fValue = startValue + inc * (fram + 1);
                                if (inc < 0)
                                {
                                    fValue = fValue < 0 ? 0 : fValue;
                                }
                                else
                                {
                                    fValue = fValue > 255 ? 255 : fValue;
                                }
                                int iValue = (int)Math.Floor(fValue * 256);
                                if (isFineTune)
                                {
                                    iValue = (int)((iValue & 0xFF) / (255.0 / (fineTuneMaxValue[channelNo] == 0 ? 0 : fineTuneMaxValue[channelNo])));
                                }
                                else
                                {
                                    iValue = (int)((iValue >> 8) & 0xFF);
                                }
                                writeBuff.Add(Convert.ToByte(iValue));
                            }
                            else
                            {
                                if (isFineTune)
                                {
                                    writeBuff.Add(0x00);
                                }
                                else
                                {
                                    writeBuff.Add(Convert.ToByte(dbValue.Value.ScrollValue));
                                }
                            }
                            if (WRITE_BUFFER_SIZE <= writeBuff.Count)
                            {
                                FileUtils.Write(writeBuff.ToArray(), writeBuff.Count, fileName, BuildMode == MODE_MAKEFILE, false, true);
                                writeBuff.Clear();
                            }
                        }
                    }
                    startValue = dbValue.Value.ScrollValue;
                }
                //数据掉头
                if (firstStep != -1)
                {
                    float inc = (item.Value[firstStep].ScrollValue - startValue) / (float)item.Value[firstStep].StepTime;
                    for (int fram = 0; fram < item.Value[firstStep].StepTime; fram++)
                    {
                        if (item.Value[firstStep].ChangeMode == Constant.MODE_C_GRADUAL)
                        {
                            float fValue = startValue + inc * (fram + 1);
                            if (inc < 0)
                            {
                                fValue = fValue < 0 ? 0 : fValue;
                            }
                            else
                            {
                                fValue = fValue > 255 ? 255 : fValue;
                            }
                            int iValue = (int)Math.Floor(fValue * 256);
                            if (isFineTune)
                            {
                                iValue = (int)((iValue & 0xFF) / (255.0 / (fineTuneMaxValue[channelNo] == 0 ? 0 : fineTuneMaxValue[channelNo])));
                            }
                            else
                            {
                                iValue = (int)((iValue >> 8) & 0xFF);
                            }
                            writeBuff.Add(Convert.ToByte(iValue));
                        }
                        else
                        {
                            if (isFineTune)
                            {
                                writeBuff.Add(0x00);
                            }
                            else
                            {
                                writeBuff.Add(Convert.ToByte(item.Value[firstStep].ScrollValue));
                            }
                        }
                    }
                    FileUtils.Write(writeBuff.ToArray(), writeBuff.Count, fileName, BuildMode == MODE_MAKEFILE, false, true);
                    writeBuff.Clear();
                }
            }
            this.cSceneStatus[sceneNo] = true;
            FileUtils.MergeFile_New(Constant.GetNumber(sceneNo), Constant.MODE_C, BuildMode == MODE_MAKEFILE, (!cSceneStatus.ContainsValue(false)) && (!mSceneStatus.ContainsValue(false)), this.Complet_Event,this.Error_Event);
            Console.WriteLine("单场景完成");
        }
        private void GeneratedM_SceneData(Dictionary<int, Dictionary<int, DB_Value>> values, int sceneNo, Dictionary<int, int> fineTuneIndex, Dictionary<int, int> fineTuneMaxValue)
        {
          
        }
    }
}
