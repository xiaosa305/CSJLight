using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordTools.Utils
{
	/// <summary>
	/// 这是个数字与字符串等相关的辅助转换类，内置一些静态的方法，用来实现不同的目的
	/// </summary>
	public class MathHelper
	{
		/// <summary>
		///  由输入的num，自动补零的两位数字符串
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public static string GetAddZeroStr(int num)
		{
			num = num + 1;
			if (num < 10)
			{
				return "0" + num;
			}
			else
			{
				return num.ToString();
			}
		}

		/// <summary>
		///  辅助方法：将一个形如“单词串+数字”的组合，提取其数字值,再将这个数字减一，即可得到index值
		/// 《LightEditor》 method：取出触发的sender的Name，对其进行操作
		/// 1.替换掉非数字的字符串;2.将取出的数字-1；即可得到数组下标
		/// </summary>
		/// <param name="addNum">取出来的index需要加的数字，比如 label1 = labels[0] 则addNum = -1</param>
		public static int GetIndexNum(String senderName,int addNum)
		{
			string indexStr = System.Text.RegularExpressions.Regex.Replace(senderName, @"[^0-9]+", "");
			int numIndex = int.Parse(indexStr) + (addNum) ;
			return numIndex;
		}

		/// <summary>
		/// 辅助方法：将一个正整数变成占四个位置的字符串
		/// </summary>
		/// <param name="num">原整数</param>
		/// <returns></returns>
		public static string GetFourWidthNumStr(int num,bool insertBefore ) {
			string result = "";
			if (num < 10) {
				if (insertBefore)
				{
					result = " " + num + "  ";
				}
				else
				{
					result = "  " + num + " ";
				}
			} else if (num >= 10 && num < 100) {
				result = " " + num + " ";
			} else if (num >= 100 && num < 1000)
			{
				if (insertBefore)
				{
					result = num + " ";
				}
				else {
					result = " " + num ;
				}				
			} else {
				result = "" + num;
			}
			return result;
		}

		/// <summary>
		/// 辅助方法：传入两个int，将其相除后的小数向上取整并返回
		/// </summary>
		/// <param name="numA"></param>
		/// <param name="numB"></param>
		/// <returns></returns>
		public static int GetDivisionCelling(int numA, int numB) {
			decimal r1 = (decimal)numA / numB; //先相除，得到精确的decimal
			decimal r2 = Math.Ceiling(r1); //向上取整
			int r3 = decimal.ToInt32(r2); //转为int
			return r3;
		}

	}
}
