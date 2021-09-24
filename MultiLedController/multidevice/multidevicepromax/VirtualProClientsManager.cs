using MultiLedController.xiaosa.madirx;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;

namespace MultiLedController.multidevice.multidevicepromax
{
    public class VirtualProClientsManager
    {
        [DllImport("winmm.dll")] internal static extern uint timeBeginPeriod(uint period);
        [DllImport("winmm.dll")] internal static extern uint timeEndPeriod(uint period);

        private const int THREAD_SLEEP_TIME = 1;
        private const int PORT = 4115;
        private const int SHOW_FRAME_COUNT_INTERVALTIME = 100;

        private static byte[] PACKAGE_END = new byte[] { 0x00, 0x00, 0x78, 0x78 };

        private Object SYNCHROLOCK_KEY { get; set; }

        private int LedInterfaceNumber { get; set; }
        private int LedSpaceNumber { get; set; }
        private int LedControlNumber { get; set; }
        private int LastFramePacketSequence { get; set; }
        private int DebugFrameCount { get; set; }
        private int RecordFrameCount { get; set; }

        private string ArtNetServerIP { get; set; }
        private string localIP { get; set; }
        private string FilePath { get; set; }
        private string ConfigPath { get; set; }
        private List<string> VirtualIPS { get; set; }
        private List<VirtualProClient> VirtualClients { get; set; }
        private bool IsRGB { get; set; }


        private ConcurrentDictionary<int, List<byte>> SpaceDmxData { get; set; }
        private ConcurrentDictionary<int, bool> SpaceDmxDataReceiveStatus { get; set; }

        private Thread DebugDmxTask { get; set; }
        private Thread RecordDmxTask { get; set; }
        private Thread LedServerReceive { get; set; }

        private bool DebugDmxTaskStatus { get; set; }
        private bool RecordDmxTaskStatus { get; set; }
        private bool IsDebugDmxData { get; set; }
        private bool IsRecordDmxData { get; set; }
        private bool IsFirstFrame { get; set; }
        private bool ReceiveStatus { get; set; }
        private bool IsFirstFrameByRecord { get; set; }
        private bool IsStartReceiveDmxDataStatus { get; set; }

        private ConcurrentQueue<ConcurrentDictionary<int, List<byte>>> DebugDmxDataQueue { get; set; }
        private ConcurrentQueue<ConcurrentDictionary<int, List<byte>>> RecordDmxDataQueue { get; set; }

        private long LastFrameReceiveTime { get; set; }
        private Socket DebugServer { get; set; }
        private UdpClient DebugServerReceiveClient { get; set; }

        private System.Timers.Timer ShowFrameCountTask { get; set; }

        public delegate void GetDebugFrameCount(int frameCount);
        public delegate void GetRecordFrameCount(int frameCount);

        private bool isSycnFirstFrame { get; set; }

        private GetDebugFrameCount GetDebugFrameCount_Event { get; set; }
        private GetRecordFrameCount GetRecordFrameCount_Event { get; set; }


