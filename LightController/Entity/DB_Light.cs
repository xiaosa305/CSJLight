using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMX512
{
    public class DB_Light
    {
		public virtual int LightNo { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string Pic { get; set; }
        public virtual int StartID { get; set; }
        public virtual int Count { get; set; }

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
