namespace RecordTools
{
	partial class RecordSetForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordSetForm));
			this.nextButton = new System.Windows.Forms.Button();
			this.previousButton = new System.Windows.Forms.Button();
			this.bigFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.saveButton = new System.Windows.Forms.Button();
			this.loadOldButton = new System.Windows.Forms.Button();
			this.pageLabel = new System.Windows.Forms.Label();
			this.myStatusStrip = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.checkBoxDemo = new System.Windows.Forms.CheckBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.myStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// nextButton
			// 
			this.nextButton.Location = new System.Drawing.Point(726, 13);
			this.nextButton.Name = "nextButton";
			this.nextButton.Size = new System.Drawing.Size(61, 23);
			this.nextButton.TabIndex = 44;
			this.nextButton.Text = "下一页";
			this.nextButton.UseVisualStyleBackColor = true;
			this.nextButton.Click += new System.EventHandler(this.pageButton_Click);
			// 
			// previousButton
			// 
			this.previousButton.Location = new System.Drawing.Point(608, 13);
			this.previousButton.Name = "previousButton";
			this.previousButton.Size = new System.Drawing.Size(61, 23);
			this.previousButton.TabIndex = 45;
			this.previousButton.Text = "上一页";
			this.previousButton.UseVisualStyleBackColor = true;
			this.previousButton.Click += new System.EventHandler(this.pageButton_Click);
			// 
			// bigFLP
			// 
			this.bigFLP.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.bigFLP.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bigFLP.Location = new System.Drawing.Point(0, 50);
			this.bigFLP.Name = "bigFLP";
			this.bigFLP.Size = new System.Drawing.Size(800, 314);
			this.bigFLP.TabIndex = 43;
			// 
			// saveButton
			// 
			this.saveButton.BackColor = System.Drawing.Color.MistyRose;
			this.saveButton.Location = new System.Drawing.Point(94, 7);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 37);
			this.saveButton.TabIndex = 41;
			this.saveButton.Text = "保存\r\n配置文件";
			this.saveButton.UseVisualStyleBackColor = false;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// loadOldButton
			// 
			this.loadOldButton.BackColor = System.Drawing.Color.PaleTurquoise;
			this.loadOldButton.Location = new System.Drawing.Point(13, 7);
			this.loadOldButton.Name = "loadOldButton";
			this.loadOldButton.Size = new System.Drawing.Size(75, 37);
			this.loadOldButton.TabIndex = 42;
			this.loadOldButton.Text = "打开\r\n配置文件";
			this.loadOldButton.UseVisualStyleBackColor = false;
			this.loadOldButton.Click += new System.EventHandler(this.loadButton_Click);
			// 
			// pageLabel
			// 
			this.pageLabel.AutoSize = true;
			this.pageLabel.BackColor = System.Drawing.Color.Transparent;
			this.pageLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.pageLabel.Location = new System.Drawing.Point(686, 18);
			this.pageLabel.Name = "pageLabel";
			this.pageLabel.Size = new System.Drawing.Size(23, 12);
			this.pageLabel.TabIndex = 46;
			this.pageLabel.Text = "1/6";
			// 
			// myStatusStrip
			// 
			this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.myStatusStrip.Location = new System.Drawing.Point(0, 364);
			this.myStatusStrip.Name = "myStatusStrip";
			this.myStatusStrip.Size = new System.Drawing.Size(800, 22);
			this.myStatusStrip.SizingGrip = false;
			this.myStatusStrip.TabIndex = 47;
			this.myStatusStrip.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(0, 17);
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkBoxDemo
			// 
			this.checkBoxDemo.Location = new System.Drawing.Point(527, 13);
			this.checkBoxDemo.Name = "checkBoxDemo";
			this.checkBoxDemo.Size = new System.Drawing.Size(72, 24);
			this.checkBoxDemo.TabIndex = 39;
			this.checkBoxDemo.Text = "通道512";
			this.checkBoxDemo.UseVisualStyleBackColor = true;
			this.checkBoxDemo.Visible = false;
			// 
			// RecordSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.ClientSize = new System.Drawing.Size(800, 386);
			this.Controls.Add(this.checkBoxDemo);
			this.Controls.Add(this.nextButton);
			this.Controls.Add(this.previousButton);
			this.Controls.Add(this.bigFLP);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.loadOldButton);
			this.Controls.Add(this.pageLabel);
			this.Controls.Add(this.myStatusStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RecordSetForm";
			this.Text = "录播文件·音频通道选择器";
			this.myStatusStrip.ResumeLayout(false);
			this.myStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button nextButton;
		private System.Windows.Forms.Button previousButton;
		private System.Windows.Forms.FlowLayoutPanel bigFLP;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button loadOldButton;
		private System.Windows.Forms.Label pageLabel;
		private System.Windows.Forms.StatusStrip myStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.CheckBox checkBoxDemo;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
	}
}

