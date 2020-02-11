using MultiLedController.Utils;
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
            //将UDP接收器配置为复用端口
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
            while (true)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, PORT);
                byte[] receiveData = client.Receive(ref endPoint);
                Console.WriteLine("控制器-" + CurrentIp + ": 接收到数据" + receiveData.Length + "字节");
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in receiveData)
                {
                    stringBuilder.Append("" + b + " ");
                }
                
                if (receiveData[8] == 0x00 && receiveData[9] == 0x21)//接收到其他设备发送ArtPollReply包
                {
                    continue;
                }
                else if (receiveData[8] == 0x00 && receiveData[9] == 0x20)//接收到ArtPoll包
                {
                    Console.WriteLine("控制器" + CurrentIp + "接收数据为：" + stringBuilder);
                    Console.WriteLine("控制器" + CurrentIp + "接收数据完成");
                    this.SearchDevice_Receive();
                }
                else if (receiveData[8] == 0x00 && receiveData[9] == 0x60)//接收到ArtAddress分组。发送DMX调试数据前会发送
                {
                    Console.WriteLine("控制器" + CurrentIp + "接收数据为：" + stringBuilder);
                    Console.WriteLine("控制器" + CurrentIp + "接收数据完成");
                }
                else if (receiveData[8] == 0x00 && receiveData[9] == 0x50)//这是ArtDMX数据包
                {
                    Console.WriteLine("控制器" + CurrentIp + "接收数据为：" + stringBuilder);
                    Console.WriteLine("控制器" + CurrentIp + "接收数据完成");
                }

            }
        }

        private void SearchDevice_Receive()
        {
            byte[] data = Constant.GetReceiveDataBySerchDeviceOrder();
            data[13] = this.Test;
            
            this.UDP_Send.SendTo(data, new IPEndPoint(IPAddress.Broadcast, PORT));
            Console.WriteLine("控制器" + CurrentIp + "发送成功，发送数据大小：" + data.Length.ToString());
        }
    }
}
