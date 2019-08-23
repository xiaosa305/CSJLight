using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin.SkinControl;
using LightController.Common;

namespace LightController.MyForm
{
	public partial class GlobalSetForm : Form
	{
		public MainFormInterface mainForm;
		private string iniFilePath;
		private IniFileAst iniAst ;
		private bool isInit = false;
		private int frameIndex = -1;

		public GlobalSetForm(MainFormInterface mainForm,string iniFilePath) {

			this.mainForm = mainForm;
			this.iniFilePath = iniFilePath;

			InitializeComponent();

			#region 初始化辅助数组，及其他默认选项

			// 初始化强电控制器的八个开关到数组中
			this.qdCheckBoxes[0] = this.checkBox1;
			this.qdCheckBoxes[1] = this.checkBox2;
			this.qdCheckBoxes[2] = this.checkBox3;
			this.qdCheckBoxes[3] = this.checkBox4;
			this.qdCheckBoxes[4] = this.checkBox5;
			this.qdCheckBoxes[5] = this.checkBox6;
			this.qdCheckBoxes[6] = this.checkBox7;
			this.qdCheckBoxes[7] = this.checkBox8;


			//// 初始化声控的属性到数组中
			this.mSkinButtons[0] = mSkinButton1;
			this.mSkinButtons[1] = mSkinButton2;
			this.mSkinButtons[2] = mSkinButton3;
			this.mSkinButtons[3] = mSkinButton4;
			this.mSkinButtons[4] = mSkinButton5;
			this.mSkinButtons[5] = mSkinButton6;
			this.mSkinButtons[6] = mSkinButton7;
			this.mSkinButtons[7] = mSkinButton8;
			this.mSkinButtons[8] = mSkinButton9;
			this.mSkinButtons[9] = mSkinButton10;
			this.mSkinButtons[10] = mSkinButton11;
			this.mSkinButtons[11] = mSkinButton12;
			this.mSkinButtons[12] = mSkinButton13;
			this.mSkinButtons[13] = mSkinButton14;
			this.mSkinButtons[14] = mSkinButton15;
			this.mSkinButtons[15] = mSkinButton16;
			this.mSkinButtons[16] = mSkinButton17;
			this.mSkinButtons[17] = mSkinButton18;
			this.mSkinButtons[18] = mSkinButton19;
			this.mSkinButtons[19] = mSkinButton20;
			this.mSkinButtons[20] = mSkinButton21;
			this.mSkinButtons[21] = mSkinButton22;
			this.mSkinButtons[22] = mSkinButton23;
			this.mSkinButtons[23] = mSkinButton24;

			this.frameStepTimeNumericUpDowns[0] = frameStepTimeNumericUpDown1;
			this.frameStepTimeNumericUpDowns[1] = frameStepTimeNumericUpDown2;
			this.frameStepTimeNumericUpDowns[2] = frameStepTimeNumericUpDown3;
			this.frameStepTimeNumericUpDowns[3] = frameStepTimeNumericUpDown4;
			this.frameStepTimeNumericUpDowns[4] = frameStepTimeNumericUpDown5;
			this.frameStepTimeNumericUpDowns[5] = frameStepTimeNumericUpDown6;
			this.frameStepTimeNumericUpDowns[6] = frameStepTimeNumericUpDown7;
			this.frameStepTimeNumericUpDowns[7] = frameStepTimeNumericUpDown8;
			this.frameStepTimeNumericUpDowns[8] = frameStepTimeNumericUpDown9;
			this.frameStepTimeNumericUpDowns[9] = frameStepTimeNumericUpDown10;
			this.frameStepTimeNumericUpDowns[10] = frameStepTimeNumericUpDown11;
			this.frameStepTimeNumericUpDowns[11] = frameStepTimeNumericUpDown12;
			this.frameStepTimeNumericUpDowns[12] = frameStepTimeNumericUpDown13;
			this.frameStepTimeNumericUpDowns[13] = frameStepTimeNumericUpDown14;
			this.frameStepTimeNumericUpDowns[14] = frameStepTimeNumericUpDown15;
			this.frameStepTimeNumericUpDowns[15] = frameStepTimeNumericUpDown16;
			this.frameStepTimeNumericUpDowns[16] = frameStepTimeNumericUpDown17;
			this.frameStepTimeNumericUpDowns[17] = frameStepTimeNumericUpDown18;
			this.frameStepTimeNumericUpDowns[18] = frameStepTimeNumericUpDown19;
			this.frameStepTimeNumericUpDowns[19] = frameStepTimeNumericUpDown20;
			this.frameStepTimeNumericUpDowns[20] = frameStepTimeNumericUpDown21;
			this.frameStepTimeNumericUpDowns[21] = frameStepTimeNumericUpDown22;
			this.frameStepTimeNumericUpDowns[22] = frameStepTimeNumericUpDown23;
			this.frameStepTimeNumericUpDowns[23] = frameStepTimeNumericUpDown24;

			this.jgtNumericUpDowns[0] = jgtNumericUpDown1;
			this.jgtNumericUpDowns[1] = jgtNumericUpDown2;
			this.jgtNumericUpDowns[2] = jgtNumericUpDown3;
			this.jgtNumericUpDowns[3] = jgtNumericUpDown4;
			this.jgtNumericUpDowns[4] = jgtNumericUpDown5;
			this.jgtNumericUpDowns[5] = jgtNumericUpDown6;
			this.jgtNumericUpDowns[6] = jgtNumericUpDown7;
			this.jgtNumericUpDowns[7] = jgtNumericUpDown8;
			this.jgtNumericUpDowns[8] = jgtNumericUpDown9;
			this.jgtNumericUpDowns[9] = jgtNumericUpDown10;
			this.jgtNumericUpDowns[10] = jgtNumericUpDown11;
			this.jgtNumericUpDowns[11] = jgtNumericUpDown12;
			this.jgtNumericUpDowns[12] = jgtNumericUpDown13;
			this.jgtNumericUpDowns[13] = jgtNumericUpDown14;
			this.jgtNumericUpDowns[14] = jgtNumericUpDown15;
			this.jgtNumericUpDowns[15] = jgtNumericUpDown16;
			this.jgtNumericUpDowns[16] = jgtNumericUpDown17;
			this.jgtNumericUpDowns[17] = jgtNumericUpDown18;
			this.jgtNumericUpDowns[18] = jgtNumericUpDown19;
			this.jgtNumericUpDowns[19] = jgtNumericUpDown20;
			this.jgtNumericUpDowns[20] = jgtNumericUpDown21;
			this.jgtNumericUpDowns[21] = jgtNumericUpDown22;
			this.jgtNumericUpDowns[22] = jgtNumericUpDown23;
			this.jgtNumericUpDowns[23] = jgtNumericUpDown24;



			// 初始化多场景组合播放输入项
			this.frameComboBoxes[0] = frame1ComboBox;
			this.frameComboBoxes[1] = frame2ComboBox;
			this.frameComboBoxes[2] = frame3ComboBox;
			this.frameComboBoxes[3] = frame4ComboBox;

			this.frameNumericUpDowns[0] = frame1numericUpDown;
			this.frameNumericUpDowns[1] = frame2numericUpDown;
			this.frameNumericUpDowns[2] = frame3numericUpDown;
			this.frameNumericUpDowns[3] = frame4numericUpDown;

			//this.frameMethodComboBoxes[0] = frame1methodComboBox;
			//this.frameMethodComboBoxes[1] = frame2methodComboBox;
			//this.frameMethodComboBoxes[2] = frame3methodComboBox;
			//this.frameMethodComboBoxes[3] = frame4methodComboBox;

			//各个下拉框的默认值
			qdFrameComboBox.SelectedIndex = 0;
			zuheFrameComboBox.SelectedIndex = 0;
			startupComboBox.SelectedIndex = 1;
			tongdaoCountComboBox.SelectedIndex = 0;
			eachStepTimeNumericUpDown.Value = 30;
			eachChangeModeComboBox.SelectedIndex = 0;


			frame1ComboBox.SelectedIndex = 0;
			frame2ComboBox.SelectedIndex = 0;
			frame3ComboBox.SelectedIndex = 0;
			frame4ComboBox.SelectedIndex = 0;

			for (int i = 0; i < 24; i++)
			{
				this.mSkinButtons[i].Click += new System.EventHandler(this.mSkinButton_Click);
			}

			
			#endregion

			// 初始化iniAst
			iniAst = new IniFileAst(iniFilePath);
			isInit = true;
			
		}

