using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.Multiplex
{
	public partial class SoundMultiForm : Form
	{
		private MainFormBase mainForm;

		public SoundMultiForm(MainFormBase mainForm, int lightIndex, int tdIndex, string tdName, IList<Ast.StepWrapper> stepWrapperList)
		{
			this.mainForm = mainForm;

			InitializeComponent();

			tdLabelDemo.Text = tdName;
			comboBoxDemo.SelectedIndex = 0;

			for (int stepIndex = 0; stepIndex < stepWrapperList.Count; stepIndex++)
			{
				Panel stepPanel = new Panel
				{
					Name = "stepPanel" + stepIndex,
					BorderStyle = stepPanelDemo.BorderStyle,
					Size = stepPanelDemo.Size
				};

				Label stepLabel = new Label
				{
					Name = "stepLabel" + stepIndex,
					Text = "第" + (stepIndex + 1) + "步",
					AutoSize = stepLabelDemo.AutoSize,
					ForeColor = stepLabelDemo.ForeColor,
					Location = stepLabelDemo.Location,
					Size = stepLabelDemo.Size
				};

				Button topButton = new Button
				{
					Name = "topButton" + stepIndex,
					Location = topButtonDemo.Location,
					Size = topButtonDemo.Size,
					UseVisualStyleBackColor = true,
					Text = "↑"
				};


				Button bottomButton = new Button
				{
					Name = "topButton" + stepIndex,
					Location = bottomButtonDemo.Location,
					Size = bottomButtonDemo.Size,
					UseVisualStyleBackColor = true,
					Text = "↓"
				};

				NumericUpDown stepNUD = new NumericUpDown
				{
					Name = "stepNUD" + stepIndex,
					Location = stepNUDDemo.Location,
					Size = stepNUDDemo.Size,
					TextAlign = stepNUDDemo.TextAlign,
					Increment = stepNUDDemo.Increment,
					Maximum = stepNUDDemo.Maximum,
					Value = stepWrapperList[stepIndex].TongdaoList[tdIndex].ScrollValue,
				};

				stepPanel.Controls.Add(stepLabel);
				stepPanel.Controls.Add(topButton);
				stepPanel.Controls.Add(bottomButton);
				stepPanel.Controls.Add(stepNUD);

				stepFlowLayoutPanelDemo.Controls.Add(stepPanel);

				topButton.Click += topButton_Click;
				bottomButton.Click += bottomButton_Click;
				stepNUD.MouseWheel += someNUD_MouseWheel;
				stepNUD.ValueChanged += StepNUD_ValueChanged;
			}

		}


		/// <summary>
		///  Load方法内设定窗口位置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SoundMultiForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
		}

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
					nud.Value = decimal.ToInt32(dd);
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = nud.Value - nud.Increment;
				if (dd >= nud.Minimum)
				{
					nud.Value = decimal.ToInt32(dd);
				}
			}
		}

		/// <summary>
		/// 事件：点击《↑》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void topButton_Click(object sender, EventArgs e)
		{
			((sender as Button).Parent.Controls[3] as NumericUpDown).Value = 255;
		}

		/// <summary>
		/// 事件：点击《↓》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bottomButton_Click(object sender, EventArgs e)
		{
			((sender as Button).Parent.Controls[3] as NumericUpDown).Value = 0;
		}

		/// <summary>
		/// 事件：stepNUD的值发生变化，则更改mainForm内相应数据的值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StepNUD_ValueChanged(object sender, EventArgs e)
		{
			
		}

		/// <summary>
		/// 事件：点击《↑（统一设为255）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyTopButtonDemo_Click(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// 事件：点击《↑（统一设为0）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyBottomButtonDemo_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 事件：点击《(统一)设置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyButton_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 辅助方法：把相应的步数设为
		/// </summary>
		/// <param name="unifyValue"></param>
		private void setUnifyValue(int unifyValue  ) {




		}

		
	}
}
