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
		private string projectPath;   // 已有工程的路径（选到CSJ这一层）		
		private string globalSetPath;   // 全局配置路径

		private BaseCommunication myConnect; // 保持着一个设备连接（串网口通用）
		private bool isConnected = false; //是否连接
		private bool isConnectCom = true; //是否串口连接
		private IList<NetworkDeviceInfo> networkDeviceList;  // 网络设备的列表			

		public ProjectUpdateForm(MainFormBase mainForm, string globalSetPath, string projectPath)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			//this.dbWrapper = dbWrapper;
			this.globalSetPath = globalSetPath;
			this.projectPath = projectPath;
			pathLabel.Text = projectPath;
		}

		private void ProjectUpdateForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			// 设false可在其他文件中修改本类的UI
			Control.CheckForIllegalCrossThreadCalls = false;

			// 主动刷新设备列表
			refreshDeviceComboBox();
		}

		/// <summary>
		/// 事件：《窗口关闭》时，主动关闭连接
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProjectUpdateForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (isConnected)
			{
				myConnect.DisConnect();
				myConnect = null ;
			}
			Dispose();
			mainForm.Activate();
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
		/// 事件：点击《更换连接方式》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void switchButton_Click(object sender, EventArgs e)
		{
			isConnectCom = !isConnectCom;
			switchButton.Text = isConnectCom ? "切换为\n网络连接" : "切换为\n串口连接";
			refreshButton.Text = isConnectCom ? "刷新串口" : "刷新网络";
			deviceConnectButton.Text = isConnectCom ? "打开串口" : "连接设备";
			refreshDeviceComboBox(); // switchButton_Click
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
				SetNotice("已刷新" + (isConnectCom ? "串口" : "网络设备") + "列表。", false);
			}
			else
			{
				SetNotice("未找到可用设备，请检查设备连接后重试。", false);
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
				SetNotice("已" + (isConnectCom ? "关闭串口(" + deviceComboBox.Text + ")" : "断开连接"), true);
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
			updateButton.Enabled = isConnected;

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
						SetNotice("已打开串口(" + deviceComboBox.Text + ")。", true);
					}
				}
				catch (Exception ex)
				{
					SetNotice("打开串口失败，原因是：\n" + ex.Message, true);
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
					SetNotice("成功连接网络设备(" + deviceName + ")。", true);
				}
				else
				{
					SetNotice("连接网络设备(" + deviceName + ")失败。", true);
				}
			}
		}

		/// <summary>
		/// 事件：点击《更新工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateButton_Click(object sender, EventArgs e)
		{
			SetBusy(true);
			bool generateNow = false;
			DBWrapper dbWrapper = mainForm.GetDBWrapper(false);

			if (String.IsNullOrEmpty(projectPath))
			{
				DialogResult dr = MessageBox.Show("更新工程会覆盖设备(tf卡)内原有的工程，是否继续？",
					"是否继续更新工程?",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question);
				if (dr == DialogResult.Cancel)
				{
					SetBusy(false);
					return;
				}

				dr = MessageBox.Show("检查到您未选中已导出的工程文件夹，如继续操作会实时生成数据(将消耗较长时间)，是否继续？",
					"是否实时生成工程?",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question);
				if (dr == DialogResult.Cancel)
				{
					SetBusy(false);
					return;
				}
				
				if (dbWrapper.lightList == null || dbWrapper.lightList.Count == 0)
				{
					SetNotice("当前工程无灯具，无法更新工程。",true);
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
					SetNotice("所选目录为空,无法更新工程。请选择正确的已有工程目录，并重新更新。",true);
					SetBusy(false);
					return;
				}
			}

			if (myConnect == null || !isConnected) {
				SetNotice("尚未连接设备，请连接后重试。", true);
				SetBusy(false);
				return;
			}

			if (generateNow)
			{
				SetNotice("正在实时生成工程数据，请耐心等待...", false);
				DataConvertUtils.SaveProjectFile(dbWrapper, mainForm, globalSetPath, new GenerateProjectCallBack(this));
			}
			else
			{
				FileUtils.CopyFileToDownloadDir(projectPath);
				DownloadProject();
			}
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
		/// 辅助方法：将本地的工程文件，传送到设备中(因可能由外部类回调，故需单独写一个方法)
		/// </summary>
		public void DownloadProject()
		{
			myConnect.DownloadProject(DownloadCompleted, DownloadError, DrawProgress);
		}

		/// <summary>
		/// 辅助回调方法：工程更新成功
		/// </summary>
		/// <param name="obj"></param>
		public void DownloadCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				SetNotice("工程更新成功。", true);
				myProgressBar.Value = 0;
				SetBusy(false);
			});
		}

		/// <summary>
		/// 辅助回调方法：工程更新失败
		/// </summary>
		/// <param name="obj"></param>
		public void DownloadError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				SetNotice("工程更新失败[" + msg + "]", true);
				myProgressBar.Value = 0;
				SetBusy(false);
			});
		}

		/// <summary>
		/// 辅助回调方法：写进度条
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="progress"></param>
		public void DrawProgress(string fileName, int progressPercent)
		{
			SetNotice(string.IsNullOrEmpty(fileName) ? "" : "正在传输文件：" + fileName, false);
			myProgressBar.Value = progressPercent;
		}
				
		/// <summary>
		/// 辅助方法：显示信息
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="messageBoxShow">是否在提示盒内提示</param>
		public void SetNotice(string msg, bool messageBoxShow)
		{
			if (messageBoxShow)
			{
				MessageBox.Show(msg);
			}
			myStatusLabel.Text = msg;
			statusStrip1.Refresh();
		}

		/// <summary>
		/// 辅助方法：设定忙时
		/// </summary>
		/// <param name="busy"></param>
		public void SetBusy(bool busy)
		{
			Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
			Enabled = ! busy ;
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
			   		
	}

	public class GenerateProjectCallBack : ISaveProjectCallBack
	{
		private ProjectUpdateForm puForm;

		public GenerateProjectCallBack(ProjectUpdateForm puForm)
		{
			this.puForm = puForm;
		}

		public void Completed()
		{
			puForm.SetNotice("数据生成成功，即将传输数据到设备。", false);
			if (FileUtils.CopyProjectFileToDownloadDir())
			{
				puForm.GenerateSourceZip(Application.StartupPath + @"\DataCache\Download\CSJ\Source.zip");
				puForm.DownloadProject();
			}
			else {
				MessageBox.Show("拷贝工程文件到临时目录时出错。");
				puForm.SetBusy(false);
			}			
		}

		public void Error(string msg)
		{
			MessageBox.Show("数据生成出错");
			puForm.SetBusy(false);
		}

		public void UpdateProgress(string name)
		{
			puForm.SetNotice("正在生成工程文件(" + name + ")" ,false);
		}
	}

}
