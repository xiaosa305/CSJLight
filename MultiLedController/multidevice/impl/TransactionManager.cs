using MultiLedController.entity;
using MultiLedController.utils;
using MultiLedController.utils.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiLedController.multidevice.impl
{
    public class TransactionManager:ITransactionManager
    {
        private const int SEARCH_DEVICE_PORT = 9999;
        private readonly byte[] SEARCH_DEVICE_ORDER = new byte[] { 0xEB,0x55};
        private static ITransactionManager Instance { get; set; }

        private Socket SearchUdpServer { get; set; }
        private UdpClient SearchUdpClient { get; set; }
        private Thread SearchReceiveThread { get; set; }
        private string LocalIp { get; set; }
        private bool SearchReceiveStatus { get; set; }
        private List<ControlDevice> ControlDevices { get; set; }


        private List<VirtualControlDevice> VirtualControlDevices { get; set; }


        private TransactionManager()
        {
            InitParameter();
        }

        /// <summary>
        /// 功能：初始化参数
        /// </summary>
        private void InitParameter()
        {
            this.SearchReceiveStatus = false;
            this.ControlDevices = new List<ControlDevice>();
            this.VirtualControlDevices = new List<VirtualControlDevice>();
        }

        public static ITransactionManager GetTransactionManager()
        {
            if (Instance == null)
            {
                Instance = new TransactionManager();
            }
            return Instance;
        }

        /// <summary>
        /// 功能：添加若干个控制卡
        /// </summary>
        /// <param name="devices">设备信息，绑定设备编号</param>
        /// <param name="Ips">设备对应虚拟Ip地址，绑定编号</param>
        public void AddDevice(List<ControlDevice> devices,List<List<string>> ips,string serverIp)
        {
            //每个device分配虚拟Ip，分配空间编号
            int startLedSpace = 0;
            for (int index = 0; index < devices.Count; index++)
            {
                this.VirtualControlDevices.Add(new VirtualControlDevice(index,startLedSpace, devices[index], ips[index],serverIp));
                startLedSpace += devices[index].Led_interface_num * devices[index].Led_space;
            }
        }

        /// <summary>
        /// 功能：搜索幻彩控制卡
        /// </summary>
        /// <param name="localIp">网卡Ip</param>
        public void SearchDevice(string localIp)
        {
            try
            {
                //配置服务器
                this.LocalIp = localIp;
                this.SearchReceiveStatus = false;
                if (this.SearchUdpServer != null)
                {
                    this.SearchUdpServer.Close();
                    this.SearchUdpClient.Close();
                    this.SearchUdpServer = null;
                    this.SearchUdpClient = null;
                    this.SearchReceiveThread = null;
                }
                this.SearchUdpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.SearchUdpServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                this.SearchUdpServer.Bind(new IPEndPoint(IPAddress.Parse(this.LocalIp), SEARCH_DEVICE_PORT));
                this.SearchUdpClient = new UdpClient() { Client = this.SearchUdpServer };
                this.SearchReceiveThread = new Thread(this.SearchDeviceReceiveMsg) { IsBackground = true };
                this.SearchReceiveThread.Start(this.SearchUdpClient);
                this.SearchReceiveStatus = true;
                //发送搜索命令
                this.SearchUdpServer.SendTo(SEARCH_DEVICE_ORDER, new IPEndPoint(IPAddress.Broadcast, SEARCH_DEVICE_PORT));
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "搜索设备失败", ex);
                this.SearchReceiveStatus = false;
                this.SearchReceiveThread = null;
            }
        }
        /// <summary>
        /// 功能：搜索设备
        /// </summary>
        /// <param name="obj"></param>
        private void SearchDeviceReceiveMsg(Object obj)
        {
            try
            {
                UdpClient udpClient = obj as UdpClient;
                while (this.SearchReceiveStatus)
                {
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, SEARCH_DEVICE_PORT);
                    byte[] receiveData = udpClient.Receive(ref endPoint);
                    if (receiveData.Length == 41)//设备探索回复
                    {
                        this.ControlDevices.Add(new ControlDevice(receiveData));
                        LogTools.Debug(Constant.TAG_XIAOSA, "搜索到一台设备");
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
        /// <returns></returns>
        public List<ControlDevice> GetControlDevicesList()
        {
            return this.ControlDevices;
        }
        /// <summary>
        /// 功能：清除设备列表
        /// </summary>
        public void ClearControlDeviceList()
        {
            this.ControlDevices.Clear();
        }

    }
}
