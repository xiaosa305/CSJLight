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
	public partial class MaterialForm : Form
	{
		private MainForm mainForm;
		private List<StepWrapper> stepWrapperList;

		public MaterialForm(MainForm mainForm, List<StepWrapper> stepWrapperList)
		{

			if (stepWrapperList == null || stepWrapperList.Count == 0)
			{
				MessageBox.Show("传入的数据为空，无法生成素材:");
				this.Dispose();
				return;
			}

			InitializeComponent();
			this.mainForm = mainForm;
			this.stepWrapperList = stepWrapperList;

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

			StepWrapper firstStep = stepWrapperList[0];
			for (int i = 0; i < firstStep.TongdaoList.Count; i++){
				tdCheckBoxes[i].Text = firstStep.TongdaoList[i].TongdaoName;
				tdCheckBoxes[i].Show();
			}			

			#endregion
			



		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
