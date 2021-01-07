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
			this.components = new System.ComponentModel.Container();
			this.astPanel = new System.Windows.Forms.Panel();
			this.astLabel = new System.Windows.Forms.Label();
			this.deleteButton = new System.Windows.Forms.Button();
			this.colorPanelDemo = new System.Windows.Forms.Panel();
			this.cmCBDemo = new System.Windows.Forms.CheckBox();
			this.stNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.tgNUD = new System.Windows.Forms.NumericUpDown();
			this.label21 = new System.Windows.Forms.Label();
			this.tgTrackBar = new System.Windows.Forms.TrackBar();
			this.colorFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.stLabel = new System.Windows.Forms.Label();
			this.editButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.myColorDialog = new System.Windows.Forms.ColorDialog();
			this.cancelButton = new System.Windows.Forms.Button();
			this.enterButton = new System.Windows.Forms.Button();
			this.previewButton = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.clearButton = new System.Windows.Forms.Button();
			this.shieldCheckBox = new System.Windows.Forms.CheckBox();
			this.modeLabel = new System.Windows.Forms.Label();
			this.colorPanelDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.stNUDDemo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tgNUD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tgTrackBar)).BeginInit();
			this.colorFLP.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// astPanel
			// 
			this.astPanel.Location = new System.Drawing.Point(7, 122);
			this.astPanel.Name = "astPanel";
			this.astPanel.Size = new System.Drawing.Size(68, 24);
			this.astPanel.TabIndex = 73;
			// 
			// astLabel
			// 
			this.astLabel.AutoSize = true;
			this.astLabel.Location = new System.Drawing.Point(7, 95);
			this.astLabel.Name = "astLabel";
			this.astLabel.Size = new System.Drawing.Size(53, 12);
			this.astLabel.TabIndex = 0;
			this.astLabel.Text = "未选中步";
			this.astLabel.TextChanged += new System.EventHandler(this.someControl_TextChanged);
			// 
			// deleteButton
			// 
			this.deleteButton.Enabled = false;
			this.deleteButton.Location = new System.Drawing.Point(126, 22);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(50, 45);
			this.deleteButton.TabIndex = 72;
			this.deleteButton.Text = "删除";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// colorPanelDemo
			// 
			this.colorPanelDemo.Controls.Add(this.cmCBDemo);
			this.colorPanelDemo.Controls.Add(this.stNUDDemo);
			this.colorPanelDemo.Location = new System.Drawing.Point(1, 1);
			this.colorPanelDemo.Margin = new System.Windows.Forms.Padding(1);
			this.colorPanelDemo.Name = "colorPanelDemo";
			this.colorPanelDemo.Size = new System.Drawing.Size(58, 138);
			this.colorPanelDemo.TabIndex = 68;
			this.colorPanelDemo.Visible = false;
			this.colorPanelDemo.Click += new System.EventHandler(this.colorPanel_Click);
			// 
			// cmCBDemo
			// 
			this.cmCBDemo.AutoSize = true;
			this.cmCBDemo.BackColor = System.Drawing.Color.MintCream;
			this.cmCBDemo.Location = new System.Drawing.Point(21, 119);
			this.cmCBDemo.Name = "cmCBDemo";
			this.cmCBDemo.Size = new System.Drawing.Size(15, 14);
			this.cmCBDemo.TabIndex = 1;
			this.cmCBDemo.UseVisualStyleBackColor = false;
			this.cmCBDemo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmCheckBox_KeyPress);
			// 
			// stNUDDemo
			// 
			this.stNUDDemo.DecimalPlaces = 2;
			this.stNUDDemo.Location = new System.Drawing.Point(4, 92);
			this.stNUDDemo.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
			this.stNUDDemo.Name = "stNUDDemo";
			this.stNUDDemo.Size = new System.Drawing.Size(50, 21);
			this.stNUDDemo.TabIndex = 0;
			this.stNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.stNUDDemo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.stNUD_KeyPress);
			// 
			// tgNUD
			// 
			this.tgNUD.Location = new System.Drawing.Point(312, 46);
			this.tgNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.tgNUD.Name = "tgNUD";
			this.tgNUD.Size = new System.Drawing.Size(57, 21);
			this.tgNUD.TabIndex = 70;
			this.tgNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.tgNUD.ValueChanged += new System.EventHandler(this.tgNUD_ValueChanged);
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
			// tgTrackBar
			// 
			this.tgTrackBar.BackColor = System.Drawing.Color.White;
			this.tgTrackBar.Location = new System.Drawing.Point(242, 22);
			this.tgTrackBar.Maximum = 255;
			this.tgTrackBar.Name = "tgTrackBar";
			this.tgTrackBar.Size = new System.Drawing.Size(145, 45);
			this.tgTrackBar.TabIndex = 69;
			this.tgTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
			this.tgTrackBar.ValueChanged += new System.EventHandler(this.tgTrackBar_ValueChanged);
			// 
			// colorFLP
			// 
			this.colorFLP.AutoScroll = true;
			this.colorFLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.colorFLP.Controls.Add(this.colorPanelDemo);
			this.colorFLP.Location = new System.Drawing.Point(81, 79);
			this.colorFLP.Name = "colorFLP";
			this.colorFLP.Size = new System.Drawing.Size(306, 168);
			this.colorFLP.TabIndex = 65;
			this.colorFLP.WrapContents = false;
			// 
			// stLabel
			// 
			this.stLabel.AutoSize = true;
			this.stLabel.Location = new System.Drawing.Point(7, 176);
			this.stLabel.Name = "stLabel";
			this.stLabel.Size = new System.Drawing.Size(65, 12);
			this.stLabel.TabIndex = 66;
			this.stLabel.Text = "步时间(S):";
			this.stLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// editButton
			// 
			this.editButton.Enabled = false;
			this.editButton.Location = new System.Drawing.Point(70, 22);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(50, 45);
			this.editButton.TabIndex = 63;
			this.editButton.Text = "修改";
			this.editButton.UseVisualStyleBackColor = true;
			this.editButton.Click += new System.EventHandler(this.editButton_Click);
			// 
			// addButton
			// 
			this.addButton.Location = new System.Drawing.Point(7, 22);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(50, 45);
			this.addButton.TabIndex = 64;
			this.addButton.Text = "添加";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// myColorDialog
			// 
			this.myColorDialog.AnyColor = true;
			this.myColorDialog.FullOpen = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(312, 272);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 37);
			this.cancelButton.TabIndex = 74;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(233, 272);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(75, 37);
			this.enterButton.TabIndex = 75;
			this.enterButton.Text = "应用颜色";
			this.myToolTip.SetToolTip(this.enterButton, "左键为插入模式；\r\n中键为追加模式；\r\n右键为覆盖模式；");
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			this.enterButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.enterButton_MouseDown);
			// 
			// previewButton
			// 
			this.previewButton.BackColor = System.Drawing.Color.SandyBrown;
			this.previewButton.Enabled = false;
			this.previewButton.Location = new System.Drawing.Point(14, 270);
			this.previewButton.Name = "previewButton";
			this.previewButton.Size = new System.Drawing.Size(75, 37);
			this.previewButton.TabIndex = 76;
			this.previewButton.Text = "预览";
			this.previewButton.UseVisualStyleBackColor = false;
			this.previewButton.TextChanged += new System.EventHandler(this.someControl_TextChanged);
			this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 325);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(407, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 77;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.BackColor = System.Drawing.Color.Transparent;
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(392, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// clearButton
			// 
			this.clearButton.Enabled = false;
			this.clearButton.Location = new System.Drawing.Point(182, 22);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(50, 45);
			this.clearButton.TabIndex = 72;
			this.clearButton.Text = "清空";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
			// 
			// shieldCheckBox
			// 
			this.shieldCheckBox.AutoSize = true;
			this.shieldCheckBox.Checked = true;
			this.shieldCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.shieldCheckBox.Location = new System.Drawing.Point(118, 276);
			this.shieldCheckBox.Name = "shieldCheckBox";
			this.shieldCheckBox.Size = new System.Drawing.Size(96, 28);
			this.shieldCheckBox.TabIndex = 78;
			this.shieldCheckBox.Text = "屏蔽其它步数\r\n相关通道";
			this.shieldCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.shieldCheckBox.UseVisualStyleBackColor = true;
			this.shieldCheckBox.Visible = false;
			// 
			// modeLabel
			// 
			this.modeLabel.AutoSize = true;
			this.modeLabel.Location = new System.Drawing.Point(7, 201);
			this.modeLabel.Name = "modeLabel";
			this.modeLabel.Size = new System.Drawing.Size(65, 12);
			this.modeLabel.TabIndex = 67;
			this.modeLabel.Text = "是否渐变：";
			this.modeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ColorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.MintCream;
			this.ClientSize = new System.Drawing.Size(407, 347);
			this.Controls.Add(this.shieldCheckBox);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.astLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.previewButton);
			this.Controls.Add(this.astPanel);
			this.Controls.Add(this.clearButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.tgNUD);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.tgTrackBar);
			this.Controls.Add(this.modeLabel);
			this.Controls.Add(this.colorFLP);
			this.Controls.Add(this.stLabel);
			this.Controls.Add(this.editButton);
			this.Controls.Add(this.addButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ColorForm";
			this.Text = "快速调色";
			this.Activated += new System.EventHandler(this.ColorForm_Activated);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ColorForm_FormClosed);
			this.Load += new System.EventHandler(this.ColorForm_Load);
			this.colorPanelDemo.ResumeLayout(false);
			this.colorPanelDemo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.stNUDDemo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tgNUD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tgTrackBar)).EndInit();
			this.colorFLP.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel astPanel;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Panel colorPanelDemo;
		private System.Windows.Forms.CheckBox cmCBDemo;
		private System.Windows.Forms.NumericUpDown stNUDDemo;
		private System.Windows.Forms.NumericUpDown tgNUD;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.TrackBar tgTrackBar;
		private System.Windows.Forms.FlowLayoutPanel colorFLP;
		private System.Windows.Forms.Label stLabel;
		private System.Windows.Forms.Button editButton;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.ColorDialog myColorDialog;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button previewButton;
		private System.Windows.Forms.Label astLabel;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.ToolTip myToolTip;
		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.CheckBox shieldCheckBox;
		private System.Windows.Forms.Label modeLabel;
	}
}