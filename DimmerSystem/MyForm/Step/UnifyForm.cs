using LightController.Ast;
using LightController.Ast.Enum;
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

namespace LightController.MyForm.Step
{
    public partial class UnifyForm : UIForm
    {
		private MainFormBase mainForm;
		private StepWrapper stepTemplate; //传入模板步，用以提取通道名列表
		private int startStep = 1, endStep = 1;
		private List<int> tdIndexList = new List<int>();
		private int stepPos = 0; // 0：区间内全部； 1：区间内的单数步； 2：区间内的偶（双）数步

		private int lightIndex;
		private IList<StepWrapper> stepWrapperList;
		private List<CheckBox> tdCheckBoxList ;

		public UnifyForm(MainFormBase mainForm,
			int currentStep, int totalStep, StepWrapper stepTemplate, 
			int lightIndex, IList<StepWrapper> stepWrapperList)
		{
			this.mainForm = mainForm;
			this.stepTemplate = stepTemplate;

			this.lightIndex = lightIndex;
			this.stepWrapperList = stepWrapperList;

			InitializeComponent();
			//Text = LanguageHelper.TranslateSentence(Text);
			Text += "【" + mainForm.LightAstList[lightIndex].LightType + ":" + mainForm.LightAstList[lightIndex].LightAddr + "】";

			#region 初始化自定义数组等			

			tdCheckBoxList = new List<CheckBox>();

			for (int tdIndex = 0; tdIndex < stepTemplate.TongdaoList.Count; tdIndex++)
			{
				TongdaoWrapper td = stepTemplate.TongdaoList[tdIndex];
				tdCheckBoxList.Add(new CheckBox()
				{
					Text = td.TongdaoCommon.Address + ":" + td.TongdaoCommon.TongdaoName,
					Cursor = tdCheckBoxDemo.Cursor,
					Font = tdCheckBoxDemo.Font,
					ForeColor = tdCheckBoxDemo.ForeColor,
					Location = tdCheckBoxDemo.Location,
					Margin = tdCheckBoxDemo.Margin,					
					Padding = tdCheckBoxDemo.Padding,
					Size = tdCheckBoxDemo.Size,
					UseVisualStyleBackColor = tdCheckBoxDemo.UseVisualStyleBackColor
				});				
			
				tdFLP.Controls.Add(tdCheckBoxList[tdIndex]);
			}

			#endregion

			startNumericUpDown.Maximum = totalStep;
			startNumericUpDown.Value = currentStep;
			endNumericUpDown.Maximum = totalStep;
			endNumericUpDown.Value = totalStep;

			if (mainForm.CurrentMode == 0)
			{
				modeLabel.Text = "当前模式：常规模式";
				this.unifyCmComboBox.Items.Add("跳变");
				this.unifyCmComboBox.Items.Add("渐变");
				this.unifyCmComboBox.Items.Add("屏蔽");
				this.unifyCmComboBox.SelectedIndex = 0;

				this.unifyStNumericUpDown.MouseWheel += new MouseEventHandler(this.unifyStNumericUpDown_MouseWheel);
				this.unifyStNumericUpDown.Maximum = MainFormBase.MAX_StTimes * mainForm.EachStepTime;
				this.unifyStNumericUpDown.Increment = mainForm.EachStepTime;
			}
			else
			{
				modeLabel.Text = "当前模式：音频模式";				
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

		private void UnifyForm_Load(object sender, EventArgs e)
		{
			//Location = new Point(mainForm.Location.X + 200, mainForm.Location.Y + 200);
			//LanguageHelper.InitForm(this);
		}

		/// <summary>
		/// 事件：点击《右上角关闭(X)》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UnifyForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：勾选《（通道）全选》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void selectAllCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			for (int tdIndex = 0; tdIndex < stepTemplate.TongdaoList.Count; tdIndex++)
			{
				tdCheckBoxList[tdIndex].Checked = selectAllCheckBox.Checked;
			}
		}

		/// <summary>
		/// 事件：点击《统一屏蔽》;
		/// 需按照不同的mode进行处理，mode=0，,设为2；mode=1时，设为0
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ignoreButton_Click(object sender, EventArgs e)
		{
			if (checkStepAndTds())
			{
				mainForm.SetMultiStepValues(
					EnumUnifyWhere.CHANGE_MODE, 
					tdIndexList, startStep, endStep,	stepPos,
					mainForm.CurrentMode == 0 ? 2 : 0 );
			}
		}

		/// <summary>
		/// 事件：点击《统一归零》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zeroButton_Click(object sender, EventArgs e)
		{
			if (checkStepAndTds())
			{
				mainForm.SetMultiStepValues(
					EnumUnifyWhere.SCROLL_VALUE, 
					tdIndexList, startStep, endStep, stepPos, 
					0);
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
			if (checkStepAndTds())
			{
				mainForm.SetMultiStepValues(
					EnumUnifyWhere.SCROLL_VALUE,
					tdIndexList, startStep, endStep, stepPos,
					decimal.ToInt32(unifyValueNumericUpDown.Value));
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
				mainForm.SetMultiStepValues(
					EnumUnifyWhere.CHANGE_MODE, 
					tdIndexList, startStep, endStep, stepPos,
					unifyCmComboBox.SelectedIndex);
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
				mainForm.SetMultiStepValues(
					EnumUnifyWhere.STEP_TIME, 
					tdIndexList, startStep, endStep, stepPos,
					decimal.ToInt32(unifyStNumericUpDown.Value / mainForm.EachStepTime));
			}
		}

		/// <summary>
		/// 事件：点击《（右侧步数）全选》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allStepButton_Click(object sender, EventArgs e)
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
				MessageBox.Show(LanguageHelper.TranslateSentence("结束步不可大于起始步"));
				endNumericUpDown.Value = startStep;
				return false;
			}
			else
			{
				tdIndexList.Clear();
				for (int tdIndex = 0; tdIndex < tdCheckBoxList.Count; tdIndex++)
				{
					if (tdCheckBoxList[tdIndex].Checked)
					{
						tdIndexList.Add(tdIndex);
					}
				}
				if (tdIndexList.Count == 0)
				{
					MessageBox.Show(LanguageHelper.TranslateSentence("请选择至少一个通道。"));
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
		private void unifyStNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
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
			int stepTime = decimal.ToInt32(unifyStNumericUpDown.Value / mainForm.EachStepTime);
			unifyStNumericUpDown.Value = stepTime * mainForm.EachStepTime;
		}

		/// <summary>
		/// 事件：选中全、单、双步的单选框
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void stepRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rb = sender as RadioButton;
			if (rb.Name == "allRadioButton")
			{
				stepPos = 0;
			}
			else if (rb.Name == "singleRadioButton")
			{
				stepPos = 1;
			}
			else
			{
				stepPos = 2;
			}
		}
		
	}
}
