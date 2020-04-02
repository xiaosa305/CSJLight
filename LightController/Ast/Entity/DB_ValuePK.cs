using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMX512
{
    public class DB_ValuePK
    {
		public virtual int LightIndex { get; set;}  //其所属灯具的初始地址，也是灯具的编号
		public virtual int LightID { get; set; }  // LightID，指向的是这个通道的地址，LightID = LightIndex+tongdaoIndex
		public virtual int Frame { get; set; } //场景号，0开始
		public virtual int Mode { get; set; }       // 模式，0常规，1音频
		public virtual int Step { get; set; }   //步数，0开始        

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

		/// <summary>
		/// 重写ToString()
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return LightIndex + " - " + LightID + " - " + Frame + " - " + Mode + " - " + Step;
		}
	}
}
