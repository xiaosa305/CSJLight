using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace LBDConfigTool.Common
{
	class SerializeUtils
	{
		private static string binPath = @"C:\Temp\user.bin";

		/// <summary>
		/// 可更改binPath
		/// </summary>
		/// <param name="binPath"></param>
		public static void SetBinPath(string binPath ) {
			SerializeUtils.binPath = binPath;
		}

		//序列化操作
		public static void SerializeObject(object obj)
		{
			using (FileStream fs = new FileStream(binPath, FileMode.Create))
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(fs, obj);
				Console.WriteLine("序列化成功!");
			}
		}

		//反序列化操作
		public static object ReserializeMethod()
		{
			using (FileStream fs = new FileStream(binPath, FileMode.Open))
			{
				BinaryFormatter bf = new BinaryFormatter();
				object obj = bf.Deserialize(fs);
				return obj;
			}
		}

	}
}
