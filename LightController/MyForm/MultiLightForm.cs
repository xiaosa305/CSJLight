using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LightController.Ast;

namespace LightController.MyForm
{
	public partial class MultiLightForm : Form
	{
		private MainFormInterface mainForm;
		private int selectedIndex = -1;

		public MultiLightForm(MainFormInterface mainForm, IList<Ast.LightAst> lightAstList, IList<int> lightIndices)
		{
			this.mainForm = mainForm;
			InitializeComponent();

			for (int i = 0; i < lightIndices.Count; i++)
			{
				int lightIndex = lightIndices[i];
				LightAst tempLA = lightAstList[lightIndex];
				ListViewItem item = new ListViewItem(tempLA.LightName);
				item.SubItems.Add(tempLA.LightType);
				item.SubItems.Add(tempLA.LightAddr);

				lightsSkinListView.Items.Add(item);
			}			
		}		

		private void MultiLightForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 200, mainForm.Location.Y + 200);
		}

		/// <summary>
		/// 事件：点击《确认》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterSkinButton_Click(object sender, EventArgs e)
		{
			// 多做一重判断
			if (selectedIndex != -1) {
				mainForm.EnterMultiMode(selectedIndex);
				Dispose();
				mainForm.Activate();
			}
		}

		/// <summary>
		/// 事件：点击《取消》及《右上角关闭》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelSkinButton_Click(object sender, EventArgs e)
		{
			Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：listView选中项发生变化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsSkinListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 必须判断这个字段(Count)，否则会报异常
			if (lightsSkinListView.SelectedIndices.Count > 0) {
				selectedIndex = lightsSkinListView.SelectedIndices[0];
				enterSkinButton.Enabled = true;
			}
		}
	}
}
