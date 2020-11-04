using MultiLedController.multidevice.multidevicepromax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;

namespace MultiLedController.multidevice.newmultidevice
{
    //分控数1-5，用户自主选择
    //路数可选，1、2、4、8 四种选项
    //点数可选（512/170）= 3 、(1024/70) = 6  两种

    public class NewVirtualDevice
    {
        [DllImport("winmm.dll")] internal static extern uint timeBeginPeriod(uint period);
        [DllImport("winmm.dll")] internal static extern uint timeEndPeriod(uint period);

        private const int THREAD_SLEEP_TIME = 1;
        private const int PORT = 4115;
        private const int SHOW_FRAME_COUNT_INTERVALTIME = 100;

        private static byte[] PACKAGE_END = new byte[] { 0x00, 0x00, 0x78, 0x78 };

        private Object SYNCHROLOCK_KEY;

        private int LedInterfaceNumber { get; set; }
        private int LedSpaceNumber { get; set; }
        private int ControlNumber { get; set; }
        private int LastFramePacketSequence { get; set; }
        private int DebugFramCount { get; set; }
        private int RecordFramCount { get; set; }

        private string ServerIP { get; set; }
        private string LocalIP { get; set; }
        private string FilePath { get; set; }
        private string ConfigPath { get; set; }

        private List<string> IPs { get; set; }
        private List<NewVirtualClient> Clients { get; set; }

        private Dictionary<int,List<byte>> SpaceDMXDatas { get; set; }
        private Dictionary<int,bool> SpaceDMXDataResponseStatus { get; set; }

        private Thread DebugDMXTask { get; set; }
        private Thread RecordDMXTask { get; set; }
        private Thread Receive { get; set; }
        private bool DebugDMXTaskStatus { get; set; }
        private bool RecordDMXTaskStatus { get; set; }
        private bool IsDebugDMXData { get; set; }
        private bool IsRecordDMXData { get; set; }
        private bool IsResponseDMXDatasStatus { get;set; }
        private bool IsFirstFrame { get; set; }
        private bool ReceiveStatus { get; set; }
        private bool IsFirstFrameByRecord { get; set; }

        private Queue<Dictionary<int, List<byte>>> DebugDMXDataQueue { get; set; }
        private Queue<Dictionary<int, List<byte>>> RecordDMXDataQueue { get; set; }

        private long LastFrameResponseTime { get; set; }

        private Socket Send { get; set; }
        private UdpClient Client { get; set; }

        private System.Timers.Timer ShowFrameCountTask { get; set; }

        public delegate void GetDebugFramCount(int framCount);
        public delegate void GetRecordFramCount(int framCount);

        private GetDebugFramCount GetDebugFramCount_Event { get; set; }
        private GetRecordFramCount GetRecordFramCount_Event { get; set; }

        public void Test()
        {
            //this.IsResponseDMXDatasStatus = true;
            //this.StartDebug();
            //this.StartRecord(@"C:\Users\99729\Desktop\Test\Test\RecordTest.bin", @"C:\Users\99729\Desktop\Test\Test\Config.bin");

        }

        /// <summary>
        /// 数据接收以及发送
        /// </summary>
        /// <param name="ledInterfaceNumber">分控路数</param>
        /// <param name="virtualIPs">虚拟IP集</param>
        /// <param name="ledSpaceNumber">每一路占用空间数</param>
        /// <param name="controlNumber">分控数</param>
        /// <param name="serverIP">服务器IP</param>
        public NewVirtualDevice(int ledInterfaceNumber,List<string> virtualIPs,int ledSpaceNumber,int controlNumber,string serverIP,string localIP)
        {
            this.LedInterfaceNumber = ledInterfaceNumber;
            this.IPs = virtualIPs;
            this.LedSpaceNumber = ledSpaceNumber;
            this.ControlNumber = controlNumber;
            this.ServerIP = serverIP;
            this.LocalIP = localIP;
            this.InitParam();
            this.InitSocket();
            this.SetVirtualClients();
            this.ShowFrameCountTask = new System.Timers.Timer(SHOW_FRAME_COUNT_INTERVALTIME)
            {
                AutoReset = true,
            };
            this.ShowFrameCountTask.Elapsed += this.ShowFrameCount;
            this.StartShowFrameCountTask();
        }

