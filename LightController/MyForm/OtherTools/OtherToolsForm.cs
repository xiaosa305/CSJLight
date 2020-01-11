using CCWin.SkinControl;
using LightController.Common;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using NPOI;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using LightController.Entity;
using LightController.PeripheralDevice;
using static LightController.PeripheralDevice.BaseCommunication;
using System.Timers;
using LightController.Ast;
using LightController.Tools;
using System.Net;
using System.Net.Sockets;
using LightController.MyForm.OtherTools;

namespace OtherTools
{
	public partial class OtherToolsForm : Form
	{
		enum ConnectStatus
		{
			No,
			Normal,
			Lc,
			Cc,
			Kp
			//	,			
			//Tclc,
			//Tccc
		}

		private IList<Button> buttonList = new List<Button>();		
		private LightControlData lcEntity; //灯控封装对象
		private CCEntity ccEntity; // 中控封装对象
		private KeyEntity keyEntity;  // 墙板封装对象

		private bool isReadLC = false;
		private string protocolXlsPath = "C:\\Controller1.xls";
		private HSSFWorkbook xlsWorkbook;
		private IList<string> sheetList;

		private bool isKeepLightOn = false;
		private int lcFrameIndex = 0; // 灯控选中的场景，用以显示不同场景的灯光开启状态

		private ConnectStatus connStatus = ConnectStatus.No;
		
		private bool isDecoding = false; //中控是否开启解码
		private bool isKpShowDetails = true;

		private BaseCommunication myConnect;
		private IList<IPAst> ipaList;
		private ConnectTools connectTools;

		//private bool isConnected = false;  //是否已连上设备
		//private bool isOvertime = false; //是否超时
		//private bool isCCConnected = false;
		//private bool isDownloading = false;

		private System.Timers.Timer kpTimer; //墙板定时刷新的定时器（因为透传模式，若太久（10s）没有连接，则会自动退出透传模式）

		public OtherToolsForm()
		{
			InitializeComponent();					

			#region 初始化各组件		

			// 初始化灯控（强电）各配置
			qdFrameComboBox.SelectedIndex = 0;

			lightButtons[0] = lightButton1;
			lightButtons[1] = lightButton2;
			lightButtons[2] = lightButton3;
			lightButtons[3] = lightButton4;
			lightButtons[4] = lightButton5;
			lightButtons[5] = lightButton6;
			lightButtons[6] = lightButton7;
			lightButtons[7] = lightButton8;
			lightButtons[8] = lightButton9;
			lightButtons[9] = lightButton10;
			lightButtons[10] = lightButton11;
			lightButtons[11] = lightButton12;
			lightButtons[12] = lightButton13;
			lightButtons[13] = lightButton14;
			lightButtons[14] = lightButton15;
			lightButtons[15] = lightButton16;
			lightButtons[16] = lightButton17;
			lightButtons[17] = lightButton18;
			lightButtons[18] = lightButton19;
			lightButtons[19] = lightButton20;
			lightButtons[20] = lightButton21;
			lightButtons[21] = lightButton22;
			lightButtons[22] = lightButton23;
			lightButtons[23] = lightButton24;
			for (int i = 1; i < 24; i++)
			{
				lightButtons[i].Click += new System.EventHandler(this.lightButton_Click);
			}

			tgPanels[0] = tgPanel1;
			tgPanels[1] = tgPanel2;
			tgPanels[2] = tgPanel3;
			tgPanels[3] = tgPanel4;

			fanChannelComboBoxes[0] = fanChannelComboBox;
			fanChannelComboBoxes[1] = lowFanChannelComboBox;
			fanChannelComboBoxes[2] = midFanChannelComboBox;
			fanChannelComboBoxes[3] = highFanChannelComboBox;
			fanChannelComboBoxes[4] = fopenChannelComboBox;
			fanChannelComboBoxes[5] = fcloseChannelComboBox;			

			myInfoToolTip.SetToolTip(keepLightOnCheckBox, "选中常亮模式后，手动点亮或关闭每一个灯光通道，\n都会点亮或关闭所有场景的该灯光通道。");

			// 初始化墙板配置界面的TabControl
			tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
			tabControl1.Alignment = TabAlignment.Left;
			tabControl1.SizeMode = TabSizeMode.Fixed;
			tabControl1.Multiline = true;
			tabControl1.ItemSize = new Size(60, 100);

			#endregion
			
		}

		/// <summary>
		/// 刷新所有被connStatus影响的按键
		/// </summary>
		private void refreshButtons()
		{
			// 三个连接按键
			lcConnectButton.Enabled = connStatus > ConnectStatus.No;
			ccConnectButton.Enabled = connStatus > ConnectStatus.No;
			kpConnectButton.Enabled = connStatus > ConnectStatus.No;

			// 灯控相关按键
			lcReadButton.Enabled = connStatus == ConnectStatus.Lc;			
			lcDownloadButton.Enabled = connStatus == ConnectStatus.Lc && lcEntity != null ;
			//lcLoadButton.Enabled = connStatus == ConnectStatus.Lc;
			lcSaveButton.Enabled = ccEntity != null;

			// 灯控相关
			ccDecodeButton.Enabled = connStatus == ConnectStatus.Cc;
			ccDownloadButton.Enabled = connStatus == ConnectStatus.Cc && ccEntity != null && !isDecoding;

			// 墙板相关按键			
			kpReadButton.Enabled = connStatus == ConnectStatus.Kp;
			kpListenButton.Enabled = connStatus == ConnectStatus.Kp;
			bool keNotNull = keyEntity != null;
			kpSaveButton.Enabled = keNotNull;
			kpDownloadButton.Enabled = connStatus == ConnectStatus.Kp && keNotNull ;
		}

		/// <summary>
		/// 刷新所有被connStatus影响的statusLabel
		/// </summary>
		private void refreshStatusLabels()
		{
			switch (connStatus)
			{
				case ConnectStatus.Lc: setAllStatusLabel2("已切换为灯控配置"); break;
				case ConnectStatus.Cc: setAllStatusLabel2("已切换为中控模式"); break;
				case ConnectStatus.Kp: setAllStatusLabel2("已切换为墙板配置"); break;
				//case ConnectStatus.Tclc: setAllStatusLabel2("已切换为透传灯控模式"); break;
				//case ConnectStatus.Tccc: setAllStatusLabel2("已切换为透传中控模式"); break;
				default: setAllStatusLabel2(""); break;
			}

		}



		private void OtherToolsForm_Load(object sender, EventArgs e)
		{
			//直接刷新串口列表
			refreshDeviceComboBox();

			// 添加皮肤列表
			DirectoryInfo fdir = new DirectoryInfo(Application.StartupPath + "\\irisSkins");
			try
			{
				FileInfo[] file = fdir.GetFiles();
				if (file.Length > 0)
				{
					//TODO：禁用irisSkin皮肤的代码，不够完美，暂不启用。
					//skinComboBox.Items.Add("不使用皮肤");
					foreach (var item in file)
					{
						if (item.FullName.EndsWith(".ssk"))
						{
							skinComboBox.Items.Add(item.Name.Substring(0, item.Name.Length - 4));
						}
					}
					skinComboBox.SelectedIndex = 0;

					skinComboBox.Show();
					skinChangeButton.Show();					
				}
			}
			catch (Exception ex)
			{				
				Console.WriteLine(ex.Message);
			}

			
		}

		private void skinChangeButton_Click(object sender, EventArgs e)
		{
			string sskName = skinComboBox.Text;

			//TODO ：禁用皮肤的代码，还不够完美
			if (sskName.Equals("不使用皮肤")) {

				this.skinEngine1.Active = false;
				return;
			}

			this.skinEngine1.SkinFile = Application.StartupPath + "\\irisSkins\\" + sskName + ".ssk";
			//this.skinEngine1.Active = true;		
		}



