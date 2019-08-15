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

namespace LightController.MyForm
{

	public partial class NewMainForm : MainFormInterface
	{

		private PlayTools playTools;
		public NewMainForm()
		{
			InitializeComponent();
			
			#region 初始化各种选项及容纳数组

			frameSkinComboBox.Items.AddRange(new object[]
			{		"标准",
					"动感",
					"商务",
					"抒情",
					"清洁",
					"柔和",
					"激情",
					"明亮",
					"浪漫",
					"演出",
					"暂停",
					"全关",
					"全开",
					"全开关",
					"电影",
					"备用1",
					"备用2",
					"备用3",
					"备用4",
					"备用5",
					"备用6",
					"摇麦",
					"喝彩",
					"倒彩"});
			frameSkinComboBox.SelectedIndex = 0;
			modeSkinComboBox.Items.AddRange(new object[] {
					"常规模式","音频模式"
			});
			modeSkinComboBox.SelectedIndex = 0;

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

			#endregion

			modeSkinComboBox.SelectedIndex = 0;
			frameSkinComboBox.SelectedIndex = 0;

			isInit = true;

		}
			   		

		private void NewMainForm_Load(object sender, EventArgs e)
		{
			//this.lightSkinListView.Scrollable = false;
			//ShowScrollBar((int)this.lightSkinListView.Handle, SB_HORZ, 1);			
		}

		
		/// <summary>
		///  点击《连接设备|断开连接》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectSkinButton_Click(object sender, EventArgs e)
		{			
			// 如果还没连接（按钮显示为“连接设备”)，那就连接
			if (!isConnect)
			{				
				connectSkinButton.Image  = global::LightController.Properties.Resources.断开连接;
				connectSkinButton.Text = "断开连接";
				showViewButtons(true);

				playTools = PlayTools.GetInstance();
				playTools.ConnectDevice();

				isConnect = true;

			}
			else //否则( 按钮显示为“断开连接”）断开连接
			{
				connectSkinButton.Image = global::LightController.Properties.Resources.连接;
				connectSkinButton.Text = "连接设备";
				showViewButtons(false);

				playTools.EndView();
				playTools = null;

				isConnect = false;
			}
		}

		/// <summary>
		///  辅助方法：调试的按钮组是否显示
		/// </summary>
		/// <param name="v"></param>
		private void showViewButtons(bool visible)
		{
			realtimeSkinButton.Visible = visible;
			oneLightOneStepSkinButton.Visible = visible;
			makeSoundSkinButton.Visible = visible;
			previewSkinButton.Visible = visible;
			endviewSkinButton.Visible = visible;
		}

		
		/// <summary>
		/// 点击《实时调试》按钮
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
		///  点击《单灯单步》，先设为有颜色，但跑完就立刻恢复正常！
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void oneLightOneStepSkinButton_Click(object sender, EventArgs e)
		{
			// 1.先把预览效果关闭
			previewSkinButton.Image = global::LightController.Properties.Resources.浏览效果前;

			// 2.开始单灯单步效果
			oneLightOneStepSkinButton.Image = global::LightController.Properties.Resources.单灯单步后;			
			this.Refresh();
			//TODO			
			
			oneLightOneStepSkinButton.Image = global::LightController.Properties.Resources.单灯单步;
		}


		/// <summary>
		///  点击《预览效果》后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewSkinButton_Click(object sender, EventArgs e)
		{
			previewSkinButton.Image = global::LightController.Properties.Resources.浏览效果后;			
			// TODO


		}

		/// <summary>
		///  点击《结束预览》后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void endviewSkinButton_Click(object sender, EventArgs e)
		{

			oneLightOneStepSkinButton.Image = global::LightController.Properties.Resources.单灯单步;
			makeSoundSkinButton.Image = global::LightController.Properties.Resources.触发音频;
			previewSkinButton.Image = global::LightController.Properties.Resources.浏览效果前;			
			// TODO


		}

		/// <summary>
		///  点击《触发音频》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void makeSoundSkinButton_Click(object sender, EventArgs e)
		{
			
		}





		/// <summary>
		///  辅助方法:
		/// 1.通过Location.X==3,判断在第一列位置的tdPanel的数量，从而决定要显示几个labelPanel
		/// 2.当通道列表的滚动条滚动时，labelPanel也随着滚动。
		/// </summary>		
		private void scrollLabelPanel()
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
			labelFlowLayoutPanel.AutoScrollPosition = new Point(0,  - tdSkinFlowLayoutPanel.AutoScrollPosition.Y);
		}

