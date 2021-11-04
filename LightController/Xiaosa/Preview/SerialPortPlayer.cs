using LightController.MyForm;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using static LightController.Utils.OldFileToNewFileUtils;

namespace LightController.Xiaosa.Preview
{
    public class SerialPortPlayer
    {
        private static SerialPortPlayer Instance;
        private static readonly Object SingleKey = new object();
        private MainFormInterface MainFormInterface;
        private ChannelGroup Group;
        private int FrameIntervalTime;
        private System.Timers.Timer PlayTimer;
        private System.Timers.Timer SingleStepPlayTimer;
        private byte[] SingleStepDmxData;
        private SerialPort COM;
        private SerialPortPlayer()
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
        public static SerialPortPlayer GetPlayer()
        {
            if (Instance == null)
            {
                lock (SingleKey)
                {
                    if (Instance == null)
                    {
                        Instance = new SerialPortPlayer();
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
                Console.WriteLine("打开串口失败：" + ex.Message + "::::::" + ex.StackTrace);
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
            if (PlayTimer.Enabled)
            {
                PlayTimer.Stop();
            }
            if (SingleStepPlayTimer.Enabled)
            {
                SingleStepPlayTimer.Stop();
            }
        }
        public void SingleStepPreview(byte[] data,MainFormInterface mainFormInterface)
        {
            if (PlayTimer.Enabled)
            {
                PlayTimer.Stop();
                Thread.Sleep(100);
            }
            MainFormInterface = mainFormInterface;
            SetFrameIntervalTime();
            lock (SingleStepDmxData)
            {
                List<byte> buff = new List<byte>();
                buff.Add(Convert.ToByte(0x00));
                buff.AddRange(data);
                SingleStepDmxData = buff.ToArray() ;
            }
            if (!SingleStepPlayTimer.Enabled)
            {
                SingleStepPlayTimer.Start();
            }
        }
        public bool TriggerAudio()
        {
            return Group.MusicControl();
        }
        public bool MusicControlState()
        {
            return Group.MusicControlState && Group.IsMusicMode;
        }
        private void PlayTask(byte[] dmxData)
        {
            if (COM.IsOpen)
            {
                try
                {
                    COM.BreakState = true;
                    Thread.Sleep(10);
                    COM.BreakState = false;
                    COM.Write(dmxData, 0, dmxData.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}