        //MAIN
        public VirtualProClientsManager(string localIP, string artnetServerIP,List<String> virtualIP,int ledSpaceNumber,int ledInterfaceNumber,int ledControlNumber,int ledType)
        {
            switch (ledType)
            {
                case 1:
                    this.IsRGB = false;
                    break;
                case 0:
                default:
                    this.IsRGB = true;
                    break;
            }
            this.isSycnFirstFrame = false;
            this.localIP = localIP;
            this.ArtNetServerIP = artnetServerIP;
            this.VirtualIPS = virtualIP;
            this.LedSpaceNumber = ledSpaceNumber;
            this.LedInterfaceNumber = ledInterfaceNumber;
            this.LedControlNumber = ledControlNumber;
            this.Init();
            this.InitLedServer();
            //ArtNetClient.Build(virtualIP, ledSpaceNumber * ledInterfaceNumber * ledControlNumber, localIP, this.Manager, this.SyncDMXDataCache);
            int clientCount = this.LedControlNumber * this.LedInterfaceNumber * this.LedSpaceNumber / 256 + ((this.LedControlNumber * this.LedInterfaceNumber * this.LedSpaceNumber) % 256 == 0 ? 0 : 1);
            for (int clientIndex = 0; clientIndex < clientCount; clientIndex++)
            {
                int portCount = 0;
                if (clientIndex == clientCount - 1)
                {
                    portCount = this.LedControlNumber * this.LedInterfaceNumber * this.LedSpaceNumber - clientIndex * 256;
                }
                else
                {
                    portCount = 256;
                }
                this.VirtualClients.Add(VirtualProClient.Build(clientIndex, this.VirtualIPS[clientIndex], this.ArtNetServerIP, portCount, this.Manager, this.SyncDMXDataCache));
            }
            for (int i = 0; i < this.LedControlNumber * this.LedInterfaceNumber * this.LedSpaceNumber; i++)
            {
                this.SpaceDmxData.TryAdd(i, new List<byte>());
                this.SpaceDmxDataReceiveStatus.TryAdd(i, false);
            }
        }

        private void Init()
        {
            this.SpaceDmxData = new ConcurrentDictionary<int, List<byte>>();
            this.SpaceDmxDataReceiveStatus = new ConcurrentDictionary<int, bool>();
            this.DebugDmxDataQueue = new ConcurrentQueue<ConcurrentDictionary<int, List<byte>>>();
            this.RecordDmxDataQueue = new ConcurrentQueue<ConcurrentDictionary<int, List<byte>>>();
            this.VirtualClients = new List<VirtualProClient>();
            this.SYNCHROLOCK_KEY = new object();
            this.DebugFrameCount = 0;
            this.RecordFrameCount = 0;
            this.LastFramePacketSequence = 0;
            this.LastFrameReceiveTime = -1;
            this.IsStartReceiveDmxDataStatus = false;
            this.IsDebugDmxData = false;
            this.IsRecordDmxData = false;
            this.IsFirstFrame = true;
            this.ShowFrameCountTask = new System.Timers.Timer(SHOW_FRAME_COUNT_INTERVALTIME) { AutoReset = true };
            this.ShowFrameCountTask.Elapsed += this.ShowFrameCountListen;
            this.IsFirstFrameByRecord = true;
            this.DebugDmxTaskStatus = true;
            this.RecordDmxTaskStatus = true;
            this.ReceiveStatus = true;
            this.DebugDmxTask = new Thread(Debug) { IsBackground = true };
            this.RecordDmxTask = new Thread(Record) { IsBackground = true };
            this.DebugDmxTask.Start();
            this.RecordDmxTask.Start();
            this.ShowFrameCountTask.Start();
        }

