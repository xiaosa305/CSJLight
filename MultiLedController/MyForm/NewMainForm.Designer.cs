namespace MultiLedController.MyForm
{
	partial class NewMainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMainForm));
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.myStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.recordButton = new System.Windows.Forms.Button();
			this.debugButton = new System.Windows.Forms.Button();
			this.startButton = new System.Windows.Forms.Button();
			this.recordPathLabel = new System.Windows.Forms.Label();
			this.setFilePathButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.recordTextBox = new System.Windows.Forms.TextBox();
			this.plusButton = new System.Windows.Forms.Button();
			this.minusButton = new System.Windows.Forms.Button();
			this.recordFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.topPanel = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.refreshNetcardButton = new System.Windows.Forms.Button();
			this.netcardComboBox = new System.Windows.Forms.ComboBox();
			this.networkButton = new System.Windows.Forms.Button();
			this.controllerListView = new System.Windows.Forms.ListView();
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.virtualIPListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.netcardInfoGroupBox = new System.Windows.Forms.GroupBox();
			this.dhcpButton = new System.Windows.Forms.Button();
			this.refreshCurButton = new System.Windows.Forms.Button();
			this.dnsLabel2 = new System.Windows.Forms.Label();
			this.dnsLabel = new System.Windows.Forms.Label();
			this.gatewayLabel2 = new System.Windows.Forms.Label();
			this.gatewayLabel = new System.Windows.Forms.Label();
			this.submaskLabel2 = new System.Windows.Forms.Label();
			this.submaskLabel = new System.Windows.Forms.Label();
			this.ipLabel2 = new System.Windows.Forms.Label();
			this.ipLabel = new System.Windows.Forms.Label();
			this.searchButton = new System.Windows.Forms.Button();
			this.clearVIPButton = new System.Windows.Forms.Button();
			this.statusStrip.SuspendLayout();
			this.topPanel.SuspendLayout();
			this.netcardInfoGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel1,
            this.myStatusLabel2});
			this.statusStrip.Location = new System.Drawing.Point(0, 539);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(664, 22);
			this.statusStrip.SizingGrip = false;
			this.statusStrip.TabIndex = 32;
			this.statusStrip.Text = "statusStrip1";
			// 
			// myStatusLabel1
			// 
			this.myStatusLabel1.Name = "myStatusLabel1";
			this.myStatusLabel1.Size = new System.Drawing.Size(324, 17);
			this.myStatusLabel1.Spring = true;
			this.myStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// myStatusLabel2
			// 
			this.myStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.myStatusLabel2.Name = "myStatusLabel2";
			this.myStatusLabel2.Size = new System.Drawing.Size(324, 17);
			this.myStatusLabel2.Spring = true;
			this.myStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// recordButton
			// 
			this.recordButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.recordButton.Location = new System.Drawing.Point(562, 498);
			this.recordButton.Name = "recordButton";
			this.recordButton.Size = new System.Drawing.Size(88, 31);
			this.recordButton.TabIndex = 35;
			this.recordButton.Text = "录制";
			this.recordButton.UseVisualStyleBackColor = true;
			this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
			// 
			// debugButton
			// 
			this.debugButton.Enabled = false;
			this.debugButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.debugButton.Location = new System.Drawing.Point(106, 455);
			this.debugButton.Name = "debugButton";
			this.debugButton.Size = new System.Drawing.Size(76, 64);
			this.debugButton.TabIndex = 36;
			this.debugButton.Text = "开始调试";
			this.debugButton.UseVisualStyleBackColor = true;
			this.debugButton.Click += new System.EventHandler(this.debugButton_Click);
			// 
			// startButton
			// 
			this.startButton.BackColor = System.Drawing.Color.Coral;
			this.startButton.Enabled = false;
			this.startButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.startButton.Location = new System.Drawing.Point(12, 453);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(88, 68);
			this.startButton.TabIndex = 37;
			this.startButton.Text = "启动模拟";
			this.startButton.UseVisualStyleBackColor = false;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// recordPathLabel
			// 
			this.recordPathLabel.Location = new System.Drawing.Point(369, 460);
			this.recordPathLabel.Name = "recordPathLabel";
			this.recordPathLabel.Size = new System.Drawing.Size(282, 30);
			this.recordPathLabel.TabIndex = 39;
			// 
			// setFilePathButton
			// 
			this.setFilePathButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.setFilePathButton.Location = new System.Drawing.Point(265, 457);
			this.setFilePathButton.Name = "setFilePathButton";
			this.setFilePathButton.Size = new System.Drawing.Size(87, 31);
			this.setFilePathButton.TabIndex = 38;
			this.setFilePathButton.Text = "选择存放目录";
			this.setFilePathButton.UseVisualStyleBackColor = true;
			this.setFilePathButton.Click += new System.EventHandler(this.setFilePathButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(266, 511);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 12);
			this.label1.TabIndex = 40;
			this.label1.Text = "设置录制文件名： SC";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(433, 511);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 12);
			this.label3.TabIndex = 40;
			this.label3.Text = ".bin";
			// 
			// recordTextBox
			// 
			this.recordTextBox.Location = new System.Drawing.Point(391, 505);
			this.recordTextBox.MaxLength = 3;
			this.recordTextBox.Name = "recordTextBox";
			this.recordTextBox.Size = new System.Drawing.Size(39, 21);
			this.recordTextBox.TabIndex = 41;
			this.recordTextBox.Text = "000";
			this.recordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.recordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.recordTextBox_KeyPress);
			// 
			// plusButton
			// 
			this.plusButton.Location = new System.Drawing.Point(472, 498);
			this.plusButton.Name = "plusButton";
			this.plusButton.Size = new System.Drawing.Size(30, 31);
			this.plusButton.TabIndex = 42;
			this.plusButton.Text = "+";
			this.plusButton.UseVisualStyleBackColor = true;
			this.plusButton.Click += new System.EventHandler(this.plusButton_Click);
			// 
			// minusButton
			// 
			this.minusButton.Location = new System.Drawing.Point(505, 498);
			this.minusButton.Name = "minusButton";
			this.minusButton.Size = new System.Drawing.Size(30, 31);
			this.minusButton.TabIndex = 43;
			this.minusButton.Text = "-";
			this.minusButton.UseVisualStyleBackColor = true;
			this.minusButton.Click += new System.EventHandler(this.minusButton_Click);
			// 
			// recordFolderBrowserDialog
			// 
			this.recordFolderBrowserDialog.Description = "请选择录制文件存放目录，本程序将会在点击《录制》按钮之后，将录制文件保存在该目录下。";
			this.recordFolderBrowserDialog.SelectedPath = "C:\\Temp\\MultiLedFile";
			// 
			// topPanel
			// 
			this.topPanel.Controls.Add(this.label2);
			this.topPanel.Controls.Add(this.refreshNetcardButton);
			this.topPanel.Controls.Add(this.netcardComboBox);
			this.topPanel.Controls.Add(this.networkButton);
			this.topPanel.Controls.Add(this.controllerListView);
			this.topPanel.Controls.Add(this.virtualIPListView);
			this.topPanel.Controls.Add(this.netcardInfoGroupBox);
			this.topPanel.Controls.Add(this.searchButton);
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPanel.Location = new System.Drawing.Point(0, 0);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(664, 450);
			this.topPanel.TabIndex = 48;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(23, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 12);
			this.label2.TabIndex = 55;
			this.label2.Text = "网卡:";
			// 
			// refreshNetcardButton
			// 
			this.refreshNetcardButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.refreshNetcardButton.Location = new System.Drawing.Point(339, 17);
			this.refreshNetcardButton.Name = "refreshNetcardButton";
			this.refreshNetcardButton.Size = new System.Drawing.Size(62, 25);
			this.refreshNetcardButton.TabIndex = 53;
			this.refreshNetcardButton.Text = "刷新列表";
			this.refreshNetcardButton.UseVisualStyleBackColor = true;
			this.refreshNetcardButton.Click += new System.EventHandler(this.refreshNetcardButton_Click);
			// 
			// netcardComboBox
			// 
			this.netcardComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.netcardComboBox.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.netcardComboBox.FormattingEnabled = true;
			this.netcardComboBox.Location = new System.Drawing.Point(62, 20);
			this.netcardComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.netcardComboBox.Name = "netcardComboBox";
			this.netcardComboBox.Size = new System.Drawing.Size(270, 20);
			this.netcardComboBox.TabIndex = 54;
			this.netcardComboBox.SelectedIndexChanged += new System.EventHandler(this.netcardComboBox_SelectedIndexChanged);
			// 
			// networkButton
			// 
			this.networkButton.Location = new System.Drawing.Point(579, 12);
			this.networkButton.Name = "networkButton";
			this.networkButton.Size = new System.Drawing.Size(75, 25);
			this.networkButton.TabIndex = 52;
			this.networkButton.Text = "网络设置";
			this.networkButton.UseVisualStyleBackColor = true;
			this.networkButton.Click += new System.EventHandler(this.networkButton_Click);
			// 
			// controllerListView
			// 
			this.controllerListView.BackColor = System.Drawing.Color.MintCream;
			this.controllerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader6,
            this.columnHeader7});
			this.controllerListView.FullRowSelect = true;
			this.controllerListView.GridLines = true;
			this.controllerListView.HideSelection = false;
			this.controllerListView.Location = new System.Drawing.Point(13, 228);
			this.controllerListView.MultiSelect = false;
			this.controllerListView.Name = "controllerListView";
			this.controllerListView.Size = new System.Drawing.Size(394, 205);
			this.controllerListView.TabIndex = 51;
			this.controllerListView.UseCompatibleStateImageBehavior = false;
			this.controllerListView.View = System.Windows.Forms.View.Details;
			this.controllerListView.SelectedIndexChanged += new System.EventHandler(this.controllerListView_SelectedIndexChanged);
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "";
			this.columnHeader2.Width = 20;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "设备名称";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader3.Width = 165;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "设备mac";
			this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader6.Width = 150;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "路数";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader7.Width = 50;
			// 
			// virtualIPListView
			// 
			this.virtualIPListView.BackColor = System.Drawing.Color.LightBlue;
			this.virtualIPListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5});
			this.virtualIPListView.FullRowSelect = true;
			this.virtualIPListView.GridLines = true;
			this.virtualIPListView.HideSelection = false;
			this.virtualIPListView.Location = new System.Drawing.Point(423, 57);
			this.virtualIPListView.Name = "virtualIPListView";
			this.virtualIPListView.Size = new System.Drawing.Size(229, 377);
			this.virtualIPListView.TabIndex = 48;
			this.virtualIPListView.UseCompatibleStateImageBehavior = false;
			this.virtualIPListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "";
			this.columnHeader1.Width = 21;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "虚拟IP";
			this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader4.Width = 141;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "关联路数";
			this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// netcardInfoGroupBox
			// 
			this.netcardInfoGroupBox.Controls.Add(this.clearVIPButton);
			this.netcardInfoGroupBox.Controls.Add(this.dhcpButton);
			this.netcardInfoGroupBox.Controls.Add(this.refreshCurButton);
			this.netcardInfoGroupBox.Controls.Add(this.dnsLabel2);
			this.netcardInfoGroupBox.Controls.Add(this.dnsLabel);
			this.netcardInfoGroupBox.Controls.Add(this.gatewayLabel2);
			this.netcardInfoGroupBox.Controls.Add(this.gatewayLabel);
			this.netcardInfoGroupBox.Controls.Add(this.submaskLabel2);
			this.netcardInfoGroupBox.Controls.Add(this.submaskLabel);
			this.netcardInfoGroupBox.Controls.Add(this.ipLabel2);
			this.netcardInfoGroupBox.Controls.Add(this.ipLabel);
			this.netcardInfoGroupBox.Location = new System.Drawing.Point(17, 57);
			this.netcardInfoGroupBox.Name = "netcardInfoGroupBox";
			this.netcardInfoGroupBox.Size = new System.Drawing.Size(390, 114);
			this.netcardInfoGroupBox.TabIndex = 50;
			this.netcardInfoGroupBox.TabStop = false;
			this.netcardInfoGroupBox.Text = "选中网卡基本信息";
			// 
			// dhcpButton
			// 
			this.dhcpButton.Font = new System.Drawing.Font("黑体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dhcpButton.Location = new System.Drawing.Point(12, 81);
			this.dhcpButton.Name = "dhcpButton";
			this.dhcpButton.Size = new System.Drawing.Size(89, 20);
			this.dhcpButton.TabIndex = 27;
			this.dhcpButton.Text = "启用DHCP";
			this.dhcpButton.UseVisualStyleBackColor = true;
			this.dhcpButton.Click += new System.EventHandler(this.dhcpButton_Click);
			// 
			// refreshCurButton
			// 
			this.refreshCurButton.Font = new System.Drawing.Font("黑体", 8F);
			this.refreshCurButton.Location = new System.Drawing.Point(317, 29);
			this.refreshCurButton.Name = "refreshCurButton";
			this.refreshCurButton.Size = new System.Drawing.Size(66, 72);
			this.refreshCurButton.TabIndex = 27;
			this.refreshCurButton.Text = "刷新当前\r\n网卡信息";
			this.refreshCurButton.UseVisualStyleBackColor = true;
			this.refreshCurButton.Click += new System.EventHandler(this.refreshCurButton_Click);
			// 
			// dnsLabel2
			// 
			this.dnsLabel2.AutoSize = true;
			this.dnsLabel2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dnsLabel2.Location = new System.Drawing.Point(209, 53);
			this.dnsLabel2.Name = "dnsLabel2";
			this.dnsLabel2.Size = new System.Drawing.Size(11, 12);
			this.dnsLabel2.TabIndex = 0;
			this.dnsLabel2.Text = " ";
			// 
			// dnsLabel
			// 
			this.dnsLabel.AutoSize = true;
			this.dnsLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dnsLabel.Location = new System.Drawing.Point(173, 53);
			this.dnsLabel.Name = "dnsLabel";
			this.dnsLabel.Size = new System.Drawing.Size(35, 12);
			this.dnsLabel.TabIndex = 0;
			this.dnsLabel.Text = "DNS：";
			// 
			// gatewayLabel2
			// 
			this.gatewayLabel2.AutoSize = true;
			this.gatewayLabel2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.gatewayLabel2.Location = new System.Drawing.Point(49, 53);
			this.gatewayLabel2.Name = "gatewayLabel2";
			this.gatewayLabel2.Size = new System.Drawing.Size(29, 12);
			this.gatewayLabel2.TabIndex = 0;
			this.gatewayLabel2.Text = "    ";
			// 
			// gatewayLabel
			// 
			this.gatewayLabel.AutoSize = true;
			this.gatewayLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.gatewayLabel.Location = new System.Drawing.Point(12, 53);
			this.gatewayLabel.Name = "gatewayLabel";
			this.gatewayLabel.Size = new System.Drawing.Size(41, 12);
			this.gatewayLabel.TabIndex = 0;
			this.gatewayLabel.Text = "网关：";
			// 
			// submaskLabel2
			// 
			this.submaskLabel2.AutoSize = true;
			this.submaskLabel2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.submaskLabel2.Location = new System.Drawing.Point(208, 29);
			this.submaskLabel2.Name = "submaskLabel2";
			this.submaskLabel2.Size = new System.Drawing.Size(23, 12);
			this.submaskLabel2.TabIndex = 0;
			this.submaskLabel2.Text = "   ";
			// 
			// submaskLabel
			// 
			this.submaskLabel.AutoSize = true;
			this.submaskLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.submaskLabel.Location = new System.Drawing.Point(170, 29);
			this.submaskLabel.Name = "submaskLabel";
			this.submaskLabel.Size = new System.Drawing.Size(41, 12);
			this.submaskLabel.TabIndex = 0;
			this.submaskLabel.Text = "掩码：";
			// 
			// ipLabel2
			// 
			this.ipLabel2.AutoSize = true;
			this.ipLabel2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ipLabel2.Location = new System.Drawing.Point(49, 29);
			this.ipLabel2.Name = "ipLabel2";
			this.ipLabel2.Size = new System.Drawing.Size(23, 12);
			this.ipLabel2.TabIndex = 0;
			this.ipLabel2.Text = "   ";
			// 
			// ipLabel
			// 
			this.ipLabel.AutoSize = true;
			this.ipLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ipLabel.Location = new System.Drawing.Point(12, 29);
			this.ipLabel.Name = "ipLabel";
			this.ipLabel.Size = new System.Drawing.Size(29, 12);
			this.ipLabel.TabIndex = 0;
			this.ipLabel.Text = "IP：";
			// 
			// searchButton
			// 
			this.searchButton.Enabled = false;
			this.searchButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.searchButton.Location = new System.Drawing.Point(13, 184);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(88, 31);
			this.searchButton.TabIndex = 49;
			this.searchButton.Text = "搜索设备";
			this.searchButton.UseVisualStyleBackColor = true;
			this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// clearVIPButton
			// 
			this.clearVIPButton.Font = new System.Drawing.Font("黑体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.clearVIPButton.Location = new System.Drawing.Point(170, 81);
			this.clearVIPButton.Name = "clearVIPButton";
			this.clearVIPButton.Size = new System.Drawing.Size(89, 20);
			this.clearVIPButton.TabIndex = 27;
			this.clearVIPButton.Text = "清空虚拟IP";
			this.clearVIPButton.UseVisualStyleBackColor = true;
			this.clearVIPButton.Click += new System.EventHandler(this.clearVIPButton_Click);
			// 
			// NewMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(664, 561);
			this.Controls.Add(this.topPanel);
			this.Controls.Add(this.minusButton);
			this.Controls.Add(this.plusButton);
			this.Controls.Add(this.recordTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.recordPathLabel);
			this.Controls.Add(this.setFilePathButton);
			this.Controls.Add(this.recordButton);
			this.Controls.Add(this.debugButton);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.statusStrip);
			this.ForeColor = System.Drawing.SystemColors.InfoText;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(680, 600);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(680, 600);
			this.Name = "NewMainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "幻彩灯带控制器";
			this.Activated += new System.EventHandler(this.NewMainForm_Activated);
			this.Load += new System.EventHandler(this.NewMainForm_Load);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.topPanel.ResumeLayout(false);
			this.topPanel.PerformLayout();
			this.netcardInfoGroupBox.ResumeLayout(false);
			this.netcardInfoGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel1;
		private System.Windows.Forms.Button recordButton;
		private System.Windows.Forms.Button debugButton;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Label recordPathLabel;
		private System.Windows.Forms.Button setFilePathButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox recordTextBox;
		private System.Windows.Forms.Button plusButton;
		private System.Windows.Forms.Button minusButton;
		private System.Windows.Forms.FolderBrowserDialog recordFolderBrowserDialog;
		private System.Windows.Forms.ToolTip myToolTip;
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button refreshNetcardButton;
		private System.Windows.Forms.ComboBox netcardComboBox;
		private System.Windows.Forms.Button networkButton;
		private System.Windows.Forms.ListView controllerListView;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ListView virtualIPListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.GroupBox netcardInfoGroupBox;
		private System.Windows.Forms.Button dhcpButton;
		private System.Windows.Forms.Button refreshCurButton;
		private System.Windows.Forms.Label dnsLabel2;
		private System.Windows.Forms.Label dnsLabel;
		private System.Windows.Forms.Label gatewayLabel2;
		private System.Windows.Forms.Label gatewayLabel;
		private System.Windows.Forms.Label submaskLabel2;
		private System.Windows.Forms.Label submaskLabel;
		private System.Windows.Forms.Label ipLabel2;
		private System.Windows.Forms.Label ipLabel;
		private System.Windows.Forms.Button searchButton;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel2;
		private System.Windows.Forms.Button clearVIPButton;
	}
}