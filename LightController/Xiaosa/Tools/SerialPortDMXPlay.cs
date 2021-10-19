using LightController.Ast;
using LightController.MyForm;
using LightController.PeripheralDevice;
using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace LightController.Xiaosa.Tools
{
    public class SerialPortDMXPlay
    {
        private static SerialPortDMXPlay Instance { get; set; }
        private readonly byte[] StartCode = new byte[] { 0x00 };
        private byte[] PlayData { get; set; }
        private CSJ_Config Config { get; set; }
        private int SceneNo { get; set; }
        private int TimeFactory { get; set; }
        private bool IsMusicControl { get; set; }
        private bool MusicData { get; set; }
        private bool MusicWaiting { get; set; }
        private int MusicStep { get; set; }
        private int MusicStepTime { get; set; }
        private int[] StepList { get; set; }
        private int StepListCount { get; set; }
        private int MusicIntervalTime { get; set; }
        private int MusicStepPoint { get; set; }
        private static int PreviewTimerStatus = 0;
        private static int MusicControlTimerStatus = 0;
        private static int SendTimerStatus = 0;
        private System.Timers.Timer PreviewTimer { get; set; }
        private System.Timers.Timer MusicControlTimer { get; set; }
        private System.Timers.Timer SendTimer { get; set; }
        private System.Timers.Timer Timer { get; set; }
        private Dictionary<int, byte> MusicDataBuff { get; set; }
        private List<PlayPoint> M_PlayPoints { get; set; }
        private List<PlayPoint> C_PlayPoints { get; set; }
        private SerialPort COM { get; set; }
        private MainFormInterface MainFormInterface;
        private SerialPortDMXPlay()
        {
            try
            {
                this.MusicDataBuff = new Dictionary<int, byte>();
                this.PlayData = Enumerable.Repeat(Convert.ToByte(0x00), 512).ToArray();
                this.TimeFactory = 32;
                this.MusicStepTime = 0;
                this.COM = new SerialPort();
                this.COM.DataBits = 8;
                this.COM.StopBits = StopBits.Two;
                this.COM.Parity = Parity.None;
                this.COM.BaudRate = 250000;
                this.Timer = new System.Timers.Timer
                {
                    AutoReset = false,
                };
                this.Timer.Elapsed += this.MusicWaitingHandl;
                this.MusicWaiting = true;
                this.PreviewTimer = new System.Timers.Timer
                {
                    Interval = this.TimeFactory,
                    AutoReset = true
                };
                this.PreviewTimer.Elapsed += this.PreviewOnTimer;
                this.SendTimer = new System.Timers.Timer()
                {
                    Interval = this.TimeFactory,
                    AutoReset = true
                };
                this.SendTimer.Elapsed += this.SendOnTimer;
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "初始化播放工具失败", ex);
            }

        }
        public static SerialPortDMXPlay GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SerialPortDMXPlay();
            }
            return Instance;
        }
        public void EndView()
        {
            try
            {
                this.PreviewTimer.Stop();
                this.MusicWaiting = true;
                this.IsMusicControl = false;
                this.MusicStepPoint = 0;
                this.ResetDebugDataToEmpty();
                if (!this.SendTimer.Enabled)
                {
                    this.SendTimer.Start();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "结束预览出错", ex);
            }
        }
        public void ResetDebugDataToEmpty()
        {
            lock (this.PlayData)
            {
                this.PlayData = Enumerable.Repeat(Convert.ToByte(0x00), 512).ToArray();
            }
            if (this.SendTimer != null)
            {
                this.SendTimer.Stop();
                this.Play();
                this.SendTimer.Start();
            }
        }
        public void PreView(MainFormInterface mainFormInterface, string configPath, int sceneNo)
        {
            try
            {
                if (!FileUtils.IsDefaultFile())
                {
                    return;
                }
                this.MusicData = false;
                this.SceneNo = sceneNo;
                MainFormInterface = mainFormInterface;
                this.Config = new CSJ_Config(mainFormInterface.GetLights(), configPath);
                this.C_PlayPoints = FileUtils.GetCPlayPoints();
                this.TimeFactory = Config.TimeFactory;
                if (FileUtils.IsMusicFile())
                {
                    this.MusicData = true;
                    this.MusicStepPoint = 0;
                    this.MusicStepTime = FileUtils.GetMusicTime();
                    this.StepListCount = FileUtils.GetMusicStepCount();
                    this.MusicIntervalTime = FileUtils.GetMusicIntervalTime();
                    this.StepList = FileUtils.GetMusicStepList();
                    this.M_PlayPoints = FileUtils.GetMPlayPoints();
                }
                this.MusicStep = this.Config.Music_Control_Enable[SceneNo];
                if (this.PreviewTimer.Enabled)
                {
                    this.PreviewTimer.Stop();
                }
                this.PreviewTimer.Interval = this.TimeFactory;
                if (this.SendTimer.Enabled)
                {
                    this.SendTimer.Stop();
                }
                this.SendTimer.Interval = this.TimeFactory;
                this.PreviewTimer.Start();
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "实时预览发生错误", ex);
                this.EndView();
            }
        }
        public void OLOSView(byte[] data)
        {
            try
            {
                if (this.PreviewTimer.Enabled)
                {
                    this.PreviewTimer.Stop();
                }
                if (this.Config != null)
                {
                    this.TimeFactory = this.Config.TimeFactory;
                }
                else
                {
                    this.TimeFactory = 32;
                }
                lock (this.PlayData)
                {
                    this.PlayData = data;
                }
                this.SendTimer.Interval = this.TimeFactory;
                if (!this.SendTimer.Enabled)
                {
                    this.SendTimer.Start();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "单灯单步预览发生错误", ex);
                this.EndView();
            }
        }
        public void MusicControl()
        {
            try
            {
                if (this.Config.Music_Control_Enable[this.SceneNo] == 0 || !this.PreviewTimer.Enabled || !this.MusicData)
                {
                    return;
                }
                if (this.MusicWaiting)
                {
                    this.Timer.Stop();
                    if (this.MusicControlTimer == null)
                    {
                        this.MusicControlTimer = new System.Timers.Timer();
                        this.MusicControlTimer.Elapsed += this.MusicControlOnTimer;
                        this.MusicControlTimer.AutoReset = false;
                    }
                    if (!this.MusicControlTimer.Enabled)
                    {
                        this.MusicWaiting = false;
                        this.MusicControlTimer.Start();
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "音频控制触发预览发生错误", ex);
            }
        }

        /// <summary>
        /// 功能：获取音频触发状态
        /// </summary>
        /// <returns></returns>
        public bool GetMusicStatus()
        {
            try
            {
                if (this.Config.Music_Control_Enable[this.SceneNo] == 0 || !this.PreviewTimer.Enabled || !this.MusicData)
                {
                    return false;
                }
                if (!this.MusicWaiting)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                LogTools.Debug(Constant.TAG_XIAOSA, "SerialPortDMXPlay 触发音频失败");
            }
            return false;
        }
        private void MusicWaitingHandl(object sender, ElapsedEventArgs e)
        {
            this.IsMusicControl = false;
        }
        /// <summary>
        /// 功能：预览定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewOnTimer(object sender, ElapsedEventArgs e)
        {
            if (Interlocked.Exchange(ref PreviewTimerStatus, 1) == 0)
            {
                this.PlayData = Enumerable.Repeat(Convert.ToByte(0x00), 512).ToArray();
                foreach (PlayPoint item in C_PlayPoints)
                {
                    this.PlayData[item.ChannelNo - 1] = item.Read();
                }
                if (IsMusicControl)
                {
                    lock (this.MusicDataBuff)
                    {
                        List<int> keys = MusicDataBuff.Keys.ToList();
                        foreach (int item in keys)
                        {
                            this.PlayData[item - 1] = MusicDataBuff[item];
                        }
                    }
                }
                if (this.SendTimer.Enabled)
                {
                    this.SendTimer.Stop();
                }
                this.Play();
                Interlocked.Exchange(ref PreviewTimerStatus, 0);
            }
        }
        /// <summary>
        /// 功能：音频触发定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MusicControlOnTimer(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (Interlocked.Exchange(ref MusicControlTimerStatus, 1) == 0)
                {
                    if (this.MusicStepPoint == this.StepListCount)
                    {
                        this.MusicStepPoint = 0;
                    }

                    this.MusicStep = this.StepList[this.MusicStepPoint++];
                    for (int i = 0; i < this.MusicStep; i++)
                    {
                        lock (this.MusicDataBuff)
                        {
                            this.MusicDataBuff.Clear();
                            foreach (PlayPoint item in M_PlayPoints)
                            {
                                this.MusicDataBuff.Add(item.ChannelNo, item.Read());
                            }
                        }
                        this.IsMusicControl = true;
                        Thread.Sleep(this.TimeFactory * this.MusicStepTime);
                    }
                    if (this.MusicIntervalTime != 0)
                    {
                        this.Timer.Interval = this.MusicIntervalTime;
                        this.Timer.Start();
                        this.MusicWaiting = true;
                    }
                    else
                    {
                        this.IsMusicControl = false;
                        this.MusicWaiting = true;
                    }
                    Interlocked.Exchange(ref MusicControlTimerStatus, 0);
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "音频控制数据叠加发生错误", ex);
            }
        }
        /// <summary>
        /// 发送数据定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendOnTimer(object sender, ElapsedEventArgs e)
        {
            if (Interlocked.Exchange(ref SendTimerStatus, 1) == 0)
            {
                this.Play();
                Interlocked.Exchange(ref SendTimerStatus, 0);
            }
        }
        private void Play()
        {
            try
            {
                List<byte> buff = new List<byte>();
                buff.AddRange(this.StartCode);
                lock (this.PlayData)
                {
                    buff.AddRange(this.PlayData);
                }
                if (this.COM.IsOpen)
                {
                    this.COM.BreakState = true;
                    Thread.Sleep(10);
                    this.COM.BreakState = false;
                    this.COM.Write(buff.ToArray(), 0, buff.Count);
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "播放数据输出发生错误", ex);
                this.EndView();
            }
        }
        public bool SerialPortSwitch(string comName, bool switchState)
        {
            try
            {
                if (switchState)
                {
                    if (this.COM.IsOpen)
                    {
                        this.COM.Close();
                    }
                    this.COM.PortName = comName;
                    this.COM.Open();
                }
                else
                {
                    if (this.COM.IsOpen)
                    {
                        this.COM.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "", ex);
            }
            return false;
        }
    }
    enum PreViewState
    {
        PreView, OLOSView, Null
    }
}
