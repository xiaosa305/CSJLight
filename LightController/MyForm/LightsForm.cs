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

		public LightsForm(MainForm mainForm,IList<LightAst> lightAstList)
		{
			InitializeComponent();

			this.mainForm = mainForm;
			if (lightAstList != null && lightAstList.Count > 0) {
				this.lightAstList = lightAstList; 
			}

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

			//TODO 2.载入lightAstList到右边的框中
			// 只有加载旧项目（已有LightAst列表）时，才加载lightAstList到右边
			if (lightAstList != null && lightAstList.Count > 0) {



			}

		}

		private void LightsForm_Load(object sender, EventArgs e)
		{
			
			

		}
				
		

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

		internal void AddListView(String lightPath,string lightName, string lightType, 
					string lightAddr,string lightPic,int startNum,int endNum,int lightCount)
		{
			// 新增时，1.直接往listView加数据，
			ListViewItem item = new ListViewItem(lightName);
			item.SubItems.Add(lightType);
			item.SubItems.Add(lightAddr);

			// 若lightPic不在imageList中，则设置默认图片
			if ( ! this.largeImageList.Images.ContainsKey(lightPic))
			{
				lightPic = "未知.ico";
			}
			item.ImageKey = lightPic;

			lightsListView.Items.Add(item);

			// 2.往lightAstList添加新的数据
			lightAstList.Add(new LightAst() {
				LightAddr = lightAddr,
				LightName = lightName,
				LightType = lightType,
				LightPic = lightPic,
				LightPath = lightPath,
				StartNum = startNum,
				EndNum = endNum,
				Count  = lightCount
			} );
					   
			// 3.设置minNum的值 
			minNum = endNum + 1;			
		}	

		private void deleteLightButton_Click(object sender, EventArgs e)
		{

		}

		private void enterButton_Click(object sender, EventArgs e)
		{
			// 1.当点击确认时，应该将所有的listViewItem 传回到mainForm里。
			mainForm.AddLightAstList(lightAstList);
			// 2.关闭窗口（资源还未释放）
			this.Dispose();
			mainForm.Activate();
		}

		private void lightsListView_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		

	}
}
