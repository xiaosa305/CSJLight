using DMX512;
using LightController.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.MyForm
{
	interface MainFormInterface
	{
		IList<TongdaoWrapper> GetFMTDList(DB_ValuePK pk);

	}
}
