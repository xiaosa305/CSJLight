using LightController.Common;

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
			this.bigFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.tdPanelDemo = new System.Windows.Forms.Panel();
			this.stepFLPDemo = new System.Windows.Forms.FlowLayoutPanel();
			this.stepPanelDemo = new System.Windows.Forms.Panel();
			this.topBottomButtonDemo = new System.Windows.Forms.Button();
			this.stepNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.tdSmallPanelDemo = new System.Windows.Forms.Panel();
			this.tdCheckBoxDemo = new System.Windows.Forms.CheckBox();
			this.saComboBoxDemo = new System.Windows.Forms.ComboBox();
			this.unifyPanel = new System.Windows.Forms.Panel();
			this.nextPageButton = new System.Windows.Forms.Button();
			this.backPageButton = new System.Windows.Forms.Button();
			this.stepShowFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.stepCheckBoxDemo = new System.Windows.Forms.CheckBox();
			this.unifySmallPanel = new System.Windows.Forms.Panel();
			this.groupComboBox = new System.Windows.Forms.ComboBox();
			this.stepComboBox = new System.Windows.Forms.ComboBox();
			this.returnButton = new System.Windows.Forms.Button();
			this.tdComboBox = new System.Windows.Forms.ComboBox();
			this.myStatusStrip = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.bigFLP.SuspendLayout();
			this.tdPanelDemo.SuspendLayout();
			this.stepFLPDemo.SuspendLayout();
			this.stepPanelDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.stepNUDDemo)).BeginInit();
			this.tdSmallPanelDemo.SuspendLayout();
			this.unifyPanel.SuspendLayout();
			this.stepShowFLP.SuspendLayout();
			this.unifySmallPanel.SuspendLayout();
			this.myStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// bigFLP
			// 
			this.bigFLP.AutoScroll = true;
			this.bigFLP.Controls.Add(this.tdPanelDemo);
			this.bigFLP.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bigFLP.Location = new System.Drawing.Point(0, 57);
			this.bigFLP.Name = "bigFLP";
			this.bigFLP.Size = new System.Drawing.Size(1134, 682);
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
			this.stepPanelDemo.Controls.Add(this.topBottomButtonDemo);
			this.stepPanelDemo.Controls.Add(this.stepNUDDemo);
			this.stepPanelDemo.Location = new System.Drawing.Point(1, 1);
			this.stepPanelDemo.Margin = new System.Windows.Forms.Padding(1);
			this.stepPanelDemo.Name = "stepPanelDemo";
			this.stepPanelDemo.Size = new System.Drawing.Size(42, 49);
			this.stepPanelDemo.TabIndex = 1;
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
			this.unifyPanel.Controls.Add(this.backPageButton);
			this.unifyPanel.Controls.Add(this.stepShowFLP);
			this.unifyPanel.Controls.Add(this.unifySmallPanel);
			this.unifyPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.unifyPanel.Location = new System.Drawing.Point(0, 0);
			this.unifyPanel.Name = "unifyPanel";
			this.unifyPanel.Size = new System.Drawing.Size(1134, 57);
			this.unifyPanel.TabIndex = 1;
			// 
			// nextPageButton
			// 
			this.nextPageButton.Location = new System.Drawing.Point(1077, 29);
			this.nextPageButton.Name = "nextPageButton";
			this.nextPageButton.Size = new System.Drawing.Size(51, 23);
			this.nextPageButton.TabIndex = 2;
			this.nextPageButton.Text = "下页";
			this.nextPageButton.UseVisualStyleBackColor = true;
			this.nextPageButton.Click += new System.EventHandler(this.pageButton_Click);
			// 
			// backPageButton
			// 
			this.backPageButton.Location = new System.Drawing.Point(1077, 4);
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
			this.stepShowFLP.Controls.Add(this.stepCheckBoxDemo);
			this.stepShowFLP.Dock = System.Windows.Forms.DockStyle.Left;
			this.stepShowFLP.Location = new System.Drawing.Point(187, 0);
			this.stepShowFLP.Margin = new System.Windows.Forms.Padding(1);
			this.stepShowFLP.Name = "stepShowFLP";
			this.stepShowFLP.Size = new System.Drawing.Size(884, 57);
			this.stepShowFLP.TabIndex = 1;
			this.stepShowFLP.Tag = "";
			// 
			// stepCheckBoxDemo
			// 
			this.stepCheckBoxDemo.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.stepCheckBoxDemo.ForeColor = System.Drawing.Color.Black;
			this.stepCheckBoxDemo.Location = new System.Drawing.Point(1, 1);
			this.stepCheckBoxDemo.Margin = new System.Windows.Forms.Padding(1);
			this.stepCheckBoxDemo.Name = "stepCheckBoxDemo";
			this.stepCheckBoxDemo.Size = new System.Drawing.Size(42, 49);
			this.stepCheckBoxDemo.TabIndex = 0;
			this.stepCheckBoxDemo.Text = "1";
			this.stepCheckBoxDemo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.stepCheckBoxDemo.UseVisualStyleBackColor = true;
			this.stepCheckBoxDemo.Visible = false;
			this.stepCheckBoxDemo.CheckedChanged += new System.EventHandler(this.StepCheckBox_CheckedChanged);
			// 
			// unifySmallPanel
			// 
			this.unifySmallPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.unifySmallPanel.Controls.Add(this.groupComboBox);
			this.unifySmallPanel.Controls.Add(this.stepComboBox);
			this.unifySmallPanel.Controls.Add(this.returnButton);
			this.unifySmallPanel.Controls.Add(this.tdComboBox);
			this.unifySmallPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.unifySmallPanel.Location = new System.Drawing.Point(0, 0);
			this.unifySmallPanel.Name = "unifySmallPanel";
			this.unifySmallPanel.Size = new System.Drawing.Size(187, 57);
			this.unifySmallPanel.TabIndex = 0;
			// 
			// groupComboBox
			// 
			this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.groupComboBox.FormattingEnabled = true;
			this.groupComboBox.Items.AddRange(new object[] {
            "请选择编组"});
			this.groupComboBox.Location = new System.Drawing.Point(6, 6);
			this.groupComboBox.Name = "groupComboBox";
			this.groupComboBox.Size = new System.Drawing.Size(102, 20);
			this.groupComboBox.TabIndex = 1;
			this.groupComboBox.SelectedIndexChanged += new System.EventHandler(this.groupComboBox_SelectedIndexChanged);
			// 
			// stepComboBox
			// 
			this.stepComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stepComboBox.FormattingEnabled = true;
			this.stepComboBox.Items.AddRange(new object[] {
            "请选步",
            "全步",
            "奇数步",
            "偶数步"});
			this.stepComboBox.Location = new System.Drawing.Point(124, 33);
			this.stepComboBox.Name = "stepComboBox";
			this.stepComboBox.Size = new System.Drawing.Size(58, 20);
			this.stepComboBox.TabIndex = 1;
			this.stepComboBox.SelectedIndexChanged += new System.EventHandler(this.unifyComboBox_SelectedIndexChanged);
			// 
			// returnButton
			// 
			this.returnButton.Location = new System.Drawing.Point(114, 6);
			this.returnButton.Name = "returnButton";
			this.returnButton.Size = new System.Drawing.Size(68, 21);
			this.returnButton.TabIndex = 2;
			this.returnButton.Text = "重选通道";
			this.returnButton.UseVisualStyleBackColor = true;
			this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
			// 
			// tdComboBox
			// 
			this.tdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tdComboBox.FormattingEnabled = true;
			this.tdComboBox.Items.AddRange(new object[] {
            "请选择通道"});
			this.tdComboBox.Location = new System.Drawing.Point(6, 32);
			this.tdComboBox.Name = "tdComboBox";
			this.tdComboBox.Size = new System.Drawing.Size(112, 20);
			this.tdComboBox.TabIndex = 1;
			this.tdComboBox.SelectedIndexChanged += new System.EventHandler(this.tdComboBox_SelectedIndexChanged);
			// 
			// myStatusStrip
			// 
			this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.myStatusStrip.Location = new System.Drawing.Point(0, 739);
			this.myStatusStrip.Name = "myStatusStrip";
			this.myStatusStrip.Size = new System.Drawing.Size(1134, 22);
			this.myStatusStrip.SizingGrip = false;
			this.myStatusStrip.TabIndex = 1;
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
			this.Controls.Add(this.myStatusStrip);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1150, 800);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(1150, 800);
			this.Name = "DetailMultiPageForm";
			this.Text = "多通道多步联调";
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
			this.unifySmallPanel.ResumeLayout(false);
			this.myStatusStrip.ResumeLayout(false);
			this.myStatusStrip.PerformLayout();
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
		private System.Windows.Forms.CheckBox stepCheckBoxDemo;
		private System.Windows.Forms.ComboBox groupComboBox;
		private System.Windows.Forms.ComboBox stepComboBox;
		private System.Windows.Forms.Button backPageButton;
		private System.Windows.Forms.Button nextPageButton;
		private System.Windows.Forms.Panel stepPanelDemo;
		private System.Windows.Forms.NumericUpDown stepNUDDemo;
		private System.Windows.Forms.Button topBottomButtonDemo;
		private System.Windows.Forms.StatusStrip myStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.Button returnButton;
		private System.Windows.Forms.ComboBox tdComboBox;
	}
}