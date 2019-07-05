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

            ////socket
            //ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            ////bind
            //ListenSocket.Bind(iPEndPoint);

            ////listen
            //ListenSocket.Listen(MaxCount);

            ////Accept
            //ListenSocket.BeginAccept(AcceptCb, null);
        }

        ////Accept回调函数
        //private void AcceptCb(IAsyncResult asyncResult)
        //{
        //    try
        //    {
        //        Socket socket = ListenSocket.EndAccept(asyncResult);
        //        int index = NewIndex();
        //        if (index == -1)
        //        {
        //            Console.WriteLine("Conn连接池已满");
        //        }
        //        else
        //        {
        //            Conn conn = conns[index];
        //            conn.Init(socket);
        //            string addr = conn.GetAddress();
        //            Console.WriteLine("客户端 [" + addr + "] 连接");
        //            Console.WriteLine("已连接" + (index + 1) + "个客户端");
        //            conn.Socket.BeginReceive(conn.ReadBuff, conn.BuffCount, conn.BuffRemain(), SocketFlags.None, ReceiveCb, conn);
        //            ListenSocket.BeginAccept(AcceptCb, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("AcceptCb 失败：" + ex.Message);
        //    }
        //}

        //添加新连接
        public void AddConnect(string ip,int port)
        {
            try
            {
                for (int i = 0; i < MaxCount; i++)
                {
                    if (conns[i] != null || conns[i].IsUse)
                    {
                        if (conns[i].GetAddress().Equals(ip))
                        {
                            return;
                        }
                    }
                }
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                int index = NewIndex();
                if (index == -1)
                {
                    throw new Exception("网络连接池已满");
                }
                else
                {

                    Conn conn = conns[index];
                    conn.Init(socket);
                    socket.Connect(iPEndPoint);
                    string addr = conn.GetAddress();
                    Console.WriteLine("客户端 [" + addr + "] 连接");
                    Console.WriteLine("已连接" + (index + 1) + "个客户端");
                    conn.BeginReceive();

                }
            }
            catch (Exception)
            {
                Console.WriteLine("设备连接失败");
            }
        }

        public void SendData(string ip,byte[] data ,ORDER order,string[] strArray)
        {
            foreach (Conn value in conns)
            {
                string addr = value.GetAddress().Split(':')[0];
                if (addr.Equals(ip))
                {
                    value.SendData(data, order, strArray);
                }
            }
        }
    }
}
