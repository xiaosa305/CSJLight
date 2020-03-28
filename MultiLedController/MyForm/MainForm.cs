using MultiLedController.Common;
using MultiLedController.entity;
using MultiLedController.utils;
using MultiLedController.utils.impl;
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

namespace MultiLedController.MyForm
{
	public partial class MainForm : Form
	{
		private string appName = "TRANS-JOY MultiLed Controller";
		//private IList<string> ipList;
		private bool isRecording = false;
		private bool isDebuging = false;
		private string filePath = @"C:\Temp\MultiLedFile\record.bin";

		private string deviceIP ; 

		/// <summary>
		/// 左侧栏的首要选中项索引值
		/// </summary>
		private int controllerSelectedIndex = -1;

		/// <summary>
		/// 右侧栏的首要选中项索引值
		/// </summary>
		private int virtualIPSelectedIndex = -1;
		//private IList<int> virtualIPSelectedIndices;
		private Dictionary<string, ControlDevice> ledControlDevices ;

		List<VirtualControlInfo> virtuals; 

		public MainForm()
		{
			InitializeComponent();
			this.Text = appName;
		}


		private void MainForm_Load(object sender, EventArgs e)
		{
			refreshIPList();
		}

		/// <summary>
		/// 事件：点击《网络设置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkButton_Click(object sender, EventArgs e)
		{
			new NetworkForm( this ).ShowDialog();
		}

		//事件：点击《刷新IP列表》
		private void refreshButton_Click(object sender, EventArgs e)
		{
			refreshIPList();
		}

		/// <summary>
		/// 辅助方法：刷新本地IP列表
		/// </summary>
		private void refreshIPList()
		{
			setNotice("正在搜索网络设备，请稍候...");

			localIPComboBox.Items.Clear();
			
			IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
			if (ipe.AddressList != null && ipe.AddressList.Length > 0)
			{
				foreach (IPAddress ip in ipe.AddressList)
				{
					if (ip.AddressFamily == AddressFamily.InterNetwork) //当前ip为ipv4时，才加入到列表中
					{
						localIPComboBox.Items.Add(ip);
					}
				}
				localIPComboBox.SelectedIndex = 0;
				setSearchEnable(true);
				setNotice("已刷新本地IP列表，请选择主IP。");
			}
			else {
				localIPComboBox.SelectedIndex = -1;
				setSearchEnable(false);
				setNotice("本地无可用IP，请设置后重试。");
			}
		}

		/// <summary>
		/// 辅助方法：《搜索按键》等是否可用
		/// </summary>
		/// <param name="v"></param>
		private void setSearchEnable(bool enable)
		{
			this.searchButton.Enabled = enable;
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

			Art_Net_Manager.GetInstance().SearchDevice(localIPComboBox.Text);
			Thread.Sleep(1000);
			ledControlDevices = Art_Net_Manager.GetInstance().GetLedControlDevices();

			controllerListView.Items.Clear();		
			if (ledControlDevices.Count == 0) {
				setNotice("未搜索到任何设备，请确认后重试。");
				return;
			}

			foreach (ControlDevice led in ledControlDevices.Values)
			{				
				AddLedController(led);
			}
			setNotice("已将搜索到的设备添加到ListView中。");
		}

		/// <summary>
		/// 辅助方法：将LedControllor添加到controllerListView中
		/// </summary>
		/// <param name="led"></param>
		private void AddLedController(ControlDevice led)
		{			
			this.controllerListView.Items.Add(
				new ListViewItem(new string[]{
					"",										
					led.LedName,
					led.Mac
				})	{
					Tag = led.IP + "," +
					led.Led_interface_num+"," + 
					led.Led_space + "," +
					led.Mac + "," +
					led.LedName
				}
			);
		}

		/// <summary>
		/// 辅助方法：添加除初始ip外的8个虚拟ip到主界面的virtualIPListView中。
		/// </summary>
		/// <param name="virtualIPList"></param>
		internal void AddVirtualIPS(IList<string> virtualIPList)
		{
			virtualIPListView.Items.Clear();
			for(int i=1;i<virtualIPList.Count; i++)
			{
				ListViewItem lvItem = new ListViewItem(new String[] { i.ToString() , virtualIPList[i] , ""});
				virtualIPListView.Items.Add(lvItem);
			}
		}

		/// <summary>
		/// 辅助方法：清除virtualIPListView的选项
		/// </summary>
		internal void ClearIPListView()
		{
			virtualIPListView.Items.Clear();
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
				Art_Net_Manager.GetInstance().StopSaveToFile();

				isRecording = false;
				recordButton.Text = "录制数据";				
			}
			else {
				//string dirPath = filePath.Substring(0, filePath.LastIndexOf(@"\"));
				//string fileName = filePath.Substring(filePath.LastIndexOf(@"\") + 1);

				Art_Net_Manager.GetInstance().SetSaveFilePath(filePath);
				Art_Net_Manager.GetInstance().StartSaveToFile();

				isRecording = true;
				recordButton.Text = "停止录制";		
			}			

		}


