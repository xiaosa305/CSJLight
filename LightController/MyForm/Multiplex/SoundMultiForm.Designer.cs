namespace LightController.MyForm.Multiplex
{
	partial class SoundMultiForm
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
			this.stepFlowLayoutPanelDemo = new System.Windows.Forms.FlowLayoutPanel();
			this.stepPanelDemo = new System.Windows.Forms.Panel();
			this.stepLabelDemo = new System.Windows.Forms.Label();
			this.topButtonDemo = new System.Windows.Forms.Button();
			this.bottomButtonDemo = new System.Windows.Forms.Button();
			this.stepNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.tdPanel = new System.Windows.Forms.Panel();
			this.tdSmallPanel = new System.Windows.Forms.Panel();
			this.comboBoxDemo = new System.Windows.Forms.ComboBox();
			this.unifyTopButtonDemo = new System.Windows.Forms.Button();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.button1 = new System.Windows.Forms.Button();
			this.unifyBottomButtonDemo = new System.Windows.Forms.Button();
			this.tdLabelDemo = new System.Windows.Forms.Label();
			this.bigFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.stepFlowLayoutPanelDemo.SuspendLayout();
			this.stepPanelDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.stepNUDDemo)).BeginInit();
			this.tdPanel.SuspendLayout();
			this.tdSmallPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.bigFlowLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// stepFlowLayoutPanelDemo
			// 
			this.stepFlowLayoutPanelDemo.AutoScroll = true;
			this.stepFlowLayoutPanelDemo.Controls.Add(this.stepPanelDemo);
			this.stepFlowLayoutPanelDemo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stepFlowLayoutPanelDemo.Location = new System.Drawing.Point(123, 0);
			this.stepFlowLayoutPanelDemo.Name = "stepFlowLayoutPanelDemo";
			this.stepFlowLayoutPanelDemo.Size = new System.Drawing.Size(802, 108);
			this.stepFlowLayoutPanelDemo.TabIndex = 1;
			this.stepFlowLayoutPanelDemo.WrapContents = false;
			// 
			// stepPanelDemo
			// 
			this.stepPanelDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
			this.stepNUDDemo.Location = new System.Drawing.Point(1, 56);
			this.stepNUDDemo.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.stepNUDDemo.Name = "stepNUDDemo";
			this.stepNUDDemo.Size = new System.Drawing.Size(46, 21);
			this.stepNUDDemo.TabIndex = 1;
			this.stepNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.stepNUDDemo.ValueChanged += new System.EventHandler(this.StepNUD_ValueChanged);
			// 
			// tdPanel
			// 
			this.tdPanel.Controls.Add(this.stepFlowLayoutPanelDemo);
			this.tdPanel.Controls.Add(this.tdSmallPanel);
			this.tdPanel.Location = new System.Drawing.Point(3, 3);
			this.tdPanel.Name = "tdPanel";
			this.tdPanel.Size = new System.Drawing.Size(925, 108);
			this.tdPanel.TabIndex = 2;
			// 
			// tdSmallPanel
			// 
			this.tdSmallPanel.BackColor = System.Drawing.Color.Beige;
			this.tdSmallPanel.Controls.Add(this.comboBoxDemo);
			this.tdSmallPanel.Controls.Add(this.unifyTopButtonDemo);
			this.tdSmallPanel.Controls.Add(this.numericUpDown1);
			this.tdSmallPanel.Controls.Add(this.button1);
			this.tdSmallPanel.Controls.Add(this.unifyBottomButtonDemo);
			this.tdSmallPanel.Controls.Add(this.tdLabelDemo);
			this.tdSmallPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.tdSmallPanel.Location = new System.Drawing.Point(0, 0);
			this.tdSmallPanel.Name = "tdSmallPanel";
			this.tdSmallPanel.Size = new System.Drawing.Size(123, 108);
			this.tdSmallPanel.TabIndex = 0;
			// 
			// comboBoxDemo
			// 
			this.comboBoxDemo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDemo.FormattingEnabled = true;
			this.comboBoxDemo.Items.AddRange(new object[] {
            "全步",
            "单步",
            "双步"});
			this.comboBoxDemo.Location = new System.Drawing.Point(6, 53);
			this.comboBoxDemo.Name = "comboBoxDemo";
			this.comboBoxDemo.Size = new System.Drawing.Size(48, 20);
			this.comboBoxDemo.TabIndex = 4;
			// 
			// unifyTopButtonDemo
			// 
			this.unifyTopButtonDemo.Location = new System.Drawing.Point(63, 52);
			this.unifyTopButtonDemo.Name = "unifyTopButtonDemo";
			this.unifyTopButtonDemo.Size = new System.Drawing.Size(22, 23);
			this.unifyTopButtonDemo.TabIndex = 3;
			this.unifyTopButtonDemo.Text = "↑";
			this.unifyTopButtonDemo.UseVisualStyleBackColor = true;
			this.unifyTopButtonDemo.Click += new System.EventHandler(this.unifyTopButtonDemo_Click);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(6, 82);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(48, 21);
			this.numericUpDown1.TabIndex = 1;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericUpDown1.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(62, 81);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(50, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "设值";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// unifyBottomButtonDemo
			// 
			this.unifyBottomButtonDemo.Location = new System.Drawing.Point(90, 52);
			this.unifyBottomButtonDemo.Name = "unifyBottomButtonDemo";
			this.unifyBottomButtonDemo.Size = new System.Drawing.Size(22, 23);
			this.unifyBottomButtonDemo.TabIndex = 2;
			this.unifyBottomButtonDemo.Text = "↓";
			this.unifyBottomButtonDemo.UseVisualStyleBackColor = true;
			this.unifyBottomButtonDemo.Click += new System.EventHandler(this.unifyBottomButtonDemo_Click);
			// 
			// tdLabelDemo
			// 
			this.tdLabelDemo.Location = new System.Drawing.Point(3, 6);
			this.tdLabelDemo.Name = "tdLabelDemo";
			this.tdLabelDemo.Size = new System.Drawing.Size(114, 39);
			this.tdLabelDemo.TabIndex = 0;
			this.tdLabelDemo.Text = "通道：\r\n11通道摇头灯\r\n总调光\r\n";
			// 
			// bigFlowLayoutPanel
			// 
			this.bigFlowLayoutPanel.BackColor = System.Drawing.Color.Gray;
			this.bigFlowLayoutPanel.Controls.Add(this.tdPanel);
			this.bigFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bigFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.bigFlowLayoutPanel.Name = "bigFlowLayoutPanel";
			this.bigFlowLayoutPanel.Size = new System.Drawing.Size(928, 332);
			this.bigFlowLayoutPanel.TabIndex = 3;
			// 
			// SoundMultiForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(928, 332);
			this.Controls.Add(this.bigFlowLayoutPanel);
			this.Name = "SoundMultiForm";
			this.Text = "多步联调(音频模式)";
			this.Load += new System.EventHandler(this.SoundMultiForm_Load);
			this.stepFlowLayoutPanelDemo.ResumeLayout(false);
			this.stepPanelDemo.ResumeLayout(false);
			this.stepPanelDemo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.stepNUDDemo)).EndInit();
			this.tdPanel.ResumeLayout(false);
			this.tdSmallPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.bigFlowLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.FlowLayoutPanel stepFlowLayoutPanelDemo;
		private System.Windows.Forms.Panel stepPanelDemo;
		private System.Windows.Forms.Panel tdPanel;
		private System.Windows.Forms.Panel tdSmallPanel;
		private System.Windows.Forms.Label stepLabelDemo;
		private System.Windows.Forms.NumericUpDown stepNUDDemo;
		private System.Windows.Forms.Button bottomButtonDemo;
		private System.Windows.Forms.Button topButtonDemo;
		private System.Windows.Forms.Label tdLabelDemo;
		private System.Windows.Forms.FlowLayoutPanel bigFlowLayoutPanel;
		private System.Windows.Forms.Button unifyBottomButtonDemo;
		private System.Windows.Forms.Button unifyTopButtonDemo;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox comboBoxDemo;
	}
}