using LightController.Common;
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
	public partial class SKForm : Form
	{
		private MainFormBase mainForm;
		private int frame;
		private IniFileAst iniAst;
		private decimal eachStepTime2 = .03m;

		public SKForm(MainFormBase mainForm,int frame,string frameName)
		{
			this.mainForm = mainForm;
			this.frame = frame;

			InitializeComponent();

			// 初始化iniPath，并读取数据填入各框中
			iniAst = new IniFileAst(mainForm.GlobalIniPath);		

			// 添加时间因子，用以显示实际步时间（单位s）
			eachStepTime2 = iniAst.ReadInt("Set", "EachStepTime", 30) / 1000m;

			//添加frameStepTimeNumericUpDown相关初始化及监听事件
			frameStepTimeNumericUpDown.Value = iniAst.ReadInt("SK", frame + "ST", 0) * eachStepTime2;
			frameStepTimeNumericUpDown.Increment = eachStepTime2;
			frameStepTimeNumericUpDown.Maximum = MainFormBase.MAX_StTimes * eachStepTime2;
			frameStepTimeNumericUpDown.MouseWheel += new MouseEventHandler(this.frameStepTimeNumericUpDown_MouseWheel);

			//其他几个动态属性
			jgtNumericUpDown.Value = iniAst.ReadInt("SK", frame + "JG", 0);
			mFrameLKTextBox.Text = iniAst.ReadString("SK", frame + "LK", "");
			frameLabel.Text = frameName;
		}

		/// <summary>
		/// 事件：调整窗口初始位置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SKForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 200, mainForm.Location.Y + 200);
		}

		/// <summary>
		///  事件：点击《保存》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mFrameSaveSkinButton_Click(object sender, EventArgs e)
		{
			iniAst.WriteInt("SK", frame + "ST", frameStepTimeNumericUpDown.Value / eachStepTime2);
			iniAst.WriteInt("SK", frame + "JG", jgtNumericUpDown.Value);
			iniAst.WriteString("SK", frame + "LK", mFrameLKTextBox.Text);
			MessageBox.Show("设置保存成功");
		}

		/// <summary>
		///  事件：点击《取消》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelSkinButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：点击《右上角关闭》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SKForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：《frameStepTimeNumericUpDown》的鼠标滚动事件（只+/-1）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frameStepTimeNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			if (e.Delta > 0)
			{
				decimal dd = frameStepTimeNumericUpDown.Value + frameStepTimeNumericUpDown.Increment;
				if (dd <= frameStepTimeNumericUpDown.Maximum)
				{
					frameStepTimeNumericUpDown.Value = dd;
				}
			}
			else if (e.Delta < 0)
			{
				decimal dd = frameStepTimeNumericUpDown.Value - frameStepTimeNumericUpDown.Increment;
				if (dd >= frameStepTimeNumericUpDown.Minimum)
				{
					frameStepTimeNumericUpDown.Value = dd;
				}
			}
		}

		/// <summary>
		/// 事件：《frameStepTimeNumericUpDown》值发生变化后，进行相关验证
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frameStepTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int stepTime = Decimal.ToInt32(frameStepTimeNumericUpDown.Value / eachStepTime2);
			frameStepTimeNumericUpDown.Value = stepTime * eachStepTime2;
		}

		/// <summary>
		/// 事件：键盘按键点击事件:确保textBox内只能是0-9、及回退键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mFrameLKTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8)
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}
	}
}
