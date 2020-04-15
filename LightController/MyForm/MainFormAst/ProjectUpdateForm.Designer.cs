namespace LightController.MyForm
{
	partial class ProjectUpdateForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectUpdateForm));
			this.label1 = new System.Windows.Forms.Label();
			this.ipsComboBox = new System.Windows.Forms.ComboBox();
			this.networkFileLabel = new System.Windows.Forms.Label();
			this.networkFileShowLabel = new System.Windows.Forms.Label();
			this.networkSkinProgressBar = new CCWin.SkinControl.SkinProgressBar();
			this.skinTabControl = new CCWin.SkinControl.SkinTabControl();
			this.networkTab = new CCWin.SkinControl.SkinTabPage();
			this.networkPanel = new System.Windows.Forms.Panel();
			this.networkUpdateButton = new System.Windows.Forms.Button();
			this.getLocalIPsButton = new System.Windows.Forms.Button();
			this.networkSearchButton = new System.Windows.Forms.Button();
			this.localIPsComboBox = new System.Windows.Forms.ComboBox();
			this.skinTabPage2 = new CCWin.SkinControl.SkinTabPage();
			this.comPanel = new System.Windows.Forms.Panel();
			this.comUpdateButton = new System.Windows.Forms.Button();
			this.comOpenButton = new System.Windows.Forms.Button();
			this.comSearchButton = new System.Windows.Forms.Button();
			this.comNameLabel = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.comFileShowLabel = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comSkinProgressBar = new CCWin.SkinControl.SkinProgressBar();
			this.comFileLabel = new System.Windows.Forms.Label();
			this.comComboBox = new System.Windows.Forms.ComboBox();
			this.pathLabel = new System.Windows.Forms.Label();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.fileOpenButton = new System.Windows.Forms.Button();
			this.clearButton = new System.Windows.Forms.Button();
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
			this.label1.Location = new System.Drawing.Point(42, 137);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "下载总进度：";
			// 
			// ipsComboBox
			// 
			this.ipsComboBox.Enabled = false;
			this.ipsComboBox.FormattingEnabled = true;
			this.ipsComboBox.Location = new System.Drawing.Point(160, 85);
			this.ipsComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.ipsComboBox.Name = "ipsComboBox";
			this.ipsComboBox.Size = new System.Drawing.Size(274, 20);
			this.ipsComboBox.TabIndex = 5;
			this.ipsComboBox.SelectedIndexChanged += new System.EventHandler(this.networkDevicesComboBox_SelectedIndexChanged);
			// 
			// networkFileLabel
			// 
			this.networkFileLabel.AutoSize = true;
			this.networkFileLabel.Location = new System.Drawing.Point(42, 178);
			this.networkFileLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.networkFileLabel.Name = "networkFileLabel";
			this.networkFileLabel.Size = new System.Drawing.Size(89, 12);
			this.networkFileLabel.TabIndex = 6;
			this.networkFileLabel.Text = "当前操作内容：";
			// 
			// networkFileShowLabel
			// 
			this.networkFileShowLabel.AutoSize = true;
			this.networkFileShowLabel.Location = new System.Drawing.Point(160, 178);
			this.networkFileShowLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.networkFileShowLabel.Name = "networkFileShowLabel";
			this.networkFileShowLabel.Size = new System.Drawing.Size(179, 12);
			this.networkFileShowLabel.TabIndex = 6;
			this.networkFileShowLabel.Text = "                             ";
			// 
			// networkSkinProgressBar
			// 
			this.networkSkinProgressBar.Back = null;
			this.networkSkinProgressBar.BackColor = System.Drawing.Color.Transparent;
			this.networkSkinProgressBar.BarBack = null;
			this.networkSkinProgressBar.BarRadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.networkSkinProgressBar.ForeColor = System.Drawing.Color.Red;
			this.networkSkinProgressBar.Location = new System.Drawing.Point(160, 132);
			this.networkSkinProgressBar.Name = "networkSkinProgressBar";
			this.networkSkinProgressBar.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.networkSkinProgressBar.Size = new System.Drawing.Size(372, 23);
			this.networkSkinProgressBar.TabIndex = 7;
			// 
			// skinTabControl
			// 
			this.skinTabControl.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
			this.skinTabControl.BackColor = System.Drawing.Color.Gainsboro;
			this.skinTabControl.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
			this.skinTabControl.Controls.Add(this.networkTab);
			this.skinTabControl.Controls.Add(this.skinTabPage2);
			this.skinTabControl.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinTabControl.HeadBack = null;
			this.skinTabControl.ImgTxtOffset = new System.Drawing.Point(0, 0);
			this.skinTabControl.ItemSize = new System.Drawing.Size(70, 36);
			this.skinTabControl.Location = new System.Drawing.Point(0, 66);
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
			this.skinTabControl.Size = new System.Drawing.Size(566, 265);
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
			this.networkTab.Size = new System.Drawing.Size(566, 229);
			this.networkTab.TabIndex = 0;
			this.networkTab.TabItemImage = null;
			this.networkTab.Text = "网络模式";
			// 
			// networkPanel
			// 
			this.networkPanel.Controls.Add(this.networkUpdateButton);
			this.networkPanel.Controls.Add(this.getLocalIPsButton);
			this.networkPanel.Controls.Add(this.networkSearchButton);
			this.networkPanel.Controls.Add(this.networkFileShowLabel);
			this.networkPanel.Controls.Add(this.label1);
			this.networkPanel.Controls.Add(this.networkSkinProgressBar);
			this.networkPanel.Controls.Add(this.localIPsComboBox);
			this.networkPanel.Controls.Add(this.networkFileLabel);
			this.networkPanel.Controls.Add(this.ipsComboBox);
			this.networkPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.networkPanel.Location = new System.Drawing.Point(0, 0);
			this.networkPanel.Name = "networkPanel";
			this.networkPanel.Size = new System.Drawing.Size(566, 229);
			this.networkPanel.TabIndex = 9;
			// 
			// networkUpdateButton
			// 
			this.networkUpdateButton.Location = new System.Drawing.Point(451, 78);
			this.networkUpdateButton.Name = "networkUpdateButton";
			this.networkUpdateButton.Size = new System.Drawing.Size(81, 31);
			this.networkUpdateButton.TabIndex = 10;
			this.networkUpdateButton.Text = "下载工程";
			this.networkUpdateButton.UseVisualStyleBackColor = true;
			this.networkUpdateButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// getLocalIPsButton
			// 
			this.getLocalIPsButton.Location = new System.Drawing.Point(36, 30);
			this.getLocalIPsButton.Name = "getLocalIPsButton";
			this.getLocalIPsButton.Size = new System.Drawing.Size(108, 31);
			this.getLocalIPsButton.TabIndex = 9;
			this.getLocalIPsButton.Text = "获取本机IP列表";
			this.getLocalIPsButton.UseVisualStyleBackColor = true;
			this.getLocalIPsButton.Click += new System.EventHandler(this.getLocalIPsSkinButton_Click);
			// 
			// networkSearchButton
			// 
			this.networkSearchButton.Enabled = false;
			this.networkSearchButton.Location = new System.Drawing.Point(36, 79);
			this.networkSearchButton.Name = "networkSearchButton";
			this.networkSearchButton.Size = new System.Drawing.Size(108, 31);
			this.networkSearchButton.TabIndex = 9;
			this.networkSearchButton.Text = "搜索网络设备";
			this.networkSearchButton.UseVisualStyleBackColor = true;
			this.networkSearchButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// localIPsComboBox
			// 
			this.localIPsComboBox.Enabled = false;
			this.localIPsComboBox.FormattingEnabled = true;
			this.localIPsComboBox.Location = new System.Drawing.Point(160, 37);
			this.localIPsComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.localIPsComboBox.Name = "localIPsComboBox";
			this.localIPsComboBox.Size = new System.Drawing.Size(274, 20);
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
			this.skinTabPage2.Size = new System.Drawing.Size(566, 229);
			this.skinTabPage2.TabIndex = 1;
			this.skinTabPage2.TabItemImage = null;
			this.skinTabPage2.Text = "串口模式";
			// 
			// comPanel
			// 
			this.comPanel.Controls.Add(this.comUpdateButton);
			this.comPanel.Controls.Add(this.comOpenButton);
			this.comPanel.Controls.Add(this.comSearchButton);
			this.comPanel.Controls.Add(this.comNameLabel);
			this.comPanel.Controls.Add(this.label6);
			this.comPanel.Controls.Add(this.comFileShowLabel);
			this.comPanel.Controls.Add(this.label4);
			this.comPanel.Controls.Add(this.comSkinProgressBar);
			this.comPanel.Controls.Add(this.comFileLabel);
			this.comPanel.Controls.Add(this.comComboBox);
			this.comPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comPanel.Location = new System.Drawing.Point(0, 0);
			this.comPanel.Name = "comPanel";
			this.comPanel.Size = new System.Drawing.Size(566, 229);
			this.comPanel.TabIndex = 10;
			// 
			// comUpdateButton
			// 
			this.comUpdateButton.Enabled = false;
			this.comUpdateButton.Location = new System.Drawing.Point(451, 40);
			this.comUpdateButton.Name = "comUpdateButton";
			this.comUpdateButton.Size = new System.Drawing.Size(76, 31);
			this.comUpdateButton.TabIndex = 12;
			this.comUpdateButton.Text = "下载工程";
			this.comUpdateButton.UseVisualStyleBackColor = true;
			this.comUpdateButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// comOpenButton
			// 
			this.comOpenButton.Enabled = false;
			this.comOpenButton.Location = new System.Drawing.Point(360, 40);
			this.comOpenButton.Name = "comOpenButton";
			this.comOpenButton.Size = new System.Drawing.Size(76, 31);
			this.comOpenButton.TabIndex = 12;
			this.comOpenButton.Text = "打开串口";
			this.comOpenButton.UseVisualStyleBackColor = true;
			this.comOpenButton.Click += new System.EventHandler(this.comOpenButton_Click);
			// 
			// comSearchButton
			// 
			this.comSearchButton.Location = new System.Drawing.Point(36, 40);
			this.comSearchButton.Name = "comSearchButton";
			this.comSearchButton.Size = new System.Drawing.Size(88, 31);
			this.comSearchButton.TabIndex = 12;
			this.comSearchButton.Text = "搜索串口设备";
			this.comSearchButton.UseVisualStyleBackColor = true;
			this.comSearchButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// comNameLabel
			// 
			this.comNameLabel.AutoSize = true;
			this.comNameLabel.Location = new System.Drawing.Point(287, 54);
			this.comNameLabel.Name = "comNameLabel";
			this.comNameLabel.Size = new System.Drawing.Size(0, 12);
			this.comNameLabel.TabIndex = 10;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(270, 40);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(65, 12);
			this.label6.TabIndex = 11;
			this.label6.Text = "已选串口：";
			// 
			// comFileShowLabel
			// 
			this.comFileShowLabel.AutoSize = true;
			this.comFileShowLabel.Location = new System.Drawing.Point(148, 156);
			this.comFileShowLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.comFileShowLabel.Name = "comFileShowLabel";
			this.comFileShowLabel.Size = new System.Drawing.Size(137, 12);
			this.comFileShowLabel.TabIndex = 6;
			this.comFileShowLabel.Text = "                      ";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(39, 108);
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
			this.comSkinProgressBar.Location = new System.Drawing.Point(150, 101);
			this.comSkinProgressBar.Name = "comSkinProgressBar";
			this.comSkinProgressBar.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
			this.comSkinProgressBar.Size = new System.Drawing.Size(379, 23);
			this.comSkinProgressBar.TabIndex = 7;
			// 
			// comFileLabel
			// 
			this.comFileLabel.AutoSize = true;
			this.comFileLabel.Location = new System.Drawing.Point(39, 156);
			this.comFileLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.comFileLabel.Name = "comFileLabel";
			this.comFileLabel.Size = new System.Drawing.Size(89, 12);
			this.comFileLabel.TabIndex = 6;
			this.comFileLabel.Text = "当前操作内容：";
			// 
			// comComboBox
			// 
			this.comComboBox.Enabled = false;
			this.comComboBox.FormattingEnabled = true;
			this.comComboBox.Location = new System.Drawing.Point(150, 46);
			this.comComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.comComboBox.Name = "comComboBox";
			this.comComboBox.Size = new System.Drawing.Size(92, 20);
			this.comComboBox.TabIndex = 5;
			// 
			// pathLabel
			// 
			this.pathLabel.Location = new System.Drawing.Point(139, 17);
			this.pathLabel.Name = "pathLabel";
			this.pathLabel.Size = new System.Drawing.Size(311, 33);
			this.pathLabel.TabIndex = 13;
			// 
			// folderBrowserDialog
			// 
			this.folderBrowserDialog.Description = "请选择工程目录的最后一层（即CSJ目录），本操作会将该目录下的所有文件传给设备。";
			this.folderBrowserDialog.ShowNewFolderButton = false;
			// 
			// fileOpenButton
			// 
			this.fileOpenButton.Location = new System.Drawing.Point(36, 17);
			this.fileOpenButton.Name = "fileOpenButton";
			this.fileOpenButton.Size = new System.Drawing.Size(86, 33);
			this.fileOpenButton.TabIndex = 15;
			this.fileOpenButton.Text = "选择已有工程";
			this.fileOpenButton.UseVisualStyleBackColor = true;
			this.fileOpenButton.Click += new System.EventHandler(this.fileOpenSkinButton_Click);
			// 
			// clearButton
			// 
			this.clearButton.Location = new System.Drawing.Point(469, 17);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(63, 33);
			this.clearButton.TabIndex = 16;
			this.clearButton.Text = "清空";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.clearSkinButton_Click);
			// 
			// ProjectUpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(566, 331);
			this.Controls.Add(this.clearButton);
			this.Controls.Add(this.fileOpenButton);
			this.Controls.Add(this.pathLabel);
			this.Controls.Add(this.skinTabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProjectUpdateForm";
			this.Text = "更新工程到设备";
			this.Load += new System.EventHandler(this.ProjectUpdateForm_Load);
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
		private System.Windows.Forms.ComboBox ipsComboBox;
		private System.Windows.Forms.Label networkFileLabel;
		private System.Windows.Forms.Label networkFileShowLabel;
		private CCWin.SkinControl.SkinProgressBar networkSkinProgressBar;
		private CCWin.SkinControl.SkinTabControl skinTabControl;
		private CCWin.SkinControl.SkinTabPage networkTab;
		private System.Windows.Forms.Panel networkPanel;
		private CCWin.SkinControl.SkinTabPage skinTabPage2;
		private System.Windows.Forms.Panel comPanel;
		private System.Windows.Forms.Label comFileShowLabel;
		private System.Windows.Forms.Label label4;
		private CCWin.SkinControl.SkinProgressBar comSkinProgressBar;
		private System.Windows.Forms.Label comFileLabel;
		private System.Windows.Forms.ComboBox comComboBox;
		private System.Windows.Forms.ComboBox localIPsComboBox;
		private System.Windows.Forms.Label comNameLabel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label pathLabel;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.Button getLocalIPsButton;
		private System.Windows.Forms.Button networkSearchButton;
		private System.Windows.Forms.Button networkUpdateButton;
		private System.Windows.Forms.Button fileOpenButton;
		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.Button comUpdateButton;
		private System.Windows.Forms.Button comOpenButton;
		private System.Windows.Forms.Button comSearchButton;
	}
}