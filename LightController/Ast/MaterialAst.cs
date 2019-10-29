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
		public int TongdaoCount { set; get; }
		/// <summary>
		/// 左步数，右通道数
		/// </summary>
		public IList<string> TdNameList { set; get; }
		public TongdaoWrapper[,] TongdaoList { set; get; }

		/// <summary>
		/// 辅助方法：静态方法，直接通过传入的配置文件路径，生成对应的MaterialAst
		/// </summary>
		/// <param name="materialPath"></param>
		/// <returns></returns>
		public static MaterialAst GenerateMaterialAst(string materialPath) {
			// 1.初始化IniFileAst
			IniFileAst iniFileAst = new IniFileAst(materialPath);

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
				TongdaoCount = tongdaoCount,
				TdNameList = tdNameList,
				TongdaoList = tongdaoList
			};
		}

	}
}
