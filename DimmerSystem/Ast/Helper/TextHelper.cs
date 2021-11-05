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
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static IList<string> Read(string filePath)		{
		
			StreamReader srReadFile = new StreamReader(filePath);
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

		/// <summary>
		/// 静态辅助方法：把List<string>的内容，保存到文件中
		/// </summary>
		/// <param name="filePath"></param>
		public static void Write(string filePath,IList<string> strList) {

			StreamWriter sw = new StreamWriter(filePath , false, System.Text.Encoding.UTF8);
			for (int strIndex = 0; strIndex < strList.Count; strIndex++)
			{
				sw.WriteLine(strList[strIndex]);
			}
			sw.Close();
		}
		
	}
}
