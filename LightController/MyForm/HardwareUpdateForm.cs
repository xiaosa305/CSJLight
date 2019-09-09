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
	public partial class HardwareUpdateForm : Form
	{
		private MainFormInterface mainForm;
		private string filePath;
		private bool isChooseFile = false; 

		private IList<string> selectedIPs;
		private IList<string> ips;
		private string localIP;
		private string comName;

		private ConnectTools cTools;
		private SerialPortTools comTools;

		public HardwareUpdateForm(MainFormInterface mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;

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
				setLocalIPSkinButton.Enabled = true;
			}
			else
			{
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
				searchCOMList();
			}		
		}

		/// <summary>
		/// 辅助方法：搜索本机连接的串口列表：①load时使用；②点击《搜索串口》时
		/// </summary>
		private void searchCOMList()
		{
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
				comComboBox.Enabled = false;
				comComboBox.SelectedIndex = 0;
				comChooseSkinButton.Enabled = false;
				MessageBox.Show("未找到可用串口，请重试");
			}
			comSearchSkinButton.Enabled = true;
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
				if (isChooseFile)
				{					
					networkdUpdateSkinButton.Enabled = true;
				}
				else {
					MessageBox.Show("请先选择升级文件,再重新点击本按钮。");
					networkdUpdateSkinButton.Enabled = false;
				}
				
			}
			else {
				comName = comComboBox.Text ;
				MessageBox.Show("已选中串口设备" + comName);
				comNameLabel.Text = comName;
				comTools.OpenCom(comName);
				if (isChooseFile)
				{					
					comUpdateSkinButton.Enabled = true;
				}
				else
				{
					MessageBox.Show("请先选择升级文件,再重新点击本按钮。");
					comUpdateSkinButton.Enabled = false;
				}				
			}

			/// <summary>
			///  辅助委托方法：将数据写进度条
			/// </summary>
			/// <param name="a"></param>		
			void networkPaintProgress(string fileName, int a)
			{
				networkSkinProgressBar.Value = a;
			}

			/// <summary>
			///  辅助委托方法：将数据写进度条
			/// </summary>
			/// <param name="a"></param>		
			void comPaintProgress(string fileName, int a)
			{
				comSkinProgressBar.Value = a;
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

				cTools.Update(selectedIPs, filePath, new NetworkDownloadReceiveCallBack());
			}
			else {
				comTools.Update(filePath, new ComDownloadReceiveCallBack() );
			}		
		}		
			   

		/// <summary>
		/// 事件：点击《选择升级文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fileOpenSkinButton_Click(object sender, EventArgs e)
		{
			openFileDialog.ShowDialog();
		}

		/// <summary>
		///  事件：在《打开灯具》对话框内选择文件，并点击确认时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			filePath = openFileDialog.FileName;
			filePathLabel.Text = "选中文件：" + filePath;
			if (!String.IsNullOrEmpty(filePath))
			{
				isChooseFile = true;
			}
			else {
				isChooseFile = false;
				networkdUpdateSkinButton.Enabled = false;
				comUpdateSkinButton.Enabled = false;
			}
		}

		private void HardwareUpdateForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show("此升级方式，是只适用于硬件出现重大问题时的解决方案，请谨慎使用！");
			e.Cancel = true;
		}
	}

}
