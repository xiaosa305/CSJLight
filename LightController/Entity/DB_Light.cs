using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMX512
{
    public class DB_Light
    {
        public int LightNo { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Pic { get; set; }
        public int StartID { get; set; }
        public int Count { get; set; }

		public override string ToString()
		{
			return "LightNo:" + LightNo + "\n"
				+ "Name:" + Name + "\n"
				+ "Type:" + Type + "\n"
				+ "Pic:" + Pic + "\n"
				+ "Count:" + Count;

		}
	}
}
