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

			// 处理编组
			for (int groupIndex = 0; groupIndex < mainForm.GroupList.Count; groupIndex++)
			{
				groupComboBox.Items.Add(mainForm.GroupList[groupIndex].GroupName);
			}
			groupComboBox.SelectedIndex = 0;

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
					BackColor = lightPanelDemo.BackColor,
					Tag = lightIndex,
				};

				Label lightLabel = new Label
				{
					Name = "lightLabel" + (lightIndex + 1),
					Font = lightLabelDemo.Font,
					Location = lightLabelDemo.Location,
					Size = lightLabelDemo.Size,
					Text = la.LightType + "\n(" + la.LightAddr + ")",										
					TextAlign = lightLabelDemo.TextAlign,
					Tag = lightIndex,
				};

				CheckBox allCheckBox = new CheckBox
				{
					Name = "allCheckBox" + (lightIndex + 1),
					AutoSize = allCheckBoxDemo.AutoSize,
					Font = allCheckBoxDemo.Font,
					Location = allCheckBoxDemo.Location,
					Size = allCheckBoxDemo.Size,
					Text = allCheckBoxDemo.Text,
					UseVisualStyleBackColor = allCheckBoxDemo.UseVisualStyleBackColor,
					Tag = lightIndex,
				};
				allCheckBox.CheckedChanged += allCheckBox_CheckedChanged;

				FlowLayoutPanel lightFLP = new FlowLayoutPanel
				{
					Name = "lightFLP" + (lightIndex + 1),
					AutoScroll = lightFLPDemo.AutoScroll,
					Location = lightFLPDemo.Location,
					Margin = lightFLPDemo.Margin,
					Size = lightFLPDemo.Size,
					Tag = lightIndex,
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
						UseVisualStyleBackColor = tdCheckBoxDemo.UseVisualStyleBackColor,
						Enabled = tdCheckBoxDemo.Enabled ,
						Tag = lightIndex ,
					};
					tdCheckBox.CheckedChanged += tdCheckBox_CheckedChanged;
					lightFLP.Controls.Add(tdCheckBox);
				}

				lightPanel.Controls.Add(lightLabel);
				lightPanel.Controls.Add(allCheckBox);
				lightPanel.Controls.Add(lightFLP);

				bigFLP.Controls.Add(lightPanel);
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
			LanguageHelper.InitForm(this);

			// 在load中决定哪些灯具要进行显示		
			for (int lightIndex = 0; lightIndex < mainForm.LightAstList.Count; lightIndex++)
			{
				LightStepWrapper lsWrapper = mainForm.LightWrapperList[lightIndex].LightStepWrapperList[mainForm.CurrentScene, mainForm.CurrentMode];
				if (lsWrapper != null && lsWrapper.StepWrapperList != null && lsWrapper.StepWrapperList.Count > 0)
				{
					bigFLP.Controls[lightIndex + 1].Show(); 
				}
				else {
					bigFLP.Controls[lightIndex + 1].Hide() ; 
				}
			}
		}

		/// <summary>
		/// 事件：更改《编组选项》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void groupComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 先清空所有选项（只要更改了编组，都得进行这个操作）
			for (int panelIndex = 0; panelIndex < bigFLP.Controls.Count; panelIndex++)
			{
				(bigFLP.Controls[panelIndex] as Panel).BackColor = SystemColors.Menu; ;
			}

			// 取出groupIndex，若<0则return,否则开始勾选组内的灯具
			int groupIndex = groupComboBox.SelectedIndex - 1;
			if (groupIndex < 0) {
				setNotice("已清空编组。", false);
				return;
			}
			// 如果选项不为空，则勾选上组内成员
			foreach (int lightIndex in mainForm.GroupList[groupIndex].LightIndexList)
			{
				(bigFLP.Controls[lightIndex + 1] as Panel).BackColor = Color.OldLace ;
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
		/// 事件：勾选｜取消勾选通道复选框
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			int groupIndex = groupComboBox.SelectedIndex - 1;
			if (groupIndex < 0) {
				return;
			}

			CheckBox cb = sender as CheckBox;			
			int lightIndex = (int)(cb.Tag);
			int tdIndex = MathHelper.GetIndexNum(cb.Name, -1);
			bool isChecked = cb.Checked;

			if (mainForm.GroupList[groupIndex].LightIndexList.Contains( lightIndex )) {
				for(int panelIndex=1; panelIndex < bigFLP.Controls.Count; panelIndex++){
					int curLightIndex = panelIndex - 1;
					if (mainForm.GroupList[groupIndex].LightIndexList.Contains(curLightIndex)) {
						((bigFLP.Controls[panelIndex].Controls[2] as FlowLayoutPanel).Controls[tdIndex] as CheckBox).Checked = isChecked;						
					}
				}
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

			int tdCount = 0; 
			foreach (Panel panel in bigFLP.Controls)
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
							tdCount++;
						}						
					}
					if (tdIndices.Count > 0) {
						tdDict.Add(lightIndex, tdIndices);
					}
				}
			}

			if (tdCount == 0) {
				setNotice("请至少选择一个通道，才可进行多步联调。", true);
				return;
			}

			if (tdCount > 50)
			{
				setNotice("因操作系统限制，无法添加超过50个通道，请取消选择部分通道后重试。", true);
				return;
			}
			
			Hide();
			mainForm.TdDict = tdDict;
			mainForm.Activate();
			//new DetailMultiForm(mainForm, tdDict).ShowDialog();
			new DetailMultiPageForm(mainForm, tdDict).ShowDialog();   
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

		#region 通用方法

		/// <summary>
		/// 辅助方法：设置提醒
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="msgBoxShow"></param>
		private void setNotice(string msg, bool msgBoxShow)
		{
			myStatusLabel.Text = msg;
			if (msgBoxShow)
			{
				MessageBox.Show(msg);
			}
		}

		#endregion
			
	}
}
