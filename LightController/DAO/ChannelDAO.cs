using LightController.EntityNew;
using System;
using System.Collections.Generic;

namespace LightController.Ast
{
    public class ChannelDAO : BaseDAO<DB_Channel>
    {
        public ChannelDAO(string dbFile, bool isEncrypt) : base(dbFile, isEncrypt) { }

        internal void SaveSceneChannels(int scene, Dictionary<DB_ChannelPK, string> channelDict)
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
    }
}
