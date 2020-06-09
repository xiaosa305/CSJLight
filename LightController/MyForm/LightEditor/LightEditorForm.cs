using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections;
using LighEditor.Tools;
using LighEditor;
using LightController.MyForm;
using LightEditor.Ast;
using LightController.Common;

namespace LightEditor
{
	public partial class LightEditorForm : Form
	{
		// 全局变量，启动时载入，之后不会更改
		private MainFormBase mainForm;

		private string softwareName;  //动态更改软件名
		private string savePath;  //软件各项功能保存的路径(软件目录或C:\Temp)
		private string picDirectory;     // 图片目录
		private string lightDirectory;   // ini保存目录

		// 与当前灯具相关的变量，最后会进行存储		
		public List<TongdaoWrapper> TongdaoList;
		public int TongdaoCount = 0;
		public SAWrapper[] SawArray;
		private bool isNew = true;

		//调试相关变量，最无关紧要
		private OneLightOneStep player; // 灯具测试的实例
		private int firstTDValue = 1;  // 初始通道地址值：最小为1,最大为512
		private bool isRealTime = false; //是否勾选“实时调试”
		private bool isConnect = false; // 辅助变量：是否连接设备			

		private WaySetForm waySetForm;    // 设置一个全局的WaySetForm变量，默认为null （ 因为窗体需要Show而非ShowDialog，若非全局，可能创建过多的WaySetForm）

		public LightEditorForm(MainFormBase mainForm)
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

			#region 初始化几个数组

			valueVScrollBars[0] = vScrollBar1;
			valueVScrollBars[1] = vScrollBar2;
			valueVScrollBars[2] = vScrollBar3;
			valueVScrollBars[3] = vScrollBar4;
			valueVScrollBars[4] = vScrollBar5;
			valueVScrollBars[5] = vScrollBar6;
			valueVScrollBars[6] = vScrollBar7;
			valueVScrollBars[7] = vScrollBar8;
			valueVScrollBars[8] = vScrollBar9;
			valueVScrollBars[9] = vScrollBar10;
			valueVScrollBars[10] = vScrollBar11;
			valueVScrollBars[11] = vScrollBar12;
			valueVScrollBars[12] = vScrollBar13;
			valueVScrollBars[13] = vScrollBar14;
			valueVScrollBars[14] = vScrollBar15;
			valueVScrollBars[15] = vScrollBar16;
			valueVScrollBars[16] = vScrollBar17;
			valueVScrollBars[17] = vScrollBar18;
			valueVScrollBars[18] = vScrollBar19;
			valueVScrollBars[19] = vScrollBar20;
			valueVScrollBars[20] = vScrollBar21;
			valueVScrollBars[21] = vScrollBar22;
			valueVScrollBars[22] = vScrollBar23;
			valueVScrollBars[23] = vScrollBar24;
			valueVScrollBars[24] = vScrollBar25;
			valueVScrollBars[25] = vScrollBar26;
			valueVScrollBars[26] = vScrollBar27;
			valueVScrollBars[27] = vScrollBar28;
			valueVScrollBars[28] = vScrollBar29;
			valueVScrollBars[29] = vScrollBar30;
			valueVScrollBars[30] = vScrollBar31;
			valueVScrollBars[31] = vScrollBar32;

