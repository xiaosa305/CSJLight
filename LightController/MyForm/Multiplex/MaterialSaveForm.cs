using LightController.Ast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LightController.Common;

namespace LightController.MyForm
{
	public partial class MaterialSaveForm : Form
	{
		private CheckBox[] tdCBs;		

		private MainFormBase mainForm;
		private IList<StepWrapper> stepWrapperList;
		private int tongdaoCount = 0;
		private int stepCount = 0;
		private int mode;
		private string materialPath;
		private string lightName;
		private string lightType;		

		public MaterialSaveForm(MainFormBase mainForm, IList<StepWrapper> stepWrapperList ,int mode,LightAst la,int currentStep)
		{			
			if (stepWrapperList == null || stepWrapperList.Count == 0)
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("步数据为空，无法生成素材:"));
				Dispose();
				return;
			}

			stepCount = stepWrapperList.Count;
			StepWrapper firstStep = stepWrapperList[0];
			tongdaoCount = firstStep.TongdaoList.Count;
			if (tongdaoCount == 0) {
				MessageBox.Show(LanguageHelper.TranslateSentence("通道数据为空，无法生成素材:"));
				this.Dispose();
				return;
			}

			this.mainForm = mainForm;
			this.stepWrapperList = stepWrapperList;
			this.mode = mode;
			lightName = la.LightName;
			lightType = la.LightType;

			InitializeComponent();

			lightNameLabel.Text = lightName + " - " + lightType;

			materialPath = IniHelper.GetSavePath() + @"\LightMaterial\";
			materialPath += mode == 0 ? "Normal" : "Sound";

			startNumericUpDown.Maximum = stepCount;			
			endNumericUpDown.Maximum = stepCount;
			startNumericUpDown.Value = currentStep;
			endNumericUpDown.Value = stepCount;

			#region 初始化自定义数组等

			tdCBs = new CheckBox[tongdaoCount];
			for (int tdIndex = 0; tdIndex < tongdaoCount ; tdIndex++){
				tdCBs[tdIndex] = new CheckBox
				{
					Font = tdCBDemo.Font,
					Location = tdCBDemo.Location,
					Margin = tdCBDemo.Margin,			
					Size = tdCBDemo.Size,		
					UseVisualStyleBackColor = tdCBDemo.UseVisualStyleBackColor,
					Text = firstStep.TongdaoList[tdIndex].TongdaoCommon.TongdaoName,
				};
				tdFLP.Controls.Add(tdCBs[tdIndex]);
			}

