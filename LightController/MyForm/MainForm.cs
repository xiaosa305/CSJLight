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
using LightController.MyForm;
using LightController.Common;

namespace LightController
{
	public partial class MainForm :  System.Windows.Forms.Form
	{
		// 只能有一个lightsForm，在点击编辑灯具时（未生成过或已被销毁）新建，或在Hide时显示
		private LightsForm lightsForm;
		private List<LightAst> lightAstList;

		// 只能有一个GlobalSetForm，在点击全局设置时新建(为生成过或已被销毁)，或在Hide时显示
		private GlobalSetForm globalSetForm; 

		// 辅助的变量：
		// 点击新建后，点击保存前，这个属性是true；如果是使用打开文件或已经点击了保存按钮，则设为false
		private bool isNew = true;

		// 点击保存后|刚打开一个文件时，这个属性就设为true;如果对内容稍有变动，则设为false
		//private bool isSaved = false;
		public string globalIniFilePath;

		// 数据库连接
		// 数据库地址：每个项目都有自己的db，所以需要一个可以改变的dbFile字符串，存放数据库连接相关信息
		public string dbFilePath;
		private bool ifEncrypt = false; //是否加密

		private LightDAO lightDAO;
		private StepCountDAO stepCountDAO;
		private ValueDAO valueDAO;		

		// 这几个数据 ，能存放所有数据库数据
		public DBWrapper allData;
		List<DB_Light> lightList = new List<DB_Light>();
		List<DB_StepCount> stepCountList = new List<DB_StepCount>();
		List<DB_Value> valueList = new List<DB_Value>();

		// 辅助的灯具变量：记录所有（灯具）的（所有场景和模式）的 每一步（通道列表）
		private List<LightWrapper> lightWrapperList = new List<LightWrapper>();

		private int selectedLightIndex; //选择的灯具的index
		private int frame = 0; // 0-23 表示24种场景
		private int mode = 0;  // 0-1 表示常规程序和音频程序

