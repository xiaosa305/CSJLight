using LightController.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm
{
	public partial class CopySceneForm : Form
	{
		private MainFormBase mainForm;	
		private IList<int> sceneIndexList  = new List<int>();  // 辅助变量，用于存储场景列表的index列表 

		public CopySceneForm(MainFormBase mainForm, int currentFrameIndex)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			for (int sceneIndex = 0; sceneIndex < MainFormBase.AllSceneList.Count; sceneIndex++)
			{
				if (sceneIndex != currentFrameIndex)
				{
					sceneComboBox.Items.Add(MainFormBase.AllSceneList[sceneIndex]);
					sceneIndexList.Add(sceneIndex); 
				}
			}
			sceneComboBox.SelectedIndex = 0;
		}

		private void CopySceneForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 300, mainForm.Location.Y + 300);			
			LanguageHelper.InitForm(this);
			if (mainForm.CurrentMode == 0)
			{
				normalCheckBox.Checked = true;
			}
			else {
				soundCheckBox.Checked = true;
			}
		}
		
		/// <summary>
		/// 事件：点击《取消、右上角关闭》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：点击《将选中场景复制到当前场景》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fromButton_Click(object sender, EventArgs e)
		{			
			mainForm.CopyOtherScene(
				getSceneIndex(),
				mainForm.CurrentScene,				
				normalCheckBox.Checked ,
				soundCheckBox.Checked); 
		}

		/// <summary>
		/// 事件：点击《将当前场景复制到选中场景》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toButton_Click(object sender, EventArgs e)
		{
			mainForm.CopyOtherScene(				
				mainForm.CurrentScene,
				getSceneIndex(),
				normalCheckBox.Checked,
				soundCheckBox.Checked);
		}

		private void modeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			fromButton.Enabled = normalCheckBox.Checked || soundCheckBox.Checked;
			toButton.Enabled = normalCheckBox.Checked || soundCheckBox.Checked;
		}

		/// <summary>
		/// 辅助方法：获取场景index的方法
		///
		/// </summary>
		/// <returns></returns>
		private int getSceneIndex()
		{
			// 获取当前选中项，然后取出其在frameIndexList中的位置
			int selectedFrameIndex = sceneIndexList[sceneComboBox.SelectedIndex];
			return selectedFrameIndex ;
		}

        

	}
}
