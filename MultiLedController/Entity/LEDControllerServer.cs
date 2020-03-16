using MultiLedController.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiLedController.Entity
{
    public class LEDControllerServer
    {
        private static LEDControllerServer Instance { get; set; }
        private Art_Net_Manager Manager { get; set; }
        private const int PORT = 1025;
        private Socket UDPSend { get; set; }
        private UdpClient UDPReceiveClient { get; set; }
        private string ServerCurrentIp { get; set; }
        private bool IsStart { get; set; }
        private bool ReceiveStartStatus { get; set; }
        private Thread ReceiveThread { get; set; }
        private Dictionary<string,ControlDevice> ControlDevices { get; set; }

        private LEDControllerServer()
        {
            this.IsStart = false;
            this.ReceiveStartStatus = false;
            this.InitDeviceList();
        }
        public static LEDControllerServer GetInstance()
        {
            if (Instance == null)
            {
                Instance = new LEDControllerServer();
            }
            return Instance;
        }

        public void SetManager(Art_Net_Manager manager)
        {
            this.Manager = manager;
        }
        /// <summary>
        /// 配置本地服务器IP并启动服务器
        /// </summary>
        /// <param name="ip"></param>
        public void StartServer(string ip)
        {
            this.ReceiveStartStatus = false;
            this.ServerCurrentIp = ip;
            if (this.UDPSend != null)
            {
                this.UDPSend.Close();
                this.UDPReceiveClient.Close();
                Thread.Sleep(100);
            }
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(ServerCurrentIp), PORT);
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
        /// <summary>
        /// 网络接收监听模块
        /// </summary>
        /// <param name="obj"></param>
        private void ReceiveMsg(Object obj)
        {
            try
            {
                UdpClient client = obj as UdpClient;
                while (this.ReceiveStartStatus)
                {
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, PORT);
                    byte[] receiveData = client.Receive(ref endPoint);
                    if (Encoding.Default.GetString(receiveData).Equals("OK:poweron>"))//发送起始命令回复
                    {
                        this.Manager.StartDebug();
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
            catch (Exception)
            {
                Console.WriteLine("关闭UDPClient");
            }
        }
        /// <summary>
        /// 网络发送模块
        /// </summary>
        /// <param name="data"></param>
        public void SendDebugData(List<byte> data)
        {
            if (data[4] == 0x00)
            {
                Console.WriteLine("addr-8：" + data[7] + ",addr-9：" + data[8] + ",addr-10：" + data[9]);
                if (data[7] == 0 && data[8] == 0 & data[9] == 0)
                {
                    Console.WriteLine("全0");
                }
            }
            //Console.WriteLine("addr-11：" + data[10] + ",addr-12：" + data[11] + ",addr-13：" + data[12]);
            this.UDPSend.SendTo(data.ToArray(), new IPEndPoint(IPAddress.Parse("192.168.31.10"), PORT));
        }
        /// <summary>
        /// 搜索设备
        /// </summary>
        /// <param name="data"></param>
        public void SearchDevice(List<byte> data)
        {
            this.UDPSend.SendTo(data.ToArray(), new IPEndPoint(IPAddress.Broadcast, 9999));
        }

        /// <summary>
        /// 初始化设备存储缓存区
        /// </summary>
        public void InitDeviceList()
        {
            this.ControlDevices = new Dictionary<string, ControlDevice>();
        }

        public Dictionary<string,ControlDevice> GetControlDevices()
        {
            return this.ControlDevices;
        }
    }
}
