using LightController.Ast;

namespace LightController.Entity
{
    public class DB_Light
    {
		public virtual int LightID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string Pic { get; set; }     
        public virtual int Count { get; set; }
		public virtual string Remark { get; set; }

		/// <summary>
		/// 因为存在有入参的构造器，必须显式声明一个空构造器
		/// </summary>
		public DB_Light() { }
		
		/// <summary>
		/// 使用LightAst作为入参的构造器
		/// </summary>
		/// <param name="la"></param>
		public DB_Light(LightAst la) {
				LightID = la.StartNum;	
				Name = la.LightName;
				Type = la.LightType;
				Pic = la.LightPic;
				Count = la.Count;
				Remark = la.Remark;
		}

	}
}
