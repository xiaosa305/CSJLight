using LightController.Tools.CSJ.IMPL;
using LightController.Utils;

namespace LightController.MyForm
{
	internal class NetworkUpdateReceiveCallBack : ICommunicatorCallBack
	{
		public void Completed(string deviceTag)
		{
			throw new System.NotImplementedException();
		}

		public void Error(string deviceTag, string errorMessage)
		{
			throw new System.NotImplementedException();
		}

		public void GetParam(CSJ_Hardware hardware)
		{
			throw new System.NotImplementedException();
		}

		public void UpdateProgress(string deviceTag, string fileName, int newProgress)
		{
			throw new System.NotImplementedException();
		}
	}
}