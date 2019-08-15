using LightController.Ast;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightController.Tools.CSJ
{
    public abstract class ICommunicator
    {
        private bool IsSending { get; set; }
        private bool DownloadStatus { get; set; }
        private int DownloadFileToTalSize { get; set; }
        private int CurrentDownloadCompletedSize { get; set; }
        private string CurrentFileName { get; set; }
        private string HardwarePath { get; set; }
        private string ConfigPath { get; set; }
        private DBWrapper Wrapper { get; set; }
        private IReceiveCallBack CallBack { get; set; }
        private Thread DownloadThread { get; set; }
        private GetParamDelegate GetParamDelegate { get; set; }
        private DownloadProgressDelegate DownloadProgressDelegate { get; set; }

        public abstract void Send();
        public abstract void Recive();
        private void SendData(byte[] data,string order,string[] parameters) { }
        private void SendOrderPackage() { }
        private void SendDataPackage() { }
        private byte GetDataMark()
        {
            return 0x00;
        }
        private byte GetOrderMark()
        {
            return 0x00;
        }
        private void TimeOut() { }
        private void ReceiveMessageManage(byte[] receiveMessage) { }
        private void SendComplected() { }
        public void DownloadProject(DBWrapper wrapper,string configPath,IReceiveCallBack receiveCallBack,DownloadProgressDelegate download)
        {
            if (!this.IsSending)
            {
                this.DownloadProgressDelegate = download;
                this.Wrapper = wrapper;
                this.ConfigPath = configPath;
                this.CallBack = receiveCallBack;
                this.IsSending = true;
                DownloadThread = new Thread(new ThreadStart(DownloadStart))
                {
                    IsBackground = true
                };
                DownloadThread.Start();
            }
        }
        private void DownloadStart()
        {
            string fileName = string.Empty;
            string fileSize = string.Empty;
            this.DownloadFileToTalSize = 0;
            CurrentDownloadCompletedSize = 0;
            CSJ_Project project = DmxDataConvert.GetInstance().GetCSJProjectFiles(this.Wrapper, this.ConfigPath);
            this.DownloadFileToTalSize = project.GetProjectFileSize();
            this.DownloadStatus = false;
            SendData(null,Constant.ORDER_BEGIN_SEND,null);
        }
        public void PutParam(string filePath,IReceiveCallBack receiveCallBack)
        {
            if (!this.IsSending)
            {
                this.CallBack = receiveCallBack;
                this.HardwarePath = filePath;
                this.IsSending = true;
                ICSJFile file = DmxDataConvert.GetInstance().GetHardware();
                byte[] data = file.GetData();
                string fileName = @"Hardware.bin";
                string fileSize = data.Length.ToString();
                byte[] crcBuff = CRCTools.GetInstance().GetCRC(data);
                string fileCrc = crcBuff[0].ToString() + crcBuff[1].ToString();
                SendData(data, Constant.ORDER_PUT_PARAM, new string[] { fileName, fileSize, fileCrc });
            }
        }
        public void GetPatam(GetParamDelegate getParam,IReceiveCallBack receiveCallBack)
        {
            if (!this.IsSending)
            {
                this.CallBack = receiveCallBack;
                this.GetParamDelegate = getParam;
                this.IsSending = true;
                this.SendData(null, Constant.ORDER_GET_PARAM, null);
            }
        }
    }
}
