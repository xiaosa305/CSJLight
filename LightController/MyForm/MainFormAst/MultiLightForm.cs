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

		public MultiLightForm(MainFormInterface mainForm, bool isCopyAll,IList<Ast.LightAst> lightAstList, IList<int> lightIndices)
		{
			this.mainForm = mainForm;
			InitializeComponent();
			this.copyAllCheckBox.Checked = isCopyAll ; 

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
			this.Location = new Point(mainForm.Location.X + 200, mainForm.Location.Y + 200);
		}

		/// <summary>
		/// 事件：点击《统一模式》按钮（或双击灯具也可选择组长）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterSkinButton_Click(object sender, EventArgs e)
		{
			// 第一层判断,避免未选中组长时就往下运行
			if (selectedIndex < 0)
			{
				MessageBox.Show("请先选中组长。");
				return;
			}

			// 这一层判断，若用户选中《局部多灯模式》则需要所有编组的成员都有一样的步数，否则后期处理十分麻烦（比如添加灯之类的）。
			if ( !copyAllCheckBox.Checked && !mainForm.CheckSameStepCounts()) {
				MessageBox.Show("列表中灯具的步数并非完全一致，无法进入（局部）多灯模式。");
				return;				
			}

			mainForm.EnterMultiMode( selectedIndex  ,  copyAllCheckBox.Checked);
			Dispose();
			mainForm.Activate();			
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
		private void lightsListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 必须判断这个字段(Count)，否则会报异常
			if (lightsListView.SelectedIndices.Count > 0) {
				selectedIndex = lightsListView.SelectedIndices[0];
				enterButton.Enabled = true;
			}
		}
	}
}
