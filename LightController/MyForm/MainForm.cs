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
using LightController.Tools;

namespace LightController
{
	public partial class MainForm :  MainFormInterface
	{
		public MainForm()
		{
			InitializeComponent();

			#region 皮肤相关代码
			IniFileAst iniFileAst = new IniFileAst(Application.StartupPath + @"\GlobalSet.ini");
			string skin = iniFileAst.ReadString("SkinSet", "skin", "");
			if (!String.IsNullOrEmpty(skin))
			{
				this.skinEngine1.SkinFile = Application.StartupPath + "\\irisSkins\\" + skin;
			}		
			DirectoryInfo fdir = new DirectoryInfo(Application.StartupPath + "\\irisSkins");
			try
			{
				FileInfo[] file = fdir.GetFiles();
				if (file.Length > 0)
				{
					foreach (var item in file)
					{
						if (item.FullName.EndsWith(".ssk"))
						{
							skinComboBox.Items.Add(item.Name.Substring(0, item.Name.Length - 4));
						}
					}
					skinComboBox.SelectedIndex = 0;

					skinComboBox.Show();
					skinButton.Show();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			#endregion

			#region 是否显示某些调试按钮
			testGroupBox.Visible = IniFileAst.GetButtonShow(Application.StartupPath, "testButton");
			hardwareUpdateToolStripMenuItem.Visible = IniFileAst.GetButtonShow(Application.StartupPath, "hardwareUpdateButton");



			#endregion

			#region 将同类属性填入数组，方便操作

			this.vScrollBars[0] = vScrollBar1;
			this.vScrollBars[1] = vScrollBar2;
			this.vScrollBars[2] = vScrollBar3;
			this.vScrollBars[3] = vScrollBar4;
			this.vScrollBars[4] = vScrollBar5;
			this.vScrollBars[5] = vScrollBar6;
			this.vScrollBars[6] = vScrollBar7;
			this.vScrollBars[7] = vScrollBar8;
			this.vScrollBars[8] = vScrollBar9;
			this.vScrollBars[9] = vScrollBar10;
			this.vScrollBars[10] = vScrollBar11;
			this.vScrollBars[11] = vScrollBar12;
			this.vScrollBars[12] = vScrollBar13;
			this.vScrollBars[13] = vScrollBar14;
			this.vScrollBars[14] = vScrollBar15;
			this.vScrollBars[15] = vScrollBar16;
			this.vScrollBars[16] = vScrollBar17;
			this.vScrollBars[17] = vScrollBar18;
			this.vScrollBars[18] = vScrollBar19;
			this.vScrollBars[19] = vScrollBar20;
			this.vScrollBars[20] = vScrollBar21;
			this.vScrollBars[21] = vScrollBar22;
			this.vScrollBars[22] = vScrollBar23;
			this.vScrollBars[23] = vScrollBar24;
			this.vScrollBars[24] = vScrollBar25;
			this.vScrollBars[25] = vScrollBar26;
			this.vScrollBars[26] = vScrollBar27;
			this.vScrollBars[27] = vScrollBar28;
			this.vScrollBars[28] = vScrollBar29;
			this.vScrollBars[29] = vScrollBar30;
			this.vScrollBars[30] = vScrollBar31;
			this.vScrollBars[31] = vScrollBar32;

			this.labels[0] = label1;
			this.labels[1] = label2;
			this.labels[2] = label3;
			this.labels[3] = label4;
			this.labels[4] = label5;
			this.labels[5] = label6;
			this.labels[6] = label7;
			this.labels[7] = label8;
			this.labels[8] = label9;
			this.labels[9] = label10;
			this.labels[10] = label11;
			this.labels[11] = label12;
			this.labels[12] = label13;
			this.labels[13] = label14;
			this.labels[14] = label15;
			this.labels[15] = label16;
			this.labels[16] = label17;
			this.labels[17] = label18;
			this.labels[18] = label19;
			this.labels[19] = label20;
			this.labels[20] = label21;
			this.labels[21] = label22;
			this.labels[22] = label23;
			this.labels[23] = label24;
			this.labels[24] = label25;
			this.labels[25] = label26;
			this.labels[26] = label27;
			this.labels[27] = label28;
			this.labels[28] = label29;
			this.labels[29] = label30;
			this.labels[30] = label31;
			this.labels[31] = label32;

			this.valueNumericUpDowns[0] = numericUpDown1;
			this.valueNumericUpDowns[1] = numericUpDown2;
			this.valueNumericUpDowns[2] = numericUpDown3;
			this.valueNumericUpDowns[3] = numericUpDown4;
			this.valueNumericUpDowns[4] = numericUpDown5;
			this.valueNumericUpDowns[5] = numericUpDown6;
			this.valueNumericUpDowns[6] = numericUpDown7;
			this.valueNumericUpDowns[7] = numericUpDown8;
			this.valueNumericUpDowns[8] = numericUpDown9;
			this.valueNumericUpDowns[9] = numericUpDown10;
			this.valueNumericUpDowns[10] = numericUpDown11;
			this.valueNumericUpDowns[11] = numericUpDown12;
			this.valueNumericUpDowns[12] = numericUpDown13;
			this.valueNumericUpDowns[13] = numericUpDown14;
			this.valueNumericUpDowns[14] = numericUpDown15;
			this.valueNumericUpDowns[15] = numericUpDown16;
			this.valueNumericUpDowns[16] = numericUpDown17;
			this.valueNumericUpDowns[17] = numericUpDown18;
			this.valueNumericUpDowns[18] = numericUpDown19;
			this.valueNumericUpDowns[19] = numericUpDown20;
			this.valueNumericUpDowns[20] = numericUpDown21;
			this.valueNumericUpDowns[21] = numericUpDown22;
			this.valueNumericUpDowns[22] = numericUpDown23;
			this.valueNumericUpDowns[23] = numericUpDown24;
			this.valueNumericUpDowns[24] = numericUpDown25;
			this.valueNumericUpDowns[25] = numericUpDown26;
			this.valueNumericUpDowns[26] = numericUpDown27;
			this.valueNumericUpDowns[27] = numericUpDown28;
			this.valueNumericUpDowns[28] = numericUpDown29;
			this.valueNumericUpDowns[29] = numericUpDown30;
			this.valueNumericUpDowns[30] = numericUpDown31;
			this.valueNumericUpDowns[31] = numericUpDown32;

			this.steptimeNumericUpDowns[0] = numericUpDown33;
			this.steptimeNumericUpDowns[1] = numericUpDown34;
			this.steptimeNumericUpDowns[2] = numericUpDown35;
			this.steptimeNumericUpDowns[3] = numericUpDown36;
			this.steptimeNumericUpDowns[4] = numericUpDown37;
			this.steptimeNumericUpDowns[5] = numericUpDown38;
			this.steptimeNumericUpDowns[6] = numericUpDown39;
			this.steptimeNumericUpDowns[7] = numericUpDown40;
			this.steptimeNumericUpDowns[8] = numericUpDown41;
			this.steptimeNumericUpDowns[9] = numericUpDown42;
			this.steptimeNumericUpDowns[10] = numericUpDown43;
			this.steptimeNumericUpDowns[11] = numericUpDown44;
			this.steptimeNumericUpDowns[12] = numericUpDown45;
			this.steptimeNumericUpDowns[13] = numericUpDown46;
			this.steptimeNumericUpDowns[14] = numericUpDown47;
			this.steptimeNumericUpDowns[15] = numericUpDown48;
			this.steptimeNumericUpDowns[16] = numericUpDown49;
			this.steptimeNumericUpDowns[17] = numericUpDown50;
			this.steptimeNumericUpDowns[18] = numericUpDown51;
			this.steptimeNumericUpDowns[19] = numericUpDown52;
			this.steptimeNumericUpDowns[20] = numericUpDown53;
			this.steptimeNumericUpDowns[21] = numericUpDown54;
			this.steptimeNumericUpDowns[22] = numericUpDown55;
			this.steptimeNumericUpDowns[23] = numericUpDown56;
			this.steptimeNumericUpDowns[24] = numericUpDown57;
			this.steptimeNumericUpDowns[25] = numericUpDown58;
			this.steptimeNumericUpDowns[26] = numericUpDown59;
			this.steptimeNumericUpDowns[27] = numericUpDown60;
			this.steptimeNumericUpDowns[28] = numericUpDown61;
			this.steptimeNumericUpDowns[29] = numericUpDown62;
			this.steptimeNumericUpDowns[30] = numericUpDown63;
			this.steptimeNumericUpDowns[31] = numericUpDown64;

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

			foreach (string frame in AllFrameList)
			{
				this.frameComboBox.Items.Add(frame);
			}

			for (int i = 0; i < FrameCount ; i++) {
				changeModeComboBoxes[i].Items.Clear();
				changeModeComboBoxes[i].Items.AddRange(new object[]{"跳变","渐变","屏蔽"});

				// 弃用此监听器：因为用鼠标拖动松开时，会额外多调节一些数值。=》改成使用valueChanged方法
				//vScrollBars[i].Scroll += new System.Windows.Forms.ScrollEventHandler(this.valueVScrollBar_Scroll_old);
				vScrollBars[i].ValueChanged += new System.EventHandler(this.valueVScrollBar_ValueChanged);
				vScrollBars[i].MouseEnter += new EventHandler(this.vScrollBar_MouseEnter);
				labels[i].MouseEnter += new EventHandler(this.tdLabel_MouseEnter);
				valueNumericUpDowns[i].MouseEnter += new EventHandler(this.valueNumericUpDown_MouseEnter);
				valueNumericUpDowns[i].MouseWheel += new MouseEventHandler(this.valueNumericUpDown_MouseWheel);
				steptimeNumericUpDowns[i].MouseEnter += new EventHandler(this.steptimeNumericUpDown_MouseEnter);
				steptimeNumericUpDowns[i].MouseWheel += new MouseEventHandler(this.steptimeNumericUpDown_MouseWheel);						
			}

			#endregion

			//动态加载可用的串口
			SerialPortTools comTools = SerialPortTools.GetInstance();
			comList = comTools.GetDMX512DeviceList();

			if (comList!=null && comList.Length > 0)
			{
				foreach (string com in comList)
				{
					comComboBox.Items.Add(com);
				}
				comComboBox.SelectedIndex = 0;
				comComboBox.Enabled = true;
				chooseComButton.Enabled = true;
			}
			else {
				comComboBox.SelectedIndex = -1;
				comComboBox.Enabled = false;
				chooseComButton.Enabled = false;
			}

			// 设置几个下拉框默认值
			modeComboBox.SelectedIndex = 0;
			frameComboBox.SelectedIndex = 0;
			cmComboBox.SelectedIndex = 0; // 统一跳渐变ComboBox

			isInit = true;					

		}
		
		private void Form1_Load(object sender, EventArgs e)
		{
			

		}		   
	

		

		/// <summary>
		///  辅助方法：将 save的两个按钮Enabled设为选定值
		/// </summary>
		protected override void enableSave(bool enable) {
			saveButton.Enabled = enable;
			exportButton.Enabled = enable;
		}

		/// <summary>
		///  辅助方法：将所有全局配置相关的按钮（灯具、全局、摇麦、网络）Enabled设为选定值
		/// </summary>
		/// <param name="v"></param>
		protected override void enableGlobalSet(bool enable)
		{
			connectButton.Enabled = enable;
			lightsEditToolStripMenuItem.Enabled = enable;
			updateToolStripMenuItem.Enabled = enable;
			globalSetToolStripMenuItem.Enabled = enable;
			ymSetToolStripMenuItem.Enabled = enable;

		}

		/// <summary>
		///  清空相关的所有数据
		/// </summary>
		protected override void clearAllData()
		{
			base.clearAllData();

			//单独针对本MainForm的代码: ①清空listView列表；②禁用《通道相关groupBox》
			lightsListView.Clear();
			tongdaoGroupBox.Hide();
		}
			
		
		/// <summary>
		/// 事件：点击《选择串口》后的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chooseCombutton_Click(object sender, EventArgs e)
		{
			playTools = PlayTools.GetInstance();
			comName = comComboBox.Text;
			if (!comName.Trim().Equals(""))
			{
				connectButton.Show();
			}
			else
			{
				MessageBox.Show("未选中可用串口");
			}
			connectButton.Enabled = true;
		}



		/// <summary>
		///  彻底退出应用，结束所有线程
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exitButton_Click(object sender, EventArgs e)
		{
			Exit();
		}

		/// <summary>
		/// 点击《打开工程》按钮 ==》新建一个OpenForm，再在里面回调OpenProject()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void openButton_Click(object sender, EventArgs e)
		{
			OpenForm openForm = new OpenForm(this,currentProjectName);
			openForm.ShowDialog();
		}
				
		/// <summary>
		/// 点击《新建工程》按钮：新建一个NewForm，再在里面回调InitProject()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newButton_Click(object sender, EventArgs e)
		{
			NewForm newForm = new NewForm(this);
			newForm.ShowDialog(this);

			//8.21 ：当IsCreateSuccess==true时，打开灯具编辑
			if (IsCreateSuccess)
			{
				lightsEditToolStripMenuItem1_Click(null, null);
			}
		}
		
		/// <summary>
		/// 点击《保存工程》按钮
		/// 保存需要进行的操作：
		/// 1.将lightAstList添加到light表中 --> 分新建或打开文件两种情况
		/// 2.将步数、素材、value表的数据都填进各自的表中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e) 		{ 
			saveAll();  
		}

	

		/// <summary>
		///添加lightAst列表到主界面内存中,主要供 LightsForm调用（以及OpenProject调用）
		/// --对比删除后，生成新的lightWrapperList；
		/// --lightListView也更新为最新的数据
		/// </summary>
		/// <param name="lightAstList2"></param>
		public override void AddLightAstList( IList<LightAst> lightAstList2)
		{
			// 0.先调用统一的操作，填充lightAstList和lightWrapperList
			base.AddLightAstList(lightAstList2);

			// 1.清空lightListView,重新填充新数据
			lightsListView.Items.Clear();			
			for (int i = 0; i < lightAstList2.Count; i++)
			{
				// 添加灯具数据到LightsListView中
				lightsListView.Items.Add(new ListViewItem(
					lightAstList2[i].LightName + ":" + lightAstList2[i].LightType,
					lightAstList2[i].LightPic
				));				
			}
			// 2.最后处理通道显示：每次调用此方法后应该隐藏通道数据，避免误操作。
			tongdaoGroupBox .Hide();
		}

		/// <summary>
		///  灯具列表选中项被改变后的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 必须判断这个字段(Count)，否则会报异常
			if (lightsListView.SelectedIndices.Count > 0)
			{
				selectedIndex = lightsListView.SelectedIndices[0];
				generateLightData();
				// 这里控制pasteLightButton的Enabled值
				checkIfCanCopyLight();
			}			
		}

	

