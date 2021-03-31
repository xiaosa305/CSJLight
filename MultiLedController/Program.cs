using MultiLedController.multidevice;
using MultiLedController.MyForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MultiLedController
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

			//Application.Run(new MainForm1());
			//Application.Run(new MainForm2());
			//Application.Run(new MainForm3());
			try
			{
				//Application.Run(new XiaosaTestForm());
				Application.Run(new MainForm4());
			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            //xiaosa的TestForm
            //Application.Run(new XiaosaTestFrom());
        }
	}
}
