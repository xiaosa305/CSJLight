namespace LightController.MyForm.Multiplex
{
	partial class DetailSingleForm
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
			this.tdSmallPanel = new System.Windows.Forms.Panel();
			this.stepPanelDemo = new System.Windows.Forms.Panel();
			this.topBottomButtonDemo = new System.Windows.Forms.Button();
			this.stepLabelDemo = new System.Windows.Forms.Label();
			this.stepNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.saComboBox = new System.Windows.Forms.ComboBox();
			this.unifyComboBox = new System.Windows.Forms.ComboBox();
			this.unifyTopButton = new System.Windows.Forms.Button();
			this.unifyNUD = new System.Windows.Forms.NumericUpDown();
			this.unifyValueButton = new System.Windows.Forms.Button();
			this.unifyBottomButton = new System.Windows.Forms.Button();
			this.stepFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tdSmallPanel.SuspendLayout();
			this.stepPanelDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.stepNUDDemo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.unifyNUD)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tdSmallPanel
			// 
			this.tdSmallPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.tdSmallPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tdSmallPanel.Controls.Add(this.stepPanelDemo);
			this.tdSmallPanel.Controls.Add(this.saComboBox);
			this.tdSmallPanel.Controls.Add(this.unifyComboBox);
			this.tdSmallPanel.Controls.Add(this.unifyTopButton);
			this.tdSmallPanel.Controls.Add(this.unifyNUD);
			this.tdSmallPanel.Controls.Add(this.unifyValueButton);
			this.tdSmallPanel.Controls.Add(this.unifyBottomButton);
			this.tdSmallPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.tdSmallPanel.Location = new System.Drawing.Point(0, 0);
			this.tdSmallPanel.Name = "tdSmallPanel";
			this.tdSmallPanel.Size = new System.Drawing.Size(123, 641);
			this.tdSmallPanel.TabIndex = 1;
			// 
			// stepPanelDemo
			// 
			this.stepPanelDemo.BackColor = System.Drawing.Color.IndianRed;
			this.stepPanelDemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.stepPanelDemo.Controls.Add(this.topBottomButtonDemo);
			this.stepPanelDemo.Controls.Add(this.stepLabelDemo);
			this.stepPanelDemo.Controls.Add(this.stepNUDDemo);
			this.stepPanelDemo.Location = new System.Drawing.Point(33, 177);
			this.stepPanelDemo.Name = "stepPanelDemo";
			this.stepPanelDemo.Size = new System.Drawing.Size(52, 85);
			this.stepPanelDemo.TabIndex = 0;
			this.stepPanelDemo.Visible = false;
			// 
			// topBottomButtonDemo
			// 
			this.topBottomButtonDemo.Location = new System.Drawing.Point(4, 30);
			this.topBottomButtonDemo.Name = "topBottomButtonDemo";
			this.topBottomButtonDemo.Size = new System.Drawing.Size(40, 20);
			this.topBottomButtonDemo.TabIndex = 3;
			this.topBottomButtonDemo.Text = "↑↓";
			this.topBottomButtonDemo.UseVisualStyleBackColor = true;
			this.topBottomButtonDemo.Click += new System.EventHandler(this.topBottomButton_Click);
			// 
			// stepLabelDemo
			// 
			this.stepLabelDemo.AutoSize = true;
			this.stepLabelDemo.ForeColor = System.Drawing.Color.White;
			this.stepLabelDemo.Location = new System.Drawing.Point(3, 9);
			this.stepLabelDemo.Name = "stepLabelDemo";
			this.stepLabelDemo.Size = new System.Drawing.Size(47, 12);
			this.stepLabelDemo.TabIndex = 2;
			this.stepLabelDemo.Text = "第100步";
			// 
			// stepNUDDemo
			// 
			this.stepNUDDemo.Location = new System.Drawing.Point(4, 56);
			this.stepNUDDemo.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.stepNUDDemo.Name = "stepNUDDemo";
			this.stepNUDDemo.Size = new System.Drawing.Size(40, 21);
			this.stepNUDDemo.TabIndex = 1;
			this.stepNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.stepNUDDemo.ValueChanged += new System.EventHandler(this.StepNUD_ValueChanged);
			// 
			// saComboBox
			// 
			this.saComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.saComboBox.FormattingEnabled = true;
			this.saComboBox.Location = new System.Drawing.Point(6, 79);
			this.saComboBox.Name = "saComboBox";
			this.saComboBox.Size = new System.Drawing.Size(106, 20);
			this.saComboBox.TabIndex = 5;
			this.saComboBox.Visible = false;
			// 
			// unifyComboBox
			// 
			this.unifyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.unifyComboBox.FormattingEnabled = true;
			this.unifyComboBox.Items.AddRange(new object[] {
            "全步",
            "奇数步",
            "偶数步"});
			this.unifyComboBox.Location = new System.Drawing.Point(6, 14);
			this.unifyComboBox.Name = "unifyComboBox";
			this.unifyComboBox.Size = new System.Drawing.Size(57, 20);
			this.unifyComboBox.TabIndex = 4;
			// 
			// unifyTopButton
			// 
			this.unifyTopButton.Location = new System.Drawing.Point(66, 13);
			this.unifyTopButton.Name = "unifyTopButton";
			this.unifyTopButton.Size = new System.Drawing.Size(22, 23);
			this.unifyTopButton.TabIndex = 3;
			this.unifyTopButton.Tag = "255";
			this.unifyTopButton.Text = "↑";
			this.unifyTopButton.UseVisualStyleBackColor = true;
			this.unifyTopButton.Click += new System.EventHandler(this.unifyValueButton_Click);
			// 
			// unifyNUD
			// 
			this.unifyNUD.Location = new System.Drawing.Point(6, 47);
			this.unifyNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.unifyNUD.Name = "unifyNUD";
			this.unifyNUD.Size = new System.Drawing.Size(48, 21);
			this.unifyNUD.TabIndex = 1;
			this.unifyNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.unifyNUD.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
			// 
			// unifyValueButton
			// 
			this.unifyValueButton.Location = new System.Drawing.Point(62, 46);
			this.unifyValueButton.Name = "unifyValueButton";
			this.unifyValueButton.Size = new System.Drawing.Size(50, 23);
			this.unifyValueButton.TabIndex = 2;
			this.unifyValueButton.Text = "设值";
			this.unifyValueButton.UseVisualStyleBackColor = true;
			this.unifyValueButton.Click += new System.EventHandler(this.unifyValueButton_Click);
			// 
			// unifyBottomButton
			// 
			this.unifyBottomButton.Location = new System.Drawing.Point(90, 13);
			this.unifyBottomButton.Name = "unifyBottomButton";
			this.unifyBottomButton.Size = new System.Drawing.Size(22, 23);
			this.unifyBottomButton.TabIndex = 2;
			this.unifyBottomButton.Tag = "0";
			this.unifyBottomButton.Text = "↓";
			this.unifyBottomButton.UseVisualStyleBackColor = true;
			this.unifyBottomButton.Click += new System.EventHandler(this.unifyValueButton_Click);
			// 
			// stepFLP
			// 
			this.stepFLP.AutoScroll = true;
			this.stepFLP.BackColor = System.Drawing.Color.Gray;
			this.stepFLP.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stepFLP.Location = new System.Drawing.Point(123, 0);
			this.stepFLP.Name = "stepFLP";
			this.stepFLP.Size = new System.Drawing.Size(893, 641);
			this.stepFLP.TabIndex = 2;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 641);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1016, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(1001, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DetailSingleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(1016, 663);
			this.Controls.Add(this.stepFLP);
			this.Controls.Add(this.tdSmallPanel);
			this.Controls.Add(this.statusStrip1);
			this.Name = "DetailSingleForm";
			this.Text = "单通道多步联调";
			this.Load += new System.EventHandler(this.DetailSingleForm_Load);
			this.tdSmallPanel.ResumeLayout(false);
			this.stepPanelDemo.ResumeLayout(false);
			this.stepPanelDemo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.stepNUDDemo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.unifyNUD)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel tdSmallPanel;
		private System.Windows.Forms.ComboBox saComboBox;
		private System.Windows.Forms.ComboBox unifyComboBox;
		private System.Windows.Forms.Button unifyTopButton;
		private System.Windows.Forms.NumericUpDown unifyNUD;
		private System.Windows.Forms.Button unifyValueButton;
		private System.Windows.Forms.Button unifyBottomButton;
		private System.Windows.Forms.FlowLayoutPanel stepFLP;
		private System.Windows.Forms.Panel stepPanelDemo;
		private System.Windows.Forms.Label stepLabelDemo;
		private System.Windows.Forms.NumericUpDown stepNUDDemo;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.Button topBottomButtonDemo;
	}
}