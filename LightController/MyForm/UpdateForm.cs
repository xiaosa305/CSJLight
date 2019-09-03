using CCWin.SkinControl;
using LighEditor.Ast;
using LighEditor.Tools;
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

namespace LighEditor.MyForm
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
				setLocalIPSkinButton.Enabled = true;
			}
			else {
				localIPsComboBox.Enabled = false;
				localIPsComboBox.SelectedIndex = -1;				
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
			localIP = localIPsComboBox.Text;
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
			//点击《搜索网络设备》
			if (buttonName.Equals("networkSearchSkinButton")) 
			{
				cTools = ConnectTools.GetInstance();
				cTools.Start(localIP);
				cTools.SearchDevice();
				// 需要延迟片刻，才能找到设备;	故在此期间，主动暂停一秒
				networkChooseSkinButton.Enabled = false;
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
					networkChooseSkinButton.Enabled = true;
				}
				else
				{
					networkDevicesComboBox.Enabled = false;
					networkDevicesComboBox.SelectedIndex = -1;
					networkChooseSkinButton.Enabled = false;
					MessageBox.Show("未找到可用设备，请确认后重试。");
				}
			}
			// 点击《搜索串口设备》
			else {

				comSearchSkinButton.Enabled = false;
				comChooseSkinButton.Enabled = false;
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
					comChooseSkinButton.Enabled = true;
				}
				else
				{
					comComboBox.Enabled = false ;
					comComboBox.SelectedIndex = 0;
					comChooseSkinButton.Enabled = false;
					MessageBox.Show("未找到可用串口，请重试");
				}
				comSearchSkinButton.Enabled = true;

			}


		
		}

		/// <summary>
		/// 事件：点击《选择网络设备》、《选择串口》，两个按钮点击事件集成在一起
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void choosetButton_Click(object sender, EventArgs e)
		{
			string buttonName = ((SkinButton)sender).Name;
			if (buttonName.Equals("networkChooseSkinButton"))
			{
				selectedIPs = new List<string>();
				selectedIPs.Add(ips[networkDevicesComboBox.SelectedIndex]);
				MessageBox.Show("已选中网络设备");
				networkdUpdateSkinButton.Enabled = true;
			}
			else {
				comName = comComboBox.Text ;
				MessageBox.Show("已选中串口设备" + comName);
				comNameLabel.Text = comName;
				comTools.OpenCom(comName);
				comUpdateSkinButton.Enabled = true;
			}			
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
				networkChooseSkinButton.Enabled = false;
				networkdUpdateSkinButton.Enabled = false;
				networkDevicesComboBox.Enabled = false;

				cTools.Download(selectedIPs, dbWrapper, globalSetPath, new NetworkDownloadReceiveCallBack(), new DownloadProgressDelegate(paintProgress));								
			}
			else {
				comTools.DownloadProject(dbWrapper, globalSetPath, new ComDownloadReceiveCallBack(), new DownloadProgressDelegate(paintProgress2));
			}
		
		}

		/// <summary>
		///  辅助委托方法：将数据写进度条
		/// </summary>
		/// <param name="a"></param>		
		void paintProgress(string fileName,int a)
		{
			networkCurrentFileLabel.Text = fileName;
			networkSkinProgressBar.Value =  a;				
		}

		/// <summary>
		///  辅助委托方法：将数据写进度条
		/// </summary>
		/// <param name="a"></param>		
		void paintProgress2(string fileName, int a)
		{
			comCurrentFileLabel.Text = fileName;
			comSkinProgressBar.Value = a;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			localIPsComboBox.Enabled = false;
			localIPsComboBox.Items.Clear();
			localIPsComboBox.SelectedIndex = -1;
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