		private void showLabelPanels(int j)
		{
			switch (j) {
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
		///   tdSkinFlowLayoutPanel的重绘事件
		///  </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdSkinFlowLayoutPanel_Paint(object sender, PaintEventArgs e)
		{
			//Console.WriteLine(tdSkinFlowLayoutPanel);
			scrollLabelPanel();
		}

		/// <summary>
		///  点击《新建工程》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newSkinButton_Click(object sender, EventArgs e)
		{
			NewForm newForm = new NewForm(this);
			newForm.Show();
		}


		/// <summary>
		///  初始化一些参数
		/// </summary>
		/// <param name="projectName"></param>
		/// <param name="isNew"></param>
		public override void InitProject(string projectName, bool isNew)
		{
			base.InitProject(projectName, isNew);

			enableGlobalSet(true);
			enableSave(true);
		}

		/// <summary>
		///  这个方法，通过打开已有的工程，来加载各种数据到mainForm中
		/// data.db3的载入：把相关内容，放到数据列表中
		///    ①lightList 、stepCountList、valueList
		///    ②lightAstList（由lightList生成）
		///    ③lightWrapperList(由lightAstList生成)
		/// </summary>
		/// <param name="directoryPath"></param>
		public override void OpenProject(string projectName)
		{
			// 0.初始化
			InitProject(projectName, false);

			// 把数据库的内容填充进来，并设置好对应的DAO
			dbLightList = getLightList();
			dbStepCountList = getStepCountList();
			dbValueList = getValueList();
			// 通过lightList填充lightAstList
			lightAstList = reCreateLightAstList(dbLightList);
			AddLightAstList(lightAstList);

			// 针对每个lightWrapper，获取其已有步数的场景和模式
			for (int lightListIndex = 0; lightListIndex < dbLightList.Count; lightListIndex++)
			{
				IList<DB_StepCount> scList = stepCountDAO.getStepCountList(dbLightList[lightListIndex].LightNo);

				if (scList != null && scList.Count > 0)
				{
					// 只要有步数的，优先生成StepMode
					StepWrapper stepMode = generateStepMode(lightAstList[lightListIndex]);
					lightWrapperList[lightListIndex].StepMode = stepMode;
					foreach (DB_StepCount sc in scList)
					{
						int frame = sc.PK.Frame;
						int mode = sc.PK.Mode;
						int lightIndex = sc.PK.LightIndex;
						int stepCount = sc.StepCount;

						for (int step = 1; step <= stepCount; step++)
						{
							IList<DB_Value> stepValueList = valueDAO.getStepValueList(lightIndex, frame, mode, step);
							StepWrapper stepWrapper = StepWrapper.GenerateStepWrapper(stepMode, stepValueList, mode);
							if (lightWrapperList[lightListIndex].LightStepWrapperList[frame, mode] == null)
							{
								lightWrapperList[lightListIndex].LightStepWrapperList[frame, mode] = new LightStepWrapper();
							}
							lightWrapperList[lightListIndex].LightStepWrapperList[frame, mode].AddStep(stepWrapper);
						}
					}
				}
			}
			isInit = true;
			MessageBox.Show("成功打开工程");
		}




		/// 点击《打开工程》按钮 ==》新建一个OpenForm，再在里面回调OpenProject()
		private void openSkinButton_Click(object sender, EventArgs e)
		{
			OpenForm openForm = new OpenForm(this);
			openForm.ShowDialog();			
		}

		/// <summary>
		///  辅助方法：《保存工程》Enabled设为选定值
		/// </summary>
		protected override void enableSave(bool enable)
		{
			saveSkinButton.Enabled = enable;
		}

		/// <summary>
		///  辅助方法：将所有全局配置相关的按钮（灯具、升级、全局、摇麦、网络、连接设备）Enabled设为选定值
		/// </summary>
		/// <param name="v"></param>
		protected override void enableGlobalSet(bool enable)
		{			
			// 菜单栏几个按钮
			updateSkinButton.Enabled = enable;
			lightListSkinButton.Enabled = enable;
			globalSetSkinButton.Enabled = enable;
			ymSkinButton. Enabled = enable;
			networkSkinButton.Enabled = enable;

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

			//TODO   单独的针对本Form的处理代码：listView更新为最新数据

			// 1.清空lightListView,重新填充新数据
			lightsSkinListView.Items.Clear();
			for (int i = 0; i < lightAstList2.Count; i++)
			{
				// 添加灯具数据到LightsListView中
				lightsSkinListView.Items.Add(new ListViewItem(
					lightAstList2[i].LightName + ":" + lightAstList2[i].LightType,
					lightAstList2[i].LightPic
				));
			}

			// 2.最后处理通道显示：每次调用此方法后应该隐藏通道数据，避免误操作。
			hideAllTongdao();

		}


		/// <summary>
		///  点击《硬件设置》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hardwareSetSkinButton_Click(object sender, EventArgs e)
		{
			HardwareSetChooseForm hscForm = new HardwareSetChooseForm(this);
			hscForm.ShowDialog();
		}

		/// <summary>
		/// 点击《在线升级》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateSkinButton_Click(object sender, EventArgs e)
		{
			bool isFromDB = true; 
			UpdateForm updateForm = new UpdateForm(this, GetDBWrapper(isFromDB), globalIniFilePath);
			updateForm.ShowDialog();
		}

		/// <summary>
		///  勾选《（是否）使用模板生成步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addStepCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			isUseStepMode = addStepCheckBox.Checked;
		}

		/// <summary>
		///  更改《选择场景》选项后
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
		/// 更改《选择模式》选项后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void modeSkinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isInit)
			{
				mode = modeSkinComboBox.SelectedIndex;
				// 若模式为声控模式
				// 1.改变几个label的Text; 
				// 2.改变跳变渐变-->是否声控；
				// 3.所有步时间值的调节，改为enabled=false			
				if (mode == 1)
				{
					secondLabel1.Text = "是否声控";
					secondLabel2.Text = "是否声控";
					secondLabel3.Text = "是否声控"; 
					for (int i = 0; i < 32; i++)
					{
						this.tdChangeModeSkinComboBoxes[i].Items.Clear();
						this.tdChangeModeSkinComboBoxes[i].Items.AddRange(new object[] {"否","是"});
						this.tdStepTimeNumericUpDowns[i].Enabled = false;
					}

					//changeModeButton.Text = "统一声控";

					//cmComboBox.Items.Clear();
					//cmComboBox.Items.AddRange(new object[] {"否",	"是"}	);
					//cmComboBox.SelectedIndex = 0;
				}
				else //mode=0
				{
					secondLabel1.Text = "变化方式";
					secondLabel2.Text = "变化方式";
					secondLabel3.Text = "变化方式"; 
					for (int i = 0; i < 32; i++)
					{
						this.tdChangeModeSkinComboBoxes[i].Items.Clear();
						this.tdChangeModeSkinComboBoxes[i].Items.AddRange(new object[] {
								"跳变",
								"渐变",
								"屏蔽"
						});
						this.tdStepTimeNumericUpDowns[i].Enabled = true;
					}

					//changeModeButton.Text = "统一跳渐变";
					//cmComboBox.Items.Clear();
					//cmComboBox.Items.AddRange(new object[] {	"跳变","渐变"});
					//cmComboBox.SelectedIndex = 0;
			}
				if (lightAstList != null && lightAstList.Count > 0)
				{
					changeFrameMode();
				}
			}
		}


