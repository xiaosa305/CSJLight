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
        private static ConnectTools Instance { get; set; }
        private Socket UdpServer { get; set; }

        private ConnectTools()
        {
            Console.WriteLine("Started UDP Server");
            SockTools.GetInstance().Start(new IPEndPoint(IPAddress.Parse("192.168.31.235"), 7070));
            UdpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpClient udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 7070))
;           Thread thread = new Thread(RecevieMsg);
            thread.IsBackground = true;
            thread.Start(udpClient);
            Console.WriteLine("启动线程:" + thread.GetHashCode());
        }

        public static ConnectTools GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ConnectTools();
            }
            return Instance;
        }

        public void SerchDevice()
        {
            Console.WriteLine("SerchDevice");
            UdpServer.SendTo(Encoding.Default.GetBytes("192.168.31.235" + " " + "7070"), new IPEndPoint(IPAddress.Parse("192.168.31.235"), 7070));
        }

        private void RecevieMsg(object obj)
        {
            UdpClient udpClient = obj as UdpClient;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                udpClient.BeginReceive(delegate (IAsyncResult result)
                {
                    //接受消息
                    string str = result.AsyncState.ToString();
                    string ip = str.Split(' ')[0];
                    if (!ip.Equals("192.168.31.235"))
                    {
                        int port = Convert.ToInt32(str.Split(' ')[1]);
                        SockTools.GetInstance().AddConnect(ip, port);
                        Console.WriteLine("Finded Client :" + ip);
                    }
                }, Encoding.UTF8.GetString(udpClient.Receive(ref endPoint)));
            }
        }

        public void SendData(string ip,byte[] data,ORDER order,string[] strArray)
        {
            SockTools.GetInstance().SendData(ip, data, order, strArray);
        }
    }
}
