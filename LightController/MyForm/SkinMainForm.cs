using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using DMX512;
using LightController.Ast;
using LightController.Tools;
using LightController.Common;
using CCWin.SkinControl;
using System.IO;
using System.Net;
using System.Net.Sockets;
using LightController.Utils;
using LightController.Tools.CSJ.IMPL;
using OtherTools;

namespace LightController.MyForm
{
	public partial class SkinMainForm : MainFormBase
	{
		private bool isPainting = false;		

		public SkinMainForm()
		{
			initGeneralControls();
			InitializeComponent();

			// 动态设定软件存储目录
			savePath = @IniFileAst.GetSavePath(Application.StartupPath);

			// 动态显示测试按钮
			softwareName = IniFileAst_UTF8.ReadString(Application.StartupPath + @"/GlobalSet.ini", "Show", "softwareName", "TRANS-JOY");
			this.Text = softwareName + " Dimmer System";

			bool isShowTestButton = IniFileAst.GetControlShow(Application.StartupPath, "testButton");
			testGroupBox.Visible = isShowTestButton;
			bigTestButton.Visible = isShowTestButton;

			//MARK：添加这一句，会去掉其他线程使用本ui空间的问题。
			CheckForIllegalCrossThreadCalls = false;

			// 动态显示硬件升级按钮
			bool isShowHardwareUpddate = IniFileAst.GetControlShow(Application.StartupPath, "hardwareUpdateButton");
			hardwareUpdateSkinButton.Visible = isShowHardwareUpddate;

			#region 初始化各种辅助数组

			tdPanels[0] = tdPanel1;
			tdPanels[1] = tdPanel2;
			tdPanels[2] = tdPanel3;
			tdPanels[3] = tdPanel4;
			tdPanels[4] = tdPanel5;
			tdPanels[5] = tdPanel6;
			tdPanels[6] = tdPanel7;
			tdPanels[7] = tdPanel8;
			tdPanels[8] = tdPanel9;
			tdPanels[9] = tdPanel10;
			tdPanels[10] = tdPanel11;
			tdPanels[11] = tdPanel12;
			tdPanels[12] = tdPanel13;
			tdPanels[13] = tdPanel14;
			tdPanels[14] = tdPanel15;
			tdPanels[15] = tdPanel16;
			tdPanels[16] = tdPanel17;
			tdPanels[17] = tdPanel18;
			tdPanels[18] = tdPanel19;
			tdPanels[19] = tdPanel20;
			tdPanels[20] = tdPanel21;
			tdPanels[21] = tdPanel22;
			tdPanels[22] = tdPanel23;
			tdPanels[23] = tdPanel24;
			tdPanels[24] = tdPanel25;
			tdPanels[25] = tdPanel26;
			tdPanels[26] = tdPanel27;
			tdPanels[27] = tdPanel28;
			tdPanels[28] = tdPanel29;
			tdPanels[29] = tdPanel30;
			tdPanels[30] = tdPanel31;
			tdPanels[31] = tdPanel32;

			tdNoLabels[0] = tdNoLabel1;
			tdNoLabels[1] = tdNoLabel2;
			tdNoLabels[2] = tdNoLabel3;
			tdNoLabels[3] = tdNoLabel4;
			tdNoLabels[4] = tdNoLabel5;
			tdNoLabels[5] = tdNoLabel6;
			tdNoLabels[6] = tdNoLabel7;
			tdNoLabels[7] = tdNoLabel8;
			tdNoLabels[8] = tdNoLabel9;
			tdNoLabels[9] = tdNoLabel10;
			tdNoLabels[10] = tdNoLabel11;
			tdNoLabels[11] = tdNoLabel12;
			tdNoLabels[12] = tdNoLabel13;
			tdNoLabels[13] = tdNoLabel14;
			tdNoLabels[14] = tdNoLabel15;
			tdNoLabels[15] = tdNoLabel16;
			tdNoLabels[16] = tdNoLabel17;
			tdNoLabels[17] = tdNoLabel18;
			tdNoLabels[18] = tdNoLabel19;
			tdNoLabels[19] = tdNoLabel20;
			tdNoLabels[20] = tdNoLabel21;
			tdNoLabels[21] = tdNoLabel22;
			tdNoLabels[22] = tdNoLabel23;
			tdNoLabels[23] = tdNoLabel24;
			tdNoLabels[24] = tdNoLabel25;
			tdNoLabels[25] = tdNoLabel26;
			tdNoLabels[26] = tdNoLabel27;
			tdNoLabels[27] = tdNoLabel28;
			tdNoLabels[28] = tdNoLabel29;
			tdNoLabels[29] = tdNoLabel30;
			tdNoLabels[30] = tdNoLabel31;
			tdNoLabels[31] = tdNoLabel32;

			tdNameLabels[0] = tdNameLabel1;
			tdNameLabels[1] = tdNameLabel2;
			tdNameLabels[2] = tdNameLabel3;
			tdNameLabels[3] = tdNameLabel4;
			tdNameLabels[4] = tdNameLabel5;
			tdNameLabels[5] = tdNameLabel6;
			tdNameLabels[6] = tdNameLabel7;
			tdNameLabels[7] = tdNameLabel8;
			tdNameLabels[8] = tdNameLabel9;
			tdNameLabels[9] = tdNameLabel10;
			tdNameLabels[10] = tdNameLabel11;
			tdNameLabels[11] = tdNameLabel12;
			tdNameLabels[12] = tdNameLabel13;
			tdNameLabels[13] = tdNameLabel14;
			tdNameLabels[14] = tdNameLabel15;
			tdNameLabels[15] = tdNameLabel16;
			tdNameLabels[16] = tdNameLabel17;
			tdNameLabels[17] = tdNameLabel18;
			tdNameLabels[18] = tdNameLabel19;
			tdNameLabels[19] = tdNameLabel20;
			tdNameLabels[20] = tdNameLabel21;
			tdNameLabels[21] = tdNameLabel22;
			tdNameLabels[22] = tdNameLabel23;
			tdNameLabels[23] = tdNameLabel24;
			tdNameLabels[24] = tdNameLabel25;
			tdNameLabels[25] = tdNameLabel26;
			tdNameLabels[26] = tdNameLabel27;
			tdNameLabels[27] = tdNameLabel28;
			tdNameLabels[28] = tdNameLabel29;
			tdNameLabels[29] = tdNameLabel30;
			tdNameLabels[30] = tdNameLabel31;
			tdNameLabels[31] = tdNameLabel32;

			tdSkinTrackBars[0] = tdSkinTrackBar1;
			tdSkinTrackBars[1] = tdSkinTrackBar2;
			tdSkinTrackBars[2] = tdSkinTrackBar3;
			tdSkinTrackBars[3] = tdSkinTrackBar4;
			tdSkinTrackBars[4] = tdSkinTrackBar5;
			tdSkinTrackBars[5] = tdSkinTrackBar6;
			tdSkinTrackBars[6] = tdSkinTrackBar7;
			tdSkinTrackBars[7] = tdSkinTrackBar8;
			tdSkinTrackBars[8] = tdSkinTrackBar9;
			tdSkinTrackBars[9] = tdSkinTrackBar10;
			tdSkinTrackBars[10] = tdSkinTrackBar11;
			tdSkinTrackBars[11] = tdSkinTrackBar12;
			tdSkinTrackBars[12] = tdSkinTrackBar13;
			tdSkinTrackBars[13] = tdSkinTrackBar14;
			tdSkinTrackBars[14] = tdSkinTrackBar15;
			tdSkinTrackBars[15] = tdSkinTrackBar16;
			tdSkinTrackBars[16] = tdSkinTrackBar17;
			tdSkinTrackBars[17] = tdSkinTrackBar18;
			tdSkinTrackBars[18] = tdSkinTrackBar19;
			tdSkinTrackBars[19] = tdSkinTrackBar20;
			tdSkinTrackBars[20] = tdSkinTrackBar21;
			tdSkinTrackBars[21] = tdSkinTrackBar22;
			tdSkinTrackBars[22] = tdSkinTrackBar23;
			tdSkinTrackBars[23] = tdSkinTrackBar24;
			tdSkinTrackBars[24] = tdSkinTrackBar25;
			tdSkinTrackBars[25] = tdSkinTrackBar26;
			tdSkinTrackBars[26] = tdSkinTrackBar27;
			tdSkinTrackBars[27] = tdSkinTrackBar28;
			tdSkinTrackBars[28] = tdSkinTrackBar29;
			tdSkinTrackBars[29] = tdSkinTrackBar30;
			tdSkinTrackBars[30] = tdSkinTrackBar31;
			tdSkinTrackBars[31] = tdSkinTrackBar32;

			tdValueNumericUpDowns[0] = tdValueNumericUpDown1;
			tdValueNumericUpDowns[1] = tdValueNumericUpDown2;
			tdValueNumericUpDowns[2] = tdValueNumericUpDown3;
			tdValueNumericUpDowns[3] = tdValueNumericUpDown4;
			tdValueNumericUpDowns[4] = tdValueNumericUpDown5;
			tdValueNumericUpDowns[5] = tdValueNumericUpDown6;
			tdValueNumericUpDowns[6] = tdValueNumericUpDown7;
			tdValueNumericUpDowns[7] = tdValueNumericUpDown8;
			tdValueNumericUpDowns[8] = tdValueNumericUpDown9;
			tdValueNumericUpDowns[9] = tdValueNumericUpDown10;
			tdValueNumericUpDowns[10] = tdValueNumericUpDown11;
			tdValueNumericUpDowns[11] = tdValueNumericUpDown12;
			tdValueNumericUpDowns[12] = tdValueNumericUpDown13;
			tdValueNumericUpDowns[13] = tdValueNumericUpDown14;
			tdValueNumericUpDowns[14] = tdValueNumericUpDown15;
			tdValueNumericUpDowns[15] = tdValueNumericUpDown16;
			tdValueNumericUpDowns[16] = tdValueNumericUpDown17;
			tdValueNumericUpDowns[17] = tdValueNumericUpDown18;
			tdValueNumericUpDowns[18] = tdValueNumericUpDown19;
			tdValueNumericUpDowns[19] = tdValueNumericUpDown20;
			tdValueNumericUpDowns[20] = tdValueNumericUpDown21;
			tdValueNumericUpDowns[21] = tdValueNumericUpDown22;
			tdValueNumericUpDowns[22] = tdValueNumericUpDown23;
			tdValueNumericUpDowns[23] = tdValueNumericUpDown24;
			tdValueNumericUpDowns[24] = tdValueNumericUpDown25;
			tdValueNumericUpDowns[25] = tdValueNumericUpDown26;
			tdValueNumericUpDowns[26] = tdValueNumericUpDown27;
			tdValueNumericUpDowns[27] = tdValueNumericUpDown28;
			tdValueNumericUpDowns[28] = tdValueNumericUpDown29;
			tdValueNumericUpDowns[29] = tdValueNumericUpDown30;
			tdValueNumericUpDowns[30] = tdValueNumericUpDown31;
			tdValueNumericUpDowns[31] = tdValueNumericUpDown32;

			tdChangeModeSkinComboBoxes[0] = tdChangeModeSkinComboBox1;
			tdChangeModeSkinComboBoxes[1] = tdChangeModeSkinComboBox2;
			tdChangeModeSkinComboBoxes[2] = tdChangeModeSkinComboBox3;
			tdChangeModeSkinComboBoxes[3] = tdChangeModeSkinComboBox4;
			tdChangeModeSkinComboBoxes[4] = tdChangeModeSkinComboBox5;
			tdChangeModeSkinComboBoxes[5] = tdChangeModeSkinComboBox6;
			tdChangeModeSkinComboBoxes[6] = tdChangeModeSkinComboBox7;
			tdChangeModeSkinComboBoxes[7] = tdChangeModeSkinComboBox8;
			tdChangeModeSkinComboBoxes[8] = tdChangeModeSkinComboBox9;
			tdChangeModeSkinComboBoxes[9] = tdChangeModeSkinComboBox10;
			tdChangeModeSkinComboBoxes[10] = tdChangeModeSkinComboBox11;
			tdChangeModeSkinComboBoxes[11] = tdChangeModeSkinComboBox12;
			tdChangeModeSkinComboBoxes[12] = tdChangeModeSkinComboBox13;
			tdChangeModeSkinComboBoxes[13] = tdChangeModeSkinComboBox14;
			tdChangeModeSkinComboBoxes[14] = tdChangeModeSkinComboBox15;
			tdChangeModeSkinComboBoxes[15] = tdChangeModeSkinComboBox16;
			tdChangeModeSkinComboBoxes[16] = tdChangeModeSkinComboBox17;
			tdChangeModeSkinComboBoxes[17] = tdChangeModeSkinComboBox18;
			tdChangeModeSkinComboBoxes[18] = tdChangeModeSkinComboBox19;
			tdChangeModeSkinComboBoxes[19] = tdChangeModeSkinComboBox20;
			tdChangeModeSkinComboBoxes[20] = tdChangeModeSkinComboBox21;
			tdChangeModeSkinComboBoxes[21] = tdChangeModeSkinComboBox22;
			tdChangeModeSkinComboBoxes[22] = tdChangeModeSkinComboBox23;
			tdChangeModeSkinComboBoxes[23] = tdChangeModeSkinComboBox24;
			tdChangeModeSkinComboBoxes[24] = tdChangeModeSkinComboBox25;
			tdChangeModeSkinComboBoxes[25] = tdChangeModeSkinComboBox26;
			tdChangeModeSkinComboBoxes[26] = tdChangeModeSkinComboBox27;
			tdChangeModeSkinComboBoxes[27] = tdChangeModeSkinComboBox28;
			tdChangeModeSkinComboBoxes[28] = tdChangeModeSkinComboBox29;
			tdChangeModeSkinComboBoxes[29] = tdChangeModeSkinComboBox30;
			tdChangeModeSkinComboBoxes[30] = tdChangeModeSkinComboBox31;
			tdChangeModeSkinComboBoxes[31] = tdChangeModeSkinComboBox32;

			tdStepTimeNumericUpDowns[0] = tdStepTimeNumericUpDown1;
			tdStepTimeNumericUpDowns[1] = tdStepTimeNumericUpDown2;
			tdStepTimeNumericUpDowns[2] = tdStepTimeNumericUpDown3;
			tdStepTimeNumericUpDowns[3] = tdStepTimeNumericUpDown4;
			tdStepTimeNumericUpDowns[4] = tdStepTimeNumericUpDown5;
			tdStepTimeNumericUpDowns[5] = tdStepTimeNumericUpDown6;
			tdStepTimeNumericUpDowns[6] = tdStepTimeNumericUpDown7;
			tdStepTimeNumericUpDowns[7] = tdStepTimeNumericUpDown8;
			tdStepTimeNumericUpDowns[8] = tdStepTimeNumericUpDown9;
			tdStepTimeNumericUpDowns[9] = tdStepTimeNumericUpDown10;
			tdStepTimeNumericUpDowns[10] = tdStepTimeNumericUpDown11;
			tdStepTimeNumericUpDowns[11] = tdStepTimeNumericUpDown12;
			tdStepTimeNumericUpDowns[12] = tdStepTimeNumericUpDown13;
			tdStepTimeNumericUpDowns[13] = tdStepTimeNumericUpDown14;
			tdStepTimeNumericUpDowns[14] = tdStepTimeNumericUpDown15;
			tdStepTimeNumericUpDowns[15] = tdStepTimeNumericUpDown16;
			tdStepTimeNumericUpDowns[16] = tdStepTimeNumericUpDown17;
			tdStepTimeNumericUpDowns[17] = tdStepTimeNumericUpDown18;
			tdStepTimeNumericUpDowns[18] = tdStepTimeNumericUpDown19;
			tdStepTimeNumericUpDowns[19] = tdStepTimeNumericUpDown20;
			tdStepTimeNumericUpDowns[20] = tdStepTimeNumericUpDown21;
			tdStepTimeNumericUpDowns[21] = tdStepTimeNumericUpDown22;
			tdStepTimeNumericUpDowns[22] = tdStepTimeNumericUpDown23;
			tdStepTimeNumericUpDowns[23] = tdStepTimeNumericUpDown24;
			tdStepTimeNumericUpDowns[24] = tdStepTimeNumericUpDown25;
			tdStepTimeNumericUpDowns[25] = tdStepTimeNumericUpDown26;
			tdStepTimeNumericUpDowns[26] = tdStepTimeNumericUpDown27;
			tdStepTimeNumericUpDowns[27] = tdStepTimeNumericUpDown28;
			tdStepTimeNumericUpDowns[28] = tdStepTimeNumericUpDown29;
			tdStepTimeNumericUpDowns[29] = tdStepTimeNumericUpDown30;
			tdStepTimeNumericUpDowns[30] = tdStepTimeNumericUpDown31;
			tdStepTimeNumericUpDowns[31] = tdStepTimeNumericUpDown32;

			tdTrueTimeLabels[0] = trueTimeLabel1;
			tdTrueTimeLabels[1] = trueTimeLabel2;
			tdTrueTimeLabels[2] = trueTimeLabel3;
			tdTrueTimeLabels[3] = trueTimeLabel4;
			tdTrueTimeLabels[4] = trueTimeLabel5;
			tdTrueTimeLabels[5] = trueTimeLabel6;
			tdTrueTimeLabels[6] = trueTimeLabel7;
			tdTrueTimeLabels[7] = trueTimeLabel8;
			tdTrueTimeLabels[8] = trueTimeLabel9;
			tdTrueTimeLabels[9] = trueTimeLabel10;
			tdTrueTimeLabels[10] = trueTimeLabel11;
			tdTrueTimeLabels[11] = trueTimeLabel12;
			tdTrueTimeLabels[12] = trueTimeLabel13;
			tdTrueTimeLabels[13] = trueTimeLabel14;
			tdTrueTimeLabels[14] = trueTimeLabel15;
			tdTrueTimeLabels[15] = trueTimeLabel16;
			tdTrueTimeLabels[16] = trueTimeLabel17;
			tdTrueTimeLabels[17] = trueTimeLabel18;
			tdTrueTimeLabels[18] = trueTimeLabel19;
			tdTrueTimeLabels[19] = trueTimeLabel20;
			tdTrueTimeLabels[20] = trueTimeLabel21;
			tdTrueTimeLabels[21] = trueTimeLabel22;
			tdTrueTimeLabels[22] = trueTimeLabel23;
			tdTrueTimeLabels[23] = trueTimeLabel24;
			tdTrueTimeLabels[24] = trueTimeLabel25;
			tdTrueTimeLabels[25] = trueTimeLabel26;
			tdTrueTimeLabels[26] = trueTimeLabel27;
			tdTrueTimeLabels[27] = trueTimeLabel28;
			tdTrueTimeLabels[28] = trueTimeLabel29;
			tdTrueTimeLabels[29] = trueTimeLabel30;
			tdTrueTimeLabels[30] = trueTimeLabel31;
			tdTrueTimeLabels[31] = trueTimeLabel32;

			#endregion

			#region 几个下拉框的初始化及赋值

			//添加FramList文本中的场景列表
			AllFrameList = TextAst.Read(Application.StartupPath + @"\FrameList.txt");
			// 场景选项框			
			foreach (string frame in AllFrameList)
			{
				frameSkinComboBox.Items.Add(frame);
			}
			FrameCount = AllFrameList.Count;
			if (FrameCount == 0) {
				MessageBox.Show("FrameList.txt中的场景不可为空，否则软件无法使用，请修改后重启。");
				exit();
			}
			frameSkinComboBox.SelectedIndex = 0;

			//模式选项框
			modeSkinComboBox.Items.AddRange(new object[] { "常规模式", "音频模式" });
			modeSkinComboBox.SelectedIndex = 0;

			// 《统一跳渐变》numericUpDown不得为空，否则会造成点击后所有通道的changeMode形式上为空（不过Value不是空）
			unifyChangeModeSkinComboBox.SelectedIndex = 1;
			#endregion

			#region 各类监听器
			// MARK：SkinMainForm 各种td监听器
			for (int i = 0; i < 32; i++) {

				tdSkinTrackBars[i].MouseEnter += new EventHandler(tdTrackBars_MouseEnter);
				tdSkinTrackBars[i].MouseWheel += new MouseEventHandler(this.tdSkinTrackBars_MouseWheel);
				tdSkinTrackBars[i].ValueChanged += new System.EventHandler(this.tdSkinTrackBars_ValueChanged);

				tdValueNumericUpDowns[i].MouseEnter += new EventHandler(this.tdValueNumericUpDowns_MouseEnter);
				tdValueNumericUpDowns[i].MouseWheel += new MouseEventHandler(this.tdValueNumericUpDowns_MouseWheel);
				tdValueNumericUpDowns[i].ValueChanged += new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);

				tdChangeModeSkinComboBoxes[i].SelectedIndexChanged += new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);

