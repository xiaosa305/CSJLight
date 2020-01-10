using System.Windows.Forms;

namespace LightController.MyForm
{
	partial class YMSetForm
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
			this.skGroupBox = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.panel27 = new System.Windows.Forms.Panel();
			this.label30 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.saveSkinButton = new CCWin.SkinControl.SkinButton();
			this.commonZXSkinButton = new CCWin.SkinControl.SkinButton();
			this.commonJGSkinButton = new CCWin.SkinControl.SkinButton();
			this.allCheckBox = new System.Windows.Forms.CheckBox();
			this.commonZXNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.commonJGNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.skGroupBox.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel27.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.commonZXNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.commonJGNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// skGroupBox
			// 
			this.skGroupBox.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.skGroupBox.Controls.Add(this.flowLayoutPanel2);
			this.skGroupBox.Controls.Add(this.flowLayoutPanel1);
			this.skGroupBox.Controls.Add(this.saveSkinButton);
			this.skGroupBox.Controls.Add(this.commonZXSkinButton);
			this.skGroupBox.Controls.Add(this.commonJGSkinButton);
			this.skGroupBox.Controls.Add(this.allCheckBox);
			this.skGroupBox.Controls.Add(this.commonZXNumericUpDown);
			this.skGroupBox.Controls.Add(this.commonJGNumericUpDown);
			this.skGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skGroupBox.Location = new System.Drawing.Point(0, 0);
			this.skGroupBox.Margin = new System.Windows.Forms.Padding(2);
			this.skGroupBox.Name = "skGroupBox";
			this.skGroupBox.Padding = new System.Windows.Forms.Padding(2);
			this.skGroupBox.Size = new System.Drawing.Size(1020, 610);
			this.skGroupBox.TabIndex = 3;
			this.skGroupBox.TabStop = false;
			this.skGroupBox.Text = "各场景摇麦设置";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.panel2);
			this.flowLayoutPanel2.Controls.Add(this.panel3);
			this.flowLayoutPanel2.Controls.Add(this.panel27);
			this.flowLayoutPanel2.Location = new System.Drawing.Point(6, 19);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(134, 455);
			this.flowLayoutPanel2.TabIndex = 13;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Location = new System.Drawing.Point(3, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(126, 141);
			this.panel2.TabIndex = 14;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 47);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "是否开启";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(14, 79);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(101, 12);
			this.label5.TabIndex = 6;
			this.label5.Text = "摇麦间隔时间(分)";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(14, 112);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(101, 12);
			this.label6.TabIndex = 6;
			this.label6.Text = "摇麦执行时间(秒)";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label7);
			this.panel3.Controls.Add(this.label8);
			this.panel3.Controls.Add(this.label9);
			this.panel3.Location = new System.Drawing.Point(3, 150);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(126, 141);
			this.panel3.TabIndex = 14;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(14, 47);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(53, 12);
			this.label7.TabIndex = 6;
			this.label7.Text = "是否开启";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(14, 79);
			this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(101, 12);
			this.label8.TabIndex = 6;
			this.label8.Text = "摇麦间隔时间(分)";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(14, 112);
			this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(101, 12);
			this.label9.TabIndex = 6;
			this.label9.Text = "摇麦执行时间(秒)";
			// 
			// panel27
			// 
			this.panel27.Controls.Add(this.label30);
			this.panel27.Controls.Add(this.label32);
			this.panel27.Controls.Add(this.label33);
			this.panel27.Location = new System.Drawing.Point(3, 297);
			this.panel27.Name = "panel27";
			this.panel27.Size = new System.Drawing.Size(126, 141);
			this.panel27.TabIndex = 15;
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.Location = new System.Drawing.Point(14, 47);
			this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(53, 12);
			this.label30.TabIndex = 6;
			this.label30.Text = "是否开启";
			// 
			// label32
			// 
			this.label32.AutoSize = true;
			this.label32.Location = new System.Drawing.Point(14, 79);
			this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(101, 12);
			this.label32.TabIndex = 6;
			this.label32.Text = "摇麦间隔时间(分)";
			// 
			// label33
			// 
			this.label33.AutoSize = true;
			this.label33.Location = new System.Drawing.Point(14, 112);
			this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(101, 12);
			this.label33.TabIndex = 6;
			this.label33.Text = "摇麦执行时间(秒)";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Location = new System.Drawing.Point(143, 19);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(869, 455);
			this.flowLayoutPanel1.TabIndex = 12;
			// 
			// saveSkinButton
			// 
			this.saveSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.saveSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.saveSkinButton.BorderColor = System.Drawing.Color.Black;
			this.saveSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.saveSkinButton.DownBack = null;
			this.saveSkinButton.Location = new System.Drawing.Point(831, 514);
			this.saveSkinButton.MouseBack = null;
			this.saveSkinButton.Name = "saveSkinButton";
			this.saveSkinButton.NormlBack = null;
			this.saveSkinButton.Size = new System.Drawing.Size(75, 32);
			this.saveSkinButton.TabIndex = 11;
			this.saveSkinButton.Text = "保存设置";
			this.saveSkinButton.UseVisualStyleBackColor = false;
			this.saveSkinButton.Click += new System.EventHandler(this.ymSaveButton_Click);
			// 
			// commonZXSkinButton
			// 
			this.commonZXSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.commonZXSkinButton.BaseColor = System.Drawing.Color.Tan;
			this.commonZXSkinButton.BorderColor = System.Drawing.Color.Black;
			this.commonZXSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.commonZXSkinButton.DownBack = null;
			this.commonZXSkinButton.Location = new System.Drawing.Point(548, 517);
			this.commonZXSkinButton.MouseBack = null;
			this.commonZXSkinButton.Name = "commonZXSkinButton";
			this.commonZXSkinButton.NormlBack = null;
			this.commonZXSkinButton.Size = new System.Drawing.Size(110, 26);
			this.commonZXSkinButton.TabIndex = 10;
			this.commonZXSkinButton.Text = "统一摇麦执行时间";
			this.commonZXSkinButton.UseVisualStyleBackColor = false;
			this.commonZXSkinButton.Click += new System.EventHandler(this.commonZXButton_Click);
			// 
			// commonJGSkinButton
			// 
			this.commonJGSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.commonJGSkinButton.BaseColor = System.Drawing.Color.Tan;
			this.commonJGSkinButton.BorderColor = System.Drawing.Color.Black;
			this.commonJGSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.commonJGSkinButton.DownBack = null;
			this.commonJGSkinButton.Location = new System.Drawing.Point(324, 517);
			this.commonJGSkinButton.MouseBack = null;
			this.commonJGSkinButton.Name = "commonJGSkinButton";
			this.commonJGSkinButton.NormlBack = null;
			this.commonJGSkinButton.Size = new System.Drawing.Size(110, 26);
			this.commonJGSkinButton.TabIndex = 10;
			this.commonJGSkinButton.Text = "统一摇麦间隔时间";
			this.commonJGSkinButton.UseVisualStyleBackColor = false;
			this.commonJGSkinButton.Click += new System.EventHandler(this.commonJGButton_Click);
			// 
			// allCheckBox
			// 
			this.allCheckBox.AutoSize = true;
			this.allCheckBox.Location = new System.Drawing.Point(155, 523);
			this.allCheckBox.Margin = new System.Windows.Forms.Padding(2);
			this.allCheckBox.Name = "allCheckBox";
			this.allCheckBox.Size = new System.Drawing.Size(72, 16);
			this.allCheckBox.TabIndex = 8;
			this.allCheckBox.Text = "全部开启";
			this.allCheckBox.UseVisualStyleBackColor = true;
			this.allCheckBox.CheckedChanged += new System.EventHandler(this.allCheckBox_CheckedChanged);
			// 
			// commonZXNumericUpDown
			// 
			this.commonZXNumericUpDown.Location = new System.Drawing.Point(499, 521);
			this.commonZXNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.commonZXNumericUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.commonZXNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.commonZXNumericUpDown.Name = "commonZXNumericUpDown";
			this.commonZXNumericUpDown.Size = new System.Drawing.Size(44, 21);
			this.commonZXNumericUpDown.TabIndex = 7;
			this.commonZXNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.commonZXNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// commonJGNumericUpDown
			// 
			this.commonJGNumericUpDown.Location = new System.Drawing.Point(275, 521);
			this.commonJGNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.commonJGNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.commonJGNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.commonJGNumericUpDown.Name = "commonJGNumericUpDown";
			this.commonJGNumericUpDown.Size = new System.Drawing.Size(44, 21);
			this.commonJGNumericUpDown.TabIndex = 5;
			this.commonJGNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.commonJGNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// YMSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1020, 610);
			this.Controls.Add(this.skGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "YMSetForm";
			this.Text = "摇麦设置";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.YMSetForm_FormClosed);
			this.Load += new System.EventHandler(this.YMSetForm_Load);
			this.skGroupBox.ResumeLayout(false);
			this.skGroupBox.PerformLayout();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel27.ResumeLayout(false);
			this.panel27.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.commonZXNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.commonJGNumericUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox skGroupBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;	
		private System.Windows.Forms.CheckBox allCheckBox;
		private System.Windows.Forms.NumericUpDown commonZXNumericUpDown;
		private System.Windows.Forms.NumericUpDown commonJGNumericUpDown;		

		private Panel[] framePanels;
		private Label[] frameLabels;
		private CheckBox[] ymCheckBoxes = new CheckBox[24];
		private NumericUpDown[] zxNumericUpDowns = new NumericUpDown[24];
		private NumericUpDown[] jgNumericUpDowns = new NumericUpDown[24];

		private CCWin.SkinControl.SkinButton saveSkinButton;
		private CCWin.SkinControl.SkinButton commonZXSkinButton;
		private CCWin.SkinControl.SkinButton commonJGSkinButton;
		private FlowLayoutPanel flowLayoutPanel2;
		private Panel panel2;
		private FlowLayoutPanel flowLayoutPanel1;
		private Panel panel3;
		private Label label7;
		private Label label8;
		private Label label9;
		private Panel panel27;
		private Label label30;
		private Label label32;
		private Label label33;
	}
}