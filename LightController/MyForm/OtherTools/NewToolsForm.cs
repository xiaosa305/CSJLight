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
using LightController.MyForm;

namespace OtherTools
{
	public partial class NewToolsForm : Form
	{
		/// <summary>
		/// 连接状态的枚举
		/// </summary>
		enum ConnectStatus
		{
			No,
			Normal,
			Lc,
			Cc,
			Kp
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

		enum StatusLabel
		{
			NO = 0, CC1, CC2, LC1, LC2, KP1, KP2, ALL1, ALL2
		}

		private const int END_DECODING_TIME = 200; // 关闭中控解码需要一定的时间，才能往下操作；正常情况下200毫秒应该足够，但应设为可调节的

		private IList<Button> buttonList = new List<Button>();	 // 动态添加的按钮组（灯控各个开关）
		private LightControlData lcEntity; //灯控封装对象
		private CCEntity ccEntity; // 中控封装对象
		private KeyEntity keyEntity;  // 墙板封装对象

		private bool isReadLC = false;  // 是否已回读灯控配置？
		private bool isReadXLS = false; // 是否已加载XLS文件
		private string protocolXlsPath = Application.StartupPath + @"\Controller.xls"; //默认的中控配置文件路径
		private HSSFWorkbook xlsWorkbook;  // 通过本对象实现相应的xls文件的映射
		private IList<string> sheetList;  // 每个不同的sheet的列表（不同协议在不同的sheet中）

		private bool isKeepLightOn = false; //是否保持灯光常亮
		private int lcFrameIndex = 0; // 灯控选中的场景，用以显示不同场景的灯光开启状态

		private ConnectStatus connStatus = ConnectStatus.No;   //初始状态为未连接
		
		private bool isDecoding = false; //中控是否开启解码
		private bool isKpShowDetails = true; // 墙板是否显示列表

		private BaseCommunication myConnect; // 保持着一个设备连接（串网口通用）
		private bool isConnectCom = true; //是否串口连接
		private IList<NetworkDeviceInfo> networkDeviceList;  // 网络设备的列表	

		private MainFormBase mainForm; 
		private System.Timers.Timer kpTimer; //墙板定时刷新的定时器（因为透传模式，若太久（10s）没有连接，则会自动退出透传模式）
		private int ccSelectedIndex = -1 ;

		public NewToolsForm(MainFormBase mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;

			#region 初始化各组件		

			// 初始化灯控（强电）各配置
			qdFrameComboBox.SelectedIndex = 0;

			// 各强电开关
			for (int switchIndex = 0; switchIndex < 24; switchIndex++)
			{
				switchButtons[switchIndex] = new SkinButton
				{
					BackColor = switchButtonDemo.BackColor,
					BaseColor = switchButtonDemo.BaseColor,
					BorderColor = switchButtonDemo.BorderColor,
					ControlState = switchButtonDemo.ControlState,
					DownBack = switchButtonDemo.DownBack,
					DrawType = switchButtonDemo.DrawType,
					Font = switchButtonDemo.Font,
					ForeColor = switchButtonDemo.ForeColor,
					ForeColorSuit = switchButtonDemo.ForeColorSuit,
					ImageAlign = switchButtonDemo.ImageAlign,
					ImageIndex = switchButtonDemo.ImageIndex,
					ImageList = switchButtonDemo.ImageList,
					ImageSize = switchButtonDemo.ImageSize,
					InheritColor = switchButtonDemo.InheritColor,
					IsDrawBorder = switchButtonDemo.IsDrawBorder,
					Location = switchButtonDemo.Location,
					Margin = switchButtonDemo.Margin,
					MouseBack = switchButtonDemo.MouseBack,
					NormlBack = switchButtonDemo.NormlBack,
					Size = switchButtonDemo.Size,
					Tag = switchButtonDemo.Tag,
					TextAlign = switchButtonDemo.TextAlign,
					UseVisualStyleBackColor = switchButtonDemo.UseVisualStyleBackColor,
					Visible = switchButtonDemo.Visible,

					Name = "switchButtons" + (switchIndex + 1),
					Text = LanguageHelper.TranslateWord("开关") + (switchIndex + 1)
				};
				switchButtons[switchIndex].Click += switchesButton_Click;

				switchFLP.Controls.Add(switchButtons[switchIndex]);
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
			myInfoToolTip.SetToolTip(tcCheckBox, "选中透传模式后，可由当前设备串联旧设备(如ISC-080C、ISC-075A等)，\n并对该设备进行配置。");
			myInfoToolTip.SetToolTip(ccDecodeButton, "1.若间隔超过一分钟没有点击遥控按钮，则设备会退出解码模式，\n只需点击关闭解码，再重新开启解码即可。\n2.在开启解码状态下，不能下载数据(协议)。");

			// 初始化墙板配置界面的TabControl
			tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
			tabControl1.Alignment = TabAlignment.Left;
			tabControl1.SizeMode = TabSizeMode.Fixed;
			tabControl1.Multiline = true;
			tabControl1.ItemSize = new Size(60, 100);

			#endregion			
		}

		/// <summary>
		/// 事件：加载窗体（设置窗体初始位置等）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewToolsForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			LanguageHelper.InitForm(this);
			LanguageHelper.InitListView(protocolListView);
			LanguageHelper.InitListView(keypressListView);

			bool isShowTestButton = IniFileHelper.GetControlShow(Application.StartupPath, "testButton"); ;
			zwjTestButton.Visible = isShowTestButton;

			//直接刷新串口列表
			refreshDeviceComboBox(); //NewToolsForm_Load

			// 帮着点击一次加载协议
			loadProtocolButton_Click(null, null);
		}
		
