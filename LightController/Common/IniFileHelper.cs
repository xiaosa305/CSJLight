using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace LightController.Common
{
	public class IniFileHelper
	{
		public string filePath;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="aFileName">Ini文件路径</param>
		public IniFileHelper(string filePath)
		{
			this.filePath = filePath;
		}

		[DllImport("kernel32.dll")]
		private static extern int GetPrivateProfileInt(
			string lpAppName,
			string lpKeyName,
			int nDefault,
			string lpFileName
			);

		[DllImport("kernel32.dll")]
		private static extern int GetPrivateProfileString(
			string lpAppName,
			string lpKeyName,
			string lpDefault,
			StringBuilder lpReturnedString,
			int nSize,
			string lpFileName
			);		

		[DllImport("kernel32.dll")]
		private static extern int WritePrivateProfileString(
			string lpAppName,
			string lpKeyName,
			string lpString,
			string lpFileName
			);

		/// <summary>
		/// [扩展]读Int数值
		/// </summary>
		/// <param name="section">节</param>
		/// <param name="name">键</param>
		/// <param name="def">默认值</param>
		/// <returns></returns>
		public int ReadInt(string section, string name, int def)
		{
			return GetPrivateProfileInt(section, name, def, this.filePath);
		}

		/// <summary>
		/// [扩展]读取string字符串
		/// </summary>
		/// <param name="section">节</param>
		/// <param name="name">键</param>
		/// <param name="def">默认值</param>
		/// <returns>以key读取到的value字符串</returns>
		public string ReadString(string section, string name, string def)
		{
			StringBuilder vRetSb = new StringBuilder(2048);
			GetPrivateProfileString(section, name, def, vRetSb, 2048, this.filePath);
			//Console.WriteLine( " -----" +  vRetSb .ToString() );
			return vRetSb.ToString();
		}
		
		/// <summary>
		/// 读取指定 节-键 的值
		/// </summary>
		/// <param name="section"></param>
		/// <param name="name"></param>
		/// <returns>通过键名称获取相关值，统一先视为之字符串</returns>
		public string IniReadValue(string section, string name)
		{
			StringBuilder strSb = new StringBuilder(256);
			GetPrivateProfileString(section, name, "", strSb, 256, this.filePath);
			return strSb.ToString();
		}


		/// <summary>
		/// [扩展]写入Int数值，如果不存在 节-键，则会自动创建
		/// </summary>
		/// <param name="section">节</param>
		/// <param name="name">键</param>
		/// <param name="Ival">写入值</param>
		public void WriteInt(string section, string name, int Ival)
		{
			WritePrivateProfileString(section, name, Ival.ToString(), this.filePath);
		}

		/// <summary>
		///  [拓展]用于decimal类型的数据直接写入
		/// </summary>
		/// <param name="section"></param>
		/// <param name="name"></param>
		/// <param name="Ival"></param>
		public void WriteInt(string section, string name, Decimal Ival)
		{
			int value = 0;
			try
			{
				value  = int.Parse(Ival.ToString()); 
			}
			catch (Exception) {
				double tempValue = Double.Parse(Ival.ToString());
				value = Convert.ToInt16(tempValue);
			}

			WritePrivateProfileString(section, name, value.ToString(), this.filePath);
		}

		/// <summary>
		/// [扩展]写入String字符串，如果不存在 节-键，则会自动创建
		/// </summary>
		/// <param name="section">节</param>
		/// <param name="name">键</param>
		/// <param name="strVal">写入值</param>
		public void WriteString(string section, string name, string strVal)
		{
			WritePrivateProfileString(section, name, strVal, this.filePath);
		}

		/// <summary>
		/// 删除指定的 节
		/// </summary>
		/// <param name="section"></param>
		public void DeleteSection(string section)
		{
			WritePrivateProfileString(section, null, null, this.filePath);
		}

		/// <summary>
		/// 删除全部 节
		/// </summary>
		public void DeleteAllSection()
		{
			WritePrivateProfileString(null, null, null, this.filePath);
		}		

		/// <summary>
		/// 写入指定值，如果不存在 节-键，则会自动创建
		/// </summary>
		/// <param name="section"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public void IniWriteValue(string section, string name, string value)
		{
			WritePrivateProfileString(section, name, value, this.filePath);
		}

		/// <summary>
		/// 辅助方法：直接通过本类的实例，获取相关的savePath（当前应用目录还是固定位置）
		/// </summary>
		/// <returns></returns>
		public static string GetSavePath(string appPathStr) {

			IniFileHelper iniFileAst = new IniFileHelper(appPathStr + @"\GlobalSet.ini");
			string appPath = iniFileAst.ReadString("SavePath", "useAppPath", "false");  
			if (appPath.Trim().ToLower().Equals("true"))
			{
				return appPathStr;
			}
			else
			{
				return iniFileAst.ReadString("SavePath", "otherPath", "");
			} 
		}

		/// <summary>
		/// 辅助方法：取出是否显示按钮
		/// </summary>
		/// <returns></returns>
		public static bool GetControlShow(string appPathStr, string buttonName)
		{
			IniFileHelper iniFileAst = new IniFileHelper(appPathStr + @"\GlobalSet.ini");
			string isShow = iniFileAst.ReadString("Show", buttonName, "false");
			if (isShow.Trim().ToLower().Equals("true")){
				return true;
			}
			else {
				return false;
			}
		}

		/// <summary>
		/// 辅助方法：取出是否关联外部的软件
		/// </summary>
		/// <returns></returns>
		public static bool GetIsLink(string appPathStr, string appName)
		{
			IniFileHelper iniFileAst = new IniFileHelper(appPathStr + @"\GlobalSet.ini");
			string isLink = iniFileAst.ReadString("Link", appName, "false");
			if (isLink.Trim().ToLower().Equals("true"))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 辅助方法：取出系统的一些数值
		/// </summary>
		/// <param name="startupPath"></param>
		/// <param name="v"></param>
		/// <returns></returns>
		public static int GetSystemCount(string appPathStr, string attrName)
		{
			IniFileHelper iniFileAst = new IniFileHelper(appPathStr + @"\GlobalSet.ini");
			return iniFileAst.ReadInt("System", attrName, 0);
		}

		//与ini交互必须统一编码格式
		private static byte[] getBytes(string s, string encodingName)
		{
			return null == s ? null : Encoding.GetEncoding(encodingName).GetBytes(s);
		}

	
	}
}
