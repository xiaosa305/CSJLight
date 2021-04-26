using Crc32C;
using LBDConfigTool.utils.conf;
using LBDConfigTool.utils.crc;
using LBDConfigTool.utils.entity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;

namespace LBDConfigTool.utils.communication
{
    public abstract class BaseFunctionalModule
    {
        [DllImport("winmm.dll")] internal static extern uint timeBeginPeriod(uint period);
        [DllImport("winmm.dll")] internal static extern uint timeEndPeriod(uint period);
        protected const int TIME_OUT_COUNT = 5000;

        protected System.Timers.Timer TimeOut { get; set; }
        private System.Timers.Timer TaskTimer { get; set; }
        private Module CurrentModule { get; set; }
        private bool IsSending { get; set; }
        public delegate void Completed(Object obj, string msg);
        public delegate void Error(string msg);
        public delegate void Progress(int value);
        private Completed Completed_Event { get; set; }
        private Error Error_Event { get; set; }
        private Progress Progress_Event { get; set; }
        protected ConcurrentQueue<List<byte>> MessageQueue { get; set; }
        private System.Timers.Timer MessageTransaction { get; set; }
        private void ThreadSleep(int time)
        {
            timeBeginPeriod(1);
            Thread.Sleep(time);
            timeEndPeriod(1);
        }
        protected abstract void Send(byte[] data);
        protected void SendCompleted()
        {
            switch (CurrentModule)
            {
                case Module.ReadDeviceId:
                case Module.WriteEncrypt:
                case Module.WriteData:
                case Module.IsEncrypt:
                    this.StartTimeOutTask();
                    break;
                case Module.SearchDevice:
                case Module.UpdateFPGA256:
                case Module.UpdataMCU256:
                case Module.WriteParam:
                    break;
            }
        }
        protected void Init()
        {
            this.MessageTransaction = new System.Timers.Timer() { AutoReset = false };
            this.MessageTransaction.Elapsed += this.MessageTransactionTask;
            this.MessageQueue = new ConcurrentQueue<List<byte>>();
            this.TimeOut = new System.Timers.Timer(TIME_OUT_COUNT) { AutoReset = false };
            this.TimeOut.Elapsed += this.TimeOutTask;
            this.CurrentModule = Module.Null;
            this.IsSending = false;
            this.MessageTransaction.Start();
        }
        private void InitParam()
        {
            this.TaskTimer = null;
            this.Error_Event = null;
            this.Completed_Event = null;
            this.Progress_Event = null;
            this.CurrentModule = Module.Null;
        }
        protected void StartTimeOutTask()
        {
            this.TimeOut.Start();
        }
        protected void StopTimeOutTask()
        {
            this.TimeOut.Stop();
        }
        protected void TimeOutTask(object sender, ElapsedEventArgs e)
        {
            string value = "";
            switch (this.CurrentModule)
            {
                case Module.WriteEncrypt:
                    value = "加密固件";
                    break;
                case Module.UpdateFPGA256:
                    value = "升级FPGA";
                    break;
                case Module.UpdataMCU256:
                    value = "升级MCU";
                    break;
                case Module.WriteData:
                    break;
                case Module.WriteParam:
                    value = "下载配置参数";
                    break;
            }
            value += "失败";
            this.TaskError(value);
        }
        protected void TaskCompleted()
        {
            this.TaskCompleted(null, "");
        }
        protected void TaskCompleted(string msg)
        {
            this.TaskCompleted(null,msg);
        }
        protected void TaskCompleted(Object obj,string msg)
        {
            if (this.Completed_Event != null)
            {
                this.IsSending = false;
                this.StopTimeOutTask();
                this.Completed_Event(obj, msg);
                this.InitParam();
            }
        }
        protected void TaskError()
        {
            this.TaskError("");
        }
        protected void TaskError(string msg)
        {
            if (this.Error_Event != null)
            {
                this.IsSending = false;
                if (this.TaskTimer != null)
                {
                    this.TaskTimer.Stop();
                }
                this.Error_Event(msg);
                this.InitParam();
            }
        }
        //消息事务管理器
        private void MessageTransactionTask(Object obj, ElapsedEventArgs e)
        {
            while (true)
            {
                if (this.MessageQueue.Count > 0)
                {
                    this.MessageQueue.TryDequeue(out List<byte> message);
                    this.ReceiveManage(message);
                }
                else
                {
                    Thread.Sleep(15);
                }
            }
        }
        //搜索设备
        public void SearchDevice(Completed completed,Error error)
        {
            this.Completed_Event = completed;
            this.Error_Event = error;
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.CurrentModule = Module.SearchDevice;
                    byte[] order = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xA1 };
                    this.Send(order);
                    this.IsSending = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError();
            }
        }
        private void SearchDeviceTask(Object obj, ElapsedEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError();
            }
        }
        //加密
        public void WriteEncrypt(string pwd,Completed completed, Error error)
        {
            this.Completed_Event = completed;
            this.Error_Event = error;
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.CurrentModule = Module.WriteEncrypt;
                    byte[] data = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xD0 };
                    List<byte> buff = new List<byte>();
                    buff.AddRange(data);
                    for (int i = 0; i < 16; i++)
                    {
                        if (i < pwd.Length)
                        {
                            buff.Add(Convert.ToByte(pwd[i]));
                        }
                        else
                        {
                            buff.Add(0x00);
                        }
                    }
                    this.Send(buff.ToArray());
                    this.IsSending = false;
                }
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError();
            }
        }
        
        //升级FPGA
        public void UpdateFPGA256(string filePath,ParamEntity param,Progress progress,Completed completed, Error error)
        {
            this.Completed_Event = completed;
            this.Error_Event = error;
            this.Progress_Event = progress;
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.CurrentModule = Module.UpdateFPGA256;
                    this.TaskTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => UpdateFPGA256Task(filePath,param,s, e));
                    this.TaskTimer.Start();
                }
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError();
            }
        }
        private void UpdateFPGA256Task(string filePath, ParamEntity param, Object obj, ElapsedEventArgs e)
        {
            try
            {
                uint crc = 0;
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    byte[] data = new byte[fileStream.Length];
                    fileStream.Read(data, 0, data.Length);
                    crc = Crc32SUM.GetSumCRC(data);
                }
                using (FileStream file = new FileStream(filePath, FileMode.Open))
                {
                    int seek = 0;
                    long length = file.Length;
                    bool flag = file.Length % param.PacketSize == 0;
                    int lastPackageSize = flag ? param.PacketSize : (int)(length % param.PacketSize);
                    int packetCount = (int)(length / param.PacketSize);
                    List<byte> buff = new List<byte>();
                    byte[] packetHead = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xC0 };
                    byte[] readBuff = new byte[param.PacketSize];
                    for (int i = 0; i < packetCount; i++)
                    {
                        buff.AddRange(packetHead);
                        buff.Add(Convert.ToByte(param.PacketSize & 0xFF));
                        buff.Add(Convert.ToByte((param.PacketSize >> 8) & 0xFF));
                        seek = param.PacketSize * i;
                        buff.Add(Convert.ToByte(seek & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                        if (i == 0)
                        {
                            buff.Add(Convert.ToByte(length & 0xFF));
                            buff.Add(Convert.ToByte((length >> 8) & 0xFF));
                            buff.Add(Convert.ToByte((length >> 16) & 0xFF));
                            buff.Add(Convert.ToByte((length >> 24) & 0xFF));
                            //crc
                            buff.Add(Convert.ToByte(crc & 0xFF));
                            buff.Add(Convert.ToByte((crc >> 8) & 0xFF));
                            buff.Add(Convert.ToByte((crc >> 16) & 0xFF));
                            buff.Add(Convert.ToByte((crc >> 24) & 0xFF));
                        }
                        file.Read(readBuff, 0, param.PacketSize);
                        buff.AddRange(readBuff);
                        this.Send(buff.ToArray());
                        if (seek % param.PartitionIndex == 0)
                        {
                            this.ThreadSleep(param.PacketIntervalTimeByPartitionIndex);
                        }
                        else
                        {
                            if (i == 0)
                            {
                                this.ThreadSleep(param.FirstPacketIntervalTime);
                            }
                            else
                            {
                                this.ThreadSleep(param.PacketIntervalTime);
                            }
                        }
                        buff.Clear();
                        double progress = ((i + 1) * param.PacketSize * 100) / (1.0 * length);
                        this.Progress_Event((int)Math.Floor(progress));
                    }
                    buff.AddRange(packetHead);
                    buff.Add(Convert.ToByte(lastPackageSize & 0xFF));
                    buff.Add(Convert.ToByte((lastPackageSize >> 8) & 0xFF));
                    seek = param.PacketSize * packetCount;
                    buff.Add(Convert.ToByte(seek & 0xFF));
                    buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                    buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                    buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                    if (length < param.PacketSize)
                    {
                        buff.Add(Convert.ToByte(length & 0xFF));
                        buff.Add(Convert.ToByte((length >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((length >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((length >> 24) & 0xFF));
                        //crc
                        buff.Add(Convert.ToByte(crc & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 24) & 0xFF));
                    }
                    readBuff = new byte[lastPackageSize];
                    file.Read(readBuff, 0, lastPackageSize);
                    buff.AddRange(readBuff);
                    this.Send(buff.ToArray());
                }
                byte[] endPacket = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xFF };
                this.Send(endPacket);
                this.ThreadSleep(param.FPGAUpdateCompletedIntervalTime);
                this.Progress_Event(100);
                this.TaskCompleted("FPGA升级成功");
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError("FPGA升级失败");
            }
        }
        //升级MCU
        public void UpdataMCU256(string filePath,ParamEntity param,Progress progress,Completed completed, Error error)
        {
            this.Completed_Event = completed;
            this.Error_Event = error;
            this.Progress_Event = progress;
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.CurrentModule = Module.UpdataMCU256;
                    this.TaskTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => UpdataMCU256Task(filePath,param,s, e));
                    this.TaskTimer.Start();
                }
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError();
            }
        }
        private void UpdataMCU256Task(string filePath, ParamEntity param, Object obj, ElapsedEventArgs e)
        {
            try
            {
                uint crc = 0;
                using (FileStream fileStream = new FileStream(filePath,FileMode.Open))
                {
                    byte[] data = new byte[fileStream.Length];
                    fileStream.Read(data, 0, data.Length);
                    crc = Crc32SUM.GetSumCRC(data);
                }
                using (FileStream file = new FileStream(filePath, FileMode.Open))
                {
                    int seek = 0;
                    long length = file.Length;
                    bool flag = file.Length % param.PacketSize == 0;
                    int lastPackageSize = flag ? param.PacketSize : (int)(length % param.PacketSize);
                    int packetCount = (int)(length / param.PacketSize);
                    List<byte> buff = new List<byte>();
                    byte[] packetHead = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xB0 };
                    byte[] readBuff = new byte[param.PacketSize];
                    for (int i = 0; i < packetCount; i++)
                    {
                        buff.AddRange(packetHead);
                        buff.Add(Convert.ToByte(param.PacketSize & 0xFF));
                        buff.Add(Convert.ToByte((param.PacketSize >> 8) & 0xFF));
                    
                        seek = param.PacketSize * i;
                        buff.Add(Convert.ToByte(seek & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                        if (i == 0)
                        {
                            buff.Add(Convert.ToByte(length & 0xFF));
                            buff.Add(Convert.ToByte((length >> 8) & 0xFF));
                            buff.Add(Convert.ToByte((length >> 16) & 0xFF));
                            buff.Add(Convert.ToByte((length >> 24) & 0xFF));
                            //crc
                            buff.Add(Convert.ToByte(crc & 0xFF));
                            buff.Add(Convert.ToByte((crc >> 8) & 0xFF));
                            buff.Add(Convert.ToByte((crc >> 16) & 0xFF));
                            buff.Add(Convert.ToByte((crc >> 24) & 0xFF));
                        }
                        file.Read(readBuff, 0, param.PacketSize);
                        buff.AddRange(readBuff);
                        this.Send(buff.ToArray());
                        if (seek % param.PartitionIndex == 0)
                        {
                            this.ThreadSleep(param.PacketIntervalTimeByPartitionIndex);
                        }
                        else
                        {
                            if (i == 0)
                            {
                                this.ThreadSleep(param.FirstPacketIntervalTime);
                            }
                            else
                            {
                                this.ThreadSleep(param.PacketIntervalTime);
                            }
                        }
                        buff.Clear();
                        double progress = ((i + 1) * param.PacketSize * 100) / (1.0 * length);
                        this.Progress_Event((int)Math.Floor(progress));
                    }
                    buff.AddRange(packetHead);
                    buff.Add(Convert.ToByte(lastPackageSize & 0xFF));
                    buff.Add(Convert.ToByte((lastPackageSize >> 8) & 0xFF));
                    seek = param.PacketSize * packetCount;
                    buff.Add(Convert.ToByte(seek & 0xFF));
                    buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                    buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                    buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                    if (length < param.PacketSize)
                    {
                        buff.Add(Convert.ToByte(length & 0xFF));
                        buff.Add(Convert.ToByte((length >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((length >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((length >> 24) & 0xFF));
                        //crc
                        buff.Add(Convert.ToByte(crc & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 24) & 0xFF));
                    }
                    readBuff = new byte[lastPackageSize];
                    file.Read(readBuff, 0, lastPackageSize);
                    buff.AddRange(readBuff);
                    this.Send(buff.ToArray());
                }
                byte[] endPacket = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xFF };
                this.Send(endPacket);
                this.ThreadSleep(param.FPGAUpdateCompletedIntervalTime);
                this.Progress_Event(100);
                this.TaskCompleted("MCU升级成功");
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError("MCU升级失败");
            }
        }
        //写入参数
        public void WriteParam(CSJConf conf,Completed completed, Error error)
        {
            this.Completed_Event = completed;
            this.Error_Event = error;
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.CurrentModule = Module.WriteParam;
                    this.TaskTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => WriteParamTask(conf,s, e));
                    this.TaskTimer.Start();
                }
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError();
            }
        }
        private void WriteParamTask(CSJConf conf,Object obj, ElapsedEventArgs e)
        {
            try
            {
                byte[] sourceData = conf.GetData();
                byte[] data = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xA0, Convert.ToByte((sourceData.Length) & 0xFF), Convert.ToByte(((sourceData.Length) >> 8) & 0xFF), 0x00,0x00,0x00,0x00 };
                List<byte> buff = new List<byte>();
                buff.AddRange(data);
                buff.AddRange(sourceData);
                this.Send(buff.ToArray());
                this.WriteParamCompleted();
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError();
            }
        }

        //下载字库
        public void DownloadWordBank(string filePath, ParamEntity param, Progress progress, Completed completed, Error error)
        {
            this.Completed_Event = completed;
            this.Error_Event = error;
            this.Progress_Event = progress;
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.CurrentModule = Module.DownloadWordBank;
                    this.TaskTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => DownloadWordBankTask(filePath, param, s, e));
                    this.TaskTimer.Start();
                }
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError();
            }
        }
        private void DownloadWordBankTask(string filePath, ParamEntity param, Object obj, ElapsedEventArgs e)
        {
            try
            {
                uint crc = 0;
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    byte[] data = new byte[fileStream.Length];
                    fileStream.Read(data, 0, data.Length);
                    crc = Crc32SUM.GetSumCRC(data);
                }
                using (FileStream file = new FileStream(filePath, FileMode.Open))
                {
                    int seek = 0;
                    long length = file.Length;
                    bool flag = file.Length % param.PacketSize == 0;
                    int lastPackageSize = flag ? param.PacketSize : (int)(length % param.PacketSize);
                    int packetCount = (int)(length / param.PacketSize);
                    List<byte> buff = new List<byte>();
                    byte[] packetHead = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xE0 };
                    byte[] readBuff = new byte[param.PacketSize];
                    for (int i = 0; i < packetCount; i++)
                    {
                        buff.AddRange(packetHead);
                        buff.Add(Convert.ToByte(param.PacketSize & 0xFF));
                        buff.Add(Convert.ToByte((param.PacketSize >> 8) & 0xFF));

                        seek = param.PacketSize * i;
                        buff.Add(Convert.ToByte(seek & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                        if (i == 0)
                        {
                            buff.Add(Convert.ToByte(length & 0xFF));
                            buff.Add(Convert.ToByte((length >> 8) & 0xFF));
                            buff.Add(Convert.ToByte((length >> 16) & 0xFF));
                            buff.Add(Convert.ToByte((length >> 24) & 0xFF));
                            //crc
                            buff.Add(Convert.ToByte(crc & 0xFF));
                            buff.Add(Convert.ToByte((crc >> 8) & 0xFF));
                            buff.Add(Convert.ToByte((crc >> 16) & 0xFF));
                            buff.Add(Convert.ToByte((crc >> 24) & 0xFF));
                        }
                        file.Read(readBuff, 0, param.PacketSize);
                        buff.AddRange(readBuff);
                        this.Send(buff.ToArray());
                        if (seek % param.PartitionIndex == 0)
                        {
                            this.ThreadSleep(param.PacketIntervalTimeByPartitionIndex);
                        }
                        else
                        {
                            if (i == 0)
                            {
                                this.ThreadSleep(param.FirstPacketIntervalTime);
                            }
                            else
                            {
                                this.ThreadSleep(param.PacketIntervalTime);
                            }
                        }
                        buff.Clear();
                        double progress = ((i + 1) * param.PacketSize * 100) / (1.0 * length);
                        this.Progress_Event((int)Math.Floor(progress));
                    }
                    buff.AddRange(packetHead);
                    buff.Add(Convert.ToByte(lastPackageSize & 0xFF));
                    buff.Add(Convert.ToByte((lastPackageSize >> 8) & 0xFF));
                    seek = param.PacketSize * packetCount;
                    buff.Add(Convert.ToByte(seek & 0xFF));
                    buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                    buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                    buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                    if (length < param.PacketSize)
                    {
                        buff.Add(Convert.ToByte(length & 0xFF));
                        buff.Add(Convert.ToByte((length >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((length >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((length >> 24) & 0xFF));
                        //crc
                        buff.Add(Convert.ToByte(crc & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 24) & 0xFF));
                    }
                    readBuff = new byte[lastPackageSize];
                    file.Read(readBuff, 0, lastPackageSize);
                    buff.AddRange(readBuff);
                    this.Send(buff.ToArray());
                }
                byte[] endPacket = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xFF };
                this.Send(endPacket);
                this.ThreadSleep(param.FPGAUpdateCompletedIntervalTime);
                this.Progress_Event(100);
                this.TaskCompleted("下载字库成功");
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError("下载字库失败");
            }
        }


        private void WriteData(Completed completed, Error error)
        {
            this.Completed_Event = completed;
            this.Error_Event = error;
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.CurrentModule = Module.WriteData;
                    this.TaskTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => WriteDataTask(s, e));
                    this.TaskTimer.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError();
            }
        }
        private void WriteDataTask(Object obj, ElapsedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError();
            }
        }
        private void IsEncrypt(Completed completed, Error error)
        {
            this.Completed_Event = completed;
            this.Error_Event = error;
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.CurrentModule = Module.IsEncrypt;
                    this.TaskTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => IsEncryptTask(s, e));
                    this.TaskTimer.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError();
            }
        }
        private void IsEncryptTask(Object obj, ElapsedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.TaskError();
            }
        }


        protected void ReceiveManage(List<byte> recData)
        {
            switch (this.CurrentModule)
            {
                case Module.SearchDevice:
                    this.SearchDeviceReceiveManage(recData);
                    break;
                case Module.ReadDeviceId:
                    this.ReadDeviceIdReceiveManage(recData);
                    break;
                case Module.WriteEncrypt:
                    this.WriteEncryptReceiveManage(recData);
                    break;
                case Module.WriteData:
                    this.WriteDataReceiveManage(recData);
                    break;
                case Module.WriteParam:
                    this.WriteParamReceiveManage(recData);
                    break;
                case Module.IsEncrypt:
                    this.IsEncryptReceiveManage(recData);
                    break;
            }
        }
        private void SearchDeviceReceiveManage(List<byte> recData)
        {
            try
            {
                //CSJConf conf = CSJConf.Build(recData.ToArray());
                CSJConf conf = CSJConf.BuildParamEmtity(recData.ToArray());

                this.SearchDeviceCompleted(conf);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.SearchDeviceError();
            }
        }
        private void SearchDeviceCompleted(Object obj)
        {
            if (obj != null)
            {
                this.TaskCompleted(obj,"搜索设备成功");
            }else
            {
                this.TaskError("搜索设备失败");
            }
        }
        private void SearchDeviceError()
        {
            this.TaskError();
        }
        private void ReadDeviceIdReceiveManage(List<byte> recData)
        {
            bool flag = false;
            if (flag)
            {
                this.ReadDeviceIdCompleted();
            }
            else
            {
                this.ReadDeviceIdError();
            }
        }
        private void ReadDeviceIdCompleted()
        {
            this.TaskCompleted();
        }
        private void ReadDeviceIdError()
        {
            this.TaskError();
        }
        private void WriteEncryptReceiveManage(List<byte> recData)
        {

        }
        private void WriteEncryptCompleted()
        {
            this.TaskCompleted();
        }
        private void WriteEncryptError()
        {
            this.TaskError();
        }

        private void WriteDataReceiveManage(List<byte> recData)
        {
            bool flag = false;
            if (flag)
            {
                this.WriteDataCompleted();
            }
            else
            {
                this.WriteDataError();
            }
        }
        private void WriteDataCompleted()
        {
            this.TaskCompleted();
        }
        private void WriteDataError()
        {
            this.TaskError();
        }

        private void WriteParamReceiveManage(List<byte> recData)
        {
            bool flag = false;
            if (flag)
            {
                this.WriteParamCompleted();
            }
            else
            {
                this.WriteParamError();
            }
        }
        private void WriteParamCompleted()
        {
            this.TaskCompleted();
        }
        private void WriteParamError()
        {
            this.TaskError();
        }

        private void IsEncryptReceiveManage(List<byte> recData)
        {
            bool flag = false;
            if (flag)
            {
                this.IsEncryptCompleted();
            }
            else
            {
                this.IsEncryptError();
            }
        }
        private void IsEncryptCompleted()
        {
            this.TaskCompleted();
        }
        private void IsEncryptError()
        {
            this.TaskError();
        }

        protected enum Module
        {
            SearchDevice,
            ReadDeviceId,
            WriteEncrypt,
            UpdateFPGA256,
            UpdataMCU256,
            WriteData,
            WriteParam,
            IsEncrypt,
            DownloadWordBank,
            Null
        }
    }
}
