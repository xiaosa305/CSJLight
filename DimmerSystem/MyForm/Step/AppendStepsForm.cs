using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.Step
{
    public partial class AppendStepsForm : UIForm
    {
		private MainFormBase mainForm;

		public AppendStepsForm(MainFormBase mainForm, int retainStep)
		{
			this.mainForm = mainForm;
			InitializeComponent();

			stepCountNUD.Minimum = 1;
			stepCountNUD.Maximum = retainStep;
			stepCountNUD.Value = 1;

		}

		private void AppendStepsForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			//LanguageHelper.InitForm(this);
		}

		/// <summary>
		/// 事件：点击《追加步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void appendButton_Click(object sender, EventArgs e)
		{
			string result = mainForm.AddSteps(decimal.ToInt32(stepCountNUD.Value));
			if (result == null)
			{
				Dispose();
				mainForm.Activate();
			}
			else
			{
				MessageBox.Show(result);
			}
		}

		/// <summary>
		/// 事件：点击《取消》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Dispose();
			mainForm.Activate();
		}
		       
    }
}
