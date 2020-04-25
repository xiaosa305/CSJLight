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
	public partial class ProjectRenameOrCopyForm : Form
	{
		private OpenForm openForm;
		private string oldProjectName;
		private string savePath;
		private bool copy = false;

		public ProjectRenameOrCopyForm(OpenForm openForm, string oldProjectName, bool copy = false)
		{
			this.openForm = openForm;
			this.oldProjectName = oldProjectName;
			this.copy = copy;		

			InitializeComponent();
			projectNameLabel.Text = oldProjectName;
			projectNameTextBox.Text = oldProjectName;
			this.Text = copy ? "工程复制" : "工程重命名";
		}

		private void RenameForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(openForm.Location.X + 100, openForm.Location.Y + 100);
		}

		/// <summary>
		/// 事件：点击《取消、右上角X》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelSkinButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			openForm.Activate();
		}

		/// <summary>
		/// 事件：点击《确定》
		/// -- 重命名需要的操作流程：
		/// ①把选中的文件夹改名（如果无法改名，那就先复制一个出来，在删除原来的工程文件夹）:
		///	-- 涉及 命名是否合法、同名验证 和 同名工程 的验证，可参考使用NewForm中的验证。
		/// ②刷新openForm中的listView1
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterSkinButton_Click(object sender, EventArgs e)
		{
			string projectName = projectNameTextBox.Text;

			if (!FileHelper.CheckFileName(projectName))
			{
				MessageBox.Show("工程名包含非法字符，请重新输入。");
				return;
			}

			if (String.IsNullOrEmpty(projectName))
			{
				MessageBox.Show("工程名不得为空，请重新输入。");
				return;
			}

			savePath = IniFileHelper.GetSavePath(Application.StartupPath);
			string destDirPath = savePath + @"\LightProject\" + projectName;
			if (Directory.Exists(destDirPath)) {
				MessageBox.Show("当前工程名已存在同名工程，请重新输入。");
				return;
			}

			string sourceDirPath = savePath + @"\LightProject\" + oldProjectName;
			try
			{
				if (copy)
				{
					// 复制的流程：先创建文件夹（之前的校验确定该文件夹不存在）：再遍历把原文件夹的所有文件，都复制到目标文件夹中。
					Directory.CreateDirectory(destDirPath);
					string[] files = Directory.GetFiles(sourceDirPath);
					foreach (string file in files)
					{
						string pFilePath = destDirPath + "\\" + Path.GetFileName(file);						
						File.Copy(file, pFilePath, true);
					}
					MessageBox.Show("工程复制成功。");
				}
				else {
					Directory.Move(sourceDirPath, destDirPath);
					MessageBox.Show("工程重命名成功。");
				}
				openForm.RefreshDirTreeView();				
				this.Dispose();
				openForm.Activate();
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
				return;
			}				
		}
	}
}
