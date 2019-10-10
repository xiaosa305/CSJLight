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

namespace LightController.MyForm
{

	public partial class SkinMainForm : MainFormInterface
	{
		private bool isPainting = false;

		public SkinMainForm()
		{
			InitializeComponent();

			// 动态设定软件存储目录
			savePath = @IniFileAst.GetSavePath(Application.StartupPath);

			// 动态显示测试按钮
			bool isShowTestButton = IniFileAst.GetButtonShow(Application.StartupPath, "testButton");
			testGroupBox.Visible = isShowTestButton;
			bigTestButton.Visible = isShowTestButton;

			// 动态显示硬件升级按钮
			bool isShowHardwareUpddate = IniFileAst.GetButtonShow(Application.StartupPath, "hardwareUpdateButton");
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
			if (FrameCount == 0){
				MessageBox.Show("FrameList.txt中的场景不可为空，否则软件无法使用，请修改后重启。");
				Exit();
			}
			frameSkinComboBox.SelectedIndex =0;						

			//模式选项框
			modeSkinComboBox.Items.AddRange(new object[] {	"常规模式","音频模式"});
			modeSkinComboBox.SelectedIndex = 0;

			// 《统一跳渐变》numericUpDown不得为空，否则会造成点击后所有通道的changeMode形式上为空（不过Value不是空）
			commonChangeModeSkinComboBox.SelectedIndex = 1;
			#endregion

			#region 各类监听器
			// MARK：此处的TODO只是作为标记，以快速定位到监听器
			for (int i = 0; i < FrameCount; i++) {

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
			commonValueNumericUpDown.MouseEnter += new EventHandler(this.commonValueNumericUpDown_MouseEnter);
			commonValueNumericUpDown.MouseWheel += new MouseEventHandler(this.commonValueNumericUpDown_MouseWheel);
			commonValueNumericUpDown.ValueChanged += new System.EventHandler(this.commonValueNumericUpDown_ValueChanged);

			commonValueTrackBar.MouseEnter += new EventHandler(this.commonValueTrackBar_MouseEnter);
			commonValueTrackBar.MouseWheel += new MouseEventHandler(this.commonValueTrackBar_MouseWheel);
			commonValueTrackBar.ValueChanged += new EventHandler(this.commonValueTrackBar_ValuiChanged);

			commonStepTimeNumericUpDown.MouseEnter += new EventHandler(this.commonStepTimeNumericUpDown_MouseEnter);
			commonStepTimeNumericUpDown.MouseWheel += new MouseEventHandler(this.commonStepTimeNumericUpDown_MouseWheel);

			#endregion

			isInit = true;
		}



		private void SkinMainForm_Load(object sender, EventArgs e)
		{
			// 启动时刷新可用串口列表			
			refreshComList();
		}

		#region 各种工具按钮


		/// <summary>
		/// 事件：点击《其他工具》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void otherToolsSkinButton_Click(object sender, EventArgs e)
		{
			ToolsForm toolsForm = new ToolsForm(this);
			toolsForm.ShowDialog();
		}

		/// <summary>
		///  事件：点击《硬件设置》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hardwareSetSkinButton_Click(object sender, EventArgs e)
		{
			HardwareSetChooseForm hscForm = new HardwareSetChooseForm(this);
			hscForm.ShowDialog();
		}

		/// <summary>
		/// 点击《设备更新》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateSkinButton_Click(object sender, EventArgs e)
		{
			bool isFromDB = true;
			UpdateForm updateForm = new UpdateForm(this, GetDBWrapper(isFromDB), globalIniPath);
			updateForm.ShowDialog();
		}

		/// <summary>
		/// 事件：点击《灯具列表(编辑)》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightListSkinButton_Click(object sender, EventArgs e)
		{
			LightsForm skinLightsForm = new LightsForm(this, lightAstList);
			skinLightsForm.ShowDialog();
		}

		/// <summary>
		///  事件：点击《全局配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void globalSetSkinButton_Click(object sender, EventArgs e)
		{
			GlobalSetForm globalSetForm = new GlobalSetForm(this, globalIniPath);
			globalSetForm.ShowDialog();
		}

		/// <summary>
		///  事件：点击《摇麦设置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ymSkinButton_Click(object sender, EventArgs e)
		{
			YMSetForm ymSetForm = new YMSetForm(this, globalIniPath);
			ymSetForm.ShowDialog();
		}

		/// <summary>
		///  事件：点击《硬件更新》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void networkSkinButton_Click(object sender, EventArgs e)
		{
			new HardwareUpdateForm(this).ShowDialog();
		}

		/// <summary>
		///  事件：点击《退出应用》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exitSkinButton_Click(object sender, EventArgs e)
		{
			Exit();
		}

		#endregion


		#region 工程相关 及 初始化辅助方法		

		/// <summary>
		/// 事件： 点击《新建工程》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newSkinButton_Click(object sender, EventArgs e)
		{
			// 9.10 每次打开新建窗口时，先将isCreateSuccess设为false;避免取消新建，仍会打开添加灯。
			IsCreateSuccess = false;

			NewForm newForm = new NewForm(this);
			newForm.ShowDialog();

			//8.21 ：当IsCreateSuccess==true时，打开灯具编辑
			if (IsCreateSuccess) {
				lightListSkinButton_Click(null, null);
			}
		}

		/// <summary>
		/// 事件：点击《打开工程》按钮 
		/// --新建一个OpenForm，再在里面回调OpenProject()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openSkinButton_Click(object sender, EventArgs e)
		{
			OpenForm openForm = new OpenForm(this, currentProjectName);
			openForm.ShowDialog();
		}


		/// <summary>
		/// 辅助方法：ClearAllDate()最后一步，但需针对不同的MainForm子类来实现。
		/// MARK：子Form.ClearAllData() 需要经常跑到此处来验证,每次新添变量时处理下
		/// </summary>
		protected override void clearAllData()
		{
			base.clearAllData();

			//单独针对本MainForm的代码: 
			// ①清空listView列表；
			// ②禁用步调节按钮组、隐藏所有通道、stepLabel设为0/0、选中灯具信息清空
			lightsSkinListView.Clear();
			
			stepSkinPanel.Enabled = false;
			hideAllTDPanels();
			showStepLabel(0, 0);
			editLightInfo(null);

			enableSingleMode(true);
		}

		/// <summary>
		///  事件：点击《保存工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveSkinButton_Click(object sender, EventArgs e)
		{
			saveAll();
		}

		/// <summary>
		///事件：点击《导出工程》按钮：将当前保存好的内容，导出到项目目录下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exportSkinButton_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show("此操作只会导出已保存的工程，确定现在导出吗？",
				"导出工程",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Question);
			if (dr == DialogResult.OK)
			{
				exportFolderBrowserDialog.ShowDialog();
				string exportPath = exportFolderBrowserDialog.SelectedPath;
				if ( ! string.IsNullOrEmpty(exportPath) ){
					exportPath +=  @"\CSJ";
					DBWrapper dbWrapper = GetDBWrapper(true);

					FileTools fileTools = FileTools.GetInstance();
					fileTools.ProjectToFile(dbWrapper, globalIniPath, exportPath);
					//导出成功后，打开文件夹
					System.Diagnostics.Process.Start(exportPath);
				}
			}
		}


