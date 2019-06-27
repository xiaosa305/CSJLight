using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightEditor
{	
	// 辅助类：包装[data]属性的三个数据
	public class TongdaoWrapper
	{		
		public string TongdaoName { get;set; }
		public int InitValue { get; set; }
		public int CurrentValue { get; set; }
		public int Address { get; set; }

		public TongdaoWrapper(string tongdaoName, int initValue, int address,int currentValue)
		{
			this.TongdaoName = tongdaoName;
			this.InitValue = initValue;
			this.Address = address;
			this.CurrentValue = currentValue;
		}
	}
}
