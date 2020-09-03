namespace LightController.MyForm.Multiplex
{
	partial class DetailMultiPageForm
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
			this.bigFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.tdPanelDemo = new System.Windows.Forms.Panel();
			this.stepFLPDemo = new System.Windows.Forms.FlowLayoutPanel();
			this.stepPanelDemo = new System.Windows.Forms.Panel();
			this.stepNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.topBottomButtonDemo = new System.Windows.Forms.Button();
			this.tdSmallPanelDemo = new System.Windows.Forms.Panel();
			this.tdCheckBoxDemo = new System.Windows.Forms.CheckBox();
			this.saComboBoxDemo = new System.Windows.Forms.ComboBox();
			this.unifyPanel = new System.Windows.Forms.Panel();
			this.nextPageButton = new System.Windows.Forms.Button();
			this.testButton = new System.Windows.Forms.Button();
			this.backPageButton = new System.Windows.Forms.Button();
			this.stepShowFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.stepShowPanelDemo = new System.Windows.Forms.Panel();
			this.stepCheckBoxDemo = new System.Windows.Forms.CheckBox();
			this.unifySmallPanel = new System.Windows.Forms.Panel();
			this.tdComboBox = new System.Windows.Forms.ComboBox();
			this.useTdCheckBox = new System.Windows.Forms.CheckBox();
			this.groupComboBox = new System.Windows.Forms.ComboBox();
			this.unifyComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.bigFLP.SuspendLayout();
			this.tdPanelDemo.SuspendLayout();
			this.stepFLPDemo.SuspendLayout();
			this.stepPanelDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.stepNUDDemo)).BeginInit();
			this.tdSmallPanelDemo.SuspendLayout();
			this.unifyPanel.SuspendLayout();
			this.stepShowFLP.SuspendLayout();
			this.stepShowPanelDemo.SuspendLayout();
			this.unifySmallPanel.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bigFLP
			// 
			this.bigFLP.AutoScroll = true;
			this.bigFLP.Controls.Add(this.tdPanelDemo);
			this.bigFLP.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bigFLP.Location = new System.Drawing.Point(0, 79);
			this.bigFLP.Name = "bigFLP";
			this.bigFLP.Size = new System.Drawing.Size(1134, 660);
			this.bigFLP.TabIndex = 0;
			// 
			// tdPanelDemo
			// 
			this.tdPanelDemo.Controls.Add(this.stepFLPDemo);
			this.tdPanelDemo.Controls.Add(this.tdSmallPanelDemo);
			this.tdPanelDemo.Location = new System.Drawing.Point(0, 0);
			this.tdPanelDemo.Margin = new System.Windows.Forms.Padding(0);
			this.tdPanelDemo.Name = "tdPanelDemo";
			this.tdPanelDemo.Size = new System.Drawing.Size(1071, 51);
			this.tdPanelDemo.TabIndex = 0;
			this.tdPanelDemo.Visible = false;
			// 
			// stepFLPDemo
			// 
			this.stepFLPDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.stepFLPDemo.Controls.Add(this.stepPanelDemo);
			this.stepFLPDemo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stepFLPDemo.Location = new System.Drawing.Point(187, 0);
			this.stepFLPDemo.Margin = new System.Windows.Forms.Padding(1);
			this.stepFLPDemo.Name = "stepFLPDemo";
			this.stepFLPDemo.Size = new System.Drawing.Size(884, 51);
			this.stepFLPDemo.TabIndex = 1;
			// 
			// stepPanelDemo
			// 
			this.stepPanelDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.stepPanelDemo.Controls.Add(this.stepNUDDemo);
			this.stepPanelDemo.Controls.Add(this.topBottomButtonDemo);
			this.stepPanelDemo.Location = new System.Drawing.Point(1, 1);
			this.stepPanelDemo.Margin = new System.Windows.Forms.Padding(1);
			this.stepPanelDemo.Name = "stepPanelDemo";
			this.stepPanelDemo.Size = new System.Drawing.Size(42, 49);
			this.stepPanelDemo.TabIndex = 1;
			// 
			// stepNUDDemo
			// 
			this.stepNUDDemo.Location = new System.Drawing.Point(1, 24);
			this.stepNUDDemo.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.stepNUDDemo.Name = "stepNUDDemo";
			this.stepNUDDemo.Size = new System.Drawing.Size(38, 21);
			this.stepNUDDemo.TabIndex = 2;
			this.stepNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.stepNUDDemo.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.stepNUDDemo.ValueChanged += new System.EventHandler(this.StepNUD_ValueChanged);
			// 
			// topBottomButtonDemo
			// 
			this.topBottomButtonDemo.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.topBottomButtonDemo.Location = new System.Drawing.Point(2, 3);
			this.topBottomButtonDemo.Name = "topBottomButtonDemo";
			this.topBottomButtonDemo.Size = new System.Drawing.Size(36, 18);
			this.topBottomButtonDemo.TabIndex = 1;
			this.topBottomButtonDemo.Text = "↑↓";
			this.topBottomButtonDemo.UseVisualStyleBackColor = true;
			this.topBottomButtonDemo.Click += new System.EventHandler(this.topBottomButton_Click);
			// 
			// tdSmallPanelDemo
			// 
			this.tdSmallPanelDemo.BackColor = System.Drawing.SystemColors.Menu;
			this.tdSmallPanelDemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tdSmallPanelDemo.Controls.Add(this.tdCheckBoxDemo);
			this.tdSmallPanelDemo.Controls.Add(this.saComboBoxDemo);
			this.tdSmallPanelDemo.Dock = System.Windows.Forms.DockStyle.Left;
			this.tdSmallPanelDemo.Location = new System.Drawing.Point(0, 0);
			this.tdSmallPanelDemo.Name = "tdSmallPanelDemo";
			this.tdSmallPanelDemo.Size = new System.Drawing.Size(187, 51);
			this.tdSmallPanelDemo.TabIndex = 0;
			// 
			// tdCheckBoxDemo
			// 
			this.tdCheckBoxDemo.AutoSize = true;
			this.tdCheckBoxDemo.Location = new System.Drawing.Point(3, 6);
			this.tdCheckBoxDemo.Name = "tdCheckBoxDemo";
			this.tdCheckBoxDemo.Size = new System.Drawing.Size(180, 16);
			this.tdCheckBoxDemo.TabIndex = 2;
			this.tdCheckBoxDemo.Tag = "-1";
			this.tdCheckBoxDemo.Text = "15通道染色摇头灯：6.总调光";
			this.tdCheckBoxDemo.UseVisualStyleBackColor = true;
			this.tdCheckBoxDemo.CheckedChanged += new System.EventHandler(this.tdCheckBox_CheckedChanged);
			// 
			// saComboBoxDemo
			// 
			this.saComboBoxDemo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.saComboBoxDemo.FormattingEnabled = true;
			this.saComboBoxDemo.Location = new System.Drawing.Point(11, 25);
			this.saComboBoxDemo.Name = "saComboBoxDemo";
			this.saComboBoxDemo.Size = new System.Drawing.Size(161, 20);
			this.saComboBoxDemo.TabIndex = 1;
			// 
			// unifyPanel
			// 
			this.unifyPanel.Controls.Add(this.nextPageButton);
			this.unifyPanel.Controls.Add(this.testButton);
			this.unifyPanel.Controls.Add(this.backPageButton);
			this.unifyPanel.Controls.Add(this.stepShowFLP);
			this.unifyPanel.Controls.Add(this.unifySmallPanel);
			this.unifyPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.unifyPanel.Location = new System.Drawing.Point(0, 0);
			this.unifyPanel.Name = "unifyPanel";
			this.unifyPanel.Size = new System.Drawing.Size(1134, 79);
			this.unifyPanel.TabIndex = 1;
			// 
			// nextPageButton
			// 
			this.nextPageButton.Location = new System.Drawing.Point(1077, 25);
			this.nextPageButton.Name = "nextPageButton";
			this.nextPageButton.Size = new System.Drawing.Size(51, 23);
			this.nextPageButton.TabIndex = 2;
			this.nextPageButton.Text = "下页";
			this.nextPageButton.UseVisualStyleBackColor = true;
			this.nextPageButton.Click += new System.EventHandler(this.pageButton_Click);
			// 
			// testButton
			// 
			this.testButton.Location = new System.Drawing.Point(1077, 49);
			this.testButton.Name = "testButton";
			this.testButton.Size = new System.Drawing.Size(51, 27);
			this.testButton.TabIndex = 2;
			this.testButton.Text = "Test";
			this.testButton.UseVisualStyleBackColor = true;
			this.testButton.Click += new System.EventHandler(this.testButton_Click);
			// 
			// backPageButton
			// 
			this.backPageButton.Location = new System.Drawing.Point(1077, 2);
			this.backPageButton.Name = "backPageButton";
			this.backPageButton.Size = new System.Drawing.Size(51, 23);
			this.backPageButton.TabIndex = 2;
			this.backPageButton.Text = "上页";
			this.backPageButton.UseVisualStyleBackColor = true;
			this.backPageButton.Click += new System.EventHandler(this.pageButton_Click);
			// 
			// stepShowFLP
			// 
			this.stepShowFLP.BackColor = System.Drawing.Color.Silver;
			this.stepShowFLP.Controls.Add(this.stepShowPanelDemo);
			this.stepShowFLP.Dock = System.Windows.Forms.DockStyle.Left;
			this.stepShowFLP.Location = new System.Drawing.Point(187, 0);
			this.stepShowFLP.Margin = new System.Windows.Forms.Padding(1);
			this.stepShowFLP.Name = "stepShowFLP";
			this.stepShowFLP.Size = new System.Drawing.Size(884, 79);
			this.stepShowFLP.TabIndex = 1;
			this.stepShowFLP.Tag = "";
			// 
			// stepShowPanelDemo
			// 
			this.stepShowPanelDemo.Controls.Add(this.stepCheckBoxDemo);
			this.stepShowPanelDemo.Location = new System.Drawing.Point(1, 1);
			this.stepShowPanelDemo.Margin = new System.Windows.Forms.Padding(1);
			this.stepShowPanelDemo.Name = "stepShowPanelDemo";
			this.stepShowPanelDemo.Size = new System.Drawing.Size(42, 49);
			this.stepShowPanelDemo.TabIndex = 1;
			this.stepShowPanelDemo.Visible = false;
			// 
			// stepCheckBoxDemo
			// 
			this.stepCheckBoxDemo.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.stepCheckBoxDemo.ForeColor = System.Drawing.Color.Black;
			this.stepCheckBoxDemo.Location = new System.Drawing.Point(6, 5);
			this.stepCheckBoxDemo.Name = "stepCheckBoxDemo";
			this.stepCheckBoxDemo.Size = new System.Drawing.Size(30, 37);
			this.stepCheckBoxDemo.TabIndex = 0;
			this.stepCheckBoxDemo.Text = "1";
			this.stepCheckBoxDemo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.stepCheckBoxDemo.UseVisualStyleBackColor = true;
			this.stepCheckBoxDemo.CheckedChanged += new System.EventHandler(this.StepCheckBox_CheckedChanged);
			// 
			// unifySmallPanel
			// 
			this.unifySmallPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.unifySmallPanel.Controls.Add(this.tdComboBox);
			this.unifySmallPanel.Controls.Add(this.useTdCheckBox);
			this.unifySmallPanel.Controls.Add(this.groupComboBox);
			this.unifySmallPanel.Controls.Add(this.unifyComboBox);
			this.unifySmallPanel.Controls.Add(this.label2);
			this.unifySmallPanel.Controls.Add(this.label1);
			this.unifySmallPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.unifySmallPanel.Location = new System.Drawing.Point(0, 0);
			this.unifySmallPanel.Name = "unifySmallPanel";
			this.unifySmallPanel.Size = new System.Drawing.Size(187, 79);
			this.unifySmallPanel.TabIndex = 0;
			// 
			// tdComboBox
			// 
			this.tdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tdComboBox.FormattingEnabled = true;
			this.tdComboBox.Items.AddRange(new object[] {
            "请选择通道"});
			this.tdComboBox.Location = new System.Drawing.Point(94, 51);
			this.tdComboBox.Name = "tdComboBox";
			this.tdComboBox.Size = new System.Drawing.Size(80, 20);
			this.tdComboBox.TabIndex = 1;
			this.tdComboBox.SelectedIndexChanged += new System.EventHandler(this.tdComboBox_SelectedIndexChanged);
			// 
			// useTdCheckBox
			// 
			this.useTdCheckBox.AutoSize = true;
			this.useTdCheckBox.ForeColor = System.Drawing.SystemColors.Window;
			this.useTdCheckBox.Location = new System.Drawing.Point(6, 55);
			this.useTdCheckBox.Name = "useTdCheckBox";
			this.useTdCheckBox.Size = new System.Drawing.Size(84, 16);
			this.useTdCheckBox.TabIndex = 2;
			this.useTdCheckBox.Text = "按通道通道";
			this.myToolTip.SetToolTip(this.useTdCheckBox, "勾选此选项后，所有的调整将会应用于编组内所有灯具的选定通道；\r\n若不勾选，则会根据每个通道之前的勾选框进行统一调整。");
			this.useTdCheckBox.UseVisualStyleBackColor = true;
			// 
			// groupComboBox
			// 
			this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.groupComboBox.FormattingEnabled = true;
			this.groupComboBox.Items.AddRange(new object[] {
            "请选择编组"});
			this.groupComboBox.Location = new System.Drawing.Point(94, 28);
			this.groupComboBox.Name = "groupComboBox";
			this.groupComboBox.Size = new System.Drawing.Size(80, 20);
			this.groupComboBox.TabIndex = 1;
			this.groupComboBox.SelectedIndexChanged += new System.EventHandler(this.groupComboBox_SelectedIndexChanged);
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
			this.unifyComboBox.Location = new System.Drawing.Point(94, 5);
			this.unifyComboBox.Name = "unifyComboBox";
			this.unifyComboBox.Size = new System.Drawing.Size(80, 20);
			this.unifyComboBox.TabIndex = 1;
			this.unifyComboBox.SelectedIndexChanged += new System.EventHandler(this.unifyComboBox_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.SystemColors.Window;
			this.label2.Location = new System.Drawing.Point(6, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "灯具编组：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.SystemColors.Window;
			this.label1.Location = new System.Drawing.Point(6, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "快速选步：";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 739);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1134, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(1119, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DetailMultiPageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(1134, 761);
			this.Controls.Add(this.bigFLP);
			this.Controls.Add(this.unifyPanel);
			this.Controls.Add(this.statusStrip1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1150, 800);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(1150, 800);
			this.Name = "DetailMultiPageForm";
			this.Text = "多通道多步联调（分页版）";
			this.Load += new System.EventHandler(this.DetailMultiPageForm_Load);
			this.bigFLP.ResumeLayout(false);
			this.tdPanelDemo.ResumeLayout(false);
			this.stepFLPDemo.ResumeLayout(false);
			this.stepPanelDemo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.stepNUDDemo)).EndInit();
			this.tdSmallPanelDemo.ResumeLayout(false);
			this.tdSmallPanelDemo.PerformLayout();
			this.unifyPanel.ResumeLayout(false);
			this.stepShowFLP.ResumeLayout(false);
			this.stepShowPanelDemo.ResumeLayout(false);
			this.unifySmallPanel.ResumeLayout(false);
			this.unifySmallPanel.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel bigFLP;
		private System.Windows.Forms.Panel tdPanelDemo;
		private System.Windows.Forms.FlowLayoutPanel stepFLPDemo;
		private System.Windows.Forms.Panel tdSmallPanelDemo;
		private System.Windows.Forms.CheckBox tdCheckBoxDemo;
		private System.Windows.Forms.ComboBox saComboBoxDemo;
		private System.Windows.Forms.Panel unifyPanel;
		private System.Windows.Forms.FlowLayoutPanel stepShowFLP;
		private System.Windows.Forms.Panel unifySmallPanel;
		private System.Windows.Forms.Panel stepShowPanelDemo;
		private System.Windows.Forms.CheckBox stepCheckBoxDemo;
		private System.Windows.Forms.ComboBox groupComboBox;
		private System.Windows.Forms.ComboBox unifyComboBox;
		private System.Windows.Forms.Button backPageButton;
		private System.Windows.Forms.Button nextPageButton;
		private System.Windows.Forms.Panel stepPanelDemo;
		private System.Windows.Forms.NumericUpDown stepNUDDemo;
		private System.Windows.Forms.Button topBottomButtonDemo;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.Button testButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox tdComboBox;
		private System.Windows.Forms.CheckBox useTdCheckBox;
		private System.Windows.Forms.ToolTip myToolTip;
	}
}