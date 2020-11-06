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


        private const int RECEIVEBUFFSIZE = 1024 * 2;
        private byte[] ReceiveBuff { get; set; }//接收缓存区
        private int BuffCount { get; set; }
        private Socket Client;


        public OnlineConnect()
        {
            this.Init();
            this.ReceiveBuff = new byte[RECEIVEBUFFSIZE];
            this.BuffCount = 0;
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
            if (this.Client != null)
            {
                return this.Client.Connected;
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
                                break;
                            case 0x01:
                                break;
                            case 0x02:
                                break;
                            case 0x03:
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

        public void UnBindDevice()
        {

        }

        public void BindDevice(String deviceId)
        {

        }

        public void ChangeBindDevice(String deviceId)
        {

        }

        public void GetOnlineDevices()
        {

        }

        private void UnBindDeviceReceiveManager(byte[] data)
        {

        }

        private void BindDeviceReceiveManager(byte[] data)
        {

        }

        private void ChangeBindDeviceReceiveManager(byte[] data)
        {

        }

        public void GetOnlineDevice(byte[] data)
        {

        }
    }
}
