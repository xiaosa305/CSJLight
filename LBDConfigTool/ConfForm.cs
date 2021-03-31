using LBDConfigTool.Common;
using LBDConfigTool.utils.communication;
using LBDConfigTool.utils.conf;
using LBDConfigTool.utils.entity;
using LBDConfigTool.utils.record;
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
		private CSJConf specialCC;  // 辅助的cc，在程序初始化后应该设为一个默认值，除非用户进行修改

		private string deviceIP ;  // 必须是从设备回读的信息，才能设为true。即必须是readCompleted方法：
		private bool isRecording = false; //正在录制时，设为true；
		private DMXManager simulator;  //
		private string dirPath ; //录制文件存储路径
		//private int recordIndex = 0; //录制文件序号

		public ConfForm()
		{
			InitializeComponent();

			// 加密panel是否显示
			securePanel.Visible = Properties.Settings.Default.showSecure;

			//MARK：添加这一句，会去掉其他线程使用本UI控件时弹出异常的问题(权宜之计，并非长久方案)。
			CheckForIllegalCrossThreadCalls = false;

			string loadexeName = Application.ExecutablePath;
			FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(loadexeName);
			string appFileVersion = string.Format("{0}.{1}.{2}.{3}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);
			Text += " v" + appFileVersion;

			// 芯片类型处理
			IniUtils iniUtils = new IniUtils(Application.StartupPath + @"\CtrlType.ini");
			for (int typeIndex = 0; typeIndex <= 55; typeIndex++)
			{
				icCB.Items.Add(iniUtils.ReadString("main", "EN" + typeIndex, ""));
			}
			icCB.SelectedIndex = 0;
			baudCB.SelectedIndex = 3;

			#region  添加上次更改的各种配置

			if ( File.Exists(Properties.Settings.Default.abinPath) ){
				abinOpenDialog.FileName = Properties.Settings.Default.abinPath;				
			}else {
				Properties.Settings.Default.abinPath = null;
				Properties.Settings.Default.Save();
			}

			if (File.Exists(Properties.Settings.Default.ebinPath))
			{
				ebinPathLabel.Text = Properties.Settings.Default.ebinPath;				
			}
			else
			{
				Properties.Settings.Default.ebinPath = null;
				Properties.Settings.Default.Save();
			}

			if (File.Exists(Properties.Settings.Default.fbinPath))
			{
				fbinPathLabel.Text = Properties.Settings.Default.fbinPath;
			}
			else
			{
				Properties.Settings.Default.fbinPath = null;
				Properties.Settings.Default.Save();
			}

			dirPath = Properties.Settings.Default.recordPath;
			setRecordPathLabel();

			firstRelayNUD.Value = Properties.Settings.Default.firstRelayTime;
			relayTimeNUD.Value = Properties.Settings.Default.relayTime;
			packageSizeNUD.Value = Properties.Settings.Default.packageSize;
			partitionTimeNUD.Value = Properties.Settings.Default.partitionTime;
			partitionSizeNUD.Value = Properties.Settings.Default.partitionSize;
			fpgaWaitTimeNUD.Value = Properties.Settings.Default.fpgaWaitTime;

			secureTB.Text = Properties.Settings.Default.secureStr;
			scuNameTB.Text = Properties.Settings.Default.scuName;
			fileNameTB.Text =  Properties.Settings.Default.fileName;
			suffixTB.Text = Properties.Settings.Default.suffixName;

			#endregion

			//添加各类监听器
			dimmerNUD.MouseWheel += someNUD_MouseWheel;
			rNUD.MouseWheel += someNUD_MouseWheel;
			gNUD.MouseWheel += someNUD_MouseWheel;
			bNUD.MouseWheel += someNUD_MouseWheel;
			wNUD.MouseWheel += someNUD_MouseWheel;
			sceneNUD.MouseWheel += someNUD_MouseWheel;
			stepTimeNUD.MouseWheel += someNUD_MouseWheel;
			firstRelayNUD.MouseWheel += someNUD_MouseWheel;
			relayTimeNUD.MouseWheel += someNUD_MouseWheel;
			packageSizeNUD.MouseWheel += someNUD_MouseWheel;
			partitionTimeNUD.MouseWheel += someNUD_MouseWheel;
			partitionSizeNUD.MouseWheel += someNUD_MouseWheel;
			fpgaWaitTimeNUD.MouseWheel += someNUD_MouseWheel;

			//specialCC,填充默认值
			makeSpecialCC();

			// 每次启动后，加载默认的配置			
			CSJConf cc = (CSJConf)SerializeUtils.DeserializeToObject(Application.StartupPath + @"\default.abin");
			SetSpecialCC(cc);
			renderAllControls(cc);

		}

		/// <summary>
		/// 辅助方法：填充默认的specialCC
		/// </summary>
		private void makeSpecialCC()
		{
			specialCC= new CSJConf()
			{
				OLD_MIA_HAO = "",
				MIA_HAO = "",  // 密码限定为6位，不能多不能少				
				IsSetBad = false,
				CardType = 0,
				SumUseTimes = 0,
				CurrUseTimes = 0
			};
		}	

		/// <summary>
		/// 辅助方法：供《SpecialForm》调用，替换当前的specialCC
		/// </summary>
		/// <param name="scc"></param>
		public void SetSpecialCC(CSJConf cc) {			
			specialCC.OLD_MIA_HAO = cc.OLD_MIA_HAO;
			specialCC.MIA_HAO = cc.MIA_HAO;  
			specialCC.IsSetBad = cc.IsSetBad;
			specialCC.CardType = cc.CardType;
			specialCC.SumUseTimes = cc.SumUseTimes;
			specialCC.CurrUseTimes = cc.CurrUseTimes;
		}

		/// <summary>
		///  
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ConfForm_Load(object sender, EventArgs e)	{

			// 监听在最后加，避免自循环后修改并保存内容
			firstRelayNUD.ValueChanged += saveNUD_ValueChanged;
			relayTimeNUD.ValueChanged += saveNUD_ValueChanged;
			packageSizeNUD.ValueChanged += saveNUD_ValueChanged;
			partitionSizeNUD.ValueChanged += saveNUD_ValueChanged;
			partitionTimeNUD.ValueChanged += saveNUD_ValueChanged;
			fpgaWaitTimeNUD.ValueChanged += saveNUD_ValueChanged;

			// 软件Load时，搜索一次设备（在加载默认配置之后了）
			cnc = CSJNetCommunitor.GetInstance();
			cnc.Start(); //启动服务
			cnc.SearchDevice(readCompleted, readError);// 搜设备

		}

		#region 通用方法

		/// <summary>
		/// 辅助方法：按要求显示提示
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="isMsbShow"></param>
		private void setNotice(int labelPos, string msg, bool isMsbShow)
		{
			if (labelPos == 1)myStatusLabel1.Text = msg;
			else	myStatusLabel2.Text = msg;			
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
			//RecordTest.GetInstance().Test();
			//Console.WriteLine(specialCC);

			//if (pswTB.Text.Trim().Length != 16) {
			//	setNotice(1, "加密文本必须是16位。", true);
			//	return;
			//}

			cnc.WriteEncrypt(secureTB.Text, null, null);

			Properties.Settings.Default.secureStr = secureTB.Text;
			Properties.Settings.Default.Save();
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
			SetSpecialCC(cc);
			renderAllControls(cc);
			setNotice(1,msg, isSuccessShow);
		}

		/// <summary>
		/// 回读失败后的操作
		/// </summary>
		/// <param name="msg"></param>
		private void readError(string msg)
		{
			setNotice(1, msg, true);
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
				// 先填入specialCC内的数据
				cc.OLD_MIA_HAO = specialCC.OLD_MIA_HAO;
				cc.MIA_HAO = specialCC.MIA_HAO;  // 密码限定为6位，不能多不能少				
				cc.IsSetBad = specialCC.IsSetBad;
				cc.CardType = specialCC.CardType;
				cc.SumUseTimes = specialCC.SumUseTimes;
				cc.CurrUseTimes = specialCC.CurrUseTimes;

				// 普通参数
				cc.Addr = int.Parse(addrTB.Text);
				cc.Baud = baudCB.SelectedIndex;				
				cc.DiskFlag = int.Parse(diskFlagTB.Text);
				cc.Play_Mod = playModeCB.SelectedIndex;
				cc.PlayScene = decimal.ToInt32(sceneNUD.Value);
				cc.LedName = ledNameTB.Text.Trim(); // 上限为16,无下限
				cc.Ver = verTB.Text.Trim(); //上限为16，,无下限
				cc.Max_scan_dot = int.Parse(maxDotTB.Text);
				cc.Led_out_type = outTypeCB.SelectedIndex;
				cc.Led_fx = fxCheckBox.Checked ? 1:0; 
				cc.RGB_Type = rgbCB.SelectedIndex;
				cc.IC_Type = icCB.SelectedIndex;
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

			}
			catch (Exception ex)
			{
				setNotice(1, "参数格式有误" + ex.Message, true);
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
			setNotice(1, "参数下载成功", isSuccessShow);
		}

		/// <summary>
		/// 回调方法：下载参数失败
		/// </summary>
		/// <param name="msg"></param>
		private void writeError(string msg)
		{
			setNotice(1, "参数下载失败", true);
		}

		/// <summary>
		/// 辅助方法：使能所有的控件，并填入cc的内容；
		/// </summary>
		/// <param name="v"></param>
		private void renderAllControls(CSJConf cc)
		{
			// 根据cc,渲染各个控件
			if (cc != null)
			{
				addrTB.Text = cc.Addr + "";
				baudCB.SelectedIndex = cc.Baud;				
				diskFlagTB.Text = cc.DiskFlag + "";
				playModeCB.SelectedIndex = cc.Play_Mod;
				sceneNUD.Value = cc.PlayScene;
				ledNameTB.Text = cc.LedName; // 上限为16,无下限
				verTB.Text = cc.Ver; //上限为16，,无下限
				maxDotTB.Text = cc.Max_scan_dot + "";
				outTypeCB.SelectedIndex = cc.Led_out_type;
				fxCheckBox.Checked = cc.Led_fx == 1;
				rgbCB.SelectedIndex = cc.RGB_Type;
				icCB.SelectedIndex = cc.IC_Type;
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
			}
		}

		/// <summary>
		/// 事件：点击《打开配置文件》
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
					// 根据情况，决定是否在加载本地配置后，设置相关的加密的内容；
					SetSpecialCC(cc);
					renderAllControls(cc);
					setNotice(1, "成功加载本地配置文件(" + abinOpenDialog.SafeFileName + ")。", isSuccessShow);
				}
				catch (Exception ex)
				{
					setNotice(1, "加载配置文件时发生异常：" + ex.Message, true);
				}
			}
		}

		/// <summary>
		/// 事件：点击《保存配置文件》
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
						setNotice(1,"成功保存配置到本地。", isSuccessShow);
					}
					catch (Exception ex)
					{
						setNotice(1,"保存配置时发生异常：" + ex.Message, true);
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
				cnc.UpdataMCU256(ebinPathLabel.Text, makePE(), DrawProgress,UpdateCompleted, UpdateError);
				Enabled = false;
			}
		}

		/// <summary>
		/// 辅助回调方法：写进度条
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="progress"></param>
		private void DrawProgress(int progressPercent)
		{
			setNotice(1,"正在升级固件(mcu)，请稍候...", false);
			mcuProgressBar.Value = progressPercent;			
		}

		/// <summary>
		///  事件：点击《选择fpga升级包》
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
			if (!string.IsNullOrEmpty(fbinPathLabel.Text))
			{
				cnc.UpdateFPGA256(fbinPathLabel.Text, makePE(), fpgaDrawProgress, UpdateCompleted, UpdateError);
				Enabled = false;
			}
		}

		/// <summary>
		/// 辅助回调方法：写进度条
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="progress"></param>
		private void fpgaDrawProgress(int progressPercent)
		{
			setNotice(1,"正在升级固件(fpga)，请稍候...", false);
			fpgaProgressBar.Value = progressPercent;
			
		}

		/// <summary>
		/// 辅助方法：由几个NUD生成ParamEntity
		/// </summary>
		/// <returns></returns>
		private ParamEntity makePE()
		{
			return new ParamEntity
			{
				FirstPacketIntervalTime = decimal.ToInt32(firstRelayNUD.Value),
				PacketSize = decimal.ToInt32(packageSizeNUD.Value),
				PacketIntervalTime = decimal.ToInt32(relayTimeNUD.Value),
				PacketIntervalTimeByPartitionIndex = decimal.ToInt32(partitionTimeNUD.Value),
				PartitionIndex = decimal.ToInt32(partitionSizeNUD.Value),
				FPGAUpdateCompletedIntervalTime = decimal.ToInt32(fpgaWaitTimeNUD.Value) * 1000,
			};
		}

		/// <summary>
		/// 升级失败回调方法
		/// </summary>
		/// <param name="msg"></param>
		private void UpdateError(string msg)
		{
			setNotice(1,msg, isSuccessShow);
			mcuProgressBar.Value = 0;
			fpgaProgressBar.Value = 0;
			Enabled = true;
		}

		/// <summary>
		/// 升级失败回调方法
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="msg"></param>
		private void UpdateCompleted(object obj, string msg)
		{
			setNotice(1, msg, true);
			mcuProgressBar.Value = 0;
			fpgaProgressBar.Value = 0;
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
			//if (string.IsNullOrEmpty(specialCC.OLD_MIA_HAO)) {
			//	return;
			//}			

			clickTime++;
			if (clickTime == 3)
			{
				new SpecialForm(this,specialCC).ShowDialog();
				clickTime = 0;
			}
		}

		/// <summary>
		/// 只要更改《6个发送参数》，就把所有值都存储到注册表中（消耗多一点资源但更有效率）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveNUD_ValueChanged(object sender, EventArgs e)
		{
			Console.WriteLine("saveNUD_ValueChanged");

			Properties.Settings.Default.firstRelayTime = decimal.ToInt32(firstRelayNUD.Value);
			Properties.Settings.Default.relayTime = decimal.ToInt32(relayTimeNUD.Value);
			Properties.Settings.Default.packageSize = decimal.ToInt32(packageSizeNUD.Value);
			Properties.Settings.Default.partitionTime = decimal.ToInt32(partitionTimeNUD.Value);
			Properties.Settings.Default.partitionSize = decimal.ToInt32(partitionSizeNUD.Value);
			Properties.Settings.Default.fpgaWaitTime = decimal.ToInt32(fpgaWaitTimeNUD.Value);
			Properties.Settings.Default.Save();
		}

        private void button2_Click(object sender, EventArgs e)
        {
            RecordTest.GetInstance().Test();
        }

		private void recordButton_Click(object sender, EventArgs e)
		{
			if (simulator == null) {
				simulator = new DMXManager(makeCC());
			}

			//停止录制
			if (isRecording)
			{
				simulator.StopRecord();
				enableRecordButtons(false);				;
				setNotice(2, "已停止录制。", false);
				recordButton.Text = "录制数据";
			}
			// 开始录制
			else
			{
				if (string.IsNullOrEmpty(dirPath)
					|| string.IsNullOrEmpty(scuNameTB.Text.Trim()) 
					|| string.IsNullOrEmpty(fileNameTB.Text.Trim()) 
					|| string.IsNullOrEmpty(suffixTB.Text.Trim())
				) {
					setNotice(2, "路径和文件名都不得为空", true);
					return; 
				}

				string configPath = dirPath + @"\" + scuNameTB.Text.Trim() + ".scu";
				string binPath = dirPath + @"\" + fileNameTB.Text.Trim() + "." + suffixTB.Text.Trim();
				if (File.Exists(binPath)) {
					if(DialogResult.No == MessageBox.Show(
						"检测到存在同名录制文件，是否覆盖？",
						"覆盖录制文件？" , 
						 MessageBoxButtons.YesNo,MessageBoxIcon.Asterisk)){
						return;
					}
				}				
				
				try
				{
					setNotice(2, "正在录制文件...", false);
					simulator.StartRecord(binPath, configPath, showRecordFrame);

					Properties.Settings.Default.scuName = scuNameTB.Text.Trim();
					Properties.Settings.Default.fileName = fileNameTB.Text.Trim();
					Properties.Settings.Default.suffixName = suffixTB.Text.Trim();
					Properties.Settings.Default.Save();
				}
				catch (Exception ex)
				{
					setNotice(1, "出现异常:" + ex.Message, true);
				}
				enableRecordButtons(true);
				recordButton.Text = "停止录制";
			}
		}

		/// <summary>
		/// 辅助方法：设置录制相关控件是否可用
		/// </summary>
		/// <param name="recording"></param>
		private void enableRecordButtons(bool recording)
		{
			isRecording = recording;
			setFilePathButton.Enabled = !recording;
			scuNameTB.Enabled = !recording;
			fileNameTB.Enabled = !recording;

			//recordTextBox.Enabled = !recording;
			//plusButton.Enabled = !recording;
			//minusButton.Enabled = !recording;
		}

		///// <summary>
		///// 事件：点击《+》
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void plusButton_Click(object sender, EventArgs e)
		//{
		//	if (recordIndex >= 255)
		//	{
		//		setNotice(2, "录制文件序号不得大于255。", true);
		//		return;
		//	}
		//	recordTextBox.Text = transformRecordIndex(++recordIndex);
		//	setNotice(2, "已设置录制文件名为：SC" + recordTextBox.Text + ".bin", false);
		//}

		///// <summary>
		///// 事件：点击《-》
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void minusButton_Click(object sender, EventArgs e)
		//{
		//	if (recordIndex <= 0)
		//	{
		//		setNotice(2, "录制文件序号不得小于000。", true);
		//		return;
		//	}
		//	recordTextBox.Text = transformRecordIndex(--recordIndex);
		//	setNotice(2, "已设置录制文件名为：SC" + recordTextBox.Text + ".bin", false);
		//}

		///// <summary>
		///// 辅助方法：处理int型,使之成为两位数的string表示
		///// </summary>
		///// <param name="recordIndex"></param>
		///// <returns></returns>
		//private string transformRecordIndex(int recordIndex)
		//{
		//	if (recordIndex < 0)
		//	{
		//		return "000";
		//	}
		//	if (recordIndex > 255)
		//	{
		//		return "255";
		//	}

		//	if (recordIndex < 100)
		//	{
		//		if (recordIndex < 10)
		//		{
		//			return "00" + recordIndex;
		//		}
		//		return "0" + recordIndex;
		//	}
		//	else
		//	{
		//		return recordIndex.ToString();
		//	}
		//}

		/// <summary>
		/// 辅助方法：实现展示录制帧数的委托
		/// </summary>
		/// <param name="count"></param>
		private void showRecordFrame(int count)
		{
			try
			{
				setNotice(2, "当前录制帧数：" + count, false);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}
		}

		/// <summary>
		/// 事件：点击《设置存放目录》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void setFilePathButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == recordFolderBrowserDialog.ShowDialog())
			{
				dirPath = recordFolderBrowserDialog.SelectedPath;
				if ( !dirPath.EndsWith(@"\CSJ_SC"))
				{
					dirPath += @"\CSJ_SC";
				}

				setRecordPathLabel();
				setNotice(2, "已设置存放目录为：" + dirPath, false);
			}
		}

		/// <summary>
		/// 辅助方法：根据当前的recordPath，设置label及toolTip
		/// </summary>
		private void setRecordPathLabel()
		{
			dirPathLabel.Text = dirPath;
			myToolTip.SetToolTip(dirPathLabel, dirPath);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
		{
			// 根据判断是否已经回读了设备参数，才允许进行之后的操作
			if ( string.IsNullOrEmpty(deviceIP) && e.TabPageIndex == 3) {
				//e.Cancel = true;
			}			
		}
	}

}
