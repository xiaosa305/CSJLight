using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportProtocol.Common
{
	public class DBUtils
	{
		private static Configuration conf;//  NHibernate初始化(默认会解析hibernate.cfg.xml)															 
		private static ISessionFactory sessionFactory; //  创建工厂		

		public static ISession GetDBSession() {

			if (conf == null) {
				conf = new Configuration().Configure();
			}
			if (sessionFactory == null) {
				sessionFactory = conf.BuildSessionFactory();
			}

			return sessionFactory.OpenSession();
		}

		public static void Close() {
			if (sessionFactory != null) {
				sessionFactory.Close();
			}
		}

	}
}
