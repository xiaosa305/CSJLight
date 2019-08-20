namespace LightController.MyForm
{
	partial class HardwareSetForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.commonGroupBox = new System.Windows.Forms.GroupBox();
			this.playFlagComboBox = new System.Windows.Forms.ComboBox();
			this.addrNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.baudComboBox = new System.Windows.Forms.ComboBox();
			this.currUseTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label20 = new System.Windows.Forms.Label();
			this.sumUseTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label16 = new System.Windows.Forms.Label();
			this.heartbeatCycleNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.diskFlagComboBox = new System.Windows.Forms.ComboBox();
			this.heartbeatTextBox = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.hardwareIDTextBox = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.deviceNameTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.networkGroupBox = new System.Windows.Forms.GroupBox();
			this.linkModeComboBox = new System.Windows.Forms.ComboBox();
			this.linkPortTextBox = new System.Windows.Forms.TextBox();
			this.gatewayTextBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.macTextBox = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.IPTextBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.netmaskTextBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.otherGroupBox = new System.Windows.Forms.GroupBox();
			this.remotePortTextBox = new System.Windows.Forms.TextBox();
			this.domainServerTextBox = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.remoteHostTextBox = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.domainNameTextBox = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.saveSkinButton = new CCWin.SkinControl.SkinButton();
			this.connectSkinButton = new CCWin.SkinControl.SkinButton();
			this.downloadSkinButton = new CCWin.SkinControl.SkinButton();
			this.commonGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.addrNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.currUseTimeNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sumUseTimeNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.heartbeatCycleNumericUpDown)).BeginInit();
			this.networkGroupBox.SuspendLayout();
			this.otherGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// commonGroupBox
			// 
			this.commonGroupBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.commonGroupBox.Controls.Add(this.playFlagComboBox);
			this.commonGroupBox.Controls.Add(this.addrNumericUpDown);
			this.commonGroupBox.Controls.Add(this.baudComboBox);
			this.commonGroupBox.Controls.Add(this.currUseTimeNumericUpDown);
			this.commonGroupBox.Controls.Add(this.label20);
			this.commonGroupBox.Controls.Add(this.sumUseTimeNumericUpDown);
			this.commonGroupBox.Controls.Add(this.label16);
			this.commonGroupBox.Controls.Add(this.heartbeatCycleNumericUpDown);
			this.commonGroupBox.Controls.Add(this.diskFlagComboBox);
			this.commonGroupBox.Controls.Add(this.heartbeatTextBox);
			this.commonGroupBox.Controls.Add(this.label19);
			this.commonGroupBox.Controls.Add(this.label4);
			this.commonGroupBox.Controls.Add(this.label3);
			this.commonGroupBox.Controls.Add(this.hardwareIDTextBox);
			this.commonGroupBox.Controls.Add(this.label13);
			this.commonGroupBox.Controls.Add(this.label18);
			this.commonGroupBox.Controls.Add(this.label2);
			this.commonGroupBox.Controls.Add(this.label17);
			this.commonGroupBox.Controls.Add(this.deviceNameTextBox);
			this.commonGroupBox.Controls.Add(this.label1);
			this.commonGroupBox.Location = new System.Drawing.Point(-2, 6);
			this.commonGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.commonGroupBox.Name = "commonGroupBox";
			this.commonGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.commonGroupBox.Size = new System.Drawing.Size(441, 186);
			this.commonGroupBox.TabIndex = 0;
			this.commonGroupBox.TabStop = false;
			this.commonGroupBox.Text = "通用设置";
			// 
			// playFlagComboBox
			// 
			this.playFlagComboBox.FormattingEnabled = true;
			this.playFlagComboBox.Items.AddRange(new object[] {
            "录播文件",
            "程序文件"});
			this.playFlagComboBox.Location = new System.Drawing.Point(318, 153);
			this.playFlagComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.playFlagComboBox.Name = "playFlagComboBox";
			this.playFlagComboBox.Size = new System.Drawing.Size(80, 20);
			this.playFlagComboBox.TabIndex = 5;
			// 
			// addrNumericUpDown
			// 
			this.addrNumericUpDown.Location = new System.Drawing.Point(317, 21);
			this.addrNumericUpDown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.addrNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.addrNumericUpDown.Name = "addrNumericUpDown";
			this.addrNumericUpDown.Size = new System.Drawing.Size(82, 21);
			this.addrNumericUpDown.TabIndex = 3;
			this.addrNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// baudComboBox
			// 
			this.baudComboBox.FormattingEnabled = true;
			this.baudComboBox.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
			this.baudComboBox.Location = new System.Drawing.Point(100, 153);
			this.baudComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.baudComboBox.Name = "baudComboBox";
			this.baudComboBox.Size = new System.Drawing.Size(80, 20);
			this.baudComboBox.TabIndex = 6;
			// 
			// currUseTimeNumericUpDown
			// 
			this.currUseTimeNumericUpDown.Location = new System.Drawing.Point(317, 86);
			this.currUseTimeNumericUpDown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.currUseTimeNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.currUseTimeNumericUpDown.Name = "currUseTimeNumericUpDown";
			this.currUseTimeNumericUpDown.Size = new System.Drawing.Size(82, 21);
			this.currUseTimeNumericUpDown.TabIndex = 3;
			this.currUseTimeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(224, 156);
			this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(65, 12);
			this.label20.TabIndex = 3;
			this.label20.Text = "优先播放：";
			// 
			// sumUseTimeNumericUpDown
			// 
			this.sumUseTimeNumericUpDown.Location = new System.Drawing.Point(100, 86);
			this.sumUseTimeNumericUpDown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.sumUseTimeNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.sumUseTimeNumericUpDown.Name = "sumUseTimeNumericUpDown";
			this.sumUseTimeNumericUpDown.Size = new System.Drawing.Size(82, 21);
			this.sumUseTimeNumericUpDown.TabIndex = 3;
			this.sumUseTimeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(14, 156);
			this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(53, 12);
			this.label16.TabIndex = 4;
			this.label16.Text = "波特率：";
			// 
			// heartbeatCycleNumericUpDown
			// 
			this.heartbeatCycleNumericUpDown.Location = new System.Drawing.Point(317, 120);
			this.heartbeatCycleNumericUpDown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.heartbeatCycleNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.heartbeatCycleNumericUpDown.Name = "heartbeatCycleNumericUpDown";
			this.heartbeatCycleNumericUpDown.Size = new System.Drawing.Size(82, 21);
			this.heartbeatCycleNumericUpDown.TabIndex = 3;
			this.heartbeatCycleNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// diskFlagComboBox
			// 
			this.diskFlagComboBox.FormattingEnabled = true;
			this.diskFlagComboBox.Items.AddRange(new object[] {
            "SD卡",
            "U盘",
            "内部存储"});
			this.diskFlagComboBox.Location = new System.Drawing.Point(98, 54);
			this.diskFlagComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.diskFlagComboBox.Name = "diskFlagComboBox";
			this.diskFlagComboBox.Size = new System.Drawing.Size(85, 20);
			this.diskFlagComboBox.TabIndex = 2;
			// 
			// heartbeatTextBox
			// 
			this.heartbeatTextBox.Location = new System.Drawing.Point(100, 120);
			this.heartbeatTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.heartbeatTextBox.MaxLength = 8;
			this.heartbeatTextBox.Name = "heartbeatTextBox";
			this.heartbeatTextBox.Size = new System.Drawing.Size(84, 21);
			this.heartbeatTextBox.TabIndex = 1;
			this.heartbeatTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateLetterOrDigit_KeyPress);
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(12, 124);
			this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(53, 12);
			this.label19.TabIndex = 0;
			this.label19.Text = "心跳包：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(10, 90);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 12);
			this.label4.TabIndex = 0;
			this.label4.Text = "总使用次数:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(224, 25);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "硬件地址:";
			// 
			// hardwareIDTextBox
			// 
			this.hardwareIDTextBox.Location = new System.Drawing.Point(293, 53);
			this.hardwareIDTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.hardwareIDTextBox.MaxLength = 16;
			this.hardwareIDTextBox.Name = "hardwareIDTextBox";
			this.hardwareIDTextBox.Size = new System.Drawing.Size(108, 21);
			this.hardwareIDTextBox.TabIndex = 1;
			this.hardwareIDTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateDigit_KeyPress);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(224, 57);
			this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(53, 12);
			this.label13.TabIndex = 0;
			this.label13.Text = "硬盘ID：";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(224, 124);
			this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(83, 12);
			this.label18.TabIndex = 0;
			this.label18.Text = "心跳周期(s)：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 57);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "硬盘标识：";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(224, 90);
			this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(89, 12);
			this.label17.TabIndex = 0;
			this.label17.Text = "当前使用次数：";
			// 
			// deviceNameTextBox
			// 
			this.deviceNameTextBox.Location = new System.Drawing.Point(73, 19);
			this.deviceNameTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.deviceNameTextBox.MaxLength = 16;
			this.deviceNameTextBox.Name = "deviceNameTextBox";
			this.deviceNameTextBox.Size = new System.Drawing.Size(111, 21);
			this.deviceNameTextBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 23);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "主控标识：";
			// 
			// networkGroupBox
			// 
			this.networkGroupBox.BackColor = System.Drawing.SystemColors.Control;
			this.networkGroupBox.Controls.Add(this.linkModeComboBox);
			this.networkGroupBox.Controls.Add(this.linkPortTextBox);
			this.networkGroupBox.Controls.Add(this.gatewayTextBox);
			this.networkGroupBox.Controls.Add(this.label9);
			this.networkGroupBox.Controls.Add(this.label5);
			this.networkGroupBox.Controls.Add(this.macTextBox);
			this.networkGroupBox.Controls.Add(this.label10);
			this.networkGroupBox.Controls.Add(this.IPTextBox);
			this.networkGroupBox.Controls.Add(this.label6);
			this.networkGroupBox.Controls.Add(this.netmaskTextBox);
			this.networkGroupBox.Controls.Add(this.label7);
			this.networkGroupBox.Controls.Add(this.label8);
			this.networkGroupBox.Location = new System.Drawing.Point(-2, 196);
			this.networkGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.networkGroupBox.Name = "networkGroupBox";
			this.networkGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.networkGroupBox.Size = new System.Drawing.Size(441, 178);
			this.networkGroupBox.TabIndex = 0;
			this.networkGroupBox.TabStop = false;
			this.networkGroupBox.Text = "网络设置";
			// 
			// linkModeComboBox
			// 
			this.linkModeComboBox.FormattingEnabled = true;
			this.linkModeComboBox.Items.AddRange(new object[] {
            "TCP",
            "UDP"});
			this.linkModeComboBox.Location = new System.Drawing.Point(102, 30);
			this.linkModeComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.linkModeComboBox.Name = "linkModeComboBox";
			this.linkModeComboBox.Size = new System.Drawing.Size(80, 20);
			this.linkModeComboBox.TabIndex = 2;
			// 
			// linkPortTextBox
			// 
			this.linkPortTextBox.Location = new System.Drawing.Point(286, 69);
			this.linkPortTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.linkPortTextBox.Name = "linkPortTextBox";
			this.linkPortTextBox.Size = new System.Drawing.Size(57, 21);
			this.linkPortTextBox.TabIndex = 1;
			this.linkPortTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateDigit_KeyPress);
			// 
			// gatewayTextBox
			// 
			this.gatewayTextBox.Location = new System.Drawing.Point(286, 105);
			this.gatewayTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.gatewayTextBox.Name = "gatewayTextBox";
			this.gatewayTextBox.Size = new System.Drawing.Size(146, 21);
			this.gatewayTextBox.TabIndex = 1;
			this.gatewayTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateIP_KeyPress);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(230, 73);
			this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(53, 12);
			this.label9.TabIndex = 0;
			this.label9.Text = "端口号：";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(230, 109);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 0;
			this.label5.Text = "网关：";
			// 
			// macTextBox
			// 
			this.macTextBox.Location = new System.Drawing.Point(76, 141);
			this.macTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.macTextBox.Name = "macTextBox";
			this.macTextBox.Size = new System.Drawing.Size(146, 21);
			this.macTextBox.TabIndex = 1;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(14, 145);
			this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(53, 12);
			this.label10.TabIndex = 0;
			this.label10.Text = "Mac地址:";
			// 
			// IPTextBox
			// 
			this.IPTextBox.Location = new System.Drawing.Point(76, 69);
			this.IPTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.IPTextBox.Name = "IPTextBox";
			this.IPTextBox.Size = new System.Drawing.Size(146, 21);
			this.IPTextBox.TabIndex = 1;
			this.IPTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateIP_KeyPress);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(14, 73);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(47, 12);
			this.label6.TabIndex = 0;
			this.label6.Text = "IP地址:";
			// 
			// netmaskTextBox
			// 
			this.netmaskTextBox.Location = new System.Drawing.Point(76, 105);
			this.netmaskTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.netmaskTextBox.Name = "netmaskTextBox";
			this.netmaskTextBox.Size = new System.Drawing.Size(146, 21);
			this.netmaskTextBox.TabIndex = 1;
			this.netmaskTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateIP_KeyPress);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(14, 109);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(65, 12);
			this.label7.TabIndex = 0;
			this.label7.Text = "子网掩码：";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(14, 34);
			this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(89, 12);
			this.label8.TabIndex = 0;
			this.label8.Text = "网络连接类型：";
			// 
			// otherGroupBox
			// 
			this.otherGroupBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.otherGroupBox.Controls.Add(this.remotePortTextBox);
			this.otherGroupBox.Controls.Add(this.domainServerTextBox);
			this.otherGroupBox.Controls.Add(this.label11);
			this.otherGroupBox.Controls.Add(this.label12);
			this.otherGroupBox.Controls.Add(this.remoteHostTextBox);
			this.otherGroupBox.Controls.Add(this.label14);
			this.otherGroupBox.Controls.Add(this.domainNameTextBox);
			this.otherGroupBox.Controls.Add(this.label15);
			this.otherGroupBox.Location = new System.Drawing.Point(-2, 383);
			this.otherGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.otherGroupBox.Name = "otherGroupBox";
			this.otherGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.otherGroupBox.Size = new System.Drawing.Size(441, 122);
			this.otherGroupBox.TabIndex = 0;
			this.otherGroupBox.TabStop = false;
			this.otherGroupBox.Text = "其他设置";
			// 
			// remotePortTextBox
			// 
			this.remotePortTextBox.Location = new System.Drawing.Point(289, 32);
			this.remotePortTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.remotePortTextBox.Name = "remotePortTextBox";
			this.remotePortTextBox.Size = new System.Drawing.Size(57, 21);
			this.remotePortTextBox.TabIndex = 1;
			this.remotePortTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateDigit_KeyPress);
			// 
			// domainServerTextBox
			// 
			this.domainServerTextBox.Location = new System.Drawing.Point(288, 69);
			this.domainServerTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.domainServerTextBox.Name = "domainServerTextBox";
			this.domainServerTextBox.Size = new System.Drawing.Size(146, 21);
			this.domainServerTextBox.TabIndex = 1;
			this.domainServerTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateIP_KeyPress);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(231, 36);
			this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(65, 12);
			this.label11.TabIndex = 0;
			this.label11.Text = "远端端口：";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(232, 73);
			this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(65, 12);
			this.label12.TabIndex = 0;
			this.label12.Text = "服务器IP：";
			// 
			// remoteHostTextBox
			// 
			this.remoteHostTextBox.Location = new System.Drawing.Point(76, 32);
			this.remoteHostTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.remoteHostTextBox.Name = "remoteHostTextBox";
			this.remoteHostTextBox.Size = new System.Drawing.Size(146, 21);
			this.remoteHostTextBox.TabIndex = 1;
			this.remoteHostTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateIP_KeyPress);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(14, 36);
			this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(53, 12);
			this.label14.TabIndex = 0;
			this.label14.Text = "远端IP：";
			// 
			// domainNameTextBox
			// 
			this.domainNameTextBox.Location = new System.Drawing.Point(76, 69);
			this.domainNameTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.domainNameTextBox.MaxLength = 32;
			this.domainNameTextBox.Name = "domainNameTextBox";
			this.domainNameTextBox.Size = new System.Drawing.Size(146, 21);
			this.domainNameTextBox.TabIndex = 1;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(14, 73);
			this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(65, 12);
			this.label15.TabIndex = 0;
			this.label15.Text = "服务器域名";
			// 
			// saveSkinButton
			// 
			this.saveSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.saveSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.saveSkinButton.BorderColor = System.Drawing.Color.Black;
			this.saveSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.saveSkinButton.DownBack = null;
			this.saveSkinButton.Location = new System.Drawing.Point(51, 519);
			this.saveSkinButton.MouseBack = null;
			this.saveSkinButton.Name = "saveSkinButton";
			this.saveSkinButton.NormlBack = null;
			this.saveSkinButton.Size = new System.Drawing.Size(70, 33);
			this.saveSkinButton.TabIndex = 2;
			this.saveSkinButton.Text = "保存";
			this.saveSkinButton.UseVisualStyleBackColor = false;
			this.saveSkinButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// connectSkinButton
			// 
			this.connectSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.connectSkinButton.BaseColor = System.Drawing.Color.SeaGreen;
			this.connectSkinButton.BorderColor = System.Drawing.Color.Black;
			this.connectSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.connectSkinButton.DownBack = null;
			this.connectSkinButton.Location = new System.Drawing.Point(286, 519);
			this.connectSkinButton.MouseBack = null;
			this.connectSkinButton.Name = "connectSkinButton";
			this.connectSkinButton.NormlBack = null;
			this.connectSkinButton.Size = new System.Drawing.Size(70, 33);
			this.connectSkinButton.TabIndex = 2;
			this.connectSkinButton.Text = "连接";
			this.connectSkinButton.UseVisualStyleBackColor = false;
			this.connectSkinButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// downloadSkinButton
			// 
			this.downloadSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.downloadSkinButton.BaseColor = System.Drawing.Color.Tan;
			this.downloadSkinButton.BorderColor = System.Drawing.Color.Black;
			this.downloadSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.downloadSkinButton.DownBack = null;
			this.downloadSkinButton.Location = new System.Drawing.Point(362, 519);
			this.downloadSkinButton.MouseBack = null;
			this.downloadSkinButton.Name = "downloadSkinButton";
			this.downloadSkinButton.NormlBack = null;
			this.downloadSkinButton.Size = new System.Drawing.Size(70, 33);
			this.downloadSkinButton.TabIndex = 2;
			this.downloadSkinButton.Text = "下载";
			this.downloadSkinButton.UseVisualStyleBackColor = false;
			this.downloadSkinButton.Click += new System.EventHandler(this.downloadButton_Click);
			// 
			// HardwareSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(439, 564);
			this.Controls.Add(this.downloadSkinButton);
			this.Controls.Add(this.connectSkinButton);
			this.Controls.Add(this.saveSkinButton);
			this.Controls.Add(this.otherGroupBox);
			this.Controls.Add(this.networkGroupBox);
			this.Controls.Add(this.commonGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Name = "HardwareSetForm";
			this.Text = "硬件设置";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HardwareSetForm_FormClosed);
			this.Load += new System.EventHandler(this.HardwareSetForm_Load);
			this.commonGroupBox.ResumeLayout(false);
			this.commonGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.addrNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.currUseTimeNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sumUseTimeNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.heartbeatCycleNumericUpDown)).EndInit();
			this.networkGroupBox.ResumeLayout(false);
			this.networkGroupBox.PerformLayout();
			this.otherGroupBox.ResumeLayout(false);
			this.otherGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox commonGroupBox;
		private System.Windows.Forms.TextBox deviceNameTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox networkGroupBox;
		private System.Windows.Forms.ComboBox linkModeComboBox;
		private System.Windows.Forms.TextBox linkPortTextBox;
		private System.Windows.Forms.TextBox gatewayTextBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox macTextBox;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox IPTextBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox netmaskTextBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox otherGroupBox;
		private System.Windows.Forms.TextBox remotePortTextBox;
		private System.Windows.Forms.TextBox domainServerTextBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox remoteHostTextBox;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox domainNameTextBox;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox heartbeatTextBox;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox hardwareIDTextBox;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.ComboBox diskFlagComboBox;
		private System.Windows.Forms.NumericUpDown heartbeatCycleNumericUpDown;
		private System.Windows.Forms.NumericUpDown currUseTimeNumericUpDown;
		private System.Windows.Forms.NumericUpDown sumUseTimeNumericUpDown;
		private System.Windows.Forms.NumericUpDown addrNumericUpDown;
		private System.Windows.Forms.ComboBox playFlagComboBox;
		private System.Windows.Forms.ComboBox baudComboBox;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label16;
		private CCWin.SkinControl.SkinButton saveSkinButton;
		private CCWin.SkinControl.SkinButton connectSkinButton;
		private CCWin.SkinControl.SkinButton downloadSkinButton;
	}
}