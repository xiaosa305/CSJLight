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
			this.stepBigPanel = new System.Windows.Forms.Panel();
			this.unifyPanel = new System.Windows.Forms.Panel();
			this.unifyComboBox = new System.Windows.Forms.ComboBox();
			this.unifyNUD = new System.Windows.Forms.NumericUpDown();
			this.unifyTopButton = new System.Windows.Forms.Button();
			this.unifyBottomButton = new System.Windows.Forms.Button();
			this.unifyValueButton = new System.Windows.Forms.Button();
			this.stepShowFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.stepShowPanelDemo = new System.Windows.Forms.Panel();
			this.stepLabelDemo = new System.Windows.Forms.Label();
			this.stepCheckBoxDemo = new System.Windows.Forms.CheckBox();
			this.unifyHScrollBar = new System.Windows.Forms.HScrollBar();
			this.stepFLPDemo = new System.Windows.Forms.FlowLayoutPanel();
			this.stepPanelDemo = new System.Windows.Forms.Panel();
			this.topBottomButtonDemo = new System.Windows.Forms.Button();
			this.stepNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.tdSmallPanelDemo = new System.Windows.Forms.Panel();
			this.tdLabelDemo = new System.Windows.Forms.Label();
			this.saComboBoxDemo = new System.Windows.Forms.ComboBox();
			this.bigFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.tdPanelDemo = new System.Windows.Forms.Panel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.stepBigPanel.SuspendLayout();
			this.unifyPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.unifyNUD)).BeginInit();
			this.stepShowFLP.SuspendLayout();
			this.stepShowPanelDemo.SuspendLayout();
			this.stepFLPDemo.SuspendLayout();
			this.stepPanelDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.stepNUDDemo)).BeginInit();
			this.tdSmallPanelDemo.SuspendLayout();
			this.bigFLP.SuspendLayout();
			this.tdPanelDemo.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// stepBigPanel
			// 
			this.stepBigPanel.Controls.Add(this.unifyPanel);
			this.stepBigPanel.Controls.Add(this.stepShowFLP);
			this.stepBigPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.stepBigPanel.Location = new System.Drawing.Point(0, 0);
			this.stepBigPanel.Name = "stepBigPanel";
			this.stepBigPanel.Size = new System.Drawing.Size(1227, 60);
			this.stepBigPanel.TabIndex = 15;
			// 
			// unifyPanel
			// 
			this.unifyPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.unifyPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.unifyPanel.Controls.Add(this.unifyComboBox);
			this.unifyPanel.Controls.Add(this.unifyNUD);
			this.unifyPanel.Controls.Add(this.unifyTopButton);
			this.unifyPanel.Controls.Add(this.unifyBottomButton);
			this.unifyPanel.Controls.Add(this.unifyValueButton);
			this.unifyPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.unifyPanel.Location = new System.Drawing.Point(0, 0);
			this.unifyPanel.Margin = new System.Windows.Forms.Padding(1);
			this.unifyPanel.Name = "unifyPanel";
			this.unifyPanel.Size = new System.Drawing.Size(118, 60);
			this.unifyPanel.TabIndex = 1;
			// 
			// unifyComboBox
			// 
			this.unifyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.unifyComboBox.FormattingEnabled = true;
			this.unifyComboBox.Items.AddRange(new object[] {
            "全步",
            "单步",
            "双步",
            "清空"});
			this.unifyComboBox.Location = new System.Drawing.Point(4, 6);
			this.unifyComboBox.Name = "unifyComboBox";
			this.unifyComboBox.Size = new System.Drawing.Size(60, 20);
			this.unifyComboBox.TabIndex = 23;
			this.unifyComboBox.SelectedIndexChanged += new System.EventHandler(this.unifyComboBox_SelectedIndexChanged);
			// 
			// unifyNUD
			// 
			this.unifyNUD.Location = new System.Drawing.Point(70, 6);
			this.unifyNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.unifyNUD.Name = "unifyNUD";
			this.unifyNUD.Size = new System.Drawing.Size(40, 21);
			this.unifyNUD.TabIndex = 19;
			this.unifyNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// unifyTopButton
			// 
			this.unifyTopButton.Location = new System.Drawing.Point(5, 30);
			this.unifyTopButton.Name = "unifyTopButton";
			this.unifyTopButton.Size = new System.Drawing.Size(25, 23);
			this.unifyTopButton.TabIndex = 22;
			this.unifyTopButton.Tag = "255";
			this.unifyTopButton.Text = "↑";
			this.unifyTopButton.UseVisualStyleBackColor = true;
			this.unifyTopButton.Click += new System.EventHandler(this.unifyValueButton_Click);
			// 
			// unifyBottomButton
			// 
			this.unifyBottomButton.Location = new System.Drawing.Point(37, 30);
			this.unifyBottomButton.Name = "unifyBottomButton";
			this.unifyBottomButton.Size = new System.Drawing.Size(25, 23);
			this.unifyBottomButton.TabIndex = 21;
			this.unifyBottomButton.Tag = "0";
			this.unifyBottomButton.Text = "↓";
			this.unifyBottomButton.UseVisualStyleBackColor = true;
			this.unifyBottomButton.Click += new System.EventHandler(this.unifyValueButton_Click);
			// 
			// unifyValueButton
			// 
			this.unifyValueButton.Location = new System.Drawing.Point(70, 30);
			this.unifyValueButton.Name = "unifyValueButton";
			this.unifyValueButton.Size = new System.Drawing.Size(40, 23);
			this.unifyValueButton.TabIndex = 20;
			this.unifyValueButton.Text = "设值";
			this.unifyValueButton.UseVisualStyleBackColor = true;
			this.unifyValueButton.Click += new System.EventHandler(this.unifyValueButton_Click);
			// 
			// stepShowFLP
			// 
			this.stepShowFLP.AutoScroll = true;
			this.stepShowFLP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.stepShowFLP.BackColor = System.Drawing.Color.Transparent;
			this.stepShowFLP.Controls.Add(this.stepShowPanelDemo);
			this.stepShowFLP.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.stepShowFLP.Location = new System.Drawing.Point(121, 3);
			this.stepShowFLP.Name = "stepShowFLP";
			this.stepShowFLP.Size = new System.Drawing.Size(1084, 79);
			this.stepShowFLP.TabIndex = 2;
			// 
			// stepShowPanelDemo
			// 
			this.stepShowPanelDemo.BackColor = System.Drawing.Color.Transparent;
			this.stepShowPanelDemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.stepShowPanelDemo.Controls.Add(this.stepLabelDemo);
			this.stepShowPanelDemo.Controls.Add(this.stepCheckBoxDemo);
			this.stepShowPanelDemo.Location = new System.Drawing.Point(0, 0);
			this.stepShowPanelDemo.Margin = new System.Windows.Forms.Padding(0);
			this.stepShowPanelDemo.Name = "stepShowPanelDemo";
			this.stepShowPanelDemo.Size = new System.Drawing.Size(50, 54);
			this.stepShowPanelDemo.TabIndex = 1;
			this.stepShowPanelDemo.Visible = false;
			// 
			// stepLabelDemo
			// 
			this.stepLabelDemo.AutoSize = true;
			this.stepLabelDemo.BackColor = System.Drawing.Color.Transparent;
			this.stepLabelDemo.ForeColor = System.Drawing.Color.Black;
			this.stepLabelDemo.Location = new System.Drawing.Point(0, 8);
			this.stepLabelDemo.Name = "stepLabelDemo";
			this.stepLabelDemo.Size = new System.Drawing.Size(47, 12);
			this.stepLabelDemo.TabIndex = 0;
			this.stepLabelDemo.Text = "第100步";
			// 
			// stepCheckBoxDemo
			// 
			this.stepCheckBoxDemo.AutoSize = true;
			this.stepCheckBoxDemo.BackColor = System.Drawing.Color.Transparent;
			this.stepCheckBoxDemo.ForeColor = System.Drawing.Color.Black;
			this.stepCheckBoxDemo.Location = new System.Drawing.Point(15, 32);
			this.stepCheckBoxDemo.Name = "stepCheckBoxDemo";
			this.stepCheckBoxDemo.Size = new System.Drawing.Size(15, 14);
			this.stepCheckBoxDemo.TabIndex = 1;
			this.stepCheckBoxDemo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.stepCheckBoxDemo.UseVisualStyleBackColor = false;
			// 
			// unifyHScrollBar
			// 
			this.unifyHScrollBar.LargeChange = 848;
			this.unifyHScrollBar.Location = new System.Drawing.Point(119, -2);
			this.unifyHScrollBar.Maximum = 1242;
			this.unifyHScrollBar.Name = "unifyHScrollBar";
			this.unifyHScrollBar.Size = new System.Drawing.Size(1084, 18);
			this.unifyHScrollBar.TabIndex = 16;
			this.unifyHScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.unifyHScrollBar_Scroll);
			// 
			// stepFLPDemo
			// 
			this.stepFLPDemo.AutoScroll = true;
			this.stepFLPDemo.BackColor = System.Drawing.Color.Gray;
			this.stepFLPDemo.Controls.Add(this.stepPanelDemo);
			this.stepFLPDemo.Location = new System.Drawing.Point(118, 0);
			this.stepFLPDemo.Name = "stepFLPDemo";
			this.stepFLPDemo.Size = new System.Drawing.Size(1084, 81);
			this.stepFLPDemo.TabIndex = 2;
			this.stepFLPDemo.WrapContents = false;
			// 
			// stepPanelDemo
			// 
			this.stepPanelDemo.BackColor = System.Drawing.Color.Transparent;
			this.stepPanelDemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.stepPanelDemo.Controls.Add(this.topBottomButtonDemo);
			this.stepPanelDemo.Controls.Add(this.stepNUDDemo);
			this.stepPanelDemo.Location = new System.Drawing.Point(0, 0);
			this.stepPanelDemo.Margin = new System.Windows.Forms.Padding(0);
			this.stepPanelDemo.Name = "stepPanelDemo";
			this.stepPanelDemo.Size = new System.Drawing.Size(50, 57);
			this.stepPanelDemo.TabIndex = 1;
			// 
			// topBottomButtonDemo
			// 
			this.topBottomButtonDemo.Location = new System.Drawing.Point(3, 4);
			this.topBottomButtonDemo.Name = "topBottomButtonDemo";
			this.topBottomButtonDemo.Size = new System.Drawing.Size(40, 20);
			this.topBottomButtonDemo.TabIndex = 2;
			this.topBottomButtonDemo.Text = "↑↓";
			this.topBottomButtonDemo.UseVisualStyleBackColor = true;
			this.topBottomButtonDemo.Click += new System.EventHandler(this.topBottomButton_Click);
			// 
			// stepNUDDemo
			// 
			this.stepNUDDemo.Location = new System.Drawing.Point(3, 29);
			this.stepNUDDemo.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.stepNUDDemo.Name = "stepNUDDemo";
			this.stepNUDDemo.Size = new System.Drawing.Size(40, 21);
			this.stepNUDDemo.TabIndex = 3;
			this.stepNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.stepNUDDemo.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.stepNUDDemo.ValueChanged += new System.EventHandler(this.StepNUD_ValueChanged);
			this.stepNUDDemo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.stepNUD_MouseDoubleClick);
			// 
			// tdSmallPanelDemo
			// 
			this.tdSmallPanelDemo.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.tdSmallPanelDemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tdSmallPanelDemo.Controls.Add(this.tdLabelDemo);
			this.tdSmallPanelDemo.Controls.Add(this.saComboBoxDemo);
			this.tdSmallPanelDemo.Dock = System.Windows.Forms.DockStyle.Left;
			this.tdSmallPanelDemo.Location = new System.Drawing.Point(0, 0);
			this.tdSmallPanelDemo.Margin = new System.Windows.Forms.Padding(1);
			this.tdSmallPanelDemo.Name = "tdSmallPanelDemo";
			this.tdSmallPanelDemo.Size = new System.Drawing.Size(118, 58);
			this.tdSmallPanelDemo.TabIndex = 1;
			// 
			// tdLabelDemo
			// 
			this.tdLabelDemo.Location = new System.Drawing.Point(5, 3);
			this.tdLabelDemo.Name = "tdLabelDemo";
			this.tdLabelDemo.Size = new System.Drawing.Size(110, 24);
			this.tdLabelDemo.TabIndex = 17;
			this.tdLabelDemo.Text = "15通道摇头染色灯\r\n红色\r\n";
			// 
			// saComboBoxDemo
			// 
			this.saComboBoxDemo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.saComboBoxDemo.FormattingEnabled = true;
			this.saComboBoxDemo.Location = new System.Drawing.Point(5, 32);
			this.saComboBoxDemo.Name = "saComboBoxDemo";
			this.saComboBoxDemo.Size = new System.Drawing.Size(106, 20);
			this.saComboBoxDemo.TabIndex = 18;
			this.saComboBoxDemo.Visible = false;
			// 
			// bigFLP
			// 
			this.bigFLP.AutoScroll = true;
			this.bigFLP.BackColor = System.Drawing.Color.Transparent;
			this.bigFLP.Controls.Add(this.tdPanelDemo);
			this.bigFLP.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bigFLP.Location = new System.Drawing.Point(0, 60);
			this.bigFLP.Margin = new System.Windows.Forms.Padding(0);
			this.bigFLP.Name = "bigFLP";
			this.bigFLP.Size = new System.Drawing.Size(1227, 706);
			this.bigFLP.TabIndex = 17;
			// 
			// tdPanelDemo
			// 
			this.tdPanelDemo.Controls.Add(this.tdSmallPanelDemo);
			this.tdPanelDemo.Controls.Add(this.stepFLPDemo);
			this.tdPanelDemo.Location = new System.Drawing.Point(3, 3);
			this.tdPanelDemo.Name = "tdPanelDemo";
			this.tdPanelDemo.Size = new System.Drawing.Size(1202, 58);
			this.tdPanelDemo.TabIndex = 17;
			this.tdPanelDemo.Tag = "-1";
			this.tdPanelDemo.Visible = false;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 766);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1227, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 18;
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(1212, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.unifyHScrollBar);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 746);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1227, 20);
			this.panel1.TabIndex = 19;
			// 
			// DetailMultiForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(1227, 788);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.bigFLP);
			this.Controls.Add(this.stepBigPanel);
			this.Controls.Add(this.statusStrip1);
			this.Name = "DetailMultiForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "多通道多步联调";
			this.Load += new System.EventHandler(this.DetailMultiForm_Load);
			this.stepBigPanel.ResumeLayout(false);
			this.unifyPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.unifyNUD)).EndInit();
			this.stepShowFLP.ResumeLayout(false);
			this.stepShowPanelDemo.ResumeLayout(false);
			this.stepShowPanelDemo.PerformLayout();
			this.stepFLPDemo.ResumeLayout(false);
			this.stepPanelDemo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.stepNUDDemo)).EndInit();
			this.tdSmallPanelDemo.ResumeLayout(false);
			this.bigFLP.ResumeLayout(false);
			this.tdPanelDemo.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel stepBigPanel;
		private System.Windows.Forms.HScrollBar unifyHScrollBar;
		private System.Windows.Forms.FlowLayoutPanel stepShowFLP;
		private System.Windows.Forms.Panel unifyPanel;
		private System.Windows.Forms.ComboBox unifyComboBox;
		private System.Windows.Forms.Button unifyTopButton;
		private System.Windows.Forms.Button unifyBottomButton;
		private System.Windows.Forms.NumericUpDown unifyNUD;
		private System.Windows.Forms.Button unifyValueButton;
		private System.Windows.Forms.Panel stepShowPanelDemo;
		private System.Windows.Forms.Panel tdSmallPanelDemo;
		private System.Windows.Forms.Label tdLabelDemo;
		private System.Windows.Forms.ComboBox saComboBoxDemo;
		private System.Windows.Forms.FlowLayoutPanel stepFLPDemo;
		private System.Windows.Forms.Panel stepPanelDemo;
		private System.Windows.Forms.Button topBottomButtonDemo;
		private System.Windows.Forms.NumericUpDown stepNUDDemo;
		private System.Windows.Forms.FlowLayoutPanel bigFLP;
		private System.Windows.Forms.Panel tdPanelDemo;
		private System.Windows.Forms.Label stepLabelDemo;
		private System.Windows.Forms.CheckBox stepCheckBoxDemo;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.Panel panel1;
	}
}