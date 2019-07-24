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
	public partial class HardwareSetForm : Form
	{
		private MainForm mainForm;
		private string iniPath;
		private bool isNew = true;

		/// <summary>
		/// 构造函数：初始化各个变量
		/// </summary>
		/// <param name="iniPath">通过传入iniPath（空值或有值）来决定要生成的数据的模板</param>
		public HardwareSetForm(MainForm mainForm, string iniPath)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			this.iniPath = iniPath;

			// 若iniPath 为空，则新建-》读取默认Hardware.ini，并载入到当前form中
			if (String.IsNullOrEmpty(iniPath) ){
				isNew = true;
				iniPath = Application.StartupPath + @"\HardwareSet.ini";				
			}// 否则打开相应配置文件，并载入到当前form中
			else {
				isNew = false;		
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

			sumUseTimesTextBox.Text = iniFileAst.ReadString("Common", "SumUseTimes","");
			currUseTimesTextBox.Text = iniFileAst.ReadString("Common", "CurrUseTimes", "");
			diskFlagTextBox.Text = iniFileAst.ReadString("Common", "DiskFlag", "");
			deviceNameTextBox.Text = iniFileAst.ReadString("Common", "DeviceName", "");
			addrTextBox.Text = iniFileAst.ReadString("Common", "Addr", "");
			hardwareIDTextBox.Text = iniFileAst.ReadString("Common", "HardwareID", "");
			heartbeatTextBox.Text = iniFileAst.ReadString("Common", "Heartbeat", "");
			heartbeatCycleTextBox.Text = iniFileAst.ReadString("Common", "HeartbeatCycle", "");

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
				SaveAll(iniPath);
			}
			
		}

		/// <summary>
		/// 辅助方法：通用的方法，供新建(NewHardwareForm)及旧版本的保存
		/// </summary>
		/// <param name="hardwareSetForm"></param>
		internal void SaveAll(String iniPath)
		{
			this.iniPath = iniPath;
			IniFileAst iniFileAst = new IniFileAst(iniPath);

			iniFileAst.WriteString("Common", "SumUseTimes", sumUseTimesTextBox.Text);
			iniFileAst.WriteString("Common", "CurrUseTimes", currUseTimesTextBox.Text);
			iniFileAst.WriteString("Common", "DiskFlag", diskFlagTextBox.Text);
			iniFileAst.WriteString("Common", "DeviceName", deviceNameTextBox.Text);
			iniFileAst.WriteString("Common", "Addr", addrTextBox.Text);
			iniFileAst.WriteString("Common", "HardwareID", hardwareIDTextBox.Text);
			iniFileAst.WriteString("Common", "Heartbeat", heartbeatTextBox.Text);
			iniFileAst.WriteString("Common", "HeartbeatCycle", heartbeatCycleTextBox.Text);

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
			MessageBox.Show("成功保存");
		}
	}


}
