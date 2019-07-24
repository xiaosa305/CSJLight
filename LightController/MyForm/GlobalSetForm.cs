using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LightController.Common;

namespace LightController.MyForm
{
	public partial class GlobalSetForm : Form
	{
		public MainForm mainForm;
		private string iniFilePath;
		private IniFileAst iniAst ;
		private bool isInit = false;
		
		public GlobalSetForm(MainForm mainForm,string iniFilePath) {

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
			// 初始化声控的属性到数组中
			this.skComboBoxes[0] = skComboBox1;
			this.skComboBoxes[1] = skComboBox2;
			this.skComboBoxes[2] = skComboBox3;
			this.skComboBoxes[3] = skComboBox4;
			this.skComboBoxes[4] = skComboBox5;
			this.skComboBoxes[5] = skComboBox6;
			this.skComboBoxes[6] = skComboBox7;
			this.skComboBoxes[7] = skComboBox8;
			this.skComboBoxes[8] = skComboBox9;
			this.skComboBoxes[9] = skComboBox10;
			this.skComboBoxes[10] = skComboBox11;
			this.skComboBoxes[11] = skComboBox12;
			this.skComboBoxes[12] = skComboBox13;
			this.skComboBoxes[13] = skComboBox14;
			this.skComboBoxes[14] = skComboBox15;
			this.skComboBoxes[15] = skComboBox16;
			this.skComboBoxes[16] = skComboBox17;
			this.skComboBoxes[17] = skComboBox18;
			this.skComboBoxes[18] = skComboBox19;
			this.skComboBoxes[19] = skComboBox20;
			this.skComboBoxes[20] = skComboBox21;
			this.skComboBoxes[21] = skComboBox22;
			this.skComboBoxes[22] = skComboBox23;
			this.skComboBoxes[23] = skComboBox24;

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
			startupComboBox.SelectedIndex = 0;
			tongdaoCountComboBox.SelectedIndex = 0;
			eachStepTimeNumericUpDown.Value = 25;
			eachChangeModeComboBox.SelectedIndex = 0;


			frame1ComboBox.SelectedIndex = 0;
			frame2ComboBox.SelectedIndex = 0;
			frame3ComboBox.SelectedIndex = 0;
			frame4ComboBox.SelectedIndex = 0;

			for (int i = 0; i < 24; i++)
			{
				skComboBoxes[i].SelectedIndex = 0;
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
				startupComboBox.SelectedIndex = iniAst.ReadInt("Set", "StartupFrame", 0);
				eachStepTimeNumericUpDown.Value = iniAst.ReadInt("Set", "EachStepTime", 25);
				eachChangeModeComboBox.SelectedIndex = iniAst.ReadInt("Set", "EachChangeMode", 0);
			}
			catch (Exception) {
				tongdaoCountComboBox.SelectedIndex = 0;
				startupComboBox.SelectedIndex = 0;
				eachStepTimeNumericUpDown.Value =  25;
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
			circleTimeNumericUpDown.Value = iniAst.ReadInt("Multiple", frame + "CT", 0);
			
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
			for(int i = 0; i < 24; i++)
			{
				skComboBoxes[i].SelectedIndex = iniAst.ReadInt("SK",i.ToString(),0);
			}
		}

		
		/// <summary>
		/// 是否开启组合播放按钮被更改后，两个地方的值也变得不能修改
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
		///  读取文件，取出其中的数据，并将参数设置到zuheGroupBox和zuheCheckBox中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zuheFrameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isInit)
				loadZuheSet(zuheFrameComboBox.SelectedIndex);
		}

		/// <summary>
		/// 当选择项发生变化时，读取配置文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void qdFrameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(isInit)
				loadQDSet(qdFrameComboBox.SelectedIndex);
		}

		/// <summary>
		///  保存当前选择场景的八个开关的设置
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
		///  保存1.最大通道数2.开机自动播放场景 两个数据
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
		/// 声控保存功能
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void skSaveButton_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < 24; i++) {
				iniAst.WriteInt("SK", i.ToString() ,skComboBoxes[i].SelectedIndex);
			}
			MessageBox.Show("保存成功");
		}

		private void frameSaveButton_Click(object sender, EventArgs e)
		{
			int frame = zuheFrameComboBox.SelectedIndex;
			iniAst.WriteInt("Multiple", frame + "OPEN", (zuheCheckBox.Checked ? 1 : 0));
			iniAst.WriteInt("Multiple", frame + "CT", circleTimeNumericUpDown.Value);
			//iniAst.WriteInt("Multiple", frame + "F0M", frame0methodComboBox.SelectedIndex);
			iniAst.WriteInt("Multiple", frame + "F0V", frame0numericUpDown.Value);
			for(int i = 0; i < 4; i++)
			{
				iniAst.WriteInt("Multiple", frame + "F" + (i + 1) + "F",  frameComboBoxes[i].SelectedIndex);
				//iniAst.WriteInt("Multiple", frame + "F" + (i + 1) + "M", frameMethodComboBoxes[i].SelectedIndex);
				iniAst.WriteInt("Multiple", frame + "F" + (i + 1) + "V",  frameNumericUpDowns[i].Value);
			}
			MessageBox.Show("保存成功");
		}

		/// <summary>
		///  右上角点击关闭按钮后的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlobalSetForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}
	}
}
