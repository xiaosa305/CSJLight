using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController
{
	public class LightAst
	{
		public String LightPath { get; set; }
		
		// 此三个属性，可由ini文件获取
		public string LightName { get; set; }
		public string LightType { get; set; }
		public string LightPic { get; set; }

		// 此三个属性，每个灯都不一样，在灯库编辑时添加
		public string LightAddr { get; set; }
		public int startNum { get; set; }
		public int endNum { get; set; }
		
	
		public override string ToString()
		{
			return LightName + ":" + LightType + ":" + LightAddr + ":" + LightPic;
		}

		public static void AddLightListView(LightAst la)
		{


		}

	}
}
