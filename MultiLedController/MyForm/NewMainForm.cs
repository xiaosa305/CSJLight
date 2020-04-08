using MultiLedController.Ast;
using MultiLedController.entity;
using MultiLedController.utils.impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MultiLedController.MyForm
{
	public partial class NewMainForm : Form
	{
		private IList<ManagementObject> moList;
		private int netcardIndex = -1;
		private string mainIP;

		private Dictionary<string, ControlDevice> ledControlDevices;

		public NewMainForm()
		{
			InitializeComponent();
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
		/// 事件：点击《搜索（设备）》
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
				})
				{
					Tag = led.IP + "," +
					led.Led_interface_num + "," +
					led.Led_space + "," +
					led.Mac + "," +
					led.LedName
				}
			);
		}

		/// <summary>
		/// 辅助方法：刷新网卡列表
		/// </summary>
		private void refreshNetcardList()
		{
			clearNetcardList();

			// 获取本地计算机所有网卡信息			
			ManagementObjectSearcher search = new ManagementObjectSearcher("SELECT * FROM Win32_NetWorkAdapterConfiguration");
			moList = new List<ManagementObject>();
			foreach (ManagementObject mo in search.Get())
			{
				if (mo["IPAddress"] != null)
				{
					string carName = mo["Description"].ToString().Trim();
					netcardComboBox.Items.Add(carName);
					moList.Add(mo);
				}
			}
			netcardComboBox.SelectedIndex = 0;
			netcardIndex = 0;
		}

		/// <summary>
		/// 辅助方法：清空网卡列表
		/// </summary>
		private void clearNetcardList()
		{
			netcardComboBox.Items.Clear();
			netcardComboBox.Text = "";
			netcardComboBox.SelectedIndex = -1;

			moList = null;
		}

		/// <summary>
		/// 事件：选中不同网卡(ipComboBox)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void netcardComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			netcardIndex = netcardComboBox.SelectedIndex;
			virtualIPListView.Items.Clear();

			if (netcardIndex > -1)
			{
				IPAst ipAst = new IPAst(moList[netcardIndex]);			

				if (ipAst.IpArray.Length > 0)
				{
					mainIP = ipAst.IpArray[ipAst.IpArray.Length - 1];
					ipLabel2.Text = mainIP;
					submaskLabel2.Text = ipAst.SubmaskArray[0];
					gatewayLabel2.Text = ipAst.GatewayArray[0];
					dnsLabel2.Text = ipAst.DnsArray[0];

					for (int i = ipAst.IpArray.Length-2 ; i >= 0 ;  i--) {
						ListViewItem item = new ListViewItem();
						item.SubItems.Add(ipAst.IpArray[i]);									
						virtualIPListView.Items.Add( item);
					}							
				}				
			}
		}


		/// <summary>
		/// 辅助方法：设置提示信息
		/// </summary>
		/// <param name="msg"></param>
		private void setNotice(string msg) {
			myStatusLabel.Text = msg;
		}


	}
}
