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
	}
}
