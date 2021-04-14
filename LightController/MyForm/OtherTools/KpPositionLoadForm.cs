using LightController.Common;
using OtherTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.OtherTools
{
	public partial class KpPositionLoadForm : Form
	{
		private NewToolsForm otForm;
		private string kpPosPath;	
		private bool ifJustDelete = false;  // 辅助变量，主要是是删除选中节点后，treeView1会自动选择下一个节点，但不会显示出来；故需要有一个标记位来处理这个情况

		public KpPositionLoadForm(NewToolsForm otForm , int keyCount)
		{
			InitializeComponent();
			this.otForm = otForm;
			kpPosPath = IniHelper.GetSavePath() + @"\KeypressPosition\" + keyCount + @"\";
			Text = "加载按键位置(" + keyCount + "键)";

			if (Directory.Exists(kpPosPath))
			{
				string[] filePaths = Directory.GetFiles(kpPosPath);
				foreach (string filePath in filePaths)
				{
					FileInfo file = new FileInfo(filePath);
					string fileName = file.Name;
					if (fileName.EndsWith(".ini"))
					{
						TreeNode node = new TreeNode(
							fileName.Substring(0, fileName.IndexOf("."))
						);
						treeView1.Nodes.Add(node);
					}
				}				
			}
		}

		private void KpPositionLoadForm_Load(object sender, EventArgs e)
		{
			//this.Location = new Point(otForm.Location.X + 100, otForm.Location.Y + 100);
			Location = MousePosition;
		}

		/// <summary>
		///  辅助方法：供删除及使用素材使用，通过此方法可以直接获取选中项的物理路径
		/// </summary>
		/// <returns></returns>
		private string getIniPath()
		{
			// 验证是否刚删除文件 或 空选
			if (ifJustDelete || treeView1.SelectedNode == null)
			{
				MessageBox.Show("请选择正确的文件名");
				return null;
			}

			//验证文件名是否为空
			string materialName = treeView1.SelectedNode.Text;
			if (String.IsNullOrEmpty(materialName))
			{
				MessageBox.Show("文件名不得为空。");
				return null;
			}

			string astPath = treeView1.SelectedNode.Text;
			string arrangeIniPath = kpPosPath +astPath+ ".ini";

			return arrangeIniPath;
		}

		/// <summary>
		/// 事件：点击《确定》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			string arrangeIniPath = getIniPath();
			if (arrangeIniPath == null) {
				return;
			}

			if (otForm.KPLoadPosition(arrangeIniPath)) {
				this.Dispose();
				otForm.Activate();
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
			otForm.Activate();
		}

		/// <summary>
		/// 事件：删除位置文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteButton_Click(object sender, EventArgs e)
		{
			string arrangeIniPath = getIniPath();
			if (arrangeIniPath != null)
			{
				// 删除文件
				try
				{
					File.Delete(arrangeIniPath);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}

				// 删除treeView1.SelectedNode;并设置ifJustDelete属性为true，避免用户误操作
				treeView1.SelectedNode.Remove();
				ifJustDelete = true;
			}
		}
		
		/// <summary>
		/// 事件：鼠标点击《节点》-- 用以删除前的确认。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			ifJustDelete = false;
		}
	}
}
