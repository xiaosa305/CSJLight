using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LightController.Common;

namespace LightController.Ast
{
	public class MaterialAst
	{
		/// <summary>
		/// 9.23 添加的辅助项，主要供《复制、粘贴多步》使用
		/// </summary>
		public int Mode { get; set; }
		public int StepCount { set; get; }
		public IList<string> TdNameList { set; get; }
		/// <summary>
		/// 左步数，右通道数
		/// </summary>
		public TongdaoWrapper[,] TongdaoList { set; get; }

		/// <summary>
		/// 辅助方法：静态方法，直接通过传入的配置文件路径，生成对应的MaterialAst
		/// </summary>
		/// <param name="materialPath"></param>
		/// <returns></returns>
		public static MaterialAst GenerateMaterialAst(string materialPath) {
			// 1.初始化IniFileAst
			IniFileHelper iniFileAst = new IniFileHelper(materialPath);

			// 2.取[Set]和[TD]的值
			int stepCount = iniFileAst.ReadInt("Set", "step", 0);
			int tongdaoCount = iniFileAst.ReadInt("Set", "tongdaoCount", 0);
			if (stepCount == 0 || tongdaoCount == 0) {
				return null;
			}
			IList<string> tdNameList = new List<string>();
			for (int tdIndex=0; tdIndex < tongdaoCount; tdIndex++)
			{				
				tdNameList.Add(iniFileAst.ReadString("TD",  tdIndex.ToString(), "无名通道") );
			}
			// 3.给各tongdaoList赋值
			TongdaoWrapper[,]  tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];
			for (int stepIndex = 0; stepIndex < stepCount; stepIndex++)
			{
				for (int tongdaoIndex = 0; tongdaoIndex < tongdaoCount; tongdaoIndex++)
				{
					tongdaoList[stepIndex, tongdaoIndex] = new TongdaoWrapper
					{
						TongdaoName = tdNameList[tongdaoIndex],
						ScrollValue = iniFileAst.ReadInt("Data", stepIndex + "_" + tongdaoIndex + "_V", 0),
						ChangeMode = iniFileAst.ReadInt("Data", stepIndex + "_" + tongdaoIndex + "_CM", 0),
						StepTime = iniFileAst.ReadInt("Data", stepIndex + "_" + tongdaoIndex + "_ST", 0)
					};
				}
			}

			return new MaterialAst() {
				StepCount = stepCount,
				TdNameList = tdNameList,
				TongdaoList = tongdaoList
			};
		}

		/// <summary>
		/// 辅助方法：
		/// </summary>
		/// <param name="materialPath"></param>
		/// <returns></returns>
		public static MaterialAst ProcessMaterialAst( MaterialAst ma1 ,	Dictionary<string,int> tdDict) {
			// 若tdDict没有数据，则直接返回ma1
			if (tdDict == null || tdDict.Count==0) {
				return ma1;
			}

			IList<string> ma2TdNameList = new List<string>(ma1.TdNameList);
			foreach (string tdName in tdDict.Keys)
			{
				if (!ma1.TdNameList.Contains(tdName))
				{
					ma2TdNameList.Add(tdName);
				}
			}
			//若没有多出的通道，则直接返回ma1
			if (ma2TdNameList.Count == ma1.TdNameList.Count) {
				return ma1;
			}

			TongdaoWrapper[,] ma2TongdaoList = new TongdaoWrapper[ ma1.StepCount , ma2TdNameList.Count ] ;
			for(int stepIndex=0; stepIndex< ma1.StepCount; stepIndex++)
			{
				for (int tongdaoIndex = 0; tongdaoIndex < ma2TdNameList.Count ; tongdaoIndex++)
				{
					ma2TongdaoList[stepIndex, tongdaoIndex] = new TongdaoWrapper
					{
						TongdaoName = ma2TdNameList[tongdaoIndex],
						ScrollValue = tongdaoIndex < ma1.TdNameList.Count ? ma1.TongdaoList[stepIndex, tongdaoIndex].ScrollValue : tdDict[ma2TdNameList[tongdaoIndex]],
						ChangeMode = tongdaoIndex < ma1.TdNameList.Count ? ma1.TongdaoList[stepIndex, tongdaoIndex].ChangeMode : 1 ,//不论哪种模式，默认都是1（渐变、跳变）
						StepTime = tongdaoIndex < ma1.TdNameList.Count ? ma1.TongdaoList[stepIndex, tongdaoIndex].StepTime : 50 // 默认0.04*50 = 2S
					};
				}
			}

			MaterialAst ma2 = new MaterialAst
			{
				Mode = ma1.Mode,
				StepCount = ma1.StepCount,
				TdNameList = ma2TdNameList,
				TongdaoList = ma2TongdaoList
			};

			return ma2;
		}




	}
}
 