using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Common
{
	class ZipAst
	{
		/// <summary>
		/// 压缩成zip(只压缩文件，不压缩文件夹)
		/// </summary>
		/// <param name="dirPath">要压缩的目录全路径</param>
		/// <param name="zipFile">要生成的zip文件全路径</param>
		/// <param name="level">压缩级别。0-9,0最快无压缩；9最好但最耗时。</param>
		/// <param name="password">加密密码，若为null或空字符串，则不加密（没有Trim,也就是可以是N个空格，N>0）</param>
		/// <param name="deletePath">要删除的冗余路径(避免压缩目录在极深的目录内，生成的压缩文件也有过多的层级)</param>
		public static void CompressAllToZip(string dirPath, string zipPath,int level,string password,string deletePath)
		{
			if (!Directory.Exists(dirPath))
			{
				Console.WriteLine("Cannot find directory '{0}'", dirPath);
				return;
			}
			try
			{
				Directory.CreateDirectory(Path.GetDirectoryName(zipPath));  //若zipPath所在的文件夹不存在，则创建该目录
				using (ZipOutputStream zos = new ZipOutputStream(File.Create(zipPath)))  //创建zip文件(无则创建，有则覆盖)，并用该文件流创建ZipOutputStream
				{
					zos.SetLevel( level );
					if (!string.IsNullOrEmpty(password)) {
						zos.Password = password;
						//zos.SetComment("Password:" + password);
					}
					Compress(dirPath, zos,deletePath);
					zos.Finish();  //结束写入
					zos.Close(); //关闭
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception during processing {0}", ex);
			}
		}

		/// <summary>
		/// 压缩目录内所有文件，采用递归的方法
		/// </summary>
		/// <param name="dirPath">目录路径</param>
		/// <param name="zipPath">zip文件路径</param>
		/// <param name="deletePath">要删除的冗余路径</param>
		public static void Compress(string dirPath, ZipOutputStream zos,string deletePath) {

			string[] allNames = Directory.GetFileSystemEntries(dirPath);
			foreach (string file in allNames) {
				//当file是文件夹时，递归跑子文件夹
				if (Directory.Exists(file))
				{
					Compress(file, zos, deletePath);
				}
				//当file是文件时，正常压缩
				else {
					using (FileStream fs = File.OpenRead(file)) {
						byte[] buffer = new byte[4 * 1024];
						ZipEntry entry = new ZipEntry(file.Replace(deletePath, ""));  //去掉文件的盘符（如D:）
						entry.DateTime = DateTime.Now;  //设定压缩文件内文件的创建时间
						zos.PutNextEntry(entry);  //往ZipOutputStream里写入一个ZipEntry
						int sourceBytes;
						do
						{
							sourceBytes = fs.Read(buffer, 0, buffer.Length);
							zos.Write(buffer, 0, sourceBytes);
						} while (sourceBytes > 0);
					}
				}
			}
		}

		/// <summary>
		/// 解压zip
		/// </summary>
		/// <param name="zipFilePath">d:\a.zip</param>
		public static void UnZipFile(string zipFilePath)
		{
			if (!File.Exists(zipFilePath))
			{
				Console.WriteLine("Cannot find file '{0}'", zipFilePath);
				return;
			}

			using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
			{

				ZipEntry theEntry;
				while ((theEntry = s.GetNextEntry()) != null)
				{

					Console.WriteLine(theEntry.Name);

					string directoryName = Path.GetDirectoryName(theEntry.Name);
					string fileName = Path.GetFileName(theEntry.Name);

					// create directory
					if (directoryName.Length > 0)
					{
						Directory.CreateDirectory(directoryName);
					}

					if (fileName != String.Empty)
					{
						using (FileStream streamWriter = File.Create(theEntry.Name))
						{

							int size = 2048;
							byte[] data = new byte[2048];
							while (true)
							{
								size = s.Read(data, 0, data.Length);
								if (size > 0)
								{
									streamWriter.Write(data, 0, size);
								}
								else
								{
									break;
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// 压缩成zip(只压缩文件，不压缩文件夹)
		/// </summary>
		/// <param name="dirPath">d:\</param>
		/// <param name="zipFilePath">d:\a.zip</param>
		public static void CompressFileToZip(string dirPath, string zipFilePath)
		{
			if (!Directory.Exists(dirPath))
			{
				Console.WriteLine("Cannot find directory '{0}'", dirPath);
				return;
			}
			try
			{
				string[] filenames = Directory.GetFiles(dirPath);
				using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
				{
					s.SetLevel(9); // 压缩级别 0-9,9最好但最耗时					
								   //s.Password = "TRANSJOY"; //Zip压缩文件密码
					byte[] buffer = new byte[4096]; //缓冲区大小
					foreach (string file in filenames)
					{
						ZipEntry entry = new ZipEntry(Path.GetFileName(file));
						entry.DateTime = DateTime.Now;
						s.PutNextEntry(entry);
						using (FileStream fs = File.OpenRead(file))
						{
							int sourceBytes;
							do
							{
								sourceBytes = fs.Read(buffer, 0, buffer.Length);
								s.Write(buffer, 0, sourceBytes);
							} while (sourceBytes > 0);
						}
					}
					s.Finish();
					s.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception during processing {0}", ex);
			}
		}

	}
}
