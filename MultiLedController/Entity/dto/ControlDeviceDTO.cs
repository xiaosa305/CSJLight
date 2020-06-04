using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.entity.dto
{
    public class ControlDeviceDTO
    {
        public ControlDevice ControlDevice { get; set; }
        public List<String> VirtualIps { get; set; }
        public ControlDeviceDTO(ControlDevice controlDevice,List<string> virtualIps)
        {
            this.ControlDevice = controlDevice;
            this.VirtualIps = virtualIps;
        }
    }
}
