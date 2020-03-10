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
				this.tdNoLabels[i].Name = "tdNoLabel" + (i+1);
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
				this.tdTrackBars[i].Name = "tdTrackBar" + (i+1);
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
				this.tdNameLabels[i].Name = "tdNameLabel" + (i + 1);
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
				this.tdValueNumericUpDowns[i].Name = "tdValueNumericUpDown" + (i + 1);
				this.tdValueNumericUpDowns[i].Size = new System.Drawing.Size(50, 20);
				this.tdValueNumericUpDowns[i].TabIndex = 1;
				this.tdValueNumericUpDowns[i].TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

				// 
				// tdCmComboBox1
				// 
				this.tdCmComboBoxes[i].FormattingEnabled = true;
				this.tdCmComboBoxes[i].Location = new System.Drawing.Point(12, 247);
				this.tdCmComboBoxes[i].Name = "tdCmComboBox" + (i + 1);
				this.tdCmComboBoxes[i].Size = new System.Drawing.Size(60, 20);
				this.tdCmComboBoxes[i].TabIndex = 2;
				this.tdCmComboBoxes[i].Items.AddRange(new object[] {
			"跳变",
			"渐变",
			"屏蔽"});

				// 
				// tdStNumericUpDown1
				// 
				this.tdStNumericUpDowns[i].Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
				this.tdStNumericUpDowns[i].Location = new System.Drawing.Point(12, 271);
				this.tdStNumericUpDowns[i].Name = "tdStNumericUpDown" + (i + 1);
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

			}
		}


		#region 几个基类的纯虚函数在子类的实现

		/// <summary>
		///  辅助方法：将所有工程相关的按钮（灯具列表、工程升级、全局设置、摇麦设置）Enabled设为传入bool值
		/// </summary>
		/// <param name="v"></param>
		protected override void enableGlobalSet(bool enable) {
			projectToolStripMenuItem.Enabled = enable;
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
		/// 设置提示信息
		/// </summary>
		/// <param name="notice"></param>
		public override void SetNotice(string notice)
		{
			myStatusLabel.Text = notice;
			this.Refresh();
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

		#region 菜单栏点击事件

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
			lightsListView.Items.Clear();
			for (int i = 0; i < lightAstList2.Count; i++)
			{
				// 添加灯具数据到LightsListView中
				lightsListView.Items.Add(new ListViewItem(
						//lightAstList2[i].LightName + ":" + 
						lightAstList2[i].LightType +
						"\n" +
						"(" + lightAstList2[i].LightAddr + ")",
					lightLargeImageList.Images.ContainsKey(lightAstList2[i].LightPic) ? lightAstList2[i].LightPic : "灯光图.png"
				)
				{ Tag = lightAstList2[i].LightName + ":" + lightAstList2[i].LightType }
				);
			}

			// 2.最后处理通道显示：每次调用此方法后应该隐藏通道数据，避免误操作。
			hideAllTDPanels();
		}
	

		/// <summary>
		///辅助方法：隐藏所有的TdPanel
		/// </summary>
		private void hideAllTDPanels()
		{
			for (int i = 0; i < 32; i++)
			{
				tdPanels[i].Hide();
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
				for (int i = 0; i < tongdaoList.Count; i++)
				{
					tdTrackBars[i].ValueChanged -= new System.EventHandler(this.tdTrackBars_ValueChanged);
					tdValueNumericUpDowns[i].ValueChanged -= new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);
					tdCmComboBoxes[i].SelectedIndexChanged -= new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);
					tdStNumericUpDowns[i].ValueChanged -= new EventHandler(this.tdStepTimeNumericUpDowns_ValueChanged);

					tdNoLabels[i].Text = "通道" + (startNum + i);
					tdNameLabels[i].Text = tongdaoList[i].TongdaoName;
					myToolTip.SetToolTip(tdNameLabels[i], tongdaoList[i].TongdaoName);
					tdTrackBars[i].Value = tongdaoList[i].ScrollValue;
					tdValueNumericUpDowns[i].Text = tongdaoList[i].ScrollValue.ToString();
					tdCmComboBoxes[i].SelectedIndex = tongdaoList[i].ChangeMode;
					tdStNumericUpDowns[i].Text = tongdaoList[i].StepTime.ToString();					

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

		/// <summary>
		/// 事件：点击《保存工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveProjectButton_Click(object sender, EventArgs e)
		{
			SetNotice("正在保存工程,请稍候...");
			setBusy(true);
			saveAll();
			setBusy(false);
			SetNotice("成功保存工程");
		}


		#region tdPanel的监听事件

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
			int tdValue = Convert.ToInt16(Double.Parse(tdValueNumericUpDowns[tongdaoIndex].Text));

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
		///  辅助方法:根据当前《 变动方式》选项 是否屏蔽，处理相关通道是否可设置
		///  --9.4禁用此功能，即无论是否屏蔽，
		/// </summary>
		/// <param name="tongdaoIndex">tongdaoList的Index</param>
		/// <param name="shielded">是否被屏蔽</param>
		private void enableTongdaoEdit(int tongdaoIndex, bool shielded)
		{
			tdTrackBars[tongdaoIndex].Enabled = shielded;
			tdValueNumericUpDowns[tongdaoIndex].Enabled = shielded;
			tdStNumericUpDowns[tongdaoIndex].Enabled = shielded;
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
			int stepTime = Decimal.ToInt32(tdStNumericUpDowns[tdIndex].Value);
			step.TongdaoList[tdIndex].StepTime = stepTime;
			//TDOO : 3.10 td设为实际步时间长度（直接换算好）
			//this.tdTrueTimeLabels[tdIndex].Text = (float)step.TongdaoList[tdIndex].StepTime * eachStepTime / 1000 + "s";

			if (isMultiMode)
			{
				copyValueToAll(tdIndex, WHERE.STEP_TIME, stepTime);
			}
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
			SetNotice("正在切换场景...");

			// 只要更改了场景，直接结束预览
			endview();

			DialogResult dr = MessageBox.Show("切换场景前，是否保存之前场景(" + AllFrameList[frame] + ")？",
				"保存场景",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Question);
			if (dr == DialogResult.OK)
			{
				setBusy(true);
				saveFrame();
				setBusy(false);
			}

			frame = frameComboBox.SelectedIndex;
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
		private void modeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//11.13 若未初始化，直接return；
			if (!isInit)
			{
				return;
			}

			mode = modeComboBox.SelectedIndex;
			// 若模式为声控模式mode=1
			// 1.改变几个label的Text; 
			// 2.改变跳变渐变-->是否声控；
			// 3.所有步时间值的调节，改为enabled=false						
			if (mode == 1)
			{
				for (int i = 0; i < FrameCount; i++)
				{
					this.tdCmComboBoxes[i].Items.Clear();
					this.tdCmComboBoxes[i].Items.AddRange(new object[] { "屏蔽", "跳变" });
					this.tdStNumericUpDowns[i].Hide();
					//this.tdTrueTimeLabels[i].Hide();
				}
				unifyChangeModeButton.Text = "统一声控";
				unifyChangeModeComboBox.Items.Clear();
				unifyChangeModeComboBox.Items.AddRange(new object[] { "屏蔽", "跳变" });
				unifyChangeModeComboBox.SelectedIndex = 0;

				unifyStepTimeNumericUpDown.Hide();
				unifyStepTimeButton.Text = "修改此音频场景全局设置";
				unifyStepTimeButton.Size = new System.Drawing.Size(210, 27);

				//thirdLabel1.Hide();
				//thirdLabel2.Hide();
				//thirdLabel3.Hide();
			}
			else //mode=0，常规模式
			{
				for (int i = 0; i < FrameCount; i++)
				{
					this.tdCmComboBoxes[i].Items.Clear();
					this.tdCmComboBoxes[i].Items.AddRange(new object[] { "跳变", "渐变", "屏蔽" });
					this.tdStNumericUpDowns[i].Show();
					//this.tdTrueTimeLabels[i].Show();
				}
				unifyChangeModeButton.Text = "统一跳渐变";
				unifyChangeModeComboBox.Items.Clear();
				unifyChangeModeComboBox.Items.AddRange(new object[] { "跳变", "渐变", "屏蔽" });
				unifyChangeModeComboBox.SelectedIndex = 0;

				unifyStepTimeNumericUpDown.Show();
				unifyStepTimeButton.Text = "统一步时间";
				unifyStepTimeButton.Size = new System.Drawing.Size(111, 27);

				//thirdLabel1.Show();
				//thirdLabel2.Show();
				//thirdLabel3.Show();
			}

			if (lightAstList != null && lightAstList.Count > 0)
			{
				changeFrameMode();
			}
		}

		/// <summary>
		/// 辅助方法： 改变了模式和场景后的操作		
		/// </summary>
		private void changeFrameMode()
		{
			// 9.2 不可让selectedIndex为-1  , 否则会出现数组越界错误
			if (selectedIndex == -1)
			{
				return;
			}

			/// 添加处理SyncSkinButton的显示（Visible和 Text)，以及相应的全局变量isSysn；
			ResetSyncMode();

			//最后都要用上RefreshStep()
			RefreshStep();
		}

	
		/// <summary>
		/// TODO：辅助方法：重置syncMode的相关属性，ChangeFrameMode、ClearAllData()、更改灯具列表后等？应该进行处理。
		/// </summary>
		public override void ResetSyncMode()
		{
			syncButton.Text = "进入同步";
			isSyncMode = false;
		}

		/// <summary>
		///  事件：点击《上一步》
		///  先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backStepButton_Click(object sender, EventArgs e)
		{
			int currentStep = getCurrentStep();
			chooseStep(currentStep > 1 ? currentStep - 1 : getTotalStep());
		}

		/// <summary>
		///  事件：点击《下一步》
		///  先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nextStepButton_Click(object sender, EventArgs e)
		{
			int currentStep = getCurrentStep();
			int totalStep = getTotalStep();
			chooseStep(currentStep < totalStep ? currentStep + 1 : 1);
		}

		/// <summary>
		/// 事件：点击《插入步》
		/// --前插和后插都调用同一个方法(触发键的Name决定)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void insertStepButton_Click(object sender, EventArgs e)
		{
			// 获取当前步与最高值，总步数			
			// 若当前步 <= 总步数，则可以插入，并将之后的步数往后移动
			// 否则报错
			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			if (lsWrapper.CurrentStep > lsWrapper.TotalStep)
			{
				MessageBox.Show("Dickov:当前步大于总步数");
				return;
			}

			bool insertBefore = ((Button)sender).Name.Equals("insertBeforeButton"); // 插入的方式：前插(true）还是后插（false)			
			int currentStep = lsWrapper.CurrentStep;    // 当前步
			int stepIndex = currentStep - 1;  //插入的位置：InsertStep方法中有针对前后插的判断，无需处理

			StepWrapper newStep;
			if (insertBefore)
			{
				newStep = StepWrapper.GenerateNewStep(stepIndex == 0 ? getCurrentStepTemplate() : getSelectedLightSelectedStepWrapper(selectedIndex, stepIndex - 1), mode);
			}
			else
			{
				newStep = StepWrapper.GenerateNewStep(currentStep == 0 ? getCurrentStepTemplate() : getCurrentStepWrapper(), mode);
			}
			lsWrapper.InsertStep(stepIndex, newStep, insertBefore);

			if (isSyncMode)
			{
				for (int lightIndex = 0; lightIndex < lightAstList.Count; lightIndex++)
				{
					if (lightIndex != selectedIndex)
					{
						if (insertBefore)
						{
							newStep = StepWrapper.GenerateNewStep(stepIndex == 0 ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightSelectedStepWrapper(lightIndex, stepIndex - 1), mode);
						}
						else
						{
							newStep = StepWrapper.GenerateNewStep(currentStep == 0 ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightCurrentStepWrapper(lightIndex), mode);
						}
						getSelectedLightStepWrapper(lightIndex).InsertStep(stepIndex, newStep, insertBefore);
					}
				}
			}
			else if (isMultiMode)
			{
				foreach (int lightIndex in selectedIndices)
				{
					if (lightIndex != selectedIndex)
					{
						if (insertBefore)
						{
							newStep = StepWrapper.GenerateNewStep(stepIndex == 0 ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightSelectedStepWrapper(lightIndex, stepIndex - 1), mode);
						}
						else
						{
							newStep = StepWrapper.GenerateNewStep(currentStep == 0 ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightCurrentStepWrapper(lightIndex), mode);
						}
						getSelectedLightStepWrapper(lightIndex).InsertStep(stepIndex, newStep, insertBefore);
					}
				}
			}

			RefreshStep();
		}

		/// <summary>
		/// 事件：点击《追加步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addStepButton_Click(object sender, EventArgs e)
		{
			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();

			//1.若当前灯具在本F/M下总步数为0 ，则使用stepTemplate数据，
			//2.否则使用本灯当前最大步的数据			 
			bool addTemplate = getTotalStep() == 0;
			StepWrapper newStep = StepWrapper.GenerateNewStep(addTemplate ? getCurrentStepTemplate() : getCurrentLightLastStepWrapper(), mode);
			lsWrapper.AddStep(newStep);
			if (isSyncMode)
			{
				for (int lightIndex = 0; lightIndex < lightAstList.Count; lightIndex++)
				{
					if (lightIndex != selectedIndex) //多一层保险...
					{
						newStep = StepWrapper.GenerateNewStep(addTemplate ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightLastStepWrapper(lightIndex), mode);
						getSelectedLightStepWrapper(lightIndex).AddStep(newStep);
					}
				}
			}
			else if (isMultiMode)
			{
				foreach (int lightIndex in selectedIndices)
				{
					if (lightIndex != selectedIndex)
					{
						newStep = StepWrapper.GenerateNewStep(addTemplate ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightLastStepWrapper(lightIndex), mode);
						getSelectedLightStepWrapper(lightIndex).AddStep(newStep);
					}
				}
			}
			RefreshStep();
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
			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			int stepIndex = getCurrentStep() - 1;

			// 调用包装类内部的方法:删除某一步
			try
			{
				lsWrapper.DeleteStep(stepIndex);
				if (isSyncMode)
				{
					for (int lightIndex = 0; lightIndex < lightAstList.Count; lightIndex++)
					{
						if (lightIndex != selectedIndex)
						{
							getSelectedLightStepWrapper(lightIndex).DeleteStep(stepIndex);
						}
					}
				}
				else if (isMultiMode)
				{
					foreach (int lightIndex in selectedIndices)
					{
						if (lightIndex != selectedIndex)
						{
							getSelectedLightStepWrapper(lightIndex).DeleteStep(stepIndex);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}

			RefreshStep();
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


			//3.如果是多灯模式，则需要在复制步之后处理下每个灯具的信息
			if (isMultiMode)
			{
				copyStepToAll(getCurrentStep(), WHERE.ALL);
			}

			//4.刷新当前步
			RefreshStep();

		}

		//MARK：chooseStep(int)子类的实现。
		/// <summary>
		/// 辅助方法：抽象了【选择某一个指定步数后，统一的操作；NextStep和BackStep等应该都使用这个方法】
		/// --所有更换通道数据的操作后，都应该使用这个方法。
		/// </summary>
		protected override void chooseStep(int stepNum)
		{
			if (stepNum == 0)
			{
				showTDPanels(null, 0);
				showStepLabel(0, 0);
				return;
			}

			LightStepWrapper lightStepWrapper = getCurrentLightStepWrapper();
			StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[stepNum - 1];
			lightStepWrapper.CurrentStep = stepNum;

			//TODO：chooseStep()使用isReadDelay属性后的代码，暂时隐藏。
			//if (isReadDelay) {
			//	MakeCurrentStepWrapperData(stepNum); 
			//}		

			if (isMultiMode)
			{
				foreach (int lightIndex in selectedIndices)
				{
					getSelectedLightStepWrapper(lightIndex).CurrentStep = stepNum;
				}
			}
			//11.27 若是同步状态，则选择步时，将所有灯都设为一致的步数
			if (isSyncMode)
			{
				for (int lightIndex = 0; lightIndex < lightAstList.Count; lightIndex++)
				{
					getSelectedLightStepWrapper(lightIndex).CurrentStep = stepNum;
				}
			}

			showTDPanels(stepWrapper.TongdaoList, stepWrapper.StartNum);
			showStepLabel(lightStepWrapper.CurrentStep, lightStepWrapper.TotalStep);

			if (isConnected && isRealtime)
			{
				oneStepWork();
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
			   
		/// <summary>
		/// 辅助方法：进入《单灯模式》
		/// </summary>
		/// <param name="v"></param>
		private void enableSingleMode(bool v)
		{

		}

		#endregion

		#region 灯控调试按钮组点击事件及辅助方法

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
				connectButton.Enabled = true;
			}
			else
			{
				connectButton.Enabled = false;
				MessageBox.Show("未选中可用串口");
			}
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
		/// 事件：点击《连接设备|断开连接》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectButton_Click(object sender, EventArgs e)
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
					if (String.IsNullOrEmpty(comName) || deviceComboBox.SelectedIndex < 0)
					{
						MessageBox.Show("未选中可用网络连接。");
						return;
					}

					IPAst ipAst = ipaList[deviceComboBox.SelectedIndex];
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

				//previewButton.Image = global::LightController.Properties.Resources.浏览效果前;
				EnableConnectedButtons(false);

				//MARK：11.23 延迟的骗术，在每次断开连接后立即重新搜索网络设备并建立socket连接。
				if (!isConnectCom)
				{
					Thread.Sleep(500);
					refreshNetworkList();
				}
			}
		}

		/// <summary>
		///  辅助方法：选择串口按钮、刷新串口按钮、调试的按钮组是否显示
		/// </summary>
		/// <param name="v"></param>
		public override void EnableConnectedButtons(bool connected)
		{
			// 左上角的《串口列表》《刷新串口列表》可用与否，与下面《各调试按钮》是否可用刚刚互斥
			changeConnectMethodButton.Enabled = !connected;
			deviceComboBox.Enabled = !connected;
			refreshDeviceButton.Enabled = !connected;

			realtimeButton.Enabled = connected;
			keepButton.Enabled = connected;
			makeSoundButton.Enabled = connected;
			previewButton.Enabled = connected;
			endviewButton.Enabled = connected;

			// 是否连接
			isConnected = connected;
			connectButton.Text = isConnected ? "断开连接" : "连接设备";
		}

		/// <summary>
		/// 事件：点击《实时调试》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void realtimeButton_Click(object sender, EventArgs e)
		{
			// 默认情况下，实时调试还没打开，点击后设为打开状态（文字显示为关闭实时调试，图片加颜色）
			if (!isRealtime)
			{				
				realtimeButton.Text = "关闭实时";
				isRealtime = true;
			}
			else //否则( 按钮显示为“断开连接”）断开连接
			{
				realtimeButton.Text = "实时调试";
				isRealtime = false;
			}
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
		/// 辅助方法：结束预览
		/// </summary>
		private void endview()
		{
			// 1.几个按钮图标设置
			//makeSoundButton.Image = global::LightController.Properties.Resources.触发音频;
			//previewButton.Image = global::LightController.Properties.Resources.浏览效果前;

			// 2.调用结束预览方法
			playTools.EndView();
		}

		#endregion

		
	}
}
