using LightController.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.MyForm
{
	public interface MainFormInterface
	{
		//DOTO 211012 要重写 GetFMTDList
		//MARK 只开单场景：10.0 抽象出GetFMTDList() ，主要供维佳调用（放在MainFormInterface中）
		IList<TongdaoWrapper> GetFMTDList(/*DB_ValuePK pk*/);
	}
}
