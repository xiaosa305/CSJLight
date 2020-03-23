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
	}
}
