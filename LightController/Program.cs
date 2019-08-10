using LightController.MyForm;
using System;
using System.Windows.Forms;

namespace LightController
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new NewMainForm());
			//Application.Run(new MainForm());
		}
	}
}
