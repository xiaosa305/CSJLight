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
		private int netcardIndex = -1; //选中网卡index
		private ManagementObject mo; //存放当前网卡的mo对象
		
		private string mainIP;  //当前网卡的主IP(第一个设的IP，mo对象取回来时在最后面)
		private string mainMask; // 当前网卡的主掩码
		private IList<string> vipList; //当前网卡的虚拟IP列表（不包含主ip）

		private Dictionary<string, ControlDevice> ledControlDevices; //搜索到的设备 字典（key为mac）
		private int controllerSelectedIndex = -1;  //选中的设备index

		private bool isStart = false; //是否启动模拟
		private bool isDebuging = false;  //是否启用调试
		private bool isRecording = false; // 是否正在录制
		private string recordPath = "C:\\Temp\\MultiLedFile"; //录制文件存储路径
		private int recordIndex = 0; //录制文件序号

		private bool networkChanged = false; //是否由《NewNetworkForm》更改网络设置：点过《多ip设置》《DHCP》《恢复设置》这三个按钮后需要设为true

		public NewMainForm()
		{
			InitializeComponent();

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
					refreshCurButton_Click(null, null);
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
			//搜索设备，只需在点击事件最后添加即可，不要放在实际方法内，避免重复操作
			searchDevices();
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
				//搜索设备，只需在点击事件最后添加即可，不要放在实际方法内，避免重复操作
				searchDevices();
			}
		}

		/// <summary>
		/// 事件：点击《启用DHCP》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dhcpButton_Click(object sender, EventArgs e)
		{
			if (netcardIndex == -1)
			{
				setNotice(1,"未选中可用网卡，请刷新后重试");
				return;
			}
			mo.InvokeMethod("SetDNSServerSearchOrder", null);
			mo.InvokeMethod("EnableDHCP", null);

			Thread.Sleep(1000);
			refreshNetcardInfo();
			setNotice(1,"已启用DHCP，即将自动为您搜索设备。");
			Thread.Sleep(1000);
			searchDevices();
		}

		/// <summary>
		/// 事件：点击《清空虚拟IP》（作用和启用DHCP相似，但主要是为了在无DHCP环境下，主动只保留当前主IP的设定）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void clearVIPButton_Click(object sender, EventArgs e)
		{
			if (netcardIndex == -1)
			{
				setNotice(1, "未选中可用网卡，请刷新后重试");
				return;
			}

			setBusy(true);
			IPAst ipAst = new IPAst(mo)
			{
				IpArray = new string[] { mainIP},
				SubmaskArray = new string[] { mainMask },
			};

			setNotice(1, "正在为您清空虚拟IP,清空后将刷新当前网卡信息，请稍候...");
			IPHelper.SetIPAddress(mo, ipAst);
			refreshCurButton_Click(null, null);
			setBusy(false);
		}

		/// <summary>
		/// 事件：点击《刷新当前网卡信息》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshCurButton_Click(object sender, EventArgs e)
		{
			refreshNetcardInfo();
			//搜索设备，只需在点击事件最后添加即可，不要放在实际方法内，避免重复操作
			searchDevices();
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
		/// 事件：点击《启动|关闭模拟》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void startButton_Click(object sender, EventArgs e)
		{
			if (!isStart)
			{
				setBusy(true);

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

					Ping ping = new Ping();

					// 此处为第一层获取可用IP的方法；
					for (; lastStr < 255; lastStr++)
					{
						string addIP = top3str + lastStr;
						setNotice(1,"正在检测" + addIP + "是否可用，请稍候...");
						
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
					// 若以上循环走完后，仍未达到所需的VIP数量，则从2开始，再走一遍获取可用IP的方法；
					if (addVIPCount > 0)
					{
						for (lastStr = 2; lastStr < 255; lastStr++)
						{
							string addIP = top3str + lastStr;
							setNotice(1,"正在检测" + addIP + "是否可用，请稍候...");
							
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
					ping.Dispose();//试试主动释放ping

					// 若仍未完成，则必须提示用户无可用ip并中断操作
					if (addVIPCount > 0)
					{
						MessageBox.Show("检测到当前网段无足够可用的IP地址，无法继续操作。");
						setNotice(1,"检测到当前网段无足够可用的IP地址，已中断操作。");
						
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
					
					IPHelper.SetIPAddress(mo, ipAst);
					refreshVirtualIPListView(newIPList);
				}

				ControlDevice device = getSelectedLedControl(controllerSelectedIndex);
				List<VirtualControlInfo> virtuals = new List<VirtualControlInfo>();
				////补充相应的虚拟IP后（可能原来已经足够了），利用interfaceCount数量（N）， 在右侧取前N个虚拟IP（N可能小于ipList.Count）；并填充virtuals列表
				for (int interfaceIndex = 0; interfaceIndex < interfaceCount; interfaceIndex++)
				{
					virtualIPListView.Items[interfaceIndex].SubItems[2].Text = (interfaceIndex + 1).ToString();
					virtuals.Add(new VirtualControlInfo(virtualIPListView.Items[interfaceIndex].SubItems[1].Text, device));
				}

				setNotice(1,"正在关联虚拟IP与设备，请稍候...");
				
				Art_Net_Manager.GetInstance().Start(virtuals, mainIP, mainIP, device);

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
		/// 辅助方法：把所有的组件都设为最初的空值
		/// </summary>
		private void clearAll()
		{
			mo = null;
			mainIP = null;
			mainMask = null;
			vipList = null;
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
				vipList = new List<string>();
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
						vipList.Add(ipAst.IpArray[i]);
					}
					searchButton.Enabled = true;		
				}
				else {
					searchButton.Enabled = false;
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
		/// 辅助方法：通过index，取出字典中Values内对应的设备对象
		/// </summary>
		/// <returns></returns>
		private ControlDevice getSelectedLedControl(int index)
		{
			return ledControlDevices.Values.ElementAt(index);
		}

		/// <summary>
		///辅助方法：搜索设备
		/// </summary>
		private void searchDevices()
		{
			if (searchButton.Enabled)
			{
				setNotice(1,"开始搜索设备，请稍候...");
				
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

				setNotice(1,"已将搜索到的设备添加设备列表中,并选中了第一个设备。");
			}
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
		/// <param name="enable"></param>
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
		/// <param name="msg"></param>
		private void setNotice(int place, string msg)
		{
			if (place == 1)
			{
				myStatusLabel1.Text = msg;
				statusStrip.Refresh();
			}
			if (place == 2)
			{
				myStatusLabel2.Text = msg;
				statusStrip.Refresh();
			}
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

		
	}
}
