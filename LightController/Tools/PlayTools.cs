﻿using FTD2XX_NET;
using LightController.Ast;
using LightController.Tools.CSJ;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace LightController.Tools
{
    public class PlayTools
    {
        //TODO Test
        private SerialPort TestCom { get; set; }


        private static PlayTools Instance { get; set; }
        private static FTDI Device { get; set; }                    
        private readonly byte[] StartCode = new byte[] { 0x00 };
        private DBWrapper DBWrapper { get; set; }
        private string ConfigPath { get; set; }
        private CSJ_Config Config { get; set; }
        private int SceneNo { get; set; }
        private int TimeFactory { get; set; }
        private byte[] PlayData { get; set; }
        private bool IsMusicControl { get; set; }
        private bool IsSendEmptyData { get; set; }
        private bool MusicData { get; set; }
        private bool MusicWaiting { get; set; }
        private int MusicStep { get; set; }
        private int MusicStepTime { get; set; }
        private PreViewState State { get; set; }
        private int[] StepList { get; set; }
        private int StepListCount { get; set; }
        private int MusicIntervalTime { get; set; }
        private int MusicStepPoint { get; set; }


        private System.Timers.Timer PreviewTimer { get; set; }
        private System.Timers.Timer MusicControlTimer { get; set; }
        private System.Timers.Timer SendTimer { get; set; }

        private System.Timers.Timer Timer { get; set; }
        private Dictionary<int,byte> MusicDataBuff { get; set; }
        private List<PlayPoint> M_PlayPoints { get; set; }
        private List<PlayPoint> C_PlayPoints { get; set; }

        public const int STATE_SERIALPREVIEW = 0;
        public const int STATE_INTENETPREVIEW = 1;
        public const int STATE_TEST = 2;
        private int PreviewWayState { get; set; }
        private string DeviceIpByIntentPreview { get; set; }
        private bool IsInitIntentDebug { get; set; }
        private ICommunicatorCallBack IntentDebugCallback { get; set; }
        private Thread SendEmptyDebugDataThread { get; set; }

        private PlayTools()
        {
            try
            {
                this.TimeFactory = 32;
                this.MusicStepTime = 0;
                this.State = PreViewState.Null;
                Device = new FTDI();
                this.IsSendEmptyData = false;

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
                CSJLogs.GetInstance().ErrorLog(ex);
            }
           
        }
        public void StartInternetPreview(string deviceIp, ICommunicatorCallBack receiveCallBack, int timeFactory)
        {
            this.PreviewWayState = STATE_INTENETPREVIEW;
            this.DeviceIpByIntentPreview = deviceIp;
            this.IntentDebugCallback = receiveCallBack;
            this.IsInitIntentDebug = true;
            this.TimeFactory = timeFactory;
            if (!this.SendTimer.Enabled)
            {
                this.SendTimer.Start();
            }
        }
        public void StopInternetPreview(ICommunicatorCallBack receiveCallBack)
        {
            ConnectTools.GetInstance().StopIntentPreview(this.DeviceIpByIntentPreview, receiveCallBack);
            this.IsInitIntentDebug = false;
        }
        public static PlayTools GetInstance()
        {
            if (Instance == null)
            {
                Instance = new PlayTools();
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
                this.State = PreViewState.Null;
                this.ResetDebugDataToEmpty();
                if (!this.SendTimer.Enabled)
                {
                    this.SendTimer.Start();
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        public void ResetDebugDataToEmpty()
        {
            this.PlayData = Enumerable.Repeat(Convert.ToByte(0x00), 512).ToArray();
        }
        public void StopSend()
        {
            this.SendTimer.Stop();
        }

        private long Time1 { get; set; }
        private long Time2 { get; set; }
        private long Time3 { get; set; }


        public void PreView(DBWrapper wrapper, string configPath, int sceneNo)
        {
            try
            {
                if (!FileUtils.IsDefaultFile())
                {
                    return;
                }
                this.MusicData = false;
                this.DBWrapper = wrapper;
                this.ConfigPath = configPath;
                this.SceneNo = sceneNo;
                this.Config = new CSJ_Config(wrapper, configPath);
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
                this.State = PreViewState.PreView;
                if (this.PreviewTimer.Enabled)
                {
                    this.PreviewTimer.Stop();
                }
                this.PreviewTimer.Interval = this.TimeFactory;
                this.SendTimer.Interval = this.TimeFactory;
                if (this.SendTimer.Enabled)
                {
                    this.SendTimer.Stop();
                }
                this.PreviewTimer.Start();
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                EndView();
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
                this.State = PreViewState.OLOSView;
                this.PlayData = data;
                this.SendTimer.Interval = this.TimeFactory;
                if (!this.SendTimer.Enabled)
                {
                    this.SendTimer.Start();
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
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
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
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
        private void PreviewOnTimer(object sender,ElapsedEventArgs e)
        {
            this.PlayData = Enumerable.Repeat(Convert.ToByte(0x00), 512).ToArray();
            foreach (PlayPoint item in C_PlayPoints)
            {
                this.PlayData[item.ChannelNo - 1] = item.Read();
            }
            if (IsMusicControl)
            {
                List<int> keys =  MusicDataBuff.Keys.ToList();
                foreach (int item in keys)
                {
                    this.PlayData[item - 1] = MusicDataBuff[item];
                }
            }
            if (this.SendTimer.Enabled)
            {
                this.SendTimer.Stop();
            }
            this.Play();
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
                if (this.MusicStepPoint == this.StepListCount)
                {
                    this.MusicStepPoint = 0;
                }

                this.MusicStep = this.StepList[this.MusicStepPoint++];
                for (int i = 0; i < this.MusicStep; i++)
                {
                    this.MusicDataBuff = new Dictionary<int, byte>();
                    foreach (PlayPoint item in M_PlayPoints)
                    {
                        this.MusicDataBuff.Add(item.ChannelNo, item.Read());
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
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        /// <summary>
        /// 发送数据定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendOnTimer(object sender, ElapsedEventArgs e)
        {
            this.Play();
        }
        private void Play()
        {
            try
            {
                List<byte> buff = new List<byte>();
                buff.AddRange(this.StartCode);
                buff.AddRange(this.PlayData);
                if (this.PreviewWayState == STATE_SERIALPREVIEW)
                {
                    UInt32 count = 0;
                    if (Device.IsOpen)
                    {
                        Device.SetBreak(true);
                        Thread.Sleep(0);
                        Device.SetBreak(false);
                        Thread.Sleep(0);
                        Device.Purge(FTDI.FT_PURGE.FT_PURGE_TX);
                        Device.Write(buff.ToArray(), buff.ToArray().Length, ref count);
                        Device.SetBreak(false);
                    }
                }
                else
                {
                    if (IsInitIntentDebug)
                    {
                        ConnectTools.GetInstance().StartIntentPreview(this.DeviceIpByIntentPreview, TimeFactory, this.IntentDebugCallback);
                        IsInitIntentDebug = false;
                    }
                    ConnectTools.GetInstance().SendIntenetPreview(DeviceIpByIntentPreview, buff.ToArray());
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                this.EndView();
            }
        }
        public bool ConnectDevice(string comName)
        {
            try
            {
                UInt32 deviceCount = 0;
                FTDI.FT_STATUS status = FTDI.FT_STATUS.FT_OK;
                status = Device.GetNumberOfDevices(ref deviceCount);
                if (status == FTDI.FT_STATUS.FT_OK)
                {
                    if (deviceCount > 0)
                    {
                        FTDI.FT_DEVICE_INFO_NODE[] deviceList = new FTDI.FT_DEVICE_INFO_NODE[deviceCount];
                        status = Device.GetDeviceList(deviceList);
                        if (status == FTDI.FT_STATUS.FT_OK)
                        {
                            for (int i = 0; i < deviceCount; i++)
                            {
                                status = Device.OpenBySerialNumber(deviceList[i].SerialNumber);
                                if (status == FTDI.FT_STATUS.FT_OK)
                                {
                                    string portName;
                                    Device.GetCOMPort(out portName);
                                    if (portName == null || portName == "" || portName != comName)
                                    {
                                        Device.Close();
                                    }
                                    else
                                    {
                                        Device.SetBaudRate(250000);
                                        Device.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_2, FTDI.FT_PARITY.FT_PARITY_NONE);
                                        PreviewWayState = STATE_SERIALPREVIEW;
                                        return Device.IsOpen;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
            return false;
        }
        public void CloseDevice()
        {
            try
            {
                this.EndView();
                if (Device != null)
                {
                    if (Device.IsOpen)
                    {
                        Device.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
           
        }
    }
    enum PreViewState
    {
        PreView,OLOSView,Null
    }
}
