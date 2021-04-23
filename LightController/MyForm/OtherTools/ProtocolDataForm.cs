using LightController.Common;
using LightController.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.OtherTools
{
	public partial class ProtocolDataForm : Form
	{
		private MainFormBase mainForm; //传mainForm便于开启解码
		private ToolsForm toolsForm;
		private int ccdIndex; 
		private CCData ccd;
		private bool isDecoding = false; //默认情况下，解码是关闭的状态
		private TextBox selectedTextBox;  //选中的红外码值文本框

		public ProtocolDataForm(MainFormBase mainForm,ToolsForm toolsForm, int ccdIndex,CCData ccd)
		{
			this.mainForm = mainForm;
			this.toolsForm = toolsForm;
			this.ccdIndex = ccdIndex;
			this.ccd = ccd;

			InitializeComponent();

			// 以CCData渲染各个参数；
			functionTextBox.Text = ccd.Function;
			codeTextBox.Text = ccd.Code;
			com0UpTextBox.Text = ccd.Com0Up;
			com0DownTextBox.Text = ccd.Com0Down;
			com1UpTextBox.Text = ccd.Com1Up;
			com1DownTextBox.Text = ccd.Com1Down;
			infraredSendTextBox.Text = ccd.InfraredSend;
			infraredReceiveTextBox.Text = ccd.InfraredReceive;
			ps2UpTextBox.Text = ccd.PS2Up;
			ps2DownTextBox.Text = ccd.PS2Down;
		}

		private void ProtocolDataForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			//主动开启解码
			//ccDecodeButton_Click(null, null);
		}

		private void ProtocolDataForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// 如果正在解码中，需先关闭解码
			if (isDecoding)
			{
				ccDecodeButton_Click(null, null);
			}
		}

		/// <summary>
		/// 事件：点击《(右上角)?》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProtocolDataForm_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			e.Cancel = true;			
			MessageBox.Show("1.在《开启红外解码》的状态下：①点击《红外发送》或《红外接收》文本框；②把红外遥控对准设备按按钮，该按钮的码值将自动写入到①点击的文本框中；\n" +
				"2.若间隔超过一分钟没有点击遥控按钮，则设备会退出解码模式(此时按遥控无法记录码值)；只需点击《关闭红外解码》，再重新《开启红外解码》即可；\n " +
				"3.修改的码值不会自动保存，修改的内容只在本次软件运行期间(且未切换协议)生效，如需设为长期协议，可在《外设配置》界面内点击《协议另存为》进行保存。",	
				"使用提示？"
				,MessageBoxButtons.OK,
				MessageBoxIcon.Information);
		}

		/// <summary>
		/// 事件：点击《开启解码|关闭解码》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ccDecodeButton_Click(object sender, EventArgs e)
		{
			mainForm.SleepBetweenSend(1);
			// 点击《关闭解码》
			if (isDecoding)
			{				
				mainForm.MyConnect.CenterControlStopCopy(CCStopCompleted, CCStopError);
			}
			// 点击《开启解码》
			else
			{
				mainForm.MyConnect.CenterControlStartCopy(CCStartCompleted, CCStartError, CCListen);
			}
		}

		/// <summary>
		/// 辅助回调方法：启动《中控-调试解码》成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCStartCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice( "红外解码开启成功。", false, true);
				isDecoding = true;
				ccDecodeButton.Text = "关闭红外解码";					
			});
		}

		/// <summary>
		/// 辅助回调方法：启动《中控-调试解码》失败
		/// </summary>
		public void CCStartError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(LanguageHelper.TranslateSentence("红外解码开启失败 : ") + msg, true, false);
			});
		}

		/// <summary>
		/// 辅助回调方法：成功回读《中控-点击的红外码值》
		/// </summary>
		/// <param name="obj"></param>
		public void CCListen(Object obj)
		{
			Invoke((EventHandler)delegate {
				List<byte> byteList = obj as List<byte>;
				if (byteList != null && byteList.Count != 0)
				{
					string strTemp = "";
					foreach (byte item in byteList)
					{
						strTemp += StringHelper.DecimalStringToBitHex(item.ToString(), 2) + " ";
					}

					//DOTO 3.0420 按红外遥控时，需要根据当前选中的TextBox填入码值；
					if ( selectedTextBox != null  ) {
						selectedTextBox.Text = strTemp;
					}
				}
				setNotice("红外解码成功"+(selectedTextBox == null?"。" : "，并填入最后点击的红外文本框中。"), false, true);
			});
		}

		/// <summary>
		/// 事件：两个红外（上下行）文本框若被鼠标点击，则表示这个TextBox为选中项；在开启解码状态下，点遥控按键时，把码值填进去；
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void infraredSendTextBox_MouseClick(object sender, MouseEventArgs e)
		{
			selectedTextBox = sender as TextBox;
		}

		/// <summary>
		///  辅助回调方法：结束《中控-调试解码》成功
		/// </summary>
		/// <param name="obj"></param>
		public void CCStopCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice("成功关闭红外解码", false, true);				
				isDecoding = false;
				ccDecodeButton.Text = "开启红外解码";
			});
		}

		/// <summary>
		/// 辅助回调方法：结束《中控-调试解码》失败
		/// </summary>
		public void CCStopError(string msg)
		{
			Invoke((EventHandler)delegate
			{
				setNotice(LanguageHelper.TranslateSentence("关闭红外解码失败:") + msg, true, false);
			});
		}


		/// <summary>
		///事件：点击《修改码值》 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>		
		private void editCodeButton_Click(object sender, EventArgs e)
		{
			// 如果正在解码中，需先关闭解码
			if (isDecoding) {
				ccDecodeButton_Click(null, null);
			}

			ccd.Com0Up = com0UpTextBox.Text.Trim();
			ccd.Com0Down = com0DownTextBox.Text .Trim();
			ccd.Com1Up = com1UpTextBox.Text .Trim();
			ccd.Com1Down = com1DownTextBox.Text.Trim();
			ccd.InfraredSend = infraredSendTextBox.Text.Trim();
			ccd.InfraredReceive = infraredReceiveTextBox.Text.Trim();
			ccd.PS2Down = ps2DownTextBox.Text.Trim();
			ccd.PS2Up = ps2UpTextBox.Text.Trim();
			toolsForm.EditProtocol(ccdIndex, ccd);
			Dispose();
		}

		#region 通用方法

		/// <summary>
		/// 辅助方法：显示信息
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="messageBoxShow">是否在提示盒内提示</param>
		private void setNotice(string msg, bool messageBoxShow, bool isTranslate)
		{
			if (isTranslate)
			{
				msg = LanguageHelper.TranslateSentence(msg);
			}

			if (messageBoxShow)
			{
				MessageBox.Show(msg);
			}
			myStatusLabel.Text = msg;
			statusStrip1.Refresh();
		}

		#endregion

		
	}
}
