using DMX512;
using LightController.Ast;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.Utils.Ver2
{
    public class ChannelDataGeneratior
    {
        private  readonly string PROJECT_DIRECTORY_PATH = @"\DataCache\Project\CSJ";
        private  readonly string PROJECT_CHANNEL_CACHE_DIRECTORY_PATH = @"\DataCache\Project\Cache";
        private  readonly string PREVIEW_DIRECTORY_PATH = @"\DataCache\Preview\CSJ";
        private  readonly string PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH = @"\DataCache\Preview\Cache";
        private  readonly Object KEY = new object();
        private static ChannelDataGeneratior Instance { get; set; }
        private readonly string DirctoryPath = Application.StartupPath;

        private  Dictionary<int, bool> BasicTaskStatus { get; set; }
        private  Dictionary<int, bool> MusicTaskStatus { get; set; }

        private bool BasicStatus { get; set; }
        private bool MusicStatus { get; set; }
        private bool TaskStatus { get; set; }


        private Completed Completed_Event { get; set; }
        private Error Error_Event { get; set; }


        //Test
        private Stopwatch Stopwatch { get; set; }


        private ChannelDataGeneratior()
        {
            this.InitParam();
        }
        public static ChannelDataGeneratior GetInstance()
        {
            if (null == Instance)
            {
                Instance = new ChannelDataGeneratior();
            }
            return Instance;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitParam()
        {
            this.BasicTaskStatus = new Dictionary<int, bool>();
            this.MusicTaskStatus = new Dictionary<int, bool>();
            this.Completed_Event = null;
            this.Error_Event = null;
            this.BasicStatus = false;
            this.MusicStatus = false;
            this.TaskStatus = false;
        }

        private void ClearPreviewCacheDir()
        {
            if (Directory.Exists(DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH))
            {
                Directory.Delete(DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH, true);
            }
        }

        private void ClearPreviewDir()
        {
            if (Directory.Exists(DirctoryPath + PREVIEW_DIRECTORY_PATH))
            {
                Directory.Delete(DirctoryPath + PREVIEW_DIRECTORY_PATH, true);
            }
        }

        public void PreviewFileBuild(DBWrapper wrapper,string configPath,int sceneNo,Completed completed,Error error)
        {
            try
            {
                if (!TaskStatus)
                {
                    this.Stopwatch = new Stopwatch();
                    this.Stopwatch.Start();
                    GlobalBean global = new GlobalBean(configPath, wrapper.lightList);
                    this.InitParam();
                    this.ClearPreviewCacheDir();
                    this.ClearPreviewDir();
                    this.TaskStatus = true;
                    this.Completed_Event = completed;
                    this.Error_Event = error;
                    Dictionary<int, List<DB_Value>> cValues = new Dictionary<int, List<DB_Value>>();
                    Dictionary<int, List<DB_Value>> mValues = new Dictionary<int, List<DB_Value>>();
                    for (int valueIndex = 0; valueIndex < wrapper.valueList.Count; valueIndex++)
                    {
                        if (wrapper.valueList[valueIndex].PK.Frame == sceneNo)
                        {
                            switch (wrapper.valueList[valueIndex].PK.Mode)
                            {
                                case ChannelDataBean.MODE_C:
                                    if (wrapper.valueList[valueIndex].ChangeMode != ChannelDataBean.MODE_C_HIDDEN)
                                    {
                                        if (!cValues.ContainsKey(wrapper.valueList[valueIndex].PK.LightID))
                                        {
                                            cValues.Add(wrapper.valueList[valueIndex].PK.LightID, new List<DB_Value>());
                                        }
                                        cValues[wrapper.valueList[valueIndex].PK.LightID].Add(wrapper.valueList[valueIndex]);
                                    }
                                    break;
                                case ChannelDataBean.MODE_M:
                                    if (wrapper.valueList[valueIndex].ChangeMode != ChannelDataBean.MODE_M_HIDDEN)
                                    {
                                        if (!mValues.ContainsKey(wrapper.valueList[valueIndex].PK.LightID))
                                        {
                                            mValues.Add(wrapper.valueList[valueIndex].PK.LightID, new List<DB_Value>());
                                        }
                                        mValues[wrapper.valueList[valueIndex].PK.LightID].Add(wrapper.valueList[valueIndex]);
                                    }
                                    break;
                            }
                        }
                    }
                    if (cValues.Count != 0)
                    {
                        foreach (KeyValuePair<int, List<DB_Value>> item in cValues)
                        {
                            lock (this.BasicTaskStatus)
                            {
                                this.BasicTaskStatus.Add(item.Key + 0, false);
                            }
                            ThreadPool.QueueUserWorkItem(new WaitCallback(DBToBean), new WaitCallbackObject(item, 0 + sceneNo, Mode.Basics, global));
                        }
                    }
                    else
                    {
                        this.BasicStatus = true;
                        this.Complected();
                    }
                    if (mValues.Count != 0)
                    {
                        foreach (KeyValuePair<int, List<DB_Value>> item in mValues)
                        {
                            lock (this.MusicTaskStatus)
                            {
                                this.MusicTaskStatus.Add(item.Key + 0, false);
                            }
                            ThreadPool.QueueUserWorkItem(new WaitCallback(DBToBean), new WaitCallbackObject(item, 0 + sceneNo, Mode.Music, global));
                        }
                    }
                    else
                    {
                        this.MusicStatus = true;
                        this.Complected();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error(ex);
            }
        }

        public void PreviewFileBuild(DBWrapper wrapper, GlobalBean global, int sceneNo, Completed completed, Error error)
        {
            try
            {
                if (!TaskStatus)
                {
                    this.Stopwatch = new Stopwatch();
                    this.Stopwatch.Start();
                    this.InitParam();
                    this.ClearPreviewCacheDir();
                    this.ClearPreviewDir();
                    this.TaskStatus = true;
                    this.Completed_Event = completed;
                    this.Error_Event = error;
                    Dictionary<int, List<DB_Value>> cValues = new Dictionary<int, List<DB_Value>>();
                    Dictionary<int, List<DB_Value>> mValues = new Dictionary<int, List<DB_Value>>();
                    for (int valueIndex = 0; valueIndex < wrapper.valueList.Count; valueIndex++)
                    {
                        if (wrapper.valueList[valueIndex].PK.Frame == sceneNo)
                        {
                            switch (wrapper.valueList[valueIndex].PK.Mode)
                            {
                                case ChannelDataBean.MODE_C:
                                    if (wrapper.valueList[valueIndex].ChangeMode != ChannelDataBean.MODE_C_HIDDEN)
                                    {
                                        if (!cValues.ContainsKey(wrapper.valueList[valueIndex].PK.LightID))
                                        {
                                            cValues.Add(wrapper.valueList[valueIndex].PK.LightID, new List<DB_Value>());
                                        }
                                        cValues[wrapper.valueList[valueIndex].PK.LightID].Add(wrapper.valueList[valueIndex]);
                                    }
                                    break;
                                case ChannelDataBean.MODE_M:
                                    if (wrapper.valueList[valueIndex].ChangeMode != ChannelDataBean.MODE_M_HIDDEN)
                                    {
                                        if (!mValues.ContainsKey(wrapper.valueList[valueIndex].PK.LightID))
                                        {
                                            mValues.Add(wrapper.valueList[valueIndex].PK.LightID, new List<DB_Value>());
                                        }
                                        mValues[wrapper.valueList[valueIndex].PK.LightID].Add(wrapper.valueList[valueIndex]);
                                    }
                                    break;
                            }
                        }
                    }
                    if (cValues.Count != 0)
                    {
                        foreach (KeyValuePair<int, List<DB_Value>> item in cValues)
                        {
                            lock (this.BasicTaskStatus)
                            {
                                this.BasicTaskStatus.Add(item.Key + 0, false);
                            }
                            ThreadPool.QueueUserWorkItem(new WaitCallback(DBToBean), new WaitCallbackObject(item, sceneNo + 0, Mode.Basics, global));
                        }
                    }
                    else
                    {
                        this.BasicStatus = true;
                        this.Complected();
                    }
                    if (mValues.Count != 0)
                    {
                        foreach (KeyValuePair<int, List<DB_Value>> item in mValues)
                        {
                            lock (this.MusicTaskStatus)
                            {
                                this.MusicTaskStatus.Add(item.Key + 0, false);
                            }
                            ThreadPool.QueueUserWorkItem(new WaitCallback(DBToBean), new WaitCallbackObject(item, sceneNo + 0, Mode.Music, global));
                        }
                    }
                    else
                    {
                        this.MusicStatus = true;
                        this.Complected();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error(ex);
            }
        }

        private  void DBToBean(Object obj)
        {
            try
            {
                ChannelDataBean dataBean = new ChannelDataBean()
                {
                    ChannelNo = (obj as WaitCallbackObject).KeyValuePair.Key + 0,
                    StepCount = (obj as WaitCallbackObject).KeyValuePair.Value.Count + 0,
                    SceneNo = (obj as WaitCallbackObject).SceneNo + 0,
                    Mode = (obj as WaitCallbackObject).Mode,
                    StepValues = new List<int>(),
                    StepMode = new List<int>(),
                    StepTime = new List<int>()
                };
                foreach (DB_Value value in (obj as WaitCallbackObject).KeyValuePair.Value.OrderBy(m => m.PK.Step).ToList())
                {
                    dataBean.StepValues.Add(value.ScrollValue + 0);
                    dataBean.StepMode.Add(value.ChangeMode + 0);
                    dataBean.StepTime.Add(value.StepTime + 0);
                }
                this.CreatePreviewSceneChannelFile(dataBean, (obj as WaitCallbackObject).GlobalBean);
            }
            catch (Exception ex)
            {
                this.Error(ex);
            }
        }

        /// <summary>
        /// 生成工程通道文件入口
        /// </summary>
        /// <param name="dataBean"></param>
        /// <param name="dirPath"></param>
        private  void CreateSceneChannelFile(ChannelDataBean dataBean,GlobalBean global)
        {
            switch (dataBean.Mode)
            {
                case Mode.Basics:
                    CreateProjectBasicSceneCacheFile(dataBean,global);
                    break;
                case Mode.Music:
                    CreateProjectMusicSceneCacheFile(dataBean,global);
                    break;
            }
        }

        /// <summary>
        /// 生成预览通道文件入口
        /// </summary>
        /// <param name="dataBean"></param>
        /// <param name="dirPath"></param>
        private  void CreatePreviewSceneChannelFile(ChannelDataBean dataBean, GlobalBean global)
        {
            switch (dataBean.Mode)
            {
                case Mode.Basics:
                    CreatePreviewBasicSceneCacheFile(dataBean,global);
                    break;
                case Mode.Music:
                    CreatePreviewMusicSceneCacheFile(dataBean,global);
                    break;
            }
        }

        /// <summary>
        /// 创建工程基础场景通道碎片文件
        /// </summary>
        private  void CreateProjectBasicSceneCacheFile(ChannelDataBean dataBean, GlobalBean global)
        {
            try
            {
                string filePath = DirctoryPath + PROJECT_CHANNEL_CACHE_DIRECTORY_PATH + @"\C" + dataBean.SceneNo + "-" + dataBean.ChannelNo + ".bin";
                float inc;
                int stepValue;
                int stepMode;
                int startValue = dataBean.StepValues[0];
                int stepTime;
                float fValue;
                int iValue;
                List<byte> buff = new List<byte>();
                if (!Directory.Exists(DirctoryPath + PROJECT_CHANNEL_CACHE_DIRECTORY_PATH))
                {
                    Directory.CreateDirectory(DirctoryPath + PROJECT_CHANNEL_CACHE_DIRECTORY_PATH);
                }
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                buff.Add(Convert.ToByte(dataBean.ChannelFlag == ChannelFlag.FineTune ? 0 : dataBean.StepValues[0]));
                for (int stepIndex = 0; stepIndex < dataBean.StepValues.Count + 1; stepIndex++)
                {

                    int index = stepIndex == dataBean.StepCount ? 0 : stepIndex;
                    stepValue = dataBean.StepValues[index];
                    stepMode = dataBean.StepMode[index];
                    stepTime = dataBean.StepTime[index];
                    inc = (stepValue - startValue) / (float)stepTime;
                    for (int fram = 0; fram < stepTime; fram++)
                    {
                        if (stepIndex == dataBean.StepValues.Count && fram == stepTime - 1)
                        {
                            break;
                        }
                        else
                        {
                            if (stepMode == ChannelDataBean.MODE_C_GRADUAL)
                            {
                                fValue = startValue + inc * (fram + 1);
                                if (inc < 0)
                                {
                                    fValue = fValue < 0 ? 0 : fValue;
                                }
                                else
                                {
                                    fValue = fValue > 255 ? 255 : fValue;
                                }
                                iValue = (int)Math.Floor(fValue * 256);
                                if (dataBean.ChannelFlag == ChannelFlag.FineTune)
                                {
                                    iValue = (int)((iValue & 0xFF) / (255.0 / dataBean.MaxValue));
                                }
                                else
                                {
                                    iValue = (int)((iValue >> 8) & 0xFF);
                                }
                                buff.Add(Convert.ToByte(iValue));
                            }
                            else if (stepMode == ChannelDataBean.MODE_C_JUMP)
                            {
                                buff.Add(Convert.ToByte(dataBean.ChannelFlag == ChannelFlag.FineTune ? 0 : stepValue));

                            }
                        }
                    }
                }
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    stream.Write(buff.ToArray(), 0, buff.Count);
                }
                lock (this.BasicTaskStatus)
                {
                    if (this.BasicTaskStatus.ContainsKey(dataBean.ChannelNo))
                    {
                        this.BasicTaskStatus[dataBean.ChannelNo] = true;
                    }
                    if (!this.BasicTaskStatus.ContainsValue(false))
                    {
                        this.BasicProjectFileSynthesising(dataBean.SceneNo + 0,global);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error(ex);
            }
        }

        /// <summary>
        /// 创建工程音频场景通道碎片文件
        /// </summary>
        private  void CreateProjectMusicSceneCacheFile(ChannelDataBean dataBean, GlobalBean global)
        {
            try
            {
                string filePath = DirctoryPath + PROJECT_CHANNEL_CACHE_DIRECTORY_PATH + @"\M" + dataBean.SceneNo + "-" + dataBean.ChannelNo + ".bin";
                List<byte> buff = new List<byte>();
                if (!Directory.Exists(DirctoryPath + PROJECT_CHANNEL_CACHE_DIRECTORY_PATH))
                {
                    Directory.CreateDirectory(DirctoryPath + PROJECT_CHANNEL_CACHE_DIRECTORY_PATH);
                }
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                for (int stepIndex = 0; stepIndex < dataBean.StepValues.Count; stepIndex++)
                {
                    if (dataBean.StepMode[stepIndex] == ChannelDataBean.MODE_M_JUMP)
                    {
                        buff.Add(Convert.ToByte(dataBean.StepValues[stepIndex]));
                    }
                }
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    stream.Write(buff.ToArray(), 0, buff.Count);
                }
                lock (this.MusicTaskStatus)
                {
                    if (this.MusicTaskStatus.ContainsKey(dataBean.ChannelNo))
                    {
                        this.MusicTaskStatus[dataBean.ChannelNo] = true;
                    }
                    if (!this.MusicTaskStatus.ContainsValue(false))
                    {
                        this.MusicProjectFileSynthesising(dataBean.SceneNo + 0,global);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error(ex);
            }
        }

        /// <summary>
        /// 创建预览基础场景通道碎片文件
        /// </summary>
        private  void CreatePreviewBasicSceneCacheFile(ChannelDataBean dataBean, GlobalBean global)
        {
            try
            {
                string filePath = DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH + @"\C1" + "-" + dataBean.ChannelNo + ".bin";
                float inc;
                int stepValue;
                int stepMode;
                int startValue = dataBean.StepValues[0];
                int stepTime;
                float fValue;
                int iValue;
                List<byte> buff = new List<byte>();
                if (!Directory.Exists(DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH))
                {
                    Directory.CreateDirectory(DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH);
                }
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                buff.Add(Convert.ToByte(dataBean.ChannelFlag == ChannelFlag.FineTune ? 0 : dataBean.StepValues[0]));
                for (int stepIndex = 1; stepIndex < dataBean.StepValues.Count + 1; stepIndex++)
                {
                    int index = stepIndex == dataBean.StepCount ? 0 : stepIndex;
                    stepValue = dataBean.StepValues[index];
                    stepMode = dataBean.StepMode[index];
                    stepTime = dataBean.StepTime[index];
                    inc = (stepValue - startValue) / (float)stepTime;
                    for (int fram = 0; fram < stepTime; fram++)
                    {
                        if (stepIndex == dataBean.StepValues.Count && fram == stepTime - 1)
                        {
                            break;
                        }
                        else
                        {
                            if (stepMode == ChannelDataBean.MODE_C_GRADUAL)
                            {
                                fValue = startValue + inc * (fram + 1);
                                if (inc < 0)
                                {
                                    fValue = fValue < 0 ? 0 : fValue;
                                }
                                else
                                {
                                    fValue = fValue > 255 ? 255 : fValue;
                                }
                                iValue = (int)Math.Floor(fValue * 256);
                                if (dataBean.ChannelFlag == ChannelFlag.FineTune)
                                {
                                    iValue = (int)((iValue & 0xFF) / (255.0 / dataBean.MaxValue));
                                }
                                else
                                {
                                    iValue = (int)((iValue >> 8) & 0xFF);
                                }
                                buff.Add(Convert.ToByte(iValue));
                            }
                            else if (stepMode == ChannelDataBean.MODE_C_JUMP)
                            {
                                buff.Add(Convert.ToByte(dataBean.ChannelFlag == ChannelFlag.FineTune ? 0 : stepValue));

                            }
                        }
                    }
                    startValue = stepValue;
                }
                if (this.TaskStatus)
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        stream.Write(buff.ToArray(), 0, buff.Count);
                    }
                    lock (this.BasicTaskStatus)
                    {
                        if (this.BasicTaskStatus.ContainsKey(dataBean.ChannelNo))
                        {
                            this.BasicTaskStatus[dataBean.ChannelNo] = true;
                        }
                        if (!this.BasicTaskStatus.ContainsValue(false))
                        {
                            this.BasicPreviewFileSynthesising(global,dataBean.SceneNo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error(ex);
            }
        }

        /// <summary>
        /// 创建预览音频场景通道碎片文件
        /// </summary>
        private  void CreatePreviewMusicSceneCacheFile(ChannelDataBean dataBean,GlobalBean global)
        {
            try
            {
                string filePath = DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH + @"\M1" + "-" + dataBean.ChannelNo + ".bin";
                List<byte> buff = new List<byte>();
                if (!Directory.Exists(DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH))
                {
                    Directory.CreateDirectory(DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH);
                }
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                for (int stepIndex = 0; stepIndex < dataBean.StepValues.Count; stepIndex++)
                {
                    if (dataBean.StepMode[stepIndex] == ChannelDataBean.MODE_M_JUMP)
                    {
                        buff.Add(Convert.ToByte(dataBean.StepValues[stepIndex]));
                    }
                }
                if (this.TaskStatus)
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        stream.Write(buff.ToArray(), 0, buff.Count);
                    }
                    lock (this.MusicTaskStatus)
                    {
                        if (this.MusicTaskStatus.ContainsKey(dataBean.ChannelNo))
                        {
                            this.MusicTaskStatus[dataBean.ChannelNo] = true;
                        }
                        if (!this.MusicTaskStatus.ContainsValue(false))
                        {
                            this.MusicPreviewFileSynthesising(global,dataBean.SceneNo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error(ex);
            }
        }

        /// <summary>
        /// 合成工程场景文件
        /// </summary>
        private void ProjectFileSynthesising(int sceneNo,GlobalBean global)
        {
            BasicProjectFileSynthesising(sceneNo,global);
            MusicProjectFileSynthesising(sceneNo,global);
        }

        /// <summary>
        /// 合成预览场景文件
        /// </summary>
        private void PreviewFileSynthesising( GlobalBean global,int sceneNo)
        {
            BasicPreviewFileSynthesising(global,sceneNo);
            MusicPreviewFileSynthesising(global,sceneNo);
        }

        /// <summary>
        /// TODO 生成工程基础场景文件
        /// </summary>
        /// <param name="sceneNo"></param>
        private  void BasicProjectFileSynthesising(int sceneNo,GlobalBean global)
        {
            try
            {
                if (!Directory.Exists(DirctoryPath + PROJECT_DIRECTORY_PATH))
                {
                    Directory.CreateDirectory(DirctoryPath + PROJECT_DIRECTORY_PATH);
                }
                this.BasicStatus = true;
                this.Complected();
            }
            catch (Exception ex)
            {
                this.Error(ex);
            }
        }

        /// <summary>
        /// TODO 生成工程音频场景文件
        /// </summary>
        /// <param name="sceneNo"></param>
        private  void MusicProjectFileSynthesising(int sceneNo,GlobalBean global)
        {
            try
            {
                if (!Directory.Exists(DirctoryPath + PROJECT_DIRECTORY_PATH))
                {
                    Directory.CreateDirectory(DirctoryPath + PROJECT_DIRECTORY_PATH);
                }
                this.MusicStatus = true;
                this.Complected();
            }
            catch (Exception ex)
            {
                this.Error(ex);
            }
        }

        /// <summary>
        /// 生成预览基础场景文件
        /// </summary>
        /// <param name="sceneNo"></param>
        private  void BasicPreviewFileSynthesising(GlobalBean global,int frameNo)
        {
            try
            {
                List<byte> dataBuff = new List<byte>();
                if (!Directory.Exists(DirctoryPath + PREVIEW_DIRECTORY_PATH))
                {
                    Directory.CreateDirectory(DirctoryPath + PREVIEW_DIRECTORY_PATH);
                }
                if (File.Exists(DirctoryPath + PREVIEW_DIRECTORY_PATH + @"\C1.bin"))
                {
                    File.Delete(DirctoryPath + PREVIEW_DIRECTORY_PATH + @"\C1.bin");
                }
                if (Directory.Exists(DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH))
                {
                    File.Create(DirctoryPath + PREVIEW_DIRECTORY_PATH + @"\C1.bin").Dispose();
                    #region 包头
                    long seek = 0;
                    dataBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 4).ToArray());
                    dataBuff.Add(Convert.ToByte(global.ShakeMics[frameNo].IsOpen ? 0x01 : 0x00));
                    dataBuff.Add(Convert.ToByte((global.ShakeMics[frameNo].IntervalTime * 60) & 0xFF));
                    dataBuff.Add(Convert.ToByte(((global.ShakeMics[frameNo].IntervalTime * 60) >> 8) & 0xFF));
                    dataBuff.Add(Convert.ToByte((global.ShakeMics[frameNo].RunTime * 60) & 0xFF));
                    dataBuff.Add(Convert.ToByte(((global.ShakeMics[frameNo].RunTime * 60) >> 8) & 0xFF));
                    dataBuff.Add(Convert.ToByte(this.BasicTaskStatus.Count & 0xFF));
                    dataBuff.Add(Convert.ToByte((this.BasicTaskStatus.Count >> 8) & 0xFF));
                    seek += dataBuff.Count;
                    #endregion
                    #region 包体
                    foreach (string filePath in Directory.GetFileSystemEntries(DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH))
                    {
                        long dataSize = 0;
                        string projectFilePath = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                        if (!projectFilePath[0].Equals('C'))
                        {
                            continue;
                        }
                        string strChannel = projectFilePath.Split('.')[0].Split('-')[1];
                        string strScene = projectFilePath.Split('.')[0].Split('-')[0].Substring(1);
                        int.TryParse(strScene, out int sceneNo);
                        int.TryParse(strChannel, out int channelNo);
                        if (sceneNo == 1)
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Open))
                            {
                                dataSize = stream.Length;
                                seek += 8;
                                dataBuff.Add(Convert.ToByte(channelNo & 0xFF));
                                dataBuff.Add(Convert.ToByte((channelNo >> 8) & 0xFF));
                                dataBuff.Add(Convert.ToByte(dataSize & 0xFF));
                                dataBuff.Add(Convert.ToByte((dataSize >> 8) & 0xFF));
                                dataBuff.Add(Convert.ToByte(seek & 0xFF));
                                dataBuff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                                dataBuff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                                dataBuff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                                byte[] buff = new byte[dataSize];
                                while (stream.Read(buff, 0, buff.Length) != 0)
                                {
                                    dataBuff.AddRange(buff);
                                }
                            }
                            using (FileStream stream = new FileStream(DirctoryPath + PREVIEW_DIRECTORY_PATH + @"\C1.bin", FileMode.Append))
                            {
                                stream.Write(dataBuff.ToArray(), 0, dataBuff.Count());
                            }
                            seek += dataSize;
                            dataBuff.Clear();
                        }
                    }
                    dataBuff.Clear();
                    dataBuff.Add(Convert.ToByte(seek & 0xFF));
                    dataBuff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                    dataBuff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                    dataBuff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                    using (FileStream stream = new FileStream(DirctoryPath + PREVIEW_DIRECTORY_PATH + @"\C1.bin", FileMode.Open))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.Write(dataBuff.ToArray(), 0, dataBuff.Count);
                    }
                    #endregion
                    if (seek < 12)
                    {
                        File.Delete(DirctoryPath + PREVIEW_DIRECTORY_PATH + @"\C1.bin");
                    }
                }
                this.BasicStatus = true;
                this.Complected();
            }
            catch (Exception ex)
            {
                this.Error(ex);
            }
        }

        /// <summary>
        /// 生成预览音频场景文件
        /// </summary>
        /// <param name="sceneNo"></param>
        private  void MusicPreviewFileSynthesising(GlobalBean global,int frameNo)
        {
            try
            {
                List<byte> dataBuff = new List<byte>();
                if (!Directory.Exists(DirctoryPath + PREVIEW_DIRECTORY_PATH))
                {
                    Directory.CreateDirectory(DirctoryPath + PREVIEW_DIRECTORY_PATH);
                }
                if (File.Exists(DirctoryPath + PREVIEW_DIRECTORY_PATH + @"\M1.bin"))
                {
                    File.Delete(DirctoryPath + PREVIEW_DIRECTORY_PATH + @"\M1.bin");
                }
                if (Directory.Exists(DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH))
                {
                    File.Create(DirctoryPath + PREVIEW_DIRECTORY_PATH + @"\M1.bin").Dispose();
                    #region 包头
                    long seek = 0;
                    dataBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 4).ToArray());
                    dataBuff.Add(Convert.ToByte(global.MusicSceneSets[frameNo].MusicStepTime & 0xFF));
                    dataBuff.Add(Convert.ToByte(global.MusicSceneSets[frameNo].MusicIntervalTime & 0xFF));
                    dataBuff.Add(Convert.ToByte((global.MusicSceneSets[frameNo].MusicIntervalTime >> 8) & 0xFF));
                    dataBuff.Add(Convert.ToByte((global.MusicSceneSets[frameNo].MusicStepList.Count) & 0xFF));
                    for (int index = 0; index < global.MusicSceneSets[frameNo].MusicStepList.Count; index++)
                    {
                        dataBuff.Add(Convert.ToByte(global.MusicSceneSets[frameNo].MusicStepList[index] & 0xFF));
                    }
                    for (int index = global.MusicSceneSets[frameNo].MusicStepList.Count; index < 20; index++)
                    {
                        dataBuff.Add(0x00);
                    }
                    dataBuff.Add(Convert.ToByte(this.MusicTaskStatus.Count & 0xFF));
                    dataBuff.Add(Convert.ToByte((this.MusicTaskStatus.Count >> 8) & 0xFF));
                    seek += dataBuff.Count;

                    #endregion
                    #region 包体
                    foreach (string filePath in Directory.GetFileSystemEntries(DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH))
                    {
                        long dataSize = 0;
                        string projectFilePath = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                        if (!projectFilePath[0].Equals('M'))
                        {
                            continue;
                        }
                        string strChannel = projectFilePath.Split('.')[0].Split('-')[1];
                        string strScene = projectFilePath.Split('.')[0].Split('-')[0].Substring(1);
                        int.TryParse(strScene, out int sceneNo);
                        int.TryParse(strChannel, out int channelNo);
                        if (sceneNo == 1)
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Open))
                            {
                                dataSize = stream.Length;
                                seek += 8;
                                dataBuff.Add(Convert.ToByte(channelNo & 0xFF));
                                dataBuff.Add(Convert.ToByte((channelNo >> 8) & 0xFF));
                                dataBuff.Add(Convert.ToByte(dataSize & 0xFF));
                                dataBuff.Add(Convert.ToByte((dataSize >> 8) & 0xFF));
                                dataBuff.Add(Convert.ToByte(seek & 0xFF));
                                dataBuff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                                dataBuff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                                dataBuff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                                byte[] buff = new byte[dataSize];
                                while (stream.Read(buff, 0, buff.Length) != 0)
                                {
                                    dataBuff.AddRange(buff);
                                }
                            }
                            using (FileStream stream = new FileStream(DirctoryPath + PREVIEW_DIRECTORY_PATH + @"\M1.bin", FileMode.Append))
                            {
                                stream.Write(dataBuff.ToArray(), 0, dataBuff.Count());
                            }
                            seek += dataSize;
                            dataBuff.Clear();
                        }
                    }
                    dataBuff.Clear();
                    dataBuff.Add(Convert.ToByte(seek & 0xFF));
                    dataBuff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                    dataBuff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                    dataBuff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                    using (FileStream stream = new FileStream(DirctoryPath + PREVIEW_DIRECTORY_PATH + @"\M1.bin", FileMode.Open))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.Write(dataBuff.ToArray(), 0, dataBuff.Count);
                    }
                    #endregion
                    if (seek < 31)
                    {
                        File.Delete(DirctoryPath + PREVIEW_DIRECTORY_PATH + @"\M1.bin");
                    }
                }
                this.MusicStatus = true;
                this.Complected();
            }
            catch (Exception ex)
            {
                this.Error(ex);
            }
        }

        private void Complected()
        {
            lock (KEY)
            {
                if (this.TaskStatus)
                {
                    if (this.MusicStatus & this.BasicStatus)
                    {
                        Console.WriteLine("总耗时:" + this.Stopwatch.ElapsedMilliseconds);
                        this.Completed_Event("完成");
                        this.InitParam();
                    }
                }
            }
        }

        private void Error(Exception ex)
        {
            lock (KEY)
            {
                Console.WriteLine(ex.Message);
                if (this.TaskStatus)
                {
                    this.Error_Event("失败" + ex.Message);
                    this.InitParam();
                }
            }
        }
    }

    public delegate void Completed(string msg);
    public delegate void Error(string msg);

    enum Mode
    {
        Basics,Music
    }

    enum ChannelFlag
    {
        MainTune,FineTune,Null
    }

    class WaitCallbackObject
    {
        public KeyValuePair<int, List<DB_Value>> KeyValuePair { get; set; }
        public int SceneNo { get; set; }
        public Mode Mode { get; set; }
        public GlobalBean GlobalBean { get; set; }
        public WaitCallbackObject(KeyValuePair<int, List<DB_Value>> keyValuePair, int sceneNo,Mode mode,GlobalBean global)
        {
            this.KeyValuePair = keyValuePair;
            this.SceneNo = sceneNo;
            this.Mode = mode;
            this.GlobalBean = global;
        }
    }
}
