using FTD2XX_NET;
using LightController.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
    public class PlayTools
    {
        private static PlayTools Instance { get; set; }
        //串口设备
        private static FTDI Device { get; set; }                    
        //起始标记码
        private readonly byte[] StartCode = new byte[] { 0x00 };
        //数据库数据
        private DBWrapper DBWrapper { get; set; }
        //全局配置文件路径
        private string ConfigPath { get; set; }
        //全局配置文件数据
        private DMXConfigData ConfigData { get; set; }
        //单前场景编号
        private int SceneNo { get; set; }
        //时间因子
        private int TimeFactory { get; set; }
        //播放缓存区
        private byte[] PlayData { get; set; }
        //常规程序通道指针
        private int[] C_ChanelPoint { get; set; }
        //音频程序通道指针
        private int[] M_ChanelPoint { get; set; }
        //常规程序通道编号
        private int[] C_ChanelId { get; set; }
        //音频程序通道编号
        private int[] M_ChanelId { get; set; }
        //常规程序通道数据缓存区
        private byte[][] C_ChanelData { get; set; }
        //音频程序通道数据缓存区
        private byte[][] M_ChanelData { get; set; }
        //常规程序通道总数
        private int C_ChanelCount { get; set; }
        //音频程序通道总数
        private int M_ChanelCount { get; set; }
        //单灯单步预览线程
        private Thread OLOSThread { get; set; }
        //项目预览线程
        private Thread PreViewThread { get; set; }
        //音频触发线程
        private Thread MusicControlThread { get; set; }
        //音频是否触发
        private bool IsMusicControl { get; set; }
        //更新数据暂停生成新数据标识
        private bool IsPausePlay { get; set; }
        //音频步数
        private int MusicStep { get; set; }
        //音频步时长
        private int MusicStepTime { get; set; }
        //当前播放模式
        private PreViewState State { get; set; }

        /// <summary>
        /// test
        /// </summary>
        private DateTime BeforDT { get; set; }
        private DateTime AftetDT { get; set; }

        private PlayTools()
        {
            TimeFactory = 32;
            MusicStepTime = 0;
            ConnectDevice();
            State = PreViewState.Null;
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
            if (PreViewThread != null)
            {
                try
                {
                    PreViewThread.Abort();
                }
                catch (Exception)
                {
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
                catch (Exception)
                {
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
                catch (Exception)
                {
                }
                finally
                {
                    MusicControlThread = null;
                }
            }
            if (Device != null)
            {
                try
                {
                    Device.Close();
                }
                catch (Exception)
                {
                }
                finally
                {
                    Device = null;
                }

            }
            State = PreViewState.Null;
        }

        public void PreView(DBWrapper wrapper, string configPath, int sceneNo)
        {
            try
            {
                //暂停播放准备生成数据
                IsPausePlay = true;
               
                if (wrapper.valueList == null || wrapper.stepCountList == null || wrapper.lightList == null) return;
                if (wrapper.lightList.Count == 0 || wrapper.stepCountList.Count == 0 || wrapper.valueList.Count == 0) return;
                this.DBWrapper = wrapper;
                this.ConfigPath = configPath;
                this.SceneNo = sceneNo;
                //获取常规程序数据以及音频程序数据
                DMX_C_File c_File = DataTools.GetInstance().GetC_File(DBWrapper, SceneNo, ConfigPath);
                DMX_M_File m_File = DataTools.GetInstance().GetM_File(DBWrapper, SceneNo, ConfigPath);
                ConfigData = DataTools.GetInstance().GetConfigData(DBWrapper, ConfigPath);
                TimeFactory = ConfigData.TimeFactory;
                try
                {
                    //如果单灯单步线程运行中，将其强制关闭
                    if (OLOSThread != null)
                    {
                        OLOSThread.Abort();
                    }
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine("关闭单灯单步线程——" + ex.Message);
                }
                finally
                {

                    //将单灯单步线程置为null
                    OLOSThread = null;
                    //重连设备
                    if (State == PreViewState.OLOSView || State == PreViewState.Null)
                    {
                        ReConnectDevice();
                    }
                    //预读常规程序数据到缓存区
                    if (c_File != null)
                    {
                        C_ChanelCount = c_File.Data.HeadData.ChanelCount;
                        IList<C_Data> c_Datas = c_File.Data.Datas;
                        C_ChanelData = new byte[C_ChanelCount][];
                        C_ChanelId = new int[C_ChanelCount];
                        C_ChanelPoint = new int[C_ChanelCount];
                        for (int i = 0; i < C_ChanelCount; i++)
                        {
                            C_Data c_Data = c_Datas[i];
                            C_ChanelPoint[i] = 0;
                            C_ChanelId[i] = c_Data.ChanelNo;
                            IList<byte> data = new List<byte>();
                            for (int j = 0; j < c_Data.DataSize; j++)
                            {
                                data.Add(Convert.ToByte(c_Data.Datas[j]));
                            }
                            C_ChanelData[i] = data.ToArray();
                        }
                    }
                    //预读音频程序数据到缓存区
                    if (m_File != null)
                    {
                        IList<M_Data> m_Datas = m_File.Data.Datas;
                        M_ChanelCount = m_Datas.Count();
                        M_ChanelData = new byte[M_ChanelCount][];
                        M_ChanelId = new int[M_ChanelCount];
                        M_ChanelPoint = new int[M_ChanelCount];
                        MusicStepTime = m_File.Data.HeadData.FrameTime;
                        for (int i = 0; i < M_ChanelCount; i++)
                        {
                            M_Data m_Data = m_Datas[i];
                            M_ChanelId[i] = m_Data.ChanelNo;
                            M_ChanelPoint[i] = 0;
                            IList<byte> data = new List<byte>();
                            for (int j = 0; j < m_Data.DataSize; j++)
                            {
                                data.Add(Convert.ToByte(m_Data.Datas[j]));
                            }
                            M_ChanelData[i] = data.ToArray();
                        }
                    }
                    MusicStep = ConfigData.Music_Control_Enable[SceneNo];
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
            catch (Exception)
            {
                EndView();
            }
        }

        public void OLOSView(byte[] data)
        {
            try
            {
                IsPausePlay = true;
                TimeFactory = 32;
                try
                {
                    if (PreViewThread != null)
                    {
                        PreViewThread.Abort();
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    PreViewThread = null;
                    if(State == PreViewState.PreView || State == PreViewState.Null)
                    {
                        ReConnectDevice();
                    }
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
            catch (Exception)
            {
                EndView();
            }
            
        }

        public void MusicControl()
        {
            try
            {
                if (PreViewThread == null)
                {
                    return;
                }
                if(MusicControlThread != null)
                {
                    return;
                    //try
                    //{
                    //    MusicControlThread.Abort();
                    //}
                    //catch (Exception)
                    //{
                    //    MusicControlThread = null;
                    //}
                    //finally
                    //{
                    //    MusicControlThread = new Thread(new ThreadStart(MusicControlThreadStart))
                    //    {
                    //        IsBackground = true
                    //    };
                    //    MusicControlThread.Start();
                    //}
                }
                else
                {
                    MusicControlThread = new Thread(new ThreadStart(MusicControlThreadStart))
                    {
                        IsBackground = true
                    };
                    MusicControlThread.Start();
                }
            }
            catch (Exception)
            {
            }
        }

        private void MusicControlThreadStart()
        {
            for (int i = 0; i < MusicStep; i++)
            {
                IsMusicControl = true;
                Thread.Sleep(TimeFactory * MusicStepTime);
                for (int j = 0; j < M_ChanelPoint.Length; j++)
                {
                    M_ChanelPoint[j]++;
                }
            }
            IsMusicControl = false;
            MusicControlThread = null;
        }

        private void OLOSViewThreadStart()
        {
            
            while (true)
            {
                BeforDT = System.DateTime.Now;
                Play();
                //Thread.Sleep(TimeFactory);
                Thread.Sleep(1000);
            }
        }

        private void PreViewThreadStart()
        {
            PlayData = Enumerable.Repeat(Convert.ToByte(0x00), 512).ToArray();
            while (true)
            {
                if (!IsPausePlay)
                {
                    AftetDT = System.DateTime.Now;
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

        private void Play()
        {
            UInt32 count = 0;
            try
            {
                //发送Break|
                Device.SetBreak(true);
                Thread.Sleep(0);
                Device.SetBreak(false);
                Thread.Sleep(0);
                List<byte> buff = new List<byte>();
                buff.AddRange(StartCode);
                buff.AddRange(PlayData);
                Device.Write(buff.ToArray(), buff.ToArray().Length, ref count);
                Device.SetBreak(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("未连接DMX512设备——" + ex.Message);
                EndView();
            }
        }

        public void ReConnectDevice()
        {
            if (Device != null)
            {
                Device.Close();
                Device = null;
            }
            ConnectDevice();
        }

        public bool ConnectDevice()
        {
            UInt32 deviceCount = 0;
            FTDI.FT_STATUS status = FTDI.FT_STATUS.FT_OK;
            Device = new FTDI();
            status = Device.GetNumberOfDevices(ref deviceCount);
            if (status == FTDI.FT_STATUS.FT_OK)
            {
                if (deviceCount > 0)
                {
                    FTDI.FT_DEVICE_INFO_NODE[] deviceList = new FTDI.FT_DEVICE_INFO_NODE[deviceCount];
                    status = Device.GetDeviceList(deviceList);
                    if (status == FTDI.FT_STATUS.FT_OK)
                    {
                        status = Device.OpenBySerialNumber(deviceList[0].SerialNumber);
                        if (status == FTDI.FT_STATUS.FT_OK)
                        {
                            string portName;
                            Device.GetCOMPort(out portName);
                            if (portName == null || portName == "")
                            {
                                Device.Close();
                                Device = null;
                            }
                            Device.SetBaudRate(250000);
                            Device.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_2, FTDI.FT_PARITY.FT_PARITY_NONE);
                        }
                    }
                }
                else
                {
                    Device = null;
                    Console.WriteLine("未连接DMX512设备");
                    EndView();
                }
            }
            return Device.IsOpen;
        }

    }

    enum PreViewState
    {
        PreView,OLOSView,Null
    }
}
