using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Security.Cryptography;
using LightController.Ast;
using System.Data.SQLite;
 

namespace LightController
{
	public partial class NewForm :Form
	{
		private	MainForm mainForm;
		public NewForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
			
			this.StartPosition = FormStartPosition.Manual;
			this.Location = new Point(mainForm.Location.X + 200 , mainForm.Location.Y + 200);

			InitializeComponent();
		}

		private void enterButton_Click(object sender, EventArgs e)
		{
			string s = textBox1.Text;		   
			if (!String.IsNullOrEmpty(s))
			{
				string directoryPath = "C:\\Temp\\LightProject\\" + s;
				DirectoryInfo di = null;
				try
				{
					 di = new DirectoryInfo(directoryPath);
				}
				catch (Exception ex) {
					MessageBox.Show(ex.Message);
					return;
				}
				
				if (di.Exists)
				{
					MessageBox.Show("这个名称已经被使用了，请使用其他名称。");
					return;
				}
				else
				{
					// 1.由新建时取的项目名，来新建相关文件夹
					di.Create();
					// 2.将相关global.ini和data.db3拷贝到文件夹内
					string sourcePath = Application.StartupPath;
					string globalIniFilePath = directoryPath + @"\global.ini";					
					File.Copy( sourcePath+@"\global.ini",  globalIniFilePath);

					// 3.添加密码 -- 正式使用时添加，测试时就不要加了。
					// SQLiteHelper.SetPassword(dbFile);

					mainForm.BuildProject(s,true);
					MessageBox.Show("成功新建项目");
					this.Dispose();
				}				
			}
			else
			{
				MessageBox.Show("请输入项目名");
				return;
			}
		}
		


		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}
	}
}
