using LightController.Common;
using LightController.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Entity
{
	public class KeyEntity
	{
		public string CRC { get; set; }
		public string[] Key0Array { get; set; }
		public string[] Key1Array { get; set; }

		public KeyEntity()
		{
			Key0Array = new string[24];
			Key1Array = new string[24]; 
		}

        public KeyEntity(List<byte> data)
        {
            for (int i = 0; i < 24; i++)
            {
                Key0Array[i] = StringHelper.DecimalStringToBitHex(Convert.ToInt16(data[i]).ToString(), 8);
                Key1Array[i] = StringHelper.DecimalStringToBitHex(Convert.ToInt16(data[i + 24]).ToString(), 8);
            }
        }

        public byte[] GetData()
        {
            List<byte> data = new List<byte>();
            for (int i = 0; i < 24; i++)
            {
                data.Add(Convert.ToByte(StringHelper.HexStringToDecimal(Key0Array[i])));
            }
            for (int i = 0; i < 24; i++)
            {
                data.Add(Convert.ToByte(StringHelper.HexStringToDecimal(Key1Array[i])));
            }
            data.AddRange(CRCTools.GetInstance().GetLightControlCRC(data.ToArray()));
            return data.ToArray() ;
        }
	}
}
