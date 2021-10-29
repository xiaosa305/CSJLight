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

			nameLabel.Text = lightAst.LightName + "-" + lightAst.LightType;
			addrLabel.Text = lightAst.LightAddr;			

			startNUD.Value = lightAst.StartNum != 0 ? lightAst.StartNum : lightsForm.MinNum ;
		}

		private void LightsEditForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			LanguageHelper.InitForm(this);
		}

		/// <summary>
		///  点击《修改》键=》应该回调LightsForm的方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			int startAddr = decimal.ToInt32(startNUD.Value );
            int endAddr = startAddr + lightAst.Count - 1;
            if (!lightsForm.CheckAddrAvailable( lightIndex,  startAddr , endAddr))
            {
                MessageBox.Show(LanguageHelper.TranslateSentence("检测到您修改的新灯具地址中，有部分地址已被其他灯具占用，\n请重新设置起始地址后重试。"));
                return;
            }

            bool editResult = lightsForm.UpdateLight(lightIndex, startAddr);
			if (editResult) {
				Dispose();
				lightsForm.Activate();
			}
		}

		/// <summary>
		/// 事件：点击《取消》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Dispose();
			lightsForm.Activate();
		}

	}
}
