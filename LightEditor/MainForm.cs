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


namespace LightEditor
{
	public partial class MainForm : Form
	{
		private WaySetForm wsForm;

		public MainForm()
		{
			InitializeComponent();
			
			this.skinEngine2.SkinFile = Application.StartupPath + @"\Vista2_color7.ssk";
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			//skinEngine1.SkinFile = @"C:\Users\Dickov\Desktop\皮肤控件\皮肤\MacOS\MacOS.ssk";
			// 先将几个vScrollBar加入数组吧;

			//写几个辅助方法：可自动生成语句
			//for (int i = 0; i < 32; i++)Console.WriteLine("vScrollBar[" + i + "]=vScrollBar" + (i + 1) + ";");
			//for (int i = 0; i < 32; i++) Console.WriteLine("valueLabels[" + i + "]=valueLabel" + (i + 1) + ";");
			//for (int i = 0; i < 32; i++) Console.WriteLine("labels[" + i + "]=label" + (i + 1) + ";");

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
			//countComboBox.SelectedItem = 1;

		}

		private void openComButton_Click(object sender, EventArgs e)
		{
			// 测试锁定当前窗口
			//MessageBox.Show("锁定当前窗口,10秒后解锁。");
			//this.Enabled = false;
			//Thread.Sleep(10000);
			//this.Enabled = true;
		}

		private void NewLightButton_Click(object sender, EventArgs e)
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
					dataWrappers = null;
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
			//MessageBox.Show("成功打开工程文件:" + openFileDialog.FileName);
			string fsFileName = openFileDialog.FileName;
			string iniFileName = fsFileName.Substring(0, fsFileName.LastIndexOf(".FS"));
			iniFileName += ".ini";

			// 简单读取文本文件-->打开ini文件
			try
			{
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

				//MessageBox.Show("TotalLineCount:" + lineCount);
				if (lineCount < 5) {
					MessageBox.Show("打开的ini文件格式有误");
					return;
				}

				this.isGenerated = true;
				this.isSaved = true;

				this.typeTextBox.Enabled = false;
				this.typeTextBox.Text = lineList[1].ToString().Substring(5);
				this.picTextBox.Enabled = false;
				this.picTextBox.Text = lineList[2].ToString().Substring(4);
				string selectItem = lineList[3].ToString().Substring(6);//第七个字符开始截取 
				// 此处请注意：并不是用SelectedText，而是直接设Text
				this.countComboBox.Text = selectItem;
				this.tongdaoCount = int.Parse(selectItem);
				this.nameTextBox.Enabled = false;
				this.nameTextBox.Text = lineList[4].ToString().Substring(5);
				this.editGroupBox.Show();
								

				if (lineCount > 5) {
					int tongdaoCount2 = (lineCount - 6) / 3;
					if (tongdaoCount2 != tongdaoCount) {
						MessageBox.Show("打开的ini文件的count值与实际值不符合");
					}
					dataWrappers = new TongdaoWrapper[tongdaoCount2];
					//MessageBox.Show("共有"+tongdaoCount+"个通道");
					for (int i=0; i < tongdaoCount2; i++) {
						string tongdaoName = lineList[3 * i + 6].ToString().Substring(4);
						int initNum = int.Parse(lineList[3 * i + 7].ToString().Substring(4));
						int address = int.Parse(lineList[3 * i + 8].ToString().Substring(4));
						//MessageBox.Show(tongdaoName+" | "+initNum+" | "+address);
						dataWrappers[i] = new TongdaoWrapper(tongdaoName, initNum, address);
					}
					this.ShowVScrollBars();
				}
				this.generateButton.Hide();
				this.tongdaoEditButton.Show();
			}
			catch (FileNotFoundException fnfe) {
				MessageBox.Show("未找到相关的ini文件");
			}	

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

			// 记得要关闭输出流；如果没有关闭或Flush()，将无法写入
			// 测试代码
			//StreamWriter writer = new StreamWriter(fileName + ".FS");
			//writer.WriteLine("12");
			//writer.Flush();

