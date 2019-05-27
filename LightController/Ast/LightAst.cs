using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController
{
	public class LightAst
	{
		public String LightPath { get; set; }
		public string LightName { get; set; }
		public string LightType { get; set; }
		public string LightAddr { get; set; }
		public string LightPic { get; set; }

		public static void AddLightListView(LightAst la) {




		}

		public override string ToString()
		{
			return LightName + ":" + LightType + ":" + LightAddr + ":" + LightPic;
		}
	}
}
