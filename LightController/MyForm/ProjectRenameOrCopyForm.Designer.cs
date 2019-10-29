namespace LightController.MyForm
{
	partial class ProjectRenameOrCopyForm
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
			this.cancelSkinButton = new CCWin.SkinControl.SkinButton();
			this.enterSkinButton = new CCWin.SkinControl.SkinButton();
			this.projectNameTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.projectNameLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cancelSkinButton
			// 
			this.cancelSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.cancelSkinButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.cancelSkinButton.BorderColor = System.Drawing.Color.Black;
			this.cancelSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.cancelSkinButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelSkinButton.DownBack = null;
			this.cancelSkinButton.Location = new System.Drawing.Point(160, 111);
			this.cancelSkinButton.MouseBack = null;
			this.cancelSkinButton.Name = "cancelSkinButton";
			this.cancelSkinButton.NormlBack = null;
			this.cancelSkinButton.Size = new System.Drawing.Size(72, 26);
			this.cancelSkinButton.TabIndex = 6;
			this.cancelSkinButton.Text = "取消";
			this.cancelSkinButton.UseVisualStyleBackColor = false;
			this.cancelSkinButton.Click += new System.EventHandler(this.cancelSkinButton_Click);
			// 
			// enterSkinButton
			// 
			this.enterSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.enterSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.enterSkinButton.BorderColor = System.Drawing.Color.Black;
			this.enterSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.enterSkinButton.DownBack = null;
			this.enterSkinButton.Location = new System.Drawing.Point(45, 111);
			this.enterSkinButton.MouseBack = null;
			this.enterSkinButton.Name = "enterSkinButton";
			this.enterSkinButton.NormlBack = null;
			this.enterSkinButton.Size = new System.Drawing.Size(72, 26);
			this.enterSkinButton.TabIndex = 7;
			this.enterSkinButton.Text = "确定";
			this.enterSkinButton.UseVisualStyleBackColor = false;
			this.enterSkinButton.Click += new System.EventHandler(this.enterSkinButton_Click);
			// 
			// projectNameTextBox
			// 
			this.projectNameTextBox.Location = new System.Drawing.Point(94, 67);
			this.projectNameTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.projectNameTextBox.Name = "projectNameTextBox";
			this.projectNameTextBox.Size = new System.Drawing.Size(152, 21);
			this.projectNameTextBox.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(28, 71);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 4;
			this.label1.Text = "新工程名：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(28, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 8;
			this.label2.Text = "原工程名：";
			// 
			// projectNameLabel
			// 
			this.projectNameLabel.AutoSize = true;
			this.projectNameLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.projectNameLabel.Location = new System.Drawing.Point(99, 31);
			this.projectNameLabel.Name = "projectNameLabel";
			this.projectNameLabel.Size = new System.Drawing.Size(57, 12);
			this.projectNameLabel.TabIndex = 9;
			this.projectNameLabel.Text = "原工程名";
			// 
			// ProjectRenameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelSkinButton;
			this.ClientSize = new System.Drawing.Size(265, 161);
			this.Controls.Add(this.projectNameLabel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cancelSkinButton);
			this.Controls.Add(this.enterSkinButton);
			this.Controls.Add(this.projectNameTextBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ProjectRenameForm";
			this.Text = "工程重命名";
			this.Load += new System.EventHandler(this.RenameForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private CCWin.SkinControl.SkinButton cancelSkinButton;
		private CCWin.SkinControl.SkinButton enterSkinButton;
		private System.Windows.Forms.TextBox projectNameTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label projectNameLabel;
	}
}