        private void StartShowFrameCountTask()
        {
            this.ShowFrameCountTask.Start();
        }

        private void StopShowFrameCountTask()
        {
            this.ShowFrameCountTask.Stop();
        }

        private void InitSocket()
        {
            try
            {
                this.Send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.Send.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                this.Send.Bind(new IPEndPoint(IPAddress.Parse(this.LocalIP), PORT));
                this.Client = new UdpClient() { Client = this.Send };
                this.Receive = new Thread(this.ReceiveEvent) { IsBackground = true };
                this.ReceiveStatus = true;
                this.Receive.Start(this.Client);
            }
            catch (Exception ex)
            {
                Console.WriteLine("虚拟服务器初始化网络报错");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            
        }

        /// <summary>
        /// 配置虚拟客户端
        /// </summary>
        private void SetVirtualClients()
        {
            try
            {
                int startSpaceNumber = 0;
                for (int index = 0; index < this.IPs.Count; index++)
                {
                    if (this.ControlNumber * LedSpaceNumber * LedInterfaceNumber - startSpaceNumber < 4)
                    {
                        int spaceNumber = this.ControlNumber * LedSpaceNumber * LedInterfaceNumber - startSpaceNumber;
                        NewVirtualClient client = new NewVirtualClient(this.ServerIP, this.IPs[index], startSpaceNumber, spaceNumber, this.VirtualClentsDMXResponse);
                        for (int spaceIndex = 0; spaceIndex < spaceNumber; spaceIndex++)
                        {
                            this.SpaceDMXDatas[startSpaceNumber + spaceIndex] = new List<byte>();
                            this.SpaceDMXDataResponseStatus[startSpaceNumber + spaceIndex] = false;
                        }
                        startSpaceNumber += spaceNumber;

                        client.index = this.Clients.Count + 1;

                        this.Clients.Add(client);
                    }
                    else
                    {
                        NewVirtualClient client = new NewVirtualClient(this.ServerIP, this.IPs[index], startSpaceNumber, 4, this.VirtualClentsDMXResponse);
                        for (int spaceIndex = 0; spaceIndex < 4; spaceIndex++)
                        {
                            this.SpaceDMXDatas[startSpaceNumber + spaceIndex] = new List<byte>();
                            this.SpaceDMXDataResponseStatus[startSpaceNumber + spaceIndex] = false;
                        }
                        startSpaceNumber += 4;

                        client.index = this.Clients.Count + 1;

                        this.Clients.Add(client);
                    }
                }
                Console.WriteLine("设备添加成功，添加设备数量：" + this.Clients.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("虚拟服务器报错");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// 配置初始化参数
        /// </summary>
        private void InitParam()
        {
            try
            {
                this.SpaceDMXDatas = new Dictionary<int, List<byte>>();
                this.SpaceDMXDataResponseStatus = new Dictionary<int, bool>();
                this.DebugDMXDataQueue = new Queue<Dictionary<int, List<byte>>>();
                this.RecordDMXDataQueue = new Queue<Dictionary<int, List<byte>>>();
                this.Clients = new List<NewVirtualClient>();
                this.LastFrameResponseTime = -1;
                this.LastFramePacketSequence = 0;
                this.DebugFramCount = 0;
                this.RecordFramCount = 0;
                this.SYNCHROLOCK_KEY = new object();
                this.IsResponseDMXDatasStatus = false;
                this.DebugDMXTaskStatus = false;
                this.RecordDMXTaskStatus = false;
                this.ReceiveStatus = false;
                this.IsDebugDMXData = false;
                this.IsRecordDMXData = false;
                this.IsFirstFrameByRecord = true;
                this.IsFirstFrame = true;
                this.DebugDMXTask = new Thread(this.DebugDMXDataTask) { IsBackground = true };
                this.RecordDMXTask = new Thread(this.RecordDMXDataTask) { IsBackground = true };
                this.DebugDMXTaskStatus = true;
                this.RecordDMXTaskStatus = true;
                this.DebugDMXTask.Start();
                this.RecordDMXTask.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("虚拟服务器初始化参数报错");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
          
        }

        public bool IsRunning()
        {
            return this.Send != null;
        }

        public void Close()
        {
            try
            {
                this.StopShowFrameCountTask();
                this.IsResponseDMXDatasStatus = false;
                this.ReceiveStatus = false;
                this.IsDebugDMXData = false;
                this.IsRecordDMXData = false;
                this.DebugDMXTaskStatus = false;
                this.RecordDMXTaskStatus = false;
                Thread.Sleep(100);
                this.IsFirstFrame = true;
                this.IsFirstFrameByRecord = true;
                foreach (NewVirtualClient client in this.Clients)
                {
                    client.Close();
                }

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
            catch (Exception ex)
            {
                Console.WriteLine("关闭虚拟服务器报错");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            
        }

        public NewVirtualDevice StartResponseDMXData()
        {
            this.IsResponseDMXDatasStatus = true;
            return this;
        }

        public NewVirtualDevice StopResponseDMXData()
        {
            this.IsResponseDMXDatasStatus = false;
            return this;
        }

        public NewVirtualDevice StartDebug(GetDebugFramCount getDebugFramCount)
        {
            this.IsDebugDMXData = true;
            this.DebugFramCount = 0;
            this.GetDebugFramCount_Event = getDebugFramCount;
            //VirtualProClientsManager.Test();
            return this;
        }

        public NewVirtualDevice StopDebug()
        {
            try
            {
                this.IsDebugDMXData = false;
                this.GetDebugFramCount_Event = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("关闭调试报错");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return this;
        }

        public NewVirtualDevice StartRecord(string filePath,string config, GetRecordFramCount getRecordFramCount)
        {
            try
            {
                this.RecordFramCount = 0;
                this.GetRecordFramCount_Event = getRecordFramCount;
                this.FilePath = filePath;
                this.ConfigPath = config;
                this.IsFirstFrameByRecord = true;
                this.IsRecordDMXData = true;
                DirectoryInfo paraentDir = Directory.GetParent(this.FilePath);
                if (!paraentDir.Exists)
                {
                    paraentDir.Create();
                }
                paraentDir = Directory.GetParent(this.ConfigPath);
                if (!paraentDir.Exists)
                {
                    paraentDir.Create();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("启动录制报错");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
           
            return this;
        }

        public NewVirtualDevice StopRecord()
        {
            try
            {
                this.IsRecordDMXData = false;
                this.GetRecordFramCount_Event = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("关闭录制报错");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return this;
        }

        private void ReceiveEvent(Object obj)
        {
            UdpClient client = obj as UdpClient;
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, PORT);
            while (this.ReceiveStatus)
            {
                try
                {
                    byte[] receiveBuff = client.Receive(ref iPEndPoint);
                }
                catch (Exception)
                {
                    Console.WriteLine("关闭Socket");
                }
            }
        }

        private void VirtualClentsDMXResponse(int spaceNumber,List<byte> dmxData)
        {
            try
            {
                lock (this.SYNCHROLOCK_KEY)
                {
                    if (this.IsResponseDMXDatasStatus)
                    {
                        long frameIntervalTime = 0;
                        if (-1 == this.LastFrameResponseTime)
                        {
                            this.LastFrameResponseTime = DateTime.Now.Ticks;
                        }
                        else
                        {
                            long nowTime = DateTime.Now.Ticks;
                            frameIntervalTime = (nowTime - this.LastFrameResponseTime) / 10000;
                            this.LastFrameResponseTime = nowTime;
                        }
                        if (frameIntervalTime > 5 && this.IsFirstFrame)
                        {
                            this.IsFirstFrame = false;
                            foreach (int key in this.SpaceDMXDataResponseStatus.Keys)
                            {
                                if (this.SpaceDMXDataResponseStatus[key])
                                {
                                    this.LastFramePacketSequence += key;
                                }
                            }
                        }
                        else if (frameIntervalTime > 5 && !this.IsFirstFrame)
                        {
                            int nowPacketSequence = 0;
                            foreach (int key in this.SpaceDMXDataResponseStatus.Keys)
                            {
                                if (this.SpaceDMXDataResponseStatus[key])
                                {
                                    nowPacketSequence += key;
                                }
                            }
                            if (nowPacketSequence == this.LastFramePacketSequence)
                            {
                                bool flag = true;
                                var keyList = this.SpaceDMXDatas.Keys;
                                //foreach (int key in keyList)
                                //{
                                //    if (this.SpaceDMXDatas[key].Count > 510)
                                //    {
                                //        flag = false;
                                //    }
                                //}
                                if (this.IsDebugDMXData && flag)
                                {
                                    lock (this.DebugDMXDataQueue)
                                    {
                                        this.DebugDMXDataQueue.Enqueue(this.SpaceDMXDatas);
                                    }
                                }
                                if (this.IsRecordDMXData && flag)
                                {
                                    lock (this.RecordDMXDataQueue)
                                    {
                                        this.RecordDMXDataQueue.Enqueue(this.SpaceDMXDatas);
                                    }
                                }
                                List<int> keys = this.SpaceDMXDatas.Keys.ToList();
                                this.SpaceDMXDatas = new Dictionary<int, List<byte>>();
                                this.SpaceDMXDataResponseStatus = new Dictionary<int, bool>();
                                foreach (int key in keys)
                                {
                                    this.SpaceDMXDatas.Add(key, new List<byte>());
                                    this.SpaceDMXDataResponseStatus.Add(key, false);
                                }
                            }
                            this.LastFramePacketSequence = nowPacketSequence;
                        }
                        this.SpaceDMXDatas[spaceNumber] = dmxData;
                        this.SpaceDMXDataResponseStatus[spaceNumber] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("接收DMX数据报错");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void DebugDMXDataTask(Object obj)
        {
            while (this.DebugDMXTaskStatus)
            {
                try
                {
                    if (this.IsDebugDMXData)
                    {
                        int queueCount = 0;
                        lock (this.DebugDMXDataQueue)
                        {
                            queueCount = this.DebugDMXDataQueue.Count;
                        }
                        if (queueCount > 0)
                        {
                            lock (this.DebugDMXDataQueue)
                            {
                                try
                                {
                                    this.DebugDMXDataEvent(this.DebugDMXDataQueue.Dequeue());
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("实时调试报错1");
                                    Console.WriteLine(ex.Message);
                                    Console.WriteLine(ex.StackTrace);
                                }
                            }
                        }
                        else
                        {
                            timeBeginPeriod(1);
                            Thread.Sleep(THREAD_SLEEP_TIME);
                            timeEndPeriod(1);
                        }
                    }
                    else
                    {
                        timeBeginPeriod(1);
                        Thread.Sleep(THREAD_SLEEP_TIME);
                        timeEndPeriod(1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("实时调试报错2");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private void RecordDMXDataTask(Object obj)
        {
            while (this.RecordDMXTaskStatus)
            {
                try
                {
                    if (this.IsRecordDMXData)
                    {
                        int queueCount = 0;
                        lock (this.RecordDMXDataQueue)
                        {
                            queueCount = this.RecordDMXDataQueue.Count;
                        }
                        if (queueCount > 0)
                        {
                            lock (this.RecordDMXDataQueue)
                            {
                                this.RecordDMXDataEvent(this.RecordDMXDataQueue.Dequeue());
                            }
                        }
                        else
                        {
                            timeBeginPeriod(1);
                            Thread.Sleep(THREAD_SLEEP_TIME);
                            timeEndPeriod(1);
                        }
                    }
                    else
                    {
                        timeBeginPeriod(1);
                        Thread.Sleep(THREAD_SLEEP_TIME);
                        timeEndPeriod(1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("录制任务执行报错");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private void DebugDMXDataEvent(Dictionary<int, List<byte>> dmxData)
        {
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Broadcast, PORT);
            Dictionary<int, Queue<List<byte>>> dataBuff = new Dictionary<int, Queue<List<byte>>>();
            Dictionary<int, Dictionary<int, List<byte>>> dmxDataBuff = new Dictionary<int, Dictionary<int, List<byte>>>();
            Dictionary<int, Stack<byte>> ledInterfaceDMXDatas = new Dictionary<int, Stack<byte>>();
            try
            {
                #region 数据整理
                for (int controlIndex = 0; controlIndex < this.ControlNumber; controlIndex++)
                {
                    int controlNo = controlIndex + 1;
                    int ledInterfaceNo = 1;
                    dmxDataBuff.Add(controlNo, new Dictionary<int, List<byte>>());
                    for (int spaceIndex = controlIndex * this.LedInterfaceNumber * this.LedSpaceNumber; spaceIndex < controlIndex * this.LedInterfaceNumber * this.LedSpaceNumber + this.LedInterfaceNumber * this.LedSpaceNumber; spaceIndex += this.LedSpaceNumber)
                    {
                        dmxDataBuff[controlNo].Add(ledInterfaceNo, new List<byte>());
                        for (int index = 0; index < this.LedSpaceNumber; index++)
                        {
                            dmxDataBuff[controlNo][ledInterfaceNo].AddRange(dmxData[spaceIndex + index]);
                            if (dmxData[spaceIndex + index].Count < 510)
                            {
                                dmxDataBuff[controlNo][ledInterfaceNo].AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 510 - dmxData[spaceIndex + index].Count).ToArray());
                            }
                        }
                        if (dmxDataBuff[controlNo][ledInterfaceNo].Count < (512 * 6))
                        {
                            dmxDataBuff[controlNo][ledInterfaceNo].AddRange(Enumerable.Repeat(Convert.ToByte(0x00), (512 * 6) - dmxDataBuff[controlNo][ledInterfaceNo].Count).ToArray());
                        }
                        ledInterfaceNo++;
                    }
                }
                for (int controlIndex = 0; controlIndex < this.ControlNumber; controlIndex++)
                {
                    int controlNo = controlIndex + 1;
                    ledInterfaceDMXDatas.Add(controlNo, new Stack<byte>());
                    for (int dataIndex = 0; dataIndex < this.LedSpaceNumber * 512; dataIndex += 3)
                    {
                        //R
                        for (int intefaceIndex = 0; intefaceIndex < this.LedInterfaceNumber; intefaceIndex++)
                        {
                            byte value  = dmxDataBuff[controlNo][intefaceIndex + 1][dataIndex];
                            ledInterfaceDMXDatas[controlNo].Push(value);
                        }
                        //G
                        for (int intefaceIndex = 0; intefaceIndex < this.LedInterfaceNumber; intefaceIndex++)
                        {
                            byte value = dmxDataBuff[controlNo][intefaceIndex + 1][dataIndex + 1];
                            ledInterfaceDMXDatas[controlNo].Push(value);
                        }
                        //B
                        for (int intefaceIndex = 0; intefaceIndex < this.LedInterfaceNumber; intefaceIndex++)
                        {
                            byte value  = dmxDataBuff[controlNo][intefaceIndex + 1][dataIndex + 2];
                            ledInterfaceDMXDatas[controlNo].Push(value);
                        }
                    }
                }
                #endregion
                #region 数据分包
                int packageSize = 1024;
                for (int controlIndex = 0; controlIndex < this.ControlNumber; controlIndex++)
                {
                    int controlNo = controlIndex + 1;
                    int packageIndex = 1;
                    dataBuff.Add(controlNo, new Queue<List<byte>>());
                    List<byte> packageData = new List<byte>();
                    int seek = 0;
                    packageData.AddRange(new byte[] { 0x00, Convert.ToByte(controlNo), 0x88, 0x77, Convert.ToByte(seek & 0xFF), Convert.ToByte((seek >> 8) & 0xFF), 0xF8, 0x03 });
                    while (ledInterfaceDMXDatas[controlNo].Count > 0)
                    {
                        packageData.Add(ledInterfaceDMXDatas[controlNo].Pop());
                        if (packageData.Count == packageSize)
                        {
                            dataBuff[controlNo].Enqueue(packageData);
                            packageIndex++;
                            seek = (packageIndex - 1) * 1016;
                            packageData = new List<byte>();
                            if (ledInterfaceDMXDatas[controlNo].Count >= packageSize)
                            {
                                packageData.AddRange(new byte[] { 0x00, Convert.ToByte(controlNo), 0x88, 0x77, Convert.ToByte(seek & 0xFF), Convert.ToByte((seek >> 8) & 0xFF), 0xF8, 0x03 });
                            }
                            else
                            {
                                packageData.AddRange(new byte[] { 0x00, Convert.ToByte(controlNo), 0x88, 0x77, Convert.ToByte(seek & 0xFF), Convert.ToByte((seek >> 8) & 0xFF), Convert.ToByte(ledInterfaceDMXDatas[controlNo].Count & 0xFF), Convert.ToByte((ledInterfaceDMXDatas[controlNo].Count >> 8) & 0xFF) });
                            }
                        }
                    }
                    packageData.AddRange(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
                    dataBuff[controlNo].Enqueue(packageData);
                }
                #endregion
                #region 发包
                for (int controlIndex = 0; controlIndex < this.ControlNumber; controlIndex++)
                {
                    int controlNo = controlIndex + 1;
                    byte[] head = new byte[] { Convert.ToByte((controlNo >> 8) & 0xFF), Convert.ToByte(controlNo & 0xFF), 0x33, 0x44 };
                    this.Send.SendTo(head, iPEnd);
                    while (dataBuff[controlNo].Count > 0)
                    {
                        this.Send.SendTo(dataBuff[controlNo].Dequeue().ToArray(), iPEnd);
                        for (int i = 0; i < 5000; i++)
                        {
                            ;
                        }
                    }
                }
                this.Send.SendTo(PACKAGE_END.ToArray(), iPEnd);
                #endregion
                this.DebugFramCount++;
            }
            catch (Exception ex)
            {
                Console.WriteLine("实时调试发送报错");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void RecordDMXDataEvent(Dictionary<int, List<byte>> dmxData)
        {
            try
            {
                Dictionary<int, Queue<List<byte>>> dataBuff = new Dictionary<int, Queue<List<byte>>>();
                Dictionary<int, Dictionary<int, List<byte>>> dmxDataBuff = new Dictionary<int, Dictionary<int, List<byte>>>();
                Dictionary<int, Stack<byte>> ledInterfaceDMXDatas = new Dictionary<int, Stack<byte>>();
                #region 数据整理
                for (int controlIndex = 0; controlIndex < this.ControlNumber; controlIndex++)
                {
                    int controlNo = controlIndex + 1;
                    int ledInterfaceNo = 1;
                    dmxDataBuff.Add(controlNo, new Dictionary<int, List<byte>>());
                    for (int spaceIndex = controlIndex * this.LedInterfaceNumber * this.LedSpaceNumber; spaceIndex < controlIndex * this.LedInterfaceNumber * this.LedSpaceNumber + this.LedInterfaceNumber * this.LedSpaceNumber; spaceIndex += this.LedSpaceNumber)
                    {
                        dmxDataBuff[controlNo].Add(ledInterfaceNo, new List<byte>());
                        for (int index = 0; index < this.LedSpaceNumber; index++)
                        {
                            dmxDataBuff[controlNo][ledInterfaceNo].AddRange(dmxData[spaceIndex + index]);
                            //if (dmxData[spaceIndex + index].Count > 510)
                            //{
                            //    Console.WriteLine("超过510");
                            //}
                        }
                        ledInterfaceNo++;
                    }
                }
                for (int controlIndex = 0; controlIndex < this.ControlNumber; controlIndex++)
                {
                    int controlNo = controlIndex + 1;
                    ledInterfaceDMXDatas.Add(controlNo, new Stack<byte>());
                    int maxLength = 0;
                    for (int index = 0; index < this.LedInterfaceNumber; index++)
                    {
                        maxLength = maxLength > dmxDataBuff[controlNo][index + 1].Count ? maxLength : dmxDataBuff[controlNo][index + 1].Count;
                    }
                    int count = maxLength % 3 == 0 ? 3 : 4;
                    for (int dataIndex = 0; dataIndex < maxLength; dataIndex += count)
                    {
                        //R
                        for (int intefaceIndex = 0; intefaceIndex < this.LedInterfaceNumber; intefaceIndex++)
                        {
                            byte value;
                            if (dataIndex < dmxDataBuff[controlNo][intefaceIndex + 1].Count)
                            {
                                value = dmxDataBuff[controlNo][intefaceIndex + 1][dataIndex];
                            }
                            else
                            {
                                value = 0x00;
                            }
                            ledInterfaceDMXDatas[controlNo].Push(value);
                        }
                        //G
                        for (int intefaceIndex = 0; intefaceIndex < this.LedInterfaceNumber; intefaceIndex++)
                        {
                            try
                            {
                                byte value;
                                if (dataIndex + 1 < dmxDataBuff[controlNo][intefaceIndex + 1].Count)
                                {
                                    value = dmxDataBuff[controlNo][intefaceIndex + 1][dataIndex + 1];
                                }
                                else
                                {
                                    value = 0x00;
                                }
                                ledInterfaceDMXDatas[controlNo].Push(value);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.WriteLine(ex.StackTrace);
                            }
                           
                        }
                        //B
                        for (int intefaceIndex = 0; intefaceIndex < this.LedInterfaceNumber; intefaceIndex++)
                        {
                            byte value;
                            if (dataIndex + 2 < dmxDataBuff[controlNo][intefaceIndex + 1].Count)
                            {
                                value = dmxDataBuff[controlNo][intefaceIndex + 1][dataIndex + 2];
                            }
                            else
                            {
                                value = 0x00;
                            }
                            ledInterfaceDMXDatas[controlNo].Push(value);
                        }
                        if (count == 4)
                        {
                            //W
                            for (int intefaceIndex = 0; intefaceIndex < this.LedInterfaceNumber; intefaceIndex++)
                            {
                                byte value;
                                if (dataIndex + 3 < dmxDataBuff[controlNo][intefaceIndex + 1].Count)
                                {
                                    value = dmxDataBuff[controlNo][intefaceIndex + 1][dataIndex + 3];
                                }
                                else
                                {
                                    value = 0x00;
                                }
                                ledInterfaceDMXDatas[controlNo].Push(value);
                            }
                        }
                    }
                }
                #endregion
                #region 写参数包
                FileStream stream;
                int frameDataLength = 0;
                if (this.IsFirstFrameByRecord)
                {
                    this.IsFirstFrameByRecord = false;
                    this.CreateConfigFile(dmxData);
                    byte[] paramPackage = Enumerable.Repeat(Convert.ToByte(0x00), 512).ToArray();
                    if (File.Exists(this.FilePath))
                    {
                        File.Delete(this.FilePath);
                    }
                    using (stream = new FileStream(FilePath, FileMode.CreateNew))
                    {
                        stream.Write(paramPackage, 0, paramPackage.Length);
                    }
                }
                #endregion
                #region 写分控数据包
                for (int controlIndex = 0; controlIndex < this.ControlNumber; controlIndex++)
                {
                    int controlNo = controlIndex + 1;
                    List<byte> buff = new List<byte>();
                    if (ledInterfaceDMXDatas[controlNo].Count % 24 != 0)
                    {
                        buff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), (24 - (ledInterfaceDMXDatas[controlNo].Count % 24))).ToArray());
                    }
                    int dataSize = ledInterfaceDMXDatas[controlNo].Count;
                    for (int index = 0; index < dataSize; index++)
                    {
                        buff.Add(ledInterfaceDMXDatas[controlNo].Pop());
                    }
                    frameDataLength += buff.Count;
                    using (stream = new FileStream(FilePath, FileMode.Append))
                    {
                        stream.Write(buff.ToArray(), 0, buff.Count);
                    }
                }
                if (frameDataLength % 512 != 0)
                {
                    byte[] emptyData = Enumerable.Repeat(Convert.ToByte(0x00), (512 - (frameDataLength % 512))).ToArray();
                    using (stream = new FileStream(FilePath, FileMode.Append))
                    {
                        stream.Write(emptyData.ToArray(), 0, emptyData.Length);
                    }
                }
                #endregion
                this.RecordFramCount++;
            }
            catch (Exception ex)
            {
                Console.WriteLine("录制文件报错");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void CreateConfigFile(Dictionary<int, List<byte>> dmxData)
        {
            try
            {
                List<byte> buff = new List<byte>();
                int ledInterfaceCount = 0;
                buff.AddRange(new byte[] { 0x53, 0x54, 0x55, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                buff.AddRange(new byte[] { 0x06, 0x00, 0x08, 0x00 });
                buff.AddRange(new byte[] { 0x05, 0x00, 0x00, 0x00, 0x00, 0x00 });
                for (int ledInterfaceIndex = 0; ledInterfaceIndex < this.ControlNumber * this.LedInterfaceNumber; ledInterfaceIndex++)
                {
                    int ledInterfaceDataSize = 0;
                    for (int spaceIndex = 0; spaceIndex < this.LedSpaceNumber; spaceIndex++)
                    {
                        ledInterfaceDataSize += dmxData[ledInterfaceIndex * this.LedSpaceNumber + spaceIndex].Count;
                    }
                    if (ledInterfaceDataSize > 0)
                    {
                        ledInterfaceCount = ledInterfaceIndex + 1;
                    }
                }
                buff.AddRange(new byte[] {  Convert.ToByte((ledInterfaceCount) & 0xFF),
                                        Convert.ToByte(((ledInterfaceCount)>> 8 ) & 0xFF),
                                        Convert.ToByte(((ledInterfaceCount) >> 16) & 0xFF),
                                        Convert.ToByte(((ledInterfaceCount) >> 24) & 0xFF) });
                for (int ledInterfaceIndex = 0; ledInterfaceIndex < this.ControlNumber * this.LedInterfaceNumber; ledInterfaceIndex++)
                {
                    if (ledInterfaceIndex == ledInterfaceCount)
                    {
                        break;
                    }
                    List<byte> ledInterfaceBuff = new List<byte>();
                    for (int spaceIndex = 0; spaceIndex < this.LedSpaceNumber; spaceIndex++)
                    {
                        int spaceNo = ledInterfaceIndex * this.LedSpaceNumber + spaceIndex;
                        ledInterfaceBuff.AddRange(dmxData[spaceNo]);
                    }
                    int ledInterfaceDataLength = ledInterfaceBuff.Count % 3 == 0 ? ledInterfaceBuff.Count / 3 : ledInterfaceBuff.Count / 4;
                    buff.AddRange(new byte[] {  Convert.ToByte(ledInterfaceDataLength & 0xFF),
                                        Convert.ToByte((ledInterfaceDataLength>> 8 ) & 0xFF),
                                        Convert.ToByte((ledInterfaceDataLength >> 16) & 0xFF),
                                        Convert.ToByte((ledInterfaceDataLength >> 24) & 0xFF) });
                    if (ledInterfaceBuff.Count > 0)
                    {
                        for (int index = 0; index < ledInterfaceDataLength; index++)
                        {
                            buff.Add(Convert.ToByte(index & 0xFF));
                            buff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 3).ToArray());
                        }
                    }
                }
                buff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 4).ToArray());
                buff.AddRange(Enumerable.Repeat(Convert.ToByte(0xFF), 64).ToArray());
                buff.Add(Convert.ToByte(this.LedInterfaceNumber));
                if (File.Exists(this.ConfigPath))
                {
                    File.Delete(this.ConfigPath);
                }
                using (FileStream fileStream = new FileStream(this.ConfigPath, FileMode.Create))
                {
                    fileStream.Write(buff.ToArray(), 0, buff.Count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("生成配置文件报错");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void ShowFrameCount(object sender, ElapsedEventArgs e)
        {
            if (this.IsRecordDMXData)
            {
                if (this.GetRecordFramCount_Event != null)
                {
                    this.GetRecordFramCount_Event(this.RecordFramCount);
                }
            }
            if (this.IsDebugDMXData)
            {
                if (this.GetDebugFramCount_Event != null)
                {
                    this.GetDebugFramCount_Event(this.DebugFramCount);
                }
            }
        }
    }
}
