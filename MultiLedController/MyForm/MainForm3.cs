using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Management;
using MultiLedController.Common;
using MultiLedController.Ast;
using System.Threading;
using MultiLedController.multidevice.newmultidevice;

namespace MultiLedController.MyForm
{
	public partial class MainForm3 : Form
	{
		private IniFileHelper iniHelper;		
		private bool isFirstTime = true; //只用一次的方法，避免每次激活都跑一次刷新			

		private ManagementObject mo; //存放当前网卡的mo对象
		private string mainIP;  //当前网卡的主IP(第一个设的IP，mo对象取回来时在最后面)
		private string mainMask; // 当前网卡的主掩码
		private List<string> vipList; //当前网卡的虚拟IP列表（不包含主ip）

		private bool isStart = false; //是否启动模拟
		private bool isDebuging = false;  //是否正在调试
		private bool isRecording = false; // 是否正在录制
		private string recordPath = "C:\\Temp\\CSJ_SC"; //录制文件存储路径
		private int recordIndex = 0; //录制文件序号

		private NewVirtualDevice simulator;

		public MainForm3()
		{
			InitializeComponent();

			//MARK：添加这一句，会去掉其他线程使用本UI控件时弹出异常的问题(权宜之计，并非长久方案)。
			CheckForIllegalCrossThreadCalls = false;

			// 动态从ini文件内读取相应的数据
			iniHelper = new IniFileHelper(Application.StartupPath + @"\CommonSet.ini");
			string version = iniHelper.ReadString("CommonSet", "version", "3");
			Text += " v" + version + " beta";			
			interfaceCountComboBox.SelectedIndex = iniHelper.ReadInt( "CommonSet", "interfaceCount",0);
			spaceCountComboBox.SelectedIndex = iniHelper.ReadInt("CommonSet", "spaceCount", 0);
			controllerCountNUD.Value = iniHelper.ReadInt("CommonSet", "controllerCount", 1);
			autosetControllerCountNUDMaxinum();
		}

		private void MainForm3_Load(object sender, EventArgs e)
		{
			setRecordPathLabel();
			recordTextBox.Text = transformRecordIndex(recordIndex);

			//为recordTextBox添加失去焦点的监听事件（没有在VS中）
			recordTextBox.LostFocus += new EventHandler(recordTextBox_LostFocus);
			controllerCountNUD.MouseWheel += someNUD_MouseWheel;
		}

		/// <summary>
		///  事件：只在首次激活时，跑一次刷新网卡列表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm3_Activated(object sender, EventArgs e)
		{
            try
            {
				if (isFirstTime)
				{
					refreshNetcardList();
					isFirstTime = false;
				}
			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
		}

		/// <summary>
		/// 事件：选中不同的网卡（刷新网卡信息）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void netcardComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			refreshNetcardInfo();
		}

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
		/// 事件：点击《刷新网卡信息》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshNetcardInfoButton_Click(object sender, EventArgs e)
		{
			refreshNetcardInfo();
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
					
			//startButton.Enabled = false;
			debugButton.Enabled = false;
			recordButton.Enabled = false;
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

					int tempIndex = 1;
					for (int vipIndex = ipAst.IpArray.Length - 2; vipIndex >= 0; vipIndex--)
					{
						virtualIPListView.Items.Add(new ListViewItem(new string[] {
							 tempIndex++ +"",
							 ipAst.IpArray[vipIndex],							 
							 ""
						}));
						vipList.Add(ipAst.IpArray[vipIndex]);
					}

					setNotice(1, "已刷新当前网卡信息。", false);
				}
			}
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
				setNotice(1, "未选中可用网卡，请刷新后重试", true);
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

