using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightEditor.Common
{
	public class FileAst
	{

		/// <summary>
		/// 辅助静态方法：用以验证文件名是否含有非法字符
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static bool checkFileName(string fileName) {

			char[] illegalChars = new char[] { '\\' , '/' , ':' , '?' , '<', '>', '|', '"' };
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
