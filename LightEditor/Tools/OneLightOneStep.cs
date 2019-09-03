using FTD2XX_NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LighEditor.Tools
{
    class OneLightOneStep
    {
        private static OneLightOneStep Instance { get; set; }
        private Thread PreViewThread { get; set; }
        private byte[] PlayData { get; set; }
        private FTDI Device { get; set; }
        private int TimeFactory { get; set; }

        private readonly byte[] StartCode = new byte[] { 0x00 };
        private OneLightOneStep()
        {
            TimeFactory = 31;
            Device = new FTDI();
        }
        public static OneLightOneStep GetInstance()
        {
            if (Instance == null)
            {
                Instance = new OneLightOneStep();
            }
            return Instance;
        }
        public void EndView()
        {
            if (PreViewThread != null)
            {
                try
                {
                    PreViewThread.Abort();
                }
                finally
                {
                    PreViewThread = null;
                }
            }
        }
        public void Preview(byte[] data)
        {
           
            try
            {
                if (data == null)
                {
                    data = Enumerable.Repeat(Convert.ToByte(0x00), 512).ToArray();
                }
                PlayData = data;
                if (PreViewThread == null)
                {
                    PreViewThread = new Thread(new ThreadStart(PreviewThreadStart))
                    {
                        IsBackground =true
                    };
                    PreViewThread.Start();
                }
            }
            catch (Exception)
            {
                EndView();
            }
        }
        private void PreviewThreadStart()
        {
            while (true)
            {
                Play();
                Thread.Sleep(TimeFactory);
            }
        }
        private void Play()
        {
            UInt32 count = 0;
            try
            {
                if (Device != null)
                {
                    if (Device.IsOpen)
                    {
                        //发送Break|
                        Device.SetBreak(true);
                        Thread.Sleep(0);
                        Device.SetBreak(false);
                        Thread.Sleep(0);
                        List<byte> buff = new List<byte>();
                        buff.AddRange(StartCode);
                        buff.AddRange(PlayData);
                        Device.Purge(FTDI.FT_PURGE.FT_PURGE_TX);
                        Device.Write(buff.ToArray(), buff.ToArray().Length, ref count);
                        Device.SetBreak(false);
                    }
                }
            }
            catch (Exception)
            {
                EndView();
            }
        }
        public string[] GetDMX512DeviceList()
        {
            List<string> deviceNameList = new List<string>();
            UInt32 deviceCount = 0;
            FTDI.FT_STATUS status = FTDI.FT_STATUS.FT_OK;
            FTDI dmx512Device = new FTDI();
            status = dmx512Device.GetNumberOfDevices(ref deviceCount);
            if (status == FTDI.FT_STATUS.FT_OK)
            {
                if (deviceCount > 0)
                {
                    FTDI.FT_DEVICE_INFO_NODE[] deviceList = new FTDI.FT_DEVICE_INFO_NODE[deviceCount];
                    status = dmx512Device.GetDeviceList(deviceList);
                    if (status == FTDI.FT_STATUS.FT_OK)
                    {
                        for (int i = 0; i < deviceList.Length; i++)
                        {
                            status = dmx512Device.OpenBySerialNumber(deviceList[i].SerialNumber);
                            if (status == FTDI.FT_STATUS.FT_OK)
                            {
                                string portName;
                                dmx512Device.GetCOMPort(out portName);
                                if (portName != null && portName != "")
                                {
                                    deviceNameList.Add(portName);
                                    dmx512Device.Close();
                                }
                            }
                        }
                    }
                }
            }
            return deviceNameList.ToArray();
        }
        public bool ConnectDevice(string comName)
        {
            UInt32 deviceCount = 0;
            FTDI.FT_STATUS status = FTDI.FT_STATUS.FT_OK;
            status = Device.GetNumberOfDevices(ref deviceCount);
            if (status == FTDI.FT_STATUS.FT_OK)
            {
                if (deviceCount > 0)
                {
                    FTDI.FT_DEVICE_INFO_NODE[] deviceList = new FTDI.FT_DEVICE_INFO_NODE[deviceCount];
                    status = Device.GetDeviceList(deviceList);
                    if (status == FTDI.FT_STATUS.FT_OK)
                    {
                        for (int i = 0; i < deviceCount; i++)
                        {
                            status = Device.OpenBySerialNumber(deviceList[i].SerialNumber);
                            if (status == FTDI.FT_STATUS.FT_OK)
                            {
                                string portName;
                                Device.GetCOMPort(out portName);
                                if (portName == null || portName == "" || portName != comName)
                                {
                                    Device.Close();
                                }
                                else
                                {
                                    Device.SetBaudRate(250000);
                                    Device.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_2, FTDI.FT_PARITY.FT_PARITY_NONE);
                                    return Device.IsOpen;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public void CloseDevice()
        {
            EndView();
            Thread.Sleep(200);
            if (Device != null)
            {
                Device.Close();
            }
        }
    }
}
