using MultiLedController.entity;
using MultiLedController.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiLedController.utils.impl
{
    public class LEDControllerServer : ILEDControllerServer
    {
        private static LEDControllerServer Instance { get; set; }
        private IArt_Net_Manager Manager { get; set; }
        private Socket UDPSend { get; set; }
        private UdpClient UDPReceiveClient { get; set; }
        private string ServerCurrentIp { get; set; }
        private bool IsStart { get; set; }
        private bool ReceiveStartStatus { get; set; }
        private Thread ReceiveThread { get; set; }
        private Dictionary<string,ControlDevice> ControlDevices { get; set; }
        private ControlDevice CurrentDevice { get; set; }

        private LEDControllerServer()
        {
            this.IsStart = false;
            this.ReceiveStartStatus = false;
            this.InitDeviceList();
        }
        public static ILEDControllerServer GetInstance()
        {
            if (Instance == null)
            {
                Instance = new LEDControllerServer();
            }
            return Instance;
        }
        /// <summary>
        /// 功能：配置管理器
        /// </summary>
        /// <param name="manager"></param>
        public void SetManager(IArt_Net_Manager manager)
        {
            this.Manager = manager;
        }
        /// <summary>
        /// 功能：配置本地服务器IP并启动服务器
        /// </summary>
        /// <param name="ip"></param>
        public void StartServer(string ip)
        {
            try
            {
                this.ReceiveStartStatus = false;
                this.ServerCurrentIp = ip;
                if (this.UDPSend != null)
                {
                    this.UDPSend.Close();
                    this.UDPReceiveClient.Close();
                    Thread.Sleep(100);
                }
                IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(ServerCurrentIp), 9999);
                this.UDPSend = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.UDPSend.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                this.UDPSend.Bind(iPEnd);
                this.UDPReceiveClient = new UdpClient()
                {
                    Client = this.UDPSend
                };
                this.ReceiveThread = new Thread(this.ReceiveMsg)
                {
                    IsBackground = true
                };
                this.ReceiveThread.Start(this.UDPReceiveClient);
                this.ReceiveStartStatus = true;
                this.IsStart = true;
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "控制卡服务器绑定本地IP失败", ex, true, "控制卡服务器绑定本地IP失败");
            }
            
        }
        /// <summary>
        /// 功能：网络接收监听模块
        /// </summary>
        /// <param name="obj"></param>
        private void ReceiveMsg(Object obj)
        {
            try
            {
                UdpClient client = obj as UdpClient;
                while (this.ReceiveStartStatus)
                {
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 9999);
                    byte[] receiveData = client.Receive(ref endPoint);
                    if (Encoding.Default.GetString(receiveData).Equals("OK:poweron>"))//发送起始命令回复
                    {
                        this.Manager.StartDebugMode();
                    }
                    else if (receiveData.Length == 41)//设备探索回复
                    {
                        Console.WriteLine("搜索到一台设备");
                        ControlDevice controlDevice = new ControlDevice(receiveData);
                        this.ControlDevices.Add(controlDevice.Mac,controlDevice);
                    }
                    else
                    {
                        //Console.WriteLine("接收到数据包，包大小：" + receiveData.Length + "-------包序为：" + receiveData[4]);
                    }
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "关闭控制卡服务器", ex);
            }
        }
        /// <summary>
        /// 功能：网络发送模块
        /// </summary>
        /// <param name="data"></param>
        public void SendDebugData(List<byte> data)
        {
            this.UDPSend.SendTo(data.ToArray(), new IPEndPoint(IPAddress.Parse(this.CurrentDevice.IP), this.CurrentDevice.LinkPort));
        }
        /// <summary>
        /// 功能：搜索设备
        /// </summary>
        /// <param name="data"></param>
        public void SearchDevice(List<byte> data)
        {
            this.UDPSend.SendTo(data.ToArray(), new IPEndPoint(IPAddress.Broadcast, 9999));
        }
        /// <summary>
        /// 功能：初始化设备存储缓存区
        /// </summary>
        public void InitDeviceList()
        {
            this.ControlDevices = new Dictionary<string, ControlDevice>();
        }
        /// <summary>
        /// 功能：获取所有控制卡信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,ControlDevice> GetControlDevices()
        {
            return this.ControlDevices;
        }
        /// <summary>
        /// 功能：关闭控制卡服务器
        /// </summary>
        public void Close()
        {
            try
            {
                if (UDPSend != null)
                {
                    this.ReceiveStartStatus = false;
                    this.UDPSend.Close();
                    this.UDPReceiveClient.Close();
                    this.UDPSend = null;
                    this.UDPReceiveClient = null;
                    this.InitDeviceList();
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "关闭控制器服务器失败", ex);
            }
        }
        /// <summary>
        /// 功能：设置控制卡信息
        /// </summary>
        public void SetControlDevice(ControlDevice device)
        {
            this.CurrentDevice = device;
        }
    }
}
