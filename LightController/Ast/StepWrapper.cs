using DMX512;
using LightController.Common;
using LightController.EntityNew;
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
			// 此处需要多一重验证， 若传进来的valueList的数量与stepTemplate的tongdaoList数量不合，则直接返回null，不再往下走
			if (stepTemplate.TongdaoList.Count != stepValueList.Count) {
				return null;
			}

			List<TongdaoWrapper> tongdaoList = new List<TongdaoWrapper>();
			for (int tdIndex = 0; tdIndex < stepValueList.Count; tdIndex++)
			{
				DB_Value value = stepValueList[tdIndex];
				TongdaoWrapper td = new TongdaoWrapper()
				{
					TongdaoName = stepTemplate.TongdaoList[tdIndex].TongdaoName,
					Address = stepTemplate.TongdaoList[tdIndex].Address,					
					// 添加处理，若是数据库内步时间大于上限，则设为上限值
					StepTime = value.StepTime>MAX_StTimes ? MAX_StTimes : value.StepTime,
					ChangeMode = value.ChangeMode,
					ScrollValue = value.ScrollValue,
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
		/// 辅助方法：(读取) 由 步数模板 和 channelList，为指定SMlightStepWrapper
		/// </summary>
		/// <param name=lsWrapper" 指定S/M的lightStepWrapper对象，要调用其自带的AddStep来添加步
		/// <param name="stepTemplate"></param>
		/// <param name="channelList"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public static void GenerateStepWrapperList(  
			LightStepWrapper lsWrapper,
			StepWrapper stepTemplate, 
			IList<DB_Channel> channelList, 
			int mode)
		{
			List<string[]> strArrayList = new List<string[]>();
			for (int chanIndex = 0; chanIndex < channelList.Count; chanIndex++)
			{
				strArrayList.Add(channelList[chanIndex].Value.Split(',') );
			}
			
			for ( int step = 0; step < channelList[0].Value.Split(',').Length; step ++ )
            {
				StepWrapper newStep = GenerateNewStep(stepTemplate , mode);			
				for (int chanIndex = 0; chanIndex < channelList.Count; chanIndex++)
				{
					string[] valueArray = strArrayList[chanIndex][step].Split('-');
					newStep.TongdaoList[chanIndex].ChangeMode = int.Parse( valueArray[0] );
					newStep.TongdaoList[chanIndex].ScrollValue = int.Parse(valueArray[1] );
					newStep.TongdaoList[chanIndex].StepTime = int.Parse( valueArray[2] );
				}
				lsWrapper.AddStep(newStep);
			}			
		}

		/// <summary>
		///  辅助方法	:通过stepTemplate和mode，来生成新的stepWrapper
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

		/// <summary>
		/// 辅助方法：检测当前灯具是否存在X、Y轴；存在则返回true
		/// </summary>
		/// <returns></returns>
		public static bool CheckXY(StepWrapper stepWrapper)
		{
			bool existX = false;
			bool existY = false;
			IList<TongdaoWrapper> tongdaoList = stepWrapper.TongdaoList;
			foreach (TongdaoWrapper td in tongdaoList)
			{
				if (td.TongdaoName == LanguageHelper.TranslateWord("X轴"))
				{
					existX = true;
				}
				else if (td.TongdaoName == LanguageHelper.TranslateWord("Y轴"))
				{
					existY = true;
				}
			}
			return existX && existY;					
		}

		/// <summary>
		/// 辅助方法：检测当前灯具是否存在RGB及总调光；存在则返回true
		/// </summary>
		/// <returns></returns>
		public static bool CheckRGB(StepWrapper stepWrapper)
		{
			HashSet<string> existSet = new HashSet<string>();
			string[] rgbStrList = {
					LanguageHelper.TranslateWord("总调光"),
					LanguageHelper.TranslateWord("红"),
					LanguageHelper.TranslateWord("绿"),
					LanguageHelper.TranslateWord("蓝")
			};
			IList<TongdaoWrapper> tongdaoList = stepWrapper.TongdaoList;
			foreach (TongdaoWrapper td in tongdaoList)
			{
				if (rgbStrList.Contains(td.TongdaoName))
				{
					existSet.Add(td.TongdaoName);
				}
			}
			return existSet.Count == 4;						
		}

      
    }
}
