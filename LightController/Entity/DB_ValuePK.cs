using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMX512
{
    public class DB_ValuePK
    {
		public virtual int LightIndex { get; set;}
        public virtual int Frame { get; set; }
        public virtual int Step { get; set; }
        public virtual int Mode { get; set; }

		// 这个对象，指向的是这个通道的地址，LightID = LightIndex+tongdaoIndex
        public virtual int LightID { get ; set; }

		/// <summary>
		/// 判断两个对象是否相同，这个方法需要重写
		/// </summary>
		/// <param name="obj">进行比较的对象</param>
		/// <returns>真true或假false</returns>
		public override bool Equals(object obj)
		{
			if (obj is DB_ValuePK)
			{
				DB_ValuePK pk = obj as DB_ValuePK;
				if (this.LightIndex == pk.LightIndex && this.Frame == pk.Frame && this.Mode == pk.Mode && this.LightID == pk.LightID && this.Step == pk.Step)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 联合主键类，这个方法也必须重写，哪怕不改动其内容或只是从base中执行方法
		/// </summary>
		/// <returns>基类的GetHashCode()值</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
