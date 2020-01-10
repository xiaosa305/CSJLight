using CCWin.Win32.Const;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LightController.PeripheralDevice
{
    public class NetworkConnect : BaseCommunication
    {
        private const int TCPPORT = 7060;
        private const int RECEIVEBUFFSIZE = 2048;
        private string DeviceName { get; set; }//设备名称
        private string DeviceIp { get; set; }//设备IP地址
        private int DevicePort { get; set; }//设备端口
        private Socket Socket { get; set; }//网络连接套接字
        private byte[] ReceiveBuff { get; set; }//接收缓存区
        private int BuffCount { get; set; }

        public NetworkConnect(NetworkDeviceInfo deviceInfo)
        {
            this.Init();
            this.ReceiveBuff = new byte[RECEIVEBUFFSIZE];
            this.BuffCount = 0;
            this.Connect(deviceInfo);
        }
        public bool IsConnected()
        {
            return this.Socket.Connected;
        }
        /// <summary>
        /// 连接目标设备
        /// </summary>
        /// <param name="deviceInfo"></param>
        private void Connect(NetworkDeviceInfo deviceInfo)
        {
            this.DeviceName = deviceInfo.DeviceName;
            this.DeviceIp = deviceInfo.DeviceIp;
            this.DevicePort = TCPPORT;
            this.DeviceAddr = deviceInfo.DeviceAddr;
            this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Socket.Connect(new IPEndPoint(IPAddress.Parse(this.DeviceIp), this.DevicePort));
            Thread.Sleep(200);
            this.Socket.BeginReceive(ReceiveBuff, this.BuffCount, this.BuffRemain(), SocketFlags.None, this.NetworkReceive, this);

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
        /// 网络发送数据
        /// </summary>
        /// <param name="data"></param>
        protected override void Send(byte[] data)
        {
            this.Socket.BeginSend(data, 0, data.Length, SocketFlags.None, this.SendCallBack, this);
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
        /// 网络回复数据接受
        /// </summary>
        /// <param name="asyncResult"></param>
        private void NetworkReceive(IAsyncResult asyncResult)
        {
            NetworkConnect connect = asyncResult.AsyncState as NetworkConnect;
            int aa = connect.Socket.ReceiveBufferSize;
            int count = connect.Socket.EndReceive(asyncResult);
            Console.WriteLine("网络接收数据大小："+ count);
            if (count <= 0)
            {
                Console.WriteLine("设备断开连接");
                return;
            }
            else
            {
                byte[] buff = new byte[count];
                Array.Copy(connect.ReceiveBuff, buff, count);
                ReadBuff.AddRange(buff);
                this.Receive();
                connect.Socket.BeginReceive(connect.ReceiveBuff, connect.BuffCount, connect.BuffRemain(), SocketFlags.None, NetworkReceive, connect);
            }
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        public override void DisConnect()
        {
            this.Socket.Close();
        }
    }
}
