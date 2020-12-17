using LightController.Common;
using LightController.PeripheralDevice;
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
		private string iniPath;  //配置文件(hardware.ini)路径
		private string hsName;	// 配置文件名
		private bool isNew = true;//是否新建：亦即是否已设定存储目录
		private BaseCommunication myConnect; // 保持着一个设备连接（串网口通用）
		private bool isConnected = false; //是否连接
		private bool isConnectCom = true; //是否串口连接
		private IList<NetworkDeviceInfo> networkDeviceList;  // 网络设备的列表			

		/// <summary>
		/// 构造函数：初始化各个变量
		/// </summary>
		/// <param name="iniPath">通过传入iniPath（空值或有值）来决定要生成的数据的模板</param>
		public HardwareSetForm(MainFormBase mainForm, string iniPath, string hsName)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			this.iniPath = iniPath;
			
			// 若iniPath 为空，则新建-》读取默认Hardware.ini，并载入到当前form中
			if (string.IsNullOrEmpty(iniPath))
			{
				isNew = true;
				iniPath = Application.StartupPath + @"\HardwareSet.ini";
				Text = "硬件配置(未保存)";
			}
			// 否则打开相应配置文件，并载入到当前form中
			else
			{
				isNew = false;
				this.hsName = hsName;
				Text = "硬件配置(" + hsName + ")";
			}
			CSJ_Hardware ch = new CSJ_Hardware(iniPath);
			SetParamFromDevice(ch);
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

			// 主动刷新设备列表
			refreshDeviceComboBox();
		}

		/// <summary>
		/// 事件：点击右上角关闭按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HardwareSetForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (isConnected)
			{
				myConnect.DisConnect();
				myConnect = null ;
			}

			Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：点击《右上角？》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HardwareSetForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show("1.此界面设置，用户需要更改的只有《主控标识》、《优先播放》及《网络配置》等少数配置；其他输入框暂时没有作用，无需更改；\n" +
											"2.常规的操作步骤为：先从设备回读配置，在修改需要变动的配置后，下载新配置；\n" +
											"3.下载配置前，软件需在本地生成配置文件(ini)，才能下载到设备中，以避免误操作。",
				"使用提示或说明",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
			e.Cancel = true;
		}

		/// <summary>
		/// 事件：点击《保存》操作：
		/// 1、若是全新的版本，用一个newHardwareForm来生成文件夹名
		/// 2、若是旧的版本，则直接使用该版本来保存信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			if (! checkAllFormat() ) {
				setNotice("有异常参数，请校对后重试！", false);
				return;
			}

			if (isNew)
			{
				HardwareSaveForm nhForm = new HardwareSaveForm(this);
				nhForm.ShowDialog();
			}
			else
			{
				SaveAll(iniPath, hsName,true);
			}
		}	

		/// <summary>
		/// 事件：点击《取消》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			this.mainForm.Activate();
		}
		
		/// <summary>
		/// 辅助方法：供Save()使用，主要是当 《（串口或网络）下载 》时，应先保存一遍此ini,此时不要弹出成功保存功能。
		/// </summary>
		/// <param name="iniPath"></param>
		/// <param name="hsName"></param>
		internal void SaveAll(string iniPath, string hsName,bool msgShow)
		{
			this.iniPath = iniPath;
			this.hsName = hsName;
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

			isNew = false;
			Text = "硬件配置(" + hsName + ")";

			setNotice("已成功保存配置。", msgShow);
		}
			   
		#region 几个输入监视器、及格式校验方法

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
		private bool checkAllFormat() {
			bool result = true;
			string errorMsg = "配置参数有错误，请重新输入：";

			if ( ! StringHelper.IsIP(IPTextBox.Text) ) {
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

			if( IPTextBox.Text.Trim() != "0.0.0.0" && gatewayTextBox.Text.Trim() == "0.0.0.0"  ){
				errorMsg += "\n未启用DHCP的情况下，【网关不能设为0.0.0.0】";
				result = false;
			}

			if (!result) {
				MessageBox.Show(errorMsg);
			}
			return result;
		}

		#endregion

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
				IPTextBox.Text = ch.IP ;
				linkPortTextBox.Text = ch.LinkPort.ToString();
				netmaskTextBox.Text = ch.NetMask;
				gatewayTextBox.Text = ch.GateWay;
				macTextBox.Text = ch.Mac;

				dhcpCheckBox.Checked = IPTextBox.Text.Trim().Equals("0.0.0.0");
				macCheckBox.Checked = macTextBox.Text.Trim().Equals("00-00-00-00-00-00");

				remoteHostTextBox.Text = ch.RemoteHost;
				remotePortTextBox.Text = ch.RemotePort.ToString();
				domainNameTextBox.Text = ch.DomainName;
				domainServerTextBox.Text = ch.DomainServer;				
			}
			catch (Exception ex)
			{
				MessageBox.Show("回读异常:" + ex.Message);
			}
		}
		
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

			if (enableDHCP)
			{
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
			macTextBox.Enabled = !autosetMac;
			if (autosetMac)
			{
				macTextBox.Text = "00-00-00-00-00-00";
			}
		}
		
		/// <summary>
		/// 事件：点击《切换为网络连接|串口连接》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void switchButton_Click(object sender, EventArgs e)
		{
			isConnectCom = !isConnectCom;
			switchButton.Text = isConnectCom ? "切换为网络连接" : "切换为串口连接";
			refreshButton.Text = isConnectCom ? "刷新串口" : "刷新网络";
			deviceConnectButton.Text = isConnectCom ? "打开串口" : "连接设备";
			refreshDeviceComboBox(); // switchButton_Click
		}

		/// <summary>
		/// 辅助方法：刷新deviceComboBox(设备列表），区分不同的连接方法。
		/// </summary>
		private void refreshDeviceComboBox()
		{
			// 刷新前，先清空列表(也先断开连接：只是保护性再跑一次)
			if (isConnected)
			{
				disConnect(); // refreshDeviceComboBox
			}

			disableDeviceComboBox();
			deviceConnectButton.Enabled = false;
			Refresh();

			// 获取串口列表（不代表一定能连上，串口需用户自行确认）
			if (isConnectCom)
			{
				if (myConnect == null)
				{
					myConnect = new SerialConnect();
				}
				List<string> comList = (myConnect as SerialConnect).GetSerialPortNames();
				if (comList != null && comList.Count > 0)
				{
					foreach (string comName in comList)
					{
						deviceComboBox.Items.Add(comName);
					}
				}
			}
			// 获取网络设备列表
			else
			{
				IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
				foreach (IPAddress ip in ipe.AddressList)
				{
					if (ip.AddressFamily == AddressFamily.InterNetwork) //当前ip为ipv4时，才加入到列表中
					{
						NetworkConnect.SearchDevice(ip.ToString());
						// 需要延迟片刻，才能找到设备;	故在此期间，主动暂停片刻
						Thread.Sleep(MainFormBase.NETWORK_WAITTIME);
					}
				}

				Dictionary<string, Dictionary<string, NetworkDeviceInfo>> allDevices = NetworkConnect.GetDeviceList();
				networkDeviceList = new List<NetworkDeviceInfo>();
				if (allDevices.Count > 0)
				{
					foreach (KeyValuePair<string, Dictionary<string, NetworkDeviceInfo>> device in allDevices)
					{
						foreach (KeyValuePair<string, NetworkDeviceInfo> d2 in device.Value)
						{
							string localIPLast = device.Key.ToString().Substring(device.Key.ToString().LastIndexOf("."));
							deviceComboBox.Items.Add(d2.Value.DeviceName + "(" + d2.Value.DeviceIp + ")" + localIPLast);
							networkDeviceList.Add(d2.Value);
						}
					}
				}
			}

			if (deviceComboBox.Items.Count > 0)
			{
				deviceComboBox.SelectedIndex = 0;
				deviceComboBox.Enabled = true;
				deviceConnectButton.Enabled = true;
				setNotice("已刷新" + (isConnectCom ? "串口" : "网络设备") + "列表。", false);
			}
			else
			{
				setNotice("未找到可用设备，请检查设备连接后重试。", false);
			}
		}
		
		/// <summary>
		/// 事件：点击《刷新串口|网络》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshButton_Click(object sender, EventArgs e)
		{
			refreshDeviceComboBox();
		}

		/// <summary>
		/// 事件：点击《连接、断开网络、| 打开、关闭串口》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceConnectButton_Click(object sender, EventArgs e)
		{
			// 如果已连接（按钮显示为“连接设备”)，则关闭连接
			if (isConnected)
			{
				disConnect(); //deviceConnectButton_Click			
				return;
			}

			// 若未连接，则连接；并分情况处理
			if (isConnectCom)
			{
				myConnect = new SerialConnect();
				try
				{			
					if( (myConnect as SerialConnect).OpenSerialPort(deviceComboBox.Text ) ) {
						isConnected = true;
						refreshConnectButtons();
						setNotice("已打开串口(" + deviceComboBox.Text + ")。", true);
					}				
				}
				catch (Exception ex)
				{
					setNotice("打开串口失败，原因是：\n" + ex.Message, true);
				}
			}
			else
			{
				NetworkDeviceInfo selectedNetworkDevice = networkDeviceList[deviceComboBox.SelectedIndex];
				string deviceName = selectedNetworkDevice.DeviceName;
				myConnect = new NetworkConnect();				
				if ( myConnect.Connect(selectedNetworkDevice) )
				{
					isConnected = true;
					refreshConnectButtons();
					setNotice("成功连接网络设备(" + deviceName + ")。", true);
				}
				else
				{
					setNotice("连接网络设备(" + deviceName + ")失败。", true);
				}
			}
		}

		/// <summary>
		/// 辅助方法：断开连接（主动断开连接、退出Form及切换连接方式时，都跑一次这个方法）
		/// </summary>
		private void disConnect()
		{
			if (myConnect != null && myConnect.IsConnected())
			{
				myConnect.DisConnect();					
				myConnect = null;
				isConnected = false;
				refreshConnectButtons();
				setNotice("已" + (isConnectCom ? "关闭串口(" + deviceComboBox.Text + ")" : "断开连接"), true);
			}
		}
		
		/// <summary>
		/// 辅助方法：刷新按键[可用性]及[显示的文字]
		/// </summary>
		private void refreshConnectButtons()
		{
			switchButton.Enabled = ! isConnected;
			deviceComboBox.Enabled =deviceComboBox.Items.Count>0 && !isConnected;
			refreshButton.Enabled =  !isConnected;
			deviceConnectButton.Enabled = deviceComboBox.Items.Count > 0 ;
			readButton.Enabled = isConnected;
			downloadButton.Enabled = isConnected;
			if (isConnectCom)
			{
				deviceConnectButton.Text = isConnected ? "关闭串口" : "打开串口";
			}
			else
			{
				deviceConnectButton.Text = isConnected ? "断开连接" : "连接设备";
			}
		}

		/// <summary>
		/// 事件：点击《回读配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void readButton_Click(object sender, EventArgs e)
		{
			myConnect.GetParam(	GetParamCompleted, GetParamError);
		}

		/// <summary>
		/// 辅助回调方法：回读配置成功
		/// </summary>
		/// <param name="obj"></param>
		public void GetParamCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				CSJ_Hardware ch = obj as CSJ_Hardware;
				SetParamFromDevice(ch);
				setNotice("成功回读硬件配置。", true);
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
				setNotice("回读配置失败[" + msg + "]", true);
			});
		}	

		/// <summary>
		/// 事件：点击《下载配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void downloadButton_Click(object sender, EventArgs e)
		{
			if (!checkAllFormat())
			{
				setNotice("有异常参数，请校对后重试！", false);
				return;
			}

			if (isNew)
			{
				setNotice("下载之前需先保存配置(设定配置文件名)。",true);
				return;
			}		

			// 11.7 保存前，先保存一遍当前数据。
			SaveAll(iniPath, hsName,false);

			// 下载配置			
			setNotice("正在下载配置，请稍候...",false);
			setBusy(true);
			myConnect.PutParam(iniPath, PutParamCompleted, PutParamError);
		}

		/// <summary>
			/// 辅助回调方法：下载配置成功
			/// </summary>
			/// <param name="obj"></param>
		public void PutParamCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice("硬件配置下载成功,请等待设备重启(约耗时5s)...", true);
				Thread.Sleep(5000);
				if (isConnectCom)
				{
					setNotice("请继续操作。如出现错误，可先关闭再打开串口后重试。",true);	
				}
				else
				{
					myConnect.DisConnect();
					myConnect = null ;
					isConnected = false;
					disableDeviceComboBox();
					refreshConnectButtons();					
					setNotice("请刷新网络，并重新连接设备。如未找到设备，请稍等片刻重试。", true);					
				}
				setBusy(false);
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
				setNotice("下载配置失败[" + msg + "]", true);
                setBusy(false);
            });
		}
		
		/// <summary>
		/// 辅助方法：禁用设备列表下拉框,并清空其数据
		/// </summary>
		private void disableDeviceComboBox() {
			deviceComboBox.Items.Clear();
			deviceComboBox.SelectedIndex = -1;
			deviceComboBox.Text = "";
			deviceComboBox.Enabled = false;
		}

		#region 通用方法

		/// <summary>
		/// 辅助方法：显示信息
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="messageBoxShow">是否在提示盒内提示</param>
		private void setNotice(string msg, bool messageBoxShow)
		{
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

		#endregion

	}

}
