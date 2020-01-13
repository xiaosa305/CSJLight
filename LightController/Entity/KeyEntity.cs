using LightController.Common;
using LightController.Tools;
using System;
using System.Collections.Generic;
using System.IO;
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
            this.Key0Array = new string[24];
            this.Key1Array = new string[24];
            for (int i = 0; i < 24; i++)
            {
                this.Key0Array[i] = StringHelper.DecimalStringToBitHex(Convert.ToInt16(data[i]).ToString(), 2);
                this.Key1Array[i] = StringHelper.DecimalStringToBitHex(Convert.ToInt16(data[i + 24]).ToString(), 2);
            }
        }

        public static KeyEntity GetTest()
        {
            KeyEntity entity = new KeyEntity();
            for (int i = 0; i < 24; i++)
            {
                entity.Key0Array[i] = StringHelper.DecimalStringToBitHex(i.ToString(), 2);
                entity.Key1Array[i] = StringHelper.DecimalStringToBitHex((i + 24).ToString(), 2);
            }
            return entity;
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
    }
}
