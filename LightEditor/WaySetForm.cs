using LightEditor.Common;
using System;
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
		private int tongdaoCount;
		private List<TongdaoWrapper> tongdaoList;
		private MainForm mainForm; 

		/// <summary>
		///  初始化，并将mainForm（及其相关内容）也传进来；并显示tdPanel相关数据
		/// </summary>
		/// <param name="mainForm"></param>
		public WaySetForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
			this.tongdaoCount = mainForm.tongdaoCount;
			this.tongdaoList = mainForm.tongdaoList;
				
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

			foreach(NumericUpDown item in tdNumericUpDowns)
			{
				item.MouseWheel += new MouseEventHandler(valueNumericUpDown_MouseWheel);
			}

			// 动态添加通道预选名称
			IList<string> tdNameList = TextAst.Read(Application.StartupPath + @"\PreTDNameList");
			foreach (string item in tdNameList)
			{
				this.nameListBox.Items.Add(item);
			}

			#endregion

			hideAllTongdao();
			generateTongdaoList();			

		}

		/// 让滚轮每次滚动只调节一个数字
		private void valueNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			int tdIndex = MathAst.getIndexNum(((NumericUpDown)sender).Name, -1);

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
		/// 辅助方法：隐藏所有的通道
		/// </summary>
		private void hideAllTongdao()
		{
			for (int i = 0; i < 32; i++)
			{
				this.tdLabels[i].Visible = false ;
				this.tdTextBoxes[i].Visible = false;
				this.tdNumericUpDowns[i].Visible = false;
			}
		}

		/// <summary>
		///  辅助方法：通过tongdaoCount和tongdaoList，将数据填入tdPanel中，并显示对应数量的通道
		/// </summary>
		private void generateTongdaoList()
		{
			for (int i = 0; i < tongdaoCount; i++)
			{
				this.tdTextBoxes[i].Text = tongdaoList[i].TongdaoName;
				this.tdNumericUpDowns[i].Value = tongdaoList[i].InitValue;

				this.tdLabels[i].Show();
				this.tdTextBoxes[i].Show();
				this.tdNumericUpDowns[i].Show();
			}
		}
		
		/// <summary>
		///  双击把右侧选择的通道名称值填入左侧选择的文本框中
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
		/// 辅助变量，用来记录鼠标选择的textBox
		/// </summary>
		private TextBox selectedTextBox = null;
		/// <summary>
		/// 鼠标点击tdTextBox后，更改selectedTextBox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdTextBox_MouseClick(object sender, MouseEventArgs e)
		{		
			selectedTextBox = ((TextBox)sender);
		}

		/// <summary>
		/// 点击重置后的操作：将所有的通道数据重设为mainForm的tongdaoList内的值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void resetButton_Click(object sender, EventArgs e)
		{
			tongdaoCount = mainForm.tongdaoCount;
			tongdaoList = mainForm.tongdaoList ;
			generateTongdaoList();
		}

		/// <summary>
		/// 点击确认按钮后：
		/// 1. 确认操作；
		/// 2.关闭窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			enterAndApply();
			this.Dispose();
			mainForm.Activate();
		}

		/// <summary>
		///  点击应用后:确认操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param
		private void applyButton_Click(object sender, EventArgs e)
		{
			enterAndApply();
		}

		/// <summary>
		/// 辅助方法：确认操作(《应用》《确认》按钮通用
		/// 1. 先检查所有的 tdTextBoxes.Text是不是为空,并设置tongdaoList的相应数据(只改tongdaoName和initValue)
		/// 2.设置tongdaoList到mainForm中
		/// </summary>
		private void enterAndApply() {
			// 1.逐一检查textBoxes值;同时设置tongdaoList值
			for (int i = 0; i < tongdaoCount; i++)
			{
				if (tdTextBoxes[i].Text == null || tdTextBoxes[i].Text.Trim() == "")
				{
					MessageBox.Show("Dickov:通道名称不得为空");
					break;
				}
				else
				{
					tongdaoList[i].TongdaoName = tdTextBoxes[i].Text.Trim();
					tongdaoList[i].InitValue = Decimal.ToInt16(tdNumericUpDowns[i].Value);
				}
			}

			// 2.设置tongdaoList到mainForm中；
			mainForm.SetTongdaoList(this.tongdaoList);

		}

		private void WaySetForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}
	}
}
