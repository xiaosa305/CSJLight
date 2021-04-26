namespace LightController.MyForm
{
	partial class SpecialForm
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
			this.aiCheckBox = new System.Windows.Forms.CheckBox();
			this.enterButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.pswTB = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.loginButton = new System.Windows.Forms.Button();
			this.remotePortTextBox = new System.Windows.Forms.TextBox();
			this.domainServerTextBox = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.pswPanel = new System.Windows.Forms.Panel();
			this.chPanel = new System.Windows.Forms.Panel();
			this.pswPanel.SuspendLayout();
			this.chPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// aiCheckBox
			// 
			this.aiCheckBox.AutoSize = true;
			this.aiCheckBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.aiCheckBox.Location = new System.Drawing.Point(117, 25);
			this.aiCheckBox.Name = "aiCheckBox";
			this.aiCheckBox.Size = new System.Drawing.Size(36, 16);
			this.aiCheckBox.TabIndex = 69;
			this.aiCheckBox.Text = "是";
			this.aiCheckBox.UseVisualStyleBackColor = true;
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(43, 155);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(63, 31);
			this.enterButton.TabIndex = 74;
			this.enterButton.Text = "修改";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(175, 155);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(63, 31);
			this.cancelButton.TabIndex = 74;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// pswTB
			// 
			this.pswTB.Location = new System.Drawing.Point(39, 88);
			this.pswTB.MaxLength = 10;
			this.pswTB.Name = "pswTB";
			this.pswTB.Size = new System.Drawing.Size(114, 21);
			this.pswTB.TabIndex = 68;
			this.pswTB.UseSystemPasswordChar = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
			this.label4.Location = new System.Drawing.Point(26, 29);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77, 12);
			this.label4.TabIndex = 67;
			this.label4.Text = "是否AI版本：";
			// 
			// loginButton
			// 
			this.loginButton.Location = new System.Drawing.Point(163, 86);
			this.loginButton.Name = "loginButton";
			this.loginButton.Size = new System.Drawing.Size(75, 23);
			this.loginButton.TabIndex = 75;
			this.loginButton.Text = "验证密码";
			this.loginButton.UseVisualStyleBackColor = true;
			this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
			// 
			// remotePortTextBox
			// 
			this.remotePortTextBox.Location = new System.Drawing.Point(117, 107);
			this.remotePortTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.remotePortTextBox.Name = "remotePortTextBox";
			this.remotePortTextBox.Size = new System.Drawing.Size(57, 21);
			this.remotePortTextBox.TabIndex = 78;
			// 
			// domainServerTextBox
			// 
			this.domainServerTextBox.Location = new System.Drawing.Point(117, 65);
			this.domainServerTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.domainServerTextBox.Name = "domainServerTextBox";
			this.domainServerTextBox.Size = new System.Drawing.Size(117, 21);
			this.domainServerTextBox.TabIndex = 79;
			this.domainServerTextBox.Text = "192.168.111.111";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.BackColor = System.Drawing.Color.Transparent;
			this.label11.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
			this.label11.Location = new System.Drawing.Point(26, 113);
			this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(77, 12);
			this.label11.TabIndex = 76;
			this.label11.Text = "服务器端口：";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.BackColor = System.Drawing.Color.Transparent;
			this.label12.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
			this.label12.Location = new System.Drawing.Point(26, 71);
			this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(65, 12);
			this.label12.TabIndex = 77;
			this.label12.Text = "服务器IP：";
			// 
			// pswPanel
			// 
			this.pswPanel.Controls.Add(this.pswTB);
			this.pswPanel.Controls.Add(this.loginButton);
			this.pswPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pswPanel.Location = new System.Drawing.Point(0, 0);
			this.pswPanel.Name = "pswPanel";
			this.pswPanel.Size = new System.Drawing.Size(264, 211);
			this.pswPanel.TabIndex = 80;
			// 
			// chPanel
			// 
			this.chPanel.Controls.Add(this.label4);
			this.chPanel.Controls.Add(this.aiCheckBox);
			this.chPanel.Controls.Add(this.label11);
			this.chPanel.Controls.Add(this.domainServerTextBox);
			this.chPanel.Controls.Add(this.label12);
			this.chPanel.Controls.Add(this.remotePortTextBox);
			this.chPanel.Controls.Add(this.enterButton);
			this.chPanel.Controls.Add(this.cancelButton);
			this.chPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chPanel.Location = new System.Drawing.Point(0, 0);
			this.chPanel.Name = "chPanel";
			this.chPanel.Size = new System.Drawing.Size(264, 211);
			this.chPanel.TabIndex = 76;
			// 
			// SpecialForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.GrayText;
			this.ClientSize = new System.Drawing.Size(264, 211);
			this.Controls.Add(this.pswPanel);
			this.Controls.Add(this.chPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(280, 250);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(280, 250);
			this.Name = "SpecialForm";
			this.Text = "修改高级属性";
			this.Load += new System.EventHandler(this.SpecialForm_Load);
			this.pswPanel.ResumeLayout(false);
			this.pswPanel.PerformLayout();
			this.chPanel.ResumeLayout(false);
			this.chPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.CheckBox aiCheckBox;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox pswTB;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button loginButton;
		private System.Windows.Forms.TextBox remotePortTextBox;
		private System.Windows.Forms.TextBox domainServerTextBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Panel pswPanel;
		private System.Windows.Forms.Panel chPanel;
	}
}