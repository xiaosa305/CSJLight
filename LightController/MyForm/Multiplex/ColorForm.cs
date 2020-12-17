using LightController.Ast;
using LightController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.Multiplex
{
	public partial class ColorForm : Form
	{
		private MainFormBase mainForm;
		private int selectedPanelIndex = -1 ;		
		private MaterialAst material ;
		private decimal eachStepTime = .04m;
		private decimal commonStepTime = 50;

		private int tongdaoCount = 4;
		private int stepCount = 0;
		private TongdaoWrapper[,] tongdaoList ;
		private IList<string> tdNameList = new List<string> { "总调光", "红", "绿", "蓝" }; // 为tdNameList赋值；此列表是固定的
		
		public ColorForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;		

			InitializeComponent();

			tgNUD.MouseWheel += someNUD_MouseWheel;

		}

		private void ColorForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
		}

		/// <summary>
		/// 事件：每次激活后，需要重新刷新步时间（避免主界面更改了时间因子造成的显示问题）；也必须重新隐藏或显示预览的按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ColorForm_Activated(object sender, EventArgs e)
		{
			previewButton.Visible = mainForm.IsConnected;
			if ( eachStepTime != mainForm.EachStepTime2 ) {								
				for (int panelIndex = 1; panelIndex < stepCount; panelIndex++)
				{
					NumericUpDown stNUD = (colorFLP.Controls[panelIndex] as Panel).Controls[0] as NumericUpDown;
					decimal oldValue = stNUD.Value / eachStepTime ;
										
					stNUD.Maximum = MainFormBase.MAX_StTimes * mainForm.EachStepTime2 ;
					stNUD.Increment = mainForm.EachStepTime2;
					stNUD.Value = mainForm.EachStepTime2 * oldValue ; 
				}
				eachStepTime = mainForm.EachStepTime2;
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

			Console.WriteLine(myColorDialog.Color.R + myColorDialog.Color.G + myColorDialog.Color.B);
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
				Maximum = MainFormBase.MAX_STEP * eachStepTime,
				Increment = eachStepTime,
				Value = mainForm.EachStepTime2 * commonStepTime
			};
			stNUD.MouseWheel += someNUD_MouseWheel;

			CheckBox cmCB = new CheckBox()
			{
				Font = cmCBDemo.Font,
				Location = cmCBDemo.Location,
				Size = cmCBDemo.Size,
				TextAlign = cmCBDemo.TextAlign,
				Text = cmCBDemo.Text,
				ForeColor = cmCBDemo.ForeColor,
				BackColor = cmCBDemo.BackColor,				
			};

			colorPanel.Controls.Add(stNUD);
			colorPanel.Controls.Add(cmCB);

			colorFLP.Controls.Add(colorPanel);
			stepCount = colorFLP.Controls.Count - 1;
		}

		/// <summary>
		/// 事件：点击《色块(Panel)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorPanel_Click(object sender, EventArgs e)
		{
			Panel colorPanel = sender as Panel;			

			selectedPanelIndex = colorFLP.Controls.IndexOf(colorPanel) ;
			astPanel.BackColor = colorPanel.BackColor;
			astLabel.Text = "第"+selectedPanelIndex+"步";

			refreshButtons();
		}

		/// <summary>
		/// 辅助方法：根据当前选中的colorPanel，来确认几个按键的可用性；
		/// </summary>
		private void refreshButtons()
		{
			editButton.Enabled = selectedPanelIndex > 0;
			deleteButton.Enabled = selectedPanelIndex > 0;
		}

		/// <summary>
		/// 事件：点击《修改(色块)》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void editButton_Click(object sender, EventArgs e)
		{
			Color selectedColor = colorFLP.Controls[selectedPanelIndex].BackColor;
			myColorDialog.Color = selectedColor;

			if (DialogResult.Cancel == myColorDialog.ShowDialog())
			{
				return;
			}

			colorFLP.Controls[selectedPanelIndex].BackColor = myColorDialog.Color;
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
			stepCount = colorFLP.Controls.Count - 1;

			astPanel.BackColor = Color.MintCream;
			astLabel.ForeColor = Color.Black;
			astLabel.Text = "未选中步";

			refreshButtons();
		}

		/// <summary>
		/// 事件：点击《预览》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewButton_Click(object sender, EventArgs e){	}

		/// <summary>
		/// 事件：鼠标左右键点击《预览》：左键全部预览，右键单步预览
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewButton_MouseDown(object sender, MouseEventArgs e)
		{
			previewButton.Text = mainForm.IsPreviewing ? "预览" : "停止预览";
			if (e.Button == MouseButtons.Left) {

				

				if( ! generateColorMaterial()){					
					return;
				}

			}else if (e.Button == MouseButtons.Right) {

				if (selectedPanelIndex <= 0) {
					setNotice("请先选择色块，再进行单色预览。", true);
					return;
				}

				tongdaoList = new TongdaoWrapper[1, tongdaoCount];

				Panel colorPanel = colorFLP.Controls[selectedPanelIndex] as Panel;
				Color bColor = colorPanel.BackColor;
				int stepTime = decimal.ToInt32((colorPanel.Controls[0] as NumericUpDown).Value / eachStepTime);

				tongdaoList[0, 0] = new TongdaoWrapper("总调光", tgTrackBar.Value, 50,0);
				tongdaoList[0, 1] = new TongdaoWrapper("红", bColor.R, stepTime,0);
				tongdaoList[0, 2] = new TongdaoWrapper("绿", bColor.G, stepTime,0);
				tongdaoList[0, 3] = new TongdaoWrapper("蓝", bColor.B, stepTime,0);

			}
			else {
				setNotice("请不要使用鼠标中键点击按键。", true);
				return;
			}

			MaterialAst material = new MaterialAst
			{
				StepCount = stepCount,
				TongdaoCount = tongdaoCount,
				TdNameList = tdNameList,
				TongdaoList = tongdaoList,
			};
			mainForm.PreviewButtonClick(material);
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private bool generateColorMaterial()
		{
			if (stepCount == 0) {
				setNotice("尚未添加颜色块。", true);
				return false;
			}
			else
			{
				try
				{
					tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];
					for (int panelIndex = 1; panelIndex <= stepCount; panelIndex++)
					{
						Panel colorPanel = colorFLP.Controls[panelIndex] as Panel;
						int stepTime = decimal.ToInt32((colorPanel.Controls[0] as NumericUpDown).Value / eachStepTime);
						int changeMode = (colorPanel.Controls[1] as CheckBox).Checked ? 1 : 0;

						tongdaoList[panelIndex - 1, 0] = new TongdaoWrapper("总调光", tgTrackBar.Value, stepTime, changeMode);
						tongdaoList[panelIndex - 1, 1] = new TongdaoWrapper("红", colorPanel.BackColor.R, stepTime, changeMode);
						tongdaoList[panelIndex - 1, 2] = new TongdaoWrapper("绿", colorPanel.BackColor.G, stepTime, changeMode);
						tongdaoList[panelIndex - 1, 3] = new TongdaoWrapper("蓝", colorPanel.BackColor.B, stepTime, changeMode);
					}
					return true;
				}
				catch (Exception ex) {
					setNotice("生成数据出错：" + ex.Message, true);
					return false;
				}				
			}
		}

		#region 调节总调光

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
		/// 辅助方法：显示提示
		/// </summary>
		/// <param name="msgBoxShow"></param>
		private void setNotice(string msg, bool msgBoxShow)
		{
			myStatusLabel.Text = msg;
			if (msgBoxShow)
			{
				MessageBox.Show(msg);
			}
		}


		#endregion

		
	}
}
