using LightController.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm
{
	public partial class NewMainForm : Form
	{
		Image buttonImg1 = global::LightController.Properties.Resources.Ok3w_Net图标13;
		Image buttonImg2 = global::LightController.Properties.Resources.Ok3w_Net图标1;

		public NewMainForm()
		{
			InitializeComponent();

			//#region 皮肤相关代码
			//IniFileAst iniFileAst = new IniFileAst(Application.StartupPath + @"\GlobalSet.ini");
			//string skin = iniFileAst.ReadString("SkinSet", "skin", "");
			//if (!String.IsNullOrEmpty(skin))
			//{
			//	this.skinEngine1.SkinFile = Application.StartupPath + "\\irisSkins\\" + skin;
			//}
			//DirectoryInfo fdir = new DirectoryInfo(Application.StartupPath + "\\irisSkins");
			//try
			//{
			//	FileInfo[] file = fdir.GetFiles();
			//	if (file.Length > 0)
			//	{
			//		foreach (var item in file)
			//		{
			//			if (item.FullName.EndsWith(".ssk"))
			//			{
			//				skinComboBox.Items.Add(item.Name.Substring(0, item.Name.Length - 4));
			//			}
			//		}
			//		skinComboBox.SelectedIndex = 0;

			//		skinComboBox.Show();
			//		skinButton.Show();
			//	}
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
			//#endregion

		}

		private void skinButton_Click(object sender, EventArgs e)
		{
			//string sskName = skinComboBox.Text;
			//this.skinEngine1.SkinFile = Application.StartupPath + "\\irisSkins\\" + sskName + ".ssk";
		}



		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Environment.Exit(0);
		}

		private void NewMainForm_Load(object sender, EventArgs e)
		{

		}
	}
}
