using DMX512;
using LightController.Ast;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Environment = NHibernate.Cfg.Environment;

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
			Application.Run(new MainForm());

			//List<int> intList = new List<int>();
			//intList.Add(1);
			//intList.Add(2);
			//intList.Add(3);

			//List<int> intList2 = new List<int>(intList);
			//intList.Add(4);
			//Console.WriteLine(intList2.Count);
			//foreach (var item in intList2)
			//{
			//	Console.WriteLine(item.ToString());
			//}

			//string[,] strArr = new string[2, 2];
			//strArr[0, 0] = "Dickov";
			//string b = strArr[0, 0];
			//b = "Fan";
			//Console.WriteLine(strArr[0,0]);
			//strArr[0, 0] = b;
			//b = "zwj";
			//Console.WriteLine(strArr[0,0]);

		}
	}
}