		private void GlobalSetForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			loadQDSet(0);
			loadGlobalSet();
			loadZuheSet(0);
			loadSKSet();
		}

		
		/// <summary>
		/// 读取《智能灯光控制器》设置
		/// </summary>
		/// <param name="frame">场景编号，由0开始</param>
		private void loadQDSet(int frame)
		{
				string QDValues = iniAst.ReadString("QD", frame.ToString(), "00000000");
				char[] values = QDValues.ToCharArray();
				for (int i = 0; i < 8; i++)
				{
					this.qdCheckBoxes[i].Checked = (values[i] == '1') ;		
				}
			
		}

		/// <summary>
		/// 读取四个配置：通道总数、开机播放场景、时间因子、场景间切换跳渐变
		/// </summary>
		private void loadGlobalSet()
		{
			try
			{
				tongdaoCountComboBox.SelectedIndex = iniAst.ReadInt("Set", "TongdaoCount", 0);
				startupComboBox.SelectedIndex = iniAst.ReadInt("Set", "StartupFrame", 1);
				int sjyz = iniAst.ReadInt("Set", "EachStepTime", 30);
				eachStepTimeNumericUpDown.Value =  sjyz<30?30:sjyz;
				eachChangeModeComboBox.SelectedIndex = iniAst.ReadInt("Set", "EachChangeMode", 0);
			}
			catch (Exception) {
				tongdaoCountComboBox.SelectedIndex = 0;
				startupComboBox.SelectedIndex = 1;
				eachStepTimeNumericUpDown.Value =  30;
				eachChangeModeComboBox.SelectedIndex = 0 ;
			}

		}

