namespace LightController.MyForm
{
	partial class ProjectUpdateForm
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
			this.pathLabel = new System.Windows.Forms.Label();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.fileOpenButton = new System.Windows.Forms.Button();
			this.clearButton = new System.Windows.Forms.Button();
			this.switchButton = new System.Windows.Forms.Button();
			this.deviceComboBox = new System.Windows.Forms.ComboBox();
			this.deviceConnectButton = new System.Windows.Forms.Button();
			this.refreshButton = new System.Windows.Forms.Button();
			this.updateButton = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.myProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.connectPanel = new System.Windows.Forms.Panel();
			this.statusStrip1.SuspendLayout();
			this.connectPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// pathLabel
			// 
			this.pathLabel.Location = new System.Drawing.Point(139, 17);
			this.pathLabel.Name = "pathLabel";
			this.pathLabel.Size = new System.Drawing.Size(311, 33);
			this.pathLabel.TabIndex = 13;
			// 
			// folderBrowserDialog
			// 
			this.folderBrowserDialog.Description = "请选择工程目录的最后一层（即CSJ目录），本操作会将该目录下的所有文件传给设备。";
			this.folderBrowserDialog.ShowNewFolderButton = false;
			// 
			// fileOpenButton
			// 
			this.fileOpenButton.Location = new System.Drawing.Point(36, 17);
			this.fileOpenButton.Name = "fileOpenButton";
			this.fileOpenButton.Size = new System.Drawing.Size(86, 33);
			this.fileOpenButton.TabIndex = 15;
			this.fileOpenButton.Text = "选择已有工程";
			this.fileOpenButton.UseVisualStyleBackColor = true;
			this.fileOpenButton.Click += new System.EventHandler(this.fileOpenSkinButton_Click);
			// 
			// clearButton
			// 
			this.clearButton.Location = new System.Drawing.Point(469, 17);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(63, 33);
			this.clearButton.TabIndex = 16;
			this.clearButton.Text = "清空";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.clearSkinButton_Click);
			// 
			// switchButton
			// 
			this.switchButton.Location = new System.Drawing.Point(34, 30);
			this.switchButton.Margin = new System.Windows.Forms.Padding(2);
			this.switchButton.Name = "switchButton";
			this.switchButton.Size = new System.Drawing.Size(88, 59);
			this.switchButton.TabIndex = 34;
			this.switchButton.Text = "切换为\r\n网络连接";
			this.switchButton.UseVisualStyleBackColor = true;
			this.switchButton.Click += new System.EventHandler(this.switchButton_Click);
			// 
			// deviceComboBox
			// 
			this.deviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.deviceComboBox.Enabled = false;
			this.deviceComboBox.FormattingEnabled = true;
			this.deviceComboBox.Location = new System.Drawing.Point(164, 30);
			this.deviceComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.deviceComboBox.Name = "deviceComboBox";
			this.deviceComboBox.Size = new System.Drawing.Size(246, 20);
			this.deviceComboBox.TabIndex = 29;
			// 
			// deviceConnectButton
			// 
			this.deviceConnectButton.Enabled = false;
			this.deviceConnectButton.Location = new System.Drawing.Point(301, 63);
			this.deviceConnectButton.Margin = new System.Windows.Forms.Padding(2);
			this.deviceConnectButton.Name = "deviceConnectButton";
			this.deviceConnectButton.Size = new System.Drawing.Size(109, 26);
			this.deviceConnectButton.TabIndex = 31;
			this.deviceConnectButton.Text = "打开串口";
			this.deviceConnectButton.UseVisualStyleBackColor = true;
			this.deviceConnectButton.Click += new System.EventHandler(this.deviceConnectButton_Click);
			// 
			// refreshButton
			// 
			this.refreshButton.Location = new System.Drawing.Point(164, 63);
			this.refreshButton.Margin = new System.Windows.Forms.Padding(2);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(107, 26);
			this.refreshButton.TabIndex = 32;
			this.refreshButton.Text = "刷新串口";
			this.refreshButton.UseVisualStyleBackColor = true;
			this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
			// 
			// updateButton
			// 
			this.updateButton.BackColor = System.Drawing.Color.Chocolate;
			this.updateButton.Enabled = false;
			this.updateButton.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.updateButton.ForeColor = System.Drawing.SystemColors.WindowText;
			this.updateButton.Location = new System.Drawing.Point(450, 30);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(81, 59);
			this.updateButton.TabIndex = 12;
			this.updateButton.Text = "更新工程";
			this.updateButton.UseVisualStyleBackColor = false;
			this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.BackColor = System.Drawing.Color.White;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel,
            this.myProgressBar});
			this.statusStrip1.Location = new System.Drawing.Point(0, 194);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(564, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 35;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(342, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// myProgressBar
			// 
			this.myProgressBar.BackColor = System.Drawing.Color.Black;
			this.myProgressBar.Name = "myProgressBar";
			this.myProgressBar.Size = new System.Drawing.Size(180, 16);
			this.myProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			// 
			// connectPanel
			// 
			this.connectPanel.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.connectPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.connectPanel.Controls.Add(this.switchButton);
			this.connectPanel.Controls.Add(this.refreshButton);
			this.connectPanel.Controls.Add(this.updateButton);
			this.connectPanel.Controls.Add(this.deviceConnectButton);
			this.connectPanel.Controls.Add(this.deviceComboBox);
			this.connectPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.connectPanel.Location = new System.Drawing.Point(0, 65);
			this.connectPanel.Name = "connectPanel";
			this.connectPanel.Size = new System.Drawing.Size(564, 129);
			this.connectPanel.TabIndex = 36;
			// 
			// ProjectUpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DarkGray;
			this.ClientSize = new System.Drawing.Size(564, 216);
			this.Controls.Add(this.connectPanel);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.clearButton);
			this.Controls.Add(this.fileOpenButton);
			this.Controls.Add(this.pathLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProjectUpdateForm";
			this.Text = "更新工程到设备";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProjectUpdateForm_FormClosed);
			this.Load += new System.EventHandler(this.ProjectUpdateForm_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.connectPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label pathLabel;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.Button fileOpenButton;
		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.Button switchButton;
		private System.Windows.Forms.ComboBox deviceComboBox;
		private System.Windows.Forms.Button deviceConnectButton;
		private System.Windows.Forms.Button refreshButton;
		private System.Windows.Forms.Button updateButton;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.ToolStripProgressBar myProgressBar;
		private System.Windows.Forms.Panel connectPanel;
	}
}