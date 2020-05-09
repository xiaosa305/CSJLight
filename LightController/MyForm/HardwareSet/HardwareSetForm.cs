﻿using LightController.Common;
using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.MyForm
{
	public partial class HardwareSetForm : Form
	{
		private MainFormBase mainForm;
		private string iniPath;  
		private string hName;

		/// <summary>
		/// 是否新建：亦即是否已设定存储目录
		/// </summary>
		private bool isNew = true; 

		private ConnectTools connectTools;		
		private SerialPortTools comTools;
		private bool isComConnected = false ;

		private IList<string> ips;  //搜索到的ip列表 ，将填进ipsComboBox
		private IList<string> selectedIPs;  //填充进去的ip列表，用以发送数据

		private string[] comList; // 搜索到的除DMX512外的所有串口
		private string comName;  // 选中的串口名

		private string localIP; //设置的本地IP

		/// <summary>
		/// 构造函数：初始化各个变量
		/// </summary>
		/// <param name="iniPath">通过传入iniPath（空值或有值）来决定要生成的数据的模板</param>
		public HardwareSetForm(MainFormBase mainForm, string iniPath, string hName)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			this.iniPath = iniPath;
			skinTabControl.SelectedIndex = 0;

			// 若iniPath 为空，则新建-》读取默认Hardware.ini，并载入到当前form中
			if (String.IsNullOrEmpty(iniPath)) {
				isNew = true;
				//isSetDir = false;
				iniPath = Application.StartupPath + @"\HardwareSet.ini";
				this.Text = "硬件设置(未保存)";
			}// 否则打开相应配置文件，并载入到当前form中
			else {
				isNew = false;
				//isSetDir = true;
				this.hName = hName;
				this.Text = "硬件设置(" + hName + ")";
			}
			readIniFile(iniPath);
		}

		/// <summary>
		///  事件：窗口绘制时设初始地址
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HardwareSetForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			// 设false可在其他文件中修改本类的UI
			Control.CheckForIllegalCrossThreadCalls = false;

			//9.7 自动搜索本地IP列表及串口列表
			getLocalIPs();
			comSearch();
		}

		#region 几个通用方法：保存、取消(关闭窗口)等

		/// <summary>
		/// 事件：点击《保存》操作：
		/// 1、若是全新的版本，用一个newHardwareForm来生成文件夹名
		/// 2、若是旧的版本，则直接使用该版本来保存信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			if (isNew)
			{
				HardwareSaveForm nhForm = new HardwareSaveForm(this);
				nhForm.ShowDialog();
			}
			else
			{
				Save(iniPath, hName);
			}
		}

		/// <summary>
		/// 事件：点击《取消》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			this.mainForm.Activate();
		}

		/// <summary>
		/// 辅助方法：通用的方法，供新建(NewHardwareForm)及旧版本的保存
		/// </summary>
		/// <param name="hardwareSetForm"></param>
		internal void Save(String iniPath, string hName)
		{
			saveAll(iniPath, hName);
			MessageBox.Show("成功保存");
		}

		/// <summary>
		/// 辅助方法：供Save()使用，主要是当 《（串口或网络）下载 》时，应先保存一遍此ini,此时不要弹出成功保存功能。
		/// </summary>
		/// <param name="iniPath"></param>
		/// <param name="hName"></param>
		private void saveAll(String iniPath, string hName) {

			this.iniPath = iniPath;
			this.hName = hName;
			IniFileHelper iniFileAst = new IniFileHelper(iniPath);

			// 9.28 直接保存numericUpDown表面上看到的Text(因为写到ini中去了）
			iniFileAst.WriteString("Common", "SumUseTimes", sumUseTimeNumericUpDown.Text);
			iniFileAst.WriteString("Common", "CurrUseTimes", currUseTimeNumericUpDown.Text);
			iniFileAst.WriteInt("Common", "DiskFlag", diskFlagComboBox.SelectedIndex);
			iniFileAst.WriteString("Common", "DeviceName", deviceNameTextBox.Text);
			iniFileAst.WriteString("Common", "Addr", addrNumericUpDown.Text);
			iniFileAst.WriteString("Common", "HardwareID", hardwareIDTextBox.Text);
			iniFileAst.WriteString("Common", "Heartbeat", heartbeatTextBox.Text);
			iniFileAst.WriteString("Common", "HeartbeatCycle", heartbeatCycleNumericUpDown.Text);
			iniFileAst.WriteInt("Common", "PlayFlag", playFlagComboBox.SelectedIndex);

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
			//this.isSetDir = true;

		}
		
		/// <summary>
		/// 点击右上角关闭按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HardwareSetForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (isComConnected) {
				comTools.CloseDevice();
			}
			this.Dispose();
			mainForm.Activate();
		}

		#endregion

		#region 几个输入监视器

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
			if ( (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8 || e.KeyChar == '.')
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
		/// 事件(监视器)：处理NumericUpDown的Leave 事件，以恢复显示
		/// -- 这种数字框，如果用户主动删除内容，则之后设value都不会显示，容易产生误导,
		/// --	 其value不一定等于输入框中的数字！
		/// -- 因为value绝对不为空，但输入框可能为空，则当输入框为空时，value会保留之前的Decimal值
		/// </summary>
		/// <param name="s"></param>
		/// <param name="e"></param>
		private void numericUpDown_RecoverNum(object s, EventArgs e)
		{
			var n = (NumericUpDown)s;
			if (n.Text == "")
			{
				n.Value = 0;
				n.Text = "0";
			}
		}

		#endregion

		#region 网络相关读写

		/// <summary>
		///  事件：点击《获取本地IP列表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void getLocalIPsButton_Click(object sender, EventArgs e)
		{
			getLocalIPs();
		}

		/// <summary>
		/// 辅助方法：获取本地IP列表：①Form刚载入时（Load) ；②点击《获取本地IP列表》按钮
		/// </summary>
		private void getLocalIPs()
		{
			IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
			localIPsComboBox.Items.Clear();
			foreach (IPAddress ip in ipe.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork) //当前ip为ipv4时，才加入到列表中
				{
					localIPsComboBox.Items.Add(ip);
				}
			}

			if (localIPsComboBox.Items.Count > 0)
			{
				localIPsComboBox.Enabled = true;
				localIPsComboBox.SelectedIndex = 0;
			}
			else
			{
				localIPsComboBox.Enabled = false;
				localIPsComboBox.SelectedIndex = -1;
			}
		}

		/// <summary>
		/// 辅助方法：直接在选中本地ip（主动或被动）的时候，设置localIP即可
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void localIPsComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{			
			localIP = localIPsComboBox.Text;

			ipsComboBox.Text = "";
			ipsComboBox.Enabled = false;

			networkSearchButton.Enabled = !String.IsNullOrEmpty(localIP);
			networkReadButton.Enabled = false;
			networkDownloadButton.Enabled = false; 			
		}

		/// <summary>
		/// 事件：点击《搜索网络连接》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkSearchButton_Click(object sender, EventArgs e)
		{
			ipsComboBox.Items.Clear();
			ips = new List<string>();

			//搜索期间不可进行其他操作
			networkSearchButton.Enabled = false;
			networkReadButton.Enabled = false;
			networkDownloadButton.Enabled = false;

			connectTools = ConnectTools.GetInstance();
			connectTools.Start(localIP);
			connectTools.SearchDevice();
			Thread.Sleep(MainFormBase.NETWORK_WAITTIME);

			
			Dictionary<string, Dictionary<string, NetworkDeviceInfo>> allDevices = connectTools.GetDeivceInfos();			
			foreach (KeyValuePair<string, NetworkDeviceInfo> d2 in allDevices[localIP])
			{
				ipsComboBox.Items.Add(d2.Value.DeviceName + "(" + d2.Value.DeviceIp + ")");
				ips.Add(d2.Value.DeviceIp);
			}
						
			if (ipsComboBox.Items.Count > 0)
			{
				ipsComboBox.SelectedIndex = 0;
				ipsComboBox.Enabled = true;
			}
			else {
					MessageBox.Show("未找到可用网络设备，请确定设备已连接后重试");
					ipsComboBox.SelectedIndex = -1;
					ipsComboBox.Enabled = false;
			}

			//搜索完成后，再将按钮开放
			networkSearchButton.Enabled = true;
		}


		/// <summary>
		/// 辅助方法：只要改变了网络设备，就更改相关的网络下载和回读之类的，并设置为选中的ip地址
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ipsComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 若未选中任何项或选中项为空字符串，则使不能回读或下载，并退出。			
			if (ipsComboBox.SelectedIndex == -1 || String.IsNullOrEmpty(ipsComboBox.Text) ) {				
				networkReadButton.Enabled = false;
				networkDownloadButton.Enabled = false;
				return;
			}

			selectedIPs = new List<string>	{ips[ipsComboBox.SelectedIndex]	};
			networkReadButton.Enabled = true;
			networkDownloadButton.Enabled = true;
		}	

		/// <summary>
		/// 事件：点击《网络回读》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkReadButton_Click(object sender, EventArgs e)
		{
			if (connectTools.Connect(connectTools.GetDeivceInfos()[localIP][selectedIPs[0]]))
			{
				dhcpCheckBox.Checked = false;
				macCheckBox.Checked = false;
				connectTools.GetParam(selectedIPs, new UploadCallBackHardwareSet(this));
			}
			else
			{
				MessageBox.Show("网络设备连接失败，无法回读配置。");	
			}
		}

		/// <summary>
		///  事件：点击《网络下载》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkDownloadButton_Click(object sender, EventArgs e)
		{
			if (isNew)
			{
				MessageBox.Show("下载之前需先保存配置(设置配置文件名)。");
				return;
			}
			
			// 若被去掉了勾选，则需要提示用户
			if (!autoSaveCheckBox.Checked) {
				DialogResult dr = MessageBox.Show("下载配置时会自动保存当前配置，是否继续？",
				"继续下载？",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Warning
			);
				if (dr == DialogResult.Cancel)
				{
					return;
				}
			}		

			// 11.7 保存前，先保存一遍当前数据。
			saveAll(iniPath,hName);

			// 此语句只发送《硬件配置》到选中的设备中
			if (connectTools.Connect(connectTools.GetDeivceInfos()[localIP][selectedIPs[0]]))
			{
				connectTools.PutPara(selectedIPs, iniPath, new DownloadCallBackHardwareSet( this ));

			}
			else {
				MessageBox.Show("网络设备连接失败，无法下载配置。");
			}
		}
	

		#endregion

		#region  串口读写相关

		/// <summary>
		///  事件：点击《搜索串口连接》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comSearchSkinButton_Click(object sender, EventArgs e)
		{
			comSearch();
		}

		/// <summary>
		///  辅助方法：搜索串口列表：①Form载入时（load）；②点击《搜索串口连接》
		/// </summary>
		private void comSearch()
		{
			comSearchButton.Enabled = false;
			comComboBox.Enabled = false;
			comConnectButton.Enabled = false;
			comReadButton.Enabled = false;
			comDownloadButton.Enabled = false;

			comTools = SerialPortTools.GetInstance();
			comList = comTools.GetSerialPortNameList();
			comComboBox.Items.Clear();
			if (comList.Length > 0)
			{
				foreach (string com in comList)
				{
					comComboBox.Items.Add(com);
				}
				comComboBox.Enabled = true;
				comComboBox.SelectedIndex = 0;
				comConnectButton.Enabled = true;
			}
			else
			{
				MessageBox.Show("未找到可用串口，请重试");
				comComboBox.SelectedIndex = -1;
			}
			comSearchButton.Enabled = true;
		}

		/// <summary>
		/// 事件：点击《打开|关闭串口连接》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comConnectSkinButton_Click(object sender, EventArgs e)
		{
			isComConnected = !isComConnected;

			if (isComConnected) {
				comName = comComboBox.Text;
				comTools.OpenCom(comName);				
			}
			else
			{
				comTools.CloseDevice();
			}
			comSearchButton.Enabled =! isComConnected;
			comComboBox.Enabled = !isComConnected;
			comConnectButton.Text = (isComConnected ? "关闭" : "打开") + "串口连接";
			comReadButton.Enabled = isComConnected;
			comDownloadButton.Enabled = isComConnected;

			MessageBox.Show("已" + (isComConnected?"打开":"关闭") + "串口连接");
		}

		/// <summary>
		/// 事件：点击《串口回读》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comReadButton_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(comName))
			{
				MessageBox.Show("请选择串口设备。");
				return;
			}

			dhcpCheckBox.Checked = false;
			macCheckBox.Checked = false;
			
			comTools.OpenCom(comName); // 为保证串口没有断开，需主动连接一次
			comTools.GetParam(new UploadCallBackHardwareSet(this));
		}

		/// <summary>
		/// 事件：点击《串口下载》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comDownloadSkinButton_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(comName))
			{
				MessageBox.Show("请选择串口设备。");
				return;
			}

			if (isNew ) {
				MessageBox.Show("下载之前需先保存配置(设置配置文件名)。");
				return;
			}

			// 若被去掉了勾选，则需要提示用户
			if (!autoSaveCheckBox.Checked)
			{
				DialogResult dr = MessageBox.Show("下载配置时会自动保存当前配置，是否继续？",
					"继续下载？",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Warning
				);
				if (dr == DialogResult.Cancel)
				{
					return;
				}
			}

			// 11.7 保存前，先保存一遍当前数据。
			saveAll(iniPath, hName);
			// 此语句只发送《硬件配置》到选中的设备中
			comTools.OpenCom(comName);// 为保证串口没有断开，需主动连接一次
			comTools.PutParam(iniPath, new DownloadCallBackHardwareSet(this));	
		}

		#endregion

		#region 几个通用辅助方法

		/// <summary>
		/// 辅助方法：读取配置文件
		/// </summary>
		/// <param name="iniPath"></param>
		private void readIniFile(string iniPath)
		{
			IniFileHelper iniFileAst = new IniFileHelper(iniPath);

			deviceNameTextBox.Text = iniFileAst.ReadString("Common", "DeviceName", "");
			addrNumericUpDown.Value = iniFileAst.ReadInt("Common", "Addr", 0);
			diskFlagComboBox.SelectedIndex = iniFileAst.ReadInt("Common", "DiskFlag", 0);
			hardwareIDTextBox.Text = iniFileAst.ReadString("Common", "HardwareID", "");
			sumUseTimeNumericUpDown.Value = iniFileAst.ReadInt("Common", "SumUseTimes", 0);
			currUseTimeNumericUpDown.Value = iniFileAst.ReadInt("Common", "CurrUseTimes", 0);
			heartbeatTextBox.Text = iniFileAst.ReadString("Common", "Heartbeat", "");
			heartbeatCycleNumericUpDown.Value = iniFileAst.ReadInt("Common", "HeartbeatCycle", 0);
			baudComboBox.SelectedIndex = iniFileAst.ReadInt("Other", "Baud", 0);
			playFlagComboBox.SelectedIndex = iniFileAst.ReadInt("Common", "PlayFlag", 1);

			linkModeComboBox.SelectedIndex = iniFileAst.ReadInt("Network", "LinkMode", 0);
			IPTextBox.Text = iniFileAst.ReadString("Network", "IP", "");
			linkPortTextBox.Text = iniFileAst.ReadString("Network", "LinkPort", "");
			netmaskTextBox.Text = iniFileAst.ReadString("Network", "NetMask", "");
			gatewayTextBox.Text = iniFileAst.ReadString("Network", "GateWay", "");
			macTextBox.Text = iniFileAst.ReadString("Network", "Mac", "");

			remoteHostTextBox.Text = iniFileAst.ReadString("Other", "RemoteHost", "");
			remotePortTextBox.Text = iniFileAst.ReadString("Other", "RemotePort", "");
			domainNameTextBox.Text = iniFileAst.ReadString("Other", "DomainName", "");
			domainServerTextBox.Text = iniFileAst.ReadString("Other", "DomainServer", "");
		}

		/// <summary>
		///  辅助方法：通过回读的CSJ_Hardware对象，来填充左侧的所有输入框。
		/// </summary>
		/// <param name="ch"></param>
		public void SetParamFromDevice(CSJ_Hardware ch)
		{
			try
			{
				deviceNameTextBox.Text = ch.DeviceName;
				addrNumericUpDown.Value = ch.Addr;
				diskFlagComboBox.SelectedIndex = ch.DiskFlag;
				hardwareIDTextBox.Text = ch.HardWareID;
				sumUseTimeNumericUpDown.Value = ch.SumUseTimes;
				currUseTimeNumericUpDown.Value = ch.CurrUseTimes;
				heartbeatTextBox.Text = Encoding.Default.GetString(ch.Heartbeat);
				heartbeatCycleNumericUpDown.Value = ch.HeartbeatCycle;
				baudComboBox.SelectedIndex = ch.Baud;
				playFlagComboBox.SelectedIndex = ch.PlayFlag;

				linkModeComboBox.SelectedIndex = ch.LinkMode;
				IPTextBox.Text = ch.IP;
				linkPortTextBox.Text = ch.LinkPort.ToString();
				netmaskTextBox.Text = ch.NetMask;
				gatewayTextBox.Text = ch.GateWay;
				macTextBox.Text = ch.Mac;
				// 根据回读的配置，主动勾选《DHCP》及《mac》
				if (ch.IP.Equals("0.0.0.0")) {
					dhcpCheckBox.Checked = true;
				}
				if (ch.Mac.Equals("00-00-00-00-00-00")) {
					macCheckBox.Checked = true;
				}

				remoteHostTextBox.Text = ch.RemoteHost;
				remotePortTextBox.Text = ch.RemotePort.ToString();
				domainNameTextBox.Text = ch.DomainName;
				domainServerTextBox.Text = ch.DomainServer;
				
			}
			catch (Exception ex) {
				MessageBox.Show("回读异常:" + ex.Message);
			}
		}
		#endregion


		/// <summary>
		/// 事件：勾选《启用DHCP》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dhcpCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			bool enableDHCP = dhcpCheckBox.Checked;			
			
			IPTextBox.Enabled = !enableDHCP;
			netmaskTextBox.Enabled = !enableDHCP; 		
			gatewayTextBox.Enabled = !enableDHCP;

			if ( enableDHCP) {
				IPTextBox.Text = "0.0.0.0";
				netmaskTextBox.Text = "255.255.255.0";
				gatewayTextBox.Text = "0.0.0.0";
			}
		}

		/// <summary>
		/// 事件：勾选《自动获取MAC地址》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void macCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			bool autosetMac = macTextBox.Enabled;
			macTextBox.Enabled = !autosetMac ;
			if (autosetMac)
			{
				macTextBox.Text = "00-00-00-00-00-00";
			}				
		}

		/// <summary>
		/// 事件：点击《右上角？》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HardwareSetForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show("1.此界面设置，用户需要更改的是《主动标识》及《网络设置》内的相关设置；其他设置暂时没有作用，无需更改。\n" +
				"2.常规的操作步骤：先从设备回读配置，再修改需要变动的配置后，下载新配置。\n" +
				"3.下载配置前，软件需在本地生成配置文件，才能下载到设备中，避免误操作。");
			e.Cancel = true;
		}

		/// <summary>
		/// 辅助方法：重置《网络相关按钮组》
		/// </summary>
		internal void ResetNetworkButtons()
		{
			ipsComboBox.SelectedIndex = -1;
			ipsComboBox.Text = "";
			ipsComboBox.Enabled = false;

			networkReadButton.Enabled = false;
			networkDownloadButton.Enabled = false;
		}
	}

	/// <summary>
	/// 辅助类：用以下载硬件设置时供底层调用的回调类，显示回馈信息
	/// </summary>
	class DownloadCallBackHardwareSet : ICommunicatorCallBack
	{
		private HardwareSetForm huForm;		
		public DownloadCallBackHardwareSet(HardwareSetForm huForm)
		{
			this.huForm = huForm;
		}

		public void Completed(string deviceTag)
		{
			MessageBox.Show("硬件设置下载成功，请稍等片刻，等待设备重启。\n如使用网络模式，需重新搜索并连接网络设备。");
			huForm.ResetNetworkButtons();				
		}

		public void Error(string deviceTag, string errorMessage)
		{
			MessageBox.Show("硬件设置下载失败\n。如使用网络模式，需重新搜索并连接网络设备。");
			huForm.ResetNetworkButtons();
		}

		public void GetParam(CSJ_Hardware hardware)
		{
			//throw new NotImplementedException();
		}

		public void UpdateProgress(string deviceTag, string fileName, int newProgress)
		{
			//throw new NotImplementedException();
		}
	}

	/// <summary>
	/// 辅助类：用以回读硬件设置时供底层调用的回调类，显示回馈信息
	/// </summary>
	class UploadCallBackHardwareSet : ICommunicatorCallBack
	{

		private HardwareSetForm hsForm;	

		public UploadCallBackHardwareSet(HardwareSetForm hsForm)
		{
			this.hsForm = hsForm;
		}

		public void Completed(string deviceTag)
		{
			MessageBox.Show("回读成功");
		}

		public void Error(string deviceTag, string errorMessage)
		{
			MessageBox.Show("回读失败");
		}

		public void GetParam(CSJ_Hardware ch)
		{
			hsForm.SetParamFromDevice(ch);
		}

		public void UpdateProgress(string deviceTag, string fileName, int newProgress)
		{
			//throw new NotImplementedException();
		}
	}

}