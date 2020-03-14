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
	public partial class UseFrameForm : Form
	{
		private MainFormBase mainForm;	
		private IList<int> frameIndexList  = new List<int>();  // 辅助变量，用于存储场景列表的index列表 


		public UseFrameForm(MainFormBase mainForm, int currentFrameIndex)
		{
			InitializeComponent();

			this.mainForm = mainForm;
			
			for (int frameIndex = 0; frameIndex < MainFormBase.AllFrameList.Count; frameIndex++)
			{
				if (frameIndex != currentFrameIndex)
				{
					frameComboBox.Items.Add(MainFormBase.AllFrameList[frameIndex]);
					frameIndexList.Add(frameIndex); 
				}
			}
			frameComboBox.SelectedIndex = 0;
		}


		private void UseFrameForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 300, mainForm.Location.Y + 300);
		}

		
		/// <summary>
		/// 事件：点击《取消、右上角关闭》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelSkinButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：点击《确定》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterSkinButton_Click(object sender, EventArgs e)
		{			
			mainForm.UseOtherForm(getFrameIndex()); 
			this.Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 辅助方法：获取场景index的方法
		///
		/// </summary>
		/// <returns></returns>
		private int getFrameIndex()
		{
			// 1.  返回当前选中项的index, ( 缺点在于需要列表中有全部的场景名，优点在于简单 ) ，弃用：为了用户体验，当前frame不在调用列表中
			// return frameSkinComboBox.SelectedIndex;

			// 2. 获取当前选中项，然后取出其在frameIndexList中的位置
			int  selectedIndex = frameComboBox.SelectedIndex;
			int selectedFrameIndex = frameIndexList[selectedIndex];
			return selectedFrameIndex ;
		}
		
	}
}
