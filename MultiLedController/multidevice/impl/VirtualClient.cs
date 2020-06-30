using MultiLedController.entity;
using MultiLedController.utils;
using MultiLedController.utils.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiLedController.multidevice.impl
{
    public class VirtualClient
    {
        private const int PORT = 6454;
        private Socket UdpSend { get; set; }
        private UdpClient UdpClient { get; set; }
        private Thread UdpReceiveThread { get; set; }
        private ControlDevice ControlDevice { get; set; }
        private string ServerIp { get; set; }
        private string VirtualClientIp { get; set; }
        private int StartLedSpace { get; set; }
        private bool UdpReceiveStatus { get; set; }
        private int[] VirtualClientLedSpaceNumbers { get; set; }
        public delegate void DmxDataResponse(int ledSpaceNumber,List<byte> data);
        private DmxDataResponse DmxDataResponse_Event { get; set; }

        public VirtualClient(int startLedSpace,ControlDevice device,string virtualClientIp,string serverIp,DmxDataResponse response)
        {
            this.DmxDataResponse_Event = response;
            this.ControlDevice = device;
            this.ServerIp = serverIp;
            this.VirtualClientIp = virtualClientIp;
            this.StartLedSpace = startLedSpace;
            this.InitParameter();
            this.InitServers();
        }

        private void InitParameter()
        {
            this.VirtualClientLedSpaceNumbers = new int[4];
            int startLedSpace = this.StartLedSpace;
            for (int index = 0; index < this.ControlDevice.Led_space; index++)
            {
                this.VirtualClientLedSpaceNumbers[index] = startLedSpace;
                startLedSpace++;
            }
        }

        public void CloseVirtualClient()
        {
            if (this.UdpSend != null)
            {
                this.UdpReceiveStatus = false;
                this.UdpSend.Close();
                if (this.UdpClient != null)
                {
                    this.UdpClient.Close();
                    this.UdpClient = null;
                }
                this.UdpSend.Dispose();
                this.UdpSend = null;
            }
            this.DmxDataResponse_Event = null;
        }

        private void InitServers()
        {
            try
            {
                this.UdpSend = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.UdpSend.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                this.UdpSend.Bind(new IPEndPoint(IPAddress.Parse(VirtualClientIp), PORT));
                this.UdpClient = new UdpClient() { Client = UdpSend };
                this.UdpReceiveThread = new Thread(UdpReceiveMsg) { IsBackground = true };
                this.UdpReceiveStatus = true;
                this.UdpReceiveThread.Start(this.UdpClient);
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, this.ControlDevice.IP + "启动虚拟客户" + VirtualClientIp + "端失败", ex);
            }
           
        }

        private void UdpReceiveMsg(Object obj)
        {
            try
            {
                UdpClient udpClient = obj as UdpClient;
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(this.ServerIp), PORT);
                while (this.UdpReceiveStatus)
                {
                    byte[] receiveDataBuff = udpClient.Receive(ref iPEndPoint);
                    if (receiveDataBuff.Length > 18 &&  receiveDataBuff[8] == 0x00 && receiveDataBuff[9] == 0x50)//这是ArtDMX数据包
                    {
                        //int physicalPortIndex = Convert.ToInt16(receiveDataBuff[13]);
                        int universe = (int)(receiveDataBuff[14] & 0xFF) | ((receiveDataBuff[15] & 0xFF) << 8);//实际空间编号
                        int dataLength = (int)(receiveDataBuff[17] & 0xFF) | ((receiveDataBuff[16] & 0xFF) << 8);
                        byte[] dmxData = new byte[dataLength];
                        Array.Copy(receiveDataBuff, 18, dmxData, 0, dataLength);
                        List<byte> data = new List<byte>();
                        data.AddRange(dmxData);
                        this.DmxDataResponse_Event(universe, new List<byte>(data));
                    }
                    else if (receiveDataBuff.Length > 10 &&receiveDataBuff[8] == 0x00 && receiveDataBuff[9] == 0x20)//接收到ArtPoll包
                    {
                        this.ResponseForSearchDevice();
                    }
                    //else if (receiveDataBuff[8] == 0x00 && receiveDataBuff[9] == 0x21)//接收到其他设备发送ArtPollReply包
                    //{
                    //    continue;
                    //}

                    //else if (receiveDataBuff[8] == 0x00 && receiveDataBuff[9] == 0x60)//接收到ArtAddress分组。发送DMX调试数据前会发送
                    //{
                    //}
                    //Thread.Sleep(0);
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "关闭虚拟客户端：" + this.VirtualClientIp,ex);
            }
        }

        /// <summary>
        /// 功能：终端搜索回复虚拟控制器信息
        /// </summary>
        private void ResponseForSearchDevice()
        {
            byte[] souce = Constant.GetReceiveDataBySerchDeviceOrder();
            byte[] data = new byte[souce.Length];
            Array.Copy(souce, data, souce.Length);
            //修改IP地址
            string[] iPAddress = this.VirtualClientIp.Split('.');
            data[10] = Convert.ToByte(Convert.ToInt16(iPAddress[0]));
            data[11] = Convert.ToByte(Convert.ToInt16(iPAddress[1]));
            data[12] = Convert.ToByte(Convert.ToInt16(iPAddress[2]));
            data[13] = Convert.ToByte(Convert.ToInt16(iPAddress[3]));
            //修改空间编号
            data[190] = data[186] = Convert.ToByte(this.VirtualClientLedSpaceNumbers[0]);
            data[191] = data[187] = Convert.ToByte(this.VirtualClientLedSpaceNumbers[1]);
            data[192] = data[188] = Convert.ToByte(this.VirtualClientLedSpaceNumbers[2]);
            data[193] = data[189] = Convert.ToByte(this.VirtualClientLedSpaceNumbers[3]);
            //修改MAC地址
            string[] macAddress = GetMacUtils.GetMac().Split(':');
            data[201] = Convert.ToByte(macAddress[0],16);
            data[202] = Convert.ToByte(macAddress[1],16);
            data[203] = Convert.ToByte(macAddress[2],16);
            data[204] = Convert.ToByte(macAddress[3],16);
            data[205] = Convert.ToByte(macAddress[4],16);
            data[206] = Convert.ToByte(macAddress[5],16);
            this.UdpSend.SendTo(data, new IPEndPoint(IPAddress.Broadcast, PORT));
        }
    }
}
