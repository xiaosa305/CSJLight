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
	public partial class ColorForm : Form
	{
		public ColorForm()
		{
			InitializeComponent();
		}

		List<Color> colorList = new List<Color>();
		List<int> stList = new List<int>();
		List<bool> cmList = new List<bool>();

		private void colorAddButton_Click(object sender, EventArgs e)
		{
			myColorDialog.ShowDialog();
			colorList.Add(myColorDialog.Color);

			Panel colorPanel = new Panel()
			{
				Size = colorPanelDemo.Size,
				Margin = colorPanelDemo.Margin,
				Visible = true,
				BackColor = myColorDialog.Color,
			};
			colorPanel.Click += ColorPanel_Click;






			colorFLP.Controls.Add(colorPanel);

		}


		private void ColorPanel_Click(object sender, EventArgs e)
		{
			Panel colorPanel = sender as Panel;
			colorPanel.BorderStyle = BorderStyle.Fixed3D;
			Console.WriteLine(colorFLP.Controls.IndexOf(colorPanel));

		}

		private void colorMinusButton_Click(object sender, EventArgs e)
		{

		}

		private void colorPanelDemo_Paint(object sender, PaintEventArgs e)
		{

		}

	}
}
