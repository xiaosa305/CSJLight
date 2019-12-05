using CCWin.SkinControl;
using LightController.Ast;
using LightController.Tools;
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

namespace LightController.MyForm
{
	public partial class UpdateForm : Form
	{
		private MainFormInterface mainForm;
		private DBWrapper dbWrapper;
		private string globalSetPath;

		private IList<string> selectedIPs;
		private IList<string> ips;
		private string localIP;
		private string comName;

		private ConnectTools connectTools;
		private SerialPortTools comTools;

		public UpdateForm(MainFormInterface mainForm,DBWrapper dbWrapper,string globalSetPath)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			this.dbWrapper = dbWrapper;
			this.globalSetPath = globalSetPath;

			this.skinTabControl.SelectedIndex = 0;			
		}

		private void UpdateForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			// 设false可在其他文件中修改本类的UI
			Control.CheckForIllegalCrossThreadCalls = false;

			// 9.7 进来就自动搜索本地IP列表。
			getLocalIPs();
			searchCOMList();
		}

		/// <summary>
		/// 事件：点击《获取本地ip列表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void getLocalIPsSkinButton_Click(object sender, EventArgs e)
		{
			getLocalIPs();
		}

		/// <summary>
		///  辅助方法：获取本地IP列表，①开始进来就搜到 ； ②点击后重新搜索；
		/// </summary>
		private void getLocalIPs() {
			IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
			localIPsComboBox.Items.Clear();
			foreach (IPAddress ip in ipe.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork) //当前ip为ipv4时，才加入到列表中
				{
					localIPsComboBox.Items.Add(ip);
				}
			}
			if (localIPsComboBox.Items.Count > 0)
			{
				localIPsComboBox.Enabled = true;
				localIPsComboBox.SelectedIndex = 0;
			}
			else
			{
				localIPsComboBox.Enabled = false;
				localIPsComboBox.SelectedIndex = -1;
			}
		}

		/// <summary>
		/// 辅助方法：改变了本地ip选项后，设置localIP的值，并清空 网络设备的选项 ；若localIP为空，则不可搜索。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void localIPsComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			localIP = localIPsComboBox.Text;

			networkDevicesComboBox.Text = "";
			networkDevicesComboBox.SelectedIndex = -1;
			networkDevicesComboBox.Enabled = false;

			networkSearchSkinButton.Enabled = !String.IsNullOrEmpty(localIP);
		}
			   		

		/// <summary>
		///事件：点击《搜索网络/串口设备》，两个按钮点击事件集成在一起
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void searchButton_Click(object sender, EventArgs e)
		{
			string buttonName =((SkinButton)sender).Name;
			//点击《搜索网络设备》
			if (buttonName.Equals("networkSearchSkinButton")) 
			{
				connectTools = ConnectTools.GetInstance();
				connectTools.Start(localIP);
				connectTools.SearchDevice();
				// 需要延迟片刻，才能找到设备;	故在此期间，主动暂停一秒
				Thread.Sleep(1000);

				Dictionary<string, string> allDevices = connectTools.GetDeviceInfo();
				networkDevicesComboBox.Items.Clear();
				ips = new List<string>();
				if (allDevices.Count > 0)
				{
					foreach (KeyValuePair<string, string> device in allDevices)
					{
						networkDevicesComboBox.Items.Add(device.Value + "(" + device.Key + ")");
						ips.Add(device.Key);
					}
					networkDevicesComboBox.Enabled = true;
					networkDevicesComboBox.SelectedIndex = 0;
				}
				else
				{
					networkDevicesComboBox.Enabled = false;
					networkDevicesComboBox.SelectedIndex = -1;
					MessageBox.Show("未找到可用设备，请确认后重试。");
				}
			}
			// 点击《搜索串口设备》
			else {
				searchCOMList();
			}		
		}

		/// <summary>
		/// 辅助方法：搜索本机连接的串口列表：①load时使用；②点击《搜索串口》时
		/// </summary>
		private void searchCOMList()
		{
			comSearchSkinButton.Enabled = false;
			comOpenSkinButton.Enabled = false;
			comUpdateSkinButton.Enabled = false;

			comTools = SerialPortTools.GetInstance();
			string[] comList = comTools.GetSerialPortNameList();
			comComboBox.Items.Clear();
			if (comList.Length > 0)
			{
				foreach (string com in comList)
				{
					comComboBox.Items.Add(com);
				}
				comComboBox.Enabled = true;
				comComboBox.SelectedIndex = 0;
				comOpenSkinButton.Enabled = true;
			}
			else
			{
				comComboBox.Enabled = false;
				comComboBox.SelectedIndex = -1;
				comOpenSkinButton.Enabled = false;
				MessageBox.Show("未找到可用串口，请重试");
			}
			comSearchSkinButton.Enabled = true;
		}



		/// <summary>
		/// 辅助方法：修改网络设备的选项之后，设置相关的值。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkDevicesComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (networkDevicesComboBox.SelectedIndex == -1 || String.IsNullOrEmpty(networkDevicesComboBox.Text))
			{
				//MessageBox.Show(" --------- ");
				networkdUpdateSkinButton.Enabled = false;
				return;
			}

			selectedIPs = new List<string>();
			selectedIPs.Add(ips[networkDevicesComboBox.SelectedIndex]);
			networkdUpdateSkinButton.Enabled = true;
		}

		/// <summary>
		/// 事件：点击《《选择串口》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void choosetButton_Click(object sender, EventArgs e)
		{
			comName = comComboBox.Text ;
			MessageBox.Show("已选中串口设备" + comName);
			comNameLabel.Text = comName;
			comTools.OpenCom(comName);
			comUpdateSkinButton.Enabled = true;			
		}


		/// <summary>
		/// 事件：点击《下载数据》，两个按钮点击事件集成在一起
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateButton_Click(object sender, EventArgs e)
		{
			string buttonName = ((SkinButton)sender).Name;

			if (buttonName.Equals("networkdUpdateSkinButton"))
			{
				networkdUpdateSkinButton.Enabled = false;
				networkDevicesComboBox.Enabled = false;
				//MessageBox.Show(localIP + " ---> " + selectedIPs[0]);
				connectTools.Download(selectedIPs, dbWrapper, globalSetPath, new NetworkDownloadReceiveCallBack(), new DownloadProgressDelegate(networkPaintProgress));								
			}
			else {
				comTools.DownloadProject(dbWrapper, globalSetPath, new ComDownloadReceiveCallBack(), new DownloadProgressDelegate(comPaintProgress));
			}		
		}

		/// <summary>
		///  辅助委托方法：将数据写进度条
		/// </summary>
		/// <param name="a"></param>		
		void networkPaintProgress(string fileName,int a)
		{
			networkFileShowLabel.Text = fileName;
			networkSkinProgressBar.Value =  a;		
		}

		/// <summary>
		///  辅助委托方法：将数据写进度条
		/// </summary>
		/// <param name="a"></param>		
		void comPaintProgress(string fileName, int a)
		{
			comFileShowLabel.Text = fileName;
			comSkinProgressBar.Value = a;
		}		
	}


	public class NetworkDownloadReceiveCallBack : IReceiveCallBack
	{
		public void SendCompleted(string deviceName, string order)
		{
			MessageBox.Show("设备：" + deviceName + "  下载成功并断开连接"
				//+"发回了命令："+order 
				);
		}
		public void SendError(string deviceName, string order)
		{
			MessageBox.Show("设备：" + deviceName + " 下载失败并断开连接" 
				//+ "发回了命令：" + order 
				);
		}
	}

	public class ComDownloadReceiveCallBack : IReceiveCallBack
	{
		public void SendCompleted(string deviceName, string order)
		{
			MessageBox.Show("下载成功");
		}
		public void SendError(string deviceName, string order)
		{
			MessageBox.Show("下载失败");
		}
	}
}