		/// <summary>
		/// 事件：点击《启动模拟》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void startButton_Click(object sender, EventArgs e)
		{
			if (virtuals == null || virtuals.Count == 0) {
				setNotice("尚未绑定虚拟IP。");
				return;
			}

			if (!IPHelper.IsIPV4(mjsTextBox.Text)) {
				setNotice("请输入完整的mjsIP。");
				return;
			}

			string[] args = controllerListView.Items[controllerSelectedIndex].Tag.ToString().Split(',');
			deviceIP = args[0];

			Art_Net_Manager.GetInstance().Start(virtuals , localIPComboBox.Text  ,mjsTextBox.Text , getSelectedLedControl(controllerSelectedIndex) );
			debugButton.Enabled = true;
			recordButton.Enabled = true;
		}


		/// <summary>
		/// 辅助方法：
		/// </summary>
		/// <returns></returns>
		private ControlDevice getSelectedLedControl(int index)
		{
			return ledControlDevices.Values.ElementAt(index);
		}		

		/// <summary>
		/// 事件：选中《ControllerListView》的子项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void controllerListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (controllerListView.SelectedIndices.Count > 0)
			{
				controllerSelectedIndex = controllerListView.SelectedIndices[0];
				enableLinkButtons();
			}
		}

		/// <summary>
		/// 辅助方法：由是否选中控制器，来决定是否中间三个按钮是否可用
		/// </summary>
		private void enableLinkButtons()
		{
			networkButton2.Enabled = controllerSelectedIndex > -1;
			linkButton.Enabled = controllerSelectedIndex > -1;
		}

		/// <summary>
		/// 事件：点击《关联到IP》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void linkButton_Click(object sender, EventArgs e)
		{
			if (controllerSelectedIndex == -1) {
				setNotice("请选择物理设备");
				return;
			}
			if (virtualIPSelectedIndex == -1) {
				setNotice("请选择虚拟IP地址");				
				return;
			}

			string[] args = controllerListView.Items[controllerSelectedIndex].Tag.ToString().Split(',');
			string ledIp = args[0];
			int interfaceCount = int.Parse(args[1]);
			int spaceCount = int.Parse(args[2]);
			string mac = args[3];
			string ledName = args[4];

			ControlDevice device = getSelectedLedControl(controllerSelectedIndex);
			virtuals = new List<VirtualControlInfo>();

			foreach (ListViewItem item in virtualIPListView.Items)
			{
				item.SubItems[2].Text = "";			
			}

			foreach (ListViewItem item in virtualIPListView.SelectedItems)
			{
				item.SubItems[2].Text =mac;				
				virtuals.Add(new VirtualControlInfo(item.SubItems[1].Text ,  device));				
			}

			if (virtuals == null || virtuals.Count == 0)
			{
				setNotice("已清空旧的关联");
				startButton.Enabled = false;
			}
			else {
				setNotice("成功关联虚拟IP");
				startButton.Enabled = true;
			}
		}

		/// <summary>
		/// 辅助方法：显示提示
		/// </summary>
		/// <param name="msg"></param>
		private void setNotice(string msg)
		{
			myStatusLabel.Text = msg;
		}

		/// <summary>
		/// 事件：更改 《虚拟ipListBox》选中项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void virtualIPListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (virtualIPListView.SelectedIndices.Count > 0)
			{
				virtualIPSelectedIndex = this.virtualIPListView.SelectedIndices[0];	
			}
		}

		/// <summary>
		/// 事件：点击《实时调试》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void debugButton_Click(object sender, EventArgs e)
		{
			isDebuging = !isDebuging;
			debugButton.Text = isDebuging ? "停止调试" : "实时调试";
			if (isDebuging)
			{
				Art_Net_Manager.GetInstance().StartDebug();
			}
			else {
				Art_Net_Manager.GetInstance().StopDebug();
			}			
		}



		/// <summary>
		/// 事件：点击《录制文件路径》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setFilePathButton_Click(object sender, EventArgs e)
		{
			dmxSaveFileDialog.ShowDialog();	
		}

		/// <summary>
		/// 事件：确认《录制文件路径》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dmxSaveFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			filePath = dmxSaveFileDialog.FileName;
			this.filePathLabel.Text = filePath;
		}

		/// <summary>
		/// 事件：点击《清除关联》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void clearLinkButton_Click(object sender, EventArgs e)
		{
			virtuals = new List<VirtualControlInfo>();
			foreach (ListViewItem item in virtualIPListView.Items)
			{
				item.SubItems[2].Text = "";
			}
		}

		/// <summary>
		/// 事件：点击《网络设置（小）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkButton2_Click(object sender, EventArgs e)
		{
			if (controllerSelectedIndex == -1)
			{
				setNotice("请选择物理设备");
				return;
			}

			string[] args = controllerListView.Items[controllerSelectedIndex].Tag.ToString().Split(',');
			int ipLast = int.Parse(localIPComboBox.Text.Split('.')[3]);
			int interfaceCount = int.Parse(args[1]);
			//int spaceCount = int.Parse(args[2]);
			//string mac = args[3];
			//string ledName = args[4];

			new NetworkForm(this, ipLast, interfaceCount).ShowDialog(); 

		}
	}
}
