using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MultiLedController.utils.lbdconfigtor
{
    public class NetCommunitor : BaseFunctionalModule
    {
        private const int REC_BUFF_SIZE = 1024 * 2;
        private const int PORT = 7070;
        private static NetCommunitor Instance { get; set; }
        private Socket UdpServer { get; set; }
        private byte[] RecBuff { get; set; }
        private NetCommunitor()
        {
           
        }
        public static NetCommunitor GetInstance()
        {
            if (Instance == null)
            {
                Instance = new NetCommunitor();
            }
            return Instance;
        }

        protected override void Send(byte[] data)
        {
            this.UdpServer.BeginSendTo(data, 0, data.Length, SocketFlags.None, new IPEndPoint(IPAddress.Broadcast,PORT), SendCallback, UdpServer);
        }

        public NetCommunitor Start()
        {
            this.InitParam();
            this.InitUdpServer();
            return this;
        }

        public void Close()
        {

        }

        private void InitParam()
        {
            this.RecBuff = new byte[REC_BUFF_SIZE];
        }

        private void InitUdpServer()
        {
            try
            {
                this.UdpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.UdpServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                this.UdpServer.Bind(new IPEndPoint(IPAddress.Parse("192.168.50.43"), 7070));
                this.UdpServer.BeginReceive(this.RecBuff, 0, REC_BUFF_SIZE, SocketFlags.None, this.ReceiveCallback, UdpServer);
                Console.WriteLine("启动服务器成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine("启动服务器成失败: " + ex.Message);
            }
          
        }

        private void SendCallback(IAsyncResult result)
        {
            base.SendCompleted();
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            Socket socket = (Socket)result.AsyncState;
            int count = socket.EndReceive(result);
            result.AsyncWaitHandle.Close();
            byte[] buff = new byte[count];
            Array.Copy(this.RecBuff, 0, buff, 0, count);
            Console.WriteLine("收到消息：" + Encoding.ASCII.GetString(buff));
            this.RecBuff = new byte[REC_BUFF_SIZE];
            socket.BeginReceive(this.RecBuff, 0, REC_BUFF_SIZE, SocketFlags.None, this.ReceiveCallback, socket);
        }
    }
}