		/// <summary>
		/// 辅助方法：初始化灯具数据。
		/// 0.先查看当前内存是否已有此数据 
		/// 1.若还未有，则取出相关的ini进行渲染
		/// 2.若内存内 或 数据库内已有相关数据，则使用这个数据。
		/// </summary>
		/// <param name="la"></param>
		private void generateLightData()
		{
			LightAst lightAst = lightAstList[selectedIndex];
			
			// 显示选中灯的部分信息在label中
			lightValueLabel.Text = lightAst.LightName + ":" + lightAst.LightType + "(" + lightAst.LightAddr + ")";

			//1.让tongdaoGroupBox显示出来
			this.tongdaoGroupBox.Show();

			//2.判断是不是已经有stepTemplate了了
			// ①若无，则生成数据，并hideAllTongdao 并设stepLabel为“0/0” --> 因为刚创建，肯定没有步数	
			// ②若有，还需判断该LightData的LightStepWrapperList[frame,mode]是不是为null
			//			若是null，则说明该FM下，并未有步数，hideAllTongdao
			//			若不为null，则说明已有数据，
			LightWrapper lightWrapper = lightWrapperList[selectedIndex];

			if (lightWrapper.StepTemplate == null)
			{				
				lightWrapper.StepTemplate = generateStepTemplate(lightAst);
				hideAllTongdao();
				showStepLabel(0, 0);				
			}			
			else
			{
				changeFrameMode();
			}
		}

