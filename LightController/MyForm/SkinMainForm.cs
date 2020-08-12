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
using LightEditor.Ast;
using LightController.PeripheralDevice;
using LightController.MyForm.Multiplex;

namespace LightController.MyForm
{
	public partial class SkinMainForm : MainFormBase
	{	
		private Panel[] tdPanels = new Panel[32];
		private SkinLabel[] tdNoLabels = new SkinLabel[32];
		private Label[] tdNameLabels = new Label[32];
		private SkinTrackBar[] tdSkinTrackBars = new SkinTrackBar[32];
		private NumericUpDown[] tdValueNumericUpDowns = new NumericUpDown[32];
		private SkinComboBox[] tdChangeModeSkinComboBoxes = new SkinComboBox[32];
		private NumericUpDown[] tdStepTimeNumericUpDowns = new NumericUpDown[32];	

		private Panel[] saPanels = new Panel[32];

		public SkinMainForm()
		{
			initGeneralControls();
			InitializeComponent();

			Text = SoftwareName;
			hardwareUpdateSkinButton.Visible = IsShowHardwareUpdate;
			oldToolsSkinButton.Visible = IsLinkOldTools;
			testGroupBox.Visible = IsShowTestButton;
			bigTestButton.Visible = IsShowTestButton;

			//MARK：添加这一句，会去掉其他线程使用本UI控件时弹出异常的问题(权宜之计，并非长久方案)。
			CheckForIllegalCrossThreadCalls = false;			

			#region 初始化各通道控件
			
			for (int tdIndex = 0; tdIndex<32;tdIndex++) {

				tdPanels[tdIndex] = new Panel
				{
					Size = new Size(80, 275),
					Visible = false,
					Name = "tdPanel"+ (tdIndex + 1)
				};

				tdNoLabels[tdIndex] = new SkinLabel
				{
					AutoSize = true,
					BackColor = System.Drawing.Color.Transparent,
					BorderColor = System.Drawing.Color.White,
					Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
					Location = new System.Drawing.Point(14, 11),					
					Size = new System.Drawing.Size(53, 17),
					Name = "tdNoLabel" + (tdIndex + 1)
				};

				tdNameLabels[tdIndex] = new Label
				{
					Font = new System.Drawing.Font("宋体", 8F),
					Location = new System.Drawing.Point(16, 42),
					Size = new System.Drawing.Size(14,143),					
					Name = "tdNameLabel" + (tdIndex + 1),
					TextAlign = System.Drawing.ContentAlignment.TopCenter
				};					

				tdSkinTrackBars[tdIndex] = new SkinTrackBar
				{
					AutoSize = false,
					BackColor = System.Drawing.Color.Transparent,
					Bar = null,
					BarStyle = HSLTrackBarStyle.Opacity,
					BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(112)))), ((int)(((byte)(122))))),
					Location = new System.Drawing.Point(19, 31),
					Maximum = 255,					
					Orientation = System.Windows.Forms.Orientation.Vertical,
					Size = new System.Drawing.Size(44, 170),					
					TickStyle = System.Windows.Forms.TickStyle.Both,
					Track = null,
					Name = "tdTrackBar" + (tdIndex + 1)
				};

				tdValueNumericUpDowns[tdIndex] = new NumericUpDown
				{
					Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
					TextAlign = HorizontalAlignment.Center,
					Size = new System.Drawing.Size(56, 21),
					Location = new System.Drawing.Point(15, 206),
					Maximum = new decimal(255),
					Name ="tdValueNUD" + (tdIndex + 1)
				};

				tdChangeModeSkinComboBoxes[tdIndex] = new SkinComboBox
				{
					ArrowColor = System.Drawing.Color.White,
					BaseColor = System.Drawing.Color.Gray,
					BorderColor = System.Drawing.Color.Gray,
					DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed,
					FormattingEnabled = true,
					ItemBorderColor = System.Drawing.Color.LightSlateGray,					
					Location = new System.Drawing.Point(15, 229),
					Size = new System.Drawing.Size(56, 22),
					Name = "tdChangeModeSkinComboBox" + (tdIndex + 1)
				};
				tdChangeModeSkinComboBoxes[tdIndex].Items.AddRange(new object[] {"跳变","渐变","屏蔽"});

				tdStepTimeNumericUpDowns[tdIndex] = new NumericUpDown
				{
					Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
					TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
					Size = new System.Drawing.Size(56, 21),
					Location = new System.Drawing.Point(15, 253),
					DecimalPlaces = 2,
					Maximum = new decimal(new int[] {255,0,	0,0}),
					Name = "tdStNUD" + (tdIndex + 1)
				};

				tdPanels[tdIndex].Controls.Add(tdNameLabels[tdIndex]); //插入的先后顺序很重要；如果先插入，会造成label文字被遮挡
				tdPanels[tdIndex].Controls.Add(tdNoLabels[tdIndex]);				
				tdPanels[tdIndex].Controls.Add(tdSkinTrackBars[tdIndex]);			
				tdPanels[tdIndex].Controls.Add(tdValueNumericUpDowns[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdChangeModeSkinComboBoxes[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdStepTimeNumericUpDowns[tdIndex]);				

				tdSkinFlowLayoutPanel.Controls.Add(tdPanels[tdIndex]);

				#region 各监听器

				tdNoLabels[tdIndex].Click += new EventHandler(this.tdNameNumLabels_Click);
				tdNameLabels[tdIndex].Click += new EventHandler(this.tdNameNumLabels_Click);

				tdSkinTrackBars[tdIndex].MouseEnter += new EventHandler(tdTrackBars_MouseEnter);
				tdSkinTrackBars[tdIndex].MouseWheel += new MouseEventHandler(this.tdSkinTrackBars_MouseWheel);
				tdSkinTrackBars[tdIndex].ValueChanged += new System.EventHandler(this.tdSkinTrackBars_ValueChanged);

				tdValueNumericUpDowns[tdIndex].MouseEnter += new EventHandler(this.tdValueNumericUpDowns_MouseEnter);
				tdValueNumericUpDowns[tdIndex].MouseWheel += new MouseEventHandler(this.tdValueNumericUpDowns_MouseWheel);
				tdValueNumericUpDowns[tdIndex].ValueChanged += new System.EventHandler(this.tdValueNumericUpDowns_ValueChanged);

				tdChangeModeSkinComboBoxes[tdIndex].SelectedIndexChanged += new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);

				tdStepTimeNumericUpDowns[tdIndex].MouseEnter += new EventHandler(this.tdStepTimeNumericUpDowns_MouseEnter);
				tdStepTimeNumericUpDowns[tdIndex].MouseWheel += new MouseEventHandler(this.tdStepTimeNumericUpDowns_MouseWheel);
				tdStepTimeNumericUpDowns[tdIndex].ValueChanged += new EventHandler(this.tdStepTimeNumericUpDowns_ValueChanged);

				#endregion

				saPanels[tdIndex] = new Panel
				{
					Location = new System.Drawing.Point(3, 3),
					Name = "saPanel" + (tdIndex + 1),
					Size = new System.Drawing.Size(95, 275),
					Visible = false,
				};
				tdSkinFlowLayoutPanel.Controls.Add(saPanels[tdIndex]);
			}

			#endregion

			#region 几个下拉框的初始化及赋值

			//添加FramList文本中的场景列表
			AllFrameList = TextHelper.Read(Application.StartupPath + @"\FrameList.txt");
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
						
			#endregion

			// 几个按钮添加提示
			myToolTip.SetToolTip(useFrameSkinButton, useFrameNotice);
			myToolTip.SetToolTip(chooseStepSkinButton, chooseStepNotice);
			myToolTip.SetToolTip(keepSkinButton, keepNotice);
			myToolTip.SetToolTip(insertSkinButton, insertNotice);
			myToolTip.SetToolTip(backStepSkinButton, backStepNotice);
			myToolTip.SetToolTip(nextStepSkinButton, nextStepNotice);

			isInit = true;
		}
		
		private void SkinMainForm_Load(object sender, EventArgs e)
		{
			// 启动时刷新可用串口列表;
			deviceRefresh(); //SkinMainForm_Load
			SetNotice("");
			
			// 额外处理 lightsSkinListView 会被VS吞掉的问题
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

		/// <summary>
		/// 事件：关闭Form前的操作，在此事件内可取消关闭窗体
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SkinMainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			formClosing(e);
		}

		// MARK：SkinMainForm各种工具按钮
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
			if (IsConnected)
			{
				connectSkinButton_Click(null, null);
			}
			new OldToolsForm(this).ShowDialog();
		}

		/// <summary>
		///  事件：点击《退出程序》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exitSkinButton_Click(object sender, EventArgs e)
		{
			exitClick();
		}

		/// <summary>
		/// 事件：点击《使用说明》
		/// </summary>
		/// <param name="noticeText"></param>
		private void helpSkinButton_Click(object sender, EventArgs e)
		{
			helpButtonClick();
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
		///  事件：点击《保存工程》(空方法：便于查找MouseDown方法）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveSkinButton_Click(object sender, EventArgs e)
		{
			
		}

		/// <summary>
		///  事件：点击《保存工程》（区分左右键）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveSkinButton_MouseDown(object sender, MouseEventArgs e)
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
		/// 事件：《导出工程》鼠标下压事件（判断是左键还是右键）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exportSkinButton_MouseDown(object sender, MouseEventArgs e)
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
		/// 事件：点击《导出工程》（空方法：主要作用是方便查找鼠标下压方法）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exportSkinButton_Click(object sender, EventArgs e)
		{

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
		protected override void enableProjectRelative(bool enable)
		{
			//常规的四个按钮
			saveSkinButton.Enabled = enable;
			exportSkinButton.Enabled = enable && lightAstList != null && lightAstList.Count > 0;
			frameSaveSkinButton.Enabled = enable;
			closeSkinButton.Enabled = enable;

			// 不同MainForm在不同位置的按钮
			useFrameSkinButton.Enabled = enable && lightAstList != null && lightAstList.Count > 0;

			// 菜单栏相关按钮
			lightListSkinButton.Enabled = enable;
			globalSetSkinButton.Enabled = enable;
			ymSkinButton.Enabled = enable;
			projectUpdateSkinButton.Enabled = enable;
		}

		/// <summary>
		/// 辅助方法：ClearAllDate()最后一步，但需针对不同的MainForm子类来实现。
		/// MARK：ClearAllData() in SkinMainForm
		/// </summary>
		protected override void clearAllData()
		{			
			base.clearAllData();

			lightsSkinListView.Clear();			
			stepSkinPanel.Enabled = false;
			editLightInfo(null);			
		}

		/// <summary>
		/// MARK 重构BuildLightList：reBuildLightListView() in SkinMainForm
		///辅助方法：根据现有的lightAstList，重新渲染listView
		/// </summary>
		protected override void reBuildLightListView()
		{			
			lightsSkinListView.Items.Clear();
			for (int i = 0; i < lightAstList.Count; i++)
			{
				// 添加灯具数据到LightsListView中
				lightsSkinListView.Items.Add(new ListViewItem(
						lightAstList[i].LightType +"\n(" 
						+ lightAstList[i].LightAddr +")"
						+ lightAstList[i].Remark
						,
					lightLargeImageList.Images.ContainsKey(lightAstList[i].LightPic) ? lightAstList[i].LightPic : "灯光图.png"
				)
				{ Tag = lightAstList[i].LightName + ":" + lightAstList[i].LightType }
				);
			}
			Refresh(); 
		}
		
		/// <summary>
		///  辅助方法：设定是否显示《 （调试区域的N个按钮）panel》
		/// </summary>
		/// <param name="visible"></param>
		protected override void showPlayPanel(bool visible)
		{
			playFlowLayoutPanel.Visible = visible;
		}

		/// <summary>
		/// 辅助方法：初始化（StepTime）各控件的属性值
		/// </summary>
		protected override void initStNumericUpDowns()
		{			
			for (int i = 0; i < 32; i++)
			{
				tdStepTimeNumericUpDowns[i].Maximum = EachStepTime2 * MAX_StTimes;
				tdStepTimeNumericUpDowns[i].Increment = EachStepTime2;
			}
		}

		//MARK 只开单场景：02.0.2 (SkinMainForm)改变当前Frame
		protected override void changeCurrentFrame(int frameIndex)
		{
			currentFrame = frameIndex;
			frameSkinComboBox.SelectedIndexChanged -= new System.EventHandler(this.frameSkinComboBox_SelectedIndexChanged);
			frameSkinComboBox.SelectedIndex = currentFrame;
			frameSkinComboBox.SelectedIndexChanged += new System.EventHandler(this.frameSkinComboBox_SelectedIndexChanged);
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
			//MARK 0701 通道子属性 1.3：SkinMainForm.lightsSkinListView_SelectedIndexChanged()			
			if (lightsSkinListView.SelectedIndices.Count > 0)
			{
				selectedIndex = lightsSkinListView.SelectedIndices[0];
				//disposeSauForm();
				if (generateNow)
				{					
					generateLightData();					
				}
			}
		}
		
		/// <summary>
		///  辅助方法：通过LightAst，显示选中灯具信息
		/// </summary>
		protected override void editLightInfo(LightAst la)
		{
			if (la == null) {
				currentLightPictureBox.Image = null;
				lightNameLabel.Text = null;
				lightTypeLabel.Text = null;
				lightsAddrLabel.Text = null;
				lightRemarkLabel.Text = null;
				return;
			}
			
			currentLightPictureBox.Image =lightLargeImageList.Images[la.LightPic]!=null ? lightLargeImageList.Images[la.LightPic] : global::LightController.Properties.Resources.灯光图;
			lightNameLabel.Text = "灯具厂商：" + la.LightName;
			lightTypeLabel.Text = "灯具型号：" + la.LightType;
			lightsAddrLabel.Text = "灯具地址：" + la.LightAddr;
			lightRemarkLabel.Text = "灯具备注：" + la.Remark;
		
		}

		/// <summary>
		/// 辅助方法：
		/// 1.根据传进的lightIndex，生成相应的SaPanel-->若已存在，则不再生成；
		/// 2.显示相应的Panel，隐藏无关的Panel（至于Panel的Enable属性，则由ShowStepLabel方法来进行判断）
		/// </summary>
		/// <param name="lightIndex"></param>
		protected override void generateSaPanels()
		{
			// 无步数时，直接跳过生成或显示saPanels的步骤
			if (getCurrentStep() == 0)
			{
				return;
			}

			//0. 每个灯具存储一个自身的saPanelDict,记录每个通道拥有的子属性（有的才加入到字典中）
			//1. 若还未生成saPanelDict，则在选择灯具后进行生成；
			//2. 不论是新生成还是已经存在的数据，按是否存在进行显示；

			LightAst la = lightAstList[selectedIndex];
			// 若已经存在saPanelDict，则不再重复生成了
			if (la.saPanelDict == null)
			{
				la.saPanelDict = new Dictionary<int, FlowLayoutPanel>();
				for (int tdIndex = 0; tdIndex < la.SawList.Count; tdIndex++)
				{
					// 只有尚未生成Panel 且 子属性的数量大于0时 才需要生成子属性FlowLayoutPanel
					if (la.SawList[tdIndex].SaList.Count > 0)
					{
						la.saPanelDict.Add(tdIndex, new FlowLayoutPanel()
						{
							Size = new Size(95, 297),
							Dock = DockStyle.Fill,
							AutoScroll = true,
							//BorderStyle = BorderStyle.Fixed3D
						});

						la.saPanelDict[tdIndex].Controls.Add(new Label
						{
							Size = new Size(70, 36),
							TextAlign = ContentAlignment.MiddleCenter,
							Text = "<-"
						});
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
							saToolTip.SetToolTip(saButton, sa.SAName + "\n" + sa.StartValue + " - " + sa.EndValue);
							la.saPanelDict[tdIndex].Controls.Add(saButton);
						}
						Console.WriteLine("灯具【" + selectedIndex + "】生成了一个saPanel，tdIndex = " + tdIndex + " ,其子属性数量 = " + la.SawList[tdIndex].SaList.Count);
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
			stepSkinPanel.Enabled = enable;
		}

		/// <summary>
		/// 辅助方法: 确认选中灯具是否否同一种灯具：是则返回true,否则返回false。
		/// 验证方法：取出第一个选中灯具的名字，若后面的灯具的全名（Tag =lightName + ":" + lightType)与它不同，说明不是同种灯具。（只要一个不同即可判断）
		/// </summary>
		/// <returns></returns>
		private bool checkSameLights()
		{
			if (lightsSkinListView.SelectedItems.Count == 0) {
				return false;
			}
			if (lightsSkinListView.SelectedItems.Count == 1) {
				return true;
			}

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
				labelPanel.Hide();
			}
			//2.将dataWrappers的内容渲染到起VScrollBar中
			else
			{				
				labelPanel.Show();
				for (int i = 0; i < tongdaoList.Count; i++)
				{
					tdSkinTrackBars[i].ValueChanged -= new System.EventHandler(tdSkinTrackBars_ValueChanged);
					tdValueNumericUpDowns[i].ValueChanged -= new System.EventHandler(tdValueNumericUpDowns_ValueChanged);
					tdChangeModeSkinComboBoxes[i].SelectedIndexChanged -= new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);
					tdStepTimeNumericUpDowns[i].ValueChanged -= new EventHandler(tdStepTimeNumericUpDowns_ValueChanged);

					tdNoLabels[i].Text = "通道" + (startNum + i);
					tdNameLabels[i].Text = tongdaoList[i].TongdaoName;
					myToolTip.SetToolTip(tdNameLabels[i], tongdaoList[i].Remark);
					tdSkinTrackBars[i].Value = tongdaoList[i].ScrollValue;
					tdValueNumericUpDowns[i].Text = tongdaoList[i].ScrollValue.ToString();
					tdChangeModeSkinComboBoxes[i].SelectedIndex = tongdaoList[i].ChangeMode;

					//MARK 步时间改动 SkinMainForm：主动 乘以时间因子 后 再展示
					tdStepTimeNumericUpDowns[i].Text = (tongdaoList[i].StepTime * EachStepTime2).ToString();

					tdSkinTrackBars[i].ValueChanged += new System.EventHandler(tdSkinTrackBars_ValueChanged);
					tdValueNumericUpDowns[i].ValueChanged += new System.EventHandler(tdValueNumericUpDowns_ValueChanged);
					tdChangeModeSkinComboBoxes[i].SelectedIndexChanged += new System.EventHandler(tdChangeModeSkinComboBoxes_SelectedIndexChanged);
					tdStepTimeNumericUpDowns[i].ValueChanged += new EventHandler(tdStepTimeNumericUpDowns_ValueChanged);

					tdPanels[i].Show();
				}
				for (int i = tongdaoList.Count; i < 32; i++)
				{
					tdPanels[i].Hide();
				}

				if (from0on)
				{
					generateSaPanels();
				}
			}
		}

		/// <summary>
		/// 辅助方法：隐藏所有tdPanels,因为所有panels为空了，则《统一调整框》enabled应设为false
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
		/// 辅助方法：通过选中的灯具，生成相应的saButtons
		/// </summary>
		private void generateSAButtons()
		{		
			//saFlowLayoutPanel.Controls.Clear();
			//saToolTip.RemoveAll();

			//if (selectedIndex < 0 || lightAstList == null || lightAstList.Count == 0)
			//{
			//	MessageBox.Show("generateSAButtons()出错:\n[selectedIndex < 0 || lightAstList == null || lightAstList.Count == 0]。");
			//	return;
			//}

			//LightAst la = lightAstList[selectedIndex];
			//try
			//{
			//	for (int tdIndex = 0; tdIndex < la.SawList.Count; tdIndex++)
			//	{
			//		addTdSaButtons(la, tdIndex);
			//	}
			//}
			//catch (Exception ex) {
			//	MessageBox.Show("添加子属性按键出现异常:\n" +ex.Message );				
			//}		

			//// 若当前步为0，则说明该灯具没有步数，则子属性仅显示，但不可用
			//saFlowLayoutPanel.Enabled = getCurrentStep() != 0;	
		}
				
		/// <summary>
		///  事件：双击《灯具列表的灯具》，修改备注
		/// </summary>
		private void lightsSkinListView_DoubleClick(object sender, EventArgs e)
		{
			int lightIndex = lightsSkinListView.SelectedIndices[0];
			lightsListViewDoubleClick(lightIndex);
		}

		/// <summary>
		/// MARK 修改备注：EditLightRemark()子类实现（SkinMainForm）
		/// 辅助方法：添加或修改备注
		/// </summary>
		/// <param name="lightIndex"></param>
		/// <param name="remark"></param>
		public override void EditLightRemark(int lightIndex, string remark)
		{
			base.EditLightRemark(lightIndex, remark);
			// 界面的Items[lightIndex]也要改动相应的值；			
			lightsSkinListView.Items[lightIndex].SubItems[0].Text =
				lightAstList[lightIndex].LightType + "\n("
				+ lightAstList[lightIndex].LightAddr + ")"
				+ lightAstList[lightIndex].Remark;
			lightsSkinListView.Refresh();
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
				string picStr = IniFileHelper_UTF8.ReadString(lightPath, "set", "pic", "灯光图.png");
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
			isAutoArrange = autoArrangeToolStripMenuItem.Checked;
			lightsSkinListView.AllowDrop = !isAutoArrange;
			lightsSkinListView.AutoArrange = isAutoArrange;
			autoEnableSLArrange();			
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
			IniFileHelper iniFileAst = new IniFileHelper(arrangeIniPath);
			iniFileAst.WriteInt("Common", "Count", lightsSkinListView.Items.Count);
			for (int i = 0; i < lightsSkinListView.Items.Count; i++)
			{
				iniFileAst.WriteInt("Position", i + "X", lightsSkinListView.Items[i].Position.X);
				iniFileAst.WriteInt("Position", i + "Y", lightsSkinListView.Items[i].Position.Y);
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
			if (lightCount != lightsSkinListView.Items.Count)
			{
				MessageBox.Show("灯具位置文件的灯具数量与当前工程的灯具数量不匹配，无法读取位置。");
				return;
			}

			// 4.开始读取并绘制		
			// 在选择自动排列再去掉自动排列后，必须要先设一个不同的position，才能让读取到的position真正给到items[i].Position?
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
		protected override void autoEnableSLArrange()
		{
			saveArrangeToolStripMenuItem.Enabled = ! isAutoArrange;
			loadArrangeToolStripMenuItem.Enabled = File.Exists(arrangeIniPath);
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

			//若选中项与当前项相同，则不再往下执行
			int tempFrame = frameSkinComboBox.SelectedIndex;
			if (tempFrame == currentFrame) {
				return;
			}

			setBusy(true);
			SetNotice("正在切换场景,请稍候...");			

			// 只要更改了场景，直接结束预览
			endview();

			DialogResult dr = MessageBox.Show("切换场景前，是否保存之前场景(" + AllFrameList[currentFrame] + ")？",
				"保存场景?",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			if (dr == DialogResult.Yes)
			{
				saveFrameClick();
				//MARK 只开单场景：06.0.2 切换场景时，若选择保存之前场景，则frameSaveArray设为false，意味着以后不需要再保存了。
				frameSaveArray[currentFrame] = false;				
			}

			currentFrame = frameSkinComboBox.SelectedIndex;
			//MARK 只开单场景：06.1.2 更改场景时，只有frameLoadArray为false，才需要从DB中加载相关数据（调用generateFrameData）；若为true，则说明已经加载因而无需重复读取。
			if (!frameLoadArray[currentFrame])
			{
				generateFrameData(currentFrame);
			}
			//MARK 只开单场景：06.2.2 更改场景后，需要将frameSaveArray设为true，表示当前场景需要保存
			frameSaveArray[currentFrame] = true;

			changeFrameMode();
			setBusy(false);
			SetNotice("成功切换为场景(" + AllFrameList[currentFrame] + ")");
		}

		/// <summary>
		/// 事件：更改《选择模式》选项后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void modeSkinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			currentMode = modeSkinComboBox.SelectedIndex;

			//11.13 若未初始化，直接return；
			if (!isInit)
			{
				return;
			}
			
			// 若模式为声控模式mode=1
			// 1.改变几个label的Text; 
			// 2.改变跳变渐变-->是否声控；
			// 3.所有步时间值的调节，改为enabled=false						
			if (currentMode == 1)
			{
				for (int i = 0; i < FrameCount; i++)
				{
					this.tdChangeModeSkinComboBoxes[i].Items.Clear();
					this.tdChangeModeSkinComboBoxes[i].Items.AddRange(new object[] { "屏蔽", "跳变" });
					this.tdStepTimeNumericUpDowns[i].Hide();
				}			
			}
			else //mode=0，常规模式
			{
				for (int i = 0; i < FrameCount; i++)
				{
					this.tdChangeModeSkinComboBoxes[i].Items.Clear();
					this.tdChangeModeSkinComboBoxes[i].Items.AddRange(new object[] { "跳变", "渐变", "屏蔽" });
					this.tdStepTimeNumericUpDowns[i].Show();
				}
            }

			changeFrameMode();
			SetNotice("成功切换模式");
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
				enterMultiMode();
			}
			// 退出多灯模式
			else
			{
				exitMultiMode();
			}
		}

		/// <summary>
		/// 辅助方法：进入多灯模式的子类实现
		/// </summary>
		protected override void enterMultiMode() {

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

		/// <summary>
		/// 辅助方法：退出多灯模式的子类实现
		/// </summary>
		protected override void exitMultiMode() {

			foreach (ListViewItem item in lightsSkinListView.Items)
			{
				item.BackColor = Color.White;
			}
			RefreshMultiModeButtons(false);

			try
			{
				for (int i = 0; i < lightsSkinListView.Items.Count; i++)
				{
					lightsSkinListView.Items[i].Selected = i == selectedIndex;
				}
				lightsSkinListView.Select();
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
		private void backStepSkinButton_Click(object sender, EventArgs e) { }

		/// <summary>
		/// 事件：鼠标（左|右键）按下《上一步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backStepSkinButton_MouseDown(object sender, MouseEventArgs e)
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
		private void nextStepSkinButton_Click(object sender, EventArgs e) { }

		/// <summary>
		/// 事件：鼠标（左|右键）按下《下一步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nextStepSkinButton_MouseDown(object sender, MouseEventArgs e)
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
		private void chooseStepSkinButton_Click(object sender, EventArgs e)
		{
			int step = decimal.ToInt32(chooseStepNumericUpDown.Value);
			if (step == 0)
			{
				MessageBox.Show("不可选择0步");
				return;
			}
			chooseStep(step);
		}

		/// <summary>
		/// 事件：点击《插入步》
		/// --前插和后插都调用同一个方法(触发键的Name决定)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void insertSkinButton_Click(object sender, EventArgs e) 	{	}

		/// <summary>
		/// 事件：鼠标左右键按下《插入步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void insertSkinButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				insertStepClick(false);
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (getCurrentStep() == 0)
				{
					insertStepClick(false);
					return;
				}
				insertStepClick(true);
			}
		}

		/// <summary>
		/// 事件：空方法,为方便查找addStepSkinButton_MouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addStepSkinButton_Click(object sender, EventArgs e)	{ }

		/// <summary>
		/// 事件：鼠标（左|右键）按下《追加步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addStepSkinButton_MouseDown(object sender, MouseEventArgs e)
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
		///  事件：空方法，作用为方便查找deleteStepSkinButton_MouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteStepSkinButton_Click(object sender, EventArgs e){	}

		/// <summary>
		/// 事件：鼠标（左|右键）按下《删除步》
		///  左键 1.获取当前步，当前步对应的stepIndex
		///	       2.通过stepIndex，DeleteStep(index);
		///		   3.获取新步(step删除后会自动生成新的)，并重新渲染stepLabel和vScrollBars
		///  右键 新建DeleteStepForm并显示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteStepSkinButton_MouseDown(object sender, MouseEventArgs e)
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
		private void actionButton_Click(object sender, EventArgs e)	{}

		/// <summary>
		/// 事件：左右键点击《内置动作》（右键为《RGB调光》）
		/// </summary>
		private void actionButton_MouseDown(object sender, MouseEventArgs e) {

			if (e.Button == MouseButtons.Left)
			{
				actionButtonClick();
			}
			else if (e.Button == MouseButtons.Right)
			{
				
			}
		}

		/// <summary>
		/// 事件：点击《复制步》
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
		/// 事件：点击《进入同步|退出同步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void syncSkinButton_Click(object sender, EventArgs e)
		{
			syncButtonClick();
		}

		/// <summary>
		/// 事件：点击《多步复用》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiplexSkinButton_Click(object sender, EventArgs e)
		{
			multiplexButtonClick();
		}

		/// <summary>
		///  辅助方法：实现《多灯模式》
		/// </summary>
		/// <param name="captainIndex"></param>
		public override void EnterMultiMode(int captainIndex, bool isCopyAll)
		{
			// 基类中统一的处理
			base.EnterMultiMode(captainIndex, isCopyAll);

			// 以下为单独针对本Form的方法：			
			foreach (ListViewItem item in lightsSkinListView.Items)
			{
				item.BackColor = Color.White;
			}

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
			RefreshMultiModeButtons(true);
		}

		/// <summary>
		/// 辅助方法：退出多灯模式或单灯模式后的相关操作
		/// </summary>
		/// <param name="exit"></param>
		protected override void RefreshMultiModeButtons(bool isMultiMode)
		{
			this.isMultiMode = isMultiMode;
		
			//MARK 只开单场景：15.2 《灯具列表》是否可用，由单灯模式决定
			lightListSkinButton.Enabled = !isMultiMode;
			lightsSkinListView.Enabled = !isMultiMode;
			frameSkinComboBox.Enabled = !isMultiMode;
			modeSkinComboBox.Enabled = !isMultiMode;
			useFrameSkinButton.Enabled = !isMultiMode;
			groupFlowLayoutPanel.Enabled = lightAstList != null;   // 只要当前工程有灯具，就可以进入编组（再由按钮点击事件进行进一步确认）

			multiLightSkinButton.Text = !isMultiMode ? "多灯模式" : "单灯模式";
		}

		/// <summary>
		///辅助方法：重置syncMode的相关属性，ChangeFrameMode、ClearAllData()、更改灯具列表后等？应该进行处理。
		/// </summary>
		public override void EnterSyncMode(bool isSyncMode)
		{
			this.isSyncMode = isSyncMode;
			syncSkinButton.Text = isSyncMode? "退出同步":"进入同步";
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
			stepLabel.Text = MathHelper.GetFourWidthNumStr(currentStep, true) + "/" + MathHelper.GetFourWidthNumStr(totalStep, false);

			// 2.1 设定《删除步》按钮是否可用
			deleteStepSkinButton.Enabled = totalStep != 0;

			// 2.2 设定《追加步》、《前插入步》《后插入步》按钮是否可用			
			bool insertEnabled = totalStep < MAX_STEP;
			addStepSkinButton.Enabled = insertEnabled;
			insertSkinButton.Enabled = insertEnabled ;

			// 2.3 设定《上一步》《下一步》是否可用			
			backStepSkinButton.Enabled = totalStep > 1;
			nextStepSkinButton.Enabled = totalStep > 1;

			//3 设定《复制(多)步、保存素材》、《多步复用》等是否可用
			copyStepSkinButton.Enabled = currentStep > 0;
			pasteStepSkinButton.Enabled = currentStep > 0 && tempStep != null;

			multiCopySkinButton.Enabled = currentStep > 0;
			multiPasteSkinButton.Enabled = TempMaterialAst != null && TempMaterialAst.Mode == currentMode;

			multiplexSkinButton.Enabled = currentStep > 0;

			// 4.设定统一调整区是否可用
			groupButton.Enabled = lightAstList != null && lightsSkinListView.SelectedIndices.Count > 0; //只有工程非空（有灯具列表）且选择项不为空 才可点击
			groupFlowLayoutPanel.Enabled = lightAstList != null ; 
			initButton.Enabled = totalStep != 0;
			multiButton.Enabled = totalStep != 0;
            soundListButton.Enabled = !string.IsNullOrEmpty(currentProjectName) && currentMode == 1 ;

            // 5. 处理选择步数的框及按钮
            chooseStepNumericUpDown.Enabled = totalStep != 0;			
			chooseStepNumericUpDown.Minimum = totalStep != 0 ? 1 : 0;
			chooseStepNumericUpDown.Maximum = totalStep;
			chooseStepSkinButton.Enabled = totalStep != 0;

			// 6.子属性按钮组是否可用(及可见（当步数为空时，设为不可见）:因只有切换灯具，才会生成，故无需担心会多次生成按钮组)						
			//MARK 0629 子属性Panel 2.3：NewMainForm.showStepLabel()中，显示或使能saPanelArray
			//if (selectedIndex != -1 && saPanelArray[selectedIndex] != null)
			//{
			//	saPanelArray[selectedIndex].Enabled = totalStep != 0;
			//}

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
			int tdIndex = MathHelper.GetIndexNum(((SkinTrackBar)sender).Name, -1);
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
			int tdIndex = MathHelper.GetIndexNum(((SkinTrackBar)sender).Name, -1);
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
					tdSkinTrackBars[tdIndex].Value = Decimal.ToInt32(dd);
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = tdSkinTrackBars[tdIndex].Value - tdSkinTrackBars[tdIndex].SmallChange;
				if (dd >= tdSkinTrackBars[tdIndex].Minimum)
				{
					tdSkinTrackBars[tdIndex].Value = Decimal.ToInt32(dd);
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
            int tongdaoIndex = MathHelper.GetIndexNum(((SkinTrackBar)sender).Name, -1);
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
            int tongdaoIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
            int tdValue = Decimal.ToInt32(tdValueNumericUpDowns[tongdaoIndex].Value);

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
			int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
			tdStepTimeNumericUpDowns[tdIndex].Select();
		}

		/// <summary>
		///  事件：鼠标滚动时，步时间值每次只变动一个Increment值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdStepTimeNumericUpDowns_MouseWheel(object sender, MouseEventArgs e)
		{
			int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name,-1);
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
			int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);

			//2.取出recentStep，这样就能取出一个步数，使用取出的index，给stepWrapper.TongdaoList[index]赋值
			StepWrapper step = getCurrentStepWrapper();

			//MARK 步时间改动 SkinMainForm：处理为数据库所需数值：将 (显示的步时间* 时间因子)后再放入内存
			int stepTime = Decimal.ToInt32(tdStepTimeNumericUpDowns[tdIndex].Value / EachStepTime2); // 取得的值自动向下取整（即舍去多余的小数位）
			step.TongdaoList[tdIndex].StepTime = stepTime;
			tdStepTimeNumericUpDowns[tdIndex].Value = stepTime * EachStepTime2; //若与所见到的值有所区别，则将界面控件的值设为处理过的值

			if (isMultiMode) {
				copyValueToAll(tdIndex, WHERE.STEP_TIME, stepTime);
			}
		}

		//MARK 0629 子属性Panel 2.4：点击tdNameLabels怎么实现该通道的子属性（待完成）
		/// <summary>
		/// 事件：点击《tdNameLabels》时，右侧的子属性按钮组，会显示当前通道相关的子属性，其他通道的子属性，则隐藏掉
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdNameNumLabels_Click(object sender, EventArgs e)
		{
			tdNameNumLabelClick(sender);
		}		
		
		#endregion

		//MARK：SkinMainForm统一调整框各事件处理
		#region 统一调整框的组件及事件绑定
		
		/// <summary>
		/// 事件：点击《灯具编组》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void groupSkinButton_Click(object sender, EventArgs e)
		{
			if (lightsSkinListView.SelectedIndices.Count < 1)
			{
				MessageBox.Show("请选择至少一个灯具，否则无法进行编组。");
				return;
			}

			if ( !checkSameLights())
			{
				MessageBox.Show("未选中灯具或选中的灯具并非同一类型的，无法进行编组；请再次选择后重试。");
				return;
			}

			selectedIndices = new List<int>();
			foreach (int item in lightsSkinListView.SelectedIndices)
			{
				selectedIndices.Add(item);
			}
			new GroupForm(this, lightAstList, selectedIndices).ShowDialog();
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
		private void multiSkinButton_Click(object sender, EventArgs e){  }

		/// <summary>
		/// 事件：左右键点击《多步调节》：右键是多步联调
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiSkinButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				multiButtonClick();
			}
			else if (e.Button == MouseButtons.Right)
			{
				soundMultiButtonClick();
			}
		}
		

		/// <summary>
		/// 事件：点击《音频链表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void soundListButton_Click(object sender, EventArgs e)
        {
           new SKForm(this, currentFrame, frameSkinComboBox.Text).ShowDialog();  
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
		/// 事件：点击《saButton(子属性)》按钮组的任意按键
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
			if (groupList != null && groupList.Count > 0)
			{
				for (int groupIndex = 0; groupIndex < groupList.Count; groupIndex++)
				{
					addGroupPanel(groupIndex, groupList[groupIndex]);
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
			SkinButton inButton = new SkinButton();
			SkinButton delButton = new SkinButton();

			panel.Controls.Add(inButton);
			panel.Controls.Add(delButton);
			panel.Location = new System.Drawing.Point(0, 0);
			panel.Name = "groupPanel";
			panel.Size = new System.Drawing.Size(118, 26);
			panel.TabIndex = 56;
			panel.Padding = new Padding(1);
			panel.Margin = new Padding(1);

			inButton.BackColor = System.Drawing.Color.Transparent;
			inButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			inButton.BorderColor = System.Drawing.Color.Silver;
			inButton.ForeColor = System.Drawing.Color.Black;
			inButton.Size = new Size(105, 24);
			inButton.Text = ga.GroupName;
			inButton.Tag = groupIndex;
			inButton.Click += new EventHandler(groupInButton_Click);

			delButton.BackColor = System.Drawing.Color.Transparent;
			delButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			delButton.BorderColor = System.Drawing.Color.Silver;
			delButton.ForeColor = System.Drawing.Color.Black;
			delButton.Text = ga.GroupName;
			delButton.Location = new Point(106, 0);
			delButton.Size = new Size(12, 24);
			delButton.Text = "-";
			delButton.Tag = groupIndex;
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
			foreach (ListViewItem item in lightsSkinListView.Items)
			{
				item.Selected = false;
			}
			for (int i = 0; i < selectedIndices.Count; i++)
			{
				if (i == selectedIndices.Count - 1)
				{
					generateNow = true;
				}
				int lightIndex = selectedIndices[i];
				lightsSkinListView.Items[lightIndex].Selected = true;
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
			SetNotice("正在切换连接模式,请稍候...");
			isConnectCom = !isConnectCom;
			changeConnectMethodSkinButton.Text = isConnectCom ? "以网络连接" : "以串口连接";
			deviceRefreshSkinButton.Text = isConnectCom ? "刷新串口" : "刷新网络";
			SetNotice("成功切换为" + (isConnectCom ? "串口连接" : "网络连接"));

			deviceRefresh(); //changeConnectMethodSkinButton_Click : 切换连接后，手动帮用户搜索相应的设备列表。
		}

		/// <summary>
		/// 事件：点击《刷新串口|网络》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceRefreshSkinButton_Click(object sender, EventArgs e)
		{
			deviceRefresh(); //deviceRefreshButton_Click
		}

		/// <summary>
		/// 辅助方法：刷新设备
		/// </summary>
		private void deviceRefresh() {

			//	 刷新前，先清空按键等
			SetNotice("正在" + (isConnectCom ? "刷新串口列表" : "搜索网络设备") + "，请稍候...");
			deviceSkinComboBox.Items.Clear();
			deviceSkinComboBox.Text = "";
			deviceSkinComboBox.SelectedIndex = -1;
			deviceSkinComboBox.Enabled = false;
			deviceConnectSkinButton.Enabled = false;
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
						deviceSkinComboBox.Items.Add(com);
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

				networkDeviceList = new List<NetworkDeviceInfo>();
				Dictionary<string, Dictionary<string, NetworkDeviceInfo>> allDevices = NetworkConnect.GetDeviceList();
				if (allDevices.Count > 0)
				{
					foreach (KeyValuePair<string, Dictionary<string, NetworkDeviceInfo>> device in allDevices)
					{
						foreach (KeyValuePair<string, NetworkDeviceInfo> d2 in device.Value)
						{
							string localIPLast = device.Key.ToString().Substring(device.Key.ToString().LastIndexOf("."));
							deviceSkinComboBox.Items.Add(d2.Value.DeviceName + "(" + d2.Key + ")" + localIPLast);
							networkDeviceList.Add(d2.Value);
						}
					}
				}
			}

			if (deviceSkinComboBox.Items.Count > 0)
			{
				deviceSkinComboBox.SelectedIndex = 0;
				deviceSkinComboBox.Enabled = true;
				deviceConnectSkinButton.Enabled = true;
				SetNotice("已刷新" + (isConnectCom ? "串口" : "网络") + "列表，可选择并连接设备进行调试");
			}
			else
			{
				SetNotice("未找到可用的" + (isConnectCom ? "串口" : "网络") + "设备，请确认后重试。");
			}
		}

		/// <summary>
		/// 事件：改变deviceComboBox的选中项。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceSkinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{			
			if (!deviceSkinComboBox.Text.Trim().Equals(""))
			{	
				deviceConnectSkinButton.Enabled = true;
			}
			else
			{
				deviceConnectSkinButton.Enabled = false;
				MessageBox.Show("未选中可用设备");
			}
		}
		
		/// <summary>
		///  事件：点击《连接设备|断开连接》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectSkinButton_Click(object sender, EventArgs e)
		{			
			connectButtonClick( deviceSkinComboBox.Text , deviceSkinComboBox.SelectedIndex );
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
				keepSkinButton.Image = global::LightController.Properties.Resources.单灯单步后;
				keepSkinButton.Text = "取消保持";
				isKeepOtherLights = true;
			}
			else //否则( 按钮显示为“保持其他灯状态”）断开连接
			{
				keepSkinButton.Image = global::LightController.Properties.Resources.单灯单步;
				keepSkinButton.Text = "保持状态";
				isKeepOtherLights = false;
			}
			RefreshStep();
		}

		/// <summary>
		///  事件：点击《预览效果》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewSkinButton_Click(object sender, EventArgs e)
		{
			previewButtonClick();
		}		
		
        /// <summary>
        ///  事件：点击《触发音频》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void makeSoundSkinButton_Click(object sender, EventArgs e)
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

			// 左上角的《串口列表》《刷新串口列表》可用与否，与下面《各调试按钮》是否可用刚刚互斥
			comPanel.Enabled = !IsConnected;				
			
			keepSkinButton.Enabled = IsConnected && !isPreviewing;			
			previewSkinButton.Enabled = IsConnected;
			makeSoundSkinButton.Enabled = IsConnected && isPreviewing;			

			if (IsConnected)
			{
				deviceConnectSkinButton.Image = global::LightController.Properties.Resources.断开连接;
				deviceConnectSkinButton.Text = "断开连接";
			}
			else
			{                
                deviceConnectSkinButton.Image = global::LightController.Properties.Resources.连接;
				deviceConnectSkinButton.Text = "连接设备";
			}
			SetPreview(isPreviewing);

			//721：进入连接但非调试模式时，刷新当前步(因为有些操作是异步的，可能造成即时的刷新步数，无法进入单灯单步)
			if (IsConnected && !isPreviewing)
			{
				RefreshStep();
			}
		}

		/// <summary>
		/// 辅助方法：调用基类的单灯单步发送DMX512帧数据;并操作本类中的相关数据
		/// </summary>
		protected override void oneStepWork()
		{
			base.oneStepWork();            
        }

		/// <summary>
		/// 辅助方法：根据入参，调整《预览效果|停止预览》按键的显示
		/// </summary>
		/// <param name="preview"></param>
		public override void SetPreview(bool preview)
		{
			previewSkinButton.Image = preview ? Properties.Resources.浏览效果后 : Properties.Resources.浏览效果前;
			previewSkinButton.Text = preview ? "停止预览" : "预览效果";
		}

		/// <summary>
		/// 辅助方法：根据入参，调整《触发音频》按键的显示
		/// </summary>
		/// <param name="preview"></param>
		protected override void setMakeSound(bool makeSound)
		{
			makeSoundSkinButton.Image = makeSound ? Properties.Resources.触发音频后 : Properties.Resources.触发音频;
			Refresh();
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
			noticeStatusStrip.Refresh();
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
			this.unifyPanel.Enabled = !busy;
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
			int buttonIndex = MathHelper.GetIndexNum(((Button)sender).Name, -1);
			Console.WriteLine(buttonIndex);
			Tools.Test test = new Tools.Test(GetDBWrapper(true), this, GlobalIniPath);
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
			
		}




		/// <summary>
		///  辅助方法:根据当前《 变动方式》选项 是否屏蔽，处理相关通道是否可设置
		///  --9.4禁用此功能，即无论是否屏蔽，
		/// </summary>
		/// <param name="tongdaoIndex">tongdaoList的Index</param>
		/// <param name="shielded">是否被屏蔽</param>
		//private void enableTongdaoEdit(int tongdaoIndex, bool shielded)
		//{
		//	tdSkinTrackBars[tongdaoIndex].Enabled = shielded;
		//	tdValueNumericUpDowns[tongdaoIndex].Enabled = shielded;
		//	tdStepTimeNumericUpDowns[tongdaoIndex].Enabled = shielded;
		//}

		/// <summary>
		/// 事件：点击《实时调试》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//private void realtimeSkinButton_Click(object sender, EventArgs e)
		//{
		//// 默认情况下，实时调试还没打开，点击后设为打开状态（文字显示为关闭实时调试，图片加颜色）
		//if (!isRealtime)
		//{
		//	realtimeSkinButton.Image = global::LightController.Properties.Resources.实时调试;
		//	realtimeSkinButton.Text = "关闭实时";
		//	isRealtime = true;
		//	if (!isConnectCom)
		//	{
		//		playTools.StartInternetPreview( myConnect, ConnectCompleted, ConnectAndDisconnectError, eachStepTime);
		//	}
		//	RefreshStep();
		//	SetNotice("已开启实时调试。");
		//}
		//else //否则( 按钮显示为“断开连接”）断开连接
		//{
		//	realtimeSkinButton.Image = global::LightController.Properties.Resources.实时调试02;
		//	realtimeSkinButton.Text = "实时调试";
		//	isRealtime = false;
		//	playTools.ResetDebugDataToEmpty();
		//	SetNotice("已退出实时调试。");
		//}
		//}

		/// <summary>
		///   事件：tdSkinFlowLayoutPanel的paint事件
		///  </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//private void tdSkinFlowLayoutPanel_Paint(object sender, PaintEventArgs e)
		//{
		//	if (!isPainting)
		//	{
		//		repaintTDPanels();
		//	}
		//}

		///// <summary>
		/////  辅助方法：重绘tdPanels
		///// </summary>
		//private void repaintTDPanels()
		//{
		//	// 1. j是tdPanels中在第一列的panel的数量，
		//	int j = 0;
		//	foreach (var tdPanel in tdPanels)
		//	{
		//		if (tdPanel.Visible && tdPanel.Location.X == 3)
		//		{
		//			j++;
		//		}
		//	}
		//	showLabelPanels(j);

		//	// 2. 设置滚动条的位置
		//	labelFlowLayoutPanel.AutoScrollPosition = new Point(0, -tdSkinFlowLayoutPanel.AutoScrollPosition.Y);
		//}

		///// <summary>
		/////  辅助方法：通过实时计算tdPanels占用的行数，来显示相同行数的labelPanels
		///// </summary>
		///// <param name="j"></param>
		//private void showLabelPanels(int j)
		//{
		//	switch (j)
		//	{
		//		case 0:
		//			labelPanel1.Hide();
		//			labelPanel2.Hide();
		//			labelPanel3.Hide();
		//			break;
		//		case 1:
		//			labelPanel1.Show();
		//			labelPanel2.Hide();
		//			labelPanel3.Hide();
		//			break;
		//		case 2:
		//			labelPanel1.Show();
		//			labelPanel2.Show();
		//			labelPanel3.Hide();
		//			break;
		//		case 3:
		//			labelPanel1.Show();
		//			labelPanel2.Show();
		//			labelPanel3.Show();
		//			break;
		//	}
		//}

		///// <summary>
		/////  事件：拦截labelFlowLayoutPanel的鼠标滚动。
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void labelFlowLayoutPanel_MouseWheel(object sender, MouseEventArgs e)
		//{
		//	Point oldPoint = labelFlowLayoutPanel.AutoScrollPosition;
		//	labelFlowLayoutPanel.AutoScrollPosition = oldPoint;
		//}

		#endregion

	
	}
	   
}
