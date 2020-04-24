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

		/// <summary>
		/// 静态辅助方法：拷贝文件到其他目录去；若源文件不存在，则不往下走；若目标文件夹不存在，则创建之
		/// </summary>
		/// <param name="sourceFile"></param>
		/// <param name="destPath"></param>
		/// <param name="overwrite">是否覆盖</param>
		public static void CopyFile(string sourceFile, string destPath,bool overwrite) {
			FileInfo fi = new FileInfo(sourceFile);
			if (fi.Exists) {
				DirectoryInfo di = new DirectoryInfo(destPath);
				if (!di.Exists) {
					di.Create();
				}
				File.Copy(fi.FullName, destPath + "\\" + fi.Name, overwrite);
			}		
		}
	}
}
