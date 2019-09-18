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
		public StepCountDAO(String dbFile,bool isEncrypt):base(dbFile,isEncrypt)	{	}

		///// <summary>
		/////  通过lightIndex 获取stepCount列表
		///// </summary>
		///// <param name="lightIndex"></param>
		///// <returns></returns>
		public IList<DB_StepCount> getStepCountList(int lightIndex)
		{
			using (var session = sessionFactory.OpenSession())
			{
				IList<DB_StepCount> scList = (IList<DB_StepCount>)session
					.CreateQuery("FROM DB_StepCount sc WHERE sc.PK.LightIndex =:lightIndex")
					.SetInt32("lightIndex", lightIndex)
					.List<DB_StepCount>();
				return scList;
			}
		}

	}
}
