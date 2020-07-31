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
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.deleteButton = new System.Windows.Forms.Button();
			this.insertButton = new System.Windows.Forms.Button();
			this.coverButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.insertLastButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Margin = new System.Windows.Forms.Padding(2);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(279, 315);
			this.treeView1.TabIndex = 0;
			this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
			// 
			// deleteButton
			// 
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
			this.insertButton.Location = new System.Drawing.Point(118, 336);
			this.insertButton.Name = "insertButton";
			this.insertButton.Size = new System.Drawing.Size(67, 27);
			this.insertButton.TabIndex = 3;
			this.insertButton.Tag = "0";
			this.insertButton.Text = "插入";
			this.insertButton.UseVisualStyleBackColor = true;
			this.insertButton.Click += new System.EventHandler(this.insertOrCoverButton_Click);
			// 
			// coverButton
			// 
			this.coverButton.Location = new System.Drawing.Point(198, 336);
			this.coverButton.Name = "coverButton";
			this.coverButton.Size = new System.Drawing.Size(67, 27);
			this.coverButton.TabIndex = 3;
			this.coverButton.Tag = "1";
			this.coverButton.Text = "覆盖";
			this.coverButton.UseVisualStyleBackColor = true;
			this.coverButton.Click += new System.EventHandler(this.insertOrCoverButton_Click);
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(14, 370);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(67, 27);
			this.helpButton.TabIndex = 3;
			this.helpButton.Text = "使用说明";
			this.helpButton.UseVisualStyleBackColor = true;
			this.helpButton.Click += new System.EventHandler(this.helpSkinButton_Click);
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
			// insertLastButton
			// 
			this.insertLastButton.Location = new System.Drawing.Point(118, 370);
			this.insertLastButton.Name = "insertLastButton";
			this.insertLastButton.Size = new System.Drawing.Size(67, 27);
			this.insertLastButton.TabIndex = 3;
			this.insertLastButton.Tag = "2";
			this.insertLastButton.Text = "追加";
			this.insertLastButton.UseVisualStyleBackColor = true;
			this.insertLastButton.Click += new System.EventHandler(this.insertOrCoverButton_Click);
			// 
			// MaterialUseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(279, 416);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.insertLastButton);
			this.Controls.Add(this.coverButton);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.insertButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.treeView1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MaterialUseForm";
			this.ShowInTaskbar = false;
			this.Text = "使用素材";
			this.Load += new System.EventHandler(this.MaterialUseForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button insertButton;
		private System.Windows.Forms.Button coverButton;
		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button insertLastButton;
	}
}