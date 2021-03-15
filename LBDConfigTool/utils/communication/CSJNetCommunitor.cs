using LBDConfigTool.utils.conf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LBDConfigTool.utils.communication
{
    public class CSJNetCommunitor : BaseFunctionalModule
    {
        private const int REC_BUFF_SIZE = 1024 * 2;
        private const int PORT = 6264;
        private static CSJNetCommunitor Instance { get; set; }
        private Socket UdpServer { get; set; }
        private byte[] RecBuff { get; set; }
        private CSJNetCommunitor()
        {
           
        }
        public static CSJNetCommunitor GetInstance()
        {
            if (Instance == null)
            {
                Instance = new CSJNetCommunitor();
            }
            return Instance;
        }

        protected override void Send(byte[] data)
        {
            this.UdpServer.BeginSendTo(data, 0, data.Length, SocketFlags.None, new IPEndPoint(IPAddress.Broadcast,PORT), SendCallback, UdpServer);
        }

        public CSJNetCommunitor Start()
        {
            this.InitParam();
            this.InitUdpServer();
            this.Init();
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
                EndPoint receiveEndPoint = new IPEndPoint(IPAddress.Any,PORT) ;
                this.UdpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.UdpServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                this.UdpServer.Bind(new IPEndPoint(IPAddress.Any, 4883));
                this.UdpServer.BeginReceiveFrom(this.RecBuff, 0, REC_BUFF_SIZE, SocketFlags.None,ref receiveEndPoint, this.ReceiveCallback, UdpServer);
                Console.WriteLine("启动服务器成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine("启动服务器成失败: " + ex.Message);
            }
          
        }

        private void SendCallback(IAsyncResult result)
        {
            this.SendCompleted();
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            EndPoint iPEnd = new IPEndPoint(IPAddress.Any, PORT);
            Socket socket = (Socket)result.AsyncState;
            int count = socket.EndReceiveFrom(result, ref iPEnd);
            result.AsyncWaitHandle.Close();
            if ((iPEnd as IPEndPoint).Port == PORT)
            {
                byte[] buff = new byte[count];
                Array.Copy(this.RecBuff, 0, buff, 0, count);
                Console.WriteLine("收到消息：" + Encoding.ASCII.GetString(buff));
                this.MessageQueue.Enqueue(new List<byte>(buff));
            }
            this.RecBuff = new byte[REC_BUFF_SIZE];
            socket.BeginReceiveFrom(this.RecBuff, 0, REC_BUFF_SIZE, SocketFlags.None, ref iPEnd, this.ReceiveCallback, socket);
        }
    }
}
