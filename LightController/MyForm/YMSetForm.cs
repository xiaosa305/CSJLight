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

namespace LightController.MyForm
{
	public partial class YMSetForm : Form
	{
		private MainForm mainForm;
		private IniFileAst iniFileAst;
		private bool isNew;


		public YMSetForm(MainForm mainForm,string iniPath,bool isNew)
		{
			this.mainForm = mainForm;
			iniFileAst = new IniFileAst(iniPath);
			this.isNew = isNew;
			InitializeComponent();

			#region 初始化几个数组

			ymCheckBoxes[0] = checkBox1;
			ymCheckBoxes[1] = checkBox2;
			ymCheckBoxes[2] = checkBox3;
			ymCheckBoxes[3] = checkBox4;
			ymCheckBoxes[4] = checkBox5;
			ymCheckBoxes[5] = checkBox6;
			ymCheckBoxes[6] = checkBox7;
			ymCheckBoxes[7] = checkBox8;
			ymCheckBoxes[8] = checkBox9;
			ymCheckBoxes[9] = checkBox10;
			ymCheckBoxes[10] = checkBox11;
			ymCheckBoxes[11] = checkBox12;
			ymCheckBoxes[12] = checkBox13;
			ymCheckBoxes[13] = checkBox14;
			ymCheckBoxes[14] = checkBox15;
			ymCheckBoxes[15] = checkBox16;
			ymCheckBoxes[16] = checkBox17;
			ymCheckBoxes[17] = checkBox18;
			ymCheckBoxes[18] = checkBox19;
			ymCheckBoxes[19] = checkBox20;
			ymCheckBoxes[20] = checkBox21;
			ymCheckBoxes[21] = checkBox22;
			ymCheckBoxes[22] = checkBox23;
			ymCheckBoxes[23] = checkBox24;


			jgNumericUpDowns[0] = jgNumericUpDown1;
			jgNumericUpDowns[1] = jgNumericUpDown2;
			jgNumericUpDowns[2] = jgNumericUpDown3;
			jgNumericUpDowns[3] = jgNumericUpDown4;
			jgNumericUpDowns[4] = jgNumericUpDown5;
			jgNumericUpDowns[5] = jgNumericUpDown6;
			jgNumericUpDowns[6] = jgNumericUpDown7;
			jgNumericUpDowns[7] = jgNumericUpDown8;
			jgNumericUpDowns[8] = jgNumericUpDown9;
			jgNumericUpDowns[9] = jgNumericUpDown10;
			jgNumericUpDowns[10] = jgNumericUpDown11;
			jgNumericUpDowns[11] = jgNumericUpDown12;
			jgNumericUpDowns[12] = jgNumericUpDown13;
			jgNumericUpDowns[13] = jgNumericUpDown14;
			jgNumericUpDowns[14] = jgNumericUpDown15;
			jgNumericUpDowns[15] = jgNumericUpDown16;
			jgNumericUpDowns[16] = jgNumericUpDown17;
			jgNumericUpDowns[17] = jgNumericUpDown18;
			jgNumericUpDowns[18] = jgNumericUpDown19;
			jgNumericUpDowns[19] = jgNumericUpDown20;
			jgNumericUpDowns[20] = jgNumericUpDown21;
			jgNumericUpDowns[21] = jgNumericUpDown22;
			jgNumericUpDowns[22] = jgNumericUpDown23;
			jgNumericUpDowns[23] = jgNumericUpDown24;


			zxNumericUpDowns[0] = zxNumericUpDown1;
			zxNumericUpDowns[1] = zxNumericUpDown2;
			zxNumericUpDowns[2] = zxNumericUpDown3;
			zxNumericUpDowns[3] = zxNumericUpDown4;
			zxNumericUpDowns[4] = zxNumericUpDown5;
			zxNumericUpDowns[5] = zxNumericUpDown6;
			zxNumericUpDowns[6] = zxNumericUpDown7;
			zxNumericUpDowns[7] = zxNumericUpDown8;
			zxNumericUpDowns[8] = zxNumericUpDown9;
			zxNumericUpDowns[9] = zxNumericUpDown10;
			zxNumericUpDowns[10] = zxNumericUpDown11;
			zxNumericUpDowns[11] = zxNumericUpDown12;
			zxNumericUpDowns[12] = zxNumericUpDown13;
			zxNumericUpDowns[13] = zxNumericUpDown14;
			zxNumericUpDowns[14] = zxNumericUpDown15;
			zxNumericUpDowns[15] = zxNumericUpDown16;
			zxNumericUpDowns[16] = zxNumericUpDown17;
			zxNumericUpDowns[17] = zxNumericUpDown18;
			zxNumericUpDowns[18] = zxNumericUpDown19;
			zxNumericUpDowns[19] = zxNumericUpDown20;
			zxNumericUpDowns[20] = zxNumericUpDown21;
			zxNumericUpDowns[21] = zxNumericUpDown22;
			zxNumericUpDowns[22] = zxNumericUpDown23;
			zxNumericUpDowns[23] = zxNumericUpDown24;

			for (int i = 0; i < 24; i++)
			{
				ymCheckBoxes[i].CheckedChanged += new EventHandler(ymCheckBox_CheckedChanged);
				jgNumericUpDowns[i].ValueChanged += new EventHandler(jgNumericUpDown_ValueChanged);
				zxNumericUpDowns[i].ValueChanged += new EventHandler(zxNumericUpDown_ValueChanged);
			}

			#endregion
		}

