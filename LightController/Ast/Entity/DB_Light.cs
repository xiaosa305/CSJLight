using LightController.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMX512
{
    public class DB_Light
    {
		public virtual int LightNo { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string Pic { get; set; }
        public virtual int StartID { get; set; }
        public virtual int Count { get; set; }
		public virtual string Remark { get; set; }

		/// <summary>
		///  内置静态辅助方法 : 使用LightAst生成DB_Light
		/// </summary>
		/// <param name="la"></param>
		/// <returns></returns>
		public static DB_Light GenerateLight(LightAst la)
		{
			return new DB_Light()
			{
				LightNo = la.StartNum,
				StartID = la.StartNum,
				Name = la.LightName,
				Type = la.LightType,
				Pic = la.LightPic,
				Count = la.Count,
				Remark = la.Remark
			};
		}

		// 重写ToString()；
		//public override string ToString()
		//{
		//	return "LightNo:" + LightNo + "\n"
		//		+ "Name:" + Name + "\n"
		//		+ "Type:" + Type + "\n"
		//		+ "Pic:" + Pic + "\n"
		//		+ "Count:" + Count;
		//}
	}
}
