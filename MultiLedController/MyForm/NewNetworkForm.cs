using MultiLedController.Ast;
using MultiLedController.Common;
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
	public partial class NewNetworkForm : Form
	{
		private NewMainForm mainForm; //主窗体，主要作用：1.提供窗体位置；2.提供SetChanged值供改变
		private IList<ManagementObject> moList; 
		private int netcardIndex = -1;
		private IPAst tempIpAst;

		public NewNetworkForm(NewMainForm mainForm)
		{
			InitializeComponent();			
			this.mainForm = mainForm;
		}

		private void NewNetworkForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			refreshNetcardList();
		}

		/// <summary>
		/// 事件：点击《右上角？》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewNetworkForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show("1.若当前网卡有多个IP地址或DNS地址，将鼠标悬浮在该地址上，即可查看全部；\n" +
				"2.在设置多个IP地址前，请确认即将设置的地址在本网段内未被使用，否则无法设置成功，甚至程序将卡死。");
			e.Cancel = true;
		}

		/// <summary>
		/// 事件：点击《刷新（网卡列表）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshNetcardButton_Click(object sender, EventArgs e)
		{
			refreshNetcardList();
			setNotice("已刷新网卡列表；若当前网卡有多个IP，可鼠标悬停在IP地址上查看全部。");
		}

		/// <summary>
		/// 事件：选中不同网卡(ipComboBox)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void netcardComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			netcardIndex = netcardComboBox.SelectedIndex;
			if (netcardIndex > -1)
			{
				IPAst ipAst = new IPAst(moList[netcardIndex]);
				try
				{
					if (ipAst.IpArray != null && ipAst.IpArray.Length > 0) {
						string mainIP = ipAst.IpArray[ipAst.IpArray.Length - 1];
						ipLabel2.Text = mainIP;
						string ipListStr = "";
						for (int ipIndex = ipAst.IpArray.Length - 1; ipIndex >= 0; ipIndex--)
						{
							ipListStr += ipAst.IpArray[ipIndex] + "\n";
						}
						myToolTip.SetToolTip(ipLabel2, ipListStr);

						string[] ipStrArray = mainIP.Split('.');
						int thirdIP = int.Parse(ipStrArray[2]);
						thirdNumericUpDown.Value = thirdIP;
						int finalIP = int.Parse(ipStrArray[3]);
						finalNumericUpDown.Value = finalIP;
					}
					if (ipAst.SubmaskArray != null && ipAst.SubmaskArray.Length > 0)
					{
						submaskLabel2.Text = ipAst.SubmaskArray[0];
					}

					if (ipAst.GatewayArray != null && ipAst.GatewayArray.Length > 0) {
						gatewayLabel2.Text = ipAst.GatewayArray[0];
					}

					if (ipAst.DnsArray != null && ipAst.DnsArray.Length > 0)
					{
						dnsLabel2.Text = ipAst.DnsArray[0];
						string dnsListStr = "";
						for (int dnsIndex = 0; dnsIndex <ipAst.DnsArray.Length ; dnsIndex++)
						{
							dnsListStr += ipAst.DnsArray[dnsIndex] + "\n";
						}
						myToolTip.SetToolTip(dnsLabel2, dnsListStr);
					}
				}
				catch (Exception ex) {
					setNotice("出现异常:" + ex.Message);
				}		
			}
		}

		/// <summary>
		///事件：点击《保存初始设置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			tempIpAst = new IPAst(moList[netcardIndex]);
			loadButton.Enabled = tempIpAst != null;
		}

		/// <summary>
		/// 事件：点击《恢复初始设置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loadButton_Click(object sender, EventArgs e)
		{
			IPHelper.SetIPAddress(moList[netcardIndex], tempIpAst);
			mainForm.SetNetworkChangedTrue();
			setNotice("已成功恢复保存的设置，请刷新。");
		}

		/// <summary>
		/// 事件：点击《启用dhcp》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dhcpButton_Click(object sender, EventArgs e)
		{
			if (netcardIndex == -1)
			{
				setNotice("未选中可用网卡，请刷新后重试。");
				return;
			}
			ManagementObject mo = moList[netcardIndex];
			EnableDHCP(mo);
			Thread.Sleep(500);
			mainForm.SetNetworkChangedTrue();
			setNotice("已启用DHCP,请刷新。");	
		}

		/// <summary>
		/// 事件：点击《设置连续IP地址》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void set9IPButton_Click(object sender, EventArgs e)
		{
			if (tempIpAst == null)
			{
				DialogResult dr = MessageBox.Show(
					"尚未存储当前IP配置，是否存储设置后再进行设置？",
					"继续设置？",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Warning);

				if (dr == DialogResult.Cancel)
				{
					setNotice("已取消多IP设置。");
					return;
				}
				else if (dr == DialogResult.Yes)
				{
					saveButton_Click(null, null);
				}
			}
			setBusy(true);
			setNotice("正在为您设置多个IP地址，请稍候...");
			int thirdIP = Decimal.ToInt16(thirdNumericUpDown.Value);
			int firstIP = Decimal.ToInt16(finalNumericUpDown.Value);			
			int ipCount = Decimal.ToInt16(countNumericUpDown.Value);

			IList<string> virtualIPList = new List<string>();
			IList<string> submaskList = new List<string>();		
			
			for (int i = 0; i < ipCount; i++)
			{
				virtualIPList.Add("192.168." + thirdIP + "." + firstIP++);
				submaskList.Add("255.255.255.0");
			}

			if (IPHelper.SetIPAddress(
					moList[netcardIndex],
					virtualIPList.ToArray(),
					submaskList.ToArray(),
					new string[] { "192.168." + thirdNumericUpDown.Value + ".1" },
					new string[] { "192.168." + thirdNumericUpDown.Value + ".1", "114.114.114.114" }))
			{
				mainForm.SetNetworkChangedTrue();
				setNotice("已设置多IP，请刷新。");
				setBusy(false);
			}
			else {				
				setNotice("设置多IP失败，已恢复初始配置，请刷新。");
				setBusy(false);
			}
		}

		/// <summary>
		/// 辅助方法：启用DHCP
		/// </summary>
		private void EnableDHCP(ManagementObject mo)
		{
			mo.InvokeMethod("SetDNSServerSearchOrder", null);
			mo.InvokeMethod("EnableDHCP", null);
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
					String carName = mo["Description"].ToString().Trim();
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
		/// 辅助方法：设置提示label的文本
		/// </summary>
		/// <param name="msg"></param>
		private void setNotice(string msg)
		{
			toolStripStatusLabel1.Text = msg;
			Refresh();
		}

		/// <summary>
		/// 辅助方法：设置是否忙时
		/// </summary>
		/// <param name="busy"></param>
		private void setBusy(bool busy) {
			this.Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
		}

	}
}
