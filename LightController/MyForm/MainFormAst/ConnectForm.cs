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
		private MainFormBase mainForm;
		private IList<NetworkDeviceInfo> networkDeviceList; //记录所有的device列表(包括连接的本地IP和设备信息，故如有多个同网段IP，则同一个设备可能有多个列表值)		

		public ConnectForm(MainFormBase mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;
		}

		private void ConnectForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
		}

		/// <summary>
		/// 事件：点击《刷新列表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceRefreshButton_Click(object sender, EventArgs e)
		{
			deviceRefresh();
		}

		/// <summary>
		/// 辅助方法：刷新设备
		/// </summary>
		protected void deviceRefresh()
		{
			deviceRefreshButton.Enabled = false;
			deviceConnectButton.Enabled = false;

			//	 刷新前，先清空按键等
			setNotice("正在搜索设备，请稍候...", false, true);
			deviceComboBox.Items.Clear();
			deviceComboBox.Text = "";
			deviceComboBox.SelectedIndex = -1;
			deviceComboBox.Enabled = false;			

			// 先获取本地ip列表，遍历使用这些ip，搜索设备;-->都搜索完毕再统一显示
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
			if ( mainForm.IsConnected)
			{
				//disConnect(); //connectButtonClick
			}
			else
			{
				// 从源头把问题处理掉，不要再在此处判断
				//if ( deviceComboBox.SelectedIndex < 0)
				//{
				//	setNotice("未选中可用设备，请选中后再点击连接。", true, true);
				//	return;
				//}

				mainForm.MyConnect = new NetworkConnect();
				if (mainForm.MyConnect.Connect(networkDeviceList[deviceComboBox.SelectedIndex ]))
				{
					mainForm.StartDebug();
					setNotice("设备连接成功。", false, true);
				}
				else
				{
					setNotice("设备连接失败，请刷新设备列表后重试。", true, true);
				}
			}
		}

	

		///// <summary>
		/////  辅助方法：点击《切换连接方式》
		///// </summary>
		//protected void changeConnectMethodButtonClick()
		//{
		//	SetNotice("正在切换连接模式,请稍候...", false, true);
		//	isConnectCom = !isConnectCom;
		//	refreshConnectMethod();
		//	SetNotice("成功切换为" + (isConnectCom ? "串口连接" : "网络连接"), false, true);

		//	//保存此连接方式到Settings中
		//	Properties.Settings.Default.IsConnectCom = isConnectCom;
		//	Properties.Settings.Default.Save();

		//	deviceRefresh();  //changeConnectMethodButton_Click : 切换连接后，手动帮用户搜索相应的设备列表。
		//}

		///// <summary>
		///// 辅助方法：点击《连接设备 | 断开连接》
		///// </summary>
		//protected void connectButtonClick(string deviceName, int deviceSelectedIndex)
		//{
		//	// 如果已连接（按钮显示为“连接设备”)，则关闭连接
		//	if (IsConnected)
		//	{
		//		disConnect(); //connectButtonClick
		//	}
		//	else
		//	{
		//		playTools = PlayTools.GetInstance();
		//		if (isConnectCom)
		//		{
		//			if (string.IsNullOrEmpty(deviceName))
		//			{
		//				SetNotice("未选中可用串口，请选中后再点击连接。", true, true);
		//				return;
		//			}
		//			if (playTools.ConnectDevice(deviceName))
		//			{
		//				SetNotice("设备(以串口方式)连接成功,并进入调试模式。", false, true);
		//				EnableConnectedButtons(true, false);
		//			}
		//			else
		//			{
		//				SetNotice("设备连接失败，请刷新串口列表后重试。", true, true);
		//			}
		//		}
		//		else
		//		{
		//			if (deviceSelectedIndex < 0)
		//			{
		//				SetNotice("未选中可用网络连接，请选中后再点击连接。", true, true);
		//				return;
		//			}

		//			MyConnect = new NetworkConnect();
		//			if (MyConnect.Connect(networkDeviceList[deviceSelectedIndex]))
		//			{
		//				playTools.StartInternetPreview(MyConnect, ConnectCompleted, ConnectAndDisconnectError, eachStepTime);
		//				SetNotice("设备(以网络方式)连接成功,并进入调试模式。", false, true);
		//			}
		//			else
		//			{
		//				SetNotice("设备连接失败，请刷新网络设备列表后重试。", true, true);
		//			}
		//		}
		//	}
		//}

		///// <summary>
		///// 辅助方法：断开连接
		///// </summary>
		//protected void disConnect()
		//{

		//	if (IsConnected)
		//	{
		//		playTools.ResetDebugDataToEmpty();
		//		playTools.StopSend();
		//		if (isConnectCom)
		//		{
		//			playTools.CloseDevice();
		//			//MARK0413 mainForm.disConnect()内忘了调用DisConnect()
		//			MyConnect.DisConnect();
		//			EnableConnectedButtons(false, false);
		//		}
		//		else
		//		{
		//			playTools.StopInternetPreview(DisconnectCompleted, ConnectAndDisconnectError);
		//		}
		//		SetNotice("已断开连接。", false, true);
		//	}
		//}


		/// <summary>
		/// 辅助方法：刷新几个按键的文本
		/// </summary>
		//protected override void refreshConnectMethod()
		//{
		//	//changeConnectMethodSkinButton.Text = isConnectCom ? "以网络连接" : "以串口连接";
		//	//deviceRefreshSkinButton.Text = isConnectCom ? "刷新串口" : "刷新网络";
		//}


		///// <summary>
		/////  辅助方法：点击《切换连接方式》
		///// </summary>
		//protected void changeConnectMethodButtonClick()
		//{
		//	SetNotice("正在切换连接模式,请稍候...", false, true);
		//	isConnectCom = !isConnectCom;
		//	refreshConnectMethod();
		//	SetNotice("成功切换为" + (isConnectCom ? "串口连接" : "网络连接"), false, true);

		//	//保存此连接方式到Settings中
		//	Properties.Settings.Default.IsConnectCom = isConnectCom;
		//	Properties.Settings.Default.Save();

		//	deviceRefresh();  //changeConnectMethodButton_Click : 切换连接后，手动帮用户搜索相应的设备列表。
		//}

		/// <summary>
		/// 辅助方法：点击《连接设备 | 断开连接》
		/// </summary>
		//protected void connectButtonClick(string deviceName, int deviceSelectedIndex)
		//{
		//	// 如果已连接（按钮显示为“连接设备”)，则关闭连接
		//	if (IsConnected)
		//	{
		//		disConnect(); //connectButtonClick
		//	}
		//	else
		//	{
		//		playTools = PlayTools.GetInstance();
		//		if (isConnectCom)
		//		{
		//			if (string.IsNullOrEmpty(deviceName))
		//			{
		//				SetNotice("未选中可用串口，请选中后再点击连接。", true, true);
		//				return;
		//			}
		//			if (playTools.ConnectDevice(deviceName))
		//			{
		//				SetNotice("设备(以串口方式)连接成功,并进入调试模式。", false, true);
		//				EnableConnectedButtons(true, false);
		//			}
		//			else
		//			{
		//				SetNotice("设备连接失败，请刷新串口列表后重试。", true, true);
		//			}
		//		}
		//		else
		//		{
		//			if (deviceSelectedIndex < 0)
		//			{
		//				SetNotice("未选中可用网络连接，请选中后再点击连接。", true, true);
		//				return;
		//			}

		//			MyConnect = new NetworkConnect();
		//			if (MyConnect.Connect(networkDeviceList[deviceSelectedIndex]))
		//			{
		//				playTools.StartInternetPreview(MyConnect, ConnectCompleted, ConnectAndDisconnectError, eachStepTime);
		//				SetNotice("设备(以网络方式)连接成功,并进入调试模式。", false, true);
		//			}
		//			else
		//			{
		//				SetNotice("设备连接失败，请刷新网络设备列表后重试。", true, true);
		//			}
		//		}
		//	}
		//}

		/// <summary>
		/// 辅助方法：断开连接
		/// </summary>
		//protected void disConnect()
		//{
		//		if (IsConnected)
		//		{
		//			playTools.ResetDebugDataToEmpty();
		//			playTools.StopSend();
		//		if (isConnectCom)
		//		{
		//			playTools.CloseDevice();
		//			//MARK0413 mainForm.disConnect()内忘了调用DisConnect()
		//			MyConnect.DisConnect();
		//			EnableConnectedButtons(false, false);
		//		}
		//		else
		//		{
		//			playTools.StopInternetPreview(DisconnectCompleted, ConnectAndDisconnectError);
		//		}
		//		SetNotice("已断开连接。", false, true);
		//	}
		//}

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
	}
}
