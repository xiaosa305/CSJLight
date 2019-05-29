using DMX512;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
	public class DBWrapper
	{
		public DBWrapper(List<DB_Light> lightList, List<DB_StepCount> stepCountList, List<DB_Value> valueList)
		{
			LightList = lightList;
			StepCountList = stepCountList;
			ValueList = valueList;
		}

		public List<DB_Light> LightList { get; set; }
		public List<DB_StepCount> StepCountList { get; set; }
		public List<DB_Value> ValueList { get; set; }
	}
}
