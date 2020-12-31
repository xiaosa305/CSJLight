namespace LightController.MyForm
{
	partial class LightsEditForm
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
			this.startCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.nameTypeLabel = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.addrLabel = new System.Windows.Forms.Label();
			this.noticePanel = new System.Windows.Forms.Panel();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.iSeeButton = new System.Windows.Forms.Button();
			this.enterButton = new System.Windows.Forms.Button();
			this.cancelButton2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.startCountNumericUpDown)).BeginInit();
			this.noticePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// startCountNumericUpDown
			// 
			this.startCountNumericUpDown.Location = new System.Drawing.Point(126, 108);
			this.startCountNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.startCountNumericUpDown.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
			this.startCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.startCountNumericUpDown.Name = "startCountNumericUpDown";
			this.startCountNumericUpDown.Size = new System.Drawing.Size(82, 21);
			this.startCountNumericUpDown.TabIndex = 8;
			this.startCountNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.startCountNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(32, 108);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "起始地址：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(32, 30);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 4;
			this.label1.Text = "灯具信息：";
			// 
			// nameTypeLabel
			// 
			this.nameTypeLabel.AutoSize = true;
			this.nameTypeLabel.Location = new System.Drawing.Point(126, 30);
			this.nameTypeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.nameTypeLabel.Name = "nameTypeLabel";
			this.nameTypeLabel.Size = new System.Drawing.Size(41, 12);
			this.nameTypeLabel.TabIndex = 9;
			this.nameTypeLabel.Text = "label3";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(32, 69);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77, 12);
			this.label4.TabIndex = 4;
			this.label4.Text = "原灯具地址：";
			// 
			// addrLabel
			// 
			this.addrLabel.AutoSize = true;
			this.addrLabel.Location = new System.Drawing.Point(126, 69);
			this.addrLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.addrLabel.Name = "addrLabel";
			this.addrLabel.Size = new System.Drawing.Size(41, 12);
			this.addrLabel.TabIndex = 9;
			this.addrLabel.Text = "label3";
			// 
			// noticePanel
			// 
			this.noticePanel.BackColor = System.Drawing.Color.Transparent;
			this.noticePanel.Controls.Add(this.cancelButton);
			this.noticePanel.Controls.Add(this.label3);
			this.noticePanel.Controls.Add(this.iSeeButton);
			this.noticePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.noticePanel.Location = new System.Drawing.Point(0, 0);
			this.noticePanel.Margin = new System.Windows.Forms.Padding(2);
			this.noticePanel.Name = "noticePanel";
			this.noticePanel.Size = new System.Drawing.Size(274, 204);
			this.noticePanel.TabIndex = 10;
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(153, 140);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(97, 43);
			this.cancelButton.TabIndex = 12;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(274, 120);
			this.label3.TabIndex = 0;
			this.label3.Text = "若改变了灯具通道地址，基于原地址编辑的步数信息将会消失，请谨慎操作！";
			// 
			// iSeeButton
			// 
			this.iSeeButton.BackColor = System.Drawing.Color.Transparent;
			this.iSeeButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.iSeeButton.Location = new System.Drawing.Point(30, 140);
			this.iSeeButton.Name = "iSeeButton";
			this.iSeeButton.Size = new System.Drawing.Size(97, 43);
			this.iSeeButton.TabIndex = 12;
			this.iSeeButton.Text = "坚持修改";
			this.iSeeButton.UseVisualStyleBackColor = false;
			this.iSeeButton.Click += new System.EventHandler(this.iSeeButton_Click);
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(34, 152);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(75, 27);
			this.enterButton.TabIndex = 12;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// cancelButton2
			// 
			this.cancelButton2.Location = new System.Drawing.Point(149, 152);
			this.cancelButton2.Name = "cancelButton2";
			this.cancelButton2.Size = new System.Drawing.Size(75, 27);
			this.cancelButton2.TabIndex = 12;
			this.cancelButton2.Text = "取消";
			this.cancelButton2.UseVisualStyleBackColor = true;
			this.cancelButton2.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// LightsEditForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(274, 204);
			this.Controls.Add(this.noticePanel);
			this.Controls.Add(this.addrLabel);
			this.Controls.Add(this.nameTypeLabel);
			this.Controls.Add(this.startCountNumericUpDown);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.cancelButton2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "LightsEditForm";
			this.Text = "修改灯具地址";
			this.Load += new System.EventHandler(this.LightsEditForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.startCountNumericUpDown)).EndInit();
			this.noticePanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown startCountNumericUpDown;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label nameTypeLabel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label addrLabel;
		private System.Windows.Forms.Panel noticePanel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button cancelButton2;
		private System.Windows.Forms.Button iSeeButton;
		private System.Windows.Forms.Button cancelButton;
	}
}