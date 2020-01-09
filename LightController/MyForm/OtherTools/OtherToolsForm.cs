﻿using CCWin.SkinControl;
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
			Kp,
			Tc
		}

		private IList<Button> buttonList = new List<Button>();
		private String cfgPath; // 灯控页打开的 灯控配置文件路径(*.cfg)
		private LightControlData lcData; //灯控封装对象
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

		//private bool isConnected = false;  //是否已连上设备
		//private bool isOvertime = false; //是否超时
		//private bool isCCConnected = false;
		//private bool isDownloading = false;

		private System.Timers.Timer kpTimer; //墙板定时刷新的定时器（因为透传模式，若太久（10s）没有连接，则会自动退出透传模式）

		public OtherToolsForm()
		{
			InitializeComponent();					

			#region 初始化各组件		

			// 初始化强电各种配置
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
			fanChannelComboBoxes[1] = highFanChannelComboBox;
			fanChannelComboBoxes[2] = midFanChannelComboBox;
			fanChannelComboBoxes[3] = lowFanChannelComboBox;
			fanChannelComboBoxes[4] = fopenChannelComboBox;
			fanChannelComboBoxes[5] = fcloseChannelComboBox;

			foreach (ComboBox item in fanChannelComboBoxes)
			{
				item.SelectedIndex = 0;
			}

			myInfoToolTip.SetToolTip(keepLightOnCheckBox, "选中常亮模式后，手动点亮或关闭每一个灯光通道，\n都会点亮或关闭所有场景的该灯光通道。");


			// 初始化墙板配置界面的TabControl
			tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
			tabControl1.Alignment = TabAlignment.Left;
			tabControl1.SizeMode = TabSizeMode.Fixed;
			tabControl1.Multiline = true;
			tabControl1.ItemSize = new Size(60, 100);

			#endregion
			
		}

		private Point mouse_offset;
		private void button1_MouseDown(object sender, MouseEventArgs e)
		{
			mouse_offset = new Point(-e.X, -e.Y);
		}

		private void button1_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Point mousePos = Control.MousePosition;
				mousePos.Offset(mouse_offset.X, mouse_offset.Y);
				((Control)sender).Location = ((Control)sender).Parent.PointToClient(mousePos);
			}

			//this.button1.Text = "横坐标:" + mouse_offset.X + "纵坐标" + mouse_offset.Y;
			//this.button2.Text = "横坐标:" + mouse_offset.X + "纵坐标" + mouse_offset.Y;
		}


		private void commonButton_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			Console.WriteLine(button.Name);
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
				bool tempLightOnMode = lcData.SceneData[lcFrameIndex, lightIndex];
				for (int frameIndex = 0; frameIndex < 17; frameIndex++) {
					lcData.SceneData[frameIndex, lightIndex] = tempLightOnMode;
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
			lcData.SceneData[lcFrameIndex, lightIndex] = !lcData.SceneData[lcFrameIndex, lightIndex];
			lightButtons[lightIndex].ImageIndex = lcData.SceneData[lcFrameIndex, lightIndex] ? 1 : 0;
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
			cfgPath = cfgOpenFileDialog.FileName;
			loadLCParam(cfgPath);
			//ReadCompleted(lcData);
		}

		/// <summary>
		/// 辅助方法，当《灯控配置页》加载cfg文件后，lcTagPage的其他控件才开始可用。
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
			lcData = new LightControlData(paramList);
			setLcForm();
			lcToolStripStatusLabel2.Text = " 已加载配置文件：" + cfgPath;
		}

		private void setLcForm() {
			if (lcData == null) {
				MessageBox.Show("lcData==null");
				return;
			}

			try
			{
				switch (lcData.LightMode)
				{
					case 0: lightModeDJRadioButton.Checked = true; break;
					case 1: lightModeQHRadioButton.Checked = true; break;
				}

				switch (lcData.AirControlSwitch)
				{
					case 0: fjJYRadioButton.Checked = true; break;
					case 1: fjDXFRadioButton.Checked = true; break;
					case 2: fjSXFRadioButton.Checked = true; break;
				}

				if (lcData.RelayCount == 0)
				{
					lightGroupBox.Enabled = false;
				}
				else
				{
					foreach (SkinButton btn in lightButtons)
					{
						btn.Visible = false;
					}
					for (int relayIndex = 0; relayIndex < lcData.RelayCount; relayIndex++)
					{
						lightButtons[relayIndex].Visible = true;
					}
				}

				enableLCInit();
				enableAirCondition();
				enableFan();
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
			fanChannelComboBox.Enabled = lcData.IsOpenFan;
			lightButton7.Visible = !lcData.IsOpenFan;
			fanButton.Text = lcData.IsOpenFan ? "点击禁用\r\n排风通道" : "点击启用\r\n排风通道";
		}

		// <summary>
		/// 辅助方法：是否使用空调
		/// </summary>
		/// <param name="isOpenAirCondition"></param>
		private void enableAirCondition()
		{
			highFanChannelComboBox.Enabled = lcData.IsOpenAirCondition;
			midFanChannelComboBox.Enabled = lcData.IsOpenAirCondition;
			lowFanChannelComboBox.Enabled = lcData.IsOpenAirCondition;
			fopenChannelComboBox.Enabled = lcData.IsOpenAirCondition;
			fcloseChannelComboBox.Enabled = lcData.IsOpenAirCondition;

			// 在启用或禁用空调后，应该让空调占用的通道隐藏或显示出来，为避免错误，先用一个三目运算取出相应的数据。
			int maxLightIndex = lcData.RelayCount < 12 ? lcData.RelayCount : 12;
			for (int i = 7; i < maxLightIndex; i++)
			{
				lightButtons[i].Visible = !lcData.IsOpenAirCondition;
			}
			acButton.Text = lcData.IsOpenAirCondition ? "点击禁用\r\n空调通道" : "点击启用\r\n空调通道";
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

			for (int relayIndex = 0; relayIndex < lcData.RelayCount; relayIndex++)
			{
				lightButtons[relayIndex].ImageIndex = lcData.SceneData[lcFrameIndex, relayIndex] ? 1 : 0;
			}
			//debugLC();
		}

		private void debugLC() {
			
			if (connStatus != ConnectStatus.Lc) {				
				return;
			}

			if (isConnectByCom )
			{
				byte[] tempData = lcData.GetFrameBytes(lcFrameIndex);
				comConnect.LightControlDebug(tempData, ComLCSendCompleted, ComLCSendError);
			}
			else{

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
			lcData.LightMode = Convert.ToInt16(radio.Tag);
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
			lcData.AirControlSwitch = Convert.ToInt16(radio.Tag);
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

			lcData.IsOpenFan = !lcData.IsOpenFan;
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

			lcData.IsOpenAirCondition = !lcData.IsOpenAirCondition;
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
			cfgPath = cfgSaveFileDialog.FileName;
			lcData.WriteToFile(cfgPath);
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
			if (comConnect != null) {
				comConnect.LightControlDownload(lcData, ComLCDownloadCompleted, ComLCDownloadError);
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
				comConnect.CenterControlStopCopy(ComCCStopCompleted, ComCCEndError);
			}
			// 点击《开启解码》
			else {
				comConnect.CenterControlStartCopy(ComCCStartCompleted, ComCCStartError, ComCCListen);
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

			comConnect.CenterControlDownload(ccEntity, ComCCDownloadCompleted, ComCCDownloadError);

			//ccToolStripStatusLabel2.Text = "成功下载中控数据。";
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

			int keyIndex = keypressListView.SelectedIndices[0];
			keypressListView.Items[keyIndex].SubItems[2].Text = kpKey0TextBox.Text.ToLower().PadLeft(2, '0');
			if (kpKey1TextBox.Text.Length == 0)
			{
				keypressListView.Items[keyIndex].SubItems[3].Text = kpKey0TextBox.Text.ToLower().PadLeft(2, '0');
			}
			else {
				keypressListView.Items[keyIndex].SubItems[3].Text = kpKey1TextBox.Text.ToLower().PadLeft(2, '0');
			}
		}


		/// <summary>
		/// 两个自定义墙板键码值的输入文字的验证。
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
			isConnectByCom = !isConnectByCom;
			switchButton.Text = isConnectByCom ? "以网络连接" : "以串口连接";
			refreshButton.Text = isConnectByCom ?  "刷新串口" : "刷新网络";
			connectButton.Text = isConnectByCom ?  "打开串口" : "连接设备";
			refreshDeviceComboBox();
		}

		private SerialConnect comConnect;
		/// <summary>
		/// 辅助方法：通过不同的isConnectByCom来刷新deviceComboBox。
		/// </summary>
		private void refreshDeviceComboBox()
		{
			deviceComboBox.Items.Clear();
			if (isConnectByCom)
			{
				if (comConnect == null) {
					comConnect = new SerialConnect();
				}

				List<string> comList = comConnect.GetSerialPortNames();
				if (comList == null || comList.Count == 0)
				{
					//MessageBox.Show();
					return;
				}

				foreach (string comName in comList) {
					deviceComboBox.Items.Add(comName);
				}
			}
			else {

			}

			if (deviceComboBox.Items.Count == 0)
			{
				MessageBox.Show("未找到可用设备，请检查设备连接后重试。");
				connectButton.Enabled = false;
				deviceComboBox.Enabled = false;
			}
			else {
				connectButton.Enabled = true;
				deviceComboBox.Enabled = true;
				deviceComboBox.SelectedIndex = 0;
			}
		}

		private void refreshButton_Click(object sender, EventArgs e)
		{
			refreshDeviceComboBox();
		}

		private void connectButton_Click(object sender, EventArgs e)
		{
			if (isConnectByCom)
			{
				if (comConnect == null) {
					setAllStatusLabel1("打开串口失败，原因是：comConnect为null");
					return;
				}

				try
				{
					comConnect.OpenSerialPort(deviceComboBox.Text);
					setAllStatusLabel1("已打开串口(" + deviceComboBox.Text + ")");
					connStatus = ConnectStatus.Normal;
					enableAllTabPages();
					enableAllButtons();
				}
				catch (Exception ex) {
					setAllStatusLabel1("打开串口失败，原因是：" + ex.Message);
					connStatus = ConnectStatus.No;
				}
			}
			else {

			}
		}

		/// <summary>
		/// 辅助方法：打开串口连接后，才能使所有的连接可用
		/// </summary>
		private void enableAllTabPages()
		{
			lcConnectButton.Enabled = connStatus > ConnectStatus.No;
			ccConnectButton.Enabled = connStatus > ConnectStatus.No;
			kpConnectButton.Enabled = connStatus > ConnectStatus.No;
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
		/// 辅助方法：关闭连接
		/// </summary>
		private void closeConnect()
		{
			throw new NotImplementedException();
		}


		/// <summary>
		/// 事件：点击《灯控 - 回读配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcReadButton_Click(object sender, EventArgs e)
		{
			comConnect.LightControlRead(ComLCReadCompleted, ComLCReadError);
			lcToolStripStatusLabel2.Text = "正在回读灯控配置，请稍候...";

			//TODO 多测试检查以下代码
			//isOvertime = false;
			//while (!isOvertime && lcData == null)
			//{
			//	Thread.Sleep(100);
			//	lcToolStripStatusLabel2.Text = "正在回读灯控配置，请稍候...";
			//}

			//if (lcData != null)
			//{
			//	setLcForm();
			//	lcToolStripStatusLabel2.Text = "成功回读灯控配置";
			//}
			//else
			//{
			//	lcToolStripStatusLabel2.Text = "回读灯控配置失败";
			//}

		}

		public void ComLCConnectCompleted(Object obj) {
			Invoke((EventHandler)delegate {
				connStatus = ConnectStatus.Lc;
				enableAllButtons();
				lcToolStripStatusLabel2.Text = "已切换成灯控配置(connStatus=lc)";
			});
		}

		public void ComLCConnectError() {
			//isOvertime = true;
			MessageBox.Show("请求超时，切换灯控配置失败");
			lcToolStripStatusLabel2.Text = "请求超时，切换灯控配置失败";
			lcToolStripStatusLabel1.Text = "请求超时，设备可能并未连接到串口";
		}

		/// <summary>
		/// 成功切换到中控连接后的操作
		/// </summary>
		/// <param name="obj"></param>
		public void ComCCConnectCompleted(Object obj)
		{
			Invoke((EventHandler)delegate {
				ccToolStripStatusLabel2.Text = "已切换成中控配置(connStatus=cc)";
				connStatus = ConnectStatus.Cc;
				enableAllButtons();
			});
		}

		public void ComCCConnectError()
		{
			Invoke((EventHandler)delegate {
				ccToolStripStatusLabel2.Text = "切换中控配置失败";
				ccDecodeButton.Enabled = false;
				ccDownloadButton.Enabled = false;
			});
		}


		public void ComDownloadCompleted()
		{
			MessageBox.Show("灯控数据下载成功");
		}

		/// <summary>
		///  灯控debug(实时调试的数据)发送成功
		/// </summary>
		/// <param name="obj"></param>
		public void ComLCSendCompleted(Object obj)
		{
			Invoke((EventHandler)delegate
			{
				
			});
		}

		/// <summary>
		///  灯控debug发送出错
		/// </summary>
		/// <param name="obj"></param>
		public void ComLCSendError()
		{
			Invoke((EventHandler)delegate
			{
				lcToolStripStatusLabel2.Text = "灯控已离线，发送失败，请重新连接后重试";
			});
		}


		public void ComLCReadCompleted(Object lcDataTemp)
		{
			Invoke((EventHandler)delegate {
				if (lcDataTemp == null)
				{
					MessageBox.Show("回读数据有异常(lcDataTemp==null),回读失败。");
					lcToolStripStatusLabel2.Text = "回读数据有异常(lcDataTemp==null),回读失败。";
					return;
				}

				lcData = lcDataTemp as LightControlData;
				setLcForm();
				lcToolStripStatusLabel2.Text = "成功回读灯控配置";
			});
		}

		public void ComLCReadError()
		{
			Invoke((EventHandler)delegate {
				lcToolStripStatusLabel2.Text = "回读灯控配置失败";
			});
		}

		/// <summary>
		/// 灯控数据下载成功回调方法
		/// </summary>
		/// <param name="obj"></param>
		public void ComLCDownloadCompleted(Object obj)
		{
			Invoke((EventHandler)delegate {
				lcToolStripStatusLabel2.Text = "灯控配置下载成功,请等待机器重启(约耗时5s)...";
				MessageBox.Show("灯控配置下载成功,请等待机器重启(约耗时5s)。");				
				Thread.Sleep(5000);
				lcConnectButton_Click(null, null);
			});
		}

		// 灯控数据下载错误回调方法
		public void ComLCDownloadError()
		{
			Invoke((EventHandler)delegate
			{
				lcToolStripStatusLabel2.Text = "灯控配置下载失败";
			});
		}

		/// <summary>
		/// 中控配置下载成功
		/// </summary>
		/// <param name="obj"></param>
		public void ComCCDownloadCompleted(Object obj)
		{
			Invoke((EventHandler)delegate {
				ccToolStripStatusLabel2.Text = "中控配置下载成功,请等待机器重启(约耗时5s)...";
				MessageBox.Show("中控配置下载成功,请等待机器重启(约耗时5s)。");
				Thread.Sleep(5000);
				ccConnectButton_Click(null, null);
			});	

		}

		/// <summary>
		/// 中控配置下载出错
		/// </summary>
		public void ComCCDownloadError()
		{
			Invoke((EventHandler)delegate
			{
				ccToolStripStatusLabel2.Text = "中控配置下载失败";
			});
		}

		/// <summary>
		/// 启动《中控调试解码》成功
		/// </summary>
		/// <param name="obj"></param>
		public void ComCCStartCompleted(Object obj)
		{
			Invoke((EventHandler)delegate
			{
				ccToolStripStatusLabel2.Text = "灯控解码开启成功";
				isDecoding = true;
				ccDecodeButton.Text = "关闭解码" ;
				ccDecodeRichTextBox.Enabled = true;
			});
		}

		/// <summary>
		/// 启动《中控调试解码》失败
		/// </summary>
		public void ComCCStartError()
		{
			Invoke((EventHandler)delegate
			{
				ccToolStripStatusLabel2.Text = "灯控解码开启失败";
			});
		}

		/// <summary>
		///  中控解码 红外码值 回读
		/// </summary>
		/// <param name="obj"></param>
		public void ComCCListen(Object obj)
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
		///  结束《中控调试解码》成功
		/// </summary>
		/// <param name="obj"></param>
		public void ComCCStopCompleted(Object obj)
		{
			Invoke((EventHandler)delegate
			{
				ccToolStripStatusLabel2.Text = "成功关闭中控解码";
				isDecoding = false;
				ccDecodeButton.Text = "开启解码";
				ccDecodeRichTextBox.Enabled = false ;
			});
		}



		/// <summary>
		/// 结束《中控调试解码》失败
		/// </summary>
		public void ComCCEndError()
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
			comConnect.CenterControlConnect(ComCCConnectCompleted, ComCCConnectError);
		}

		/// <summary>
		/// 事件：点击《连接灯控》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcConnectButton_Click(object sender, EventArgs e)
		{
			comConnect.LightControlConnect(ComLCConnectCompleted, ComLCConnectError);
		}


		/// <summary>
		/// 事件：点击《连接墙板》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpConnectButton_Click(object sender, EventArgs e)
		{
			comConnect.PassThroughKeyPressConnect(ComKPFirstConnectCompleted, ComKPConnectError);
		}

		/// <summary>
		///  连接墙板成功
		/// </summary>
		/// <param name="obj"></param>
		public void ComKPFirstConnectCompleted(Object obj)
		{
			Invoke((EventHandler)delegate
			{
				kpToolStripStatusLabel2.Text = "成功连接墙板(connStatus=kp)";
				connStatus = ConnectStatus.Kp;
				enableAllButtons();

				Thread.Sleep(500);								
				kpReadButton_Click(null, null);

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
		/// 刷新所有被connStatus影响的按键
		/// </summary>
		private void enableAllButtons() {
			// 灯控相关按键
			lcReadButton.Enabled = connStatus == ConnectStatus.Lc;
			lcDownloadButton.Enabled = connStatus == ConnectStatus.Lc;

			// 灯控相关
			ccDecodeButton.Enabled = connStatus == ConnectStatus.Cc;
			ccDownloadButton.Enabled = connStatus == ConnectStatus.Cc;

			// 墙板相关按键
			kpDownloadButton.Enabled = connStatus == ConnectStatus.Kp;
			kpReadButton.Enabled = connStatus == ConnectStatus.Kp;
			kpListenButton.Enabled = connStatus == ConnectStatus.Kp;
			kpLoadButton.Enabled = connStatus == ConnectStatus.Kp;
		}
	

		/// <summary>
		/// 辅助方法：定时器自动重连墙板的方法，此回调方法无需定义执行任何操作
		/// </summary>
		/// <param name="obj"></param>
		public void ComKPTimerConnectCompleted(Object obj)
		{			
			Invoke((EventHandler)delegate
			{

			});
		}

		/// <summary>
		/// 辅助方法：定时器定时执行的方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpOnTimer(object sender, ElapsedEventArgs e)
		{
			Invoke((EventHandler)delegate {
				if (comConnect != null) {
					comConnect.PassThroughKeyPressConnect(ComKPTimerConnectCompleted, ComKPConnectError);	
				}
			});
		}

		/// <summary>
		/// 连接墙板失败
		/// </summary>
		public void ComKPConnectError()
		{
			Invoke((EventHandler)delegate
			{				
				kpToolStripStatusLabel2.Text = "连接墙板失败";			

				//MARK：连接墙板失败，是否还要进行其他操作？
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
			comConnect.PassThroughKeyPressSetClickListener(ComKPStartListenClick);
		}

		/// <summary>
		/// 事件：点击《读取码值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpReadButton_Click(object sender, EventArgs e)
		{
			comConnect.PassThroughKeyPressRead(ComKPReadCompleted, ComKPReadError);
			kpToolStripStatusLabel2.Text = "正在读取墙板码值，请稍候..." ;
			this.Cursor = Cursors.WaitCursor;
			this.Enabled = false;
		}

		/// <summary>
		///  读取墙板码值成功
		/// </summary>
		/// <param name="obj"></param>
		public void ComKPReadCompleted(Object obj)
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
				this.Enabled = true;
				this.Cursor = Cursors.Default;
			});			
		}

		/// <summary>
		/// 读取墙板码值失败
		/// </summary>
		public void ComKPReadError()
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
		public void ComKPStartListenClick(Object obj)
		{			
			Invoke((EventHandler)delegate
			{
				if (obj == null) {
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

		}

		/// <summary>
		/// 事件：点击《墙板-读取按键位置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpPositonLoadButton_Click(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// 事件：点击《墙板-保存文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpSaveButton_Click(object sender, EventArgs e)
		{

		}
	}
}