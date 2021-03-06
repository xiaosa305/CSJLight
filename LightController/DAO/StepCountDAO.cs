using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMX512;
using LightController.MyForm;

namespace LightController.Ast
{
	public class StepCountDAO : BaseDAO<DB_StepCount>
	{
		public StepCountDAO(String dbFile, bool isEncrypt) : base(dbFile, isEncrypt) { }

		/// <summary>
		/// 辅助方法：通过lightIndex 获取此灯具的stepCount列表
		/// </summary>
		/// <param name="lightIndex"></param>
		/// <returns></returns>
		public IList<DB_StepCount> GetStepCountList(int lightIndex)
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

		///MARK 只开单场景：05.0 添加取步数值的辅助方法
		/// <summary>
		/// 辅助方法：通过lightIndex及frame值，获取指定场景的StepCount值列表（注意会拿到两个模式的灯具数据)
		/// </summary>
		/// <param name="frame"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public IList<DB_StepCount> GetStepCountListByFrame(int lightIndex,int frame) {
			using (var session = sessionFactory.OpenSession())
			{
				IList<DB_StepCount> scList = (IList<DB_StepCount>)session
					.CreateQuery("FROM DB_StepCount sc " +
							"WHERE sc.PK.LightIndex =:lightIndex " +
							"AND sc.PK.Frame=:frame")
					.SetInt32("lightIndex", lightIndex)
					.SetInt32("frame", frame)
					.List<DB_StepCount>();
				return scList;
			}
		}

		/// <summary>
		/// MARK 只开单场景：13.0 StepCountDAO内添加通过pk取值的方法（12.3辅助方法）
		/// 辅助方法：通过pk取StepCount对象的方法
		/// </summary>
		/// <param name="stepCountPK"></param>
		/// <returns></returns>
		internal DB_StepCount GetStepCountByPK(DB_StepCountPK pk)
		{
			using (var session = sessionFactory.OpenSession())
			{
				DB_StepCount sc = (DB_StepCount)session
					.CreateQuery("FROM DB_StepCount sc " +
						"WHERE sc.PK.LightIndex =:lightIndex " +
						"AND sc.PK.Frame=:frame " +
						"AND sc.PK.Mode = :mode")
					.SetInt32("lightIndex", pk.LightIndex)
					.SetInt32("frame", pk.Frame)
					.SetInt32("mode",pk.Mode)
					.UniqueResult();
				return sc;
			}
		}

		/// <summary>
		/// 传入frameLoadArray,算出已加载到内存的Index列表，把这些数据排除，查找剩余的数据（DB）
		/// </summary>
		/// <param name="frameLoadArray"></param>
		/// <returns></returns>
		internal IList<DB_StepCount> GetAllWithList(bool[] frameLoadArray)
		{
			//先找出不查找的Index列表
			List<int> notList = new List<int>();
			for(int frameIndex=0;frameIndex< frameLoadArray.Length;frameIndex++)
			{
				if (frameLoadArray[frameIndex]) {
					notList.Add(frameIndex);
				}
			}

			using (var session = sessionFactory.OpenSession())
			{
				IList<DB_StepCount> scList = (IList<DB_StepCount>)session
					.CreateQuery("FROM DB_StepCount sc WHERE " +							
							"sc.PK.Frame not in (:notList)")
					.SetParameterList("notList",notList)
					.List<DB_StepCount>();
				return scList;
			}
		}
	}		
}
