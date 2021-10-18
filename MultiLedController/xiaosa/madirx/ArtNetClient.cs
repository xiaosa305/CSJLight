using MultiLedController.utils;
using MultiLedController.xiaosa.entity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiLedController.xiaosa.madirx
{
    public class ArtNetClient
    {
        protected const int ARTNETPORT = 6454;
        private const string BRODCASTADDRESS = ".255";
        protected  IPEndPoint BRODCAST = new IPEndPoint(IPAddress.Broadcast, ARTNETPORT);
        private int SIO_RCVALL = -2147483648 | 0x18000000 | 1;
        byte[] buffer;
        protected Socket Client;
        protected Dictionary<string, int> SpaceInfo;
        protected DMXManager Manager;
        protected DMXDataSync Sync;
        protected int IPCount;
        protected string LocalIP;
        protected List<string> IPS;
        protected int PortCount;
        protected UdpClient UdpClient;
        protected ConcurrentQueue<Packet> Packets;
        protected Thread PacketManager;

        public delegate void DMXManager(int ipIndex,int port, List<byte> dmxData);
        public delegate void DMXDataSync();

        protected ArtNetClient()
        {
            ;
        }

        protected void Init()
        {
            Packets = new ConcurrentQueue<Packet>();
            PacketManager = new Thread(new ThreadStart(PacketsManage)) { IsBackground = true };
            buffer = new byte[1024 * 1024];
            IPAddress iP = IPAddress.Parse(LocalIP);
            Client = new Socket(iP.AddressFamily, SocketType.Raw, ProtocolType.IP);
            Client.Bind(new IPEndPoint(iP, 0));
            Client.IOControl(SIO_RCVALL, BitConverter.GetBytes((int)1), null);
            Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(this.OnReceive), null);
            SpaceInfo = new Dictionary<string, int>();
            UdpClient = new UdpClient();
            UdpClient.Client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            PacketManager.Start();
        }

        public static ArtNetClient Build(List<string> ips,int portCount,string localIP,DMXManager manager,DMXDataSync sync)
        {
            ArtNetClient client = new ArtNetClient
            {
                PortCount = portCount,
                IPS = ips,
                IPCount = (portCount / 256) + (portCount % 256 == 0 ? 0 : 1),
                LocalIP = localIP,
                Manager = manager,
                Sync = sync
            };
            client.Init();
            for (int ipIndex  = 0; ipIndex < client.IPCount; ipIndex ++)
            {
                client.SpaceInfo.Add(ips[ipIndex], ipIndex);
            }
            return client;
        }

        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        /// <param name="ar"></param>
        private void OnReceive(IAsyncResult ar)
        {
            if (Client != null)
            {
                int len = Client.EndReceive(ar);
                byte[] receiveBuffer = new byte[len];
                Array.Copy(buffer, 0, receiveBuffer, 0, len);
                Packet packet = new Packet(receiveBuffer);
                if (SpaceInfo.ContainsKey(packet.Des_IP) || packet.Des_IP.EndsWith(BRODCASTADDRESS))
                {
                    Packets.Enqueue(packet);
                }
            }
            Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(this.OnReceive), null);
        }

        protected void PacketsManage()
        {
            while (true)
            {
                if (Packets.TryDequeue(out Packet packet))
                {
                    if (packet.Des_PORT.Equals(ARTNETPORT.ToString()))
                    {
                        if (SpaceInfo.ContainsKey(packet.Des_IP))
                        {
                            int port = (packet.Byte_Data[22] & 0xFF) | ((packet.Byte_Data[23] & 0xFF) << 8) + SpaceInfo[packet.Des_IP] * 256;
                            int length = (int)(packet.Byte_Data[25] & 0xFF) | ((packet.Byte_Data[24] & 0xFF) << 8);
                            byte[] dmxData = new byte[length];
                            Array.Copy(packet.Byte_Data.ToArray(), 26, dmxData, 0, length);
                            Console.WriteLine("接收到DMX512数据包，空间编号为：" + port);
                            Manager(SpaceInfo[packet.Des_IP], port, new List<byte>(dmxData));
                        }
                        else if (packet.Des_IP.EndsWith(BRODCASTADDRESS))
                        {
                            if (packet.Byte_Data[8] == 0x41 &&
                                packet.Byte_Data[9] == 0x72 &&
                                packet.Byte_Data[10] == 0x74 &&
                                packet.Byte_Data[11] == 0x2D &&
                                packet.Byte_Data[12] == 0x4E &&
                                packet.Byte_Data[13] == 0x65 &&
                                packet.Byte_Data[14] == 0x74 &&
                                packet.Byte_Data[15] == 0x00 &&
                                packet.Byte_Data[16] == 0x00 &&
                                packet.Byte_Data[17] == 0x20 &&
                                packet.Byte_Data[18] == 0x00 &&
                                packet.Byte_Data[19] == 0x0E &&
                                packet.Byte_Data[20] == 0x02 &&
                                packet.Byte_Data[21] == 0x10)
                            {
                                Console.WriteLine("收到搜索包");
                                AnswerArtNetSearch();
                            }
                            else if (packet.Byte_Data[8] == 0x4D &&
                                     packet.Byte_Data[9] == 0x61 &&
                                     packet.Byte_Data[10] == 0x64 &&
                                     packet.Byte_Data[11] == 0x72 &&
                                     packet.Byte_Data[12] == 0x69 &&
                                     packet.Byte_Data[13] == 0x78 &&
                                     packet.Byte_Data[14] == 0x4E &&
                                     packet.Byte_Data[15] == 0x00 &&
                                     packet.Byte_Data[16] == 0x02 &&
                                     packet.Byte_Data[17] == 0x52 &&
                                     packet.Byte_Data[18] == 0x00 &&
                                     packet.Byte_Data[19] == 0x0E)
                            {
                                Sync();
                                Console.WriteLine("收到同步包1");
                            }
                            else if (packet.Byte_Data[8] == 0x4D &&
                                     packet.Byte_Data[9] == 0x61 &&
                                     packet.Byte_Data[10] == 0x64 &&
                                     packet.Byte_Data[11] == 0x72 &&
                                     packet.Byte_Data[12] == 0x69 &&
                                     packet.Byte_Data[13] == 0x78 &&
                                     packet.Byte_Data[14] == 0x4E &&
                                     packet.Byte_Data[15] == 0x00 &&
                                     packet.Byte_Data[16] == 0x01 &&
                                     packet.Byte_Data[17] == 0x51 &&
                                     packet.Byte_Data[18] == 0x00 &&
                                     packet.Byte_Data[19] == 0x0E)
                            {
                                Sync();
                                Console.WriteLine("收到同步包2");
                            }
                            else if (packet.Byte_Data[8] == 0x41 &&
                                     packet.Byte_Data[9] == 0x72 &&
                                     packet.Byte_Data[10] == 0x74 &&
                                     packet.Byte_Data[11] == 0x2D &&
                                     packet.Byte_Data[12] == 0x4E &&
                                     packet.Byte_Data[13] == 0x65 &&
                                     packet.Byte_Data[14] == 0x74 &&
                                     packet.Byte_Data[15] == 0x00 &&
                                     packet.Byte_Data[16] == 0x00 &&
                                     packet.Byte_Data[17] == 0x52 &&
                                     packet.Byte_Data[18] == 0x00 &&
                                     packet.Byte_Data[19] == 0x0E)
                            {
                                Sync();
                                Console.WriteLine("收到同步包3");
                            }
                        }
                    }
                }
            }
        }

        protected void AnswerArtNetSearch()
        {
            byte[] souce = Constant.GetReceiveDataBySerchDeviceOrder();
            byte[] data = new byte[souce.Length];
            Array.Copy(souce, data, souce.Length);
            //修改MAC地址
            string[] macAddress = GetMacUtils.GetMac().Split(':');
            data[201] = Convert.ToByte(macAddress[0], 16);
            data[202] = Convert.ToByte(macAddress[1], 16);
            data[203] = Convert.ToByte(macAddress[2], 16);
            data[204] = Convert.ToByte(macAddress[3], 16);
            data[205] = Convert.ToByte(macAddress[4], 16);
            data[206] = Convert.ToByte(macAddress[5], 16);
            for (int ipIndex = 0; ipIndex < IPCount; ipIndex++)
            {
                string[] iPAddress = IPS[ipIndex].Split('.');
                data[10] = Convert.ToByte(Convert.ToInt16(iPAddress[0]));
                data[11] = Convert.ToByte(Convert.ToInt16(iPAddress[1]));
                data[12] = Convert.ToByte(Convert.ToInt16(iPAddress[2]));
                data[13] = Convert.ToByte(Convert.ToInt16(iPAddress[3]));
                int currentIPPortCount = (ipIndex == IPCount - 1 ? PortCount - 256 * ipIndex : 256);
                int bindIndex = 0;
                for (int portIndex = 0; portIndex < currentIPPortCount; portIndex += 4,bindIndex++)
                {
                    //修改空间编号
                    for (int i = 0; i < 4; i++)
                    {
                        if (portIndex + i >= currentIPPortCount)
                        {
                            data[190 + i] = data[186 + i] = data[190 + i - 1];
                        }
                        else
                        {
                            data[190 + i] = data[186 + i] = Convert.ToByte(portIndex + i);
                        }
                    }
                    data[211] = Convert.ToByte(bindIndex);
                    UdpClient.Send(data, data.Length, BRODCAST);
                }
            }
        }
    }
}
