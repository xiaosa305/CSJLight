using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightEditor.Ast
{
	public class SAWrapper
	{
		//public int TdAddress { get; set; }
		//public int SaCount { get; }
		public IList<SA> SaList{ get; set; }

		public SAWrapper()
		{
			SaList = new List<SA>();
		}

		/// <summary>
		/// 辅助静态方法：深度复制sawArray，《LightEditorForm》及《WaySetForm》皆会用到
		/// </summary>
		/// <param name="sourceSawArray"></param>
		/// <returns></returns>
		public static SAWrapper[] DeepCopy(SAWrapper[] sourceSawArray) {

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

	}
}