		public MainForm()
		{
			InitializeComponent();
			//this.skinEngine1.SkinFile = Application.StartupPath + @"\MacOS.ssk";

			//TODO : 动态加载可用的串口
			string[] comList = { "COM1", "COM2" };
			foreach (string com in comList)
			{
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

			this.stepNumericUpDowns[0] = numericUpDown33;
			this.stepNumericUpDowns[1] = numericUpDown34;
			this.stepNumericUpDowns[2] = numericUpDown35;
			this.stepNumericUpDowns[3] = numericUpDown36;
			this.stepNumericUpDowns[4] = numericUpDown37;
			this.stepNumericUpDowns[5] = numericUpDown38;
			this.stepNumericUpDowns[6] = numericUpDown39;
			this.stepNumericUpDowns[7] = numericUpDown40;
			this.stepNumericUpDowns[8] = numericUpDown41;
			this.stepNumericUpDowns[9] = numericUpDown42;
			this.stepNumericUpDowns[10] = numericUpDown43;
			this.stepNumericUpDowns[11] = numericUpDown44;
			this.stepNumericUpDowns[12] = numericUpDown45;
			this.stepNumericUpDowns[13] = numericUpDown46;
			this.stepNumericUpDowns[14] = numericUpDown47;
			this.stepNumericUpDowns[15] = numericUpDown48;
			this.stepNumericUpDowns[16] = numericUpDown49;
			this.stepNumericUpDowns[17] = numericUpDown50;
			this.stepNumericUpDowns[18] = numericUpDown51;
			this.stepNumericUpDowns[19] = numericUpDown52;
			this.stepNumericUpDowns[20] = numericUpDown53;
			this.stepNumericUpDowns[21] = numericUpDown54;
			this.stepNumericUpDowns[22] = numericUpDown55;
			this.stepNumericUpDowns[23] = numericUpDown56;
			this.stepNumericUpDowns[24] = numericUpDown57;
			this.stepNumericUpDowns[25] = numericUpDown58;
			this.stepNumericUpDowns[26] = numericUpDown59;
			this.stepNumericUpDowns[27] = numericUpDown60;
			this.stepNumericUpDowns[28] = numericUpDown61;
			this.stepNumericUpDowns[29] = numericUpDown62;
			this.stepNumericUpDowns[30] = numericUpDown63;
			this.stepNumericUpDowns[31] = numericUpDown64;

			this.changeModeComboBoxes[0] = changeModeComboBox1;
			this.changeModeComboBoxes[1] = changeModeComboBox2;
			this.changeModeComboBoxes[2] = changeModeComboBox3;
			this.changeModeComboBoxes[3] = changeModeComboBox4;
			this.changeModeComboBoxes[4] = changeModeComboBox5;
			this.changeModeComboBoxes[5] = changeModeComboBox6;
			this.changeModeComboBoxes[6] = changeModeComboBox7;
			this.changeModeComboBoxes[7] = changeModeComboBox8;
			this.changeModeComboBoxes[8] = changeModeComboBox9;
			this.changeModeComboBoxes[9] = changeModeComboBox10;
			this.changeModeComboBoxes[10] = changeModeComboBox11;
			this.changeModeComboBoxes[11] = changeModeComboBox12;
			this.changeModeComboBoxes[12] = changeModeComboBox13;
			this.changeModeComboBoxes[13] = changeModeComboBox14;
			this.changeModeComboBoxes[14] = changeModeComboBox15;
			this.changeModeComboBoxes[15] = changeModeComboBox16;
			this.changeModeComboBoxes[16] = changeModeComboBox17;
			this.changeModeComboBoxes[17] = changeModeComboBox18;
			this.changeModeComboBoxes[18] = changeModeComboBox19;
			this.changeModeComboBoxes[19] = changeModeComboBox20;
			this.changeModeComboBoxes[20] = changeModeComboBox21;
			this.changeModeComboBoxes[21] = changeModeComboBox22;
			this.changeModeComboBoxes[22] = changeModeComboBox23;
			this.changeModeComboBoxes[23] = changeModeComboBox24;
			this.changeModeComboBoxes[24] = changeModeComboBox25;
			this.changeModeComboBoxes[25] = changeModeComboBox26;
			this.changeModeComboBoxes[26] = changeModeComboBox27;
			this.changeModeComboBoxes[27] = changeModeComboBox28;
			this.changeModeComboBoxes[28] = changeModeComboBox29;
			this.changeModeComboBoxes[29] = changeModeComboBox30;
			this.changeModeComboBoxes[30] = changeModeComboBox31;
			this.changeModeComboBoxes[31] = changeModeComboBox32;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			

		}

		/// <summary>
		/// 这个方法用来设置一些内容;
		/// 会被NewForm调用，并从中获取dbFilePath和projectName的值；
		/// 并初始化几个DAO组件
		/// </summary>
		/// <param name="dbFilePath"></param>
		internal void BuildProject(string globalIniFilePath,string dbFilePath,string projectName)
		{
			this.globalIniFilePath = globalIniFilePath;
			this.dbFilePath = dbFilePath;
			this.Text = "智控配置(当前工程:" + projectName+")";
			this.isNew = true;

			this.lightsEditToolStripMenuItem1.Enabled = true;
			this.globalSetToolStripMenuItem.Enabled = true;

			// 创建数据库:
			// 因为是新建，所以先让所有的DAO指向null，避免连接到错误的数据库(已打开过旧的工程的情况下)；为了新建数据库，将lightDAO指向新的对象
			lightDAO = null;
			lightDAO = new LightDAO(dbFilePath, false);
			lightDAO.CreateSchema(true,true);

			stepCountDAO = null;
			valueDAO = null;

			enableSave();
		}

		/// <summary>
		/// 这个方法，一般在新建或者打开项目后使用，作用是让两个保存按键可用
		/// </summary>
		private void enableSave() {
			saveButton.Enabled = true;
			saveAsButton.Enabled = true;
		}

		/// <summary>
		/// 待完成：打开串口后的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openCOMbutton_Click(object sender, EventArgs e)
		{
			//TODO：打开串口，并将剩下几个按钮设成enable
			newFileButton.Enabled = true;
			openFileButton.Enabled = true;
		}

		/// <summary>
		///  当COM下拉框选择内容后，按钮才可使用
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//MessageBox.Show(comboBox1.SelectedItem.ToString()); 
			if (comComboBox.SelectedItem.ToString() != "")
			{
				openComButton.Enabled = true;
			}
		}

