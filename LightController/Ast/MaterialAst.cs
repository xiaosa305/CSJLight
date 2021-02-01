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
		/// 9.23 Mode为添加的辅助项，主要供《复制、粘贴多步》使用
		/// </summary>
		public int Mode { get; set; }
		public int StepCount { set; get; }
		public IList<string> TdNameList { set; get; }
		/// <summary>
		/// 左步数，右通道数
		/// </summary>
		public TongdaoWrapper[,] TongdaoArray { set; get; }

		/// <summary>
		/// 辅助方法：直接通过传入的配置文件路径，生成对应的MaterialAst
		/// </summary>
		/// <param name="materialPath"></param>
		/// <returns></returns>
		public static MaterialAst GenerateMaterialAst(int mode, string materialPath) {
			// 1.初始化IniFileAst
			IniFileHelper iniFileAst = new IniFileHelper(materialPath);

			// 2.取[Set]和[TD]的值
			int stepCount = iniFileAst.ReadInt("Set", "step", 0);
			int tongdaoCount = iniFileAst.ReadInt("Set", "tongdaoCount", 0);
			if (stepCount == 0 || tongdaoCount == 0) {
				return null;
			}
			IList<string> tdNameList = new List<string>();
			for (int tdIndex = 0; tdIndex < tongdaoCount; tdIndex++)
			{
				tdNameList.Add(iniFileAst.ReadString("TD", tdIndex.ToString(), "无名通道"));
			}
			// 3.给各tongdaoList赋值
			TongdaoWrapper[,] tongdaoArray = new TongdaoWrapper[stepCount, tongdaoCount];
			for (int stepIndex = 0; stepIndex < stepCount; stepIndex++)
			{
				for (int tongdaoIndex = 0; tongdaoIndex < tongdaoCount; tongdaoIndex++)
				{
					tongdaoArray[stepIndex, tongdaoIndex] = new TongdaoWrapper
					{
						TongdaoName = tdNameList[tongdaoIndex],
						ScrollValue = iniFileAst.ReadInt("Data", stepIndex + "_" + tongdaoIndex + "_V", 0),
						ChangeMode = iniFileAst.ReadInt("Data", stepIndex + "_" + tongdaoIndex + "_CM", 0),
						StepTime = iniFileAst.ReadInt("Data", stepIndex + "_" + tongdaoIndex + "_ST", 0)
					};
				}
			}

			return new MaterialAst() {
				Mode = mode,
				StepCount = stepCount,
				TdNameList = tdNameList,
				TongdaoArray = tongdaoArray
			};
		}

		/// <summary>
		/// 辅助方法：通过传入的Dictionary，生成相应的MaterialAst
		/// </summary>
		/// <param name="tdDict"></param>
		/// <returns></returns>
		public static MaterialAst GenerateMaterialAst(int mode, Dictionary<string, int> tdDict)
		{
			// 当tdDict为空时，返回null
			if (tdDict == null || tdDict.Count == 0){
				return null;
			}

			IList<string> tdNameList = tdDict.Keys.ToList(); 
			TongdaoWrapper[,] tongdaoArray = new TongdaoWrapper[ 1 ,tdDict.Count];

			for(int tdIndex=0; tdIndex<tdNameList.Count; tdIndex++)
			{
				string tdName = tdNameList[tdIndex];
				tongdaoArray[0, tdIndex] = new TongdaoWrapper(tdName, tdDict[tdName], 50,1);	
			}

			return new MaterialAst()
			{
				Mode = mode,
				StepCount = 1,
				TdNameList = tdNameList,
				TongdaoArray = tongdaoArray
			};
		}

		/// <summary>
		/// 辅助方法：通过传入的tdDict，加工旧的material，并返回新material
		/// </summary>
		/// <param name="materialPath"></param>
		/// <returns></returns>
		public static MaterialAst ProcessMaterialAst( MaterialAst ma1 ,	Dictionary<string,int> tdDict) {
			// 若tdDict没有数据，则直接返回ma1
			if (tdDict == null || tdDict.Count==0) {
				return ma1;
			}

			//DOTO  ProcessMaterialAst
			// 若ma1为空，则可以为tdDict生成合适materialAst(单步)
			if (ma1 == null) {
				return null; 
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
						ScrollValue = tongdaoIndex < ma1.TdNameList.Count ? ma1.TongdaoArray[stepIndex, tongdaoIndex].ScrollValue : tdDict[ma2TdNameList[tongdaoIndex]],
						ChangeMode = tongdaoIndex < ma1.TdNameList.Count ? ma1.TongdaoArray[stepIndex, tongdaoIndex].ChangeMode : 1 ,//不论哪种模式，默认都是1（渐变、跳变）
						StepTime = tongdaoIndex < ma1.TdNameList.Count ? ma1.TongdaoArray[stepIndex, tongdaoIndex].StepTime : 50 // 默认0.04*50 = 2S
					};
				}
			}

			MaterialAst ma2 = new MaterialAst
			{
				Mode = ma1.Mode,
				StepCount = ma1.StepCount,
				TdNameList = ma2TdNameList,
				TongdaoArray = ma2TongdaoList
			};

			return ma2;
		}

		/// <summary>
		/// 辅助方法：把新素材插到旧的素材中，前者优先级更高（即如果存在同名通道，则保留前者的数据） 
		/// </summary>
		/// <param name="complexMaterial"></param>
		/// <param name="newMaterial"></param>
		/// <returns></returns>
		public static MaterialAst ComplexMaterialAst(MaterialAst complexMaterial, MaterialAst newMaterial)
		{
			if (complexMaterial == null) { return newMaterial; }
			if (newMaterial == null) { return complexMaterial; }

			//DOTO MaterialAst.ComplexMaterialAst() 方法块核心代码

			// 先找出后者和前者不同的通道
			Dictionary<string,int> addTdDict = new Dictionary<string,int>();
			List<string> newTdNameList = new List<string>(complexMaterial.TdNameList);			

			for (int nIndex = 0; nIndex < newMaterial.TdNameList.Count; nIndex++) {
				if ( ! complexMaterial.TdNameList.Contains(newMaterial.TdNameList[nIndex])) {
					string tdName = newMaterial.TdNameList[nIndex];
					addTdDict.Add(tdName,nIndex);
					newTdNameList.Add(tdName);
				}
			}

			// 若添加的通道数为0，则直接返回complexMaterial;
			if (addTdDict.Count == 0) {
				return complexMaterial;
			}
			else {
				int newStepCount = complexMaterial.StepCount >= newMaterial.StepCount ? complexMaterial.StepCount : newMaterial.StepCount ;
				TongdaoWrapper[,] tdArray = new TongdaoWrapper[newStepCount , newTdNameList.Count];

				for (int stepIndex = 0; stepIndex < newStepCount; stepIndex++)
				{
					for (int tdIndex = 0;  tdIndex < newTdNameList.Count; tdIndex++)
					{
						if (tdIndex < complexMaterial.TdNameList.Count )
						{
							tdArray[stepIndex, tdIndex] = TongdaoWrapper.GetTongdaoFromArray(complexMaterial.TongdaoArray, stepIndex, tdIndex, complexMaterial.Mode);
						}
						else {
							tdArray[stepIndex, tdIndex] = TongdaoWrapper.GetTongdaoFromArray( newMaterial.TongdaoArray, stepIndex , addTdDict[newTdNameList[tdIndex]], newMaterial.Mode);
						}
					}
				}

				return new MaterialAst()
				{
					Mode = complexMaterial.Mode,
					TdNameList = newTdNameList,
					StepCount = newStepCount,
					TongdaoArray = tdArray
				};
			}			
		}
				
	}
}
 