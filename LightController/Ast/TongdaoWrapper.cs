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
		
		public int ScrollValue { get; set; }// 调节杆的值 --》两种方法改变：1拉杆 2.填值
		public int ChangeMode { get; set; }     // 变化模式： | 常规：跳变0；渐变1；屏蔽2    |  声控：屏蔽0；跳变1；（渐变2）
		public int StepTime { get; set; }    // 步时间：某内部时间因子的倍数

		public string Remark { get; set; } //备注,主要用以显示各个子属性，在渲染时要写进去

		/// <summary>
		/// 通过模板的通道数据，生成新的非引用(要摆脱与StepMode的关系)的tongdaoList
		/// </summary>
		/// <param name="oldTongdaoList"></param>
		/// <returns></returns>
		public static IList<TongdaoWrapper> GenerateTongdaoList(IList<TongdaoWrapper> stepTemplateTongdaoList ,int mode)
		{
			IList<TongdaoWrapper> newList = new List<TongdaoWrapper>();
			foreach (TongdaoWrapper td in stepTemplateTongdaoList)
			{
				// 9.7 如果是模板数据，则根据mode来决定changeMode的初值
				newList.Add(new TongdaoWrapper()	{
						StepTime = td.StepTime,
						TongdaoName = td.TongdaoName,
						ScrollValue = td.ScrollValue,
						ChangeMode = td.ChangeMode == -1 ? (mode == 0 ? 1 : 0) : td.ChangeMode ,
						Address = td.Address ,
						Remark = td.Remark
					}
				);
			}
			return newList;
		}

		
	}
}
