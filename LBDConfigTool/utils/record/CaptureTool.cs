using System;
using SharpPcap;
using PacketDotNet;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBDConfigTool.utils.record
{
    public class CaptureTool
    {
        private ICaptureDevice CurrentDevice { get; set; }

        private CaptureTool()
        {
            this.Start();
        }

        public void Start()
        {
            this.Stop();
            foreach (SharpPcap.LibPcap.PcapDevice device in CaptureDeviceList.Instance)
            {
                SharpPcap.LibPcap.PcapInterface @interface = device.Interface;
                if (@interface.FriendlyName.Equals("以太网"))
                {
                    this.CurrentDevice = device;
                    break;
                }
            }
        }
        public CaptureTool Stop()
        {
            if (this.CurrentDevice != null)
            {
                try
                {
                    this.CurrentDevice.StopCapture();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
            return this;
        }
        private void StartCapture()
        {
            if (this.CurrentDevice != null)
            {
                //this.CurrentDevice.OnPacketArrival += new PacketArrivalEventHandler();
                this.CurrentDevice.Open(DeviceMode.Normal,1000);
            }
        }

    }
}
