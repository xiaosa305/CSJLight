using LightController.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightEditor.Ast
{
	public class SAWrapper
	{
		public IList<SA> SaList{ get; set; }

		public SAWrapper()
		{
			SaList = new List<SA>();
		}

		/// <summary>
		/// 静态辅助方法：深度复制sawArray，《LightEditorForm》及《WaySetForm》皆会用到
		/// </summary>
		/// <param name="sourceSawArray"></param>
		/// <returns></returns>
		public static SAWrapper[] DeepCopy(SAWrapper[] sourceSawArray) {

			// 先验证是否为null
			if (sourceSawArray == null) {
				return null;
			}
			// 只能一一拷贝，才能实现真正的深拷贝（因为数组sawArray内的变量，是列表IList，其值也是引用传递）
			SAWrapper[] destSawArray = new SAWrapper[sourceSawArray.Length];
			for (int tdIndex = 0; tdIndex < sourceSawArray.Length; tdIndex++)
			{
				destSawArray[tdIndex] = new SAWrapper();

				for (int saIndex2 = 0; saIndex2 < sourceSawArray[tdIndex].SaList.Count; saIndex2++)
				{
					SA sa = new SA
					{
						SAName = sourceSawArray[tdIndex].SaList[saIndex2].SAName,
						StartValue = sourceSawArray[tdIndex].SaList[saIndex2].StartValue,
						EndValue = sourceSawArray[tdIndex].SaList[saIndex2].EndValue
					};
					destSawArray[tdIndex].SaList.Add(sa);
				}
			}
			return destSawArray;
		}

		/// <summary>
		/// 静态辅助方法：由ini，生成相应的SawArray
		/// </summary>
		/// <returns></returns>
		public static SAWrapper[] GetSawArrayFromIni(string iniPath) {

			if (!String.IsNullOrEmpty(iniPath))
			{
				IniFileHelper iniAst = new IniFileHelper(iniPath);
				int tongdaoCount = iniAst.ReadInt("set", "count", 0);
                if (tongdaoCount == 0) {
                    throw new Exception("打开灯库文件失败！请确认该灯库文件的保存格式为UTF-8，且count不为0。");
                }

				SAWrapper[]  sawArray = new SAWrapper[tongdaoCount];
				for (int tdIndex = 0; tdIndex < tongdaoCount ; tdIndex++)
				{
					sawArray[tdIndex] = new SAWrapper();
					for (int saIndex = 0; saIndex < iniAst.ReadInt("sa", tdIndex + "_saCount", 0); saIndex++)
					{
						SA sa = new SA
						{
							SAName = IniFileHelper_UTF8.ReadString(iniPath, "sa", tdIndex + "_" + saIndex + "_saName", ""),
							StartValue = iniAst.ReadInt("sa", tdIndex + "_" + saIndex + "_saStart", 0),
							EndValue = iniAst.ReadInt("sa", tdIndex + "_" + saIndex + "_saEnd", 0)
						};
						sawArray[tdIndex].SaList.Add(sa);
					}
				}
				return sawArray;
			}
			else{
				throw new Exception("iniPath不得为null或控制符串,这样无法获取子属性数组。");				
			}
		}
	}
}