		/// <summary>
		///  改变了模式和场景后的操作
		/// </summary>
		private void changeFrameMode()
		{
			LightWrapper lightWrapper = lightWrapperList[selectedLightIndex];
			LightStepWrapper lightStepWrapper = lightWrapper.LightStepWrapperList[frame, mode];

			// 为空或StepList数量是0
			if (lightStepWrapper == null || lightStepWrapper.StepWrapperList.Count == 0)
			{
				hideAllTongdao();
				showStepLabel(0, 0);
			}
			else // lightStepWrapper != null && lightStepWrapper.StepList.Count>0 : 也就是已经有值了
			{
				int currentStep = lightStepWrapper.CurrentStep;
				int totalStep = lightStepWrapper.TotalStep;

				StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[currentStep - 1];								
				ShowVScrollBars(stepWrapper.TongdaoList, stepWrapper.StartNum);
				showStepLabel(currentStep, totalStep);
			}
		}

		/// <summary>
		/// 隐藏所有通道:包含五个部分 		
		/// </summary>
		private void hideAllTongdao()
		{
			for (int i = 0; i < 32; i++)
			{
				tdPanels[i].Hide();
			}
		}

		/// <summary>
		///  辅助方法：用来显示stepLabel-->当前步/总步数
		/// 7.2 +隐藏《删除步》按钮
		/// </summary>
		/// <param name="currentStep"></param>
		/// <param name="totalStep"></param>
		private  void showStepLabel(int currentStep, int totalStep)
		{
			// 1. 设label的Text值
			stepLabel.Text = currentStep + "/" + totalStep;
			
			// 2.1 设定《删除步》按钮是否可用
			deleteStepSkinButton.Enabled = totalStep != 0;

			// 2.2 设定《追加步》、《前插入步》《后插入步》按钮是否可用			
			bool insertEnabled =  (mode == 0 && totalStep < 32) || ( mode == 1 && totalStep < 48 );
			addStepSkinButton.Enabled = insertEnabled;				
			insertAfterSkinButton.Enabled = insertEnabled;			
			insertBeforeSkinButton.Enabled = insertEnabled &&  currentStep > 0;

			// 2.3 设定《上一步》《下一步》是否可用
			// -- 7.19修改为循环使用步数：
			backStepSkinButton .Enabled = totalStep > 1;		
			nextStepSkinButton.Enabled = totalStep >1 ;

			//2.4 设定《复制步》是否可用
			copyStepSkinButton.Enabled = currentStep > 0;
			pasteStepSkinButton.Enabled = currentStep > 0 && tempStep != null; 
		}


