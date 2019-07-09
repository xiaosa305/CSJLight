using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
    public class Conn
    {
        //接收缓冲区大小
        private const int BUFFER_SIZE = 1024;
        //socket
        public Socket Socket { get; set; }
        //是否被使用
        public bool IsUse { get; set; }
        //Buff
        public byte[] ReadBuff { get; set; }
        //连接ip地址
        public string Ip { get; set; }
        //连接端口号
        public int Port { get; set; }
        //接收数据偏移量
        public int BuffCount { get; set; }
        //数据包总包数
        private int Pakege_Count { get; set; }
        //当前数据包编号数据包
        private int Pakege_No { get; set; }
        //待发送数据
        private byte[] Data { get; set; }
        //发送数据等待超时处理线程
        private Thread TimeOutThread { get; set; }
        //发送命令
        private string Order { get; set; }
        //接收命令
        private RECEIVE MReceive { get; set; }
        //备注信息
        private string[] Strs { get; set; }
        //发送缓冲区大小
        private int PakegeSize { get; set; }
        //接收消息回调函数
        private IReceiveCallBack ReceiveCallBack { get; set; }
        //发送数据完成状态
        private bool IsSendCompleted { get; set; }
        /// <summary>
        /// 接收数据状态
        /// </summary>
        private bool IsReceive { get; set; }
        private bool IsTiomeOutThreadStart { get; set; }
        private int TimeOutCount { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Conn()
        {
            ReadBuff = new byte[BUFFER_SIZE];
            IsUse = false;
            PakegeSize = 512;
            IsSendCompleted = true;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="socket"></param>
        public void Init(Socket socket)
        {
            this.Socket = socket;
            IsUse = true;
            IsReceive = false;
            IsTiomeOutThreadStart = false;
            BuffCount = 0;
            TimeOutCount = 0;
            TimeOutThread = new Thread(new ThreadStart(TimeOut));
            TimeOutThread.IsBackground = true;
            string addr = Socket.RemoteEndPoint.ToString();
            Ip = addr.Split(':')[0];
            int.TryParse(addr.Split(':')[1], out int connPort);
            Port = connPort;
            TimeOutThread.Start();
        }
        /// <summary>
        /// 设定发送数据缓冲区大小
        /// </summary>
        /// <param name="size"></param>
        public void SetPakegeSize(int size)
        {
            this.PakegeSize = size;
        }
        /// <summary>
        /// 获取发送数据是否完成
        /// </summary>
        /// <returns></returns>
        public bool GetIsSendComplet()
        {
            return IsSendCompleted;
        }
        /// <summary>
        /// 接收缓冲区大小
        /// </summary>
        /// <returns></returns>
        public int BuffRemain()
        {
            return BUFFER_SIZE - BuffCount;
        }
        /// <summary>
        /// 获取客户端地址
        /// </summary>
        /// <returns></returns>
        public string GetAddress()
        {
            if (!IsUse) return "获取地址失败";
            return Socket.RemoteEndPoint.ToString();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (!IsUse) return;
            Console.WriteLine(GetAddress() + "断开连接");
            Socket.Close();
            IsUse = false;
        }
        /// <summary>
        /// 发送命令事务管理
        /// </summary>
        /// <param name="data">数据主体</param>
        /// <param name="order">命令</param>
        /// <param name="strarray">备注信息</param>
        public void SendData(byte[] data, string order, string[] strarray,IReceiveCallBack callBack)
        {
            IsSendCompleted = false;
            Data = data;
            Order = order;
            Strs = strarray;
            ReceiveCallBack = callBack;
            switch (order)
            {
                case Constant.ORDER_PUT:
                    SendOrder();
                    break;
                case Constant.ORDER_BEGIN_SEND:
                    SendOrder();
                    break;
                case Constant.ORDER_END_SEND:
                    SendOrder();
                    break;
                default:
                    SendOrder();
                    break;
            }

        }
        /// <summary>
        /// 返回消息事务管理
        /// </summary>
        /// <param name="receive">返回消息类型</param>
        private void Receive(RECEIVE receive)
        {
            MReceive = receive;
            switch (receive)
            {
                case RECEIVE.Send:
                    Console.WriteLine("发送第" + (Pakege_No + 1) + "包数据，包总数为" + Pakege_Count);
                    Receive_Send();
                    break;
                default:
                    break;
            }
            
        }

        private void SendOrder()
        {
            if (Data != null)
            {
                Pakege_Count = Data.Length / PakegeSize;
                Pakege_Count = (Data.Length % PakegeSize == 0) ? Pakege_Count : Pakege_Count + 1;
                Pakege_No = 0;
            }
            List<byte> pakege = new List<byte>();
            List<byte> pakegeData = new List<byte>();
            byte[] pakegeOrder = Encoding.Default.GetBytes(Order);
            byte[] space = Encoding.Default.GetBytes(" ");
            pakegeData.AddRange(pakegeOrder);
            if (Strs != null)
            {
                pakegeData.AddRange(space);
                for (int i = 0; i < Strs.Length; i++)
                {
                    pakegeData.AddRange(Encoding.Default.GetBytes(Strs[i]));
                    if (i != Strs.Length - 1)
                    {
                        pakegeData.AddRange(space);
                    }
                }
            }
            byte[] pakegeDataLength = new byte[] { Convert.ToByte(pakegeData.Count & 0xFF), Convert.ToByte((pakegeData.Count >> 8) & 0xFF) };
            byte pakegeMark = GetOrderMark();
            byte[] pakegeHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), pakegeDataLength[0], pakegeDataLength[1], pakegeMark, Convert.ToByte(0x00), Convert.ToByte(0x00) };
            pakege.AddRange(pakegeHead);
            pakege.AddRange(pakegeData);
            byte[] pakegeCRC = CRCTools.GetInstance().GetCRC(pakege.ToArray());
            pakege[6] = pakegeCRC[0];
            pakege[7] = pakegeCRC[1];
            Socket.BeginSend(pakege.ToArray(), 0, pakege.ToArray().Length, SocketFlags.None, SendCb,this);
        }
        /// <summary>
        /// 获取协议头标记位
        /// </summary>
        /// <returns></returns>
        private byte GetOrderMark()
        {
            byte result;
            switch (Order)
            {
                case Constant.ORDER_PUT:
                    result = Convert.ToByte(Constant.MARK_ORDER_TAKE_DATA, 2);
                    break;

                default:
                    result = Convert.ToByte(Constant.MARK_ORDER_NO_TAKE_DATA, 2);
                    break;
            }
            return result;
        }

        private byte GetDataMark()
        {
            byte result;
            if (Pakege_No == Pakege_Count)
            {
                result = Convert.ToByte(Constant.MARK_DATA_END, 2);
            }
            else
            {
                result = Convert.ToByte(Constant.MARK_DATA_NO_END, 2);
            }
            return result;
        }
        /// <summary>
        /// 发送数据包
        /// </summary>
        /// <param name="receive"></param>
        private void Receive_Send()
        {
            Pakege_No++;
            byte[] pakegeData;
            List<byte> pakege = new List<byte>();
            if (Pakege_No != Pakege_Count)
            {
                pakegeData = new byte[PakegeSize];
                for (int i = 0; i < PakegeSize; i++)
                {
                    pakegeData[i] = Data[(Pakege_No - 1) * PakegeSize + i];
                }
            }
            else
            {
                pakegeData = new byte[Data.Length - (Pakege_No - 1) * PakegeSize];
                for (int i = 0; i < pakegeData.Length - (Pakege_No - 1) * PakegeSize; i++)
                {
                    pakegeData[i] = Data[(Pakege_No - 1) * 512 + i];
                }
            }
            byte[] pakegeDataSize = new byte[] { Convert.ToByte(pakegeData.Length & 0xFF), Convert.ToByte((pakegeData.Length >> 8) & 0xFF) };
            byte pakegeMark = GetDataMark();
            byte[] pakegeHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), pakegeDataSize[0], pakegeDataSize[1], pakegeMark, Convert.ToByte(0x00), Convert.ToByte(0x00) };
            pakege.AddRange(pakegeHead);
            pakege.AddRange(pakegeData);
            byte[] pakegeCRC = CRCTools.GetInstance().GetCRC(pakege.ToArray());
            pakege[6] = pakegeCRC[0];
            pakege[7] = pakegeCRC[1];
            Socket.BeginSend(pakege.ToArray(), 0, pakege.ToArray().Length, SocketFlags.None, SendCb, this);
        }
        /// <summary>
        /// 超时处理
        /// </summary>
        private void TimeOut()
        {
            while (true)
            {
                if (IsTiomeOutThreadStart)
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        Thread.Sleep(1);
                        if (IsReceive)
                        {
                            break;
                        }
                    }
                    Console.WriteLine("超时" + TimeOutCount + "次");
                    IsTiomeOutThreadStart = false;
                    if (!IsReceive)
                    {
                        if (TimeOutCount > 5)
                        {
                            TimeOutCount = 0;
                            ReceiveCallBack.SendError();
                        }
                        else
                        {
                            TimeOutCount++;
                            ReceiveCallBack.Resend();
                        }
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }
        /// <summary>
        /// 绑定数据接收监听
        /// </summary>
        public void BeginReceive()
        {
            Socket.BeginReceive(ReadBuff, BuffCount, BuffRemain(), SocketFlags.None, ReceiveCb,this);
        }
        /// <summary>
        /// Recevie回调函数
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ReceiveCb(IAsyncResult asyncResult)
        {
            IsTiomeOutThreadStart = false;
            IsReceive = true;
            //获取接受对象
            Conn conn = (Conn)asyncResult.AsyncState;
            try
            {
                int count = conn.Socket.EndReceive(asyncResult);
                //关闭信号
                if (count <= 0)
                {
                    Console.WriteLine("收到 [" + conn.GetAddress() + "] 断开连接");
                    conn.Close();
                    return;
                }
                //数据处理
                string receiveStr = Encoding.UTF8.GetString(conn.ReadBuff, 0, count);
                Console.WriteLine("Receive Data : " + receiveStr);
                switch (Order)
                {
                    case Constant.ORDER_PUT:
                        switch (receiveStr)
                        {
                            case Constant.RECEIVE_ORDER_SENDNEXT:
                                if (Pakege_No == Pakege_Count)
                                {
                                    Console.WriteLine("下载完成");
                                    IsSendCompleted = true;
                                    ReceiveCallBack.SendCompleted();
                                }
                                else
                                {
                                    Receive(RECEIVE.Send);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case Constant.ORDER_BEGIN_SEND:
                        switch (receiveStr.Split(':')[0])
                        {
                            case Constant.RECEIVE_ORDER_BEGIN_OK:
                                ReceiveCallBack.SendCompleted();
                                break;
                            case Constant.RECEIVE_ORDER_END_ERROR:
                                ReceiveCallBack.SendError();
                                break;
                            default:
                                break;
                        }
                        break;
                    case Constant.ORDER_END_SEND:
                        switch (receiveStr.Split(':')[0])
                        {
                            case Constant.RECEIVE_ORDER_END_OK:
                                ReceiveCallBack.SendCompleted();
                                break;
                            case Constant.RECEIVE_ORDER_END_ERROR:
                                ReceiveCallBack.SendError();
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        switch (receiveStr.Split(':')[0])
                        {
                            case Constant.RECEIVE_ORDER_END_OK:
                                ReceiveCallBack.SendCompleted();
                                break;
                            case Constant.RECEIVE_ORDER_END_ERROR:
                                ReceiveCallBack.SendError();
                                break;
                            default:
                                break;
                        }
                        break;
                }
                //继续接收消息
                conn.Socket.BeginReceive(conn.ReadBuff, conn.BuffCount, conn.BuffRemain(), SocketFlags.None, ReceiveCb, conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[" + conn.GetAddress() + "] 断开连接" + "Exception :" + ex.Message);
                conn.Close();
            }
        }
        /// <summary>
        /// Send回调函数
        /// </summary>
        /// <param name="asyncResult"></param>
        private void SendCb(IAsyncResult asyncResult)
        {
            IsReceive = false;
            IsTiomeOutThreadStart = true;
            Console.WriteLine("发送完成");
            
        }
    }
}
