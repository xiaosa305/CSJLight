using LightController.Common;
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
		private MainFormInterface mainForm;
		private string iniPath;  
		private string hName;
		private bool isNew = true;
		private bool isSaved = false;

		private ConnectTools connectTools;
		private SerialPortTools comTools;

		private IList<string> ips;  //搜索到的ip列表 ，将填进ipsComboBox
		private IList<string> selectedIPs;  //填充进去的ip列表，用以发送数据

		private string[] comList; // 搜索到的除DMX512外的所有串口
		private string comName;  // 选中的串口名

		private string localIP; //设置的本地IP

		/// <summary>
		/// 构造函数：初始化各个变量
		/// </summary>
		/// <param name="iniPath">通过传入iniPath（空值或有值）来决定要生成的数据的模板</param>
		public HardwareSetForm(MainFormInterface mainForm, string iniPath, string hName)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			this.iniPath = iniPath;
			skinTabControl.SelectedIndex = 0;

			// 若iniPath 为空，则新建-》读取默认Hardware.ini，并载入到当前form中
			if (String.IsNullOrEmpty(iniPath)) {
				isNew = true;
				isSaved = false;
				iniPath = Application.StartupPath + @"\HardwareSet.ini";
				this.Text = "硬件设置(未保存)";
			}// 否则打开相应配置文件，并载入到当前form中
			else {
				isNew = false;
				isSaved = true;
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
		private void cancelSkinButton_Click(object sender, EventArgs e)
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
			IniFileAst iniFileAst = new IniFileAst(iniPath);

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
			this.isSaved = true;

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
		private void getLocalIPsSkinButton_Click(object sender, EventArgs e)
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

			networkSearchSkinButton.Enabled = !String.IsNullOrEmpty(localIP);
			networkUploadSkinButton.Enabled = false;
			networkDownloadSkinButton.Enabled = false; 			
		}

		/// <summary>
		/// 事件：点击《搜索网络连接》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkSearchSkinButton_Click(object sender, EventArgs e)
		{
			//搜索期间不可进行其他操作
			networkSearchSkinButton.Enabled = false;
			networkUploadSkinButton.Enabled = false;
			networkDownloadSkinButton.Enabled = false;

			connectTools = ConnectTools.GetInstance();
			connectTools.Start(localIP);
			connectTools.SearchDevice();
			Thread.Sleep(1000);

			Dictionary<string,string> allDevices = 	connectTools.GetDeviceInfo();			
			ipsComboBox.Items.Clear();
			ips = new List<string>();
			if (allDevices.Count > 0)
			{
				foreach (KeyValuePair<string, string> device in allDevices)
				{
					ipsComboBox.Items.Add(device.Value + "(" + device.Key + ")");
					ips.Add(device.Key);
				}
				ipsComboBox.SelectedIndex = 0;
				ipsComboBox.Enabled = true;
			}
			else {
				MessageBox.Show("未找到可用网络设备，请确定设备已连接后重试");
				ipsComboBox.SelectedIndex = -1;
				ipsComboBox.Enabled = false;
			}
			//搜索完成后，再将按钮开放
			networkSearchSkinButton.Enabled = true;
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
				networkUploadSkinButton.Enabled = false;
				networkDownloadSkinButton.Enabled = false;
				return;
			}

			selectedIPs = new List<string>();
			selectedIPs.Add(ips[ipsComboBox.SelectedIndex]);
			networkUploadSkinButton.Enabled = true;
			networkDownloadSkinButton.Enabled = true;
		}	

		/// <summary>
		/// 事件：点击《网络回读》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void uploadSkinButton_Click(object sender, EventArgs e)
		{			
			connectTools.GetParam(selectedIPs, new UploadCallBackHardwareSet(this));
			afterReadOrWrite();
		}
		
		/// <summary>
		///  事件：点击《网络下载》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkDownloadButton_Click(object sender, EventArgs e)
		{
			if (isSaved)
			{
				// 11.7 保存前，先保存一遍当前数据。
				saveAll(iniPath,hName);
				// 此语句只发送《硬件配置》到选中的设备中
				connectTools.PutPara(selectedIPs, iniPath, new DownloadCallBackHardwareSet());
				afterReadOrWrite();
			}
			else {
				MessageBox.Show("下载之前需先保存当前设置。");
			}
		}

		/// <summary>
		/// 辅助方法：网络下载或回读不论成功失败，都会把所有按钮都设为不可用，应重新搜索网络设备才行
		/// </summary>
		private void afterReadOrWrite() {
			ipsComboBox.Enabled = false;
			networkUploadSkinButton.Enabled = false;
			networkDownloadSkinButton.Enabled = false;
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
			comSearchSkinButton.Enabled = false;
			comComboBox.Enabled = false;
			comConnectSkinButton.Enabled = false;
			comUploadSkinButton.Enabled = false;
			comDownloadSkinButton.Enabled = false;

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
				comConnectSkinButton.Enabled = true;
			}
			else
			{
				MessageBox.Show("未找到可用串口，请重试");
				comComboBox.SelectedIndex = -1;
			}
			comSearchSkinButton.Enabled = true;
		}

		/// <summary>
		/// 事件：点击《选择串口设备》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comConnectSkinButton_Click(object sender, EventArgs e)
		{
				comName = comComboBox.Text;
				comTools.OpenCom(comName);
				MessageBox.Show("已选中串口设备");
				comUploadSkinButton.Enabled = true;
				comDownloadSkinButton.Enabled = true;			
		}

		/// <summary>
		/// 事件：点击《串口回读》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comUploadSkinButton_Click(object sender, EventArgs e)
		{
			comTools.GetParam(new UploadCallBackHardwareSet(this));
		}

		/// <summary>
		/// 事件：点击《串口下载》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comDownloadSkinButton_Click(object sender, EventArgs e)
		{
			if (isSaved)
			{
				// 11.7 保存前，先保存一遍当前数据。
				saveAll(iniPath, hName);
				// 此语句只发送《硬件配置》到选中的设备中
				comTools.PutParam(iniPath, new DownloadCallBackHardwareSet());
			}
			else
			{
				MessageBox.Show("下载之前需先保存当前设置。");
			}			
		}

		#endregion


		#region 几个通用辅助方法

		/// <summary>
		/// 辅助方法：读取配置文件
		/// </summary>
		/// <param name="iniPath"></param>
		private void readIniFile(string iniPath)
		{
			IniFileAst iniFileAst = new IniFileAst(iniPath);

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
	}

	/// <summary>
	/// 辅助类：用以下载硬件设置时供底层调用的回调类，显示回馈信息
	/// </summary>
	class DownloadCallBackHardwareSet : ICommunicatorCallBack
	{
		public void Completed(string deviceTag)
		{
			MessageBox.Show("下载成功。");
		}

		public void Error(string deviceTag, string errorMessage)
		{
			MessageBox.Show("下载失败。");
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
