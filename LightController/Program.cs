using LightController.MyForm;
using OtherTools;
using System;
using System.Windows.Forms;

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
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SkinMainForm());
            //Application.Run(new OtherToolsForm());
            //Application.Run(new MainForm());
            //Application.Run(new TestForm());
        }
	}
}