		/// <summary>
		///  辅助方法：《保存工程》《导出工程》enabled设为传入bool值
		/// </summary>
		protected override void enableSave(bool enable)
		{
			saveSkinButton.Enabled = enable;
			exportSkinButton.Enabled = enable;
		}

		/// <summary>
		///  辅助方法：《保存|读取灯具位置》按钮是否可用
		/// </summary>
		/// <param name="enable"></param>
		protected override void enableSLArrange(bool enableSave, bool enableLoad) {
			saveArrangeToolStripMenuItem.Enabled = enableSave;
			loadArrangeToolStripMenuItem.Enabled = enableLoad;
		}

		/// <summary>
		///  辅助方法：将所有全局配置相关的按钮（灯具、升级、全局、摇麦、网络、连接设备）Enabled设为传入bool值
		/// </summary>
		/// <param name="v"></param>
		protected override void enableGlobalSet(bool enable)
		{
			// 菜单栏几个按钮
			updateSkinButton.Enabled = enable;
			lightListSkinButton.Enabled = enable;
			globalSetSkinButton.Enabled = enable;
			ymSkinButton.Enabled = enable;
			hardwareUpdateSkinButton.Enabled = enable;

			// 调试栏	->《连接设备》按钮
			connectSkinButton.Enabled = enable;
		}