		/// <summary>
		/// 事件：点击《灯光通道按键》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightButton_Click(object sender, EventArgs e)
		{
			if (!isReadLC)
			{
				return;
			}

			int lightIndex = MathAst.GetIndexNum(((SkinButton)sender).Name, -1);
			setLightButtonValue(lightIndex);
			//若勾选常亮模式，则需要主动把所有场景的选中灯光亮暗设为一致。
			if (isKeepLightOn) {
				bool tempLightOnMode = lcEntity.SceneData[lcFrameIndex, lightIndex];
				for (int frameIndex = 0; frameIndex < 17; frameIndex++) {
					lcEntity.SceneData[frameIndex, lightIndex] = tempLightOnMode;
				}
			}
			debugLC();
		}

		/// <summary>
		///  
		/// </summary>
		/// <param name="lightIndex"></param>
		private void setLightButtonValue(int lightIndex)
		{
			if (!isReadLC)
			{
				return;
			}
			lcEntity.SceneData[lcFrameIndex, lightIndex] = !lcEntity.SceneData[lcFrameIndex, lightIndex];
			lightButtons[lightIndex].ImageIndex = lcEntity.SceneData[lcFrameIndex, lightIndex] ? 1 : 0;
		}

		/// <summary>
		///  事件：点击《载入配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcLoadButton_Click(object sender, EventArgs e)
		{
			cfgOpenFileDialog.ShowDialog();
		}

		/// <summary>
		/// 事件：选中cfg文件后，点击确认
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cfgOpenFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			string cfgPath = cfgOpenFileDialog.FileName;
			loadLCParam(cfgPath);
		}

		/// <summary>
		/// 辅助方法，当《灯控配置(页)》加载cfg文件后，lcTagPage的其他控件才开始可用。
		/// </summary>
		private void enableLCInit()
		{
			this.isReadLC = true;
			this.lcSaveButton.Enabled = true;
			reloadLightGroupBox();
		}

		/// <summary>
		/// 辅助方法：读取lcParam，并加载到内存中lcData，然后渲染进Form中。
		/// </summary>
		/// <param name="paramList"></param>
		private void loadLCParam(string cfgPath)
		{
			IList<string> paramList = getParamListFromPath(cfgPath);
			lcEntity = new LightControlData(paramList);
			lcSetForm();
			lcToolStripStatusLabel2.Text = " 已加载配置文件：" + cfgPath;
		}

		private void lcSetForm() {

			if (lcEntity == null) {
				//MessageBox.Show("lcEntity==null");
				Console.WriteLine("lcEntity为null。");
				lightGroupBox.Enabled = false;
				tgGroupBox.Enabled = false;
				lcGroupBox3.Enabled = false;
				lcGroupBox4.Enabled = false;
				lcGroupBox5.Enabled = false;
				refreshButtons();
				return;
			}

			try
			{
				lightGroupBox.Enabled = true;
				tgGroupBox.Enabled = true;
				lcGroupBox3.Enabled = true;
				lcGroupBox4.Enabled = true;
				lcGroupBox5.Enabled = true;

				switch (lcEntity.LightMode)
				{
					case 0: lightModeDJRadioButton.Checked = true; break;
					case 1: lightModeQHRadioButton.Checked = true; break;
				}

				switch (lcEntity.AirControlSwitch)
				{
					case 0: fjJYRadioButton.Checked = true; break;
					case 1: fjDXFRadioButton.Checked = true; break;
					case 2: fjSXFRadioButton.Checked = true; break;
				}

				if (lcEntity.RelayCount == 0)
				{
					lightGroupBox.Enabled = false;
				}
				else
				{
					foreach (SkinButton btn in lightButtons)
					{
						btn.Visible = false;
					}
					for (int relayIndex = 0; relayIndex < lcEntity.RelayCount; relayIndex++)
					{
						lightButtons[relayIndex].Visible = true;
					}
				}

				//TODO : 直接通过通道数量，把后六位给固定的排风及空调通道
				//for (int lightIndex = 0; lightIndex < 6;  lightIndex++) {
				//		fanChannelComboBoxes[lightIndex].Items.Add("通道"+ (lcEntity.RelayCount - 6 + lightIndex + 1) );
				//		fanChannelComboBoxes[lightIndex].SelectedIndex = 0;
				//}

				fanChannelComboBox.Items.Clear();
				fanChannelComboBox.Items.Add("通道"+(lcEntity.FanChannel + 1));
				fanChannelComboBox.SelectedIndex = 0;

				lowFanChannelComboBox.Items.Clear();
				lowFanChannelComboBox.Items.Add("通道" +( lcEntity.LowFanChannel + 1));
				lowFanChannelComboBox.SelectedIndex = 0;

				highFanChannelComboBox.Items.Clear();
				highFanChannelComboBox.Items.Add("通道" + (lcEntity.HighFanChannel + 1));
				highFanChannelComboBox.SelectedIndex = 0;

				midFanChannelComboBox.Items.Clear();
				midFanChannelComboBox.Items.Add("通道" + (lcEntity.MiddleFanChannel + 1 ));
				midFanChannelComboBox.SelectedIndex = 0;

				fopenChannelComboBox.Items.Clear();
				fopenChannelComboBox.Items.Add("通道" + ( lcEntity.OpenAirConditionChannel+ 1));
				fopenChannelComboBox.SelectedIndex = 0;

				fcloseChannelComboBox.Items.Clear();
				fcloseChannelComboBox.Items.Add("通道" + (lcEntity.CloseAirConditionChannel +1) );
				fcloseChannelComboBox.SelectedIndex = 0;

				enableLCInit();
				enableAirCondition();
				enableFan();

				refreshButtons();
			}
			catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
		}

		private void setFanChannel(airModeEnum airMode, int fanChannel)
		{
			fanChannelComboBoxes[(int)airMode].SelectedIndex = fanChannel;
		}

		/// <summary>
		/// 空调或排风模式的枚举
		/// </summary>
		enum airModeEnum
		{
			FAN,
			HIGH,
			MID,
			LOW,
			FOPEN,
			FCLOSE
		}

		/// <summary>
		/// 辅助方法：是否使用排风
		/// </summary>
		/// <param name="isOpenFan"></param>
		private void enableFan()
		{
			fanChannelComboBox.Enabled = lcEntity.IsOpenFan;
			int lightIndex = Convert.ToInt16(fanChannelComboBox.Text.Substring(2)) -1 ;
			lightButtons[lightIndex].Visible = !lcEntity.IsOpenFan;
			fanButton.Text = lcEntity.IsOpenFan ? "点击禁用\r\n排风通道" : "点击启用\r\n排风通道";
		}

		// <summary>
		/// 辅助方法：是否使用空调
		/// </summary>
		/// <param name="isOpenAirCondition"></param>
		private void enableAirCondition()
		{
			highFanChannelComboBox.Enabled = lcEntity.IsOpenAirCondition;
			midFanChannelComboBox.Enabled = lcEntity.IsOpenAirCondition;
			lowFanChannelComboBox.Enabled = lcEntity.IsOpenAirCondition;
			fopenChannelComboBox.Enabled = lcEntity.IsOpenAirCondition;
			fcloseChannelComboBox.Enabled = lcEntity.IsOpenAirCondition;

			// 在启用或禁用空调后，应该让空调占用的通道隐藏或显示出来，为避免错误，先用一个三目运算取出相应的数据。			
			for (int lightIndex = 1; lightIndex < 6; lightIndex++)
			{
				int tempIndex = lcEntity.RelayCount - 6 + lightIndex;
				lightButtons[tempIndex].Visible = !lcEntity.IsOpenAirCondition;
			}
			acButton.Text = lcEntity.IsOpenAirCondition ? "点击禁用\r\n空调通道" : "点击启用\r\n空调通道";
		}

		/// <summary>
		/// 事件：监听选择通道，若选中的通道已被占用，则恢复原先设置。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fanChannelComboBoxes_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox cb = (ComboBox)sender;
			//MessageBox.Show(cb.Text);

