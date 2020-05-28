using CCWin.SkinControl;
using LightController.Ast;
using LightController.Common;
using LightController.PeripheralDevice;
using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.MyForm
{
	public partial class ProjectUpdateForm : Form
	{
		private MainFormBase mainForm;
		private DBWrapper dbWrapper;
		private string projectPath;
		private string globalSetPath;

		private IList<string> selectedIPs;
		private IList<string> ips;
		private string localIP;
		private string comName;

		private ConnectTools connectTools;
		private SerialPortTools comTools;
		private bool isComConnected = false;		

		public ProjectUpdateForm(MainFormBase mainForm, DBWrapper dbWrapper, string globalSetPath, string projectPath)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			this.dbWrapper = dbWrapper;
			this.globalSetPath = globalSetPath;
			this.projectPath = projectPath;
			pathLabel.Text = projectPath;

			this.skinTabControl.SelectedIndex = 0;
		}

		private void ProjectUpdateForm_Load(object sender, EventArgs e)
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
			string buttonName = ((Button)sender).Name;
			if (buttonName.Equals("networkSearchButton"))
			{
				RefreshNetworkDevice();
			}			
			else {
				searchCOMList();
			}
		}

		/// <summary>
		/// 辅助方法：刷新《网络设备》列表
		/// </summary>
		private void RefreshNetworkDevice() {

			ipsComboBox.Items.Clear();
			ips = new List<string>();

			connectTools = ConnectTools.GetInstance();
			connectTools.Start(localIP);
			connectTools.SearchDevice();
			// 需要延迟片刻，才能找到设备;	故在此期间，主动暂停一秒
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

		/// <summary>
		/// 辅助方法：搜索本机连接的串口列表：①load时使用；②点击《搜索串口》时
		/// </summary>
		private void searchCOMList()
		{
			comSearchButton.Enabled = false;
			comOpenButton.Enabled = false;
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
				comOpenButton.Enabled = true;
			}
			else
			{
				comComboBox.Enabled = false;
				comComboBox.SelectedIndex = -1;
				comOpenButton.Enabled = false;
				MessageBox.Show("未找到可用串口，请重试");
			}
			comSearchButton.Enabled = true;
		}

		/// <summary>
		/// 辅助方法：修改网络设备的选项之后，设置相关的值。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkDevicesComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ipsComboBox.SelectedIndex == -1 || String.IsNullOrEmpty(ipsComboBox.Text))
			{				
				networkUpdateButton.Enabled = false;
				return;
			}

			selectedIPs = new List<string>();
			selectedIPs.Add(ips[ipsComboBox.SelectedIndex]);
			networkUpdateButton.Enabled = true;
		}

		/// <summary>
		/// 事件：点击《《打开|关闭串口》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comOpenButton_Click(object sender, EventArgs e)
		{
			isComConnected = !isComConnected;
			if (isComConnected)
			{
				comName = comComboBox.Text;
				comTools.OpenCom(comName);
			}
			else
			{
				comTools.CloseDevice();
			}
			comSearchButton.Enabled = !isComConnected;
			comComboBox.Enabled = !isComConnected;
			comNameLabel.Text = isComConnected ? comName : "";
			comOpenButton.Text = isComConnected ? "关闭串口" : "打开串口";
			comUpdateButton.Enabled = isComConnected; //只需满足"串口已打开"即可进行升级
			MessageBox.Show((isComConnected ? "已打开串口" : "已关闭串口") + comName);
		}

		/// <summary>
		/// 事件：点击《下载数据》，两个按钮点击事件集成在一起
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateButton_Click(object sender, EventArgs e)
		{
			SetBusy(true);
			bool generateNow = false;
			if (String.IsNullOrEmpty(projectPath))
			{
				DialogResult dr = MessageBox.Show("检查到您未选中已导出的工程文件夹，如继续操作会实时生成数据(将消耗较长时间)，是否继续？",
					"下载工程?",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question);
				if (dr == DialogResult.Cancel)
				{
					SetBusy(false);
					return;
				}

				if (dbWrapper.lightList == null || dbWrapper.lightList.Count == 0)
				{
					MessageBox.Show("当前工程无灯具，无法更新工程。");
					SetBusy(false);
					return;
				}
				generateNow = true;//只有当前无projectPath且选择继续后会rightNow
			}
			//若用户选择了已存在目录，则需要验证是否空目录
			else {
				if (Directory.GetFiles(projectPath).Length == 0)
				{
					MessageBox.Show("所选目录为空,无法下载工程。请选择正确的已有工程目录，并重新下载。");
					SetBusy(false);
					return;
				}
			}

			bool isNetwork = ((Button)sender).Name.Equals("networkUpdateButton");
			//使用串口升级，需要先打开串口，避免出错
			if( !isNetwork ) 
			{
				comTools.OpenCom(comName);				
			}

			if (generateNow)
			{
				SetLabelText(isNetwork, "正在实时生成工程数据，请耐心等待...");
				DataConvertUtils.SaveProjectFile(dbWrapper, mainForm, globalSetPath, new GenerateProjectCallBack(this, isNetwork));
			}
			else
			{
				FileUtils.CopyFileToDownloadDir(projectPath);
				DownloadProject(isNetwork);
			}
		}

		/// <summary>
		/// 辅助方法：将本地的工程文件，传送到设备中
		/// </summary>
		private void DownloadProject() {

			myConnect.DownloadProject(    );
		}


		/// <summary>
		/// 辅助方法：显示提示消息
		/// </summary>
		/// <param name="busy"></param>
		public void SetBusy( bool busy )
		{
			Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
			fileOpenButton.Enabled = !busy;
			clearButton.Enabled = !busy;
			skinTabControl.Enabled = !busy;
		}

		/// <summary>
		/// 辅助方法：下载工程
		/// </summary>
		/// <param name="isNetwork"></param>
		public void DownloadProject(bool isNetwork) {	
			if (isNetwork)
			{
				if (connectTools.Connect(connectTools.GetDeivceInfos()[localIP][selectedIPs[0]])) {					
					connectTools.Download(selectedIPs, dbWrapper, globalSetPath, new NetworkDownloadReceiveCallBack(this));
				}
				else
				{
					MessageBox.Show("网络设备连接失败，无法下载工程。");
				}				
			}
			else {
				comTools.DownloadProject(dbWrapper, globalSetPath, new ComDownloadReceiveCallBack(this));
			}
		}

		/// <summary>
		///  辅助委托方法：将数据写进度条
		/// </summary>
		/// <param name="processPercent"></param>		
		public void networkPaintProgress(string fileName,int processPercent)
		{
			networkFileShowLabel.Text = string.IsNullOrEmpty(fileName) ? "" : "正在传输文件：" + fileName;
			networkSkinProgressBar.Value =  processPercent;		
		}

		/// <summary>
		///  辅助委托方法：将数据写进度条
		/// </summary>
		/// <param name="processPercent"></param>		
		public void comPaintProgress(string fileName, int processPercent)
		{
			comFileShowLabel.Text = string.IsNullOrEmpty(fileName)?"" : "正在传输文件：" + fileName;
			comSkinProgressBar.Value = processPercent;
		}
			   
		/// <summary>
		/// 事件：点击《选择已有工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fileOpenSkinButton_Click(object sender, EventArgs e)
		{
			DialogResult dr = folderBrowserDialog.ShowDialog();
			projectPath = folderBrowserDialog.SelectedPath;
			pathLabel.Text = projectPath;
			mainForm.SetProjectPath(projectPath);
		}

		/// <summary>
		/// 事件：点击《清空》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void clearSkinButton_Click(object sender, EventArgs e)
		{
			projectPath = null;
			pathLabel.Text = null;
			mainForm.SetProjectPath(null);
		}

		/// <summary>
		/// 辅助方法：设置 当前更新文件Label的 文字
		/// </summary>
		/// <param name="isNetwork"></param>
		/// <param name="msg"></param>
		internal void SetLabelText(bool isNetwork,string msg)
		{
			if (isNetwork)
			{
				networkFileShowLabel.Text = msg;
			}
			else {
				comFileShowLabel.Text = msg;
			}
		}

		internal void ClearNetworkDevices()
		{
			ipsComboBox.Text = "";
			ipsComboBox.Enabled = false;
			ips = new List<string>();
		}

		/// <summary>
		/// 辅助方法：数据生成后，会把所有的文件放到destDir中，我们生成的Source也要压缩到这里来（Source.zip）
		/// </summary>
		/// <param name="zipPath"></param>
		public void GenerateSourceZip(string zipPath)
		{
			if (mainForm.GenerateSourceProject())
			{						
				ZipHelper.CompressAllToZip(mainForm.SavePath + @"\Source", zipPath, 9, null, mainForm.SavePath + @"\");							
			}
		}

		/// <summary>
		/// 事件：《窗口关闭》时，主动关闭串口连接
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProjectUpdateForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (isComConnected)
			{
				comTools.CloseDevice();
			}
			Dispose();
			mainForm.Activate();
		}

		#region 新连接方式

		private BaseCommunication myConnect; // 保持着一个设备连接（串网口通用）
		private bool isConnected = false; //是否连接
		private bool isConnectCom = true; //是否串口连接
		private IList<NetworkDeviceInfo> networkDeviceList;  // 网络设备的列表			
		
		/// <summary>
		/// 事件：点击《更换连接方式》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void switchButton_Click(object sender, EventArgs e)
		{
			isConnectCom = !isConnectCom;
			switchButton.Text = isConnectCom ? "切换为网络连接" : "切换为串口连接";
			refreshButton.Text = isConnectCom ? "刷新串口" : "刷新网络";
			deviceConnectButton.Text = isConnectCom ? "打开串口" : "连接设备";
			refreshDeviceComboBox(); // switchButton_Click
		}

		/// <summary>
		/// 辅助方法：刷新deviceComboBox(设备列表），区分不同的连接方法。
		/// </summary>
		private void refreshDeviceComboBox()
		{
			// 刷新前，先清空列表(也先断开连接：只是保护性再跑一次)
			if (isConnected)
			{
				disConnect(); // refreshDeviceComboBox
			}

			disableDeviceComboBox();
			deviceConnectButton.Enabled = false;
			Refresh();

			// 获取串口列表（不代表一定能连上，串口需用户自行确认）
			if (isConnectCom)
			{
				if (myConnect == null)
				{
					myConnect = new SerialConnect();
				}
				List<string> comList = (myConnect as SerialConnect).GetSerialPortNames();
				if (comList != null && comList.Count > 0)
				{
					foreach (string comName in comList)
					{
						deviceComboBox.Items.Add(comName);
					}
				}
			}
			// 获取网络设备列表
			else
			{
				IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
				foreach (IPAddress ip in ipe.AddressList)
				{
					if (ip.AddressFamily == AddressFamily.InterNetwork) //当前ip为ipv4时，才加入到列表中
					{
						NetworkConnect.SearchDevice(ip.ToString());
						// 需要延迟片刻，才能找到设备;	故在此期间，主动暂停片刻
						Thread.Sleep(MainFormBase.NETWORK_WAITTIME);
					}
				}

				Dictionary<string, Dictionary<string, NetworkDeviceInfo>> allDevices = NetworkConnect.GetDeviceList();
				networkDeviceList = new List<NetworkDeviceInfo>();
				if (allDevices.Count > 0)
				{
					foreach (KeyValuePair<string, Dictionary<string, NetworkDeviceInfo>> device in allDevices)
					{
						foreach (KeyValuePair<string, NetworkDeviceInfo> d2 in device.Value)
						{
							string localIPLast = device.Key.ToString().Substring(device.Key.ToString().LastIndexOf("."));
							deviceComboBox.Items.Add(d2.Value.DeviceName + "(" + d2.Value.DeviceIp + ")" + localIPLast);
							networkDeviceList.Add(d2.Value);
						}
					}
				}
			}

			if (deviceComboBox.Items.Count > 0)
			{
				deviceComboBox.SelectedIndex = 0;
				deviceComboBox.Enabled = true;
				deviceConnectButton.Enabled = true;
				setNotice("已搜到可用设备列表。", false);
			}
			else
			{
				setNotice("未找到可用设备，请检查设备连接后重试。", true);
			}
		}

		/// <summary>
		/// 辅助方法：断开连接（主动断开连接、退出Form及切换连接方式时，都跑一次这个方法）
		/// </summary>
		private void disConnect()
		{
			if (myConnect != null && myConnect.IsConnected())
			{
				myConnect.DisConnect();
				myConnect = null;
				isConnected = false;
				refreshConnectButtons();
				setNotice("已" + (isConnectCom ? "关闭串口(" + deviceComboBox.Text + ")" : "断开连接"), true);
			}
		}

		/// <summary>
		/// 辅助方法：刷新按键[可用性]及[显示的文字]
		/// </summary>
		private void refreshConnectButtons()
		{
			switchButton.Enabled = !isConnected;
			deviceComboBox.Enabled = deviceComboBox.Items.Count > 0 && !isConnected;
			refreshButton.Enabled = !isConnected;
			deviceConnectButton.Enabled = deviceComboBox.Items.Count > 0;
			updateButton.Enabled = isConnected ; 
			
			if (isConnectCom)
			{
				deviceConnectButton.Text = isConnected ? "关闭串口" : "打开串口";
			}
			else
			{
				deviceConnectButton.Text = isConnected ? "断开连接" : "连接设备";
			}
		}

		/// <summary>
		/// 事件：点击《刷新串口|网络》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshButton_Click(object sender, EventArgs e)
		{
			refreshDeviceComboBox();
		}

		/// <summary>
		/// 辅助方法：显示信息
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="messageBoxShow">是否在提示盒内提示</param>
		private void setNotice(string msg, bool messageBoxShow)
		{
			if (messageBoxShow)
			{
				MessageBox.Show(msg);
			}
			myStatusLabel.Text = msg;
			statusStrip1.Refresh();
		}	

		/// <summary>
		/// 辅助方法：禁用设备列表下拉框,并清空其数据
		/// </summary>
		private void disableDeviceComboBox()
		{
			deviceComboBox.Items.Clear();
			deviceComboBox.SelectedIndex = -1;
			deviceComboBox.Text = "";
			deviceComboBox.Enabled = false;
		}

		/// <summary>
		/// 事件：点击《打开串口|关闭串口 | 连接设备|断开连接》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceConnectButton_Click(object sender, EventArgs e)
		{
			// 如果已连接（按钮显示为“连接设备”)，则关闭连接
			if (isConnected)
			{
				disConnect(); //deviceConnectButton_Click			
				return;
			}

			// 若未连接，则连接；并分情况处理
			if (isConnectCom)
			{
				myConnect = new SerialConnect();
				try
				{
					if ((myConnect as SerialConnect).OpenSerialPort(deviceComboBox.Text))
					{
						isConnected = true;
						refreshConnectButtons();
						setNotice("已打开串口(" + deviceComboBox.Text + ")。", true);
					}
				}
				catch (Exception ex)
				{
					setNotice("打开串口失败，原因是：\n" + ex.Message, true);
				}
			}
			else
			{
				NetworkDeviceInfo selectedNetworkDevice = networkDeviceList[deviceComboBox.SelectedIndex];
				string deviceName = selectedNetworkDevice.DeviceName;
				myConnect = new NetworkConnect();
				if (myConnect.Connect(selectedNetworkDevice))
				{
					isConnected = true;
					refreshConnectButtons();
					setNotice("成功连接网络设备(" + deviceName + ")。", true);
				}
				else
				{
					setNotice("连接网络设备(" + deviceName + ")失败。", true);
				}
			}
		}

		/// <summary>
		/// 事件：点击《下载工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateButton_Click_1(object sender, EventArgs e)
		{
			SetBusy(true);
			bool generateNow = false;
			if (String.IsNullOrEmpty(projectPath))
			{
				DialogResult dr = MessageBox.Show("检查到您未选中已导出的工程文件夹，如继续操作会实时生成数据(将消耗较长时间)，是否继续？",
					"下载工程?",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question);
				if (dr == DialogResult.Cancel)
				{
					SetBusy(false);
					return;
				}

				if (dbWrapper.lightList == null || dbWrapper.lightList.Count == 0)
				{
					MessageBox.Show("当前工程无灯具，无法更新工程。");
					SetBusy(false);
					return;
				}
				generateNow = true;//只有当前无projectPath且选择继续后会rightNow
			}
			//若用户选择了已存在目录，则需要验证是否空目录
			else
			{
				if (Directory.GetFiles(projectPath).Length == 0)
				{
					MessageBox.Show("所选目录为空,无法下载工程。请选择正确的已有工程目录，并重新下载。");
					SetBusy(false);
					return;
				}
			}

			if (generateNow)
			{
				setNotice("正在实时生成工程数据，请耐心等待...",false);
				DataConvertUtils.SaveProjectFile(dbWrapper, mainForm, globalSetPath,  new GenerateProjectCallBack(this, isConnectCom) );
			}
			else
			{
				FileUtils.CopyFileToDownloadDir(projectPath);
				DownloadProject(isConnectCom);
			}
		}




		#endregion

	}

	public class NetworkDownloadReceiveCallBack : ICommunicatorCallBack
	{
		private ProjectUpdateForm puForm;
		public NetworkDownloadReceiveCallBack(ProjectUpdateForm downloadForm)
		{
			this.puForm = downloadForm;
		}

		public void Completed(string deviceTag)
		{
			MessageBox.Show("设备：" + deviceTag + "  下载成功");		
			puForm.SetBusy(false);								
		}

		public void Error(string deviceTag, string errorMessage)
		{
			MessageBox.Show("设备：" + deviceTag + " 下载失败\n错误原因是:" + errorMessage);			
			puForm.SetBusy(false);			
			puForm.ClearNetworkDevices();
		}

		public void GetParam(CSJ_Hardware hardware)
		{
			throw new NotImplementedException();
		}

		public void UpdateProgress(string deviceTag, string fileName, int newProgress)
		{
			puForm.networkPaintProgress(fileName, newProgress);
		}

		
	}

	public class ComDownloadReceiveCallBack : ICommunicatorCallBack
	{
		private ProjectUpdateForm puForm;
		public ComDownloadReceiveCallBack(ProjectUpdateForm downloadForm)
		{
			this.puForm = downloadForm;
		}

		public void Completed(string deviceTag)
		{
			MessageBox.Show("下载成功");
			puForm.SetBusy(false);
		}

		public void Error(string deviceTag, string errorMessage)
		{
			MessageBox.Show("下载失败，错误原因是:\n" + errorMessage);
			puForm.SetBusy(false);
		}

		public void GetParam(CSJ_Hardware hardware)
		{
			throw new NotImplementedException();
		}

		public void UpdateProgress(string deviceTag, string fileName, int newProgress)
		{
			puForm.comPaintProgress(fileName, newProgress);
		}
	}

	public class GenerateProjectCallBack : ISaveProjectCallBack
	{
		private ProjectUpdateForm puForm;
		private bool isNetwork;
		public GenerateProjectCallBack(ProjectUpdateForm puForm, bool isNetwork)
		{
			this.puForm = puForm;
			this.isNetwork = isNetwork;
		}

		public void Completed()
		{
			puForm.SetLabelText(isNetwork,"数据生成成功，即将传输数据到设备。");
			FileUtils.CopyProjectFileToDownloadDir();
			puForm.GenerateSourceZip(Application.StartupPath + @"\DataCache\Download\CSJ\Source.zip");	
			puForm.DownloadProject(isNetwork);
		}

		public void Error()
		{
			MessageBox.Show("数据生成出错");
			puForm.SetBusy(false);
		}
		public void UpdateProgress(string name)
		{
			puForm.SetLabelText(isNetwork, name);
		}
	}
}