		/// <summary>
		/// 刷新所有被connStatus影响的按键
		/// </summary>
		private void refreshButtons()
		{
			// 刷新几个连接按键的可用性
			switchButton.Enabled = connStatus == ConnectStatus.No ;
			deviceComboBox.Enabled = connStatus == ConnectStatus.No && deviceComboBox.Items.Count > 0;
			refreshButton.Enabled = connStatus == ConnectStatus.No;

			// 刷新连接设备的名称
			if (isConnectCom)
			{
				deviceConnectButton.Text = connStatus > ConnectStatus.No ? "关闭串口" : "打开串口";
			}
			else {
				deviceConnectButton.Text = connStatus > ConnectStatus.No ? "断开连接" : "连接设备";
			}			

			// 三个连接按键
			lcConnectButton.Enabled = connStatus > ConnectStatus.No;
			ccConnectButton.Enabled = connStatus > ConnectStatus.No;
			kpConnectButton.Enabled = connStatus > ConnectStatus.No;

			// 灯控相关按键
			lcReadButton.Enabled = connStatus == ConnectStatus.Lc;			
			lcDownloadButton.Enabled = connStatus == ConnectStatus.Lc && lcEntity != null ;
			//lcLoadButton.Enabled = connStatus == ConnectStatus.Lc;
			lcSaveButton.Enabled = lcEntity != null;

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
				case ConnectStatus.No: setNotice(StatusLabel.ALL1,"尚未连接设备",false,true);break;
				//case ConnectStatus.Normal: setAllStatusLabel1("已连接设备"); break;
				case ConnectStatus.Lc: setNotice(StatusLabel.ALL2,"已切换为灯控配置",false,true); break;
				case ConnectStatus.Cc: setNotice(StatusLabel.ALL2,"已切换为中控模式",false,true); break;
				case ConnectStatus.Kp: setNotice(StatusLabel.ALL2,"已切换为墙板配置",false,true); break;
				default: setNotice(StatusLabel.ALL2,"",false,false); break;
			}
		}			

		/// <summary>
		/// 事件：点击《开关按键(1-24)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void switchesButton_Click(object sender, EventArgs e)
		{
			if (!isReadLC)
			{
				return;
			}

			int switchIndex = MathHelper.GetIndexNum(((Button)sender).Name, -1);
			setLightButtonValue(switchIndex);
			//若勾选常亮模式，则需要主动把所有场景的选中灯光亮暗设为一致。
			if (isKeepLightOn) {
				bool tempLightOnMode = lcEntity.SceneData[lcFrameIndex, switchIndex];
				for (int frameIndex = 0; frameIndex < 17; frameIndex++) {
					lcEntity.SceneData[frameIndex, switchIndex] = tempLightOnMode;
				}
			}
			debugLC();
		}

		/// <summary>
		///  
		/// </summary>
		/// <param name="switchIndex"></param>
		private void setLightButtonValue(int switchIndex)
		{
			if (!isReadLC)
			{
				return;
			}
			lcEntity.SceneData[lcFrameIndex, switchIndex] = !lcEntity.SceneData[lcFrameIndex, switchIndex];
			switchButtons[switchIndex].ImageIndex = lcEntity.SceneData[lcFrameIndex, switchIndex] ? 1 : 0;
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
			lcSetLoad();

			setNotice(StatusLabel.LC2,
				LanguageHelper.TranslateSentence("已加载配置文件：") + cfgPath,
				false, false);
		}