        private void InitLedServer()
        {
            try
            {
                this.DebugServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.DebugServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                this.DebugServer.Bind(new IPEndPoint(IPAddress.Parse(this.localIP), PORT));
                this.DebugServerReceiveClient = new UdpClient() { Client = this.DebugServer };
                this.LedServerReceive = new Thread(this.LedServerReceiveListen) { IsBackground = true };
                this.ReceiveStatus = true;
                this.LedServerReceive.Start(this.DebugServerReceiveClient);
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void Close()
        {
            try
            {
                this.ShowFrameCountTask.Stop();
                this.IsStartReceiveDmxDataStatus = false;
                this.ReceiveStatus = false;
                this.IsDebugDmxData = false;
                this.IsRecordDmxData = false;
                this.DebugDmxTaskStatus = false;
                this.RecordDmxTaskStatus = false;
                Thread.Sleep(100);
                this.IsFirstFrame = true;
                this.IsFirstFrameByRecord = true;
                foreach (VirtualProClient client in this.VirtualClients)
                {
                    client.Close();
                }
                if (this.DebugServer != null)
                {
                    if (this.DebugServerReceiveClient != null)
                    {
                        this.DebugServerReceiveClient.Close();
                        this.DebugServerReceiveClient = null;
                    }
                    this.DebugServer.Close();
                    this.DebugServer = null;
                }
                this.SpaceDmxData = null;
                this.SpaceDmxDataReceiveStatus = null;
                this.DebugDmxDataQueue = null;
                this.RecordDmxDataQueue = null;
                this.VirtualClients = null;
                this.VirtualIPS = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public VirtualProClientsManager Start()
        {
            this.IsStartReceiveDmxDataStatus = true;
            this.isSycnFirstFrame = false;
            return this;
        }

        public VirtualProClientsManager Stop()
        {
            this.IsStartReceiveDmxDataStatus = false;
            return this;
        }

        public VirtualProClientsManager StartDebug(GetDebugFrameCount getDebugFrameCount)
        {
            this.DebugFrameCount = 0;
            this.IsDebugDmxData = true;
            this.GetDebugFrameCount_Event = getDebugFrameCount;
            return this;
        }

        public VirtualProClientsManager StopDebug()
        {
            this.IsDebugDmxData = false;
            this.GetDebugFrameCount_Event = null;
            this.DebugFrameCount = 0;
            return this;
        }

        public VirtualProClientsManager StartRecord(String filePath,String config, GetRecordFrameCount getRecordFrameCount)
        {
            try
            {
                this.FilePath = filePath;
                this.ConfigPath = config;

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
                this.RecordFrameCount = 0;
                this.IsFirstFrameByRecord = true;
                this.IsRecordDmxData = true;
                this.GetRecordFrameCount_Event = getRecordFrameCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return this;
        }

        public VirtualProClientsManager StopRecord()
        {
            this.IsRecordDmxData = false;
            this.GetRecordFrameCount_Event = null;
            this.RecordFrameCount = 0;
            return this;
        }

        private void SyncDMXDataCache()
        {
            if (this.IsStartReceiveDmxDataStatus)
            {
                lock (this.SYNCHROLOCK_KEY)
                {
                    if (this.isSycnFirstFrame)
                    {
                        if (this.IsDebugDmxData)
                        {
                            lock (this.DebugDmxDataQueue)
                            {
                                if (this.SpaceDmxData.Count > 0)
                                {
                                    this.DebugDmxDataQueue.Enqueue(this.SpaceDmxData);
                                }
                            }
                        }
                        if (this.IsRecordDmxData)
                        {
                            lock (this.RecordDmxDataQueue)
                            {
                                if (this.SpaceDmxData.Count > 0)
                                {
                                    this.RecordDmxDataQueue.Enqueue(this.SpaceDmxData);
                                }
                            }
                        }
                        //TODO 添加最大包数验证
                        List<int> keys = this.SpaceDmxData.Keys.ToList();
                        this.SpaceDmxData = new ConcurrentDictionary<int, List<byte>>();
                        this.SpaceDmxDataReceiveStatus = new ConcurrentDictionary<int, bool>();
                        foreach (int spaceIndex in keys)
                        {
                            this.SpaceDmxData.TryAdd(spaceIndex, new List<byte>());
                            this.SpaceDmxDataReceiveStatus.TryAdd(spaceIndex, false);
                        }
                    }
                    else
                    {
                        this.isSycnFirstFrame = true;
                    }
                }
            }
        }

        private void Manager(int clientIndex,int space,List<byte> dmxData)
        {
            int port = clientIndex * 256 + space;
            try
            {
                if (this.IsStartReceiveDmxDataStatus && this.isSycnFirstFrame)
                {
                    lock (this.SYNCHROLOCK_KEY)
                    {
                        this.SpaceDmxData[port] = dmxData;
                        this.SpaceDmxDataReceiveStatus[port] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void LedServerReceiveListen(Object obj)
        {
            UdpClient client = obj as UdpClient;
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, PORT);
            while (this.ReceiveStatus)
            {
                try
                {
                    byte[] receiveBuff = client.Receive(ref iPEndPoint);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private void Debug(Object obj)
        {
            while (this.DebugDmxTaskStatus)
            {
                try
                {
                    if (this.IsDebugDmxData)
                    {
                        int queueCount = 0;
                        lock (this.DebugDmxDataQueue)
                        {
                            queueCount = this.DebugDmxDataQueue.Count;
                        }
                        if (queueCount > 0)
                        {
                            lock (this.DebugDmxDataQueue)
                            {
                                ConcurrentDictionary<int, List<byte>> dmxData = new ConcurrentDictionary<int, List<byte>>();
                                this.DebugDmxDataQueue.TryDequeue(out dmxData);
                                if (dmxData.Count > 0)
                                {
                                    this.DebugTask(dmxData);
                                }
                                else
                                {
                                    this.ThreadSleepOneSe();
                                }
                            }
                        }
                        else
                        {
                            this.ThreadSleepOneSe();
                        }
                    }
                    else
                    {
                        this.ThreadSleepOneSe();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private void DebugTask(ConcurrentDictionary<int,List<byte>> dmxData)
        {
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Broadcast, PORT);

            Dictionary<int, Queue<List<byte>>> dataBuff = new Dictionary<int, Queue<List<byte>>>();
            Dictionary<int, Dictionary<int, List<byte>>> dmxDataBuff = new Dictionary<int, Dictionary<int, List<byte>>>();
            Dictionary<int, Stack<byte>> ledInterfaceDMXDatas = new Dictionary<int, Stack<byte>>();
            try
            {
                #region 数据整理
                for (int controlIndex = 0; controlIndex < this.LedControlNumber; controlIndex++)
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
                for (int controlIndex = 0; controlIndex < this.LedControlNumber; controlIndex++)
                {
                    int controlNo = controlIndex + 1;
                    ledInterfaceDMXDatas.Add(controlNo, new Stack<byte>());
                    for (int dataIndex = 0; dataIndex < this.LedSpaceNumber * 512; dataIndex += 3)
                    {
                        //R
                        for (int intefaceIndex = 0; intefaceIndex < this.LedInterfaceNumber; intefaceIndex++)
                        {
                            byte value = dmxDataBuff[controlNo][intefaceIndex + 1][dataIndex];
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
                            byte value = dmxDataBuff[controlNo][intefaceIndex + 1][dataIndex + 2];
                            ledInterfaceDMXDatas[controlNo].Push(value);
                        }
                    }
                }
                #endregion
                #region 数据分包
                int packageSize = 1024;
                for (int controlIndex = 0; controlIndex < this.LedControlNumber; controlIndex++)
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
                for (int controlIndex = 0; controlIndex < this.LedControlNumber; controlIndex++)
                {
                    int controlNo = controlIndex + 1;
                    byte[] head = new byte[] { Convert.ToByte((controlNo >> 8) & 0xFF), Convert.ToByte(controlNo & 0xFF), 0x33, 0x44 };
                    this.DebugServer.SendTo(head, iPEnd);
                    while (dataBuff[controlNo].Count > 0)
                    {
                        this.DebugServer.SendTo(dataBuff[controlNo].Dequeue().ToArray(), iPEnd);
                        for (int i = 0; i < 60000; i++)
                        {
                            ;
                        }
                    }
                }
                this.DebugServer.SendTo(PACKAGE_END.ToArray(), iPEnd);
                #endregion
                this.DebugFrameCount++;
            }
            catch (Exception ex)
            {
                Console.WriteLine("实时调试发送报错");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void Record(Object obj)
        {
            while (this.RecordDmxTaskStatus)
            {
                try
                {
                    if (this.IsRecordDmxData)
                    {
                        int queueCount = 0;
                        lock (this.RecordDmxDataQueue)
                        {
                            queueCount = this.RecordDmxDataQueue.Count;
                        }
                        if (queueCount > 0)
                        {
                            lock (this.RecordDmxDataQueue)
                            {
                                ConcurrentDictionary<int, List<byte>> dmxData = new ConcurrentDictionary<int, List<byte>>();
                                this.RecordDmxDataQueue.TryDequeue(out dmxData);
                                if (dmxData.Count > 0)
                                {
                                    this.RecordTask(dmxData);
                                }
                                else
                                {
                                    this.ThreadSleepOneSe();
                                }
                            }
                        }
                        else
                        {
                            this.ThreadSleepOneSe();
                        }
                    }
                    else
                    {
                        this.ThreadSleepOneSe();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private void RecordTask(ConcurrentDictionary<int, List<byte>> dmxData)
        {
            try
            {
                Dictionary<int, Queue<List<byte>>> dataBuff = new Dictionary<int, Queue<List<byte>>>();
                Dictionary<int, Dictionary<int, List<byte>>> dmxDataBuff = new Dictionary<int, Dictionary<int, List<byte>>>();
                Dictionary<int, Stack<byte>> ledInterfaceDMXDatas = new Dictionary<int, Stack<byte>>();
                #region 数据整理
                for (int controlIndex = 0; controlIndex < this.LedControlNumber; controlIndex++)
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
                for (int controlIndex = 0; controlIndex < this.LedControlNumber; controlIndex++)
                {
                    int controlNo = controlIndex + 1;
                    ledInterfaceDMXDatas.Add(controlNo, new Stack<byte>());
                    int maxLength = 0;
                    for (int index = 0; index < this.LedInterfaceNumber; index++)
                    {
                        maxLength = maxLength > dmxDataBuff[controlNo][index + 1].Count ? maxLength : dmxDataBuff[controlNo][index + 1].Count;
                    }
                    //int count = maxLength % 3 == 0 ? 3 : 4;
                    int count = this.IsRGB ? 3 : 4;
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
                for (int controlIndex = 0; controlIndex < this.LedControlNumber; controlIndex++)
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
                this.RecordFrameCount++;
            }
            catch (Exception ex)
            {
                Console.WriteLine("录制文件报错");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void CreateConfigFile(ConcurrentDictionary<int, List<byte>> dmxData)
        {
            try
            {
                List<byte> buff = new List<byte>();
                int ledInterfaceCount = 0;
                buff.AddRange(new byte[] { 0x53, 0x54, 0x55, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                buff.AddRange(new byte[] { 0x06, 0x00, 0x08, 0x00 });
                buff.AddRange(new byte[] { 0x05, 0x00, 0x00, 0x00, 0x00, 0x00 });
                for (int ledInterfaceIndex = 0; ledInterfaceIndex < this.LedControlNumber * this.LedInterfaceNumber; ledInterfaceIndex++)
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
                for (int ledInterfaceIndex = 0; ledInterfaceIndex < this.LedControlNumber * this.LedInterfaceNumber; ledInterfaceIndex++)
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
                    //int ledInterfaceDataLength = ledInterfaceBuff.Count % 3 == 0 ? ledInterfaceBuff.Count / 3 : ledInterfaceBuff.Count / 4;
                    int ledInterfaceDataLength = this.IsRGB ? ledInterfaceBuff.Count / 3 : ledInterfaceBuff.Count / 4;

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

        private void ShowFrameCountListen(object sender, ElapsedEventArgs e)
        {
            if (this.IsRecordDmxData)
            {
                if (this.GetRecordFrameCount_Event != null)
                {
                    this.GetRecordFrameCount_Event(this.RecordFrameCount);
                }
            }
            if (this.IsDebugDmxData)
            {
                if (this.GetDebugFrameCount_Event != null)
                {
                    this.GetDebugFrameCount_Event(this.DebugFrameCount);
                }
            }
        }

        private void ThreadSleepOneSe()
        {
            timeBeginPeriod(1);
            Thread.Sleep(THREAD_SLEEP_TIME);
            timeEndPeriod(1);
        }
    }
}
