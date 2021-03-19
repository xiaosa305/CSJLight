using FTD2XX_NET;
using LightController.Ast;
using LightController.PeripheralDevice;
using LightController.Tools.CSJ;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using NPOI.SS.Formula;
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

        

        private static int PreviewTimerStatus = 0;
        private static int MusicControlTimerStatus = 0;
        private static int SendTimerStatus = 0;


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
        private bool IsInitIntentDebug { get; set; }
        private Thread SendEmptyDebugDataThread { get; set; }


        private BaseCommunication Communication { get; set; }
        private BaseCommunication.Completed StartIntentPreviewCompleted { get; set; }
        private BaseCommunication.Error StartIntentPreviewError { get; set; }
        private BaseCommunication.Completed StopIntentPreviewCompleted { get; set; }
        private BaseCommunication.Error StopIntentPreviewError { get; set; }


        //TODO XIAOSA：待删除测试
        public bool IsTest { get; set; }
        private SerialPort TestComDevice { get; set; }
        private PlayTools()
        {
            try
            {
                //TODO XIAOSA：待删除测试
                this.IsTest = false;

                this.MusicDataBuff = new Dictionary<int, byte>();
                this.PlayData = Enumerable.Repeat(Convert.ToByte(0x00), 512).ToArray();
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
                LogTools.Error(Constant.TAG_XIAOSA, "初始化播放工具失败", ex);
            }
           
        }

        public void StartInternetPreview(BaseCommunication communication, BaseCommunication.Completed completed, BaseCommunication.Error error, int timeFactory)
        {
            this.Communication = communication;
            this.StartIntentPreviewCompleted = completed;
            this.StartIntentPreviewError = error;
            this.PreviewWayState = STATE_INTENETPREVIEW;
            this.IsInitIntentDebug = true;
            this.TimeFactory = timeFactory;
            if (!this.SendTimer.Enabled)
            {
                this.SendTimer.Start();
            }
        }

        public void StopInternetPreview(BaseCommunication.Completed completed, BaseCommunication.Error error)
        {
            this.StopIntentPreviewCompleted = completed;
            this.StopIntentPreviewError = error;
            if (this.Communication != null)
            {
                this.Communication.StopIntentPreview(completed, error);
            }
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
        public void StopSend()
        {
            this.SendTimer.Stop();
        }
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
                //this.PreviewTimer.Interval = 100;

                this.SendTimer.Interval = this.TimeFactory;
                if (this.SendTimer.Enabled)
                {
                    this.SendTimer.Stop();
                }
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
                this.State = PreViewState.OLOSView;
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
                    Console.WriteLine("正在触发音频，已取消1");
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
                    Console.WriteLine("正在触发音频，已取消2");
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
                Console.WriteLine("音频触发成功");
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

        public byte[] GetTestData()
        {
            byte[] data = new byte[512];
            lock (this.PlayData)
            {
                Array.Copy(this.PlayData, data, 512);
            }
            return data;
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
                //Console.WriteLine("X轴：" + buff[1] + "  ------------------" + "X轴微调：" + buff[2] + "  ------------------" + "Y轴：" + buff[3] + "  ------------------" + "Y轴微调：" + buff[4]);
                if (this.IsTest)
                {
                    this.SendTestData(buff.ToArray());
                    Thread.Sleep(30);
                }
                else if (this.PreviewWayState == STATE_SERIALPREVIEW)
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
                        if (this.Communication != null)
                        {
                            this.Communication.StartIntentPreview(TimeFactory, StartIntentPreviewCompleted, StartIntentPreviewError);
                        }
                        IsInitIntentDebug = false;
                    }
                    if (this.Communication != null)
                    {
                        (this.Communication as NetworkConnect).IntentPreview((this.Communication as NetworkConnect).DeviceIp, buff.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "播放数据输出发生错误", ex);
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
                LogTools.Error(Constant.TAG_XIAOSA, "连接DMX串口设备失败", ex);
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
                LogTools.Error(Constant.TAG_XIAOSA, "关闭DMX串口设备失败", ex);
            }
           
        }



        //TODO XIAOSA：512测试模块
        public void StartTestMode(string comName)
        {
            if (this.TestComDevice == null)
            {
                this.TestComDevice = new SerialPort();
                this.TestComDevice.PortName = comName;
                //this.TestComDevice.BaudRate = 256000;
                this.TestComDevice.BaudRate = 115200;
                this.TestComDevice.DataBits = 8;
                this.TestComDevice.StopBits = StopBits.One;
                this.TestComDevice.Parity = Parity.None;
                this.TestComDevice.DataReceived += new SerialDataReceivedEventHandler(this.TestComDeviceReceive);
            }
            if (this.TestComDevice.IsOpen)
            {
                this.TestComDevice.Close();
            }
            this.TestComDevice.Open();
            this.IsTest = true;
        }
        private void SendTestData(byte[] data)
        {
            if (this.TestComDevice != null)
            {
                if (this.TestComDevice.IsOpen)
                {
                    this.TestComDevice.Write(data, 0, data.Length);
                }
            }
        }
        public void CloseTestMode()
        {
            this.IsTest = false;
            if (this.TestComDevice != null)
            {
                if (this.TestComDevice.IsOpen)
                {
                    this.TestComDevice.Close();
                }
            }
        }
        public string[] GetTestSerialPortNameList()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                return ports;
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "获取串口列表失败", ex);
                return null;
            }
        }
        private void TestComDeviceReceive(object sender, SerialDataReceivedEventArgs s)
        {
            List<byte> RxBuff = new List<byte>();
            while (this.IsTest)
            {
                RxBuff.Add(Convert.ToByte(TestComDevice.ReadByte()));
                if (RxBuff.Count == 513) 
                {
                    RxBuff.Clear();
                }
            }
        }
    }
    enum PreViewState
    {
        PreView,OLOSView,Null
    }
}
