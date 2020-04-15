using DMX512;
using ICSharpCode.SharpZipLib.Zip;
using LightController.Ast;
using LightController.Common;
using LightController.MyForm.Test;
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
	public partial class NewMainForm:MainFormBase 
	{
		#region 此处定义一些全局变量，用以界面风格的统一设置

		private BorderStyle unifyBorderStyle = BorderStyle.Fixed3D; //统一为局内的所有panel设置统一的BorderStyle		
		private Color unifyColor = SystemColors.Window;
		private Color unifyColor2 = SystemColors.Window;

		#endregion

		public NewMainForm()
		{	
			initGeneralControls(); //几个全局控件的初始化
			InitializeComponent();
			
			Text = SoftwareName + " Dimmer System";// 动态更改软件名称			
			hardwareUpdateToolStripMenuItem.Enabled = IsShowHardwareUpdate;// 动态显示硬件升级按钮
			QDControllerToolStripMenuItem.Enabled = IsLinkOldTools; //旧外设是否进行关联
			CenterControllerToolStripMenuItem.Enabled = IsLinkOldTools;//旧外设是否进行关联
			KeyPressToolStripMenuItem.Enabled = IsLinkOldTools; //旧外设是否进行关联
			testButton.Visible = IsShowTestButton;
			testButton2.Visible = IsShowTestButton;
			wjTestButton.Visible = IsShowTestButton;

			//MARK：添加这一句，会去掉其他线程使用本UI控件时弹出异常的问题(权宜之计，并非长久方案)。
			CheckForIllegalCrossThreadCalls = false;	

			//动态添加32个tdPanel的内容及其监听事件
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
				// tdNoLabel
				// 
				this.tdNoLabels[i].AutoSize = true;
				this.tdNoLabels[i].Location = new System.Drawing.Point(15, 18);
				this.tdNoLabels[i].Name = "tdNoLabel" + (i + 1);
				this.tdNoLabels[i].Size = new System.Drawing.Size(47, 12);
				this.tdNoLabels[i].TabIndex = 3;
				this.tdNoLabels[i].Text = "通道" + (i + 1);
				// 
				// tdTrackBar
				// 
				this.tdTrackBars[i].AutoSize = false;
				this.tdTrackBars[i].BackColor = unifyColor2;
				this.tdTrackBars[i].Location = new System.Drawing.Point(32, 35);
				this.tdTrackBars[i].Maximum = 255;
				this.tdTrackBars[i].Name = "tdTrackBar" + (i + 1);
				this.tdTrackBars[i].Orientation = System.Windows.Forms.Orientation.Vertical;
				this.tdTrackBars[i].Size = new System.Drawing.Size(35, 188);
				this.tdTrackBars[i].TabIndex = 0;
				this.tdTrackBars[i].TickFrequency = 0;
				this.tdTrackBars[i].TickStyle = System.Windows.Forms.TickStyle.None;
				// 
				// tdNameLabel
				// 
				this.tdNameLabels[i].Font = new System.Drawing.Font("宋体", 8F);
				this.tdNameLabels[i].Location = new System.Drawing.Point(17, 47);
				this.tdNameLabels[i].Name = "tdNameLabel" + (i + 1);
				this.tdNameLabels[i].Size = new System.Drawing.Size(14, 153);
				this.tdNameLabels[i].TabIndex = 23;
				this.tdNameLabels[i].Text = "x/y轴转速" + (i + 1);
				this.tdNameLabels[i].TextAlign = System.Drawing.ContentAlignment.TopCenter;
				// 
				// tdValueNumericUpDown
				// 
				this.tdValueNumericUpDowns[i].Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
				this.tdValueNumericUpDowns[i].Location = new System.Drawing.Point(17, 223);
				this.tdValueNumericUpDowns[i].Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
				this.tdValueNumericUpDowns[i].Name = "tdValueNumericUpDown" + (i + 1);
				this.tdValueNumericUpDowns[i].Size = new System.Drawing.Size(50, 20);
				this.tdValueNumericUpDowns[i].TabIndex = 1;
				this.tdValueNumericUpDowns[i].TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

				// 
				// tdCmComboBox
				// 
				this.tdCmComboBoxes[i].FormattingEnabled = true;
				this.tdCmComboBoxes[i].Location = new System.Drawing.Point(17, 247);
				this.tdCmComboBoxes[i].Name = "tdCmComboBox" + (i + 1);
				this.tdCmComboBoxes[i].Size = new System.Drawing.Size(50, 20);
				this.tdCmComboBoxes[i].TabIndex = 2;
				this.tdCmComboBoxes[i].Items.AddRange(new object[] {
			"跳变",
			"渐变",
			"屏蔽"});
				
				// 
				// tdStNumericUpDown
				// 
				this.tdStNumericUpDowns[i].Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
				this.tdStNumericUpDowns[i].Location = new System.Drawing.Point(17, 271);
				this.tdStNumericUpDowns[i].Name = "tdStNumericUpDown" + (i + 1);
				this.tdStNumericUpDowns[i].Size = new System.Drawing.Size(50, 20);
				this.tdStNumericUpDowns[i].TabIndex = 1;
				this.tdStNumericUpDowns[i].TextAlign = System.Windows.Forms.HorizontalAlignment.Center;				
				this.tdStNumericUpDowns[i].DecimalPlaces = 2;				

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

				this.tdPanels[i].Visible = false;
				this.tdPanels[i].Tag = 9999;

				this.tdFlowLayoutPanel.Controls.Add(this.tdPanels[i]);

				tdTrackBars[i].MouseEnter += new EventHandler(tdTrackBars_MouseEnter);
				tdTrackBars[i].MouseWheel += new MouseEventHandler(this.tdTrackBars_MouseWheel);
				tdTrackBars[i].ValueChanged += new System.EventHandler(this.tdTrackBars_ValueChanged);

				tdValueNumericUpDowns[i].MouseEnter += new EventHandler(this.tdValueNumericUpDowns_MouseEnter);
				tdValueNumericUpDowns[i].MouseWheel += new MouseEventHandler(this.tdValueNumericUpDowns_MouseWheel);
				tdValueNumericUpDowns[i].ValueChanged += new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);

				tdCmComboBoxes[i].SelectedIndexChanged += new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);

				tdStNumericUpDowns[i].MouseEnter += new EventHandler(this.tdStepTimeNumericUpDowns_MouseEnter);
				tdStNumericUpDowns[i].MouseWheel += new MouseEventHandler(this.tdStepTimeNumericUpDowns_MouseWheel);
				tdStNumericUpDowns[i].ValueChanged += new EventHandler(this.tdStepTimeNumericUpDowns_ValueChanged);

				tdNameLabels[i].Click += new EventHandler(this.tdNameLabels_Click);
			}
			
			// 场景选项框		
			//添加FramList.txt中的场景列表
			AllFrameList = TextAst.Read(Application.StartupPath + @"\FrameList.txt");				
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
			myToolTip.SetToolTip(useFrameButton, "使用本功能，将以选中的场景数据替换当前的场景数据。");
			myToolTip.SetToolTip(chooseStepButton, "跳转指定步");
			myToolTip.SetToolTip(keepButton, "点击此按钮后，当前未选中的其它灯具将会保持它们最后调整时的状态，方便调试。");

			#region 皮肤 及 panel样式 相关代码
					   		
			setDeepStyle(false);
			if (IniFileAst.GetControlShow(Application.StartupPath, "useSkin") ) {
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

			isInit = true;
		}

		private void NewMainForm_Load(object sender, EventArgs e)
		{
			//启动时刷新可用串口列表，但把显示给删除
			refreshComList();
			SetNotice("");
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
			foreach (TrackBar  item in tdTrackBars)
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
			if (String.IsNullOrEmpty(sskName)  || sskName.Equals("浅色皮肤"))
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
		/// 事件：点击《保存工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveProjectButton_Click(object sender, EventArgs e)
		{
			saveProjectClick();
		}

		/// <summary>
		/// 事件：点击《导出工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exportProjectButton_Click(object sender, EventArgs e)
		{
			exportProjectClick(); 
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
			exportProjectButton.Enabled = enable && lightAstList!=null && lightAstList.Count>0;
			saveFrameButton.Enabled = enable;			
			closeProjectButton.Enabled = enable;

			// 不同MainForm在不同位置的按钮
			useFrameButton.Enabled = enable && lightAstList != null && lightAstList.Count > 0;

			// 菜单栏相关按钮组
			projectToolStripMenuItem.Enabled = enable;		

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
		///辅助方法：添加lightAst列表到主界面内存中,主要供 LightsForm以及OpenProject调用）
		/// --对比删除后，生成新的lightWrapperList；
		/// --lightListView也更新为最新的数据
		/// </summary>
		/// <param name="lightAstList2"></param>
		public override void BuildLightList(IList<LightAst> lightAstList2)
		{
			//先调用统一的操作，填充lightAstList和lightWrapperList
			base.BuildLightList(lightAstList2);

			//针对本Form的处理代码：listView更新为最新数据			
			lightsListView.Items.Clear();
			for (int i = 0; i < lightAstList2.Count; i++)
			{
				// 添加灯具数据到LightsListView中
				lightsListView.Items.Add(new ListViewItem(
						//lightAstList2[i].LightName + ":" + 
						lightAstList2[i].LightType + "\n" +	"(" + lightAstList2[i].LightAddr + ")"
						//+"\n这是备注哦"
						,
					lightImageList.Images.ContainsKey(lightAstList2[i].LightPic) ? lightAstList2[i].LightPic : "灯光图.png"
				)
				{ Tag = lightAstList2[i].LightName + ":" + lightAstList2[i].LightType }
				);
			}
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
			unifyStepTimeNumericUpDown.Maximum = eachStepTime2 * MAX_StTimes; ;
			unifyStepTimeNumericUpDown.Increment = eachStepTime2;

			for (int i = 0; i < 32; i++) {
				tdStNumericUpDowns[i].Maximum = eachStepTime2 * MAX_StTimes;
				tdStNumericUpDowns[i].Increment = eachStepTime2;
			}			
		}

		//MARK 只开单场景：02.0.1 (NewMainForm)改变当前Frame
		protected override void changeCurrentFrame(int frameIndex)
		{
			currentFrame = frameIndex;
			frameComboBox.SelectedIndexChanged -= new System.EventHandler(this.frameComboBox_SelectedIndexChanged);
			frameComboBox.SelectedIndex = currentFrame;
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
				generateLightData();
				generateSAButtons();
			}
		}
		
		/// <summary>
		/// 辅助方法：根据传进来的LightAst对象，修改当前灯具内的显示内容
		/// </summary>
		/// <param name="lightAst"></param>
		protected override void editLightInfo(LightAst lightAst)
		{
			if (lightAst == null)
			{
				currentLightPictureBox.Image = null;
				lightNameLabel.Text = null;
				lightTypeLabel.Text = null;
				lightsAddrLabel.Text = null;
				selectedLightName = "";
				return;
			}

			currentLightPictureBox.Image = lightLargeImageList.Images[lightAst.LightPic] != null ? lightLargeImageList.Images[lightAst.LightPic] : global::LightController.Properties.Resources.灯光图;
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
			stepPanel.Enabled = enable;
		}

		/// <summary>
		/// 辅助方法: 确认选中灯具是否否同一种灯具：是则返回true,否则返回false。
		/// 验证方法：取出第一个选中灯具的名字，若后面的灯具的全名（Tag =lightName + ":" + lightType)与它不同，说明不是同种灯具。（只要一个不同即可判断）
		/// </summary>
		/// <returns></returns>
		private bool checkSameLights()
		{
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
				return;
			}

			//2.将dataWrappers的内容渲染到起VScrollBar中
			else
			{
				for (int i = 0; i < tongdaoList.Count; i++)
				{
					tdTrackBars[i].ValueChanged -= new System.EventHandler(this.tdTrackBars_ValueChanged);
					tdValueNumericUpDowns[i].ValueChanged -= new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);
					tdCmComboBoxes[i].SelectedIndexChanged -= new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);
					tdStNumericUpDowns[i].ValueChanged -= new EventHandler(this.tdStepTimeNumericUpDowns_ValueChanged);

					tdNoLabels[i].Text = "通道" + (startNum + i);
					tdNameLabels[i].Text = tongdaoList[i].TongdaoName;
					myToolTip.SetToolTip(tdNameLabels[i], tongdaoList[i].Remark);
					tdTrackBars[i].Value = tongdaoList[i].ScrollValue;
					tdValueNumericUpDowns[i].Text = tongdaoList[i].ScrollValue.ToString();
					tdCmComboBoxes[i].SelectedIndex = tongdaoList[i].ChangeMode;
					
					//MARK 步时间改动 NewMainForm：主动 乘以时间因子 后 再展示
					tdStNumericUpDowns[i].Text = (tongdaoList[i].StepTime * eachStepTime2).ToString() ;

					tdTrackBars[i].ValueChanged += new System.EventHandler(this.tdTrackBars_ValueChanged);
					tdValueNumericUpDowns[i].ValueChanged += new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);
					tdCmComboBoxes[i].SelectedIndexChanged += new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);
					tdStNumericUpDowns[i].ValueChanged += new EventHandler(this.tdStepTimeNumericUpDowns_ValueChanged);

					tdPanels[i].Show();
				}
				for (int i = tongdaoList.Count; i < 32; i++)
				{
					tdPanels[i].Hide();
				}
			}
		}

		/// <summary>
		///辅助方法：隐藏所有的TdPanel
		/// </summary>
		protected override void hideAllTDPanels()
		{
			for (int i = 0; i < 32; i++)
			{
				tdPanels[i].Hide();
			}
		}

		/// <summary>
		/// 辅助方法：通过选中的灯具，生成相应的saButtons
		/// </summary>
		private void generateSAButtons()
		{
			saFlowLayoutPanel.Controls.Clear();

			LightAst la = lightAstList[selectedIndex];
			for (int tdIndex = 0; tdIndex<la.SawList.Count; tdIndex++)
			{
				addTdSaButtons(la, tdIndex);
			}

			// 若当前步为0，则说明该灯具没有步数，则子属性仅显示，但不可用
			saFlowLayoutPanel.Enabled = getCurrentStep() != 0;				
			saFlowLayoutPanel.Refresh();
		}

		/// <summary>
		/// 事件：点击《saButton》按钮组的任意按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saButton_Click(object sender, EventArgs e)
		{
			if (getCurrentStepWrapper() == null) {
				SetNotice("当前无选中步，不可点击子属性按钮");
				return;
			}

			Button btn = (Button)sender;
			string[] btnTagArr = btn.Tag.ToString().Split('*');
			int tdIndex = int.Parse(btnTagArr[0]);
			int tdValue = int.Parse(btnTagArr[1]);

			getCurrentStepWrapper().TongdaoList[tdIndex].ScrollValue = tdValue;	
			if (isMultiMode)
			{
				copyValueToAll(  tdIndex, WHERE.SCROLL_VALUE, tdValue );
			}

			RefreshStep();
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

		#endregion

		//MARK：SkinMainForm灯具listView相关（右键菜单+位置等）
		#region  灯具listView相关（右键菜单+位置等）

		/// <summary>
		/// 事件：点击《为灯具添加备注》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addLightRemarkToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (selectedIndex == -1)
			{
				Console.WriteLine("尚未选中灯具");
				return;
			}

			lightsListView.SelectedItems[0].SubItems[0].Text += "\nhahah";
		}

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
				lightsListView.Items[lightIndex].ImageKey = tempPicStr;
			}

		}
		
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
			lightsListView.AllowDrop = !isAutoArrange;
			lightsListView.AutoArrange = isAutoArrange;

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
			bool tempAutoArrange = lightsListView.AutoArrange;
			lightsListView.AutoArrange = true;
			lightsListView.AutoArrange = tempAutoArrange;
			lightsListView.Update();
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
			iniFileAst.WriteInt("Common", "Count", lightsListView.Items.Count);
			for (int i = 0; i < lightsListView.Items.Count; i++)
			{
				iniFileAst.WriteInt("Position", i + "X", lightsListView.Items[i].Position.X);
				iniFileAst.WriteInt("Position", i + "Y", lightsListView.Items[i].Position.Y);
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
		/// 辅助方法：是否使能重新加载灯具图片
		/// </summary>
		/// <param name="enable"></param>
		protected override void enableRefreshPic(bool enable)
		{
			refreshPicToolStripMenuItem.Enabled = enable;
		}

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
		
		#endregion

		#region 几个显示或隐藏面板的菜单项

		/// <summary>
		/// 辅助方法：点击《隐藏|显示主菜单面板》菜单项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hideMenuPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mainMenuStrip.Visible = !mainMenuStrip.Visible;
			hideMenuStriplToolStripMenuItem.Text = mainMenuStrip.Visible ? "隐藏主菜单面板" : "显示主菜单面板";			
		}

		/// <summary>
		/// 辅助方法：点击《隐藏|显示工程面板》菜单项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hideProjectPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			projectPanel.Visible = !projectPanel.Visible;
			hideProjectPanelToolStripMenuItem.Text = projectPanel.Visible ? "隐藏工程面板" : "显示工程面板";			
		}

		/// <summary>
		/// 辅助方法：点击《隐藏|显示辅助面板》菜单项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hideUnifyPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			unifyPanel.Visible = !unifyPanel.Visible;
			hideUnifyPanelToolStripMenuItem.Text = unifyPanel.Visible ? "隐藏辅助面板" : "显示辅助面板";			
		}

		/// <summary>
		/// 辅助方法：点击《隐藏|显示调试面板》菜单项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hidePlayPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			playBasePanel.Visible = !playBasePanel.Visible;
			hidePlayPanelToolStripMenuItem.Text = playBasePanel.Visible ? "隐藏调试面板" : "显示调试面板";		
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
			SetNotice("正在切换场景,请稍候...");			

			// 只要更改了场景，直接结束预览
			endview();

			DialogResult dr = MessageBox.Show("切换场景前，是否保存之前场景(" + AllFrameList[currentFrame] + ")？",
				"保存场景?",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Question);
			if (dr == DialogResult.OK)
			{
				saveFrameClick();
				//MARK 只开单场景：06.0.1 切换场景时，若选择保存之前场景，则frameSaveArray设为false，意味着以后不需要再保存了。
				frameSaveArray[currentFrame] = false;				
			}

			currentFrame = frameComboBox.SelectedIndex;
			//MARK 只开单场景：06.1.1 更改场景时，只有frameLoadArray为false，才需要从DB中加载相关数据；若为true，则说明已经加载因而无需重复读取。
			if (!frameLoadArray[currentFrame])
			{				
				generateFrameData(currentFrame);
			}
			//MARK 只开单场景：06.2.1 更改场景后，需要将frameSaveArray设为true，表示当前场景需要保存
			frameSaveArray[currentFrame] = true;

			changeFrameMode();
			setBusy(false);
			SetNotice("成功切换为场景("+AllFrameList[currentFrame]+")");
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

			SetNotice("正在切换模式");
			currentMode = modeComboBox.SelectedIndex;
			// 若模式为声控模式mode=1
			// 1.改变几个label的Text; 
			// 2.改变跳变渐变-->是否声控；
			// 3.所有步时间值的调节，改为enabled=false						
			if (currentMode == 1)
			{
				for (int i = 0; i < FrameCount; i++)
				{
					this.tdCmComboBoxes[i].Items.Clear();
					this.tdCmComboBoxes[i].Items.AddRange(new object[] { "屏蔽", "跳变" });
					this.tdStNumericUpDowns[i].Hide();
					this.thirdLabel.Hide();
				}

				unifyChangeModeComboBox.Items.Clear();
				unifyChangeModeComboBox.Items.AddRange(new object[] { "屏蔽", "跳变" });
				unifyChangeModeComboBox.SelectedIndex = 0;
				unifyChangeModeButton.Text = "统一声控";

				unifyStepTimeNumericUpDown.Hide();
				unifyStepTimeButton.Text = "修改此音频场景全局设置";
				unifyStepTimeButton.Location = new Point(10,299);
				unifyStepTimeButton.Size = new System.Drawing.Size(154, 23);

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
				
				unifyChangeModeComboBox.Items.Clear();
				unifyChangeModeComboBox.Items.AddRange(new object[] { "跳变", "渐变", "屏蔽" });
				unifyChangeModeComboBox.SelectedIndex = 0;
				unifyChangeModeButton.Text = "统一跳渐变";

				unifyStepTimeNumericUpDown.Show();
				unifyStepTimeButton.Text = "统一步时间";
				unifyStepTimeButton.Location = new Point(82,299);
				unifyStepTimeButton.Size = new System.Drawing.Size(83, 23);
			}

			changeFrameMode();			
			SetNotice("成功切换模式");
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
			if (!isMultiMode)
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
				selectedIndices = new List<int>();
				foreach (int item in lightsListView.SelectedIndices)
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
					lightsListView.Items[lightIndex].BackColor = Color.White;
				}
				enableSingleMode(true);
			}
		}
	
		/// <summary>
		/// 事件：点击切换《进入同步|退出同步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void syncButton_Click(object sender, EventArgs e)
		{
			// 如果当前已经是同步模式，则退出同步模式，这比较简单，不需要进行任何比较，直接操作即可。
			if (isSyncMode)
			{
				isSyncMode = false;
				syncButton.Text = "进入同步";
				return;
			}

			// 异步时，要切换到同步模式，需要先进行检查。
			if (!CheckAllSameStepCounts())
			{
				MessageBox.Show("当前场景所有灯具步数不一致，无法进入同步模式。");
				return;
			}
			// 通过检查，则可以进行设值等相关操作了
			isSyncMode = true;
			syncButton.Text = "退出同步";
		}

		/// <summary>
		///  事件：点击《上一步》
		///  先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backStepButton_Click(object sender, EventArgs e)
		{
			backStepClick();
		}	

		/// <summary>
		///  事件：点击《下一步》
		///  先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nextStepButton_Click(object sender, EventArgs e)
		{
			nextStepClick();
		}		

		/// <summary>
		/// 事件：点击《跳转步》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chooseStepButton_Click(object sender, EventArgs e)
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
			bool insertBefore = ((Button)sender).Name.Equals("insertBeforeButton"); // 插入的方式：前插(true）还是后插（false)		
			insertStepClick(insertBefore);
		}
		
		/// <summary>
		/// 事件：点击《追加步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addStepButton_Click(object sender, EventArgs e)
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
		private void deleteStepButton_Click(object sender, EventArgs e)
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
		private void multiCopyButton_Click(object sender, EventArgs e)
		{
			multiCopyClick();
		}

		/// <summary>
		/// 事件：点击《粘贴多步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiPasteButton_Click(object sender, EventArgs e)
		{
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
					lightsListView.Items[lightIndex].BackColor = Color.LightSkyBlue;
				}
				else
				{
					lightsAddrLabel.Text += lightAstList[lightIndex].LightAddr + " ";
					lightsListView.Items[lightIndex].BackColor = Color.SkyBlue;
				}
			}

			enableSingleMode(false);
		}

		/// <summary>
		/// 辅助方法：退出多灯模式或单灯模式后的相关操作
		/// </summary>
		/// <param name="isSingleMode"></param>
		protected override void enableSingleMode(bool isSingleMode)
		{
			isMultiMode = !isSingleMode;

			//MARK 只开单场景：15.1 《灯具列表》是否可用，由单灯模式决定
			lightListToolStripMenuItem.Enabled = isSingleMode;
			lightsListView.Enabled = isSingleMode;		
			frameComboBox.Enabled = isSingleMode;
			modeComboBox.Enabled = isSingleMode;
			useFrameButton.Enabled = isSingleMode;

			multiLightButton.Text = isSingleMode ? "多灯模式" : "单灯模式";
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

			// 3. 设定《复制(多)步》是否可用
			copyStepButton.Enabled = currentStep > 0;
			pasteStepButton.Enabled = currentStep > 0 && tempStep != null;

			multiCopyButton.Enabled = currentStep > 0;
			multiPasteButton.Enabled = TempMaterialAst != null && TempMaterialAst.Mode == currentMode;

			saveFrameButton.Enabled = currentStep > 0;

			// 4.设定统一调整区是否可用			
			zeroButton.Enabled = totalStep != 0;
			initButton.Enabled = totalStep != 0; 
			multiButton.Enabled = totalStep != 0;
			unifyValueButton.Enabled = totalStep != 0;
			unifyChangeModeButton.Enabled = totalStep != 0;
			unifyStepTimeButton.Enabled = (totalStep != 0) || (currentMode == 1 ) ;
			unifyValueNumericUpDown.Enabled = totalStep != 0;
			unifyChangeModeComboBox.Enabled = totalStep != 0;
			unifyStepTimeNumericUpDown.Enabled = totalStep != 0;

			// 5.处理选择步数的框及按钮
			chooseStepNumericUpDown.Enabled = totalStep != 0;
			chooseStepNumericUpDown.Minimum = totalStep != 0 ? 1 : 0;
			chooseStepNumericUpDown.Maximum = totalStep;
			chooseStepButton.Enabled = totalStep != 0;

			// 6.判断子属性按钮组是否可用			
			saFlowLayoutPanel.Visible = totalStep != 0;
			saFlowLayoutPanel.Enabled = totalStep != 0;
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
			int tdIndex = MathAst.GetIndexNum(((TrackBar)sender).Name, -1);
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
			int tdIndex = MathAst.GetIndexNum(((TrackBar)sender).Name, -1);
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
					tdTrackBars[tdIndex].Value = Decimal.ToInt16(dd);
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = tdTrackBars[tdIndex].Value - tdTrackBars[tdIndex].SmallChange;
				if (dd >= tdTrackBars[tdIndex].Minimum)
				{
					tdTrackBars[tdIndex].Value = Decimal.ToInt16(dd);
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
			int tongdaoIndex = MathAst.GetIndexNum(((TrackBar)sender).Name, -1);
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
			int tongdaoIndex = MathAst.GetIndexNum(((NumericUpDown)sender).Name, -1);
			int tdValue = Decimal.ToInt16(tdValueNumericUpDowns[tongdaoIndex].Value);

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
			int changeMode = tdCmComboBoxes[tdIndex].SelectedIndex;
			step.TongdaoList[tdIndex].ChangeMode = tdCmComboBoxes[tdIndex].SelectedIndex;

			//3.多灯模式下，需要把调整复制到各个灯具去
			if (isMultiMode)
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
			int tdIndex = MathAst.GetIndexNum(((NumericUpDown)sender).Name, -1);
			tdStNumericUpDowns[tdIndex].Select();
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
			int tdIndex = MathAst.GetIndexNum(((NumericUpDown)sender).Name, -1);

			//2.取出recentStep，这样就能取出一个步数，使用取出的index，给stepWrapper.TongdaoList[index]赋值
			StepWrapper step = getCurrentStepWrapper();

			// MARK 步时间改动 NewMainForm：处理为数据库所需数值：将 (显示的步时间* 时间因子)后再放入内存
			int stepTime = Decimal.ToInt16(tdStNumericUpDowns[tdIndex].Value / eachStepTime2 ); // 取得的值自动向下取整（即舍去多余的小数位）
			step.TongdaoList[tdIndex].StepTime = stepTime;
			tdStNumericUpDowns[tdIndex].Value = stepTime * eachStepTime2 ; //若与所见到的值有所区别，则将界面控件的值设为处理过的值

			if (isMultiMode)	{
				copyValueToAll(tdIndex, WHERE.STEP_TIME, stepTime);
			}
		}

		/// <summary>
		/// 事件：点击《tdNameLabels》时，右侧的子属性按钮组，会显示当前通道相关的子属性，其他通道的子属性，则隐藏掉
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdNameLabels_Click(object sender, EventArgs e)
		{
			saFlowLayoutPanel.Controls.Clear();
			LightAst la = lightAstList[selectedIndex];
			int tdIndex = MathAst.GetIndexNum(((Label)sender).Name, -1);
			addTdSaButtons(la, tdIndex);
			saFlowLayoutPanel.Refresh();
		}

		/// <summary>
		/// 辅助方法：抽象出添加通道相关的saButtons，供《切换灯具》及点击《通道名label》时使用
		/// </summary>
		/// <param name="la"></param>
		/// <param name="tdIndex"></param>
		private void addTdSaButtons(LightAst la, int tdIndex)
		{
			for (int saIndex = 0; saIndex < la.SawList[tdIndex].SaList.Count; saIndex++)
			{
				SA sa = la.SawList[tdIndex].SaList[saIndex];
				Button saButton = new Button
				{
					Text = sa.SAName,
					Size = new Size(68, 20),
					Tag = tdIndex + "*" + sa.StartValue,
					UseVisualStyleBackColor = true
				};
				saButton.Click += new EventHandler(saButton_Click);
				myToolTip.SetToolTip(saButton, sa.SAName +"\n"  + sa.StartValue+" - " + sa.EndValue);
				saFlowLayoutPanel.Controls.Add(saButton);
			}
		}

		#endregion

		//MARK：NewMainForm：统一调整框各事件处理
		#region unifyPanel（辅助调节面板）

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
		/// 事件：点击《设为初值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void initButton_Click(object sender, EventArgs e)
		{
			initButtonClick();
		}

		/// <summary>
		/// 事件：点击《多步调节》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiButton_Click(object sender, EventArgs e)
		{
			multiButtonClick();
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
				MessageBox.Show("请先选中任意步数，才能进行统一调整！");
				SetNotice("请先选中任意步数，才能进行统一调整！");
				return;
			}

			int commonValue = Convert.ToInt16(unifyValueNumericUpDown.Text);
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
		private void unifyChangeModeButton_Click(object sender, EventArgs e)
		{
			StepWrapper currentStep = getCurrentStepWrapper();
			if (currentStep == null || currentStep.TongdaoList == null || currentStep.TongdaoList.Count == 0)
			{
				MessageBox.Show("请先选中任意步数，才能进行统一调整！");
				SetNotice("请先选中任意步数，才能进行统一调整！");
				return;
			}

			int commonChangeMode = unifyChangeModeComboBox.SelectedIndex;

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
			int stepTime = Decimal.ToInt16(unifyStepTimeNumericUpDown.Value / eachStepTime2);			
			unifyStepTimeNumericUpDown.Value = stepTime * eachStepTime2;
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
					MessageBox.Show("请先选中任意步数，才能进行统一调整！");
					SetNotice("请先选中任意步数，才能进行统一调整！");
					return;
				}

				//MARK 步时间改动 NewMainForm：点击《统一步时间》的处理
				int unifyStepTimeParsed = Decimal.ToInt16(unifyStepTimeNumericUpDown.Value / eachStepTime2);
				for (int i = 0; i < currentStep.TongdaoList.Count; i++)
				{
					getCurrentStepWrapper().TongdaoList[i].StepTime = unifyStepTimeParsed;
				}
				if (isMultiMode)
				{
					copyUnifyValueToAll(getCurrentStep(), WHERE.STEP_TIME, unifyStepTimeParsed);
				}
				RefreshStep();
			}
			//若按键名称变动，则说明是音频模式
			else
			{
				new SKForm(this,  currentFrame, frameComboBox.Text).ShowDialog();
			}
		}

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
			SetNotice("正在切换连接模式,请稍候...");
			Refresh();
			isConnectCom = !isConnectCom;
			changeConnectMethodButton.Text = isConnectCom ? "切换为\n网络连接" : "切换为\n串口连接";
			deviceRefreshButton.Text = isConnectCom ? "刷新串口" : "刷新网络";
			SetNotice("成功切换为" + (isConnectCom ? "串口连接" : "网络连接") );

			deviceRefreshButton_Click(null, null);  // 切换连接后，手动帮用户搜索相应的设备列表。
		}

		/// <summary>
		/// 事件：点击《刷新串口|网络》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceRefreshButton_Click(object sender, EventArgs e)
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

		/// <summary>
		/// 事件：更改《设备列表》选项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			comName = deviceComboBox.Text;
			if (!comName.Trim().Equals(""))
			{
				deviceConnectButton.Enabled = true;
			}
			else
			{
				deviceConnectButton.Enabled = false;
				MessageBox.Show("未选中可用串口");
			}
		}		
				
		/// <summary>
		/// 事件：点击《连接设备|断开连接》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectButton_Click(object sender, EventArgs e)
		{
			connectButtonClick();
		}		

		/// <summary>
		/// 事件：点击《实时调试|关闭实时》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void realtimeButton_Click(object sender, EventArgs e)
		{
			// 默认情况下，实时调试还没打开，点击后设为打开状态（文字显示为关闭实时调试，图片加颜色）
			if (!isRealtime)
			{				
				realtimeButton.Text = "关闭\n实时调试";
				isRealtime = true;				
				if (!isConnectCom)
				{
					playTools.StartInternetPreview(selectedIpAst.DeviceIP, new NetworkDebugReceiveCallBack(this), eachStepTime);
				}				
				RefreshStep();
				SetNotice("已开启实时调试。");
			}
			else //否则( 按钮显示为“断开连接”）断开连接
			{
				realtimeButton.Text = "实时调试";
				isRealtime = false;
				playTools.ResetDebugDataToEmpty();
				SetNotice("已退出实时调试。");
			}
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
				//keepButton.Image = global::LightController.Properties.Resources.保持状态2;
				keepButton.Text = "取消\n保持状态";
				isKeepOtherLights = true;
			}
			else //否则( 按钮显示为“保持其他灯状态”）断开连接
			{
				//keepButton.Image = global::LightController.Properties.Resources.保持状态1;
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
			if (lightAstList == null || lightAstList.Count == 0)
			{
				MessageBox.Show("当前工程还未添加灯具，无法预览。");
				return;
			}

			setBusy(true);
			SetNotice("正在生成预览数据，请稍候...");
			try
			{
				DataConvertUtils.SaveProjectFileByPreviewData(GetDBWrapper(false), GlobalIniPath, currentFrame, new PreviewCallBack(this));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally {
				SetNotice("正在预览效果");
				setBusy(false);
			}
		}

		/// <summary>
		/// 事件：点击《触发音频》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void makeSoundButton_Click(object sender, EventArgs e)
		{
			playTools.MusicControl();
			//SetNotice("触发音频");
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
		/// 辅助方法：重新搜索com列表：供启动时及需要重新搜索设备时使用。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshComList()
		{
			SetNotice("正在刷新串口列表，请稍候...");
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
				SetNotice("已刷新串口列表，可选择并连接设备进行调试");
			}
			else
			{
				deviceComboBox.Text = "";
				deviceComboBox.Enabled = false;
				deviceComboBox.Enabled = false;
				SetNotice("未找到可用串口。");
			}

		}

		/// <summary>
		/// 辅助方法：重新搜索ip列表-》填入deviceComboBox中
		/// </summary>
		private void refreshNetworkList()
		{
			SetNotice("正在搜索网络设备，请稍候...");
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
					Thread.Sleep(MainFormBase.NETWORK_WAITTIME);
				}
			}

			allNetworkDevices = new List<NetworkDeviceInfo>();

			Dictionary<string, Dictionary<string, NetworkDeviceInfo>> allDevices =  connectTools.GetDeivceInfos();			
			if (allDevices.Count > 0)
			{
				foreach (KeyValuePair<string, Dictionary<string, NetworkDeviceInfo>> device in allDevices)
				{
					foreach (KeyValuePair<string, NetworkDeviceInfo> d2 in device.Value)
					{
						string localIPLast = device.Key.ToString().Substring(device.Key.ToString().LastIndexOf("."));
						deviceComboBox.Items.Add(d2.Value.DeviceName + "(" + d2.Key + ")" + localIPLast);
						ipaList.Add(new IPAst() { LocalIP = device.Key, DeviceIP = d2.Value.DeviceIp, DeviceName = d2.Value.DeviceName });
						allNetworkDevices.Add(d2.Value);
					}
				}
			}

			if (ipaList.Count > 0)
			{
				deviceComboBox.Enabled = true;
				deviceComboBox.SelectedIndex = 0;
				SetNotice("成功获取网络设备列表，可选择并连接设备进行调试。");
			}
			else
			{				
				SetNotice("未找到可用的网络设备，请确认后重试。");
			}
		}

		/// <summary>
		///  辅助方法：《连接设备按钮组》是否显示
		/// </summary>
		/// <param name="v"></param>
		public override void EnableConnectedButtons(bool connected)
		{
			// 《设备列表》《刷新列表》可用与否，与下面《各调试按钮》是否可用刚刚互斥
			changeConnectMethodButton.Enabled = !connected;
			deviceComboBox.Enabled = !connected;
			deviceRefreshButton.Enabled = !connected;
									
			realtimeButton.Enabled = connected;
			keepButton.Enabled = connected;
			makeSoundButton.Enabled = connected;
			previewButton.Enabled = connected;
			endviewButton.Enabled = connected;

			// 是否连接
			isConnected = connected;
			deviceConnectButton.Text = isConnected ? "断开连接" : "连接设备";
		}

		/// <summary>
		/// 辅助方法：点击《连接设备|断开连接》的子类实现
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
						MessageBox.Show("未选中可用串口，请选中后再点击连接。");
						return;
					}
					playTools.ConnectDevice(comName);
					EnableConnectedButtons(true);
				}
				else
				{
					if (String.IsNullOrEmpty(comName) || deviceComboBox.SelectedIndex < 0)
					{
						MessageBox.Show("未选中可用网络连接，请选中后再点击连接。");
						return;
					}
					selectedIpAst = ipaList[deviceComboBox.SelectedIndex];
					ConnectTools.GetInstance().Start(selectedIpAst.LocalIP);
					if (ConnectTools.GetInstance().Connect(allNetworkDevices[deviceComboBox.SelectedIndex]))
					{
						playTools.StartInternetPreview(selectedIpAst.DeviceIP, new NetworkDebugReceiveCallBack(this), eachStepTime);
						SetNotice("网络设备连接成功。");
					}
					else {
						MessageBox.Show("设备连接失败，请重试。");
					}
				}
			}
			else //否则( 按钮显示为“断开连接”）断开连接
			{
				playTools.StopSend();
				if (isConnectCom)
				{					
					playTools.CloseDevice();					
				}
				else
				{
					playTools.StopInternetPreview(new NetworkEndDebugReceiveCallBack());			
				}
				EnableConnectedButtons(false);
				SetNotice("已断开连接。");
			}
		}

		#endregion

		#region 全局辅助方法

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

		/// <summary>
		/// 事件：点击《Test1》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void testButton_Click(object sender, EventArgs e)
		{
			Console.WriteLine("场景名\tfsa\tfla");
			for (int frameIndex = 0; frameIndex < FrameCount; frameIndex++)
			{
				Console.WriteLine(AllFrameList[frameIndex] + " : " + frameSaveArray[frameIndex] + " - " + frameLoadArray[frameIndex]);
			}
		}

		/// <summary>
		/// 事件：点击《Test2》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void testButton2_Click(object sender, EventArgs e)
		{
			////GenerateSourceProject();

			//setBusy(true);
			//SetNotice("正在压缩文件");
			//Refresh();

			//string dirPath = @"Z:\MC100\mcdata\demo1";
			//string zipPath = @"Z:\GUAN\demo1.zip";
			//ZipAst.CompressAllToZip(dirPath, zipPath, 0, null, @"Z:\MC100\mcdata\");

			//SetNotice("已完成压缩");
			//setBusy(false);
		}

		private void wjTestButton_Click(object sender, EventArgs e)
		{
			new TestForm().ShowDialog();
		}
	}
}
