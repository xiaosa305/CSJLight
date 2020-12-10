using LighEditor.Tools;
using LightController.Common;
using LightController.MyForm;
using LightEditor.Ast;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightEditor
{
	public partial class NewLightEditorForm : Form
	{
		private MainFormBase mainForm;	

		private string softwareName;  //动态更改软件名
		private string picDirectory;     // 图片目录
		private string lightDirectory;   // ini保存目录

		private bool isNew = true;  //是否新建

		// 与当前灯具相关的变量，最后会进行存储		
		private List<TongdaoWrapper> tongdaoList;
		private int tongdaoCount = 0;
		private SAWrapper[] sawArray;
		private Dictionary<int, FlowLayoutPanel> saDict;

		// 各通道值存储的数组
		private Panel[] tdPanels = new Panel[32];
		private TextBox[] tdTextBoxes = new TextBox[32];
		private Label[] tdLabels = new Label[32];
		private TrackBar[] tdTrackBars = new TrackBar[32];
		private NumericUpDown[] tdNUDs = new NumericUpDown[32];

		//调试相关变量
		private OneLightOneStep player; // 调试工具类的实例
		private int firstTDValue = 1;  // 初始通道地址值：最小为1,最大为512		
		private bool isConnect = false; // 辅助变量：是否连接设备
				
		// 原WaySetForm辅助变量
		private TextBox selectedTextBox = null; //辅助变量，用来记录鼠标选择的textBox
		private int selectedTdIndex = -1 ;

		public NewLightEditorForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;
			InitializeComponent();

			softwareName = mainForm.SoftwareName + " Light Editor";
			Text = softwareName ;
			
			picDirectory = mainForm.SavePath + @"\LightPic";
			openImageDialog.InitialDirectory = picDirectory; //图片加载路径使用当前软件所在文件夹
			lightDirectory = mainForm.SavePath + @"\LightLibrary";
			openFileDialog.InitialDirectory = lightDirectory;  //灯具目录				

			// 动态添加通道预选名称
			IList<string> tdNameList = TextHelper.Read(Application.StartupPath + @"\PreTDNameList");
			foreach (string item in tdNameList)
			{
				this.nameListBox.Items.Add(item);
			}

			//初始化各个通道的控件
			for (int tdIndex = 0; tdIndex < 32; tdIndex++)
			{
				tdPanels[tdIndex] = new Panel
				{
					Name = "tdPanels" + (tdIndex +1),
					Size = tdPanelDemo.Size,
					Visible = false
				};

				tdTextBoxes[tdIndex] = new TextBox
				{
					Name = "tdTextBoxes" + (tdIndex + 1),
					Location = tdTextBoxDemo.Location,
					Size = tdTextBoxDemo.Size,
					TextAlign = tdTextBoxDemo.TextAlign
				};

				tdLabels[tdIndex] = new Label
				{
					Name = "tdLabels" + (tdIndex + 1),
					Font = tdLabelDemo.Font,
					Location = tdLabelDemo.Location,
					Size = tdLabelDemo.Size,
					Text = "通道" + (tdIndex + 1),
					TextAlign = tdLabelDemo.TextAlign,
					ForeColor = tdLabelDemo.ForeColor
				};

				tdTrackBars[tdIndex] = new TrackBar
				{
					Name = "tdTrackBars" + (tdIndex + 1),
					//AutoSize = false,
					BackColor = tdTrackBarDemo.BackColor,
					Location = tdTrackBarDemo.Location,
					Size = tdTrackBarDemo.Size,
					Orientation = tdTrackBarDemo.Orientation,
					TickStyle = tdTrackBarDemo.TickStyle,
					Maximum = tdTrackBarDemo.Maximum
				};

				tdNUDs[tdIndex] = new NumericUpDown
				{
					Name = "tdNUDs" + (tdIndex + 1),
					Location = tdNUDDemo.Location,
					Size = tdNUDDemo.Size,
					TextAlign = tdNUDDemo.TextAlign,
					Maximum = tdNUDDemo.Maximum,
					Minimum = tdNUDDemo.Minimum,
					Increment = tdNUDDemo.Increment
				};
				
				tdFlowLayoutPanel.Controls.Add( tdPanels[tdIndex] );
				tdPanels[tdIndex].Controls.Add(tdTextBoxes[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdLabels[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdTrackBars[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdNUDs[tdIndex]);	
				
				tdTextBoxes[tdIndex].MouseClick += tdTextBoxes_MouseClick;
				tdTextBoxes[tdIndex].LostFocus += tdTextBoxes_LostFocus;
				tdLabels[tdIndex].MouseEnter += tdLabels_MouseEnter;
				tdLabels[tdIndex].Click += tdLabels_Click;
				tdTrackBars[tdIndex].MouseEnter += tdTrackBars_MouseEnter;
				tdTrackBars[tdIndex].MouseWheel += tdTrackBars_MouseWheel;
				tdTrackBars[tdIndex].ValueChanged += tdTrackBars_ValueChanged;
				tdNUDs[tdIndex].MouseWheel += someNUD_MouseWheel;
				tdNUDs[tdIndex].ValueChanged += tdNUDs_ValueChanged;	

				countComboBox.Items.Add(tdIndex + 1);
			}

			countComboBox.SelectedIndex = 0;
			firstTDNumericUpDown.MouseWheel += someNUD_MouseWheel ;
			unifyValueNumericUpDown.MouseWheel += someNUD_MouseWheel;
			refreshComList();
		}	

		private void NewLightEditorForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 40, mainForm.Location.Y + 60);
		}
		
		/// <summary>
		/// 事件：关闭窗体时，若处于连接状态，则断开连接（=null可写可不写）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewLightEditorForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (isConnect)
			{
				player.CloseDevice();
				player = null;
			}

			Dispose();
			mainForm.Activate();
		}
		
		/// <summary>
		///  事件：点击《新建灯具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newLightButton_Click(object sender, EventArgs e)
		{
			if (RequestSaveLight("新建灯具前，是否保存当前灯具？"))
			{
				countComboBox.SelectedIndex = 0;
				tongdaoCount = 0;				
				tongdaoList = null;

				//子属性相关的先清理
				sawArray = null;
				saDict = null;
				clearTdRelated(true);

				nameTextBox.Text = "";
				typeTextBox.Text = "";
				picTextBox.Text = "";
				openPictureBox.Image = null;

				firstTDNumericUpDown.Value = 1;

				showTds();
				showAllPanels();				
				enableRename(true);
				checkGenerateEnable();
			}
		}
		
		/// <summary>
		///  事件：点击《打开灯具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openLightButton_Click(object sender, EventArgs e)
		{
			if (RequestSaveLight("打开灯具前，是否保存当前灯具？"))
			{
				openFileDialog.ShowDialog();
			}
		}

		/// <summary>
		///  事件：在《打开灯具》对话框内选择文件，并点击确认时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			string iniPath = openFileDialog.FileName;
			// 简单读取文本文件-->打开ini文件
			using (FileStream file = new FileStream(iniPath, FileMode.Open))
			using (StreamReader reader = new StreamReader(file, Encoding.UTF8))   // 可指定编码，默认的用Default，它会读取系统的编码（ANSI-->针对不同地区的系统使用不同编码，中文就是GBK）
			{
				string s = "";
				ArrayList lineList = new ArrayList();
				int lineCount = 0;
				while ((s = reader.ReadLine()) != null)
				{
					lineList.Add(s);
					lineCount++;
				}

				// 无论如何，灯具tongdaoList应该有至少一个值！
				if (lineCount < 5)
				{
					MessageBox.Show("打开的ini文件格式有误");
					return;
				}

				nameTextBox.Text = lineList[4].ToString().Substring(5);
				typeTextBox.Text = lineList[1].ToString().Substring(5);
				string imagePath = lineList[2].ToString().Substring(4);
				if (imagePath != null && !imagePath.Trim().Equals(""))
				{
					this.setImage(picDirectory + "\\" + imagePath);
				}

				tongdaoCount = int.Parse(lineList[3].ToString().Substring(6));//第七个字符开始截取			

				countComboBox.SelectedIndex = tongdaoCount - 1;   // 此处请注意：并不是用SelectedText，而是直接设Text			

				tongdaoList = new List<TongdaoWrapper>();
				for (int i = 0; i < tongdaoCount; i++)
				{
					string tongdaoName = lineList[3 * i + 6].ToString().Substring(4);
					int initValue = int.Parse(lineList[3 * i + 7].ToString().Substring(4));
					int address = int.Parse(lineList[3 * i + 8].ToString().Substring(4));
					tongdaoList.Add(new TongdaoWrapper()
					{
						TongdaoName = tongdaoName,
						InitValue = initValue,
						Address = address,
						CurrentValue = initValue
					});
				}

				try
				{
					sawArray = SAWrapper.GetSawArrayFromIni(iniPath);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}				
			}

			showTds();
			
			handleTongdaoCount(); 
			saDict = new Dictionary<int, FlowLayoutPanel>() ;
			checkGenerateEnable();

			showAllPanels();
			enableRename(false);
			refreshPlayGroupBox();
		}

		/// <summary>
		/// 辅助方法：验证《生成》按键是否可用（内存里的TongdaoCount和下拉框的数据是否相同）
		/// </summary>
		private void checkGenerateEnable()
		{			
			generateButton.Enabled = int.Parse(countComboBox.Text) != tongdaoCount;
		}

		/// <summary>
		/// 辅助方法：每次可能改动通道数量的操作后，要对初始的通道值进行限制（避免超过512）
		/// 主要使用情况为：1.更改通道数量（点击生成、及新建灯具）；2.更换《打开的灯具》
		/// </summary>
		private void handleTongdaoCount() 
		{
			firstTDNumericUpDown.Maximum = 513 - tongdaoCount ;
		}
		
		/// <summary>
		/// 辅助方法：不管什么情况（打开或新建灯具），都把隐藏的界面打开
		/// </summary>
		private void showAllPanels() {
			lightGroupBox.Show();			
			saFLPDemo.Show();
		}

		/// <summary>
		///  辅助方法：根据tongdaoList，来确定是否显示调试面板；
		/// </summary>
		private void refreshPlayGroupBox() {
			playGroupBox.Visible = tongdaoList != null && tongdaoList.Count > 0;
		}

		/// <summary>
		///  辅助方法：渲染并显示相应的通道列表
		///  1.将tongdaoList渲染进下拉条组中
		///  2.显示显示当前数量的下拉条，并将剩余的隐藏起来
		///  3.根据通道数，显示相应的GroupBox
		/// </summary> 
		private void showTds()
		{
			tdFlowLayoutPanel.Show();

			// 1.tongdaoList的数据渲染进各个通道显示项(label+valueLabel+vScrollBar)中, 并显示有数据的通道
			for (int tdIndex = 0; tdIndex < tongdaoCount; tdIndex++)
			{
				tdTextBoxes[tdIndex].Text =  tongdaoList[tdIndex].TongdaoName;
				tdLabels[tdIndex].Text = "通道" + (tdIndex + 1);
				tdTrackBars[tdIndex].Value = tongdaoList[tdIndex].CurrentValue;				
				tdNUDs[tdIndex].Value = tongdaoList[tdIndex].CurrentValue;

				string tdRemark = tongdaoList[tdIndex].TongdaoName;
				foreach (SA sa in sawArray[tdIndex].SaList)
				{
					tdRemark += "\n" + sa.SAName + "：" + sa.StartValue + " - " + sa.EndValue;
				}
				tdPanels[tdIndex].Show();
			}

			// 2.隐藏其余通道
			for (int tdIndex = tongdaoCount; tdIndex < 32; tdIndex++)
			{
				tdPanels[tdIndex].Hide();
			}
		}

		#region 灯具图片

		/// <summary>
		/// 事件：点击《灯具图片框》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openPictureBox_Click(object sender, EventArgs e)
		{
			openImageDialog.ShowDialog();
		}

		/// <summary>
		///  打开图片对话框，选择图片后的操作：调用相关方法，设置两个值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openImageDialog_FileOk(object sender, CancelEventArgs e)
		{
			string imagePath = openImageDialog.FileName;
			setImage(imagePath);
		}

		/// <summary>
		/// 辅助方法：通过图片路径，改变image相关的两个内容：①打开灯具②更改pictureBox内容后调用
		/// </summary>
		/// <param name="imagePathName"></param>
		private void setImage(string imagePath)
		{
			string shortFileName = imagePath.Substring(imagePath.LastIndexOf("\\") + 1);
			// 从本地目录加载图片			
			FileInfo imageFileInfo = new FileInfo(imagePath);
			if (imageFileInfo.Exists)
			{
				openPictureBox.Image = Image.FromFile(imagePath);
				picTextBox.Text = shortFileName;
			}
			else
			{
				setNotice("未找到图片，可点击灯具图片框重新选择灯具图片。", true);
			}
		}

		#endregion

		#region 保存灯具

		/// <summary>
		/// 事件：点击《保存灯具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveLightButton_Click(object sender, EventArgs e)
		{
			saveLight();
		}

		/// <summary>
		/// 事件：点击《允许改名另存》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void renameButton_Click(object sender, EventArgs e)
		{
			enableRename(true);
		}

		/// <summary>
		/// 辅助方法：请求保存
		/// </summary>
		/// <returns>返回true，则继续下去；返回false，则不再往下走</returns>
		/// <param name="v"></param>
		private bool RequestSaveLight(string msg)
		{
			// 若下面的灯具不可见，说明还没有打开或新建灯具，则直接返回true，无需进行保存。
			if (!lightGroupBox.Visible)
			{
				return true;
			}

			DialogResult dr = MessageBox.Show(
				msg,
				"保存灯具?",
				MessageBoxButtons.YesNoCancel,
				 MessageBoxIcon.Question
			);

			if (dr == DialogResult.Yes)
			{
				saveLight();
				return true;
			}
			else if (dr == DialogResult.No)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 辅助方法：保存当前灯具
		/// </summary>
		private void saveLight()
		{
			// 记住一个大原则，保存灯具时不对Form内任何内容进行改动，只读取！
			// 若修改了通道数后，未点击《生成》，则无法保存(不再有冗余的isGenerated属性，而直接由按键是否可用来判断是否已经生成过TongdaoList)
			if (generateButton.Enabled)
			{
				MessageBox.Show("请先点击《生成》按钮以生成新通道列表");
				return;
			}

			// 检验厂家名和型号名
			string name = nameTextBox.Text.Trim();
			string type = typeTextBox.Text.Trim();
			if (String.IsNullOrEmpty(name))
			{
				MessageBox.Show("请输入厂家名。");
				return;
			}
			if (!FileHelper.CheckFileName(name))
			{
				MessageBox.Show("厂家名含有非法字符，无法保存。");
				return;
			}
			if (String.IsNullOrEmpty(type))
			{
				MessageBox.Show("请输入型号名");
				return;
			}
			if (!FileHelper.CheckFileName(type))
			{
				MessageBox.Show("型号名含有非法字符，无法保存。");
				return;
			}

			///检查文件是否已经存在
			string fileName = lightDirectory + "\\" + name + "\\" + type + ".ini";
			FileInfo fi = new FileInfo(fileName);
			if (fi.Exists)
			{
				if (isNew)
				{
					DialogResult dr = MessageBox.Show("检查到系统中已存在同名灯具，是否覆盖？",
					"覆盖灯具？",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);
					//选中否（不覆盖），则退出本方法
					if (dr == DialogResult.No)
					{
						return;
					}
				}
			}
			//若文件已存在，说明目录肯定也存在，此时就无需判断目录是否存在了；只有fi.Exists == false 时，才走else内语句
			else
			{
				DirectoryInfo di = new DirectoryInfo(lightDirectory + "\\" + name);
				if (!di.Exists)
				{
					di.Create();
				}
			}

			//开始保存灯具
			using (StreamWriter iniWriter = new StreamWriter(fileName))
			{
				// 写[set]的数据
				iniWriter.WriteLine("[set]");
				iniWriter.WriteLine("type=" + type);
				iniWriter.WriteLine("pic=" + picTextBox.Text);
				iniWriter.WriteLine("count=" + tongdaoCount);
				iniWriter.WriteLine("name=" + name);

				//写[Data]数据
				iniWriter.WriteLine("[Data]");
				for (int tdIndex = 0; tdIndex < tongdaoCount; tdIndex++)
				{
					// 未满10的前面加0
					string index = (tdIndex < 9) ? ("0" + (tdIndex + 1)) : ("" + (tdIndex + 1));
					//iniWriter.WriteLine(index + "A=" + tongdaoList[tdIndex].TongdaoName);
					//用下行代码比较保险，因为用TongdaoName，会出现某些情况因考虑不周导致的未改变内存中的通道名（比如双击右侧nameLis【非lostFocus】）;这样就能所见即所得了
					iniWriter.WriteLine(index + "A=" + tdTextBoxes[tdIndex].Text.Trim());
					iniWriter.WriteLine(index + "B=" + tongdaoList[tdIndex].InitValue);
					iniWriter.WriteLine(index + "C=" + tongdaoList[tdIndex].Address);
				}

				//写[sa]数据
				iniWriter.WriteLine("[sa]");
				for (int tdIndex = 0; tdIndex < tongdaoCount; tdIndex++)
				{
					iniWriter.WriteLine(tdIndex + "_saCount=" + sawArray[tdIndex].SaList.Count);
					for (int saIndex = 0; saIndex < sawArray[tdIndex].SaList.Count; saIndex++)
					{
						iniWriter.WriteLine(tdIndex + "_" + saIndex + "_saName=" + sawArray[tdIndex].SaList[saIndex].SAName);
						iniWriter.WriteLine(tdIndex + "_" + saIndex + "_saStart=" + sawArray[tdIndex].SaList[saIndex].StartValue);
						iniWriter.WriteLine(tdIndex + "_" + saIndex + "_saEnd=" + sawArray[tdIndex].SaList[saIndex].EndValue);
					}
				}
			}
			enableRename(false);
			MessageBox.Show("已成功保存灯具。");
		}

		/// <summary>
		/// 辅助方法：允许更名-》此后将isNew设为true，可在保存前多一层验证
		/// </summary>
		/// <param name="enable"></param>
		private void enableRename(bool enable)
		{
			isNew = enable;
			nameTextBox.Enabled = enable;
			typeTextBox.Enabled = enable;
		}

		#endregion

		/// <summary>
		///  事件：更改《通道数下拉框》选中项：若与当前值不同则生成按钮可用；		
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void countComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			checkGenerateEnable();
		}

		/// <summary>
		/// 事件：点击《生成》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void generateButton_Click(object sender, EventArgs e)
		{
			generateTongdaoList();
			checkGenerateEnable();
			showTds();
			handleTongdaoCount();
			refreshPlayGroupBox();
		}

		/// <summary>
		/// 辅助方法：用于生成默认的tongdaoList（由tongdaoCount决定）
		/// 1.若之前列表为空，则从头开始添加列表数据
		/// 2.若之前列表已有数据，
		///   ①当tongdaoCount > tongdaoList.Count，添加新的数据到列表中去. -->相应的，SawArray也进行填充
		///   ②当tongdaoCount 小于等于 tongdaoList.Count，不进行任何操作(数据仍放在tongdaoList中）-- 但后期并不会存起来
		/// </summary>
		private void generateTongdaoList()
		{
			//0330 点击《生成》时，需要真的设置TongdaoCount了--》之前的版本只要改了《CountComboBox》就会更改TongdaoCount，显然有问题，但并未触发而已
			tongdaoCount = int.Parse(countComboBox.Text);

			// 新建的情况
			if (tongdaoList == null || tongdaoList.Count == 0)  
			{
				tongdaoList = new List<TongdaoWrapper>();
				sawArray = new SAWrapper[tongdaoCount];
				saDict = new Dictionary<int, FlowLayoutPanel>();

				for (int tdIndex = 0; tdIndex < tongdaoCount; tdIndex++)
				{
					tongdaoList.Add(new TongdaoWrapper()
					{
						TongdaoName = "通道" + (tdIndex + 1),
						Address = tdIndex + 1,
						InitValue = 0,
						CurrentValue = 0
					});
					sawArray[tdIndex] = new SAWrapper();
				}
			}
			else
			{
				if (tongdaoCount > tongdaoList.Count)
				{
					//先把旧数据存起来
					SAWrapper[] sawArrayTemp = SAWrapper.DeepCopy(sawArray);
					sawArray = new SAWrapper[tongdaoCount];
					// 小于等于新通道数量的数据，用旧数据填充
					for (int tdIndex = 0; tdIndex < sawArrayTemp.Length; tdIndex++)
					{
						sawArray[tdIndex] = sawArrayTemp[tdIndex];
					}

					for (int tdIndex = tongdaoList.Count; tdIndex < tongdaoCount; tdIndex++)
					{
						tongdaoList.Add(new TongdaoWrapper()
						{
							TongdaoName = "通道" + (tdIndex + 1),
							Address = tdIndex + 1,
							InitValue = 0,
							CurrentValue = 0
						});
						// 大于新通道数量的数据，用空数据填充
						sawArray[tdIndex] = new SAWrapper();
					}
				}
			}
			tdFlowLayoutPanel.Refresh();
		}
		
		#region 调试
						
		/// <summary>
		/// 事件：点击《刷新串口》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshButton_Click(object sender, EventArgs e)
		{
			refreshComList();
		}

		/// <summary>
		/// 辅助方法：刷新串口列表
		/// </summary>
		private void refreshComList()
		{
			player = OneLightOneStep.GetInstance();
			// 填充comComboBox
			IList<string> comList = player.GetDMX512DeviceList();
			comComboBox.Items.Clear();
			comComboBox.Text = "";
			if (comList.Count > 0)
			{
				foreach (string com in comList)
				{
					comComboBox.Items.Add(com);
				}
				comComboBox.SelectedIndex = 0;
				comComboBox.Enabled = true;
				connectButton.Enabled = true;
			}
			else
			{
				comComboBox.SelectedIndex = -1;
				comComboBox.Enabled = false;
				connectButton.Enabled = false;
			}
		}

		/// <summary>
		/// 事件：点击《连接设备》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectButton_Click(object sender, EventArgs e)
		{
			// 如果还没连接，那就连接  -->连接状态下《选择串口》不可用
			if (!isConnect)
			{
				if (player.ConnectDevice(comComboBox.Text))  //判断是否连接成功
				{					
					comComboBox.Enabled = false;
					refreshButton.Enabled = false;
					connectButton.Text = "断开连接";
					isConnect = true;					
					setNotice("成功打开串口(" + comComboBox.Text+")，并进入调试模式。" ,false);
					if (isConnect) {
						oneLightOneStep();
					}
				}
				else
				{
					setNotice("串口：" + comComboBox.Text + " 连接失败。"  , true);
				}
			}
			//否则断开连接: --> 《选择串口》设为可用
			else
			{
				player.CloseDevice();
				comComboBox.Enabled = true;
				refreshButton.Enabled = true;				
				connectButton.Text = "连接设备";
				isConnect = false;
				setNotice("成功断开连接，并退出调试模式。", false);
			}
		}

		/// <summary>
		///  辅助方法：单灯单步的操作
		/// </summary>
		private void oneLightOneStep()
		{
			if (!isConnect) {
				return;
			}

			byte[] stepBytes = new byte[512];
			foreach (TongdaoWrapper td in tongdaoList)
			{
				// firstTDValue 从1开始； td.Address也从1开始； 故如果初始地址为1，Address也是1，而512通道的第一个index应该是0
				// --> tongdaoIndex  = 1 + 1 -2；
				int tongdaoIndex = firstTDValue + td.Address - 2;
				stepBytes[tongdaoIndex] = (byte)(td.CurrentValue);
			}
			player.Preview(stepBytes);
		}
		
		/// <summary>
		/// 事件：点击设置初始通道地址
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setFirstTDButton_Click(object sender, EventArgs e)
		{
			firstTDValue = decimal.ToInt32(firstTDNumericUpDown.Value);
			for (int tdIndex = 0; tdIndex < tongdaoCount; tdIndex++)
			{
				tdLabels[tdIndex].Text = "通道" + (firstTDValue + tdIndex);
				if (selectedTextBox != null) {
					tdTextBoxSelected();
				}
				if (isConnect) {
					oneLightOneStep();
				}
			}
		}

		#endregion

		#region 统一设通道值

		/// <summary>
		/// 事件：点击《统一通道值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyValueButton_Click(object sender, EventArgs e)
		{
			int unifyValue = Decimal.ToInt32(unifyValueNumericUpDown.Value);
			setUnifyValue(unifyValue);
		}

		/// <summary>
		/// 事件：点击《全部归零》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zeroButton_Click(object sender, EventArgs e)
		{
			setUnifyValue(0);
		}

		/// <summary>
		/// 辅助方法：所有在用通道统一设值
		/// </summary>
		/// <param name="unifyValue"></param>
		private void setUnifyValue(int unifyValue)
		{
			for (int tdIndex = 0; tdIndex < tongdaoList.Count; tdIndex++)
			{
				// 统一调节时，不进行切换选中通道的操作;
				tdTrackBars[tdIndex].ValueChanged -= tdTrackBars_ValueChanged;
				tdNUDs[tdIndex].ValueChanged -= tdNUDs_ValueChanged;

				tdTrackBars[tdIndex].Value = unifyValue;
				tdNUDs[tdIndex].Value = unifyValue;
				tongdaoList[tdIndex].CurrentValue = unifyValue;

				tdTrackBars[tdIndex].ValueChanged += tdTrackBars_ValueChanged;
				tdNUDs[tdIndex].ValueChanged += tdNUDs_ValueChanged;
			}

			oneLightOneStep();

		}

		/// <summary>
		/// 事件：点击《全设初值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setInitButton_Click(object sender, EventArgs e)
		{
			for (int tdIndex = 0; tdIndex < tongdaoList.Count; tdIndex++)
			{
				tdTrackBars[tdIndex].Value = tongdaoList[tdIndex].InitValue;
				tdNUDs[tdIndex].Value = tongdaoList[tdIndex].InitValue;
				tongdaoList[tdIndex].CurrentValue = tongdaoList[tdIndex].InitValue;
			}
		}

		/// <summary>
		/// 事件：点击《设当前通道值为初始值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setCurrentToInitButton_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show("确定要把当前所有通道数值，设为此灯具的默认值吗？",
				"",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Question);
			if (dr == DialogResult.OK)
			{
				for (int tdIndex = 0; tdIndex < tongdaoList.Count; tdIndex++)
				{
					tongdaoList[tdIndex].InitValue = tongdaoList[tdIndex].CurrentValue;
				}
			}
		}

		#endregion

		#region 单独设通道值
		
		/// <summary>
		/// 事件：鼠标点击tdTextBox后，更改selectedTextBox（并刷新子属性按钮组）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdTextBoxes_MouseClick(object sender, MouseEventArgs e)
		{
			selectedTextBox = sender as TextBox;
			tdTextBoxSelected();
		}

		/// <summary>
		/// 事件：tdTextBoxes失去焦点时，进行是否空字符串的判断
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdTextBoxes_LostFocus(object sender, EventArgs e)
		{			
			TextBox tb = sender as TextBox;
			int tdIndex = MathHelper.GetIndexNum(tb.Name, -1);

			if (tb.Text.Trim() == "")
			{
				setNotice("请输入通道名，否则系统将自动为您生成名称。", true);
				tb.Text = "通道" + (tdIndex+1)  ;
			}
			
			tongdaoList[tdIndex].TongdaoName = tb.Text.Trim(); //更改为最新的名称					
		}

		/// <summary>
		/// 事件：鼠标点击标签
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdLabels_Click(object sender, EventArgs e)
		{
			int tdIndex = MathHelper.GetIndexNum(((Label)sender).Name, -1);
			selectedTextBox = tdTextBoxes[tdIndex];
			tdTextBoxes[tdIndex].Select();
			tdTextBoxSelected();
		}

		/// <summary>
		/// 事件：鼠标进入《tdLabel》时，把焦点切换到其numericUpDown中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdLabels_MouseEnter(object sender, EventArgs e)
		{
			int labelIndex = MathHelper.GetIndexNum(((Label)sender).Name, -1);
			tdNUDs[labelIndex].Select();
		}
				
		/// <summary>
		/// 事件：鼠标进入tdTrackBar时，把焦点切换到其numericUpDown中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdTrackBars_MouseEnter(object sender, EventArgs e)
		{
			int tdIndex = MathHelper.GetIndexNum(((TrackBar)sender).Name, -1);
			tdNUDs[tdIndex].Select();
		}

		/// <summary>
		/// 事件：《通道值NumericUpDown》鼠标中轴滚动时的操作：
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdTrackBars_MouseWheel(object sender, MouseEventArgs e)
		{			
			int tdIndex = MathHelper.GetIndexNum(((TrackBar)sender).Name, -1);
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{			
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
		///  事件：《通道值滚轴》值改变时的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdTrackBars_ValueChanged(object sender, EventArgs e)
		{
			// 1.先找出对应vScrollBars的index 
			int tongdaoIndex = MathHelper.GetIndexNum(((TrackBar)sender).Name, -1);

			//2.把滚动条的值赋给valueNumericUpDowns
			tdNUDs[tongdaoIndex].Value = tdTrackBars[tongdaoIndex].Value;

			//3.取出recentStep,使用取出的index，给stepWrapper.TongdaoList[index]赋值；并检查是否实时生成数据进行操作
			changeCurrentValue(tongdaoIndex, Decimal.ToInt32(tdNUDs[tongdaoIndex].Value));
		}

		/// <summary>
		/// 事件：《通道值NumericUpDown》鼠标中轴滚动时的操作：（其实方法可以通用，故可以把这段代码删除）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//private void tdNUDs_MouseWheel(object sender, MouseEventArgs e)
		//{
		//	Console.WriteLine("tdNUDs_MouseWheel");
		//	int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
		//	HandledMouseEventArgs hme = e as HandledMouseEventArgs;
		//	if (hme != null)
		//	{
		//		// Dickov: 当Handled设为true时，不再触发父控件的相关操作，即屏蔽滚动事件
		//		hme.Handled = true;
		//	}
		//	// 向上滚
		//	if (e.Delta > 0)
		//	{
		//		decimal dd = tdNUDs[tdIndex].Value + tdNUDs[tdIndex].Increment;
		//		if (dd <= tdNUDs[tdIndex].Maximum)
		//		{
		//			tdNUDs[tdIndex].Value = dd;
		//		}
		//	}
		//	// 向下滚
		//	else if (e.Delta < 0)
		//	{
		//		decimal dd = tdNUDs[tdIndex].Value - tdNUDs[tdIndex].Increment;
		//		if (dd >= tdNUDs[tdIndex].Minimum)
		//		{
		//			tdNUDs[tdIndex].Value = dd;
		//		}
		//	}
		//}

		/// <summary>
		/// 事件：调节或输入《通道值numericUpDown》的值后，1.调节通道值 2.调节tongdaoWrapper的相关值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdNUDs_ValueChanged(object sender, EventArgs e)
		{			
			// 1. 找出对应的index
			int tongdaoIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);

			// 2.调整相应的vScrollBar的数值
			tdTrackBars[tongdaoIndex].Value = decimal.ToInt32(tdNUDs[tongdaoIndex].Value);

			//3.取出tongdaoIndex，给tongdaoList[index]赋值；并检查是否实时生成数据进行操作
			changeCurrentValue(tongdaoIndex, decimal.ToInt32(tdNUDs[tongdaoIndex].Value));
		}

		/// <summary>
		///  辅助方法：改变值之后，更改对应的tongdaoList的值；并根据ifRealTime，决定是否实时调试灯具。
		/// </summary>
		/// <param name="tongdaoIndex"></param>
		private void changeCurrentValue(int tongdaoIndex, int tdValue)
		{		
			// 设tongdaoWrapper的值
			tongdaoList[tongdaoIndex].CurrentValue = tdValue;		

			if (isConnect) { 
				oneLightOneStep();
			}
		}

		/// <summary>
		/// 辅助方法：在点击通道名TextBox或通道名Label时（后期可考虑更多），右侧子属性栏进行相关
		/// </summary>
		private void tdTextBoxSelected()
		{
			if (selectedTextBox == null)
			{							
				clearTdRelated(true);
				setNotice("尚未选择通道（selectedTextBox == null）。", true);
				return;
			}
			selectedTdIndex = MathHelper.GetIndexNum(selectedTextBox.Name, -1);
			if (selectedTdIndex == -1)
			{				
				clearTdRelated(true);
				setNotice("尚未选择通道（selectedTDIndex==-1）。", true);
				return;	
			}

			tdNumLabel.Text = "已选中: " + tdLabels[selectedTdIndex].Text + "(" + (selectedTdIndex + 1) + ")" + " - " + tdTextBoxes[selectedTdIndex].Text;

			// 若不存在saDict，则生成
			if (saDict == null)
			{
				saDict = new Dictionary<int, FlowLayoutPanel>();
			}

			// 若当前通道没有存在相应的FlowLayoutPanel，则生成
			if (!saDict.ContainsKey(selectedTdIndex))
			{
				FlowLayoutPanel saFLP = new FlowLayoutPanel
				{
					AutoScroll = saFLPDemo.AutoScroll,
					BackColor = saFLPDemo.BackColor,
					BorderStyle = saFLPDemo.BorderStyle,
					Dock = saFLPDemo.Dock,
					Location = saFLPDemo.Location,
					Size = saFLPDemo.Size,
				};

				for(int saIndex =0; saIndex < sawArray[selectedTdIndex].SaList.Count; saIndex++) {
					saFLP.Controls.Add(  generateSaPanel(false,selectedTdIndex, saIndex ,0) );
				}
				
				saDict.Add(selectedTdIndex, saFLP);
			}

			// 此时一定存在相应的saFlowLayoutPanel,把它加入到界面中
			clearTdRelated(false);			
			saSmallPanel.Controls.Add( saDict[selectedTdIndex] );	

		}

		#endregion
		
		#region 通道名

		/// <summary>
		///  事件：双击把《右侧选择的通道名称值》填入左侧选择的《文本框》中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nameListBox_DoubleClick(object sender, EventArgs e)
		{
			if (selectedTextBox != null)
			{
				selectedTextBox.Text = (nameListBox.Text);
				if (selectedTdIndex != -1) {
					tongdaoList[selectedTdIndex].TongdaoName = selectedTextBox.Text.Trim();
				}
			}
			else
			{
				setNotice("请先选择通道名称文本框。", false);
			}
		}

		#endregion

		#region 子属性

		/// <summary>
		/// 辅助方法：清空td相关选项(未选中时，命名Panel和saPanel都隐藏掉)
		/// </summary>
		private void clearTdRelated(bool clear)
		{
			saSmallPanel.Controls.Clear(); // 不论是否清除，都把saSmallPanel内的flowLayoutPanel删掉

			tdNamePanel.Enabled = !clear;
			saBigPanel.Enabled = !clear;
			saTitlePanel2.Visible= !clear;
			if ( clear ) {				
				noticeLabel.Text = "请选择通道";
			}
		}
			   
		/// <summary>
		///事件：点击《添加子属性》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saAddButton_Click(object sender, EventArgs e)
		{
			saDict[selectedTdIndex].Controls.Add(
				generateSaPanel(
					true,
					selectedTdIndex,
					sawArray[selectedTdIndex].SaList.Count + 1,
					decimal.ToInt32(tdNUDs[selectedTdIndex].Value)
				)
			);			
		}

		/// <summary>
		///  事件：点击《清空子属性》:需要删除两个地方(saDict、sawArray)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saClearButton_Click(object sender, EventArgs e) {

			if (selectedTdIndex == -1 || !saDict.ContainsKey(selectedTdIndex))
			{
				setNotice("未选中通道或saDict内没有该通道的FLP数据。", true);
				return;
			}
			saDict[selectedTdIndex].Controls.Clear();
			sawArray[selectedTdIndex].SaList.Clear();		
		}
		
		/// <summary>
		/// 辅助方法：由内存里的sawList 和 传入的tdIndex及saIndex，实时生成saPanel
		/// </summary>
		private Panel generateSaPanel(bool newSa , int tdIndex, int saIndex , int startEndValue)
		{
			Panel saPanelTemp = new Panel
			{			
				Location = saPanelDemo.Location,
				Size = saPanelDemo.Size
			};

			TextBox saTextBoxTemp = new TextBox
			{
				Location = saTextBoxDemo.Location,
				Margin = saTextBoxDemo.Margin,
				Size = saTextBoxDemo.Size,
				TextAlign = saTextBoxDemo.TextAlign,
				MaxLength = saTextBoxDemo.MaxLength,
				Text = newSa?  "子属性"+saIndex : sawArray[tdIndex].SaList[saIndex].SAName
			};

			NumericUpDown saStartNUDTemp = new NumericUpDown
			{
				Name = "saStartNUD",
				Location = saNUDDemo1.Location,
				Size = saNUDDemo1.Size,
				TextAlign = saNUDDemo1.TextAlign,
				Maximum = saNUDDemo1.Maximum,
				Value = newSa ?  startEndValue : sawArray[tdIndex].SaList[saIndex].StartValue
			};

			NumericUpDown saEndNUDTemp = new NumericUpDown
			{
				Name = "saEndNUD",
				Location = saNUDDemo2.Location,
				Size = saNUDDemo2.Size,
				TextAlign = saNUDDemo2.TextAlign,
				Maximum = saNUDDemo2.Maximum,
				Minimum = newSa ? startEndValue : sawArray[tdIndex].SaList[saIndex].StartValue,
				Value = newSa ? startEndValue : sawArray[tdIndex].SaList[saIndex].EndValue
			};

			Button saDelButtonTemp = new Button
			{
				Location = saDelButtonDemo.Location,
				Size = saDelButtonDemo.Size,
				Text = "-",
				UseVisualStyleBackColor = true
			};

			saTextBoxTemp.LostFocus += saTextBox_LostFocus;
			saTextBoxTemp.MouseDoubleClick += saTextBoxes_MouseDoubleClick; // 双击则把通道值设备本子属性的值
			saStartNUDTemp.MouseWheel += someNUD_MouseWheel; // 通用鼠标滚动方法即可
			saEndNUDTemp.MouseWheel += someNUD_MouseWheel; 
			saStartNUDTemp.ValueChanged += SaNUD_ValueChanged;  // 起始都可以共用一个方法：用Name进行位置判断			
			saEndNUDTemp.ValueChanged += SaNUD_ValueChanged;
			saDelButtonTemp.Click += saDelButton_Click;

			saPanelTemp.Controls.Add(saTextBoxTemp);
			saPanelTemp.Controls.Add(saStartNUDTemp);
			saPanelTemp.Controls.Add(saEndNUDTemp);
			saPanelTemp.Controls.Add(saDelButtonTemp);

			// 当是新添加的Panel时，给内存数据也添加相同的内容（若是旧数据无需此操作，因为旧数据时，saPanel就是由该数据生成的）
			if (newSa) {
				sawArray[selectedTdIndex].SaList.Add(new SA
				{
					SAName = "子属性" + saIndex,
					StartValue = startEndValue,
					EndValue = startEndValue,
				});
			}

			return saPanelTemp;
		}
		
		/// <summary>
		/// 事件：saTextBox失去焦点时，把相应的数据更新到sawArray中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saTextBox_LostFocus(object sender, EventArgs e)
		{
			TextBox tb = sender as TextBox;			
			int saIndex = saDict[selectedTdIndex].Controls.IndexOf(tb.Parent);
			if (tb.Text.Trim() == "")
			{
				setNotice("请输入子属性名，否则系统将自动为您生成名称。", true);
				tb.Text = "子属性" + (saIndex + 1); 
			}
			sawArray[selectedTdIndex].SaList[saIndex].SAName = tb.Text.Trim();
		}

		/// <summary>
		/// 事件：双击《子属性名称TextBox》，可将该通道值设为子属性值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saTextBoxes_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			TextBox tb = sender as TextBox;
			int saIndex = saDict[selectedTdIndex].Controls.IndexOf(tb.Parent);
			tdNUDs[selectedTdIndex].Value = sawArray[selectedTdIndex].SaList[saIndex].StartValue;
		}

		/// <summary>
		/// 事件：当NUD值改变时，需要把sawArray内数据进行更新(通过控件的Name来判断起、始)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaNUD_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown nud = sender as NumericUpDown;
			int saIndex = saDict[selectedTdIndex].Controls.IndexOf(nud.Parent);
			//Console.WriteLine("现在ValueChanged的是" + selectedTdIndex + " -- " + saIndex + " ++ " + nud.Name);

			if (nud.Name == "saStartNUD")
			{
				(saDict[selectedTdIndex].Controls[saIndex].Controls[2] as NumericUpDown).Minimum = nud.Value;
				sawArray[selectedTdIndex].SaList[saIndex].StartValue = decimal.ToInt32(nud.Value);			
			}
			else {
				sawArray[selectedTdIndex].SaList[saIndex].EndValue = decimal.ToInt32(nud.Value);				
			}
		}		

		/// <summary>
		/// 事件：点击《-（删除子属性）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saDelButton_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			int saIndex = saDict[selectedTdIndex].Controls.IndexOf(btn.Parent);
			//Console.WriteLine("现在删除的是" + selectedTdIndex + " -- " + saDict[selectedTdIndex].Controls.IndexOf(btn.Parent) );

			// 两个地方都要删除：控件 及 sawArray
			saDict[selectedTdIndex].Controls.RemoveAt(saIndex);
			sawArray[selectedTdIndex].SaList.RemoveAt(saIndex);
		}
			   
		#endregion

		#region 通用

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
		/// 辅助方法：显示提示
		/// </summary>
		/// <param name="msgBoxShow"></param>
		private void setNotice(string msg, bool msgBoxShow)
		{
			myStatusLabel.Text = msg;
			if (msgBoxShow)
			{
				MessageBox.Show(msg);
			}
		}
			   
		#endregion

		
	}
}
