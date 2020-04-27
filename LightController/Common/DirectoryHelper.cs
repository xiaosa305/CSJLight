using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Common
{
	public class DirectoryHelper
	{
		public enum SortMethodEnum{
			FILE_NAME,
			CREATION_TIME,
			LAST_WRITE_TIME
		}

		/// <summary>
		/// 静态辅助方法：将源路径的所有文件（不复制文件夹），复制到目标路径内
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

		/// <summary>
		/// 静态辅助方法：通过传入的string数组，创建相应的DirectoryInfo数组，主要是供排序方法使用
		/// </summary>
		/// <param name="dirStrArray"></param>
		/// <returns></returns>
		public static DirectoryInfo[] GenerateDiretoryInfoArray(string[] dirStrArray) {
			
			if (dirStrArray == null || dirStrArray.Length == 0)
			{
				return null;
			}

			DirectoryInfo[] diArray = new DirectoryInfo[dirStrArray.Length];
			for (int i = 0; i < dirStrArray.Length; i++)
			{
				diArray[i] = new DirectoryInfo(dirStrArray[i]);
			}
			return diArray;
		}

		/// <summary>
		/// 辅助方法：按文件夹最后访问时间排序（倒序）
		/// </summary>
		/// <param name="dirs">待排序文件夹数组</param>
		public static void SortAsFolderByCreationTime(ref DirectoryInfo[] dirs)
		{
			Array.Sort(dirs, delegate (DirectoryInfo x, DirectoryInfo y) {
				return y.CreationTime.CompareTo(x.CreationTime);
			});
		}

		/// <summary>
		/// 辅助方法：按文件夹最后写入时间排序（倒序）:最新的在最前面
		/// </summary>
		/// <param name="dirs">待排序文件夹数组</param>
		public static void SortAsFolderByLastWriteTime(ref DirectoryInfo[] dirs)
		{
			Array.Sort(dirs, delegate (DirectoryInfo x, DirectoryInfo y) {				
				return y.LastWriteTime.CompareTo(x.LastWriteTime);
			});
		}
	}
}
