using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace MultiLedController.utils.lbdconfigtor
{
    public abstract class BaseFunctionalModule
    {
        protected const int TIME_OUT_COUNT = 5000;
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
        protected abstract void Send(byte[] data);
        protected void SendCompleted()
        {
            switch (CurrentModule)
            {
                case Module.SearchDevice:
                case Module.ReadDeviceId:
                case Module.WriteEncrypt:
                case Module.UpdateFPGA256:
                case Module.UpdataMCU256:
                case Module.WriteData:
                case Module.WriteParam:
                case Module.IsEncrypt:
                case Module.SetArtNetSpaceNumber:
                    this.StartTimeOutTask();
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
                this.Error_Event(msg);
                this.TaskTimer.Stop();
                this.InitParam();
            }
        }
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
                    this.TaskTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => SearchDeviceTask(s, e));
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
                    this.TaskTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => WriteEncryptTask(s, e));
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
        private void WriteEncryptTask(Object obj, ElapsedEventArgs e)
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
        public void UpdateFPGA256(Completed completed, Error error)
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
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => UpdateFPGA256Task(s, e));
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
        private void UpdateFPGA256Task(Object obj, ElapsedEventArgs e)
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
        public void UpdataMCU256(Completed completed, Error error)
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
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => UpdataMCU256Task(s, e));
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
        private void UpdataMCU256Task(Object obj, ElapsedEventArgs e)
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
        public void WriteData(Completed completed, Error error)
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
        public void WriteParam(Completed completed, Error error)
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
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => WriteParamTask(s, e));
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
        private void WriteParamTask(Object obj, ElapsedEventArgs e)
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
        public void IsEncrypt(Completed completed, Error error)
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
        public void SetArtNetSpaceNumber(Completed completed, Error error)
        {
            this.Completed_Event = completed;
            this.Error_Event = error;
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.CurrentModule = Module.SetArtNetSpaceNumber;
                    this.TaskTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TaskTimer.Elapsed += new ElapsedEventHandler((s, e) => SetArtNetSpaceNumberTask(s, e));
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
        private void SetArtNetSpaceNumberTask(Object obj, ElapsedEventArgs e)
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
                case Module.UpdateFPGA256:
                    this.UpdateFPGA256ReceiveManage(recData);
                    break;
                case Module.UpdataMCU256:
                    this.UpdataMCU256ReceiveManage(recData);
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
                case Module.SetArtNetSpaceNumber:
                    this.SetArtNetSpaceNumberReceiveManage(recData);
                    break;
            }
        }
        private void SearchDeviceReceiveManage(List<byte> recData)
        {
            bool flag = false;
            if (flag)
            {
                this.SearchDeviceCompleted();
            }
            else
            {
                this.SearchDeviceError();
            }
        }
        private void SearchDeviceCompleted()
        {
            this.TaskCompleted();
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
            bool flag = false;
            if (flag)
            {
                this.WriteEncryptCompleted();
            }
            else
            {
                this.WriteEncryptError();
            }
        }
        private void WriteEncryptCompleted()
        {
            this.TaskCompleted();
        }
        private void WriteEncryptError()
        {
            this.TaskError();
        }
        private void UpdateFPGA256ReceiveManage(List<byte> recData)
        {
            bool flag = false;
            if (flag)
            {
                this.UpdateFPGA256Completed();
            }
            else
            {
                this.UpdateFPGA256Error();
            }
        }
        private void UpdateFPGA256Completed()
        {
            this.TaskCompleted();
        }
        private void UpdateFPGA256Error()
        {
            this.TaskError();
        }
        private void UpdataMCU256ReceiveManage(List<byte> recData)
        {
            bool flag = false;
            if (flag)
            {
                this.UpdataMCU256Completed();
            }
            else
            {
                this.UpdataMCU256Error();
            }
        }
        private void UpdataMCU256Completed()
        {
            this.TaskCompleted();
        }
        private void UpdataMCU256Error()
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
        private void SetArtNetSpaceNumberReceiveManage(List<byte> recData)
        {
            bool flag = false;
            if (flag)
            {
                this.SetArtNetSpaceNumberCompleted();
            }
            else
            {
                this.SetArtNetSpaceNumberError();
            }
        }
        private void SetArtNetSpaceNumberCompleted()
        {
            this.TaskCompleted();
        }
        private void SetArtNetSpaceNumberError()
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
            SetArtNetSpaceNumber,
            Null
        }
    }
}
