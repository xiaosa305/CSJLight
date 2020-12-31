namespace LightController.MyForm.Multiplex
{
	partial class AddStepsForm
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
			this.stepCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.stepCountNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// stepCountNumericUpDown
			// 
			this.stepCountNumericUpDown.Location = new System.Drawing.Point(149, 23);
			this.stepCountNumericUpDown.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
			this.stepCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.stepCountNumericUpDown.Name = "stepCountNumericUpDown";
			this.stepCountNumericUpDown.Size = new System.Drawing.Size(68, 21);
			this.stepCountNumericUpDown.TabIndex = 20;
			this.stepCountNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.stepCountNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.SystemColors.Window;
			this.label1.Location = new System.Drawing.Point(19, 27);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(125, 12);
			this.label1.TabIndex = 19;
			this.label1.Text = "请选择要追加的步数：";
			// 
			// cancelButton
			// 
			this.cancelButton.BackColor = System.Drawing.SystemColors.Window;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(136, 60);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(78, 26);
			this.cancelButton.TabIndex = 23;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = false;
			// 
			// addButton
			// 
			this.addButton.BackColor = System.Drawing.SystemColors.Window;
			this.addButton.Location = new System.Drawing.Point(22, 60);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(78, 26);
			this.addButton.TabIndex = 24;
			this.addButton.Text = "追加步";
			this.addButton.UseVisualStyleBackColor = false;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// AddStepsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(235, 102);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.stepCountNumericUpDown);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AddStepsForm";
			this.Text = "追加步数";
			this.Load += new System.EventHandler(this.AddStepsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.stepCountNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown stepCountNumericUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button addButton;
	}
}