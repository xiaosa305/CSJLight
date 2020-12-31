using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast.Entity
{
	public class TranslateResult
	{
		public string from { set; get; }
		public string to { set; get; }
		public List<Dictionary<string,string>>  trans_result { set; get; }  // 嵌套在数组中
	}
}
