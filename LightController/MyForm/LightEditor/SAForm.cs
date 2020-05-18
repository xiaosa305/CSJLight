using LightEditor.Ast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightEditor.MyForm
{
	public partial class SAForm : Form
	{
		public WaySetForm wsForm;
		private int saIndex = -1;

		public SAForm(WaySetForm wsForm , int saIndex, string saName,int startValue, int endValue)
		{
			this.wsForm = wsForm;
			this.saIndex = saIndex ;

			InitializeComponent();

			Text = saIndex == -1 ? "添加子属性": "修改子属性";					
			saNameTextBox.Text = saName;
			startValueNumericUpDown.Value = startValue;
			endValueNumericUpDown.Value = endValue;
		}		

		private void SAForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(wsForm.Location.X + 100, wsForm.Location.Y + 100);
		}

		/// <summary>
		/// 事件：点击《(右上角)？》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SAForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show("子属性名称，请勿使用任何标点符号及空格，并尽可能简短。",
				"使用提示或说明",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
			e.Cancel = true;
		}

		/// <summary>
		/// 辅助方法：检查截止数值是否大于等于起始数值
		/// </summary>
		/// <returns></returns>
		private bool checkStartAndEnd()
		{
			if (startValueNumericUpDown.Value > endValueNumericUpDown.Value) {
				return false;
			}
			return true;
		}

		/// <summary>
		/// 辅助方法：检查SAName是否符合要求
		/// </summary>
		/// <returns></returns>
		private bool checkSAName() {

			if (String.IsNullOrEmpty(saNameTextBox.Text.Trim()))
			{
				MessageBox.Show("子属性名不得为空!");
				return false;
			}

			return true;
		}

		/// <summary>
		/// 事件：点击《确定》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{		
			if (!checkSAName()) {
				return;
			}
			if (!checkStartAndEnd())
			{
				MessageBox.Show("截止值不能小于起始值,请检查后重试。");
				return;
			}
			if ( saIndex == -1)
			{
				wsForm.AddSAPanel( saNameTextBox.Text.Trim(), Decimal.ToInt16(startValueNumericUpDown.Value), Decimal.ToInt16(endValueNumericUpDown.Value)) ;
				wsForm.AddSA(new SA() {
					SAName = saNameTextBox.Text.Trim(),
					StartValue = Decimal.ToInt16(startValueNumericUpDown.Value),
					EndValue = Decimal.ToInt16(endValueNumericUpDown.Value)
				} );
			}
			else {
				wsForm.EditSA(saIndex, saNameTextBox.Text, Decimal.ToInt16(startValueNumericUpDown.Value), Decimal.ToInt16(endValueNumericUpDown.Value));
			}
			
			this.Dispose();
			wsForm.Activate();
		}

		/// <summary>
		/// 事件：点击《取消》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			wsForm.Activate();
		}
		
	}
}
