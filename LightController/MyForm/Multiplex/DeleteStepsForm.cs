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
	public partial class DeleteStepsForm : Form
	{
		private MainFormBase mainForm;

		public DeleteStepsForm(MainFormBase mainForm,int currentStep,int totalStep)
		{
			this.mainForm = mainForm;
			InitializeComponent();

			startNumericUpDown.Maximum = totalStep;
			startNumericUpDown.Value = currentStep;
			endNumericUpDown.Maximum = totalStep;
			endNumericUpDown.Value = currentStep;			
		}

		private void DeleteStepsForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
		}

		/// <summary>
		/// 事件：点击《全选》功能
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allStepButton_Click(object sender, EventArgs e)
		{
			startNumericUpDown.Value = startNumericUpDown.Minimum;
			endNumericUpDown.Value = endNumericUpDown.Maximum;
		}		

		/// <summary>
		///  事件：点击《删除》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteButton_Click(object sender, EventArgs e)
		{
			int firstStep = decimal.ToInt32(startNumericUpDown.Value);
			int lastStep = decimal.ToInt32(endNumericUpDown.Value);

			if (lastStep < firstStep) {
				MessageBox.Show("起始步不可大于结束步；请检查后重试。");
				return; 
			}

			int stepCount = lastStep - firstStep + 1;

			string result = mainForm.DeleteSteps(firstStep, stepCount);
			if ( result == null )
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
