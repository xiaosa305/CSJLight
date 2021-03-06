using LightController.Ast;
using LightController.Common;
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
	public partial class MultiplexForm : Form
	{
		private MainFormBase mainForm;		
		private int stepCount;
		private bool isInit = false;  //是否初始化
		private bool isSyncMode ;
		private IList<int> selectedIndices;

		public MultiplexForm(MainFormBase mainForm, IList<LightAst> lightAstList, int stepCount, bool isSyncMode ,IList<int> selectedIndices ) 
		{
			InitializeComponent();

			this.mainForm = mainForm;
			this.stepCount = stepCount ;
			this.isSyncMode = isSyncMode;

			startNumericUpDown.Maximum = stepCount;			
			endNumericUpDown.Maximum = stepCount;
			endNumericUpDown.Value = stepCount;

			refreshMaxTimes( stepCount );
			
			for (int lightIndex = 0; lightIndex < lightAstList.Count; lightIndex++)
			{
				if ( isSyncMode || selectedIndices.Contains(lightIndex))
				{
					ListViewItem item = new ListViewItem();
					item.SubItems.Add(lightAstList[lightIndex].LightType);
					item.SubItems.Add(lightAstList[lightIndex].LightAddr);
					item.SubItems.Add(lightAstList[lightIndex].Remark);				
					lightsListView.Items.Add(item);
				}
			}

			//非同步模式，需要进行相关操作
			if ( !isSyncMode ) {
				lightsListView.CheckBoxes = false;
				allCheckBox.Visible = false;
				noticeLabel.Visible = false;
				this.selectedIndices = selectedIndices;
				Text = "多步复用（非同步）";
			}
		}

		/// <summary>
		///  窗口Load方法：作用是初始化窗体位置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MultiplexForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			LanguageHelper.InitForm(this);
			LanguageHelper.TranslateListView(lightsListView);

			lightsListView.HideSelection = true;    //主动设置一下这个属性，避免被VS吃掉设置
			
			allCheckBox.Checked = true; //默认勾选所有灯具

			isInit = true;
		}

		/// <summary>
		/// 事件：点击《（右上角）?》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MultiplexForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show(
				LanguageHelper.TranslateSentence("1.点击灯具左侧的方格，或双击灯具，皆可选中灯具；\n" +
					"2.使用同步复用功能，将会复制选中灯具的指定步数，并将这些步粘贴到最大步之后；\n" +
					"3.用户可通过更改复用次数输入框的数值，自行设置复制的次数；\n" +
					"4.未选中的灯具，仍会添加相应数量的新步，并以该灯具最大步的数据填充新步；"),
				LanguageHelper.TranslateSentence("提示"),
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
				);
			e.Cancel = true;
		}

		/// <summary>
		/// 事件：勾选或取消勾选《灯具全选》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allCheckBox_CheckedChanged(object sender, EventArgs e)
		{			
			foreach (ListViewItem item in lightsListView.Items)
			{
				item.Checked = allCheckBox.Checked;
			}			
		}

		/// <summary>
		/// 事件：更改《start|endNumericUpDown》的值，需要及时修改复用次数的上限
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void startEndNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			// 避免一进Form，就跑这个方法
			if (!isInit)
			{
				return;
			}

			// 计算将要复用的步数
			int copyStepCount = decimal.ToInt32(endNumericUpDown.Value) - decimal.ToInt32(startNumericUpDown.Value) + 1;

			// 当输入（或点选）的步数值不合规时，处理输入框
			if (copyStepCount < 1)
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("起始步不可大于结束步。"));
				if (((NumericUpDown)sender).Name.Equals("startNumericUpDown"))
				{
					startNumericUpDown.ValueChanged -= new System.EventHandler(this.startEndNumericUpDown_ValueChanged);
					startNumericUpDown.Value = endNumericUpDown.Value;
					startNumericUpDown.ValueChanged += new System.EventHandler(this.startEndNumericUpDown_ValueChanged);
				}
				else
				{
					endNumericUpDown.ValueChanged -= new System.EventHandler(this.startEndNumericUpDown_ValueChanged);
					endNumericUpDown.Value = startNumericUpDown.Value;
					endNumericUpDown.ValueChanged += new System.EventHandler(this.startEndNumericUpDown_ValueChanged);
				}
				// 处理步数后，直接用1，设置最大复用次数
				refreshMaxTimes(1);
			}
			// 若输入值合规，直接通过步数设置最大复用次数
			else
			{
				refreshMaxTimes(copyStepCount);
			}
		}

		/// <summary>
		/// 事件：点击《（步数）全选》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allStepButton_Click(object sender, EventArgs e)
		{
			startNumericUpDown.Value = startNumericUpDown.Minimum;
			endNumericUpDown.Value = endNumericUpDown.Maximum;
		}

		/// <summary>
		/// 事件：点击《复用》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			//若为同步模式，则需要重新填充selectedIndices
			if ( isSyncMode) { 
				selectedIndices = new List<int>();
				foreach (int item in lightsListView.CheckedIndices)
				{
					selectedIndices.Add(item);
				}

				if (selectedIndices == null || selectedIndices.Count == 0) {
					MessageBox.Show(LanguageHelper.TranslateSentence("请选择至少一个灯具，否则无法使用复用功能。"));
					return;
				}
			}

			string result = mainForm.MultiplexSteps(selectedIndices,	
				decimal.ToInt32(startNumericUpDown.Value),
				decimal.ToInt32(endNumericUpDown.Value),
				decimal.ToInt32(timesNumericUpDown.Value)
				);

			if( result == null)
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("成功复用多灯多步。"));
				Dispose();
				mainForm.Activate();
			}
			else
			{
				MessageBox.Show(LanguageHelper.TranslateSentence(result));
			}
		}

		/// <summary>
		/// 事件：点击《取消》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Dispose();
			mainForm.Activate();
		}		

		/// <summary>
		/// 辅助方法：通过相应的值，刷新复用次数的最大值
		/// </summary>
		private void refreshMaxTimes(int copyStepCount) {
			timesNumericUpDown.Maximum = (MainFormBase.MAX_STEP - stepCount) / copyStepCount;
		}
	
	}
}
