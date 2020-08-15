using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LightController.Ast;
using LightController.Common;

namespace LightController.MyForm.Multiplex
{
	public partial class DetailMultiForm : Form
	{
		private MainFormBase mainForm;		
		private IList<int> tdIndices;		
		
		public DetailMultiForm(MainFormBase mainForm, string lightInfo,  IList<int> tdIndices,  IList<StepWrapper> stepWrapperList)
		{
			this.mainForm = mainForm;			
			this.tdIndices = tdIndices;

			InitializeComponent();			
			Text += "【"+lightInfo+ "】"; 

			// 验证有否步数
			if (stepWrapperList == null || stepWrapperList.Count == 0)
			{
				setNotice("检测到当前灯具没有步数，无法使用多步联调。", true);
				return;
			}

			// 当传入的tdIndex为-1时，表示要对所有通道多步联调
			if (tdIndices == null || tdIndices.Count == 0)
			{
				for (int tdIndex = 0; tdIndex < stepWrapperList[0].TongdaoList.Count; tdIndex++)
				{
					addTdPanel(stepWrapperList, tdIndex);
				}
			}
			else {
				foreach (int tdIndex in tdIndices)
				{
					addTdPanel(stepWrapperList, tdIndex);
				}				
			}
		}

		/// <summary>
		/// 辅助方法：通过传入的tdIndex，生成相应的tdPanel(某通道所有步数据的调节面板)
		/// </summary>
		/// <param name="stepWrapperList"></param>
		/// <param name="tdIndex"></param>
		private void addTdPanel(IList<StepWrapper> stepWrapperList , int tdIndex)
		{
			Panel tdPanel = new Panel
			{
				Name = "tdPanel" + (tdIndex + 1),
				Location = tdPanelDemo.Location,
				Size = tdPanelDemo.Size,
				BorderStyle = tdPanelDemo.BorderStyle,
			};

			Panel tdSmallPanel = new Panel
			{
				Name = "tdSmallPanel" + (tdIndex + 1),
				BackColor = tdSmallPanelDemo.BackColor,
				Dock = tdSmallPanelDemo.Dock,
				Location = tdSmallPanelDemo.Location,
				Size = tdSmallPanelDemo.Size,
				BorderStyle = tdSmallPanelDemo.BorderStyle				
			};

			FlowLayoutPanel stepFLP = new FlowLayoutPanel
			{
				Name = "stepFLP" + (tdIndex + 1),
				AutoScroll = stepFLPDemo.AutoScroll,
				Dock = stepFLPDemo.Dock,
				Location = stepFLPDemo.Location,
				Size = stepFLPDemo.Size,
				WrapContents = false,
				BackColor = stepFLPDemo.BackColor,
			};

			bigFLP.Controls.Add(tdPanel);			
			tdPanel.Controls.Add(stepFLP);
			tdPanel.Controls.Add(tdSmallPanel);

			Label tdLabel = new Label
			{
				Name = "tdLabel" + (tdIndex + 1),
				Location = tdLabelDemo.Location,
				Size = tdLabelDemo.Size,
				Text = stepWrapperList[0].TongdaoList[tdIndex].Address +" - "+  stepWrapperList[0].TongdaoList[tdIndex].TongdaoName
			};

			ComboBox unifyComboBox = new ComboBox
			{
				Name = "unifyComboBox" + (tdIndex + 1),
				DropDownStyle = unifyComboBoxDemo.DropDownStyle,
				Location = unifyComboBoxDemo.Location,
				Size = unifyComboBoxDemo.Size
			};
			unifyComboBox.Items.AddRange(new object[] { "全步", "单步", "双步" });
			unifyComboBox.SelectedIndex = 0;

			Button unifyTopButton = new Button
			{
				Name = "unifyTopButton" + (tdIndex + 1),
				Location = unifyTopButtonDemo.Location,
				Size = unifyTopButtonDemo.Size,
				Tag = unifyTopButtonDemo.Tag,
				Text = unifyTopButtonDemo.Text,
				UseVisualStyleBackColor = unifyTopButtonDemo.UseVisualStyleBackColor
			};
			unifyTopButton.Click += unifyValueButton_Click;

			Button unifyBottomButton = new Button
			{
				Name = "unifyBottomButton" + (tdIndex + 1),
				Location = unifyBottomButtonDemo.Location,
				Size = unifyBottomButtonDemo.Size,
				Tag = unifyBottomButtonDemo.Tag,
				Text = unifyBottomButtonDemo.Text,
				UseVisualStyleBackColor = unifyBottomButtonDemo.UseVisualStyleBackColor
			};
			unifyBottomButton.Click += unifyValueButton_Click;

			NumericUpDown unifyNUD = new NumericUpDown
			{
				Name = "unifyNUD" + (tdIndex + 1),
				Location = unifyNUDDemo.Location,
				Size = unifyNUDDemo.Size,
				TextAlign = unifyNUDDemo.TextAlign,
				Maximum = unifyNUDDemo.Maximum,
				Value = unifyNUDDemo.Value
			};
			unifyNUD.MouseWheel += someNUD_MouseWheel;

			Button unifyValueButton = new Button
			{
				Name = "unifyValueButton" + (tdIndex + 1),
				Location = unifyValueButtonDemo.Location,
				Size = unifyValueButtonDemo.Size,
				Text = unifyValueButtonDemo.Text,
				UseVisualStyleBackColor = unifyValueButtonDemo.UseVisualStyleBackColor
			};
			unifyValueButton.Click += new System.EventHandler(this.unifyValueButton_Click);

			tdSmallPanel.Controls.Add(tdLabel);
			tdSmallPanel.Controls.Add(unifyComboBox);
			tdSmallPanel.Controls.Add(unifyTopButton);
			tdSmallPanel.Controls.Add(unifyBottomButton);
			tdSmallPanel.Controls.Add(unifyNUD);
			tdSmallPanel.Controls.Add(unifyValueButton);
			
			for (int stepIndex = 0; stepIndex < stepWrapperList.Count; stepIndex++)
			{
				Panel stepPanel = new Panel
				{
					Name = "stepPanel" + (stepIndex + 1),
					BorderStyle = stepPanelDemo.BorderStyle,
					Size = stepPanelDemo.Size
				};

				Label stepLabel = new Label
				{
					Name = "stepLabel" + (stepIndex + 1),
					Text = "第" + (stepIndex + 1) + "步",
					AutoSize = stepLabelDemo.AutoSize,
					ForeColor = stepLabelDemo.ForeColor,
					Location = stepLabelDemo.Location,
					Size = stepLabelDemo.Size
				};

				Button topButton = new Button
				{
					Name = "topButton" + (stepIndex + 1),
					Location = topButtonDemo.Location,
					Size = topButtonDemo.Size,
					UseVisualStyleBackColor = true,
					Text = "↑",
					Tag = 255
				};

				Button bottomButton = new Button
				{
					Name = "topButton" + (stepIndex + 1),
					Location = bottomButtonDemo.Location,
					Size = bottomButtonDemo.Size,
					UseVisualStyleBackColor = true,
					Text = "↓",
					Tag = 0
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
				};

				stepPanel.Controls.Add(stepLabel);
				stepPanel.Controls.Add(topButton);
				stepPanel.Controls.Add(bottomButton);
				stepPanel.Controls.Add(stepNUD);

				stepFLP.Controls.Add(stepPanel);

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
		/// 事件：点击《(每个步数内的)↑》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void topButton_Click(object sender, EventArgs e)
		{
			((sender as Button).Parent.Controls[3] as NumericUpDown).Value = 255;
		}

		/// <summary>
		/// 事件：点击《(每个步数内的)↓》
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
			NumericUpDown nud = sender as NumericUpDown;
		    int tdIndex = MathHelper.GetIndexNum((nud.Parent.Parent as FlowLayoutPanel).Name, -1);
			int stepIndex = MathHelper.GetIndexNum(nud.Name, -1);
			int stepValue = decimal.ToInt32( nud.Value );
			//Console.WriteLine( tdIndex + " +++ " +  stepIndex +"  --- "+ stepValue);			
			mainForm.SetTdStepValue(  tdIndex, stepIndex , stepValue);
		}

		/// <summary>
		/// 事件：点击《(统一)设值 + ↑ + ↓》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyValueButton_Click(object sender, EventArgs e)
		{
			// 本按键对应的tdIndex; 
			Button btn = sender as Button;
			int tdIndex = MathHelper.GetIndexNum(btn.Name, -1);

			// 统一调整的位置
			int unifyPos = ((btn.Parent as Panel).Controls[1] as ComboBox).SelectedIndex;

			// 统一调整的数值
			int unifyValue =decimal.ToInt32( (  (btn.Parent as Panel).Controls[4] as NumericUpDown) . Value );
			if (btn.Text == "↑")
			{
				unifyValue = 255;
			}
			else if (btn.Text == "↓")
			{
				unifyValue = 0;
			}
					

			// 好几个if else 语句，合成这个for语句（只有双步时，从第二步开始调整；只有“全部”时，每次步进的数量为1）
			for (int stepIndex =( unifyPos == 2 ? 1 : 0) ; 
				stepIndex < btn.Parent.Parent.Controls[0].Controls.Count; 
				stepIndex +=( unifyPos == 0 ? 1 : 2) )
			{
				Console.WriteLine( "--------------------"	);
				NumericUpDown stepNUD = btn.Parent.Parent.Controls[0].Controls[stepIndex].Controls[3] as NumericUpDown; 				
				stepNUD.Value = unifyValue;
			}

			// Console.WriteLine( tdIndex + " - "+unifyPos + " : " + unifyValue );
		}

		/// <summary>
		/// 辅助方法：设置提醒
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="msgBoxShow"></param>
		private void setNotice(string msg, bool msgBoxShow) {
			myStatusLabel.Text = msg;
			if (msgBoxShow) {
				MessageBox.Show(msg);
			}
		}		
	}
}
 