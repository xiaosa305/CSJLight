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
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.StartDrawPicture = new System.Windows.Forms.Button();
            this.ClearPictureBox = new System.Windows.Forms.Button();
            this.NewConnectedTestBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
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
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(232, 91);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(300, 300);
            this.PictureBox.TabIndex = 4;
            this.PictureBox.TabStop = false;
            // 
            // StartDrawPicture
            // 
            this.StartDrawPicture.Location = new System.Drawing.Point(32, 234);
            this.StartDrawPicture.Name = "StartDrawPicture";
            this.StartDrawPicture.Size = new System.Drawing.Size(75, 23);
            this.StartDrawPicture.TabIndex = 5;
            this.StartDrawPicture.Text = "开始绘制";
            this.StartDrawPicture.UseVisualStyleBackColor = true;
            this.StartDrawPicture.Click += new System.EventHandler(this.StartDrawPicture_Click);
            // 
            // ClearPictureBox
            // 
            this.ClearPictureBox.Location = new System.Drawing.Point(32, 280);
            this.ClearPictureBox.Name = "ClearPictureBox";
            this.ClearPictureBox.Size = new System.Drawing.Size(75, 23);
            this.ClearPictureBox.TabIndex = 6;
            this.ClearPictureBox.Text = "清空画布";
            this.ClearPictureBox.UseVisualStyleBackColor = true;
            this.ClearPictureBox.Click += new System.EventHandler(this.ClearPictureBox_Click);
            // 
            // NewConnectedTestBtn
            // 
            this.NewConnectedTestBtn.Location = new System.Drawing.Point(32, 91);
            this.NewConnectedTestBtn.Name = "NewConnectedTestBtn";
            this.NewConnectedTestBtn.Size = new System.Drawing.Size(75, 23);
            this.NewConnectedTestBtn.TabIndex = 7;
            this.NewConnectedTestBtn.Text = "新网络测试";
            this.NewConnectedTestBtn.UseVisualStyleBackColor = true;
            this.NewConnectedTestBtn.Click += new System.EventHandler(this.NewConnectedTestBtn_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.NewConnectedTestBtn);
            this.Controls.Add(this.ClearPictureBox);
            this.Controls.Add(this.StartDrawPicture);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.ReLoadSerialPortBox);
            this.Controls.Add(this.StartTestMode);
            this.Controls.Add(this.SerialPortBox);
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TestForm";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.ComboBox SerialPortBox;
        private System.Windows.Forms.Button StartTestMode;
        private System.Windows.Forms.Button ReLoadSerialPortBox;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Button StartDrawPicture;
        private System.Windows.Forms.Button ClearPictureBox;
        private System.Windows.Forms.Button NewConnectedTestBtn;
    }
}