using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    class DMXHardware
    {
        private readonly byte Ver = 0xEA;
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
        public int HardWareID { get; set; }
        public byte Heartbeat { get; set; }
        public int HeartbeatCycle { get; set; }
    }
}
