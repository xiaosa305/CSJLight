using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm
{
	public partial class UpdateForm : Form
	{
		public UpdateForm()
		{
			InitializeComponent();


		}

		/// <summary>
		/// 点击《测试按钮》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void testButton_Click(object sender, EventArgs e)
		{
			progressBar1.Maximum = 100;//设置最大长度值
			progressBar1.Value = 0;//设置当前值
			progressBar1.Step = 1;//设置没次增长多少
			for (int i = 0; i < 100; i++)//循环
			{
				System.Threading.Thread.Sleep(100);//暂停1秒
				progressBar1.Value += progressBar1.Step;  // 让进度条增加一次
			}
			if (progressBar1.Value == progressBar1.Maximum) {
				MessageBox.Show("Dickov:已完成");
			}
		}
	}
}
