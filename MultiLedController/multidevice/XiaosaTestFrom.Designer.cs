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
            this.SuspendLayout();
            // 
            // TestBtn1
            // 
            this.TestBtn1.Location = new System.Drawing.Point(62, 33);
            this.TestBtn1.Name = "TestBtn1";
            this.TestBtn1.Size = new System.Drawing.Size(75, 23);
            this.TestBtn1.TabIndex = 0;
            this.TestBtn1.Text = "测试按钮1";
            this.TestBtn1.UseVisualStyleBackColor = true;
            this.TestBtn1.Click += new System.EventHandler(this.TestBtn1_Click);
            // 
            // TestBtn2
            // 
            this.TestBtn2.Location = new System.Drawing.Point(62, 94);
            this.TestBtn2.Name = "TestBtn2";
            this.TestBtn2.Size = new System.Drawing.Size(75, 23);
            this.TestBtn2.TabIndex = 1;
            this.TestBtn2.Text = "测试按钮2";
            this.TestBtn2.UseVisualStyleBackColor = true;
            // 
            // XiaosaTestFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TestBtn2);
            this.Controls.Add(this.TestBtn1);
            this.Name = "XiaosaTestFrom";
            this.Text = "XiaosaTestFrom";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TestBtn1;
        private System.Windows.Forms.Button TestBtn2;
    }
}