			valueNumericUpDowns[0] = numericUpDown1;
			valueNumericUpDowns[1] = numericUpDown2;
			valueNumericUpDowns[2] = numericUpDown3;
			valueNumericUpDowns[3] = numericUpDown4;
			valueNumericUpDowns[4] = numericUpDown5;
			valueNumericUpDowns[5] = numericUpDown6;
			valueNumericUpDowns[6] = numericUpDown7;
			valueNumericUpDowns[7] = numericUpDown8;
			valueNumericUpDowns[8] = numericUpDown9;
			valueNumericUpDowns[9] = numericUpDown10;
			valueNumericUpDowns[10] = numericUpDown11;
			valueNumericUpDowns[11] = numericUpDown12;
			valueNumericUpDowns[12] = numericUpDown13;
			valueNumericUpDowns[13] = numericUpDown14;
			valueNumericUpDowns[14] = numericUpDown15;
			valueNumericUpDowns[15] = numericUpDown16;
			valueNumericUpDowns[16] = numericUpDown17;
			valueNumericUpDowns[17] = numericUpDown18;
			valueNumericUpDowns[18] = numericUpDown19;
			valueNumericUpDowns[19] = numericUpDown20;
			valueNumericUpDowns[20] = numericUpDown21;
			valueNumericUpDowns[21] = numericUpDown22;
			valueNumericUpDowns[22] = numericUpDown23;
			valueNumericUpDowns[23] = numericUpDown24;
			valueNumericUpDowns[24] = numericUpDown25;
			valueNumericUpDowns[25] = numericUpDown26;
			valueNumericUpDowns[26] = numericUpDown27;
			valueNumericUpDowns[27] = numericUpDown28;
			valueNumericUpDowns[28] = numericUpDown29;
			valueNumericUpDowns[29] = numericUpDown30;
			valueNumericUpDowns[30] = numericUpDown31;
			valueNumericUpDowns[31] = numericUpDown32;

			labels[0] = label1;
			labels[1] = label2;
			labels[2] = label3;
			labels[3] = label4;
			labels[4] = label5;
			labels[5] = label6;
			labels[6] = label7;
			labels[7] = label8;
			labels[8] = label9;
			labels[9] = label10;
			labels[10] = label11;
			labels[11] = label12;
			labels[12] = label13;
			labels[13] = label14;
			labels[14] = label15;
			labels[15] = label16;
			labels[16] = label17;
			labels[17] = label18;
			labels[18] = label19;
			labels[19] = label20;
			labels[20] = label21;
			labels[21] = label22;
			labels[22] = label23;
			labels[23] = label24;
			labels[24] = label25;
			labels[25] = label26;
			labels[26] = label27;
			labels[27] = label28;
			labels[28] = label29;
			labels[29] = label30;
			labels[30] = label31;
			labels[31] = label32;

			for (int i = 0; i < 32; i++)
			{
				countComboBox.Items.Add(i + 1);
				valueNumericUpDowns[i].MouseWheel += new System.Windows.Forms.MouseEventHandler(valueNumericUpDown_MouseWheel);
				valueVScrollBars[i].ValueChanged += new System.EventHandler(valueVScrollBar_ValueChanged);
				labels[i].Click += new System.EventHandler(labels_Click);
			}
			countComboBox.SelectedIndex = 0;
			firstTDNumericUpDown.MouseWheel += new System.Windows.Forms.MouseEventHandler(firstTDNumericUpDown_MouseWheel);
			commonValueNumericUpDown.MouseWheel += new System.Windows.Forms.MouseEventHandler(commonValueNumericUpDown_MouseWheel);

			#endregion

			refreshComList();
		}

