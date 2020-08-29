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
	public partial class DetailMultiAstForm : Form
	{
		private MainFormBase mainForm;
		
		public DetailMultiAstForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;

			InitializeComponent();

			// 初始化时，所有的灯具都要生成相应面板（但显示与否由 load内决定）【这样之前的勾选可以保留】
			for (int lightIndex = 0; lightIndex < mainForm.LightAstList.Count; lightIndex++)
			{
				IList<TongdaoWrapper> tongdaoList = mainForm.LightWrapperList[lightIndex].StepTemplate.TongdaoList;
				LightAst la = mainForm.LightAstList[lightIndex];

				Panel lightPanel = new Panel
				{
					Name = "lightPanel" + (lightIndex + 1),
					Location = lightPanelDemo.Location,
					Size = lightPanelDemo.Size,
					BackColor = lightPanelDemo.BackColor
				};

				Label lightLabel = new Label
				{
					Name = "lightLabel" + (lightIndex + 1),
					Text = la.LightType + "\n(" + la.LightAddr + ")",
					Dock = lightLabelDemo.Dock,
					Location = lightLabelDemo.Location,
					Size = lightLabelDemo.Size,
					TextAlign = lightLabelDemo.TextAlign,
					Font = lightLabelDemo.Font,
				};

				CheckBox allCheckBox = new CheckBox
				{
					Name = "allCheckBox" + (lightIndex + 1),
					AutoSize = allCheckBoxDemo.AutoSize,
					Font = allCheckBoxDemo.Font,
					Location = allCheckBoxDemo.Location,
					Size = allCheckBoxDemo.Size,
					Text = allCheckBoxDemo.Text,
					UseVisualStyleBackColor = allCheckBoxDemo.UseVisualStyleBackColor
				};
				allCheckBox.CheckedChanged += allCheckBox_CheckedChanged;

				FlowLayoutPanel lightFLP = new FlowLayoutPanel
				{
					Name = "lightFLP" + (lightIndex + 1),
					AutoScroll = lightFLPDemo.AutoScroll,
					Location = lightFLPDemo.Location,
					Margin = lightFLPDemo.Margin,
					Size = lightFLPDemo.Size
				};

				for (int tdIndex = 0; tdIndex < tongdaoList.Count; tdIndex++)
				{
					CheckBox tdCheckBox = new CheckBox
					{
						Name = "tdCheckBox" + (tdIndex + 1),
						Text = tongdaoList[tdIndex].TongdaoName,
						AutoSize = tdCheckBoxDemo.AutoSize,
						Location = tdCheckBoxDemo.Location,
						Size = tdCheckBoxDemo.Size,
						UseVisualStyleBackColor = tdCheckBoxDemo.UseVisualStyleBackColor
					};
					lightFLP.Controls.Add(tdCheckBox);
				}

				lightPanel.Controls.Add(lightLabel);
				lightPanel.Controls.Add(allCheckBox);
				lightPanel.Controls.Add(lightFLP);

				bigFlowLayoutPanel.Controls.Add(lightPanel);
			}
		}

		/// <summary>
		///  在load中进行窗体位置的更改；以及没有步数的灯具的隐藏
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DetailMultiAstForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 200);

			// 在load中决定哪些灯具要进行显示		
			for (int lightIndex = 0; lightIndex < mainForm.LightAstList.Count; lightIndex++)
			{
				LightStepWrapper lsWrapper = mainForm.LightWrapperList[lightIndex].LightStepWrapperList[mainForm.CurrentFrame, mainForm.CurrentMode];
				if (lsWrapper != null && lsWrapper.StepWrapperList != null && lsWrapper.StepWrapperList.Count > 0)
				{
					bigFlowLayoutPanel.Controls[lightIndex + 1].Show();
				}
				else {
					bigFlowLayoutPanel.Controls[lightIndex + 1].Hide() ; 
				}
			}
		}
		
		/// <summary>
		/// 事件：勾选或去掉勾选allCheckBox复选框
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox acb = sender as CheckBox;
			bool checkAll = acb.Checked;
			foreach (CheckBox cb in (acb.Parent.Controls[2] as FlowLayoutPanel).Controls)
			{
				cb.Checked = checkAll;
			}
		}

		/// <summary>
		/// 事件：点击《确定》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			Dictionary<int,List<int>> tdDict = new Dictionary<int ,List<int>>();
			
			foreach (Panel panel in bigFlowLayoutPanel.Controls)
			{
				// 除了可以屏蔽panelDemo之外，刚好也屏蔽了其他步数为0的灯具；
				if (panel.Visible) { 
					int lightIndex = MathHelper.GetIndexNum(panel.Name, -1);					

					FlowLayoutPanel lightFLP = panel.Controls[2] as FlowLayoutPanel;
					List<int> tdIndices = new List<int>();
					foreach (CheckBox cb in lightFLP.Controls )
					{
						if (cb.Checked) {
							int tdIndex = MathHelper.GetIndexNum(cb.Name, -1);
							tdIndices.Add(tdIndex);
						}						
					}
					if (tdIndices.Count > 0) {
						tdDict.Add(lightIndex, tdIndices);
					}
				}
			}

			Hide();
			mainForm.Activate();
			new DetailMultiForm(mainForm, tdDict).ShowDialog();
		}
		
		/// <summary>
		///  事件：点击《取消》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Hide();
			mainForm.Activate();
		}
			   	
	}
}
