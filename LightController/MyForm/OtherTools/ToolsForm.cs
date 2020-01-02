using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm
{
	public partial class ToolsForm : Form
	{

		private MainFormInterface mainForm;

		public ToolsForm(MainFormInterface mainForm)
		{
			this.mainForm = mainForm;
			InitializeComponent();
		}

		private void ToolsForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
		}

		/// <summary>
		/// 事件：点击右上角关闭按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolsForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：点击《灯库编辑工具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightEditorSkinButton_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(Application.StartupPath + @"\LightEditor.exe");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 事件：点击《传视界灯控工具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DKToolSkinButton_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(Application.StartupPath + @"\QDController\灯光控制器.exe");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 事件：点击《传视界中控工具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ZKToolSkinButton_Click(object sender, EventArgs e)
		{
			try
			{
				
				System.Diagnostics.Process.Start(Application.StartupPath + @"\CenterController\KTV中央控制器.exe");
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 事件：点击《传视界墙板工具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void QBToolSkinButton_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(Application.StartupPath + @"\KeyPress\墙板码值.exe");
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}			
		}
	}
	
}
