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
        public Dictionary<int,bool> VirtualControlDeviceDmxDataResponseStatus { get; set; }
        public int FrameIntervalTime { get; set; }
        public ControlDevice ControlDevice { get; set; }
        public RecodeDmxData(Dictionary<int,List<byte>> dmxData, Dictionary<int, bool> virtualControlDeviceDmxDataResponseStatus, int frameIntervalTime,ControlDevice controlDevice)
        {
            this.VirtualControlDeviceDmxDatas = new Dictionary<int, List<byte>>();
            this.VirtualControlDeviceDmxDataResponseStatus = new Dictionary<int, bool>();
            List<int> keys = dmxData.Keys.ToList();
            foreach (int key in keys)
            {
                this.VirtualControlDeviceDmxDatas.Add(key, dmxData[key]);
            }
            keys = virtualControlDeviceDmxDataResponseStatus.Keys.ToList();
            foreach (int key in keys)
            {
                this.VirtualControlDeviceDmxDataResponseStatus.Add(key, virtualControlDeviceDmxDataResponseStatus[key]);
            }
            this.FrameIntervalTime = frameIntervalTime;
            this.ControlDevice = controlDevice;
        }
    }
}