		/// <summary>
		/// 辅助方法：通过lcEntity，渲染灯控Tab内相关的控件
		/// </summary>
		private void lcSetLoad() {

			if (lcEntity == null) {				
				lightGroupBox.Enabled = false;
				tgGroupBox.Enabled = false;
				lcGroupBox3.Enabled = false;
				lcGroupBox4.Enabled = false;
				lcGroupBox5.Enabled = false;
				refreshButtons();

				setNotice(0, "lcEntity==null。", true, false);
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
					foreach (SkinButton btn in switchButtons)
					{
						btn.Visible = false;
					}
					for (int relayIndex = 0; relayIndex < lcEntity.RelayCount; relayIndex++)
					{
						switchButtons[relayIndex].Visible = true;
					}
				}

				//DOTO：直接通过通道数量，把后六位给固定的排风及空调通道
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
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 辅助方法：设置风扇通道
		/// </summary>
		/// <param name="airMode"></param>
		/// <param name="fanChannel"></param>
		private void setFanChannel(airModeEnum airMode, int fanChannel)
		{
			fanChannelComboBoxes[(int)airMode].SelectedIndex = fanChannel;
		}	

		/// <summary>
		/// 辅助方法：是否使用排风
		/// </summary>
		/// <param name="isOpenFan"></param>
		private void enableFan()
		{
			fanChannelComboBox.Enabled = lcEntity.IsOpenFan;
			int switchIndex = Convert.ToInt32(fanChannelComboBox.Text.Substring(2)) -1 ;
			switchButtons[switchIndex].Visible = !lcEntity.IsOpenFan;
			fanButton.Text = lcEntity.IsOpenFan ? "禁用排风通道" : "启用排风通道";
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
				switchButtons[tempIndex].Visible = !lcEntity.IsOpenAirCondition;
			}
			acButton.Text = lcEntity.IsOpenAirCondition ? "禁用空调通道" : "启用空调通道";
		}

		/// <summary>
		/// 事件：监听选择通道，若选中的通道已被占用，则恢复原先设置。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fanChannelComboBoxes_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox cb = (ComboBox)sender;
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
				switchButtons[relayIndex].ImageIndex = lcEntity.SceneData[lcFrameIndex, relayIndex] ? 1 : 0;
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
			lcEntity.LightMode = radio.Name == "lightModeQHRadioButton" ? 1 : 0 ;

			Console.WriteLine(lcEntity.LightMode + " 	lcEntity.LightMode");
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
					
			switch (radio.Name) {
				case "fjJYRadioButton" :	lcEntity.AirControlSwitch = 0;break;
				case "fjDXFRadioButton": lcEntity.AirControlSwitch = 1; break;
				case "fjSXFRadioButton": lcEntity.AirControlSwitch = 2; break;
			}
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
			setNotice(StatusLabel.LC2,
				LanguageHelper.TranslateSentence("成功保存配置文件：") + cfgPath,
				true, false);
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
				setNotice(StatusLabel.LC2, "myConnect==null，无法下载数据", true, true);
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
			try
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
					setNotice(StatusLabel.CC2,
						LanguageHelper.TranslateSentence("已加载xls文件：") + protocolXlsPath,
						false, false);
				}
				else
				{
					isReadXLS = false;
					setNotice(StatusLabel.CC2, "请检查xls文件是否正确，其Sheet数量为0。", true, true);
				}
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
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

		/// <summary>
		/// 事件：更改了《协议选项框》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
			com0Label.Text = LanguageHelper.TranslateWord("串口0 = ") + ccEntity.Com0;
			com1Label.Text = LanguageHelper.TranslateWord("串口1 = ")+ ccEntity.Com1;
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

		/// <summary>
		///  事件：点击《中控-下载数据》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ccDownloadButton_Click(object sender, EventArgs e)
		{
			if (ccEntity == null)
			{
				setNotice(StatusLabel.CC2, "请先加载xls文件并选择协议。",true,true);
				return;
			}

			// 正常情况下，在解码模式下不能下载数据，加这个判断以防万一
			if(isDecoding) {
				setNotice(StatusLabel.CC2, "在解码状态下无法下载协议，请先关闭解码。",true,true);
				return;
			}
				
			setNotice(StatusLabel.CC2, "正在下载中控协议到设备，请稍候...", false, true);
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
			cc.Com0 = Convert.ToInt32(cell.ToString().Substring(4));
			rows.MoveNext();
			row = (HSSFRow)rows.Current;
			cell = row.GetCell(0);
			cc.Com1 = Convert.ToInt32(cell.ToString().Substring(4));
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
				setNotice(0, "请输入搜索关键字。", true, true);
				return;
			}

