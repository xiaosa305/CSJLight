using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Common
{
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
	}
}
