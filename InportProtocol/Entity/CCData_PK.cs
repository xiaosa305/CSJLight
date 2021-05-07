using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportProtocol.Entity
{
	public class CCData_PK
	{
		public virtual int CCIndex { get; set; }
		public virtual int Code { get; set; }

		/// <summary>
		/// 判断两个对象是否相同，这个方法需要重写
		/// </summary>
		/// <param name="obj">进行比较的对象</param>
		/// <returns>真true或假false</returns>
		public override bool Equals(object obj)
		{
			if (obj is CCData_PK)
			{
				CCData_PK pk = obj as CCData_PK;
				if (this.CCIndex == pk.CCIndex && this.Code == pk.Code )
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
