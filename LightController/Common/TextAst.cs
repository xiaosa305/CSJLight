using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightEditor.Common
{
	public class File
	{
		public IList<string> read(string txtPath)		{
		
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
