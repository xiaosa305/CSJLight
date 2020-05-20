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
	public partial class GroupForm : Form
	{
		private MainFormBase mainForm;
		private IList<int> selectedIndices;

		public GroupForm(MainFormBase mainForm, IList<int> selectedIndices)
		{
			this.mainForm = mainForm;
			this.selectedIndices = selectedIndices;

			InitializeComponent();
		}

		private void GroupForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 200, mainForm.Location.Y + 200);
		}

		/// <summary>
		/// 事件：点击《确定》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			string groupName = nameTextBox.Text.Trim();
			if ( string.IsNullOrEmpty(groupName) ) {
				MessageBox.Show("编组名不得为空。");
				return; 
			}
			string result = mainForm.CreateGroup(groupName, selectedIndices);
			if ( result == null )
			{
				MessageBox.Show("编组成功");
				Dispose();
				mainForm.Activate();
			}
			else {
				MessageBox.Show("编组失败,原因是:\n" + result);	
			}			
		}

		/// <summary>
		/// 事件：点击《右上角关闭（X）》按钮、《取消》按钮
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
