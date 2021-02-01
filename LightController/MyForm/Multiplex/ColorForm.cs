﻿using LightController.Ast;
using LightController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LightController.Common;

namespace LightController.MyForm.Multiplex
{
	public partial class ColorForm : Form
	{
		private MainFormBase mainForm;
		private int selectedPanelIndex = -1 ;
		private decimal eachStepTime = .04m;
		private int mode = 0;
		private int soundStepTime = 10; 
		private decimal commonStepTime = 50;

		private int tongdaoCount = 4;
		private int stepCount = 0;
		private TongdaoWrapper[,] tongdaoArray ;
		private IList<string> tdNameList ;
		private string dimmerStr = LanguageHelper.TranslateWord("总调光");
		private string rStr = LanguageHelper.TranslateWord("红");
		private string gStr = LanguageHelper.TranslateWord("绿");
		private string bStr = LanguageHelper.TranslateWord("蓝");

		private MaterialAst material; 
		
		public ColorForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;		

			InitializeComponent();
			tdNameList = new List<string> { dimmerStr , rStr, gStr,bStr } ; // 为tdNameList赋值；此列表是固定的

			tgNUD.MouseWheel += someNUD_MouseWheel;
			tgTrackBar.MouseWheel += someTrackBar_MouseWheel;

		}

