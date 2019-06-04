using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
	public class LightData
	{
		// stepMode:就是每一步的模型。每次新建StepList的数据时，先采用这个模型。
		public StepAst stepMode { get; set; }
		public StepAst[,] stepList { get; set; }

		public LightData()
		{
			stepList = new StepAst[24, 2];
		}
		public LightData(StepAst[,] stepList) {
			this.stepList = stepList; 
		}
	}
}
