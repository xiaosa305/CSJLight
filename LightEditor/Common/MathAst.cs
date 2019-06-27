using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightEditor.Common
{
	/// <summary>
	/// 这是个数字与字符串等相关的辅助转换类，内置一些静态的方法，用来实现不同的目的
	/// </summary>
	public class MathAst
	{

		/// <summary>
		///  由输入的num，自动补零的两位数字符串
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public static string getAddZeroStr(int num)
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
		///  将一个形如“单词串+数字”的组合，提取其数字值,再将这个数字减一，即可得到index值
		/// 《LightEditor》 method：取出触发的sender的Name，对其进行操作
		/// 1.替换掉非数字的字符串;2.将取出的数字-1；即可得到数组下标
		/// </summary>
		/// <param name="addNum">取出来的index需要加的数字，比如 label1 = labels[0] 则addNum = -1</param>
		public static int getIndexNum(String senderName,int addNum)
		{
			string labelIndexStr = System.Text.RegularExpressions.Regex.Replace(senderName, @"[^0-9]+", "");
			int numIndex = int.Parse(labelIndexStr) + (addNum) ;
			return numIndex;
		}

	}
}
