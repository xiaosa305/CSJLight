﻿using LBDConfigTool.Common;
using LBDConfigTool.utils.communication;
using LBDConfigTool.utils.conf;
using LBDConfigTool.utils.test;
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
		private CSJNetCommunitor cnc;
		private bool isSuccessShow = true;

		public ConfForm()
		{
			InitializeComponent();

			//MARK：添加这一句，会去掉其他线程使用本UI控件时弹出异常的问题(权宜之计，并非长久方案)。
			CheckForIllegalCrossThreadCalls = false;

			string loadexeName = Application.ExecutablePath;
			FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(loadexeName);
			string appFileVersion = string.Format("{0}.{1}.{2}.{3}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);
			Text += " v" + appFileVersion;

			// 添加上次存储的固件升级包、传输配置等; 先做非空确认
			if ( File.Exists(Properties.Settings.Default.abinPath) ){
				abinOpenDialog.FileName = Properties.Settings.Default.abinPath;				
			}else {
				Properties.Settings.Default.abinPath = null;
				Properties.Settings.Default.Save();
			}

			if (File.Exists(Properties.Settings.Default.ebinPath))
			{
				ebinPathLabel.Text = Properties.Settings.Default.ebinPath;
				ebinSelectDialog.FileName = Properties.Settings.Default.ebinPath;
			}
			else
			{
				Properties.Settings.Default.ebinPath = null;
				Properties.Settings.Default.Save();
			}

			if (File.Exists(Properties.Settings.Default.fbinPath))
			{
				fbinPathLabel.Text = Properties.Settings.Default.fbinPath;
				fbinSelectDialog.FileName = Properties.Settings.Default.fbinPath;
			}
			else
			{
				Properties.Settings.Default.fbinPath = null;
				Properties.Settings.Default.Save();
			}
						
			relayTimeNUD.Value = Properties.Settings.Default.relayTime;
			packageSizeNUD.Value = Properties.Settings.Default.packageSize;

			//添加各类监听器
			dimmerNUD.MouseWheel += someNUD_MouseWheel;
			rNUD.MouseWheel += someNUD_MouseWheel;
			gNUD.MouseWheel += someNUD_MouseWheel;
			bNUD.MouseWheel += someNUD_MouseWheel;
			wNUD.MouseWheel += someNUD_MouseWheel;
			sceneNUD.MouseWheel += someNUD_MouseWheel;
			stepTimeNUD.MouseWheel += someNUD_MouseWheel;
			relayTimeNUD.MouseWheel += someNUD_MouseWheel;
			packageSizeNUD.MouseWheel += someNUD_MouseWheel;

			// 软件启动时，顺手搜索一次设备
			cnc = CSJNetCommunitor.GetInstance();
			cnc.Start(); //启动服务
			cnc.SearchDevice(readCompleted, readError);// 搜设备	
		}

		/// <summary>
		///  
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ConfForm_Load(object sender, EventArgs e)	{	}

		#region 通用方法

		/// <summary>
		/// 辅助方法：按要求显示提示
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="isMsbShow"></param>
		private void setNotice(string msg, bool isMsbShow)
		{
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
			RecordTest.GetInstance().Test();
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
		/// 回读成功后的操作
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="msg"></param>
		private void readCompleted(object obj, string msg)
		{
			CSJConf cc = obj as CSJConf;
			renderAllControls(cc);
			setNotice(msg, isSuccessShow);
		}

		/// <summary>
		/// 回读失败后的操作
		/// </summary>
		/// <param name="msg"></param>
		private void readError(string msg)
		{
			setNotice(msg, true);
		}

		/// <summary>
		/// 事件：点击《下载参数》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void writeButton_Click(object sender, EventArgs e)
		{
			CSJConf cc = makeCC();
			if (cc != null)
			{
				cnc.WriteParam(cc, writeCompleted, writeError);
			}
		}

		/// <summary>
		/// 辅助方法：根据界面的内容，封装CSJConf对象
		/// </summary>
		private CSJConf makeCC()
		{
			CSJConf cc = new CSJConf();
			try
			{
				//cc.MIA_HAO = pswTB.Text;  // 密码限定为6位，不能多不能少
				if (cc.MIA_HAO.Length != 6)
				{
					setNotice("密码必须为6位，请修改后重试", true);
					return null;
				}
				cc.Addr = int.Parse(addrTB.Text);
				cc.Baud = baudCB.SelectedIndex;
				//cc.IsSetBad = badCheckBox.Checked;
				cc.DiskFlag = int.Parse(diskFlagTB.Text);
				cc.Play_Mod = playModeCB.SelectedIndex;
				cc.PlayScene = decimal.ToInt32(sceneNUD.Value);
				cc.LedName = ledNameTB.Text.Trim(); // 上限为16,无下限
				cc.Ver = verTB.Text.Trim(); //上限为16，,无下限
				cc.Max_scan_dot = int.Parse(maxDotTB.Text);
				//cc.CardType = cardRB1.Checked ? 0 : 2;
				cc.Led_out_type = outTypeCB.SelectedIndex;
				cc.Led_fx = int.Parse(fxTB.Text);
				cc.RGB_Type = rgbCB.SelectedIndex;
				cc.IC_Type = int.Parse(icTB.Text);
				cc.Play_hz = decimal.ToInt32(stepTimeNUD.Value);
				cc.Clk_shzhong = int.Parse(clockTB.Text);
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
				//cc.SumUseTimes = int.Parse(aSumTB.Text);
				//cc.CurrUseTimes = int.Parse(aCurrTB.Text);

			}
			catch (Exception ex)
			{
				setNotice("参数格式有误" + ex.Message, true);
				return null;
			}
			return cc;
		}

		/// <summary>
		/// 回调方法：下载参数成功
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="msg"></param>
		private void writeCompleted(object obj, string msg)
		{
			setNotice("参数下载成功", isSuccessShow);
		}

		/// <summary>
		/// 回调方法：下载参数失败
		/// </summary>
		/// <param name="msg"></param>
		private void writeError(string msg)
		{
			setNotice("参数下载失败", true);
		}

		/// <summary>
		/// 辅助方法：使能所有的控件，并填入cc的内容；
		/// </summary>
		/// <param name="v"></param>
		private void renderAllControls(CSJConf cc)
		{
			//if (cc == null) {
			//	cc = readFromLocal();
			//}

			// 根据cc,渲染各个控件
			if (cc != null)
			{
				//pswTB.Text = cc.MIA_HAO ;  // 密码限定为6位，不能多不能少
				addrTB.Text = cc.Addr + "";
				baudCB.SelectedIndex = cc.Baud;
				//badCheckBox.Checked = cc.IsSetBad ;
				diskFlagTB.Text = cc.DiskFlag + "";
				playModeCB.SelectedIndex = cc.Play_Mod;
				sceneNUD.Value = cc.PlayScene;
				ledNameTB.Text = cc.LedName; // 上限为16,无下限
				verTB.Text = cc.Ver; //上限为16，,无下限
				maxDotTB.Text = cc.Max_scan_dot + "";
				//cardRB1.Checked = cc.CardType == 0;
				//cardRB2.Checked = cc.CardType == 2;
				outTypeCB.SelectedIndex = cc.Led_out_type;
				fxTB.Text = cc.Led_fx + "";
				rgbCB.SelectedIndex = cc.RGB_Type;
				icTB.Text = cc.IC_Type + "";
				stepTimeNUD.Value = cc.Play_hz;
				clockTB.Text = cc.Clk_shzhong + "";
				ledGamTB.Text = cc.Led_gam + "";
				dimmerNUD.Value = cc.Led_ld;
				rNUD.Value = cc.R_LD;
				gNUD.Value = cc.G_LD;
				bNUD.Value = cc.B_LD;
				wNUD.Value = cc.W_LD;

				// ARTNET参数
				aMacTB.Text = cc.Mac;
				aIpTB.Text = cc.Ip;
				aFKLSTB.Text = cc.Fk_lushu + "";
				aJLFKSTB.Text = cc.Jl_fk_num + "";
				aStartTB.Text = cc.Art_Net_Start_Space + "";
				aPerTB.Text = cc.Art_Net_Pre + "";
				aTdLenTB.Text = cc.Art_Net_td_len + "";
				aFKHTB.Text = cc.Art_Net_fk_id + "";
				//aSumTB.Text = cc.SumUseTimes +"";
				//aCurrTB.Text = cc.CurrUseTimes +"";
			}
		}

		/// <summary>
		/// 事件：点击《加载配置》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loadButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == abinOpenDialog.ShowDialog())
			{
				string binPath = abinOpenDialog.FileName;
				try
				{
					CSJConf cc = (CSJConf)SerializeUtils.DeserializeToObject(binPath);
					renderAllControls(cc);
					setNotice("成功加载本地配置文件(" + abinOpenDialog.SafeFileName + ")。", isSuccessShow);
				}
				catch (Exception ex)
				{
					setNotice("加载配置文件时发生异常：" + ex.Message, true);
				}
			}
		}

		/// <summary>
		/// 事件：点击《保存配置(到本地)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == abinSaveDialog.ShowDialog())
			{

				CSJConf cc = makeCC();
				if (cc != null)
				{
					try
					{
						string binPath = abinSaveDialog.FileName;
						SerializeUtils.SerializeObject(binPath, cc);
						setNotice("成功保存配置到本地。", isSuccessShow);
					}
					catch (Exception ex)
					{
						setNotice("保存配置时发生异常：" + ex.Message, true);
					}
				}
			}
		}

		/// <summary>
		/// 事件：点击《选择ebin升级文件》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ebinSelectButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == ebinSelectDialog.ShowDialog())
			{
				ebinPathLabel.Text = ebinSelectDialog.FileName;
				Properties.Settings.Default.ebinPath = ebinSelectDialog.FileName;
				Properties.Settings.Default.Save();
			}
		}

		/// <summary>
		/// 事件：点击《MCU升级》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mcuUpdateButton_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(ebinPathLabel.Text))
			{
				cnc.UpdataMCU256(ebinPathLabel.Text, UpdateCompleted, UpdateError);
				Enabled = false;
			}
		}

		/// <summary>
		///  
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fbinSelectButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == fbinSelectDialog.ShowDialog())
			{
				fbinPathLabel.Text = fbinSelectDialog.FileName;
				Properties.Settings.Default.fbinPath = fbinSelectDialog.FileName;
				Properties.Settings.Default.Save();
			}
		}

		/// <summary>
		/// 事件：点击《FPGA升级》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fpgaUpdateButton_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(ebinPathLabel.Text))
			{
				cnc.UpdateFPGA256(ebinPathLabel.Text, UpdateCompleted, UpdateError);
				Enabled = false;
			}
		}

		/// <summary>
		/// 升级失败回调方法
		/// </summary>
		/// <param name="msg"></param>
		private void UpdateError(string msg)
		{
			setNotice(msg, isSuccessShow);
			Enabled = true;
		}

		/// <summary>
		/// 升级失败回调方法
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="msg"></param>
		private void UpdateCompleted(object obj, string msg)
		{
			setNotice(msg, true);
			Enabled = true;
		}

		private int clickTime = 0;
		/// <summary>
		/// 两次双击参数配置Tab的空白处，会出现加密工具框
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void paramTab_DoubleClick(object sender, EventArgs e)
		{
			clickTime++;
			if (clickTime == 3)
			{
				new SpecialForm(this).ShowDialog();
				clickTime = 0;
			}
		}

		/// <summary>
		/// 更改延时时间后，存储到注册表中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void relayTimeNUD_ValueChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.relayTime = decimal.ToInt32(relayTimeNUD.Value);
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// 更改包大小后，存储到注册表中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void packageSizeNUD_ValueChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.packageSize = decimal.ToInt32(packageSizeNUD.Value);
			Properties.Settings.Default.Save();
		}
	}

}
