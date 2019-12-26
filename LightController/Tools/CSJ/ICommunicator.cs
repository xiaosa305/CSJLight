using LightController.Ast;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

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
        protected long DownloadFileToTalSize { get; set; }//工程项目文件总大小
        protected long CurrentDownloadCompletedSize { get; set; }//当前文件大小
        protected int TimeOutCount { get; set; }//超时计数
        protected int PackageSize { get; set; }//数据包大小
        protected long PackageCount { get; set; }//数据包总数
        protected long Package_Index { get; set; }//当前数据包编号
        protected int Addr { get; set; }//设备地址
        protected int TimeIndex { get; set; }//超时时间计数
        protected string CurrentFileName { get; set; }//当前下载文件名称
        protected string HardwarePath { get; set; }//硬件配置文件路径
        protected string ConfigPath { get; set; }//全局配置文件路径
        protected string Order { get; set; }//当前执行命令
        protected string SecondOrder { get; set; }//灯控以及中控二级命令
        protected string DeviceName { get; set; }//当前设备名称
        protected string[] Parameters { get; set; }//命令参数组
        protected string UpdateFilePath { get; set; }//硬件更新文件路径
        protected byte[] Data { get; set; }//文件数据

        protected DBWrapper Wrapper { get; set; }//数据库文件
        protected Thread DownloadThread { get; set; }//下载工程项目线程
        protected Thread TimeOutThread { get; set; }//超时处理线程
        protected Thread UpdateThread { get; set; }//硬件更新线程
        protected ICommunicatorCallBack CallBack { get; set; }//命令结束执行回调
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
        //TODO 网络下载专属方法
        protected void SendDataPackageByDownload(byte[] data, int datalength ,bool isLastPackage)
        {
            try
            {
                this.Package_Index++;
                List<byte> package = new List<byte>();
                List<byte> packageData = new List<byte>();
                for (int i = 0; i < datalength; i++)
                {
                    packageData.Add(data[i]);
                }
                byte[] packageDataSize = new byte[]
                {
                    Convert.ToByte(datalength & 0xFF),
                    Convert.ToByte((datalength >> 8) & 0xFF)
                };
                byte packageMark = isLastPackage ? Convert.ToByte(Constant.MARK_DATA_END, 2) : Convert.ToByte(Constant.MARK_DATA_NO_END, 2);
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
                        if (IsReceive)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }
                    if (!this.IsReceive)
                    {
                        Console.WriteLine();
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
                                    if (null != DownloadThread)
                                    {
                                        this.DownloadStatus = false;
                                        this.DownloadThread.Abort();
                                    }
                                }
                                finally
                                {
                                    this.CallBack.UpdateProgress("", "", 0);
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
                                    this.CallBack.UpdateProgress("", "", 0);
                                }
                            }
                            CSJLogs.GetInstance().DebugLog("超时，操作失败");
                            this.CallBack.Error(deviceName,"超时");
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
                        this.IsTimeOutThreadStart = false;
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
            this.IsReceive = true;
            this.IsTimeOutThreadStart = false;
            this.TimeIndex = Constant.TIMEOUT;
            Thread.Sleep(1);
            string devicename = this.DeviceName;
            string rxStr = Encoding.UTF8.GetString(rxBuff, 0, rxCount);
            Console.WriteLine("通信接收：" + rxStr);
            switch (this.Order)
            {
                case Constant.ORDER_BEGIN_SEND:
                    switch (rxStr.Split(':')[0])
                    {
                        case Constant.RECEIVE_ORDER_BEGIN_OK:
                            string value = rxStr.Split(':')[1].Split(' ')[1];
                            long.TryParse(value, out long intValue);
                            long DiskUsableSize = intValue * 1024 * 1024;
                            if (DiskUsableSize >= DownloadFileToTalSize)
                            {
                                Console.WriteLine("XIAOSA:" + "" + "BeginSend" + "操作成功");
                                this.DownloadStatus = true;
                            }
                            else
                            {
                                try
                                {
                                    if (null != this.DownloadThread)
                                    {
                                        this.DownloadThread.Abort();
                                    }
                                }
                                finally
                                {
                                    Console.WriteLine("XIAOSA:" + "" + "BeginSend" + "操作失败==>设备空间不足");
                                    this.DownloadStatus = false;
                                    this.IsSending = false;
                                    this.CallBack.UpdateProgress("", "", 0);
                                    this.CallBack.Error(devicename, "目标设备空间不足，更新失败");
                                    this.CloseDevice();
                                }
                            }
                            break;
                        case Constant.RECEIVE_ORDER_BEGIN_ERROR:
                            try
                            {
                                if (null != this.DownloadThread)
                                {
                                    this.DownloadThread.Abort();
                                }
                            }
                            finally
                            {
                                Console.WriteLine("XIAOSA:" + "" + "BeginSend" + "操作失败");
                                this.DownloadStatus = false;
                                this.IsSending = false;
                                this.CallBack.UpdateProgress("", "", 0);
                                this.CallBack.Error(devicename, "更新失败");
                                this.CloseDevice();
                            }
                            break;
                        case Constant.RECEIVE_ORDER_BEGIN_ERROR_DISK:
                        default:
                            try
                            {
                                if (null != this.DownloadThread)
                                {
                                    this.DownloadThread.Abort();
                                }
                            }
                            finally
                            {
                                Console.WriteLine("XIAOSA:" + "" + "BeginSend" + "操作失败");
                                this.DownloadStatus = false;
                                this.IsSending = false;
                                this.CallBack.UpdateProgress("", "", 0);
                                this.CallBack.Error(devicename, "设备未插入SD卡，更新失败");
                                this.CloseDevice();
                            }
                            break;
                    }
                    break;
                case Constant.ORDER_PUT:
                    switch (rxStr)
                    {
                        case Constant.RECEIVE_ORDER_PUT:
                            Console.WriteLine("XIAOSA:" + CurrentFileName + "Put" + "操作成功");
                            this.DownloadStatus = true;
                            break;
                        case Constant.RECEIVE_ORDER_SENDNEXT:
                            Console.WriteLine("XIAOSA:" + CurrentFileName + "发送数据包" + "操作成功");
                            this.DownloadStatus = true;
                            break;
                        case Constant.RECEIVE_ORDER_DONE:
                            Console.WriteLine("XIAOSA:" + CurrentFileName + "全部发送成功" + "");
                            this.DownloadStatus = true;
                            break;
                        default:
                            try
                            {
                                if (null != this.DownloadThread)
                                {
                                    this.DownloadThread.Abort();
                                }
                            }
                            finally
                            {
                                Console.WriteLine("XIAOSA:" + "" + "Put" + "操作失败");
                                this.DownloadStatus = false;
                                this.IsSending = false;
                                this.CallBack.UpdateProgress("", "", 0);
                                this.CallBack.Error(devicename, CurrentFileName + "更新失败");
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
                            this.CallBack.UpdateProgress("", "", 0);
                            this.CallBack.Completed(devicename);
                            this.CloseDevice();
                            Console.WriteLine("XIAOSA:" + "" + "EndSend" + "操作成功");
                            break;
                        case Constant.RECEIVE_ORDER_ENDSEND_ERROR:
                        default:
                            try
                            {
                                if (null != this.DownloadThread)
                                {
                                    this.DownloadThread.Abort();
                                }
                            }
                            finally
                            {
                                Console.WriteLine("XIAOSA:" + "" + "EndSend" + "操作失败");
                                this.DownloadStatus = false;
                                this.IsSending = false;
                                this.CallBack.UpdateProgress("", "", 0);
                                CallBack.Error(devicename,"更新失败");
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
                            this.CallBack.Completed(devicename);
                            this.CloseDevice();
                            break;
                        default:
                            this.IsSending = false;
                            this.CallBack.Error(devicename, "硬件配置文件更新失败");
                            this.CloseDevice();
                            break;
                    }
                    break;
                case Constant.ORDER_UPDATE:
                    switch (rxStr)
                    {
                        case Constant.RECEIVE_ORDER_UPDATE_OK:
                        case Constant.RECEIVE_ORDER_SENDNEXT:
                            this.SendDataPackage();
                            break;
                        case Constant.RECEIVE_ORDER_DONE:
                            this.IsSending = false;
                            this.CallBack.Completed(devicename);
                            this.CloseDevice();
                            break;
                        case Constant.RECEIVE_ORDER_UPDATE_ERROR:
                        default:
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
                                this.CallBack.UpdateProgress("", "", 0);
                                this.CallBack.Error(devicename, "更新失败");
                                this.CloseDevice();
                            }
                            break;
                    }
                    break;
                case Constant.ORDER_GET_PARAM:
                    try
                    {
                        string data = Encoding.Default.GetString(rxBuff);
                        CSJ_Hardware hardware = null;
                        if (!data.Equals(Constant.RECEIVE_ORDER_GET_PARAM))
                        {
                            hardware = new CSJ_Hardware(rxBuff);
                            this.IsSending = false;
                            this.CallBack.GetParam(hardware);
                            this.CallBack.Completed(devicename);
                        }
                        else
                        {
                            this.IsSending = false;
                            this.CallBack.Error(devicename, "更新失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        CSJLogs.GetInstance().ErrorLog(ex);
                        this.IsSending = false;
                        this.CallBack.Error(devicename, "更新失败");
                    }
                    finally
                    {
                        this.CloseDevice();
                    }
                    break;
                case Constant.ORDER_START_DEBUG:
                    try
                    {
                        string data = Encoding.Default.GetString(rxBuff);
                        switch (data)
                        {
                            case Constant.RECEIVE_ORDER_START_DEBUG_OK:
                                CallBack.Completed(devicename);
                                break;
                            case Constant.RECEIVE_ORDER_START_DEBUG_ERROR:
                            default:
                                CallBack.Error(devicename, "网络调试启动失败");
                                this.CloseDevice();
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        CSJLogs.GetInstance().ErrorLog(ex);
                        CallBack.Error(devicename, "网络调试启动失败");
                        this.CloseDevice();
                    }
                    break;
                case Constant.ORDER_END_DEBUG:
                    CallBack.Completed(devicename);
                        this.CloseDevice();
                    break;
                default:
                    switch (rxStr.Split(':')[0])
                    {
                        case Constant.RECEIVE_ORDER_OTHER_OK:
                            this.IsSending = false;
                            this.CallBack.Completed(devicename);
                            break;
                        default:
                            this.IsSending = false;
                            this.CallBack.Error(devicename,"操作失败");
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
                this.TimeIndex = 0;
                this.IsReceive = false;
                this.IsTimeOutThreadStart = true;
                Thread.Sleep(1);
                if (this.Order.Equals(Constant.ORDER_PUT) || this.Order.Equals(Constant.ORDER_UPDATE))
                {
                    int progress = Convert.ToInt16(this.CurrentDownloadCompletedSize / (this.DownloadFileToTalSize * 1.0) * 100);
                    this.CallBack.UpdateProgress(this.DeviceName, this.CurrentFileName, progress);
                    //TODO待删除
                    //this.DownloadProgressDelegate(this.CurrentFileName, progress);
                }
            }
            catch (Exception ex)
            {
                this.CloseDevice();
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        public void DownloadProject(DBWrapper wrapper, string configPath, ICommunicatorCallBack receiveCallBack)
        {
            try
            {
                if (!this.IsSending)
                {
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
                this.IsSending = false;
                if (null != this.CallBack)
                {
                    this.CallBack.Error(this.DeviceName, "网络下载启动失败");
                }
            }
        }
        protected void DownloadStart()
        {
            string projectDirPath = Application.StartupPath + @"\DataCache\Download\CSJ";
            this.CurrentDownloadCompletedSize = 0;
            this.DownloadFileToTalSize = 0;
            string fileName = string.Empty;
            string fileSize = string.Empty;
            string fileCRC = string.Empty;
            byte[] readBuff = new byte[this.PackageSize];
            int readSize = 0;
            this.TimeIndex = 0;
            bool TimeOutIsStart = false;
            this.DownloadStatus = false;
            try
            {
                //读取所有文件大小
                if (Directory.Exists(projectDirPath))
                {
                    foreach (string filePath in Directory.GetFileSystemEntries(projectDirPath))
                    {
                        this.DownloadFileToTalSize += (new FileInfo(filePath)).Length;
                    }
                    if (this.DownloadFileToTalSize == 0)
                    {
                        return;
                    }
                    this.SendData(null, Constant.ORDER_BEGIN_SEND, null);
                    Console.WriteLine("XIAOSA:" + "" + "BeginSend" + "发送完成");
                    while (true)
                    {
                        if (DownloadStatus)
                        {
                            this.DownloadStatus = false;
                            break;
                        }
                    }
                    foreach (string filePath in Directory.GetFileSystemEntries(projectDirPath))
                    {
                        FileInfo info = new FileInfo(filePath);
                        fileName = info.Name;
                        fileSize = info.Length.ToString();
                        byte[] crcBuff = CRCTools.GetInstance().GetCRC(filePath);
                        fileCRC = Convert.ToInt32((crcBuff[0] & 0xFF) | ((crcBuff[1] & 0xFF) << 8)) + "";
                        this.CurrentFileName = fileName;
                        this.SendData(null, Constant.ORDER_PUT, new string[] { fileName, fileSize, fileCRC });
                        Console.WriteLine("XIAOSA:" +  fileName + "Put" + "发送完成");
                        while (true)
                        {
                            if (DownloadStatus)
                            {
                                this.DownloadStatus = false;
                                break;
                            }
                        }
                        using (FileStream fileStream = info.OpenRead())
                        {
                            while ((readSize = fileStream.Read(readBuff,0,readBuff.Length)) > 0)
                            {
                                if (readSize < this.PackageSize)
                                {
                                    SendDataPackageByDownload(readBuff, readSize, true);
                                }
                                else
                                {
                                    SendDataPackageByDownload(readBuff, readSize, false);
                                }
                                Console.WriteLine("XIAOSA:" + fileName + "数据包" + "发送完成");
                                while (true)
                                {
                                    if (DownloadStatus)
                                    {
                                        Console.WriteLine("XIAOSA:" + fileName + "数据包" + "接收成功");
                                        this.DownloadStatus = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    this.SendData(null, Constant.ORDER_END_SEND, null);
                    Console.WriteLine("XIAOSA:" + "" + "SendData" + "发送完成");
                }
            }
            catch(Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                this.IsSending = false;
                if (!TimeOutIsStart)
                {
                    if (null != this.CallBack)
                    {
                        this.CallBack.Error(this.DeviceName, fileName + " 下载失败");
                    }
                }
            }
              
        }
        public void PutParam(string filePath, ICommunicatorCallBack receiveCallBack)
        {
            bool TimeOutIsStart = false;
            try
            {
                if (!this.IsSending)
                {
                    this.CallBack = receiveCallBack;
                    this.HardwarePath = filePath;
                    this.IsSending = true;
                    ICSJFile file = new CSJ_Hardware(filePath);
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
                this.IsSending = false;
                if (!TimeOutIsStart)
                {
                    if (null != this.CallBack)
                    {
                        this.CallBack.Error(this.DeviceName, "更新硬件配置谢谢失败");
                    }
                }
            }
        }
        public void GetParam(ICommunicatorCallBack receiveCallBack)
        {
            bool TimeOutIsStart = false;
            try
            {
                if (!this.IsSending)
                {
                    this.CallBack = receiveCallBack;
                    this.IsSending = true;
                    this.SendData(null, Constant.ORDER_GET_PARAM, null);
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                this.IsSending = false;
                if (!TimeOutIsStart)
                {
                    if (null != this.CallBack)
                    {
                        this.CallBack.Error(this.DeviceName, "读取硬件配置信息失败");
                    }
                }
            }
        }
        public void SendOrder(string order, string[] parameters, ICommunicatorCallBack receiveCallBack)
        {
            bool TimeOutIsStart = false;
            try
            {
                this.CallBack = receiveCallBack;
                this.SendData(null, order, parameters);
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                this.IsSending = false;
                if (!TimeOutIsStart)
                {
                    if (null != this.CallBack)
                    {
                        this.CallBack.Error(this.DeviceName,"命令发送失败");
                    }
                }
            }
        }
        public void Update(string filePath, ICommunicatorCallBack receiveCallBack)
        {
            try
            {
                if (!this.IsSending)
                {
                    if (File.Exists(filePath))
                    {
                        this.IsSending = true;
                        this.UpdateFilePath = filePath;
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
                this.IsSending = false;
                if (null != this.CallBack)
                {
                    this.CallBack.Error(this.DeviceName, "硬件更新失败");
                }
            }
        }
        public void StartIntenetPreview(int timeFactory, ICommunicatorCallBack receiveCallBack)
        {
            this.CallBack = receiveCallBack;
            SendData(null, Constant.ORDER_START_DEBUG, new string[] {Convert.ToString(timeFactory)});
        }
        public void StopIntenetPreview(ICommunicatorCallBack receiveCallBack)
        {
            this.CallBack = receiveCallBack;
            SendData(null, Constant.ORDER_END_DEBUG, null);
        }
        protected void UpdateStart()
        {
            bool TimeOutIsStart = false;
            try
            {
                FileInfo info = new FileInfo(this.UpdateFilePath);
                FileStream fileStream = File.OpenRead(this.UpdateFilePath);
                this.DownloadFileToTalSize = 0;
                this.CurrentDownloadCompletedSize = 0;
                byte[] data = new byte[fileStream.Length];
                fileStream.Read(data, 0, data.Length);
                string fileSize = data.Length.ToString();
                this.DownloadFileToTalSize = data.Length;
                //string fileName = info.Name;
                string fileName = "Update.xbin";
                this.CurrentFileName = fileName;
                byte[] crc = CRCTools.GetInstance().GetCRC(data);
                string fileCrc = Convert.ToInt32((crc[0] & 0xFF) | ((crc[1] & 0xFF) << 8)) + "";
                this.SendData(data, Constant.ORDER_UPDATE, new string[] { fileName, fileSize, fileCrc });
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                this.IsSending = false;
                if (!TimeOutIsStart)
                {
                    if (null != this.CallBack)
                    {
                        //TODO 待删除
                        //this.CallBack.SendError(this.DeviceName, this.Order);
                        this.CallBack.Error(this.DeviceName,"文件读取失败");
                    }
                }
            }
        }


        //910读取灯控数据
        public void NewLightControlRead()
        {
            if (!this.IsSending)
            {
                this.IsSending = true;
                //TODO 发送命令获取灯控数据
                this.SecondOrder = "rg";
                SendData(null, "LightControl", new string[] { "rg" });
            }
        }

        //810读取灯控数据
        public void OldLightControlRead()
        {
            if (!this.IsSending)
            {
                this.IsSending = true;
            }
            this.Send(Encoding.Default.GetBytes("zg"));
            while (DownloadStatus)
            {
                this.Send(Encoding.Default.GetBytes("rg"));
            }
        }
        //910下载灯控数据
        public void NewLightControlDownload(LightControlData lightControl)
        {
            if (!this.IsSending)
            {
                this.IsSending = true;
                //TODO 发送命令下载灯控数据到设备
                this.SecondOrder = "dg";
                SendData(lightControl.GetData(), "LightControl",new string[] { "dg" });
            }
        }
        //910调试灯控数据
        public void NewLightControlDebug()
        {
            if (!this.IsSending)
            {
                this.IsSending = true;
                //TODO 发送调试信息=>带参命令
                byte[] debugInfo = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                this.SecondOrder = "yg";
                SendData(null, "LightControl", new string[] { "yg", debugInfo[0].ToString(), debugInfo[1].ToString(), debugInfo[2].ToString(), debugInfo[3].ToString(), debugInfo[4].ToString(), debugInfo[5].ToString(), debugInfo[6].ToString(), debugInfo[7].ToString(), debugInfo[8].ToString() });
            }
        }
    }
}
