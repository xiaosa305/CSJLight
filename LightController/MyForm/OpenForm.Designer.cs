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
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.sqLiteCommand1 = new System.Data.SQLite.SQLiteCommand();
			this.enterSkinButton = new CCWin.SkinControl.SkinButton();
			this.deleteSkinButton = new CCWin.SkinControl.SkinButton();
			this.cancelSkinButton = new CCWin.SkinControl.SkinButton();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Margin = new System.Windows.Forms.Padding(2);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(309, 233);
			this.treeView1.TabIndex = 0;
			this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
			this.treeView1.DoubleClick += new System.EventHandler(this.enterButton_Click);
			// 
			// sqLiteCommand1
			// 
			this.sqLiteCommand1.CommandText = null;
			// 
			// enterSkinButton
			// 
			this.enterSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.enterSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.enterSkinButton.BorderColor = System.Drawing.Color.Black;
			this.enterSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.enterSkinButton.DownBack = null;
			this.enterSkinButton.Location = new System.Drawing.Point(38, 248);
			this.enterSkinButton.MouseBack = null;
			this.enterSkinButton.Name = "enterSkinButton";
			this.enterSkinButton.NormlBack = null;
			this.enterSkinButton.Size = new System.Drawing.Size(61, 28);
			this.enterSkinButton.TabIndex = 2;
			this.enterSkinButton.Text = "确定";
			this.enterSkinButton.UseVisualStyleBackColor = false;
			this.enterSkinButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// deleteSkinButton
			// 
			this.deleteSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.deleteSkinButton.BaseColor = System.Drawing.Color.MistyRose;
			this.deleteSkinButton.BorderColor = System.Drawing.Color.Black;
			this.deleteSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.deleteSkinButton.DownBack = null;
			this.deleteSkinButton.Location = new System.Drawing.Point(124, 248);
			this.deleteSkinButton.MouseBack = null;
			this.deleteSkinButton.Name = "deleteSkinButton";
			this.deleteSkinButton.NormlBack = null;
			this.deleteSkinButton.Size = new System.Drawing.Size(61, 28);
			this.deleteSkinButton.TabIndex = 2;
			this.deleteSkinButton.Text = "删除";
			this.deleteSkinButton.UseVisualStyleBackColor = false;
			this.deleteSkinButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// cancelSkinButton
			// 
			this.cancelSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.cancelSkinButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.cancelSkinButton.BorderColor = System.Drawing.Color.Black;
			this.cancelSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.cancelSkinButton.DownBack = null;
			this.cancelSkinButton.Location = new System.Drawing.Point(209, 248);
			this.cancelSkinButton.MouseBack = null;
			this.cancelSkinButton.Name = "cancelSkinButton";
			this.cancelSkinButton.NormlBack = null;
			this.cancelSkinButton.Size = new System.Drawing.Size(61, 28);
			this.cancelSkinButton.TabIndex = 2;
			this.cancelSkinButton.Text = "取消";
			this.cancelSkinButton.UseVisualStyleBackColor = false;
			this.cancelSkinButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// OpenForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(309, 294);
			this.Controls.Add(this.cancelSkinButton);
			this.Controls.Add(this.deleteSkinButton);
			this.Controls.Add(this.enterSkinButton);
			this.Controls.Add(this.treeView1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(325, 333);
			this.Name = "OpenForm";
			this.Text = "打开工程";
			this.Load += new System.EventHandler(this.OpenForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private System.Data.SQLite.SQLiteCommand sqLiteCommand1;
		private CCWin.SkinControl.SkinButton enterSkinButton;
		private CCWin.SkinControl.SkinButton deleteSkinButton;
		private CCWin.SkinControl.SkinButton cancelSkinButton;
	}
}