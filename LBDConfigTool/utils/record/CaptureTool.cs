using System;
using PacketDotNet;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LBDConfigTool.utils.conf;
using System.Threading;
using System.Timers;
using SharpPcap;

namespace LBDConfigTool.utils.record
{
    public class CaptureTool
    {
        private ICaptureDevice CurrentDevice { get; set; }
        private bool IsFirstFrame { get; set; }
        public delegate void FrameSync();
        public delegate void DMXDataCaptureed(int port,List<byte> data);
        private FrameSync FrameSync_Event { get; set; }
        private DMXDataCaptureed DMXDataCaptureed_Event { get; set; }
        private int StartSpace { get; set; }
        private bool CaptureStatus { get; set; }
        private System.Timers.Timer CaptureTimer { get; set; }

        private void Init()
        {
            this.IsFirstFrame = true;
        }

        public CaptureTool(CSJConf conf ,FrameSync frameSync,DMXDataCaptureed dataCaptureed)
        {
            this.FrameSync_Event = frameSync;
            this.DMXDataCaptureed_Event = dataCaptureed;
            this.StartSpace = conf.Art_Net_Start_Space;
            this.CaptureStatus = false;
            //this.StartSpace = 1;

            this.Init();
        }
        public void Reset()
        {
            this.IsFirstFrame = true;
        }
        public void Start()
        {
            this.Stop();
            if(this.CurrentDevice == null)
            {
                foreach (SharpPcap.LibPcap.PcapDevice device in CaptureDeviceList.Instance)
                {
                    SharpPcap.LibPcap.PcapInterface @interface = device.Interface;
                    if (@interface.FriendlyName.StartsWith("以太网"))
                    {
                        this.CurrentDevice = device;
                        this.CaptureStatus = true;
                        CaptureTimer = new System.Timers.Timer() { AutoReset = false };
                        CaptureTimer.Elapsed += this.CaptureTask;
                        CaptureTimer.Start();
                        //this.StartCapture();
                        return;
                    }
                }
            }
            else
            {
                this.CaptureStatus = true;
                //this.StartCapture();
            }
        }
        public CaptureTool Stop()
        {
            if (this.CurrentDevice != null)
            {
                try
                {
                    //this.CurrentDevice.StopCapture();
                    //this.CurrentDevice = null;
                    this.CaptureStatus = false;
                    this.Reset();
                    //this.CaptureTimer.Stop();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
            return this;
        }
      
        private void CaptureTask(object sender, ElapsedEventArgs e)
        {
            this.CurrentDevice.OnPacketArrival += new PacketArrivalEventHandler(CaptureData);
            this.CurrentDevice.Open(DeviceMode.Normal, 1000);
            this.CurrentDevice.Capture();
        }

        private void CaptureData(object sender, CaptureEventArgs e)
        {
            if (this.CaptureStatus)
            {
                Packet packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
                UdpPacket udpPacket = (UdpPacket)packet.Extract(typeof(UdpPacket));
                if (udpPacket != null && udpPacket.DestinationPort == 6454)
                {
                    byte[] data = packet.Bytes;
                    if (!this.IsFirstFrame && data[42 + 0] == 0x41 && data[42 + 1] == 0x72 && data[42 + 2] == 0x74 && data[42 + 3] == 0x2D && data[42 + 4] == 0x4E && data[42 + 5] == 0x65 && data[42 + 6] == 0x74 && data[42 + 7] == 0x00 && data.Length > 18 && data[42 + 8] == 0x00 && data[42 + 9] == 0x50)
                    {
                        int port = (int)((data[42 + 14] & 0xFF) | ((data[42 + 15] & 0xFF) << 8));
                        int dataLength = (int)(data[42 + 17] & 0xFF) | ((data[42 + 16] & 0xFF) << 8);
                        if (dataLength != 0 && data.Length == (dataLength + 18 + 42))
                        {
                            Console.WriteLine("Receive DMXData");
                            byte[] DMXDataBuff = new byte[dataLength];
                            Array.Copy(data, 42 + 18, DMXDataBuff, 0, dataLength);
                            this.DMXDataCaptureed_Event(port - this.StartSpace + 1, new List<byte>(DMXDataBuff));
                        }
                    }
                    else if (data[42 + 0] == 0x4D && data[42 + 1] == 0x61 && data[42 + 2] == 0x64 && data[42 + 3] == 0x72 && data[42 + 4] == 0x69 && data[42 + 5] == 0x78 && data[42 + 6] == 0x4E && data[42 + 7] == 0x00 && data[42 + 8] == 0x02 && data[42 + 9] == 0x52 && data[42 + 10] == 0x00)
                    {
                        Console.WriteLine("Receive FrameSync");
                        if (this.IsFirstFrame)
                        {
                            this.IsFirstFrame = false;
                        }
                        else
                        {
                            this.IsFirstFrame = false;
                            this.FrameSync_Event();
                        }
                    }
                }
            }
        }
    }
}
