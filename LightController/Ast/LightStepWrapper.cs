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
		public int CurrentStep { get; set; }
		public int TotalStep { get; set; } 
		public List<StepWrapper> StepWrapperList { get; set; }

		/// <summary>
		/// 添加步的行为放在这里:总数加1，而CurrentStep切换到最后一步
		/// </summary>
		/// <param name="stepWrapper"></param>
		public void AddStep(StepWrapper stepWrapper) {
			if (StepWrapperList == null) {
				StepWrapperList = new List<StepWrapper>();
			}
			StepWrapperList.Add(stepWrapper);
			TotalStep = StepWrapperList.Count; 			
			CurrentStep = TotalStep;
		}

		/// <summary>
		///  删除步
		/// </summary>
		/// <param name="stepWrapper"></param>
		public void DeleteStep(int stepIndex) {  

			if (StepWrapperList.Count > 1) {
				StepWrapperList.RemoveAt(stepIndex);
				TotalStep = StepWrapperList.Count;
				if (CurrentStep > 1)
				{
					CurrentStep = CurrentStep - 1;
				}
			}
			else
			{
				throw new Exception("最少需要一步");
			}	
		}		
	}
}
