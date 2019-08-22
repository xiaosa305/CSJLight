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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
			this.label1 = new System.Windows.Forms.Label();
			this.networkDevicesComboBox = new System.Windows.Forms.ComboBox();
			this.networkCurrentFileLabel = new System.Windows.Forms.Label();
			this.currentFileLabel = new System.Windows.Forms.Label();
			this.networkSkinProgressBar = new CCWin.SkinControl.SkinProgressBar();
			this.networkSearchSkinButton = new CCWin.SkinControl.SkinButton();
			this.networkConnectSkinButton = new CCWin.SkinControl.SkinButton();
			this.networkdUpdateSkinButton = new CCWin.SkinControl.SkinButton();
			this.skinTabControl = new CCWin.SkinControl.SkinTabControl();
			this.networkTab = new CCWin.SkinControl.SkinTabPage();
			this.networkPanel = new System.Windows.Forms.Panel();
			this.localIPLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.getLocalIPsSkinButton = new CCWin.SkinControl.SkinButton();
			this.setLocalIPSkinButton = new CCWin.SkinControl.SkinButton();
			this.localIPSComboBox = new System.Windows.Forms.ComboBox();
			this.skinTabPage2 = new CCWin.SkinControl.SkinTabPage();
			this.comPanel = new System.Windows.Forms.Panel();
			this.comSearchSkinButton = new CCWin.SkinControl.SkinButton();
			this.comUpdateSkinButton = new CCWin.SkinControl.SkinButton();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comSkinProgressBar = new CCWin.SkinControl.SkinProgressBar();
			this.comConnectSkinButton = new CCWin.SkinControl.SkinButton();
			this.comCurrentFileLabel = new System.Windows.Forms.Label();
			this.comComboBox = new System.Windows.Forms.ComboBox();
			this.skinTabControl.SuspendLayout();
			this.networkTab.SuspendLayout();
			this.networkPanel.SuspendLayout();
			this.skinTabPage2.SuspendLayout();
			this.comPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(36, 137);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "下载总进度：";
			// 
			// networkDevicesComboBox
			// 
			this.networkDevicesComboBox.Enabled = false;
			this.networkDevicesComboBox.FormattingEnabled = true;
			this.networkDevicesComboBox.Location = new System.Drawing.Point(164, 85);
			this.networkDevicesComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.networkDevicesComboBox.Name = "networkDevicesComboBox";
			this.networkDevicesComboBox.Size = new System.Drawing.Size(171, 20);
			this.networkDevicesComboBox.TabIndex = 5;
			// 
			// networkCurrentFileLabel
			// 
			this.networkCurrentFileLabel.AutoSize = true;
			this.networkCurrentFileLabel.Location = new System.Drawing.Point(36, 178);
			this.networkCurrentFileLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.networkCurrentFileLabel.Name = "networkCurrentFileLabel";
			this.networkCurrentFileLabel.Size = new System.Drawing.Size(89, 12);
			this.networkCurrentFileLabel.TabIndex = 6;
			this.networkCurrentFileLabel.Text = "当前下载文件：";
			// 
			// currentFileLabel
			// 
			this.currentFileLabel.AutoSize = true;
			this.currentFileLabel.Location = new System.Drawing.Point(125, 178);
			this.currentFileLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.currentFileLabel.Name = "currentFileLabel";
			this.currentFileLabel.Size = new System.Drawing.Size(0, 12);
			this.currentFileLabel.TabIndex = 6;
			// 
			// networkSkinProgressBar
			// 
			this.networkSkinProgressBar.Back = null;
			this.networkSkinProgressBar.BackColor = System.Drawing.Color.Transparent;
			this.networkSkinProgressBar.BarBack = null;
			this.networkSkinProgressBar.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.networkSkinProgressBar.ForeColor = System.Drawing.Color.Red;
			this.networkSkinProgressBar.Location = new System.Drawing.Point(126, 132);
			this.networkSkinProgressBar.Name = "networkSkinProgressBar";
			this.networkSkinProgressBar.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.networkSkinProgressBar.Size = new System.Drawing.Size(404, 23);
			this.networkSkinProgressBar.TabIndex = 7;
			// 
			// networkSearchSkinButton
			// 
			this.networkSearchSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.networkSearchSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.networkSearchSkinButton.BorderColor = System.Drawing.Color.Black;
			this.networkSearchSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.networkSearchSkinButton.DownBack = null;
			this.networkSearchSkinButton.Enabled = false;
			this.networkSearchSkinButton.Location = new System.Drawing.Point(37, 79);
			this.networkSearchSkinButton.MouseBack = null;
			this.networkSearchSkinButton.Name = "networkSearchSkinButton";
			this.networkSearchSkinButton.NormlBack = null;
			this.networkSearchSkinButton.Size = new System.Drawing.Size(107, 31);
			this.networkSearchSkinButton.TabIndex = 8;
			this.networkSearchSkinButton.Text = "搜索网络设备";
			this.networkSearchSkinButton.UseVisualStyleBackColor = false;
			this.networkSearchSkinButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// networkConnectSkinButton
			// 
			this.networkConnectSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.networkConnectSkinButton.BaseColor = System.Drawing.Color.SeaGreen;
			this.networkConnectSkinButton.BorderColor = System.Drawing.Color.Black;
			this.networkConnectSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.networkConnectSkinButton.DownBack = null;
			this.networkConnectSkinButton.Enabled = false;
			this.networkConnectSkinButton.Location = new System.Drawing.Point(353, 80);
			this.networkConnectSkinButton.MouseBack = null;
			this.networkConnectSkinButton.Name = "networkConnectSkinButton";
			this.networkConnectSkinButton.NormlBack = null;
			this.networkConnectSkinButton.Size = new System.Drawing.Size(86, 31);
			this.networkConnectSkinButton.TabIndex = 8;
			this.networkConnectSkinButton.Text = "选择网络设备";
			this.networkConnectSkinButton.UseVisualStyleBackColor = false;
			this.networkConnectSkinButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// networkdUpdateSkinButton
			// 
			this.networkdUpdateSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.networkdUpdateSkinButton.BaseColor = System.Drawing.Color.Tan;
			this.networkdUpdateSkinButton.BorderColor = System.Drawing.Color.Black;
			this.networkdUpdateSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.networkdUpdateSkinButton.DownBack = null;
			this.networkdUpdateSkinButton.Enabled = false;
			this.networkdUpdateSkinButton.Location = new System.Drawing.Point(454, 80);
			this.networkdUpdateSkinButton.MouseBack = null;
			this.networkdUpdateSkinButton.Name = "networkdUpdateSkinButton";
			this.networkdUpdateSkinButton.NormlBack = null;
			this.networkdUpdateSkinButton.Size = new System.Drawing.Size(76, 31);
			this.networkdUpdateSkinButton.TabIndex = 8;
			this.networkdUpdateSkinButton.Text = "下载数据";
			this.networkdUpdateSkinButton.UseVisualStyleBackColor = false;
			this.networkdUpdateSkinButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// skinTabControl
			// 
			this.skinTabControl.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
			this.skinTabControl.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
			this.skinTabControl.Controls.Add(this.networkTab);
			this.skinTabControl.Controls.Add(this.skinTabPage2);
			this.skinTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skinTabControl.HeadBack = null;
			this.skinTabControl.ImgTxtOffset = new System.Drawing.Point(0, 0);
			this.skinTabControl.ItemSize = new System.Drawing.Size(70, 36);
			this.skinTabControl.Location = new System.Drawing.Point(0, 0);
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
			this.skinTabControl.Size = new System.Drawing.Size(576, 265);
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
			this.networkTab.Size = new System.Drawing.Size(576, 229);
			this.networkTab.TabIndex = 0;
			this.networkTab.TabItemImage = null;
			this.networkTab.Text = "网络模式";
			// 
			// networkPanel
			// 
			this.networkPanel.Controls.Add(this.localIPLabel);
			this.networkPanel.Controls.Add(this.label2);
			this.networkPanel.Controls.Add(this.getLocalIPsSkinButton);
			this.networkPanel.Controls.Add(this.networkSearchSkinButton);
			this.networkPanel.Controls.Add(this.networkdUpdateSkinButton);
			this.networkPanel.Controls.Add(this.currentFileLabel);
			this.networkPanel.Controls.Add(this.label1);
			this.networkPanel.Controls.Add(this.networkSkinProgressBar);
			this.networkPanel.Controls.Add(this.setLocalIPSkinButton);
			this.networkPanel.Controls.Add(this.networkConnectSkinButton);
			this.networkPanel.Controls.Add(this.localIPSComboBox);
			this.networkPanel.Controls.Add(this.networkCurrentFileLabel);
			this.networkPanel.Controls.Add(this.networkDevicesComboBox);
			this.networkPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.networkPanel.Location = new System.Drawing.Point(0, 0);
			this.networkPanel.Name = "networkPanel";
			this.networkPanel.Size = new System.Drawing.Size(576, 229);
			this.networkPanel.TabIndex = 9;
			// 
			// localIPLabel
			// 
			this.localIPLabel.AutoSize = true;
			this.localIPLabel.Location = new System.Drawing.Point(452, 45);
			this.localIPLabel.Name = "localIPLabel";
			this.localIPLabel.Size = new System.Drawing.Size(0, 12);
			this.localIPLabel.TabIndex = 9;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(452, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 9;
			this.label2.Text = "本地ip：";
			// 
			// getLocalIPsSkinButton
			// 
			this.getLocalIPsSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.getLocalIPsSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.getLocalIPsSkinButton.BorderColor = System.Drawing.Color.Black;
			this.getLocalIPsSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.getLocalIPsSkinButton.DownBack = null;
			this.getLocalIPsSkinButton.Location = new System.Drawing.Point(37, 31);
			this.getLocalIPsSkinButton.MouseBack = null;
			this.getLocalIPsSkinButton.Name = "getLocalIPsSkinButton";
			this.getLocalIPsSkinButton.NormlBack = null;
			this.getLocalIPsSkinButton.Size = new System.Drawing.Size(108, 31);
			this.getLocalIPsSkinButton.TabIndex = 8;
			this.getLocalIPsSkinButton.Text = "获取本机ip列表";
			this.getLocalIPsSkinButton.UseVisualStyleBackColor = false;
			this.getLocalIPsSkinButton.Click += new System.EventHandler(this.getLocalIPsSkinButton_Click);
			// 
			// setLocalIPSkinButton
			// 
			this.setLocalIPSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.setLocalIPSkinButton.BaseColor = System.Drawing.Color.SeaGreen;
			this.setLocalIPSkinButton.BorderColor = System.Drawing.Color.Black;
			this.setLocalIPSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.setLocalIPSkinButton.DownBack = null;
			this.setLocalIPSkinButton.Enabled = false;
			this.setLocalIPSkinButton.GlowColor = System.Drawing.Color.Tan;
			this.setLocalIPSkinButton.Location = new System.Drawing.Point(353, 31);
			this.setLocalIPSkinButton.MouseBack = null;
			this.setLocalIPSkinButton.Name = "setLocalIPSkinButton";
			this.setLocalIPSkinButton.NormlBack = null;
			this.setLocalIPSkinButton.Size = new System.Drawing.Size(86, 31);
			this.setLocalIPSkinButton.TabIndex = 8;
			this.setLocalIPSkinButton.Text = "设置本地IP";
			this.setLocalIPSkinButton.UseVisualStyleBackColor = false;
			this.setLocalIPSkinButton.Click += new System.EventHandler(this.setLocalIPSkinButton_Click);
			// 
			// localIPSComboBox
			// 
			this.localIPSComboBox.Enabled = false;
			this.localIPSComboBox.FormattingEnabled = true;
			this.localIPSComboBox.Location = new System.Drawing.Point(164, 37);
			this.localIPSComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.localIPSComboBox.Name = "localIPSComboBox";
			this.localIPSComboBox.Size = new System.Drawing.Size(171, 20);
			this.localIPSComboBox.TabIndex = 5;
			// 
			// skinTabPage2
			// 
			this.skinTabPage2.BackColor = System.Drawing.Color.White;
			this.skinTabPage2.Controls.Add(this.comPanel);
			this.skinTabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skinTabPage2.Location = new System.Drawing.Point(0, 36);
			this.skinTabPage2.Name = "skinTabPage2";
			this.skinTabPage2.Size = new System.Drawing.Size(576, 229);
			this.skinTabPage2.TabIndex = 1;
			this.skinTabPage2.TabItemImage = null;
			this.skinTabPage2.Text = "串口模式";
			// 
			// comPanel
			// 
			this.comPanel.Controls.Add(this.comSearchSkinButton);
			this.comPanel.Controls.Add(this.comUpdateSkinButton);
			this.comPanel.Controls.Add(this.label3);
			this.comPanel.Controls.Add(this.label4);
			this.comPanel.Controls.Add(this.comSkinProgressBar);
			this.comPanel.Controls.Add(this.comConnectSkinButton);
			this.comPanel.Controls.Add(this.comCurrentFileLabel);
			this.comPanel.Controls.Add(this.comComboBox);
			this.comPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comPanel.Location = new System.Drawing.Point(0, 0);
			this.comPanel.Name = "comPanel";
			this.comPanel.Size = new System.Drawing.Size(576, 229);
			this.comPanel.TabIndex = 10;
			// 
			// comSearchSkinButton
			// 
			this.comSearchSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.comSearchSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.comSearchSkinButton.BorderColor = System.Drawing.Color.Black;
			this.comSearchSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.comSearchSkinButton.DownBack = null;
			this.comSearchSkinButton.Location = new System.Drawing.Point(35, 30);
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
			this.comUpdateSkinButton.Location = new System.Drawing.Point(453, 30);
			this.comUpdateSkinButton.MouseBack = null;
			this.comUpdateSkinButton.Name = "comUpdateSkinButton";
			this.comUpdateSkinButton.NormlBack = null;
			this.comUpdateSkinButton.Size = new System.Drawing.Size(76, 31);
			this.comUpdateSkinButton.TabIndex = 8;
			this.comUpdateSkinButton.Text = "下载数据";
			this.comUpdateSkinButton.UseVisualStyleBackColor = false;
			this.comUpdateSkinButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(123, 148);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(0, 12);
			this.label3.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(34, 98);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77, 12);
			this.label4.TabIndex = 1;
			this.label4.Text = "下载总进度：";
			// 
			// comSkinProgressBar
			// 
			this.comSkinProgressBar.Back = null;
			this.comSkinProgressBar.BackColor = System.Drawing.Color.Transparent;
			this.comSkinProgressBar.BarBack = null;
			this.comSkinProgressBar.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.comSkinProgressBar.ForeColor = System.Drawing.Color.Red;
			this.comSkinProgressBar.Location = new System.Drawing.Point(125, 91);
			this.comSkinProgressBar.Name = "comSkinProgressBar";
			this.comSkinProgressBar.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.comSkinProgressBar.Size = new System.Drawing.Size(404, 23);
			this.comSkinProgressBar.TabIndex = 7;
			// 
			// comConnectSkinButton
			// 
			this.comConnectSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.comConnectSkinButton.BaseColor = System.Drawing.Color.SeaGreen;
			this.comConnectSkinButton.BorderColor = System.Drawing.Color.Black;
			this.comConnectSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.comConnectSkinButton.DownBack = null;
			this.comConnectSkinButton.Enabled = false;
			this.comConnectSkinButton.Location = new System.Drawing.Point(363, 30);
			this.comConnectSkinButton.MouseBack = null;
			this.comConnectSkinButton.Name = "comConnectSkinButton";
			this.comConnectSkinButton.NormlBack = null;
			this.comConnectSkinButton.Size = new System.Drawing.Size(76, 31);
			this.comConnectSkinButton.TabIndex = 8;
			this.comConnectSkinButton.Text = "选择串口";
			this.comConnectSkinButton.UseVisualStyleBackColor = false;
			this.comConnectSkinButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// comCurrentFileLabel
			// 
			this.comCurrentFileLabel.AutoSize = true;
			this.comCurrentFileLabel.Location = new System.Drawing.Point(34, 147);
			this.comCurrentFileLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.comCurrentFileLabel.Name = "comCurrentFileLabel";
			this.comCurrentFileLabel.Size = new System.Drawing.Size(89, 12);
			this.comCurrentFileLabel.TabIndex = 6;
			this.comCurrentFileLabel.Text = "当前下载文件：";
			// 
			// comComboBox
			// 
			this.comComboBox.FormattingEnabled = true;
			this.comComboBox.Location = new System.Drawing.Point(169, 36);
			this.comComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.comComboBox.Name = "comComboBox";
			this.comComboBox.Size = new System.Drawing.Size(144, 20);
			this.comComboBox.TabIndex = 5;
			// 
			// UpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(576, 265);
			this.Controls.Add(this.skinTabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateForm";
			this.Text = "下载数据到设备";
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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox networkDevicesComboBox;
		private System.Windows.Forms.Label networkCurrentFileLabel;
		private System.Windows.Forms.Label currentFileLabel;
		private CCWin.SkinControl.SkinProgressBar networkSkinProgressBar;
		private CCWin.SkinControl.SkinButton networkSearchSkinButton;
		private CCWin.SkinControl.SkinButton networkConnectSkinButton;
		private CCWin.SkinControl.SkinButton networkdUpdateSkinButton;
		private CCWin.SkinControl.SkinTabControl skinTabControl;
		private CCWin.SkinControl.SkinTabPage networkTab;
		private System.Windows.Forms.Panel networkPanel;
		private CCWin.SkinControl.SkinTabPage skinTabPage2;
		private System.Windows.Forms.Panel comPanel;
		private CCWin.SkinControl.SkinButton comSearchSkinButton;
		private CCWin.SkinControl.SkinButton comUpdateSkinButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private CCWin.SkinControl.SkinProgressBar comSkinProgressBar;
		private CCWin.SkinControl.SkinButton comConnectSkinButton;
		private System.Windows.Forms.Label comCurrentFileLabel;
		private System.Windows.Forms.ComboBox comComboBox;
		private CCWin.SkinControl.SkinButton getLocalIPsSkinButton;
		private System.Windows.Forms.ComboBox localIPSComboBox;
		private CCWin.SkinControl.SkinButton setLocalIPSkinButton;
		private System.Windows.Forms.Label localIPLabel;
		private System.Windows.Forms.Label label2;
	}
}