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
			this.传视界工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.QDControllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CenterControllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.KeyPressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playPanel = new System.Windows.Forms.Panel();
			this.changeConnectMethodButton = new System.Windows.Forms.Button();
			this.refreshDeviceButton = new System.Windows.Forms.Button();
			this.deviceComboBox = new System.Windows.Forms.ComboBox();
			this.connectButton = new System.Windows.Forms.Button();
			this.soundButton = new System.Windows.Forms.Button();
			this.endviewButton = new System.Windows.Forms.Button();
			this.previewButton = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.lightsAddrLabel = new System.Windows.Forms.Label();
			this.lightTypeLabel = new System.Windows.Forms.Label();
			this.lightNameLabel = new System.Windows.Forms.Label();
			this.currentLightPictureBox = new System.Windows.Forms.PictureBox();
			this.lightInfoGroupBox = new System.Windows.Forms.GroupBox();
			this.labelPanel = new System.Windows.Forms.Panel();
			this.thirdLabel1 = new System.Windows.Forms.Label();
			this.secondLabel1 = new System.Windows.Forms.Label();
			this.firstLabel1 = new System.Windows.Forms.Label();
			this.unifyChangeModeComboBox = new System.Windows.Forms.ComboBox();
			this.unifyValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.unifySteptimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.unifyChangemodeButton = new System.Windows.Forms.Button();
			this.unifyValueButton = new System.Windows.Forms.Button();
			this.unifySteptimeButton = new System.Windows.Forms.Button();
			this.initButton = new System.Windows.Forms.Button();
			this.zeroButton = new System.Windows.Forms.Button();
			this.tdFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.tdPanel1 = new System.Windows.Forms.Panel();
			this.tdNameLabel1 = new System.Windows.Forms.Label();
			this.tdNoLabel1 = new System.Windows.Forms.Label();
			this.tdCmComboBox1 = new System.Windows.Forms.ComboBox();
			this.tdStNumericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.tdValueNumericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.tdTrackBar1 = new System.Windows.Forms.TrackBar();
			this.buttonPanel = new System.Windows.Forms.Panel();
			this.unifyPanel = new System.Windows.Forms.Panel();
			this.multiButton = new System.Windows.Forms.Button();
			this.playBasePanel = new System.Windows.Forms.Panel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.skinComboBox = new System.Windows.Forms.ComboBox();
			this.panel7 = new System.Windows.Forms.Panel();
			this.newProjectButton = new System.Windows.Forms.Button();
			this.exportProjectButton = new System.Windows.Forms.Button();
			this.useFrameButton = new System.Windows.Forms.Button();
			this.openProjectButton = new System.Windows.Forms.Button();
			this.saveProjectButton = new System.Windows.Forms.Button();
			this.saveFrameButton = new System.Windows.Forms.Button();
			this.closeProjectButton = new System.Windows.Forms.Button();
			this.lightsListView = new System.Windows.Forms.ListView();
			this.lightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.stepBasePanel = new System.Windows.Forms.Panel();
			this.stepPanel = new System.Windows.Forms.Panel();
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
			this.chooseStepButton = new System.Windows.Forms.Button();
			this.multiCopyButton = new System.Windows.Forms.Button();
			this.insertBeforeButton = new System.Windows.Forms.Button();
			this.copyStepButton = new System.Windows.Forms.Button();
			this.addStepButton = new System.Windows.Forms.Button();
			this.pasteStepButton = new System.Windows.Forms.Button();
			this.topPanel = new System.Windows.Forms.Panel();
			this.lightLargeImageList = new System.Windows.Forms.ImageList(this.components);
			this.mainMenuStrip.SuspendLayout();
			this.playPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.currentLightPictureBox)).BeginInit();
			this.lightInfoGroupBox.SuspendLayout();
			this.labelPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.unifyValueNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.unifySteptimeNumericUpDown)).BeginInit();
			this.tdFlowLayoutPanel.SuspendLayout();
			this.tdPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tdStNumericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tdValueNumericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tdTrackBar1)).BeginInit();
			this.buttonPanel.SuspendLayout();
			this.unifyPanel.SuspendLayout();
			this.playBasePanel.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.panel7.SuspendLayout();
			this.stepBasePanel.SuspendLayout();
			this.stepPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chooseStepNumericUpDown)).BeginInit();
			this.topPanel.SuspendLayout();
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
            this.传视界工具ToolStripMenuItem,
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
			// 传视界工具ToolStripMenuItem
			// 
			this.传视界工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator1,
            this.QDControllerToolStripMenuItem,
            this.CenterControllerToolStripMenuItem,
            this.KeyPressToolStripMenuItem});
			this.传视界工具ToolStripMenuItem.Name = "传视界工具ToolStripMenuItem";
			this.传视界工具ToolStripMenuItem.Size = new System.Drawing.Size(68, 26);
			this.传视界工具ToolStripMenuItem.Text = "其他工具";
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
			this.playPanel.Controls.Add(this.refreshDeviceButton);
			this.playPanel.Controls.Add(this.deviceComboBox);
			this.playPanel.Controls.Add(this.connectButton);
			this.playPanel.Controls.Add(this.soundButton);
			this.playPanel.Controls.Add(this.endviewButton);
			this.playPanel.Controls.Add(this.previewButton);
			this.playPanel.Location = new System.Drawing.Point(366, 3);
			this.playPanel.Name = "playPanel";
			this.playPanel.Size = new System.Drawing.Size(533, 68);
			this.playPanel.TabIndex = 30;
			// 
			// changeConnectMethodButton
			// 
			this.changeConnectMethodButton.Location = new System.Drawing.Point(17, 6);
			this.changeConnectMethodButton.Margin = new System.Windows.Forms.Padding(2);
			this.changeConnectMethodButton.Name = "changeConnectMethodButton";
			this.changeConnectMethodButton.Size = new System.Drawing.Size(74, 54);
			this.changeConnectMethodButton.TabIndex = 20;
			this.changeConnectMethodButton.Text = "以网络连接";
			this.changeConnectMethodButton.UseVisualStyleBackColor = true;
			this.changeConnectMethodButton.Click += new System.EventHandler(this.changeConnectMethodButton_Click);
			// 
			// refreshDeviceButton
			// 
			this.refreshDeviceButton.Location = new System.Drawing.Point(105, 34);
			this.refreshDeviceButton.Margin = new System.Windows.Forms.Padding(2);
			this.refreshDeviceButton.Name = "refreshDeviceButton";
			this.refreshDeviceButton.Size = new System.Drawing.Size(80, 26);
			this.refreshDeviceButton.TabIndex = 20;
			this.refreshDeviceButton.Text = "刷新列表";
			this.refreshDeviceButton.UseVisualStyleBackColor = true;
			this.refreshDeviceButton.Click += new System.EventHandler(this.refreshDeviceButton_Click);
			// 
			// deviceComboBox
			// 
			this.deviceComboBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.deviceComboBox.FormattingEnabled = true;
			this.deviceComboBox.Location = new System.Drawing.Point(98, 8);
			this.deviceComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.deviceComboBox.Name = "deviceComboBox";
			this.deviceComboBox.Size = new System.Drawing.Size(196, 20);
			this.deviceComboBox.TabIndex = 19;
			// 
			// connectButton
			// 
			this.connectButton.BackColor = System.Drawing.Color.Transparent;
			this.connectButton.Enabled = false;
			this.connectButton.Location = new System.Drawing.Point(202, 34);
			this.connectButton.Margin = new System.Windows.Forms.Padding(2);
			this.connectButton.Name = "connectButton";
			this.connectButton.Size = new System.Drawing.Size(80, 26);
			this.connectButton.TabIndex = 23;
			this.connectButton.Text = "连接设备";
			this.connectButton.UseVisualStyleBackColor = false;
			this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// soundButton
			// 
			this.soundButton.Location = new System.Drawing.Point(376, 8);
			this.soundButton.Margin = new System.Windows.Forms.Padding(2);
			this.soundButton.Name = "soundButton";
			this.soundButton.Size = new System.Drawing.Size(69, 54);
			this.soundButton.TabIndex = 25;
			this.soundButton.Text = "触发音频";
			this.soundButton.UseVisualStyleBackColor = true;
			// 
			// endviewButton
			// 
			this.endviewButton.Location = new System.Drawing.Point(451, 8);
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
			this.previewButton.Location = new System.Drawing.Point(301, 8);
			this.previewButton.Margin = new System.Windows.Forms.Padding(2);
			this.previewButton.Name = "previewButton";
			this.previewButton.Size = new System.Drawing.Size(69, 54);
			this.previewButton.TabIndex = 24;
			this.previewButton.Text = "预览效果";
			this.previewButton.UseVisualStyleBackColor = true;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "上一步.png");
			this.imageList1.Images.SetKeyName(1, "保存素材2.png");
			this.imageList1.Images.SetKeyName(2, "多灯模式.png");
			this.imageList1.Images.SetKeyName(3, "复制步.png");
			this.imageList1.Images.SetKeyName(4, "复制灯1.png");
			this.imageList1.Images.SetKeyName(5, "后插入步1.png");
			this.imageList1.Images.SetKeyName(6, "前插入步1.png");
			this.imageList1.Images.SetKeyName(7, "删除步.png");
			this.imageList1.Images.SetKeyName(8, "使用素材.png");
			this.imageList1.Images.SetKeyName(9, "跳转.png");
			this.imageList1.Images.SetKeyName(10, "下一步.png");
			this.imageList1.Images.SetKeyName(11, "粘贴步.png");
			this.imageList1.Images.SetKeyName(12, "粘贴灯1.png");
			this.imageList1.Images.SetKeyName(13, "追加步.png");
			// 
			// lightsAddrLabel
			// 
			this.lightsAddrLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightsAddrLabel.ForeColor = System.Drawing.Color.Black;
			this.lightsAddrLabel.Location = new System.Drawing.Point(6, 209);
			this.lightsAddrLabel.Name = "lightsAddrLabel";
			this.lightsAddrLabel.Size = new System.Drawing.Size(165, 83);
			this.lightsAddrLabel.TabIndex = 5;
			this.lightsAddrLabel.Text = " ";
			// 
			// lightTypeLabel
			// 
			this.lightTypeLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightTypeLabel.ForeColor = System.Drawing.Color.Black;
			this.lightTypeLabel.Location = new System.Drawing.Point(6, 175);
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
			this.lightNameLabel.Location = new System.Drawing.Point(6, 141);
			this.lightNameLabel.Name = "lightNameLabel";
			this.lightNameLabel.Size = new System.Drawing.Size(166, 18);
			this.lightNameLabel.TabIndex = 8;
			this.lightNameLabel.Text = " ";
			// 
			// currentLightPictureBox
			// 
			this.currentLightPictureBox.InitialImage = null;
			this.currentLightPictureBox.Location = new System.Drawing.Point(34, 16);
			this.currentLightPictureBox.Name = "currentLightPictureBox";
			this.currentLightPictureBox.Size = new System.Drawing.Size(110, 115);
			this.currentLightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.currentLightPictureBox.TabIndex = 6;
			this.currentLightPictureBox.TabStop = false;
			// 
			// lightInfoGroupBox
			// 
			this.lightInfoGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.lightInfoGroupBox.BackColor = System.Drawing.Color.Transparent;
			this.lightInfoGroupBox.Controls.Add(this.currentLightPictureBox);
			this.lightInfoGroupBox.Controls.Add(this.lightsAddrLabel);
			this.lightInfoGroupBox.Controls.Add(this.lightNameLabel);
			this.lightInfoGroupBox.Controls.Add(this.lightTypeLabel);
			this.lightInfoGroupBox.Dock = System.Windows.Forms.DockStyle.Right;
			this.lightInfoGroupBox.Location = new System.Drawing.Point(1089, 0);
			this.lightInfoGroupBox.Name = "lightInfoGroupBox";
			this.lightInfoGroupBox.Size = new System.Drawing.Size(175, 296);
			this.lightInfoGroupBox.TabIndex = 51;
			this.lightInfoGroupBox.TabStop = false;
			this.lightInfoGroupBox.Text = "当前灯具";
			// 
			// labelPanel
			// 
			this.labelPanel.BackColor = System.Drawing.Color.Transparent;
			this.labelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelPanel.Controls.Add(this.thirdLabel1);
			this.labelPanel.Controls.Add(this.secondLabel1);
			this.labelPanel.Controls.Add(this.firstLabel1);
			this.labelPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelPanel.Location = new System.Drawing.Point(0, 0);
			this.labelPanel.Name = "labelPanel";
			this.labelPanel.Size = new System.Drawing.Size(95, 335);
			this.labelPanel.TabIndex = 28;
			// 
			// thirdLabel1
			// 
			this.thirdLabel1.AutoSize = true;
			this.thirdLabel1.Location = new System.Drawing.Point(24, 278);
			this.thirdLabel1.Name = "thirdLabel1";
			this.thirdLabel1.Size = new System.Drawing.Size(59, 12);
			this.thirdLabel1.TabIndex = 0;
			this.thirdLabel1.Text = "步时间(s)";
			// 
			// secondLabel1
			// 
			this.secondLabel1.AutoSize = true;
			this.secondLabel1.Location = new System.Drawing.Point(33, 253);
			this.secondLabel1.Name = "secondLabel1";
			this.secondLabel1.Size = new System.Drawing.Size(41, 12);
			this.secondLabel1.TabIndex = 0;
			this.secondLabel1.Text = "跳渐变";
			// 
			// firstLabel1
			// 
			this.firstLabel1.AutoSize = true;
			this.firstLabel1.Location = new System.Drawing.Point(33, 227);
			this.firstLabel1.Name = "firstLabel1";
			this.firstLabel1.Size = new System.Drawing.Size(41, 12);
			this.firstLabel1.TabIndex = 0;
			this.firstLabel1.Text = "通道值";
			// 
			// unifyChangeModeComboBox
			// 
			this.unifyChangeModeComboBox.FormattingEnabled = true;
			this.unifyChangeModeComboBox.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.unifyChangeModeComboBox.Location = new System.Drawing.Point(10, 255);
			this.unifyChangeModeComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.unifyChangeModeComboBox.Name = "unifyChangeModeComboBox";
			this.unifyChangeModeComboBox.Size = new System.Drawing.Size(58, 20);
			this.unifyChangeModeComboBox.TabIndex = 62;
			// 
			// unifyValueNumericUpDown
			// 
			this.unifyValueNumericUpDown.Location = new System.Drawing.Point(12, 221);
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
			// unifySteptimeNumericUpDown
			// 
			this.unifySteptimeNumericUpDown.Location = new System.Drawing.Point(12, 285);
			this.unifySteptimeNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.unifySteptimeNumericUpDown.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.unifySteptimeNumericUpDown.Name = "unifySteptimeNumericUpDown";
			this.unifySteptimeNumericUpDown.Size = new System.Drawing.Size(57, 21);
			this.unifySteptimeNumericUpDown.TabIndex = 61;
			this.unifySteptimeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// unifyChangemodeButton
			// 
			this.unifyChangemodeButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.unifyChangemodeButton.Location = new System.Drawing.Point(83, 253);
			this.unifyChangemodeButton.Margin = new System.Windows.Forms.Padding(2);
			this.unifyChangemodeButton.Name = "unifyChangemodeButton";
			this.unifyChangemodeButton.Size = new System.Drawing.Size(83, 23);
			this.unifyChangemodeButton.TabIndex = 57;
			this.unifyChangemodeButton.Text = "统一跳渐变";
			this.unifyChangemodeButton.UseVisualStyleBackColor = true;
			// 
			// unifyValueButton
			// 
			this.unifyValueButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.unifyValueButton.Location = new System.Drawing.Point(83, 219);
			this.unifyValueButton.Margin = new System.Windows.Forms.Padding(2);
			this.unifyValueButton.Name = "unifyValueButton";
			this.unifyValueButton.Size = new System.Drawing.Size(84, 23);
			this.unifyValueButton.TabIndex = 58;
			this.unifyValueButton.Text = "统一通道值";
			this.unifyValueButton.UseVisualStyleBackColor = true;
			// 
			// unifySteptimeButton
			// 
			this.unifySteptimeButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.unifySteptimeButton.Location = new System.Drawing.Point(83, 283);
			this.unifySteptimeButton.Margin = new System.Windows.Forms.Padding(2);
			this.unifySteptimeButton.Name = "unifySteptimeButton";
			this.unifySteptimeButton.Size = new System.Drawing.Size(83, 23);
			this.unifySteptimeButton.TabIndex = 59;
			this.unifySteptimeButton.Text = "统一步时间";
			this.unifySteptimeButton.UseVisualStyleBackColor = true;
			// 
			// initButton
			// 
			this.initButton.Location = new System.Drawing.Point(12, 76);
			this.initButton.Margin = new System.Windows.Forms.Padding(2);
			this.initButton.Name = "initButton";
			this.initButton.Size = new System.Drawing.Size(75, 23);
			this.initButton.TabIndex = 55;
			this.initButton.Text = "设为初值";
			this.initButton.UseVisualStyleBackColor = true;
			// 
			// zeroButton
			// 
			this.zeroButton.Location = new System.Drawing.Point(12, 49);
			this.zeroButton.Margin = new System.Windows.Forms.Padding(2);
			this.zeroButton.Name = "zeroButton";
			this.zeroButton.Size = new System.Drawing.Size(75, 23);
			this.zeroButton.TabIndex = 56;
			this.zeroButton.Text = "全部归零";
			this.zeroButton.UseVisualStyleBackColor = true;
			this.zeroButton.Click += new System.EventHandler(this.zeroButton_Click);
			// 
			// tdFlowLayoutPanel
			// 
			this.tdFlowLayoutPanel.AutoScroll = true;
			this.tdFlowLayoutPanel.BackColor = System.Drawing.Color.MintCream;
			this.tdFlowLayoutPanel.Controls.Add(this.tdPanel1);
			this.tdFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tdFlowLayoutPanel.Location = new System.Drawing.Point(95, 0);
			this.tdFlowLayoutPanel.Name = "tdFlowLayoutPanel";
			this.tdFlowLayoutPanel.Size = new System.Drawing.Size(994, 335);
			this.tdFlowLayoutPanel.TabIndex = 54;
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
			this.tdCmComboBox1.Location = new System.Drawing.Point(12, 247);
			this.tdCmComboBox1.Name = "tdCmComboBox1";
			this.tdCmComboBox1.Size = new System.Drawing.Size(60, 20);
			this.tdCmComboBox1.TabIndex = 2;
			// 
			// tdStNumericUpDown1
			// 
			this.tdStNumericUpDown1.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tdStNumericUpDown1.Location = new System.Drawing.Point(12, 271);
			this.tdStNumericUpDown1.Name = "tdStNumericUpDown1";
			this.tdStNumericUpDown1.Size = new System.Drawing.Size(60, 20);
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
			this.tdTrackBar1.BackColor = System.Drawing.Color.MintCream;
			this.tdTrackBar1.Location = new System.Drawing.Point(33, 33);
			this.tdTrackBar1.Maximum = 255;
			this.tdTrackBar1.Name = "tdTrackBar1";
			this.tdTrackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.tdTrackBar1.Size = new System.Drawing.Size(35, 184);
			this.tdTrackBar1.TabIndex = 0;
			this.tdTrackBar1.TickFrequency = 0;
			this.tdTrackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
			// 
			// buttonPanel
			// 
			this.buttonPanel.Controls.Add(this.tdFlowLayoutPanel);
			this.buttonPanel.Controls.Add(this.unifyPanel);
			this.buttonPanel.Controls.Add(this.labelPanel);
			this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.buttonPanel.Location = new System.Drawing.Point(0, 413);
			this.buttonPanel.Name = "buttonPanel";
			this.buttonPanel.Size = new System.Drawing.Size(1264, 335);
			this.buttonPanel.TabIndex = 63;
			// 
			// unifyPanel
			// 
			this.unifyPanel.BackColor = System.Drawing.Color.White;
			this.unifyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.unifyPanel.Controls.Add(this.zeroButton);
			this.unifyPanel.Controls.Add(this.multiButton);
			this.unifyPanel.Controls.Add(this.initButton);
			this.unifyPanel.Controls.Add(this.unifyChangeModeComboBox);
			this.unifyPanel.Controls.Add(this.unifyValueNumericUpDown);
			this.unifyPanel.Controls.Add(this.unifySteptimeButton);
			this.unifyPanel.Controls.Add(this.unifySteptimeNumericUpDown);
			this.unifyPanel.Controls.Add(this.unifyValueButton);
			this.unifyPanel.Controls.Add(this.unifyChangemodeButton);
			this.unifyPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.unifyPanel.Location = new System.Drawing.Point(1089, 0);
			this.unifyPanel.Name = "unifyPanel";
			this.unifyPanel.Size = new System.Drawing.Size(175, 335);
			this.unifyPanel.TabIndex = 64;
			// 
			// multiButton
			// 
			this.multiButton.Location = new System.Drawing.Point(91, 49);
			this.multiButton.Margin = new System.Windows.Forms.Padding(2);
			this.multiButton.Name = "multiButton";
			this.multiButton.Size = new System.Drawing.Size(75, 50);
			this.multiButton.TabIndex = 55;
			this.multiButton.Text = "多步调节";
			this.multiButton.UseVisualStyleBackColor = true;
			// 
			// playBasePanel
			// 
			this.playBasePanel.BackColor = System.Drawing.Color.White;
			this.playBasePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.playBasePanel.Controls.Add(this.statusStrip1);
			this.playBasePanel.Controls.Add(this.playPanel);
			this.playBasePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.playBasePanel.Location = new System.Drawing.Point(0, 748);
			this.playBasePanel.Name = "playBasePanel";
			this.playBasePanel.Size = new System.Drawing.Size(1264, 96);
			this.playBasePanel.TabIndex = 67;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 72);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1262, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 31;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.AutoSize = false;
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(1247, 17);
			this.myStatusLabel.Spring = true;
			// 
			// skinComboBox
			// 
			this.skinComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.skinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.skinComboBox.FormattingEnabled = true;
			this.skinComboBox.Location = new System.Drawing.Point(1145, 6);
			this.skinComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.skinComboBox.Name = "skinComboBox";
			this.skinComboBox.Size = new System.Drawing.Size(118, 20);
			this.skinComboBox.TabIndex = 52;
			this.skinComboBox.SelectedIndexChanged += new System.EventHandler(this.skinComboBox_SelectedIndexChanged);
			// 
			// panel7
			// 
			this.panel7.Controls.Add(this.newProjectButton);
			this.panel7.Controls.Add(this.exportProjectButton);
			this.panel7.Controls.Add(this.useFrameButton);
			this.panel7.Controls.Add(this.openProjectButton);
			this.panel7.Controls.Add(this.saveProjectButton);
			this.panel7.Controls.Add(this.saveFrameButton);
			this.panel7.Controls.Add(this.closeProjectButton);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel7.Location = new System.Drawing.Point(0, 0);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(95, 383);
			this.panel7.TabIndex = 69;
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
			this.exportProjectButton.Enabled = false;
			this.exportProjectButton.Location = new System.Drawing.Point(11, 281);
			this.exportProjectButton.Margin = new System.Windows.Forms.Padding(2);
			this.exportProjectButton.Name = "exportProjectButton";
			this.exportProjectButton.Size = new System.Drawing.Size(74, 40);
			this.exportProjectButton.TabIndex = 29;
			this.exportProjectButton.Text = "导出工程";
			this.exportProjectButton.UseVisualStyleBackColor = true;
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
			// 
			// lightsListView
			// 
			this.lightsListView.BackColor = System.Drawing.Color.MintCream;
			this.lightsListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lightsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lightType});
			this.lightsListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lightsListView.HideSelection = false;
			this.lightsListView.Location = new System.Drawing.Point(95, 0);
			this.lightsListView.Margin = new System.Windows.Forms.Padding(2);
			this.lightsListView.MultiSelect = false;
			this.lightsListView.Name = "lightsListView";
			this.lightsListView.Size = new System.Drawing.Size(1169, 383);
			this.lightsListView.TabIndex = 50;
			this.lightsListView.UseCompatibleStateImageBehavior = false;
			this.lightsListView.SelectedIndexChanged += new System.EventHandler(this.lightsListView_SelectedIndexChanged);
			// 
			// lightType
			// 
			this.lightType.Text = "LightType";
			// 
			// stepBasePanel
			// 
			this.stepBasePanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.stepBasePanel.Controls.Add(this.stepPanel);
			this.stepBasePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.stepBasePanel.Location = new System.Drawing.Point(95, 296);
			this.stepBasePanel.Name = "stepBasePanel";
			this.stepBasePanel.Size = new System.Drawing.Size(1169, 87);
			this.stepBasePanel.TabIndex = 68;
			// 
			// stepPanel
			// 
			this.stepPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.stepPanel.BackColor = System.Drawing.Color.Transparent;
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
			this.stepPanel.Controls.Add(this.chooseStepButton);
			this.stepPanel.Controls.Add(this.multiCopyButton);
			this.stepPanel.Controls.Add(this.insertBeforeButton);
			this.stepPanel.Controls.Add(this.copyStepButton);
			this.stepPanel.Controls.Add(this.addStepButton);
			this.stepPanel.Controls.Add(this.pasteStepButton);
			this.stepPanel.Location = new System.Drawing.Point(98, 1);
			this.stepPanel.Name = "stepPanel";
			this.stepPanel.Size = new System.Drawing.Size(938, 83);
			this.stepPanel.TabIndex = 65;
			// 
			// saveMaterialButton
			// 
			this.saveMaterialButton.Location = new System.Drawing.Point(829, 16);
			this.saveMaterialButton.Name = "saveMaterialButton";
			this.saveMaterialButton.Size = new System.Drawing.Size(75, 23);
			this.saveMaterialButton.TabIndex = 49;
			this.saveMaterialButton.Text = "保存素材";
			this.saveMaterialButton.UseVisualStyleBackColor = true;
			// 
			// modeComboBox
			// 
			this.modeComboBox.FormattingEnabled = true;
			this.modeComboBox.Location = new System.Drawing.Point(68, 49);
			this.modeComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.modeComboBox.Name = "modeComboBox";
			this.modeComboBox.Size = new System.Drawing.Size(84, 20);
			this.modeComboBox.TabIndex = 18;
			// 
			// frameComboBox
			// 
			this.frameComboBox.FormattingEnabled = true;
			this.frameComboBox.Location = new System.Drawing.Point(68, 17);
			this.frameComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.frameComboBox.Name = "frameComboBox";
			this.frameComboBox.Size = new System.Drawing.Size(84, 20);
			this.frameComboBox.TabIndex = 17;
			// 
			// stepLabel
			// 
			this.stepLabel.AutoSize = true;
			this.stepLabel.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.stepLabel.ForeColor = System.Drawing.Color.White;
			this.stepLabel.Location = new System.Drawing.Point(398, 21);
			this.stepLabel.Name = "stepLabel";
			this.stepLabel.Size = new System.Drawing.Size(31, 14);
			this.stepLabel.TabIndex = 53;
			this.stepLabel.Text = "0/0";
			// 
			// frameChooseLabel
			// 
			this.frameChooseLabel.AutoSize = true;
			this.frameChooseLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.frameChooseLabel.Location = new System.Drawing.Point(23, 21);
			this.frameChooseLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.frameChooseLabel.Name = "frameChooseLabel";
			this.frameChooseLabel.Size = new System.Drawing.Size(41, 12);
			this.frameChooseLabel.TabIndex = 20;
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
			this.modeChooseLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.modeChooseLabel.Location = new System.Drawing.Point(23, 53);
			this.modeChooseLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.modeChooseLabel.Name = "modeChooseLabel";
			this.modeChooseLabel.Size = new System.Drawing.Size(41, 12);
			this.modeChooseLabel.TabIndex = 19;
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
			this.syncButton.Text = "多灯模式";
			this.syncButton.UseVisualStyleBackColor = false;
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
			// 
			// backStepButton
			// 
			this.backStepButton.Location = new System.Drawing.Point(290, 16);
			this.backStepButton.Name = "backStepButton";
			this.backStepButton.Size = new System.Drawing.Size(75, 23);
			this.backStepButton.TabIndex = 49;
			this.backStepButton.Text = "上一步";
			this.backStepButton.UseVisualStyleBackColor = true;
			// 
			// multiPasteButton
			// 
			this.multiPasteButton.Location = new System.Drawing.Point(743, 48);
			this.multiPasteButton.Name = "multiPasteButton";
			this.multiPasteButton.Size = new System.Drawing.Size(75, 23);
			this.multiPasteButton.TabIndex = 49;
			this.multiPasteButton.Text = "粘贴多步";
			this.multiPasteButton.UseVisualStyleBackColor = true;
			// 
			// insertAfterButton
			// 
			this.insertAfterButton.Location = new System.Drawing.Point(375, 48);
			this.insertAfterButton.Name = "insertAfterButton";
			this.insertAfterButton.Size = new System.Drawing.Size(75, 23);
			this.insertAfterButton.TabIndex = 49;
			this.insertAfterButton.Text = "后插步";
			this.insertAfterButton.UseVisualStyleBackColor = true;
			// 
			// nextStepButton
			// 
			this.nextStepButton.Location = new System.Drawing.Point(460, 16);
			this.nextStepButton.Name = "nextStepButton";
			this.nextStepButton.Size = new System.Drawing.Size(75, 23);
			this.nextStepButton.TabIndex = 49;
			this.nextStepButton.Text = "下一步";
			this.nextStepButton.UseVisualStyleBackColor = true;
			// 
			// useMaterialButton
			// 
			this.useMaterialButton.Location = new System.Drawing.Point(829, 48);
			this.useMaterialButton.Name = "useMaterialButton";
			this.useMaterialButton.Size = new System.Drawing.Size(75, 23);
			this.useMaterialButton.TabIndex = 49;
			this.useMaterialButton.Text = "使用素材";
			this.useMaterialButton.UseVisualStyleBackColor = true;
			// 
			// deleteStepButton
			// 
			this.deleteStepButton.Location = new System.Drawing.Point(545, 48);
			this.deleteStepButton.Name = "deleteStepButton";
			this.deleteStepButton.Size = new System.Drawing.Size(75, 23);
			this.deleteStepButton.TabIndex = 49;
			this.deleteStepButton.Text = "删除步";
			this.deleteStepButton.UseVisualStyleBackColor = true;
			// 
			// chooseStepButton
			// 
			this.chooseStepButton.FlatAppearance.BorderSize = 0;
			this.chooseStepButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.chooseStepButton.ImageKey = "跳转.png";
			this.chooseStepButton.ImageList = this.imageList1;
			this.chooseStepButton.Location = new System.Drawing.Point(602, 18);
			this.chooseStepButton.Name = "chooseStepButton";
			this.chooseStepButton.Size = new System.Drawing.Size(24, 18);
			this.chooseStepButton.TabIndex = 49;
			this.chooseStepButton.UseVisualStyleBackColor = true;
			// 
			// multiCopyButton
			// 
			this.multiCopyButton.Location = new System.Drawing.Point(743, 16);
			this.multiCopyButton.Name = "multiCopyButton";
			this.multiCopyButton.Size = new System.Drawing.Size(75, 23);
			this.multiCopyButton.TabIndex = 49;
			this.multiCopyButton.Text = "复制多步";
			this.multiCopyButton.UseVisualStyleBackColor = true;
			// 
			// insertBeforeButton
			// 
			this.insertBeforeButton.Location = new System.Drawing.Point(290, 48);
			this.insertBeforeButton.Name = "insertBeforeButton";
			this.insertBeforeButton.Size = new System.Drawing.Size(75, 23);
			this.insertBeforeButton.TabIndex = 49;
			this.insertBeforeButton.Text = "前插步";
			this.insertBeforeButton.UseVisualStyleBackColor = true;
			// 
			// copyStepButton
			// 
			this.copyStepButton.Location = new System.Drawing.Point(659, 16);
			this.copyStepButton.Name = "copyStepButton";
			this.copyStepButton.Size = new System.Drawing.Size(75, 23);
			this.copyStepButton.TabIndex = 49;
			this.copyStepButton.Text = "复制步";
			this.copyStepButton.UseVisualStyleBackColor = true;
			// 
			// addStepButton
			// 
			this.addStepButton.Location = new System.Drawing.Point(460, 48);
			this.addStepButton.Name = "addStepButton";
			this.addStepButton.Size = new System.Drawing.Size(75, 23);
			this.addStepButton.TabIndex = 49;
			this.addStepButton.Text = "追加步";
			this.addStepButton.UseVisualStyleBackColor = true;
			// 
			// pasteStepButton
			// 
			this.pasteStepButton.Location = new System.Drawing.Point(659, 48);
			this.pasteStepButton.Name = "pasteStepButton";
			this.pasteStepButton.Size = new System.Drawing.Size(75, 23);
			this.pasteStepButton.TabIndex = 49;
			this.pasteStepButton.Text = "粘贴步";
			this.pasteStepButton.UseVisualStyleBackColor = true;
			// 
			// topPanel
			// 
			this.topPanel.Controls.Add(this.lightInfoGroupBox);
			this.topPanel.Controls.Add(this.stepBasePanel);
			this.topPanel.Controls.Add(this.lightsListView);
			this.topPanel.Controls.Add(this.panel7);
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.topPanel.Location = new System.Drawing.Point(0, 30);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(1264, 383);
			this.topPanel.TabIndex = 70;
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
			this.Controls.Add(this.buttonPanel);
			this.Controls.Add(this.mainMenuStrip);
			this.Controls.Add(this.playBasePanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "NewMainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.NewMainForm_Load);
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.playPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.currentLightPictureBox)).EndInit();
			this.lightInfoGroupBox.ResumeLayout(false);
			this.labelPanel.ResumeLayout(false);
			this.labelPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.unifyValueNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.unifySteptimeNumericUpDown)).EndInit();
			this.tdFlowLayoutPanel.ResumeLayout(false);
			this.tdPanel1.ResumeLayout(false);
			this.tdPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tdStNumericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tdValueNumericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tdTrackBar1)).EndInit();
			this.buttonPanel.ResumeLayout(false);
			this.unifyPanel.ResumeLayout(false);
			this.playBasePanel.ResumeLayout(false);
			this.playBasePanel.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.panel7.ResumeLayout(false);
			this.stepBasePanel.ResumeLayout(false);
			this.stepPanel.ResumeLayout(false);
			this.stepPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.chooseStepNumericUpDown)).EndInit();
			this.topPanel.ResumeLayout(false);
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
		private System.Windows.Forms.ToolStripMenuItem 传视界工具ToolStripMenuItem;
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
		private System.Windows.Forms.Button refreshDeviceButton;
		private System.Windows.Forms.ComboBox deviceComboBox;
		private System.Windows.Forms.Button connectButton;
		private System.Windows.Forms.Button previewButton;
		private System.Windows.Forms.Button soundButton;
		private System.Windows.Forms.Button changeConnectMethodButton;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label lightsAddrLabel;
		private System.Windows.Forms.Label lightTypeLabel;
		private System.Windows.Forms.Label lightNameLabel;
		private System.Windows.Forms.PictureBox currentLightPictureBox;
		private System.Windows.Forms.GroupBox lightInfoGroupBox;
		private System.Windows.Forms.Panel labelPanel;
		private System.Windows.Forms.Label thirdLabel1;
		private System.Windows.Forms.Label secondLabel1;
		private System.Windows.Forms.Label firstLabel1;
		private System.Windows.Forms.ComboBox unifyChangeModeComboBox;
		private System.Windows.Forms.NumericUpDown unifyValueNumericUpDown;
		private System.Windows.Forms.NumericUpDown unifySteptimeNumericUpDown;
		private System.Windows.Forms.Button unifyChangemodeButton;
		private System.Windows.Forms.Button unifyValueButton;
		private System.Windows.Forms.Button unifySteptimeButton;
		private System.Windows.Forms.Button initButton;
		private System.Windows.Forms.Button zeroButton;
		private System.Windows.Forms.FlowLayoutPanel tdFlowLayoutPanel;
		private System.Windows.Forms.Panel buttonPanel;
		private System.Windows.Forms.Panel unifyPanel;
		private System.Windows.Forms.Panel playBasePanel;
		private System.Windows.Forms.Button multiButton;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ComboBox skinComboBox;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Button newProjectButton;
		private System.Windows.Forms.Button exportProjectButton;
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
		private System.Windows.Forms.Button chooseStepButton;
		private System.Windows.Forms.Button multiCopyButton;
		private System.Windows.Forms.Button insertBeforeButton;
		private System.Windows.Forms.Button copyStepButton;
		private System.Windows.Forms.Button addStepButton;
		private System.Windows.Forms.Button pasteStepButton;
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ImageList lightLargeImageList;

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
	}
}