using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
	/// <summary>
	///  这个类记载某一个light，在选定场景和模式下，所有步数的集合,以前该步数的相关信息
	/// </summary>
	public class LightStepWrapper
	{
		public int RecentStep { get; set; }
		public int TotalStep { get; set; } 
		public List<StepWrapper> StepWrapperList { get; set; }

		// 将验证步骤放在这里
		public void AddStep(StepWrapper stepWrapper) {
			StepWrapperList.Add(stepWrapper);
			RecentStep ++;
			TotalStep++; 
		}

	}
}
