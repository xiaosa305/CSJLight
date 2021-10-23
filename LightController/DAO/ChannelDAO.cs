using LightController.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightController.Ast
{
    public class ChannelDAO : BaseDAO<DB_Channel>
    {
        public ChannelDAO(string dbFile, bool isEncrypt) : base(dbFile, isEncrypt) { }

		/// <summary>
		/// 保存指定场景的所有通道值，保存前先把旧的数据都清空
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="channelDict"></param>
		public void SaveSceneChannels(int scene, Dictionary<DB_ChannelPK, StringBuilder> channelDict)
        {
			using (var session = sessionFactory.OpenSession())
			{
				using (var tx = session.BeginTransaction())
				{
					try
					{
						session.CreateSQLQuery("delete from channel where scene =:scene ")
							.SetInt32("scene", scene)
							.ExecuteUpdate();

						foreach (DB_ChannelPK pk in channelDict.Keys)
						{
							session.Save(new DB_Channel() { 
								PK = pk,
								Value = channelDict[pk].Remove( channelDict[pk].Length -1,1) .ToString ()
							});
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
		/// 获取指定Scene、Mode、灯具ID的通道值列表,以ChannelID升序排序
		/// </summary>
		/// <param name="lightID"></param>
		/// <param name="scene"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
        public IList<DB_Channel> GetList(int lightID, int scene, int mode)
        {
			using (var session = sessionFactory.OpenSession())
			{
				IList<DB_Channel> channelList = session
                    .CreateQuery("FROM DB_Channel c WHERE " +
                        "c.PK.LightID = :lightID " +
                        "AND c.PK.Scene = :scene " +
                        "AND c.PK.Mode = :mode " +
                        "ORDER BY c.PK.ChannelID")
                    .SetInt32("lightID", lightID)
                    .SetInt32("scene", scene)
                    .SetInt32("mode", mode)
                    .List<DB_Channel>();
				return channelList;
			}
		}

		/// <summary>
		/// 通过DB_ChannelPK对象，获取唯一的channel值（PK内LightID不能用于查询语句）
		/// </summary>
		/// <param name="pk"></param>
		/// <returns></returns>
        public DB_Channel GetByPK(DB_ChannelPK pk)
        {
			using (var session = sessionFactory.OpenSession())
			{
				DB_Channel channel = session
					.CreateQuery("FROM DB_Channel c " +
						"WHERE c.PK.ChannelID = :ChannelID " +
						"AND c.PK.Scene = :Scene " +
						"AND c.PK.Mode = :Mode")		
					.SetInt32("ChannelID",pk.ChannelID)
					.SetInt32("Scene",pk.Scene)
					.SetInt32("Mode",pk.Mode)
					.UniqueResult<DB_Channel>();
				return channel;
			}
		}

		/// <summary>
		///删除表内非需保留的灯具的所有数据
		/// </summary>
		public void DeleteRedundantData(IList<int> retainLightIndices)
		{
			using (var session = sessionFactory.OpenSession())
			{
				if (retainLightIndices == null || retainLightIndices.Count == 0)
				{
					Clear();
					return;
				}

				session
					.CreateQuery("DELETE FROM DB_Channel c WHERE c.PK.LightID NOT IN (:lightIndices)")
					.SetParameterList("lightIndices", retainLightIndices)
					.ExecuteUpdate();
			}
		}

        public IList<int> GetExistSceneList()
        {
			using (var session = sessionFactory.OpenSession())
			{
				IList<int> sceneList = session
					.CreateSQLQuery("select distinct(scene) from channel where mode = 0")
					.List<int>();
				return sceneList;
			}
		}
    }
}
