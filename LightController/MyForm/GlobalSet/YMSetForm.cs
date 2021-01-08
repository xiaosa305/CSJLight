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
		
		public YMSetForm(MainFormBase mainForm)
		{
			this.mainForm = mainForm;
			iniFileAst = new IniFileHelper(mainForm.GlobalIniPath);
			InitializeComponent();

			#region 初始化几个数组			

			//sceneCount = MainFormBase.SceneCount;

			scenePanels = new Panel[MainFormBase.SceneCount]; 
			sceneLabels = new Label[MainFormBase.SceneCount];
			ymCheckBoxes = new CheckBox[MainFormBase.SceneCount];
			zxNumericUpDowns = new NumericUpDown[MainFormBase.SceneCount];
			jgNumericUpDowns = new NumericUpDown[MainFormBase.SceneCount];

			for (int frameIndex = 0; frameIndex < MainFormBase.SceneCount; frameIndex++)
			{
				addScenePanel(frameIndex,MainFormBase.AllFrameList[frameIndex]);
			}

			#endregion
		}

		/// <summary>
		/// 辅助方法：添加每个场景的相关设置到FlowLayout中去
		/// </summary>
		private void addScenePanel(int sceneIndex , string sceneName) {
			
			// 
			// 容器
			// 
			scenePanels[sceneIndex] = new Panel
			{
				Location = new System.Drawing.Point(3, 3),
				Name = "panel" + (sceneIndex + 1),
				Size = new System.Drawing.Size(66, 141),
			};

			// 
			// 场景名
			// 
			sceneLabels[sceneIndex] = new Label
			{
				AutoSize = true,
				Font = new System.Drawing.Font("黑体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
				Location = new System.Drawing.Point(8, 20),
				Margin = new System.Windows.Forms.Padding(2, 0, 2, 0),
				Name = "frameLabel" + (sceneIndex + 1),
				Size = new System.Drawing.Size(45, 14),
				Text = sceneName
			};
			myToolTip.SetToolTip(sceneLabels[sceneIndex], sceneName);

			// 
			// 选中与否
			// 
			ymCheckBoxes[sceneIndex] = new CheckBox
			{
				AutoSize = true,
				Location = new System.Drawing.Point(11, 47),
				Margin = new System.Windows.Forms.Padding(2),
				Name = "checkBox" + (sceneIndex + 1),
				Size = new System.Drawing.Size(48, 16),
				Text = LanguageHelper.TranslateWord("摇麦"),
				UseVisualStyleBackColor = true
			};

			// 
			// jgNumericUpDown12
			// 
			jgNumericUpDowns[sceneIndex] = new NumericUpDown
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
			zxNumericUpDowns[sceneIndex] = new NumericUpDown
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
			scenePanels[sceneIndex].Controls.Add(ymCheckBoxes[sceneIndex]);
			scenePanels[sceneIndex].Controls.Add(sceneLabels[sceneIndex]);
			scenePanels[sceneIndex].Controls.Add(jgNumericUpDowns[sceneIndex]);
			scenePanels[sceneIndex].Controls.Add(zxNumericUpDowns[sceneIndex]);

			this.flowLayoutPanel1.Controls.Add(scenePanels[sceneIndex]);
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
		
		/// <summary>
		/// 辅助方法：从配置文件读取配置，并填入所有的框中
		/// </summary>
		private void loadAll()
		{
			for (int i = 0; i < MainFormBase.SceneCount; i++)
			{
				ymCheckBoxes[i].Checked = ( iniFileAst.ReadInt("YM", i + "CK", 0) == 1);
				jgNumericUpDowns[i].Value = iniFileAst.ReadInt("YM", i + "JG", 1);
				zxNumericUpDowns[i].Value =iniFileAst.ReadInt("YM", i + "ZX", 1);
			}
		}

		/// <summary>
		///  辅助方法：点击《统一间隔时间》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyJGButton_Click(object sender, EventArgs e)
		{
			foreach (NumericUpDown item in jgNumericUpDowns)
			{
				item.Value  =  unifyJGNumericUpDown.Value;
			}
		}

		/// <summary>
		///  事件：点击《统一执行时间》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyZXButton_Click(object sender, EventArgs e)
		{
			foreach (NumericUpDown item in zxNumericUpDowns)
			{
				item.Value = unifyZXNumericUpDown.Value;
			}
		}

		/// <summary>
		///  事件：点击《保存设置》后，将所需的数据保存到global.ini中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ymSaveButton_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < MainFormBase.SceneCount ; i++)
			{
				iniFileAst.WriteInt("YM", i + "CK", ymCheckBoxes[i].Checked?1:0);
				iniFileAst.WriteString("YM", i + "JG", jgNumericUpDowns[i].Text );
				iniFileAst.WriteString("YM", i + "ZX", zxNumericUpDowns[i].Text);
			}
			MessageBox.Show(LanguageHelper.TranslateSentence("保存成功。"));
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
