using LighEditor.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LighEditor.Tools
{
    public class SocketTools
    {
        private static SocketTools Instance { get; set; }//Socket实例
        private Conn[] conns;//客户端连接（异步）
        private int MaxCount { get; set; }//最大连接数

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
        public void AddConnect(byte[] receiveBuff, int port)
        {
            string ip = null;
            try
            {
                string strBuff = Encoding.Default.GetString(receiveBuff);
                string[] strarrau = strBuff.Split(' ');
                ip = strBuff.Split(' ')[0];
                string addr = strBuff.Split(' ')[1];
                string deviceName = strBuff.Split(' ')[2];
                if (strarrau.Length > 3)
                {
                    for (int i = 3; i < strarrau.Length; i++)
                    {
                        deviceName += " " + strarrau[i];
                    }
                }
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
                    CSJLogs.GetInstance().DebugLog("网络连接池已满");
                }
                else
                {
                    Conn conn = conns[index];
                    socket.Connect(iPEndPoint);
                    int.TryParse(addr, out int addrValue);
                    conn.SetAddr(addrValue);
                    conn.Init(socket);
                    conn.SetDeviceName(deviceName);
                    CSJLogs.GetInstance().DebugLog("客户端 [" + conn.GetAddress() + "] 连接");
                    conn.BeginReceive();
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
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
                                    conns[i].CloseDevice();
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
        public void SetPackageSize(string ip, int size)
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
        public Dictionary<string, string> GetDeviceInfos()
        {
            Dictionary<string, string> infos = new Dictionary<string, string>();
            foreach (Conn value in conns)
            {
                if (value != null && value.IsUse)
                {
                    if (value.Ip != "" && value.Ip != null && value.GetDeviceName() != null)
                    {
                        infos.Add(value.Ip, value.GetDeviceName());
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
                if (value != null && value.IsUse)
                {
                    if (value.Ip != "" && value.Ip != null && value.GetDeviceName() != null)
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
                if (value != null && value.IsUse)
                {
                    if (value.Ip != "" && value.Ip != null && value.GetDeviceName() != null)
                    {
                        deviceNameList.Add(value.GetDeviceName());
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
        public void Download(string ip, DBWrapper dBWrapper, string configPath, IReceiveCallBack callBack, DownloadProgressDelegate download)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] != null || conns[i].IsUse)
                {
                    if (conns[i].Ip.Equals(ip))
                    {
                        conns[i].DownloadProject(dBWrapper, configPath, callBack, download);
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
        public void SendOrder(string ip, string order, string[] array, IReceiveCallBack receiveCallBack)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] != null || conns[i].IsUse)
                {
                    if (conns[i].Ip.Equals(ip))
                    {
                        conns[i].SendOrder(order, array, receiveCallBack);
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
        public void PutParam(string ip, string filePath, IReceiveCallBack receiveCallBack)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] != null || conns[i].IsUse)
                {
                    if (conns[i].Ip.Equals(ip))
                    {
                        conns[i].PutParam(filePath, receiveCallBack);
                    }
                }
            }
        }

        public void GetParam(string ip, IReceiveCallBack receiveCallBack, GetParamDelegate getParam)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] != null || conns[i].IsUse)
                {
                    if (conns[i].Ip.Equals(ip))
                    {
                        conns[i].GetParam(getParam, receiveCallBack);
                    }
                }
            }
        }
    }
}
