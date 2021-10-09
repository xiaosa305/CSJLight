using System;
using LightController.EntityNew;

namespace LightController.Ast
{
	public class NewFineTuneDAO: BaseDAO<DB_NewFineTune>
	{
		public NewFineTuneDAO(String dbFile, bool isEncrypt) : base(dbFile, isEncrypt)
		{
			
		}

	}
}
