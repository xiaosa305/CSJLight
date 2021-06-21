using LightController.Common;
using LightController.PeripheralDevice;
using LightController.Tools.CSJ.IMPL;
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

namespace LightController.MyForm.MainFormAst
{
	public partial class ConnectForm : Form
	{
		public static int SEARCH_WAITTIME = 1000; //网络搜索时的通用暂停时间
		public static int REBOOT_WATITIME = 5000; //设备重启时间
		public static int SEND_WAITTIME = 500; // 发送指令后等待时间
		private MainFormBase mainForm;
		private IList<NetworkDeviceInfo> networkDeviceList; //记录所有的device列表(包括连接的本地IP和设备信息，故如有多个同网段IP，则同一个设备可能有多个列表值)		

		public ConnectForm(MainFormBase mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			SEARCH_WAITTIME = IniHelper.GetSystemCount("waitTime", 1000);
			REBOOT_WATITIME = IniHelper.GetSystemCount("rebootTime", 5000);
			SEND_WAITTIME = IniHelper.GetSystemCount("sendTime", 500);
		}

		/// <summary>
		///  Load事件：根据鼠标位置放置窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ConnectForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
		}

		/// <summary>
		///  介于Load和Activate之间的一个事件：可以使得某些操作在界面加载完成才继续操作(Load则需完成后才渲染出来)，但又可以避免Activated事件每次重获焦点就执行的缺陷；
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ConnectForm_Shown(object sender, EventArgs e)
		{
			Console.WriteLine("ConnectForm_Shown ");
			if (!mainForm.IsConnected)
			{
				deviceRefreshButton_Click(null, null);
			}
		}

		/// <summary>
		/// 事件：点击《（右上角）?》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ConnectForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			if (DialogResult.Yes == MessageBox.Show("设备出厂时的默认IP地址为192.168.2.10；如果当前电脑当前不在此网段内(即本机IP非192.168.2.X)，将无法搜到设备。是否要打开《网络连接》设置本地IP?",
				"设置IP？",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question))
			{
				System.Diagnostics.Process.Start("ncpa.cpl");
			}
		}

		/// <summary>
		/// 事件：关闭本窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ConnectForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Hide();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：点击《刷新列表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceRefreshButton_Click(object sender, EventArgs e)
		{
			setNotice("正在搜索设备，请稍候...", false, true);

			deviceComboBox.Items.Clear();
			deviceComboBox.SelectedIndex = -1;
			deviceComboBox.Enabled = false;
			deviceRefreshButton.Enabled = false;
			deviceConnectButton.Text = "连接设备"; // 每次点击刷新列表，都需要重连设备
			deviceConnectButton.Enabled = false;
			refreshRestartButton();
			Refresh();

			NetworkConnect.ClearDeviceList();

			// 先获取本地ip列表，遍历使用这些ip，搜索设备;-->都搜索完毕再统一显示
			IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
			foreach (IPAddress ip in ipe.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork) //当前ip为ipv4时，才加入到列表中
				{
					NetworkConnect.SearchDevice(ip.ToString());
					// 需要延迟片刻，才能找到设备;	故在此期间，主动暂停片刻
					Thread.Sleep(SEARCH_WAITTIME);
				}
			}

			networkDeviceList = new List<NetworkDeviceInfo>();
			Dictionary<string, Dictionary<string, NetworkDeviceInfo>> allDevices = NetworkConnect.GetDeviceList();
			if (allDevices.Count > 0)
			{
				foreach (KeyValuePair<string, Dictionary<string, NetworkDeviceInfo>> device in allDevices)
				{
					foreach (KeyValuePair<string, NetworkDeviceInfo> d2 in device.Value)
					{
						string localIPLast = device.Key.ToString().Substring(device.Key.ToString().LastIndexOf("."));
						deviceComboBox.Items.Add(d2.Value.DeviceName + "(" + d2.Key + ")" + localIPLast);
						networkDeviceList.Add(d2.Value);
					}
				}
			}

			if (deviceComboBox.Items.Count > 0)
			{
				deviceComboBox.SelectedIndex = 0;
				deviceComboBox.Enabled = true;
				deviceConnectButton.Enabled = true;
				setNotice("已刷新设备列表，可选择并连接设备。", false, true);
			}
			else
			{
				setNotice("未找到可用的网络设备，请确认后重试。", false, true);
			}
			deviceRefreshButton.Enabled = true;
		}

		/// <summary>
		/// 事件：点击《设备连接》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceConnectButton_Click(object sender, EventArgs e)
		{
			// 如果已连接（按钮显示为“连接设备”)，则关闭连接
			if (mainForm.IsConnected)
			{
				disconnectDevice();
			}
			else
			{
				setNotice("正在连接设备，请稍候...",false,true);
				if (mainForm.Connect(networkDeviceList[deviceComboBox.SelectedIndex]))
				{										
					deviceComboBox.Enabled = false;
					deviceRefreshButton.Enabled = false;
					deviceConnectButton.Text = "断开连接";
					refreshRestartButton();
					setNotice("设备连接成功。", false, true);
				}
				else
				{
					setNotice("设备连接失败，请刷新设备列表后重试。", true, true);
				}
			}		
		}

		private void refreshRestartButton() {
			deviceRestartButton.Visible = mainForm.IsConnected;
			deviceRestartButton.Enabled = mainForm.IsConnected;
			Size = mainForm.IsConnected ? new Size(372, 173) : new Size(285,173);
		}

		/// <summary>
		/// 辅助方法：主动断开连接
		/// </summary>
		private void disconnectDevice() {

			setNotice("正在断开连接，请稍候...", false, true);
			mainForm.DisConnect();  // deviceConnectButton_Click() 主动断开连接
			deviceComboBox.Enabled = true;
			deviceRefreshButton.Enabled = true;
			deviceConnectButton.Text = "连接设备";
			refreshRestartButton();
			setNotice("已断开连接。", false, true);
		}

		/// <summary>
		/// 事件：点击《重启设备》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void restartButton_Click(object sender, EventArgs e)
		{
			if (mainForm.IsConnected)
			{
				setNotice("正在发送重启命令，请稍候片刻(约耗时5s)；重新搜索并连接设备。", true, true);

				mainForm.MyConnect.ResetDevice(); // 发送命令
				disconnectDevice();// 断开连接
				deviceRefreshButton_Click(null,null); //刷新设备
			}
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



		#endregion

		
	}	
}
