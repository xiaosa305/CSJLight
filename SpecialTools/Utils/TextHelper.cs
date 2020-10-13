using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Common
{
	public class TextHelper
	{
		/// <summary>
		/// 静态辅助方法：读取文本文件，把每一行的文本，输出到一个IList<string>中
		/// </summary>
		/// <param name="txtPath"></param>
		/// <returns></returns>
		public static IList<string> Read(string txtPath)		{
		
			StreamReader srReadFile = new StreamReader(txtPath);
			IList<string> strList = new List<string>();
			// 读取流直至文件末尾结束
			while (!srReadFile.EndOfStream)
			{
				string strReadLine = srReadFile.ReadLine(); //读取每行数据
				strList.Add(strReadLine);
			}

			// 关闭读取流文件
			srReadFile.Close();
			return strList;
		}
		
	}
}
