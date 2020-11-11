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

        public delegate void CommandSuccessed();
        public delegate void CommandFailed();

        private CommandSuccessed CommandSuccessed_Event { get; set; }
        private CommandFailed CommandFailed_Event { get; set; }

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

        /// <summary>
        /// 网络发送完成回调方法
        /// </summary>
        /// <param name="async"></param>
        private void SendCallBack(IAsyncResult async)
        {
            this.SendDataCompleted();
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
                        switch (buff[2])
                        {
                            case 0x00:
                                this.UnBindDeviceReceiveManager(buff);
                                break;
                            case 0x01:
                                this.BindDeviceReceiveManager(buff);
                                break;
                            case 0x02:
                                this.ChangeBindDeviceReceiveManager(buff);
                                break;
                            case 0x03:
                                this.GetOnlineDeviceReceiveManager(buff);
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
                        this.Client.BeginReceive(connect.ReceiveBuff, connect.BuffCount, connect.BuffRemain(), SocketFlags.None, NetworkReceive, connect);
                    }
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "网络设备已断开或网络接收模块发生异常", ex);
            }
        }

        public void UnBindDevice(CommandSuccessed successed,CommandFailed failed)
        {
            this.CommandSuccessed_Event = successed;
            this.CommandFailed_Event = failed;
            byte[] data = new byte[] { 0xBB, 0xAA, 0x00, 0x00 };
            this.Client.BeginSend(data, 0, data.Length, SocketFlags.None, this.SendCallBack, this);
            //this.Send(data);
        }

        public void BindDevice(String deviceId,CommandSuccessed successed,CommandFailed failed)
        {
            this.CommandSuccessed_Event = successed;
            this.CommandFailed_Event = failed;
            List<byte> data = new List<byte>();
            data.Add(0xBB);
            data.Add(0xAA);
            data.Add(0x01);
            data.Add(0x00);
            data.AddRange(Encoding.Default.GetBytes(deviceId));
            this.Client.BeginSend(data.ToArray(), 0, data.Count, SocketFlags.None, this.SendCallBack, this);
            //this.Send(data.ToArray());
        }

        public void ChangeBindDevice(String deviceId,CommandSuccessed successed,CommandFailed failed)
        {
            this.CommandSuccessed_Event = successed;
            this.CommandFailed_Event = failed;
            List<byte> data = new List<byte>();
            data.Add(0xBB);
            data.Add(0xAA);
            data.Add(0x02);
            data.Add(0x00);
            data.AddRange(Encoding.Default.GetBytes(deviceId));
            this.Client.BeginSend(data.ToArray(), 0, data.Count, SocketFlags.None, this.SendCallBack, this);
            //this.Send(data.ToArray());
        }

        public void GetOnlineDevices(CommandSuccessed successed,CommandFailed failed)
        {
            this.CommandSuccessed_Event = successed;
            this.CommandFailed_Event = failed;
            byte[] data = new byte[] { 0xBB, 0xAA, 0x03,0x00,0x00 };
            this.Client.BeginSend(data, 0, data.Length, SocketFlags.None, this.SendCallBack, this);
            //this.Send(data);
        }

        public void SetSessionId()
        {
            List<byte> data = new List<byte>();
            data.Add(0xBB);
            data.Add(0xAA);
            data.Add(0x04);
            data.Add(0x00);
            data.AddRange(Encoding.Default.GetBytes(this.SessionId));
            this.Send(data.ToArray());
        }

        private void UnBindDeviceReceiveManager(byte[] data)
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

        private void UnBindDeviceFailed()
        {
            this.IsBind = false;
            this.CommandFailed_Event();
        }

        private void UnBindDeviceSuccessed()
        {
            this.IsBind = false;
            this.CommandSuccessed_Event();
        }

        private void BindDeviceReceiveManager(byte[] data)
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

        private void BindDeviceFailed()
        {
            this.IsBind = false;
            this.CommandFailed_Event();
        }

        private void BindDeviceSuccessed()
        {
            this.IsBind = true;
            this.CommandSuccessed_Event();
        }

        private void ChangeBindDeviceReceiveManager(byte[] data)
        {
            switch (data[3])
            {
                case 0x00:
                    this.ChgangeBindDeviceFailed();
                    break;
                case 0x01:
                    this.ChangeBindDeviceSuccessed();
                    break;
            }
        }

        private void ChgangeBindDeviceFailed()
        {
            this.CommandFailed_Event();
        }

        private void ChangeBindDeviceSuccessed()
        {
            this.IsBind = true;
            this.CommandSuccessed_Event();
        }

        public void GetOnlineDeviceReceiveManager(byte[] data)
        {
            switch (data[3])
            {
                case 0x00:
                    this.GetOnlineDeviceFailed();
                    break;
                case 0x01:
                    this.GetOnlineDeviceSuccessed(data);
                    break;
            }
        }
        private void GetOnlineDeviceFailed()
        {
            this.DeviceInfos = new List<OnlineDeviceInfo>();
            //this.CommandFailed_Event();
        }

        private void GetOnlineDeviceSuccessed(byte[] data)
        {
            byte[] jsonBuff = new byte[data.Length - 4];
            Array.Copy(data, 4, jsonBuff, 0, data.Length - 4);
            string json = Encoding.UTF8.GetString(jsonBuff);
            this.DeviceInfos = JSON.ToObject<List<OnlineDeviceInfo>>(json);
            //this.CommandSuccessed_Event();
        }
    }
}
