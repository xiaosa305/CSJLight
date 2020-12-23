using LightController.Ast;
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
	public partial class MaterialUseForm : Form
	{
		private MainFormBase mainForm;
		private int mode;
		private string materialPath ;
		private string lightName;
		private string lightType;
		
		private string generalStr = @"\通用\";
		private string specialStr;		
	
		/// <summary>
		/// 构造方法：主要作用是加载已有的素材到listView中
		/// </summary>
		/// <param name="mainForm"></param>
		public MaterialUseForm(MainFormBase mainForm,int mode,string lightName,string lightType)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			this.mode = mode;
			this.lightName = lightName;
			this.lightType = lightType;

			materialPath = IniFileHelper.GetSavePath(Application.StartupPath) + @"\LightMaterial\" ; 
			materialPath +=  mode==0?"Normal":"Sound";

			// 添加通用的素材
			string generalPath = materialPath + generalStr;
			if (Directory.Exists(generalPath ))
			{
				TreeNode generalTreeNode = new TreeNode("通用素材");

				string[] filePaths = Directory.GetFiles( generalPath );
				foreach (string filePath in filePaths)
				{
					FileInfo file = new FileInfo(filePath);
					string fileName = file.Name;
					if (fileName.EndsWith(".ini"))
					{
						TreeNode node = new TreeNode(
							fileName.Substring(0, fileName.IndexOf("."))
						);
						generalTreeNode.Nodes.Add(node);
					}					
				}
				materialTreeView.Nodes.Add(generalTreeNode);
			}

			// 添加该灯的素材
			specialStr = @"\" + lightName + @"\" + lightType + @"\";
			string specialPath = materialPath + specialStr;
			if (Directory.Exists(specialPath))
			{
				TreeNode specialTreeNode = new TreeNode(lightName + @"\" + lightType);
				string[] filePaths = Directory.GetFiles(specialPath);
				foreach (string filePath in filePaths)
				{
					FileInfo file = new FileInfo(filePath);
					string fileName = file.Name;
					if (fileName.EndsWith(".ini"))
					{
						TreeNode node = new TreeNode(
							fileName.Substring(0, fileName.IndexOf("."))
						);
						specialTreeNode.Nodes.Add(node);
					}
				}				
				materialTreeView.Nodes.Add(specialTreeNode);				
			}
			materialTreeView.ExpandAll();

			previewButton.Visible = mainForm.IsConnected;
			previewButton.Text = mainForm.IsPreviewing ? "停止预览" : "预览素材";
		}

		private void MaterialUseForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
		}
		
		/// <summary>
		/// 事件：点击右上角《？》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MaterialUseForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show("1.点击《插入》按钮，会在当前步与下一步之间插入你所选中的素材，未涉及的通道将使用灯具初始值；\n" +
				"2.点击《覆盖》按钮，会从当前步开始覆盖相关步数，素材内未涉及通道将保留原值；若现有步数不足，会自动添加新步，未涉及的通道将使用灯具初始值;\n" +
				"3.点击《追加》按钮，会在最后一步之后插入素材，未涉及的通道将使用灯具初始值;\n" +
				"4.插入或覆盖之后的步数不能超过灯具当前模式所允许的最大步数，否则会添加失败。",
			"素材使用帮助");
			e.Cancel = true;
		}

		/// <summary>
		/// 事件：《关闭窗体》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MaterialUseForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			// 因为是退出窗体，不需要其他额外操作，直接关闭预览即可
			if( mainForm.IsConnected && mainForm.IsPreviewing ){
				mainForm.PreviewButtonClick(null);
			}
		}

		/// <summary>
		/// 事件：点击《删除》；后期可能不保留
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteButton_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("确认删除素材【" + materialTreeView.SelectedNode.FullPath + "】吗？",
				"删除素材？",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Warning) == DialogResult.Cancel)
			{
				return;
			}

			string iniPath = getIniPath();
			if (iniPath != null)
			{
				// 1.删除文件
				try
				{
					File.Delete(iniPath);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}

				// 2.删除treeView1.SelectedNode;并设置ifJustDelete属性为true，避免用户误操作
				materialTreeView.SelectedNode.Remove();
				materialTreeView.SelectedNode = null; // 需主动设置为null，才不会选到被删节点的兄弟节点；但仍会选中第一个节点（如“通用”）
				enableButtons(false);
			}
		}

		/// <summary>
		/// 事件：点击《预览素材|停止预览》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewButton_Click(object sender, EventArgs e)
		{
			if ( !mainForm.IsConnected) {
				setNotice("尚未连接设备",true);
				return;
			}

			if (mainForm.IsPreviewing)
			{
				mainForm.PreviewButtonClick(null);
				previewButton.Text = "预览素材";
				setNotice("已停止预览", false);
			}
			else {
				string iniPath = getIniPath();
				if (iniPath != null)
				{
					MaterialAst materialAst = MaterialAst.GenerateMaterialAst(iniPath);					
					mainForm.PreviewButtonClick(materialAst);
					previewButton.Text = "停止预览";
					setNotice("正在预览素材【"+materialTreeView.SelectedNode.Text+"】...", false);
				}
			}
		}

		/// <summary>
		///  事件：《插入、覆盖、追加》素材插入到主窗口的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void insertOrCoverButton_Click(object sender, EventArgs e)
		{
			string iniPath = getIniPath();
			if (iniPath != null)
			{
				MaterialAst materialAst = MaterialAst.GenerateMaterialAst(iniPath);
				InsertMethod insMethod = (InsertMethod)int.Parse(((Button)sender).Tag.ToString());
				mainForm.InsertOrCoverMaterial(materialAst, insMethod, false);
				Dispose();
				mainForm.Activate();
			}
		}
				
		/// <summary>
		///  事件：点击《取消》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 用NodeMouseClick事件取代AfterSelect事件：
		/// 这样就必须在视觉上无选中节点时，需重新选中某一节点才可执行下一操作。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void materialTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			enableButtons( e.Node.Level > 0);
		}

		/// <summary>
		/// 辅助方法：传入bool值，使能某些按键；(删除和选中素材用得到)
		/// </summary>
		/// <param name="enable"></param>
		private void enableButtons(bool enable)
		{
			deleteButton.Enabled = enable;
			previewButton.Enabled = enable;
			insertButton.Enabled = enable;
			coverButton.Enabled = enable;
			appendButton.Enabled = enable;
		}

		/// <summary>
		///  辅助方法：供删除及使用素材使用，通过此方法可以直接获取选中项的物理路径
		/// </summary>
		/// <returns></returns>
		private string getIniPath() {

				// 1.先验证是否刚删除素材 或 空选
				if( materialTreeView.SelectedNode == null )
				{
					MessageBox.Show("请选择正确的素材名");
					return null;
				}

				//2. 验证是否子节点，父节点不是素材
				if (materialTreeView.SelectedNode.Level == 0)
				{
					MessageBox.Show("请选择素材树的子节点。");
					return null;
				}

				//3.验证素材名是否为空
				string materialName = materialTreeView.SelectedNode.Text;
				if ( String.IsNullOrEmpty(materialName)) {
					MessageBox.Show("素材名不得为空。");
					return null;
				}

				string astPath = materialTreeView.SelectedNode.Parent.Text.Equals("通用素材") ? generalStr :specialStr ;
				string iniPath = materialPath + astPath + materialName + ".ini";

				return iniPath;				
		}
				
		#region 通用方法

		/// <summary>
		/// 辅助方法：设置提醒
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="msgBoxShow"></param>
		private void setNotice(string msg, bool msgBoxShow)
		{
			myStatusLabel.Text = msg;
			if (msgBoxShow)
			{
				MessageBox.Show(msg);
			}
		}


		#endregion
		
	}
}
