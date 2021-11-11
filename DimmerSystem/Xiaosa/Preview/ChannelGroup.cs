using LightController.MyForm;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace LightController.Xiaosa.Preview
{
    public class ChannelGroup
    {
        private const int BASIC_MODE = 0;
        private const int MUSIC_MODE = 1;
        private MainFormInterface MainFormInterface;
        private System.Timers.Timer MusicWaitControlTimer;
        private System.Timers.Timer MusicControlTimer;
        private Dictionary<int, Channel> BasicChannels;
        private Dictionary<int, Channel> MusicChannels;
        private Dictionary<int, byte> MusicDataBuff;
        private List<int> MusicStepList;
        private int MusicStepListPoint;
        private int MusicWaitTime;
        private int MusicControlTime;
        private int MusicStep;
        public bool MusicControlState { get; set; }
        private bool MusicWaitState;
        public bool IsMusicMode { get; set; }
        private bool IsNoEmptyBasic;
        public ChannelGroup(MainFormInterface mainFormInterface)
        {
            MainFormInterface = mainFormInterface;
            Init();
            ReadMusicConfigInfo();
            InitMusicControlTimer();
            InitMusicWaitControlTimer();
            BuildChannels();
        }
        private void Init()
        {
            BasicChannels = new Dictionary<int, Channel>();
            MusicChannels = new Dictionary<int, Channel>();
            MusicDataBuff = new Dictionary<int, byte>();
            MusicStepList = new List<int>();
            MusicControlState = false;
            MusicStepListPoint = 0;
            IsMusicMode = false;
            IsNoEmptyBasic = false;
            MusicWaitState = false;
        }
        private void InitMusicControlTimer()
        {
            MusicControlTimer = new System.Timers.Timer();
            MusicControlTimer.Interval = 1;
            MusicControlTimer.AutoReset = false;
            MusicControlTimer.Elapsed += delegate
            {
                if (IsMusicMode)
                {
                    MusicStep = MusicStepList[MusicStepListPoint];
                    for (int i = 0; i < MusicStep; i++)
                    {
                        lock (MusicDataBuff)
                        {
                            foreach (var key in MusicChannels.Keys)
                            {
                                byte value = MusicChannels[key].TakeDmxData();
                                if (MusicDataBuff.ContainsKey(key))
                                {
                                    MusicDataBuff[key] = value;
                                }
                                else
                                {
                                    MusicDataBuff.Add(key, value);
                                }
                            }
                        }
                        MusicControlState = true;
                        Thread.Sleep(MusicControlTime);
                    }
                    MusicStepListPoint++;
                    MusicStepListPoint = MusicStepListPoint == MusicStepList.Count ? 0 : MusicStepListPoint;
                    MusicWaitState = true;
                    MusicWaitControlTimer.Start();
                    MusicControlState = false;
                }
            };
        }
        private void InitMusicWaitControlTimer()
        {
            MusicWaitControlTimer = new System.Timers.Timer();
            MusicWaitControlTimer.Interval = MusicWaitTime;
            MusicWaitControlTimer.AutoReset = false;
            MusicWaitControlTimer.Elapsed += delegate
            {
                MusicWaitState = false;
            };
        }
        private void ReadMusicConfigInfo()
        {
            MusicControlTime = MainFormInterface.GetPreviewMusicControlTime();
            MusicWaitTime = MainFormInterface.GetPreviewMusicWaitTime();
            MusicStepList = new List<int>();
            foreach (var item in MainFormInterface.GetPreviewMusicStepList())
            {
                if (item != 0)
                {
                    MusicStepList.Add(item);
                }
            }
        }
        private void BuildChannels()
        {
            BuildBasicChannels();
            BuildMusicChannels();
        }
        private void BuildBasicChannels()
        {
            foreach (var item in MainFormInterface.GetPreviewChannelIDList())
            {
                Channel channel = new Channel(item, BASIC_MODE, MainFormInterface);
                if (channel.IsNoEmpty())
                {
                    BasicChannels.Add(item, channel);
                }
            }
            if (BasicChannels.Count > 0)
            {
                IsNoEmptyBasic = true;
            }
        }
        private void BuildMusicChannels()
        {
            foreach (var item in MainFormInterface.GetPreviewChannelIDList())
            {
                Channel channel = new Channel(item, MUSIC_MODE, MainFormInterface);
                if (channel.IsNoEmpty())
                {
                    MusicChannels.Add(item, channel);
                }
            }
            if (MusicChannels.Count > 0)
            {
                IsMusicMode = true;
            }
        }
        public bool MusicControl()
        {
            if (IsMusicMode && IsNoEmptyBasic)
            {
                if (MusicControlState)
                {
                    return false;
                }
                else
                {
                    if (MusicWaitState)
                    {
                        MusicWaitState = false;
                        MusicWaitControlTimer.Stop();
                    }
                    MusicControlTimer.Start();
                }
                return true;
            }
            return false;
        }
        public byte[] ReadDMXData()
        {
            byte[] dmxData = Enumerable.Repeat<byte>(Convert.ToByte(0x00), 513).ToArray();
            if (IsNoEmptyBasic)
            {
                foreach (var key in BasicChannels.Keys)
                {
                    dmxData[key] = BasicChannels[key].TakeDmxData();
                }
                if (MusicControlState || MusicWaitState)
                {
                    foreach (var key in MusicDataBuff.Keys)
                    {
                        dmxData[key] = MusicDataBuff[key];
                    }
                }
            }
            return dmxData;
        }
    }
}
