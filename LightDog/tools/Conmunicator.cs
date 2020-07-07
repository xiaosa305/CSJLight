using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace LightDog.tools
{
    public abstract class Conmunicator
    {
        private string OldPassword { get; set; }
        private string NewPassword { get; set; }
        protected List<byte> RxBuff { get; set; }
        private Thread ReceiveThread { get; set; }
        protected SerialPortReceiveQueue Queue { get; set; }
        protected bool ReceiveThreadStatus { get; set; }
        protected Order CurrentOrder { get; set; }
        public delegate void Completed(Object obj, string message);
        public delegate void Error(Object obj, string message);
        protected Completed Completed_Event { get; set; }
        protected Error Error_Event { get; set; }
        protected bool Flag { get; set; }

        private System.Timers.Timer TimeOutTimer { get; set; }
        private System.Timers.Timer TranstionTimer { get; set; }


        public abstract void Send(byte[] data, int offset, int count);

        protected void InitParam()
        {
            this.Flag = false;
            this.RxBuff = new List<byte>();
            this.Queue = new SerialPortReceiveQueue();
            this.ReceiveThreadStatus = true;
            this.ReceiveThread = new Thread(this.ReceiveThreadStart) { IsBackground = true };
            this.ReceiveThread.Start();
            this.TimeOutTimer = new System.Timers.Timer(Constant.ACTION_TIMEOUT) { AutoReset = false };
            this.TimeOutTimer.Elapsed += new ElapsedEventHandler((s, e) => TimeOutAction(s, e));
        }

        private void TimeOutAction(Object obj, ElapsedEventArgs e)
        {
            Console.WriteLine("超时：");
            switch (CurrentOrder)
            {
                case Order.SetPassword:
                    if (this.TranstionTimer != null)
                    {
                        this.TranstionTimer.Stop();
                    }
                    this.Error_Event(null,"设置新密码失败，通信超时");
                    break;
                case Order.SetTime:
                    if (this.TranstionTimer != null)
                    {
                        this.TranstionTimer.Stop();
                    }
                    this.Error_Event(null, "设置时间失败，通信超时");
                    break;
                case Order.Check:
                    if (this.TranstionTimer != null)
                    {
                        this.TranstionTimer.Stop();
                    }
                    break;
                case Order.Login:
                    if (this.TranstionTimer != null)
                    {
                        this.TranstionTimer.Stop();
                    }
                    this.Error_Event(null, "登录失败，密码错误");
                    break;
                case Order.Null:
                default:
                    break;
            }
            this.RxBuff.Clear();
            this.CurrentOrder = Order.Null;
        }

        private void StartTimeOut()
        {
            this.TimeOutTimer.Enabled = false;
            this.TimeOutTimer.Start();
        }

        private void StopTimeOut()
        {
            this.TimeOutTimer.Stop();
        }

        protected List<byte> AddPackageHead(List<byte> sourceBuff)
        {
            List<byte> resultBuff = new List<byte>();
            resultBuff.Add(0xAA);
            resultBuff.Add(0xBB);
            resultBuff.Add(0xFF);
            resultBuff.Add(Convert.ToByte(sourceBuff.Count & 0xFF));
            resultBuff.Add(Convert.ToByte((sourceBuff.Count >> 8) & 0xFF));
            resultBuff.Add(0x01);
            resultBuff.Add(0x00);
            resultBuff.Add(0x00);
            for (int index = 0; index < sourceBuff.Count; index++)
            {
                resultBuff.Add(sourceBuff[index]);
            }
            byte[] crc = CRCTools.GetInstance().GetCRC(resultBuff.ToArray());
            resultBuff[6] = crc[0];
            resultBuff[7] = crc[1];
            return resultBuff;
        }

        protected byte[] RemovePackageHead()
        {
            List<byte> resultBuff = new List<byte>();
            for (int index = 8; index < this.RxBuff.Count; index++)
            {
                resultBuff.Add(this.RxBuff[index]);
            }
            return resultBuff.ToArray();
        }

        private bool CheckPackageHead(List<byte> sourceBuff)
        {
            bool result = false;
            byte[] sourceCrc = new byte[2];
            if (sourceBuff[0] == 0xAA && sourceBuff[1] == 0xBB && sourceBuff[2] == 0x00 && sourceBuff[5] == 0x02)
            {
                sourceCrc[0] = sourceBuff[6];
                sourceCrc[1] = sourceBuff[7];
                sourceBuff[6] = 0;
                sourceBuff[7] = 0;
                byte[] crc = CRCTools.GetInstance().GetCRC(sourceBuff.ToArray());
                if (crc[0] == sourceCrc[0] && crc[1] == sourceCrc[1])
                {
                    result = true;
                }
            }
            return result;
        }

        private bool CheckPackageLength(List<byte> sourceBuff)
        {
            bool result = false;
            int packageLength = (sourceBuff[3] & 0xFF) | ((sourceBuff[4] << 8) & 0xFF);
            if (packageLength == (sourceBuff.Count - 8))
            {
                result = true;
            }
            return result;
        }

        protected bool CheckData()
        {
            bool result = false;
            List<byte> sourceBuff = new List<byte>(this.RxBuff);
            if (this.CheckPackageLength(sourceBuff))
            {
                result = CheckPackageHead(sourceBuff);
            }
            return result;
        }

        protected void SendCompleted(Object obj)
        {
            switch (CurrentOrder)
            {
                case Order.SetPassword:
                    this.TimeOutTimer.Interval = Constant.ACTION_TIMEOUT;
                    this.StartTimeOut();
                    break;
                case Order.SetTime:
                    this.TimeOutTimer.Interval = Constant.ACTION_TIMEOUT;
                    this.StartTimeOut();
                    break;
                case Order.Login:
                    this.TimeOutTimer.Interval = Constant.ACTION_TIMEOUT;
                    this.StartTimeOut();
                    break;
                case Order.Check:
                case Order.Null:
                default:
                    break;
            }
        }

        protected void ReceiveThreadStart()
        {
            while (this.ReceiveThreadStatus)
            {
                try
                {
                    byte[] data;
                    lock (this.Queue)
                    {
                       data = Queue.Dequeue();
                    }
                    if (data != null)
                    {
                        this.ReceiveManager(data);
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
                catch (Exception)
                {
                    this.ReceiveThreadStatus = false;
                    break;
                }
            }
        }

        private void ReceiveManager(byte[] data)
        {
            switch (this.CurrentOrder)
            {
                case Order.SetPassword:
                    this.SetPasswordReceiveManager(data);
                    break;
                case Order.SetTime:
                    this.SetTimeReceiveManager(data);
                    break;
                case Order.Check:
                    this.SerialPortDeviceCheckReceiveManager(data);
                    break;
                case Order.Login:
                    this.LoginReceiveManager(data);
                    break;
                case Order.Null:
                default:
                    break;
            }
        }

        private void LoginReceiveManager(byte[] data)
        {
            if (Constant.RECEIVE_SET_PASSWORD.Equals(Encoding.Default.GetString(data)))
            {
                this.StopTimeOut();
                this.Completed_Event(null, "登录成功");
                this.CurrentOrder = Order.Null;
            }
        }

        private void SetPasswordReceiveManager(byte[] data)
        {
            if (Constant.RECEIVE_LOGIN.Equals(Encoding.Default.GetString(data)))
            {
                this.StopTimeOut();
                this.Completed_Event(null, "设置新密码成功");
                this.CurrentOrder = Order.Null;
            }
        }

        private void SetTimeReceiveManager(byte[] data)
        {
            if (Constant.RECEIVE_SET_TIME.Equals(Encoding.Default.GetString(data)))
            {
                this.StopTimeOut();
                this.Completed_Event(null, "设置时间成功");
                this.CurrentOrder = Order.Null;
            }
        }

        protected void SerialPortDeviceCheckReceiveManager(byte[] data)
        {
            if (Constant.RECEIVE_CHECK.Equals(Encoding.Default.GetString(data)))
            {
                this.Flag = true;
                this.CurrentOrder = Order.Null;
            }
        }

        public void SetLightControlDevicePassword(string newPassword,Completed completed,Error error)
        {
            if (this.TranstionTimer == null || !this.TranstionTimer.Enabled)
            {
                if (newPassword.Length == 0)
                {
                    this.Error_Event(null, "设置新密码失败，新密码不能为空");
                    return;
                }
                this.CurrentOrder = Order.SetPassword;
                this.Completed_Event = completed;
                this.Error_Event = error;
                this.NewPassword = newPassword;
                this.TranstionTimer = new System.Timers.Timer() { AutoReset = false };
                this.TranstionTimer.Elapsed += new ElapsedEventHandler((s, e) => SetLightControlDevicePasswordTask(s, e));
                this.TranstionTimer.Start();
            }
        }

        private void SetLightControlDevicePasswordTask(Object obj, ElapsedEventArgs e)
        {
            string dataStr = Constant.ORDER_SET_PASSWORD + " " + this.NewPassword + " " + this.OldPassword;
            List<byte> data = new List<byte>(Encoding.Default.GetBytes(dataStr));
            Console.WriteLine(Encoding.Default.GetString(data.ToArray()));
            data.Add(0x00);
            data = this.AddPackageHead(data);
            this.Send(data.ToArray(), 0, data.Count);
        }

        public void SetLightControlDeviceTime(uint time,Completed completed,Error error)
        {
            if (this.TranstionTimer == null || !this.TranstionTimer.Enabled)
            {
                this.Completed_Event = completed;
                this.Error_Event = error;
                if (this.OldPassword.Length == 0 || time < 0)
                {
                    this.Error_Event(null, "设置时间失败，时间不能为空且不能为负数");
                    return;
                }
                this.CurrentOrder = Order.SetTime;
                this.TranstionTimer = new System.Timers.Timer() { AutoReset = false };
                this.TranstionTimer.Elapsed += new ElapsedEventHandler((s, e) => SetLightControlDeviceTimeTask(s, e, time));
                this.TranstionTimer.Start();
            }
        }

        private void SetLightControlDeviceTimeTask(Object obj, ElapsedEventArgs e,uint time)
        {
            string dataStr = Constant.ORDER_SET_TIME + " " + time + " " + this.OldPassword;
            List<byte> data = new List<byte>(Encoding.Default.GetBytes(dataStr));
            data.Add(0x00);
            data = this.AddPackageHead(data);
            this.Send(data.ToArray(), 0, data.Count);
        }

        public void Login(string password, Completed completed, Error error)
        {
            if (this.TranstionTimer == null || !this.TranstionTimer.Enabled)
            {
                if (password.Length == 0)
                {
                    this.Error_Event(null, "密码不能为空");
                    return;
                }
                this.Completed_Event = completed;
                this.Error_Event = error;
                this.SetOldPassword(password);
                this.CurrentOrder = Order.Login;
                this.TranstionTimer = new System.Timers.Timer() { AutoReset = false };
                this.TranstionTimer.Elapsed += new ElapsedEventHandler((s, e) => LoginTask(s, e, password));
                this.TranstionTimer.Start();
            }
        }

        private void LoginTask(Object obj, ElapsedEventArgs e, string password)
        {
            string dataStr = Constant.ORDER_LOGIN + " " + password;
            List<byte> data = new List<byte>(Encoding.Default.GetBytes(dataStr));
            data.Add(0x00);
            data = this.AddPackageHead(data);
            this.Send(data.ToArray(), 0, data.Count);
        }

        protected void CheckDevice()
        {
            if (this.TranstionTimer == null || !this.TranstionTimer.Enabled)
            {
                this.CurrentOrder = Order.Check;
                this.TranstionTimer = new System.Timers.Timer() { AutoReset = false };
                this.TranstionTimer.Elapsed += new ElapsedEventHandler((s, e) => CheckDeviceTask(s, e));
                this.TranstionTimer.Start();
            }
        }

        private void CheckDeviceTask(Object obj, ElapsedEventArgs e)
        {

            string dataStr = Constant.ORDER_CHECK_DEVICE;
            List<byte> data = new List<byte>(Encoding.Default.GetBytes(dataStr));
            data = this.AddPackageHead(data);
            this.Send(data.ToArray(), 0, data.Count);
        }

        public void SetOldPassword(string password)
        {
            this.OldPassword = password;
        }

        public string GetNewPassword()
        {
            return this.OldPassword;
        }
    }

    public enum Order
    {
        Null,SetPassword,SetTime,Check,Login
    }
}
