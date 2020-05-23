using MultiLedController.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.multidevice.impl
{
    public class RecodeDmxData
    {
        public Dictionary<int, List<byte>> VirtualControlDeviceDmxDatas { get; set; }
        public int FrameIntervalTime { get; set; }
        public ControlDevice ControlDevice { get; set; }
        public RecodeDmxData(Dictionary<int,List<byte>> dmxData,int frameIntervalTime,ControlDevice controlDevice)
        {
            this.VirtualControlDeviceDmxDatas = new Dictionary<int, List<byte>>();
            foreach (int key in dmxData.Keys)
            {
                this.VirtualControlDeviceDmxDatas.Add(key, dmxData[key]);
            }
            this.FrameIntervalTime = frameIntervalTime;
            this.ControlDevice = controlDevice;
        }
    }
}
