using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RecordTools.Entity;
using Dickov.Utils;

namespace RecordTools
{
	public partial class RecordSetForm : Form
	{
		private IniFileHelper iniHelper;
		private decimal eachStepTime = .04m;
		private string savePath;
		private int sceneNo = 1;	

		// 以下变量，为《分页显示》功能必须的变量 
		private int currentPage = 1;
		private int pageCount;
		private int eachCount = 100;    // 如果此项大于tdCount,则应设为tdCount的值
		private int tdCount = 512;  //注意：此项不得为0，否则分页毫无意义
		
		private HashSet<int> tdSet; // 记录使用的通道

		private MusicSceneConfig musicSceneConfig;

		public RecordSetForm()
		{
			InitializeComponent();

			// 读取各个默认配置
			iniHelper = new IniFileHelper(Application.StartupPath + @"\CommonSet.ini");
			eachStepTime = iniHelper.ReadInt("CommonSet", "EachStepTime", 40) / 1000m;
			int stepTime = iniHelper.ReadInt("CommonSet", "StepTime", 10);
			stepTimeNumericUpDown.Value = eachStepTime * stepTime;
			//添加frameStepTimeNumericUpDown相关初始化及监听事件			
			stepTimeNumericUpDown.Increment = eachStepTime;
			stepTimeNumericUpDown.Maximum = 250*eachStepTime;
			stepTimeNumericUpDown.MouseWheel += someNUD_MouseWheel;

			jgtNumericUpDown.Value = iniHelper.ReadInt("CommonSet", "JG", 2000);
			jgtNumericUpDown.MouseWheel += someNUD_MouseWheel;

			savePath = iniHelper.ReadString("CommonSet", "SavePath", @"C:\Temp\CSJ");
			saveFolderBrowserDialog.SelectedPath = savePath;
			setSavePathLabel();
			sceneNo = iniHelper.ReadInt("CommonSet", "SceneNo", 1);
			if (sceneNo < 1 || sceneNo > 32) {
				sceneNo = 1;
			}
			setSceneNo(false);

			// 初始化各个组件
			tdSet = new HashSet<int>();

			eachCount = eachCount > tdCount ? tdCount : eachCount;
			pageCount = MathHelper.GetDivisionCelling(tdCount, eachCount);

			for (int cbIndex = 0; cbIndex < eachCount; cbIndex++)
			{
				int tdIndex = (currentPage - 1) * eachCount + cbIndex;
				CheckBox cb = new CheckBox
				{
					Location = checkBoxDemo.Location,
					Size = checkBoxDemo.Size,
					UseVisualStyleBackColor = checkBoxDemo.UseVisualStyleBackColor,
					Visible = true,
				};
				bigFLP.Controls.Add(cb);
			}

			refreshPage();
		}

		private void RecordSetForm_Load(object sender, EventArgs e) { }

