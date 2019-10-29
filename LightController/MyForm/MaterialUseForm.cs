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
		private MainFormInterface mainForm;
		private int mode;
		private string materialPath ;
		private string lightName;
		private string lightType;
		// 辅助变量，主要是是删除选中节点后，treeView1会自动选择下一个节点，但不会显示出来；故需要有一个标记位来处理这个情况
		private bool ifJustDelete = false;

		private string generalStr = @"\通用\";
		private string specialStr;


		/// <summary>
		/// 构造方法：主要作用是加载已有的素材到listView中
		/// </summary>
		/// <param name="mainForm"></param>
		public MaterialUseForm(MainFormInterface mainForm,int mode,string lightName,string lightType)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			this.mode = mode;
			this.lightName = lightName;
			this.lightType = lightType;

			materialPath = @IniFileAst.GetSavePath(Application.StartupPath) + @"\LightMaterial\" ; 
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
				this.treeView1.Nodes.Add(generalTreeNode);
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
				this.treeView1.Nodes.Add(specialTreeNode);				
			}
			this.treeView1.ExpandAll();
		}

		private void MaterialUseForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
		}
	
		/// <summary>
		///  事件：《插入、覆盖》素材插入到主窗口的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void insertOrCoverButton_Click(object sender, EventArgs e)
		{
			string iniPath = getIniPath();
			if (iniPath != null) {
				MaterialAst materialAst = MaterialAst.GenerateMaterialAst(iniPath);
				InsertMethod method = ((Button)sender).Name == "insertSkinButton" ? InsertMethod.INSERT : InsertMethod.COVER;
				mainForm.InsertOrCoverMaterial(materialAst, method);

				this.Dispose();
				mainForm.Activate();
			}
		}

		/// <summary>
		/// 事件：点击《删除》；后期可能不保留
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteButton_Click(object sender, EventArgs e)
		{
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
				treeView1.SelectedNode.Remove();
				ifJustDelete = true;
			}
		}


		/// <summary>
		///  事件：点击《取消》后的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
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
		/// 事件：点击《右上角帮助》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void helpSkinButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("1.点击《插入》按钮，会在当前步与下一步之间插入你所选中的素材，未涉及的通道将使用灯具初始值；\n" +
				"2.点击《覆盖》按钮，会从当前步开始覆盖相关步数，素材内未涉及通道将保留原值；若现有步数不足，会自动添加新步，未涉及的通道将使用灯具初始值;\n" +
				"3.插入或覆盖之后的步数不能超过灯具当前模式所允许的最大步数，否则会添加失败。",
			"素材使用帮助");
		}

		/// <summary>
		///  辅助方法：供删除及使用素材使用，通过此方法可以直接获取选中项的物理路径
		/// </summary>
		/// <returns></returns>
		private string getIniPath() {

				// 1.先验证是否刚删除素材 或 空选
				if (ifJustDelete || treeView1.SelectedNode == null)
				{
					MessageBox.Show("请选择正确的素材名");
					return null;
				}

				//2. 验证是否子节点，父节点不是素材
				if (treeView1.SelectedNode.Level == 0)
				{
					MessageBox.Show("请选择最后一级的节点，不要选择父节点。");
					return null;
				}

				//3.验证素材名是否为空
				string materialName = treeView1.SelectedNode.Text;
				if ( String.IsNullOrEmpty(materialName)) {
					MessageBox.Show("素材名不得为空。");
					return null;
				}

				string astPath = treeView1.SelectedNode.Parent.Text.Equals("通用素材") ? generalStr :specialStr ;
				string iniPath = materialPath + astPath + materialName + ".ini";

				return iniPath;				
		}

	}
}
