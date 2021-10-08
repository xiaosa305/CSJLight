namespace LightController.Ast.Entity
{
    public class DB_Light
    {
		public virtual int LightAddr { get; set; }
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
		public static DB_Light GenerateLight(LightAst la)
		{
			return new DB_Light()
			{
				LightAddr = la.StartNum,	
				Name = la.LightName,
				Type = la.LightType,
				Pic = la.LightPic,
				Count = la.Count,
				Remark = la.Remark
			};
		}

	}
}
