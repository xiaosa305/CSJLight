using LightController.Common;
using LightEditor.Ast;
using LightEditor.MyForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightEditor
{

	public partial class WaySetForm : Form
	{
		private LightEditorForm lightEditorForm;
		private TextBox selectedTextBox = null; //辅助变量，用来记录鼠标选择的textBox
		private int selectedTdIndex = -1 ;

		//子属性相关		
		private SAWrapper[] sawArray2;
		private IList<Panel> saPanels = new List<Panel>();
		private IList<Label> saNameLabels = new List<Label>();
		private IList<Label> startValueLabels = new List<Label>();
		private IList<Label> lineLabels = new List<Label>();
		private IList<Label> endValueLabels = new List<Label>();
		private IList<Button> saDeleteButtons = new List<Button>();

		/// <summary>
		///  初始化，并将mainForm（及其相关内容）也传进来；并显示tdPanel相关数据
		/// </summary>
		/// <param name="lightEditorForm"></param>
		public WaySetForm(LightEditorForm lightEditorForm, int tdIndex)
		{
			this.lightEditorForm = lightEditorForm;
			sawArray2 = SAWrapper.DeepCopy(lightEditorForm.SawArray);

			InitializeComponent();

			#region 初始化几个通道值数组
			tdLabels[0] = tdLabel1;
			tdLabels[1] = tdLabel2;
			tdLabels[2] = tdLabel3;
			tdLabels[3] = tdLabel4;
			tdLabels[4] = tdLabel5;
			tdLabels[5] = tdLabel6;
			tdLabels[6] = tdLabel7;
			tdLabels[7] = tdLabel8;
			tdLabels[8] = tdLabel9;
			tdLabels[9] = tdLabel10;
			tdLabels[10] = tdLabel11;
			tdLabels[11] = tdLabel12;
			tdLabels[12] = tdLabel13;
			tdLabels[13] = tdLabel14;
			tdLabels[14] = tdLabel15;
			tdLabels[15] = tdLabel16;
			tdLabels[16] = tdLabel17;
			tdLabels[17] = tdLabel18;
			tdLabels[18] = tdLabel19;
			tdLabels[19] = tdLabel20;
			tdLabels[20] = tdLabel21;
			tdLabels[21] = tdLabel22;
			tdLabels[22] = tdLabel23;
			tdLabels[23] = tdLabel24;
			tdLabels[24] = tdLabel25;
			tdLabels[25] = tdLabel26;
			tdLabels[26] = tdLabel27;
			tdLabels[27] = tdLabel28;
			tdLabels[28] = tdLabel29;
			tdLabels[29] = tdLabel30;
			tdLabels[30] = tdLabel31;
			tdLabels[31] = tdLabel32;

			tdTextBoxes[0] = textBox1;
			tdTextBoxes[1] = textBox2;
			tdTextBoxes[2] = textBox3;
			tdTextBoxes[3] = textBox4;
			tdTextBoxes[4] = textBox5;
			tdTextBoxes[5] = textBox6;
			tdTextBoxes[6] = textBox7;
			tdTextBoxes[7] = textBox8;
			tdTextBoxes[8] = textBox9;
			tdTextBoxes[9] = textBox10;
			tdTextBoxes[10] = textBox11;
			tdTextBoxes[11] = textBox12;
			tdTextBoxes[12] = textBox13;
			tdTextBoxes[13] = textBox14;
			tdTextBoxes[14] = textBox15;
			tdTextBoxes[15] = textBox16;
			tdTextBoxes[16] = textBox17;
			tdTextBoxes[17] = textBox18;
			tdTextBoxes[18] = textBox19;
			tdTextBoxes[19] = textBox20;
			tdTextBoxes[20] = textBox21;
			tdTextBoxes[21] = textBox22;
			tdTextBoxes[22] = textBox23;
			tdTextBoxes[23] = textBox24;
			tdTextBoxes[24] = textBox25;
			tdTextBoxes[25] = textBox26;
			tdTextBoxes[26] = textBox27;
			tdTextBoxes[27] = textBox28;
			tdTextBoxes[28] = textBox29;
			tdTextBoxes[29] = textBox30;
			tdTextBoxes[30] = textBox31;
			tdTextBoxes[31] = textBox32;

			tdNumericUpDowns[0] = numericUpDown1;
			tdNumericUpDowns[1] = numericUpDown2;
			tdNumericUpDowns[2] = numericUpDown3;
			tdNumericUpDowns[3] = numericUpDown4;
			tdNumericUpDowns[4] = numericUpDown5;
			tdNumericUpDowns[5] = numericUpDown6;
			tdNumericUpDowns[6] = numericUpDown7;
			tdNumericUpDowns[7] = numericUpDown8;
			tdNumericUpDowns[8] = numericUpDown9;
			tdNumericUpDowns[9] = numericUpDown10;
			tdNumericUpDowns[10] = numericUpDown11;
			tdNumericUpDowns[11] = numericUpDown12;
			tdNumericUpDowns[12] = numericUpDown13;
			tdNumericUpDowns[13] = numericUpDown14;
			tdNumericUpDowns[14] = numericUpDown15;
			tdNumericUpDowns[15] = numericUpDown16;
			tdNumericUpDowns[16] = numericUpDown17;
			tdNumericUpDowns[17] = numericUpDown18;
			tdNumericUpDowns[18] = numericUpDown19;
			tdNumericUpDowns[19] = numericUpDown20;
			tdNumericUpDowns[20] = numericUpDown21;
			tdNumericUpDowns[21] = numericUpDown22;
			tdNumericUpDowns[22] = numericUpDown23;
			tdNumericUpDowns[23] = numericUpDown24;
			tdNumericUpDowns[24] = numericUpDown25;
			tdNumericUpDowns[25] = numericUpDown26;
			tdNumericUpDowns[26] = numericUpDown27;
			tdNumericUpDowns[27] = numericUpDown28;
			tdNumericUpDowns[28] = numericUpDown29;
			tdNumericUpDowns[29] = numericUpDown30;
			tdNumericUpDowns[30] = numericUpDown31;
			tdNumericUpDowns[31] = numericUpDown32;

			foreach (NumericUpDown item in tdNumericUpDowns)
			{
				item.MouseWheel += new MouseEventHandler(valueNumericUpDown_MouseWheel);
			}

			// 动态添加通道预选名称
			IList<string> tdNameList = TextHelper.Read(Application.StartupPath + @"\PreTDNameList");
			foreach (string item in tdNameList)
			{
				this.nameListBox.Items.Add(item);
			}

			#endregion

			hideAllTongdao();
			generateTongdaoList();
			
			if (tdIndex > -1) {
				selectedTextBox = tdTextBoxes[tdIndex];
				tdTextBoxes[tdIndex].Select();
				selectedTdIndex = tdIndex;
				refreshSAPanels() ; 
			}
		}

		/// <summary>
		/// load事件：初始化起始位置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WaySetForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(lightEditorForm.Location.X + 100, lightEditorForm.Location.Y + 100);
		}

		/// <summary>
		/// 事件：点击《右上角？》按钮，提示X、Y轴微调相关设置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WaySetForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show("1.请尽量使用右侧列表中已有的通道名进行填充，便于素材保存；\n" +
					"2.X轴微调和Y轴微调，因各灯具情况不同，若非正常变化(满255进1），可在试验之后确定该微调通道的上限值，并将其填入初始值中；若将初始值设为0或255，则程序会视此通道为常规微调通道，后期不再做特殊处理；\n" +
					"3.子属性名称，请勿使用任何标点符号及空格，并尽可能简短；\n" +
					"4.点击子属性显示区，可对该子属性进行修改；点击右侧的'-'按钮，可以删除相应的子属性。",
				"使用提示或说明",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
			e.Cancel = true;
		}

		#region 点击《应用、确定、重置、关闭》等操作

		/// <summary>
		/// 事件：点击《右上角关闭》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WaySetForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			lightEditorForm.Activate();
		}

		/// <summary>
		/// 事件：点击《重置》后的操作：将所有的通道数据重设为mainForm的tongdaoList内的值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void resetButton_Click(object sender, EventArgs e)
		{
			generateTongdaoList();
			sawArray2 = SAWrapper.DeepCopy(lightEditorForm.SawArray);
			refreshSAPanels();
		}

		/// <summary>
		///  事件：点击《应用》后:确认操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param
		private void applyButton_Click(object sender, EventArgs e)
		{
			applyChange();
		}

		/// <summary>
		/// 事件：点击《确认》
		/// 1.确认操作；
		/// 2.关闭窗口。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			applyChange();
			this.Dispose();
			lightEditorForm.Activate();
		}

		/// <summary>
		/// 辅助方法：确认操作(《应用》《确认》按钮通用
		/// 1. 先检查所有的 tdTextBoxes.Text是不是为空,并设置tongdaoList的相应数据(只改tongdaoName和initValue)
		/// 2.设置tongdaoList到mainForm中
		/// </summary>
		private void applyChange()
		{

			// 1.逐一检查textBoxes值;同时设置tongdaoList值
			for (int tdIndex = 0; tdIndex < lightEditorForm.TongdaoCount; tdIndex++)
			{
				if (tdTextBoxes[tdIndex].Text == null || tdTextBoxes[tdIndex].Text.Trim() == "")
				{
					MessageBox.Show("Dickov:通道名称不得为空");
					break;
				}
				else
				{
					lightEditorForm.TongdaoList[tdIndex].TongdaoName = tdTextBoxes[tdIndex].Text.Trim();
					lightEditorForm.TongdaoList[tdIndex].InitValue = Decimal.ToInt16(tdNumericUpDowns[tdIndex].Value);
				}
			}

			// 2.设置tongdaoList到mainForm中；
			lightEditorForm.SetSawArray(sawArray2);
			lightEditorForm.ShowTds();
		}

		#endregion

		#region 通道名称相关

		/// <summary>
		/// 事件：鼠标点击tdTextBox后，更改selectedTextBox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdTextBox_MouseClick(object sender, MouseEventArgs e)
		{
			selectedTextBox = (TextBox)sender;
			if (selectedTextBox != null)
			{
				selectedTdIndex = MathHelper.GetIndexNum(selectedTextBox.Name, -1);
				if (selectedTdIndex > -1)
				{
					refreshSAPanels();
				}
			}
		}

		/// <summary>
		///  事件：双击把《右侧选择的通道名称值》填入左侧选择的《文本框》中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nameListBox_DoubleClick(object sender, EventArgs e)
		{
			if (selectedTextBox != null)
			{
				selectedTextBox.Text = (nameListBox.Text);
			}
			else {
				MessageBox.Show("请先选择通道名称文本框！");
			}
		}

		/// <summary>
		/// 事件：让滚轮每次滚动只调节一个数字
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);

			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = tdNumericUpDowns[tdIndex].Value + numericUpDown1.Increment;
				if (dd <= tdNumericUpDowns[tdIndex].Maximum)
				{
					tdNumericUpDowns[tdIndex].Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = tdNumericUpDowns[tdIndex].Value - tdNumericUpDowns[tdIndex].Increment;
				if (dd >= tdNumericUpDowns[tdIndex].Minimum)
				{
					tdNumericUpDowns[tdIndex].Value = dd;
				}
			}

		}

		/// <summary>
		///  辅助方法：通过tongdaoCount和tongdaoList，将数据填入tdPanel中，并显示对应数量的通道
		/// </summary>
		private void generateTongdaoList()
		{
			for (int i = 0; i < lightEditorForm.TongdaoCount; i++)
			{
				this.tdTextBoxes[i].Text = lightEditorForm.TongdaoList[i].TongdaoName;
				this.tdNumericUpDowns[i].Value = lightEditorForm.TongdaoList[i].InitValue;

				this.tdLabels[i].Show();
				this.tdTextBoxes[i].Show();
				this.tdNumericUpDowns[i].Show();
			}
		}

		/// <summary>
		/// 辅助方法：隐藏所有的通道
		/// </summary>
		private void hideAllTongdao()
		{
			for (int i = 0; i < 32; i++)
			{
				this.tdLabels[i].Visible = false;
				this.tdTextBoxes[i].Visible = false;
				this.tdNumericUpDowns[i].Visible = false;
			}
		}

		#endregion

		#region 子属性相关

		/// <summary>
		/// 事件：点击《添加子属性》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addSAButton_Click(object sender, EventArgs e)
		{
			int startEndValue = getCurMaxSAValue();
			new SAForm(this, -1, "", startEndValue, startEndValue).ShowDialog();
		}

		/// <summary>
		/// 事件：点击《清空子属性（当前选定通道）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saClearButton_Click(object sender, EventArgs e)
		{
			sawArray2[selectedTdIndex].SaList.Clear();
			clearSaPanels();
		}

		/// <summary>
		/// 事件：点击《saPanel内的saLabels(包括saNameLabel、startValueLabel、lineLabel、endValueLabel)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saLabel_Click(object sender, EventArgs e)
		{
			int saIndex;
			Label label = ((Label)sender);
			switch (label.Name)
			{
				case "saNameLabel": saIndex = saNameLabels.IndexOf(label); break;
				case "startValueLabel": saIndex = startValueLabels.IndexOf(label); break;
				case "lineLabel": saIndex = lineLabels.IndexOf(label); break;
				case "endValueLable": saIndex = endValueLabels.IndexOf(label); break;
				default: return;
			}
			saPanelsClick(saIndex);
		}

		/// <summary>
		/// 事件：点击《saPanel内的剩余空白区域》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saPanel_Click(object sender, EventArgs e)
		{
			int saIndex = saPanels.IndexOf((Panel)sender);
			saPanelsClick(saIndex);
		}

		/// <summary>
		/// 事件：点击《-（删除子属性）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saDeleteButton_Click(object sender, EventArgs e)
		{
			int saIndex = saDeleteButtons.IndexOf((Button)sender);
			if (saIndex == -1)
			{
				MessageBox.Show("这个按键不属于saDeleteButtons");
				return;
			}

			saFlowLayoutPanel.Controls.Remove(saPanels[saIndex]);
			saFlowLayoutPanel.Controls.Remove(saDeleteButtons[saIndex]);
			saPanels.RemoveAt(saIndex);
			saNameLabels.RemoveAt(saIndex);
			startValueLabels.RemoveAt(saIndex);
			lineLabels.RemoveAt(saIndex);
			endValueLabels.RemoveAt(saIndex);
			saDeleteButtons.RemoveAt(saIndex);

			sawArray2[selectedTdIndex].SaList.RemoveAt(saIndex);
		}

		/// <summary>
		/// 辅助方法：清空所有的SAPanel
		/// </summary>
		private void clearSaPanels()
		{
			foreach (Panel saPanel in saPanels)
			{
				saFlowLayoutPanel.Controls.Remove(saPanel);
			}
			foreach (Button saDelButton in saDeleteButtons)
			{
				saFlowLayoutPanel.Controls.Remove(saDelButton);
			}
			saPanels.Clear();
			saNameLabels.Clear();
			startValueLabels.Clear();
			lineLabels.Clear();
			endValueLabels.Clear();
			saDeleteButtons.Clear();
		}

		/// <summary>
		/// 辅助方法：刷新SAPanels
		/// </summary>
		private void refreshSAPanels()
		{
			if (selectedTdIndex > -1)
			{
				saFlowLayoutPanel.Enabled = true;
				tdNumLabel.Text = "选中的通道地址：" + (selectedTdIndex + 1);
				clearSaPanels();
				foreach (SA sa in sawArray2[selectedTdIndex].SaList)
				{
					AddSAPanel(sa);
				}
			}
			else {
				saFlowLayoutPanel.Enabled = false;
				tdNumLabel.Text = "选中的通道地址：";
				clearSaPanels();
			}
		}

		/// <summary>
		/// 辅助方法：添加saPanel，主要供SAForm回调使用
		/// </summary>
		public void AddSAPanel(string saName, int startValue, int endValue) {

			Panel saPanelTemp = new Panel();
			Label saNameLabelTemp = new Label();
			Label startLabelTemp = new Label();
			Label lineLabelTemp = new Label();
			Label endLabelTemp = new Label();			
			Button saDeleteButtonTemp = new Button();

			saPanels.Add(saPanelTemp);
			saNameLabels.Add(saNameLabelTemp);
			startValueLabels.Add(startLabelTemp);
			lineLabels.Add(lineLabelTemp);
			endValueLabels.Add(endLabelTemp);
			saDeleteButtons.Add(saDeleteButtonTemp);

			this.saFlowLayoutPanel.Controls.Add(saPanelTemp);
			this.saFlowLayoutPanel.Controls.Add(saDeleteButtonTemp);
			// 
			// saPanel
			// 
			saPanelTemp.BackColor = SystemColors.Window;
			saPanelTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			saPanelTemp.Controls.Add(saNameLabelTemp);
			saPanelTemp.Controls.Add(startLabelTemp);
			saPanelTemp.Controls.Add(lineLabelTemp);
			saPanelTemp.Controls.Add(endLabelTemp);			
			saPanelTemp.Location = new System.Drawing.Point(3, 42);
			saPanelTemp.Name = "saPanel";
			saPanelTemp.Size = new System.Drawing.Size(168, 33);
			saPanelTemp.TabIndex = 1;
			saPanelTemp.Click += new EventHandler(saPanel_Click);

			// 
			// saNameLabel
			// 
			saNameLabelTemp.Location = new System.Drawing.Point(4,9);
			saNameLabelTemp.Name = "saNameLabel";
			saNameLabelTemp.Size = new System.Drawing.Size(90, 12);
			saNameLabelTemp.TabIndex = 0;
			saNameLabelTemp.Text = saName;
			saNameLabelTemp.Click += new EventHandler(saLabel_Click);
			myToolTip.SetToolTip(saNameLabelTemp, saName);
			// 
			// startValueLabel
			// 
			startLabelTemp.Location = new System.Drawing.Point(101, 9);
			startLabelTemp.Name = "startValueLabel";
			startLabelTemp.Size = new System.Drawing.Size(23, 12);
			startLabelTemp.TabIndex = 2;
			startLabelTemp.Text = startValue.ToString();
			startLabelTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			startLabelTemp.Click += new EventHandler(saLabel_Click);
			// 
			// lineLabel
			// 
			lineLabelTemp.AutoSize = true;
			lineLabelTemp.Location = new System.Drawing.Point(128, 9);
			lineLabelTemp.Name = "lineLabel";
			lineLabelTemp.Size = new System.Drawing.Size(11, 12);
			lineLabelTemp.TabIndex = 3;
			lineLabelTemp.Text = "-";
			lineLabelTemp.Click += new EventHandler(saLabel_Click);
			// 
			// endValueLabel
			// 
			endLabelTemp.Location = new System.Drawing.Point(143, 9);
			endLabelTemp.Name = "endValueLabel";
			endLabelTemp.Size = new System.Drawing.Size(23, 12);
			endLabelTemp.TabIndex = 4;
			endLabelTemp.Text = endValue.ToString();
			endLabelTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			endLabelTemp.Click += new EventHandler(saLabel_Click);

			// 
			// saDeleteButton
			// 
			//saDeleteButtonTemp.Location = new System.Drawing.Point(170, 4);
			saDeleteButtonTemp.Name = "saDeleteButton";
			saDeleteButtonTemp.Size = new System.Drawing.Size(19, 33);
			saDeleteButtonTemp.TabIndex = 1;
			saDeleteButtonTemp.Text = "-";
			saDeleteButtonTemp.UseVisualStyleBackColor = true;
			saDeleteButtonTemp.Click += new System.EventHandler(this.saDeleteButton_Click);
		}

		/// <summary>
		/// 辅助方法：添加saPanel，主要供SAForm回调使用
		/// </summary>
		public void AddSAPanel(SA sa)
		{
			AddSAPanel(sa.SAName, sa.StartValue, sa.EndValue);
		}

		/// <summary>
		/// 辅助方法：添加SA到当前选中的通道的saList中，供SAForm回调使用
		/// </summary>
		public void AddSA(SA sa) {
			sawArray2[selectedTdIndex].SaList.Add(sa);
		}

		/// <summary>
		/// 辅助方法：在saPanels内点击任意区域，皆可弹出子属性修改栏
		/// </summary>
		/// <param name="saIndex"></param>
		private void saPanelsClick(int saIndex) {

			if (saIndex == -1)
			{
				MessageBox.Show("所点击区域不属于saPanels");
				return;
			}
			new SAForm(
				this,
				saIndex,
				saNameLabels[saIndex].Text,
				int.Parse(startValueLabels[saIndex].Text),
				int.Parse(endValueLabels[saIndex].Text)
			).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：修改子属性，主要供SAForm回调使用
		/// </summary>
		public void EditSA(int saIndex, string saName, int startValue, int endValue)
		{
			saNameLabels[saIndex].Text = saName;
			startValueLabels[saIndex].Text = startValue.ToString();
			endValueLabels[saIndex].Text = endValue.ToString();

			sawArray2[selectedTdIndex].SaList[saIndex].SAName = saName;
			sawArray2[selectedTdIndex].SaList[saIndex].StartValue = startValue;
			sawArray2[selectedTdIndex].SaList[saIndex].EndValue = endValue;
		}

		private int getCurMaxSAValue() {
			if (selectedTdIndex == -1 || sawArray2[selectedTdIndex].SaList==null || sawArray2[selectedTdIndex].SaList.Count == 0)
			{
				return 0;
			}
			else {
				List<int> endValueList = new List<int>();
				foreach (SA sa in sawArray2[selectedTdIndex].SaList)
				{
					endValueList.Add(sa.EndValue);
				}
				endValueList.Sort();//升序排序;注意：IList没有Sort方法
				int result = endValueList[endValueList.Count - 1] + 1 ;
				if (result > 255) { //若超过255，则只返回255
					result = 255;
				}
				return result;
			}
		}

		#endregion
	}
}