				tdStepTimeNumericUpDowns[i].MouseEnter += new EventHandler(this.tdStepTimeNumericUpDowns_MouseEnter);
				tdStepTimeNumericUpDowns[i].MouseWheel += new MouseEventHandler(this.tdStepTimeNumericUpDowns_MouseWheel);
				tdStepTimeNumericUpDowns[i].ValueChanged += new EventHandler(this.tdStepTimeNumericUpDowns_ValueChanged);

			}
			// 防止人为滚动左侧的labelPanels，用这个监听事件来处理
			labelFlowLayoutPanel.MouseWheel += new MouseEventHandler(this.labelFlowLayoutPanel_MouseWheel);

			// 几个《统一调整区》的鼠标滚动事件绑定
			unifyValueNumericUpDown.MouseEnter += new EventHandler(this.commonValueNumericUpDown_MouseEnter);
			unifyValueNumericUpDown.MouseWheel += new MouseEventHandler(this.commonValueNumericUpDown_MouseWheel);
			unifyValueNumericUpDown.ValueChanged += new System.EventHandler(this.commonValueNumericUpDown_ValueChanged);

			unifyValueTrackBar.MouseEnter += new EventHandler(this.commonValueTrackBar_MouseEnter);
			unifyValueTrackBar.MouseWheel += new MouseEventHandler(this.commonValueTrackBar_MouseWheel);
			unifyValueTrackBar.ValueChanged += new EventHandler(this.commonValueTrackBar_ValuiChanged);

			unifyStepTimeNumericUpDown.MouseEnter += new EventHandler(this.commonStepTimeNumericUpDown_MouseEnter);
			unifyStepTimeNumericUpDown.MouseWheel += new MouseEventHandler(this.commonStepTimeNumericUpDown_MouseWheel);

			#endregion

