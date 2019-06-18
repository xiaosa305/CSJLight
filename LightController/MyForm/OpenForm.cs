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

		private void enterButton_Click(object sender, EventArgs e)
		{
			string projectName =  treeView1.SelectedNode.Text;			
			if (!String.IsNullOrEmpty(projectName))
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

		private void deleteButton_Click(object sender, EventArgs e)
		{

		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}
	}
}
