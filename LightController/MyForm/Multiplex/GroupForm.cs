using LightController.Ast;
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

		public GroupForm(MainFormBase mainForm, IList<LightAst> lightAstList , IList<int> selectedIndices) 
		{
			this.mainForm = mainForm;
			this.selectedIndices = selectedIndices;

			InitializeComponent();

			for (int i = 0; i < selectedIndices.Count; i++)
			{
				int lightIndex = selectedIndices[i];
				LightAst tempLA = lightAstList[lightIndex];
				ListViewItem item = new ListViewItem( (i+1).ToString() );
				item.SubItems.Add(tempLA.LightType);
				item.SubItems.Add(tempLA.LightAddr);
				lightsListView.Items.Add(item);
			}
		}

		private void GroupForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 200, mainForm.Location.Y + 200);
			//Location = MousePosition;
		}

		/// <summary>
		/// 事件：点击《确定》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			string groupName = nameTextBox.Text.Trim();
			if (string.IsNullOrEmpty(groupName))
			{
				MessageBox.Show("编组名不得为空。");
				return;
			}

			int captainIndex = 0;
			// 只有选中项不为空，才能更改组长；
			if (lightsListView.SelectedIndices.Count > 0)
			{
				captainIndex = lightsListView.SelectedIndices[0];
			}

			string result = mainForm.CreateGroup(groupName, captainIndex);
			if (result == null)
			{
				MessageBox.Show("编组成功");
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
