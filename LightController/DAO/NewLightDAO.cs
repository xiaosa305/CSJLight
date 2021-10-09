using System;
using LightController.EntityNew;

namespace LightController.Ast
{
	public class NewLightDAO: BaseDAO<DB_NewLight>
	{
		public NewLightDAO(String dbFile,bool isEncrypt) : base(dbFile,isEncrypt)	{ }	

	}
}
