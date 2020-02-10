using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiLedController.Entity
{
    public class Art_Net_Client
    {
        private const int PORT = 6454;
        private Socket UDP_Send { get; set; }
        private UdpClient UDP_Receive { get; set; }
        private string SeriveIp { get; set; }
        private string CurrentIp { get; set; }
        private int Field_1 { get; set; }
        private int Field_2 { get; set; }
        private int Field_3 { get; set; }
        private int Field_4 { get; set; }
        private Thread ReceiveThread { get; set; }
        private byte Test { get; set; }


        public Art_Net_Client(string seriveIp,string currentIp)
        {
            //配置服务IP
            this.SeriveIp = seriveIp;
            //配置本地IP
            this.CurrentIp = currentIp;
            //配置子空间编号
            this.Field_1 = 0;
            this.Field_2 = 1;
            this.Field_3 = 2;
            this.Field_4 = 3;
            //配置UDP发送器
            this.UDP_Send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.UDP_Send.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            //this.UDP_Send.Bind(new IPEndPoint(IPAddress.Parse(CurrentIp), PORT));
            //配置UDP接收器
            //this.UDP_Receive = new UdpClient(new IPEndPoint(IPAddress.Any, PORT));
            this.UDP_Receive = new UdpClient();
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            socket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            this.UDP_Receive.Client = socket;
            //配置线程接收UDP数据包
            this.ReceiveThread = new Thread(ReceiveMsg)
            {
                IsBackground = true
            };
            //启动接收线程
            this.ReceiveThread.Start(UDP_Receive);
            Console.WriteLine("控制器" + CurrentIp +  "配置完成并且启动成功");

            string addr = CurrentIp.Split('.')[3];
            Test = Convert.ToByte(Convert.ToInt16(addr));
            Console.WriteLine("Test value is:" + Test);
        }

        private void ReceiveMsg(Object obj)
        {
            UdpClient client = obj as UdpClient;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, PORT);
            byte[] receiveData = client.Receive(ref endPoint);
            Console.WriteLine("控制器-" + CurrentIp + ": 接收到数据" + receiveData.Length + "字节");
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in receiveData)
            {
                stringBuilder.Append("" + b + " ");
            }
            Console.WriteLine("控制器" + CurrentIp + "接收数据为：" + stringBuilder);
            Console.WriteLine("控制器" + CurrentIp + "接收数据完成");
            this.TestSend();
        }

        private void TestSend()
        {
            byte[] data = new byte[]
            {
                0x41,0x72,0x74,0x2D,0x4E,0x65,0x74,0x00
                ,0x00,0x021
                ,0xC0,0xA8,0x01,this.Test//ip地址
                ,0x36,0x19
                ,0x04
                ,0x00
                ,0x00
                ,0x00
                ,0xB1
                ,0x08
                ,0x00
                ,0x4B
                ,0x53
                ,0x43,0x59,0x4C,0x2D,0x32,0x32,0x30,0x34,0x38,0x20,0x20,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
                ,0x43,0x59,0x4C,0x2D,0x32,0x30,0x34,0x38,0x20,0x20,0x41,0x72,0x74,0x2D,0x4E,0x65,0x74,0x20,0x6E,0x6F,0x64,0x65,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
                ,0x23,0x30,0x30,0x30,0x31,0x20,0x5B,0x30,0x30,0x30,0x30,0x5D,0x20,0x53,0x74,0x6D,0x41,0x72,0x74,0x4E,0x6F,0x64,0x65,0x20,0x69,0x73,0x20,0x72,0x65,0x61,0x64,0x72,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
                ,0x00
                ,0x04
                ,0x80,0x80,0x80,0x80
                ,0x00,0x00,0x00,0x00
                ,0x00,0x01,0x02,0x03//4个端口
                ,0x00,0x01,0x02,0x03//5个端口
                ,0x00
                ,0x00
                ,0x00
                ,0x00,0x00,0x00
                ,0x00
                ,0xE0,0xD5,0x5E,0xA7,0x91,0x11//MAC
                ,0x00,0x00,0x00,0x00
                ,0x00
                ,0x00
                ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            };
            this.UDP_Send.SendTo(data, new IPEndPoint(IPAddress.Broadcast, PORT));
            Console.WriteLine("控制器" + CurrentIp + "发送成功，发送数据大小：" + data.Length.ToString());
        }
    }
}
