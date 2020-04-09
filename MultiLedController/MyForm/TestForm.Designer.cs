namespace MultiLedController
{
	partial class TestForm
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
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.getIPButton = new System.Windows.Forms.Button();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(344, 54);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "ping";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.pingButton_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(190, 54);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(134, 21);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "192.168.31.";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 428);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(800, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(785, 17);
			this.myStatusLabel.Spring = true;
			// 
			// getIPButton
			// 
			this.getIPButton.Location = new System.Drawing.Point(344, 96);
			this.getIPButton.Name = "getIPButton";
			this.getIPButton.Size = new System.Drawing.Size(75, 23);
			this.getIPButton.TabIndex = 3;
			this.getIPButton.Text = "button";
			this.getIPButton.UseVisualStyleBackColor = true;
			// 
			// TestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.getIPButton);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button1);
			this.Name = "TestForm";
			this.Text = "TestForm";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.Button getIPButton;
	}
}