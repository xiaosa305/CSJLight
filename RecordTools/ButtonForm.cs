using Dickov.Utils;
using RecordTools.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RecordTools
{
	public partial class ButtonForm : Form
	{
		private IniFileHelper iniHelper;
		private decimal eachStepTime = .04m;
		private string savePath;
		private int sceneNo = 1;

		// 以下变量，为《分页显示》功能必须的变量 
		private int currentPage = 1;		
		private int eachCount = 16;    // 如果此项大于tdCount,则应设为tdCount的值

		private HashSet<int> tdSet; // 记录使用的通道
		private MusicSceneConfig musicSceneConfig;  //维佳的接口

		public ButtonForm()
		{
			InitializeComponent();

			string loadexeName = Application.ExecutablePath;
			FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(loadexeName);
			string appFileVersion = string.Format("{0}.{1}.{2}.{3}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);
			Text += " v" + appFileVersion;

			// 载入场景
			IList<string> frameList = TextHelper.Read(Application.StartupPath + @"\FrameList.txt");
			frameComboBox.Items.Add("无开机场景");
			for (int frameIndex = 0; frameIndex < frameList.Count; frameIndex++)
			{
				frameComboBox.Items.Add(frameList[frameIndex]);
			}
			frameComboBox.SelectedIndex = sceneNo;

			// 读取各个默认配置
			iniHelper = new IniFileHelper(Application.StartupPath + @"\CommonSet.ini");
			eachStepTime = iniHelper.ReadInt("CommonSet", "EachStepTime", 40) / 1000m;
			int stepTime = iniHelper.ReadInt("CommonSet", "StepTime", 10);

			//添加frameStepTimeNumericUpDown相关初始化及监听事件			
			stepTimeNumericUpDown.Increment = eachStepTime;
			stepTimeNumericUpDown.Maximum = 250 * eachStepTime;
			stepTimeNumericUpDown.Value = eachStepTime * stepTime;
			stepTimeNumericUpDown.MouseWheel += someNUD_MouseWheel;

			jgtNumericUpDown.Value = iniHelper.ReadInt("CommonSet", "JG", 0);
			jgtNumericUpDown.MouseWheel += someNUD_MouseWheel;

			savePath = iniHelper.ReadString("CommonSet", "SavePath", @"C:\Temp\CSJ");
			saveFolderBrowserDialog.SelectedPath = savePath;
			setSavePathLabel();
			sceneNo = iniHelper.ReadInt("CommonSet", "SceneNo", 1);
			if (sceneNo < 1 || sceneNo > 32)
			{
				sceneNo = 1;
			}
			setSceneNo(false);

			sceneNoTextBox.LostFocus += new EventHandler(sceneNoTextBox_LostFocus);

			// 初始化各个组件
			tdSet = new HashSet<int>() { };

			for (int pageIndex = 0; pageIndex < 32; pageIndex++)
			{
				Button pageBtn = new Button
				{
					Text = (pageIndex + 1).ToString(),
					Size = tdButtonDemo.Size,
					ForeColor = tdButtonDemo.ForeColor,
					Visible = true,
				};
				pageFLP.Controls.Add(pageBtn);
				pageBtn.Click += pageBtn_Click;
			}

			for (int cbIndex = 0; cbIndex < eachCount; cbIndex++)
			{
				Button tdBtn = new Button
				{
					Size = tdButtonDemo.Size,
					ForeColor = tdButtonDemo.ForeColor,
					Visible = true,
				};
				tdFLP.Controls.Add(tdBtn);
				tdBtn.Click += tdBtn_Click;
			}

			refreshPage();
		}

		private void ButtonForm_Load(object sender, EventArgs e) { }

		/// <summary>
		/// 事件：点击《页面按钮》切换页面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pageBtn_Click(object sender, EventArgs e)
		{
			currentPage = int.Parse((sender as Button).Text);
			refreshPage();
		}

		/// <summary>
		/// 事件：点击《通道按钮》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdBtn_Click(object sender, EventArgs e)
		{
			Button tdBtn = sender as Button;
			int tdIndex = int.Parse(tdBtn.Text);

			if (!tdSet.Contains(tdIndex))
			{
				tdBtn.BackColor = Color.Blue;
				tdBtn.ForeColor = Color.AliceBlue;
				tdSet.Add(tdIndex);
			}
			else
			{
				tdBtn.BackColor = SystemColors.Control;
				tdBtn.ForeColor = SystemColors.ControlText;
				tdSet.Remove(tdIndex);
			}
		}

		/// <summary>
		/// 辅助方法：刷新页面（根据当前的页面，把相应的checkBox的CheckBox设为正确的值）
		/// </summary>
		private void refreshPage()
		{
			Console.WriteLine("refreshPage()");
			// 显示页数(灯具)
			for (int pageIndex = 0; pageIndex < 32; pageIndex++)
			{
				Button pageBtn = pageFLP.Controls[pageIndex] as Button;
				pageBtn.BackColor = (pageIndex + 1) == currentPage ? Color.DarkSalmon : SystemColors.Control;
			}

			// 遍历改通道名 ,并将已在Set内的通道值勾上
			for (int btnIndex = 0; btnIndex < eachCount; btnIndex++)
			{
				Button btn = tdFLP.Controls[btnIndex] as Button;

				int tdIndex = (currentPage - 1) * eachCount + btnIndex;
				btn.Text = (tdIndex + 1).ToString();
				btn.Name = "tbBtn" + (tdIndex + 1);

				if (tdSet.Contains(tdIndex + 1))
				{
					btn.BackColor = Color.Blue;
					btn.ForeColor = Color.AliceBlue;
				}
				else
				{
					btn.BackColor = SystemColors.Control;
					btn.ForeColor = SystemColors.ControlText;
				}
			}
		}
				
		#region 配置相关的一些参数，及监听事件

		/// <summary>
		/// 事件：步时间发生变化时，改变相应的stepTime；
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void stepTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int stepTime = Decimal.ToInt32(stepTimeNumericUpDown.Value / eachStepTime);
			stepTimeNumericUpDown.Value = stepTime * eachStepTime;
		}

		/// <summary>
		///  事件：点击《设置保存目录》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setFilePathButton_Click(object sender, EventArgs e)
		{
			DialogResult dr = saveFolderBrowserDialog.ShowDialog();
			if (dr == DialogResult.OK)
			{
				savePath = saveFolderBrowserDialog.SelectedPath;
				setSavePathLabel();

				setNotice("已设置存放目录为：" + savePath, false);
			}
		}

		/// <summary>
		/// 辅助方法：根据当前的savePath，设置label及toolTip
		/// </summary>
		private void setSavePathLabel()
		{
			if (savePath != null)
			{
				recordPathLabel.Text = savePath;
				myToolTip.SetToolTip(recordPathLabel, savePath);
			}
		}

		/// <summary>
		/// 事件：《recordTextBox》失去焦点，把文字做相关的转换
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void sceneNoTextBox_LostFocus(object sender, EventArgs e)
		{
			if (sceneNoTextBox.Text.Length == 0)
			{
				setNotice("文件序号不得为空", true);
				sceneNoTextBox.Text = "1";
			}
			sceneNo = int.Parse(sceneNoTextBox.Text);
			if (sceneNo < 1)
			{
				setNotice("文件序号不得小于1", true);
				sceneNo = 1;
			}
			else if (sceneNo > 32)
			{
				setNotice("文件序号不得大于32", true);
				sceneNo = 32;
			}

			setSceneNo(true);
		}

		/// <summary>
		/// 事件：《场景选择框》更改选项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (frameComboBox.SelectedIndex != 0)
			{
				sceneNo = frameComboBox.SelectedIndex;
				setSceneNo(true);
			}
		}

		/// <summary>
		/// 事件：点击《+》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void plusButton_Click(object sender, EventArgs e)
		{
			if (sceneNo >= 32)
			{
				setNotice("文件序号不得大于32。", true);
				return;
			}
			sceneNo++;
			setSceneNo(true);
		}

		/// <summary>
		/// 事件：点击《-》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void minusButton_Click(object sender, EventArgs e)
		{
			if (sceneNo <= 1)
			{
				setNotice("文件序号不得小于1。", true);
				return;
			}
			sceneNo--;
			setSceneNo(true);
		}

		/// <summary>
		/// 辅助方法：根据当前的savePath，设置label及toolTip
		/// </summary>
		private void setSceneNo(bool isNotice)
		{
			frameComboBox.SelectedIndexChanged -= frameComboBox_SelectedIndexChanged;
			frameComboBox.SelectedIndex = sceneNo;
			frameComboBox.SelectedIndexChanged += frameComboBox_SelectedIndexChanged;

			sceneNoTextBox.Text = sceneNo.ToString();
			if (isNotice)
			{
				setNotice("已设置文件名为M" + sceneNo + ".bin", false);
			}
		}

		#endregion

		#region 保存或加载配置

		/// <summary>
		/// 事件：点击《打开音频文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loadButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string fileName = openFileDialog.FileName;

					musicSceneConfig = MusicSceneConfig.ReadFromFile(fileName);
					stepTimeNumericUpDown.Value = eachStepTime * musicSceneConfig.StepTime;
					jgtNumericUpDown.Value = musicSceneConfig.StepWaitTIme;
					mLKTextBox.Text = musicSceneConfig.MusicStepList;
					tdSet = musicSceneConfig.MusicChannelNoList;

					refreshPage();

					//想办法通过文件名，来更改sceneNo的值
					int tempSceneNo = int.Parse(System.Text.RegularExpressions.Regex.Replace(fileName, @"[^0-9]+", ""));
					if (tempSceneNo > 0 && tempSceneNo <= 32)
					{
						sceneNo = tempSceneNo;
						setSceneNo(false);
					}
					setNotice("已成功打开文件：" + fileName, true);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("打开文件异常:\n" + ex.Message);
			}
		}

		/// <summary>
		/// 事件：点击《保存音频文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			if (tdSet == null || tdSet.Count == 0)
			{
				setNotice("请选择至少一个通道！", true);
				return;
			}

			musicSceneConfig = new MusicSceneConfig
			{
				StepTime = decimal.ToInt32(stepTimeNumericUpDown.Value / eachStepTime),
				StepWaitTIme = decimal.ToInt32(jgtNumericUpDown.Value),
				MusicStepList = mLKTextBox.Text.Trim(),
				MusicChannelNoList = tdSet
			};

			if (musicSceneConfig.WriteToFile(savePath, "M" + sceneNo + ".bin"))
			{
				setNotice("成功保存配置文件,路径为：" + savePath + @"\M" + sceneNo + ".bin", true);
			}
			else
			{
				setNotice("保存配置文件失败", true);
			}
		}

		/// <summary>
		/// 事件：点击《保存全局配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveConfigButton_Click(object sender, EventArgs e)
		{
			GlobalConfig gc = new GlobalConfig(frameComboBox.SelectedIndex);
			if (gc.WriteToFile(savePath, "Config.bin"))
			{
				setNotice("成功生成全局配置文件", true);
			}
			else
			{
				setNotice("生成全局配置文件失败", true);
			}
		}

		#endregion

		#region 通用方法(这些方法往往只需稍微修改或完全不动，就可以在不同的界面中通用)

		/// <summary>
		/// 辅助方法：设置提醒
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="msgBoxShow"></param>
		private void setNotice(string msg, bool msgBoxShow)
		{
			myStatusLabel.Text = msg;
			if (msgBoxShow)
			{
				MessageBox.Show(msg);
			}
		}

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
		/// 事件：键盘按键点击事件:确保textBox内只能是0-9、及回退键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mLKTextBox_KeyPress(object sender, KeyPressEventArgs e)
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
		
	}
}
