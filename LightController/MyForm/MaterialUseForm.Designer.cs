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
			this.insertButton = new System.Windows.Forms.Button();
			this.coverButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(326, 394);
			this.treeView1.TabIndex = 0;
			this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
			// 
			// insertButton
			// 
			this.insertButton.Location = new System.Drawing.Point(64, 420);
			this.insertButton.Name = "insertButton";
			this.insertButton.Size = new System.Drawing.Size(75, 34);
			this.insertButton.TabIndex = 1;
			this.insertButton.Text = "插入";
			this.insertButton.UseVisualStyleBackColor = true;
			this.insertButton.Click += new System.EventHandler(this.insertOrCoverButton_Click);
			// 
			// coverButton
			// 
			this.coverButton.Location = new System.Drawing.Point(186, 420);
			this.coverButton.Name = "coverButton";
			this.coverButton.Size = new System.Drawing.Size(75, 34);
			this.coverButton.TabIndex = 1;
			this.coverButton.Text = "覆盖";
			this.coverButton.UseVisualStyleBackColor = true;
			this.coverButton.Click += new System.EventHandler(this.insertOrCoverButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(186, 474);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 34);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Location = new System.Drawing.Point(64, 474);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(75, 34);
			this.deleteButton.TabIndex = 1;
			this.deleteButton.Text = "删除";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// MaterialUseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(326, 531);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.coverButton);
			this.Controls.Add(this.insertButton);
			this.Controls.Add(this.treeView1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MaterialUseForm";
			this.ShowInTaskbar = false;
			this.Text = "使用素材";
			this.Load += new System.EventHandler(this.MaterialUseForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button insertButton;
		private System.Windows.Forms.Button coverButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button deleteButton;
	}
}