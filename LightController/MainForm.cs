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
using System.Collections;
using LightEditor;

namespace LightController
{
	public partial class MainForm : Form
	{
		// 只能有一个lightsForm，在点击编辑灯具时（未生成过或已被销毁）新建，或在Hide时显示
		private LightsForm lightsForm; 
		private List<LightAst> lightAstList;

		// 辅助的变量
		// private string projectName; 
		// 点击新建后，点击保存前，这个属性是true；如果是使用打开文件或已经点击了保存按钮，则设为false
		// private bool isNew = true;
		// 点击保存后|刚打开一个文件时，这个属性就设为true;如果对内容稍有变动，则设为false
		private bool isSaved = false;


			   
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

			comboBox1.SelectedIndex = 0;

			vScrollBars[0] = vScrollBar1;
			vScrollBars[1] = vScrollBar2;
			vScrollBars[2] = vScrollBar3;
			vScrollBars[3] = vScrollBar4;
			vScrollBars[4] = vScrollBar5;
			vScrollBars[5] = vScrollBar6;
			vScrollBars[6] = vScrollBar7;
			vScrollBars[7] = vScrollBar8;
			vScrollBars[8] = vScrollBar9;
			vScrollBars[9] = vScrollBar10;
			vScrollBars[10] = vScrollBar11;
			vScrollBars[11] = vScrollBar12;
			vScrollBars[12] = vScrollBar13;
			vScrollBars[13] = vScrollBar14;
			vScrollBars[14] = vScrollBar15;
			vScrollBars[15] = vScrollBar16;
			vScrollBars[16] = vScrollBar17;
			vScrollBars[17] = vScrollBar18;
			vScrollBars[18] = vScrollBar19;
			vScrollBars[19] = vScrollBar20;
			vScrollBars[20] = vScrollBar21;
			vScrollBars[21] = vScrollBar22;
			vScrollBars[22] = vScrollBar23;
			vScrollBars[23] = vScrollBar24;
			vScrollBars[24] = vScrollBar25;
			vScrollBars[25] = vScrollBar26;
			vScrollBars[26] = vScrollBar27;
			vScrollBars[27] = vScrollBar28;
			vScrollBars[28] = vScrollBar29;
			vScrollBars[29] = vScrollBar30;
			vScrollBars[30] = vScrollBar31;
			vScrollBars[31] = vScrollBar32;

			valueLabels[0] = valueLabel1;
			valueLabels[1] = valueLabel2;
			valueLabels[2] = valueLabel3;
			valueLabels[3] = valueLabel4;
			valueLabels[4] = valueLabel5;
			valueLabels[5] = valueLabel6;
			valueLabels[6] = valueLabel7;
			valueLabels[7] = valueLabel8;
			valueLabels[8] = valueLabel9;
			valueLabels[9] = valueLabel10;
			valueLabels[10] = valueLabel11;
			valueLabels[11] = valueLabel12;
			valueLabels[12] = valueLabel13;
			valueLabels[13] = valueLabel14;
			valueLabels[14] = valueLabel15;
			valueLabels[15] = valueLabel16;
			valueLabels[16] = valueLabel17;
			valueLabels[17] = valueLabel18;
			valueLabels[18] = valueLabel19;
			valueLabels[19] = valueLabel20;
			valueLabels[20] = valueLabel21;
			valueLabels[21] = valueLabel22;
			valueLabels[22] = valueLabel23;
			valueLabels[23] = valueLabel24;
			valueLabels[24] = valueLabel25;
			valueLabels[25] = valueLabel26;
			valueLabels[26] = valueLabel27;
			valueLabels[27] = valueLabel28;
			valueLabels[28] = valueLabel29;
			valueLabels[29] = valueLabel30;
			valueLabels[30] = valueLabel31;
			valueLabels[31] = valueLabel32;

			labels[0] = label1;
			labels[1] = label2;
			labels[2] = label3;
			labels[3] = label4;
			labels[4] = label5;
			labels[5] = label6;
			labels[6] = label7;
			labels[7] = label8;
			labels[8] = label9;
			labels[9] = label10;
			labels[10] = label11;
			labels[11] = label12;
			labels[12] = label13;
			labels[13] = label14;
			labels[14] = label15;
			labels[15] = label16;
			labels[16] = label17;
			labels[17] = label18;
			labels[18] = label19;
			labels[19] = label20;
			labels[20] = label21;
			labels[21] = label22;
			labels[22] = label23;
			labels[23] = label24;
			labels[24] = label25;
			labels[25] = label26;
			labels[26] = label27;
			labels[27] = label28;
			labels[28] = label29;
			labels[29] = label30;
			labels[30] = label31;
			labels[31] = label32;
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

