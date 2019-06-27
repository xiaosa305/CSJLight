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
		// 暂时无用
		public bool IsSaved { get; set; }

		// 这两项项为复制灯数据时使用：
		// 不同的灯具全名必然不同(厂商名+型号）；
		// lightMode则用以区分常规场景和声控场景-->常规场景无法复制到声控场景中，反之亦然
		public int lightMode { get; set; }  
		public string lightFullName { get; set; }
			   
		// 这个列表记录通道数据
		public List<TongdaoWrapper> TongdaoList { get; set; }

	}
}
