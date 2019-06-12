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
	public partial class GlobalSetForm : Form
	{
		public MainForm mainForm;
		
		public GlobalSetForm(MainForm mainForm) {
			this.mainForm = mainForm;
			InitializeComponent();
		}


		private void GlobalSetForm_Load(object sender, EventArgs e)
		{
			jidianqiFrameComboBox.SelectedIndex = 0;
			zuheFrameComboBox.SelectedIndex = 0;
			startupComboBox.SelectedIndex = 0;
			tongdaoCountComboBox.SelectedIndex = 0;
			frame1ComboBox.SelectedIndex = 0;
			frame2ComboBox.SelectedIndex = 0;
			frame3ComboBox.SelectedIndex = 0;
			frame4ComboBox.SelectedIndex = 0;

		}


		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void zuheCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (zuheCheckBox.Checked)
			{
				zuheEnableGroupBox.Enabled = true;
			}
			else {
				zuheEnableGroupBox.Enabled = false;
			}
		}

		/// <summary>
		///  读取文件，取出其中的数据，并将参数设置到zuheGroupBox和zuheCheckBox中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zuheFrameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{





		}
	}
}
