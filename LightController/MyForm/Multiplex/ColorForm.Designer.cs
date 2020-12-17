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
			this.label20 = new System.Windows.Forms.Label();
			this.colorFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.label19 = new System.Windows.Forms.Label();
			this.editButton = new System.Windows.Forms.Button();
			this.addButton = new System.Windows.Forms.Button();
			this.myColorDialog = new System.Windows.Forms.ColorDialog();
			this.cancelButton = new System.Windows.Forms.Button();
			this.enterButton = new System.Windows.Forms.Button();
			this.previewButton = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
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
			this.astPanel.Location = new System.Drawing.Point(14, 122);
			this.astPanel.Name = "astPanel";
			this.astPanel.Size = new System.Drawing.Size(59, 24);
			this.astPanel.TabIndex = 73;
			// 
			// astLabel
			// 
			this.astLabel.AutoSize = true;
			this.astLabel.Location = new System.Drawing.Point(14, 95);
			this.astLabel.Name = "astLabel";
			this.astLabel.Size = new System.Drawing.Size(53, 12);
			this.astLabel.TabIndex = 0;
			this.astLabel.Text = "未选中步";
			// 
			// deleteButton
			// 
			this.deleteButton.Enabled = false;
			this.deleteButton.Location = new System.Drawing.Point(125, 22);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(38, 45);
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
			this.cmCBDemo.Location = new System.Drawing.Point(4, 119);
			this.cmCBDemo.Name = "cmCBDemo";
			this.cmCBDemo.Size = new System.Drawing.Size(48, 16);
			this.cmCBDemo.TabIndex = 1;
			this.cmCBDemo.Text = "渐变";
			this.cmCBDemo.UseVisualStyleBackColor = false;
			// 
			// stNUDDemo
			// 
			this.stNUDDemo.DecimalPlaces = 2;
			this.stNUDDemo.Location = new System.Drawing.Point(4, 92);
			this.stNUDDemo.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.stNUDDemo.Name = "stNUDDemo";
			this.stNUDDemo.Size = new System.Drawing.Size(50, 21);
			this.stNUDDemo.TabIndex = 0;
			this.stNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(14, 199);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(59, 12);
			this.label20.TabIndex = 67;
			this.label20.Text = "是否渐变:";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(14, 176);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(47, 12);
			this.label19.TabIndex = 66;
			this.label19.Text = "步时间:";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// editButton
			// 
			this.editButton.Enabled = false;
			this.editButton.Location = new System.Drawing.Point(81, 22);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(38, 45);
			this.editButton.TabIndex = 63;
			this.editButton.Text = "修改";
			this.editButton.UseVisualStyleBackColor = true;
			this.editButton.Click += new System.EventHandler(this.editButton_Click);
			// 
			// addButton
			// 
			this.addButton.Location = new System.Drawing.Point(14, 22);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(38, 45);
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
			this.cancelButton.Location = new System.Drawing.Point(313, 270);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 37);
			this.cancelButton.TabIndex = 74;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(233, 270);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(75, 37);
			this.enterButton.TabIndex = 75;
			this.enterButton.Text = "应用\r\n颜色变化";
			this.enterButton.UseVisualStyleBackColor = true;
			// 
			// previewButton
			// 
			this.previewButton.BackColor = System.Drawing.Color.SandyBrown;
			this.previewButton.Location = new System.Drawing.Point(14, 270);
			this.previewButton.Name = "previewButton";
			this.previewButton.Size = new System.Drawing.Size(75, 37);
			this.previewButton.TabIndex = 76;
			this.previewButton.Text = "预览";
			this.myToolTip.SetToolTip(this.previewButton, "左键点击为全部预览；\r\n右键点击为单色预览。");
			this.previewButton.UseVisualStyleBackColor = false;
			this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
			this.previewButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.previewButton_MouseDown);
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
			// ColorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.MintCream;
			this.ClientSize = new System.Drawing.Size(407, 347);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.astLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.previewButton);
			this.Controls.Add(this.astPanel);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.tgNUD);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.tgTrackBar);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.colorFLP);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.editButton);
			this.Controls.Add(this.addButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ColorForm";
			this.Text = "快速调色";
			this.Activated += new System.EventHandler(this.ColorForm_Activated);
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
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.FlowLayoutPanel colorFLP;
		private System.Windows.Forms.Label label19;
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
	}
}