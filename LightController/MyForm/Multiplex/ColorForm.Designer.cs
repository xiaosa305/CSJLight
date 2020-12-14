namespace LightController.MyForm.Multiplex
{
	partial class ColorForm
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.button2 = new System.Windows.Forms.Button();
			this.colorPanelDemo = new System.Windows.Forms.Panel();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.colorNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label21 = new System.Windows.Forms.Label();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.label20 = new System.Windows.Forms.Label();
			this.colorFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.label19 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.colorAddButton = new System.Windows.Forms.Button();
			this.myColorDialog = new System.Windows.Forms.ColorDialog();
			this.colorPanelDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.colorNUDDemo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(26, 220);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(47, 24);
			this.panel1.TabIndex = 73;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(174, 22);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(38, 45);
			this.button2.TabIndex = 72;
			this.button2.Text = "删除";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// colorPanelDemo
			// 
			this.colorPanelDemo.Controls.Add(this.checkBox1);
			this.colorPanelDemo.Controls.Add(this.colorNUDDemo);
			this.colorPanelDemo.Location = new System.Drawing.Point(20, 22);
			this.colorPanelDemo.Margin = new System.Windows.Forms.Padding(1);
			this.colorPanelDemo.Name = "colorPanelDemo";
			this.colorPanelDemo.Size = new System.Drawing.Size(58, 138);
			this.colorPanelDemo.TabIndex = 68;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(5, 119);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(48, 16);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "渐变";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// colorNUDDemo
			// 
			this.colorNUDDemo.DecimalPlaces = 2;
			this.colorNUDDemo.Location = new System.Drawing.Point(4, 92);
			this.colorNUDDemo.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.colorNUDDemo.Name = "colorNUDDemo";
			this.colorNUDDemo.Size = new System.Drawing.Size(50, 21);
			this.colorNUDDemo.TabIndex = 0;
			this.colorNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(312, 46);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(57, 21);
			this.numericUpDown1.TabIndex = 70;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(255, 50);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(53, 12);
			this.label21.TabIndex = 71;
			this.label21.Text = "总调光：";
			// 
			// trackBar1
			// 
			this.trackBar1.BackColor = System.Drawing.Color.White;
			this.trackBar1.Location = new System.Drawing.Point(242, 22);
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(145, 45);
			this.trackBar1.TabIndex = 69;
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(28, 195);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(47, 12);
			this.label20.TabIndex = 67;
			this.label20.Text = "跳渐变:";
			// 
			// colorFLP
			// 
			this.colorFLP.AutoScroll = true;
			this.colorFLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.colorFLP.Location = new System.Drawing.Point(81, 79);
			this.colorFLP.Name = "colorFLP";
			this.colorFLP.Size = new System.Drawing.Size(306, 168);
			this.colorFLP.TabIndex = 65;
			this.colorFLP.WrapContents = false;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(28, 172);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(47, 12);
			this.label19.TabIndex = 66;
			this.label19.Text = "步时间:";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(130, 22);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(38, 45);
			this.button3.TabIndex = 63;
			this.button3.Text = "修改";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// colorAddButton
			// 
			this.colorAddButton.Location = new System.Drawing.Point(86, 22);
			this.colorAddButton.Name = "colorAddButton";
			this.colorAddButton.Size = new System.Drawing.Size(38, 45);
			this.colorAddButton.TabIndex = 64;
			this.colorAddButton.Text = "添加";
			this.colorAddButton.UseVisualStyleBackColor = true;
			this.colorAddButton.Click += new System.EventHandler(this.colorAddButton_Click);
			// 
			// myColorDialog
			// 
			this.myColorDialog.AnyColor = true;
			this.myColorDialog.FullOpen = true;
			// 
			// ColorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(407, 347);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.colorPanelDemo);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.trackBar1);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.colorFLP);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.colorAddButton);
			this.Name = "ColorForm";
			this.Text = "ColorForm";
			this.colorPanelDemo.ResumeLayout(false);
			this.colorPanelDemo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.colorNUDDemo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Panel colorPanelDemo;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.NumericUpDown colorNUDDemo;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.FlowLayoutPanel colorFLP;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button colorAddButton;
		private System.Windows.Forms.ColorDialog myColorDialog;
	}
}