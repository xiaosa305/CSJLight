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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMainForm));
			this.label2 = new System.Windows.Forms.Label();
			this.netcardComboBox = new System.Windows.Forms.ComboBox();
			this.refreshNetcardButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dnsLabel2 = new System.Windows.Forms.Label();
			this.dnsLabel = new System.Windows.Forms.Label();
			this.gatewayLabel2 = new System.Windows.Forms.Label();
			this.gatewayLabel = new System.Windows.Forms.Label();
			this.submaskLabel2 = new System.Windows.Forms.Label();
			this.submaskLabel = new System.Windows.Forms.Label();
			this.ipLabel2 = new System.Windows.Forms.Label();
			this.ipLabel = new System.Windows.Forms.Label();
			this.virtualIPListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.searchButton = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.controllerListView = new System.Windows.Forms.ListView();
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.recordButton = new System.Windows.Forms.Button();
			this.debugButton = new System.Windows.Forms.Button();
			this.startButton = new System.Windows.Forms.Button();
			this.filePathLabel = new System.Windows.Forms.Label();
			this.setFilePathButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.plusButton = new System.Windows.Forms.Button();
			this.minusButton = new System.Windows.Forms.Button();
			this.networkButton = new System.Windows.Forms.Button();
			this.refreshCurButton = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(17, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 12);
			this.label2.TabIndex = 29;
			this.label2.Text = "网卡:";
			// 
			// netcardComboBox
			// 
			this.netcardComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.netcardComboBox.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.netcardComboBox.FormattingEnabled = true;
			this.netcardComboBox.Location = new System.Drawing.Point(56, 16);
			this.netcardComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.netcardComboBox.Name = "netcardComboBox";
			this.netcardComboBox.Size = new System.Drawing.Size(281, 20);
			this.netcardComboBox.TabIndex = 28;
			this.netcardComboBox.SelectedIndexChanged += new System.EventHandler(this.netcardComboBox_SelectedIndexChanged);
			// 
			// refreshNetcardButton
			// 
			this.refreshNetcardButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.refreshNetcardButton.Location = new System.Drawing.Point(342, 14);
			this.refreshNetcardButton.Name = "refreshNetcardButton";
			this.refreshNetcardButton.Size = new System.Drawing.Size(62, 25);
			this.refreshNetcardButton.TabIndex = 27;
			this.refreshNetcardButton.Text = "刷新列表";
			this.refreshNetcardButton.UseVisualStyleBackColor = true;
			this.refreshNetcardButton.Click += new System.EventHandler(this.refreshNetcardButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.refreshCurButton);
			this.groupBox1.Controls.Add(this.dnsLabel2);
			this.groupBox1.Controls.Add(this.dnsLabel);
			this.groupBox1.Controls.Add(this.gatewayLabel2);
			this.groupBox1.Controls.Add(this.gatewayLabel);
			this.groupBox1.Controls.Add(this.submaskLabel2);
			this.groupBox1.Controls.Add(this.submaskLabel);
			this.groupBox1.Controls.Add(this.ipLabel2);
			this.groupBox1.Controls.Add(this.ipLabel);
			this.groupBox1.Location = new System.Drawing.Point(16, 59);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(390, 93);
			this.groupBox1.TabIndex = 31;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "选中网卡基本信息";
			// 
			// dnsLabel2
			// 
			this.dnsLabel2.AutoSize = true;
			this.dnsLabel2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dnsLabel2.Location = new System.Drawing.Point(207, 59);
			this.dnsLabel2.Name = "dnsLabel2";
			this.dnsLabel2.Size = new System.Drawing.Size(11, 12);
			this.dnsLabel2.TabIndex = 0;
			this.dnsLabel2.Text = " ";
			// 
			// dnsLabel
			// 
			this.dnsLabel.AutoSize = true;
			this.dnsLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dnsLabel.Location = new System.Drawing.Point(172, 59);
			this.dnsLabel.Name = "dnsLabel";
			this.dnsLabel.Size = new System.Drawing.Size(35, 12);
			this.dnsLabel.TabIndex = 0;
			this.dnsLabel.Text = "DNS：";
			// 
			// gatewayLabel2
			// 
			this.gatewayLabel2.AutoSize = true;
			this.gatewayLabel2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.gatewayLabel2.Location = new System.Drawing.Point(49, 59);
			this.gatewayLabel2.Name = "gatewayLabel2";
			this.gatewayLabel2.Size = new System.Drawing.Size(29, 12);
			this.gatewayLabel2.TabIndex = 0;
			this.gatewayLabel2.Text = "    ";
			// 
			// gatewayLabel
			// 
			this.gatewayLabel.AutoSize = true;
			this.gatewayLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.gatewayLabel.Location = new System.Drawing.Point(10, 59);
			this.gatewayLabel.Name = "gatewayLabel";
			this.gatewayLabel.Size = new System.Drawing.Size(41, 12);
			this.gatewayLabel.TabIndex = 0;
			this.gatewayLabel.Text = "网关：";
			// 
			// submaskLabel2
			// 
			this.submaskLabel2.AutoSize = true;
			this.submaskLabel2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.submaskLabel2.Location = new System.Drawing.Point(206, 35);
			this.submaskLabel2.Name = "submaskLabel2";
			this.submaskLabel2.Size = new System.Drawing.Size(11, 12);
			this.submaskLabel2.TabIndex = 0;
			this.submaskLabel2.Text = " ";
			// 
			// submaskLabel
			// 
			this.submaskLabel.AutoSize = true;
			this.submaskLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.submaskLabel.Location = new System.Drawing.Point(168, 35);
			this.submaskLabel.Name = "submaskLabel";
			this.submaskLabel.Size = new System.Drawing.Size(41, 12);
			this.submaskLabel.TabIndex = 0;
			this.submaskLabel.Text = "掩码：";
			// 
			// ipLabel2
			// 
			this.ipLabel2.AutoSize = true;
			this.ipLabel2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ipLabel2.Location = new System.Drawing.Point(49, 35);
			this.ipLabel2.Name = "ipLabel2";
			this.ipLabel2.Size = new System.Drawing.Size(95, 12);
			this.ipLabel2.TabIndex = 0;
			this.ipLabel2.Text = "192.168.222.111";
			// 
			// ipLabel
			// 
			this.ipLabel.AutoSize = true;
			this.ipLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ipLabel.Location = new System.Drawing.Point(12, 35);
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
			this.virtualIPListView.FullRowSelect = true;
			this.virtualIPListView.GridLines = true;
			this.virtualIPListView.HideSelection = false;
			this.virtualIPListView.Location = new System.Drawing.Point(424, 59);
			this.virtualIPListView.Name = "virtualIPListView";
			this.virtualIPListView.Size = new System.Drawing.Size(229, 427);
			this.virtualIPListView.TabIndex = 3;
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
			// searchButton
			// 
			this.searchButton.Enabled = false;
			this.searchButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.searchButton.Location = new System.Drawing.Point(11, 164);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(88, 31);
			this.searchButton.TabIndex = 27;
			this.searchButton.Text = "搜索设备";
			this.searchButton.UseVisualStyleBackColor = true;
			this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 497);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(665, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 32;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(650, 17);
			this.myStatusLabel.Spring = true;
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
			this.controllerListView.Location = new System.Drawing.Point(12, 208);
			this.controllerListView.MultiSelect = false;
			this.controllerListView.Name = "controllerListView";
			this.controllerListView.Size = new System.Drawing.Size(394, 191);
			this.controllerListView.TabIndex = 33;
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
			// recordButton
			// 
			this.recordButton.Enabled = false;
			this.recordButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.recordButton.Location = new System.Drawing.Point(318, 455);
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
			this.debugButton.Location = new System.Drawing.Point(313, 164);
			this.debugButton.Name = "debugButton";
			this.debugButton.Size = new System.Drawing.Size(88, 31);
			this.debugButton.TabIndex = 36;
			this.debugButton.Text = "实时调试";
			this.debugButton.UseVisualStyleBackColor = true;
			// 
			// startButton
			// 
			this.startButton.Enabled = false;
			this.startButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.startButton.Location = new System.Drawing.Point(207, 164);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(88, 31);
			this.startButton.TabIndex = 37;
			this.startButton.Text = "启动";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// filePathLabel
			// 
			this.filePathLabel.Location = new System.Drawing.Point(116, 419);
			this.filePathLabel.Name = "filePathLabel";
			this.filePathLabel.Size = new System.Drawing.Size(287, 30);
			this.filePathLabel.TabIndex = 39;
			this.filePathLabel.Text = "C:\\Temp\\MultiLedFile";
			// 
			// setFilePathButton
			// 
			this.setFilePathButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.setFilePathButton.Location = new System.Drawing.Point(12, 414);
			this.setFilePathButton.Name = "setFilePathButton";
			this.setFilePathButton.Size = new System.Drawing.Size(87, 31);
			this.setFilePathButton.TabIndex = 38;
			this.setFilePathButton.Text = "选择存放目录";
			this.setFilePathButton.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 466);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 12);
			this.label1.TabIndex = 40;
			this.label1.Text = "设置录制文件名： SC";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(180, 466);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 12);
			this.label3.TabIndex = 40;
			this.label3.Text = ".bin";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(138, 462);
			this.textBox1.MaxLength = 2;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(39, 21);
			this.textBox1.TabIndex = 41;
			this.textBox1.Text = "00";
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// plusButton
			// 
			this.plusButton.Location = new System.Drawing.Point(232, 455);
			this.plusButton.Name = "plusButton";
			this.plusButton.Size = new System.Drawing.Size(30, 31);
			this.plusButton.TabIndex = 42;
			this.plusButton.Text = "+";
			this.plusButton.UseVisualStyleBackColor = true;
			this.plusButton.Click += new System.EventHandler(this.plusButton_Click);
			// 
			// minusButton
			// 
			this.minusButton.Location = new System.Drawing.Point(265, 455);
			this.minusButton.Name = "minusButton";
			this.minusButton.Size = new System.Drawing.Size(30, 31);
			this.minusButton.TabIndex = 43;
			this.minusButton.Text = "-";
			this.minusButton.UseVisualStyleBackColor = true;
			this.minusButton.Click += new System.EventHandler(this.minusButton_Click);
			// 
			// networkButton
			// 
			this.networkButton.Location = new System.Drawing.Point(578, 14);
			this.networkButton.Name = "networkButton";
			this.networkButton.Size = new System.Drawing.Size(75, 25);
			this.networkButton.TabIndex = 44;
			this.networkButton.Text = "网络设置";
			this.networkButton.UseVisualStyleBackColor = true;
			this.networkButton.Click += new System.EventHandler(this.networkButton_Click);
			// 
			// refreshCurButton
			// 
			this.refreshCurButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.refreshCurButton.Location = new System.Drawing.Point(323, 20);
			this.refreshCurButton.Name = "refreshCurButton";
			this.refreshCurButton.Size = new System.Drawing.Size(62, 57);
			this.refreshCurButton.TabIndex = 27;
			this.refreshCurButton.Text = "刷新当前\r\n网卡信息";
			this.refreshCurButton.UseVisualStyleBackColor = true;
			this.refreshCurButton.Click += new System.EventHandler(this.refreshCurButton_Click);
			// 
			// NewMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(665, 519);
			this.Controls.Add(this.networkButton);
			this.Controls.Add(this.minusButton);
			this.Controls.Add(this.plusButton);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.filePathLabel);
			this.Controls.Add(this.setFilePathButton);
			this.Controls.Add(this.recordButton);
			this.Controls.Add(this.debugButton);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.controllerListView);
			this.Controls.Add(this.virtualIPListView);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.netcardComboBox);
			this.Controls.Add(this.searchButton);
			this.Controls.Add(this.refreshNetcardButton);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.label2);
			this.ForeColor = System.Drawing.SystemColors.InfoText;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(681, 558);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(681, 558);
			this.Name = "NewMainForm";
			this.Text = "幻彩灯带控制器";
			this.Activated += new System.EventHandler(this.NewMainForm_Activated);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox netcardComboBox;
		private System.Windows.Forms.Button refreshNetcardButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label dnsLabel2;
		private System.Windows.Forms.Label dnsLabel;
		private System.Windows.Forms.Label gatewayLabel2;
		private System.Windows.Forms.Label gatewayLabel;
		private System.Windows.Forms.Label submaskLabel2;
		private System.Windows.Forms.Label submaskLabel;
		private System.Windows.Forms.Label ipLabel2;
		private System.Windows.Forms.Label ipLabel;
		private System.Windows.Forms.ListView virtualIPListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Button searchButton;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.ListView controllerListView;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button recordButton;
		private System.Windows.Forms.Button debugButton;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Label filePathLabel;
		private System.Windows.Forms.Button setFilePathButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.Button plusButton;
		private System.Windows.Forms.Button minusButton;
		private System.Windows.Forms.Button networkButton;
		private System.Windows.Forms.Button refreshCurButton;
	}
}