namespace MultiLedController
{
	partial class NetworkForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.netcardComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.refreshNetcardButton = new System.Windows.Forms.Button();
			this.getNetworkButton = new System.Windows.Forms.Button();
			this.finalNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.set9IPButton = new System.Windows.Forms.Button();
			this.dhcpButton = new System.Windows.Forms.Button();
			this.myStatusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dnsLabel2 = new System.Windows.Forms.Label();
			this.dnsLabel = new System.Windows.Forms.Label();
			this.gatewayLabel2 = new System.Windows.Forms.Label();
			this.gatewayLabel = new System.Windows.Forms.Label();
			this.submaskLabel2 = new System.Windows.Forms.Label();
			this.submaskLabel = new System.Windows.Forms.Label();
			this.ipLabel2 = new System.Windows.Forms.Label();
			this.ipLabel = new System.Windows.Forms.Label();
			this.saveButton = new System.Windows.Forms.Button();
			this.loadButton = new System.Windows.Forms.Button();
			this.testButton = new System.Windows.Forms.Button();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.addVirtualIpButton = new System.Windows.Forms.Button();
			this.countNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.thirdNumericUpDown = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.finalNumericUpDown)).BeginInit();
			this.myStatusStrip.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.countNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.thirdNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// netcardComboBox
			// 
			this.netcardComboBox.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.netcardComboBox.FormattingEnabled = true;
			this.netcardComboBox.Location = new System.Drawing.Point(73, 18);
			this.netcardComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.netcardComboBox.Name = "netcardComboBox";
			this.netcardComboBox.Size = new System.Drawing.Size(283, 20);
			this.netcardComboBox.TabIndex = 24;
			this.netcardComboBox.SelectedIndexChanged += new System.EventHandler(this.ipComboBox_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(14, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 26;
			this.label2.Text = "网卡列表";
			// 
			// refreshNetcardButton
			// 
			this.refreshNetcardButton.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.refreshNetcardButton.Location = new System.Drawing.Point(374, 18);
			this.refreshNetcardButton.Name = "refreshNetcardButton";
			this.refreshNetcardButton.Size = new System.Drawing.Size(43, 20);
			this.refreshNetcardButton.TabIndex = 0;
			this.refreshNetcardButton.Text = "刷新";
			this.refreshNetcardButton.UseVisualStyleBackColor = true;
			this.refreshNetcardButton.Click += new System.EventHandler(this.refreshNetcardButton_Click);
			// 
			// getNetworkButton
			// 
			this.getNetworkButton.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.getNetworkButton.Location = new System.Drawing.Point(323, 18);
			this.getNetworkButton.Name = "getNetworkButton";
			this.getNetworkButton.Size = new System.Drawing.Size(43, 20);
			this.getNetworkButton.TabIndex = 0;
			this.getNetworkButton.Text = "刷新";
			this.getNetworkButton.UseVisualStyleBackColor = true;
			this.getNetworkButton.Click += new System.EventHandler(this.refreshNetcardButton_Click);
			// 
			// finalNumericUpDown
			// 
			this.finalNumericUpDown.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.finalNumericUpDown.Location = new System.Drawing.Point(371, 61);
			this.finalNumericUpDown.Maximum = new decimal(new int[] {
            246,
            0,
            0,
            0});
			this.finalNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.finalNumericUpDown.Name = "finalNumericUpDown";
			this.finalNumericUpDown.Size = new System.Drawing.Size(45, 19);
			this.finalNumericUpDown.TabIndex = 27;
			this.finalNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.finalNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(242, 51);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 24);
			this.label1.TabIndex = 28;
			this.label1.Text = "起始地址：\r\n    192.168.\r\n";
			// 
			// set9IPButton
			// 
			this.set9IPButton.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.set9IPButton.Location = new System.Drawing.Point(241, 118);
			this.set9IPButton.Name = "set9IPButton";
			this.set9IPButton.Size = new System.Drawing.Size(179, 20);
			this.set9IPButton.TabIndex = 0;
			this.set9IPButton.Text = "设置连续IP地址";
			this.set9IPButton.UseVisualStyleBackColor = true;
			this.set9IPButton.Click += new System.EventHandler(this.set9IPButton_Click);
			// 
			// dhcpButton
			// 
			this.dhcpButton.Font = new System.Drawing.Font("黑体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dhcpButton.Location = new System.Drawing.Point(172, 213);
			this.dhcpButton.Name = "dhcpButton";
			this.dhcpButton.Size = new System.Drawing.Size(54, 20);
			this.dhcpButton.TabIndex = 0;
			this.dhcpButton.Text = "启用DHCP";
			this.dhcpButton.UseVisualStyleBackColor = true;
			this.dhcpButton.Click += new System.EventHandler(this.dhcpButton_Click);
			// 
			// myStatusStrip
			// 
			this.myStatusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.myStatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.myStatusStrip.Location = new System.Drawing.Point(0, 257);
			this.myStatusStrip.Name = "myStatusStrip";
			this.myStatusStrip.Size = new System.Drawing.Size(439, 22);
			this.myStatusStrip.SizingGrip = false;
			this.myStatusStrip.TabIndex = 29;
			this.myStatusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(424, 17);
			this.toolStripStatusLabel1.Spring = true;
			this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dnsLabel2);
			this.groupBox1.Controls.Add(this.dnsLabel);
			this.groupBox1.Controls.Add(this.gatewayLabel2);
			this.groupBox1.Controls.Add(this.gatewayLabel);
			this.groupBox1.Controls.Add(this.submaskLabel2);
			this.groupBox1.Controls.Add(this.submaskLabel);
			this.groupBox1.Controls.Add(this.ipLabel2);
			this.groupBox1.Controls.Add(this.ipLabel);
			this.groupBox1.Location = new System.Drawing.Point(14, 61);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(211, 141);
			this.groupBox1.TabIndex = 30;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "初始设置";
			// 
			// dnsLabel2
			// 
			this.dnsLabel2.AutoSize = true;
			this.dnsLabel2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dnsLabel2.Location = new System.Drawing.Point(68, 109);
			this.dnsLabel2.Name = "dnsLabel2";
			this.dnsLabel2.Size = new System.Drawing.Size(11, 12);
			this.dnsLabel2.TabIndex = 0;
			this.dnsLabel2.Text = " ";
			// 
			// dnsLabel
			// 
			this.dnsLabel.AutoSize = true;
			this.dnsLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dnsLabel.Location = new System.Drawing.Point(18, 109);
			this.dnsLabel.Name = "dnsLabel";
			this.dnsLabel.Size = new System.Drawing.Size(35, 12);
			this.dnsLabel.TabIndex = 0;
			this.dnsLabel.Text = "DNS：";
			// 
			// gatewayLabel2
			// 
			this.gatewayLabel2.AutoSize = true;
			this.gatewayLabel2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.gatewayLabel2.Location = new System.Drawing.Point(68, 85);
			this.gatewayLabel2.Name = "gatewayLabel2";
			this.gatewayLabel2.Size = new System.Drawing.Size(11, 12);
			this.gatewayLabel2.TabIndex = 0;
			this.gatewayLabel2.Text = " ";
			// 
			// gatewayLabel
			// 
			this.gatewayLabel.AutoSize = true;
			this.gatewayLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.gatewayLabel.Location = new System.Drawing.Point(15, 85);
			this.gatewayLabel.Name = "gatewayLabel";
			this.gatewayLabel.Size = new System.Drawing.Size(41, 12);
			this.gatewayLabel.TabIndex = 0;
			this.gatewayLabel.Text = "网关：";
			// 
			// submaskLabel2
			// 
			this.submaskLabel2.AutoSize = true;
			this.submaskLabel2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.submaskLabel2.Location = new System.Drawing.Point(68, 61);
			this.submaskLabel2.Name = "submaskLabel2";
			this.submaskLabel2.Size = new System.Drawing.Size(11, 12);
			this.submaskLabel2.TabIndex = 0;
			this.submaskLabel2.Text = " ";
			// 
			// submaskLabel
			// 
			this.submaskLabel.AutoSize = true;
			this.submaskLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.submaskLabel.Location = new System.Drawing.Point(15, 61);
			this.submaskLabel.Name = "submaskLabel";
			this.submaskLabel.Size = new System.Drawing.Size(41, 12);
			this.submaskLabel.TabIndex = 0;
			this.submaskLabel.Text = "掩码：";
			// 
			// ipLabel2
			// 
			this.ipLabel2.AutoSize = true;
			this.ipLabel2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ipLabel2.Location = new System.Drawing.Point(68, 37);
			this.ipLabel2.Name = "ipLabel2";
			this.ipLabel2.Size = new System.Drawing.Size(11, 12);
			this.ipLabel2.TabIndex = 0;
			this.ipLabel2.Text = " ";
			// 
			// ipLabel
			// 
			this.ipLabel.AutoSize = true;
			this.ipLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ipLabel.Location = new System.Drawing.Point(21, 37);
			this.ipLabel.Name = "ipLabel";
			this.ipLabel.Size = new System.Drawing.Size(29, 12);
			this.ipLabel.TabIndex = 0;
			this.ipLabel.Text = "IP：";
			// 
			// saveButton
			// 
			this.saveButton.Font = new System.Drawing.Font("黑体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.saveButton.Location = new System.Drawing.Point(16, 213);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(54, 20);
			this.saveButton.TabIndex = 1;
			this.saveButton.Text = "存储配置";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// loadButton
			// 
			this.loadButton.Enabled = false;
			this.loadButton.Font = new System.Drawing.Font("黑体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.loadButton.Location = new System.Drawing.Point(79, 213);
			this.loadButton.Name = "loadButton";
			this.loadButton.Size = new System.Drawing.Size(54, 20);
			this.loadButton.TabIndex = 0;
			this.loadButton.Text = "恢复配置";
			this.loadButton.UseVisualStyleBackColor = true;
			this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
			// 
			// testButton
			// 
			this.testButton.Location = new System.Drawing.Point(345, 165);
			this.testButton.Name = "testButton";
			this.testButton.Size = new System.Drawing.Size(75, 23);
			this.testButton.TabIndex = 31;
			this.testButton.Text = "BigTest";
			this.testButton.UseVisualStyleBackColor = true;
			this.testButton.Click += new System.EventHandler(this.testButton_Click);
			// 
			// addVirtualIpButton
			// 
			this.addVirtualIpButton.Enabled = false;
			this.addVirtualIpButton.Font = new System.Drawing.Font("黑体", 9F);
			this.addVirtualIpButton.Location = new System.Drawing.Point(243, 213);
			this.addVirtualIpButton.Name = "addVirtualIpButton";
			this.addVirtualIpButton.Size = new System.Drawing.Size(177, 20);
			this.addVirtualIpButton.TabIndex = 31;
			this.addVirtualIpButton.Text = "添加虚拟IP到主界面";
			this.addVirtualIpButton.UseVisualStyleBackColor = true;
			this.addVirtualIpButton.Click += new System.EventHandler(this.addVirtualIpButton_Click);
			// 
			// countNumericUpDown
			// 
			this.countNumericUpDown.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.countNumericUpDown.Location = new System.Drawing.Point(370, 90);
			this.countNumericUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.countNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.countNumericUpDown.Name = "countNumericUpDown";
			this.countNumericUpDown.Size = new System.Drawing.Size(45, 19);
			this.countNumericUpDown.TabIndex = 27;
			this.countNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.countNumericUpDown.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
			this.countNumericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(241, 91);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89, 12);
			this.label3.TabIndex = 28;
			this.label3.Text = "连续IP的数量：";
			// 
			// thirdNumericUpDown3
			// 
			this.thirdNumericUpDown.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.thirdNumericUpDown.Location = new System.Drawing.Point(320, 61);
			this.thirdNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.thirdNumericUpDown.Name = "thirdNumericUpDown3";
			this.thirdNumericUpDown.Size = new System.Drawing.Size(45, 19);
			this.thirdNumericUpDown.TabIndex = 27;
			this.thirdNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.thirdNumericUpDown.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
			// 
			// NetworkForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.ClientSize = new System.Drawing.Size(439, 279);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.addVirtualIpButton);
			this.Controls.Add(this.testButton);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.myStatusStrip);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.countNumericUpDown);
			this.Controls.Add(this.thirdNumericUpDown);
			this.Controls.Add(this.finalNumericUpDown);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.netcardComboBox);
			this.Controls.Add(this.loadButton);
			this.Controls.Add(this.dhcpButton);
			this.Controls.Add(this.set9IPButton);
			this.Controls.Add(this.refreshNetcardButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NetworkForm";
			this.Text = "网络设置";
			this.Load += new System.EventHandler(this.NetworkForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.finalNumericUpDown)).EndInit();
			this.myStatusStrip.ResumeLayout(false);
			this.myStatusStrip.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.countNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.thirdNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ComboBox netcardComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button refreshNetcardButton;
		private System.Windows.Forms.Button getNetworkButton;
		private System.Windows.Forms.NumericUpDown finalNumericUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button set9IPButton;
		private System.Windows.Forms.Button dhcpButton;
		private System.Windows.Forms.StatusStrip myStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label dnsLabel;
		private System.Windows.Forms.Label gatewayLabel;
		private System.Windows.Forms.Label submaskLabel;
		private System.Windows.Forms.Label ipLabel;
		private System.Windows.Forms.Button testButton;
		private System.Windows.Forms.Label dnsLabel2;
		private System.Windows.Forms.Label gatewayLabel2;
		private System.Windows.Forms.Label submaskLabel2;
		private System.Windows.Forms.Label ipLabel2;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button loadButton;
		private System.Windows.Forms.ToolTip myToolTip;
		private System.Windows.Forms.Button addVirtualIpButton;
		private System.Windows.Forms.NumericUpDown countNumericUpDown;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown thirdNumericUpDown;
	}
}

