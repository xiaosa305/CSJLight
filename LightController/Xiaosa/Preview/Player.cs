using LightController.MyForm;
using LightController.PeripheralDevice;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
        private System.Timers.Timer SingleStepPlayTimer;
        private NetworkConnect Connect;
        private byte[] SingleStepDmxData;
        private Player()
        {
            Init();
            InitPlayTimer();
            InitSingleStepPlayTimer();
        }
        private void Init()
        {
            FrameIntervalTime = 40;
            SingleStepDmxData = Enumerable.Repeat<byte>(Convert.ToByte(0x00), 513).ToArray();
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
                PlayTask(Group.ReadDMXData());
            };
        }
        private void InitSingleStepPlayTimer()
        {
            SingleStepPlayTimer = new System.Timers.Timer()
            {
                AutoReset = true,
                Interval = 40
            };
            SingleStepPlayTimer.Elapsed += delegate
            {
                PlayTask(SingleStepDmxData);
                
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
            if (SingleStepPlayTimer.Enabled)
            {
                SingleStepPlayTimer.Stop();
                Thread.Sleep(100);
            }
            MainFormInterface = mainFormInterface;
            SetFrameIntervalTime();
            PlayTimer.Interval = FrameIntervalTime;
            Connect = connect;
            Group = new ChannelGroup(MainFormInterface, sceneNo);
            Connect.StartIntentPreview(FrameIntervalTime, delegate { Console.WriteLine("Preview Success"); ; PlayTimer.Start(); completed(); }, delegate { Console.WriteLine("Preview Failed"); error("启动调试失败");});
        }
        public void EndPreview(Completed completed, Error error)
        {
            if (PlayTimer.Enabled)
            {
                PlayTimer.Stop();
            }
            if (SingleStepPlayTimer.Enabled)
            {
                SingleStepPlayTimer.Stop();
            }
            if (Connect.IsConnected())
            {
                Connect.StopIntentPreview(delegate { completed(); }, delegate { error("关闭调试失败"); });
            }
        }
        public void SingleStepPreview(byte[] data)
        {
            if (PlayTimer.Enabled)
            {
                PlayTimer.Stop();
                Thread.Sleep(100);
            }
            SetFrameIntervalTime();
            SingleStepDmxData = data;
            SingleStepPlayTimer.Start();
        }
        public bool TriggerAudio()
        {
            return Group.MusicControl();
        }
        private void PlayTask(byte[] dmxData)
        {
            Connect.IntentPreview(Connect.DeviceIp, dmxData);
        }
    }
}
