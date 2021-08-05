using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LBDConfigTool.utils.communication
{
    public class Communitor : FunctionModule
    {
        private static Communitor Instance;
        private Socket Server;
        private byte[] ServerRecBuff;
        private bool IsStart;

        private Communitor()
        {
            ServerRecBuff = new byte[1024 * 2];
            IsStart = false;
        }
        public static Communitor GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Communitor();
            }
            return Instance;
        }
        
        public bool Start()
        {
            try
            {
                if (!IsStart)
                {
                    InitParam();
                    Init();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("启动服务器成失败:" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
            return false;
        }

        private void InitParam()
        {
            EndPoint receiveEndPoint = new IPEndPoint(IPAddress.Any, 6264);
            Server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            Server.Bind(new IPEndPoint(IPAddress.Any, 4883));
            Server.BeginReceiveFrom(ServerRecBuff, 0, 1024 * 2, SocketFlags.None, ref receiveEndPoint, ReceiveCallback, Server);
        }

        protected override void Send(byte[] data)
        {
            if (Server != null)
            {
                Server.BeginSendTo(data, 0, data.Length, SocketFlags.None, new IPEndPoint(IPAddress.Broadcast, 6264), SendCallback, Server);
            }
        }
        private void SendCallback(IAsyncResult result)
        {
            this.SendCompleted();
        }
        private void ReceiveCallback(IAsyncResult result)
        {
            EndPoint iPEnd = new IPEndPoint(IPAddress.Any, 6264);
            Socket socket = (Socket)result.AsyncState;
            int count = socket.EndReceiveFrom(result, ref iPEnd);
            result.AsyncWaitHandle.Close();
            if ((iPEnd as IPEndPoint).Port == 6264)
            {
                byte[] buff = new byte[count];
                Array.Copy(this.ServerRecBuff, 0, buff, 0, count);
                this.RecMessageQueue.Enqueue(new List<byte>(buff));
            }
            this.ServerRecBuff = new byte[1024 * 2];
            socket.BeginReceiveFrom(this.ServerRecBuff, 0, 1024 * 2, SocketFlags.None, ref iPEnd, this.ReceiveCallback, socket);
        }
    }
}
