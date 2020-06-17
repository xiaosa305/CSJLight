﻿using LightController.Ast;
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
	public partial class MultiStepPasteForm : Form
	{

		private MainFormBase mainForm;

		public MultiStepPasteForm(MainFormBase mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;
		}

		private void MultiStepPasteForm_Load(object sender, EventArgs e)
		{
			//this.Location = new Point(mainForm.Location.X + 500, mainForm.Location.Y + 500);
			Location = MousePosition;
		}

		/// <summary>
		/// 事件：点击《取消》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelSkinButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}


		/// <summary>
		/// 事件：点击《插入、覆盖》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void insertOrCoverSkinButton_Click(object sender, EventArgs e)
		{
			InsertMethod method = ((Button)sender).Name == "insertButton" ? InsertMethod.INSERT : InsertMethod.COVER;
			mainForm.InsertOrCoverMaterial(mainForm.TempMaterialAst  ,method);

			Dispose();
			mainForm.Activate();
		}

		/// <summary>
		///  事件：点击《使用说明》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void helpSkinButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("1.点击《插入》按钮，会在当前步与下一步之间粘贴保存的“复制多步”数据，未涉及的通道将使用灯具初始值；\n" +
				"2.点击《覆盖》按钮，会从当前步开始覆盖相关步数，“复制多步”数据内未涉及通道将保留原值；若现有步数不足，会自动添加新步，未涉及的通道将使用灯具初始值;\n" +
				"3.插入或覆盖之后的步数不能超过灯具当前模式所允许的最大步数，否则会添加失败。",
			"粘贴多步使用帮助");			
		}
	}
}
