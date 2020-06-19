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
		public MainFormBase mainForm;		
		private IniFileHelper iniAst ;
		private bool isInit = false;
		private int eachStepTime = 30;
		private decimal eachStepTime2 = .03m;

		private int frameCount = 0 ;
		public static int MULTI_SCENE_COUNT = 16 ;

		private Panel[] skPanels;
		private Label[] skLabels;
		private NumericUpDown[] skStepTimeNumericUpDowns;
		private Label[] stLabels;
		private NumericUpDown[] skJGTimeNumericUpDowns;
		private Label[] jgLabels;
		private TextBox[] lkTextBoxes;	

		public GlobalSetForm(MainFormBase mainForm) {

			this.mainForm = mainForm;			

			InitializeComponent();

			#region 初始化辅助数组，及其他默认选项
		
			// 初始化多场景组合播放输入项
			this.frameComboBoxes[0] = frame1ComboBox;
			this.frameComboBoxes[1] = frame2ComboBox;
			this.frameComboBoxes[2] = frame3ComboBox;
			this.frameComboBoxes[3] = frame4ComboBox;

			this.frameNumericUpDowns[0] = frame1numericUpDown;
			this.frameNumericUpDowns[1] = frame2numericUpDown;
			this.frameNumericUpDowns[2] = frame3numericUpDown;
			this.frameNumericUpDowns[3] = frame4numericUpDown;
						
			// 将所有的场景加入到《开机启动场景》及《强电选择框》中			
			foreach (string frame in MainFormBase.AllFrameList)
			{
				startupComboBox.Items.Add(frame);
			}
			// 组合播放只有前面 n 个场景可以用(全局静态变量，便于随时改动)。
			for (int i = 0; i < MULTI_SCENE_COUNT; i++)
			{
				zuheFrameComboBox.Items.Add(MainFormBase.AllFrameList[i]);
			}
			// 组合播放可调用的子场景--》目前全部可用
			for (int i = 0; i < 32; i++)
			{
				frame1ComboBox.Items.Add(MainFormBase.AllFrameList[i]);
				frame2ComboBox.Items.Add(MainFormBase.AllFrameList[i]);
				frame3ComboBox.Items.Add(MainFormBase.AllFrameList[i]);
				frame4ComboBox.Items.Add(MainFormBase.AllFrameList[i]);
			}

			//各个下拉框的默认值
			zuheFrameComboBox.SelectedIndex = 0; 
			startupComboBox.SelectedIndex = 1; // 开机启动默认设为标准，但用户也可以选择不设置开机场景
			tongdaoCountComboBox.SelectedIndex = 0;
			eachStepTimeNumericUpDown.Value = 30;
			eachChangeModeComboBox.SelectedIndex = 0;
			
			frame1ComboBox.SelectedIndex = 0;
			frame2ComboBox.SelectedIndex = 0;
			frame3ComboBox.SelectedIndex = 0;
			frame4ComboBox.SelectedIndex = 0;

			//	9.12 动态添加各种场景panel
			frameCount =  MainFormBase.AllFrameList.Count;

			skPanels = new Panel[frameCount];
			skLabels = new Label[frameCount];
			skStepTimeNumericUpDowns = new NumericUpDown[frameCount];
			stLabels = new Label[frameCount];
			skJGTimeNumericUpDowns = new NumericUpDown[frameCount];
			jgLabels = new Label[frameCount];
			lkTextBoxes = new TextBox[frameCount];

			#endregion

			// 初始化iniAst
			iniAst = new IniFileHelper(mainForm.GlobalIniPath);
			isInit = true;			
		}

		private void GlobalSetForm_Load(object sender, EventArgs e)
		{
			this.Location = new Point(mainForm.Location.X + 30, mainForm.Location.Y + 100);						

			loadGlobalSet();
			loadZuheSet(0);
			loadSKSet();
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

		#region 四个读取ini内容并载入form中的辅助方法

		/// <summary>
		/// 辅助方法：读取四个全局配置：通道总数、开机播放场景、时间因子、场景间切换跳渐变
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
			eachStepTime = Decimal.ToInt32(eachStepTimeNumericUpDown.Value);
			eachStepTime2 = eachStepTime / 1000m;
		}

		/// <summary>
		/// 辅助方法：读取组合的配置，根据选进来的frame来设置
		/// </summary>
		/// <param name="frame"></param>
		private void loadZuheSet(int frame)
		{
			zuheCheckBox.Checked = ( iniAst.ReadInt("Multiple", frame + "OPEN", 0) != 0 );
			circleTimeNumericUpDown.Value = iniAst.ReadInt("Multiple", frame + "CT", 9999);
			
			//frame0methodComboBox.SelectedIndex = iniAst.ReadInt("Multiple", frame + "F0M", 0);
			frame0NumericUpDown.Value = iniAst.ReadInt("Multiple", frame + "F0V", 0);
			for (int i = 0; i < 4; i++) {
				frameComboBoxes[i].SelectedIndex = iniAst.ReadInt("Multiple", frame + "F"+(i+1)+"F", 0);
				frameNumericUpDowns[i].Value = iniAst.ReadInt("Multiple", frame + "F" + (i + 1) + "V", 0);
			}
		}

		/// <summary>
		/// 辅助方法：读取声控程序的所有设置
		/// </summary>
		private void loadSKSet()
		{			
			for (int frameIndex = 0; frameIndex < frameCount;  frameIndex++)
			{
				addFramePanel(frameIndex, MainFormBase.AllFrameList[frameIndex]);

				int currentStepTime = iniAst.ReadInt("SK", frameIndex + "ST", 0);
				skStepTimeNumericUpDowns[frameIndex].Value = currentStepTime * eachStepTime2;
				skStepTimeNumericUpDowns[frameIndex].ValueChanged += new EventHandler(skStepTimeNumericUpDowns_ValueChanged);
				skStepTimeNumericUpDowns[frameIndex].MouseWheel += new MouseEventHandler(this.skStepTimeNumericUpDowns_MouseWheel);

				skJGTimeNumericUpDowns[frameIndex].Value = iniAst.ReadInt("SK", frameIndex + "JG", 0);

				lkTextBoxes[frameIndex].Text = iniAst.ReadString("SK", frameIndex + "LK","");
				lkTextBoxes[frameIndex].KeyPress += new KeyPressEventHandler(skFrameTextBox_KeyPress);
			}
			
		}

		/// <summary>
		/// 辅助方法：添加自动添加的panel到skFrameFlowLayoutPanel中；
		/// </summary>
		/// <param name="frameIndex"></param>
		private void addFramePanel(int frameIndex, string frameName)
		{
			skPanels[frameIndex] = new Panel
			{
				Location = new System.Drawing.Point(3, 42),				
				Size = new System.Drawing.Size(460, 33),
				BorderStyle = BorderStyle.FixedSingle
			};

			skLabels[frameIndex] = new Label
			{
				AutoSize = frameLabel.AutoSize,
				Location = frameLabel.Location,
				Size = frameLabel.Size,
				Text = frameName
			};			
			myToolTip.SetToolTip(skLabels[frameIndex], frameName);

			// 步时间
			skStepTimeNumericUpDowns[frameIndex] = new NumericUpDown
			{
				Location = stNumericUpDown.Location,
				Size = stNumericUpDown.Size,				
				Font  =stNumericUpDown.Font,
				TextAlign = stNumericUpDown.TextAlign,
				Maximum = MainFormBase.MAX_StTimes * eachStepTime2,
				Increment = eachStepTime2,
				DecimalPlaces = 2
			};

			// 步时间的Label
			stLabels[frameIndex] = new Label
			{
				AutoSize = stLabel.AutoSize,
				Location = stLabel.Location,				
				Size =stLabel.Size,				
				Text = stLabel.Text
			};		

			// 间隔时间
			skJGTimeNumericUpDowns[frameIndex] = new NumericUpDown
			{
				Location = jgNumericUpDown.Location,
				Size = jgNumericUpDown.Size,
				Font = jgNumericUpDown.Font,
				TextAlign = jgNumericUpDown.TextAlign,
				Maximum = new decimal(new int[] { 10000, 0, 0, 0 })
			};

			// 间隔时间的Label
			jgLabels[frameIndex] = new Label
			{
				AutoSize = jgLabel.AutoSize,
				Location = jgLabel.Location,			
				Size = jgLabel.Size,
				Text = jgLabel.Text
			};

			// 链表输入框
			lkTextBoxes[frameIndex] = new TextBox {
				BackColor = lkTextBox.BackColor,	
				Location = lkTextBox.Location,
				Size = lkTextBox.Size,
				MaxLength = 20				
			};

			skPanels[frameIndex].Controls.Add(skLabels[frameIndex]);
			skPanels[frameIndex].Controls.Add(skStepTimeNumericUpDowns[frameIndex]);
			skPanels[frameIndex].Controls.Add(stLabels[frameIndex]);
			skPanels[frameIndex].Controls.Add(skJGTimeNumericUpDowns[frameIndex]);
			skPanels[frameIndex].Controls.Add(jgLabels[frameIndex]);
			skPanels[frameIndex].Controls.Add(lkTextBoxes[frameIndex]);

			skFlowLayoutPanel.Controls.Add(skPanels[frameIndex]);
		}

		#endregion

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
		///  事件：点击《（Dmx512设置)保存设置》按钮
		///  --保存1.最大通道数;2.开机自动播放场景;3.时间因子;4.场景切换跳渐变
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void globalSaveButton_Click(object sender, EventArgs e)
		{
			iniAst.WriteInt("Set","TongdaoCount",tongdaoCountComboBox.SelectedIndex);		
			iniAst.WriteInt("Set", "StartupFrame", startupComboBox.SelectedIndex) ;				
			iniAst.WriteInt("Set", "EachChangeMode", eachChangeModeComboBox.SelectedIndex);

			// 弃用下列代码：时间因子不可在此处变动
			//iniAst.WriteString("Set", "EachStepTime", eachStepTimeNumericUpDown.Text);
			//eachStepTime = Decimal.ToInt32(eachStepTimeNumericUpDown.Value);
			//mainForm.ChangeEachStepTime( eachStepTime );
	
			MessageBox.Show("开机场景、场景切换跳渐变\n等全局设置保存成功");
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
			iniAst.WriteString("Multiple", frame + "CT",  circleTimeNumericUpDown.Text);
			iniAst.WriteInt("Multiple", frame + "F0V", frame0NumericUpDown.Value);
			for(int i = 0; i < 4; i++)
			{
				iniAst.WriteInt("Multiple", frame + "F" + (i + 1) + "F",  frameComboBoxes[i].SelectedIndex);
				iniAst.WriteString("Multiple", frame + "F" + (i + 1) + "V",  frameNumericUpDowns[i].Text );
			}
			MessageBox.Show( "场景【"+zuheFrameComboBox.Text + "】的组合播放设置\n保存成功");
		}

		/// <summary>
		///  事件：点击《提示》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void skNoticeButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("请在音频链表文本框内输入每一次执行的步数（范围为1-9），并将每步数字连在一起（如1234）；若设为\"0\"或空字符串，则表示该场景不执行声控模式；链表数量上限为20个。");
		}

		/// <summary>
		/// 事件：点击《保存(音频场景)设置》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void skSaveButton_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < frameCount; i++)
			{
				iniAst.WriteString("SK", i + "ST", (skStepTimeNumericUpDowns[i].Value / eachStepTime2).ToString());
				iniAst.WriteString("SK", i + "JG", skJGTimeNumericUpDowns[i].Text);
				iniAst.WriteString("SK", i + "LK", lkTextBoxes[i].Text.Trim());
			}
			MessageBox.Show("音频场景设置保存成功");
		}
			   
		#region  《声控全局配置》各种监听事件

		/// <summary>
		/// 事件：键盘按键点击事件:确保textBox内只能是0-9、回退键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void skFrameTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8)
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}

		/// <summary>
		/// 事件：更改《音频步时间》的值时，实时生成真实步时间
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void skStepTimeNumericUpDowns_ValueChanged(object sender, EventArgs e)
		{					
			NumericUpDown nud = (NumericUpDown)sender;
			int stepTime = Decimal.ToInt32(nud.Value / mainForm.eachStepTime2);
			nud.Value = stepTime * mainForm.eachStepTime2;
		}

		/// <summary>
		///  事件：鼠标滚动时，步时间值每次只变动一个Increment值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void skStepTimeNumericUpDowns_MouseWheel(object sender, MouseEventArgs e)
		{
			int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			if (e.Delta > 0)
			{
				decimal dd = skStepTimeNumericUpDowns[tdIndex].Value + skStepTimeNumericUpDowns[tdIndex].Increment;
				if (dd <= skStepTimeNumericUpDowns[tdIndex].Maximum)
				{
					skStepTimeNumericUpDowns[tdIndex].Value = dd;
				}
			}
			else if (e.Delta < 0)
			{
				decimal dd = skStepTimeNumericUpDowns[tdIndex].Value - skStepTimeNumericUpDowns[tdIndex].Increment;
				if (dd >= skStepTimeNumericUpDowns[tdIndex].Minimum)
				{
					skStepTimeNumericUpDowns[tdIndex].Value = dd;
				}
			}
		}

		#endregion
	}
}
