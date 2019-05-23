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
				comboBox1.Items.Add(com);
			}

		}

		private void button1_Click(object sender, EventArgs e)
		{
			//TODO：打开串口，并将剩下几个按钮设成enable
			//MessageBox.Show(button1.Enabled.ToString());
			newFileButton.Enabled = true;
			openFileButton.Enabled = true;
				

		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			//MessageBox.Show(comboBox1.SelectedItem.ToString()); 
			if (comboBox1.SelectedItem.ToString() != "") {
				openComButton.Enabled = true;
			}


		}

		private void button6_Click(object sender, EventArgs e)
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
			//String s = Interaction.InputBox("请输入工程名", "新建", "",-1,-1);
			//if (!String.IsNullOrEmpty(s))
			//{
			//	MessageBox.Show(s);
			//}

			// 学习使用一个新的form

		}

		private LightsForm lightsForm;
		private void lightEditButton_Click(object sender, EventArgs e)
		{
			


			lightsForm = new LightsForm();
			lightsForm.Show();
		}
	}
}
