namespace LightController.MyForm.Multiplex
{
	partial class DetailMultiForm
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
			this.stepFLPDemo = new System.Windows.Forms.FlowLayoutPanel();
			this.stepPanelDemo = new System.Windows.Forms.Panel();
			this.stepLabelDemo = new System.Windows.Forms.Label();
			this.topButtonDemo = new System.Windows.Forms.Button();
			this.bottomButtonDemo = new System.Windows.Forms.Button();
			this.stepNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.tdPanelDemo = new System.Windows.Forms.Panel();
			this.tdSmallPanelDemo = new System.Windows.Forms.Panel();
			this.unifyComboBoxDemo = new System.Windows.Forms.ComboBox();
			this.unifyTopButtonDemo = new System.Windows.Forms.Button();
			this.unifyNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.unifyValueButtonDemo = new System.Windows.Forms.Button();
			this.unifyBottomButtonDemo = new System.Windows.Forms.Button();
			this.tdLabelDemo = new System.Windows.Forms.Label();
			this.bigFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.stepFLPDemo.SuspendLayout();
			this.stepPanelDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.stepNUDDemo)).BeginInit();
			this.tdPanelDemo.SuspendLayout();
			this.tdSmallPanelDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.unifyNUDDemo)).BeginInit();
			this.bigFLP.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// stepFLPDemo
			// 
			this.stepFLPDemo.AutoScroll = true;
			this.stepFLPDemo.BackColor = System.Drawing.Color.Gray;
			this.stepFLPDemo.Controls.Add(this.stepPanelDemo);
			this.stepFLPDemo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stepFLPDemo.Location = new System.Drawing.Point(123, 0);
			this.stepFLPDemo.Name = "stepFLPDemo";
			this.stepFLPDemo.Size = new System.Drawing.Size(1037, 108);
			this.stepFLPDemo.TabIndex = 1;
			// 
			// stepPanelDemo
			// 
			this.stepPanelDemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.stepPanelDemo.Controls.Add(this.stepLabelDemo);
			this.stepPanelDemo.Controls.Add(this.topButtonDemo);
			this.stepPanelDemo.Controls.Add(this.bottomButtonDemo);
			this.stepPanelDemo.Controls.Add(this.stepNUDDemo);
			this.stepPanelDemo.Location = new System.Drawing.Point(3, 3);
			this.stepPanelDemo.Name = "stepPanelDemo";
			this.stepPanelDemo.Size = new System.Drawing.Size(52, 85);
			this.stepPanelDemo.TabIndex = 0;
			this.stepPanelDemo.Visible = false;
			// 
			// stepLabelDemo
			// 
			this.stepLabelDemo.AutoSize = true;
			this.stepLabelDemo.ForeColor = System.Drawing.Color.White;
			this.stepLabelDemo.Location = new System.Drawing.Point(6, 9);
			this.stepLabelDemo.Name = "stepLabelDemo";
			this.stepLabelDemo.Size = new System.Drawing.Size(35, 12);
			this.stepLabelDemo.TabIndex = 2;
			this.stepLabelDemo.Text = "第N步";
			// 
			// topButtonDemo
			// 
			this.topButtonDemo.Location = new System.Drawing.Point(2, 28);
			this.topButtonDemo.Name = "topButtonDemo";
			this.topButtonDemo.Size = new System.Drawing.Size(22, 23);
			this.topButtonDemo.TabIndex = 0;
			this.topButtonDemo.Text = "↑";
			this.topButtonDemo.UseVisualStyleBackColor = true;
			this.topButtonDemo.Click += new System.EventHandler(this.topButton_Click);
			// 
			// bottomButtonDemo
			// 
			this.bottomButtonDemo.Location = new System.Drawing.Point(26, 28);
			this.bottomButtonDemo.Name = "bottomButtonDemo";
			this.bottomButtonDemo.Size = new System.Drawing.Size(22, 23);
			this.bottomButtonDemo.TabIndex = 0;
			this.bottomButtonDemo.Text = "↓";
			this.bottomButtonDemo.UseVisualStyleBackColor = true;
			this.bottomButtonDemo.Click += new System.EventHandler(this.bottomButton_Click);
			// 
			// stepNUDDemo
			// 
			this.stepNUDDemo.Location = new System.Drawing.Point(2, 56);
			this.stepNUDDemo.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.stepNUDDemo.Name = "stepNUDDemo";
			this.stepNUDDemo.Size = new System.Drawing.Size(45, 21);
			this.stepNUDDemo.TabIndex = 1;
			this.stepNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.stepNUDDemo.ValueChanged += new System.EventHandler(this.StepNUD_ValueChanged);
			// 
			// tdPanelDemo
			// 
			this.tdPanelDemo.Controls.Add(this.stepFLPDemo);
			this.tdPanelDemo.Controls.Add(this.tdSmallPanelDemo);
			this.tdPanelDemo.Location = new System.Drawing.Point(3, 3);
			this.tdPanelDemo.Name = "tdPanelDemo";
			this.tdPanelDemo.Size = new System.Drawing.Size(1160, 108);
			this.tdPanelDemo.TabIndex = 2;
			this.tdPanelDemo.Visible = false;
			// 
			// tdSmallPanelDemo
			// 
			this.tdSmallPanelDemo.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.tdSmallPanelDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tdSmallPanelDemo.Controls.Add(this.unifyComboBoxDemo);
			this.tdSmallPanelDemo.Controls.Add(this.unifyTopButtonDemo);
			this.tdSmallPanelDemo.Controls.Add(this.unifyNUDDemo);
			this.tdSmallPanelDemo.Controls.Add(this.unifyValueButtonDemo);
			this.tdSmallPanelDemo.Controls.Add(this.unifyBottomButtonDemo);
			this.tdSmallPanelDemo.Controls.Add(this.tdLabelDemo);
			this.tdSmallPanelDemo.Dock = System.Windows.Forms.DockStyle.Left;
			this.tdSmallPanelDemo.Location = new System.Drawing.Point(0, 0);
			this.tdSmallPanelDemo.Name = "tdSmallPanelDemo";
			this.tdSmallPanelDemo.Size = new System.Drawing.Size(123, 108);
			this.tdSmallPanelDemo.TabIndex = 0;
			// 
			// unifyComboBoxDemo
			// 
			this.unifyComboBoxDemo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.unifyComboBoxDemo.FormattingEnabled = true;
			this.unifyComboBoxDemo.Items.AddRange(new object[] {
            "全步",
            "单步",
            "双步"});
			this.unifyComboBoxDemo.Location = new System.Drawing.Point(6, 31);
			this.unifyComboBoxDemo.Name = "unifyComboBoxDemo";
			this.unifyComboBoxDemo.Size = new System.Drawing.Size(48, 20);
			this.unifyComboBoxDemo.TabIndex = 4;
			// 
			// unifyTopButtonDemo
			// 
			this.unifyTopButtonDemo.Location = new System.Drawing.Point(63, 30);
			this.unifyTopButtonDemo.Name = "unifyTopButtonDemo";
			this.unifyTopButtonDemo.Size = new System.Drawing.Size(22, 23);
			this.unifyTopButtonDemo.TabIndex = 3;
			this.unifyTopButtonDemo.Tag = "255";
			this.unifyTopButtonDemo.Text = "↑";
			this.unifyTopButtonDemo.UseVisualStyleBackColor = true;
			this.unifyTopButtonDemo.Click += new System.EventHandler(this.unifyValueButton_Click);
			// 
			// unifyNUDDemo
			// 
			this.unifyNUDDemo.Location = new System.Drawing.Point(6, 60);
			this.unifyNUDDemo.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.unifyNUDDemo.Name = "unifyNUDDemo";
			this.unifyNUDDemo.Size = new System.Drawing.Size(48, 21);
			this.unifyNUDDemo.TabIndex = 1;
			this.unifyNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.unifyNUDDemo.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
			// 
			// unifyValueButtonDemo
			// 
			this.unifyValueButtonDemo.Location = new System.Drawing.Point(62, 59);
			this.unifyValueButtonDemo.Name = "unifyValueButtonDemo";
			this.unifyValueButtonDemo.Size = new System.Drawing.Size(50, 23);
			this.unifyValueButtonDemo.TabIndex = 2;
			this.unifyValueButtonDemo.Text = "设值";
			this.unifyValueButtonDemo.UseVisualStyleBackColor = true;
			this.unifyValueButtonDemo.Click += new System.EventHandler(this.unifyValueButton_Click);
			// 
			// unifyBottomButtonDemo
			// 
			this.unifyBottomButtonDemo.Location = new System.Drawing.Point(90, 30);
			this.unifyBottomButtonDemo.Name = "unifyBottomButtonDemo";
			this.unifyBottomButtonDemo.Size = new System.Drawing.Size(22, 23);
			this.unifyBottomButtonDemo.TabIndex = 2;
			this.unifyBottomButtonDemo.Tag = "0";
			this.unifyBottomButtonDemo.Text = "↓";
			this.unifyBottomButtonDemo.UseVisualStyleBackColor = true;
			this.unifyBottomButtonDemo.Click += new System.EventHandler(this.unifyValueButton_Click);
			// 
			// tdLabelDemo
			// 
			this.tdLabelDemo.Location = new System.Drawing.Point(6, 6);
			this.tdLabelDemo.Name = "tdLabelDemo";
			this.tdLabelDemo.Size = new System.Drawing.Size(110, 19);
			this.tdLabelDemo.TabIndex = 0;
			this.tdLabelDemo.Text = "通道：\r\n\r\n";
			// 
			// bigFLP
			// 
			this.bigFLP.AutoScroll = true;
			this.bigFLP.BackColor = System.Drawing.SystemColors.Window;
			this.bigFLP.Controls.Add(this.tdPanelDemo);
			this.bigFLP.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bigFLP.Location = new System.Drawing.Point(0, 0);
			this.bigFLP.Name = "bigFLP";
			this.bigFLP.Size = new System.Drawing.Size(1184, 659);
			this.bigFLP.TabIndex = 3;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 659);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1184, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(1169, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DetailMultiForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1184, 681);
			this.Controls.Add(this.bigFLP);
			this.Controls.Add(this.statusStrip1);
			this.Name = "DetailMultiForm";
			this.Text = "多步联调";
			this.Load += new System.EventHandler(this.SoundMultiForm_Load);
			this.stepFLPDemo.ResumeLayout(false);
			this.stepPanelDemo.ResumeLayout(false);
			this.stepPanelDemo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.stepNUDDemo)).EndInit();
			this.tdPanelDemo.ResumeLayout(false);
			this.tdSmallPanelDemo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.unifyNUDDemo)).EndInit();
			this.bigFLP.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.FlowLayoutPanel stepFLPDemo;
		private System.Windows.Forms.Panel stepPanelDemo;
		private System.Windows.Forms.Panel tdPanelDemo;
		private System.Windows.Forms.Panel tdSmallPanelDemo;
		private System.Windows.Forms.Label stepLabelDemo;
		private System.Windows.Forms.NumericUpDown stepNUDDemo;
		private System.Windows.Forms.Button bottomButtonDemo;
		private System.Windows.Forms.Button topButtonDemo;
		private System.Windows.Forms.Label tdLabelDemo;
		private System.Windows.Forms.FlowLayoutPanel bigFLP;
		private System.Windows.Forms.Button unifyBottomButtonDemo;
		private System.Windows.Forms.Button unifyTopButtonDemo;
		private System.Windows.Forms.NumericUpDown unifyNUDDemo;
		private System.Windows.Forms.Button unifyValueButtonDemo;
		private System.Windows.Forms.ComboBox unifyComboBoxDemo;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
	}
}