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
		IList<int> GetChannelIDList();
	}
}
