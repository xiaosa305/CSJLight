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
		private MainForm mainForm;
		private DBWrapper dbWrapper;
		private string globalSetPath;

		private IList<string> selectedIPs;
		private IList<string> ips;		

		public UpdateForm(MainForm mainForm,DBWrapper dbWrapper,string globalSetPath)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			this.dbWrapper = dbWrapper;
			this.globalSetPath = globalSetPath;
		}

		private void UpdateForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
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
			connectButton.Enabled = false;
			Thread.Sleep(1000);
			Dictionary<string, string> allDevices = cTools.GetDeviceInfo();
			devicesComboBox.Items.Clear();
			ips = new List<string>();
			if(allDevices.Count > 0) {
				foreach (KeyValuePair<string,string> device in allDevices)
				{				
					devicesComboBox.Items.Add(device.Value + "(" + device.Key + ")");
					ips.Add(device.Value);
				}
				devicesComboBox.SelectedIndex = 0;
			}
			connectButton.Enabled = true;
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
		}


		/// <summary>
		/// 点击《下载数据》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UpdateButton_Click(object sender, EventArgs e)
		{
			ConnectTools cTools = ConnectTools.GetInstance();
			cTools.Download(selectedIPs, dbWrapper, globalSetPath, new DownloadReceiveCallBack() ,new DownloadProgressDelegate(testProgress) );		
		
		}


		/// <summary>
		///  测试委托
		/// </summary>
		/// <param name="a"></param>
		void testProgress(double a)
		{
			MessageBox.Show("Dickov:" + a);
		}
		// 测试进度条的绘制
		public void paintPrograssBar() {
			//progressBar1.Maximum = 100;//设置最大长度值
			//progressBar1.Value = 0;//设置当前值
			//progressBar1.Step = 1;//设置没次增长多少
			//for (int i = 0; i < 100; i++)//循环
			//{
			//	System.Threading.Thread.Sleep(100);//暂停1秒
			//	progressBar1.Value += progressBar1.Step;  // 让进度条增加一次
			//}
			//if (progressBar1.Value == progressBar1.Maximum) {
			//	MessageBox.Show("Dickov:已完成");
			//}
		}

	}


	public class DownloadReceiveCallBack : IReceiveCallBack
	{
		public void SendCompleted(string ip, string order)
		{
			MessageBox.Show("IP："+ip+" 的设备已经成功下载；发回了命令："+order);
		}

		public void SendError(string ip, string order)
		{
			MessageBox.Show("IP：" + ip + " 的设备已经下载失败；发回了命令：" + order);
		}
	}
}
