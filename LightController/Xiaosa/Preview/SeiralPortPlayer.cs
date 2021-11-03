using LightController.MyForm;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using static LightController.Utils.OldFileToNewFileUtils;

namespace LightController.Xiaosa.Preview
{
    public class SeiralPortPlayer
    {
        private static SeiralPortPlayer Instance;
        private static readonly Object SingleKey = new object();
        private MainFormInterface MainFormInterface;
        private ChannelGroup Group;
        private int FrameIntervalTime;
        private System.Timers.Timer PlayTimer;
        private System.Timers.Timer SingleStepPlayTimer;
        private byte[] SingleStepDmxData;
        private SerialPort COM;
        private SeiralPortPlayer()
        {
            Init();
            InitPlayTimer();
            InitSingleStepPlayTimer();
        }
        private void Init()
        {
            FrameIntervalTime = 40;
            SingleStepDmxData = Enumerable.Repeat<byte>(Convert.ToByte(0x00), 513).ToArray();
            COM = new SerialPort
            {
                DataBits = 8,
                StopBits = StopBits.Two,
                Parity = Parity.None,
                BaudRate = 250000
            };
        }
        public void SetFrameIntervalTime()
        {
            FrameIntervalTime = new CSJ_Config(MainFormInterface.GetLights(), MainFormInterface.GetConfigPath()).TimeFactory;
        }
        private void InitPlayTimer()
        {
            PlayTimer = new System.Timers.Timer
            {
                AutoReset = true,
                Interval = FrameIntervalTime
            };
            PlayTimer.Elapsed += delegate
            {
                PlayTask(Group.ReadDMXData());
            };
        }
        private void InitSingleStepPlayTimer()
        {
            SingleStepPlayTimer = new System.Timers.Timer()
            {
                AutoReset = true,
                Interval = 40
            };
            SingleStepPlayTimer.Elapsed += delegate
            {
                PlayTask(SingleStepDmxData);

            };
        }
        public static SeiralPortPlayer GetPlayer()
        {
            if (Instance == null)
            {
                lock (SingleKey)
                {
                    if (Instance == null)
                    {
                        Instance = new SeiralPortPlayer();
                    }
                }
            }
            return Instance;
        }
        public bool OpenSerialPort(string serialPortName)
        {
            try
            {
                if (COM.IsOpen)
                {
                    COM.Close();
                }
                COM.PortName = serialPortName;
                COM.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("打开串口失败：" + ex.Message);
            }
            return false;
        }
        public void CloseSerialPort()
        {
            if (COM.IsOpen)
            {
                COM.Close();
            }
        }
        public string[] GetSerialPortNames()
        {
            return SerialPort.GetPortNames();
        }
        public void Preview(MainFormInterface mainFormInterface, int sceneNo)
        {
            if (SingleStepPlayTimer.Enabled)
            {
                SingleStepPlayTimer.Stop();
                Thread.Sleep(100);
            }
            MainFormInterface = mainFormInterface;
            SetFrameIntervalTime();
            PlayTimer.Interval = FrameIntervalTime;
            Group = new ChannelGroup(MainFormInterface, sceneNo);
        }
        public void EndPreview()
        {
            PlayTimer.Stop();
        }
        public void SingleStepPreview(byte[] data)
        {
            if (PlayTimer.Enabled)
            {
                PlayTimer.Stop();
                Thread.Sleep(100);
            }
            SetFrameIntervalTime();
            SingleStepDmxData = data;
            SingleStepPlayTimer.Start();
        }
        public bool TriggerAudio()
        {
            return Group.MusicControl();
        }
        private void PlayTask(byte[] dmxData)
        {
            if (COM.IsOpen)
            {
                COM.Write(dmxData, 0, dmxData.Length);
            }
        }
    }
}
