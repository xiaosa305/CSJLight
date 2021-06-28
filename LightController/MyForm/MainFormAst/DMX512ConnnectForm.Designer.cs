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
			this.connectButton = new System.Windows.Forms.Button();
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
			this.myStatusLabel.Size = new System.Drawing.Size(223, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// portRefreshButton
			// 
			this.portRefreshButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.portRefreshButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.portRefreshButton.Location = new System.Drawing.Point(45, 65);
			this.portRefreshButton.Name = "portRefreshButton";
			this.portRefreshButton.Size = new System.Drawing.Size(73, 23);
			this.portRefreshButton.TabIndex = 31;
			this.portRefreshButton.Text = "刷新串口";
			this.portRefreshButton.UseVisualStyleBackColor = false;
			this.portRefreshButton.Click += new System.EventHandler(this.portRefreshButton_Click);
			// 
			// connectButton
			// 
			this.connectButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.connectButton.Enabled = false;
			this.connectButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.connectButton.Location = new System.Drawing.Point(160, 65);
			this.connectButton.Margin = new System.Windows.Forms.Padding(2);
			this.connectButton.Name = "connectButton";
			this.connectButton.Size = new System.Drawing.Size(73, 23);
			this.connectButton.TabIndex = 30;
			this.connectButton.Text = "连接灯具";
			this.connectButton.UseVisualStyleBackColor = false;
			this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// portComboBox
			// 
			this.portComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.portComboBox.Enabled = false;
			this.portComboBox.FormattingEnabled = true;
			this.portComboBox.Location = new System.Drawing.Point(30, 24);
			this.portComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.portComboBox.Name = "portComboBox";
			this.portComboBox.Size = new System.Drawing.Size(213, 20);
			this.portComboBox.TabIndex = 29;
			// 
			// DMX512ConnnectForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Gray;
			this.ClientSize = new System.Drawing.Size(269, 134);
			this.Controls.Add(this.portRefreshButton);
			this.Controls.Add(this.connectButton);
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
		private System.Windows.Forms.Button connectButton;
		private System.Windows.Forms.ComboBox portComboBox;
	}
}