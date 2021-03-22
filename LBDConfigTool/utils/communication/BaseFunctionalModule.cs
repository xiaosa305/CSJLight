using LBDConfigTool.utils.conf;
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
        private const int PACKSIZE = 256;

        protected System.Timers.Timer TimeOut { get; set; }
        private System.Timers.Timer TaskTimer { get; set; }
        private Module CurrentModule { get; set; }
        private bool IsSending { get; set; }
        public delegate void Completed(Object obj, string msg);
        public delegate void Error(string msg);
        private Completed Completed_Event { get; set; }
        private Error Error_Event { get; set; }
        protected ConcurrentQueue<List<byte>> MessageQueue { get; set; }
        private System.Timers.Timer MessageTransaction { get; set; }
        private void ThreadSleep(int time)
        {
            timeBeginPeriod(1);
            Thread.Sleep(time);
            timeEndPeriod(1);
        }

        private int PacketIntervalTime { get; set; }
        private int PacketIntervalTimeBySPAN { get; set; }
        private int SPANCount { get; set; }

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
            this.SPANCount = 1024 * 4;
            this.PacketIntervalTime = 5;
            this.PacketIntervalTimeBySPAN = 50;
            this.MessageTransaction = new System.Timers.Timer() { AutoReset = false };
            this.MessageTransaction.Elapsed += this.MessageTransactionTask;
            this.MessageQueue = new ConcurrentQueue<List<byte>>();
            this.TimeOut = new System.Timers.Timer(TIME_OUT_COUNT) { AutoReset = false };
            this.TimeOut.Elapsed += this.TimeOutTask;
            this.CurrentModule = Module.Null;
            this.IsSending = false;
            this.MessageTransaction.Start();
        }
        public void SetPacketIntervalTime(int time)
        {
            this.PacketIntervalTime = time;
        }
        public void SetPacketIntervalTimeBySPAN (int time)
        {
            this.PacketIntervalTimeBySPAN = time;
        }
        public void SetSPANCount(int cout)
        {
            this.SPANCount = cout;
        }
        private void InitParam()
        {
            this.TaskTimer = null;
            this.Error_Event = null;
            this.Completed_Event = null;
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
            this.TaskError("通信超时");
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
                this.TaskTimer.Stop();
                this.InitParam();
                this.Error_Event(msg);
            }
        }
        //消息事务管理器
        private void MessageTransactionTask(Object obj, ElapsedEventArgs e)
        {
            while (this.MessageTransaction.Enabled)
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
                    byte[] order = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xB0 };
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
        //读取设备信息TODO
        public void ReadDeviceId(Completed completed, Error error)
        {
            this.Completed_Event = completed;
            this.Error_Event = error;
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.CurrentModule = Module.ReadDeviceId;
                    this.TaskTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => ReadDeviceIdTask(s, e));
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
        private void ReadDeviceIdTask(Object obj, ElapsedEventArgs e)
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
        public void WriteEncrypt(Completed completed, Error error)
        {
            this.Completed_Event = completed;
            this.Error_Event = error;
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.CurrentModule = Module.WriteEncrypt;
                    byte[] data = new byte[] { 0xAA, 0xBB, 0x04, 0x04 };
                    this.Send(data);
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
        private void WriteEncryptTask(byte[] sourceData)
        {
            try
            {
                byte[] desData = new byte[12];
                //加密
                ;
                //发送
                List<byte> buff = new List<byte>();
                buff.Add(0xAA);
                buff.Add(0xBB);
                buff.Add(0x05);
                buff.Add(0x05);
                buff.AddRange(desData);
                this.Send(buff.ToArray());
                this.WriteEncryptCompleted();
            }
            catch (Exception ex)
            {
                this.IsSending = false;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.WriteEncryptError();
            }
        }
        //升级FPGA
        public void UpdateFPGA256(string filePath,Completed completed, Error error)
        {
            this.Completed_Event = completed;
            this.Error_Event = error;
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
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => UpdateFPGA256Task(filePath,s, e));
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
        private void UpdateFPGA256Task(string filePath, Object obj, ElapsedEventArgs e)
        {
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open))
                {
                    byte[] order = new byte[] { 0xAA,0xBB,0x00,0x00,0xC0};
                    this.Send(order);
                    int seek = 0;
                    long length = file.Length;
                    bool flag = file.Length % PACKSIZE == 0;
                    int lastPackageSize = flag ? PACKSIZE : (int)(length % PACKSIZE);
                    int packetCount = (int)(length / PACKSIZE);
                    List<byte> buff = new List<byte>();
                    byte[] packetHead = new byte[] { 0xAA, 0xBB, 0x02, 0x02, 0xA1 };
                    byte[] readBuff = new byte[PACKSIZE];
                    for (int i = 0; i < packetCount; i++)
                    {
                        buff.AddRange(packetHead);
                        buff.Add(0xFF);
                        buff.Add(0x00);
                        seek = PACKSIZE * i;
                        buff.Add(Convert.ToByte(seek & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                        file.Read(readBuff, 0, PACKSIZE);
                        buff.AddRange(readBuff);
                        this.Send(buff.ToArray());
                        if (seek % this.SPANCount == 0)
                        {
                            this.ThreadSleep(this.PacketIntervalTimeBySPAN);
                        }
                        else
                        {
                            this.ThreadSleep(this.PacketIntervalTime);
                        }
                        buff.Clear();
                    }
                    buff.AddRange(packetHead);
                    buff.Add(Convert.ToByte(lastPackageSize & 0xFF));
                    buff.Add(Convert.ToByte((lastPackageSize >> 8) & 0xFF));
                    seek = PACKSIZE * packetCount;
                    buff.Add(Convert.ToByte(seek & 0xFF));
                    buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                    buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                    readBuff = new byte[lastPackageSize];
                    file.Read(readBuff, 0, lastPackageSize);
                    buff.AddRange(readBuff);
                    this.Send(buff.ToArray());
                    this.Send(Encoding.Default.GetBytes("SendEnd"));
                    this.TaskCompleted();
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
        //升级MCU
        public void UpdataMCU256(string filePath,Completed completed, Error error)
        {
            this.Completed_Event = completed;
            this.Error_Event = error;
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
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => UpdataMCU256Task(filePath,s, e));
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
        private void UpdataMCU256Task(string filePath, Object obj, ElapsedEventArgs e)
        {
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open))
                {
                    byte[] order = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xB0 };
                    this.Send(order);
                    int seek = 0;
                    long length = file.Length;
                    bool flag = file.Length % PACKSIZE == 0;
                    int lastPackageSize = flag ? PACKSIZE : (int)(length % PACKSIZE);
                    int packetCount = (int)(length / PACKSIZE);
                    List<byte> buff = new List<byte>();
                    byte[] packetHead = new byte[] { 0xAA, 0xBB, 0x02, 0x02, 0xA1 };
                    byte[] readBuff = new byte[PACKSIZE];
                    for (int i = 0; i < packetCount; i++)
                    {
                        buff.AddRange(packetHead);
                        buff.Add(0xFF);
                        buff.Add(0x00);
                        seek = PACKSIZE * i;
                        buff.Add(Convert.ToByte(seek & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                        file.Read(readBuff, 0, PACKSIZE);
                        buff.AddRange(readBuff);
                        this.Send(buff.ToArray());
                        if (seek % this.SPANCount == 0)
                        {
                            this.ThreadSleep(this.PacketIntervalTimeBySPAN);
                        }
                        else
                        {
                            this.ThreadSleep(this.PacketIntervalTime);
                        }
                        buff.Clear();
                    }
                    buff.AddRange(packetHead);
                    buff.Add(Convert.ToByte(lastPackageSize & 0xFF));
                    buff.Add(Convert.ToByte((lastPackageSize >> 8) & 0xFF));
                    seek = PACKSIZE * packetCount;
                    buff.Add(Convert.ToByte(seek & 0xFF));
                    buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                    buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                    readBuff = new byte[lastPackageSize];
                    file.Read(readBuff, 0, lastPackageSize);
                    buff.AddRange(readBuff);
                    this.Send(buff.ToArray());
                    this.Send(Encoding.Default.GetBytes("SendEnd"));
                    this.TaskCompleted();
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
                byte[] data = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xA0, Convert.ToByte((sourceData.Length + 5) & 0xFF), Convert.ToByte(((sourceData.Length + 5) >> 8) & 0xFF), Convert.ToByte((sourceData.Length + 10) & 0xFF), Convert.ToByte(((sourceData.Length + 10) >> 8) & 0xFF), Convert.ToByte(((sourceData.Length + 5) >> 16) & 0xFF) };
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
                CSJConf conf = CSJConf.Build(recData.ToArray());
                this.SearchDeviceCompleted(conf);

            }
            catch (Exception)
            {
                this.SearchDeviceError();
            }
        }
        private void SearchDeviceCompleted(Object obj)
        {
            if (obj != null)
            {
                this.TaskCompleted(obj,"read config success");
            }else
            {
                this.TaskError("read config failed");
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
            this.WriteEncryptTask(recData.ToArray());
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
            Null
        }
    }
}