		// 新建工程：新建一个NewForm，并ShowDialog
		private void newButton_Click(object sender, EventArgs e)
		{
			NewForm newForm = new NewForm(this);
			newForm.ShowDialog();
			
		}

		// 保存需要进行的操作：
		// 3.将lightAstList添加到light表中
		// 4.将步数、素材、value表的数据都填进各自的表中
		private void saveButton_Click(object sender, EventArgs e)
		{
			
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
			if (lightsListView.SelectedIndices.Count > 0) {
				int lightIndex = lightsListView.SelectedIndices[0];
				LightAst light = lightAstList[lightIndex];
				generateLightData(light);								
			}
		}

		// 5.24 此方法用于生成light通道的数据；
		// 1.通过lightAst的某些数据，来读取数据库中是否有相关记录；
		// 2.若有则使用*.ini的通道设置+数据库的数据; 
		// 3.若无则只需载入*.ini的相关数据
		private void generateLightData(LightAst light)
		{
			this.tongdaoGroupBox.Show();			
						
			FileStream file = new FileStream(light.LightPath, FileMode.Open);
			// 可指定编码，默认的用Default，它会读取系统的编码（ANSI-->针对不同地区的系统使用不同编码，中文就是GBK）
			StreamReader reader = new StreamReader(file, Encoding.UTF8);
			string s = "";
			ArrayList lineList = new ArrayList();
			int lineCount = 0;
			while ((s = reader.ReadLine()) != null)
			{
				lineList.Add(s);
				lineCount++;
			}			
			if (lineCount < 5)
			{
				MessageBox.Show("打开的ini文件格式有误");
				return;
			}

			//this.isGenerated = true;
			//this.isSaved = true;

			//this.typeTextBox.Enabled = false;
			//this.typeTextBox.Text = lineList[1].ToString().Substring(5);
			//this.picTextBox.Enabled = false;
			//this.picTextBox.Text = lineList[2].ToString().Substring(4);
			//string selectItem = lineList[3].ToString().Substring(6);//第七个字符开始截取 
			//														// 此处请注意：并不是用SelectedText，而是直接设Text
			//this.countComboBox.Text = selectItem;
			//this.tongdaoCount = int.Parse(selectItem);
			//this.nameTextBox.Enabled = false;
			//this.nameTextBox.Text = lineList[4].ToString().Substring(5);
			//this.editGroupBox.Show();


			if (lineCount > 5)
			{
				int tongdaoCount2 = (lineCount - 6) / 3;
				//if (tongdaoCount2 != tongdaoCount)
				//{
				//	MessageBox.Show("打开的ini文件的count值与实际值不符合");
				//}
				DataWrapper[] dataWrappers = new DataWrapper[tongdaoCount2];
				for (int i = 0; i < tongdaoCount2; i++)
				{
					string tongdaoName = lineList[3 * i + 6].ToString().Substring(4);
					int initNum = int.Parse(lineList[3 * i + 7].ToString().Substring(4));
					int address = int.Parse(lineList[3 * i + 8].ToString().Substring(4));					
					dataWrappers[i] = new DataWrapper(tongdaoName, initNum, address);
				}
				this.ShowVScrollBars(dataWrappers);
			}
			file.Close();
		}

		private void ShowVScrollBars(DataWrapper[] dataWrappers) {
						
			Console.WriteLine(dataWrappers.Length.ToString());
			// 1.每次更换灯具，都先清空通道
			for (int i = dataWrappers.Length; i < 32; i++)
			{
				vScrollBars[i].Visible = false;
				valueLabels[i].Visible = false; 
				labels[i].Visible = false;
			}

			// 2.将dataWrappers的内容渲染到起VScrollBar中
			for (int i = 0; i < dataWrappers.Length; i++)
			{
				DataWrapper dataWrapper = dataWrappers[i];
				this.labels[i].Text = dataWrapper.TongdaoName;
				this.valueLabels[i].Text = dataWrapper.InitNum.ToString();
				this.vScrollBars[i].Value = dataWrapper.InitNum;

				vScrollBars[i].Show();
				labels[i].Show();
				valueLabels[i].Show();
			}
		}



		
	}
}
