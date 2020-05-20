using CCWin.Win32.Const;
using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public string DeviceIp { get; set; }//设备IP地址
        private int DevicePort { get; set; }//设备端口
        private Socket Socket { get; set; }//网络连接套接字
        private byte[] ReceiveBuff { get; set; }//接收缓存区
        private int BuffCount { get; set; }
        public NetworkConnect()
        {
            this.Init();
            this.ReceiveBuff = new byte[RECEIVEBUFFSIZE];
            this.BuffCount = 0;
            //this.Connect(deviceInfo);
        }
        public override  bool IsConnected()
        {
            if (this.Socket != null)
            {
                return this.Socket.Connected;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 连接目标设备
        /// </summary>
        /// <param name="deviceInfo"></param>
        public override bool Connect(NetworkDeviceInfo deviceInfo)
        {
            try
            {
                if (this.IsConnected())
                {
                    return true;
                }
                this.DeviceName = deviceInfo.DeviceName;
                this.DeviceIp = deviceInfo.DeviceIp;
                this.DevicePort = TCPPORT;
                this.DeviceAddr = deviceInfo.DeviceAddr;
                this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.Socket.ReceiveBufferSize = RECEIVEBUFFSIZE;

                //TODO XIAOSA Test
                //this.Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 500);

                this.Socket.Connect(new IPEndPoint(IPAddress.Parse(this.DeviceIp), this.DevicePort));
                this.Socket.BeginReceive(ReceiveBuff, this.BuffCount, this.BuffRemain(), SocketFlags.None, this.NetworkReceive, this);
                LogTools.Debug(Constant.TAG_XIAOSA, "连接设备成功!");
                return true;
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "设备链接超时", ex);
                return false;
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
            try
            {
                NetworkConnect connect = asyncResult.AsyncState as NetworkConnect;
                int count = connect.Socket.EndReceive(asyncResult);
                if (count <= 0)
                {
                    //LogTools.Debug(Constant.TAG_XIAOSA, "设备断开连接");
                    return;
                }
                else
                {
                    byte[] buff = new byte[count];
                    Array.Copy(connect.ReceiveBuff, buff, count);
                    for (int i = 0; i < count; i++)
                    {
                        ReadBuff.Add(buff[i]);
                        //this.Receive();
                    }
                    connect.ReceiveBuff = new byte[RECEIVEBUFFSIZE];
                    this.Socket.BeginReceive(connect.ReceiveBuff, connect.BuffCount, connect.BuffRemain(), SocketFlags.None, NetworkReceive, connect);
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "网络设备已断开或网络接收模块发生异常", ex);
            }
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        public override void DisConnect()
        {
            try
            {
                if (this.Socket != null)
                {
                    if (this.Socket.Connected)
                    {
                        this.Socket.Close();
                    }
                    this.Socket = null;
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "关闭网络连接失败", ex);
            }
        }

        //901灯控功能设备搜索
        private const int UDP_SERVER_PORT = 7070;
        private const int UDP_CLIENT_PORT = 7060;
        private const int UDP_INTENT_PREVIEW_PORT = 7080;
        private static Socket UDPServer { get; set; }
        private static UdpClient UdpClient { get; set; }
        private static Thread UDPReceiveThread { get; set; }
        private static bool UDPReceiveStatus { get; set; }
        private static string LocalIp { get; set; }
        private static Dictionary<string, Dictionary<string, NetworkDeviceInfo>> DeviceInfos = new Dictionary<string, Dictionary<string, NetworkDeviceInfo>>();
        private Socket IntentPreviewUDPSender { get; set; }

        //发送网络模拟调试数据测试
        /// <summary>
        /// 功能：发送网络模拟调试数据
        /// </summary>
        /// <param name="targetIp"></param>
        /// <param name="data"></param>
        public void IntentPreview(string targetIp, byte[] data)
        {
            if (this.IntentPreviewUDPSender == null)
            {
                this.IntentPreviewUDPSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.IntentPreviewUDPSender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            }
            this.IntentPreviewUDPSender.SendTo(data, new IPEndPoint(IPAddress.Parse(targetIp), UDP_INTENT_PREVIEW_PORT));
        }
        /// <summary>
        /// 功能：搜索设备
        /// </summary>
        /// <param name="localIP">网卡IP</param>
        public static void SearchDevice(string localIP)
        {
            try
            {
                //配置UDP服务器
                LocalIp = localIP;
                UDPReceiveStatus = false;
                if (UDPServer != null)
                {
                    UDPServer.Close();
                    UdpClient.Close();
                    UDPServer = null;
                    UdpClient = null;
                    UDPReceiveThread = null;
                }
                UDPServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                UDPServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                UDPServer.Bind(new IPEndPoint(IPAddress.Parse(localIP), 8080));
                UdpClient = new UdpClient(new IPEndPoint(IPAddress.Any, UDP_SERVER_PORT));
                UDPReceiveThread = new Thread(SearchDeviceReceiveMsg) { IsBackground = true };
                UDPReceiveThread.Start(UdpClient);
                UDPReceiveStatus = true;
                //开始发送搜索包搜索设备
                if (DeviceInfos.ContainsKey(localIP))
                {
                    DeviceInfos[localIP].Clear();
                }
                else
                {
                    DeviceInfos.Add(localIP, new Dictionary<string, NetworkDeviceInfo>());
                }
                List<byte> packageBuff = new List<byte>();
                byte[] dataBuff = Encoding.Default.GetBytes(Constant.UDP_ORDER);
                byte[] dataLengthBuff = new byte[] { Convert.ToByte(dataBuff.Length & 0xFF), Convert.ToByte((dataBuff.Length >> 8) & 0xFF) };
                byte[] headBuff = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), dataLengthBuff[0], dataLengthBuff[1], Convert.ToByte("00000001", 2), Convert.ToByte(0x00), Convert.ToByte(0x00) };
                packageBuff.AddRange(headBuff);
                packageBuff.AddRange(dataBuff);
                byte[] CRC = CRCTools.GetInstance().GetCRC(packageBuff.ToArray());
                packageBuff[6] = CRC[0];
                packageBuff[7] = CRC[1];
                UDPServer.SendTo(packageBuff.ToArray(), new IPEndPoint(IPAddress.Broadcast, UDP_CLIENT_PORT));
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "搜索设备失败", ex);
                UDPReceiveStatus = false;
                UDPReceiveThread = null;
            }
        }
        /// <summary>
        /// 功能：搜索设备接收模块
        /// </summary>
        /// <param name="obj"></param>
        private static void SearchDeviceReceiveMsg(Object obj)
        {
            try
            {
                UdpClient udpClient = obj as UdpClient;
                while (UDPReceiveStatus)
                {
                    IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, UDP_SERVER_PORT);
                    NetworkDeviceInfo info = new NetworkDeviceInfo();
                    byte[] data = udpClient.Receive(ref iPEndPoint);
                    byte[] buff = new byte[data.Length - 8];
                    Array.Copy(data, 8, buff, 0, buff.Length);
                    string strBuff = Encoding.Default.GetString(buff);
                    string[] strarrau = strBuff.Split(' ');
                    info.DeviceIp = strBuff.Split(' ')[0];
                    int.TryParse(strBuff.Split(' ')[1], out int addr);
                    info.DeviceAddr = addr;
                    info.LocalIp = LocalIp;
                    info.DeviceName = strBuff.Split(' ')[2];
                    if (!DeviceInfos[LocalIp].ContainsKey(info.DeviceIp))
                    {
                        DeviceInfos[LocalIp].Add(info.DeviceIp, info);
                    }
                }
            }
            catch (Exception ex)
            {
                //LogTools.Debug(Constant.TAG_XIAOSA, "搜索设备服务器已关闭");
            }
        }
        /// <summary>
        /// 功能：获取设备列表
        /// </summary>
        public static Dictionary<string, Dictionary<string, NetworkDeviceInfo>> GetDeviceList()
        {
            return DeviceInfos;
        }
        /// <summary>
        /// 功能：清除设备列表
        /// </summary>
        public static void ClearDeviceList()
        {
            DeviceInfos.Clear();
        }


        /// <summary>
        /// 串口专属，网络模块不操作
        /// </summary>
        /// <param name="portName"></param>
        public override void OpenSerialPort(string portName)
        {

        }
    }
}
