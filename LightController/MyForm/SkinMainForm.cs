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
using LightEditor.Ast;
using LightController.PeripheralDevice;
using LightController.MyForm.Multiplex;
using Newtonsoft.Json;
using LightController.MyForm.HardwareSet;
using LightController.MyForm.MainFormAst;

namespace LightController.MyForm
{
	public partial class SkinMainForm : MainFormBase
	{	
		private Panel[] tdPanels = new Panel[32];
		private SkinLabel[] tdNoLabels = new SkinLabel[32];
		private Label[] tdNameLabels = new Label[32];
		private SkinTrackBar[] tdTrackBars = new SkinTrackBar[32];
		private NumericUpDown[] tdValueNumericUpDowns = new NumericUpDown[32];
		private SkinComboBox[] tdCmComboBoxes = new SkinComboBox[32];
		private NumericUpDown[] tdStNumericUpDowns = new NumericUpDown[32];	
		private Panel[] saPanels = new Panel[32];		

		public SkinMainForm()
		{			
			InitializeComponent();
			initGeneralControls();							

			#region 初始化各通道控件
			
			for (int tdIndex = 0; tdIndex<32;tdIndex++) {

				tdPanels[tdIndex] = new Panel
				{
					Name = "tdPanel" + (tdIndex + 1),
					Size = tdPanelDemo.Size,						
					Visible = tdPanelDemo.Visible,
					Margin = tdPanelDemo.Margin
				};

				tdNoLabels[tdIndex] = new SkinLabel
				{
					Name = "tdNoLabel" + (tdIndex + 1),
					AutoSize = tdNoLabelDemo.AutoSize,
					BackColor = tdNoLabelDemo.BackColor,
					BorderColor = tdNoLabelDemo.BorderColor,
					Font = tdNoLabelDemo.Font,
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

				//这里设置属性的先后顺序，也会使得控件可能显示不出来...
				tdTrackBars[tdIndex] = new SkinTrackBar
				{
					Name = "tdTrackBar" + (tdIndex + 1),
					AutoSize = tdTrackBarDemo.AutoSize,
					BackColor = tdTrackBarDemo.BackColor,
					Bar = tdTrackBarDemo.Bar,
					BarStyle = tdTrackBarDemo.BarStyle,
					BaseColor = tdTrackBarDemo.BaseColor,
					Location = tdTrackBarDemo.Location,
					Maximum = tdTrackBarDemo.Maximum,
					Orientation = tdTrackBarDemo.Orientation,
					Size = tdTrackBarDemo.Size,
					TickStyle = tdTrackBarDemo.TickStyle,
					Track = tdTrackBarDemo.Track,					
				};

				tdValueNumericUpDowns[tdIndex] = new NumericUpDown
				{
					Name = "tdValueNUD" + (tdIndex + 1),
					Font = tdValueNUDDemo.Font,
					TextAlign = tdValueNUDDemo.TextAlign,
					Size = tdValueNUDDemo.Size,
					Location = tdValueNUDDemo.Location,
					Maximum = tdValueNUDDemo.Maximum,
					Tag = 0,
				};

				tdCmComboBoxes[tdIndex] = new SkinComboBox
				{
					Name = "tdCmComboBox" + (tdIndex + 1),
					ArrowColor = tdChangeModeComboBoxDemo.ArrowColor,
					BaseColor = tdChangeModeComboBoxDemo.BaseColor,
					BorderColor = tdChangeModeComboBoxDemo.BorderColor,
					DrawMode = tdChangeModeComboBoxDemo.DrawMode,
					FormattingEnabled = tdChangeModeComboBoxDemo.FormattingEnabled,
					ItemBorderColor = tdChangeModeComboBoxDemo.ItemBorderColor,
					Location = tdChangeModeComboBoxDemo.Location,
					Size = tdChangeModeComboBoxDemo.Size,
					DropDownStyle = tdChangeModeComboBoxDemo.DropDownStyle,
					Tag = 1
				};
				tdCmComboBoxes[tdIndex].Items.AddRange(new object[] {
					LanguageHelper.TranslateWord("跳变"),
					LanguageHelper.TranslateWord("渐变"),
					LanguageHelper.TranslateWord("屏蔽")
				});

				tdStNumericUpDowns[tdIndex] = new NumericUpDown
				{
					Name = "tdStNUD" + (tdIndex + 1),
					Font = tdStepTimeNUDDemo.Font,
					TextAlign = tdStepTimeNUDDemo.TextAlign,
					Size = tdStepTimeNUDDemo.Size,
					Location = tdStepTimeNUDDemo.Location,
					DecimalPlaces = tdStepTimeNUDDemo.DecimalPlaces,
					Maximum = tdStepTimeNUDDemo.Maximum,
					Tag = 2
				};

				tdPanels[tdIndex].Controls.Add(tdNameLabels[tdIndex]); //插入的先后顺序很重要；顺序不对时，会造成label文字被遮挡
				tdPanels[tdIndex].Controls.Add(tdNoLabels[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdTrackBars[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdValueNumericUpDowns[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdCmComboBoxes[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdStNumericUpDowns[tdIndex]);

				tdSkinFlowLayoutPanel.Controls.Add(tdPanels[tdIndex]);

				#region 各监听器

				tdNoLabels[tdIndex].Click += tdNameNumLabels_Click;
				tdNameLabels[tdIndex].Click += tdNameNumLabels_Click;

				tdTrackBars[tdIndex].MouseEnter += tdTrackBars_MouseEnter;
				tdTrackBars[tdIndex].MouseWheel += tdSkinTrackBars_MouseWheel;
				tdTrackBars[tdIndex].ValueChanged += tdSkinTrackBars_ValueChanged;

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

				#endregion

				saPanels[tdIndex] = new Panel
				{
					Name = "saPanel" + (tdIndex + 1),
					Location = saPanelDemo.Location,
					Size = saPanelDemo.Size,					
					Margin = saPanelDemo.Margin,
					Visible = true,
				};
				tdSkinFlowLayoutPanel.Controls.Add(saPanels[tdIndex]);
			}

			#endregion

			#region 几个下拉框的初始化及赋值
			
			//模式选项框
			modeSkinComboBox.Items.AddRange(new object[] {
				LanguageHelper.TranslateWord("常规模式"),
				LanguageHelper.TranslateWord("音频模式")
			});
			modeSkinComboBox.SelectedIndex = 0;
						
			#endregion

			// 几个按钮添加提示
			myToolTip.SetToolTip(copyFrameSkinButton, copyFrameNotice);			
			myToolTip.SetToolTip(keepSkinButton, keepNotice);
			myToolTip.SetToolTip(insertSkinButton, insertNotice);
			myToolTip.SetToolTip(appendSkinButton, appendNotice);
			myToolTip.SetToolTip(deleteSkinButton, deleteNotice);
			myToolTip.SetToolTip(backStepSkinButton, backStepNotice);
			myToolTip.SetToolTip(nextStepSkinButton,nextStepNotice);

			// 添加子属性按键组是否显示的菜单
			showSaPanelsToolStripMenuItem.Text = IsShowSaPanels ? "隐藏子属性面板" : "显示子属性面板";

			//  动态加载灯具图片列表
			lightsSkinListView.LargeImageList = lightImageList ; 
			RefreshLightImageList(); //SkinMainForm构造函数

			// 刷新为设置的语言
			LanguageHelper.InitForm(this);
			LanguageHelper.TranslateMenuStrip( mySkinContextMenuStrip);

			isInit = true;
		}

		private void SkinMainForm_Load(object sender, EventArgs e)
		{
			// 额外处理 lightsSkinListView 会被VS吞掉的问题
			lightsSkinListView.HideSelection = true;

			// 用以处理大工程时，子属性列表会连在一起的bug；
			foreach (Panel panel in saPanels)
			{
				panel.Hide();
			}
		}
		
		private void SkinMainForm_Activated(object sender, EventArgs e)
		{
			startPreview();
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

		/// <summary>
		/// 辅助公用方法：渲染场景选择框
		/// </summary>
		public override void RenderSceneCB()
		{
			sceneSkinComboBox.SelectedIndexChanged -= sceneSkinComboBox_SelectedIndexChanged;
			sceneSkinComboBox.Items.Clear();
			foreach (string frame in AllSceneList)
			{
				sceneSkinComboBox.Items.Add(frame);
			}
			sceneSkinComboBox.SelectedIndex = CurrentScene;
			sceneSkinComboBox.SelectedIndexChanged += sceneSkinComboBox_SelectedIndexChanged;
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
			//MARK0412  修改《硬件设置》点击事件 ：用NewHardwareSetForm处理
			//new HardwareSetChooseForm(this).ShowDialog();

			hardwareSetButtonClick();
		}		

		/// <summary>
		///  事件：点击《设备连接》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deviceConnectButton_Click(object sender, EventArgs e)
		{
			connectButtonClick();
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
		///  事件：点击《退出程序》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exitSkinButton_Click(object sender, EventArgs e)
		{
			exitClick();
		}

		/// <summary>
		/// 事件：点击《使用说明》->导航功能
		/// </summary>
		/// <param name="noticeText"></param>
		private void helpSkinButton_Click(object sender, EventArgs e)	{	}

		/// <summary>
		/// 事件：鼠标左右键按下《使用说明》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void helpSkinButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				helpButtonClick();
			}
			else if (e.Button == MouseButtons.Right)
			{
				updateLogButtonClick();
			}
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
			saveSceneClick();
		}

		/// <summary>
		///  事件：点击《保存工程》(空方法：便于查找MouseDown方法）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveSkinButton_Click(object sender, EventArgs e)	{	}

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
		/// 事件：点击《导出工程》（空方法：主要作用是方便查找鼠标下压方法）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exportSkinButton_Click(object sender, EventArgs e){	}

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
			exportSkinButton.Enabled = enable && LightAstList != null && LightAstList.Count > 0;
			frameSaveSkinButton.Enabled = enable;
			closeSkinButton.Enabled = enable;

			// 不同MainForm在不同位置的按钮
			copyFrameSkinButton.Enabled = enable && LightAstList != null && LightAstList.Count > 0;

			// 菜单栏相关按钮
			lightListSkinButton.Enabled = enable;
			globalSetSkinButton.Enabled = enable;				
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
			for (int i = 0; i < LightAstList.Count; i++)
			{
				// 添加灯具数据到LightsListView中
				lightsSkinListView.Items.Add(new ListViewItem(
						LightAstList[i].LightType +"\n(" 
						+ LightAstList[i].LightAddr +")"
						+ LightAstList[i].Remark	,
					lightImageList.Images.ContainsKey(LightAstList[i].LightPic) ? LightAstList[i].LightPic : "灯光图.png"
				)
				{ Tag = LightAstList[i].LightName + ":" + LightAstList[i].LightType }
				);
			}
			Refresh();
		}
		
		/// <summary>
		///  辅助方法：设定是否显示《 （调试区域的N个按钮）panel》
		/// </summary>
		/// <param name="visible"></param>
		protected override void enablePlayPanel(bool enable)
		{
			//MARK 3.0413 设定是否显示《 （调试区域的N个按钮）panel》
			//playFlowLayoutPanel.Enabled = enable;
		}

		/// <summary>
		/// 辅助方法：初始化（StepTime）各控件的属性值
		/// </summary>
		protected override void initStNumericUpDowns()
		{			
			for (int i = 0; i < 32; i++)
			{
				tdStNumericUpDowns[i].Maximum = EachStepTime2 * MAX_StTimes;
				tdStNumericUpDowns[i].Increment = EachStepTime2;
			}
		}

		//MARK 只开单场景：02.0.2 (SkinMainForm)改变当前Scene
		protected override void changeCurrentScene(int sceneIndex)
		{
			CurrentScene = sceneIndex;
			sceneSkinComboBox.SelectedIndexChanged -= sceneSkinComboBox_SelectedIndexChanged;
			sceneSkinComboBox.SelectedIndex = CurrentScene;
			sceneSkinComboBox.SelectedIndexChanged += sceneSkinComboBox_SelectedIndexChanged;
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
				if (generateNow)
				{					
					generateLightData();    //lightsSkinListView_SelectedIndexChanged
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

			currentLightPictureBox.Image = lightImageList.Images.ContainsKey(la.LightPic) ? Image.FromFile(SavePath + @"\LightPic\" + la.LightPic):global::LightController.Properties.Resources.灯光图;
			lightNameLabel.Text = LanguageHelper.TranslateWord("厂商：") + la.LightName;
			lightTypeLabel.Text = LanguageHelper.TranslateWord("型号：") + la.LightType;
			lightsAddrLabel.Text = LanguageHelper.TranslateWord("地址：") + la.LightAddr;
			lightRemarkLabel.Text = LanguageHelper.TranslateWord("备注：") + la.Remark;
		
		}

		/// <summary>
		/// 辅助方法：
		/// 1.根据传进的lightIndex，生成相应的SaPanel-->若已存在，则不再生成；
		/// 2.显示相应的Panel，隐藏无关的Panel（至于Panel的Enable属性，则由ShowStepLabel方法来进行判断）
		/// </summary>
		/// <param name="lightIndex"></param>
		protected override void generateSaPanels()
		{
			if (!IsShowSaPanels)
			{
				for (int tdIndex = 0; tdIndex < 32; tdIndex++)
				{
					saPanels[tdIndex].Hide();
				}
				return;
			}

			// 无步数时，直接跳过生成或显示saPanels的步骤
			if (getCurrentStep() == 0)
			{
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
							Size = saFLPDemo.Size,
							Dock = saFLPDemo.Dock,
							AutoScroll = saFLPDemo.AutoScroll,
							//BorderStyle = BorderStyle.Fixed3D
						});

						la.saPanelDict[tdIndex].Controls.Add(new Label
						{
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
				if (la.saPanelDict.ContainsKey(tdIndex) && IsShowSaPanels )
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
		/// 辅助方法：使能《stepPanel》
		/// </summary>
		/// <param name="la"></param>
		protected override void enableStepPanel(bool enable)
		{
			stepSkinPanel.Enabled = enable;
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
				for (int tdIndex = 0; tdIndex < 32; tdIndex++)
				{
					tdPanels[tdIndex].Hide();
					saPanels[tdIndex].Hide();
				}
				labelPanel.Hide();
			}
			//2.将dataWrappers的内容渲染到起VScrollBar中
			else
			{				
				labelPanel.Show();
				for (int i = 0; i < tongdaoList.Count; i++)
				{
					tdTrackBars[i].ValueChanged -= tdSkinTrackBars_ValueChanged;
					tdValueNumericUpDowns[i].ValueChanged -= tdValueNumericUpDowns_ValueChanged;
					tdCmComboBoxes[i].SelectedIndexChanged -= tdChangeModeSkinComboBoxes_SelectedIndexChanged;
					tdStNumericUpDowns[i].ValueChanged -= tdStepTimeNumericUpDowns_ValueChanged;

					tdNoLabels[i].Text = LanguageHelper.TranslateWord("通道") + (startNum + i);
					tdNameLabels[i].Text = tongdaoList[i].TongdaoName;
					myToolTip.SetToolTip(tdNameLabels[i], tongdaoList[i].Remark);
					tdTrackBars[i].Value = tongdaoList[i].ScrollValue;
					tdValueNumericUpDowns[i].Text = tongdaoList[i].ScrollValue.ToString();
					tdCmComboBoxes[i].SelectedIndex = tongdaoList[i].ChangeMode;
					//MARK 步时间 SkinMainForm：主动 乘以时间因子 后 再展示
					tdStNumericUpDowns[i].Text = (tongdaoList[i].StepTime * EachStepTime2).ToString();

					tdTrackBars[i].ValueChanged += tdSkinTrackBars_ValueChanged;
					tdValueNumericUpDowns[i].ValueChanged += tdValueNumericUpDowns_ValueChanged;
					tdCmComboBoxes[i].SelectedIndexChanged += tdChangeModeSkinComboBoxes_SelectedIndexChanged;
					tdStNumericUpDowns[i].ValueChanged += tdStepTimeNumericUpDowns_ValueChanged;

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
		/// 辅助方法：添加或修改备注
		/// </summary>
		/// <param name="lightIndex"></param>
		/// <param name="remark"></param>
		public override void EditLightRemark(int lightIndex, string remark)
		{
			base.EditLightRemark(lightIndex, remark);
			// 界面的Items[lightIndex]也要改动相应的值；			
			lightsSkinListView.Items[lightIndex].SubItems[0].Text =
				LightAstList[lightIndex].LightType + "\n("
				+ LightAstList[lightIndex].LightAddr + ")"
				+ LightAstList[lightIndex].Remark;
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
				lightsSkinListView.Items[lightIndex].ImageKey = lightImageList.Images.ContainsKey(tempPicStr) ? tempPicStr: "灯光图.png";				
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
			autoEnableSLArrange(); //autoArrangeToolStripMenuItem_Click
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
			if (LightAstList == null || LightAstList.Count == 0)
			{
				MessageBox.Show("当前工程尚无灯具，无法保存灯具位置，请添加灯具后重新保存。");
				return;
			}

			// 4.保存操作
			IniHelper iniFileAst = new IniHelper(arrangeIniPath);
			iniFileAst.WriteInt("Common", "Count", lightsSkinListView.Items.Count);
			for (int i = 0; i < lightsSkinListView.Items.Count; i++)
			{
				iniFileAst.WriteInt("Position", i + "X", lightsSkinListView.Items[i].Position.X);
				iniFileAst.WriteInt("Position", i + "Y", lightsSkinListView.Items[i].Position.Y);
			}
			autoEnableSLArrange(); //saveArrangeToolStripMenuItem_Click

			SetNotice("灯具位置保存成功。",true, true);
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
			IniHelper iniFileAst = new IniHelper(arrangeIniPath);
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
			hideMenuPanelToolStripMenuItem2.Text = menuSkinPanel.Visible ? "隐藏主菜单面板" : "显示主菜单面板";
		}

		private void hideProjectPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			projectSkinPanel.Visible = !projectSkinPanel.Visible;
			hideProjectPanelToolStripMenuItem2.Text = projectSkinPanel.Visible ? "隐藏工程面板" : "显示工程面板";
		}

		private void hideAstPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			astSkinPanel.Visible = !astSkinPanel.Visible;
			hideAstPanelToolStripMenuItem2.Text = astSkinPanel.Visible ? "隐藏辅助面板" : "显示辅助面板";
		}

		private void showSaPanelsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IsShowSaPanels = !IsShowSaPanels;
			showSaPanelsToolStripMenuItem.Text = IsShowSaPanels ? "隐藏子属性面板" : "显示子属性面板";
			generateSaPanels();
		}

		#endregion

		//MARK：SkinMainForm步数相关的按钮及辅助方法起点
		#region  stepPanel相关的方法

		/// <summary>
		/// 事件：更改《选择场景》选项后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void sceneSkinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			sceneSelectedChanged( sceneSkinComboBox.SelectedIndex);
		}

		/// <summary>
		/// 事件：更改《选择模式》选项后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void modeSkinComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			modeSelectedChanged(
				modeSkinComboBox.SelectedIndex,
				tdCmComboBoxes,
				tdStNumericUpDowns,
				thirdLabel);
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
			enterMultiMode(  !IsMultiMode , lightsSkinListView );			
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
				chooseStep(1); //backStepSkinButton_MouseDown
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
				chooseStep(getCurrentTotalStep());  // nextStepSkinButton_MouseDown
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
			chooseStep(step);  // chooseStepSkinButton_Click
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
			InsertStepClick(e.Button);			
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
			appendStepClick(e.Button);
		}

		/// <summary>
		///  事件：空方法，作用为方便查找deleteStepSkinButton_MouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteStepSkinButton_Click(object sender, EventArgs e){	}

		/// <summary>
		/// 事件：鼠标（左|右键）按下《删除步》	
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteStepSkinButton_MouseDown(object sender, MouseEventArgs e)
		{
			deleteStepClick(e.Button);
		}

		/// <summary>
		/// 事件：点击《复制步》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void copyStepSkinButton_Click(object sender, EventArgs e)
		{
			copyStepClick();
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
		///  事件：点击《保存素材》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveMaterialSkinButton_Click(object sender, EventArgs e)
		{
			saveMaterialClick();
		}

		/// <summary>
		///  事件：点击《使用素材》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void useMaterialSkinButton_Click(object sender, EventArgs e)
		{
			useMaterialClick();
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
			foreach (int lightIndex in SelectedIndices)
			{
				if (lightIndex == selectedIndex)
				{
					lightsAddrLabel.Text += "(" + LightAstList[lightIndex].LightAddr + ") ";
					lightsSkinListView.Items[lightIndex].BackColor = Color.LightSkyBlue;
				}
				else
				{
					lightsAddrLabel.Text += LightAstList[lightIndex].LightAddr + " ";
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
			this.IsMultiMode = isMultiMode;
		
			//MARK 只开单场景：15.2 《灯具列表》是否可用，由单灯模式决定
			lightListSkinButton.Enabled = !isMultiMode;
			lightsSkinListView.Enabled = !isMultiMode;
			sceneSkinComboBox.Enabled = !isMultiMode;
			modeSkinComboBox.Enabled = !isMultiMode;
			copyFrameSkinButton.Enabled = !isMultiMode;
			groupFlowLayoutPanel.Enabled = LightAstList != null;   // 只要当前工程有灯具，就可以进入编组（再由按钮点击事件进行进一步确认）

			multiLightSkinButton.Text = !isMultiMode ? "多灯模式" : "单灯模式";
		}

		/// <summary>
		///辅助方法：重置syncMode的相关属性，ChangeFrameMode、ClearAllData()、更改灯具列表后等？应该进行处理。
		/// </summary>
		public override void EnterSyncMode(bool isSyncMode)
		{
			this.isSyncMode = isSyncMode;
			syncSkinButton.Text = isSyncMode ? "退出同步" : "进入同步";			
			SetNotice(isSyncMode ? "已进入同步模式。" : "已退出同步模式。", false, true);
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
			deleteSkinButton.Enabled = totalStep != 0;

			// 2.2 设定《追加步》、《前插入步》《后插入步》按钮是否可用			
			bool insertEnabled = totalStep < MAX_STEP;
			appendSkinButton.Enabled = insertEnabled;
			insertSkinButton.Enabled = insertEnabled ;

			// 2.3 设定《上一步》《下一步》是否可用			
			backStepSkinButton.Enabled = totalStep > 1;
			nextStepSkinButton.Enabled = totalStep > 1;

			//3 设定《复制(多)步、保存素材》、《多步复用》等是否可用
			copyStepSkinButton.Enabled = currentStep > 0;
			pasteStepSkinButton.Enabled = currentStep > 0 && tempStep != null;

			saveMaterialSkinButton.Enabled = currentStep > 0; // 当前步不为0时才能保存素材；但无论什么情况都能使用素材，故不做判断
			multiplexSkinButton.Enabled = currentStep > 0;						

			// 4.设定统一调整区是否可用
			groupButton.Enabled = LightAstList != null && lightsSkinListView.SelectedIndices.Count > 0; //只有工程非空（有灯具列表）且选择项不为空 才可点击
			groupFlowLayoutPanel.Enabled = LightAstList != null ; 			
			multiButton.Enabled = totalStep != 0;
			detailMultiButton.Enabled = totalStep != 0;
			soundListButton.Enabled = !string.IsNullOrEmpty(currentProjectName) && CurrentMode == 1 ;

            // 5. 处理选择步数的框及按钮
            chooseStepNumericUpDown.Enabled = totalStep != 0;			
			chooseStepNumericUpDown.Minimum = totalStep != 0 ? 1 : 0;
			chooseStepNumericUpDown.Maximum = totalStep;
			chooseStepSkinButton.Enabled = totalStep != 0;

			//6.内置动作是否可用：因为需要一直有效，所以只能在点击时进行判断了
			//actionButton.Enabled = CurrentMode == 0;

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
        private void tdSkinTrackBars_ValueChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("tdSkinTrackBars_ValueChanged");
            // 1.先找出对应tdSkinTrackBars的index 
            int tongdaoIndex = MathHelper.GetIndexNum(((SkinTrackBar)sender).Name, -1);
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
            tdTrackBars[tongdaoIndex].ValueChanged -= tdSkinTrackBars_ValueChanged;
            tdTrackBars[tongdaoIndex].Value = tdValue;
            tdTrackBars[tongdaoIndex].ValueChanged += tdSkinTrackBars_ValueChanged;

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
			if (IsMultiMode) {
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
			int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name,-1);
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

			//MARK 步时间 SkinMainForm：处理为数据库所需数值：将 (显示的步时间* 时间因子)后再放入内存
			int stepTime = decimal.ToInt32(tdStNumericUpDowns[tdIndex].Value / EachStepTime2); // 取得的值自动向下取整（即舍去多余的小数位）
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
		///   事件：通道相关的按键点击事件：设为该输入框的值（a为当前通道所有步、s为当前步所有通道）
		///  </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyTd_KeyPress(object sender, KeyPressEventArgs e)
		{
			unifyTdKeyPress(sender, e);
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
			groupButtonClick(lightsSkinListView);
		}

		/// <summary>
		/// 事件：点击《多步调节》按钮
		/// 多步调整，传入当前灯的LightWrapper，在里面回调setMultiStepValues以调节相关的步数的值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multiButton_Click(object sender, EventArgs e){  }

		/// <summary>
		/// 事件：左右键点击《多步调节》：右键暂无作用
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

		/// <summary>
		/// 事件：为方便定位detailMultiButton_MouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void detailMultiButton_Click(object sender, EventArgs e) { }

		/// <summary>
		/// 事件：左右键点击《多步调节》
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
		/// 事件：点击《音频链表》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void soundListButton_Click(object sender, EventArgs e)
        {
           new SKForm(this, CurrentScene, sceneSkinComboBox.Text).ShowDialog();  
        }

        /// <summary>
        /// 事件：点击《groupInButtons(进入编组)》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupInButton_Click(object sender, EventArgs e)
		{
			groupInButtonClick(sender,lightsSkinListView);
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
			for (int i = 0; i < SelectedIndices.Count; i++)
			{
				if (i == SelectedIndices.Count - 1)
				{
					generateNow = true;
				}
				int lightIndex = SelectedIndices[i];
				lightsSkinListView.Items[lightIndex].Selected = true;
			}
		}
			   	
		#endregion

		//MARK：SkinMainForm：playPanel相关点击事件及辅助方法	
		#region 灯控调试按钮组(playPanel)点击事件及辅助方法		

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
			PreviewButtonClick(null);
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

			// MARK3.0414 EnableConnectedButtons()
			hardwareSetSkinButton.Enabled = connected;
			newToolsSkinButton.Enabled = connected;
			projectUpdateSkinButton.Enabled = connected;
	
			keepSkinButton.Enabled = IsConnected && !IsPreviewing;
			previewSkinButton.Enabled = IsConnected;
			makeSoundSkinButton.Enabled = IsConnected && IsPreviewing;
			
			SetPreview(IsPreviewing);

			//721：进入连接但非调试模式时，刷新当前步(因为有些操作是异步的，可能造成即时的刷新步数，无法进入单灯单步)
			if (IsConnected && !IsPreviewing)
			{
				RefreshStep();
			}
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
		/// <param name="notice"></param>
		public override void SetNotice(string notice, bool msgBoxShow,bool isTranslate)
		{
			if (isTranslate) {
				notice = LanguageHelper.TranslateSentence(notice);
			}

			noticeLabel.Text = notice;
			noticeStatusStrip.Refresh();
			if (msgBoxShow) {
				MessageBox.Show(notice);
			}
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
				Console.WriteLine("Dickov : bgWorker_WorkerCompleted");
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
			//Test test = new Test(GetDBWrapper(true) );
		}

		/// <summary>
		/// 事件：点击《自定义测试按钮》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bigTestButton_Click(object sender, EventArgs e)	{

			LanguageHelper.SetLanguage("en-US");

		}

		#endregion

		/// <summary>
		/// 部分Control文本更新，需要进行翻译
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someControl_TextChanged(object sender, EventArgs e)
		{
			LanguageHelper.TranslateControl(sender as Control);
		}

		/// <summary>
		///  部分MenuItem需要在文本发生变化时，进行翻译
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someMenuItem_TextChanged(object sender, EventArgs e)
		{
			LanguageHelper.TranslateMenuItem( sender as ToolStripMenuItem);
		}


	}
	   
}
