namespace LightController.MyForm.MainFormAst
{
	partial class DMX512ConnnectForm
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
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.portRefreshButton = new System.Windows.Forms.Button();
			this.portConnectButton = new System.Windows.Forms.Button();
			this.portComboBox = new System.Windows.Forms.ComboBox();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 112);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(269, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 28;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(254, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// portRefreshButton
			// 
			this.portRefreshButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.portRefreshButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.portRefreshButton.Location = new System.Drawing.Point(32, 65);
			this.portRefreshButton.Name = "portRefreshButton";
			this.portRefreshButton.Size = new System.Drawing.Size(88, 30);
			this.portRefreshButton.TabIndex = 31;
			this.portRefreshButton.Text = "刷新串口";
			this.portRefreshButton.UseVisualStyleBackColor = false;
			this.portRefreshButton.Click += new System.EventHandler(this.portRefreshButton_Click);
			// 
			// portConnectButton
			// 
			this.portConnectButton.BackColor = System.Drawing.Color.Tomato;
			this.portConnectButton.Enabled = false;
			this.portConnectButton.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.portConnectButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.portConnectButton.Location = new System.Drawing.Point(160, 65);
			this.portConnectButton.Margin = new System.Windows.Forms.Padding(2);
			this.portConnectButton.Name = "portConnectButton";
			this.portConnectButton.Size = new System.Drawing.Size(88, 30);
			this.portConnectButton.TabIndex = 30;
			this.portConnectButton.Text = "连接灯具";
			this.portConnectButton.UseVisualStyleBackColor = false;
			this.portConnectButton.Click += new System.EventHandler(this.portConnectButton_Click);
			// 
			// portComboBox
			// 
			this.portComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.portComboBox.Enabled = false;
			this.portComboBox.FormattingEnabled = true;
			this.portComboBox.Location = new System.Drawing.Point(21, 23);
			this.portComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.portComboBox.Name = "portComboBox";
			this.portComboBox.Size = new System.Drawing.Size(232, 20);
			this.portComboBox.TabIndex = 29;
			// 
			// DMX512ConnnectForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Gray;
			this.ClientSize = new System.Drawing.Size(269, 134);
			this.Controls.Add(this.portRefreshButton);
			this.Controls.Add(this.portConnectButton);
			this.Controls.Add(this.portComboBox);
			this.Controls.Add(this.statusStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "DMX512ConnnectForm";
			this.Text = "《DMX512调试线》直连灯具";
			this.Load += new System.EventHandler(this.DMX512ConnnectForm_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.Button portRefreshButton;
		private System.Windows.Forms.Button portConnectButton;
		private System.Windows.Forms.ComboBox portComboBox;
	}
}