			using (StreamWriter fsWriter = new StreamWriter(fileName + ".FS"))
			{
				fsWriter.WriteLine("12");
			}
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
					for (int i = 0; i < dataWrappers.Length; i++)
					{
						// 未满10的前面加0
						string index = (i < 9) ? ("0" + (i+1) ) : ("" + (i+1) );
						iniWriter.WriteLine(index + "A=" + dataWrappers[i].TongdaoName);
						iniWriter.WriteLine(index + "B=" + dataWrappers[i].InitNum);
						iniWriter.WriteLine(index + "C=" + dataWrappers[i].Address);
					}
				}

			}
			MessageBox.Show("已成功保存。");

		}

		private void ExitButton_Click(object sender, EventArgs e)
		{

			System.Environment.Exit(0);
			
		}

		private void pictureBox_Click(object sender, EventArgs e)
		{
			openImageDialog.ShowDialog();

		}

		private void openImageDialog_FileOk(object sender, CancelEventArgs e) {

			string fileName = openImageDialog.FileName;
			string shortFileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
			// 从本地目录加载图片			
			openPictureBox.Image = Image.FromFile(fileName);

			// 从网络加载图片（测试用）
			//fileName = @"https://www.zzhaoxing.com/cwzz/html/images/y2.jpg";
			//try
			//{
			//	openPictureBox.Image = Image.FromStream(
			//		System.Net.WebRequest.Create(fileName).GetResponse().GetResponseStream());
			//}
			//catch (Exception ex) {
			//	MessageBox.Show("无法加载图片，可能是图片地址有误");
			//}

			picTextBox.Text = shortFileName;
		}
				
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

		private void generateButton_Click(object sender, EventArgs e)
		{
			//如果检查通道为false(未选择)，此时!false=true,方法不再运行。 
			if (!CheckCountComboBox())
			{
				return;
			}

			isGenerated = true;
			ShowVScrollBars();
			//显示按钮
			tongdaoEditButton.Show();
			generateButton.Hide();
		}
		

		// 辅助方法：用来显示不同的通道页；通道调整条，同时也使其对应的两个标签可见
		private void ShowVScrollBars() {			
			
			//dataWrappers = new DataWrapper[tongdaoCount];
			//类单例模式:当Form还未生成（或被右上角点击后默认Dispose()）时，生成一个，否则用旧的 
			if (wsForm == null || wsForm.IsDisposed)
			{
				wsForm = new WaySetForm(this);
			}

			/// 1.如果dataWrapper尚未有值(新建灯),则传入tongdaoCount,由wsForm生成默认值，再回调生成dataWrappers
			/// 2.如果dataWrapper已经有值(打开灯.ini),则将该数据填进wsForm的textBoxes
			wsForm.setTongdaoCount(tongdaoCount);
			// 由dataWrappers生成vScrollBars
			generateVScrollBars(tongdaoCount);

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

			// 切换是否显示通道滚动条页
			tongdaoGroupBox1.Show();
			if (tongdaoCount > 16)
			{
				tongdaoGroupBox2.Show();
			}
			else {
				tongdaoGroupBox2.Hide();
			}
		}
		
		private void countComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isGenerated) {
				ShowVScrollBars();
			}				
		}

		private void tongdaoEditButton_Click(object sender, EventArgs e)
		{
			// 如果资源是第一次生成：应该设一些默认值
			//wsForm.setTongdaoCount(int.Parse(countComboBox.SelectedItem.ToString()));

			// 如果不是第一次生成： -->读取ini内配置；若加了通道，则追加到顺延的textBox中
			if (!CheckCountComboBox()) {
				return;
			}
			
			wsForm.setTongdaoCount(int.Parse(countComboBox.SelectedItem.ToString()));
			wsForm.Show();
			wsForm.Activate();
				
				
		}

		//TODO：通过注入值后的dataWrappers,为vScrollBars赋值；
		public void generateVScrollBars(int tongdaoCount)
		{
			
			for (int i = 0; i < tongdaoCount; i++)
			{
				TongdaoWrapper dataWrapper = dataWrappers[i];
				this.labels[i].Text = dataWrapper.TongdaoName;
				this.valueLabels[i].Text = dataWrapper.InitNum.ToString();
				this.vScrollBars[i].Value = dataWrapper.InitNum;
			}
		}

		private void kg1Button_Click(object sender, EventArgs e)
		{
			MessageBox.Show("开光1");
		}

		private void gg1Button_Click(object sender, EventArgs e)
		{
			MessageBox.Show("关光1");
		}

		private void kg2Button_Click(object sender, EventArgs e)
		{
			MessageBox.Show("开光2");
		}

		private void gg2Button_Click(object sender, EventArgs e)
		{
			MessageBox.Show("关光2");
		}

		private void redButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("红");
		}

		private void greenButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("绿");
		}

		private void blueButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("蓝");
		}

		private void zeroButton_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show("确定把所有数值归零吗？",
				"",
				MessageBoxButtons.OKCancel,  
				MessageBoxIcon.Question);  
			if (dr == DialogResult.OK)
			{
				for (int i = 0; i < 32; i++)
				{
					vScrollBars[i].Value = 0;
					valueLabels[i].Text = "0";
				}
			}
			//MessageBox.Show("归零");
		}

		private void xzButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("X轴中位");
		}

		private void yzButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Y轴中位");
		}
		
		private void changePageButton_Click(object sender, EventArgs e)
		{

			//if (tongdaoGroupBox1.Visible == true)
			//{
			//	MessageBox.Show("tongdao1可见");
			//	tongdaoGroupBox1.Hide();
			//	tongdaoGroupBox2.Show();
			//}
			//else {
			//	MessageBox.Show("tongdao1不可见");
			//	tongdaoGroupBox2.Hide();
			//	tongdaoGroupBox1.Show();
			//}


		}
		
		// 通用的方法：通过sender获取被滑动的滚动条，然后给它的值标签赋值
		private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			//new method：
			string vScrollBarName =((VScrollBar)sender).Name;
			// 方法：替换掉非数字的字符串;(另一个方法，截取“_”之前,“Bar”之后的字符串也可以)
			string labelIndexStr = System.Text.RegularExpressions.Regex.Replace(vScrollBarName, @"[^0-9]+", "");
			// 处理labelIndex,将取出的数字-1
			int labelIndex = int.Parse(labelIndexStr) - 1;
			valueLabels[labelIndex].Text = vScrollBars[labelIndex].Value.ToString();

			// old method：通过反射获取当前方法名称(类似于vScrollBar1_Scroll)；然后再一个个对应
			//ChangeValueLabel(System.Reflection.MethodBase.GetCurrentMethod().Name);
		}

		
	}
}
