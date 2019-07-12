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
    public class SocketTools
    {
        //Socket实例
        private static SocketTools Instance { get; set; }
        //客户端连接（异步）
        private Conn[] conns;
        //最大连接数
        private int MaxCount { get; set; }
        /// <summary>
        /// 获取连接池索引
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 构造函数
        /// </summary>
        private SocketTools()
        {
            MaxCount = 100;
        }
        /// <summary>
        /// 获取SockeTools实例
        /// </summary>
        /// <returns></returns>
        public static SocketTools GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SocketTools();
            }
            return Instance;
        }
        /// <summary>
        /// 开启连接池
        /// </summary>
        /// <param name="iPEndPoint"></param>
        public void Start()
        {
            //连接对象池
            conns = new Conn[MaxCount];
            for (int i = 0; i < MaxCount; i++)
            {
                conns[i] = new Conn();
            }
        }
        /// <summary>
        /// 添加新的连接到连接池
        /// </summary>
        /// <param name="ip">连接ip</param>
        /// <param name="port">连接端口</param>
        public void AddConnect(string ip,int port)
        {
            try
            {
                for (int i = 0; i < MaxCount; i++)
                {
                    if (conns[i] != null || conns[i].IsUse)
                    {
                        if(conns[i].Ip != null)
                        {
                            if (conns[i].Ip.Equals(ip))
                            {
                                return;
                            }
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
                    socket.Connect(iPEndPoint);
                    conn.Init(socket);
                    Console.WriteLine("客户端 [" + conn.GetAddress() + "] 连接");
                    Console.WriteLine("已连接" + (index + 1) + "个客户端");
                    conn.BeginReceive();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("设备连接失败");
                for (int i = 0; i < MaxCount; i++)
                {
                    if (conns[i] != null || conns[i].IsUse)
                    {
                        if (conns[i].Ip != null)
                        {
                            if (conns[i].Ip.Equals(ip))
                            {
                                conns[i].Close();
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 配置连接发送数据包的包大小
        /// </summary>
        /// <param name="ip">连接ip</param>
        /// <param name="size">包大小</param>
        public void SetPakegeSize(string ip,int size)
        {
            foreach (Conn value in conns)
            {
                if (value.Ip.Equals(ip))
                {
                    value.SetPakegeSize(size);
                }
            }
        }
        /// <summary>
        /// 获取所有已连接设备ip
        /// </summary>
        /// <returns></returns>
        public IList<string> GetDeviceList()
        {
            IList<string> deviceList = new List<string>();
            {
                foreach (Conn value in conns)
                {
                    if (value != null || value.IsUse)
                    {
                        if (value.Ip != null)
                        {
                            deviceList.Add(value.Ip);
                        }
                    }
                }
            }
            return deviceList;
        }
        /// <summary>
        /// 下载所有文件数据
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="dBWrapper"></param>
        /// <param name="configPath"></param>
        public void Download(string ip,DBWrapper dBWrapper,string configPath,IReceiveCallBack callBack)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] != null || conns[i].IsUse)
                {
                    if (conns[i].Ip.Equals(ip))
                    {
                        conns[i].DownloadFile(dBWrapper, configPath,callBack);
                    }
                }
            }
        }
        /// <summary>
        /// 发送控制命令
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="order"></param>
        /// <param name="array"></param>
        public void SendOrder(string ip,string order,string[] array,IReceiveCallBack receiveCallBack)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] != null || conns[i].IsUse)
                {
                    if (conns[i].Ip.Equals(ip))
                    {
                        conns[i].SendOrder(order, array,receiveCallBack);
                    }
                }
            }
        }
    }
}
