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
	public class LightDAO: BaseDAO<DB_Light>
	{

		public LightDAO(String dbFile,bool isEncrypt) : base(dbFile,isEncrypt)
		{

		}

	

	}
}
