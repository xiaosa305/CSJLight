using CCWin.SkinControl;
using LightController.Ast;
using LightController.Common;
using LightController.MyForm.MainFormAst;
using LightController.PeripheralDevice;
using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.MyForm
{
	public partial class HardwareUpdateForm : Form
	{
		private MainFormBase mainForm;
		private string xbinPath; 	 

		private BaseCommunication myConnect; // 保持着一个设备连接（串网口通用）
		private bool isConnected = false; //是否连接
		private bool isConnectCom = true; //是否串口连接
		private IList<NetworkDeviceInfo> networkDeviceList;  // 网络设备的列表		

		public HardwareUpdateForm(MainFormBase mainForm ) 
		{
			InitializeComponent();
			this.mainForm = mainForm;		
			
			xbinPath = Properties.Settings.Default.xbinPath;			
			if (File.Exists(xbinPath))
			{
				pathLabel.Text = xbinPath;
			}
			else {
				xbinPath = null;
				Properties.Settings.Default.xbinPath = xbinPath;
				Properties.Settings.Default.Save();
			}
		}

		private void UpdateForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			LanguageHelper.InitForm(this);
			
			// 设false可在其他文件中修改本类的UI
			Control.CheckForIllegalCrossThreadCalls = false;

			//主动刷新(串口)列表
			refreshDeviceComboBox();			
		}

		/// <summary>
		/// 事件：《窗口关闭》：若已打开串口设备，则需要断开连接
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HardwareUpdateForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if ( isConnected)
			{
				myConnect.DisConnect();
				myConnect = null;
			}
			Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：点击《右上角？》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HardwareUpdateForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show(
				LanguageHelper.TranslateSentence("此升级方式，是只适用于硬件出现重大问题时的解决方案，请谨慎使用！"),
				LanguageHelper.TranslateSentence( "警告！"),
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning);
			e.Cancel = true;
		}
		
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
				updateButton.Enabled = isConnected && !string.IsNullOrEmpty(xbinPath);
			}
		}
					   
		/// <summary>
		/// 事件：点击《更换连接方式》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void switchButton_Click(object sender, EventArgs e)
		{
			isConnectCom = !isConnectCom;
			switchButton.Text = isConnectCom ? "以网络连接" : "以串口连接";
			refreshButton.Text = isConnectCom ? "刷新串口" : "刷新网络";
			deviceConnectButton.Text = isConnectCom ? "打开串口" : "连接设备";
			refreshDeviceComboBox(); // switchButton_Click
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
						Thread.Sleep(ConnectForm.SEARCH_WAITTIME);
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
				SetNotice("已刷新"+ (isConnectCom?"串口":"网络设备")+"列表。", false,true);
			}
			else
			{
				SetNotice("未找到可用设备，请检查设备连接后重试。", false, true);
			}
			refreshConnectButtons();
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
				SetNotice("已" + (isConnectCom ? "关闭串口" : "断开连接"), true, true);
			}
		}

		/// <summary>
		/// 辅助方法：刷新按键[可用性]及[显示的文字]
		/// </summary>
		private void refreshConnectButtons()
		{
			switchButton.Enabled = !isConnected;
			deviceComboBox.Enabled = !isConnected && deviceComboBox.Items.Count > 0;
			refreshButton.Enabled = !isConnected;
			deviceConnectButton.Enabled = deviceComboBox.Items.Count > 0;
			versionButton.Enabled = isConnected;
			updateButton.Enabled = isConnected && !string.IsNullOrEmpty(xbinPath);

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
		/// 事件：点击《打开串口|关闭串口 | 连接设备|断开连接》
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
					if ((myConnect as SerialConnect).OpenSerialPort(deviceComboBox.Text))
					{
						isConnected = true;
						refreshConnectButtons();
						SetNotice("已打开串口。", true, true);
					}
				}
				catch (Exception ex)
				{
					SetNotice("打开串口失败，原因是：\n" + ex.Message, true,false);
				}
			}
			else
			{
				NetworkDeviceInfo selectedNetworkDevice = networkDeviceList[deviceComboBox.SelectedIndex];
				string deviceName = selectedNetworkDevice.DeviceName;
				myConnect = new NetworkConnect();
				if (myConnect.Connect(selectedNetworkDevice))
				{
					isConnected = true;
					refreshConnectButtons();
					SetNotice("成功连接网络设备。", true, true);
				}
				else
				{
					SetNotice("连接网络设备失败。", true, true);
				}
			}
		}

		/// <summary>
		/// 事件：点击《获取当前版本》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void versionButton_Click(object sender, EventArgs e)
		{
			if (myConnect == null || !isConnected)
			{
				SetNotice("尚未连接设备，请连接后重试。", true, true);
				return;
			}

			//DOTO：《硬件升级》获取版本信息

		}

		/// <summary>
		/// 事件：点击《升级》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateButton_Click(object sender, EventArgs e)
		{
			if ( string.IsNullOrEmpty(xbinPath) )
			{
				SetNotice("尚未选择xbin文件，请在选择后重试。",true, true);
				return;
			}

			if (myConnect == null || !isConnected) {
				SetNotice("尚未连接设备，请连接后重试。", true, true);
				return;
			}

			SetBusy(true);			
			myConnect.UpdateDeviceSystem(xbinPath, UpdateCompleted, UpdateError, DrawProgress);		
		}
			
		/// <summary>
		/// 辅助回调方法：硬件升级成功
		/// </summary>
		/// <param name="obj"></param>
		public void UpdateCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				SetNotice("硬件升级成功，设备将自动重启，请稍等片刻。" , true, true);
				Thread.Sleep(5000);
				myProgressBar.Value = 0;
				progressStatusLabel.Text = "";

				if(isConnectCom)
				{
					SetNotice("请继续操作(如出现错误，可先关闭再打开串口后重试)。", true, true);
				}else{
					myConnect.DisConnect();
					myConnect = null;
					isConnected = false;
					disableDeviceComboBox();
					refreshConnectButtons();
					SetNotice("请刷新网络，并重新连接设备(如未找到设备，请稍等片刻后重试)。", true, true);
				}				
				SetBusy(false);
			});
		}

		/// <summary>
		/// 辅助回调方法：硬件升级失败
		/// </summary>
		/// <param name="obj"></param>
		public void UpdateError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				SetNotice("硬件升级失败[" + msg + "]", true,false);
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
			SetNotice( "正在升级硬件，请稍候..." , false, true);			
			myProgressBar.Value = progressPercent;
			progressStatusLabel.Text = progressPercent + "%";
			statusStrip1.Refresh();
		}
		
		/// <summary>
		/// 辅助方法：禁用设备列表下拉框,并清空其数据
		/// </summary>
		private void disableDeviceComboBox()
		{
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
		public void SetNotice(string msg, bool messageBoxShow,bool isTranslate)
		{
			if (isTranslate) {
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
		/// 辅助方法：设定忙时
		/// </summary>
		/// <param name="busy"></param>
		public void SetBusy(bool busy)
		{
			Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
			Enabled = !busy;
		}

		/// <summary>
		/// 通用方法：部分按键文本更新时，进行翻译
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someButtton_TextChanged(object sender, EventArgs e)
		{
			LanguageHelper.TranslateControl(sender as Button);
		}

		#endregion
	}
}
