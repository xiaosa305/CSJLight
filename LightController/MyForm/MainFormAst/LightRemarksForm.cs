using LightController.Ast;
using LightController.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.LightList
{
	public partial class LightRemarkForm : Form
	{
		private MainFormBase mainForm;		
		private int lightIndex;

		public LightRemarkForm(MainFormBase mainForm,LightAst la, int lightIndex)
		{
			this.mainForm = mainForm;
			this.lightIndex = lightIndex;

			InitializeComponent();

			lightTypeLabel.Text = la.LightType;
			lightAddrLabel.Text = la.LightAddr;
			lightRemarkTextBox.Text = la.Remark;
			myToolTip.SetToolTip(label3, "备注上限为8个字");
		}

		private void LightRemarkForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 200, mainForm.Location.Y + 200);
			LanguageHelper.InitForm(  this);
		}
		
		/// <summary>
		/// 事件：点击《确定》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			mainForm.EditLightRemark(lightIndex, lightRemarkTextBox.Text.Trim());
			this.Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：点击《取消 | 右上角X》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}

	}
}
