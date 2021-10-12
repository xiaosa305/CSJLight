using System;
using LightController.Entity;

namespace LightController.Ast
{
	public class FineTuneDAO: BaseDAO<DB_FineTune>
	{
		public FineTuneDAO(String dbFile, bool isEncrypt) : base(dbFile, isEncrypt)
		{
			
		}

	}
}