		/// <summary>
		/// 通过传来的数值，生成通道列表的数据
		/// </summary>
		/// <param name="tongdaoList"></param>
		/// <param name="startNum"></param>
		private void ShowVScrollBars(List<TongdaoWrapper> tongdaoList, int startNum)
		{

			// 1.每次更换灯具，都先清空通道
			hideAllTongdao();

			// 2.判断tongdaoList，为null或数量为0时，设定deleteStepButton键不可用，并退出此方法
			if (tongdaoList == null || tongdaoList.Count == 0)
			{
				return;
			}
			//3.将dataWrappers的内容渲染到起VScrollBar中
			else
			{
				for (int i = 0; i < tongdaoList.Count; i++)
				{
					TongdaoWrapper tongdaoWrapper = tongdaoList[i];

					tdNoLabels[i].Text = "通道" + (startNum + i);
					tdNameLabels[i].Text = tongdaoWrapper.TongdaoName;
					tdSkinTrackBars[i].Value = tongdaoWrapper.ScrollValue;
					tdValueNumericUpDowns[i].Text = tongdaoWrapper.ScrollValue.ToString();				
					tdChangeModeSkinComboBoxes[i].SelectedIndex = tongdaoWrapper.ChangeMode;
					tdStepTimeNumericUpDowns[i].Text = tongdaoWrapper.StepTime.ToString();

					tdPanels[i].Show();

					// 8.8: 用changeModeComboBoxes[i].SelectedIndex==2的值来调节几个通道相关的数值是否可以编辑
					//enableTongdaoEdit( i, changeModeComboBoxes[i].SelectedIndex == 2);
				}				
			}
		}

	


