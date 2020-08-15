﻿namespace MultiLedController.MyForm
{
	partial class MainForm3
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
			this.netcardComboBox = new System.Windows.Forms.ComboBox();
			this.refreshNetcardButton = new System.Windows.Forms.Button();
			this.netcardInfoGroupBox = new System.Windows.Forms.GroupBox();
			this.clearVIPButton = new System.Windows.Forms.Button();
			this.dhcpButton = new System.Windows.Forms.Button();
			this.dnsLabel2 = new System.Windows.Forms.Label();
			this.dnsLabel = new System.Windows.Forms.Label();
			this.gatewayLabel2 = new System.Windows.Forms.Label();
			this.refreshNetcardInfoButton = new System.Windows.Forms.Button();
			this.gatewayLabel = new System.Windows.Forms.Label();
			this.submaskLabel2 = new System.Windows.Forms.Label();
			this.submaskLabel = new System.Windows.Forms.Label();
			this.ipLabel2 = new System.Windows.Forms.Label();
			this.ipLabel = new System.Windows.Forms.Label();
			this.virtualIPListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.simulatePanel = new System.Windows.Forms.Panel();
			this.recordButton = new System.Windows.Forms.Button();
			this.debugButton = new System.Windows.Forms.Button();
			this.setFilePathButton = new System.Windows.Forms.Button();
			this.startButton = new System.Windows.Forms.Button();
			this.recordPathLabel = new System.Windows.Forms.Label();
			this.minusButton = new System.Windows.Forms.Button();
			this.nameLabel = new System.Windows.Forms.Label();
			this.plusButton = new System.Windows.Forms.Button();
			this.binLabel = new System.Windows.Forms.Label();
			this.recordTextBox = new System.Windows.Forms.TextBox();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.myStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.controllerCountNUD = new System.Windows.Forms.NumericUpDown();
			this.spaceCountComboBox = new System.Windows.Forms.ComboBox();
			this.interfaceCountComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.netcardInfoGroupBox.SuspendLayout();
			this.simulatePanel.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.controllerCountNUD)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// netcardComboBox
			// 
			this.netcardComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.netcardComboBox.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.netcardComboBox.FormattingEnabled = true;
			this.netcardComboBox.Location = new System.Drawing.Point(14, 12);
			this.netcardComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.netcardComboBox.Name = "netcardComboBox";
			this.netcardComboBox.Size = new System.Drawing.Size(356, 20);
			this.netcardComboBox.TabIndex = 61;
			this.netcardComboBox.SelectedIndexChanged += new System.EventHandler(this.netcardComboBox_SelectedIndexChanged);
			// 
			// refreshNetcardButton
			// 
			this.refreshNetcardButton.Font = new System.Drawing.Font("黑体", 8F);
			this.refreshNetcardButton.Location = new System.Drawing.Point(14, 51);
			this.refreshNetcardButton.Name = "refreshNetcardButton";
			this.refreshNetcardButton.Size = new System.Drawing.Size(99, 24);
			this.refreshNetcardButton.TabIndex = 60;
			this.refreshNetcardButton.Text = "刷新网卡列表";
			this.refreshNetcardButton.UseVisualStyleBackColor = true;
			this.refreshNetcardButton.Click += new System.EventHandler(this.refreshNetcardButton_Click);
			// 
			// netcardInfoGroupBox
			// 
			this.netcardInfoGroupBox.Controls.Add(this.clearVIPButton);
			this.netcardInfoGroupBox.Controls.Add(this.dhcpButton);
			this.netcardInfoGroupBox.Controls.Add(this.dnsLabel2);
			this.netcardInfoGroupBox.Controls.Add(this.dnsLabel);
			this.netcardInfoGroupBox.Controls.Add(this.gatewayLabel2);
			this.netcardInfoGroupBox.Controls.Add(this.refreshNetcardInfoButton);
			this.netcardInfoGroupBox.Controls.Add(this.gatewayLabel);
			this.netcardInfoGroupBox.Controls.Add(this.submaskLabel2);
			this.netcardInfoGroupBox.Controls.Add(this.submaskLabel);
			this.netcardInfoGroupBox.Controls.Add(this.ipLabel2);
			this.netcardInfoGroupBox.Controls.Add(this.ipLabel);
			this.netcardInfoGroupBox.Enabled = false;
			this.netcardInfoGroupBox.Location = new System.Drawing.Point(14, 93);
			this.netcardInfoGroupBox.Name = "netcardInfoGroupBox";
			this.netcardInfoGroupBox.Size = new System.Drawing.Size(356, 112);
			this.netcardInfoGroupBox.TabIndex = 62;
			this.netcardInfoGroupBox.TabStop = false;
			this.netcardInfoGroupBox.Text = "选中网卡基本信息";
			// 
			// clearVIPButton
			// 
			this.clearVIPButton.Font = new System.Drawing.Font("黑体", 8F);
			this.clearVIPButton.Location = new System.Drawing.Point(259, 81);
			this.clearVIPButton.Name = "clearVIPButton";
			this.clearVIPButton.Size = new System.Drawing.Size(78, 20);
			this.clearVIPButton.TabIndex = 60;
			this.clearVIPButton.Text = "清空虚拟IP";
			this.clearVIPButton.UseVisualStyleBackColor = true;
			// 
			// dhcpButton
			// 
			this.dhcpButton.Font = new System.Drawing.Font("黑体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dhcpButton.Location = new System.Drawing.Point(170, 81);
			this.dhcpButton.Name = "dhcpButton";
			this.dhcpButton.Size = new System.Drawing.Size(78, 20);
			this.dhcpButton.TabIndex = 27;
			this.dhcpButton.Text = "启用DHCP";
			this.dhcpButton.UseVisualStyleBackColor = true;
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
			this.dnsLabel.Location = new System.Drawing.Point(174, 53);
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
			// refreshNetcardInfoButton
			// 
			this.refreshNetcardInfoButton.Font = new System.Drawing.Font("黑体", 8F);
			this.refreshNetcardInfoButton.Location = new System.Drawing.Point(14, 81);
			this.refreshNetcardInfoButton.Name = "refreshNetcardInfoButton";
			this.refreshNetcardInfoButton.Size = new System.Drawing.Size(85, 20);
			this.refreshNetcardInfoButton.TabIndex = 27;
			this.refreshNetcardInfoButton.Text = "刷新网卡信息";
			this.refreshNetcardInfoButton.UseVisualStyleBackColor = true;
			// 
			// gatewayLabel
			// 
			this.gatewayLabel.AutoSize = true;
			this.gatewayLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.gatewayLabel.Location = new System.Drawing.Point(14, 53);
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
			this.ipLabel.Location = new System.Drawing.Point(16, 29);
			this.ipLabel.Name = "ipLabel";
			this.ipLabel.Size = new System.Drawing.Size(29, 12);
			this.ipLabel.TabIndex = 0;
			this.ipLabel.Text = "IP：";
			// 
			// virtualIPListView
			// 
			this.virtualIPListView.BackColor = System.Drawing.Color.LightBlue;
			this.virtualIPListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5});
			this.virtualIPListView.Dock = System.Windows.Forms.DockStyle.Right;
			this.virtualIPListView.FullRowSelect = true;
			this.virtualIPListView.GridLines = true;
			this.virtualIPListView.HideSelection = false;
			this.virtualIPListView.Location = new System.Drawing.Point(396, 0);
			this.virtualIPListView.Name = "virtualIPListView";
			this.virtualIPListView.Size = new System.Drawing.Size(319, 445);
			this.virtualIPListView.TabIndex = 63;
			this.virtualIPListView.UseCompatibleStateImageBehavior = false;
			this.virtualIPListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "";
			this.columnHeader1.Width = 45;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "虚拟IP";
			this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader4.Width = 170;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "关联路数";
			this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader5.Width = 79;
			// 
			// simulatePanel
			// 
			this.simulatePanel.Controls.Add(this.recordButton);
			this.simulatePanel.Controls.Add(this.debugButton);
			this.simulatePanel.Controls.Add(this.setFilePathButton);
			this.simulatePanel.Controls.Add(this.startButton);
			this.simulatePanel.Controls.Add(this.recordPathLabel);
			this.simulatePanel.Controls.Add(this.minusButton);
			this.simulatePanel.Controls.Add(this.nameLabel);
			this.simulatePanel.Controls.Add(this.plusButton);
			this.simulatePanel.Controls.Add(this.binLabel);
			this.simulatePanel.Controls.Add(this.recordTextBox);
			this.simulatePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.simulatePanel.Location = new System.Drawing.Point(0, 445);
			this.simulatePanel.Name = "simulatePanel";
			this.simulatePanel.Size = new System.Drawing.Size(715, 109);
			this.simulatePanel.TabIndex = 76;
			// 
			// recordButton
			// 
			this.recordButton.BackColor = System.Drawing.Color.Transparent;
			this.recordButton.Enabled = false;
			this.recordButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.recordButton.Location = new System.Drawing.Point(632, 19);
			this.recordButton.Name = "recordButton";
			this.recordButton.Size = new System.Drawing.Size(76, 72);
			this.recordButton.TabIndex = 65;
			this.recordButton.Text = "录制数据";
			this.recordButton.UseVisualStyleBackColor = false;
			// 
			// debugButton
			// 
			this.debugButton.Enabled = false;
			this.debugButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.debugButton.Location = new System.Drawing.Point(103, 19);
			this.debugButton.Name = "debugButton";
			this.debugButton.Size = new System.Drawing.Size(76, 72);
			this.debugButton.TabIndex = 73;
			this.debugButton.Text = "开始调试";
			this.debugButton.UseVisualStyleBackColor = true;
			// 
			// setFilePathButton
			// 
			this.setFilePathButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.setFilePathButton.Location = new System.Drawing.Point(244, 20);
			this.setFilePathButton.Name = "setFilePathButton";
			this.setFilePathButton.Size = new System.Drawing.Size(87, 39);
			this.setFilePathButton.TabIndex = 66;
			this.setFilePathButton.Text = "选择存放目录";
			this.setFilePathButton.UseVisualStyleBackColor = true;
			// 
			// startButton
			// 
			this.startButton.BackColor = System.Drawing.Color.Coral;
			this.startButton.Enabled = false;
			this.startButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.startButton.Location = new System.Drawing.Point(18, 19);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(76, 72);
			this.startButton.TabIndex = 74;
			this.startButton.Text = "启动模拟";
			this.startButton.UseVisualStyleBackColor = false;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// recordPathLabel
			// 
			this.recordPathLabel.Location = new System.Drawing.Point(339, 24);
			this.recordPathLabel.Name = "recordPathLabel";
			this.recordPathLabel.Size = new System.Drawing.Size(249, 30);
			this.recordPathLabel.TabIndex = 67;
			this.recordPathLabel.Text = "请选择工程存放目录，程序会自动为您创建子文件夹，用以区分不同的控制器。";
			this.recordPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// minusButton
			// 
			this.minusButton.Location = new System.Drawing.Point(561, 59);
			this.minusButton.Name = "minusButton";
			this.minusButton.Size = new System.Drawing.Size(30, 31);
			this.minusButton.TabIndex = 72;
			this.minusButton.Text = "-";
			this.minusButton.UseVisualStyleBackColor = true;
			this.minusButton.Click += new System.EventHandler(this.minusButton_Click);
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(246, 75);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(119, 12);
			this.nameLabel.TabIndex = 69;
			this.nameLabel.Text = "设置录制文件名： SC";
			// 
			// plusButton
			// 
			this.plusButton.Location = new System.Drawing.Point(528, 59);
			this.plusButton.Name = "plusButton";
			this.plusButton.Size = new System.Drawing.Size(30, 31);
			this.plusButton.TabIndex = 71;
			this.plusButton.Text = "+";
			this.plusButton.UseVisualStyleBackColor = true;
			this.plusButton.Click += new System.EventHandler(this.plusButton_Click);
			// 
			// binLabel
			// 
			this.binLabel.AutoSize = true;
			this.binLabel.Location = new System.Drawing.Point(413, 75);
			this.binLabel.Name = "binLabel";
			this.binLabel.Size = new System.Drawing.Size(29, 12);
			this.binLabel.TabIndex = 68;
			this.binLabel.Text = ".bin";
			// 
			// recordTextBox
			// 
			this.recordTextBox.Location = new System.Drawing.Point(371, 69);
			this.recordTextBox.MaxLength = 3;
			this.recordTextBox.Name = "recordTextBox";
			this.recordTextBox.Size = new System.Drawing.Size(39, 21);
			this.recordTextBox.TabIndex = 70;
			this.recordTextBox.Text = "000";
			this.recordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel1,
            this.myStatusLabel2});
			this.statusStrip.Location = new System.Drawing.Point(0, 554);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(715, 26);
			this.statusStrip.SizingGrip = false;
			this.statusStrip.TabIndex = 77;
			this.statusStrip.Text = "statusStrip1";
			// 
			// myStatusLabel1
			// 
			this.myStatusLabel1.Name = "myStatusLabel1";
			this.myStatusLabel1.Size = new System.Drawing.Size(350, 21);
			this.myStatusLabel1.Spring = true;
			this.myStatusLabel1.Text = "   ";
			this.myStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// myStatusLabel2
			// 
			this.myStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.myStatusLabel2.Name = "myStatusLabel2";
			this.myStatusLabel2.Size = new System.Drawing.Size(350, 21);
			this.myStatusLabel2.Spring = true;
			this.myStatusLabel2.Text = "  ";
			this.myStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.controllerCountNUD);
			this.panel1.Controls.Add(this.spaceCountComboBox);
			this.panel1.Controls.Add(this.interfaceCountComboBox);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(14, 241);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(356, 155);
			this.panel1.TabIndex = 78;
			// 
			// controllerCountNUD
			// 
			this.controllerCountNUD.Location = new System.Drawing.Point(187, 113);
			this.controllerCountNUD.Maximum = new decimal(new int[] {
            21,
            0,
            0,
            0});
			this.controllerCountNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.controllerCountNUD.Name = "controllerCountNUD";
			this.controllerCountNUD.Size = new System.Drawing.Size(61, 21);
			this.controllerCountNUD.TabIndex = 4;
			this.controllerCountNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.controllerCountNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// spaceCountComboBox
			// 
			this.spaceCountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.spaceCountComboBox.FormattingEnabled = true;
			this.spaceCountComboBox.Items.AddRange(new object[] {
            "1020",
            "850",
            "680",
            "520"});
			this.spaceCountComboBox.Location = new System.Drawing.Point(187, 68);
			this.spaceCountComboBox.Name = "spaceCountComboBox";
			this.spaceCountComboBox.Size = new System.Drawing.Size(61, 20);
			this.spaceCountComboBox.TabIndex = 3;
			this.spaceCountComboBox.SelectedIndexChanged += new System.EventHandler(this.countComboBox_SelectedIndexChanged);
			// 
			// interfaceCountComboBox
			// 
			this.interfaceCountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.interfaceCountComboBox.FormattingEnabled = true;
			this.interfaceCountComboBox.Items.AddRange(new object[] {
            "4",
            "8"});
			this.interfaceCountComboBox.Location = new System.Drawing.Point(187, 23);
			this.interfaceCountComboBox.Name = "interfaceCountComboBox";
			this.interfaceCountComboBox.Size = new System.Drawing.Size(61, 20);
			this.interfaceCountComboBox.TabIndex = 3;
			this.interfaceCountComboBox.SelectedIndexChanged += new System.EventHandler(this.countComboBox_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(55, 71);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(113, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "每一路占用空间数：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(55, 113);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "分控数：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(55, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "分控路数：";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.virtualIPListView);
			this.panel2.Controls.Add(this.netcardComboBox);
			this.panel2.Controls.Add(this.panel1);
			this.panel2.Controls.Add(this.netcardInfoGroupBox);
			this.panel2.Controls.Add(this.refreshNetcardButton);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(715, 445);
			this.panel2.TabIndex = 79;
			// 
			// MainForm3
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(715, 580);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.simulatePanel);
			this.Controls.Add(this.statusStrip);
			this.Name = "MainForm3";
			this.Text = "幻彩灯带控制器(广播版）";
			this.Activated += new System.EventHandler(this.MainForm3_Activated);
			this.Load += new System.EventHandler(this.MainForm3_Load);
			this.netcardInfoGroupBox.ResumeLayout(false);
			this.netcardInfoGroupBox.PerformLayout();
			this.simulatePanel.ResumeLayout(false);
			this.simulatePanel.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.controllerCountNUD)).EndInit();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox netcardComboBox;
		private System.Windows.Forms.Button refreshNetcardButton;
		private System.Windows.Forms.GroupBox netcardInfoGroupBox;
		private System.Windows.Forms.Button clearVIPButton;
		private System.Windows.Forms.Button dhcpButton;
		private System.Windows.Forms.Label dnsLabel2;
		private System.Windows.Forms.Label dnsLabel;
		private System.Windows.Forms.Label gatewayLabel2;
		private System.Windows.Forms.Button refreshNetcardInfoButton;
		private System.Windows.Forms.Label gatewayLabel;
		private System.Windows.Forms.Label submaskLabel2;
		private System.Windows.Forms.Label submaskLabel;
		private System.Windows.Forms.Label ipLabel2;
		private System.Windows.Forms.Label ipLabel;
		private System.Windows.Forms.ListView virtualIPListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Panel simulatePanel;
		private System.Windows.Forms.Button recordButton;
		private System.Windows.Forms.Button debugButton;
		private System.Windows.Forms.Button setFilePathButton;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Label recordPathLabel;
		private System.Windows.Forms.Button minusButton;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Button plusButton;
		private System.Windows.Forms.Label binLabel;
		private System.Windows.Forms.TextBox recordTextBox;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel2;
		private System.Windows.Forms.ToolTip myToolTip;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ComboBox spaceCountComboBox;
		private System.Windows.Forms.ComboBox interfaceCountComboBox;
		private System.Windows.Forms.NumericUpDown controllerCountNUD;
	}
}