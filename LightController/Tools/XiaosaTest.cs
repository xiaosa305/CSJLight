using DMX512;
using LightController.Ast;
using LightController.Entity;
using LightController.PeripheralDevice;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using LightController.Utils.LightConfig;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
    public class XiaosaTest
    {
        private static XiaosaTest Instance { get; set; }
        private SerialConnect SerialConnect { get; set; }
        private OnlineConnect Connect { get; set; }
        private bool Status { get; set; }

        private XiaosaTest()
        {
            this.Status = false;
        }
        public static XiaosaTest GetInstance()
        {
            if (Instance == null)
            {
                Instance = new XiaosaTest();
            }
            return Instance;
        }

        public void Test()
        {
            Console.WriteLine("小撒的测试");
            this.SocketTest();
            //this.DMXTest();
        }
        private void OnlineTest()
        {
            this.Connect = new OnlineConnect("admin");
            if (this.Connect.Connect(null))
            {
                Thread.Sleep(1000);
                this.Connect.SetSessionId(Completed1, Error1);
            }
        }
        private void Completed1(Object obj, string msg)
        {
            Console.WriteLine("1: " + msg);
            this.Connect.GetOnlineDevices(Completed2, Error2);
        }
        private void Error1(string msg)
        {
            Console.WriteLine("1: " + msg);
        }
        private void Completed2(Object obj, string msg)
        {
            Console.WriteLine("2: " + msg);
            List<OnlineDeviceInfo> devices = this.Connect.DeviceInfos;
            this.Connect.BindDevice(devices[0], Completed3, Error3);
        }
        private void Error2(string msg)
        {
            Console.WriteLine("2: " + msg);
        }
        private void Completed3(Object obj, string msg)
        {
            Console.WriteLine("3: " + msg);
        }
        private void Error3(string msg)
        {
            Console.WriteLine("3: " + msg);
        }

        private void DMXTest()
        {
            if (this.Status ==false)
            {
                this.Status = true;
                Thread thread = new Thread(Task) { IsBackground = true };
                thread.Start();
            }
            else
            {
                this.Status = false;
            }
        }

        private void Task(Object obj)
        {
            SerialPort com = new SerialPort();
            com.BaudRate = 256000;
            com.StopBits = StopBits.Two;
            com.DataBits = 8;
            com.Parity = Parity.None;
            string[] names = SerialPort.GetPortNames();
            com.PortName = "COM8";
            byte[] dmx = Enumerable.Repeat(Convert.ToByte(0x00), 513).ToArray();
            dmx[1] = 0x64;
            dmx[2] = 0x80;
            dmx[5] = 0xFF;
            dmx[6] = 0xFF;
            dmx[7] = 0xFF;
            dmx[8] = 0xFF;
            Console.WriteLine("dmx 开启");
            com.Open();
            while (this.Status)
            {
                try
                {
                    com.BreakState = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                Thread.Sleep(0);
                try
                {
                    com.BreakState = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                Thread.Sleep(0);
                //com.DiscardOutBuffer();
                com.Write(dmx, 0, dmx.Length);
                try
                {
                    com.BreakState = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                Thread.Sleep(30);
            }
            com.Close();
            Console.WriteLine("dmx 关闭");
        }

        private Socket Server { get; set; }
        private byte[] ReceiveBuff { get; set; }//接收缓存区
        private int BuffCount { get; set; }
        private const int RECEIVEBUFFSIZE = 2048;
        public Dictionary<string, Dictionary<string, NetworkDeviceInfo>> Devices { get; set; }

        /// <summary>
        /// 获取缓存区大小
        /// </summary>
        /// <returns></returns>
        private int BuffRemain()
        {
            return RECEIVEBUFFSIZE - this.BuffCount;
        }

        public void SocketTest()
        {
            if (Server == null)
            {
                ReceiveBuff = new byte[RECEIVEBUFFSIZE];
                this.BuffCount = 0;
                Server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                Server.Bind(new IPEndPoint(IPAddress.Parse("192.168.31.147"), 7070));
                Server.BeginReceive(ReceiveBuff, this.BuffCount, this.BuffRemain(), SocketFlags.None, this.NetworkReceive, this);
                Devices = new Dictionary<string, Dictionary<string, NetworkDeviceInfo>>();
                Devices.Add("192.168.31.147", new Dictionary<string, NetworkDeviceInfo>());
            }
            Devices = new Dictionary<string, Dictionary<string, NetworkDeviceInfo>>();
            Devices.Add("192.168.31.147", new Dictionary<string, NetworkDeviceInfo>());
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
        }

        private void SendCallBack(IAsyncResult async)
        {
            CommandLogUtils.GetInstance().Enqueue("搜索包发送完成");
        }

        private void NetworkReceive(IAsyncResult asyncResult)
        {
            try
            {
                XiaosaTest connect = asyncResult.AsyncState as XiaosaTest;
                int count = connect.Server.EndReceive(asyncResult);
                if (count <= 0)
                {
                    CommandLogUtils.GetInstance().Enqueue("设备断开");
                    return;
                }
                else
                {
                    byte[] buff = new byte[count];
                    Array.Copy(connect.ReceiveBuff,8, buff,0, count);
                    string str = Encoding.Default.GetString(buff);
                    Console.WriteLine("收到数据包：" + str);
                    //CommandLogUtils.GetInstance().Enqueue(str);
                    string strBuff = Encoding.Default.GetString(buff);
                    string[] strarrau = strBuff.Split(' ');
                    NetworkDeviceInfo info = new NetworkDeviceInfo();
                    info.DeviceIp = strBuff.Split(' ')[0];
                    int.TryParse(strBuff.Split(' ')[1], out int addr);
                    info.DeviceAddr = addr;
                    info.LocalIp = "192.168.31.147";
                    info.DeviceName = strBuff.Split(' ')[2];
                    Console.WriteLine(Encoding.Default.GetString(buff));
                    if (!Devices["192.168.31.147"].ContainsKey(info.DeviceIp))
                    {
                        Devices["192.168.31.147"].Add(info.DeviceIp, info);
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
