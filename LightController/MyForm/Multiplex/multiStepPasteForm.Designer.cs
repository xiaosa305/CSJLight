﻿namespace LightController.MyForm
{
	partial class MultiStepPasteForm
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
			this.insertButton = new System.Windows.Forms.Button();
			this.coverButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// insertButton
			// 
			this.insertButton.Location = new System.Drawing.Point(33, 27);
			this.insertButton.Name = "insertButton";
			this.insertButton.Size = new System.Drawing.Size(75, 23);
			this.insertButton.TabIndex = 8;
			this.insertButton.Text = "插入";
			this.insertButton.UseVisualStyleBackColor = true;
			this.insertButton.Click += new System.EventHandler(this.insertOrCoverSkinButton_Click);
			// 
			// coverButton
			// 
			this.coverButton.Location = new System.Drawing.Point(135, 27);
			this.coverButton.Name = "coverButton";
			this.coverButton.Size = new System.Drawing.Size(75, 23);
			this.coverButton.TabIndex = 8;
			this.coverButton.Text = "覆盖";
			this.coverButton.UseVisualStyleBackColor = true;
			this.coverButton.Click += new System.EventHandler(this.insertOrCoverSkinButton_Click);
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(33, 74);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 23);
			this.helpButton.TabIndex = 8;
			this.helpButton.Text = "使用说明";
			this.helpButton.UseVisualStyleBackColor = true;
			this.helpButton.Click += new System.EventHandler(this.helpSkinButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(135, 74);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 8;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelSkinButton_Click);
			// 
			// MultiStepPasteForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(239, 124);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.coverButton);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.insertButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MultiStepPasteForm";
			this.Text = "粘贴多步";
			this.Load += new System.EventHandler(this.MultiStepPasteForm_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button insertButton;
		private System.Windows.Forms.Button coverButton;
		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.Button cancelButton;
	}
}