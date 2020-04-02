using MultiLedController.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiLedController.utils.impl
{
    public class Art_Net_Client : IArt_Net_Client
    {
        private const int PORT = 6454;
        private Socket UDP_Send { get; set; }
        private UdpClient UDP_Receive { get; set; }
        private string CurrentIp { get; set; }
        private string ServerIp { get; set; }
        private List<int> Fields { get; set; }
        private Thread ReceiveThread { get; set; }
        private byte Addr { get; set; }
        private bool ReceiveStartStatus { get; set; }
        private Art_Net_Manager Manager { get; set; }

        private Dictionary<int,List<byte>> Field_Datas { get; set; }
        private Dictionary<int,bool> Field_Datas_Status { get; set; }

        /// <summary>
        /// 功能：虚拟控制器客户端构造函数
        /// </summary>
        /// <param name="currentIp">分配给虚拟控制器的IP地址</param>
        /// <param name="serverIp">麦爵士所在服务器IP地址</param>
        /// <param name="startIndex">控制器空间起始编号</param>
        /// <param name="spaceNum">控制器空间数量</param>
        /// <param name="manager">管理器既Art_Net_Manager</param>
        public Art_Net_Client(string currentIp, string serverIp, int startIndex, int spaceNum, Art_Net_Manager manager)
        {
            //初始化
            this.Init();
            //配置本地IP
            this.CurrentIp = currentIp;
            this.ServerIp = serverIp;
            //将管理器传入用于数据组包
            this.Manager = manager;
            for (int i = startIndex; i < startIndex + spaceNum; i++)
            {
                this.Fields.Add(i);
                this.Field_Datas.Add(i, new List<byte>());
                this.Field_Datas_Status.Add(i, false);
            }
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(this.CurrentIp), PORT);
            //配置UDP发送器
            this.UDP_Send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.UDP_Send.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            this.UDP_Send.Bind(iPEnd);
            //配置UDP接收器
            this.UDP_Receive = new UdpClient
            {
                Client = this.UDP_Send
            };
            //配置线程接收UDP数据包
            this.ReceiveThread = new Thread(this.ReceiveMsg)
            {
                IsBackground = true
            };
            this.ReceiveStartStatus = true;
            //启动接收线程
            this.ReceiveThread.Start(this.UDP_Receive);
            this.Addr = Convert.ToByte(Convert.ToInt16(this.CurrentIp.Split('.')[3]));
            Console.WriteLine(currentIp + "虚拟客户端启动");
        }
        /// <summary>
        /// 功能：初始化缓存区
        /// </summary>
        private void Init()
        {
            this.Fields = new List<int>();
            this.ReceiveStartStatus = false;
            this.Field_Datas = new Dictionary<int, List<byte>>();
            this.Field_Datas_Status = new Dictionary<int, bool>();
        }
        /// <summary>
        /// 功能：网络数据接收模块
        /// </summary>
        /// <param name="obj"></param>
        private void ReceiveMsg(Object obj)
        {
            try
            {
                UdpClient client = obj as UdpClient;
                while (this.ReceiveStartStatus)
                {
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(this.ServerIp), PORT);
                    byte[] receiveData = client.Receive(ref endPoint);
                    if (receiveData[8] == 0x00 && receiveData[9] == 0x21)//接收到其他设备发送ArtPollReply包
                    {
                        continue;
                    }
                    else if (receiveData[8] == 0x00 && receiveData[9] == 0x20)//接收到ArtPoll包
                    {
                        this.SearchDevice_Receive();
                    }
                    else if (receiveData[8] == 0x00 && receiveData[9] == 0x60)//接收到ArtAddress分组。发送DMX调试数据前会发送
                    {
                        Console.WriteLine("接收到地址分组包");
                    }
                    else if (receiveData[8] == 0x00 && receiveData[9] == 0x50)//这是ArtDMX数据包
                    {
                        int physicalPortIndex = Convert.ToInt16(receiveData[13]);
                        int universe = (int)(receiveData[14] & 0xFF) | ((receiveData[15] & 0xFF) << 8);
                        int dataLength = (int)(receiveData[17] & 0xFF) | ((receiveData[16] & 0xFF) << 8);
                        byte[] dmxData = new byte[dataLength];
                        Array.Copy(receiveData, 18, dmxData, 0, dataLength);
                        List<byte> data = new List<byte>();
                        data.AddRange(dmxData);
                        this.Manager.AddFieldData(universe, data);
                    }
                    else
                    {
                        Console.WriteLine(CurrentIp + "其他数据包,包大小:" + receiveData.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(CurrentIp + "关闭UDPClient == >" + ex.Message);
            }
        }
        /// <summary>
        /// 功能：终端搜索回复虚拟控制器信息
        /// </summary>
        private void SearchDevice_Receive()
        {
            byte[] souce = Constant.GetReceiveDataBySerchDeviceOrder();
            byte[] data = new byte[souce.Length];
            Array.Copy(souce, data, souce.Length);
            data[10] = Convert.ToByte(Convert.ToInt16(this.CurrentIp.Split('.')[0]));
            data[11] = Convert.ToByte(Convert.ToInt16(this.CurrentIp.Split('.')[1]));
            data[12] = Convert.ToByte(Convert.ToInt16(this.CurrentIp.Split('.')[2]));
            data[13] = Convert.ToByte(Convert.ToInt16(this.CurrentIp.Split('.')[3]));
            data[186] = Convert.ToByte(this.Fields[0]);
            data[187] = Convert.ToByte(this.Fields[1]);
            data[188] = Convert.ToByte(this.Fields[2]);
            data[189] = Convert.ToByte(this.Fields[3]);
            data[190] = Convert.ToByte(this.Fields[0]);
            data[191] = Convert.ToByte(this.Fields[1]);
            data[192] = Convert.ToByte(this.Fields[2]);
            data[193] = Convert.ToByte(this.Fields[3]);
            Thread.Sleep(30);
            this.UDP_Send.SendTo(data, new IPEndPoint(IPAddress.Broadcast, PORT));
        }
        /// <summary>
        ///功能： 关闭虚拟控制器
        /// </summary>
        public void Close()
        {
            this.ReceiveStartStatus = false;
            Thread.Sleep(100);
            this.UDP_Receive.Close();
            this.UDP_Send.Close();
            this.UDP_Receive = null;
            this.UDP_Send = null;
            Console.WriteLine(CurrentIp + "关闭客户端");
        }
    }
}
