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
			No,	Normal,Lc,Cc,	Kp
		}

		enum StatusLabel
		{
			LEFT, RIGHT
		}

		private MainFormBase mainForm;
		private ConnectStatus connStatus = ConnectStatus.No;   //初始状态为未连接
				
		private IList<string> sceneCodeList ;		
		private string protocolXlsPath = Application.StartupPath + @"\Protocol\Controller.xls"; //默认的中控配置文件路径
		private HSSFWorkbook xlsWorkbook;  // 通过本对象实现相应的xls文件的映射

		private CCEntity ccEntity; // 中控封装对象
		private const int END_DECODING_TIME = 200; // 关闭中控解码需要一定的时间，才能往下操作；正常情况下200毫秒应该足够，但应设为可调节的		
		private bool isDecoding = false; //中控是否开启解码

		private LightControlData lcEntity; //灯控封装对象		
		private Button[] relayButtons;  // 动态添加的按钮组（灯控各个开关）	
		private int relayCount = 6; // 开关的数量		 		
			
		private KeyEntity kpEntity;  // 墙板封装对象
		private List<string> kpCodeList;   // 记录搜索到的码值列表（不用Dictionary，因为没有存储功能描述的必要，且Dictionary无序）
		private bool isKpShowDetails = true; // 墙板是否显示列表				
		private System.Timers.Timer kpTimer; //墙板定时刷新的定时器（因为透传模式，若太久（10s）没有连接，则会自动退出透传模式）
		
		public ToolsForm(MainFormBase mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;

			#region 初始化各组件		

			sceneCodeList = TextHelper.Read(Application.StartupPath + @"\Protocol\SceneCode");
			pbinSaveDialog.InitialDirectory = Application.StartupPath + @"\protocol";
			// 初始化灯控（强电）各配置
			sceneComboBox.SelectedIndex = 0;

			// 各强电开关
			relayButtons = new SkinButton[relayCount];
			for (int relayIndex = 0; relayIndex < relayCount; relayIndex++)
			{
				relayButtons[relayIndex] = new SkinButton
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
					Name = "switchButtons" + (relayIndex + 1),
					Text = LanguageHelper.TranslateWord("开关") + (relayIndex + 1)
				};
				relayButtons[relayIndex].Click += relayButtons_Click;

				relayFLP.Controls.Add(relayButtons[relayIndex]);
			}

			myToolTip.SetToolTip(keepLightOnCheckBox, "选中常亮模式后，手动点亮或关闭每一个灯光通道，\n都会点亮或关闭所有场景的该灯光通道。");
			myToolTip.SetToolTip(fillCodeAllButton, "点击此按键会将选中项的键码值填入左侧两个文本框中;\n双击右边列表的键码值也可实现同样效果。");

			#endregion
		}
		
		private void ToolsFormcs_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			//LanguageHelper.InitForm(this);
			//LanguageHelper.TranslateListView(protocolListView);
			//LanguageHelper.TranslateListView(keypressListView);		

			loadProtocols(Properties.Settings.Default.protocolIndex); //主动加载协议,并选择注册表中记录的选项			
		}

		private void ToolsForm_Shown(object sender, EventArgs e)
		{			
			tabControl1_SelectedIndexChanged(null, null); // 主动触发连接外设的操作
		}

		/// <summary>
		///  辅助方法：加载所有protocol文件，包括xls内的和用户另存为的；并选中入参index的协议
		/// </summary>
		private void loadProtocols(int selectedProtocolIndex)
		{
			try
			{
				protocolComboBox.Items.Clear();
				protocolComboBox.SelectedIndex = -1; //此处不触发保存协议选择的事件
				// 由xls文件加载协议列表；
				using (FileStream file = new FileStream(protocolXlsPath, FileMode.Open, FileAccess.Read))
				{
					xlsWorkbook = new HSSFWorkbook(file);
				}
				for (int protocolIndex = 0; protocolIndex < xlsWorkbook.NumberOfSheets; protocolIndex++)
				{
					ISheet sheet = xlsWorkbook.GetSheetAt(protocolIndex);					
					protocolComboBox.Items.Add(sheet.SheetName);
				}

				// 加载所有pbin文件；
				FileInfo[] pbinArray = new DirectoryInfo(Application.StartupPath + @"\Protocol\").GetFiles("*.pbin");
				if (pbinArray.Length > 0) {
					protocolComboBox.Items.Add("================");
					foreach (FileInfo pbin in pbinArray)
					{
						protocolComboBox.Items.Add(pbin.Name.Substring(0, pbin.Name.LastIndexOf(".pbin")));
					}
				}

				// 主动选中传入的协议index
				if(protocolComboBox.Items.Count > selectedProtocolIndex)
				{
					protocolComboBox.SelectedIndex = selectedProtocolIndex;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 事件：更改协议ComboBox的选项，先渲染后保存到注册表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void protocolComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 一进来先把ccEntity置空
			ccEntity = null;
			string protocolName = "";
			if (protocolComboBox.SelectedIndex == -1 || protocolComboBox.SelectedIndex == xlsWorkbook.NumberOfSheets)
			{
				setNotice(StatusLabel.RIGHT, "请选择可用协议（不要选择分隔符========）。",false, true);
				return;
			}
			
			// 选中xls中协议
			if (protocolComboBox.SelectedIndex < xlsWorkbook.NumberOfSheets)
			{
				ccEntity = new CCEntity();
				ISheet sheet = xlsWorkbook.GetSheetAt(protocolComboBox.SelectedIndex);
				ccEntity.ProtocolName = sheet.SheetName;
				System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
				// 处理通用数据(com0,com1,ps2)
				rows.MoveNext();
				IRow row = (HSSFRow)rows.Current;
				ICell cell = row.GetCell(0);
				ccEntity.Com0 = Convert.ToInt32(cell.ToString().Substring(4));
				rows.MoveNext();
				row = (HSSFRow)rows.Current;
				cell = row.GetCell(0);
				ccEntity.Com1 = Convert.ToInt32(cell.ToString().Substring(4));
				rows.MoveNext();
				row = (HSSFRow)rows.Current;
				cell = row.GetCell(0);
				ccEntity.PS2 = cell.ToString().Equals("PS2=主") ? 0 : 1;
				rows.MoveNext();

				//逐一处理每一行的数据
				int rowIndex = 0;
				while (rows.MoveNext())
				{
					row = (HSSFRow)rows.Current;

					CCData ccData = new CCData();
					cell = row.GetCell(0);
					ccData.Function = (cell == null ? "" : cell.ToString().Trim());
					cell = row.GetCell(1);
					ccData.Code = (cell == null ? "" : cell.ToString().Trim());
					cell = row.GetCell(2);
					ccData.Com0Up = (cell == null ? "" : cell.ToString().Trim());
					cell = row.GetCell(3);
					ccData.Com0Down = (cell == null ? "" : cell.ToString().Trim());
					cell = row.GetCell(4);
					ccData.Com1Up = (cell == null ? "" : cell.ToString().Trim());
					cell = row.GetCell(5);
					ccData.Com1Down = (cell == null ? "" : cell.ToString().Trim());
					cell = row.GetCell(6);
					ccData.InfraredSend = (cell == null ? "" : cell.ToString().Trim());
					cell = row.GetCell(7);
					ccData.InfraredReceive = (cell == null ? "" : cell.ToString().Trim());
					cell = row.GetCell(8);
					ccData.PS2Up = (cell == null ? "" : cell.ToString().Trim());
					cell = row.GetCell(9);
					ccData.PS2Down = (cell == null ? "" : cell.ToString().Trim());

					ccEntity.CCDataList.Add(ccData);
					rowIndex++;
				}
				protocolName = "excel表格中的【"+ protocolComboBox.Text+"】协议";
			}
			// 选中本地协议
			else
			{
				try
				{
					ccEntity = (CCEntity)SerializeUtils.DeserializeToObject(Application.StartupPath + @"\protocol\" + protocolComboBox.Text + ".pbin");
					protocolName = "用户另存的【" + protocolComboBox.Text + "】协议";
				}
				catch (Exception)
				{
					ccEntity = null;
					setNotice(StatusLabel.RIGHT, "用户另存的【" + protocolComboBox.Text + "】协议损坏，无法生成CC，请重选协议。", true, true);					
				}
			}

			if (ccEntity != null)
			{
				com0Label.Text = LanguageHelper.TranslateWord("串口0 = ") + ccEntity.Com0;
				com1Label.Text = LanguageHelper.TranslateWord("串口1 = ") + ccEntity.Com1;
				PS2Label.Text = "PS2 = " + (ccEntity.PS2 == 0?"主":"从");

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
				setNotice(StatusLabel.RIGHT,"已加载" + protocolName,false,true);

				// MARK3 0420 如果选择了一个可以用的cc，则保存到注册表
				Properties.Settings.Default.protocolIndex = protocolComboBox.SelectedIndex;
				Properties.Settings.Default.Save();

				int lcSceneIndex = sceneComboBox.SelectedIndex ;		// 先记录当前灯控选中的场景index，在渲染后重新选择				
				if (sceneCodeList != null && sceneCodeList.Count == 16)
				{
					sceneComboBox.Items.Clear();
					sceneComboBox.Items.Add("开机场景");
					
					for (int codeIndex = 0; codeIndex < 16; codeIndex++)
					{						
						sceneComboBox.Items.Add(ccEntity.CCDataList[Convert.ToInt32(sceneCodeList[codeIndex], 16) - 1].Function);
					}
					sceneComboBox.SelectedIndex = lcSceneIndex;
				}
			}
			refreshButtons();
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
			if (mainForm.IsConnected)
			{
				setBusy(true);
				mainForm.SleepBetweenSend(1);
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
			refreshButtons();
		}

		/// <summary>
		/// 刷新所有被connStatus影响的按键
		/// </summary>
		private void refreshButtons()
		{
			// 灯控相关按键
			lcReadButton.Enabled = connStatus == ConnectStatus.Lc;
			lcDownloadButton.Enabled = connStatus == ConnectStatus.Lc && lcEntity != null;			
			lcSaveButton.Enabled = lcEntity != null;

			// 中控相关			
			ccDownloadButton.Enabled = connStatus == ConnectStatus.Cc && ccEntity != null && !isDecoding;

			// 墙板相关按键
			kpReadButton.Enabled = connStatus == ConnectStatus.Kp;
			kpListenButton.Enabled = connStatus == ConnectStatus.Kp;
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
			if (mainForm.IsConnected)
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
				setNotice(StatusLabel.RIGHT, LanguageHelper.TranslateSentence("切换中控配置失败:") + msg, true, false);
				setBusy(false);
			});
		}
		
		/// <summary>
		/// 事件：点击《下载协议》
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
				reconnectDevice();
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

			int relayIndex = MathHelper.GetIndexNum(((Button)sender).Name, -1);
			setLightButtonValue(relayIndex);
			//若勾选常亮模式，则需要主动把所有场景的选中灯光亮暗设为一致。
			if (keepLightOnCheckBox.Checked)
			{
				bool tempLightOnMode = lcEntity.SceneData[sceneComboBox.SelectedIndex, relayIndex];
				for (int frameIndex = 0; frameIndex < 17; frameIndex++)
				{
					lcEntity.SceneData[frameIndex, relayIndex] = tempLightOnMode;
				}
			}
			debugLC();
		}

		/// <summary>
		///  辅助方法：在点击《开关按键》时，更改相关的lcEntity内的数据；
		/// </summary>
		/// <param name="relayIndex"></param>
		private void setLightButtonValue(int relayIndex)
		{
			if (lcEntity == null)
			{
				return;
			}
			lcEntity.SceneData[sceneComboBox.SelectedIndex, relayIndex] = !lcEntity.SceneData[sceneComboBox.SelectedIndex, relayIndex];
			relayButtons[relayIndex].ImageIndex = lcEntity.SceneData[sceneComboBox.SelectedIndex, relayIndex] ? 1 : 0;
		}

		/// <summary>
		/// 辅助方法：向设备发送当前场景的灯光通道数据。
		/// </summary>
		private void debugLC()
		{
			if (connStatus != ConnectStatus.Lc)
			{
				return;
			}
			setNotice(StatusLabel.RIGHT, "正在发送《灯控开关》调试数据..." ,false,true);
			Refresh();
			byte[] tempData = lcEntity.GetFrameBytes(sceneComboBox.SelectedIndex);
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
				setNotice( StatusLabel.RIGHT, "已成功发送《灯控开关》调试数据。", false,true);
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
				setNotice(StatusLabel.RIGHT, "发送《灯控开关》调试数据失败，请重连设备后重试[" + msg + "]",false,true);
			});
		}

		/// <summary>
		/// 事件：更改了场景之后，重新填充灯光通道数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void qdFrameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			reloadLightGroupBox();
		}

		/// <summary>
		/// 事件：点击《(灯控)下载配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcDownloadButton_Click(object sender, EventArgs e)
		{
			setNotice(StatusLabel.RIGHT, "正在下载灯控配置，请稍候...", false, true);
			processLC();
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
				for (int scene = 0; scene < 17; scene++)
				{
					for (int relayIndex = 6; relayIndex < 12; relayIndex++)
					{
						lcEntity.SceneData[scene, lcEntity.FanChannel] = false;
					}
				}
			}
		}

		/// <summary>
		/// 事件：点击《(灯控)保存配置》
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
						string pbinPath = pbinSaveDialog.FileName;
						SerializeUtils.SerializeObject(pbinPath, ccEntity);
						setNotice(StatusLabel.RIGHT, "成功另存协议。", true, true);						
					}
					catch (Exception ex)
					{
						setNotice(StatusLabel.RIGHT, "另存协议失败：" + ex.Message, true, false);
					}
				}
			}
		}

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
				//mainForm.SleepBetweenSend(1); // 回读灯控配置无需延时
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
				lightGroupBox.Enabled = false;
				refreshButtons();
				setNotice(0, "lcEntity==null。", true, false);
				return;
			}

			try
			{				
				lightGroupBox.Enabled = lcEntity.RelayCount != 0;				
				reloadLightGroupBox();
				refreshButtons();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		/// <summary>
		/// 辅助方法：当场景编号改变时，动态加载内存中的lcData.SceneData[selectedFrameIndex]；
		/// </summary>
		private void reloadLightGroupBox()
		{
			if (lcEntity == null)
			{
				return;
			}

			for (int relayIndex = 0; relayIndex < relayCount; relayIndex++)
			{
				relayButtons[relayIndex].ImageIndex = lcEntity.SceneData[sceneComboBox.SelectedIndex, relayIndex] ? 1 : 0;
			}
			debugLC();
		}				

		/// <summary>
		/// 辅助回调方法：灯控连接成功
		/// </summary>
		/// <param name="obj"></param>
		public void LCConnectCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {
				setConnStatus(ConnectStatus.Lc);
				setNotice(StatusLabel.RIGHT, "已切换成中控配置(connStatus=lc)", false,true);
				setBusy(false);

				// 当还没有任何形式地加载lcEntity时，主动从机器回读
				if (lcEntity == null) 	lcReadButton_Click(null, null);				
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
				setNotice(StatusLabel.RIGHT, LanguageHelper.TranslateSentence("切换灯控配置失败:") + msg, true, false);
				setBusy(false);
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
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控配置下载成功
		/// </summary>
		/// <param name="obj"></param>
		public void LCDownloadCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {				
				setNotice(StatusLabel.RIGHT, "灯控配置下载成功,请等待设备重启(约耗时5s)，重新搜索并连接设备。", true, true);
				reconnectDevice();
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控配置下载错误
		/// </summary>
		public void LCDownloadError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.RIGHT, LanguageHelper.TranslateSentence("灯控配置下载失败:") + msg, true, false);
			});
		}

		#endregion

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

				Thread.Sleep(500);
				kpListenButton_Click(null, null);
								
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
		/// 事件：点击《监听按键》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpListenButton_Click(object sender, EventArgs e)
		{
			mainForm.MyConnect.PassThroughKeyPressSetClickListener(KPStartListenClick);
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
				refreshButtons();
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
			for (int keyIndex = 0; keyIndex < 24; keyIndex++)
			{
				if (kpEntity.Key0Array[keyIndex].Equals("00") && kpEntity.Key1Array[keyIndex].Equals("00")) 
				{
					continue;
				}
				ListViewItem item = new ListViewItem("键序" + (keyIndex + 1).ToString() + "\n" + kpEntity.Key0Array[keyIndex] + ":" + kpEntity.Key1Array[keyIndex]);	
				//item.ImageIndex = 2;

				item.SubItems.Add((keyIndex + 1).ToString());
				string key0 = kpEntity.Key0Array[keyIndex] ;
				item.SubItems.Add(key0);
				item.SubItems.Add(  ccEntity==null? "" :  ccEntity.CCDataList[Convert.ToInt32( key0, 16) - 1 ] .Function );
				string key1 = kpEntity.Key1Array[keyIndex];
				item.SubItems.Add(key1);
				item.SubItems.Add(ccEntity == null ? "" : ccEntity.CCDataList[Convert.ToInt32(key1, 16) - 1].Function);
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
		/// 事件：点击《加载墙板cfg文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpLoadButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == keyOpenFileDialog.ShowDialog()) {

				string keyPath = keyOpenFileDialog.FileName;

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
				refreshButtons();

				setNotice(StatusLabel.RIGHT,
					LanguageHelper.TranslateSentence("已加载本地墙板配置文件：") + keyPath,
					true, false);
			}
		}
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpSaveButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == keySaveFileDialog.ShowDialog()) {
				string keyPath = keySaveFileDialog.FileName;
				kpEntity.WriteToFile(keyPath);
				setNotice(StatusLabel.RIGHT, LanguageHelper.TranslateSentence("成功保存墙板配置文件：") + keyPath, true, false);
			}
		}

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


		#endregion



		/// <summary>
		/// 事件：点击《搜索码值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpSearchButton_Click(object sender, EventArgs e)
		{
			if (ccEntity == null || string.IsNullOrEmpty(kpSearchTextBox.Text.Trim() )) {
				setNotice(StatusLabel.RIGHT, "搜索码值时，协议必须已经加载且搜索关键字不得为空！", true, false);
				return;
			}

			// 由关键字搜索相应的indexList ，再遍历选中所有匹配项
			kpCodeListBox.Items.Clear();
			kpCodeList = new List<string>(); 

			IList<int> ccdIndexList = ccEntity.SearchIndices(kpSearchTextBox.Text.Trim());
			foreach (int ccdIndex in ccdIndexList)
			{
				kpCodeListBox.Items.Add(  ccEntity.CCDataList[ccdIndex].Function+"["+ ccEntity.CCDataList[ccdIndex].Code + "]" );
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
			if (kpCodeList==null || kpCodeList.Count == 0  || kpCodeListBox.SelectedIndex == -1 ) {
				setNotice(StatusLabel.RIGHT, "选中的码值为空，无法设为键码值，请重新搜索或选择", true, false);
				return;
			}			

			Button btn = sender as Button;
			switch (btn.Name) {
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
				keypressListView.Items[keyIndex].SubItems[5].Text = ccEntity==null ? "" : ccEntity.CCDataList[Convert.ToInt32(key0, 16) - 1].Function ;
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


		/// <summary>
		/// 事件：点击《(灯控)打开配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcLoadButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == cfgOpenFileDialog.ShowDialog()){
				setNotice(StatusLabel.RIGHT, "正在打开本地灯控配置文件，请稍候...", false, true);
				IList<string> paramList = getParamListFromPath( cfgOpenFileDialog.FileName);
				lcEntity = new LightControlData(paramList);
				lcRender();
				setNotice(StatusLabel.RIGHT,
					LanguageHelper.TranslateSentence("已加载本地灯控配置文件：") + cfgOpenFileDialog.FileName,	
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
			if (DialogResult.OK == cfgSaveFileDialog.ShowDialog()) {
				processLC();
				lcEntity.WriteToFile(cfgSaveFileDialog.FileName);
				setNotice(StatusLabel.RIGHT,
					LanguageHelper.TranslateSentence("成功保存灯控配置文件为：") + cfgSaveFileDialog.FileName,
					true, false);
			}
		}
	}
}
