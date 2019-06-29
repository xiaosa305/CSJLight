using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

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
            if (IsUse) return;
            Console.WriteLine(GetAddress() + "断开连接");
            Socket.Close();
            IsUse = false;
        }
    }
}
