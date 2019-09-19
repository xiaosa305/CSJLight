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
using LightEditor.Common;
using LighEditor;

namespace LightEditor
{
	public partial class MainForm : Form
	{		
		public bool isGenerated = false;
		// 打开文件 或 保存文件 后，将isSaved设成true；这个吧变量决定是否填充*.ini内[data]内容
		public bool isSaved = false;
		private OneLightOneStep player; // 灯具测试的实例
		private int firstTDValue = 1;  // 初始通道地址值：最小为1,最大为512
		private bool isRealTime = false; //是否勾选“实时调试”
		private bool isConnect = false; // 辅助变量：是否连接设备	

		string comName;
		// 9.6 把图片路径放到软件中来
		private string savePath; 
		private string picDirectory;
		private string lightDirectory;

		public MainForm()
		{
			InitializeComponent();
			//this.skinEngine2.SkinFile = Application.StartupPath + @"\Vista2_color7.ssk";

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
				valueNumericUpDowns[i].MouseWheel += new System.Windows.Forms.MouseEventHandler(this.valueNumericUpDown_MouseWheel);
				valueVScrollBars[i].ValueChanged += new System.EventHandler(this.valueVScrollBar_ValueChanged);
			}
			firstTDNumericUpDown.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.firstTDNumericUpDown_MouseWheel);
			commonValueNumericUpDown.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.commonValueNumericUpDown_MouseWheel);

			#endregion

			refreshComList();
		}

		private void refreshComList() {

			player = OneLightOneStep.GetInstance();
			// 填充comComboBox
			IList<string> comList = player.GetDMX512DeviceList();
			comComboBox.Items.Clear();
			if (comList.Count > 0)
			{
				foreach (string com in comList)
				{
					comComboBox.Items.Add(com);
				}
				comComboBox.SelectedIndex = 0;
			}

		}

		/// <summary>
		///  事件：渲染Form时，选择皮肤
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{			
			IniFileAst iniFileAst = new IniFileAst(Application.StartupPath + @"\GlobalSet.ini");
			string skin = iniFileAst.ReadString("SkinSet", "skin", "");
			if (!String.IsNullOrEmpty(skin))
			{
				this.skinEngine2.SkinFile = Application.StartupPath + "\\" + skin;
			}

			// 9.6 图片加载使用当前软件所在文件夹
			savePath = IniFileAst.GetSavePath(Application.StartupPath);
			picDirectory = @savePath + @"\LightPic";
			this.openImageDialog.InitialDirectory = picDirectory;
			lightDirectory = @savePath + @"\LightLibrary";
			this.openFileDialog.InitialDirectory = lightDirectory;			
		}

		

		/// <summary>
		/// 事件：点击《新建灯具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newLightButton_Click(object sender, EventArgs e)
		{
			if (editGroupBox.Visible)
			{
				DialogResult dr = MessageBox.Show(
					"确认要新建吗？",
					"",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question
					);
				if (dr == DialogResult.OK)
				{
					isSaved = false;
					isGenerated = false;
					countComboBox.SelectedIndex = -1;
					tongdaoList = null;
					nameTextBox.Enabled = true;
					nameTextBox.Text = "";
					typeTextBox.Enabled = true;
					typeTextBox.Text = "";
					tongdaoEditButton.Hide();
					generateButton.Show();
					tongdaoGroupBox1.Hide();
					tongdaoGroupBox2.Hide();
					picTextBox.Text = "";
					openPictureBox.Image = null;
					firstTDNumericUpDown.Value = 1;					
				}
			}
			else {
				editGroupBox.Visible = true;
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
			openFileDialog.ShowDialog();
		}


		/// <summary>
		///  事件：在《打开灯具》对话框内选择文件，并点击确认时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			string iniFileName = openFileDialog.FileName;	
			// 简单读取文本文件-->打开ini文件
			
			FileStream file = new FileStream(iniFileName, FileMode.Open);
			// 可指定编码，默认的用Default，它会读取系统的编码（ANSI-->针对不同地区的系统使用不同编码，中文就是GBK）
			StreamReader reader = new StreamReader(file, Encoding.UTF8);
			string s = "";
			ArrayList lineList = new ArrayList();
			int lineCount = 0;
			while ((s = reader.ReadLine()) != null)
			{
				lineList.Add(s);
				lineCount++;
			}

			if (lineCount < 5) {
				MessageBox.Show("打开的ini文件格式有误");
				return;
			}

			this.isGenerated = true;
			this.isSaved = true;

			this.typeTextBox.Enabled = false;
			this.typeTextBox.Text = lineList[1].ToString().Substring(5);
			this.picTextBox.Enabled = false;
			string imagePath = lineList[2].ToString().Substring(4);
			if (imagePath != null && !imagePath.Trim().Equals(""))
			{
				this.setImage( picDirectory +"\\" + imagePath); 
			}
			

			string selectItem = lineList[3].ToString().Substring(6);//第七个字符开始截取 
			// 此处请注意：并不是用SelectedText，而是直接设Text
			this.countComboBox.Text = selectItem;
			this.tongdaoCount = int.Parse(selectItem);
			this.nameTextBox.Enabled = false;
			this.nameTextBox.Text = lineList[4].ToString().Substring(5);
			this.editGroupBox.Show();								

			if (lineCount > 5) {
				// 先通过tongdaoCount2,将ini已有的数据，添加进tongdaoList中
				int tongdaoCount2 = (lineCount - 6) / 3;
				tongdaoList = new List<TongdaoWrapper>();
				for (int i = 0; i < tongdaoCount2; i++)
				{
					string tongdaoName = lineList[3 * i + 6].ToString().Substring(4);
					int initNum = int.Parse(lineList[3 * i + 7].ToString().Substring(4));
					int address = int.Parse(lineList[3 * i + 8].ToString().Substring(4));
					tongdaoList.Add(new TongdaoWrapper(tongdaoName, initNum, address,initNum));
				}
				// 当小于设定值时，应该输出错误信息，并调用generateTongdaoList()方法：多出的通道设初值
				if (tongdaoCount2 < tongdaoCount)
				{
					MessageBox.Show("记录的tongdao信息小于所需信息");
					this.generateTongdaoList();
				}				
						
			}
			this.NewShowVScrollBars();
			this.showTongdaoButton(true);
			this.connectPanel.Show();
			
			reader.Close();
			file.Close();			

		}

		/// <summary>
		///  通过改变通道List，来重新渲染tdPanel（注意：tongdaoCount不会发生变化）
		/// </summary>
		/// <param name="tongdaoList"></param>
		internal void SetTongdaoList(List<TongdaoWrapper> tongdaoList)
		{
			this.tongdaoList = tongdaoList;
			this.NewShowVScrollBars();
		}

		private void saveLightButton_Click(object sender, EventArgs e)
		{
			//如果检查通道为false(未选择)，此时!false=true,方法不再运行。 
			if (!CheckCountComboBox())
			{
				return;
			}

			string name = nameTextBox.Text;
			string type = typeTextBox.Text;

			if (String.IsNullOrEmpty(name)) {
				MessageBox.Show("请输入厂家名。");
				return;
			}
			if (!FileAst.CheckFileName(name)) {
				MessageBox.Show("厂家名含有非法字符，无法保存。");
				return;
			}


			if (String.IsNullOrEmpty(type))
			{
				MessageBox.Show("请输入型号名");
				return;
			}
			if (!FileAst.CheckFileName(type))
			{
				MessageBox.Show("型号名含有非法字符，无法保存。");
				return;
			}

			string pic = picTextBox.Text;
			int count = int.Parse(countComboBox.SelectedItem.ToString());

			DirectoryInfo di = new DirectoryInfo(lightDirectory+ "\\" + name);
			if (!di.Exists)
			{
				di.Create();
			}

			string fileName = lightDirectory + "\\" + name + "\\" + type;
					
			using (StreamWriter iniWriter = new StreamWriter(fileName + ".ini"))
			{
				// 写[set]的数据
				iniWriter.WriteLine("[set]");
				iniWriter.WriteLine("type=" + type);
				iniWriter.WriteLine("pic=" + pic);
				iniWriter.WriteLine("count=" + count);
				iniWriter.WriteLine("name=" + name);

				//写[Data]数据;先判断是否已经点击生成按钮（已打开也应将此值设为true）
				if (isGenerated)
				{
					iniWriter.WriteLine("[Data]");
					for (int i = 0; i < tongdaoList.Count; i++)
					{
						// 未满10的前面加0
						string index = (i < 9) ? ("0" + (i+1) ) : ("" + (i+1) );
						iniWriter.WriteLine(index + "A=" + tongdaoList[i].TongdaoName);
						iniWriter.WriteLine(index + "B=" + tongdaoList[i].InitValue);
						iniWriter.WriteLine(index + "C=" + tongdaoList[i].Address);
					}
				}
			}
			MessageBox.Show("已成功保存。");

		}

		private void exitButton_Click(object sender, EventArgs e)
		{
			System.Environment.Exit(0);			
		}

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
		/// 通过图片路径，改变image相关的两个内容
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
		///  辅助方法：检查通道数下拉框是否已经被勾选，1若是则设置tongdaoCount ; 2否则返回false
		/// </summary>
		/// <returns></returns>
		private bool CheckCountComboBox()
		{
			if (countComboBox.Text == "" || countComboBox.SelectedIndex == -1)
			{
				MessageBox.Show("请选择通道数");
				return false;
			}
			else {
				tongdaoCount = int.Parse(countComboBox.SelectedItem.ToString());
				return true;
			}
			
		}

		/// <summary>
		///  点击生成按钮后的操作：
		///  1.检查通道数 ；
		///  2.检查若通过，则生成默认通道列表		
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void generateButton_Click(object sender, EventArgs e)
		{
			//如果检查通道为false(未选择)，此时!false=true,方法不再运行。 
			if (!CheckCountComboBox())
			{
				return;
			}
			
			isGenerated = true;
			showTongdaoButton(true);

			generateTongdaoList();
			NewShowVScrollBars();			
		}

		/// <summary>
		///  显示通道编辑按钮(true)或生成(false)按钮（二选一）
		/// </summary>
		/// <param name="showEditButton"></param>
		private void showTongdaoButton(bool showEditButton) {
			//显示《通道编辑》按钮
			if (showEditButton)
			{
				tongdaoEditButton.Show();
				generateButton.Hide();
			}//显示《生成》按钮
			else {
				tongdaoEditButton.Hide();
				generateButton.Show();
			}				
		}
				
		/// <summary>
		///  (新）辅助方法：
		///  1.将tongdaoList渲染进下拉条组中
		///  2.先隐藏所有，再显示当前数量的下拉条
		///  3.根据通道数，显示相应的GroupBox
		/// </summary> 
		private void NewShowVScrollBars()
		{
			// 1.tongdaoList的数据渲染进各个通道显示项(label+valueLabel+vScrollBar)中
			generateVScrollBars();
			// 2.显示所需通道（groupBox+通道）
			showNeedTDs();			
		}

		/// <summary>
		///  显示
		///  1.tongdaoCount数量的通道(nameLabel+vscrollbar+valueLabel)
		///  2.所需的通道groupBox
		/// </summary>
		private void showNeedTDs() {

			// 1.显示tongdaoCount数量的通道
			for (int i = tongdaoCount; i < 32; i++)
			{
				valueVScrollBars[i].Visible = false;
				labels[i].Visible = false;
				valueNumericUpDowns[i].Visible = false;
			}
			for (int i = 0; i < tongdaoCount; i++)
			{
				valueVScrollBars[i].Show();
				labels[i].Show();
				valueNumericUpDowns[i].Show();
			}

			// 2.按需显示通道GroupBox
			if (tongdaoCount > 0 && tongdaoCount <= 16)
			{
				tongdaoGroupBox1.Show();
				tongdaoGroupBox2.Hide();
			}
			else if (tongdaoCount > 16 && tongdaoCount <= 32)
			{
				tongdaoGroupBox1.Show();
				tongdaoGroupBox2.Show();
			}
			else if (tongdaoCount == 0)
			{
				tongdaoGroupBox1.Hide();
				tongdaoGroupBox2.Hide();
			}
			else
			{
				MessageBox.Show("Dickov:TongdaoCount错误！");
			}
		}

		/// <summary>
		/// 辅助方法：用于生成默认的tongdaoList（由tongdaoCount决定）
		/// 1.若之前列表为空，则从头开始添加列表数据
		/// 2.若之前列表已有数据，
		///   ①当tongdaoCount > tongdaoList.Count，添加新的数据到列表中去.
		///   ②当tongdaoCount <= tongdaoList.Count，不进行任何操作(数据仍放在tongdaoList中）
		/// </summary>
		private void generateTongdaoList()
		{
			if (tongdaoList == null || tongdaoList.Count == 0)
			{
				tongdaoList = new List<TongdaoWrapper>();
				for (int i = 0; i < tongdaoCount; i++)
				{
					int j = i + 1;
					tongdaoList.Add(new TongdaoWrapper("通道" + j, 0, j ,0));
				}
			}
			else {
				if (tongdaoCount > tongdaoList.Count) {
					for (int i = tongdaoList.Count; i < tongdaoCount; i++)
					{
						int j = i + 1;
						tongdaoList.Add(new TongdaoWrapper("通道" + j, 0, j, 0));
					}
				}
			}
		}
				
		/// <summary>
		///  通道数下拉框更改后，进行的操作：
		///  1.修改tongdaoCount的值为选中值；
		///  2.显示《生成》按钮，隐藏《通道编辑》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void countComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isGenerated) {
				tongdaoCount = int.Parse(countComboBox.SelectedItem.ToString());
				showTongdaoButton(false);				
			}				
		}

		/// <summary>
		///  通道编辑按钮点击后的操作：
		///  1. 生成一个新的的WaySetForm，并将tongdaoList的数据渲染进这个form中
		///  2.显示这个form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tongdaoEditButton_Click(object sender, EventArgs e)
		{				
			WaySetForm wsf = new WaySetForm(this);
			wsf.ShowDialog();				
		}
		
		/// <summary>
		/// 辅助方法：通过注入值后的tongdaoList,为vScrollBars赋值；
		/// </summary>
		/// <param name="tongdaoCount"></param>
		public void generateVScrollBars()
		{				
			for (int i = 0; i < tongdaoCount; i++)
			{				
				this.labels[i].Text = (firstTDValue+i )+ "-  " + tongdaoList[i].TongdaoName;
				myToolTip.SetToolTip(labels[i], tongdaoList[i].TongdaoName);
				this.valueVScrollBars[i].Value = 255- tongdaoList[i].CurrentValue;
				this.valueNumericUpDowns[i].Value =   tongdaoList[i].CurrentValue;
			}
		}	


		/// <summary>
		///  滚轴值改变时的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueVScrollBar_ValueChanged(object sender, EventArgs e)
		{
			// 1.先找出对应vScrollBars的index 
			int tongdaoIndex = MathAst.getIndexNum(((VScrollBar)sender).Name, -1);

			//2.把滚动条的值赋给valueNumericUpDowns
			valueNumericUpDowns[tongdaoIndex].Value = 255 - valueVScrollBars[tongdaoIndex].Value;

			//3.取出recentStep,使用取出的index，给stepWrapper.TongdaoList[index]赋值；并检查是否实时生成数据进行操作
			changeCurrentValue(tongdaoIndex, Decimal.ToInt16(valueNumericUpDowns[tongdaoIndex].Value) );
		}

		/// <summary>
		/// 辅助方法:鼠标进入vScrollBar时，把焦点切换到其numericUpDown中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void vScrollBar_MouseEnter(object sender, EventArgs e)
		{
			int labelIndex = MathAst.getIndexNum(((VScrollBar)sender).Name, -1);
			valueNumericUpDowns[labelIndex].Select();
		}

		/// <summary>
		/// 辅助方法:鼠标进入label时，把焦点切换到其numericUpDown中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void label_MouseEnter(object sender, EventArgs e)
		{
			int labelIndex = MathAst.getIndexNum(((Label)sender).Name, -1);
			valueNumericUpDowns[labelIndex].Select();
		}

		/// <summary>
		/// 调节或输入numericUpDown的值后，1.调节通道值 2.调节tongdaoWrapper的相关值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			// 1. 找出对应的index
			int tongdaoIndex = MathAst.getIndexNum(((NumericUpDown)sender).Name, -1);

			// 2.调整相应的vScrollBar的数值
			valueVScrollBars[tongdaoIndex].Value = 255 - Decimal.ToInt32(valueNumericUpDowns[tongdaoIndex].Value);

			//3.取出tongdaoIndex，给tongdaoList[index]赋值；并检查是否实时生成数据进行操作
			changeCurrentValue(tongdaoIndex , Decimal.ToInt32(valueNumericUpDowns[tongdaoIndex].Value));
		}


		/// <summary>
		///  改变值之后，更改对应的tongdaoList的值；并根据ifRealTime，决定是否实时调试灯具。
		/// </summary>
		/// <param name="tongdaoIndex"></param>
		private void changeCurrentValue(int tongdaoIndex,int tdValue)
		{
			// 1.设tongdaoWrapper的值
			tongdaoList[tongdaoIndex].CurrentValue = tdValue;
			//2.是否实时单灯单步
			if (isRealTime)
			{
				oneLightOneStep();
			}
		}

		/// <summary>
		/// NumericUpDown鼠标中轴滚动时的操作：
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			int tdIndex = MathAst.getIndexNum(((NumericUpDown)sender).Name, -1);			
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
		/// 初始通道地址NumericUpDown鼠标中轴滚动时的操作：
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
		/// 统一通道值NumericUpDown鼠标中轴滚动时的操作：
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
		///  点击《全部归零》后：所有TongdaoList的CurrentValue=0
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zeroButton_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show("确定把所有数值归零吗？",
				"",
				MessageBoxButtons.OKCancel,  
				MessageBoxIcon.Question);  
			if (dr == DialogResult.OK)
			{
				for (int i = 0; i < tongdaoList.Count; i++)
				{
					valueVScrollBars[i].Value = 255;					
					tongdaoList[i].CurrentValue = 0;
					valueNumericUpDowns[i].Value = 0;
				}
			}			
		}

		/// <summary>
		/// 点击《设初始值》后：所有TongdaoList的CurrentValue=InitValue
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setInitButton_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show("确定把所有数值设为默认值吗？",
				"",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Question);
			if (dr == DialogResult.OK)
			{
				for (int i = 0; i < tongdaoList.Count; i++)
				{
					valueVScrollBars[i].Value = (255-tongdaoList[i].InitValue);					
					tongdaoList[i].CurrentValue = tongdaoList[i].InitValue;
					valueNumericUpDowns[i].Value = tongdaoList[i].InitValue;
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
			for (int i = 0; i < tongdaoCount; i++)
			{
				this.labels[i].Text = (firstTDValue + i) + "-  " + tongdaoList[i].TongdaoName;
			}
		}


		/// <summary>
		/// 事件：点击《选择串口》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chooseComButton_Click(object sender, EventArgs e)
		{
			comName = comComboBox.Text;
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
				if(	player.ConnectDevice(comName))  //判断是否连接成功
				{
					setDMX512TestButtonsEnable(true);					
					connectButton.Text = "断开连接";					
					isConnect = true;
				}
				else
				{
					MessageBox.Show("串口：" + comName + " 连接失败") ;
				}
			}
			else //否则断开连接: --> 《选择串口》设为可用
			{
				setDMX512TestButtonsEnable(false);				
				player.CloseDevice();
				connectButton.Text = "连接设备";
				isConnect = false;
			}
		}

		/// <summary>
		///  辅助方法：一次性配置DMX512调试按钮组可见与否
		/// </summary>
		/// <param name="visible"></param>
		private void setDMX512TestButtonsEnable(bool visible)
		{
			lightTestGroupBox.Visible = visible;
			chooseComButton.Enabled = !visible;
			refreshButton.Enabled = !visible;
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
		private void testButton_Click(object sender, EventArgs e)
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
		///  辅助方法：单灯单步的操作
		/// </summary>
		private void oneLightOneStep()
		{
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
		/// 事件：点击《统一通道值》
		/// --将当前所有通道值设为commonValueNumericUpDown 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueButton_Click(object sender, EventArgs e)
		{
			int commonValue = Decimal.ToInt16(commonValueNumericUpDown.Value);
			for (int i = 0; i < tongdaoList.Count; i++)
			{
				valueVScrollBars[i].Value = commonValue;
				tongdaoList[i].CurrentValue = commonValue;
				valueNumericUpDowns[i].Value = commonValue;
			}
		}

		/// <summary>
		/// 事件：点击《设当前通道值为初始值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setCurrentToInitButton_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < tongdaoList.Count; i++)
			{
				tongdaoList[i].InitValue = tongdaoList[i].CurrentValue;
			}
		}

		/// <summary>
		/// 事件：点击《右上角？》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show("灯具的厂家名及型号名都不可使用\\、/、:、*、?、\"、<、>、| 等字符，否则操作系统(windows)无法保存，会出现错误。");
			e.Cancel = true;
		}


		/// <summary>
		///  事件：点击《刷新》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshButton_Click(object sender, EventArgs e)
		{
			refreshComList();
		}
	}
}
