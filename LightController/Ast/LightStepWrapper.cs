using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
	/// <summary>
	///  这个类记载某一个light，在选定场景和模式下 -> 所有步数的集合, 以及当前步及最大步
	/// </summary>
	public class LightStepWrapper
	{
		public int CurrentStep { get; set; }
		public int TotalStep { get; set; } 
		public IList<StepWrapper> StepWrapperList { get; set; }

		/// <summary>
		/// 方法：在index处插入新的步，后面的步往后移动， CurrentStep = stepIndex+1 , TotalStep+1
		/// </summary>
		/// <param name="stepIndex"></param>
		/// <param name="newStep"></param>
		/// /// <param name="insertBefore">true为前插，false为后插</param>
		public void InsertStep(int stepIndex, StepWrapper newStep,bool insertBefore)
		{
			if (StepWrapperList == null)
			{
				StepWrapperList = new List<StepWrapper>();
			}
			// 前插时，currentStep不变，
			if (insertBefore) {				
				StepWrapperList.Insert(stepIndex, newStep);
				CurrentStep = stepIndex + 1;
			}
			// 后插时，currentStep 要+1；也就是stepIndex+2 
			else
			{
				StepWrapperList.Insert(stepIndex+1, newStep);
				CurrentStep = stepIndex + 2;
			}			
			// TotalStep统一结算
			TotalStep = StepWrapperList.Count;			
		}

		/// <summary>
		/// 方法：在最后面追加步：调用InsertStep方法
		/// </summary>
		/// <param name="newStep"></param>
		public void AddStep(StepWrapper newStep)
		{
			// 当前最大步为TotalStep，要追加数据到最后面，则要设index为totalStep，
			// 比如当前只有一步，要追加需要Insert(1,data,fasle)
			InsertStep(TotalStep-1, newStep,false);
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

		/// <summary>
		/// 辅助方法（静态）：通过传来的组长（或需被深复制的灯具）的数据，以及相关的StepTemplate的数据，来生成LightStepWrapper
		/// </summary>
		/// <param name="captainLSWrapper"></param>
		/// <param name="currentStepTemplate"></param>
		/// <returns></returns>
		public static LightStepWrapper GenerateLightStepWrapper(LightStepWrapper captainLSWrapper, StepWrapper currentStepTemplate , int mode)
		{
			if (captainLSWrapper == null || captainLSWrapper.StepWrapperList==null || captainLSWrapper.StepWrapperList.Count==0) {
				return null;
			}
			LightStepWrapper lsWrapper = new LightStepWrapper();
			foreach (StepWrapper  captainStepWrapper in captainLSWrapper.StepWrapperList)
			{
				StepWrapper newStep = StepWrapper.GenerateStepWrapper(currentStepTemplate, captainStepWrapper.TongdaoList, mode);
				lsWrapper.AddStep(newStep);
			}
			return lsWrapper;
		}	

	}
}
