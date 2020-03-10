namespace LightController.MyForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HardwareUpdateForm));
			this.ipsComboBox = new System.Windows.Forms.ComboBox();
			this.skinTabControl = new CCWin.SkinControl.SkinTabControl();
			this.networkTab = new CCWin.SkinControl.SkinTabPage();
			this.networkPanel = new System.Windows.Forms.Panel();
			this.networkdUpdateButton = new System.Windows.Forms.Button();
			this.networkSearchButton = new System.Windows.Forms.Button();
			this.getLocalIPsButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.networkSkinProgressBar = new CCWin.SkinControl.SkinProgressBar();
			this.localIPLabel = new System.Windows.Forms.Label();
			this.localIPsComboBox = new System.Windows.Forms.ComboBox();
			this.skinTabPage2 = new CCWin.SkinControl.SkinTabPage();
			this.comPanel = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.comSkinProgressBar = new CCWin.SkinControl.SkinProgressBar();
			this.comNameLabel = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.comComboBox = new System.Windows.Forms.ComboBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.filePathLabel = new System.Windows.Forms.Label();
			this.fileOpenButton = new System.Windows.Forms.Button();
			this.comSearchButton = new System.Windows.Forms.Button();
			this.comConnectButton = new System.Windows.Forms.Button();
			this.comUpdateButton = new System.Windows.Forms.Button();
			this.skinTabControl.SuspendLayout();
			this.networkTab.SuspendLayout();
			this.networkPanel.SuspendLayout();
			this.skinTabPage2.SuspendLayout();
			this.comPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ipsComboBox
			// 
			this.ipsComboBox.Enabled = false;
			this.ipsComboBox.FormattingEnabled = true;
			this.ipsComboBox.Location = new System.Drawing.Point(163, 80);
			this.ipsComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.ipsComboBox.Name = "ipsComboBox";
			this.ipsComboBox.Size = new System.Drawing.Size(281, 20);
			this.ipsComboBox.TabIndex = 5;
			this.ipsComboBox.SelectedIndexChanged += new System.EventHandler(this.networkDevicesComboBox_SelectedIndexChanged);
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
			this.skinTabControl.SelectedIndex = 1;
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
			this.networkPanel.Controls.Add(this.networkdUpdateButton);
			this.networkPanel.Controls.Add(this.networkSearchButton);
			this.networkPanel.Controls.Add(this.getLocalIPsButton);
			this.networkPanel.Controls.Add(this.label1);
			this.networkPanel.Controls.Add(this.networkSkinProgressBar);
			this.networkPanel.Controls.Add(this.localIPLabel);
			this.networkPanel.Controls.Add(this.localIPsComboBox);
			this.networkPanel.Controls.Add(this.ipsComboBox);
			this.networkPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.networkPanel.Location = new System.Drawing.Point(0, 0);
			this.networkPanel.Name = "networkPanel";
			this.networkPanel.Size = new System.Drawing.Size(564, 184);
			this.networkPanel.TabIndex = 9;
			// 
			// networkdUpdateButton
			// 
			this.networkdUpdateButton.Enabled = false;
			this.networkdUpdateButton.Location = new System.Drawing.Point(463, 72);
			this.networkdUpdateButton.Name = "networkdUpdateButton";
			this.networkdUpdateButton.Size = new System.Drawing.Size(76, 32);
			this.networkdUpdateButton.TabIndex = 16;
			this.networkdUpdateButton.Text = "升级";
			this.networkdUpdateButton.UseVisualStyleBackColor = true;
			this.networkdUpdateButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// networkSearchButton
			// 
			this.networkSearchButton.Location = new System.Drawing.Point(35, 73);
			this.networkSearchButton.Name = "networkSearchButton";
			this.networkSearchButton.Size = new System.Drawing.Size(108, 31);
			this.networkSearchButton.TabIndex = 15;
			this.networkSearchButton.Text = "搜索网络设备";
			this.networkSearchButton.UseVisualStyleBackColor = true;
			this.networkSearchButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// getLocalIPsButton
			// 
			this.getLocalIPsButton.Location = new System.Drawing.Point(35, 26);
			this.getLocalIPsButton.Name = "getLocalIPsButton";
			this.getLocalIPsButton.Size = new System.Drawing.Size(108, 31);
			this.getLocalIPsButton.TabIndex = 14;
			this.getLocalIPsButton.Text = "获取本机IP列表";
			this.getLocalIPsButton.UseVisualStyleBackColor = true;
			this.getLocalIPsButton.Click += new System.EventHandler(this.getLocalIPsButton_Click);
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
			this.networkSkinProgressBar.Tag = "9999";
			// 
			// localIPLabel
			// 
			this.localIPLabel.AutoSize = true;
			this.localIPLabel.Location = new System.Drawing.Point(506, 40);
			this.localIPLabel.Name = "localIPLabel";
			this.localIPLabel.Size = new System.Drawing.Size(0, 12);
			this.localIPLabel.TabIndex = 9;
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
			this.comPanel.Controls.Add(this.comUpdateButton);
			this.comPanel.Controls.Add(this.comConnectButton);
			this.comPanel.Controls.Add(this.comSearchButton);
			this.comPanel.Controls.Add(this.label4);
			this.comPanel.Controls.Add(this.comSkinProgressBar);
			this.comPanel.Controls.Add(this.comNameLabel);
			this.comPanel.Controls.Add(this.label6);
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
			// filePathLabel
			// 
			this.filePathLabel.Location = new System.Drawing.Point(152, 19);
			this.filePathLabel.Name = "filePathLabel";
			this.filePathLabel.Size = new System.Drawing.Size(387, 33);
			this.filePathLabel.TabIndex = 11;
			// 
			// fileOpenButton
			// 
			this.fileOpenButton.Location = new System.Drawing.Point(36, 19);
			this.fileOpenButton.Name = "fileOpenButton";
			this.fileOpenButton.Size = new System.Drawing.Size(86, 33);
			this.fileOpenButton.TabIndex = 12;
			this.fileOpenButton.Text = "选择升级文件";
			this.fileOpenButton.UseVisualStyleBackColor = true;
			this.fileOpenButton.Click += new System.EventHandler(this.fileOpenButton_Click);
			// 
			// comSearchButton
			// 
			this.comSearchButton.Location = new System.Drawing.Point(33, 46);
			this.comSearchButton.Name = "comSearchButton";
			this.comSearchButton.Size = new System.Drawing.Size(88, 31);
			this.comSearchButton.TabIndex = 16;
			this.comSearchButton.Text = "搜索串口设备";
			this.comSearchButton.UseVisualStyleBackColor = true;
			// 
			// comConnectButton
			// 
			this.comConnectButton.Enabled = false;
			this.comConnectButton.Location = new System.Drawing.Point(369, 46);
			this.comConnectButton.Name = "comConnectButton";
			this.comConnectButton.Size = new System.Drawing.Size(75, 31);
			this.comConnectButton.TabIndex = 17;
			this.comConnectButton.Text = "打开串口";
			this.comConnectButton.UseVisualStyleBackColor = true;
			this.comConnectButton.Click += new System.EventHandler(this.comConnectButton_Click);
			// 
			// comUpdateButton
			// 
			this.comUpdateButton.Enabled = false;
			this.comUpdateButton.Location = new System.Drawing.Point(464, 46);
			this.comUpdateButton.Name = "comUpdateButton";
			this.comUpdateButton.Size = new System.Drawing.Size(75, 31);
			this.comUpdateButton.TabIndex = 17;
			this.comUpdateButton.Text = "升级";
			this.comUpdateButton.UseVisualStyleBackColor = true;
			this.comUpdateButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// HardwareUpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.ClientSize = new System.Drawing.Size(564, 296);
			this.Controls.Add(this.fileOpenButton);
			this.Controls.Add(this.filePathLabel);
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
		private System.Windows.Forms.ComboBox ipsComboBox;
		private CCWin.SkinControl.SkinTabControl skinTabControl;
		private CCWin.SkinControl.SkinTabPage networkTab;
		private System.Windows.Forms.Panel networkPanel;
		private CCWin.SkinControl.SkinTabPage skinTabPage2;
		private System.Windows.Forms.Panel comPanel;
		private System.Windows.Forms.ComboBox comComboBox;
		private System.Windows.Forms.ComboBox localIPsComboBox;
		private System.Windows.Forms.Label localIPLabel;
		private System.Windows.Forms.Label comNameLabel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Label filePathLabel;
		private System.Windows.Forms.Label label1;
		private CCWin.SkinControl.SkinProgressBar networkSkinProgressBar;
		private System.Windows.Forms.Label label4;
		private CCWin.SkinControl.SkinProgressBar comSkinProgressBar;
		private System.Windows.Forms.Button fileOpenButton;
		private System.Windows.Forms.Button getLocalIPsButton;
		private System.Windows.Forms.Button networkSearchButton;
		private System.Windows.Forms.Button networkdUpdateButton;
		private System.Windows.Forms.Button comSearchButton;
		private System.Windows.Forms.Button comUpdateButton;
		private System.Windows.Forms.Button comConnectButton;
	}
}