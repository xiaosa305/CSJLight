namespace LightController
{
	partial class NewForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.enterButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(34, 49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "工程名：";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(107, 39);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(193, 25);
			this.textBox1.TabIndex = 1;
			// 
			// enterButton
			// 
			this.enterButton.BackColor = System.Drawing.SystemColors.Info;
			this.enterButton.Location = new System.Drawing.Point(51, 92);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(96, 33);
			this.enterButton.TabIndex = 2;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = false;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.cancelButton.Location = new System.Drawing.Point(195, 92);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(96, 33);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = false;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// NewForm
			// 
			this.AcceptButton = this.enterButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(353, 155);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MinimizeBox = false;
			this.Name = "NewForm";
			this.Text = "新建工程";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button cancelButton;
	}
}