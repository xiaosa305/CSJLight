using fastJSON;
using LightController.Ast.Entity;
using LightController.Common;
using LightController.MyForm;
using LightController.MyForm.OtherTools;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using LightController.PeripheralDevice;
using LightController.Ast;

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
			// 下列代码作用为：避免重复打开此软件
			bool ret;
			System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out ret);

			if (ret)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				if (IniHelper.GetIsShow("newMainForm"))
				{
					Application.Run(new NewMainForm());
				}
				else
				{
					 Application.Run(new SkinMainForm());
				}
				mutex.ReleaseMutex();
			}
			else
			{
				MessageBox.Show("有一个和本程序相同的应用程序已经在运行，请不要同时运行多个本程序。");
				Application.Exit();
			}			
		}
		
	}
}