			// 几个按钮添加提示
			myToolTip.SetToolTip(useFrameSkinButton, "使用本功能，将以选中的场景数据替换当前的场景数据。");
			myToolTip.SetToolTip(chooseStepSkinButton, "跳转指定步");
			myToolTip.SetToolTip(keepSkinButton, "点击此按钮后，当前未选中的其它灯具将会保持它们最后调整时的状态，方便调试。");

			isInit = true;
		}
		
		private void SkinMainForm_Load(object sender, EventArgs e)
		{
			// 启动时刷新可用串口列表;
			//refreshComList();
			
			//MARK：额外处理 lightsSkinListView 会被VS吞掉的问题
			this.lightsSkinListView.HideSelection = true;

		}

		/// <summary>
		/// 事件：界面的Size发生变化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SkinMainForm_SizeChanged(object sender, EventArgs e)
		{
			tdSkinFlowLayoutPanel.AutoScrollPosition = new Point(0, 0); ;
		}
		
		// MARK：SkinMainForm各种工具按钮起点
		#region 工具按钮组 - 非工程相关

		/// <summary>
		/// 事件：点击“灯库编辑”
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightLibrarySkinButton_Click(object sender, EventArgs e)
		{
			openLightEditor();
		}

		/// <summary>
		///  事件：点击《硬件设置》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hardwareSetSkinButton_Click(object sender, EventArgs e)
		{
			new HardwareSetChooseForm(this).ShowDialog();
		}

		/// <summary>
		///  事件：点击《硬件升级》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hardwareUpdateButton_Click(object sender, EventArgs e)
		{
			hardwareUpdateClick();
		}

		/// <summary>
		/// 事件：点击《外设配置》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void otherToolsSkinButton_Click(object sender, EventArgs e)
		{
			newToolClick();
		}

		/// <summary>
		/// 事件：点击《旧版外设配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void otherToolsSkinButton2_Click(object sender, EventArgs e)
		{
			// 若要进入《其他工具》，应该先将连接断开
			if (isConnected)
			{
				connectSkinButton_Click(null, null);
			}
			new OldToolsForm(this).ShowDialog();
		}

		/// <summary>
		///  事件：点击《退出应用》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exitSkinButton_Click(object sender, EventArgs e)
		{
			exit();
		}

		#endregion

		#region 工具按钮组 - 工程相关

		/// <summary>
		/// 事件：点击《灯具列表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightListSkinButton_Click(object sender, EventArgs e)
		{
			editLightList();
		}

		/// <summary>
		///  事件：点击《全局配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void globalSetSkinButton_Click(object sender, EventArgs e)
		{
			globalSetClick();
		}

		/// <summary>
		///  事件：点击《摇麦设置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ymSkinButton_Click(object sender, EventArgs e)
		{
			ymSetClick();
		}

		/// <summary>
		/// 事件：《工程更新》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void projectUpdateSkinButton_Click(object sender, EventArgs e)
		{
			projectUpdateClick();
		}

		#endregion

		//MARK：SkinMainForm工程相关 及 初始化辅助方法			
		#region 工程及场景相关：点击事件 及 辅助方法		

		/// <summary>
		/// 事件： 点击《新建工程》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newSkinButton_Click(object sender, EventArgs e)
		{
			newProjectClick();
		}

		/// <summary>
		/// 事件：点击《打开工程》按钮 
		/// --新建一个OpenForm，再在里面回调OpenProject()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openSkinButton_Click(object sender, EventArgs e)
		{
			openProjectClick();
		}
		
		/// <summary>
		///  事件：点击《调用其他场景》--备注：虽放在步数面板内，实际上应该属于这块的内容
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void useFrameSkinButton_Click(object sender, EventArgs e)
		{
			useFrameClick();
		}

		/// <summary>
		///  事件：点击《保存场景》（此操作可能耗时较久，故在方法体前后添加鼠标样式的变化）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frameSaveSkinButton_Click(object sender, EventArgs e)
		{
			saveFrameClick();
		}

		/// <summary>
		///  事件：点击《保存工程》（此操作可能耗时较久，故在方法体前后添加鼠标样式的变化）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveSkinButton_Click(object sender, EventArgs e)
		{
			saveProjectClick();
		}

		/// <summary>
		///事件：点击《导出工程》按钮：将当前保存好的内容，导出到项目目录下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exportSkinButton_Click(object sender, EventArgs e)
		{
			exportProjectClick();
		}
				
		/// <summary>
		/// 事件：点击《关闭工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void closeSkinButton_Click(object sender, EventArgs e)
		{
			closeProjectClick();
		}

		/// <summary>
		///  辅助方法：以传入值设置《保存工程》《导出工程》按钮是否可用
		/// </summary>
		protected override void enableSave(bool enable)
		{
			saveSkinButton.Enabled = enable;
			exportSkinButton.Enabled = enable;
			frameSaveSkinButton.Enabled = enable;
			closeSkinButton.Enabled = enable;
		}

		/// <summary>
		/// 辅助方法：ClearAllDate()最后一步，但需针对不同的MainForm子类来实现。
		/// MARK：SkinMainFormForm.ClearAllData()：子类中针对本Form清除数据
		/// </summary>
		protected override void clearAllData()
		{			
			base.clearAllData();

			//MARK＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋＋1111
			//单独针对本MainForm的代码: 
			// ①清空listView列表；
			// ②禁用步调节按钮组、隐藏所有通道、stepLabel设为0/0、选中灯具信息清空
			this.Text = softwareName;
			lightsSkinListView.Clear();
			stepSkinPanel.Enabled = false;
			hideAllTDPanels();
			showStepLabel(0, 0);
			editLightInfo(null);
			enableSingleMode(true);

			// 10.17 清空数据时，应该结束预览。
			endview();
		}

		/// <summary>
		///辅助方法：添加lightAst列表到主界面内存中,主要供 LightsForm以及OpenProject调用）
		/// --对比删除后，生成新的lightWrapperList；
		/// --lightListView也更新为最新的数据
		/// </summary>
		/// <param name="lightAstList2"></param>
		public override void AddLightAstList(IList<LightAst> lightAstList2)
		{
			// 0.先调用统一的操作，填充lightAstList和lightWrapperList
			base.AddLightAstList(lightAstList2);

			//下列为针对本Form的处理代码：listView更新为最新数据

			// 1.清空lightListView,重新填充新数据
			lightsSkinListView.Items.Clear();
			for (int i = 0; i < lightAstList2.Count; i++)
			{
				// 添加灯具数据到LightsListView中
				lightsSkinListView.Items.Add(new ListViewItem(
						lightAstList2[i].LightName + ":" + lightAstList2[i].LightType +
						"\n(" + lightAstList2[i].LightAddr + ")",
					lightLargeImageList.Images.ContainsKey(lightAstList2[i].LightPic) ? lightAstList2[i].LightPic : "灯光图.png"
				)
				{ Tag = lightAstList2[i].LightName + ":" + lightAstList2[i].LightType }
				);
			}

			// 2.最后处理通道显示：每次调用此方法后应该隐藏通道数据，避免误操作。
			hideAllTDPanels();
		}

		/// <summary>
		///  辅助方法：将所有全局配置相关的按钮（灯具列表、工程升级、全局设置、摇麦设置）Enabled设为传入bool值
		/// </summary>
		/// <param name="v"></param>
		protected override void enableGlobalSet(bool enable)
		{
			// 菜单栏几个工程相关按钮
			projectUpdateSkinButton.Enabled = enable;
			lightListSkinButton.Enabled = enable;
			globalSetSkinButton.Enabled = enable;
			ymSkinButton.Enabled = enable;
		}

		/// <summary>
		///  辅助方法：设定是否显示《 （调试区域的N个按钮）panel》
		/// </summary>
		/// <param name="visible"></param>
		protected override void showPlayPanel(bool visible)
		{
			playFlowLayoutPanel.Visible = visible;
		}	

		#endregion

		#region lightsListView相关事件及辅助方法

		/// <summary>
		/// 事件：改变选中的灯时进行的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsSkinListView_SelectedIndexChanged(object sender, EventArgs e)
		{			
			if (lightsSkinListView.SelectedIndices.Count > 0)
			{
				selectedIndex = lightsSkinListView.SelectedIndices[0];				
				generateLightData();
			}
		}
		
		/// <summary>
		///  辅助方法：通过LightAst，显示选中灯具信息
		/// </summary>
		protected override void editLightInfo(LightAst lightAst)
		{
			if (lightAst == null) {
				currentLightPictureBox.Image = null;
				lightNameLabel.Text = null;
				lightTypeLabel.Text = null;
				lightsAddrLabel.Text = null;
				return;
			}

			try
			{
				currentLightPictureBox.Image = Image.FromFile(savePath + @"\LightPic\" + lightAst.LightPic);
			}
			catch (Exception)
			{
				currentLightPictureBox.Image = global::LightController.Properties.Resources.灯光图;
			}
			lightNameLabel.Text = "灯具厂商：" + lightAst.LightName;
			lightTypeLabel.Text = "灯具型号：" + lightAst.LightType;
			lightsAddrLabel.Text = "灯具地址：" + lightAst.LightAddr;
			selectedLightName = lightAst.LightName + "-" + lightAst.LightType;
		}

		/// <summary>
		/// 辅助方法：初始化灯具数据。
		/// 0.先查看当前内存是否已有此数据 
		/// 1.若还未有，则取出相关的ini进行渲染
		/// 2.若内存内 或 数据库内已有相关数据，则使用这个数据。
		/// </summary>
		/// <param name="la"></param>
		protected override void enableStepPanel(bool enable)
		{
			stepSkinPanel.Enabled = enable;
		}

		/// <summary>
		/// 辅助方法: 确认选中灯具是否否同一种灯具：是则返回true,否则返回false。
		/// 验证方法：取出第一个选中灯具的名字，若后面的灯具的全名（Tag =lightName + ":" + lightType)与它不同，说明不是同种灯具。（只要一个不同即可判断）
		/// </summary>
		/// <returns></returns>
		private bool checkSameLights()
		{
			bool result = true;
			string firstTag = lightsSkinListView.SelectedItems[0].Tag.ToString();
			for (int i = 1; i < lightsSkinListView.SelectedItems.Count; i++) // 从第二个选中灯具开始比对
			{
				string tempTag = lightsSkinListView.SelectedItems[i].Tag.ToString();
				if (!firstTag.Equals(tempTag))
				{
					result = false;
					break;
				}
			}
			return result;
		}

		/// <summary>
		/// 辅助方法：通过传来的数值，生成通道列表的数据
		/// </summary>
		/// <param name="tongdaoList"></param>
		/// <param name="startNum"></param>
		protected override void showTDPanels(IList<TongdaoWrapper> tongdaoList, int startNum)
		{
			// 1.判断tongdaoList，为null或数量为0时：①隐藏所有通道；②退出此方法
			if (tongdaoList == null || tongdaoList.Count == 0)
			{
				hideAllTDPanels();
				return;
			}
			//2.将dataWrappers的内容渲染到起VScrollBar中
			else
			{
				isPainting = true;

				for (int i = 0; i < tongdaoList.Count; i++)
				{
					tdSkinTrackBars[i].ValueChanged -= new System.EventHandler(this.tdSkinTrackBars_ValueChanged);
					tdValueNumericUpDowns[i].ValueChanged -= new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);
					tdChangeModeSkinComboBoxes[i].SelectedIndexChanged -= new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);
					tdStepTimeNumericUpDowns[i].ValueChanged -= new EventHandler(this.tdStepTimeNumericUpDowns_ValueChanged);

					tdNoLabels[i].Text = "通道" + (startNum + i);
					tdNameLabels[i].Text = tongdaoList[i].TongdaoName;
					myToolTip.SetToolTip(tdNameLabels[i], tongdaoList[i].TongdaoName);
					tdSkinTrackBars[i].Value = tongdaoList[i].ScrollValue;
					tdValueNumericUpDowns[i].Text = tongdaoList[i].ScrollValue.ToString();
					tdChangeModeSkinComboBoxes[i].SelectedIndex = tongdaoList[i].ChangeMode;
					tdStepTimeNumericUpDowns[i].Text = tongdaoList[i].StepTime.ToString();
					tdTrueTimeLabels[i].Text = (float)tongdaoList[i].StepTime * eachStepTime / 1000 + "s";

					tdSkinTrackBars[i].ValueChanged += new System.EventHandler(this.tdSkinTrackBars_ValueChanged);
					tdValueNumericUpDowns[i].ValueChanged += new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);
					tdChangeModeSkinComboBoxes[i].SelectedIndexChanged += new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);
					tdStepTimeNumericUpDowns[i].ValueChanged += new EventHandler(this.tdStepTimeNumericUpDowns_ValueChanged);

					tdPanels[i].Show();
				}
				for (int i = tongdaoList.Count; i < 32; i++)
				{
					tdPanels[i].Hide();
				}

				isPainting = false;
			}
		}

		/// <summary>
		/// 辅助方法：隐藏所有tdPanels,因为所有panels为空了，则《统一调整框》enabled应设为false
		/// </summary>
		protected override void hideAllTDPanels()
		{
			isPainting = true;

			for (int i = 0; i < 32; i++)
			{
				tdPanels[i].Hide();
			}			

			isPainting = false;
		}

		#endregion

		//MARK：SkinMainForm灯具listView相关（右键菜单+位置等）
		#region  灯具listView相关（右键菜单+位置等）

		/// <summary>
		/// 事件：重新加载灯具图片
		///	-- 工程中添加的灯具，是忘了加图片的灯库文件，保存工程后其Pic属性是空的；
		///	-- 而在修改灯具后这个值不会主动更新，此功能可手动修复此问题。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshPicToolStripMenuItem_Click(object sender, EventArgs e)
		{
			HashSet<string> lightPathHashSet = new HashSet<string>();
			foreach (LightAst la in lightAstList)
			{
				lightPathHashSet.Add(la.LightPath);
			}

			Dictionary<string, string> lightDict = new Dictionary<string, string>();
			foreach (var lightPath in lightPathHashSet)
			{
				string picStr = IniFileAst_UTF8.ReadString(lightPath, "set", "pic", "灯光图.png");
				if (String.IsNullOrEmpty(picStr))
				{
					picStr = "灯光图.png";
				}
				lightDict.Add(lightPath, picStr);
			}

			for (int lightIndex = 0; lightIndex < lightAstList.Count; lightIndex++)
			{
				string tempPicStr = lightDict[lightAstList[lightIndex].LightPath];
				lightAstList[lightIndex].LightPic = tempPicStr;
				lightsSkinListView.Items[lightIndex].ImageKey = tempPicStr;
			}

		}

		// 这个别忘了
		// listView1.AllowDrop = true;
		// listView1.AutoArrange = false;
		private Point startPoint = Point.Empty;

		private double getVector(Point pt1, Point pt2) // 获取两点间的距离
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
		private void lightsSkinListView_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem[])))
				e.Effect = DragDropEffects.Move;
		}

		/// <summary>
		/// 事件：松开鼠标时发生（VS：拖动操作时发生）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsSkinListView_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem[])))
			{
				var items = e.Data.GetData(typeof(ListViewItem[])) as ListViewItem[];

				var pos = lightsSkinListView.PointToClient(new Point(e.X, e.Y));

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
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsSkinListView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				startPoint = e.Location;
		}

		/// <summary>
		/// 事件：listView鼠标移动
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsSkinListView_MouseMove(object sender, MouseEventArgs e)
		{
			if (lightsSkinListView.SelectedItems.Count == 0)
				return;

			if (e.Button == MouseButtons.Left)
			{
				var vector = getVector(startPoint, e.Location);
				if (vector < 10) return;

				var data = lightsSkinListView.SelectedItems.OfType<ListViewItem>().ToArray();

				lightsSkinListView.DoDragDrop(data, DragDropEffects.Move);
			}
		}

		/// <summary>
		/// 事件：点选《自动排列》与否
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void autoArrangeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//MessageBox.Show(autoArrangeToolStripMenuItem.Checked.ToString());
			isAutoArrange = autoArrangeToolStripMenuItem.Checked;
			lightsSkinListView.AllowDrop = !isAutoArrange;
			lightsSkinListView.AutoArrange = isAutoArrange;

			if (isAutoArrange)
			{
				enableSLArrange(false, false);
			}
			else
			{
				enableSLArrange(true, File.Exists(arrangeIniPath));
			}
		}

		/// <summary>
		/// 事件：点击《重新排列》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void arrangeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool tempAutoArrange = lightsSkinListView.AutoArrange;
			lightsSkinListView.AutoArrange = true;
			lightsSkinListView.AutoArrange = tempAutoArrange;
			lightsSkinListView.Update();
		}

		/// <summary>
		/// 事件：点击《保存灯具位置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveArrangeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//1.先判断是否在自动排列下
			if (isAutoArrange)
			{
				MessageBox.Show("在自动排列模式下，无法保存灯具位置，请取消勾选后重新保存。");
				return;
			}

			// 2.判断当前是否已打开工程(arrangeIniPath不为空）
			if (String.IsNullOrEmpty(arrangeIniPath))
			{
				MessageBox.Show("当前尚未新建或打开工程，无法保存灯具位置。");
				return;
			}

			// 3.判断灯具数量是否为空
			if (lightAstList == null || lightAstList.Count == 0)
			{
				MessageBox.Show("当前工程尚无灯具，无法保存灯具位置，请添加灯具后重新保存。");
				return;
			}

			// 4.保存操作
			IniFileAst iniFileAst = new IniFileAst(arrangeIniPath);
			iniFileAst.WriteInt("Common", "Count", lightsSkinListView.Items.Count);
			for (int i = 0; i < lightsSkinListView.Items.Count; i++)
			{
				iniFileAst.WriteInt("Position", i + "X", lightsSkinListView.Items[i].Position.X);
				iniFileAst.WriteInt("Position", i + "Y", lightsSkinListView.Items[i].Position.Y);
			}
			enableSLArrange(true, File.Exists(arrangeIniPath));

			MessageBox.Show("灯具位置保存成功。");
		}

		/// <summary>
		///  事件：点击《读取灯具位置》：
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// 1.先验证ini文件是否存在
			if (!File.Exists(arrangeIniPath))
			{
				MessageBox.Show("未找到灯具位置文件，无法读取。");
				return;
			}

			//2.验证灯具数目是否一致
			IniFileAst iniFileAst = new IniFileAst(arrangeIniPath);
			int lightCount = iniFileAst.ReadInt("Common", "Count", 0);
			if (lightCount == 0)
			{
				MessageBox.Show("灯具位置文件的灯具数量为0，此文件无实际效果。");
				return;
			}

			//3. 验证灯具数量是否一致
			if (lightCount != lightsSkinListView.Items.Count)
			{
				MessageBox.Show("灯具位置文件的灯具数量与当前工程的灯具数量不匹配，无法读取位置。");
				return;
			}

			// 4.开始读取并绘制		
			//MARK : SkinMainForm 特别奇怪的一个地方，在选择自动排列再去掉自动排列后，必须要先设一个不同的position，才能让读取到的position真正给到items[i].Position?
			lightsSkinListView.BeginUpdate();
			for (int i = 0; i < lightsSkinListView.Items.Count; i++)
			{
				//Console.WriteLine(lightsSkinListView.Items[i].Position);
				int tempX = iniFileAst.ReadInt("Position", i + "X", 0);
				int tempY = iniFileAst.ReadInt("Position", i + "Y", 0);
				lightsSkinListView.Items[i].Position = new Point(0, 0);
				lightsSkinListView.Items[i].Position = new Point(tempX, tempY);
			}

			lightsSkinListView.EndUpdate();
			MessageBox.Show("灯具位置读取成功。");
		}

		/// <summary>
		/// 辅助方法： 是否使能重新加载灯具图片
		/// </summary>
		/// <param name="enable"></param>
		protected override void enableRefreshPic(bool enable)
		{
			refreshPicToolStripMenuItem.Enabled = enable;
		}

		/// <summary>
		///  辅助方法：《保存|读取灯具位置》按钮是否可用
		/// </summary>
		/// <param name="enable"></param>
		protected override void enableSLArrange(bool enableSave, bool enableLoad)
		{
			saveArrangeToolStripMenuItem.Enabled = enableSave;
			loadArrangeToolStripMenuItem.Enabled = enableLoad;
		}

		#endregion

		#region 几个显示或隐藏面板的菜单项

		private void hideMenuPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			menuSkinPanel.Visible = !menuSkinPanel.Visible;
			hideMenuPanelToolStripMenuItem.Text = menuSkinPanel.Visible ? "隐藏主菜单面板" : "显示主菜单面板";
			hideMenuPanelToolStripMenuItem2.Text = menuSkinPanel.Visible ? "隐藏主菜单面板" : "显示主菜单面板";
		}

		private void hideProjectPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			projectSkinPanel.Visible = !projectSkinPanel.Visible;
			hideProjectPanelToolStripMenuItem.Text = projectSkinPanel.Visible ? "隐藏工程面板" : "显示工程面板";
			hideProjectPanelToolStripMenuItem2.Text = projectSkinPanel.Visible ? "隐藏工程面板" : "显示工程面板";
		}

		private void hideAstPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			astSkinPanel.Visible = !astSkinPanel.Visible;
			hideAstPanelToolStripMenuItem.Text = astSkinPanel.Visible ? "隐藏辅助面板" : "显示辅助面板";
			hideAstPanelToolStripMenuItem2.Text = astSkinPanel.Visible ? "隐藏辅助面板" : "显示辅助面板";
		}

		private void hidePlayPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			playPanel.Visible = !playPanel.Visible;
			hidePlayPanelToolStripMenuItem.Text = playPanel.Visible ? "隐藏调试面板" : "显示调试面板";
			hidePlayPanelToolStripMenuItem2.Text = playPanel.Visible ? "隐藏调试面板" : "显示调试面板";
		}

		#endregion

		//MARK：SkinMainForm步数相关的按钮及辅助方法起点
		#region  stepPanel相关的方法

		/// <summary>
		/// 事件：更改《选择场景》选项后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frameSkinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//11.13 若未初始化，直接return；
			if (!isInit)
			{
				return;
			}
			SetNotice("正在切换场景...");

			// 只要更改了场景，直接结束预览
			endview();

			DialogResult dr = MessageBox.Show("切换场景前，是否保存之前场景(" + AllFrameList[frame] + ")？",
				"保存场景?",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Question);
			if (dr == DialogResult.OK)
			{
				setBusy(true);
				saveFrame();
				setBusy(false);
			}

			frame = frameSkinComboBox.SelectedIndex;
			if (lightAstList != null && lightAstList.Count > 0)
			{
				changeFrameMode();
			}
			SetNotice("成功切换场景");
		}

		/// <summary>
		/// 事件：更改《选择模式》选项后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void modeSkinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			mode = modeSkinComboBox.SelectedIndex;

			//11.13 若未初始化，直接return；
			if (!isInit)
			{
				return;
			}
			
			// 若模式为声控模式mode=1
			// 1.改变几个label的Text; 
			// 2.改变跳变渐变-->是否声控；
			// 3.所有步时间值的调节，改为enabled=false						
			if (mode == 1)
			{
				for (int i = 0; i < FrameCount; i++)
				{
					this.tdChangeModeSkinComboBoxes[i].Items.Clear();
					this.tdChangeModeSkinComboBoxes[i].Items.AddRange(new object[] { "屏蔽", "跳变" });
					this.tdStepTimeNumericUpDowns[i].Hide();
					this.tdTrueTimeLabels[i].Hide();
				}
				unifyChangeModeSkinButton.Text = "统一声控";
				unifyChangeModeSkinComboBox.Items.Clear();
				unifyChangeModeSkinComboBox.Items.AddRange(new object[] { "屏蔽", "跳变" });
				unifyChangeModeSkinComboBox.SelectedIndex = 0;

				unifyStepTimeNumericUpDown.Hide();
				unifyStepTimeSkinButton.Text = "修改此音频场景全局设置";
				unifyStepTimeSkinButton.Size = new System.Drawing.Size(210, 27);

				thirdLabel1.Hide();
				thirdLabel2.Hide();
				thirdLabel3.Hide();
			}
			else //mode=0，常规模式
			{
				for (int i = 0; i < FrameCount; i++)
				{
					this.tdChangeModeSkinComboBoxes[i].Items.Clear();
					this.tdChangeModeSkinComboBoxes[i].Items.AddRange(new object[] { "跳变", "渐变", "屏蔽" });
					this.tdStepTimeNumericUpDowns[i].Show();
					this.tdTrueTimeLabels[i].Show();
				}
				unifyChangeModeSkinButton.Text = "统一跳渐变";
				unifyChangeModeSkinComboBox.Items.Clear();
				unifyChangeModeSkinComboBox.Items.AddRange(new object[] { "跳变", "渐变", "屏蔽" });
				unifyChangeModeSkinComboBox.SelectedIndex = 0;

				unifyStepTimeNumericUpDown.Show();
				unifyStepTimeSkinButton.Text = "统一步时间";
				unifyStepTimeSkinButton.Size = new System.Drawing.Size(111, 27);

				thirdLabel1.Show();
				thirdLabel2.Show();
				thirdLabel3.Show();
			}

			if (lightAstList != null && lightAstList.Count > 0)
			{
				changeFrameMode();
			}
		}

		/// <summary>
		/// 事件：点击切换《多灯模式|单灯模式》。
		/// 一.多灯模式：
		///		0.至少选择两个灯具，才能使用多灯模式
		///		1.判断所有选中的灯，是否同类型；若选中的不是同类型的灯无法进入此模式(直接return)
		///		2.若是同类型的，应选择其中之一作为编组的组长（其他灯直接使用此灯的数据 ：先复制组长的数据，然后后台直接粘贴到其余灯具上面）
		///		3.之后每次编辑灯具，都是编辑组内的所有数据 （包括添加步、删除步，步调节等）
		///		4.下面的调试按钮中"单灯单步"-》“多灯单步”；
		///		5.若是选择其他模式或者场景，应自动恢复《单灯调节》模式 
		/// 二.单灯模式（与单灯刚好是反操作）：	
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiLightSkinButton_Click(object sender, EventArgs e)
		{
			// 进入多灯模式
			if (!isMultiMode)
			{
				if (lightsSkinListView.SelectedIndices.Count < 2)
				{
					MessageBox.Show("请选择至少两个(同型)灯具，否则无法使用多灯模式。");
					return;
				}
				if (!checkSameLights())
				{
					MessageBox.Show("选中的灯具并非都是同一类型的，无法进行编组；请再次选择后重试。");
					return;
				}
				selectedIndices = new List<int>();
				foreach (int item in lightsSkinListView.SelectedIndices)
				{
					selectedIndices.Add(item);
				}
				new MultiLightForm(this, isCopyAll, lightAstList, selectedIndices).ShowDialog();
			}
			// 退出多灯模式
			else
			{
				lightsAddrLabel.Text = "灯具地址：" + lightAstList[selectedIndex].LightAddr;
				for (int lightIndex = 0; lightIndex < lightWrapperList.Count; lightIndex++)
				{
					lightsSkinListView.Items[lightIndex].BackColor = Color.White;
				}
				enableSingleMode(true);
			}
		}

		/// <summary>
		/// 事件：点击切换《进入同步|退出同步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void syncSkinButton_Click(object sender, EventArgs e)
		{
			// 如果当前已经是同步模式，则退出同步模式，这比较简单，不需要进行任何比较，直接操作即可。
			if (isSyncMode)
			{
				isSyncMode = false;
				syncSkinButton.Text = "进入同步";
				return;
			}

			// 异步时，要切换到同步模式，需要先进行检查。
			if (!CheckAllSameStepCounts())
			{
				MessageBox.Show("当前场景所有灯具步数不一致，无法进入同步模式。");
				return;
			}

			isSyncMode = true;
			syncSkinButton.Text = "退出同步";
		}

		/// <summary>
		///  事件：点击《上一步》
		///  先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backStepSkinButton_Click(object sender, EventArgs e)
		{
			backStepClick();
		}
		
		/// <summary>
		///  事件：点击《下一步》
		///  先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nextStepSkinButton_Click(object sender, EventArgs e)
		{
			nextStepClick();
		}

		/// <summary>
		/// 事件：点击《跳转步》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chooseStepSkinButton_Click(object sender, EventArgs e)
		{
			int step = Decimal.ToInt16(chooseStepNumericUpDown.Value);
			if (step != 0)
			{
				chooseStep(step);
			}
		}

		/// <summary>
		/// 事件：点击《插入步》
		/// --前插和后插都调用同一个方法(触发键的Name决定)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void insertStepButton_Click(object sender, EventArgs e)
		{			
			bool insertBefore = ((Button)sender).Name.Equals("insertBeforeSkinButton"); // 插入的方式：前插(true）还是后插（false)			
			insertStepClick(insertBefore);
		}

		/// <summary>
		/// 事件：点击《追加步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addStepSkinButton_Click(object sender, EventArgs e)
		{
			addStepClick();
		}
		
		/// <summary>
		///  事件：点击《删除步》
		///  1.获取当前步，当前步对应的stepIndex
		///  2.通过stepIndex，DeleteStep(index);
		///  3.获取新步(step删除后会自动生成新的)，并重新渲染stepLabel和vScrollBars
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteStepSkinButton_Click(object sender, EventArgs e)
		{
			deleteStepClick();
		}
		
		/// <summary>
		/// 事件：点击《复制步》
		/// 1.从项目中选择当前灯的当前步，(若当前步为空，则无法复制），把它赋给tempStep数据。
		/// 2.若复制成功，则《粘贴步》按钮可用
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void copyStepSkinButton_Click(object sender, EventArgs e)
		{
			if (getCurrentStepWrapper() == null)
			{
				MessageBox.Show("当前步数据为空，无法复制");
			}
			else
			{
				tempStep = getCurrentStepWrapper();
				pasteStepSkinButton.Enabled = true;
			}
		}

		/// <summary>
		/// 事件：点击《粘贴步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pasteStepSkinButton_Click(object sender, EventArgs e)
		{
			pasteStepClick();
		}
		
		/// <summary>		
		///  事件：点击《复制多步》：弹出类似于保存素材的form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiCopySkinButton_Click(object sender, EventArgs e)
		{
			multiCopyClick();
		}

		/// <summary>	
		///  事件：点击《粘贴多步》：弹出类似于保存素材的form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiPasteSkinButton_Click(object sender, EventArgs e)
		{
			multiPasteClick();
		}

		/// <summary>
		///  事件：点击《保存素材》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveMaterialSkinButton_Click(object sender, EventArgs e)
		{
			saveMaterial();
		}

		/// <summary>
		///  事件：点击《使用素材》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void useMaterialSkinButton_Click(object sender, EventArgs e)
		{
			useMaterial();
		}

		/// <summary>
		///  9.16 辅助方法：进入《多灯模式》
		/// </summary>
		/// <param name="groupSelectedIndex"></param>
		public override void EnterMultiMode(int groupSelectedIndex, bool isCopyAll)
		{
			// 基类中统一的处理
			base.EnterMultiMode(groupSelectedIndex, isCopyAll);

			// 以下为单独针对本Form的方法：			
			lightsAddrLabel.Text = "灯具地址列表：";
			foreach (int lightIndex in selectedIndices)
			{
				if (lightIndex == selectedIndex)
				{
					lightsAddrLabel.Text += "(" + lightAstList[lightIndex].LightAddr + ") ";
					lightsSkinListView.Items[lightIndex].BackColor = Color.LightSkyBlue;
				}
				else
				{
					lightsAddrLabel.Text += lightAstList[lightIndex].LightAddr + " ";
					lightsSkinListView.Items[lightIndex].BackColor = Color.SkyBlue;
				}
			}
			enableSingleMode(false);
		}

		/// <summary>
		/// 辅助方法：退出多灯模式或单灯模式后的相关操作
		/// </summary>
		/// <param name="isSingleMode"></param>
		private void enableSingleMode(bool isSingleMode)
		{
			isMultiMode = !isSingleMode;

			lightsSkinListView.Enabled = isSingleMode;
			frameSkinComboBox.Enabled = isSingleMode;
			modeSkinComboBox.Enabled = isSingleMode;
			useFrameSkinButton.Enabled = isSingleMode;

			multiLightSkinButton.Text = isSingleMode ? "多灯模式" : "单灯模式";
		}

		/// <summary>
		/// TODO：辅助方法：重置syncMode的相关属性，ChangeFrameMode、ClearAllData()、更改灯具列表后等？应该进行处理。
		/// </summary>
		public override void ResetSyncMode()
		{
			syncSkinButton.Text = "进入同步";
			isSyncMode = false;
		}	

		/// <summary>
		///  辅助方法：用来显示stepLabel-->当前步/总步数
		/// 7.2 +隐藏《删除步》按钮
		/// </summary>
		/// <param name="currentStep"></param>
		/// <param name="totalStep"></param>
		protected override void showStepLabel(int currentStep, int totalStep)
		{
			// 1. 设label的Text值					   
			stepLabel.Text = MathAst.GetFourWidthNumStr(currentStep, true) + "/" + MathAst.GetFourWidthNumStr(totalStep, false);

			// 2.1 设定《删除步》按钮是否可用
			deleteStepSkinButton.Enabled = totalStep != 0;

			// 2.2 设定《追加步》、《前插入步》《后插入步》按钮是否可用			
			bool insertEnabled = totalStep < MAX_STEP;
			addStepSkinButton.Enabled = insertEnabled;
			insertAfterSkinButton.Enabled = insertEnabled;
			insertBeforeSkinButton.Enabled = insertEnabled && currentStep > 0;

			// 2.3 设定《上一步》《下一步》是否可用
			// -- 7.19修改为循环使用步数：
			backStepSkinButton.Enabled = totalStep > 1;
			nextStepSkinButton.Enabled = totalStep > 1;

			//2.4 设定《复制步》是否可用
			copyStepSkinButton.Enabled = currentStep > 0;
			pasteStepSkinButton.Enabled = currentStep > 0 && tempStep != null;

			multiCopySkinButton.Enabled = currentStep > 0;
			multiPasteSkinButton.Enabled = TempMaterialAst != null && TempMaterialAst.Mode == mode;

			// 3.设定统一调整区是否可用
			zeroSkinButton.Enabled = totalStep != 0;
			initSkinButton.Enabled = totalStep != 0;
			multiSkinButton.Enabled = totalStep != 0;
			unifyValueSkinButton.Enabled = totalStep != 0;
			unifyChangeModeSkinButton.Enabled = totalStep != 0;
			unifyStepTimeSkinButton.Enabled = (totalStep != 0) || (mode == 1);
			unifyValueNumericUpDown.Enabled = totalStep != 0;
			unifyValueTrackBar.Enabled = totalStep != 0;
			unifyChangeModeSkinComboBox.Enabled = totalStep != 0;
			unifyStepTimeNumericUpDown.Enabled = totalStep != 0;

			// 10.9 添加一个选择步数的框
			chooseStepNumericUpDown.Enabled = totalStep != 0;
			chooseStepSkinButton.Enabled = totalStep != 0;
			chooseStepNumericUpDown.Maximum = totalStep;
			if (totalStep != 0)
			{
				chooseStepNumericUpDown.Minimum = 1;
			}
		}

		#endregion

		//MARK：SkinMainForm：tdPanels内部数值调整及辅助方法
		#region tdPanels相关：内部数值的调整事件及辅助方法	

		/// <summary>
		/// 事件:鼠标进入tdTrackBar时，把焦点切换到其对应的tdValueNumericUpDown中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdTrackBars_MouseEnter(object sender, EventArgs e)
		{
			int tdIndex = MathAst.GetIndexNum(((SkinTrackBar)sender).Name, -1);
			tdValueNumericUpDowns[tdIndex].Select();
		}

		/// <summary>
		///  事件：鼠标滚动时，通道值每次只变动一个Increment值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdSkinTrackBars_MouseWheel(object sender, MouseEventArgs e)
		{
			//Console.WriteLine("tdSkinTrackBars_MouseWheel");
			int tdIndex = MathAst.GetIndexNum(((SkinTrackBar)sender).Name, -1);
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				//获取或设置是否应将此事件转发到控件的父容器。
				// public bool Handled { get; set; } ==> 如果鼠标事件应转到父控件，则为 true；否则为 false。
				// Dickov: 实际上就是当Handled设为true时，不再触发本控件的默认相关操作(即屏蔽滚动事件)
				hme.Handled = true;
			}

			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = tdSkinTrackBars[tdIndex].Value + tdSkinTrackBars[tdIndex].SmallChange;
				if (dd <= tdSkinTrackBars[tdIndex].Maximum)
				{
					tdSkinTrackBars[tdIndex].Value = Decimal.ToInt16(dd);
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = tdSkinTrackBars[tdIndex].Value - tdSkinTrackBars[tdIndex].SmallChange;
				if (dd >= tdSkinTrackBars[tdIndex].Minimum)
				{
					tdSkinTrackBars[tdIndex].Value = Decimal.ToInt16(dd);
				}
			}
		}

		/// <summary>
		///  事件：TrackBar滚轴值改变时的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdSkinTrackBars_ValueChanged(object sender, EventArgs e)
		{
			//Console.WriteLine("tdSkinTrackBars_ValueChanged");
			// 1.先找出对应tdSkinTrackBars的index 
			int tongdaoIndex = MathAst.GetIndexNum(((SkinTrackBar)sender).Name, -1);
			int tdValue = tdSkinTrackBars[tongdaoIndex].Value;

			//2.把滚动条的值赋给tdValueNumericUpDowns
			// 8.28	：在修改时取消其监听事件，修改成功恢复监听；这样就能避免重复触发监听事件
			tdValueNumericUpDowns[tongdaoIndex].ValueChanged -= new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);
			tdValueNumericUpDowns[tongdaoIndex].Value = tdValue;
			tdValueNumericUpDowns[tongdaoIndex].ValueChanged += new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);

			//3.取出recentStep,使用取出的index，给stepWrapper.TongdaoList[index]赋值；并检查是否实时生成数据进行操作
			changeScrollValue(tongdaoIndex, tdValue);
		}

		/// <summary>
		/// 事件：调节或输入numericUpDown的值后，1.调节通道值 2.调节tongdaoWrapper的相关值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdValueNumericUpDowns_ValueChanged(object sender, EventArgs e)
		{
			//Console.WriteLine("tdValueNumericUpDowns_ValueChanged");
			// 1. 找出对应的index
			int tongdaoIndex = MathAst.GetIndexNum(((NumericUpDown)sender).Name, -1);
			int tdValue = Decimal.ToInt16(tdValueNumericUpDowns[tongdaoIndex].Value);

			// 2.调整相应的vScrollBar的数值；
			// 8.28 ：在修改时取消其监听事件，修改成功恢复监听；这样就能避免重复触发监听事件
			tdSkinTrackBars[tongdaoIndex].ValueChanged -= new System.EventHandler(this.tdSkinTrackBars_ValueChanged);
			tdSkinTrackBars[tongdaoIndex].Value = tdValue;
			tdSkinTrackBars[tongdaoIndex].ValueChanged += new System.EventHandler(this.tdSkinTrackBars_ValueChanged);

			//3.取出recentStep,使用取出的index，给stepWrapper.TongdaoList[index]赋值；并检查是否实时生成数据进行操作
			changeScrollValue(tongdaoIndex, tdValue);
		}

		/// <summary>
		/// 事件：鼠标进入通道值输入框时，切换焦点;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdValueNumericUpDowns_MouseEnter(object sender, EventArgs e)
		{
			//Console.WriteLine("tdValueNumericUpDowns_MouseEnter");
			int tdIndex = MathAst.GetIndexNum(((NumericUpDown)sender).Name, -1);
			tdValueNumericUpDowns[tdIndex].Select();
		}

		/// <summary>
		///  事件：鼠标滚动时，通道值每次只变动一个Increment值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdValueNumericUpDowns_MouseWheel(object sender, MouseEventArgs e)
		{
			//Console.WriteLine("tdValueNumericUpDowns_MouseWheel");
			int tdIndex = MathAst.GetIndexNum(((NumericUpDown)sender).Name, -1);
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				//获取或设置是否应将此事件转发到控件的父容器。
				// public bool Handled { get; set; } ==> 如果鼠标事件应转到父控件，则为 true；否则为 false。
				// Dickov: 实际上就是当Handled设为true时，不再触发本控件的默认相关操作(即屏蔽滚动事件)
				hme.Handled = true;
			}
			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = tdValueNumericUpDowns[tdIndex].Value + tdValueNumericUpDowns[tdIndex].Increment;
				if (dd <= tdValueNumericUpDowns[tdIndex].Maximum)
				{
					tdValueNumericUpDowns[tdIndex].Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = tdValueNumericUpDowns[tdIndex].Value - tdValueNumericUpDowns[tdIndex].Increment;
				if (dd >= tdValueNumericUpDowns[tdIndex].Minimum)
				{
					tdValueNumericUpDowns[tdIndex].Value = dd;
				}
			}
		}

		/// <summary>
		///  事件：每个通道对应的变化模式下拉框，值改变后，对应的tongdaoWrapper也应该设置参数 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdChangeModeSkinComboBoxes_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 1.先找出对应changeModeComboBoxes的index
			int tdIndex = MathAst.GetIndexNum(((ComboBox)sender).Name, -1);

			//2.取出recentStep，这样就能取出一个步数，使用取出的index，给stepWrapper.TongdaoList[index]赋值
			StepWrapper step = getCurrentStepWrapper();
			int changeMode = tdChangeModeSkinComboBoxes[tdIndex].SelectedIndex;
			step.TongdaoList[tdIndex].ChangeMode = tdChangeModeSkinComboBoxes[tdIndex].SelectedIndex;

			//3.多灯模式下，需要把调整复制到各个灯具去
			if (isMultiMode) {
				copyValueToAll(tdIndex, WHERE.CHANGE_MODE, changeMode);
			}						
		}
		
		/// <summary>
		/// 事件：鼠标进入步时间输入框时，切换焦点;
		/// 注意：用MouseEnter事件，而非MouseHover事件;这样才会无延时响应
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdStepTimeNumericUpDowns_MouseEnter(object sender, EventArgs e)
		{
			int tdIndex = MathAst.GetIndexNum(((NumericUpDown)sender).Name, -1);
			tdStepTimeNumericUpDowns[tdIndex].Select();
		}

		/// <summary>
		///  事件：鼠标滚动时，步时间值每次只变动一个Increment值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdStepTimeNumericUpDowns_MouseWheel(object sender, MouseEventArgs e)
		{
			int tdIndex = MathAst.GetIndexNum(((NumericUpDown)sender).Name, -1);
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			if (e.Delta > 0)
			{
				decimal dd = tdStepTimeNumericUpDowns[tdIndex].Value + tdStepTimeNumericUpDowns[tdIndex].Increment;
				if (dd <= tdStepTimeNumericUpDowns[tdIndex].Maximum)
				{
					tdStepTimeNumericUpDowns[tdIndex].Value = dd;
				}
			}
			else if (e.Delta < 0)
			{
				decimal dd = tdStepTimeNumericUpDowns[tdIndex].Value - tdStepTimeNumericUpDowns[tdIndex].Increment;
				if (dd >= tdStepTimeNumericUpDowns[tdIndex].Minimum)
				{
					tdStepTimeNumericUpDowns[tdIndex].Value = dd;
				}
			}
		}

		/// <summary>
		/// 事件： tdStepTimeNumericUpDown值变化时,修改内存中相应Step的tongdaoList的stepTime值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdStepTimeNumericUpDowns_ValueChanged(object sender, EventArgs e)
		{
			// 1.先找出对应stepNumericUpDowns的index（这个比较麻烦，因为其NumericUpDown的序号是从33开始的 即： name33 = names[0] =>addNum = -33）
			int tdIndex = MathAst.GetIndexNum(((NumericUpDown)sender).Name, -1);

			//2.取出recentStep，这样就能取出一个步数，使用取出的index，给stepWrapper.TongdaoList[index]赋值
			StepWrapper step = getCurrentStepWrapper();
			int stepTime = Decimal.ToInt32(tdStepTimeNumericUpDowns[tdIndex].Value);
			step.TongdaoList[tdIndex].StepTime = stepTime;
			this.tdTrueTimeLabels[tdIndex].Text = (float)step.TongdaoList[tdIndex].StepTime * eachStepTime / 1000 + "s";

			if (isMultiMode) {
				copyValueToAll(tdIndex, WHERE.STEP_TIME, stepTime);
			}

		}

		#region  因为使用滚动方法，故需要这些方法，NewMainForm中因为使用从左到右排序tdPanels，可以不用这些方法
		
		/// <summary>
		///   事件：tdSkinFlowLayoutPanel的paint事件
		///  </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdSkinFlowLayoutPanel_Paint(object sender, PaintEventArgs e)
		{
			if (!isPainting)
			{
				repaintTDPanels();
			}
		}

		/// <summary>
		///  辅助方法：重绘tdPanels
		/// </summary>
		private void repaintTDPanels()
		{
			// 1. j是tdPanels中在第一列的panel的数量，
			int j = 0;
			foreach (var tdPanel in tdPanels)
			{
				if (tdPanel.Visible && tdPanel.Location.X == 3)
				{
					j++;
				}
			}
			showLabelPanels(j);

			// 2. 设置滚动条的位置
			labelFlowLayoutPanel.AutoScrollPosition = new Point(0, -tdSkinFlowLayoutPanel.AutoScrollPosition.Y);
		}

		/// <summary>
		///  辅助方法：通过实时计算tdPanels占用的行数，来显示相同行数的labelPanels
		/// </summary>
		/// <param name="j"></param>
		private void showLabelPanels(int j)
		{
			switch (j)
			{
				case 0:
					labelPanel1.Hide();
					labelPanel2.Hide();
					labelPanel3.Hide();
					break;
				case 1:
					labelPanel1.Show();
					labelPanel2.Hide();
					labelPanel3.Hide();
					break;
				case 2:
					labelPanel1.Show();
					labelPanel2.Show();
					labelPanel3.Hide();
					break;
				case 3:
					labelPanel1.Show();
					labelPanel2.Show();
					labelPanel3.Show();
					break;
			}
		}

		/// <summary>
		///  事件：拦截labelFlowLayoutPanel的鼠标滚动。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void labelFlowLayoutPanel_MouseWheel(object sender, MouseEventArgs e)
		{
			Point oldPoint = labelFlowLayoutPanel.AutoScrollPosition;
			labelFlowLayoutPanel.AutoScrollPosition = oldPoint;
		}

		#endregion

		#endregion

		//MARK：SkinMainForm统一调整框各事件处理
		#region 统一调整框的组件及事件绑定

		/// <summary>
		/// 事件：《统一设置步时间numericUpDown》的鼠标滚动事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonStepTimeNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			if (e.Delta > 0)
			{
				decimal dd = unifyStepTimeNumericUpDown.Value + unifyStepTimeNumericUpDown.Increment;
				if (dd <= unifyStepTimeNumericUpDown.Maximum)
				{
					unifyStepTimeNumericUpDown.Value = dd;
				}
			}
			else if (e.Delta < 0)
			{
				decimal dd = unifyStepTimeNumericUpDown.Value - unifyStepTimeNumericUpDown.Increment;
				if (dd >= unifyStepTimeNumericUpDown.Minimum)
				{
					unifyStepTimeNumericUpDown.Value = dd;
				}
			}
		}

		/// <summary>
		///  事件：《统一设置步时间numericUpDown》的鼠标进入区域获取焦点
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonStepTimeNumericUpDown_MouseEnter(object sender, EventArgs e)
		{
			unifyStepTimeNumericUpDown.Select();
		}

		/// <summary>
		///  事件：《统一设置通道值numericUpDown》的鼠标滚动事件（只+/-1）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			if (e.Delta > 0)
			{
				decimal dd = unifyValueNumericUpDown.Value + unifyValueNumericUpDown.Increment;
				if (dd <= unifyValueNumericUpDown.Maximum)
				{
					unifyValueNumericUpDown.Value = dd;
				}
			}
			else if (e.Delta < 0)
			{
				decimal dd = unifyValueNumericUpDown.Value - unifyValueNumericUpDown.Increment;
				if (dd >= unifyValueNumericUpDown.Minimum)
				{
					unifyValueNumericUpDown.Value = dd;
				}
			}
		}

		/// <summary>
		///  事件：《统一设置通道值numericUpDown》的鼠标进入区域获取焦点
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueNumericUpDown_MouseEnter(object sender, EventArgs e)
		{
			unifyValueNumericUpDown.Select();
		}

		/// <summary>		
		/// 事件: 《统一设置通道值trackBar》鼠标滚动事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueTrackBar_MouseWheel(object sender, MouseEventArgs e)
		{
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				//获取或设置是否应将此事件转发到控件的父容器。
				// public bool Handled { get; set; } ==> 如果鼠标事件应转到父控件，则为 true；否则为 false。
				// Dickov: 实际上就是当Handled设为true时，不再触发本控件的默认相关操作(即屏蔽滚动事件)
				hme.Handled = true;
			}
			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = unifyValueTrackBar.Value + unifyValueTrackBar.SmallChange;
				if (dd <= unifyValueTrackBar.Maximum)
				{
					unifyValueTrackBar.Value = Decimal.ToInt16(dd);
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = unifyValueTrackBar.Value - unifyValueTrackBar.SmallChange;
				if (dd >= unifyValueTrackBar.Minimum)
				{
					unifyValueTrackBar.Value = Decimal.ToInt16(dd);
				}
			}
		}

		/// <summary>
		/// 事件: 《统一设置通道值trackBar》鼠标进入区域事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueTrackBar_MouseEnter(object sender, EventArgs e)
		{
			unifyValueNumericUpDown.Select();
		}

		/// <summary>
		///  事件：《统一设置通道值trackBar》值变动事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueTrackBar_ValuiChanged(object sender, EventArgs e)
		{
			unifyValueNumericUpDown.Value = unifyValueTrackBar.Value;
		}

		/// <summary>
		///  事件：《统一设置通道值numericUpDown》值变动事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			unifyValueTrackBar.Value = Decimal.ToInt16(unifyValueNumericUpDown.Value);
		}	

		/// <summary>
		/// 事件：点击《全部归零》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zeroSkinButton_Click(object sender, EventArgs e)
		{
			zeroButtonClick();
		}

		/// <summary>
		///  事件：点击《设为初值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void initSkinButton_Click(object sender, EventArgs e)
		{
			initButtonClick();
		}

		/// <summary>
		/// 事件：点击《多步调节》按钮
		/// 多步调整，传入当前灯的LightWrapper，在里面回调setMultiStepValues以调节相关的步数的值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiSkinButton_Click(object sender, EventArgs e)
		{
			multiButtonClick();
		}

		/// <summary>
		/// 事件：点击《统一通道值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueSkinButton_Click(object sender, EventArgs e)
		{
			StepWrapper currentStep = getCurrentStepWrapper();
			if (currentStep == null || currentStep.TongdaoList == null || currentStep.TongdaoList.Count == 0) {
				MessageBox.Show("请先选中任意步数，才能进行统一调整！");
				SetNotice("请先选中任意步数，才能进行统一调整！");
				return;
			}

			int commonValue = Decimal.ToInt16(unifyValueNumericUpDown.Value);
			for (int i = 0; i < currentStep.TongdaoList.Count; i++)
			{
				getCurrentStepWrapper().TongdaoList[i].ScrollValue = commonValue;
			}

			if (isMultiMode)
			{
				copyUnifyValueToAll(getCurrentStep(), WHERE.SCROLL_VALUE, commonValue);
			}
			RefreshStep();
		}

		/// <summary>
		/// 事件：点击《统一跳渐变》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonChangeModeSkinButton_Click(object sender, EventArgs e)
		{
			StepWrapper currentStep = getCurrentStepWrapper();
			if (currentStep == null || currentStep.TongdaoList == null || currentStep.TongdaoList.Count == 0)
			{
				MessageBox.Show("请先选中任意步数，才能进行统一调整！");
				SetNotice("请先选中任意步数，才能进行统一调整！");
				return;
			}

			int commonChangeMode = unifyChangeModeSkinComboBox.SelectedIndex;

			for (int i = 0; i < currentStep.TongdaoList.Count; i++)
			{
				getCurrentStepWrapper().TongdaoList[i].ChangeMode = commonChangeMode;
			}
			if (isMultiMode)
			{
				copyUnifyValueToAll(getCurrentStep(), WHERE.CHANGE_MODE, commonChangeMode);
			}
			RefreshStep();
		}

		/// <summary>
		/// 事件：点击《统一步时间》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonStepTimeSkinButton_Click(object sender, EventArgs e)
		{
			string buttonText = unifyStepTimeSkinButton.Text;
			if (buttonText.Equals("统一步时间"))
			{
				StepWrapper currentStep = getCurrentStepWrapper();
				if (currentStep == null || currentStep.TongdaoList == null || currentStep.TongdaoList.Count == 0)
				{
					MessageBox.Show("请先选中任意步数，才能进行统一调整！");
					SetNotice("请先选中任意步数，才能进行统一调整！");
					return;
				}

				int commonStepTime = Convert.ToInt16(unifyStepTimeNumericUpDown.Value);
				for (int i = 0; i < currentStep.TongdaoList.Count; i++)
				{
					getCurrentStepWrapper().TongdaoList[i].StepTime = commonStepTime;
				}
				if (isMultiMode)
				{
					copyUnifyValueToAll(getCurrentStep(), WHERE.STEP_TIME, commonStepTime);
				}
				RefreshStep();
			}
			else
			{
				new SKForm(this, globalIniPath, frame, frameSkinComboBox.Text).ShowDialog();
			}
		}

		#endregion

		//MARK：SkinMainForm：playPanel相关点击事件及辅助方法	
		#region 灯控调试按钮组(playPanel)点击事件及辅助方法		

		/// <summary>
		///事件：点击《以网络|串口连接》。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void changeConnectMethodSkinButton_Click(object sender, EventArgs e)
		{
			isConnectCom = !isConnectCom;
			changeConnectMethodSkinButton.Text = isConnectCom ? "以网络连接" : "以串口连接";
			deviceRefreshSkinButton.Text = isConnectCom ? "刷新串口" : "刷新网络";

			deviceRefreshSkinButton_Click(null, null);  // 切换连接后，手动帮用户搜索相应的设备列表。
		}

		/// <summary>
		/// 事件：点击《刷新串口|网络》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceRefreshSkinButton_Click(object sender, EventArgs e)
		{
			if( isConnectCom )
			{
				refreshComList();
			}
			else {
				refreshNetworkList();
			}						
		}

		/// <summary>
		/// 事件：改变deviceComboBox的选中项。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceSkinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			comName = deviceSkinComboBox.Text;
			if (!comName.Trim().Equals(""))
			{
				connectSkinButton.Enabled = true;
			}
			else
			{
				connectSkinButton.Enabled = false;
				MessageBox.Show("未选中可用串口");
			}
		}
		
		/// <summary>
		///  事件：点击《连接设备|断开连接》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectSkinButton_Click(object sender, EventArgs e)
		{
			connectButtonClick();
		}	

		/// <summary>
		/// 事件：点击《实时调试》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void realtimeSkinButton_Click(object sender, EventArgs e)
		{
			// 默认情况下，实时调试还没打开，点击后设为打开状态（文字显示为关闭实时调试，图片加颜色）
			if (!isRealtime)
			{
				realtimeSkinButton.Image = global::LightController.Properties.Resources.实时调试;
				realtimeSkinButton.Text = "关闭实时";
				isRealtime = true;
			}
			else //否则( 按钮显示为“断开连接”）断开连接
			{
				realtimeSkinButton.Image = global::LightController.Properties.Resources.实时调试02;
				realtimeSkinButton.Text = "实时调试";
				isRealtime = false;
			}
		}

		/// <summary>
		/// 事件：点击《保持状态|取消保持》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void keepSkinButton_Click(object sender, EventArgs e)
		{
			// 默认情况下，《保持其它灯状态》还没打开，点击后设为打开状态（文字显示为关闭实时调试，图片加颜色）
			if (!isKeepOtherLights)
			{
				keepSkinButton.Image = global::LightController.Properties.Resources.保持状态2;
				keepSkinButton.Text = "取消保持";
				isKeepOtherLights = true;
			}
			else //否则( 按钮显示为“保持其他灯状态”）断开连接
			{
				keepSkinButton.Image = global::LightController.Properties.Resources.保持状态1;
				keepSkinButton.Text = "保持状态";
				isKeepOtherLights = false;
			}
		}

		/// <summary>
		///  事件：点击《预览效果》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewSkinButton_Click(object sender, EventArgs e)
		{			
			if (lightAstList == null || lightAstList.Count == 0) {
				MessageBox.Show("当前工程还未添加灯具，无法预览。");
				previewSkinButton.Image = global::LightController.Properties.Resources.浏览效果前;
				return;
			}

			previewSkinButton.Image = global::LightController.Properties.Resources.浏览效果后;			
			SetNotice("正在生成预览数据，请稍候...");
			try
			{				
				DataConvertUtils.SaveProjectFileByPreviewData(GetDBWrapper(false), globalIniPath, frame, new PreviewCallBack(this));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		///  事件：点击《触发音频》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void makeSoundSkinButton_Click(object sender, EventArgs e)
		{
			makeSoundSkinButton.Image = global::LightController.Properties.Resources.触发音频后;
			this.Refresh();

			playTools.MusicControl();

			makeSoundSkinButton.Image = global::LightController.Properties.Resources.触发音频;
		}

		/// <summary>
		/// 事件： 点击《结束预览》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void endviewSkinButton_Click(object sender, EventArgs e)
		{
			endview();

			makeSoundSkinButton.Image = global::LightController.Properties.Resources.触发音频;
			previewSkinButton.Image = global::LightController.Properties.Resources.浏览效果前;	

		}				

		/// <summary>
		/// 辅助方法：重新搜索com列表：供启动时及需要重新搜索设备时使用。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshComList()
		{
			// 动态加载可用的dmx512串口列表		 
			deviceSkinComboBox.Items.Clear();
			SerialPortTools comTools = SerialPortTools.GetInstance();
			comList = comTools.GetDMX512DeviceList();
			if (comList != null && comList.Length > 0)
			{
				foreach (string com in comList)
				{
					deviceSkinComboBox.Items.Add(com);
				}
				deviceSkinComboBox.SelectedIndex = 0;
				deviceSkinComboBox.Enabled = true;
			}
			else
			{
				deviceSkinComboBox.Text = "";
				deviceSkinComboBox.Enabled = false;
				connectSkinButton.Enabled = false;
			}
		}

		/// <summary>
		/// TODO：11.22 网络连接
		/// 辅助方法：重新搜索ip列表-》填入deviceComboBox中
		/// </summary>
		private void refreshNetworkList()
		{
			deviceSkinComboBox.Items.Clear();
			deviceSkinComboBox.Enabled = false;
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
					Thread.Sleep(MainFormBase.NETWORK_WAITTIME);
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
						deviceSkinComboBox.Items.Add(d2.Value.DeviceName + "(" + d2.Key + ")" + localIPLast);
						ipaList.Add(new IPAst() { LocalIP = device.Key, DeviceIP = d2.Value.DeviceIp, DeviceName = d2.Value.DeviceName });
					}
				}
			}

			if (ipaList.Count > 0)
			{
				deviceSkinComboBox.Enabled = true;
				deviceSkinComboBox.SelectedIndex = 0;
			}
			else
			{
				MessageBox.Show("未找到可用的网络设备，请确认后重试。");
			}
		}

		/// <summary>
		///  辅助方法：《连接设备按钮组》是否显示
		/// </summary>
		/// <param name="v"></param>
		public override void EnableConnectedButtons(bool connected)
		{
			// 左上角的《串口列表》《刷新串口列表》可用与否，与下面《各调试按钮》是否可用刚刚互斥
			comPanel.Enabled = !connected;
			//comRefreshSkinButton.Enabled = !connected;

			keepSkinButton.Enabled = connected;
			realtimeSkinButton.Enabled = connected;
			makeSoundSkinButton.Enabled = connected;
			previewSkinButton.Enabled = connected;
			endviewSkinButton.Enabled = connected;

			// 是否连接
			isConnected = connected;

			if (connected)
			{
				connectSkinButton.Image = global::LightController.Properties.Resources.断开连接;
				connectSkinButton.Text = "断开连接";
			}
			else
			{
				connectSkinButton.Image = global::LightController.Properties.Resources.连接;
				connectSkinButton.Text = "连接设备";
			}
		}
		
		/// <summary>
		/// 辅助方法：调用基类的单灯单步发送DMX512帧数据;并操作本类中的相关数据
		/// </summary>
		protected override void oneStepWork()
		{
			base.oneStepWork();
			previewSkinButton.Image = global::LightController.Properties.Resources.浏览效果前;
		}

		/// <summary>
		/// 辅助方法：点击《连接设备》
		/// </summary>
		protected override void connectButtonClick()
		{
			playTools = PlayTools.GetInstance();
			// 如果还没连接（按钮显示为“连接设备”)，那就连接
			if (!isConnected)
			{
				if (isConnectCom)
				{
					if (String.IsNullOrEmpty(comName))
					{
						MessageBox.Show("未选中可用串口。");
						return;
					}
					playTools.ConnectDevice(comName);
					EnableConnectedButtons(true);
				}
				else
				{
					if (String.IsNullOrEmpty(comName) || deviceSkinComboBox.SelectedIndex < 0)
					{
						MessageBox.Show("未选中可用网络连接。");
						return;
					}

					IPAst ipAst = ipaList[deviceSkinComboBox.SelectedIndex];
					ConnectTools.GetInstance().Start(ipAst.LocalIP);
					playTools.StartInternetPreview(ipAst.DeviceIP, new NetworkDebugReceiveCallBack(this), eachStepTime);
				}
			}
			else //否则( 按钮显示为“断开连接”）断开连接
			{
				if (isConnectCom)
				{
					playTools.CloseDevice();
					
				}
				else
				{
					playTools.StopInternetPreview(new NetworkEndDebugReceiveCallBack());
				}

				previewSkinButton.Image = global::LightController.Properties.Resources.浏览效果前;
				EnableConnectedButtons(false);
				SetNotice("已断开连接");

				//MARK：11.23 延迟的骗术，在每次断开连接后立即重新搜索网络设备并建立socket连接。
				if (!isConnectCom)
				{
					Thread.Sleep(500);
					refreshNetworkList();
				}
			}
		}

		#endregion

		#region 几个全局辅助方法

		/// <summary>
		/// 辅助方法：设置提醒 - 实现基类的纯虚函数
		/// </summary>
		/// <param name="noticeText"></param>
		public override void SetNotice(string noticeText)
		{
			noticeLabel.Text = noticeText;
		}

		/// <summary>
		///  辅助方法：设置是否忙时 - 进行某些操作时，应避免让控件可用（如导出工程、保存工程）；完成后再设回来。
		/// </summary>
		/// <param name="busy">是否处于忙时（不要操作其他控件）</param>
		protected override void setBusy(bool busy)
		{
			this.Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
			this.middleTableLayoutPanel.Enabled = !busy;
			this.projectSkinPanel.Enabled = !busy;
			this.tdCommonPanel.Enabled = !busy;
		}

		#endregion

		#region bgWorker相关事件
		/// <summary>
		/// 事件：bgWorker的后台工作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			for (int i = 0; i <= 100; i++)
			{
				if (bgWorker.CancellationPending)
				{
					e.Cancel = true;
					return;
				}
				else
				{
					bgWorker.ReportProgress(i, "Working");
					System.Threading.Thread.Sleep(100);
				}
			}
		}

		public void bgWorker_ProgessChanged(object sender, ProgressChangedEventArgs e)
		{
			//string state = (string)e.UserState;//接收ReportProgress方法传递过来的userState
			//this.progressBar1.Value = e.ProgressPercentage;
			//this.label1.Text = "处理进度:" + Convert.ToString(e.ProgressPercentage) + "%";
		}

		public void bgWorker_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.ToString());
				return;
			}
			//if (!e.Cancelled)
			//	this.label1.Text = "处理完毕!";
			//else
			//	this.label1.Text = "处理终止!";
		}

		#endregion

		#region 测试按钮及废弃方法块

		/// <summary>
		///  事件：《（曾维佳）测试按钮组》点击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newTestButton_Click(object sender, EventArgs e)
		{
			int buttonIndex = MathAst.GetIndexNum(((Button)sender).Name, 0);
			Console.WriteLine(buttonIndex);
			Test test = new Test(GetDBWrapper(true), this, globalIniPath);
			//Test test = new Test(GetDBWrapper(true) );
			test.Start(buttonIndex);
		}

		/// <summary>
		/// 事件：点击《自定义测试按钮》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bigTestButton_Click(object sender, EventArgs e)
		{
			// showAllLightCurrentAndTotalStep();						

			//Console.WriteLine(TempMaterialAst);

			//lightsSkinListView.Dock = DockStyle.Fill;

			// lightsSkinListView.AutoArrange = !lightsSkinListView.AutoArrange;

			//for (int i = 0; i < lightsSkinListView.Items.Count; i++)
			//{
			//	Console.WriteLine(i + " :: " + lightsSkinListView.Items[i].Text);
			//}

			// 测lambda表达式取相关数据
			//dbValueList = getValueList();
			//var tempValueList = dbValueList.Where(t => t.PK.LightIndex == 1);
			////Console.WriteLine(tempValueList.GetType());
			//int i = 0;
			//foreach (var item in tempValueList)
			//{
			//	i++;
			//	Console.WriteLine(i + " : " + item.GetType());
			//}

			// 10.24 测新的读取指定通道在某FM情况下的步数列表
			//if (valueDAO != null) {
			//	for (int lightID = 1; lightID < 512; lightID++)
			//	{
			//		IList<DB_Value> pkValueList = valueDAO.GetPKList(new DB_ValuePK() { LightID = lightID, Frame = frame, Mode = mode });
			//		Console.WriteLine(lightID + " - " +pkValueList.Count);
			//	}
			//}

			// 10.25 测某个灯具当前FM的LightStepWrapper
			//LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			////LightStepWrapper lsWrapper = lightWrapperList[1].LightStepWrapperList[frame, mode];
			//Console.WriteLine(lsWrapper);		

			//MakeCurrentStepWrapperData();

			//SocketTools.GetInstance().Start();

			// 11.27 测getNotSelectedIndices
			//foreach (int  item in getNotSelectedIndices())
			//{
			//	Console.Write(item + "、");
			//}
			//Console.WriteLine();	

			//if (selectedIndex != -1) {
			//	DB_ValuePK pk = new DB_ValuePK();
			//	pk.Frame = frame;
			//	pk.Mode = mode;
			//	pk.LightID = lightAstList[selectedIndex].StartNum;
			//	pk.LightIndex = lightAstList[selectedIndex].StartNum;

			//	IList<TongdaoWrapper> tdList = GetFMTDList(pk);
			//	foreach (TongdaoWrapper tongdao in tdList)
			//	{
			//		Console.WriteLine(tongdao.ScrollValue + " : " + tongdao.ChangeMode + " : " + tongdao.StepTime);
			//	}
			//}

			//string a = "zwj";
			//bool flag = false ;
			// flag ?  "frh":"flh";

		}

		/// <summary>
		///  辅助方法:根据当前《 变动方式》选项 是否屏蔽，处理相关通道是否可设置
		///  --9.4禁用此功能，即无论是否屏蔽，
		/// </summary>
		/// <param name="tongdaoIndex">tongdaoList的Index</param>
		/// <param name="shielded">是否被屏蔽</param>
		private void enableTongdaoEdit(int tongdaoIndex, bool shielded)
		{
			tdSkinTrackBars[tongdaoIndex].Enabled = shielded;
			tdValueNumericUpDowns[tongdaoIndex].Enabled = shielded;
			tdStepTimeNumericUpDowns[tongdaoIndex].Enabled = shielded;
		}		

		#endregion

	}





}
