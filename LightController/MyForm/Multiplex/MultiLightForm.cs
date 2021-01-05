using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LightController.Ast;
using LightController.Common;

namespace LightController.MyForm
{
	public partial class MultiLightForm : Form
	{
		private MainFormBase mainForm;
		private int captainIndex = -1; // 组长在[选中灯具列表]中的序号，默认-1，选择后改变值

		public MultiLightForm(MainFormBase mainForm, bool isCopyAll,IList<Ast.LightAst> lightAstList, IList<int> lightIndices)
		{
			this.mainForm = mainForm;
			InitializeComponent();

			copyAllCheckBox.Checked = isCopyAll ; 

			for (int i = 0; i < lightIndices.Count; i++)
			{
				int lightIndex = lightIndices[i];
				LightAst tempLA = lightAstList[lightIndex];
				ListViewItem item = new ListViewItem(tempLA.LightName);
				item.SubItems.Add(tempLA.LightType);
				item.SubItems.Add(tempLA.LightAddr);

				lightsListView.Items.Add(item);
			}			
		}		

		private void MultiLightForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			LanguageHelper.InitForm(this);
			LanguageHelper.InitListView(lightsListView);
		}

		/// <summary>
		/// 事件：点击《确认》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterSkinButton_Click(object sender, EventArgs e)
		{
			// 第一层判断,避免未选中组长时就往下运行
			if (captainIndex < 0)
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("请先选中组长。"));
				return;
			}

			// 这一层判断，若用户选中《局部多灯模式》则需要所有编组的成员都有一样的步数，否则后期处理十分麻烦（比如添加灯之类的）。
			if ( !copyAllCheckBox.Checked && !mainForm.CheckSameStepCounts()) {
				MessageBox.Show(LanguageHelper.TranslateSentence("列表中灯具的步数并非完全一致，无法进入多灯模式。"));
				return;				
			}

			mainForm.EnterMultiMode( captainIndex , copyAllCheckBox.Checked);
			Dispose();
			mainForm.Activate();			
		}	

		/// <summary>
		/// 事件：点击《取消》
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
		private void lightsListView_SelectedIndexChanged(object sender, EventArgs e)
		{			
			// 选中组长后，哪怕失去焦点，也不会改变之前的选择；除非更改选项
			if (lightsListView.SelectedIndices.Count > 0) {
				captainIndex = lightsListView.SelectedIndices[0];
				enterButton.Enabled = true;
			}
		}
	}
}
