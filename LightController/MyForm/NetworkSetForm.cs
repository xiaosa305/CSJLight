using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LighEditor.MyForm
{
	public partial class NetworkSetForm : Form
	{

		private MainForm mainForm;

		public NetworkSetForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
			InitializeComponent();
		}

		private void NetworkSetForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
		}

		private void NetworkSetForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}
	}
}
