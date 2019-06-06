using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{

	/// <summary>
	///  这个类：某一个加载进来的灯具的全部数据：包括各个模式和场景的组合下，所有步数的集合
	/// </summary>
	public class LightWrapper
	{
		// stepMode:就是每一步的模型。每次新建StepList的数据时，先采用这个模型。
		public StepWrapper StepMode { get; set; }
		// StepAstList类:是某一个场景模式（ＦＭ）下步数的集合;　
		// StepList 数组:所有FM的StepAstList的集合
		public LightStepWrapper[,] LightStepWrapperList { get; set; }

		// 初始化时，就定义这些集合的数量
		public LightWrapper()
		{
			LightStepWrapperList = new LightStepWrapper[24, 2];
		}

		//public LightData(StepAstList[,] stepList) {
		//	this.StepList = stepList; 
		//}
	}
}
