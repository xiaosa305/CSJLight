using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController
{
	public partial class LightsAstForm : Form
	{
		public LightsForm lightsForm; // 存储一个对LightsForm的引用

		private string lightPath; // 存储一个新建的灯具的路径		
		private string lightName; // 厂商名
		private string lightType;  //灯型号
		private string lightAddr; // 地址 ： 由【初始地址 + "-" + （初始地址+通道数）】组成
		private int lightCount;
		private int endNum;

		private int startNum; // 新建灯具的起始地址

		public LightsAstForm(LightsForm lightsForm, string lightPath,int startNum)
		{
			InitializeComponent();

			this.lightsForm = lightsForm;
			this.lightPath = lightPath;

			this.startCountNumericUpDown.Minimum = startNum;
			this.startCountNumericUpDown.Value = startNum;
			
			readFile(lightPath);

		}
		// 辅助方法：用以读取灯具的数据：必须有的 通道数 ；可选的 图片地址
		public void readFile(string lightPath) {
			string[] lines = File.ReadAllLines(lightPath);
			lightCount = int.Parse(lines[3].Substring(6));
			lightName = lines[4].Substring(5);
			lightType = lines[1].Substring(5);
			this.startNum = int.Parse(this.startCountNumericUpDown.Value.ToString());
			this.endNum = startNum + lightCount - 1;

			lightAddr = startNum + "-" + endNum;

		}


		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			try
			{
				int textBoxNum = int.Parse(textBox.Text);
				if (textBoxNum < 0)
				{
					MessageBox.Show("通道地址编号不得小于0");
					return;
				}
				
			}
			catch (Exception ex)
			{

				textBox.Text = "";
				MessageBox.Show("地址编号只能是数字");
				//TODO ：在下方提示该处只能输入数字 
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			lightsForm.AddListView(lightName,lightType,lightAddr);
			this.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Hide();
		}
	}
}
