using CCWin.SkinControl;
using LightController.Ast;
using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
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
		private MainFormBase mainForm;
		private string binPath;
		private bool isChooseFile = false; 

		private IList<string> selectedIPs;
		private IList<string> ips;
		private string localIP;
		private string comName;

		private ConnectTools connectTools;
		private SerialPortTools comTools;

		public HardwareUpdateForm(MainFormBase mainForm , string binPath) 
		{
			InitializeComponent();
			this.mainForm = mainForm;
			this.binPath = binPath;
			setPathLabel();

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
		private void getLocalIPsButton_Click(object sender, EventArgs e)
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
		/// 事件：当本地ip选项被改变后，设置localIP，并设置网络搜索框是否可用。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void localIPsComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			localIP = localIPsComboBox.Text;

			ipsComboBox.Text = "";
			ipsComboBox.SelectedIndex = -1;
			ipsComboBox.Enabled = false;

			networkSearchButton.Enabled = !String.IsNullOrEmpty(localIP);
		}		

		/// <summary>
		///事件：点击《搜索网络/串口设备》，两个按钮点击事件集成在一起
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void searchButton_Click(object sender, EventArgs e)
		{
			string buttonName =((Button)sender).Name;

			//点击《搜索网络设备》
			if (buttonName.Equals("networkSearchButton")) 
			{
				ipsComboBox.Items.Clear();
				ips = new List<string>();

				connectTools = ConnectTools.GetInstance();
				connectTools.Start(localIP);
				connectTools.SearchDevice();
				// 需要延迟片刻，才能找到设备;	故在此期间，主动暂停片刻
				Thread.Sleep(MainFormBase.NETWORK_WAITTIME);

				Dictionary<string, Dictionary<string, NetworkDeviceInfo>> allDevices = connectTools.GetDeivceInfos();
				foreach (KeyValuePair<string, NetworkDeviceInfo> d2 in allDevices[localIP])
				{
					ipsComboBox.Items.Add(d2.Value.DeviceName + "(" + d2.Value.DeviceIp + ")");
					ips.Add(d2.Value.DeviceIp);
				}

				if (ipsComboBox.Items.Count > 0)
				{
					ipsComboBox.SelectedIndex = 0;
					ipsComboBox.Enabled = true;
				}
				else
				{
					MessageBox.Show("未找到可用网络设备，请确定设备已连接后重试");
					ipsComboBox.SelectedIndex = -1;
					ipsComboBox.Enabled = false;
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
			comSearchButton.Enabled = false;
			comConnectButton.Enabled = false;
			comUpdateButton.Enabled = false;

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
				comConnectButton.Enabled = true;
			}
			else
			{
				comComboBox.Enabled = false;
				comComboBox.SelectedIndex = -1;
				comConnectButton.Enabled = false;
				MessageBox.Show("未找到可用串口，请重试");
			}
			comSearchButton.Enabled = true;
		}


		/// <summary>
		/// 事件：更改《网络设备列表》的选中项，则自动将即将连接的设备设为选中项。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkDevicesComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ipsComboBox.SelectedIndex == -1 || String.IsNullOrEmpty(ipsComboBox.Text)) {
				networkdUpdateButton.Enabled = false;
				return;
			}

			selectedIPs = new List<string>();
			selectedIPs.Add(ips[ipsComboBox.SelectedIndex]);
			networkdUpdateButton.Enabled = isChooseFile;
		}

		/// <summary>
		/// 事件：点击《打开串口》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comConnectButton_Click(object sender, EventArgs e)
		{
			comName = comComboBox.Text;
			MessageBox.Show("已打开串口：" + comName);
			comNameLabel.Text = comName;
			comTools.OpenCom(comName);
			comUpdateButton.Enabled = isChooseFile;
		}


		/// <summary>
		/// 事件：点击《下载数据》，两个按钮点击事件集成在一起
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateButton_Click(object sender, EventArgs e)
		{
			if (!isChooseFile) {
				MessageBox.Show("尚未选中xbin文件。");
				return;
			}

			string buttonName = ((Button)sender).Name;
			if (buttonName.Equals("networkdUpdateButton"))
			{
				networkdUpdateButton.Enabled = false;
				ipsComboBox.Enabled = false;
				connectTools.Connect(connectTools.GetDeivceInfos()[localIP][selectedIPs[0]]);
				connectTools.Update( selectedIPs , binPath,  new HardwareUpdateReceiveCallBack(this,true));
			}
			else {
				comTools.Update(binPath, new HardwareUpdateReceiveCallBack(this,false));
			}		
		}		
			   

		/// <summary>
		/// 事件：点击《选择升级文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fileOpenButton_Click(object sender, EventArgs e)
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
			binPath = openFileDialog.FileName;
			setPathLabel();		
		}

		/// <summary>
		///  辅助方法：设置label及其他选项
		/// </summary>
		/// <param name="binPath"></param>
		private void setPathLabel() {

			filePathLabel.Text =  binPath ;
			mainForm.SetBinPath(binPath);

			isChooseFile = !String.IsNullOrEmpty(binPath);
			networkdUpdateButton.Enabled = isChooseFile && !String.IsNullOrEmpty(ipsComboBox.Text);
			comUpdateButton.Enabled = isChooseFile && !String.IsNullOrEmpty(comName);	
		}

		private void HardwareUpdateForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show("此升级方式，是只适用于硬件出现重大问题时的解决方案，请谨慎使用！");
			e.Cancel = true;
		}


		/// <summary>
		///  辅助委托方法：将数据写进度条
		/// </summary>
		/// <param name="progressPercent"></param>		
		public void PaintProgress(bool isNetwork ,  int progressPercent)
		{
			if (isNetwork)
			{
				networkSkinProgressBar.Value = progressPercent;
			}
			else {
				comSkinProgressBar.Value = progressPercent;
			}		
		}	
	}

	internal class HardwareUpdateReceiveCallBack : ICommunicatorCallBack
	{
		private HardwareUpdateForm huForm;
		private bool isNetwork;
		public HardwareUpdateReceiveCallBack(HardwareUpdateForm huForm, bool isNetwork)
		{
			this.huForm = huForm;
			this.isNetwork = isNetwork;
		}

		public void Completed(string deviceTag)
		{
			MessageBox.Show("设备(" + deviceTag + ")升级成功");
		}

		public void Error(string deviceTag, string errorMessage)
		{
			MessageBox.Show("设备(" + deviceTag + ")升级出错。\n错误信息是：" + errorMessage);
		}

		public void GetParam(CSJ_Hardware hardware)
		{
			//throw new System.NotImplementedException();
		}

		public void UpdateProgress(string deviceTag, string fileName, int progressPercent)
		{
			huForm.PaintProgress(isNetwork, progressPercent);
		}
	}	


}
