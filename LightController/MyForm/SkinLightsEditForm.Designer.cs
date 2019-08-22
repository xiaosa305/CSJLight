namespace LightController.MyForm
{
	partial class SkinLightsEditForm
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
			this.components = new System.ComponentModel.Container();
			this.startCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.nameTypeLabel = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.addrLabel = new System.Windows.Forms.Label();
			this.noticePanel = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.cancelSkinButton = new CCWin.SkinControl.SkinButton();
			this.iSeeSkinButton = new CCWin.SkinControl.SkinButton();
			this.enterSkinButton = new CCWin.SkinControl.SkinButton();
			this.cancelSkinButton2 = new CCWin.SkinControl.SkinButton();
			((System.ComponentModel.ISupportInitialize)(this.startCountNumericUpDown)).BeginInit();
			this.noticePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// startCountNumericUpDown
			// 
			this.startCountNumericUpDown.Location = new System.Drawing.Point(142, 108);
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
			this.label1.Size = new System.Drawing.Size(59, 12);
			this.label1.TabIndex = 4;
			this.label1.Text = "灯具信息:";
			// 
			// nameTypeLabel
			// 
			this.nameTypeLabel.AutoSize = true;
			this.nameTypeLabel.Location = new System.Drawing.Point(132, 30);
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
			this.addrLabel.Location = new System.Drawing.Point(132, 69);
			this.addrLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.addrLabel.Name = "addrLabel";
			this.addrLabel.Size = new System.Drawing.Size(41, 12);
			this.addrLabel.TabIndex = 9;
			this.addrLabel.Text = "label3";
			// 
			// noticePanel
			// 
			this.noticePanel.Controls.Add(this.label3);
			this.noticePanel.Controls.Add(this.cancelSkinButton);
			this.noticePanel.Controls.Add(this.iSeeSkinButton);
			this.noticePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.noticePanel.Location = new System.Drawing.Point(0, 0);
			this.noticePanel.Margin = new System.Windows.Forms.Padding(2);
			this.noticePanel.Name = "noticePanel";
			this.noticePanel.Size = new System.Drawing.Size(270, 199);
			this.noticePanel.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(270, 120);
			this.label3.TabIndex = 0;
			this.label3.Text = "若改变了通道的初始地址，基于原通道地址编辑的步数信息将会消失，请谨慎操作！";
			// 
			// cancelSkinButton
			// 
			this.cancelSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.cancelSkinButton.BaseColor = System.Drawing.Color.DarkGray;
			this.cancelSkinButton.BorderColor = System.Drawing.Color.Black;
			this.cancelSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.cancelSkinButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelSkinButton.DownBack = null;
			this.cancelSkinButton.Location = new System.Drawing.Point(142, 139);
			this.cancelSkinButton.MouseBack = null;
			this.cancelSkinButton.Name = "cancelSkinButton";
			this.cancelSkinButton.NormlBack = null;
			this.cancelSkinButton.Size = new System.Drawing.Size(97, 43);
			this.cancelSkinButton.TabIndex = 2;
			this.cancelSkinButton.Text = "取消修改";
			this.cancelSkinButton.UseVisualStyleBackColor = false;
			this.cancelSkinButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// iSeeSkinButton
			// 
			this.iSeeSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.iSeeSkinButton.BaseColor = System.Drawing.Color.LightSalmon;
			this.iSeeSkinButton.BorderColor = System.Drawing.Color.Black;
			this.iSeeSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.iSeeSkinButton.DownBack = null;
			this.iSeeSkinButton.ForeColor = System.Drawing.Color.Black;
			this.iSeeSkinButton.Location = new System.Drawing.Point(23, 139);
			this.iSeeSkinButton.MouseBack = null;
			this.iSeeSkinButton.Name = "iSeeSkinButton";
			this.iSeeSkinButton.NormlBack = null;
			this.iSeeSkinButton.Size = new System.Drawing.Size(97, 43);
			this.iSeeSkinButton.TabIndex = 2;
			this.iSeeSkinButton.Text = "坚持修改";
			this.iSeeSkinButton.UseVisualStyleBackColor = false;
			this.iSeeSkinButton.Click += new System.EventHandler(this.iSeeButton_Click);
			// 
			// enterSkinButton
			// 
			this.enterSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.enterSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.enterSkinButton.BorderColor = System.Drawing.Color.Black;
			this.enterSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.enterSkinButton.DownBack = null;
			this.enterSkinButton.Location = new System.Drawing.Point(34, 148);
			this.enterSkinButton.MouseBack = null;
			this.enterSkinButton.Name = "enterSkinButton";
			this.enterSkinButton.NormlBack = null;
			this.enterSkinButton.Size = new System.Drawing.Size(75, 27);
			this.enterSkinButton.TabIndex = 11;
			this.enterSkinButton.Text = "确定";
			this.enterSkinButton.UseVisualStyleBackColor = false;
			this.enterSkinButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// cancelSkinButton2
			// 
			this.cancelSkinButton2.BackColor = System.Drawing.Color.Transparent;
			this.cancelSkinButton2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.cancelSkinButton2.BorderColor = System.Drawing.Color.Black;
			this.cancelSkinButton2.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.cancelSkinButton2.DownBack = null;
			this.cancelSkinButton2.Location = new System.Drawing.Point(149, 148);
			this.cancelSkinButton2.MouseBack = null;
			this.cancelSkinButton2.Name = "cancelSkinButton2";
			this.cancelSkinButton2.NormlBack = null;
			this.cancelSkinButton2.Size = new System.Drawing.Size(75, 27);
			this.cancelSkinButton2.TabIndex = 11;
			this.cancelSkinButton2.Text = "取消";
			this.cancelSkinButton2.UseVisualStyleBackColor = false;
			this.cancelSkinButton2.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// SkinLightsEditForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelSkinButton;
			this.ClientSize = new System.Drawing.Size(270, 199);
			this.Controls.Add(this.noticePanel);
			this.Controls.Add(this.cancelSkinButton2);
			this.Controls.Add(this.enterSkinButton);
			this.Controls.Add(this.addrLabel);
			this.Controls.Add(this.nameTypeLabel);
			this.Controls.Add(this.startCountNumericUpDown);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "SkinLightsEditForm";
			this.Text = "灯具修改";
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
		private CCWin.SkinControl.SkinButton iSeeSkinButton;
		private CCWin.SkinControl.SkinButton cancelSkinButton;
		private CCWin.SkinControl.SkinButton enterSkinButton;
		private CCWin.SkinControl.SkinButton cancelSkinButton2;
	}
}