using LightController.Ast;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightController.Tools.CSJ
{
    public abstract class ICommunicator
    {
        protected const int ORDER = 1;
        protected const int DATA = 2;
        protected bool IsSending { get; set; }//发送状态标记
        protected bool IsReceive { get; set; }//接收状态标记
        protected bool IsTimeOutThreadStart { get; set; }//超时执行启动状态标记
        protected bool DownloadStatus { get; set; }//下载状态标记
        protected int OrderOrData { get; set; }//当前数据包类型
        protected int DownloadFileToTalSize { get; set; }//工程项目文件总大小
        protected int CurrentDownloadCompletedSize { get; set; }//当前文件大小
        protected int TimeOutCount { get; set; }//超时计数
        protected int PackageSize { get; set; }//数据包大小
        protected int PackageCount { get; set; }//数据包总数
        protected int Package_Index { get; set; }//当前数据包编号
        protected int Addr { get; set; }//设备地址
        protected int TimeIndex { get; set; }//超时时间计数
        protected string CurrentFileName { get; set; }//当前下载文件名称
        protected string HardwarePath { get; set; }//硬件配置文件路径
        protected string ConfigPath { get; set; }//全局配置文件路径
        protected string Order { get; set; }//当前执行命令
        protected string DeviceName { get; set; }//当前设备名称
        protected string[] Parameters { get; set; }//命令参数组
        protected string UpdateFilePath { get; set; }//硬件更新文件路径
        protected byte[] Data { get; set; }//文件数据
        protected DBWrapper Wrapper { get; set; }//数据库文件
        protected Thread DownloadThread { get; set; }//下载工程项目线程
        protected Thread TimeOutThread { get; set; }//超时处理线程
        protected Thread UpdateThread { get; set; }//硬件更新线程
        protected IReceiveCallBack CallBack { get; set; }//命令结束执行回调
        protected GetParamDelegate GetParamDelegate { get; set; }//获取硬件配置信息回调委托方法
        protected DownloadProgressDelegate DownloadProgressDelegate { get; set; }//下载工程项目文件进度回调委托方法
        protected abstract void Send(byte[] txBuff);
        public abstract void CloseDevice();
        public void SetPackageSize(int size)
        {
            this.PackageSize = size;
        }
        public void SetAddr(int addr)
        {
            this.Addr = addr;
        }
        public void SetDeviceName(string name)
        {
            this.DeviceName = name;
        }
        public int GetAddr()
        {
            return this.Addr;
        }
        public string GetDeviceName()
        {
            return this.DeviceName;
        }
        protected void InitParameters()
        {
            this.IsSending = false;
            this.IsReceive = true;
            this.IsTimeOutThreadStart = false;
            this.DownloadStatus = true;
            this.CurrentDownloadCompletedSize = 0;
            this.DeviceName = string.Empty;
            this.DownloadFileToTalSize = 0;
            this.TimeOutCount = 0;
            this.PackageCount = 0;
            this.Package_Index = 0;
            this.CurrentFileName = string.Empty;
            this.HardwarePath = string.Empty;
            this.ConfigPath = string.Empty;
            this.Order = string.Empty;
            this.Parameters = null;
            this.Data = null;
            this.Wrapper = null;
            this.CallBack = null;
            this.GetParamDelegate = null;
            this.DownloadProgressDelegate = null;
            this.TimeIndex = 0;
        }
        protected void SendData(byte[] data, string order, string[] parameters)
        {
            this.Data = data;
            this.Order = order;
            this.Parameters = parameters;
            this.SendOrderPackage();
        }
        protected void SendOrderPackage()
        {
            try
            {
                if (this.Data != null)
                {
                    this.PackageCount = this.Data.Length / this.PackageSize;
                    this.PackageCount = (this.Data.Length % this.PackageSize == 0) ? this.PackageCount : this.PackageCount + 1;
                    this.Package_Index = 0;
                }
                List<byte> package = new List<byte>();
                List<byte> packageData = new List<byte>();
                byte[] packageOrder = Encoding.Default.GetBytes(this.Order);
                byte[] space = Encoding.Default.GetBytes(" ");
                packageData.AddRange(packageOrder);
                if (this.Parameters != null)
                {
                    packageData.AddRange(space);
                    for (int i = 0; i < this.Parameters.Length; i++)
                    {
                        packageData.AddRange(Encoding.Default.GetBytes(this.Parameters[i]));
                        if (i != this.Parameters.Length - 1)
                        {
                            packageData.AddRange(space);
                        }
                    }
                    packageData.Add(0x00);
                }
                byte[] packageDataLength = new byte[]
                {
                    Convert.ToByte(packageData.Count & 0xFF),
                    Convert.ToByte((Convert.ToByte(packageData.Count >> 8) & 0xFF))
                };
                byte packageMark = this.GetOrderMark();
                byte[] packageHead = new byte[]
                {
                    Convert.ToByte(0xAA),
                    Convert.ToByte(0xBB),
                    Convert.ToByte(Addr),
                    packageDataLength[0],
                    packageDataLength[1],
                    packageMark,
                    Convert.ToByte(0x00),
                    Convert.ToByte(0x00)
                };
                package.AddRange(packageHead);
                package.AddRange(packageData);
                byte[] packageCRC = CRCTools.GetInstance().GetCRC(package.ToArray());
                package[6] = packageCRC[0];
                package[7] = packageCRC[1];
                this.OrderOrData = ORDER;
                this.Send(package.ToArray());
            }
            catch (Exception)
            {
                this.CloseDevice();
            }
        }
        protected void SendDataPackage()
        {
            try
            {
                this.Package_Index++;
                byte[] packageData;
                List<byte> package = new List<byte>();
                if (this.Package_Index != this.PackageCount)
                {
                    packageData = new byte[this.PackageSize];
                    for (int i = 0; i < this.PackageSize; i++)
                    {
                        packageData[i] = this.Data[(this.Package_Index - 1) * this.PackageSize + i];
                    }
                }
                else
                {
                    packageData = new byte[this.Data.Length - (this.Package_Index - 1) * this.PackageSize];
                    for (int i = 0; i < Data.Length - (this.Package_Index - 1) * this.PackageSize; i++)
                    {
                        packageData[i] = this.Data[(this.Package_Index - 1) * this.PackageSize + i];
                    }
                }
                byte[] packageDataSize = new byte[]
                {
                    Convert.ToByte(packageData.Length & 0xFF),
                    Convert.ToByte((packageData.Length >> 8) & 0xFF)
                };
                byte packageMark = this.GetDataMark();
                byte[] packageHead = new byte[]
                {
                    Convert.ToByte(0xAA),
                    Convert.ToByte(0xBB),
                    Convert.ToByte(this.Addr),
                    packageDataSize[0],
                    packageDataSize[1],
                    packageMark,
                    Convert.ToByte(0x00),
                    Convert.ToByte(0x00)
                };
                package.AddRange(packageHead);
                package.AddRange(packageData);
                byte[] packageCRC = CRCTools.GetInstance().GetCRC(package.ToArray());
                package[6] = packageCRC[0];
                package[7] = packageCRC[1];
                if (this.Order.Equals(Constant.ORDER_PUT) || this.Order.Equals(Constant.ORDER_UPDATE))
                {
                    this.CurrentDownloadCompletedSize += packageData.Count();
                }
                this.OrderOrData = DATA;
                this.Send(package.ToArray());
            }
            catch (Exception)
            {
                this.CloseDevice();
            }
        }
        protected byte GetDataMark()
        {
            byte mark;
            if (this.Package_Index == this.PackageCount)
            {
                mark = Convert.ToByte(Constant.MARK_DATA_END, 2);
            }
            else
            {
                mark = Convert.ToByte(Constant.MARK_DATA_NO_END, 2);
            }
            return mark;
        }
        protected byte GetOrderMark()
        {
            byte mark;
            switch (this.Order)
            {
                case Constant.ORDER_PUT:
                    mark = Convert.ToByte(Constant.MARK_ORDER_TAKE_DATA, 2);
                    break;
                case Constant.ORDER_PUT_PARAM:
                    mark = Convert.ToByte(Constant.MARK_ORDER_TAKE_DATA, 2);
                    break;
                case Constant.ORDER_UPDATE:
                    mark = Convert.ToByte(Constant.MARK_ORDER_TAKE_DATA, 2);
                    break;
                default:
                    mark = Convert.ToByte(Constant.MARK_ORDER_NO_TAKE_DATA, 2);
                    break;
            }
            return mark;
        }
        protected void TimeOut()
        {
            while (true)
            {
                if (this.IsTimeOutThreadStart)
                {
                    for (this.TimeIndex = 0; this.TimeIndex < Constant.TIMEOUT; this.TimeIndex++)
                    {
                        Console.WriteLine("超时时间：" + this.TimeIndex);
                        Thread.Sleep(1);
                        if (this.IsReceive)
                        {
                            break;
                        }
                    }
                    this.IsTimeOutThreadStart = false;
                    if (!this.IsReceive)
                    {
                        CSJLogs.GetInstance().DebugLog(CurrentFileName + "==>" + Order + "SendDataTimeout");
                        string deviceName = this.DeviceName;
                        if (this.TimeOutCount == Constant.TIMEMAXCOUNT)
                        {
                            this.TimeOutCount = 0;
                            this.IsSending = false;
                            if (Order.Equals(Constant.ORDER_BEGIN_SEND) || Order.Equals(Constant.ORDER_END_SEND) || Order.Equals(Constant.ORDER_PUT))
                            {
                                try
                                {
                                    this.DownloadStatus = false;
                                    this.DownloadThread.Abort();
                                }
                                finally
                                {
                                    this.DownloadProgressDelegate("", 0);
                                }
                            }
                            else if (this.Order.Equals(Constant.ORDER_UPDATE))
                            {
                                try
                                {
                                    if (null != UpdateThread)
                                    {
                                        this.UpdateThread.Abort();
                                    }
                                }
                                finally
                                {
                                    this.DownloadProgressDelegate("", 0);
                                }
                            }
                            this.CallBack.SendError(deviceName, Order);
                            this.CloseDevice();
                        }
                        else
                        {
                            this.TimeOutCount++;
                            switch (this.OrderOrData)
                            {
                                case ORDER:
                                    this.SendOrderPackage();
                                    break;
                                case DATA:
                                    this.Package_Index--;
                                    this.SendDataPackage();
                                    break;
                            }
                        }
                        this.TimeIndex = 0;
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }
        protected void ReceiveMessageManage(byte[] rxBuff, int rxCount)
        {
            this.TimeIndex = Constant.TIMEOUT;
            this.IsTimeOutThreadStart = false;
            this.IsReceive = true;
            string devicename = this.DeviceName;
            string rxStr = Encoding.UTF8.GetString(rxBuff, 0, rxCount);
            Console.WriteLine("Order is :" + rxStr);
            switch (this.Order)
            {
                case Constant.ORDER_BEGIN_SEND:
                    switch (rxStr.Split(':')[0])
                    {
                        case Constant.RECEIVE_ORDER_BEGIN_OK:
                            this.DownloadStatus = true;
                            break;
                        case Constant.RECEIVE_ORDER_BEGIN_ERROR:
                        case Constant.RECEIVE_ORDER_BEGIN_ERROR_DISK:
                        default:
                            try
                            {
                                this.DownloadThread.Abort();
                            }
                            finally
                            {
                                this.DownloadStatus = false;
                                this.IsSending = false;
                                this.DownloadProgressDelegate("", 0);
                                this.CallBack.SendError(devicename, Order);
                                this.CloseDevice();
                            }
                            break;
                    }
                    break;
                case Constant.ORDER_PUT:
                    switch (rxStr)
                    {
                        case Constant.RECEIVE_ORDER_PUT:
                            this.SendDataPackage();
                            break;
                        case Constant.RECEIVE_ORDER_SENDNEXT:
                            this.SendDataPackage();
                            break;
                        case Constant.RECEIVE_ORDER_DONE:
                            this.DownloadStatus = true;
                            break;
                        default:
                            try
                            {
                                this.DownloadThread.Abort();
                            }
                            finally
                            {
                                this.DownloadStatus = false;
                                this.IsSending = false;
                                this.DownloadProgressDelegate("", 0);
                                this.CallBack.SendError(devicename, this.Order);
                                this.CloseDevice();
                            }
                            break;
                    }
                    break;
                case Constant.ORDER_END_SEND:
                    switch (rxStr.Split(':')[0])
                    {
                        case Constant.RECEIVE_ORDER_ENDSEND_OK:
                            this.DownloadStatus = true;
                            this.IsSending = false;
                            this.DownloadProgressDelegate("", 0);
                            this.CallBack.SendCompleted(devicename, this.Order);
                            this.CloseDevice();
                            break;
                        case Constant.RECEIVE_ORDER_ENDSEND_ERROR:
                        default:
                            try
                            {
                                this.DownloadThread.Abort();
                            }
                            finally
                            {
                                this.DownloadStatus = false;
                                this.IsSending = false;
                                this.DownloadProgressDelegate("", 0);
                                CallBack.SendError(devicename, this.Order);
                                this.CloseDevice();
                            }
                            break;
                    }
                    break;
                case Constant.ORDER_PUT_PARAM:
                    switch (rxStr)
                    {
                        case Constant.RECEIVE_ORDER_PUT_PARAM:
                            this.SendDataPackage();
                            break;
                        case Constant.RECEIVE_ORDER_DONE:
                            this.IsSending = false;
                            this.CallBack.SendCompleted(devicename, this.Order);
                            this.CloseDevice();
                            break;
                    }
                    break;
                case Constant.ORDER_UPDATE:
                    switch (rxStr)
                    {
                        case Constant.RECEIVE_ORDER_UPDATE_OK:
                            this.SendDataPackage();
                            break;
                        case Constant.RECEIVE_ORDER_SENDNEXT:
                            this.SendDataPackage();
                            break;
                        case Constant.RECEIVE_ORDER_DONE:
                            this.IsSending = false;
                            this.CallBack.SendCompleted(devicename, this.Order);
                            this.CloseDevice();
                            break;
                        case Constant.RECEIVE_ORDER_UPDATE_ERROR:
                            try
                            {
                                if (null != this.UpdateThread)
                                {
                                    this.UpdateThread.Abort();
                                }
                            }
                            finally
                            {
                                this.IsSending = false;
                                this.DownloadProgressDelegate("", 0);
                                this.CallBack.SendError(devicename, this.Order);
                                this.CloseDevice();
                            }
                            break;
                    }
                    break;
                case Constant.ORDER_GET_PARAM:
                    this.IsSending = false;
                    string data = Encoding.Default.GetString(rxBuff);
                    CSJ_Hardware hardware = null;
                    if (!data.Equals(Constant.RECEIVE_ORDER_GET_PARAM))
                    {
                        hardware = DmxDataConvert.GetInstance().GetHardware(rxBuff) as CSJ_Hardware;
                    }
                    this.GetParamDelegate(hardware);
                    this.CloseDevice();
                    break;
                default:
                    switch (rxStr.Split(':')[0])
                    {
                        case Constant.RECEIVE_ORDER_OTHER_OK:
                            this.CallBack.SendCompleted(devicename, this.Order);
                            break;
                        default:
                            this.CallBack.SendError(devicename, Order);
                            break;
                    }
                    this.IsSending = false;
                    this.CloseDevice();
                    break;
            }
        }
        protected void SendComplected()
        {
            try
            {
                this.IsReceive = false;
                this.IsTimeOutThreadStart = true;
                this.TimeIndex = 0;
                if (this.Order.Equals(Constant.ORDER_PUT) || this.Order.Equals(Constant.ORDER_UPDATE))
                {
                    int progress = Convert.ToInt16(this.CurrentDownloadCompletedSize / (this.DownloadFileToTalSize * 1.0) * 100);
                    this.DownloadProgressDelegate(this.CurrentFileName, progress);
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        public void DownloadProject(DBWrapper wrapper, string configPath, IReceiveCallBack receiveCallBack, DownloadProgressDelegate download)
        {
            try
            {
                if (!this.IsSending)
                {
                    this.DownloadProgressDelegate = download;
                    this.Wrapper = wrapper;
                    this.ConfigPath = configPath;
                    this.CallBack = receiveCallBack;
                    this.IsSending = true;
                    this.DownloadThread = new Thread(new ThreadStart(DownloadStart))
                    {
                        IsBackground = true
                    };
                    this.DownloadThread.Start();
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                throw;
            }
        }
        protected void DownloadStart()
        {
            try
            {
                string fileName = string.Empty;
                string fileSize = string.Empty;
                string fileCRC = string.Empty;
                byte[] crcBuff = new byte[2];
                this.DownloadFileToTalSize = 0;
                this.CurrentDownloadCompletedSize = 0;
                CSJ_Project project = DmxDataConvert.GetInstance().GetCSJProjectFiles(this.Wrapper, this.ConfigPath);
                ScenesInitData scenesInitData = new ScenesInitData(project);
                this.DownloadFileToTalSize = project.GetProjectFileSize();
                if (null != scenesInitData)
                {
                    this.DownloadFileToTalSize += scenesInitData.GetData().Length;
                }
                this.DownloadStatus = false;
                this.SendData(null, Constant.ORDER_BEGIN_SEND, null);
                if (project.CFiles != null)
                {
                    foreach (ICSJFile file in project.CFiles)
                    {
                        fileName = "C" + ((file as CSJ_C).SceneNo + 1) + ".bin";
                        fileSize = file.GetData().Length.ToString();
                        crcBuff = CRCTools.GetInstance().GetCRC(file.GetData());
                        fileCRC = Convert.ToInt32((crcBuff[0] & 0xFF) | ((crcBuff[1] & 0xFF) << 8)) + "";
                        while (true)
                        {
                            if (this.DownloadStatus)
                            {
                                this.CurrentFileName = fileName;
                                this.SendData(file.GetData(), Constant.ORDER_PUT, new string[] { fileName, fileSize, fileCRC });
                                this.DownloadStatus = false;
                                break;
                            }
                        }
                        if ((file as CSJ_C).SceneNo == Constant.SCENE_ALL_ON || (file as CSJ_C).SceneNo == Constant.SCENE_ALL_OFF)
                        {
                            while (true)
                            {
                                if (this.DownloadStatus)
                                {
                                    int sceneno = ((file as CSJ_C).SceneNo == Constant.SCENE_ALL_ON) ? Constant.SCENE_ALL_ON_NO : Constant.SCENE_ALL_OFF_NO;
                                    fileName = "C" + (sceneno + 1) + ".bin";
                                    this.CurrentFileName = fileName;
                                    this.SendData(file.GetData(), Constant.ORDER_PUT, new string[] { fileName, fileSize, fileCRC });
                                    this.DownloadStatus = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (project.MFiles != null)
                {
                    foreach (ICSJFile file in project.MFiles)
                    {
                        fileName = "M" + ((file as CSJ_M).SceneNo + 1) + ".bin";
                        fileSize = file.GetData().Length.ToString();
                        crcBuff = CRCTools.GetInstance().GetCRC(file.GetData());
                        fileCRC = Convert.ToInt32((crcBuff[0] & 0xFF) | ((crcBuff[1] & 0xFF) << 8)) + "";
                        while (true)
                        {
                            if (this.DownloadStatus)
                            {
                                this.CurrentFileName = fileName;
                                this.SendData(file.GetData(), Constant.ORDER_PUT, new string[] { fileName, fileSize, fileCRC });
                                this.DownloadStatus = false;
                                break;
                            }
                        }
                        if ((file as CSJ_M).SceneNo == Constant.SCENE_ALL_ON || (file as CSJ_M).SceneNo == Constant.SCENE_ALL_OFF)
                        {
                            while (true)
                            {
                                if (this.DownloadStatus)
                                {
                                    int sceneno = ((file as CSJ_M).SceneNo == Constant.SCENE_ALL_ON) ? Constant.SCENE_ALL_ON_NO : Constant.SCENE_ALL_OFF_NO;
                                    fileName = "M" + (sceneno + 1) + ".bin";
                                    this.CurrentFileName = fileName;
                                    this.SendData(file.GetData(), Constant.ORDER_PUT, new string[] { fileName, fileSize, fileCRC });
                                    this.DownloadStatus = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                fileName = "Config.bin";
                fileSize = project.ConfigFile.GetData().Length.ToString();
                crcBuff = CRCTools.GetInstance().GetCRC(project.ConfigFile.GetData());
                fileCRC = Convert.ToInt32((crcBuff[0] & 0xFF) | ((crcBuff[1] & 0xFF) << 8)) + "";
                while (true)
                {
                    if (this.DownloadStatus)
                    {
                        this.CurrentFileName = fileName;
                        this.SendData(project.ConfigFile.GetData(), Constant.ORDER_PUT, new string[] { fileName, fileSize, fileCRC });
                        this.DownloadStatus = false;
                        break;
                    }
                }
                fileName = "GradientData.bin";
                fileSize = scenesInitData.GetData().Length.ToString();
                crcBuff = CRCTools.GetInstance().GetCRC(scenesInitData.GetData());
                fileCRC = Convert.ToInt32((crcBuff[0] & 0xFF) | ((crcBuff[1] & 0xFF) << 8)) + "";
                while (true)
                {
                    if (this.DownloadStatus)
                    {
                        this.CurrentFileName = fileName;
                        this.SendData(scenesInitData.GetData(), Constant.ORDER_PUT, new string[] { fileName, fileSize, fileCRC });
                        this.DownloadStatus = false;
                        break;
                    }
                }
                while (true)
                {
                    if (true)
                    {
                        if (this.DownloadStatus)
                        {
                            this.SendData(null, Constant.ORDER_END_SEND, null);
                            this.DownloadStatus = false;
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        public void PutParam(string filePath, IReceiveCallBack receiveCallBack)
        {
            try
            {
                if (!this.IsSending)
                {
                    this.CallBack = receiveCallBack;
                    this.HardwarePath = filePath;
                    this.IsSending = true;
                    ICSJFile file = DmxDataConvert.GetInstance().GetHardware(filePath);
                    byte[] data = file.GetData();
                    string fileName = @"Hardware.bin";
                    string fileSize = data.Length.ToString();
                    byte[] crcBuff = CRCTools.GetInstance().GetCRC(data);
                    string fileCrc = Convert.ToInt32((crcBuff[0] & 0xFF) | ((crcBuff[1] & 0xFF) << 8)) + "";
                    this.SendData(data, Constant.ORDER_PUT_PARAM, new string[] { fileName, fileSize, fileCrc });
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        public void GetParam(GetParamDelegate getParam, IReceiveCallBack receiveCallBack)
        {
            try
            {
                if (!this.IsSending)
                {
                    this.CallBack = receiveCallBack;
                    this.GetParamDelegate = getParam;
                    this.IsSending = true;
                    this.SendData(null, Constant.ORDER_GET_PARAM, null);
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        public void SendOrder(string order, string[] parameters, IReceiveCallBack receiveCallBack)
        {
            try
            {
                this.CallBack = receiveCallBack;
                this.SendData(null, order, parameters);
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        public void Update(string filePath,IReceiveCallBack receiveCallBack,DownloadProgressDelegate download)
        {
            try
            {
                if (!this.IsSending)
                {
                    if (File.Exists(filePath))
                    {
                        this.IsSending = true;
                        this.UpdateFilePath = filePath;
                        this.DownloadProgressDelegate = download;
                        this.CallBack = receiveCallBack;
                        this.UpdateThread = new Thread(new ThreadStart(UpdateStart))
                        {
                            IsBackground = true
                        };
                        this.UpdateThread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                throw;
            }
        }
        protected void UpdateStart()
        {
            FileInfo info = new FileInfo(this.UpdateFilePath);
            FileStream fileStream = File.OpenRead(this.UpdateFilePath);
            this.DownloadFileToTalSize = 0;
            this.CurrentDownloadCompletedSize = 0;
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            string fileSize = data.Length.ToString();
            this.DownloadFileToTalSize = data.Length;
            string fileName = info.Name;
            byte[] crc = CRCTools.GetInstance().GetCRC(data);
            string fileCrc = Convert.ToInt32((crc[0] & 0xFF) | ((crc[1] & 0xFF) << 8)) + "";
            this.SendData(data, Constant.ORDER_UPDATE, new string[] { fileName, fileSize, fileCrc});
        }
        public class PacketSize
        {
            public const int BYTE_512 = 504;
            public const int BYTE_1024 = 1016;
            public const int BYTE_2048 = 2040;

        }
    }
}
