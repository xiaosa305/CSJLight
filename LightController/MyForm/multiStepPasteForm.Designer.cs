namespace LightController.MyForm
{
	partial class MultiStepPasteForm
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
			this.helpSkinButton = new CCWin.SkinControl.SkinButton();
			this.cancelSkinButton = new CCWin.SkinControl.SkinButton();
			this.converSkinButton = new CCWin.SkinControl.SkinButton();
			this.insertSkinButton = new CCWin.SkinControl.SkinButton();
			this.SuspendLayout();
			// 
			// helpSkinButton
			// 
			this.helpSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.helpSkinButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.helpSkinButton.BorderColor = System.Drawing.Color.Black;
			this.helpSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.helpSkinButton.DownBack = null;
			this.helpSkinButton.Location = new System.Drawing.Point(34, 74);
			this.helpSkinButton.MouseBack = null;
			this.helpSkinButton.Name = "helpSkinButton";
			this.helpSkinButton.NormlBack = null;
			this.helpSkinButton.Size = new System.Drawing.Size(71, 27);
			this.helpSkinButton.TabIndex = 3;
			this.helpSkinButton.Text = "使用说明";
			this.helpSkinButton.UseVisualStyleBackColor = false;
			this.helpSkinButton.Click += new System.EventHandler(this.helpSkinButton_Click);
			// 
			// cancelSkinButton
			// 
			this.cancelSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.cancelSkinButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.cancelSkinButton.BorderColor = System.Drawing.Color.Black;
			this.cancelSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.cancelSkinButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelSkinButton.DownBack = null;
			this.cancelSkinButton.Location = new System.Drawing.Point(140, 74);
			this.cancelSkinButton.MouseBack = null;
			this.cancelSkinButton.Name = "cancelSkinButton";
			this.cancelSkinButton.NormlBack = null;
			this.cancelSkinButton.Size = new System.Drawing.Size(71, 27);
			this.cancelSkinButton.TabIndex = 4;
			this.cancelSkinButton.Text = "取消";
			this.cancelSkinButton.UseVisualStyleBackColor = false;
			this.cancelSkinButton.Click += new System.EventHandler(this.cancelSkinButton_Click);
			// 
			// converSkinButton
			// 
			this.converSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.converSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.converSkinButton.BorderColor = System.Drawing.Color.Black;
			this.converSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.converSkinButton.DownBack = null;
			this.converSkinButton.Location = new System.Drawing.Point(140, 27);
			this.converSkinButton.MouseBack = null;
			this.converSkinButton.Name = "converSkinButton";
			this.converSkinButton.NormlBack = null;
			this.converSkinButton.Size = new System.Drawing.Size(71, 27);
			this.converSkinButton.TabIndex = 6;
			this.converSkinButton.Text = "覆盖";
			this.converSkinButton.UseVisualStyleBackColor = false;
			this.converSkinButton.Click += new System.EventHandler(this.insertOrCoverSkinButton_Click);
			// 
			// insertSkinButton
			// 
			this.insertSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.insertSkinButton.BaseColor = System.Drawing.Color.LightSkyBlue;
			this.insertSkinButton.BorderColor = System.Drawing.Color.Black;
			this.insertSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.insertSkinButton.DownBack = null;
			this.insertSkinButton.Location = new System.Drawing.Point(34, 27);
			this.insertSkinButton.MouseBack = null;
			this.insertSkinButton.Name = "insertSkinButton";
			this.insertSkinButton.NormlBack = null;
			this.insertSkinButton.Size = new System.Drawing.Size(71, 27);
			this.insertSkinButton.TabIndex = 7;
			this.insertSkinButton.Text = "插入";
			this.insertSkinButton.UseVisualStyleBackColor = false;
			this.insertSkinButton.Click += new System.EventHandler(this.insertOrCoverSkinButton_Click);
			// 
			// MultiStepPasteForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelSkinButton;
			this.ClientSize = new System.Drawing.Size(239, 124);
			this.Controls.Add(this.helpSkinButton);
			this.Controls.Add(this.cancelSkinButton);
			this.Controls.Add(this.converSkinButton);
			this.Controls.Add(this.insertSkinButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MultiStepPasteForm";
			this.Text = "粘贴多步";
			this.Load += new System.EventHandler(this.MultiStepPasteForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private CCWin.SkinControl.SkinButton helpSkinButton;
		private CCWin.SkinControl.SkinButton cancelSkinButton;
		private CCWin.SkinControl.SkinButton converSkinButton;
		private CCWin.SkinControl.SkinButton insertSkinButton;
	}
}