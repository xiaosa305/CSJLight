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
using Microsoft.VisualBasic;
using System.Collections;
using System.Data.SQLite;
using DMX512;
using LightController.Ast;

namespace LightController
{
	public partial class MainForm : Form
	{
		// 只能有一个lightsForm，在点击编辑灯具时（未生成过或已被销毁）新建，或在Hide时显示
		private LightsForm lightsForm; 
		private List<LightAst> lightAstList;

		// 辅助的变量
		// private string projectName; 
		// 点击新建后，点击保存前，这个属性是true；如果是使用打开文件或已经点击了保存按钮，则设为false
		// private bool isNew = true;
		// 点击保存后|刚打开一个文件时，这个属性就设为true;如果对内容稍有变动，则设为false
		private bool isSaved = false;
		public string dbFile;

		public DBWrapper allData;
		List<DB_Light> lightList = new List<DB_Light>();
		List<DB_StepCount> stepCountList = new List<DB_StepCount>();
		List<DB_Value> valueList = new List<DB_Value>();

		// 数据库连接
		private LightDAO lightDAO;
		private StepCountDAO stepCountDAO;
		private ValueDAO valueDAO;
		private bool ifEncrypt = false;

		// 辅助的灯具变量：记录所有（灯具）的（所有场景和模式）的 每一个 步（通道列表）
		private List<LightData> lightDataList = new List<LightData>();
		
		private int selectedLightIndex ; 
		private int frame = 0; // 0-23 表示24种场景
		private int mode = 0;  // 0-1 表示常规程序和音频程序


		public MainForm()
		{
			InitializeComponent();
			this.skinEngine1.SkinFile = Application.StartupPath + @"\MacOS.ssk";
		}	

		private void Form1_Load(object sender, EventArgs e)
		{
			//TODO : 动态加载可用的串口
			string[] comList = { "COM1", "COM2" } ;
			foreach (string com in comList) {
				comComboBox.Items.Add(com);
			}

			modeComboBox.SelectedIndex = 0;
			frameComboBox.SelectedIndex = 0;

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

		}

		/// <summary>
		/// 这个方法用来设置一些内容；
		/// 会被NewForm调用，并从中获取dbFile的值
		/// </summary>
		/// <param name="dbFile"></param>
		internal void BuildProject(string dbFile)
		{
			this.dbFile = dbFile;
			this.lightEditButton.Enabled = true;
			this.globleSetButton.Enabled = true;

		}

		private void openCOMbutton_Click(object sender, EventArgs e)
		{
			//TODO：打开串口，并将剩下几个按钮设成enable
			//MessageBox.Show(button1.Enabled.ToString());
			newFileButton.Enabled = true;
			openFileButton.Enabled = true;
			saveButton.Enabled = true ;
		}

		private void comComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//MessageBox.Show(comboBox1.SelectedItem.ToString()); 
			if (comComboBox.SelectedItem.ToString() != "") {
				openComButton.Enabled = true;
			}
		}

		private void exitButton_Click(object sender, EventArgs e)
		{
			System.Environment.Exit(0);
		}

