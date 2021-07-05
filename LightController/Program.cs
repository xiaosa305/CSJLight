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
using System.Reflection;

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

			if (IniHelper.GetParamBool("multiRun") || ret ) //允许多开 或 当前尚未打开一个软件的情况
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				//用反射创建MainForm( 得转成Form类型，且为全限定类名(=包名+类名，带包路径的用"."隔开) )
				Application.Run( Assembly.GetExecutingAssembly().CreateInstance("LightController.MyForm."+ (IniHelper.GetParamBool("newMainForm")? "NewMainForm":"SkinMainForm")) as Form	);
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