			#endregion
		}

		private void MaterialForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			LanguageHelper.InitForm(this);
		}

		/// <summary>
		///  点击取消按钮后,Dispose窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Dispose();
			mainForm.Activate();
		}

		/// <summary>
		/// 事件：点击《右上角？》按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MaterialSaveForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show(
				LanguageHelper.TranslateSentence("素材名不可使用\\、/、:、*、?、\"、<、>、| 等字符，否则操作系统(windows)无法保存，会出现错误。"),
				LanguageHelper.TranslateSentence("提示"),
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
			e.Cancel = true;
		}

		/// <summary>
		///  保存按钮点击后
		///  1. 验证文件名是否为空
		///  2.验证文件夹是否已存在
		///  3.取出select的所有checkBox，然后加入一个stringList中
		///  4.先试着保存materialSet.ini
		///  5.保存成功后，关闭窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			// 验证《起始步》、《结束步》
			int startNum = decimal.ToInt32(startNumericUpDown.Value);
			int endNum =  decimal.ToInt32(endNumericUpDown.Value);
			if (startNum > endNum)
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("起始步不可大于结束步；请检查后重新保存。"));
				return;
			}

			// 验证《选中通道》
			IList<int> tdIndexList = new List<int>();
			IList<string> tdNameList = new List<string>();
			for (int tdIndex = 0; tdIndex < tongdaoCount; tdIndex++)
			{
				if (tdCBs[tdIndex].Checked)
				{
					tdIndexList.Add(tdIndex);
					tdNameList.Add(tdCBs[tdIndex].Text);
				}
			}
			if (tdIndexList.Count == 0)
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("请选择至少一个通道，选择完成后重新保存。"));
				return;
			}

			// 判断是临时素材(内存)还是持久素材(硬盘)
			if ( tempRadioButton.Checked ) {

				int selectedStepCount = endNum - startNum + 1;
				int selectedTongdaoCount = tdIndexList.Count;
				TongdaoWrapper[,] tdArray = new TongdaoWrapper[selectedStepCount, selectedTongdaoCount];

				int selectedStepIndex = 0;
				for (int stepIndex = startNum - 1; stepIndex < endNum; stepIndex++)
				{
					StepWrapper stepWrapper = stepWrapperList[stepIndex];
					for (int selectedTdIndex = 0; selectedTdIndex < selectedTongdaoCount; selectedTdIndex++)
					{
						int tdIndex = tdIndexList[selectedTdIndex];
						TongdaoWrapper tongdaoWrapper = stepWrapper.TongdaoList[tdIndex];

						tdArray[selectedStepIndex, selectedTdIndex] = new TongdaoWrapper()
						{
							TongdaoCommon = tongdaoWrapper.TongdaoCommon,
							ScrollValue = tongdaoWrapper.ScrollValue,
							ChangeMode = tongdaoWrapper.ChangeMode,
							StepTime = tongdaoWrapper.StepTime
						};
					}
					selectedStepIndex++;
				}

				mainForm.TempMaterialAst = new MaterialAst()
				{
					Mode = mode,
					StepCount = selectedStepCount,
					TdNameList = tdNameList,
					TongdaoArray = tdArray
				};
				MessageBox.Show(LanguageHelper.TranslateSentence("成功保存临时素材。"));
			}
			else {
				// 验证《素材名》
				string materialName = mNameTextBox.Text;
				if (string.IsNullOrEmpty(materialName))
				{
					MessageBox.Show(LanguageHelper.TranslateSentence("请输入素材名。"));
					return;
				}
				// 判断是否有非法字符 "\"和“/”
				if (!FileHelper.CheckFileName(materialName))
				{
					MessageBox.Show(LanguageHelper.TranslateSentence("素材命名不规范，无法保存。"));
					return;
				}
				// 验证是否可以生成DirectoryInfo
				string addName = genericRadioButton.Checked ? @"\通用\" : @"\" + lightName + @"\" + lightType + @"\";
				materialName = addName + materialName;
				string filePath = materialPath + @materialName + "(" + (endNum - startNum + 1) + "步)" + ".ini";

				FileInfo fi = null;
				try
				{
					fi = new FileInfo(filePath);
				}
				catch (Exception ex)
				{
					MessageBox.Show("输入了错误的字符;\n" + ex.Message);
					return;
				}
				// 判断名称是否已存在；若存在，选覆盖则先删除旧文件夹；否则退出方法。
				if (fi.Exists)
				{
					DialogResult dr = MessageBox.Show(
						LanguageHelper.TranslateSentence("当前名称已有素材，是否覆盖？"),
						"",
						MessageBoxButtons.OKCancel,
						MessageBoxIcon.Question
					);
					if (dr == DialogResult.OK)
					{
						fi.Delete();
					}
					else
					{
						return;
					}
				}

				// 2.将相关文件拷贝到文件夹内	
				string sourcePath = Application.StartupPath + @"\materialSet.ini";
				string strPath = Path.GetDirectoryName(filePath);
				if (!Directory.Exists(strPath))
				{
					Directory.CreateDirectory(strPath);
				}
				File.Copy(sourcePath, filePath, true);

				//3.修改其中的数据
				IniHelper iniFileAst = new IniHelper(filePath);

				// 3.1 写[Set]内数据，包括几个要被记录的通道名
				iniFileAst.WriteString("Set", "name", materialName);
				iniFileAst.WriteInt("Set", "step", endNum - startNum + 1);
				iniFileAst.WriteInt("Set", "tongdaoCount", tdIndexList.Count);
				for (int i = 0; i < tdIndexList.Count; i++)
				{
					iniFileAst.WriteString("TD", i.ToString(), tdNameList[i]);
				}
				// 3.2 写[Data]内数据，记录每一步的tongdaoList
				int selectedStepIndex = 0;
				for (int stepIndex = startNum - 1; stepIndex < endNum; stepIndex++)
				{
					StepWrapper stepWrapper = stepWrapperList[stepIndex];
					for (int selectedTdIndex = 0; selectedTdIndex < tdIndexList.Count; selectedTdIndex++)
					{
						int tdIndex = tdIndexList[selectedTdIndex];
						TongdaoWrapper tongdaoWrapper = stepWrapper.TongdaoList[tdIndex];

						iniFileAst.WriteInt("Data", selectedStepIndex + "_" + selectedTdIndex + "_V", tongdaoWrapper.ScrollValue);
						iniFileAst.WriteInt("Data", selectedStepIndex + "_" + selectedTdIndex + "_CM", tongdaoWrapper.ChangeMode);
						iniFileAst.WriteInt("Data", selectedStepIndex + "_" + selectedTdIndex + "_ST", tongdaoWrapper.StepTime);
					}
					selectedStepIndex++;
				}
				MessageBox.Show(LanguageHelper.TranslateSentence("成功保存素材。"));
			}
						
			Dispose();
			mainForm.Activate();
		}
		
		#region 各监听事件

		/// <summary>
		///  事件：输入字符事件，不可输入 \ 和 / ; 其他非法字符可以在之后被DirectoryInfo检查出来
		///  -- 可先将shortcutEnable 设为false；若不设置，仍可以用粘贴方法导入错误的素材名
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
			if (e.KeyChar != '\\' && e.KeyChar != '/')
			{
				e.Handled = false;
			}
		}
						
		/// <summary>
		/// 事件：点击《全选》功能
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allStepSkinButton_Click(object sender, EventArgs e)
		{
			startNumericUpDown.Value = startNumericUpDown.Minimum;
			endNumericUpDown.Value = endNumericUpDown.Maximum;
		}

		/// <summary>
		/// 全选框勾选与否的操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void selectAllCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < tongdaoCount; i++)
			{
				tdCBs[i].Checked = selectAllCheckBox.Checked;
			}
		}

		/// <summary>
		///  事件：根据选中的单选框，更改《按钮名》和使能《素材名称》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tempRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			mNameTextBox.Enabled = ! tempRadioButton.Checked;
		}

		#endregion

	}
}
