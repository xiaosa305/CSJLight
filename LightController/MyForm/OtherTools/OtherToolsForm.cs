using CCWin.SkinControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OtherTools
{
	public partial class OtherToolsForm : Form
	{

		private IList<Button> buttonList = new List<Button>();

		public OtherToolsForm()
		{
			InitializeComponent();

			#region 初始化lightButtons数组

			lightButtons[0] = lightButton1;
			lightButtons[1] = lightButton2;
			lightButtons[2] = lightButton3;
			lightButtons[3] = lightButton4;
			lightButtons[4] = lightButton5;
			lightButtons[5] = lightButton6;
			lightButtons[6] = lightButton7;
			lightButtons[7] = lightButton8;
			lightButtons[8] = lightButton9;
			lightButtons[9] = lightButton10;
			lightButtons[10] = lightButton11;
			lightButtons[11] = lightButton12;
			lightButtons[12] = lightButton13;
			lightButtons[13] = lightButton14;
			lightButtons[14] = lightButton15;
			lightButtons[15] = lightButton16;
			lightButtons[16] = lightButton17;
			lightButtons[17] = lightButton18;
			lightButtons[18] = lightButton19;
			lightButtons[19] = lightButton20;
			lightButtons[20] = lightButton21;
			lightButtons[21] = lightButton22;
			lightButtons[22] = lightButton23;
			lightButtons[23] = lightButton24;

			foreach (SkinButton lightButton in lightButtons)
			{
				lightButton.Click += new System.EventHandler(this.lightButton_Click);
			}

			#endregion
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

			//this.button1.Text = "横坐标:" + mouse_offset.X + "纵坐标" + mouse_offset.Y;
			//this.button2.Text = "横坐标:" + mouse_offset.X + "纵坐标" + mouse_offset.Y;
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
			panel1.Controls.Add(buttonList[buttonList.Count - 1]);

		}

		private void addButton_Click2(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			Console.WriteLine(button.Name);
		}

		private void commonButton_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			Console.WriteLine(button.Name);
		}

		private void OtherToolsForm_Load(object sender, EventArgs e)
		{
			DirectoryInfo fdir = new DirectoryInfo(Application.StartupPath + "\\irisSkins");
			try
			{
				FileInfo[] file = fdir.GetFiles();
				if (file.Length > 0)
				{
					foreach (var item in file)
					{
						if (item.FullName.EndsWith(".ssk"))
						{
							skinComboBox.Items.Add(item.Name.Substring(0, item.Name.Length - 4));
						}
					}
					skinComboBox.SelectedIndex = 0;

					skinComboBox.Show();
					skinChangeButton.Show();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void skinChangeButton_Click(object sender, EventArgs e)
		{
			string sskName = skinComboBox.Text;
			this.skinEngine1.SkinFile = Application.StartupPath + "\\irisSkins\\" + sskName + ".ssk";
		}

		private void groupBox2_Enter(object sender, EventArgs e)
		{

		}



		private void lightButton_Click(object sender, EventArgs e)
		{
			SkinButton btn = (SkinButton)sender;
			btn.ImageIndex = btn.ImageIndex == 2 ? 1 : 2;
		}

		private void lcLoadButton_Click(object sender, EventArgs e)
		{
			string filePath = @"C:\Users\Dickov\Desktop\2.cfg";
			IList<string> paramList = new List<string>();
			try
			{
				if (File.Exists(filePath))
				{
					using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
					using (StreamReader sr = new StreamReader(fs))
					{
						string sTemp;
						while ((sTemp = sr.ReadLine()) != null)
						{
							if (sTemp.Length > 0)
							{
								paramList.Add(sTemp);
							}
						}
					}
				}
				else
				{
					MessageBox.Show("文件不存在");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			foreach (string param in paramList)
			{
				Console.WriteLine(param);
			}

		}

		private void lcDownloadButton_Click(object sender, EventArgs e)
		{

		}
	}
}
