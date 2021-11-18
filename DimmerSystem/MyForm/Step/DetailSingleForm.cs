using LightController.Ast;
using LightController.Ast.Helper;
using LightController.Common;
using LightEditor.Ast;
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
    public partial class DetailSingleForm : UIForm
    {
		private MainFormBase mainForm;
		private int lightIndex;
		private int tdIndex;
		private bool isJumpStep = true; // 必需的一个辅助变量，统一设值时，避免重复操作；
		
		public DetailSingleForm(MainFormBase mainForm, int lightIndex, int tdIndex, IList<StepWrapper> stepWrapperList)
		{
			this.mainForm = mainForm;
			this.lightIndex = lightIndex;
			this.tdIndex = tdIndex;

			InitializeComponent();
			//Text = LanguageHelper.TranslateSentence(Text);
			Text += "【"
				+ mainForm.LightAstList[lightIndex].LightType
				+ "(" + mainForm.LightAstList[lightIndex].LightAddr + ")"
				+ ":" + stepWrapperList[0].TongdaoList[tdIndex].TongdaoCommon.TongdaoName
				+ "】";
			unifyComboBox.SelectedIndex = 0;

			// 验证有否步数
			if (stepWrapperList == null || stepWrapperList.Count == 0)
			{
				setNotice("检测到当前灯具没有步数，无法使用多步联调。", true, true);
				return;
			}
						
			// 满足多个条件，才会显示子属性的comboBox
			SAWrapper saw = mainForm.GetSeletecdLightTdSaw(lightIndex, tdIndex);
			if (saw != null && saw.SaList != null && saw.SaList.Count != 0)
			{
				foreach (SA sa in saw.SaList)
				{
					saComboBox.Items.Add(sa.SAName + "(" + sa.StartValue + ")");
				}
				saComboBox.SelectedIndex = 0;				
				saComboBox.Show();
				saComboBox.SelectedIndexChanged += saComboBox_SelectedIndexChanged;
			}

			//渲染步数列表
			for (int stepIndex = 0; stepIndex < stepWrapperList.Count; stepIndex++)
			{
				Panel stepPanel = new Panel
				{
					Name = "stepPanel" + (stepIndex + 1),
					BorderStyle = stepPanelDemo.BorderStyle,
					Size = stepPanelDemo.Size,
					Location = stepPanelDemo.Location ,
					BackColor = ColorHelper.GetBackColor(stepWrapperList[stepIndex].TongdaoList[tdIndex].ScrollValue , stepPanelDemo.BackColor),
					Tag = lightIndex,
					Margin = stepPanelDemo.Margin,
					Padding = stepPanelDemo.Padding,
				};

				Label stepLabel = new Label
				{
					Name = "stepLabel" + (stepIndex + 1),
					Text =  (stepIndex + 1).ToString(),
					AutoSize = stepLabelDemo.AutoSize,
					ForeColor = stepLabelDemo.ForeColor,
					Location = stepLabelDemo.Location,
					Size = stepLabelDemo.Size,
					Tag = lightIndex,
					Font = stepLabelDemo.Font,
				};

				UIButton topBottomButton = new UIButton
				{
					Name = "topBottomButton" + (stepIndex + 1),
					Location = topBottomButtonDemo.Location,
					Size = topBottomButtonDemo.Size,					
					Text = topBottomButtonDemo.Text,
					Tag = lightIndex,
					Cursor = topBottomButtonDemo.Cursor ,
					FillColor = topBottomButtonDemo.FillColor ,
					FillDisableColor = topBottomButtonDemo.FillDisableColor ,
					FillHoverColor = topBottomButtonDemo.FillHoverColor ,
					FillPressColor = topBottomButtonDemo.FillPressColor,
					FillSelectedColor = topBottomButtonDemo.FillSelectedColor ,
					Font = topBottomButtonDemo.Font ,
					ForeDisableColor = topBottomButtonDemo.ForeDisableColor ,
					MinimumSize = topBottomButtonDemo.MinimumSize ,
					RectColor = topBottomButtonDemo.RectColor ,
					RectDisableColor = topBottomButtonDemo.RectDisableColor ,
					RectHoverColor = topBottomButtonDemo.RectHoverColor ,
					RectPressColor = topBottomButtonDemo.RectPressColor ,
					RectSelectedColor = topBottomButtonDemo.RectSelectedColor ,
					Style = topBottomButtonDemo.Style ,
				};			

				NumericUpDown stepNUD = new NumericUpDown
				{
					Name = "stepNUD" + (stepIndex + 1),
					Location = stepNUDDemo.Location,
					Size = stepNUDDemo.Size,
					TextAlign = stepNUDDemo.TextAlign,
					Increment = stepNUDDemo.Increment,
					Maximum = stepNUDDemo.Maximum,
					Value = stepWrapperList[stepIndex].TongdaoList[tdIndex].ScrollValue,
					Tag = lightIndex,
					Font = stepNUDDemo.Font,
				};

				stepPanel.Controls.Add(stepLabel);
				stepPanel.Controls.Add(topBottomButton);
				stepPanel.Controls.Add(stepNUD);

				stepFLP.Controls.Add(stepPanel);

				topBottomButton.Click += topBottomButton_Click;
				stepNUD.MouseWheel += someNUD_MouseWheel;
				stepNUD.ValueChanged += StepNUD_ValueChanged;
				stepNUD.MouseDoubleClick += stepNUD_MouseDoubleClick;
			}
		}

		private void DetailSingleForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			LanguageHelper.InitForm(this);
		}

		/// <summary>
		/// 事件：点击每个步数的《↑↓》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void topBottomButton_Click(object sender, EventArgs e)
		{
			NumericUpDown nud = (sender as Control).Parent.Controls[2] as NumericUpDown;
			int curValue = decimal.ToInt32(nud.Value);
			nud.Value = curValue == 255 ? 0 : 255;
		}

		/// <summary>
		/// 事件：双击通道值时，将当前通道值设为左侧的统一通道值；
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void stepNUD_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (!saComboBox.Visible)
			{
				setNotice("没有子属性，双击无效。", false, true);
				return;
			}

			NumericUpDown nud = sender as NumericUpDown;
			nud.Value = StringHelper.GetInnerValue(saComboBox.Text);
			setNotice("已设为子属性值。", false, true);
		}

		/// <summary>
		/// 事件：stepNUD的值发生变化，则更改mainForm内相应数据的值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StepNUD_ValueChanged(object sender, EventArgs e)
		{			
				NumericUpDown nud = sender as NumericUpDown;
				int stepIndex = MathHelper.GetIndexNum(nud.Name, -1);
				int stepValue = decimal.ToInt32(nud.Value);
				(nud.Parent as Panel).BackColor = ColorHelper.GetBackColor(stepValue,stepPanelDemo.BackColor);

				mainForm.SetTdStepValue(lightIndex, tdIndex, stepIndex, stepValue, isJumpStep);  
		}

		/// <summary>
		/// 事件：更改saComboBox内的选中项，并将unifyStepNUD内的数字设为这个子属性对应的值（起始值）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			UIComboBox cb = sender as UIComboBox;
			int saIndex = cb.SelectedIndex;
			int saValue = mainForm.GetSeletecdLightTdSaw(lightIndex, tdIndex).SaList[saIndex].StartValue;
			unifyNUD.Value = saValue;
		}

        /// <summary>
        /// 事件：点击《(统一)设值 + ↑ + ↓》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void unifyValueButton_Click(object sender, EventArgs e)
		{
			isJumpStep = false; //统一调制时，仍然执行每个NUD的ValueChanged，但isJumpStep在此期间为false

			// 本按键对应的tdIndex; 
			UIButton btn = sender as UIButton;
			// 统一调整的位置
			int unifyPos = unifyComboBox.SelectedIndex;
			// 统一调整的数值
			int unifyValue = decimal.ToInt32(unifyNUD.Value);
			if (btn.Text == "↑")
			{
				unifyValue = 255;
			}
			else if (btn.Text == "↓")
			{
				unifyValue = 0;
			}
			// 好几个if else 语句，合成这个for语句（只有双步时，从第二步开始调整；只有“全部”时，每次步进的数量为1）
			for (int stepIndex = (unifyPos == 2 ? 1 : 0);
				stepIndex < stepFLP.Controls.Count;
				stepIndex += (unifyPos == 0 ? 1 : 2))
			{
				NumericUpDown stepNUD = stepFLP.Controls[stepIndex].Controls[2] as NumericUpDown;
				stepNUD.Value = unifyValue;
			}

			isJumpStep = true;  // 所有值设值完成后，调回来
		}
		
		#region 通用方法

		/// <summary>
		/// 验证：对某些NumericUpDown进行鼠标滚轮的验证，避免一次性滚动过多
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someNUD_MouseWheel(object sender, MouseEventArgs e)
		{
			NumericUpDown nud = sender as NumericUpDown;
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = nud.Value + nud.Increment;
				if (dd <= nud.Maximum)
				{
					nud.Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = nud.Value - nud.Increment;
				if (dd >= nud.Minimum)
				{
					nud.Value = dd;
				}
			}
		}

		/// <summary>
		/// 辅助方法：设置提醒
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="msgBoxShow"></param>
		private void setNotice(string msg, bool msgBoxShow, bool isTranslate)
		{
			if (isTranslate)
			{
				msg = LanguageHelper.TranslateSentence(msg);
			}
			myStatusLabel.Text = msg;
			if (msgBoxShow)
			{
				MessageBox.Show(msg);
			}
		}

		#endregion
	}
}
