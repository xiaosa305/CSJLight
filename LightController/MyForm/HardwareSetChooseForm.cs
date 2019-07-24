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
		private MainForm mainForm;
		private bool ifJustDelete = false;

		public HardwareSetChooseForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
			InitializeComponent();

			// 读取硬盘上的,硬件设置列表
			string path = @"C:\Temp\HardwareLibrary";
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

		private void HardwareSetChooseForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
		}

		/// <summary>
		///  点击《打开》：打开硬件配置文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openButton_Click(object sender, EventArgs e)
		{
			// 1.先验证是否刚删除项目
			if (ifJustDelete)
			{
				MessageBox.Show("请选择正确的配置文件名");
				return;
			}
			// 2.验证是否为空选项
			if (treeView1.SelectedNode != null)
			{
				string projectName = treeView1.SelectedNode.Text;
				if (!String.IsNullOrEmpty(projectName))
				{
					this.Dispose();
					// 打开相关的配置文件，再加载到HardwareSetForm中
					string iniPath = @"C:\Temp\HardwareLibrary\" + projectName + @"\HardwareSet.ini";
					this.Dispose();
					mainForm.Activate();
					HardwareSetForm hsForm = new HardwareSetForm(mainForm, iniPath);					
					hsForm.ShowDialog();
				}
				else
				{
					MessageBox.Show("请选择正确的配置文件名");
					return;
				}
			}
		}


		/// <summary>
		///  重新选中节点后，设ifJusetDelete值为false
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			this.ifJustDelete = false;
		}
	}
}
