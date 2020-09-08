namespace LightController.MyForm.Multiplex
{
	partial class DetailMultiAstForm
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
			this.bigFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.lightPanelDemo = new System.Windows.Forms.Panel();
			this.lightLabelDemo = new System.Windows.Forms.Label();
			this.allCheckBoxDemo = new System.Windows.Forms.CheckBox();
			this.lightFLPDemo = new System.Windows.Forms.FlowLayoutPanel();
			this.tdCheckBoxDemo = new System.Windows.Forms.CheckBox();
			this.enterButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.myStatusStrip = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.groupComboBox = new System.Windows.Forms.ComboBox();
			this.groupLabel = new System.Windows.Forms.Label();
			this.bigFLP.SuspendLayout();
			this.lightPanelDemo.SuspendLayout();
			this.lightFLPDemo.SuspendLayout();
			this.myStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// bigFLP
			// 
			this.bigFLP.AutoScroll = true;
			this.bigFLP.BackColor = System.Drawing.SystemColors.Window;
			this.bigFLP.Controls.Add(this.lightPanelDemo);
			this.bigFLP.Dock = System.Windows.Forms.DockStyle.Top;
			this.bigFLP.Location = new System.Drawing.Point(0, 0);
			this.bigFLP.Name = "bigFLP";
			this.bigFLP.Size = new System.Drawing.Size(1037, 516);
			this.bigFLP.TabIndex = 0;
			this.bigFLP.WrapContents = false;
			// 
			// lightPanelDemo
			// 
			this.lightPanelDemo.BackColor = System.Drawing.Color.LightBlue;
			this.lightPanelDemo.Controls.Add(this.lightLabelDemo);
			this.lightPanelDemo.Controls.Add(this.allCheckBoxDemo);
			this.lightPanelDemo.Controls.Add(this.lightFLPDemo);
			this.lightPanelDemo.Location = new System.Drawing.Point(3, 3);
			this.lightPanelDemo.Name = "lightPanelDemo";
			this.lightPanelDemo.Size = new System.Drawing.Size(110, 489);
			this.lightPanelDemo.TabIndex = 1;
			this.lightPanelDemo.Visible = false;
			// 
			// lightLabelDemo
			// 
			this.lightLabelDemo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightLabelDemo.Location = new System.Drawing.Point(4, 0);
			this.lightLabelDemo.Name = "lightLabelDemo";
			this.lightLabelDemo.Size = new System.Drawing.Size(106, 48);
			this.lightLabelDemo.TabIndex = 4;
			this.lightLabelDemo.Text = "15通道染色摇头灯\r\n（100-255）";
			this.lightLabelDemo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// allCheckBoxDemo
			// 
			this.allCheckBoxDemo.AutoSize = true;
			this.allCheckBoxDemo.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.allCheckBoxDemo.Location = new System.Drawing.Point(22, 57);
			this.allCheckBoxDemo.Name = "allCheckBoxDemo";
			this.allCheckBoxDemo.Size = new System.Drawing.Size(54, 17);
			this.allCheckBoxDemo.TabIndex = 3;
			this.allCheckBoxDemo.Text = "全选";
			this.allCheckBoxDemo.UseVisualStyleBackColor = true;
			this.allCheckBoxDemo.CheckedChanged += new System.EventHandler(this.allCheckBox_CheckedChanged);
			// 
			// lightFLPDemo
			// 
			this.lightFLPDemo.AutoScroll = true;
			this.lightFLPDemo.Controls.Add(this.tdCheckBoxDemo);
			this.lightFLPDemo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lightFLPDemo.Location = new System.Drawing.Point(0, 77);
			this.lightFLPDemo.Margin = new System.Windows.Forms.Padding(0);
			this.lightFLPDemo.Name = "lightFLPDemo";
			this.lightFLPDemo.Size = new System.Drawing.Size(110, 412);
			this.lightFLPDemo.TabIndex = 2;
			// 
			// tdCheckBoxDemo
			// 
			this.tdCheckBoxDemo.Location = new System.Drawing.Point(3, 3);
			this.tdCheckBoxDemo.Name = "tdCheckBoxDemo";
			this.tdCheckBoxDemo.Size = new System.Drawing.Size(87, 16);
			this.tdCheckBoxDemo.TabIndex = 0;
			this.tdCheckBoxDemo.Text = "X/Y轴转速";
			this.tdCheckBoxDemo.UseVisualStyleBackColor = true;
			this.tdCheckBoxDemo.CheckedChanged += new System.EventHandler(this.tdCheckBox_CheckedChanged);
			// 
			// enterButton
			// 
			this.enterButton.BackColor = System.Drawing.Color.Transparent;
			this.enterButton.Location = new System.Drawing.Point(848, 538);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(86, 43);
			this.enterButton.TabIndex = 1;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = false;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(942, 538);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(86, 43);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 538);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(526, 43);
			this.label1.TabIndex = 2;
			this.label1.Text = "提示：\r\n①请不要一次性选择过多通道，否则多步联调界面可能出现卡顿的情况（上限为50通道）；\r\n②若当前是多灯模式状态，可只选择多灯内的其中一个灯具，更改操作会作" +
    "用于组内的每个灯具。";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// myStatusStrip
			// 
			this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.myStatusStrip.Location = new System.Drawing.Point(0, 602);
			this.myStatusStrip.Name = "myStatusStrip";
			this.myStatusStrip.Size = new System.Drawing.Size(1037, 22);
			this.myStatusStrip.SizingGrip = false;
			this.myStatusStrip.TabIndex = 3;
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(1022, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupComboBox
			// 
			this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.groupComboBox.FormattingEnabled = true;
			this.groupComboBox.Items.AddRange(new object[] {
            "请选择编组"});
			this.groupComboBox.Location = new System.Drawing.Point(627, 561);
			this.groupComboBox.Name = "groupComboBox";
			this.groupComboBox.Size = new System.Drawing.Size(106, 20);
			this.groupComboBox.TabIndex = 5;
			this.groupComboBox.SelectedIndexChanged += new System.EventHandler(this.groupComboBox_SelectedIndexChanged);
			// 
			// groupLabel
			// 
			this.groupLabel.AutoSize = true;
			this.groupLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.groupLabel.Location = new System.Drawing.Point(627, 541);
			this.groupLabel.Name = "groupLabel";
			this.groupLabel.Size = new System.Drawing.Size(65, 12);
			this.groupLabel.TabIndex = 7;
			this.groupLabel.Text = "灯具编组：";
			// 
			// DetailMultiAstForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(1037, 624);
			this.Controls.Add(this.groupLabel);
			this.Controls.Add(this.groupComboBox);
			this.Controls.Add(this.myStatusStrip);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.bigFLP);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "DetailMultiAstForm";
			this.Text = "请选择多灯多步联调的通道";
			this.Load += new System.EventHandler(this.DetailMultiAstForm_Load);
			this.bigFLP.ResumeLayout(false);
			this.lightPanelDemo.ResumeLayout(false);
			this.lightPanelDemo.PerformLayout();
			this.lightFLPDemo.ResumeLayout(false);
			this.myStatusStrip.ResumeLayout(false);
			this.myStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel bigFLP;
		private System.Windows.Forms.Panel lightPanelDemo;
		private System.Windows.Forms.FlowLayoutPanel lightFLPDemo;
		private System.Windows.Forms.CheckBox tdCheckBoxDemo;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.CheckBox allCheckBoxDemo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.StatusStrip myStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.ComboBox groupComboBox;
		private System.Windows.Forms.Label groupLabel;
		private System.Windows.Forms.Label lightLabelDemo;
	}
}