		private void ymCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			int frameIndex = MathAst.getIndexNum(((CheckBox)sender).Name, -1 );
				
		}
		private void jgNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int frameIndex = MathAst.getIndexNum(((NumericUpDown)sender).Name, -1);
			//MessageBox.Show("frameIndex:" + ((NumericUpDown)sender).Value);
		}
		private void zxNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int frameIndex = MathAst.getIndexNum(((NumericUpDown)sender).Name, -1);
			//MessageBox.Show("frameIndex:" + ((NumericUpDown)sender).Value);
		}

		private void YMSetForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);

			//1. 先设置各个下拉框的默认值：这里的三个选项(checkbox和numericUpDown)都不太需要设置
			//2.读取各个配置=>若是新建，则不读取配置
			if (!isNew)
			{
				loadAll();				
			}
		}

		/// <summary>
		/// 辅助方法：从配置文件读取配置，并填入所有的框中
		/// </summary>
		private void loadAll()
		{
			for (int i = 0; i <24; i++)
			{
				ymCheckBoxes[i].Checked = ( iniFileAst.ReadInt("YM", i + "CK", 0) == 1);
				jgNumericUpDowns[i].Value = new decimal(iniFileAst.ReadInt("YM", i + "JG", 1) );
				zxNumericUpDowns[i].Value = new decimal(iniFileAst.ReadInt("YM", i + "ZX", 1));
			}
		}

		/// <summary>
		///  点击统一间隔时间按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonJGButton_Click(object sender, EventArgs e)
		{
			foreach (NumericUpDown item in jgNumericUpDowns)
			{
				item.Value = commonJGNumericUpDown.Value;
			}
		}

		/// <summary>
		///  点击统一执行时间按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonZXButton_Click(object sender, EventArgs e)
		{
			foreach (NumericUpDown item in zxNumericUpDowns)
			{
				item.Value = commonZXNumericUpDown.Value;
			}
		}

		/// <summary>
		///  点击保存设置后，将所需的数据保存到global.ini中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ymSaveButton_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < 24; i++)
			{
				iniFileAst.WriteInt("YM", i + "CK", ymCheckBoxes[i].Checked?1:0);
				iniFileAst.WriteInt("YM", i + "JG", Decimal.ToInt16(jgNumericUpDowns[i].Value) );
				iniFileAst.WriteInt("YM", i + "ZX", Decimal.ToInt16(zxNumericUpDowns[i].Value));
			}
			MessageBox.Show("保存成功:");
		}

		private void allCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			foreach (CheckBox item in ymCheckBoxes)
			{
				item.Checked = allCheckBox.Checked;
			}
		}

		private void YMSetForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
		}
	}
}
