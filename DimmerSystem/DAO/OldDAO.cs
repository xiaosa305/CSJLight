using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using LightController.Common;
using System.Collections;

namespace LightController.DAO
{
	public class OldDAO
	{
		public Configuration config;
		public ISessionFactory sessionFactory;				

		public OldDAO(string dbFile, bool isEncrypt)
		{		
			config = new Configuration().Configure();
			config.SetProperty("connection.connection_string", @"Data Source=" + dbFile + (isEncrypt ? ";password=" + MD5Helper.MD5_UTF8("Dickov" + dbFile) : ""));
			sessionFactory = config.BuildSessionFactory();
		}		

		/// <summary>
		/// 获取当前session
		/// </summary>
		/// <returns>Session</returns>
		public ISession GetCurrentSession()
		{
			return sessionFactory.GetCurrentSession();
		}

		/// <summary>
		/// 获取新session
		/// </summary>
		/// <returns>Session</returns>
		public ISession OpenSession()
		{
			return sessionFactory.OpenSession();
		}

        /// <summary>
        ///  获取所有T对象
        /// </summary>
        /// <returns></returns>
        public IList<object[]> GetListBySQL(string sql)
		{
			using (ISession session = OpenSession())
			{
				return session.CreateSQLQuery(sql).List<object[]>();
			}
		}

	
		

	}
}
