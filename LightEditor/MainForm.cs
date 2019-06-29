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
using LightEditor.Common;

namespace LightEditor
{
	public partial class MainForm : Form
	{		
		public bool isGenerated = false;
		// 打开文件 或 保存文件 后，将isSaved设成true；这个吧变量决定是否填充*.ini内[data]内容
		public bool isSaved = false;	

		public MainForm()
		{
			InitializeComponent();			
			//this.skinEngine2.SkinFile = Application.StartupPath + @"\Vista2_color7.ssk";
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			skinEngine2.SkinFile = @"C:\Users\Dickov\Desktop\皮肤控件\皮肤\MacOS\MacOS.ssk";
			
			#region 初始化几个数组

			vScrollBars[0] = vScrollBar1;
			vScrollBars[1] = vScrollBar2;
			vScrollBars[2] = vScrollBar3;
			vScrollBars[3] = vScrollBar4;
			vScrollBars[4] = vScrollBar5;
			vScrollBars[5] = vScrollBar6;
			vScrollBars[6] = vScrollBar7;
			vScrollBars[7] = vScrollBar8;
			vScrollBars[8] = vScrollBar9;
			vScrollBars[9] = vScrollBar10;
			vScrollBars[10] = vScrollBar11;
			vScrollBars[11] = vScrollBar12;
			vScrollBars[12] = vScrollBar13;
			vScrollBars[13] = vScrollBar14;
			vScrollBars[14] = vScrollBar15;
			vScrollBars[15] = vScrollBar16;
			vScrollBars[16] = vScrollBar17;
			vScrollBars[17] = vScrollBar18;
			vScrollBars[18] = vScrollBar19;
			vScrollBars[19] = vScrollBar20;
			vScrollBars[20] = vScrollBar21;
			vScrollBars[21] = vScrollBar22;
			vScrollBars[22] = vScrollBar23;
			vScrollBars[23] = vScrollBar24;
			vScrollBars[24] = vScrollBar25;
			vScrollBars[25] = vScrollBar26;
			vScrollBars[26] = vScrollBar27;
			vScrollBars[27] = vScrollBar28;
			vScrollBars[28] = vScrollBar29;
			vScrollBars[29] = vScrollBar30;
			vScrollBars[30] = vScrollBar31;
			vScrollBars[31] = vScrollBar32;

			valueLabels[0] = valueLabel1;
			valueLabels[1] = valueLabel2;
			valueLabels[2] = valueLabel3;
			valueLabels[3] = valueLabel4;
			valueLabels[4] = valueLabel5;
			valueLabels[5] = valueLabel6;
			valueLabels[6] = valueLabel7;
			valueLabels[7] = valueLabel8;
			valueLabels[8] = valueLabel9;
			valueLabels[9] = valueLabel10;
			valueLabels[10] = valueLabel11;
			valueLabels[11] = valueLabel12;
			valueLabels[12] = valueLabel13;
			valueLabels[13] = valueLabel14;
			valueLabels[14] = valueLabel15;
			valueLabels[15] = valueLabel16;
			valueLabels[16] = valueLabel17;
			valueLabels[17] = valueLabel18;
			valueLabels[18] = valueLabel19;
			valueLabels[19] = valueLabel20;
			valueLabels[20] = valueLabel21;
			valueLabels[21] = valueLabel22;
			valueLabels[22] = valueLabel23;
			valueLabels[23] = valueLabel24;
			valueLabels[24] = valueLabel25;
			valueLabels[25] = valueLabel26;
			valueLabels[26] = valueLabel27;
			valueLabels[27] = valueLabel28;
			valueLabels[28] = valueLabel29;
			valueLabels[29] = valueLabel30;
			valueLabels[30] = valueLabel31;
			valueLabels[31] = valueLabel32;

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

			for (int i = 1; i <= 32; i++)
			{
				countComboBox.Items.Add(i);
			}

			#endregion

		}

		private void openComButton_Click(object sender, EventArgs e)
		{
			// 测试锁定当前窗口
			//MessageBox.Show("锁定当前窗口,10秒后解锁。");
			//this.Enabled = false;
			//Thread.Sleep(10000);
			//this.Enabled = true;
		}

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
					isSaved = false;
					isGenerated = false;
				}
			}
			else {
				editGroupBox.Visible = true;
			}
		}

		private void openLightButton_Click(object sender, EventArgs e)
		{
			this.openFileDialog.ShowDialog();
		}

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
				this.setImage("C:\\Temp\\LightPic\\" + imagePath); 
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
				MessageBox.Show("请输入厂家名");
				return;
			}
			if (String.IsNullOrEmpty(type))
			{
				MessageBox.Show("请输入型号名");
				return;
			}

			string pic = picTextBox.Text;
			int count = int.Parse(countComboBox.SelectedItem.ToString());

			DirectoryInfo di = new DirectoryInfo("C:\\Temp\\LightLibrary\\" + name);
			if (!di.Exists)
			{
				di.Create();
			}

			string fileName = "C:\\Temp\\LightLibrary\\" + name + "\\" + type;
					
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
				vScrollBars[i].Visible = false;
				labels[i].Visible = false;
				valueLabels[i].Visible = false;
			}
			for (int i = 0; i < tongdaoCount; i++)
			{
				vScrollBars[i].Show();
				labels[i].Show();
				valueLabels[i].Show();
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
				this.labels[i].Text = tongdaoList[i].TongdaoName;
				this.valueLabels[i].Text = tongdaoList[i].CurrentValue.ToString();
				this.vScrollBars[i].Value = tongdaoList[i].CurrentValue;
			}
		}

		/// <summary>
		///  通用的方法：通过sender获取被滑动的滚动条，然后给它的值标签赋值，并更改相应的tongdaoList值
		/// </summary>
		private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			int labelIndex = MathAst.getIndexNum( ((VScrollBar)sender).Name ,  -1 );
			valueLabels[labelIndex].Text = vScrollBars[labelIndex].Value.ToString();

			tongdaoList[labelIndex].CurrentValue = vScrollBars[labelIndex].Value;
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
					vScrollBars[i].Value = 0;
					valueLabels[i].Text = "0";
					tongdaoList[i].CurrentValue = 0;
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
					vScrollBars[i].Value = tongdaoList[i].InitValue;
					valueLabels[i].Text = tongdaoList[i].InitValue.ToString();
					tongdaoList[i].CurrentValue = tongdaoList[i].InitValue;
				}
			}
		}
	}
}
