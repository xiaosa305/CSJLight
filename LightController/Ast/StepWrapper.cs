using DMX512;
using LightController.MyForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static LightController.MyForm.MainFormBase;

namespace LightController.Ast
{
	/// <summary>
	///  这个类记载某一个light，在选定场景和模式下，其中的一个步数
	/// </summary>
	public class StepWrapper
	{
		// 这两项项为复制灯数据时使用：
		// 不同的灯具全名必然不同(厂商名+型号）；
		// lightMode则用以区分常规场景和声控场景-->常规场景无法复制到声控场景中，反之亦然
		public int LightMode { get; set; }
		public string LightFullName { get; set; }
		public int StartNum { get; set; }		
		
		//MARK：StepWrapper.Class 新增的Flag字段：用以记录该StepWrapper状态（因为单个页面只能显示一个StepWrapper(哪怕进入多灯时，也可以用此字段控制））
		public int Flag { get; set; }

		// 这个列表记录通道数据
		public IList<TongdaoWrapper> TongdaoList { get; set; }

		/// <summary>
		///  辅助方法： 由 步数模板 和 步数值集合 , 来生成某一步的StepWrapper;
		///  主要供从数据库里读取数据填入内存时使用
		///  复制灯时也有可能用到这个方法
		/// </summary>
		/// <param name="stepTemplate">模板Step</param>
		/// <param name="stepValueList">从数据库读取的相同lightIndex、frame、mode、step的数值集合：即某一步的通道值列表</param>
		/// <returns></returns>
		public static StepWrapper GenerateStepWrapper(StepWrapper stepTemplate, IList<DB_Value> stepValueList, int mode)
		{
			List<TongdaoWrapper> tongdaoList = new List<TongdaoWrapper>();
			for (int tdIndex = 0; tdIndex < stepValueList.Count; tdIndex++)
			{
				DB_Value value = stepValueList[tdIndex];
				TongdaoWrapper td = new TongdaoWrapper()
				{
					TongdaoName = stepTemplate.TongdaoList[tdIndex].TongdaoName,
					Address = stepTemplate.TongdaoList[tdIndex].Address,
					StepTime = value.StepTime,
					ChangeMode = value.ChangeMode,
					ScrollValue = value.ScrollValue,
					//TODO：20.03.25 以模板数据生成备注
					Remark = stepTemplate.TongdaoList[tdIndex].Remark
				};
				tongdaoList.Add(td);
			}
			return new StepWrapper()
			{
				TongdaoList = tongdaoList,
				LightMode = mode,
				LightFullName = stepTemplate.LightFullName,
				StartNum = stepTemplate.StartNum
			};
		}

		/// <summary>
		///  辅助方法： 由 步数模板 和 TongdaoList集合 , 来生成某一步的StepWrapper;
		///  复制灯时将用到这个方法
		/// </summary>
		/// <param name="stepTemplate">模板Step</param>
		/// <param name="stepValueList">从数据库读取的相同lightIndex、frame、mode、step的数值集合：即某一步的通道值列表</param>
		/// <returns></returns>
		public static StepWrapper GenerateStepWrapper(StepWrapper stepTemplate, IList<TongdaoWrapper> tempTongdaoList, int mode)
		{
			List<TongdaoWrapper> tongdaoList = new List<TongdaoWrapper>();
			for (int tdIndex = 0; tdIndex < tempTongdaoList.Count; tdIndex++)
			{
				TongdaoWrapper td = new TongdaoWrapper()
				{
					TongdaoName = stepTemplate.TongdaoList[tdIndex].TongdaoName,
					Address = stepTemplate.TongdaoList[tdIndex].Address,
					StepTime = tempTongdaoList[tdIndex].StepTime,
					ChangeMode = tempTongdaoList[tdIndex].ChangeMode,
					ScrollValue = tempTongdaoList[tdIndex].ScrollValue
				};
				tongdaoList.Add(td);
			}
			return new StepWrapper()
			{
				TongdaoList = tongdaoList,
				LightMode = mode,
				LightFullName = stepTemplate.LightFullName,
				StartNum = stepTemplate.StartNum
			};
		}
		
		/// <summary>
		///  辅助方法	:通过stepMode和mode，来生成新的stepWrapper
		///  （包括mode,lightName,startNum,tongdaoList等属性）;
		///  主要供新建步、插入素材 等情况使用
		/// </summary>
		/// <param name="stepTemplate"></param>
		/// <returns></returns>
		public static StepWrapper GenerateNewStep(StepWrapper stepTemplate, int mode)
		{
			if (stepTemplate == null) {
				return null;
			}
			return new StepWrapper()
			{
				TongdaoList = TongdaoWrapper.GenerateTongdaoList(stepTemplate.TongdaoList, mode),
				LightMode = mode,
				LightFullName = stepTemplate.LightFullName,
				StartNum = stepTemplate.StartNum,
				//UNDONE：此处添加Flag，所有新加的步，Flag=0（ )
				Flag = 0  
			};
		}

		/// <summary>
		///  辅助方法	:通过stepMode和mode，来生成新的stepWrapper
		///  （包括mode,lightName,startNum,tongdaoList等属性）;
		///  主要供新建步、插入素材 等情况使用
		/// </summary>
		/// <param name="stepTemplate"></param>
		/// <returns></returns>
		public static StepWrapper GenerateNewStep(StepWrapper stepTemplate, StepWrapper mainStepTemplate, int mode)
		{
			if (stepTemplate == null || mainStepTemplate==null)
			{
				return null;
			}
			return new StepWrapper()
			{
				TongdaoList = TongdaoWrapper.GenerateTongdaoList(stepTemplate.TongdaoList, mode),
				LightMode = mode,
				LightFullName = stepTemplate.LightFullName,
				StartNum = stepTemplate.StartNum
			};
		}

		/// <summary>
		///  辅助方法：改变这其中tongdaoList相应的值
		/// </summary>
		/// <param name="where"></param>
		/// <param name="tdIndexList"></param>
		/// <param name="commonValue"></param>
		public void MultiChangeValue(WHERE where, IList<int> tdIndexList, int commonValue) {
			foreach (int tdIndex in tdIndexList)
			{
				switch (where) {
					case WHERE.SCROLL_VALUE:   TongdaoList[tdIndex].ScrollValue = commonValue;  break;
					case WHERE.CHANGE_MODE:  TongdaoList[tdIndex].ChangeMode = commonValue; break;
					case WHERE.STEP_TIME:			TongdaoList[tdIndex].StepTime = commonValue ;	break;
				}
			}
		}
	}
}
