using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Common
{
	public class FileAst
	{
		/// <summary>
		/// 静态辅助方法：用以验证文件名是否含有非法字符:返回true为合法，false非法
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static bool CheckFileName(string fileName) {

			char[] illegalChars = new char[] { '\\' , '/' , ':' , '*', '?' , '"','<', '>', '|' };
			if (fileName.IndexOfAny(illegalChars) > -1 )
			{
				return false;
			}
			else {
				return true;
			}			
		}	

	}
}
