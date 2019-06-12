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
			this.enterButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.lightCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.startCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.lightCountNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.startCountNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(25, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "添加灯具数量";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(25, 71);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(97, 15);
			this.label2.TabIndex = 0;
			this.label2.Text = "起始灯具地址";
			// 
			// enterButton
			// 
			this.enterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.enterButton.Location = new System.Drawing.Point(28, 126);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(94, 34);
			this.enterButton.TabIndex = 2;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = false;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.BackColor = System.Drawing.SystemColors.Info;
			this.cancelButton.Location = new System.Drawing.Point(182, 126);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(100, 34);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = false;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// lightCountNumericUpDown
			// 
			this.lightCountNumericUpDown.Location = new System.Drawing.Point(150, 30);
			this.lightCountNumericUpDown.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.lightCountNumericUpDown.Name = "lightCountNumericUpDown";
			this.lightCountNumericUpDown.Size = new System.Drawing.Size(132, 25);
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
			this.startCountNumericUpDown.Location = new System.Drawing.Point(150, 69);
			this.startCountNumericUpDown.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.startCountNumericUpDown.Name = "startCountNumericUpDown";
			this.startCountNumericUpDown.Size = new System.Drawing.Size(132, 25);
			this.startCountNumericUpDown.TabIndex = 3;
			this.startCountNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.startCountNumericUpDown.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// LightsAstForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(309, 188);
			this.Controls.Add(this.startCountNumericUpDown);
			this.Controls.Add(this.lightCountNumericUpDown);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "LightsAstForm";
			this.Text = "添加灯具选项";
			((System.ComponentModel.ISupportInitialize)(this.lightCountNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.startCountNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.NumericUpDown lightCountNumericUpDown;
		private System.Windows.Forms.NumericUpDown startCountNumericUpDown;
	}
}