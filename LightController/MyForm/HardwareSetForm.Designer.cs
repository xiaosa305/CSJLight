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
			this.commonGroupBox = new System.Windows.Forms.GroupBox();
			this.addrNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.currUseTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.sumUseTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
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
			this.saveButton = new System.Windows.Forms.Button();
			this.downloadButton = new System.Windows.Forms.Button();
			this.connectButton = new System.Windows.Forms.Button();
			this.playFlagComboBox = new System.Windows.Forms.ComboBox();
			this.baudComboBox = new System.Windows.Forms.ComboBox();
			this.label20 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
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
			this.commonGroupBox.Location = new System.Drawing.Point(-2, 7);
			this.commonGroupBox.Name = "commonGroupBox";
			this.commonGroupBox.Size = new System.Drawing.Size(588, 232);
			this.commonGroupBox.TabIndex = 0;
			this.commonGroupBox.TabStop = false;
			this.commonGroupBox.Text = "通用设置";
			// 
			// addrNumericUpDown
			// 
			this.addrNumericUpDown.Location = new System.Drawing.Point(423, 26);
			this.addrNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.addrNumericUpDown.Name = "addrNumericUpDown";
			this.addrNumericUpDown.Size = new System.Drawing.Size(110, 25);
			this.addrNumericUpDown.TabIndex = 3;
			this.addrNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// currUseTimeNumericUpDown
			// 
			this.currUseTimeNumericUpDown.Location = new System.Drawing.Point(423, 108);
			this.currUseTimeNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.currUseTimeNumericUpDown.Name = "currUseTimeNumericUpDown";
			this.currUseTimeNumericUpDown.Size = new System.Drawing.Size(110, 25);
			this.currUseTimeNumericUpDown.TabIndex = 3;
			this.currUseTimeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sumUseTimeNumericUpDown
			// 
			this.sumUseTimeNumericUpDown.Location = new System.Drawing.Point(133, 108);
			this.sumUseTimeNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.sumUseTimeNumericUpDown.Name = "sumUseTimeNumericUpDown";
			this.sumUseTimeNumericUpDown.Size = new System.Drawing.Size(110, 25);
			this.sumUseTimeNumericUpDown.TabIndex = 3;
			this.sumUseTimeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// heartbeatCycleNumericUpDown
			// 
			this.heartbeatCycleNumericUpDown.Location = new System.Drawing.Point(423, 150);
			this.heartbeatCycleNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.heartbeatCycleNumericUpDown.Name = "heartbeatCycleNumericUpDown";
			this.heartbeatCycleNumericUpDown.Size = new System.Drawing.Size(110, 25);
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
			this.diskFlagComboBox.Location = new System.Drawing.Point(131, 67);
			this.diskFlagComboBox.Name = "diskFlagComboBox";
			this.diskFlagComboBox.Size = new System.Drawing.Size(112, 23);
			this.diskFlagComboBox.TabIndex = 2;
			// 
			// heartbeatTextBox
			// 
			this.heartbeatTextBox.Location = new System.Drawing.Point(133, 150);
			this.heartbeatTextBox.MaxLength = 8;
			this.heartbeatTextBox.Name = "heartbeatTextBox";
			this.heartbeatTextBox.Size = new System.Drawing.Size(110, 25);
			this.heartbeatTextBox.TabIndex = 1;
			this.heartbeatTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateLetterOrDigit_KeyPress);
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(16, 155);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(67, 15);
			this.label19.TabIndex = 0;
			this.label19.Text = "心跳包：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 113);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(90, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "总使用次数:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(298, 31);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "硬件地址:";
			// 
			// hardwareIDTextBox
			// 
			this.hardwareIDTextBox.Location = new System.Drawing.Point(391, 66);
			this.hardwareIDTextBox.MaxLength = 16;
			this.hardwareIDTextBox.Name = "hardwareIDTextBox";
			this.hardwareIDTextBox.Size = new System.Drawing.Size(142, 25);
			this.hardwareIDTextBox.TabIndex = 1;
			this.hardwareIDTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateDigit_KeyPress);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(298, 71);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(68, 15);
			this.label13.TabIndex = 0;
			this.label13.Text = "硬盘ID：";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(298, 155);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(106, 15);
			this.label18.TabIndex = 0;
			this.label18.Text = "心跳周期(s)：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 71);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 15);
			this.label2.TabIndex = 0;
			this.label2.Text = "硬盘标识：";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(298, 113);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(112, 15);
			this.label17.TabIndex = 0;
			this.label17.Text = "当前使用次数：";
			// 
			// deviceNameTextBox
			// 
			this.deviceNameTextBox.Location = new System.Drawing.Point(97, 24);
			this.deviceNameTextBox.MaxLength = 16;
			this.deviceNameTextBox.Name = "deviceNameTextBox";
			this.deviceNameTextBox.Size = new System.Drawing.Size(147, 25);
			this.deviceNameTextBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "主控标识：";
			// 
			// networkGroupBox
			// 
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
			this.networkGroupBox.Location = new System.Drawing.Point(-2, 245);
			this.networkGroupBox.Name = "networkGroupBox";
			this.networkGroupBox.Size = new System.Drawing.Size(588, 222);
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
			this.linkModeComboBox.Location = new System.Drawing.Point(136, 38);
			this.linkModeComboBox.Name = "linkModeComboBox";
			this.linkModeComboBox.Size = new System.Drawing.Size(106, 23);
			this.linkModeComboBox.TabIndex = 2;
			// 
			// linkPortTextBox
			// 
			this.linkPortTextBox.Location = new System.Drawing.Point(382, 86);
			this.linkPortTextBox.Name = "linkPortTextBox";
			this.linkPortTextBox.Size = new System.Drawing.Size(75, 25);
			this.linkPortTextBox.TabIndex = 1;
			this.linkPortTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateDigit_KeyPress);
			// 
			// gatewayTextBox
			// 
			this.gatewayTextBox.Location = new System.Drawing.Point(381, 131);
			this.gatewayTextBox.Name = "gatewayTextBox";
			this.gatewayTextBox.Size = new System.Drawing.Size(194, 25);
			this.gatewayTextBox.TabIndex = 1;
			this.gatewayTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateIP_KeyPress);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(306, 91);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(67, 15);
			this.label9.TabIndex = 0;
			this.label9.Text = "端口号：";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(306, 136);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(52, 15);
			this.label5.TabIndex = 0;
			this.label5.Text = "网关：";
			// 
			// macTextBox
			// 
			this.macTextBox.Location = new System.Drawing.Point(102, 176);
			this.macTextBox.Name = "macTextBox";
			this.macTextBox.Size = new System.Drawing.Size(193, 25);
			this.macTextBox.TabIndex = 1;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(18, 181);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(69, 15);
			this.label10.TabIndex = 0;
			this.label10.Text = "Mac地址:";
			// 
			// IPTextBox
			// 
			this.IPTextBox.Location = new System.Drawing.Point(102, 86);
			this.IPTextBox.Name = "IPTextBox";
			this.IPTextBox.Size = new System.Drawing.Size(193, 25);
			this.IPTextBox.TabIndex = 1;
			this.IPTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateIP_KeyPress);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(18, 91);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(61, 15);
			this.label6.TabIndex = 0;
			this.label6.Text = "IP地址:";
			// 
			// netmaskTextBox
			// 
			this.netmaskTextBox.Location = new System.Drawing.Point(101, 131);
			this.netmaskTextBox.Name = "netmaskTextBox";
			this.netmaskTextBox.Size = new System.Drawing.Size(194, 25);
			this.netmaskTextBox.TabIndex = 1;
			this.netmaskTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateIP_KeyPress);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(18, 136);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(82, 15);
			this.label7.TabIndex = 0;
			this.label7.Text = "子网掩码：";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(18, 42);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(112, 15);
			this.label8.TabIndex = 0;
			this.label8.Text = "网络连接类型：";
			// 
			// otherGroupBox
			// 
			this.otherGroupBox.Controls.Add(this.remotePortTextBox);
			this.otherGroupBox.Controls.Add(this.domainServerTextBox);
			this.otherGroupBox.Controls.Add(this.label11);
			this.otherGroupBox.Controls.Add(this.label12);
			this.otherGroupBox.Controls.Add(this.remoteHostTextBox);
			this.otherGroupBox.Controls.Add(this.label14);
			this.otherGroupBox.Controls.Add(this.domainNameTextBox);
			this.otherGroupBox.Controls.Add(this.label15);
			this.otherGroupBox.Location = new System.Drawing.Point(-2, 479);
			this.otherGroupBox.Name = "otherGroupBox";
			this.otherGroupBox.Size = new System.Drawing.Size(588, 153);
			this.otherGroupBox.TabIndex = 0;
			this.otherGroupBox.TabStop = false;
			this.otherGroupBox.Text = "其他设置";
			// 
			// remotePortTextBox
			// 
			this.remotePortTextBox.Location = new System.Drawing.Point(385, 40);
			this.remotePortTextBox.Name = "remotePortTextBox";
			this.remotePortTextBox.Size = new System.Drawing.Size(75, 25);
			this.remotePortTextBox.TabIndex = 1;
			this.remotePortTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateDigit_KeyPress);
			// 
			// domainServerTextBox
			// 
			this.domainServerTextBox.Location = new System.Drawing.Point(384, 86);
			this.domainServerTextBox.Name = "domainServerTextBox";
			this.domainServerTextBox.Size = new System.Drawing.Size(194, 25);
			this.domainServerTextBox.TabIndex = 1;
			this.domainServerTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateIP_KeyPress);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(308, 45);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(82, 15);
			this.label11.TabIndex = 0;
			this.label11.Text = "远端端口：";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(309, 91);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(83, 15);
			this.label12.TabIndex = 0;
			this.label12.Text = "服务器IP：";
			// 
			// remoteHostTextBox
			// 
			this.remoteHostTextBox.Location = new System.Drawing.Point(102, 40);
			this.remoteHostTextBox.Name = "remoteHostTextBox";
			this.remoteHostTextBox.Size = new System.Drawing.Size(193, 25);
			this.remoteHostTextBox.TabIndex = 1;
			this.remoteHostTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateIP_KeyPress);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(18, 45);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(68, 15);
			this.label14.TabIndex = 0;
			this.label14.Text = "远端IP：";
			// 
			// domainNameTextBox
			// 
			this.domainNameTextBox.Location = new System.Drawing.Point(101, 86);
			this.domainNameTextBox.MaxLength = 32;
			this.domainNameTextBox.Name = "domainNameTextBox";
			this.domainNameTextBox.Size = new System.Drawing.Size(194, 25);
			this.domainNameTextBox.TabIndex = 1;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(18, 91);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(82, 15);
			this.label15.TabIndex = 0;
			this.label15.Text = "服务器域名";
			// 
			// saveButton
			// 
			this.saveButton.BackColor = System.Drawing.Color.PeachPuff;
			this.saveButton.Location = new System.Drawing.Point(100, 649);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(93, 41);
			this.saveButton.TabIndex = 1;
			this.saveButton.Text = "保存";
			this.saveButton.UseVisualStyleBackColor = false;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// downloadButton
			// 
			this.downloadButton.BackColor = System.Drawing.Color.DarkSalmon;
			this.downloadButton.Location = new System.Drawing.Point(483, 649);
			this.downloadButton.Name = "downloadButton";
			this.downloadButton.Size = new System.Drawing.Size(93, 41);
			this.downloadButton.TabIndex = 1;
			this.downloadButton.Text = "下载";
			this.downloadButton.UseVisualStyleBackColor = false;
			this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
			// 
			// connectButton
			// 
			this.connectButton.BackColor = System.Drawing.Color.DarkSeaGreen;
			this.connectButton.Location = new System.Drawing.Point(379, 649);
			this.connectButton.Name = "connectButton";
			this.connectButton.Size = new System.Drawing.Size(93, 41);
			this.connectButton.TabIndex = 1;
			this.connectButton.Text = "连接";
			this.connectButton.UseVisualStyleBackColor = false;
			this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// playFlagComboBox
			// 
			this.playFlagComboBox.FormattingEnabled = true;
			this.playFlagComboBox.Items.AddRange(new object[] {
            "录播文件",
            "程序文件"});
			this.playFlagComboBox.Location = new System.Drawing.Point(424, 191);
			this.playFlagComboBox.Name = "playFlagComboBox";
			this.playFlagComboBox.Size = new System.Drawing.Size(106, 23);
			this.playFlagComboBox.TabIndex = 5;
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
			this.baudComboBox.Location = new System.Drawing.Point(134, 191);
			this.baudComboBox.Name = "baudComboBox";
			this.baudComboBox.Size = new System.Drawing.Size(106, 23);
			this.baudComboBox.TabIndex = 6;
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(298, 195);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(82, 15);
			this.label20.TabIndex = 3;
			this.label20.Text = "优先播放：";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(18, 195);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(67, 15);
			this.label16.TabIndex = 4;
			this.label16.Text = "波特率：";
			// 
			// HardwareSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(585, 705);
			this.Controls.Add(this.connectButton);
			this.Controls.Add(this.downloadButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.otherGroupBox);
			this.Controls.Add(this.networkGroupBox);
			this.Controls.Add(this.commonGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.ComboBox diskFlagComboBox;
		private System.Windows.Forms.NumericUpDown heartbeatCycleNumericUpDown;
		private System.Windows.Forms.NumericUpDown currUseTimeNumericUpDown;
		private System.Windows.Forms.NumericUpDown sumUseTimeNumericUpDown;
		private System.Windows.Forms.NumericUpDown addrNumericUpDown;
		private System.Windows.Forms.Button downloadButton;
		private System.Windows.Forms.Button connectButton;
		private System.Windows.Forms.ComboBox playFlagComboBox;
		private System.Windows.Forms.ComboBox baudComboBox;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label16;
	}
}