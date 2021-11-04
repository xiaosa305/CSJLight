using LightController.MyForm;
using LightController.PeripheralDevice;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
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
        public void StartDebug(NetworkConnect connect, Completed completed, Error error)
        {
            Connect = connect;
            Connect.StartIntentPreview(FrameIntervalTime, delegate { completed(); }, delegate {error("启动调试失败");});
        }
        public void StopDebug(Completed completed, Error error)
        {
            Connect.StopIntentPreview(delegate {completed();}, error);
            Connect = null;
        }
        public void Preview(MainFormInterface mainFormInterface)
        {
            if (SingleStepPlayTimer.Enabled)
            {
                SingleStepPlayTimer.Stop();
                Thread.Sleep(100);
            }
            if (PlayTimer.Enabled)
            {
                PlayTimer.Stop();
                Thread.Sleep(100);
            }
            MainFormInterface = mainFormInterface;
            SetFrameIntervalTime();
            PlayTimer.Interval = FrameIntervalTime;
            Group = new ChannelGroup(MainFormInterface);
            if (!PlayTimer.Enabled)
            {
                PlayTimer.Start();
            }
        }
        public void EndPreview()
        {
            if (PlayTimer.Enabled)
            {
                PlayTimer.Stop();
            }
            if (SingleStepPlayTimer.Enabled)
            {
                SingleStepPlayTimer.Stop();
            }
        }
        public void SingleStepPreview(byte[] data,MainFormInterface mainFormInterface)
        {
            if (PlayTimer.Enabled)
            {
                PlayTimer.Stop();
                Thread.Sleep(100);
            }
            MainFormInterface = mainFormInterface;
            SetFrameIntervalTime();
            lock (SingleStepDmxData)
            {
                List<byte> buff = new List<byte>();
                buff.Add(Convert.ToByte(0x00));
                buff.AddRange(data);
                SingleStepDmxData = buff.ToArray();
            }
            if (!SingleStepPlayTimer.Enabled)
            {
                SingleStepPlayTimer.Start();
            }
        }
        public bool TriggerAudio()
        {
            return Group.MusicControl();
        }
        public bool MusicControlState()
        {
            return Group.MusicControlState && Group.IsMusicMode;
        }
        private void PlayTask(byte[] dmxData)
        {
            if (Connect != null)
            {
                Connect.IntentPreview(Connect.DeviceIp, dmxData);
            }
        }
    }
}
