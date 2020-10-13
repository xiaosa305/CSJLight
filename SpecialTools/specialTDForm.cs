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
	public partial class specialTDForm : Form
	{
		private int[] tdArray;

		public specialTDForm()
		{
			InitializeComponent();
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
			if (tdStrArray.Length != 10) {
				setNotice("通道数量非十个,请重新填写！",true);
				return;
			}
			try
			{
				tdArray = new int[10];
				for (int tdIndex = 0; tdIndex < 10; tdIndex++)
				{
					tdArray[tdIndex] = int.Parse(tdStrArray[tdIndex]);
				}				
			}
			catch (Exception) {
				setNotice("请填入正确的通道数，不得填入非数字!",true);
				return;
			}
			generateFLP();
		}

		/// <summary>
		/// 根据通道列表，生成相应的面板；
		/// </summary>
		private void generateFLP()
		{
			LightConfigBean lcb = new LightConfigBean();
			lcb.SceneConfigs = new SceneConfigBean[32];
			for (int scIndex= 0;   scIndex<lcb.SceneConfigs.Length; scIndex++)
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

			LightConfigBean.WriteToFile(  @"C:\lightConfig.bin" , lcb);

			//tdFLP.Controls.Clear();//先清空
			//for (int tdIndex = 0; tdIndex < 10; tdIndex++)
			//{
			//	Panel tdPanel = new Panel
			//	{
			//		Name = "tdPanel" +(tdIndex+1),
			//		Size = tdPanelDemo.Size,
			//		BorderStyle = tdPanelDemo.BorderStyle,
			//		Visible = true
			//	};

			//	Label tdLabel = new Label
			//	{
			//		Name = "tdLabel" + (tdIndex + 1),
			//		Text = "通道" + tdArray[tdIndex] ,
			//		AutoSize = tdLabelDemo.AutoSize,
			//		Location = tdLabelDemo.Location,					
			//		Size = tdLabelDemo.Size,
			//	};

			//	CheckBox tdCB = new CheckBox
			//	{
			//		Name = "tdCB" + (tdIndex + 1),
			//		Text = tdCBDemo.Text ,
			//		AutoSize = tdCBDemo.AutoSize,
			//		Location = tdCBDemo.Location,
			//		Size = tdCBDemo.Size,					
			//		UseVisualStyleBackColor = tdCBDemo.UseVisualStyleBackColor,
			//	};			

			//	NumericUpDown tdNUD = new NumericUpDown
			//	{
			//		Name = "tdNUD" + (tdIndex + 1),
			//		Location = tdNUDDemo.Location,
			//		Size = tdNUDDemo.Size,
			//		TextAlign = tdNUDDemo.TextAlign,
			//	};

			//	tdPanel.Controls.Add( tdLabel );
			//	tdPanel.Controls.Add( tdCB );
			//	tdPanel.Controls.Add( tdNUD );

			//	tdFLP.Controls.Add(tdPanel);
			//}
		}

		private void saveButton_Click(object sender, EventArgs e)
		{

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

	}
}
