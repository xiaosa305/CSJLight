using MultiLedController.entity;
using MultiLedController.utils;
using MultiLedController.utils.impl;
using System;
using System.Collections.Generic;
using System.Diagnostics.Design;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiLedController.multidevice.impl
{
    public class VirtualControlDevice
    {
        private const int VIRTUAL_CLIENT_PORT = 6454; 
        private readonly Object SYNCHROLOCK_KEY = new object();//同步资源锁


        private List<VirtualClient> VirtualClients { get; set; }//虚拟客户端池
        private int VirtualDeviceIndex { get; set; }//虚拟控制卡编号
        private ControlDevice ControlDevice { get; set; }//控制卡信息

        private bool IsStartResponseDmxDataStatus { get; set; }//是否开始接受DMX数据标记位
        private bool IsRecordStatus { get; set; }//数据记录标记位
        private bool IsDebugStatus { get; set; }//实时调试标记位
        private bool IsFirstFrameDmxDataStatus { get; set; }//首帧标记位
        private int StartLedSpace { get; set; }

        private long LastFrameResponseTime { get; set; }//帧间隔时间
        private int LastFramePacketSequence { get; set; }//包序

        private Dictionary<int,List<byte>> VirtualClientDmxDatas { get; set; }//虚拟客户端DMX数据缓存池
        private Dictionary<int,bool> VirtualClientDmxDataResponseStatus { get; set; }//虚拟客户端DMX数据接收状态标记池

        private DebugDmxDataQueue DebugDmxDataQueue { get; set; }//实时调试队列
        private RecodeDmxDataQueue RecordDmxDataQueue { get; set; }//录制数据队列


        private Socket ControlDeviceUdpSend { get; set; }//控制卡服务器
        private UdpClient ControlDeviceUdpClient { get; set; }//控制卡客户端
        private Thread ControlDeviceUdpReceiveThread { get; set; }//控制卡客户端接收模块线程
        private bool ControlDeviceUdpReceiveStatus { get; set; }//控制卡客户端接收标记位


        private Thread ControlDeviceDebugThread { get; set; }//实时调试线程
        private Thread ControlDeviceRecordThread { get; set; }//录制线程
        private bool ControlDeviceDebugThreadStatus { get; set; }//实时调试线程标记位
        private bool ControlDeviceRecordThreadStatus { get; set; }//录制线程标记位

        private int RecordFrameCount { get; set; }//录制帧数记录
        private int DebugFrameCount { get; set; }//实时调试帧数记录
        private string RecordFilePath { get; set; }//录制文件存储路径
        private string ServersIp { get; set; }

        public delegate void RecordFrameCountResponse(int frameCount);
        public delegate void DebugFrameCountResponse(int frameCount);

        private RecordFrameCountResponse RecordFrameCountResponse_Event { get; set; }
        private DebugFrameCountResponse DebugFrameCountResponse_Event { get; set; }


        private List<string> VirtualClientIPs { get; set; }


        public VirtualControlDevice(int index, int startLedSpace, ControlDevice device, List<string> ips,string serverIp)
        {
            this.VirtualDeviceIndex = index;
            this.StartLedSpace = startLedSpace;
            this.ControlDevice = device;
            this.VirtualClientIPs = ips;
            this.ServersIp = serverIp;
            this.InitParameter();
            this.InitUdpServers();
            this.InitControlDeviceDebugAndRecodeThread();
            this.CreateVirtualClient(startLedSpace, device, ips);
        }
        /// <summary>
        /// 功能：初始化参数
        /// </summary>
        private void InitParameter()
        {
            this.IsStartResponseDmxDataStatus = false;
            this.DebugDmxDataQueue = new DebugDmxDataQueue();
            this.RecordDmxDataQueue = new RecodeDmxDataQueue();


            this.VirtualClients = new List<VirtualClient>();
            this.VirtualClientDmxDatas = new Dictionary<int, List<byte>>();
            this.VirtualClientDmxDataResponseStatus = new Dictionary<int, bool>();
            this.IsRecordStatus = false;
            this.IsDebugStatus = false;
            this.IsFirstFrameDmxDataStatus = true;
            this.LastFrameResponseTime = -1;
            this.LastFramePacketSequence = 0;
            this.ControlDeviceUdpReceiveStatus = false;
            this.ControlDeviceDebugThreadStatus = false;
            this.ControlDeviceRecordThreadStatus = false;
        }
        /// <summary>
        /// 功能：初始化控制卡服务器和客户端
        /// </summary>
        private void InitUdpServers()
        {
            try
            {
                this.ControlDeviceUdpSend = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.ControlDeviceUdpSend.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                Console.WriteLine(this.ControlDevice.IP + " ：    Port is  " + (9999 + this.VirtualDeviceIndex + 1));
                this.ControlDeviceUdpSend.Bind(new IPEndPoint(IPAddress.Parse(this.ServersIp), 9999 + this.VirtualDeviceIndex + 1));
                this.ControlDeviceUdpClient = new UdpClient() { Client = this.ControlDeviceUdpSend};
                this.ControlDeviceUdpReceiveThread = new Thread(this.ControlDeviceUdpReceiveMsg) { IsBackground = true };
                this.ControlDeviceUdpReceiveThread.Start(this.ControlDeviceUdpClient);
                this.ControlDeviceUdpReceiveStatus = true;
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "配置控制卡服务器失败", ex);
            }
        }
        /// <summary>
        /// 关闭并释放资源
        /// </summary>
        public void CloseVirtualControlDevice()
        {
            try
            {
                this.ControlDeviceDebugThreadStatus = false;
                this.ControlDeviceRecordThreadStatus = false;
                this.IsStartResponseDmxDataStatus = false;
                this.IsDebugStatus = false;
                this.IsRecordStatus = false;
                this.ControlDeviceDebugThreadStatus = false;
                this.ControlDeviceRecordThreadStatus = false;
                this.ControlDeviceUdpReceiveStatus = false;
                if (this.ControlDeviceUdpSend != null)
                {
                    this.ControlDeviceUdpSend.Close();
                    if (this.ControlDeviceUdpClient != null)
                    {
                        this.ControlDeviceUdpClient.Close();
                        this.ControlDeviceUdpClient = null;
                    }
                    this.ControlDeviceUdpSend.Dispose();
                    this.ControlDeviceUdpSend = null;
                }
                this.DebugFrameCountResponse_Event = null;
                this.RecordFrameCountResponse_Event = null;
                foreach (VirtualClient client in this.VirtualClients)
                {
                    client.CloseVirtualClient();
                }
                this.VirtualClients = new List<VirtualClient>();
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "关闭虚拟控制器" + this.ControlDevice.IP + "失败", ex);
            }
        }
        /// <summary>
        /// 功能：初始化实时调试以及录制线程
        /// </summary>
        private void InitControlDeviceDebugAndRecodeThread()
        {
            this.ControlDeviceDebugThread = new Thread(this.ControlDeviceDebugStart) { IsBackground = true };
            this.ControlDeviceRecordThread = new Thread(this.ControlDeviceRecordStart) { IsBackground = true };
            this.ControlDeviceDebugThreadStatus = true;
            this.ControlDeviceRecordThreadStatus = true;
            this.ControlDeviceDebugThread.Start();
            this.ControlDeviceRecordThread.Start();
        }

        /// <summary>
        /// 功能：创建虚拟客户端接收DMX数据
        /// </summary>
        /// <param name="startLedSpace"></param>
        /// <param name="device"></param>
        /// <param name="ips"></param>
        /// <param name="serverIp"></param>
        private void CreateVirtualClient(int startLedSpace, ControlDevice device,List<string> ips)
        {
            for (int virtualClientIndex = 0; virtualClientIndex < device.Led_interface_num; virtualClientIndex++)
            {
                //新建虚拟客户端
                VirtualClient virtualClient = new VirtualClient(startLedSpace, device, ips[virtualClientIndex], this.ServersIp, DmxDataResponse);
                //将虚拟客户端添加到虚拟客户端池中
                //this.VirtualClients.Add(virtualClient);
                for (int ledSpaceNumber = startLedSpace; ledSpaceNumber < device.Led_space + startLedSpace; ledSpaceNumber++)
                {
                    this.VirtualClientDmxDatas.Add(ledSpaceNumber, new List<byte>());
                    this.VirtualClientDmxDataResponseStatus.Add(ledSpaceNumber, false);
                }
                //调整下一个虚拟客户端首个空间编号
                startLedSpace += device.Led_space;
            }
        }
        /// <summary>
        /// 功能：处理虚拟客户端接收的DMX数据
        /// </summary>
        /// <param name="ledSpaceNumber"></param>
        /// <param name="data"></param>
        private void DmxDataResponse(int ledSpaceNumber,List<byte> data)
        {
            lock (this.SYNCHROLOCK_KEY)
            {
                if (this.IsStartResponseDmxDataStatus)
                {
                    long frameIntervalTime = 0;
                    if (this.LastFrameResponseTime == -1)
                    {
                        this.LastFrameResponseTime = DateTime.Now.Ticks;
                    }
                    else
                    {
                        long nowTime = DateTime.Now.Ticks;
                        frameIntervalTime = (nowTime - this.LastFrameResponseTime) / 10000;
                        this.LastFrameResponseTime = nowTime;
                    }
                    if (frameIntervalTime > 5 && this.IsFirstFrameDmxDataStatus)
                    {
                        this.IsFirstFrameDmxDataStatus = false;
                        foreach (int key in this.VirtualClientDmxDataResponseStatus.Keys)
                        {
                            if (this.VirtualClientDmxDataResponseStatus[key])
                            {
                                this.LastFramePacketSequence += key;
                            }
                        }
                    }
                    else if (frameIntervalTime > 5 && !this.IsFirstFrameDmxDataStatus)
                    {
                        int nowPacketSequence = 0;
                        foreach (int key in this.VirtualClientDmxDataResponseStatus.Keys)
                        {
                            if (this.VirtualClientDmxDataResponseStatus[key])
                            {
                                nowPacketSequence += key;
                            }
                        }
                        if (nowPacketSequence == this.LastFramePacketSequence)
                        {
                            if (this.IsDebugStatus)
                            {
                                lock (this.DebugDmxDataQueue)
                                {
                                    this.DebugDmxDataQueue.Enqueue(this.VirtualClientDmxDatas, Convert.ToInt16(frameIntervalTime), this.ControlDevice);
                                }
                            }
                            if (this.IsRecordStatus)
                            {
                                lock (this.RecordDmxDataQueue)
                                {
                                    this.RecordDmxDataQueue.Enqueue(this.VirtualClientDmxDatas, new Dictionary<int, bool>(this.VirtualClientDmxDataResponseStatus), Convert.ToInt16(frameIntervalTime), this.ControlDevice);
                                }
                            }
                            List<int> keys = this.VirtualClientDmxDatas.Keys.ToList();
                            foreach (int key in keys)
                            {
                                this.VirtualClientDmxDatas[key] = new List<byte>();
                                this.VirtualClientDmxDataResponseStatus[key] = false;
                            }
                            //LogTools.Debug(Constant.TAG_XIAOSA,this.ControlDevice.IP + "接收到一帧");
                        }
                        this.LastFramePacketSequence = nowPacketSequence;
                    }
                    this.VirtualClientDmxDatas[ledSpaceNumber] = data;
                    this.VirtualClientDmxDataResponseStatus[ledSpaceNumber] = true;
                }
            }
        }
        /// <summary>
        /// 功能：控制卡服务器接收模块
        /// </summary>
        /// <param name="obj"></param>
        private void ControlDeviceUdpReceiveMsg(Object obj)
        {
            try
            {
                UdpClient udpClient = obj as UdpClient;
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(this.ControlDevice.IP), this.ControlDevice.LinkPort);
                while (this.ControlDeviceUdpReceiveStatus)
                {
                    byte[] receiveData = udpClient.Receive(ref iPEndPoint);
                    if (Encoding.Default.GetString(receiveData).Equals(Constant.RECEIVE_START_DEBUF_MODE))
                    {
                        this.DebugFrameCount = 0;
                        this.IsDebugStatus = true;
                        Console.WriteLine(this.ControlDevice.IP +"启动调试成功");
                    }
                    Thread.Sleep(0);
                }
            }
            catch (Exception)
            {
                //LogTools.Debug(Constant.TAG_XIAOSA, "控制卡服务器已关闭");
            }
        }

        /// <summary>
        /// 功能：启动调试模式
        /// </summary>
        public void StartDebugMode()
        {
            List<byte> package = new List<byte>();
            package.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x00, 0x00, 0x00, 0x07 });
            package.AddRange(Encoding.Default.GetBytes("poweron"));
            this.ControlDeviceUdpSend.SendTo(package.ToArray(), new IPEndPoint(IPAddress.Parse(this.ControlDevice.IP), this.ControlDevice.LinkPort));
        }
        /// <summary>
        /// 功能：关闭调试模式
        /// </summary>
        public void StopDebugMode()
        {
            this.IsDebugStatus = false;
        }
        /// <summary>
        /// 功能：实时调试线程执行任务
        /// </summary>
        /// <param name="obj"></param>
        private void ControlDeviceDebugStart(Object obj)
        {
            while (this.ControlDeviceDebugThreadStatus)
            {
                if (this.IsDebugStatus)
                {
                    DebugDmxData data = null;
                    lock (this.DebugDmxDataQueue)
                    {
                        data = DebugDmxDataQueue.Dequeue();
                    }
                    if (data != null)
                    {
                        this.RealTimeDebugging(data);
                        //耗时等待
                        long beforTime = DateTime.Now.Ticks;
                        while (true)
                        {
                            if ((DateTime.Now.Ticks - beforTime) / 10000 > (10))
                            {
                                break;
                            }
                            Thread.Sleep(0);
                        }
                    }
                }
                Thread.Sleep(0);
            }
        }
        /// <summary>
        /// 功能：修改存储文件路径
        /// </summary>
        /// <param name="dirPath">新的文件存储路径</param>
        public void SetRecordFilePath(string filePath)
        {
            this.RecordFilePath = filePath;
            DirectoryInfo directoryInfo = Directory.GetParent(filePath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            if (!File.Exists(this.RecordFilePath))
            {
                File.Create(this.RecordFilePath).Dispose();
            }
            List<byte> emptyData = new List<byte>();
            for (int i = 0; i < 35; i++)
            {
                emptyData.Add(0x00);
            }
            FileUtils.GetInstance().WriteToFileByCreate(emptyData, RecordFilePath);
        }
        /// <summary>
        /// 功能：启动录制
        /// </summary>
        public void StartRecord()
        {
            this.RecordFrameCount = 0;
            if (!File.Exists(this.RecordFilePath))
            {
                File.Create(this.RecordFilePath).Dispose();
            }
            List<byte> emptyData = new List<byte>();
            for (int i = 0; i < 35; i++)
            {
                emptyData.Add(0x00);
            }
            FileUtils.GetInstance().WriteToFileByCreate(emptyData, RecordFilePath);
            this.IsRecordStatus = true;

        }
        /// <summary>
        /// 功能：关闭录制
        /// </summary>
        public void StopRecord()
        {
            this.IsRecordStatus = false;
        }
        /// <summary>
        /// 功能：录制线程执行任务
        /// </summary>
        /// <param name="obj"></param>
        private void ControlDeviceRecordStart(Object obj)
        {
            this.RecordFrameCount = 0;
            while (this.ControlDeviceRecordThreadStatus)
            {
                if (this.IsRecordStatus)
                {
                    lock (this.RecordDmxDataQueue)
                    {
                        RecodeDmxData recodeDnxData = this.RecordDmxDataQueue.Dequeue();
                        if (recodeDnxData != null)
                        {
                            List<byte> head = new List<byte>
                            {
                                Convert.ToByte(recodeDnxData.ControlDevice.Led_interface_num),
                                Convert.ToByte(recodeDnxData.ControlDevice.Led_space),
                                Convert.ToByte(recodeDnxData.FrameIntervalTime & 0xFF)
                            };
                            for (int interficeIndex = 0; interficeIndex < recodeDnxData.ControlDevice.Led_interface_num; interficeIndex++)
                            {
                                int dataLength = 0;
                                for (int ledSpaceIndex = 0; ledSpaceIndex < recodeDnxData.ControlDevice.Led_space; ledSpaceIndex++)
                                {
                                    int ledSpaceNumber = this.StartLedSpace + interficeIndex * recodeDnxData.ControlDevice.Led_space + ledSpaceIndex;
                                    dataLength += recodeDnxData.VirtualControlDeviceDmxDatas[ledSpaceNumber].Count;
                                }
                                head.Add(Convert.ToByte(dataLength & 0xFF));
                                head.Add(Convert.ToByte((dataLength >> 8) & 0xFF));
                                head.Add(Convert.ToByte((dataLength >> 16) & 0xFF));
                                head.Add(Convert.ToByte((dataLength >> 24) & 0xFF));
                            }
                            FileUtils.GetInstance().WriteToFileBySeek(head, RecordFilePath, 0);
                            List<byte> framData = new List<byte>();
                            for (int interficeIndex = 0; interficeIndex < recodeDnxData.ControlDevice.Led_interface_num; interficeIndex++)
                            {
                                List<byte> routeDatas = new List<byte>();
                                for (int ledSpaceIndex = 0; ledSpaceIndex < recodeDnxData.ControlDevice.Led_space; ledSpaceIndex++)
                                {
                                    int ledSpaceNumber = this.StartLedSpace + interficeIndex * recodeDnxData.ControlDevice.Led_space + ledSpaceIndex;
                                    if (recodeDnxData.VirtualControlDeviceDmxDataResponseStatus[ledSpaceNumber] || recodeDnxData.VirtualControlDeviceDmxDatas[ledSpaceNumber].Count > 0)
                                    {
                                        routeDatas.AddRange(recodeDnxData.VirtualControlDeviceDmxDatas[ledSpaceNumber]);
                                    }
                                }
                                framData.AddRange(routeDatas);
                            }
                            FileUtils.GetInstance().WriteToFile(framData, RecordFilePath);
                            this.RecordFrameCount++;
                            this.RecordFrameCountResponse_Event?.Invoke(this.RecordFrameCount);
                            //LogTools.Debug(Constant.TAG_XIAOSA, "已录制" + this.RecodeFrameCount + "帧");
                        }
                    }
                    //锁的结束
                }
            }
        }
        /// <summary>
        /// 功能：实时调试模块
        /// </summary>
        /// <param name="debugDmxData"></param>
        private void RealTimeDebugging(DebugDmxData debugDmxData)
        {
            Dictionary<int, List<byte>> dmxData = debugDmxData.VirtualControlDeviceDmxDatas;
            List<byte> sendBuff = new List<byte>();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(debugDmxData.ControlDevice.IP), debugDmxData.ControlDevice.LinkPort);
            for (int i = 0; i < debugDmxData.ControlDevice.Led_interface_num; i++)
            {
                sendBuff.Clear();
                List<byte> data = new List<byte>();
                for (int j = 0; j < debugDmxData.ControlDevice.Led_space; j++)
                {
                    int index = this.StartLedSpace + i * debugDmxData.ControlDevice.Led_space + j;
                    data.AddRange(dmxData[index]);
                }
                if (data.Count == 0)
                {
                    sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2), 0x00, 0x00 });
                    sendBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 1024));
                    this.ControlDeviceUdpSend.SendTo(sendBuff.ToArray(), iPEndPoint);

                    sendBuff.Clear();
                    sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2 + 1), 0x00, 0x00 });
                    sendBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 1024));
                    this.ControlDeviceUdpSend.SendTo(sendBuff.ToArray(), iPEndPoint);
                }
                else
                {
                    if (data.Count > 1024)
                    {
                        sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2), Convert.ToByte((1024 >> 8) & 0xFF), Convert.ToByte(1024 & 0xFF) });
                        sendBuff.AddRange(data.Take(1024).ToList());
                        this.ControlDeviceUdpSend.SendTo(sendBuff.ToArray(), iPEndPoint);

                        sendBuff.Clear();
                        sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2 + 1), Convert.ToByte(((data.Count - 1024) >> 8) & 0xFF), Convert.ToByte((data.Count - 1024) & 0xFF) });
                        sendBuff.AddRange(data.Skip(1024).Take(1024).ToList());
                        sendBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), (1024) - sendBuff.Count + 7));
                        this.ControlDeviceUdpSend.SendTo(sendBuff.ToArray(), iPEndPoint);
                    }
                    else
                    {
                        sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2), Convert.ToByte((data.Count >> 8) & 0xFF), Convert.ToByte(data.Count & 0xFF) });
                        sendBuff.AddRange(data);
                        sendBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), (1024) - sendBuff.Count + 7));
                        this.ControlDeviceUdpSend.SendTo(sendBuff.ToArray(), iPEndPoint);

                        sendBuff.Clear();
                        sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2 + 1), 0x00, 0x00 });
                        sendBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 1024));
                        this.ControlDeviceUdpSend.SendTo(sendBuff.ToArray(), iPEndPoint);
                    }
                }
                if ((i + 1) % 3 == 0)
                {
                    Thread.Sleep(3);
                }
            }
            this.DebugFrameCount++;
            this.DebugFrameCountResponse_Event?.Invoke(this.DebugFrameCount);
        }
        /// <summary>
        /// 功能：启动接收DMX数据
        /// </summary>
        public void StartReceiveDMXData()
        {
            this.IsStartResponseDmxDataStatus = true;
        }
        /// <summary>
        /// 功能：关闭接收DMX数据
        /// </summary>
        public void StopReceiveDMXData()
        {
            this.IsStartResponseDmxDataStatus = false;
        }
        /// <summary>
        /// 判断Ip是否为该虚拟控制器
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool IsThisDevice(string ip)
        {
            if (ip != null )
            {
                if (ip.Equals(this.ControlDevice.IP))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 设置读取录制帧数委托事件
        /// </summary>
        /// <param name="recordFrameCountResponse"></param>
        public void SetRecordFrameCountResponse(RecordFrameCountResponse recordFrameCountResponse)
        {
            this.RecordFrameCountResponse_Event = recordFrameCountResponse;
        }
        /// <summary>
        /// 设置实时调试帧数委托事件
        /// </summary>
        /// <param name="debugFrameCountResponse"></param>
        public void SetDebugFrameCountResponse(DebugFrameCountResponse debugFrameCountResponse)
        {
            this.DebugFrameCountResponse_Event = debugFrameCountResponse;
        }
    }
}
