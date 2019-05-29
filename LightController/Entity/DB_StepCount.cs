using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMX512
{
    public class DB_StepCount
    {
        public int LightIndex { get; set; }
        public int Frame { get; set; }
        public int Mode { get; set; }
        public int StepCount { get; set; }

		public override string ToString()
		{
			return "LightIndex:" + LightIndex + "\n"
				+ "Frame:" + Frame + "\n"
				+ "Mode:" + Mode + "\n"
				+ "StepCount:" + StepCount + "\n";
		}	
	}
}
