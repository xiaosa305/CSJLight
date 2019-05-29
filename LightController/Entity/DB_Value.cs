using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMX512
{
    public class DB_Value
    {
		public int LightIndex { get; set; }
        public int Frame { get; set; }
        public int Step { get; set; }
        public int Mode { get; set; }
        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int Value3 { get; set; }
        public int LightID { get ; set; }

		public override string ToString()
		{
			return "LightIndex:" + LightIndex + "\n"
				+ "Frame:" + Frame + "\n"
				+ "Step:" + Step + "\n"
				+ "Mode:" + Mode + "\n"
				+ "Value1:" + Value1 + "\n"
				+ "Value2:" + Value2 + "\n"
				+ "Value3:" + Value3 + "\n"
				+ "LightID:" + LightID + "\n";

		}
	}
}
