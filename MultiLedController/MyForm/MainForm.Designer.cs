namespace MultiLedController.MyForm
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.delAllButton = new System.Windows.Forms.Button();
			this.delButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.choosenListView = new System.Windows.Forms.ListView();
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.deviceListView = new System.Windows.Forms.ListView();
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.searchButton = new System.Windows.Forms.Button();
			this.virtualIPListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.refreshNetcardButton = new System.Windows.Forms.Button();
			this.netcardComboBox = new System.Windows.Forms.ComboBox();
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
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.myStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.minusButton = new System.Windows.Forms.Button();
			this.plusButton = new System.Windows.Forms.Button();
			this.recordTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.recordPathLabel = new System.Windows.Forms.Label();
			this.setFilePathButton = new System.Windows.Forms.Button();
			this.recordButton = new System.Windows.Forms.Button();
			this.debugButton = new System.Windows.Forms.Button();
			this.startButton = new System.Windows.Forms.Button();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.recordFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.panel2 = new System.Windows.Forms.Panel();
			this.topPanel = new System.Windows.Forms.Panel();
			this.testButton = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.netcardInfoGroupBox.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.panel2.SuspendLayout();
			this.topPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.delAllButton);
			this.panel1.Controls.Add(this.delButton);
			this.panel1.Controls.Add(this.addButton);
			this.panel1.Controls.Add(this.choosenListView);
			this.panel1.Controls.Add(this.deviceListView);
			this.panel1.Location = new System.Drawing.Point(8, 124);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(379, 500);
			this.panel1.TabIndex = 1;
			// 
			// delAllButton
			// 
			this.delAllButton.Location = new System.Drawing.Point(294, 248);
			this.delAllButton.Name = "delAllButton";
			this.delAllButton.Size = new System.Drawing.Size(55, 23);
			this.delAllButton.TabIndex = 55;
			this.delAllButton.Text = "↑↑↑";
			this.delAllButton.UseVisualStyleBackColor = true;
			this.delAllButton.Click += new System.EventHandler(this.delAllButton_Click);
			// 
			// delButton
			// 
			this.delButton.Enabled = false;
			this.delButton.Location = new System.Drawing.Point(217, 248);
			this.delButton.Name = "delButton";
			this.delButton.Size = new System.Drawing.Size(55, 23);
			this.delButton.TabIndex = 55;
			this.delButton.Text = "↑";
			this.delButton.UseVisualStyleBackColor = true;
			this.delButton.Click += new System.EventHandler(this.delButton_Click);
			// 
			// addButton
			// 
			this.addButton.Enabled = false;
			this.addButton.Location = new System.Drawing.Point(73, 248);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(55, 23);
			this.addButton.TabIndex = 54;
			this.addButton.Text = "↓";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// choosenListView
			// 
			this.choosenListView.BackColor = System.Drawing.Color.MintCream;
			this.choosenListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
			this.choosenListView.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.choosenListView.FullRowSelect = true;
			this.choosenListView.GridLines = true;
			this.choosenListView.HideSelection = false;
			this.choosenListView.Location = new System.Drawing.Point(0, 285);
			this.choosenListView.MultiSelect = false;
			this.choosenListView.Name = "choosenListView";
			this.choosenListView.Size = new System.Drawing.Size(379, 215);
			this.choosenListView.TabIndex = 53;
			this.choosenListView.UseCompatibleStateImageBehavior = false;
			this.choosenListView.View = System.Windows.Forms.View.Details;
			this.choosenListView.SelectedIndexChanged += new System.EventHandler(this.choosenListView_SelectedIndexChanged);
			this.choosenListView.DoubleClick += new System.EventHandler(this.delButton_Click);
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "";
			this.columnHeader8.Width = 20;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "设备名称";
			this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader9.Width = 155;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "设备IP";
			this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader10.Width = 137;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "路数";
			this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader11.Width = 50;
			// 
			// deviceListView
			// 
			this.deviceListView.BackColor = System.Drawing.Color.OldLace;
			this.deviceListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader6,
            this.columnHeader7});
			this.deviceListView.Dock = System.Windows.Forms.DockStyle.Top;
			this.deviceListView.FullRowSelect = true;
			this.deviceListView.GridLines = true;
			this.deviceListView.HideSelection = false;
			this.deviceListView.Location = new System.Drawing.Point(0, 0);
			this.deviceListView.MultiSelect = false;
			this.deviceListView.Name = "deviceListView";
			this.deviceListView.Size = new System.Drawing.Size(379, 242);
			this.deviceListView.TabIndex = 52;
			this.deviceListView.UseCompatibleStateImageBehavior = false;
			this.deviceListView.View = System.Windows.Forms.View.Details;
			this.deviceListView.SelectedIndexChanged += new System.EventHandler(this.controllerListView_SelectedIndexChanged);
			this.deviceListView.DoubleClick += new System.EventHandler(this.addButton_Click);
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "";
			this.columnHeader2.Width = 0;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "设备名称";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader3.Width = 165;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "设备IP";
			this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader6.Width = 150;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "路数";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader7.Width = 50;
			// 
			// searchButton
			// 
			this.searchButton.Enabled = false;
			this.searchButton.Font = new System.Drawing.Font("黑体", 8F);
			this.searchButton.Location = new System.Drawing.Point(12, 82);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(88, 27);
			this.searchButton.TabIndex = 54;
			this.searchButton.Text = "搜索设备";
			this.searchButton.UseVisualStyleBackColor = true;
			this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// virtualIPListView
			// 
			this.virtualIPListView.BackColor = System.Drawing.Color.LightBlue;
			this.virtualIPListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader12,
            this.columnHeader5});
			this.virtualIPListView.FullRowSelect = true;
			this.virtualIPListView.GridLines = true;
			this.virtualIPListView.HideSelection = false;
			this.virtualIPListView.Location = new System.Drawing.Point(393, 124);
			this.virtualIPListView.Name = "virtualIPListView";
			this.virtualIPListView.Size = new System.Drawing.Size(348, 500);
			this.virtualIPListView.TabIndex = 49;
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
			// columnHeader12
			// 
			this.columnHeader12.Text = "关联设备名";
			this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader12.Width = 110;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "关联路数";
			this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader5.Width = 62;
			// 
			// refreshNetcardButton
			// 
			this.refreshNetcardButton.Font = new System.Drawing.Font("黑体", 8F);
			this.refreshNetcardButton.Location = new System.Drawing.Point(12, 44);
			this.refreshNetcardButton.Name = "refreshNetcardButton";
			this.refreshNetcardButton.Size = new System.Drawing.Size(88, 27);
			this.refreshNetcardButton.TabIndex = 56;
			this.refreshNetcardButton.Text = "刷新网卡列表";
			this.refreshNetcardButton.UseVisualStyleBackColor = true;
			this.refreshNetcardButton.Click += new System.EventHandler(this.refreshNetcardButton_Click);
			// 
			// netcardComboBox
			// 
			this.netcardComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.netcardComboBox.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.netcardComboBox.FormattingEnabled = true;
			this.netcardComboBox.Location = new System.Drawing.Point(12, 12);
			this.netcardComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.netcardComboBox.Name = "netcardComboBox";
			this.netcardComboBox.Size = new System.Drawing.Size(365, 20);
			this.netcardComboBox.TabIndex = 57;
			this.netcardComboBox.SelectedIndexChanged += new System.EventHandler(this.netcardComboBox_SelectedIndexChanged);
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
			this.netcardInfoGroupBox.Location = new System.Drawing.Point(393, 6);
			this.netcardInfoGroupBox.Name = "netcardInfoGroupBox";
			this.netcardInfoGroupBox.Size = new System.Drawing.Size(348, 112);
			this.netcardInfoGroupBox.TabIndex = 59;
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
			this.clearVIPButton.Click += new System.EventHandler(this.clearVIPButton_Click);
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
			this.dhcpButton.Click += new System.EventHandler(this.dhcpButton_Click);
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
			this.refreshNetcardInfoButton.Click += new System.EventHandler(this.refreshNetcardinfoButton_Click);
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
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel1,
            this.myStatusLabel2});
			this.statusStrip.Location = new System.Drawing.Point(0, 715);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(749, 26);
			this.statusStrip.SizingGrip = false;
			this.statusStrip.TabIndex = 60;
			this.statusStrip.Text = "statusStrip1";
			// 
			// myStatusLabel1
			// 
			this.myStatusLabel1.Name = "myStatusLabel1";
			this.myStatusLabel1.Size = new System.Drawing.Size(367, 21);
			this.myStatusLabel1.Spring = true;
			this.myStatusLabel1.Text = "   ";
			this.myStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// myStatusLabel2
			// 
			this.myStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.myStatusLabel2.Name = "myStatusLabel2";
			this.myStatusLabel2.Size = new System.Drawing.Size(367, 21);
			this.myStatusLabel2.Spring = true;
			this.myStatusLabel2.Text = "  ";
			this.myStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// minusButton
			// 
			this.minusButton.Location = new System.Drawing.Point(595, 51);
			this.minusButton.Name = "minusButton";
			this.minusButton.Size = new System.Drawing.Size(30, 31);
			this.minusButton.TabIndex = 72;
			this.minusButton.Text = "-";
			this.minusButton.UseVisualStyleBackColor = true;
			this.minusButton.Click += new System.EventHandler(this.minusButton_Click);
			// 
			// plusButton
			// 
			this.plusButton.Location = new System.Drawing.Point(562, 51);
			this.plusButton.Name = "plusButton";
			this.plusButton.Size = new System.Drawing.Size(30, 31);
			this.plusButton.TabIndex = 71;
			this.plusButton.Text = "+";
			this.plusButton.UseVisualStyleBackColor = true;
			this.plusButton.Click += new System.EventHandler(this.plusButton_Click);
			// 
			// recordTextBox
			// 
			this.recordTextBox.Location = new System.Drawing.Point(405, 61);
			this.recordTextBox.MaxLength = 3;
			this.recordTextBox.Name = "recordTextBox";
			this.recordTextBox.Size = new System.Drawing.Size(39, 21);
			this.recordTextBox.TabIndex = 70;
			this.recordTextBox.Text = "000";
			this.recordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.recordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.recordTextBox_KeyPress);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(447, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 12);
			this.label3.TabIndex = 68;
			this.label3.Text = ".bin";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(280, 67);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 12);
			this.label1.TabIndex = 69;
			this.label1.Text = "设置录制文件名： SC";
			// 
			// recordPathLabel
			// 
			this.recordPathLabel.Location = new System.Drawing.Point(373, 16);
			this.recordPathLabel.Name = "recordPathLabel";
			this.recordPathLabel.Size = new System.Drawing.Size(249, 30);
			this.recordPathLabel.TabIndex = 67;
			this.recordPathLabel.Text = "请选择工程存放目录，程序会自动为您创建子文件夹，用以区分不同的控制器。";
			this.recordPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// setFilePathButton
			// 
			this.setFilePathButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.setFilePathButton.Location = new System.Drawing.Point(278, 12);
			this.setFilePathButton.Name = "setFilePathButton";
			this.setFilePathButton.Size = new System.Drawing.Size(87, 39);
			this.setFilePathButton.TabIndex = 66;
			this.setFilePathButton.Text = "选择存放目录";
			this.setFilePathButton.UseVisualStyleBackColor = true;
			this.setFilePathButton.Click += new System.EventHandler(this.setFilePathButton_Click);
			// 
			// recordButton
			// 
			this.recordButton.BackColor = System.Drawing.Color.Transparent;
			this.recordButton.Enabled = false;
			this.recordButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.recordButton.Location = new System.Drawing.Point(666, 11);
			this.recordButton.Name = "recordButton";
			this.recordButton.Size = new System.Drawing.Size(76, 72);
			this.recordButton.TabIndex = 65;
			this.recordButton.Text = "录制数据";
			this.recordButton.UseVisualStyleBackColor = false;
			this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
			// 
			// debugButton
			// 
			this.debugButton.Enabled = false;
			this.debugButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.debugButton.Location = new System.Drawing.Point(90, 11);
			this.debugButton.Name = "debugButton";
			this.debugButton.Size = new System.Drawing.Size(76, 72);
			this.debugButton.TabIndex = 73;
			this.debugButton.Text = "开始调试";
			this.debugButton.UseVisualStyleBackColor = true;
			this.debugButton.Click += new System.EventHandler(this.debugButton_Click);
			// 
			// startButton
			// 
			this.startButton.BackColor = System.Drawing.Color.Coral;
			this.startButton.Enabled = false;
			this.startButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.startButton.Location = new System.Drawing.Point(8, 11);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(76, 72);
			this.startButton.TabIndex = 74;
			this.startButton.Text = "启动模拟";
			this.startButton.UseVisualStyleBackColor = false;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// recordFolderBrowserDialog
			// 
			this.recordFolderBrowserDialog.Description = "请选择录制文件存放目录，本程序将会在点击《录制》按钮之后，将录制文件保存在该目录下。";
			this.recordFolderBrowserDialog.SelectedPath = "C:\\Temp\\CSJ_SC";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.recordButton);
			this.panel2.Controls.Add(this.debugButton);
			this.panel2.Controls.Add(this.setFilePathButton);
			this.panel2.Controls.Add(this.startButton);
			this.panel2.Controls.Add(this.recordPathLabel);
			this.panel2.Controls.Add(this.minusButton);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.plusButton);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.recordTextBox);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 626);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(749, 89);
			this.panel2.TabIndex = 75;
			// 
			// topPanel
			// 
			this.topPanel.Controls.Add(this.testButton);
			this.topPanel.Controls.Add(this.virtualIPListView);
			this.topPanel.Controls.Add(this.panel1);
			this.topPanel.Controls.Add(this.searchButton);
			this.topPanel.Controls.Add(this.netcardComboBox);
			this.topPanel.Controls.Add(this.refreshNetcardButton);
			this.topPanel.Controls.Add(this.netcardInfoGroupBox);
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPanel.Location = new System.Drawing.Point(0, 0);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(749, 633);
			this.topPanel.TabIndex = 76;
			// 
			// testButton
			// 
			this.testButton.BackColor = System.Drawing.Color.Red;
			this.testButton.Font = new System.Drawing.Font("隶书", 14F, System.Drawing.FontStyle.Bold);
			this.testButton.Location = new System.Drawing.Point(106, 82);
			this.testButton.Name = "testButton";
			this.testButton.Size = new System.Drawing.Size(88, 27);
			this.testButton.TabIndex = 60;
			this.testButton.Text = "Test";
			this.testButton.UseVisualStyleBackColor = false;
			this.testButton.Click += new System.EventHandler(this.testButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(749, 741);
			this.Controls.Add(this.topPanel);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.statusStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(765, 780);
			this.MinimumSize = new System.Drawing.Size(765, 780);
			this.Name = "MainForm";
			this.Text = "幻彩灯带控制器(多设备版)";
			this.Activated += new System.EventHandler(this.MainForm_Activated);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.panel1.ResumeLayout(false);
			this.netcardInfoGroupBox.ResumeLayout(false);
			this.netcardInfoGroupBox.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.topPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListView virtualIPListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ListView deviceListView;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.Button refreshNetcardButton;
		private System.Windows.Forms.ComboBox netcardComboBox;
		private System.Windows.Forms.ListView choosenListView;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.Button searchButton;
		private System.Windows.Forms.GroupBox netcardInfoGroupBox;
		private System.Windows.Forms.Button dhcpButton;
		private System.Windows.Forms.Button refreshNetcardInfoButton;
		private System.Windows.Forms.Label dnsLabel2;
		private System.Windows.Forms.Label dnsLabel;
		private System.Windows.Forms.Label gatewayLabel2;
		private System.Windows.Forms.Label gatewayLabel;
		private System.Windows.Forms.Label submaskLabel2;
		private System.Windows.Forms.Label submaskLabel;
		private System.Windows.Forms.Label ipLabel2;
		private System.Windows.Forms.Label ipLabel;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel2;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button delButton;
		private System.Windows.Forms.Button minusButton;
		private System.Windows.Forms.Button plusButton;
		private System.Windows.Forms.TextBox recordTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label recordPathLabel;
		private System.Windows.Forms.Button setFilePathButton;
		private System.Windows.Forms.Button recordButton;
		private System.Windows.Forms.Button debugButton;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.ToolTip myToolTip;
		private System.Windows.Forms.FolderBrowserDialog recordFolderBrowserDialog;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.Button clearVIPButton;
		private System.Windows.Forms.Button testButton;
		private System.Windows.Forms.Button delAllButton;
	}
}