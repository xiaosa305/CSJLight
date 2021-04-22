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
			NO = 0, CC1, CC2, LC1, LC2, KP1, KP2, ALL1, ALL2
		}

		private MainFormBase mainForm;
		private ConnectStatus connStatus = ConnectStatus.No;   //初始状态为未连接

		
		private string protocolXlsPath = Application.StartupPath + @"\Protocol\Controller.xls"; //默认的中控配置文件路径
		private HSSFWorkbook xlsWorkbook;  // 通过本对象实现相应的xls文件的映射

		private CCEntity ccEntity; // 中控封装对象
		private const int END_DECODING_TIME = 200; // 关闭中控解码需要一定的时间，才能往下操作；正常情况下200毫秒应该足够，但应设为可调节的		
		private bool isDecoding = false; //中控是否开启解码

		private LightControlData lcEntity; //灯控封装对象		
		private Button[] relayButtons;  // 动态添加的按钮组（灯控各个开关）	
		private int relayCount = 6; // 开关的数量		 		
		private int lcFrameIndex = 0; // 灯控选中的场景，用以显示不同场景的灯光开启状态
		
		private KeyEntity keyEntity;  // 墙板封装对象
		private bool isKpShowDetails = true; // 墙板是否显示列表				
		private System.Timers.Timer kpTimer; //墙板定时刷新的定时器（因为透传模式，若太久（10s）没有连接，则会自动退出透传模式）
		
		public ToolsForm(MainFormBase mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;

			#region 初始化各组件		

			pbinSaveDialog.InitialDirectory = Application.StartupPath + @"\protocol";

			// 初始化灯控（强电）各配置
			qdFrameComboBox.SelectedIndex = 0;

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

			myInfoToolTip.SetToolTip(keepLightOnCheckBox, "选中常亮模式后，手动点亮或关闭每一个灯光通道，\n都会点亮或关闭所有场景的该灯光通道。");					

			#endregion
		}
		
		/// <summary>
		/// 事件：
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolsFormcs_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			LanguageHelper.InitForm(this);
			LanguageHelper.TranslateListView(protocolListView);
			LanguageHelper.TranslateListView(keypressListView);

			loadProtocols( Properties.Settings.Default.protocolIndex ); //主动加载协议,并选择注册表中记录的选项			
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
			if (protocolComboBox.SelectedIndex == -1 || protocolComboBox.SelectedIndex == xlsWorkbook.NumberOfSheets)
			{
				return;
			}

			CCEntity ccTemp= generateCC();
			if (ccTemp != null)
			{
				ccEntity = ccTemp;
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
				refreshButtons();

				// MARK3 0420 如果选择了一个可以用的cc，则保存到注册表
				Properties.Settings.Default.protocolIndex = protocolComboBox.SelectedIndex;
				Properties.Settings.Default.Save();
			}
			else
			{
				protocolComboBox.SelectedIndex = -1;
			}
		}
			
		/// <summary>
		/// 辅助方法：根据当前选中的协议生成CCEntity数据( 供下载数据及搜索数据使用）
		/// </summary>
		/// <returns></returns>
		private CCEntity generateCC()
		{
			if (protocolComboBox.SelectedIndex == -1 || protocolComboBox.SelectedIndex == xlsWorkbook.NumberOfSheets)
			{
				setNotice(StatusLabel.CC2, "未选择协议，无法生成CC", false, true);
				return null;
			}
						
			CCEntity cc = null;
			// 选中xls中协议
			if (protocolComboBox.SelectedIndex < xlsWorkbook.NumberOfSheets)
			{
				cc = new CCEntity();
				ISheet sheet = xlsWorkbook.GetSheetAt(protocolComboBox.SelectedIndex);
				cc.ProtocolName = sheet.SheetName;
				System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
				// 处理通用数据(com0,com1,ps2)
				rows.MoveNext();
				IRow row = (HSSFRow)rows.Current;
				ICell cell = row.GetCell(0);
				cc.Com0 = Convert.ToInt32(cell.ToString().Substring(4));
				rows.MoveNext();
				row = (HSSFRow)rows.Current;
				cell = row.GetCell(0);
				cc.Com1 = Convert.ToInt32(cell.ToString().Substring(4));
				rows.MoveNext();
				row = (HSSFRow)rows.Current;
				cell = row.GetCell(0);
				cc.PS2 = cell.ToString().Equals("PS2=主") ? 0 : 1;
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

					cc.CCDataList.Add(ccData);
					rowIndex++;
				}
			}
			// 选中本地协议
			else {				
				try
				{
					cc = (CCEntity)SerializeUtils.DeserializeToObject(Application.StartupPath + @"\protocol\" + protocolComboBox.Text + ".pbin");	
				}
				catch (Exception ex) {					
					setNotice(StatusLabel.CC2, "本地协议损坏，无法生成CC，请重选协议。", true, true);
				}				
			}
			
			return cc;
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
				case StatusLabel.CC1: ccToolStripStatusLabel1.Text = msg; break;
				case StatusLabel.CC2: ccToolStripStatusLabel2.Text = msg; break;
				case StatusLabel.LC1: lcToolStripStatusLabel1.Text = msg; break;
				case StatusLabel.LC2: lcToolStripStatusLabel2.Text = msg; break;
				case StatusLabel.KP1: kpToolStripStatusLabel1.Text = msg; break;
				case StatusLabel.KP2: kpToolStripStatusLabel2.Text = msg; break;
				case StatusLabel.ALL1: lcToolStripStatusLabel1.Text = msg; ccToolStripStatusLabel1.Text = msg; kpToolStripStatusLabel1.Text = msg; break;
				case StatusLabel.ALL2: lcToolStripStatusLabel2.Text = msg; ccToolStripStatusLabel2.Text = msg; kpToolStripStatusLabel2.Text = msg; break;
			}
		}

		/// <summary>
		/// 辅助方法：设忙时或解除忙时
		/// </summary>
		/// <param name="busy"></param>
		private void setBusy(bool busy) {
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
						mainForm.MyConnect.CenterControlConnect(CCConnectCompleted, CCConnectError);
						break;
					case 1:
						mainForm.MyConnect.LightControlConnect(LCConnectCompleted, LCConnectError);
						break;
					case 2:						
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
				case ConnectStatus.No: setNotice(StatusLabel.ALL1, "尚未连接设备", false, true); break;
				case ConnectStatus.Normal: setNotice(StatusLabel.ALL1, "已连接设备", false, true); break;
				case ConnectStatus.Lc: setNotice(StatusLabel.ALL2, "已切换为灯控配置", false, true); break;
				case ConnectStatus.Cc: setNotice(StatusLabel.ALL2, "已切换为中控模式", false, true); break;
				case ConnectStatus.Kp: setNotice(StatusLabel.ALL2, "已切换为墙板配置", false, true); break;
				default: setNotice(StatusLabel.ALL1, "", false, false); setNotice(StatusLabel.ALL2, "", false, false); break;
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
			bool keNotNull = keyEntity != null;
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

			setNotice(StatusLabel.CC2, "成功修改协议(临时)，如需长期修改请另存协议。", false, false);
		}
		
		/// <summary>
		/// 辅助回调方法：中控连接成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCConnectCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice(StatusLabel.CC2, "已切换成中控配置(connStatus=cc)", false, true);
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
				setNotice(StatusLabel.CC2, LanguageHelper.TranslateSentence("切换中控配置失败:") + msg, true, false);
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
				setNotice(StatusLabel.CC2, "当前协议为空，请先选择协议。", true, true);
				return;
			}

			// 正常情况下，在解码模式下不能下载数据，加这个判断以防万一
			if (isDecoding)
			{
				setNotice(StatusLabel.CC2, "在解码状态下无法下载协议，请先关闭解码。", true, true);
				return;
			}

			setNotice(StatusLabel.CC2, "正在下载中控协议到设备，请稍候...", false, true);
			mainForm.MyConnect.CenterControlDownload(ccEntity, CCDownloadCompleted, CCDownloadError);
		}

		/// <summary>
		/// 辅助回调方法：中控配置下载成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCDownloadCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice(StatusLabel.CC2, "中控配置下载成功,请等待设备重启(约耗时5s)，重新搜索并连接设备。", true, true);
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
				setNotice(StatusLabel.CC2, LanguageHelper.TranslateSentence("中控配置下载失败：") + msg, true, false);
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
				bool tempLightOnMode = lcEntity.SceneData[lcFrameIndex, relayIndex];
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
			lcEntity.SceneData[lcFrameIndex, relayIndex] = !lcEntity.SceneData[lcFrameIndex, relayIndex];
			relayButtons[relayIndex].ImageIndex = lcEntity.SceneData[lcFrameIndex, relayIndex] ? 1 : 0;
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
			byte[] tempData = lcEntity.GetFrameBytes(lcFrameIndex);
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
				Console.WriteLine("灯控debug(实时调试的数据)发送成功");
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
				lcToolStripStatusLabel2.Text = "灯控已离线，发送debug数据失败，请重新连接后重试[" + msg + "]";
			});
		}

		/// <summary>
		/// 事件：更改了场景之后，重新填充灯光通道数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void qdFrameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			lcFrameIndex = qdFrameComboBox.SelectedIndex;
			reloadLightGroupBox();
		}

		/// <summary>
		/// 事件：点击《(灯控)下载配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lcDownloadButton_Click(object sender, EventArgs e)
		{
			setNotice(StatusLabel.LC2, "正在下载灯控配置，请稍候...", false, true);
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
						setNotice(StatusLabel.CC2, "成功另存协议。", true, true);
						loadProtocols(-1);
					}
					catch (Exception ex)
					{
						setNotice(StatusLabel.CC2, "另存协议失败:" + ex.Message, true, false);
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
				lcToolStripStatusLabel2.Text = "正在回读灯控配置，请稍候...";
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
				relayButtons[relayIndex].ImageIndex = lcEntity.SceneData[lcFrameIndex, relayIndex] ? 1 : 0;
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
				setNotice(StatusLabel.LC2, "已切换成中控配置(connStatus=lc)", false,true);
				setBusy(false);
				lcReadButton_Click(null, null);				
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
				setNotice(StatusLabel.LC2, LanguageHelper.TranslateSentence("切换灯控配置失败:") + msg, true, false);
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
					setNotice(StatusLabel.LC2, "灯控回读配置异常(lcDataTemp==null)", true, true);
					setBusy(false);
					return;
				}
				lcEntity = lcDataTemp as LightControlData;
				lcRender();
				setNotice(StatusLabel.LC2, "成功回读灯控配置", true, true);
				setBusy(false);
			});
		}

		/// <summary>
		/// 辅助回调方法：灯控配置回读失败
		/// </summary>
		public void LCReadError(string msg)
		{
			Invoke((EventHandler)delegate {
				setNotice(StatusLabel.LC2, LanguageHelper.TranslateSentence("回读灯控配置失败:") + msg, true, false);
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
				setNotice(StatusLabel.LC2, "灯控配置下载成功,请等待设备重启(约耗时5s)，重新搜索并连接设备。", true, true);
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
				setNotice(StatusLabel.LC2, LanguageHelper.TranslateSentence("灯控配置下载失败:") + msg, true, false);
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
				setNotice(StatusLabel.KP2, "成功连接墙板(connStatus=kp)", false, true);
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
				setNotice(StatusLabel.KP2, LanguageHelper.TranslateSentence("连接墙板失败:") + msg, true, false);
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
			setNotice(StatusLabel.KP2, "正在读取墙板码值，请稍候...", false, true);
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
					setNotice(StatusLabel.KP2, "异常:执行kpReadCompleted时返回的对象为null", true, true);
					return;
				}

				keyEntity = obj as KeyEntity;
				reloadKeypressListView();
				setNotice(StatusLabel.KP2, "读取墙板码值成功。", true, true);
				refreshButtons();
				this.Enabled = true;
				this.Cursor = Cursors.Default;
			});
		}

		/// <summary>
		/// 辅助方法：重新加载墙板码值
		/// </summary>
		private void reloadKeypressListView()
		{
			keypressListView.Items.Clear();
			keypressListView.Enabled = true;
			for (int keyIndex = 0; keyIndex < 24; keyIndex++)
			{
				if (keyEntity.Key0Array[keyIndex].Equals("00") && keyEntity.Key1Array[keyIndex].Equals("00"))
				{
					continue;
				}
				ListViewItem item = new ListViewItem("键序" + (keyIndex + 1).ToString() + "\n" + keyEntity.Key0Array[keyIndex] + ":" + keyEntity.Key1Array[keyIndex]);
				item.ImageIndex = 2;
				item.SubItems.Add((keyIndex + 1).ToString());
				item.SubItems.Add(keyEntity.Key0Array[keyIndex]);
				item.SubItems.Add(keyEntity.Key1Array[keyIndex]);
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
				setNotice(StatusLabel.KP2, LanguageHelper.TranslateSentence("读取墙板码值失败:") + msg, true, false);
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
				if (obj == null || keyEntity == null)
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
			mainForm.MyConnect.PassThroughKeyPressDownload(keyEntity, KeypressDownloadCompleted, KeypressDownladError);
		}

		/// <summary>
		/// 辅助回调方法：下载墙板成功
		/// </summary>
		/// <param name="obj"></param>
		private void KeypressDownloadCompleted(object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.KP2, "成功下载墙板码值", true, true);
			});
		}

		/// <summary>
		/// 若下载墙板失败，运行此回调方法
		/// </summary>
		private void KeypressDownladError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(StatusLabel.KP2, LanguageHelper.TranslateSentence("下载墙板码值失败:") + msg, true, false);
			});
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
					setNotice(StatusLabel.KP2, "key文件有错误，无法加载。", true, true);
					return;
				}

				keyEntity = new KeyEntity();
				for (int i = 0; i < 24; i++)
				{
					keyEntity.Key0Array[i] = StringHelper.DecimalStringToBitHex(paramList[i], 2);
					keyEntity.Key1Array[i] = StringHelper.DecimalStringToBitHex(paramList[i + 24], 2);
				}
				keyEntity.CRC = paramList[48] + paramList[49];

				reloadKeypressListView();
				refreshButtons();

				setNotice(StatusLabel.KP2,
					LanguageHelper.TranslateSentence("已加载墙板配置文件:") + keyPath,
					false, false);
			}
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
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void kpSaveButton_Click(object sender, EventArgs e)
		{
			keySaveFileDialog.ShowDialog();
		}

		
	}
}
