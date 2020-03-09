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
	public partial class OpenForm : Form
	{
		private MainFormInterface mainForm;
		private string currentProjectName = "";  // 辅助变量：若当前已在打开某工程状态下，不应该可以删除这个工程。此变量便于与选中工程进行比较，避免误删		
		private bool isJustDelete = false;  // 辅助变量，主要是是删除选中节点后，treeView1会自动选择下一个节点，但不会显示出来；此时为用户体验考虑，不应该可以删除，
		private string savePath;   // 辅助变量，获取软件的存储目录。
		private string selectedProjectName; // 临时变量，存储右键选中后弹出的重命名菜单

		public OpenForm(MainFormInterface mainForm, string currentProjectName)
		{
			InitializeComponent();
		
			this.mainForm = mainForm;
			this.currentProjectName = currentProjectName;
			savePath = @IniFileAst.GetSavePath(Application.StartupPath);

			RefreshTreeView1();
		}

		/// <summary>
		/// 事件：窗体载入时，定义初始位置。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
		}

		/// <summary>
		/// 辅助方法：刷新treeView1的节点列表
		/// </summary>
		internal void RefreshTreeView1()
		{
			treeView1.Nodes.Clear();
			string path = savePath + @"\LightProject";
			if (Directory.Exists(path))
			{			
				string[] dirs = Directory.GetDirectories(path);
				foreach (string dir in dirs)
				{
					DirectoryInfo di = new DirectoryInfo(dir);
					TreeNode treeNode = new TreeNode(di.Name);
					treeView1.Nodes.Add(treeNode);
				}
			}
		}

		/// <summary>
		///  事件：选中node后，点击《打开》后的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			// 1.先验证是否刚删除项目 或 空选项
			if (isJustDelete  || treeView1.SelectedNode == null)
			{
				MessageBox.Show("请选择正确的项目名");
				return;
			}

			string projectName =  treeView1.SelectedNode.Text;			
			if ( ! String.IsNullOrEmpty(projectName) )
			{
				this.Dispose();
				mainForm.Activate();
				mainForm.OpenProject(projectName);
			}
			else
			{
				MessageBox.Show("请选择正确的项目名");
				return;
			}			
		}			

		/// <summary>
		/// 事件：点击《删除》按钮（后期可能删除的功能）；
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteButton_Click(object sender, EventArgs e)
		{
			// 1.先验证是否刚删除项目
			if (isJustDelete || treeView1.SelectedNode == null) {
				MessageBox.Show("请选择要删除的工程:");
				return;
			}

			// 1. 先取出目录path
			string projectName = treeView1.SelectedNode.Text;		

			// 8.21 验证是否当前项目，若是则不可删除
			if (projectName.Equals(currentProjectName)) {
				MessageBox.Show("无法删除正在使用的工程！");
				return;
			}

			// 1. 弹出是否删除的确认框
			DialogResult dr = MessageBox.Show("确定删除此工程吗？", "删除工程", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (dr == DialogResult.Cancel)
			{
				return;
			}

			string directoryPath = savePath +  @"\LightProject\" + projectName;
			DirectoryInfo di = new DirectoryInfo(directoryPath);

			// 2.删除目录
			try
			{
				di.Delete(true);
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			// 3.删除treeView1.SelectedNode;并设置ifJustDelete属性为true，避免客户误操作
			treeView1.SelectedNode.Remove();
			isJustDelete = true;			
		}		

		/// <summary>
		///  点击《取消》按钮的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：选中某节点后，isJustDelete设为false，以便后面的操作	
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			this.isJustDelete = false;
		}
			   		 	  		
		/// <summary>
		/// 事件： 10.22 选中某个节点后，可以弹出右键菜单（不在此处过滤是否打开文件，因为复制工程可以是使用中的工程）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)//判断你点的是不是右键
			{
				Point ClickPoint = new Point(e.X, e.Y);
				TreeNode CurrentNode = treeView1.GetNodeAt(ClickPoint);
				if (CurrentNode != null)//判断你点的是不是一个节点
				{					
					treeView1.SelectedNode = CurrentNode;//选中这个节点
					selectedProjectName = treeView1.SelectedNode.Text;
					CurrentNode.ContextMenuStrip = mySkinContextMenuStrip;
				}
			}
		}

		/// <summary>
		/// 事件：点击《右键->工程重命名》 
		///  -- 弹出一个新名称窗口，输入新名称，点击确定可以重命名，并刷新当前的treeView1
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void renameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (selectedProjectName.Equals(currentProjectName))
			{
				MessageBox.Show("无法重命名当前打开的工程。");
			}
			else {
				// 这里用到了形参默认值的方法，在没有设置的情况下，copy值默认为false（重命名）
				new ProjectRenameOrCopyForm(this, selectedProjectName).ShowDialog();
			}
		}

		/// <summary>
		/// 事件：左键双击才打开工程（右键不行）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_DoubleClick(object sender, EventArgs e)
		{
			MouseEventArgs me = e as MouseEventArgs;

			if (me.Button == MouseButtons.Left) {
				enterButton_Click(null, null);
			}
		}

		/// <summary>
		///  事件：点击《右键->复制工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void copyProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new ProjectRenameOrCopyForm(this, selectedProjectName,true).ShowDialog();
		}

	}
}
