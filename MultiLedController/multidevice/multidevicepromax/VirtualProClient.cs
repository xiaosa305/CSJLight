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
using System.Threading.Tasks;

namespace MultiLedController.multidevice.multidevicepromax
{

    //main
    public class VirtualProClient
    {
        private const int ARTNET_PORT = 6454;
        private String ArtNetServerIP { get; set; }
        private String LocalIP { get; set; }
        private int OutPortCount { get; set; }
        private Socket ArtNetClient { get; set; }
        private Thread ArtNetClientReceive { get; set; }
        private UdpClient ArtNetReceiveClient { get; set; }
        private bool IsReceive { get; set; }
        private DMXDataManager Manager { get; set; }
        private DMXDataCacheSync DMXSync { get; set; }
        private int ClientIndex { get; set; }
        protected ConcurrentQueue<byte[]> MessageQueue { get; set; }
        protected Thread MessageTrans { get; set; }
        protected bool MessageTransStatus { get; set; }

        public delegate void DMXDataManager(int cliendIndex,int port,List<byte> dmxData);
        public delegate void DMXDataCacheSync();

        private VirtualProClient()
        {
            MessageTransStatus = true;
            MessageQueue = new ConcurrentQueue<byte[]>();
            MessageTrans = new Thread(new ThreadStart(MessageTransTask)) { IsBackground = true };
            MessageTrans.Start();
        }

        public static VirtualProClient Build(int clientIndex,String localIP,int portCount, DMXDataManager manager,DMXDataCacheSync sync)
        {
            return VirtualProClient.Build(clientIndex,localIP, localIP, portCount, manager,sync);
        }

        public static VirtualProClient Build(int clientIndex, String localIP,String ArtNetServerIP,int portCount, DMXDataManager manager,DMXDataCacheSync sync)
        {
            VirtualProClient client = new VirtualProClient()
            {
                LocalIP = localIP,
                ArtNetServerIP = ArtNetServerIP,
                OutPortCount = portCount,
                Manager = manager,
                ClientIndex = clientIndex,
                DMXSync = sync  
            };
            client.StartArtNetClient();
            return client;
        }


        public void Close()
        {
            try
            {
                MessageTransStatus = false;
                this.IsReceive = false;
                if (this.ArtNetClient != null)
                {
                    if (this.ArtNetReceiveClient != null)
                    {
                        this.ArtNetReceiveClient.Close();
                        this.ArtNetReceiveClient = null;
                    }
                    this.ArtNetClient.Close();
                    this.ArtNetClient = null;
                }
                MessageQueue = new ConcurrentQueue<byte[]>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }


        private void StartArtNetClient()
        {
            this.Init();
        }


        private bool issaved = true;
        private delegate void updateInfodelegate();
        private int SIO_RCVALL = -2147483648 | 0x18000000 | 1;
        List<Packet> packets = new List<Packet>();
        Packet temp_packet;
        byte[] buffer;
        bool iscallstop = false;


        private void Init()
        {
            try
            {
                //this.ArtNetClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                //this.ArtNetClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                //this.ArtNetClient.Bind(new IPEndPoint(IPAddress.Parse(this.LocalIP), ARTNET_PORT));
                //this.ArtNetReceiveClient = new UdpClient { Client = this.ArtNetClient };
                //this.ArtNetClientReceive = new Thread(this.ArtNetClientReceiveListen) { IsBackground = true };
                //this.IsReceive = true;
                //this.ArtNetClientReceive.Start(this.ArtNetReceiveClient);


                //buffer = new byte[1024 * 1024];
                //IPAddress iP = IPAddress.Parse(LocalIP);
                //ArtNetClient = new Socket(iP.AddressFamily, SocketType.Raw, ProtocolType.IP);
                //ArtNetClient.Bind(new IPEndPoint(iP, 0));
                //ArtNetClient.IOControl(SIO_RCVALL, BitConverter.GetBytes((int)1), null);
                //if (!iscallstop)
                //{
                //    ArtNetClient.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(this.OnReceive), null);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        /// <param name="ar"></param>
        private void OnReceive(IAsyncResult ar)
        {
            int len = ArtNetClient.EndReceive(ar);
            if (ArtNetClient != null)
            {
                byte[] receiveBuffer = new byte[len];
                Array.Copy(buffer, 0, receiveBuffer, 0, len);
                Packet packet = new Packet(receiveBuffer);
                if (packet.Des_PORT.Equals("6454"))
                {
                    if (packet.Des_IP.EndsWith(".255"))
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
                            Console.WriteLine("收到同步包3");
                        }
                    }
                    Console.WriteLine("desIP :   " + packet.Des_IP + "---------" + packet.ProString);
                }

                packets.Add(packet);
                temp_packet = packet;
            }
            if (!iscallstop)
            {
                ArtNetClient.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(this.OnReceive), null);
            }
        }

