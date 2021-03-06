using MultiLedController.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiLedController.multidevice.multidevicepromax
{
    public class M5VirtualProClient
    {
        private const int ARTNET_PORT = 6454;
        private String ArtNetServerIP { get; set; }
        private String LocalIP { get; set; }
        private int OutPortCount { get; set; }
        private Socket ArtNetClient { get; set; }
        private Thread ArtNetClientReceive { get; set; }
        private UdpClient ArtNetReceiveClient { get; set; }
        private bool IsReceive { get; set; }
        private M5DMXDataManager Manager { get; set; }
        private int ClientIndex { get; set; }

        public delegate void M5DMXDataManager(int port, List<byte> dmxData);

        private M5VirtualProClient()
        {

        }

        public static M5VirtualProClient Build(int clientIndex, String localIP, int portCount, M5DMXDataManager manager)
        {
            return M5VirtualProClient.Build(clientIndex, localIP, localIP, portCount, manager);
        }

        public static M5VirtualProClient Build(int clientIndex, String localIP, String ArtNetServerIP, int portCount, M5DMXDataManager manager)
        {
            M5VirtualProClient client = new M5VirtualProClient()
            {
                LocalIP = localIP,
                ArtNetServerIP = ArtNetServerIP,
                OutPortCount = portCount,
                Manager = manager,
                ClientIndex = clientIndex
            };
            client.StartArtNetClient();
            return client;
        }


        public void Close()
        {
            try
            {
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

        private void Init()
        {
            try
            {
                this.ArtNetClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.ArtNetClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                this.ArtNetClient.Bind(new IPEndPoint(IPAddress.Parse(this.LocalIP), ARTNET_PORT));
                this.ArtNetReceiveClient = new UdpClient { Client = this.ArtNetClient };
                this.ArtNetClientReceive = new Thread(this.ArtNetClientReceiveListen) { IsBackground = true };
                this.IsReceive = true;
                this.ArtNetClientReceive.Start(this.ArtNetReceiveClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void ArtNetClientReceiveListen(Object obj)
        {
            UdpClient client = obj as UdpClient;
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(this.ArtNetServerIP), ARTNET_PORT);
            while (this.IsReceive)
            {
                try
                {
                    byte[] receiveBuff = client.Receive(ref iPEndPoint);
                    if (receiveBuff.Length > 18 && receiveBuff[8] == 0x00 && receiveBuff[9] == 0x50)
                    {
                        int port = (int)((receiveBuff[14] & 0xFF) | ((receiveBuff[15] & 0xFF) << 8));
                        int dataLength = (int)(receiveBuff[17] & 0xFF) | ((receiveBuff[16] & 0xFF) << 8);
                        if (dataLength != 0 && receiveBuff.Length == (dataLength + 18))
                        {
                            byte[] DMXDataBuff = new byte[dataLength];
                            Array.Copy(receiveBuff, 18, DMXDataBuff, 0, dataLength);
                            this.Manager(port, new List<byte>(DMXDataBuff));
                        }
                    }
                    else if (receiveBuff.Length > 10 && receiveBuff[8] == 0x00 && receiveBuff[9] == 0x20)
                    {
                        int index = 0;
                        for (int startPort = 1024 * this.ClientIndex; startPort < this.ClientIndex * 1024 + this.OutPortCount; startPort += 4)
                        {
                            this.ReplyServer(startPort, this.ClientIndex * 64 + index);
                            index++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private void ReplyServer(int startPort, int bindIndex)
        {
            byte[] souce = Constant.GetM5ReceiveDataBySerchDeviceOrder();
            byte[] data = new byte[souce.Length];
            Array.Copy(souce, data, souce.Length);
            string[] iPAddress = this.LocalIP.Split('.');
            data[10] = Convert.ToByte(Convert.ToInt16(iPAddress[0]));
            data[11] = Convert.ToByte(Convert.ToInt16(iPAddress[1]));
            data[12] = Convert.ToByte(Convert.ToInt16(iPAddress[2]));
            data[13] = Convert.ToByte(Convert.ToInt16(iPAddress[3]));


            //Test
            data[18] = Convert.ToByte((startPort >> 8 ) & 0xFF);//高字节空间编号
            data[207] = Convert.ToByte(Convert.ToInt16(iPAddress[0]));//绑定IP地址
            data[208] = Convert.ToByte(Convert.ToInt16(iPAddress[1]));
            data[209] = Convert.ToByte(Convert.ToInt16(iPAddress[2]));
            data[210] = Convert.ToByte(Convert.ToInt16(iPAddress[3]));

            //修改空间编号
            for (int i = 0; i < 4; i++)
            {

                if (startPort + i >= OutPortCount)
                {
                    data[190 + i] = data[186 + i] = data[190 + i - 1];
                }
                else
                {
                    data[190 + i] = data[186 + i] = Convert.ToByte((startPort + i) & 0xFF);
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
