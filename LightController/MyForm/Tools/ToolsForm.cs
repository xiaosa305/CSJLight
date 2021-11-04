using CCWin.SkinControl;
using LightController.Common;
using LightController.Entity;
using LightController.Tools.CSJ.IMPL;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace LightController.MyForm.OtherTools
{
	public partial class ToolsForm : Form
	{
		/// <summary>
		/// 连接状态的枚举
		/// </summary>
		enum ConnectStatus
		{
			No,	
			Normal, 
			Lc, 
			Cc,
			Kp
		}
		/// <summary>
		/// 状态栏的枚举
		/// </summary>
		/// Root
		enum StatusLabel
		{
			LEFT, RIGHT
		}

		private MainFormBase mainForm;
		private ConnectStatus connStatus = ConnectStatus.No;   //初始状态为未连接
						
		private CCEntity ccEntity; // 中控封装对象
		private bool isDecoding = false; //中控是否开启解码

		private LightControlData lcEntity; //灯控封装对象	
		private int sceneCount = 17; // 场景的数量（开机场景 + 16）
		private int relayCount = 6; // 开关的数量		 		
		public static int SCR_MAX = 18;
		private FlowLayoutPanel[] relayFLPs;
		private Panel[] relayPanels;
		private Label[] sceneLabels;
		private CheckBox[] sceneCBs;
		private Button[,] relayButtons;
		private bool isDebuging = false; // 是否正在发送调试数据，只有Completed|Error回调后，才会设为false；
		private string selectedSceneName;
		private int tgCount = 2; // 调光通道相关
		private Panel[,] tgPanels;
		private Label[,] tgLabels;
		private TrackBar[,] tgTrackBars;
		private NumericUpDown[,] tgNUDs;

		private KeyEntity kpEntity;  // 墙板封装对象
		private List<string> kpCodeList;   // 记录搜索到的码值列表（不用Dictionary，因为没有存储功能描述的必要，且Dictionary无序）		
		private System.Timers.Timer kpTimer; //墙板定时刷新的定时器（因为透传模式，若太久（10s）没有连接，则会自动退出透传模式）
		
		public ToolsForm(MainFormBase mainForm,int selectedProtocolIndex)
		{
			InitializeComponent();
			this.mainForm = mainForm;

			#region 初始化各组件					
			pbinSaveDialog.InitialDirectory = Application.StartupPath + @"\protocol";

			// 各强电开关
			relayFLPs = new FlowLayoutPanel[sceneCount];
			relayPanels = new Panel[sceneCount];
			sceneLabels = new Label[sceneCount];
			sceneCBs = new CheckBox[sceneCount];
			relayButtons = new SkinButton[sceneCount , relayCount];
			// 各调光通道
			tgPanels = new Panel[sceneCount, tgCount];
			tgLabels = new Label[sceneCount, tgCount];
			tgTrackBars = new TrackBar[sceneCount, tgCount];
			tgNUDs = new NumericUpDown[sceneCount, tgCount];

			for (int sceneIndex = 0; sceneIndex < sceneCount; sceneIndex++) {

				relayFLPs[sceneIndex] = new FlowLayoutPanel
				{
					BorderStyle = relayFLPDemo.BorderStyle,
					Location = relayFLPDemo.Location,
					Size = relayFLPDemo.Size,
					AutoSize = relayFLPDemo.AutoSize,
				};
				relayBigFLP.Controls.Add(relayFLPs[sceneIndex]);

				relayPanels[sceneIndex] = new Panel()
				{
					Location = relayPanelDemo.Location,
					Size = relayPanelDemo.Size,
					BorderStyle = relayPanelDemo.BorderStyle,
				};

				sceneLabels[sceneIndex] = new Label
				{
					Location = sceneLabelDemo.Location,
					Size = sceneLabelDemo.Size,
					TextAlign = sceneLabelDemo.TextAlign,
					Name = sceneIndex.ToString()
				};
				sceneLabels[sceneIndex].Click += sceneLabels_Click;
				sceneCBs[sceneIndex] = new CheckBox
				{
					AutoSize = sceneCBDemo.AutoSize = true,
					Location = sceneCBDemo.Location,
					Size = sceneCBDemo.Size,
					UseVisualStyleBackColor = sceneCBDemo.UseVisualStyleBackColor,
					Text = sceneCBDemo.Text,
					Name = sceneIndex.ToString()
				};
				sceneCBs[sceneIndex].CheckedChanged += sceneCBs_CheckedChanged;

				relayPanels[sceneIndex].Controls.Add(sceneLabels[sceneIndex]);
				relayPanels[sceneIndex].Controls.Add(sceneCBs[sceneIndex]);

				relayFLPs[sceneIndex].Controls.Add(relayPanels[sceneIndex]);

				for (int relayIndex = 0; relayIndex < relayCount; relayIndex++)
				{
					relayButtons[sceneIndex,relayIndex] = new SkinButton
					{
						BackColor = relayButtonDemo.BackColor,
						BaseColor = relayButtonDemo.BaseColor,
						BorderColor = relayButtonDemo.BorderColor,
						ControlState = relayButtonDemo.ControlState,
						DownBack = relayButtonDemo.DownBack,
						DrawType = relayButtonDemo.DrawType,
						Font = relayButtonDemo.Font,
						ForeColor = relayButtonDemo.ForeColor,
						ForeColorSuit = relayButtonDemo.ForeColorSuit,
						ImageAlign = relayButtonDemo.ImageAlign,
						ImageIndex = relayButtonDemo.ImageIndex,
						ImageList = relayButtonDemo.ImageList,
						ImageSize = relayButtonDemo.ImageSize,
						InheritColor = relayButtonDemo.InheritColor,
						IsDrawBorder = relayButtonDemo.IsDrawBorder,
						Location = relayButtonDemo.Location,
						Margin = relayButtonDemo.Margin,
						MouseBack = relayButtonDemo.MouseBack,
						NormlBack = relayButtonDemo.NormlBack,
						Size = relayButtonDemo.Size,
						Tag = relayButtonDemo.Tag,
						TextAlign = relayButtonDemo.TextAlign,
						UseVisualStyleBackColor = relayButtonDemo.UseVisualStyleBackColor,
						Visible = true,
						Name = sceneIndex + "," + relayIndex ,
						Text = LanguageHelper.TranslateWord("开关") + (relayIndex + 1)
					};
					relayButtons[sceneIndex,relayIndex].Click += relayButtons_Click ;
					relayFLPs[sceneIndex].Controls.Add(relayButtons[sceneIndex, relayIndex]);
				}
				
				// 各调光通道
				for (int tgIndex = 0; tgIndex < tgCount; tgIndex++) {

					tgPanels[sceneIndex, tgIndex] = new Panel
					{
						Location = tgPanelDemo.Location,
						Size = tgPanelDemo.Size,
						BorderStyle = tgPanelDemo.BorderStyle,
						Visible = tgPanelDemo.Visible
					};

                    tgLabels[sceneIndex, tgIndex] = new Label
                    {
                        AutoSize = tgLabelDemo.AutoSize,
                        Location = tgLabelDemo.Location,
                        Size = tgLabelDemo.Size,
                        Text = "调光"+(tgIndex+1)+"："
                    };

                    tgTrackBars[sceneIndex, tgIndex] = new TrackBar
                    {
                        Location = tgTrackBarDemo.Location,
                        Maximum = SCR_MAX,
                        Size = tgTrackBarDemo.Size,
                        TickStyle = tgTrackBarDemo.TickStyle,
						Name = sceneIndex + "," + tgIndex ,
					};

					tgNUDs[sceneIndex, tgIndex] = new NumericUpDown
                    {
                        Location = tgNUDDemo.Location,
                        Size = tgNUDDemo.Size,
                        TextAlign = tgNUDDemo.TextAlign,
						Maximum = SCR_MAX,
						Name = sceneIndex + "," + tgIndex,
					};					

					tgPanels[sceneIndex, tgIndex].Controls.Add(tgLabels[sceneIndex, tgIndex]);  
					tgPanels[sceneIndex, tgIndex].Controls.Add(tgTrackBars[sceneIndex, tgIndex]);
					tgPanels[sceneIndex, tgIndex].Controls.Add(tgNUDs[sceneIndex, tgIndex]);

					relayFLPs[sceneIndex].Controls.Add(tgPanels[sceneIndex,tgIndex]);

					//添加监听器
					tgTrackBars[sceneIndex, tgIndex].ValueChanged += tgTrackBars_ValueChanged;
					tgTrackBars[sceneIndex, tgIndex].MouseWheel += someTrackBar_MouseWheel;					
					tgNUDs[sceneIndex, tgIndex].ValueChanged += tgNUDs_ValueChanged;
					tgNUDs[sceneIndex, tgIndex].MouseWheel += someNUD_MouseWheel;					
					tgNUDs[sceneIndex, tgIndex].KeyUp += tgNUDs_KeyUp;					
				}
			}

			myToolTip.SetToolTip(protocolSaveButton, "请不要在软件使用过程中删除pbin文件。");
			myToolTip.SetToolTip(keepLightOnCheckBox, "选中常亮模式后:\n1.点亮或关闭每一个《继电器开关》，所有场景的相应《继电器开关》都会随之变动;\n2.调节《调光值》时，所有场景的《调光值》都会随之变动。");
			myToolTip.SetToolTip(fillCodeAllButton, "点击此按键会将选中项的键码值填入左侧两个文本框中;\n双击右边列表的键码值也可实现同样效果。");

			// 初始化墙板配置界面的TabControl
			tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
			tabControl1.Alignment = TabAlignment.Left;
			tabControl1.SizeMode = TabSizeMode.Fixed;
			tabControl1.Multiline = true;
			tabControl1.ItemSize = new Size(60, 100);
			
			// 由传入的协议index，渲染协议ComboBox
			renderProtocolCB(selectedProtocolIndex);

			#endregion
		}
		
		private void ToolsFormcs_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);

            //LanguageHelper.InitForm(this);
            //LanguageHelper.TranslateListView(protocolListView);
            //LanguageHelper.TranslateListView(keypressListView);		
            //deviceNameLabel.Text = mainForm.MyConnect.DeviceName;

			
		}

		private void ToolsForm_Shown(object sender, EventArgs e)
		{			
			tabControl1_SelectedIndexChanged(null, null); // 主动触发连接外设的操作
		}
		
		/// <summary>
		/// 辅助方法：通过filePath，读取其内部数据（基本上相同的文件存储格式)，组成IList<string>并返回。
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		private IList<string> getParamListFromPath(string filePath)
		{
			IList<string> paramList = new List<string>();
			try
			{
				if (File.Exists(filePath))
				{
					using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
					using (StreamReader sr = new StreamReader(fs))
					{
						string sTemp;
						while ((sTemp = sr.ReadLine()) != null)
						{
							sTemp = sTemp.Trim();
							if (sTemp.Length > 0)
							{
								paramList.Add(sTemp);
							}
						}
					}
				}
				else
				{
					setNotice(0, "文件不存在", true, true);
					return null;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}
			return paramList;
		}
		
		/// <summary>
		/// 事件：切换不同的tabPage时，切换连接的方式
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (mainForm.IsDeviceConnected)
			{
				setBusy(true);
				mainForm.SleepBetweenSend("Order : 切换外设状态[" + tabControl1.SelectedIndex+"]", 1);
				switch (tabControl1.SelectedIndex)
				{
					case 0:
						setNotice(StatusLabel.RIGHT, "正在切换中控配置，请稍候...", false, true);
						mainForm.MyConnect.CenterControlConnect(CCConnectCompleted, CCConnectError);
						break;
					case 1:
						setNotice(StatusLabel.RIGHT, "正在切换灯控配置，请稍候...", false, true);
						mainForm.MyConnect.LightControlConnect(LCConnectCompleted, LCConnectError);
						break;
					case 2:
						setNotice(StatusLabel.RIGHT, "正在切换墙板配置，请稍候...", false, true);
						mainForm.MyConnect.PassThroughKeyPressConnect(KPFirstConnectCompleted, KPConnectError);
						break;
				}
			}
		}

		/// <summary>
		/// 辅助方法：设置connStatus值，并根据此值刷新按键是否可用；
		/// </summary>
		/// <param name="cs"></param>
		private void setConnStatus(ConnectStatus cs)
		{
			connStatus = cs;
			switch (connStatus)
			{
				case ConnectStatus.No: setNotice(StatusLabel.LEFT, "尚未连接设备", false, true); break;
				case ConnectStatus.Normal: setNotice(StatusLabel.LEFT, "已连接设备", false, true); break;
				case ConnectStatus.Lc: setNotice(StatusLabel.LEFT, "当前状态为：灯控配置", false, true); break;
				case ConnectStatus.Cc: setNotice(StatusLabel.LEFT, "当前状态为：中控配置", false, true); break;
				case ConnectStatus.Kp: setNotice(StatusLabel.LEFT, "当前状态为：墙板配置", false, true); break;
				default: setNotice(StatusLabel.LEFT, "", false, false); setNotice(StatusLabel.RIGHT, "", false, false); break;
			}
			refreshStatusButtons(); //setConnStatus
		}

		/// <summary>
		/// 刷新所有被connStatus影响的按键
		/// </summary>
		private void refreshStatusButtons()
		{
			// 协议另存按键
			protocolSaveButton.Enabled = ccEntity != null ;
			
			// 灯控相关按键
			lcReadButton.Enabled = connStatus == ConnectStatus.Lc;
			lcDownloadButton.Enabled = connStatus == ConnectStatus.Lc && lcEntity != null;			
			lcSaveButton.Enabled = lcEntity != null;

			// 中控相关			
			ccDownloadButton.Enabled = connStatus == ConnectStatus.Cc && ccEntity != null && !isDecoding;

			// 墙板相关按键
			kpReadButton.Enabled = connStatus == ConnectStatus.Kp;		
			bool keNotNull = kpEntity != null;
			kpSaveButton.Enabled = keNotNull;
			kpDownloadButton.Enabled = connStatus == ConnectStatus.Kp && keNotNull;
		}

		/// <summary>
		/// 辅助方法：如果下载配置成功，则应该重连设备；此法被灯控和中控下载成功的回调函数调用；
		/// </summary>
		private void reconnectDevice()
		{
			mainForm.DisConnect();
			setConnStatus(ConnectStatus.No);
			mainForm.ConnForm.ShowDialog();
			if (mainForm.IsDeviceConnected)
			{
				setConnStatus(ConnectStatus.Normal);
				tabControl1_SelectedIndexChanged(null, null);
			}
			else
			{
				MessageBox.Show("请重新连接设备，否则无法进行外设配置!");
				Dispose();
			}
		}

		#region 协议相关
		private void renderProtocolCB(int protocolIndex) {
			protocolComboBox.Items.Clear();
			foreach (string protocolName in mainForm.ProtocolList)
			{
				protocolComboBox.Items.Add(protocolName);
			}
			protocolComboBox.SelectedIndex = protocolIndex;
		}

		/// <summary>
		/// 事件：更改协议ComboBox的选项，先渲染后保存到注册表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void protocolComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ccEntity = mainForm.GenerateCCEntity( protocolComboBox.SelectedIndex );

			if (ccEntity != null)
			{
				com0Label.Text = LanguageHelper.TranslateWord("串口0 = ") + ccEntity.Com0;
				com1Label.Text = LanguageHelper.TranslateWord("串口1 = ") + ccEntity.Com1;
				PS2Label.Text = "PS2 = " + (ccEntity.PS2 == 0 ? "主" : "从");

				protocolListView.Items.Clear();
				for (int rowIndex = 0; rowIndex < ccEntity.CCDataList.Count; rowIndex++)
				{
					ListViewItem item = new ListViewItem((rowIndex + 1).ToString());
					item.SubItems.Add(ccEntity.CCDataList[rowIndex].Function.Trim());
					item.SubItems.Add(ccEntity.CCDataList[rowIndex].Code.Trim());
					item.SubItems.Add(ccEntity.CCDataList[rowIndex].Com0Up.Trim());
					item.SubItems.Add(ccEntity.CCDataList[rowIndex].Com0Down.Trim());
					item.SubItems.Add(ccEntity.CCDataList[rowIndex].Com1Up.Trim());
					item.SubItems.Add(ccEntity.CCDataList[rowIndex].Com1Down.Trim());
					item.SubItems.Add(ccEntity.CCDataList[rowIndex].InfraredSend.Trim());
					item.SubItems.Add(ccEntity.CCDataList[rowIndex].InfraredReceive.Trim());
					item.SubItems.Add(ccEntity.CCDataList[rowIndex].PS2Up.Trim());
					item.SubItems.Add(ccEntity.CCDataList[rowIndex].PS2Down.Trim());
					protocolListView.Items.Add(item);
				}
				setNotice(StatusLabel.RIGHT, "已加载" + mainForm.ProtocolList[protocolComboBox.SelectedIndex], false, true);

				sceneLabels[0].Text = "开机场景";	
				for (int codeIndex = 0; codeIndex < 16; codeIndex++)
				{
					sceneLabels[codeIndex+1].Text = ccEntity.CCDataList[ Convert.ToInt32(mainForm.SceneCodeList[codeIndex], 16) - 1]. Function ;
				}
				
			}
			refreshStatusButtons();
		}

		/// <summary>
		/// 事件：点击《协议另存为》后把用户修改过的协议保存成pbin二进制文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void protocolSaveButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == pbinSaveDialog.ShowDialog())
			{
				if (ccEntity != null)
				{
					try
					{						
						string pbinPath = pbinSaveDialog.FileName; // 因为存储为pbin的名字不会重复，所以只要超过xls数量的索引，可以由名字获取相应的index
						
						SerializeUtils.SerializeObject(pbinPath, ccEntity);
						setNotice(StatusLabel.RIGHT, "成功另存协议。", true, true);

                        //211103 只有在新增了协议之后，才可能修改  本界面选择的协议 以及 主页的选择项； 
						string oldProtocolName = mainForm.ProtocolList[mainForm.CurrentProtocol] ; //在改变之前存储一个之前协议名称，如果首页选中的是pbin项，则会用到
						mainForm.LoadProtocols();
						int newProtocolIndex = mainForm.GetIndexByPbinName( Path.GetFileNameWithoutExtension(pbinPath) );  
						renderProtocolCB(newProtocolIndex);
						mainForm.RenderProtocolCB(oldProtocolName); // 传旧名字给mainForm，就能在更改过protocolCB的项后，仍然定位到原来的index

					}
					catch (Exception ex)
					{
						setNotice(StatusLabel.RIGHT, "另存协议失败：" + ex.Message, true, false);
					}
				}
			}
		}		

		#endregion

		#region 中控相关

		/// <summary>
		/// 事件：点击《(中控)搜索》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ccSearchButton_Click(object sender, EventArgs e)
		{
			// 检查代码的顺序，会影响程序的效率
			// 检查关键字
			string keyword = ccSearchTextBox.Text.Trim();
			if (keyword.Equals(""))
			{
				setNotice(0, "请输入搜索关键字。", true, true);
				return;
			}

			// 检查是否已载入协议
			if (ccEntity == null)
			{
				setNotice(0, "请先加载协议(cc为空)", true, true);
				return;
			}

			// 清空之前的选择项
			foreach (int seletedIndex in protocolListView.SelectedIndices)
			{
				protocolListView.Items[seletedIndex].Selected = false;
			}

			// 由关键字搜索相应的indexList ，再遍历选中所有匹配项
			IList<int> matchIndexList = ccEntity.SearchIndices(keyword);
			foreach (int matchIndex in matchIndexList)
			{
				protocolListView.Items[matchIndex].Selected = true;
			}
			// 最后恢复listView的焦点
			protocolListView.Select();
		}

		/// <summary>
		/// 事件：双击《协议列表》的其中一项，应该弹出该项的各个数值，其中有些写死有些可以更改（新的窗口）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void protocolListView_DoubleClick(object sender, EventArgs e)
		{
			int protocolDataIndex = protocolListView.SelectedIndices[0];
			new ProtocolDataForm(mainForm, this,protocolDataIndex, ccEntity.CCDataList[protocolDataIndex]).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：主要供《ProtocolDataForm》调用，用以修改协议行的内容；
		/// </summary>
		/// <param name="ccdIndex"></param>
		/// <param name="ccd"></param>
		public void EditProtocol( int ccdIndex,CCData ccd)
		{
			ccEntity.CCDataList[ccdIndex] = ccd;
			
			protocolListView.Items[ccdIndex].SubItems[3].Text = ccEntity.CCDataList[ccdIndex].Com0Up;
			protocolListView.Items[ccdIndex].SubItems[4].Text = ccEntity.CCDataList[ccdIndex].Com0Down;
			protocolListView.Items[ccdIndex].SubItems[5].Text = ccEntity.CCDataList[ccdIndex].Com1Up;
			protocolListView.Items[ccdIndex].SubItems[6].Text = ccEntity.CCDataList[ccdIndex].Com1Down;
			protocolListView.Items[ccdIndex].SubItems[7].Text = ccEntity.CCDataList[ccdIndex].InfraredSend;
			protocolListView.Items[ccdIndex].SubItems[8].Text = ccEntity.CCDataList[ccdIndex].InfraredReceive;
			protocolListView.Items[ccdIndex].SubItems[9].Text = ccEntity.CCDataList[ccdIndex].PS2Up;
			protocolListView.Items[ccdIndex].SubItems[10].Text = ccEntity.CCDataList[ccdIndex].PS2Down;

			setNotice(StatusLabel.RIGHT, "成功修改协议(临时)，如需长期修改请另存协议。", false, false);
		}
		
		/// <summary>
		/// 辅助回调方法：中控连接成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCConnectCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice(StatusLabel.RIGHT, "已切换成中控配置(connStatus=cc)", false, true);
				setConnStatus(ConnectStatus.Cc);
				setBusy(false);
			});
		}

		/// <summary>
		/// 辅助回调方法：中控连接失败
		/// </summary>
		public void CCConnectError(string msg)
		{
			Invoke((EventHandler)delegate {
				// 切换失败，只给提示，不更改原来的状态
				setNotice(StatusLabel.RIGHT, LanguageHelper.TranslateSentence("切换中控配置失败：") + msg, true, false);
				setBusy(false);

				reconnectDevice(); //CCConnectError
			});
		}
		
		/// <summary>
		/// 事件：点击《写入设备(下载协议)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ccDownloadButton_Click(object sender, EventArgs e)
		{
			if (ccEntity == null)
			{
				setNotice(StatusLabel.RIGHT, "当前协议为空，请先选择协议。", true, true);
				return;
			}

			// 正常情况下，在解码模式下不能下载数据，加这个判断以防万一
			if (isDecoding)
			{
				setNotice(StatusLabel.RIGHT, "在解码状态下无法下载协议，请先关闭解码。", true, true);
				return;
			}

			setNotice(StatusLabel.RIGHT, "正在下载中控协议到设备，请稍候...", false, true);
			mainForm.MyConnect.CenterControlDownload(ccEntity, CCDownloadCompleted, CCDownloadError);
		}

		/// <summary>
		/// 辅助回调方法：中控配置下载成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCDownloadCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice(StatusLabel.RIGHT, "中控配置下载成功,请等待设备重启(约耗时5s)，重新搜索并连接设备。", true, true);
				reconnectDevice();  //CCDownloadCompleted
			});
		}

		/// <summary>
		///  辅助回调方法：中控配置下载失败
		/// </summary>
		public void CCDownloadError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.RIGHT, LanguageHelper.TranslateSentence("中控配置下载失败：") + msg, true, false);
			});
		}

		#endregion

		#region 灯控相关		
						
		/// <summary>
		/// 事件：点击《灯控 - 回读配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcReadButton_Click(object sender, EventArgs e)
		{
			if (connStatus == ConnectStatus.Lc) {
				setBusy(true);
				setNotice(StatusLabel.RIGHT,"正在回读灯控配置，请稍候...",false,true);				
				mainForm.MyConnect.LightControlRead(LCReadCompleted, LCReadError);
			}		
		}

		/// <summary>
		/// 辅助方法：通过lcEntity，渲染灯控Tab内相关的控件
		/// </summary>
		private void lcRender()
		{
			if (lcEntity == null)
			{
				relayBigFLP.Enabled = false;
				refreshStatusButtons();
				setNotice(0, "lcEntity==null。", true, false);
				return;
			}

			try
			{
				relayBigFLP.Enabled = lcEntity.RelayCount != 0;

				//渲染可控硅和空调二选一的Checkbox
				tgCheckBox.Visible = lcEntity.LightControllerSCR != null;				
				tgCheckBox.Checked = lcEntity.LightControllerSCR != null && lcEntity.LightControllerSCR.IsStartSCR;			

				for (int sceneIndex = 0; sceneIndex < sceneCount; sceneIndex++)
				{
					for (int relayIndex = 0; relayIndex < relayCount; relayIndex++)
					{
						relayButtons[sceneIndex, relayIndex].ImageIndex = lcEntity.SceneData[sceneIndex, relayIndex] ? 1 : 0;
						if (lcEntity.SequencerData != null)
						{
							relayButtons[sceneIndex, relayIndex].Text = lcEntity.SequencerData.RelaySwitchNames[relayIndex];
						}
					}

					//只要LightControllerSCR不为空，就直接渲染相关控件的值，因为是否显示只需一次勾选；
					if (lcEntity.LightControllerSCR != null)
					{
						for (int tgIndex = 0; tgIndex < tgCount; tgIndex++)
						{
							tgTrackBars[sceneIndex, tgIndex].ValueChanged -= tgTrackBars_ValueChanged;
							tgTrackBars[sceneIndex, tgIndex].Value = lcEntity.LightControllerSCR.ScrData[sceneIndex, tgIndex];
							tgTrackBars[sceneIndex, tgIndex].ValueChanged += tgTrackBars_ValueChanged;

							tgNUDs[sceneIndex, tgIndex].ValueChanged -= tgNUDs_ValueChanged;
							tgNUDs[sceneIndex, tgIndex].Value = lcEntity.LightControllerSCR.ScrData[sceneIndex, tgIndex];
							tgNUDs[sceneIndex, tgIndex].ValueChanged += tgNUDs_ValueChanged;

							tgPanels[sceneIndex, tgIndex].Visible = lcEntity.LightControllerSCR.IsStartSCR;
						}
					}
				}				
				refreshStatusButtons();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}	
	
		/// <summary>
		/// 事件：点击《(灯控)打开配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcLoadButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == lbinOpenDialog.ShowDialog())
			{
				setNotice(StatusLabel.RIGHT, "正在打开本地灯控配置文件，请稍候...", false, true);
				try
				{
					if (lbinOpenDialog.SafeFileName.EndsWith("lbin"))
					{
						lcEntity = (LightControlData)SerializeUtils.DeserializeToObject(lbinOpenDialog.FileName);
					}
					else //兼容旧版cfg文件：SequencerData=null
					{
						lcEntity = new LightControlData(getParamListFromPath(lbinOpenDialog.FileName));
					}
				}
				catch (Exception ex)
				{
					setNotice(StatusLabel.RIGHT,"加载本地配置文件时发生异常：" + ex.Message, true, true);
					return;
				}
			
				lcRender();
				setNotice(StatusLabel.RIGHT,
					LanguageHelper.TranslateSentence("已加载本地灯控配置文件：") + lbinOpenDialog.FileName,
					true, false);
			}
		}

		/// <summary>
		/// 事件：点击《(灯控)保存配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcSaveButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == lbinSaveDialog.ShowDialog())
			{
				processLC(); //lcSaveButton_Click
				try
				{
					string lbinPath = lbinSaveDialog.FileName;
					SerializeUtils.SerializeObject(lbinPath, lcEntity);
					setNotice(StatusLabel.RIGHT,
						LanguageHelper.TranslateSentence("成功保存灯控配置文件为：") + lbinSaveDialog.FileName,
						true, false);
				}
				catch (Exception ex)
				{
					setNotice(StatusLabel.RIGHT, "保存配置时发生异常：" + ex.Message, true, true);
				}				
			}
		}
			   
		/// <summary>
		/// 辅助回调方法：灯控连接成功
		/// </summary>
		/// <param name="obj"></param>
		public void LCConnectCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {
				setConnStatus(ConnectStatus.Lc);
				setNotice(StatusLabel.RIGHT, "已切换成灯控配置(connStatus=lc)", false,true);		

				// 当还没有任何形式地加载lcEntity时，主动从机器回读
				if (lcEntity == null) 				lcReadButton_Click(null, null);

                // 最后再统一设使能，避免界面 反复使能开和关（视觉上效果较差）
                setBusy(false);
            });
		}	

		/// <summary>
		/// 辅助回调方法：灯控连接出错
		/// </summary>
		/// <param name="obj"></param>
		public void LCConnectError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				// 切换失败，只给提示，不更改原来的状态
				setNotice(StatusLabel.RIGHT, LanguageHelper.TranslateSentence("切换灯控配置失败：") + msg, true, false);
				setBusy(false);

				reconnectDevice(); //LCConnectError
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控数据回读成功
		/// </summary>
		/// <param name="lcDataTemp"></param>
		public void LCReadCompleted(Object lcDataTemp, string msg)
		{
			Invoke((EventHandler)delegate {
				if (lcDataTemp == null)
				{
					setNotice(StatusLabel.RIGHT, "灯控回读配置异常(lcDataTemp==null)", true, true);
					setBusy(false);
					return;
				}
				lcEntity = lcDataTemp as LightControlData;
				lcRender();
				setNotice(StatusLabel.RIGHT, "成功回读灯控配置", true, true);
				setBusy(false);
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控配置回读失败
		/// </summary>
		public void LCReadError(string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice(StatusLabel.RIGHT, LanguageHelper.TranslateSentence("回读灯控配置失败:") + msg, true, false);
				setBusy(false);
				reconnectDevice(); //LCReadError
			});
		}

		/// <summary>
		/// 事件：点击《写入设备(灯控下载配置)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcDownloadButton_Click(object sender, EventArgs e)
		{
			setNotice(StatusLabel.RIGHT, "正在下载灯控配置，请稍候...", false, true);
			processLC(); // lcDownloadButton_Click
			mainForm.MyConnect.LightControlDownload(lcEntity, LCDownloadCompleted, LCDownloadError);
		}

		/// <summary>
		/// 辅助方法：把LC中SceneData内大于6的继电器的所有值都设为false，有两处调用：写入配置和保存配置
		/// </summary>
		private void processLC()
		{
			if (lcEntity != null)
			{
				// 下载之前，因为设备处理的原因，需要把屏蔽掉的通道（启用排风或空调）的SceneData都设为false
				for (int sceneIndex = 0; sceneIndex < sceneCount; sceneIndex++)
				{
					for (int relayIndex = 6; relayIndex < 12; relayIndex++)
					{
						lcEntity.SceneData[sceneIndex, relayIndex] = false;
					}
				}
			}
		}

		/// <summary>
		/// 辅助回调方法：灯控配置下载成功
		/// </summary>
		/// <param name="obj"></param>
		public void LCDownloadCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {				
				setNotice(StatusLabel.RIGHT, "灯控配置下载成功,请等待设备重启(约耗时5s)，重新搜索并连接设备。", true, true);
				reconnectDevice(); // LCDownloadCompleted
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控配置下载错误
		/// </summary>
		public void LCDownloadError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.RIGHT, LanguageHelper.TranslateSentence("灯控配置下载失败：") + msg, true, false);
				reconnectDevice(); //LCDownloadError
			});
		}
				
		/// <summary>
		///  事件：点击《灯控配置-场景名》，发送调试数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void sceneLabels_Click(object sender, EventArgs e)
		{
			Label sceneLabel = sender as Label;
			selectedSceneName = sceneLabel.Text;

			if (connStatus != ConnectStatus.Lc && !isDebuging)
			{
				return;
			}
			isDebuging = true;
			setNotice(StatusLabel.RIGHT, "正在发送【"+ selectedSceneName + "】的《灯控开关》调试数据，请稍候...", false, true);
			Refresh();
			
			byte[] tempData = lcEntity.GetSceneDebugBytes(int.Parse(sceneLabel.Name) );			
			mainForm.MyConnect.LightControlDebug(tempData, LCSendCompleted, LCSendError);
		}

		/// <summary>
		///  辅助回调方法：灯控debug(实时调试的数据)发送成功
		/// </summary>
		/// <param name="obj"></param>
		public void LCSendCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				isDebuging = false;
				setNotice(StatusLabel.RIGHT, "已成功发送【" + selectedSceneName + "】的《灯控开关》调试数据。", false, true);
			});
		}

		/// <summary>
		///  辅助回调方法：灯控debug发送出错
		/// </summary>
		/// <param name="obj"></param>
		public void LCSendError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				isDebuging = false;
				setNotice(StatusLabel.RIGHT, "发送《灯控开关》调试数据失败，请重连设备后重试[" + msg + "]", false, true);
				reconnectDevice();  // LCSendError
			});
		}

		/// <summary>
		///  事件：点击《灯控配置-场景全开复选框》，则此场景的所有灯光都开起来或关闭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void sceneCBs_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox sceneCB = sender as CheckBox;
			int sceneIndex = int.Parse(sceneCB.Name);
			bool tempRelayOn = sceneCB.Checked;

			for (int relayIndex = 0; relayIndex < relayCount; relayIndex++)
			{
				lcEntity.SceneData[sceneIndex, relayIndex] = tempRelayOn;
				relayButtons[sceneIndex, relayIndex].ImageIndex = tempRelayOn ? 1 : 0;
			}
		}

		/// <summary>
		/// 事件：点击《开关按键(1-6)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void relayButtons_Click(object sender, EventArgs e)
		{
			if (lcEntity == null)
			{
				return;
			}

			Button relayButton = sender as Button;
			int sceneIndex = int.Parse(relayButton.Name.Split(',')[0]);
			int relayIndex = int.Parse(relayButton.Name.Split(',')[1]);
			bool tempRelayOn = !lcEntity.SceneData[sceneIndex, relayIndex];

			for (int sceneIndex2 = 0; sceneIndex2 < sceneCount; sceneIndex2++)
			{
				// 两种情况要把相应的通道给亮上：1.点击按键；2.常亮模式
				if (sceneIndex == sceneIndex2 || keepLightOnCheckBox.Checked) 
				{
					lcEntity.SceneData[sceneIndex2, relayIndex] = tempRelayOn;
					relayButtons[sceneIndex2, relayIndex].ImageIndex = tempRelayOn ? 1 : 0;
				}
			}

			sceneLabels_Click(sceneLabels[sceneIndex], null);
		}

		/// <summary>
		///  事件：tgTrackBar滚轴值改变事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tgTrackBars_ValueChanged(object sender, EventArgs e)
		{
			TrackBar tgTrackBar = sender as TrackBar;
			int tgValue = tgTrackBar.Value;

			changeSCRValue(tgTrackBar.Name, tgValue);
		}

		/// <summary>
		/// 事件：tgNUDs值改动事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tgNUDs_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown tgNUD = sender as NumericUpDown;
			int tgValue = decimal.ToInt32(tgNUD.Value);

			changeSCRValue(tgNUD.Name, tgValue);
		}

		/// <summary>
		/// 把更改SCR值的操作，抽象到一起，避免重复代码
		/// </summary>
		/// <param name="sceneIndex"></param>
		/// <param name="tgIndex"></param>
		/// <param name="tgValue"></param>
		private void changeSCRValue(string controlName, int tgValue)
		{
			int sceneIndex, tgIndex;
			getIndex(controlName, out sceneIndex, out tgIndex);

			for (int sIndex = 0; sIndex < sceneCount; sIndex++)
			{
				if (keepLightOnCheckBox.Checked || sIndex == sceneIndex)
				{
					tgTrackBars[sIndex, tgIndex].ValueChanged -= tgTrackBars_ValueChanged;
					tgTrackBars[sIndex, tgIndex].Value = tgValue;
					tgTrackBars[sIndex, tgIndex].ValueChanged += tgTrackBars_ValueChanged;

					tgNUDs[sIndex, tgIndex].ValueChanged -= tgNUDs_ValueChanged;
					tgNUDs[sIndex, tgIndex].Value = tgValue;
					tgNUDs[sIndex, tgIndex].ValueChanged += tgNUDs_ValueChanged;

					lcEntity.LightControllerSCR.ScrData[sIndex, tgIndex] = tgValue;
				}
			}

			sceneLabels_Click(sceneLabels[sceneIndex], null);
		}

		/// <summary>
		/// 辅助方法：通过控件名，读取出相应的场景和通道的索引值，并使用【out关键字】来改变入参值；
		/// </summary>
		/// <param name="ctrlName"></param>
		/// <param name="sceneIndex"></param>
		/// <param name="tgIndex"></param>
		private void getIndex(string ctrlName, out int sceneIndex, out int tgIndex)
		{
			sceneIndex = int.Parse(ctrlName.Split(',')[0]);
			tgIndex = int.Parse(ctrlName.Split(',')[1]);
		}

		/// <summary>
		/// 事件：键盘按键Up事件（避免用户在更改数据后没有失去焦点或者按回车，此时实际的调光值 != 所见值）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tgNUDs_KeyUp(object sender, KeyEventArgs e)
		{
			NumericUpDown tgNUD = sender as NumericUpDown;
			tgNUD.Value = tgNUD.Value;  // 会触发tgNUDs_ValueChanged
		}

		/// <summary>
		/// 事件：勾选|取消勾选《启用调光》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tgCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (lcEntity == null || lcEntity.LightControllerSCR == null) {
				setNotice(StatusLabel.RIGHT, "启用调光出错，请确认当前固件是否支持调光通道。", true, true);
				return;				
			}

			// 确保存在SCR数据的情况下，才进行相应的显示；
			lcEntity.LightControllerSCR.IsStartSCR = tgCheckBox.Checked;
			for (int sIndex = 0; sIndex < sceneCount; sIndex++) {
				for (int tgIndex = 0; tgIndex < tgCount; tgIndex++) {
					tgPanels[sIndex, tgIndex].Visible = tgCheckBox.Checked;
				}				
			}			
		}

		#endregion

		#region 墙板相关

		/// <summary>
		/// 辅助回调方法： 连接墙板成功
		/// </summary>
		/// <param name="obj"></param>
		public void KPFirstConnectCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.RIGHT, "成功连接墙板(connStatus=kp)", false, true);
				setConnStatus(ConnectStatus.Kp);
				setBusy(false);

				//连接后休息片刻，就开始监听《墙板按键》的点击
				//Thread.Sleep(500);
				mainForm.MyConnect.PassThroughKeyPressSetClickListener(KPStartListenClick); 

				// 切换成功后，开启定时器让墙板自动更新（切换到其他的模式时，应将kpTimer停止或设为null）
				if (kpTimer == null)
				{
					kpTimer = new System.Timers.Timer(8000);
					kpTimer.Elapsed += new System.Timers.ElapsedEventHandler(kpOnTimer);
					kpTimer.AutoReset = true;
					kpTimer.Enabled = true;
				}

				// 若还未加载任何墙板值，则主动点击《读取码值》
				if (kpEntity == null) {
					kpReadButton_Click(null, null);
				}				
			});
		}

		/// <summary>
		/// 辅助回调方法：连接墙板失败
		/// </summary>
		public void KPConnectError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				// 切换失败，只给提示，不更改原来的状态
				setNotice(StatusLabel.RIGHT, LanguageHelper.TranslateSentence("连接墙板失败:") + msg, true, false);
				setBusy(false);
			});
		}

		/// <summary>
		/// 辅助方法：定时器定时执行的方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpOnTimer(object sender, ElapsedEventArgs e)
		{
			Invoke((EventHandler)delegate {
				if (mainForm.MyConnect != null && connStatus == ConnectStatus.Kp)
				{
					mainForm.MyConnect.PassThroughKeyPressConnect(KPTimerConnectCompleted, KPConnectError);
				}
			});
		}

		/// <summary>
		/// 辅助回调方法：定时器自动重连墙板的方法，此回调方法无需定义执行任何操作（代码中只有后台打印的代码，方便调试）
		/// </summary>
		/// <param name="obj"></param>
		public void KPTimerConnectCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				Console.WriteLine("Dickov：墙板定时重连成功...");
			});
		}
		
		/// <summary>
		/// 辅助方法：清空墙板listView的所有数据，及其他相关数据。
		/// </summary>
		private void clearKeypressListView()
		{
			keypressListView.Items.Clear();
			kpOrderTextBox.Text = "";
			kpKey0TextBox.Text = "";
			kpKey1TextBox.Text = "";
		}
			   
		/// <summary>
		/// 事件：点击《读取码值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpReadButton_Click(object sender, EventArgs e)
		{
			mainForm.MyConnect.PassThroughKeyPressRead(KPReadCompleted, KPReadError);
			setNotice(StatusLabel.RIGHT, "正在读取墙板码值，请稍候...", false, true);
			Cursor = Cursors.WaitCursor;
			Enabled = false;
		}

		/// <summary>
		///  辅助回调方法：读取墙板码值成功
		/// </summary>
		/// <param name="obj"></param>
		public void KPReadCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				if (obj == null)
				{
					setNotice(StatusLabel.RIGHT, "异常:执行kpReadCompleted时返回的对象为null", true, true);
					return;
				}

				kpEntity = obj as KeyEntity;
				reloadKeypressListView();
				setNotice(StatusLabel.RIGHT, "读取墙板码值成功。", true, true);
				refreshStatusButtons();
				this.Enabled = true;
				this.Cursor = Cursors.Default;
			});
		}

		/// <summary>
		/// 辅助方法：重新加载墙板码值，并把当下选中的协议渲染到ListView中；
		/// </summary>
		private void reloadKeypressListView()
		{
			keypressListView.Items.Clear();
			keypressListView.Enabled = true;
			string noKeyCode = "未设置键码值";
			for (int keyIndex = 0; keyIndex < 24; keyIndex++)
			{
				ListViewItem item = new ListViewItem("键序" + (keyIndex + 1).ToString() + "\n" + kpEntity.Key0Array[keyIndex] + ":" + kpEntity.Key1Array[keyIndex]);
				//item.ImageIndex = 2;

				item.SubItems.Add((keyIndex + 1).ToString());
				string key0 = kpEntity.Key0Array[keyIndex];
				item.SubItems.Add(key0);
				if (key0.Equals("00"))
				{
					item.SubItems.Add(noKeyCode);
				}
				else
				{
					item.SubItems.Add(ccEntity == null ? "" : ccEntity.CCDataList[Convert.ToInt32(key0, 16) - 1].Function);
				}
				//item.SubItems.Add(  ccEntity==null? "" :  ccEntity.CCDataList[Convert.ToInt32( key0, 16) - 1 ] .Function );
				string key1 = kpEntity.Key1Array[keyIndex];
				item.SubItems.Add(key1);
				if (key1.Equals("00"))
				{
					item.SubItems.Add(noKeyCode);
				}
				else
				{
					item.SubItems.Add(ccEntity == null ? "" : ccEntity.CCDataList[Convert.ToInt32(key1, 16) - 1].Function);
				}
				keypressListView.Items.Add(item);
			}
		}

		/// <summary>
		/// 辅助回调方法：读取墙板码值失败
		/// </summary>
		public void KPReadError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.RIGHT, LanguageHelper.TranslateSentence("读取墙板码值失败:") + msg, true, false);
				this.Enabled = true;
				this.Cursor = Cursors.Default;
			});
		}

		/// <summary>
		///  读取用户点击的墙板键值（键序+一种不需理会的编码）
		/// </summary>
		/// <param name="obj"></param>
		public void KPStartListenClick(Object obj)
		{
			Invoke((EventHandler)delegate
			{
				// 当keyEntity为null时，表示还未加载任何键盘键值数据，此方法不再执行。
				if (obj == null || kpEntity == null)
				{
					return;
				}

				List<byte> byteList = obj as List<byte>;
				int keyNum = byteList[0];

				//因为是多选，所以每次点击按键，先清空当前选择（否则会不停添加选中）
				keypressListView.SelectedItems.Clear();
				foreach (ListViewItem item in keypressListView.Items)
				{
					if (Convert.ToByte(item.SubItems[1].Text) == keyNum)
					{
						Console.WriteLine("Dickov:KeyPressNum: " + keyNum);
						item.Selected = true;
						break;
					}
				}
				keypressListView.Select();

				// 一旦收到这个回复，就重启定时器(避免不停重连)
				if (kpTimer != null)
				{
					kpTimer.Stop();
					kpTimer.Start();
				}
			});
		}
			
		/// <summary>
		/// 事件：点击《墙板-下载文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpDownloadButton_Click(object sender, EventArgs e)
		{
			mainForm.MyConnect.PassThroughKeyPressDownload(kpEntity, KeypressDownloadCompleted, KeypressDownladError);
		}

		/// <summary>
		/// 辅助回调方法：下载墙板成功
		/// </summary>
		/// <param name="obj"></param>
		private void KeypressDownloadCompleted(object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.RIGHT, "成功下载墙板码值", true, true);
			});
		}

		/// <summary>
		/// 若下载墙板失败，运行此回调方法
		/// </summary>
		private void KeypressDownladError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.RIGHT, LanguageHelper.TranslateSentence("下载墙板码值失败:") + msg, true, false);
			});
		}
			   
		/// <summary>
		/// 事件：选择不同的按键，可以修改其键值（）-》原版本无此功能。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void keypressListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 必须判断这个字段(Count)，否则会报异常
			if (keypressListView.SelectedIndices.Count > 0)
			{
				int keyIndex = keypressListView.SelectedIndices[0];
				kpOrderTextBox.Text = keypressListView.Items[keyIndex].SubItems[1].Text;
				kpKey0TextBox.Text = keypressListView.Items[keyIndex].SubItems[2].Text;
				kpKey1TextBox.Text = keypressListView.Items[keyIndex].SubItems[4].Text;
			}
		}

		/// <summary>
		/// 事件：点击《加载本地（墙板配置)文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpLoadButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == keyOpenDialog.ShowDialog()) {

				string keyPath = keyOpenDialog.FileName;

				IList<string> paramList = getParamListFromPath(keyPath);
				if (paramList == null || paramList.Count != 50)
				{
					setNotice(StatusLabel.RIGHT, "key文件有错误，无法加载。", true, true);
					return;
				}

				kpEntity = new KeyEntity();
				for (int i = 0; i < 24; i++)
				{
					kpEntity.Key0Array[i] = StringHelper.DecimalStringToBitHex(paramList[i], 2);
					kpEntity.Key1Array[i] = StringHelper.DecimalStringToBitHex(paramList[i + 24], 2);
				}
				kpEntity.CRC = paramList[48] + paramList[49];

				reloadKeypressListView();
				refreshStatusButtons();

				setNotice(StatusLabel.RIGHT,
					LanguageHelper.TranslateSentence("已加载本地墙板配置文件：") + keyPath,
					true, false);
			}
		}
	
		/// <summary>
		/// 事件：点击《保存(墙板配置)到本地》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpSaveButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == keySaveDialog.ShowDialog()) {
				string keyPath = keySaveDialog.FileName;
				kpEntity.WriteToFile(keyPath);
				setNotice(StatusLabel.RIGHT, LanguageHelper.TranslateSentence("成功保存墙板配置文件：") + keyPath, true, false);
			}
		}

		/// <summary>
		/// 事件：点击《搜索码值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpSearchButton_Click(object sender, EventArgs e)
		{
			if (ccEntity == null || string.IsNullOrEmpty(kpSearchTextBox.Text.Trim()))
			{
				setNotice(StatusLabel.RIGHT, "搜索码值时，协议必须已经加载且搜索关键字不得为空！", true, false);
				return;
			}

			// 由关键字搜索相应的indexList ，再遍历选中所有匹配项
			kpCodeListBox.Items.Clear();
			kpCodeList = new List<string>();

			IList<int> ccdIndexList = ccEntity.SearchIndices(kpSearchTextBox.Text.Trim());
			foreach (int ccdIndex in ccdIndexList)
			{
				kpCodeListBox.Items.Add(ccEntity.CCDataList[ccdIndex].Function + "[" + ccEntity.CCDataList[ccdIndex].Code + "]");
				kpCodeList.Add(ccEntity.CCDataList[ccdIndex].Code);
			}
		}

		/// <summary>
		/// 事件：点击《(填充文本到键码值输入框)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fillCodeButton_Click(object sender, EventArgs e)
		{
			if (kpCodeList == null || kpCodeList.Count == 0 || kpCodeListBox.SelectedIndex == -1)
			{
				setNotice(StatusLabel.RIGHT, "选中的码值为空，无法设为键码值，请重新搜索或选择", true, false);
				return;
			}

			Button btn = sender as Button;
			switch (btn.Name)
			{
				case "fillCode0Button":
					kpKey0TextBox.Text = kpCodeList[kpCodeListBox.SelectedIndex];
					break;
				case "fillCode1Button":
					kpKey1TextBox.Text = kpCodeList[kpCodeListBox.SelectedIndex];
					break;
				case "fillCodeAllButton":
					kpKey0TextBox.Text = kpCodeList[kpCodeListBox.SelectedIndex];
					kpKey1TextBox.Text = kpCodeList[kpCodeListBox.SelectedIndex];
					break;
			}
			setNotice(StatusLabel.RIGHT, "已将选中的键码值填入相应框中，确认修改可点击《修改键码值》。", false, true);
		}

		/// <summary>
		/// 事件：双击《kpCodeListBox》填入相应的键码值码值到两个文本框中；
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpCodeListBox_DoubleClick(object sender, EventArgs e)
		{
			kpKey0TextBox.Text = kpCodeList[kpCodeListBox.SelectedIndex];
			kpKey1TextBox.Text = kpCodeList[kpCodeListBox.SelectedIndex];
		}

		/// <summary>
		/// 事件：点击《修改键码值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpEditButton_Click(object sender, EventArgs e)
		{
			if (keypressListView.SelectedIndices.Count == 0)
			{
				setNotice(0, "请先选择需要设置键码值的按键。", true, true);
				return;
			}
			if (kpKey0TextBox.Text.Length == 0)
			{
				setNotice(0, "键码值0不得为空。", true, true);
				return;
			}

			// 处理TextBox内容，填入相应的listView中
			int keyIndex = keypressListView.SelectedIndices[0];
			string key0 = kpKey0TextBox.Text.ToLower().PadLeft(2, '0');
			keypressListView.Items[keyIndex].SubItems[2].Text = key0;
			keypressListView.Items[keyIndex].SubItems[3].Text = ccEntity == null ? "" : ccEntity.CCDataList[Convert.ToInt32(key0, 16) - 1].Function;

			if (kpKey1TextBox.Text.Length == 0)
			{
				keypressListView.Items[keyIndex].SubItems[4].Text = key0;
				keypressListView.Items[keyIndex].SubItems[5].Text = ccEntity == null ? "" : ccEntity.CCDataList[Convert.ToInt32(key0, 16) - 1].Function;
			}
			else
			{
				string key1 = kpKey1TextBox.Text.ToLower().PadLeft(2, '0');
				keypressListView.Items[keyIndex].SubItems[4].Text = key1;
				keypressListView.Items[keyIndex].SubItems[5].Text = ccEntity == null ? "" : ccEntity.CCDataList[Convert.ToInt32(key1, 16) - 1].Function;
			}

			// 将改变后的值填入keyEntity中
			int keyArrayIndex = Convert.ToInt32(kpOrderTextBox.Text) - 1; //keyEntity中的array索引号
			kpEntity.Key0Array[keyArrayIndex] = kpKey0TextBox.Text;
			kpEntity.Key1Array[keyArrayIndex] = kpKey1TextBox.Text;

		}

		#endregion

		#region 通用方法

		/// <summary>
		/// 重绘控件:让tabControl1的文本显示为横向的
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
		{
			Rectangle tabArea = tabControl1.GetTabRect(e.Index);//主要是做个转换来获得TAB项的RECTANGELF
			RectangleF tabTextArea = (RectangleF)(tabControl1.GetTabRect(e.Index));
			Graphics g = e.Graphics;
			StringFormat sf = new StringFormat();//封装文本布局信息
			sf.LineAlignment = StringAlignment.Center;
			sf.Alignment = StringAlignment.Center;
			Font font = this.tabControl1.Font;
			SolidBrush brush = new SolidBrush(Color.Black);//绘制边框的画笔
			g.DrawString(((TabControl)(sender)).TabPages[e.Index].Text, font, brush, tabTextArea, sf);
		}

		/// <summary>
		/// 辅助方法：通用的通知方法（这个Form比较复杂，因为Tab太多了）
		/// </summary>
		/// <param name="position">放到底部通知栏的哪一侧，1为左侧，2为右侧</param>
		/// <param name="msg"></param>
		/// <param name="isMsgShow"></param>
		/// <param name="isTranslate"></param>
		private void setNotice(StatusLabel position, string msg, bool isMsgShow, bool isTranslate)
		{
			if (isTranslate) { msg = LanguageHelper.TranslateSentence(msg); }
			if (isMsgShow) { MessageBox.Show(msg); }
			switch (position)
			{
				case StatusLabel.LEFT: leftStatusLabel.Text = msg; break;
				case StatusLabel.RIGHT: rightStatusLabel.Text = msg; break;			
			}
		}

		/// <summary>
		/// 辅助方法：设忙时或解除忙时
		/// </summary>
		/// <param name="busy"></param>
		private void setBusy(bool busy)
		{
			Enabled = !busy;
		}

		/// <summary>
		///  辅助方法：一些Control文本改变时，进行翻译
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someControl_TextChanged(object sender, EventArgs e)
		{
			LanguageHelper.TranslateControl(sender as Control);
		}

		/// <summary>
		/// 验证：对某些NumericUpDown进行鼠标滚轮的验证，避免一次性滚动过多
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someNUD_MouseWheel(object sender, MouseEventArgs e)
		{
			NumericUpDown nud = sender as NumericUpDown;
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = nud.Value + nud.Increment;
				if (dd <= nud.Maximum)
				{
					nud.Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = nud.Value - nud.Increment;
				if (dd >= nud.Minimum)
				{
					nud.Value = dd;
				}
			}
		}

		/// <summary>
		///  验证：对某些TrackBar进行鼠标滚轮的验证，避免一次性滚动过多（与OS设置有关）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someTrackBar_MouseWheel(object sender, MouseEventArgs e)
		{
			TrackBar tb = sender as TrackBar;
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true; //设为true则屏蔽之后系统自行处理的操作（就是原来加3(Win10)之类的操作）
			}
			// 向上滚
			if (e.Delta > 0)
			{
				int dd = tb.Value + tb.SmallChange;
				if (dd <= tb.Maximum)
				{
					tb.Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				int dd = tb.Value - tb.SmallChange;
				if (dd >= tb.Minimum)
				{
					tb.Value = dd;
				}
			}
		}

		#endregion

      
    }
}
