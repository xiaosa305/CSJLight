using FTD2XX_NET;
using LightController.Ast;
using LightController.Tools.CSJ;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
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
        private CSJ_Project Project { get; set; }
        private int SceneNo { get; set; }
        private int TimeFactory { get; set; }
        private byte[] PlayData { get; set; }
        private int[] C_ChanelPoint { get; set; }
        private int[] M_ChanelPoint { get; set; }
        private int[] C_ChanelId { get; set; }
        private int[] M_ChanelId { get; set; }
        private byte[][] C_ChanelData { get; set; }
        private byte[][] M_ChanelData { get; set; }
        private int C_ChanelCount { get; set; }
        private int M_ChanelCount { get; set; }
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

        /// <summary>
        /// test
        /// </summary>
        private DateTime BeforDT { get; set; }
        private DateTime AftetDT { get; set; }

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
                IsPausePlay = true;
                MusicData = false;
                this.DBWrapper = wrapper;
                this.ConfigPath = configPath;
                this.SceneNo = sceneNo;
                //获取常规程序数据以及音频程序数据
                this.Project = DmxDataConvert.GetInstance().GetCSJProjectFiles(this.DBWrapper, this.ConfigPath);
                TimeFactory = this.Project.ConfigFile.TimeFactory;
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
                    Thread.Sleep(200);
                    //将单灯单步线程置为null
                    OLOSThread = null;
                    //预读常规程序数据到缓存区
                    CSJ_C c_File = null;
                    if (this.Project.CFiles != null)
                    {
                        foreach (CSJ_C item in this.Project.CFiles)
                        {
                            if (item.SceneNo == this.SceneNo)
                            {
                                c_File = item;
                            }
                        }
                        C_ChanelCount = c_File.ChannelCount;
                        List<ChannelData> c_Datas = c_File.ChannelDatas;
                        C_ChanelData = new byte[C_ChanelCount][];
                        C_ChanelId = new int[C_ChanelCount];
                        C_ChanelPoint = new int[C_ChanelCount];
                        for (int i = 0; i < C_ChanelCount; i++)
                        {
                            ChannelData c_Data = c_Datas[i];
                            C_ChanelPoint[i] = 0;
                            C_ChanelId[i] = c_Data.ChannelNo;
                            List<byte> data = new List<byte>();
                            for (int j = 0; j < c_Data.DataSize; j++)
                            {
                                data.Add(Convert.ToByte(c_Data.Datas[j]));
                            }
                            C_ChanelData[i] = data.ToArray();
                        }
                    }
                    CSJ_M m_File = null;
                    if (this.Project.MFiles != null)
                    {
                        foreach (CSJ_M item in this.Project.MFiles)
                        {
                            if (item.SceneNo == this.SceneNo)
                            {
                                m_File = item;
                            }
                        }
                        //预读音频程序数据到缓存区
                        if (m_File != null)
                        {
                            List<ChannelData> m_Datas = m_File.ChannelDatas;
                            M_ChanelCount = m_Datas.Count();
                            M_ChanelData = new byte[M_ChanelCount][];
                            M_ChanelId = new int[M_ChanelCount];
                            M_ChanelPoint = new int[M_ChanelCount];
                            MusicStepTime = m_File.FrameTime;
                            for (int i = 0; i < M_ChanelCount; i++)
                            {
                                ChannelData m_Data = m_Datas[i];
                                M_ChanelId[i] = m_Data.ChannelNo;
                                M_ChanelPoint[i] = -1;
                                List<byte> data = new List<byte>();
                                for (int j = 0; j < m_Data.DataSize; j++)
                                {
                                    data.Add(Convert.ToByte(m_Data.Datas[j]));
                                }
                                M_ChanelData[i] = data.ToArray();
                            }
                            this.StepList = m_File.StepList.ToArray();
                            this.StepListCount = m_File.StepListCount;
                            this.MusicIntervalTime = m_File.MusicIntervalTime;
                            this.MusicStepPoint = 0;
                        }
                    }
                    if (m_File != null && c_File != null)
                    {
                        MusicData = true;
                    }
                    MusicStep = this.Project.ConfigFile.Music_Control_Enable[SceneNo];
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
                IsPausePlay = true;
                if (Project!=null)
                {
                    TimeFactory = Project.ConfigFile.TimeFactory;
                }
                else
                {
                    TimeFactory = 32;
                }
                try
                {
                    if (PreViewThread != null)
                    {
                        PreViewThread.Abort();
                    }
                }
                finally
                {
                    Thread.Sleep(200);
                    PreViewThread = null;
                    PlayData = data;
                    IsPausePlay = false;
                    if (OLOSThread == null)
                    {
                        OLOSThread = new Thread(new ThreadStart(OLOSViewThreadStart))
                        {
                            IsBackground = true
                        };
                        OLOSThread.Start();
                    }
                    State = PreViewState.OLOSView;
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                EndView();
            }
            
        }
        public void MusicControl()
        {
            try
            {
                if (Project.ConfigFile.Music_Control_Enable[SceneNo] == 0)
                {
                    return;
                }
                if (PreViewThread == null)
                {
                    return;
                }
                if (!MusicData)
                {
                    return;
                }
                if (MusicWaiting)
                {
                    Timer.Stop();
                    MusicControlThread = new Thread(new ThreadStart(MusicControlThreadStart))
                    {
                        IsBackground = true
                    };
                    MusicWaiting = false;
                    MusicControlThread.Start();
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
                if (MusicStepPoint == StepListCount)
                {
                    MusicStepPoint = 0;
                }

                MusicStep = StepList[MusicStepPoint++];
                for (int i = 0; i < MusicStep; i++)
                {
                    for (int j = 0; j < M_ChanelPoint.Length; j++)
                    {
                        M_ChanelPoint[j]++;
                    }
                    IsMusicControl = true;
                    Thread.Sleep(TimeFactory * MusicStepTime);
                }
                if (MusicIntervalTime != 0)
                {
                    Timer.Interval = MusicIntervalTime;
                    Timer.AutoReset = false;
                    Timer.Elapsed += MusicWaitingHandl;
                    Timer.Start();
                    MusicWaiting = true;
                    MusicControlThread = null;
                }
                else
                {
                    IsMusicControl = false;
                    MusicWaiting = true;
                    MusicControlThread = null;
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }

        private void MusicWaitingHandl(object sender, ElapsedEventArgs e)
        {
            IsMusicControl = false;
        }
        private void OLOSViewThreadStart()
        {
            try
            {
                while (true)
                {
                    Play();
                    Thread.Sleep(TimeFactory);
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
                PlayData = Enumerable.Repeat(Convert.ToByte(0x00), 512).ToArray();
                while (true)
                {
                    if (!IsPausePlay)
                    {
                        for (int i = 0; i < C_ChanelCount; i++)
                        {
                            if (C_ChanelPoint[i] == C_ChanelData[i].Length)
                            {
                                C_ChanelPoint[i] = 0;
                            }
                            PlayData[C_ChanelId[i] - 1] = C_ChanelData[i][C_ChanelPoint[i]++];
                        }
                        if (IsMusicControl)
                        {
                            for (int i = 0; i < M_ChanelCount; i++)
                            {
                                if (M_ChanelPoint[i] == M_ChanelData[i].Length)
                                {
                                    M_ChanelPoint[i] = 0;
                                }
                                PlayData[M_ChanelId[i] - 1] = M_ChanelData[i][M_ChanelPoint[i]];
                            }
                        }
                    }
                    Play();
                    Thread.Sleep(TimeFactory - 21);
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
                UInt32 count = 0;
                if (Device.IsOpen)
                {
                    //发送Break|
                    Device.SetBreak(true);
                    Thread.Sleep(0);
                    Device.SetBreak(false);
                    Thread.Sleep(0);
                    List<byte> buff = new List<byte>();
                    //test
                    int value1 = PlayData[2];
                    int value2 = PlayData[3];
                    int value3 = PlayData[0];
                    int value4 = PlayData[1];
                    Console.WriteLine("X轴：" + value3 + "--------X轴微调：" + value4 +";*****Y轴：" + value1 + "--------Y轴微调：" + value2);
                    //Test
                    buff.AddRange(StartCode);
                    buff.AddRange(PlayData);
                    Device.Purge(FTDI.FT_PURGE.FT_PURGE_TX);
                    Device.Write(buff.ToArray(), buff.ToArray().Length, ref count);
                    Device.SetBreak(false);
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                EndView();
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
                EndView();
                Thread.Sleep(100);
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
