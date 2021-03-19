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
using LightController.MyForm;
using LightController.Common;

namespace LightController
{
	public partial class NewForm :Form
	{
		private MainFormBase mainForm;
		public NewForm(MainFormBase mainForm,int currentFrame)
		{
			this.mainForm = mainForm;
			InitializeComponent();

			//MARK 只开单场景：00.0 NewForm加场景选择
			for (int frameIndex = 0; frameIndex < MainFormBase.AllFrameList.Count; frameIndex++)
			{
				frameComboBox.Items.Add(MainFormBase.AllFrameList[frameIndex]);
			}
			frameComboBox.SelectedIndex = currentFrame;
			
		}

		private void NewForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 200, mainForm.Location.Y + 200);
			// 翻译
			LanguageHelper.InitForm(this);
		
		}

		/// <summary>
		///  事件：点击《新建》按钮：
		///  1.创建目录
		///  2.拷贝默认的global.ini到新工程的目录中；
		///  3.回调mainForm中的InitProject()
		///  4.收尾：Dispose这个窗口，激活主窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			string projectName = textBox1.Text;

			if ( ! FileHelper.CheckFileName(projectName)) {
				MessageBox.Show(LanguageHelper.TranslateSentence("工程名包含非法字符，请重新输入！"));
				return;
			}

			if (String.IsNullOrEmpty(projectName)) {
				MessageBox.Show(LanguageHelper.TranslateSentence("请输入工程名"));				
				return;
			}

			string directoryPath = mainForm.SavePath + @"\LightProject\" + projectName;
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
				MessageBox.Show(LanguageHelper.TranslateSentence("这个名称已经被使用了，请使用其他名称。"));
				return;
			}
			else
			{
				// 1.由新建时取的工程名，来新建相关文件夹
				di.Create();
				// 2.将相关global.ini和data.db3拷贝到文件夹内
				string sourcePath = Application.StartupPath;
				string globalIniFilePath = directoryPath + @"\global.ini";					
				File.Copy( sourcePath+@"\global.ini",  globalIniFilePath);

				// 3.添加密码 -- 正式使用时添加，测试时就不要加了。
				// SQLiteHelper.SetPassword(dbFile);
				//MARK 只开单场景：01.1 NewForm点确定时，传入frameIndex
				mainForm.NewProject(projectName,frameComboBox.SelectedIndex);				
				Dispose();
				mainForm.IsCreateSuccess = true;
				mainForm.Activate();					
			}	
		}
		
		/// <summary>
		/// 事件：点击《右上角关闭（X）》按钮、《取消》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}
		
		/// <summary>
		///  事件：点击《右上角？》按钮提示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show(
				LanguageHelper.TranslateSentence("工程名不可使用\\、/、:、*、?、\"、<、>、| 等字符，否则操作系统(windows)无法保存，会出现错误。"),
				LanguageHelper.TranslateSentence("提示"),
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
			e.Cancel = true;
		}
	}
}
