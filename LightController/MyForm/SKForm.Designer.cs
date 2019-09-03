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
			this.components = new System.ComponentModel.Container();
			this.panel26 = new System.Windows.Forms.Panel();
			this.label37 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.frameLabel = new System.Windows.Forms.Label();
			this.jgtNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.frameStepTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.mFrameLKTextBox = new System.Windows.Forms.TextBox();
			this.mFrameSaveSkinButton = new CCWin.SkinControl.SkinButton();
			this.cancelSkinButton = new CCWin.SkinControl.SkinButton();
			this.noticeLabel = new System.Windows.Forms.Label();
			this.panel26.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.jgtNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.frameStepTimeNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// panel26
			// 
			this.panel26.Controls.Add(this.label37);
			this.panel26.Controls.Add(this.label36);
			this.panel26.Controls.Add(this.label8);
			this.panel26.Location = new System.Drawing.Point(12, 12);
			this.panel26.Name = "panel26";
			this.panel26.Size = new System.Drawing.Size(138, 102);
			this.panel26.TabIndex = 5;
			// 
			// label37
			// 
			this.label37.AutoSize = true;
			this.label37.Location = new System.Drawing.Point(17, 75);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(119, 12);
			this.label37.TabIndex = 0;
			this.label37.Text = "叠加后间隔时间(ms):";
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(17, 47);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(77, 12);
			this.label36.TabIndex = 0;
			this.label36.Text = "音频步时间：";
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
			this.panel2.Controls.Add(this.frameLabel);
			this.panel2.Controls.Add(this.jgtNumericUpDown);
			this.panel2.Controls.Add(this.frameStepTimeNumericUpDown);
			this.panel2.Location = new System.Drawing.Point(156, 12);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(73, 102);
			this.panel2.TabIndex = 3;
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
			this.jgtNumericUpDown.Location = new System.Drawing.Point(9, 71);
			this.jgtNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.jgtNumericUpDown.Name = "jgtNumericUpDown";
			this.jgtNumericUpDown.Size = new System.Drawing.Size(55, 21);
			this.jgtNumericUpDown.TabIndex = 1;
			// 
			// frameStepTimeNumericUpDown
			// 
			this.frameStepTimeNumericUpDown.Location = new System.Drawing.Point(11, 43);
			this.frameStepTimeNumericUpDown.Name = "frameStepTimeNumericUpDown";
			this.frameStepTimeNumericUpDown.Size = new System.Drawing.Size(51, 21);
			this.frameStepTimeNumericUpDown.TabIndex = 1;
			// 
			// mFrameLKTextBox
			// 
			this.mFrameLKTextBox.BackColor = System.Drawing.Color.White;
			this.mFrameLKTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mFrameLKTextBox.Location = new System.Drawing.Point(12, 215);
			this.mFrameLKTextBox.MaxLength = 20;
			this.mFrameLKTextBox.Multiline = true;
			this.mFrameLKTextBox.Name = "mFrameLKTextBox";
			this.mFrameLKTextBox.Size = new System.Drawing.Size(217, 22);
			this.mFrameLKTextBox.TabIndex = 8;
			this.mFrameLKTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mFrameLKTextBox_KeyPress);
			// 
			// mFrameSaveSkinButton
			// 
			this.mFrameSaveSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.mFrameSaveSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.mFrameSaveSkinButton.BorderColor = System.Drawing.Color.Black;
			this.mFrameSaveSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.mFrameSaveSkinButton.DownBack = null;
			this.mFrameSaveSkinButton.Location = new System.Drawing.Point(26, 268);
			this.mFrameSaveSkinButton.MouseBack = null;
			this.mFrameSaveSkinButton.Name = "mFrameSaveSkinButton";
			this.mFrameSaveSkinButton.NormlBack = null;
			this.mFrameSaveSkinButton.Size = new System.Drawing.Size(80, 25);
			this.mFrameSaveSkinButton.TabIndex = 7;
			this.mFrameSaveSkinButton.Text = "保存设置";
			this.mFrameSaveSkinButton.UseVisualStyleBackColor = false;
			this.mFrameSaveSkinButton.Click += new System.EventHandler(this.mFrameSaveSkinButton_Click);
			// 
			// cancelSkinButton
			// 
			this.cancelSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.cancelSkinButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.cancelSkinButton.BorderColor = System.Drawing.Color.Black;
			this.cancelSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.cancelSkinButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelSkinButton.DownBack = null;
			this.cancelSkinButton.Location = new System.Drawing.Point(136, 268);
			this.cancelSkinButton.MouseBack = null;
			this.cancelSkinButton.Name = "cancelSkinButton";
			this.cancelSkinButton.NormlBack = null;
			this.cancelSkinButton.Size = new System.Drawing.Size(80, 25);
			this.cancelSkinButton.TabIndex = 7;
			this.cancelSkinButton.Text = "取消";
			this.cancelSkinButton.UseVisualStyleBackColor = false;
			this.cancelSkinButton.Click += new System.EventHandler(this.cancelSkinButton_Click);
			// 
			// noticeLabel
			// 
			this.noticeLabel.Location = new System.Drawing.Point(14, 132);
			this.noticeLabel.Name = "noticeLabel";
			this.noticeLabel.Size = new System.Drawing.Size(215, 70);
			this.noticeLabel.TabIndex = 10;
			this.noticeLabel.Text = "提示：请在下面文本框内输入每一次执行的步数（范围为1-9），并将每步数字连在一起（如1234）；若设为\"0\"或空字符串，则表示该场景不执行声控模式;链表数量不可超" +
    "过20个。";
			// 
			// SKForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelSkinButton;
			this.ClientSize = new System.Drawing.Size(247, 327);
			this.Controls.Add(this.noticeLabel);
			this.Controls.Add(this.mFrameLKTextBox);
			this.Controls.Add(this.cancelSkinButton);
			this.Controls.Add(this.mFrameSaveSkinButton);
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
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.NumericUpDown jgtNumericUpDown;
		private System.Windows.Forms.NumericUpDown frameStepTimeNumericUpDown;
		private System.Windows.Forms.Label frameLabel;
		private System.Windows.Forms.TextBox mFrameLKTextBox;
		private CCWin.SkinControl.SkinButton mFrameSaveSkinButton;
		private CCWin.SkinControl.SkinButton cancelSkinButton;
		private System.Windows.Forms.Label noticeLabel;
	}
}