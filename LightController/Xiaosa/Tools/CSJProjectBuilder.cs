using LightController.Ast;
using LightController.Entity;
using LightController.MyForm;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.Xiaosa.Tools
{
    /*
     * TODO 新版全局工程以及局部工程文件生成
     * 1：全局工程文件生成
     * 1.1：根据场景及通道分配线程执行后续操作，有数据的场景及通道添加任务完成指示器
     * 1.2：分配固定线程数，每个线程分配16个通道进行处理
     * 
     * */
    public class CSJProjectBuilder
    {
        private static Object SingleKey = new object();
        private static Object BasicTaskKey = new object();
        private static Object MusicTaskKey = new object();
        private const int STEPLISTSIZE = 20;
        private const int BASIC_MODE = 0;
        private const int MUSIC_MODE = 1;
        private static string ProjectCacheDir = Application.StartupPath + @"\DataCache\Project\Cache";
        private static string ProjectFileDir = Application.StartupPath + @"\DataCache\Project\CSJ";
        private static CSJProjectBuilder Instance;
        private ConcurrentDictionary<int, bool> MultipartChannelBasicTaskState;
        private ConcurrentDictionary<int, bool> MultipartChannelMusicTaskState;
        private bool SceneBasicTaskState;
        private bool SceneMusicTaskState;
        private int CurrentSceneNo;//从0开始
        private MainFormInterface MainFormInterface;

        private CSJProjectBuilder()
        {
            Init();
        }
        private void Init()
        {
            InitChannelTaskState();
        }
        private void InitChannelTaskState()
        {
            MultipartChannelBasicTaskState = new ConcurrentDictionary<int, bool>();
            MultipartChannelMusicTaskState = new ConcurrentDictionary<int, bool>();
            SceneBasicTaskState = false;
            SceneMusicTaskState = false;
            CurrentSceneNo = 0;
        }
        private void InitProjectCacheDir()
        {
            if (Directory.Exists(ProjectCacheDir))
            {
                Directory.Delete(ProjectCacheDir, true);
            }
            Directory.CreateDirectory(ProjectCacheDir);
        }
        private void InitProjectFileDir()
        {
            if (Directory.Exists(ProjectFileDir))
            {
                Directory.Delete(ProjectFileDir, true);
            }
            Directory.CreateDirectory(ProjectFileDir);
        }
        public static CSJProjectBuilder GetInstance()
        {
            if (Instance == null)
            {
                lock (SingleKey)
                {
                    if (Instance == null)
                    {
                        Instance = new CSJProjectBuilder();
                    }
                }
            }
            return Instance;
        }

        public bool BuildProjects(MainFormInterface mainFormInterface)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                InitProjectFileDir();
                InitProjectCacheDir();
                MainFormInterface = mainFormInterface;
                stopwatch.Start();
                for (int sceneNo = 0; sceneNo < MainFormInterface.GetSceneCount(); sceneNo++)
                {
                    if (sceneNo == 10)
                    {
                        stopwatch.Stop();
                        Console.WriteLine("----------------------------------------------------------- 耗时：" + stopwatch.ElapsedMilliseconds.ToString());
                    }
                    bool result = BuildProject(sceneNo, mainFormInterface);
                    if (!result)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return false;
        }

        private bool BuildProject(int sceneNo,MainFormInterface mainFormInterface)
        {
            try
            {
                InitChannelTaskState();
                CurrentSceneNo = sceneNo;
                MainFormInterface = mainFormInterface;
                for (int i = 0; i < 512; i++)
                {
                    var channelNo = i + 1;
                    lock (MultipartChannelBasicTaskState)
                    {
                        MultipartChannelBasicTaskState.TryAdd(channelNo, false);
                        MultipartChannelMusicTaskState.TryAdd(channelNo, false);
                    }
                }
                for (int i = 0; i < 512; i += 16)
                {
                    var startChannelNo = i + 1;
                    Thread thread = new Thread(() => MultipartChannelTask(startChannelNo, sceneNo));
                    thread.Start();
                }
                while (!SceneBasicTaskState | !SceneMusicTaskState)
                {
                    Thread.Sleep(100);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return false;
        }

        //TODO 有待测试多线程下情况
        private void MultipartChannelTask(int startChannelNo,int currentSceneNo)
        {
            //常规场景数据处理
            for (int i = 0; i < 16; i++)
            {
                var channelNo = startChannelNo + i;
                ChannelBasicTask(channelNo);
            }
            lock (BasicTaskKey)
            {
                if (!MultipartChannelBasicTaskState.Values.Contains(false))
                {
                    SceneBasicTaskCompleted();
                }
            }
            //音频场景数据处理
            for (int i = 0; i < 16; i++)
            {
                var channelNo = startChannelNo + i;
                ChannelMusicTask(channelNo);
            }
            lock (MusicTaskKey)
            {
                if (!MultipartChannelMusicTaskState.Values.Contains(false))
                {
                    SceneMusicTaskCompleted();
                }
            }
        }

        private void ChannelBasicTask(int channelNo)
        {
            DB_FineTune fineTune = null;
            foreach (var item in MainFormInterface.GetFineTunes())
            {
                if (item.FineTuneIndex == channelNo)
                {
                    item.MaxValue = item.MaxValue == 0 ? 255 : item.MaxValue;
                    fineTune = item;
                    break;
                }
            }
            DB_ChannelPK pk = new DB_ChannelPK()
            {
                LightID = 0,
                ChannelID = fineTune == null ? channelNo : fineTune.MainIndex ,
                Scene = CurrentSceneNo,
                Mode = BASIC_MODE
            };
            List<byte> writeBuff = new List<byte>();
            var channelValues = MainFormInterface.GetSMTDList(pk);
            TongdaoWrapper firstStepInfo = null;
            int startValue = 0;
            float inc;
            if (channelValues.Count > 0)
            {
                foreach (var item in channelValues)
                {
                    if (item.ChangeMode != 0 && item.ChangeMode != 1)//0-跳变，1-渐变，2-屏蔽
                    {
                        continue;
                    }
                    if (firstStepInfo == null)
                    {
                        firstStepInfo = item;
                        startValue = item.ScrollValue;
                        writeBuff.Add(Convert.ToByte(startValue));
                    }
                    else
                    {
                        if (item.ChangeMode == 1)//渐变
                        {
                            inc = (item.ScrollValue - startValue) / item.StepTime;
                            for (int i = 0; i < item.StepTime; i++)
                            {
                                var value = startValue + (i + 1) * inc;
                                value = value < 0 ? 0 : value;
                                value = value > 255 ? 255 : value;
                                int intValue = (int)Math.Floor(value * 256);
                                writeBuff.Add(Convert.ToByte(fineTune == null ? (intValue >> 8) & 0xFF : (intValue & 0xFF) / (255 / fineTune.MaxValue)));
                            }
                        }
                        else if (item.ChangeMode == 0)//跳变
                        {
                            writeBuff.AddRange(Enumerable.Repeat<byte>(fineTune == null ? Convert.ToByte(item.ScrollValue) : Convert.ToByte(0x00), item.StepTime));
                        }
                        startValue = item.ScrollValue;
                    }
                }
                if (firstStepInfo.ChangeMode == 1)//渐变
                {
                    inc = (firstStepInfo.ScrollValue - startValue) / firstStepInfo.StepTime;
                    for (int i = 0; i < firstStepInfo.StepTime - 1; i++)
                    {
                        var value = startValue + (i + 1) * inc;
                        value = value < 0 ? 0 : value;
                        value = value > 255 ? 255 : value;
                        int intValue = (int)Math.Floor(value * 256);
                        writeBuff.Add(Convert.ToByte(fineTune == null ? (intValue >> 8) & 0xFF : (intValue & 0xFF) / (255 / fineTune.MaxValue)));
                    }
                }
                else//跳变
                {
                    writeBuff.AddRange(Enumerable.Repeat<byte>(fineTune == null ? Convert.ToByte(firstStepInfo.ScrollValue) : Convert.ToByte(0x00), firstStepInfo.StepTime - 1));
                }
                //写入缓存文件
                if (writeBuff.Count > 0)
                {
                    var cachePath = ProjectCacheDir + @"\S" + CurrentSceneNo + @"C" + channelNo + @".cache";
                    using (FileStream writeStream = new FileStream(cachePath, FileMode.Create))
                    {
                        writeStream.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                    }
                    writeBuff.Clear();
                }
            }
            lock (BasicTaskKey)
            {
                MultipartChannelBasicTaskState[channelNo] = true;
            }
        }

        private void ChannelMusicTask(int channelNo)
        {
            DB_ChannelPK pk = new DB_ChannelPK()
            {
                LightID = 0,
                ChannelID = channelNo,
                Scene = CurrentSceneNo,
                Mode = MUSIC_MODE
            };
            List<byte> writeBuff = new List<byte>();
            var channelValues = MainFormInterface.GetSMTDList(pk);
            TongdaoWrapper firstStep = null;
            if (channelValues.Count > 0)
            {
                foreach (var item in channelValues)
                {
                    if (item.ChangeMode != 1)//0-屏蔽，1-跳变，2-渐变
                    {
                        continue;
                    }
                    if (firstStep == null)
                    {
                        firstStep = item;
                        writeBuff.Add(Convert.ToByte(item.ScrollValue));
                    }
                    writeBuff.AddRange(Enumerable.Repeat<byte>(Convert.ToByte(item.ScrollValue), item.StepTime));
                }
                writeBuff.AddRange(Enumerable.Repeat<byte>(Convert.ToByte(firstStep.ScrollValue), firstStep.StepTime));
                //写入缓存文件
                if (writeBuff.Count > 0)
                {
                    var cachePath = ProjectCacheDir + @"\S" + CurrentSceneNo + @"M" + channelNo + @".cache";
                    using (FileStream writeStream = new FileStream(cachePath, FileMode.Create))
                    {
                        writeStream.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                    }
                    writeBuff.Clear();
                }
            }
            lock (MusicTaskKey)
            {
                MultipartChannelMusicTaskState[channelNo] = true;
            }
        }

        private void SceneBasicTaskCompleted()
        {
            Dictionary<int, string> cachePaths = new Dictionary<int, string>();
            List<byte> writeBuff = new List<byte>();
            string projectFilePath = ProjectFileDir + @"\C" + CurrentSceneNo + @".bin";
            long seek = 0;
            for (int i = 0; i < 512; i++)
            {
                int channelNo = i + 1;
                var cachePath = ProjectCacheDir + @"\S" + CurrentSceneNo + @"C" + channelNo + @".cache";
                if (File.Exists(cachePath))
                {
                    cachePaths.Add(channelNo,cachePath);
                }
            }
            if (cachePaths.Count > 0)
            {
                //TODO 后续要把全局配置信息解析封装成实体，便于后续读取
                using (var reader = new StreamReader(MainFormInterface.GetConfigPath()))
                {
                    string lineStr = "";
                    string strValue = "";
                    int micSensor = 0;
                    int senseFreq = 0;
                    int intValue = 0;
                    int runTime = 0;
                    while ((lineStr = reader.ReadLine()) != null)
                    {
                        if (lineStr.Equals("[YM]"))
                        {
                            for (int i = 0; i < MainFormInterface.GetSceneCount(); i++)
                            {
                                lineStr = reader.ReadLine();
                                if (lineStr.StartsWith(CurrentSceneNo + "CK"))
                                {
                                    strValue = lineStr.Split('=')[1];
                                    int.TryParse(strValue, out intValue);
                                    micSensor = intValue;
                                }
                                lineStr = reader.ReadLine();
                                if (lineStr.StartsWith(CurrentSceneNo + "JG"))
                                {
                                    strValue = lineStr.Split('=')[1];
                                    int.TryParse(strValue, out intValue);
                                    senseFreq = intValue;
                                }
                                lineStr = reader.ReadLine();
                                if (lineStr.StartsWith(CurrentSceneNo + "ZX"))
                                {
                                    strValue = lineStr.Split('=')[1];
                                    int.TryParse(strValue, out intValue);
                                    runTime = intValue;
                                }
                            }
                            break;
                        }
                    }
                    writeBuff.AddRange(Enumerable.Repeat<byte>(Convert.ToByte(0x00),4));
                    writeBuff.Add(Convert.ToByte(micSensor));
                    writeBuff.Add(Convert.ToByte(((senseFreq) * 60) & 0xFF));
                    writeBuff.Add(Convert.ToByte((((senseFreq) * 60) >> 8) & 0xFF));
                    writeBuff.Add(Convert.ToByte((runTime) & 0xFF));
                    writeBuff.Add(Convert.ToByte(((runTime) >> 8) & 0xFF));
                    writeBuff.Add(Convert.ToByte((cachePaths.Count) & 0xFF));
                    writeBuff.Add(Convert.ToByte(((cachePaths.Count) >> 8) & 0xFF));
                }
                seek = writeBuff.Count;
                foreach (var item in cachePaths.Keys)
                {
                    using (FileStream readStream = new FileStream(cachePaths[item],FileMode.Open))
                    {
                        seek += 8;
                        writeBuff.Add(Convert.ToByte(item & 0xFF));
                        writeBuff.Add(Convert.ToByte((item >> 8) & 0xFF));
                        writeBuff.Add(Convert.ToByte(readStream.Length & 0xFF));
                        writeBuff.Add(Convert.ToByte((readStream.Length >> 8) & 0xFF));
                        writeBuff.Add(Convert.ToByte(seek & 0xFF));
                        writeBuff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                        writeBuff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                        writeBuff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                        seek += readStream.Length;
                        byte[] readBuff = new byte[readStream.Length];
                        readStream.Read(readBuff, 0, readBuff.Length);
                        writeBuff.AddRange(readBuff);
                    }
                }
                writeBuff[0] = Convert.ToByte(writeBuff.Count & 0xFF);
                writeBuff[1] = Convert.ToByte((writeBuff.Count >> 8) & 0xFF);
                writeBuff[2] = Convert.ToByte((writeBuff.Count >> 16) & 0xFF);
                writeBuff[3] = Convert.ToByte((writeBuff.Count >> 24) & 0xFF);
                using (FileStream writeStream = new FileStream(projectFilePath,FileMode.Create))
                {
                    writeStream.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                }
            }
            Console.WriteLine("##########################   场景" + CurrentSceneNo + "工程生成完成,当前线程ID ：" + Thread.CurrentThread.ManagedThreadId);
            SceneBasicTaskState = true;
        }

        private void SceneMusicTaskCompleted()
        {
            Dictionary<int, string> cachePaths = new Dictionary<int, string>();
            List<byte> writeBuff = new List<byte>();
            string projectFilePath = ProjectFileDir + @"\M" + CurrentSceneNo + @".bin";
            long seek = 0;
            for (int i = 0; i < 512; i++)
            {
                int channelNo = i + 1;
                var cachePath = ProjectCacheDir + @"\S" + CurrentSceneNo + @"M" + channelNo + @".cache";
                if (File.Exists(cachePath))
                {
                    cachePaths.Add(channelNo, cachePath);
                }
            }
            if (cachePaths.Count > 0)
            {
                //TODO 后续要把全局配置信息解析封装成实体，便于后续读取
                using (StreamReader reader = new StreamReader(MainFormInterface.GetConfigPath()))
                {
                    List<int> stepList = new List<int>();
                    int frameTime = 0;
                    int musicIntervalTime = 0;
                    string lineStr;
                    string strValue = string.Empty;
                    int intValue;
                    while (true)
                    {
                        lineStr = reader.ReadLine();
                        if (lineStr.Equals("[SK]"))
                        {
                            for (int i = 0; i < MainFormInterface.GetSceneCount(); i++)
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
                                if (sceneNumber.Equals(CurrentSceneNo.ToString()))
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
                    writeBuff.AddRange(Enumerable.Repeat<byte>(Convert.ToByte(0x00), 4));
                    writeBuff.Add(Convert.ToByte(frameTime));
                    writeBuff.Add(Convert.ToByte(musicIntervalTime & 0xFF));
                    writeBuff.Add(Convert.ToByte((musicIntervalTime >> 8) & 0xFF));
                    writeBuff.Add(Convert.ToByte(stepList.Count));
                    for (int i = 0; i < stepList.Count; i++)
                    {
                        writeBuff.Add(Convert.ToByte(stepList[i]));
                    }
                    writeBuff.AddRange(Enumerable.Repeat<byte>(Convert.ToByte(0x00), STEPLISTSIZE - stepList.Count));
                    writeBuff.Add(Convert.ToByte(cachePaths.Count & 0xFF));
                    writeBuff.Add(Convert.ToByte((cachePaths.Count >> 8) & 0xFF));
                }
                seek = writeBuff.Count;
                foreach (var item in cachePaths.Keys)
                {
                    using (FileStream readStream = new FileStream(cachePaths[item], FileMode.Open))
                    {
                        seek += 8;
                        writeBuff.Add(Convert.ToByte(item & 0xFF));
                        writeBuff.Add(Convert.ToByte((item >> 8) & 0xFF));
                        writeBuff.Add(Convert.ToByte(readStream.Length & 0xFF));
                        writeBuff.Add(Convert.ToByte((readStream.Length >> 8) & 0xFF));
                        writeBuff.Add(Convert.ToByte(seek & 0xFF));
                        writeBuff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                        writeBuff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                        writeBuff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                        seek += readStream.Length;
                        byte[] readBuff = new byte[readStream.Length];
                        readStream.Read(readBuff, 0, readBuff.Length);
                        writeBuff.AddRange(readBuff);
                    }
                }
                writeBuff[0] = Convert.ToByte(writeBuff.Count & 0xFF);
                writeBuff[1] = Convert.ToByte((writeBuff.Count >> 8) & 0xFF);
                writeBuff[2] = Convert.ToByte((writeBuff.Count >> 16) & 0xFF);
                writeBuff[3] = Convert.ToByte((writeBuff.Count >> 24) & 0xFF);
                using (FileStream writeStream = new FileStream(projectFilePath, FileMode.Create))
                {
                    writeStream.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                }
            }
            SceneMusicTaskState = true;
        }
    }
}
