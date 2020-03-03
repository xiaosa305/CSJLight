using MultiLedController.Entity;
using MultiLedController.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace MultiLedController.MyForm
{
	public partial class MainForm : Form
	{
		private string appName = "TRANS-JOY MultiLed Controller";
		private IList<string> ipList;
		private bool isRecording = false;


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
			new NetworkForm(this).ShowDialog();
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
			setStatusLabel("正在搜索网络设备，请稍候...");

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
				setSeachEnable(true);
				setStatusLabel("已刷新本地IP列表，请选择主IP。");
			}
			else {
				localIPComboBox.SelectedIndex = -1;
				setSeachEnable(false);
				setStatusLabel("本地无可用IP，请设置后重试。");
			}		
		}

		/// <summary>
		/// 辅助方法：下列是否可用
		/// </summary>
		/// <param name="v"></param>
		private void setSeachEnable(bool enable)
		{
			this.searchButton.Enabled = enable;
		}

		/// <summary>
		/// 辅助方法：显示提示
		/// </summary>
		/// <param name="msg"></param>
		private void setStatusLabel(string msg)
		{
			myStatusLabel.Text = msg;
		}


		/// <summary>
		/// 事件：点击《搜索设备》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void searchButton_Click(object sender, EventArgs e)
		{
			setStatusLabel("开始搜索设备，请稍候");
			controllorListView.Items.Clear();

			Art_Net_Manager.GetInstance().SearchDevice(localIPComboBox.Text);
			Dictionary<string, ControlDevice> ledControlDevices = Art_Net_Manager.GetInstance().GetLedControlDevices();
			
			foreach (ControlDevice led in ledControlDevices.Values)
			{
				Console.WriteLine(led);
				AddLedControllor(led);
			}

		}

		/// <summary>
		/// 辅助方法：将LedControllor添加到controllerListView中
		/// </summary>
		/// <param name="led"></param>
		private void AddLedControllor(ControlDevice led)
		{
			this.controllorListView.Items.Add(new ListViewItem( new string[]{ "1","Hello","World"} ) );
		}

		/// <summary>
		/// 辅助方法：添加除初始ip外的8个虚拟ip到主界面的virtualIPListView中。
		/// </summary>
		/// <param name="virtualIPList"></param>
		internal void AddVirtualIPS(IList<string> virtualIPList)
		{
			virtualIPListView.Items.Clear();
			for(int i=0;i<virtualIPList.Count; i++)
			{
				ListViewItem lvItem = new ListViewItem(new String[] { (i+1).ToString() , virtualIPList[i] });
				virtualIPListView.Items.Add(lvItem);
			}
		}

		internal void ClearIPListView()
		{
			virtualIPListView.Items.Clear();
		}

		/// <summary>
		/// 事件：点击《录制DMX | 停止录制》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void recordButton_Click(object sender, EventArgs e)
		{
			isRecording = !isRecording;
			recordButton.Text = isRecording ? "停止录制" : "录制DMX";
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			//Art_Net_Manager.GetInstance().Start(virtuals, maiajueshiIP);
		}

		private void linkButton_Click(object sender, EventArgs e)
		{
			AddLedControllor(null);
		}
	}
}
