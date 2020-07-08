namespace LightDog
{
	partial class DogForm
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
			this.myTabControl = new System.Windows.Forms.TabControl();
			this.pswTabPage = new System.Windows.Forms.TabPage();
			this.newPswTextBox = new System.Windows.Forms.TextBox();
			this.pswUpdateButton = new System.Windows.Forms.Button();
			this.timeTabPage = new System.Windows.Forms.TabPage();
			this.timeCheckBox = new System.Windows.Forms.CheckBox();
			this.hourLabel = new System.Windows.Forms.Label();
			this.hourTextBox = new System.Windows.Forms.TextBox();
			this.timeSetButton = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.loginPanel = new System.Windows.Forms.Panel();
			this.loginButton = new System.Windows.Forms.Button();
			this.pswTextBox = new System.Windows.Forms.TextBox();
			this.connectButton1 = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.myTabControl.SuspendLayout();
			this.pswTabPage.SuspendLayout();
			this.timeTabPage.SuspendLayout();
			this.panel1.SuspendLayout();
			this.loginPanel.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// myTabControl
			// 
			this.myTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.myTabControl.Controls.Add(this.pswTabPage);
			this.myTabControl.Controls.Add(this.timeTabPage);
			this.myTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.myTabControl.Enabled = false;
			this.myTabControl.ItemSize = new System.Drawing.Size(60, 30);
			this.myTabControl.Location = new System.Drawing.Point(0, 0);
			this.myTabControl.Name = "myTabControl";
			this.myTabControl.SelectedIndex = 0;
			this.myTabControl.Size = new System.Drawing.Size(384, 179);
			this.myTabControl.TabIndex = 0;
			// 
			// pswTabPage
			// 
			this.pswTabPage.BackColor = System.Drawing.Color.MintCream;
			this.pswTabPage.Controls.Add(this.newPswTextBox);
			this.pswTabPage.Controls.Add(this.pswUpdateButton);
			this.pswTabPage.Location = new System.Drawing.Point(4, 4);
			this.pswTabPage.Name = "pswTabPage";
			this.pswTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.pswTabPage.Size = new System.Drawing.Size(376, 141);
			this.pswTabPage.TabIndex = 0;
			this.pswTabPage.Text = "修改密码";
			// 
			// newPswTextBox
			// 
			this.newPswTextBox.Location = new System.Drawing.Point(102, 78);
			this.newPswTextBox.MaxLength = 8;
			this.newPswTextBox.Name = "newPswTextBox";
			this.newPswTextBox.PasswordChar = '*';
			this.newPswTextBox.Size = new System.Drawing.Size(58, 21);
			this.newPswTextBox.TabIndex = 4;
			this.newPswTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pswTextBox_KeyPress);
			// 
			// pswUpdateButton
			// 
			this.pswUpdateButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pswUpdateButton.Location = new System.Drawing.Point(196, 74);
			this.pswUpdateButton.Name = "pswUpdateButton";
			this.pswUpdateButton.Size = new System.Drawing.Size(75, 27);
			this.pswUpdateButton.TabIndex = 1;
			this.pswUpdateButton.Text = "修改密码";
			this.pswUpdateButton.UseVisualStyleBackColor = true;
			this.pswUpdateButton.Click += new System.EventHandler(this.pswUpdateButton_Click);
			// 
			// timeTabPage
			// 
			this.timeTabPage.BackColor = System.Drawing.Color.Linen;
			this.timeTabPage.Controls.Add(this.timeCheckBox);
			this.timeTabPage.Controls.Add(this.hourLabel);
			this.timeTabPage.Controls.Add(this.hourTextBox);
			this.timeTabPage.Controls.Add(this.timeSetButton);
			this.timeTabPage.Location = new System.Drawing.Point(4, 4);
			this.timeTabPage.Name = "timeTabPage";
			this.timeTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.timeTabPage.Size = new System.Drawing.Size(376, 141);
			this.timeTabPage.TabIndex = 1;
			this.timeTabPage.Text = "设置可用时间";
			// 
			// timeCheckBox
			// 
			this.timeCheckBox.AutoSize = true;
			this.timeCheckBox.Location = new System.Drawing.Point(38, 78);
			this.timeCheckBox.Name = "timeCheckBox";
			this.timeCheckBox.Size = new System.Drawing.Size(72, 16);
			this.timeCheckBox.TabIndex = 5;
			this.timeCheckBox.Text = "不限时间";
			this.timeCheckBox.UseVisualStyleBackColor = true;
			// 
			// hourLabel
			// 
			this.hourLabel.AutoSize = true;
			this.hourLabel.Location = new System.Drawing.Point(207, 80);
			this.hourLabel.Name = "hourLabel";
			this.hourLabel.Size = new System.Drawing.Size(29, 12);
			this.hourLabel.TabIndex = 4;
			this.hourLabel.Text = "小时";
			// 
			// hourTextBox
			// 
			this.hourTextBox.Enabled = false;
			this.hourTextBox.Location = new System.Drawing.Point(143, 76);
			this.hourTextBox.Name = "hourTextBox";
			this.hourTextBox.Size = new System.Drawing.Size(61, 21);
			this.hourTextBox.TabIndex = 1;
			// 
			// timeSetButton
			// 
			this.timeSetButton.BackColor = System.Drawing.Color.Transparent;
			this.timeSetButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.timeSetButton.Location = new System.Drawing.Point(277, 71);
			this.timeSetButton.Name = "timeSetButton";
			this.timeSetButton.Size = new System.Drawing.Size(75, 30);
			this.timeSetButton.TabIndex = 3;
			this.timeSetButton.Text = "设置";
			this.timeSetButton.UseVisualStyleBackColor = false;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this.loginPanel);
			this.panel1.Controls.Add(this.connectButton1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(384, 39);
			this.panel1.TabIndex = 1;
			// 
			// loginPanel
			// 
			this.loginPanel.Controls.Add(this.loginButton);
			this.loginPanel.Controls.Add(this.pswTextBox);
			this.loginPanel.Location = new System.Drawing.Point(219, 4);
			this.loginPanel.Name = "loginPanel";
			this.loginPanel.Size = new System.Drawing.Size(162, 31);
			this.loginPanel.TabIndex = 5;
			// 
			// loginButton
			// 
			this.loginButton.BackColor = System.Drawing.Color.Transparent;
			this.loginButton.Location = new System.Drawing.Point(80, 4);
			this.loginButton.Name = "loginButton";
			this.loginButton.Size = new System.Drawing.Size(75, 23);
			this.loginButton.TabIndex = 3;
			this.loginButton.Text = "密码校验";
			this.loginButton.UseVisualStyleBackColor = false;
			this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
			// 
			// pswTextBox
			// 
			this.pswTextBox.Location = new System.Drawing.Point(6, 5);
			this.pswTextBox.MaxLength = 8;
			this.pswTextBox.Name = "pswTextBox";
			this.pswTextBox.PasswordChar = '*';
			this.pswTextBox.Size = new System.Drawing.Size(58, 21);
			this.pswTextBox.TabIndex = 1;
			this.pswTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pswTextBox_KeyPress);
			// 
			// connectButton1
			// 
			this.connectButton1.BackColor = System.Drawing.Color.LightBlue;
			this.connectButton1.Location = new System.Drawing.Point(11, 8);
			this.connectButton1.Name = "connectButton1";
			this.connectButton1.Size = new System.Drawing.Size(75, 23);
			this.connectButton1.TabIndex = 0;
			this.connectButton1.Text = "设备连接";
			this.connectButton1.UseVisualStyleBackColor = false;
			this.connectButton1.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 179);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(384, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(369, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(384, 201);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.myTabControl);
			this.Controls.Add(this.statusStrip1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DogForm";
			this.Opacity = 0.9D;
			this.Text = "JKC工具";
			this.Load += new System.EventHandler(this.DogForm_Load);
			this.myTabControl.ResumeLayout(false);
			this.pswTabPage.ResumeLayout(false);
			this.pswTabPage.PerformLayout();
			this.timeTabPage.ResumeLayout(false);
			this.timeTabPage.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.loginPanel.ResumeLayout(false);
			this.loginPanel.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl myTabControl;
		private System.Windows.Forms.TabPage pswTabPage;
		private System.Windows.Forms.TabPage timeTabPage;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.Button connectButton1;
		private System.Windows.Forms.Button loginButton;
		private System.Windows.Forms.TextBox pswTextBox;
		private System.Windows.Forms.TextBox hourTextBox;
		private System.Windows.Forms.TextBox newPswTextBox;
		private System.Windows.Forms.Button pswUpdateButton;
		private System.Windows.Forms.Label hourLabel;
		private System.Windows.Forms.Button timeSetButton;
		private System.Windows.Forms.CheckBox timeCheckBox;
		private System.Windows.Forms.Panel loginPanel;
		private System.Windows.Forms.ToolTip myToolTip;
	}
}