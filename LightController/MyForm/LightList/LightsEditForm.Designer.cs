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
			this.startNUD = new System.Windows.Forms.NumericUpDown();
			this.addrLabel2 = new System.Windows.Forms.Label();
			this.nameLabel2 = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.oldAddrLabel = new System.Windows.Forms.Label();
			this.addrLabel = new System.Windows.Forms.Label();
			this.noticePanel = new System.Windows.Forms.Panel();
			this.cancelButton = new System.Windows.Forms.Button();
			this.noticeLabel = new System.Windows.Forms.Label();
			this.iSeeButton = new System.Windows.Forms.Button();
			this.enterButton = new System.Windows.Forms.Button();
			this.cancelButton2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.startNUD)).BeginInit();
			this.noticePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// startNUD
			// 
			this.startNUD.Location = new System.Drawing.Point(149, 108);
			this.startNUD.Margin = new System.Windows.Forms.Padding(2);
			this.startNUD.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
			this.startNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.startNUD.Name = "startNUD";
			this.startNUD.Size = new System.Drawing.Size(82, 21);
			this.startNUD.TabIndex = 8;
			this.startNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.startNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// addrLabel2
			// 
			this.addrLabel2.AutoSize = true;
			this.addrLabel2.Location = new System.Drawing.Point(32, 108);
			this.addrLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.addrLabel2.Name = "addrLabel2";
			this.addrLabel2.Size = new System.Drawing.Size(65, 12);
			this.addrLabel2.TabIndex = 4;
			this.addrLabel2.Text = "起始地址：";
			// 
			// nameLabel2
			// 
			this.nameLabel2.AutoSize = true;
			this.nameLabel2.Location = new System.Drawing.Point(32, 30);
			this.nameLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.nameLabel2.Name = "nameLabel2";
			this.nameLabel2.Size = new System.Drawing.Size(65, 12);
			this.nameLabel2.TabIndex = 4;
			this.nameLabel2.Text = "灯具信息：";
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(122, 30);
			this.nameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(41, 12);
			this.nameLabel.TabIndex = 9;
			this.nameLabel.Text = "label3";
			// 
			// oldAddrLabel
			// 
			this.oldAddrLabel.AutoSize = true;
			this.oldAddrLabel.Location = new System.Drawing.Point(32, 69);
			this.oldAddrLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.oldAddrLabel.Name = "oldAddrLabel";
			this.oldAddrLabel.Size = new System.Drawing.Size(77, 12);
			this.oldAddrLabel.TabIndex = 4;
			this.oldAddrLabel.Text = "原灯具地址：";
			// 
			// addrLabel
			// 
			this.addrLabel.AutoSize = true;
			this.addrLabel.Location = new System.Drawing.Point(149, 69);
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
			this.noticePanel.Controls.Add(this.noticeLabel);
			this.noticePanel.Controls.Add(this.iSeeButton);
			this.noticePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.noticePanel.Location = new System.Drawing.Point(0, 0);
			this.noticePanel.Margin = new System.Windows.Forms.Padding(2);
			this.noticePanel.Name = "noticePanel";
			this.noticePanel.Size = new System.Drawing.Size(287, 211);
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
			// noticeLabel
			// 
			this.noticeLabel.BackColor = System.Drawing.Color.Transparent;
			this.noticeLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.noticeLabel.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.noticeLabel.Location = new System.Drawing.Point(0, 0);
			this.noticeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.noticeLabel.Name = "noticeLabel";
			this.noticeLabel.Size = new System.Drawing.Size(287, 120);
			this.noticeLabel.TabIndex = 0;
			this.noticeLabel.Text = "若改变了灯具通道地址，基于原地址编辑的步数信息将会消失，请谨慎操作！";
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
			this.cancelButton2.Location = new System.Drawing.Point(169, 151);
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
			this.ClientSize = new System.Drawing.Size(287, 211);
			this.Controls.Add(this.noticePanel);
			this.Controls.Add(this.startNUD);
			this.Controls.Add(this.oldAddrLabel);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.nameLabel2);
			this.Controls.Add(this.addrLabel);
			this.Controls.Add(this.addrLabel2);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.cancelButton2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "LightsEditForm";
			this.Text = "修改灯具地址";
			this.Load += new System.EventHandler(this.LightsEditForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.startNUD)).EndInit();
			this.noticePanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown startNUD;
		private System.Windows.Forms.Label addrLabel2;
		private System.Windows.Forms.Label nameLabel2;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label oldAddrLabel;
		private System.Windows.Forms.Label addrLabel;
		private System.Windows.Forms.Panel noticePanel;
		private System.Windows.Forms.Label noticeLabel;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button cancelButton2;
		private System.Windows.Forms.Button iSeeButton;
		private System.Windows.Forms.Button cancelButton;
	}
}