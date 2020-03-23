using MultiLedController.Entity;
using MultiLedController.Utils.IMPL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;

namespace MultiLedController.Utils.IMPL
{
    public class Art_Net_Manager: IArt_Net_Manager
    {
        private string SaveFilePath = @"C:\WorkSpace\Save\SC00.bin";
        private static IArt_Net_Manager Instance { get; set; }
        private List<Art_Net_Client> Clients { get; set; }
        private Dictionary<int, bool> FieldsReceiveStatus { get; set; }
        private Dictionary<int, List<byte>> FieldsData { get; set; }
        private static readonly Object KEY = new object();
        private Dictionary<int, int> FieldsReceiveDataSize { get; set; }
        private bool DebugStatus { get; set; }
        private bool IsSaveToFile { get; set; }
        private bool TimerStatus { get; set; }
        private long SystemTime { get; set; }
        private int Flag { get; set; }
        private int PackNumSum { get; set; }
        private Thread OnTimeThread { get; set; }
        private System.Timers.Timer SaveTimers { get; set; }
        private Socket DebugServer { get; set; }
        private Art_Net_Manager()
        {
            this.Init();
        }
        public static IArt_Net_Manager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Art_Net_Manager();
            }
            return Instance;
        }
        private void Init()
        {
            this.StopDebug();
            this.StopSaveToFile();
            this.TimerStatus = false;
            this.Clients = new List<Art_Net_Client>();
            this.FieldsReceiveStatus = new Dictionary<int, bool>();
            this.FieldsData = new Dictionary<int, List<byte>>();
            this.FieldsReceiveDataSize = new Dictionary<int, int>();
            //启动定时器
            this.OnTimeThread = new Thread(this.OnTimer)
            {
                IsBackground = true
            };
            this.OnTimeThread.Start();
            this.Flag = 0;
            this.PackNumSum = 0;
            this.SystemTime = -1;
            LEDControllerServer.GetInstance().SetManager(this);

        }
        /// <summary>
        /// 关闭服务器
        /// </summary>
        public void Close()
        {
            if (this.Clients.Count != 0)
            {
                DataQueue.GetInstance().Reset();
                foreach (Art_Net_Client item in this.Clients)
                {
                    item.Close();
                }
            }
           
            this.OnTimeThread.Abort();
        }
        /// <summary>
        /// 启动虚拟控制器对接麦爵士
        /// </summary>
        /// <param name="virtuals">虚拟控制器信息，包含虚拟控制器使用的Ip地址以及虚拟控制器空间数量</param>
        /// <param name="serverIp">麦爵士所在的服务器IP</param>
        /// <param name="currentMainIP">本地主IP</param>
        public void Start(List<VirtualControlInfo> virtuals, string currentIP, string serverIp)
        {
            this.Close();
            Thread.Sleep(1000);
            this.Init();
            if (virtuals.Count == 0)
            {
                return;
            }
            int startIndex = 0;
            //添加虚拟设备信息
            for (int i = 0; i < virtuals.Count; i++)
            {
                //添加虚拟设备客户端
                this.Clients.Add(new Art_Net_Client(virtuals[i].IP, serverIp, startIndex, virtuals[i].SpaceNum, this));
                //添虚拟设备子空间接收状态、接收数据缓存、接收数据大小缓存
                for (int j = 0; j < virtuals[i].SpaceNum; j++)
                {
                    int fieldNum = startIndex + j;
                    this.FieldsReceiveStatus.Add(fieldNum, false);
                    this.FieldsData.Add(fieldNum, new List<byte>());
                    this.FieldsReceiveDataSize.Add(fieldNum, 0);
                }
                startIndex += virtuals[i].SpaceNum;
            }
            LEDControllerServer.GetInstance().StartServer(currentIP);
        }
        /// <summary>
        /// 接收DMX数据包处理
        /// </summary>
        /// <param name="fieldNum">空间编号</param>
        /// <param name="data">接收到的DMX数据</param>
        public void AddFieldData(int fieldNum, List<byte> data)
        {
            lock (KEY)
            {
                long time = 0;
                if (this.SystemTime == -1)
                {
                    this.SystemTime = DateTime.Now.Ticks;
                }
                else
                {
                    long temp = DateTime.Now.Ticks;
                    time = (temp - this.SystemTime) / 10000;
                    this.SystemTime = temp;
                }
                if (time > 5 && this.Flag == 0)
                {
                    this.Flag = 1;
                    foreach (int key in this.FieldsReceiveStatus.Keys)
                    {
                        if (this.FieldsReceiveStatus[key])
                        {
                            this.PackNumSum += key;
                        }
                    }
                }
                else if (time > 5 && this.Flag == 1)
                {
                    //计算包序
                    int sum = 0;
                    foreach (int key in this.FieldsReceiveStatus.Keys)
                    {
                        if (this.FieldsReceiveStatus[key])
                        {
                            sum += key;
                        }
                    }
                    //检测与上一包包序累加和
                    if (sum == this.PackNumSum)
                    {
                        //存储文件
                        if (this.IsSaveToFile)
                        {
                            DataQueue.GetInstance().SaveEnqueue(FieldsData, Convert.ToInt16(time), this.Clients.Count,4);
                        }
                        //启动实时调试状态
                        if (this.DebugStatus)
                        {
                            DataQueue.GetInstance().DebugEnqueue(FieldsData, Convert.ToInt16(time));
                        }
                        //组包完成，清除包数据缓存区以及数据包接收标记
                        List<int> keys = new List<int>();
                        keys.AddRange(this.FieldsData.Keys);
                        foreach (int item in keys)
                        {
                            this.FieldsData[item] = new List<byte>();
                        }
                        keys.Clear();
                        keys.AddRange(this.FieldsReceiveStatus.Keys);
                        foreach (int item in keys)
                        {
                            this.FieldsReceiveStatus[item] = false;
                        }
                    }
                    this.PackNumSum = sum;
                }
                this.FieldsData[fieldNum] = data;
                this.FieldsReceiveStatus[fieldNum] = true;
                this.FieldsReceiveDataSize[fieldNum] = data.Count;
            }
        }
        /// <summary>
        /// 发送调试数据
        /// </summary>
        /// <param name="frameTime">帧间隔时间</param>
        private void DebugMode(Object obj)
        {
            Dictionary<int, List<byte>> Data = (Dictionary<int, List<byte>>)obj;
            List<byte> sendBuff = new List<byte>();
            for (int i = 0; i < this.Clients.Count; i++)
            {
                sendBuff.Clear();
                List<byte> data = new List<byte>();
                for (int j = 0; j < 4; j++)
                {
                    data.AddRange(Data[i * 4 + j]);
                }
                if (data.Count == 0)
                {
                    sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2), 0x00, 0x00 });
                    sendBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 1024));
                    LEDControllerServer.GetInstance().SendDebugData(sendBuff);
                    sendBuff.Clear();
                    sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2 + 1), 0x00, 0x00 });
                    sendBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 1024));
                    LEDControllerServer.GetInstance().SendDebugData(sendBuff);
                }
                else
                {
                    if (data.Count > 1024)
                    {
                        sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2), Convert.ToByte((1024 >> 8) & 0xFF), Convert.ToByte(1024 & 0xFF) });
                        sendBuff.AddRange(data.Take(1024).ToList());
                        LEDControllerServer.GetInstance().SendDebugData(sendBuff);
                        sendBuff.Clear();
                        sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2 + 1), Convert.ToByte(((data.Count - 1024) >> 8) & 0xFF), Convert.ToByte((data.Count - 1024) & 0xFF) });
                        sendBuff.AddRange(data.Skip(1024).Take(1024).ToList());
                        sendBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), (1024) - sendBuff.Count + 7));
                        LEDControllerServer.GetInstance().SendDebugData(sendBuff);
                    }
                    else
                    {
                        sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2), Convert.ToByte((data.Count >> 8) & 0xFF), Convert.ToByte(data.Count & 0xFF) });
                        sendBuff.AddRange(data);
                        sendBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), (1024) - sendBuff.Count + 7));
                        LEDControllerServer.GetInstance().SendDebugData(sendBuff);
                        sendBuff.Clear();
                        sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2 + 1), 0x00, 0x00 });
                        sendBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 1024));
                        LEDControllerServer.GetInstance().SendDebugData(sendBuff);
                    }
                }
                if (i == 2)
                {
                    Thread.Sleep(3);
                }
            }
        }
        /// <summary>
        /// 发送启动实时调试命令
        /// </summary>
        public void StartDebug()
        {
            //发送起始命令
            List<byte> beginOrder = new List<byte>();
            beginOrder.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x00, 0x00, 0x00, 0x07 });
            beginOrder.AddRange(Encoding.Default.GetBytes("poweron"));
            LEDControllerServer.GetInstance().SendDebugData(beginOrder);
        }
        /// <summary>
        /// 启动实时调试
        /// </summary>
        public void StartDebugMode()
        {
            this.Flag = 0;
            this.PackNumSum = 0;
            this.DebugStatus = true;
            DataQueue.GetInstance().Reset();
        }
        /// <summary>
        /// 关闭实时调试
        /// </summary>
        public void StopDebug()
        {
            this.DebugStatus = false;
        }
        /// <summary>
        /// 搜索设备
        /// </summary>
        /// <param name="currentMainIp">本机主要IP</param>
        public void SearchDevice(string currentMainIp)
        {
            LEDControllerServer.GetInstance().StartServer(currentMainIp);
            LEDControllerServer.GetInstance().InitDeviceList();
            List<byte> order = new List<byte>();
            order.Add(0xEB);
            order.Add(0x55);
            LEDControllerServer.GetInstance().SearchDevice(order);
        }
        /// <summary>
        /// 获取控制器列表
        /// </summary>
        /// <returns>搜索到的设备信息字典，KEY为控制器MAC，VALUE为控制器信息</returns>
        public Dictionary<string, ControlDevice> GetLedControlDevices()
        {
            return LEDControllerServer.GetInstance().GetControlDevices();
        }
        /// <summary>
        /// 修改存储文件路径
        /// </summary>
        /// <param name="dirPath">新的文件存储路径</param>
        public void SetSaveFilePath(string filePath)
        {
            this.SaveFilePath = filePath;
            if (!File.Exists(this.SaveFilePath))
            {
                File.Create(this.SaveFilePath).Dispose();
            }
            List<byte> emptyData = new List<byte>();
            for (int i = 0; i < 35; i++)
            {
                emptyData.Add(0x00);
            }
            FileUtils.GetInstance().WriteToFileByCreate(emptyData, SaveFilePath);
        }
        /// <summary>
        /// 启动数据存储至文件
        /// </summary>
        public void StartSaveToFile()
        {
            if (!File.Exists(this.SaveFilePath))
            {
                File.Create(this.SaveFilePath).Dispose();
            }
            //文件写入预设文件头
            List<byte> emptyData = new List<byte>();
            for (int i = 0; i < 35; i++)
            {
                emptyData.Add(0x00);
            }
            FileUtils.GetInstance().WriteToFileByCreate(emptyData, SaveFilePath);
            this.IsSaveToFile = true;
        }
        /// <summary>
        /// 关闭数据存储至文件
        /// </summary>
        public void StopSaveToFile()
        {
            this.IsSaveToFile = false;
        }
        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="obj"></param>
        private void OnTimer(Object obj)
        {
            while (true)
            {
                if (this.DebugStatus)
                {
                    DebugQueueCacheData data = DataQueue.GetInstance().DebugDequeue();
                    if (data != null)
                    {
                        this.DebugMode(data.FieldDatas);
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
                if (IsSaveToFile)
                {
                    SaveQueueCacheData data = DataQueue.GetInstance().SaveDequeue();
                    if (data != null)
                    {
                        List<byte> head = new List<byte>()
                    {
                        Convert.ToByte(data.Led_Interface_num),
                        Convert.ToByte(data.Led_space),
                        Convert.ToByte(data.FramTime & 0xFF),
                    };
                        for (int interficeIndex = 0; interficeIndex < data.Led_Interface_num; interficeIndex++)
                        {
                            int dataLength = 0;
                            for (int ledSpaceIndex = 0; ledSpaceIndex < data.Led_space; ledSpaceIndex++)
                            {
                                dataLength += data.FieldDatas[interficeIndex * data.Led_space + ledSpaceIndex].Count;
                            }
                            head.Add(Convert.ToByte(dataLength & 0xFF));
                            head.Add(Convert.ToByte((dataLength >> 8) & 0xFF));
                            head.Add(Convert.ToByte((dataLength >> 16) & 0xFF));
                            head.Add(Convert.ToByte((dataLength >> 24) & 0xFF));
                        }
                        FileUtils.GetInstance().WriteToFileBySeek(head, SaveFilePath, 0);
                        List<byte> framData = new List<byte>();
                        for (int interficeIndex = 0; interficeIndex < data.Led_Interface_num; interficeIndex++)
                        {
                            List<byte> routeDatas = new List<byte>();
                            for (int ledSpaceIndex = 0; ledSpaceIndex < data.Led_space; ledSpaceIndex++)
                            {
                                int num = interficeIndex * 4 + ledSpaceIndex;
                                if (this.FieldsReceiveStatus[num])
                                {
                                    routeDatas.AddRange(data.FieldDatas[num]);
                                }
                            }
                            framData.AddRange(routeDatas);
                        }
                        FileUtils.GetInstance().WriteToFile(framData, SaveFilePath);
                        Console.WriteLine("录制一帧数据");
                    }
                }
                Thread.Sleep(0);
            }
        }
    }
}
