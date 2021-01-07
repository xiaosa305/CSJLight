namespace LightController.MyForm
{
	partial class SKForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.frameLabel = new System.Windows.Forms.Label();
			this.jgtNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.frameStepTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.mFrameLKTextBox = new System.Windows.Forms.TextBox();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.jgtNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.frameStepTimeNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(29, 95);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "叠加后间隔时间(ms):";
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(29, 64);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(95, 12);
			this.label36.TabIndex = 0;
			this.label36.Text = "音频步时间(s)：";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(29, 33);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(53, 12);
			this.label8.TabIndex = 0;
			this.label8.Text = "场景名：";
			// 
			// frameLabel
			// 
			this.frameLabel.AutoSize = true;
			this.frameLabel.Location = new System.Drawing.Point(167, 33);
			this.frameLabel.Name = "frameLabel";
			this.frameLabel.Size = new System.Drawing.Size(53, 12);
			this.frameLabel.TabIndex = 2;
			this.frameLabel.Tag = "999";
			this.frameLabel.Text = "场景名称";
			// 
			// jgtNumericUpDown
			// 
			this.jgtNumericUpDown.Location = new System.Drawing.Point(167, 91);
			this.jgtNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.jgtNumericUpDown.Name = "jgtNumericUpDown";
			this.jgtNumericUpDown.Size = new System.Drawing.Size(55, 21);
			this.jgtNumericUpDown.TabIndex = 1;
			this.jgtNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// frameStepTimeNumericUpDown
			// 
			this.frameStepTimeNumericUpDown.DecimalPlaces = 2;
			this.frameStepTimeNumericUpDown.Location = new System.Drawing.Point(167, 58);
			this.frameStepTimeNumericUpDown.Name = "frameStepTimeNumericUpDown";
			this.frameStepTimeNumericUpDown.Size = new System.Drawing.Size(55, 21);
			this.frameStepTimeNumericUpDown.TabIndex = 1;
			this.frameStepTimeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.frameStepTimeNumericUpDown.ValueChanged += new System.EventHandler(this.frameStepTimeNumericUpDown_ValueChanged);
			// 
			// mFrameLKTextBox
			// 
			this.mFrameLKTextBox.BackColor = System.Drawing.Color.White;
			this.mFrameLKTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mFrameLKTextBox.Location = new System.Drawing.Point(22, 172);
			this.mFrameLKTextBox.MaxLength = 20;
			this.mFrameLKTextBox.Multiline = true;
			this.mFrameLKTextBox.Name = "mFrameLKTextBox";
			this.mFrameLKTextBox.Size = new System.Drawing.Size(217, 22);
			this.mFrameLKTextBox.TabIndex = 8;
			this.mFrameLKTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mFrameLKTextBox_KeyPress);
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(32, 214);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(80, 25);
			this.saveButton.TabIndex = 11;
			this.saveButton.Text = "保存设置";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.mFrameSaveSkinButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(150, 214);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(80, 25);
			this.cancelButton.TabIndex = 11;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelSkinButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 147);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 12;
			this.label2.Text = "音频链表：";
			// 
			// SKForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(254, 263);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.frameLabel);
			this.Controls.Add(this.jgtNumericUpDown);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.label36);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.mFrameLKTextBox);
			this.Controls.Add(this.frameStepTimeNumericUpDown);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SKForm";
			this.Text = "音频场景设置";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.SKForm_HelpButtonClicked);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SKForm_FormClosed);
			this.Load += new System.EventHandler(this.SKForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.jgtNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.frameStepTimeNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown jgtNumericUpDown;
		private System.Windows.Forms.NumericUpDown frameStepTimeNumericUpDown;
		private System.Windows.Forms.Label frameLabel;
		private System.Windows.Forms.TextBox mFrameLKTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label label2;
	}
}