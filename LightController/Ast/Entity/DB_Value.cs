using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMX512
{
    public class DB_Value
    {
		public virtual DB_ValuePK PK { get; set; }
		public virtual int ScrollValue { get; set; }
        public virtual int StepTime { get; set; }
        public virtual int ChangeMode { get; set; }
			   

		// Dickov ： 最初的重写ToString()，暂时保留，但最后应该用不到
		//public override string ToString()
		//{
		//	return "LightIndex:" + LightIndex + "\n"
		//		+ "Frame:" + Frame + "\n"
		//		+ "Step:" + Step + "\n"
		//		+ "Mode:" + Mode + "\n"
		//		+ "Value1:" + Value1 + "\n"
		//		+ "Value2:" + Value2 + "\n"
		//		+ "Value3:" + Value3 + "\n"
		//		+ "LightID:" + LightID + "\n";

		//}
	}
}
