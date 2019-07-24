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
			this.openButton = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(302, 506);
			this.treeView1.TabIndex = 0;
			this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
			// 
			// openButton
			// 
			this.openButton.Location = new System.Drawing.Point(344, 371);
			this.openButton.Name = "openButton";
			this.openButton.Size = new System.Drawing.Size(75, 41);
			this.openButton.TabIndex = 1;
			this.openButton.Text = "打开";
			this.openButton.UseVisualStyleBackColor = true;
			this.openButton.Click += new System.EventHandler(this.openButton_Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.OrangeRed;
			this.button2.Location = new System.Drawing.Point(344, 23);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 41);
			this.button2.TabIndex = 1;
			this.button2.Text = "删除->";
			this.button2.UseVisualStyleBackColor = false;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(344, 443);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 41);
			this.button3.TabIndex = 1;
			this.button3.Text = "取消";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// HardwareSetChooseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(446, 506);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.openButton);
			this.Controls.Add(this.treeView1);
			this.Name = "HardwareSetChooseForm";
			this.Text = "选择硬件配置文件";
			this.Load += new System.EventHandler(this.HardwareSetChooseForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button openButton;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
	}
}