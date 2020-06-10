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
		private int tdCount;    // 灯具的通道数
        
		public LightsAstForm(LightsForm lightsForm, string lightPath, int startNum)
		{
			if (startNum >= LightsForm.MAX_TD) {
				MessageBox.Show("当前初始地址已经到达DMX512地址上限，请谨慎设置");
				startNum = 512;
			}

			InitializeComponent();
			this.lightsForm = lightsForm;

			this.lightPath = lightPath;
			startAddrNumericUpDown.Value = startNum;

			readFile(lightPath);
		}

        private void LightsAstForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(lightsForm.Location.X + 100, lightsForm.Location.Y + 100);
        }

        // 辅助方法：用以读取灯具的数据：必须有的 通道数 ；可选的 图片地址
        private void readFile(string lightPath)
		{
			// 配置的每一行都要写死
			string[] lines = File.ReadAllLines(lightPath);
			tdCount = int.Parse(lines[3].Substring(6));
			lightName = lines[4].Substring(5);
			lightType = lines[1].Substring(5);
			lightPic = lines[2].Substring(4);

			nameTypeLabel.Text = lightName + ":" + lightType;
		}
        
		/// <summary>
		///  点击确认键后，由添加的数量和开始的地址，来插入多个或一个灯具
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
            int firstStartAddr = decimal.ToInt16(startAddrNumericUpDown.Value);
            int addLightCount = decimal.ToInt16(lightCountNumericUpDown.Value);           
            int lastEndAddr = firstStartAddr + addLightCount * tdCount - 1;  
        
			if ( lastEndAddr > LightsForm.MAX_TD) {
				MessageBox.Show("添加灯具的最后地址超过了DMX512灯具的地址上限(512)，\n，请重新设置起始地址或灯具数量。");
				return;
			}

            if ( ! lightsForm.CheckAddrAvailale(-1, decimal.ToInt16(startAddrNumericUpDown.Value), lastEndAddr) ){
                MessageBox.Show("检测到您添加的灯具部分地址已被占用，\n请重新设置起始地址或灯具数量。");
                return; 
            }

			for (int addLightIndex = 0; addLightIndex < addLightCount; addLightIndex++)
			{              
                int startAddr = addLightIndex * tdCount + firstStartAddr;
                int endAddr = startAddr + tdCount - 1;
                lightAddr = startAddr + "-" + endAddr;

                lightsForm.AddListViewAndLightAst(
					lightPath, lightName, lightType, lightAddr, lightPic,
					startAddr, endAddr, tdCount);
			}
			Dispose();
			lightsForm.Activate();
		}

        /// <summary>
        /// 事件：关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Dispose();
			lightsForm.Activate();
		}

	}
}
