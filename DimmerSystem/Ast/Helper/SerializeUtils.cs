using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace LightController.Common
{
	class SerializeUtils
	{
		//静态公用方法：序列化操作，把文件实例化到binPath内
		public static void SerializeObject(string binPath , object obj)
		{
			using (FileStream fs = new FileStream(binPath, FileMode.Create))
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(fs, obj);
			}
		}

		//静态方法：反序列化操作，把路径还原成obj
		public static object DeserializeToObject(string binPath)
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
