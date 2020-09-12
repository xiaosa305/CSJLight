using LightController.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.OtherTools
{
	public partial class RecordSetForm : Form
	{
		// 以下变量，为分页显示内容项必须的变量 
		private int currentPage = 1;　  
		private int pageCount ;
		private int eachCount = 100;    // 如果此项大于tdCount,则应设为tdCount的值
		private int tdCount = 512;  //注意：此项不得为0，否则分页毫无意义

		private bool[] tdArray; // 记录了各个通道是否开启了音频控制的功能的数组(true为开启)

		public RecordSetForm()
		{
			InitializeComponent();

			tdArray = new bool[tdCount];
			eachCount = eachCount > tdCount ? tdCount : eachCount ;			
			pageCount = MathHelper.GetDividionCelling(tdCount, eachCount);
		
			for (int cbIndex = 0; cbIndex < eachCount; cbIndex++)
			{
				int tdIndex = (currentPage - 1) * eachCount + cbIndex;
				CheckBox cb = new CheckBox
				{
					Location = checkBoxDemo.Location,
					Size = checkBoxDemo.Size,					
					UseVisualStyleBackColor = checkBoxDemo.UseVisualStyleBackColor,
					Visible = true,
				};
				bigFLP.Controls.Add(cb);
			}

			refreshPage();
		}
					   
		/// <summary>
		/// 辅助方法：刷新页面（根据当前的页面，把相应的checkBox的CheckBox设为正确的值）
		/// </summary>
		private void refreshPage()
		{
			// 显示页数
			pageLabel.Text = currentPage + "/" + pageCount;

			// 遍历显示(或隐藏)通道,并改名
			for (int cbIndex = 0; cbIndex < eachCount; cbIndex++)
			{
				CheckBox cb = bigFLP.Controls[cbIndex] as CheckBox ; 
				if (currentPage < pageCount || tdCount % eachCount == 0 || cbIndex < tdCount % eachCount )
				{
					int tdIndex = (currentPage - 1) * eachCount + cbIndex;					
					cb.Text = "通道" + (tdIndex + 1);
					cb.Name = "checkBox" + (tdIndex + 1);
					cb.CheckedChanged -= tdCheckBox_CheckedChanged;
					cb.Checked = tdArray[tdIndex];
					cb.CheckedChanged += tdCheckBox_CheckedChanged;
					cb.Show();
				}
				else {
					cb.Hide();
				}
			}
		}

		/// <summary>
		/// 事件：当通道的CheckBox发生Checked变动时，更改相应的tdArray值；
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Console.WriteLine("tdCheckBox_CheckedChanged");

			CheckBox cb = sender as CheckBox;
			int tdIndex = MathHelper.GetIndexNum(cb.Name, -1);
			tdArray[tdIndex] = cb.Checked;

		}
			   
		#region 保存或加载配置

		/// <summary>
		/// 事件：点击《加载旧版配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loadOldButton_Click(object sender, EventArgs e)
		{
			for (int tdIndex = 0; tdIndex < tdCount; tdIndex++)
			{
				tdArray[tdIndex] = (tdIndex + 1) % 3 == 0;
			}
			refreshPage();
		}

		/// <summary>
		/// 事件：点击《加载新版配置文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loadNewButton_Click(object sender, EventArgs e)
		{
			for (int tdIndex = 0; tdIndex < tdCount; tdIndex++)
			{
				tdArray[tdIndex] = (tdIndex + 1) % 5 == 0;
			}
			refreshPage();
		}

		/// <summary>
		/// 事件：点击《保存配置文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			Console.WriteLine(tdArray);

			currentPage = pageCount;
			refreshPage();
		}

		#endregion

		#region 通用方法(这些方法往往只需稍微修改或完全不动，就可以在不同的界面中通用)

		// 双缓冲解决刷新页面时，慢慢减少部分控件的情况
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x02000000; // 用双缓冲绘制窗口的所有子控件
				return cp;
			}
		}

		private void RecordSetForm_Load(object sender, EventArgs e) { }

		/// <summary>
		/// 事件：点击《上|下一页》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pageButton_Click(object sender, EventArgs e)
		{
			if ((sender as Button).Name == "previousButton")
			{
				if (currentPage > 1)
				{
					currentPage--;
					refreshPage();
				}
			}
			else
			{
				if (currentPage < pageCount)
				{
					currentPage++;
					refreshPage();
				}
			}
		}

		/// <summary>
		/// 辅助方法：设置提醒
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="msgBoxShow"></param>
		private void setNotice(string msg, bool msgBoxShow)
		{
			myStatusLabel.Text = msg;
			if (msgBoxShow)
			{
				MessageBox.Show(msg);
			}
		}

		#endregion

	}
}
