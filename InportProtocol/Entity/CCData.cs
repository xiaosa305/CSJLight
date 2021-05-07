using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImportProtocol.Entity
{
	[Serializable]
	public class CCData
	{
		public virtual CCData_PK PK { get; set; }

		public virtual string Function { get; set; }		
		public virtual string Com0Up { get; set; }
		public virtual string Com0Down { get; set; }
		public virtual string Com1Up { get; set; }
		public virtual string Com1Down { get; set; }
		public virtual string InfraredSend { get; set; }
		public virtual string InfraredReceive { get; set; }
		public virtual string PS2Up { get; set; }
		public virtual string PS2Down { get; set; }
	}
}
