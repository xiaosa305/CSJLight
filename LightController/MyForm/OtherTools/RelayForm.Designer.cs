namespace LightController.MyForm.OtherTools
{
	partial class RelayForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RelayForm));
			this.openButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lightImageList = new System.Windows.Forms.ImageList(this.components);
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.readButton = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.relayFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.relayPanelDemo = new System.Windows.Forms.Panel();
			this.relayButtonDemo = new CCWin.SkinControl.SkinButton();
			this.relayTBDemo = new System.Windows.Forms.TextBox();
			this.labelPanel = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.timePanelDemo = new System.Windows.Forms.Panel();
			this.timeNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.noticeStatusStrip = new System.Windows.Forms.StatusStrip();
			this.noticeLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.relayFLP.SuspendLayout();
			this.relayPanelDemo.SuspendLayout();
			this.labelPanel.SuspendLayout();
			this.timePanelDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.timeNUDDemo)).BeginInit();
			this.noticeStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// openButton
			// 
			this.openButton.Location = new System.Drawing.Point(38, 26);
			this.openButton.Name = "openButton";
			this.openButton.Size = new System.Drawing.Size(59, 75);
			this.openButton.TabIndex = 0;
			this.openButton.Text = "开台";
			this.openButton.UseVisualStyleBackColor = true;
			this.openButton.Click += new System.EventHandler(this.openButton_Click);
			// 
			// closeButton
			// 
			this.closeButton.Location = new System.Drawing.Point(608, 26);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(59, 75);
			this.closeButton.TabIndex = 0;
			this.closeButton.Text = "关台";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(187, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(329, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "----------------------------------------------------->\r\n<------------------------" +
    "-----------------------------";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(303, 76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89, 12);
			this.label3.TabIndex = 1;
			this.label3.Text = "继电器关闭顺序";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(310, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(89, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "继电器开启顺序";
			// 
			// lightImageList
			// 
			this.lightImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lightImageList.ImageStream")));
			this.lightImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.lightImageList.Images.SetKeyName(0, "Ok3w.Net图标1.png");
			this.lightImageList.Images.SetKeyName(1, "Ok3w.Net图标15.png");
			this.lightImageList.Images.SetKeyName(2, "墙板按钮.png");
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(38, 370);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 48);
			this.button3.TabIndex = 3;
			this.button3.Text = "打开配置";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(153, 370);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 48);
			this.button4.TabIndex = 3;
			this.button4.Text = "保存配置";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// readButton
			// 
			this.readButton.Location = new System.Drawing.Point(480, 370);
			this.readButton.Name = "readButton";
			this.readButton.Size = new System.Drawing.Size(75, 48);
			this.readButton.TabIndex = 3;
			this.readButton.Text = "回读配置";
			this.readButton.UseVisualStyleBackColor = true;
			this.readButton.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.button6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.button6.Location = new System.Drawing.Point(592, 370);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(75, 48);
			this.button6.TabIndex = 3;
			this.button6.Text = "下载配置";
			this.button6.UseVisualStyleBackColor = false;
			// 
			// relayFLP
			// 
			this.relayFLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.relayFLP.Controls.Add(this.relayPanelDemo);
			this.relayFLP.Controls.Add(this.labelPanel);
			this.relayFLP.Controls.Add(this.timePanelDemo);
			this.relayFLP.Location = new System.Drawing.Point(38, 128);
			this.relayFLP.Name = "relayFLP";
			this.relayFLP.Size = new System.Drawing.Size(629, 193);
			this.relayFLP.TabIndex = 4;
			// 
			// relayPanelDemo
			// 
			this.relayPanelDemo.Controls.Add(this.relayButtonDemo);
			this.relayPanelDemo.Controls.Add(this.relayTBDemo);
			this.relayPanelDemo.Location = new System.Drawing.Point(3, 3);
			this.relayPanelDemo.Name = "relayPanelDemo";
			this.relayPanelDemo.Size = new System.Drawing.Size(83, 144);
			this.relayPanelDemo.TabIndex = 0;
			this.relayPanelDemo.Visible = false;
			// 
			// relayButtonDemo
			// 
			this.relayButtonDemo.BackColor = System.Drawing.Color.Transparent;
			this.relayButtonDemo.BaseColor = System.Drawing.Color.Transparent;
			this.relayButtonDemo.BorderColor = System.Drawing.Color.White;
			this.relayButtonDemo.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.relayButtonDemo.DownBack = null;
			this.relayButtonDemo.DrawType = CCWin.SkinControl.DrawStyle.Img;
			this.relayButtonDemo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.relayButtonDemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
			this.relayButtonDemo.ForeColorSuit = true;
			this.relayButtonDemo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.relayButtonDemo.ImageKey = "Ok3w.Net图标1.png";
			this.relayButtonDemo.ImageList = this.lightImageList;
			this.relayButtonDemo.ImageSize = new System.Drawing.Size(45, 45);
			this.relayButtonDemo.InheritColor = true;
			this.relayButtonDemo.IsDrawBorder = false;
			this.relayButtonDemo.Location = new System.Drawing.Point(7, 13);
			this.relayButtonDemo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 10);
			this.relayButtonDemo.MouseBack = null;
			this.relayButtonDemo.Name = "relayButtonDemo";
			this.relayButtonDemo.NormlBack = null;
			this.relayButtonDemo.Size = new System.Drawing.Size(63, 85);
			this.relayButtonDemo.TabIndex = 16;
			this.relayButtonDemo.Tag = "9999";
			this.relayButtonDemo.Text = "开关1";
			this.relayButtonDemo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.relayButtonDemo.UseVisualStyleBackColor = true;
			this.relayButtonDemo.Visible = false;
			// 
			// relayTBDemo
			// 
			this.relayTBDemo.Location = new System.Drawing.Point(11, 111);
			this.relayTBDemo.Name = "relayTBDemo";
			this.relayTBDemo.Size = new System.Drawing.Size(54, 21);
			this.relayTBDemo.TabIndex = 15;
			this.relayTBDemo.Text = "音响";
			this.relayTBDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelPanel
			// 
			this.labelPanel.Controls.Add(this.label5);
			this.labelPanel.Location = new System.Drawing.Point(92, 3);
			this.labelPanel.Name = "labelPanel";
			this.labelPanel.Size = new System.Drawing.Size(59, 30);
			this.labelPanel.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(11, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 12);
			this.label5.TabIndex = 0;
			this.label5.Text = "时延(S)";
			// 
			// timePanelDemo
			// 
			this.timePanelDemo.Controls.Add(this.timeNUDDemo);
			this.timePanelDemo.Location = new System.Drawing.Point(157, 3);
			this.timePanelDemo.Name = "timePanelDemo";
			this.timePanelDemo.Size = new System.Drawing.Size(83, 30);
			this.timePanelDemo.TabIndex = 2;
			this.timePanelDemo.Visible = false;
			// 
			// timeNUDDemo
			// 
			this.timeNUDDemo.Location = new System.Drawing.Point(4, 4);
			this.timeNUDDemo.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
			this.timeNUDDemo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.timeNUDDemo.Name = "timeNUDDemo";
			this.timeNUDDemo.Size = new System.Drawing.Size(44, 21);
			this.timeNUDDemo.TabIndex = 2;
			this.timeNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.timeNUDDemo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// noticeStatusStrip
			// 
			this.noticeStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noticeLabel});
			this.noticeStatusStrip.Location = new System.Drawing.Point(0, 459);
			this.noticeStatusStrip.Name = "noticeStatusStrip";
			this.noticeStatusStrip.Size = new System.Drawing.Size(704, 22);
			this.noticeStatusStrip.SizingGrip = false;
			this.noticeStatusStrip.TabIndex = 28;
			// 
			// noticeLabel
			// 
			this.noticeLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.noticeLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.noticeLabel.ForeColor = System.Drawing.Color.DimGray;
			this.noticeLabel.Name = "noticeLabel";
			this.noticeLabel.Size = new System.Drawing.Size(689, 17);
			this.noticeLabel.Spring = true;
			this.noticeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// RelayForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(192)))), ((int)(((byte)(222)))));
			this.ClientSize = new System.Drawing.Size(704, 481);
			this.Controls.Add(this.noticeStatusStrip);
			this.Controls.Add(this.relayFLP);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.readButton);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.openButton);
			this.Name = "RelayForm";
			this.Text = "时序器设置";
			this.Load += new System.EventHandler(this.RelayForm_Load);
			this.relayFLP.ResumeLayout(false);
			this.relayPanelDemo.ResumeLayout(false);
			this.relayPanelDemo.PerformLayout();
			this.labelPanel.ResumeLayout(false);
			this.labelPanel.PerformLayout();
			this.timePanelDemo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.timeNUDDemo)).EndInit();
			this.noticeStatusStrip.ResumeLayout(false);
			this.noticeStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button openButton;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button readButton;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.ImageList lightImageList;
		private System.Windows.Forms.FlowLayoutPanel relayFLP;
		private System.Windows.Forms.Panel relayPanelDemo;
		private CCWin.SkinControl.SkinButton relayButtonDemo;
		private System.Windows.Forms.TextBox relayTBDemo;
		private System.Windows.Forms.Panel labelPanel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel timePanelDemo;
		private System.Windows.Forms.NumericUpDown timeNUDDemo;
		private System.Windows.Forms.StatusStrip noticeStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel noticeLabel;
	}
}