using fastJSON;
using LightController.Ast.Entity;
using LightController.Common;
using LightController.MyForm;
using LightController.MyForm.OtherTools;
using Microsoft.VisualBasic.ApplicationServices;
using OtherTools;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using fastJSON;
using LightController.PeripheralDevice;

namespace LightController
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		// 10.15 下行注释掉：该属性标识当前控制台程序的线程模型为单线程,因此在该模型下编写多线程程序,并不能很好的兼容
		[STAThread]
		static void Main()
		{
			Dictionary<string, string> testDict = new Dictionary<string, string>();
			testDict.Add("userName", "admin");
			testDict.Add("password", "123");
			string resp = HttpHelper.PostUrlWithDict("http://localhost/lc/csLogin", testDict);

			//string jsonStr = fastJSON.JSON.ToJSON(testDict);
			//Console.WriteLine(jsonStr);
			//string resp = HttpHelper.PostUrl("http://localhost/lc/csLogin", jsonStr);

			Console.WriteLine(resp);

			ReturnDTO<object> rd = JSON.ToObject<ReturnDTO<object>>(resp);
			Dictionary<string,object> userDict = rd.data as Dictionary<string, object>;
			string sessionId = userDict["userName"] as string;

			OnlineConnect connect = new OnlineConnect(sessionId);
			
			connect.Connect( null );

			connect.SetSessionId();

			List<OnlineDeviceInfo> deviceList = connect.DeviceInfos;
			if (deviceList != null && deviceList.Count > 0)
			{
				foreach (var dev in deviceList)
				{
					Console.WriteLine(dev.DevoceName);
				}
			}
			else {
				Console.WriteLine("在线列表为空");
			}
			
			Console.WriteLine(connect);


			//// 下列代码作用为：避免重复打开此软件
			//bool ret;
			//System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out ret);

			//if (ret) {
			//	Application.EnableVisualStyles();
			//	Application.SetCompatibleTextRenderingDefault(false);
			//	if (IniFileHelper.GetControlShow(Application.StartupPath, "newMainForm"))
			//	{
			//		Application.Run(new NewMainForm());
			//	}
			//	else
			//	{
			//		Application.Run(new SkinMainForm());
			//	}
			//	mutex.ReleaseMutex();
			//}
			//else {
			//	MessageBox.Show("有一个和本程序相同的应用程序已经在运行，请不要同时运行多个本程序。");	
			//	Application.Exit();
			//}


		}
	}
}