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
            this.TestBtn3.Size = new System.Drawing.Size(75, 23);
            this.TestBtn3.TabIndex = 2;
            this.TestBtn3.Text = "开始接收麦爵士数据";
            this.TestBtn3.UseVisualStyleBackColor = true;
            this.TestBtn3.Click += new System.EventHandler(this.TestBtn3_Click);
            // 
            // XiaosaTestFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}