namespace OtherTools
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.middleTabPage = new System.Windows.Forms.TabPage();
			this.lightTabPage = new System.Windows.Forms.TabPage();
			this.keyTabPage = new System.Windows.Forms.TabPage();
			this.panel2 = new System.Windows.Forms.Panel();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.keyTabPage.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.middleTabPage);
			this.tabControl1.Controls.Add(this.lightTabPage);
			this.tabControl1.Controls.Add(this.keyTabPage);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1224, 839);
			this.tabControl1.TabIndex = 0;
			// 
			// middleTabPage
			// 
			this.middleTabPage.Location = new System.Drawing.Point(4, 22);
			this.middleTabPage.Name = "middleTabPage";
			this.middleTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.middleTabPage.Size = new System.Drawing.Size(1216, 813);
			this.middleTabPage.TabIndex = 0;
			this.middleTabPage.Text = "中控配制";
			this.middleTabPage.UseVisualStyleBackColor = true;
			// 
			// lightTabPage
			// 
			this.lightTabPage.Location = new System.Drawing.Point(4, 22);
			this.lightTabPage.Name = "lightTabPage";
			this.lightTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.lightTabPage.Size = new System.Drawing.Size(1216, 813);
			this.lightTabPage.TabIndex = 1;
			this.lightTabPage.Text = "灯控配置";
			this.lightTabPage.UseVisualStyleBackColor = true;
			// 
			// keyTabPage
			// 
			this.keyTabPage.Controls.Add(this.panel2);
			this.keyTabPage.Controls.Add(this.button4);
			this.keyTabPage.Location = new System.Drawing.Point(4, 22);
			this.keyTabPage.Name = "keyTabPage";
			this.keyTabPage.Size = new System.Drawing.Size(1216, 813);
			this.keyTabPage.TabIndex = 2;
			this.keyTabPage.Text = "墙板配置";
			this.keyTabPage.UseVisualStyleBackColor = true;
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel2.Controls.Add(this.button3);
			this.panel2.Controls.Add(this.button5);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel2.Location = new System.Drawing.Point(186, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1030, 813);
			this.panel2.TabIndex = 3;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(23, 39);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(198, 116);
			this.button3.TabIndex = 0;
			this.button3.Text = "button3";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(11, 690);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(169, 42);
			this.button4.TabIndex = 1;
			this.button4.Text = "button4";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(287, 39);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(198, 116);
			this.button5.TabIndex = 0;
			this.button5.Text = "button1";
			this.button5.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1224, 839);
			this.Controls.Add(this.tabControl1);
			this.Name = "Form1";
			this.Text = "其它配置";
			this.tabControl1.ResumeLayout(false);
			this.keyTabPage.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage middleTabPage;
		private System.Windows.Forms.TabPage lightTabPage;
		private System.Windows.Forms.TabPage keyTabPage;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
	}
}

