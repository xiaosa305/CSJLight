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
		public int ScrollValue { get; set; }
		public int ChangeMode { get; set; }
		public int StepTime { get; set; }

	}
}
