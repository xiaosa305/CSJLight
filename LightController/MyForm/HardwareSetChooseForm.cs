using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LighEditor.MyForm
{
	public partial class HardwareSetChooseForm : Form
	{
		private MainFormInterface mainForm;
		private bool isJustDelete = false;

		public HardwareSetChooseForm(MainFormInterface mainForm)
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
			openFile();
		}


		/// <summary>
		///  重新选中节点后，设ifJusetDelete值为false
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			this.isJustDelete = false;
		}


		/// <summary>
		///  点击《删除》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteButton_Click(object sender, EventArgs e)
		{
			// 若非刚删除
			if (!isJustDelete)
			{
				// 1. 先取出目录path
				string projectName = treeView1.SelectedNode.Text;
				string directoryPath = @"C:\Temp\HardwareLibrary\" + projectName;
				DirectoryInfo di = new DirectoryInfo(directoryPath);

				// 2.删除目录
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
				MessageBox.Show("请选择要删除的配置:");
				return;
			}
		}

		/// <summary>
		///  点击《取消》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}

		private void HardwareSetChooseForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}

		private void treeView1_DoubleClick(object sender, EventArgs e)
		{
			openFile();
		}

		/// <summary>
		///  辅助方法：双击或点击打开按钮时会打开配置文件。
		/// </summary>
		private void openFile() {
			// 1.先验证是否刚删除项目
			if (isJustDelete)
			{
				MessageBox.Show("请选择正确的配置文件名");
				return;
			}
			// 2.验证是否为空选项
			if (treeView1.SelectedNode != null)
			{
				string hName = treeView1.SelectedNode.Text;
				if (!String.IsNullOrEmpty(hName))
				{
					this.Dispose();
					// 打开相关的配置文件，再加载到HardwareSetForm中
					string iniPath = @"C:\Temp\HardwareLibrary\" + hName + @"\HardwareSet.ini";
					this.Dispose();
					mainForm.Activate();
					HardwareSetForm hsForm = new HardwareSetForm(mainForm, iniPath, hName);
					hsForm.ShowDialog();
				}
				else
				{
					MessageBox.Show("请选择正确的配置文件名");
					return;
				}
			}
		}

		private void newButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			this.mainForm.Activate();
			HardwareSetForm hsForm = new HardwareSetForm(mainForm, null, null);
			hsForm.ShowDialog();			
		}
	}
}
