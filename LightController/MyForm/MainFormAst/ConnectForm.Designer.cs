namespace LightController.MyForm.MainFormAst
{
	partial class ConnectForm
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
			this.deviceComboBox = new System.Windows.Forms.ComboBox();
			this.deviceConnectButton = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.deviceRestartButton = new System.Windows.Forms.Button();
			this.deviceRefreshButton = new System.Windows.Forms.Button();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// deviceComboBox
			// 
			this.deviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.deviceComboBox.Enabled = false;
			this.deviceComboBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.deviceComboBox.FormattingEnabled = true;
			this.deviceComboBox.Location = new System.Drawing.Point(21, 23);
			this.deviceComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.deviceComboBox.Name = "deviceComboBox";
			this.deviceComboBox.Size = new System.Drawing.Size(232, 20);
			this.deviceComboBox.TabIndex = 24;
			// 
			// deviceConnectButton
			// 
			this.deviceConnectButton.BackColor = System.Drawing.Color.Tomato;
			this.deviceConnectButton.Enabled = false;
			this.deviceConnectButton.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.deviceConnectButton.Location = new System.Drawing.Point(156, 62);
			this.deviceConnectButton.Margin = new System.Windows.Forms.Padding(2);
			this.deviceConnectButton.Name = "deviceConnectButton";
			this.deviceConnectButton.Size = new System.Drawing.Size(88, 31);
			this.deviceConnectButton.TabIndex = 26;
			this.deviceConnectButton.Text = "连接设备";
			this.deviceConnectButton.UseVisualStyleBackColor = false;
			this.deviceConnectButton.Click += new System.EventHandler(this.deviceConnectButton_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 112);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(269, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 27;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(366, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// deviceRestartButton
			// 
			this.deviceRestartButton.Enabled = false;
			this.deviceRestartButton.Location = new System.Drawing.Point(275, 23);
			this.deviceRestartButton.Name = "deviceRestartButton";
			this.deviceRestartButton.Size = new System.Drawing.Size(68, 70);
			this.deviceRestartButton.TabIndex = 28;
			this.deviceRestartButton.Text = "设备重启";
			this.deviceRestartButton.UseVisualStyleBackColor = true;
			this.deviceRestartButton.Visible = false;
			this.deviceRestartButton.Click += new System.EventHandler(this.restartButton_Click);
			// 
			// deviceRefreshButton
			// 
			this.deviceRefreshButton.Location = new System.Drawing.Point(33, 63);
			this.deviceRefreshButton.Name = "deviceRefreshButton";
			this.deviceRefreshButton.Size = new System.Drawing.Size(88, 31);
			this.deviceRefreshButton.TabIndex = 29;
			this.deviceRefreshButton.Text = "刷新列表";
			this.deviceRefreshButton.UseVisualStyleBackColor = true;
			this.deviceRefreshButton.Click += new System.EventHandler(this.deviceRefreshButton_Click);
			// 
			// ConnectForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.ClientSize = new System.Drawing.Size(269, 134);
			this.Controls.Add(this.deviceRestartButton);
			this.Controls.Add(this.deviceRefreshButton);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.deviceComboBox);
			this.Controls.Add(this.deviceConnectButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConnectForm";
			this.Text = "设备连接";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.ConnectForm_HelpButtonClicked);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConnectForm_FormClosed);
			this.Load += new System.EventHandler(this.ConnectForm_Load);
			this.Shown += new System.EventHandler(this.ConnectForm_Shown);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ComboBox deviceComboBox;
		private System.Windows.Forms.Button deviceConnectButton;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.Button deviceRestartButton;
		private System.Windows.Forms.Button deviceRefreshButton;
	}
}