				setNotice(1, "正在为您清空虚拟IP,清空后将刷新当前网卡信息，请稍候...", false);
				IPHelper.SetIPAddress(mo, ipAst);
				refreshNetcardInfo(); // 无论设置成功与否，都主动刷新网卡信息			
			}
			else
			{
				setNotice(1, "检测到当前网卡并无虚拟IP，无需清空。", true);
			}

			setBusy(false);
		}
		
		#region 设置路数 及 启动模拟等

		/// <summary>
		///  事件：更改《 分控路数》和《每路空间数》后，调节《分控数NUD》的Maxium
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void countComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isFirstTime) {
				return;
			}
			autosetControllerCountNUDMaxinum();
		}

		private void autosetControllerCountNUDMaxinum()
		{
			int interfaceCount = int.Parse(interfaceCountComboBox.Text);
			int spaceCount = int.Parse(spaceCountComboBox.Text) / 170;
			controllerCountNUD.Maximum = 256 / interfaceCount / spaceCount;
		}

		/// <summary>
		/// 事件：点击《启动（停止）模拟》
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

			// 启动模拟
			if (!isStart)
			{
				DateTime beforeDT = System.DateTime.Now;
				setBusy(true);

				int interfaceCount = int.Parse(interfaceCountComboBox.Text);
				int spaceCount = int.Parse(spaceCountComboBox.Text) / 170;
				int controllerCount = decimal.ToInt32(controllerCountNUD.Value);
				int totalSpaceCount = interfaceCount * spaceCount * controllerCount;

				int neededVipCount = (int)(Math.Ceiling(interfaceCount * spaceCount * controllerCount / 4.0));
				int addVIPCount = neededVipCount - vipList.Count;

				if (addVIPCount > 0)
				{
					setNotice(1, "即将为您添加相应路数数量的虚拟IP,请稍候...", false);
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
						setNotice(1, "检测到当前网段无足够可用的IP地址，已中断操作。", true);
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

					setNotice(1, "正在为您设置虚拟IP，请稍候...", false);

					if (IPHelper.SetIPAddress(mo, ipAst))
					{
						refreshVirtualIPListView(newIPList);
					}
					else
					{
						setNotice(1, "启动模拟失败(虚拟IP设置失败，已恢复初始设置)。", true);
						setBusy(false);
						return;
					}
				}			
				
				Refresh();

				List<string> neededVipList = new List<string>();
				for (int vipIndex =0; vipIndex < neededVipCount; vipIndex++)
				{
					neededVipList.Add(vipList[vipIndex]);
					virtualIPListView.Items[vipIndex].SubItems[2].Text = "是";
				}

				
				if (simulator != null ) {
					simulator.Close();
				}
				simulator =  new NewVirtualDevice(interfaceCount, neededVipList, spaceCount, controllerCount, mainIP, mainIP);
				simulator.StartResponseDMXData();

				DateTime afterDT = System.DateTime.Now;
				TimeSpan ts = afterDT.Subtract(beforeDT);

				enableStartButtons(true);
				setNotice(1, "已启动模拟,共耗时: " + ts.TotalSeconds.ToString("#0.00") + " s", false);
				saveLastSet();
				setBusy(false);

				/// 启动模拟后，主动点击《开始调试》
				if (!isDebuging)
				{
					debugButton_Click(null, null);
				}
			}
			// 关闭模拟			
			else
			{
				setBusy(true);
				setNotice(1, "正在关闭模拟，请稍候...", false);

				if (isDebuging)
				{
					debugButton_Click(null, null);
				}

				if (simulator != null) {
					simulator.Close();
					simulator = null;
				}

				enableStartButtons(false);
				setNotice(1, "已关闭模拟。", false);
				setBusy(false);
			}
		}

		/// <summary>
		///辅助方法：保存启动模拟时的配置（下次打开软件时可直接使用此配置）
		/// </summary>
		private void saveLastSet()
		{
			iniHelper.WriteInt("CommonSet", "interfaceCount", interfaceCountComboBox.SelectedIndex);
			iniHelper.WriteInt("CommonSet", "spaceCount", spaceCountComboBox.SelectedIndex);
			iniHelper.WriteInt("CommonSet", "controllerCount", controllerCountNUD.Value);
			setNotice(2, "成功保存当前配置", false);
		}

		/// <summary>
		/// 事件：点击《开始调试 | 停止调试》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void debugButton_Click(object sender, EventArgs e)
		{
			isDebuging = !isDebuging;
			debugButton.Text = isDebuging ? "停止调试" : "开始调试";
			if (isDebuging)
			{
				simulator.StartDebug( showDebugFrame );
			}
			else
			{
				simulator.StopDebug();
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
					 "是"
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
				if (!recordPath.EndsWith(@"\CSJ_SC")) {
					recordPath += @"\CSJ_SC";
				}

				setRecordPathLabel();
				setNotice(2, "已设置存放目录为：" + recordPath, false);
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
			if (recordIndex >= 255)
			{
				setNotice(2, "录制文件序号不得大于255。", true);
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
				setNotice(2, "录制文件序号不得小于000。", true);
				return;
			}
			recordTextBox.Text = transformRecordIndex(--recordIndex);
			setNotice(2, "已设置录制文件名为：SC" + recordTextBox.Text + ".bin", false);
		}

		/// <summary>
		/// 事件：点击《录制数据 | 停止录制》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void recordButton_Click(object sender, EventArgs e)
		{
			//停止录制
			if (isRecording)
			{
				simulator.StopRecord();
				enableRecordButtons(false);
				plusButton_Click(null, null);
				setNotice(2, "已停止录制,并把录制序号加1。", false);
				recordButton.Text = "录制数据";
			}
			// 开始录制
			else
			{
				setNotice(2, "正在录制文件...", false);

				string binPath = recordPath + @"\SC" + recordTextBox.Text + ".bin";
				string configPath = recordPath + @"\csj.scu" ;

				try
				{
					simulator.StartRecord(binPath, configPath, showRecordFrame);
				}
				catch (Exception ex){
					Console.WriteLine(ex.Message);
				}

				enableRecordButtons(true);
				recordButton.Text = "停止录制";
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
			if (recordIndex > 255)
			{
				return "255";
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

		#region 全局方法

		/// <summary>
		/// 辅助方法：设置提示信息
		/// </summary>
		/// <param name="place">1 | 2 左1右2</param>
		/// <param name="msg"></param>
		private void setNotice(int place, string msg, bool msgShow)
		{
            try
            {
				if (place == 1)
				{
					myStatusLabel1.Text = msg;
				}
				if (place == 2)
				{
					myStatusLabel2.Text = msg;
				}

				if (msgShow)
				{
					MessageBox.Show(msg);
				}
			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
			
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
            try
            {
				setNotice(2, "当前录制帧数：" + count, false);
			}
			catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
		}

		/// <summary>
		/// 验证：对某些NumericUpDown进行鼠标滚轮的验证，避免一次性滚动过多
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someNUD_MouseWheel(object sender, MouseEventArgs e)
		{
			NumericUpDown nud = sender as NumericUpDown;
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = nud.Value + nud.Increment;
				if (dd <= nud.Maximum)
				{
					nud.Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = nud.Value - nud.Increment;
				if (dd >= nud.Minimum)
				{
					nud.Value = dd;
				}
			}
		}

		#endregion


	}
}
