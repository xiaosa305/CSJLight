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
        private bool IsFirstFrame { get; set; }
        public delegate void FrameSync();
        public delegate void DMXDataCaptureed(int port,byte[] data);
        private FrameSync FrameSync_Event { get; set; }
        private DMXDataCaptureed DMXDataCaptureed_Event { get; set; }

        private void Init()
        {
            this.IsFirstFrame = true;
        }

        private CaptureTool(FrameSync frameSync,DMXDataCaptureed dataCaptureed)
        {
            this.FrameSync_Event = frameSync;
            this.DMXDataCaptureed_Event = dataCaptureed;
            this.Init();
            this.Start();
        }
        public void Reset()
        {
            this.IsFirstFrame = true;
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
                    return;
                }
            }
            this.StartCapture();
        }
        public CaptureTool Stop()
        {
            if (this.CurrentDevice != null)
            {
                try
                {
                    this.CurrentDevice.StopCapture();
                    this.CurrentDevice = null;
                    this.Reset();
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
                this.CurrentDevice.OnPacketArrival += new PacketArrivalEventHandler(CaptureData);
                this.CurrentDevice.Open(DeviceMode.Normal,1000);
                this.CurrentDevice.Capture();
            }
        }
        private void CaptureData(object sender, CaptureEventArgs e)
        {
            Packet packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            UdpPacket udpPacket = (UdpPacket)packet.Extract(typeof(UdpPacket));
            if (udpPacket != null && udpPacket.DestinationPort == 6454)
            {
                byte[] data = packet.Bytes;
                if (!this.IsFirstFrame && data[0] == 0x41 && data[1] == 0x72 && data[2] == 0x74 && data[3] == 0x2D && data[4] == 0x4E && data[5] == 0x65 && data[6] == 0x74 && data[7] == 0x00 && data.Length > 18 && data[8] == 0x00 && data[9] == 0x50)
                {
                    int port = (int)((data[14] & 0xFF) | ((data[15] & 0xFF) << 8));
                    int dataLength = (int)(data[17] & 0xFF) | ((data[16] & 0xFF) << 8);
                    if (dataLength != 0 && data.Length == (dataLength + 18))
                    {
                        byte[] DMXDataBuff = new byte[dataLength];
                        Array.Copy(data, 18, DMXDataBuff, 0, dataLength);
                        this.DMXDataCaptureed_Event(port, DMXDataBuff);
                    }
                }
                else if (data[0] == 0x4D && data[1] == 0x61 && data[2] == 0x64 && data[3] == 0x72 && data[4] == 0x69 && data[5] == 0x78 && data[6] == 0x4E && data[7] == 0x00 && data[8] == 0x02 && data[9] == 0x52 && data[10] == 0x00 && data[11] == 0x0E && data[12] == 0xC5)
                {
                    this.IsFirstFrame = false;
                    this.FrameSync_Event();
                }
            }
        }
    }
}
