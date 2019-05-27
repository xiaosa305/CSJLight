using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Microsoft.VisualBasic;

namespace LightController
{
	public partial class MainForm : Form
	{
		// 只能有一个lightsForm，在点击编辑灯具时（未生成过或已被销毁）新建，或在Hide时显示
		private LightsForm lightsForm; 
		private List<LightAst> lightAstList;



		public MainForm()
		{
			InitializeComponent();
			this.skinEngine1.SkinFile = Application.StartupPath + @"\Vista2_color7.ssk";
		}	

		private void Form1_Load(object sender, EventArgs e)
		{
			//TODO
			string[] comList = { "COM1", "COM2" } ;
			foreach (string com in comList) {
				comComboBox.Items.Add(com);
			}
		}

		private void openCOMbutton_Click(object sender, EventArgs e)
		{
			//TODO：打开串口，并将剩下几个按钮设成enable
			//MessageBox.Show(button1.Enabled.ToString());
			newFileButton.Enabled = true;
			openFileButton.Enabled = true;		

		}

		private void comComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//MessageBox.Show(comboBox1.SelectedItem.ToString()); 
			if (comComboBox.SelectedItem.ToString() != "") {
				openComButton.Enabled = true;
			}
		}

		private void exitButton_Click(object sender, EventArgs e)
		{
			System.Environment.Exit(0);
		}

		private void openButton_Click(object sender, EventArgs e)
		{
			openFileDialog.ShowDialog();
			
		}

		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			MessageBox.Show("成功打开文件:"+openFileDialog.FileName);	
			// 简单读取文本文件
			FileStream file = (FileStream)openFileDialog.OpenFile();	
			// 可指定编码，默认的用Default，它会读取系统的编码（ANSI-->针对不同地区的系统使用不同编码，中文就是GBK）
			StreamReader sr = new StreamReader(file,Encoding.Default);
			string fileName = file.Name;
			fileName = fileName.Substring(fileName.LastIndexOf("\\")+1);
			string s,fileText = fileName + "\n";
			while (( s = sr.ReadLine()) != null)
			{
				fileText += "\n" + s;
			}
			MessageBox.Show(fileText);

		}

		private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
		{

		}

		private void newButton_Click(object sender, EventArgs e)
		{
			////使用VB的控件写的输入对话框
			////最后两个参数一般为-1;返回的是输入文本的内容
			String s = Interaction.InputBox("请输入工程名", "新建", "", -1, -1);
			if (!String.IsNullOrEmpty(s))
			{
				MessageBox.Show(s);
			}

		}


		private void lightEditButton_Click(object sender, EventArgs e)
		{
			if (lightsForm == null || lightsForm.IsDisposed) {
				lightsForm = new LightsForm(this,lightAstList);
			}			
			lightsForm.Show();
		}

		
		internal void AddLights(List<LightAst> lightAstList)
		{
			// 1.成功编辑灯具列表后，将这个列表放到主界面来
			this.lightAstList = lightAstList; 

			// 2.旧的先删除，再将新的加入到lightAstList中；（此过程中，并没有比较的过程，直接操作）
			lightsListView.Items.Clear();
			foreach (LightAst la in this.lightAstList)
			{
				Console.WriteLine(la.LightPic);
				ListViewItem light = new ListViewItem(
					la.LightName + ":" + la.LightType
					//+"("+la.LightAddr+")"
					,la.LightPic
				);			
				lightsListView.Items.Add(light);				
			}
		}

		private void lightsListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			try {
				int lightIndex = lightsListView.SelectedIndices[0];
				LightAst light = lightAstList[lightIndex];
				generateLightData(light);								
			}catch(Exception ex)
			{
				//MessageBox.Show(ex.Message);
			}
		}

		// 5.24 此方法用于生成light通道的数据；
		// 1.通过lightAst的某些数据，来读取数据库中是否有相关记录；
		// 2.若有则使用*.ini的通道设置+数据库的数据; 
		// 3.若无则只需载入*.ini的相关数据
		private void generateLightData(LightAst light)
		{
			

		}
	}
}
