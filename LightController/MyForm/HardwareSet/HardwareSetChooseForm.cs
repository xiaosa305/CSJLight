﻿using LightController.Common;
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
	public partial class HardwareSetChooseForm : Form
	{
		private MainFormBase mainForm;
		private bool isJustDelete = false;
		private string savePath ;

		public HardwareSetChooseForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;
			InitializeComponent();

			// 读取硬盘上的,硬件设置列表
			savePath = IniHelper.GetSavePath(Application.StartupPath);
			string  hardwareLibraryPath = savePath + @"\HardwareLibrary";
			if (Directory.Exists(hardwareLibraryPath))
			{
				string[] dirs = Directory.GetDirectories(hardwareLibraryPath);
				DirectoryInfo[] diArray = DirectoryHelper.GenerateDiretoryInfoArray(dirs);
				if (diArray == null)
				{
					return;
				}
				DirectoryHelper.SortAsFolderByLastWriteTime(ref diArray);
				foreach (DirectoryInfo di in diArray)
				{
					TreeNode treeNode = new TreeNode(di.Name);
					treeView1.Nodes.Add(treeNode);
				}
			}
		}

		private void HardwareSetChooseForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			LanguageHelper.InitForm(this);
		}

		/// <summary>
		///  事件：关闭窗体
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HardwareSetChooseForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Dispose();
			mainForm.Activate();
		}

		/// <summary>
		///  事件：点击《打开》：打开硬件配置文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openButton_Click(object sender, EventArgs e)
		{
			openFile();
		}
		
		/// <summary>
		/// 事件：双击treeView1的选项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_DoubleClick(object sender, EventArgs e)
		{
			openFile();
		}

		/// <summary>
		///  重新选中节点后，设ifJusetDelete值为false
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			isJustDelete = false;
		}
		
		/// <summary>
		/// 事件：点击《删除》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteButton_Click(object sender, EventArgs e)
		{
			// 若非刚删除
			if (!isJustDelete)
			{
				//  弹出是否删除的确认框
				if (MessageBox.Show(
					LanguageHelper.TranslateSentence("确定删除此硬件配置吗？"),
					LanguageHelper.TranslateSentence("删除硬件配置？"),
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Warning) == DialogResult.Cancel)
				{
					return;
				}

				// 先取出目录path
				string projectName = treeView1.SelectedNode.Text;
				string directoryPath =  savePath + @"\HardwareLibrary\" + projectName;
				DirectoryInfo di = new DirectoryInfo(directoryPath);

				// 删除目录
				try
				{
					di.Delete(true);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}
				// 3.删除treeView1.SelectedNode;并设置ifJustDelete属性为true，避免客户误操作
				treeView1.SelectedNode.Remove();
				isJustDelete = true;
			}
			else
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("请选择要删除的配置。"));
				return;
			}
		}

		/// <summary>
		/// 事件：点击《取消》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}
					
		/// <summary>
		/// 事件：点击《新建》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			this.mainForm.Activate();
			new HardwareSetForm(mainForm, null, null).ShowDialog();
		}

		/// <summary>
		///  辅助方法：双击或点击打开按钮时会打开配置文件。
		/// </summary>
		private void openFile() {
			// 1.先验证是否刚删除项目
			if (isJustDelete)
			{
				MessageBox.Show( LanguageHelper.TranslateSentence("请选择正确的配置文件。") );
				return;
			}
			// 2.验证是否为空选项
			if (treeView1.SelectedNode != null)
			{
				string hName = treeView1.SelectedNode.Text;
				if (!String.IsNullOrEmpty(hName))
				{
					// 打开相关的配置文件，再加载到HardwareSetForm中
					string iniPath = savePath + @"\HardwareLibrary\" + hName + @"\HardwareSet.ini";
					Dispose();
					mainForm.Activate();
					HardwareSetForm hsForm = new HardwareSetForm(mainForm, iniPath, hName);
					hsForm.ShowDialog();
				}
				else
				{
					MessageBox.Show( LanguageHelper.TranslateSentence("请选择正确的配置文件。") );
					return;
				}
			}
		}
			
	}
}
