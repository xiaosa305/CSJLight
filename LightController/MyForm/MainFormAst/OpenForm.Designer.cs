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
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.sortCreateTimeButton = new System.Windows.Forms.Button();
			this.sortNameButton = new System.Windows.Forms.Button();
			this.changeWorkspaceButton = new System.Windows.Forms.Button();
			this.sortLastTimeButton = new System.Windows.Forms.Button();
			this.wsFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.myContextMenuStrip.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// projectTreeView
			// 
			this.projectTreeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.projectTreeView.Location = new System.Drawing.Point(0, 0);
			this.projectTreeView.Margin = new System.Windows.Forms.Padding(2);
			this.projectTreeView.Name = "projectTreeView";
			this.projectTreeView.Size = new System.Drawing.Size(242, 244);
			this.projectTreeView.TabIndex = 0;
			this.projectTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
			this.projectTreeView.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
			this.projectTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(24, 299);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(75, 23);
			this.enterButton.TabIndex = 1;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Location = new System.Drawing.Point(120, 299);
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
			this.cancelButton.Location = new System.Drawing.Point(216, 299);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// frameLabel
			// 
			this.frameLabel.AutoSize = true;
			this.frameLabel.Location = new System.Drawing.Point(23, 267);
			this.frameLabel.Name = "frameLabel";
			this.frameLabel.Size = new System.Drawing.Size(83, 12);
			this.frameLabel.TabIndex = 2;
			this.frameLabel.Text = "选择初始场景:";
			// 
			// frameComboBox
			// 
			this.frameComboBox.FormattingEnabled = true;
			this.frameComboBox.Location = new System.Drawing.Point(112, 263);
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
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.sortCreateTimeButton);
			this.panel1.Controls.Add(this.sortNameButton);
			this.panel1.Controls.Add(this.changeWorkspaceButton);
			this.panel1.Controls.Add(this.sortLastTimeButton);
			this.panel1.Controls.Add(this.projectTreeView);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(309, 246);
			this.panel1.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(247, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 12);
			this.label1.TabIndex = 6;
			this.label1.Text = "排序方法:";
			// 
			// sortCreateTimeButton
			// 
			this.sortCreateTimeButton.Location = new System.Drawing.Point(243, 86);
			this.sortCreateTimeButton.Name = "sortCreateTimeButton";
			this.sortCreateTimeButton.Size = new System.Drawing.Size(63, 45);
			this.sortCreateTimeButton.TabIndex = 7;
			this.sortCreateTimeButton.Text = "创建时间";
			this.sortCreateTimeButton.UseVisualStyleBackColor = true;
			this.sortCreateTimeButton.Click += new System.EventHandler(this.sortCreateTimeButton_Click);
			// 
			// sortNameButton
			// 
			this.sortNameButton.Location = new System.Drawing.Point(243, 32);
			this.sortNameButton.Name = "sortNameButton";
			this.sortNameButton.Size = new System.Drawing.Size(63, 45);
			this.sortNameButton.TabIndex = 8;
			this.sortNameButton.Text = "文件名";
			this.sortNameButton.UseVisualStyleBackColor = true;
			this.sortNameButton.Click += new System.EventHandler(this.sortNameButton_Click);
			// 
			// changeWorkspaceButton
			// 
			this.changeWorkspaceButton.Location = new System.Drawing.Point(243, 194);
			this.changeWorkspaceButton.Name = "changeWorkspaceButton";
			this.changeWorkspaceButton.Size = new System.Drawing.Size(63, 45);
			this.changeWorkspaceButton.TabIndex = 9;
			this.changeWorkspaceButton.Text = "更改\r\n工作目录";
			this.changeWorkspaceButton.UseVisualStyleBackColor = true;
			this.changeWorkspaceButton.Click += new System.EventHandler(this.changeWorkspaceButton_Click);
			// 
			// sortLastTimeButton
			// 
			this.sortLastTimeButton.Location = new System.Drawing.Point(243, 140);
			this.sortLastTimeButton.Name = "sortLastTimeButton";
			this.sortLastTimeButton.Size = new System.Drawing.Size(63, 45);
			this.sortLastTimeButton.TabIndex = 9;
			this.sortLastTimeButton.Text = "保存时间";
			this.sortLastTimeButton.UseVisualStyleBackColor = true;
			this.sortLastTimeButton.Click += new System.EventHandler(this.sortLastTimeButton_Click);
			// 
			// wsFolderBrowserDialog
			// 
			this.wsFolderBrowserDialog.Description = "使用本功能，只在本次软件开启期间有效。本功能主要供高级用户调试使用，建议不要轻易在打开导出工程之后，随意更改工程或编辑灯具。";
			// 
			// OpenForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(309, 338);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.frameComboBox);
			this.Controls.Add(this.frameLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.enterButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(325, 377);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(325, 370);
			this.Name = "OpenForm";
			this.Text = "打开工程";
			this.Load += new System.EventHandler(this.OpenForm_Load);
			this.myContextMenuStrip.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
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
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button sortCreateTimeButton;
		private System.Windows.Forms.Button sortNameButton;
		private System.Windows.Forms.Button sortLastTimeButton;
		private System.Windows.Forms.Button changeWorkspaceButton;
		private System.Windows.Forms.FolderBrowserDialog wsFolderBrowserDialog;
	}
}