		private void openButton_Click(object sender, EventArgs e)
		{
			openFileDialog.ShowDialog();			
		}

		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			MessageBox.Show("成功打开文件:"+openFileDialog.FileName);	
			// 简单读取文本文件
			FileStream file = (FileStream)openFileDialog.OpenFile();	
			// 可指定编码，默认的用Default，它会读取系统的编码（ANSI-->针对不同地区的系统使用不同编码，中文就是GBK）
			StreamReader sr = new StreamReader(file,Encoding.Default);
			string fileName = file.Name;
			fileName = fileName.Substring(fileName.LastIndexOf("\\")+1);
			string s,fileText = fileName + "\n";
			while (( s = sr.ReadLine()) != null)
			{
				fileText += "\n" + s;
			}
			MessageBox.Show(fileText);
		}

		/// <summary>
		/// 另存为才需要打开这个对话框
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
		{

		}

		// 新建工程：新建一个NewForm，并ShowDialog
		private void newButton_Click(object sender, EventArgs e)
		{
			NewForm newForm = new NewForm(this);
			newForm.ShowDialog();
			
		}
		 

		// 保存需要进行的操作：
		// 1.将lightAstList添加到light表中 --> 分新建还是打开文件来说
		// 2.将步数、素材、value表的数据都填进各自的表中
		private void saveButton_Click(object sender, EventArgs e)
		{
			// 1.先判断是否有灯具数据；若无，则直接停止
			if (lightAstList == null || lightAstList.Count == 0){
				MessageBox.Show("当前并没有灯具数据，无法保存！");
				return;
			}

			// 2.保存灯具数据；有几个灯具就保存几个； 最好用SaveOrUpload方法；
			lightDAO = new LightDAO(dbFile,ifEncrypt);
			// lightDAO.CreateSchema(true, true);

			foreach (LightAst la in lightAstList)
			{ 
				DB_Light light = GenerateLight(la);
				lightList.Add(light);
				lightDAO.SaveOrUpdate(light); 
			}
			// 测试代码
			foreach (LightAst la in lightAstList)
			{
				DB_Light light = GenerateLight(la);
				light.Pic = "UPDATE"; 
				lightDAO.SaveOrUpdate(light);
			}

			// 3.保存步数信息：针对每个灯具，保存其相关的步数情况; 
			// 且应该有个 List存放每一步的临时存放的步数信息。
			stepCountDAO = new StepCountDAO(dbFile, ifEncrypt);


			// 4.存放相应的每一步每一通道的值，记录数据到db.Value表中
			valueDAO = new ValueDAO(dbFile, ifEncrypt); 

			//IList<DB_StepCount> scList = stepDAO.GetAll();
			//DB_StepCount sc2 = scList[0];
			//Console.WriteLine("");
		}
		
	
		private void lightEditButton_Click(object sender, EventArgs e)
		{
			if (lightsForm == null || lightsForm.IsDisposed) {
				lightsForm = new LightsForm(this,lightAstList);
			}			
			lightsForm.Show();
		}

		/// <summary>
		///  操作所有灯具，一并清空后，再赋新值
		/// </summary>
		/// <param name="lightAstList"></param>
		internal void AddLights(List<LightAst> lightAstList)
		{
			// 1.成功编辑灯具列表后，将这个列表放到主界面来
			this.lightAstList = lightAstList; 

			// 2.旧的先删除，再将新的加入到lightAstList中；（此过程中，并没有比较的过程，直接操作）
			lightsListView.Items.Clear();
			lightDataList.Clear();

			foreach (LightAst la in this.lightAstList)
			{				
				ListViewItem light = new ListViewItem(
					la.LightName + ":" + la.LightType
					//+"("+la.LightAddr+")" //是否保存占用通道地址
					,la.LightPic
				);			
				lightsListView.Items.Add(light);
				lightDataList.Add( new LightData() );
			}
		}


		private void lightsListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 必须判断这个字段(Count)，否则会报异常
			if (lightsListView.SelectedIndices.Count > 0) {
				selectedLightIndex = lightsListView.SelectedIndices[0];
				generateLightData();
			}
		}

		/// <summary>
		/// 这个方法应该需要分许多步骤：
		/// 0.先查看当前内存是否已有此数据 
		/// 1.若还未有，则取出相关的ini进行渲染
		/// 2.若内存内 或 数据库内已有相关数据，则使用这个数据。
		/// </summary>
		/// <param name="la"></param>
		private void generateLightData()
		{
			LightAst lightAst = lightAstList[selectedLightIndex];
			lightValueLabel.Text = lightAst.LightName + ":" + lightAst.LightType + "(" + lightAst.LightAddr + ")";

			//1.让tongdaoGroupBox显示出来
			this.tongdaoGroupBox.Show();

			//2.判断是不是已经有stepMode了
			// ①若无，则生成数据，并hideAllTongdao-->因为刚创建，肯定没有步			
			LightData lightData = lightDataList[selectedLightIndex];
			if (lightData.StepMode == null)
			{
				Console.WriteLine("Dickov:开始生成模板文件");
				lightData.StepMode = generateStepMode(lightAst);
				hideAllTongdao();
			}
			// ②若有，还需判断该LightData的StepList[frame,mode]是不是为null
			else
			{
				if (lightData.StepList[frame, mode].StepList.Count == 0) {
					hideAllTongdao();
				}




			}
			

		}

		private void hideAllTongdao()
		{
			for (int i = 0; i < 32; i++) {
				valueNumericUpDowns[i].Visible = false;
				labels[i].Visible = false;
				vScrollBars[i].Visible = false;
			}
		}

		/// <summary>
		/// 生成模板Step --》 之后每新建一步，都复制模板step的数据。
		/// </summary>
		/// <param name="lightAst"></param>
		/// <returns></returns>
		private StepAst generateStepMode(LightAst lightAst)
		{
			using (FileStream file = new FileStream(lightAst.LightPath, FileMode.Open))
			{
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
				// 当行数小于5行时，这个文件肯定是错的;最少需要5行(实际上真实数据至少9行: [set]+4+[data]+3 ）
				if (lineCount < 5)
				{
					MessageBox.Show("Dickov：打开的ini文件格式有误，无法生成StepAst！");
					return null;
				}
				else
				{
					int tongdaoCount2 = (lineCount - 6) / 3;
					int tongdaoCount = int.Parse(lineList[3].ToString().Substring(6));
					if (tongdaoCount2 != tongdaoCount)
					{
						MessageBox.Show("Dickov：打开的ini文件的count值与实际值不符合，无法生成StepAst！");
						return null;
					}

					List<TongdaoWrapper> tongdaoList = new List<TongdaoWrapper>();
					for (int i = 0; i < tongdaoCount; i++)
					{
						string tongdaoName = lineList[3 * i + 6].ToString().Substring(4);
						int initNum = int.Parse(lineList[3 * i + 7].ToString().Substring(4));
						int address = int.Parse(lineList[3 * i + 8].ToString().Substring(4));
						tongdaoList.Add(new TongdaoWrapper(){ TongdaoName = tongdaoName, ScrollValue = initNum } );
					}
					return new StepAst() { TongdaoList = tongdaoList ,IsSaved = false };
				}
			}
		}

		/// <summary>
		/// 通过传来的数值，生成通道列表的数据
		/// </summary>
		/// <param name="tongdaoWrappers"></param>
		/// <param name="startNum"></param>
		private void ShowVScrollBars(List<TongdaoWrapper> tongdaoWrappers,int startNum) {

			// 1.每次更换灯具，都先清空通道
			hideAllTongdao();

			// 2.将dataWrappers的内容渲染到起VScrollBar中
			for (int i = 0; i < tongdaoWrappers.Count; i++)
			{
				TongdaoWrapper tongdaoWrapper = tongdaoWrappers[i];

				this.labels[i].Text = (startNum+i) + "\n\n-\n " + tongdaoWrapper.TongdaoName;
				this.valueNumericUpDowns[i].Text = tongdaoWrapper.ScrollValue.ToString();
				this.vScrollBars[i].Value = tongdaoWrapper.ScrollValue;

				vScrollBars[i].Show();
				labels[i].Show();
				valueNumericUpDowns[i].Show();
			}
		}

		/// <summary>
		///  使用LightAst生成DB_Light
		/// </summary>
		/// <param name="la"></param>
		/// <returns></returns>
		private DB_Light GenerateLight(LightAst la)
		{
			return new DB_Light()
			{
				LightNo = la.StartNum,
				StartID = la.StartNum,
				Name = la.LightName,
				Type = la.LightType,
				Pic = la.LightPic,
				Count  = la.Count
			};			
		}

		

		/// <summary>
		///  用户移动滚轴时发生
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			// 正常方法:可通过比对vsBar与vsBars的index的值，若是同一个，则取出index
			int index = getIndexNum( ((VScrollBar)sender).Name);
			valueNumericUpDowns[index].Text = vScrollBars[index].Value.ToString();
		}

		/// <summary>
		///  textBox发生变化时发生
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueTextBox_TextChanged(object sender, EventArgs e)
		{
			//// 1.先验证参数值是不是正确的
			//TextBox tb = (TextBox)sender;
			//int index = getIndexNum(tb.Name);
			//int tempValue = vScrollBars[index].Value;
			//int value = 0;
			//try {
			//	value = int.Parse(tb.Text);
			//	if (value > 255) {
			//		value = 255;					
			//	}
			//	tb.Text = tempValue.ToString();
			//} catch (Exception ex) {
			//	Console.WriteLine("Dickov:" + ex.Message);
			//	tb.Text = tempValue.ToString();
			//	value = tempValue ; 				
			//}

			//// 2.调整相应的vScrollBar的数值
			
			//vScrollBars[index].Value = value; 
		}

		/// <summary>
		///  辅助方法：将一个形如“单词串+数字”的字符串，提取其数字值,再将这个数字减一，即可得到index值
		/// </summary>
		private int getIndexNum(String senderName) {

			//《LightEditor》 method：取出触发的sender的Name，对其进行操作
			// 1.替换掉非数字的字符串;(另一个方法，截取“_”之前,“Bar”之后的字符串也可以)
			// 2.将取出的数字-1；即可得到数组下标
			string labelIndexStr = System.Text.RegularExpressions.Regex.Replace( senderName,  @"[^0-9]+", "");		
			int numIndex = int.Parse(labelIndexStr) - 1;

			return numIndex;
		}

		/// <summary>
		/// 每次按下按钮时，验证输入的是否是整数（不带“.”或其他字符，只有0-9）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!( (e.KeyChar >= '0') && (e.KeyChar <= '9')	)) {
				MessageBox.Show("请输入整数");
			}
			
		}		
		
		private void newStepButton_Click(object sender, EventArgs e)
		{
			LightData lightData = lightDataList[selectedLightIndex];
			StepAst stepMode = lightData.StepMode;
			//StepAst stepAst = lightData.StepList[frame, mode];			
			
			//// 只有stepList[x,y]为空时，需生成一个stepAst，并将stepList[x,y]指向它，否则stepList[frame,mode]就是等价的
			//if (stepAst == null) {
			//	stepAst = new StepAst()
			//	{
			//		TongdaoList = generateTongdaoList(stepMode.TongdaoList),
			//		IsSaved = false
			//	};
			//	lightData.StepList[frame, mode] = stepAst;
			//}
			

			//this.ShowVScrollBars(stepAst.TongdaoList , 1);
		}

		private List<TongdaoWrapper> generateTongdaoList(List<TongdaoWrapper> oldTongdaoList)
		{
			// 方法1:这个方法能保证，肯定生成的是【非引用】类型的数据
			List<TongdaoWrapper> newList = new List<TongdaoWrapper>();
			foreach (TongdaoWrapper item in oldTongdaoList)
			{
				newList.Add(new TongdaoWrapper()
					{
						StepTime = 32,
						TongdaoName = item.TongdaoName,
						ScrollValue = item.ScrollValue
					}
				);
			}

			// 方法2:用内置方法,来回转化，无法保证是非引用数据 --》经过测试：确定是相同数据
			//TongdaoWrapper[] temp = new TongdaoWrapper[oldTongdaoList.Count];
			//oldTongdaoList.CopyTo(temp);
			//List<TongdaoWrapper> newList = temp.ToList<TongdaoWrapper>();

			// 测试方法：是不是同一个数据： 结论 方法2是同一个数据！
			//for (int i = 0; i < newList.Count; i++)
			//{
			//	if (newList[i] == oldTongdaoList[i]) {
			//		MessageBox.Show("Dickov:新旧是同一个");
			//	}
			//}

			return newList;
		}

		private void helpNDBC()
		{

			//MessageBox.Show(lightAstList.Count.ToString());

			dbFile = @"C:\\Temp\\testDB.db3";
			SQLiteHelper sqlHelper = new SQLiteHelper(dbFile);
			sqlHelper.Connect();

			// 设置数据库密码
			// sqlHelper.ChangePassword(MD5Ast.MD5(dbFile));

			//向数据库中user表中插入了一条(name = "马兆瑞"，age = 21)的记录
			//string insert_sql = "insert into user(name,age) values(?,?)";        //插入的SQL语句(带参数)
			//SQLiteParameter[] para = new SQLiteParameter[2];                        //构造并绑定参数
			//para[0] = new SQLiteParameter("name", "马朝旭");
			//para[1] = new SQLiteParameter("age", 21);

			//int ret = sqlHelper.ExecuteNonQuery(insert_sql, para); //返回影响的行数

			//// 查询表数据，并放到实体类中
			string select_sql = "select * from Light";                            //查询的SQL语句
			DataTable dt = sqlHelper.ExecuteDataTable(select_sql, null);               //执行查询操作,结果存放在dt中
			if (dt != null)
			{
				foreach (DataRow dr in dt.Rows)
				{
					object[] lightValues = dr.ItemArray;
					DB_Light light = new DB_Light();
					light.LightNo = Convert.ToInt32(lightValues.GetValue(0));
					light.Name = (string)(lightValues.GetValue(1));
					light.Type = (string)(lightValues.GetValue(2));
					light.Pic = (string)(lightValues.GetValue(3));
					light.StartID = Convert.ToInt32(lightValues.GetValue(4));
					light.Count = Convert.ToInt32(lightValues.GetValue(5));
					lightList.Add(light);
				}
			}
			else
			{
				Console.WriteLine("Light表没有数据");
			}

			select_sql = "select * from stepCount";
			dt = sqlHelper.ExecuteDataTable(select_sql, null);
			if (dt != null)
			{
				foreach (DataRow dr in dt.Rows)
				{
					object[] stepValues = dr.ItemArray;
					DB_StepCountPK pk = new DB_StepCountPK();
					pk.LightIndex = Convert.ToInt32(stepValues.GetValue(0));
					pk.Frame = Convert.ToInt32(stepValues.GetValue(1));
					pk.Mode = Convert.ToInt32(stepValues.GetValue(2));

					DB_StepCount stepCount = new DB_StepCount();
					stepCount.StepCount = Convert.ToInt32(stepValues.GetValue(3));
					stepCount.PK = pk;

					stepCountList.Add(stepCount);
				}
			}
			else
			{
				Console.WriteLine("stepCount表没有数据");
			}

			select_sql = "select * from value";
			dt = sqlHelper.ExecuteDataTable(select_sql, null);
			if (dt != null)
			{
				foreach (DataRow dr in dt.Rows)
				{
					object[] values = dr.ItemArray;

					DB_ValuePK pk = new DB_ValuePK();
					pk.LightIndex = Convert.ToInt32(values.GetValue(0));
					pk.Frame = Convert.ToInt32(values.GetValue(1));
					pk.Step = Convert.ToInt32(values.GetValue(2));
					pk.Mode = Convert.ToInt32(values.GetValue(3));
					pk.LightID = Convert.ToInt32(values.GetValue(7));

					DB_Value val = new DB_Value();
					val.PK = pk;
					val.ScrollValue = Convert.ToInt32(values.GetValue(4));
					val.StepTime = Convert.ToInt32(values.GetValue(5));
					val.ChangeMode = Convert.ToInt32(values.GetValue(6));

					valueList.Add(val);
				}
			}
			else
			{
				Console.WriteLine("value表没有数据");
			}
			allData = new DBWrapper(lightList, stepCountList, valueList);

		}

		private void helpHibernate()
		{

			DB_Light light = new DB_Light()
			{
				LightNo = 300,
				Name = "OUP3",
				Type = "XDD3",
				Pic = "3.bmp",
				StartID = 300,
				Count = 3
			};

			LightDAO lightDAO = new LightDAO(@"C:\Temp\test.db3", true);

			// CRUD : 1.增 2.查 3.改 4.删
			//lightDAO.Save(light);
			//DB_Light light2 = lightDAO.Get(2);
			//light2.Name = "Nice too mee you";
			//lightDAO.Update(light2);
			//lightDAO.Delete(light2);

			IList<DB_Light> lightList = lightDAO.GetAll();
			foreach (var eachLight in lightList)
			{
				Console.WriteLine(eachLight);
			}

			Console.WriteLine();



		}

		private void frameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			frame = frameComboBox.SelectedIndex; 
		}

		private void modeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			mode = modeComboBox.SelectedIndex;
		}
	}
}
