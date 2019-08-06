using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class DMXHardware
    {
        private readonly byte Flag = 0xEA;
        private int Ver { get; set; }
        private int SumUseTimes { get; set; }
        private int DiskFlag { get; set; }
        private int PlayFlag { get; set; }
        private string DeviceName { get; set; }
        private int Addr { get; set; }
        private int LinkMode { get; set; }
        private int LinkPort { get; set; }
        private string IP { get; set; }
        private string NetMask { get; set; }
        private string GateWay { get; set; }
        private string Mac { get; set; }
        private int Baud { get; set; }
        private int CurrUseTimes { get; set; }
        private string RemoteHost { get; set; }
        private int RemotePort { get; set; }
        private string DomainName { get; set; }
        private string DomainServer { get; set; }
        private string HardWareID { get; set; }
        private byte[] Heartbeat { get; set; }
        private int HeartbeatCycle { get; set; }

        public DMXHardware(string filePath)
        {
            ReadFromFile(filePath);
        }

        public DMXHardware(byte[] data)
        {

        }

        private void ReadFromFile(string filePath)
        {
            string lineStr = "";
            string strValue;
            int intValue;
            StreamReader reader;
            if (filePath != null)
            {
                try
                {
                    using (reader = new StreamReader(filePath))
                    {
                        lineStr = reader.ReadLine();
                        lineStr = reader.ReadLine();
                        if (lineStr.Equals("[Common]"))
                        {
                            strValue = (reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            this.SumUseTimes = intValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            this.CurrUseTimes = intValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            this.DiskFlag = intValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            this.PlayFlag = intValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            this.DeviceName = strValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            this.Addr = intValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            this.HardWareID = strValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            this.Heartbeat = Encoding.Default.GetBytes(strValue);

                            strValue = (reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            this.HeartbeatCycle = intValue;
                        }
                        lineStr = reader.ReadLine();
                        if (lineStr.Equals("[Network]"))
                        {
                            strValue = (reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            this.LinkMode = intValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            this.LinkPort = intValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            this.IP = strValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            this.NetMask = strValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            this.GateWay = strValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            this.Mac = strValue;
                        }
                        lineStr = reader.ReadLine();
                        if (lineStr.Equals("[Other]"))
                        {
                            strValue = (reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            this.Baud = intValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            this.RemoteHost = strValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            this.RemotePort = intValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            this.DomainName = strValue;

                            strValue = (reader.ReadLine().Split('='))[1];
                            this.DomainServer = strValue;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }

        }

        public byte[] GetHardware()
        {
            List<byte> data = new List<byte>();
            data.Add(Flag);
            data.Add(Convert.ToByte(Ver));
            data.Add(Convert.ToByte(PlayFlag));
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
            string[] macBuff = Mac.Split('-');
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
            int len = HardWareID.Length;
            for (int i = 0; i < 16-len; i++)
            {
                HardWareID = 0 + HardWareID;
            }
           
            byte[] HardWareIDBuff = Encoding.Default.GetBytes(HardWareID);
            data.AddRange(HardWareIDBuff);
            for (int i = 0; i < 16 - HardWareIDBuff.Length; i++)
            {
                data.Add(Convert.ToByte(0x00));
            }
            data.AddRange(Heartbeat);
            for (int i = 0; i < 8-Heartbeat.Length; i++)
            {
                data.Add(Convert.ToByte(0x00));
            }
            data.Add(Convert.ToByte((HeartbeatCycle) & 0xFF));
            data.Add(Convert.ToByte((HeartbeatCycle >> 8) & 0xFF));
            data.AddRange(CRCTools.GetInstance().GetCRC(data.ToArray()));
            return data.ToArray();
        }
    }
}
