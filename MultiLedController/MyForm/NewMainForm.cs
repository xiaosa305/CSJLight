using MultiLedController.Ast;
using MultiLedController.Common;
using MultiLedController.entity;
using MultiLedController.utils.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MultiLedController.MyForm
{
	public partial class NewMainForm : Form
	{
		private bool isFirstTime = true; //只用一次的方法，避免每次激活都跑一次刷新
		private int netcardIndex = -1;
		private ManagementObject mo;
		
		private string mainIP;
		private string mainMask;
		private IList<string> ipList;

		private Dictionary<string, ControlDevice> ledControlDevices;		
		private int controllerSelectedIndex = -1;

		public NewMainForm()
		{
			InitializeComponent();
		}


		private void NewMainForm_Activated(object sender, EventArgs e)
		{
			if (isFirstTime)
			{
				refreshNetcardList();
				isFirstTime = false;
			}				
		}

		/// <summary>
		/// 事件：点击《刷新（网络列表）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshNetcardButton_Click(object sender, EventArgs e)
		{
			refreshNetcardList();
		}

		/// <summary>
		/// 事件：选中不同网卡(ipComboBox)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void netcardComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (netcardIndex != netcardComboBox.SelectedIndex) {
				Console.WriteLine("Here");
				netcardIndex = netcardComboBox.SelectedIndex;
				refreshNetcardInfo();
			}
		}

		/// <summary>
		/// 事件：点击《刷新当前网卡信息》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshCurButton_Click(object sender, EventArgs e)
		{
			refreshNetcardInfo();
		}

		/// <summary>
		/// 事件：点击《搜索设备》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void searchButton_Click(object sender, EventArgs e)
		{
			setNotice("开始搜索设备，请稍候");
			controllerListView.Items.Clear();

			Art_Net_Manager.GetInstance().SearchDevice(ipLabel2.Text);
			Thread.Sleep(1000);
			ledControlDevices = Art_Net_Manager.GetInstance().GetLedControlDevices();

			controllerListView.Items.Clear();
			if (ledControlDevices.Count == 0)
			{
				setNotice("未搜索到任何设备，请确认后重试。");
				return;
			}

			int tempIndex = 1;
			foreach (ControlDevice led in ledControlDevices.Values)
			{
				AddLedController(tempIndex++, led);
			}
			controllerListView.Items[0].Selected = true;
			setNotice("已将搜索到的设备添加设备列表中。");
		}

		/// <summary>
		/// 事件：更改《设备ListView》选中项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void controllerListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (controllerListView.SelectedIndices.Count > 0)
			{
				controllerSelectedIndex = controllerListView.SelectedIndices[0];
				startButton.Enabled = true;
			}
			else {
				//controllerSelectedIndex = -1;
				startButton.Enabled = false ;
			}
		}

		/// <summary>
		/// 事件：点击《启动》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void startButton_Click(object sender, EventArgs e)
		{
			setBusy(true);

			string mac = controllerListView.Items[controllerSelectedIndex].SubItems[2].Text ;			
			int interfaceCount = ledControlDevices[mac].Led_interface_num - 1;
			int addVIPCount = interfaceCount - ipList.Count;

			if (addVIPCount > 0) {
				setNotice("应用将自动为您添加相应路数数量的虚拟IP,请稍候...");				
				string firstIP; 
				if (ipList == null || ipList.Count == 0)
				{
					firstIP = mainIP;
				}
				else {
					firstIP = ipList[0];
				}
				string top3str = firstIP.Substring(0, firstIP.LastIndexOf('.')+1);			
				int lastStr =  int.Parse( firstIP.Substring(firstIP.LastIndexOf('.') + 1 ) ) ;

				IList<string> addIPList = new List<string>();
				Ping ping = new Ping();
				// 此处为第一层获取可用IP的方法；
				for (  ; lastStr < 255; lastStr++)
				{
					string addIP =  top3str + lastStr;
					setNotice("正在检测" + addIP + "是否可用，请稍候...");
					IPStatus ipStatus = ping.Send(addIP). Status;					
					if (ipStatus != IPStatus.Success)
					{						
						addIPList.Add(addIP);
						addVIPCount -- ;
					}

					if (addVIPCount <= 0) {
						break;
					}					
				}
				// 若以上循环走完后，仍未达到所需的VIP数量，则从2开始，再走一遍获取可用IP的方法；
				if (addVIPCount > 0) {
					for (lastStr =2 ; lastStr < 255; lastStr++)
					{
						string addIP = top3str + lastStr;
						setNotice("正在检测" + addIP + "是否可用，请稍候...");
						IPStatus ipStatus = ping.Send(addIP).Status;
						if (ipStatus != IPStatus.Success)
						{							
							addIPList.Add(addIP);
							addVIPCount--;
						}

						if (addVIPCount <= 0)
						{
							break;
						}
					}
				}
				// 若仍未完成，则必须提示用户无可用ip了
				if (addVIPCount > 0) {
					MessageBox.Show("检测到当前网段无足够可用的IP地址，无法继续操作。");
					setNotice("检测到当前网段无足够可用的IP地址，已中断操作。");	
					setBusy(false);
					return;
				}

				//以新IP及掩码列表， 改造IPAst；并设置到系统中
				foreach (string tempIP in addIPList)
				{
					ipList.Add(tempIP);					
				}

				List<string> newIPList = new List<string>();
				List<string> newMaskList = new List<string>();
				newIPList.Add(mainIP);
				newMaskList.Add(mainMask);	
								
				foreach (string tempIP in ipList)
				{
					newIPList.Add(tempIP);
					newMaskList.Add(mainMask);
				}				

				IPAst ipAst = new IPAst(mo)
				{
					IpArray = newIPList.ToArray(),
					SubmaskArray = newMaskList.ToArray()
				};

				IPHelper.SetIPAddress(mo, ipAst);
				refreshNetcardInfo();
			}

			//补充相应的虚拟IP后（可能原来已经足够了），利用interfaceCount数量（N）， 在右侧取前N个虚拟IP（N可能小于ipList.Count）
			for (int interfaceIndex = 0; interfaceIndex < interfaceCount; interfaceIndex++)
			{
				virtualIPListView.Items[interfaceIndex].SubItems[2].Text = (interfaceIndex+1).ToString();				
			}


					   			 
			setNotice("成功启动");
			setBusy(false);
		}

		/// <summary>
		/// 事件：点击《+》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void plusButton_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 事件：点击《-》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void minusButton_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 事件：点击《录制》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void recordButton_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 事件：点击《网络设置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkButton_Click(object sender, EventArgs e)
		{
			//new NetworkForm(this).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：把所有的组件都设为最初的空值
		/// </summary>
		private void clearAll()
		{
			mo = null;
			mainIP = null;
			mainMask = null;
			ipList = null;
			ledControlDevices = null;
			controllerSelectedIndex = -1;			
			netcardIndex = -1;

			netcardComboBox.Items.Clear();
			netcardComboBox.Text = "";			

			virtualIPListView.Items.Clear();

			searchButton.Enabled = false;
			startButton.Enabled = false;
			debugButton.Enabled = false;
			recordButton.Enabled = false;
		}

		/// <summary>
		/// 辅助方法：刷新网卡列表（但不再使用moList了，亦即只保存一个当前选中网卡的mo对象）
		/// </summary>
		private void refreshNetcardList()
		{
			clearAll();

			// 获取本地计算机所有网卡信息			
			ManagementObjectSearcher search = new ManagementObjectSearcher("SELECT * FROM Win32_NetWorkAdapterConfiguration");
						
			foreach (ManagementObject mo in search.Get())
			{
				if (mo["IPAddress"] != null)
				{
					string carName = mo["Description"].ToString().Trim();					
					netcardComboBox.Items.Add(carName);					
				}
			}

			if (netcardComboBox.Items.Count > 0) {
				netcardComboBox.SelectedIndex = 0;
				netcardIndex = 0;				
			}
		}
		
		/// <summary>
		/// 辅助方法：刷新网卡信息，主要用于更改网卡列表及主动刷新网卡时
		/// </summary>
		private void refreshNetcardInfo()
		{
			virtualIPListView.Items.Clear();
			controllerListView.Items.Clear();

			if (netcardIndex > -1)
			{
				mo = IPHelper.GetNetCardMO(netcardComboBox.Text);
				IPAst ipAst = new IPAst(mo);
				ipList = new List<string>();
				if (ipAst.IpArray.Length > 0)
				{
					mainIP = ipAst.IpArray[ipAst.IpArray.Length - 1];
					ipLabel2.Text = mainIP;
					mainMask = ipAst.SubmaskArray[ipAst.SubmaskArray.Length - 1];
					submaskLabel2.Text = mainMask;
					gatewayLabel2.Text = ipAst.GatewayArray[0];
					dnsLabel2.Text = ipAst.DnsArray[0];

					int tempIndex = 1;
					for (int i = ipAst.IpArray.Length - 2; i >= 0; i--)
					{
						ListViewItem item = new ListViewItem(new string[] {
							 tempIndex++ +"",
							 ipAst.IpArray[i],
							 ""
						});
						virtualIPListView.Items.Add(item);
						ipList.Add(ipAst.IpArray[i]);
					}
					searchButton.Enabled = true;
					searchButton_Click(null ,null);					
				}
				else {
					searchButton.Enabled = false;
				}				
			}
		}

		/// <summary>
		/// 辅助方法：将LedControllor添加到controllerListView中
		/// </summary>
		/// <param name="led"></param>
		private void AddLedController(int index,ControlDevice led)
		{
			this.controllerListView.Items.Add(
				new ListViewItem(new string[]{
					index.ToString(),
					led.LedName,
					led.Mac,
					(led.Led_interface_num-1).ToString()
				})				
			);
		}

		/// <summary>
		/// 辅助方法：设置提示信息
		/// </summary>
		/// <param name="msg"></param>
		private void setNotice(string msg) {
			myStatusLabel.Text = msg;
			Refresh();
		}

		/// <summary>
		/// 辅助方法：设置是否忙时
		/// </summary>
		/// <param name="v"></param>
		private void setBusy(bool busy)
		{
			this.Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
		}

		
	}
}
