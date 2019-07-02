using FTD2XX_NET;
using LightController.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
	public class DMX512Player
	{
		private static DMX512Player Instance { get; set; }
		private byte[] PlayData { get; set; }
		private bool IsNewData { get; set; }
		private bool IsStartPlay { get; set; }
		private bool IsPause { get; set; }
		private bool IsPlay { get; set; }
		private bool IsMusicControl { get; set; }
		private bool IsOneLightStep { get; set; }
		private byte[][] ChanelData { get; set; }
		private int[] ChanelNos { get; set; }
		private int[] DataPoint { get; set; }
		private int ChanelCount { get; set; }
		private readonly byte[] StartCode = { 0x00 };
		private FTDI MyFtdiDevice { get; set; }
		private Thread PlayThread { get; set; }

		//test
		private int MusicChanelCount { get; set; }
		private byte[][] MusicChanelData { get; set; }
		private int[] MusicChanelNos { get; set; }
		private int[] MusicDataPoint { get; set; }

		private DMX512Player()
		{
			IsNewData = true;
			IsStartPlay = false;
			IsPlay = false;
			IsMusicControl = false;
			IsOneLightStep = false;
		}

		private void Init()
		{
			DataPoint = new int[ChanelCount];
			MusicDataPoint = new int[MusicChanelCount];
			PlayData = new byte[512];
			for (int i = 0; i < Constant.DMX512; i++)
			{
				PlayData[i] = Convert.ToByte(0);
			}
			for (int num = 0; num < ChanelCount; num++)
			{
				DataPoint[num] = 0;
			}
			for (int i = 0; i < MusicChanelCount; i++)
			{
				MusicDataPoint[i] = 0;
			}
		}

		/// <summary>
		/// 结束预览
		/// </summary>
		public void EndPreview()
		{
			if (IsOneLightStep)
			{
				IsOneLightStep = false;
			}
			if (IsPlay)
            {
                IsPlay = false;
            }

		}

        private void Reset()
        {
            IsPlay = false;
            IsOneLightStep = false;
            IsStartPlay = false;
            IsMusicControl = false;
            IsNewData = true;
            if (MyFtdiDevice != null)
            {
                if (MyFtdiDevice.IsOpen)
                {
                    MyFtdiDevice.Close();
                    MyFtdiDevice = null;
                }
            }
            ChanelData = null;
            ChanelNos = null;
            DataPoint = null;
            PlayData = null;
        }

		public void Preview(DBWrapper wrapper, int senceNo)
		{
            IsPause = true;
            if (IsOneLightStep)
            {
                IsOneLightStep = false;
                Thread.Sleep(300);
            }
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
				}
				else
				{
                    throw new DeviceException("没有场景数据");
				}
			}
			else
			{
				if (SetData(wrapper, senceNo))
				{
					Init();
					if (!Connect())
					{
                        throw new DeviceException("未连接设备");
					}
					else
					{
						IsPause = false;
						PlayThread = new Thread(new ThreadStart(Play));
						PlayThread.Start();
						IsStartPlay = true;
					}
				}
				else
				{
                    throw new DeviceException("没有该场景数据");
				}
			}
		}

		public static DMX512Player GetInstance()
		{
			if (Instance == null)
			{
				Instance = new DMX512Player();
			}
			return Instance;
		}

		private bool SetData(DBWrapper wrapper, int senceNo)
		{
			bool resultFlag = false;
			DMX_C_File c_File = null;
			IList<C_Data> c_Datas = null;
			FormatTools formatTool = new FormatTools(wrapper.lightList, wrapper.stepCountList, wrapper.valueList);
			IList<DMX_C_File> c_Files = DMXTools.GetInstance().Get_C_Files(formatTool.GetC_SenceDatas(), Constant.MODE_C);

			//test
			DMX_M_File m_File = null;
			IList<DMX_M_File> m_Files = DMXTools.GetInstance().Get_M_Files(formatTool.GetM_SenceDatas(), Constant.MODE_M);
			foreach (DMX_M_File file in m_Files)
			{
				if (senceNo == file.SenceNo)
				{
					m_File = file;
					resultFlag = true;
				}
			}
			if (m_File != null)
			{
				SetMusicData(m_File);
				IsMusicControl = true;
			}
			foreach (DMX_C_File file in c_Files)
			{
				if (senceNo == file.SenceNo)
				{
                    c_File = file;
                    c_File.Test();
					resultFlag = true;
				}
			}
			if (c_File != null)
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

		private bool SetMusicData(DMX_M_File file)
		{
			MusicChanelCount = file.data.HeadData.ChanelCount;
			IList<M_Data> m_Datas = file.data.Datas;
			MusicChanelData = new byte[MusicChanelCount][];
			MusicChanelNos = new int[MusicChanelCount];
			for (int i = 0; i < MusicChanelCount; i++)
			{
				M_Data m_Data = m_Datas[i];
				MusicChanelNos[i] = m_Data.ChanelNo;
				IList<byte> data = new List<byte>();
				for (int j = 0; j < m_Data.DataSize; j++)
				{
					byte value = Convert.ToByte(m_Data.Datas[j]);
					data.Add(value);
				}
				MusicChanelData[i] = data.ToArray();
			}
			return true;
		}
		/// <summary>
		/// 音频触发模拟（有bug）
		/// </summary>
		public void MusicControl()
		{
			UInt32 count = 0;
			if (IsMusicControl)
			{
				IsPause = true;

				for (int i = 0; i < MusicChanelCount; i++)
				{
					PlayData[MusicChanelNos[i] - 1] = MusicChanelData[i][MusicDataPoint[i]];
					MusicDataPoint[i]++;
					if (MusicDataPoint[i] == MusicChanelData[i].Length)
					{
						MusicDataPoint[i] = 0;
					}
				}
				MyFtdiDevice.SetBreak(true);
				Thread.Sleep(10);
				MyFtdiDevice.SetBreak(false);
				MyFtdiDevice.Write(StartCode, StartCode.Length, ref count);
				MyFtdiDevice.Write(PlayData, PlayData.Length, ref count);
				IsPause = false;
			}
			else
			{
                throw new DeviceException("没有音频数据");
			}

		}

		private void Play()
		{
			UInt32 count = 0;
			while (IsPlay)
			{
				//填充dmx512数据
				if (IsNewData)
				{
					Init();
					IsNewData = false;
				}
				for (int i = 0; i < ChanelCount; i++)
				{
					PlayData[ChanelNos[i] - 1] = ChanelData[i][DataPoint[i]];
					DataPoint[i]++;
					if (DataPoint[i] == ChanelData[i].Length)
					{
						DataPoint[i] = 0;
					}
				}
				if (!IsPause)
				{
					//发送Break
					MyFtdiDevice.SetBreak(true);
					Thread.Sleep(10);
					MyFtdiDevice.SetBreak(false);
					MyFtdiDevice.Write(StartCode, StartCode.Length, ref count);
					//发送dmx512数据
					MyFtdiDevice.Write(PlayData, PlayData.Length, ref count);
				}
				Thread.Sleep(32);
			}
            Reset();
		}

		private bool Connect()
		{
			string potName;
			UInt32 ftdiDeviceCount = 0;
			FTDI.FT_STATUS fT_STATUS = FTDI.FT_STATUS.FT_OK;
			MyFtdiDevice = new FTDI();
			fT_STATUS = MyFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);
			if (fT_STATUS == FTDI.FT_STATUS.FT_OK)
			{
				if (ftdiDeviceCount > 0)
				{
					FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];
					fT_STATUS = MyFtdiDevice.GetDeviceList(ftdiDeviceList);
					if (fT_STATUS == FTDI.FT_STATUS.FT_OK)
					{
						fT_STATUS = MyFtdiDevice.OpenBySerialNumber(ftdiDeviceList[0].SerialNumber);
						if (fT_STATUS == FTDI.FT_STATUS.FT_OK)
						{
							MyFtdiDevice.GetCOMPort(out potName);
							if (potName == null || potName == "")
							{
								MyFtdiDevice.Close();
							}
							MyFtdiDevice.SetBaudRate(250000);
							MyFtdiDevice.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_2, FTDI.FT_PARITY.FT_PARITY_NONE);
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
		/// <summary>
		/// 单灯单步调试
		/// </summary>
		/// <param name="data">dmx512数据</param>
		public void OneLightStep(byte[] data)
		{
            if (IsPlay)
            {
                IsPlay = false;
                Thread.Sleep(300);
            }
			if (!IsOneLightStep)
			{
				if (MyFtdiDevice != null)
				{
					IsOneLightStep = false;
				}
				IsOneLightStep = true;
				PlayData = data;
				Thread OneLightStepThread = new Thread(new ThreadStart(OneLightStepPlay));
				OneLightStepThread.Start();
			}
			else
			{
				PlayData = data;
			}
		}
		private void OneLightStepPlay()
		{
			UInt32 count = 0;
			if (Connect())
			{
				while (IsOneLightStep)
				{
					//发送Break
					MyFtdiDevice.SetBreak(true);
					Thread.Sleep(10);
					MyFtdiDevice.SetBreak(false);
					MyFtdiDevice.Write(StartCode, StartCode.Length, ref count);
					//发送dmx512数据
					MyFtdiDevice.Write(PlayData, PlayData.Length, ref count);
					Thread.Sleep(32);
				}
				Reset();
			}
			else
			{
                throw new DeviceException("连接设备失败");
			}
		}
	}
}
