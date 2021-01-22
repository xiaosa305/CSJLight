using LightController.Ast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LightController.Common;

namespace LightController.MyForm
{
	public partial class MultiStepCopyForm : Form
	{
		private MainFormBase mainForm;
		private IList<StepWrapper> stepWrapperList;
		private int tongdaoCount = 0;
		private int stepCount = 0;
		private int mode;

		public MultiStepCopyForm(MainFormBase mainForm, IList<StepWrapper> stepWrapperList ,int mode,LightAst la ,int currentStep)
		{
			if (currentStep == 0 || stepWrapperList == null || stepWrapperList.Count == 0)
			{
				MessageBox.Show("步数据为空，无法复制步。");
				this.Dispose();
				return;
			}

			stepCount = stepWrapperList.Count;
			StepWrapper firstStep = stepWrapperList[0];
			tongdaoCount = firstStep.TongdaoList.Count;
			if (tongdaoCount == 0) {
				MessageBox.Show("通道数据为空，无法复制步。");
				this.Dispose();
				return;
			}

			InitializeComponent();

			this.mainForm = mainForm;
			this.stepWrapperList = stepWrapperList;
			this.mode = mode;
			lightNameLabel.Text = la.LightName + " - " + la.LightType;

			startNumericUpDown.Maximum = stepCount;			
			endNumericUpDown.Maximum = stepCount;
			startNumericUpDown.Value = currentStep; 
			endNumericUpDown.Value = currentStep ; 

			#region 初始化自定义数组等

			tdCheckBoxes[0] = checkBox1;
			tdCheckBoxes[1] = checkBox2;
			tdCheckBoxes[2] = checkBox3;
			tdCheckBoxes[3] = checkBox4;
			tdCheckBoxes[4] = checkBox5;
			tdCheckBoxes[5] = checkBox6;
			tdCheckBoxes[6] = checkBox7;
			tdCheckBoxes[7] = checkBox8;
			tdCheckBoxes[8] = checkBox9;
			tdCheckBoxes[9] = checkBox10;
			tdCheckBoxes[10] = checkBox11;
			tdCheckBoxes[11] = checkBox12;
			tdCheckBoxes[12] = checkBox13;
			tdCheckBoxes[13] = checkBox14;
			tdCheckBoxes[14] = checkBox15;
			tdCheckBoxes[15] = checkBox16;
			tdCheckBoxes[16] = checkBox17;
			tdCheckBoxes[17] = checkBox18;
			tdCheckBoxes[18] = checkBox19;
			tdCheckBoxes[19] = checkBox20;
			tdCheckBoxes[20] = checkBox21;
			tdCheckBoxes[21] = checkBox22;
			tdCheckBoxes[22] = checkBox23;
			tdCheckBoxes[23] = checkBox24;
			tdCheckBoxes[24] = checkBox25;
			tdCheckBoxes[25] = checkBox26;
			tdCheckBoxes[26] = checkBox27;
			tdCheckBoxes[27] = checkBox28;
			tdCheckBoxes[28] = checkBox29;
			tdCheckBoxes[29] = checkBox30;
			tdCheckBoxes[30] = checkBox31;
			tdCheckBoxes[31] = checkBox32;

			for (int i = 0; i < tongdaoCount ; i++){
				tdCheckBoxes[i].Text = firstStep.TongdaoList[i].TongdaoName;
				tdCheckBoxes[i].Show();
			}

			#endregion
		}

		private void MaterialForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			LanguageHelper.InitForm(this);
		}

		/// <summary>
		///  保存按钮点击后
		///  1. 验证文件名是否为空
		///  2.验证文件夹是否已存在
		///  3.取出select的所有checkBox，然后加入一个stringList中
		///  4.先试着保存materialSet.ini
		///  5.保存成功后，关闭窗口
		///  6.主界面的粘贴多步设为可用（RefreshStep())
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			// 1. 起始步、结束步的验证
			int startNum = Decimal.ToInt32(startNumericUpDown.Value);
			int endNum = Decimal.ToInt32(endNumericUpDown.Value);
			if (startNum > endNum)
			{
				MessageBox.Show("起始步不可大于结束步；请重新选择。");
				return;
			}

			// 2. 判断选择通道数
			IList<int> tdIndexList = new List<int>();
			IList<string> tdNameList = new List<string>();
			for (int i = 0; i < tongdaoCount; i++)
			{
				if (tdCheckBoxes[i].Checked)
				{
					tdIndexList.Add(i);
					tdNameList.Add(tdCheckBoxes[i].Text);
				}
			}
			if (tdIndexList.Count == 0)
			{
				MessageBox.Show("请选择至少一个通道。");
				return;
			}

			// 3.	保存相关数据			
			int selectedStepCount = endNum - startNum + 1;
			int selectedTongdaoCount = tdIndexList.Count; 
			TongdaoWrapper[,] tongdaoList =  new TongdaoWrapper[selectedStepCount, selectedTongdaoCount];

			int selectedStepIndex = 0;
			for (int stepIndex = startNum - 1;  stepIndex < endNum;  stepIndex++)
			{
				StepWrapper stepWrapper = stepWrapperList[stepIndex];
				for (int selectedTdIndex = 0; selectedTdIndex < selectedTongdaoCount ; selectedTdIndex++)
				{
					int tdIndex = tdIndexList[selectedTdIndex];
					TongdaoWrapper tongdaoWrapper = stepWrapper.TongdaoList[tdIndex];

					tongdaoList[selectedStepIndex, selectedTdIndex] = new TongdaoWrapper()
					{
						TongdaoName = tongdaoWrapper.TongdaoName,
						ScrollValue = tongdaoWrapper.ScrollValue,
						ChangeMode = tongdaoWrapper.ChangeMode,
						StepTime = tongdaoWrapper.StepTime
					};					
				}
				selectedStepIndex++;
			}

			mainForm.TempMaterialAst = new MaterialAst()
			{
				Mode = mode,
				StepCount = selectedStepCount,
				TdNameList = tdNameList,
				TongdaoList = tongdaoList
			};

			MessageBox.Show("成功复制多步。");

			this.Dispose();
			mainForm.RefreshStep();
			mainForm.Activate();
		}

		/// <summary>
		///  事件：点击《取消、右上角关闭》按钮后,Dispose窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：《通道全选框》勾选与否的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void selectAllCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < tongdaoCount; i++)
			{
				tdCheckBoxes[i].Checked = selectAllCheckBox.Checked;
			}
		}	


		/// <summary>
		/// 事件：点击《全选》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allStepSkinButton_Click(object sender, EventArgs e)
		{
			startNumericUpDown.Value = 1;
			endNumericUpDown.Value = endNumericUpDown.Maximum;
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}
	}
}
