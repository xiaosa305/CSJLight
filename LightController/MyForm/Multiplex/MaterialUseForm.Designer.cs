namespace LightController.MyForm
{
	partial class MaterialUseForm
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
			this.materialTreeView = new System.Windows.Forms.TreeView();
			this.deleteButton = new System.Windows.Forms.Button();
			this.insertButton = new System.Windows.Forms.Button();
			this.coverButton = new System.Windows.Forms.Button();
			this.previewButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.appendButton = new System.Windows.Forms.Button();
			this.myStatusStrip = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.myStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// materialTreeView
			// 
			this.materialTreeView.Dock = System.Windows.Forms.DockStyle.Top;
			this.materialTreeView.Location = new System.Drawing.Point(0, 0);
			this.materialTreeView.Margin = new System.Windows.Forms.Padding(2);
			this.materialTreeView.Name = "materialTreeView";
			this.materialTreeView.Size = new System.Drawing.Size(279, 315);
			this.materialTreeView.TabIndex = 0;
			this.materialTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.materialTreeView_NodeMouseClick);
			// 
			// deleteButton
			// 
			this.deleteButton.Enabled = false;
			this.deleteButton.Location = new System.Drawing.Point(14, 336);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(67, 27);
			this.deleteButton.TabIndex = 3;
			this.deleteButton.Text = "删除";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// insertButton
			// 
			this.insertButton.Enabled = false;
			this.insertButton.Location = new System.Drawing.Point(118, 336);
			this.insertButton.Name = "insertButton";
			this.insertButton.Size = new System.Drawing.Size(67, 27);
			this.insertButton.TabIndex = 3;
			this.insertButton.Text = "插入";
			this.insertButton.UseVisualStyleBackColor = true;
			this.insertButton.Click += new System.EventHandler(this.insertOrCoverButton_Click);
			// 
			// coverButton
			// 
			this.coverButton.Enabled = false;
			this.coverButton.Location = new System.Drawing.Point(198, 336);
			this.coverButton.Name = "coverButton";
			this.coverButton.Size = new System.Drawing.Size(67, 27);
			this.coverButton.TabIndex = 3;
			this.coverButton.Text = "覆盖";
			this.coverButton.UseVisualStyleBackColor = true;
			this.coverButton.Click += new System.EventHandler(this.insertOrCoverButton_Click);
			// 
			// previewButton
			// 
			this.previewButton.BackColor = System.Drawing.Color.DarkSalmon;
			this.previewButton.Enabled = false;
			this.previewButton.Location = new System.Drawing.Point(14, 370);
			this.previewButton.Name = "previewButton";
			this.previewButton.Size = new System.Drawing.Size(67, 27);
			this.previewButton.TabIndex = 3;
			this.previewButton.Text = "预览";
			this.previewButton.UseVisualStyleBackColor = false;
			this.previewButton.Visible = false;
			this.previewButton.TextChanged += new System.EventHandler(this.someControl_TextChanged);
			this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(198, 370);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(67, 27);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// appendButton
			// 
			this.appendButton.Enabled = false;
			this.appendButton.Location = new System.Drawing.Point(118, 370);
			this.appendButton.Name = "appendButton";
			this.appendButton.Size = new System.Drawing.Size(67, 27);
			this.appendButton.TabIndex = 3;
			this.appendButton.Text = "追加";
			this.appendButton.UseVisualStyleBackColor = true;
			this.appendButton.Click += new System.EventHandler(this.insertOrCoverButton_Click);
			// 
			// myStatusStrip
			// 
			this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.myStatusStrip.Location = new System.Drawing.Point(0, 414);
			this.myStatusStrip.Name = "myStatusStrip";
			this.myStatusStrip.Size = new System.Drawing.Size(279, 22);
			this.myStatusStrip.SizingGrip = false;
			this.myStatusStrip.TabIndex = 4;
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(264, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MaterialUseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(279, 436);
			this.Controls.Add(this.myStatusStrip);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.appendButton);
			this.Controls.Add(this.coverButton);
			this.Controls.Add(this.previewButton);
			this.Controls.Add(this.insertButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.materialTreeView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HelpButton = true;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MaterialUseForm";
			this.ShowInTaskbar = false;
			this.Text = "使用·用户素材 ";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.MaterialUseForm_HelpButtonClicked);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MaterialUseForm_FormClosed);
			this.Load += new System.EventHandler(this.MaterialUseForm_Load);
			this.myStatusStrip.ResumeLayout(false);
			this.myStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView materialTreeView;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button insertButton;
		private System.Windows.Forms.Button coverButton;
		private System.Windows.Forms.Button previewButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button appendButton;
		private System.Windows.Forms.StatusStrip myStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
	}
}