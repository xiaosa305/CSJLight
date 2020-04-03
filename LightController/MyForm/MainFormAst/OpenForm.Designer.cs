namespace LightController.MyForm
{
	partial class OpenForm
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
			this.projectTreeView = new System.Windows.Forms.TreeView();
			this.enterButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.frameLabel = new System.Windows.Forms.Label();
			this.frameComboBox = new System.Windows.Forms.ComboBox();
			this.myContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.renameButton = new System.Windows.Forms.ToolStripMenuItem();
			this.copyButton = new System.Windows.Forms.ToolStripMenuItem();
			this.myContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// projectTreeView
			// 
			this.projectTreeView.Dock = System.Windows.Forms.DockStyle.Top;
			this.projectTreeView.Location = new System.Drawing.Point(0, 0);
			this.projectTreeView.Margin = new System.Windows.Forms.Padding(2);
			this.projectTreeView.Name = "projectTreeView";
			this.projectTreeView.Size = new System.Drawing.Size(309, 233);
			this.projectTreeView.TabIndex = 0;
			this.projectTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
			this.projectTreeView.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
			this.projectTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(22, 290);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(75, 23);
			this.enterButton.TabIndex = 1;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Location = new System.Drawing.Point(118, 290);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(75, 23);
			this.deleteButton.TabIndex = 1;
			this.deleteButton.Text = "删除";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(214, 290);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// frameLabel
			// 
			this.frameLabel.AutoSize = true;
			this.frameLabel.Location = new System.Drawing.Point(20, 257);
			this.frameLabel.Name = "frameLabel";
			this.frameLabel.Size = new System.Drawing.Size(83, 12);
			this.frameLabel.TabIndex = 2;
			this.frameLabel.Text = "选择初始场景:";
			// 
			// frameComboBox
			// 
			this.frameComboBox.FormattingEnabled = true;
			this.frameComboBox.Location = new System.Drawing.Point(109, 253);
			this.frameComboBox.Name = "frameComboBox";
			this.frameComboBox.Size = new System.Drawing.Size(93, 20);
			this.frameComboBox.TabIndex = 3;
			// 
			// myContextMenuStrip
			// 
			this.myContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameButton,
            this.copyButton});
			this.myContextMenuStrip.Name = "contextMenuStrip1";
			this.myContextMenuStrip.Size = new System.Drawing.Size(137, 48);
			// 
			// renameButton
			// 
			this.renameButton.Name = "renameButton";
			this.renameButton.Size = new System.Drawing.Size(136, 22);
			this.renameButton.Text = "工程重命名";
			this.renameButton.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
			// 
			// copyButton
			// 
			this.copyButton.Name = "copyButton";
			this.copyButton.Size = new System.Drawing.Size(136, 22);
			this.copyButton.Text = "工程复制";
			this.copyButton.Click += new System.EventHandler(this.copyProjectToolStripMenuItem_Click);
			// 
			// OpenForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(309, 332);
			this.Controls.Add(this.frameComboBox);
			this.Controls.Add(this.frameLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.projectTreeView);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(325, 371);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(325, 371);
			this.Name = "OpenForm";
			this.Text = "打开工程";
			this.Load += new System.EventHandler(this.OpenForm_Load);
			this.myContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView projectTreeView;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label frameLabel;
		private System.Windows.Forms.ComboBox frameComboBox;
		private System.Windows.Forms.ContextMenuStrip myContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem renameButton;
		private System.Windows.Forms.ToolStripMenuItem copyButton;
	}
}