using System;
using LightController.Entity;

namespace LightController.Ast
{
	public class LightDAO: BaseDAO<DB_Light>
	{
		public LightDAO(String dbFile,bool isEncrypt) : base(dbFile,isEncrypt)	{ }	

	}
}
