using MultiLedController.utils;
using MultiLedController.xiaosa.entity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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
        protected void MessageTransTask()
        {
            Stopwatch stopwatch = new Stopwatch();
            while (this.MessageTransStatus)
            {
                if (MessageQueue.Count > 0)
                {
                    try
                    {
                        stopwatch.Restart();
                        MessageQueue.TryDequeue(out byte[] receiveBuff);
                        if (receiveBuff != null)
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
                            else if (CheckSync(receiveBuff, ClientIndex))
                            {
                                this.DMXSync();
                            }
                        }
                        //if (stopwatch.ElapsedTicks > 100)
                        //{
                        //    Console.WriteLine("Time : " + stopwatch.ElapsedMilliseconds);
                        //}
                        stopwatch.Stop();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
        }
        private static bool CheckSync(byte[] data,int clientIndex)
        {
            bool result = false;
            if (data.Length == 13 &&
                                 data[0] == 0x4D &&
                                 data[1] == 0x61 &&
                                 data[2] == 0x64 &&
                                 data[3] == 0x72 &&
                                 data[4] == 0x69 &&
                                 data[5] == 0x78 &&
                                 data[6] == 0x4E &&
                                 data[7] == 0x00 &&
                                 data[8] == 0x02 &&
                                 data[9] == 0x52 &&
                                 data[10] == 0x00 &&
                                 data[11] == 0x0E && clientIndex == 0)
            {
                result = true;
            }
            else if (data.Length == 13 &&
                                 data[0] == 0x4D &&
                                 data[1] == 0x61 &&
                                 data[2] == 0x64 &&
                                 data[3] == 0x72 &&
                                 data[4] == 0x69 &&
                                 data[5] == 0x78 &&
                                 data[6] == 0x4E &&
                                 data[7] == 0x00 &&
                                 data[8] == 0x01 &&
                                 data[9] == 0x51 &&
                                 data[10] == 0x00 &&
                                 data[11] == 0x0E && clientIndex == 0)
            {
                result = true;
            }
            else if (data.Length == 13 &&
                                 data[0] == 0x41 &&
                                 data[1] == 0x72 &&
                                 data[2] == 0x74 &&
                                 data[3] == 0x2D &&
                                 data[4] == 0x4E &&
                                 data[5] == 0x65 &&
                                 data[6] == 0x74 &&
                                 data[7] == 0x00 &&
                                 data[8] == 0x00 &&
                                 data[9] == 0x52 &&
                                 data[10] == 0x00 &&
                                 data[11] == 0x0E && clientIndex == 0)
            {
                result = true;
            }
            return result;
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
            //修改MAC地址
            string[] macAddress = GetMacUtils.GetMac().Split(':');
            data[201] = Convert.ToByte(macAddress[0], 16);
            data[202] = Convert.ToByte(macAddress[1], 16);
            data[203] = Convert.ToByte(macAddress[2], 16);
            data[204] = Convert.ToByte(macAddress[3], 16);
            data[205] = Convert.ToByte(macAddress[4], 16);
            data[206] = Convert.ToByte(macAddress[5], 16);
            data[211] = Convert.ToByte(bindIndex);
            this.ArtNetClient.SendTo(data, new IPEndPoint(IPAddress.Broadcast, ARTNET_PORT));
        }
    }
}
