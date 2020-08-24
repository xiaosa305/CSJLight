using LightController.Ast;
using LightController.MyForm.Multiplex;
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
	public partial class MultiStepForm : Form
	{
		private MainFormBase mainForm;
		private int currentStep; // 当前步
		private int totalStep ;  // 最大步数
		private StepWrapper stepTemplate; //传入模板步，用以提取通道名列表
		private int mode;
		private int startStep = 1, endStep =1;
		private IList<int> tdIndexList = new List<int>();

		private int lightIndex;
		private IList<StepWrapper> stepWrapperList;

		public MultiStepForm(MainFormBase mainForm, int currentStep,int totalStep,StepWrapper stepTemplate, int mode, int lightIndex, IList<StepWrapper> stepWrapperList)
		{
			this.mainForm = mainForm;
			this.currentStep = currentStep;
			this.totalStep = totalStep;
			this.stepTemplate = stepTemplate;			
			this.mode = mode;

			this.lightIndex = lightIndex;
			this.stepWrapperList = stepWrapperList;

			InitializeComponent();
			Text += "【" + mainForm.LightAstList[lightIndex].LightType + ":" + mainForm.LightAstList[lightIndex].LightAddr  + "】";

			#region 初始化自定义数组等

			tdCheckBoxes[0] = checkBox1;
			tdCheckBoxes[1] = checkBox2;
			tdCheckBoxes[2] = checkBox3;
			tdCheckBoxes[3] = checkBox4;
			tdCheckBoxes[4] = checkBox5;
			tdCheckBoxes[5] = checkBox6;
			tdCheckBoxes[6] = checkBox7;
			tdCheckBoxes[7] = checkBox8;
			tdCheckBoxes[8] = checkBox9;
			tdCheckBoxes[9] = checkBox10;
			tdCheckBoxes[10] = checkBox11;
			tdCheckBoxes[11] = checkBox12;
			tdCheckBoxes[12] = checkBox13;
			tdCheckBoxes[13] = checkBox14;
			tdCheckBoxes[14] = checkBox15;
			tdCheckBoxes[15] = checkBox16;
			tdCheckBoxes[16] = checkBox17;
			tdCheckBoxes[17] = checkBox18;
			tdCheckBoxes[18] = checkBox19;
			tdCheckBoxes[19] = checkBox20;
			tdCheckBoxes[20] = checkBox21;
			tdCheckBoxes[21] = checkBox22;
			tdCheckBoxes[22] = checkBox23;
			tdCheckBoxes[23] = checkBox24;
			tdCheckBoxes[24] = checkBox25;
			tdCheckBoxes[25] = checkBox26;
			tdCheckBoxes[26] = checkBox27;
			tdCheckBoxes[27] = checkBox28;
			tdCheckBoxes[28] = checkBox29;
			tdCheckBoxes[29] = checkBox30;
			tdCheckBoxes[30] = checkBox31;
			tdCheckBoxes[31] = checkBox32;

			for (int i = 0; i < stepTemplate.TongdaoList.Count; i++)
			{
				TongdaoWrapper td = stepTemplate.TongdaoList[i];
				tdCheckBoxes[i].Text = td.Address + ":" + td.TongdaoName;
				tdCheckBoxes[i].Show();
			}

			#endregion

			startNumericUpDown.Maximum = totalStep;
			startNumericUpDown.Value = currentStep;
			endNumericUpDown.Maximum = totalStep;
			endNumericUpDown.Value = totalStep;		

			if (mode == 0)
			{
				modeLabel.Text = "当前模式：常规模式";
				this.unifyCmComboBox.Items.Add("跳变");
				this.unifyCmComboBox.Items.Add("渐变");
				this.unifyCmComboBox.Items.Add("屏蔽");
				this.unifyCmComboBox.SelectedIndex = 0;

				this.unifyStNumericUpDown.MouseWheel += new MouseEventHandler(this.unifyStepTimeNumericUpDown_MouseWheel);
				this.unifyStNumericUpDown.Maximum = MainFormBase.MAX_StTimes * mainForm.EachStepTime2;
				this.unifyStNumericUpDown.Increment = mainForm.EachStepTime2;				
			}
			else {
				modeLabel.Text = "当前模式：音频模式";
				this.unifyCmButton.Text =  "统一声控";
				this.unifyCmComboBox.Items.Add("屏蔽");
				this.unifyCmComboBox.Items.Add("跳变");
				//this.commonChangeModeComboBox.Items.Add("渐变");
				this.unifyCmComboBox.SelectedIndex = 0;
				// 音频模式下：《步时间调整值》几个按钮和秒(Label)隐藏。
				unifyStNumericUpDown.Hide();
				unifyStButton.Hide();
                sLabel.Visible = false;
			}


		}

		private void MultiStepForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 200, mainForm.Location.Y + 200);
			//Location = MousePosition;
		}
		
		/// <summary>
		/// 事件：点击《右上角关闭(X)》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MultiStepForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}
		
		/// <summary>
		/// 事件：勾选《（通道）全选》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void selectAllCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < stepTemplate.TongdaoList.Count; i++)
			{
				tdCheckBoxes[i].Checked = selectAllCheckBox.Checked;
			}
		}

		/// <summary>
		/// 事件：点击《全部屏蔽》;
		/// 需按照不同的mode进行处理，mode=0，,设为2；mode=1时，设为0
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ignoreSkinButton_Click(object sender, EventArgs e)
		{
			if (checkStepAndTds())
			{
				if (mode == 0)
				{
					mainForm.SetMultiStepValues(MainFormBase.WHERE.CHANGE_MODE, tdIndexList, startStep, endStep, 2);
				}
				else {
					mainForm.SetMultiStepValues(MainFormBase.WHERE.CHANGE_MODE, tdIndexList, startStep, endStep, 0);
				}				
			}
		}

		/// <summary>
		/// 事件：点击《通道值归零》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zeroSkinButton_Click(object sender, EventArgs e)
		{
			if (checkStepAndTds())
			{
				mainForm.SetMultiStepValues(MainFormBase.WHERE.SCROLL_VALUE, tdIndexList, startStep, endStep, 0);
			}
		}

		/// <summary>
		/// 事件：点击《统一通道值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyValueButton_Click(object sender, EventArgs e)
		{
			//通过了验证，才能继续运行核心代码
			if (checkStepAndTds()) {
				int commonValue = Decimal.ToInt32(commonValueNumericUpDown.Value);
				mainForm.SetMultiStepValues(MainFormBase.WHERE.SCROLL_VALUE,  tdIndexList, startStep,  endStep,  commonValue);
			}
		}

		/// <summary>
		/// 事件：点击《统一跳渐变|统一声控》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyCmButton_Click(object sender, EventArgs e)
		{
			if (checkStepAndTds())
			{
				int  commonChangeModeSelectedIndex = unifyCmComboBox.SelectedIndex;
				mainForm.SetMultiStepValues(MainFormBase.WHERE.CHANGE_MODE, tdIndexList, startStep, endStep, commonChangeModeSelectedIndex);
			}
		}

		/// <summary>
		/// 事件：点击《统一步时间》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyStButton_Click(object sender, EventArgs e)
		{
			if (checkStepAndTds())
			{
				int unifyStepTime = decimal.ToInt32(unifyStNumericUpDown.Value / mainForm.EachStepTime2);
				mainForm.SetMultiStepValues(MainFormBase.WHERE.STEP_TIME, tdIndexList, startStep, endStep, unifyStepTime);
			}
		}

		/// <summary>
		/// 事件：点击《（右侧步数）全选》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allStepSkinButton_Click(object sender, EventArgs e)
		{
			startNumericUpDown.Value = 1;
			endNumericUpDown.Value = endNumericUpDown.Maximum;
		}

		/// <summary>
		/// 辅助方法：
		/// 1. 测试起始步和结束步是否冲突；
		/// 2.测试通道是否选择通道，若未选中任何通道，则更改无意义
		/// </summary>
		private bool checkStepAndTds()
		{
			startStep = decimal.ToInt32(startNumericUpDown.Value);
			endStep = decimal.ToInt32(endNumericUpDown.Value);

			if (endStep < startStep)
			{
				MessageBox.Show("结束步不可大于起始步");
				endNumericUpDown.Value = startStep;
				return false;
			}
			else {
				tdIndexList.Clear();
				for (int i = 0; i < 32; i++)
				{
					if (tdCheckBoxes[i].Checked) {
						tdIndexList.Add(i);
					}
				}
				if (tdIndexList.Count == 0) {
					MessageBox.Show("请选择至少一个通道。");
					return false;
				}
				else
				{
					return true;
				}				
			}			
		}

		/// <summary>
		/// 事件：《统一设置步时间numericUpDown》的鼠标滚动事件（只+/-1）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyStepTimeNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			if (e.Delta > 0)
			{
				decimal dd = unifyStNumericUpDown.Value + unifyStNumericUpDown.Increment;
				if (dd <= unifyStNumericUpDown.Maximum)
				{
					unifyStNumericUpDown.Value = dd;
				}
			}
			else if (e.Delta < 0)
			{
				decimal dd = unifyStNumericUpDown.Value - unifyStNumericUpDown.Increment;
				if (dd >= unifyStNumericUpDown.Minimum)
				{
					unifyStNumericUpDown.Value = dd;
				}
			}
		}
			
		/// <summary>
		/// 事件：《统一设置步时间numericUpDown》值被用户主动变化时，需要验证，并主动设置值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyStNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int stepTime = Decimal.ToInt32(unifyStNumericUpDown.Value / mainForm.EachStepTime2);
			unifyStNumericUpDown.Value = stepTime * mainForm.EachStepTime2;
		}
		
		/// <summary>
		/// 事件：点击《多步联调》(以勾选的通道进行多步联调)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void detailMultiButton_Click(object sender, EventArgs e)
		{
			if( checkStepAndTds()){

				Dispose();
				mainForm.Activate();

				new DetailMultiForm(mainForm, lightIndex, tdIndexList, stepWrapperList).ShowDialog();
			}
		}

	}
}
