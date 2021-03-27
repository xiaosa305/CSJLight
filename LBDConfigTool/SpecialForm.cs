using LBDConfigTool.utils.conf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LBDConfigTool
{
	public partial class SpecialForm : Form
	{
		private ConfForm confForm;
		private string oldPassword;

		public SpecialForm(ConfForm confForm,CSJConf specialCC)
		{
			this.confForm = confForm;
			
			InitializeComponent() ;

			oldPassword = specialCC.OLD_MIA_HAO;
			badCheckBox.Checked = specialCC.IsSetBad;
			aSumTB.Text = specialCC.SumUseTimes + "";
			aCurrTB.Text = specialCC.CurrUseTimes + "";			
			cardRB1.Checked = specialCC.CardType == 0;
			cardRB2.Checked = specialCC.CardType == 2;
		}

		private void SpecialForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
		}

		/// <summary>
		/// 事件：点击《确定》：先验证旧密码是否相符，如不相符则弹出提醒；用户可根据这个密码决定是否继续
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			if (oldPswTB.Text.Length != 6 || newPswTB.Text.Trim().Length != 6) {
				MessageBox.Show("密码必须为6位");
				return;
			}
			
			//if (oldPswTB.Text != oldPassword) {
			//	if (DialogResult.No == MessageBox.Show("检测到您输入的旧密码有误，可能无法更改相关设置，是否继续？", "继续操作？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
			//	{
			//		return;
			//	}
			//}

			try
			{
				confForm.SetSpecialCC(new CSJConf()
				{
					OLD_MIA_HAO = oldPswTB.Text,
					MIA_HAO = newPswTB.Text,  // 密码限定为6位，不能多不能少				
					IsSetBad = badCheckBox.Checked,
					CardType = cardRB1.Checked ? 0 : 2,
					SumUseTimes = int.Parse(aSumTB.Text),
					CurrUseTimes = int.Parse(aCurrTB.Text)
				});
				Dispose();
				confForm.Activate();
			}
			catch (Exception ex) {
				MessageBox.Show("输入参数有误：" + ex.Message);
				return;
			}			
				
		}
	}
}
