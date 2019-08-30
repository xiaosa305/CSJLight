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
		private int totalStep; // 最大步数
		

		public MultiStepForm(MainFormInterface mainForm)
		{
			this.mainForm = mainForm;
			InitializeComponent();
		}

		private void MultiStepForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 200, mainForm.Location.Y + 200);
		}
	}
}
