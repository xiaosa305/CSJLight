using System.Windows.Forms;

namespace LightController.MyForm
{
	partial class NewMainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMainForm));
			this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.lightLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hardwareSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hardwareSetNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hardwareSetOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.hardwareUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.lightListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.globalSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ymSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.projectUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.使用说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.更新日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playPanel = new System.Windows.Forms.Panel();
			this.changeConnectMethodButton = new System.Windows.Forms.Button();
			this.deviceRefreshButton = new System.Windows.Forms.Button();
			this.keepButton = new System.Windows.Forms.Button();
			this.deviceComboBox = new System.Windows.Forms.ComboBox();
			this.deviceConnectButton = new System.Windows.Forms.Button();
			this.makeSoundButton = new System.Windows.Forms.Button();
			this.previewButton = new System.Windows.Forms.Button();
			this.lightTypeLabel = new System.Windows.Forms.Label();
			this.lightNameLabel = new System.Windows.Forms.Label();
			this.myContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.refreshPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.hideMenuStriplToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hideProjectPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hideUnifyPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hidePlayPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.showSaPanelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.labelPanel = new System.Windows.Forms.Panel();
			this.firstLabel = new System.Windows.Forms.Label();
			this.secondLabel = new System.Windows.Forms.Label();
			this.thirdLabel = new System.Windows.Forms.Label();
			this.tdFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.tdPanelDemo = new System.Windows.Forms.Panel();
			this.tdNameLabelDemo = new System.Windows.Forms.Label();
			this.tdNoLabelDemo = new System.Windows.Forms.Label();
			this.tdCmComboBoxDemo = new System.Windows.Forms.ComboBox();
			this.tdStNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.tdValueNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.tdTrackBarDemo = new System.Windows.Forms.TrackBar();
			this.saPanelDemo = new System.Windows.Forms.Panel();
			this.saFLPDemo = new System.Windows.Forms.FlowLayoutPanel();
			this.saLabelDemo = new System.Windows.Forms.Label();
			this.saButtonDemo = new System.Windows.Forms.Button();
			this.copyStepButton = new System.Windows.Forms.Button();
			this.pasteStepButton = new System.Windows.Forms.Button();
			this.tdPanel = new System.Windows.Forms.Panel();
			this.unifyPanel = new System.Windows.Forms.Panel();
			this.groupButton = new System.Windows.Forms.Button();
			this.groupFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.groupInButtonDemo = new System.Windows.Forms.Button();
			this.groupDelButtonDemo = new System.Windows.Forms.Button();
			this.saButton = new System.Windows.Forms.Button();
			this.soundListButton = new System.Windows.Forms.Button();
			this.detailMultiButton = new System.Windows.Forms.Button();
			this.multiButton = new System.Windows.Forms.Button();
			this.playBasePanel = new System.Windows.Forms.Panel();
			this.wjTestButton = new System.Windows.Forms.Button();
			this.testButton2 = new System.Windows.Forms.Button();
			this.testButton1 = new System.Windows.Forms.Button();
			this.myStatusStrip = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.skinComboBox = new System.Windows.Forms.ComboBox();
			this.projectPanel = new System.Windows.Forms.Panel();
			this.newProjectButton = new System.Windows.Forms.Button();
			this.exportButton = new System.Windows.Forms.Button();
			this.useFrameButton = new System.Windows.Forms.Button();
			this.openProjectButton = new System.Windows.Forms.Button();
			this.saveProjectButton = new System.Windows.Forms.Button();
			this.saveFrameButton = new System.Windows.Forms.Button();
			this.closeProjectButton = new System.Windows.Forms.Button();
			this.lightsListView = new System.Windows.Forms.ListView();
			this.lightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.stepBasePanel = new System.Windows.Forms.Panel();
			this.stepPanel = new System.Windows.Forms.Panel();
			this.chooseStepButton = new System.Windows.Forms.Button();
			this.saveMaterialButton = new System.Windows.Forms.Button();
			this.modeComboBox = new System.Windows.Forms.ComboBox();
			this.frameComboBox = new System.Windows.Forms.ComboBox();
			this.stepLabel = new System.Windows.Forms.Label();
			this.frameChooseLabel = new System.Windows.Forms.Label();
			this.chooseStepNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.modeChooseLabel = new System.Windows.Forms.Label();
			this.syncButton = new System.Windows.Forms.Button();
			this.multiLightButton = new System.Windows.Forms.Button();
			this.backStepButton = new System.Windows.Forms.Button();
			this.multiPasteButton = new System.Windows.Forms.Button();
			this.insertButton = new System.Windows.Forms.Button();
			this.nextStepButton = new System.Windows.Forms.Button();
			this.actionButton = new System.Windows.Forms.Button();
			this.useMaterialButton = new System.Windows.Forms.Button();
			this.deleteStepButton = new System.Windows.Forms.Button();
			this.multiplexButton = new System.Windows.Forms.Button();
			this.multiCopyButton = new System.Windows.Forms.Button();
			this.addStepButton = new System.Windows.Forms.Button();
			this.topPanel = new System.Windows.Forms.Panel();
			this.lightInfoPanel = new System.Windows.Forms.Panel();
			this.lightsAddrLabel = new System.Windows.Forms.Label();
			this.currentLightPictureBox = new System.Windows.Forms.PictureBox();
			this.lightRemarkLabel = new System.Windows.Forms.Label();
			this.groupToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.saToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.mainMenuStrip.SuspendLayout();
			this.playPanel.SuspendLayout();
			this.myContextMenuStrip.SuspendLayout();
			this.labelPanel.SuspendLayout();
			this.tdFlowLayoutPanel.SuspendLayout();
			this.tdPanelDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tdStNUDDemo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tdValueNUDDemo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tdTrackBarDemo)).BeginInit();
			this.saPanelDemo.SuspendLayout();
			this.saFLPDemo.SuspendLayout();
			this.tdPanel.SuspendLayout();
			this.unifyPanel.SuspendLayout();
			this.groupFlowLayoutPanel.SuspendLayout();
			this.playBasePanel.SuspendLayout();
			this.myStatusStrip.SuspendLayout();
			this.projectPanel.SuspendLayout();
			this.stepBasePanel.SuspendLayout();
			this.stepPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chooseStepNumericUpDown)).BeginInit();
			this.topPanel.SuspendLayout();
			this.lightInfoPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.currentLightPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// skinEngine1
			// 
			this.skinEngine1.@__DrawButtonFocusRectangle = true;
			this.skinEngine1.DisabledButtonTextColor = System.Drawing.Color.Gray;
			this.skinEngine1.DisabledMenuFontColor = System.Drawing.SystemColors.GrayText;
			this.skinEngine1.InactiveCaptionColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.skinEngine1.SerialNumber = "";
			this.skinEngine1.SkinFile = null;
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.AutoSize = false;
			this.mainMenuStrip.BackColor = System.Drawing.SystemColors.ControlLight;
			this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightLibraryToolStripMenuItem,
            this.hardwareSetToolStripMenuItem,
            this.newToolStripMenuItem,
            this.toolStripMenuItem1,
            this.lightListToolStripMenuItem,
            this.globalSetToolStripMenuItem,
            this.ymSetToolStripMenuItem,
            this.projectUpdateToolStripMenuItem,
            this.toolStripMenuItem2,
            this.helpToolStripMenuItem,
            this.ExitToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
			this.mainMenuStrip.Size = new System.Drawing.Size(1264, 30);
			this.mainMenuStrip.TabIndex = 24;
			this.mainMenuStrip.Text = "menuStrip1";
			// 
			// lightLibraryToolStripMenuItem
			// 
			this.lightLibraryToolStripMenuItem.Name = "lightLibraryToolStripMenuItem";
			this.lightLibraryToolStripMenuItem.Size = new System.Drawing.Size(68, 26);
			this.lightLibraryToolStripMenuItem.Text = "灯库编辑";
			this.lightLibraryToolStripMenuItem.Click += new System.EventHandler(this.lightLibraryToolStripMenuItem_Click);
			// 
			// hardwareSetToolStripMenuItem
			// 
			this.hardwareSetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hardwareSetNewToolStripMenuItem,
            this.hardwareSetOpenToolStripMenuItem,
            this.toolStripSeparator3,
            this.hardwareUpdateToolStripMenuItem});
			this.hardwareSetToolStripMenuItem.Name = "hardwareSetToolStripMenuItem";
			this.hardwareSetToolStripMenuItem.Size = new System.Drawing.Size(68, 26);
			this.hardwareSetToolStripMenuItem.Text = "硬件配置";
			// 
			// hardwareSetNewToolStripMenuItem
			// 
			this.hardwareSetNewToolStripMenuItem.Name = "hardwareSetNewToolStripMenuItem";
			this.hardwareSetNewToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.hardwareSetNewToolStripMenuItem.Text = "新建配置";
			this.hardwareSetNewToolStripMenuItem.Click += new System.EventHandler(this.hardwareSetNewToolStripMenuItem_Click);
			// 
			// hardwareSetOpenToolStripMenuItem
			// 
			this.hardwareSetOpenToolStripMenuItem.Name = "hardwareSetOpenToolStripMenuItem";
			this.hardwareSetOpenToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.hardwareSetOpenToolStripMenuItem.Text = "打开配置";
			this.hardwareSetOpenToolStripMenuItem.Click += new System.EventHandler(this.hardwareSetOpenToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(121, 6);
			// 
			// hardwareUpdateToolStripMenuItem
			// 
			this.hardwareUpdateToolStripMenuItem.Name = "hardwareUpdateToolStripMenuItem";
			this.hardwareUpdateToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.hardwareUpdateToolStripMenuItem.Text = "硬件升级";
			this.hardwareUpdateToolStripMenuItem.Click += new System.EventHandler(this.hardwareUpdateToolStripMenuItem_Click);
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(68, 26);
			this.newToolStripMenuItem.Text = "外设配置";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Enabled = false;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(23, 26);
			this.toolStripMenuItem1.Text = "|";
			// 
			// lightListToolStripMenuItem
			// 
			this.lightListToolStripMenuItem.Enabled = false;
			this.lightListToolStripMenuItem.Name = "lightListToolStripMenuItem";
			this.lightListToolStripMenuItem.Size = new System.Drawing.Size(68, 26);
			this.lightListToolStripMenuItem.Text = "灯具列表";
			this.lightListToolStripMenuItem.Click += new System.EventHandler(this.lightListToolStripMenuItem_Click);
			// 
			// globalSetToolStripMenuItem
			// 
			this.globalSetToolStripMenuItem.Enabled = false;
			this.globalSetToolStripMenuItem.Name = "globalSetToolStripMenuItem";
			this.globalSetToolStripMenuItem.Size = new System.Drawing.Size(68, 26);
			this.globalSetToolStripMenuItem.Text = "全局配置";
			this.globalSetToolStripMenuItem.Click += new System.EventHandler(this.globalSetToolStripMenuItem_Click);
			// 
			// ymSetToolStripMenuItem
			// 
			this.ymSetToolStripMenuItem.Enabled = false;
			this.ymSetToolStripMenuItem.Name = "ymSetToolStripMenuItem";
			this.ymSetToolStripMenuItem.Size = new System.Drawing.Size(68, 26);
			this.ymSetToolStripMenuItem.Text = "摇麦设置";
			this.ymSetToolStripMenuItem.Click += new System.EventHandler(this.ymToolStripMenuItem_Click);
			// 
			// projectUpdateToolStripMenuItem
			// 
			this.projectUpdateToolStripMenuItem.Enabled = false;
			this.projectUpdateToolStripMenuItem.Name = "projectUpdateToolStripMenuItem";
			this.projectUpdateToolStripMenuItem.Size = new System.Drawing.Size(68, 26);
			this.projectUpdateToolStripMenuItem.Text = "工程更新";
			this.projectUpdateToolStripMenuItem.Click += new System.EventHandler(this.projectUpdateToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Enabled = false;
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(23, 26);
			this.toolStripMenuItem2.Text = "|";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.使用说明ToolStripMenuItem,
            this.更新日志ToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 26);
			this.helpToolStripMenuItem.Text = "帮助";
			// 
			// 使用说明ToolStripMenuItem
			// 
			this.使用说明ToolStripMenuItem.Name = "使用说明ToolStripMenuItem";
			this.使用说明ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.使用说明ToolStripMenuItem.Text = "使用说明";
			this.使用说明ToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
			// 
			// 更新日志ToolStripMenuItem
			// 
			this.更新日志ToolStripMenuItem.Name = "更新日志ToolStripMenuItem";
			this.更新日志ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.更新日志ToolStripMenuItem.Text = "更新日志";
			this.更新日志ToolStripMenuItem.Click += new System.EventHandler(this.updateLogToolStripMenuItem_Click);
			// 
			// ExitToolStripMenuItem
			// 
			this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			this.ExitToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.ExitToolStripMenuItem.Size = new System.Drawing.Size(68, 26);
			this.ExitToolStripMenuItem.Text = "退出程序";
			this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// playPanel
			// 
			this.playPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.playPanel.Controls.Add(this.changeConnectMethodButton);
			this.playPanel.Controls.Add(this.deviceRefreshButton);
			this.playPanel.Controls.Add(this.keepButton);
			this.playPanel.Controls.Add(this.deviceComboBox);
			this.playPanel.Controls.Add(this.deviceConnectButton);
			this.playPanel.Controls.Add(this.makeSoundButton);
			this.playPanel.Controls.Add(this.previewButton);
			this.playPanel.Enabled = false;
			this.playPanel.Location = new System.Drawing.Point(364, 0);
			this.playPanel.Name = "playPanel";
			this.playPanel.Size = new System.Drawing.Size(549, 68);
			this.playPanel.TabIndex = 30;
			// 
			// changeConnectMethodButton
			// 
			this.changeConnectMethodButton.Location = new System.Drawing.Point(17, 7);
			this.changeConnectMethodButton.Margin = new System.Windows.Forms.Padding(2);
			this.changeConnectMethodButton.Name = "changeConnectMethodButton";
			this.changeConnectMethodButton.Size = new System.Drawing.Size(74, 54);
			this.changeConnectMethodButton.TabIndex = 20;
			this.changeConnectMethodButton.Text = "切换为\r\n网络连接";
			this.changeConnectMethodButton.UseVisualStyleBackColor = true;
			this.changeConnectMethodButton.Click += new System.EventHandler(this.changeConnectMethodButton_Click);
			// 
			// deviceRefreshButton
			// 
			this.deviceRefreshButton.Location = new System.Drawing.Point(104, 34);
			this.deviceRefreshButton.Margin = new System.Windows.Forms.Padding(2);
			this.deviceRefreshButton.Name = "deviceRefreshButton";
			this.deviceRefreshButton.Size = new System.Drawing.Size(88, 26);
			this.deviceRefreshButton.TabIndex = 20;
			this.deviceRefreshButton.Text = "刷新串口";
			this.deviceRefreshButton.UseVisualStyleBackColor = true;
			this.deviceRefreshButton.Click += new System.EventHandler(this.deviceRefreshButton_Click);
			// 
			// keepButton
			// 
			this.keepButton.Enabled = false;
			this.keepButton.Location = new System.Drawing.Point(307, 7);
			this.keepButton.Margin = new System.Windows.Forms.Padding(2);
			this.keepButton.Name = "keepButton";
			this.keepButton.Size = new System.Drawing.Size(69, 54);
			this.keepButton.TabIndex = 24;
			this.keepButton.Text = "保持状态";
			this.keepButton.UseVisualStyleBackColor = true;
			this.keepButton.Click += new System.EventHandler(this.keepButton_Click);
			// 
			// deviceComboBox
			// 
			this.deviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.deviceComboBox.Enabled = false;
			this.deviceComboBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.deviceComboBox.FormattingEnabled = true;
			this.deviceComboBox.Location = new System.Drawing.Point(101, 8);
			this.deviceComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.deviceComboBox.Name = "deviceComboBox";
			this.deviceComboBox.Size = new System.Drawing.Size(196, 20);
			this.deviceComboBox.TabIndex = 19;
			this.deviceComboBox.SelectedIndexChanged += new System.EventHandler(this.deviceComboBox_SelectedIndexChanged);
			// 
			// deviceConnectButton
			// 
			this.deviceConnectButton.BackColor = System.Drawing.Color.Transparent;
			this.deviceConnectButton.Location = new System.Drawing.Point(206, 34);
			this.deviceConnectButton.Margin = new System.Windows.Forms.Padding(2);
			this.deviceConnectButton.Name = "deviceConnectButton";
			this.deviceConnectButton.Size = new System.Drawing.Size(88, 26);
			this.deviceConnectButton.TabIndex = 23;
			this.deviceConnectButton.Text = "连接设备";
			this.deviceConnectButton.UseVisualStyleBackColor = false;
			this.deviceConnectButton.Click += new System.EventHandler(this.deviceConnectButton_Click);
			// 
			// makeSoundButton
			// 
			this.makeSoundButton.Enabled = false;
			this.makeSoundButton.Location = new System.Drawing.Point(465, 7);
			this.makeSoundButton.Margin = new System.Windows.Forms.Padding(2);
			this.makeSoundButton.Name = "makeSoundButton";
			this.makeSoundButton.Size = new System.Drawing.Size(69, 54);
			this.makeSoundButton.TabIndex = 25;
			this.makeSoundButton.Text = "触发音频";
			this.makeSoundButton.UseVisualStyleBackColor = true;
			this.makeSoundButton.Click += new System.EventHandler(this.makeSoundButton_Click);
			// 
			// previewButton
			// 
			this.previewButton.Enabled = false;
			this.previewButton.Location = new System.Drawing.Point(386, 7);
			this.previewButton.Margin = new System.Windows.Forms.Padding(2);
			this.previewButton.Name = "previewButton";
			this.previewButton.Size = new System.Drawing.Size(69, 54);
			this.previewButton.TabIndex = 24;
			this.previewButton.Text = "预览效果";
			this.previewButton.UseVisualStyleBackColor = true;
			this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
			// 
			// lightTypeLabel
			// 
			this.lightTypeLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightTypeLabel.ForeColor = System.Drawing.Color.Black;
			this.lightTypeLabel.Location = new System.Drawing.Point(4, 153);
			this.lightTypeLabel.Name = "lightTypeLabel";
			this.lightTypeLabel.Size = new System.Drawing.Size(144, 18);
			this.lightTypeLabel.TabIndex = 7;
			this.lightTypeLabel.Text = " ";
			// 
			// lightNameLabel
			// 
			this.lightNameLabel.BackColor = System.Drawing.Color.Transparent;
			this.lightNameLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightNameLabel.ForeColor = System.Drawing.Color.Black;
			this.lightNameLabel.Location = new System.Drawing.Point(4, 129);
			this.lightNameLabel.Name = "lightNameLabel";
			this.lightNameLabel.Size = new System.Drawing.Size(144, 18);
			this.lightNameLabel.TabIndex = 8;
			this.lightNameLabel.Text = " ";
			// 
			// myContextMenuStrip
			// 
			this.myContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshPicToolStripMenuItem,
            this.toolStripSeparator4,
            this.hideMenuStriplToolStripMenuItem,
            this.hideProjectPanelToolStripMenuItem,
            this.hideUnifyPanelToolStripMenuItem,
            this.hidePlayPanelToolStripMenuItem,
            this.toolStripSeparator1,
            this.showSaPanelsToolStripMenuItem});
			this.myContextMenuStrip.Name = "myContextMenuStrip";
			this.myContextMenuStrip.Size = new System.Drawing.Size(173, 148);
			// 
			// refreshPicToolStripMenuItem
			// 
			this.refreshPicToolStripMenuItem.Enabled = false;
			this.refreshPicToolStripMenuItem.Name = "refreshPicToolStripMenuItem";
			this.refreshPicToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.refreshPicToolStripMenuItem.Text = "重新加载灯具图片";
			this.refreshPicToolStripMenuItem.Click += new System.EventHandler(this.refreshPicToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(169, 6);
			// 
			// hideMenuStriplToolStripMenuItem
			// 
			this.hideMenuStriplToolStripMenuItem.Name = "hideMenuStriplToolStripMenuItem";
			this.hideMenuStriplToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.hideMenuStriplToolStripMenuItem.Text = "隐藏主菜单面板";
			this.hideMenuStriplToolStripMenuItem.Click += new System.EventHandler(this.hideMenuPanelToolStripMenuItem_Click);
			// 
			// hideProjectPanelToolStripMenuItem
			// 
			this.hideProjectPanelToolStripMenuItem.Name = "hideProjectPanelToolStripMenuItem";
			this.hideProjectPanelToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.hideProjectPanelToolStripMenuItem.Text = "隐藏工程面板";
			this.hideProjectPanelToolStripMenuItem.Click += new System.EventHandler(this.hideProjectPanelToolStripMenuItem_Click);
			// 
			// hideUnifyPanelToolStripMenuItem
			// 
			this.hideUnifyPanelToolStripMenuItem.Name = "hideUnifyPanelToolStripMenuItem";
			this.hideUnifyPanelToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.hideUnifyPanelToolStripMenuItem.Text = "隐藏辅助面板";
			this.hideUnifyPanelToolStripMenuItem.Click += new System.EventHandler(this.hideUnifyPanelToolStripMenuItem_Click);
			// 
			// hidePlayPanelToolStripMenuItem
			// 
			this.hidePlayPanelToolStripMenuItem.Name = "hidePlayPanelToolStripMenuItem";
			this.hidePlayPanelToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.hidePlayPanelToolStripMenuItem.Text = "隐藏调试面板";
			this.hidePlayPanelToolStripMenuItem.Click += new System.EventHandler(this.hidePlayPanelToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
			// 
			// showSaPanelsToolStripMenuItem
			// 
			this.showSaPanelsToolStripMenuItem.Name = "showSaPanelsToolStripMenuItem";
			this.showSaPanelsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.showSaPanelsToolStripMenuItem.Text = "隐藏子属性面板";
			this.showSaPanelsToolStripMenuItem.Click += new System.EventHandler(this.showSaPanelsToolStripMenuItem_Click);
			// 
			// labelPanel
			// 
			this.labelPanel.BackColor = System.Drawing.Color.Transparent;
			this.labelPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelPanel.Controls.Add(this.firstLabel);
			this.labelPanel.Controls.Add(this.secondLabel);
			this.labelPanel.Controls.Add(this.thirdLabel);
			this.labelPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelPanel.Location = new System.Drawing.Point(0, 0);
			this.labelPanel.Name = "labelPanel";
			this.labelPanel.Size = new System.Drawing.Size(95, 335);
			this.labelPanel.TabIndex = 28;
			// 
			// firstLabel
			// 
			this.firstLabel.AutoSize = true;
			this.firstLabel.Location = new System.Drawing.Point(24, 227);
			this.firstLabel.Name = "firstLabel";
			this.firstLabel.Size = new System.Drawing.Size(41, 12);
			this.firstLabel.TabIndex = 0;
			this.firstLabel.Text = "通道值";
			// 
			// secondLabel
			// 
			this.secondLabel.AutoSize = true;
			this.secondLabel.Location = new System.Drawing.Point(24, 253);
			this.secondLabel.Name = "secondLabel";
			this.secondLabel.Size = new System.Drawing.Size(41, 12);
			this.secondLabel.TabIndex = 0;
			this.secondLabel.Text = "跳渐变";
			// 
			// thirdLabel
			// 
			this.thirdLabel.AutoSize = true;
			this.thirdLabel.Location = new System.Drawing.Point(23, 278);
			this.thirdLabel.Name = "thirdLabel";
			this.thirdLabel.Size = new System.Drawing.Size(59, 12);
			this.thirdLabel.TabIndex = 0;
			this.thirdLabel.Text = "步时间(s)";
			// 
			// tdFlowLayoutPanel
			// 
			this.tdFlowLayoutPanel.AutoScroll = true;
			this.tdFlowLayoutPanel.BackColor = System.Drawing.SystemColors.Window;
			this.tdFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tdFlowLayoutPanel.Controls.Add(this.tdPanelDemo);
			this.tdFlowLayoutPanel.Controls.Add(this.saPanelDemo);
			this.tdFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tdFlowLayoutPanel.Location = new System.Drawing.Point(95, 0);
			this.tdFlowLayoutPanel.Name = "tdFlowLayoutPanel";
			this.tdFlowLayoutPanel.Size = new System.Drawing.Size(994, 335);
			this.tdFlowLayoutPanel.TabIndex = 54;
			this.tdFlowLayoutPanel.Tag = "9999";
			this.tdFlowLayoutPanel.WrapContents = false;
			// 
			// tdPanelDemo
			// 
			this.tdPanelDemo.Controls.Add(this.tdNameLabelDemo);
			this.tdPanelDemo.Controls.Add(this.tdNoLabelDemo);
			this.tdPanelDemo.Controls.Add(this.tdCmComboBoxDemo);
			this.tdPanelDemo.Controls.Add(this.tdStNUDDemo);
			this.tdPanelDemo.Controls.Add(this.tdValueNUDDemo);
			this.tdPanelDemo.Controls.Add(this.tdTrackBarDemo);
			this.tdPanelDemo.Location = new System.Drawing.Point(1, 1);
			this.tdPanelDemo.Margin = new System.Windows.Forms.Padding(1);
			this.tdPanelDemo.Name = "tdPanelDemo";
			this.tdPanelDemo.Size = new System.Drawing.Size(60, 297);
			this.tdPanelDemo.TabIndex = 24;
			this.tdPanelDemo.Visible = false;
			// 
			// tdNameLabelDemo
			// 
			this.tdNameLabelDemo.Font = new System.Drawing.Font("宋体", 8F);
			this.tdNameLabelDemo.Location = new System.Drawing.Point(11, 47);
			this.tdNameLabelDemo.Name = "tdNameLabelDemo";
			this.tdNameLabelDemo.Size = new System.Drawing.Size(14, 153);
			this.tdNameLabelDemo.TabIndex = 23;
			this.tdNameLabelDemo.Text = "x/y轴转速";
			this.tdNameLabelDemo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.tdNameLabelDemo.Click += new System.EventHandler(this.tdNameNumLabels_Click);
			// 
			// tdNoLabelDemo
			// 
			this.tdNoLabelDemo.AutoSize = true;
			this.tdNoLabelDemo.Location = new System.Drawing.Point(8, 18);
			this.tdNoLabelDemo.Name = "tdNoLabelDemo";
			this.tdNoLabelDemo.Size = new System.Drawing.Size(47, 12);
			this.tdNoLabelDemo.TabIndex = 3;
			this.tdNoLabelDemo.Text = "通道555";
			this.tdNoLabelDemo.Click += new System.EventHandler(this.tdNameNumLabels_Click);
			// 
			// tdCmComboBoxDemo
			// 
			this.tdCmComboBoxDemo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tdCmComboBoxDemo.FormattingEnabled = true;
			this.tdCmComboBoxDemo.Location = new System.Drawing.Point(8, 247);
			this.tdCmComboBoxDemo.Name = "tdCmComboBoxDemo";
			this.tdCmComboBoxDemo.Size = new System.Drawing.Size(50, 20);
			this.tdCmComboBoxDemo.TabIndex = 2;
			this.tdCmComboBoxDemo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.unifyTd_KeyPress);
			// 
			// tdStNUDDemo
			// 
			this.tdStNUDDemo.DecimalPlaces = 2;
			this.tdStNUDDemo.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tdStNUDDemo.Increment = new decimal(new int[] {
            3,
            0,
            0,
            131072});
			this.tdStNUDDemo.Location = new System.Drawing.Point(8, 271);
			this.tdStNUDDemo.Name = "tdStNUDDemo";
			this.tdStNUDDemo.Size = new System.Drawing.Size(50, 20);
			this.tdStNUDDemo.TabIndex = 1;
			this.tdStNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.tdStNUDDemo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.unifyTd_KeyPress);
			// 
			// tdValueNUDDemo
			// 
			this.tdValueNUDDemo.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tdValueNUDDemo.Location = new System.Drawing.Point(8, 223);
			this.tdValueNUDDemo.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.tdValueNUDDemo.Name = "tdValueNUDDemo";
			this.tdValueNUDDemo.Size = new System.Drawing.Size(50, 20);
			this.tdValueNUDDemo.TabIndex = 1;
			this.tdValueNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.tdValueNUDDemo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.unifyTd_KeyPress);
			// 
			// tdTrackBarDemo
			// 
			this.tdTrackBarDemo.AutoSize = false;
			this.tdTrackBarDemo.BackColor = System.Drawing.SystemColors.Window;
			this.tdTrackBarDemo.Location = new System.Drawing.Point(23, 33);
			this.tdTrackBarDemo.Maximum = 255;
			this.tdTrackBarDemo.Name = "tdTrackBarDemo";
			this.tdTrackBarDemo.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.tdTrackBarDemo.Size = new System.Drawing.Size(35, 184);
			this.tdTrackBarDemo.TabIndex = 0;
			this.tdTrackBarDemo.TickFrequency = 0;
			this.tdTrackBarDemo.TickStyle = System.Windows.Forms.TickStyle.None;
			// 
			// saPanelDemo
			// 
			this.saPanelDemo.Controls.Add(this.saFLPDemo);
			this.saPanelDemo.Location = new System.Drawing.Point(63, 1);
			this.saPanelDemo.Margin = new System.Windows.Forms.Padding(1, 1, 4, 1);
			this.saPanelDemo.Name = "saPanelDemo";
			this.saPanelDemo.Size = new System.Drawing.Size(63, 297);
			this.saPanelDemo.TabIndex = 64;
			this.saPanelDemo.Visible = false;
			// 
			// saFLPDemo
			// 
			this.saFLPDemo.AutoScroll = true;
			this.saFLPDemo.Controls.Add(this.saLabelDemo);
			this.saFLPDemo.Controls.Add(this.saButtonDemo);
			this.saFLPDemo.Dock = System.Windows.Forms.DockStyle.Left;
			this.saFLPDemo.Location = new System.Drawing.Point(0, 0);
			this.saFLPDemo.Margin = new System.Windows.Forms.Padding(0);
			this.saFLPDemo.Name = "saFLPDemo";
			this.saFLPDemo.Size = new System.Drawing.Size(85, 297);
			this.saFLPDemo.TabIndex = 63;
			this.saFLPDemo.Visible = false;
			// 
			// saLabelDemo
			// 
			this.saLabelDemo.Location = new System.Drawing.Point(1, 1);
			this.saLabelDemo.Margin = new System.Windows.Forms.Padding(1);
			this.saLabelDemo.Name = "saLabelDemo";
			this.saLabelDemo.Size = new System.Drawing.Size(61, 36);
			this.saLabelDemo.TabIndex = 12;
			this.saLabelDemo.Text = "<-  ";
			this.saLabelDemo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// saButtonDemo
			// 
			this.saButtonDemo.Location = new System.Drawing.Point(1, 39);
			this.saButtonDemo.Margin = new System.Windows.Forms.Padding(1);
			this.saButtonDemo.Name = "saButtonDemo";
			this.saButtonDemo.Size = new System.Drawing.Size(61, 25);
			this.saButtonDemo.TabIndex = 8;
			this.saButtonDemo.Text = "子属性事";
			this.saButtonDemo.UseVisualStyleBackColor = true;
			// 
			// copyStepButton
			// 
			this.copyStepButton.Location = new System.Drawing.Point(678, 16);
			this.copyStepButton.Name = "copyStepButton";
			this.copyStepButton.Size = new System.Drawing.Size(75, 23);
			this.copyStepButton.TabIndex = 49;
			this.copyStepButton.Text = "复制步";
			this.copyStepButton.UseVisualStyleBackColor = true;
			this.copyStepButton.Click += new System.EventHandler(this.copyStepButton_Click);
			// 
			// pasteStepButton
			// 
			this.pasteStepButton.Location = new System.Drawing.Point(678, 47);
			this.pasteStepButton.Name = "pasteStepButton";
			this.pasteStepButton.Size = new System.Drawing.Size(75, 23);
			this.pasteStepButton.TabIndex = 49;
			this.pasteStepButton.Text = "粘贴步";
			this.pasteStepButton.UseVisualStyleBackColor = true;
			this.pasteStepButton.Click += new System.EventHandler(this.pasteStepButton_Click);
			// 
			// tdPanel
			// 
			this.tdPanel.Controls.Add(this.tdFlowLayoutPanel);
			this.tdPanel.Controls.Add(this.labelPanel);
			this.tdPanel.Controls.Add(this.unifyPanel);
			this.tdPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tdPanel.Location = new System.Drawing.Point(0, 413);
			this.tdPanel.Name = "tdPanel";
			this.tdPanel.Size = new System.Drawing.Size(1264, 335);
			this.tdPanel.TabIndex = 63;
			// 
			// unifyPanel
			// 
			this.unifyPanel.BackColor = System.Drawing.Color.White;
			this.unifyPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.unifyPanel.Controls.Add(this.groupButton);
			this.unifyPanel.Controls.Add(this.groupFlowLayoutPanel);
			this.unifyPanel.Controls.Add(this.soundListButton);
			this.unifyPanel.Controls.Add(this.detailMultiButton);
			this.unifyPanel.Controls.Add(this.multiButton);
			this.unifyPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.unifyPanel.Location = new System.Drawing.Point(1089, 0);
			this.unifyPanel.Name = "unifyPanel";
			this.unifyPanel.Size = new System.Drawing.Size(175, 335);
			this.unifyPanel.TabIndex = 64;
			// 
			// groupButton
			// 
			this.groupButton.Enabled = false;
			this.groupButton.Location = new System.Drawing.Point(7, 9);
			this.groupButton.Margin = new System.Windows.Forms.Padding(2);
			this.groupButton.Name = "groupButton";
			this.groupButton.Size = new System.Drawing.Size(75, 23);
			this.groupButton.TabIndex = 55;
			this.groupButton.Text = "灯具编组";
			this.groupButton.UseVisualStyleBackColor = true;
			this.groupButton.Click += new System.EventHandler(this.groupButton_Click);
			// 
			// groupFlowLayoutPanel
			// 
			this.groupFlowLayoutPanel.AutoScroll = true;
			this.groupFlowLayoutPanel.Controls.Add(this.groupInButtonDemo);
			this.groupFlowLayoutPanel.Controls.Add(this.groupDelButtonDemo);
			this.groupFlowLayoutPanel.Controls.Add(this.saButton);
			this.groupFlowLayoutPanel.Location = new System.Drawing.Point(4, 67);
			this.groupFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(1);
			this.groupFlowLayoutPanel.Name = "groupFlowLayoutPanel";
			this.groupFlowLayoutPanel.Size = new System.Drawing.Size(166, 262);
			this.groupFlowLayoutPanel.TabIndex = 63;
			// 
			// groupInButtonDemo
			// 
			this.groupInButtonDemo.Location = new System.Drawing.Point(3, 3);
			this.groupInButtonDemo.Name = "groupInButtonDemo";
			this.groupInButtonDemo.Size = new System.Drawing.Size(118, 25);
			this.groupInButtonDemo.TabIndex = 0;
			this.groupInButtonDemo.Text = "进入编组";
			this.groupInButtonDemo.UseVisualStyleBackColor = true;
			this.groupInButtonDemo.Visible = false;
			this.groupInButtonDemo.Click += new System.EventHandler(this.groupInButton_Click);
			// 
			// groupDelButtonDemo
			// 
			this.groupDelButtonDemo.Location = new System.Drawing.Point(127, 3);
			this.groupDelButtonDemo.Name = "groupDelButtonDemo";
			this.groupDelButtonDemo.Size = new System.Drawing.Size(32, 25);
			this.groupDelButtonDemo.TabIndex = 0;
			this.groupDelButtonDemo.Text = "-";
			this.groupDelButtonDemo.UseVisualStyleBackColor = true;
			this.groupDelButtonDemo.Visible = false;
			this.groupDelButtonDemo.Click += new System.EventHandler(this.groupDelButton_Click);
			// 
			// saButton
			// 
			this.saButton.Location = new System.Drawing.Point(3, 34);
			this.saButton.Name = "saButton";
			this.saButton.Size = new System.Drawing.Size(154, 25);
			this.saButton.TabIndex = 1;
			this.saButton.Text = "子属性事件";
			this.saButton.UseVisualStyleBackColor = true;
			this.saButton.Visible = false;
			this.saButton.Click += new System.EventHandler(this.saButton_Click);
			// 
			// soundListButton
			// 
			this.soundListButton.BackColor = System.Drawing.Color.Transparent;
			this.soundListButton.Enabled = false;
			this.soundListButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.soundListButton.Location = new System.Drawing.Point(7, 36);
			this.soundListButton.Margin = new System.Windows.Forms.Padding(2);
			this.soundListButton.Name = "soundListButton";
			this.soundListButton.Size = new System.Drawing.Size(75, 24);
			this.soundListButton.TabIndex = 55;
			this.soundListButton.Text = "音频链表";
			this.soundListButton.UseVisualStyleBackColor = false;
			this.soundListButton.Click += new System.EventHandler(this.soundListButton_Click);
			// 
			// detailMultiButton
			// 
			this.detailMultiButton.BackColor = System.Drawing.Color.Transparent;
			this.detailMultiButton.Enabled = false;
			this.detailMultiButton.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.detailMultiButton.Location = new System.Drawing.Point(91, 36);
			this.detailMultiButton.Margin = new System.Windows.Forms.Padding(2);
			this.detailMultiButton.Name = "detailMultiButton";
			this.detailMultiButton.Size = new System.Drawing.Size(75, 24);
			this.detailMultiButton.TabIndex = 55;
			this.detailMultiButton.Text = "多步联调";
			this.detailMultiButton.UseVisualStyleBackColor = false;
			this.detailMultiButton.Click += new System.EventHandler(this.detailMultiButton_Click);
			this.detailMultiButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.detailMultiButton_MouseDown);
			// 
			// multiButton
			// 
			this.multiButton.BackColor = System.Drawing.Color.Transparent;
			this.multiButton.Enabled = false;
			this.multiButton.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.multiButton.Location = new System.Drawing.Point(91, 9);
			this.multiButton.Margin = new System.Windows.Forms.Padding(2);
			this.multiButton.Name = "multiButton";
			this.multiButton.Size = new System.Drawing.Size(75, 24);
			this.multiButton.TabIndex = 55;
			this.multiButton.Text = "多步调节";
			this.multiButton.UseVisualStyleBackColor = false;
			this.multiButton.Click += new System.EventHandler(this.multiButton_Click);
			this.multiButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.multiButton_MouseDown);
			// 
			// playBasePanel
			// 
			this.playBasePanel.BackColor = System.Drawing.Color.White;
			this.playBasePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.playBasePanel.Controls.Add(this.wjTestButton);
			this.playBasePanel.Controls.Add(this.testButton2);
			this.playBasePanel.Controls.Add(this.testButton1);
			this.playBasePanel.Controls.Add(this.myStatusStrip);
			this.playBasePanel.Controls.Add(this.playPanel);
			this.playBasePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.playBasePanel.Location = new System.Drawing.Point(0, 748);
			this.playBasePanel.Name = "playBasePanel";
			this.playBasePanel.Size = new System.Drawing.Size(1264, 96);
			this.playBasePanel.TabIndex = 67;
			// 
			// wjTestButton
			// 
			this.wjTestButton.Location = new System.Drawing.Point(6, 7);
			this.wjTestButton.Name = "wjTestButton";
			this.wjTestButton.Size = new System.Drawing.Size(84, 54);
			this.wjTestButton.TabIndex = 35;
			this.wjTestButton.Text = "wjTest";
			this.wjTestButton.UseVisualStyleBackColor = true;
			this.wjTestButton.Click += new System.EventHandler(this.wjTestButton_Click);
			// 
			// testButton2
			// 
			this.testButton2.Location = new System.Drawing.Point(1174, 7);
			this.testButton2.Name = "testButton2";
			this.testButton2.Size = new System.Drawing.Size(84, 54);
			this.testButton2.TabIndex = 34;
			this.testButton2.Text = "Test2";
			this.testButton2.UseVisualStyleBackColor = true;
			this.testButton2.Click += new System.EventHandler(this.testButton2_Click);
			// 
			// testButton1
			// 
			this.testButton1.Location = new System.Drawing.Point(1088, 7);
			this.testButton1.Name = "testButton1";
			this.testButton1.Size = new System.Drawing.Size(84, 54);
			this.testButton1.TabIndex = 34;
			this.testButton1.Text = "Test1";
			this.testButton1.UseVisualStyleBackColor = true;
			this.testButton1.Visible = false;
			this.testButton1.Click += new System.EventHandler(this.testButton1_Click);
			// 
			// myStatusStrip
			// 
			this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.myStatusStrip.Location = new System.Drawing.Point(0, 70);
			this.myStatusStrip.Name = "myStatusStrip";
			this.myStatusStrip.Size = new System.Drawing.Size(1260, 22);
			this.myStatusStrip.SizingGrip = false;
			this.myStatusStrip.TabIndex = 33;
			this.myStatusStrip.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(1245, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// skinComboBox
			// 
			this.skinComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.skinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.skinComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.skinComboBox.FormattingEnabled = true;
			this.skinComboBox.Location = new System.Drawing.Point(1145, 6);
			this.skinComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.skinComboBox.Name = "skinComboBox";
			this.skinComboBox.Size = new System.Drawing.Size(118, 20);
			this.skinComboBox.TabIndex = 52;
			this.skinComboBox.Visible = false;
			this.skinComboBox.SelectedIndexChanged += new System.EventHandler(this.skinComboBox_SelectedIndexChanged);
			// 
			// projectPanel
			// 
			this.projectPanel.AutoScroll = true;
			this.projectPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.projectPanel.Controls.Add(this.newProjectButton);
			this.projectPanel.Controls.Add(this.exportButton);
			this.projectPanel.Controls.Add(this.useFrameButton);
			this.projectPanel.Controls.Add(this.openProjectButton);
			this.projectPanel.Controls.Add(this.saveProjectButton);
			this.projectPanel.Controls.Add(this.saveFrameButton);
			this.projectPanel.Controls.Add(this.closeProjectButton);
			this.projectPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.projectPanel.Location = new System.Drawing.Point(0, 0);
			this.projectPanel.Name = "projectPanel";
			this.projectPanel.Size = new System.Drawing.Size(95, 383);
			this.projectPanel.TabIndex = 69;
			// 
			// newProjectButton
			// 
			this.newProjectButton.BackColor = System.Drawing.Color.Coral;
			this.newProjectButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.newProjectButton.Location = new System.Drawing.Point(10, 16);
			this.newProjectButton.Margin = new System.Windows.Forms.Padding(2);
			this.newProjectButton.Name = "newProjectButton";
			this.newProjectButton.Size = new System.Drawing.Size(74, 40);
			this.newProjectButton.TabIndex = 26;
			this.newProjectButton.Text = "新建工程";
			this.newProjectButton.UseVisualStyleBackColor = false;
			this.newProjectButton.Click += new System.EventHandler(this.newProjectButton_Click);
			// 
			// exportButton
			// 
			this.exportButton.Enabled = false;
			this.exportButton.Location = new System.Drawing.Point(11, 268);
			this.exportButton.Margin = new System.Windows.Forms.Padding(2);
			this.exportButton.Name = "exportButton";
			this.exportButton.Size = new System.Drawing.Size(74, 40);
			this.exportButton.TabIndex = 29;
			this.exportButton.Text = "导出工程";
			this.exportButton.UseVisualStyleBackColor = true;
			this.exportButton.Click += new System.EventHandler(this.exportProjectButton_Click);
			this.exportButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.exportProjectButton_MouseDown);
			// 
			// useFrameButton
			// 
			this.useFrameButton.Enabled = false;
			this.useFrameButton.Location = new System.Drawing.Point(10, 124);
			this.useFrameButton.Name = "useFrameButton";
			this.useFrameButton.Size = new System.Drawing.Size(74, 40);
			this.useFrameButton.TabIndex = 49;
			this.useFrameButton.Text = "调用场景";
			this.useFrameButton.UseVisualStyleBackColor = true;
			this.useFrameButton.Click += new System.EventHandler(this.useFrameButton_Click);
			// 
			// openProjectButton
			// 
			this.openProjectButton.BackColor = System.Drawing.Color.CadetBlue;
			this.openProjectButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.openProjectButton.Location = new System.Drawing.Point(10, 65);
			this.openProjectButton.Margin = new System.Windows.Forms.Padding(2);
			this.openProjectButton.Name = "openProjectButton";
			this.openProjectButton.Size = new System.Drawing.Size(74, 40);
			this.openProjectButton.TabIndex = 27;
			this.openProjectButton.Text = "打开工程";
			this.openProjectButton.UseVisualStyleBackColor = false;
			this.openProjectButton.Click += new System.EventHandler(this.openProjectButton_Click);
			// 
			// saveProjectButton
			// 
			this.saveProjectButton.Enabled = false;
			this.saveProjectButton.Location = new System.Drawing.Point(10, 220);
			this.saveProjectButton.Margin = new System.Windows.Forms.Padding(2);
			this.saveProjectButton.Name = "saveProjectButton";
			this.saveProjectButton.Size = new System.Drawing.Size(74, 40);
			this.saveProjectButton.TabIndex = 31;
			this.saveProjectButton.Text = "保存工程";
			this.saveProjectButton.UseVisualStyleBackColor = true;
			this.saveProjectButton.Click += new System.EventHandler(this.saveProjectButton_Click);
			this.saveProjectButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.saveProjectButton_MouseDown);
			// 
			// saveFrameButton
			// 
			this.saveFrameButton.Enabled = false;
			this.saveFrameButton.Location = new System.Drawing.Point(10, 172);
			this.saveFrameButton.Margin = new System.Windows.Forms.Padding(2);
			this.saveFrameButton.Name = "saveFrameButton";
			this.saveFrameButton.Size = new System.Drawing.Size(74, 40);
			this.saveFrameButton.TabIndex = 31;
			this.saveFrameButton.Text = "保存场景";
			this.saveFrameButton.UseVisualStyleBackColor = true;
			this.saveFrameButton.Click += new System.EventHandler(this.saveFrameButton_Click);
			// 
			// closeProjectButton
			// 
			this.closeProjectButton.Enabled = false;
			this.closeProjectButton.Location = new System.Drawing.Point(11, 330);
			this.closeProjectButton.Margin = new System.Windows.Forms.Padding(2);
			this.closeProjectButton.Name = "closeProjectButton";
			this.closeProjectButton.Size = new System.Drawing.Size(74, 40);
			this.closeProjectButton.TabIndex = 31;
			this.closeProjectButton.Text = "关闭工程";
			this.closeProjectButton.UseVisualStyleBackColor = true;
			this.closeProjectButton.Click += new System.EventHandler(this.closeProjectButton_Click);
			// 
			// lightsListView
			// 
			this.lightsListView.BackColor = System.Drawing.SystemColors.Window;
			this.lightsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lightType});
			this.lightsListView.ContextMenuStrip = this.myContextMenuStrip;
			this.lightsListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lightsListView.HideSelection = false;
			this.lightsListView.Location = new System.Drawing.Point(95, 0);
			this.lightsListView.Margin = new System.Windows.Forms.Padding(2);
			this.lightsListView.Name = "lightsListView";
			this.lightsListView.Size = new System.Drawing.Size(994, 296);
			this.lightsListView.TabIndex = 50;
			this.lightsListView.UseCompatibleStateImageBehavior = false;
			this.lightsListView.SelectedIndexChanged += new System.EventHandler(this.lightsListView_SelectedIndexChanged);
			this.lightsListView.DoubleClick += new System.EventHandler(this.lightsListView_DoubleClick);
			// 
			// lightType
			// 
			this.lightType.Text = "LightType";
			this.lightType.Width = 414;
			// 
			// stepBasePanel
			// 
			this.stepBasePanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.stepBasePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.stepBasePanel.Controls.Add(this.stepPanel);
			this.stepBasePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.stepBasePanel.Location = new System.Drawing.Point(95, 296);
			this.stepBasePanel.Name = "stepBasePanel";
			this.stepBasePanel.Size = new System.Drawing.Size(1169, 87);
			this.stepBasePanel.TabIndex = 68;
			// 
			// stepPanel
			// 
			this.stepPanel.BackColor = System.Drawing.Color.Transparent;
			this.stepPanel.Controls.Add(this.chooseStepButton);
			this.stepPanel.Controls.Add(this.saveMaterialButton);
			this.stepPanel.Controls.Add(this.modeComboBox);
			this.stepPanel.Controls.Add(this.frameComboBox);
			this.stepPanel.Controls.Add(this.stepLabel);
			this.stepPanel.Controls.Add(this.frameChooseLabel);
			this.stepPanel.Controls.Add(this.chooseStepNumericUpDown);
			this.stepPanel.Controls.Add(this.pasteStepButton);
			this.stepPanel.Controls.Add(this.copyStepButton);
			this.stepPanel.Controls.Add(this.modeChooseLabel);
			this.stepPanel.Controls.Add(this.syncButton);
			this.stepPanel.Controls.Add(this.multiLightButton);
			this.stepPanel.Controls.Add(this.backStepButton);
			this.stepPanel.Controls.Add(this.multiPasteButton);
			this.stepPanel.Controls.Add(this.insertButton);
			this.stepPanel.Controls.Add(this.nextStepButton);
			this.stepPanel.Controls.Add(this.actionButton);
			this.stepPanel.Controls.Add(this.useMaterialButton);
			this.stepPanel.Controls.Add(this.deleteStepButton);
			this.stepPanel.Controls.Add(this.multiplexButton);
			this.stepPanel.Controls.Add(this.multiCopyButton);
			this.stepPanel.Controls.Add(this.addStepButton);
			this.stepPanel.Enabled = false;
			this.stepPanel.Location = new System.Drawing.Point(84, 0);
			this.stepPanel.Name = "stepPanel";
			this.stepPanel.Size = new System.Drawing.Size(1035, 83);
			this.stepPanel.TabIndex = 65;
			this.stepPanel.Tag = "";
			// 
			// chooseStepButton
			// 
			this.chooseStepButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chooseStepButton.Location = new System.Drawing.Point(593, 16);
			this.chooseStepButton.Name = "chooseStepButton";
			this.chooseStepButton.Size = new System.Drawing.Size(28, 23);
			this.chooseStepButton.TabIndex = 54;
			this.chooseStepButton.Text = "->";
			this.chooseStepButton.UseVisualStyleBackColor = true;
			this.chooseStepButton.Click += new System.EventHandler(this.chooseStepButton_Click);
			// 
			// saveMaterialButton
			// 
			this.saveMaterialButton.Location = new System.Drawing.Point(849, 16);
			this.saveMaterialButton.Name = "saveMaterialButton";
			this.saveMaterialButton.Size = new System.Drawing.Size(75, 23);
			this.saveMaterialButton.TabIndex = 49;
			this.saveMaterialButton.Text = "保存素材";
			this.saveMaterialButton.UseVisualStyleBackColor = true;
			this.saveMaterialButton.Click += new System.EventHandler(this.saveMaterialButton_Click);
			// 
			// modeComboBox
			// 
			this.modeComboBox.Enabled = false;
			this.modeComboBox.FormattingEnabled = true;
			this.modeComboBox.Location = new System.Drawing.Point(68, 49);
			this.modeComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.modeComboBox.Name = "modeComboBox";
			this.modeComboBox.Size = new System.Drawing.Size(84, 20);
			this.modeComboBox.TabIndex = 18;
			this.modeComboBox.SelectedIndexChanged += new System.EventHandler(this.modeComboBox_SelectedIndexChanged);
			// 
			// frameComboBox
			// 
			this.frameComboBox.Enabled = false;
			this.frameComboBox.FormattingEnabled = true;
			this.frameComboBox.Location = new System.Drawing.Point(68, 17);
			this.frameComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.frameComboBox.Name = "frameComboBox";
			this.frameComboBox.Size = new System.Drawing.Size(84, 20);
			this.frameComboBox.TabIndex = 17;
			this.frameComboBox.SelectedIndexChanged += new System.EventHandler(this.frameComboBox_SelectedIndexChanged);
			// 
			// stepLabel
			// 
			this.stepLabel.AutoSize = true;
			this.stepLabel.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.stepLabel.ForeColor = System.Drawing.Color.White;
			this.stepLabel.Location = new System.Drawing.Point(374, 20);
			this.stepLabel.Name = "stepLabel";
			this.stepLabel.Size = new System.Drawing.Size(71, 14);
			this.stepLabel.TabIndex = 53;
			this.stepLabel.Tag = "999";
			this.stepLabel.Text = "  0 /  0";
			// 
			// frameChooseLabel
			// 
			this.frameChooseLabel.AutoSize = true;
			this.frameChooseLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.frameChooseLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.frameChooseLabel.Location = new System.Drawing.Point(23, 21);
			this.frameChooseLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.frameChooseLabel.Name = "frameChooseLabel";
			this.frameChooseLabel.Size = new System.Drawing.Size(41, 12);
			this.frameChooseLabel.TabIndex = 20;
			this.frameChooseLabel.Tag = "999";
			this.frameChooseLabel.Text = "场景：";
			// 
			// chooseStepNumericUpDown
			// 
			this.chooseStepNumericUpDown.Location = new System.Drawing.Point(546, 17);
			this.chooseStepNumericUpDown.Name = "chooseStepNumericUpDown";
			this.chooseStepNumericUpDown.Size = new System.Drawing.Size(47, 21);
			this.chooseStepNumericUpDown.TabIndex = 52;
			this.chooseStepNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// modeChooseLabel
			// 
			this.modeChooseLabel.AutoSize = true;
			this.modeChooseLabel.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.modeChooseLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.modeChooseLabel.Location = new System.Drawing.Point(23, 53);
			this.modeChooseLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.modeChooseLabel.Name = "modeChooseLabel";
			this.modeChooseLabel.Size = new System.Drawing.Size(41, 12);
			this.modeChooseLabel.TabIndex = 19;
			this.modeChooseLabel.Tag = "999";
			this.modeChooseLabel.Text = "模式：";
			// 
			// syncButton
			// 
			this.syncButton.BackColor = System.Drawing.Color.Transparent;
			this.syncButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.syncButton.FlatAppearance.BorderSize = 0;
			this.syncButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.syncButton.ImageKey = "复制步.png";
			this.syncButton.Location = new System.Drawing.Point(934, 16);
			this.syncButton.Name = "syncButton";
			this.syncButton.Size = new System.Drawing.Size(75, 23);
			this.syncButton.TabIndex = 49;
			this.syncButton.Text = "进入同步";
			this.syncButton.UseVisualStyleBackColor = false;
			this.syncButton.Click += new System.EventHandler(this.syncButton_Click);
			// 
			// multiLightButton
			// 
			this.multiLightButton.BackColor = System.Drawing.Color.Transparent;
			this.multiLightButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.multiLightButton.FlatAppearance.BorderSize = 0;
			this.multiLightButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.multiLightButton.ImageKey = "复制步.png";
			this.multiLightButton.Location = new System.Drawing.Point(185, 16);
			this.multiLightButton.Name = "multiLightButton";
			this.multiLightButton.Size = new System.Drawing.Size(75, 55);
			this.multiLightButton.TabIndex = 49;
			this.multiLightButton.Text = "多灯模式";
			this.multiLightButton.UseVisualStyleBackColor = false;
			this.multiLightButton.Click += new System.EventHandler(this.multiLightButton_Click);
			// 
			// backStepButton
			// 
			this.backStepButton.Location = new System.Drawing.Point(290, 16);
			this.backStepButton.Name = "backStepButton";
			this.backStepButton.Size = new System.Drawing.Size(75, 23);
			this.backStepButton.TabIndex = 49;
			this.backStepButton.Text = "上一步";
			this.backStepButton.UseVisualStyleBackColor = true;
			this.backStepButton.Click += new System.EventHandler(this.backStepButton_Click);
			this.backStepButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.backStepButton_MouseDown);
			// 
			// multiPasteButton
			// 
			this.multiPasteButton.Location = new System.Drawing.Point(763, 48);
			this.multiPasteButton.Name = "multiPasteButton";
			this.multiPasteButton.Size = new System.Drawing.Size(75, 23);
			this.multiPasteButton.TabIndex = 49;
			this.multiPasteButton.Text = "粘贴多步";
			this.multiPasteButton.UseVisualStyleBackColor = true;
			this.multiPasteButton.Click += new System.EventHandler(this.multiPasteButton_Click);
			// 
			// insertButton
			// 
			this.insertButton.Location = new System.Drawing.Point(290, 48);
			this.insertButton.Name = "insertButton";
			this.insertButton.Size = new System.Drawing.Size(75, 23);
			this.insertButton.TabIndex = 49;
			this.insertButton.Text = "插入步";
			this.insertButton.UseVisualStyleBackColor = true;
			this.insertButton.Click += new System.EventHandler(this.insertStepButton_Click);
			this.insertButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.insertAfterButton_MouseDown);
			// 
			// nextStepButton
			// 
			this.nextStepButton.Location = new System.Drawing.Point(460, 16);
			this.nextStepButton.Name = "nextStepButton";
			this.nextStepButton.Size = new System.Drawing.Size(75, 23);
			this.nextStepButton.TabIndex = 49;
			this.nextStepButton.Text = "下一步";
			this.nextStepButton.UseVisualStyleBackColor = true;
			this.nextStepButton.Click += new System.EventHandler(this.nextStepButton_Click);
			this.nextStepButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.nextStepButton_MouseDown);
			// 
			// actionButton
			// 
			this.actionButton.Location = new System.Drawing.Point(546, 48);
			this.actionButton.Name = "actionButton";
			this.actionButton.Size = new System.Drawing.Size(75, 23);
			this.actionButton.TabIndex = 49;
			this.actionButton.Text = "内置动作";
			this.actionButton.UseVisualStyleBackColor = true;
			this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
			this.actionButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.actionButton_MouseDown);
			// 
			// useMaterialButton
			// 
			this.useMaterialButton.Location = new System.Drawing.Point(849, 48);
			this.useMaterialButton.Name = "useMaterialButton";
			this.useMaterialButton.Size = new System.Drawing.Size(75, 23);
			this.useMaterialButton.TabIndex = 49;
			this.useMaterialButton.Text = "使用素材";
			this.useMaterialButton.UseVisualStyleBackColor = true;
			this.useMaterialButton.Click += new System.EventHandler(this.useMaterialButton_Click);
			// 
			// deleteStepButton
			// 
			this.deleteStepButton.Location = new System.Drawing.Point(460, 48);
			this.deleteStepButton.Name = "deleteStepButton";
			this.deleteStepButton.Size = new System.Drawing.Size(75, 23);
			this.deleteStepButton.TabIndex = 49;
			this.deleteStepButton.Text = "删除步";
			this.deleteStepButton.UseVisualStyleBackColor = true;
			this.deleteStepButton.Click += new System.EventHandler(this.deleteStepButton_Click);
			this.deleteStepButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.deleteStepButton_MouseDown);
			// 
			// multiplexButton
			// 
			this.multiplexButton.Enabled = false;
			this.multiplexButton.Location = new System.Drawing.Point(934, 48);
			this.multiplexButton.Name = "multiplexButton";
			this.multiplexButton.Size = new System.Drawing.Size(75, 23);
			this.multiplexButton.TabIndex = 49;
			this.multiplexButton.Text = "多步复用";
			this.multiplexButton.UseVisualStyleBackColor = true;
			this.multiplexButton.Click += new System.EventHandler(this.multiplexButton_Click);
			// 
			// multiCopyButton
			// 
			this.multiCopyButton.Location = new System.Drawing.Point(763, 16);
			this.multiCopyButton.Name = "multiCopyButton";
			this.multiCopyButton.Size = new System.Drawing.Size(75, 23);
			this.multiCopyButton.TabIndex = 49;
			this.multiCopyButton.Text = "复制多步";
			this.multiCopyButton.UseVisualStyleBackColor = true;
			this.multiCopyButton.Click += new System.EventHandler(this.multiCopyButton_Click);
			// 
			// addStepButton
			// 
			this.addStepButton.Location = new System.Drawing.Point(375, 48);
			this.addStepButton.Name = "addStepButton";
			this.addStepButton.Size = new System.Drawing.Size(75, 23);
			this.addStepButton.TabIndex = 49;
			this.addStepButton.Text = "追加步";
			this.addStepButton.UseVisualStyleBackColor = true;
			this.addStepButton.Click += new System.EventHandler(this.addStepButton_Click);
			this.addStepButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.addStepButton_MouseDown);
			// 
			// topPanel
			// 
			this.topPanel.Controls.Add(this.lightsListView);
			this.topPanel.Controls.Add(this.lightInfoPanel);
			this.topPanel.Controls.Add(this.stepBasePanel);
			this.topPanel.Controls.Add(this.projectPanel);
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.topPanel.Location = new System.Drawing.Point(0, 30);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(1264, 383);
			this.topPanel.TabIndex = 70;
			// 
			// lightInfoPanel
			// 
			this.lightInfoPanel.AutoScroll = true;
			this.lightInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lightInfoPanel.Controls.Add(this.lightsAddrLabel);
			this.lightInfoPanel.Controls.Add(this.currentLightPictureBox);
			this.lightInfoPanel.Controls.Add(this.lightRemarkLabel);
			this.lightInfoPanel.Controls.Add(this.lightTypeLabel);
			this.lightInfoPanel.Controls.Add(this.lightNameLabel);
			this.lightInfoPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.lightInfoPanel.Location = new System.Drawing.Point(1089, 0);
			this.lightInfoPanel.Name = "lightInfoPanel";
			this.lightInfoPanel.Size = new System.Drawing.Size(175, 296);
			this.lightInfoPanel.TabIndex = 9;
			// 
			// lightsAddrLabel
			// 
			this.lightsAddrLabel.AutoEllipsis = true;
			this.lightsAddrLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightsAddrLabel.ForeColor = System.Drawing.Color.Black;
			this.lightsAddrLabel.Location = new System.Drawing.Point(4, 201);
			this.lightsAddrLabel.Name = "lightsAddrLabel";
			this.lightsAddrLabel.Size = new System.Drawing.Size(144, 90);
			this.lightsAddrLabel.TabIndex = 5;
			// 
			// currentLightPictureBox
			// 
			this.currentLightPictureBox.InitialImage = null;
			this.currentLightPictureBox.Location = new System.Drawing.Point(32, 8);
			this.currentLightPictureBox.Name = "currentLightPictureBox";
			this.currentLightPictureBox.Size = new System.Drawing.Size(110, 115);
			this.currentLightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.currentLightPictureBox.TabIndex = 6;
			this.currentLightPictureBox.TabStop = false;
			// 
			// lightRemarkLabel
			// 
			this.lightRemarkLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightRemarkLabel.ForeColor = System.Drawing.Color.Black;
			this.lightRemarkLabel.Location = new System.Drawing.Point(4, 177);
			this.lightRemarkLabel.Name = "lightRemarkLabel";
			this.lightRemarkLabel.Size = new System.Drawing.Size(144, 18);
			this.lightRemarkLabel.TabIndex = 7;
			this.lightRemarkLabel.Text = " ";
			// 
			// NewMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.ClientSize = new System.Drawing.Size(1264, 844);
			this.Controls.Add(this.topPanel);
			this.Controls.Add(this.skinComboBox);
			this.Controls.Add(this.tdPanel);
			this.Controls.Add(this.playBasePanel);
			this.Controls.Add(this.mainMenuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(1280, 883);
			this.Name = "NewMainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewMainForm_FormClosing);
			this.Load += new System.EventHandler(this.NewMainForm_Load);
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.playPanel.ResumeLayout(false);
			this.myContextMenuStrip.ResumeLayout(false);
			this.labelPanel.ResumeLayout(false);
			this.labelPanel.PerformLayout();
			this.tdFlowLayoutPanel.ResumeLayout(false);
			this.tdPanelDemo.ResumeLayout(false);
			this.tdPanelDemo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tdStNUDDemo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tdValueNUDDemo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tdTrackBarDemo)).EndInit();
			this.saPanelDemo.ResumeLayout(false);
			this.saFLPDemo.ResumeLayout(false);
			this.tdPanel.ResumeLayout(false);
			this.unifyPanel.ResumeLayout(false);
			this.groupFlowLayoutPanel.ResumeLayout(false);
			this.playBasePanel.ResumeLayout(false);
			this.playBasePanel.PerformLayout();
			this.myStatusStrip.ResumeLayout(false);
			this.myStatusStrip.PerformLayout();
			this.projectPanel.ResumeLayout(false);
			this.stepBasePanel.ResumeLayout(false);
			this.stepPanel.ResumeLayout(false);
			this.stepPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.chooseStepNumericUpDown)).EndInit();
			this.topPanel.ResumeLayout(false);
			this.lightInfoPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.currentLightPictureBox)).EndInit();
			this.ResumeLayout(false);

		}



		#endregion
		private Sunisoft.IrisSkin.SkinEngine skinEngine1;
		private System.Windows.Forms.MenuStrip mainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem lightLibraryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hardwareSetToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hardwareSetNewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hardwareSetOpenToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hardwareUpdateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.Panel playPanel;
		private System.Windows.Forms.Button deviceRefreshButton;
		private System.Windows.Forms.ComboBox deviceComboBox;
		private System.Windows.Forms.Button deviceConnectButton;
		private System.Windows.Forms.Button previewButton;
		private System.Windows.Forms.Button makeSoundButton;
		private System.Windows.Forms.Button changeConnectMethodButton;
		private System.Windows.Forms.Label lightTypeLabel;
		private System.Windows.Forms.Label lightNameLabel;
		private System.Windows.Forms.PictureBox currentLightPictureBox;
		private System.Windows.Forms.Panel labelPanel;
		private System.Windows.Forms.Label thirdLabel;
		private System.Windows.Forms.Label secondLabel;
		private System.Windows.Forms.Label firstLabel;
		private System.Windows.Forms.FlowLayoutPanel tdFlowLayoutPanel;
		private System.Windows.Forms.Panel tdPanel;
		private System.Windows.Forms.Panel playBasePanel;
		private System.Windows.Forms.ComboBox skinComboBox;
		private System.Windows.Forms.Panel projectPanel;
		private System.Windows.Forms.Button newProjectButton;
		private System.Windows.Forms.Button exportButton;
		private System.Windows.Forms.Button useFrameButton;
		private System.Windows.Forms.Button openProjectButton;
		private System.Windows.Forms.Button saveProjectButton;
		private System.Windows.Forms.Button saveFrameButton;
		private System.Windows.Forms.Button closeProjectButton;
		private System.Windows.Forms.ListView lightsListView;
		private System.Windows.Forms.ColumnHeader lightType;
		private System.Windows.Forms.Panel stepBasePanel;
		private System.Windows.Forms.Panel stepPanel;
		private System.Windows.Forms.Button saveMaterialButton;
		private System.Windows.Forms.ComboBox modeComboBox;
		private System.Windows.Forms.ComboBox frameComboBox;
		private System.Windows.Forms.Label stepLabel;
		private System.Windows.Forms.Label frameChooseLabel;
		private System.Windows.Forms.NumericUpDown chooseStepNumericUpDown;
		private System.Windows.Forms.Label modeChooseLabel;
		private System.Windows.Forms.Button syncButton;
		private System.Windows.Forms.Button multiLightButton;
		private System.Windows.Forms.Button backStepButton;
		private System.Windows.Forms.Button multiPasteButton;
		private System.Windows.Forms.Button insertButton;
		private System.Windows.Forms.Button nextStepButton;
		private System.Windows.Forms.Button useMaterialButton;
		private System.Windows.Forms.Button deleteStepButton;
		private System.Windows.Forms.Button multiCopyButton;
		private System.Windows.Forms.Button copyStepButton;
		private System.Windows.Forms.Button addStepButton;
		private System.Windows.Forms.Button pasteStepButton;
		private System.Windows.Forms.Panel topPanel;
		
		private Panel tdPanelDemo;
		private Label tdNameLabelDemo;
		private Label tdNoLabelDemo;
		private ComboBox tdCmComboBoxDemo;
		private NumericUpDown tdStNUDDemo;
		private NumericUpDown tdValueNUDDemo;
		private TrackBar tdTrackBarDemo;
		private Button chooseStepButton;
		private Button keepButton;
		private StatusStrip myStatusStrip;
		private ToolStripStatusLabel myStatusLabel;
		private ContextMenuStrip myContextMenuStrip;
		private ToolStripMenuItem hideMenuStriplToolStripMenuItem;
		private ToolStripMenuItem hideProjectPanelToolStripMenuItem;
		private ToolStripMenuItem hideUnifyPanelToolStripMenuItem;
		private ToolStripMenuItem hidePlayPanelToolStripMenuItem;
		private Panel lightInfoPanel;
		private ToolStripMenuItem refreshPicToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator4;
		private Panel unifyPanel;
		private Button multiButton;
		private Button testButton1;
		private Button testButton2;
		private Button wjTestButton;
		private Label lightRemarkLabel;
		private ToolStripMenuItem helpToolStripMenuItem;
		private Button multiplexButton;
		private Button groupButton;
		private FlowLayoutPanel groupFlowLayoutPanel;
		private Button groupInButtonDemo;
		private Button groupDelButtonDemo;
		private ToolTip groupToolTip;
		private ToolTip saToolTip;
        private Button soundListButton;
		private Button saButton;
		private ToolStripMenuItem 使用说明ToolStripMenuItem;
		private ToolStripMenuItem 更新日志ToolStripMenuItem;
		private FlowLayoutPanel saFLPDemo;
		private Button saButtonDemo;
		private Label saLabelDemo;
		private ToolStripMenuItem toolStripMenuItem1;
		private ToolStripMenuItem lightListToolStripMenuItem;
		private ToolStripMenuItem globalSetToolStripMenuItem;
		private ToolStripMenuItem ymSetToolStripMenuItem;
		private ToolStripMenuItem projectUpdateToolStripMenuItem;
		private ToolStripMenuItem toolStripMenuItem2;
		private Button actionButton;
		private Panel saPanelDemo;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem showSaPanelsToolStripMenuItem;
		private Button detailMultiButton;
		private Label lightsAddrLabel;
	}
}