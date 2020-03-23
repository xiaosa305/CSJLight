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
		private bool add;
		private int saIndex = -1;

		public SAForm(WaySetForm wsForm, bool add ,string saName,int saIndex,int startValue, int endValue)
		{
			this.wsForm = wsForm;
			this.add = add;
			this.saIndex = saIndex ;

			InitializeComponent();

			if(add)
			{
				Text = "添加子属性";
				this.startValueNumericUpDown.Value = startValue;							
			}
			else
			{
				Text = "修改子属性";
				this.saNameTextBox.Text = saName;
				this.startValueNumericUpDown.Value = startValue;
				this.endValueNumericUpDown.Value = endValue;
			}
		}		

		private void SAForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(wsForm.Location.X + 100, wsForm.Location.Y + 100);
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

		private bool checkSAName() {
			if(String.IsNullOrEmpty(saNameTextBox.Text))
			{
				MessageBox.Show("子属性名不得为空!");
				return false;
			}

			//TODO : （需考虑有无必要）检查是否重复，相对比较麻烦。
			//if (wsForm.CheckSAName( saNameTextBox.Text) ) {
			//	MessageBox.Show()				
			//}

			return true;
		}

		/// <summary>
		/// 事件：点击《确定》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			if ( !checkStartAndEnd() ) {
				MessageBox.Show("截止值不能小于起始值,请检查后重试。");
				return;
			}
			if (!checkSAName()) {
				return;
			}

			if (add)
			{
				wsForm.AddSA(saNameTextBox.Text, Decimal.ToInt16(startValueNumericUpDown.Value), Decimal.ToInt16(endValueNumericUpDown.Value));
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
