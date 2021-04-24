﻿using LightController.Ast;
using LightController.Common;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.MyForm.MainFormAst
{
	public partial class NewProjectUpdateForm : Form
	{
		public MainFormBase MainForm;
		private string exportProjectPath;

		public NewProjectUpdateForm(MainFormBase mainForm)
		{
			this.MainForm = mainForm;

			InitializeComponent();

			exportedCheckBox.Checked = Properties.Settings.Default.updateExported;

			folderBrowserDialog.Description = LanguageHelper.TranslateSentence("请选择工程目录的最后一层（即CSJ目录），本操作会将该目录下的所有文件传给设备。");
			// 当注册表内存储的文件夹不存在时，直接设为null并保存起来
			exportProjectPath = Properties.Settings.Default.exportProjectPath;
			if (Directory.Exists(exportProjectPath))
			{
				pathLabel.Text = exportProjectPath;
			}
			else
			{
				exportProjectPath = null;
				Properties.Settings.Default.exportProjectPath = null;
				Properties.Settings.Default.Save();
			}

			refreshButtons();  // 初始化
			
		}

		private void NewProjectUpdateForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			Control.CheckForIllegalCrossThreadCalls = false;  // 设false可在其他文件中修改本类的UI
			LanguageHelper.TranslateControl(this);
			
			// 在Load中再验证一下是否连接，如果没有连接，则关闭窗口（但这个操作因为太快 或 压根还没渲染出来，用户看不到）
			if (!MainForm.IsConnected) Dispose();			
		}

		private void ProjectUpdateForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Dispose();
			MainForm.Activate();
		}

		/// <summary>
		///  事件：当pathLable发生变化后，更改exportProjectPath的值(保存在注册表中)；并刷新各个按键的可用性
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pathLabel_TextChanged(object sender, EventArgs e)
		{
			exportProjectPath = pathLabel.Text;
			Properties.Settings.Default.exportProjectPath = exportProjectPath;
			Properties.Settings.Default.Save();
			refreshButtons();
		}

		/// <summary>
		/// 辅助方法：刷新《工程更新》按键是否可用（启动后 及 更改路径Lable后执行）
		/// </summary>
		private void refreshButtons()
		{
			dirPanel.Visible = exportedCheckBox.Checked;
			updateButton.Enabled = MainForm.IsConnected && //必要条件
				(exportedCheckBox.Checked && (!string.IsNullOrEmpty(exportProjectPath))   // 如果勾选《更新已有工程》
				|| (!exportedCheckBox.Checked && !string.IsNullOrEmpty(MainForm.GlobalIniPath)));   // 如果选择更新当前工程，则必须当前已打开工程（用GlobalIniPath判断即可）
		}

		/// <summary>
		/// 事件：点击《选择已有工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dirChooseButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == folderBrowserDialog.ShowDialog())
			{
				pathLabel.Text = folderBrowserDialog.SelectedPath;
			}
		}

		/// <summary>
		/// 事件：选中《更新当前工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void currentCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			refreshButtons();
			Properties.Settings.Default.updateExported = exportedCheckBox.Checked ;
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// 事件：点击《更新工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateButton_Click(object sender, EventArgs e)
		{
			// 0.最开始的提示
			DialogResult dr = MessageBox.Show(
					LanguageHelper.TranslateSentence("更新工程会覆盖设备(tf卡)内原有的工程，是否继续？"),
					LanguageHelper.TranslateSentence("是否继续更新工程?"),
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question);
			if (dr == DialogResult.Cancel)	return;
			
			// 1.决定更新后，先设为忙时
			SetBusy(true);
			
			// 1.1更新已有工程
			if (exportedCheckBox.Checked) 
			{
				if (string.IsNullOrEmpty(exportProjectPath) || Directory.GetFiles(exportProjectPath).Length == 0)
				{
					SetNotice("未选择工程目录或所选目录为空，无法更新工程。请选择正确的已有工程目录，并重新更新。", true, true);
					SetBusy(false);
					return;
				}
				FileUtils.CopyFileToDownloadDir(exportProjectPath);
				DownloadProject();
			}
			// 1.2更新当前工程
			else {			
				if (string.IsNullOrEmpty(MainForm.GlobalIniPath) )
				{
					SetNotice("主界面尚未打开工程，无法更新工程。", true, true);
					SetBusy(false);
					return;
				}
				SetNotice("正在实时生成工程数据，请耐心等待...", false, true);
				DataConvertUtils.SaveProjectFile(MainForm.GetDBWrapper(false), MainForm, MainForm.GlobalIniPath, new NewGenerateProjectCallBack(this) );
			}
		}

		/// <summary>
		/// 辅助方法：将本地的工程文件，传送到设备中(因可能由外部类回调，故需单独写一个方法)
		/// </summary>
		public void DownloadProject()
		{
			MainForm.SleepBetweenSend(1);
			MainForm.MyConnect.DownloadProject(DownloadCompleted, DownloadError, DrawProgress);
		}

		/// <summary>
		/// 辅助回调方法：工程更新成功
		/// </summary>
		/// <param name="obj"></param>
		public void DownloadCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				SetNotice("工程更新成功。", true, true);
				myProgressBar.Value = 0;
				SetBusy(false);
			});
		}

		/// <summary>
		/// 辅助回调方法：工程更新失败
		/// </summary>
		/// <param name="obj"></param>
		public void DownloadError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				SetNotice("工程更新失败[" + msg + "]", true, false);
				myProgressBar.Value = 0;
				SetBusy(false);
			});
		}

		/// <summary>
		/// 辅助回调方法：写进度条
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="progress"></param>
		public void DrawProgress(string fileName, int progressPercent)
		{
			SetNotice(string.IsNullOrEmpty(fileName) ? "" : LanguageHelper.TranslateSentence("正在传输文件：") + fileName, false, false);
			myProgressBar.Value = progressPercent;
		}		

		#region 通用方法

		/// <summary>
		/// 辅助方法：显示信息
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="messageBoxShow">是否在提示盒内提示</param>
		public void SetNotice(string msg, bool messageBoxShow, bool isTranslate)
		{
			if (isTranslate)
			{
				msg = LanguageHelper.TranslateSentence(msg);
			}

			if (messageBoxShow)
			{
				MessageBox.Show(msg);
			}
			myStatusLabel.Text = msg;
			statusStrip1.Refresh();
		}

		/// <summary>
		/// 辅助方法：设定忙时
		/// </summary>
		/// <param name="busy"></param>
		public void SetBusy(bool busy)
		{
			Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
			Enabled = !busy;
		}

		/// <summary>
		/// 事件：各个按键文字发生变化后，也要相应进行翻译
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someButton_TextChanged(object sender, EventArgs e)
		{
			LanguageHelper.TranslateControl(sender as Button);
		}

		#endregion		
	}

	public class NewGenerateProjectCallBack : ISaveProjectCallBack
	{
		private NewProjectUpdateForm puForm;		

		public NewGenerateProjectCallBack(NewProjectUpdateForm puForm)
		{
			this.puForm = puForm;
		}

		public void Completed()
		{
			puForm.SetNotice("数据生成成功，即将传输数据到设备。", false, true);
			if (FileUtils.CopyProjectFileToDownloadDir())
			{
				puForm.MainForm.GenerateSourceZip(Application.StartupPath + @"\DataCache\Download\CSJ\Source.zip");
				puForm.DownloadProject();
			}
			else
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("拷贝工程文件到临时目录时出错。"));
				puForm.SetBusy(false);
			}
		}

		public void Error(string msg)
		{
			MessageBox.Show(LanguageHelper.TranslateSentence("数据生成出错"));
			puForm.SetBusy(false);
		}

		public void UpdateProgress(string name)
		{
			puForm.SetNotice(LanguageHelper.TranslateSentence("正在生成工程文件") + "(" + name + ")", false, false);
		}
	}
}