﻿using LightController.Common;
using LightController.Utils.LightConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpecialTools
{
	public partial class SpecialTDForm : Form
	{
		private IList<string> frameList;
		private int[] tdArray;
		private LightConfigBean lcb;
		private int frameCount = 32;
		private int tdCount = 10;

		private Panel[] tdPanels;
		private Label[] tdLabels;
		private CheckBox[] tdCBs;
		private NumericUpDown[] tdNUDs;

		public SpecialTDForm()
		{
			InitializeComponent();
					   
			// 初始化场景选择
			frameList = TextHelper.Read(Application.StartupPath + @"\FrameList.txt");
			foreach (string frame in frameList)
			{
				frameComboBox.Items.Add(frame);
			}
			frameComboBox.SelectedIndexChanged -= frameComboBox_SelectedIndexChanged;
			frameComboBox.SelectedIndex = 0;
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
			generateFLP();
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
			generateFLP();
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
		}
			   
		/// <summary>
		/// 根据通道列表，生成相应的面板；
		/// </summary>
		private void generateFLP()
		{
			if (lcb == null) {
				setNotice("尚未生成lcb，无法渲染FLP", true);
				return;
			}

			int frame = frameComboBox.SelectedIndex;
			for (int tdIndex = 0; tdIndex < tdCount; tdIndex++)
			{
				tdPanels[tdIndex].Visible = true ;
				tdLabels[tdIndex].Text = "通道" + lcb.SceneConfigs[frame].ChannelConfigs[tdIndex].ChannelNo;

				tdCBs[tdIndex].CheckedChanged -= tdCB_CheckedChanged;
				tdCBs[tdIndex].Checked = lcb.SceneConfigs[frame].ChannelConfigs[tdIndex].IsOpen;
				tdCBs[tdIndex].CheckedChanged += tdCB_CheckedChanged;

				tdNUDs[tdIndex].ValueChanged -= tdNUD_ValueChanged;
				tdNUDs[tdIndex].Value = lcb.SceneConfigs[frame].ChannelConfigs[tdIndex].DefaultValue;
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
			if (lcb == null) {
				setNotice( "尚未生成lcb对象，无法导出文件。" ,  true);
				return;
			}
			LightConfigBean.WriteToFile(@"C:\lightConfig.bin", lcb);
		}

		/// <summary>
		/// 辅助方法：设置提醒
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="mbShow"></param>
		private void setNotice(string msg, bool mbShow) {
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

				//lcb.SceneConfigs[frameIndex].ChannelConfigs[tdIndex]
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


	}
}
