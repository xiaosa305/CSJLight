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
	public partial class SoundMultiForm : Form
	{
		private MainFormBase mainForm;

		public SoundMultiForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;

			InitializeComponent();
		}

		/// <summary>
		///  Load方法内设定窗口位置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SoundMultiForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
		}
	}
}