		/// <summary>
		///  改变了模式和场景后的操作
		/// </summary>
		private void changeFrameMode()
		{
			if (selectedIndex == -1) {
				return;
			}

			LightWrapper lightWrapper = lightWrapperList[selectedIndex];
			LightStepWrapper lightStepWrapper = lightWrapper.LightStepWrapperList[frame, mode];

			// 为空或StepList数量是0
			if (lightStepWrapper == null || lightStepWrapper.StepWrapperList.Count == 0)
			{
				hideAllTongdao();
				showStepLabel(0, 0);
			}
			else // lightStepWrapper != null && lightStepWrapper.StepList.Count>0 : 也就是已经有值了
			{
				int currentStep = lightStepWrapper.CurrentStep;
				int totalStep = lightStepWrapper.TotalStep;

				StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[currentStep - 1];
				ShowVScrollBars(stepWrapper.TongdaoList, stepWrapper.StartNum);
				showStepLabel(currentStep, totalStep);
			}
		}

		/// <summary>
		/// 隐藏所有通道:包含五个部分 		
		/// </summary>
		private void hideAllTongdao()
		{
			for (int i = 0; i < 32; i++)
			{
				valueNumericUpDowns[i].Visible = false;
				labels[i].Visible = false;
				vScrollBars[i].Visible = false;
				changeModeComboBoxes[i].Visible = false ;
				steptimeNumericUpDowns[i].Visible = false;
			}
		}

		/// <summary>
		///  辅助方法：用来显示stepLabel-->当前步/总步数
		/// 7.2 +隐藏《删除步》按钮
		/// </summary>
		/// <param name="currentStep"></param>
		/// <param name="totalStep"></param>
		private  void showStepLabel(int currentStep, int totalStep)
		{
			// 1. 设label的Text值
			stepLabel.Text = currentStep + "/" + totalStep;
			
			// 2.1 设定《删除步》按钮是否可用
			deleteStepButton.Enabled = totalStep != 0;

			// 2.2 设定《追加步》、《前插入步》《后插入步》按钮是否可用			
			bool insertEnabled =  (mode == 0 && totalStep < 32) || ( mode == 1 && totalStep < 48 );
			addStepButton.Enabled = insertEnabled;				
			insertAfterStepButton.Enabled = insertEnabled;			
			insertBeforeStepButton.Enabled = insertEnabled &&  currentStep > 0;

			// 2.3 设定《上一步》《下一步》是否可用
			// -- 7.19修改为循环使用步数：
			backStepButton.Enabled = totalStep > 1;		
			nextStepButton.Enabled = totalStep >1 ;

			//2.4 设定《复制步》是否可用
			copyStepButton.Enabled = currentStep > 0;
			pasteStepButton.Enabled = currentStep > 0 && tempStep != null;
		}		


