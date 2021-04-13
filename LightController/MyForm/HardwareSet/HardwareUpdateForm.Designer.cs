namespace LightController.MyForm
{
	partial class HardwareUpdateForm
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
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.pathLabel = new System.Windows.Forms.Label();
			this.fileOpenButton = new System.Windows.Forms.Button();
			this.connectPanel = new System.Windows.Forms.Panel();
			this.switchButton = new System.Windows.Forms.Button();
			this.refreshButton = new System.Windows.Forms.Button();
			this.versionButton = new System.Windows.Forms.Button();
			this.updateButton = new System.Windows.Forms.Button();
			this.deviceConnectButton = new System.Windows.Forms.Button();
			this.deviceComboBox = new System.Windows.Forms.ComboBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.myProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.progressStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.connectPanel.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "*.xbin(自定义二进制文件)|*.xbin";
			// 
			// pathLabel
			// 
			this.pathLabel.Location = new System.Drawing.Point(152, 19);
			this.pathLabel.Name = "pathLabel";
			this.pathLabel.Size = new System.Drawing.Size(387, 33);
			this.pathLabel.TabIndex = 39;
			// 
			// fileOpenButton
			// 
			this.fileOpenButton.Location = new System.Drawing.Point(36, 19);
			this.fileOpenButton.Name = "fileOpenButton";
			this.fileOpenButton.Size = new System.Drawing.Size(86, 33);
			this.fileOpenButton.TabIndex = 12;
			this.fileOpenButton.Text = "选择升级文件";
			this.fileOpenButton.UseVisualStyleBackColor = true;
			this.fileOpenButton.Click += new System.EventHandler(this.fileOpenButton_Click);
			// 
			// connectPanel
			// 
			this.connectPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.connectPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.connectPanel.Controls.Add(this.switchButton);
			this.connectPanel.Controls.Add(this.refreshButton);
			this.connectPanel.Controls.Add(this.versionButton);
			this.connectPanel.Controls.Add(this.updateButton);
			this.connectPanel.Controls.Add(this.deviceConnectButton);
			this.connectPanel.Controls.Add(this.deviceComboBox);
			this.connectPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.connectPanel.Location = new System.Drawing.Point(0, 65);
			this.connectPanel.Name = "connectPanel";
			this.connectPanel.Size = new System.Drawing.Size(564, 129);
			this.connectPanel.TabIndex = 38;
			// 
			// switchButton
			// 
			this.switchButton.Location = new System.Drawing.Point(37, 30);
			this.switchButton.Margin = new System.Windows.Forms.Padding(2);
			this.switchButton.Name = "switchButton";
			this.switchButton.Size = new System.Drawing.Size(88, 59);
			this.switchButton.TabIndex = 34;
			this.switchButton.Text = "以网络连接";
			this.switchButton.UseVisualStyleBackColor = true;
			this.switchButton.TextChanged += new System.EventHandler(this.someButtton_TextChanged);
			this.switchButton.Click += new System.EventHandler(this.switchButton_Click);
			// 
			// refreshButton
			// 
			this.refreshButton.Location = new System.Drawing.Point(149, 63);
			this.refreshButton.Margin = new System.Windows.Forms.Padding(2);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(93, 26);
			this.refreshButton.TabIndex = 32;
			this.refreshButton.Text = "刷新串口";
			this.refreshButton.UseVisualStyleBackColor = true;
			this.refreshButton.TextChanged += new System.EventHandler(this.someButtton_TextChanged);
			this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
			// 
			// versionButton
			// 
			this.versionButton.BackColor = System.Drawing.Color.Silver;
			this.versionButton.Enabled = false;
			this.versionButton.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.versionButton.ForeColor = System.Drawing.SystemColors.WindowText;
			this.versionButton.Location = new System.Drawing.Point(385, 29);
			this.versionButton.Name = "versionButton";
			this.versionButton.Size = new System.Drawing.Size(66, 59);
			this.versionButton.TabIndex = 12;
			this.versionButton.Text = "获取\r\n当前版本";
			this.versionButton.UseVisualStyleBackColor = false;
			this.versionButton.Visible = false;
			this.versionButton.Click += new System.EventHandler(this.versionButton_Click);
			// 
			// updateButton
			// 
			this.updateButton.BackColor = System.Drawing.Color.OrangeRed;
			this.updateButton.Enabled = false;
			this.updateButton.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.updateButton.ForeColor = System.Drawing.SystemColors.WindowText;
			this.updateButton.Location = new System.Drawing.Point(468, 30);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(81, 59);
			this.updateButton.TabIndex = 12;
			this.updateButton.Text = "升级";
			this.updateButton.UseVisualStyleBackColor = false;
			this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// deviceConnectButton
			// 
			this.deviceConnectButton.Enabled = false;
			this.deviceConnectButton.Location = new System.Drawing.Point(273, 63);
			this.deviceConnectButton.Margin = new System.Windows.Forms.Padding(2);
			this.deviceConnectButton.Name = "deviceConnectButton";
			this.deviceConnectButton.Size = new System.Drawing.Size(88, 26);
			this.deviceConnectButton.TabIndex = 31;
			this.deviceConnectButton.Text = "打开串口";
			this.deviceConnectButton.UseVisualStyleBackColor = true;
			this.deviceConnectButton.TextChanged += new System.EventHandler(this.someButtton_TextChanged);
			this.deviceConnectButton.Click += new System.EventHandler(this.deviceConnectButton_Click);
			// 
			// deviceComboBox
			// 
			this.deviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.deviceComboBox.Enabled = false;
			this.deviceComboBox.FormattingEnabled = true;
			this.deviceComboBox.Location = new System.Drawing.Point(142, 30);
			this.deviceComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.deviceComboBox.Name = "deviceComboBox";
			this.deviceComboBox.Size = new System.Drawing.Size(226, 20);
			this.deviceComboBox.TabIndex = 29;
			// 
			// statusStrip1
			// 
			this.statusStrip1.BackColor = System.Drawing.Color.White;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel,
            this.myProgressBar,
            this.progressStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 194);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(564, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 37;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(336, 17);
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
			// HardwareUpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.ClientSize = new System.Drawing.Size(564, 216);
			this.Controls.Add(this.connectPanel);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.fileOpenButton);
			this.Controls.Add(this.pathLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HelpButton = true;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HardwareUpdateForm";
			this.Text = "固件升级";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.HardwareUpdateForm_HelpButtonClicked);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HardwareUpdateForm_FormClosed);
			this.Load += new System.EventHandler(this.UpdateForm_Load);
			this.connectPanel.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Label pathLabel;
		private System.Windows.Forms.Button fileOpenButton;
		private System.Windows.Forms.Panel connectPanel;
		private System.Windows.Forms.Button switchButton;
		private System.Windows.Forms.Button refreshButton;
		private System.Windows.Forms.Button updateButton;
		private System.Windows.Forms.Button deviceConnectButton;
		private System.Windows.Forms.ComboBox deviceComboBox;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.ToolStripProgressBar myProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel progressStatusLabel;
		private System.Windows.Forms.Button versionButton;
	}
}