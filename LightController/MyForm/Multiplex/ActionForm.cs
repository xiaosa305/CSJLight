using LightController.Ast;
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
	public partial class ActionForm : Form
	{
		private MainFormBase mainForm;
		private MaterialAst material;
		private int commonStepTime = 50;
		IList<string> tdNameList = new List<string> { "X轴", "Y轴" }; // 为tdNameList赋值；此列表是固定的

		public ActionForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;

			InitializeComponent();			
		}

		/// <summary>
		/// 辅助方法：鼠标所在之处为界面Location
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActionForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
		}

		/// <summary>
		/// 每次激活后，需要重新刷新步时间（避免主界面更改了时间因子造成的显示问题）；也必须重新隐藏或显示预览的按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActionForm_Activated(object sender, EventArgs e)
		{
			previewButton.Visible = mainForm.IsConnected; 

			StNumericUpDown.Maximum = MainFormBase.MAX_StTimes * mainForm.EachStepTime2;
			StNumericUpDown.Increment = mainForm.EachStepTime2;
			StNumericUpDown.Value = mainForm.EachStepTime2 * commonStepTime;
		}

		/// <summary>
		/// 事件：关闭窗体时，如果正在预览，则停止预览
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ActionForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (mainForm.IsPreviewing)
			{
				mainForm.PreviewButtonClick(null);
			}
		}

		/// <summary>
		/// 事件：点击《预览 | 停止预览》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewButton_Click(object sender, EventArgs e)
		{
			previewButton.Text = mainForm.IsPreviewing ? "预览" : "停止预览";

			//DOTO：需要判断 预览还是停止预览
			if (generateAction())
			{
				mainForm.PreviewButtonClick(material);				
			}
		}

		/// <summary>
		/// 事件：点击《使用动作》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{
			if ( generateAction() ) {
				mainForm.InsertOrCoverMaterial(material, InsertMethod.INSERT);
				Hide();
				mainForm.Activate();
			}				
		}

		/// <summary>
		/// 辅助方法：生成动作的素材（供预览和使用动作）
		/// </summary>
		private bool generateAction()
		{
			int tabIndex = actionTabControl.SelectedIndex;
			switch (tabIndex)
			{
				case 0: drawLine(); break;
				case 1: drawCircle(); break;
				case 2: drawSemicircle(); break;
				case 3: drawWave(); break;
				case 4: draw8(); break;
			}
			if (material == null)
			{
				setNotice("生成动作数据出错，请重试", true);
				return false;
			}
			return true;
		}
		
		/// <summary>
		/// 事件：点击《取消》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Hide();
			mainForm.Activate();
		}
		
		#region 画直线相关的方法：包括验证，生成数据等；

		/// <summary>
		///  辅助方法：制造画圆的图形
		/// </summary>
		private void drawLine()
		{
			int xValue = decimal.ToInt32(lineXNumericUpDown.Value);
			int y1Value = decimal.ToInt32(lineY1NumericUpDown.Value);
			int y2Value = decimal.ToInt32(lineY2NumericUpDown.Value);
			int phase = decimal.ToInt32(linePhaseNumericUpDown.Value) - 1;
		
			int stepCount = 4;
			int tongdaoCount = 2;
						
			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			// X轴值固定，无需处理相位
			tongdaoList[0, 0] = new TongdaoWrapper("X轴", xValue, commonStepTime);
			tongdaoList[1, 0] = new TongdaoWrapper("X轴", xValue, commonStepTime);
			tongdaoList[2, 0] = new TongdaoWrapper("X轴", xValue, commonStepTime);
			tongdaoList[3, 0] = new TongdaoWrapper("X轴", xValue, commonStepTime);
						
			tongdaoList[(0+phase) % stepCount , 1] = new TongdaoWrapper("Y轴", y1Value, commonStepTime);
			tongdaoList[(1+phase) % stepCount , 1] = new TongdaoWrapper("Y轴", y1Value + y2Value / 2, commonStepTime);
			tongdaoList[(2+phase) % stepCount , 1] = new TongdaoWrapper("Y轴", y1Value + y2Value, commonStepTime);
			tongdaoList[(3+phase) % stepCount , 1] = new TongdaoWrapper("Y轴", y1Value + y2Value / 2, commonStepTime);

			material = new MaterialAst
			{
				StepCount = stepCount,
				TongdaoCount = tongdaoCount,
				TdNameList = tdNameList,
				TongdaoList = tongdaoList
			};
		}
				
		/// <summary>
		/// 事件：当Y1发生变化时，动态调整Y2的最大值（避免超过范围）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lineY1NumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int y1Value = decimal.ToInt32(lineY1NumericUpDown.Value);
			lineY2NumericUpDown.Maximum = 255 - y1Value;
		}

		/// <summary>
		/// 事件：更改了步时间之后，应该调整相应的stepTime
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lineStNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			commonStepTime = decimal.ToInt32(StNumericUpDown.Value / mainForm.EachStepTime2);

			// 需要在这里对波浪的倍率项进行限制
			waveTimesNumericUpDown.Maximum = 250 / commonStepTime;
			
		}

		#endregion

		#region 画圆及半圆相关的方法：包括验证，生成数据等；

		/// <summary>
		/// 辅助方法：生成圆的素材
		/// </summary>
		private void drawCircle()
		{
			int xValue = decimal.ToInt32(circleXNumericUpDown.Value);
			int yValue = decimal.ToInt32(circleYNumericUpDown.Value);
			int phase = decimal.ToInt32(circlePhaseNumericUpDown.Value) - 1;

			int stepCount = 4;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			tongdaoList[(0+ phase) % stepCount, 0] = new TongdaoWrapper("X轴", 0, commonStepTime);
		    tongdaoList[(1 + phase) % stepCount, 0] = new TongdaoWrapper("X轴", xValue / 2, commonStepTime);
			tongdaoList[(2 + phase) % stepCount, 0] = new TongdaoWrapper("X轴", xValue , commonStepTime);
			tongdaoList[(3 + phase) % stepCount, 0] = new TongdaoWrapper("X轴", xValue /2 , commonStepTime);

			//　Y轴每步都一样，无需处理相位。
			tongdaoList[0, 1] = new TongdaoWrapper("Y轴", yValue, commonStepTime);
			tongdaoList[1, 1] = new TongdaoWrapper("Y轴", yValue, commonStepTime);
			tongdaoList[2, 1] = new TongdaoWrapper("Y轴", yValue, commonStepTime);
			tongdaoList[3, 1] = new TongdaoWrapper("Y轴", yValue, commonStepTime);


			material = new MaterialAst
			{
				StepCount = stepCount,
				TongdaoCount = tongdaoCount,
				TdNameList = tdNameList,
				TongdaoList = tongdaoList
			};
		}

		/// <summary>
		/// 辅助方法：生成半圆的素材
		/// </summary>
		private void drawSemicircle()
		{
			int xValue = decimal.ToInt32(scXNumericUpDown.Value);
			int yValue = decimal.ToInt32(scYNumericUpDown.Value);
			int phase = decimal.ToInt32(semicirclePhaseNumericUpDown.Value) - 1;
			
			int stepCount = 4;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			// 其实和圆的代码完全相同，区别只在于圆的X轴 转向需要超过170（而半圆必须小于170）
			tongdaoList[(0 + phase) % stepCount, 0] = new TongdaoWrapper("X轴", 0, commonStepTime);
			tongdaoList[(1 + phase) % stepCount, 0] = new TongdaoWrapper("X轴", xValue / 2, commonStepTime);
			tongdaoList[(2 + phase) % stepCount, 0] = new TongdaoWrapper("X轴", xValue, commonStepTime);
			tongdaoList[(3 + phase) % stepCount, 0] = new TongdaoWrapper("X轴", xValue / 2, commonStepTime);

			//Y轴每步都一样，无需处理相位。
			tongdaoList[0, 1] = new TongdaoWrapper("Y轴", yValue, commonStepTime);
			tongdaoList[1, 1] = new TongdaoWrapper("Y轴", yValue, commonStepTime);
			tongdaoList[2, 1] = new TongdaoWrapper("Y轴", yValue, commonStepTime);
			tongdaoList[3, 1] = new TongdaoWrapper("Y轴", yValue, commonStepTime);

			material = new MaterialAst
			{
				StepCount = stepCount,
				TongdaoCount = tongdaoCount,
				TdNameList = tdNameList,
				TongdaoList = tongdaoList
			};
		}

		#endregion

		#region 画波浪相关的方法：包括验证，生成数据等；

		/// <summary>
		/// 生成波浪的素材
		/// </summary>
		private void drawWave()
		{
			int x1Value = decimal.ToInt32(waveX1NumericUpDown.Value);
			int x2Value = decimal.ToInt32(waveX2NumericUpDown.Value);
			int y1Value = decimal.ToInt32(waveY1NumericUpDown.Value);
			int y2Value = decimal.ToInt32(waveY2NumericUpDown.Value);
			int times = decimal.ToInt32(waveTimesNumericUpDown.Value);

			int stepCount = 2;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			tongdaoList[0, 0] = new TongdaoWrapper("X轴", x1Value, commonStepTime * times);
			tongdaoList[1, 0] = new TongdaoWrapper("X轴", x1Value + x2Value, commonStepTime * times);

			tongdaoList[0, 1] = new TongdaoWrapper("Y轴", y1Value, commonStepTime );
			tongdaoList[1, 1] = new TongdaoWrapper("Y轴", y1Value + y2Value, commonStepTime );

			material = new MaterialAst
			{
				StepCount = stepCount,
				TongdaoCount = tongdaoCount,
				TdNameList = tdNameList,
				TongdaoList = tongdaoList
			};

		}

		/// <summary>
		///  当《X轴初始位置》发生改变后，《X轴变化幅度》也要有所限制
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void waveX1NumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int x1Value = decimal.ToInt32(waveX1NumericUpDown.Value);
			waveX2NumericUpDown.Maximum = 255 - x1Value;
		}

		/// <summary>
		///  当《Y轴初始位置》发生改变后，《Y轴摆动幅度》也要有所限制
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void waveY1NumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int y1Value = decimal.ToInt32(waveY1NumericUpDown.Value);
			waveY2NumericUpDown.Maximum = 127 - y1Value;
		}

		#endregion

		#region 画8字相关方法：包括验证，生成数据等；

		/// <summary>
		/// 辅助方法：生成8字的素材
		/// </summary>
		private void draw8()
		{
			int yValue = decimal.ToInt32(eightYNumericUpDown.Value);
			int phase = decimal.ToInt32(eightPhaseNumericUpDown.Value) - 1;

			int stepCount = 4;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];
						
			tongdaoList[(0 + phase) % stepCount,  0] = new TongdaoWrapper("X轴", 0, commonStepTime);
			tongdaoList[(1 + phase)  % stepCount, 0] = new TongdaoWrapper("X轴", 85 , commonStepTime);
			tongdaoList[(2 + phase)  % stepCount,  0] = new TongdaoWrapper("X轴", 85, commonStepTime);
			tongdaoList[(3 + phase)  % stepCount,  0] = new TongdaoWrapper("X轴", 0, commonStepTime);

			tongdaoList[(0 + phase) % stepCount, 1] = new TongdaoWrapper("Y轴", yValue, commonStepTime);
			tongdaoList[(1 + phase) % stepCount, 1] = new TongdaoWrapper("Y轴", yValue, commonStepTime);
			tongdaoList[(2 + phase) % stepCount, 1] = new TongdaoWrapper("Y轴", 255-yValue, commonStepTime);
			tongdaoList[(3 + phase) % stepCount, 1] = new TongdaoWrapper("Y轴", 255 - yValue, commonStepTime);

			material = new MaterialAst
			{
				StepCount = stepCount,
				TongdaoCount = tongdaoCount,
				TdNameList = tdNameList,
				TongdaoList = tongdaoList
			};

		}

		#endregion

		/// <summary>
		/// 辅助方法：显示提示文字（如有必要，用弹窗提示）
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="msgShow"></param>
		private void setNotice(string msg, bool msgShow) {
			myStatusLabel.Text = msg;
			if (msgShow) {
				MessageBox.Show(msg);
			}
		}
			
	}
}
