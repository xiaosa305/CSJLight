using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using LBDConfigTool.utils.conf;

namespace LBDConfigTool.utils.communication
{
    public class HCXNetCommunitor
    {
        private const int REC_BUFF_SIZE = 1024 * 2;
        private const int PORT = 4626;
        private static HCXNetCommunitor Instance { get; set; }
        private Socket UdpServer { get; set; }
        private byte[] RecBuff { get; set; }
        public delegate void Completed(Object obj);
        private Completed Completed_Event { get; set; }
        private HCXNetCommunitor()
        {
            ;
        }
        public static HCXNetCommunitor GetInstance()
        {
            if (Instance == null)
            {
                Instance = new HCXNetCommunitor();
            }
            return Instance;
        }

        protected void Send(byte[] data)
        {
            this.UdpServer.BeginSendTo(data, 0, data.Length, SocketFlags.None, new IPEndPoint(IPAddress.Broadcast,PORT), SendCallback, UdpServer);
        }

        public HCXNetCommunitor Start()
        {
            this.InitParam();
            this.InitUdpServer();
            return this;
        }

        public void Close()
        {
            ;
        }

        private void InitParam()
        {
            this.RecBuff = new byte[REC_BUFF_SIZE];
        }

        private void InitUdpServer()
        {
            try
            {
                EndPoint receiveEndPoint = new IPEndPoint(IPAddress.Any, PORT);
                this.UdpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.UdpServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                this.UdpServer.Bind(new IPEndPoint(IPAddress.Any, 4882));
                this.UdpServer.BeginReceiveFrom(this.RecBuff, 0, REC_BUFF_SIZE, SocketFlags.None,ref receiveEndPoint , this.ReceiveCallback, UdpServer);
                Console.WriteLine("启动HCX服务器成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine("启动HCX服务器成失败: " + ex.Message);
            }
          
        }

        private void SendCallback(IAsyncResult result)
        {
            Console.WriteLine("发送完成"); ;
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            EndPoint endPoint = new IPEndPoint(IPAddress.Any, PORT);
            Socket socket = (Socket)result.AsyncState;
            int count = socket.EndReceiveFrom(result,ref endPoint);
            result.AsyncWaitHandle.Close();
            if ((endPoint as IPEndPoint).Port == PORT)
            {
                byte[] buff = new byte[count];
                Array.Copy(this.RecBuff, 0, buff, 0, count);
                Console.WriteLine("收到消息：" + Encoding.ASCII.GetString(buff));
                if (buff[0] == 0x48 && buff[1] == 0x38 && buff[2] == 0x30 && buff[3] == 0x31 && buff[4] == 0x52 && buff[5] == 0x54)
                {
                    HCXConf conf = HCXConf.Build(buff);
                    if (conf != null)
                    {
                        Console.WriteLine("正确");
                        this.Completed_Event(conf);
                    }
                    else
                    {
                        Console.WriteLine("错误");
                    }
                }
            }
            this.RecBuff = new byte[REC_BUFF_SIZE];
            socket.BeginReceiveFrom(this.RecBuff, 0, REC_BUFF_SIZE, SocketFlags.None,ref endPoint, this.ReceiveCallback, socket);
        }

        public void SearchDevice(Completed completed)
        {
            try
            {
                this.Completed_Event = completed;
                byte[] data = new byte[] { 0x52, 0x65, 0x61, 0x64,0x50,0x61,0x72,0x61,0x6D,0x65,0x74,0x65,0x72,0x00,0x00,0x00,0x00,0x00 };
                this.Send(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        public void WriteParam(HCXConf conf)
        {
            try
            {
                this.Send(conf.GetData());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
