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
        public const int BUFFER_SIZE = 1024;
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
        public int BuffCount { get; set; }
        private int Pakege_Count { get; set; }
        private int Pakege_No { get; set; }
        private byte[] Data { get; set; }
        private Thread TimeOutThread { get; set; }
        private ORDER MOrder { get; set; }
        private RECEIVE MReceive { get; set; }
        private string[] Strs { get; set; }
        private int PakegeSize { get; set; }
        private bool IsSendCompleted { get; set; }

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
            BuffCount = 0;
            TimeOutThread = new Thread(new ThreadStart(TimeOut));
            TimeOutThread.IsBackground = true;
            string addr = Socket.RemoteEndPoint.ToString();
            Ip = addr.Split(':')[0];
            int.TryParse(addr.Split(':')[1], out int connPort);
            Port = connPort;
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
        /// 缓冲区剩余字节
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
        /// 发送命令包
        /// </summary>
        /// <param name="data">数据主体</param>
        /// <param name="order">命令</param>
        /// <param name="strarray">备注信息</param>
        public void SendData(byte[] data,ORDER order,string[] strarray)
        {
            IsSendCompleted = false;
            Data = data;
            MOrder = order;
            Strs = strarray;
            try
            {
                TimeOutThread.Abort();
            }
            catch (Exception)
            {
                Console.WriteLine("发送命令包超时，结束超时线程");
               
            }
            finally
            {
                switch (order)
                {
                    case ORDER.Put:
                        Order_Put();
                        break;
                    default:
                        break;
                }
            }
          
        }
        /// <summary>
        /// 返回消息事务管理
        /// </summary>
        /// <param name="receive">返回消息类型</param>
        private void Receive(RECEIVE receive)
        {
            MReceive = receive;
            try
            {
                TimeOutThread.Abort();
            }
            catch (Exception)
            {
                Console.WriteLine("发送数据包超时，结束超时线程");
            }
            finally
            {
                switch (receive)
                {
                    case RECEIVE.Send:
                        Receive_Send();
                        break;

                    case RECEIVE.Done:
                        IsSendCompleted = true;
                        Console.WriteLine("下载数据完成");
                        break;
                    case RECEIVE.Resend:
                        Pakege_No--;
                        Receive(RECEIVE.Send);
                        break;
                    case RECEIVE.Ok:
                        IsSendCompleted = true;
                        Receive(RECEIVE.Send);
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 发送Put命令
        /// </summary>
        /// <param name="data"></param>
        /// <param name="strarray"></param>
        private void Order_Put()
        {
            Pakege_Count = Data.Length / PakegeSize;
            Pakege_Count = (Data.Length % PakegeSize == 0) ? Pakege_Count : Pakege_Count + 1;
            Pakege_No = 0;
            List<byte> pakege = new List<byte>();
            byte[] pakegeOrder = Encoding.Default.GetBytes("put ");
            byte[] pakegeFileCRC = CRCTools.GetInstance().GetCRC(Data);
            byte[] pakegeFileSize = new byte[]
            { Convert.ToByte(Data.Length & 0xFF), Convert.ToByte((Data.Length >> 8) & 0xFF),
                      Convert.ToByte((Data.Length >> 16) & 0xFF), Convert.ToByte((Data.Length >> 24) & 0xFF)
            };
            byte[] pakegeFileName = Encoding.Default.GetBytes(Strs[0]);
            int dataSize = pakegeFileName.Length + pakegeFileSize.Length + pakegeFileCRC.Length;
            byte[] pakegeDataSize = new byte[] { Convert.ToByte(dataSize & 0xFF), Convert.ToByte(dataSize >> 8 & 0xFF) };
            byte[] pakegeHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), pakegeDataSize[0], pakegeDataSize[1], Convert.ToByte(0x01), Convert.ToByte(0x00), Convert.ToByte(0x00) };
            pakege.AddRange(pakegeHead);
            pakege.AddRange(pakegeFileName);
            pakege.AddRange(pakegeFileSize);
            pakege.AddRange(pakegeFileCRC);
            byte[] pakegeCRC = CRCTools.GetInstance().GetCRC(pakege.ToArray());
            pakege[6] = pakegeCRC[0];
            pakege[7] = pakegeCRC[1];
            Socket.BeginSend(pakege.ToArray(), 0, pakege.ToArray().Length, SocketFlags.None, SendCb, this);
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
            byte[] pakegeHead = new byte[] { Convert.ToByte(0xAA), Convert.ToByte(0xBB), Convert.ToByte(0xFF), pakegeDataSize[0], pakegeDataSize[1], Convert.ToByte(0x02), Convert.ToByte(0x00), Convert.ToByte(0x00) };
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
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("TimeOut :" + i);
            }
            if (Pakege_No == 0)
            {
                Console.WriteLine("Resend 0 pakege");
                SendData(Data, MOrder, Strs);
            }
            else
            {
                Console.WriteLine("Resend " + Pakege_No + "pakege");
                Pakege_No--;
                Receive(MReceive);
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
            try
            {
                TimeOutThread.Abort();
            }
            catch (Exception)
            {
                Console.WriteLine("接收到消息结束超时线程");
            }
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
                //string str = conn.Socket.RemoteEndPoint.ToString() + ":" + Encoding.UTF8.GetString(conn.ReadBuff, 0, count);
                string str = Encoding.UTF8.GetString(conn.ReadBuff, 0, count);
                Console.WriteLine("Receive Data : " + str);
                if (str.Equals(Constant.RECEIVE_ORDER_DONE))
                {
                    Receive(RECEIVE.Done);
                }
                else if (str.Equals(Constant.RECEIVE_ORDER_Send))
                {
                    if (Pakege_No == Pakege_Count)
                    {
                        Receive(RECEIVE.Done);
                    }
                    else
                    {
                        Receive(RECEIVE.Send);
                    }
                }
                else if (str.Equals(Constant.RECEIVE_ORDER_ReSend))
                {
                    Receive(RECEIVE.Resend);
                }
                else if (str.Equals(Constant.RECEIVE_ORDER_OK))
                {
                    Receive(RECEIVE.Ok);
                }

                //继续接受
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
            TimeOutThread = new Thread(new ThreadStart(TimeOut));
            TimeOutThread.IsBackground = true;
            TimeOutThread.Start();
        }
    }
}
