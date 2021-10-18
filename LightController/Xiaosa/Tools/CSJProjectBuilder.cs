using LightController.Ast;
using LightController.Entity;
using LightController.MyForm;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
        private static int BASIC_MODE = 0;
        private static int MUSIC_MODE = 1;
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
        public static CSJProjectBuilder GetInstance()
        {
            if (Instance == null)
            {
                lock (Instance)
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
                MainFormInterface = mainFormInterface;
                for (int sceneNo = 0; sceneNo < MainFormInterface.GetSceneCount(); sceneNo++)
                {
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

        public bool BuildProject(int sceneNo,MainFormInterface mainFormInterface)
        {
            try
            {
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
                    var channelNo = i + 1;
                    Thread thread = new Thread(() => MultipartChannelTask(channelNo, sceneNo));
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

        private void MultipartChannelTask(int startChannelNo,int currentSceneNo)
        {
            //常规场景数据处理
            for (int i = 0; i < 16; i++)
            {
                var channelNo = startChannelNo + i;
                ChannelBasicTask(channelNo);
            }
            lock (MultipartChannelBasicTaskState)
            {
                if (!MultipartChannelBasicTaskState.Values.Contains(false))
                {
                    SceneBasicTaskCompleted(currentSceneNo);
                }
            }
            //音频场景数据处理
            for (int i = 0; i < 16; i++)
            {
                var channelNo = startChannelNo + i;
                ChannelMusicTask(channelNo);
            }
            lock (MultipartChannelMusicTaskState)
            {
                if (!MultipartChannelMusicTaskState.Values.Contains(false))
                {
                    SceneMusicTaskCompleted(currentSceneNo);
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
            float inc = 0;
            foreach (var item in channelValues)
            {
                if (item.ChangeMode != 0 && item.ChangeMode != 1)
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

            lock (MultipartChannelBasicTaskState)
            {
                MultipartChannelBasicTaskState[channelNo] = true;
            }
        }

        private void ChannelMusicTask(int channelNo)
        {


            lock (MultipartChannelMusicTaskState)
            {
                MultipartChannelMusicTaskState[channelNo] = true;
            }
        }

        private void SceneBasicTaskCompleted(int currentSceneNo)
        {

        }

        private void SceneMusicTaskCompleted(int currentSceneNo)
        {

        }

    }
}
