namespace LightController.MyForm.OtherTools
{
	partial class RecordSetForm
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
			this.myStatusStrip = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.loadOldButton = new System.Windows.Forms.Button();
			this.bigFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.checkBoxDemo = new System.Windows.Forms.CheckBox();
			this.previousButton = new System.Windows.Forms.Button();
			this.nextButton = new System.Windows.Forms.Button();
			this.pageLabel = new System.Windows.Forms.Label();
			this.saveButton = new System.Windows.Forms.Button();
			this.loadNewButton = new System.Windows.Forms.Button();
			this.myStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// myStatusStrip
			// 
			this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.myStatusStrip.Location = new System.Drawing.Point(0, 380);
			this.myStatusStrip.Name = "myStatusStrip";
			this.myStatusStrip.Size = new System.Drawing.Size(784, 22);
			this.myStatusStrip.SizingGrip = false;
			this.myStatusStrip.TabIndex = 34;
			this.myStatusStrip.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(0, 17);
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// loadOldButton
			// 
			this.loadOldButton.Location = new System.Drawing.Point(13, 14);
			this.loadOldButton.Name = "loadOldButton";
			this.loadOldButton.Size = new System.Drawing.Size(75, 37);
			this.loadOldButton.TabIndex = 35;
			this.loadOldButton.Text = "打开旧版\r\n配置文件";
			this.loadOldButton.UseVisualStyleBackColor = true;
			this.loadOldButton.Click += new System.EventHandler(this.loadOldButton_Click);
			// 
			// bigFLP
			// 
			this.bigFLP.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.bigFLP.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bigFLP.Location = new System.Drawing.Point(0, 66);
			this.bigFLP.Name = "bigFLP";
			this.bigFLP.Size = new System.Drawing.Size(784, 314);
			this.bigFLP.TabIndex = 36;
			// 
			// checkBoxDemo
			// 
			this.checkBoxDemo.Location = new System.Drawing.Point(512, 13);
			this.checkBoxDemo.Name = "checkBoxDemo";
			this.checkBoxDemo.Size = new System.Drawing.Size(72, 24);
			this.checkBoxDemo.TabIndex = 0;
			this.checkBoxDemo.Text = "通道512";
			this.checkBoxDemo.UseVisualStyleBackColor = true;
			this.checkBoxDemo.Visible = false;
			this.checkBoxDemo.CheckedChanged += new System.EventHandler(this.tdCheckBox_CheckedChanged);
			// 
			// previousButton
			// 
			this.previousButton.Location = new System.Drawing.Point(593, 13);
			this.previousButton.Name = "previousButton";
			this.previousButton.Size = new System.Drawing.Size(61, 23);
			this.previousButton.TabIndex = 37;
			this.previousButton.Text = "上一页";
			this.previousButton.UseVisualStyleBackColor = true;
			this.previousButton.Click += new System.EventHandler(this.pageButton_Click);
			// 
			// nextButton
			// 
			this.nextButton.Location = new System.Drawing.Point(711, 13);
			this.nextButton.Name = "nextButton";
			this.nextButton.Size = new System.Drawing.Size(61, 23);
			this.nextButton.TabIndex = 37;
			this.nextButton.Text = "下一页";
			this.nextButton.UseVisualStyleBackColor = true;
			this.nextButton.Click += new System.EventHandler(this.pageButton_Click);
			// 
			// pageLabel
			// 
			this.pageLabel.AutoSize = true;
			this.pageLabel.BackColor = System.Drawing.Color.Transparent;
			this.pageLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.pageLabel.Location = new System.Drawing.Point(660, 18);
			this.pageLabel.Name = "pageLabel";
			this.pageLabel.Size = new System.Drawing.Size(47, 12);
			this.pageLabel.TabIndex = 38;
			this.pageLabel.Text = "512/512";
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(215, 14);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 37);
			this.saveButton.TabIndex = 35;
			this.saveButton.Text = "保存\r\n配置文件";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// loadNewButton
			// 
			this.loadNewButton.Location = new System.Drawing.Point(94, 14);
			this.loadNewButton.Name = "loadNewButton";
			this.loadNewButton.Size = new System.Drawing.Size(75, 37);
			this.loadNewButton.TabIndex = 35;
			this.loadNewButton.Text = "打开新版\r\n配置文件";
			this.loadNewButton.UseVisualStyleBackColor = true;
			this.loadNewButton.Click += new System.EventHandler(this.loadNewButton_Click);
			// 
			// RecordSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.ClientSize = new System.Drawing.Size(784, 402);
			this.Controls.Add(this.checkBoxDemo);
			this.Controls.Add(this.nextButton);
			this.Controls.Add(this.previousButton);
			this.Controls.Add(this.bigFLP);
			this.Controls.Add(this.loadNewButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.loadOldButton);
			this.Controls.Add(this.myStatusStrip);
			this.Controls.Add(this.pageLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "RecordSetForm";
			this.Text = "录播文件通道配置";
			this.Load += new System.EventHandler(this.RecordSetForm_Load);
			this.myStatusStrip.ResumeLayout(false);
			this.myStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip myStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.Button loadOldButton;
		private System.Windows.Forms.FlowLayoutPanel bigFLP;
		private System.Windows.Forms.CheckBox checkBoxDemo;
		private System.Windows.Forms.Button previousButton;
		private System.Windows.Forms.Button nextButton;
		private System.Windows.Forms.Label pageLabel;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button loadNewButton;
	}
}