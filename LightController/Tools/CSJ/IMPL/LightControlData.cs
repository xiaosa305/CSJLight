using LightController.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
	[Serializable]
	public class LightControlData : ICSJFile
	{
		private const int SCENECOUNT = 17;

		public int RelayCount { get; set; }//Address:0-继电器通道数
		public int DmxCount { get; set; }//Address:1-调光通道数
		public int RelayDataSize { get; set; }//Address:2-继电器占用的字节数
		public bool IsOpenFan { get; set; }//Address:3-排风功能：开启/关闭
		public int AirControlSwitch{ get; set; }//Address:4-空调阀开关功能：0x01为2线，0x02为3线; 0x00为禁用
		public bool IsOpenAirCondition { get; set; }//Address:5-空调功能：开启/禁用
		public int LightProtocol { get; set; }//Address:6-灯光协议（未用到）
		public int LightMode { get; set; }//Address:7-灯光模式：0x00叠加/0x01切换
		public int FanChannel { get; set; }//Address8-排风占用的通道数
		public int HighFanChannel { get; set; }//Address9-空调高风占用的通道数
		public int MiddleFanChannel { get; set; }//Address10-空调中风占用的通道数
		public int LowFanChannel { get; set; }//Address11-空调低风占用的通道数
		public int OpenAirConditionChannel { get; set; }//Address12-空调阀开占用的通道数
		public int CloseAirConditionChannel { get; set; }//Address13-空调阀关占用的通道数
		public int PlaceHolder1 { get; set; }//Address14-预留
		public int PlaceHolder2 { get; set; }//Address15-预留
		public bool[,] SceneData { get; set; }//Address16~15 + (17 * ((RelayCount - 1) / 8 + 1))  : 1true 0 false
        public int[] DmxData { get; set; }//Address16


        public SequencerData SequencerData { get; set; }
        public LightControllerSCR LightControllerSCR { get; set; }

        public static LightControlData GetTestData()
        {
            LightControlData data = new LightControlData();
            data.RelayCount = 12;
            data.DmxCount = 0;
            data.RelayDataSize = 2;
            data.IsOpenFan = false;
            data.AirControlSwitch = 2;
            data.IsOpenAirCondition = false;
            data.LightProtocol = 0;
            data.LightMode = 1;
            data.FanChannel = 6;
            data.HighFanChannel = 9;
            data.MiddleFanChannel = 8;
            data.LowFanChannel = 7;
            data.OpenAirConditionChannel = 10;
            data.CloseAirConditionChannel = 11;
            data.PlaceHolder1 = 0;
            data.PlaceHolder2 = 0;
            data.SceneData = new bool[17, 12];
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    data.SceneData[i, j] = j%2 == 0 ;
                }
            }
            return data;
        }
        public LightControlData()
        {
				
		}
        public LightControlData(List<byte> data)
        {
            RelayCount = Convert.ToInt32(data[0]);//0
            DmxCount = Convert.ToInt32(data[1]);//1
            RelayDataSize = Convert.ToInt32(data[2]);//2
            IsOpenFan = Convert.ToInt32(data[3]) == 1;//3
            AirControlSwitch = Convert.ToInt32(data[4]);//4
            IsOpenAirCondition = Convert.ToInt32(data[5]) == 1;//5
            LightProtocol = Convert.ToInt32(data[6]);//6
            LightMode = Convert.ToInt32(data[7]);//7
            FanChannel = Convert.ToInt32(data[8]);//8
            HighFanChannel = Convert.ToInt32(data[9]);//9
            MiddleFanChannel = Convert.ToInt32(data[10]);//10
            LowFanChannel = Convert.ToInt32(data[11]);//11
            OpenAirConditionChannel = Convert.ToInt32(data[12]);//12
            CloseAirConditionChannel = Convert.ToInt32(data[13]);//13
            PlaceHolder1 = Convert.ToInt32(data[14]);//14
            PlaceHolder2 = Convert.ToInt32(data[15]);//15
            SceneData = new bool[17, RelayCount];
            for (int frameIndex = 0; frameIndex < 17; frameIndex++)
            {
                for (int dataIndex = 0; dataIndex < RelayDataSize; dataIndex++)
                {
                    byte value = data[16 + 17 * dataIndex + frameIndex];
                    string valueStr = StringHelper.ReverseString(StringHelper.DecimalStringToBitBinary(Convert.ToInt32(value).ToString(), 8));
                    for (int i = 0; i < 8 && (dataIndex * 8 + i) < RelayCount; i++)
                    {
                        SceneData[frameIndex, dataIndex * 8 + i] = valueStr[i].Equals('1');
                    }
                }
            }
            if (DmxCount != 0)
            {
                DmxData = new int[DmxCount];
                for (int i = 0; i < DmxCount; i++)
                {
                    DmxData[i] = data[15 + 17 * RelayDataSize + i];
                }
            }
            this.SequencerData = SequencerData.Build(data.ToArray());
            this.LightControllerSCR = LightControllerSCR.Build(data.ToArray());
        }

        /// <summary>
		/// Dickov：逐行读取cfg文件，并把内容填入lcEntity中（旧版的读灯控配置的方法，需保留以兼容）
		/// </summary>
		/// <param name="data"></param>
        public LightControlData(IList<string> data)
        {
            RelayCount = Convert.ToInt32(data[0]);//0
            DmxCount = Convert.ToInt32(data[1]);//1
            RelayDataSize = Convert.ToInt32(data[2]);//2
            IsOpenFan = Convert.ToInt32(data[3]) == 1;//3
			AirControlSwitch = Convert.ToInt32(data[4]);//4
            IsOpenAirCondition = Convert.ToInt32(data[5]) == 1;//5
            LightProtocol = Convert.ToInt32(data[6]);//6
            LightMode = Convert.ToInt32(data[7]);//7
            FanChannel = Convert.ToInt32(data[8]);//8
            HighFanChannel = Convert.ToInt32(data[9]);//9
            MiddleFanChannel = Convert.ToInt32(data[10]);//10
            LowFanChannel = Convert.ToInt32(data[11]);//11
            OpenAirConditionChannel = Convert.ToInt32(data[12]);//12
            CloseAirConditionChannel = Convert.ToInt32(data[13]);//13
            PlaceHolder1 = Convert.ToInt32(data[14]);//14
            PlaceHolder2 = Convert.ToInt32(data[15]);//15

			//Dickov: 填入SceneData;
			SceneData = new bool[ 17 , RelayCount];			
			for (int frameIndex = 0; frameIndex < 17; frameIndex++)
			{
				string tempSceneDataStr = "";
				for (int relayDataIndex = 0; relayDataIndex < RelayDataSize; relayDataIndex++)
				{					
					int index = 16 + frameIndex + relayDataIndex * 17;
					tempSceneDataStr += StringHelper.ReverseString(StringHelper.DecimalStringToBitBinary(data[index], 8));					
				}			
				for (int relayDataIndex = 0; relayDataIndex < RelayCount; relayDataIndex++)
				{
					SceneData[frameIndex, relayDataIndex] = tempSceneDataStr.ElementAt(relayDataIndex).Equals('1');
				}
			}
		}

        
		//MARK：此处我有稍微修改：SceneData[][] -> SceneData[,] ；后期需测试 --By Dickov
        public byte[] GetData()
        {
            List<byte> data = new List<byte>();
            data.Add(Convert.ToByte(RelayCount));//0
            data.Add(Convert.ToByte(DmxCount));//1
            data.Add(Convert.ToByte(RelayDataSize));//2
            data.Add(Convert.ToByte(IsOpenFan ? 1 : 0));//3
            data.Add(Convert.ToByte(AirControlSwitch));//4
            data.Add(Convert.ToByte(IsOpenAirCondition ? 1 : 0));//5
            data.Add(Convert.ToByte(LightProtocol));//6
            data.Add(Convert.ToByte(LightMode));//7
            data.Add(Convert.ToByte(FanChannel));//8
            data.Add(Convert.ToByte(HighFanChannel));//9
            data.Add(Convert.ToByte(MiddleFanChannel));//10
            data.Add(Convert.ToByte(LowFanChannel));//11
            data.Add(Convert.ToByte(OpenAirConditionChannel));//12
            data.Add(Convert.ToByte(CloseAirConditionChannel));//13
            data.Add(Convert.ToByte(PlaceHolder1));//14
            data.Add(Convert.ToByte(PlaceHolder2));//15


            for (int relayDataIndex = 0; relayDataIndex < RelayDataSize; relayDataIndex++)
            {
                for (int sceneIndex = 0; sceneIndex < 17; sceneIndex++)
                {
                    string strValue = "";
                    for (int relayIndex = 0; relayIndex < ((RelayCount - relayDataIndex * 8) > 8 ? 8 : RelayCount - relayDataIndex * 8); relayIndex++)
                    {
                        strValue =(SceneData[sceneIndex, relayDataIndex * 8 + relayIndex] ? "1" : "0") + strValue;
                    }
                    strValue = strValue.PadLeft(8, '0');
                    data.Add(Convert.ToByte(strValue, 2));
                }
            }
            if (DmxData != null)
            {
                for (int i = 0; i < DmxData.Length; i++)
                {
                    data.Add(Convert.ToByte(DmxData[i]));
                }
            }
            try
            {
                if (this.SequencerData != null)
                {
                    data.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 80 - data.Count).ToArray());
                    data[60] = this.SequencerData.IsOpenSequencer ? Convert.ToByte(0x01) : Convert.ToByte(0x00);
                    data.AddRange(this.SequencerData.GetData());
                }
                if (this.LightControllerSCR != null)
                {
                    if (data.Count < 400)
                    {
                        data.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 400 - data.Count).ToArray());
                    }
                    data.AddRange(LightControllerSCR.GetData());
                }
                if (this.SequencerData != null && data.Count < 498)
                {
                    data.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 498 - data.Count).ToArray());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
          
            data.AddRange(CRCTools.GetInstance().GetLightControlCRC(data.ToArray()));
            return data.ToArray();
        }
        public void WriteToFile(string filepath)
        {
            byte[] data = this.GetData();
            string strValue = "";
            for (int i = 0; i < data.Length; i++)
            {
                strValue = strValue + data[i].ToString() + "\r\n";
            }
            File.WriteAllText(filepath, strValue);
        }
		public override string ToString()
		{
			return base.ToString();
		}

		/// <summary>
		///  辅助方法：把某个场景的开关列表，转为byte数组
		/// </summary>
		/// <param name="sceneIndex"></param>
		/// <returns></returns>
		public byte[] GetSceneDebugBytes(int sceneIndex)
		{
			byte[] data = new byte[RelayDataSize];

			string tempStr = "";
			for (int relayIndex = 0; relayIndex < RelayCount; relayIndex++)
			{
				tempStr += SceneData[sceneIndex, relayIndex] ? "1" : "0";
			}

			if (RelayCount <= 8)
			{
				tempStr = StringHelper.ReverseString(tempStr.PadRight(8, '0'));
				data[0] = Convert.ToByte(tempStr, 2);
			}
			else if (RelayCount <= 16)
			{
				tempStr = tempStr.PadRight(16, '0');
				string str1 = StringHelper.ReverseString(tempStr.Substring(0, 8));
				data[0] = Convert.ToByte(str1, 2);				
				string str2 = StringHelper.ReverseString(tempStr.Substring(8, 8));
				data[1] = Convert.ToByte(str2, 2);
			}
			else
			{
				tempStr = tempStr.PadRight(24, '0');

				string str1 = StringHelper.ReverseString(tempStr.Substring(0, 8));
				data[0] = Convert.ToByte(str1, 2);
				string str2 = StringHelper.ReverseString(tempStr.Substring(8, 8));
				data[1] = Convert.ToByte(str2, 2);
				string str3 = StringHelper.ReverseString(tempStr.Substring(16, 8));
				data[2] = Convert.ToByte(str3, 2);
			}

            List<byte> buff = new List<byte>();
            buff.AddRange(data);

            //Dickov 直接从当前对象读取调光数据即可
            if ( LightControllerSCR != null )
            {
                for (int tgIndex = 0; tgIndex < 2; tgIndex++)
                {
                    buff.Add(Convert.ToByte(LightControllerSCR.ScrData[sceneIndex,tgIndex]));    
                }
                for (int tgIndex = 2; tgIndex < 4; tgIndex++) {
                    buff.Add(Convert.ToByte(0));
                }
            }

            //TODEL
            //Console.WriteLine( buff );

            return buff.ToArray();
		}
	}
}
