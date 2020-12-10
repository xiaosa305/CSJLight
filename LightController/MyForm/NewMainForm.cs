using DMX512;
using ICSharpCode.SharpZipLib.Zip;
using LightController.Ast;
using LightController.Common;
using LightController.MyForm.Multiplex;
using LightController.MyForm.Test;
using LightController.PeripheralDevice;
using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using LightEditor.Ast;
using OtherTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
	public partial class NewMainForm : MainFormBase
	{
		#region 此处定义一些全局变量，用以界面风格的统一设置

		private BorderStyle unifyBorderStyle = BorderStyle.Fixed3D; //统一为局内的所有panel设置统一的BorderStyle		
		private Color unifyColor = SystemColors.Window;
		private Color unifyColor2 = SystemColors.Window;

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
			initGeneralControls(); //几个全局控件的初始化
			InitializeComponent();

			// 定义标题栏文字+Icon
			string iconPath = Application.StartupPath + @"\favicon.ico";
			if (File.Exists(iconPath))
			{
				Icon = Icon.ExtractAssociatedIcon(iconPath);
			}
			Text = SoftwareName;

			hardwareUpdateToolStripMenuItem.Enabled = IsShowHardwareUpdate;// 动态显示硬件升级按钮
			testButton1.Visible = IsShowTestButton;
			testButton2.Visible = IsShowTestButton;
			wjTestButton.Visible = IsShowTestButton;

			//MARK：添加这一句，会去掉其他线程使用本UI控件时弹出异常的问题(权宜之计，并非长久方案)。
			CheckForIllegalCrossThreadCalls = false;

			//动态添加32个tdPanel的内容及其监听事件; 动态添加32个saPanel 
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
					"跳变",
					"渐变",
					"屏蔽"});

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

				tdPanels[tdIndex].Controls.Add(this.tdNameLabels[tdIndex]);
				tdPanels[tdIndex].Controls.Add(this.tdNoLabels[tdIndex]);
				tdPanels[tdIndex].Controls.Add(this.tdTrackBars[tdIndex]);
				tdPanels[tdIndex].Controls.Add(this.tdValueNumericUpDowns[tdIndex]);
				tdPanels[tdIndex].Controls.Add(this.tdCmComboBoxes[tdIndex]);
				tdPanels[tdIndex].Controls.Add(this.tdStNumericUpDowns[tdIndex]);	

				tdTrackBars[tdIndex].MouseEnter += new EventHandler(tdTrackBars_MouseEnter);
				tdTrackBars[tdIndex].MouseWheel += new MouseEventHandler(this.tdTrackBars_MouseWheel);
				tdTrackBars[tdIndex].ValueChanged += new System.EventHandler(this.tdTrackBars_ValueChanged);

				tdValueNumericUpDowns[tdIndex].MouseEnter += new EventHandler(this.tdValueNumericUpDowns_MouseEnter);
				tdValueNumericUpDowns[tdIndex].MouseWheel += new MouseEventHandler(this.tdValueNumericUpDowns_MouseWheel);
				tdValueNumericUpDowns[tdIndex].ValueChanged += new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);

				tdCmComboBoxes[tdIndex].SelectedIndexChanged += new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);

				tdStNumericUpDowns[tdIndex].MouseEnter += new EventHandler(this.tdStepTimeNumericUpDowns_MouseEnter);
				tdStNumericUpDowns[tdIndex].MouseWheel += new MouseEventHandler(this.tdStepTimeNumericUpDowns_MouseWheel);
				tdStNumericUpDowns[tdIndex].ValueChanged += new EventHandler(this.tdStepTimeNumericUpDowns_ValueChanged);

				tdNoLabels[tdIndex].Click += new EventHandler(this.tdNameNumLabels_Click);
				tdNameLabels[tdIndex].Click += new EventHandler(this.tdNameNumLabels_Click);

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

				tdValueNumericUpDowns[tdIndex].KeyPress += td_KeyPress;
				tdCmComboBoxes[tdIndex].KeyPress += td_KeyPress;
				tdStNumericUpDowns[tdIndex].KeyPress += td_KeyPress;

			}

			// 场景选项框		
			//添加FramList.txt中的场景列表
			AllFrameList = TextHelper.Read(Application.StartupPath + @"\FrameList.txt");
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

			///统一调节的几个输入框，设置监听事件
			unifyValueNumericUpDown.MouseWheel += new MouseEventHandler(this.unifyValueNumericUpDown_MouseWheel);
			unifyChangeModeComboBox.SelectedIndex = 1;    // 《统一跳渐变》numericUpDown不得为空，否则会造成点击后所有通道的changeMode形式上为空（不过Value不是空）
			unifyStepTimeNumericUpDown.MouseWheel += new MouseEventHandler(this.unifyStepTimeNumericUpDown_MouseWheel);

			// 几个按钮添加提示
			myToolTip.SetToolTip(useFrameButton, useFrameNotice);
			myToolTip.SetToolTip(chooseStepButton, chooseStepNotice);
			myToolTip.SetToolTip(keepButton, keepNotice);
			myToolTip.SetToolTip(insertButton, insertNotice);
			myToolTip.SetToolTip(backStepButton, backStepNotice);
			myToolTip.SetToolTip(nextStepButton, nextStepNotice);

			#region 皮肤 及 panel样式 相关代码

			setDeepStyle(false);
			if (IniFileHelper.GetControlShow(Application.StartupPath, "useSkin")) {
				//加载皮肤列表		
				DirectoryInfo fdir = new DirectoryInfo(Application.StartupPath + "\\irisSkins");
				try
				{
					FileInfo[] file = fdir.GetFiles();
					if (file.Length > 0)
					{
						skinComboBox.Items.Add("浅色皮肤");
						skinComboBox.Items.Add("深色皮肤");

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
			this.lightsListView.LargeImageList = lightImageList;
			RefreshLightImageList();

			isInit = true;
		}	

		private void NewMainForm_Load(object sender, EventArgs e)
		{
			//启动时刷新可用串口列表，但把显示给删除
			deviceRefresh();    //NewMainForm_Load
			SetNotice("", false);

			// 用以处理大工程时，子属性列表会连在一起的bug；
			foreach (Panel panel in saPanels)
			{			
				panel.Hide();
			}
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
			playBasePanel.BackColor = unifyColor;

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
			playBasePanel.BorderStyle = unifyBorderStyle;
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

			string sskName = skinComboBox.Text;
			if (String.IsNullOrEmpty(sskName) || sskName.Equals("浅色皮肤"))
			{
				this.skinEngine1.Active = false;
				setDeepStyle(false);
				return;
			}
			if (sskName.Equals("深色皮肤"))
			{
				this.skinEngine1.Active = false;
				setDeepStyle(true);
				return;
			}
			
			this.skinEngine1.Active = true;
			this.skinEngine1.SkinFile = Application.StartupPath + "\\irisSkins\\" + sskName + ".ssk";
			//额外加一句其他的句子(需要与SkniFile相关又不影响效果)，可以解决有些控件无法被渲染的问题
			this.skinEngine1.SkinFile = sskName + ".ssk";

			// 若需保存用户自选的皮肤，则启用下句
			//new IniFileAst(Application.StartupPath+@"\GlobalSet.ini").WriteString("SkinSet", "skin", sskName + ".ssk");

			// 网上搜到的处理闪烁的解决方案，测试有没有效果
			//SetStyle(ControlStyles.DoubleBuffer, true);    //设置双缓冲，防止图像抖动      
			//SetStyle(ControlStyles.AllPaintingInWmPaint, true);    //忽略系统消息，防止图像闪烁

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
		/// 事件：点击《硬件配置 - 打开配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hardwareSetOpenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new HardwareSetChooseForm(this).ShowDialog();
		}

		/// <summary>
		/// 事件：点击《硬件配置 - 新建配置》
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
			hardwareUpdateClick();
		}

		/// <summary>
		/// 事件：点击《其他工具 - 外设配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			newToolClick();
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
		/// 事件：点击《工程相关 - 摇麦配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ymToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ymSetClick();
		}

		/// <summary>
		/// 事件：点击《工程相关 - 工程更新》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void projectUpdateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			projectUpdateClick();
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
		private void useFrameButton_Click(object sender, EventArgs e)
		{
			useFrameClick();
		}

		/// <summary>
		/// 事件：点击《保存场景》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveFrameButton_Click(object sender, EventArgs e)
		{
			saveFrameClick();
		}

		/// <summary>
		/// 事件：点击《保存工程》（空方法：便于查找鼠标下压方法）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveProjectButton_Click(object sender, EventArgs e) { }

		/// <summary>
		/// 事件：点击《保存工程》；根据点击按键的不同，采用不同的处理方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveProjectButton_MouseDown(object sender, MouseEventArgs e)
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
		private void exportProjectButton_Click(object sender, EventArgs e) { }

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
				exportFrameClick();
			}
		}

		/// <summary>
		/// 事件：点击《关闭工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void closeProjectButton_Click(object sender, EventArgs e)
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
			saveProjectButton.Enabled = enable;
			exportButton.Enabled = enable && LightAstList != null && LightAstList.Count > 0;
			saveFrameButton.Enabled = enable;
			closeProjectButton.Enabled = enable;

			// 不同MainForm在不同位置的按钮
			useFrameButton.Enabled = enable && LightAstList != null && LightAstList.Count > 0;

			// 菜单栏相关按钮组			
			lightListToolStripMenuItem.Enabled = enable;
			globalSetToolStripMenuItem.Enabled = enable;
			ymSetToolStripMenuItem.Enabled = enable;
			projectUpdateToolStripMenuItem.Enabled = enable;
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
			editLightInfo(null);	
		}

		/// <summary>
		/// MARK 重构BuildLightList：reBuildLightListView() in NewMainForm
		///辅助方法：根据现有的lightAstList，重新渲染listView
		/// </summary>
		protected override void reBuildLightListView()
		{
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
				{ Tag = LightAstList[i].LightName + ":" + LightAstList[i].LightType }
				);
			}
			Refresh();
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
		/// 辅助方法：初始化（StepTime）各控件的属性值
		/// </summary>
		protected override void initStNumericUpDowns()
		{
			unifyStepTimeNumericUpDown.Maximum = EachStepTime2 * MAX_StTimes; ;
			unifyStepTimeNumericUpDown.Increment = EachStepTime2;

			for (int i = 0; i < 32; i++) {
				tdStNumericUpDowns[i].Maximum = EachStepTime2 * MAX_StTimes;
				tdStNumericUpDowns[i].Increment = EachStepTime2;
			}
		}

		//MARK 只开单场景：02.0.1 (NewMainForm)改变当前Frame
		protected override void changeCurrentFrame(int frameIndex)
		{
			CurrentFrame = frameIndex;
			frameComboBox.SelectedIndexChanged -= new System.EventHandler(this.frameComboBox_SelectedIndexChanged);
			frameComboBox.SelectedIndex = CurrentFrame;
			frameComboBox.SelectedIndexChanged += new System.EventHandler(this.frameComboBox_SelectedIndexChanged);
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
				//disposeSauForm();
				if ( generateNow)
				{
					generateLightData();
				}
			}
		}	

		/// <summary>
		/// 辅助方法：根据传进来的LightAst对象，修改当前灯具内的显示内容
		/// </summary>
		/// <param name="la"></param>
		protected override void editLightInfo(LightAst la)
		{			
			if (la == null)
			{
				currentLightPictureBox.Image = null;
				lightNameLabel.Text = null;
				lightTypeLabel.Text = null;
				lightsAddrLabel.Text = null;
				lightRemarkLabel.Text = null;
				return;
			}

			currentLightPictureBox.Image = lightImageList.Images.ContainsKey(la.LightPic) ? Image.FromFile(SavePath + @"\LightPic\" + la.LightPic) : global::LightController.Properties.Resources.灯光图;
			lightNameLabel.Text = "厂商：" + la.LightName;
			lightTypeLabel.Text = "型号：" + la.LightType;
			lightsAddrLabel.Text = "地址：" + la.LightAddr;
			lightRemarkLabel.Text = "备注：" + la.Remark;
			myToolTip.SetToolTip(lightRemarkLabel, "备注：\n" + la.Remark);		
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
		/// 辅助方法: 确认选中灯具是否否同一种灯具：是则返回true,否则返回false。
		/// 验证方法：取出第一个选中灯具的名字，若后面的灯具的全名（Tag =lightName + ":" + lightType)与它不同，说明不是同种灯具。（只要一个不同即可判断）
		/// </summary>
		/// <returns></returns>
		private bool checkSameLights()
		{
			if (lightsListView.SelectedItems.Count == 0)
			{
				return false;
			}
			if (lightsListView.SelectedItems.Count == 1)
			{
				return true;
			}

			bool result = true;
			string firstTag = lightsListView.SelectedItems[0].Tag.ToString();
			for (int i = 1; i < lightsListView.SelectedItems.Count; i++) // 从第二个选中灯具开始比对
			{
				string tempTag = lightsListView.SelectedItems[i].Tag.ToString();
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
			}
			//2.将dataWrappers的内容渲染到起VScrollBar中
			else
			{				
				for (int tdIndex = 0; tdIndex < tongdaoList.Count; tdIndex++)
				{
					tdTrackBars[tdIndex].ValueChanged -= new System.EventHandler(this.tdTrackBars_ValueChanged);
					tdValueNumericUpDowns[tdIndex].ValueChanged -= new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);
					tdCmComboBoxes[tdIndex].SelectedIndexChanged -= new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);
					tdStNumericUpDowns[tdIndex].ValueChanged -= new EventHandler(this.tdStepTimeNumericUpDowns_ValueChanged);

					tdNoLabels[tdIndex].Text = "通道" + (startNum + tdIndex);
					tdNameLabels[tdIndex].Text = tongdaoList[tdIndex].TongdaoName;
					myToolTip.SetToolTip(tdNameLabels[tdIndex], tongdaoList[tdIndex].Remark);
					tdTrackBars[tdIndex].Value = tongdaoList[tdIndex].ScrollValue;
					tdValueNumericUpDowns[tdIndex].Text = tongdaoList[tdIndex].ScrollValue.ToString();
					tdCmComboBoxes[tdIndex].SelectedIndex = tongdaoList[tdIndex].ChangeMode;

					//MARK 步时间 NewMainForm：主动 乘以时间因子 后 再展示
					tdStNumericUpDowns[tdIndex].Text = (tongdaoList[tdIndex].StepTime * EachStepTime2).ToString();

					tdTrackBars[tdIndex].ValueChanged += new System.EventHandler(this.tdTrackBars_ValueChanged);
					tdValueNumericUpDowns[tdIndex].ValueChanged += new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);
					tdCmComboBoxes[tdIndex].SelectedIndexChanged += new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);
					tdStNumericUpDowns[tdIndex].ValueChanged += new EventHandler(this.tdStepTimeNumericUpDowns_ValueChanged);

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
		///辅助方法：隐藏所有的TdPanel
		/// </summary>
		protected override void hideAllTDPanels()
		{
			for (int tdIndex = 0; tdIndex < 32; tdIndex++)
			{				
				tdPanels[tdIndex].Hide();
				saPanels[tdIndex].Hide();
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
				string picStr = IniFileHelper_UTF8.ReadString(lightPath, "set", "pic", "灯光图.png");
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

		#region 灯具listView相关（右键菜单+位置等）newMainForm内暂时不采用可以移动灯具图标，故下列代码暂时无用

		// listView1.AllowDrop = true; 
		// listView1.AutoArrange = false;
		private Point startPoint = Point.Empty;

		/// <summary>
		/// 辅助方法：获取亮点之间的距离
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
		private void lightsListView_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem[])))
				e.Effect = DragDropEffects.Move;
		}

		/// <summary>
		/// 事件：松开鼠标时发生（VS：拖动操作时发生）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsListView_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem[])))
			{
				var items = e.Data.GetData(typeof(ListViewItem[])) as ListViewItem[];

				var pos = lightsListView.PointToClient(new Point(e.X, e.Y));

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
		private void lightsListView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				startPoint = e.Location;
		}

		/// <summary>
		/// 事件：listView鼠标移动
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsListView_MouseMove(object sender, MouseEventArgs e)
		{
			if (lightsListView.SelectedItems.Count == 0)
				return;

			if (e.Button == MouseButtons.Left)
			{
				var vector = getVector(startPoint, e.Location);
				if (vector < 10) return;

				var data = lightsListView.SelectedItems.OfType<ListViewItem>().ToArray();

				lightsListView.DoDragDrop(data, DragDropEffects.Move);
			}
		}

		/// <summary>
		/// 事件：点选《自动排列》与否
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void autoArrangeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//isAutoArrange = autoArrangeToolStripMenuItem.Checked;
			//lightsListView.AllowDrop = !isAutoArrange;
			//lightsListView.AutoArrange = isAutoArrange;
			//autoEnableSLArrange();
		}

		/// <summary>
		/// 事件：点击《重新排列》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void arrangeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//bool tempAutoArrange = lightsListView.AutoArrange;
			//lightsListView.AutoArrange = true;
			//lightsListView.AutoArrange = tempAutoArrange;
			//lightsListView.Update();
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
			if (LightAstList == null || LightAstList.Count == 0)
			{
				MessageBox.Show("当前工程尚无灯具，无法保存灯具位置，请添加灯具后重新保存。");
				return;
			}

			// 4.保存操作
			IniFileHelper iniFileAst = new IniFileHelper(arrangeIniPath);
			iniFileAst.WriteInt("Common", "Count", lightsListView.Items.Count);
			for (int i = 0; i < lightsListView.Items.Count; i++)
			{
				iniFileAst.WriteInt("Position", i + "X", lightsListView.Items[i].Position.X);
				iniFileAst.WriteInt("Position", i + "Y", lightsListView.Items[i].Position.Y);
			}
			autoEnableSLArrange();

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
			IniFileHelper iniFileAst = new IniFileHelper(arrangeIniPath);
			int lightCount = iniFileAst.ReadInt("Common", "Count", 0);
			if (lightCount == 0)
			{
				MessageBox.Show("灯具位置文件的灯具数量为0，此文件无实际效果。");
				return;
			}

			//3. 验证灯具数量是否一致
			if (lightCount != lightsListView.Items.Count)
			{
				MessageBox.Show("灯具位置文件的灯具数量与当前工程的灯具数量不匹配，无法读取位置。");
				return;
			}

			// 4.开始读取并绘制		
			// 特别奇怪的一个地方，在选择自动排列再去掉自动排列后，必须要先设一个不同的position，才能让读取到的position真正给到items[i].Position?
			lightsListView.BeginUpdate();
			for (int i = 0; i < lightsListView.Items.Count; i++)
			{
				//Console.WriteLine(lightsSkinListView.Items[i].Position);
				int tempX = iniFileAst.ReadInt("Position", i + "X", 0);
				int tempY = iniFileAst.ReadInt("Position", i + "Y", 0);
				lightsListView.Items[i].Position = new Point(0, 0);
				lightsListView.Items[i].Position = new Point(tempX, tempY);
			}

			lightsListView.EndUpdate();
			MessageBox.Show("灯具位置读取成功。");
		}

		/// <summary>
		///  辅助方法：是否显示《 存、取 灯具位置》	
		/// </summary>
		/// <param name="enableSave"></param>
		/// <param name="enableLoad"></param>
		protected override void autoEnableSLArrange()
		{
			//saveArrangeToolStripMenuItem.Enabled = enableSave;
			//loadArrangeToolStripMenuItem.Enabled = enableLoad;
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
		/// 事件：点击《隐藏|显示调试面板》菜单项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hidePlayPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			playBasePanel.Visible = !playBasePanel.Visible;
			hidePlayPanelToolStripMenuItem.Text = playBasePanel.Visible ? "隐藏调试面板" : "显示调试面板";
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
		/// 事件：更改《选择场景》选项后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//11.13 若未初始化，直接return；
			if (!isInit)
			{
				return;
			}
			setBusy(true);
			SetNotice("正在切换场景,请稍候...",false);

			// 只要更改了场景，直接结束预览
			endview();

			DialogResult dr = MessageBox.Show("切换场景前，是否保存之前场景(" + AllFrameList[CurrentFrame] + ")？",
				"保存场景?",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			if (dr == DialogResult.Yes)
			{
				saveFrameClick();
				//MARK 只开单场景：06.0.1 切换场景时，若选择保存之前场景，则frameSaveArray设为false，意味着以后不需要再保存了。
				frameSaveArray[CurrentFrame] = false;
			}

			CurrentFrame = frameComboBox.SelectedIndex;
			//MARK 只开单场景：06.1.1 更改场景时，只有frameLoadArray为false，才需要从DB中加载相关数据（调用generateFrameData）；若为true，则说明已经加载因而无需重复读取。
			if (!frameLoadArray[CurrentFrame])
			{
				generateFrameData(CurrentFrame);
			}
			//MARK 只开单场景：06.2.1 更改场景后，需要将frameSaveArray设为true，表示当前场景需要保存
			frameSaveArray[CurrentFrame] = true;

			changeFrameMode();
			setBusy(false);
			SetNotice("成功切换为场景(" + AllFrameList[CurrentFrame] + ")" ,false);
		}

		/// <summary>
		/// 事件：更改《选择模式》选项后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void modeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//11.13 若未初始化，直接return；
			if (!isInit)
			{
				return;
			}

			SetNotice("正在切换模式", false);
			CurrentMode = modeComboBox.SelectedIndex;

			// 若模式为声控模式mode=1
			// 1.改变几个label的Text; 
			// 2.改变跳变渐变-->是否声控；
			// 3.所有步时间值的调节，改为enabled=false						
			if (CurrentMode == 1)
			{
				for (int i = 0; i < FrameCount; i++)
				{
					this.tdCmComboBoxes[i].Items.Clear();
					this.tdCmComboBoxes[i].Items.AddRange(new object[] { "屏蔽", "跳变" });
					this.tdStNumericUpDowns[i].Hide();					
				}
				this.thirdLabel.Hide();
			}
			else //mode=0，常规模式
			{
				for (int i = 0; i < FrameCount; i++)
				{
					this.tdCmComboBoxes[i].Items.Clear();
					this.tdCmComboBoxes[i].Items.AddRange(new object[] { "跳变", "渐变", "屏蔽" });
					this.tdStNumericUpDowns[i].Show();
					this.thirdLabel.Show();
				}
			}

			changeFrameMode();
			SetNotice("成功切换模式", false);
		}

		/// <summary>
		/// 事件：点击切换《多灯模式|单灯模式》
		/// 	 一.多灯模式：
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
		private void multiLightButton_Click(object sender, EventArgs e)
		{
			// 进入多灯模式
			if (!IsMultiMode)
			{
				enterMultiMode();
			}
			// 退出多灯模式
			else
			{				
				exitMultiMode();
			}
		}

		/// <summary>
		/// 辅助方法：进入同步模式的子类实现
		/// </summary>
		protected override void enterMultiMode()
		{
			if (lightsListView.SelectedIndices.Count < 2)
			{
				MessageBox.Show("请选择至少两个(同型)灯具，否则无法使用多灯模式。");
				return;
			}
			if (!checkSameLights())
			{
				MessageBox.Show("选中的灯具并非都是同一类型的，无法进行编组；请再次选择后重试。");
				return;
			}
			SelectedIndices = new List<int>();
			foreach (int item in lightsListView.SelectedIndices)
			{
				SelectedIndices.Add(item);
			}
			new MultiLightForm(this, isCopyAll, LightAstList, SelectedIndices).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：退出同步的子类实现
		/// </summary>
		protected override void exitMultiMode()
		{
			foreach (ListViewItem item in lightsListView.Items)
			{
				item.BackColor = Color.White;
			}
			RefreshMultiModeButtons(false);

			try
			{
				for (int i = 0; i < lightsListView.Items.Count; i++)
				{
					lightsListView.Items[i].Selected = i == selectedIndex;
				}
				lightsListView.Select();
			}
			catch (Exception ex)
			{
				MessageBox.Show("退出多灯模式选择灯具时出现异常：\n" + ex.Message);
			}
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
				chooseStep(1);
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
				chooseStep(getTotalStep());
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
			chooseStep(stepNum);	
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
			if (e.Button == MouseButtons.Left)
			{
				insertStepClick( false);
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (getCurrentStep() == 0) {
					insertStepClick(false);
					return;
				}
				insertStepClick( true );
			}
		}
		
		/// <summary>
		/// 事件：点击《追加步》(空方法)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addStepButton_Click(object sender, EventArgs e) {  }

		/// <summary>
		///  事件：鼠标左|右键按下《追加步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addStepButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				addStepClick();
			}
			else if (e.Button == MouseButtons.Right)
			{
				addSomeStepClick();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteStepButton_Click(object sender, EventArgs e){	}

		/// <summary>
		/// 事件：鼠标（左|右键）按下《删除步》
		///  1.获取当前步，当前步对应的stepIndex
		///  2.通过stepIndex，DeleteStep(index);
		///  3.获取新步(step删除后会自动生成新的)，并重新渲染stepLabel和vScrollBars
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteStepButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				deleteStepClick();
			}
			else if (e.Button == MouseButtons.Right)
			{
				deleteSomeStepClick();
			}
		}

		/// <summary>
		/// 事件：点击《内置动作》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void actionButton_Click(object sender, EventArgs e)	{	}

		/// <summary>
		/// 事件：鼠标（左|右键）按下《内置动作》
		///  1.获取当前步，当前步对应的stepIndex
		///  2.通过stepIndex，DeleteStep(index);
		///  3.获取新步(step删除后会自动生成新的)，并重新渲染stepLabel和vScrollBars
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void actionButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				actionButtonClick();
			}
			else if (e.Button == MouseButtons.Right)
			{
				rgbButtonClick();
			}
		}	

		/// <summary>
		/// 事件：点击《复制步》
		/// 1.从项目中选择当前灯的当前步，(若当前步为空，则无法复制），把它赋给tempStep数据。
		/// 2.若复制成功，则《粘贴步》按钮可用
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void copyStepButton_Click(object sender, EventArgs e)
		{
			if (getCurrentStepWrapper() == null)
			{
				MessageBox.Show("当前步数据为空，无法复制");
			}
			else
			{
				tempStep = getCurrentStepWrapper();
				pasteStepButton.Enabled = true;
			}
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
		/// 事件：点击《复制多步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiCopyButton_Click(object sender, EventArgs e)	{ multiCopyClick(); }
		
		/// <summary>
		/// 事件：点击《粘贴多步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiPasteButton_Click(object sender, EventArgs e) {
			multiPasteClick();
		}
		
		/// <summary>
		/// 事件：点击《保存素材》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveMaterialButton_Click(object sender, EventArgs e)
		{
			saveMaterial();
		}

		/// <summary>
		/// 事件：点击《使用素材》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void useMaterialButton_Click(object sender, EventArgs e)
		{
			useMaterial();
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
		///  9.16 辅助方法：进入《多灯模式》
		/// </summary>
		/// <param name="captainIndex"></param>
		public override void EnterMultiMode(int captainIndex, bool isCopyAll)
		{
			// 基类中统一的处理
			base.EnterMultiMode(captainIndex, isCopyAll);

			// 以下为单独针对本Form的方法：			
			foreach (ListViewItem item in lightsListView.Items)
			{
				item.BackColor = Color.White;
			}
			lightsAddrLabel.Text = "灯具地址列表：";
			foreach (int lightIndex in SelectedIndices)
			{
				if (lightIndex == selectedIndex)
				{
					lightsAddrLabel.Text += "(" + LightAstList[lightIndex].LightAddr + ") ";
				}
				else
				{
					lightsAddrLabel.Text += LightAstList[lightIndex].LightAddr + " ";
				}
			}
			RefreshMultiModeButtons(true);
		}

		/// <summary>
		/// 辅助方法：退出多灯模式或单灯模式后的相关操作
		/// </summary>
		/// <param name="exit"></param>
		protected override void RefreshMultiModeButtons(bool isMultiMode)
		{
			this.IsMultiMode = isMultiMode;		

			//MARK 只开单场景：15.1 《灯具列表》是否可用，由单灯模式决定
			lightListToolStripMenuItem.Enabled = !isMultiMode;
			lightsListView.Enabled = !isMultiMode;
			frameComboBox.Enabled = !isMultiMode;
			modeComboBox.Enabled = !isMultiMode;
			useFrameButton.Enabled = !isMultiMode;
			groupFlowLayoutPanel.Enabled = LightAstList != null ; // 只要当前工程有灯具，就可以进入编组（再由按钮点击事件进行进一步确认）

			multiLightButton.Text = !isMultiMode ? "多灯模式" : "单灯模式";
		}

		/// <summary>
		/// 辅助方法：重置syncMode的相关属性，ChangeFrameMode、ClearAllData()、更改灯具列表后等？应该进行处理。
		/// </summary>
		public override void EnterSyncMode(bool isSyncMode)
		{
			this.isSyncMode = isSyncMode;
			syncButton.Text = isSyncMode ? "退出同步" : "进入同步";
		}

		/// <summary>
		/// 辅助方法：显示步数标签，并判断stepPanel按钮组是否可用
		/// </summary>		
		protected override void showStepLabel(int currentStep, int totalStep)
		{
			// 1. 设label的Text值					   
			stepLabel.Text = MathHelper.GetFourWidthNumStr(currentStep, true) + "/" + MathHelper.GetFourWidthNumStr(totalStep, false);

			// 2.1 设定《删除步》按钮是否可用
			deleteStepButton.Enabled = totalStep != 0;

			// 2.2 设定《追加步》、《前插入步》《后插入步》按钮是否可用			
			bool insertEnabled = totalStep < MAX_STEP;
			addStepButton.Enabled = insertEnabled;
			insertButton.Enabled = insertEnabled;

			// 2.3 设定《上一步》《下一步》是否可用			
			backStepButton.Enabled = totalStep > 1;
			nextStepButton.Enabled = totalStep > 1;

			// 3. 设定《复制(多)步》是否可用
			copyStepButton.Enabled = currentStep > 0;
			pasteStepButton.Enabled = currentStep > 0 && tempStep != null;

			multiCopyButton.Enabled = currentStep > 0;
			multiPasteButton.Enabled = TempMaterialAst != null && TempMaterialAst.Mode == CurrentMode;

			multiplexButton.Enabled = currentStep > 0;

			// 4.设定统一调整区是否可用						
			groupButton.Enabled = LightAstList != null && lightsListView.SelectedIndices.Count > 0; // 只有工程非空（有灯具列表）且选择项不为空才可点击
			groupFlowLayoutPanel.Enabled = LightAstList != null;
			multiButton.Enabled = totalStep != 0;
			detailMultiButton.Enabled = totalStep != 0;
			soundListButton.Enabled = !string.IsNullOrEmpty(currentProjectName) && CurrentMode == 1;

			//initButton.Enabled = totalStep != 0;
			//zeroButton.Enabled = totalStep != 0;
			//unifyValueButton.Enabled = totalStep != 0;
			//unifyChangeModeButton.Enabled = totalStep != 0;
			//unifyStepTimeButton.Enabled = (totalStep != 0) || (currentMode == 1 ) ;
			//unifyValueNumericUpDown.Enabled = totalStep != 0;
			//unifyChangeModeComboBox.Enabled = totalStep != 0;
			//unifyStepTimeNumericUpDown.Enabled = totalStep != 0;

			// 5.处理选择步数的框及按钮
			chooseStepNumericUpDown.Enabled = totalStep != 0;
			chooseStepNumericUpDown.Minimum = totalStep != 0 ? 1 : 0;
			chooseStepNumericUpDown.Maximum = totalStep;
			chooseStepButton.Enabled = totalStep != 0;

			// 6. 《内置动作》是否可用
			actionButton.Enabled = CurrentMode == 0;
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
					tdTrackBars[tdIndex].Value = Decimal.ToInt32(dd);
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = tdTrackBars[tdIndex].Value - tdTrackBars[tdIndex].SmallChange;
				if (dd >= tdTrackBars[tdIndex].Minimum)
				{
					tdTrackBars[tdIndex].Value = Decimal.ToInt32(dd);
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
			//Console.WriteLine("tdSkinTrackBars_ValueChanged");
			// 1.先找出对应tdSkinTrackBars的index 
			int tongdaoIndex = MathHelper.GetIndexNum(((TrackBar)sender).Name, -1);
			int tdValue = tdTrackBars[tongdaoIndex].Value;

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
			int tongdaoIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
			int tdValue = Decimal.ToInt32(tdValueNumericUpDowns[tongdaoIndex].Value);

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
			//Console.WriteLine("tdValueNumericUpDowns_MouseEnter");
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
			//Console.WriteLine("tdValueNumericUpDowns_MouseWheel");
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
			if (IsMultiMode)
			{
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
			int stepTime = Decimal.ToInt32(tdStNumericUpDowns[tdIndex].Value / EachStepTime2); // 取得的值自动向下取整（即舍去多余的小数位）
			step.TongdaoList[tdIndex].StepTime = stepTime;
			tdStNumericUpDowns[tdIndex].Value = stepTime * EachStepTime2; //若与所见到的值有所区别，则将界面控件的值设为处理过的值

			if (IsMultiMode) {
				copyValueToAll(tdIndex, WHERE.STEP_TIME, stepTime);
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
		private void td_KeyPress(object sender, KeyPressEventArgs e)
		{
			tdUnifyKeyPress(sender, e);
		}

		#endregion

		//MARK：NewMainForm：统一调整框各事件处理
		#region unifyPanel（辅助调节面板）

		/// <summary>
		/// 事件：点击《灯具编组》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void groupButton_Click(object sender, EventArgs e)
		{
			if (lightsListView.SelectedIndices.Count < 1)
			{
				MessageBox.Show("请选择至少一个灯具，否则无法进行编组。");
				return;
			}

			if ( !checkSameLights())
			{
				MessageBox.Show("未选中灯具或选中的灯具并非同一类型，无法进行编组；请再次选择后重试。");
				return;
			}

			SelectedIndices = new List<int>();
			foreach (int item in lightsListView.SelectedIndices)
			{
				SelectedIndices.Add(item);
			}
			new GroupForm(this, LightAstList, SelectedIndices).ShowDialog();
		}

		/// <summary>
		/// 事件：点击《音频链表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void soundListButton_Click(object sender, EventArgs e)
		{
			new SKForm(this, CurrentFrame, frameComboBox.Text).ShowDialog();
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
				
			}
		}

		//为方便定位的空方法
		private void detailMultiButton_Click(object sender, EventArgs e) { }

		/// <summary>
		/// 事件：左右键点击《多步调节》：右键是多步联调
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
			groupInButtonClick(sender);
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
			SaButtonClick(sender);
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

		/// <summary>
		/// 辅助方法：根据selectedIndices，选中lightsListView中的灯具(在这个过程中，就不再生成相应的灯具描述和子属性按钮组了)
		/// </summary>
		protected override void selectLights()
		{
			generateNow = false;
			foreach (ListViewItem item in lightsListView.Items)
			{
				item.Selected = false;
			}
			for (int i = 0; i < SelectedIndices.Count; i++) {
				if (i == SelectedIndices.Count - 1) {
					generateNow = true;
				}
				int lightIndex = SelectedIndices[i];
				lightsListView.Items[lightIndex].Selected = true;
			}
		}
		
		#region 弃用的快捷设置按钮组

		/// <summary>
		/// 事件：点击《全部归零》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zeroButton_Click(object sender, EventArgs e)
		{
			zeroButtonClick();
		}

		/// <summary>
		///  事件：《统一设置通道值numericUpDown》的鼠标滚动事件（只+/-1）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyValueNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
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
		/// 事件：点击《统一通道值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyValueButton_Click(object sender, EventArgs e)
		{
			StepWrapper currentStep = getCurrentStepWrapper();
			if (currentStep == null || currentStep.TongdaoList == null || currentStep.TongdaoList.Count == 0)
			{
				SetNotice("请先选中任意步数，才能进行统一调整！",true);
				return;
			}

			int commonValue = Convert.ToInt32(unifyValueNumericUpDown.Text);
			for (int i = 0; i < currentStep.TongdaoList.Count; i++)
			{
				getCurrentStepWrapper().TongdaoList[i].ScrollValue = commonValue;
			}

			if (IsMultiMode)
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
		private void unifyChangeModeButton_Click(object sender, EventArgs e)
		{
			StepWrapper currentStep = getCurrentStepWrapper();
			if (currentStep == null || currentStep.TongdaoList == null || currentStep.TongdaoList.Count == 0)
			{
				SetNotice("请先选中任意步数，才能进行统一调整！",true);
				return;
			}

			int commonChangeMode = unifyChangeModeComboBox.SelectedIndex;

			for (int i = 0; i < currentStep.TongdaoList.Count; i++)
			{
				getCurrentStepWrapper().TongdaoList[i].ChangeMode = commonChangeMode;
			}
			if (IsMultiMode)
			{
				copyUnifyValueToAll(getCurrentStep(), WHERE.CHANGE_MODE, commonChangeMode);
			}
			RefreshStep();
		}

		/// <summary>
		/// 事件：《统一设置步时间numericUpDown》的鼠标滚动事件（只+/-1）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyStepTimeNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
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
		/// 事件：《统一设置步时间numericUpDown》值被用户主动变化时，需要验证，并主动设置值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyStepTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int stepTime = Decimal.ToInt32(unifyStepTimeNumericUpDown.Value / EachStepTime2);			
			unifyStepTimeNumericUpDown.Value = stepTime * EachStepTime2;
		}
		
		/// <summary>
		/// 事件：点击《统一步时间》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyStepTimeButton_Click(object sender, EventArgs e)
		{
			string buttonText = unifyStepTimeButton.Text;
			if (buttonText.Equals("统一步时间"))
			{
				StepWrapper currentStep = getCurrentStepWrapper();
				if (currentStep == null || currentStep.TongdaoList == null || currentStep.TongdaoList.Count == 0)
				{
					SetNotice("请先选中任意步数，才能进行统一调整！",true);
					return;
				}

				//MARK 步时间 NewMainForm：点击《统一步时间》的处理
				int unifyStepTimeParsed = Decimal.ToInt32(unifyStepTimeNumericUpDown.Value / EachStepTime2);
				for (int i = 0; i < currentStep.TongdaoList.Count; i++)
				{
					getCurrentStepWrapper().TongdaoList[i].StepTime = unifyStepTimeParsed;
				}
				if (IsMultiMode)
				{
					copyUnifyValueToAll(getCurrentStep(), WHERE.STEP_TIME, unifyStepTimeParsed);
				}
				RefreshStep();
			}
			//若按键名称变动，则说明是音频模式
			else
			{
				new SKForm(this,  CurrentFrame, frameComboBox.Text).ShowDialog();
			}
		}

		#endregion 

		#endregion

		//MARK：NewMainForm：playPanel相关点击事件及辅助方法
		#region 灯控调试按钮组（playPanel）点击事件及辅助方法

		/// <summary>
		/// 事件：点击《以网络|串口连接》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void changeConnectMethodButton_Click(object sender, EventArgs e)
		{
			SetNotice("正在切换连接模式,请稍候...", false);		
			isConnectCom = !isConnectCom;
			changeConnectMethodButton.Text = isConnectCom ? "切换为\n网络连接" : "切换为\n串口连接";
			deviceRefreshButton.Text = isConnectCom ? "刷新串口" : "刷新网络";
			SetNotice("成功切换为" + (isConnectCom ? "串口连接" : "网络连接"), false);

			deviceRefresh();  //changeConnectMethodButton_Click : 切换连接后，手动帮用户搜索相应的设备列表。
		}

		/// <summary>
		/// 事件：点击《刷新串口|网络》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceRefreshButton_Click(object sender, EventArgs e)
		{
			deviceRefresh(); //deviceRefreshButton_Click
		}

		/// <summary>
		/// 辅助方法：刷新设备
		/// </summary>
		protected override void deviceRefresh() {

			deviceRefreshButton.Enabled = false;

			//	 刷新前，先清空按键等
			SetNotice("正在" + (isConnectCom ? "刷新串口列表" : "搜索网络设备") + "，请稍候...", false);
			deviceComboBox.Items.Clear();
			deviceComboBox.SelectedIndex = -1;
			deviceComboBox.Text = "";
			deviceComboBox.Enabled = false;			
			deviceConnectButton.Enabled = false;
			Refresh();

			// 刷新串口连接
			if (isConnectCom)
			{
				SerialPortTools comTools = SerialPortTools.GetInstance();
				string[] comList = comTools.GetDMX512DeviceList();
				if (comList != null && comList.Length > 0)
				{
					foreach (string com in comList)
					{
						deviceComboBox.Items.Add(com);
					}
				}
			}
			// 刷新网络设备
			else
			{
				// 先获取本地ip列表，遍历使用这些ip，搜索设备;-->都搜索完毕再统一显示
				IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
				foreach (IPAddress ip in ipe.AddressList)
				{
					if (ip.AddressFamily == AddressFamily.InterNetwork) //当前ip为ipv4时，才加入到列表中
					{
						NetworkConnect.SearchDevice(ip.ToString());
						// 需要延迟片刻，才能找到设备;	故在此期间，主动暂停片刻
						Thread.Sleep(NETWORK_WAITTIME);
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
							deviceComboBox.Items.Add(d2.Value.DeviceName + "(" + d2.Key + ")" + localIPLast);
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
				SetNotice("已刷新" + (isConnectCom ? "串口" : "网络") + "列表，可选择并连接设备进行调试", false);
			}
			else
			{
				SetNotice("未找到可用的" + (isConnectCom ? "串口" : "网络") + "设备，请确认后重试。", false);
			}
			deviceRefreshButton.Enabled = true;
		}

		/// <summary>
		/// 事件：更改《设备列表》选项
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
				MessageBox.Show("未选中可用设备");
			}
		}		
				
		/// <summary>
		/// 事件：点击《连接设备|断开连接》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceConnectButton_Click(object sender, EventArgs e)
		{
			connectButtonClick(deviceComboBox.Text, deviceComboBox.SelectedIndex );
		}		
		
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
				keepButton.Text = "取消\n保持状态";
				isKeepOtherLights = true;
			}
			else //否则( 按钮显示为“保持其他灯状态”）断开连接
			{
				keepButton.Text = "保持状态";
				isKeepOtherLights = false;
			}
			RefreshStep();
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
		public override void EnableConnectedButtons(bool connected,bool previewing)
		{
			base.EnableConnectedButtons(connected, previewing);

			// 《设备列表》《刷新列表》可用与否，与下面《各调试按钮》是否可用刚刚互斥
			changeConnectMethodButton.Enabled = !IsConnected;
			deviceComboBox.Enabled = !IsConnected;
			deviceRefreshButton.Enabled = !IsConnected;

			keepButton.Enabled = IsConnected && !IsPreviewing; 
			previewButton.Enabled = IsConnected ;
			makeSoundButton.Enabled = IsConnected && IsPreviewing;		

			deviceConnectButton.Text = IsConnected ? "断开连接":"连接设备";
			previewButton.Text = IsPreviewing ? "停止预览" : "预览效果";

			//721：刷新当前步(因为有些操作是异步的，可能造成即时的刷新步数，无法进入单灯单步)
			if (IsConnected && !IsPreviewing) {
				RefreshStep();
			}						
		}

		#endregion

		#region 全局辅助方法

		/// <summary>
		/// 设置提示信息
		/// </summary>
		/// <param name="notice"></param>
		public override void SetNotice(string notice,bool msgBoxShow)
		{
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
		/// 事件：点击《Test1》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void testButton1_Click(object sender, EventArgs e)
		{			

		}
		
		/// <summary>
		/// 事件：点击《Test2》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void testButton2_Click(object sender, EventArgs e)
		{
				
		}

		/// <summary>
		/// 事件：点击《wjTest》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void wjTestButton_Click(object sender, EventArgs e)
		{
			new TestForm(GetDBWrapper(false), GlobalIniPath).ShowDialog();
		}

		private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string loadexeName = System.Windows.Forms.Application.ExecutablePath;
			//loadexeName : "D:\\YokiSystem\\Yoki.UI\\bin\\Debug\\Yoki.UI.exe"

			FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(loadexeName);

			String serverFileVersion = string.Format("{0}.{1}.{2}.{3}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);
			MessageBox.Show(serverFileVersion);
		}


		#region 弃用方法

		///// <summary>
		///// 事件：点击《实时调试|关闭实时》
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void realtimeButton_Click(object sender, EventArgs e)
		//{
		//默认情况下，实时调试还没打开，点击后设为打开状态（文字显示为关闭实时调试，图片加颜色）
		//if (!isRealtime)
		//{
		//	realtimeButton.Text = "关闭\n实时调试";
		//	isRealtime = true;
		//	if (!isConnectCom)
		//	{
		//		playTools.StartInternetPreview(myConnect, ConnectCompleted, ConnectAndDisconnectError, eachStepTime);
		//	}
		//	RefreshStep();
		//	SetNotice("已开启实时调试。");
		//}
		//else //否则( 按钮显示为“断开连接”）断开连接
		//{
		//	realtimeButton.Text = "实时调试";
		//	isRealtime = false;
		//	playTools.ResetDebugDataToEmpty();
		//	SetNotice("已退出实时调试。");
		//}
		//}

		/// <summary>
		/// 辅助方法：通过选中的灯具，生成相应的saButtons
		/// </summary>
		//private void generateSAButtons()
		//{
		//	saFlowLayoutPanel.Controls.Clear();
		//	saToolTip.RemoveAll();

		//	if (selectedIndex < 0 || lightAstList == null || lightAstList.Count == 0)
		//	{
		//		MessageBox.Show("generateSAButtons()出错\n[selectedIndex < 0 || lightAstList == null || lightAstList.Count == 0]。");
		//		return;
		//	}

		//	LightAst la = lightAstList[selectedIndex];
		//	try
		//	{
		//		if (la.SawList != null)
		//		{
		//			for (int tdIndex = 0; tdIndex < la.SawList.Count; tdIndex++)
		//			{
		//				addTdSaButtons(la, tdIndex);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show("添加子属性按键出现异常:\n" + ex.Message);
		//	}

		//	// 若当前步为0，则说明该灯具没有步数，则子属性仅显示，但不可用
		//	saFlowLayoutPanel.Enabled = getCurrentStep() != 0;
		//}

		/// <summary>
		/// 辅助方法：抽象出添加通道相关的saButtons，供《切换灯具》及点击《通道名label》时使用
		/// </summary>
		/// <param name="la"></param>
		/// <param name="tdIndex"></param>
		//private void addTdSaButtons(LightAst la, int tdIndex)
		//{

		//	for (int saIndex = 0; saIndex < la.SawList[tdIndex].SaList.Count; saIndex++)
		//	{
		//		SA sa = la.SawList[tdIndex].SaList[saIndex];
		//		Button saButton = new Button
		//		{
		//			Text = sa.SAName,
		//			Size = new Size(68, 20),
		//			Tag = tdIndex + "*" + sa.StartValue,
		//			UseVisualStyleBackColor = true
		//		};
		//		saButton.Click += new EventHandler(saButton_Click);
		//		saToolTip.SetToolTip(saButton, sa.SAName + "\n" + sa.StartValue + " - " + sa.EndValue);
		//		saPanelArray[selectedIndex].Controls.Add(saButton);
		//	}
		//	Console.WriteLine( saPanelArray[selectedIndex] );
		//}

		/// <summary>
		/// 事件：点击《设为初值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void initButton_Click(object sender, EventArgs e)
		{
			initButtonClick();
		}

		#endregion


	}
}
