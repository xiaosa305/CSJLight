using MultiLedController.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiLedController.multidevice.newmultidevice
{
    public class NewVirtualClient
    {
        private const int SPACE_MAX_COUNT = 4;
        private const int SERVER_PORT = 6454;
        public delegate void DMXDataResponse(int ledSpaceNumber, List<byte> data);

        private Socket Send { get; set; }
        private UdpClient Client { get; set; }
        private Thread Receive { get; set; }
        private string VirtualClientIP { get; set; }
        private string ServerIP { get; set; }
        private int StartLedSpace { get; set; }
        private int SpaceNumber { get; set; }
        private bool ReceiveStatus { get; set; }
        private int[] VirtualClientLedSpaceNumbers { get; set; }
        private DMXDataResponse Response { get; set; }



        //test
        public int index { get; set; }



        public NewVirtualClient(string serverIP,string virtualClientIP,int startLedSpace,int spaceNumber,DMXDataResponse response)
        {
            this.ServerIP = serverIP;
            this.VirtualClientIP = virtualClientIP;
            this.StartLedSpace = startLedSpace;
            this.SpaceNumber = spaceNumber;
            this.Response = response;
            this.InitParam();
            this.InitSocket();
        }

        public void Close()
        {
            this.ReceiveStatus = false;
            if (this.Send != null)
            {
                if (this.Client != null)
                {
                    this.Client.Close();
                    this.Client = null;
                }
                this.Send.Close();
                this.Send = null;
            }
        }

        private void InitSocket()
        {
            this.Send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.Send.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            this.Send.Bind(new IPEndPoint(IPAddress.Parse(this.VirtualClientIP), SERVER_PORT));
            this.Client = new UdpClient() { Client = this.Send };
            this.Receive = new Thread(this.ReceiveEvent) { IsBackground = true };
            this.ReceiveStatus = true;
            this.Receive.Start(this.Client);
        }

        private void InitParam()
        {
            try
            {
                this.VirtualClientLedSpaceNumbers = new int[SPACE_MAX_COUNT];
                for (int index = 0; index < SPACE_MAX_COUNT; index++)
                {
                    if (index >= this.SpaceNumber)
                    {
                        this.VirtualClientLedSpaceNumbers[index] = this.VirtualClientLedSpaceNumbers[index - 1];
                    }
                    else
                    {
                        this.VirtualClientLedSpaceNumbers[index] = this.StartLedSpace + index;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client：Error");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
           
        }

        private void ReceiveEvent(Object obj)
        {
            UdpClient client = obj as UdpClient;
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(this.ServerIP), SERVER_PORT);
            while (this.ReceiveStatus)
            {
                try
                {
                    byte[] receiveBuff = client.Receive(ref iPEndPoint);
                    if (receiveBuff.Length > 18 && receiveBuff[8] == 0x00 && receiveBuff[9] == 0x50)
                    {
                        int port = (int)(receiveBuff[14] & 0xFF) | ((receiveBuff[15] & 0xFF) << 8);
                        int dataLength = (int)(receiveBuff[17] & 0xFF) | ((receiveBuff[16] & 0xFF) << 8);
                        byte[] DMXDataBuff = new byte[dataLength];
                        Array.Copy(receiveBuff, 18, DMXDataBuff, 0, dataLength);
                        if (receiveBuff.Length == (18 + dataLength))
                        {
                            this.Response(port, new List<byte>(DMXDataBuff));
                        }
                        else
                        {
                            Console.WriteLine("麦爵士关闭调试");
                        }
                    }
                    else if (receiveBuff.Length > 10 && receiveBuff[8] == 0x00 && receiveBuff[9] == 0x20)
                    {
                        this.ReponseSearch();
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("XIAOSA：" + this.VirtualClientIP + "关闭接收");
                }
            }
        }

        private void ReponseSearch()
        {
            byte[] souce = Constant.GetReceiveDataBySerchDeviceOrder();
            byte[] data = new byte[souce.Length];
            Array.Copy(souce, data, souce.Length);
            //修改IP地址
            string[] iPAddress = this.VirtualClientIP.Split('.');
            data[10] = Convert.ToByte(Convert.ToInt16(iPAddress[0]));
            data[11] = Convert.ToByte(Convert.ToInt16(iPAddress[1]));
            data[12] = Convert.ToByte(Convert.ToInt16(iPAddress[2]));
            data[13] = Convert.ToByte(Convert.ToInt16(iPAddress[3]));

            //data[10] = 0xC0;
            //data[11] = 0xA8;
            //data[12] = 0x32;
            //data[13] = 0x29;

            //Test
            data[18] = 0x00;
            data[19] = 0x00;



            //修改空间编号
            data[190] = data[186] = Convert.ToByte(this.VirtualClientLedSpaceNumbers[0]);
            data[191] = data[187] = Convert.ToByte(this.VirtualClientLedSpaceNumbers[1]);
            data[192] = data[188] = Convert.ToByte(this.VirtualClientLedSpaceNumbers[2]);
            data[193] = data[189] = Convert.ToByte(this.VirtualClientLedSpaceNumbers[3]);
            //修改MAC地址
            string[] macAddress = GetMacUtils.GetMac().Split(':');
            data[201] = Convert.ToByte(macAddress[0], 16);
            data[202] = Convert.ToByte(macAddress[1], 16);
            data[203] = Convert.ToByte(macAddress[2], 16);
            data[204] = Convert.ToByte(macAddress[3], 16);
            data[205] = Convert.ToByte(macAddress[4], 16);
            data[206] = Convert.ToByte(macAddress[5], 16);


            //data[211] = Convert.ToByte(61 + index);


            this.Send.SendTo(data, new IPEndPoint(IPAddress.Broadcast, SERVER_PORT));
        }
    }
}
