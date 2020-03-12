using MultiLedController.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiLedController.Utils
{
    public class Art_Net_Manager
    {
        private static string SaveFileName = "Art_Net_DMX.bin";

        private static Art_Net_Manager Instance { get; set; }
        private List<Art_Net_Client> Clients { get; set; }
        private Dictionary<int, bool> FieldsReceiveStatus { get; set; }
        private Dictionary<int, List<byte>> FieldsData { get; set; }
        private static readonly Object KEY = new object();
        private Stopwatch Stopwatch { get; set; }
        private List<double> FramTimes { get; set; }
        private Dictionary<int, int> FieldsReceiveDataSize { get; set; }
        private bool DebugStatus { get; set; }
        private bool IsSaveToFile { get; set; }

        private Socket DebugServer { get; set; }

        private Art_Net_Manager()
        {
            this.Init();
        }

        public static Art_Net_Manager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Art_Net_Manager();
            }
            return Instance;
        }

        private void Init()
        {
            this.IsSaveToFile = true;
            this.Clients = new List<Art_Net_Client>();
            this.FieldsReceiveStatus = new Dictionary<int, bool>();
            this.FieldsData = new Dictionary<int, List<byte>>();
            this.FieldsReceiveDataSize = new Dictionary<int, int>();
            this.DebugStatus = false;
            LEDControllerServer.GetInstance().SetManager(this);
        }
        /// <summary>
        /// 测试启动器
        /// </summary>
        public void TestStart()
        {
            if (this.Clients.Count != 0)
            {
                foreach (Art_Net_Client item in this.Clients)
                {
                    item.Close();
                }
                this.Init();
                Thread.Sleep(500);
            }
            //Test用添加
            Clients.Add(new Art_Net_Client("192.168.1.32","192.168.1.21", 0, 4, this));

			Clients.Add(new Art_Net_Client("192.168.1.33", "192.168.1.21", 4, 4, this));
			Clients.Add(new Art_Net_Client("192.168.1.34", "192.168.1.21", 8, 4, this));
			Clients.Add(new Art_Net_Client("192.168.1.35", "192.168.1.21", 12, 4, this));
			Clients.Add(new Art_Net_Client("192.168.1.36", "192.168.1.21", 16, 4, this));
			Clients.Add(new Art_Net_Client("192.168.1.37", "192.168.1.21", 20, 4, this));
			//Clients.Add(new Art_Net_Client("192.168.1.120", "192.168.1.14", 24, 4, this));
			//Clients.Add(new Art_Net_Client("192.168.1.121", "192.168.1.14", 28, 4, this));

			for (int i = 0; i < Clients.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int fieldNum = i * 4 + j;
                    this.FieldsReceiveStatus.Add(fieldNum, false);
                    this.FieldsData.Add(fieldNum, new List<byte>());
                    this.FieldsReceiveDataSize.Add(fieldNum, 0);
                }
            }
            //List<byte> emptyData = new List<byte>();
            //for (int i = 0; i < 35; i++)
            //{
            //    emptyData.Add(0x00);
            //}
            //FileUtils.WriteToFileByCreate(emptyData, "Art_Net_DMX.bin");

            ////启动控制器通信服务
            LEDControllerServer.GetInstance().StartServer("192.168.1.31");
        }
        /// <summary>
        /// 启动虚拟控制器对接麦爵士
        /// </summary>
        /// <param name="virtuals">虚拟控制器信息，包含虚拟控制器使用的Ip地址以及虚拟控制器空间数量</param>
        /// <param name="serverIp">麦爵士所在的服务器IP</param>
        /// <param name="currentMainIP">本地主IP</param>
        public void Start(List<VirtualControlInfo> virtuals,string currentIP, string serverIp)
        {
            if (this.Clients.Count != 0)
            {
                foreach (Art_Net_Client item in this.Clients)
                {
                    item.Close();
                }
                this.Init();
            }
            if (virtuals.Count == 0)
            {
                return;
            }
            int startIndex = 0;
            //添加虚拟设备信息
            for (int i = 0; i < virtuals.Count; i++)
            {
                //添加虚拟设备客户端
                Clients.Add(new Art_Net_Client(virtuals[i].IP, serverIp, startIndex, virtuals[i].SpaceNum, this));
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
            if (Stopwatch == null)
            {
                this.Stopwatch = new Stopwatch();
                this.Stopwatch.Start();
                this.FramTimes = new List<double>();
            }
            lock (KEY)
            {
                if (this.FieldsReceiveStatus[fieldNum])
                {
                    //计时
                    if (this.FramTimes.Count > 100)
                    {
                        this.FramTimes.RemoveRange(0, 90);
                    }
                    this.Stopwatch.Stop();
                    this.FramTimes.Add(this.Stopwatch.Elapsed.TotalMilliseconds);

                    //写文件头
                    int led_Interface_num = this.Clients.Count;
                    int led_space = 4;
                    double temp = 0;
                    foreach (double item in this.FramTimes)
                    {
                        temp += item;
                    }
                    int frame_time = (int)(temp / this.FramTimes.Count);

                    List<byte> head = new List<byte>
                    {
                        Convert.ToByte(led_Interface_num),
                        Convert.ToByte(led_space),
                        Convert.ToByte(frame_time)
                    };
                    for (int i = 0; i < this.Clients.Count; i++)
                    {
                        int length = this.FieldsReceiveDataSize[i * 4] + this.FieldsReceiveDataSize[i * 4 + 1] + this.FieldsReceiveDataSize[i * 4 + 2] + this.FieldsReceiveDataSize[i * 4 + 3];
                        head.Add(Convert.ToByte(length & 0xFF));
                        head.Add(Convert.ToByte((length >> 8) & 0xFF));
                        head.Add(Convert.ToByte((length >> 16) & 0xFF));
                        head.Add(Convert.ToByte((length >> 24) & 0xFF));
                    }
                    //已经接受到第二帧数据，开始组包第一帧数据
                    if (this.IsSaveToFile)
                    {
                        FileUtils.WriteToFileBySeek(head, SaveFileName, 0);
                        List<byte> framData = new List<byte>();
                        for (int i = 0; i < this.Clients.Count; i++)
                        {
                            List<byte> routeDatas = new List<byte>();
                            for (int j = 0; j < 4; j++)
                            {
                                int num = i * 4 + j;
                                if (this.FieldsReceiveStatus[num])
                                {
                                    routeDatas.AddRange(this.FieldsData[num]);
                                }
                            }
                            if (routeDatas.Count < 512 * 4 && routeDatas.Count > 0)
                            {
                                routeDatas.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), (512 * 4) - routeDatas.Count));
                            }
                            framData.AddRange(routeDatas);
                        }
                        FileUtils.WriteToFile(framData, SaveFileName);
                    }
                    Console.WriteLine("接收完一帧数据");
                    //启动实时调试状态
                    if (this.DebugStatus)
                    {
                        this.DebugMode(frame_time);
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

                    //计时复位
                    this.Stopwatch.Restart();
                }
                this.FieldsData[fieldNum] = data;
                this.FieldsReceiveStatus[fieldNum] = true;

                //记录包大小
                this.FieldsReceiveDataSize[fieldNum] = data.Count;
            }
        }
        /// <summary>
        /// 发送调试数据
        /// </summary>
        /// <param name="frameTime">帧间隔时间</param>
        private void DebugMode(int frameTime)
        {
            List<byte> sendBuff = new List<byte>();
            for (int i = 0; i < this.Clients.Count; i++)
            {
                sendBuff.Clear();
                List<byte> data = new List<byte>();
                for (int j = 0; j < 4; j++)
                {
                    data.AddRange(this.FieldsData[i * 4 + j]);
                }
                if (data.Count == 0)
                {
                    if (i == this.Clients.Count - 1)
                    {
                        sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2 + 1), 0x00, 0x00 });
                        sendBuff.Add(0x00);
                        LEDControllerServer.GetInstance().SendDebugData(sendBuff);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (data.Count > 1024)
                    {
                        //发送第一包数据
                        sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2), Convert.ToByte((1024 >> 8) & 0xFF), Convert.ToByte(1024 & 0xFF) });
                        sendBuff.AddRange(data.Take(1024).ToList());
                        LEDControllerServer.GetInstance().SendDebugData(sendBuff);
                        sendBuff.Clear();
                        //发送第二包数据
                        sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2 + 1), Convert.ToByte(((data.Count - 1024) >> 8) & 0xFF), Convert.ToByte((data.Count - 1024) & 0xFF) });
                        sendBuff.AddRange(data.Skip(1024).Take(1024).ToList());
                        LEDControllerServer.GetInstance().SendDebugData(sendBuff);
                    }
                    else
                    {
                        sendBuff.AddRange(new byte[] { 0xAA, 0xFF, 0xBB, 0x20, Convert.ToByte(i * 2), Convert.ToByte((data.Count >> 8) & 0xFF), Convert.ToByte(data.Count & 0xFF) });
                        sendBuff.AddRange(data);
                        LEDControllerServer.GetInstance().SendDebugData(sendBuff);
                    }
                }
                //if (i == 1 || i == 3)
                //{
                //    Thread.Sleep(4);
                //}
                if (i == 2)
                {
                    Thread.Sleep(5);
                }
            }
        }
        /// <summary>
        /// 发送启动实时调试命令
        /// </summary>
        public void SendStartDebugOrder()
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
        public void StartDebug()
        {
            this.DebugStatus = true;
        }
        /// <summary>
        /// 关闭实时调试
        /// </summary>
        public void EndDebug()
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
        public  Dictionary<string,ControlDevice> GetLedControlDevices()
        {
            return LEDControllerServer.GetInstance().GetControlDevices();
        }
        /// <summary>
        /// 修改存储文件路径
        /// </summary>
        /// <param name="dirPath">新的文件存储路径</param>
        public void SetSaveDirPath(string dirPath)
        {
            FileUtils.SetSaveDirPath(dirPath);
        }
        /// <summary>
        /// 修改存储文件的名称
        /// </summary>
        /// <param name="fileName">新的文件名称</param>
        public void SetSaveFileName(string fileName)
        {
            SaveFileName = fileName;
        }
        /// <summary>
        /// 启动数据存储至文件
        /// </summary>
        public void StartSaveToFile()
        {
            //文件写入预设文件头
            List<byte> emptyData = new List<byte>();
            for (int i = 0; i < 35; i++)
            {
                emptyData.Add(0x00);
            }
            FileUtils.WriteToFileByCreate(emptyData,SaveFileName);
            this.IsSaveToFile = true;
        }
        /// <summary>
        /// 关闭数据存储至文件
        /// </summary>
        public void StopSaveToFile()
        {
            this.IsSaveToFile = false;
        }
    }
}
