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
using System.Threading.Tasks;
using System.Timers;
using static LBDConfigTool.utils.cusdelegate.CallbackFunction;

namespace LBDConfigTool.utils.communication
{
    public abstract class FunctionModule
    {
        //功能类别枚举
        public enum FunctionModuleType
        {
            UpdateFPGA256ByAnswer,
            UpdateMCU256ByAnswer,
            DownloadFontByAnswer,
            UpdateFPGA256,
            UpdataMCU256,
            DownloadFont,
            SearchDevice,
            WriteEncrypt,
            WriteParam,
            Null
        }
        //虚函数接口
        protected abstract void Send(byte[] data);
        //导入DLL库
        [DllImport("winmm.dll")] internal static extern uint timeBeginPeriod(uint period);
        [DllImport("winmm.dll")] internal static extern uint timeEndPeriod(uint period);

        protected bool IsWorking { get; set; }
        protected bool IsAnswerMode { get; set; }
        protected int ResendCount { get; set; }
        protected byte[] LastPackageData { get; set; }
        protected System.Timers.Timer Timeout_Timer { get; set; }//超时计时器
        protected Thread ReadRecMessageTask { get; set; }//循环读取回复消息线程
        protected FunctionModuleType CurrentModuleType { get; set; }//当前功能模块类别
        protected CompletedByObjAndMsg Completed { get; set; }//任务完成回调
        protected ErrorByMsg Error { get; set; }//任务失败回调
        protected TaskProgress Progress { get; set; }//任务进度回调
        protected ConcurrentQueue<List<byte>> RecMessageQueue { get; set; }//回复消息缓存队列
        protected string DownloadFilePath { get; set; }//下载文件路径
        protected ParamEntity Param { get; set; }//下载配置参数
        protected int FirmwarePackageCount { get; set; }
        protected int FirmwarePackageIndex { get; set; }
        protected void Init()
        {
            ResendCount = 0;
            FirmwarePackageIndex = 0;
            FirmwarePackageCount = 0;
            IsAnswerMode = false;
            IsWorking = false;
            RecMessageQueue = new ConcurrentQueue<List<byte>>();
            CurrentModuleType = FunctionModuleType.Null;
            Timeout_Timer = new System.Timers.Timer(5000) { AutoReset = false };
            Timeout_Timer.Elapsed += this.TimeOutTimerTask;
            ReadRecMessageTask = new Thread(new ThreadStart(RecMessageTranscation)) { IsBackground = true };
            ReadRecMessageTask.Start();

        }
        protected void ThreadSleep(int time)
        {
            timeBeginPeriod(1);
            Thread.Sleep(time);
            timeEndPeriod(1);
        }
        protected void SendCompleted()
        {
            switch (this.CurrentModuleType)
            {
                case FunctionModuleType.UpdateFPGA256ByAnswer:
                case FunctionModuleType.UpdateMCU256ByAnswer:
                case FunctionModuleType.DownloadFontByAnswer:
                    if (this.IsAnswerMode)
                    {
                        this.StartTimout();
                    }
                    break;
            }
        }
        protected void TaskCompleted(Object obj, string msg)
        {
            IsWorking = false;
            CurrentModuleType = FunctionModuleType.Null;
            Completed(obj, msg);
        }
        protected void TaskError(string errorMsg)
        {
            IsWorking = false;
            CurrentModuleType = FunctionModuleType.Null;
            Error(errorMsg);
        }
        //超时执行器
        protected void TimeOutTimerTask(Object senderr, ElapsedEventArgs e)
        {
            if (IsAnswerMode && this.ResendCount < 3)
            {
                new Thread(new ThreadStart(Resend)) { IsBackground = true }.Start();
            }
            else
            {
                string value = "";
                switch (CurrentModuleType)
                {
                    case FunctionModuleType.WriteEncrypt:
                        value = "加密固件";
                        break;
                    case FunctionModuleType.UpdateFPGA256ByAnswer:
                        value = "升级FPGA";
                        break;
                    case FunctionModuleType.UpdateMCU256ByAnswer:
                        value = "升级MCU";
                        break;
                    case FunctionModuleType.DownloadFontByAnswer:
                        value = "下载字库";
                        break;
                    case FunctionModuleType.WriteParam:
                        value = "下载配置参数";
                        break;
                }
                value += "失败";
                TaskError(value);
            }
        }
        //启动超时计时器
        protected void StartTimout()
        {
            Timeout_Timer.Start();
        }
        //关闭超时计时器
        protected void StopTimeout()
        {
            Timeout_Timer.Stop();
        }
        //超时重发执行器
        protected void Resend()
        {
            if (LastPackageData != null)
            {
                Send(LastPackageData);
            }
            ResendCount++;
        }
        //消息回复管理器
        protected void RecMessageTranscation()
        {
            while (true)
            {
                if (RecMessageQueue.Count > 0)
                {
                    RecMessageQueue.TryDequeue(out List<byte> data);
                    switch (CurrentModuleType)
                    {
                        case FunctionModuleType.UpdateFPGA256ByAnswer:
                            UpdataFPGA256AnswerManage(data);
                            break;
                        case FunctionModuleType.UpdateMCU256ByAnswer:
                            UpdataMCU256AnswerManage(data);
                            break;
                        case FunctionModuleType.DownloadFontByAnswer:
                            DownloadFontLibraryAnswerManage(data);
                            break;
                        case FunctionModuleType.SearchDevice:
                            SearchDeviceAnswerManager(data);
                            break;
                        case FunctionModuleType.WriteEncrypt:
                            break;
                        case FunctionModuleType.WriteParam:
                            break;
                        case FunctionModuleType.Null:
                            break;
                    }
                }
                else
                {
                    Thread.Sleep(20);
                }
            }
        }

