﻿using LightController.Common;
using LightController.MyForm.MainFormAst;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.MyForm.HardwareSet
{
	public partial class NewHardwareSetForm : Form
	{
		private MainFormBase mainForm;
		private CSJ_Hardware ch;
		private string xbinPath;

		public NewHardwareSetForm(MainFormBase mainForm)
		{			
			InitializeComponent();

			this.mainForm = mainForm;

			//《硬件配置》相关
			ch = new CSJ_Hardware();
			SetParamFromCH();

			//《固件升级》相关
			xbinPath = Properties.Settings.Default.xbinPath;
			if (File.Exists(xbinPath))
			{
				pathLabel.Text = xbinPath;
			}
			else //若路径有错（文件不存在），则设为空，并保存下来；
			{
				xbinPath = null;
				Properties.Settings.Default.xbinPath = xbinPath;
				Properties.Settings.Default.Save();
			}
			updateButton.Enabled = mainForm.IsConnected && !string.IsNullOrEmpty(xbinPath);
		}

		private void NewHardwareSet_Load(object sender, EventArgs e)
		{
			Location = MousePosition;			
		}

		/// <summary>
		/// Activated事件：激活窗口后，回读一下设备信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewHardwareSetForm_Activated(object sender, EventArgs e)
		{
			readButton_Click(null, null);
		}

		#region 硬件配置相关

		/// <summary>
		///  事件：点击《从设备回读》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void readButton_Click(object sender, EventArgs e)
		{
			if (mainForm.MyConnect.GetParam(GetParamCompleted, GetParamError))
			{
				SetNotice("正在回读设备信息，请稍候...", false, true);
			}
			else
			{
				Thread.Sleep(100);
				readButton_Click(null,null);
			}
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
				SetNotice("成功回读硬件配置。", false, true);
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
				SetNotice("回读配置失败[" + msg + "]", true, false);
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
			if (!checkAllFormat())
			{
				SetNotice("有异常参数，请校对后重试！", false, true);
				return;
			}
			
			// 写入配置			
			SetNotice("正在写入配置到设备，请稍候...", false, true);
			//setBusy(true);

			//MARK0412 修改 《写入硬件配置》的入参
			ch.DeviceName = deviceNameTextBox.Text.Trim();
			ch.IP = IPTextBox.Text.Trim();
			ch.NetMask = netmaskTextBox.Text.Trim();
			ch.GateWay = gatewayTextBox.Text.Trim();
			ch.Mac = macTextBox.Text.Trim();

			mainForm.MyConnect.PutParam(ch , PutParamCompleted, PutParamError);
		}

		/// <summary>
		/// 辅助回调方法：下载配置成功
		/// </summary>
		/// <param name="obj"></param>
		public void PutParamCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				mainForm.DisConnect();
				SetBusy(false);
				SetNotice("硬件配置下载成功,需要重启(约5S)，请稍后重连设备...", true, true);
				//Thread.Sleep(ConnectForm.REBOOT_WATITIME);
				mainForm.ConnForm.ShowDialog();
			});
		}

		/// <summary>
		/// 辅助回调方法：下载配置失败
		/// </summary>
		/// <param name="obj"></param>
		public void PutParamError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				SetNotice("下载配置失败[" + msg + "]", true, false);
				SetBusy(false);
			});
		}

		#endregion

		#region 固件升级相关

		/// <summary>
		/// 事件：点击《选择升级文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fileOpenButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == openFileDialog.ShowDialog())
			{
				xbinPath = openFileDialog.FileName;
				Properties.Settings.Default.xbinPath = xbinPath;
				Properties.Settings.Default.Save();

				pathLabel.Text = xbinPath;
				updateButton.Enabled = mainForm.IsConnected && !string.IsNullOrEmpty(xbinPath);
			}
		}

		/// <summary>
		/// 事件：点击《(固件)升级》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateButton_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(xbinPath))
			{
				SetNotice("尚未选择xbin文件，请在选择后重试。", true, true);
				return;
			}

			if (!mainForm.IsConnected)
			{
				SetNotice("尚未连接设备，请连接后重试。", true, true);
				return;
			}

			SetBusy(true);
			mainForm.MyConnect.UpdateDeviceSystem(xbinPath, UpdateCompleted, UpdateError, DrawProgress);
		}

		/// <summary>
		/// 辅助回调方法：固件升级成功
		/// </summary>
		/// <param name="obj"></param>
		public void UpdateCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				SetNotice("硬件升级成功，设备将自动重启，请稍等片刻后重新连接。", true, true);

				Thread.Sleep(5000);
				myProgressBar.Value = 0;
				progressStatusLabel.Text = "";

				SetBusy(false);
			});
		}

		/// <summary>
		/// 辅助回调方法：固件升级失败
		/// </summary>
		/// <param name="obj"></param>
		public void UpdateError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				SetNotice("固件升级失败[" + msg + "]", true, false);
				myProgressBar.Value = 0;
				progressStatusLabel.Text = "";
				SetBusy(false);
			});
		}

		/// <summary>
		/// 辅助回调方法：写进度条
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="progress"></param>
		public void DrawProgress(string fileName, int progressPercent)
		{
			SetNotice("正在升级固件，请稍候...", false, true);
			myProgressBar.Value = progressPercent;
			progressStatusLabel.Text = progressPercent + "%";
			statusStrip1.Refresh();
		}

		#endregion


		#region 通用方法

		/// <summary>
		/// 辅助方法：显示信息
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="messageBoxShow">是否在提示盒内提示</param>
		public void SetNotice(string msg, bool messageBoxShow, bool isTranslate)
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
		public void SetBusy(bool busy)
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

		#region 几个输入监视器、及格式校验方法

		/// <summary>
		/// 事件：勾选《自动获取MAC地址》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void macCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			bool autosetMac = macTextBox.Enabled;
			macTextBox.Enabled = !autosetMac;
			if (autosetMac)
			{
				macTextBox.Text = "00-00-00-00-00-00";
			}
		}

		/// <summary>
		/// 辅助监听器：只能输入字母或数字及退格键的验证
		/// </summary>
		private void validateLetterOrDigit_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z')
				|| (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8))
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}

		/// <summary>
		/// 辅助监听器：验证IP
		/// -- 只能输入 数字或"."号
		/// </summary>
		private void validateIP_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8 || e.KeyChar == '.')
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}

		/// <summary>
		/// 辅助监听器:只能输入数字
		/// </summary>
		private void validateDigit_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8))
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}

		/// <summary>
		/// 事件(监视器)：处理NumericUpDown的Leave 事件，以恢复显示
		/// -- 这种数字框，如果用户主动删除内容，则之后设value都不会显示，容易产生误导,
		/// --	 其value不一定等于输入框中的数字！
		/// -- 因为value绝对不为空，但输入框可能为空，则当输入框为空时，value会保留之前的Decimal值
		/// </summary>
		/// <param name="s"></param>
		/// <param name="e"></param>
		private void numericUpDown_Leave(object s, EventArgs e)
		{
			var n = (NumericUpDown)s;
			if (n.Text == "")
			{
				n.Value = 0;
				n.Text = "0";
			}
		}

		/// <summary>
		/// 辅助方法：统一校验各个输入框是否有错误
		/// </summary>
		/// <returns></returns>
		private bool checkAllFormat()
		{
			bool result = true;
			string errorMsg = "配置参数有错误，请重新输入：";

			if (!StringHelper.IsIP(IPTextBox.Text))
			{
				errorMsg += "\n【IP地址】格式有误";
				result = false;
			}
			if (!StringHelper.IsIP(netmaskTextBox.Text))
			{
				errorMsg += "\n【子网掩码】格式有误";
				result = false;
			}
			if (!StringHelper.IsIP(gatewayTextBox.Text))
			{
				errorMsg += "\n【网关】格式有误";
				result = false;
			}
			if (!StringHelper.IsMAC(macTextBox.Text))
			{
				errorMsg += "\n【MAC地址】格式有误";
				result = false;
			}

			if (IPTextBox.Text.Trim() != "0.0.0.0" && gatewayTextBox.Text.Trim() == "0.0.0.0")
			{
				errorMsg += "\n未启用DHCP的情况下，【网关不能设为0.0.0.0】";
				result = false;
			}

			if (!result)
			{
				MessageBox.Show(errorMsg);
			}
			return result;
		}


		#endregion

		
	}
}
