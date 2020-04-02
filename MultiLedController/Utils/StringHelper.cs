using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.utils
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

	}
}