		/// <summary>
		///  改变选中的灯时进行的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsSkinListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 必须判断这个字段(Count)，否则会报异常
			if (lightsSkinListView.SelectedIndices.Count > 0)
			{
				selectedLightIndex = lightsSkinListView.SelectedIndices[0];
				generateLightData();
				// 这里主要是控制pasteLightButton的Enabled值
				//checkIfCanCopyLight();
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
			LightAst lightAst = lightAstList[selectedLightIndex];

			//TODO 
			// 1.在右侧灯具信息内显示选中灯具相关信息

			currentLightPictureBox.Image = Image.FromFile(@"C:\Temp\LightPic\" + lightAst.LightPic);
			lightNameSkinLabel.Text = "灯具厂商："+lightAst.LightName;
			lightTypeSkinLabel.Text	 = "灯具型号：" + lightAst.LightType;
			lightAddrSkinLabel.Text = "灯具地址：" + lightAst.LightAddr;

			
			//2.判断是不是已经有stepMode了
			// ①若无，则生成数据，并hideAllTongdao 并设stepLabel为“0/0” --> 因为刚创建，肯定没有步数	
			// ②若有，还需判断该LightData的LightStepWrapperList[frame,mode]是不是为null
			//			若是null，则说明该FM下，并未有步数，hideAllTongdao
			//			若不为null，则说明已有数据，
			LightWrapper lightWrapper = lightWrapperList[selectedLightIndex];

			if (lightWrapper.StepMode == null)
			{
				lightWrapper.StepMode = generateStepMode(lightAst);
				showStepLabel(0, 0);
				hideAllTongdao();
			}
			else
			{
				changeFrameMode();
			}
			stepSkinPanel.Enabled = true;
		}



		#region 一些临时方法：用于测试等

		private int i = 31;
		/// <summary>
		///  临时测试方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, EventArgs e)
		{
			if (i >= 0)
			{
				Console.WriteLine(tdPanels[i].Visible);
				Console.WriteLine("qan:" + tdPanels[i].Name + " : " + tdPanels[i].Location);
				tdPanels[i].Hide();
				//tdNoLabels[i].Hide();
				//tdNameLabels[i].Hide();
				//tdSkinTrackBars[i].Hide();
				//tdValueNumericUpDowns[i].Hide();
				//tdChangeModeSkinComboBoxes[i].Hide();
				//tdStepTimeNumericUpDowns[i].Hide();				
				Console.WriteLine(tdPanels[i].Visible);
				Console.WriteLine("hou:" + tdPanels[i].Name + " : " + tdPanels[i].Location);
				i--;
			}


			//Console.WriteLine(button1.Location);
		}

		#endregion

		/// <summary>
		///  点击《上一步》：先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backStepSkinButton_Click(object sender, EventArgs e)
		{
			int currentStepValue = getCurrentStepValue();
			if (currentStepValue > 1)
			{
				chooseStep(currentStepValue - 1);
			}
			else
			{
				chooseStep(getTotalStepValue());
			}
		}

		/// <summary>
		///  点击《下一步》：先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nextStepSkinButton_Click(object sender, EventArgs e)
		{
			int currentStepValue = getCurrentStepValue();
			int totalStepValue = getTotalStepValue();
			if (currentStepValue < totalStepValue)
			{
				chooseStep(currentStepValue + 1);
			}
			else
			{
				chooseStep(1);
			}
		}
		
		/// <summary>
		/// 辅助方法：抽象了【选择某一个指定步数后，统一的操作；NextStep和BackStep等应该都使用这个方法】
		/// </summary>
		private void chooseStep(int stepValue)
		{
			LightStepWrapper lightStepWrapper = getCurrentLightStepWrapper();
			StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[stepValue - 1];
			lightStepWrapper.CurrentStep = stepValue;

			this.ShowVScrollBars(stepWrapper.TongdaoList, stepWrapper.StartNum);
			this.showStepLabel(lightStepWrapper.CurrentStep, lightStepWrapper.TotalStep);

			//if (isRealtime)
			//{
			//	oneLightStepWork();
			//}
		}

		/// <summary>
		/// 插入步(前插或后插由触发键的Name决定)的操作：前插和后插都调用同一个方法
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
					(isUseStepMode || getCurrentStepWrapper() == null) ? getCurrentStepMode() : getCurrentStepWrapper(),
					mode
				);
				// 要插入的位置的index
				int stepIndex = getCurrentStepValue() - 1;
				// 插入的方式：前插(true）还是后插（false)
				bool insertBefore = ((Button)sender).Name.Equals("insertBeforeSkinButton");

				lsWrapper.InsertStep(stepIndex, newStep, insertBefore);

