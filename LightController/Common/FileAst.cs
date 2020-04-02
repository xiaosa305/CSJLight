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
		/// 静态辅助方法：将源路径的所有文件，复制到目标路径内
		/// </summary>
		/// <param name="srcPath"></param>
		/// <param name="destPath"></param>
		public static void CopyDirectory(string srcPath, string destPath)
		{
			try
			{
				DirectoryInfo dir = new DirectoryInfo(srcPath);
				FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
				foreach (FileSystemInfo i in fileinfo)
				{
					if (i is DirectoryInfo)     //判断是否文件夹
					{
						if (!Directory.Exists(destPath + "\\" + i.Name))
						{
							Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
						}
						CopyDirectory(i.FullName, destPath + "\\" + i.Name);    //递归调用复制子文件夹
					}
					else
					{
						File.Copy(i.FullName, destPath + "\\" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

	}
}
