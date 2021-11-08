using LightController.Common;
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
    public partial class DeleteStepsForm : UIForm
    {
		private MainFormBase mainForm;
		public DeleteStepsForm(MainFormBase mainForm, int currentStep, int totalStep)
		{
			this.mainForm = mainForm;
			InitializeComponent();

			startNUD.Maximum = totalStep;
			startNUD.Value = currentStep;
			endNUD.Maximum = totalStep;
			endNUD.Value = currentStep;
		}

		private void DeleteStepsForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			//LanguageHelper.InitForm(this);
		}

		/// <summary>
		/// 事件：点击《全选》功能
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allStepButton_Click(object sender, EventArgs e)
		{
			startNUD.Value = startNUD.Minimum;
			endNUD.Value = endNUD.Maximum;
		}

		/// <summary>
		///  事件：点击《删除》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteButton_Click(object sender, EventArgs e)
		{
			int startStep = decimal.ToInt32(startNUD.Value);
			int lastStep = decimal.ToInt32(endNUD.Value);

			if (lastStep < startStep)
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("起始步不可大于结束步；请检查后重试。"));
				return;
			}

			int stepCount = lastStep - startStep + 1;

			string result = mainForm.DeleteSteps(startStep, stepCount);
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
