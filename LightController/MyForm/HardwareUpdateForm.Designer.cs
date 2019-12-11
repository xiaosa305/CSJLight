﻿namespace LightController.MyForm
{
	partial class HardwareUpdateForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HardwareUpdateForm));
			this.networkDevicesComboBox = new System.Windows.Forms.ComboBox();
			this.networkSearchSkinButton = new CCWin.SkinControl.SkinButton();
			this.networkdUpdateSkinButton = new CCWin.SkinControl.SkinButton();
			this.skinTabControl = new CCWin.SkinControl.SkinTabControl();
			this.networkTab = new CCWin.SkinControl.SkinTabPage();
			this.networkPanel = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.networkSkinProgressBar = new CCWin.SkinControl.SkinProgressBar();
			this.localIPLabel = new System.Windows.Forms.Label();
			this.getLocalIPsSkinButton = new CCWin.SkinControl.SkinButton();
			this.localIPsComboBox = new System.Windows.Forms.ComboBox();
			this.skinTabPage2 = new CCWin.SkinControl.SkinTabPage();
			this.comPanel = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.comSkinProgressBar = new CCWin.SkinControl.SkinProgressBar();
			this.comNameLabel = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.comSearchSkinButton = new CCWin.SkinControl.SkinButton();
			this.comUpdateSkinButton = new CCWin.SkinControl.SkinButton();
			this.comChooseSkinButton = new CCWin.SkinControl.SkinButton();
			this.comComboBox = new System.Windows.Forms.ComboBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.fileOpenSkinButton = new CCWin.SkinControl.SkinButton();
			this.filePathLabel = new System.Windows.Forms.Label();
			this.skinTabControl.SuspendLayout();
			this.networkTab.SuspendLayout();
			this.networkPanel.SuspendLayout();
			this.skinTabPage2.SuspendLayout();
			this.comPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// networkDevicesComboBox
			// 
			this.networkDevicesComboBox.Enabled = false;
			this.networkDevicesComboBox.FormattingEnabled = true;
			this.networkDevicesComboBox.Location = new System.Drawing.Point(163, 80);
			this.networkDevicesComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.networkDevicesComboBox.Name = "networkDevicesComboBox";
			this.networkDevicesComboBox.Size = new System.Drawing.Size(281, 20);
			this.networkDevicesComboBox.TabIndex = 5;
			this.networkDevicesComboBox.SelectedIndexChanged += new System.EventHandler(this.networkDevicesComboBox_SelectedIndexChanged);
			// 
			// networkSearchSkinButton
			// 
			this.networkSearchSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.networkSearchSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.networkSearchSkinButton.BorderColor = System.Drawing.Color.Black;
			this.networkSearchSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.networkSearchSkinButton.DownBack = null;
			this.networkSearchSkinButton.Enabled = false;
			this.networkSearchSkinButton.Location = new System.Drawing.Point(36, 74);
			this.networkSearchSkinButton.MouseBack = null;
			this.networkSearchSkinButton.Name = "networkSearchSkinButton";
			this.networkSearchSkinButton.NormlBack = null;
			this.networkSearchSkinButton.Size = new System.Drawing.Size(107, 31);
			this.networkSearchSkinButton.TabIndex = 8;
			this.networkSearchSkinButton.Text = "搜索网络设备";
			this.networkSearchSkinButton.UseVisualStyleBackColor = false;
			this.networkSearchSkinButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// networkdUpdateSkinButton
			// 
			this.networkdUpdateSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.networkdUpdateSkinButton.BaseColor = System.Drawing.Color.Tan;
			this.networkdUpdateSkinButton.BorderColor = System.Drawing.Color.Black;
			this.networkdUpdateSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.networkdUpdateSkinButton.DownBack = null;
			this.networkdUpdateSkinButton.Enabled = false;
			this.networkdUpdateSkinButton.Location = new System.Drawing.Point(463, 73);
			this.networkdUpdateSkinButton.MouseBack = null;
			this.networkdUpdateSkinButton.Name = "networkdUpdateSkinButton";
			this.networkdUpdateSkinButton.NormlBack = null;
			this.networkdUpdateSkinButton.Size = new System.Drawing.Size(76, 32);
			this.networkdUpdateSkinButton.TabIndex = 8;
			this.networkdUpdateSkinButton.Text = "升级";
			this.networkdUpdateSkinButton.UseVisualStyleBackColor = false;
			this.networkdUpdateSkinButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// skinTabControl
			// 
			this.skinTabControl.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
			this.skinTabControl.BackColor = System.Drawing.SystemColors.Control;
			this.skinTabControl.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
			this.skinTabControl.Controls.Add(this.networkTab);
			this.skinTabControl.Controls.Add(this.skinTabPage2);
			this.skinTabControl.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinTabControl.HeadBack = null;
			this.skinTabControl.ImgTxtOffset = new System.Drawing.Point(0, 0);
			this.skinTabControl.ItemSize = new System.Drawing.Size(70, 36);
			this.skinTabControl.Location = new System.Drawing.Point(0, 76);
			this.skinTabControl.Name = "skinTabControl";
			this.skinTabControl.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl.PageArrowDown")));
			this.skinTabControl.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl.PageArrowHover")));
			this.skinTabControl.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl.PageCloseHover")));
			this.skinTabControl.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("skinTabControl.PageCloseNormal")));
			this.skinTabControl.PageDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl.PageDown")));
			this.skinTabControl.PageHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl.PageHover")));
			this.skinTabControl.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
			this.skinTabControl.PageNorml = null;
			this.skinTabControl.SelectedIndex = 0;
			this.skinTabControl.Size = new System.Drawing.Size(564, 220);
			this.skinTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.skinTabControl.TabIndex = 9;
			// 
			// networkTab
			// 
			this.networkTab.BackColor = System.Drawing.Color.White;
			this.networkTab.Controls.Add(this.networkPanel);
			this.networkTab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.networkTab.Location = new System.Drawing.Point(0, 36);
			this.networkTab.Name = "networkTab";
			this.networkTab.Size = new System.Drawing.Size(564, 184);
			this.networkTab.TabIndex = 0;
			this.networkTab.TabItemImage = null;
			this.networkTab.Text = "网络模式";
			// 
			// networkPanel
			// 
			this.networkPanel.Controls.Add(this.label1);
			this.networkPanel.Controls.Add(this.networkSkinProgressBar);
			this.networkPanel.Controls.Add(this.localIPLabel);
			this.networkPanel.Controls.Add(this.getLocalIPsSkinButton);
			this.networkPanel.Controls.Add(this.networkSearchSkinButton);
			this.networkPanel.Controls.Add(this.networkdUpdateSkinButton);
			this.networkPanel.Controls.Add(this.localIPsComboBox);
			this.networkPanel.Controls.Add(this.networkDevicesComboBox);
			this.networkPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.networkPanel.Location = new System.Drawing.Point(0, 0);
			this.networkPanel.Name = "networkPanel";
			this.networkPanel.Size = new System.Drawing.Size(564, 184);
			this.networkPanel.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(36, 130);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 12);
			this.label1.TabIndex = 10;
			this.label1.Text = "升级总进度：";
			// 
			// networkSkinProgressBar
			// 
			this.networkSkinProgressBar.Back = null;
			this.networkSkinProgressBar.BackColor = System.Drawing.Color.Transparent;
			this.networkSkinProgressBar.BarBack = null;
			this.networkSkinProgressBar.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.networkSkinProgressBar.ForeColor = System.Drawing.Color.Red;
			this.networkSkinProgressBar.Location = new System.Drawing.Point(126, 125);
			this.networkSkinProgressBar.Name = "networkSkinProgressBar";
			this.networkSkinProgressBar.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.networkSkinProgressBar.Size = new System.Drawing.Size(413, 23);
			this.networkSkinProgressBar.TabIndex = 13;
			// 
			// localIPLabel
			// 
			this.localIPLabel.AutoSize = true;
			this.localIPLabel.Location = new System.Drawing.Point(506, 40);
			this.localIPLabel.Name = "localIPLabel";
			this.localIPLabel.Size = new System.Drawing.Size(0, 12);
			this.localIPLabel.TabIndex = 9;
			// 
			// getLocalIPsSkinButton
			// 
			this.getLocalIPsSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.getLocalIPsSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.getLocalIPsSkinButton.BorderColor = System.Drawing.Color.Black;
			this.getLocalIPsSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.getLocalIPsSkinButton.DownBack = null;
			this.getLocalIPsSkinButton.Location = new System.Drawing.Point(36, 26);
			this.getLocalIPsSkinButton.MouseBack = null;
			this.getLocalIPsSkinButton.Name = "getLocalIPsSkinButton";
			this.getLocalIPsSkinButton.NormlBack = null;
			this.getLocalIPsSkinButton.Size = new System.Drawing.Size(108, 31);
			this.getLocalIPsSkinButton.TabIndex = 8;
			this.getLocalIPsSkinButton.Text = "获取本机ip列表";
			this.getLocalIPsSkinButton.UseVisualStyleBackColor = false;
			this.getLocalIPsSkinButton.Click += new System.EventHandler(this.getLocalIPsSkinButton_Click);
			// 
			// localIPsComboBox
			// 
			this.localIPsComboBox.Enabled = false;
			this.localIPsComboBox.FormattingEnabled = true;
			this.localIPsComboBox.Location = new System.Drawing.Point(163, 32);
			this.localIPsComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.localIPsComboBox.Name = "localIPsComboBox";
			this.localIPsComboBox.Size = new System.Drawing.Size(281, 20);
			this.localIPsComboBox.TabIndex = 5;
			this.localIPsComboBox.SelectedIndexChanged += new System.EventHandler(this.localIPsComboBox_SelectedIndexChanged);
			// 
			// skinTabPage2
			// 
			this.skinTabPage2.BackColor = System.Drawing.Color.White;
			this.skinTabPage2.Controls.Add(this.comPanel);
			this.skinTabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skinTabPage2.Location = new System.Drawing.Point(0, 36);
			this.skinTabPage2.Name = "skinTabPage2";
			this.skinTabPage2.Size = new System.Drawing.Size(564, 184);
			this.skinTabPage2.TabIndex = 1;
			this.skinTabPage2.TabItemImage = null;
			this.skinTabPage2.Text = "串口模式";
			// 
			// comPanel
			// 
			this.comPanel.Controls.Add(this.label4);
			this.comPanel.Controls.Add(this.comSkinProgressBar);
			this.comPanel.Controls.Add(this.comNameLabel);
			this.comPanel.Controls.Add(this.label6);
			this.comPanel.Controls.Add(this.comSearchSkinButton);
			this.comPanel.Controls.Add(this.comUpdateSkinButton);
			this.comPanel.Controls.Add(this.comChooseSkinButton);
			this.comPanel.Controls.Add(this.comComboBox);
			this.comPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comPanel.Location = new System.Drawing.Point(0, 0);
			this.comPanel.Name = "comPanel";
			this.comPanel.Size = new System.Drawing.Size(564, 184);
			this.comPanel.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(36, 112);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77, 12);
			this.label4.TabIndex = 12;
			this.label4.Text = "升级总进度：";
			// 
			// comSkinProgressBar
			// 
			this.comSkinProgressBar.Back = null;
			this.comSkinProgressBar.BackColor = System.Drawing.Color.Transparent;
			this.comSkinProgressBar.BarBack = null;
			this.comSkinProgressBar.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.comSkinProgressBar.ForeColor = System.Drawing.Color.Red;
			this.comSkinProgressBar.Location = new System.Drawing.Point(135, 107);
			this.comSkinProgressBar.Name = "comSkinProgressBar";
			this.comSkinProgressBar.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.comSkinProgressBar.Size = new System.Drawing.Size(404, 23);
			this.comSkinProgressBar.TabIndex = 15;
			// 
			// comNameLabel
			// 
			this.comNameLabel.AutoSize = true;
			this.comNameLabel.Location = new System.Drawing.Point(285, 64);
			this.comNameLabel.Name = "comNameLabel";
			this.comNameLabel.Size = new System.Drawing.Size(0, 12);
			this.comNameLabel.TabIndex = 10;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(278, 46);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(65, 12);
			this.label6.TabIndex = 11;
			this.label6.Text = "已选串口：";
			// 
			// comSearchSkinButton
			// 
			this.comSearchSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.comSearchSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.comSearchSkinButton.BorderColor = System.Drawing.Color.Black;
			this.comSearchSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.comSearchSkinButton.DownBack = null;
			this.comSearchSkinButton.Location = new System.Drawing.Point(36, 46);
			this.comSearchSkinButton.MouseBack = null;
			this.comSearchSkinButton.Name = "comSearchSkinButton";
			this.comSearchSkinButton.NormlBack = null;
			this.comSearchSkinButton.Size = new System.Drawing.Size(88, 31);
			this.comSearchSkinButton.TabIndex = 8;
			this.comSearchSkinButton.Text = "搜索串口设备";
			this.comSearchSkinButton.UseVisualStyleBackColor = false;
			this.comSearchSkinButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// comUpdateSkinButton
			// 
			this.comUpdateSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.comUpdateSkinButton.BaseColor = System.Drawing.Color.Tan;
			this.comUpdateSkinButton.BorderColor = System.Drawing.Color.Black;
			this.comUpdateSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.comUpdateSkinButton.DownBack = null;
			this.comUpdateSkinButton.Enabled = false;
			this.comUpdateSkinButton.Location = new System.Drawing.Point(463, 46);
			this.comUpdateSkinButton.MouseBack = null;
			this.comUpdateSkinButton.Name = "comUpdateSkinButton";
			this.comUpdateSkinButton.NormlBack = null;
			this.comUpdateSkinButton.Size = new System.Drawing.Size(76, 31);
			this.comUpdateSkinButton.TabIndex = 8;
			this.comUpdateSkinButton.Text = "升级";
			this.comUpdateSkinButton.UseVisualStyleBackColor = false;
			this.comUpdateSkinButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// comChooseSkinButton
			// 
			this.comChooseSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.comChooseSkinButton.BaseColor = System.Drawing.Color.SeaGreen;
			this.comChooseSkinButton.BorderColor = System.Drawing.Color.Black;
			this.comChooseSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.comChooseSkinButton.DownBack = null;
			this.comChooseSkinButton.Enabled = false;
			this.comChooseSkinButton.Location = new System.Drawing.Point(369, 46);
			this.comChooseSkinButton.MouseBack = null;
			this.comChooseSkinButton.Name = "comChooseSkinButton";
			this.comChooseSkinButton.NormlBack = null;
			this.comChooseSkinButton.Size = new System.Drawing.Size(76, 31);
			this.comChooseSkinButton.TabIndex = 8;
			this.comChooseSkinButton.Text = "打开串口";
			this.comChooseSkinButton.UseVisualStyleBackColor = false;
			this.comChooseSkinButton.Click += new System.EventHandler(this.comChooseButton_Click);
			// 
			// comComboBox
			// 
			this.comComboBox.Enabled = false;
			this.comComboBox.FormattingEnabled = true;
			this.comComboBox.Location = new System.Drawing.Point(154, 52);
			this.comComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.comComboBox.Name = "comComboBox";
			this.comComboBox.Size = new System.Drawing.Size(96, 20);
			this.comComboBox.TabIndex = 5;
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog";
			this.openFileDialog.Filter = "*.xbin(自定义二进制文件)|*.xbin";
			this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
			// 
			// fileOpenSkinButton
			// 
			this.fileOpenSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.fileOpenSkinButton.BaseColor = System.Drawing.Color.Silver;
			this.fileOpenSkinButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.fileOpenSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.fileOpenSkinButton.DownBack = null;
			this.fileOpenSkinButton.Location = new System.Drawing.Point(36, 19);
			this.fileOpenSkinButton.MouseBack = null;
			this.fileOpenSkinButton.Name = "fileOpenSkinButton";
			this.fileOpenSkinButton.NormlBack = null;
			this.fileOpenSkinButton.Size = new System.Drawing.Size(86, 33);
			this.fileOpenSkinButton.TabIndex = 10;
			this.fileOpenSkinButton.Text = "选择升级文件";
			this.fileOpenSkinButton.UseVisualStyleBackColor = false;
			this.fileOpenSkinButton.Click += new System.EventHandler(this.fileOpenSkinButton_Click);
			// 
			// filePathLabel
			// 
			this.filePathLabel.Location = new System.Drawing.Point(152, 19);
			this.filePathLabel.Name = "filePathLabel";
			this.filePathLabel.Size = new System.Drawing.Size(387, 33);
			this.filePathLabel.TabIndex = 11;
			// 
			// HardwareUpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.ClientSize = new System.Drawing.Size(564, 296);
			this.Controls.Add(this.filePathLabel);
			this.Controls.Add(this.fileOpenSkinButton);
			this.Controls.Add(this.skinTabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HelpButton = true;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HardwareUpdateForm";
			this.Text = "手动更新硬件";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.HardwareUpdateForm_HelpButtonClicked);
			this.Load += new System.EventHandler(this.UpdateForm_Load);
			this.skinTabControl.ResumeLayout(false);
			this.networkTab.ResumeLayout(false);
			this.networkPanel.ResumeLayout(false);
			this.networkPanel.PerformLayout();
			this.skinTabPage2.ResumeLayout(false);
			this.comPanel.ResumeLayout(false);
			this.comPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ComboBox networkDevicesComboBox;
		private CCWin.SkinControl.SkinButton networkSearchSkinButton;
		private CCWin.SkinControl.SkinButton networkdUpdateSkinButton;
		private CCWin.SkinControl.SkinTabControl skinTabControl;
		private CCWin.SkinControl.SkinTabPage networkTab;
		private System.Windows.Forms.Panel networkPanel;
		private CCWin.SkinControl.SkinTabPage skinTabPage2;
		private System.Windows.Forms.Panel comPanel;
		private CCWin.SkinControl.SkinButton comSearchSkinButton;
		private CCWin.SkinControl.SkinButton comUpdateSkinButton;
		private CCWin.SkinControl.SkinButton comChooseSkinButton;
		private System.Windows.Forms.ComboBox comComboBox;
		private CCWin.SkinControl.SkinButton getLocalIPsSkinButton;
		private System.Windows.Forms.ComboBox localIPsComboBox;
		private System.Windows.Forms.Label localIPLabel;
		private System.Windows.Forms.Label comNameLabel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private CCWin.SkinControl.SkinButton fileOpenSkinButton;
		private System.Windows.Forms.Label filePathLabel;
		private System.Windows.Forms.Label label1;
		private CCWin.SkinControl.SkinProgressBar networkSkinProgressBar;
		private System.Windows.Forms.Label label4;
		private CCWin.SkinControl.SkinProgressBar comSkinProgressBar;
	}
}