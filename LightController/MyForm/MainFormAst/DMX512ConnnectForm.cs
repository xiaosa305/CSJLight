using System;
using System.IO.Ports;
using System.Windows.Forms;
using LightController.Common;
using LightController.Xiaosa.Tools;

namespace LightController.MyForm.MainFormAst
{
	public partial class DMX512ConnnectForm : Form
	{
		private MainFormBase mainForm;

		public DMX512ConnnectForm(MainFormBase mainForm)
		{			
			InitializeComponent();
			this.mainForm = mainForm;
		}

		private void DMX512ConnnectForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
		}

		/// <summary>
		/// 事件：点击《刷新串口》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void portRefreshButton_Click(object sender, EventArgs e)
		{
			refreshPortList();
		}

		/// <summary>
		/// 辅助方法：刷新串口列表
		/// </summary>
		private void refreshPortList() {

			string[] portNameList = SerialPort.GetPortNames();
			portComboBox.Items.Clear();
			portComboBox.Text = "";
			if (portNameList.Length > 0)
			{
				foreach (string com in portNameList)
				{
					portComboBox.Items.Add(com);
				}
				portComboBox.SelectedIndex = 0;
				portComboBox.Enabled = true;
				connectButton.Enabled = true;
			}
			else
			{
				portComboBox.SelectedIndex = -1;
				portComboBox.Enabled = false;
				connectButton.Enabled = false;
			}
		}
		
		/// <summary>
		/// 事件：点击《连接灯具|断开连接》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectButton_Click(object sender, EventArgs e)
		{
			if (mainForm.IsDMXConnected)
			{
				setNotice("正在断开连接，请稍候...", false, true);

				mainForm.SerialPlayTools.SerialPortSwitch(null, false);
				portComboBox.Enabled = true;
				portRefreshButton.Enabled = true;				
				connectButton.Text = "连接灯具";
				mainForm.IsDMXConnected = false;
				

				setNotice("已断开DMX512连接。", false, true);

			}
			else {
				setNotice("正在连接灯具，请稍候...", false, true);

				mainForm.SerialPlayTools.SerialPortSwitch(  portComboBox.Text , true);
				portComboBox.Enabled = false;
				portRefreshButton.Enabled = false;
				connectButton.Text = "断开连接";
				mainForm.IsDMXConnected = true;

				setNotice("已使用DMX512线连接灯具，可用以调试。", false, true);
			}			
		}

		#region 通用方法

		/// <summary>
		/// 辅助方法：显示信息
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="messageBoxShow">是否在提示盒内提示</param>
		private void setNotice(string msg, bool messageBoxShow, bool isTranslate)
		{
			if (isTranslate)
			{
				msg = LanguageHelper.TranslateSentence(msg);
			}

			if (messageBoxShow)
			{
				MessageBox.Show(msg);
			}
			myStatusLabel.Text = msg;
			statusStrip1.Refresh();
		}

		#endregion

	}
}
