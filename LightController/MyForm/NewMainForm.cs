using LightController.Ast;
using LightController.Ast.Enum;
using LightController.Common;
using LightEditor.Ast;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace LightController.MyForm
{
	public partial class NewMainForm : MainFormBase
	{
		#region 此处定义一些全局变量，用以界面风格的统一设置

		private BorderStyle unifyBorderStyle = BorderStyle.Fixed3D; //统一为局内的所有panel设置统一的BorderStyle		
		private Color unifyColor = SystemColors.Window;
		private Color unifyColor2 = SystemColors.Window;
		private bool isUseSkin = false;

		#endregion

		private Panel[] tdPanels = new Panel[32];
		private Label[] tdNoLabels = new Label[32];
		private Label[] tdNameLabels = new Label[32];
		private TrackBar[] tdTrackBars = new TrackBar[32];
		private NumericUpDown[] tdValueNumericUpDowns = new NumericUpDown[32];
		private ComboBox[] tdCmComboBoxes = new ComboBox[32];
		private NumericUpDown[] tdStNumericUpDowns = new NumericUpDown[32];
		private Panel[] saPanels = new Panel[32];

		public NewMainForm()
		{			
			InitializeComponent();
			initGeneralControls(); //几个全局控件的初始化
			
			#region 动态添加32个tdPanel的内容及其监听事件; 动态添加32个saPanel 

			for (int tdIndex = 0; tdIndex < 32; tdIndex++)
			{
				tdPanels[tdIndex] = new Panel
				{
					Name = "tdPanel" + (tdIndex + 1),
					Location = tdPanelDemo.	Location,
					Size = tdPanelDemo.Size,
					Visible = tdPanelDemo.Visible
				};

				tdNoLabels[tdIndex] = new Label
				{
					Name = "tdNoLabel" + (tdIndex + 1),					
					AutoSize = tdNoLabelDemo.AutoSize,
					Location = tdNoLabelDemo.Location,
					Size = tdNoLabelDemo.Size,							
				};

				tdNameLabels[tdIndex] = new Label
				{
					Name = "tdNameLabel" + (tdIndex + 1),
					Font = tdNameLabelDemo.Font,
					Location = tdNameLabelDemo.Location,
					Size = tdNameLabelDemo.Size,					
					TextAlign = tdNameLabelDemo.TextAlign
				};

				tdTrackBars[tdIndex] = new TrackBar
				{
					Name = "tdTrackBar" + (tdIndex + 1),
					AutoSize = tdTrackBarDemo.AutoSize,
					BackColor = tdTrackBarDemo.BackColor,
					Location = tdTrackBarDemo.Location,
					Maximum = tdTrackBarDemo.Maximum,
					Orientation = tdTrackBarDemo.Orientation,
					Size = tdTrackBarDemo.Size,					
					TickFrequency = tdTrackBarDemo.TickFrequency,
					TickStyle = tdTrackBarDemo.TickStyle
				};

				tdValueNumericUpDowns[tdIndex] = new NumericUpDown
				{
					Name = "tdValueNUD" + (tdIndex + 1),
					Font = tdValueNUDDemo.Font,
					Location = tdValueNUDDemo.Location,
					Maximum = tdValueNUDDemo.Maximum,
					Size = tdValueNUDDemo.Size,
					TextAlign = tdValueNUDDemo.TextAlign,
					Tag = 0,
				};

				tdCmComboBoxes[tdIndex] = new ComboBox
				{
					Name = "tdCmComboBox" + (tdIndex + 1),
					FormattingEnabled = tdCmComboBoxDemo.FormattingEnabled,
					Location = tdCmComboBoxDemo.Location,
					Size = tdCmComboBoxDemo.Size,
					DropDownStyle = tdCmComboBoxDemo.DropDownStyle,
					Tag = 1,
				};
				tdCmComboBoxes[tdIndex].Items.AddRange(new object[] {
						LanguageHelper.TranslateWord("跳变"),
						LanguageHelper.TranslateWord("渐变"),
						LanguageHelper.TranslateWord("屏蔽")
				});

				tdStNumericUpDowns[tdIndex] = new NumericUpDown
				{
					Name = "tdStNUD" + (tdIndex + 1),
					Font = tdStNUDDemo.Font,
					Location = tdStNUDDemo.Location,
					Size = tdStNUDDemo.Size,			
					TextAlign = tdStNUDDemo.TextAlign,
					DecimalPlaces = tdStNUDDemo.DecimalPlaces,
					Tag = 2
				};

				tdPanels[tdIndex].Controls.Add(tdNameLabels[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdNoLabels[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdTrackBars[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdValueNumericUpDowns[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdCmComboBoxes[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdStNumericUpDowns[tdIndex]);	

				tdTrackBars[tdIndex].MouseEnter += tdTrackBars_MouseEnter;
				tdTrackBars[tdIndex].MouseWheel += tdTrackBars_MouseWheel;
				tdTrackBars[tdIndex].ValueChanged += tdTrackBars_ValueChanged;

				tdValueNumericUpDowns[tdIndex].MouseEnter += tdValueNumericUpDowns_MouseEnter;
				tdValueNumericUpDowns[tdIndex].MouseWheel += tdValueNumericUpDowns_MouseWheel;
				tdValueNumericUpDowns[tdIndex].ValueChanged += tdValueNumericUpDowns_ValueChanged;
				tdValueNumericUpDowns[tdIndex].KeyPress += unifyTd_KeyPress;

				tdCmComboBoxes[tdIndex].SelectedIndexChanged += tdChangeModeSkinComboBoxes_SelectedIndexChanged;
				tdCmComboBoxes[tdIndex].KeyPress += unifyTd_KeyPress;				

				tdStNumericUpDowns[tdIndex].MouseEnter += tdStepTimeNumericUpDowns_MouseEnter;
				tdStNumericUpDowns[tdIndex].MouseWheel += tdStepTimeNumericUpDowns_MouseWheel;
				tdStNumericUpDowns[tdIndex].ValueChanged += tdStepTimeNumericUpDowns_ValueChanged;
				tdStNumericUpDowns[tdIndex].KeyPress += unifyTd_KeyPress;

				tdNoLabels[tdIndex].Click += tdNameNumLabels_Click;
				tdNameLabels[tdIndex].Click += tdNameNumLabels_Click;

				tdFlowLayoutPanel.Controls.Add(tdPanels[tdIndex]);

				saPanels[tdIndex] = new Panel
				{
					Name = "saPanel" + (tdIndex + 1),
					Location = saPanelDemo.Location,					
					Size = saPanelDemo.Size,
					Margin =saPanelDemo.Margin,
					Visible = true,										
				};
				tdFlowLayoutPanel.Controls.Add(saPanels[tdIndex]);

			}

			#endregion

			//模式选项框
			modeComboBox.Items.AddRange(new object[] {
				LanguageHelper.TranslateWord("常规模式"),
				LanguageHelper.TranslateWord("音频模式")
			});
			modeComboBox.SelectedIndex = 0;
			
			// 几个按钮添加提示
			myToolTip.SetToolTip(copySceneButton, copyFrameNotice);			
			myToolTip.SetToolTip(keepButton,keepNotice);
			myToolTip.SetToolTip(insertButton, insertNotice);
			myToolTip.SetToolTip(appendButton, appendNotice);
			myToolTip.SetToolTip(deleteButton, deleteNotice);
			myToolTip.SetToolTip(backStepButton, backStepNotice);
			myToolTip.SetToolTip(nextStepButton, nextStepNotice);

			#region 皮肤 及 panel样式 相关代码

			setDeepStyle(false);
			isUseSkin = IniHelper.GetParamBool( "useSkin");
			if( isUseSkin ) {
				//加载皮肤列表		
				DirectoryInfo fdir = new DirectoryInfo(Application.StartupPath + "\\irisSkins");
				try
				{
					FileInfo[] file = fdir.GetFiles();
					if (file.Length > 0)
					{
						skinComboBox.Items.Add(LanguageHelper.TranslateWord("浅色皮肤"));
						skinComboBox.Items.Add(LanguageHelper.TranslateWord("深色皮肤"));

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
			}

			#endregion

			// 添加子属性按键组是否显示的菜单
			showSaPanelsToolStripMenuItem.Text = IsShowSaPanels ? "隐藏子属性面板" : "显示子属性面板";

			// 刷新灯具图片列表（从硬盘读取）
			lightsListView.LargeImageList = lightImageList;
			RefreshLightImageList(); //NewMainForm构造函数

			// 处理语言			
			LanguageHelper.InitForm(this);

			isInit = true;
		}

		private void NewMainForm_Load(object sender, EventArgs e)
		{
			// 用以处理大工程时，子属性列表会连在一起的bug；
			foreach (Panel panel in saPanels)
			{
				panel.Hide();
			}

			// 根据之前打开时存在Settings内的数据，设置皮肤
			if (isUseSkin) {
				skinComboBox.SelectedIndex = Properties.Settings.Default.IrisSkinIndex;  // 触发skinComboBox_SelectedIndexChanged事件				
			}	
		}

		/// <summary>
		/// 事件：每次窗口激活后，都StartPreview一次
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewMainForm_Activated(object sender, EventArgs e)
		{
			startPreview(); // NewMainForm_Activated
		}

		/// <summary>
		/// 事件：关闭Form前的操作，在此事件内可取消关闭窗体
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewMainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			formClosing(e);
		}

		/// <summary>
		/// 辅助公用方法：渲染场景选择框
		/// </summary>
		public override void RenderSceneCB() {
			sceneComboBox.SelectedIndexChanged -= sceneComboBox_SelectedIndexChanged;
			sceneComboBox.Items.Clear();			
			foreach (string frame in AllSceneList)
			{
				sceneComboBox.Items.Add(frame);
			}
			sceneComboBox.SelectedIndex = CurrentScene;
			sceneComboBox.SelectedIndexChanged += sceneComboBox_SelectedIndexChanged;
		}

		/// <summary>
		/// 辅助方法：根据传进来的bool值，决定界面是深色系还是浅色系（相应的BorderStyle也发生变化）
		/// </summary>
		/// <param name="deep"></param>
		private void setDeepStyle(bool deep)
		{
			if (deep) {
				unifyColor = Color.FromArgb(166, 173, 189);
				unifyColor2 = Color.FromArgb(232, 235, 241);
				unifyBorderStyle = BorderStyle.FixedSingle;
			} else {
				unifyColor = SystemColors.Window;
				unifyColor2 = SystemColors.Window;
				unifyBorderStyle = BorderStyle.Fixed3D;
			}

			mainMenuStrip.BackColor = unifyColor;
			projectPanel.BackColor = unifyColor;
			lightInfoPanel.BackColor = unifyColor;
			labelPanel.BackColor = unifyColor;
			unifyPanel.BackColor = unifyColor;

			lightsListView.BackColor = unifyColor2;
			tdFlowLayoutPanel.BackColor = unifyColor2;
			foreach (TrackBar item in tdTrackBars)
			{
				item.BackColor = unifyColor2;
			}

			projectPanel.BorderStyle = unifyBorderStyle;
			lightsListView.BorderStyle = unifyBorderStyle;
			lightInfoPanel.BorderStyle = unifyBorderStyle;
			stepBasePanel.BorderStyle = unifyBorderStyle;
			labelPanel.BorderStyle = unifyBorderStyle;
			tdFlowLayoutPanel.BorderStyle = unifyBorderStyle;
			unifyPanel.BorderStyle = unifyBorderStyle;
		}


		#region 菜单栏 - 非工程相关

		/// <summary>
		/// 事件：更换《更换皮肤》选项（直接按选中项更换皮肤）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void skinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{			
			if (!isInit) {
				return;
			}
			
			// 保存用户自选的皮肤到OS注册表中
			Properties.Settings.Default.IrisSkinIndex = skinComboBox.SelectedIndex;
			Properties.Settings.Default.Save();

			// 处理皮肤显示
			string sskName = skinComboBox.Text;
			if (string.IsNullOrEmpty(sskName) || skinComboBox.SelectedIndex == 0)
			{
				skinEngine1.Active = false;
				setDeepStyle(false);
				return;
			}
			if (skinComboBox.SelectedIndex == 1 )
			{
				skinEngine1.Active = false;
				setDeepStyle(true);
				return;
			}
			
			skinEngine1.Active = true;
			skinEngine1.SkinFile = Application.StartupPath + "\\irisSkins\\" + sskName + ".ssk";
			//额外加一句其他的句子(需要与SkniFile相关又不影响效果)，可以解决有些控件无法被渲染的问题
			skinEngine1.SkinFile = sskName + ".ssk";
					   
		}

		/// <summary>
		/// 事件：点击《灯库编辑》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightLibraryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openLightEditor();
		}

		/// <summary>
		/// 事件：点击《其他工具 - 外设配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripMenuItem_Click(object sender, EventArgs e)
		{
			toolButtonClick();
		}

		/// <summary>
		/// 事件：点击《其他工具 - 传视界灯控工具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void QDControllerToolStripMenuItem_Click(object sender, EventArgs e)
		{

			//try
			//{
			//	System.Diagnostics.Process.Start(Application.StartupPath + @"\QDController\灯光控制器.exe");
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
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
		/// 事件：点击《菜单栏 - 使用说明》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void helpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			helpButtonClick();
		}
			   
		/// <summary>
		/// 事件：点击《菜单栏 - 更新日志》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateLogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			updateLogButtonClick();
		}		

		/// <summary>
		/// 事件：点击《退出程序》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			exitClick();
		}

		#endregion

		#region 菜单栏 -工程相关

		/// <summary>
		/// 事件：点击《工程相关 - 灯具列表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			editLightList();
		}

		/// <summary>
		/// 事件：点击《工程相关 - 全局配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void globalSetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			globalSetClick();
		}
		
		/// <summary>
		/// 事件：点击《工程相关 - 工程更新》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void projectDownloadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			projectDownloadClick();
		}

		#endregion

		#region 工程及场景相关：点击事件及辅助方法

		/// <summary>
		/// 事件：点击《新建工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newProjectButton_Click(object sender, EventArgs e)
		{
			newProjectClick();
		}

		/// <summary>
		/// 事件：点击《打开工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openProjectButton_Click(object sender, EventArgs e)
		{
			openProjectClick();
		}

		/// <summary>
		/// 事件：点击《调用场景》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void copySceneButton_Click(object sender, EventArgs e)
		{
			copySceneClick();
		}

		/// <summary>
		/// 事件：点击《保存场景》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveSceneButton_Click(object sender, EventArgs e)
		{
			saveSceneClick();
		}

		/// <summary>
		/// 事件：点击《保存工程》（空方法：便于查找鼠标下压方法）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e) { }

		/// <summary>
		/// 事件：点击《保存工程》；根据点击按键的不同，采用不同的处理方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				saveProjectClick();
			}
			else if (e.Button == MouseButtons.Right)
			{
				exportSourceClick();
			}
		}

		/// <summary>
		/// 事件：点击《导出工程》（空方法：主要作用是方便查找鼠标下压方法）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exportProjectButton_Click(object sender, EventArgs e) {	}

		/// <summary>
		/// 事件：《导出工程》鼠标下压事件（判断是左键还是右键）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exportProjectButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				exportProjectClick();
			}
			else if (e.Button == MouseButtons.Right)
			{
				exportSceneClick();
			}
		}

		/// <summary>
		/// 事件：点击《关闭工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void closeButton_Click(object sender, EventArgs e)
		{
			closeProjectClick();
		}

		/// <summary>
		/// 辅助方法：是否显示《（打开工程后）工程相关》的各种按钮及控件
		/// </summary>
		/// <param name="enable"></param>
		protected override void enableProjectRelative(bool enable)
		{
			//常规的四个按钮
			saveButton.Enabled = enable;
			exportButton.Enabled = enable && LightAstList != null && LightAstList.Count > 0;
			saveSceneButton.Enabled = enable;
			closeButton.Enabled = enable;

			// 不同MainForm在不同位置的按钮
			copySceneButton.Enabled = enable && LightAstList != null && LightAstList.Count > 0;

			// 菜单栏相关按钮组			
			lightListToolStripMenuItem.Enabled = enable;
			globalSetToolStripMenuItem.Enabled = enable;			
		}

		/// <summary>
		/// 辅助方法： ClearAllData()最后一步
		///MARK：ClearAllData() in NewMainForm
		/// </summary>
		protected override void clearAllData()
		{
			base.clearAllData();

			lightsListView.Clear();
			stepPanel.Enabled = false;			
		}

		/// <summary>
		/// MARK 重构BuildLightList：reBuildLightListView() in NewMainForm
		///辅助方法：根据现有的lightAstList，重新渲染listView
		/// </summary>
		protected override void reBuildLightListView()
		{
            //listView用BeginUpdate和EndUpdate [能有效的节省一些资源，不用每加一个灯具就重绘一次;另外SkinListView不适合这种优化方法]
            lightsListView.BeginUpdate();

            lightsListView.Items.Clear();
			for (int i = 0; i < LightAstList.Count; i++)
			{
				// 添加灯具数据到LightsListView中
				lightsListView.Items.Add(new ListViewItem(
					LightAstList[i].LightType + "\n" +
						"(" + LightAstList[i].LightAddr + ")\n" +
						LightAstList[i].Remark,
					lightImageList.Images.ContainsKey(LightAstList[i].LightPic) ? LightAstList[i].LightPic : "灯光图.png"
				)
				{ Tag = LightAstList[i].LightName + ":" + LightAstList[i].LightType }	 );
			}           

           lightsListView.EndUpdate();
        }

		/// <summary>
		/// 辅助方法：初始化（StepTime）各控件的属性值
		/// </summary>
		protected override void initStNumericUpDowns()
		{
			for (int i = 0; i < 32; i++) {
				tdStNumericUpDowns[i].Maximum = EachStepTime2 * MAX_StTimes;
				tdStNumericUpDowns[i].Increment = EachStepTime2;
			}
		}

		//MARK 只开单场景：02.0.1 (NewMainForm)单纯地改变当前场景
		protected override void changeCurrentScene(int sceneIndex)
		{
			CurrentScene = sceneIndex;
			sceneComboBox.SelectedIndexChanged -= sceneComboBox_SelectedIndexChanged;
			sceneComboBox.SelectedIndex = CurrentScene;
			sceneComboBox.SelectedIndexChanged += sceneComboBox_SelectedIndexChanged;
		}

		#endregion

		#region lightsListView相关事件及辅助方法

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
				generateLightData(); //lightsListView_SelectedIndexChanged				
			}
		}	

		/// <summary>
		/// 辅助方法：根据传进来的LightAst对象，修改当前灯具内的显示内容
		/// </summary>
		/// <param name="la"></param>
		protected override void showLightsInfo()
		{
			if(checkNoLightSelected())
			{
				currentLightPictureBox.Image = null;
				lightNameLabel.Text = null;
				lightTypeLabel.Text = null;
				lightsAddrLabel.Text = null;
				lightRemarkLabel.Text = null;
				return;
			}

			LightAst la = LightAstList[selectedIndex];
			currentLightPictureBox.Image = lightImageList.Images.ContainsKey(la.LightPic) ? Image.FromFile(SavePath + @"\LightPic\" + la.LightPic) : global::LightController.Properties.Resources.灯光图;
			lightNameLabel.Text = LanguageHelper.TranslateWord("厂商：") + la.LightName;
			lightTypeLabel.Text = LanguageHelper.TranslateWord("型号：") + la.LightType;			
			lightRemarkLabel.Text = LanguageHelper.TranslateWord("备注：") + (isMultiMode ? "" : la.Remark);
			lightsAddrLabel.Text = LanguageHelper.TranslateWord("地址：") + generateAddrStr();
		}

		/// <summary>
		/// 辅助方法：
		/// 1.根据传进的lightIndex，生成相应的SaPanel-->若已存在，则不再生成；
		/// 2.显示相应的Panel，隐藏无关的Panel（至于Panel的Enable属性，则由ShowStepLabel方法来进行判断）
		/// </summary>
		/// <param name="lightIndex"></param>
		protected override void generateSaPanels()
		{			
			if ( ! IsShowSaPanels) {

				for (int tdIndex = 0; tdIndex < 32; tdIndex++)
				{
					saPanels[tdIndex].Hide();
				}
				return;
			}

			// 无步数时，直接跳过生成或显示saPanels的步骤
			if (getCurrentStep() == 0) {
				return;
			}

			//0. 每个灯具存储一个自身的saPanelDict,记录每个通道拥有的子属性（有的才加入到字典中）
			//1. 若还未生成saPanelDict，则在选择灯具后进行生成；
			//2. 不论是新生成还是已经存在的数据，按是否存在进行显示；
			LightAst la = LightAstList[selectedIndex];
			if (la.SawList == null)
			{
				generateStepTemplate(la);
			}

			if (la.saPanelDict == null) {
				la.saPanelDict = new Dictionary<int, FlowLayoutPanel>();
				for (int tdIndex = 0; tdIndex < la.SawList.Count; tdIndex++)
				{
					// 只有尚未生成Panel 且 子属性的数量大于0时 才需要生成子属性FlowLayoutPanel
					if (la.SawList[tdIndex].SaList.Count > 0)
					{
						la.saPanelDict.Add(tdIndex, new FlowLayoutPanel()
						{
							Size = saFLPDemo.Size,
							Dock = saFLPDemo.Dock,
							AutoScroll = saFLPDemo.AutoScroll,
							//BorderStyle = BorderStyle.Fixed3D
						});

						la.saPanelDict[tdIndex].Controls.Add(new Label {
							Size = saLabelDemo.Size,
							TextAlign = saLabelDemo.TextAlign,
							Text = saLabelDemo.Text,
							Margin = saLabelDemo.Margin,
						});

						//for (int saIndex = 0; saIndex < la.SawList[tdIndex].SaList.Count; saIndex++)  // 此代码为正序显示子属性
						for (int saIndex = la.SawList[tdIndex].SaList.Count - 1; saIndex >= 0; saIndex--)  //此代码为倒序显示子属性						
						{
							SA sa = la.SawList[tdIndex].SaList[saIndex];
							Button saButton = new Button
							{								
								Text = sa.SAName,
								TextAlign = saButtonDemo.TextAlign,
								Size = saButtonDemo.Size,
								Tag = tdIndex + "*" + sa.StartValue,
								UseVisualStyleBackColor = saButtonDemo.UseVisualStyleBackColor,
								Margin = saButtonDemo.Margin,			
								
							};
							saButton.Click += new EventHandler(saButton_Click);
							saToolTip.SetToolTip(saButton, sa.SAName + "\n" + sa.StartValue + " - " + sa.EndValue);
							la.saPanelDict[tdIndex].Controls.Add(saButton);
						}
						//Console.WriteLine("灯具【" + selectedIndex + "】生成了一个saPanel，tdIndex = " + tdIndex + " ,其子属性数量 = " + la.SawList[tdIndex].SaList.Count);
					}
				}
			}

			// 显示Keys中有的saPanel
			for (int tdIndex = 0; tdIndex < 32; tdIndex++)
			{				
				if (la.saPanelDict.ContainsKey(tdIndex))
				{					
					saPanels[tdIndex].Controls.Clear();
					saPanels[tdIndex].Controls.Add(la.saPanelDict[tdIndex]);
					saPanels[tdIndex].Show();
				}
				else
				{
					saPanels[tdIndex].Hide();
				}
			}					
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
			stepPanel.Enabled = enable;			
		}		
		
		/// <summary>
		/// 辅助方法：通过传来的数值，生成通道列表的数据
		/// </summary>
		/// <param name="tongdaoList"></param>
		/// <param name="startNum"></param>
		protected override void showTDPanels(IList<TongdaoWrapper> tongdaoList)
		{
			// 1.判断tongdaoList，为null或数量为0时：①隐藏所有通道；②退出此方法
			if (tongdaoList == null || tongdaoList.Count == 0)
			{
				for (int tdIndex = 0; tdIndex < 32; tdIndex++)
				{
					tdPanels[tdIndex].Hide();
					saPanels[tdIndex].Hide();
				}
			}
			//2.将dataWrappers的内容渲染到起VScrollBar中
			else
			{				
				for (int tdIndex = 0; tdIndex < tongdaoList.Count; tdIndex++)
				{
					tdTrackBars[tdIndex].ValueChanged -= tdTrackBars_ValueChanged;
					tdValueNumericUpDowns[tdIndex].ValueChanged -= tdValueNumericUpDowns_ValueChanged;
					tdCmComboBoxes[tdIndex].SelectedIndexChanged -= tdChangeModeSkinComboBoxes_SelectedIndexChanged;
					tdStNumericUpDowns[tdIndex].ValueChanged -= tdStepTimeNumericUpDowns_ValueChanged;

					tdNoLabels[tdIndex].Text = LanguageHelper.TranslateWord("通道") + tongdaoList[tdIndex].TongdaoCommon.Address;
					tdNameLabels[tdIndex].Text = tongdaoList[tdIndex].TongdaoCommon.TongdaoName;
					myToolTip.SetToolTip(tdNameLabels[tdIndex], tongdaoList[tdIndex].TongdaoCommon.Remark);
					tdTrackBars[tdIndex].Value = tongdaoList[tdIndex].ScrollValue;
					tdValueNumericUpDowns[tdIndex].Text = tongdaoList[tdIndex].ScrollValue.ToString();
					tdCmComboBoxes[tdIndex].SelectedIndex = tongdaoList[tdIndex].ChangeMode;

					//MARK 步时间 NewMainForm：主动 乘以时间因子 后 再展示
					tdStNumericUpDowns[tdIndex].Text = (tongdaoList[tdIndex].StepTime * EachStepTime2).ToString();

					tdTrackBars[tdIndex].ValueChanged += tdTrackBars_ValueChanged;
					tdValueNumericUpDowns[tdIndex].ValueChanged += tdValueNumericUpDowns_ValueChanged;
					tdCmComboBoxes[tdIndex].SelectedIndexChanged += tdChangeModeSkinComboBoxes_SelectedIndexChanged;
					tdStNumericUpDowns[tdIndex].ValueChanged += tdStepTimeNumericUpDowns_ValueChanged;

					tdPanels[tdIndex].Show();			
				}
				for (int tdIndex = tongdaoList.Count; tdIndex < 32; tdIndex++)
				{
					tdPanels[tdIndex].Hide();
				}

				if (from0on) {
					generateSaPanels();
				}
			}
		}
				
		/// <summary>
		/// 事件：双击《LightListView》内灯具，更改备注
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsListView_DoubleClick(object sender, EventArgs e)
		{
			int lightIndex = lightsListView.SelectedIndices[0];
			lightsListViewDoubleClick(lightIndex);
		}

		/// <summary>
		/// 辅助方法：添加或修改备注
		/// </summary>
		/// <param name="lightIndex"></param>
		/// <param name="remark"></param>
		public override void EditLightRemark(int lightIndex, string remark)
		{
			base.EditLightRemark(lightIndex, remark);
			// 界面的Items[lightIndex]也要改动相应的值；			
			lightsListView.Items[lightIndex].SubItems[0].Text =
				LightAstList[lightIndex].LightType + "\n("
				+ LightAstList[lightIndex].LightAddr + ")\n"
				+ LightAstList[lightIndex].Remark;
			lightsListView.Refresh();
		}

		#endregion

		//MARK：SkinMainForm灯具listView相关（右键菜单+位置等）

		#region  灯具listView相关（重新加载图片）

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
			foreach (LightAst la in LightAstList)
			{
				lightPathHashSet.Add(la.LightPath);
			}

			Dictionary<string, string> lightDict = new Dictionary<string, string>();
			foreach (var lightPath in lightPathHashSet)
			{
				string picStr = InHelper_UTF8.ReadString(lightPath, "set", "pic", "灯光图.png");
				lightDict.Add(lightPath, picStr);
			}

			for (int lightIndex = 0; lightIndex < LightAstList.Count; lightIndex++)
			{
				string tempPicStr = lightDict[LightAstList[lightIndex].LightPath];
				LightAstList[lightIndex].LightPic = tempPicStr;
				lightsListView.Items[lightIndex].ImageKey = lightImageList.Images.ContainsKey(tempPicStr) ? tempPicStr : "灯光图.png";
			}
		}

		/// <summary>
		/// 辅助方法：是否使能重新加载灯具图片
		/// </summary>
		/// <param name="enable"></param>
		protected override void enableRefreshPic(bool enable)
		{
			refreshPicToolStripMenuItem.Enabled = enable;
		}

		#endregion

		#region 几个显示或隐藏面板的菜单项

		/// <summary>
		/// 事件：点击《隐藏|显示主菜单面板》菜单项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hideMenuPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mainMenuStrip.Visible = !mainMenuStrip.Visible;
			hideMenuStriplToolStripMenuItem.Text = mainMenuStrip.Visible ? "隐藏主菜单面板" : "显示主菜单面板";
		}

		/// <summary>
		/// 事件：点击《隐藏|显示工程面板》菜单项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hideProjectPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			projectPanel.Visible = !projectPanel.Visible;
			hideProjectPanelToolStripMenuItem.Text = projectPanel.Visible ? "隐藏工程面板" : "显示工程面板";
		}

		/// <summary>
		/// 事件：点击《隐藏|显示辅助面板》菜单项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hideUnifyPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			unifyPanel.Visible = !unifyPanel.Visible;
			hideUnifyPanelToolStripMenuItem.Text = unifyPanel.Visible ? "隐藏辅助面板" : "显示辅助面板";
		}
		
		/// <summary>
		/// 事件：点击《隐藏|显示子属性面板》菜单项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void showSaPanelsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IsShowSaPanels = !IsShowSaPanels;
			showSaPanelsToolStripMenuItem.Text = IsShowSaPanels ? "隐藏子属性面板" : "显示子属性面板";

			generateSaPanels();
		}

		#endregion

		#region stepPanel相关的事件和辅助方法

		/// <summary>
		/// 更改《选择场景》选项后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void sceneComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			sceneSelectedChanged( sceneComboBox.SelectedIndex );
		}		

		/// <summary>
		/// 事件：更改《选择模式》选项后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void modeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			modeSelectedChanged( modeComboBox.SelectedIndex ,
				tdCmComboBoxes,	
				tdStNumericUpDowns,	
				thirdLabel );
		}

		/// <summary>
		/// 事件：点击《音频链表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void soundListButton_Click(object sender, EventArgs e)
		{
			new SKForm(this, CurrentScene, sceneComboBox.Text).ShowDialog();
		}

		/// <summary>
		///  事件：点击《上一步》
		///  先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backStepButton_Click(object sender, EventArgs e)	{		}

		/// <summary>
		/// 事件：鼠标（左|右键）按下《上一步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backStepButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				backStepClick();
			}
			else if (e.Button == MouseButtons.Right)
			{
				chooseStep(1); //backStepButton_MouseDown
			}
		}

		/// <summary>
		///  事件：点击《下一步》
		///  先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nextStepButton_Click(object sender, EventArgs e)	{	}

		/// <summary>
		/// 事件：鼠标（左|右键）按下《下一步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nextStepButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				nextStepClick();
			}
			else if (e.Button == MouseButtons.Right)
			{
				chooseStep(getCurrentTotalStep()); //nextStepButton_MouseDown
			}
		}

		/// <summary>
		/// 事件：点击《跳转步》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chooseStepButton_Click(object sender, EventArgs e)
		{
			int stepNum = decimal.ToInt32(chooseStepNumericUpDown.Value);
			if (stepNum == 0) {
				MessageBox.Show("不可选择0步");
				return;
			}
			chooseStep(stepNum);     //chooseStepButton_Click
		}

		/// <summary>
		/// 事件：点击《插入步》	(空方法，为方便定位insertAfterButton_MouseDown)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void insertStepButton_Click(object sender, EventArgs e)	{	}

		/// <summary>
		/// 事件：鼠标左右键按下《插入步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void insertAfterButton_MouseDown(object sender, MouseEventArgs e)
		{
			InsertStepClick(e.Button);
		}

		/// <summary>
		/// 事件：点击《追加步》(空方法方便定位到appendStepButton_MouseDown)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void appendStepButton_Click(object sender, EventArgs e) {  }

		/// <summary>
		///  事件：鼠标左|右键按下《追加步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void appendStepButton_MouseDown(object sender, MouseEventArgs e)
		{
			appendStepClick(e.Button);
		}

		/// <summary>
		///  事件：空方法，作用为方便查找deleteStepButton_MouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteStepButton_Click(object sender, EventArgs e){	}

		/// <summary>
		/// 事件：鼠标（左|右键）按下《删除步》	
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteStepButton_MouseDown(object sender, MouseEventArgs e)
		{
			deleteStepClick(e.Button);			
		}
		
		/// <summary>
		/// 事件：点击《复制步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void copyStepButton_Click(object sender, EventArgs e)
		{
			copyStepClick();
		}
		
		/// <summary>
		/// 事件：点击《粘贴步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pasteStepButton_Click(object sender, EventArgs e)
		{
			pasteStepClick();
		}	
		
		/// <summary>
		/// 事件：点击《保存素材》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveMaterialButton_Click(object sender, EventArgs e)
		{
			saveMaterialClick();
		}

		/// <summary>
		/// 事件：点击《使用素材》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void useMaterialButton_Click(object sender, EventArgs e)
		{
			useMaterialClick();
		}

		/// <summary>
		/// 事件：点击《进入同步|退出同步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void syncButton_Click(object sender, EventArgs e)
		{
			syncButtonClick();
		}

		/// <summary>
		/// 事件：点击《多步复用》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiplexButton_Click(object sender, EventArgs e)
		{
			multiplexButtonClick();
		}

		/// <summary>
		/// 辅助方法：《进入|退出编组》后的刷新相关控件显示
		/// </summary>
		protected override void refreshMultiModeControls()
		{
			//MARK 只开单场景：15.1 《灯具列表》是否可用，由单灯模式决定
			lightListToolStripMenuItem.Enabled = !isMultiMode;
			lightsListView.Enabled = !isMultiMode;
			sceneComboBox.Enabled = !isMultiMode;
			modeComboBox.Enabled = !isMultiMode;
			copySceneButton.Enabled = !isMultiMode;
			groupFlowLayoutPanel.Enabled = LightAstList != null ; // 只要当前工程有灯具，就可以进入编组（再由按钮点击事件进行进一步确认）
			groupButton.Text = isMultiMode ? "退出编组" : "灯具编组";

			lightsListView.SelectedIndexChanged -= lightsListView_SelectedIndexChanged;
			for (int lightIndex = 0; lightIndex < lightsListView.Items.Count; lightIndex++)
			{
				lightsListView.Items[lightIndex].Selected = selectedIndexList.Contains(lightIndex);
			}
			lightsListView.SelectedIndexChanged += lightsListView_SelectedIndexChanged;
		}

		/// <summary>
		/// 辅助方法：重置syncMode的相关属性，ChangeFrameMode、ClearAllData()、更改灯具列表后等？应该进行处理。
		/// </summary>
		protected override void enterSyncMode(bool isSyncMode)
		{
			this.isSyncMode = isSyncMode;
			syncButton.Text = isSyncMode ? "退出同步" : "进入同步";
			SetNotice(isSyncMode ? "已进入同步模式" : "已退出同步模式", false, true);
		}

		/// <summary>
		/// 辅助方法：显示步数标签，并判断stepPanel按钮组是否可用
		/// </summary>		
		protected override void showStepLabelMore(int currentStep, int totalStep)
		{
			// 1. 设label的Text值					   
			stepLabel.Text = MathHelper.GetFourWidthNumStr(currentStep, true) + "/" + MathHelper.GetFourWidthNumStr(totalStep, false);

			// 2.1 设定《删除步》按钮是否可用
			deleteButton.Enabled = totalStep != 0;

			// 2.2 设定《追加步》、《前插入步》《后插入步》按钮是否可用			
			bool insertEnabled = totalStep < MAX_STEP;
			appendButton.Enabled = insertEnabled;
			insertButton.Enabled = insertEnabled;

			// 2.3 设定《上一步》《下一步》是否可用			
			backStepButton.Enabled = totalStep > 1;
			nextStepButton.Enabled = totalStep > 1;

			//3 设定《复制|粘贴步、保存素材》、《多步复用》等是否可用
			copyStepButton.Enabled = currentStep > 0;
			pasteStepButton.Enabled = currentStep > 0 && tempStep != null;			
			saveMaterialButton.Enabled = currentStep > 0;

			// 4.设定统一调整区是否可用
			groupButton.Enabled = (LightAstList != null && lightsListView.SelectedIndices.Count > 0) || isMultiMode; // 只有工程非空（有灯具列表）且选择项不为空才可点击
			groupFlowLayoutPanel.Enabled = LightAstList != null;
			multiButton.Enabled = totalStep != 0;
			detailMultiButton.Enabled = totalStep != 0;
			multiplexButton.Enabled = currentStep > 0;
			soundListButton.Enabled = !string.IsNullOrEmpty(currentProjectName) && CurrentMode == 1;

			// 5.处理选择步数的框及按钮
			chooseStepNumericUpDown.Enabled = totalStep != 0;
			chooseStepNumericUpDown.Minimum = totalStep != 0 ? 1 : 0;
			chooseStepNumericUpDown.Maximum = totalStep;
			chooseStepButton.Enabled = totalStep != 0;

		}

		#endregion

		//MARK：NewMainForm：tdPanels内部数值调整及辅助方法
		#region tdPanel相关：内部数值的调整事件及辅助方法

		/// <summary>
		/// 事件:鼠标进入tdTrackBar时，把焦点切换到其对应的tdValueNumericUpDown中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdTrackBars_MouseEnter(object sender, EventArgs e)
		{
			int tdIndex = MathHelper.GetIndexNum(((TrackBar)sender).Name, -1);
			tdValueNumericUpDowns[tdIndex].Select();
		}

		/// <summary>
		///  事件：鼠标滚动时，通道值每次只变动一个Increment值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdTrackBars_MouseWheel(object sender, MouseEventArgs e)
		{
			//Console.WriteLine(	"trackBar_mouseWheel");
			int tdIndex = MathHelper.GetIndexNum(((TrackBar)sender).Name, -1);
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
				decimal dd = tdTrackBars[tdIndex].Value + tdTrackBars[tdIndex].SmallChange;
				if (dd <= tdTrackBars[tdIndex].Maximum)
				{
					tdTrackBars[tdIndex].Value = decimal.ToInt32(dd);
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = tdTrackBars[tdIndex].Value - tdTrackBars[tdIndex].SmallChange;
				if (dd >= tdTrackBars[tdIndex].Minimum)
				{
					tdTrackBars[tdIndex].Value = decimal.ToInt32(dd);
				}
			}
		}

		/// <summary>
		///  事件：TrackBar滚轴值改变时的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdTrackBars_ValueChanged(object sender, EventArgs e)
		{
			// 1.先找出对应tdSkinTrackBars的index 
			int tongdaoIndex = MathHelper.GetIndexNum(((TrackBar)sender).Name, -1);
			int tdValue = tdTrackBars[tongdaoIndex].Value;

			//2.把滚动条的值赋给tdValueNumericUpDowns
			// 8.28	：在修改时取消其监听事件，修改成功恢复监听；这样就能避免重复触发监听事件
			tdValueNumericUpDowns[tongdaoIndex].ValueChanged -= tdValueNumericUpDowns_ValueChanged;
			tdValueNumericUpDowns[tongdaoIndex].Value = tdValue;
			tdValueNumericUpDowns[tongdaoIndex].ValueChanged += tdValueNumericUpDowns_ValueChanged;

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
			// 1. 找出对应的index
			int tongdaoIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
			int tdValue = decimal.ToInt32(tdValueNumericUpDowns[tongdaoIndex].Value);

			// 2.调整相应的vScrollBar的数值；
			// 8.28 ：在修改时取消其监听事件，修改成功恢复监听；这样就能避免重复触发监听事件
			tdTrackBars[tongdaoIndex].ValueChanged -= new System.EventHandler(this.tdTrackBars_ValueChanged);
			tdTrackBars[tongdaoIndex].Value = tdValue;
			tdTrackBars[tongdaoIndex].ValueChanged += new System.EventHandler(this.tdTrackBars_ValueChanged);

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
			int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
			tdValueNumericUpDowns[tdIndex].Select();
		}

		/// <summary>
		///  事件：鼠标滚动时，通道值每次只变动一个Increment值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdValueNumericUpDowns_MouseWheel(object sender, MouseEventArgs e)
		{
			int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
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
			int tdIndex = MathHelper.GetIndexNum(((ComboBox)sender).Name, -1);

			//2.取出recentStep，这样就能取出一个步数，使用取出的index，给stepWrapper.TongdaoList[index]赋值
			StepWrapper step = getCurrentStepWrapper();
			int changeMode = tdCmComboBoxes[tdIndex].SelectedIndex;
			step.TongdaoList[tdIndex].ChangeMode = tdCmComboBoxes[tdIndex].SelectedIndex;

			//3.多灯模式下，需要把调整复制到各个灯具去
			if (isMultiMode)
			{
				copyValueToAll(tdIndex, EnumUnifyWhere.CHANGE_MODE, changeMode);
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
			int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
			tdStNumericUpDowns[tdIndex].Select();
		}

		/// <summary>
		///  事件：鼠标滚动时，步时间值每次只变动一个Increment值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdStepTimeNumericUpDowns_MouseWheel(object sender, MouseEventArgs e)
		{
			int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			if (e.Delta > 0)
			{
				decimal dd = tdStNumericUpDowns[tdIndex].Value + tdStNumericUpDowns[tdIndex].Increment;
				if (dd <= tdStNumericUpDowns[tdIndex].Maximum)
				{
					tdStNumericUpDowns[tdIndex].Value = dd;
				}
			}
			else if (e.Delta < 0)
			{
				decimal dd = tdStNumericUpDowns[tdIndex].Value - tdStNumericUpDowns[tdIndex].Increment;
				if (dd >= tdStNumericUpDowns[tdIndex].Minimum)
				{
					tdStNumericUpDowns[tdIndex].Value = dd;
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
			int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);

			//2.取出recentStep，这样就能取出一个步数，使用取出的index，给stepWrapper.TongdaoList[index]赋值
			StepWrapper step = getCurrentStepWrapper();

			// MARK 步时间 NewMainForm：处理为数据库所需数值：将 (显示的步时间* 时间因子)后再放入内存
			int stepTime = decimal.ToInt32(tdStNumericUpDowns[tdIndex].Value / EachStepTime2); // 取得的值自动向下取整（即舍去多余的小数位）
			step.TongdaoList[tdIndex].StepTime = stepTime;
			tdStNumericUpDowns[tdIndex].Value = stepTime * EachStepTime2; //若与所见到的值有所区别，则将界面控件的值设为处理过的值

			if (isMultiMode) {
				copyValueToAll(tdIndex, EnumUnifyWhere.STEP_TIME, stepTime);
			}
		}

		/// <summary>
		/// 事件：点击《tdNameLabels》时，右侧的子属性按钮组，会显示当前通道相关的子属性，其他通道的子属性，则隐藏掉
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdNameNumLabels_Click(object sender, EventArgs e)
		{			
			tdNameNumLabelClick(sender);
		}

		/// <summary>
		/// 事件：监听几个通道输入框的键盘点击
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyTd_KeyPress(object sender, KeyPressEventArgs e)
		{
			unifyTdKeyPress(sender, e);
		}

		#endregion

		//MARK：NewMainForm：统一调整框各事件处理
		#region unifyPanel（辅助调节面板）

		/// <summary>
		/// 事件：点击《灯具编组 | 退出编组》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void groupButton_Click(object sender, EventArgs e)
		{
			groupButtonClick(lightsListView);
		}		
				
		/// <summary>
		/// 事件：点击《多步调节》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiButton_Click(object sender, EventArgs e)	{	}

		/// <summary>
		/// 事件：左右键点击《多步调节》：右键是多步联调
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiButton_MouseDown(object sender, MouseEventArgs e)
		{
 			if (e.Button == MouseButtons.Left)
			{
				multiButtonClick();
			}
			else if (e.Button == MouseButtons.Right)
			{
				//目前没有进行任何关联
			}
		}

		/// <summary>
		/// 事件：点击《多步联调》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void detailMultiButton_Click(object sender, EventArgs e) { }

		/// <summary>
		/// 事件：左右键点击《多步联调》右键进入之前选过的联调界面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void detailMultiButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				DetailMultiButtonClick(false);
			}
			else if (e.Button == MouseButtons.Right)
			{
				DetailMultiButtonClick(true);
			}
		}
		
		/// <summary>
		/// 事件：点击《groupInButtons(进入编组)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void groupInButton_Click(object sender, EventArgs e)
		{
			groupInButtonClick(sender,lightsListView);
		}

		/// <summary>
		/// 事件：点击《groupDelButtons(删除编组)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void groupDelButton_Click(object sender, EventArgs e)
		{
			groupDelButtonClick(sender);
		}

		/// <summary>
		/// 事件：点击《saButton》按钮组的任意按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saButton_Click(object sender, EventArgs e)
		{
			saButtonClick(sender);
		}

		/// <summary>
		/// 辅助方法：生成编组按钮组（先清空，再由groupList直接生成新的按钮组）
		/// </summary>
		protected override void refreshGroupPanels()
		{
			groupFlowLayoutPanel.Controls.Clear();
			groupToolTip.RemoveAll();
			if (GroupList != null && GroupList.Count > 0)
			{
				for (int groupIndex = 0; groupIndex < GroupList.Count; groupIndex++)
				{
					addGroupPanel(groupIndex, GroupList[groupIndex]);
				}
			}
		}

		/// <summary>
		///辅助方法：添加编组按钮（一个编组一个Panel，包含两个按钮：使用编组 和 删除编组）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addGroupPanel(int groupIndex, GroupAst ga)
		{
			Panel panel = new Panel();
			Button inButton = new Button();
			Button delButton = new Button();

			panel.Controls.Add(inButton);
			panel.Controls.Add(delButton);
			panel.Location = new System.Drawing.Point(0, 0);
			panel.Name = "groupPanel";
			panel.Size = new System.Drawing.Size(140, 26);
			panel.TabIndex = 56;
			panel.Padding = new Padding(0);
			//panel.BorderStyle = BorderStyle.FixedSingle;

			inButton.BackColor = System.Drawing.Color.White;
			inButton.Enabled = true;
			inButton.Location = new System.Drawing.Point(0, 0);
			inButton.Margin = new System.Windows.Forms.Padding(0);
			inButton.Name = "groupInButton";
			inButton.Size = new System.Drawing.Size(114, 26);
			inButton.TabIndex = 55;
			inButton.Text = ga.GroupName;
			inButton.Tag = groupIndex;
			inButton.UseVisualStyleBackColor = true;
			inButton.Click += new EventHandler(groupInButton_Click);

			delButton.BackColor = System.Drawing.Color.White;
			delButton.Enabled = true;
			delButton.Location = new System.Drawing.Point(118, 0);
			delButton.Margin = new System.Windows.Forms.Padding(0);
			delButton.Name = "groupDelButton";
			delButton.Size = new System.Drawing.Size(20, 26);
			delButton.TabIndex = 55;
			delButton.Text = "-";
			delButton.Tag = groupIndex;
			delButton.UseVisualStyleBackColor = true;
			delButton.Click += new EventHandler(groupDelButton_Click);

			groupFlowLayoutPanel.Controls.Add(panel);
			groupToolTip.SetToolTip(inButton, ga.GroupName + "\n" + StringHelper.MakeIntListToString(ga.LightIndexList, 1, ga.CaptainIndex));
		}

		#endregion

		//MARK：NewMainForm：playPanel相关点击事件及辅助方法
		#region 灯控调试按钮组（playPanel）点击事件及辅助方法
					
		/// <summary>
		/// 事件：点击《保持状态|取消保持》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void keepButton_Click(object sender, EventArgs e)
		{
			// 默认情况下，《保持其它灯状态》还没打开，点击后设为打开状态（文字显示为关闭实时调试，图片加颜色）
			if (!isKeepOtherLights)
			{
				keepButton.Text = "取消保持";
				isKeepOtherLights = true;
			}
			else //否则( 按钮显示为“保持其他灯状态”）断开连接
			{
				keepButton.Text = "保持状态";
				isKeepOtherLights = false;
			}
			refreshStep();
		}

		/// <summary>
		/// 事件：点击《预览效果》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewButton_Click(object sender, EventArgs e)
		{
			PreviewButtonClick(null);
		}
		
		/// <summary>
		/// 事件：点击《触发音频》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void makeSoundButton_Click(object sender, EventArgs e)
		{
			makeSoundButtonClick();
		}

		/// <summary>
		///  辅助方法：《连接设备按钮组》是否显示
		/// </summary>
		/// <param name="v"></param>
		protected override void refreshConnectedControls(bool connected,bool previewing)
		{
			base.refreshConnectedControls(connected, previewing);

			hardwareSetToolStripMenuItem.Enabled = connected;
			toolStripMenuItem.Enabled = connected;
			seqToolStripMenuItem.Enabled = connected;
			projectDownloadToolStripMenuItem.Enabled = connected;

			keepButton.Enabled = IsEnableOneStepPlay();
			previewButton.Text = IsPreviewing ? "停止预览" : "预览效果";
			previewButton.Enabled = IsOneMoreConnected(); 
			makeSoundButton.Enabled = IsOneMoreConnected() && IsPreviewing ;		
			
			//721：刷新当前步(因为有些操作是异步的，可能造成即时的刷新步数，无法进入单灯单步)
			if ( IsEnableOneStepPlay() ) {
				refreshStep();
			}						
		}

		#endregion

		#region 全局辅助方法

		/// <summary>
		/// 设置提示信息
		/// </summary>
		/// <param name="notice"></param>
		public override void SetNotice(string notice,bool msgBoxShow,bool isTranslate)
		{
			if (isTranslate) {
				notice = LanguageHelper.TranslateSentence(notice);
			}

			myStatusLabel.Text = notice;
			myStatusStrip.Refresh();
			if (msgBoxShow) {
				MessageBox.Show(notice);
			}
		}

		/// <summary>
		/// 设置是否忙时
		/// </summary>
		/// <param name="buzy"></param>
		protected override void setBusy(bool busy)
		{
			Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
			Enabled = !busy;
		}

		#endregion
		
		/// <summary>
		/// 事件：各个按键的文本更改后，有需要就进行翻译
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someButton_TextChanged(object sender, EventArgs e)
		{
			LanguageHelper.TranslateControl( sender as Button);
		}
			   
		/// <summary>
		/// 事件：点击《设备连接》->导航用
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectToolStripMenuItem_Click(object sender, EventArgs e)	{	}
					
		/// <summary>
		/// 事件：左右键按《设备连接》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
		{
			connectButtonClick(e.Button);
		}

		/// <summary>
		/// 事件：点击《硬件配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hardwareSetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			hardwareSetButtonClick();
		}

		/// <summary>
		/// 事件：点击《继电器配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void seqToolStripMenuItem_Click(object sender, EventArgs e)
		{
			sequencerButtonClick();
		}

		private void currentLightPictureBox_Click(object sender, EventArgs e)
		{
			testButtonClick();
		}
			
	}
}
