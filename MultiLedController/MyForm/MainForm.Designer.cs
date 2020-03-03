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
			this.networkButton = new System.Windows.Forms.Button();
			this.refreshButton = new System.Windows.Forms.Button();
			this.localIPComboBox = new System.Windows.Forms.ComboBox();
			this.searchButton = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.virtualIPListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.recordButton = new System.Windows.Forms.Button();
			this.startButton = new System.Windows.Forms.Button();
			this.linkButton = new System.Windows.Forms.Button();
			this.controllorListView = new System.Windows.Forms.ListView();
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.statusStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// networkButton
			// 
			this.networkButton.Location = new System.Drawing.Point(708, 12);
			this.networkButton.Name = "networkButton";
			this.networkButton.Size = new System.Drawing.Size(80, 48);
			this.networkButton.TabIndex = 0;
			this.networkButton.Text = "网络设置";
			this.networkButton.UseVisualStyleBackColor = true;
			this.networkButton.Click += new System.EventHandler(this.networkButton_Click);
			// 
			// refreshButton
			// 
			this.refreshButton.Location = new System.Drawing.Point(21, 39);
			this.refreshButton.Margin = new System.Windows.Forms.Padding(2);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(90, 24);
			this.refreshButton.TabIndex = 25;
			this.refreshButton.Text = "刷新IP列表";
			this.refreshButton.UseVisualStyleBackColor = true;
			this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
			// 
			// localIPComboBox
			// 
			this.localIPComboBox.FormattingEnabled = true;
			this.localIPComboBox.Location = new System.Drawing.Point(21, 11);
			this.localIPComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.localIPComboBox.Name = "localIPComboBox";
			this.localIPComboBox.Size = new System.Drawing.Size(186, 20);
			this.localIPComboBox.TabIndex = 24;
			// 
			// searchButton
			// 
			this.searchButton.Location = new System.Drawing.Point(116, 39);
			this.searchButton.Margin = new System.Windows.Forms.Padding(2);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(90, 24);
			this.searchButton.TabIndex = 26;
			this.searchButton.Text = "搜索设备";
			this.searchButton.UseVisualStyleBackColor = true;
			this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 442);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(800, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 27;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(785, 17);
			this.myStatusLabel.Spring = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tabControl1.ItemSize = new System.Drawing.Size(60, 36);
			this.tabControl1.Location = new System.Drawing.Point(0, 72);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(800, 370);
			this.tabControl1.TabIndex = 28;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.virtualIPListView);
			this.tabPage1.Controls.Add(this.recordButton);
			this.tabPage1.Controls.Add(this.startButton);
			this.tabPage1.Controls.Add(this.linkButton);
			this.tabPage1.Controls.Add(this.controllorListView);
			this.tabPage1.Location = new System.Drawing.Point(4, 40);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(792, 326);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "设备列表";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// virtualIPListView
			// 
			this.virtualIPListView.BackColor = System.Drawing.Color.LightSkyBlue;
			this.virtualIPListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5});
			this.virtualIPListView.Dock = System.Windows.Forms.DockStyle.Right;
			this.virtualIPListView.FullRowSelect = true;
			this.virtualIPListView.GridLines = true;
			this.virtualIPListView.HideSelection = false;
			this.virtualIPListView.Location = new System.Drawing.Point(466, 3);
			this.virtualIPListView.Name = "virtualIPListView";
			this.virtualIPListView.Size = new System.Drawing.Size(323, 320);
			this.virtualIPListView.TabIndex = 2;
			this.virtualIPListView.UseCompatibleStateImageBehavior = false;
			this.virtualIPListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "";
			this.columnHeader1.Width = 20;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "虚拟IP";
			this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader4.Width = 146;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "关联设备";
			this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader5.Width = 153;
			// 
			// recordButton
			// 
			this.recordButton.Location = new System.Drawing.Point(357, 239);
			this.recordButton.Name = "recordButton";
			this.recordButton.Size = new System.Drawing.Size(75, 23);
			this.recordButton.TabIndex = 1;
			this.recordButton.Text = "录制DMX";
			this.recordButton.UseVisualStyleBackColor = true;
			this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(357, 187);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(75, 23);
			this.startButton.TabIndex = 1;
			this.startButton.Text = "启动模拟";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// linkButton
			// 
			this.linkButton.Location = new System.Drawing.Point(357, 64);
			this.linkButton.Name = "linkButton";
			this.linkButton.Size = new System.Drawing.Size(75, 23);
			this.linkButton.TabIndex = 1;
			this.linkButton.Text = "关联到IP->";
			this.linkButton.UseVisualStyleBackColor = true;
			this.linkButton.Click += new System.EventHandler(this.linkButton_Click);
			// 
			// controllorListView
			// 
			this.controllorListView.BackColor = System.Drawing.Color.PowderBlue;
			this.controllorListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader6});
			this.controllorListView.Dock = System.Windows.Forms.DockStyle.Left;
			this.controllorListView.FullRowSelect = true;
			this.controllorListView.GridLines = true;
			this.controllorListView.HideSelection = false;
			this.controllorListView.Location = new System.Drawing.Point(3, 3);
			this.controllorListView.MultiSelect = false;
			this.controllorListView.Name = "controllorListView";
			this.controllorListView.Size = new System.Drawing.Size(323, 320);
			this.controllorListView.TabIndex = 0;
			this.controllorListView.UseCompatibleStateImageBehavior = false;
			this.controllorListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "";
			this.columnHeader2.Width = 0;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "设备Mac";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader3.Width = 151;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "设备信息";
			this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader6.Width = 166;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.ClientSize = new System.Drawing.Size(800, 464);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.searchButton);
			this.Controls.Add(this.refreshButton);
			this.Controls.Add(this.localIPComboBox);
			this.Controls.Add(this.networkButton);
			this.Name = "MainForm";
			this.Text = "多彩灯带控制器";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button networkButton;
		private System.Windows.Forms.Button refreshButton;
		private System.Windows.Forms.ComboBox localIPComboBox;
		private System.Windows.Forms.Button searchButton;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ListView controllorListView;
		private System.Windows.Forms.Button linkButton;
		private System.Windows.Forms.ListView virtualIPListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Button recordButton;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.ColumnHeader columnHeader6;
	}
}