namespace LightController.MyForm
{
	partial class UpdateForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.devicesComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.currentFileLabel = new System.Windows.Forms.Label();
			this.skinProgressBar1 = new CCWin.SkinControl.SkinProgressBar();
			this.searchSkinButton = new CCWin.SkinControl.SkinButton();
			this.connectSkinButton = new CCWin.SkinControl.SkinButton();
			this.updateSkinButton = new CCWin.SkinControl.SkinButton();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(46, 85);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "下载总进度：";
			// 
			// devicesComboBox
			// 
			this.devicesComboBox.FormattingEnabled = true;
			this.devicesComboBox.Location = new System.Drawing.Point(158, 33);
			this.devicesComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.devicesComboBox.Name = "devicesComboBox";
			this.devicesComboBox.Size = new System.Drawing.Size(179, 20);
			this.devicesComboBox.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(46, 126);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(89, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "当前下载文件：";
			// 
			// currentFileLabel
			// 
			this.currentFileLabel.AutoSize = true;
			this.currentFileLabel.Location = new System.Drawing.Point(135, 126);
			this.currentFileLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.currentFileLabel.Name = "currentFileLabel";
			this.currentFileLabel.Size = new System.Drawing.Size(0, 12);
			this.currentFileLabel.TabIndex = 6;
			// 
			// skinProgressBar1
			// 
			this.skinProgressBar1.Back = null;
			this.skinProgressBar1.BackColor = System.Drawing.Color.Transparent;
			this.skinProgressBar1.BarBack = null;
			this.skinProgressBar1.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.skinProgressBar1.ForeColor = System.Drawing.Color.Red;
			this.skinProgressBar1.Location = new System.Drawing.Point(137, 80);
			this.skinProgressBar1.Name = "skinProgressBar1";
			this.skinProgressBar1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.skinProgressBar1.Size = new System.Drawing.Size(404, 23);
			this.skinProgressBar1.TabIndex = 7;
			// 
			// searchSkinButton
			// 
			this.searchSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.searchSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.searchSkinButton.BorderColor = System.Drawing.Color.Black;
			this.searchSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.searchSkinButton.DownBack = null;
			this.searchSkinButton.Location = new System.Drawing.Point(47, 28);
			this.searchSkinButton.MouseBack = null;
			this.searchSkinButton.Name = "searchSkinButton";
			this.searchSkinButton.NormlBack = null;
			this.searchSkinButton.Size = new System.Drawing.Size(76, 31);
			this.searchSkinButton.TabIndex = 8;
			this.searchSkinButton.Text = "搜索设备";
			this.searchSkinButton.UseVisualStyleBackColor = false;
			this.searchSkinButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// connectSkinButton
			// 
			this.connectSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.connectSkinButton.BaseColor = System.Drawing.Color.SeaGreen;
			this.connectSkinButton.BorderColor = System.Drawing.Color.Black;
			this.connectSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.connectSkinButton.DownBack = null;
			this.connectSkinButton.Enabled = false;
			this.connectSkinButton.Location = new System.Drawing.Point(375, 28);
			this.connectSkinButton.MouseBack = null;
			this.connectSkinButton.Name = "connectSkinButton";
			this.connectSkinButton.NormlBack = null;
			this.connectSkinButton.Size = new System.Drawing.Size(76, 31);
			this.connectSkinButton.TabIndex = 8;
			this.connectSkinButton.Text = "连接设备";
			this.connectSkinButton.UseVisualStyleBackColor = false;
			this.connectSkinButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// updateSkinButton
			// 
			this.updateSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.updateSkinButton.BaseColor = System.Drawing.Color.Tan;
			this.updateSkinButton.BorderColor = System.Drawing.Color.Black;
			this.updateSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.updateSkinButton.DownBack = null;
			this.updateSkinButton.Enabled = false;
			this.updateSkinButton.Location = new System.Drawing.Point(465, 28);
			this.updateSkinButton.MouseBack = null;
			this.updateSkinButton.Name = "updateSkinButton";
			this.updateSkinButton.NormlBack = null;
			this.updateSkinButton.Size = new System.Drawing.Size(76, 31);
			this.updateSkinButton.TabIndex = 8;
			this.updateSkinButton.Text = "下载数据";
			this.updateSkinButton.UseVisualStyleBackColor = false;
			this.updateSkinButton.Click += new System.EventHandler(this.UpdateButton_Click);
			// 
			// UpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(592, 175);
			this.Controls.Add(this.updateSkinButton);
			this.Controls.Add(this.connectSkinButton);
			this.Controls.Add(this.searchSkinButton);
			this.Controls.Add(this.skinProgressBar1);
			this.Controls.Add(this.currentFileLabel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.devicesComboBox);
			this.Controls.Add(this.label1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateForm";
			this.Text = "下载数据到设备";
			this.Load += new System.EventHandler(this.UpdateForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox devicesComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label currentFileLabel;
		private CCWin.SkinControl.SkinProgressBar skinProgressBar1;
		private CCWin.SkinControl.SkinButton searchSkinButton;
		private CCWin.SkinControl.SkinButton connectSkinButton;
		private CCWin.SkinControl.SkinButton updateSkinButton;
	}
}