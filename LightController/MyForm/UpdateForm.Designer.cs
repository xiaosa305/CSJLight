﻿namespace LightController.MyForm
{
	partial class UpdateForm
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
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.updateButton = new System.Windows.Forms.Button();
			this.connectButton = new System.Windows.Forms.Button();
			this.searchButton = new System.Windows.Forms.Button();
			this.devicesComboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(180, 104);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(538, 39);
			this.progressBar1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(63, 114);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "进度条测试：";
			// 
			// updateButton
			// 
			this.updateButton.Location = new System.Drawing.Point(617, 33);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(101, 39);
			this.updateButton.TabIndex = 2;
			this.updateButton.Text = "下载数据";
			this.updateButton.UseVisualStyleBackColor = true;
			this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
			// 
			// connectButton
			// 
			this.connectButton.Location = new System.Drawing.Point(499, 33);
			this.connectButton.Name = "connectButton";
			this.connectButton.Size = new System.Drawing.Size(101, 39);
			this.connectButton.TabIndex = 3;
			this.connectButton.Text = "连接设备";
			this.connectButton.UseVisualStyleBackColor = true;
			this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// searchButton
			// 
			this.searchButton.Location = new System.Drawing.Point(66, 33);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(101, 39);
			this.searchButton.TabIndex = 4;
			this.searchButton.Text = "搜索设备";
			this.searchButton.UseVisualStyleBackColor = true;
			this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// devicesComboBox
			// 
			this.devicesComboBox.FormattingEnabled = true;
			this.devicesComboBox.Location = new System.Drawing.Point(211, 42);
			this.devicesComboBox.Name = "devicesComboBox";
			this.devicesComboBox.Size = new System.Drawing.Size(237, 23);
			this.devicesComboBox.TabIndex = 5;
			// 
			// UpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(790, 188);
			this.Controls.Add(this.devicesComboBox);
			this.Controls.Add(this.searchButton);
			this.Controls.Add(this.connectButton);
			this.Controls.Add(this.updateButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.progressBar1);
			this.Name = "UpdateForm";
			this.Text = "下载数据到设备";
			this.Load += new System.EventHandler(this.UpdateForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button updateButton;
		private System.Windows.Forms.Button connectButton;
		private System.Windows.Forms.Button searchButton;
		private System.Windows.Forms.ComboBox devicesComboBox;
	}
}