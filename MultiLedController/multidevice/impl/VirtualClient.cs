using MultiLedController.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiLedController.multidevice.impl
{
    public class VirtualClient
    {
        private Socket UdpSend { get; set; }
        private UdpClient UdpClient { get; set; }
        private Thread UdpReceiveThread { get; set; }
        private ControlDevice ControlDevice { get; set; }
        private string ServerIp { get; set; }
        private string VirtualClientIp { get; set; }
        private int StartLedSpace { get; set; }

        public VirtualClient(int startLedSpace,ControlDevice device,string virtualClientIp,string serverIp)
        {
            this.ControlDevice = device;
            this.ServerIp = serverIp;
            this.VirtualClientIp = virtualClientIp;
            this.StartLedSpace = startLedSpace;
            this.InitParameter();
        }

        private void InitParameter()
        {
            this.UdpSend = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }
    }
}