		/// <summary>
		///添加lightAst列表到主界面内存中,主要供 LightsForm调用（以及OpenProject调用）
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
				){Tag = lightAstList2[i].LightName + ":" + lightAstList2[i].LightType}
				);
			}

			// 2.最后处理通道显示：每次调用此方法后应该隐藏通道数据，避免误操作。
			hideAllTDPanels();
		}


		#endregion


		#region 选中listView中的灯具

		/// <summary>
		/// 事件：改变选中的灯时进行的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsSkinListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			//Console.WriteLine(DateTime.Now.ToString("hh:mm:ss") + " : lightsSkinListView_SelectedIndexChanged。");

			// 必须判断这个字段(Count)，否则会报异常
			if (lightsSkinListView.SelectedIndices.Count > 0)
			{
				selectedIndex = lightsSkinListView.SelectedIndices[0];
				//Console.WriteLine(lightsSkinListView.SelectedItems[0].Text|Tag);
				generateLightData();
			}			
		}

		/// <summary>
		/// 辅助方法：初始化灯具数据。
		/// 0.先查看当前内存是否已有此数据 
		/// 1.若还未有，则取出相关的ini进行渲染
		/// 2.若内存内 或 数据库内已有相关数据，则使用这个数据。
		/// </summary>
		/// <param name="la"></param>
		private void generateLightData()
		{
			if (selectedIndex == -1) {
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
				hideAllTDPanels();
				showStepLabel(0, 0);
			}
			else
			{
				changeFrameMode();
			}
			stepSkinPanel.Enabled = true;
		}

		/// <summary>
		///  辅助方法：通过LightAst，显示选中灯具信息
		/// </summary>
		private void editLightInfo(LightAst lightAst)
		{
			if (lightAst == null) {
				currentLightPictureBox.Image = null ;
				lightNameLabel.Text = null ;
				lightTypeLabel.Text = null ;
				lightsAddrLabel.Text = null ;
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
			selectedLightName = lightAst.LightName+"-" +lightAst.LightType;
		}

		#endregion


		#region 步数相关的按钮及辅助方法


		/// <summary>
		///  事件：勾选《（是否）使用模板生成步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addStepCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			isUseStepTemplate = addStepCheckBox.Checked;
		}

		/// <summary>
		/// 事件：更改《选择场景》选项后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frameSkinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			frame = frameSkinComboBox.SelectedIndex;

			if (lightAstList != null && lightAstList.Count > 0)
			{
				changeFrameMode();
			}
		}

		/// <summary>
		/// 事件：更改《选择模式》选项后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void modeSkinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isInit)
			{
				mode = modeSkinComboBox.SelectedIndex;
				// 若模式为声控模式mode=1
				// 1.改变几个label的Text; 
				// 2.改变跳变渐变-->是否声控；
				// 3.所有步时间值的调节，改为enabled=false			
				if (mode == 1)
				{				
					for (int i = 0; i < FrameCount; i++)
					{
						this.tdChangeModeSkinComboBoxes[i].Items.Clear();
						this.tdChangeModeSkinComboBoxes[i].Items.AddRange(new object[] {	"屏蔽",	"跳变"});
						this.tdStepTimeNumericUpDowns[i].Hide();
						this.tdTrueTimeLabels[i].Hide();
					}
					commonChangeModeSkinButton.Text = "统一声控";
					commonChangeModeSkinComboBox.Items.Clear();
					commonChangeModeSkinComboBox.Items.AddRange(new object[] { "屏蔽", "跳变"});
					commonChangeModeSkinComboBox.SelectedIndex = 0;

					commonStepTimeNumericUpDown.Hide();
					commonStepTimeSkinButton.Text = "修改此音频场景全局设置";
					commonStepTimeSkinButton.Size = new System.Drawing.Size(210,27);

					thirdLabel1.Hide();
					thirdLabel2.Hide();
					thirdLabel3.Hide();
				}
				else //mode=0，常规模式
				{
					for (int i = 0; i < FrameCount; i++)
					{
						this.tdChangeModeSkinComboBoxes[i].Items.Clear();
						this.tdChangeModeSkinComboBoxes[i].Items.AddRange(new object[] {	"跳变","渐变","屏蔽"});
						this.tdStepTimeNumericUpDowns[i].Show();
						this.tdTrueTimeLabels[i].Show();
					}
					commonChangeModeSkinButton.Text = "统一跳渐变";
					commonChangeModeSkinComboBox.Items.Clear();
					commonChangeModeSkinComboBox.Items.AddRange(new object[] { "跳变", "渐变", "屏蔽" });
					commonChangeModeSkinComboBox.SelectedIndex = 0;

					commonStepTimeNumericUpDown.Show();
					commonStepTimeSkinButton.Text = "统一步时间";
					commonStepTimeSkinButton.Size = new System.Drawing.Size(111, 27);

					thirdLabel1.Show();
					thirdLabel2.Show();
					thirdLabel3.Show();
				}
				if (lightAstList != null && lightAstList.Count > 0)
				{
					changeFrameMode();
				}
			}
		}

		/// <summary>
		/// 辅助方法： 改变了模式和场景后的操作
		/// </summary>
		private void changeFrameMode()
		{
			// 9.2 不可让selectedIndex为-1,否则会出现数组越界错误
			if (selectedIndex == -1) {
				return;
			}

			LightWrapper lightWrapper = lightWrapperList[selectedIndex];
			LightStepWrapper lightStepWrapper = lightWrapper.LightStepWrapperList[frame, mode];

			// 为空或StepList数量是0
			if (lightStepWrapper == null || lightStepWrapper.StepWrapperList.Count == 0)
			{
				hideAllTDPanels();
				showStepLabel(0, 0);
			}
			else // lightStepWrapper != null && lightStepWrapper.StepList.Count>0 : 也就是已经有值了
			{
				int currentStep = lightStepWrapper.CurrentStep;
				int totalStep = lightStepWrapper.TotalStep;

				StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[currentStep - 1];
				showTDPanels(stepWrapper.TongdaoList, stepWrapper.StartNum);
				showStepLabel(currentStep, totalStep);
			}
		}

		/// <summary>
		///  事件：点击《上一步》
		///  先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backStepSkinButton_Click(object sender, EventArgs e)
		{
			int currentStep = getCurrentStep();
			int totalStep = getTotalStep();			
			if (currentStep > 1)
			{
				chooseStep(currentStep - 1);
			}
			else
			{
				chooseStep(totalStep);
			}
		}

		/// <summary>
		///  事件：点击《下一步》
		///  先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nextStepSkinButton_Click(object sender, EventArgs e)
		{
			int currentStep = getCurrentStep();			
			int totalStep = getTotalStep();
			if (currentStep < totalStep)
			{
				chooseStep(currentStep + 1);
			}
			else
			{
				chooseStep(1);
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
			// 1.获取当前步与最高值，总步数			
			// 若当前步 <= 总步数，则可以插入，并将之后的步数往后移动
			// 否则报错

			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			if (lsWrapper.CurrentStep <= lsWrapper.TotalStep)
			{
				// 根据isUseStepMode，生成要插入步的内容
				StepWrapper newStep = StepWrapper.GenerateNewStep(
					(isUseStepTemplate || getCurrentStepWrapper() == null) ? getCurrentStepTemplate() : getCurrentStepWrapper(),
					mode
				);
				// 要插入的位置的index
				int stepIndex = getCurrentStep() - 1;
				// 插入的方式：前插(true）还是后插（false)
				bool insertBefore = ((Button)sender).Name.Equals("insertBeforeSkinButton");

				lsWrapper.InsertStep(stepIndex, newStep, insertBefore);

				this.showTDPanels(newStep.TongdaoList, newStep.StartNum);
				this.showStepLabel(lsWrapper.CurrentStep, lsWrapper.TotalStep);

				if (isMultiMode)
				{
					copyToAll(0);
				}
			}
			else
			{
				MessageBox.Show("Dickov:当前步大于总步数");
			}
		}

		/// <summary>
		/// 事件：点击《追加步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addStepSkinButton_Click(object sender, EventArgs e)
		{
			LightWrapper currentLightWrapper = getCurrentLightWrapper();
			// 如果此值为空，则创建之
			if (currentLightWrapper.LightStepWrapperList[frame, mode] == null)
			{
				currentLightWrapper.LightStepWrapperList[frame, mode] = new LightStepWrapper()
				{
					StepWrapperList = new List<StepWrapper>()
				};
			}

			// 根据isUseStepMode，生成要插入步的内容 :
			//1.若勾选了使用模板 或 当前灯具在本场景及模式下总步数为0 ，则使用stepMode数据，
			//2.否则使用本灯当前最大步的数据			 
			StepWrapper newStep = StepWrapper.GenerateNewStep(
				(isUseStepTemplate || getTotalStep() == 0) ? getCurrentStepTemplate() : getCurrentLightMaxStepWrapper(),
				mode
				);

			// 调用包装类内部的方法,来追加步
			currentLightWrapper.LightStepWrapperList[frame, mode].AddStep(newStep);

			// 显示新步
			this.showTDPanels(newStep.TongdaoList, newStep.StartNum);
			this.showStepLabel(currentLightWrapper.LightStepWrapperList[frame, mode].CurrentStep, currentLightWrapper.LightStepWrapperList[frame, mode].TotalStep);

			if (isMultiMode) {
				copyToAll(0);
			}
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
			LightStepWrapper lightStepWrapper = getCurrentLightStepWrapper();
			int stepIndex = getCurrentStep() - 1;

			// 调用包装类内部的方法:删除某一步
			try
			{
				lightStepWrapper.DeleteStep(stepIndex);
				if (isMultiMode)
				{
					copyToAll(0);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}

			int currentStep = lightStepWrapper.CurrentStep;
			if (currentStep > 0)
			{
				StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[currentStep - 1];
				this.showTDPanels(stepWrapper.TongdaoList, stepWrapper.StartNum);
				this.showStepLabel(lightStepWrapper.CurrentStep, lightStepWrapper.TotalStep);				
			}
			else
			{
				this.showTDPanels(null, 0);
				this.showStepLabel(0, 0);
			}
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
			// 1. 先判断是不是同模式及同一种灯具（非同一个灯具也可以复制，但需类型(同一个灯库内容)一样)
			StepWrapper currentStep = getCurrentStepWrapper();
			if (currentStep == null)
			{
				MessageBox.Show("当前步数据为空，无法粘贴步");
				return;
			}
			if (currentStep.LightMode != tempStep.LightMode)
			{
				MessageBox.Show("不同模式下无法复制步");
				return;
			}
			if (currentStep.LightFullName != tempStep.LightFullName)
			{
				MessageBox.Show("不同类型灯具无法复制步");
				return;
			}

			// 2.逐一将TongdaoList的某些数值填入tempStep中，而非粗暴地将currentStep 设为tempStep
			for (int i = 0; i < tempStep.TongdaoList.Count; i++)
			{
				currentStep.TongdaoList[i].ScrollValue = tempStep.TongdaoList[i].ScrollValue;
				currentStep.TongdaoList[i].ChangeMode = tempStep.TongdaoList[i].ChangeMode;
				currentStep.TongdaoList[i].StepTime = tempStep.TongdaoList[i].StepTime;
			}

			// 3.重新渲染当前步的所有通道
			showTDPanels(currentStep.TongdaoList, currentStep.StartNum);		

			//4.如果是多灯模式，则需要在复制步之后处理下每个灯具的信息
			if (isMultiMode) {
				copyToAll(0);
			}
		}

		/// <summary>
		/// 辅助方法：抽象了【选择某一个指定步数后，统一的操作；NextStep和BackStep等应该都使用这个方法】
		/// </summary>
		protected override void chooseStep(int stepNum)
		{		
			if (stepNum == 0) {
				showTDPanels(null,0);
				showStepLabel(0,0);
				return;
			}
			
			LightStepWrapper lightStepWrapper = getCurrentLightStepWrapper();
			StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[stepNum - 1];			
			lightStepWrapper.CurrentStep = stepNum;	
			showTDPanels(stepWrapper.TongdaoList, stepWrapper.StartNum);
			showStepLabel(lightStepWrapper.CurrentStep, lightStepWrapper.TotalStep);

			if (isMultiMode) {
				foreach (int lightIndex in selectedIndices)
				{
					getSelectedLightStepWrapper(lightIndex).CurrentStep = stepNum; 				
				}
			}

			if (isConnected && isRealtime)
			{
				oneLightStepWork();
			}
		}

		/// <summary>
		///  辅助方法：用来显示stepLabel-->当前步/总步数
		/// 7.2 +隐藏《删除步》按钮
		/// </summary>
		/// <param name="currentStep"></param>
		/// <param name="totalStep"></param>
		private void showStepLabel(int currentStep, int totalStep)
		{
			// 1. 设label的Text值					   
			stepLabel.Text = MathAst.GetFourWidthNumStr(currentStep,true) + "/" + MathAst.GetFourWidthNumStr(totalStep, false);

			// 2.1 设定《删除步》按钮是否可用
			deleteStepSkinButton.Enabled = totalStep != 0;

			// 2.2 设定《追加步》、《前插入步》《后插入步》按钮是否可用			
			bool insertEnabled = totalStep < 1000 ;
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
			multiPasteSkinButton.Enabled = TempMaterialAst != null && TempMaterialAst.Mode==mode;

			// 3.设定统一调整区是否可用
			tdCommonPanel.Enabled =  totalStep != 0 ;

			// 10.9 添加一个选择步数的框
			chooseStepNumericUpDown.Enabled = totalStep != 0;
			chooseStepSkinButton.Enabled = totalStep != 0;			
			chooseStepNumericUpDown.Maximum = totalStep;						

		}

		#endregion


		#region tdPanels内部数值的调整事件及辅助方法

		/// <summary>
		/// 辅助方法：通过传来的数值，生成通道列表的数据
		/// </summary>
		/// <param name="tongdaoList"></param>
		/// <param name="startNum"></param>
		private void showTDPanels(IList<TongdaoWrapper> tongdaoList, int startNum)
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
					tdTrueTimeLabels[i].Text = (float) tongdaoList[i].StepTime * eachStepTime / 1000 + "s";

					tdSkinTrackBars[i].ValueChanged += new System.EventHandler(this.tdSkinTrackBars_ValueChanged);
					tdValueNumericUpDowns[i].ValueChanged += new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);
					tdChangeModeSkinComboBoxes[i].SelectedIndexChanged += new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);
					tdStepTimeNumericUpDowns[i].ValueChanged += new EventHandler(this.tdStepTimeNumericUpDowns_ValueChanged);

					tdPanels[i].Show();
				}
				for (int i = tongdaoList.Count; i < 32; i++) {
					tdPanels[i].Hide();
				}

				isPainting = false;
			}
			
		}

		/// <summary>
		/// 辅助方法：隐藏所有tdPanels,因为所有panels为空了，则《统一调整框》enabled应设为false
		/// </summary>
		private void hideAllTDPanels()
		{
			isPainting = true;

			for (int i = 0; i < 32; i++)
			{
				tdPanels[i].Hide();
			}
			tdCommonPanel.Enabled = false;

			isPainting = false;
		}

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
			int tdValue =  tdSkinTrackBars[tongdaoIndex].Value;

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
			int tdValue = Convert.ToInt16(Double.Parse(tdValueNumericUpDowns[tongdaoIndex].Text));

			// 2.调整相应的vScrollBar的数值；
			// 8.28 ：在修改时取消其监听事件，修改成功恢复监听；这样就能避免重复触发监听事件
			tdSkinTrackBars[tongdaoIndex].ValueChanged -= new System.EventHandler(this.tdSkinTrackBars_ValueChanged);
			tdSkinTrackBars[tongdaoIndex].Value = tdValue;
			tdSkinTrackBars[tongdaoIndex].ValueChanged += new System.EventHandler(this.tdSkinTrackBars_ValueChanged);

		

			//3.取出recentStep,使用取出的index，给stepWrapper.TongdaoList[index]赋值；并检查是否实时生成数据进行操作
			changeScrollValue(tongdaoIndex , tdValue);
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
				copyToAll2(0,tdIndex,WHERE.CHANGE_MODE,changeMode);
			}

			#region 废弃代码块：

			//if (isInit)
			//{
			//	// 3.（6.29修改）若当前模式是声控模式：
			//	//		则更改其中某一个通道的是否声控的值，则此通道的所有声控步，都要统一改变其是否声控值
			//	if (mode == 1)
			//	{
			//		IList<StepWrapper> stepWrapperList = getCurrentLightStepWrapper().StepWrapperList;
			//		foreach (StepWrapper stepWrapper in stepWrapperList)
			//		{
			//			stepWrapper.TongdaoList[index].ChangeMode = tdChangeModeSkinComboBoxes[index].SelectedIndex;
			//		}
			//	}
			//	// 4.(8.8新增判断）若当前模式是普通模式：
			//	//		被屏蔽掉的通道，其数值不再可以改动;否则可以调整
			//	//else
			//	//{
			//	//	enableTongdaoEdit(index, tdChangeModeSkinComboBoxes[index].SelectedIndex != 2);
			//	//}
			//}

			#endregion
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
				copyToAll2(0,tdIndex,WHERE.STEP_TIME,stepTime);
			}

		}


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
			showLabelPanels( j );

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
				decimal dd = commonStepTimeNumericUpDown.Value + commonStepTimeNumericUpDown.Increment;
				if (dd <= commonStepTimeNumericUpDown.Maximum)
				{
					commonStepTimeNumericUpDown.Value = dd;
				}
			}
			else if (e.Delta < 0)
			{
				decimal dd = commonStepTimeNumericUpDown.Value - commonStepTimeNumericUpDown.Increment;
				if (dd >= commonStepTimeNumericUpDown.Minimum)
				{
					commonStepTimeNumericUpDown.Value = dd;
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
			commonStepTimeNumericUpDown.Select();
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
				decimal dd = commonValueNumericUpDown.Value + commonValueNumericUpDown.Increment;
				if (dd <= commonValueNumericUpDown.Maximum)
				{
					commonValueNumericUpDown.Value = dd;
				}
			}
			else if (e.Delta < 0)
			{
				decimal dd = commonValueNumericUpDown.Value - commonValueNumericUpDown.Increment;
				if (dd >= commonValueNumericUpDown.Minimum)
				{
					commonValueNumericUpDown.Value = dd;
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
			commonValueNumericUpDown.Select();
		}

		/// <summary>shubiao		
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
				decimal dd = commonValueTrackBar.Value + commonValueTrackBar.SmallChange;
				if (dd <= commonValueTrackBar.Maximum)
				{
					commonValueTrackBar.Value = Decimal.ToInt16(dd);
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = commonValueTrackBar.Value - commonValueTrackBar.SmallChange;
				if (dd >= commonValueTrackBar.Minimum)
				{
					commonValueTrackBar.Value = Decimal.ToInt16(dd);
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
			commonValueNumericUpDown.Select();
		}

		/// <summary>
		///  事件：《统一设置通道值trackBar》值变动事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueTrackBar_ValuiChanged(object sender, EventArgs e)
		{
			commonValueNumericUpDown.Value = commonValueTrackBar.Value;
		}

		/// <summary>
		///  事件：《统一设置通道值numericUpDown》值变动事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
				commonValueTrackBar.Value = Decimal.ToInt16(commonValueNumericUpDown.Value);
		}

		/// <summary>
		/// 事件：点击《统一通道值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueSkinButton_Click(object sender, EventArgs e)
		{
			StepWrapper currentStep = getCurrentStepWrapper();
			//Console.WriteLine("统一通道值value:" + commonValueNumericUpDown.Value + " | text:" + commonValueNumericUpDown.Text);

			int commonValue = Convert.ToInt16(commonValueNumericUpDown.Text);
			for (int i = 0; i < currentStep.TongdaoList.Count; i++)
			{
				getCurrentStepWrapper().TongdaoList[i].ScrollValue = commonValue;
			}

			if (isMultiMode) {
				copyToAll(0);
			}
			refreshStep();
		}

		/// <summary>
		/// 事件：点击《统一跳渐变》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonChangeModeSkinButton_Click(object sender, EventArgs e)
		{
			StepWrapper currentStep = getCurrentStepWrapper();
			int commonChangeMode = commonChangeModeSkinComboBox.SelectedIndex;

			for (int i = 0; i < currentStep.TongdaoList.Count; i++)
			{
				getCurrentStepWrapper().TongdaoList[i].ChangeMode = commonChangeMode;
			}
			if (isMultiMode)
			{
				copyToAll(0);
			}
			refreshStep();
		}

		/// <summary>
		/// 事件：点击《统一步时间》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonStepTimeSkinButton_Click(object sender, EventArgs e)
		{
			string buttonText = commonStepTimeSkinButton.Text;
			if (buttonText.Equals("统一步时间"))
			{
				StepWrapper currentStep = getCurrentStepWrapper();
				int commonStepTime = Convert.ToInt16( commonStepTimeNumericUpDown.Text );			
				for (int i = 0; i < currentStep.TongdaoList.Count; i++)
				{
					getCurrentStepWrapper().TongdaoList[i].StepTime= commonStepTime;
				}
				if (isMultiMode) {
					copyToAll(0);
				}
				refreshStep();
			}
			else 
			{
				new SKForm(this, globalIniPath, frame,frameSkinComboBox.Text).ShowDialog();
			}
		}

		/// <summary>
		/// 事件：点击《全部归零》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zeroSkinButton_Click(object sender, EventArgs e)
		{
			StepWrapper currentStep = getCurrentStepWrapper();
			for (int i = 0; i < currentStep.TongdaoList.Count; i++)
			{
				getCurrentStepWrapper().TongdaoList[i].ScrollValue = 0;
			}
			if (isMultiMode)
			{
				copyToAll(0);
			}
			refreshStep();
		}

		/// <summary>
		///  事件：点击《设为初值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void initSkinButton_Click(object sender, EventArgs e)
		{
			StepWrapper stepNow = getCurrentStepWrapper();
			StepWrapper stepMode = getCurrentStepTemplate();
			for (int i = 0; i < stepNow.TongdaoList.Count; i++)
			{
				getCurrentStepWrapper().TongdaoList[i].ScrollValue = stepMode.TongdaoList[i].ScrollValue;
			}
			if (isMultiMode) {
				copyToAll(0);
			}
			refreshStep();
		}

		/// <summary>
		/// 事件：点击《多步调节》按钮
		/// 多步调整，传入当前灯的LightWrapper，在里面回调setMultiStepValues以调节相关的步数的值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiSkinButton_Click(object sender, EventArgs e)
		{
			MultiStepForm msForm = new MultiStepForm(this , getCurrentStep() , getTotalStep(),getCurrentStepWrapper() ,mode );
			msForm.ShowDialog();
		}

		#endregion

		#region 素材相关按钮及辅助方法

		/// <summary>
		///  事件：点击《使用素材》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void useMaterialSkinButton_Click(object sender, EventArgs e)
		{
			LightAst la = lightAstList[selectedIndex];
			MaterialUseForm materialUseForm = new MaterialUseForm(this, mode, la.LightName, la.LightType);
			materialUseForm.ShowDialog();

		}

		/// <summary>
		///  事件：点击《保存素材》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveMaterialSkinButton_Click(object sender, EventArgs e)
		{
			LightAst lightAst = lightAstList[selectedIndex];
			MaterialSaveForm materialForm = new MaterialSaveForm(this, getCurrentLightStepWrapper().StepWrapperList, mode, lightAst.LightName, lightAst.LightType);
			if (materialForm != null && !materialForm.IsDisposed)
			{
				materialForm.ShowDialog();
			}
		}

		#endregion


		#region 调试相关按钮

		/// <summary>
		/// 辅助方法：重新搜索com列表：供启动时及需要重新搜索设备时使用。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshComList()
		{
            // 动态加载可用的dmx512串口列表		
            Thread.Sleep(500);
			SerialPortTools comTools = SerialPortTools.GetInstance();
			comList = comTools.GetDMX512DeviceList();
			comSkinComboBox.Items.Clear();
			if (comList != null && comList.Length > 0)
			{
				foreach (string com in comList)
				{
					comSkinComboBox.Items.Add(com);
				}
				comSkinComboBox.SelectedIndex = 0;
				comSkinComboBox.Enabled = true;
				comChooseSkinButton.Enabled = true;
			}
			else
			{
				comSkinComboBox.Text = "";
				comSkinComboBox.Enabled = false;
				comChooseSkinButton.Enabled = false;
			}
		}
		
		/// <summary>
		/// 事件：点击《刷新Com列表（图标）》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comRefreshSkinButton_Click(object sender, EventArgs e)
		{
			refreshComList();
		}

		/// <summary>
		///  事件：点击《选择调试串口》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comChooseSkinButton_Click(object sender, EventArgs e)
		{
			playTools = PlayTools.GetInstance();
			comName = comSkinComboBox.Text;
			if (!comName.Trim().Equals(""))
			{
				playPanel.Show();
			}
			else
			{
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
			// 如果还没连接（按钮显示为“连接设备”)，那就连接
			if (!isConnected)
			{
				connectSkinButton.Image = global::LightController.Properties.Resources.断开连接;
				connectSkinButton.Text = "断开连接";				

				playTools.ConnectDevice(comName);

				showConnectedButtons(true);
			}
			else //否则( 按钮显示为“断开连接”）断开连接
			{
				connectSkinButton.Image = global::LightController.Properties.Resources.连接;
				connectSkinButton.Text = "连接设备";				
								
				playTools.CloseDevice();

				previewSkinButton.Image = global::LightController.Properties.Resources.浏览效果前;
				showConnectedButtons(false);
			}
		}

		/// <summary>
		///  辅助方法：选择串口按钮、刷新串口按钮、调试的按钮组是否显示
		/// </summary>
		/// <param name="v"></param>
		private void showConnectedButtons(bool connected)
		{
			// 左上角的《串口列表》《刷新串口列表》可用与否，与下面《各调试按钮》是否可用刚刚互斥
			comChooseSkinButton.Enabled = !connected;
			comRefreshSkinButton.Enabled = !connected;

			keepSkinButton.Visible = connected;
			realtimeSkinButton.Visible = connected;
			oneLightOneStepSkinButton.Visible = connected;
			makeSoundSkinButton.Visible = connected;
			previewSkinButton.Visible = connected;
			endviewSkinButton.Visible = connected;

			// 是否连接
			isConnected  = connected;
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
		///  事件：点击《单灯单步》
		///  (-- 先设按钮为有颜色，但跑完就立刻恢复正常！)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void oneLightOneStepSkinButton_Click(object sender, EventArgs e)
		{					
			oneLightOneStepSkinButton.Image = global::LightController.Properties.Resources.单灯单步后;
			this.Refresh();

			oneLightStepWork();

			oneLightOneStepSkinButton.Image = global::LightController.Properties.Resources.单灯单步;
		}

		/// <summary>
		/// 辅助方法：调用基类的单灯单步发送DMX512帧数据;并操作本类中的相关数据
		/// </summary>
		protected override void oneLightStepWork()
		{
			base.oneLightStepWork();
			previewSkinButton.Image = global::LightController.Properties.Resources.浏览效果前;
		}

		/// <summary>
		///  事件：点击《预览效果》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewSkinButton_Click(object sender, EventArgs e)
		{
			previewSkinButton.Image = global::LightController.Properties.Resources.浏览效果后;

			// 设为false，从内存取数据
			DBWrapper allData = GetDBWrapper(false);
			try
			{
				playTools.PreView(allData, globalIniPath, frame);
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
			// 1.几个按钮图标设置
			oneLightOneStepSkinButton.Image = global::LightController.Properties.Resources.单灯单步;
			makeSoundSkinButton.Image = global::LightController.Properties.Resources.触发音频;
			previewSkinButton.Image = global::LightController.Properties.Resources.浏览效果前;

			// 2.调用结束预览方法
			playTools.EndView();
		}
		
		/// <summary>
		///  事件：《（曾维佳）测试按钮组》点击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newTestButton_Click(object sender, EventArgs e)
		{
			int buttonIndex = MathAst.GetIndexNum(((Button)sender).Name, 0);
			Console.WriteLine(buttonIndex);
			Test test = new Test(GetDBWrapper(true));
			test.Start(buttonIndex);
		}

		#endregion

		/// <summary>
		/// 事件：界面的Size发生变化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SkinMainForm_SizeChanged(object sender, EventArgs e)
		{
			tdSkinFlowLayoutPanel.AutoScrollPosition = new Point(0, 0);			;
		}

		/// <summary>
		/// 事件：点击后进入《多灯模式|单灯模式》，
		/// 一.多灯模式：
		///		0.至少选择两个灯具，才能使用多灯模式
		///		1.判断所有选中的灯，是否同类型；若选中的不是同类型的灯无法进入此模式(直接return)
		///		2.若是同类型的，应选择其中之一作为编组的组长（其他灯直接使用此灯的数据 ：先复制组长的数据，然后后台直接粘贴到其余灯具上面）
		///		3.之后每次编辑灯具，都是编辑组内的所有数据 （包括添加步、删除步，步调节等）
		///		4.下面的调试按钮中"单灯单步"-》“多灯单步”；
		///		5.若是选择其他模式或者场景，应自动恢复《单灯调节》模式 
		/// 二.单灯模式（与单灯刚好是反操作）：
		///		
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiLightSkinButton_Click(object sender, EventArgs e)
		{
			// 进入多灯模式
			if (!isMultiMode) {
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
				foreach (int item in lightsSkinListView.SelectedIndices) {
					selectedIndices.Add(item);
				}				
				new MultiLightForm(this,lightAstList,selectedIndices).ShowDialog();
			}
			// 退出多灯模式（单灯）
			else{					
				lightsAddrLabel.Text = "灯具地址:" + lightAstList[selectedIndex].LightAddr;				
				for(int lightIndex=0; lightIndex < lightWrapperList.Count; lightIndex++)
				{					
					lightsSkinListView.Items[lightIndex].BackColor = Color.White;
				}
				enableSingleMode(true);
			}	
		}

		/// <summary>
		///  9.16 辅助方法：进入《多灯模式》
		/// </summary>
		/// <param name="selectedIndex"></param>
		public override void EnterMultiMode(int selectedIndex)
		{
			// 基类中统一的处理
			base.EnterMultiMode(selectedIndex);

			// 以下为单独针对本Form的方法：			
			lightsAddrLabel.Text = "灯具地址列表：";			
			foreach (int lightIndex in selectedIndices)
			{
				lightsAddrLabel.Text += lightAstList[lightIndex].LightAddr + " ";
				lightsSkinListView.Items[lightIndex].BackColor = Color.SkyBlue;
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
			lightsSkinListView.Enabled = isSingleMode ;

			frameSkinComboBox.Enabled = isSingleMode;
			modeSkinComboBox.Enabled = isSingleMode;


			if (isSingleMode)
			{
				oneLightOneStepSkinButton.Text = "单灯单步";
				multiLightSkinButton.Text = "多灯模式";
			}
			else {
				oneLightOneStepSkinButton.Text = "多灯单步";
				multiLightSkinButton.Text = "单灯模式";
			}
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
			for(int i=1; i< lightsSkinListView.SelectedItems.Count; i++ ) // 从第二个选中灯具开始比对
			{
				string tempTag = lightsSkinListView.SelectedItems[i].Tag.ToString();
				if (!firstTag.Equals(tempTag)) {
					result = false;
					break;
				}
			}
			return result;					   			 		  			
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
			
			for (int i = 0; i < lightsSkinListView.Items.Count; i++)
			{
				Console.WriteLine(i + " :: " + lightsSkinListView.Items[i].Text);
			}
		}

		/// <summary>
		/// 事件：勾选《保持其它灯状态》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void keepSkinButton_Click(object sender, EventArgs e)
		{
			// 默认情况下，《保持其它灯状态》还没打开，点击后设为打开状态（文字显示为关闭实时调试，图片加颜色）
			if (!isKeepOtherLights)
			{
				keepSkinButton.Image = global::LightController.Properties.Resources.实时调试;
				keepSkinButton.Text = "取消保持状态";
				isKeepOtherLights = true;
			}
			else //否则( 按钮显示为“保持其他灯状态”）断开连接
			{
				keepSkinButton.Image = global::LightController.Properties.Resources.实时调试02;
				keepSkinButton.Text = "保持其他灯状态";
				isKeepOtherLights = false;
			}
		}
		

		/// <summary>		
		///  事件：点击《复制多步》：弹出类似于保存素材的form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiCopySkinButton_Click(object sender, EventArgs e)
		{
			MultiStepCopyForm mscForm = new MultiStepCopyForm(this, getCurrentLightStepWrapper().StepWrapperList, mode, selectedLightName,getCurrentStep());
			if (mscForm != null && !mscForm.IsDisposed)
			{
				mscForm.ShowDialog();
			}
		}

		/// <summary>	
		///  事件：点击《复制多步》：弹出类似于保存素材的form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiPasteSkinButton_Click(object sender, EventArgs e)
		{
			if (TempMaterialAst == null)
			{
				MessageBox.Show("还未复制多步，无法粘贴。");
				return;
			}
			if (TempMaterialAst.Mode != mode) {
				MessageBox.Show("复制的多步与当前模式不同，无法粘贴。");
				return; 
			}
			new MultiStepPasteForm(this).ShowDialog();
		}

		/// <summary>
		///  事件：点击《调用其他场景》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void useFrameSkinButton_Click(object sender, EventArgs e)
		{
			new UseFrameForm( this,frame).ShowDialog(); 
		}


		#region  灯具listView可移动位置并保存
		//MARK：灯具listView位置相关

		private  Point startPoint = Point.Empty;	

		// 这个别忘了
		// listView1.AllowDrop = true;
		// listView1.AutoArrange = false;

		private double getVector(Point pt1, Point pt2) // 获取两点间的距离
		{
			var x = Math.Pow((pt1.X - pt2.X), 2);
			var y = Math.Pow((pt1.Y - pt2.Y), 2);
			return Math.Abs(Math.Sqrt(x - y));
		}

		private void lightsSkinListView_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem[])))
				e.Effect = DragDropEffects.All;
		}

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

		private void lightsSkinListView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				startPoint = e.Location;
		}

		private void lightsSkinListView_MouseMove(object sender, MouseEventArgs e)
		{
			if (lightsSkinListView.SelectedItems.Count == 0)
				return;

			if (e.Button == MouseButtons.Left)
			{
				var vector = getVector(startPoint, e.Location);
				if (vector < 10) return;

				var data = lightsSkinListView.SelectedItems.OfType<ListViewItem>().ToArray();

				lightsSkinListView.DoDragDrop(data, DragDropEffects.All);
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
			lightsSkinListView.AllowDrop = ! isAutoArrange;
			lightsSkinListView.AutoArrange = isAutoArrange;

			if ( isAutoArrange)
			{
				enableSLArrange(false, false);
			}
			else {				
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
			if ( isAutoArrange ) {
				MessageBox.Show("在自动排列模式下，无法保存灯具位置，请取消勾选后重新保存。");
				return;
			}

			// 2.判断当前是否已打开工程(arrangeIniPath不为空）
			if (String.IsNullOrEmpty(arrangeIniPath)) {
				MessageBox.Show("当前尚未新建或打开工程，无法保存灯具位置。");
				return;
			}

			// 3.判断灯具数量是否为空
			if (lightAstList == null || lightAstList.Count == 0) {
				MessageBox.Show("当前工程尚无灯具，无法保存灯具位置，请添加灯具后重新保存。");
				return;
			}

			// 4.保存操作
			IniFileAst iniFileAst = new IniFileAst(arrangeIniPath);
			iniFileAst.WriteInt("Common", "Count", lightsSkinListView.Items.Count);
			for(int i = 0; i< lightsSkinListView.Items.Count; i++)
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
			//TODO：读取灯具位置
			// 1.先验证ini文件是否存在
			if (!File.Exists(arrangeIniPath)) {
				MessageBox.Show("未找到灯具位置文件，无法读取。");
				return;
			}

			//2.验证灯具数目是否一致
			IniFileAst iniFileAst = new IniFileAst(arrangeIniPath);
			int lightCount = iniFileAst.ReadInt("Common", "Count", 0);
			if (lightCount == 0) {
				MessageBox.Show("灯具位置文件的灯具数量为0，此文件无实际效果。");
				return;
			}
			
			//3. 验证灯具数量是否一致
			if ( lightCount != lightsSkinListView.Items.Count)
			{
				MessageBox.Show("灯具位置文件的灯具数量与当前工程的灯具数量不匹配，无法读取位置。");
				//TODO: 灯具数量不匹配，如何处理？
				return;
			}

			// 4.开始读取并绘制		
			//MARK : 特别奇怪的一个地方，在选择自动排列再去掉自动排列后，必须要先设一个不同的position，才能让 读取到的position真正给到items[i].Position?
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



		#endregion

		/// <summary>
		/// 事件：点击《跳转步》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chooseStepSkinButton_Click(object sender, EventArgs e)
		{
			int step = Decimal.ToInt16(chooseStepNumericUpDown.Value);
			chooseStep(step);
		}
	}
}
