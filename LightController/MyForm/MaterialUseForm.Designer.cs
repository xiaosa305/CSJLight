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
			this.components = new System.ComponentModel.Container();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.insertSkinButton = new CCWin.SkinControl.SkinButton();
			this.converSkinButton = new CCWin.SkinControl.SkinButton();
			this.deleteSkinButton = new CCWin.SkinControl.SkinButton();
			this.cancelSkinButton = new CCWin.SkinControl.SkinButton();
			this.helpSkinButton = new CCWin.SkinControl.SkinButton();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Margin = new System.Windows.Forms.Padding(2);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(244, 315);
			this.treeView1.TabIndex = 0;
			this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
			// 
			// insertSkinButton
			// 
			this.insertSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.insertSkinButton.BaseColor = System.Drawing.Color.LightSkyBlue;
			this.insertSkinButton.BorderColor = System.Drawing.Color.Black;
			this.insertSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.insertSkinButton.DownBack = null;
			this.insertSkinButton.Location = new System.Drawing.Point(91, 335);
			this.insertSkinButton.MouseBack = null;
			this.insertSkinButton.Name = "insertSkinButton";
			this.insertSkinButton.NormlBack = null;
			this.insertSkinButton.Size = new System.Drawing.Size(56, 27);
			this.insertSkinButton.TabIndex = 2;
			this.insertSkinButton.Text = "插入";
			this.insertSkinButton.UseVisualStyleBackColor = false;
			this.insertSkinButton.Click += new System.EventHandler(this.insertOrCoverButton_Click);
			// 
			// converSkinButton
			// 
			this.converSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.converSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.converSkinButton.BorderColor = System.Drawing.Color.Black;
			this.converSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.converSkinButton.DownBack = null;
			this.converSkinButton.Location = new System.Drawing.Point(168, 335);
			this.converSkinButton.MouseBack = null;
			this.converSkinButton.Name = "converSkinButton";
			this.converSkinButton.NormlBack = null;
			this.converSkinButton.Size = new System.Drawing.Size(56, 27);
			this.converSkinButton.TabIndex = 2;
			this.converSkinButton.Text = "覆盖";
			this.converSkinButton.UseVisualStyleBackColor = false;
			this.converSkinButton.Click += new System.EventHandler(this.insertOrCoverButton_Click);
			// 
			// deleteSkinButton
			// 
			this.deleteSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.deleteSkinButton.BaseColor = System.Drawing.Color.MistyRose;
			this.deleteSkinButton.BorderColor = System.Drawing.Color.Black;
			this.deleteSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.deleteSkinButton.DownBack = null;
			this.deleteSkinButton.Location = new System.Drawing.Point(12, 335);
			this.deleteSkinButton.MouseBack = null;
			this.deleteSkinButton.Name = "deleteSkinButton";
			this.deleteSkinButton.NormlBack = null;
			this.deleteSkinButton.Size = new System.Drawing.Size(56, 27);
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
			this.cancelSkinButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelSkinButton.DownBack = null;
			this.cancelSkinButton.Location = new System.Drawing.Point(168, 382);
			this.cancelSkinButton.MouseBack = null;
			this.cancelSkinButton.Name = "cancelSkinButton";
			this.cancelSkinButton.NormlBack = null;
			this.cancelSkinButton.Size = new System.Drawing.Size(56, 27);
			this.cancelSkinButton.TabIndex = 2;
			this.cancelSkinButton.Text = "取消";
			this.cancelSkinButton.UseVisualStyleBackColor = false;
			this.cancelSkinButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// helpSkinButton
			// 
			this.helpSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.helpSkinButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.helpSkinButton.BorderColor = System.Drawing.Color.Black;
			this.helpSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.helpSkinButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.helpSkinButton.DownBack = null;
			this.helpSkinButton.Location = new System.Drawing.Point(12, 382);
			this.helpSkinButton.MouseBack = null;
			this.helpSkinButton.Name = "helpSkinButton";
			this.helpSkinButton.NormlBack = null;
			this.helpSkinButton.Size = new System.Drawing.Size(71, 27);
			this.helpSkinButton.TabIndex = 2;
			this.helpSkinButton.Text = "使用说明";
			this.helpSkinButton.UseVisualStyleBackColor = false;
			this.helpSkinButton.Click += new System.EventHandler(this.helpSkinButton_Click);
			// 
			// MaterialUseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelSkinButton;
			this.ClientSize = new System.Drawing.Size(244, 425);
			this.Controls.Add(this.helpSkinButton);
			this.Controls.Add(this.cancelSkinButton);
			this.Controls.Add(this.deleteSkinButton);
			this.Controls.Add(this.converSkinButton);
			this.Controls.Add(this.insertSkinButton);
			this.Controls.Add(this.treeView1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MaterialUseForm";
			this.ShowInTaskbar = false;
			this.Text = "使用素材";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MaterialUseForm_FormClosed);
			this.Load += new System.EventHandler(this.MaterialUseForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private CCWin.SkinControl.SkinButton insertSkinButton;
		private CCWin.SkinControl.SkinButton converSkinButton;
		private CCWin.SkinControl.SkinButton deleteSkinButton;
		private CCWin.SkinControl.SkinButton cancelSkinButton;
		private CCWin.SkinControl.SkinButton helpSkinButton;
	}
}