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

namespace MultiLedController.MyForm
{
	public partial class MainForm3 : Form
	{
		private bool isFirstTime = true; //只用一次的方法，避免每次激活都跑一次刷新			

		private ManagementObject mo; //存放当前网卡的mo对象
		private string mainIP;  //当前网卡的主IP(第一个设的IP，mo对象取回来时在最后面)
		private string mainMask; // 当前网卡的主掩码
		private IList<string> vipList; //当前网卡的虚拟IP列表（不包含主ip）

		private bool isStart = false; //是否启动模拟
		private bool isDebuging = false;  //是否正在调试
		private bool isRecording = false; // 是否正在录制
		private string recordPath = "C:\\Temp\\CSJ_SC"; //录制文件存储路径
		private int recordIndex = 0; //录制文件序号
				
		public MainForm3()
		{
			InitializeComponent();			

			string version = "3.0.0.815"; 
			Text += " v" + version + " beta";

			// 设两个可调节ComboBox的默认值
			interfaceCountComboBox.SelectedIndex = 0;
			spaceCountComboBox.SelectedIndex = 0;

		
		}

		private void MainForm3_Load(object sender, EventArgs e)
		{
			setRecordPathLabel();
			recordTextBox.Text = transformRecordIndex(recordIndex);

			//为recordTextBox添加失去焦点的监听事件（没有在VS中）
			recordTextBox.LostFocus += new EventHandler(recordTextBox_LostFocus);
		}

		/// <summary>
		///  事件：只在首次激活时，跑一次刷新网卡列表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm3_Activated(object sender, EventArgs e)
		{
			if (isFirstTime)
			{
				refreshNetcardList();
				isFirstTime = false;
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
			//mo = null;

			//mainIP = null;
			//mainMask = null;
			//ipLabel2.Text = "";
			//submaskLabel2.Text = "";
			//gatewayLabel2.Text = "";
			//dnsLabel2.Text = "";

			//vipList = null;
			//virtualIPListView.Items.Clear();

			//deviceList = null;
			//deviceListView.Items.Clear();
			//choosenIndexList = null;
			//choosenListView.Items.Clear();

			//searchButton.Enabled = false;
			startButton.Enabled = false;
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

					setNotice(1, "已刷新当前网卡信息。", false);
				}
			}
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

			int interfaceCount = int.Parse( interfaceCountComboBox.Text );
			int spaceCount = int.Parse(spaceCountComboBox.Text) / 170;
			int maxControllerCount = 256 / interfaceCount / spaceCount;
			controllerCountNUD.Maximum = maxControllerCount;			
		
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
				item.SubItems[3].Text = "";
			}
			Refresh();
		}

		#endregion

		#region 录制相关

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
				setNotice(2, "录制文件序号不得大于999。", true);
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

		#endregion

		#region 全局方法

		/// <summary>
		/// 辅助方法：设置提示信息
		/// </summary>
		/// <param name="place">1 | 2 左1右2</param>
		/// <param name="msg"></param>
		private void setNotice(int place, string msg, bool msgShow)
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

		/// <summary>
		/// 辅助方法：设置是否忙时
		/// </summary>
		/// <param name="v"></param>
		private void setBusy(bool busy)
		{
			Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
			Enabled = !busy;
		}

		#endregion
			
	}
}
