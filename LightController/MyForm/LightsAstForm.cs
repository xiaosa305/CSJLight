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

namespace LightController
{
	public partial class LightsAstForm : System.Windows.Forms.Form
	{
		public LightsForm lightsForm; // 存储一个对LightsForm的引用

		private string lightPath; // 存储一个新建的灯具的路径		
		private string lightName; // 厂商名
		private string lightType;  //灯型号
		private string lightAddr; // 地址 ： 由【初始地址 + "-" + （初始地址+通道数）】组成
		private string lightPic; //灯的图片地址
		private int lightCount;    // 灯具的通道数

		private int startNum; // 灯具的起始地址
		private int endNum; // 灯具的结束地址

		public LightsAstForm(LightsForm lightsForm, string lightPath, int startNum)
		{
			InitializeComponent();
			this.lightsForm = lightsForm;

			this.lightPath = lightPath;
			this.startCountNumericUpDown.Value = startNum;

			readFile(lightPath);

		}
		// 辅助方法：用以读取灯具的数据：必须有的 通道数 ；可选的 图片地址
		private void readFile(string lightPath)
		{
			// 配置的每一行都要写死
			string[] lines = File.ReadAllLines(lightPath);
			lightCount = int.Parse(lines[3].Substring(6));
			lightName = lines[4].Substring(5);
			lightType = lines[1].Substring(5);
			lightPic = lines[2].Substring(4);

			this.nameTypeLabel.Text = lightName + ":" + lightType;

		}

		/// <summary>
		/// 辅助方法：通过(添加灯具的索引i和startNum输入框获取的值）来计算endNum和lightAddr;
		/// </summary>
		/// <param name="i">添加灯具的索引</param>
		private void calcEndAddr(int i) {
			int tempStartNum = Decimal.ToInt16(startCountNumericUpDown.Value);
			startNum = i * lightCount + tempStartNum;
			endNum = startNum + lightCount - 1;
			lightAddr = startNum + "-" + endNum;
		}		

		/// <summary>
		///  点击确认键后，由添加的数量和开始的地址，来插入多个或一个灯具
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{	
			int addLightCount = Decimal.ToInt16(lightCountNumericUpDown.Value);
			for (int i = 0; i < addLightCount; i++)
			{
				calcEndAddr(i);
				lightsForm.AddListViewAndLightAst(
					lightPath, lightName, lightType, lightAddr, lightPic,
					startNum, endNum, lightCount);
			}
			this.Dispose();
			lightsForm.Activate();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			lightsForm.Activate();
		}

		private void LightsAstForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(lightsForm.Location.X + 100, lightsForm.Location.Y + 100);
		}
	}
}
