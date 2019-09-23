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
	public partial class UseFrameForm : Form
	{
		private MainFormInterface mainForm; 

		public UseFrameForm(MainFormInterface mainForm, string currentFrameName)
		{
			InitializeComponent();

			this.mainForm = mainForm;
			foreach (string frameName in MainFormInterface.AllFrameList) {
				if ( ! currentFrameName.Equals(frameName)) {
					frameSkinComboBox.Items.Add(frameName);
				}				
			}
			frameSkinComboBox.SelectedIndex = 0;

		}


		private void UseFrameForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 300, mainForm.Location.Y + 300);
		}

		
		/// <summary>
		/// 事件：点击《取消、右上角关闭》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelSkinButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：点击《确定》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterSkinButton_Click(object sender, EventArgs e)
		{
			mainForm.UseOtherForm(frameSkinComboBox.Text);

		}

	}
}
