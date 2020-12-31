using LightController.Ast;
using LightController.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm
{
	public partial class YMSetForm : Form
	{
		private MainFormBase mainForm;
		private IniFileHelper iniFileAst;
		private int frameCount = 0;

		public YMSetForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;
			iniFileAst = new IniFileHelper(mainForm.GlobalIniPath);
			InitializeComponent();

			#region 初始化几个数组			

			frameCount = MainFormBase.AllFrameList.Count;

			framePanels = new Panel[frameCount]; 
			frameLabels = new Label[frameCount];
			ymCheckBoxes = new CheckBox[frameCount];
			zxNumericUpDowns = new NumericUpDown[frameCount];
			jgNumericUpDowns = new NumericUpDown[frameCount];

			for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
			{
				addFramePanel(frameIndex,MainFormBase.AllFrameList[frameIndex]);
				ymCheckBoxes[frameIndex].CheckedChanged += new EventHandler(ymCheckBox_CheckedChanged);
				jgNumericUpDowns[frameIndex].ValueChanged += new EventHandler(jgNumericUpDown_ValueChanged);
				zxNumericUpDowns[frameIndex].ValueChanged += new EventHandler(zxNumericUpDown_ValueChanged);
			}

			#endregion
		}

		/// <summary>
		/// 辅助方法：添加每个场景的相关设置到FlowLayout中去
		/// </summary>
		private void addFramePanel(int frameIndex , string frameName) {
			
			// 
			// 容器
			// 
			framePanels[frameIndex] = new Panel
			{
				Location = new System.Drawing.Point(3, 3),
				Name = "panel" + (frameIndex + 1),
				Size = new System.Drawing.Size(66, 141),
			};

			// 
			// 场景名
			// 
			frameLabels[frameIndex] = new Label
			{
				AutoSize = true,
				Font = new System.Drawing.Font("黑体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
				Location = new System.Drawing.Point(8, 20),
				Margin = new System.Windows.Forms.Padding(2, 0, 2, 0),
				Name = "frameLabel" + (frameIndex + 1),
				Size = new System.Drawing.Size(45, 14),
				Text = frameName
			};
			myToolTip.SetToolTip(frameLabels[frameIndex], frameName);

			// 
			// 选中与否
			// 
			ymCheckBoxes[frameIndex] = new CheckBox
			{
				AutoSize = true,
				Location = new System.Drawing.Point(11, 47),
				Margin = new System.Windows.Forms.Padding(2),
				Name = "checkBox" + (frameIndex + 1),
				Size = new System.Drawing.Size(48, 16),
				Text = "摇麦",
				UseVisualStyleBackColor = true
			};

			// 
			// jgNumericUpDown12
			// 
			jgNumericUpDowns[frameIndex] = new NumericUpDown
			{
				Location = new System.Drawing.Point(11, 74),
				Margin = new System.Windows.Forms.Padding(2),
				Maximum = new decimal(new int[] { 10, 0, 0, 0 }),
				Minimum = new decimal(new int[] { 1, 0, 0, 0 }),
				Name = "jgNumericUpDown12",
				Size = new System.Drawing.Size(44, 21),
				TabIndex = 5,
				Value = new decimal(new int[] { 1, 0, 0, 0 })
			};
			// 
			// zxNumericUpDown12
			// 
			zxNumericUpDowns[frameIndex] = new NumericUpDown
			{
				Location = new System.Drawing.Point(11, 106),
				Margin = new System.Windows.Forms.Padding(2),
				Maximum = new decimal(new int[] { 60, 0, 0, 0 }),
				Minimum = new decimal(new int[] { 1, 0, 0, 0 }),
				Name = "zxNumericUpDown12",
				Size = new System.Drawing.Size(44, 21),
				TabIndex = 7,
				Value = new decimal(new int[] { 1, 0, 0, 0 })
			};
			// 
			// flowLayoutPanel1
			// 
			framePanels[frameIndex].Controls.Add(ymCheckBoxes[frameIndex]);
			framePanels[frameIndex].Controls.Add(frameLabels[frameIndex]);
			framePanels[frameIndex].Controls.Add(jgNumericUpDowns[frameIndex]);
			framePanels[frameIndex].Controls.Add(zxNumericUpDowns[frameIndex]);

			this.flowLayoutPanel1.Controls.Add(framePanels[frameIndex]);
		}

		/// <summary>
		///  事件：在Load中，执行loadAll();
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void YMSetForm_Load(object sender, EventArgs e)
		{
			Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
			LanguageHelper.InitForm(this);

			//1. 先设置各个下拉框的默认值：这里的三个选项(checkbox和numericUpDown)都不太需要设置
			//2.读取各个配置
			loadAll();
		}


		private void ymCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			int frameIndex = MathHelper.GetIndexNum(((CheckBox)sender).Name, -1 );
				
		}
		private void jgNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int frameIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
			//MessageBox.Show("frameIndex:" + ((NumericUpDown)sender).Value);
		}
		private void zxNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int frameIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
			//MessageBox.Show("frameIndex:" + ((NumericUpDown)sender).Value);
		}

		

		/// <summary>
		/// 辅助方法：从配置文件读取配置，并填入所有的框中
		/// </summary>
		private void loadAll()
		{
			for (int i = 0; i <frameCount; i++)
			{
				ymCheckBoxes[i].Checked = ( iniFileAst.ReadInt("YM", i + "CK", 0) == 1);
				jgNumericUpDowns[i].Value = iniFileAst.ReadInt("YM", i + "JG", 1);
				zxNumericUpDowns[i].Value =iniFileAst.ReadInt("YM", i + "ZX", 1);
			}
		}

		/// <summary>
		///  点击统一间隔时间按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyJGButton_Click(object sender, EventArgs e)
		{
			int tempValue = Convert.ToInt32(Double.Parse(commonJGNumericUpDown.Text));
			commonJGNumericUpDown.Value = tempValue;
			foreach (NumericUpDown item in jgNumericUpDowns)
			{
				item.Value  =  tempValue;
			}
		}

		/// <summary>
		///  事件：点击《统一执行时间》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyZXButton_Click(object sender, EventArgs e)
		{
			int tempValue = Convert.ToInt32(Double.Parse(commonZXNumericUpDown.Text));
			commonZXNumericUpDown.Value = tempValue;
			foreach (NumericUpDown item in zxNumericUpDowns)
			{
				item.Value = tempValue;
			}
		}

		/// <summary>
		///  事件：点击《保存设置》后，将所需的数据保存到global.ini中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ymSaveButton_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < frameCount; i++)
			{
				iniFileAst.WriteInt("YM", i + "CK", ymCheckBoxes[i].Checked?1:0);
				iniFileAst.WriteString("YM", i + "JG", jgNumericUpDowns[i].Text );
				iniFileAst.WriteString("YM", i + "ZX", zxNumericUpDowns[i].Text);
			}
			MessageBox.Show("保存成功:");
		}

		/// <summary>
		///  事件：勾选或取消勾选《全选》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			foreach (CheckBox item in ymCheckBoxes)
			{
				item.Checked = allCheckBox.Checked;
			}
		}

		/// <summary>
		/// 事件：点击《右上角关闭（X）》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void YMSetForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
		}
	}
}
