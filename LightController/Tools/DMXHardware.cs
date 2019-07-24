using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    class DMXHardware
    {
        private readonly byte Flag = 0xEA;
        private int Ver { get; set; }
        public int SumUseTimes { get; set; }
        public int DiskFlag { get; set; }
        public string DeviceName { get; set; }
        public int Addr { get; set; }
        public int LinkMode { get; set; }
        public int LinkPort { get; set; }
        public string IP { get; set; }
        public string NetMask { get; set; }
        public string GateWay { get; set; }
        public string Mac { get; set; }
        public int Baud { get; set; }
        public int CurrUseTimes { get; set; }
        public string RemoteHost { get; set; }
        public int RemotePort { get; set; }
        public string DomainName { get; set; }
        public string DomainServer { get; set; }
        public string HardWareID { get; set; }
        public byte[] Heartbeat { get; set; }
        public int HeartbeatCycle { get; set; }

        public DMXHardware(string filePath)
        {
            Test();
        }

        public DMXHardware()
        {
            Test();
        }

        private void Test()
        {
            this.Ver = 1;
            this.SumUseTimes = 5000000;
            this.DiskFlag = 0;
            this.DeviceName = "AOL 001";
            this.Addr = 110;
            this.LinkMode = 0;
            this.LinkPort = 7070;
            this.IP = "192.168.31.15";
            this.NetMask = "255.255.255.0";
            this.GateWay = "192.168.31.1";
            this.Mac = "F1:A5:1B:E2:FA:C9";
            this.Baud = 0;
            this.CurrUseTimes = 1;
            this.RemoteHost = "192.168.31.235";
            this.RemotePort = 7070;
            this.DomainName = "www.baidu.com";
            this.DomainServer = "192.168.31.110";
            this.HardWareID = "0000000000000001";
            this.Heartbeat = new byte[] {0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07};
            this.HeartbeatCycle = 10000;
        }

        public byte[] GetHardware()
        {
            List<byte> data = new List<byte>();
            data.Add(Flag);
            data.Add(Convert.ToByte(Ver));
            byte[] SumUseTimesBuff = new byte[4];
            SumUseTimesBuff[0] = Convert.ToByte((SumUseTimes) & 0xFF);
            SumUseTimesBuff[1] = Convert.ToByte((SumUseTimes >> 8) & 0xFF);
            SumUseTimesBuff[2] = Convert.ToByte((SumUseTimes >> 16) & 0xFF);
            SumUseTimesBuff[3] = Convert.ToByte((SumUseTimes >> 24) & 0xFF);
            data.AddRange(SumUseTimesBuff);
            data.Add(Convert.ToByte(DiskFlag));
            byte[] DeviceNameBuff = Encoding.Default.GetBytes(DeviceName);
            data.AddRange(DeviceNameBuff);
            if (DeviceNameBuff.Length < 16)
            {
                for (int i = 0; i < 16- DeviceNameBuff.Length; i++)
                {
                    data.Add(Convert.ToByte(0x00));
                }
            }
            data.Add(Convert.ToByte(Addr));
            data.Add(Convert.ToByte(LinkMode));
            byte[] LinkPortBuff = new byte[2];
            LinkPortBuff[0] = Convert.ToByte((LinkPort) & 0xFF);
            LinkPortBuff[1] = Convert.ToByte((LinkPort >> 8) & 0xFF);
            data.AddRange(LinkPortBuff);
            data.Add(Convert.ToByte(IP.Split('.')[0]));
            data.Add(Convert.ToByte(IP.Split('.')[1]));
            data.Add(Convert.ToByte(IP.Split('.')[2]));
            data.Add(Convert.ToByte(IP.Split('.')[3]));
            data.Add(Convert.ToByte(NetMask.Split('.')[0]));
            data.Add(Convert.ToByte(NetMask.Split('.')[1]));
            data.Add(Convert.ToByte(NetMask.Split('.')[2]));
            data.Add(Convert.ToByte(NetMask.Split('.')[3]));
            data.Add(Convert.ToByte(GateWay.Split('.')[0]));
            data.Add(Convert.ToByte(GateWay.Split('.')[1]));
            data.Add(Convert.ToByte(GateWay.Split('.')[2]));
            data.Add(Convert.ToByte(GateWay.Split('.')[3]));
            string[] macBuff = Mac.Split(':');
            data.Add(Convert.ToByte(macBuff[0], 16));
            data.Add(Convert.ToByte(macBuff[1], 16));
            data.Add(Convert.ToByte(macBuff[2], 16));
            data.Add(Convert.ToByte(macBuff[3], 16));
            data.Add(Convert.ToByte(macBuff[4], 16));
            data.Add(Convert.ToByte(macBuff[5], 16));
            data.Add(Convert.ToByte(Baud));
            data.Add(Convert.ToByte((CurrUseTimes) & 0xFF));
            data.Add(Convert.ToByte((CurrUseTimes >> 8) & 0xFF));
            data.Add(Convert.ToByte((CurrUseTimes >> 16) & 0xFF));
            data.Add(Convert.ToByte((CurrUseTimes >> 24) & 0xFF));
            data.Add(Convert.ToByte(RemoteHost.Split('.')[0]));
            data.Add(Convert.ToByte(RemoteHost.Split('.')[1]));
            data.Add(Convert.ToByte(RemoteHost.Split('.')[2]));
            data.Add(Convert.ToByte(RemoteHost.Split('.')[3]));
            data.Add(Convert.ToByte((RemotePort) & 0xFF));
            data.Add(Convert.ToByte((RemotePort >> 8) & 0xFF));
            byte[] DomainNameBuff = Encoding.Default.GetBytes(DomainName);
            data.AddRange(DomainNameBuff);
            if (DomainNameBuff.Length < 32)
            {
                for (int i = 0; i < 32-DomainNameBuff.Length; i++)
                {
                    data.Add(Convert.ToByte(0x00));
                }
            }
            data.Add(Convert.ToByte(DomainServer.Split('.')[0]));
            data.Add(Convert.ToByte(DomainServer.Split('.')[1]));
            data.Add(Convert.ToByte(DomainServer.Split('.')[2]));
            data.Add(Convert.ToByte(DomainServer.Split('.')[3]));
            data.AddRange(Encoding.Default.GetBytes(HardWareID));
            data.AddRange(Heartbeat);
            data.Add(Convert.ToByte((HeartbeatCycle) & 0xFF));
            data.Add(Convert.ToByte((HeartbeatCycle >> 8) & 0xFF));
            data.AddRange(CRCTools.GetInstance().GetCRC(data.ToArray()));
            return data.ToArray();
        }
    }
}
