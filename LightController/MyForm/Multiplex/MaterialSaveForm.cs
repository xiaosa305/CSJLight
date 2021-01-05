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
		private MainFormBase mainForm;
		private IList<StepWrapper> stepWrapperList;
		private int tongdaoCount = 0;
		private int stepCount = 0;
		private int mode;
		private string materialPath;
		private string lightName;
		private string lightType;

		public MaterialSaveForm(MainFormBase mainForm, IList<StepWrapper> stepWrapperList ,int mode,string lightName,string lightType)
		{			
			if (stepWrapperList == null || stepWrapperList.Count == 0)
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("步数据为空，无法生成素材:"));
				this.Dispose();
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

			InitializeComponent();
			this.mainForm = mainForm;
			this.stepWrapperList = stepWrapperList;
			this.mode = mode;
			lightNameLabel.Text = lightName + " - " + lightType;

			materialPath = IniFileHelper.GetSavePath(Application.StartupPath) + @"\LightMaterial\";
			materialPath += mode == 0 ? "Normal" : "Sound";
			this.lightName = lightName ;
			this.lightType = lightType;

			startNumericUpDown.Maximum = stepCount;			
			endNumericUpDown.Maximum = stepCount;
			endNumericUpDown.Value = stepCount;

			#region 初始化自定义数组等

			tdCheckBoxes[0] = checkBox1;
			tdCheckBoxes[1] = checkBox2;
			tdCheckBoxes[2] = checkBox3;
			tdCheckBoxes[3] = checkBox4;
			tdCheckBoxes[4] = checkBox5;
			tdCheckBoxes[5] = checkBox6;
			tdCheckBoxes[6] = checkBox7;
			tdCheckBoxes[7] = checkBox8;
			tdCheckBoxes[8] = checkBox9;
			tdCheckBoxes[9] = checkBox10;
			tdCheckBoxes[10] = checkBox11;
			tdCheckBoxes[11] = checkBox12;
			tdCheckBoxes[12] = checkBox13;
			tdCheckBoxes[13] = checkBox14;
			tdCheckBoxes[14] = checkBox15;
			tdCheckBoxes[15] = checkBox16;
			tdCheckBoxes[16] = checkBox17;
			tdCheckBoxes[17] = checkBox18;
			tdCheckBoxes[18] = checkBox19;
			tdCheckBoxes[19] = checkBox20;
			tdCheckBoxes[20] = checkBox21;
			tdCheckBoxes[21] = checkBox22;
			tdCheckBoxes[22] = checkBox23;
			tdCheckBoxes[23] = checkBox24;
			tdCheckBoxes[24] = checkBox25;
			tdCheckBoxes[25] = checkBox26;
			tdCheckBoxes[26] = checkBox27;
			tdCheckBoxes[27] = checkBox28;
			tdCheckBoxes[28] = checkBox29;
			tdCheckBoxes[29] = checkBox30;
			tdCheckBoxes[30] = checkBox31;
			tdCheckBoxes[31] = checkBox32;

			for (int i = 0; i < tongdaoCount ; i++){
				tdCheckBoxes[i].Text = firstStep.TongdaoList[i].TongdaoName;
				tdCheckBoxes[i].Show();
			}

			#endregion
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
			string materialName = nameTextBox.Text;

			// 1.1 先判断各种信息，没问题了再保存
			if ( String.IsNullOrEmpty(materialName)) {
				MessageBox.Show(LanguageHelper.TranslateSentence("请输入素材名。"));
				return;
			}				
				
			//1.2 判断是否有非法字符 "\"和“/”
			if ( ! FileHelper.CheckFileName(materialName))
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("素材命名不规范，无法保存。"));
				return;
			}

			// 1.4 起始步、结束步的验证
			int startNum = decimal.ToInt32(startNumericUpDown.Value);
			int endNum =  decimal.ToInt32(endNumericUpDown.Value);
			if (startNum > endNum)
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("起始步不可大于结束步；请检查后重新保存。"));
				return;
			}

			// 1.5 判断选择通道数
			IList<int> tdIndexList = new List<int>();
			IList<string> tdNameList = new List<string>();
			for (int i = 0; i < tongdaoCount; i++)
			{
				if (tdCheckBoxes[i].Checked)
				{
					tdIndexList.Add(i);
					tdNameList.Add(tdCheckBoxes[i].Text);
				}
			}
			if (tdIndexList.Count == 0)
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("请选择至少一个通道，选择完成后重新保存。"));
				return;
			}

            // 1.6 直接检查是否可以生成DirectoryInfo
            string addName = genericRadioButton.Checked ? @"\通用\" : @"\" + lightName + @"\" + lightType+@"\"   ;

            materialName = addName + materialName;			
			string filePath = materialPath + @materialName +"("+ (endNum - startNum + 1) + "步)"+ ".ini";	

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

			// 1.7 判断名称是否已存在；若存在，选覆盖则先删除旧文件夹；否则退出方法。
			if (fi.Exists )
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
			File.Copy(sourcePath, filePath,true);

			//3.修改其中的数据
			IniFileHelper iniFileAst = new IniFileHelper(filePath);

			// 3.1 写[Set]内数据，包括几个要被记录的通道名
			iniFileAst.WriteString("Set","name", materialName);
			iniFileAst.WriteInt("Set","step",endNum - startNum +1);
			iniFileAst.WriteInt("Set", "tongdaoCount", tdIndexList.Count);
			for(int i=0;i<tdIndexList.Count;i++)
			{               
				iniFileAst.WriteString("TD",i.ToString(), tdNameList[i]);
			}
			// 3.2 写[Data]内数据，记录每一步的tongdaoList
			int selectedStepIndex = 0;
			for(int stepIndex = startNum-1; stepIndex < endNum; stepIndex++)
			{
				StepWrapper stepWrapper = stepWrapperList[stepIndex];
				for (int selectedTdIndex = 0; selectedTdIndex < tdIndexList.Count; selectedTdIndex++)
				{
					int tdIndex = tdIndexList[selectedTdIndex];
					TongdaoWrapper tongdaoWrapper = stepWrapper.TongdaoList[tdIndex];

					iniFileAst.WriteInt("Data", selectedStepIndex + "_" + selectedTdIndex + "_V" ,  tongdaoWrapper.ScrollValue );
					iniFileAst.WriteInt("Data", selectedStepIndex + "_" + selectedTdIndex + "_CM", tongdaoWrapper.ChangeMode);
					iniFileAst.WriteInt("Data", selectedStepIndex + "_" + selectedTdIndex + "_ST", tongdaoWrapper.StepTime);
				}
				selectedStepIndex++;
			}										
			MessageBox.Show(LanguageHelper.TranslateSentence("成功保存素材。"));

			Dispose();
			mainForm.Activate();
		}

		/// <summary>
		///  点击取消按钮后,Dispose窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
			mainForm.Activate();
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
				tdCheckBoxes[i].Checked = selectAllCheckBox.Checked;
			}
		}

		private void MaterialForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			LanguageHelper.InitForm(this);
		}
		
		/// <summary>
		///  事件：输入字符事件，不可输入 \ 和 / ; 其他非法字符可以在之后被DirectoryInfo检查出来
		///  -- 可先将shortcutEnable 设为false；若不设置，仍可以用粘贴方法导入错误的素材名
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nameTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
			if (e.KeyChar != '\\' && e.KeyChar != '/')
			{
				e.Handled = false;
			}
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
		/// 事件：点击《全选》功能
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allStepSkinButton_Click(object sender, EventArgs e)
		{
			startNumericUpDown.Value = startNumericUpDown.Minimum;
			endNumericUpDown.Value = endNumericUpDown.Maximum;
		}
	}
}
