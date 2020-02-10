using LightController.Common;
using OtherTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.OtherTools
{
	public partial class KpPositionSaveForm : Form
	{
		private NewToolsForm otForm;
		private string kpPosPath;

		public KpPositionSaveForm(NewToolsForm otForm,int keyCount)
		{
			InitializeComponent();

			this.otForm = otForm;
			kpPosPath = @IniFileAst.GetSavePath(Application.StartupPath) + @"\KeypressPosition\" + keyCount + @"\";
			Text = "保存墙板位置(" + keyCount + "键)";
		}

		private void KpPositionSaveForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(otForm.Location.X + 100, otForm.Location.Y + 100);
		}

		/// <summary>
		/// 事件：点击《取消》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			otForm.Activate();
		}

		private void enterButton_Click(object sender, EventArgs e)
		{
			string fileName = fileNameTextBox.Text.Trim();
			if (String.IsNullOrEmpty(fileName))
			{
				MessageBox.Show("请输入文件名。");
				return;
			}					   			
			if (!FileAst.CheckFileName(fileName))
			{
				MessageBox.Show("文件命名不规范，无法保存。");
				return;
			}

			string arrangeIniPath = kpPosPath + fileName + ".ini";

			FileInfo fi = null;
			try
			{
				fi = new FileInfo(arrangeIniPath);
			}
			catch (Exception ex)
			{
				MessageBox.Show("输入了错误的字符;\n" + ex.Message);
				return;
			}

			// 1.7 判断名称是否已存在；若存在，选覆盖则先删除旧文件夹；否则退出方法。
			if (fi.Exists)
			{
				DialogResult dr = MessageBox.Show(
					"当前名称的文件已存在，是否覆盖？",
					"",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question
				);
				if (dr == DialogResult.OK)
				{
					fi.Delete();
				}
				else
				{
					return;
				}
			}

			// 保存操作
			if (otForm.KPSavePosition(arrangeIniPath)) {
				this.Dispose();
				otForm.Activate();
			}
		}
	}
}
