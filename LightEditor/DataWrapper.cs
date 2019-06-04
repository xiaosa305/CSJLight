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
		private string tongdaoName;
		private int initNum;
		private int address;

		public string TongdaoName { get => tongdaoName; set => tongdaoName = value; }
		public int InitNum { get => initNum; set => initNum = value; }
		public int Address { get => address; set => address = value; }

		public TongdaoWrapper(string tongdaoName, int initNum, int address)
		{
			this.tongdaoName = tongdaoName;
			this.initNum = initNum;
			this.address = address;
		}
	}
}
