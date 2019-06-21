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
	public partial class OpenForm : Form
	{
		private MainForm mainForm;
		public OpenForm(MainForm mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;

			string path = @"C:\Temp\LightProject";
			if (Directory.Exists(path))
			{
				string[] dirs = Directory.GetDirectories(path);
				foreach (string dir in dirs)
				{
					DirectoryInfo di = new DirectoryInfo(dir);
					TreeNode treeNode = new TreeNode(di.Name);							
					this.treeView1.Nodes.Add(treeNode);
				}
			}
		}

		// 辅助变量，主要是是删除选中节点后，treeView1会自动选择下一个节点，但不会显示出来
		private bool ifJustDelete = false;

		/// <summary>
		///  选中node后，点击打开文件后的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			// 1.先验证是否刚删除项目
			if (ifJustDelete)
			{
				MessageBox.Show("请选择正确的项目名");
				return;
			}
			// 2.验证是否为空选项
			if(treeView1.SelectedNode != null) { 
				string projectName =  treeView1.SelectedNode.Text;			
				if ( ! String.IsNullOrEmpty(projectName) )
				{				
					mainForm.OpenProject(projectName);
					this.Dispose();						
				}
				else
				{
					MessageBox.Show("请选择正确的项目名");
					return;
				}
			}
		}
		
		/// <summary>
		///  删除文件后，若从新选中一个新的node，则ifJustDelete就重新设为false;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			ifJustDelete = false;
		}

		/// <summary>
		/// 删除项目功能；后期可能不保留
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteButton_Click(object sender, EventArgs e)
		{
			// 1. 先取出目录path
			string projectName = treeView1.SelectedNode.Text;
			string directoryPath = "C:\\Temp\\LightProject\\" + projectName;
			DirectoryInfo di = new DirectoryInfo(directoryPath);

			// 2.删除目录
			di.Delete(true);

			// 3.删除treeView1.SelectedNode;并设置ifJustDelete属性为true，避免客户误操作
			treeView1.SelectedNode.Remove();
			ifJustDelete = true;
		}
		
		/// <summary>
		///  取消按钮点击后的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}
		
	}
}
