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
			this.lightTabPage = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.middleTabPage = new System.Windows.Forms.TabPage();
			this.keyTabPage = new System.Windows.Forms.TabPage();
			this.tabControl1.SuspendLayout();
			this.lightTabPage.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.lightTabPage);
			this.tabControl1.Controls.Add(this.middleTabPage);
			this.tabControl1.Controls.Add(this.keyTabPage);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1224, 763);
			this.tabControl1.TabIndex = 0;
			// 
			// lightTabPage
			// 
			this.lightTabPage.Controls.Add(this.panel1);
			this.lightTabPage.Controls.Add(this.addButton);
			this.lightTabPage.Location = new System.Drawing.Point(4, 22);
			this.lightTabPage.Name = "lightTabPage";
			this.lightTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.lightTabPage.Size = new System.Drawing.Size(1216, 737);
			this.lightTabPage.TabIndex = 0;
			this.lightTabPage.Text = "灯控配置";
			this.lightTabPage.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(183, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1030, 731);
			this.panel1.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(18, 15);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(198, 116);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
			this.button1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button1_MouseMove);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(222, 15);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(198, 116);
			this.button2.TabIndex = 0;
			this.button2.Text = "button1";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
			this.button2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button1_MouseMove);
			// 
			// addButton
			// 
			this.addButton.Location = new System.Drawing.Point(8, 6);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(169, 66);
			this.addButton.TabIndex = 1;
			this.addButton.Text = "addButton";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// middleTabPage
			// 
			this.middleTabPage.Location = new System.Drawing.Point(4, 22);
			this.middleTabPage.Name = "middleTabPage";
			this.middleTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.middleTabPage.Size = new System.Drawing.Size(1216, 737);
			this.middleTabPage.TabIndex = 1;
			this.middleTabPage.Text = "tabPage2";
			this.middleTabPage.UseVisualStyleBackColor = true;
			// 
			// keyTabPage
			// 
			this.keyTabPage.Location = new System.Drawing.Point(4, 22);
			this.keyTabPage.Name = "keyTabPage";
			this.keyTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.keyTabPage.Size = new System.Drawing.Size(1216, 737);
			this.keyTabPage.TabIndex = 2;
			this.keyTabPage.Text = "墙板配置";
			this.keyTabPage.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1224, 763);
			this.Controls.Add(this.tabControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.tabControl1.ResumeLayout(false);
			this.lightTabPage.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage lightTabPage;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TabPage middleTabPage;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabPage keyTabPage;
	}
}

