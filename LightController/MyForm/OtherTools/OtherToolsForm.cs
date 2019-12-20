using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OtherTools
{
	public partial class Form1 : Form
	{

		private IList<Button> buttonList = new List<Button>(); 

		public Form1()
		{
			InitializeComponent();
		}

		private Point mouse_offset;
		private void button1_MouseDown(object sender, MouseEventArgs e)
		{
			mouse_offset = new Point(-e.X, -e.Y);
		}

		private void button1_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Point mousePos = Control.MousePosition;
				mousePos.Offset(mouse_offset.X, mouse_offset.Y);
				((Control)sender).Location = ((Control)sender).Parent.PointToClient(mousePos);
			}

			this.button1.Text = "横坐标:" + mouse_offset.X + "纵坐标" + mouse_offset.Y;
			this.button2.Text = "横坐标:" + mouse_offset.X + "纵坐标" + mouse_offset.Y;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Console.WriteLine("GGG");
		}

		private void addButton_Click(object sender, EventArgs e)
		{

			Button buttonTemp = new Button
			{
				Location = new Point(500, 500)
			};
			buttonTemp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
			buttonTemp.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button1_MouseMove);
			buttonTemp.Click += new System.EventHandler(this.addButton_Click2);

			buttonList.Add(buttonTemp);
			int buttonIndex = buttonList.Count - 1;
			buttonList[buttonIndex].Name = "button" + buttonIndex; 
			buttonList[buttonIndex].Text = buttonList[buttonIndex].Name;
			panel1.Controls.Add(buttonList[buttonList.Count-1]);

		}

		private void addButton_Click2(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			Console.WriteLine(button.Name);
		}
	}
}
