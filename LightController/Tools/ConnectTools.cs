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
        //服务器端口
        private int Serverport { get; set; }
        private bool IsStart { get; set; }

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
        public void Start(string ip, int port)
        {
            UdpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpClient udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 7070));
            Thread thread = new Thread(RecevieMsg);
            thread.IsBackground = true;
            thread.Start(udpClient);
            IsStart = false;
            SocketTools.GetInstance().Start(new IPEndPoint(IPAddress.Parse(ip), port));
            IsStart = true;
        }
        /// <summary>
        /// 发送UDP广播包检索设备
        /// </summary>
        /// <param name="udpServerIp">服务器ip地址</param>
        /// <param name="udpPort">服务器端口号</param>
        public void SerchDevice(string udpServerIp,int udpPort)
        {
            if (IsStart)
            {
                Console.WriteLine("Start SerchDevice");
                ServerIp = udpServerIp;
                Serverport = udpPort;
                UdpServer.SendTo(Encoding.Default.GetBytes(udpServerIp + " " + udpPort), new IPEndPoint(IPAddress.Parse(udpServerIp), udpPort));
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
                udpClient.BeginReceive(delegate (IAsyncResult result)
                {
                    string str = result.AsyncState.ToString();
                    string ip = str.Split(' ')[0];
                    int port = Convert.ToInt32(str.Split(' ')[1]);
                    if (!ip.Equals(ServerIp))
                    {
                        SocketTools.GetInstance().AddConnect(ip, port);
                    }
                }, Encoding.UTF8.GetString(udpClient.Receive(ref endPoint)));
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="ip">目标ip</param>
        /// <param name="data">数据包</param>
        /// <param name="order">命令</param>
        /// <param name="strArray">备注信息</param>
        public void Send(string ip,byte[] data,ORDER order,string[] strArray)
        {
            if (IsStart)
            {
                SocketTools.GetInstance().Send(ip, data, order, strArray);
            }
            else
            {
                throw new Exception("未启动服务");
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
                throw new Exception("未启动服务");
            }
        }
    }
}
