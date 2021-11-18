using LightController.Common;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.Project
{
    public partial class SKForm : UIForm
    {
		private MainFormBase mainForm;
		private IniHelper iniHelper;

		public SKForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;

			InitializeComponent();

			// 初始化iniPath，并读取数据填入各框中
			iniHelper = new IniHelper(mainForm.GetConfigPath());

			//添加frameStepTimeNumericUpDown相关初始化及监听事件			
			sceneStepTimeNumericUpDown.Increment = mainForm.EachStepTime;
			sceneStepTimeNumericUpDown.Maximum = MainFormBase.MAX_StTimes * mainForm.EachStepTime;
			sceneStepTimeNumericUpDown.Value = iniHelper.ReadInt("SK", mainForm.CurrentScene + "ST", 0) * mainForm.EachStepTime;
			sceneStepTimeNumericUpDown.MouseWheel += sceneStepTimeNumericUpDown_MouseWheel;

			//其他几个动态属性
			jgtNumericUpDown.Value = iniHelper.ReadInt("SK", mainForm.CurrentScene + "JG", 0);
			mSceneLKTextBox.Text = iniHelper.ReadString("SK", mainForm.CurrentScene + "LK", "");
			sceneLabel.Text = MainFormBase.AllSceneList[mainForm.CurrentScene];
		}

		/// <summary>
		/// 事件：调整窗口初始位置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SKForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			//LanguageHelper.InitForm(this);
		}

		/// <summary>
		///  事件：点击《保存设置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			iniHelper.WriteInt("SK", mainForm.CurrentScene + "ST", sceneStepTimeNumericUpDown.Value / mainForm.EachStepTime);
			iniHelper.WriteInt("SK", mainForm.CurrentScene + "JG", jgtNumericUpDown.Value);
			iniHelper.WriteString("SK", mainForm.CurrentScene + "LK", mSceneLKTextBox.Text.Trim());
			MessageBox.Show(LanguageHelper.TranslateSentence("设置保存成功"));

			Dispose();
			mainForm.Activate();
		}

		/// <summary>
		///  事件：点击《取消》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelSkinButton_Click(object sender, EventArgs e)
		{
			Dispose();
			mainForm.Activate();
		}
				
		/// <summary>
		/// 事件：《sceneStepTimeNumericUpDown》的鼠标滚动事件（只+/-1）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void sceneStepTimeNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			if (e.Delta > 0)
			{
				decimal dd = sceneStepTimeNumericUpDown.Value + sceneStepTimeNumericUpDown.Increment;
				if (dd <= sceneStepTimeNumericUpDown.Maximum)
				{
					sceneStepTimeNumericUpDown.Value = dd;
				}
			}
			else if (e.Delta < 0)
			{
				decimal dd = sceneStepTimeNumericUpDown.Value - sceneStepTimeNumericUpDown.Increment;
				if (dd >= sceneStepTimeNumericUpDown.Minimum)
				{
					sceneStepTimeNumericUpDown.Value = dd;
				}
			}
		}

		/// <summary>
		/// 事件：《sceneStepTimeNumericUpDown》值发生变化后，进行相关验证（避免用户手动输入时，值不符合要求）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void sceneStepTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int stepTime = decimal.ToInt32(sceneStepTimeNumericUpDown.Value / mainForm.EachStepTime);
			sceneStepTimeNumericUpDown.Value = stepTime * mainForm.EachStepTime;
		}

		/// <summary>
		/// 事件：键盘按键点击事件:确保textBox内只能是0-9、及回退键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mSceneLKTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

		// 事件：点击提示
		private void SKForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show(LanguageHelper.TranslateSentence("请在文本框内输入每一次音频触发时执行的步数（范围为1-9），并将每步数字连在一起（如1234）；若设为0或空字符串，则表示该场景不执行声控模式；链表数量不可超过20个。"));
			e.Cancel = true;
		}

    }
}
