using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
    public class SockTools
    {
        private static SockTools Instance { get; set; }

        //监听套接字
        private Socket ListenSocket { get; set; }

        //客户端连接（异步）
        private Conn[] conns;

        //最大连接数
        private int MaxCount { get; set; }

        //获取连接池索引
        private int NewIndex()
        {
            if (conns == null) return -1;
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] == null)
                {
                    conns[i] = new Conn();
                    return i;
                }
                else if (conns[i].IsUse == false)
                {
                    return i;
                }
            }
            return -1;
        }

        private SockTools()
        {
            MaxCount = 100;
        }

        public static SockTools GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SockTools();
            }
            return Instance;
        }

        //开启服务器
        public void Start(IPEndPoint iPEndPoint)
        {
            //连接对象池
            conns = new Conn[MaxCount];
            for (int i = 0; i < MaxCount; i++)
            {
                conns[i] = new Conn();
            }

            //socket
            ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //bind
            ListenSocket.Bind(iPEndPoint);

            //listen
            ListenSocket.Listen(MaxCount);

            //Accept
            ListenSocket.BeginAccept(AcceptCb, null);
        }

        //Accept回调函数
        private void AcceptCb(IAsyncResult asyncResult)
        {
            try
            {
                Socket socket = ListenSocket.EndAccept(asyncResult);
                int index = NewIndex();
                if (index == -1)
                {
                    Console.WriteLine("Conn连接池已满");
                }
                else
                {
                    Conn conn = conns[index];
                    conn.Init(socket);
                    string addr = conn.GetAddress();
                    Console.WriteLine("客户端 [" + addr + "] 连接");
                    Console.WriteLine("已连接" + (index + 1) + "个客户端");
                    conn.Socket.BeginReceive(conn.ReadBuff, conn.BuffCount, conn.BuffRemain(), SocketFlags.None, ReceiveCb, conn);
                    ListenSocket.BeginAccept(AcceptCb, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("AcceptCb 失败：" + ex.Message);
            }
        }

        //Recevie回调函数
        private void ReceiveCb(IAsyncResult asyncResult)
        {
            //获取接受对象
            Conn conn = (Conn)asyncResult.AsyncState;
            try
            {
                int count = conn.Socket.EndReceive(asyncResult);
                //关闭信号
                if (count <= 0)
                {
                    Console.WriteLine("收到 [" + conn.GetAddress() + "] 断开连接");
                    conn.Close();
                    return;
                }
                //数据处理
                string str = conn.Socket.RemoteEndPoint.ToString() + ":" + Encoding.UTF8.GetString(conn.ReadBuff, 0, count);
                Console.WriteLine("[" + conn.GetAddress() + "] : " + str);
                byte[] sendMsg = Encoding.UTF8.GetBytes(str);
                //将所有的消息广播
                for (int i = 0; i < MaxCount; i++)
                {
                    if (conns[i] == null || !conns[i].IsUse) continue;
                    conns[i].Socket.Send(sendMsg);
                }

                //继续接受
                conn.Socket.BeginReceive(conn.ReadBuff, conn.BuffCount, conn.BuffRemain(), SocketFlags.None, ReceiveCb, conn);
            }
            catch (Exception)
            {
                Console.WriteLine("[" + conn.GetAddress() + "] 断开连接");
                conn.Close();
            }
        }

        //Udp广播测试
        public void UdpTest(string str)
        {
            Conn conn = null;
            int index = 0;
            try
            {
                byte[] sendMsg = Encoding.UTF8.GetBytes(str);
                for (int i = 0; i < MaxCount; i++)
                {
                    index = i;
                    conn = conns[i];
                    if (conn == null || !conn.IsUse) continue;
                    conn.Socket.Send(sendMsg);
                }
            }
            catch (Exception)
            {
                if (conn != null)
                {
                    Console.WriteLine("[" + conn.GetAddress() + "] 断开连接");
                    conns[index].IsUse = false;
                    conns[index].Close();
                }
            }
        }

        public void Test(string ipadd)
        {
            IPAddress iPAddress = IPAddress.Parse(ipadd);
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 7060);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(iPEndPoint);
            int i = 0;
            while (true)
            {
                byte[] data = Encoding.Default.GetBytes("ack " + i);
                i++;
                client.BeginSend(data, 0, data.Length, SocketFlags.None, TestCb, client);
            }
        }

        private void TestCb(IAsyncResult asyncResult)
        {

        }

       
    }
}
