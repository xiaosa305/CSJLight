namespace LightController.MyForm.HardwareSet
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
			this.label5 = new System.Windows.Forms.Label();
			this.deviceNameTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.macCheckBox = new System.Windows.Forms.CheckBox();
			this.gatewayTextBox = new System.Windows.Forms.TextBox();
			this.macTextBox = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.IPTextBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.netmaskTextBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.downloadButton = new System.Windows.Forms.Button();
			this.readButton = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.hardwarePage = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.hidePanel = new System.Windows.Forms.Panel();
			this.protocolLabel = new System.Windows.Forms.Label();
			this.protocolTextBox = new System.Windows.Forms.TextBox();
			this.firmwarePage = new System.Windows.Forms.TabPage();
			this.fileOpenButton = new System.Windows.Forms.Button();
			this.pathLabel = new System.Windows.Forms.Label();
			this.versionButton = new System.Windows.Forms.Button();
			this.updateButton = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.myProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.progressStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.tabControl1.SuspendLayout();
			this.hardwarePage.SuspendLayout();
			this.panel1.SuspendLayout();
			this.firmwarePage.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(37, 109);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 17;
			this.label5.Text = "网关：";
			// 
			// deviceNameTextBox
			// 
			this.deviceNameTextBox.Location = new System.Drawing.Point(116, 31);
			this.deviceNameTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.deviceNameTextBox.MaxLength = 16;
			this.deviceNameTextBox.Name = "deviceNameTextBox";
			this.deviceNameTextBox.Size = new System.Drawing.Size(124, 21);
			this.deviceNameTextBox.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(37, 35);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "设备名：";
			// 
			// macCheckBox
			// 
			this.macCheckBox.AutoSize = true;
			this.macCheckBox.Location = new System.Drawing.Point(294, 144);
			this.macCheckBox.Name = "macCheckBox";
			this.macCheckBox.Size = new System.Drawing.Size(114, 16);
			this.macCheckBox.TabIndex = 15;
			this.macCheckBox.Text = "自动获取MAC地址";
			this.macCheckBox.UseVisualStyleBackColor = true;
			this.macCheckBox.CheckedChanged += new System.EventHandler(this.macCheckBox_CheckedChanged);
			// 
			// gatewayTextBox
			// 
			this.gatewayTextBox.Location = new System.Drawing.Point(116, 105);
			this.gatewayTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.gatewayTextBox.Name = "gatewayTextBox";
			this.gatewayTextBox.Size = new System.Drawing.Size(124, 21);
			this.gatewayTextBox.TabIndex = 11;
			this.gatewayTextBox.Text = "192.168.2.1";
			// 
			// macTextBox
			// 
			this.macTextBox.Location = new System.Drawing.Point(116, 142);
			this.macTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.macTextBox.MaxLength = 17;
			this.macTextBox.Name = "macTextBox";
			this.macTextBox.Size = new System.Drawing.Size(124, 21);
			this.macTextBox.TabIndex = 12;
			this.macTextBox.Text = "00-00-00-00-00-00";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(37, 146);
			this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(59, 12);
			this.label10.TabIndex = 6;
			this.label10.Text = "Mac地址：";
			// 
			// IPTextBox
			// 
			this.IPTextBox.Location = new System.Drawing.Point(116, 68);
			this.IPTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.IPTextBox.Name = "IPTextBox";
			this.IPTextBox.Size = new System.Drawing.Size(124, 21);
			this.IPTextBox.TabIndex = 13;
			this.IPTextBox.Text = "192.168,2.10";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(37, 72);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 12);
			this.label6.TabIndex = 7;
			this.label6.Text = "IP地址：";
			// 
			// netmaskTextBox
			// 
			this.netmaskTextBox.Location = new System.Drawing.Point(371, 68);
			this.netmaskTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.netmaskTextBox.Name = "netmaskTextBox";
			this.netmaskTextBox.Size = new System.Drawing.Size(109, 21);
			this.netmaskTextBox.TabIndex = 14;
			this.netmaskTextBox.Text = "255.255.255.0";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(292, 72);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(65, 12);
			this.label7.TabIndex = 8;
			this.label7.Text = "子网掩码：";
			// 
			// downloadButton
			// 
			this.downloadButton.BackColor = System.Drawing.Color.OrangeRed;
			this.downloadButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.downloadButton.ForeColor = System.Drawing.Color.Black;
			this.downloadButton.Location = new System.Drawing.Point(403, 208);
			this.downloadButton.Margin = new System.Windows.Forms.Padding(2);
			this.downloadButton.Name = "downloadButton";
			this.downloadButton.Size = new System.Drawing.Size(91, 36);
			this.downloadButton.TabIndex = 28;
			this.downloadButton.Text = "写入设备";
			this.downloadButton.UseVisualStyleBackColor = false;
			this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
			// 
			// readButton
			// 
			this.readButton.BackColor = System.Drawing.Color.Transparent;
			this.readButton.Location = new System.Drawing.Point(297, 208);
			this.readButton.Margin = new System.Windows.Forms.Padding(2);
			this.readButton.Name = "readButton";
			this.readButton.Size = new System.Drawing.Size(91, 36);
			this.readButton.TabIndex = 29;
			this.readButton.Text = "从设备回读";
			this.readButton.UseVisualStyleBackColor = false;
			this.readButton.Click += new System.EventHandler(this.readButton_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.hardwarePage);
			this.tabControl1.Controls.Add(this.firmwarePage);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(534, 287);
			this.tabControl1.TabIndex = 30;
			// 
			// hardwarePage
			// 
			this.hardwarePage.BackColor = System.Drawing.Color.SlateGray;
			this.hardwarePage.Controls.Add(this.panel1);
			this.hardwarePage.Controls.Add(this.downloadButton);
			this.hardwarePage.Controls.Add(this.readButton);
			this.hardwarePage.Location = new System.Drawing.Point(4, 22);
			this.hardwarePage.Name = "hardwarePage";
			this.hardwarePage.Padding = new System.Windows.Forms.Padding(3);
			this.hardwarePage.Size = new System.Drawing.Size(526, 261);
			this.hardwarePage.TabIndex = 0;
			this.hardwarePage.Text = "硬件配置";
			this.hardwarePage.DoubleClick += new System.EventHandler(this.hardwarePage_DoubleClick);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.deviceNameTextBox);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.macCheckBox);
			this.panel1.Controls.Add(this.gatewayTextBox);
			this.panel1.Controls.Add(this.macTextBox);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.IPTextBox);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.netmaskTextBox);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.hidePanel);
			this.panel1.Controls.Add(this.protocolLabel);
			this.panel1.Controls.Add(this.protocolTextBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(520, 186);
			this.panel1.TabIndex = 4;
			// 
			// hidePanel
			// 
			this.hidePanel.Location = new System.Drawing.Point(294, 30);
			this.hidePanel.Name = "hidePanel";
			this.hidePanel.Size = new System.Drawing.Size(186, 21);
			this.hidePanel.TabIndex = 18;
			this.hidePanel.DoubleClick += new System.EventHandler(this.hidePanel_DoubleClick);
			// 
			// protocolLabel
			// 
			this.protocolLabel.AutoSize = true;
			this.protocolLabel.Location = new System.Drawing.Point(292, 33);
			this.protocolLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.protocolLabel.Name = "protocolLabel";
			this.protocolLabel.Size = new System.Drawing.Size(41, 12);
			this.protocolLabel.TabIndex = 8;
			this.protocolLabel.Text = "协议：";
			// 
			// protocolTextBox
			// 
			this.protocolTextBox.Location = new System.Drawing.Point(371, 30);
			this.protocolTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.protocolTextBox.Name = "protocolTextBox";
			this.protocolTextBox.Size = new System.Drawing.Size(109, 21);
			this.protocolTextBox.TabIndex = 14;
			// 
			// firmwarePage
			// 
			this.firmwarePage.Controls.Add(this.fileOpenButton);
			this.firmwarePage.Controls.Add(this.pathLabel);
			this.firmwarePage.Controls.Add(this.versionButton);
			this.firmwarePage.Controls.Add(this.updateButton);
			this.firmwarePage.Location = new System.Drawing.Point(4, 22);
			this.firmwarePage.Name = "firmwarePage";
			this.firmwarePage.Padding = new System.Windows.Forms.Padding(3);
			this.firmwarePage.Size = new System.Drawing.Size(526, 261);
			this.firmwarePage.TabIndex = 1;
			this.firmwarePage.Text = "固件升级";
			this.firmwarePage.UseVisualStyleBackColor = true;
			// 
			// fileOpenButton
			// 
			this.fileOpenButton.Location = new System.Drawing.Point(17, 28);
			this.fileOpenButton.Name = "fileOpenButton";
			this.fileOpenButton.Size = new System.Drawing.Size(86, 33);
			this.fileOpenButton.TabIndex = 40;
			this.fileOpenButton.Text = "选择升级文件";
			this.fileOpenButton.UseVisualStyleBackColor = true;
			this.fileOpenButton.Click += new System.EventHandler(this.fileOpenButton_Click);
			// 
			// pathLabel
			// 
			this.pathLabel.Location = new System.Drawing.Point(118, 28);
			this.pathLabel.Name = "pathLabel";
			this.pathLabel.Size = new System.Drawing.Size(387, 33);
			this.pathLabel.TabIndex = 43;
			this.pathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// versionButton
			// 
			this.versionButton.BackColor = System.Drawing.Color.Silver;
			this.versionButton.Enabled = false;
			this.versionButton.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.versionButton.ForeColor = System.Drawing.SystemColors.WindowText;
			this.versionButton.Location = new System.Drawing.Point(335, 169);
			this.versionButton.Name = "versionButton";
			this.versionButton.Size = new System.Drawing.Size(81, 59);
			this.versionButton.TabIndex = 41;
			this.versionButton.Text = "获取当前\r\n固件版本";
			this.versionButton.UseVisualStyleBackColor = false;
			this.versionButton.Visible = false;
			// 
			// updateButton
			// 
			this.updateButton.BackColor = System.Drawing.Color.OrangeRed;
			this.updateButton.Enabled = false;
			this.updateButton.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.updateButton.ForeColor = System.Drawing.SystemColors.WindowText;
			this.updateButton.Location = new System.Drawing.Point(424, 169);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(81, 59);
			this.updateButton.TabIndex = 42;
			this.updateButton.Text = "升级";
			this.updateButton.UseVisualStyleBackColor = false;
			this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.BackColor = System.Drawing.Color.White;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel,
            this.myProgressBar,
            this.progressStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 287);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(534, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 38;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(337, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// myProgressBar
			// 
			this.myProgressBar.BackColor = System.Drawing.Color.Black;
			this.myProgressBar.Name = "myProgressBar";
			this.myProgressBar.Size = new System.Drawing.Size(148, 16);
			this.myProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			// 
			// progressStatusLabel
			// 
			this.progressStatusLabel.AutoSize = false;
			this.progressStatusLabel.Name = "progressStatusLabel";
			this.progressStatusLabel.Size = new System.Drawing.Size(32, 17);
			this.progressStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "*.xbin(自定义二进制文件)|*.xbin";
			// 
			// HardwareSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.ClientSize = new System.Drawing.Size(534, 309);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.statusStrip1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(550, 348);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(550, 348);
			this.Name = "HardwareSetForm";
			this.Text = "硬件配置";
			this.Load += new System.EventHandler(this.NewHardwareSet_Load);
			this.Shown += new System.EventHandler(this.NewHardwareSetForm_Shown);
			this.tabControl1.ResumeLayout(false);
			this.hardwarePage.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.firmwarePage.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox deviceNameTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox macCheckBox;
		private System.Windows.Forms.TextBox gatewayTextBox;
		private System.Windows.Forms.TextBox macTextBox;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox IPTextBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox netmaskTextBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button downloadButton;
		private System.Windows.Forms.Button readButton;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage hardwarePage;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabPage firmwarePage;
		private System.Windows.Forms.Button fileOpenButton;
		private System.Windows.Forms.Label pathLabel;
		private System.Windows.Forms.Button versionButton;
		private System.Windows.Forms.Button updateButton;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.ToolStripProgressBar myProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel progressStatusLabel;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.TextBox protocolTextBox;
		private System.Windows.Forms.Label protocolLabel;
		private System.Windows.Forms.Panel hidePanel;
	}
}