		/// <summary>
		/// 读取组合的配置，根据选进来的frame来设置
		/// </summary>
		/// <param name="frame"></param>
		private void loadZuheSet(int frame)
		{

			zuheCheckBox.Checked = ( iniAst.ReadInt("Multiple", frame + "OPEN", 0) != 0 );
			circleTimeNumericUpDown.Value = iniAst.ReadInt("Multiple", frame + "CT", 9999);
			
			//frame0methodComboBox.SelectedIndex = iniAst.ReadInt("Multiple", frame + "F0M", 0);
			frame0numericUpDown.Value = iniAst.ReadInt("Multiple", frame + "F0V", 0);
			for (int i = 0; i < 4; i++) {
				frameComboBoxes[i].SelectedIndex = iniAst.ReadInt("Multiple", frame + "F"+(i+1)+"F", 0);
				//frameMethodComboBoxes[i].SelectedIndex = iniAst.ReadInt("Multiple", frame + "F" + (i + 1) + "M", 0);
				frameNumericUpDowns[i].Value = iniAst.ReadInt("Multiple", frame + "F" + (i + 1) + "V", 0);
			}
		}

		/// <summary>
		/// 读取声控程序的所有设置
		/// </summary>
		private void loadSKSet()
		{
			for (int i = 0; i < 24; i++)
			{
				frameStepTimeNumericUpDowns[i].Value = iniAst.ReadInt("SK", i+"ST", 0);
				jgtNumericUpDowns[i].Value = iniAst.ReadInt("SK", i + "JG", 0);
			}
		}


