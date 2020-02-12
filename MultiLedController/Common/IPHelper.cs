using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MultiLedController.Common
{
	public class IPHelper
	{
		/// <summary>
		/// 分解字符串以判断是否ipv4
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		public static bool IsIPV4(string ip) {

			int num;
			//分割成四段
			string[] ip_4 = ip.Split('.');
			if (ip_4.Length != 4) return false;

			for (int i = 0; i < 4; i++)
			{
				if (!int.TryParse(ip_4[i], out num)) return false;
				if (num < 0 && num > 255) return false;
			}
			//全部检查完毕 无错误 
			return true;
		}

		/// <summary>
		/// 用正则表达式来判断是否ipv4
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		public static bool IsIPV4Regex(string ip) {
			return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
		}

		/// <summary>
		/// 判断传入字符串是否子网掩码
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		public static bool IsSubmask(string mask) {

			//11111 0000  是否分割 标志位
			bool zero = false;
			//分割成四段
			string[] ip_4 = mask.Split('.');
			if (ip_4.Length != 4) return false;
			for (int i = 0; i < 4; i++)
			{
				//如果是255
				if (ip_4[i].Equals("255"))
				{
					if (zero) return false;
				}
				//其它网段 前面全为1 后面全为0
				else if (ip_4[i].Equals("128") ||
						 ip_4[i].Equals("192") || ip_4[i].Equals("224") ||
						 ip_4[i].Equals("240") || ip_4[i].Equals("248") ||
						 ip_4[i].Equals("252") || ip_4[i].Equals("254"))
				{
					//标志位为1 后面应该全为0 退出 掩码格式错误
					if (zero) return false;
					zero = true;
				}
				//全是0
				else if (ip_4[i].Equals("0") || ip_4[i].Equals("00") || ip_4[i].Equals("000"))
				{
					//还没有分割 则标志位为1 分割 1 0
					if (!zero) zero = true;
				}
				//其它数字或者字符
				else return false;
			}
			//全部检查完毕 无错误 
			return true;
		}

		/// <summary>
		/// 将子网掩码字符串形式转换为位数
		/// </summary>
		/// <param name="submask"></param>
		/// <returns></returns>
		public static int CalcSubmask(string submask) {

			int totalBits = 0;
			foreach (string octet in submask.Split('.'))
			{
				byte octetByte = byte.Parse(octet);
				while (octetByte != 0)
				{
					totalBits += octetByte & 1;     // logical AND on the LSB
					octetByte >>= 1;            // do a bitwise shift to the right to create a new LSB
				}
			}
			return totalBits;
		}

	}
}
