namespace LightEditor
{
	partial class NewSAForm
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
			this.saNameTextBox = new System.Windows.Forms.TextBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.enterButton = new System.Windows.Forms.Button();
			this.startValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.endValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.startValueNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.endValueNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// saNameTextBox
			// 
			this.saNameTextBox.Location = new System.Drawing.Point(139, 26);
			this.saNameTextBox.MaxLength = 8;
			this.saNameTextBox.Name = "saNameTextBox";
			this.saNameTextBox.Size = new System.Drawing.Size(79, 21);
			this.saNameTextBox.TabIndex = 24;
			this.saNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(146, 140);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(70, 27);
			this.cancelButton.TabIndex = 23;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(39, 140);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(70, 27);
			this.enterButton.TabIndex = 22;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// startValueNumericUpDown
			// 
			this.startValueNumericUpDown.Location = new System.Drawing.Point(139, 64);
			this.startValueNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.startValueNumericUpDown.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			this.startValueNumericUpDown.Name = "startValueNumericUpDown";
			this.startValueNumericUpDown.Size = new System.Drawing.Size(79, 21);
			this.startValueNumericUpDown.TabIndex = 20;
			this.startValueNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// endValueNumericUpDown
			// 
			this.endValueNumericUpDown.Location = new System.Drawing.Point(139, 97);
			this.endValueNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.endValueNumericUpDown.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			this.endValueNumericUpDown.Name = "endValueNumericUpDown";
			this.endValueNumericUpDown.Size = new System.Drawing.Size(79, 21);
			this.endValueNumericUpDown.TabIndex = 21;
			this.endValueNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(37, 66);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 12);
			this.label2.TabIndex = 17;
			this.label2.Text = "起始属性值：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(37, 29);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 12);
			this.label3.TabIndex = 18;
			this.label3.Text = "子属性名称：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(37, 98);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 12);
			this.label1.TabIndex = 19;
			this.label1.Text = "截止属性值：";
			// 
			// NewSAForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(249, 199);
			this.Controls.Add(this.saNameTextBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.startValueNumericUpDown);
			this.Controls.Add(this.endValueNumericUpDown);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewSAForm";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.SAForm_HelpButtonClicked);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SAForm_FormClosing);
			this.Load += new System.EventHandler(this.NewSAForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.startValueNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.endValueNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox saNameTextBox;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.NumericUpDown startValueNumericUpDown;
		private System.Windows.Forms.NumericUpDown endValueNumericUpDown;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
	}
}