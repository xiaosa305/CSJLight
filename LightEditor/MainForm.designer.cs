using LighEditor;
using System.Collections.Generic;
using System.Windows.Forms;


namespace LightEditor
{
	partial class MainForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.comComboBox = new System.Windows.Forms.ComboBox();
			this.newLightButton = new System.Windows.Forms.Button();
			this.openLightButton = new System.Windows.Forms.Button();
			this.saveLightButton = new System.Windows.Forms.Button();
			this.exitButton = new System.Windows.Forms.Button();
			this.lightTestGroupBox = new System.Windows.Forms.GroupBox();
			this.realtimeCheckBox = new System.Windows.Forms.CheckBox();
			this.firstTDNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.endTestButton = new System.Windows.Forms.Button();
			this.testButton = new System.Windows.Forms.Button();
			this.setFirstTDButton = new System.Windows.Forms.Button();
			this.setInitButton = new System.Windows.Forms.Button();
			this.zeroButton = new System.Windows.Forms.Button();
			this.tongdaoEditButton = new System.Windows.Forms.Button();
			this.generateButton = new System.Windows.Forms.Button();
			this.openPictureBox = new System.Windows.Forms.PictureBox();
			this.picTextBox = new System.Windows.Forms.TextBox();
			this.typeTextBox = new System.Windows.Forms.TextBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.tongdaoCountLabel = new System.Windows.Forms.Label();
			this.picLabel = new System.Windows.Forms.Label();
			this.typeLabel = new System.Windows.Forms.Label();
			this.countComboBox = new System.Windows.Forms.ComboBox();
			this.nameLabel = new System.Windows.Forms.Label();
			this.tongdaoGroupBox2 = new System.Windows.Forms.GroupBox();
			this.numericUpDown32 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown31 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown30 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown29 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown28 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown27 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown26 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown25 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown24 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown23 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown22 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown21 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown20 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown19 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown18 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown17 = new System.Windows.Forms.NumericUpDown();
			this.label32 = new System.Windows.Forms.Label();
			this.vScrollBar17 = new System.Windows.Forms.VScrollBar();
			this.label31 = new System.Windows.Forms.Label();
			this.vScrollBar18 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar19 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar20 = new System.Windows.Forms.VScrollBar();
			this.label30 = new System.Windows.Forms.Label();
			this.vScrollBar21 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar22 = new System.Windows.Forms.VScrollBar();
			this.label29 = new System.Windows.Forms.Label();
			this.vScrollBar23 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar24 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar25 = new System.Windows.Forms.VScrollBar();
			this.label28 = new System.Windows.Forms.Label();
			this.vScrollBar26 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar27 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar28 = new System.Windows.Forms.VScrollBar();
			this.label27 = new System.Windows.Forms.Label();
			this.vScrollBar29 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar30 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar31 = new System.Windows.Forms.VScrollBar();
			this.label26 = new System.Windows.Forms.Label();
			this.vScrollBar32 = new System.Windows.Forms.VScrollBar();
			this.label17 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.tongdaoGroupBox1 = new System.Windows.Forms.GroupBox();
			this.numericUpDown16 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown15 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown14 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown13 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown12 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown11 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown10 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown9 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.vScrollBar16 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar12 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar8 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar4 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar15 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar11 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar14 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar10 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar7 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar13 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar6 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar9 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar3 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar5 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar2 = new System.Windows.Forms.VScrollBar();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.skinEngine2 = new Sunisoft.IrisSkin.SkinEngine();
			this.editGroupBox = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.setCurrentToInitButton = new System.Windows.Forms.Button();
			this.commonValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.commonValueButton = new System.Windows.Forms.Button();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.connectButton = new System.Windows.Forms.Button();
			this.connectPanel = new System.Windows.Forms.Panel();
			this.refreshButton = new System.Windows.Forms.Button();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.lightTestGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.firstTDNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.openPictureBox)).BeginInit();
			this.tongdaoGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown32)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown31)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown30)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown29)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown28)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown27)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown26)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown25)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown24)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown23)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown22)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown21)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown20)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown19)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown18)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown17)).BeginInit();
			this.tongdaoGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown16)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown15)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown14)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown13)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.editGroupBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.commonValueNumericUpDown)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.connectPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// comComboBox
			// 
			this.comComboBox.FormattingEnabled = true;
			this.comComboBox.Location = new System.Drawing.Point(24, 13);
			this.comComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.comComboBox.Name = "comComboBox";
			this.comComboBox.Size = new System.Drawing.Size(86, 20);
			this.comComboBox.TabIndex = 0;
			this.comComboBox.SelectedIndexChanged += new System.EventHandler(this.comComboBox_SelectedIndexChanged);
			// 
			// newLightButton
			// 
			this.newLightButton.Location = new System.Drawing.Point(18, 19);
			this.newLightButton.Margin = new System.Windows.Forms.Padding(2);
			this.newLightButton.Name = "newLightButton";
			this.newLightButton.Size = new System.Drawing.Size(99, 46);
			this.newLightButton.TabIndex = 2;
			this.newLightButton.Text = "新建灯具";
			this.newLightButton.UseVisualStyleBackColor = true;
			this.newLightButton.Click += new System.EventHandler(this.newLightButton_Click);
			// 
			// openLightButton
			// 
			this.openLightButton.Location = new System.Drawing.Point(140, 19);
			this.openLightButton.Margin = new System.Windows.Forms.Padding(2);
			this.openLightButton.Name = "openLightButton";
			this.openLightButton.Size = new System.Drawing.Size(99, 46);
			this.openLightButton.TabIndex = 3;
			this.openLightButton.Text = "打开灯具";
			this.openLightButton.UseVisualStyleBackColor = true;
			this.openLightButton.Click += new System.EventHandler(this.openLightButton_Click);
			// 
			// saveLightButton
			// 
			this.saveLightButton.Location = new System.Drawing.Point(264, 19);
			this.saveLightButton.Margin = new System.Windows.Forms.Padding(2);
			this.saveLightButton.Name = "saveLightButton";
			this.saveLightButton.Size = new System.Drawing.Size(99, 46);
			this.saveLightButton.TabIndex = 4;
			this.saveLightButton.Text = "保存灯具";
			this.saveLightButton.UseVisualStyleBackColor = true;
			this.saveLightButton.Click += new System.EventHandler(this.saveLightButton_Click);
			// 
			// exitButton
			// 
			this.exitButton.Location = new System.Drawing.Point(712, 18);
			this.exitButton.Margin = new System.Windows.Forms.Padding(2);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(99, 46);
			this.exitButton.TabIndex = 5;
			this.exitButton.Text = "退出";
			this.exitButton.UseVisualStyleBackColor = true;
			this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
			// 
			// lightTestGroupBox
			// 
			this.lightTestGroupBox.Controls.Add(this.realtimeCheckBox);
			this.lightTestGroupBox.Controls.Add(this.firstTDNumericUpDown);
			this.lightTestGroupBox.Controls.Add(this.endTestButton);
			this.lightTestGroupBox.Controls.Add(this.testButton);
			this.lightTestGroupBox.Controls.Add(this.setFirstTDButton);
			this.lightTestGroupBox.Cursor = System.Windows.Forms.Cursors.Default;
			this.lightTestGroupBox.Location = new System.Drawing.Point(555, 30);
			this.lightTestGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.lightTestGroupBox.Name = "lightTestGroupBox";
			this.lightTestGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.lightTestGroupBox.Size = new System.Drawing.Size(249, 127);
			this.lightTestGroupBox.TabIndex = 10;
			this.lightTestGroupBox.TabStop = false;
			this.lightTestGroupBox.Text = "灯具测试";
			this.lightTestGroupBox.Visible = false;
			// 
			// realtimeCheckBox
			// 
			this.realtimeCheckBox.AutoSize = true;
			this.realtimeCheckBox.Location = new System.Drawing.Point(18, 78);
			this.realtimeCheckBox.Margin = new System.Windows.Forms.Padding(2);
			this.realtimeCheckBox.Name = "realtimeCheckBox";
			this.realtimeCheckBox.Size = new System.Drawing.Size(72, 16);
			this.realtimeCheckBox.TabIndex = 2;
			this.realtimeCheckBox.Text = "实时调试";
			this.realtimeCheckBox.UseVisualStyleBackColor = true;
			this.realtimeCheckBox.CheckedChanged += new System.EventHandler(this.realtimeCheckBox_CheckedChanged);
			// 
			// firstTDNumericUpDown
			// 
			this.firstTDNumericUpDown.Location = new System.Drawing.Point(21, 39);
			this.firstTDNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.firstTDNumericUpDown.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
			this.firstTDNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.firstTDNumericUpDown.Name = "firstTDNumericUpDown";
			this.firstTDNumericUpDown.Size = new System.Drawing.Size(64, 21);
			this.firstTDNumericUpDown.TabIndex = 1;
			this.firstTDNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.firstTDNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// endTestButton
			// 
			this.endTestButton.Location = new System.Drawing.Point(167, 74);
			this.endTestButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.endTestButton.Name = "endTestButton";
			this.endTestButton.Size = new System.Drawing.Size(64, 23);
			this.endTestButton.TabIndex = 0;
			this.endTestButton.Text = "停止调试";
			this.endTestButton.UseVisualStyleBackColor = true;
			this.endTestButton.Click += new System.EventHandler(this.endTestButton_Click);
			// 
			// testButton
			// 
			this.testButton.Location = new System.Drawing.Point(99, 74);
			this.testButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.testButton.Name = "testButton";
			this.testButton.Size = new System.Drawing.Size(64, 23);
			this.testButton.TabIndex = 0;
			this.testButton.Text = "单灯单步";
			this.testButton.UseVisualStyleBackColor = true;
			this.testButton.Click += new System.EventHandler(this.testButton_Click);
			// 
			// setFirstTDButton
			// 
			this.setFirstTDButton.Location = new System.Drawing.Point(99, 38);
			this.setFirstTDButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.setFirstTDButton.Name = "setFirstTDButton";
			this.setFirstTDButton.Size = new System.Drawing.Size(132, 23);
			this.setFirstTDButton.TabIndex = 0;
			this.setFirstTDButton.Text = "设初始通道地址";
			this.setFirstTDButton.UseVisualStyleBackColor = true;
			this.setFirstTDButton.Click += new System.EventHandler(this.setFirstTDButton_Click);
			// 
			// setInitButton
			// 
			this.setInitButton.Location = new System.Drawing.Point(106, 58);
			this.setInitButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.setInitButton.Name = "setInitButton";
			this.setInitButton.Size = new System.Drawing.Size(73, 23);
			this.setInitButton.TabIndex = 0;
			this.setInitButton.Text = "全设初始值";
			this.setInitButton.UseVisualStyleBackColor = true;
			this.setInitButton.Click += new System.EventHandler(this.setInitButton_Click);
			// 
			// zeroButton
			// 
			this.zeroButton.Location = new System.Drawing.Point(20, 58);
			this.zeroButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.zeroButton.Name = "zeroButton";
			this.zeroButton.Size = new System.Drawing.Size(71, 23);
			this.zeroButton.TabIndex = 0;
			this.zeroButton.Text = "全部归零";
			this.zeroButton.UseVisualStyleBackColor = true;
			this.zeroButton.Click += new System.EventHandler(this.zeroButton_Click);
			// 
			// tongdaoEditButton
			// 
			this.tongdaoEditButton.Location = new System.Drawing.Point(250, 108);
			this.tongdaoEditButton.Margin = new System.Windows.Forms.Padding(2);
			this.tongdaoEditButton.Name = "tongdaoEditButton";
			this.tongdaoEditButton.Size = new System.Drawing.Size(61, 47);
			this.tongdaoEditButton.TabIndex = 3;
			this.tongdaoEditButton.Text = "通道编辑";
			this.tongdaoEditButton.UseVisualStyleBackColor = true;
			this.tongdaoEditButton.Visible = false;
			this.tongdaoEditButton.Click += new System.EventHandler(this.tongdaoEditButton_Click);
			// 
			// generateButton
			// 
			this.generateButton.Location = new System.Drawing.Point(169, 133);
			this.generateButton.Margin = new System.Windows.Forms.Padding(2);
			this.generateButton.Name = "generateButton";
			this.generateButton.Size = new System.Drawing.Size(60, 22);
			this.generateButton.TabIndex = 4;
			this.generateButton.Text = "生成";
			this.generateButton.UseVisualStyleBackColor = true;
			this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
			// 
			// openPictureBox
			// 
			this.openPictureBox.BackColor = System.Drawing.SystemColors.MenuBar;
			this.openPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.openPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("openPictureBox.Image")));
			this.openPictureBox.Location = new System.Drawing.Point(250, 30);
			this.openPictureBox.Margin = new System.Windows.Forms.Padding(2);
			this.openPictureBox.Name = "openPictureBox";
			this.openPictureBox.Size = new System.Drawing.Size(61, 62);
			this.openPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.openPictureBox.TabIndex = 2;
			this.openPictureBox.TabStop = false;
			this.openPictureBox.Click += new System.EventHandler(this.pictureBox_Click);
			// 
			// picTextBox
			// 
			this.picTextBox.Location = new System.Drawing.Point(100, 99);
			this.picTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.picTextBox.Name = "picTextBox";
			this.picTextBox.ReadOnly = true;
			this.picTextBox.Size = new System.Drawing.Size(129, 21);
			this.picTextBox.TabIndex = 2;
			// 
			// typeTextBox
			// 
			this.typeTextBox.Location = new System.Drawing.Point(100, 65);
			this.typeTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.typeTextBox.Name = "typeTextBox";
			this.typeTextBox.Size = new System.Drawing.Size(128, 21);
			this.typeTextBox.TabIndex = 1;
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(100, 30);
			this.nameTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(129, 21);
			this.nameTextBox.TabIndex = 0;
			// 
			// tongdaoCountLabel
			// 
			this.tongdaoCountLabel.AutoSize = true;
			this.tongdaoCountLabel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tongdaoCountLabel.Location = new System.Drawing.Point(29, 137);
			this.tongdaoCountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.tongdaoCountLabel.Name = "tongdaoCountLabel";
			this.tongdaoCountLabel.Size = new System.Drawing.Size(63, 14);
			this.tongdaoCountLabel.TabIndex = 3;
			this.tongdaoCountLabel.Text = "通道数量";
			// 
			// picLabel
			// 
			this.picLabel.AutoSize = true;
			this.picLabel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.picLabel.Location = new System.Drawing.Point(29, 102);
			this.picLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.picLabel.Name = "picLabel";
			this.picLabel.Size = new System.Drawing.Size(63, 14);
			this.picLabel.TabIndex = 2;
			this.picLabel.Text = "灯具图片";
			// 
			// typeLabel
			// 
			this.typeLabel.AutoSize = true;
			this.typeLabel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.typeLabel.Location = new System.Drawing.Point(29, 68);
			this.typeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.typeLabel.Name = "typeLabel";
			this.typeLabel.Size = new System.Drawing.Size(63, 14);
			this.typeLabel.TabIndex = 1;
			this.typeLabel.Text = "灯具型号";
			// 
			// countComboBox
			// 
			this.countComboBox.Font = new System.Drawing.Font("宋体", 10F);
			this.countComboBox.FormattingEnabled = true;
			this.countComboBox.Location = new System.Drawing.Point(100, 134);
			this.countComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.countComboBox.Name = "countComboBox";
			this.countComboBox.Size = new System.Drawing.Size(52, 21);
			this.countComboBox.TabIndex = 2;
			this.countComboBox.SelectedIndexChanged += new System.EventHandler(this.countComboBox_SelectedIndexChanged);
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.nameLabel.Location = new System.Drawing.Point(29, 34);
			this.nameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(63, 14);
			this.nameLabel.TabIndex = 4;
			this.nameLabel.Text = "灯具厂家";
			// 
			// tongdaoGroupBox2
			// 
			this.tongdaoGroupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown32);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown31);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown30);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown29);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown28);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown27);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown26);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown25);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown24);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown23);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown22);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown21);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown20);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown19);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown18);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown17);
			this.tongdaoGroupBox2.Controls.Add(this.label32);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar17);
			this.tongdaoGroupBox2.Controls.Add(this.label31);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar18);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar19);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar20);
			this.tongdaoGroupBox2.Controls.Add(this.label30);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar21);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar22);
			this.tongdaoGroupBox2.Controls.Add(this.label29);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar23);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar24);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar25);
			this.tongdaoGroupBox2.Controls.Add(this.label28);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar26);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar27);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar28);
			this.tongdaoGroupBox2.Controls.Add(this.label27);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar29);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar30);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar31);
			this.tongdaoGroupBox2.Controls.Add(this.label26);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar32);
			this.tongdaoGroupBox2.Controls.Add(this.label17);
			this.tongdaoGroupBox2.Controls.Add(this.label22);
			this.tongdaoGroupBox2.Controls.Add(this.label25);
			this.tongdaoGroupBox2.Controls.Add(this.label20);
			this.tongdaoGroupBox2.Controls.Add(this.label19);
			this.tongdaoGroupBox2.Controls.Add(this.label21);
			this.tongdaoGroupBox2.Controls.Add(this.label23);
			this.tongdaoGroupBox2.Controls.Add(this.label24);
			this.tongdaoGroupBox2.Controls.Add(this.label18);
			this.tongdaoGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tongdaoGroupBox2.Location = new System.Drawing.Point(2, 261);
			this.tongdaoGroupBox2.Margin = new System.Windows.Forms.Padding(2);
			this.tongdaoGroupBox2.Name = "tongdaoGroupBox2";
			this.tongdaoGroupBox2.Padding = new System.Windows.Forms.Padding(2);
			this.tongdaoGroupBox2.Size = new System.Drawing.Size(789, 255);
			this.tongdaoGroupBox2.TabIndex = 9;
			this.tongdaoGroupBox2.TabStop = false;
			this.tongdaoGroupBox2.Visible = false;
			// 
			// numericUpDown32
			// 
			this.numericUpDown32.Location = new System.Drawing.Point(737, 221);
			this.numericUpDown32.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown32.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown32.Name = "numericUpDown32";
			this.numericUpDown32.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown32.TabIndex = 11;
			this.numericUpDown32.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown31
			// 
			this.numericUpDown31.Location = new System.Drawing.Point(688, 221);
			this.numericUpDown31.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown31.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown31.Name = "numericUpDown31";
			this.numericUpDown31.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown31.TabIndex = 11;
			this.numericUpDown31.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown30
			// 
			this.numericUpDown30.Location = new System.Drawing.Point(640, 221);
			this.numericUpDown30.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown30.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown30.Name = "numericUpDown30";
			this.numericUpDown30.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown30.TabIndex = 11;
			this.numericUpDown30.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown29
			// 
			this.numericUpDown29.Location = new System.Drawing.Point(592, 221);
			this.numericUpDown29.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown29.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown29.Name = "numericUpDown29";
			this.numericUpDown29.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown29.TabIndex = 11;
			this.numericUpDown29.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown28
			// 
			this.numericUpDown28.Location = new System.Drawing.Point(545, 221);
			this.numericUpDown28.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown28.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown28.Name = "numericUpDown28";
			this.numericUpDown28.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown28.TabIndex = 11;
			this.numericUpDown28.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown27
			// 
			this.numericUpDown27.Location = new System.Drawing.Point(497, 221);
			this.numericUpDown27.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown27.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown27.Name = "numericUpDown27";
			this.numericUpDown27.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown27.TabIndex = 11;
			this.numericUpDown27.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown26
			// 
			this.numericUpDown26.Location = new System.Drawing.Point(448, 221);
			this.numericUpDown26.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown26.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown26.Name = "numericUpDown26";
			this.numericUpDown26.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown26.TabIndex = 11;
			this.numericUpDown26.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown25
			// 
			this.numericUpDown25.Location = new System.Drawing.Point(401, 221);
			this.numericUpDown25.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown25.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown25.Name = "numericUpDown25";
			this.numericUpDown25.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown25.TabIndex = 11;
			this.numericUpDown25.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown24
			// 
			this.numericUpDown24.Location = new System.Drawing.Point(352, 221);
			this.numericUpDown24.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown24.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown24.Name = "numericUpDown24";
			this.numericUpDown24.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown24.TabIndex = 11;
			this.numericUpDown24.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown23
			// 
			this.numericUpDown23.Location = new System.Drawing.Point(304, 221);
			this.numericUpDown23.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown23.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown23.Name = "numericUpDown23";
			this.numericUpDown23.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown23.TabIndex = 11;
			this.numericUpDown23.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown22
			// 
			this.numericUpDown22.Location = new System.Drawing.Point(256, 221);
			this.numericUpDown22.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown22.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown22.Name = "numericUpDown22";
			this.numericUpDown22.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown22.TabIndex = 11;
			this.numericUpDown22.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown21
			// 
			this.numericUpDown21.Location = new System.Drawing.Point(207, 221);
			this.numericUpDown21.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown21.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown21.Name = "numericUpDown21";
			this.numericUpDown21.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown21.TabIndex = 11;
			this.numericUpDown21.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown20
			// 
			this.numericUpDown20.Location = new System.Drawing.Point(161, 221);
			this.numericUpDown20.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown20.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown20.Name = "numericUpDown20";
			this.numericUpDown20.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown20.TabIndex = 11;
			this.numericUpDown20.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown19
			// 
			this.numericUpDown19.Location = new System.Drawing.Point(113, 221);
			this.numericUpDown19.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown19.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown19.Name = "numericUpDown19";
			this.numericUpDown19.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown19.TabIndex = 11;
			this.numericUpDown19.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown18
			// 
			this.numericUpDown18.Location = new System.Drawing.Point(65, 221);
			this.numericUpDown18.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown18.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown18.Name = "numericUpDown18";
			this.numericUpDown18.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown18.TabIndex = 11;
			this.numericUpDown18.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown17
			// 
			this.numericUpDown17.Location = new System.Drawing.Point(16, 221);
			this.numericUpDown17.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown17.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown17.Name = "numericUpDown17";
			this.numericUpDown17.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown17.TabIndex = 11;
			this.numericUpDown17.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// label32
			// 
			this.label32.Font = new System.Drawing.Font("宋体", 8F);
			this.label32.Location = new System.Drawing.Point(735, 28);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(14, 182);
			this.label32.TabIndex = 10;
			this.label32.Text = "总调光1";
			this.label32.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label32.Visible = false;
			this.label32.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// vScrollBar17
			// 
			this.vScrollBar17.Location = new System.Drawing.Point(32, 28);
			this.vScrollBar17.Maximum = 264;
			this.vScrollBar17.Name = "vScrollBar17";
			this.vScrollBar17.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar17.TabIndex = 0;
			this.vScrollBar17.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// label31
			// 
			this.label31.Font = new System.Drawing.Font("宋体", 8F);
			this.label31.Location = new System.Drawing.Point(687, 28);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(14, 182);
			this.label31.TabIndex = 10;
			this.label31.Text = "总调光1";
			this.label31.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label31.Visible = false;
			this.label31.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// vScrollBar18
			// 
			this.vScrollBar18.Location = new System.Drawing.Point(80, 28);
			this.vScrollBar18.Maximum = 264;
			this.vScrollBar18.Name = "vScrollBar18";
			this.vScrollBar18.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar18.TabIndex = 0;
			this.vScrollBar18.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar19
			// 
			this.vScrollBar19.Location = new System.Drawing.Point(128, 28);
			this.vScrollBar19.Maximum = 264;
			this.vScrollBar19.Name = "vScrollBar19";
			this.vScrollBar19.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar19.TabIndex = 0;
			this.vScrollBar19.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar20
			// 
			this.vScrollBar20.Location = new System.Drawing.Point(176, 28);
			this.vScrollBar20.Maximum = 264;
			this.vScrollBar20.Name = "vScrollBar20";
			this.vScrollBar20.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar20.TabIndex = 0;
			this.vScrollBar20.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// label30
			// 
			this.label30.Font = new System.Drawing.Font("宋体", 8F);
			this.label30.Location = new System.Drawing.Point(639, 28);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(14, 182);
			this.label30.TabIndex = 10;
			this.label30.Text = "总调光1";
			this.label30.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label30.Visible = false;
			this.label30.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// vScrollBar21
			// 
			this.vScrollBar21.Location = new System.Drawing.Point(224, 28);
			this.vScrollBar21.Maximum = 264;
			this.vScrollBar21.Name = "vScrollBar21";
			this.vScrollBar21.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar21.TabIndex = 0;
			this.vScrollBar21.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar22
			// 
			this.vScrollBar22.Location = new System.Drawing.Point(272, 28);
			this.vScrollBar22.Maximum = 264;
			this.vScrollBar22.Name = "vScrollBar22";
			this.vScrollBar22.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar22.TabIndex = 0;
			this.vScrollBar22.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// label29
			// 
			this.label29.Font = new System.Drawing.Font("宋体", 8F);
			this.label29.Location = new System.Drawing.Point(591, 28);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(14, 182);
			this.label29.TabIndex = 10;
			this.label29.Text = "总调光1";
			this.label29.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label29.Visible = false;
			this.label29.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// vScrollBar23
			// 
			this.vScrollBar23.Location = new System.Drawing.Point(320, 28);
			this.vScrollBar23.Maximum = 264;
			this.vScrollBar23.Name = "vScrollBar23";
			this.vScrollBar23.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar23.TabIndex = 0;
			this.vScrollBar23.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar24
			// 
			this.vScrollBar24.Location = new System.Drawing.Point(368, 28);
			this.vScrollBar24.Maximum = 264;
			this.vScrollBar24.Name = "vScrollBar24";
			this.vScrollBar24.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar24.TabIndex = 0;
			this.vScrollBar24.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar25
			// 
			this.vScrollBar25.Location = new System.Drawing.Point(416, 28);
			this.vScrollBar25.Maximum = 264;
			this.vScrollBar25.Name = "vScrollBar25";
			this.vScrollBar25.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar25.TabIndex = 0;
			this.vScrollBar25.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// label28
			// 
			this.label28.Font = new System.Drawing.Font("宋体", 8F);
			this.label28.Location = new System.Drawing.Point(543, 28);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(14, 182);
			this.label28.TabIndex = 10;
			this.label28.Text = "总调光1";
			this.label28.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label28.Visible = false;
			this.label28.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// vScrollBar26
			// 
			this.vScrollBar26.Location = new System.Drawing.Point(464, 28);
			this.vScrollBar26.Maximum = 264;
			this.vScrollBar26.Name = "vScrollBar26";
			this.vScrollBar26.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar26.TabIndex = 0;
			this.vScrollBar26.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar27
			// 
			this.vScrollBar27.Location = new System.Drawing.Point(512, 28);
			this.vScrollBar27.Maximum = 264;
			this.vScrollBar27.Name = "vScrollBar27";
			this.vScrollBar27.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar27.TabIndex = 0;
			this.vScrollBar27.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar28
			// 
			this.vScrollBar28.Location = new System.Drawing.Point(560, 28);
			this.vScrollBar28.Maximum = 264;
			this.vScrollBar28.Name = "vScrollBar28";
			this.vScrollBar28.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar28.TabIndex = 0;
			this.vScrollBar28.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// label27
			// 
			this.label27.Font = new System.Drawing.Font("宋体", 8F);
			this.label27.Location = new System.Drawing.Point(495, 28);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(14, 182);
			this.label27.TabIndex = 10;
			this.label27.Text = "总调光1";
			this.label27.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label27.Visible = false;
			this.label27.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// vScrollBar29
			// 
			this.vScrollBar29.Location = new System.Drawing.Point(608, 28);
			this.vScrollBar29.Maximum = 264;
			this.vScrollBar29.Name = "vScrollBar29";
			this.vScrollBar29.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar29.TabIndex = 0;
			this.vScrollBar29.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar30
			// 
			this.vScrollBar30.Location = new System.Drawing.Point(656, 28);
			this.vScrollBar30.Maximum = 264;
			this.vScrollBar30.Name = "vScrollBar30";
			this.vScrollBar30.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar30.TabIndex = 0;
			this.vScrollBar30.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar31
			// 
			this.vScrollBar31.Location = new System.Drawing.Point(704, 28);
			this.vScrollBar31.Maximum = 264;
			this.vScrollBar31.Name = "vScrollBar31";
			this.vScrollBar31.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar31.TabIndex = 0;
			this.vScrollBar31.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("宋体", 8F);
			this.label26.Location = new System.Drawing.Point(446, 28);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(14, 182);
			this.label26.TabIndex = 10;
			this.label26.Text = "总调光1";
			this.label26.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label26.Visible = false;
			this.label26.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// vScrollBar32
			// 
			this.vScrollBar32.Location = new System.Drawing.Point(752, 28);
			this.vScrollBar32.Maximum = 264;
			this.vScrollBar32.Name = "vScrollBar32";
			this.vScrollBar32.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar32.TabIndex = 0;
			this.vScrollBar32.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// label17
			// 
			this.label17.Font = new System.Drawing.Font("宋体", 8F);
			this.label17.Location = new System.Drawing.Point(14, 28);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(14, 182);
			this.label17.TabIndex = 10;
			this.label17.Text = "总调光1";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label17.Visible = false;
			this.label17.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label22
			// 
			this.label22.Font = new System.Drawing.Font("宋体", 8F);
			this.label22.Location = new System.Drawing.Point(255, 28);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(14, 182);
			this.label22.TabIndex = 10;
			this.label22.Text = "总调光1";
			this.label22.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label22.Visible = false;
			this.label22.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label25
			// 
			this.label25.Font = new System.Drawing.Font("宋体", 8F);
			this.label25.Location = new System.Drawing.Point(399, 28);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(14, 182);
			this.label25.TabIndex = 10;
			this.label25.Text = "总调光1";
			this.label25.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label25.Visible = false;
			this.label25.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("宋体", 8F);
			this.label20.Location = new System.Drawing.Point(159, 28);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(14, 182);
			this.label20.TabIndex = 10;
			this.label20.Text = "总调光1";
			this.label20.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label20.Visible = false;
			this.label20.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("宋体", 8F);
			this.label19.Location = new System.Drawing.Point(111, 28);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(14, 182);
			this.label19.TabIndex = 10;
			this.label19.Text = "总调光1";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label19.Visible = false;
			this.label19.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label21
			// 
			this.label21.Font = new System.Drawing.Font("宋体", 8F);
			this.label21.Location = new System.Drawing.Point(205, 28);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(14, 182);
			this.label21.TabIndex = 10;
			this.label21.Text = "总调光1";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label21.Visible = false;
			this.label21.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label23
			// 
			this.label23.Font = new System.Drawing.Font("宋体", 8F);
			this.label23.Location = new System.Drawing.Point(303, 28);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(14, 182);
			this.label23.TabIndex = 10;
			this.label23.Text = "总调光1";
			this.label23.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label23.Visible = false;
			this.label23.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label24
			// 
			this.label24.Font = new System.Drawing.Font("宋体", 8F);
			this.label24.Location = new System.Drawing.Point(351, 28);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(14, 182);
			this.label24.TabIndex = 10;
			this.label24.Text = "总调光1";
			this.label24.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label24.Visible = false;
			this.label24.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label18
			// 
			this.label18.Font = new System.Drawing.Font("宋体", 8F);
			this.label18.Location = new System.Drawing.Point(63, 28);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(14, 182);
			this.label18.TabIndex = 10;
			this.label18.Text = "总调光1";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label18.Visible = false;
			this.label18.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// tongdaoGroupBox1
			// 
			this.tongdaoGroupBox1.BackColor = System.Drawing.Color.MediumTurquoise;
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown16);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown15);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown14);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown13);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown12);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown11);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown10);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown9);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown8);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown7);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown6);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown5);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown4);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown3);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown2);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown1);
			this.tongdaoGroupBox1.Controls.Add(this.label16);
			this.tongdaoGroupBox1.Controls.Add(this.label15);
			this.tongdaoGroupBox1.Controls.Add(this.label14);
			this.tongdaoGroupBox1.Controls.Add(this.label13);
			this.tongdaoGroupBox1.Controls.Add(this.label12);
			this.tongdaoGroupBox1.Controls.Add(this.label11);
			this.tongdaoGroupBox1.Controls.Add(this.label10);
			this.tongdaoGroupBox1.Controls.Add(this.label9);
			this.tongdaoGroupBox1.Controls.Add(this.label8);
			this.tongdaoGroupBox1.Controls.Add(this.label7);
			this.tongdaoGroupBox1.Controls.Add(this.label6);
			this.tongdaoGroupBox1.Controls.Add(this.label5);
			this.tongdaoGroupBox1.Controls.Add(this.label4);
			this.tongdaoGroupBox1.Controls.Add(this.label3);
			this.tongdaoGroupBox1.Controls.Add(this.label2);
			this.tongdaoGroupBox1.Controls.Add(this.label1);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar16);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar12);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar8);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar4);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar15);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar11);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar14);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar10);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar7);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar13);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar6);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar9);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar3);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar5);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar2);
			this.tongdaoGroupBox1.Controls.Add(this.vScrollBar1);
			this.tongdaoGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tongdaoGroupBox1.Location = new System.Drawing.Point(2, 2);
			this.tongdaoGroupBox1.Margin = new System.Windows.Forms.Padding(2);
			this.tongdaoGroupBox1.Name = "tongdaoGroupBox1";
			this.tongdaoGroupBox1.Padding = new System.Windows.Forms.Padding(2);
			this.tongdaoGroupBox1.Size = new System.Drawing.Size(789, 255);
			this.tongdaoGroupBox1.TabIndex = 8;
			this.tongdaoGroupBox1.TabStop = false;
			this.tongdaoGroupBox1.Visible = false;
			// 
			// numericUpDown16
			// 
			this.numericUpDown16.Location = new System.Drawing.Point(737, 223);
			this.numericUpDown16.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown16.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown16.Name = "numericUpDown16";
			this.numericUpDown16.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown16.TabIndex = 11;
			this.numericUpDown16.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown15
			// 
			this.numericUpDown15.Location = new System.Drawing.Point(689, 223);
			this.numericUpDown15.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown15.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown15.Name = "numericUpDown15";
			this.numericUpDown15.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown15.TabIndex = 11;
			this.numericUpDown15.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown14
			// 
			this.numericUpDown14.Location = new System.Drawing.Point(641, 223);
			this.numericUpDown14.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown14.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown14.Name = "numericUpDown14";
			this.numericUpDown14.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown14.TabIndex = 11;
			this.numericUpDown14.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown13
			// 
			this.numericUpDown13.Location = new System.Drawing.Point(593, 223);
			this.numericUpDown13.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown13.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown13.Name = "numericUpDown13";
			this.numericUpDown13.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown13.TabIndex = 11;
			this.numericUpDown13.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown12
			// 
			this.numericUpDown12.Location = new System.Drawing.Point(545, 223);
			this.numericUpDown12.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown12.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown12.Name = "numericUpDown12";
			this.numericUpDown12.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown12.TabIndex = 11;
			this.numericUpDown12.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown11
			// 
			this.numericUpDown11.Location = new System.Drawing.Point(497, 223);
			this.numericUpDown11.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown11.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown11.Name = "numericUpDown11";
			this.numericUpDown11.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown11.TabIndex = 11;
			this.numericUpDown11.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown10
			// 
			this.numericUpDown10.Location = new System.Drawing.Point(448, 223);
			this.numericUpDown10.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown10.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown10.Name = "numericUpDown10";
			this.numericUpDown10.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown10.TabIndex = 11;
			this.numericUpDown10.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown9
			// 
			this.numericUpDown9.Location = new System.Drawing.Point(401, 223);
			this.numericUpDown9.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown9.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown9.Name = "numericUpDown9";
			this.numericUpDown9.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown9.TabIndex = 11;
			this.numericUpDown9.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown8
			// 
			this.numericUpDown8.Location = new System.Drawing.Point(353, 223);
			this.numericUpDown8.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown8.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown8.Name = "numericUpDown8";
			this.numericUpDown8.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown8.TabIndex = 11;
			this.numericUpDown8.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown7
			// 
			this.numericUpDown7.Location = new System.Drawing.Point(305, 223);
			this.numericUpDown7.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown7.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown7.Name = "numericUpDown7";
			this.numericUpDown7.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown7.TabIndex = 11;
			this.numericUpDown7.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown6
			// 
			this.numericUpDown6.Location = new System.Drawing.Point(257, 223);
			this.numericUpDown6.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown6.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown6.Name = "numericUpDown6";
			this.numericUpDown6.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown6.TabIndex = 11;
			this.numericUpDown6.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown5
			// 
			this.numericUpDown5.Location = new System.Drawing.Point(207, 223);
			this.numericUpDown5.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown5.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown5.Name = "numericUpDown5";
			this.numericUpDown5.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown5.TabIndex = 11;
			this.numericUpDown5.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown4
			// 
			this.numericUpDown4.Location = new System.Drawing.Point(161, 223);
			this.numericUpDown4.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown4.TabIndex = 11;
			this.numericUpDown4.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown3
			// 
			this.numericUpDown3.Location = new System.Drawing.Point(110, 223);
			this.numericUpDown3.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown3.TabIndex = 11;
			this.numericUpDown3.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Location = new System.Drawing.Point(64, 223);
			this.numericUpDown2.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown2.TabIndex = 11;
			this.numericUpDown2.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(16, 223);
			this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(39, 21);
			this.numericUpDown1.TabIndex = 11;
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// label16
			// 
			this.label16.Font = new System.Drawing.Font("宋体", 8F);
			this.label16.Location = new System.Drawing.Point(734, 30);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(14, 182);
			this.label16.TabIndex = 10;
			this.label16.Text = "总调光1";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label16.Visible = false;
			this.label16.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("宋体", 8F);
			this.label15.Location = new System.Drawing.Point(686, 30);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(14, 182);
			this.label15.TabIndex = 10;
			this.label15.Text = "总调光1";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label15.Visible = false;
			this.label15.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("宋体", 8F);
			this.label14.Location = new System.Drawing.Point(638, 30);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(14, 182);
			this.label14.TabIndex = 10;
			this.label14.Text = "总调光1";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label14.Visible = false;
			this.label14.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("宋体", 8F);
			this.label13.Location = new System.Drawing.Point(590, 30);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(14, 182);
			this.label13.TabIndex = 10;
			this.label13.Text = "总调光1";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label13.Visible = false;
			this.label13.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("宋体", 8F);
			this.label12.Location = new System.Drawing.Point(542, 30);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(14, 182);
			this.label12.TabIndex = 10;
			this.label12.Text = "总调光1";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label12.Visible = false;
			this.label12.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("宋体", 8F);
			this.label11.Location = new System.Drawing.Point(494, 30);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(14, 182);
			this.label11.TabIndex = 10;
			this.label11.Text = "总调光1";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label11.Visible = false;
			this.label11.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("宋体", 8F);
			this.label10.Location = new System.Drawing.Point(446, 30);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(14, 182);
			this.label10.TabIndex = 10;
			this.label10.Text = "总调光1";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label10.Visible = false;
			this.label10.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("宋体", 8F);
			this.label9.Location = new System.Drawing.Point(398, 30);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(14, 182);
			this.label9.TabIndex = 10;
			this.label9.Text = "总调光1";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label9.Visible = false;
			this.label9.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("宋体", 8F);
			this.label8.Location = new System.Drawing.Point(350, 30);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(14, 182);
			this.label8.TabIndex = 10;
			this.label8.Text = "总调光1";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label8.Visible = false;
			this.label8.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("宋体", 8F);
			this.label7.Location = new System.Drawing.Point(302, 30);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(14, 182);
			this.label7.TabIndex = 10;
			this.label7.Text = "总调光1";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label7.Visible = false;
			this.label7.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("宋体", 8F);
			this.label6.Location = new System.Drawing.Point(254, 30);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(14, 182);
			this.label6.TabIndex = 10;
			this.label6.Text = "总调光1";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label6.Visible = false;
			this.label6.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("宋体", 8F);
			this.label5.Location = new System.Drawing.Point(204, 30);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(14, 182);
			this.label5.TabIndex = 10;
			this.label5.Text = "总调光1";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label5.Visible = false;
			this.label5.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("宋体", 8F);
			this.label4.Location = new System.Drawing.Point(158, 30);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(14, 182);
			this.label4.TabIndex = 10;
			this.label4.Text = "总调光1";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label4.Visible = false;
			this.label4.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 8F);
			this.label3.Location = new System.Drawing.Point(110, 30);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(14, 182);
			this.label3.TabIndex = 10;
			this.label3.Text = "总调光1";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label3.Visible = false;
			this.label3.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 8F);
			this.label2.Location = new System.Drawing.Point(62, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(14, 182);
			this.label2.TabIndex = 10;
			this.label2.Text = "总调光1";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label2.Visible = false;
			this.label2.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 8F);
			this.label1.Location = new System.Drawing.Point(14, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(14, 182);
			this.label1.TabIndex = 10;
			this.label1.Text = "总调光1";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label1.Visible = false;			
			this.label1.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// vScrollBar16
			// 
			this.vScrollBar16.Location = new System.Drawing.Point(751, 30);
			this.vScrollBar16.Maximum = 264;
			this.vScrollBar16.Name = "vScrollBar16";
			this.vScrollBar16.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar16.TabIndex = 0;
			this.vScrollBar16.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar12
			// 
			this.vScrollBar12.Location = new System.Drawing.Point(559, 30);
			this.vScrollBar12.Maximum = 264;
			this.vScrollBar12.Name = "vScrollBar12";
			this.vScrollBar12.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar12.TabIndex = 0;
			this.vScrollBar12.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar8
			// 
			this.vScrollBar8.Location = new System.Drawing.Point(367, 30);
			this.vScrollBar8.Maximum = 264;
			this.vScrollBar8.Name = "vScrollBar8";
			this.vScrollBar8.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar8.TabIndex = 0;
			this.vScrollBar8.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar4
			// 
			this.vScrollBar4.Location = new System.Drawing.Point(175, 30);
			this.vScrollBar4.Maximum = 264;
			this.vScrollBar4.Name = "vScrollBar4";
			this.vScrollBar4.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar4.TabIndex = 0;
			this.vScrollBar4.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar15
			// 
			this.vScrollBar15.Location = new System.Drawing.Point(703, 30);
			this.vScrollBar15.Maximum = 264;
			this.vScrollBar15.Name = "vScrollBar15";
			this.vScrollBar15.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar15.TabIndex = 0;
			this.vScrollBar15.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar11
			// 
			this.vScrollBar11.Location = new System.Drawing.Point(511, 30);
			this.vScrollBar11.Maximum = 264;
			this.vScrollBar11.Name = "vScrollBar11";
			this.vScrollBar11.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar11.TabIndex = 0;
			this.vScrollBar11.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar14
			// 
			this.vScrollBar14.Location = new System.Drawing.Point(655, 30);
			this.vScrollBar14.Maximum = 264;
			this.vScrollBar14.Name = "vScrollBar14";
			this.vScrollBar14.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar14.TabIndex = 0;
			this.vScrollBar14.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar10
			// 
			this.vScrollBar10.Location = new System.Drawing.Point(463, 30);
			this.vScrollBar10.Maximum = 264;
			this.vScrollBar10.Name = "vScrollBar10";
			this.vScrollBar10.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar10.TabIndex = 0;
			this.vScrollBar10.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar7
			// 
			this.vScrollBar7.Location = new System.Drawing.Point(319, 30);
			this.vScrollBar7.Maximum = 264;
			this.vScrollBar7.Name = "vScrollBar7";
			this.vScrollBar7.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar7.TabIndex = 0;
			this.vScrollBar7.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar13
			// 
			this.vScrollBar13.Location = new System.Drawing.Point(607, 30);
			this.vScrollBar13.Maximum = 264;
			this.vScrollBar13.Name = "vScrollBar13";
			this.vScrollBar13.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar13.TabIndex = 0;
			this.vScrollBar13.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar6
			// 
			this.vScrollBar6.Location = new System.Drawing.Point(271, 30);
			this.vScrollBar6.Maximum = 264;
			this.vScrollBar6.Name = "vScrollBar6";
			this.vScrollBar6.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar6.TabIndex = 0;
			this.vScrollBar6.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar9
			// 
			this.vScrollBar9.Location = new System.Drawing.Point(415, 30);
			this.vScrollBar9.Maximum = 264;
			this.vScrollBar9.Name = "vScrollBar9";
			this.vScrollBar9.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar9.TabIndex = 0;
			this.vScrollBar9.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar3
			// 
			this.vScrollBar3.Location = new System.Drawing.Point(127, 30);
			this.vScrollBar3.Maximum = 264;
			this.vScrollBar3.Name = "vScrollBar3";
			this.vScrollBar3.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar3.TabIndex = 0;
			this.vScrollBar3.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar5
			// 
			this.vScrollBar5.Location = new System.Drawing.Point(223, 30);
			this.vScrollBar5.Maximum = 264;
			this.vScrollBar5.Name = "vScrollBar5";
			this.vScrollBar5.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar5.TabIndex = 0;
			this.vScrollBar5.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar2
			// 
			this.vScrollBar2.Location = new System.Drawing.Point(79, 30);
			this.vScrollBar2.Maximum = 264;
			this.vScrollBar2.Name = "vScrollBar2";
			this.vScrollBar2.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar2.TabIndex = 0;
			this.vScrollBar2.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Location = new System.Drawing.Point(31, 30);
			this.vScrollBar1.Maximum = 264;
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(24, 182);
			this.vScrollBar1.TabIndex = 0;
			this.vScrollBar1.MouseEnter += new System.EventHandler(this.vScrollBar_MouseEnter);
			// 
			// openImageDialog
			// 
			this.openImageDialog.Filter = "所有图片文件|*.bmp;*.jpeg;*.jpg;*.png;*.ico|PNG文件(*.png)|*.png|BMP文件(*.bmp)|*.bmp|JPG文件" +
    "(*.jpg;*.jpeg)|*.jpg;*jpeg|图标文件(*.ico)|*.ico";
			this.openImageDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openImageDialog_FileOk);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "*.ini";
			this.openFileDialog.Filter = "配置文件(*.ini)|*.ini";
			this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
			// 
			// skinEngine2
			// 
			this.skinEngine2.@__DrawButtonFocusRectangle = true;
			this.skinEngine2.DisabledButtonTextColor = System.Drawing.Color.Gray;
			this.skinEngine2.DisabledMenuFontColor = System.Drawing.SystemColors.GrayText;
			this.skinEngine2.InactiveCaptionColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.skinEngine2.SerialNumber = "";
			this.skinEngine2.SkinFile = null;
			// 
			// editGroupBox
			// 
			this.editGroupBox.AutoSize = true;
			this.editGroupBox.Controls.Add(this.groupBox1);
			this.editGroupBox.Controls.Add(this.flowLayoutPanel1);
			this.editGroupBox.Controls.Add(this.lightTestGroupBox);
			this.editGroupBox.Controls.Add(this.tongdaoEditButton);
			this.editGroupBox.Controls.Add(this.generateButton);
			this.editGroupBox.Controls.Add(this.openPictureBox);
			this.editGroupBox.Controls.Add(this.picTextBox);
			this.editGroupBox.Controls.Add(this.typeTextBox);
			this.editGroupBox.Controls.Add(this.nameTextBox);
			this.editGroupBox.Controls.Add(this.tongdaoCountLabel);
			this.editGroupBox.Controls.Add(this.picLabel);
			this.editGroupBox.Controls.Add(this.typeLabel);
			this.editGroupBox.Controls.Add(this.countComboBox);
			this.editGroupBox.Controls.Add(this.nameLabel);
			this.editGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.editGroupBox.Location = new System.Drawing.Point(8, 79);
			this.editGroupBox.Margin = new System.Windows.Forms.Padding(2);
			this.editGroupBox.Name = "editGroupBox";
			this.editGroupBox.Padding = new System.Windows.Forms.Padding(2);
			this.editGroupBox.Size = new System.Drawing.Size(812, 448);
			this.editGroupBox.TabIndex = 6;
			this.editGroupBox.TabStop = false;
			this.editGroupBox.Visible = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.setCurrentToInitButton);
			this.groupBox1.Controls.Add(this.setInitButton);
			this.groupBox1.Controls.Add(this.commonValueNumericUpDown);
			this.groupBox1.Controls.Add(this.zeroButton);
			this.groupBox1.Controls.Add(this.commonValueButton);
			this.groupBox1.Location = new System.Drawing.Point(344, 30);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
			this.groupBox1.Size = new System.Drawing.Size(206, 127);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "设通道值";
			// 
			// setCurrentToInitButton
			// 
			this.setCurrentToInitButton.Location = new System.Drawing.Point(20, 94);
			this.setCurrentToInitButton.Margin = new System.Windows.Forms.Padding(2);
			this.setCurrentToInitButton.Name = "setCurrentToInitButton";
			this.setCurrentToInitButton.Size = new System.Drawing.Size(159, 23);
			this.setCurrentToInitButton.TabIndex = 2;
			this.setCurrentToInitButton.Text = "设当前通道值为初始值";
			this.setCurrentToInitButton.UseVisualStyleBackColor = true;
			this.setCurrentToInitButton.Click += new System.EventHandler(this.setCurrentToInitButton_Click);
			// 
			// commonValueNumericUpDown
			// 
			this.commonValueNumericUpDown.Location = new System.Drawing.Point(20, 28);
			this.commonValueNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.commonValueNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.commonValueNumericUpDown.Name = "commonValueNumericUpDown";
			this.commonValueNumericUpDown.Size = new System.Drawing.Size(71, 21);
			this.commonValueNumericUpDown.TabIndex = 1;
			this.commonValueNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// commonValueButton
			// 
			this.commonValueButton.Location = new System.Drawing.Point(106, 26);
			this.commonValueButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.commonValueButton.Name = "commonValueButton";
			this.commonValueButton.Size = new System.Drawing.Size(73, 23);
			this.commonValueButton.TabIndex = 0;
			this.commonValueButton.Text = "统一通道值";
			this.commonValueButton.UseVisualStyleBackColor = true;
			this.commonValueButton.Click += new System.EventHandler(this.commonValueButton_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.Controls.Add(this.tongdaoGroupBox1);
			this.flowLayoutPanel1.Controls.Add(this.tongdaoGroupBox2);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 166);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(810, 264);
			this.flowLayoutPanel1.TabIndex = 7;
			// 
			// connectButton
			// 
			this.connectButton.Enabled = false;
			this.connectButton.Location = new System.Drawing.Point(133, 13);
			this.connectButton.Margin = new System.Windows.Forms.Padding(2);
			this.connectButton.Name = "connectButton";
			this.connectButton.Size = new System.Drawing.Size(86, 46);
			this.connectButton.TabIndex = 4;
			this.connectButton.Text = "连接设备";
			this.connectButton.UseVisualStyleBackColor = true;
			this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// connectPanel
			// 
			this.connectPanel.Controls.Add(this.refreshButton);
			this.connectPanel.Controls.Add(this.comComboBox);
			this.connectPanel.Controls.Add(this.connectButton);
			this.connectPanel.Location = new System.Drawing.Point(426, 5);
			this.connectPanel.Name = "connectPanel";
			this.connectPanel.Size = new System.Drawing.Size(240, 68);
			this.connectPanel.TabIndex = 7;
			this.connectPanel.Visible = false;
			// 
			// refreshButton
			// 
			this.refreshButton.Location = new System.Drawing.Point(24, 40);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(86, 20);
			this.refreshButton.TabIndex = 5;
			this.refreshButton.Text = "刷新串口";
			this.refreshButton.UseVisualStyleBackColor = true;
			this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoSize = true;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(828, 530);
			this.Controls.Add(this.connectPanel);
			this.Controls.Add(this.editGroupBox);
			this.Controls.Add(this.exitButton);
			this.Controls.Add(this.saveLightButton);
			this.Controls.Add(this.openLightButton);
			this.Controls.Add(this.newLightButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HelpButton = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Padding = new System.Windows.Forms.Padding(8, 2, 8, 3);
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.MainForm_HelpButtonClicked);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.lightTestGroupBox.ResumeLayout(false);
			this.lightTestGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.firstTDNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.openPictureBox)).EndInit();
			this.tongdaoGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown32)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown31)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown30)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown29)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown28)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown27)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown26)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown25)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown24)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown23)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown22)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown21)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown20)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown19)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown18)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown17)).EndInit();
			this.tongdaoGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown16)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown15)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown14)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown13)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown10)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.editGroupBox.ResumeLayout(false);
			this.editGroupBox.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.commonValueNumericUpDown)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.connectPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion



		public Label[] labels = new Label[32];
		public VScrollBar[] valueVScrollBars = new VScrollBar[32];
		public NumericUpDown[] valueNumericUpDowns = new NumericUpDown[32];

		private Button tongdaoEditButton;
		private Button zeroButton;
		private GroupBox lightTestGroupBox;
		public Label label1;
		public Label label2;
		public Label label3;
		public Label label4;
		public Label label5;
		public Label label6;
		public Label label7;
		public Label label8;
		public Label label9;
		public Label label10;
		public Label label11;
		public Label label12;
		public Label label13;
		public Label label14;
		public Label label15;
		public Label label16;
		public Label label17;
		public Label label18;
		public Label label19;
		public Label label20;
		public Label label21;
		public Label label22;
		public Label label23;
		public Label label24;
		public Label label25;
		public Label label26;
		public Label label27;
		public Label label28;
		public Label label29;
		public Label label30;
		public Label label31;
		public Label label32;
		private System.Windows.Forms.Button exitButton;
		private System.Windows.Forms.Button newLightButton;
		private System.Windows.Forms.Button generateButton;
		private System.Windows.Forms.Button openLightButton;
		private System.Windows.Forms.Button saveLightButton;
		private System.Windows.Forms.ComboBox comComboBox;
		private System.Windows.Forms.ComboBox countComboBox;
		private System.Windows.Forms.GroupBox tongdaoGroupBox1;
		private System.Windows.Forms.GroupBox tongdaoGroupBox2;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label picLabel;
		private System.Windows.Forms.Label tongdaoCountLabel;
		private System.Windows.Forms.Label typeLabel;
		private System.Windows.Forms.OpenFileDialog openImageDialog;
		private System.Windows.Forms.PictureBox openPictureBox;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.TextBox picTextBox;
		private System.Windows.Forms.TextBox typeTextBox;
		public System.Windows.Forms.VScrollBar vScrollBar1;
		public System.Windows.Forms.VScrollBar vScrollBar2;
		public System.Windows.Forms.VScrollBar vScrollBar3;
		public System.Windows.Forms.VScrollBar vScrollBar4;
		public System.Windows.Forms.VScrollBar vScrollBar5;
		public System.Windows.Forms.VScrollBar vScrollBar6;
		public System.Windows.Forms.VScrollBar vScrollBar7;
		public System.Windows.Forms.VScrollBar vScrollBar8;
		public System.Windows.Forms.VScrollBar vScrollBar9;
		public System.Windows.Forms.VScrollBar vScrollBar10;
		public System.Windows.Forms.VScrollBar vScrollBar11;
		public System.Windows.Forms.VScrollBar vScrollBar12;
		public System.Windows.Forms.VScrollBar vScrollBar13;
		public System.Windows.Forms.VScrollBar vScrollBar14;
		public System.Windows.Forms.VScrollBar vScrollBar15;
		public System.Windows.Forms.VScrollBar vScrollBar16;
		public System.Windows.Forms.VScrollBar vScrollBar17;
		public System.Windows.Forms.VScrollBar vScrollBar18;
		public System.Windows.Forms.VScrollBar vScrollBar19;
		public System.Windows.Forms.VScrollBar vScrollBar20;
		public System.Windows.Forms.VScrollBar vScrollBar21;
		public System.Windows.Forms.VScrollBar vScrollBar22;
		public System.Windows.Forms.VScrollBar vScrollBar23;
		public System.Windows.Forms.VScrollBar vScrollBar24;
		public System.Windows.Forms.VScrollBar vScrollBar25;
		public System.Windows.Forms.VScrollBar vScrollBar26;
		public System.Windows.Forms.VScrollBar vScrollBar27;
		public System.Windows.Forms.VScrollBar vScrollBar28;
		public System.Windows.Forms.VScrollBar vScrollBar29;
		public System.Windows.Forms.VScrollBar vScrollBar30;
		public System.Windows.Forms.VScrollBar vScrollBar31;
		public System.Windows.Forms.VScrollBar vScrollBar32;

		private NumericUpDown numericUpDown1;
		private NumericUpDown numericUpDown2;
		private NumericUpDown numericUpDown3;
		private NumericUpDown numericUpDown4;
		private NumericUpDown numericUpDown5;
		private NumericUpDown numericUpDown6;
		private NumericUpDown numericUpDown7;
		private NumericUpDown numericUpDown8;
		private NumericUpDown numericUpDown9;
		private NumericUpDown numericUpDown10;
		private NumericUpDown numericUpDown11;
		private NumericUpDown numericUpDown12;
		private NumericUpDown numericUpDown13;
		private NumericUpDown numericUpDown14;
		private NumericUpDown numericUpDown15;
		private NumericUpDown numericUpDown16;
		private NumericUpDown numericUpDown17;
		private NumericUpDown numericUpDown18;
		private NumericUpDown numericUpDown19;
		private NumericUpDown numericUpDown20;
		private NumericUpDown numericUpDown21;
		private NumericUpDown numericUpDown22;
		private NumericUpDown numericUpDown23;
		private NumericUpDown numericUpDown24;
		private NumericUpDown numericUpDown25;
		private NumericUpDown numericUpDown26;
		private NumericUpDown numericUpDown27;
		private NumericUpDown numericUpDown28;
		private NumericUpDown numericUpDown29;
		private NumericUpDown numericUpDown30;
		private NumericUpDown numericUpDown31;
		private NumericUpDown numericUpDown32;

		public List<TongdaoWrapper> tongdaoList;
		private OpenFileDialog openFileDialog;
		public int tongdaoCount;
		private Sunisoft.IrisSkin.SkinEngine skinEngine2;
		private Button setInitButton;
		private GroupBox editGroupBox;
		private FlowLayoutPanel flowLayoutPanel1;
		private NumericUpDown firstTDNumericUpDown;
		private Button setFirstTDButton;
		private Button testButton;
		private CheckBox realtimeCheckBox;
		private Button endTestButton;
		private Button connectButton;
		private GroupBox groupBox1;
		private NumericUpDown commonValueNumericUpDown;
		private Button commonValueButton;
		private Button setCurrentToInitButton;
		private Panel connectPanel;
		private Button refreshButton;
		private ToolTip myToolTip;
	}
}

