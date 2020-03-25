using LightController.MyForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{

	/// <summary>
	///  这个类：某一个加载进来的灯具的全部数据：包括1.各个模式和场景的组合下，所有步数的集合 2.从ini读取数据生成的模板
	/// </summary>
	public class LightWrapper
	{
		/// <summary>
		/// 每一步的模型。每次新建StepList的数据时，先采用这个模型。
		/// </summary>
		public StepWrapper StepTemplate { get; set; }

		// StepAstList类:是某一个场景模式（FＭ）下步数的集合;　
		// StepList 数组:所有FM的StepAstList的集合
		public LightStepWrapper[,] LightStepWrapperList { get; set; }

		/// <summary>
		///  初始化时，就定义这些集合的数量
		/// </summary>
		public LightWrapper()
		{
			LightStepWrapperList = new LightStepWrapper[MainFormBase.FrameCount , 2];
		}

		/// <summary>
		///  辅助方法：用两个Light的值，来生成一个新的LightWrapper。
		/// </summary>
		/// <param name="tempLight"></param>
		/// <returns></returns>
		internal static LightWrapper CopyLight(LightWrapper tempLight, LightWrapper selectedLight)
		{
			LightWrapper newLight = new LightWrapper();
			newLight.StepTemplate = selectedLight.StepTemplate;
			for (int frame = 0; frame < MainFormBase.FrameCount; frame++) {
				for (int mode = 0; mode < 2; mode++)
				{
					if (tempLight.LightStepWrapperList[frame, mode] != null) {
						newLight.LightStepWrapperList[frame, mode] = new LightStepWrapper() {
							CurrentStep = 0,
							TotalStep = 0,
							StepWrapperList = new List<StepWrapper>()
						};
											
						for (int currentStep = 0; currentStep < tempLight.LightStepWrapperList[frame, mode].TotalStep; currentStep++)
						{
							IList<TongdaoWrapper> tempTongdaoList = tempLight.LightStepWrapperList[frame, mode].StepWrapperList[currentStep].TongdaoList;
							StepWrapper newStep = StepWrapper.GenerateStepWrapper(newLight.StepTemplate,tempTongdaoList , mode);
							newLight.LightStepWrapperList[frame, mode].AddStep(newStep);
						}
					}
				}
			}
			return newLight;
		}	
	}
}