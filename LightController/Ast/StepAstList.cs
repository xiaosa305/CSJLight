using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
	/// <summary>
	///  这个类记载某一个light，在选定场景和模式下，所有步数的集合
	/// </summary>
	public class StepAstList
	{
		public List<StepAst> StepList { get; set; }

	}
}
