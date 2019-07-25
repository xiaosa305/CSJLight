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
        private static SocketTools Instance { get; set; }//Socket实例
        private Conn[] conns;//客户端连接（异步）
        private int MaxCount { get; set; }//最大连接数
        private static int test = 0;

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
                test++;
            }
            return Instance;
        }

        /// <summary>
        /// 开启连接池
        /// </summary>
        /// <param name="iPEndPoint"></param>
        public void Start()
        {
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
        public void AddConnect(string str,int port)
        {
            string[] strarrau = str.Split(' ');
            string ip = str.Split(' ')[0];
            string addr = str.Split(' ')[1];
            string deviceName = str.Split(' ')[2];
            if (strarrau.Length > 3)
            {
                for (int i = 3; i < strarrau.Length; i++)
                {
                    deviceName += " " + strarrau[i];
                }
            }
            try
            {
                for (int i = 0; i < MaxCount; i++)
                {
                    if (conns[i] != null || conns[i].IsUse)
                    {
                        if (conns[i].Ip != null)
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
                    Console.WriteLine("网络连接池已满");
                }
                else
                {
                    Conn conn = conns[index];
                    socket.Connect(iPEndPoint);
                    int.TryParse(addr, out int addrValue);
                    conn.Addr = addrValue;
                    conn.DeviceName = deviceName;
                    conn.Init(socket);
                    Console.WriteLine("客户端 [" + conn.GetAddress() + "] 连接");
                    Console.WriteLine("已连接" + (index + 1) + "个客户端");
                    conn.BeginReceive();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("设备连接失败");
                if (conns != null)
                {
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
        }

        /// <summary>
        /// 配置连接发送数据包的包大小
        /// </summary>
        /// <param name="ip">连接ip</param>
        /// <param name="size">包大小</param>
        public void SetPackageSize(string ip,int size)
        {
            foreach (Conn value in conns)
            {
                if (value.Ip.Equals(ip))
                {
                    value.SetPackageSize(size);
                }
            }
        }

        /// <summary>
        /// 获取所有设备的ip以及设备标识
        /// </summary>
        /// <param name="ip">连接ip</param>
        /// <param name="size">包大小</param>

        public Dictionary<string,string> GetDeviceInfos()
        {
            Dictionary<string, string> infos = new Dictionary<string, string>();
            foreach (Conn value in conns)
            {
                if (value != null || value.IsUse)
                {
                    if (value.Ip != null)
                    {
                        infos.Add(value.Ip, value.DeviceName);
                    }
                }
            }
            return infos;
        }

        /// <summary>
        /// 获取所有已连接设备ip
        /// </summary>
        /// <returns></returns>
        public IList<string> GetDeviceList()
        {
            IList<string> deviceList = new List<string>();
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
            return deviceList;
        }

        /// <summary>
        /// 获取所有终端设备的设备标识
        /// </summary>
        /// <returns></returns>
        public IList<string> GetDeviceNameList()
        {
            IList<string> deviceNameList = new List<string>();
            foreach (Conn value in conns)
            {
                if (value != null || value.IsUse)
                {
                    if (value.Ip != null)
                    {
                        deviceNameList.Add(value.DeviceName);
                    }
                }
            }
            return deviceNameList;
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

        /// <summary>
        /// 发送配置文件
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="filePath"></param>
        /// <param name="receiveCallBack"></param>
        public void PutPara(string ip,string filePath, IReceiveCallBack receiveCallBack)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] != null || conns[i].IsUse)
                {
                    if (conns[i].Ip.Equals(ip))
                    {
                        conns[i].PutPara(filePath, receiveCallBack);
                    }
                }
            }
        }
    }
}
