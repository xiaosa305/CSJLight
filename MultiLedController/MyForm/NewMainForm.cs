﻿using MultiLedController.Ast;
using MultiLedController.Common;
using MultiLedController.entity;
using MultiLedController.utils.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MultiLedController.MyForm
{
	public partial class NewMainForm : Form
	{
		private bool isFirstTime = true; //只用一次的方法，避免每次激活都跑一次刷新	
		private ManagementObject mo; //存放当前网卡的mo对象
		
		private string mainIP;  //当前网卡的主IP(第一个设的IP，mo对象取回来时在最后面)
		private string mainMask; // 当前网卡的主掩码
		private IList<string> vipList; //当前网卡的虚拟IP列表（不包含主ip）

		private Dictionary<string, ControlDevice> ledControlDevices; //搜索到的设备 字典（key为mac）		

		private bool isStart = false; //是否启动模拟
		private bool isDebuging = false;  //是否启用调试
		private bool isRecording = false; // 是否正在录制
		private string recordPath = "C:\\Temp\\MultiLedFile"; //录制文件存储路径
		private int recordIndex = 0; //录制文件序号

		private bool networkChanged = false; //是否由《NewNetworkForm》更改网络设置：点过《多ip设置》《DHCP》《恢复设置》这三个按钮后需要设为true

		public NewMainForm()
		{
			InitializeComponent();

			// 为软件添加版本号显示
			FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
			string appFileVersion = string.Format("{0}.{1}.{2}.{3}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);
			Text += " v" + appFileVersion ;

			//MARK：添加这一句，会去掉其他线程使用本UI控件时弹出异常的问题(权宜之计，并非长久方案)。
			CheckForIllegalCrossThreadCalls = false;
		}

		private void NewMainForm_Load(object sender, EventArgs e)
		{
			setRecordPathLabel();
			recordTextBox.Text = transformRecordIndex(recordIndex);
			//为recordTextBox添加失去焦点的监听事件（没有在VS中）
			recordTextBox.LostFocus += new EventHandler(recordTextBox_LostFocus); 
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
		/// 事件：点击《网络设置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkButton_Click(object sender, EventArgs e)
		{
			new NewNetworkForm(this).ShowDialog();

			if (networkChanged) {
				// 网络设置后，应该提醒用户刷新
				DialogResult dr = MessageBox.Show("检查到您已更改了网络设置，请刷新当前网卡信息，否则接下来的操作可能会报错。是否立即刷新？",
					"刷新当前网卡信息？",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Warning
				);
				if (dr == DialogResult.Yes)
				{
					refreshNetcardinfoButton_Click(null, null);
				}
				//不论是否刷新，都将这个值设为false；表示不再计较了...
				networkChanged = false;
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
			refreshNetcardInfo();	
		}

		/// <summary>
		/// 事件：点击《启用DHCP》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dhcpButton_Click(object sender, EventArgs e)
		{
			if (netcardComboBox.SelectedIndex == -1)
			{
				setNotice(1,"未选中可用网卡，请刷新后重试");
				return;
			}
			mo.InvokeMethod("SetDNSServerSearchOrder", null);
			mo.InvokeMethod("EnableDHCP", null);
			setNotice(1, "正在启用DHCP，请稍候...");
			Refresh();

			Thread.Sleep(1000);			
			refreshNetcardInfo();
		}

		/// <summary>
		/// 事件：点击《清空虚拟IP》（作用和启用DHCP相似，但主要是为了在无DHCP环境下，主动只保留当前主IP的设定）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void clearVIPButton_Click(object sender, EventArgs e)
		{
			if (netcardComboBox.SelectedIndex == -1)
			{
				setNotice(1, "未选中可用网卡，请刷新后重试");
				return;
			}

			setBusy(true);

			if (vipList != null && vipList.Count > 0)
			{
				IPAst ipAst = new IPAst(mo)
				{
					IpArray = new string[] { mainIP },
					SubmaskArray = new string[] { mainMask },
				};

				setNotice(1, "正在为您清空虚拟IP,清空后将刷新当前网卡信息，请稍候...");
				IPHelper.SetIPAddress(mo, ipAst);
				refreshNetcardInfo(); // 无论设置成功与否，都主动刷新网卡信息			
			}
			else {
				setNotice(1, "检测到当前网卡并无虚拟IP，无需清空。");
			}	

			setBusy(false);
		}

		/// <summary>
		/// 事件：点击《刷新当前网卡信息》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshNetcardinfoButton_Click(object sender, EventArgs e)
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
			searchDevices();
		}

		/// <summary>
		/// 事件：更改《设备ListView》选中项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void controllerListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			startButton.Enabled = controllerListView.SelectedIndices.Count > 0;			
		}

		/// <summary>
		/// 事件：点击《启动|关闭模拟》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void startButton_Click(object sender, EventArgs e)
		{
			//避免更改路数之后，虚拟IP显示错误，先清空所有的《关联路数》Text ( 不论是启动还是关闭模拟，都先清空关联 )
			foreach (ListViewItem item in virtualIPListView.Items)
			{
				item.SubItems[2].Text = "";
			}
			Refresh();

			if (!isStart)
			{
				setBusy(true);
                                
                if (controllerListView.SelectedIndices.Count == 0) {
                    MessageBox.Show("尚未选中设备，无法启动模拟。");
                    setNotice(1, "尚未选中设备，无法启动模拟。");
                    setBusy(false);
                    return;                    
                }

                int controllerSelectedIndex = controllerListView.SelectedIndices[0];
				string mac = controllerListView.Items[controllerSelectedIndex].SubItems[2].Text;
				int interfaceCount = ledControlDevices[mac].Led_interface_num - 1;
				int addVIPCount = interfaceCount - vipList.Count;		

				if (addVIPCount > 0)
				{
					setNotice(1,"即将为您添加相应路数数量的虚拟IP,请稍候...");
					string firstIP;
					if (vipList == null || vipList.Count == 0)
					{
						firstIP = mainIP;
					}
					else
					{
						firstIP = vipList[0];
					}
					string top3str = firstIP.Substring(0, firstIP.LastIndexOf('.') + 1);
					int lastStr = int.Parse(firstIP.Substring(firstIP.LastIndexOf('.') + 1));

                    IList<string> addIPList = new List<string>();
                    //IList<string> addIPList = getAvailableIPList(new List<string>(), addVIPCount, top3str, lastStr+1);
                    //Console.WriteLine(addIPList);

                    //此处为第一层获取可用IP的方法；
                    for (; lastStr < 255; lastStr++)
                    {
                        string addIP = top3str + lastStr;
                        setNotice(1, "正在检测" + addIP + "是否可用，请稍候...");

                        //若IP未被占用，则可以添加到addIPList中
                        if (IPHelper.CheckIPAvailable(mainIP, addIP))
                        {
                            addIPList.Add(addIP);
                            addVIPCount--;
                        }

                        if (addVIPCount <= 0)
                        {
                            break;
                        }
                    }
                    //若以上循环走完后，仍未达到所需的VIP数量，则从2开始，再走一遍获取可用IP的方法；
                    if (addVIPCount > 0)
                    {
                        for (lastStr = 2; lastStr < 255; lastStr++)
                        {
                            string addIP = top3str + lastStr;
                            setNotice(1, "正在检测" + addIP + "是否可用，请稍候...");

                            //若IP未被占用，则可以添加到addIPList中
                            if (IPHelper.CheckIPAvailable(mainIP, addIP))
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

                    //若仍未完成，则必须提示用户无可用ip并中断操作
                    if (addVIPCount > 0)
                    {
                        MessageBox.Show("检测到当前网段无足够可用的IP地址，无法继续操作。");
                        setNotice(1, "检测到当前网段无足够可用的IP地址，已中断操作。");
                        setBusy(false);
                        return;
                    }

                    //以新IP及掩码列表， 改造IPAst；并将（mainIP及新的ipList）设置到系统中
                    foreach (string tempIP in addIPList)
					{
						vipList.Add(tempIP);
					}

					List<string> newIPList = new List<string>();
					List<string> newMaskList = new List<string>();
					newIPList.Add(mainIP);
					newMaskList.Add(mainMask);

					foreach (string tempIP in vipList)
					{
						newIPList.Add(tempIP);
						newMaskList.Add(mainMask);
					}

					IPAst ipAst = new IPAst(mo)
					{
						IpArray = newIPList.ToArray(),
						SubmaskArray = newMaskList.ToArray()
					};

					setNotice(1,"正在为您设置虚拟IP，请稍候...");

					if (IPHelper.SetIPAddress(mo, ipAst))
					{
						refreshVirtualIPListView(newIPList);
					}
					else {
						MessageBox.Show("虚拟IP设置失败，已恢复初始设置。");
						setNotice(1, "启动模拟失败(虚拟IP设置失败，已恢复初始设置)。");
						setBusy(false);
						return;
					}					
				}

				ControlDevice device = ledControlDevices.Values.ElementAt(controllerSelectedIndex);
				List<VirtualControlInfo> virtuals = new List<VirtualControlInfo>();
                // 补充相应的虚拟IP后（可能原来已经足够了），利用interfaceCount数量（N）， 
                // 在右侧取前N个虚拟IP（N可能小于ipList.Count）；并填充virtuals列表
				for (int interfaceIndex = 0; interfaceIndex < interfaceCount; interfaceIndex++)
				{
					virtualIPListView.Items[interfaceIndex].SubItems[2].Text = (interfaceIndex + 1).ToString();
					virtuals.Add(new VirtualControlInfo(virtualIPListView.Items[interfaceIndex].SubItems[1].Text, device));
				}

				setNotice(1,"正在关联虚拟IP与设备，请稍候...");

				try
				{
					Art_Net_Manager.GetInstance().Start(virtuals, mainIP, mainIP, device);
				}
				catch (Exception ex) {
					MessageBox.Show("启动模拟失败。\n" +
						"原因（异常）是：" + ex.Message+"。\n" +
						"若因IP配置失败，可《启用DHCP》或《清空虚拟IP》后重试。");
					setNotice(1, "启动模拟失败。");
					setBusy(false);
					return;
				}

				enableStartButtons(true);			
				setNotice(1,"已启动模拟。");
				setBusy(false);
			}
			else {
				setBusy(true);
				setNotice(1,"正在关闭模拟，请稍候...");

				if (isDebuging)
				{
					debugButton_Click(null, null);
				}
				Art_Net_Manager.GetInstance().Close();
				enableStartButtons(false);				
				setNotice(1,"已关闭模拟。");
				setBusy(false);
			}			
		}

		/// <summary>
		/// 事件：点击《开始调试|停止调试》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void debugButton_Click(object sender, EventArgs e)
		{
			isDebuging = !isDebuging;
			debugButton.Text = isDebuging ? "停止调试" : "开始调试";
			if (isDebuging)
			{
				Art_Net_Manager.GetInstance().StartDebug(showDebugFrame);
			}
			else
			{
				Art_Net_Manager.GetInstance().StopDebug();
			}
		}

		#region 录制文件相关

		/// <summary>
		/// 事件：点击《选择存放目录》并更改存储路径
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setFilePathButton_Click(object sender, EventArgs e)
		{
			DialogResult dr = recordFolderBrowserDialog.ShowDialog();
			if (dr == DialogResult.OK)
			{
				recordPath = recordFolderBrowserDialog.SelectedPath;
				setRecordPathLabel();
				setNotice(2,"已设置存放目录为：" + recordPath);
			}
		}

		/// <summary>
		/// 事件：《recordTextBox》，只能输入0-9及退格键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void recordTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8)
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}

		/// <summary>
		/// 事件：《recordTextBox》失去焦点，把文字做相关的转换
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void recordTextBox_LostFocus(object sender, EventArgs e)
		{
			if (recordTextBox.Text.Length == 0)
			{
				recordIndex = 0;
			}
			else {
				recordIndex = int.Parse(recordTextBox.Text);
			}			
			recordTextBox.Text = transformRecordIndex(recordIndex);
		}

		/// <summary>
		/// 事件：点击《+》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void plusButton_Click(object sender, EventArgs e)
		{
			if (recordIndex >= 999 ) {
				setNotice(2,"录制文件序号不得大于999。");
				return;
			}
			recordTextBox.Text = transformRecordIndex(++recordIndex);
			setNotice(2,"已设置录制文件名为：SC"+ recordTextBox.Text + ".bin");
		}

		/// <summary>
		/// 事件：点击《-》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void minusButton_Click(object sender, EventArgs e)
		{			
			if (recordIndex <= 0)
			{
				setNotice(2,"录制文件序号不得小于000。");
				return;
			}
			recordTextBox.Text = transformRecordIndex(--recordIndex);
			setNotice(2,"已设置录制文件名为：SC" + recordTextBox.Text + ".bin");
		}

		/// <summary>
		/// 事件：点击《录制》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void recordButton_Click(object sender, EventArgs e)
		{
			if (isRecording)
			{				
				Art_Net_Manager.GetInstance().StopSaveToFile();
				enableRecordButtons(false);
				plusButton_Click(null, null);
				setNotice(2,"已停止录制,并把录制序号加1。");
				recordButton.Text = "录制数据";				
			}
			else
			{
				setNotice(2,"正在录制文件...");				
				string recordFilePath = recordPath + @"\SC" + recordTextBox.Text + ".bin";
				Art_Net_Manager.GetInstance().SetSaveFilePath(recordFilePath);
				Art_Net_Manager.GetInstance().StartSaveToFile(showRecordFrame);
				enableRecordButtons(true);
				recordButton.Text = "停止录制";
			}
		}

		/// <summary>
		/// 辅助方法：设置录制相关控件是否可用
		/// </summary>
		/// <param name="recording"></param>
		private void enableRecordButtons(bool recording)
		{
			isRecording = recording;
			setFilePathButton.Enabled = !recording;
			recordTextBox.Enabled = !recording;
			plusButton.Enabled = !recording;
			minusButton.Enabled = !recording;
		}

		#endregion

		/// <summary>
		/// 辅助方法：把所有的组件都设为最初的空值;按钮Enabled还原为false
		/// </summary>
		private void clearAll()
		{
			clearNetcardInfo();

			netcardInfoGroupBox.Enabled = false;
						
			netcardComboBox.SelectedIndex = -1;
			netcardComboBox.Items.Clear();			
			netcardComboBox.Text = "";
		}

		/// <summary>
		/// 只清空当前网卡及相关的显示（原则：先清内存后清界面）
		/// </summary>
		private void clearNetcardInfo() {

			mo = null;

			mainIP = null;
			mainMask = null;
			ipLabel2.Text = "";
			submaskLabel2.Text = "";
			gatewayLabel2.Text = "";
			dnsLabel2.Text = "";

			vipList = null;
			virtualIPListView.Items.Clear();		

			ledControlDevices = null;
			controllerListView.Items.Clear();

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
			setNotice(1, "正在刷新网卡列表...");
			clearAll();			

			// 获取本地计算机所有网卡信息			
			ManagementObjectSearcher search = new ManagementObjectSearcher("SELECT * FROM Win32_NetWorkAdapterConfiguration");						
			foreach (ManagementObject mo in search.Get())
			{
				if (mo["IPAddress"] != null)
				{
					string netcardName = mo["Description"].ToString().Trim();					
					netcardComboBox.Items.Add(netcardName);
				}
			}

			if (netcardComboBox.Items.Count > 0)
			{
				netcardComboBox.SelectedIndex = 0;
				netcardInfoGroupBox.Enabled = true;
			}
			else {
				setNotice(1, "未找到可用网卡,请处理后《刷新列表》。");
			}
		}
		
		/// <summary>
		/// 辅助方法：刷新网卡信息，主要用于更改网卡列表及主动刷新网卡时
		/// </summary>
		private void refreshNetcardInfo()
		{			
			clearNetcardInfo();
			vipList = new List<string>();	

			if (netcardComboBox.SelectedIndex > -1)
			{
				mo = IPHelper.GetNetCardMO(netcardComboBox.Text);
				if(mo == null) {
					setNotice(1, "当前网卡不可用,请处理后《刷新当前网卡信息》。");
					return;
				}
							
				IPAst ipAst = new IPAst(mo);				
				if (ipAst.IpArray.Length > 0)
				{
					mainIP = ipAst.IpArray[ipAst.IpArray.Length - 1];
					mainMask = ipAst.SubmaskArray[ipAst.SubmaskArray.Length - 1];

					ipLabel2.Text = mainIP;					
					submaskLabel2.Text = mainMask;
					if (ipAst.GatewayArray != null && ipAst.GatewayArray.Length > 0) {
						gatewayLabel2.Text = ipAst.GatewayArray[0];
					}
					if (ipAst.DnsArray != null && ipAst.DnsArray.Length > 0)
					{
						dnsLabel2.Text = ipAst.DnsArray[0];
					}
					searchButton.Enabled = true;

					//刷新当前网卡信息后，都主动搜索设备
					setNotice(1, "已刷新当前网卡信息，即将自动为您搜索设备...");			
					Refresh();
					Thread.Sleep(1000);
					searchDevices();

					int tempIndex = 1;
					for (int i = ipAst.IpArray.Length - 2; i >= 0; i--)
					{
						ListViewItem item = new ListViewItem(new string[] {
							 tempIndex++ +"",
							 ipAst.IpArray[i],
							 ""
						});
						virtualIPListView.Items.Add(item);
						vipList.Add(ipAst.IpArray[i]);
					}					
				}					
			}
		}

		/// <summary>
		/// 辅助方法：通过ipList主动刷新右侧列表
		/// </summary>
		private void refreshVirtualIPListView(IList<string> newIPList)
		{
			virtualIPListView.Items.Clear();
			
			for (int tempIndex = 1; tempIndex< newIPList.Count; tempIndex++)
			{
				ListViewItem item = new ListViewItem(new string[] {
					 tempIndex +"",
					 newIPList[tempIndex],
					 ""
				});
				virtualIPListView.Items.Add(item);
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
		///辅助方法：搜索设备
		/// </summary>
		private void searchDevices()
		{
			Console.WriteLine("searchDevices...");

			if (netcardComboBox.SelectedIndex == -1  ) {				
				setNotice( 1, "未选中可用网卡，无法搜索设备");
				return;
			}
			if (string.IsNullOrEmpty(mainIP)) {
				setNotice(1, "未设置主IP地址，无法搜索设备");
				return;
			}
			if ( ! searchButton.Enabled ) {
				setNotice(1, "搜索按钮不可用，不可搜索设备！");
				return;
			}

			setNotice(1,"正在搜索设备，请稍候...");
			
			controllerListView.Items.Clear();
			startButton.Enabled = false;

			Art_Net_Manager.GetInstance().SearchDevice(ipLabel2.Text);
			Thread.Sleep(1000);
			ledControlDevices = Art_Net_Manager.GetInstance().GetLedControlDevices();

			controllerListView.Items.Clear();
			if (ledControlDevices.Count == 0)
			{
				setNotice(1,"未搜索到任何设备，请确认后重试。");
				return;
			}

			int tempIndex = 1;
			foreach (ControlDevice led in ledControlDevices.Values)
			{
				AddLedController(tempIndex++, led);
			}
			controllerListView.Items[0].Selected = true;

			setNotice(1,"已将搜索到的设备添加列表中,并选中了第一个设备。");			
		}

		/// <summary>
		/// 辅助方法：处理int型,使之成为两位数的string表示
		/// </summary>
		/// <param name="recordIndex"></param>
		/// <returns></returns>
		private string transformRecordIndex(int recordIndex)
		{
			if (recordIndex < 0) {
				return "000";
			}
			if (recordIndex > 999) {
				return "999";
			}

			if (recordIndex < 100)
			{
				if (recordIndex < 10) {
					return "00" + recordIndex;
				}
				return "0" + recordIndex;
			}
			else
			{
				return recordIndex.ToString();
			}
		}		

		/// <summary>
		/// 辅助方法：根据当前的recordPath，设置label及toolTip
		/// </summary>
		private void setRecordPathLabel()
		{
			recordPathLabel.Text = recordPath;
			myToolTip.SetToolTip(recordPathLabel, recordPath);
		}

		/// <summary>
		/// 辅助方法：根据入参bool，设置当前《启动》相关的按键的可用性
		/// </summary>
		/// <param name="enable">true为按键可用</param>
		private void enableStartButtons(bool enable)
		{
			topPanel.Enabled = !enable;
			startButton.Text = enable ? "关闭模拟" : "启动模拟";
			debugButton.Enabled = enable;
			recordButton.Enabled = enable;
			isStart = enable;
		}

		/// <summary>
		/// 辅助方法：设置提示信息
		/// </summary>
		/// <param name="place">1 | 2 左1右2</param>
		/// <param name="msg"></param>
		private void setNotice(int place, string msg)
		{
			if (place == 1)
			{
				myStatusLabel1.Text = msg;				
			}
			if (place == 2)
			{
				myStatusLabel2.Text = msg;
			}
			statusStrip.Refresh();
		}

		/// <summary>
		/// 辅助方法：设置是否忙时
		/// </summary>
		/// <param name="v"></param>
		private void setBusy(bool busy)
		{
			this.Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
		}

		#region 几个由外部调用本Form控件的方法：包括委托及其他方法

		/// <summary>
		/// 辅助方法：实现展示调试帧数的委托
		/// </summary>
		/// <param name="count"></param>
		private void showDebugFrame(long count)
		{
			setNotice(1,"当前调试帧数：" + count);
		}

		/// <summary>
		/// 辅助方法：实现展示录制帧数的委托
		/// </summary>
		/// <param name="count"></param>
		private void showRecordFrame(long count)
		{
			setNotice(2,"当前录制帧数：" + count);
		}

		/// <summary>
		/// 辅助方法：一旦网络设置发送变化，立即设置setChanged为true
		/// </summary>
		public void SetNetworkChangedTrue() {
			networkChanged = true;
		}

		#endregion

		/// <summary>
		/// 采用多线程去检测一些IP是否可用，并传回列表
		/// </summary>
		private List<int> getAvailableIPList(List<int> addIPList,int addVIPCount, string top3str,int lastStr) {

			Console.WriteLine("addVIPCount:" + addVIPCount );

			Thread[] threadArray = new Thread[addVIPCount];				
			for (int addIndex = 0; addIndex < addVIPCount; addIndex++)
			{
				int tempAddIndex = addIndex;
				threadArray[tempAddIndex] = new Thread(delegate ()
				{
					int addIP =  lastStr+ tempAddIndex ;

					Console.WriteLine( "正在检测" +  top3str + addIP + "是否可用...");
					if (IPHelper.CheckIPAvailable(mainIP, top3str + addIP))
					{						
						addIPList.Add(addIP);
						addVIPCount--;
					}
				});
				Thread.Sleep(100);
				threadArray[addIndex].Start();
			}

			// 下列代码，用以监视所有线程是否已经结束运行。每隔0.1s，去计算尚存活的线程数量，若数量为0，则说明所有线程已经结束了。
			while (true)
			{
				int unFinishedCount = 0;
				foreach (var thread in threadArray)
				{
					unFinishedCount += thread.IsAlive ? 1 : 0;
				}

				if (unFinishedCount == 0)
				{				
					break;
				}
				else
				{
					Thread.Sleep(100);
				}
			}

			addIPList.Sort();

			if (addVIPCount > 0)
			{
				return getAvailableIPList(addIPList, addVIPCount, top3str, addIPList[addIPList.Count-1] + 1);
			}
			else {
				return addIPList;
			}			
		}


		/// <summary>
		/// 事件：点击《Test》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void testButton_Click(object sender, EventArgs e)
		{
            //MessageBox.Show(IPHelper.CheckIPAvailableARPOnly("192.168.31.14","114.114.114.114").ToString());

            List<int> addIPList = getAvailableIPList(new List<int>(), 8, "192.168.14.", 96);

			Console.WriteLine("LIST<int>:");
			foreach (int ip in addIPList)
			{
				Console.WriteLine(ip);
			}

		}
	}
}
