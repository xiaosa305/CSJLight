using System;
using LightController.Entity;

namespace LightController.DAO
{
	public class LightDAO: BaseDAO<DB_Light>
	{
		public LightDAO(String dbFile,bool isEncrypt) : base(dbFile,isEncrypt)	{ }	

	}
}
