using LightController.Common;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.Project
{
    public partial class NewForm : UIForm
    {
        private MainFormBase mainForm;
        public NewForm(MainFormBase mainForm)
        {           
            InitializeComponent();

            this.mainForm = mainForm;

            //渲染场景号
            for (int sceneIndex = 0; sceneIndex < MainFormBase.AllSceneList.Count; sceneIndex++)
            {
                sceneComboBox.Items.Add(MainFormBase.AllSceneList[sceneIndex]);
            }
            sceneComboBox.SelectedIndex = mainForm.CurrentScene;

        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            Location = MousePosition;
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
			string projectName = projectNameTextBox.Text;

			if (!FileHelper.CheckFileName(projectName))
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("工程名包含非法字符，请重新输入！"));
				return;
			}

			if (string.IsNullOrEmpty(projectName))
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("请输入工程名"));
				return;
			}

			string directoryPath = mainForm.SavePath + @"\LightProject\" + projectName;
			DirectoryInfo di = null;
			try
			{
				di = new DirectoryInfo(directoryPath);
			}
			catch (Exception ex)
			{
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
				// 2.将相关global.ini拷贝到存储目录中
				string sourcePath = Application.StartupPath;
				string globalIniFilePath = directoryPath + @"\global.ini";
				File.Copy(sourcePath + @"\Default\global.ini", globalIniFilePath);

				// 3.添加密码 -- 正式使用时添加，测试时就不要加了。
				// SQLiteHelper.SetPassword(dbFile);
				//确定时，传入sceneIndex
				mainForm.NewProject(projectName, sceneComboBox.SelectedIndex);
				Dispose();
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
			Dispose();
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
