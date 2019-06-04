using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMX512;

namespace LightController.Ast
{
	public class ValueDAO: BaseDAO<DB_Value>
	{
		public ValueDAO(String dbFile, bool addPassword) : base(dbFile, addPassword)
		{
			
		}

	}
}
