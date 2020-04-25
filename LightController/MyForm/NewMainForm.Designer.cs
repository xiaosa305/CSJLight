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
			this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lightListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.globalSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ymToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.projectUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.QDControllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CenterControllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.KeyPressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playPanel = new System.Windows.Forms.Panel();
			this.changeConnectMethodButton = new System.Windows.Forms.Button();
			this.deviceRefreshButton = new System.Windows.Forms.Button();
			this.realtimeButton = new System.Windows.Forms.Button();
			this.keepButton = new System.Windows.Forms.Button();
			this.deviceComboBox = new System.Windows.Forms.ComboBox();
			this.deviceConnectButton = new System.Windows.Forms.Button();
			this.makeSoundButton = new System.Windows.Forms.Button();
			this.endviewButton = new System.Windows.Forms.Button();
			this.previewButton = new System.Windows.Forms.Button();
			this.lightsAddrLabel = new System.Windows.Forms.Label();
			this.lightTypeLabel = new System.Windows.Forms.Label();
			this.lightNameLabel = new System.Windows.Forms.Label();
			this.myContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.refreshPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.hideMenuStriplToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hideProjectPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hideUnifyPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hidePlayPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.labelPanel = new System.Windows.Forms.Panel();
			this.firstLabel = new System.Windows.Forms.Label();
			this.secondLabel = new System.Windows.Forms.Label();
			this.thirdLabel = new System.Windows.Forms.Label();
			this.tdFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.tdPanel1 = new System.Windows.Forms.Panel();
			this.tdNameLabel1 = new System.Windows.Forms.Label();
			this.tdNoLabel1 = new System.Windows.Forms.Label();
			this.tdCmComboBox1 = new System.Windows.Forms.ComboBox();
			this.tdStNumericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.tdValueNumericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.tdTrackBar1 = new System.Windows.Forms.TrackBar();
			this.tdPanel = new System.Windows.Forms.Panel();
			this.unifyPanel = new System.Windows.Forms.Panel();
			this.saFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.zeroButton = new System.Windows.Forms.Button();
			this.multiButton = new System.Windows.Forms.Button();
			this.initButton = new System.Windows.Forms.Button();
			this.unifyChangeModeComboBox = new System.Windows.Forms.ComboBox();
			this.unifyValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.unifyStepTimeButton = new System.Windows.Forms.Button();
			this.unifyStepTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.unifyValueButton = new System.Windows.Forms.Button();
			this.unifyChangeModeButton = new System.Windows.Forms.Button();
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
			this.lightImageList = new System.Windows.Forms.ImageList(this.components);
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
			this.insertAfterButton = new System.Windows.Forms.Button();
			this.nextStepButton = new System.Windows.Forms.Button();
			this.useMaterialButton = new System.Windows.Forms.Button();
			this.deleteStepButton = new System.Windows.Forms.Button();
			this.multiCopyButton = new System.Windows.Forms.Button();
			this.insertBeforeButton = new System.Windows.Forms.Button();
			this.copyStepButton = new System.Windows.Forms.Button();
			this.addStepButton = new System.Windows.Forms.Button();
			this.pasteStepButton = new System.Windows.Forms.Button();
			this.topPanel = new System.Windows.Forms.Panel();
			this.lightInfoPanel = new System.Windows.Forms.Panel();
			this.currentLightPictureBox = new System.Windows.Forms.PictureBox();
			this.lightRemarkLabel = new System.Windows.Forms.Label();
			this.lightLargeImageList = new System.Windows.Forms.ImageList(this.components);
			this.mainMenuStrip.SuspendLayout();
			this.playPanel.SuspendLayout();
			this.myContextMenuStrip.SuspendLayout();
			this.labelPanel.SuspendLayout();
			this.tdFlowLayoutPanel.SuspendLayout();
			this.tdPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tdStNumericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tdValueNumericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tdTrackBar1)).BeginInit();
			this.tdPanel.SuspendLayout();
			this.unifyPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.unifyValueNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.unifyStepTimeNumericUpDown)).BeginInit();
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
            this.projectToolStripMenuItem,
            this.otherToolStripMenuItem,
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
			// projectToolStripMenuItem
			// 
			this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightListToolStripMenuItem,
            this.globalSetToolStripMenuItem,
            this.ymToolStripMenuItem,
            this.toolStripSeparator2,
            this.projectUpdateToolStripMenuItem});
			this.projectToolStripMenuItem.Enabled = false;
			this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
			this.projectToolStripMenuItem.Size = new System.Drawing.Size(68, 26);
			this.projectToolStripMenuItem.Text = "工程相关";
			// 
			// lightListToolStripMenuItem
			// 
			this.lightListToolStripMenuItem.Name = "lightListToolStripMenuItem";
			this.lightListToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.lightListToolStripMenuItem.Text = "灯具列表";
			this.lightListToolStripMenuItem.Click += new System.EventHandler(this.lightListToolStripMenuItem_Click);
			// 
			// globalSetToolStripMenuItem
			// 
			this.globalSetToolStripMenuItem.Name = "globalSetToolStripMenuItem";
			this.globalSetToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.globalSetToolStripMenuItem.Text = "全局配置";
			this.globalSetToolStripMenuItem.Click += new System.EventHandler(this.globalSetToolStripMenuItem_Click);
			// 
			// ymToolStripMenuItem
			// 
			this.ymToolStripMenuItem.Name = "ymToolStripMenuItem";
			this.ymToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.ymToolStripMenuItem.Text = "摇麦配置";
			this.ymToolStripMenuItem.Click += new System.EventHandler(this.ymToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(121, 6);
			// 
			// projectUpdateToolStripMenuItem
			// 
			this.projectUpdateToolStripMenuItem.Name = "projectUpdateToolStripMenuItem";
			this.projectUpdateToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.projectUpdateToolStripMenuItem.Text = "工程更新";
			this.projectUpdateToolStripMenuItem.Click += new System.EventHandler(this.projectUpdateToolStripMenuItem_Click);
			// 
			// otherToolStripMenuItem
			// 
			this.otherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator1,
            this.QDControllerToolStripMenuItem,
            this.CenterControllerToolStripMenuItem,
            this.KeyPressToolStripMenuItem});
			this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
			this.otherToolStripMenuItem.Size = new System.Drawing.Size(68, 26);
			this.otherToolStripMenuItem.Text = "其他工具";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.newToolStripMenuItem.Text = "外设配置";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
			// 
			// QDControllerToolStripMenuItem
			// 
			this.QDControllerToolStripMenuItem.Name = "QDControllerToolStripMenuItem";
			this.QDControllerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.QDControllerToolStripMenuItem.Text = "传视界灯控工具";
			this.QDControllerToolStripMenuItem.Click += new System.EventHandler(this.QDControllerToolStripMenuItem_Click);
			// 
			// CenterControllerToolStripMenuItem
			// 
			this.CenterControllerToolStripMenuItem.Name = "CenterControllerToolStripMenuItem";
			this.CenterControllerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.CenterControllerToolStripMenuItem.Text = "传视界中控工具";
			this.CenterControllerToolStripMenuItem.Click += new System.EventHandler(this.CenterControllerToolStripMenuItem_Click);
			// 
			// KeyPressToolStripMenuItem
			// 
			this.KeyPressToolStripMenuItem.Name = "KeyPressToolStripMenuItem";
			this.KeyPressToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.KeyPressToolStripMenuItem.Text = "传视界墙板工具";
			this.KeyPressToolStripMenuItem.Click += new System.EventHandler(this.KeyPressToolStripMenuItem_Click);
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
			this.playPanel.Controls.Add(this.realtimeButton);
			this.playPanel.Controls.Add(this.keepButton);
			this.playPanel.Controls.Add(this.deviceComboBox);
			this.playPanel.Controls.Add(this.deviceConnectButton);
			this.playPanel.Controls.Add(this.makeSoundButton);
			this.playPanel.Controls.Add(this.endviewButton);
			this.playPanel.Controls.Add(this.previewButton);
			this.playPanel.Location = new System.Drawing.Point(272, 1);
			this.playPanel.Name = "playPanel";
			this.playPanel.Size = new System.Drawing.Size(708, 68);
			this.playPanel.TabIndex = 30;
			this.playPanel.Visible = false;
			// 
			// changeConnectMethodButton
			// 
			this.changeConnectMethodButton.Location = new System.Drawing.Point(17, 6);
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
			// realtimeButton
			// 
			this.realtimeButton.Enabled = false;
			this.realtimeButton.Location = new System.Drawing.Point(307, 6);
			this.realtimeButton.Margin = new System.Windows.Forms.Padding(2);
			this.realtimeButton.Name = "realtimeButton";
			this.realtimeButton.Size = new System.Drawing.Size(69, 54);
			this.realtimeButton.TabIndex = 25;
			this.realtimeButton.Text = "实时调试";
			this.realtimeButton.UseVisualStyleBackColor = true;
			this.realtimeButton.Click += new System.EventHandler(this.realtimeButton_Click);
			// 
			// keepButton
			// 
			this.keepButton.Enabled = false;
			this.keepButton.Location = new System.Drawing.Point(386, 6);
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
			this.deviceConnectButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// makeSoundButton
			// 
			this.makeSoundButton.Enabled = false;
			this.makeSoundButton.Location = new System.Drawing.Point(544, 6);
			this.makeSoundButton.Margin = new System.Windows.Forms.Padding(2);
			this.makeSoundButton.Name = "makeSoundButton";
			this.makeSoundButton.Size = new System.Drawing.Size(69, 54);
			this.makeSoundButton.TabIndex = 25;
			this.makeSoundButton.Text = "触发音频";
			this.makeSoundButton.UseVisualStyleBackColor = true;
			this.makeSoundButton.Click += new System.EventHandler(this.makeSoundButton_Click);
			// 
			// endviewButton
			// 
			this.endviewButton.Enabled = false;
			this.endviewButton.Location = new System.Drawing.Point(623, 6);
			this.endviewButton.Margin = new System.Windows.Forms.Padding(2);
			this.endviewButton.Name = "endviewButton";
			this.endviewButton.Size = new System.Drawing.Size(69, 54);
			this.endviewButton.TabIndex = 24;
			this.endviewButton.Text = "结束预览";
			this.endviewButton.UseVisualStyleBackColor = true;
			this.endviewButton.Click += new System.EventHandler(this.endviewButton_Click);
			// 
			// previewButton
			// 
			this.previewButton.Enabled = false;
			this.previewButton.Location = new System.Drawing.Point(465, 6);
			this.previewButton.Margin = new System.Windows.Forms.Padding(2);
			this.previewButton.Name = "previewButton";
			this.previewButton.Size = new System.Drawing.Size(69, 54);
			this.previewButton.TabIndex = 24;
			this.previewButton.Text = "预览效果";
			this.previewButton.UseVisualStyleBackColor = true;
			this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
			// 
			// lightsAddrLabel
			// 
			this.lightsAddrLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightsAddrLabel.ForeColor = System.Drawing.Color.Black;
			this.lightsAddrLabel.Location = new System.Drawing.Point(4, 210);
			this.lightsAddrLabel.Name = "lightsAddrLabel";
			this.lightsAddrLabel.Size = new System.Drawing.Size(165, 83);
			this.lightsAddrLabel.TabIndex = 5;
			this.lightsAddrLabel.Text = " ";
			// 
			// lightTypeLabel
			// 
			this.lightTypeLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightTypeLabel.ForeColor = System.Drawing.Color.Black;
			this.lightTypeLabel.Location = new System.Drawing.Point(4, 156);
			this.lightTypeLabel.Name = "lightTypeLabel";
			this.lightTypeLabel.Size = new System.Drawing.Size(166, 18);
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
			this.lightNameLabel.Size = new System.Drawing.Size(166, 18);
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
            this.hidePlayPanelToolStripMenuItem});
			this.myContextMenuStrip.Name = "myContextMenuStrip";
			this.myContextMenuStrip.Size = new System.Drawing.Size(173, 120);
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
			this.tdFlowLayoutPanel.Controls.Add(this.tdPanel1);
			this.tdFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tdFlowLayoutPanel.Location = new System.Drawing.Point(95, 0);
			this.tdFlowLayoutPanel.Name = "tdFlowLayoutPanel";
			this.tdFlowLayoutPanel.Size = new System.Drawing.Size(994, 335);
			this.tdFlowLayoutPanel.TabIndex = 54;
			this.tdFlowLayoutPanel.Tag = "9999";
			this.tdFlowLayoutPanel.WrapContents = false;
			// 
			// tdPanel1
			// 
			this.tdPanel1.Controls.Add(this.tdNameLabel1);
			this.tdPanel1.Controls.Add(this.tdNoLabel1);
			this.tdPanel1.Controls.Add(this.tdCmComboBox1);
			this.tdPanel1.Controls.Add(this.tdStNumericUpDown1);
			this.tdPanel1.Controls.Add(this.tdValueNumericUpDown1);
			this.tdPanel1.Controls.Add(this.tdTrackBar1);
			this.tdPanel1.Location = new System.Drawing.Point(3, 3);
			this.tdPanel1.Name = "tdPanel1";
			this.tdPanel1.Size = new System.Drawing.Size(84, 297);
			this.tdPanel1.TabIndex = 24;
			this.tdPanel1.Visible = false;
			// 
			// tdNameLabel1
			// 
			this.tdNameLabel1.Font = new System.Drawing.Font("宋体", 8F);
			this.tdNameLabel1.Location = new System.Drawing.Point(17, 47);
			this.tdNameLabel1.Name = "tdNameLabel1";
			this.tdNameLabel1.Size = new System.Drawing.Size(14, 153);
			this.tdNameLabel1.TabIndex = 23;
			this.tdNameLabel1.Text = "x/y轴转速";
			this.tdNameLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// tdNoLabel1
			// 
			this.tdNoLabel1.AutoSize = true;
			this.tdNoLabel1.Location = new System.Drawing.Point(15, 18);
			this.tdNoLabel1.Name = "tdNoLabel1";
			this.tdNoLabel1.Size = new System.Drawing.Size(47, 12);
			this.tdNoLabel1.TabIndex = 3;
			this.tdNoLabel1.Text = "通道555";
			// 
			// tdCmComboBox1
			// 
			this.tdCmComboBox1.FormattingEnabled = true;
			this.tdCmComboBox1.Location = new System.Drawing.Point(17, 247);
			this.tdCmComboBox1.Name = "tdCmComboBox1";
			this.tdCmComboBox1.Size = new System.Drawing.Size(51, 20);
			this.tdCmComboBox1.TabIndex = 2;
			// 
			// tdStNumericUpDown1
			// 
			this.tdStNumericUpDown1.DecimalPlaces = 2;
			this.tdStNumericUpDown1.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tdStNumericUpDown1.Increment = new decimal(new int[] {
            3,
            0,
            0,
            131072});
			this.tdStNumericUpDown1.Location = new System.Drawing.Point(17, 271);
			this.tdStNumericUpDown1.Name = "tdStNumericUpDown1";
			this.tdStNumericUpDown1.Size = new System.Drawing.Size(51, 20);
			this.tdStNumericUpDown1.TabIndex = 1;
			this.tdStNumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tdValueNumericUpDown1
			// 
			this.tdValueNumericUpDown1.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tdValueNumericUpDown1.Location = new System.Drawing.Point(17, 223);
			this.tdValueNumericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.tdValueNumericUpDown1.Name = "tdValueNumericUpDown1";
			this.tdValueNumericUpDown1.Size = new System.Drawing.Size(50, 20);
			this.tdValueNumericUpDown1.TabIndex = 1;
			this.tdValueNumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tdTrackBar1
			// 
			this.tdTrackBar1.AutoSize = false;
			this.tdTrackBar1.BackColor = System.Drawing.SystemColors.Window;
			this.tdTrackBar1.Location = new System.Drawing.Point(33, 33);
			this.tdTrackBar1.Maximum = 255;
			this.tdTrackBar1.Name = "tdTrackBar1";
			this.tdTrackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.tdTrackBar1.Size = new System.Drawing.Size(35, 184);
			this.tdTrackBar1.TabIndex = 0;
			this.tdTrackBar1.TickFrequency = 0;
			this.tdTrackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
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
			this.unifyPanel.Controls.Add(this.saFlowLayoutPanel);
			this.unifyPanel.Controls.Add(this.zeroButton);
			this.unifyPanel.Controls.Add(this.multiButton);
			this.unifyPanel.Controls.Add(this.initButton);
			this.unifyPanel.Controls.Add(this.unifyChangeModeComboBox);
			this.unifyPanel.Controls.Add(this.unifyValueNumericUpDown);
			this.unifyPanel.Controls.Add(this.unifyStepTimeButton);
			this.unifyPanel.Controls.Add(this.unifyStepTimeNumericUpDown);
			this.unifyPanel.Controls.Add(this.unifyValueButton);
			this.unifyPanel.Controls.Add(this.unifyChangeModeButton);
			this.unifyPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.unifyPanel.Location = new System.Drawing.Point(1089, 0);
			this.unifyPanel.Name = "unifyPanel";
			this.unifyPanel.Size = new System.Drawing.Size(175, 335);
			this.unifyPanel.TabIndex = 64;
			// 
			// saFlowLayoutPanel
			// 
			this.saFlowLayoutPanel.AutoScroll = true;
			this.saFlowLayoutPanel.Location = new System.Drawing.Point(4, 5);
			this.saFlowLayoutPanel.Name = "saFlowLayoutPanel";
			this.saFlowLayoutPanel.Size = new System.Drawing.Size(166, 128);
			this.saFlowLayoutPanel.TabIndex = 63;
			// 
			// zeroButton
			// 
			this.zeroButton.Enabled = false;
			this.zeroButton.Location = new System.Drawing.Point(8, 171);
			this.zeroButton.Margin = new System.Windows.Forms.Padding(2);
			this.zeroButton.Name = "zeroButton";
			this.zeroButton.Size = new System.Drawing.Size(75, 23);
			this.zeroButton.TabIndex = 56;
			this.zeroButton.Text = "全部归零";
			this.zeroButton.UseVisualStyleBackColor = true;
			this.zeroButton.Click += new System.EventHandler(this.zeroButton_Click);
			// 
			// multiButton
			// 
			this.multiButton.Enabled = false;
			this.multiButton.Location = new System.Drawing.Point(91, 169);
			this.multiButton.Margin = new System.Windows.Forms.Padding(2);
			this.multiButton.Name = "multiButton";
			this.multiButton.Size = new System.Drawing.Size(75, 50);
			this.multiButton.TabIndex = 55;
			this.multiButton.Text = "多步调节";
			this.multiButton.UseVisualStyleBackColor = true;
			this.multiButton.Click += new System.EventHandler(this.multiButton_Click);
			// 
			// initButton
			// 
			this.initButton.Enabled = false;
			this.initButton.Location = new System.Drawing.Point(8, 198);
			this.initButton.Margin = new System.Windows.Forms.Padding(2);
			this.initButton.Name = "initButton";
			this.initButton.Size = new System.Drawing.Size(75, 23);
			this.initButton.TabIndex = 55;
			this.initButton.Text = "设为初值";
			this.initButton.UseVisualStyleBackColor = true;
			this.initButton.Click += new System.EventHandler(this.initButton_Click);
			// 
			// unifyChangeModeComboBox
			// 
			this.unifyChangeModeComboBox.Enabled = false;
			this.unifyChangeModeComboBox.FormattingEnabled = true;
			this.unifyChangeModeComboBox.Items.AddRange(new object[] {
            "跳变",
            "渐变",
            "屏蔽"});
			this.unifyChangeModeComboBox.Location = new System.Drawing.Point(10, 268);
			this.unifyChangeModeComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.unifyChangeModeComboBox.Name = "unifyChangeModeComboBox";
			this.unifyChangeModeComboBox.Size = new System.Drawing.Size(58, 20);
			this.unifyChangeModeComboBox.TabIndex = 62;
			// 
			// unifyValueNumericUpDown
			// 
			this.unifyValueNumericUpDown.Enabled = false;
			this.unifyValueNumericUpDown.Location = new System.Drawing.Point(10, 237);
			this.unifyValueNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.unifyValueNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.unifyValueNumericUpDown.Name = "unifyValueNumericUpDown";
			this.unifyValueNumericUpDown.Size = new System.Drawing.Size(57, 21);
			this.unifyValueNumericUpDown.TabIndex = 60;
			this.unifyValueNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// unifyStepTimeButton
			// 
			this.unifyStepTimeButton.Enabled = false;
			this.unifyStepTimeButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.unifyStepTimeButton.Location = new System.Drawing.Point(82, 299);
			this.unifyStepTimeButton.Margin = new System.Windows.Forms.Padding(2);
			this.unifyStepTimeButton.Name = "unifyStepTimeButton";
			this.unifyStepTimeButton.Size = new System.Drawing.Size(84, 23);
			this.unifyStepTimeButton.TabIndex = 59;
			this.unifyStepTimeButton.Text = "统一步时间";
			this.unifyStepTimeButton.UseVisualStyleBackColor = true;
			this.unifyStepTimeButton.Click += new System.EventHandler(this.unifyStepTimeButton_Click);
			// 
			// unifyStepTimeNumericUpDown
			// 
			this.unifyStepTimeNumericUpDown.DecimalPlaces = 2;
			this.unifyStepTimeNumericUpDown.Enabled = false;
			this.unifyStepTimeNumericUpDown.Location = new System.Drawing.Point(10, 300);
			this.unifyStepTimeNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.unifyStepTimeNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.unifyStepTimeNumericUpDown.Name = "unifyStepTimeNumericUpDown";
			this.unifyStepTimeNumericUpDown.Size = new System.Drawing.Size(57, 21);
			this.unifyStepTimeNumericUpDown.TabIndex = 61;
			this.unifyStepTimeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.unifyStepTimeNumericUpDown.ValueChanged += new System.EventHandler(this.unifyStepTimeNumericUpDown_ValueChanged);
			// 
			// unifyValueButton
			// 
			this.unifyValueButton.Enabled = false;
			this.unifyValueButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.unifyValueButton.Location = new System.Drawing.Point(82, 235);
			this.unifyValueButton.Margin = new System.Windows.Forms.Padding(2);
			this.unifyValueButton.Name = "unifyValueButton";
			this.unifyValueButton.Size = new System.Drawing.Size(84, 23);
			this.unifyValueButton.TabIndex = 58;
			this.unifyValueButton.Text = "统一通道值";
			this.unifyValueButton.UseVisualStyleBackColor = true;
			this.unifyValueButton.Click += new System.EventHandler(this.unifyValueButton_Click);
			// 
			// unifyChangeModeButton
			// 
			this.unifyChangeModeButton.Enabled = false;
			this.unifyChangeModeButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.unifyChangeModeButton.Location = new System.Drawing.Point(82, 267);
			this.unifyChangeModeButton.Margin = new System.Windows.Forms.Padding(2);
			this.unifyChangeModeButton.Name = "unifyChangeModeButton";
			this.unifyChangeModeButton.Size = new System.Drawing.Size(84, 23);
			this.unifyChangeModeButton.TabIndex = 57;
			this.unifyChangeModeButton.Text = "统一跳渐变";
			this.unifyChangeModeButton.UseVisualStyleBackColor = true;
			this.unifyChangeModeButton.Click += new System.EventHandler(this.unifyChangeModeButton_Click);
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
			this.newProjectButton.Location = new System.Drawing.Point(10, 16);
			this.newProjectButton.Margin = new System.Windows.Forms.Padding(2);
			this.newProjectButton.Name = "newProjectButton";
			this.newProjectButton.Size = new System.Drawing.Size(74, 40);
			this.newProjectButton.TabIndex = 26;
			this.newProjectButton.Text = "新建工程";
			this.newProjectButton.UseVisualStyleBackColor = true;
			this.newProjectButton.Click += new System.EventHandler(this.newProjectButton_Click);
			// 
			// exportProjectButton
			// 
			this.exportButton.Enabled = false;
			this.exportButton.Location = new System.Drawing.Point(11, 281);
			this.exportButton.Margin = new System.Windows.Forms.Padding(2);
			this.exportButton.Name = "exportProjectButton";
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
			this.openProjectButton.Location = new System.Drawing.Point(10, 65);
			this.openProjectButton.Margin = new System.Windows.Forms.Padding(2);
			this.openProjectButton.Name = "openProjectButton";
			this.openProjectButton.Size = new System.Drawing.Size(74, 40);
			this.openProjectButton.TabIndex = 27;
			this.openProjectButton.Text = "打开工程";
			this.openProjectButton.UseVisualStyleBackColor = true;
			this.openProjectButton.Click += new System.EventHandler(this.openProjectButton_Click);
			// 
			// saveProjectButton
			// 
			this.saveProjectButton.Enabled = false;
			this.saveProjectButton.Location = new System.Drawing.Point(10, 222);
			this.saveProjectButton.Margin = new System.Windows.Forms.Padding(2);
			this.saveProjectButton.Name = "saveProjectButton";
			this.saveProjectButton.Size = new System.Drawing.Size(74, 40);
			this.saveProjectButton.TabIndex = 31;
			this.saveProjectButton.Text = "保存工程";
			this.saveProjectButton.UseVisualStyleBackColor = true;
			this.saveProjectButton.Click += new System.EventHandler(this.saveProjectButton_Click);
			// 
			// saveFrameButton
			// 
			this.saveFrameButton.Enabled = false;
			this.saveFrameButton.Location = new System.Drawing.Point(10, 173);
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
			this.lightsListView.LargeImageList = this.lightImageList;
			this.lightsListView.Location = new System.Drawing.Point(95, 0);
			this.lightsListView.Margin = new System.Windows.Forms.Padding(2);
			this.lightsListView.Name = "lightsListView";
			this.lightsListView.Size = new System.Drawing.Size(994, 296);
			this.lightsListView.TabIndex = 50;
			this.lightsListView.UseCompatibleStateImageBehavior = false;
			this.lightsListView.SelectedIndexChanged += new System.EventHandler(this.lightsListView_SelectedIndexChanged);
			this.lightsListView.DragOver += new System.Windows.Forms.DragEventHandler(this.lightsListView_DragOver);
			this.lightsListView.DoubleClick += new System.EventHandler(this.lightsListView_DoubleClick);
			this.lightsListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lightsListView_MouseDown);
			this.lightsListView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lightsListView_MouseMove);
			// 
			// lightType
			// 
			this.lightType.Text = "LightType";
			this.lightType.Width = 414;
			// 
			// lightImageList
			// 
			this.lightImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lightImageList.ImageStream")));
			this.lightImageList.TransparentColor = System.Drawing.Color.Gainsboro;
			this.lightImageList.Images.SetKeyName(0, "2.bmp");
			this.lightImageList.Images.SetKeyName(1, "3.bmp");
			this.lightImageList.Images.SetKeyName(2, "4.bmp");
			this.lightImageList.Images.SetKeyName(3, "5.bmp");
			this.lightImageList.Images.SetKeyName(4, "6.bmp");
			this.lightImageList.Images.SetKeyName(5, "7.bmp");
			this.lightImageList.Images.SetKeyName(6, "8.bmp");
			this.lightImageList.Images.SetKeyName(7, "9.bmp");
			this.lightImageList.Images.SetKeyName(8, "10.bmp");
			this.lightImageList.Images.SetKeyName(9, "11.bmp");
			this.lightImageList.Images.SetKeyName(10, "12.bmp");
			this.lightImageList.Images.SetKeyName(11, "13.bmp");
			this.lightImageList.Images.SetKeyName(12, "14.bmp");
			this.lightImageList.Images.SetKeyName(13, "15.bmp");
			this.lightImageList.Images.SetKeyName(14, "16.bmp");
			this.lightImageList.Images.SetKeyName(15, "17.bmp");
			this.lightImageList.Images.SetKeyName(16, "18.bmp");
			this.lightImageList.Images.SetKeyName(17, "19.bmp");
			this.lightImageList.Images.SetKeyName(18, "20.bmp");
			this.lightImageList.Images.SetKeyName(19, "21.bmp");
			this.lightImageList.Images.SetKeyName(20, "22.bmp");
			this.lightImageList.Images.SetKeyName(21, "23.bmp");
			this.lightImageList.Images.SetKeyName(22, "24.bmp");
			this.lightImageList.Images.SetKeyName(23, "25.bmp");
			this.lightImageList.Images.SetKeyName(24, "27.bmp");
			this.lightImageList.Images.SetKeyName(25, "28.bmp");
			this.lightImageList.Images.SetKeyName(26, "29.gif");
			this.lightImageList.Images.SetKeyName(27, "30.bmp");
			this.lightImageList.Images.SetKeyName(28, "31.bmp");
			this.lightImageList.Images.SetKeyName(29, "ledpar.bmp");
			this.lightImageList.Images.SetKeyName(30, "RGB.ico");
			this.lightImageList.Images.SetKeyName(31, "灯带.bmp");
			this.lightImageList.Images.SetKeyName(32, "二合一.bmp");
			this.lightImageList.Images.SetKeyName(33, "二合一50.bmp");
			this.lightImageList.Images.SetKeyName(34, "魔球.bmp");
			this.lightImageList.Images.SetKeyName(35, "帕灯.bmp");
			this.lightImageList.Images.SetKeyName(36, "未知.ico");
			this.lightImageList.Images.SetKeyName(37, "1.bmp");
			this.lightImageList.Images.SetKeyName(38, "1.jpg");
			this.lightImageList.Images.SetKeyName(39, "灯光图.png");
			this.lightImageList.Images.SetKeyName(40, "3.jpg");
			this.lightImageList.Images.SetKeyName(41, "4.jpg");
			this.lightImageList.Images.SetKeyName(42, "5.jpg");
			this.lightImageList.Images.SetKeyName(43, "60w.jpg");
			this.lightImageList.Images.SetKeyName(44, "j(1).png");
			this.lightImageList.Images.SetKeyName(45, "j(2).png");
			this.lightImageList.Images.SetKeyName(46, "j(3).png");
			this.lightImageList.Images.SetKeyName(47, "j(4).png");
			this.lightImageList.Images.SetKeyName(48, "j(5).png");
			this.lightImageList.Images.SetKeyName(49, "j(6).png");
			this.lightImageList.Images.SetKeyName(50, "j(7).png");
			this.lightImageList.Images.SetKeyName(51, "j(8).png");
			this.lightImageList.Images.SetKeyName(52, "j(9).png");
			this.lightImageList.Images.SetKeyName(53, "j(10).png");
			this.lightImageList.Images.SetKeyName(54, "j(11).png");
			this.lightImageList.Images.SetKeyName(55, "a (1).jpg");
			this.lightImageList.Images.SetKeyName(56, "a (1).png");
			this.lightImageList.Images.SetKeyName(57, "a (2).jpg");
			this.lightImageList.Images.SetKeyName(58, "a (2).png");
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
			this.stepPanel.Controls.Add(this.modeChooseLabel);
			this.stepPanel.Controls.Add(this.syncButton);
			this.stepPanel.Controls.Add(this.multiLightButton);
			this.stepPanel.Controls.Add(this.backStepButton);
			this.stepPanel.Controls.Add(this.multiPasteButton);
			this.stepPanel.Controls.Add(this.insertAfterButton);
			this.stepPanel.Controls.Add(this.nextStepButton);
			this.stepPanel.Controls.Add(this.useMaterialButton);
			this.stepPanel.Controls.Add(this.deleteStepButton);
			this.stepPanel.Controls.Add(this.multiCopyButton);
			this.stepPanel.Controls.Add(this.insertBeforeButton);
			this.stepPanel.Controls.Add(this.copyStepButton);
			this.stepPanel.Controls.Add(this.addStepButton);
			this.stepPanel.Controls.Add(this.pasteStepButton);
			this.stepPanel.Enabled = false;
			this.stepPanel.Location = new System.Drawing.Point(109, -1);
			this.stepPanel.Name = "stepPanel";
			this.stepPanel.Size = new System.Drawing.Size(938, 83);
			this.stepPanel.TabIndex = 65;
			this.stepPanel.Tag = "";
			// 
			// chooseStepButton
			// 
			this.chooseStepButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chooseStepButton.Location = new System.Drawing.Point(601, 16);
			this.chooseStepButton.Name = "chooseStepButton";
			this.chooseStepButton.Size = new System.Drawing.Size(28, 23);
			this.chooseStepButton.TabIndex = 54;
			this.chooseStepButton.Text = "->";
			this.chooseStepButton.UseVisualStyleBackColor = true;
			this.chooseStepButton.Click += new System.EventHandler(this.chooseStepButton_Click);
			// 
			// saveMaterialButton
			// 
			this.saveMaterialButton.Location = new System.Drawing.Point(829, 16);
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
			this.chooseStepNumericUpDown.Size = new System.Drawing.Size(54, 21);
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
			this.syncButton.Location = new System.Drawing.Point(183, 48);
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
			this.multiLightButton.Location = new System.Drawing.Point(183, 16);
			this.multiLightButton.Name = "multiLightButton";
			this.multiLightButton.Size = new System.Drawing.Size(75, 23);
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
			// 
			// multiPasteButton
			// 
			this.multiPasteButton.Location = new System.Drawing.Point(743, 48);
			this.multiPasteButton.Name = "multiPasteButton";
			this.multiPasteButton.Size = new System.Drawing.Size(75, 23);
			this.multiPasteButton.TabIndex = 49;
			this.multiPasteButton.Text = "粘贴多步";
			this.multiPasteButton.UseVisualStyleBackColor = true;
			this.multiPasteButton.Click += new System.EventHandler(this.multiPasteButton_Click);
			// 
			// insertAfterButton
			// 
			this.insertAfterButton.Location = new System.Drawing.Point(375, 48);
			this.insertAfterButton.Name = "insertAfterButton";
			this.insertAfterButton.Size = new System.Drawing.Size(75, 23);
			this.insertAfterButton.TabIndex = 49;
			this.insertAfterButton.Text = "后插步";
			this.insertAfterButton.UseVisualStyleBackColor = true;
			this.insertAfterButton.Click += new System.EventHandler(this.insertStepButton_Click);
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
			// 
			// useMaterialButton
			// 
			this.useMaterialButton.Location = new System.Drawing.Point(829, 48);
			this.useMaterialButton.Name = "useMaterialButton";
			this.useMaterialButton.Size = new System.Drawing.Size(75, 23);
			this.useMaterialButton.TabIndex = 49;
			this.useMaterialButton.Text = "使用素材";
			this.useMaterialButton.UseVisualStyleBackColor = true;
			this.useMaterialButton.Click += new System.EventHandler(this.useMaterialButton_Click);
			// 
			// deleteStepButton
			// 
			this.deleteStepButton.Location = new System.Drawing.Point(548, 48);
			this.deleteStepButton.Name = "deleteStepButton";
			this.deleteStepButton.Size = new System.Drawing.Size(75, 23);
			this.deleteStepButton.TabIndex = 49;
			this.deleteStepButton.Text = "删除步";
			this.deleteStepButton.UseVisualStyleBackColor = true;
			this.deleteStepButton.Click += new System.EventHandler(this.deleteStepButton_Click);
			// 
			// multiCopyButton
			// 
			this.multiCopyButton.Location = new System.Drawing.Point(743, 16);
			this.multiCopyButton.Name = "multiCopyButton";
			this.multiCopyButton.Size = new System.Drawing.Size(75, 23);
			this.multiCopyButton.TabIndex = 49;
			this.multiCopyButton.Text = "复制多步";
			this.multiCopyButton.UseVisualStyleBackColor = true;
			this.multiCopyButton.Click += new System.EventHandler(this.multiCopyButton_Click);
			// 
			// insertBeforeButton
			// 
			this.insertBeforeButton.Location = new System.Drawing.Point(290, 48);
			this.insertBeforeButton.Name = "insertBeforeButton";
			this.insertBeforeButton.Size = new System.Drawing.Size(75, 23);
			this.insertBeforeButton.TabIndex = 49;
			this.insertBeforeButton.Text = "前插步";
			this.insertBeforeButton.UseVisualStyleBackColor = true;
			this.insertBeforeButton.Click += new System.EventHandler(this.insertStepButton_Click);
			// 
			// copyStepButton
			// 
			this.copyStepButton.Location = new System.Drawing.Point(659, 16);
			this.copyStepButton.Name = "copyStepButton";
			this.copyStepButton.Size = new System.Drawing.Size(75, 23);
			this.copyStepButton.TabIndex = 49;
			this.copyStepButton.Text = "复制步";
			this.copyStepButton.UseVisualStyleBackColor = true;
			this.copyStepButton.Click += new System.EventHandler(this.copyStepButton_Click);
			// 
			// addStepButton
			// 
			this.addStepButton.Location = new System.Drawing.Point(460, 48);
			this.addStepButton.Name = "addStepButton";
			this.addStepButton.Size = new System.Drawing.Size(75, 23);
			this.addStepButton.TabIndex = 49;
			this.addStepButton.Text = "追加步";
			this.addStepButton.UseVisualStyleBackColor = true;
			this.addStepButton.Click += new System.EventHandler(this.addStepButton_Click);
			// 
			// pasteStepButton
			// 
			this.pasteStepButton.Location = new System.Drawing.Point(659, 48);
			this.pasteStepButton.Name = "pasteStepButton";
			this.pasteStepButton.Size = new System.Drawing.Size(75, 23);
			this.pasteStepButton.TabIndex = 49;
			this.pasteStepButton.Text = "粘贴步";
			this.pasteStepButton.UseVisualStyleBackColor = true;
			this.pasteStepButton.Click += new System.EventHandler(this.pasteStepButton_Click);
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
			this.lightInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lightInfoPanel.Controls.Add(this.currentLightPictureBox);
			this.lightInfoPanel.Controls.Add(this.lightsAddrLabel);
			this.lightInfoPanel.Controls.Add(this.lightRemarkLabel);
			this.lightInfoPanel.Controls.Add(this.lightTypeLabel);
			this.lightInfoPanel.Controls.Add(this.lightNameLabel);
			this.lightInfoPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.lightInfoPanel.Location = new System.Drawing.Point(1089, 0);
			this.lightInfoPanel.Name = "lightInfoPanel";
			this.lightInfoPanel.Size = new System.Drawing.Size(175, 296);
			this.lightInfoPanel.TabIndex = 9;
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
			this.lightRemarkLabel.Location = new System.Drawing.Point(4, 183);
			this.lightRemarkLabel.Name = "lightRemarkLabel";
			this.lightRemarkLabel.Size = new System.Drawing.Size(166, 18);
			this.lightRemarkLabel.TabIndex = 7;
			this.lightRemarkLabel.Text = " ";
			// 
			// lightLargeImageList
			// 
			this.lightLargeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lightLargeImageList.ImageStream")));
			this.lightLargeImageList.TransparentColor = System.Drawing.Color.Gainsboro;
			this.lightLargeImageList.Images.SetKeyName(0, "2.bmp");
			this.lightLargeImageList.Images.SetKeyName(1, "3.bmp");
			this.lightLargeImageList.Images.SetKeyName(2, "4.bmp");
			this.lightLargeImageList.Images.SetKeyName(3, "5.bmp");
			this.lightLargeImageList.Images.SetKeyName(4, "6.bmp");
			this.lightLargeImageList.Images.SetKeyName(5, "7.bmp");
			this.lightLargeImageList.Images.SetKeyName(6, "8.bmp");
			this.lightLargeImageList.Images.SetKeyName(7, "9.bmp");
			this.lightLargeImageList.Images.SetKeyName(8, "10.bmp");
			this.lightLargeImageList.Images.SetKeyName(9, "11.bmp");
			this.lightLargeImageList.Images.SetKeyName(10, "12.bmp");
			this.lightLargeImageList.Images.SetKeyName(11, "13.bmp");
			this.lightLargeImageList.Images.SetKeyName(12, "14.bmp");
			this.lightLargeImageList.Images.SetKeyName(13, "15.bmp");
			this.lightLargeImageList.Images.SetKeyName(14, "16.bmp");
			this.lightLargeImageList.Images.SetKeyName(15, "17.bmp");
			this.lightLargeImageList.Images.SetKeyName(16, "18.bmp");
			this.lightLargeImageList.Images.SetKeyName(17, "19.bmp");
			this.lightLargeImageList.Images.SetKeyName(18, "20.bmp");
			this.lightLargeImageList.Images.SetKeyName(19, "21.bmp");
			this.lightLargeImageList.Images.SetKeyName(20, "22.bmp");
			this.lightLargeImageList.Images.SetKeyName(21, "23.bmp");
			this.lightLargeImageList.Images.SetKeyName(22, "24.bmp");
			this.lightLargeImageList.Images.SetKeyName(23, "25.bmp");
			this.lightLargeImageList.Images.SetKeyName(24, "27.bmp");
			this.lightLargeImageList.Images.SetKeyName(25, "28.bmp");
			this.lightLargeImageList.Images.SetKeyName(26, "29.gif");
			this.lightLargeImageList.Images.SetKeyName(27, "30.bmp");
			this.lightLargeImageList.Images.SetKeyName(28, "31.bmp");
			this.lightLargeImageList.Images.SetKeyName(29, "ledpar.bmp");
			this.lightLargeImageList.Images.SetKeyName(30, "RGB.ico");
			this.lightLargeImageList.Images.SetKeyName(31, "灯带.bmp");
			this.lightLargeImageList.Images.SetKeyName(32, "二合一.bmp");
			this.lightLargeImageList.Images.SetKeyName(33, "二合一50.bmp");
			this.lightLargeImageList.Images.SetKeyName(34, "魔球.bmp");
			this.lightLargeImageList.Images.SetKeyName(35, "帕灯.bmp");
			this.lightLargeImageList.Images.SetKeyName(36, "未知.ico");
			this.lightLargeImageList.Images.SetKeyName(37, "1.bmp");
			this.lightLargeImageList.Images.SetKeyName(38, "1.jpg");
			this.lightLargeImageList.Images.SetKeyName(39, "灯光图.png");
			this.lightLargeImageList.Images.SetKeyName(40, "3.jpg");
			this.lightLargeImageList.Images.SetKeyName(41, "4.jpg");
			this.lightLargeImageList.Images.SetKeyName(42, "5.jpg");
			this.lightLargeImageList.Images.SetKeyName(43, "60w.jpg");
			this.lightLargeImageList.Images.SetKeyName(44, "j(1).png");
			this.lightLargeImageList.Images.SetKeyName(45, "j(2).png");
			this.lightLargeImageList.Images.SetKeyName(46, "j(3).png");
			this.lightLargeImageList.Images.SetKeyName(47, "j(4).png");
			this.lightLargeImageList.Images.SetKeyName(48, "j(5).png");
			this.lightLargeImageList.Images.SetKeyName(49, "j(6).png");
			this.lightLargeImageList.Images.SetKeyName(50, "j(7).png");
			this.lightLargeImageList.Images.SetKeyName(51, "j(8).png");
			this.lightLargeImageList.Images.SetKeyName(52, "j(9).png");
			this.lightLargeImageList.Images.SetKeyName(53, "j(10).png");
			this.lightLargeImageList.Images.SetKeyName(54, "j(11).png");
			this.lightLargeImageList.Images.SetKeyName(55, "a (1).jpg");
			this.lightLargeImageList.Images.SetKeyName(56, "a (1).png");
			this.lightLargeImageList.Images.SetKeyName(57, "a (2).jpg");
			this.lightLargeImageList.Images.SetKeyName(58, "a (2).png");
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
			this.tdPanel1.ResumeLayout(false);
			this.tdPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tdStNumericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tdValueNumericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tdTrackBar1)).EndInit();
			this.tdPanel.ResumeLayout(false);
			this.unifyPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.unifyValueNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.unifyStepTimeNumericUpDown)).EndInit();
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
		private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem QDControllerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CenterControllerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem KeyPressToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem lightListToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem globalSetToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ymToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem projectUpdateToolStripMenuItem;
		private System.Windows.Forms.Panel playPanel;
		private System.Windows.Forms.Button deviceRefreshButton;
		private System.Windows.Forms.ComboBox deviceComboBox;
		private System.Windows.Forms.Button deviceConnectButton;
		private System.Windows.Forms.Button previewButton;
		private System.Windows.Forms.Button makeSoundButton;
		private System.Windows.Forms.Button changeConnectMethodButton;
		private System.Windows.Forms.Label lightsAddrLabel;
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
		private System.Windows.Forms.Button insertAfterButton;
		private System.Windows.Forms.Button nextStepButton;
		private System.Windows.Forms.Button useMaterialButton;
		private System.Windows.Forms.Button deleteStepButton;
		private System.Windows.Forms.Button multiCopyButton;
		private System.Windows.Forms.Button insertBeforeButton;
		private System.Windows.Forms.Button copyStepButton;
		private System.Windows.Forms.Button addStepButton;
		private System.Windows.Forms.Button pasteStepButton;
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		private Panel[] tdPanels = new Panel[32];
		private Label[] tdNoLabels = new Label[32];
		private Label[] tdNameLabels = new Label[32];
		private TrackBar[] tdTrackBars = new TrackBar[32];
		private NumericUpDown[] tdValueNumericUpDowns = new NumericUpDown[32];
		private ComboBox[] tdCmComboBoxes = new ComboBox[32];
		private NumericUpDown[] tdStNumericUpDowns = new NumericUpDown[32];
		private Panel tdPanel1;
		private Label tdNameLabel1;
		private Label tdNoLabel1;
		private ComboBox tdCmComboBox1;
		private NumericUpDown tdStNumericUpDown1;
		private NumericUpDown tdValueNumericUpDown1;
		private TrackBar tdTrackBar1;
		private Button endviewButton;
		private Button chooseStepButton;
		private Button realtimeButton;
		private Button keepButton;
		private StatusStrip myStatusStrip;
		private ToolStripStatusLabel myStatusLabel;
		private ContextMenuStrip myContextMenuStrip;
		private ToolStripMenuItem hideMenuStriplToolStripMenuItem;
		private ToolStripMenuItem hideProjectPanelToolStripMenuItem;
		private ToolStripMenuItem hideUnifyPanelToolStripMenuItem;
		private ToolStripMenuItem hidePlayPanelToolStripMenuItem;
		private Panel lightInfoPanel;
		private ImageList lightImageList;
		private ToolStripMenuItem refreshPicToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator4;
		private Panel unifyPanel;
		private FlowLayoutPanel saFlowLayoutPanel;
		private Button zeroButton;
		private Button multiButton;
		private Button initButton;
		private ComboBox unifyChangeModeComboBox;
		private NumericUpDown unifyValueNumericUpDown;
		private Button unifyStepTimeButton;
		private NumericUpDown unifyStepTimeNumericUpDown;
		private Button unifyValueButton;
		private Button unifyChangeModeButton;
		private Button testButton1;
		private Button testButton2;
		private ImageList lightLargeImageList;
		private Button wjTestButton;
		private Label lightRemarkLabel;
	}
}