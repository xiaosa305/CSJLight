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
			this.panel26 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.trueSTLabel = new System.Windows.Forms.Label();
			this.frameLabel = new System.Windows.Forms.Label();
			this.jgtNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.frameStepTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.mFrameLKTextBox = new System.Windows.Forms.TextBox();
			this.noticeLabel = new System.Windows.Forms.Label();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.panel26.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.jgtNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.frameStepTimeNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// panel26
			// 
			this.panel26.Controls.Add(this.label1);
			this.panel26.Controls.Add(this.label36);
			this.panel26.Controls.Add(this.label8);
			this.panel26.Location = new System.Drawing.Point(12, 12);
			this.panel26.Name = "panel26";
			this.panel26.Size = new System.Drawing.Size(138, 115);
			this.panel26.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 81);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "叠加后间隔时间(ms):";
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(17, 50);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(95, 12);
			this.label36.TabIndex = 0;
			this.label36.Text = "音频步时间(s)：";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(17, 19);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(65, 12);
			this.label8.TabIndex = 0;
			this.label8.Text = "修改场景：";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.trueSTLabel);
			this.panel2.Controls.Add(this.frameLabel);
			this.panel2.Controls.Add(this.jgtNumericUpDown);
			this.panel2.Controls.Add(this.frameStepTimeNumericUpDown);
			this.panel2.Location = new System.Drawing.Point(156, 12);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(73, 115);
			this.panel2.TabIndex = 3;
			// 
			// trueSTLabel
			// 
			this.trueSTLabel.AutoSize = true;
			this.trueSTLabel.Location = new System.Drawing.Point(9, 75);
			this.trueSTLabel.Name = "trueSTLabel";
			this.trueSTLabel.Size = new System.Drawing.Size(0, 12);
			this.trueSTLabel.TabIndex = 3;
			// 
			// frameLabel
			// 
			this.frameLabel.AutoSize = true;
			this.frameLabel.Location = new System.Drawing.Point(9, 19);
			this.frameLabel.Name = "frameLabel";
			this.frameLabel.Size = new System.Drawing.Size(53, 12);
			this.frameLabel.TabIndex = 2;
			this.frameLabel.Text = "场景名称";
			// 
			// jgtNumericUpDown
			// 
			this.jgtNumericUpDown.Location = new System.Drawing.Point(9, 77);
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
			this.frameStepTimeNumericUpDown.Location = new System.Drawing.Point(9, 44);
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
			this.mFrameLKTextBox.Location = new System.Drawing.Point(12, 242);
			this.mFrameLKTextBox.MaxLength = 20;
			this.mFrameLKTextBox.Multiline = true;
			this.mFrameLKTextBox.Name = "mFrameLKTextBox";
			this.mFrameLKTextBox.Size = new System.Drawing.Size(217, 22);
			this.mFrameLKTextBox.TabIndex = 8;
			this.mFrameLKTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mFrameLKTextBox_KeyPress);
			// 
			// noticeLabel
			// 
			this.noticeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.noticeLabel.Location = new System.Drawing.Point(14, 145);
			this.noticeLabel.Name = "noticeLabel";
			this.noticeLabel.Size = new System.Drawing.Size(215, 79);
			this.noticeLabel.TabIndex = 10;
			this.noticeLabel.Text = "提示：请在下面文本框内输入每一次执行的步数（范围为1-9），并将每步数字连在一起（如1234）；若设为\"0\"或空字符串，则表示该场景不执行声控模式;链表数量不可超" +
    "过20个。";
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(26, 282);
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
			this.cancelButton.Location = new System.Drawing.Point(136, 283);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(80, 25);
			this.cancelButton.TabIndex = 11;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelSkinButton_Click);
			// 
			// SKForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(247, 327);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.noticeLabel);
			this.Controls.Add(this.mFrameLKTextBox);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel26);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SKForm";
			this.Text = "修改音频场景设置";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SKForm_FormClosed);
			this.Load += new System.EventHandler(this.SKForm_Load);
			this.panel26.ResumeLayout(false);
			this.panel26.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.jgtNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.frameStepTimeNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel panel26;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.NumericUpDown jgtNumericUpDown;
		private System.Windows.Forms.NumericUpDown frameStepTimeNumericUpDown;
		private System.Windows.Forms.Label frameLabel;
		private System.Windows.Forms.TextBox mFrameLKTextBox;
		private System.Windows.Forms.Label noticeLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label trueSTLabel;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
	}
}