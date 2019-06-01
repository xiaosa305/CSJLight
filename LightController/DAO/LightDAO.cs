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
	public class LightDAO<DB_Light> : BaseDAO<DB_Light>
	{
		public LightDAO(String dbFile):base(dbFile)
		{
			
		}
			


	}
}
