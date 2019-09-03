using DMX512;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LighEditor.Ast
{
	public class DBWrapper
	{
		public IList<DB_Light> lightList { get; set; }
		public IList<DB_StepCount> stepCountList { get; set; }
		public IList<DB_Value> valueList{ get; set; }
		public IList<DB_FineTune> fineTuneList { get; set; }

		public DBWrapper(IList<DB_Light> lightList, IList<DB_StepCount> stepCountList, IList<DB_Value> valueList, IList<DB_FineTune> fineTuneList)
		{
				this.lightList =lightList;
				this.stepCountList =stepCountList;
				this.valueList = valueList;
				this.fineTuneList = fineTuneList;
		}
	}
}
