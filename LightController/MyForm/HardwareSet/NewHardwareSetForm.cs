using LightController.Common;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.HardwareSet
{
	public partial class NewHardwareSetForm : Form
	{
		private MainFormBase mainForm;
		private CSJ_Hardware ch;

		public NewHardwareSetForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;			
			InitializeComponent();

			ch = new CSJ_Hardware();
			SetParamFromCH();
		}

		private void NewHardwareSet_Load(object sender, EventArgs e)
		{
			//readFromDevice();
		}

		/// <summary>
		///  事件：点击《从设备回读》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void readButton_Click(object sender, EventArgs e)
		{
			readFromDevice();
		}

		/// <summary>
		/// 辅助方法： 从设备回读的方法（可能存在多处调用）
		/// </summary>
		private void readFromDevice()
		{
			mainForm.MyConnect.GetParam(GetParamCompleted, GetParamError);
		}

		/// <summary>
		/// 辅助回调方法：回读配置成功
		/// </summary>
		/// <param name="obj"></param>
		public void GetParamCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				ch = obj as CSJ_Hardware;
				SetParamFromCH();
				setNotice("成功回读硬件配置。", true, true);
			});
		}

		/// <summary>
		/// 辅助回调方法：回读配置失败
		/// </summary>
		/// <param name="obj"></param>
		public void GetParamError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice("回读配置失败[" + msg + "]", true, false);
			});
		}

		/// <summary>
		///  辅助方法：通过回读的CSJ_Hardware对象，来填充左侧的所有输入框。
		/// </summary>
		/// <param name="ch"></param>
		public void SetParamFromCH()
		{
			try
			{
				deviceNameTextBox.Text = ch.DeviceName;
				IPTextBox.Text = ch.IP;
				netmaskTextBox.Text = ch.NetMask;
				gatewayTextBox.Text = ch.GateWay;
				macTextBox.Text = ch.Mac;
				macCheckBox.Checked = macTextBox.Text.Trim().Equals("00-00-00-00-00-00");

			}
			catch (Exception ex)
			{
				MessageBox.Show("回读异常:" + ex.Message);
			}
		}


	

		/// <summary>
		///  事件：点击《写入配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void downloadButton_Click(object sender, EventArgs e)
		{
			
		}


		#region 通用方法

		/// <summary>
		/// 辅助方法：显示信息
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="messageBoxShow">是否在提示盒内提示</param>
		private void setNotice(string msg, bool messageBoxShow, bool isTranslate)
		{
			if (isTranslate)
			{
				msg = LanguageHelper.TranslateSentence(msg);
			}

			if (messageBoxShow)
			{
				MessageBox.Show(msg);
			}
			myStatusLabel.Text = msg;
			statusStrip1.Refresh();
		}

		/// <summary>
		/// 辅助方法：设定忙时（鼠标的变化）
		/// </summary>
		/// <param name="busy"></param>
		private void setBusy(bool busy)
		{
			Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
			Enabled = !busy;
			Refresh();
		}

		/// <summary>
		/// 某些按键的文字如果发生了变化，就需要重新设置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someButton_TextChanged(object sender, EventArgs e)
		{
			LanguageHelper.TranslateControl(sender as Button);
		}
		
		#endregion



	}
}
