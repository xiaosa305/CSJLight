using LightController.Tools;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using FTD2XX_NET;

namespace LightController.Tools
{
    public class SerialPortTools
    {
        private SerialPort ComDevice { get; set; }
        public string[] PortNames { get; set; }
        public int BaudRate { get; set; }
        public StopBits StopBits { get; set; }
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        public string PortName { get; set; }
        public string Result { get; set; }

        private bool Status_Flag = true;

        public SerialPortTools()
        {
            this.ComDevice = new SerialPort();
            PortNames = SerialPort.GetPortNames();
            Init();
            //ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);
        }

        private void Init()
        {
            //PortName = PortNames[5];
            PortName = "COM7";
            BaudRate = 250000;
            DataBits = 8;
            StopBits = StopBits.Two;
            Parity = Parity.None;
            //OpenSerialport();
        }
        
        public void Test(DMX_C_File file)
        {
            DMX_C_Data dMX_C_Data = file.Data;
            IList<C_Data> c_Datas = dMX_C_Data.Datas;
            int chanelCount = dMX_C_Data.HeadData.ChanelCount;

            byte[] dmx512data = new byte[512];

            byte[][] chanelData = new byte[chanelCount][];
            int[] chanelNos = new int[chanelCount];
            int[] dataPoint = new int[chanelCount];

            for (int num = 0; num < chanelCount; num++)
            {
                dataPoint[num] = 0;
            }

            for (int i = 0; i < 512; i++)
            {
                dmx512data[i] = 0;
            }


            for (int i = 0; i < chanelCount; i++)
            {
                C_Data c_Data = c_Datas[i];
                chanelNos[i] = c_Data.ChanelNo;
                IList<byte> data = new List<byte>();
                for (int j = 0; j < c_Data.DataSize; j++)
                {
                    byte b = Convert.ToByte(c_Data.Datas[j]);
                    data.Add(b);
                }
                chanelData[i] = data.ToArray();
            }

            /*
             * Test
            */
            UInt32 ftdiDeviceCount = 0;
            FTDI.FT_STATUS fT_STATUS = FTDI.FT_STATUS.FT_OK;
            FTDI myFtdiDevice = new FTDI();
            fT_STATUS = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);
            if (fT_STATUS == FTDI.FT_STATUS.FT_OK)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("error");
            }
            if (ftdiDeviceCount > 0)
            {
                FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];
                fT_STATUS = myFtdiDevice.GetDeviceList(ftdiDeviceList);
                if (fT_STATUS == FTDI.FT_STATUS.FT_OK)
                {
                    fT_STATUS = myFtdiDevice.OpenBySerialNumber(ftdiDeviceList[0].SerialNumber);
                    if (fT_STATUS == FTDI.FT_STATUS.FT_OK)
                    {
                        string potName;
                        myFtdiDevice.GetCOMPort(out potName);
                        if (potName == null || potName == "")
                        {
                            myFtdiDevice.Close();
                        }
                        byte[] Start = { 0x00 };
                        byte[] MAB = new byte[256];
                        myFtdiDevice.SetBaudRate(250000);
                        myFtdiDevice.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_2, FTDI.FT_PARITY.FT_PARITY_NONE);

                        UInt32 count = 0;
                        //myFtdiDevice.Write(MAB, 0, ref count);
                        //myFtdiDevice.Write(Start, Start.Length, ref count);
                        int a = 0;
                        dmx512data[1] = Convert.ToByte(100);
                        dmx512data[2] = Convert.ToByte(200);
                        dmx512data[3] = Convert.ToByte(100);
                        dmx512data[4] = Convert.ToByte(100);
                        while (Status_Flag)
                        {
                            dmx512data[0] = Convert.ToByte(a++);
                            if (a > 255)
                            {
                                a = 0;
                            }

                            //模拟break效果
                            myFtdiDevice.SetBaudRate(96000);
                            myFtdiDevice.Write(Start, Start.Length, ref count);
                            myFtdiDevice.SetBaudRate(250000);

                            //myFtdiDevice.SetBreak(true);
                            //Thread.Sleep(10);
                            //myFtdiDevice.SetBreak(false);


                            //myFtdiDevice.Write(MAB, 0, ref count);
                            myFtdiDevice.Write(Start, Start.Length, ref count);

                            for (int add = 0; add < 512; add++)
                            {
                                for (int chanel = 0; chanel < chanelNos.Length; chanel++)
                                {
                                    int chanelNo = chanelNos[chanel];
                                    if (add == chanelNo + 1)
                                    {
                                        int point = dataPoint[chanel];
                                        dmx512data[add] = chanelData[chanel][point];
                                        dataPoint[chanel]++;
                                        if (dataPoint[chanel] == chanelData[chanel].Length)
                                        {
                                            dataPoint[chanel] = 0;
                                        }
                                    }
                                }
                            }
                            myFtdiDevice.Write(dmx512data, dmx512data.Length, ref count);
                            Thread.Sleep(32);
                            myFtdiDevice.Purge(FTDI.FT_PURGE.FT_PURGE_TX);
                        }
                    }
                }
            }

            /*
            * Test
           */
        }

        public bool Test1()
        {
            string str = "test";
            byte[] data = Encoding.Default.GetBytes(str);
            return SendData(data);
        }

        public void InitPort()
        {
            ComDevice.PortName = PortName;
            ComDevice.BaudRate = BaudRate;
            ComDevice.DataBits = DataBits;
            ComDevice.StopBits = StopBits;
            ComDevice.Parity = Parity;
        }

        public void OpenSerialport()
        {
            if (PortNames.Count() <= 0)
            {
                return;
            }
            if (ComDevice.IsOpen == false)
            {
                InitPort();
                try
                {
                    ComDevice.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return;
                }
            }
            else
            {
                try
                {
                    ComDevice.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return;
                }
            }
        }

        public bool SendData(byte[] data)
        {
            if (ComDevice.IsOpen)
            {
                try
                {
                    ComDevice.Write(data, 0, data.Length);
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {

            }
            return false;
        }

        //接受缓冲区数据
        public void Com_DataReceived(object sender, SerialDataReceivedEventArgs s)
        {
            //byte[] ReDatas = new byte[ComDevice.BytesToRead];
            //while ((ComDevice.Read(ReDatas, 0, ReDatas.Length)) != -1)
            //{
            //    string rxData = Encoding.Default.GetString(ReDatas);
            //    switch (rxData)
            //    {
            //        case"ack":
            //            Status_Flag = false;
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }

    }
}
