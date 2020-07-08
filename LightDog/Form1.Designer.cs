namespace LightDog
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.TestBtn1 = new System.Windows.Forms.Button();
			this.TestBtn2 = new System.Windows.Forms.Button();
			this.NewPassword = new System.Windows.Forms.TextBox();
			this.TIme = new System.Windows.Forms.TextBox();
			this.TestBtn3 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.TestBtn4 = new System.Windows.Forms.Button();
			this.OldPassword = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// TestBtn1
			// 
			this.TestBtn1.Location = new System.Drawing.Point(508, 43);
			this.TestBtn1.Name = "TestBtn1";
			this.TestBtn1.Size = new System.Drawing.Size(75, 23);
			this.TestBtn1.TabIndex = 0;
			this.TestBtn1.Text = "设备探测";
			this.TestBtn1.UseVisualStyleBackColor = true;
			this.TestBtn1.Click += new System.EventHandler(this.TestBtn1_Click);
			// 
			// TestBtn2
			// 
			this.TestBtn2.Location = new System.Drawing.Point(508, 131);
			this.TestBtn2.Name = "TestBtn2";
			this.TestBtn2.Size = new System.Drawing.Size(75, 23);
			this.TestBtn2.TabIndex = 1;
			this.TestBtn2.Text = "设置密码";
			this.TestBtn2.UseVisualStyleBackColor = true;
			this.TestBtn2.Click += new System.EventHandler(this.TestBtn2_Click);
			// 
			// NewPassword
			// 
			this.NewPassword.Location = new System.Drawing.Point(243, 43);
			this.NewPassword.Name = "NewPassword";
			this.NewPassword.Size = new System.Drawing.Size(100, 21);
			this.NewPassword.TabIndex = 2;
			// 
			// TIme
			// 
			this.TIme.Location = new System.Drawing.Point(243, 87);
			this.TIme.Name = "TIme";
			this.TIme.Size = new System.Drawing.Size(100, 21);
			this.TIme.TabIndex = 3;
			// 
			// TestBtn3
			// 
			this.TestBtn3.Location = new System.Drawing.Point(508, 169);
			this.TestBtn3.Name = "TestBtn3";
			this.TestBtn3.Size = new System.Drawing.Size(75, 23);
			this.TestBtn3.TabIndex = 4;
			this.TestBtn3.Text = "设置时间";
			this.TestBtn3.UseVisualStyleBackColor = true;
			this.TestBtn3.Click += new System.EventHandler(this.TestBtn3_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(177, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "新密码";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(189, 90);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "时间";
			// 
			// TestBtn4
			// 
			this.TestBtn4.Location = new System.Drawing.Point(508, 79);
			this.TestBtn4.Name = "TestBtn4";
			this.TestBtn4.Size = new System.Drawing.Size(75, 23);
			this.TestBtn4.TabIndex = 7;
			this.TestBtn4.Text = "密码检测";
			this.TestBtn4.UseVisualStyleBackColor = true;
			this.TestBtn4.Click += new System.EventHandler(this.TestBtn4_Click);
			// 
			// OldPassword
			// 
			this.OldPassword.Location = new System.Drawing.Point(243, 133);
			this.OldPassword.Name = "OldPassword";
			this.OldPassword.Size = new System.Drawing.Size(100, 21);
			this.OldPassword.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(177, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 9;
			this.label3.Text = "旧密码";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.OldPassword);
			this.Controls.Add(this.TestBtn4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TestBtn3);
			this.Controls.Add(this.TIme);
			this.Controls.Add(this.NewPassword);
			this.Controls.Add(this.TestBtn2);
			this.Controls.Add(this.TestBtn1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button TestBtn1;
        private System.Windows.Forms.Button TestBtn2;
        private System.Windows.Forms.TextBox NewPassword;
        private System.Windows.Forms.TextBox TIme;
        private System.Windows.Forms.Button TestBtn3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button TestBtn4;
        private System.Windows.Forms.TextBox OldPassword;
        private System.Windows.Forms.Label label3;
    }
}