		// 双缓冲解决刷新页面时，慢慢减少部分控件的情况
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x02000000; // 用双缓冲绘制窗口的所有子控件
				return cp;
			}
		}

		/// <summary>
		/// 辅助方法：刷新页面（根据当前的页面，把相应的checkBox的CheckBox设为正确的值）
		/// </summary>
		private void refreshPage()
		{
			// 显示页数
			pageLabel.Text = currentPage + "/" + pageCount;

			// 遍历显示(或隐藏)通道,并改名
			for (int cbIndex = 0; cbIndex < eachCount; cbIndex++)
			{
				CheckBox cb = bigFLP.Controls[cbIndex] as CheckBox;
				if (currentPage < pageCount || tdCount % eachCount == 0 || cbIndex < tdCount % eachCount)
				{
					int tdIndex = (currentPage - 1) * eachCount + cbIndex;
					cb.Text = "通道" + (tdIndex + 1);
					cb.Name = "checkBox" + (tdIndex + 1);
					cb.CheckedChanged -= tdCheckBox_CheckedChanged;
					cb.Checked = tdSet.Contains(tdIndex + 1);
					cb.CheckedChanged += tdCheckBox_CheckedChanged;
					cb.Show();
				}
				else
				{
					cb.Hide();
				}
			}
		}

		/// <summary>
		/// 事件：当通道的CheckBox发生Checked变动时，更改相应的tdArray值；
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			//Console.WriteLine("tdCheckBox_CheckedChanged");

			CheckBox cb = sender as CheckBox;
			int tdIndex = MathHelper.GetIndexNum(cb.Name, 0); // 通道名无需-1，应该所见即所得

			if (cb.Checked)
			{
				tdSet.Add(tdIndex);
			}
			else
			{
				tdSet.Remove(tdIndex);
			}
		}

		#region 保存或加载配置

		/// <summary>
		/// 事件：点击《打开配置文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loadButton_Click(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				musicSceneConfig = MusicSceneConfig.ReadFromFile( openFileDialog.FileName );

				stepTimeNumericUpDown.Value = eachStepTime * musicSceneConfig.StepTime ;
				jgtNumericUpDown.Value = musicSceneConfig.StepWaitTIme ;				
				tdSet = musicSceneConfig.MusicChannelNoList;
				string mLKStr = "";
				foreach (int strTemp in musicSceneConfig.MusicStepList)
				{
					mLKStr += strTemp + " ";
				}
				mLKTextBox.Text = mLKStr.Trim();

				refreshPage();
			}
		}

		/// <summary>
		/// 事件：点击《保存配置文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			if (tdSet == null || tdSet.Count == 0) {
				setNotice("请选择至少一个通道！" ,  true);
				return;
			}

			musicSceneConfig = new MusicSceneConfig
			{
				StepTime = decimal.ToInt32(stepTimeNumericUpDown.Value * 1000 / eachStepTime),
				StepWaitTIme = decimal.ToInt32(jgtNumericUpDown.Value),
				MusicStepList = makeLinkList(),
				MusicChannelNoList = tdSet
			};			

			if ( musicSceneConfig.WriteToFile(savePath, "M" + sceneNo + ".bin") ) {
				setNotice("保存配置文件成功", true);
			}
			else
			{
				setNotice("保存配置文件失败", true);
			}

		}

		#endregion

		#region 通用方法(这些方法往往只需稍微修改或完全不动，就可以在不同的界面中通用)

		/// <summary>
		/// 事件：点击《上|下一页》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pageButton_Click(object sender, EventArgs e)
		{
			if ((sender as Button).Name == "previousButton")
			{
				if (currentPage > 1)
				{
					currentPage--;
					refreshPage();
				}
			}
			else
			{
				if (currentPage < pageCount)
				{
					currentPage++;
					refreshPage();
				}
			}
		}

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
					nud.Value = decimal.ToInt32(dd);
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = nud.Value - nud.Increment;
				if (dd >= nud.Minimum)
				{
					nud.Value = decimal.ToInt32(dd);
				}
			}
		}

		#endregion

		private void stepTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int stepTime = Decimal.ToInt32(stepTimeNumericUpDown.Value / eachStepTime);
			stepTimeNumericUpDown.Value = stepTime * eachStepTime;
		}

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
			if (savePath != null) {
				recordPathLabel.Text = savePath;
				myToolTip.SetToolTip(recordPathLabel, savePath);
			}
		}

		/// <summary>
		/// 辅助方法：根据当前的savePath，设置label及toolTip
		/// </summary>
		private void setSceneNo(bool isNotice)
		{
			sceneNoTextBox.Text = sceneNo.ToString() ;
			if (isNotice) {
				setNotice("已设置文件名为M"+sceneNo+".bin", false);
			}
		}

		/// <summary>
		/// 辅助方法：封装音频链表
		/// </summary>
		private List<int> makeLinkList() {

			List<int> linkList = new List<int>();

			string linkStr = mLKTextBox.Text.Trim();
			if (linkStr == "")
			{
				linkList.Add(0);
			}
			else {
				string[] strArray = linkStr.Split(' ');
				try
				{
					foreach (string tempStr in strArray)
					{
						linkList.Add(int.Parse(tempStr));
					}
				}
				catch (Exception ex) {
					MessageBox.Show("音频链表输入有误,请重新输入！\n"+ex.Message);
					linkList.Add(0);
				}				
			}
			return linkList;
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
				setNotice( "文件序号不得大于32。", true);
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
				setNotice( "文件序号不得小于1。", true);
				return;
			}
			sceneNo--;
			setSceneNo(true);
		}

	}
}
