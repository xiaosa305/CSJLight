using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace LightController.Common
{
	public class IniFileAst_UTF8
	{

		[DllImport("kernel32")]
		public static extern bool WritePrivateProfileString(byte[] section, byte[] key, byte[] val, string filePath);

		[DllImport("kernel32")]
		public static extern int GetPrivateProfileString(byte[] section, byte[] key, byte[] def, byte[] retVal, int size, string filePath);

		//与ini交互必须统一编码格式
		private static byte[] getBytes(string s, string encodingName)
		{
			return null == s ? null : Encoding.GetEncoding(encodingName).GetBytes(s);
		}

		public static string ReadString(string fileName, string section, string key, string def,  string encodingName = "utf-8", int size = 1024)
		{
			byte[] buffer = new byte[size];
			int count = GetPrivateProfileString(getBytes(section, encodingName), getBytes(key, encodingName), getBytes(def, encodingName), buffer, size, fileName);
			return Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
		}

		public static bool WriteString(string fileName, string section, string key, string value, string encodingName = "utf-8")
		{
			return WritePrivateProfileString(getBytes(section, encodingName), getBytes(key, encodingName), getBytes(value, encodingName), fileName);
		}
	}
}
