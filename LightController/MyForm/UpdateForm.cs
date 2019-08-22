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

		private ConnectTools cTools;
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
		}

		/// <summary>
		/// 事件：点击《获取本地ip列表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void getLocalIPsSkinButton_Click(object sender, EventArgs e)
		{
			IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
			networkDevicesComboBox.Items.Clear();
			foreach (IPAddress ip in ipe.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork) //当前ip为ipv4时，才加入到列表中
				{
					localIPSComboBox.Items.Add(ip);
				}				
			}
			if (localIPSComboBox.Items.Count > 0)
			{
				localIPSComboBox.Enabled = true;
				localIPSComboBox.SelectedIndex = 0;
				setLocalIPSkinButton.Enabled = true;
			}
			else {
				localIPSComboBox.Enabled = false;
				localIPSComboBox.SelectedIndex = -1;				
				setLocalIPSkinButton.Enabled = false;
			}					
		}

		/// <summary>
		///  事件：点击《设置本地IP》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setLocalIPSkinButton_Click(object sender, EventArgs e)
		{
			localIP = localIPSComboBox.Text;
			localIPLabel.Text = localIP;
			networkSearchSkinButton.Enabled = true;
		}

		/// <summary>
		///事件：点击《搜索网络/串口设备》，两个按钮点击事件集成在一起
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void searchButton_Click(object sender, EventArgs e)
		{
			string buttonName =((SkinButton)sender).Name;
			if (buttonName.Equals("networkSearchSkinButton")) //搜索网络设备
			{
				cTools = ConnectTools.GetInstance();
				cTools.Start(localIP);
				cTools.SearchDevice();
				// 需要延迟片刻，才能找到设备;	故在此期间，主动暂停一秒
				networkConnectSkinButton.Enabled = false;
				Thread.Sleep(1000);

				Dictionary<string, string> allDevices = cTools.GetDeviceInfo();
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
					networkConnectSkinButton.Enabled = true;
				}
				else
				{
					networkDevicesComboBox.Enabled = false;
					networkDevicesComboBox.SelectedIndex = -1;
					networkConnectSkinButton.Enabled = false;
					MessageBox.Show("未找到可用设备，请确认后重试。");
				}
			}
			else {


			}


		
		}

		/// <summary>
		/// 事件：点击《选择设备》、《选择串口》，两个按钮点击事件集成在一起
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectButton_Click(object sender, EventArgs e)
		{
			selectedIPs = new List<string>();
			selectedIPs.Add(ips[networkDevicesComboBox.SelectedIndex]);
			MessageBox.Show("设备连接成功");
			networkdUpdateSkinButton.Enabled = true;
		}


		/// <summary>
		/// 事件：点击《下载数据》，两个按钮点击事件集成在一起
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateButton_Click(object sender, EventArgs e)
		{
			string buttonName = ((SkinButton)sender).Name;

			ConnectTools cTools = ConnectTools.GetInstance();
			cTools.Download(selectedIPs, dbWrapper, globalSetPath, new DownloadReceiveCallBack(), new DownloadProgressDelegate(paintProgress));
			//MessageBox.Show("断开连接");
			networkConnectSkinButton.Enabled = false;
			networkdUpdateSkinButton.Enabled = false;
			networkDevicesComboBox.Items.Clear();
			networkDevicesComboBox.Text = "";
		}

		/// <summary>
		///  辅助委托方法：将数据写进度条
		/// </summary>
		/// <param name="a"></param>		
		void paintProgress(string fileName,int a)
		{
			currentFileLabel.Text = fileName;
			networkSkinProgressBar.Value =  a;				
		}

	
	}


	public class DownloadReceiveCallBack : IReceiveCallBack
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
}
