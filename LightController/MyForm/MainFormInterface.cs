using DMX512;
using LightController.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.MyForm
{
	public interface MainFormInterface
	{
		//MARK 只开单场景：10.0 抽象出GetFMTDList() 
		IList<TongdaoWrapper> GetFMTDList(DB_ValuePK pk);
	}
}