		/// <summary>
		/// 通过传来的数值，生成通道列表的数据
		/// </summary>
		/// <param name="tongdaoList"></param>
		/// <param name="startNum"></param>
		private void ShowVScrollBars(IList<TongdaoWrapper> tongdaoList, int startNum)
		{

			// 1.每次更换灯具，都先清空通道
			hideAllTongdao();

			// 2.判断tongdaoList，为null或数量为0时，设定deleteStepButton键不可用，并退出此方法
			if (tongdaoList == null  ||  tongdaoList.Count == 0) {
				deleteStepButton.Enabled = false;
				return;
			}//3.将dataWrappers的内容渲染到起VScrollBar中
			else
			{ 				
				for (int i = 0; i < tongdaoList.Count; i++)
				{
					TongdaoWrapper tongdaoWrapper = tongdaoList[i];

					this.labels[i].Text = (startNum + i) + "\n\n-\n " + tongdaoWrapper.TongdaoName;
					this.valueNumericUpDowns[i].Text = tongdaoWrapper.ScrollValue.ToString();
					this.vScrollBars[i].Value = 255 - tongdaoWrapper.ScrollValue;
					this.changeModeComboBoxes[i].SelectedIndex = tongdaoWrapper.ChangeMode;
					this.steptimeNumericUpDowns[i].Text = tongdaoWrapper.StepTime.ToString();

					this.vScrollBars[i].Show();
					this.labels[i].Show();
					this.valueNumericUpDowns[i].Show();
					this.changeModeComboBoxes[i].Show();
					this.steptimeNumericUpDowns[i].Show();
									
				}

				// 4. tongdaoGroupBoxX的显示：0：不显示； 1-16：仅显示通道groupBox1； 16-32：两个通道groupBox都显示；
				if (tongdaoList.Count > 16)
				{
					tongdaoGroupBox1.Visible = true;
					tongdaoGroupBox2.Visible = true;
				} // 若通道总数<=16 且 >0，则显示上面的标签
				else if (tongdaoList.Count > 0)
				{
					tongdaoGroupBox1.Visible = true;
					tongdaoGroupBox2.Visible = false;
				}
				else {
					tongdaoGroupBox1.Visible = false;
					tongdaoGroupBox2.Visible = false;
				}
			}
		}
		
		/// <summary>
		/// 点击《追加步》的操作：在最后步后插入新步
		/// 修改：应该获取当前最大步的数据，然后用这个步数据填充新的步，而非当前步
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addStepButton_Click(object sender, EventArgs e)
		{
			LightWrapper currentLightWrapper = getCurrentLightWrapper();

			// 如果此值为空，则创建之
			if (currentLightWrapper.LightStepWrapperList[frame, mode] == null)
			{
				currentLightWrapper.LightStepWrapperList[frame, mode] = new LightStepWrapper()
				{
					StepWrapperList = new List<StepWrapper>()
				};
			}

			// 根据isUseStepMode，生成要插入步的内容 => (两种传入模板的写法：效果是一样的)
			StepWrapper newStep = StepWrapper.GenerateNewStep(
				// 写法1：比较冗长
				// isUseStepMode ? getCurrentStepMode() : (getCurrentStepWrapper() == null ? getCurrentStepMode() : getCurrentStepWrapper()), 
				// 写法2：	相对简洁
				(isUseStepTemplate || getCurrentStepWrapper()==null ) ? getCurrentStepTemplate() : getCurrentStepWrapper(),
				mode);			

			// 调用包装类内部的方法,来追加步
			currentLightWrapper.LightStepWrapperList[frame, mode]. AddStep(newStep);

			// 显示新步
			this.ShowVScrollBars(newStep.TongdaoList, newStep.StartNum);
			this.showStepLabel(currentLightWrapper.LightStepWrapperList[frame, mode].CurrentStep, currentLightWrapper.LightStepWrapperList[frame, mode].TotalStep);

		}
		
		/// <summary>
		/// 插入步(前插或后插由触发键的Name决定)的操作：前插和后插都调用同一个方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void insertStepButton_Click(object sender, EventArgs e)
		{
			// 1.获取当前步与最高值，总步数			
			// 若当前步 <= 总步数，则可以插入，并将之后的步数往后移动
			// 否则报错
			
			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			if (lsWrapper.CurrentStep <= lsWrapper.TotalStep) {
				// 根据isUseStepMode，生成要插入步的内容
				StepWrapper newStep = StepWrapper.GenerateNewStep(
					(isUseStepTemplate || getCurrentStepWrapper() == null) ? getCurrentStepTemplate() : getCurrentStepWrapper() ,
					mode
				);
				// 要插入的位置的index
				int stepIndex = getCurrentStep() - 1 ;
				// 插入的方式：前插(true）还是后插（false)
				bool insertBefore = ((Button)sender).Name.Equals("insertBeforeStepButton");

				lsWrapper.InsertStep(stepIndex, newStep, insertBefore);

				this.ShowVScrollBars(newStep.TongdaoList, newStep.StartNum);
				this.showStepLabel(lsWrapper.CurrentStep, lsWrapper.TotalStep);				
			}
			else
			{
				MessageBox.Show("Dickov:当前步大于总步数");
			}					   
		}	
		
		/// <summary>
		///  删除步的操作
		///  1.获取当前步，当前步对应的stepIndex
		///  2.通过stepIndex，DeleteStep(index);
		///  3.获取新步(step删除后会自动生成新的)，并重新渲染stepLabel和vScrollBars
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteStepButton_Click(object sender, EventArgs e)
		{			
			LightWrapper lightWrapper = lightWrapperList[selectedIndex];
			LightStepWrapper lightStepWrapper = lightWrapper.LightStepWrapperList[frame, mode];
			int stepIndex = getCurrentStep() - 1;

			// 调用包装类内部的方法:删除某一步
			try
			{
				lightStepWrapper.DeleteStep(stepIndex);
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
				return; 
			}
			
			int currentStep = lightStepWrapper.CurrentStep;
			if (currentStep > 0 ) {
				StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[currentStep - 1];
				this.ShowVScrollBars(stepWrapper.TongdaoList, stepWrapper.StartNum);
				this.showStepLabel(lightStepWrapper.CurrentStep, lightStepWrapper.TotalStep);
			}
			else{
				this.ShowVScrollBars(null, 0);
				this.showStepLabel(0, 0);				
			}			
		}		

