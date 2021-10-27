using LightController.Ast;
using LightController.Entity;
using System.Collections.Generic;

namespace LightController.MyForm
{
	public interface MainFormInterface
	{
		//DOTO 211012 要重写 GetSMTDList √
		//MARK 只开单场景：10.0 抽象出GetSMTDList，主要供维佳调用（放在MainFormInterface中）
		IList<TongdaoWrapper> GetSMTDList( DB_ChannelPK pk);
		IList<DB_FineTune> GetFineTunes();
		int GetSceneCount();
		IList<DB_Light> GetLights();
		string GetConfigPath();
		
		/// <summary>
		/// 获取工程中所有灯具的所有通道列表（无论是否有步数）
		/// </summary>
		/// <returns></returns>
		IList<int> GetChannelIDList();		
		/// <summary>
		/// 获取需要导出的场景编号，使用时foreach(int sceneNo in xx.GetExportSceneSet()) 即可
		/// </summary>
		/// <returns></returns>
		HashSet<int> GetExportSceneSet();
	}
}
