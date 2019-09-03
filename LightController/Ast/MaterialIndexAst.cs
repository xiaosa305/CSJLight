using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LighEditor.Ast
{	
	/// <summary>
	///  纯辅助类：调用素材时，若找到匹配的通道，就添加一个本对象
	/// </summary>
	public class MaterialIndexAst
	{
		public int MaterialTDIndex { get; set; }
		public int CurrentTDIndex { get; set; }

	}
}
