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
            Ver = (int)(data[9] & 0xFF);
            PlayFlag = (int)(data[10] & 0xFF);
            SumUseTimes = (int)((data[11] & 0xFF) | ((data[12] & 0xFF) << 8) | ((data[13] & 0xFF) << 16) | ((data[14] & 0xFF) << 24));
            DiskFlag = (int)(data[15] & 0xFF);
            List<byte> deviceNameBuff = new List<byte>();
            for (int i = 16; i < 32; i++)
            {
                if (data[i] != 0x00)
                {
                    deviceNameBuff.Add(data[i]);
                }
            }
            DeviceName = Encoding.Default.GetString(deviceNameBuff.ToArray());
            Addr = (int)(data[32] & 0xFF);
            LinkMode = (int)(data[33] & 0xFF);
            LinkPort = (int)((data[34] & 0xFF) | ((data[35] & 0xFF) << 8));
            IP = (int)(data[36] & 0xFF) + "." + (int)(data[37] & 0xFF) + "." + (int)(data[38] & 0xFF) + "." + (int)(data[39] & 0xFF);
            NetMask = (int)(data[40] & 0xFF) + "." + (int)(data[41] & 0xFF) + "." + (int)(data[42] & 0xFF) + "." + (int)(data[43] & 0xFF);
            GateWay = (int)(data[44] & 0xFF) + "." + (int)(data[45] & 0xFF) + "." + (int)(data[46] & 0xFF) + "." + (int)(data[47] & 0xFF);
            Mac = data[48].ToString() + "-" + data[49].ToString() + "-" + data[50].ToString() + "-" + data[51].ToString() + "-" + data[52].ToString() + "-" + data[53].ToString();
            Baud = (int)(data[54] & 0xFF);
            CurrUseTimes = (int)((data[55] & 0xFF) | ((data[56] & 0xFF) << 8) | ((data[57] & 0xFF) << 16) | ((data[58] & 0xFF) << 24));
            RemoteHost = (int)(data[59] & 0xFF) + "." + (int)(data[60] & 0xFF) + "." + (int)(data[61] & 0xFF) + "." + (int)(data[62] & 0xFF);
            RemotePort = (int)((data[63] & 0xFF) | ((data[64] & 0xFF) << 8));
            List<byte> domainNameBuff = new List<byte>();
            for (int i = 65; i < 97; i++)
            {
                if (data[i] != 0x00)
                {
                    domainNameBuff.Add(data[i]);
                }
            }
            DomainName = Encoding.Default.GetString(domainNameBuff.ToArray());
            DomainServer = (int)(data[97] & 0xFF) + "." + (int)(data[98] & 0xFF) + "." + (int)(data[99] & 0xFF) + "." + (int)(data[100] & 0xFF);
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
