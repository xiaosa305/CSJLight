using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LightController.Ast.Helper
{
    public class ColorHelper 
    {
		/// <summary>
		/// 辅助方法：由输入的通道值，设置背景颜色（供多步联调使用)
		/// </summary>
		/// <param name="stepValue"></param>
		/// <returns></returns>
		public static Color GetBackColor(int stepValue , Color defaultColor)
		{
			if (stepValue == 0)
			{
				return defaultColor;
			}
			else if (stepValue > 0 && stepValue < 255)
			{
				return Color.SteelBlue;
			}
			else
			{
				return Color.IndianRed;
			}
		}

	}
}
