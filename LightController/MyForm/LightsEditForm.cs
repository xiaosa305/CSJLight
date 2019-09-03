using LightController.Ast;
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
	public partial class LightsEditForm : Form
	{
		private LightsForm lightsForm;
		private LightAst lightAst;
		private int lightIndex;

		public LightsEditForm(LightsForm lightsForm, LightAst lightAst,int lightIndex)
		{
			InitializeComponent();

			this.lightsForm = lightsForm;
			this.lightAst = lightAst;
			this.lightIndex = lightIndex;

			nameTypeLabel.Text = lightAst.LightName + "-" + lightAst.LightType;
			addrLabel.Text = lightAst.LightAddr;
			startCountNumericUpDown.Value = lightAst.StartNum;

		}

		private void LightsEditForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(lightsForm.Location.X + 100, lightsForm.Location.Y + 100);
		}


		/// <summary>
		///  点击《修改》键=》应该回调LightsForm的方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			int startNum = Decimal.ToInt16(startCountNumericUpDown.Value );
			lightsForm.UpdateLight(lightIndex, startNum);
			this.Dispose();
			lightsForm.Activate();

		}

		/// <summary>
		/// 事件：点击《放弃修改》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			lightsForm.Activate();
		}

		/// <summary>
		///  点击《我知道了》按钮，会隐藏noticePanel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void iSeeButton_Click(object sender, EventArgs e)
		{
			noticePanel.Hide();
		}
	}
}
