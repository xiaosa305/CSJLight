using MultiLedController.entity;
using MultiLedController.entity.dto;
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
    public class TransactionManager
    {
        private const int SEARCH_DEVICE_PORT = 9999;
        private readonly byte[] SEARCH_DEVICE_ORDER = new byte[] { 0xEB,0x55};
        private static TransactionManager Instance { get; set; }

        private Socket SearchUdpServer { get; set; }
        private UdpClient SearchUdpClient { get; set; }
        private Thread SearchReceiveThread { get; set; }
        private string LocalIp { get; set; }
        private bool SearchReceiveStatus { get; set; }
        private Dictionary<string,ControlDevice> ControlDevices { get; set; }


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
            this.ControlDevices = new Dictionary<string, ControlDevice>();
            this.VirtualControlDevices = new List<VirtualControlDevice>();
        }

        public static TransactionManager GetTransactionManager()
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
        public TransactionManager AddDevice(List<ControlDevice> devices,List<List<string>> ips,string serverIp)
        {
            //每个device分配虚拟Ip，分配空间编号
            int startLedSpace = 0;
            for (int index = 0; index < devices.Count; index++)
            {
                VirtualControlDevice device = new VirtualControlDevice(index, startLedSpace, devices[index], ips[index], serverIp);
                this.VirtualControlDevices.Add(device);
                startLedSpace += devices[index].Led_interface_num * devices[index].Led_space;
            }
            return this;
        }

        /// <summary>
        /// 功能：添加若干个控制卡
        /// </summary>
        /// <param name="controlDeviceDTOs">设备包装类信息</param>
        /// <param name="serverIp">DMX服务器IP</param>
        public TransactionManager AddDevice(List<ControlDeviceDTO> controlDeviceDTOs ,string serverIp)
        {
            //每个device分配虚拟Ip，分配空间编号
            int startLedSpace = 0;
            for (int index = 0; index < controlDeviceDTOs.Count; index++)
            {
                VirtualControlDevice device = new VirtualControlDevice(index, startLedSpace, controlDeviceDTOs[index].ControlDevice, controlDeviceDTOs[index].VirtualIps, serverIp);
                this.VirtualControlDevices.Add(device);
                startLedSpace += controlDeviceDTOs[index].ControlDevice.Led_interface_num * controlDeviceDTOs[index].ControlDevice.Led_space;
            }
            return this;
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
                    if (this.SearchUdpClient != null)
                    {
                        this.SearchUdpClient.Close();
                    }
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
                        ControlDevice device = new ControlDevice(receiveData);
                        this.ControlDevices.Add(device.IP, device);
                        LogTools.Debug(Constant.TAG_XIAOSA, "搜索到一台设备" + device.IP);
                    }
                }
            }
            catch (Exception)
            {
                //LogTools.Debug(Constant.TAG_XIAOSA, "搜索设备服务器已关闭");
            }
        }
        /// <summary>
        /// 功能：获取设备列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, ControlDevice> GetControlDevicesList()
        {
            return this.ControlDevices;
        }
        /// <summary>
        /// 功能：清除设备列表
        /// </summary>
        private void ClearControlDeviceList()
        {
            this.ControlDevices.Clear();
        }
        /// <summary>
        /// 功能：关闭搜索设备服务器
        /// </summary>
        public void CloseSearchDeviceServers()
        {
            if (this.SearchUdpServer != null)
            {
                this.SearchReceiveStatus = false;
                this.SearchUdpServer.Close();
                this.SearchUdpClient.Close();
                this.SearchUdpServer = null;
                this.SearchUdpClient = null;
                this.SearchReceiveThread = null;
            }
        }

        //测试用
        public TransactionManager StartAllDeviceDebug()
        {
            for (int index = 0; index < this.VirtualControlDevices.Count; index++)
            {
                this.VirtualControlDevices[index].StartDebugMode();
            }
            return this;
        }

        public TransactionManager StopAllDeviceDebug()
        {
            for (int index = 0; index < this.VirtualControlDevices.Count; index++)
            {
                this.VirtualControlDevices[index].StopDebugMode();
            }
            return this;
        }

        public TransactionManager StartAllDeviceRecode()
        {
            for (int index = 0; index < this.VirtualControlDevices.Count; index++)
            {
                this.VirtualControlDevices[index].SetSaveFilePath(@"C:\Users\99729\Desktop\Test\SC00" + index + @".bin");
                this.VirtualControlDevices[index].StartRecode();
            }
            return this;
        }

        public TransactionManager StopAllDeviceRecode()
        {
            for (int index = 0; index < this.VirtualControlDevices.Count; index++)
            {
                this.VirtualControlDevices[index].StopRecode();
            }
            return this;
        }


        public TransactionManager StartAllDeviceReceiveDmxData()
        {
            for (int index = 0; index < this.VirtualControlDevices.Count; index++)
            {
                this.VirtualControlDevices[index].StartReceiveDMXData();
            }
            return this;
        }

        public TransactionManager StopAllDeviceReceiveDmxData()
        {
            for (int index = 0; index < this.VirtualControlDevices.Count; index++)
            {
                this.VirtualControlDevices[index].StopReceiveDMXData();
            }
            return this;
        }

        public TransactionManager SetRecodeFilePath(List<SetRecodeFilePathDTO> recodeFilePathDTOs)
        {
            if (this.VirtualControlDevices != null)
            {
                if (this.VirtualControlDevices.Count != 0)
                {
                    if (recodeFilePathDTOs != null)
                    {
                        foreach (SetRecodeFilePathDTO dTO in recodeFilePathDTOs)
                        {
                            foreach (VirtualControlDevice virtualControlDevice in this.VirtualControlDevices)
                            {
                                if (virtualControlDevice.IsThisDevice(dTO.ControlDevice.IP))
                                {
                                    virtualControlDevice.SetSaveFilePath(dTO.RecodeFilePath);
                                }
                            }
                        }
                    }
                }
            }
            return this;
        }

        public TransactionManager CloseAllDevice()
        {
            foreach (VirtualControlDevice device in this.VirtualControlDevices)
            {
                device.CloseVirtualControlDevice();
            }
            this.ClearControlDeviceList();
            return this;
        }
    }
}
