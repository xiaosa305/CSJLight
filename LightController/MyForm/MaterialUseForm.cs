using LightController.Ast;
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
	public partial class MaterialUseForm : Form
	{
		private MainFormInterface mainForm;
		private int mode;
		private string path = @"C:\Temp\LightMaterial\"; 

		public enum InsertMethod{
			INSERT,COVER
		}

		/// <summary>
		/// 构造方法：主要作用是加载已有的素材到listView中
		/// </summary>
		/// <param name="mainForm"></param>
		public MaterialUseForm(MainFormInterface mainForm,int mode)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			this.mode = mode;
			 
			path +=  mode==0?"Normal":"Sound" ;
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

		// 辅助变量，主要是是删除选中节点后，treeView1会自动选择下一个节点，但不会显示出来；故需要有一个标记位来处理这个情况
		private bool ifJustDelete = false;	
			   
		/// <summary>
		/// 删除项目功能；后期可能不保留
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteButton_Click(object sender, EventArgs e)
		{
			// 若非刚删除
			if ( !ifJustDelete ) { 
				// 1. 先取出目录path
				string projectName = treeView1.SelectedNode.Text;
				string directoryPath = "C:\\Temp\\LightMaterial\\" + projectName;
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

				// 3.删除treeView1.SelectedNode;并设置ifJustDelete属性为true，避免用户误操作
				treeView1.SelectedNode.Remove();				
				ifJustDelete = true;
			}
			else
			{
				MessageBox.Show("请选择要删除的素材");
				return;
			}

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

		/// <summary>
		/// 用NodeMouseClick事件取代AfterSelect事件：
		/// 这样就必须在视觉上无选中节点时，需重新选中某一节点才可执行下一操作。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			ifJustDelete = false;
		}
		
		/// <summary>
		///  将素材插入到主窗口的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void insertOrCoverButton_Click(object sender, EventArgs e)
		{
			// 1.先验证是否刚删除素材
			if (ifJustDelete)
			{
				MessageBox.Show("请选择正确的素材名");
				return;
			}
			// 2.验证是否为空选项
			if (treeView1.SelectedNode != null)
			{
				string materialName = treeView1.SelectedNode.Text;
				if (!String.IsNullOrEmpty(materialName))
				{
					MaterialAst materialAst = MaterialAst.GenerateMaterialAst( path +@"\" +materialName + @"\materialSet.ini");
					InsertMethod method =  ((Button)sender).Name == "insertButton" ? InsertMethod.INSERT : InsertMethod.COVER ;
					mainForm.InsertOrCoverMaterial(materialAst, method);
					this.Dispose();
					mainForm.Activate();
				}
				else
				{
					MessageBox.Show("请选择正确的素材名");
					return;
				}
			}
		}

		private void MaterialUseForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
		}

		private void MaterialUseForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}
	}
}
