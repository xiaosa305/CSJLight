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
			this.components = new System.ComponentModel.Container();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.deleteSkinButton = new CCWin.SkinControl.SkinButton();
			this.newSkinButton = new CCWin.SkinControl.SkinButton();
			this.openSkinButton = new CCWin.SkinControl.SkinButton();
			this.cancelSkinButton = new CCWin.SkinControl.SkinButton();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Margin = new System.Windows.Forms.Padding(2);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(228, 444);
			this.treeView1.TabIndex = 0;
			this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
			this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
			// 
			// deleteSkinButton
			// 
			this.deleteSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.deleteSkinButton.BaseColor = System.Drawing.Color.MistyRose;
			this.deleteSkinButton.BorderColor = System.Drawing.Color.Black;
			this.deleteSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.deleteSkinButton.DownBack = null;
			this.deleteSkinButton.Location = new System.Drawing.Point(255, 14);
			this.deleteSkinButton.MouseBack = null;
			this.deleteSkinButton.Name = "deleteSkinButton";
			this.deleteSkinButton.NormlBack = null;
			this.deleteSkinButton.Size = new System.Drawing.Size(56, 33);
			this.deleteSkinButton.TabIndex = 2;
			this.deleteSkinButton.Text = "删除->";
			this.deleteSkinButton.UseVisualStyleBackColor = false;
			this.deleteSkinButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// newSkinButton
			// 
			this.newSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.newSkinButton.BaseColor = System.Drawing.Color.LightSalmon;
			this.newSkinButton.BorderColor = System.Drawing.Color.Black;
			this.newSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.newSkinButton.DownBack = null;
			this.newSkinButton.Location = new System.Drawing.Point(255, 232);
			this.newSkinButton.MouseBack = null;
			this.newSkinButton.Name = "newSkinButton";
			this.newSkinButton.NormlBack = null;
			this.newSkinButton.Size = new System.Drawing.Size(56, 33);
			this.newSkinButton.TabIndex = 2;
			this.newSkinButton.Text = "新建";
			this.newSkinButton.UseVisualStyleBackColor = false;
			this.newSkinButton.Click += new System.EventHandler(this.newButton_Click);
			// 
			// openSkinButton
			// 
			this.openSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.openSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.openSkinButton.BorderColor = System.Drawing.Color.Black;
			this.openSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.openSkinButton.DownBack = null;
			this.openSkinButton.Location = new System.Drawing.Point(255, 290);
			this.openSkinButton.MouseBack = null;
			this.openSkinButton.Name = "openSkinButton";
			this.openSkinButton.NormlBack = null;
			this.openSkinButton.Size = new System.Drawing.Size(56, 33);
			this.openSkinButton.TabIndex = 2;
			this.openSkinButton.Text = "打开";
			this.openSkinButton.UseVisualStyleBackColor = false;
			this.openSkinButton.Click += new System.EventHandler(this.openButton_Click);
			// 
			// cancelSkinButton
			// 
			this.cancelSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.cancelSkinButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.cancelSkinButton.BorderColor = System.Drawing.Color.Black;
			this.cancelSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.cancelSkinButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelSkinButton.DownBack = null;
			this.cancelSkinButton.Location = new System.Drawing.Point(255, 348);
			this.cancelSkinButton.MouseBack = null;
			this.cancelSkinButton.Name = "cancelSkinButton";
			this.cancelSkinButton.NormlBack = null;
			this.cancelSkinButton.Size = new System.Drawing.Size(56, 33);
			this.cancelSkinButton.TabIndex = 2;
			this.cancelSkinButton.Text = "取消";
			this.cancelSkinButton.UseVisualStyleBackColor = false;
			this.cancelSkinButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// HardwareSetChooseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelSkinButton;
			this.ClientSize = new System.Drawing.Size(331, 405);
			this.Controls.Add(this.cancelSkinButton);
			this.Controls.Add(this.openSkinButton);
			this.Controls.Add(this.newSkinButton);
			this.Controls.Add(this.deleteSkinButton);
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
		private CCWin.SkinControl.SkinButton deleteSkinButton;
		private CCWin.SkinControl.SkinButton newSkinButton;
		private CCWin.SkinControl.SkinButton openSkinButton;
		private CCWin.SkinControl.SkinButton cancelSkinButton;
	}
}