        //搜索设备模块
        public bool SearchDevice(CompletedByObjAndMsg completed,ErrorByMsg error)
        {
            if (!IsWorking)
            {
                Completed = completed;
                Error = error;
                new Thread(new ThreadStart(SearchDeviceTask)) { IsBackground = true }.Start();
                return true;
            }
            return false;
        }
        protected void SearchDeviceTask()
        {
            try
            {
                IsWorking = true;
                CurrentModuleType = FunctionModuleType.SearchDevice;
                byte[] order = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xA1 };
                LastPackageData = order;
                Send(order);
                IsWorking = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("搜索设备失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
                TaskError("搜索设备失败");
            }
        }
        protected void SearchDeviceAnswerManager(List<byte> data)
        {
            try
            {
                CSJConf conf = CSJConf.BuildParamEmtity(data.ToArray());
                if (conf != null)
                {
                    TaskCompleted(conf, "搜索设备成功");
                }
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("搜索设备失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
            }
            TaskError("搜索设备失败");
        }
        //写入配置参数
        public bool WriteParam(CSJConf conf, CompletedByObjAndMsg completed, ErrorByMsg error)
        {
            if (!IsWorking)
            {
                Completed = completed;
                Error = error;
                new Thread(new ParameterizedThreadStart(WriteParamTask)) { IsBackground = true }.Start(conf);
                return true;
            }
            return false;
        }
        protected void WriteParamTask(Object obj)
        {
            try
            {
                IsWorking = true;
                CurrentModuleType = FunctionModuleType.WriteParam;
                CSJConf conf = obj as CSJConf;
                byte[] sourceData = conf.GetData();
                byte[] data = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xA0, Convert.ToByte((sourceData.Length) & 0xFF), Convert.ToByte(((sourceData.Length) >> 8) & 0xFF), 0x00, 0x00, 0x00, 0x00 };
                List<byte> buff = new List<byte>();
                buff.AddRange(data);
                buff.AddRange(sourceData);
                this.Send(buff.ToArray());
                this.TaskCompleted(null,"下载配置参数成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine("下载配置参数失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
                this.TaskError("下载配置参数失败");
            }
        }
        //加密模块
        public bool WriteEncrypt(string pwd, CompletedByObjAndMsg completed, ErrorByMsg error)
        {
            if (!IsWorking)
            {
                Completed = completed;
                Error = error;
                new Thread(new ParameterizedThreadStart(WriteEncryptTask)) { IsBackground = true }.Start(pwd);
                return true;
            }
            return false;
        }
        protected void WriteEncryptTask(Object obj)
        {
            try
            {
                string pwd = obj as string;
                IsWorking = true;
                CurrentModuleType = FunctionModuleType.WriteEncrypt;
                byte[] order = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xD0 };
                List<byte> buff = new List<byte>();
                buff.AddRange(order);
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
                Send(buff.ToArray());
                TaskCompleted(null, "加密成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine("加密失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
                TaskError("加密失败");
            }
        }
        //升级FPGA模块
        public bool UpdataFPGA256(string filePath,bool isAnswerMode, ParamEntity param,TaskProgress progress,CompletedByObjAndMsg completed,ErrorByMsg error)
        {
            if (!IsWorking)
            {
                IsAnswerMode = isAnswerMode;
                Completed = completed;
                Error = error;
                Progress = progress;
                DownloadFilePath = filePath;
                Param = param;
                using (FileStream stream = new FileStream( DownloadFilePath,FileMode.Open))
                {
                    FirmwarePackageCount = (int)((stream.Length / Param.PacketSize) + (stream.Length % Param.PacketSize == 0 ? 0 : 1));
                }
                new Thread(new ThreadStart(UpdataFPGA256Task)) { IsBackground = true }.Start();
                return true;
            }
            return false;
        }
        protected void UpdataFPGA256Task()
        {
            try
            {
                IsWorking = true;
                CurrentModuleType = IsAnswerMode ? FunctionModuleType.UpdateFPGA256ByAnswer : FunctionModuleType.UpdateFPGA256;
                if (IsAnswerMode)
                {
                    using (FileStream stream = new FileStream(DownloadFilePath,FileMode.Open))
                    {
                        if (FirmwarePackageIndex == FirmwarePackageCount)
                        {
                            byte[] packageEnd = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xFF };
                            Send(packageEnd);
                            FirmwarePackageIndex++;
                        }
                        else
                        {
                            int seek = FirmwarePackageIndex * Param.PacketSize;
                            int readLength = 0;
                            List<byte> buff = new List<byte>();
                            byte[] packageHead = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xC0 };
                            byte[] data = new byte[Param.PacketSize];
                            readLength = stream.Read(data, 0, Param.PacketSize);
                            buff.AddRange(packageHead);
                            buff.Add(Convert.ToByte(readLength & 0xFF));
                            buff.Add(Convert.ToByte((readLength >> 8) & 0xFF));
                            buff.Add(Convert.ToByte(seek & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                            if (FirmwarePackageIndex == 0)
                            {
                                byte[] crcData = new byte[stream.Length];
                                stream.Seek(0, SeekOrigin.Begin);
                                stream.Read(crcData, 0, crcData.Length);
                                uint crc = Crc32SUM.GetSumCRC(crcData);
                                buff.Add(Convert.ToByte(stream.Length & 0xFF));
                                buff.Add(Convert.ToByte((stream.Length >> 8) & 0xFF));
                                buff.Add(Convert.ToByte((stream.Length >> 16) & 0xFF));
                                buff.Add(Convert.ToByte((stream.Length >> 24) & 0xFF));
                                buff.Add(Convert.ToByte(crc & 0xFF));
                                buff.Add(Convert.ToByte((crc >> 8) & 0xFF));
                                buff.Add(Convert.ToByte((crc >> 16) & 0xFF));
                                buff.Add(Convert.ToByte((crc >> 24) & 0xFF));
                            }
                            buff.AddRange(data);
                            Send(buff.ToArray());
                            if (seek % Param.PartitionIndex == 0) ThreadSleep(Param.PacketIntervalTimeByPartitionIndex);
                            else Thread.Sleep(FirmwarePackageIndex == 0 ? Param.FirstPacketIntervalTime : Param.PacketIntervalTime);
                            buff.Clear();
                            Progress((int)Math.Floor((FirmwarePackageIndex + 1) * Param.PacketSize * 100 / (1.0 * stream.Length)));
                            FirmwarePackageIndex++;
                        }
                    }
                }
                else
                {
                    using (FileStream stream = new FileStream(DownloadFilePath, FileMode.Open))
                    {
                        int seek = 0;
                        int readLength = 0;
                        int fileSize = (int)stream.Length;
                        List<byte> buff = new List<byte>();
                        byte[] packageHead = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xC0 };
                        byte[] data = new byte[fileSize];
                        stream.Read(data, 0, data.Length);
                        uint crc = Crc32SUM.GetSumCRC(data);
                        data = new byte[Param.PacketSize];
                        stream.Seek(0, SeekOrigin.Begin);
                        readLength = stream.Read(data, 0, Param.PacketSize);
                        buff.AddRange(packageHead);
                        buff.Add(Convert.ToByte(readLength & 0xFF));
                        buff.Add(Convert.ToByte((readLength >> 8) & 0xFF));
                        buff.Add(Convert.ToByte(seek & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                        buff.Add(Convert.ToByte(fileSize & 0xFF));
                        buff.Add(Convert.ToByte((fileSize >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((fileSize >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((fileSize >> 24) & 0xFF));
                        buff.Add(Convert.ToByte(crc & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 24) & 0xFF));
                        buff.AddRange(data);
                        Send(buff.ToArray());
                        if (seek % Param.PartitionIndex == 0) ThreadSleep(Param.PacketIntervalTimeByPartitionIndex);
                        else Thread.Sleep(Param.FirstPacketIntervalTime);
                        buff.Clear();
                        Progress((int)Math.Floor(Param.PacketSize * 100 / (1.0 * stream.Length)));
                        for (FirmwarePackageIndex = 1; FirmwarePackageIndex < FirmwarePackageCount; FirmwarePackageIndex++)
                        {
                            data = new byte[Param.PacketSize];
                            readLength = stream.Read(data, 0, Param.PacketSize);
                            seek = FirmwarePackageIndex * Param.PacketSize;
                            buff.AddRange(packageHead);
                            buff.Add(Convert.ToByte(readLength & 0xFF));
                            buff.Add(Convert.ToByte((readLength >> 8) & 0xFF));
                            buff.Add(Convert.ToByte(seek & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                            buff.AddRange(data);
                            Send(buff.ToArray());
                            if (seek % Param.PartitionIndex == 0) ThreadSleep(Param.PacketIntervalTimeByPartitionIndex);
                            else Thread.Sleep(Param.PacketIntervalTime);
                            buff.Clear();
                            Progress((int)Math.Floor((FirmwarePackageIndex + 1) * Param.PacketSize * 100 / (1.0 * stream.Length)));
                        }
                        byte[] packageEnd = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xFF };
                        Send(packageEnd);
                        ThreadSleep(Param.FPGAUpdateCompletedIntervalTime);
                        Progress(100);
                        FirmwarePackageIndex = 0;
                        FirmwarePackageCount = 0;
                        TaskCompleted(null, "升级FPGA成功");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("升级FPGA失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
                TaskError("升级FPGA失败");
            }
        }
        protected void UpdataFPGA256AnswerManage(List<byte> data)
        {
            try
            {
                string recStr = Encoding.Default.GetString(data.ToArray());
                if (FirmwarePackageIndex == FirmwarePackageCount + 1)
                {
                    ThreadSleep(Param.FPGAUpdateCompletedIntervalTime);
                    Progress(100);
                    FirmwarePackageIndex = 0;
                    FirmwarePackageCount = 0;
                    TaskCompleted(null, "升级FPGA成功");
                }
                else
                {
                    UpdataFPGA256Task();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("升级FPGA失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
                TaskError("升级FPGA失败");
            }
        }
        //升级MCU模块
        public bool UpdataMCU256(string filePath, bool isAnswerMode, ParamEntity param, TaskProgress progress, CompletedByObjAndMsg completed, ErrorByMsg error)
        {
            if (!IsWorking)
            {
                IsAnswerMode = isAnswerMode;
                Completed = completed;
                Error = error;
                Progress = progress;
                DownloadFilePath = filePath;
                Param = param;
                using (FileStream stream = new FileStream(DownloadFilePath, FileMode.Open))
                {
                    FirmwarePackageCount = (int)((stream.Length / Param.PacketSize) + (stream.Length % Param.PacketSize == 0 ? 0 : 1));
                }
                new Thread(new ThreadStart(UpdataMCU256Task)) { IsBackground = true }.Start();
                return true;
            }
            return false;
        }
        protected void UpdataMCU256Task()
        {
            try
            {
                IsWorking = true;
                CurrentModuleType = IsAnswerMode ? FunctionModuleType.UpdateMCU256ByAnswer : FunctionModuleType.UpdataMCU256;
                if (IsAnswerMode)
                {

                    using (FileStream stream = new FileStream(DownloadFilePath, FileMode.Open))
                    {
                        if (FirmwarePackageIndex == FirmwarePackageCount)
                        {
                            byte[] packageEnd = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xFF };
                            Send(packageEnd);
                            FirmwarePackageIndex++;
                        }
                        else
                        {
                            int seek = FirmwarePackageIndex * Param.PacketSize;
                            int readLength = 0;
                            List<byte> buff = new List<byte>();
                            byte[] packageHead = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xB0 };

                            byte[] data = new byte[Param.PacketSize];
                            readLength = stream.Read(data, 0, Param.PacketSize);
                            buff.AddRange(packageHead);
                            buff.Add(Convert.ToByte(readLength & 0xFF));
                            buff.Add(Convert.ToByte((readLength >> 8) & 0xFF));
                            buff.Add(Convert.ToByte(seek & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                            if (FirmwarePackageIndex == 0)
                            {
                                byte[] crcData = new byte[stream.Length];
                                stream.Seek(0, SeekOrigin.Begin);
                                stream.Read(crcData, 0, crcData.Length);
                                uint crc = Crc32SUM.GetSumCRC(crcData);
                                buff.Add(Convert.ToByte(stream.Length & 0xFF));
                                buff.Add(Convert.ToByte((stream.Length >> 8) & 0xFF));
                                buff.Add(Convert.ToByte((stream.Length >> 16) & 0xFF));
                                buff.Add(Convert.ToByte((stream.Length >> 24) & 0xFF));
                                buff.Add(Convert.ToByte(crc & 0xFF));
                                buff.Add(Convert.ToByte((crc >> 8) & 0xFF));
                                buff.Add(Convert.ToByte((crc >> 16) & 0xFF));
                                buff.Add(Convert.ToByte((crc >> 24) & 0xFF));
                            }
                            buff.AddRange(data);
                            Send(buff.ToArray());
                            if (seek % Param.PartitionIndex == 0) ThreadSleep(Param.PacketIntervalTimeByPartitionIndex);
                            else Thread.Sleep(FirmwarePackageIndex == 0 ? Param.FirstPacketIntervalTime : Param.PacketIntervalTime);
                            buff.Clear();
                            Progress((int)Math.Floor((FirmwarePackageIndex + 1) * Param.PacketSize * 100 / (1.0 * stream.Length)));
                            FirmwarePackageIndex++;
                        }
                    }
                }
                else
                {
                    using (FileStream stream = new FileStream(DownloadFilePath, FileMode.Open))
                    {
                        int seek = 0;
                        int readLength = 0;
                        int fileSize = (int)stream.Length;
                        List<byte> buff = new List<byte>();
                        byte[] packageHead = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xB0 };
                        byte[] data = new byte[fileSize];
                        stream.Read(data, 0, data.Length);
                        uint crc = Crc32SUM.GetSumCRC(data);
                        data = new byte[Param.PacketSize];
                        stream.Seek(0, SeekOrigin.Begin);
                        readLength = stream.Read(data, 0, Param.PacketSize);
                        buff.AddRange(packageHead);
                        buff.Add(Convert.ToByte(readLength & 0xFF));
                        buff.Add(Convert.ToByte((readLength >> 8) & 0xFF));
                        buff.Add(Convert.ToByte(seek & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                        buff.Add(Convert.ToByte(fileSize & 0xFF));
                        buff.Add(Convert.ToByte((fileSize >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((fileSize >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((fileSize >> 24) & 0xFF));
                        buff.Add(Convert.ToByte(crc & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 24) & 0xFF));
                        buff.AddRange(data);
                        Send(buff.ToArray());
                        if (seek % Param.PartitionIndex == 0) ThreadSleep(Param.PacketIntervalTimeByPartitionIndex);
                        else Thread.Sleep(Param.FirstPacketIntervalTime);
                        buff.Clear();
                        Progress((int)Math.Floor(Param.PacketSize * 100 / (1.0 * stream.Length)));
                        for (FirmwarePackageIndex = 1; FirmwarePackageIndex < FirmwarePackageCount; FirmwarePackageIndex++)
                        {
                            data = new byte[Param.PacketSize];
                            readLength = stream.Read(data, 0, Param.PacketSize);
                            seek = FirmwarePackageIndex * Param.PacketSize;
                            buff.AddRange(packageHead);
                            buff.Add(Convert.ToByte(readLength & 0xFF));
                            buff.Add(Convert.ToByte((readLength >> 8) & 0xFF));
                            buff.Add(Convert.ToByte(seek & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                            buff.AddRange(data);
                            Send(buff.ToArray());
                            if (seek % Param.PartitionIndex == 0) ThreadSleep(Param.PacketIntervalTimeByPartitionIndex);
                            else Thread.Sleep(Param.PacketIntervalTime);
                            buff.Clear();
                            Progress((int)Math.Floor((FirmwarePackageIndex + 1) * Param.PacketSize * 100 / (1.0 * stream.Length)));
                        }
                        byte[] packageEnd = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xFF };
                        Send(packageEnd);
                        ThreadSleep(Param.FPGAUpdateCompletedIntervalTime);
                        Progress(100);
                        FirmwarePackageIndex = 0;
                        FirmwarePackageCount = 0;
                        TaskCompleted(null, "升级MCU成功");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("升级MCU失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
                TaskError("升级MCU失败");
            }
        }
        protected void UpdataMCU256AnswerManage(List<byte> data)
        {
            try
            {
                string recStr = Encoding.Default.GetString(data.ToArray());
                if (FirmwarePackageIndex == FirmwarePackageCount + 1)
                {
                    ThreadSleep(Param.FPGAUpdateCompletedIntervalTime);
                    Progress(100);
                    FirmwarePackageIndex = 0;
                    FirmwarePackageCount = 0;
                    TaskCompleted(null, "升级MCU成功");
                }
                else
                {
                    UpdataMCU256Task();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("升级MCU失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
                this.TaskError("升级MCU失败");
            }
        }
        //下载字库
        public bool DownloadFontLibrary(string filePath, bool isAnswerMode, ParamEntity param, TaskProgress progress, CompletedByObjAndMsg completed, ErrorByMsg error)
        {
            if (!IsWorking)
            {
                IsAnswerMode = isAnswerMode;
                Completed = completed;
                Error = error;
                Progress = progress;
                DownloadFilePath = filePath;
                Param = param;
                using (FileStream stream = new FileStream(DownloadFilePath, FileMode.Open))
                {
                    FirmwarePackageCount = (int)((stream.Length / Param.PacketSize) + (stream.Length % Param.PacketSize == 0 ? 0 : 1));
                }
                new Thread(new ThreadStart(DownloadFontLibraryTask)) { IsBackground = true }.Start();
                return true;
            }
            return false;
        }
        protected void DownloadFontLibraryTask()
        {
            try
            {
                IsWorking = true;
                CurrentModuleType = IsAnswerMode ? FunctionModuleType.DownloadFontByAnswer : FunctionModuleType.DownloadFont;
                if (IsAnswerMode)
                {
                    using (FileStream stream = new FileStream(DownloadFilePath, FileMode.Open))
                    {
                        if (FirmwarePackageIndex == FirmwarePackageCount)
                        {
                            byte[] packageEnd = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xFF };
                            Send(packageEnd);
                            FirmwarePackageIndex++;
                        }
                        else
                        {
                            int seek = FirmwarePackageIndex * Param.PacketSize;
                            int readLength = 0;
                            List<byte> buff = new List<byte>();
                            byte[] packageHead = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xE0 };
                            byte[] data = new byte[Param.PacketSize];
                            readLength = stream.Read(data, 0, Param.PacketSize);
                            buff.AddRange(packageHead);
                            buff.Add(Convert.ToByte(readLength & 0xFF));
                            buff.Add(Convert.ToByte((readLength >> 8) & 0xFF));
                            buff.Add(Convert.ToByte(seek & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                            if (FirmwarePackageIndex == 0)
                            {
                                byte[] crcData = new byte[stream.Length];
                                stream.Seek(0, SeekOrigin.Begin);
                                stream.Read(crcData, 0, crcData.Length);
                                uint crc = Crc32SUM.GetSumCRC(crcData);
                                buff.Add(Convert.ToByte(stream.Length & 0xFF));
                                buff.Add(Convert.ToByte((stream.Length >> 8) & 0xFF));
                                buff.Add(Convert.ToByte((stream.Length >> 16) & 0xFF));
                                buff.Add(Convert.ToByte((stream.Length >> 24) & 0xFF));
                                buff.Add(Convert.ToByte(crc & 0xFF));
                                buff.Add(Convert.ToByte((crc >> 8) & 0xFF));
                                buff.Add(Convert.ToByte((crc >> 16) & 0xFF));
                                buff.Add(Convert.ToByte((crc >> 24) & 0xFF));
                            }
                            buff.AddRange(data);
                            Send(buff.ToArray());
                            if (seek % Param.PartitionIndex == 0) ThreadSleep(Param.PacketIntervalTimeByPartitionIndex);
                            else Thread.Sleep(FirmwarePackageIndex == 0 ? Param.FirstPacketIntervalTime : Param.PacketIntervalTime);
                            buff.Clear();
                            Progress((int)Math.Floor((FirmwarePackageIndex + 1) * Param.PacketSize * 100 / (1.0 * stream.Length)));
                            FirmwarePackageIndex++;
                        }
                    }
                }
                else
                {
                    using (FileStream stream = new FileStream(DownloadFilePath, FileMode.Open))
                    {
                        int seek = 0;
                        int readLength = 0;
                        List<byte> buff = new List<byte>();
                        byte[] packageHead = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xE0 };
                        byte[] data = new byte[stream.Length];
                        stream.Read(data, 0, data.Length);
                        uint crc = Crc32SUM.GetSumCRC(data);
                        data = new byte[Param.PacketSize];
                        readLength = stream.Read(data, 0, Param.PacketSize);
                        buff.AddRange(packageHead);
                        buff.Add(Convert.ToByte(readLength & 0xFF));
                        buff.Add(Convert.ToByte((readLength >> 8) & 0xFF));
                        buff.Add(Convert.ToByte(seek & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                        buff.Add(Convert.ToByte(stream.Length & 0xFF));
                        buff.Add(Convert.ToByte((stream.Length >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((stream.Length >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((stream.Length >> 24) & 0xFF));
                        buff.Add(Convert.ToByte(crc & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 8) & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 16) & 0xFF));
                        buff.Add(Convert.ToByte((crc >> 24) & 0xFF));
                        buff.AddRange(data);
                        Send(buff.ToArray());
                        if (seek % Param.PartitionIndex == 0) ThreadSleep(Param.PacketIntervalTimeByPartitionIndex);
                        else Thread.Sleep(Param.FirstPacketIntervalTime);
                        buff.Clear();
                        Progress((int)Math.Floor(Param.PacketSize * 100 / (1.0 * stream.Length)));
                        for (FirmwarePackageIndex = 1; FirmwarePackageIndex < FirmwarePackageCount; FirmwarePackageIndex++)
                        {
                            data = new byte[Param.PacketSize];
                            readLength = stream.Read(data, 0, Param.PacketSize);
                            seek = FirmwarePackageIndex * Param.PacketSize;
                            buff.AddRange(packageHead);
                            buff.Add(Convert.ToByte(readLength & 0xFF));
                            buff.Add(Convert.ToByte((readLength >> 8) & 0xFF));
                            buff.Add(Convert.ToByte(seek & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                            buff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                            buff.AddRange(data);
                            Send(buff.ToArray());
                            if (seek % Param.PartitionIndex == 0) ThreadSleep(Param.PacketIntervalTimeByPartitionIndex);
                            else Thread.Sleep(Param.PacketIntervalTime);
                            buff.Clear();
                            Progress((int)Math.Floor((FirmwarePackageIndex + 1) * Param.PacketSize * 100 / (1.0 * stream.Length)));
                        }
                        byte[] packageEnd = new byte[] { 0xAA, 0xBB, 0x00, 0x00, 0xFF };
                        Send(packageEnd);
                        ThreadSleep(Param.FPGAUpdateCompletedIntervalTime);
                        Progress(100);
                        FirmwarePackageIndex = 0;
                        FirmwarePackageCount = 0;
                        TaskCompleted(null, "下载字库成功");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("下载字库失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
                TaskError("下载字库失败");
            }
        }
        protected void DownloadFontLibraryAnswerManage(List<byte> data)
        {
            try
            {
                string recStr = Encoding.Default.GetString(data.ToArray());
                if (FirmwarePackageIndex == FirmwarePackageCount + 1)
                {
                    ThreadSleep(Param.FPGAUpdateCompletedIntervalTime);
                    Progress(100);
                    FirmwarePackageIndex = 0;
                    FirmwarePackageCount = 0;
                    TaskCompleted(null, "下载字库成功");
                }
                else
                {
                    DownloadFontLibraryTask();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("下载字库失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
                TaskError("下载字库失败");
            }
        }
    }

}
