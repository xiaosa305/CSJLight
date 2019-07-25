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
	public partial class NewHardwareForm : Form
	{
		private HardwareSetForm hardwareSetForm;
		private string directoryPath = @"C:\Temp\HardwareLibrary\";

		public NewHardwareForm(HardwareSetForm hardwareSetForm)
		{
			this.hardwareSetForm = hardwareSetForm;
			InitializeComponent();
		}

		/// <summary>
		/// 点击《确认》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			string hName = hNameTextBox.Text;
			if (String.IsNullOrEmpty(hName)) {
				MessageBox.Show("请输入正确的硬件配置名称!");
				return;
			}

			directoryPath += hName;
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
				MessageBox.Show("这个名称已经被使用了，请使用其他名称。");
				return;
			}
			else
			{
				// 1.由新建时取的名称，来新建相关文件夹
				di.Create();
				// 2.将相关HardwareSet.ini拷贝到相关文件夹中，并调用hardwareSetForm的相关存储方法
				string sourcePath = Application.StartupPath + @"\HardwareSet.ini";
				string globalIniFilePath = directoryPath + @"\HardwareSet.ini";
				File.Copy(sourcePath, globalIniFilePath);
				hardwareSetForm.SaveAll(globalIniFilePath,hName);

				this.Dispose();
				hardwareSetForm.Activate();
			}

		}

		/// <summary>
		///  设初始位置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewHardwareForm_Load(object sender, EventArgs e)
		{
			Location = new Point(hardwareSetForm.Location.X + 100, hardwareSetForm.Location.Y + 100);
		}

		/// <summary>
		///  点击《取消》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			hardwareSetForm.Activate();
		}
		
		/// <summary>
		///  点击右上角关闭按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewHardwareForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			hardwareSetForm.Activate();
		}
	}
}
