using DMX512;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	}
}
