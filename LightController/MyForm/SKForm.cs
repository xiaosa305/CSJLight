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
		private MainFormInterface mainForm;
		private int frame;
		private IniFileAst iniAst;
		private int eachStepTime = 30;

		public SKForm(MainFormInterface mainForm, string iniPath,int frame,string frameName)
		{
			this.mainForm = mainForm;
			this.frame = frame;

			InitializeComponent();

			// 初始化iniPath，并读取数据填入各框中
			iniAst = new IniFileAst(iniPath);
			frameStepTimeNumericUpDown.Value = iniAst.ReadInt("SK", frame + "ST", 0);
			jgtNumericUpDown.Value = iniAst.ReadInt("SK", frame + "JG", 0);
			mFrameLKTextBox.Text = iniAst.ReadString("SK", frame + "LK", "");		
			frameLabel.Text = frameName;			

			// 9.7 添加时间因子，用以显示实际步时间（单位s）；不需要直接写trueSTLabel，会自动触发ValueChanged事件
			eachStepTime = iniAst.ReadInt("Set", "EachStepTime", 30);
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


		// 事件：点击《保存》
		private void mFrameSaveSkinButton_Click(object sender, EventArgs e)
		{
			iniAst.WriteInt("SK", frame + "ST", frameStepTimeNumericUpDown.Value);
			iniAst.WriteInt("SK", frame + "JG", jgtNumericUpDown.Value);
			iniAst.WriteString("SK", frame + "LK", mFrameLKTextBox.Text);
			MessageBox.Show("设置保存成功");
		}

		// 事件：点击《取消》
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

		private void frameStepTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			trueSTLabel.Text = Decimal.ToInt16(frameStepTimeNumericUpDown.Value) * eachStepTime / 1000.0 + "s";
		}
	}
}
