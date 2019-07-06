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
        //缓冲区大小
        public const int BUFFER_SIZE = 1024;
        //socket
        public Socket Socket { get; set; }
        //是否被使用
        public bool IsUse { get; set; }
        //Buff
        public byte[] ReadBuff { get; set; }
        public int BuffCount { get; set; }
        private int Pakege_Count { get; set; }
        private int Pakege_No { get; set; }
        private byte[] Data { get; set; }
        private Thread TimeOutThread { get; set; }
        private ORDER MOrder { get; set; }
        private RECEIVE MReceive { get; set; }
        private string[] Strs { get; set; }
        //构造函数
        public Conn()
        {
            ReadBuff = new byte[BUFFER_SIZE];
            IsUse = false;
        }

        //初始化
        public void Init(Socket socket)
        {
            this.Socket = socket;
            IsUse = true;
            BuffCount = 0;
            TimeOutThread = new Thread(new ThreadStart(TimeOut));
        }

        //缓冲区剩余字节
        public int BuffRemain()
        {
            return BUFFER_SIZE - BuffCount;
        }

        //获取客户端地址
        public string GetAddress()
        {
            if (!IsUse) return "获取地址失败";
            return Socket.RemoteEndPoint.ToString();
        }

        //关闭
        public void Close()
        {
            if (!IsUse) return;
            Console.WriteLine(GetAddress() + "断开连接");
            Socket.Close();
            IsUse = false;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">数据主体</param>
        /// <param name="order">命令</param>
        /// <param name="strarray">备注信息</param>
        public void SendData(byte[] data,ORDER order,string[] strarray)
        {
            Data = data;
            MOrder = order;
            Strs = strarray;
            switch (order)
            {
                case ORDER.Put:
                    Pakege_Count = data.Length / 512;
                    Pakege_Count = (data.Length % 512 == 0) ? Pakege_Count : Pakege_Count + 1;
                    Pakege_No = 0;
                    List<byte> pakege = new List<byte>();
                    byte[] pakegeOrder = Encoding.Default.GetBytes("put ");
                    byte[] pakegeFileCRC = CRCTools.GetInstance().GetCRC(data);
                    byte[] pakegeFileSize = new byte[] 
                    { Convert.ToByte(data.Length & 0xFF), Convert.ToByte((data.Length >> 8) & 0xFF),
                      Convert.ToByte((data.Length >> 16) & 0xFF), Convert.ToByte((data.Length >> 24) & 0xFF)
                    };
                    byte[] pakegeFileName = Encoding.Default.GetBytes(strarray[0]);
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
                    break;
                default:
                    break;
            }
        }

        private void Send(RECEIVE receive)
        {
            MReceive = receive;
            switch (receive)
            {
                case RECEIVE.Send:
                    Pakege_No++;
                    byte[] pakegeData;
                    List<byte> pakege = new List<byte>();
                    if (Pakege_No != Pakege_Count)
                    {
                        pakegeData = new byte[512];
                        for (int i = 0; i < 512; i++)
                        {
                            pakegeData[i] = Data[(Pakege_No-1) * 512 + i];
                        }
                    }
                    else
                    {
                        pakegeData = new byte[Data.Length - (Pakege_No - 1) * 512];
                        for (int i = 0; i < pakegeData.Length - (Pakege_No -1)* 512; i++)
                        {
                            pakegeData[i] = Data[(Pakege_No-1) * 512 + i];
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
                    break;

                case RECEIVE.Done:
                    TimeOutThread.Abort();
                    Console.WriteLine("下载数据完成");
                    break;
                case RECEIVE.Resend:
                    Pakege_No--;
                    Send(RECEIVE.Send);
                    break;
                case RECEIVE.Ok:
                    Send(RECEIVE.Send);
                    break;
                default:
                    break;
            }
        }

        private void TimeOut()
        {
            Thread.Sleep(1000);
            if (Pakege_No == 0)
            {
                SendData(Data, MOrder, Strs);
            }
            else
            {
                Pakege_No--;
                Send(MReceive);
            }
        }

        public void BeginReceive()
        {
            Socket.BeginReceive(ReadBuff, BuffCount, BuffRemain(), SocketFlags.None, ReceiveCb,this);
        }

        //Recevie回调函数
        private void ReceiveCb(IAsyncResult asyncResult)
        {
            TimeOutThread.Abort();
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
                Console.WriteLine("Receive Data : " + str) ;
                if (str.Equals(Constant.RECEIVE_ORDER_DONE))
                {
                    Send(RECEIVE.Done);
                }
                else if (str.Equals(Constant.RECEIVE_ORDER_Send))
                {
                    Send(RECEIVE.Send);
                }
                else if (str.Equals(Constant.RECEIVE_ORDER_ReSend))
                {
                    Send(RECEIVE.Resend);
                }
                else if (str.Equals(Constant.RECEIVE_ORDER_OK))
                {
                    Send(RECEIVE.Ok);
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

        private void SendCb(IAsyncResult asyncResult)
        {
            TimeOutThread = new Thread(new ThreadStart(TimeOut));
        }
    }
}
