using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
	/// <summary>
	///  这个类记载某一个light，在选定场景和模式下，其中的一个步数
	/// </summary>
	public class StepWrapper
	{
		public bool IsSaved { get; set; }
		public List<TongdaoWrapper> TongdaoList { get; set; }

	}
}
