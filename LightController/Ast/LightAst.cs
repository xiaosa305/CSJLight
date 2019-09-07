using DMX512;
using LightController.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.Ast
{
	public class LightAst
	{
		public String LightPath { get; set; }
		
		// 此四个属性，可由ini文件获取
		public string LightName { get; set; }
		public string LightType { get; set; }
		public string LightPic { get; set; }
		public int Count { get; set; }

		// 此三个属性，每个灯都不一样，在灯库编辑时添加
		public string LightAddr { get; set; }
		public int StartNum { get; set; }
		public int EndNum { get; set; }		
	
		public override string ToString()
		{
			return LightName + ":" + LightType + ":" + LightAddr + ":" + LightPic + ":" + Count;
		}

		/// <summary>
		///  内置静态辅助方法 : 使用LightAst生成DB_Light
		/// </summary>
		/// <param name="la"></param>
		/// <returns></returns>
		public static DB_Light GenerateLight(LightAst la)
		{
			return new DB_Light()
			{
				LightNo = la.StartNum,
				StartID = la.StartNum,
				Name = la.LightName,
				Type = la.LightType,
				Pic = la.LightPic,
				Count = la.Count
			};
		}

		/// <summary>
		///  通过DB_Light来生成对应的LightAst
		/// </summary>
		/// <param name="light"></param>
		/// <returns></returns>
		public static LightAst GenerateLightAst(DB_Light light)
		{
			int endNum = light.StartID + light.Count - 1;
			string lightAddr = light.StartID + "-" + endNum;
			
			string path = @IniFileAst.GetSavePath(Application.StartupPath) + @"\LightLibrary\" + light.Name + @"\" + light.Type + ".ini";

			return new LightAst()
			{
				StartNum = light.StartID,
				EndNum = endNum,
				LightName = light.Name,
				LightType = light.Type,
				LightPic = light.Pic,
				Count = light.Count,
				LightAddr = lightAddr,
				LightPath = path
			};
		}

		/// <summary>
		///  重写（实际上并不算，因为形参不同）Equals语句，当地址和灯的全名一致时，基本可认为是同一个灯
		/// </summary>
		/// <param name="l2"></param>
		/// <returns></returns>
		public bool Equals(LightAst l2)
		{
			if (this.LightName == l2.LightName && this.LightType == l2.LightType
				&& this.LightAddr == l2.LightAddr)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 空构造函数
		/// </summary>
		public LightAst() { }

		/// <summary>
		/// 构造函数，为了用旧对象完全创造一个新的对象
		/// </summary>
		public LightAst(LightAst laOld) {
			LightPath = laOld.LightPath;
			LightName = laOld.LightName;
			LightType = laOld.LightType;
			LightPic = laOld.LightPic;
			Count = laOld.Count;
			LightAddr = laOld.LightAddr;
			StartNum = laOld.StartNum;
			EndNum = laOld.EndNum;
		}

		
	}
}
