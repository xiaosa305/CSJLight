using DMX512;
using LightController.Ast;
using LightController.Common;
using LightController.Tools;
using OtherTools;
using System;
using System.Collections;
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
	public partial class NewMainForm : MainFormInterface
	{

		public NewMainForm()
		{
			InitializeComponent();

			#region 动态读取全局配置



			// 动态更改软件名称
			softwareName = new IniFileAst(Application.StartupPath + @"/GlobalSet.ini").ReadString("Show", "softwareName", "TRANS-JOY Dimmer System");
			Text = softwareName;
			// 动态设定软件存储目录
			savePath = @IniFileAst.GetSavePath(Application.StartupPath);
			// 动态显示测试按钮
			bool isShowTestButton = IniFileAst.GetButtonShow(Application.StartupPath, "testButton");
			// 动态显示硬件升级按钮
			hardwareUpdateToolStripMenuItem.Enabled = IniFileAst.GetButtonShow(Application.StartupPath, "hardwareUpdateButton");

			//MARK：添加这一句，会去掉其他线程使用本ui空间的问题。
			//CheckForIllegalCrossThreadCalls = false;

			#endregion

			#region 皮肤相关代码
			IniFileAst iniFileAst = new IniFileAst(Application.StartupPath + @"\GlobalSet.ini");
			string skin = iniFileAst.ReadString("SkinSet", "skin", "");
			if (!String.IsNullOrEmpty(skin))
			{
				this.skinEngine1.SkinFile = Application.StartupPath + "\\irisSkins\\" + skin;
			}
			DirectoryInfo fdir = new DirectoryInfo(Application.StartupPath + "\\irisSkins");
			try
			{
				FileInfo[] file = fdir.GetFiles();
				if (file.Length > 0)
				{
					skinComboBox.Items.Add("更改皮肤");
					foreach (var item in file)
					{
						if (item.FullName.EndsWith(".ssk"))
						{
							skinComboBox.Items.Add(item.Name.Substring(0, item.Name.Length - 4));
						}
					}
					skinComboBox.SelectedIndex = 0;
					skinComboBox.Show();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			#endregion

			#region 几个下拉框的初始化及赋值
			//添加FramList.txt中的场景列表
			AllFrameList = TextAst.Read(Application.StartupPath + @"\FrameList.txt");
			// 场景选项框			
			foreach (string frame in AllFrameList)
			{
				frameComboBox.Items.Add(frame);
			}
			FrameCount = AllFrameList.Count;
			if (FrameCount == 0)
			{
				MessageBox.Show("FrameList.txt中的场景不可为空，否则软件无法使用，请修改后重启。");
				exit();
			}
			frameComboBox.SelectedIndex = 0;
			//模式选项框
			modeComboBox.Items.AddRange(new object[] { "常规模式", "音频模式" });
			modeComboBox.SelectedIndex = 0;

			// 《统一跳渐变》numericUpDown不得为空，否则会造成点击后所有通道的changeMode形式上为空（不过Value不是空）
			unifyChangeModeComboBox.SelectedIndex = 1;
			#endregion
		}

		private void NewMainForm_Load(object sender, EventArgs e)
		{
			for (int i = 0; i < 32; i++)
			{
				tdPanels[i] = new Panel();
				tdNoLabels[i] = new Label();
				tdNameLabels[i] = new Label();
				tdTrackBars[i] = new TrackBar();
				tdValueNumericUpDowns[i] = new NumericUpDown();
				tdCmComboBoxes[i] = new ComboBox();
				tdStNumericUpDowns[i] = new NumericUpDown();
				// 
				// tdNoLabel1
				// 
				this.tdNoLabels[i].AutoSize = true;
				this.tdNoLabels[i].Location = new System.Drawing.Point(15, 18);
				this.tdNoLabels[i].Name = "tdNoLabel1";
				this.tdNoLabels[i].Size = new System.Drawing.Size(47, 12);
				this.tdNoLabels[i].TabIndex = 3;
				this.tdNoLabels[i].Text = "通道" + (i + 1);
				// 
				// tdTrackBar1
				// 
				this.tdTrackBars[i].AutoSize = false;
				this.tdTrackBars[i].BackColor = System.Drawing.Color.MintCream;
				this.tdTrackBars[i].Location = new System.Drawing.Point(32, 35);
				this.tdTrackBars[i].Maximum = 255;
				this.tdTrackBars[i].Name = "tdTrackBar1";
				this.tdTrackBars[i].Orientation = System.Windows.Forms.Orientation.Vertical;
				this.tdTrackBars[i].Size = new System.Drawing.Size(35, 188);
				this.tdTrackBars[i].TabIndex = 0;
				this.tdTrackBars[i].TickFrequency = 0;
				this.tdTrackBars[i].TickStyle = System.Windows.Forms.TickStyle.None;
				// 
				// tdNameLabel1
				// 
				this.tdNameLabels[i].Font = new System.Drawing.Font("宋体", 8F);
				this.tdNameLabels[i].Location = new System.Drawing.Point(17, 47);
				this.tdNameLabels[i].Name = "tdNameLabel1";
				this.tdNameLabels[i].Size = new System.Drawing.Size(14, 153);
				this.tdNameLabels[i].TabIndex = 23;
				this.tdNameLabels[i].Text = "x/y轴转速" + (i + 1);
				this.tdNameLabels[i].TextAlign = System.Drawing.ContentAlignment.TopCenter;
				// 
				// tdValueNumericUpDown1
				// 
				this.tdValueNumericUpDowns[i].Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
				this.tdValueNumericUpDowns[i].Location = new System.Drawing.Point(17, 223);
				this.tdValueNumericUpDowns[i].Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
				this.tdValueNumericUpDowns[i].Name = "tdValueNumericUpDown1";
				this.tdValueNumericUpDowns[i].Size = new System.Drawing.Size(50, 20);
				this.tdValueNumericUpDowns[i].TabIndex = 1;
				this.tdValueNumericUpDowns[i].TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

				// 
				// tdCmComboBox1
				// 
				this.tdCmComboBoxes[i].FormattingEnabled = true;
				this.tdCmComboBoxes[i].Location = new System.Drawing.Point(12, 247);
				this.tdCmComboBoxes[i].Name = "tdCmComboBox1";
				this.tdCmComboBoxes[i].Size = new System.Drawing.Size(60, 20);
				this.tdCmComboBoxes[i].TabIndex = 2;

				// 
				// tdStNumericUpDown1
				// 
				this.tdStNumericUpDowns[i].Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
				this.tdStNumericUpDowns[i].Location = new System.Drawing.Point(12, 271);
				this.tdStNumericUpDowns[i].Name = "tdStNumericUpDown1";
				this.tdStNumericUpDowns[i].Size = new System.Drawing.Size(60, 20);
				this.tdStNumericUpDowns[i].TabIndex = 1;
				this.tdStNumericUpDowns[i].TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

				// 
				// tdPanel
				// 
				this.tdPanels[i].Controls.Add(this.tdNameLabels[i]);
				this.tdPanels[i].Controls.Add(this.tdNoLabels[i]);
				this.tdPanels[i].Controls.Add(this.tdCmComboBoxes[i]);
				this.tdPanels[i].Controls.Add(this.tdStNumericUpDowns[i]);
				this.tdPanels[i].Controls.Add(this.tdValueNumericUpDowns[i]);
				this.tdPanels[i].Controls.Add(this.tdTrackBars[i]);
				this.tdPanels[i].Location = new System.Drawing.Point(3, 3);
				this.tdPanels[i].Name = "tdPanel" + (i + 1);
				this.tdPanels[i].Size = new System.Drawing.Size(84, 297);
				this.tdPanels[i].TabIndex = 24;

				this.tdPanels[i].Visible = true;
				this.tdPanels[i].Tag = 9999;

				this.tdFlowLayoutPanel.Controls.Add(this.tdPanels[i]);

			}


		}


		#region 几个基类的纯虚函数在子类的实现
		protected override void enableGlobalSet(bool enable) { } // 是否显示《全局设置》等

		/// <summary>
		///  辅助方法：是否显示《 存、取 灯具位置》	
		/// </summary>
		/// <param name="enableSave"></param>
		/// <param name="enableLoad"></param>
		protected override void enableSLArrange(bool enableSave, bool enableLoad)
		{
			//saveArrangeToolStripMenuItem.Enabled = enableSave;
			//loadArrangeToolStripMenuItem.Enabled = enableLoad;
		}

		/// <summary>
		/// 辅助方法：是否使能《重新加载灯具图片》
		/// </summary>
		protected override void enableRefreshPic(bool enable)
		{
			//refreshPicToolStripMenuItem.Enabled = enable;
		}

		/// <summary>
		/// 辅助方法：是否显示《保存工程》等
		/// </summary>
		/// <param name="enable"></param>
		protected override void enableSave(bool enable)
		{
			saveProjectButton.Enabled = enable;
			exportProjectButton.Enabled = enable;
			saveFrameButton.Enabled = enable;
			useFrameButton.Enabled = enable;
			closeProjectButton.Enabled = enable;
		}

		/// <summary>
		/// 辅助方法：是否显示playPanel
		/// </summary>
		/// <param name="visible"></param>
		protected override void showPlayPanel(bool visible)
		{
			playPanel.Visible = visible;
		}

		/// <summary>
		/// 辅助方法：清空syncStep
		/// </summary>
		public override void ResetSyncMode()
		{
			syncButton.Text = "进入同步";
			isSyncMode = false;
		}

		/// <summary>
		/// 设置提示信息
		/// </summary>
		/// <param name="notice"></param>
		public override void SetNotice(string notice)
		{
			myStatusLabel.Text = notice;
		}

		/// <summary>
		/// 设置是否忙时
		/// </summary>
		/// <param name="buzy"></param>
		protected override void setBusy(bool busy)
		{
			this.Cursor = busy ? Cursors.WaitCursor : Cursors.Default;



		}


		#endregion


		#region 各类点击事件


		/// <summary>
		/// 事件：点击《灯库编辑》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightLibraryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openLightEditor();
		}



		private void skinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			string sskName = skinComboBox.Text;
			if (String.IsNullOrEmpty(sskName) || sskName.Equals("更改皮肤"))
			{
				this.skinEngine1.Active = false;
				return;
			}
			this.skinEngine1.Active = true;
			this.skinEngine1.SkinFile = Application.StartupPath + "\\irisSkins\\" + sskName + ".ssk";
		}

		/// <summary>
		/// 事件：点击《打开工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openProjectButton_Click(object sender, EventArgs e)
		{
			new OpenForm(this, currentProjectName).ShowDialog();
		}


		/// <summary>
		/// 辅助方法： 清空相关的所有数据（关闭工程、新建工程、打开工程都会用到）
		/// -- 子类中需有针对该子类内部自己的部分代码（如重置listView或禁用stepPanel等）
		/// </summary>
		protected override void clearAllData()
		{
			// 从此处起为子类的实现
			//MARK＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋2222
			this.Text = "Dimmer System";
			lightsListView.Clear();
			stepPanel.Enabled = false;
			hideAllTDPanels();
			showStepLabel(0, 0);
			editLightInfo(null);
			enableSingleMode(true);
			endview(); // 清空数据时，应该结束预览。
		}

		/// <summary>
		/// 辅助方法：结束预览
		/// </summary>
		private void endview()
		{

		}

		/// <summary>
		/// 辅助方法：进入《单灯模式》
		/// </summary>
		/// <param name="v"></param>
		private void enableSingleMode(bool v)
		{

		}

		/// <summary>
		/// 辅助方法：根据传进来的LightAst对象，修改当前灯具内的显示内容
		/// </summary>
		/// <param name="lightAst"></param>
		private void editLightInfo(LightAst lightAst)
		{
			if (lightAst == null)
			{
				currentLightPictureBox.Image = null;
				lightNameLabel.Text = null;
				lightTypeLabel.Text = null;
				lightsAddrLabel.Text = null;
				return;
			}

			lightNameLabel.Text = "灯具厂商：" + lightAst.LightName;
			lightTypeLabel.Text = "灯具型号：" + lightAst.LightType;
			lightsAddrLabel.Text = "灯具地址：" + lightAst.LightAddr;
			selectedLightName = lightAst.LightName + "-" + lightAst.LightType;
			try
			{
				currentLightPictureBox.Image = Image.FromFile(savePath + @"\LightPic\" + lightAst.LightPic);
			}
			catch (Exception)
			{
				currentLightPictureBox.Image = global::LightController.Properties.Resources.灯光图;
			}
		}

		/// <summary>
		/// 辅助方法：显示步数
		/// </summary>		
		private void showStepLabel(int currentStep, int totalStep)
		{
			// 1. 设label的Text值					   
			stepLabel.Text = MathAst.GetFourWidthNumStr(currentStep, true) + "/" + MathAst.GetFourWidthNumStr(totalStep, false);

			// 2.1 设定《删除步》按钮是否可用
			deleteStepButton.Enabled = totalStep != 0;

			// 2.2 设定《追加步》、《前插入步》《后插入步》按钮是否可用			
			bool insertEnabled = totalStep < MAX_STEP;
			addStepButton.Enabled = insertEnabled;
			insertAfterButton.Enabled = insertEnabled;
			insertBeforeButton.Enabled = insertEnabled && currentStep > 0;

			// 2.3 设定《上一步》《下一步》是否可用			
			backStepButton.Enabled = totalStep > 1;
			nextStepButton.Enabled = totalStep > 1;

			// 3 设定《复制(多)步》是否可用
			copyStepButton.Enabled = currentStep > 0;
			pasteStepButton.Enabled = currentStep > 0 && tempStep != null;

			multiCopyButton.Enabled = currentStep > 0;
			multiPasteButton.Enabled = TempMaterialAst != null && TempMaterialAst.Mode == mode;

			// 4.设定统一调整区是否可用
			unifyPanel.Enabled = totalStep != 0;

			// 5.处理选择步数的框及按钮
			chooseStepNumericUpDown.Enabled = totalStep != 0;
			chooseStepNumericUpDown.Minimum = totalStep != 0 ? 1 : 0;
			chooseStepNumericUpDown.Maximum = totalStep;
			chooseStepButton.Enabled = totalStep != 0;
		}

		private void hideAllTDPanels()
		{
			for (int i = 0; i < 32; i++)
			{
				//tdPanels[i].Hide();
			}
			unifyPanel.Enabled = false;
		}

		/// <summary>
		/// 事件：点击《新建工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newProjectButton_Click(object sender, EventArgs e)
		{
			//每次打开新建窗口时，先将isCreateSuccess设为false;避免取消新建，仍会打开添加灯。
			IsCreateSuccess = false;

			new NewForm(this).ShowDialog();

			//当IsCreateSuccess==true时(NewForm中确定新建之后会修改IsCreateSuccess值)，打开灯具列表
			if (IsCreateSuccess)
			{
				lightListToolStripMenuItem_Click(null, null);
			}
		}

		/// <summary>
		/// 事件：点击《灯具列表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new LightsForm(this, lightAstList).ShowDialog();
		}


		/// <summary>
		/// 事件：更改lightsListView的选中项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lightsListView.SelectedIndices.Count > 0)
			{
				selectedIndex = lightsListView.SelectedIndices[0];
				generateLightData();
			}
		}



		/// <summary>
		/// 事件：点击《连接设备|断开连接》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectButton_Click(object sender, EventArgs e)
		{
			playTools = PlayTools.GetInstance();

			//// 如果还没连接（按钮显示为“连接设备”)，那就连接
			//if (!isConnected)
			//{
			//	if (isConnectCom)
			//	{
			//		if (String.IsNullOrEmpty(comName))
			//		{
			//			MessageBox.Show("未选中可用串口。");
			//			return;
			//		}
			//		playTools.ConnectDevice(comName);
			//		EnableConnectedButtons(true);
			//	}
			//	else
			//	{
			//		if (String.IsNullOrEmpty(comName) || deviceSkinComboBox.SelectedIndex < 0)
			//		{
			//			MessageBox.Show("未选中可用网络连接。");
			//			return;
			//		}

			//		IPAst ipAst = ipaList[deviceSkinComboBox.SelectedIndex];
			//		ConnectTools.GetInstance().Start(ipAst.LocalIP);
			//		playTools.StartInternetPreview(ipAst.DeviceIP, new NetworkDebugReceiveCallBack(this), eachStepTime);
			//	}
			//}
			//else //否则( 按钮显示为“断开连接”）断开连接
			//{
			//	if (isConnectCom)
			//	{
			//		playTools.CloseDevice();
			//	}
			//	else
			//	{
			//		playTools.StopInternetPreview(new NetworkEndDebugReceiveCallBack());
			//	}

			//	previewSkinButton.Image = global::LightController.Properties.Resources.浏览效果前;
			//	EnableConnectedButtons(false);

			//	//MARK：11.23 延迟的骗术，在每次断开连接后立即重新搜索网络设备并建立socket连接。
			//	if (!isConnectCom)
			//	{
			//		Thread.Sleep(500);
			//		refreshNetworkList();
			//	}
			//}
		}

		/// <summary>
		/// 事件：点击《硬件配置 - 打开配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hardwareSetOpenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new HardwareSetChooseForm(this).ShowDialog();
		}

		/// <summary>
		/// 时间：点击《硬件配置 - 新建配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hardwareSetNewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new HardwareSetForm(this, null, null).ShowDialog();
		}

		/// <summary>
		/// 事件：点击《硬件配置 - 硬件升级》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hardwareUpdateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new HardwareUpdateForm(this, binPath).ShowDialog();
		}

		/// <summary>
		/// 事件：点击《工程相关 - 全局配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void globalSetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new GlobalSetForm(this, globalIniPath).ShowDialog();
		}

		/// <summary>
		/// 事件：点击《工程相关 - 摇麦配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ymToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new YMSetForm(this, globalIniPath).ShowDialog();
		}

		/// <summary>
		/// 事件：点击《工程相关 - 工程更新》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void projectUpdateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (isConnected)
			{
				connectButton_Click(null, null);
			}

			new ProjectUpdateForm(this, GetDBWrapper(false), globalIniPath, projectPath).ShowDialog();
		}

		/// <summary>
		/// 事件：点击《其他工具 - 外设配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// 若要进入《其他工具》，应该先将连接断开
			if (isConnected)
			{
				connectButton_Click(null, null);
			}

			new NewToolsForm(this).ShowDialog();
		}

		/// <summary>
		/// 事件：点击《其他工具 - 传视界灯控工具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void QDControllerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(Application.StartupPath + @"\QDController\灯光控制器.exe");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// 事件：点击《其他工具 - 传视界中控工具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CenterControllerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(Application.StartupPath + @"\CenterController\KTV中央控制器.exe");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 事件：点击《其他工具 - 传视界墙板工具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void KeyPressToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(Application.StartupPath + @"\KeyPress\墙板码值.exe");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 事件：点击《退出程序》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			exit();
		}

		/// <summary>
		/// 事件:点击《playPanel - 刷新列表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshDeviceButton_Click(object sender, EventArgs e)
		{
			if (isConnectCom)
			{
				refreshComList();
			}
			else
			{
				refreshNetworkList();
			}
		}

		#endregion


		/// <summary>
		/// 辅助方法：初始化灯具数据。
		/// 0.先查看当前内存是否已有此数据 
		/// 1.若还未有，则取出相关的ini进行渲染
		/// 2.若内存内 或 数据库内已有相关数据，则使用这个数据。
		/// </summary>
		/// <param name="la"></param>
		private void generateLightData()
		{
			if (selectedIndex == -1)
			{
				return;
			}
			LightAst lightAst = lightAstList[selectedIndex];

			// 1.在右侧灯具信息内显示选中灯具相关信息
			editLightInfo(lightAst);

			//2.判断是不是已经有stepTemplate了
			// ①若无，则生成数据，并hideAllTongdao 并设stepLabel为“0/0” --> 因为刚创建，肯定没有步数	
			// ②若有，还需判断该LightData的LightStepWrapperList[frame,mode]是不是为null
			//			若是null，则说明该FM下，并未有步数，hideAllTongdao
			//			若不为null，则说明已有数据，
			LightWrapper lightWrapper = lightWrapperList[selectedIndex];
			if (lightWrapper.StepTemplate == null)
			{
				lightWrapper.StepTemplate = generateStepTemplate(lightAst);
			}
			stepPanel.Enabled = true;

			//3.手动刷新当前步信息
			RefreshStep();
		}

		

		/// <summary>
		/// 辅助方法：重新搜索com列表：供启动时及需要重新搜索设备时使用。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshComList()
		{
			// 动态加载可用的dmx512串口列表		 
			deviceComboBox.Items.Clear();
			SerialPortTools comTools = SerialPortTools.GetInstance();
			comList = comTools.GetDMX512DeviceList();
			if (comList != null && comList.Length > 0)
			{
				foreach (string com in comList)
				{
					deviceComboBox.Items.Add(com);
				}
				deviceComboBox.SelectedIndex = 0;
				deviceComboBox.Enabled = true;
			}
			else
			{
				deviceComboBox.Text = "";
				deviceComboBox.Enabled = false;
				deviceComboBox.Enabled = false;
			}
		}


		/// <summary>
		/// TODO：11.22 网络连接
		/// 辅助方法：重新搜索ip列表-》填入deviceComboBox中
		/// </summary>
		private void refreshNetworkList()
		{

			deviceComboBox.Items.Clear();
			deviceComboBox.Enabled = false;
			ipaList = new List<IPAst>();

			connectTools = ConnectTools.GetInstance();
			// 先获取本地ip列表，遍历使用这些ip，搜索设备;-->都搜索完毕再统一显示
			IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
			foreach (IPAddress ip in ipe.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork) //当前ip为ipv4时，才加入到列表中
				{
					connectTools.Start(ip.ToString());
					connectTools.SearchDevice();
					// 需要延迟片刻，才能找到设备;	故在此期间，主动暂停片刻
					Thread.Sleep(SkinMainForm.NETWORK_WAITTIME);
				}
			}
		}

		/// <summary>
		/// 事件：点击《以网络|串口连接》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void changeConnectMethodButton_Click(object sender, EventArgs e)
		{
			isConnectCom = !isConnectCom;
			changeConnectMethodButton.Text = isConnectCom ? "以网络连接" : "以串口连接";			
			refreshDeviceButton_Click(null, null);  // 切换连接后，手动帮用户搜索相应的设备列表。
		}

		/// <summary>
		/// 事件：点击《结束预览》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void endviewButton_Click(object sender, EventArgs e)
		{
			endview();
			SetNotice("已结束预览。");
		}
		
		/// <summary>
		/// 事件：点击《全部归零》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zeroButton_Click(object sender, EventArgs e)
		{
			StepWrapper currentStep = getCurrentStepWrapper();
			for (int i = 0; i < currentStep.TongdaoList.Count; i++)
			{
				getCurrentStepWrapper().TongdaoList[i].ScrollValue = 0;
			}

			if (isMultiMode)
			{
				copyCommonValueToAll(getCurrentStep(), WHERE.SCROLL_VALUE, 0);
			}

			RefreshStep();
		}
	}
}
