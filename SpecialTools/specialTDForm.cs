using LightController.Common;
using LightController.Utils.LightConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SpecialTools
{
	public partial class SpecialTDForm : Form
	{
		private IList<string> frameList;
		private int[] tdArray;
		private LightConfigBean lcb;
		private int frameCount = 32;
		private int frameIndex = -1;
		private int tdCount = 10;

		private Panel[] tdPanels;
		private Label[] tdLabels;
		private CheckBox[] tdCBs;
		private NumericUpDown[] tdNUDs;

		public SpecialTDForm()
		{
			InitializeComponent();

			string loadexeName = Application.ExecutablePath;
			FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(loadexeName);
			string appFileVersion = string.Format("{0}.{1}.{2}.{3}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);
			Text += " v" + appFileVersion;

			//处理几个NUD的鼠标滚动事件
			unifyNUD.MouseWheel += someNUD_MouseWheel;
			stepIncNUD.MouseWheel += someNUD_MouseWheel ;
					   
			// 初始化场景选择
			frameList = TextHelper.Read(Application.StartupPath + @"\FrameList.txt");
			foreach (string frame in frameList)
			{
				frameComboBox.Items.Add(frame);
			}
			frameComboBox.SelectedIndexChanged -= frameComboBox_SelectedIndexChanged;
			frameComboBox.SelectedIndex = 0;
			frameIndex = 0;
			frameComboBox.SelectedIndexChanged += frameComboBox_SelectedIndexChanged;

			// 渲染FLP
			tdPanels = new Panel[tdCount];
			tdLabels = new Label[tdCount];
			tdCBs = new CheckBox[tdCount];
			tdNUDs = new NumericUpDown[tdCount];

			for (int tdIndex = 0; tdIndex < tdCount; tdIndex++)
			{
				tdPanels[tdIndex] = new Panel
				{
					Name = "tdPanel" + (tdIndex + 1),
					Size = tdPanelDemo.Size,
					BorderStyle = tdPanelDemo.BorderStyle,
					Visible = false
				};

				tdLabels[tdIndex]= new Label
				{
					Name = "tdLabel" + (tdIndex + 1),
					AutoSize = tdLabelDemo.AutoSize,
					Location = tdLabelDemo.Location,
					Size = tdLabelDemo.Size,
				};

				tdCBs[tdIndex] = new CheckBox
				{
					Name = "tdCB" + (tdIndex + 1),
					Text = tdCBDemo.Text,
					AutoSize = tdCBDemo.AutoSize,
					Location = tdCBDemo.Location,
					Size = tdCBDemo.Size,
					UseVisualStyleBackColor = tdCBDemo.UseVisualStyleBackColor,
				};

				tdNUDs[tdIndex] = new NumericUpDown
				{
					Name = "tdNUD" + (tdIndex + 1),
					Location = tdNUDDemo.Location,
					Size = tdNUDDemo.Size,
					TextAlign = tdNUDDemo.TextAlign,
					Maximum = tdNUDDemo.Maximum,
				};
				tdNUDs[tdIndex].MouseWheel += someNUD_MouseWheel;

				tdPanels[tdIndex].Controls.Add(tdLabels[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdCBs[tdIndex]);
				tdPanels[tdIndex].Controls.Add(tdNUDs[tdIndex]);

				tdFLP.Controls.Add(tdPanels[tdIndex]);
			}
		}		

		/// <summary>
		/// 事件：加载界面时执行代码
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void specialTDForm_Load(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// 事件：更改场景
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frameComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			frameIndex = frameComboBox.SelectedIndex;
			refreshFLP();
		}

		/// <summary>
		/// 事件：点击《生成》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void generateButton_Click(object sender, EventArgs e)
		{
			string tdStr = tdTextBox.Text.Trim();
			string[] tdStrArray = tdStr.Split(' ');	
			if (tdStrArray.Length != tdCount) {
				setNotice("通道数量非"+ tdCount + "个,请重新填写！",true);
				return;
			}
			try
			{
				tdArray = new int[tdCount];
				for (int tdIndex = 0; tdIndex < tdCount; tdIndex++)
				{
					tdArray[tdIndex] = int.Parse(tdStrArray[tdIndex]);
				}				
			}
			catch (Exception) {
				setNotice("请填入正确的通道数，不得填入非数字!",true);
				return;
			}

			generateEmptyLCB(); 
			refreshFLP();

			unifyPanel.Show();
		}

		/// <summary>
		///  辅助方法：生成lcb对象
		/// </summary>
		private void generateEmptyLCB() {

			lcb = new LightConfigBean();
			
			lcb.SceneConfigs = new SceneConfigBean[32];
			for (int scIndex = 0; scIndex < lcb.SceneConfigs.Length; scIndex++)
			{
				lcb.SceneConfigs[scIndex] = new SceneConfigBean();
				lcb.SceneConfigs[scIndex].ChannelConfigs = new ChannelConfigBean[10];
				for (int ccIndex = 0; ccIndex < 10; ccIndex++)
				{
					lcb.SceneConfigs[scIndex].ChannelConfigs[ccIndex] = new ChannelConfigBean()
					{
						IsOpen = true,
						ChannelNo = tdArray[ccIndex],
						DefaultValue = 0,
					};
				}
			}
			
			exportButton.Enabled = lcb != null;			
		}
			   
		/// <summary>
		/// 根据通道列表，生成相应的面板；
		/// </summary>
		private void refreshFLP()
		{
			if (lcb == null) {
				setNotice("尚未生成lcb，无法渲染FLP", true);
				return;
			}
			
			for (int tdIndex = 0; tdIndex < tdCount; tdIndex++)
			{
				tdPanels[tdIndex].Visible = true ;
				tdLabels[tdIndex].Text = "通道" + lcb.SceneConfigs[frameIndex].ChannelConfigs[tdIndex].ChannelNo;

				tdCBs[tdIndex].CheckedChanged -= tdCB_CheckedChanged;
				tdCBs[tdIndex].Checked = lcb.SceneConfigs[frameIndex].ChannelConfigs[tdIndex].IsOpen;
				tdCBs[tdIndex].CheckedChanged += tdCB_CheckedChanged;

				tdNUDs[tdIndex].ValueChanged -= tdNUD_ValueChanged;
				tdNUDs[tdIndex].Value = lcb.SceneConfigs[frameIndex].ChannelConfigs[tdIndex].DefaultValue;
				tdNUDs[tdIndex].ValueChanged += tdNUD_ValueChanged;
			}
		}

		/// <summary>
		/// 事件：点击《导出文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exportButton_Click(object sender, EventArgs e)
		{
			if (lcb == null)
			{
				setNotice("尚未生成lcb对象，无法导出文件。", true);
				return;
			}

			DialogResult dr = exportFolderBrowserDialog.ShowDialog();
			if (dr == DialogResult.Cancel)
			{
				return;
			}
			string exportPath = exportFolderBrowserDialog.SelectedPath;

			//只在确认导出时，才填入StepInc;
			lcb.StepInc = decimal.ToInt32(stepIncNUD.Value);
			LightConfigBean.WriteToFile(exportPath, lcb);
			setNotice("已成功导出文件(" + exportPath + "LightConfig.bin)。", false);
			dr = MessageBox.Show("已成功导出文件，是否打开导出目录？",
				"打开导出目录？",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Question);
			if (dr == DialogResult.OK)
			{
				Process.Start(exportPath);
			}

		}

		/// <summary>
		/// 辅助方法：设置提醒
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="mbShow"></param>
		private void setNotice(string msg, bool mbShow) {
			Thread.Sleep(1000) ;
			myStatusLabel.Text = msg;
			if (mbShow) {
				MessageBox.Show(msg);
			}
		}

		/// <summary>
		/// 事件：通道是否启用变更后，更改lcb内的相应值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdCB_CheckedChanged(object sender, EventArgs e)
		{
			if (lcb != null)
			{
				CheckBox cb = sender as CheckBox;
				int tdIndex = MathHelper.GetIndexNum(cb.Name, -1);
				int frameIndex = frameComboBox.SelectedIndex;

				lcb.SceneConfigs[frameIndex].ChannelConfigs[tdIndex].IsOpen = cb.Checked;
			}
		}

		/// <summary>
		/// 事件：通道默认值更改后，更改lcb内相应值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdNUD_ValueChanged(object sender, EventArgs e)
		{
			if (lcb != null) {
				NumericUpDown nud = sender as NumericUpDown;
				int tdIndex = MathHelper.GetIndexNum(nud.Name, -1);
				int frameIndex = frameComboBox.SelectedIndex;

				lcb.SceneConfigs[frameIndex].ChannelConfigs[tdIndex].DefaultValue = decimal.ToInt32(nud.Value);
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
		/// 事件：勾选或去除勾选 全部启用
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (lcb != null) {				
				for (int tdIndex = 0; tdIndex < tdCount; tdIndex++) {
					tdCBs[tdIndex].Checked = allCheckBox.Checked;
				}
			}
		}

		/// <summary>
		/// 事件：点击《统一值》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unifyButton_Click(object sender, EventArgs e)
		{
			if (lcb != null)
			{				
				for (int tdIndex = 0; tdIndex < tdCount; tdIndex++)
				{
					tdNUDs[tdIndex].Value = unifyNUD.Value;
				}
			}
		}
	}
}
