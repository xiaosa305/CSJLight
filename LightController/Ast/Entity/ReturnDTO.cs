using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast.Entity
{
	public class ReturnDTO<T>
	{
		public int code { get; set; }
		public string msg { get; set; }
		public T data { get; set; }
		public List<T> dataList { get; set; }
	}
}
