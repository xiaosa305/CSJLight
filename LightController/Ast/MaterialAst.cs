using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LightController.Common;

namespace LightController.Ast
{
	public class MaterialAst
	{
		public int Step { set; get; }
		public int TongdaoCount { set; get; }
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
			int step = iniFileAst.ReadInt("Set", "step", 0);
			int tongdaoCount = iniFileAst.ReadInt("Set", "tongdaoCount", 0);
			if (step == 0 || tongdaoCount == 0) {
				return null;
			}
			IList<string> tdNameList = new List<string>();
			for (int tdIndex=0; tdIndex < tongdaoCount; tdIndex++)
			{				
				tdNameList.Add(iniFileAst.ReadString("TD",  tdIndex.ToString(), "无名通道") );
			}
			// 3.给各tongdaoList赋值
			TongdaoWrapper[,]  tongdaoList = new TongdaoWrapper[step, tongdaoCount];
			for (int stepIndex = 0; stepIndex < step; stepIndex++)
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
				Step = step,
				TongdaoCount = tongdaoCount,
				TdNameList = tdNameList,
				TongdaoList = tongdaoList
			};
		}

	}
}
