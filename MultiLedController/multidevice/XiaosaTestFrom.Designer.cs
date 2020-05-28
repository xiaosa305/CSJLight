namespace MultiLedController.multidevice
{
    partial class XiaosaTestFrom
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
            this.TestBtn1 = new System.Windows.Forms.Button();
            this.TestBtn2 = new System.Windows.Forms.Button();
            this.TestBtn3 = new System.Windows.Forms.Button();
            this.TestBtn4 = new System.Windows.Forms.Button();
            this.TestBtn5 = new System.Windows.Forms.Button();
            this.TestBtn6 = new System.Windows.Forms.Button();
            this.TestBtn7 = new System.Windows.Forms.Button();
            this.TestBtn8 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TestBtn1
            // 
            this.TestBtn1.Location = new System.Drawing.Point(62, 33);
            this.TestBtn1.Name = "TestBtn1";
            this.TestBtn1.Size = new System.Drawing.Size(75, 23);
            this.TestBtn1.TabIndex = 0;
            this.TestBtn1.Text = "搜索设备";
            this.TestBtn1.UseVisualStyleBackColor = true;
            this.TestBtn1.Click += new System.EventHandler(this.TestBtn1_Click);
            // 
            // TestBtn2
            // 
            this.TestBtn2.Location = new System.Drawing.Point(62, 94);
            this.TestBtn2.Name = "TestBtn2";
            this.TestBtn2.Size = new System.Drawing.Size(75, 23);
            this.TestBtn2.TabIndex = 1;
            this.TestBtn2.Text = "添加虚拟控制卡";
            this.TestBtn2.UseVisualStyleBackColor = true;
            this.TestBtn2.Click += new System.EventHandler(this.TestBtn2_Click);
            // 
            // TestBtn3
            // 
            this.TestBtn3.Location = new System.Drawing.Point(62, 157);
            this.TestBtn3.Name = "TestBtn3";
            this.TestBtn3.Size = new System.Drawing.Size(129, 23);
            this.TestBtn3.TabIndex = 2;
            this.TestBtn3.Text = "开始接收DMX数据";
            this.TestBtn3.UseVisualStyleBackColor = true;
            this.TestBtn3.Click += new System.EventHandler(this.TestBtn3_Click);
            // 
            // TestBtn4
            // 
            this.TestBtn4.Location = new System.Drawing.Point(62, 266);
            this.TestBtn4.Name = "TestBtn4";
            this.TestBtn4.Size = new System.Drawing.Size(75, 23);
            this.TestBtn4.TabIndex = 3;
            this.TestBtn4.Text = "开始录制";
            this.TestBtn4.UseVisualStyleBackColor = true;
            this.TestBtn4.Click += new System.EventHandler(this.TestBtn4_Click);
            // 
            // TestBtn5
            // 
            this.TestBtn5.Location = new System.Drawing.Point(157, 266);
            this.TestBtn5.Name = "TestBtn5";
            this.TestBtn5.Size = new System.Drawing.Size(75, 23);
            this.TestBtn5.TabIndex = 4;
            this.TestBtn5.Text = "停止录制";
            this.TestBtn5.UseVisualStyleBackColor = true;
            this.TestBtn5.Click += new System.EventHandler(this.TestBtn5_Click);
            // 
            // TestBtn6
            // 
            this.TestBtn6.Location = new System.Drawing.Point(214, 157);
            this.TestBtn6.Name = "TestBtn6";
            this.TestBtn6.Size = new System.Drawing.Size(133, 23);
            this.TestBtn6.TabIndex = 5;
            this.TestBtn6.Text = "停止接收DMX数据";
            this.TestBtn6.UseVisualStyleBackColor = true;
            this.TestBtn6.Click += new System.EventHandler(this.TestBtn6_Click);
            // 
            // TestBtn7
            // 
            this.TestBtn7.Location = new System.Drawing.Point(62, 211);
            this.TestBtn7.Name = "TestBtn7";
            this.TestBtn7.Size = new System.Drawing.Size(75, 23);
            this.TestBtn7.TabIndex = 6;
            this.TestBtn7.Text = "开始实时调试";
            this.TestBtn7.UseVisualStyleBackColor = true;
            this.TestBtn7.Click += new System.EventHandler(this.TestBtn7_Click);
            // 
            // TestBtn8
            // 
            this.TestBtn8.Location = new System.Drawing.Point(157, 211);
            this.TestBtn8.Name = "TestBtn8";
            this.TestBtn8.Size = new System.Drawing.Size(75, 23);
            this.TestBtn8.TabIndex = 7;
            this.TestBtn8.Text = "停止实时调试";
            this.TestBtn8.UseVisualStyleBackColor = true;
            this.TestBtn8.Click += new System.EventHandler(this.TestBtn8_Click);
            // 
            // XiaosaTestFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TestBtn8);
            this.Controls.Add(this.TestBtn7);
            this.Controls.Add(this.TestBtn6);
            this.Controls.Add(this.TestBtn5);
            this.Controls.Add(this.TestBtn4);
            this.Controls.Add(this.TestBtn3);
            this.Controls.Add(this.TestBtn2);
            this.Controls.Add(this.TestBtn1);
            this.Name = "XiaosaTestFrom";
            this.Text = "XiaosaTestFrom";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TestBtn1;
        private System.Windows.Forms.Button TestBtn2;
        private System.Windows.Forms.Button TestBtn3;
        private System.Windows.Forms.Button TestBtn4;
        private System.Windows.Forms.Button TestBtn5;
        private System.Windows.Forms.Button TestBtn6;
        private System.Windows.Forms.Button TestBtn7;
        private System.Windows.Forms.Button TestBtn8;
    }
}