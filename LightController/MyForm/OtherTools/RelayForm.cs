using CCWin.SkinControl;
using LightController.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.MyForm.OtherTools
{
	public partial class RelayForm : Form
	{
		private MainFormBase mainForm;
		private int relayCount = 7;

		private Panel[] relayPanels ;
		private SkinButton[] relayButtons;
		private TextBox[] relayTBs;
		private Panel[] timePanels; 
		private NumericUpDown[] timeNUDs;

		public RelayForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;
			InitializeComponent();

			//初始化各个 继电器开关、名称和时延 的相关控件，填入数组中，方便调用；
			relayPanels = new Panel[relayCount];
			relayButtons = new SkinButton[relayCount];
			relayTBs = new TextBox[relayCount];
			timePanels = new Panel[relayCount - 1];
			timeNUDs = new NumericUpDown[relayCount - 1];

			for (int relayIndex = 0; relayIndex < relayCount; relayIndex++)
			{

				relayButtons[relayIndex] = new SkinButton
				{
					BackColor = relayButtonDemo.BackColor,
					BaseColor = relayButtonDemo.BaseColor,
					BorderColor = relayButtonDemo.BorderColor,
					ControlState = relayButtonDemo.ControlState,
					DownBack = relayButtonDemo.DownBack,
					DrawType = relayButtonDemo.DrawType,
					Font = relayButtonDemo.Font,
					ForeColor = relayButtonDemo.ForeColor,
					ForeColorSuit = relayButtonDemo.ForeColorSuit,
					ImageAlign = relayButtonDemo.ImageAlign,
					ImageList = relayButtonDemo.ImageList,
					ImageKey = relayButtonDemo.ImageKey,
					ImageSize = relayButtonDemo.ImageSize,
					InheritColor = relayButtonDemo.InheritColor,
					IsDrawBorder = relayButtonDemo.IsDrawBorder,
					Location = relayButtonDemo.Location,
					Margin = relayButtonDemo.Margin,
					MouseBack = relayButtonDemo.MouseBack,
					NormlBack = relayButtonDemo.NormlBack,
					Size = relayButtonDemo.Size,
					Tag = relayButtonDemo.Tag,
					TextAlign = relayButtonDemo.TextAlign,
					UseVisualStyleBackColor = relayButtonDemo.UseVisualStyleBackColor,
					Visible = true,
					Name = "switchButtons" + (relayIndex + 1),
					Text = LanguageHelper.TranslateWord("开关") + (relayIndex + 1)
				};

				relayTBs[relayIndex] = new TextBox
				{
					Location = relayTBDemo.Location,
					Size = relayTBDemo.Size,
					TextAlign = relayTBDemo.TextAlign
				};

				relayPanels[relayIndex] = new Panel
				{
					Location = relayPanelDemo.Location,
					Size = relayPanelDemo.Size
				};

				relayPanels[relayIndex].Controls.Add(relayButtons[relayIndex]);
				relayPanels[relayIndex].Controls.Add(relayTBs[relayIndex]);
				relayFLP.Controls.Add(relayPanels[relayIndex]);
			}

			relayFLP.Controls.Add(labelPanel); // 调整labelPanel的位置

			for (int timeIndex = 0; timeIndex < relayCount - 1; timeIndex++)
			{
				timeNUDs[timeIndex] = new NumericUpDown
				{
					Location = timeNUDDemo.Location,
					Maximum = timeNUDDemo.Maximum,
					Minimum = timeNUDDemo.Minimum,
					Size = timeNUDDemo.Size,
					TextAlign = timeNUDDemo.TextAlign,
					Value = timeNUDDemo.Value
				};

				timePanels[timeIndex] = new Panel
				{
					Location = timePanelDemo.Location,
					Size = timePanelDemo.Size
				};

				timePanels[timeIndex].Controls.Add(timeNUDs[timeIndex]);
				relayFLP.Controls.Add(timePanels[timeIndex]);
			}
		}

		private void RelayForm_Load(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// 事件：点击《开台》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openButton_Click(object sender, EventArgs e)
		{
			int relayTime = 1;

			Enabled = false;

			//relayButton1.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();
			//Thread.Sleep(relayTime * 1000);			
			//relayButton2.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();
			//Thread.Sleep(relayTime * 1000);
			//relayButton3.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();
			//Thread.Sleep(relayTime * 1000);
			//relayButton4.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();
			//Thread.Sleep(relayTime * 1000);
			//relayButton5.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();
			//Thread.Sleep(relayTime * 1000);
			//relayButton6.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();
			//Thread.Sleep(relayTime * 1000);
			//relayButton7.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();

			Enabled = true;
		}

		/// <summary>
		/// 事件：点击《关台》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void closeButton_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 事件：点击《回读配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void readButton_Click(object sender, EventArgs e)
		{
						   
		}


	}
}
