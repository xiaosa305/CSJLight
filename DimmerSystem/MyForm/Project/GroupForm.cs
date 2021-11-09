using LightController.Ast;
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

namespace LightController.MyForm.Project
{
    public partial class GroupForm : UIForm
    {
		private MainFormBase mainForm;
		public static bool IsCopyAll = false;
		private GroupAst group;

		public GroupForm(MainFormBase mainForm, IList<LightAst> lightAstList, IList<int> selectedIndices)
		{
			InitializeComponent();

			this.mainForm = mainForm;

			group = new GroupAst()
			{
				LightIndexList = selectedIndices,
				CaptainIndex = -1
			};

			copyAllCheckBox.Checked = IsCopyAll;

			for (int selectedIndex = 0; selectedIndex < selectedIndices.Count; selectedIndex++)
			{
				int lightIndex = selectedIndices[selectedIndex];
				LightAst tempLA = lightAstList[lightIndex];
				ListViewItem item = new ListViewItem((selectedIndex + 1).ToString());
				item.SubItems.Add(tempLA.LightType);
				item.SubItems.Add(tempLA.LightAddr);
				lightsListView.Items.Add(item);
			}
		}

		private void GroupForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);

			//LanguageHelper.InitForm(this);
			//LanguageHelper.TranslateListView(lightsListView);
		}

		/// <summary>
		/// 事件：listView选中项发生变化，则更改组长（如果只是失去焦点，仍然设为之前最后选中项）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 选中组长后，哪怕失去焦点，也不会改变之前的选择；除非更改选项
			if (lightsListView.SelectedIndices.Count > 0)
			{
				group.CaptainIndex = lightsListView.SelectedIndices[0];
				enterButton.Enabled = true;
				setNotice("设灯具（序号" + (group.CaptainIndex + 1) + " ）为组长", false, false);
			}
		}

		/// <summary>
		///  事件：把是否设为组长数据的复选框设为全局变量（可保存）；
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void copyAllCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			IsCopyAll = copyAllCheckBox.Checked;
		}

		/// <summary>
		/// 事件：点击《确定》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			// 组长不得为空
			if (group.CaptainIndex < 0)
			{
				setNotice("请先选中组长。", true, true);
				return;
			}

			// 如果没有勾选《统一设为组长数据》，则所有灯具必须有一样的步数
			if (!IsCopyAll && !mainForm.CheckSameStepCounts())
			{
				setNotice("若不《统一设为组长数据》，则编组灯具的步数需相同。", true, false);
				return;
			}

			group.GroupName = nameTextBox.Text.Trim();
			if (mainForm.MakeGroup(group))  // 检查是否存储编组，空编组名则直接返回true；如果编组名不为空，则判断是否存在编组名；
			{
				mainForm.EnterMultiMode(group, IsCopyAll);
				Dispose();
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
		}

		#region  通用方法

		/// <summary>
		/// 辅助方法：设置提醒
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="msgBoxShow"></param>
		private void setNotice(string msg, bool msgBoxShow, bool isTranslate)
		{
			if (isTranslate)
			{
				msg = LanguageHelper.TranslateSentence(msg);
			}

			myStatusLabel.Text = msg;
			if (msgBoxShow)
			{
				MessageBox.Show(msg);
			}
		}

		#endregion
	}
}
