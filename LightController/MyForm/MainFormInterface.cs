using LightController.Ast;
using LightController.Entity;
using System.Collections.Generic;

namespace LightController.MyForm
{
	public interface MainFormInterface
	{
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

		/// <summary>
		/// 获取预览所需的音频步时间
		/// </summary>
		/// <returns></returns>
		int GetPreviewMusicControlTime();
		/// <summary>
		/// 获取预览所需的音频叠加后等待时间
		/// </summary>
		/// <returns></returns>
		int GetPreviewMusicWaitTime();
		/// <summary>
		/// 获取预览所需的音频步数链表
		/// </summary>
		/// <returns></returns>
		List<int> GetPreviewMusicStepList();
		/// <summary>
		/// 获取预览所需的时间因子
		/// </summary>
		/// <returns></returns>
		int GetPreviewFactoryTime();
		/// <summary>
		/// 获取预览所需的通道数据
		/// </summary>
		/// <param name="channelNo"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		IList<TongdaoWrapper> GetPreviewChannelData(int channelNo, int mode);
	}
}
