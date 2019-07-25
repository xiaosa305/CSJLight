using LightController.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
    public class ConnectTools
    {
        private readonly int UDP_SERVER_PORT = 7070;
        private readonly int UDP_CLIENT_PORT = 7060;
        private static ConnectTools Instance { get; set; }//连接工具实例
        private Socket UdpServer { get; set; }//Udp服务套接字
        private string ServerIp { get; set; }//服务器ip
        private bool IsStart { get; set; }
        private Thread SendThread { get; set; }
        private string Ip { get; set; }
        private UdpClient UdpClient { get; set; }

        private ConnectTools()
        {
        }

        /// <summary>
        /// 获取连接工具实例
        /// </summary>
        /// <returns></returns>
        public static ConnectTools GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ConnectTools();
            }
            return Instance;
        }

        /// <summary>
        /// 启动Tcp服务连接池
        /// </summary>
        /// <param name="ip">Tcp服务器Ip地址</param>
        /// <param name="port">Tcp服务器端口号</param>
        public void Start(string ip)
        {
            ServerIp = ip;
            UdpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpClient = new UdpClient(new IPEndPoint(IPAddress.Any, UDP_SERVER_PORT));
            UdpServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            Thread thread = new Thread(RecevieMsg);
            thread.IsBackground = true;
            IsStart = false;
            SocketTools.GetInstance().Start();
            thread.Start(UdpClient);
            IsStart = true;
        }

        /// <summary>
        /// 发送UDP广播包检索设备
        /// </summary>
        /// <param name="udpServerIp">服务器ip地址</param>
        /// <param name="udpPort">服务器端口号</param>
        public void SearchDevice()
        {
            if (IsStart)
            {
                Console.WriteLine("Start SerchDevice");
                List<byte> buff = new List<byte>();
                byte[] buffData = Encoding.Default.GetBytes(Constant.UDP_ORDER);
                byte[] buffDataLength = new byte[] {Convert.ToByte(buffData.Length & 0xFF),Convert.ToByte((buffData.Length >> 8) & 0xFF) };
                byte[] buffHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), buffDataLength[0], buffDataLength[1], Convert.ToByte("00000001",2), Convert.ToByte(0x00), Convert.ToByte(0x00) };
                buff.AddRange(buffHead);
                buff.AddRange(buffData);
                byte[] CRC = CRCTools.GetInstance().GetCRC(buff.ToArray());
                buff[6] = CRC[0];
                buff[7] = CRC[1];
                UdpServer.SendTo(buff.ToArray(), new IPEndPoint(IPAddress.Broadcast, UDP_CLIENT_PORT));
            }
            else
            {
                throw new Exception("未启动服务");
            }
            
        }

        /// <summary>
        /// UDP广播数据接收
        /// </summary>
        /// <param name="obj"></param>
        private void RecevieMsg(object obj)
        {
            UdpClient udpClient = obj as UdpClient;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 7070);
                SocketTools.GetInstance().AddConnect(Encoding.Default.GetString(udpClient.Receive(ref iPEndPoint)), 7060);
            }
        }

        /// <summary>
        /// 配置发送数据包单包上限
        /// </summary>
        /// <param name="ip">连接ip</param>
        /// <param name="size">数据包单包上限</param>
        public void SetConnectPackageSize(string ip,int size)
        {
            if (IsStart)
            {
                SocketTools.GetInstance().SetPackageSize(ip, size);
            }
            else
            {
                throw new Exception("未启动服务");
            }
        }

        /// <summary>
        /// 获取所有已连接设备ip
        /// </summary>
        /// <returns></returns>
        public IList<string> GetDevicesIp()
        {
            if (IsStart)
            {
                return SocketTools.GetInstance().GetDeviceList();
            }
            else
            {
                throw new Exception("未启动服务");
            }
        }

        /// <summary>
        /// 获取所有已连接设备的标识
        /// </summary>
        /// <returns></returns>
        public IList<string> GetDeviceNames()
        {
            return SocketTools.GetInstance().GetDeviceNameList();
        }

        /// <summary>
        /// 获取所有已连接设备到ip以及设备标识
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> GetDeviceInfo()
        {
            return SocketTools.GetInstance().GetDeviceInfos();
        }

        /// <summary>
        /// 下载所有常规程序、音频程序以及全局配置文件到指定终端设备
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="dBWrapper"></param>
        /// <param name="configPath"></param>
        /// <param name="callBack"></param>
        public void Download(IList<string> ips, DBWrapper dBWrapper,string configPath,IReceiveCallBack callBack)
        {
            foreach (string ip in ips)
            {
                SocketTools.GetInstance().Download(ip, dBWrapper, configPath, callBack);
            }
        }

        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="order"></param>
        /// <param name="strarray"></param>
        /// <param name="callBack"></param>
        public void SendOrder(IList<string> ips, string order,string[] strarray,IReceiveCallBack callBack)
        {
            foreach (string ip in ips)
            {
                SocketTools.GetInstance().SendOrder(ip, order, strarray, callBack);
            }
        }

        /// <summary>
        /// 发送硬件配置文件到指定终端设备
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="filePath"></param>
        /// <param name="receiveCallBack"></param>
        public void PutPara(IList<string> ips, string filePath,IReceiveCallBack receiveCallBack)
        {
            foreach (string ip in ips)
            {
                SocketTools.GetInstance().PutPara(ip, filePath, receiveCallBack);
            }
        }
    }

 
}
