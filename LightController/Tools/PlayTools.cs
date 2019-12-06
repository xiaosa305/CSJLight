using FTD2XX_NET;
using LightController.Ast;
using LightController.Tools.CSJ;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace LightController.Tools
{
    public class PlayTools
    {
        private static PlayTools Instance { get; set; }
        private static FTDI Device { get; set; }                    
        private readonly byte[] StartCode = new byte[] { 0x00 };
        private DBWrapper DBWrapper { get; set; }
        private string ConfigPath { get; set; }
        private CSJ_Config Config { get; set; }
        private int SceneNo { get; set; }
        private int TimeFactory { get; set; }
        private byte[] PlayData { get; set; }
        private Thread OLOSThread { get; set; }
        private Thread PreViewThread { get; set; }
        private Thread MusicControlThread { get; set; }
        private bool IsMusicControl { get; set; }
        private bool IsPausePlay { get; set; }
        private int MusicStep { get; set; }
        private int MusicStepTime { get; set; }
        private PreViewState State { get; set; }
        private int[] StepList { get; set; }
        private int StepListCount { get; set; }
        private int MusicIntervalTime { get; set; }
        private int MusicStepPoint { get; set; }
        private bool MusicData { get; set; }
        private bool MusicWaiting { get; set; }
        private System.Timers.Timer Timer { get; set; }
        private Dictionary<int,byte> MusicDataBuff { get; set; }
        private List<PlayPoint> M_PlayPoints { get; set; }
        private List<PlayPoint> C_PlayPoints { get; set; }

        public const int STATE_SERIALPREVIEW = 0;
        public const int STATE_INTENETPREVIEW = 1;
        private int PreviewWayState { get; set; }
        private string DeviceIpByIntentPreview { get; set; }
        private bool IsInitIntentDebug { get; set; }
        private ICommunicatorCallBack IntentDebugCallback { get; set; }
        private Thread SendEmptyDebugDataThread { get; set; }

        private PlayTools()
        {
            try
            {
                TimeFactory = 32;
                MusicStepTime = 0;
                State = PreViewState.Null;
                Device = new FTDI();
                Timer = new System.Timers.Timer();
                MusicWaiting = true;
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
            SendEmptyDebugDataThread = new Thread(new ThreadStart(SendEmptyDataStart));
            SendEmptyDebugDataThread.Start();
        }

        public void StopInternetPreview(ICommunicatorCallBack receiveCallBack)
        {
            ConnectTools.GetInstance().StopIntentPreview(this.DeviceIpByIntentPreview, receiveCallBack);
            IsInitIntentDebug = false;
        }


        private void SendEmptyDataStart()
        {
            while (true)
            {
                this.Play();
                Thread.Sleep(this.TimeFactory - 21);
            }
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
                if (PreViewThread != null)
                {
                    try
                    {
                        PreViewThread.Abort();
                    }
                    finally
                    {
                        PreViewThread = null;
                    }
                }
                if (OLOSThread != null)
                {
                    try
                    {
                        OLOSThread.Abort();
                    }
                    finally
                    {
                        OLOSThread = null;
                    }
                }
                if (MusicControlThread != null)
                {
                    try
                    {
                        MusicControlThread.Abort();
                    }
                    finally
                    {
                        MusicControlThread = null;
                    }
                }
                State = PreViewState.Null;
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        public void PreView(DBWrapper wrapper, string configPath, int sceneNo)
        {
            try
            {
                //暂停播放准备生成数据
                if (!FileUtils.IsDefaultFIle())
                {
                    return;
                }
                IsPausePlay = true;
                MusicData = false;
                this.DBWrapper = wrapper;
                this.ConfigPath = configPath;
                this.SceneNo = sceneNo;
                //获取全局配置信息
                this.Config = new CSJ_Config(wrapper, configPath);
               
                C_PlayPoints = FileUtils.GetCPlayPoints();
                TimeFactory = Config.TimeFactory;
                try
                {
                    //如果单灯单步线程运行中，将其强制关闭
                    if (OLOSThread != null)
                    {
                        OLOSThread.Abort();
                    }
                }
                finally
                {
                    //将单灯单步线程置为null
                    OLOSThread = null;
                    //是否有音频
                    if (FileUtils.IsMusicFile())
                    {
                        this.MusicData = true;
                        this.MusicStepTime = FileUtils.GetMusicTime();
                        this.StepListCount = FileUtils.GetMusicStepCount();
                        this.MusicIntervalTime = FileUtils.GetMusicIntervalTime();
                        this.StepList = FileUtils.GetMusicStepList();
                        M_PlayPoints = FileUtils.GetMPlayPoints();
                    }
                    MusicStep = this.Config.Music_Control_Enable[SceneNo];
                    //关闭暂停播放
                    IsPausePlay = false;
                    //启动项目预览线程
                    if (PreViewThread == null)
                    {
                        PreViewThread = new Thread(new ThreadStart(PreViewThreadStart))
                        {
                            //将线程设置为后台运行
                            IsBackground = true
                        };
                        PreViewThread.Start();
                    }
                    State = PreViewState.PreView;
                }
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
                this.IsPausePlay = true;
                if (this.Config != null)
                {
                    this.TimeFactory = this.Config.TimeFactory;
                }
                else
                {
                    this.TimeFactory = 32;
                }
                try
                {
                    if (this.PreViewThread != null)
                    {
                        this.PreViewThread.Abort();
                    }
                }
                finally
                {
                    this.PreViewThread = null;
                    this.PlayData = data;
                    this.IsPausePlay = false;
                    if (this.OLOSThread == null)
                    {
                        this.OLOSThread = new Thread(new ThreadStart(this.OLOSViewThreadStart))
                        {
                            IsBackground = true
                        };
                        this.OLOSThread.Start();
                    }
                    this.State = PreViewState.OLOSView;
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
                if (this.Config.Music_Control_Enable[this.SceneNo] == 0)
                {
                    return;
                }
                if (this.PreViewThread == null)
                {
                    return;
                }
                if (!this.MusicData)
                {
                    return;
                }
                if (this.MusicWaiting)
                {
                    this.Timer.Stop();
                    this.MusicControlThread = new Thread(new ThreadStart(this.MusicControlThreadStart))
                    {
                        IsBackground = true
                    };
                    this.MusicWaiting = false;
                    this.MusicControlThread.Start();
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        private void MusicControlThreadStart()
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
                    MusicDataBuff = new Dictionary<int, byte>();
                    foreach (PlayPoint item in M_PlayPoints)
                    {
                        MusicDataBuff.Add(item.ChannelNo,item.Read());
                    }
                    this.IsMusicControl = true;
                    Thread.Sleep(this.TimeFactory * this.MusicStepTime);
                }
                if (this.MusicIntervalTime != 0)
                {
                    this.Timer.Interval = this.MusicIntervalTime;
                    this.Timer.AutoReset = false;
                    this.Timer.Elapsed += this.MusicWaitingHandl;
                    this.Timer.Start();
                    this.MusicWaiting = true;
                    this.MusicControlThread = null;
                }
                else
                {
                    this.IsMusicControl = false;
                    this.MusicWaiting = true;
                    this.MusicControlThread = null;
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
        private void OLOSViewThreadStart()
        {
            try
            {
                while (true)
                {
                    this.Play();
                    Thread.Sleep(this.TimeFactory);
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }   
           
        }
        private void PreViewThreadStart()
        {
            try
            {
                Thread.Sleep(500);
                this.PlayData = Enumerable.Repeat(Convert.ToByte(0x00), 512).ToArray();
                while (true)
                {
                    foreach (PlayPoint item in C_PlayPoints)
                    {
                        this.PlayData[item.ChannelNo - 1] = item.Read();
                    }
                    if (IsMusicControl)
                    {
                        foreach (int item in MusicDataBuff.Keys)
                        {
                            this.PlayData[item - 1] = MusicDataBuff[item];
                        }
                    }
                    this.Play();
                    Thread.Sleep(this.TimeFactory - 21);
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
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
                        Thread.Sleep(300);
                    }
                    Thread.Sleep(21);
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
        public void Test()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(TestStart), null);
        }
        private void TestStart(Object obj)
        {
            List<PlayPoint> playPoints = FileUtils.GetCPlayPoints();
            Thread.Sleep(500);
            this.PlayData = Enumerable.Repeat(Convert.ToByte(0x00), 512).ToArray();
            while (true)
            {
                foreach (PlayPoint item in playPoints)
                {
                    this.PlayData[item.ChannelNo - 1] = item.Read();
                }
                Play();
                Thread.Sleep(this.TimeFactory - 21);
            }
        }
    }
    enum PreViewState
    {
        PreView,OLOSView,Null
    }
}
