namespace LightController.MyForm
{
	partial class UseFrameForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.frameSkinComboBox = new CCWin.SkinControl.SkinComboBox();
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
			this.cancelSkinButton.Location = new System.Drawing.Point(126, 83);
			this.cancelSkinButton.MouseBack = null;
			this.cancelSkinButton.Name = "cancelSkinButton";
			this.cancelSkinButton.NormlBack = null;
			this.cancelSkinButton.Size = new System.Drawing.Size(78, 26);
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
			this.enterSkinButton.Location = new System.Drawing.Point(32, 83);
			this.enterSkinButton.MouseBack = null;
			this.enterSkinButton.Name = "enterSkinButton";
			this.enterSkinButton.NormlBack = null;
			this.enterSkinButton.Size = new System.Drawing.Size(78, 26);
			this.enterSkinButton.TabIndex = 7;
			this.enterSkinButton.Text = "确定";
			this.enterSkinButton.UseVisualStyleBackColor = false;
			this.enterSkinButton.Click += new System.EventHandler(this.enterSkinButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(42, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 8;
			this.label1.Text = "场景名：";
			// 
			// frameSkinComboBox
			// 
			this.frameSkinComboBox.BaseColor = System.Drawing.Color.Gray;
			this.frameSkinComboBox.BorderColor = System.Drawing.Color.Gray;
			this.frameSkinComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.frameSkinComboBox.Font = new System.Drawing.Font("华文细黑", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.frameSkinComboBox.FormattingEnabled = true;
			this.frameSkinComboBox.Location = new System.Drawing.Point(110, 32);
			this.frameSkinComboBox.Name = "frameSkinComboBox";
			this.frameSkinComboBox.Size = new System.Drawing.Size(94, 25);
			this.frameSkinComboBox.TabIndex = 9;
			this.frameSkinComboBox.WaterText = "";
			// 
			// UseFrameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelSkinButton;
			this.ClientSize = new System.Drawing.Size(230, 134);
			this.Controls.Add(this.frameSkinComboBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cancelSkinButton);
			this.Controls.Add(this.enterSkinButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "UseFrameForm";
			this.Text = "使用其他场景";
			this.Load += new System.EventHandler(this.UseFrameForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private CCWin.SkinControl.SkinButton cancelSkinButton;
		private CCWin.SkinControl.SkinButton enterSkinButton;
		private System.Windows.Forms.Label label1;
		private CCWin.SkinControl.SkinComboBox frameSkinComboBox;
	}
}