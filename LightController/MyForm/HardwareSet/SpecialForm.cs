using LightController.Common;
using LightController.MyForm.HardwareSet;
using LightController.Tools.CSJ.IMPL;
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
	public partial class SpecialForm : Form
	{
		private NewHardwareSetForm hsForm;
		private string password = "TRANSJOY";

		public SpecialForm(NewHardwareSetForm hsForm , CSJ_Hardware ch)
		{			
			InitializeComponent() ;

			this.hsForm = hsForm;
			aiCheckBox.Checked  = ch.Addr == 1;
			domainServerTextBox.Text = ch.DomainServer;
			remotePortTextBox.Text = ch.RemotePort.ToString();
		}

		private void SpecialForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
		}

		/// <summary>
		/// 事件：点击《验证》，密码不对的情况下，直接退出本界面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loginButton_Click(object sender, EventArgs e)
		{
			if (pswTB.Text != password)
			{
				Dispose();
				return;
			}
			else {
				pswPanel.Hide();
			}
		}


		/// <summary>
		/// 事件：点击《确定》：先验证旧密码是否相符，如不相符则弹出提醒；用户可根据这个密码决定是否继续
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			if (!StringHelper.IsIP(domainServerTextBox.Text))
			{
				MessageBox.Show("服务器IP地址格式不正确，请重新输入。");
				return;
			}

			int remotePort ; 
			try
			{
				remotePort = int.Parse(remotePortTextBox.Text.Trim());
			}
			catch (Exception ex)
			{
				MessageBox.Show("端口号必须是数字 : " +ex.Message);
				return;
			}

			hsForm.SetCH(aiCheckBox.Checked ? 1 : 0, domainServerTextBox.Text.Trim(), remotePort); 				
			Dispose();		
								
		}

		
	}
}
