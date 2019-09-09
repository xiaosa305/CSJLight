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
        private readonly int UDP_SERVER_PORT = 7070;
        private readonly int UDP_CLIENT_PORT = 7060;
        private static ConnectTools Instance { get; set; }//连接工具实例
        private Socket UdpServer { get; set; }//Udp服务套接字
        private string ServerIp { get; set; }//服务器ip
        private bool IsStart { get; set; }
        private Thread SendThread { get; set; }
        private string Ip { get; set; }
        private UdpClient UdpClient { get; set; }
        private Thread receiveThread { get; set; }

        private ConnectTools()
        {
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
                    receiveThread.Abort();
                    Thread.Sleep(100);
                    receiveThread = null;
                }
                ServerIp = ip;
                UdpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                UdpClient = new UdpClient(new IPEndPoint(IPAddress.Any, UDP_SERVER_PORT));
                UdpServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                receiveThread = new Thread(RecevieMsg)
                {
                    IsBackground = true
                };
                SocketTools.GetInstance().Start();
                receiveThread.Start(UdpClient);
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
                Console.WriteLine("Start SerchDevice");
                List<byte> buff = new List<byte>();
                byte[] buffData = Encoding.Default.GetBytes(Constant.UDP_ORDER);
                byte[] buffDataLength = new byte[] {Convert.ToByte(buffData.Length & 0xFF),Convert.ToByte((buffData.Length >> 8) & 0xFF) };
                byte[] buffHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), buffDataLength[0], buffDataLength[1], Convert.ToByte("00000001",2), Convert.ToByte(0x00), Convert.ToByte(0x00) };
                buff.AddRange(buffHead);
                buff.AddRange(buffData);
                byte[] CRC = CRCTools.GetInstance().GetCRC(buff.ToArray());
                buff[6] = CRC[0];
                buff[7] = CRC[1];
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
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                while (true)
                {
                    IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 7070);
                    byte[] data = udpClient.Receive(ref iPEndPoint);
                    byte[] buff = new byte[data.Length - 8];
                    Array.Copy(data, 8, buff, 0, buff.Length);
                    SocketTools.GetInstance().AddConnect(buff, 7060);
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
           
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
        public IList<string> GetDevicesIp()
        {
            if (IsStart)
            {
                return SocketTools.GetInstance().GetDeviceList();
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public IList<string> GetDeviceNames()
        {
            if (IsStart)
            {
                return SocketTools.GetInstance().GetDeviceNameList();
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public Dictionary<string,string> GetDeviceInfo()
        {
            if (IsStart)
            {
                return SocketTools.GetInstance().GetDeviceInfos();
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void Download(IList<string> ips, DBWrapper dBWrapper, string configPath, IReceiveCallBack callBack, DownloadProgressDelegate download)
        {
            if (IsStart)
            {
                foreach (string ip in ips)
                {
                    SocketTools.GetInstance().Download(ip, dBWrapper, configPath, callBack, download);
                }
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void Download(string ip, DBWrapper dBWrapper, string configPath, IReceiveCallBack callBack, DownloadProgressDelegate download)
        {
            if (IsStart)
            {
                SocketTools.GetInstance().Download(ip, dBWrapper, configPath, callBack, download);
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void SendOrder(IList<string> ips, string order,string[] strarray,IReceiveCallBack callBack)
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
        public void SendOrder(string ip, string order, string[] strarray, IReceiveCallBack callBack)
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
        public void PutPara(IList<string> ips, string filePath,IReceiveCallBack receiveCallBack)
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
        public void PutPara(string ip, string filePath, IReceiveCallBack receiveCallBack)
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
        public void GetParam(IList<string> ips,IReceiveCallBack receiveCallBack,GetParamDelegate getParam)
        {
            if (IsStart)
            {
                foreach (string ip in ips)
                {
                    SocketTools.GetInstance().GetParam(ip, receiveCallBack, getParam);
                }
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void GetParam(string ip, IReceiveCallBack receiveCallBack, GetParamDelegate getParam)
        {
            if (IsStart)
            {
                SocketTools.GetInstance().GetParam(ip, receiveCallBack, getParam);
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void Update(string ip, string filePath,IReceiveCallBack receiveCallBack, DownloadProgressDelegate download)
        {
            if (IsStart)
            {
                SocketTools.GetInstance().Update(ip, filePath, receiveCallBack,download);
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
        public void Update(IList<string> ips, string filePath, IReceiveCallBack receiveCallBack, DownloadProgressDelegate download)
        {
            if (IsStart)
            {
                foreach (string ip in ips)
                {
                    SocketTools.GetInstance().Update(ip, filePath, receiveCallBack,download);
                }
            }
            else
            {
                CSJLogs.GetInstance().DebugLog("未启动服务");
                throw new Exception("未启动服务");
            }
        }
    }

 
}