		/// <summary>
		///  场景改变后，调用这个方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			frame = frameComboBox.SelectedIndex;	

			if (lightAstList != null && lightAstList.Count > 0)
			{
				changeFrameMode();
			}
			
		}

		/// <summary>
		/// 改变常规或声控模式后才需要进行某些操作
		/// 1.改label的text ；2.改changeModeComboBoxes的列表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void modeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isInit)
			{
				mode = modeComboBox.SelectedIndex;
			// 若模式为声控模式
			// 1.改变几个label的Text; 
			// 2.改变跳变渐变-->是否声控；
			// 3.所有步时间值的调节，改为enabled=false			
				if (mode == 1)
				{
					changeModeLabel.Text = "是否声控";
					changeModeLabel2.Text = "是否声控";
					
					for (int i = 0; i < 32; i++)
					{
						this.changeModeComboBoxes[i].Items.Clear();
						this.changeModeComboBoxes[i].Items.AddRange(new object[] {
						"否",
						"是"});
						this.steptimeNumericUpDowns[i].Enabled = false;
					}
					changeModeButton.Text = "统一声控";

					cmComboBox.Items.Clear();
					cmComboBox.Items.AddRange(new object[] {
						"否",
						"是"}
					);
					cmComboBox.SelectedIndex = 0;
				}
				else //mode=0
				{
					changeModeLabel.Text = "变化方式";
					changeModeLabel2.Text = "变化方式";				
					for (int i = 0; i < 32; i++)
					{
						this.changeModeComboBoxes[i].Items.Clear();
						this.changeModeComboBoxes[i].Items.AddRange(new object[] {
								"跳变",
								"渐变"});
						this.steptimeNumericUpDowns[i].Enabled = true;
					}
					changeModeButton.Text = "统一跳渐变";

					cmComboBox.Items.Clear();
					cmComboBox.Items.AddRange(new object[] {
								"跳变",
								"渐变"});
					cmComboBox.SelectedIndex = 0;
				}
				if (lightAstList != null && lightAstList.Count > 0)
				{
					changeFrameMode();
				}
			}			
		}

		/// <summary>
		///  点击上一步的操作：先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backStepButton_Click(object sender, EventArgs e)
		{
			int currentStepValue = getCurrentStep();
			if (currentStepValue > 1)
			{
				chooseStep(currentStepValue - 1);
			}
			else
			{
				chooseStep( getTotalStep() );
			}
		}

		/// <summary>
		/// 点击下一步的操作：先判断currentStep，再调用chooseStep(stepValue)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nextStepButton_Click(object sender, EventArgs e)
		{
			int currentStepValue = getCurrentStep();
			int totalStepValue = getTotalStep();
			if (currentStepValue < totalStepValue)
			{
				chooseStep(currentStepValue + 1);
			}
			else
			{
				chooseStep( 1 );
			}
		}

		/// <summary>
		/// 辅助方法：抽象了【选择某一个指定步数后，统一的操作；NextStep和BackStep等应该都使用这个方法】
		/// </summary>
		protected override void chooseStep(int stepNum)
		{
			LightStepWrapper lightStepWrapper = getCurrentLightStepWrapper();
			StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[stepNum - 1];
			lightStepWrapper.CurrentStep = stepNum;

			this.ShowVScrollBars(stepWrapper.TongdaoList, stepWrapper.StartNum);
			this.showStepLabel(lightStepWrapper.CurrentStep, lightStepWrapper.TotalStep);

			if (isConnected && isRealtime)
			{
				oneLightStepWork();
			}			
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
			// 只能有一个GlobalSetForm，在点击全局设置时新建(为生成过或已被销毁)，或在Hide时显示
			GlobalSetForm globalSetForm = new GlobalSetForm(this, globalIniPath);
			globalSetForm.ShowDialog();
		}
			   
		/// <summary>
		///  点击菜单栏中灯具编辑时的动作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightsEditToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			LightsForm lightsForm = new LightsForm(this, lightAstList);
			lightsForm.ShowDialog();
		}

		/// <summary>
		///  点击灯库编辑时，可以打开另外一个软件LightEditor
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lightLibraryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IniFileAst iniFileAst = new IniFileAst(Application.StartupPath + @"\GlobalSet.ini");
			string lightEditor = iniFileAst.ReadString("Link", "lightEditor","0");
			if (lightEditor.Equals("0"))
			{
				MessageBox.Show("Dickov：尚未设置关联");				
			}
			else if(lightEditor.Equals("1")) //设1时选择默认的启动目录中的LightEditor.exe
			{			
				System.Diagnostics.Process.Start(Application.StartupPath + @"\LightEditor.exe");
			}
			else// 其余情况下，启动设置的string
			{
				System.Diagnostics.Process.Start(lightEditor);
			}			
		}

		/// <summary>
		///  用户移动滚轴时发生：1.调节相关的numericUpDown; 2.放进相关的step中
		///  --拖拽的时候，这里会运行多次
		///  --7.19:弃用此方法：改用ValueChanged监听器
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueVScrollBar_Scroll_old(object sender, ScrollEventArgs e)
		{			
			// 1.先找出对应vScrollBars的index 
			int tongdaoIndex = MathAst.getIndexNum(((VScrollBar)sender).Name , -1 );
			int tdValue = 255 - vScrollBars[tongdaoIndex].Value;

			//2.把滚动条的值赋给valueNumericUpDowns
			valueNumericUpDowns[tongdaoIndex].Value = tdValue;
			 Console.WriteLine("Dickov:valueVScrollBar_Scroll：" + (255 - vScrollBars[tongdaoIndex].Value));

			//3.取出recentStep,使用取出的index，给stepWrapper.TongdaoList[index]赋值；并检查是否实时生成数据进行操作
			changeScrollValue(tongdaoIndex , tdValue);
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
			int tdValue = 255 - vScrollBars[tongdaoIndex].Value;

			//2.把滚动条的值赋给valueNumericUpDowns
			valueNumericUpDowns[tongdaoIndex].ValueChanged -= new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			valueNumericUpDowns[tongdaoIndex].Value = tdValue;
			valueNumericUpDowns[tongdaoIndex].ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			//Console.WriteLine("通道："+tongdaoIndex+"valueVScrollBar_ValueChanged：" + (255 - vScrollBars[tongdaoIndex].Value));

			//3.取出recentStep,使用取出的index，给stepWrapper.TongdaoList[index]赋值；并检查是否实时生成数据进行操作
			changeScrollValue(tongdaoIndex,tdValue);
			Console.WriteLine("vScrollBar_ValueChanged");
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
			int tdValue = Decimal.ToInt32(valueNumericUpDowns[tongdaoIndex].Value);

			// 2.调整相应的vScrollBar的数值
			vScrollBars[tongdaoIndex].ValueChanged -= new System.EventHandler(this.valueVScrollBar_ValueChanged);
			vScrollBars[tongdaoIndex].Value = 255 - Decimal.ToInt32( valueNumericUpDowns[tongdaoIndex].Value ) ;
			vScrollBars[tongdaoIndex].ValueChanged += new System.EventHandler(this.valueVScrollBar_ValueChanged);

			//3.取出recentStep,使用取出的index，给stepWrapper.TongdaoList[index]赋值；并检查是否实时生成数据进行操作
			changeScrollValue(tongdaoIndex,tdValue);
			Console.WriteLine("numericUpDown_ValueChanged");
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
		
			//if (isInit)
			//{
			//	// 3.（6.29修改）若当前模式是声控模式：
			//	//		则更改其中某一个通道的是否声控的值，则此通道的所有声控步，都要统一改变其是否声控值
			//	if ( mode == 1)
			//	{
			//		IList<StepWrapper> stepWrapperList = getCurrentLightStepWrapper().StepWrapperList;
			//		foreach (StepWrapper stepWrapper in stepWrapperList)
			//		{
			//			stepWrapper.TongdaoList[index].ChangeMode = changeModeComboBoxes[index].SelectedIndex;
			//		}
			//	}
			//	// 4.(8.8新增判断）若当前模式是普通模式：
			//	//		被屏蔽掉的通道，其数值不再可以改动;否则可以调整
			//	//else{
			//	//	enableTongdaoEdit(index,changeModeComboBoxes[index].SelectedIndex != 2) ;
			//	//}
			//}
		}


		/// <summary>
		///  辅助方法:根据当前《 变动方式》选项 是否屏蔽，处理相关通道是否可设置
		/// </summary>
		/// <param name="tongdaoIndex">tongdaoList的Index</param>
		/// <param name="shielded">是否被屏蔽</param>
		private void enableTongdaoEdit(int tongdaoIndex,bool shielded)
		{
			vScrollBars[tongdaoIndex].Enabled = shielded;
			valueNumericUpDowns[tongdaoIndex].Enabled = shielded;
			steptimeNumericUpDowns[tongdaoIndex].Enabled = shielded;
		}

		/// <summary>
		/// 每个通道对应的"步时间"值变化后，对应的tongdaoWrapper也改变参数
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void stepTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			// 1.先找出对应stepNumericUpDowns的index（这个比较麻烦，因为其NumericUpDown的序号是从33开始的 即： name33 = names[0] =>addNum = -33）
			int index = MathAst.getIndexNum(((NumericUpDown)sender).Name, -33);

			//2.取出recentStep，这样就能取出一个步数，使用取出的index，给stepWrapper.TongdaoList[index]赋值
			StepWrapper step = getCurrentStepWrapper();
			step.TongdaoList[index].StepTime = Decimal.ToInt32(steptimeNumericUpDowns[index].Value);			
		}

	
		/// <summary>
		/// 复制步：
		/// 1.从项目中选择当前灯的当前步，(若当前步为空，则无法复制），把它赋给tempStep数据。
		/// 2.若复制成功，则《粘贴步》按钮可用
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void copyStepButton_Click(object sender, EventArgs e)
		{			
			if (getCurrentStepWrapper() == null) {
				MessageBox.Show("当前步数据为空，无法复制");
			}
			else
			{
				tempStep = getCurrentStepWrapper();
				pasteStepButton.Enabled = true;
			}
		}

		/// <summary>
		/// 粘帖步：从复制的步拷贝到当前步
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pasteStepButton_Click(object sender, EventArgs e)
		{
			// 1. 先判断是不是同模式及同一种灯具（非同一个灯具也可以复制，但需类型(同一个灯库内容)一样)
			StepWrapper currentStep = getCurrentStepWrapper();
			if(currentStep == null)
			{
				MessageBox.Show("当前步数据为空，无法粘贴步");
				return;
			}
			if (currentStep.LightMode != tempStep.LightMode) {
				MessageBox.Show("不同模式下无法复制步");
				return;
			}
			if (currentStep.LightFullName != tempStep.LightFullName) { 
				MessageBox.Show("不同灯具无法复制步");
				return;
			}

			// 2.逐一将TongdaoList的某些数值填入tempStep中，而非粗暴地将currentStep 设为tempStep
			for (int i = 0; i < tempStep.TongdaoList.Count; i++)
			{
				currentStep.TongdaoList[i].ScrollValue = tempStep.TongdaoList[i].ScrollValue;
				currentStep.TongdaoList[i].ChangeMode = tempStep.TongdaoList[i].ChangeMode;
				currentStep.TongdaoList[i].StepTime = tempStep.TongdaoList[i].StepTime;
			}

			// 3.重新渲染当前步的所有通道
			ShowVScrollBars(currentStep.TongdaoList, currentStep.StartNum);
		}		

		/// <summary>
		/// 实时预览所有灯的变化：通过dbWrapper（实时封装）来生成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewButton_Click(object sender, EventArgs e)
		{
			DBWrapper allData = GetDBWrapper(false);
			try
			{
				playTools.PreView(allData, globalIniPath, frame);
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
			
		private void stopReviewButton_Click(object sender, EventArgs e)
		{			
			playTools.EndView();
		}

		/// <summary>
		///  统一跳渐变按钮点击后操作:所有当前步的跳变都设为选定值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void	changeModeButton_Click(object sender, EventArgs e)
		{
			StepWrapper step = getCurrentStepWrapper();
			for (int i = 0; i < step.TongdaoList.Count; i++)
			{
				changeModeComboBoxes[i].SelectedIndex = cmComboBox.SelectedIndex;
			}
		}

		/// <summary>
		/// 事件：点击《 统一步时间》按钮
		/// 操作:所有当前步的步时间都设为选定值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void steptimeSetButton_Click(object sender, EventArgs e)
		{
			StepWrapper step = getCurrentStepWrapper();
			for (int i = 0; i < step.TongdaoList.Count; i++)
			{
				steptimeNumericUpDowns[i].Value = stNumericUpDown.Value ;
			}
		}
		
		/// <summary>
		///  事件：点击《单灯单步》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void oneLightStepButton_Click(object sender, EventArgs e)
		{
			oneLightStepWork();
		}

		/// <summary>
		/// 事件：当勾选《实时调试》后，设ifRealTime为true
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void realTimeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			isRealtime = realTimeCheckBox.Checked;
		}		

		/// <summary>
		///  事件：点击《摇麦设置》后，打开摇麦设置Form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ymSetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			YMSetForm ymSetForm = new YMSetForm(this, globalIniPath);
			ymSetForm.ShowDialog();
		}

		/// <summary>
		/// 辅助方法 : 鼠标掠过vScrollBar时，把焦点切换到其对应的numericUpDown中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void vScrollBar_MouseEnter(object sender, EventArgs e)
		{
			int tdIndex = MathAst.getIndexNum( ((VScrollBar)sender).Name ,-1 );
			valueNumericUpDowns[tdIndex].Select();
		}

		/// <summary>
		/// 辅助方法:鼠标进入通道相关label时，把焦点切换到其numericUpDown中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdLabel_MouseEnter(object sender, EventArgs e)
		{
			int labelIndex = MathAst.getIndexNum(((Label)sender).Name, -1);
			valueNumericUpDowns[labelIndex].Select();
		}		

		/// <summary>
		///  辅助方法：鼠标滚动时，通道值每次只变动一个Increment值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			int tdIndex = MathAst.getIndexNum(((NumericUpDown)sender).Name, -1);
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				//获取或设置是否应将此事件转发到控件的父容器。
				// public bool Handled { get; set; } ==> 如果鼠标事件应转到父控件，则为 true；否则为 false。
				// Dickov: 实际上就是当Handled设为true时，不再触发本控件的默认相关操作(即屏蔽滚动事件)
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
		///  辅助方法：鼠标滚动时，步时间值每次只变动一个Increment值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void steptimeNumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			int tdIndex = MathAst.getIndexNum(((NumericUpDown)sender).Name, -33);
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{		
				hme.Handled = true;
			}
			if (e.Delta > 0)
			{
				decimal dd = steptimeNumericUpDowns[tdIndex].Value + steptimeNumericUpDowns[tdIndex].Increment;
				if (dd <= steptimeNumericUpDowns[tdIndex].Maximum)
				{
					steptimeNumericUpDowns[tdIndex].Value = dd;
				}
			}
			else if (e.Delta < 0)
			{
				decimal dd = steptimeNumericUpDowns[tdIndex].Value - steptimeNumericUpDowns[tdIndex].Increment;
				if (dd >= steptimeNumericUpDowns[tdIndex].Minimum)
				{
					steptimeNumericUpDowns[tdIndex].Value = dd;
				}
			}
		}		

		/// <summary>
		/// 辅助方法：鼠标进入步时间输入框时，切换焦点;
		/// 注意：用MouseEnter事件，而非MouseHover事件;这样才会无延时响应
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void steptimeNumericUpDown_MouseEnter(object sender, EventArgs e)
		{		
			int tdIndex = MathAst.getIndexNum(((NumericUpDown)sender).Name, -33);
			steptimeNumericUpDowns[tdIndex].Select();
		}

		/// <summary>
		/// 辅助方法：鼠标进入通道值输入框时，切换焦点;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void valueNumericUpDown_MouseEnter(object sender, EventArgs e)
		{
			int tdIndex = MathAst.getIndexNum(((NumericUpDown)sender).Name, -1);
			valueNumericUpDowns[tdIndex].Select();
		}
		

		/// <summary>
		///  保存素材操作：
		///  1.往MaterialForm传light和tongdaoList
		///  2.materialForm.showDialog()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private MaterialSaveForm materialForm;
		private void materialSaveButton_Click(object sender, EventArgs e)
		{
			materialForm = null;
			//TODO：需要额外处理保存素材（共性及个性）
			materialForm = new MaterialSaveForm(this,getCurrentLightStepWrapper().StepWrapperList,mode ,"");
			if (materialForm != null && !materialForm.IsDisposed) {
				materialForm.ShowDialog();
			}
		}

		/// <summary>
		///  使用素材：
		///  打开 MaterialUseForm
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private MaterialUseForm materialUseForm;
		private void materialUseButton_Click(object sender, EventArgs e)
		{
			materialUseForm = null;
			materialUseForm = new MaterialUseForm(this,mode);
			materialUseForm.ShowDialog();			
		}
		
		/// <summary>
		///  曾维佳测试用按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newTestButton_Click(object sender, EventArgs e)
		{
			int buttonIndex = MathAst.getIndexNum(((Button)sender).Name, 0);
			Console.WriteLine(buttonIndex);
			Test test = new Test(GetDBWrapper(true));
			test.Start(buttonIndex);
		}

		/// <summary>
		///  触发音频的按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void soundButton_Click(object sender, EventArgs e)
		{
			playTools.MusicControl();	
		}		

		/// <summary>
		///  点击《复制灯》：
		///  1.应有个全局变量lightWrapperTemp，记录要被复制的灯的信息
		///  2. 将当前选中灯具的内容，赋予lightWrapperList
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void copyLightButton_Click(object sender, EventArgs e)
		{				
			if (getCurrentLightWrapper() == null) {
				MessageBox.Show("未选中灯，无法复制");
				return;
			}
			tempLight = getCurrentLightWrapper();
		}

		/// <summary>
		///  点击《粘贴灯》
		///  1. 比对选中灯和复制的灯
		///	--①不一致，弹错误
		///	--②一致，想办法把tempLight的数据复制到选中灯中
		/// 2. generateLightData()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pasteLightButton_Click(object sender, EventArgs e)
		{			
			// 多加了一层常规情况下不会出现的判断，因为此时这个按钮不可用
			if (checkIfCanCopyLight()) {
				LightWrapper selectedLight = getCurrentLightWrapper();
				lightWrapperList[selectedIndex] = LightWrapper.CopyLight(tempLight,selectedLight);
				generateLightData();
			}
			else
			{
				//一般不会进到这里来，因为当checkIfCanCopy=false时，此按钮不可以点击
				MessageBox.Show("选中灯具与要复制的灯具种类不同,无法复制!");
			}
		}


		/// <summary>
		///  辅助方法：检查是否可以复制灯
		/// </summary>
		private bool checkIfCanCopyLight()
		{
			pasteLightButton.Enabled = false;
			LightWrapper selectedLight = getCurrentLightWrapper();
			// 只有在选中灯不为空 且 要被复制的灯与选中灯是同一种灯具时，才能复制
			if (selectedLight != null && tempLight!=null) {
				if (tempLight.StepTemplate.LightFullName == selectedLight.StepTemplate.LightFullName) {
					pasteLightButton.Enabled = true;
					return true;					
				}
			}
			return false;
		}


		/// <summary>
		/// 点击《全部归零》：当前步的所有scrollValue值设为0
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zeroButton_Click(object sender, EventArgs e)
		{
			StepWrapper step = getCurrentStepWrapper();
			for (int i = 0; i < step.TongdaoList.Count; i++)
			{
				valueNumericUpDowns[i].Value = 0;
			}
		}

		/// <summary>
		///  点击《统一通道值》：当前步的scrollValue统一（由commonValueNumericUpDown的Value决定）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commonValueButton_Click(object sender, EventArgs e)
		{
			StepWrapper step = getCurrentStepWrapper();
			for (int i = 0; i < step.TongdaoList.Count; i++)
			{
				valueNumericUpDowns[i].Value = commonValueNumericUpDown.Value;
			}
		}

		/// <summary>
		/// 点击《全部初始化》当前步的所有scrollValue值设为初始值(从StepMode复制)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void initButton_Click(object sender, EventArgs e)
		{
			StepWrapper step = getCurrentStepWrapper();
			StepWrapper stepMode = getCurrentStepTemplate();
			for (int i = 0; i < step.TongdaoList.Count; i++)
			{
				valueNumericUpDowns[i].Value = stepMode.TongdaoList[i].ScrollValue;
			}
		}

		
		/// <summary>
		///  勾选是否使用模板数据
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addStepCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			isUseStepTemplate = addStepCheckBox.Checked;
		}	
		
		/// <summary>
		///  点击《连接设备|断开连接》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectButton_Click(object sender, EventArgs e)
		{
			// 如果还没连接（按钮显示为“连接设备”)，那就连接
			if ( !isConnected )
			{
				playTools.ConnectDevice(comName);
				setDMX512TestButtonsEnable(true);
				chooseComButton.Enabled = false;
				connectButton.Text = "断开连接";
				isConnected = true;
			}
			else //否则( 按钮显示为“断开连接”）断开连接
			{
				playTools.CloseDevice();
				setDMX512TestButtonsEnable(false);
				chooseComButton.Enabled = true;
				connectButton.Text = "连接设备";
				isConnected = false;
			}
		}

		/// <summary>
		///  辅助方法：一次性配置DMX512调试按钮组可见与否
		/// </summary>
		/// <param name="visible"></param>
		private void setDMX512TestButtonsEnable(bool visible) {
			realTimeCheckBox.Visible = visible;
			oneLightStepButton.Visible = visible;
			previewButton.Visible = visible;
			soundButton.Visible = visible;
			stopReviewButton.Visible = visible;			
		}
		
		/// <summary>
		///  点击《在线升级》菜单栏：
		///  新建一个UpdateForm, ShowDialog();
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void updateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool isFromDB = true;
			UpdateForm updateForm = new UpdateForm(this, GetDBWrapper(isFromDB), globalIniPath); 
			updateForm.ShowDialog(); 
		}

		/// <summary>
		///  点击《硬件设置》-《打开配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hardwareSetOpenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			HardwareSetChooseForm hscForm = new HardwareSetChooseForm(this);
			hscForm.ShowDialog();
		}

		/// <summary>
		///  点击《硬件设置》-《新建配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hardwareSetNewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			HardwareSetForm hsForm = new HardwareSetForm(this, null,null);
			hsForm.ShowDialog();
		}
		
		/// <summary>
		///  点击《其他工具》- 《传视界灯控工具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void QDControllerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Application.StartupPath + @"\QDController\灯光控制器.exe");
		}

		/// <summary>
		///  点击《其他工具》- 《传视界中控工具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CenterControllerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Application.StartupPath + @"\CenterController\KTV中央控制器.exe");
		}

		/// <summary>
		///  点击《其他工具》- 《传视界墙板工具》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void KeyPressToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(Application.StartupPath + @"\KeyPress\墙板码值.exe");
		}

		/// <summary>
		///  点击《其他工具》- 《提示》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CSJToolNoticeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("1.使用传视界工具，需要安装相应驱动，并保持设备连接；\n" +
				"2.系统必须安装MicroSoft Office Excel才可使用中控工具。");
		}

		/// <summary>
		///  点击切换皮肤
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void skinButton_Click(object sender, EventArgs e)
		{
			string sskName = skinComboBox.Text;
			this.skinEngine1.SkinFile = Application.StartupPath + "\\irisSkins\\" + sskName + ".ssk";
		}

		/// <summary>
		/// 事件：点击《导出工程》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exportButton_Click(object sender, EventArgs e)
		{			
			DBWrapper dbWrapper = GetDBWrapper(true);
			string exportPath = savePath + @"\ExportDirectory\" + currentProjectName + @"\CSJ";

			FileTools fileTools = FileTools.GetInstance();
			fileTools.ProjectToFile(dbWrapper, globalIniPath, exportPath);

			//导出成功后，打开文件夹
			System.Diagnostics.Process.Start(exportPath);			
		}

		private void hardwareUpdateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new HardwareUpdateForm(this).ShowDialog();
		}
	}
}