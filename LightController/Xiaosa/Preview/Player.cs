using LightController.MyForm;
using LightController.PeripheralDevice;
using LightController.Tools.CSJ.IMPL;
using System;
using static LightController.Xiaosa.Entity.CallBackFunction;

namespace LightController.Xiaosa.Preview
{
    public class Player
    {
        private static Player Instance;
        private static readonly Object SingleKey = new object();
        private MainFormInterface MainFormInterface;
        private ChannelGroup Group;
        private int FrameIntervalTime;
        private System.Timers.Timer PlayTimer;
        private NetworkConnect Connect;
        private Player()
        {
            Init();
            InitPlayTimer();
        }
        private void Init()
        {
            FrameIntervalTime = 40;
        }
        public void SetFrameIntervalTime()
        {
            FrameIntervalTime = new CSJ_Config(MainFormInterface.GetLights(), MainFormInterface.GetConfigPath()).TimeFactory;
        }
        private void InitPlayTimer()
        {
            PlayTimer = new System.Timers.Timer
            {
                AutoReset = true,
                Interval = FrameIntervalTime
            };
            PlayTimer.Elapsed += delegate
            {
                PlayTask();
            };
        }
        public static Player GetPlayer()
        {
            if (Instance == null)
            {
                lock (SingleKey)
                {
                    if (Instance == null)
                    {
                        Instance = new Player();
                    }
                }
            }
            return Instance;
        }
        public void Preview(NetworkConnect connect,MainFormInterface mainFormInterface,int sceneNo,Completed completed,Error error)
        {
            MainFormInterface = mainFormInterface;
            SetFrameIntervalTime();
            Connect = connect;
            Group = new ChannelGroup(MainFormInterface, sceneNo);
            Connect.StartIntentPreview(FrameIntervalTime, delegate { PlayTimer.Start();completed(); }, delegate { error("启动调试失败");});
        }
        public void EndPreview(Completed completed,Error error)
        {
            PlayTimer.Stop();
            Connect.StopIntentPreview(delegate { completed(); }, delegate { error("关闭调试失败");});
        }
        public bool TriggerAudio()
        {
            return Group.MusicControl();
        }
        private void PlayTask()
        {
            Connect.IntentPreview(Connect.DeviceIp, Group.ReadDMXData());
        }
    }
}
