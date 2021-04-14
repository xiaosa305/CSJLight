using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LightController.PeripheralDevice
{
    public class SeachDeviceUtils
    {
        private static SeachDeviceUtils Instance { get; set; }
        private Socket Server { get; set; }
        private byte[] ReceiveBuff { get; set; }//接收缓存区
        private int BuffCount { get; set; }
        private const int RECEIVEBUFFSIZE = 2048;
        public Dictionary<string, Dictionary<string, NetworkDeviceInfo>> Devices { get; set; }
        private string LocalIP { get; set; }
        private SeachDeviceUtils()
        {
            this.Devices = new Dictionary<string, Dictionary<string, NetworkDeviceInfo>>();
        }
        public static SeachDeviceUtils GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SeachDeviceUtils();
            }
            return Instance;
        }
        private int BuffRemain()
        {
            return RECEIVEBUFFSIZE - this.BuffCount;
        }
        public void SearchDevice(string localIp)
        {
            try
            {
                if (Server != null)
                {
                    this.Server.Close();
                    this.Server = null;
                    Console.WriteLine("关闭");
                }
                this.LocalIP = localIp;
                this.ReceiveBuff = new byte[RECEIVEBUFFSIZE];
                this.BuffCount = 0;
                this.Server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                this.Server.Bind(new IPEndPoint(IPAddress.Parse(LocalIP), 7070));
                this.Server.BeginReceive(ReceiveBuff, this.BuffCount, this.BuffRemain(), SocketFlags.None, this.NetworkReceive, this);
                if (!this.Devices.ContainsKey(localIp))
                {
                    this.Devices.Add(LocalIP, new Dictionary<string, NetworkDeviceInfo>());
                }
                else
                {
                    this.Devices[localIp].Clear();
                }
                List<byte> packageBuff = new List<byte>();
                byte[] dataBuff = Encoding.Default.GetBytes(Constant.UDP_ORDER);
                byte[] dataLengthBuff = new byte[] { Convert.ToByte(dataBuff.Length & 0xFF), Convert.ToByte((dataBuff.Length >> 8) & 0xFF) };
                byte[] headBuff = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), dataLengthBuff[0], dataLengthBuff[1], Convert.ToByte("00000001", 2), Convert.ToByte(0x00), Convert.ToByte(0x00) };
                packageBuff.AddRange(headBuff);
                packageBuff.AddRange(dataBuff);
                byte[] CRC = CRCTools.GetInstance().GetCRC(packageBuff.ToArray());
                packageBuff[6] = CRC[0];
                packageBuff[7] = CRC[1];
                this.Server.BeginSendTo(packageBuff.ToArray(), 0, packageBuff.Count, SocketFlags.None, new IPEndPoint(IPAddress.Broadcast, 7060), this.SendCallBack, this);
                Console.WriteLine("LocalIP：" + this.LocalIP);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void SendCallBack(IAsyncResult async)
        {
            CommandLogUtils.GetInstance().Enqueue("搜索包发送完成");
        }

        private void NetworkReceive(IAsyncResult asyncResult)
        {
            try
            {
                SeachDeviceUtils connect = asyncResult.AsyncState as SeachDeviceUtils;
                int count = 0;
                try
                {
                    count = connect.Server.EndReceive(asyncResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                if (count <= 0)
                {
                    CommandLogUtils.GetInstance().Enqueue("设备断开");
                    return;
                }
                else
                {
                    byte[] buff = new byte[count - 8];
                    Array.Copy(connect.ReceiveBuff, 8, buff, 0, count - 8);
                    string str = Encoding.Default.GetString(buff);
                    Console.WriteLine("收到数据包：" + str);
                    string strBuff = Encoding.Default.GetString(buff);
                    string[] strarrau = strBuff.Split(' ');
                    NetworkDeviceInfo info = new NetworkDeviceInfo();
                    info.DeviceIp = strBuff.Split(' ')[0];
                    int.TryParse(strBuff.Split(' ')[1], out int addr);
                    info.DeviceAddr = addr;
                    info.LocalIp = LocalIP;
                    info.DeviceName = strBuff.Split(' ')[2];
                    Console.WriteLine(Encoding.Default.GetString(buff));
                    if (!Devices[LocalIP].ContainsKey(info.DeviceIp))
                    {
                        Devices[LocalIP].Add(info.DeviceIp, info);
                    }
                    Server.BeginReceive(connect.ReceiveBuff, connect.BuffCount, connect.BuffRemain(), SocketFlags.None, this.NetworkReceive, connect);
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "网络设备已断开或网络接收模块发生异常", ex);
            }
        }
    }
}
