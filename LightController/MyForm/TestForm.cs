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
	public partial class TestForm : Form
	{
		public TestForm()
		{
			InitializeComponent();

			labelFlowLayoutPanel.MouseWheel += new MouseEventHandler(this.valueNumericUpDown_MouseWheel);
		}

		private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{
			

			labelFlowFollowScroll();
		}

		private void labelFlowFollowScroll()
		{
			Console.WriteLine(flowLayoutPanel1.AutoScrollPosition);
			labelFlowLayoutPanel.AutoScrollPosition = new Point(0,- flowLayoutPanel1.AutoScrollPosition.Y);
		}

		private void valueNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}		
		}
	}
}
