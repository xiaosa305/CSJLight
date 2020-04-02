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
    public class ConnectTools
    {
        private readonly int UDP_SERVER_PORT = 7070;
        private readonly int UDP_CLIENT_PORT = 7060;
        private readonly int UDP_DEBUG_PORT = 7080;
        private static ConnectTools Instance { get; set; }
        private Socket UdpServer { get; set; }
        private string ServerIp { get; set; }
        private bool IsStart { get; set; }
        private Thread SendThread { get; set; }
        private string Ip { get; set; }
        private UdpClient UdpClient { get; set; }
        private Thread ReceiveThread { get; set; }
        public Dictionary<string,Dictionary<string,NetworkDeviceInfo>> DeviceInfos = new Dictionary<string, Dictionary<string, NetworkDeviceInfo>>();
        private ConnectTools()
        {
            DeviceInfos = new Dictionary<string, Dictionary<string, NetworkDeviceInfo>>();
            SocketTools.GetInstance().Start();
        }
        public static ConnectTools GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ConnectTools();
            }
            return Instance;
        }
        public void Start(string ip)
        {
            try
            {
                if (IsStart)
                {
                    IsStart = false;
                    UdpServer.Close();
                    UdpClient.Close();
                    ReceiveThread.Abort();
                    Thread.Sleep(100);
                    ReceiveThread = null;
                }
                ServerIp = ip;
                UdpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                UdpClient = new UdpClient(new IPEndPoint(IPAddress.Any, UDP_SERVER_PORT));
                UdpServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                ReceiveThread = new Thread(RecevieMsg)
                {
                    IsBackground = true
                };
                ReceiveThread.Start(UdpClient);
                IsStart = true;
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
            
        }
        public void SearchDevice()
        {
            if (IsStart)
            {
                if (DeviceInfos.ContainsKey(ServerIp))
                {
                    DeviceInfos[ServerIp].Clear();
                }
                else
                {
                    DeviceInfos.Add(ServerIp, new Dictionary<string, NetworkDeviceInfo>());
                }
                this.Disconnected();
                List<byte> buff = new List<byte>();
                byte[] buffData = Encoding.Default.GetBytes(Constant.UDP_ORDER);
                byte[] buffDataLength = new byte[] {Convert.ToByte(buffData.Length & 0xFF),Convert.ToByte((buffData.Length >> 8) & 0xFF) };
                byte[] buffHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), buffDataLength[0], buffDataLength[1], Convert.ToByte("00000001",2), Convert.ToByte(0x00), Convert.ToByte(0x00) };
                buff.AddRange(buffHead);
                buff.AddRange(buffData);
                byte[] CRC = CRCTools.GetInstance().GetCRC(buff.ToArray());
                buff[6] = CRC[0];
                buff[7] = CRC[1];
                UdpServer.Bind(new IPEndPoint(IPAddress.Parse(ServerIp), 8080));
                UdpServer.SendTo(buff.ToArray(), new IPEndPoint(IPAddress.Broadcast, UDP_CLIENT_PORT));
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
            
        }
        private void RecevieMsg(object obj)
        {
            try
            {
                UdpClient udpClient = obj as UdpClient;
                while (true)
                {
                    IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 7070);
                    NetworkDeviceInfo info = new NetworkDeviceInfo();
                    byte[] data = udpClient.Receive(ref iPEndPoint);
                    byte[] buff = new byte[data.Length - 8];
                    Array.Copy(data, 8, buff, 0, buff.Length);
                    string strBuff = Encoding.Default.GetString(buff);
                    string[] strarrau = strBuff.Split(' ');
                    info.DeviceIp = strBuff.Split(' ')[0];
                    int.TryParse(strBuff.Split(' ')[1], out int addr);
                    info.DeviceAddr = addr;
                    info.DeviceName = strBuff.Split(' ')[2];
                    if (!DeviceInfos[ServerIp].ContainsKey(info.DeviceIp))
                    {
                        DeviceInfos[ServerIp].Add(info.DeviceIp, info);
                    }
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
           
        }
        public void Disconnected()
        {
            SocketTools.GetInstance().CloseAll();
        }
        public bool Connect(NetworkDeviceInfo info)
        {
            return SocketTools.GetInstance().AddConnect(info, UDP_CLIENT_PORT);
        }
        public void SetConnectPackageSize(string ip,int size)
        {
            if (IsStart)
            {
                SocketTools.GetInstance().SetPackageSize(ip, size);
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public Dictionary<string,Dictionary<string,NetworkDeviceInfo>> GetDeivceInfos()
        {
            Thread.Sleep(500);
            return DeviceInfos;
        }
        public void Download(IList<string> ips, DBWrapper dBWrapper, string configPath, ICommunicatorCallBack callBack)
        {
            if (IsStart)
            {
                foreach (string ip in ips)
                {
                    SocketTools.GetInstance().Download(ip, dBWrapper, configPath, callBack);
                }
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void Download(string ip, DBWrapper dBWrapper, string configPath, ICommunicatorCallBack callBack)
        {
            if (IsStart)
            {
                SocketTools.GetInstance().Download(ip, dBWrapper, configPath, callBack);
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void SendOrder(IList<string> ips, string order,string[] strarray, ICommunicatorCallBack callBack)
        {
            if (IsStart)
            {
                foreach (string ip in ips)
                {
                    SocketTools.GetInstance().SendOrder(ip, order, strarray, callBack);
                }
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void SendOrder(string ip, string order, string[] strarray, ICommunicatorCallBack callBack)
        {
            if (IsStart)
            {
                SocketTools.GetInstance().SendOrder(ip, order, strarray, callBack);
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void PutPara(IList<string> ips, string filePath, ICommunicatorCallBack receiveCallBack)
        {
            if (IsStart)
            {
                foreach (string ip in ips)
                {
                    SocketTools.GetInstance().PutParam(ip, filePath, receiveCallBack);
                }
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void PutPara(string ip, string filePath, ICommunicatorCallBack receiveCallBack)
        {
            if (IsStart)
            {
                SocketTools.GetInstance().PutParam(ip, filePath, receiveCallBack);
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void GetParam(IList<string> ips, ICommunicatorCallBack receiveCallBack)
        {
            if (IsStart)
            {
                foreach (string ip in ips)
                {
                    SocketTools.GetInstance().GetParam(ip, receiveCallBack);
                }
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void GetParam(string ip, ICommunicatorCallBack receiveCallBack)
        {
            if (IsStart)
            {
                SocketTools.GetInstance().GetParam(ip, receiveCallBack);
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void Update(string ip, string filePath, ICommunicatorCallBack receiveCallBack)
        {
            if (IsStart)
            {
                SocketTools.GetInstance().Update(ip, filePath, receiveCallBack);
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void Update(IList<string> ips, string filePath, ICommunicatorCallBack receiveCallBack)
        {
            if (IsStart)
            {
                foreach (string ip in ips)
                {
                    SocketTools.GetInstance().Update(ip, filePath, receiveCallBack);
                }
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void StartIntentPreview(String ip,int timeFactory, ICommunicatorCallBack receiveCallBack)
        {
            SocketTools.GetInstance().StartDebug(ip, timeFactory, receiveCallBack);
        }
        public void StopIntentPreview(String ip, ICommunicatorCallBack receiveCallBack)
        {
            SocketTools.GetInstance().EndDebug(ip,receiveCallBack);
        }
        public void SendIntenetPreview(String ip,byte[] data)
        {
            UdpServer.SendTo(data, new IPEndPoint(IPAddress.Parse(ip), UDP_DEBUG_PORT));
        }
    }
}
