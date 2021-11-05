using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using LightController.Common;
using LightController.Entity;

namespace LightController.DAO
{
	public class BaseDAO<T>
	{
		public Configuration config;
		public ISessionFactory sessionFactory;

		/// <summary>
		/// 静态方法：根据现有的映射文件（新），重建数据库表
		/// </summary>
		public static void CreateSchema( string dbFile, bool isEncrypt )
		{
			Configuration tempConfig = new Configuration().Configure();
			tempConfig.SetProperty("connection.connection_string", @"Data Source=" + dbFile + (isEncrypt ? ";password=" + MD5Helper.MD5_UTF8("Dickov" + dbFile) : ""));
			tempConfig.AddClass(typeof(DB_Channel));
			tempConfig.AddClass(typeof(DB_Light));
			tempConfig.AddClass(typeof(DB_FineTune));
			new SchemaExport(tempConfig).Create( true , true);
		}				

		public BaseDAO(string dbFile, bool isEncrypt)
		{		
			config = new Configuration().Configure();
			config.SetProperty("connection.connection_string", @"Data Source=" + dbFile + (isEncrypt ? ";password=" + MD5Helper.MD5_UTF8("Dickov" + dbFile) : ""));
			config.AddClass(typeof(T));
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
							case "SaveOrUpdate": session.SaveOrUpdate(obj); break;
							default: Console.WriteLine("方法名出错"); break;
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
		///  获取所有T对象
		/// </summary>
		/// <returns></returns>
		public IList<T> GetAll()
		{
			using (ISession session = OpenSession())
			{
				return session.CreateCriteria(typeof(T)).List<T>();
			}
		}

		/// <summary>
		///  针对不同的数据库表，
		///  1. 先采用删除数据；
		///  2.再保存所有传进的T
		/// </summary>
		public void SaveAll(string tableName, IList<T> objList)
		{
			using (var session = sessionFactory.OpenSession())
			{
				using (var tx = session.BeginTransaction())
				{
					try
					{
						session.CreateSQLQuery("delete from " + tableName).ExecuteUpdate();
						if (objList != null && objList.Count > 0) {
							foreach (T obj in objList)
							{
								session.Save(obj);
							}
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
		/// 保存或更新所有传进来的T
		/// </summary>
		/// <param name="objList"></param>
		public void SaveOrUpdateAll(IList<T> objList)
		{
			using (var session = sessionFactory.OpenSession())
			{
				using (var tx = session.BeginTransaction())
				{
					try
					{
						if (objList != null && objList.Count > 0) {
							foreach (T obj in objList)
							{
                                session.SaveOrUpdate(obj);
							}					
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
		/// MARK 只开单场景：14.5 BaseDAO中清空相关表内的所有数据
		///删除相应的表内所有数据
		/// </summary>
		public void Clear()
		{
			using (var session = sessionFactory.OpenSession())
			{
				Type t = typeof(T);
				string hql = "DELETE FROM " + t.Name;
				session.CreateQuery(hql).ExecuteUpdate();
			}
		}
		
    }
}
