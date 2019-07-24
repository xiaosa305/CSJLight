namespace LightController.MyForm
{
	partial class NewHardwareForm
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
			this.hNameTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.enterButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// hNameTextBox
			// 
			this.hNameTextBox.Location = new System.Drawing.Point(147, 35);
			this.hNameTextBox.Name = "hNameTextBox";
			this.hNameTextBox.Size = new System.Drawing.Size(193, 25);
			this.hNameTextBox.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(44, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "硬件配置名：";
			// 
			// cancelButton
			// 
			this.cancelButton.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.cancelButton.Location = new System.Drawing.Point(223, 94);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(96, 33);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = false;
			// 
			// enterButton
			// 
			this.enterButton.BackColor = System.Drawing.Color.AntiqueWhite;
			this.enterButton.Location = new System.Drawing.Point(69, 94);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(96, 33);
			this.enterButton.TabIndex = 5;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = false;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// NewHardwareForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(391, 153);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.hNameTextBox);
			this.Controls.Add(this.label1);
			this.Name = "NewHardwareForm";
			this.Text = "新建硬件配置名称";
			this.Load += new System.EventHandler(this.NewHardwareForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox hNameTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button enterButton;
	}
}