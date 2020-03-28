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

		///MARK 0328大变动：3.0 添加取步数值的辅助方法
		/// <summary>
		/// 辅助方法：通过lightIndex及frame值，获取指定场景的StepCount值列表
		/// </summary>
		/// <param name="frame"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public IList<DB_StepCount> GetStepCountListByFrame(int lightIndex,int frame) {
			using (var session = sessionFactory.OpenSession())
			{
				IList<DB_StepCount> scList = (IList<DB_StepCount>)session
					.CreateQuery("FROM DB_StepCount sc WHERE sc.PK.LightIndex =:lightIndex AND sc.PK.Frame=:frame")
					.SetInt32("lightIndex", lightIndex)
					.SetInt32("frame", frame)
					.List<DB_StepCount>();
				return scList;
			}
		}
	}		
}
