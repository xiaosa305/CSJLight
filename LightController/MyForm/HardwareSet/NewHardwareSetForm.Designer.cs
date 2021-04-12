namespace LightController.MyForm.HardwareSet
{
	partial class NewHardwareSetForm
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
			this.panel1 = new System.Windows.Forms.Panel();
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
			this.saveButton = new System.Windows.Forms.Button();
			this.loadButton = new System.Windows.Forms.Button();
			this.statusStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 282);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(535, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(520, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(535, 216);
			this.panel1.TabIndex = 4;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(37, 121);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 17;
			this.label5.Text = "网关：";
			// 
			// deviceNameTextBox
			// 
			this.deviceNameTextBox.Location = new System.Drawing.Point(116, 32);
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
			this.macCheckBox.Location = new System.Drawing.Point(307, 169);
			this.macCheckBox.Name = "macCheckBox";
			this.macCheckBox.Size = new System.Drawing.Size(114, 16);
			this.macCheckBox.TabIndex = 15;
			this.macCheckBox.Text = "自动获取MAC地址";
			this.macCheckBox.UseVisualStyleBackColor = true;
			// 
			// gatewayTextBox
			// 
			this.gatewayTextBox.Location = new System.Drawing.Point(116, 117);
			this.gatewayTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.gatewayTextBox.Name = "gatewayTextBox";
			this.gatewayTextBox.Size = new System.Drawing.Size(124, 21);
			this.gatewayTextBox.TabIndex = 11;
			this.gatewayTextBox.Text = "192.168.2.1";
			// 
			// macTextBox
			// 
			this.macTextBox.Location = new System.Drawing.Point(116, 168);
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
			this.label10.Location = new System.Drawing.Point(37, 169);
			this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(59, 12);
			this.label10.TabIndex = 6;
			this.label10.Text = "Mac地址：";
			// 
			// IPTextBox
			// 
			this.IPTextBox.Location = new System.Drawing.Point(116, 81);
			this.IPTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.IPTextBox.Name = "IPTextBox";
			this.IPTextBox.Size = new System.Drawing.Size(124, 21);
			this.IPTextBox.TabIndex = 13;
			this.IPTextBox.Text = "192.168,2.10";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(37, 85);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 12);
			this.label6.TabIndex = 7;
			this.label6.Text = "IP地址：";
			// 
			// netmaskTextBox
			// 
			this.netmaskTextBox.Location = new System.Drawing.Point(384, 81);
			this.netmaskTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.netmaskTextBox.Name = "netmaskTextBox";
			this.netmaskTextBox.Size = new System.Drawing.Size(109, 21);
			this.netmaskTextBox.TabIndex = 14;
			this.netmaskTextBox.Text = "255.255.255.0";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(305, 85);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(65, 12);
			this.label7.TabIndex = 8;
			this.label7.Text = "子网掩码：";
			// 
			// downloadButton
			// 
			this.downloadButton.BackColor = System.Drawing.Color.Transparent;
			this.downloadButton.Location = new System.Drawing.Point(417, 231);
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
			this.readButton.Location = new System.Drawing.Point(307, 231);
			this.readButton.Margin = new System.Windows.Forms.Padding(2);
			this.readButton.Name = "readButton";
			this.readButton.Size = new System.Drawing.Size(91, 36);
			this.readButton.TabIndex = 29;
			this.readButton.Text = "从设备回读";
			this.readButton.UseVisualStyleBackColor = false;
			this.readButton.Click += new System.EventHandler(this.readButton_Click);
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(37, 231);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(91, 36);
			this.saveButton.TabIndex = 30;
			this.saveButton.Text = "保存配置";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// loadButton
			// 
			this.loadButton.Location = new System.Drawing.Point(149, 231);
			this.loadButton.Name = "loadButton";
			this.loadButton.Size = new System.Drawing.Size(91, 36);
			this.loadButton.TabIndex = 30;
			this.loadButton.Text = "打开配置";
			this.loadButton.UseVisualStyleBackColor = true;
			this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
			// 
			// NewHardwareSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.ClientSize = new System.Drawing.Size(535, 304);
			this.Controls.Add(this.loadButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.downloadButton);
			this.Controls.Add(this.readButton);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.statusStrip1);
			this.Name = "NewHardwareSetForm";
			this.Text = "硬件设置(新)";
			this.Load += new System.EventHandler(this.NewHardwareSet_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.Panel panel1;
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
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button loadButton;
	}
}