		private void ColorForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			LanguageHelper.InitForm(this);
		}

		/// <summary>
		/// 事件：每次激活后，需要重新刷新步时间（避免主界面更改了时间因子造成的显示问题）；也必须重新隐藏或显示预览的按键(用selectColorPanel)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ColorForm_Activated(object sender, EventArgs e)
		{
			previewButton.Visible = mainForm.IsConnected && mainForm.CurrentMode == 0; // 音频模式就不让预览了，没有意义

			// 当时间因子发生变化时，需要重新渲染步时间；
			if ( eachStepTime != mainForm.EachStepTime2 ) {								
				for (int panelIndex = 1; panelIndex < colorFLP.Controls.Count; panelIndex++)
				{
					NumericUpDown stNUD = (colorFLP.Controls[panelIndex] as Panel).Controls[0] as NumericUpDown;
					decimal oldValue = stNUD.Value / eachStepTime ;
										
					stNUD.Maximum = MainFormBase.MAX_StTimes * mainForm.EachStepTime2 ;
					stNUD.Minimum = mainForm.EachStepTime2; // 步时间如果为0，则毫无意义
					stNUD.Increment = mainForm.EachStepTime2;
					stNUD.Value = mainForm.EachStepTime2 * oldValue ; 
				}
				eachStepTime = mainForm.EachStepTime2;
			}

			// 当模式发生变化时，需要隐藏|显示 相关控件
			if ( mode != mainForm.CurrentMode) {
				for (int panelIndex = 1; panelIndex < colorFLP.Controls.Count; panelIndex++)
				{
					(colorFLP.Controls[panelIndex] as Panel).Controls[0].Visible = mainForm.CurrentMode == 0;
					(colorFLP.Controls[panelIndex] as Panel).Controls[1].Visible  = mainForm.CurrentMode == 0;			
				}
				stLabel.Visible = mainForm.CurrentMode == 0;
				modeLabel.Visible = mainForm.CurrentMode == 0;
				mode = mainForm.CurrentMode;
			}

			// 虽然不显示，但应用颜色时，仍需用到这些数据
			if (mainForm.CurrentMode == 1) {
				IniFileHelper iniHelper = new IniFileHelper(mainForm.GlobalIniPath);
				soundStepTime =  iniHelper.ReadInt("SK", mainForm.CurrentScene + "ST", 11);
			}

			selectColorPanel(); //ColorForm_Activated
		}

		/// <summary>
		/// 事件：关闭窗口时：
		/// 若正在预览中，则停止预览(点击按键一次);
		/// 若非预览中，则恢复到当前步，用OneStepPlay（不用RefreshStep【这个方法过于完整，不考虑】 ）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ColorForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (mainForm.IsConnected) {
				if (mainForm.IsPreviewing)
				{
					endView();
				}
				else {
					mainForm.OneStepPlay(null);
				}				
			}			
		}

		/// <summary>
		/// 事件：点击《添加(色块)》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addButton_Click(object sender, EventArgs e)
		{			
			if (DialogResult.Cancel == myColorDialog.ShowDialog() ) {
				return;
			}

			Panel colorPanel = new Panel()
			{
				Size = colorPanelDemo.Size,
				Margin = colorPanelDemo.Margin,
				Visible = true,
				BackColor = myColorDialog.Color,
			};
			colorPanel.Click += colorPanel_Click;

			NumericUpDown stNUD = new NumericUpDown()
			{
				Font = stNUDDemo.Font,
				Location = stNUDDemo.Location,
				Size = stNUDDemo.Size,
				TextAlign = stNUDDemo.TextAlign,
				DecimalPlaces = stNUDDemo.DecimalPlaces,
				Maximum = stNUDDemo.Maximum * eachStepTime,
				Minimum = eachStepTime ,
				Increment = eachStepTime,
				Value = mainForm.EachStepTime2 * commonStepTime,
				Visible = mode == 0,
			};
			stNUD.MouseWheel += someNUD_MouseWheel;
			stNUD.KeyPress += stNUD_KeyPress ;  

			CheckBox cmCB = new CheckBox()
			{
				Font = cmCBDemo.Font,
				Location = cmCBDemo.Location,
				Size = cmCBDemo.Size,
				TextAlign = cmCBDemo.TextAlign,
				Text = cmCBDemo.Text,
				ForeColor = cmCBDemo.ForeColor,
				BackColor = cmCBDemo.BackColor,
				Visible = mode == 0,
			};
			cmCB.KeyPress += cmCheckBox_KeyPress;

			colorPanel.Controls.Add(stNUD);
			colorPanel.Controls.Add(cmCB);

			colorFLP.Controls.Add(colorPanel);
			selectedPanelIndex = colorFLP.Controls.IndexOf(colorPanel);

			selectColorPanel(); //addButton_Click

		}

		/// <summary>
		/// 事件：点击《修改(色块)》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void editButton_Click(object sender, EventArgs e)
		{
			Color selectedColor = colorFLP.Controls[selectedPanelIndex].BackColor;
			myColorDialog.Color = selectedColor;  // 把选中色块的颜色，放给myColorDialog

			if (DialogResult.Cancel == myColorDialog.ShowDialog())
			{
				return;
			}
			colorFLP.Controls[selectedPanelIndex].BackColor = myColorDialog.Color;
			
			selectColorPanel(); //editButton_Click
		}

		/// <summary>
		/// 事件：点击《删除(色块)》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteButton_Click(object sender, EventArgs e)
		{
			colorFLP.Controls.RemoveAt(selectedPanelIndex);
			selectedPanelIndex = -1;
			selectColorPanel(); //deleteButton_Click
		}

		/// <summary>
		/// 事件：点击《清空(色块)》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void clearButton_Click(object sender, EventArgs e)
		{
			for (int panelIndex = colorFLP.Controls.Count - 1; panelIndex > 0; panelIndex--)
			{
				colorFLP.Controls.RemoveAt(panelIndex);
			}
			selectedPanelIndex = -1;
			selectColorPanel(); //clearButton_Click
		}

		/// <summary>
		/// 事件：点击《色块(Panel)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorPanel_Click(object sender, EventArgs e)
		{	
			selectedPanelIndex = colorFLP.Controls.IndexOf(sender as Panel) ;
			selectColorPanel(); //colorPanel_Click
		}
		
		/// <summary>
		/// 事件：点击《预览》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
 		private void previewButton_Click(object sender, EventArgs e){

			if ( !mainForm.IsConnected)
			{
				setNotice("尚未连接设备", true,true);
				return;
			}

			// 如果正在预览中，则停止预览（不需生成material）
			if (mainForm.IsPreviewing)
			{
				endView();
				// 停止预览后，恢复 单色显示(并在里面集成previewButton是否可用的代码)
				selectColorPanel();  //previewButton_Click-->点击停止预览后
				setNotice("已停止预览，并恢复单色显示。", false, true);
			}
			else if( generateComplexMaterial() ) {
				mainForm.PreviewButtonClick(material);
				previewButton.Text = "停止预览";
				setNotice("正在预览颜色变化", false, true);
			}				
		}

		/// <summary>
		/// 事件：点击《应用颜色变化》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)	{ }

		/// <summary>
		/// 事件：左右键点击《应用颜色变化》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_MouseDown(object sender, MouseEventArgs e)
		{
				InsertMethod insMethod = InsertMethod.INSERT;   //  左键插入
				if (e.Button == MouseButtons.Middle)	{		insMethod = InsertMethod.COVER;		}  //中键追加
				else if(  e.Button == MouseButtons.Right)	{ insMethod = InsertMethod.APPEND ;		}  // 右键覆盖

				if (generateComplexMaterial() )
				{					
					mainForm.InsertOrCoverMaterial( material,   insMethod,  shieldCheckBox.Checked);
					if (mainForm.IsPreviewing)
					{
						mainForm.PreviewButtonClick(null);
						previewButton.Text = "预览";
					}
					Hide();
					mainForm.Activate();
				}
		}

		/// <summary>
		/// 事件：点击取消按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Hide();
			mainForm.Activate();
		}

		/// <summary>
		/// 辅助方法：更改selectedPanelIndex后，刷新相应的一些控件；
		/// </summary>
		private void selectColorPanel()
		{
			astPanel.BackColor = selectedPanelIndex > 0 ? (colorFLP.Controls[selectedPanelIndex] as Panel).BackColor : Color.MintCream;
			astLabel.Text = selectedPanelIndex > 0 ?  selectedPanelIndex+"" : "未选中步";	
			editButton.Enabled = selectedPanelIndex > 0;
			deleteButton.Enabled = selectedPanelIndex > 0;

			stepCount = colorFLP.Controls.Count - 1;
			clearButton.Enabled = stepCount > 0;
			previewButton.Enabled = mainForm.IsConnected && (mainForm.IsPreviewing || stepCount>0 ); // 必须是连接模式；如果正在预览中则一直可用；否则需要判断是否有色块
			previewButton.Text = mainForm.IsPreviewing ? "停止预览" : "预览";

			oneStepPlay(); //selectColorPanel
		}

		/// <summary>
		/// 辅助方法：根据当前色块，直接在灯具上显示颜色(已连接且非预览中)
		/// </summary>
		private void oneStepPlay()
		{
			if (mainForm.IsConnected && !mainForm.IsPreviewing)
			{
				generateSingleMaterial(); // 若未选中色块，则material = null,此时mainForm会直接跑原来的步数据
				mainForm.OneStepPlay(material);
			}
		}

		/// <summary>
		/// 辅助方法：生成所有颜色组合的【素材】
		/// </summary>
		/// <returns></returns>
		private bool generateComplexMaterial()
		{
			if (stepCount == 0) {
				setNotice("尚未添加颜色块。", true, true);
				return false;
			}
			else
			{
				try
				{
					tongdaoArray = new TongdaoWrapper[stepCount, tongdaoCount];
					for (int panelIndex = 1; panelIndex <= stepCount; panelIndex++)
					{
						Panel colorPanel = colorFLP.Controls[panelIndex] as Panel;

						// 常规模式，取控件内的值；音频模式，取固定值；
						int stepTime = mode == 0 ? decimal.ToInt32((colorPanel.Controls[0] as NumericUpDown).Value / eachStepTime) : soundStepTime;
						int changeMode = mode == 0 ? ( (colorPanel.Controls[1] as CheckBox).Checked ? 1 : 0 ) : 1;								

						tongdaoArray[panelIndex - 1, 0] = new TongdaoWrapper(dimmerStr, tgTrackBar.Value, stepTime, changeMode);
						tongdaoArray[panelIndex - 1, 1] = new TongdaoWrapper(rStr, colorPanel.BackColor.R, stepTime, changeMode);
						tongdaoArray[panelIndex - 1, 2] = new TongdaoWrapper(gStr, colorPanel.BackColor.G, stepTime, changeMode);
						tongdaoArray[panelIndex - 1, 3] = new TongdaoWrapper(bStr, colorPanel.BackColor.B, stepTime, changeMode);

						material = new MaterialAst
						{
							StepCount = stepCount,
							TdNameList = tdNameList,
							TongdaoArray = tongdaoArray,
						};
					}
					return true;
				}
				catch (Exception ex) {
					setNotice("生成数据出错：" + ex.Message, true, false);
					return false;
				}				
			}
		}

		/// <summary>
		/// 辅助方法：生成单个颜色的【素材】
		/// </summary>
		/// <returns></returns>
		private void generateSingleMaterial() {

			if (selectedPanelIndex <= 0)
			{
				setNotice("尚未选择色块。", false, true);
				material = null;
				return;
			}

			tongdaoArray = new TongdaoWrapper[1, tongdaoCount];

			Panel colorPanel = colorFLP.Controls[selectedPanelIndex] as Panel;
			Color bColor = colorPanel.BackColor;
			int stepTime = decimal.ToInt32((colorPanel.Controls[0] as NumericUpDown).Value / eachStepTime);
			
			tongdaoArray[0, 0] = new TongdaoWrapper(dimmerStr, tgTrackBar.Value, 50, 0);
			tongdaoArray[0, 1] = new TongdaoWrapper(rStr, bColor.R,  stepTime, 0);
			tongdaoArray[0, 2] = new TongdaoWrapper(gStr, bColor.G, stepTime, 0);
			tongdaoArray[0, 3] = new TongdaoWrapper(bStr, bColor.B, stepTime, 0);

			material = new MaterialAst
			{
				StepCount = 1,  
				TdNameList = tdNameList,
				TongdaoArray = tongdaoArray,
			};
		}

		/// <summary>
		/// 辅助方法：结束预览
		/// </summary>
		private void endView() {
		
			if (mainForm .IsConnected && mainForm.IsPreviewing ) {
				mainForm.PreviewButtonClick(null);
				previewButton.Text = "预览";				
				setNotice("已停止预览", false, true);
			}			
		}

		#region 调节总调光 及 统一设值相关

		/// <summary>
		/// 事件：《步时间NUD》的键盘点击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void stNUD_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (stepCount > 1 &&  (e.KeyChar == 'a' || e.KeyChar == 'A') ) {

				decimal unifySt = (sender as NumericUpDown).Value;

				// 设置了提示，且用户点击了取消，则return。否则继续往下走
				if (mainForm.IsNoticeUnifyTd)
				{
					if (DialogResult.Cancel == MessageBox.Show(
							LanguageHelper.TranslateSentence("确定要将所有步时间都设为")+"【"+ unifySt + " S】?",
							LanguageHelper.TranslateSentence("统一步时间"),
							MessageBoxButtons.OKCancel,
							MessageBoxIcon.Question))
					{
						return;
					}
				}

				for (int controlIndex = 1; controlIndex < colorFLP.Controls.Count; controlIndex++) {
					( (colorFLP.Controls[controlIndex] as Panel).Controls[0] as NumericUpDown ).Value =unifySt;
				}
			}
		}

		/// <summary>
		/// 事件：《跳渐变复选框》的键盘点击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmCheckBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (stepCount > 1 && (e.KeyChar == 'a' || e.KeyChar == 'A'))
			{
				bool unifyCM = (sender as CheckBox).Checked;
				string cmStr= unifyCM ? "渐变" : "跳变";

				// 设置了提示，且用户点击了取消，则return。否则继续往下走
				if (mainForm.IsNoticeUnifyTd)
				{
					if (DialogResult.Cancel == MessageBox.Show(
							LanguageHelper.TranslateSentence("确定要将所有跳渐变都设为【" + cmStr + "】吗?"),
							LanguageHelper.TranslateSentence("统一跳渐变"),
							MessageBoxButtons.OKCancel,
							MessageBoxIcon.Question))
					{
						return;
					}
				}

				for (int controlIndex = 1; controlIndex < colorFLP.Controls.Count; controlIndex++)
				{
					((colorFLP.Controls[controlIndex] as Panel).Controls[1] as CheckBox).Checked = unifyCM;
				}
			}
		}

		/// <summary>
		/// 事件：(总)调光滑动杆值发生变化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tgTrackBar_ValueChanged(object sender, EventArgs e)
		{
			TrackBar tBar = sender as TrackBar;

			tgNUD.ValueChanged -= tgNUD_ValueChanged;
			tgNUD.Value = tBar.Value;
			tgNUD.ValueChanged += tgNUD_ValueChanged;

			oneStepPlay(); //tgTrackBar_ValueChanged

		}

		/// <summary>
		/// 事件：(总)调光nud的值发生变化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tgNUD_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown nud = sender as NumericUpDown;
			tgTrackBar.ValueChanged -= tgTrackBar_ValueChanged;
			tgTrackBar.Value = decimal.ToInt32(nud.Value);
			tgTrackBar.ValueChanged += tgTrackBar_ValueChanged;

			oneStepPlay(); //tgNUD_ValueChanged
		}

		#endregion

		#region 通用方法

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
		///  验证：对某些TrackBar进行鼠标滚轮的验证，避免一次性滚动过多（与OS设置有关）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someTrackBar_MouseWheel(object sender, MouseEventArgs e) {

			TrackBar tb = sender as TrackBar;
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true; //设为true则屏蔽之后系统自行处理的操作（就是原来加3(Win10)之类的操作）
			}
			// 向上滚
			if (e.Delta > 0)
			{
				int dd = tb.Value + tb.SmallChange;
				if (dd <= tb.Maximum)
				{
					tb.Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				int dd = tb.Value - tb.SmallChange;
				if (dd >= tb.Minimum)
				{
					tb.Value = dd;
				}
			}
		}

		/// <summary>
		/// 辅助方法：显示提示
		/// </summary>
		/// <param name="msgBoxShow"></param>
		private void setNotice(string msg, bool msgBoxShow,bool isTranslate)
		{
			if (isTranslate) {
				msg = LanguageHelper.TranslateSentence(msg);
			}

			myStatusLabel.Text = msg;
			if (msgBoxShow)
			{
				MessageBox.Show(msg);
			}
		}

		/// <summary>
		/// 辅助方法：预览按键改变文字后，进行翻译
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someControl_TextChanged(object sender, EventArgs e)
		{
			LanguageHelper.TranslateControl(sender as Control);
		}

		#endregion


	}
}
