using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LightController
{
	public partial class LightsForm : Form
	{
		public LightsForm()
		{
			InitializeComponent();
		}

		private void LightsForm_Load(object sender, EventArgs e)
		{
			//TreeNode treeNode = new TreeNode();
			string path = @"C:\Temp\LightLibrary";
			if (Directory.Exists(path)) {
				
				string[] dirs = Directory.GetDirectories(path);


				foreach (string dir in dirs)
				{
					DirectoryInfo di = new DirectoryInfo(dir);
					//Console.WriteLine(di.Name);
					TreeNode treeNode = new TreeNode(di.Name);
					
					//TODO 由ini文件生成子节点
					FileInfo[] files = di.GetFiles();
					foreach (FileInfo file in files)
					{
						string fileName = file.Name;
						if (fileName.EndsWith(".ini")) {
							TreeNode node = new TreeNode(
								fileName.Substring( 0,  fileName.IndexOf(".") )
							);
							treeNode.Nodes.Add(node);
						}
					}
					this.treeView1.Nodes.Add(treeNode);
				}
				treeView1.ExpandAll();				
			}
		}

		private LightsAstForm lightsAstForm;
		private void button2_Click(object sender, EventArgs e)
		{
			if (treeView1.SelectedNode == null)
			{
				MessageBox.Show("请先选择灯具！");
			}
			else {
				string fullPath = @"C:\Temp\LightLibrary\" + treeView1.SelectedNode.FullPath + ".ini";
				LightsAstForm  lightsAstForm = new LightsAstForm(this,fullPath,10);
				lightsAstForm.ShowDialog();				

				//MessageBox.Show(fullPath);
			}
			
		}

		internal void AddListView(string lightName, string lightType, string lightAddr)
		{
			this.lightsListView.BeginUpdate();

			ListViewItem item = new ListViewItem(lightAddr);
			item.SubItems.Add(lightName);
			item.SubItems.Add(lightType);
			//Random rd = new Random();
			//item.ImageIndex = rd.Next(0, 6);
			item.ImageIndex = 1;


			lightsListView.Items.Add(item);
			

			this.lightsListView.EndUpdate();

			this.lightsListView.View = System.Windows.Forms.View.Details;

			Console.WriteLine("SIZE : " + lightsListView.Items.Count);
		}

		private void lightsListView_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void LargeIconButton_Click(object sender, EventArgs e)
		{
			lightsListView.View = View.LargeIcon;
		}

		private void smallIconButton_Click(object sender, EventArgs e)
		{
			lightsListView.View = View.SmallIcon;
		}
	}
}
