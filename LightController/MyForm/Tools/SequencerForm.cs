using CCWin.SkinControl;
using LightController.Common;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.MyForm.OtherTools
{
	public partial class SequencerForm : Form
	{
		private MainFormBase mainForm;
		private bool isConnLC = false;
		private int relayCount = 7;
		private LightControlData lcEntity; //灯控封装对象		

		private Panel[] relayPanels ;
		private SkinButton[] relayButtons;
		private TextBox[] relayTBs;
		private Panel[] timePanels; 
		private NumericUpDown[] timeNUDs;

		public SequencerForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;
			InitializeComponent();

			//初始化各个 继电器开关、名称和时延 的相关控件，填入数组中，方便调用；
			relayPanels = new Panel[relayCount];
			relayButtons = new SkinButton[relayCount];
			relayTBs = new TextBox[relayCount];
			timePanels = new Panel[relayCount - 1];
			timeNUDs = new NumericUpDown[relayCount - 1];

			for (int relayIndex = 0; relayIndex < relayCount; relayIndex++)
			{

				relayButtons[relayIndex] = new SkinButton
				{
					BackColor = relayButtonDemo.BackColor,
					BaseColor = relayButtonDemo.BaseColor,
					BorderColor = relayButtonDemo.BorderColor,
					ControlState = relayButtonDemo.ControlState,
					DownBack = relayButtonDemo.DownBack,
					DrawType = relayButtonDemo.DrawType,
					Font = relayButtonDemo.Font,
					ForeColor = relayButtonDemo.ForeColor,
					ForeColorSuit = relayButtonDemo.ForeColorSuit,
					ImageAlign = relayButtonDemo.ImageAlign,
					ImageList = relayButtonDemo.ImageList,
					ImageKey = relayButtonDemo.ImageKey,
					ImageSize = relayButtonDemo.ImageSize,
					InheritColor = relayButtonDemo.InheritColor,
					IsDrawBorder = relayButtonDemo.IsDrawBorder,
					Location = relayButtonDemo.Location,
					Margin = relayButtonDemo.Margin,
					MouseBack = relayButtonDemo.MouseBack,
					NormlBack = relayButtonDemo.NormlBack,
					Size = relayButtonDemo.Size,
					Tag = relayButtonDemo.Tag,
					TextAlign = relayButtonDemo.TextAlign,
					UseVisualStyleBackColor = relayButtonDemo.UseVisualStyleBackColor,
					Visible = true,
					Name = "switchButtons" + (relayIndex + 1),
					Text = LanguageHelper.TranslateWord("开关") + (relayIndex + 1)
				};

				relayTBs[relayIndex] = new TextBox
				{
					Location = relayTBDemo.Location,
					Size = relayTBDemo.Size,
					TextAlign = relayTBDemo.TextAlign,
					MaxLength = relayTBDemo.MaxLength,
				};

				relayPanels[relayIndex] = new Panel
				{
					Location = relayPanelDemo.Location,
					Size = relayPanelDemo.Size
				};

				relayPanels[relayIndex].Controls.Add(relayButtons[relayIndex]);
				relayPanels[relayIndex].Controls.Add(relayTBs[relayIndex]);
				relayFLP.Controls.Add(relayPanels[relayIndex]);
			}

			relayFLP.Controls.Add(labelPanel); // 调整labelPanel的位置

			for (int timeIndex = 0; timeIndex < relayCount - 1; timeIndex++)
			{
				timeNUDs[timeIndex] = new NumericUpDown
				{
					Location = timeNUDDemo.Location,
					Maximum = timeNUDDemo.Maximum,
					Minimum = timeNUDDemo.Minimum,
					Size = timeNUDDemo.Size,
					TextAlign = timeNUDDemo.TextAlign,
					Value = timeNUDDemo.Value
				};
				timeNUDs[timeIndex].MouseWheel += someNUD_MouseWheel;
				timeNUDs[timeIndex].KeyPress += timeNUD_KeyPress;
				myToolTip.SetToolTip(timeNUDs[timeIndex], "更改时延后，点击a键可统一设置；时延的范围为1-15s。");

				timePanels[timeIndex] = new Panel
				{
					Location = timePanelDemo.Location,
					Size = timePanelDemo.Size
				};

				timePanels[timeIndex].Controls.Add(timeNUDs[timeIndex]);
				relayFLP.Controls.Add(timePanels[timeIndex]);
			}
		}

		private void SequencerForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;			
		}

		private void SequencerForm_Shown(object sender, EventArgs e)
		{
			switchLCMode(); //把这个方法写在此处，可避免打开本窗口过短时容易在灯控模式时超时的bug
		}

		/// <summary>
		/// 辅助方法：切换为灯控模式
		/// </summary>
		private void switchLCMode() {
			if (mainForm.IsDeviceConnected)
			{
				setBusy(true);
				mainForm.SleepBetweenSend("Order : SwitchLCMode", 1);	
				mainForm.MyConnect.LightControlConnect(LCConnectCompleted, LCConnectError);
			}
			else {
				setNotice("设备未连接，本窗口将关闭。",true,true);
				Dispose();
			}
		}

		/// <summary>
		/// 辅助回调方法：灯控连接成功
		/// </summary>
		/// <param name="obj"></param>
		public void LCConnectCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {				
				setNotice( "已切换成灯控配置(connStatus=lc)", false, true);
				isConnLC = true;
				setBusy(false);
				
				// 只要连接上就主动读取设备信息
				readButton_Click(null, null);
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控连接出错
		/// </summary>
		/// <param name="obj"></param>
		public void LCConnectError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				// 切换灯控模式失败，直接退出本窗口。
				setNotice("切换灯控模式失败,本窗口将关闭；请重连设备后重试。", true, true);
				setBusy(false);

				Dispose();
			});
		}

		#region  开台|关台相关

		/// <summary>
		/// 事件：点击《开台模拟》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openButton_Click(object sender, EventArgs e)
		{
			setNotice("正在发送开台命令，请稍候...",false,true);
			mainForm.MyConnect.OpenScene( OpenSceneCompleted,OpenSceneError);			
		}

		/// <summary>
		/// 辅助回调方法：发送开台命令成功
		/// </summary>
		/// <param name="lcDataTemp"></param>
		public void OpenSceneCompleted(Object lcDataTemp, string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice("发送开台命令成功，正在模拟(开台)继电器工作流程...", false, true);
				setBusy(true);
				for (int relayIndex = 0; relayIndex < relayCount; relayIndex++)
				{
					relayButtons[relayIndex].ImageKey = "Ok3w.Net图标15.png";
					Refresh();
					if (relayIndex < relayCount - 1)
					{
						Thread.Sleep(decimal.ToInt32(timeNUDs[relayIndex].Value) * 1000);
					}
				}				
				setBusy(false);
				setNotice("开台成功。",false,true);
			});
		}

		/// <summary>
		/// 辅助回调方法：发送开台命令失败
		/// </summary>
		public void OpenSceneError(string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice("发送开台命令失败：" + msg, true, true);
			});
		}

		/// <summary>
		/// 事件：点击《关台模拟》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void closeButton_Click(object sender, EventArgs e)
		{
			setNotice("正在发送关台命令，请稍候...", false, true);
			mainForm.MyConnect.CloseScene(CloseSceneCompleted, CloseSceneError);
		}

		/// <summary>
		/// 辅助回调方法：发送关台命令失败
		/// </summary>
		/// <param name="lcDataTemp"></param>
		public void CloseSceneCompleted(Object lcDataTemp, string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice("发送关台命令成功，正在模拟(关台)继电器工作流程...", false, false);
				setBusy(true);
				for (int relayIndex = 6; relayIndex >= 0;)
				{
					relayButtons[relayIndex].ImageKey = "Ok3w.Net图标1.png";
					Refresh();
					relayIndex--;
					if (relayIndex >= 0)
					{
						Thread.Sleep(decimal.ToInt32(timeNUDs[relayIndex].Value) * 1000);
					}
				}
				setBusy(false);
				setNotice("关台成功。", false, true);
			});
		}

		/// <summary>
		/// 辅助回调方法：发送关台命令失败
		/// </summary>
		public void CloseSceneError(string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice("发送关台命令失败：" + msg, true, true);
			});
		}

		#endregion

		/// <summary>
		/// 事件：点击《回读配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void readButton_Click(object sender, EventArgs e)
		{
			if (isConnLC)
			{
				setBusy(true);
				setNotice("正在回读继电器配置，请稍候...", false, true);
				mainForm.MyConnect.LightControlRead(LCReadCompleted, LCReadError);
			}
			else {
				setNotice("当前非灯控模式，无法回读配置，请重启本窗口重试。", false, true);
			}
		}

		/// <summary>
		/// 辅助回调方法：灯控数据回读成功
		/// </summary>
		/// <param name="lcDataTemp"></param>
		public void LCReadCompleted(Object lcDataTemp, string msg)
		{
			Invoke((EventHandler)delegate {
				if (lcDataTemp == null)
				{
					setNotice("继电器配置回读异常(lc为null)。", true, true);
					setBusy(false);
					Dispose();
					return;
				}
				lcEntity = lcDataTemp as LightControlData ;
				if (lcEntity.SequencerData == null) {
					setNotice("继电器配置回读失败：请确认此设备版本支持时序器功能。", true, true);
					setBusy(false);
					Dispose();
					return;
				}
				relayRender();
				setNotice( "成功回读继电器配置", true, true);
				setBusy(false);
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控配置回读失败
		/// </summary>
		public void LCReadError(string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice(LanguageHelper.TranslateSentence("回读继电器配置失败:") + msg, true, false);
				setBusy(false);
				Dispose();
			});
		}

		/// <summary>
		/// 辅助方法：根据继电器的配置信息，渲染各个控件；
		/// </summary>
		private void relayRender() {

			try
			{
				openCheckBox.Checked = lcEntity.SequencerData.IsOpenSequencer;
				for (int relayIndex = 0; relayIndex < relayCount; relayIndex++) {
					relayTBs[relayIndex].Text =  lcEntity.SequencerData.RelaySwitchNames[relayIndex];
				}
				for (int timeIndex = 0; timeIndex < relayCount-1; timeIndex++)
				{
					timeNUDs[timeIndex].Value = lcEntity.SequencerData.RelaySwitchDelayTimes[timeIndex];
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 事件：点击《下载配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void writeButton_Click(object sender, EventArgs e)
		{
			if (lcEntity == null) {
				setNotice("尚未加载继电器配置，无法下载。", true, true);
				return;
			}
			setNotice("正在下载继电器配置，请稍候...", false, true);
			makeLC();
			mainForm.MyConnect.LightControlDownload(lcEntity, LCDownloadCompleted, LCDownloadError);
		}

		/// <summary>
		/// 辅助方法：把当前控件内数据，填入lcEntity中
		/// </summary>
		private void makeLC() {

			lcEntity.SequencerData.IsOpenSequencer = openCheckBox.Checked; 
			for (int relayIndex = 0; relayIndex < relayCount; relayIndex++)
			{
				lcEntity.SequencerData.RelaySwitchNames[relayIndex] = relayTBs[relayIndex].Text.Trim();
			}
			for (int timeIndex = 0; timeIndex < relayCount - 1; timeIndex++)
			{
				lcEntity.SequencerData.RelaySwitchDelayTimes[timeIndex] = decimal.ToInt32(timeNUDs[timeIndex].Value);
			}
		}

		/// <summary>
		/// 辅助回调方法：灯控配置下载成功
		/// </summary>
		/// <param name="obj"></param>
		public void LCDownloadCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice( "继电器配置下载成功,请等待设备重启(约耗时5s)，重新搜索并连接设备。", true, true);
				reconnectDevice();
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控配置下载错误
		/// </summary>
		public void LCDownloadError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice( LanguageHelper.TranslateSentence("继电器配置下载失败：") + msg, true, false);
				reconnectDevice();
			});
		}

		/// <summary>
		/// 事件：点击《打开配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loadButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == lbinOpenDialog.ShowDialog())
			{
				string lbinPath = lbinOpenDialog.FileName;
				try
				{
					lcEntity = (LightControlData)SerializeUtils.DeserializeToObject(lbinPath);
					relayRender(); // loadButton_Click
					setNotice("成功加载本地配置文件(" + lbinOpenDialog.SafeFileName + ")。", true, true);
				}
				catch (Exception ex)
				{
					setNotice("加载本地配置文件时发生异常：" + ex.Message, true, true);
				}
			}
		}

		/// <summary>
		/// 事件：点击《保存配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == lbinSaveDialog.ShowDialog())
			{
				makeLC();
				try
				{
					string lbinPath = lbinSaveDialog.FileName;
					SerializeUtils.SerializeObject(lbinPath, lcEntity);
					setNotice("成功保存配置到本地。", true, true);
				}
				catch (Exception ex)
				{
					setNotice("保存配置时发生异常：" + ex.Message, true, true);
				}
			}
		}

		#region 通用方法

		/// <summary>
		/// 辅助方法：如果下载配置成功，则应该重连设备；此法被灯控和中控下载成功的回调函数调用；
		/// </summary>
		private void reconnectDevice()
		{
			mainForm.DisConnect();
			isConnLC = false;
			mainForm.ConnForm.ShowDialog();
			if (mainForm.IsDeviceConnected)
			{
				switchLCMode();	
			}
			else
			{
				MessageBox.Show("请重新连接设备，否则无法进行外设配置!");
				Dispose();
			}
		}

		/// <summary>
		/// 辅助方法：通用的通知方法（这个Form比较复杂，因为Tab太多了）
		/// </summary>
		/// <param name="position">放到底部通知栏的哪一侧，1为左侧，2为右侧</param>
		/// <param name="msg"></param>
		/// <param name="isMsgShow"></param>
		/// <param name="isTranslate"></param>
		private void setNotice(string msg, bool isMsgShow, bool isTranslate)
		{
			if (isTranslate) { msg = LanguageHelper.TranslateSentence(msg); }
			if (isMsgShow) { MessageBox.Show(msg); }
			noticeLabel.Text = msg;
		}

		/// <summary>
		/// 辅助方法：设忙时或解除忙时
		/// </summary>
		/// <param name="busy"></param>
		private void setBusy(bool busy)
		{
			Enabled = !busy;
			Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
		}

		/// <summary>
		///  辅助方法：一些Control文本改变时，进行翻译
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someControl_TextChanged(object sender, EventArgs e)
		{
			LanguageHelper.TranslateControl(sender as Control);
		}

		/// <summary>
		/// 验证：对某些NumericUpDown进行鼠标滚轮的验证，避免一次性滚动过多
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someNUD_MouseWheel(object sender, MouseEventArgs e)
		{
			NumericUpDown nud = sender as NumericUpDown;
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = nud.Value + nud.Increment;
				if (dd <= nud.Maximum)
				{
					nud.Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = nud.Value - nud.Increment;
				if (dd >= nud.Minimum)
				{
					nud.Value = dd;
				}
			}
		}


		#endregion

		/// <summary>
		/// 事件：《时延输入框》的键盘点击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timeNUD_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == 'a' || e.KeyChar == 'A' )
			{
				decimal unifySt = (sender as NumericUpDown).Value;

				// 设置了提示，且用户点击了取消，则return。否则继续往下走
				if (mainForm.IsNoticeUnifyTd)
				{
					if (DialogResult.Cancel == MessageBox.Show(
							LanguageHelper.TranslateSentence("确定要将所有时延都设为") + "【" + unifySt + " S】?",
							LanguageHelper.TranslateSentence("统一时延"),
							MessageBoxButtons.OKCancel,
							MessageBoxIcon.Question))
					{
						return;
					}
				}

				for (int timeIndex = 0; timeIndex < timeNUDs.Length; timeIndex++)
				{
					timeNUDs[timeIndex].Value = unifySt;
				}
			}
		}

	}
}
