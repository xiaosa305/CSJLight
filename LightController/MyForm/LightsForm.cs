using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LightController.Ast;

namespace LightController
{
	public partial class LightsForm : System.Windows.Forms.Form
	{
		private MainForm mainForm;		
		//每次new LightsAstForm的时候，需要填入的最小值；也就是当前所有灯具通道占用的最大值+1
		private int minNum = 1;
		private IList<LightAst> lightAstList = new List<LightAst>();

		/// <summary>
		///  构造函数：传mainForm值，并通过lightAstList判断是否旧项目
		/// </summary>
		/// <param name="mainForm"></param>
		/// <param name="lightAstList"></param>
		public LightsForm(MainForm mainForm,IList<LightAst> lightAstList)
		{
			InitializeComponent();
			this.mainForm = mainForm;			
			
			// 1. 生成左边的灯具列表，树状形式
			string path = @"C:\Temp\LightLibrary";
			if (Directory.Exists(path))
			{
				string[] dirs = Directory.GetDirectories(path);
				foreach (string dir in dirs)
				{
					DirectoryInfo di = new DirectoryInfo(dir);
					TreeNode treeNode = new TreeNode(di.Name);

					//由ini文件生成子节点
					FileInfo[] files = di.GetFiles();
					foreach (FileInfo file in files)
					{
						string fileName = file.Name;
						if (fileName.EndsWith(".ini"))
						{
							TreeNode node = new TreeNode(
								fileName.Substring(0, fileName.IndexOf("."))
							);
							treeNode.Nodes.Add(node);
						}
					}
					this.treeView1.Nodes.Add(treeNode);
				}
				this.treeView1.ExpandAll();				
			}
						
			// 2.只有加载旧项目（已有LightAst列表）时，才加载lightAstList到右边
			if (lightAstList != null && lightAstList.Count > 0) {
				this.lightAstList = new List<LightAst>(lightAstList);
				foreach (LightAst la in this.lightAstList)
				{
					addListViewItem(la.LightName, la.LightType, la.LightAddr, la.LightPic);
					minNum = la.EndNum + 1;
				}
			}
		}
			
		private void LightsForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
		}
			
		/// <summary>
		///  添加新灯具
		///  1.需选中左边的一个灯具（灯库），点击添加
		///  2.打开一个NewForm的新实例，在NewForm中填好参数后回调AddListView方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addLightButton_Click(object sender, EventArgs e)
		{
			if (treeView1.SelectedNode == null)
			{
				MessageBox.Show("请先选择灯具！");
			} else {
				if (treeView1.SelectedNode.Parent != null)
				{
					string fullPath = @"C:\Temp\LightLibrary\" + treeView1.SelectedNode.FullPath + ".ini";
					LightsAstForm lightsAstForm = new LightsAstForm(this, fullPath, minNum);
					lightsAstForm.ShowDialog();
				}
			}			
		}


		/// <summary>
		///  Internal方法：添加数据到ListView中；主要给NewForm回调使用；添加后minNum设成endNum
		/// </summary>
		/// <param name="lightPath"></param>
		/// <param name="lightName"></param>
		/// <param name="lightType"></param>
		/// <param name="lightAddr"></param>
		/// <param name="lightPic"></param>
		/// <param name="startNum"></param>
		/// <param name="endNum"></param>
		/// <param name="lightCount"></param>
		internal void AddListViewAndLightAst(String lightPath,string lightName, string lightType, 
					string lightAddr,string lightPic,int startNum,int endNum,int lightCount)
		{
			// 先检查lightPic：若lightPic不在imageList中，则设置默认图片
			if (!this.largeImageList.Images.ContainsKey(lightPic))
			{
				lightPic = "未知.ico";
			}

			// 新增时，1.直接往listView加数据，
			addListViewItem(lightName, lightType, lightAddr, lightPic);

			// 2.往lightAstList添加新的数据
			lightAstList.Add(new LightAst()
			{
				LightAddr = lightAddr,
				LightName = lightName,
				LightType = lightType,
				LightPic = lightPic,
				LightPath = lightPath,
				StartNum = startNum,
				EndNum = endNum,
				Count = lightCount
			});

			// 3.设置minNum的值 
			minNum = endNum + 1;
		}


		/// <summary>
		///  辅助方法：添加item到ListView中，需要一些参数
		/// </summary>
		/// <param name="lightName"></param>
		/// <param name="lightType"></param>
		/// <param name="lightAddr"></param>
		/// <param name="lightPic"></param>
		private void addListViewItem(string lightName, string lightType, string lightAddr,string lightPic)
		{
			ListViewItem item = new ListViewItem(lightName);
			item.SubItems.Add(lightType);
			item.SubItems.Add(lightAddr);
			item.ImageKey = lightPic;
			lightsListView.Items.Add(item);
		}

		/// <summary>
		/// 点击《删除灯具》的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteLightButton_Click(object sender, EventArgs e)
		{
			if (lightsListView.SelectedIndices.Count == 0) {
					MessageBox.Show("请先选择要删除的灯具");
			}
			else
			{
				int deleteIndex = lightsListView.SelectedIndices[0];
				lightsListView.Items.RemoveAt(deleteIndex);
				lightAstList.RemoveAt(deleteIndex);
			}								
		}

		/// <summary>
		/// 点击确认后，添加lightAstList到mainForm去，并进行相关操作
		/// --用此lightAstList替代mainForm中的原lightAstList，并顺便删减lightWrapperList和ListView中的灯具
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			// 1.当点击确认时，应该将所有的listViewItem 传回到mainForm里。
			mainForm.AddLightAstList(lightAstList);
			// 2.关闭窗口（ShowDialog()情况下,资源不会释放）
			this.Dispose();
		}

		/// <summary>
		///  关闭窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LightsForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
		}

	}
}
