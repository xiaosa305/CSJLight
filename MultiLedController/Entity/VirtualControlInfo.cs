using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.Entity
{
    public class VirtualControlInfo
    {
        public string IP { get; set; }
        public int SpaceNum { get; set; }

        public VirtualControlInfo(string ip, ControlDevice device)
        {
            this.IP = ip;
            this.SpaceNum = device.Led_space;
        }
    }
}
