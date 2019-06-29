using FTD2XX_NET;
using LightController.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
    public class DMX512Play
    {
        private static DMX512Play Instance { get; set; }
        private byte[] PlayData { get; set; }
        private bool IsNewData { get; set; }
        private bool IsStartPlay { get; set; }
        private bool IsPause { get; set; }
        private bool IsPlay { get; set; }
        private byte[][] ChanelData { get; set; }
        private int[] ChanelNos { get; set; }
        private int[] DataPoint { get; set; }
        private int ChanelCount { get; set; }
        private readonly byte[] StartCode = { 0x00 };
        private FTDI myFtdiDevice { get; set; }
        private Thread PlayThread { get; set; }

        private DMX512Play()
        {
            IsNewData = true;
            IsStartPlay = false;
            IsPlay = false;
        }

        private void Init()
        {
            DataPoint = new int[ChanelCount];
            PlayData = new byte[512];
            for (int i = 0; i < Constant.DMX512; i++)
            {
                PlayData[i] = Convert.ToByte(0);
            }
            for (int num = 0; num < ChanelCount; num++)
            {
                DataPoint[num] = 0;
            }
        }

        public void EndPreview()
        {
            IsPlay = false;
            if (myFtdiDevice != null)
            {
                if (myFtdiDevice.IsOpen)
                myFtdiDevice.Close();
                myFtdiDevice = null;
            }
            ChanelData = null;
            ChanelNos = null;
            DataPoint = null;
            PlayData = null;
            IsNewData = true;
            IsStartPlay = false;
        }

        public bool Preview(DBWrapper wrapper, int senceNo)
        {
            IsPause = true;
            if (!IsPlay)
            {
                IsPlay = true;
            }
            if (IsStartPlay)
            {
                if (SetData(wrapper, senceNo))
                {
                    IsNewData = true;
                    IsPause = false;
                    return true;
                }
                else
                {
                    Console.WriteLine("没有该场景数据");
                    return false;
                }
            }
            else
            {
                if (SetData(wrapper, senceNo))
                {
                    Init();
                    if (!Connect())
                    {
                        Console.WriteLine("未连接设备");						
                        return false;
                    }
                    else
                    {
                        IsPause = false;
                        PlayThread = new Thread(new ThreadStart(Play));
                        PlayThread.Start();
                        IsStartPlay = true;
                        return IsStartPlay;
                    }
                }
                else
                {
                    Console.WriteLine("没有该场景数据");
                    return false;
                }
            }
        }

        public static DMX512Play GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DMX512Play();
            }
            return Instance;
        }

        private bool SetData(DBWrapper wrapper,int senceNo)
        {
            bool resultFlag = false;
            DMX_C_File c_File = null;
            IList<C_Data> c_Datas = null;
            FormatTools formatTool = new FormatTools(wrapper.lightList, wrapper.stepCountList, wrapper.valueList);
            IList<DMX_C_File> c_Files = DMXTools.GetInstance().Get_C_Files(formatTool.GetC_SenceDatas(), Constant.MODE_C);
            foreach (DMX_C_File file in c_Files)
            {
                if (senceNo == file.SenceNo)
                {
                    c_File = file;
                    resultFlag = true;
                }
            }
            if(c_File != null)
            {
                ChanelCount = c_File.Data.HeadData.ChanelCount;
                c_Datas = c_File.Data.Datas;
                ChanelData = new byte[ChanelCount][];
                ChanelNos = new int[ChanelCount];
                for (int i = 0; i < ChanelCount; i++)
                {
                    C_Data c_Data = c_Datas[i];
                    ChanelNos[i] = c_Data.ChanelNo;
                    IList<byte> data = new List<byte>();
                    for (int j = 0; j < c_Data.DataSize; j++)
                    {
                        byte value = Convert.ToByte(c_Data.Datas[j]);
                        data.Add(value);
                    }
                    ChanelData[i] = data.ToArray();
                }
            }
            else
            {
                resultFlag = false;
            }
            return resultFlag;
        }

        private void Play()
        {
            UInt32 count = 0;
            while (IsPlay)
            {
               
                //模拟break效果
                myFtdiDevice.SetBaudRate(96000);
                myFtdiDevice.Write(StartCode, StartCode.Length, ref count);
                myFtdiDevice.SetBaudRate(250000);
                //发送Break
                //myFtdiDevice.SetBreak(true);
                //Thread.Sleep(10);
                //myFtdiDevice.SetBreak(false);
                myFtdiDevice.Write(StartCode, StartCode.Length, ref count);

                if (!IsPause)
                {
                    if (IsNewData)
                    {
                        Init();
                        IsNewData = false;
                    }
                    for (int addr = 0; addr < 512; addr++)
                    {
                        for (int chanel = 0; chanel < ChanelNos.Length; chanel++)
                        {
                            int chanelNo = ChanelNos[chanel];
							//  当(addr+1)和chanelNo相等时，设置值
                            if ( (addr + 1) == chanelNo)
                            {
                                int point = DataPoint[chanel];
                                PlayData[addr] = ChanelData[chanel][point];
                                DataPoint[chanel]++;
                                if (DataPoint[chanel] == ChanelData[chanel].Length)
                                {
                                    DataPoint[chanel] = 0;
                                }
                            }
                        }
                    }
                }

                //发送dmx512数据
                myFtdiDevice.Write(PlayData, PlayData.Length, ref count);
                Thread.Sleep(32);
                myFtdiDevice.Purge(FTDI.FT_PURGE.FT_PURGE_TX);
            }

        }

        private bool Connect()
        {
            string potName;
            UInt32 ftdiDeviceCount = 0;
            FTDI.FT_STATUS fT_STATUS = FTDI.FT_STATUS.FT_OK;
            myFtdiDevice = new FTDI();
            fT_STATUS = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);
            if (fT_STATUS == FTDI.FT_STATUS.FT_OK)
            {
                if (ftdiDeviceCount > 0)
                {
                    FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];
                    fT_STATUS = myFtdiDevice.GetDeviceList(ftdiDeviceList);
                    if (fT_STATUS == FTDI.FT_STATUS.FT_OK)
                    {
                        fT_STATUS = myFtdiDevice.OpenBySerialNumber(ftdiDeviceList[0].SerialNumber);
                        if (fT_STATUS == FTDI.FT_STATUS.FT_OK)
                        {
                            myFtdiDevice.GetCOMPort(out potName);
                            if (potName == null || potName == "")
                            {
                                myFtdiDevice.Close();
                            }
                            myFtdiDevice.SetBaudRate(250000);
                            myFtdiDevice.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_2, FTDI.FT_PARITY.FT_PARITY_NONE);
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
