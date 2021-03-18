using LBDConfigTool.Common;
using LBDConfigTool.utils.communication;
using LBDConfigTool.utils.conf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace LBDConfigTool
{
	public partial class ConfForm : Form
	{

		private CSJConf cc;
		CSJNetCommunitor cnc;


		public ConfForm()
		{
			InitializeComponent();			

			string loadexeName = Application.ExecutablePath;
			FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(loadexeName);
			string appFileVersion = string.Format("{0}.{1}.{2}.{3}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);
			Text += " v" + appFileVersion;

			cnc = CSJNetCommunitor.GetInstance();
			cnc.Start(); //启动服务
			cnc.SearchDevice(readCompleted, readError);// 搜设备	

			//添加各类监听器
			dimmerNUD.MouseWheel += someNUD_MouseWheel;
			rNUD.MouseWheel += someNUD_MouseWheel;
			gNUD.MouseWheel += someNUD_MouseWheel;
			bNUD.MouseWheel += someNUD_MouseWheel;
			wNUD.MouseWheel += someNUD_MouseWheel;

			sceneNUD.MouseWheel += someNUD_MouseWheel;
			stepTimeNUD.MouseWheel += someNUD_MouseWheel;
		}			
		
		
		#region 通用方法

		/// <summary>
		/// 辅助方法：按要求显示提示
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="isMsbShow"></param>
		private void setNotice(string msg, bool isMsbShow) {

			toolStripStatusLabel1.Text = msg;
			if (isMsbShow) MessageBox.Show(msg);
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
							
		/// <summary>
		/// 事件:点击《测试》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void testButton_Click(object sender, EventArgs e)
		{
			//User u1 = new User("Dickov", 31, "A93");
			//SerializeMethod(u1);
			//setNotice("序列化成功",true);

			User uRead = (User)SerializeUtils.ReserializeMethod();
			uRead.Say();
			setNotice("反序列化成功", true);
		}


		/// <summary>
		/// 事件：点击《回读参数》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void readButton_Click(object sender, EventArgs e)
		{
			cnc.SearchDevice(readCompleted, readError);// 回读参数（并不连接，直接发指令并收数据）					   
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="msg"></param>
		private void readCompleted(object obj, string msg)
		{
			cc = obj as CSJConf;
			renderAllControls();
			setNotice(msg, false);
		}

		private void readError(string msg)
		{
			cc = null;
			renderAllControls();
			setNotice(msg, true);
		}

		/// <summary>
		/// 事件：点击《下载参数》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void writeButton_Click(object sender, EventArgs e)
		{
			makeCC();

			if (cc != null) {
				SerializeUtils.SetBinPath(@"C:\Temp\" + cc.LedName + ".bin");
				SerializeUtils.SerializeObject(cc);
			}
			
			//cnc.WriteParam(cc, writeCompleted, writeError);
		}

		/// <summary>
		/// 辅助方法：根据界面的内容，封装CSJConf对象
		/// </summary>
		private void makeCC()
		{
			cc = new CSJConf();

			try
			{
				cc.MIA_HAO = pswTB.Text;  // 密码限定为6位，不能多不能少
				if (cc.MIA_HAO.Length != 6) {
					setNotice("密码必须为6位，请修改后重试",true);
					cc = null;
					return ;
				}
				cc.Addr = int.Parse(addrTB.Text);
				cc.Baud = baudCB.SelectedIndex;
				cc.IsSetBad = badCheckBox.Checked;
				cc.DiskFlag = int.Parse(diskFlagTB.Text);
				cc.Play_Mod = playModeCB.SelectedIndex;
				cc.PlayScene = decimal.ToInt32(sceneNUD.Value);
				cc.LedName =  ledNameTB.Text.Trim() ; // 上限为16,无下限
				cc.Ver = verTB.Text.Trim(); //上限为16，,无下限
				cc.Max_scan_dot = int.Parse(maxDotTB.Text);
				cc.CardType = cardRB1.Checked ? 0 : 2;
				cc.Led_out_type = outTypeCB.SelectedIndex;
				cc.Led_fx = int.Parse(fxTB.Text);
				cc.RGB_Type = rgbCB.SelectedIndex;
				cc.IC_Type = int.Parse(icTB.Text);
				cc.Play_hz =decimal.ToInt32( stepTimeNUD.Value);
				cc.Clk_shzhong = int.Parse( clockTB.Text );
				cc.Led_gam = int.Parse(ledGamTB.Text);
				cc.Led_ld = decimal.ToInt32(dimmerNUD.Value);
				cc.R_LD = decimal.ToInt32(rNUD.Value);
				cc.G_LD = decimal.ToInt32(gNUD.Value);
				cc.B_LD = decimal.ToInt32(bNUD.Value);
				cc.W_LD = decimal.ToInt32(wNUD.Value);

				// ARTNET参数
				cc.Mac = aMacTB.Text;
				cc.Ip = aIpTB.Text;
				cc.Fk_lushu = int.Parse(aFKLSTB.Text);
				cc.Jl_fk_num = int.Parse(aJLFKSTB.Text);
				cc.Art_Net_Start_Space = int.Parse(aStartTB.Text);
				cc.Art_Net_Pre = int.Parse(aPerTB.Text);
				cc.Art_Net_td_len = int.Parse(aTdLenTB.Text);
				cc.Art_Net_fk_id = int.Parse(aFKHTB.Text);
				cc.SumUseTimes = int.Parse(aSumTB.Text);
				cc.CurrUseTimes = int.Parse(aCurrTB.Text);

			}
			catch (Exception ex) {
				cc = null;
				setNotice("参数格式有误" + ex.Message, true);				
			}

		}

		private void writeCompleted(object obj, string msg)
		{
			throw new NotImplementedException();
		}

		private void writeError(string msg)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 辅助方法：使能所有的控件，并填入cc的内容；
		/// </summary>
		/// <param name="v"></param>
		private void renderAllControls()
		{
			if (cc == null) {
				cc = readFromLocal();
			}

			// 根据cc,渲染各个控件
			



		}

		/// <summary>
		/// 辅助方法： 从本地回读配置
		/// </summary>
		/// <returns></returns>
		private CSJConf readFromLocal()
		{
			throw new NotImplementedException();
		}

		

	}
}
