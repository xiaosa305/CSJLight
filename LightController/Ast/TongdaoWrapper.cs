using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightController.Ast
{
	// 通道包装类，记录了相关信息
	public class TongdaoWrapper
	{
		public string TongdaoName { get; set; }
		public int Address { get; set; }

		// 调节杆的值 --》两种方法改变：1拉杆 2.填值
		public int ScrollValue { get; set; }
		// 变化模式：跳变0；渐变1
		public int ChangeMode { get; set; }
		// 步时间：某内部时间因子的倍数
		public int StepTime { get; set; }

		/// <summary>
		/// 通过模板的通道数据，生成新的非引用(要摆脱与StepMode的关系)的tongdaoList
		/// </summary>
		/// <param name="oldTongdaoList"></param>
		/// <returns></returns>
		public static List<TongdaoWrapper> GenerateTongdaoList(List<TongdaoWrapper> stepModeTongdaoList)
		{
			List<TongdaoWrapper> newList = new List<TongdaoWrapper>();
			foreach (TongdaoWrapper item in stepModeTongdaoList)
			{
				newList.Add(new TongdaoWrapper()
				{
					StepTime = item.StepTime,
					TongdaoName = item.TongdaoName,
					ScrollValue = item.ScrollValue,
					ChangeMode = item.ChangeMode,
					Address = item.Address
				}
				);
			}
			return newList;
		}

	}
}