		/// <summary>
		/// 事件：初始化窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e) {
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			WindowState = FormWindowState.Normal;
		}

		/// <summary>
		/// 事件：《LightEditorForm》关闭时进行相关资源回收
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LightEditorForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			//因为player不属于本Form，其占用的资源不会释放（因为没有关闭整个项目），所以退出前，一定要主动释放！
			if (isConnect)
			{
				player.CloseDevice();
			}

			if (waySetForm != null) {
				waySetForm.Exit();
				waySetForm = null;
			}

			Dispose();         
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：点击《(右上角?)Help》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LightEditorForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show("灯具的厂家名及型号名都不可使用\\、/、:、*、?、\"、<、>、| 等字符，否则操作系统(windows)无法保存，会出现错误。",
				"使用提示或说明",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
			e.Cancel = true;
		}

		#region 灯具相关按钮事件及辅助方法

		/// <summary>
		/// 事件：点击《新建灯具》
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

				ShowTds();
				enableRename(true);
				editGroupBox.Show();
				connectPanel.Show();
			}
		}

		/// <summary>
		/// 事件：点击《打开灯具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openLightButton_Click(object sender, EventArgs e)
		{
			if (RequestSaveLight("打开灯具前，是否保存当前灯具？")) {
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
					TongdaoList.Add(new TongdaoWrapper() {
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

				ShowTds();
				enableRename(false);
				editGroupBox.Show();
				connectPanel.Show();
			}
		}

		/// <summary>
		/// 事件：点击《保存灯具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveLightButton_Click(object sender, EventArgs e)
		{
			// 记住一个大原则，保存灯具时不对Form内任何内容进行改动，只读取！
			// 若修改了通道数后，未点击《生成》，则无法保存(不再有冗余的isGenerated属性，而直接由按键是否可用来判断是否已经生成过TongdaoList)
			if (generateButton.Visible) {
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
				if (isNew) {
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
			}//若文件已存在，说明目录肯定也存在，此时就无需判断目录是否存在了；只有fi.Exists == false 时，才走else内语句
			else {
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
		private void enableRename(bool enable) {
			isNew = enable;
			nameTextBox.Enabled = enable;
			typeTextBox.Enabled = enable;
		}

		/// <summary>
		/// 辅助方法：请求保存
		/// </summary>
		/// <returns>返回true，则继续下去；返回false，则不再往下走</returns>
		/// <param name="v"></param>
		private bool RequestSaveLight(string msg)
		{
			// 若下面的灯具不可见，说明还没有打开或新建灯具，则直接返回true，无需进行保存。
			if (editGroupBox.Visible == false)
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
				saveLightButton_Click(null, null);
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

		#endregion

		/// <summary>
		/// 事件：点击《灯具图片框》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_Click(object sender, EventArgs e)
		{
			openImageDialog.ShowDialog();
		}

		/// <summary>
		///  打开图片对话框，选择图片后的操作：调用相关方法，设置两个值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openImageDialog_FileOk(object sender, CancelEventArgs e) {
			string imagePath = openImageDialog.FileName;
			setImage(imagePath);
		}

		/// <summary>
		/// 辅助方法：通过图片路径，改变image相关的两个内容：①打开灯具②更改pictureBox内容后调用
		/// </summary>
		/// <param name="imagePathName"></param>
		private void setImage(string imagePath) {
			string shortFileName = imagePath.Substring(imagePath.LastIndexOf("\\") + 1);
			// 从本地目录加载图片			
			FileInfo imageFileInfo = new FileInfo(imagePath);
			if (imageFileInfo.Exists)
			{
				openPictureBox.Image = Image.FromFile(imagePath);
				picTextBox.Text = shortFileName;
			}
			else {
				MessageBox.Show("未找到图片");
			}
		}

		/// <summary>
		///  事件：更改《通道数下拉框》选中项
		///  1.修改tongdaoCount的值为选中值；
		///  2.显示《生成》按钮，隐藏《通道编辑》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void countComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			int tempSelectedCount = int.Parse(countComboBox.SelectedItem.ToString());
			// 两者相同，则显示修改按键（editButton）；不同的话，则显示生成按钮
			showTongdaoEditButton(tempSelectedCount == TongdaoCount);
		}

		/// <summary>
		///  事件：点击生成按钮后的操作：
		///  1.检查通道数 ；
		///  2.检查若通过，则生成默认通道列表		
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void generateButton_Click(object sender, EventArgs e)
		{
			generateTongdaoList();
			showTongdaoEditButton(true);
			ShowTds();
		}

		/// <summary>
		///  事件：点击《通道编辑》
		///  1. 生成一个新的的WaySetForm，并将tongdaoList的数据渲染进这个form中
		///  2.显示这个form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tongdaoEditButton_Click(object sender, EventArgs e)
		{
			showWSForm(-1);
		}
		
		/// <summary>
		///  辅助方法：显示通道编辑按钮(true)或生成(false)按钮（二选一）
		/// </summary>
		/// <param name="isShowEditButton"></param>
		private void showTongdaoEditButton(bool isShowEditButton)
		{
			tongdaoEditButton.Visible = isShowEditButton;
			generateButton.Visible = !isShowEditButton;
		}

		/// <summary>
		///  辅助方法：渲染并显示相应的通道列表
		///  1.将tongdaoList渲染进下拉条组中
		///  2.显示显示当前数量的下拉条，并将剩余的隐藏起来
		///  3.根据通道数，显示相应的GroupBox
		/// </summary> 
		internal void ShowTds()
		{
			// 1.tongdaoList的数据渲染进各个通道显示项(label+valueLabel+vScrollBar)中, 并显示有数据的通道
			for (int i = 0; i < TongdaoCount; i++)
			{
				labels[i].Text = (firstTDValue + i) + "-  " + TongdaoList[i].TongdaoName;
				labels[i].Show();
				valueVScrollBars[i].Value = 255 - TongdaoList[i].CurrentValue;
				valueVScrollBars[i].Show();
				valueNumericUpDowns[i].Value = TongdaoList[i].CurrentValue;
				valueNumericUpDowns[i].Show();

				string tdRemark = TongdaoList[i].TongdaoName;
				foreach (SA sa in SawArray[i].SaList)
				{
					tdRemark += "\n" + sa.SAName + "：" + sa.StartValue + " - " + sa.EndValue;
				}
				myToolTip.SetToolTip(labels[i], tdRemark);
			}
			for (int i = TongdaoCount; i < 32; i++)
			{
				valueVScrollBars[i].Visible = false;
				labels[i].Visible = false;
				valueNumericUpDowns[i].Visible = false;
			}

			// 2.按需显示通道GroupBox
			tongdaoGroupBox1.Visible = TongdaoCount > 0;
			tongdaoGroupBox2.Visible = TongdaoCount > 16;
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
			TongdaoCount = int.Parse(countComboBox.SelectedItem.ToString());
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
		/// 辅助方法：供《WaySetForm》回调使用，用sawArray2的值，来填充本Form中的sawArray
		/// </summary>
		/// <param name="sawArray2">waySetForm中独立的SAWrapper数组</param>
		internal void SetSawArray(SAWrapper[] sawArray2)
		{
			this.SawArray = SAWrapper.DeepCopy(sawArray2);
		}

		#region 通道具体相关的滚动及值改动事件等

		/// <summary>
		/// 事件：鼠标进入《tdLabel》时，把焦点切换到其numericUpDown中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void label_MouseEnter(object sender, EventArgs e)
		{
			int labelIndex = MathHelper.GetIndexNum(((Label)sender).Name, -1);
			valueNumericUpDowns[labelIndex].Select();
		}

		/// <summary>
		/// 事件：点击《通道名（Labels）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void labels_Click(object sender, EventArgs e)
		{
			int tdIndex = MathHelper.GetIndexNum(((Label)sender).Name, -1);
			showWSForm(tdIndex);
		}

		/// <summary>
		/// 事件：鼠标进入vScrollBar时，把焦点切换到其numericUpDown中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void vScrollBar_MouseEnter(object sender, EventArgs e)
		{
			int labelIndex = MathHelper.GetIndexNum(((VScrollBar)sender).Name, -1);
			valueNumericUpDowns[labelIndex].Select();
		}

		/// <summary>
		///  事件：《通道值滚轴》值改变时的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueVScrollBar_ValueChanged(object sender, EventArgs e)
		{
			// 1.先找出对应vScrollBars的index 
			int tongdaoIndex = MathHelper.GetIndexNum(((VScrollBar)sender).Name, -1);

			//2.把滚动条的值赋给valueNumericUpDowns
			valueNumericUpDowns[tongdaoIndex].Value = 255 - valueVScrollBars[tongdaoIndex].Value;

			//3.取出recentStep,使用取出的index，给stepWrapper.TongdaoList[index]赋值；并检查是否实时生成数据进行操作
			changeCurrentValue(tongdaoIndex, Decimal.ToInt16(valueNumericUpDowns[tongdaoIndex].Value) );
		}

		/// <summary>
		/// 事件：《通道值NumericUpDown》鼠标中轴滚动时的操作：
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				// Dickov: 当Handled设为true时，不再触发父控件的相关操作，即屏蔽滚动事件
				hme.Handled = true;
			}
			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = valueNumericUpDowns[tdIndex].Value + valueNumericUpDowns[tdIndex].Increment;
				if (dd <= valueNumericUpDowns[tdIndex].Maximum)
				{
					valueNumericUpDowns[tdIndex].Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = valueNumericUpDowns[tdIndex].Value - valueNumericUpDowns[tdIndex].Increment;
				if (dd >= valueNumericUpDowns[tdIndex].Minimum)
				{
					valueNumericUpDowns[tdIndex].Value = dd;
				}
			}
		}

		/// <summary>
		/// 事件：调节或输入《通道值numericUpDown》的值后，1.调节通道值 2.调节tongdaoWrapper的相关值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			// 1. 找出对应的index
			int tongdaoIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);

			// 2.调整相应的vScrollBar的数值
			valueVScrollBars[tongdaoIndex].Value = 255 - Decimal.ToInt32(valueNumericUpDowns[tongdaoIndex].Value);

			//3.取出tongdaoIndex，给tongdaoList[index]赋值；并检查是否实时生成数据进行操作
			changeCurrentValue(tongdaoIndex, Decimal.ToInt32(valueNumericUpDowns[tongdaoIndex].Value));
		}

		/// <summary>
		///  辅助方法：改变值之后，更改对应的tongdaoList的值；并根据ifRealTime，决定是否实时调试灯具。
		/// </summary>
		/// <param name="tongdaoIndex"></param>
		private void changeCurrentValue(int tongdaoIndex, int tdValue)
		{
			// 1.设tongdaoWrapper的值
			TongdaoList[tongdaoIndex].CurrentValue = tdValue;
			//2.是否实时单灯单步
			if (isRealTime)
			{
				oneLightOneStep();
			}
		}

		#endregion

		#region 统一调整区域

		/// <summary>
		/// 事件：《初始通道地址NumericUpDown》鼠标中轴滚动时的操作：
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void firstTDNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = firstTDNumericUpDown.Value + firstTDNumericUpDown.Increment;
				if (dd <= firstTDNumericUpDown.Maximum)
				{
					firstTDNumericUpDown.Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = firstTDNumericUpDown.Value - firstTDNumericUpDown.Increment;
				if (dd >= firstTDNumericUpDown.Minimum)
				{
					firstTDNumericUpDown.Value = dd;
				}
			}
		}

		/// <summary>
		///  点击《设初始通道地址》：
		///  1.设局部变量的值将输入的值
		///  2.重写全部通道的label.Text
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setFirstTDButton_Click(object sender, EventArgs e)
		{
			firstTDValue = Decimal.ToInt16(firstTDNumericUpDown.Value);
			for (int i = 0; i < TongdaoCount; i++)
			{
				this.labels[i].Text = (firstTDValue + i) + "-  " + TongdaoList[i].TongdaoName;
			}
		}

		/// <summary>
		/// 事件：《统一通道值NumericUpDown》鼠标中轴滚动时的操作：
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = commonValueNumericUpDown.Value + commonValueNumericUpDown.Increment;
				if (dd <= commonValueNumericUpDown.Maximum)
				{
					commonValueNumericUpDown.Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = commonValueNumericUpDown.Value - commonValueNumericUpDown.Increment;
				if (dd >= commonValueNumericUpDown.Minimum)
				{
					commonValueNumericUpDown.Value = dd;
				}
			}
		}

		/// <summary>
		/// 事件：点击《统一通道值》
		/// --将当前所有通道值设为commonValueNumericUpDown 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueButton_Click(object sender, EventArgs e)
		{
			int commonValue = Decimal.ToInt16(commonValueNumericUpDown.Value);
			for (int i = 0; i < TongdaoList.Count; i++)
			{
				valueVScrollBars[i].Value = commonValue;
				TongdaoList[i].CurrentValue = commonValue;
				valueNumericUpDowns[i].Value = commonValue;
			}
		}

		/// <summary>
		///  点击《全部归零》后：所有TongdaoList的CurrentValue=0
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zeroButton_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < TongdaoList.Count; i++)
			{
					valueVScrollBars[i].Value = 255;					
					TongdaoList[i].CurrentValue = 0;
					valueNumericUpDowns[i].Value = 0;
			}						
		}

		/// <summary>
		/// 点击《设初始值》后：所有TongdaoList的CurrentValue=InitValue
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setInitButton_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < TongdaoList.Count; i++)
			{
				valueVScrollBars[i].Value = (255-TongdaoList[i].InitValue);					
				TongdaoList[i].CurrentValue = TongdaoList[i].InitValue;
				valueNumericUpDowns[i].Value = TongdaoList[i].InitValue;
			}		
		}

		/// <summary>
		/// 事件：点击《设当前通道值为初始值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setCurrentToInitButton_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show("确定把当前数值设为灯具通道的默认值吗？",
				"",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Question);
			if (dr == DialogResult.OK)
			{
				for (int i = 0; i < TongdaoList.Count; i++)
				{
					TongdaoList[i].InitValue = TongdaoList[i].CurrentValue;
				}
			}
		}

		#endregion		

		#region 实时调试相关按钮（连接设备+调试）

		/// <summary>
		///  事件：点击《刷新串口》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshButton_Click(object sender, EventArgs e)
		{
			refreshComList();
		}

		/// <summary>
		///  事件：更改《串口列表》选择项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{			
			connectButton.Enabled = true;
		}
				
		/// <summary>
		/// 事件：点击《连接设备|断开连接》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectButton_Click(object sender, EventArgs e)
		{
			// 如果还没连接，那就连接  -->连接状态下《选择串口》不可用
			if (!isConnect)
			{
				if(	player.ConnectDevice( comComboBox.Text  ))  //判断是否连接成功
				{
					setDMX512TestButtonsEnable(true);
					comComboBox.Enabled = false;
					connectButton.Text = "断开连接";					
					isConnect = true;
				}
				else
				{
					MessageBox.Show("串口：" + comComboBox.Text + " 连接失败") ;
				}
			}
			else //否则断开连接: --> 《选择串口》设为可用
			{
				setDMX512TestButtonsEnable(false);				
				player.CloseDevice();
				comComboBox.Enabled = true;
				connectButton.Text = "连接设备";
				isConnect = false;
			}
		}
			   
		/// <summary>
		/// 事件：勾选《实时调试》：将该全局变量设为勾选与否的值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void realtimeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			isRealTime = realtimeCheckBox.Checked;
		}

		/// <summary>
		/// 事件：点击《单灯单步》：调试当前灯具设置的数值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void oneStepButton_Click(object sender, EventArgs e)
		{
			oneLightOneStep();
		}	

		/// <summary>
		///  事件：点击《停止调试》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void endTestButton_Click(object sender, EventArgs e)
		{
			player.EndView();
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
		///  辅助方法：一次性配置DMX512调试按钮组是否可用
		/// </summary>
		/// <param name="visible"></param>
		private void setDMX512TestButtonsEnable(bool enable)
		{
			lightTestGroupBox.Enabled = enable;
			refreshButton.Enabled = !enable;
		}

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

		#endregion

		/// <summary>
		/// 辅助方法：在关闭WaySetForm时，清空waySetForm
		/// </summary>
		public void ClearWSForm()
		{
			waySetForm = null;
		}

		/// <summary>
		/// 辅助方法：要打开wsForm，都需由这个方法统一校验
		/// </summary>
		/// <param name="tdIndex"></param>
		private void showWSForm(int tdIndex)
		{
			if (waySetForm != null)
			{
				waySetForm.ChooseTD(tdIndex);
			}
			else
			{
				waySetForm = new WaySetForm(this, tdIndex);
				waySetForm.Show();
			}
		}


	}
}
