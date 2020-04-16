namespace LightController.MyForm.Test
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
            this.SerialPortBox = new System.Windows.Forms.ComboBox();
            this.StartTestMode = new System.Windows.Forms.Button();
            this.ReLoadSerialPortBox = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SerialPortBox
            // 
            this.SerialPortBox.FormattingEnabled = true;
            this.SerialPortBox.Location = new System.Drawing.Point(12, 12);
            this.SerialPortBox.Name = "SerialPortBox";
            this.SerialPortBox.Size = new System.Drawing.Size(121, 20);
            this.SerialPortBox.TabIndex = 0;
            // 
            // StartTestMode
            // 
            this.StartTestMode.Location = new System.Drawing.Point(220, 13);
            this.StartTestMode.Name = "StartTestMode";
            this.StartTestMode.Size = new System.Drawing.Size(75, 23);
            this.StartTestMode.TabIndex = 2;
            this.StartTestMode.Text = "启动测试模块";
            this.StartTestMode.UseVisualStyleBackColor = true;
            this.StartTestMode.Click += new System.EventHandler(this.StartTestMode_Click);
            // 
            // ReLoadSerialPortBox
            // 
            this.ReLoadSerialPortBox.Location = new System.Drawing.Point(139, 12);
            this.ReLoadSerialPortBox.Name = "ReLoadSerialPortBox";
            this.ReLoadSerialPortBox.Size = new System.Drawing.Size(75, 24);
            this.ReLoadSerialPortBox.TabIndex = 3;
            this.ReLoadSerialPortBox.Text = "刷新串口";
            this.ReLoadSerialPortBox.UseVisualStyleBackColor = true;
            this.ReLoadSerialPortBox.Click += new System.EventHandler(this.ReLoadSerialPortBox_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ReLoadSerialPortBox);
            this.Controls.Add(this.StartTestMode);
            this.Controls.Add(this.SerialPortBox);
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TestForm";
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.ComboBox SerialPortBox;
        private System.Windows.Forms.Button StartTestMode;
        private System.Windows.Forms.Button ReLoadSerialPortBox;
    }
}