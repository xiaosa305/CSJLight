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

		public ValueDAO(string dbFile, bool isEncrypt) : base(dbFile, isEncrypt) {

			//this.dbFile = dbFile;
			//this.isEncrypt = isEncrypt;
		}

		/// <summary>
		/// 辅助方法：通过lightIndex,frame,mode ,step ；来获取某一步的tongdaoList
		/// </summary>
		internal IList<DB_Value> getStepValueList(int lightIndex, int frame, int mode, int step)
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
		/// 10.16 辅助方法：通过灯具起始通道值，获取该灯具所有value数据
		/// </summary>
		/// <param name="lightNo"></param>
		/// <returns></returns>
		internal IList<DB_Value> GetByLightNo(int lightIndex)
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

	

	}


}
