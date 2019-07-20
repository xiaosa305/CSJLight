using FTD2XX_NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightEditor.Tools
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
            ConnectDevice();
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
                catch (Exception)
                {
                }
                finally
                {
                    PreViewThread = null;
                }
            }
            if(Device != null)
            {
                try
                {
                    Device.Close();
                }
                catch (Exception)
                {
                }
                finally
                {
                    Device = null;
                }
            }
        }

        public void Preview(byte[] data)
        {
           
            try
            {
                TimeFactory = 32;
                if (Device == null)
                {
                    ConnectDevice();
                }
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
                //发送Break
                Device.SetBreak(true);
                Thread.Sleep(10);
                Device.SetBreak(false);
                Thread.Sleep(1);
                List<byte> buff = new List<byte>();
                buff.AddRange(StartCode);
                buff.AddRange(PlayData);
                Device.Write(buff.ToArray(), buff.ToArray().Length, ref count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("未连接DMX512设备——" + ex.Message);
                EndView();
            }
        }

        public void ConnectDevice()
        {
            UInt32 deviceCount = 0;
            FTDI.FT_STATUS status = FTDI.FT_STATUS.FT_OK;
            Device = new FTDI();
            status = Device.GetNumberOfDevices(ref deviceCount);
            if (status == FTDI.FT_STATUS.FT_OK)
            {
                if (deviceCount > 0)
                {
                    FTDI.FT_DEVICE_INFO_NODE[] deviceList = new FTDI.FT_DEVICE_INFO_NODE[deviceCount];
                    status = Device.GetDeviceList(deviceList);
                    if (status == FTDI.FT_STATUS.FT_OK)
                    {
                        status = Device.OpenBySerialNumber(deviceList[0].SerialNumber);
                        if (status == FTDI.FT_STATUS.FT_OK)
                        {
                            string portName;
                            Device.GetCOMPort(out portName);
                            if (portName == null || portName == "")
                            {
                                Device.Close();
                                Device = null;
                            }
                            Device.SetBaudRate(250000);
                            Device.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_2, FTDI.FT_PARITY.FT_PARITY_NONE);
                        }
                    }
                }
                else
                {
                    Device = null;
                    Console.WriteLine("未连接DMX512设备");
                    EndView();
                }
            }
        }
    }
}
