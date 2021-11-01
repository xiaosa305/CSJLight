using LightController.MyForm;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        private ConcurrentDictionary<int, byte> MusicDataBuff;
        private List<int> MusicStepList;
        private int MusicStepListPoint;
        private int MusicWaitTime;
        private int MusicControlTime;
        private int MusicStep;
        private int CurrentSceneNo;
        public bool MusicControlState { get; set; }
        public bool IsMusicMode { get; set; }
        private bool IsNoEmptyBasic;
        


        public ChannelGroup(MainFormInterface mainFormInterface,int sceneNo)
        {
            MainFormInterface = mainFormInterface;
            CurrentSceneNo = sceneNo;
            Init();
            ReadMusicConfigInfo();
            InitMusicControlTimer();
            InitMusicWaitControlTimer();
            BuildChannels(sceneNo);
        }
        private void Init()
        {
            BasicChannels = new Dictionary<int, Channel>();
            MusicChannels = new Dictionary<int, Channel>();
            MusicDataBuff = new ConcurrentDictionary<int, byte>();
            MusicStepList = new List<int>();
            MusicControlState = false;
            MusicStepListPoint = 0;
            IsMusicMode = false;
            IsNoEmptyBasic = false;
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
                        foreach (var key in MusicChannels.Keys)
                        {
                            MusicDataBuff.TryAdd(key, MusicChannels[key].TakeDmxData());
                        }
                        MusicControlState = true;
                        Thread.Sleep(MusicControlTime);
                    }
                    MusicStepListPoint++;
                    MusicStepListPoint = MusicStepListPoint == MusicStepList.Count ? 0 : MusicStepListPoint;
                    MusicWaitControlTimer.Start();
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
                MusicControlState = false;
            };
        }
        private void ReadMusicConfigInfo()
        {
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
                            string sceneNumber;
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
                                reader.ReadLine();
                                reader.ReadLine();
                            }
                        }
                        break;
                    }
                }
                MusicControlTime = frameTime;
                MusicWaitTime = musicIntervalTime;
                MusicStepList = stepList;
            }
        }
        private void BuildChannels(int currentSceneNo)
        {
            BuildBasicChannels(currentSceneNo);
            BuildMusicChannels(currentSceneNo);
        }
        private void BuildBasicChannels(int currentSceneNo)
        {
            foreach (var item in MainFormInterface.GetChannelIDList())
            {
                Channel channel = new Channel(item, currentSceneNo, BASIC_MODE, MainFormInterface);
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
        private void BuildMusicChannels(int currentSceneNo)
        {
            foreach (var item in MainFormInterface.GetChannelIDList())
            {
                Channel channel = new Channel(item, currentSceneNo, MUSIC_MODE, MainFormInterface);
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
                if (MusicControlTimer.Enabled)
                {
                    return false;
                }
                else
                {
                    if (MusicWaitControlTimer.Enabled)
                    {
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
                if (MusicControlState)
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
