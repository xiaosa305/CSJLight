using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LBDConfigTool
{
	public partial class SpecialForm : Form
	{
		public SpecialForm(ConfForm confForm)
		{
			InitializeComponent();
		}

		private void SpecialForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
		}
	}
}