			// 检查是否已载入协议
			if (ccEntity == null)
			{
				setNotice(0, "请先加载协议(cc为空)", true, true);
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
				ccSelectedIndex = protocolListView.SelectedIndices[0];

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
				setNotice(StatusLabel.KP2, "加载墙板配置文件出错。", true, true);
				return;
			}

			reloadKeypressListView();
			refreshButtons();
			
			setNotice(StatusLabel.KP2, 
				LanguageHelper.TranslateSentence("已加载墙板配置文件:")+keyPath,
				false, false);
		}

		/// <summary>
		/// 辅助方法：重新加载墙板码值
		/// </summary>
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
				setNotice( StatusLabel.KP2, "key文件有错误，无法加载。", true, true);				
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
					setNotice(0, "文件不存在", true, true);
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
				setNotice(0, "请先选择需要设置键码值的按键。", true, true);
				return;
			}
			if (kpKey0TextBox.Text.Length == 0) {
				setNotice(0, "键码值0不得为空。", true, true);
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
			int keyArrayIndex = Convert.ToInt32(kpOrderTextBox.Text) - 1; //keyEntity中的array索引号
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

		/// <summary>
		///  事件：点击《切换连接方式》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void switchButton_Click(object sender, EventArgs e)
		{
			isConnectCom = !isConnectCom;
			switchButton.Text = isConnectCom ? "以网络连接" : "以串口连接";
			refreshButton.Text = isConnectCom ?  "刷新串口" : "刷新网络";
			deviceConnectButton.Text = isConnectCom ?  "打开串口" : "连接设备";

			refreshDeviceComboBox(); // switchButton_Click
		}

		/// <summary>
		/// 辅助方法：断开连接（退出Form及切换连接方式时，都跑一次这个方法）
		/// </summary>
		private void disConnect() {

			if (myConnect != null)
			{
				// 若正在解码状态，则先关闭解码，才能关闭连接
				if (isDecoding)
				{
					myConnect.CenterControlStopCopy(AsynchronousCCStopCompleted, AsynchronousCCStopError);
					Thread.Sleep(END_DECODING_TIME);					
				}

				Console.WriteLine("Now IsDecoding : " + isDecoding);
				if (connStatus > ConnectStatus.No)
				{
					myConnect.DisConnect();
				}
				setConnStatus(ConnectStatus.No);
				setNotice(StatusLabel.ALL1, "已" + (isConnectCom ? "关闭串口" : "断开连接") ,false,true ) ;
				setNotice(StatusLabel.ALL2,"",false,false); // 断开连接后，设置右侧状态栏为空字符串

				myConnect = null;

                // 每次断开连接时，要顺手关闭定时器（若不关闭，则在关闭窗口后，维佳的后台程序仍会调用kpTimer）
                if (kpTimer != null)
                {
                    kpTimer.Dispose();
                    kpTimer = null;
                }
            }		
		}

		/// <summary>
		/// 辅助方法：通过不同的isConnectByCom来刷新deviceComboBox。
		/// </summary>
		private void refreshDeviceComboBox()
		{
			// 刷新前，先清空列表(也先断开连接：只是保护性再跑一次)
			disConnect(); // refreshDeviceComboBox			
			setNotice(StatusLabel.ALL1,"正在搜索设备，请稍候...",false,true);		
			deviceComboBox.Items.Clear();
			deviceComboBox.Text = "";
			deviceComboBox.SelectedIndex = -1;
			deviceComboBox.Enabled = false;
			deviceConnectButton.Enabled = false;
			Refresh();

			// 获取串口列表（不代表一定能连上，串口需用户自行确认）
			if (isConnectCom)
			{
				if (myConnect == null) {
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
			else {				
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
				setNotice(StatusLabel.ALL1, "已搜到可用设备列表。", false, true);
			}
			else {
				setNotice(StatusLabel.ALL1, "未找到可用设备，请检查设备连接后重试。", false, true);
			}
			// 无论在何种情况下，只要刷新了列表，说明连接已经断开（先断开），此时应该设置为未连接状态
			setConnStatus(ConnectStatus.No);
		}

		//事件：点击《刷新串口|网络》
		private void refreshButton_Click(object sender, EventArgs e)
		{
			refreshDeviceComboBox();  // refreshButton_Click			
		}

		/// <summary>
		///  事件：点击《打开串口|连接设备 || 关闭串口|断开连接》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectButton_Click(object sender, EventArgs e)
		{
			// 如果已连接（按钮显示为“连接设备”)，则关闭连接
			if (connStatus > ConnectStatus.No)
			{
				disConnect(); //connectButton_Click
				return; 
			}

			// 若未连接，则连接；并分情况处理
			if (isConnectCom)
			{
				myConnect = new SerialConnect();				
				try
				{
					(myConnect as SerialConnect).OpenSerialPort(deviceComboBox.Text);

					setNotice(StatusLabel.ALL1, "已打开串口", true, true);
					setConnStatus(ConnectStatus.Normal);
				}
				catch (Exception ex) {
					setNotice(StatusLabel.ALL1,
						LanguageHelper.TranslateSentence("打开串口失败，原因是：") + ex.Message , 
						true, false);
					setConnStatus(ConnectStatus.No);
				}
			}
			else {
				NetworkDeviceInfo selectedNetworkDevice = networkDeviceList[deviceComboBox.SelectedIndex];				
				string deviceName = selectedNetworkDevice.DeviceName;

				myConnect = new NetworkConnect( );
				myConnect.Connect( selectedNetworkDevice);
				if ( myConnect.IsConnected() )
				{
					setNotice(StatusLabel.ALL1, "成功连接网络设备", true, true);
					setConnStatus(ConnectStatus.Normal);
				}
				else {
					setNotice(StatusLabel.ALL1, "连接网络设备失败", true, true);
					setConnStatus(ConnectStatus.No);
				}
			}
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
		public void LCConnectCompleted(Object obj,string  msg) {
			Invoke((EventHandler)delegate {

				setNotice(StatusLabel.LC2, "已切换成灯控配置(connStatus = lc" + (tcCheckBox.Checked?" - tc":"") +")", true, true);
				setConnStatus(ConnectStatus.Lc);
				lcReadButton_Click(null, null);
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控连接出错
		/// </summary>
		/// <param name="obj"></param>
		public void LCConnectError(string msg) {
			Invoke((EventHandler)delegate
			{
				// 切换失败，只给提示，不更改原来的状态
				setNotice(StatusLabel.LC2, LanguageHelper.TranslateSentence("切换灯控配置失败:") + msg , true, false);				
			});
		}

		/// <summary>
		/// 辅助回调方法：中控连接成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCConnectCompleted(Object obj,string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice(StatusLabel.CC2, "已切换成中控配置(connStatus=cc)" , true, true);
				setConnStatus(ConnectStatus.Cc);
			});
		}

		/// <summary>
		/// 辅助回调方法：中控连接失败
		/// </summary>
		public void CCConnectError(string msg)
		{
			Invoke((EventHandler)delegate {
				// 切换失败，只给提示，不更改原来的状态
				setNotice(StatusLabel.CC2, LanguageHelper.TranslateSentence("切换中控配置失败:") + msg, true, false);
			});
		}

		/// <summary>
		///  辅助回调方法：灯控debug(实时调试的数据)发送成功
		/// </summary>
		/// <param name="obj"></param>
		public void LCSendCompleted(Object obj,string msg)
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
		public void LCSendError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				lcToolStripStatusLabel2.Text = "灯控已离线，发送debug数据失败，请重新连接后重试["+msg+"]";
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控数据回读成功
		/// </summary>
		/// <param name="lcDataTemp"></param>
		public void LCReadCompleted(Object lcDataTemp,string msg)
		{
			Invoke((EventHandler)delegate {
				if (lcDataTemp == null)
				{
					setNotice(StatusLabel.LC2, "灯控回读配置异常(lcDataTemp==null)", true, true);
					return;
				}

				lcEntity = lcDataTemp as LightControlData;
				lcSetLoad();
				setNotice(StatusLabel.LC2, "成功回读灯控配置", true, true);
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控配置回读失败
		/// </summary>
		public void LCReadError(string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice(StatusLabel.LC2, LanguageHelper.TranslateSentence("回读灯控配置失败:") + msg, true, false);
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控配置下载成功
		/// </summary>
		/// <param name="obj"></param>
		public void LCDownloadCompleted(Object obj , string msg)
		{
			Invoke((EventHandler)delegate {
				// 当使用串口连接时，下载成功会重启设备，但因为用串口连接，代码可以主动帮助重连；
				// 同理，若是使用透传模式，则下载成功重启的并非主设备，而是透传的设备，其重启后仍会主动连上主设备，主动点击重连键即可。
				if (isConnectCom || tcCheckBox.Checked)
				{
					setNotice(StatusLabel.LC2, "灯控配置下载成功,请等待设备重启(约耗时5s)", true, true);
					Thread.Sleep(5000);
					lcConnectButton_Click(null, null);
				}
				else {
					setNotice(StatusLabel.LC2, "灯控配置下载成功,请等待设备重启(约耗时5s)，并重新搜索连接网络设备。", true, true);
					Thread.Sleep(5000);
					setConnStatus(ConnectStatus.No);
					networkDeviceRestart();	
				}				
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控配置下载错误
		/// </summary>
		public void LCDownloadError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.LC2,  LanguageHelper.TranslateSentence("灯控配置下载失败:") + msg , true, false);
			});
		}

		/// <summary>
		/// 辅助回调方法：中控配置下载成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCDownloadCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {
				if (isConnectCom)
				{
					setNotice(StatusLabel.CC2, "中控配置下载成功,请等待设备重启(约耗时5s)", true, true);
					Thread.Sleep(5000);
					ccConnectButton_Click(null, null);
				}
				else {
					setNotice(StatusLabel.CC2, "中控配置下载成功,请等待设备重启(约耗时5s)，并重新搜索连接网络设备。", true, true);
					Thread.Sleep(5000);
					setConnStatus(ConnectStatus.No);
					networkDeviceRestart();
				}
			});	
		}

		/// <summary>
		///  辅助回调方法：中控配置下载失败
		/// </summary>
		public void CCDownloadError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.CC2,  LanguageHelper.TranslateSentence("中控配置下载失败：") + msg, true, false);
			});
		}

		/// <summary>
		/// 辅助回调方法：启动《中控-调试解码》成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCStartCompleted(Object obj,string msg)
		{
			Invoke((EventHandler)delegate
			{				
				setNotice(StatusLabel.CC2, msg, false, true);
				isDecoding = true;
				ccDecodeButton.Text = "关闭解码" ;
				ccDecodeRichTextBox.Enabled = true;
				refreshButtons();
			});
		}

		/// <summary>
		/// 辅助回调方法：启动《中控-调试解码》失败
		/// </summary>
		public void CCStartError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.CC2, LanguageHelper.TranslateSentence("中控解码开启失败:") + msg, true, false);
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
				setNotice(StatusLabel.CC2, "灯控解码成功", false, true);
			});
		}

		/// <summary>
		///  辅助回调方法：结束《中控-调试解码》成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCStopCompleted(Object obj,string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.CC2, "成功关闭中控解码", false, true);
				isDecoding = false;
				ccDecodeButton.Text = "开启解码";
				ccDecodeRichTextBox.Enabled = false ;				
				refreshButtons();
			});
		}

		/// <summary>
		///  辅助回调方法：结束《中控-调试解码》成功
		///  异步处理，在disconnect方法中调用，避免界面卡死
		/// </summary>
		/// <param name="obj"></param>
		public void AsynchronousCCStopCompleted(Object obj, string msg)
		{
			BeginInvoke((EventHandler)delegate
			{
				setNotice(StatusLabel.CC2, "成功关闭中控解码", false, true);
				isDecoding = false;
				ccDecodeButton.Text = "开启解码";
				ccDecodeRichTextBox.Enabled = false;
				refreshButtons();
			});
		}

		/// <summary>
		/// 辅助回调方法：结束《中控-调试解码》失败
		/// </summary>
		public void CCStopError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.CC2, LanguageHelper.TranslateSentence("关闭中控解码失败:") + msg, true, false);
			});
		}

		/// <summary>
		/// 辅助回调方法：结束《中控-调试解码》失败
		/// 异步处理，在disConnect方法中调用，避免界面卡死
		/// </summary>
		public void AsynchronousCCStopError(string msg)
		{
			BeginInvoke((EventHandler)delegate
			{
				setNotice(StatusLabel.CC2, LanguageHelper.TranslateSentence("关闭中控解码失败:") + msg, true, false);
			});
		}

		/// <summary>
		/// 事件：点击《连接灯控》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcConnectButton_Click(object sender, EventArgs e)
		{
			// 若正在解码状态，则先关闭解码，才能进行连接
			if (myConnect != null && isDecoding ) {
				myConnect.CenterControlStopCopy(CCStopCompleted, CCStopError);
				Thread.Sleep(END_DECODING_TIME);
			}

			if (tcCheckBox.Checked)
			{
				myConnect.PassThroughLightControlConnect(LCConnectCompleted, LCConnectError);
			}
			else {
				myConnect.LightControlConnect(LCConnectCompleted, LCConnectError);
			}			
		}

		/// <summary>
		/// 事件：点击《连接中控》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ccConnectButton_Click(object sender, EventArgs e)
		{			
			// 若正在解码状态，则先关闭解码，才能进行连接
			if (myConnect != null && isDecoding)
			{
				myConnect.CenterControlStopCopy(CCStopCompleted, CCStopError);
				Thread.Sleep(END_DECODING_TIME);
			}
			myConnect.CenterControlConnect(CCConnectCompleted, CCConnectError);
		}

		/// <summary>
		/// 事件：点击《连接墙板》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpConnectButton_Click(object sender, EventArgs e)
		{
			// 若正在解码状态，则先关闭解码，才能进行连接
			if (myConnect != null && isDecoding)
			{
				myConnect.CenterControlStopCopy(CCStopCompleted, CCStopError);
				Thread.Sleep(END_DECODING_TIME);
			}
			myConnect.PassThroughKeyPressConnect(KPFirstConnectCompleted, KPConnectError);
		}

		/// <summary>
		/// 辅助回调方法： 连接墙板成功
		/// </summary>
		/// <param name="obj"></param>
		public void KPFirstConnectCompleted(Object obj,string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.KP2, "成功连接墙板(connStatus=kp)", true, true);
				setConnStatus(ConnectStatus.Kp);

				Thread.Sleep(500);
				kpListenButton_Click(null, null);

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
			if (!isConnectCom && connStatus == ConnectStatus.No)
			{
				deviceComboBox.Items.Clear();
				deviceComboBox.Text = "";
				deviceComboBox.Enabled = false;
				deviceConnectButton.Enabled = false;
				setNotice(StatusLabel.ALL1, "请重新搜索并连接网络设备", false, true);
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
		public void KPTimerConnectCompleted(Object obj,string msg)
		{
			Invoke((EventHandler)delegate
			{
				Console.WriteLine("Dickov：墙板定时重连成功...");
			});
		}

		/// <summary>
		/// 辅助回调方法：连接墙板失败
		/// </summary>
		public void KPConnectError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				// 切换失败，只给提示，不更改原来的状态
				setNotice(StatusLabel.KP2, LanguageHelper.TranslateSentence("连接墙板失败:")+msg ,  true , false);
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
			setNotice(StatusLabel.KP2, "正在读取墙板码值，请稍候...", false, true);
			Cursor = Cursors.WaitCursor;
			Enabled = false;
		}

		/// <summary>
		///  辅助回调方法：读取墙板码值成功
		/// </summary>
		/// <param name="obj"></param>
		public void KPReadCompleted(Object obj,string msg)
		{
			Invoke((EventHandler)delegate
			{
				if (obj == null) {
					setNotice(StatusLabel.KP2, "异常:执行kpReadCompleted时返回的对象为null", true, true);
					return;
				}

				keyEntity = obj as KeyEntity;
				reloadKeypressListView();
				setNotice(StatusLabel.KP2, "读取墙板码值成功。", true, true);
				refreshButtons();
				this.Enabled = true;
				this.Cursor = Cursors.Default;
			});			
		}

		/// <summary>
		/// 辅助回调方法：读取墙板码值失败
		/// </summary>
		public void KPReadError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.KP2, LanguageHelper.TranslateSentence("读取墙板码值失败:") + msg, true, false);
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
				IniFileHelper iniFileAst = new IniFileHelper(arrangeIniPath);
				iniFileAst.WriteInt("Common", "Count", keypressListView.Items.Count);
				for (int i = 0; i < keypressListView.Items.Count; i++)
				{
					iniFileAst.WriteInt("Position", i + "X", keypressListView.Items[i].Position.X);
					iniFileAst.WriteInt("Position", i + "Y", keypressListView.Items[i].Position.Y);
				}
				setNotice(StatusLabel.KP2, "墙板位置保存成功。", true, true);
				return true;
			}
			catch (Exception ex) {
				setNotice(StatusLabel.KP2,LanguageHelper.TranslateSentence("墙板位置保存失败:")+ex.Message, true, false);
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
				setNotice(StatusLabel.KP2, "未找到墙板位置文件，无法读取。", true, true);
				return false; 
			}

			//2.验证灯具数目是否一致
			IniFileHelper iniFileAst = new IniFileHelper(arrangeIniPath);
			int keyCount = iniFileAst.ReadInt("Common", "Count", 0);
			if (keyCount == 0)
			{
				setNotice(StatusLabel.KP2, "墙板位置文件的按键数量为0，此文件无实际效果。", true, true);
				return false;
			}

			//3. 验证灯具数量是否一致
			if (keyCount != keypressListView.Items.Count)
			{
				setNotice(StatusLabel.KP2, "墙板位置文件的按键数量不匹配，无法读取。", true, true);
				return false;
			}

			// 4.开始读取并绘制		
			// 在选择自动排列再去掉自动排列后，必须要先设一个不同的position，才能让读取到的position真正给到items[i].Position?
			keypressListView.BeginUpdate();
			for (int i = 0; i < keypressListView.Items.Count; i++)
			{				
				int tempX = iniFileAst.ReadInt("Position", i + "X", 0);
				int tempY = iniFileAst.ReadInt("Position", i + "Y", 0);
				keypressListView.Items[i].Position = new Point(0, 0);
				keypressListView.Items[i].Position = new Point(tempX, tempY);
			}

			keypressListView.EndUpdate();
			setNotice(StatusLabel.KP2, "墙板位置读取成功。", true, true);
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
			setNotice(StatusLabel.KP2, LanguageHelper.TranslateSentence("成功保存墙板配置文件:")+keyPath, true, false);
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
		private void KeypressDownloadCompleted(object obj,string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.KP2, "成功下载墙板码值", true, true);
			});
		}

		/// <summary>
		/// 若下载墙板失败，运行此回调方法
		/// </summary>
		private void KeypressDownladError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.KP2, LanguageHelper.TranslateSentence("下载墙板码值失败:")+msg, true, false);
			});
		}

		/// <summary>
		/// 事件：是否勾选《透传模式》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tcCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			setNotice(StatusLabel.LC2, "已" + (tcCheckBox.Checked ? "开启" : "关闭") + "透传模式，请重新连接灯控。", true, true);
		}

		/// <summary>
		/// 事件：《窗口关闭》时释放使用的串口资源
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewToolsForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			disConnect(); //NewToolsForm_FormClosed
			Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：更改《deviceComboBox》的选中项（若设为Enabled为false或者Clear(),却不会跑这里 ）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!deviceComboBox.Text.Trim().Equals(""))
			{
				deviceConnectButton.Enabled = true;
			}
			else
			{
				deviceConnectButton.Enabled = false;			
				setNotice(0, "未选中可用设备", true, true);
			}
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

		#region 几个未启用或测试方法

		/// <summary>
		///  事件：点击测试按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bigTestButton2_Click(object sender, EventArgs e)		{		}

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
		///  事件：切换不同的Tab
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			//Console.WriteLine(tabControl1.SelectedIndex);

		}
			   
		/// <summary>
		/// 事件：点击《修改码值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void editCodeButton_Click(object sender, EventArgs e)
		{




		}

		#endregion

		#region 通用方法

		/// <summary>
		/// 辅助方法：通用的通知方法（这个Form比较复杂，因为Tab太多了）
		/// </summary>
		/// <param name="position">放到底部通知栏的哪一侧，1为左侧，2为右侧</param>
		/// <param name="msg"></param>
		/// <param name="isMsgShow"></param>
		/// <param name="isTranslate"></param>
		private void setNotice(StatusLabel position, string msg, bool isMsgShow, bool isTranslate) {
			if (isTranslate)		{	msg = LanguageHelper.TranslateSentence(msg);	}
			if (isMsgShow)		{	MessageBox.Show(msg);	}
			switch (position) {
				case StatusLabel.CC1 :  ccToolStripStatusLabel1.Text = msg;break;
				case StatusLabel.CC2:   ccToolStripStatusLabel2.Text = msg; break;
				case StatusLabel.LC1:   lcToolStripStatusLabel1.Text = msg; break;
				case StatusLabel.LC2:   lcToolStripStatusLabel2.Text = msg; break;
				case StatusLabel.KP1:   kpToolStripStatusLabel1.Text = msg; break;
				case StatusLabel.KP2:   kpToolStripStatusLabel2.Text = msg; break;
				case StatusLabel.ALL1:	lcToolStripStatusLabel1.Text = msg;	ccToolStripStatusLabel1.Text = msg;	kpToolStripStatusLabel1.Text = msg ; break;
				case StatusLabel.ALL2:	lcToolStripStatusLabel2.Text = msg;	ccToolStripStatusLabel2.Text = msg;kpToolStripStatusLabel2.Text = msg;break;
			}
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