		/// <summary>
		/// 事件：是否开启组合播放按钮被更改后，两个地方的值也变得不能修改
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zuheCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (isInit) { 
				zuheEnableGroupBox.Enabled = zuheCheckBox.Checked;
				circleTimeNumericUpDown.Enabled = zuheCheckBox.Checked;
			}
		}

		/// <summary>
		///  事件：读取文件，取出其中的数据，并将参数设置到zuheGroupBox和zuheCheckBox中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zuheFrameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isInit)
				loadZuheSet(zuheFrameComboBox.SelectedIndex);
		}

		/// <summary>
		/// 事件：当强电场景选择项发生变化时，读取配置文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void qdFrameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(isInit)
				loadQDSet(qdFrameComboBox.SelectedIndex);
		}

		/// <summary>
		///  事件：点击《(智能灯光控制器)保存当前》按钮
		///  --保存当前选择场景的八个开关的设置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void qdSaveButton_Click(object sender, EventArgs e)
		{
			char[] values = new char[8];
			for (int i = 0; i < 8; i++)
			{
				values[i] = ( qdCheckBoxes[i].Checked ? '1' :'0'  );
			}
			iniAst.WriteString("QD" ,  qdFrameComboBox.SelectedIndex.ToString() , new string(values) );
			MessageBox.Show("保存成功");
		}

		/// <summary>
		///  事件：点击《（Dmx512设置)保存设置》按钮
		///  --保存1.最大通道数;2.开机自动播放场景;3.时间因子;4.场景切换跳渐变
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void globalSaveButton_Click(object sender, EventArgs e)
		{
			iniAst.WriteInt("Set","TongdaoCount",tongdaoCountComboBox.SelectedIndex);
			iniAst.WriteInt("Set", "StartupFrame", startupComboBox.SelectedIndex) ;
			iniAst.WriteInt("Set", "EachStepTime",eachStepTimeNumericUpDown.Value);
			iniAst.WriteInt("Set", "EachChangeMode", eachChangeModeComboBox.SelectedIndex);
			MessageBox.Show("保存成功");

		}
			   
		/// <summary>
		/// 事件：点击《(多场景组合播放)保存当前》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multipleFrameSaveButton_Click(object sender, EventArgs e)
		{
			int frame = zuheFrameComboBox.SelectedIndex;
			iniAst.WriteInt("Multiple", frame + "OPEN", (zuheCheckBox.Checked ? 1 : 0));
			iniAst.WriteInt("Multiple", frame + "CT", circleTimeNumericUpDown.Value);
			iniAst.WriteInt("Multiple", frame + "F0V", frame0numericUpDown.Value);
			for(int i = 0; i < 4; i++)
			{
				iniAst.WriteInt("Multiple", frame + "F" + (i + 1) + "F",  frameComboBoxes[i].SelectedIndex);
				iniAst.WriteInt("Multiple", frame + "F" + (i + 1) + "V",  frameNumericUpDowns[i].Value);
			}
			MessageBox.Show("保存成功");
		}

		/// <summary>
		/// 事件： 右上角点击关闭按钮后的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlobalSetForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}

		/// <summary>
		///  事件：点击24个《mSkinButton》时的操作：改动选中场景的文字和frameSkinTextBox的文字
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mSkinButton_Click(object sender, EventArgs e)
		{
			if (isInit) {
				frameIndex = MathAst.getIndexNum(((SkinButton)sender).Name, -1);
				mFrameLKPanel.Enabled = true;
				currentFrameLabel.Text = "选中场景：" + ((SkinButton)sender).Text;
				mFrameTextBox.Text = iniAst.ReadString("SK", frameIndex + "LK", "");
			}
		}

		/// <summary>
		/// 事件：键盘按键点击事件:确保textBox内只能是0-4、逗号或回退键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mFrameTextBox_KeyPress(object sender, KeyPressEventArgs e) {
			if ((e.KeyChar >= '0' && e.KeyChar <= '4') || e.KeyChar == 8 ){				
				e.Handled = false;				
			}
			else
			{
				e.Handled = true;
			}
		}

		/// <summary>
		///  事件：点击《提示》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mNoticeSkinButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("请在文本框内输入每一次执行的步数（范围为1-4），并将每步数字连在一起（如1234）；若设为\"0\"或空字符串，则表示该场景不执行声控模式。");
		}

		/// <summary>
		/// 事件：点击《保存链表》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mFrameLKSaveSkinButton_Click(object sender, EventArgs e)
		{
			if (frameIndex != -1)
			{
				iniAst.WriteString("SK", frameIndex + "LK", mFrameTextBox.Text);
				MessageBox.Show("当前场景链表保存成功");
			}
			else {
				MessageBox.Show("未选中场景，无法保存");
			}
		}

		/// <summary>
		/// 事件：点击《保存声控程序所有步时间和间隔时间》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mFrameSaveAllSkinButton_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < 24; i++)
			{
				iniAst.WriteInt("SK", i + "ST", frameStepTimeNumericUpDowns[i].Value);
				iniAst.WriteInt("SK", i + "JG", jgtNumericUpDowns[i].Value);
			}
			MessageBox.Show("保存成功");
		}

		private void mFrameTextBox_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
