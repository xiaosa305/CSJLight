using fastJSON;
using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace LightController.PeripheralDevice
{
    public class OnlineConnect : BaseCommunication
    {
        private static readonly String ONLINE_SERVER_IP = "192.168.31.235";
        private static readonly int ONLINE_SERVER_PORT = 7060;
        public static readonly byte COMMAND_SUCCESS = 0x01;
        public static readonly byte COMMAND_FAILED = 0x00;
        public static readonly int UNBIND_DEVICE = 0;
        public static readonly int BIND_DEVICE = 1;
        public static readonly int CHANGE_BIND_DEVICE = 2;
        public static readonly int GET_ONLINE_DEVICES = 3;
        public static readonly int LOGIN_SERVER = 4;
        private const int RECEIVEBUFFSIZE = 1024 * 2;
        private byte[] ReceiveBuff { get; set; }//接收缓存区
        private int BuffCount { get; set; }
        private Socket Client;
        public List<OnlineDeviceInfo> DeviceInfos { get; set; }
        private string SessionId { get; set; }
        public bool IsBind { get; set; }
        public bool IsSendToServer { get; set; }

        //public delegate void CommandSuccessed();
        //public delegate void CommandFailed();

        //private CommandSuccessed CommandSuccessed_Event { get; set; }
        //private CommandFailed CommandFailed_Event { get; set; }

        public OnlineConnect(String sessionId)
        {
            this.Init();
            this.ReceiveBuff = new byte[RECEIVEBUFFSIZE];
            this.BuffCount = 0;
            this.IsBind = false;
            this.SessionId = sessionId;
        }

        public override bool Connect(NetworkDeviceInfo deviceInfo)
        {
            bool result = false;
            try
            {
                if (this.IsConnected())
                {
                    return true;
                }
                this.Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.Client.ReceiveBufferSize = RECEIVEBUFFSIZE;
                this.Client.Connect(new IPEndPoint(IPAddress.Parse(ONLINE_SERVER_IP), ONLINE_SERVER_PORT));
                this.Client.BeginReceive(ReceiveBuff, this.BuffCount, this.BuffRemain(), SocketFlags.None, this.NetworkReceive, this);
                LogTools.Debug(Constant.TAG_XIAOSA, "连接设备成功!");
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return result;
        }

        public override void DisConnect()
        {
            try
            {
                if (this.Client != null)
                {
                    if (this.Client.Connected)
                    {
                        this.Client.Close();
                    }
                    this.Client = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public override bool IsConnected()
        {
            if (this.Client != null && this.IsBind)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool OpenSerialPort(string portName)
        {
            throw new NotImplementedException();
        }

        protected override void Send(byte[] data)
        {
            this.Client.BeginSend(data, 0, data.Length, SocketFlags.None, this.SendCallBack, this);
        }

        private void SendToServer(byte[] data)
        {
            this.Client.BeginSend(data, 0, data.Length, SocketFlags.None, this.SendCallBack, this);
            this.IsSendToServer = true;
        }

        /// <summary>
        /// 网络发送完成回调方法
        /// </summary>
        /// <param name="async"></param>
        private void SendCallBack(IAsyncResult async)
        {
            if (this.IsSendToServer)
            {
                this.IsSendToServer = false;
            }
            else
            {
                this.SendDataCompleted();
            }
        }

        /// <summary>
        /// 获取缓存区大小
        /// </summary>
        /// <returns></returns>
        private int BuffRemain()
        {
            return RECEIVEBUFFSIZE - this.BuffCount;
        }

        /// <summary>
        /// 网络回复数据接受
        /// </summary>
        /// <param name="asyncResult"></param>
        private void NetworkReceive(IAsyncResult asyncResult)
        {
            try
            {
                Console.WriteLine("收到数据包");
                OnlineConnect connect = asyncResult.AsyncState as OnlineConnect;
                int count = connect.Client.EndReceive(asyncResult);
                if (count <= 0)
                {
                    //LogTools.Debug(Constant.TAG_XIAOSA, "设备断开连接");
                    return;
                }
                else
                {
                    byte[] buff = new byte[count];
                    Array.Copy(connect.ReceiveBuff, buff, count);
                    if (buff[0] == 0xBB && buff[1] == 0xAA)
                    {
                        this.StopTimeOut();
                        switch (buff[2])
                        {
                            case 0x00:
                                this.UnBindDeviceReceinvManager(buff);
                                break;
                            case 0x01:
                                this.BindDeviceReceinvManager(buff);
                                break;
                            case 0x02:
                                this.ChangeBindDeviceReceinvManager(buff);
                                break;
                            case 0x03:
                                this.GetOnlineDevicesReceiveManager(buff);
                                break;
                            case 0x04:
                                this.SetSessionIdReceiveManager(buff);
                                break;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < count; i++)
                        {
                            ReadBuff.Add(buff[i]);
                            this.Receive();
                        }
                        connect.ReceiveBuff = new byte[RECEIVEBUFFSIZE];
                    }
                    this.Client.BeginReceive(connect.ReceiveBuff, connect.BuffCount, connect.BuffRemain(), SocketFlags.None, NetworkReceive, connect);
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "网络设备已断开或网络接收模块发生异常", ex);
            }
        }

        public void SetSessionId(Completed completed,Error error)
        {
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.SetCompletedEvent(completed);
                    this.SetErrordEvent(error);
                    this.CloseTransactionTimer();
                    this.TransactionTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TransactionTimer.Elapsed += new ElapsedEventHandler((s, e) => this.SetSessionIdTask(s, e));
                    this.TransactionTimer.Start();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "设置客户端账号失败", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.CommandFailed("设置客户端账号失败");
                this.CloseTransactionTimer();
            }
        }
        private void SetSessionIdTask(Object obj, ElapsedEventArgs e)
        {
            try
            {
                this.SecondOrder = Order.SERVER_SET_SESSION_ID;
                List<byte> data = new List<byte>();
                data.Add(0xBB);
                data.Add(0xAA);
                data.Add(0x04);
                data.Add(0x00);
                data.AddRange(Encoding.Default.GetBytes(this.SessionId));
                this.Send(data.ToArray());
            }
            catch (Exception ex)
            {
                this.CommandFailed("连接已断开");
            }
        }
        private void SetSessionIdReceiveManager(byte[] data)
        {
            switch (data[3])
            {
                case 0x00:
                    this.SetSessionIdFailed();
                    break;
                case 0x01:
                    this.SetSessionIdSuccessed();
                    break;
            }
        }
        private void SetSessionIdSuccessed()
        {
            this.CommandSuccessed(null, "登录成功");
        }
        private void SetSessionIdFailed()
        {
            this.CommandFailed("登录失败");
        }

        public void GetOnlineDevices(Completed completed, Error error)
        {
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.SetCompletedEvent(completed);
                    this.SetErrordEvent(error);
                    this.CloseTransactionTimer();
                    this.TransactionTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TransactionTimer.Elapsed += new ElapsedEventHandler((s, e) => this.GetOnlineDevicesTask(s, e));
                    this.TransactionTimer.Start();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "获取设备信息失败", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.CommandFailed("获取设备信息失败");
                this.CloseTransactionTimer();
            }
        }
        private void GetOnlineDevicesTask(Object obj, ElapsedEventArgs e)
        {
            try
            {
                this.SecondOrder = Order.SERVER_GET_DEVICES;
                byte[] data = new byte[] { 0xBB, 0xAA, 0x03, 0x00, 0x00 };
                this.Send(data.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.CommandFailed("连接已断开");
            }
        }
        private void GetOnlineDevicesReceiveManager(byte[] data)
        {
            switch (data[3])
            {
                case 0x00:
                    this.GetOnlineDevicesFailed();
                    break;
                case 0x01:
                    this.GetOnlineDevicesSuccessed(data);
                    break;
            }
        }
        private void GetOnlineDevicesSuccessed(byte[] data)
        {
            byte[] buff = new byte[data.Length - 4];
            Array.Copy(data, 4, buff, 0, data.Length - 4);
            string json = Encoding.UTF8.GetString(buff);
            this.DeviceInfos = JSON.ToObject<List<OnlineDeviceInfo>>(json);
            this.CommandSuccessed(null, "获取设备信息成功");
        }
        private void GetOnlineDevicesFailed()
        {
            this.CommandFailed("获取设备信息失败");
        }

        public void UnBindDevice(Completed completed ,Error error)
        {
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.SetCompletedEvent(completed);
                    this.SetErrordEvent(error);
                    this.CloseTransactionTimer();
                    this.TransactionTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TransactionTimer.Elapsed += new ElapsedEventHandler((s, e) => this.UnBindDeviceTask(s, e));
                    this.TransactionTimer.Start();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "取消绑定失败", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.CommandFailed("取消绑定失败");
                this.CloseTransactionTimer();
            }
        }
        private void UnBindDeviceTask(Object obj, ElapsedEventArgs e)
        {
            try
            {
                this.SecondOrder = Order.SERVER_UNBIND_DEVICE;
                byte[] data = new byte[] { 0xBB, 0xAA, 0x00, 0x00 };
                this.Send(data.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.CommandFailed("连接已断开");
            }
        }
        private void UnBindDeviceReceinvManager(byte[] data)
        {
            switch (data[3])
            {
                case 0x00:
                    this.UnBindDeviceFailed();
                    break;
                case 0x01:
                    this.UnBindDeviceSuccessed();
                    break;
            }
        }
        private void UnBindDeviceSuccessed()
        {
            this.CommandSuccessed(null,"取消绑定成功");
        }
        private void UnBindDeviceFailed()
        {
            this.CommandFailed("取消绑定失败");
        }

        public void BindDevice(OnlineDeviceInfo deviceInfo,Completed completed,Error error)
        {
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.SetCompletedEvent(completed);
                    this.SetErrordEvent(error);
                    this.CloseTransactionTimer();
                    this.TransactionTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TransactionTimer.Elapsed += new ElapsedEventHandler((s, e) => this.BindDeviceTask(s, e,deviceInfo));
                    this.TransactionTimer.Start();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "绑定失败", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.CommandFailed("绑定失败");
                this.CloseTransactionTimer();
            }
        }
        private void BindDeviceTask(Object obj, ElapsedEventArgs e, OnlineDeviceInfo deviceInfo)
        {
            try
            {
                this.SecondOrder = Order.SERVER_BIND_DEVICE;
                List<byte> data = new List<byte>();
                data.Add(0xBB);
                data.Add(0xAA);
                data.Add(0x01);
                data.Add(0x00);
                data.AddRange(Encoding.Default.GetBytes(deviceInfo.Device_id));
                this.Send(data.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.CommandFailed("连接已断开");
            }
        }
        private void BindDeviceReceinvManager(byte[] data)
        {
            switch (data[3])
            {
                case 0x00:
                    this.BindDeviceFailed();
                    break;
                case 0x01:
                    this.BindDeviceSuccessed();
                    break;
            }
        }
        private void BindDeviceSuccessed()
        {
            this.CommandSuccessed(null, "绑定成功");
        }
        private void BindDeviceFailed()
        {
            this.CommandFailed("绑定失败");
        }


        public void ChangeBindDevice(OnlineDeviceInfo deviceInfo, Completed completed, Error error)
        {
            try
            {
                if (!this.IsSending)
                {
                    this.IsSending = true;
                    this.SetCompletedEvent(completed);
                    this.SetErrordEvent(error);
                    this.CloseTransactionTimer();
                    this.TransactionTimer = new System.Timers.Timer
                    {
                        AutoReset = false
                    };
                    this.TransactionTimer.Elapsed += new ElapsedEventHandler((s, e) => this.ChangeBindDeviceTask(s, e, deviceInfo));
                    this.TransactionTimer.Start();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "切换绑定失败", ex);
                this.StopTimeOut();
                this.IsSending = false;
                this.CommandFailed("切换绑定失败");
                this.CloseTransactionTimer();
            }
        }
        private void ChangeBindDeviceTask(Object obj, ElapsedEventArgs e, OnlineDeviceInfo deviceInfo)
        {
            try
            {
                this.SecondOrder = Order.SERVER_CHANGE_BIND_DEVICE;
                List<byte> data = new List<byte>();
                data.Add(0xBB);
                data.Add(0xAA);
                data.Add(0x02);
                data.Add(0x00);
                data.AddRange(Encoding.Default.GetBytes(deviceInfo.Device_id));
                this.Send(data.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                this.CommandFailed("连接已断开");
            }
        }
        private void ChangeBindDeviceReceinvManager(byte[] data)
        {
            switch (data[3])
            {
                case 0x00:
                    this.ChangeBindDeviceFailed();
                    break;
                case 0x01:
                    this.ChangeBindDeviceSuccessed();
                    break;
            }
        }
        private void ChangeBindDeviceSuccessed()
        {
            this.CommandSuccessed(null, "切换绑定成功");
        }
        private void ChangeBindDeviceFailed()
        {
            this.CommandFailed("切换绑定失败");
        }
    }
}
