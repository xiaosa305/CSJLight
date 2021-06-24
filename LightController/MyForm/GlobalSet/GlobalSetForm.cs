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
		private IniHelper iniAst ;
		private bool isInit = false;
		private int eachStepTime = 40;
		private decimal eachStepTime2 = .04m;

		private int sceneCount = MainFormBase.AllSceneList.Count; 
		public static int MULTI_SCENE_COUNT = 16 ;

		private Panel[] skPanels;
		private Label[] skLabels;
		private NumericUpDown[] stNUDs;
		private Label[] stLabels;
		private NumericUpDown[] jgNUDs;
		private Label[] jgLabels;
		private TextBox[] lkTBs;	

		public GlobalSetForm(MainFormBase mainForm) {

			this.mainForm = mainForm;			

			InitializeComponent();

			#region 初始化DMX512设置

			// 将所有的场景加入到《开机启动场景》及《强电选择框》中			
			foreach (string sceneName in MainFormBase.AllSceneList)
			{
				startupComboBox.Items.Add(sceneName);
			}

			startupComboBox.SelectedIndex = 1; // 开机启动默认设为标准，但用户也可以选择不设置开机场景
			tongdaoCountComboBox.SelectedIndex = 0;
			eachStepTimeNumericUpDown.Value = 40;
			eachStepTimeNumericUpDown.MouseWheel += someNUD_MouseWheel;
			eachChangeModeComboBox.SelectedIndex = 0;

			#endregion

			#region  初始化多场景组合播放

			sceneComboBoxes[0] = scene1ComboBox;
			sceneComboBoxes[1] = scene2ComboBox;
			sceneComboBoxes[2] = scene3ComboBox;
			sceneComboBoxes[3] = scene4ComboBox;

			sceneNumericUpDowns[0] = scene1numericUpDown;
			sceneNumericUpDowns[1] = scene2numericUpDown;
			sceneNumericUpDowns[2] = scene3numericUpDown;
			sceneNumericUpDowns[3] = scene4numericUpDown;						

			// 组合播放只有前面 MULTI_SCENE_COUNT 个场景可以用(全局静态变量，便于随时改动)。
			for (int sceneIndex = 0; sceneIndex < MULTI_SCENE_COUNT; sceneIndex++)
			{
				zuheSceneComboBox.Items.Add(MainFormBase.AllSceneList[sceneIndex]);
			}

			// 组合播放可调用的子场景--》目前全部可用
			for (int sceneIndex = 0; sceneIndex < sceneCount; sceneIndex++)
			{
				scene1ComboBox.Items.Add(MainFormBase.AllSceneList[sceneIndex]);
				scene2ComboBox.Items.Add(MainFormBase.AllSceneList[sceneIndex]);
				scene3ComboBox.Items.Add(MainFormBase.AllSceneList[sceneIndex]);
				scene4ComboBox.Items.Add(MainFormBase.AllSceneList[sceneIndex]);
			}

			//各个下拉框的默认值
			zuheSceneComboBox.SelectedIndex = 0; 			
			scene1ComboBox.SelectedIndex = 0;
			scene2ComboBox.SelectedIndex = 0;
			scene3ComboBox.SelectedIndex = 0;
			scene4ComboBox.SelectedIndex = 0;

			#endregion

			#region 初始化音频场景设置

			skPanels = new Panel[sceneCount];
			skLabels = new Label[sceneCount];
			stNUDs = new NumericUpDown[sceneCount];
			stLabels = new Label[sceneCount];
			jgNUDs = new NumericUpDown[sceneCount];
			jgLabels = new Label[sceneCount];
			lkTBs = new TextBox[sceneCount];			

			for (int sceneIndex = 0; sceneIndex < sceneCount; sceneIndex++)
			{
				skPanels[sceneIndex] = new Panel
				{
					Location = skPanelDemo.Location,
					Size = skPanelDemo.Size,
					BorderStyle = skPanelDemo.BorderStyle
				};

				skLabels[sceneIndex] = new Label
				{
					AutoSize = sceneLabelDemo.AutoSize,
					Location = sceneLabelDemo.Location,
					Size = sceneLabelDemo.Size,
					Text = MainFormBase.AllSceneList[sceneIndex],
				};
				myToolTip.SetToolTip(skLabels[sceneIndex], MainFormBase.AllSceneList[sceneIndex]);

				// 步时间NUD
				stNUDs[sceneIndex] = new NumericUpDown
				{
					Location = stNUDDemo.Location,
					Size = stNUDDemo.Size,
					Font = stNUDDemo.Font,
					TextAlign = stNUDDemo.TextAlign,
					DecimalPlaces = stNUDDemo.DecimalPlaces
				};
				stNUDs[sceneIndex].ValueChanged += stNUD_ValueChanged;
				stNUDs[sceneIndex].MouseWheel += someNUD_MouseWheel;

				// 步时间Label
				stLabels[sceneIndex] = new Label
				{
					AutoSize = stLabelDemo.AutoSize,
					Location = stLabelDemo.Location,
					Size = stLabelDemo.Size,
					Text = stLabelDemo.Text
				};

				// 间隔时间NUD
				jgNUDs[sceneIndex] = new NumericUpDown
				{
					Location = jgNUDDemo.Location,
					Size = jgNUDDemo.Size,
					Font = jgNUDDemo.Font,
					TextAlign = jgNUDDemo.TextAlign,
					Maximum = jgNUDDemo.Maximum,
				};

				// 间隔时间Label
				jgLabels[sceneIndex] = new Label
				{
					AutoSize = jgLabelDemo.AutoSize,
					Location = jgLabelDemo.Location,
					Size = jgLabelDemo.Size,
					Text = jgLabelDemo.Text
				};

				// 链表输入框
				lkTBs[sceneIndex] = new TextBox
				{
					BackColor = lkTBDemo.BackColor,
					Location = lkTBDemo.Location,
					Size = lkTBDemo.Size,
					MaxLength = lkTBDemo.MaxLength,
				};
				lkTBs[sceneIndex].KeyPress += lkTB_KeyPress;

				skPanels[sceneIndex].Controls.Add(skLabels[sceneIndex]);
				skPanels[sceneIndex].Controls.Add(stNUDs[sceneIndex]);
				skPanels[sceneIndex].Controls.Add(stLabels[sceneIndex]);
				skPanels[sceneIndex].Controls.Add(jgNUDs[sceneIndex]);
				skPanels[sceneIndex].Controls.Add(jgLabels[sceneIndex]);
				skPanels[sceneIndex].Controls.Add(lkTBs[sceneIndex]);

				skFLP.Controls.Add(skPanels[sceneIndex]);							
		}
			
			#endregion

			// 初始化iniAst
			iniAst = new IniHelper(mainForm.GlobalIniPath);
			isInit = true;

		}		

		private void GlobalSetForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 30, mainForm.Location.Y + 100);
			LanguageHelper.InitForm(this);

			// 下列三行先后顺序也很重要，loadGlobalSet必须放在loadSKSet前面，因为有个读取eachStepTime的操作，skSet依赖于此数据；
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
			Dispose();
			mainForm.Activate();
		}

		#region 几个读取ini内容并载入form中的辅助方法

		/// <summary>
		/// 辅助方法：读取四个全局设置：通道总数、开机播放场景、时间因子、场景间切换跳渐变
		/// </summary>
		private void loadGlobalSet()
		{
			try
			{
				tongdaoCountComboBox.SelectedIndex = iniAst.ReadInt("Set", "TongdaoCount", 0);
				startupComboBox.SelectedIndex = iniAst.ReadInt("Set", "StartupFrame", 1);
				int sjyz = iniAst.ReadInt("Set", "EachStepTime", 40);
				eachStepTimeNumericUpDown.Value = sjyz < 30 ? 30 : sjyz;
				eachChangeModeComboBox.SelectedIndex = iniAst.ReadInt("Set", "EachChangeMode", 0);
			}
			catch (Exception)
			{
				tongdaoCountComboBox.SelectedIndex = 0;
				startupComboBox.SelectedIndex = 1;
				eachStepTimeNumericUpDown.Value = 40;
				eachChangeModeComboBox.SelectedIndex = 0;
			}
			eachStepTime = decimal.ToInt32(eachStepTimeNumericUpDown.Value);
			eachStepTime2 = eachStepTime / 1000m;
		}

		/// <summary>
		/// 辅助方法：根据填入的sceneIndex值，读取多场景的配置；供Load时和更改组合场景时调用
		/// </summary>
		/// <param name="sceneIndex"></param>
		private void loadZuheSet(int sceneIndex)
		{
			zuheCheckBox.Checked = ( iniAst.ReadInt("Multiple", sceneIndex + "OPEN", 0) != 0 );
			circleTimeNumericUpDown.Value = iniAst.ReadInt("Multiple", sceneIndex + "CT", 9);			
			scene0NumericUpDown.Value = iniAst.ReadInt("Multiple", sceneIndex + "F0V", 0);
			for (int zuheIndex = 0; zuheIndex < 4; zuheIndex++) {
				sceneComboBoxes[zuheIndex].SelectedIndex = iniAst.ReadInt("Multiple", sceneIndex + "F"+(zuheIndex+1)+"F", 0);
				sceneNumericUpDowns[zuheIndex].Value = iniAst.ReadInt("Multiple", sceneIndex + "F" + (zuheIndex + 1) + "V", 0);
			}
		}

		/// <summary>
		/// 辅助方法：读取声控设置
		/// </summary>
		private void loadSKSet()
		{
			for (int sceneIndex = 0; sceneIndex < sceneCount; sceneIndex++)
			{
				int currentStepTime = iniAst.ReadInt("SK", sceneIndex + "ST", 0);

				stNUDs[sceneIndex].Maximum = MainFormBase.MAX_StTimes * eachStepTime2;
				stNUDs[sceneIndex].Increment = eachStepTime2;
				stNUDs[sceneIndex].Value = currentStepTime * eachStepTime2;				

				jgNUDs[sceneIndex].Value = iniAst.ReadInt("SK", sceneIndex + "JG", 0);
				lkTBs[sceneIndex].Text = iniAst.ReadString("SK", sceneIndex + "LK", "");							
			}			
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
				loadZuheSet(zuheSceneComboBox.SelectedIndex);
		}
				
		/// <summary>
		///  事件：点击《（Dmx512设置)保存设置》按钮
		///  --保存1.最大通道数;2.开机自动播放场景;3.时间因子;4.场景切换跳渐变
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void globalSaveButton_Click(object sender, EventArgs e)
		{
			// 这几栏的写法很简单，只要把当前值设好即可，数据量小，无需判断是否更改过；
			iniAst.WriteInt("Set","TongdaoCount",tongdaoCountComboBox.SelectedIndex);		
			iniAst.WriteInt("Set", "StartupFrame", startupComboBox.SelectedIndex) ;				
			iniAst.WriteInt("Set", "EachChangeMode", eachChangeModeComboBox.SelectedIndex);
			
			//如果更改了时间因子，则每个音频的步时间也需要更改显示（但实际上，底层数据无需更改）
			if (decimal.ToInt32(eachStepTimeNumericUpDown.Value) != eachStepTime) {
								
				// 保存新的《时间因子》，用WriteInt方法
				eachStepTime = decimal.ToInt32(eachStepTimeNumericUpDown.Value);
				iniAst.WriteInt("Set", "EachStepTime", eachStepTime);

				// 某些与时间因子相关的控件，它们的显示值也要一起更改；
				decimal oldEachStepTime2 = eachStepTime2 ;  //decimal是传值而非传地址，可以直接用"="赋值，此处留一个eachStepTime2的备份，便于之后的运算；
				eachStepTime2 = eachStepTime / 1000m;
				for (int sceneIndex = 0; sceneIndex < sceneCount; sceneIndex++)
				{
					decimal currentStepTime = stNUDs[sceneIndex].Value / oldEachStepTime2;					

					stNUDs[sceneIndex].ValueChanged -= stNUD_ValueChanged;

					stNUDs[sceneIndex].Maximum = MainFormBase.MAX_StTimes * eachStepTime2;
					stNUDs[sceneIndex].Increment = eachStepTime2;
					stNUDs[sceneIndex].Value = currentStepTime * eachStepTime2; // 先后顺序可能有影响（比如说时间因子调大，Maximum还没调时，Value可能设不到最新的Maximum）

					stNUDs[sceneIndex].ValueChanged += stNUD_ValueChanged;
				}
				mainForm.ChangeEachStepTime(eachStepTime); // 主界面的时间因子，也要更改一下
			}


			MessageBox.Show(LanguageHelper.TranslateSentence("开机场景、场景切换跳渐变\n等全局设置保存成功")) ;
		}
			
		/// <summary>
		/// 事件：点击《(多场景组合播放)保存当前》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void multipleFrameSaveButton_Click(object sender, EventArgs e)
		{
			int frame = zuheSceneComboBox.SelectedIndex;
			iniAst.WriteInt("Multiple", frame + "OPEN", (zuheCheckBox.Checked ? 1 : 0));
			iniAst.WriteString("Multiple", frame + "CT",  circleTimeNumericUpDown.Text);
			iniAst.WriteInt("Multiple", frame + "F0V", scene0NumericUpDown.Value);
			for(int i = 0; i < 4; i++)
			{
				iniAst.WriteInt("Multiple", frame + "F" + (i + 1) + "F",  sceneComboBoxes[i].SelectedIndex);
				iniAst.WriteString("Multiple", frame + "F" + (i + 1) + "V",  sceneNumericUpDowns[i].Text );
			}
			MessageBox.Show(LanguageHelper.TranslateSentence("场景") 
				+  "【" +zuheSceneComboBox.Text + "】"
				+ LanguageHelper.TranslateSentence("的组合播放设置保存成功") );
		}

		/// <summary>
		///  事件：点击《提示》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void skNoticeButton_Click(object sender, EventArgs e)
		{
			NumericUpDown obj = stNUDs[0];
			Console.WriteLine(obj.Increment + "   -   "+obj.Maximum );
			MessageBox.Show(LanguageHelper.TranslateSentence("请在音频链表文本框内输入每一次执行的步数（范围为1-9），并将每步数字连在一起（如1234）；若设为\"0\"或空字符串，则表示该场景不执行声控模式；链表数量上限为20个。")	);
		}

		/// <summary>
		/// 事件：点击《保存(音频场景)设置》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void skSaveButton_Click(object sender, EventArgs e)
		{
			for (int frameIndex = 0; frameIndex < sceneCount; frameIndex++)
			{
				iniAst.WriteInt("SK", frameIndex + "ST", stNUDs[frameIndex].Value / eachStepTime2);
				iniAst.WriteInt("SK", frameIndex + "JG", jgNUDs[frameIndex].Value);
				iniAst.WriteString("SK", frameIndex + "LK", lkTBs[frameIndex].Text.Trim());
			}
			MessageBox.Show(LanguageHelper.TranslateSentence("音频场景设置保存成功"));
		}

		#region  各种输入框监听事件

		/// <summary>
		/// 验证：对某些NumericUpDown进行鼠标滚轮的验证，避免一次性滚动过多
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someNUD_MouseWheel(object sender, MouseEventArgs e)
		{
			NumericUpDown nud = sender as NumericUpDown;
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = nud.Value + nud.Increment;
				if (dd <= nud.Maximum)
				{
					nud.Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = nud.Value - nud.Increment;
				if (dd >= nud.Minimum)
				{
					nud.Value = dd;
				}
			}
		}

		/// <summary>
		/// 事件：单独处理时间因子输入框，以免用户手动输入时出错（这个监听方法不可省略，否则用户可能输入非法数值）
		/// </summary>
		/// <param name="sender"></param>		
		private void eachStepTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown nud = (NumericUpDown)sender;

			if (nud.Value < 35)
			{
				nud.Value = 30;
			}
			else if (nud.Value < 45)
			{
				nud.Value = 40;
			}
			else
			{
				nud.Value = 50;
			}
		}

		/// <summary>
		/// 事件：手动更改《音频步时间》的值时，主动把stepTime设为整型， 再换算回来，避免用户输入错误的数字（这个监听方法不可省略！）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void stNUD_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown nud = (NumericUpDown)sender;
			int stepTime = decimal.ToInt32(nud.Value / eachStepTime2);
			nud.Value = stepTime * eachStepTime2;
		}

		/// <summary>
		/// 事件：键盘按键点击事件:确保textBox内只能是0-9、回退键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lkTB_KeyPress(object sender, KeyPressEventArgs e)
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
		
		#endregion

		/// <summary>
		/// 事件：点击《摇麦设置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ymButton_Click(object sender, EventArgs e)
		{
			new YMSetForm(mainForm.GlobalIniPath).ShowDialog();
		}

	}
}
