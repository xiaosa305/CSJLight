using LightController.Ast;
using LightController.Tools.CSJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
    public class Conn : ICommunicator
    {
        private const int BUFFER_SIZE = 2048;//接收缓冲区大小
        public Socket Socket { get; set; }//通信套接字
        public bool IsUse { get; set; } //是否被使用
        public byte[] ReadBuff { get; set; } //接收缓存区
        public string Ip { get; set; }//客户端ip地址
        public int Port { get; set; } //客户端端口号
        public int BuffCount { get; set; } //接收数据偏移量

        public Conn()
        {
            ReadBuff = new byte[BUFFER_SIZE];
            IsUse = false;
            PackageSize = Constant.PACKAGE_SIZE_2K;
            Ip = "";
        }
        public void Init(Socket socket)
        {
            this.Socket = socket;
            InitParameters();
            IsUse = true;
            BuffCount = 0;
            TimeOutThread = new Thread(new ThreadStart(TimeOut))
            {
                IsBackground = true
            };
            string addr = Socket.RemoteEndPoint.ToString();
            Ip = addr.Split(':')[0];
            int.TryParse(addr.Split(':')[1], out int connPort);
            Port = connPort;
            TimeOutThread.Start();
        }
        public int BuffRemain()
        {
            return BUFFER_SIZE - BuffCount;
        }
        public string GetAddress()
        {
            if (!IsUse) return "获取地址失败";
            return Socket.RemoteEndPoint.ToString();
        }
        public void BeginReceive()
        {
            Socket.BeginReceive(ReadBuff, BuffCount, BuffRemain(), SocketFlags.None, Recive, this);
        }
        private void SendCb(IAsyncResult asyncResult)
        {
            SendComplected();
        }
        protected override void Send(byte[] txBuff)
        {
            Socket.BeginSend(txBuff, 0, txBuff.ToArray().Length, SocketFlags.None, SendCb, this);
        }
        protected void Recive(IAsyncResult asyncResult)
        {
            Conn conn = (Conn)asyncResult.AsyncState;
            try
            {
                int count = conn.Socket.EndReceive(asyncResult);
                if (count <= 0)
                {
                    CSJLogs.GetInstance().DebugLog("收到 [" + this.Ip + "] 断开连接");
                    conn.CloseDevice();
                    return;
                }
                byte[] rxBuff = conn.ReadBuff;
                byte[] packageHead = new byte[8];
                for (int i = 0; i < packageHead.Length; i++)
                {
                    packageHead[i] = rxBuff[i];
                }
                if (packageHead[0] != Convert.ToByte(0xAA) || packageHead[1] != Convert.ToByte(0xBB) || packageHead[2] != Convert.ToByte(0x00) || packageHead[5] != Convert.ToByte(Constant.MARK_DATA_END, 2))
                {
                    CloseDevice();
                }
                else
                {
                    int packageDataLength = (packageHead[3] & 0xFF) | ((packageHead[4] << 8) & 0xFF);
                    rxBuff[6] = Convert.ToByte(0x00);
                    rxBuff[7] = Convert.ToByte(0x00);
                    byte[] rxData = new byte[count];
                    Array.Copy(rxBuff, 0, rxData, 0, count);
                    byte[] packageCRC = CRCTools.GetInstance().GetCRC(rxData);
                    if (packageCRC[0] != packageHead[6] || packageCRC[1] != packageHead[7])
                    {
                        CloseDevice();
                    }
                    else
                    {
                        byte[] packageData = new byte[packageDataLength];
                        Array.Copy(rxBuff, 8, packageData, 0, packageDataLength);
                        ReceiveMessageManage(packageData, packageDataLength);
                        conn.Socket.BeginReceive(conn.ReadBuff, conn.BuffCount, conn.BuffRemain(), SocketFlags.None, Recive, conn);
                    }
                }

            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                conn.CloseDevice();
            }
        }
        public override void CloseDevice()
        {
            if (!IsUse) return;
            Console.WriteLine(this.Ip + "断开连接");
            this.DownloadStatus = false;
            try
            {
                switch (Order)
                {
                    case Constant.ORDER_PUT_PARAM:
                        break;
                    case Constant.ORDER_GET_PARAM:
                        break;
                    case Constant.ORDER_PUT:
                    case Constant.ORDER_BEGIN_SEND:
                    case Constant.ORDER_END_SEND:
                        this.DownloadThread.Abort();
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                try
                {
                    this.TimeOutThread.Abort();
                }
                finally
                {
                    this.Ip = "";
                    this.DeviceName = null;
                    this.Socket.Close();
                    this.IsSending = false;
                    this.IsUse = false;
                }
            }
        }
    }
}
