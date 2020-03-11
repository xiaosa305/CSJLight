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
			this.projectTreeView = new System.Windows.Forms.TreeView();
			this.mySkinContextMenuStrip = new CCWin.SkinControl.SkinContextMenuStrip();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enterButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.mySkinContextMenuStrip.SuspendLayout();
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
			// mySkinContextMenuStrip
			// 
			this.mySkinContextMenuStrip.Arrow = System.Drawing.Color.Black;
			this.mySkinContextMenuStrip.Back = System.Drawing.Color.White;
			this.mySkinContextMenuStrip.BackRadius = 4;
			this.mySkinContextMenuStrip.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
			this.mySkinContextMenuStrip.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
			this.mySkinContextMenuStrip.Fore = System.Drawing.Color.Black;
			this.mySkinContextMenuStrip.HoverFore = System.Drawing.Color.White;
			this.mySkinContextMenuStrip.ItemAnamorphosis = true;
			this.mySkinContextMenuStrip.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
			this.mySkinContextMenuStrip.ItemBorderShow = true;
			this.mySkinContextMenuStrip.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
			this.mySkinContextMenuStrip.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
			this.mySkinContextMenuStrip.ItemRadius = 4;
			this.mySkinContextMenuStrip.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.mySkinContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.copyProjectToolStripMenuItem});
			this.mySkinContextMenuStrip.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
			this.mySkinContextMenuStrip.Name = "mySkinContextMenuStrip";
			this.mySkinContextMenuStrip.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.mySkinContextMenuStrip.Size = new System.Drawing.Size(137, 48);
			this.mySkinContextMenuStrip.SkinAllColor = true;
			this.mySkinContextMenuStrip.TitleAnamorphosis = true;
			this.mySkinContextMenuStrip.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
			this.mySkinContextMenuStrip.TitleRadius = 4;
			this.mySkinContextMenuStrip.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.renameToolStripMenuItem.Text = "工程重命名";
			this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
			// 
			// copyProjectToolStripMenuItem
			// 
			this.copyProjectToolStripMenuItem.Name = "copyProjectToolStripMenuItem";
			this.copyProjectToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.copyProjectToolStripMenuItem.Text = "工程复制";
			this.copyProjectToolStripMenuItem.Click += new System.EventHandler(this.copyProjectToolStripMenuItem_Click);
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(22, 254);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(75, 23);
			this.enterButton.TabIndex = 1;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Location = new System.Drawing.Point(118, 254);
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
			this.cancelButton.Location = new System.Drawing.Point(214, 254);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// OpenForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(309, 294);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.projectTreeView);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(325, 333);
			this.Name = "OpenForm";
			this.Text = "打开工程";
			this.Load += new System.EventHandler(this.OpenForm_Load);
			this.mySkinContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView projectTreeView;
		private CCWin.SkinControl.SkinContextMenuStrip mySkinContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyProjectToolStripMenuItem;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button cancelButton;
	}
}