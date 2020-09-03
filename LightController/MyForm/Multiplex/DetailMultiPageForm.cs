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
	public partial class DetailMultiPageForm : Form
	{
		private MainFormBase mainForm;
		private bool isJumpStep = true;  // 是否跳转改动步
		private int maxStep = 0 ;  // 所有选中灯具的最大步数
		private SortedSet<int> stepSet = new SortedSet<int>();  // 记录选中的步数   
		private List<int> tdList = new List<int>(); // 记录选中的通道
		private IList<int> lightIndexList ;// 记录编组内的成员索引列表		
		private int pageCount; //  总页数
		private int currentPage = 1;  // 从0开始计算页数

		public DetailMultiPageForm(MainFormBase mainForm, Dictionary<int, List<int>> tdDict)
		{
			this.mainForm = mainForm;
			InitializeComponent();

			groupComboBox.SelectedIndex = 0;
			tdComboBox.SelectedIndex = 0 ;

			// 要先找出当前所有灯具中的最大步数（不满的通道就要进行填充、且第一行要用到）
			generateMaxStep(tdDict);

			// 处理编组
			for (int groupIndex = 0; groupIndex < mainForm.GroupList.Count; groupIndex++)
			{
				groupComboBox.Items.Add(mainForm.GroupList[groupIndex].GroupName) ; 
			}

			// 由最大步数计算最大页
			pageCount = (int)Math.Ceiling( maxStep/20d ) ;

			// 由最大步渲染 stepShowPanel(步数选择行)
			int addStepCount = maxStep > 20 ? 20 : maxStep ;
			for (int stepIndex = 1; stepIndex <= addStepCount ; stepIndex++)
			{
				Panel stepShowPanel = new Panel
				{
					Name = "stepShowPanel" + stepIndex,
					BorderStyle = stepShowPanelDemo.BorderStyle,
					Location = stepShowPanelDemo.Location,
					Margin = stepShowPanelDemo.Margin,
					Size = stepShowPanelDemo.Size,
				};
				CheckBox stepCheckBox = new CheckBox
				{
					Name = "stepCheckBox" + stepIndex,
					AutoSize = stepCheckBoxDemo.AutoSize,
					BackColor = stepCheckBoxDemo.BackColor,
					ForeColor = stepCheckBoxDemo.ForeColor,
					Location = stepCheckBoxDemo.Location,
					Size = stepCheckBoxDemo.Size,
					Text = stepIndex.ToString(),
					CheckAlign = stepCheckBoxDemo.CheckAlign,
					TextAlign = stepCheckBoxDemo.TextAlign,
					UseVisualStyleBackColor = stepCheckBoxDemo.UseVisualStyleBackColor
				};
				stepCheckBox.CheckedChanged += StepCheckBox_CheckedChanged;

				stepShowPanel.Controls.Add(stepCheckBox);
				stepShowFLP.Controls.Add(stepShowPanel);
			}

			// 根据各个通道的大小，来制定所需的stepPanel（小于20步的灯具，可渲染少一些的通道）
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

			changePage();
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
				BorderStyle = stepFLPDemo.BorderStyle,
				//Margin = stepFLPDemo.Margin,
				//Padding = stepFLPDemo.Padding,
				WrapContents = stepFLPDemo.WrapContents,
				Tag = lightIndex
			};

			bigFLP.Controls.Add(tdPanel);
			tdPanel.Controls.Add(tdSmallPanel);
			tdPanel.Controls.Add(stepFLP);

			CheckBox tdCheckBox = new CheckBox
			{
				Name = "tdLabel" + (tdIndex + 1),
				AutoSize = tdCheckBoxDemo.AutoSize,
				Location = tdCheckBoxDemo.Location,
				Size = tdCheckBoxDemo.Size ,
				Text = mainForm.LightAstList[lightIndex].LightType + ":" + stepWrapperList[0].TongdaoList[tdIndex].Address +"." + stepWrapperList[0].TongdaoList[tdIndex].TongdaoName,				
				UseVisualStyleBackColor = tdCheckBoxDemo.UseVisualStyleBackColor ,
				Tag = lightIndex,
			};
			tdCheckBox.CheckedChanged += tdCheckBox_CheckedChanged;
			tdSmallPanel.Controls.Add( tdCheckBox );

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

			int addStepCount = stepWrapperList.Count > 20 ? 20 : stepWrapperList.Count;

			for (int stepIndex = 0; stepIndex < addStepCount; stepIndex++)
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
					Font = topBottomButtonDemo.Font,
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
		}
		
		/// <summary>
		/// 辅助方法：修改当前页（只有currentPage发生变化时才运行这个方法）
		/// </summary>
		private void changePage()
		{
			setNotice(currentPage + "/" + pageCount, false);

			// 渲染stepShowFLP
			int editStepCount = maxStep - (currentPage - 1) * 20;
			if (editStepCount > 20)
			{
				editStepCount = 20;
			}

			for (int panelIndex = 1; panelIndex < stepShowFLP.Controls.Count; panelIndex++)
			{
				if (panelIndex <= editStepCount)
				{
					CheckBox cb = stepShowFLP.Controls[panelIndex].Controls[0] as CheckBox;
					int stepIndex = panelIndex + (currentPage - 1) * 20 - 1;					
					cb.Name = "stepCheckBox" + (stepIndex+1) ;
					cb.Text = (stepIndex + 1).ToString() ;
					cb.Checked = stepSet.Contains(stepIndex);

					stepShowFLP.Controls[panelIndex].Show();
				}
				else
				{
					stepShowFLP.Controls[panelIndex].Hide();
				}
			}

			// 遍历所有tdPanel
			for (int panelIndex = 1; panelIndex < bigFLP.Controls.Count; panelIndex++)
			{
				Panel tdPanel = bigFLP.Controls[panelIndex] as Panel;
				int lightIndex = (int)tdPanel.Tag;
				int tdIndex = MathHelper.GetIndexNum(tdPanel.Name, -1);

				IList<StepWrapper> swList = mainForm
					.LightWrapperList[lightIndex]
					.LightStepWrapperList[mainForm.CurrentFrame, mainForm.CurrentMode]
					.StepWrapperList;

				for (int stepPanelIndex = 0; stepPanelIndex < 20; stepPanelIndex++)
				{
					int stepIndex = (currentPage - 1) * 20 + stepPanelIndex;
					if (stepPanelIndex < tdPanel.Controls[1].Controls.Count)
					{

						Panel stepPanel = tdPanel.Controls[1].Controls[stepPanelIndex] as Panel;
						if (stepIndex < swList.Count)
						{
							(stepPanel.Controls[0] as Button).Name = "topBottomButton" + (stepIndex + 1);
							(stepPanel.Controls[1] as NumericUpDown).Name = "stepNUD" + (stepIndex + 1);
							(stepPanel.Controls[1] as NumericUpDown).Value = swList[stepIndex].TongdaoList[tdIndex].ScrollValue;
							stepPanel.Show();
						}
						else
						{
							stepPanel.Hide();
						}
					}
				}

			}
		}
				
		private void DetailMultiPageForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
		}

		/// <summary>
		/// 辅助方法：找出最大的步，以填充首行、其他步少的面板补上空白的
		/// </summary>
		/// <returns></returns>
		private void generateMaxStep(Dictionary<int, List<int>> tdDict)
		{
			foreach (int lightIndex in tdDict.Keys)
			{
				LightStepWrapper lsWrapper = mainForm
					.LightWrapperList[lightIndex]
					.LightStepWrapperList[mainForm.CurrentFrame, mainForm.CurrentMode];

				if (lsWrapper != null)
				{
					if (lsWrapper.TotalStep > maxStep)
					{
						maxStep = lsWrapper.TotalStep;
					}
				}
			}
		}

		/// <summary>
		/// 事件：点击《上|下一页》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pageButton_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;			
			if (btn.Name == "backPageButton")
			{
				if (currentPage > 1)
				{
					currentPage --;
					changePage();
				}
			}
			else if (btn.Name == "nextPageButton") {
				if (currentPage < pageCount)
				{
					currentPage ++ ;
					changePage();
				}
			}
		}

		#region 选步相关

		/// <summary>
		/// 事件：更改《快速选步》的选中项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 统一调整的位置
			int unifyPos = unifyComboBox.SelectedIndex;

			// 无论什么情况，先把stepList清空；
			stepSet = new SortedSet<int>();

			if (unifyPos != 3) {
				for (int stepIndex = (unifyPos == 2 ? 1 : 0); 
				stepIndex < maxStep;
				stepIndex += unifyPos == 0 ? 1 : 2)
				{
					stepSet.Add( stepIndex );
				}
			}
			refreshStepCheck(  );  
		}

		/// <summary>
		/// 事件：刷新步数选择（控件显示）
		/// </summary>
		private void refreshStepCheck()
		{
			for(int panelIndex=1; panelIndex < stepShowFLP.Controls.Count ; panelIndex++){
				CheckBox cb = stepShowFLP.Controls[panelIndex].Controls[0] as CheckBox;
				int tdIndex = MathHelper.GetIndexNum(cb.Name, -1 );

				cb.CheckedChanged -= StepCheckBox_CheckedChanged;  //主动刷新步数选中项时，先关闭监听
				cb.Checked = stepSet.Contains(tdIndex);
				cb.CheckedChanged += StepCheckBox_CheckedChanged; //改完后重新监听
			}
		}

		/// <summary>
		/// 事件：选中不同的步数
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StepCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox cb = sender as CheckBox;
			int stepIndex = MathHelper.GetIndexNum(cb.Name, -1);

			if (cb.Checked)
			{
				stepSet.Add(stepIndex);
			}
			else
			{
				if (stepSet.Contains(stepIndex))
				{
					stepSet.Remove(stepIndex);
				}
			}
		}

		#endregion 

		/// <summary>
		/// 事件：更改《编组下拉框》选项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void groupComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox cb = sender as ComboBox;

			// 只要更改了编组，就先清空tdComboBox的项,并关闭使能 : 但很奇怪！一旦先写这一句，tdComboBox后面加的内容就不见了
			tdComboBox.Items.Clear();
			tdComboBox.Items.Add("请选择通道");

			if (cb.SelectedIndex > 0)
			{
				lightIndexList = mainForm.GroupList[cb.SelectedIndex - 1].LightIndexList;
				int firstLightIndex = lightIndexList[0];
				StepWrapper sw  = mainForm.LightWrapperList[ firstLightIndex ].StepTemplate; 				
				
				foreach (TongdaoWrapper td in sw.TongdaoList)
				{
					tdComboBox.Items.Add(td.TongdaoName);
				}				
				tdComboBox.SelectedIndex = 0;				
			}
			else
			{						
				lightIndexList = new List<int>();
			}
		}

		/// <summary>
		/// 事件：更改《通道下拉框》选项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedTdIndex = tdComboBox.SelectedIndex - 1 ;

			for (int panelIndex = 1; panelIndex < bigFLP.Controls.Count; panelIndex++)
			{
				Panel tdPanel = bigFLP.Controls[panelIndex] as Panel;
				int lightIndex = (int)tdPanel.Tag;
				int tdIndex = MathHelper.GetIndexNum(tdPanel.Name, -1);

				(tdPanel.Controls[0].Controls[0] as CheckBox).Checked = lightIndexList.Contains(lightIndex) && tdIndex == selectedTdIndex ;
			}
		}

		/// <summary>
		/// 事件：当【灯具通道名】被勾选或去除勾选后，把其背景色设为高亮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox cb = sender as CheckBox;
			(cb.Parent as Panel).BackColor = cb.Checked ? Color.OldLace : SystemColors.Menu ;
		}

		#region 通用方法		

		/// <summary>
		/// 事件：点击stepPanel内的《↑↓》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void topBottomButton_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;

			NumericUpDown nud = btn.Parent.Controls[1] as NumericUpDown;
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
			if (nud.Parent.Parent.Parent.Controls[0].Controls.Count != 2)
			{
				return;
			}

			// 当下拉框尚未选中时，return；
			ComboBox cb = (nud.Parent.Parent.Parent.Controls[0].Controls[1] as ComboBox);
			if (cb.SelectedIndex == -1)
			{
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
					if (mainForm.SelectedIndices.Contains(lightIndexTemp) && lightIndexTemp != lightIndex)
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
		/// 辅助方法：由输入的通道值，设置背景颜色
		/// </summary>
		/// <param name="stepValue"></param>
		/// <returns></returns>
		private Color getBackColor(int stepValue)
		{
			if (stepValue == 0)
			{
				return SystemColors.Window;
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

		private void testButton_Click(object sender, EventArgs e)
		{
			//查看选中步
			Console.Write("stepSet : ");
			foreach (int item in stepSet)
			{
				Console.Write(item + "  ");
			}
			Console.WriteLine();

			// 查看选中编组
			Console.Write("编组为 : ");
			foreach (int item in lightIndexList)
			{
				Console.Write(item + "  " );
			}
			Console.WriteLine( );

		}

		
	}
}
