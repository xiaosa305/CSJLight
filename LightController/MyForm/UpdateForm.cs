using LightController.Ast;
using LightController.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

		public UpdateForm(MainFormInterface mainForm,DBWrapper dbWrapper,string globalSetPath)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			this.dbWrapper = dbWrapper;
			this.globalSetPath = globalSetPath;
		}

		private void UpdateForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			// 设false可在其他文件中修改本类的UI
			Control.CheckForIllegalCrossThreadCalls = false;
		}

		/// <summary>
		///  点击《搜索设备》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void searchButton_Click(object sender, EventArgs e)
		{
			ConnectTools cTools = ConnectTools.GetInstance();
			cTools.Start("192.168.31.14");
			cTools.SearchDevice();
			// 需要延迟片刻，才能找到设备
			connectSkinButton.Enabled = false;
			Thread.Sleep(1000);
			Dictionary<string, string> allDevices = cTools.GetDeviceInfo();
		
			devicesComboBox.Items.Clear();
			ips = new List<string>();
			if (allDevices.Count > 0)
			{
				foreach (KeyValuePair<string, string> device in allDevices)
				{
					devicesComboBox.Items.Add(device.Value + "(" + device.Key + ")");
					ips.Add(device.Key);
				}
				devicesComboBox.SelectedIndex = 0;
				connectSkinButton.Enabled = true;
			}
			else {
				devicesComboBox.SelectedIndex = -1;
				connectSkinButton.Enabled = false;
				MessageBox.Show("未找到可用设备，请确认后重试。");	
			}	
		}		

		/// <summary>
		///  点击《连接设备》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectButton_Click(object sender, EventArgs e)
		{
			selectedIPs = new List<string>();
			selectedIPs.Add(ips[devicesComboBox.SelectedIndex]);
			MessageBox.Show("设备连接成功");
			updateSkinButton.Enabled = true;
		}


		/// <summary>
		/// 点击《下载数据》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UpdateButton_Click(object sender, EventArgs e)
		{
			ConnectTools cTools = ConnectTools.GetInstance();
			cTools.Download(selectedIPs, dbWrapper, globalSetPath, new DownloadReceiveCallBack(), new DownloadProgressDelegate(paintProgress));
			//MessageBox.Show("断开连接");
			connectSkinButton.Enabled = false;
			updateSkinButton.Enabled = false;
			devicesComboBox.Items.Clear();
			devicesComboBox.Text = "";
		}

		/// <summary>
		///  辅助委托方法：将数据写进度条
		/// </summary>
		/// <param name="a"></param>		
		void paintProgress(string fileName,int a)
		{
			currentFileLabel.Text = fileName;
			skinProgressBar1.Value =  a;				
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
