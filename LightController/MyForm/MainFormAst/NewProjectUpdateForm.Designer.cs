namespace LightController.MyForm.MainFormAst
{
	partial class NewProjectUpdateForm
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
			this.dirChooseButton = new System.Windows.Forms.Button();
			this.updateButton = new System.Windows.Forms.Button();
			this.dirClearButton = new System.Windows.Forms.Button();
			this.pathLabel = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.myProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dirChooseButton
			// 
			this.dirChooseButton.Location = new System.Drawing.Point(31, 29);
			this.dirChooseButton.Name = "dirChooseButton";
			this.dirChooseButton.Size = new System.Drawing.Size(87, 33);
			this.dirChooseButton.TabIndex = 19;
			this.dirChooseButton.Text = "选择已有工程";
			this.dirChooseButton.UseVisualStyleBackColor = true;
			this.dirChooseButton.Click += new System.EventHandler(this.dirChooseButton_Click);
			// 
			// updateButton
			// 
			this.updateButton.BackColor = System.Drawing.Color.Chocolate;
			this.updateButton.Enabled = false;
			this.updateButton.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.updateButton.ForeColor = System.Drawing.SystemColors.WindowText;
			this.updateButton.Location = new System.Drawing.Point(452, 84);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(81, 59);
			this.updateButton.TabIndex = 17;
			this.updateButton.Text = "更新工程";
			this.updateButton.UseVisualStyleBackColor = false;
			this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// dirClearButton
			// 
			this.dirClearButton.Location = new System.Drawing.Point(463, 29);
			this.dirClearButton.Name = "dirClearButton";
			this.dirClearButton.Size = new System.Drawing.Size(63, 33);
			this.dirClearButton.TabIndex = 20;
			this.dirClearButton.Text = "清空";
			this.dirClearButton.UseVisualStyleBackColor = true;
			this.dirClearButton.Click += new System.EventHandler(this.dirClearButton_Click);
			// 
			// pathLabel
			// 
			this.pathLabel.Location = new System.Drawing.Point(129, 29);
			this.pathLabel.Name = "pathLabel";
			this.pathLabel.Size = new System.Drawing.Size(326, 33);
			this.pathLabel.TabIndex = 18;
			this.pathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.pathLabel.TextChanged += new System.EventHandler(this.pathLabel_TextChanged);
			// 
			// statusStrip1
			// 
			this.statusStrip1.BackColor = System.Drawing.Color.White;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel,
            this.myProgressBar});
			this.statusStrip1.Location = new System.Drawing.Point(0, 161);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(562, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 36;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(365, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// myProgressBar
			// 
			this.myProgressBar.BackColor = System.Drawing.Color.Black;
			this.myProgressBar.Name = "myProgressBar";
			this.myProgressBar.Size = new System.Drawing.Size(180, 16);
			this.myProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			// 
			// folderBrowserDialog
			// 
			this.folderBrowserDialog.ShowNewFolderButton = false;
			// 
			// NewProjectUpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(562, 183);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.dirChooseButton);
			this.Controls.Add(this.updateButton);
			this.Controls.Add(this.dirClearButton);
			this.Controls.Add(this.pathLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "NewProjectUpdateForm";
			this.Text = "工程更新";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProjectUpdateForm_FormClosed);
			this.Load += new System.EventHandler(this.NewProjectUpdateForm_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button dirChooseButton;
		private System.Windows.Forms.Button updateButton;
		private System.Windows.Forms.Button dirClearButton;
		private System.Windows.Forms.Label pathLabel;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.ToolStripProgressBar myProgressBar;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
	}
}