using LightController.EntityNew;
using System;
using System.Collections.Generic;

namespace LightController.Ast
{
    public class ChannelDAO : BaseDAO<DB_Channel>
    {
        public ChannelDAO(string dbFile, bool isEncrypt) : base(dbFile, isEncrypt) { }

		public void SaveSceneChannels(int scene, Dictionary<DB_ChannelPK, string> channelDict)
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
								Value = channelDict[pk]
							});;
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
		
		public DB_Channel GetByPK( DB_ChannelPK pk)
		{
			using (var session = sessionFactory.OpenSession())
			{
				DB_Channel channel = (DB_Channel)session
					.CreateQuery("FROM DB_Channel c WHERE c.PK = :pk")
					.SetEntity("pk",pk)				
					.UniqueResult();
				return channel;
			}
		}

	}
}
