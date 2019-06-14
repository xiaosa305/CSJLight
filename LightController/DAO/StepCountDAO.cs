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
	public class StepCountDAO: BaseDAO<DB_StepCount>
	{
		public StepCountDAO(String dbFile,bool addPassword):base(dbFile,addPassword)
		{
			
		}

		
	}
}
