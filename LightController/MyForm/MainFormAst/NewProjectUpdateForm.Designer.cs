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
			this.pathLabel = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.myProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.exportedCheckBox = new System.Windows.Forms.CheckBox();
			this.dirPanel = new System.Windows.Forms.Panel();
			this.statusStrip1.SuspendLayout();
			this.dirPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// dirChooseButton
			// 
			this.dirChooseButton.Location = new System.Drawing.Point(32, 21);
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
			this.updateButton.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.updateButton.Location = new System.Drawing.Point(467, 12);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(81, 59);
			this.updateButton.TabIndex = 17;
			this.updateButton.Text = "更新工程";
			this.updateButton.UseVisualStyleBackColor = false;
			this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// pathLabel
			// 
			this.pathLabel.Location = new System.Drawing.Point(134, 21);
			this.pathLabel.Name = "pathLabel";
			this.pathLabel.Size = new System.Drawing.Size(404, 33);
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
			this.statusStrip1.Location = new System.Drawing.Point(0, 164);
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
			// exportedCheckBox
			// 
			this.exportedCheckBox.AutoSize = true;
			this.exportedCheckBox.Location = new System.Drawing.Point(32, 23);
			this.exportedCheckBox.Name = "exportedCheckBox";
			this.exportedCheckBox.Size = new System.Drawing.Size(96, 16);
			this.exportedCheckBox.TabIndex = 37;
			this.exportedCheckBox.Text = "更新已有工程";
			this.exportedCheckBox.UseVisualStyleBackColor = true;
			this.exportedCheckBox.CheckedChanged += new System.EventHandler(this.currentCheckBox_CheckedChanged);
			// 
			// dirPanel
			// 
			this.dirPanel.BackColor = System.Drawing.SystemColors.ButtonShadow;
			this.dirPanel.Controls.Add(this.dirChooseButton);
			this.dirPanel.Controls.Add(this.pathLabel);
			this.dirPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dirPanel.Location = new System.Drawing.Point(0, 85);
			this.dirPanel.Name = "dirPanel";
			this.dirPanel.Size = new System.Drawing.Size(562, 79);
			this.dirPanel.TabIndex = 38;
			this.dirPanel.Visible = false;
			// 
			// NewProjectUpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.ClientSize = new System.Drawing.Size(562, 186);
			this.Controls.Add(this.dirPanel);
			this.Controls.Add(this.exportedCheckBox);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.updateButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "NewProjectUpdateForm";
			this.Text = "工程更新";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProjectUpdateForm_FormClosed);
			this.Load += new System.EventHandler(this.NewProjectUpdateForm_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.dirPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button dirChooseButton;
		private System.Windows.Forms.Button updateButton;
		private System.Windows.Forms.Label pathLabel;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.ToolStripProgressBar myProgressBar;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.CheckBox exportedCheckBox;
		private System.Windows.Forms.Panel dirPanel;
	}
}