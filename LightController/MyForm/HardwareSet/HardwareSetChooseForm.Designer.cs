namespace LightController.MyForm
{
	partial class HardwareSetChooseForm
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
			this.newButton = new System.Windows.Forms.Button();
			this.openButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Margin = new System.Windows.Forms.Padding(2);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(228, 405);
			this.treeView1.TabIndex = 0;
			this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
			this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
			// 
			// deleteButton
			// 
			this.deleteButton.Location = new System.Drawing.Point(255, 23);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(56, 33);
			this.deleteButton.TabIndex = 3;
			this.deleteButton.Text = "删除";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// newButton
			// 
			this.newButton.Location = new System.Drawing.Point(255, 248);
			this.newButton.Name = "newButton";
			this.newButton.Size = new System.Drawing.Size(56, 33);
			this.newButton.TabIndex = 3;
			this.newButton.Text = "新建";
			this.newButton.UseVisualStyleBackColor = true;
			this.newButton.Click += new System.EventHandler(this.newButton_Click);
			// 
			// openButton
			// 
			this.openButton.Location = new System.Drawing.Point(255, 299);
			this.openButton.Name = "openButton";
			this.openButton.Size = new System.Drawing.Size(56, 33);
			this.openButton.TabIndex = 3;
			this.openButton.Text = "打开";
			this.openButton.UseVisualStyleBackColor = true;
			this.openButton.Click += new System.EventHandler(this.openButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(255, 350);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(56, 33);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// HardwareSetChooseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(331, 405);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.openButton);
			this.Controls.Add(this.newButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.treeView1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(347, 444);
			this.Name = "HardwareSetChooseForm";
			this.Text = "选择硬件配置文件";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HardwareSetChooseForm_FormClosed);
			this.Load += new System.EventHandler(this.HardwareSetChooseForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button newButton;
		private System.Windows.Forms.Button openButton;
		private System.Windows.Forms.Button cancelButton;
	}
}