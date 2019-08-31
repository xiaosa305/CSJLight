using LightController.Ast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm
{
	public partial class MultiStepForm : Form
	{

		private MainFormInterface mainForm;
		private int currentStep; // 当前步
		private int totalStep ;  // 最大步数
		private StepWrapper stepTemplate; //传入模板步，用以提取通道名列表
		private int mode;

		public MultiStepForm(MainFormInterface mainForm, int currentStep,int totalStep,StepWrapper stepTemplate, int mode)
		{
			this.mainForm = mainForm;
			this.currentStep = currentStep;
			this.totalStep = totalStep;
			this.stepTemplate = stepTemplate;			
			this.mode = mode;

			InitializeComponent();

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

			for (int i = 0; i < stepTemplate.TongdaoList.Count; i++)
			{
				TongdaoWrapper td = stepTemplate.TongdaoList[i];
				tdCheckBoxes[i].Text = td.Address + ":" + td.TongdaoName;
				tdCheckBoxes[i].Show();
			}

			#endregion

			startNumericUpDown.Maximum = totalStep;
			startNumericUpDown.Value = currentStep;
			endNumericUpDown.Maximum = totalStep;
			endNumericUpDown.Value = currentStep;

			if (mode == 0)
			{
				modeLabel.Text = "当前模式：常规模式";
				this.cmComboBox.Items.Add("跳变");
				this.cmComboBox.Items.Add("渐变");
				this.cmComboBox.Items.Add("屏蔽");
				this.cmComboBox.SelectedIndex = 0;
			}
			else {
				modeLabel.Text = "当前模式：音频模式";

				this.commonChangeModeSkinButton.Text = "是否声控" ; 
				this.cmComboBox.Items.Add("否");
				this.cmComboBox.Items.Add("是");				
				this.cmComboBox.SelectedIndex = 0;

				stNumericUpDown.Hide();
				commonStepTimeSkinButton.Hide();
			}



		}

		private void MultiStepForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 200, mainForm.Location.Y + 200);
		}




	}
}