			int tempIndex = cb.SelectedIndex;
			cb.SelectedIndex = tempIndex;
		}

		/// <summary>
		/// 事件：更改了场景之后，重新填充灯光通道数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void qdFrameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			lcFrameIndex = qdFrameComboBox.SelectedIndex;
			reloadLightGroupBox();
		}

		/// <summary>
		/// 辅助方法：当场景编号改变时，动态加载内存中的lcData.SceneData[selectedFrameIndex]；
		/// </summary>
		private void reloadLightGroupBox()
		{
			if (!isReadLC)
			{
				return;
			}

			for (int relayIndex = 0; relayIndex < lcEntity.RelayCount; relayIndex++)
			{
				lightButtons[relayIndex].ImageIndex = lcEntity.SceneData[lcFrameIndex, relayIndex] ? 1 : 0;
			}
			debugLC();
		}

		// 向设备发送当前场景的灯光通道数据。
		private void debugLC() {
					
			if (connStatus != ConnectStatus.Lc) {				
				return;
			}

			byte[] tempData = lcEntity.GetFrameBytes(lcFrameIndex);
			if (tcCheckBox.Checked) {
				myConnect.PassThroughLightControlDebug(tempData, LCSendCompleted, LCSendError);
			}
			else
			{
				myConnect.LightControlDebug(tempData, LCSendCompleted, LCSendError);
			}

		}

		/// <summary>
		/// 事件：切换《灯光模式》的选择项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightModeRadioButton_Click(object sender, EventArgs e)
		{
			if (!isReadLC)
			{
				return;
			}

			RadioButton radio = (RadioButton)sender;
			lcEntity.LightMode = Convert.ToInt16(radio.Tag);
		}

		/// <summary>
		/// 事件：切换《风机》的选择项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fjRadioButton_Click(object sender, EventArgs e)
		{
			if (!isReadLC)
			{
				return;
			}

			RadioButton radio = (RadioButton)sender;
			lcEntity.AirControlSwitch = Convert.ToInt16(radio.Tag);
		}

		/// <summary>
		///  事件：点击《启用、禁用排风通道》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fanButton_Click(object sender, EventArgs e)
		{
			if (!isReadLC)
			{
				return;
			}

			lcEntity.IsOpenFan = !lcEntity.IsOpenFan;
			enableFan();
		}

		/// <summary>
		/// 事件：点击《启用、禁用空调通道》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void acButton_Click(object sender, EventArgs e)
		{
			if (!isReadLC)
			{
				return;
			}

			lcEntity.IsOpenAirCondition = !lcEntity.IsOpenAirCondition;
			enableAirCondition();
		}


		/// <summary>
		/// 事件：点击《保存配置按钮》
		/// -- 在此情况下，无法再考虑用户体验了，会自动将lcData.SceneData中启用了排风或空调的通道所有Frame都设为false
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcSaveButton_Click(object sender, EventArgs e)
		{
			cfgSaveFileDialog.ShowDialog();
		}

		/// <summary>
		/// 事件：确认《保存配置》对话框
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cfgSaveFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			string cfgPath = cfgSaveFileDialog.FileName;

			// 保存之前，因为设备处理的原因，需要把屏蔽掉的通道（启用排风或空调）的SceneData都设为false（若未保存，则无需如此；可供用户正常使用，避免误删）
			for (int frame = 0; frame < 17; frame++)
			{
				if (lcEntity.IsOpenFan) {				
					lcEntity.SceneData[frame, lcEntity.FanChannel] = false;
				}
				if (lcEntity.IsOpenAirCondition)
				{
					lcEntity.SceneData[frame, lcEntity.LowFanChannel] = false;
					lcEntity.SceneData[frame, lcEntity.MiddleFanChannel] = false;
					lcEntity.SceneData[frame, lcEntity.HighFanChannel] = false;
					lcEntity.SceneData[frame, lcEntity.OpenAirConditionChannel] = false;
					lcEntity.SceneData[frame, lcEntity.CloseAirConditionChannel] = false;
				}			
			}

			lcEntity.WriteToFile(cfgPath);

			//保存成功后，手动重新加载一下LightGroupBox(因为没有更换场景）
			reloadLightGroupBox();
			MessageBox.Show("成功保存配置文件(" + cfgPath + ")");
			lcToolStripStatusLabel2.Text = "成功保存配置文件(" + cfgPath + ")";
		}


		/// <summary>
		/// 事件：选中不同的《空调模式》radioButton
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ktRadioButton_Click(object sender, EventArgs e)
		{

		}


		/// <summary>
		///  事件：点击《下载配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcDownloadButton_Click(object sender, EventArgs e)
		{
			if (myConnect == null) {
				lcToolStripStatusLabel2.Text = "当前myConnect==null，无法下载数据";
				return;				
			}

			if (tcCheckBox.Checked) {
				myConnect.PassThroughLightControlDownload(lcEntity, LCDownloadCompleted, LCDownloadError);
			}
			else
			{
				myConnect.LightControlDownload(lcEntity, LCDownloadCompleted, LCDownloadError);
			}
		
		}


		/// <summary>
		/// 事件：点击《加载协议文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loadProtocolButton_Click(object sender, EventArgs e)
		{
			using (FileStream file = new FileStream(protocolXlsPath, FileMode.Open, FileAccess.Read))
			{
				xlsWorkbook = new HSSFWorkbook(file);
			}
			sheetList = new List<string>();
			protocolComboBox.Items.Clear();
			protocolComboBox.SelectedIndex = -1;
			for (int protocolIndex = 0; protocolIndex < xlsWorkbook.NumberOfSheets; protocolIndex++)
			{
				ISheet sheet = xlsWorkbook.GetSheetAt(protocolIndex);
				sheetList.Add(sheet.SheetName);
				protocolComboBox.Items.Add(sheet.SheetName);
			}

			if (sheetList.Count > 0)
			{
				protocolComboBox.SelectedIndex = 0;
				isReadXLS = true;
				reloadProtocolListView();
				ccToolStripStatusLabel2.Text = "已加载xls文件：" + protocolXlsPath;
			}
			else
			{
				isReadXLS = false;
				MessageBox.Show("请检查打开的xls文件是否正确，该文件的Sheet数量为0。");
				ccToolStripStatusLabel2.Text = "加载xls文件失败。";
			}
		}


		/// <summary>
		///  事件：点击《开启|关闭解码》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ccDecodeButton_Click(object sender, EventArgs e)
		{
			// 点击《关闭解码》
			if (isDecoding)
			{
				myConnect.CenterControlStopCopy(CCStopCompleted, CCStopError);
			}
			// 点击《开启解码》
			else {
				myConnect.CenterControlStartCopy(CCStartCompleted, CCStartError, CCListen);
				
			}
		}

		/// <summary>
		///  事件：点击《清空解码》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void clearDecodeButton_Click(object sender, EventArgs e)
		{
			ccDecodeRichTextBox.Clear();
		}

		/// <summary>
		/// 事件：点击《编辑协议》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void protocolEditButton_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(protocolXlsPath);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private bool isReadXLS = false;
		private void protocolComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isReadXLS)
			{
				return;
			}
			reloadProtocolListView();
		}

		/// <summary>
		/// 辅助方法：重新加载protocolListView的数据
		/// </summary>
		private void reloadProtocolListView()
		{
			if (protocolComboBox.SelectedIndex == -1)
			{
				return;
			}

			ccEntity = generateCC();
			com0Label.Text = "串口0 = " + ccEntity.Com0;
			com1Label.Text = "串口1 = " + ccEntity.Com1;
			PS2Label.Text = "PS2 = " + ccEntity.PS2;

			protocolListView.Items.Clear();
			for (int rowIndex = 0; rowIndex < ccEntity.CCDataList.Count; rowIndex++)
			{
				ListViewItem item = new ListViewItem((rowIndex + 1).ToString());
				item.SubItems.Add(ccEntity.CCDataList[rowIndex].Function.Trim());
				item.SubItems.Add(ccEntity.CCDataList[rowIndex].Code.Trim());
				item.SubItems.Add(ccEntity.CCDataList[rowIndex].Com0Up.Trim());
				item.SubItems.Add(ccEntity.CCDataList[rowIndex].Com0Down.Trim());
				item.SubItems.Add(ccEntity.CCDataList[rowIndex].Com1Up.Trim());
				item.SubItems.Add(ccEntity.CCDataList[rowIndex].Com1Down.Trim());
				item.SubItems.Add(ccEntity.CCDataList[rowIndex].InfraredSend.Trim());
				item.SubItems.Add(ccEntity.CCDataList[rowIndex].InfraredReceive.Trim());
				item.SubItems.Add(ccEntity.CCDataList[rowIndex].PS2Up.Trim());
				item.SubItems.Add(ccEntity.CCDataList[rowIndex].PS2Down.Trim());
				protocolListView.Items.Add(item);
			}

			refreshButtons();
		}


		private void bigTestButton2_Click(object sender, EventArgs e)
		{
			MessageBox.Show(StringHelper.HexStringToDecimal("ff"));
		}

		/// <summary>
		///  事件：点击《中控-下载数据》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ccDownloadButton_Click(object sender, EventArgs e)
		{
			if (ccEntity == null)
			{
				MessageBox.Show("请先加载xls文件并选择协议。");
				return;
			}

			ccToolStripStatusLabel2.Text = "正在下载中控数据，请稍候...";
			myConnect.CenterControlDownload(ccEntity, CCDownloadCompleted, CCDownloadError);
			
		}

		/// <summary>
		/// 辅助方法：根据当前选中的协议生成CCEntity数据( 供下载数据及搜索数据使用）
		/// </summary>
		/// <returns></returns>
		private CCEntity generateCC()
		{

			if (!isReadXLS || protocolComboBox.SelectedIndex == -1)
			{
				return null;
			}

			CCEntity cc = new CCEntity();
			ISheet sheet = xlsWorkbook.GetSheetAt(protocolComboBox.SelectedIndex);
			cc.ProtocolName = sheet.SheetName;
			System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
			// 处理通用数据(com0,com1,ps2)
			rows.MoveNext();
			IRow row = (HSSFRow)rows.Current;
			ICell cell = row.GetCell(0);
			cc.Com0 = Convert.ToInt16(cell.ToString().Substring(4));
			rows.MoveNext();
			row = (HSSFRow)rows.Current;
			cell = row.GetCell(0);
			cc.Com1 = Convert.ToInt16(cell.ToString().Substring(4));
			rows.MoveNext();
			row = (HSSFRow)rows.Current;
			cell = row.GetCell(0);
			cc.PS2 = cell.ToString().Equals("PS2=主") ? 0 : 1;
			rows.MoveNext();

			//逐一处理每一行的数据
			int rowIndex = 0;
			while (rows.MoveNext())
			{
				row = (HSSFRow)rows.Current;

				CCData ccData = new CCData();
				cell = row.GetCell(0);
				ccData.Function = (cell == null ? "" : cell.ToString().Trim());
				cell = row.GetCell(1);
				ccData.Code = (cell == null ? "" : cell.ToString().Trim());
				cell = row.GetCell(2);
				ccData.Com0Up = (cell == null ? "" : cell.ToString().Trim());
				cell = row.GetCell(3);
				ccData.Com0Down = (cell == null ? "" : cell.ToString().Trim());
				cell = row.GetCell(4);
				ccData.Com1Up = (cell == null ? "" : cell.ToString().Trim());
				cell = row.GetCell(5);
				ccData.Com1Down = (cell == null ? "" : cell.ToString().Trim());
				cell = row.GetCell(6);
				ccData.InfraredSend = (cell == null ? "" : cell.ToString().Trim());
				cell = row.GetCell(7);
				ccData.InfraredReceive = (cell == null ? "" : cell.ToString().Trim());
				cell = row.GetCell(8);
				ccData.PS2Up = (cell == null ? "" : cell.ToString().Trim());
				cell = row.GetCell(9);
				ccData.PS2Down = (cell == null ? "" : cell.ToString().Trim());

				cc.CCDataList.Add(ccData);
				rowIndex++;
			}

			return cc;
		}


		/// <summary>
		/// 事件：点击《中控--搜索》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ccSearchButton_Click(object sender, EventArgs e)
		{
			// 检查代码的顺序，会影响程序的效率
			// 检查关键字
			string keyword = ccSearchTextBox.Text.Trim();
			if (keyword.Equals(""))
			{
				MessageBox.Show("请输入搜索关键字。");
				return;
			}

			// 检查是否已载入协议
			if (ccEntity == null)
			{
				MessageBox.Show("请先加载协议(cc为空)。");
				return;
			}

			// 清空之前的选择项
			foreach (int seletedIndex in protocolListView.SelectedIndices)
			{
				protocolListView.Items[seletedIndex].Selected = false;
			}

			// 由关键字搜索相应的indexList ，再遍历选中所有匹配项
			IList<int> matchIndexList = ccEntity.SearchIndices(keyword);
			foreach (int matchIndex in matchIndexList)
			{
				protocolListView.Items[matchIndex].Selected = true;
			}
			// 最后恢复listView的焦点
			protocolListView.Select();
		}

		/// <summary>
		/// 事件：手动点选相关的listView项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void protocolListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 必须判断这个字段(Count)，否则会报异常
			if (protocolListView.SelectedIndices.Count > 0)
			{
				ListViewItem item = protocolListView.SelectedItems[0];
				functionTextBox.Text = item.SubItems[1].Text;
				com0UpTextBox.Text = item.SubItems[3].Text;
				com0DownTextBox.Text = item.SubItems[4].Text;
				com1UpTextBox.Text = item.SubItems[5].Text;
				com1DownTextBox.Text = item.SubItems[6].Text;
				infraredSendTextBox.Text = item.SubItems[7].Text;
				infraredReceiveTextBox.Text = item.SubItems[8].Text;
				ps2UpTextBox.Text = item.SubItems[9].Text;
				ps2DownTextBox.Text = item.SubItems[10].Text;
			}
		}

		/// <summary>
		/// 重绘控件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
		{
			Rectangle tabArea = tabControl1.GetTabRect(e.Index);//主要是做个转换来获得TAB项的RECTANGELF
			RectangleF tabTextArea = (RectangleF)(tabControl1.GetTabRect(e.Index));
			Graphics g = e.Graphics;
			StringFormat sf = new StringFormat();//封装文本布局信息
			sf.LineAlignment = StringAlignment.Center;
			sf.Alignment = StringAlignment.Center;
			Font font = this.tabControl1.Font;
			SolidBrush brush = new SolidBrush(Color.Black);//绘制边框的画笔
			g.DrawString(((TabControl)(sender)).TabPages[e.Index].Text, font, brush, tabTextArea, sf);
		}		

		/// <summary>
		///  事件：点击《墙板-图标|列表显示》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpShowButton_Click(object sender, EventArgs e)
		{
			isKpShowDetails = !isKpShowDetails;

			keypressListView.View = isKpShowDetails ? View.Details : View.LargeIcon;		
			kpShowButton.Text = isKpShowDetails ? "显示图标" : "显示列表";

			kpRearrangeButton.Visible = !isKpShowDetails;
			kpPositonSaveButton.Visible = !isKpShowDetails;
			kpPositonLoadButton.Visible = !isKpShowDetails;
		}

		/// <summary>
		/// 事件：点击《墙板-重新排列》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpRearrangeButton_Click(object sender, EventArgs e)
		{
			keypressListView.AutoArrange = true;
			keypressListView.AutoArrange = false;
		}


		/// <summary>
		/// 事件：点击《墙板-读取文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpLoadButton_Click(object sender, EventArgs e)
		{
			keyOpenFileDialog.ShowDialog();
		}

		/// <summary>
		/// 事件：《墙板-读取文件》打开key文件OK后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void keyOpenFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			string keyPath = keyOpenFileDialog.FileName;
			keyEntity = loadKeyFile(keyPath);
			if (keyEntity == null) {
				kpToolStripStatusLabel2.Text = "加载配置文件出错。";
				return;
			}

			reloadKeypressListView();
			refreshButtons();
			kpToolStripStatusLabel2.Text = "已加载配置文件：" + keyPath;
		}

		private void reloadKeypressListView()
		{
			keypressListView.Items.Clear();
			keypressListView.Enabled = true;
			for (int keyIndex = 0; keyIndex < 24; keyIndex++)
			{
				if (keyEntity.Key0Array[keyIndex].Equals("00") && keyEntity.Key1Array[keyIndex].Equals("00")) {
					continue;
				}
				ListViewItem item = new ListViewItem("键序" + (keyIndex + 1).ToString() + "\n" + keyEntity.Key0Array[keyIndex] + ":" + keyEntity.Key1Array[keyIndex]);
				item.ImageIndex = 2;
				item.SubItems.Add((keyIndex + 1).ToString());
				item.SubItems.Add(keyEntity.Key0Array[keyIndex]);
				item.SubItems.Add(keyEntity.Key1Array[keyIndex]);
				keypressListView.Items.Add(item);
			}
		}

		/// <summary>
		/// 辅助方法：由key文件，加载数据到keyEntity
		/// </summary>
		/// <param name="keyPath"></param>
		/// <returns></returns>
		private KeyEntity loadKeyFile(string keyPath)
		{
			IList<string> paramList = getParamListFromPath(keyPath);
			if (paramList == null || paramList.Count != 50)
			{
				MessageBox.Show("key文件有错误，无法加载。");
				return null;
			}

			KeyEntity ke = new KeyEntity();
			for (int i = 0; i < 24; i++)
			{
				ke.Key0Array[i] = StringHelper.DecimalStringToBitHex(paramList[i], 2);
				ke.Key1Array[i] = StringHelper.DecimalStringToBitHex(paramList[i + 24], 2);
			}
			ke.CRC = paramList[48] + paramList[49];
			return ke;
		}

		/// <summary>
		/// 辅助方法：通过filePath，读取其内部数据（基本上相同的文件存储格式)，组成IList<string>并返回。
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		private IList<string> getParamListFromPath(string filePath)
		{
			IList<string> paramList = new List<string>();
			try
			{
				if (File.Exists(filePath))
				{
					using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
					using (StreamReader sr = new StreamReader(fs))
					{
						string sTemp;
						while ((sTemp = sr.ReadLine()) != null)
						{
							sTemp = sTemp.Trim();
							if (sTemp.Length > 0)
							{
								paramList.Add(sTemp);
							}
						}
					}
				}
				else
				{
					MessageBox.Show("文件不存在。}");
					return null;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
			return paramList;
		}

		/// <summary>
		/// 事件：选择不同的按键，可以修改其键值（）-》原版本无此功能。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void keypressListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 必须判断这个字段(Count)，否则会报异常
			if (keypressListView.SelectedIndices.Count > 0)
			{
				setKpTextBoxes(keypressListView.SelectedIndices[0]);
			}
		}

		/// <summary>
		/// 根据keyIndex,填充相应的数据到几个TextBox中
		/// </summary>
		/// <param name="keyIndex"></param>
		private void setKpTextBoxes(int keyIndex)
		{
			kpOrderTextBox.Text = keypressListView.Items[keyIndex].SubItems[1].Text;
			kpKey0TextBox.Text = keypressListView.Items[keyIndex].SubItems[2].Text;
			kpKey1TextBox.Text = keypressListView.Items[keyIndex].SubItems[3].Text;
		}

		/// <summary>
		/// 事件：点击《修改键码》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpEditButton_Click(object sender, EventArgs e)
		{
			if (keypressListView.SelectedIndices.Count == 0) {
				MessageBox.Show("请先选择需要设置键码值的按键。");
				return;
			}
			if (kpKey0TextBox.Text.Length == 0) {
				MessageBox.Show("键码值0不得为空。");
				return;
			}

			// 处理TextBox内容，填入相应的listView中
			int keyIndex = keypressListView.SelectedIndices[0];		
			keypressListView.Items[keyIndex].SubItems[2].Text = kpKey0TextBox.Text.ToLower().PadLeft(2, '0');			
			if (kpKey1TextBox.Text.Length == 0)
			{
				keypressListView.Items[keyIndex].SubItems[3].Text = kpKey0TextBox.Text.ToLower().PadLeft(2, '0');				
			}
			else {
				keypressListView.Items[keyIndex].SubItems[3].Text = kpKey1TextBox.Text.ToLower().PadLeft(2, '0');
			}

			// 将改变后的值填入keyEntity中
			int keyArrayIndex = Convert.ToInt16(kpOrderTextBox.Text) - 1; //keyEntity中的array索引号
			keyEntity.Key0Array[keyArrayIndex] = kpKey0TextBox.Text;
			keyEntity.Key1Array[keyArrayIndex] = kpKey1TextBox.Text;

		}


		/// <summary>
		/// 辅助方法：两个自定义墙板键码值的输入文字的验证。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpKeyTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar >= '0' && e.KeyChar <= '9')
				 || (e.KeyChar >= 'a' && e.KeyChar <= 'f')
				 || (e.KeyChar >= 'A' && e.KeyChar <= 'F')
				 || e.KeyChar == 8)
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}

		/// <summary>
		/// 事件：勾选《灯控 - 常亮模式》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcKeepLightOnCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			isKeepLightOn = keepLightOnCheckBox.Checked;
		}


		private bool isConnectByCom = true;
		/// <summary>
		///  事件：点击《切换连接方式》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void switchButton_Click(object sender, EventArgs e)
		{
			// 切换前都先断开连接
			if (myConnect != null) {
				if (connStatus > ConnectStatus.No)
				{
					myConnect.DisConnect();
				}
				setConnStatus(ConnectStatus.No);
				myConnect = null ;
			}
			
			isConnectByCom = !isConnectByCom;
			switchButton.Text = isConnectByCom ? "以网络连接" : "以串口连接";
			refreshButton.Text = isConnectByCom ?  "刷新串口" : "刷新网络";
			connectButton.Text = isConnectByCom ?  "打开串口" : "连接设备";
			refreshDeviceComboBox();
		}

		/// <summary>
		/// 辅助方法：通过不同的isConnectByCom来刷新deviceComboBox。
		/// </summary>
		private void refreshDeviceComboBox()
		{
			deviceComboBox.Items.Clear();

			// 获取串口列表（不代表一定能连上，串口需用户自行确认）
			if (isConnectByCom)
			{
				if (myConnect == null) {
					myConnect = new SerialConnect();
				}

				List<string> comList = (myConnect as SerialConnect).GetSerialPortNames();
				if (comList == null || comList.Count == 0)
				{
					//MessageBox.Show();
					return;
				}

				foreach (string comName in comList) {
					deviceComboBox.Items.Add(comName);
				}
			}
			// 获取网络设备列表
			else {
				connectTools = ConnectTools.GetInstance();
				IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
				ipaList = new List<IPAst>();

				setAllStatusLabel1("正在搜索网络设备，请稍候...");
				foreach (IPAddress ip in ipe.AddressList)
				{
					if (ip.AddressFamily == AddressFamily.InterNetwork) //当前ip为ipv4时，才加入到列表中
					{
						connectTools.Start(ip.ToString());
						connectTools.SearchDevice();
						// 需要延迟片刻，才能找到设备;	故在此期间，主动暂停片刻
						Thread.Sleep(500);						
					}					
				}

				Dictionary<string, Dictionary<string, NetworkDeviceInfo>> allDevices = connectTools.GetDeivceInfos();
				if (allDevices.Count > 0)
				{
					foreach (KeyValuePair<string, Dictionary<string, NetworkDeviceInfo>> device in allDevices)
					{
						foreach (KeyValuePair<string, NetworkDeviceInfo> d2 in device.Value)
						{
							string localIPLast = device.Key.ToString().Substring(device.Key.ToString().LastIndexOf("."));
							deviceComboBox.Items.Add(d2.Value.DeviceName + "(" + d2.Value.DeviceIp + ")" + localIPLast);
							ipaList.Add(new IPAst()	{LocalIP = device.Key,DeviceIP = d2.Value.DeviceIp,DeviceName = d2.Value.DeviceName});
						}
					}
				}
			}

			if (deviceComboBox.Items.Count == 0)
			{
				MessageBox.Show("未找到可用设备，请检查设备连接后重试。");
				setAllStatusLabel1("未找到可用设备，请检查设备连接后重试。");				
				connectButton.Enabled = false;
				deviceComboBox.Text = "";
				deviceComboBox.Enabled = false;
				setConnStatus(ConnectStatus.No);
			}
			else {
				connectButton.Enabled = true;
				deviceComboBox.Enabled = true;
				deviceComboBox.SelectedIndex = 0;
				setAllStatusLabel1("已搜到可用设备列表。");
				setConnStatus(ConnectStatus.No);
			}
		}

		//事件：点击《刷新串口|网络》
		private void refreshButton_Click(object sender, EventArgs e)
		{
			refreshDeviceComboBox();
		}

		private void connectButton_Click(object sender, EventArgs e)
		{
			if (isConnectByCom)
			{
				if (myConnect == null) {
					setAllStatusLabel1("打开串口失败，原因是：myConnect为null");
					setConnStatus(ConnectStatus.No);
					return;
				}
				try
				{
					(myConnect as SerialConnect).OpenSerialPort(deviceComboBox.Text);
					setAllStatusLabel1("已打开串口(" + deviceComboBox.Text + ")");
					setConnStatus(ConnectStatus.Normal);
				}
				catch (Exception ex) {
					setAllStatusLabel1("打开串口失败，原因是：" + ex.Message);
					setConnStatus(ConnectStatus.No);
				}
			}
			else {
				string localIP =  ipaList[deviceComboBox.SelectedIndex].LocalIP; 
				string deviceIP = ipaList[deviceComboBox.SelectedIndex].DeviceIP;
				string deviceName = ipaList[deviceComboBox.SelectedIndex].DeviceName;
				myConnect = new NetworkConnect( connectTools.GetDeivceInfos()[localIP][deviceIP]);
				if ((myConnect as NetworkConnect).IsConnected())
				{					
					setAllStatusLabel1("成功连接网络设备(" + deviceName + ")");
					setConnStatus(ConnectStatus.Normal);
				}
				else {
					setAllStatusLabel1("连接网络设备(" + deviceName + ")失败");
					setConnStatus(ConnectStatus.No);
				}
			}
		}
	

		/// <summary>
		/// 辅助方法：统一设置左侧的状态栏的显示信息
		/// </summary>
		/// <param name="msg"></param>
		private void setAllStatusLabel1(string msg) {
			lcToolStripStatusLabel1.Text = msg;
			ccToolStripStatusLabel1.Text = msg;
			kpToolStripStatusLabel1.Text = msg;
		}

		/// <summary>
		/// 辅助方法：统一设置右侧的状态栏的显示信息
		/// </summary>
		/// <param name="msg"></param>
		private void setAllStatusLabel2(string msg)
		{
			lcToolStripStatusLabel2.Text = msg;
			ccToolStripStatusLabel2.Text = msg;
			kpToolStripStatusLabel2.Text = msg;
		}

		/// <summary>
		/// 事件：点击《灯控 - 回读配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcReadButton_Click(object sender, EventArgs e)
		{
			if (tcCheckBox.Checked) {
				myConnect.PassThroughLightControlRead(LCReadCompleted, LCReadError);
				lcToolStripStatusLabel2.Text = "正在回读灯控配置(tc)，请稍候...";
			}
			else
			{
				myConnect.LightControlRead(LCReadCompleted, LCReadError);
				lcToolStripStatusLabel2.Text = "正在回读灯控配置，请稍候...";
			}

		}

		/// <summary>
		/// 辅助回调方法：灯控连接成功
		/// </summary>
		/// <param name="obj"></param>
		public void LCConnectCompleted(Object obj) {
			Invoke((EventHandler)delegate {
				lcToolStripStatusLabel2.Text = "已切换成灯控配置(connStatus=lc" + (tcCheckBox.Checked?"-tc":"") +")";
				setConnStatus(ConnectStatus.Lc);
				lcReadButton_Click(null, null);
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控连接出错
		/// </summary>
		/// <param name="obj"></param>
		public void LCConnectError() {
			Invoke((EventHandler)delegate
			{
				MessageBox.Show("请求超时，切换灯控配置失败");
				lcToolStripStatusLabel2.Text = "请求超时，切换灯控配置失败";
			});
		}

		/// <summary>
		/// 辅助回调方法：中控连接成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCConnectCompleted(Object obj)
		{
			Invoke((EventHandler)delegate {
				ccToolStripStatusLabel2.Text = "已切换成中控配置(connStatus=cc)";
				setConnStatus(ConnectStatus.Cc);
			});
		}

		/// <summary>
		/// 辅助回调方法：中控连接失败
		/// </summary>
		public void CCConnectError()
		{
			Invoke((EventHandler)delegate {
				ccToolStripStatusLabel2.Text = "切换中控配置失败";
				refreshButtons();
			});
		}

		/// <summary>
		///  辅助回调方法：灯控debug(实时调试的数据)发送成功
		/// </summary>
		/// <param name="obj"></param>
		public void LCSendCompleted(Object obj)
		{
			Invoke((EventHandler)delegate
			{
				Console.WriteLine("灯控debug(实时调试的数据)发送成功");
			});
		}

		/// <summary>
		///  辅助回调方法：灯控debug发送出错
		/// </summary>
		/// <param name="obj"></param>
		public void LCSendError()
		{
			Invoke((EventHandler)delegate
			{
				lcToolStripStatusLabel2.Text = "灯控已离线，发送debug数据失败，请重新连接后重试";
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控数据回读成功
		/// </summary>
		/// <param name="lcDataTemp"></param>
		public void LCReadCompleted(Object lcDataTemp)
		{
			Invoke((EventHandler)delegate {
				if (lcDataTemp == null)
				{
					MessageBox.Show("灯控回读配置异常(lcDataTemp==null)");
					lcToolStripStatusLabel2.Text = "灯控回读配置异常(lcDataTemp==null)";
					return;
				}

				lcEntity = lcDataTemp as LightControlData;
				lcSetForm();								
				lcToolStripStatusLabel2.Text = "成功回读灯控配置";
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控配置回读失败
		/// </summary>
		public void LCReadError()
		{
			Invoke((EventHandler)delegate {
				lcToolStripStatusLabel2.Text = "回读灯控配置失败";
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控配置下载成功
		/// </summary>
		/// <param name="obj"></param>
		public void LCDownloadCompleted(Object obj)
		{
			Invoke((EventHandler)delegate {
				if (isConnectByCom)
				{
					lcToolStripStatusLabel2.Text = "灯控配置下载成功,请等待机器重启(约耗时5s)...";
					MessageBox.Show("灯控配置下载成功,请等待机器重启(约耗时5s)。");
					Thread.Sleep(5000);
					lcConnectButton_Click(null, null);
				}
				else {
					lcToolStripStatusLabel2.Text = "灯控配置下载成功,请等待机器重启(约耗时5s)，并重新搜索连接网络设备。";
					MessageBox.Show("灯控配置下载成功,请等待机器重启(约耗时5s)，并重新搜索连接网络设备。");
					Thread.Sleep(5000);										
					setConnStatus(ConnectStatus.No);
					networkDeviceRestart();
				}				
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控配置下载错误
		/// </summary>
		public void LCDownloadError()
		{
			Invoke((EventHandler)delegate
			{
				lcToolStripStatusLabel2.Text = "灯控配置下载失败";
			});
		}

		/// <summary>
		/// 辅助回调方法：中控配置下载成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCDownloadCompleted(Object obj)
		{
			Invoke((EventHandler)delegate {
				if (isConnectByCom)
				{
					ccToolStripStatusLabel2.Text = "中控配置下载成功,请等待机器重启(约耗时5s)...";
					MessageBox.Show("中控配置下载成功,请等待机器重启(约耗时5s)。");
					Thread.Sleep(5000);

					isDecoding = false;
					ccDecodeButton.Text = "开启解码";
					ccDecodeButton.Enabled = false;

					ccConnectButton_Click(null, null);
				}
				else {
					ccToolStripStatusLabel2.Text = "中控配置下载成功,请等待机器重启(约耗时5s)，并重新搜索连接网络设备。";
					MessageBox.Show("中控配置下载成功,请等待机器重启(约耗时5s)，并重新搜索连接网络设备。");
					Thread.Sleep(5000);
					setConnStatus(ConnectStatus.No);
					networkDeviceRestart();
				}
			});	

		}

		/// <summary>
		///  辅助回调方法：中控配置下载失败
		/// </summary>
		public void CCDownloadError()
		{
			Invoke((EventHandler)delegate
			{
				ccToolStripStatusLabel2.Text = "中控配置下载失败";
			});
		}

		/// <summary>
		/// 辅助回调方法：启动《中控-调试解码》成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCStartCompleted(Object obj)
		{
			Invoke((EventHandler)delegate
			{
				ccToolStripStatusLabel2.Text = "灯控解码开启成功";
				isDecoding = true;
				ccDecodeButton.Text = "关闭解码" ;
				ccDecodeRichTextBox.Enabled = true;
				refreshButtons();
			});
		}

		/// <summary>
		/// 辅助回调方法：启动《中控-调试解码》失败
		/// </summary>
		public void CCStartError()
		{
			Invoke((EventHandler)delegate
			{
				ccToolStripStatusLabel2.Text = "灯控解码开启失败";
			});
		}

		/// <summary>
		/// 辅助回调方法：成功回读《中控-点击的红外码值》
		/// </summary>
		/// <param name="obj"></param>
		public void CCListen(Object obj)
		{
			Invoke((EventHandler)delegate {
				List<byte> byteList = obj as List<byte>;
				if (byteList != null && byteList.Count != 0)
				{
					String strTemp = "";
					foreach (byte item in byteList)
					{
						strTemp += StringHelper.DecimalStringToBitHex(item.ToString(), 2) + " ";
					}
					ccDecodeRichTextBox.Text += strTemp + "\n";
				}
				ccToolStripStatusLabel2.Text = "灯控解码成功";
			});
		}


		/// <summary>
		///  辅助回调方法：结束《中控-调试解码》成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCStopCompleted(Object obj)
		{
			Invoke((EventHandler)delegate
			{
				ccToolStripStatusLabel2.Text = "成功关闭中控解码";
				isDecoding = false;
				ccDecodeButton.Text = "开启解码";
				ccDecodeRichTextBox.Enabled = false ;
				refreshButtons();
			});
		}



		/// <summary>
		/// 辅助回调方法：结束《中控-调试解码》失败
		/// </summary>
		public void CCStopError()
		{
			Invoke((EventHandler)delegate
			{
				ccToolStripStatusLabel2.Text = "关闭中控解码失败";
			});
		}


		/// <summary>
		/// 事件：点击《zwjTest》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zwjTestButton_Click(object sender, EventArgs e)
		{
			ccEntity.GetData();
		}

		/// <summary>
		/// 事件：点击《连接中控》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ccConnectButton_Click(object sender, EventArgs e)
		{
			myConnect.CenterControlConnect(CCConnectCompleted, CCConnectError);
		}

		/// <summary>
		/// 事件：点击《连接灯控》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcConnectButton_Click(object sender, EventArgs e)
		{
			if (tcCheckBox.Checked)
			{
				myConnect.PassThroughLightControlConnect(LCConnectCompleted, LCConnectError);
			}
			else {
				myConnect.LightControlConnect(LCConnectCompleted, LCConnectError);
			}
			
		}


		/// <summary>
		/// 事件：点击《连接墙板》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpConnectButton_Click(object sender, EventArgs e)
		{
			myConnect.PassThroughKeyPressConnect(KPFirstConnectCompleted, KPConnectError);
		}

		/// <summary>
		/// 辅助回调方法： 连接墙板成功
		/// </summary>
		/// <param name="obj"></param>
		public void KPFirstConnectCompleted(Object obj)
		{
			Invoke((EventHandler)delegate
			{
				kpToolStripStatusLabel2.Text = "成功连接墙板(connStatus=kp)";
				setConnStatus(ConnectStatus.Kp);

				//Thread.Sleep(500);								
				//kpReadButton_Click(null, null);

				//Thread.Sleep(500);
				//kpListenButton_Click(null, null);

				// 切换成功后，开启定时器让墙板自动更新（切换到其他的模式时，应将kpTimer停止或设为null）
				if (kpTimer == null)
				{
					kpTimer = new System.Timers.Timer(8000);
					kpTimer.Elapsed += new System.Timers.ElapsedEventHandler(kpOnTimer);
					kpTimer.AutoReset = true;
					kpTimer.Enabled = true;
				}
			});
		}

		/// <summary>
		/// 辅助方法：设置connStatus值，并根据此值刷新按键是否可用；
		/// </summary>
		/// <param name="cs"></param>
		private void setConnStatus(ConnectStatus cs)
		{
			connStatus = cs;
			refreshStatusLabels();
			refreshButtons();					
		}

		/// <summary>
		/// 辅助方法：当下载中控或灯控数据后，应该执行此方法，以引导用户进行新的操作
		/// </summary>
		private void networkDeviceRestart() {
			if (!isConnectByCom && connStatus == ConnectStatus.No)
			{
				deviceComboBox.Items.Clear();
				deviceComboBox.Text = "";
				deviceComboBox.Enabled = false;
				connectButton.Enabled = false;
				setAllStatusLabel1("设备已重启，请重新搜索并重连网络设备");
			}
		}


		/// <summary>
		/// 辅助方法：定时器定时执行的方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpOnTimer(object sender, ElapsedEventArgs e)
		{
			Invoke((EventHandler)delegate {
				if (myConnect != null && connStatus == ConnectStatus.Kp) {				
					myConnect.PassThroughKeyPressConnect(KPTimerConnectCompleted, KPConnectError);	
				}
			});
		}

		/// <summary>
		/// 辅助回调方法：定时器自动重连墙板的方法，此回调方法无需定义执行任何操作（代码中只有后台打印的代码，方便调试）
		/// </summary>
		/// <param name="obj"></param>
		public void KPTimerConnectCompleted(Object obj)
		{
			Invoke((EventHandler)delegate
			{
				Console.WriteLine("Dickov：墙板定时重连成功...");
			});
		}

		/// <summary>
		/// 辅助回调方法：连接墙板失败
		/// </summary>
		public void KPConnectError()
		{
			Invoke((EventHandler)delegate
			{				
				kpToolStripStatusLabel2.Text = "连接墙板失败";

				//MARK：连接墙板失败，是否还要进行其他操作？
				setConnStatus(ConnectStatus.No);
				//keypressListView.Enabled = false;
				//clearKeypressListView();
			});
		}



		/// <summary>
		/// 辅助方法：清空墙板listView的所有数据，及其他相关数据。
		/// </summary>
		private void clearKeypressListView()
		{
			keypressListView.Items.Clear();
			kpOrderTextBox.Text = "";
			kpKey0TextBox.Text = "";
			kpKey1TextBox.Text = "";			
		}


		/// <summary>
		/// 事件：点击《监听按键》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpListenButton_Click(object sender, EventArgs e)
		{
			myConnect.PassThroughKeyPressSetClickListener(KPStartListenClick);
		}

		/// <summary>
		/// 事件：点击《读取码值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpReadButton_Click(object sender, EventArgs e)
		{
			myConnect.PassThroughKeyPressRead(KPReadCompleted, KPReadError);
			kpToolStripStatusLabel2.Text = "正在读取墙板码值，请稍候..." ;
			this.Cursor = Cursors.WaitCursor;
			this.Enabled = false;
		}

		/// <summary>
		///  辅助回调方法：读取墙板码值成功
		/// </summary>
		/// <param name="obj"></param>
		public void KPReadCompleted(Object obj)
		{
			Invoke((EventHandler)delegate
			{
				if (obj == null) {
					kpToolStripStatusLabel2.Text = "异常:执行kpReadCompleted时返回的对象为null";
					return;
				}

				keyEntity = obj as KeyEntity;
				reloadKeypressListView();
				kpToolStripStatusLabel2.Text = "读取墙板码值成功";
				refreshButtons();
				this.Enabled = true;
				this.Cursor = Cursors.Default;
			});			
		}

		/// <summary>
		/// 辅助回调方法：读取墙板码值失败
		/// </summary>
		public void KPReadError()
		{
			Invoke((EventHandler)delegate
			{
				kpToolStripStatusLabel2.Text = "读取墙板码值失败";
				//clearKeypressListView();
				this.Enabled = true;
				this.Cursor = Cursors.Default;
			});
		}

		/// <summary>
		///  读取用户点击的墙板键值（键序+一种不需理会的编码）
		/// </summary>
		/// <param name="obj"></param>
		public void KPStartListenClick(Object obj)
		{			
			Invoke((EventHandler)delegate
			{	
				// 当keyEntity为null时，表示还未加载任何键盘键值数据，此方法不再执行。
				if (obj == null || keyEntity == null) { 
					return;
				}

				List<byte> byteList = obj as List<byte>;
				int keyNum = byteList[0];

				//因为是多选，所以每次点击按键，先清空当前选择（否则会不停添加选中）
				keypressListView.SelectedItems.Clear();
				foreach (ListViewItem item in keypressListView.Items)
				{					
					if (Convert.ToByte(item.SubItems[1].Text) == keyNum)
					{
						Console.WriteLine("Dickov:KeyPressNum: "+  keyNum); 
						item.Selected = true;						
						break;
					}
				}
				keypressListView.Select();

				// 一旦收到这个回复，就重启定时器(避免不停重连)
				if (kpTimer != null) {
					kpTimer.Stop();
					kpTimer.Start();
				}
			});
		}

		#region 几个事件，用以允许拖拽墙板按键，以自定义图标位置

		private Point startPoint = Point.Empty;
		/// <summary>
		///  获取两点间的距离
		/// </summary>
		/// <param name="pt1"></param>
		/// <param name="pt2"></param>
		/// <returns></returns>
		private double getVector(Point pt1, Point pt2) 
		{
			var x = Math.Pow((pt1.X - pt2.X), 2);
			var y = Math.Pow((pt1.Y - pt2.Y), 2);
			return Math.Abs(Math.Sqrt(x - y));
		}

		/// <summary>
		/// 事件：鼠标拖动对象时发生（VS:将对象拖过空间边界时发生）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void keypressListView_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem[])))
				e.Effect = DragDropEffects.Move;
		}

		/// <summary>
		/// 事件：松开鼠标时发生（VS：拖动操作时发生）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void keypressListView_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem[])))
			{
				var items = e.Data.GetData(typeof(ListViewItem[])) as ListViewItem[];

				var pos = keypressListView.PointToClient(new Point(e.X, e.Y));

				var offset = new Point(pos.X - startPoint.X, pos.Y - startPoint.Y);

				foreach (var item in items)
				{
					pos = item.Position;
					pos.Offset(offset);
					item.Position = pos;
				}
			}
		}

		/// <summary>
		/// 事件：按下鼠标时发生 （VS：在组件上方且按下鼠标时发生）
		/// </summary>
		private void keypressListView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				startPoint = e.Location;
		}

		/// <summary>
		/// 事件：listView鼠标移动
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void keypressListView_MouseMove(object sender, MouseEventArgs e)
		{
			if (keypressListView.SelectedItems.Count == 0)
				return;

			if (e.Button == MouseButtons.Left)
			{
				var vector = getVector(startPoint, e.Location);
				if (vector < 10) return;

				var data = keypressListView.SelectedItems.OfType<ListViewItem>().ToArray();
				keypressListView.DoDragDrop(data, DragDropEffects.Move);
			}
		}




		#endregion


		/// <summary>
		/// 事件：点击《墙板-保存按键位置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpPositonSaveButton_Click(object sender, EventArgs e)
		{
			int keyCount = keypressListView.Items.Count;
			new KpPositionSaveForm(this, keyCount).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：由KpPositionSaveForm对象来使用的方法，保存位置文件
		/// </summary>
		/// <returns></returns>
		internal bool KPSavePosition(string arrangeIniPath) {
			try
			{
				// 保存操作
				IniFileAst iniFileAst = new IniFileAst(arrangeIniPath);
				iniFileAst.WriteInt("Common", "Count", keypressListView.Items.Count);
				for (int i = 0; i < keypressListView.Items.Count; i++)
				{
					iniFileAst.WriteInt("Position", i + "X", keypressListView.Items[i].Position.X);
					iniFileAst.WriteInt("Position", i + "Y", keypressListView.Items[i].Position.Y);
				}

				MessageBox.Show("墙板位置保存成功。");
				return true;
			}
			catch (Exception ex) {
				MessageBox.Show("墙板位置保存失败，原因是:\n" + ex.Message);
				return false;
			}
		}

		/// <summary>
		/// 事件：点击《墙板-读取按键位置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpPositonLoadButton_Click(object sender, EventArgs e)
		{
			int keyCount = keypressListView.Items.Count;
			new KpPositionLoadForm(this,keyCount).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：由KpPositionLoadForm对象来使用的方法，读取数据文件
		/// </summary>
		/// <param name="arrangeIniPath"></param>
		/// <returns></returns>
		internal bool KPLoadPosition(string arrangeIniPath) {

			// 1.先验证ini文件是否存在
			if (!File.Exists(arrangeIniPath))
			{
				MessageBox.Show("未找到墙板位置文件，无法读取。");
				return false; 
			}

			//2.验证灯具数目是否一致
			IniFileAst iniFileAst = new IniFileAst(arrangeIniPath);
			int keyCount = iniFileAst.ReadInt("Common", "Count", 0);
			if (keyCount == 0)
			{
				MessageBox.Show("墙板位置文件的按键数量为0，此文件无实际效果。");
				return false;
			}

			//3. 验证灯具数量是否一致
			if (keyCount != keypressListView.Items.Count)
			{
				MessageBox.Show("墙板位置文件的按键数量不匹配，无法读取。");
				return false;
			}

			// 4.开始读取并绘制		
			//MARK : SkinMainForm 特别奇怪的一个地方，在选择自动排列再去掉自动排列后，必须要先设一个不同的position，才能让读取到的position真正给到items[i].Position?
			keypressListView.BeginUpdate();
			for (int i = 0; i < keypressListView.Items.Count; i++)
			{				
				int tempX = iniFileAst.ReadInt("Position", i + "X", 0);
				int tempY = iniFileAst.ReadInt("Position", i + "Y", 0);
				keypressListView.Items[i].Position = new Point(0, 0);
				keypressListView.Items[i].Position = new Point(tempX, tempY);
			}

			keypressListView.EndUpdate();
			MessageBox.Show("墙板位置读取成功。");
			return true;
		}


		/// <summary>
		/// 事件：点击《墙板-保存文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpSaveButton_Click(object sender, EventArgs e)
		{
			keySaveFileDialog.ShowDialog();
		}

		/// <summary>
		/// 事件：《墙板-保存文件》确认文件路径后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void keySaveFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			string keyPath = keySaveFileDialog.FileName;
			keyEntity.WriteToFile(keyPath);
			MessageBox.Show("成功保存墙板配置文件(" + keyPath + ")");			
		}

		/// <summary>
		/// 事件：点击《墙板-下载文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpDownloadButton_Click(object sender, EventArgs e)
		{
			myConnect.PassThroughKeyPressDownload(keyEntity,KeypressDownloadCompleted,KeypressDownladError);
		}

		/// <summary>
		/// 辅助回调方法：下载墙板成功
		/// </summary>
		/// <param name="obj"></param>
		private void KeypressDownloadCompleted(object obj)
		{
			Invoke((EventHandler)delegate
			{
				kpToolStripStatusLabel2.Text = "成功下载墙板码值";
				MessageBox.Show("成功下载墙板码值");
			});

		}

		/// <summary>
		/// 若下载墙板失败，运行此回调方法
		/// </summary>
		private void KeypressDownladError()
		{
			Invoke((EventHandler)delegate
			{
				kpToolStripStatusLabel2.Text = "下载墙板码值失败";
				MessageBox.Show("下载墙板码值失败");
			});
		}

		private void tcCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			setConnStatus(ConnectStatus.Normal);
		}
	}
}