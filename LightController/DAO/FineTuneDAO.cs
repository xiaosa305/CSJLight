using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMX512;

namespace LighEditor.Ast
{
	public class FineTuneDAO: BaseDAO<DB_FineTune>
	{
		public FineTuneDAO(String dbFile, bool isEncrypt) : base(dbFile, isEncrypt)
		{
			
		}

	}
}
