using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Entity
{
	[Serializable]
	public class CCData
	{
		public string Function { get; set; }
		public string Code { get; set; }
		public string Com0Up { get; set; }
		public string Com0Down { get; set; }
		public string Com1Up { get; set; }
		public string Com1Down { get; set; }
		public string InfraredSend { get; set; }
		public string InfraredReceive { get; set; }
		public string PS2Up { get; set; }
		public string PS2Down { get; set; }
	}
}