        protected void MessageTransTask()
        {
            while (this.MessageTransStatus)
            {
                if (MessageQueue.TryDequeue(out byte[] receiveBuff))
                {
                    try
                    {
                        if (receiveBuff.Length > 18 && receiveBuff[8] == 0x00 && receiveBuff[9] == 0x50)
                        {
                            int port = (int)((receiveBuff[14] & 0xFF) | ((receiveBuff[15] & 0xFF) << 8));
                            int dataLength = (int)(receiveBuff[17] & 0xFF) | ((receiveBuff[16] & 0xFF) << 8);
                            if (dataLength != 0 && receiveBuff.Length == (dataLength + 18))
                            {
                                byte[] DMXDataBuff = new byte[dataLength];
                                Array.Copy(receiveBuff, 18, DMXDataBuff, 0, dataLength);
                                this.Manager(this.ClientIndex, port, new List<byte>(DMXDataBuff));
                            }
                        }
                        else if (receiveBuff.Length > 10 && receiveBuff[8] == 0x00 && receiveBuff[9] == 0x20)
                        { 
                            int index = 1;
                            for (int startPort = 0; startPort < this.OutPortCount; startPort += 4)
                            {
                                ReplyServer(startPort, index);
                                index++;
                            }
                        }
                        else if (receiveBuff.Length == 13 &&
                                 receiveBuff[0] == 0x4D &&
                                 receiveBuff[1] == 0x61 &&
                                 receiveBuff[2] == 0x64 &&
                                 receiveBuff[3] == 0x72 &&
                                 receiveBuff[4] == 0x69 &&
                                 receiveBuff[5] == 0x78 &&
                                 receiveBuff[6] == 0x4E &&
                                 receiveBuff[7] == 0x00 &&
                                 receiveBuff[8] == 0x02 &&
                                 receiveBuff[9] == 0x52 &&
                                 receiveBuff[10] == 0x00 &&
                                 receiveBuff[11] == 0x0E && ClientIndex == 0)
                        {
                            this.DMXSync();
                        }
                        else if (receiveBuff.Length == 13 &&
                                 receiveBuff[0] == 0x4D &&
                                 receiveBuff[1] == 0x61 &&
                                 receiveBuff[2] == 0x64 &&
                                 receiveBuff[3] == 0x72 &&
                                 receiveBuff[4] == 0x69 &&
                                 receiveBuff[5] == 0x78 &&
                                 receiveBuff[6] == 0x4E &&
                                 receiveBuff[7] == 0x00 &&
                                 receiveBuff[8] == 0x01 &&
                                 receiveBuff[9] == 0x51 &&
                                 receiveBuff[10] == 0x00 &&
                                 receiveBuff[11] == 0x0E && ClientIndex == 0)
                        {
                            this.DMXSync();
                        }
                        else if (receiveBuff.Length == 13 &&
                                 receiveBuff[0] == 0x41 &&
                                 receiveBuff[1] == 0x72 &&
                                 receiveBuff[2] == 0x74 &&
                                 receiveBuff[3] == 0x2D &&
                                 receiveBuff[4] == 0x4E &&
                                 receiveBuff[5] == 0x65 &&
                                 receiveBuff[6] == 0x74 &&
                                 receiveBuff[7] == 0x00 &&
                                 receiveBuff[8] == 0x00 &&
                                 receiveBuff[9] == 0x52 &&
                                 receiveBuff[10] == 0x00 &&
                                 receiveBuff[11] == 0x0E && ClientIndex == 0)
                        {
                            this.DMXSync();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
        }


        private void ArtNetClientReceiveListen(Object obj)
        {
            UdpClient client = obj as UdpClient;
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(this.ArtNetServerIP), ARTNET_PORT);
            //IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, ARTNET_PORT);
            //EndPoint endPoint = iPEndPoint;
            while (this.IsReceive)
            {
                try
                {
                    byte[] receiveBuff = client.Receive(ref iPEndPoint);
                    if (receiveBuff != null && receiveBuff.Length > 0)
                    {
                        MessageQueue.Enqueue(receiveBuff);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void ReplyServer(int startPort,int bindIndex)
        {
            byte[] souce = Constant.GetReceiveDataBySerchDeviceOrder();
            byte[] data = new byte[souce.Length];
            Array.Copy(souce, data, souce.Length);
            string[] iPAddress = this.LocalIP.Split('.');
            data[10] = Convert.ToByte(Convert.ToInt16(iPAddress[0]));
            data[11] = Convert.ToByte(Convert.ToInt16(iPAddress[1]));
            data[12] = Convert.ToByte(Convert.ToInt16(iPAddress[2]));
            data[13] = Convert.ToByte(Convert.ToInt16(iPAddress[3]));


            ////Test
            //data[18] = Convert.ToByte(0x01);//高字节空间编号
            //data[207] = Convert.ToByte(Convert.ToInt16(iPAddress[0]));//绑定IP地址
            //data[208] = Convert.ToByte(Convert.ToInt16(iPAddress[1]));
            //data[209] = Convert.ToByte(Convert.ToInt16(iPAddress[2]));
            //data[210] = Convert.ToByte(Convert.ToInt16(iPAddress[3]));

            //修改空间编号
            for (int i = 0; i < 4; i++)
            {

                if (startPort + i >= OutPortCount)
                {
                    data[190 + i] = data[186 + i] = data[190 + i - 1];
                }
                else
                {
                    data[190 + i] = data[186 + i] = Convert.ToByte(startPort + i);
                }
            }
            //data[190] = data[186] = Convert.ToByte(startPort);
            //data[191] = data[187] = Convert.ToByte(startPort + 1);
            //data[192] = data[188] = Convert.ToByte(startPort + 2);
            //data[193] = data[189] = Convert.ToByte(startPort + 3);
            //修改MAC地址
            string[] macAddress = GetMacUtils.GetMac().Split(':');
            data[201] = Convert.ToByte(macAddress[0], 16);
            data[202] = Convert.ToByte(macAddress[1], 16);
            data[203] = Convert.ToByte(macAddress[2], 16);
            data[204] = Convert.ToByte(macAddress[3], 16);
            data[205] = Convert.ToByte(macAddress[4], 16);
            data[206] = Convert.ToByte(macAddress[5], 16);

            data[211] = Convert.ToByte(bindIndex);
            //this.ArtNetClient.SendTo(data, new IPEndPoint(IPAddress.Parse(ArtNetServerIP), ARTNET_PORT));
            this.ArtNetClient.SendTo(data, new IPEndPoint(IPAddress.Broadcast, ARTNET_PORT));

        }
    }
}