		/// <summary>
		///  彻底退出应用，结束所有线程
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exitButton_Click(object sender, EventArgs e)
		{			
			System.Environment.Exit(0);
		}

		/// <summary>
		/// 待完成：点击打开按钮后的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openButton_Click(object sender, EventArgs e)
		{
			//TODO: 打开按钮点击后的操作
			openFileDialog.ShowDialog(this);
		}

		/// <summary>
		/// 待完成：打开文件对话框选择文件后的操作;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			//TODO : 打开文件对话框选择文件后的操作
			MessageBox.Show("成功打开文件:" + openFileDialog.FileName);
			// 简单读取文本文件
			FileStream file = (FileStream)openFileDialog.OpenFile();
			// 可指定编码，默认的用Default，它会读取系统的编码（ANSI-->针对不同地区的系统使用不同编码，中文就是GBK）
			StreamReader sr = new StreamReader(file, Encoding.Default);
			string fileName = file.Name;
			fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
			string s, fileText = fileName + "\n";
			while ((s = sr.ReadLine()) != null)
			{
				fileText += "\n" + s;
			}
			MessageBox.Show(fileText);
		}

		/// <summary>
		/// 待完成：另存为才需要打开这个对话框
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			//TODO
		}

		/// <summary>
		/// 新建工程：新建一个NewForm，并ShowDialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newButton_Click(object sender, EventArgs e)
		{
			NewForm newForm = new NewForm(this);
			newForm.ShowDialog(this);

		}


		/// <summary>
		/// 保存需要进行的操作：
		/// 1.将lightAstList添加到light表中 --> 分新建或打开文件两种情况
		/// 2.将步数、素材、value表的数据都填进各自的表中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			// 1.先判断是否有灯具数据；若无，则直接停止
			if (lightAstList == null || lightAstList.Count == 0)
			{
				MessageBox.Show("当前并没有灯具数据，无法保存！");
				return;
			}

			// 2.保存各项数据
			saveAllLights();
			saveAllStepCounts();
			saveAllValues();
			MessageBox.Show("成功保存");
		}

		/// <summary>
		///  保存灯具数据；有几个灯具就保存几个； 最好用SaveOrUpload方法；
		/// </summary>
		private void saveAllLights()
		{
			if (lightDAO == null) { 
				lightDAO = new LightDAO(dbFilePath, ifEncrypt);
			}
			// 将传送所有的DB_Light给DAO,让它进行数据的保存

			lightList.Clear();
			foreach (LightAst la in lightAstList)
			{
				DB_Light light = LightAst.GenerateLight(la);
				this.lightList.Add(light);		
			}
			lightDAO.SaveAll( lightList );
		}

		/// <summary>
		/// 保存步数信息：针对每个灯具，保存其相关的步数情况; 
		/// </summary>
		private void saveAllStepCounts()
		{
			if(stepCountDAO == null){
				stepCountDAO = new StepCountDAO(dbFilePath, ifEncrypt);
			}

			// 保存所有步骤前，先清空stepCountList
			stepCountList.Clear();
			// 取出每个灯具,填入stepCountList中
			foreach (LightWrapper lightTemp in lightWrapperList)
			{
				DB_Light light = lightList[lightWrapperList.IndexOf(lightTemp)];				
				LightStepWrapper[,] lswl = lightTemp.LightStepWrapperList;

				// 取出灯具的每个常规场景(24种），并将它们保存起来
				for (int frame = 0; frame < 24; frame ++)
				{
					for (int mode = 0; mode < 2; mode ++)
					{
						LightStepWrapper lsTemp = lswl[frame, mode];
						if (lsTemp != null)
						{
							DB_StepCount stepCount = new DB_StepCount()
							{
								StepCount = lsTemp.TotalStep,
								PK = new DB_StepCountPK()
								{
									Frame = frame,
									Mode = mode,
									LightIndex = light.LightNo
								}
							};
							stepCountList.Add(stepCount);
						}
					}
				}
			}
			stepCountDAO.SaveAll(stepCountList);
			
		}

		/// <summary>
		/// 存放相应的每一步每一通道的值，记录数据到db.Value表中
		/// </summary>
		private void saveAllValues()
		{
			if(valueDAO == null) { 
				valueDAO = new ValueDAO(dbFilePath, ifEncrypt);
			}

			foreach (LightWrapper lightTemp in lightWrapperList)
			{
				DB_Light light = lightList[lightWrapperList.IndexOf(lightTemp)];
				LightStepWrapper[,] lswl = lightTemp.LightStepWrapperList;

				// 同样如果要做彻底保存，需要先清空valueList
				valueList.Clear();
				for (int frame = 0; frame < 24; frame++)
				{
					for (int mode = 0; mode < 2; mode++)
					{
						LightStepWrapper lightStep = lswl[frame, mode];
						if (lightStep != null && lightStep.TotalStep > 0)
						{  //只有不为null，才可能有需要保存的数据
							List<StepWrapper> stepList = lightStep.StepWrapperList;
							foreach (StepWrapper step in stepList)
							{
								int stepIndex = stepList.IndexOf(step) + 1;
								for (int tongdaoIndex = 0; tongdaoIndex < step.TongdaoList.Count; tongdaoIndex++)
								{
									TongdaoWrapper tongdao = step.TongdaoList[tongdaoIndex];
									DB_Value valueTemp = new DB_Value()
									{
										ChangeMode = tongdao.ChangeMode,
										ScrollValue = tongdao.ScrollValue,
										StepTime = tongdao.StepTime,
										PK = new DB_ValuePK()
										{
											Frame = frame,
											Mode = mode,
											LightID = light.LightNo + tongdaoIndex,
											LightIndex = light.LightNo,
											Step = stepIndex
										}
									};
									valueList.Add(valueTemp);
								}
							}
						}
					}
				}
				valueDAO.SaveAll(valueList);
			}

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
			lightWrapperList.Clear();

			foreach (LightAst la in this.lightAstList)
			{
				ListViewItem light = new ListViewItem(
					la.LightName + ":" + la.LightType
					//+"("+la.LightAddr+")" //是否保存占用通道地址
					, la.LightPic
				);
				lightsListView.Items.Add(light);
				lightWrapperList.Add(new LightWrapper());
			}
		}


		private void lightsListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 必须判断这个字段(Count)，否则会报异常
			if (lightsListView.SelectedIndices.Count > 0)
			{
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
			
			// 显示选中灯的部分信息在label中
			lightValueLabel.Text = lightAst.LightName + ":" + lightAst.LightType + "(" + lightAst.LightAddr + ")";

			//1.让tongdaoGroupBox显示出来
			this.tongdaoGroupBox.Show();

			//2.判断是不是已经有stepMode了
			// ①若无，则生成数据，并hideAllTongdao 并设stepLabel为“0/0” --> 因为刚创建，肯定没有步数	
			// ②若有，还需判断该LightData的LightStepWrapperList[frame,mode]是不是为null
			//			若是null，则说明该FM下，并未有步数，hideAllTongdao
			//			若不为null，则说明已有数据，
			LightWrapper lightWrapper = lightWrapperList[selectedLightIndex];
			if (lightWrapper.StepMode == null)
			{
				Console.WriteLine("Dickov:开始生成模板文件");
				lightWrapper.StepMode = generateStepMode(lightAst);
				showStepLabel(0, 0);
				hideAllTongdao();
			}			
			else
			{
				changeFrameMode();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void changeFrameMode()
		{
			LightWrapper lightWrapper = lightWrapperList[selectedLightIndex];
			LightStepWrapper stepList = lightWrapper.LightStepWrapperList[frame, mode];

			// 为空或StepList数量是0
			if (stepList == null || stepList.StepWrapperList.Count == 0)
			{
				hideAllTongdao();
				showStepLabel(0, 0);
			}
			else // stepList != null && stepList.StepList.Count>0 : 也就是已经有值了
			{
				int recentStep = stepList.CurrentStep;
				int totalStep = stepList.TotalStep;

				StepWrapper stepAst = stepList.StepWrapperList[recentStep - 1];
				ShowVScrollBars(stepAst.TongdaoList, 1);

				showStepLabel(recentStep, totalStep);
			}
		}

		/// <summary>
		/// 隐藏所有通道:包含五个部分
		/// </summary>
		private void hideAllTongdao()
		{
			//TODO : 隐藏所有通道功能待完善
			for (int i = 0; i < 32; i++)
			{
				valueNumericUpDowns[i].Visible = false;
				labels[i].Visible = false;
				vScrollBars[i].Visible = false;
				changeModeComboBoxes[i].Visible = false ;
				stepNumericUpDowns[i].Visible = false;
			}
		}

		/// <summary>
		///  辅助方法：用来显示stepLabel-->当前步/总步数
		/// </summary>
		/// <param name="recentStep"></param>
		/// <param name="totalStep"></param>
		private void showStepLabel(int recentStep, int totalStep)
		{
			stepLabel.Text = recentStep + "/" + totalStep;
		}

		/// <summary>
		/// 生成模板Step --》 之后每新建一步，都复制模板step的数据。
		/// </summary>
		/// <param name="lightAst"></param>
		/// <returns></returns>
		private StepWrapper generateStepMode(LightAst lightAst)
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
						tongdaoList.Add(new TongdaoWrapper() {
							TongdaoName = tongdaoName,
							ScrollValue = initNum,
							StepTime = 10,
							ChangeMode = 0,
							Address = address
						});
					}
					return new StepWrapper() { TongdaoList = tongdaoList, IsSaved = false };
				}
			}
		}

		/// <summary>
		/// 通过传来的数值，生成通道列表的数据
		/// </summary>
		/// <param name="tongdaoWrappers"></param>
		/// <param name="startNum"></param>
		private void ShowVScrollBars(List<TongdaoWrapper> tongdaoWrappers, int startNum)
		{

			// 1.每次更换灯具，都先清空通道
			hideAllTongdao();

			// 2.将dataWrappers的内容渲染到起VScrollBar中
			for (int i = 0; i < tongdaoWrappers.Count; i++)
			{
				TongdaoWrapper tongdaoWrapper = tongdaoWrappers[i];

				this.labels[i].Text = (startNum + i) + "\n\n-\n " + tongdaoWrapper.TongdaoName;
				this.valueNumericUpDowns[i].Text = tongdaoWrapper.ScrollValue.ToString();
				this.vScrollBars[i].Value = tongdaoWrapper.ScrollValue;
				this.changeModeComboBoxes[i].SelectedIndex = tongdaoWrapper.ChangeMode;
				this.stepNumericUpDowns[i].Text = tongdaoWrapper.StepTime.ToString();

				this.vScrollBars[i].Show();
				this.labels[i].Show();
				this.valueNumericUpDowns[i].Show();
				this.changeModeComboBoxes[i].Show();
				this.stepNumericUpDowns[i].Show();
			}
		}	



		

		private void newStepButton_Click(object sender, EventArgs e)
		{
			LightWrapper lightData = lightWrapperList[selectedLightIndex];
			StepWrapper stepMode = lightData.StepMode;

			// 如果此值为空，则创建之
			if (lightData.LightStepWrapperList[frame, mode] == null)
			{
				lightData.LightStepWrapperList[frame, mode] = new LightStepWrapper()
				{
					StepWrapperList = new List<StepWrapper>()
				};
			}

			// 验证是否超过两种mode自己的步数限制
			if (mode == 0 && lightData.LightStepWrapperList[frame, mode].TotalStep >= 32)
			{
				MessageBox.Show("常规程序最多不超过32步");
				return;
			}
			if (mode == 1 && lightData.LightStepWrapperList[frame, mode].TotalStep >= 48)
			{
				MessageBox.Show("音频程序最多不超过48步");
				return;
			}

			//TODO 若通过步数验证，则新建步，并将stepLabel切换成最新的标签

			StepWrapper newStep = new StepWrapper()
			{
				TongdaoList = generateTongdaoList(stepMode.TongdaoList),
				IsSaved = false
			};
			// 调用包装类内部的方法
			lightData.LightStepWrapperList[frame, mode].AddStep(newStep);

			this.ShowVScrollBars(newStep.TongdaoList, 1);
			this.showStepLabel(lightData.LightStepWrapperList[frame, mode].CurrentStep, lightData.LightStepWrapperList[frame, mode].TotalStep);

		}


		/// <summary>
		/// 通过模板的通道数据，生成新的非引用(要摆脱与StepMode的关系)的tongdaoList
		/// </summary>
		/// <param name="oldTongdaoList"></param>
		/// <returns></returns>
		private List<TongdaoWrapper> generateTongdaoList(List<TongdaoWrapper> oldTongdaoList)
		{
			// 方法1:这个方法能保证，肯定生成的是【非引用】类型的数据
			List<TongdaoWrapper> newList = new List<TongdaoWrapper>();
			foreach (TongdaoWrapper item in oldTongdaoList)
			{
				newList.Add(new TongdaoWrapper()
				{
					StepTime = item.StepTime,
					TongdaoName = item.TongdaoName,
					ScrollValue = item.ScrollValue,
					ChangeMode = item.ChangeMode
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

			dbFilePath = @"C:\\Temp\\testDB.db3";
			SQLiteHelper sqlHelper = new SQLiteHelper(dbFilePath);
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

		private void frameModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			frame = frameComboBox.SelectedIndex;
			mode = modeComboBox.SelectedIndex;
			if (lightAstList != null && lightAstList.Count > 0)
			{
				changeFrameMode();
			}
		}


		/// <summary>
		///  点击上一步的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backStepButton_Click(object sender, EventArgs e)
		{
			int currentStepValue = getCurrentStepValue();
			if (currentStepValue > 1)
			{
				chooseStep(currentStepValue - 1);
			}
			else
			{
				MessageBox.Show("当前已是第一步");
			}

		}

		/// <summary>
		/// 点击下一步的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nextStepButton_Click(object sender, EventArgs e)
		{
			int currentStepValue = getCurrentStepValue();
			int totalStepValue = getTotalStepValue();

			if (currentStepValue < totalStepValue)
			{
				chooseStep(currentStepValue + 1);
			}
			else
			{
				MessageBox.Show("当前已是最大步");
			}
		}

		/// <summary>
		/// 辅助： 这个方法，抽象了【选择某一个指定步数后，统一的操作，NextStep和BackStep应该都使用这个方法】
		/// </summary>
		private void chooseStep(int stepValue)
		{

			LightStepWrapper lightStepWrapper = getCurrentLightStepWrapper();

			StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[stepValue - 1];
			lightStepWrapper.CurrentStep = stepValue;

			this.ShowVScrollBars(stepWrapper.TongdaoList, 1);
			this.showStepLabel(stepValue, lightStepWrapper.TotalStep);

		}

		/// <summary>
		/// 辅助：这个方法直接取出当前步
		/// </summary>
		/// <returns></returns>
		private StepWrapper getCurrentStepWrapper()
		{
			LightStepWrapper light = getCurrentLightStepWrapper();
			StepWrapper step = light.StepWrapperList[light.CurrentStep - 1];
			return step;
		}

		/// <summary>
		///  辅助：取出当前步的currentStep值
		/// </summary>
		/// <returns></returns>
		private int getCurrentStepValue()
		{
			int currentStepValue = getCurrentLightStepWrapper().CurrentStep;
			return currentStepValue;
		}

		/// <summary>
		///  辅助：取出当前步的totalStep值
		/// </summary>
		/// <returns></returns>
		private int getTotalStepValue()
		{
			int totalStepValue = getCurrentLightStepWrapper().TotalStep;
			return totalStepValue;
		}

		/// <summary>
		///  辅助：取出选定灯具、Frame、Mode 的 所有步数集合
		/// </summary>
		/// <returns></returns>
		private LightStepWrapper getCurrentLightStepWrapper()
		{
			LightWrapper light = lightWrapperList[selectedLightIndex];
			LightStepWrapper lightStepWrapper = light.LightStepWrapperList[frame, mode];

			//若为空，则立刻创建一个
			if (lightStepWrapper == null)
			{
				lightStepWrapper = new LightStepWrapper()
				{
					StepWrapperList = new List<StepWrapper>()
				};
				light.LightStepWrapperList[frame, mode] = lightStepWrapper;
			};

			return lightStepWrapper;
		}

		
		/// <summary>
		/// 全局变量按钮点击后的操作：
		/// 1.如果未建立，则传配置文件地址及是否新建两个属性
		/// 2.已建立form,则直接打开
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void globleSetButton_Click(object sender, EventArgs e)
		{
			if (globalSetForm == null || globalSetForm.IsDisposed)
			{
				
				globalSetForm = new GlobalSetForm(this, globalIniFilePath, isNew);
			}
			globalSetForm.ShowDialog();

		}


		/// <summary>
		///  点击测试按钮后，会执行这个操作：将各种临时操作或测试操作放在这里
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void testButton_Click(object sender, EventArgs e)
		{
			this.globalSetToolStripMenuItem.Enabled = true;
			this.isNew = false ;
			
		}

		/// <summary>
		///  点击菜单栏中灯具编辑时的动作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsEditToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (lightsForm == null || lightsForm.IsDisposed)
			{
				lightsForm = new LightsForm(this, lightAstList);
			}
			lightsForm.ShowDialog();
		}
		
		/// <summary>
		///  点击灯库编辑时，可以打开另外一个软件LightEditor
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightLibraryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//TODO
		}


		/// <summary>
		///  用户移动滚轴时发生：1.调节相关的numericUpDown; 2.放进相关的step中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueVScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			// 1.先找出对应vScrollBars的index 
			int index = MathAst.getIndexNum(((VScrollBar)sender).Name , -1 );

			//2.把滚动条的值赋给valueNumericUpDowns
			valueNumericUpDowns[index].Text = vScrollBars[index].Value.ToString();

			//3.取出recentStep,使用取出的index，给stepWrapper.TongdaoList[index]赋值
			StepWrapper step = getCurrentStepWrapper();
			step.TongdaoList[index].ScrollValue = vScrollBars[index].Value;
		}

		/// <summary>
		/// 调节或输入numericUpDown的值后，1.调节通道值 2.调节tongdaoWrapper的相关值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			// 1. 找出对应的index
			int index = MathAst.getIndexNum(((NumericUpDown)sender).Name, -1);

			// 2.调整相应的vScrollBar的数值
			vScrollBars[index].Value = Decimal.ToInt32( valueNumericUpDowns[index].Value ) ;

			//3.取出recentStep,使用取出的index，给stepWrapper.TongdaoList[index]赋值
			StepWrapper step = getCurrentStepWrapper();
			step.TongdaoList[index].ScrollValue = vScrollBars[index].Value;

		}



		/// <summary>
		///  每个通道对应的变化模式下拉框，值改变后，对应的tongdaoWrapper也应该设置参数 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void changeModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 1.先找出对应changeModeComboBoxes的index
			int index = MathAst.getIndexNum(((ComboBox)sender).Name , -1);
			
			//2.取出recentStep，这样就能取出一个步数，使用取出的index，给stepWrapper.TongdaoList[index]赋值
			StepWrapper step = getCurrentStepWrapper();
			step.TongdaoList[index].ChangeMode = changeModeComboBoxes[index].SelectedIndex ;

		}

		/// <summary>
		/// 每个通道对应的"步时间"值变化后，对应的tongdaoWrapper也改变参数
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void stepNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			// 1.先找出对应stepNumericUpDowns的index（这个比较麻烦，因为其NumericUpDown的序号是从33开始的 即： name33 = names[0] =>addNum = -33）
			int index = MathAst.getIndexNum(((NumericUpDown)sender).Name, -33);

			//2.取出recentStep，这样就能取出一个步数，使用取出的index，给stepWrapper.TongdaoList[index]赋值
			StepWrapper step = getCurrentStepWrapper();
			step.TongdaoList[index].StepTime = Decimal.ToInt32(stepNumericUpDowns[index].Value);			
		}

		
	}
}