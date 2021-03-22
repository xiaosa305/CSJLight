using LBDConfigTool.utils.conf;
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

namespace LBDConfigTool.utils.record
{
    public class DMXManager
    {
        [DllImport("winmm.dll")] internal static extern uint timeBeginPeriod(uint period);
        [DllImport("winmm.dll")] internal static extern uint timeEndPeriod(uint period);

        private const int THREAD_SLEEP_TIME = 1;
        private const int SHOW_FRAME_COUNT_INTERVALTIME = 100;
        private Object SYNCHROLOCK_KEY { get; set; }
        private int LedInterfaceNumber { get; set; }
        private int LedSpaceNumber { get; set; }
        private int LedControlNumber { get; set; }
        private int RecordFrameCount { get; set; }

        private string FilePath { get; set; }
        private string ConfigPath { get; set; }
        private CaptureTool CaptureTool { get; set; }

        private ConcurrentDictionary<int, List<byte>> SpaceDmxData { get; set; }
        private ConcurrentDictionary<int, bool> SpaceDmxDataReceiveStatus { get; set; }

        private Thread RecordDmxTask { get; set; }

        private bool RecordDmxTaskStatus { get; set; }
        private bool IsRecordDmxData { get; set; }
        private bool IsFirstFrameByRecord { get; set; }
        private bool IsStartReceiveDmxDataStatus { get; set; }

        private ConcurrentQueue<ConcurrentDictionary<int, List<byte>>> RecordDmxDataQueue { get; set; }

        private System.Timers.Timer ShowFrameCountTask { get; set; }

        public delegate void GetRecordFrameCount(int frameCount);

        private GetRecordFrameCount GetRecordFrameCount_Event { get; set; }

        public DMXManager(CSJConf conf)
        {
            this.Init();
            this.LedControlNumber = 1;
            this.LedInterfaceNumber = conf.Fk_lushu;
            this.LedSpaceNumber = conf.Art_Net_Pre;

            this.CaptureTool = new CaptureTool(conf, this.FrameSync, this.Manager);
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
            this.RecordDmxDataQueue = new ConcurrentQueue<ConcurrentDictionary<int, List<byte>>>();
            this.SYNCHROLOCK_KEY = new object();
            this.RecordFrameCount = 0;
            this.IsStartReceiveDmxDataStatus = false;
            this.IsRecordDmxData = false;
            this.ShowFrameCountTask = new System.Timers.Timer(SHOW_FRAME_COUNT_INTERVALTIME) { AutoReset = true };
            this.ShowFrameCountTask.Elapsed += this.ShowFrameCountListen;
            this.IsFirstFrameByRecord = true;
            this.RecordDmxTaskStatus = true;
            this.RecordDmxTask = new Thread(Record) { IsBackground = true };
            this.RecordDmxTask.Start();
            this.ShowFrameCountTask.Start();
        }

        public void Close()
        {
            try
            {
                this.Stop();
                this.ShowFrameCountTask.Stop();
                this.IsStartReceiveDmxDataStatus = false;
                this.IsRecordDmxData = false;
                this.RecordDmxTaskStatus = false;
                Thread.Sleep(100);
                this.IsFirstFrameByRecord = true;
                this.SpaceDmxData = null;
                this.SpaceDmxDataReceiveStatus = null;
                this.RecordDmxDataQueue = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public DMXManager Start()
        {
            this.IsStartReceiveDmxDataStatus = true;
            if (this.CaptureTool != null)
            {
                this.CaptureTool.Start();
            }
            return this;
        }

        public DMXManager Stop()
        {
            this.IsStartReceiveDmxDataStatus = false;
            if (this.CaptureTool != null)
            {
                this.CaptureTool.Stop();
            }
            return this;
        }

        public DMXManager StartRecord(String filePath,String config, GetRecordFrameCount getRecordFrameCount)
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

        public DMXManager StopRecord()
        {
            this.IsRecordDmxData = false;
            this.GetRecordFrameCount_Event = null;
            this.RecordFrameCount = 0;
            return this;
        }

        private void Manager(int port,List<byte> dmxData)
        {
            try
            {
                lock (this.SYNCHROLOCK_KEY)
                {
                    if (this.IsStartReceiveDmxDataStatus)
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

        private void FrameSync()
        {
            lock (this.SYNCHROLOCK_KEY)
            {
                if (this.IsStartReceiveDmxDataStatus)
                {
                    if (this.IsRecordDmxData)
                    {
                        lock (this.RecordDmxDataQueue)
                        {
                            this.RecordDmxDataQueue.Enqueue(this.SpaceDmxData);
                        }
                        List<int> keys = this.SpaceDmxData.Keys.ToList();
                        this.SpaceDmxData = new ConcurrentDictionary<int, List<byte>>();
                        this.SpaceDmxDataReceiveStatus = new ConcurrentDictionary<int, bool>();
                        foreach (int spaceIndex in keys)
                        {
                            this.SpaceDmxData.TryAdd(spaceIndex, new List<byte>());
                            this.SpaceDmxDataReceiveStatus.TryAdd(spaceIndex, false);
                        }
                    }
                }
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

        private void ShowFrameCountListen(object sender, ElapsedEventArgs e)
        {
            if (this.IsRecordDmxData)
            {
                if (this.GetRecordFrameCount_Event != null)
                {
                    this.GetRecordFrameCount_Event(this.RecordFrameCount);
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
