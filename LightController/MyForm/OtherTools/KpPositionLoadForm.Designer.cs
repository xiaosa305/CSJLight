namespace LightController.MyForm.OtherTools
{
	partial class KpPositionLoadForm
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
			this.enterButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(12, 301);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(70, 23);
			this.enterButton.TabIndex = 4;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(196, 301);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(70, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// deleteButton
			// 
			this.deleteButton.Location = new System.Drawing.Point(104, 301);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(70, 23);
			this.deleteButton.TabIndex = 4;
			this.deleteButton.Text = "删除";
			this.deleteButton.UseVisualStyleBackColor = true;
			// 
			// treeView1
			// 
			this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Margin = new System.Windows.Forms.Padding(2);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(283, 279);
			this.treeView1.TabIndex = 3;
			// 
			// KpPositionLoadForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(283, 349);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.treeView1);
			this.Name = "KpPositionLoadForm";
			this.Text = "加载按键位置";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.TreeView treeView1;
	}
}