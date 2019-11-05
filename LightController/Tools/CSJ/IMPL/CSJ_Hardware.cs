using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.Tools.CSJ.IMPL
{
    public class CSJ_Hardware : ICSJFile
    {
        public readonly byte Flag = 0xEA;
        public int Ver { get; set; }
        public int SumUseTimes { get; set; }
        public int DiskFlag { get; set; }
        public int PlayFlag { get; set; }
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
        public CSJ_Hardware(string filePath)
        {
            ReadFromFile(filePath);
        }
        public CSJ_Hardware(byte[] data)
        {
            //添加数据判断，数据长度不符合返回默认空值
            Ver = (int)(data[1] & 0xFF);
            PlayFlag = (int)(data[2] & 0xFF);
            SumUseTimes = (int)((data[3] & 0xFF) | ((data[4] & 0xFF) << 8) | ((data[5] & 0xFF) << 16) | ((data[6] & 0xFF) << 24));
            DiskFlag = (int)(data[7] & 0xFF);
            List<byte> deviceNameBuff = new List<byte>();
            for (int i = 8; i < 24; i++)
            {
                if (data[i] != 0x00)
                {
                    deviceNameBuff.Add(data[i]);
                }
            }
            DeviceName = Encoding.Default.GetString(deviceNameBuff.ToArray());
            Addr = (int)(data[24] & 0xFF);
            LinkMode = (int)(data[25] & 0xFF);
            LinkPort = (int)((data[26] & 0xFF) | ((data[27] & 0xFF) << 8));
            IP = (int)(data[28] & 0xFF) + "." + (int)(data[29] & 0xFF) + "." + (int)(data[30] & 0xFF) + "." + (int)(data[31] & 0xFF);
            NetMask = (int)(data[32] & 0xFF) + "." + (int)(data[33] & 0xFF) + "." + (int)(data[34] & 0xFF) + "." + (int)(data[35] & 0xFF);
            GateWay = (int)(data[36] & 0xFF) + "." + (int)(data[37] & 0xFF) + "." + (int)(data[38] & 0xFF) + "." + (int)(data[39] & 0xFF);
            Mac = data[40].ToString("X2") + "-" + data[41].ToString("X2") + "-" + data[42].ToString("X2") + "-" + data[43].ToString("X2") + "-" + data[44].ToString("X2") + "-" + data[45].ToString("X2");
            Baud = (int)(data[46] & 0xFF);
            CurrUseTimes = (int)((data[47] & 0xFF) | ((data[48] & 0xFF) << 8) | ((data[49] & 0xFF) << 16) | ((data[50] & 0xFF) << 24));
            RemoteHost = (int)(data[51] & 0xFF) + "." + (int)(data[52] & 0xFF) + "." + (int)(data[53] & 0xFF) + "." + (int)(data[54] & 0xFF);
            RemotePort = (int)((data[55] & 0xFF) | ((data[56] & 0xFF) << 8));
            List<byte> domainNameBuff = new List<byte>();
            for (int i = 57; i < 89; i++)
            {
                if (data[i] != 0x00)
                {
                    domainNameBuff.Add(data[i]);
                }
            }
            DomainName = Encoding.Default.GetString(domainNameBuff.ToArray());
            DomainServer = (int)(data[89] & 0xFF) + "." + (int)(data[90] & 0xFF) + "." + (int)(data[91] & 0xFF) + "." + (int)(data[92] & 0xFF);
            List<byte> HardwareIdBuff = new List<byte>();
            HardWareID = "";
            for (int i = 93; i < 109; i++)
            {
                HardwareIdBuff.Add(data[i]);
                HardWareID += data[i].ToString("X2");
            }
            List<byte> HeartbeatBuff = new List<byte>();
            for (int i = 109; i < 117; i++)
            {
                if (data[i] == 0x00)
                {
                    continue;
                }
                HeartbeatBuff.Add(data[i]);
            }
            Heartbeat = HeartbeatBuff.ToArray();
            byte[] HeartbeatCycleBuff = new byte[2];
            HeartbeatCycleBuff[0] = data[117];
            HeartbeatCycleBuff[1] = data[118];
            HeartbeatCycle = (HeartbeatCycleBuff[0] & 0xFF) | ((HeartbeatCycleBuff[1] << 8) & 0xFF);
        }
        public CSJ_Hardware()
        {
            Ver = 1;
            SumUseTimes = 5000;
            DiskFlag = 0;
            PlayFlag = 1;
            DeviceName = "测试用机";
            Addr = 0;
            LinkMode = 0;
            LinkPort = 7060;
            IP = "192.168.1.33";
            NetMask = "255.255.255.0";
            GateWay = "192.168.1.1";
            RemoteHost = "192.168.31.1";
            RemotePort = 7070;
            Mac = "00-00-00-00-00-00";
            DomainName = "测试服务器";
            DomainServer = "192.168.31.1";
            HardWareID = "";
            Heartbeat = new byte[] { 0x00 };
        }
        public static CSJ_Hardware Empty()
        {
            return new CSJ_Hardware();
        }
        public byte[] GetData()
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
                for (int i = 0; i < 16 - DeviceNameBuff.Length; i++)
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
            //string[] macBuff = Mac.Split('-');
            string[] macBuff = new string[] { "00", "00", "00", "00", "00", "00" };
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
                for (int i = 0; i < 32 - DomainNameBuff.Length; i++)
                {
                    data.Add(Convert.ToByte(0x00));
                }
            }
            data.Add(Convert.ToByte(DomainServer.Split('.')[0]));
            data.Add(Convert.ToByte(DomainServer.Split('.')[1]));
            data.Add(Convert.ToByte(DomainServer.Split('.')[2]));
            data.Add(Convert.ToByte(DomainServer.Split('.')[3]));
            int len = HardWareID.Length;
            for (int i = 0; i < 16 - len; i++)
            {
                HardWareID = 0 + HardWareID;
            }
            HardWareID = "0000000000000000";
            byte[] HardWareIDBuff = Encoding.Default.GetBytes(HardWareID);
            data.AddRange(HardWareIDBuff);
            for (int i = 0; i < 16 - HardWareIDBuff.Length; i++)
            {
                data.Add(Convert.ToByte(0x00));
            }
            data.AddRange(Heartbeat);
            for (int i = 0; i < 8 - Heartbeat.Length; i++)
            {
                data.Add(Convert.ToByte(0x00));
            }
            data.Add(Convert.ToByte((HeartbeatCycle) & 0xFF));
            data.Add(Convert.ToByte((HeartbeatCycle >> 8) & 0xFF));
            data.AddRange(CRCTools.GetInstance().GetCRC(data.ToArray()));
            return data.ToArray();
        }
        public void WriteToFile(string filepath)
        {
            byte[] data = GetData();
            string path = filepath + @"\Hardware.bin";
            FileStream fileStream = new FileStream(path, FileMode.Create);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
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

        public static void Test()
        {
            CSJ_Hardware hardware = new CSJ_Hardware();
            byte[] datas = hardware.GetData();
            string path = Application.StartupPath + @"\DataCache\Hardware.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            StreamWriter writer = new StreamWriter(path, true);
            string hexOutput = string.Empty;
            for (int i = 0; i < datas.Length; i++)
            {
                if (i != datas.Length - 1)
                {
                    hexOutput += "0x" + string.Format("{0:X2}", datas[i]) + ",";
                }
                else
                {
                    hexOutput += "0x" + string.Format("{0:X2}", datas[i]);
                }
            }
            writer.Write(hexOutput);
            writer.Close();
        }
    }
}
