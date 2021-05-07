using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImportProtocol.Entity
{
	[Serializable]
	public class CCEntity
	{
		public virtual int CCIndex { get; set; }
		public virtual string ProtocolName { get; set; }
		public virtual int Com0 { get; set; }
		public virtual int Com1 { get; set; }
		/// <summary>
		/// 主：0 ；从：1
		/// </summary>
		public virtual int PS2 { get; set; }

		//public  IList<CCData> CCDataList { get; set; }

		public CCEntity()
		{
			//CCDataList = new List<CCData>();
		}

		//internal IList<int> SearchIndices(string keyword)
		//{
		//	IList<int> matchIndexList = new List<int>();

		//	for (int ccDataIndex = 0; ccDataIndex < CCDataList.Count; ccDataIndex++)
		//	{
		//		CCData cd = CCDataList[ccDataIndex];
		//		if (cd.Function.Contains(keyword) || cd.Code.Contains(keyword))
		//		{
		//			matchIndexList.Add(ccDataIndex);
		//		}
		//	}
		//	return matchIndexList;
		//}

		//public byte[] GetData()
		//{
		//	List<byte> dataBuff = Enumerable.Repeat(Convert.ToByte(0x00), 256 * 8 * 8).ToList();
		//	//添加串口0波特率
		//	switch (this.Com0)
		//	{
		//		case 9600:
		//			dataBuff[0] = 0x01;
		//			break;
		//		case 4800:
		//			dataBuff[0] = 0x02;
		//			break;
		//		case 1200:
		//			dataBuff[0] = 0x03;
		//			break;
		//		case 19200:
		//		default:
		//			dataBuff[0] = 0x00;
		//			break;
		//	}
		//	//添加串口1波特率
		//	switch (this.Com1)
		//	{
		//		case 9600:
		//			dataBuff[1] = 0x01;
		//			break;
		//		case 4800:
		//			dataBuff[1] = 0x02;
		//			break;
		//		case 1200:
		//			dataBuff[1] = 0x03;
		//			break;
		//		case 19200:
		//		default:
		//			dataBuff[1] = 0x00;
		//			break;
		//	}
		//	//添加PS2主从
		//	switch (this.PS2)
		//	{
		//		case 0://主
		//			dataBuff[2] = 0x01;
		//			break;
		//		case 1://从
		//		default:
		//			dataBuff[2] = 0x00;
		//			break;
		//	}
		//	for (int i = 0; i < this.CCDataList.Count; i++)
		//	{
		//		string[] values;
		//		int code = 0;
		//		CCData item = CCDataList[i];
		//		//读取码值
		//		if (item.Code == null)
		//		{
		//			return null;
		//		}
		//		code = Convert.ToInt32(item.Code, 16);
		//		//int.TryParse(item.Code, out int code);
		//		Console.WriteLine("码值为：" + code);
		//		//读取串口0上行数据组
		//		if (item.Com0Up == null)
		//		{
		//			return null;
		//		}
		//		if (item.Com0Up.Length > 0)
		//		{
		//			values = item.Com0Up.Split(' ');
		//			for (int valueIndex = 0; valueIndex < values.Length; valueIndex++)
		//			{
		//				dataBuff[0 * 2048 + code * 8 + valueIndex] = Convert.ToByte(values[valueIndex], 16);
		//			}
		//		}
		//		//读取串口0下行数据组
		//		if (item.Com0Down == null)
		//		{
		//			return null;
		//		}
		//		if (item.Com0Down.Length > 0)
		//		{
		//			values = item.Com0Down.Split(' ');
		//			for (int valueIndex = 0; valueIndex < values.Length; valueIndex++)
		//			{
		//				dataBuff[1 * 2048 + code * 8 + valueIndex] = Convert.ToByte(values[valueIndex], 16);
		//			}
		//		}
		//		//读取串口1上行
		//		if (item.Com1Up == null)
		//		{
		//			return null;
		//		}
		//		if (item.Com1Up.Length > 0)
		//		{
		//			values = item.Com1Up.Split(' ');
		//			for (int valueIndex = 0; valueIndex < values.Length; valueIndex++)
		//			{
		//				dataBuff[2 * 2048 + code * 8 + valueIndex] = Convert.ToByte(values[valueIndex], 16);
		//			}
		//		}
		//		//读取串口1下行
		//		if (item.Com1Down == null)
		//		{
		//			return null;
		//		}
		//		if (item.Com1Down.Length > 0)
		//		{
		//			values = item.Com1Down.Split(' ');
		//			for (int valueIndex = 0; valueIndex < values.Length; valueIndex++)
		//			{
		//				dataBuff[3 * 2048 + code * 8 + valueIndex] = Convert.ToByte(values[valueIndex], 16);
		//			}
		//		}
		//		//读取红外发送
		//		if (item.InfraredSend == null)
		//		{
		//			return null;
		//		}
		//		if (item.InfraredSend.Length > 0)
		//		{
		//			values = item.InfraredSend.Split(' ');
		//			for (int valueIndex = 0; valueIndex < values.Length; valueIndex++)
		//			{
		//				dataBuff[4 * 2048 + code * 8 + valueIndex] = Convert.ToByte(values[valueIndex], 16);
		//			}
		//		}
		//		//读取红外接收
		//		if (item.InfraredReceive == null)
		//		{
		//			return null;
		//		}
		//		if (item.InfraredReceive.Length > 0)
		//		{
		//			values = item.InfraredReceive.Split(' ');
		//			for (int valueIndex = 0; valueIndex < values.Length; valueIndex++)
		//			{
		//				dataBuff[5 * 2048 + code * 8 + valueIndex] = Convert.ToByte(values[valueIndex], 16);
		//			}
		//		}
		//		//读取PS2上行
		//		if (item.PS2Up == null)
		//		{
		//			return null;
		//		}
		//		if (item.PS2Up.Length > 0)
		//		{
		//			values = item.PS2Up.Split(' ');
		//			for (int valueIndex = 0; valueIndex < values.Length; valueIndex++)
		//			{
		//				dataBuff[6 * 2048 + code * 8 + valueIndex] = Convert.ToByte(values[valueIndex], 16);
		//			}
		//		}
		//		//读取PS2下行
		//		if (item.PS2Down == null)
		//		{
		//			return null;
		//		}
		//		if (item.PS2Down.Length > 0)
		//		{
		//			values = item.PS2Down.Split(' ');
		//			for (int valueIndex = 0; valueIndex < values.Length; valueIndex++)
		//			{
		//				dataBuff[7 * 2048 + code * 8 + valueIndex] = Convert.ToByte(values[valueIndex], 16);
		//			}
		//		}
		//	}
		//	return dataBuff.ToArray();
		//}
	}
}
