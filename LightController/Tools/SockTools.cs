using LightController.Ast;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
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
        private SocketTools()
        {
            MaxCount = 100;
        }
        public static SocketTools GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SocketTools();
            }
            return Instance;
        }
        public void InitCoons()
        {
            //conns = new Conn[MaxCount];
            for (int i = 0; i < MaxCount; i++)
            {
                conns[i].CloseDevice();
            }
        }
        public void Start()
        {
            if (conns == null)
            {
                conns = new Conn[MaxCount];
                for (int i = 0; i < MaxCount; i++)
                {
                    conns[i] = new Conn();
                }
            }
        }
        public void AddConnect(NetworkDeviceInfo info,int port)
        {
            for (int i = 0; i < MaxCount; i++)
            {
                if (conns[i] != null || conns[i].IsUse)
                {
                    if (conns[i].Ip != null)
                    {
                        if (conns[i].Ip.Equals(info.DeviceIp))
                        {
                            return;
                        }
                    }
                }
            }
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(info.DeviceIp), port);
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
                conn.SetAddr(info.DeviceAddr);
                conn.Init(socket);
                conn.SetDeviceName(info.DeviceName);
                CSJLogs.GetInstance().DebugLog("客户端 [" + conn.Ip + "] 连接");
                conn.BeginReceive();
            }
        }
        protected void AddConnect(byte[] receiveBuff, int port)
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
                    CSJLogs.GetInstance().DebugLog("客户端 [" + conn.Ip + "] 连接");
                    conn.BeginReceive();
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
               
            }
        }
        public void CloseAll()
        {
            try
            {
                if (conns != null)
                {
                    for (int i = 0; i < MaxCount; i++)
                    {
                        if (conns[i] != null || conns[i].IsUse)
                        {
                            if (conns[i].Ip != null)
                            {
                                conns[i].CloseDevice();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
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
        public void Download(string ip, DBWrapper dBWrapper, string configPath, ICommunicatorCallBack callBack)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] != null || conns[i].IsUse)
                {
                    if (conns[i].Ip.Equals(ip))
                    {
                        conns[i].DownloadProject(dBWrapper, configPath, callBack);
                    }
                }
            }
        }
        public void SendOrder(string ip, string order, string[] array, ICommunicatorCallBack receiveCallBack)
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
        public void PutParam(string ip, string filePath, ICommunicatorCallBack receiveCallBack)
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
        public void GetParam(string ip, ICommunicatorCallBack receiveCallBack)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] != null || conns[i].IsUse)
                {
                    if (conns[i].Ip.Equals(ip))
                    {
                        conns[i].GetParam(receiveCallBack);
                    }
                }
            }
        }
        public void Update(string ip , string filePath, ICommunicatorCallBack receiveCallBack)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] != null || conns[i].IsUse)
                {
                    if (conns[i].Ip.Equals(ip))
                    {
                        conns[i].Update(filePath, receiveCallBack);
                    }
                }
            }
        }
        public void StartDebug(string ip,int timeFactory, ICommunicatorCallBack receiveCallBack)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] != null || conns[i].IsUse)
                {
                    if (conns[i].Ip.Equals(ip))
                    {
                        conns[i].StartIntenetPreview(timeFactory,receiveCallBack);
                    }
                }
            }
        }
        public void EndDebug(string ip, ICommunicatorCallBack receiveCallBack)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] != null || conns[i].IsUse)
                {
                    if (conns[i].Ip.Equals(ip))
                    {
                        conns[i].StopIntenetPreview(receiveCallBack);
                    }
                }
            }
        }
    }
}
