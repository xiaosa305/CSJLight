using LightController.Ast;

namespace LightController.EntityNew
{
    public class DB_NewLight
    {
		public virtual int LightID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string Pic { get; set; }     
        public virtual int Count { get; set; }
		public virtual string Remark { get; set; }

		/// <summary>
		///  内置静态辅助方法 : 使用LightAst生成DB_Light
		/// </summary>
		/// <param name="la"></param>
		/// <returns></returns>
		public static DB_NewLight GenerateLight(LightAst la)
		{
			return new DB_NewLight()
			{
				LightID = la.StartNum,	
				Name = la.LightName,
				Type = la.LightType,
				Pic = la.LightPic,
				Count = la.Count,
				Remark = la.Remark
			};
		}

	}
}
