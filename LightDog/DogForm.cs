using LightDog.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightDog
{
	public partial class DogForm : Form
	{
		public DogForm()
		{
			InitializeComponent();

			// 为软件添加版本信息
			FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
			string appFileVersion = string.Format("{0}.{1}.{2}.{3}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);
			Text += " v" + appFileVersion;

			//MARK：添加这一句，会去掉其他线程使用本UI控件时弹出异常的问题(权宜之计，并非长久方案)。
			CheckForIllegalCrossThreadCalls = false;

		}

		#region 连接相关

		private void DogForm_Load(object sender, EventArgs e)
		{
			Thread.Sleep(100);
			connectButton_Click(null, null);
		}

		private void connectButton_Click(object sender, EventArgs e)
		{
			SerialPortTool.GetInstant().OpenSerialPort(  ConnectCompleted ,  ConnectError);
		}

		public void ConnectCompleted(Object obj, string msg)
		{
			loginPanel.Enabled = true;
			myTabControl.Enabled = false;
			setNotice("已连接设备，请输入密码进行校验。", false);
		}

		public void ConnectError(Object obj, string msg)
		{
			myTabControl.Enabled = false;
			setNotice("未检测到设备，请检查串口连接后重试。", true);
		}

		#endregion

		#region 密码校验相关

		/// <summary>
		/// 事件：密码框禁止输入空格
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pswTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 32)
			{
				e.Handled = true;
			}
		}

		private void loginButton_Click(object sender, EventArgs e)
		{
			string password = pswTextBox.Text.Trim();		
			if ( password.Length != 8 ) {
				setNotice("请输入8位密码",true);
				return;
			}

			if (Constant.SUPER_PASSWORD.Equals(password))
			{
				LoginCompleted(null, "密码校验成功，请进行相关设置。");
			}
			else
			{
				SerialPortTool.GetInstant().Login(password, LoginCompleted, LoginError);
			}
		}

		public void LoginCompleted(Object obj, string msg)
		{			
			myTabControl.Enabled = true;
			loginPanel.Enabled = false;
			setNotice("密码校验成功，请进行相关设置。", false);
		}

		public void LoginError(Object obj, string msg)
		{
			myTabControl.Enabled = false;
			pswTextBox.Text = null;

			setNotice("密码检验失败，请重新输入密码进行校验。", true);
		}

		#endregion

		#region 设置新密码

		/// <summary
		/// 事件：点击《修改密码》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pswUpdateButton_Click(object sender, EventArgs e)
		{
			string newPassword = newPswTextBox.Text.Trim();
			if (newPassword.Length != 8)
			{
				setNotice("请输入8位新密码", true);
				return;
			}

			string password = pswTextBox.Text.Trim();			
			SerialPortTool.GetInstant().SetLightControlDevicePassword(newPassword ,password, UpdatePasswordCompleted, UpdatePasswordError);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="msg"></param>
		public void UpdatePasswordCompleted(Object obj, string msg)
		{
			myTabControl.Enabled = false;
			loginPanel.Enabled = true;

			pswTextBox.Text = null;
			pswTextBox.Select();
			newPswTextBox.Text = null;

			setNotice( "密码修改成功，请使用新密码重新校验。",true) ;
		}

		public void UpdatePasswordError(Object obj, string msg)
		{
			myTabControl.Enabled = false;
			loginPanel.Enabled = false; 

			setNotice("密码修改失败，请重新连接设备后重试。", true);
		}

		#endregion

		#region 设置剩余时间

		private void timeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			hourNumericUpDown.Enabled = !timeCheckBox.Checked ;
		}		

		private void timeSetButton_Click(object sender, EventArgs e)
		{
			string password = pswTextBox.Text.Trim();

			if (timeCheckBox.Checked)
			{
				SerialPortTool.GetInstant().SetLightControlDeviceTime(4294967295, password, TimeSetCompleted, TimeSetError);
			}
			else {
				try
				{
					uint remainTime = uint.Parse(hourNumericUpDown.Text) * 60	;					
					SerialPortTool.GetInstant().SetLightControlDeviceTime(remainTime, password, TimeSetCompleted, TimeSetError);
				}
				catch (Exception ex) {
					setNotice(	ex.Message , true);
				}				
			}
		}
		

		public void TimeSetCompleted(object obj, string message)
		{
			myTabControl.Enabled = false;
			loginPanel.Enabled = false;

			setNotice("可用时长设置成功，设备将自动重启，请重新连接。", false);

		}

		public void TimeSetError(object obj, string message)
		{
			myTabControl.Enabled = false;
			loginPanel.Enabled = false;

			setNotice("可用时长设置失败，请重连设备后重试。", true);
		}

		#endregion



		/// <summary>
		///辅助方法：设置提示
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="msgBox"></param>
		private void setNotice(string msg, bool msgBoxShow) {
			this.myStatusLabel.Text = msg;
			//if (msgBoxShow) {
			//	MessageBox.Show(msg);
			//}
		}

	
	}
}
