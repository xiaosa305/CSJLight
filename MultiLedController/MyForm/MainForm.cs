using MultiLedController.Ast;
using MultiLedController.Common;
using MultiLedController.entity;
using MultiLedController.entity.dto;
using MultiLedController.multidevice.impl;
using MultiLedController.utils.impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MultiLedController.MyForm
{
	public partial class MainForm : Form
	{
		private bool isFirstTime = true; //只用一次的方法，避免每次激活都跑一次刷新	

		private ManagementObject mo; //存放当前网卡的mo对象
		private string mainIP;  //当前网卡的主IP(第一个设的IP，mo对象取回来时在最后面)
		private string mainMask; // 当前网卡的主掩码
		private IList<string> vipList; //当前网卡的虚拟IP列表（不包含主ip）
		
		private IList<ControlDevice> deviceList; // 将所有设备放到列表中	
		private IList<int> choosenIndexList;  //  序列设备 的索引列表				
		private List<ControlDeviceDTO> deviceDtoList;  //序列 设备Dto 列表 ； 包括设备及其对应的 虚拟IP列表，以及 录制文件路径

		private bool isStart = false; //是否启动模拟
		private bool isDebuging = false;  //是否正在调试
		private bool isRecording = false; // 是否正在录制
		private string recordPath = "C:\\Temp\\CSJ_SC"; //录制文件存储路径
		private int recordIndex = 0; //录制文件序号

		public MainForm()
		{
			InitializeComponent();

			// 为软件添加版本号显示(记录【程序集版本】，单设备版本则采用【文件版本】，便于区分)
			string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			Text += " v" + version +" beta";

			//MARK：添加这一句，会去掉其他线程使用本UI控件时弹出异常的问题(权宜之计，并非长久方案)。
			CheckForIllegalCrossThreadCalls = false;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			setRecordPathLabel();
			recordTextBox.Text = transformRecordIndex(recordIndex);
			
			//为recordTextBox添加失去焦点的监听事件（没有在VS中）
			recordTextBox.LostFocus += new EventHandler(recordTextBox_LostFocus); 
			
		}
		
		/// <summary>
		/// 事件：只在首次激活时，跑一次（写在Activated事件的原因，在于避免启动时界面加载太久）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Activated(object sender, EventArgs e)
		{
			if (isFirstTime)
			{
				refreshNetcardList();
				isFirstTime = false;
			}
		}

		#region 搜索设备相关

		/// <summary>
		/// 事件：点击《刷新网卡列表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshNetcardButton_Click(object sender, EventArgs e)
		{
			refreshNetcardList();
		}

		/// <summary>
		/// 事件：更改《网卡ComboBox》选项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void netcardComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			refreshNetcardInfo();
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
		/// 事件：点击《启用DHCP》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dhcpButton_Click(object sender, EventArgs e)
		{
			if (netcardComboBox.SelectedIndex == -1)
			{
				setNotice(1, "未选中可用网卡，请刷新后重试", true);
				return;
			}
			mo.InvokeMethod("SetDNSServerSearchOrder", null);
			mo.InvokeMethod("EnableDHCP", null);
			setNotice(1, "正在启用DHCP，请稍候...", false);
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
				setNotice(1, "未选中可用网卡，请刷新后重试",true);
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

				setNotice(1, "正在为您清空虚拟IP,清空后将刷新当前网卡信息，请稍候...",false);
				IPHelper.SetIPAddress(mo, ipAst);
				refreshNetcardInfo(); // 无论设置成功与否，都主动刷新网卡信息			
			}
			else
			{
				setNotice(1, "检测到当前网卡并无虚拟IP，无需清空。",true);
			}

			setBusy(false);
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
		/// 辅助方法：刷新网卡列表
		/// </summary>
		private void refreshNetcardList()
		{
			setNotice(1, "正在刷新网卡列表...", false);
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
			else
			{
				setNotice(1, "未找到可用网卡,请处理后《刷新列表》。", true);
			}
		}
		
		/// <summary>
		///辅助方法：搜索设备
		/// </summary>
		private void searchDevices()
		{
			Console.WriteLine("searchDevices...");

			if (netcardComboBox.SelectedIndex == -1)
			{
				setNotice(1, "未选中可用网卡，无法搜索设备。", false);
				return;
			}

			if (string.IsNullOrEmpty(mainIP))
			{
				setNotice(1, "未设置主IP地址，无法搜索设备。", false);
				return;
			}

			setNotice(1, "正在搜索设备，请稍候...", false);
		
			deviceListView.Items.Clear();
			choosenListView.Items.Clear();
			startButton.Enabled = false;

			Art_Net_Manager.GetInstance().SearchDevice(ipLabel2.Text);
			Thread.Sleep(1000);
			Dictionary<string, ControlDevice> deviceDict = Art_Net_Manager.GetInstance().GetLedControlDevices();
			deviceList = new List<ControlDevice>();
			choosenIndexList = new List<int>();

			if (deviceDict.Count == 0)
			{
				setNotice(1, "未搜索到任何设备，请确认后重试。", false);
				return;
			}

			foreach (ControlDevice dev in deviceDict.Values)
			{
				deviceList.Add(dev);
			}
			refreshLeftListView();

			setNotice(1, "已将搜索到的设备添加待命列表中，请逐一添加需使用的设备。", false);
		}

		/// <summary>
		/// 辅助方法：把所有的组件都设为最初的空值;按钮Enabled还原为false
		/// </summary>
		private void clearAll()
		{
			clearNetcardInfo(); //clearAll()

			netcardInfoGroupBox.Enabled = false;

			netcardComboBox.SelectedIndex = -1;
			netcardComboBox.Items.Clear();
			netcardComboBox.Text = "";
		}

		/// <summary>
		/// 辅助方法：刷新网卡信息，主要用于更改网卡列表及主动刷新网卡时
		/// </summary>
		private void refreshNetcardInfo()
		{
			clearNetcardInfo();  //refreshNetcardInfo()
			vipList = new List<string>();

			if (netcardComboBox.SelectedIndex > -1)
			{
				mo = IPHelper.GetNetCardMO(netcardComboBox.Text);
				if (mo == null)
				{
					setNotice(1, "当前网卡不可用,请处理后《刷新当前网卡信息》。", true);
					return;
				}

				IPAst ipAst = new IPAst(mo);
				if (ipAst.IpArray.Length > 0)
				{
					mainIP = ipAst.IpArray[ipAst.IpArray.Length - 1];
					mainMask = ipAst.SubmaskArray[ipAst.SubmaskArray.Length - 1];

					ipLabel2.Text = mainIP;
					submaskLabel2.Text = mainMask;
					if (ipAst.GatewayArray != null && ipAst.GatewayArray.Length > 0)
					{
						gatewayLabel2.Text = ipAst.GatewayArray[0];
					}
					if (ipAst.DnsArray != null && ipAst.DnsArray.Length > 0)
					{
						dnsLabel2.Text = ipAst.DnsArray[0];
					}
					searchButton.Enabled = true;

					//刷新当前网卡信息后，都主动搜索设备
					setNotice(1, "已刷新当前网卡信息，即将自动为您搜索设备...", false);
					Refresh();
					Thread.Sleep(1000);
					searchDevices();

					int tempIndex = 1;
					for (int i = ipAst.IpArray.Length - 2; i >= 0; i--)
					{
						ListViewItem item = new ListViewItem(new string[] {
							 tempIndex++ +"",
							 ipAst.IpArray[i],
							 "",
							 ""
						});
						virtualIPListView.Items.Add(item);
						vipList.Add(ipAst.IpArray[i]);
					}
				}
			}
		}
				
		/// <summary>
		/// 只清空当前网卡及相关的显示（原则：先清内存后清界面）
		/// </summary>
		private void clearNetcardInfo()
		{
			mo = null;

			mainIP = null;
			mainMask = null;
			ipLabel2.Text = "";
			submaskLabel2.Text = "";
			gatewayLabel2.Text = "";
			dnsLabel2.Text = "";

			vipList = null;
			virtualIPListView.Items.Clear();

			deviceList = null;			
			deviceListView.Items.Clear();
			choosenIndexList = null;			
			choosenListView.Items.Clear();
			
			searchButton.Enabled = false;
			startButton.Enabled = false;
			debugButton.Enabled = false;
			recordButton.Enabled = false;
		}

		#endregion
		
		#region 设置设备序列		

		/// <summary>
		/// 事件：更改《搜到列表》的选中项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void controllerListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			addButton.Enabled = deviceListView.SelectedIndices.Count > 0;
		}

		/// <summary>
		/// 事件：更改《序列列表》 的选中项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void choosenListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			delButton.Enabled = choosenListView.SelectedIndices.Count > 0 ;			
		}

		/// <summary>
		/// 事件：点击《↓》或 双击《搜到设备选项（左上listView）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addButton_Click(object sender, EventArgs e)
		{
			if (deviceListView.SelectedIndices.Count > 0) {
				int deviceIndex = int.Parse( deviceListView.Items[ deviceListView.SelectedIndices[0] ].Tag.ToString() );
				choosenIndexList.Add(deviceIndex);
				refreshLeftListView();
			}
			else {
				setNotice(1, "尚未选中设备，无法加到序列中。", false);
			}
		}

		/// <summary>
		/// 事件：点击《↑》或双击《序列设备选项（左下listView）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void delButton_Click(object sender, EventArgs e)
		{
			if (choosenListView.SelectedIndices.Count > 0)
			{			
				// 先进行验证				
				int deviceIndex = int.Parse(choosenListView.Items[choosenListView.SelectedIndices[0]].Tag.ToString());			
				if (deviceIndex != choosenIndexList[choosenListView.SelectedIndices[0]]) {
					//Console.WriteLine( deviceIndex + "  -- chooseIndexList["+ choosenSelectedIndex + "] = " + choosenIndexList[choosenSelectedIndex] );
					setNotice(1 , "删除的设备数据有错，请确认后重试。" , true);
					return;
				}
				// 数据无误后，删除相应的项，并刷新列表
				choosenIndexList.RemoveAt(choosenListView.SelectedIndices[0]);
				refreshLeftListView();
			}
			else
			{
				setNotice(1, "尚未选中设备，无法加到序列中。", false);
			}
		}

		/// <summary>
		/// 事件：点击《↑↑↑》（清空所有设备）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void delAllButton_Click(object sender, EventArgs e)
		{
			choosenIndexList.Clear();
			refreshLeftListView();
		}

		/// <summary>
		/// 辅助方法：通过两个全局列表deviceList和chooseIndexList，来渲染左侧两个ListView
		/// </summary>
		private void refreshLeftListView()
		{
			deviceListView.Items.Clear();			
			addButton.Enabled = false;						
			choosenListView.Items.Clear();
			delButton.Enabled = false;

			for (int devIndex = 0; devIndex < deviceList.Count; devIndex++)
			{
				if (!choosenIndexList.Contains(devIndex))
				{
					addDeviceItem(devIndex);
				}
			}

			for (int cIndex = 0; cIndex < choosenIndexList.Count; cIndex++)
			{
				int devIndex = choosenIndexList[cIndex];
				addChoosenItem(cIndex + 1, devIndex);
			}

			startButton.Enabled = !isStart && choosenListView.Items.Count > 0;
			delAllButton.Enabled = choosenListView.Items.Count > 0;
		}

		/// <summary>
		/// 辅助方法：将LedControllor添加到controllerListView中
		/// </summary>
		/// <param name="dev"></param>
		private void addDeviceItem(int deviceIndex)
		{
			if (deviceList == null || deviceList.Count == 0 || deviceList.Count <= deviceIndex)
			{
				setNotice(1, "列表为空，或索引大于列表项，无法加入DeviceListView。", true);
				return;
			}

			ControlDevice dev = deviceList[deviceIndex];
			deviceListView.Items.Add(
				new ListViewItem(new string[]{
					"",
					dev.LedName,
					dev.IP,
					dev.Led_interface_num.ToString()
				})
				{
					Tag = deviceIndex
				}
			);
		}

		/// <summary>
		/// 辅助方法：将LedControllor添加到controllerListView中
		/// </summary>
		/// <param name="dev"></param>
		private void addChoosenItem(int chooseIndex, int deviceIndex)
		{
			if (deviceList == null || deviceList.Count == 0 || deviceList.Count <= deviceIndex)
			{
				setNotice(1, "列表为空，或索引大于列表项，无法加入DeviceListView。", true);
				return;
			}

			ControlDevice dev = deviceList[deviceIndex];
			choosenListView.Items.Add(
				new ListViewItem(new string[]{
					chooseIndex.ToString(),
					dev.LedName,
					dev.IP,
					dev.Led_interface_num.ToString()
				})
				{
					Tag = deviceIndex
				}
			);
		}

		#endregion

		#region 模拟、调试相关

		/// <summary>
		/// 事件：点击《启动模拟 | 关闭模拟》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void startButton_Click(object sender, EventArgs e)
		{
			//避免更改路数之后，虚拟IP显示错误，先清空所有的《关联路数》Text ( 不论是启动还是关闭模拟，都先清空关联 )
			foreach (ListViewItem item in virtualIPListView.Items)
			{
				item.SubItems[2].Text = "";
				item.SubItems[3].Text = "";
			}
			Refresh();

			if (!isStart)
			{
				DateTime beforeDT = System.DateTime.Now;

				setBusy(true);

				if (choosenIndexList == null || choosenIndexList.Count == 0)
				{
					MessageBox.Show("尚未选中设备，无法启动模拟。");
					setNotice(1, "尚未选中设备，无法启动模拟。",true);
					setBusy(false);
					return;
				}

				int addVIPCount = - vipList.Count;
				for (int i = 0; i < choosenIndexList.Count; i++)
				{
					int deviceIndex = choosenIndexList[i];
					addVIPCount +=  deviceList[deviceIndex].Led_interface_num;
				}
				
				if (addVIPCount > 0)
				{
					setNotice(1, "即将为您添加相应路数数量的虚拟IP,请稍候...",false);
					string firstIP;
					if (vipList == null || vipList.Count == 0)
					{
						firstIP = mainIP;
					}
					else
					{
						firstIP = vipList[0];
					}
					string ipTop3Str = firstIP.Substring(0, firstIP.LastIndexOf('.') + 1);
					int ipLastStr = int.Parse(firstIP.Substring(firstIP.LastIndexOf('.') + 1)) + 1;

					SortedSet<int> addVIPSet = new SortedSet<int>();
					generateAvailableIPListMultiThread(ref addVIPSet, ref addVIPCount, ipTop3Str, ipLastStr, 253);

					//若仍未完成，则必须提示用户无可用ip并中断操作
					if (addVIPCount > 0)
					{
						MessageBox.Show("检测到当前网段无足够可用的IP地址，无法继续操作。");
						setNotice(1, "检测到当前网段无足够可用的IP地址，已中断操作。",true);
						setBusy(false);
						return;
					}

					//以新IP及掩码列表， 改造IPAst；并将（mainIP及新的ipList）设置到系统中
					foreach (int tempIP in addVIPSet)
					{
						vipList.Add(ipTop3Str + tempIP);
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

					setNotice(1, "正在为您设置虚拟IP，请稍候...",false);

					if (IPHelper.SetIPAddress(mo, ipAst))
					{
						refreshVirtualIPListView(newIPList);
					}
					else
					{
						MessageBox.Show("虚拟IP设置失败，已恢复初始设置。");
						setNotice(1, "启动模拟失败(虚拟IP设置失败，已恢复初始设置)。",true);
						setBusy(false);
						return;
					}
				}

				int startInterface = 0;

				deviceDtoList = new List<ControlDeviceDTO>();

				for (int i = 0; i < choosenIndexList.Count; i++)
				{
					int deviceIndex = choosenIndexList[i];
					ControlDevice device = deviceList[ deviceIndex ];				

					int interfaceCount = device.Led_interface_num;
					string devName = device.LedName ;
					string ip = device.IP ;
					List<string> VIPList = new List<string>();

					for (int interfaceIndex = 0 ; interfaceIndex < interfaceCount;  interfaceIndex++ )
					{
						virtualIPListView.Items[interfaceIndex + startInterface].SubItems[2].Text = devName;
						virtualIPListView.Items[interfaceIndex + startInterface].SubItems[3].Text = (interfaceIndex + 1).ToString();
						VIPList.Add( virtualIPListView.Items[interfaceIndex + startInterface].SubItems[1].Text  );
					}
					startInterface += interfaceCount;

					deviceDtoList.Add(new ControlDeviceDTO() {
						ControlDevice = device,
						VirtualIps = VIPList						
					});
				}
				Refresh();

				setNotice(1, "正在关联虚拟IP与设备，请稍候...",false);

				try
				{
					TransactionManager.GetTransactionManager().AddDevice(deviceDtoList, mainIP).StartAllDeviceReceiveDmxData();					
				}
				catch (Exception ex)
				{
					MessageBox.Show("启动模拟失败。\n" +
						"原因（异常）是：" + ex.Message + "。\n" +
						"若因IP配置失败，可《启用DHCP》或《清空虚拟IP》后重试。");
					setNotice(1, "启动模拟失败。", true);
					setBusy(false);
					return;
				}

				DateTime afterDT = System.DateTime.Now;
				TimeSpan ts = afterDT.Subtract(beforeDT);

				enableStartButtons(true);
				setNotice(1, "已启动模拟,共耗时: " + ts.TotalSeconds.ToString("#0.00") + " s",false);
				setBusy(false);
			}
			// 关闭模拟			
			else
			{
				setBusy(true);
				setNotice(1, "正在关闭模拟，请稍候...",false);

				if (isDebuging)
				{
					debugButton_Click(null, null);
				}
				TransactionManager.GetTransactionManager().StopAllDeviceReceiveDmxData().CloseAllDevice();			

				enableStartButtons(false);
				setNotice(1, "已关闭模拟。", false);
				setBusy(false);
			}

		}
		
		/// <summary>
		/// 事件：点击《开始调试》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void debugButton_Click(object sender, EventArgs e)
		{
			isDebuging = !isDebuging;
			debugButton.Text = isDebuging ? "停止调试" : "开始调试";
			if (isDebuging)
			{
				TransactionManager.GetTransactionManager().StartAllDeviceDebug().SetDebugFrameCountResponse(deviceDtoList, showDebugFrame);
			}
			else
			{
				TransactionManager.GetTransactionManager().StopAllDeviceDebug();
			}
		}

		/// <summary>
		/// 辅助方法：采用多线程方式，检测一些IP是否未被占用
		/// </summary>
		private void generateAvailableIPListMultiThread(ref SortedSet<int> addVIPSet, ref int addVIPCount, string top3IPStr, int lastIPStr, int retainIPCount)
		{
			Console.WriteLine("retainIPCount = " + retainIPCount + " | addVIPCount = " + addVIPCount);
			// 当剩余次数小于1时，退出本方法
			if (retainIPCount < 1)
			{
				Console.WriteLine("当剩余次数小于1时，退出本方法。");
				return;
			}

			if (addVIPCount > 254)
			{
				Console.WriteLine("虚拟IP数量不得超过254个。");
				return;
			}

			SortedSet<int> addVIPSetTemp = new SortedSet<int>();
			retainIPCount -= addVIPCount; //一旦开始执行此方法，则可以直接处理retianIPCount

			//从这里分割：
			int firstCount = 255 - lastIPStr;
			int overCount = 0;
			int lastStr2 = 0;
			if (firstCount > addVIPCount)
			{
				firstCount = addVIPCount;
				lastStr2 = lastIPStr + firstCount;
			}
			else
			{
				overCount = addVIPCount - firstCount;
				lastStr2 = 2 + overCount;
			}

			Thread[] threadArray = new Thread[addVIPCount];

			for (int addIndex = 0; addIndex < firstCount; addIndex++)
			{
				int tempAddIndex = addIndex;
				threadArray[tempAddIndex] = new Thread(delegate ()
				{
					int addIP = lastIPStr + tempAddIndex;
					Console.WriteLine("正在(多线程ThreadIndex:" + tempAddIndex + ")检测" + top3IPStr + addIP + "是否可用...");

					if (IPHelper.CheckIPAvailableARPOnly(mainIP, top3IPStr + addIP))
					{
						lock (addVIPSetTemp)
						{
							addVIPSetTemp.Add(addIP);
						}
					}
				});
				threadArray[tempAddIndex].Start();
			}

			for (int addIndex = 0; addIndex < overCount; addIndex++)
			{
				int tempAddIndex = addIndex;
				threadArray[tempAddIndex + firstCount] = new Thread(delegate ()
				{
					int addIP = 2 + tempAddIndex;
					Console.WriteLine("正在(多线程ThreadIndex:" + tempAddIndex + firstCount + ")检测" + top3IPStr + addIP + "是否可用...");
					if (IPHelper.CheckIPAvailableARPOnly(mainIP, top3IPStr + addIP))
					{
						lock (addVIPSetTemp)
						{
							addVIPSetTemp.Add(addIP);
						}
					}
				});
				threadArray[tempAddIndex + firstCount].Start();
			}

			// 下列代码，用以监视所有线程是否已经结束运行。每隔0.1s，去计算尚存活的线程数量，若数量为0，则说明所有线程已经结束了。
			while (true)
			{
				int unFinishedCount = 0;
				foreach (Thread thread in threadArray)
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

			foreach (int item in addVIPSetTemp)
			{
				// 若添加成功，则返回true，否则不处理addVIPCount
				if (addVIPSet.Add(item))
				{
					addVIPCount--;
				}
			}

			// 若①数量仍然不够；②且剩下未监测的IP数量大于addVIPCount, 才继续递归本方法；除此以外，则无意义（因为再也没有相应数量的IP可供使用了）
			if (addVIPCount > 0 && addVIPCount <= retainIPCount)
			{
				generateAvailableIPListMultiThread(ref addVIPSet, ref addVIPCount, top3IPStr, lastStr2, retainIPCount);
			}
		}

		/// <summary>
		/// 辅助方法：通过ipList主动刷新右侧列表
		/// </summary>
		private void refreshVirtualIPListView(IList<string> newIPList)
		{
			virtualIPListView.Items.Clear();

			for (int tempIndex = 1; tempIndex < newIPList.Count; tempIndex++)
			{
				ListViewItem item = new ListViewItem(new string[] {
					 tempIndex +"",
					 newIPList[tempIndex],
					 "",
					 ""
				});
				virtualIPListView.Items.Add(item);
			}
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

		#endregion

		#region 录制相关

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
				setNotice(2, "已设置存放目录为：" + recordPath ,false);
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
			else
			{
				recordIndex = int.Parse(recordTextBox.Text);
			}
			recordTextBox.Text = transformRecordIndex(recordIndex);
			setNotice(2, "已设置录制文件名为：SC" + recordTextBox.Text + ".bin", false);
		}
		
		/// <summary>
		/// 事件：点击《+》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void plusButton_Click(object sender, EventArgs e)
		{
			if (recordIndex >= 999)
			{
				setNotice(2, "录制文件序号不得大于999。",true);
				return;
			}
			recordTextBox.Text = transformRecordIndex(++recordIndex);
			setNotice(2, "已设置录制文件名为：SC" + recordTextBox.Text + ".bin", false);
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
				setNotice(2, "录制文件序号不得小于000。",true);
				return;
			}
			recordTextBox.Text = transformRecordIndex(--recordIndex);
			setNotice(2, "已设置录制文件名为：SC" + recordTextBox.Text + ".bin",false);
		}

		/// <summary>
		/// 事件：点击《录制数据 | 停止录制》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void recordButton_Click(object sender, EventArgs e)
		{
			if (isRecording)
			{
				TransactionManager.GetTransactionManager().StopAllDeviceRecode();
				enableRecordButtons(false);
				plusButton_Click(null, null);
				setNotice(2, "已停止录制,并把录制序号加1。",false);
				recordButton.Text = "录制数据";
			}
			else
			{
				if (deviceDtoList == null || deviceDtoList.Count == 0) {
					setNotice(2, "deviceDtoList数据为空，无法录制。", false);
					return;
				}

				setNotice(2, "正在录制文件...",false);
				for (int i = 0; i < deviceDtoList.Count; i++)
				{					
					deviceDtoList[i].RecodeFilePath = recordPath + @"\" + (i+1) + @"("+ deviceDtoList[i].ControlDevice.LedName.Replace("\0","")+@")\SC" + recordTextBox.Text + ".bin";
				}
				TransactionManager.GetTransactionManager().SetRecodeFilePath(deviceDtoList).StartAllDeviceRecode().SetRecordFrameCountResponse(deviceDtoList , showRecordFrame);
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

		/// <summary>
		/// 辅助方法：处理int型,使之成为两位数的string表示
		/// </summary>
		/// <param name="recordIndex"></param>
		/// <returns></returns>
		private string transformRecordIndex(int recordIndex)
		{
			if (recordIndex < 0)
			{
				return "000";
			}
			if (recordIndex > 999)
			{
				return "999";
			}

			if (recordIndex < 100)
			{
				if (recordIndex < 10)
				{
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

		#endregion

		#region 各委托方法 及 全局提示等
		
		/// <summary>
		/// 辅助方法：设置提示信息
		/// </summary>
		/// <param name="place">1 | 2 左1右2</param>
		/// <param name="msg"></param>
		private void setNotice(int place, string msg, bool msgShow)
		{
			if (msgShow)
			{
				MessageBox.Show(msg);
			}

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
			Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
			Enabled = !busy;
		}

		/// <summary>
		/// 辅助方法：实现展示调试帧数的委托
		/// </summary>
		/// <param name="count"></param>
		private void showDebugFrame(int count)
		{
			setNotice(1, "当前调试帧数：" + count, false);
		}

		/// <summary>
		/// 辅助方法：实现展示录制帧数的委托
		/// </summary>
		/// <param name="count"></param>
		private void showRecordFrame(int count)
		{
			setNotice(2, "当前录制帧数：" + count, false);
		}
			
		#endregion

		/// <summary>
		/// 事件：点击《Test》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void testButton_Click(object sender, EventArgs e)
		{
			Console.WriteLine("deviceList : ");
			foreach (ControlDevice dev in deviceList)
			{
				Console.WriteLine(dev.LedName.Replace("\0",""));
			}

			Console.Write("choosenIndexList : ");
			foreach (int item in choosenIndexList)
			{
				Console.Write(item + "    ");
			}
			Console.WriteLine();
		}


	}
}
