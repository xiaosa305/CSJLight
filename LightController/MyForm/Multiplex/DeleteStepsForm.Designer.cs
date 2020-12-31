namespace LightController.MyForm.Multiplex
{
	partial class DeleteStepsForm
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
			this.allStepButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.endNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.startNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.endNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.startNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// allStepButton
			// 
			this.allStepButton.Location = new System.Drawing.Point(148, 21);
			this.allStepButton.Name = "allStepButton";
			this.allStepButton.Size = new System.Drawing.Size(70, 22);
			this.allStepButton.TabIndex = 20;
			this.allStepButton.Text = "全选";
			this.allStepButton.UseVisualStyleBackColor = true;
			this.allStepButton.Click += new System.EventHandler(this.allStepButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.SystemColors.Window;
			this.label2.Location = new System.Drawing.Point(117, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 12);
			this.label2.TabIndex = 19;
			this.label2.Text = "- ";
			// 
			// endNumericUpDown
			// 
			this.endNumericUpDown.Location = new System.Drawing.Point(150, 59);
			this.endNumericUpDown.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
			this.endNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.endNumericUpDown.Name = "endNumericUpDown";
			this.endNumericUpDown.Size = new System.Drawing.Size(68, 21);
			this.endNumericUpDown.TabIndex = 17;
			this.endNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.endNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// startNumericUpDown
			// 
			this.startNumericUpDown.Location = new System.Drawing.Point(22, 59);
			this.startNumericUpDown.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
			this.startNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.startNumericUpDown.Name = "startNumericUpDown";
			this.startNumericUpDown.Size = new System.Drawing.Size(68, 21);
			this.startNumericUpDown.TabIndex = 18;
			this.startNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.startNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.SystemColors.Window;
			this.label1.Location = new System.Drawing.Point(22, 26);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(125, 12);
			this.label1.TabIndex = 16;
			this.label1.Text = "请选择要删除的步数：";
			// 
			// cancelButton
			// 
			this.cancelButton.BackColor = System.Drawing.SystemColors.Window;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(135, 103);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(78, 26);
			this.cancelButton.TabIndex = 21;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = false;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.BackColor = System.Drawing.SystemColors.Window;
			this.deleteButton.Location = new System.Drawing.Point(29, 103);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(78, 26);
			this.deleteButton.TabIndex = 22;
			this.deleteButton.Text = "删除";
			this.deleteButton.UseVisualStyleBackColor = false;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// DeleteStepsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(238, 151);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.allStepButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.endNumericUpDown);
			this.Controls.Add(this.startNumericUpDown);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "DeleteStepsForm";
			this.Text = "删除指定步";
			this.Load += new System.EventHandler(this.DeleteStepsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.endNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.startNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button allStepButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown endNumericUpDown;
		private System.Windows.Forms.NumericUpDown startNumericUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button deleteButton;
	}
}