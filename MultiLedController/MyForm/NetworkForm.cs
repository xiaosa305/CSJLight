//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Windows.Forms;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.IO;
using MultiLedController.Ast;
using System.Net;
using MultiLedController.Common;
using MultiLedController.Utils;
using MultiLedController.MyForm;

namespace MultiLedController
{
	public partial class NetworkForm : Form
	{		
		
		private IList<ManagementObject> moList;
		private IPAst tempIpAst;
		private int netcardIndex = -1;
		private MainForm mainForm;
		private IList<string> virtualIPList;

		private int ipCount = 9;

		public NetworkForm(MainForm mainForm)
		{
			InitializeComponent();
			refreshNetcard();
			this.mainForm = mainForm;
		}

		public NetworkForm(MainForm mainForm, int ipLast, int interfaceCount) 
		{
			InitializeComponent();
			refreshNetcard();
			this.mainForm = mainForm;
			
			numericUpDown1.Value = ipLast ;
			ipCount = interfaceCount + 1;
			numericUpDown2.Value = ipCount;		
			
		}

		private void NetworkForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
		}

		private void refreshNetcardButton_Click(object sender, EventArgs e)
		{
			refreshNetcard();
			setStatusLabel("已刷新网卡列表");
		}

		private void refreshNetcard()	{

			clearIPList();

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
		/// 事件：点击《设置连续IP地址》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void set9IPButton_Click(object sender, EventArgs e)
		{
			if (tempIpAst == null) {
				DialogResult dr = MessageBox.Show(
					"尚未存储当前IP配置，是否存储设置后再进行设置？", 
					"继续设置？",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Warning);

				if (dr == DialogResult.Cancel)
				{
					setStatusLabel("你已取消多IP设置");
					return;
				}
				else if(dr == DialogResult.OK){
					saveButton_Click(null, null);					
				}
			}

			int firstIP =Decimal.ToInt32( numericUpDown1.Value);
			virtualIPList = new List<string>();
			IList<string> submaskList = new List<string>();
			firstIP = Decimal.ToInt32(numericUpDown1.Value);
			for (int i = 0; i < ipCount; i++)
			{
				virtualIPList.Add("192.168.31." + firstIP++);
				submaskList.Add("255.255.255.0");
			}

			SetIPAddress(virtualIPList.ToArray(),
				submaskList.ToArray(),
				new string[] { "192.168.31.1" }, 
				new string[] { "192.168.31.1","114.114.114.114" });

			setStatusLabel("已设置多IP，请刷新");
			setAddButtonEnable(true);
		}


		private void setAddButtonEnable(bool v)
		{
			this.addVirtualIpButton.Enabled = v;
		}

		/// <summary>
		/// 辅助方法：清空所有ip列表
		/// </summary>
		private void clearIPList()
		{
			netcardComboBox.Items.Clear();
			netcardComboBox.Text = "";
			netcardComboBox.SelectedIndex = -1;

			moList = null;
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
				setStatusLabel("未选中可用网卡，请刷新后重试");
				return;
			}
			ManagementObject mo = moList[netcardIndex];
			EnableDHCP(mo);
			
		}

		private void setStatusLabel(string msg) {
			toolStripStatusLabel1.Text = msg;
		}

		/// <summary>
		/// 辅助方法：启用DHCP
		/// </summary>
		private void EnableDHCP(ManagementObject mo)
		{
			mo.InvokeMethod("SetDNSServerSearchOrder", null);
			mo.InvokeMethod("EnableDHCP", null);

			mainForm.ClearIPListView();
			setStatusLabel("已启用DHCP，请刷新");
		}

		/// <summary>
		/// 设置IP地址，掩码，网关和DNS
		/// </summary>
		/// <param name="ipArray">ip列表：要设置的ip列表</param>
		/// <param name="submaskArray">子网掩码列表：有多少个ip，就要对应多少个子网掩码</param>
		/// <param name="gateway">网关列表：与ip独立，只需设置一次即可</param>
		/// <param name="dns">dns列表：有多少个DNS，就设置几个</param>
		private void SetIPAddress(string[] ipArray, string[] submaskArray, string[] gatewayArray, string[] dnsArray)
		{
			ManagementClass wmi = new ManagementClass("Win32_NetworkAdapterConfiguration");
			ManagementObjectCollection moc = wmi.GetInstances();
			ManagementBaseObject inPar = null;
			ManagementBaseObject outPar = null;

			ManagementObject mo = moList[netcardIndex];			
			
			//设置IP地址和掩码
			if (ipArray != null && submaskArray != null)
			{
					inPar = mo.GetMethodParameters("EnableStatic");
					inPar["IPAddress"] = ipArray;
					inPar["SubnetMask"] = submaskArray;
					outPar = mo.InvokeMethod("EnableStatic", inPar, null);
			}

			//设置网关地址
			if (gatewayArray != null)
			{
				inPar = mo.GetMethodParameters("SetGateways");
				inPar["DefaultIPGateway"] = gatewayArray;
				outPar = mo.InvokeMethod("SetGateways", inPar, null);
			}

			//设置DNS地址
			if (dnsArray != null)
			{
				inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
				inPar["DNSServerSearchOrder"] = dnsArray;
				outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
			}				
		}

		/// <summary>
		/// 辅助方法：通过ipAst来设置本地ip
		/// </summary>
		/// <param name="ipAst"></param>
		private void SetIPAddress(IPAst ipAst)
		{
			SetIPAddress(ipAst.IpArray, ipAst.SubmaskArray, ipAst.GatewayArray, ipAst.DnsArray);
		}

		/// <summary>
		/// 事件：选中不同网卡(ipComboBox)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ipComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			netcardIndex = netcardComboBox.SelectedIndex;
			if (netcardIndex > -1) {
				IPAst ipAst = new IPAst(moList[netcardIndex]);
				try
				{
					ipLabel2.Text = ipAst.IpArray[0];
				}
				catch (Exception) { }

				try
				{
					submaskLabel2.Text = ipAst.SubmaskArray[0];
				}
				catch (Exception) { }

				try
				{
					gatewayLabel2.Text = ipAst.GatewayArray[0];
					
				}
				catch (Exception) { }

				try
				{
					dnsLabel2.Text = ipAst.DnsArray[0];
				}
				catch (Exception) { }

			}
		}

		private void testButton_Click(object sender, EventArgs e)
		{
            XiaosaTest.Test1();
		}

		/// <summary>
		/// 保存初始设置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			tempIpAst = new IPAst(moList[netcardIndex]);			
			loadButton.Enabled = tempIpAst != null;
		}

		/// <summary>
		/// 恢复初始设置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loadButton_Click(object sender, EventArgs e)
		{
			SetIPAddress(tempIpAst);
			setStatusLabel("已成功恢复保存的设置，请刷新");
		}

		/// <summary>
		/// 事件：点击《添加到主界面》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addVirtualIpButton_Click(object sender, EventArgs e)
		{
			mainForm.AddVirtualIPS(virtualIPList);
		}



		private void numericUpDown2_ValueChanged(object sender, EventArgs e)
		{
			this.ipCount = Decimal.ToInt16(numericUpDown2.Value);

		}
	}
}
