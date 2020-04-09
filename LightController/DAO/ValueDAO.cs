using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMX512;
using System.Threading;

namespace LightController.Ast
{
	public class ValueDAO : BaseDAO<DB_Value>
	{
		public ValueDAO(string dbFile, bool isEncrypt) : base(dbFile, isEncrypt)
		{
			//this.dbFile = dbFile;
			//this.isEncrypt = isEncrypt;
		}

		/// <summary>
		/// 辅助方法：通过lightIndex,frame,mode ,step ；来获取某一步的tongdaoList
		/// </summary>
		internal IList<DB_Value> GetStepValueList(int lightIndex, int frame, int mode, int step)
		{
			using (var session = sessionFactory.OpenSession())
			{
				IList<DB_Value> valueList = (IList<DB_Value>)session
					.CreateQuery("FROM DB_Value v WHERE" +
						" v.PK.LightIndex =:lightIndex " +
						"AND v.PK.Frame = :frame " +
						"AND v.PK.Mode =:mode " +
						"AND v.PK.Step =:step " +
						"ORDER BY v.PK.LightID")
					.SetInt32("lightIndex", lightIndex)
					.SetInt32("frame", frame)
					.SetInt32("mode", mode)
					.SetInt32("step", step)
					.List<DB_Value>();
				return valueList;
			}
		}


		/// <summary>
		/// MARK 只开单场景：11.0 ValueDAO中添加一个GetTDValueListOrderByStep(),作用为获取某一个TD特定FM的所有步信息
		/// 辅助方法：通过lightIndex,frame,mode；来获取某一FM的tongdao的所有步
		/// </summary>
		internal IList<DB_Value> GetTDValueListOrderByStep(DB_ValuePK pk)
		{
			using (var session = sessionFactory.OpenSession())
			{
				IList<DB_Value> valueList = (IList<DB_Value>)session
					.CreateQuery("FROM DB_Value v WHERE" +
						" v.PK.LightID=:lightID " +
						"AND v.PK.Frame = :frame " +
						"AND v.PK.Mode = :mode " +									
						"ORDER BY v.PK.Step")
					.SetInt32("lightID", pk.LightID)
					.SetInt32("frame", pk.Frame)
					.SetInt32("mode", pk.Mode)		
					.List<DB_Value>();
				return valueList;
			}
		}


		/// <summary>
		/// 10.16 辅助方法：通过灯具起始通道值，获取该灯具所有value数据
		/// </summary>
		/// <param name="lightNo"></param>
		/// <returns></returns>
		internal IList<DB_Value> GetByLightIndex(int lightIndex)
		{
			using (var session = sessionFactory.OpenSession())
			{
				IList<DB_Value> valueList = (IList<DB_Value>)session
					.CreateQuery("FROM DB_Value v WHERE" +
						" v.PK.LightIndex =:lightIndex " +
						"ORDER BY v.PK.LightID")
					.SetInt32("lightIndex", lightIndex)
					.List<DB_Value>();
				return valueList;
			}
		}

		/// <summary>
		/// 10.16 辅助方法：通过灯具起始通道值，获取该灯具所有value数据
		/// </summary>
		/// <param name="lightNo"></param>
		/// <returns></returns>
		internal IList<DB_Value> GetByLightIndexAndFrame(int lightIndex,int frame)
		{
			using (var session = sessionFactory.OpenSession())
			{
				IList<DB_Value> valueList = (IList<DB_Value>)session
					.CreateQuery("FROM DB_Value v WHERE " +
						"v.PK.LightIndex =:lightIndex " +
						"AND v.PK.Frame =:frame " +
						"ORDER BY v.PK.LightID")
					.SetInt32("lightIndex", lightIndex)
					.SetInt32("frame",frame)
					.List<DB_Value>();
				return valueList;
			}
		}

		/// <summary>
		/// 10.28 辅助方法：通过灯具起始通道值 和 场景编号，获取该灯具在某场景下的所有value数据
		/// </summary>
		/// <param name="tempLightNo"></param>
		/// <param name="frame"></param>
		/// <returns></returns>
		internal IList<DB_Value> GetByLightNoAndFrame(int lightIndex, int frame)
		{
			using (var session = sessionFactory.OpenSession())
			{
				IList<DB_Value> valueList = (IList<DB_Value>)session
					.CreateQuery("FROM DB_Value v WHERE" +
						" v.PK.LightIndex =:lightIndex " +
						"AND v.PK.Frame = :frame " +
						"ORDER BY v.PK.LightID")
					.SetInt32("lightIndex", lightIndex)
					.SetInt32("frame",frame)
					.List<DB_Value>();

				return valueList;
			}
		}

		/// <summary>
		/// 10.17 辅助方法：通过场景号，删除数据库value表中内相关的数据（每个灯具的该场景的数据清掉）
		/// </summary>
		/// <param name="frame"></param>
		internal void DeleteFrameValues(int frame)
		{
			using (var session = sessionFactory.OpenSession())
			{
				using (var tx = session.BeginTransaction())
				{
					try
					{
						session.CreateSQLQuery("delete from value where frame =:frame ")
							.SetInt32("frame",frame)
							.ExecuteUpdate();						
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
		///  辅助方法：保存某场景的所有灯具列表
		/// </summary>
		/// <param name="frameValueList"></param>
		internal void SaveFrameValues(int frame, IList<DB_Value> frameValueList)
		{
			using (var session = sessionFactory.OpenSession())
			{
				using (var tx = session.BeginTransaction())
				{
					try
					{
						session.CreateSQLQuery("delete from value where frame =:frame ")
							.SetInt32("frame", frame)
							.ExecuteUpdate();

						foreach (DB_Value value in frameValueList)
						{
							session.Save(value);
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
		/// 10.24 辅助方法：通过灯具起始通道值，获取该灯具所有value数据
		/// </summary>
		/// <param name="lightNo"></param>
		/// <returns></returns>
		public IList<DB_Value> GetPKList(DB_ValuePK pk)
		{
			using (var session = sessionFactory.OpenSession())
			{
					IList<DB_Value> valueList = (IList<DB_Value>)session
						.CreateQuery("FROM DB_Value v WHERE " +
							"v.PK.LightID =:lightID  " +
							"AND v.PK.Mode=:mode " +
							"AND v.PK.Frame=:frame " +
							//"AND v.PK.LightIndex=:lightIndex " +
							"ORDER BY v.PK.Step ASC")
						.SetInt32("mode", pk.Mode)
						.SetInt32("frame", pk.Frame)
						.SetInt32("lightID", pk.LightID)
						//.SetInt32("lightIndex",pk.LightIndex)
						.List<DB_Value>();

					return valueList;
			}				
		}

		/// <summary>
		/// 辅助方法：获取有数据的通道的列表
		/// </summary>
		/// <returns></returns>
		public IList<int> GetTDList()
		{
			using (var session = sessionFactory.OpenSession())
			{
				IList<int> tdList = (IList<int>)session
					.CreateSQLQuery("select distinct LightID from value").List<int>();												
				return tdList;
			}
		}

	
	}


}
