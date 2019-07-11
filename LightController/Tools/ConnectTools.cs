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
        //连接工具实例
        private static ConnectTools Instance { get; set; }
        //Udp服务套接字
        private Socket UdpServer { get; set; }
        //服务器ip
        private string ServerIp { get; set; }
        private bool IsStart { get; set; }
        public bool IsSending { get; set; }
        private Thread SendThread { get; set; }
        public string Ip { get; set; }
        public bool IsSendFileCompleted { get; set; }
        private UdpClient UdpClient { get; set; }

        private ConnectTools()
        {
            IsSending = false;
            IsSendFileCompleted = false;
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
            UdpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 7070));
            UdpServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            Thread thread = new Thread(RecevieMsg);
            thread.IsBackground = true;
            thread.Start(UdpClient);
            IsStart = false;
            SocketTools.GetInstance().Start();
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
                byte[] buffData = Encoding.Default.GetBytes("UdpBroadCast");
                byte[] buffDataLength = new byte[] {Convert.ToByte(buffData.Length & 0xFF),Convert.ToByte((buffData.Length >> 8) & 0xFF) };
                byte[] buffHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), buffDataLength[0], buffDataLength[1], Convert.ToByte("00000001",2), Convert.ToByte(0x00), Convert.ToByte(0x00) };
                buff.AddRange(buffHead);
                buff.AddRange(buffData);
                byte[] CRC = CRCTools.GetInstance().GetCRC(buff.ToArray());
                buff[6] = CRC[0];
                buff[7] = CRC[1];
                UdpServer.SendTo(buff.ToArray(), new IPEndPoint(IPAddress.Broadcast, 7060));
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
                byte[] readBuff = udpClient.Receive(ref iPEndPoint);
                SocketTools.GetInstance().AddConnect(Encoding.Default.GetString(readBuff), 7060);
            }
        }
        /// <summary>
        /// 配置发送数据包单包上限
        /// </summary>
        /// <param name="ip">连接ip</param>
        /// <param name="size">数据包单包上限</param>
        public void SetConnectPakegeSize(string ip,int size)
        {
            if (IsStart)
            {
                SocketTools.GetInstance().SetPakegeSize(ip, size);
            }
            else
            {
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

        public void Download(string[] ip, DBWrapper dBWrapper,string configPath)
        {
            foreach (string item in ip)
            {
                SocketTools.GetInstance().Download(item, dBWrapper, configPath, new DownloadCallBack());
            }
        }

        public void SendOrder(string[] ip,string order,string[] strarray)
        {

        }
    }

    public class DownloadCallBack : IReceiveCallBack
    {
        public void SendCompleted(string ip)
        {
            Console.WriteLine("下载完成");
        }

        public void SendError(string ip,string order)
        {
            Console.WriteLine(order + ":下载失败");
        }
    }
}
