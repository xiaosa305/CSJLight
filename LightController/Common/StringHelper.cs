using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
			int decimalInt = Convert.ToInt32( s );
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
			int decimalInt = Convert.ToInt32(s);
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

		/// <summary>
		/// 获取text括号内的字符串，并转化为数字
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static int GetInnerValue(string text) {
			try
			{
				string valueStr = System.Text.RegularExpressions.Regex.Replace(text, @"(.*\()(.*)(\).*)", "$2");
				int value = int.Parse(valueStr);
				return value;
			}
			catch (Exception) {
				return 0;
			}
		}

		/// <summary>
		/// 判断传入的文件名，是否为png、jpg、bmp三种图片格式(ico不算在内)
		/// </summary>
		/// <param name="picName"></param>
		/// <returns></returns>
		public static bool IsPicFile(string picName) {
			return 
				picName.ToLower().EndsWith(".jpg") || 
				picName.ToLower().EndsWith(".png") || 
				picName.ToLower().EndsWith(".bmp");
		}

		/// <summary>
		/// 判断传入的字符串是否合法的IP
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		public static bool IsIP(string ip) {
			ip = ip.Trim();
			if (string.IsNullOrEmpty(ip)) {
				return false;
			}
			string pattern = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
			return Regex.IsMatch(ip, pattern);
		}

		/// <summary>
		/// 判断传入的字符串是否合法的MAC地址
		/// </summary>
		/// <param name="mac"></param>
		/// <returns></returns>
		public static bool IsMAC(string mac) {
			mac = mac.Trim();			
			if (string.IsNullOrEmpty( mac ))
			{
				return false;
			}
			string pattern = @"^([0-9a-fA-F]{2})(([/\s:-][0-9a-fA-F]{2}){5})$";
			return Regex.IsMatch(mac, pattern);
		}
	}
}
