using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMX512
{

	public class DB_StepCount
	{
		public virtual DB_StepCountPK PK { get; set; }
        public virtual int StepCount { get; set; }

		public override string ToString()
		{
			return PK + " -- " + StepCount; 
		}
	}
}