				this.ShowVScrollBars(newStep.TongdaoList, newStep.StartNum);
				this.showStepLabel(lsWrapper.CurrentStep, lsWrapper.TotalStep);
			}
			else
			{
				MessageBox.Show("Dickov:当前步大于总步数");
			}
		}

		/// <summary>
		/// 点击《追加步》
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
			StepWrapper newStep = StepWrapper.GenerateNewStep	(
				(isUseStepMode || getTotalStepValue() == 0) ? getCurrentStepMode() : getCurrentLightMaxStepWrapper(), 
				mode
				);

			// 调用包装类内部的方法,来追加步
			currentLightWrapper.LightStepWrapperList[frame, mode].AddStep(newStep);

			// 显示新步
			this.ShowVScrollBars(newStep.TongdaoList, newStep.StartNum);
			this.showStepLabel(currentLightWrapper.LightStepWrapperList[frame, mode].CurrentStep, currentLightWrapper.LightStepWrapperList[frame, mode].TotalStep);
		}


		/// <summary>
		///  删除步的操作
		///  1.获取当前步，当前步对应的stepIndex
		///  2.通过stepIndex，DeleteStep(index);
		///  3.获取新步(step删除后会自动生成新的)，并重新渲染stepLabel和vScrollBars
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteStepSkinButton_Click(object sender, EventArgs e)
		{

			LightStepWrapper lightStepWrapper = getCurrentLightStepWrapper();
			int stepIndex = getCurrentStepValue() - 1;

			// 调用包装类内部的方法:删除某一步
			try
			{
				lightStepWrapper.DeleteStep(stepIndex);
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
				this.ShowVScrollBars(stepWrapper.TongdaoList, stepWrapper.StartNum);
				this.showStepLabel(lightStepWrapper.CurrentStep, lightStepWrapper.TotalStep);
			}
			else
			{
				this.ShowVScrollBars(null, 0);
				this.showStepLabel(0, 0);
			}
		}

		/// <summary>
		///  点击《保存项目》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveSkinButton_Click(object sender, EventArgs e)
		{
			saveAll();
		}


		/// <summary>
		/// 点击《复制步》
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
		/// 点击《粘贴步》
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
			ShowVScrollBars(currentStep.TongdaoList, currentStep.StartNum);
		}
			   		 

		/// <summary>
		///  点击《复制灯》：
		///  1.应有个全局变量lightWrapperTemp，记录要被复制的灯的信息
		///  2. 将当前选中灯具的内容，赋予lightWrapperList
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void copyLightSkinButton_Click(object sender, EventArgs e)
		{
			if (getCurrentLightWrapper() == null)
			{
				MessageBox.Show("未选中灯，无法复制");
				return;
			}
			tempLight = getCurrentLightWrapper();
		}




		/// <summary>
		///  点击《粘贴灯》
		///  1. 比对选中灯和复制的灯
		///	--①不一致，弹错误
		///	--②一致，想办法把tempLight的数据复制到选中灯中
		/// 2. generateLightData()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pasteLightSkinButton_Click(object sender, EventArgs e)
		{
			// 多加了一层常规情况下不会出现的判断，因为此时这个按钮不可用
			if (checkIfCanCopyLight())
			{
				LightWrapper selectedLight = getCurrentLightWrapper();
				lightWrapperList[selectedLightIndex] = LightWrapper.CopyLight(tempLight, selectedLight);
				generateLightData();
			}
			else
			{
				//一般不会进到这里来，因为当checkIfCanCopy=false时，此按钮不可以点击
				MessageBox.Show("选中灯具与要复制的灯具种类不同,无法复制!");
			}
		}


		/// <summary>
		///  辅助方法：检查是否可以复制灯
		/// </summary>
		private bool checkIfCanCopyLight()
		{
			pasteLightSkinButton.Enabled = false;
			LightWrapper selectedLight = getCurrentLightWrapper();
			// 只有在选中灯不为空 且 要被复制的灯与选中灯是同一种灯具时，才能复制
			if (selectedLight != null && tempLight != null)
			{
				if (tempLight.StepMode.LightFullName == selectedLight.StepMode.LightFullName)
				{
					pasteLightSkinButton.Enabled = true;
					return true;
				}
			}
			return false;
		}

		/// <summary>
		///  点击使用《素材》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void useMaterialSkinButton_Click(object sender, EventArgs e)
		{
			MaterialUseForm materialUseForm = new MaterialUseForm(this, mode);
			materialUseForm.ShowDialog();
		}

		/// <summary>
		/// 辅助方法:调用素材
		/// </summary>
		/// <param name="materialAst"></param>
		/// <param name="method"></param>
		public override void InsertOrCoverMaterial(MaterialAst materialAst, MaterialUseForm.InsertMethod method)
		{
			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			int totalStep = lsWrapper.TotalStep;
			int currentStep = lsWrapper.CurrentStep;
			int addStepCount = materialAst.Step;

			// 选择插入时的操作，
			if (method == MaterialUseForm.InsertMethod.INSERT)
			{
				int finalStep = addStepCount + totalStep;
				if ((mode == 0 && finalStep > 32) || (mode == 1 && finalStep > 48))
				{
					MessageBox.Show("素材步数超过当前模式剩余步数，无法调用");
					return;
				}
				StepWrapper stepMode = getCurrentStepMode();
				IList<MaterialIndexAst> sameTDIndexList = getSameTDIndexList(materialAst.TdNameList, stepMode.TongdaoList);
				if (sameTDIndexList.Count == 0)
				{
					MessageBox.Show("该素材与当前灯具不匹配，无法调用");
					return;
				}
				else
				{
					StepWrapper newStep = null;
					for (int stepIndex = 0; stepIndex < addStepCount; stepIndex++)
					{
						newStep = StepWrapper.GenerateNewStep(stepMode, mode);
						// 改造下newStep,将素材值赋给newStep 
						changeStepFromMaterial(materialAst.TongdaoList, stepIndex, sameTDIndexList, newStep);
						// 使用后插法：避免当前无数据的情况下调用素材失败
						lsWrapper.InsertStep(lsWrapper.CurrentStep - 1, newStep, false);
					}
					ShowVScrollBars(newStep.TongdaoList, newStep.StartNum);
					showStepLabel(lsWrapper.CurrentStep, lsWrapper.TotalStep);
				}
			}
			// 选择覆盖时的操作
			else
			{
				int finalStep = addStepCount + currentStep;
				if ((mode == 0 && finalStep > 32) || (mode == 1 && finalStep > 48))
				{
					MessageBox.Show("素材步数超过当前模式剩余步数，无法调用；可选择其他位置覆盖");
					return;
				}

				StepWrapper stepMode = getCurrentStepMode();
				IList<MaterialIndexAst> sameTDIndexList = getSameTDIndexList(materialAst.TdNameList, stepMode.TongdaoList);
				if (sameTDIndexList.Count == 0)
				{
					MessageBox.Show("该素材与当前灯具不匹配，无法调用");
					return;
				}
				//覆盖的核心逻辑：
				// 方法1：先找出已有的数据改造之；若没有则添加。实现比较复杂，需考虑多方面情况，不采用。
				// 方法2：①比对 finalStep（currentStep+addStepCount)  和 totalStep值，
				//					若finalStep <=totalStep,无需处理
				//					若finalStep > totalStep,则需addStep，直到totalStep=finalStep
				//				②取出currentStep到finalStep的所有步数（addStepCount数)，用changeStepFromMaterial取代之。
				else
				{
					StepWrapper newStep = null;

					if (totalStep < finalStep)
					{
						for (int i = 0; i < finalStep - totalStep; i++)
						{
							newStep = StepWrapper.GenerateNewStep(stepMode, mode);
							lsWrapper.AddStep(newStep);
						}
					}

					for (int stepIndex = currentStep, materialStepIndex = 0; stepIndex < finalStep; stepIndex++, materialStepIndex++)
					{
						changeStepFromMaterial(materialAst.TongdaoList, materialStepIndex, sameTDIndexList, lsWrapper.StepWrapperList[stepIndex]);
						newStep = lsWrapper.StepWrapperList[stepIndex];
						lsWrapper.CurrentStep = stepIndex + 1;  // 此句可替代为在循环外只执行一次：lsWrapper.CurrentStep=finalStep
					}

					if (newStep != null)
					{
						ShowVScrollBars(newStep.TongdaoList, newStep.StartNum);
						showStepLabel(lsWrapper.CurrentStep, lsWrapper.TotalStep);
					}
				}
			}

		}

		private void saveMaterialSkinButton_Click(object sender, EventArgs e)
		{
			MaterialForm materialForm =  new MaterialForm(this, getCurrentLightStepWrapper().StepWrapperList, mode);
			if (materialForm != null && !materialForm.IsDisposed)
			{
				materialForm.ShowDialog();
			}
		}


		/// <summary>
		/// 点击《灯具列表(编辑)》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightListSkinButton_Click(object sender, EventArgs e)
		{
			LightsForm lightsForm = new LightsForm(this, lightAstList);
			lightsForm.ShowDialog();
		}
	}
}
