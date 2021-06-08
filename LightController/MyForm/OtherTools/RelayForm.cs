﻿using CCWin.SkinControl;
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
	public partial class RelayForm : Form
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

		public RelayForm(MainFormBase mainForm)
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
					TextAlign = relayTBDemo.TextAlign
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

				timePanels[timeIndex] = new Panel
				{
					Location = timePanelDemo.Location,
					Size = timePanelDemo.Size
				};

				timePanels[timeIndex].Controls.Add(timeNUDs[timeIndex]);
				relayFLP.Controls.Add(timePanels[timeIndex]);
			}
		}

		private void RelayForm_Load(object sender, EventArgs e)
		{
			switchLCMode();
		}

		private void switchLCMode() {

			if (mainForm.IsConnected)
			{
				setBusy(true);
				mainForm.SleepBetweenSend(1);
				mainForm.MyConnect.LightControlConnect(LCConnectCompleted, LCConnectError);
			}

		}

		/// <summary>
		/// 辅助回调方法：灯控连接成功
		/// </summary>
		/// <param name="obj"></param>
		public void LCConnectCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {
				Console.WriteLine(" --------------------------   ");
				setNotice( "已切换成灯控配置(connStatus=lc)", false, true);
				isConnLC = true;
				setBusy(false);
				

				// 当还没有任何形式地加载lcEntity时，主动从机器回读
				if (lcEntity == null) readButton_Click(null, null);
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


		#region  暂时屏蔽

		/// <summary>
		/// 事件：点击《开台》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openButton_Click(object sender, EventArgs e)
		{
			int relayTime = 1;

			Enabled = false;

			//relayButton1.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();
			//Thread.Sleep(relayTime * 1000);			
			//relayButton2.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();
			//Thread.Sleep(relayTime * 1000);
			//relayButton3.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();
			//Thread.Sleep(relayTime * 1000);
			//relayButton4.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();
			//Thread.Sleep(relayTime * 1000);
			//relayButton5.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();
			//Thread.Sleep(relayTime * 1000);
			//relayButton6.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();
			//Thread.Sleep(relayTime * 1000);
			//relayButton7.ImageKey = "Ok3w.Net图标15.png";
			//Refresh();

			Enabled = true;
		}

		/// <summary>
		/// 事件：点击《关台》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void closeButton_Click(object sender, EventArgs e)
		{

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
					setNotice(  "继电器配置回读异常(lcDataTemp==null)", true, true);
					setBusy(false);
					return;
				}
				lcEntity = lcDataTemp as LightControlData ;
				if (lcEntity.SequencerData == null) {
					setNotice("继电器配置回读异常,请确认该设备为最新的固件版本。", true, true);
					setBusy(false);
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

				//MARK 210513 切换失败时可能是连接出错，可以断开连接再重连；
				//reconnectDevice();
			});
		}

		private void relayRender() {

			try
			{
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
			for (int relayIndex = 0; relayIndex < relayCount; relayIndex++)
			{
				lcEntity.SequencerData.RelaySwitchNames[relayIndex]  = relayTBs[relayIndex].Text.Trim();
			}
			for (int timeIndex = 0; timeIndex < relayCount - 1; timeIndex++)
			{
				lcEntity.SequencerData.RelaySwitchDelayTimes[timeIndex] = decimal.ToInt32(timeNUDs[timeIndex].Value);
			}
			mainForm.MyConnect.LightControlDownload(lcEntity, LCDownloadCompleted, LCDownloadError);

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


		#region 通用方法

		/// <summary>
		/// 辅助方法：如果下载配置成功，则应该重连设备；此法被灯控和中控下载成功的回调函数调用；
		/// </summary>
		private void reconnectDevice()
		{
			mainForm.DisConnect();
			isConnLC = false;
			mainForm.ConnForm.ShowDialog();
			if (mainForm.IsConnected)
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

		#endregion


	}
}
