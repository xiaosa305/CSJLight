using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
	public class BaseDAO<T>
	{
		protected Configuration config;
		protected ISessionFactory sessionFactory;

		public BaseDAO(string dbFile, bool addPassword)
		{
			config = new Configuration().Configure();
			if (addPassword)
			{
				config.SetProperty("connection.connection_string", @"Data Source=" + dbFile + ";password=" + MD5Ast.MD5("Dickov" + dbFile));
			}
			else {
				config.SetProperty("connection.connection_string", @"Data Source=" + dbFile);
			}
			sessionFactory = config.BuildSessionFactory();
		}

		/// <summary>
		///  慎用此功能：根据现有的映射文件，重建数据库表
		/// </summary>
		public void CreateSchema(bool ifPrint, bool ifDeleteOld)
		{
			new SchemaExport(config).Create(ifPrint, ifDeleteOld);
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
		///  执行各种操作
		/// </summary>
		/// <param name="method"></param>
		/// <param name="obj"></param>
		/// 
		public void Action(string method, T obj)
		{
			using (var session = sessionFactory.OpenSession())
			{
				using (var tx = session.BeginTransaction())
				{
					try
					{
						switch (method)
						{
							case "Save": session.Save(obj); break;
							case "Update": session.Update(obj); break;
							case "Delete": session.Delete(obj); break;
							case "SaveOrUpdate": session.SaveOrUpdate(obj);break;
							default:Console.WriteLine("方法名出错");break;
						}
						tx.Commit();
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						tx.Rollback();
					}
				}
				
			}
		}
		


		/// <summary>
		///  执行保存操作
		/// </summary>
		public void Save(T obj)
		{
			Action("Save", obj);
		}

		/// <summary>
		///  执行SaveOrUpdate操作：新的就save，旧的就update
		/// </summary>
		/// <param name="obj"></param>
		public void SaveOrUpdate(T obj)
		{
			Action("SaveOrUpdate", obj);
		}

		/// <summary>
		///  执行更新操作
		/// </summary>
		public void Update(T obj)
		{
			Action("Update", obj);
		}

		/// <summary>
		///  执行删除操作
		/// </summary>
		public void Delete(T obj)
		{
			Action("Delete", obj);
		}

		/// <summary>
		///  通过id，取出对象
		/// </summary>
		public T Get(object id)
		{
			using (ISession session = sessionFactory.OpenSession())
			{
				return session.Get<T>(id);
			}
		}

		/// <summary>
		///  获取所有
		/// </summary>
		/// <returns></returns>
		public IList<T> GetAll()
		{
			using (ISession session = OpenSession())
			{
				return session.CreateCriteria(typeof(T)).List<T>();
			}
		}

		
	}
}
