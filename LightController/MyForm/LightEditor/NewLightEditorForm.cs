using LighEditor.Tools;
using LightController.Common;
using LightController.MyForm;
using LightEditor.Ast;
using LightEditor.MyForm;
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
		private string savePath;  //软件各项功能保存的路径(软件目录或C:\Temp)
		private string picDirectory;     // 图片目录
		private string lightDirectory;   // ini保存目录

		// 与当前灯具相关的变量，最后会进行存储		
		public List<TongdaoWrapper> TongdaoList;
		public int TongdaoCount = 0;
		public SAWrapper[] SawArray;
		private bool isNew = true;  //是否新建

		//调试相关变量，最无关紧要
		private OneLightOneStep player; // 灯具测试的实例
		private int firstTDValue = 1;  // 初始通道地址值：最小为1,最大为512		
		private bool isConnect = false; // 辅助变量：是否连接设备
				
		private Panel[] tdPanels = new Panel[32];
		private TextBox[] tdTextBoxes = new TextBox[32];
		private Label[] tdLabels = new Label[32];
		private TrackBar[] tdTrackBars = new TrackBar[32];
		private NumericUpDown[] tdNUDs = new NumericUpDown[32];	

		// 原WaySetForm辅助变量
		private TextBox selectedTextBox = null; //辅助变量，用来记录鼠标选择的textBox
		private int selectedTdIndex = -1;
		//子属性相关		
		private SAWrapper[] sawArray2;
		private IList<Panel> saPanels = new List<Panel>();
		private IList<Label> saNameLabels = new List<Label>();
		private IList<Label> startValueLabels = new List<Label>();
		private IList<Label> lineLabels = new List<Label>();
		private IList<Label> endValueLabels = new List<Label>();
		private IList<Button> saDeleteButtons = new List<Button>();
		private SAForm saForm = null; // 需要一个全局变量，以实现几个Form同时可用。

		public NewLightEditorForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;
			InitializeComponent();

			softwareName = mainForm.SoftwareName + " Light Editor";
			Text = softwareName;
			savePath = mainForm.SavePath;
			picDirectory = @savePath + @"\LightPic";
			openImageDialog.InitialDirectory = picDirectory; //图片加载路径使用当前软件所在文件夹
			lightDirectory = @savePath + @"\LightLibrary";
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

				countComboBox.Items.Add(tdIndex + 1);

				tdTextBoxes[tdIndex].MouseClick += tdTextBoxes_MouseClick;
				tdLabels[tdIndex].MouseEnter += tdLabels_MouseEnter;
				tdLabels[tdIndex].Click += tdLabels_Click;
				tdTrackBars[tdIndex].MouseEnter += tdTrackBars_MouseEnter;
				tdTrackBars[tdIndex].MouseWheel += tdTrackBars_MouseWheel;
				tdTrackBars[tdIndex].ValueChanged += tdTrackBars_ValueChanged;
				tdNUDs[tdIndex].MouseWheel += someNUD_MouseWheel;
				tdNUDs[tdIndex].ValueChanged += tdNUDs_ValueChanged;
			}

			countComboBox.SelectedIndex = 0;
			firstTDNumericUpDown.MouseWheel += someNUD_MouseWheel ;
			unifyValueNumericUpDown.MouseWheel += someNUD_MouseWheel;
			refreshComList();
		}

		private void NewLightEditorForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 20, mainForm.Location.Y + 60);
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
				TongdaoCount = 0;				

				TongdaoList = null;
				SawArray = null;
				nameTextBox.Text = "";
				typeTextBox.Text = "";
				picTextBox.Text = "";
				openPictureBox.Image = null;

				firstTDNumericUpDown.Value = 1;

				showTds();
				showAllPanels();
				enableRename(true);
				
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

				TongdaoCount = int.Parse(lineList[3].ToString().Substring(6));//第七个字符开始截取			

				countComboBox.SelectedIndex = TongdaoCount - 1;   // 此处请注意：并不是用SelectedText，而是直接设Text			

				TongdaoList = new List<TongdaoWrapper>();
				for (int i = 0; i < TongdaoCount; i++)
				{
					string tongdaoName = lineList[3 * i + 6].ToString().Substring(4);
					int initValue = int.Parse(lineList[3 * i + 7].ToString().Substring(4));
					int address = int.Parse(lineList[3 * i + 8].ToString().Substring(4));
					TongdaoList.Add(new TongdaoWrapper()
					{
						TongdaoName = tongdaoName,
						InitValue = initValue,
						Address = address,
						CurrentValue = initValue
					});
				}

				try
				{
					SawArray = SAWrapper.GetSawArrayFromIni(iniPath);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}				
			}

			showTds();
			handleTongdaoCount(); // 每次可能改动通道数量的操作后，要对初始的通道值进行限制（避免超过512）
			showAllPanels();
			enableRename(false);				
		}

		/// <summary>
		/// 辅助方法：每次可能改动通道数量的操作后，要对初始的通道值进行限制（避免超过512）
		/// </summary>
		private void handleTongdaoCount()
		{
			firstTDNumericUpDown.Maximum = 513 - TongdaoCount ;
		}

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
		/// 辅助方法：不管什么情况（打开或新建灯具），都把隐藏的界面打开
		/// </summary>
		private void showAllPanels() {
			lightGroupBox.Show();
			playGroupBox.Show();
			tdNamePanel.Show();
			saFlowLayoutPanel.Show();
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
			for (int tdIndex = 0; tdIndex < TongdaoCount; tdIndex++)
			{
				tdTextBoxes[tdIndex].Text =  TongdaoList[tdIndex].TongdaoName;
				tdLabels[tdIndex].Text = "通道" + (tdIndex + 1);
				tdTrackBars[tdIndex].Value = TongdaoList[tdIndex].CurrentValue;				
				tdNUDs[tdIndex].Value = TongdaoList[tdIndex].CurrentValue;

				string tdRemark = TongdaoList[tdIndex].TongdaoName;
				foreach (SA sa in SawArray[tdIndex].SaList)
				{
					tdRemark += "\n" + sa.SAName + "：" + sa.StartValue + " - " + sa.EndValue;
				}

				tdPanels[tdIndex].Show();
			}

			// 2.隐藏其余通道
			for (int tdIndex = TongdaoCount; tdIndex < 32; tdIndex++)
			{
				tdPanels[tdIndex].Hide();
			}
		}

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
				setNotice("未找到图片", true);
			}
		}
		
		/// <summary>
		/// 辅助方法：请求保存
		/// </summary>
		/// <returns>返回true，则继续下去；返回false，则不再往下走</returns>
		/// <param name="v"></param>
		private bool RequestSaveLight(string msg)
		{
			// 若下面的灯具不可见，说明还没有打开或新建灯具，则直接返回true，无需进行保存。
			if ( ! lightGroupBox.Visible )
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
		private void saveLight() {
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
				iniWriter.WriteLine("count=" + TongdaoCount);
				iniWriter.WriteLine("name=" + name);

				//写[Data]数据
				iniWriter.WriteLine("[Data]");
				for (int tdIndex = 0; tdIndex < TongdaoCount; tdIndex++)
				{
					// 未满10的前面加0
					string index = (tdIndex < 9) ? ("0" + (tdIndex + 1)) : ("" + (tdIndex + 1));
					iniWriter.WriteLine(index + "A=" + TongdaoList[tdIndex].TongdaoName);
					iniWriter.WriteLine(index + "B=" + TongdaoList[tdIndex].InitValue);
					iniWriter.WriteLine(index + "C=" + TongdaoList[tdIndex].Address);
				}

				//写[sa]数据
				iniWriter.WriteLine("[sa]");
				for (int tdIndex = 0; tdIndex < TongdaoCount; tdIndex++)
				{
					iniWriter.WriteLine(tdIndex + "_saCount=" + SawArray[tdIndex].SaList.Count);
					for (int saIndex = 0; saIndex < SawArray[tdIndex].SaList.Count; saIndex++)
					{
						iniWriter.WriteLine(tdIndex + "_" + saIndex + "_saName=" + SawArray[tdIndex].SaList[saIndex].SAName);
						iniWriter.WriteLine(tdIndex + "_" + saIndex + "_saStart=" + SawArray[tdIndex].SaList[saIndex].StartValue);
						iniWriter.WriteLine(tdIndex + "_" + saIndex + "_saEnd=" + SawArray[tdIndex].SaList[saIndex].EndValue);
					}
				}
			}
			enableRename(false);
			MessageBox.Show("已成功保存灯具。");
		}


		/// <summary>
		/// 事件：点击《改名另存》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void renameButton_Click(object sender, EventArgs e)
		{
			enableRename(true);
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

		/// <summary>
		///  事件：更改《通道数下拉框》选中项：若与当前值不同则生成按钮可用；		
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void countComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			int tempSelectedCount = int.Parse(countComboBox.SelectedItem.ToString());
			// 两者不同的话，则生成按钮可用
			generateButton.Enabled = tempSelectedCount != TongdaoCount;
		}

		/// <summary>
		/// 事件：点击《生成》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void generateButton_Click(object sender, EventArgs e)
		{
			generateTongdaoList();
			generateButton.Enabled = false;
			showTds();
			handleTongdaoCount();
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
			TongdaoCount = int.Parse(countComboBox.Text);
			if (TongdaoList == null || TongdaoList.Count == 0)
			{
				TongdaoList = new List<TongdaoWrapper>();
				SawArray = new SAWrapper[TongdaoCount];

				for (int tdIndex = 0; tdIndex < TongdaoCount; tdIndex++)
				{
					TongdaoList.Add(new TongdaoWrapper()
					{
						TongdaoName = "通道" + (tdIndex + 1),
						Address = tdIndex + 1,
						InitValue = 0,
						CurrentValue = 0
					});
					SawArray[tdIndex] = new SAWrapper();
				}
			}
			else
			{
				if (TongdaoCount > TongdaoList.Count)
				{
					//先把旧数据存起来
					SAWrapper[] sawArrayTemp = SAWrapper.DeepCopy(SawArray);
					SawArray = new SAWrapper[TongdaoCount];
					// 小于等于新通道数量的数据，用旧数据填充
					for (int tdIndex = 0; tdIndex < sawArrayTemp.Length; tdIndex++)
					{
						SawArray[tdIndex] = sawArrayTemp[tdIndex];
					}

					for (int tdIndex = TongdaoList.Count; tdIndex < TongdaoCount; tdIndex++)
					{
						TongdaoList.Add(new TongdaoWrapper()
						{
							TongdaoName = "通道" + (tdIndex + 1),
							Address = tdIndex + 1,
							InitValue = 0,
							CurrentValue = 0
						});
						// 大于新通道数量的数据，用空数据填充
						SawArray[tdIndex] = new SAWrapper();
					}
				}
			}
		}
		
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
			}
			else
			{
				setNotice("请先选择通道名称文本框。",false);
			}
		}

	

		/// <summary>
		/// 辅助方法：刷新SAPanels
		/// </summary>
		private void refreshSAPanels()
		{
			if (selectedTdIndex > -1)
			{
				saFlowLayoutPanel.Enabled = true;
				tdNumLabel.Text = "已选中: " +  tdLabels[selectedTdIndex].Text + "(" + (selectedTdIndex+1) +")" + " - " + tdTextBoxes[selectedTdIndex].Text;
				clearSaPanels();
				foreach (SA sa in SawArray[selectedTdIndex].SaList)
				{
					AddSAPanel(sa);
				}
			}
			else
			{
				saFlowLayoutPanel.Enabled = false;
				tdNumLabel.Text = "请选择通道";
				clearSaPanels();
			}
		}

		/// <summary>
		/// 辅助方法：添加saPanel，主要供SAForm回调使用
		/// </summary>
		public void AddSAPanel(SA sa)
		{
			AddSAPanel(sa.SAName, sa.StartValue, sa.EndValue);
		}

		/// <summary>
		/// 辅助方法：添加saPanel，主要供SAForm回调使用
		/// </summary>
		public void AddSAPanel(string saName, int startValue, int endValue)
		{
			Panel saPanelTemp = new Panel();
			Label saNameLabelTemp = new Label();
			Label startLabelTemp = new Label();
			Label lineLabelTemp = new Label();
			Label endLabelTemp = new Label();
			Button saDeleteButtonTemp = new Button();

			saPanels.Add(saPanelTemp);
			saNameLabels.Add(saNameLabelTemp);
			startValueLabels.Add(startLabelTemp);
			lineLabels.Add(lineLabelTemp);
			endValueLabels.Add(endLabelTemp);
			saDeleteButtons.Add(saDeleteButtonTemp);

			this.saFlowLayoutPanel.Controls.Add(saPanelTemp);
			this.saFlowLayoutPanel.Controls.Add(saDeleteButtonTemp);
			// 
			// saPanel
			// 
			saPanelTemp.BackColor = SystemColors.Window;
			saPanelTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			saPanelTemp.Controls.Add(saNameLabelTemp);
			saPanelTemp.Controls.Add(startLabelTemp);
			saPanelTemp.Controls.Add(lineLabelTemp);
			saPanelTemp.Controls.Add(endLabelTemp);
			saPanelTemp.Location = new System.Drawing.Point(3, 42);
			saPanelTemp.Name = "saPanel";
			saPanelTemp.Size = new System.Drawing.Size(168, 33);
			saPanelTemp.TabIndex = 1;
			saPanelTemp.Click += new EventHandler(saPanel_Click);

			// 
			// saNameLabel
			// 
			saNameLabelTemp.Location = new System.Drawing.Point(4, 9);
			saNameLabelTemp.Name = "saNameLabel";
			saNameLabelTemp.Size = new System.Drawing.Size(90, 12);
			saNameLabelTemp.TabIndex = 0;
			saNameLabelTemp.Text = saName;
			saNameLabelTemp.Click += new EventHandler(saLabel_Click);
			myToolTip.SetToolTip(saNameLabelTemp, saName);
			// 
			// startValueLabel
			// 
			startLabelTemp.Location = new System.Drawing.Point(101, 9);
			startLabelTemp.Name = "startValueLabel";
			startLabelTemp.Size = new System.Drawing.Size(23, 12);
			startLabelTemp.TabIndex = 2;
			startLabelTemp.Text = startValue.ToString();
			startLabelTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			startLabelTemp.Click += new EventHandler(saLabel_Click);
			// 
			// lineLabel
			// 
			lineLabelTemp.AutoSize = true;
			lineLabelTemp.Location = new System.Drawing.Point(128, 9);
			lineLabelTemp.Name = "lineLabel";
			lineLabelTemp.Size = new System.Drawing.Size(11, 12);
			lineLabelTemp.TabIndex = 3;
			lineLabelTemp.Text = "-";
			lineLabelTemp.Click += new EventHandler(saLabel_Click);
			// 
			// endValueLabel
			// 
			endLabelTemp.Location = new System.Drawing.Point(143, 9);
			endLabelTemp.Name = "endValueLabel";
			endLabelTemp.Size = new System.Drawing.Size(23, 12);
			endLabelTemp.TabIndex = 4;
			endLabelTemp.Text = endValue.ToString();
			endLabelTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			endLabelTemp.Click += new EventHandler(saLabel_Click);

			// 
			// saDeleteButton
			// 
			//saDeleteButtonTemp.Location = new System.Drawing.Point(170, 4);
			saDeleteButtonTemp.Name = "saDeleteButton";
			saDeleteButtonTemp.Size = new System.Drawing.Size(19, 33);
			saDeleteButtonTemp.TabIndex = 1;
			saDeleteButtonTemp.Text = "-";
			saDeleteButtonTemp.UseVisualStyleBackColor = true;
			saDeleteButtonTemp.Click += new System.EventHandler(this.saDeleteButton_Click);
		}

		/// <summary>
		/// 辅助方法：清空所有的SAPanel
		/// </summary>
		private void clearSaPanels()
		{
			foreach (Panel saPanel in saPanels)
			{
				saFlowLayoutPanel.Controls.Remove(saPanel);
			}
			foreach (Button saDelButton in saDeleteButtons)
			{
				saFlowLayoutPanel.Controls.Remove(saDelButton);
			}
			saPanels.Clear();
			saNameLabels.Clear();
			startValueLabels.Clear();
			lineLabels.Clear();
			endValueLabels.Clear();
			saDeleteButtons.Clear();
		}

		/// <summary>
		/// 事件：点击《saPanel内的saLabels(包括saNameLabel、startValueLabel、lineLabel、endValueLabel)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saLabel_Click(object sender, EventArgs e)
		{
			int saIndex;
			Label label = ((Label)sender);
			switch (label.Name)
			{
				case "saNameLabel": saIndex = saNameLabels.IndexOf(label); break;
				case "startValueLabel": saIndex = startValueLabels.IndexOf(label); break;
				case "lineLabel": saIndex = lineLabels.IndexOf(label); break;
				case "endValueLable": saIndex = endValueLabels.IndexOf(label); break;
				default: return;
			}
			saPanelsClick(saIndex);
		}

		/// <summary>
		/// 辅助方法：在saPanels内点击任意区域，皆可弹出子属性修改栏
		/// </summary>
		/// <param name="saIndex"></param>
		private void saPanelsClick(int saIndex)
		{
			if (saForm == null)
			{
				if (saIndex == -1)
				{
					MessageBox.Show("所点击区域不属于saPanels");
					return;
				}
				Enabled = false;
				//TODO 810
				//saForm = new SAForm(this, saIndex,
				//	saNameLabels[saIndex].Text,
				//	int.Parse(startValueLabels[saIndex].Text),
				//	int.Parse(endValueLabels[saIndex].Text)
				//);
				//saForm.Show();
			}
			else
			{
				MessageBox.Show("检测到您已打开一个子属性窗体，\n请关闭后再重新点击修改子属性。");
				saForm.Activate();
			}
		}

		/// <summary>
		/// 事件：点击《saPanel内的剩余空白区域》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saPanel_Click(object sender, EventArgs e)
		{
			int saIndex = saPanels.IndexOf((Panel)sender);
			saPanelsClick(saIndex);
		}

		/// <summary>
		/// 事件：点击《-（删除子属性）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saDeleteButton_Click(object sender, EventArgs e)
		{
			int saIndex = saDeleteButtons.IndexOf((Button)sender);
			if (saIndex == -1)
			{
				MessageBox.Show("这个按键不属于saDeleteButtons");
				return;
			}

			saFlowLayoutPanel.Controls.Remove(saPanels[saIndex]);
			saFlowLayoutPanel.Controls.Remove(saDeleteButtons[saIndex]);
			saPanels.RemoveAt(saIndex);
			saNameLabels.RemoveAt(saIndex);
			startValueLabels.RemoveAt(saIndex);
			lineLabels.RemoveAt(saIndex);
			endValueLabels.RemoveAt(saIndex);
			saDeleteButtons.RemoveAt(saIndex);

			sawArray2[selectedTdIndex].SaList.RemoveAt(saIndex);
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

		#region playGroupBox内数值调整相关方法
						
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
					nud.Value = decimal.ToInt32(dd);
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd =nud.Value - nud.Increment;
				if (dd >= nud.Minimum)
				{
					nud.Value = decimal.ToInt32(dd);
				}
			}
		}

		/// <summary>
		/// 事件：点击设置初始通道地址
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setFirstTDButton_Click(object sender, EventArgs e)
		{
			firstTDValue = decimal.ToInt32(firstTDNumericUpDown.Value);
			for (int tdIndex = 0; tdIndex < TongdaoCount; tdIndex++)
			{
				tdLabels[tdIndex].Text = "通道" + (firstTDValue + tdIndex);
				tdTextBoxSelected();
			}
		}

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

			for (int i = 0; i < TongdaoList.Count; i++)
			{
				tdTrackBars[i].Value = unifyValue;
				tdNUDs[i].Value = unifyValue;
				TongdaoList[i].CurrentValue = unifyValue;
			}
		}

		/// <summary>
		/// 事件：点击《全设初值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setInitButton_Click(object sender, EventArgs e)
		{
			for (int tdIndex = 0; tdIndex < TongdaoList.Count; tdIndex++)
			{
				tdTrackBars[tdIndex].Value = TongdaoList[tdIndex].InitValue;
				tdNUDs[tdIndex].Value = TongdaoList[tdIndex].InitValue;
				TongdaoList[tdIndex].CurrentValue = TongdaoList[tdIndex].InitValue;
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
				for (int tdIndex = 0; tdIndex < TongdaoList.Count; tdIndex++)
				{
					TongdaoList[tdIndex].InitValue = TongdaoList[tdIndex].CurrentValue;
				}
			}
		}

		#endregion

		#region 通道具体相关的滚动及值改动事件等

		/// <summary>
		/// 事件：鼠标点击tdTextBox后，更改selectedTextBox（并刷新子属性按钮组）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdTextBoxes_MouseClick(object sender, MouseEventArgs e)
		{
			selectedTextBox = (TextBox)sender;
			tdTextBoxSelected();
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
			int labelIndex = MathHelper.GetIndexNum(((TrackBar)sender).Name, -1);
			tdNUDs[labelIndex].Select();
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
			// 1.设tongdaoWrapper的值
			TongdaoList[tongdaoIndex].CurrentValue = tdValue;

			if (isConnect) { 
				oneLightOneStep();
			}
		}

		/// <summary>
		/// 辅助方法：在点击通道名TextBox或通道名Label时（后期可考虑更多），右侧子属性栏进行相关
		/// </summary>
		private void tdTextBoxSelected()
		{
			if (selectedTextBox != null)
			{
				selectedTdIndex = MathHelper.GetIndexNum(selectedTextBox.Name, -1);
				if (selectedTdIndex > -1)
				{
					refreshSAPanels();
				}
			}
		}

		#endregion

		/// <summary>
		///  辅助方法：单灯单步的操作
		/// </summary>
		private void oneLightOneStep()
		{
			byte[] stepBytes = new byte[512];
			foreach (TongdaoWrapper td in TongdaoList)
			{
				// firstTDValue 从1开始； td.Address也从1开始； 故如果初始地址为1，Address也是1，而512通道的第一个index应该是0
				// --> tongdaoIndex  = 1 + 1 -2；
				int tongdaoIndex = firstTDValue + td.Address - 2;
				stepBytes[tongdaoIndex] = (byte)(td.CurrentValue);
			}
			player.Preview(stepBytes);
		}

	
	}
}
