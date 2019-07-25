﻿using LightController.Common;
using LightController.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.MyForm
{
	public partial class HardwareSetForm : Form
	{
		private MainForm mainForm;
		private string iniPath;
		private string hName;
		private bool isNew = true;

		/// <summary>
		/// 构造函数：初始化各个变量
		/// </summary>
		/// <param name="iniPath">通过传入iniPath（空值或有值）来决定要生成的数据的模板</param>
		public HardwareSetForm(MainForm mainForm, string iniPath,string hName)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			this.iniPath = iniPath;			

			// 若iniPath 为空，则新建-》读取默认Hardware.ini，并载入到当前form中
			if (String.IsNullOrEmpty(iniPath) ){
				isNew = true;
				iniPath = Application.StartupPath + @"\HardwareSet.ini";
				this.Text = "硬件设置(未保存)";
			}// 否则打开相应配置文件，并载入到当前form中
			else {
				isNew = false;
				this.hName = hName;
				this.Text = "硬件设置(" + hName + ")";
			}
			readIniFile(iniPath);
		}

		/// <summary>
		/// 辅助方法：读取配置文件
		/// </summary>
		/// <param name="iniPath"></param>
		private void readIniFile(string iniPath)
		{
			IniFileAst iniFileAst = new IniFileAst(iniPath);

			sumUseTimeNumericUpDown.Value = iniFileAst.ReadInt("Common", "SumUseTimes",0);
			currUseTimeNumericUpDown.Value = iniFileAst.ReadInt("Common", "CurrUseTimes", 0);
			diskFlagComboBox.SelectedIndex = iniFileAst.ReadInt("Common", "DiskFlag", 0);
			deviceNameTextBox.Text = iniFileAst.ReadString("Common", "DeviceName", "");
			addrNumericUpDown.Value = iniFileAst.ReadInt("Common", "Addr", 0);
			hardwareIDTextBox.Text = iniFileAst.ReadString("Common", "HardwareID", "");
			heartbeatTextBox.Text = iniFileAst.ReadString("Common", "Heartbeat", "");
			heartbeatCycleNumericUpDown.Value = iniFileAst.ReadInt("Common", "HeartbeatCycle", 0);

			linkModeComboBox.SelectedIndex = iniFileAst.ReadInt("Network", "LinkMode", 0);
			linkPortTextBox.Text = iniFileAst.ReadString("Network", "LinkPort", "");
			IPTextBox.Text = iniFileAst.ReadString("Network", "IP", "");
			netmaskTextBox.Text = iniFileAst.ReadString("Network", "NetMask", "");
			gatewayTextBox.Text = iniFileAst.ReadString("Network", "GateWay", "");
			macTextBox.Text = iniFileAst.ReadString("Network", "Mac", "");

			baudComboBox.SelectedIndex = iniFileAst.ReadInt("Other", "Baud", 0);
			remoteHostTextBox.Text = iniFileAst.ReadString("Other", "RemoteHost", "");
			remotePortTextBox.Text = iniFileAst.ReadString("Other", "RemotePort", "");
			domainNameTextBox.Text = iniFileAst.ReadString("Other", "DomainName", "");
			domainServerTextBox.Text = iniFileAst.ReadString("Other", "DomainServer", "");
		}

		private void HardwareSetForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
		}

		/// <summary>
		/// 点击《保存》操作：
		/// 1、若是全新的版本，用一个newHardwareForm来生成文件夹名
		/// 2、若是旧的版本，则直接使用该版本来保存信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			if (isNew)
			{
				NewHardwareForm nhForm = new NewHardwareForm(this);
				nhForm.ShowDialog();
			}
			else {
				SaveAll(iniPath,hName);
			}
			
		}

		/// <summary>
		/// 辅助方法：通用的方法，供新建(NewHardwareForm)及旧版本的保存
		/// </summary>
		/// <param name="hardwareSetForm"></param>
		internal void SaveAll(String iniPath,string hName)
		{
			this.iniPath = iniPath;
			this.hName = hName;
			IniFileAst iniFileAst = new IniFileAst(iniPath);

			iniFileAst.WriteString("Common", "SumUseTimes", sumUseTimeNumericUpDown.Value.ToString() );
			iniFileAst.WriteString("Common", "CurrUseTimes", currUseTimeNumericUpDown.Value.ToString() );
			iniFileAst.WriteInt("Common", "DiskFlag", diskFlagComboBox.SelectedIndex);
			iniFileAst.WriteString("Common", "DeviceName", deviceNameTextBox.Text);
			iniFileAst.WriteString("Common", "Addr", addrNumericUpDown.Value.ToString());
			iniFileAst.WriteString("Common", "HardwareID", hardwareIDTextBox.Text);
			iniFileAst.WriteString("Common", "Heartbeat", heartbeatTextBox.Text);
			iniFileAst.WriteString("Common", "HeartbeatCycle", heartbeatCycleNumericUpDown.Value.ToString());

			iniFileAst.WriteInt("Network", "LinkMode", linkModeComboBox.SelectedIndex);
			iniFileAst.WriteString("Network", "LinkPort", linkPortTextBox.Text);
			iniFileAst.WriteString("Network", "IP", IPTextBox.Text);
			iniFileAst.WriteString("Network", "NetMask", netmaskTextBox.Text);
			iniFileAst.WriteString("Network", "GateWay", gatewayTextBox.Text);
			iniFileAst.WriteString("Network", "Mac", macTextBox.Text);

			iniFileAst.WriteInt("Other", "Baud", baudComboBox.SelectedIndex);
			iniFileAst.WriteString("Other", "RemoteHost", remoteHostTextBox.Text);
			iniFileAst.WriteString("Other", "RemotePort", remotePortTextBox.Text);
			iniFileAst.WriteString("Other", "DomainName", domainNameTextBox.Text);
			iniFileAst.WriteString("Other", "DomainServer", domainServerTextBox.Text);

			this.isNew = false;
			this.Text = "硬件设置(" + hName + ")";

			MessageBox.Show("成功保存");
		}

		/// <summary>
		/// 点击右上角关闭按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HardwareSetForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
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
			if ( (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8) || e.KeyChar == '.')
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}

		/// <summary>
		/// 辅助监听器：验证Mac地址
		/// </summary>
		private void validateMac_KeyPress(object sender, KeyPressEventArgs e)
		{

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
		///  点击《下载》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void downloadButton_Click(object sender, EventArgs e)
		{
			ConnectTools cTools = ConnectTools.GetInstance();
			Dictionary<string,string> allDevices = cTools.GetDeviceInfo();
			cTools.PutPara(new List<string>(allDevices.Keys), iniPath, new ReceiveCallBack());	
		}

		private void connectButton_Click(object sender, EventArgs e)
		{
			ConnectTools cTools = ConnectTools.GetInstance();
			cTools.Start("192.168.31.14");
			cTools.SearchDevice();
		}
	}

	class ReceiveCallBack : IReceiveCallBack
	{
		public void SendCompleted(string ip, string order)
		{
			MessageBox.Show("下载成功");
		}

		public void SendError(string ip, string order)
		{
			MessageBox.Show("下载失败");
		}
	}

}