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
		private string lightPic; //灯的图片地址
		private int lightCount;    // 灯具的通道数

		private int startNum; // 新建灯具的起始地址
		private int endNum; // 灯具的结束地址。

		public LightsAstForm(LightsForm lightsForm, string lightPath, int startNum)
		{
			InitializeComponent();

			this.lightsForm = lightsForm;
			this.lightPath = lightPath;

			this.startCountNumericUpDown.Minimum = startNum;
			this.startCountNumericUpDown.Value = startNum;

			readFile(lightPath);

		}
		// 辅助方法：用以读取灯具的数据：必须有的 通道数 ；可选的 图片地址
		private void readFile(string lightPath) {
			string[] lines = File.ReadAllLines(lightPath);
			lightCount = int.Parse(lines[3].Substring(6));
			lightName = lines[4].Substring(5);
			lightType = lines[1].Substring(5);
			lightPic = lines[2].Substring(4);
		}

		//　辅助方法：通过startNum来计算endNum和lightAddr;
		private void calcEndAddr() {
			this.startNum = int.Parse(this.startCountNumericUpDown.Value.ToString());
			this.endNum = startNum + lightCount - 1;
			lightAddr = startNum + "-" + endNum;
		}		


		private void enterButton_Click(object sender, EventArgs e)
		{
			calcEndAddr();
			lightsForm.AddListView(lightName, lightType, lightAddr,lightPic,endNum);
			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		
	}
}
