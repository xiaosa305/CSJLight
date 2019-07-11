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
		///  在index处插入新的步，
		///  后面的步往后移动，
		///  CurrentStep+1 , TotalStep+1
		/// </summary>
		/// <param name="stepIndex"></param>
		/// <param name="stepWrapper"></param>
		public void InsertStep(int stepIndex, StepWrapper stepWrapper)
		{
			if (StepWrapperList == null)
			{
				StepWrapperList = new List<StepWrapper>();
			}
			StepWrapperList.Insert(stepIndex,stepWrapper);
			TotalStep = StepWrapperList.Count;
			CurrentStep = CurrentStep+1;
		}

		/// <summary>
		///  删除步
		///  1.判断步List不为空
		///  2.传进来的stepIndex不得大于步List
		///  3.实时生成TotalStep和CurrentStep
		/// </summary>
		/// <param name="stepWrapper"></param>
		public void DeleteStep(int stepIndex) {  
			if (StepWrapperList.Count > 0) {
				if (StepWrapperList.Count <= stepIndex) {
					throw new Exception("stepIndex有错");
				}
				else
				{
					StepWrapperList.RemoveAt(stepIndex);
					TotalStep = StepWrapperList.Count;
					// 根据TotalStep ，来生成CurrentStep的逻辑：
					// ①总步数为0，当前步数也得为0；
					// ②当总步数不为0时，当前步数不得大于总步数；
					// ③其他情况下，当前步数不发生变化
					if (TotalStep == 0)
					{
						CurrentStep = 0;
					}
					else {
						if (CurrentStep > TotalStep) {
							CurrentStep = TotalStep;
						}
					}
				}					
			}
			else
			{
				throw new Exception("当前步数为空，无法删除。");
			}	
		}

	
	}
}
