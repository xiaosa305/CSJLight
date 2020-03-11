namespace LightController
{
	partial class LightsAstForm
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
			//System.Console.WriteLine("lightastform is dispose");
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
			this.label2 = new System.Windows.Forms.Label();
			this.lightCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.startCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.nameTypeLabel = new System.Windows.Forms.Label();
			this.enterButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.lightCountNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.startCountNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(29, 89);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "添加灯具数量：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(29, 57);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(89, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "起始灯具地址：";
			// 
			// lightCountNumericUpDown
			// 
			this.lightCountNumericUpDown.Location = new System.Drawing.Point(143, 88);
			this.lightCountNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.lightCountNumericUpDown.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
			this.lightCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.lightCountNumericUpDown.Name = "lightCountNumericUpDown";
			this.lightCountNumericUpDown.Size = new System.Drawing.Size(99, 21);
			this.lightCountNumericUpDown.TabIndex = 3;
			this.lightCountNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lightCountNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// startCountNumericUpDown
			// 
			this.startCountNumericUpDown.Location = new System.Drawing.Point(143, 53);
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
			this.startCountNumericUpDown.Size = new System.Drawing.Size(99, 21);
			this.startCountNumericUpDown.TabIndex = 3;
			this.startCountNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.startCountNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(29, 25);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "添加灯具名称：";
			// 
			// nameTypeLabel
			// 
			this.nameTypeLabel.AutoSize = true;
			this.nameTypeLabel.Location = new System.Drawing.Point(122, 25);
			this.nameTypeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.nameTypeLabel.Name = "nameTypeLabel";
			this.nameTypeLabel.Size = new System.Drawing.Size(41, 12);
			this.nameTypeLabel.TabIndex = 4;
			this.nameTypeLabel.Text = "灯具名";
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(43, 133);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(70, 27);
			this.enterButton.TabIndex = 6;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(158, 133);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(70, 27);
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// LightsAstForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(272, 182);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.nameTypeLabel);
			this.Controls.Add(this.startCountNumericUpDown);
			this.Controls.Add(this.lightCountNumericUpDown);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "LightsAstForm";
			this.Text = "添加灯具选项";
			this.Load += new System.EventHandler(this.LightsAstForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.lightCountNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.startCountNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown lightCountNumericUpDown;
		private System.Windows.Forms.NumericUpDown startCountNumericUpDown;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label nameTypeLabel;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button cancelButton;
	}
}