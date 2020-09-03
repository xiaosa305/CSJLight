using LightController.Ast;
using LightController.Common;
using LightEditor.Ast;
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
	public partial class DetailMultiForm : Form
	{
		private MainFormBase mainForm;
		private bool isJumpStep = true;
		int maxWidth = 1084;
		int defaultWidth = 1084;
		int maxStep = 0;		

		/// <summary>
		/// 此构造方法：适用于多个灯具的多步联调
		/// </summary>
		/// <param name="mainForm"></param>
		/// <param name="tdDict"></param>
		public DetailMultiForm(MainFormBase mainForm, Dictionary<int, List<int>> tdDict)
		{
			this.mainForm = mainForm;

			InitializeComponent();

			// 要先找出当前所有灯具中的最大步数（不满的通道就要进行填充、且第一行要用到）
			generateMaxStep(tdDict); 

			IList<LightWrapper> lwList = mainForm.LightWrapperList;
			foreach (int lightIndex in tdDict.Keys) 
			{
				LightStepWrapper lsWrapper = lwList[lightIndex].LightStepWrapperList[mainForm.CurrentFrame, mainForm.CurrentMode];
				if (lsWrapper != null)
				{
					IList<StepWrapper> stepWrapperList = lsWrapper.StepWrapperList;
					foreach (int tdIndex in tdDict[lightIndex])
					{
						addTdPanel(stepWrapperList, lightIndex, tdIndex);
					}
				}
			}

			// 处理滚动条的宽度
			unifyHScrollBar.Maximum = maxWidth;
			unifyHScrollBar.LargeChange = defaultWidth;

			// 处理步数显示行
			for (int stepIndex = 1; stepIndex <= maxStep; stepIndex++) {
				Panel stepShowPanel = new Panel
				{
					Name = "stepShowPanel" + stepIndex ,
					BorderStyle = stepShowPanelDemo.BorderStyle ,
					Location = stepShowPanelDemo.Location,
					Margin = stepShowPanelDemo.Margin,
					Size = stepShowPanelDemo.Size ,
				};

				Label stepLabel = new Label
				{
					Name = "stepLabel" + stepIndex,
					AutoSize = stepLabelDemo.AutoSize,
					BackColor = stepLabelDemo.BackColor,
					ForeColor = stepLabelDemo.ForeColor,
					Location = stepLabelDemo.Location,
					Size = stepLabelDemo.Size,
					Text = "第" + stepIndex + "步",
				};

				CheckBox stepCheckBox = new CheckBox
				{
					Name = "stepCheckBox" + stepIndex,
					AutoSize = stepCheckBoxDemo.AutoSize,
					BackColor = stepCheckBoxDemo.BackColor , 
					ForeColor = stepCheckBoxDemo.ForeColor,
					Location = stepCheckBoxDemo.Location,					
					Size = stepCheckBoxDemo.Size,
					Text = stepCheckBoxDemo.Text ,
					TextAlign = stepCheckBoxDemo.TextAlign,
					UseVisualStyleBackColor = stepCheckBoxDemo.UseVisualStyleBackColor
				};

				stepShowPanel.Controls.Add(stepLabel);
				stepShowPanel.Controls.Add(stepCheckBox);
				stepShowFLP.Controls.Add(stepShowPanel);
			}
		}

		private void DetailMultiForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
		}

		/// <summary>
		/// 辅助方法：通过传入的tdIndex，生成相应的tdPanel(某通道所有步数据的调节面板)
		/// </summary>
		/// <param name="stepWrapperList"></param>
		/// <param name="tdIndex"></param>
		private void addTdPanel(IList<StepWrapper> stepWrapperList, int lightIndex, int tdIndex)
		{
			if (stepWrapperList == null || stepWrapperList.Count == 0)
			{
				return;
			}

			Panel tdPanel = new Panel
			{
				Name = "tdPanel" + (tdIndex + 1),
				Location = tdPanelDemo.Location,
				Size = tdPanelDemo.Size,
				BorderStyle = tdPanelDemo.BorderStyle,
				Margin = tdPanelDemo.Margin,
				Tag = lightIndex,
			};

			Panel tdSmallPanel = new Panel
			{
				Name = "tdSmallPanel" + (tdIndex + 1),
				BackColor = tdSmallPanelDemo.BackColor,
				Dock = tdSmallPanelDemo.Dock,
				Location = tdSmallPanelDemo.Location,
				Size = tdSmallPanelDemo.Size,
				BorderStyle = tdSmallPanelDemo.BorderStyle,
				Margin = tdSmallPanelDemo.Margin,
				Tag = lightIndex
			};

			FlowLayoutPanel stepFLP = new FlowLayoutPanel
			{
				Name = "stepFLP" + (tdIndex + 1),
				AutoScroll = stepFLPDemo.AutoScroll,
				BackColor = stepFLPDemo.BackColor,
				Location = stepFLPDemo.Location,
				Size = stepFLPDemo.Size,			
				//Margin = stepFLPDemo.Margin,
				//Padding = stepFLPDemo.Padding,
				WrapContents = stepFLPDemo.WrapContents,
				Tag = lightIndex
			};

			bigFLP.Controls.Add(tdPanel);
			tdPanel.Controls.Add(tdSmallPanel);
			tdPanel.Controls.Add(stepFLP);
			
			Label tdLabel = new Label
			{
				Name = "tdLabel" + (tdIndex + 1),
				Location = tdLabelDemo.Location,
				Size = tdLabelDemo.Size,
				Text = mainForm.LightAstList[lightIndex].LightType + "\n" 
								+ stepWrapperList[0].TongdaoList[tdIndex].Address + " - " + stepWrapperList[0].TongdaoList[tdIndex].TongdaoName,
				Tag = lightIndex
			};
			
			tdSmallPanel.Controls.Add(tdLabel);

			// 满足多个条件，才会添加子属性的comboBox
			SAWrapper saw = mainForm.GetSeletecdLightTdSaw(lightIndex, tdIndex);
			if (saw != null && saw.SaList != null && saw.SaList.Count != 0)
			{
				ComboBox saComboBox = new ComboBox
				{
					Name = "saComboBox" + (tdIndex + 1),
					DropDownStyle = saComboBoxDemo.DropDownStyle,
					FormattingEnabled = true,
					Location = saComboBoxDemo.Location,
					Size = saComboBoxDemo.Size,
					Tag = lightIndex
				};
				foreach (SA sa in saw.SaList)
				{
					saComboBox.Items.Add(sa.SAName + "(" + sa.StartValue + ")");
				}				

				tdSmallPanel.Controls.Add(saComboBox);
			}						

			for (int stepIndex = 0; stepIndex < stepWrapperList.Count; stepIndex++)
			{
				Panel stepPanel = new Panel
				{
					Name = "stepPanel" + (stepIndex + 1),
					BackColor = getBackColor(stepWrapperList[stepIndex].TongdaoList[tdIndex].ScrollValue),
					BorderStyle = stepPanelDemo.BorderStyle,					
					Margin = stepPanelDemo.Margin,
					Size = stepPanelDemo.Size,					
					Tag = lightIndex,
				};

				Button topBottomButton = new Button
				{
					Name = "topBottomButton" + (stepIndex + 1),
					Location = topBottomButtonDemo.Location,
					Size = topBottomButtonDemo.Size,
					UseVisualStyleBackColor = true,
					Text = topBottomButtonDemo.Text,
					Tag = lightIndex
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
					Tag = lightIndex
				};
								
				stepPanel.Controls.Add(topBottomButton);
				stepPanel.Controls.Add(stepNUD);
				stepFLP.Controls.Add(stepPanel);

				topBottomButton.Click += topBottomButton_Click;				
				stepNUD.MouseWheel += someNUD_MouseWheel;
				stepNUD.ValueChanged += StepNUD_ValueChanged;
				stepNUD.MouseDoubleClick += stepNUD_MouseDoubleClick;
			}

			// 此初起，若此通道的步数小于最大步数，用一些空白的Panel进行填充
			for (int stepIndex = stepWrapperList.Count; stepIndex < maxStep; stepIndex++) {
				Panel emptyPanel = new Panel
				{
					Name = "emptyPanel",
					//BorderStyle = stepPanelDemo.BorderStyle,
					Padding = stepPanelDemo.Padding,
					Size = stepPanelDemo.Size,
				};
				stepFLP.Controls.Add(emptyPanel);
			}

			// 每次生成stepFLP后，与maxWidth进行比较，如果更大，则替换为这个数
			if (stepFLP.DisplayRectangle.Width > maxWidth) {
				maxWidth = stepFLP.DisplayRectangle.Width ;
			}

		}

		/// <summary>
		/// 事件：点击stepPanel内的《↑↓》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void topBottomButton_Click(object sender, EventArgs e)
		{
			NumericUpDown nud = (sender as Button).Parent.Controls[1] as NumericUpDown;
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
			NumericUpDown nud = sender as NumericUpDown;
			// 当没有子属性comboBox时，return
			if ( nud.Parent.Parent.Parent.Controls[0].Controls.Count != 2){			
				return;
			}
			
			// 当下拉框尚未选中时，return；
			ComboBox cb = (nud.Parent.Parent.Parent.Controls[0].Controls[1] as ComboBox);
			if (cb.SelectedIndex == -1) {
					return;
			}

			int saValue = StringHelper.GetInnerValue(cb.Text);			
			nud.Value = saValue;						
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
			int stepValue = decimal.ToInt32(nud.Value);
			(nud.Parent as Panel).BackColor = getBackColor(stepValue);
			int lightIndex = (int)(nud.Tag);

			mainForm.SetTdStepValue(lightIndex, tdIndex, stepIndex, stepValue, isJumpStep);

			// 如果这里是多灯模式，就把相关的通道的值设一下（遍历所有通道，找到tdIndex一样的（且都在selectedIndices中的数））
			if (mainForm.IsMultiMode && mainForm.SelectedIndices.Contains(lightIndex))
			{
				foreach (Panel panel in bigFLP.Controls)
				{
					int lightIndexTemp = Convert.ToInt32(panel.Tag); //tdPanelDemo的Tag设为-1，可避免把tdPanelDemo也给算进去；
					if(mainForm.SelectedIndices.Contains(lightIndexTemp) && lightIndexTemp != lightIndex)
					{
						int tdIndexTemp = MathHelper.GetIndexNum(panel.Name, -1);
						if (tdIndexTemp == tdIndex)
						{
							NumericUpDown nudTemp = panel.Controls[1].Controls[stepIndex].Controls[1] as NumericUpDown;
							nudTemp.ValueChanged -= StepNUD_ValueChanged;
							nudTemp.Value = stepValue;
							nudTemp.Parent.BackColor = getBackColor(stepValue);
							nudTemp.ValueChanged += StepNUD_ValueChanged;
						}
					}
				}
			}
		}

		#region 通用方法
			   
		/// <summary>
		/// 辅助方法：由输入的通道值，设置背景颜色
		/// </summary>
		/// <param name="stepValue"></param>
		/// <returns></returns>
		private Color getBackColor(int stepValue)
		{

			if (stepValue == 0)
			{
				return Color.Gray;
			}
			else if (stepValue > 0 && stepValue < 255)
			{
				return Color.SteelBlue;
			}
			else
			{
				return Color.IndianRed;
			}
		}

		/// <summary>
		/// 辅助方法：找出最大的步，以填充首行、其他步少的面板补上空白的
		/// </summary>
		/// <returns></returns>
		private void generateMaxStep(Dictionary<int, List<int>> tdDict) {
			
			IList<LightWrapper> lwList = mainForm.LightWrapperList;
			
			foreach (int lightIndex in tdDict.Keys)
			{
				LightStepWrapper lsWrapper = lwList[lightIndex].LightStepWrapperList[mainForm.CurrentFrame, mainForm.CurrentMode];

				if (lsWrapper != null)
				{
					if (lsWrapper.TotalStep > maxStep) {
						maxStep = lsWrapper.TotalStep;
					}
				}
			}
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
		/// 事件：滚动 统一滚动条
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyHScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			HScrollBar hsb = sender as HScrollBar;

			stepShowFLP.AutoScrollPosition = new Point(hsb.Value, 0);
			for(int tdIndex = 1; tdIndex< bigFLP.Controls.Count; tdIndex++)
			{
				(bigFLP.Controls[tdIndex].Controls[1] as FlowLayoutPanel).AutoScrollPosition = new Point(hsb.Value, 0);
			}

		}

		#endregion

		#region 统一调值
		
		/// <summary>
		/// 事件：点击《（统一）设值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyValueButton_Click(object sender, EventArgs e)
		{
			// 通过按键确定统一调整的数值
			Button btn = sender as Button;
			
			int unifyValue = decimal.ToInt32( unifyNUD.Value);
			if (btn.Text == "↑")
			{
				unifyValue = 255;
			}
			else if (btn.Text == "↓")
			{
				unifyValue = 0;
			}

			isJumpStep = false ; 
			// 遍历checkBox列表，确定需要调整的步数
			for (int stepIndex = 0; stepIndex < maxStep; stepIndex++)
			{
				CheckBox cb = stepShowFLP.Controls[stepIndex + 1].Controls[1] as CheckBox;
				if (cb.Checked) {
					for (int tdIndex = 1; tdIndex < bigFLP.Controls.Count ; tdIndex++)
					{
						Panel stepPanel = bigFLP.Controls[tdIndex].Controls[1].Controls[stepIndex] as Panel;
						if (stepPanel.Name != "emptyPanel") {
							(stepPanel.Controls[1] as NumericUpDown).Value = unifyValue;
						}						
					}
				}
			}

			isJumpStep = true;
		}

		#endregion

		private void testButton_Click(object sender, EventArgs e)
		{
			// Test1 : 打出每个FLP的DisplayRectangle
			for (int tdIndex  = 1; tdIndex < bigFLP.Controls.Count; tdIndex ++)
			{
				Console.WriteLine(	 (bigFLP.Controls[tdIndex].Controls[1] as FlowLayoutPanel).DisplayRectangle  ) ;  
			}

			//Test2 ： 打印出maxWidth和defaultWidth
			Console.WriteLine(	maxWidth + "  :  " + defaultWidth );
			
		}

		/// <summary>
		/// 事件：更改unifyComboBox，主动为用户勾选步
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 统一调整的位置
			int unifyPos = unifyComboBox.SelectedIndex;

			// 先把所有的checked都去掉
			for (int stepIndex = 1; stepIndex < stepShowFLP.Controls.Count; stepIndex++)
			{
				(stepShowFLP.Controls[stepIndex].Controls[1] as CheckBox).Checked = false;
			}

			if (unifyPos == 3) {
				return;
			}

			// 再勾选所需的CheckBox
			for (int stepIndex = (unifyPos == 2 ? 2 : 1);
				stepIndex <= maxStep;
				stepIndex += unifyPos == 0 ? 1 : 2)
			{
				(stepShowFLP.Controls[stepIndex].Controls[1] as CheckBox).Checked = true;
			}
		}	
	}
}
