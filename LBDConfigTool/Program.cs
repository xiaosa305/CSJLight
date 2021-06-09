using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace LBDConfigTool
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			// 下列代码作用为：避免重复打开此软件
			bool ret;
			System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out ret);

			if(ret) //允许多开 或 当前尚未打开一个软件的情况
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new ConfForm());
				mutex.ReleaseMutex();
			}
			else
			{
				MessageBox.Show("本程序禁止多开！");
				Application.Exit();
			}

		}
	}
}
