using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Common
{
	public class StringHelper
	{
		/// <summary>
		/// 将十进制字符串转成二进制字符串
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string DecimalStringToBinary(string s)
		{
			int decimalInt = Convert.ToInt16( s );
			string result = Convert.ToString(decimalInt, 2);
			return result;
		}

		/// <summary>
		/// 将十进制字符串转成二进制字符串（指定位数）
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string DecimalStringToBitBinary(string s , int bitNum)
		{
			return DecimalStringToBinary(s).PadLeft(bitNum, '0');
		}

		/// <summary>
		/// 将十进制字符串转成十六进制字符串
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string DecimalStringToHex(string s)
		{
			int decimalInt = Convert.ToInt16(s);
			string result = Convert.ToString(decimalInt, 16);
			return result;
		}

		/// <summary>
		/// 十进制转换成指定位数的十六进制字符串
		/// </summary>
		/// <param name="s"></param>
		/// <param name="bitNum"></param>
		/// <returns></returns>
		public static string DecimalStringToBitHex(string s, int bitNum)
		{
			return DecimalStringToHex(s).PadLeft(bitNum, '0');
		}


		/// <summary>
		/// 将十六进制字符串转成十进制字符串
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string HexStringToDecimal(string s)
		{
			int i = int.Parse(s,System.Globalization.NumberStyles.HexNumber);
			return i.ToString();	
		}

		/// <summary>
		///  反转字符串
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string ReverseString(string s) {

			char[] strArray = s.ToCharArray();
			Array.Reverse(strArray);			
			return new string(strArray);
		}


		/// <summary>
		/// 把IList<int>转化为字符串，并用英文符号","隔开
		/// </summary>
		/// <param name="intList">要转化的List</param>
		/// <param name="addNum">需要对每个表内数字添加的数字(便于用户查看索引等)</param>
		/// <paramref name="captainIndex">若需强调其中的某个数，则输入这个参数</paramref>
		/// <returns></returns>
		public static string MakeIntListToString(IList<int> intList, int addNum, int captainIndex)
		{
			string result = "";
			for (int i = 0; i < intList.Count; i++)
			{
				if ( i == captainIndex  )
				{
					result += "(" + (intList[i] + addNum) + "),";					
				}
				else {
					result += intList[i] + addNum + ",";
				}				
			}
			return result.Substring(0, result.Length - 1);
		}
	}
}
