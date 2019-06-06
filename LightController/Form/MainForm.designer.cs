using LightController.Ast;
using System.Windows.Forms;

namespace LightController
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
			this.openComButton = new System.Windows.Forms.Button();
			this.newFileButton = new System.Windows.Forms.Button();
			this.openFileButton = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.saveAsButton = new System.Windows.Forms.Button();
			this.exitButton = new System.Windows.Forms.Button();
			this.lightEditButton = new System.Windows.Forms.Button();
			this.globleSetButton = new System.Windows.Forms.Button();
			this.oneKeyButton = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
			this.lightsListView = new System.Windows.Forms.ListView();
			this.lightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LargeImageList = new System.Windows.Forms.ImageList(this.components);
			this.tongdaoGroupBox = new System.Windows.Forms.GroupBox();
			this.nextStepButton = new System.Windows.Forms.Button();
			this.deleteStepButton = new System.Windows.Forms.Button();
			this.backStepButton = new System.Windows.Forms.Button();
			this.insertStepButton = new System.Windows.Forms.Button();
			this.newStepButton = new System.Windows.Forms.Button();
			this.lightValueLabel = new System.Windows.Forms.Label();
			this.lightLabel = new System.Windows.Forms.Label();
			this.tongdaoGroupBox2 = new System.Windows.Forms.GroupBox();
			this.numericUpDown32 = new System.Windows.Forms.NumericUpDown();
			this.label32 = new System.Windows.Forms.Label();
			this.numericUpDown31 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar17 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown30 = new System.Windows.Forms.NumericUpDown();
			this.label31 = new System.Windows.Forms.Label();
			this.numericUpDown29 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar18 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown28 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar19 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown27 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar20 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown26 = new System.Windows.Forms.NumericUpDown();
			this.label30 = new System.Windows.Forms.Label();
			this.numericUpDown25 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar21 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown24 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar22 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown23 = new System.Windows.Forms.NumericUpDown();
			this.label29 = new System.Windows.Forms.Label();
			this.numericUpDown22 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar23 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown21 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar24 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown20 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar25 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown19 = new System.Windows.Forms.NumericUpDown();
			this.label28 = new System.Windows.Forms.Label();
			this.numericUpDown18 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar26 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown17 = new System.Windows.Forms.NumericUpDown();
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
			this.groupComboBox = new System.Windows.Forms.ComboBox();
			this.frameComboBox = new System.Windows.Forms.ComboBox();
			this.modeComboBox = new System.Windows.Forms.ComboBox();
			this.stepLabel = new System.Windows.Forms.Label();
			this.tongdaoGroupBox.SuspendLayout();
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
			this.SuspendLayout();
			// 
			// comComboBox
			// 
			this.comComboBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.comComboBox.FormattingEnabled = true;
			this.comComboBox.Location = new System.Drawing.Point(11, 10);
			this.comComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.comComboBox.Name = "comComboBox";
			this.comComboBox.Size = new System.Drawing.Size(121, 28);
			this.comComboBox.TabIndex = 0;
			this.comComboBox.SelectedIndexChanged += new System.EventHandler(this.comComboBox_SelectedIndexChanged);
			// 
			// openComButton
			// 
			this.openComButton.Enabled = false;
			this.openComButton.Location = new System.Drawing.Point(143, 8);
			this.openComButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.openComButton.Name = "openComButton";
			this.openComButton.Size = new System.Drawing.Size(92, 32);
			this.openComButton.TabIndex = 1;
			this.openComButton.Text = "打开串口";
			this.openComButton.UseVisualStyleBackColor = true;
			this.openComButton.Click += new System.EventHandler(this.openCOMbutton_Click);
			// 
			// newFileButton
			// 
			this.newFileButton.Enabled = false;
			this.newFileButton.Location = new System.Drawing.Point(240, 8);
			this.newFileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.newFileButton.Name = "newFileButton";
			this.newFileButton.Size = new System.Drawing.Size(54, 32);
			this.newFileButton.TabIndex = 2;
			this.newFileButton.Text = "新建";
			this.newFileButton.UseVisualStyleBackColor = true;
			this.newFileButton.Click += new System.EventHandler(this.newButton_Click);
			// 
			// openFileButton
			// 
			this.openFileButton.Enabled = false;
			this.openFileButton.Location = new System.Drawing.Point(299, 8);
			this.openFileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.openFileButton.Name = "openFileButton";
			this.openFileButton.Size = new System.Drawing.Size(54, 32);
			this.openFileButton.TabIndex = 3;
			this.openFileButton.Text = "打开";
			this.openFileButton.UseVisualStyleBackColor = true;
			this.openFileButton.Click += new System.EventHandler(this.openButton_Click);
			// 
			// saveButton
			// 
			this.saveButton.Enabled = false;
			this.saveButton.Location = new System.Drawing.Point(358, 8);
			this.saveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(54, 32);
			this.saveButton.TabIndex = 4;
			this.saveButton.Text = "保存";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// saveAsButton
			// 
			this.saveAsButton.Enabled = false;
			this.saveAsButton.Location = new System.Drawing.Point(417, 8);
			this.saveAsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.saveAsButton.Name = "saveAsButton";
			this.saveAsButton.Size = new System.Drawing.Size(54, 32);
			this.saveAsButton.TabIndex = 5;
			this.saveAsButton.Text = "另存";
			this.saveAsButton.UseVisualStyleBackColor = true;
			this.saveAsButton.Visible = false;
			// 
			// exitButton
			// 
			this.exitButton.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.exitButton.Location = new System.Drawing.Point(889, 7);
			this.exitButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(95, 32);
			this.exitButton.TabIndex = 6;
			this.exitButton.Text = "退出";
			this.exitButton.UseVisualStyleBackColor = true;
			this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
			// 
			// lightEditButton
			// 
			this.lightEditButton.Enabled = false;
			this.lightEditButton.Location = new System.Drawing.Point(476, 8);
			this.lightEditButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.lightEditButton.Name = "lightEditButton";
			this.lightEditButton.Size = new System.Drawing.Size(83, 32);
			this.lightEditButton.TabIndex = 1;
			this.lightEditButton.Text = "灯具编辑";
			this.lightEditButton.UseVisualStyleBackColor = true;
			this.lightEditButton.Click += new System.EventHandler(this.lightEditButton_Click);
			// 
			// globleSetButton
			// 
			this.globleSetButton.Enabled = false;
			this.globleSetButton.Location = new System.Drawing.Point(564, 8);
			this.globleSetButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.globleSetButton.Name = "globleSetButton";
			this.globleSetButton.Size = new System.Drawing.Size(83, 32);
			this.globleSetButton.TabIndex = 2;
			this.globleSetButton.Text = "全局设置";
			this.globleSetButton.UseVisualStyleBackColor = true;
			// 
			// oneKeyButton
			// 
			this.oneKeyButton.Enabled = false;
			this.oneKeyButton.Location = new System.Drawing.Point(652, 8);
			this.oneKeyButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.oneKeyButton.Name = "oneKeyButton";
			this.oneKeyButton.Size = new System.Drawing.Size(82, 32);
			this.oneKeyButton.TabIndex = 4;
			this.oneKeyButton.Text = "一键配置";
			this.oneKeyButton.UseVisualStyleBackColor = true;
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "LightControllerTest_ANSI.txt";
			this.openFileDialog.Filter = "文本文件(*.txt)|*.txt|图片文件(*.png)|*.png";
			this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
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
			// lightsListView
			// 
			this.lightsListView.BackColor = System.Drawing.Color.Snow;
			this.lightsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lightType});
			this.lightsListView.LargeImageList = this.LargeImageList;
			this.lightsListView.Location = new System.Drawing.Point(11, 53);
			this.lightsListView.MultiSelect = false;
			this.lightsListView.Name = "lightsListView";
			this.lightsListView.Size = new System.Drawing.Size(1430, 162);
			this.lightsListView.SmallImageList = this.LargeImageList;
			this.lightsListView.TabIndex = 7;
			this.lightsListView.UseCompatibleStateImageBehavior = false;
			this.lightsListView.SelectedIndexChanged += new System.EventHandler(this.lightsListView_SelectedIndexChanged);
			// 
			// lightType
			// 
			this.lightType.Text = "LightType";
			// 
			// LargeImageList
			// 
			this.LargeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("LargeImageList.ImageStream")));
			this.LargeImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.LargeImageList.Images.SetKeyName(0, "RGB.ico");
			this.LargeImageList.Images.SetKeyName(1, "XY轴.ico");
			this.LargeImageList.Images.SetKeyName(2, "对焦.ico");
			this.LargeImageList.Images.SetKeyName(3, "虹膜.ico");
			this.LargeImageList.Images.SetKeyName(4, "混色.ico");
			this.LargeImageList.Images.SetKeyName(5, "棱镜.ico");
			this.LargeImageList.Images.SetKeyName(6, "频闪.ico");
			this.LargeImageList.Images.SetKeyName(7, "速度.ico");
			this.LargeImageList.Images.SetKeyName(8, "调光.ico");
			this.LargeImageList.Images.SetKeyName(9, "图案.ico");
			this.LargeImageList.Images.SetKeyName(10, "图案自转.ico");
			this.LargeImageList.Images.SetKeyName(11, "未知.ico");
			this.LargeImageList.Images.SetKeyName(12, "颜色.ico");
			// 
			// tongdaoGroupBox
			// 
			this.tongdaoGroupBox.BackColor = System.Drawing.Color.Transparent;
			this.tongdaoGroupBox.Controls.Add(this.stepLabel);
			this.tongdaoGroupBox.Controls.Add(this.nextStepButton);
			this.tongdaoGroupBox.Controls.Add(this.deleteStepButton);
			this.tongdaoGroupBox.Controls.Add(this.backStepButton);
			this.tongdaoGroupBox.Controls.Add(this.insertStepButton);
			this.tongdaoGroupBox.Controls.Add(this.newStepButton);
			this.tongdaoGroupBox.Controls.Add(this.lightValueLabel);
			this.tongdaoGroupBox.Controls.Add(this.lightLabel);
			this.tongdaoGroupBox.Controls.Add(this.tongdaoGroupBox2);
			this.tongdaoGroupBox.Controls.Add(this.tongdaoGroupBox1);
			this.tongdaoGroupBox.Controls.Add(this.groupComboBox);
			this.tongdaoGroupBox.Controls.Add(this.frameComboBox);
			this.tongdaoGroupBox.Controls.Add(this.modeComboBox);
			this.tongdaoGroupBox.Location = new System.Drawing.Point(11, 221);
			this.tongdaoGroupBox.Name = "tongdaoGroupBox";
			this.tongdaoGroupBox.Size = new System.Drawing.Size(1430, 739);
			this.tongdaoGroupBox.TabIndex = 8;
			this.tongdaoGroupBox.TabStop = false;
			this.tongdaoGroupBox.Visible = false;
			// 
			// nextStepButton
			// 
			this.nextStepButton.Location = new System.Drawing.Point(1305, 116);
			this.nextStepButton.Name = "nextStepButton";
			this.nextStepButton.Size = new System.Drawing.Size(89, 32);
			this.nextStepButton.TabIndex = 14;
			this.nextStepButton.Text = "下一步";
			this.nextStepButton.UseVisualStyleBackColor = true;
			// 
			// deleteStepButton
			// 
			this.deleteStepButton.Location = new System.Drawing.Point(1305, 63);
			this.deleteStepButton.Name = "deleteStepButton";
			this.deleteStepButton.Size = new System.Drawing.Size(89, 32);
			this.deleteStepButton.TabIndex = 14;
			this.deleteStepButton.Text = "删除步";
			this.deleteStepButton.UseVisualStyleBackColor = true;
			// 
			// backStepButton
			// 
			this.backStepButton.Location = new System.Drawing.Point(1095, 116);
			this.backStepButton.Name = "backStepButton";
			this.backStepButton.Size = new System.Drawing.Size(89, 32);
			this.backStepButton.TabIndex = 13;
			this.backStepButton.Text = "上一步";
			this.backStepButton.UseVisualStyleBackColor = true;
			// 
			// insertStepButton
			// 
			this.insertStepButton.Location = new System.Drawing.Point(1200, 63);
			this.insertStepButton.Name = "insertStepButton";
			this.insertStepButton.Size = new System.Drawing.Size(89, 32);
			this.insertStepButton.TabIndex = 14;
			this.insertStepButton.Text = "插入步";
			this.insertStepButton.UseVisualStyleBackColor = true;
			this.insertStepButton.Visible = false;
			// 
			// newStepButton
			// 
			this.newStepButton.Location = new System.Drawing.Point(1095, 63);
			this.newStepButton.Name = "newStepButton";
			this.newStepButton.Size = new System.Drawing.Size(89, 32);
			this.newStepButton.TabIndex = 13;
			this.newStepButton.Text = "新建步";
			this.newStepButton.UseVisualStyleBackColor = true;
			this.newStepButton.Click += new System.EventHandler(this.newStepButton_Click);
			// 
			// lightValueLabel
			// 
			this.lightValueLabel.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightValueLabel.Location = new System.Drawing.Point(96, 23);
			this.lightValueLabel.Name = "lightValueLabel";
			this.lightValueLabel.Size = new System.Drawing.Size(210, 15);
			this.lightValueLabel.TabIndex = 12;
			this.lightValueLabel.Text = "       ";
			// 
			// lightLabel
			// 
			this.lightLabel.AutoSize = true;
			this.lightLabel.Location = new System.Drawing.Point(7, 24);
			this.lightLabel.Name = "lightLabel";
			this.lightLabel.Size = new System.Drawing.Size(82, 15);
			this.lightLabel.TabIndex = 11;
			this.lightLabel.Text = "当前灯具：";
			// 
			// tongdaoGroupBox2
			// 
			this.tongdaoGroupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown32);
			this.tongdaoGroupBox2.Controls.Add(this.label32);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown31);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar17);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown30);
			this.tongdaoGroupBox2.Controls.Add(this.label31);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown29);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar18);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown28);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar19);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown27);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar20);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown26);
			this.tongdaoGroupBox2.Controls.Add(this.label30);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown25);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar21);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown24);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar22);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown23);
			this.tongdaoGroupBox2.Controls.Add(this.label29);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown22);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar23);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown21);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar24);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown20);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar25);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown19);
			this.tongdaoGroupBox2.Controls.Add(this.label28);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown18);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar26);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown17);
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
			this.tongdaoGroupBox2.Location = new System.Drawing.Point(7, 399);
			this.tongdaoGroupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tongdaoGroupBox2.Name = "tongdaoGroupBox2";
			this.tongdaoGroupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tongdaoGroupBox2.Size = new System.Drawing.Size(1064, 330);
			this.tongdaoGroupBox2.TabIndex = 10;
			this.tongdaoGroupBox2.TabStop = false;
			// 
			// numericUpDown32
			// 
			this.numericUpDown32.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown32.Location = new System.Drawing.Point(983, 265);
			this.numericUpDown32.Name = "numericUpDown32";
			this.numericUpDown32.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown32.TabIndex = 11;
			// 
			// label32
			// 
			this.label32.Font = new System.Drawing.Font("宋体", 8F);
			this.label32.Location = new System.Drawing.Point(980, 35);
			this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(19, 128);
			this.label32.TabIndex = 10;
			this.label32.Text = "总调光1";
			this.label32.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label32.Visible = false;
			// 
			// numericUpDown31
			// 
			this.numericUpDown31.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown31.Location = new System.Drawing.Point(919, 265);
			this.numericUpDown31.Name = "numericUpDown31";
			this.numericUpDown31.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown31.TabIndex = 11;
			// 
			// vScrollBar17
			// 
			this.vScrollBar17.Location = new System.Drawing.Point(41, 35);
			this.vScrollBar17.Maximum = 264;
			this.vScrollBar17.Name = "vScrollBar17";
			this.vScrollBar17.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar17.TabIndex = 0;
			this.vScrollBar17.Visible = false;
			this.vScrollBar17.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// numericUpDown30
			// 
			this.numericUpDown30.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown30.Location = new System.Drawing.Point(855, 265);
			this.numericUpDown30.Name = "numericUpDown30";
			this.numericUpDown30.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown30.TabIndex = 11;
			// 
			// label31
			// 
			this.label31.Font = new System.Drawing.Font("宋体", 8F);
			this.label31.Location = new System.Drawing.Point(916, 35);
			this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(19, 128);
			this.label31.TabIndex = 10;
			this.label31.Text = "总调光1";
			this.label31.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label31.Visible = false;
			// 
			// numericUpDown29
			// 
			this.numericUpDown29.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown29.Location = new System.Drawing.Point(792, 265);
			this.numericUpDown29.Name = "numericUpDown29";
			this.numericUpDown29.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown29.TabIndex = 11;
			// 
			// vScrollBar18
			// 
			this.vScrollBar18.Location = new System.Drawing.Point(105, 35);
			this.vScrollBar18.Maximum = 264;
			this.vScrollBar18.Name = "vScrollBar18";
			this.vScrollBar18.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar18.TabIndex = 0;
			this.vScrollBar18.Visible = false;
			this.vScrollBar18.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// numericUpDown28
			// 
			this.numericUpDown28.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown28.Location = new System.Drawing.Point(728, 265);
			this.numericUpDown28.Name = "numericUpDown28";
			this.numericUpDown28.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown28.TabIndex = 11;
			// 
			// vScrollBar19
			// 
			this.vScrollBar19.Location = new System.Drawing.Point(169, 35);
			this.vScrollBar19.Maximum = 264;
			this.vScrollBar19.Name = "vScrollBar19";
			this.vScrollBar19.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar19.TabIndex = 0;
			this.vScrollBar19.Visible = false;
			this.vScrollBar19.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// numericUpDown27
			// 
			this.numericUpDown27.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown27.Location = new System.Drawing.Point(663, 265);
			this.numericUpDown27.Name = "numericUpDown27";
			this.numericUpDown27.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown27.TabIndex = 11;
			// 
			// vScrollBar20
			// 
			this.vScrollBar20.Location = new System.Drawing.Point(233, 35);
			this.vScrollBar20.Maximum = 264;
			this.vScrollBar20.Name = "vScrollBar20";
			this.vScrollBar20.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar20.TabIndex = 0;
			this.vScrollBar20.Visible = false;
			this.vScrollBar20.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// numericUpDown26
			// 
			this.numericUpDown26.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown26.Location = new System.Drawing.Point(599, 265);
			this.numericUpDown26.Name = "numericUpDown26";
			this.numericUpDown26.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown26.TabIndex = 11;
			// 
			// label30
			// 
			this.label30.Font = new System.Drawing.Font("宋体", 8F);
			this.label30.Location = new System.Drawing.Point(852, 35);
			this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(19, 128);
			this.label30.TabIndex = 10;
			this.label30.Text = "总调光1";
			this.label30.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label30.Visible = false;
			// 
			// numericUpDown25
			// 
			this.numericUpDown25.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown25.Location = new System.Drawing.Point(536, 265);
			this.numericUpDown25.Name = "numericUpDown25";
			this.numericUpDown25.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown25.TabIndex = 11;
			// 
			// vScrollBar21
			// 
			this.vScrollBar21.Location = new System.Drawing.Point(297, 35);
			this.vScrollBar21.Maximum = 264;
			this.vScrollBar21.Name = "vScrollBar21";
			this.vScrollBar21.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar21.TabIndex = 0;
			this.vScrollBar21.Visible = false;
			this.vScrollBar21.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// numericUpDown24
			// 
			this.numericUpDown24.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown24.Location = new System.Drawing.Point(471, 265);
			this.numericUpDown24.Name = "numericUpDown24";
			this.numericUpDown24.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown24.TabIndex = 11;
			// 
			// vScrollBar22
			// 
			this.vScrollBar22.Location = new System.Drawing.Point(361, 35);
			this.vScrollBar22.Maximum = 264;
			this.vScrollBar22.Name = "vScrollBar22";
			this.vScrollBar22.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar22.TabIndex = 0;
			this.vScrollBar22.Visible = false;
			this.vScrollBar22.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// numericUpDown23
			// 
			this.numericUpDown23.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown23.Location = new System.Drawing.Point(408, 265);
			this.numericUpDown23.Name = "numericUpDown23";
			this.numericUpDown23.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown23.TabIndex = 11;
			// 
			// label29
			// 
			this.label29.Font = new System.Drawing.Font("宋体", 8F);
			this.label29.Location = new System.Drawing.Point(788, 35);
			this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(19, 128);
			this.label29.TabIndex = 10;
			this.label29.Text = "总调光1";
			this.label29.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label29.Visible = false;
			// 
			// numericUpDown22
			// 
			this.numericUpDown22.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown22.Location = new System.Drawing.Point(344, 265);
			this.numericUpDown22.Name = "numericUpDown22";
			this.numericUpDown22.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown22.TabIndex = 11;
			// 
			// vScrollBar23
			// 
			this.vScrollBar23.Location = new System.Drawing.Point(425, 35);
			this.vScrollBar23.Maximum = 264;
			this.vScrollBar23.Name = "vScrollBar23";
			this.vScrollBar23.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar23.TabIndex = 0;
			this.vScrollBar23.Visible = false;
			this.vScrollBar23.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// numericUpDown21
			// 
			this.numericUpDown21.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown21.Location = new System.Drawing.Point(282, 265);
			this.numericUpDown21.Name = "numericUpDown21";
			this.numericUpDown21.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown21.TabIndex = 11;
			// 
			// vScrollBar24
			// 
			this.vScrollBar24.Location = new System.Drawing.Point(489, 35);
			this.vScrollBar24.Maximum = 264;
			this.vScrollBar24.Name = "vScrollBar24";
			this.vScrollBar24.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar24.TabIndex = 0;
			this.vScrollBar24.Visible = false;
			this.vScrollBar24.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// numericUpDown20
			// 
			this.numericUpDown20.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown20.Location = new System.Drawing.Point(215, 265);
			this.numericUpDown20.Name = "numericUpDown20";
			this.numericUpDown20.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown20.TabIndex = 11;
			// 
			// vScrollBar25
			// 
			this.vScrollBar25.Location = new System.Drawing.Point(553, 35);
			this.vScrollBar25.Maximum = 264;
			this.vScrollBar25.Name = "vScrollBar25";
			this.vScrollBar25.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar25.TabIndex = 0;
			this.vScrollBar25.Visible = false;
			this.vScrollBar25.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// numericUpDown19
			// 
			this.numericUpDown19.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown19.Location = new System.Drawing.Point(151, 265);
			this.numericUpDown19.Name = "numericUpDown19";
			this.numericUpDown19.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown19.TabIndex = 11;
			// 
			// label28
			// 
			this.label28.Font = new System.Drawing.Font("宋体", 8F);
			this.label28.Location = new System.Drawing.Point(724, 35);
			this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(19, 128);
			this.label28.TabIndex = 10;
			this.label28.Text = "总调光1";
			this.label28.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label28.Visible = false;
			// 
			// numericUpDown18
			// 
			this.numericUpDown18.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown18.Location = new System.Drawing.Point(87, 265);
			this.numericUpDown18.Name = "numericUpDown18";
			this.numericUpDown18.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown18.TabIndex = 11;
			// 
			// vScrollBar26
			// 
			this.vScrollBar26.Location = new System.Drawing.Point(617, 35);
			this.vScrollBar26.Maximum = 264;
			this.vScrollBar26.Name = "vScrollBar26";
			this.vScrollBar26.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar26.TabIndex = 0;
			this.vScrollBar26.Visible = false;
			this.vScrollBar26.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// numericUpDown17
			// 
			this.numericUpDown17.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown17.Location = new System.Drawing.Point(23, 265);
			this.numericUpDown17.Name = "numericUpDown17";
			this.numericUpDown17.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown17.TabIndex = 11;
			// 
			// vScrollBar27
			// 
			this.vScrollBar27.Location = new System.Drawing.Point(681, 35);
			this.vScrollBar27.Maximum = 264;
			this.vScrollBar27.Name = "vScrollBar27";
			this.vScrollBar27.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar27.TabIndex = 0;
			this.vScrollBar27.Visible = false;
			this.vScrollBar27.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar28
			// 
			this.vScrollBar28.Location = new System.Drawing.Point(745, 35);
			this.vScrollBar28.Maximum = 264;
			this.vScrollBar28.Name = "vScrollBar28";
			this.vScrollBar28.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar28.TabIndex = 0;
			this.vScrollBar28.Visible = false;
			this.vScrollBar28.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// label27
			// 
			this.label27.Font = new System.Drawing.Font("宋体", 8F);
			this.label27.Location = new System.Drawing.Point(660, 35);
			this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(19, 128);
			this.label27.TabIndex = 10;
			this.label27.Text = "总调光1";
			this.label27.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label27.Visible = false;
			// 
			// vScrollBar29
			// 
			this.vScrollBar29.Location = new System.Drawing.Point(809, 35);
			this.vScrollBar29.Maximum = 264;
			this.vScrollBar29.Name = "vScrollBar29";
			this.vScrollBar29.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar29.TabIndex = 0;
			this.vScrollBar29.Visible = false;
			this.vScrollBar29.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar30
			// 
			this.vScrollBar30.Location = new System.Drawing.Point(873, 35);
			this.vScrollBar30.Maximum = 264;
			this.vScrollBar30.Name = "vScrollBar30";
			this.vScrollBar30.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar30.TabIndex = 0;
			this.vScrollBar30.Visible = false;
			this.vScrollBar30.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar31
			// 
			this.vScrollBar31.Location = new System.Drawing.Point(937, 35);
			this.vScrollBar31.Maximum = 264;
			this.vScrollBar31.Name = "vScrollBar31";
			this.vScrollBar31.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar31.TabIndex = 0;
			this.vScrollBar31.Visible = false;
			this.vScrollBar31.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("宋体", 8F);
			this.label26.Location = new System.Drawing.Point(595, 35);
			this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(19, 128);
			this.label26.TabIndex = 10;
			this.label26.Text = "总调光1";
			this.label26.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label26.Visible = false;
			// 
			// vScrollBar32
			// 
			this.vScrollBar32.Location = new System.Drawing.Point(1001, 35);
			this.vScrollBar32.Maximum = 264;
			this.vScrollBar32.Name = "vScrollBar32";
			this.vScrollBar32.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar32.TabIndex = 0;
			this.vScrollBar32.Visible = false;
			this.vScrollBar32.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// label17
			// 
			this.label17.Font = new System.Drawing.Font("宋体", 8F);
			this.label17.Location = new System.Drawing.Point(19, 35);
			this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(19, 128);
			this.label17.TabIndex = 10;
			this.label17.Text = "总调光1";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label17.Visible = false;
			// 
			// label22
			// 
			this.label22.Font = new System.Drawing.Font("宋体", 8F);
			this.label22.Location = new System.Drawing.Point(340, 35);
			this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(19, 128);
			this.label22.TabIndex = 10;
			this.label22.Text = "总调光1";
			this.label22.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label22.Visible = false;
			// 
			// label25
			// 
			this.label25.Font = new System.Drawing.Font("宋体", 8F);
			this.label25.Location = new System.Drawing.Point(532, 35);
			this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(19, 128);
			this.label25.TabIndex = 10;
			this.label25.Text = "总调光1";
			this.label25.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label25.Visible = false;
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("宋体", 8F);
			this.label20.Location = new System.Drawing.Point(212, 35);
			this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(19, 128);
			this.label20.TabIndex = 10;
			this.label20.Text = "总调光1";
			this.label20.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label20.Visible = false;
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("宋体", 8F);
			this.label19.Location = new System.Drawing.Point(148, 35);
			this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(19, 128);
			this.label19.TabIndex = 10;
			this.label19.Text = "总调光1";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label19.Visible = false;
			// 
			// label21
			// 
			this.label21.Font = new System.Drawing.Font("宋体", 8F);
			this.label21.Location = new System.Drawing.Point(273, 35);
			this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(19, 128);
			this.label21.TabIndex = 10;
			this.label21.Text = "总调光1";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label21.Visible = false;
			// 
			// label23
			// 
			this.label23.Font = new System.Drawing.Font("宋体", 8F);
			this.label23.Location = new System.Drawing.Point(404, 35);
			this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(19, 128);
			this.label23.TabIndex = 10;
			this.label23.Text = "总调光1";
			this.label23.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label23.Visible = false;
			// 
			// label24
			// 
			this.label24.Font = new System.Drawing.Font("宋体", 8F);
			this.label24.Location = new System.Drawing.Point(468, 35);
			this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(19, 128);
			this.label24.TabIndex = 10;
			this.label24.Text = "总调光1";
			this.label24.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label24.Visible = false;
			// 
			// label18
			// 
			this.label18.Font = new System.Drawing.Font("宋体", 8F);
			this.label18.Location = new System.Drawing.Point(84, 35);
			this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(19, 128);
			this.label18.TabIndex = 10;
			this.label18.Text = "总调光1";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label18.Visible = false;
			// 
			// tongdaoGroupBox1
			// 
			this.tongdaoGroupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
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
			this.tongdaoGroupBox1.Location = new System.Drawing.Point(7, 63);
			this.tongdaoGroupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tongdaoGroupBox1.Name = "tongdaoGroupBox1";
			this.tongdaoGroupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tongdaoGroupBox1.Size = new System.Drawing.Size(1061, 330);
			this.tongdaoGroupBox1.TabIndex = 9;
			this.tongdaoGroupBox1.TabStop = false;
			// 
			// numericUpDown16
			// 
			this.numericUpDown16.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown16.Location = new System.Drawing.Point(982, 268);
			this.numericUpDown16.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown16.Name = "numericUpDown16";
			this.numericUpDown16.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown16.TabIndex = 11;
			// 
			// numericUpDown15
			// 
			this.numericUpDown15.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown15.Location = new System.Drawing.Point(918, 268);
			this.numericUpDown15.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown15.Name = "numericUpDown15";
			this.numericUpDown15.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown15.TabIndex = 11;
			// 
			// numericUpDown14
			// 
			this.numericUpDown14.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown14.Location = new System.Drawing.Point(854, 268);
			this.numericUpDown14.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown14.Name = "numericUpDown14";
			this.numericUpDown14.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown14.TabIndex = 11;
			// 
			// numericUpDown13
			// 
			this.numericUpDown13.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown13.Location = new System.Drawing.Point(791, 268);
			this.numericUpDown13.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown13.Name = "numericUpDown13";
			this.numericUpDown13.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown13.TabIndex = 11;
			// 
			// numericUpDown12
			// 
			this.numericUpDown12.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown12.Location = new System.Drawing.Point(727, 268);
			this.numericUpDown12.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown12.Name = "numericUpDown12";
			this.numericUpDown12.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown12.TabIndex = 11;
			// 
			// numericUpDown11
			// 
			this.numericUpDown11.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown11.Location = new System.Drawing.Point(662, 268);
			this.numericUpDown11.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown11.Name = "numericUpDown11";
			this.numericUpDown11.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown11.TabIndex = 11;
			// 
			// numericUpDown10
			// 
			this.numericUpDown10.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown10.Location = new System.Drawing.Point(598, 268);
			this.numericUpDown10.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown10.Name = "numericUpDown10";
			this.numericUpDown10.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown10.TabIndex = 11;
			// 
			// numericUpDown9
			// 
			this.numericUpDown9.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown9.Location = new System.Drawing.Point(535, 268);
			this.numericUpDown9.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown9.Name = "numericUpDown9";
			this.numericUpDown9.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown9.TabIndex = 11;
			// 
			// numericUpDown8
			// 
			this.numericUpDown8.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown8.Location = new System.Drawing.Point(470, 268);
			this.numericUpDown8.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown8.Name = "numericUpDown8";
			this.numericUpDown8.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown8.TabIndex = 11;
			// 
			// numericUpDown7
			// 
			this.numericUpDown7.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown7.Location = new System.Drawing.Point(407, 268);
			this.numericUpDown7.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown7.Name = "numericUpDown7";
			this.numericUpDown7.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown7.TabIndex = 11;
			// 
			// numericUpDown6
			// 
			this.numericUpDown6.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown6.Location = new System.Drawing.Point(343, 268);
			this.numericUpDown6.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown6.Name = "numericUpDown6";
			this.numericUpDown6.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown6.TabIndex = 11;
			// 
			// numericUpDown5
			// 
			this.numericUpDown5.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown5.Location = new System.Drawing.Point(281, 268);
			this.numericUpDown5.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown5.Name = "numericUpDown5";
			this.numericUpDown5.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown5.TabIndex = 11;
			// 
			// numericUpDown4
			// 
			this.numericUpDown4.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown4.Location = new System.Drawing.Point(214, 268);
			this.numericUpDown4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown4.TabIndex = 11;
			// 
			// numericUpDown3
			// 
			this.numericUpDown3.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown3.Location = new System.Drawing.Point(150, 268);
			this.numericUpDown3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown3.TabIndex = 11;
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown2.Location = new System.Drawing.Point(86, 268);
			this.numericUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown2.TabIndex = 11;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown1.Location = new System.Drawing.Point(22, 268);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown1.TabIndex = 11;
			// 
			// label16
			// 
			this.label16.Font = new System.Drawing.Font("宋体", 8F);
			this.label16.Location = new System.Drawing.Point(979, 38);
			this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(19, 128);
			this.label16.TabIndex = 10;
			this.label16.Text = "总调光1";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label16.Visible = false;
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("宋体", 8F);
			this.label15.Location = new System.Drawing.Point(915, 38);
			this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(19, 128);
			this.label15.TabIndex = 10;
			this.label15.Text = "总调光1";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label15.Visible = false;
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("宋体", 8F);
			this.label14.Location = new System.Drawing.Point(851, 38);
			this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(19, 128);
			this.label14.TabIndex = 10;
			this.label14.Text = "总调光1";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label14.Visible = false;
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("宋体", 8F);
			this.label13.Location = new System.Drawing.Point(787, 38);
			this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(19, 128);
			this.label13.TabIndex = 10;
			this.label13.Text = "总调光1";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label13.Visible = false;
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("宋体", 8F);
			this.label12.Location = new System.Drawing.Point(723, 38);
			this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(19, 128);
			this.label12.TabIndex = 10;
			this.label12.Text = "总调光1";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label12.Visible = false;
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("宋体", 8F);
			this.label11.Location = new System.Drawing.Point(659, 38);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(19, 128);
			this.label11.TabIndex = 10;
			this.label11.Text = "总调光1";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label11.Visible = false;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("宋体", 8F);
			this.label10.Location = new System.Drawing.Point(595, 38);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(19, 128);
			this.label10.TabIndex = 10;
			this.label10.Text = "总调光1";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label10.Visible = false;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("宋体", 8F);
			this.label9.Location = new System.Drawing.Point(531, 38);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(19, 128);
			this.label9.TabIndex = 10;
			this.label9.Text = "总调光1";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label9.Visible = false;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("宋体", 8F);
			this.label8.Location = new System.Drawing.Point(467, 38);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(19, 128);
			this.label8.TabIndex = 10;
			this.label8.Text = "总调光1";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label8.Visible = false;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("宋体", 8F);
			this.label7.Location = new System.Drawing.Point(403, 38);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(19, 128);
			this.label7.TabIndex = 10;
			this.label7.Text = "总调光1";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label7.Visible = false;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("宋体", 8F);
			this.label6.Location = new System.Drawing.Point(339, 38);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(19, 128);
			this.label6.TabIndex = 10;
			this.label6.Text = "总调光1";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label6.Visible = false;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("宋体", 8F);
			this.label5.Location = new System.Drawing.Point(272, 38);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(19, 128);
			this.label5.TabIndex = 10;
			this.label5.Text = "总调光1";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label5.Visible = false;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("宋体", 8F);
			this.label4.Location = new System.Drawing.Point(211, 38);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(19, 128);
			this.label4.TabIndex = 10;
			this.label4.Text = "总调光1";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label4.Visible = false;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 8F);
			this.label3.Location = new System.Drawing.Point(147, 38);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(19, 128);
			this.label3.TabIndex = 10;
			this.label3.Text = "总调光1";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label3.Visible = false;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 8F);
			this.label2.Location = new System.Drawing.Point(83, 38);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(19, 128);
			this.label2.TabIndex = 10;
			this.label2.Text = "总调光1";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label2.Visible = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 8F);
			this.label1.Location = new System.Drawing.Point(19, 38);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(19, 128);
			this.label1.TabIndex = 10;
			this.label1.Text = "总调光1";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label1.Visible = false;
			// 
			// vScrollBar16
			// 
			this.vScrollBar16.Location = new System.Drawing.Point(1001, 38);
			this.vScrollBar16.Maximum = 264;
			this.vScrollBar16.Name = "vScrollBar16";
			this.vScrollBar16.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar16.TabIndex = 0;
			this.vScrollBar16.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar12
			// 
			this.vScrollBar12.Location = new System.Drawing.Point(745, 38);
			this.vScrollBar12.Maximum = 264;
			this.vScrollBar12.Name = "vScrollBar12";
			this.vScrollBar12.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar12.TabIndex = 0;
			this.vScrollBar12.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar8
			// 
			this.vScrollBar8.Location = new System.Drawing.Point(489, 38);
			this.vScrollBar8.Maximum = 264;
			this.vScrollBar8.Name = "vScrollBar8";
			this.vScrollBar8.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar8.TabIndex = 0;
			this.vScrollBar8.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar4
			// 
			this.vScrollBar4.Location = new System.Drawing.Point(233, 38);
			this.vScrollBar4.Maximum = 264;
			this.vScrollBar4.Name = "vScrollBar4";
			this.vScrollBar4.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar4.TabIndex = 0;
			this.vScrollBar4.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar15
			// 
			this.vScrollBar15.Location = new System.Drawing.Point(937, 38);
			this.vScrollBar15.Maximum = 264;
			this.vScrollBar15.Name = "vScrollBar15";
			this.vScrollBar15.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar15.TabIndex = 0;
			this.vScrollBar15.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar11
			// 
			this.vScrollBar11.Location = new System.Drawing.Point(681, 38);
			this.vScrollBar11.Maximum = 264;
			this.vScrollBar11.Name = "vScrollBar11";
			this.vScrollBar11.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar11.TabIndex = 0;
			this.vScrollBar11.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar14
			// 
			this.vScrollBar14.Location = new System.Drawing.Point(873, 38);
			this.vScrollBar14.Maximum = 264;
			this.vScrollBar14.Name = "vScrollBar14";
			this.vScrollBar14.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar14.TabIndex = 0;
			this.vScrollBar14.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar10
			// 
			this.vScrollBar10.Location = new System.Drawing.Point(617, 38);
			this.vScrollBar10.Maximum = 264;
			this.vScrollBar10.Name = "vScrollBar10";
			this.vScrollBar10.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar10.TabIndex = 0;
			this.vScrollBar10.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar7
			// 
			this.vScrollBar7.Location = new System.Drawing.Point(425, 38);
			this.vScrollBar7.Maximum = 264;
			this.vScrollBar7.Name = "vScrollBar7";
			this.vScrollBar7.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar7.TabIndex = 0;
			this.vScrollBar7.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar13
			// 
			this.vScrollBar13.Location = new System.Drawing.Point(809, 38);
			this.vScrollBar13.Maximum = 264;
			this.vScrollBar13.Name = "vScrollBar13";
			this.vScrollBar13.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar13.TabIndex = 0;
			this.vScrollBar13.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar6
			// 
			this.vScrollBar6.Location = new System.Drawing.Point(361, 38);
			this.vScrollBar6.Maximum = 264;
			this.vScrollBar6.Name = "vScrollBar6";
			this.vScrollBar6.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar6.TabIndex = 0;
			this.vScrollBar6.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar9
			// 
			this.vScrollBar9.Location = new System.Drawing.Point(553, 38);
			this.vScrollBar9.Maximum = 264;
			this.vScrollBar9.Name = "vScrollBar9";
			this.vScrollBar9.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar9.TabIndex = 0;
			this.vScrollBar9.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar3
			// 
			this.vScrollBar3.Location = new System.Drawing.Point(169, 38);
			this.vScrollBar3.Maximum = 264;
			this.vScrollBar3.Name = "vScrollBar3";
			this.vScrollBar3.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar3.TabIndex = 0;
			this.vScrollBar3.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar5
			// 
			this.vScrollBar5.Location = new System.Drawing.Point(297, 38);
			this.vScrollBar5.Maximum = 264;
			this.vScrollBar5.Name = "vScrollBar5";
			this.vScrollBar5.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar5.TabIndex = 0;
			this.vScrollBar5.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar2
			// 
			this.vScrollBar2.Location = new System.Drawing.Point(105, 38);
			this.vScrollBar2.Maximum = 264;
			this.vScrollBar2.Name = "vScrollBar2";
			this.vScrollBar2.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar2.TabIndex = 0;
			this.vScrollBar2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Location = new System.Drawing.Point(41, 38);
			this.vScrollBar1.Maximum = 264;
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar1.TabIndex = 0;
			this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
			// 
			// groupComboBox
			// 
			this.groupComboBox.FormattingEnabled = true;
			this.groupComboBox.Location = new System.Drawing.Point(555, 24);
			this.groupComboBox.Name = "groupComboBox";
			this.groupComboBox.Size = new System.Drawing.Size(121, 23);
			this.groupComboBox.TabIndex = 0;
			this.groupComboBox.Visible = false;
			// 
			// frameComboBox
			// 
			this.frameComboBox.FormattingEnabled = true;
			this.frameComboBox.Items.AddRange(new object[] {
            "标准",
            "动感",
            "商务",
            "抒情",
            "清洁",
            "柔和",
            "激情",
            "明亮",
            "浪漫",
            "演出",
            "暂停",
            "全关",
            "全开",
            "全开关",
            "电影",
            "备用1",
            "备用2",
            "备用3",
            "备用4",
            "备用5",
            "备用6",
            "摇麦",
            "喝彩",
            "倒彩"});
			this.frameComboBox.Location = new System.Drawing.Point(350, 24);
			this.frameComboBox.Name = "frameComboBox";
			this.frameComboBox.Size = new System.Drawing.Size(89, 23);
			this.frameComboBox.TabIndex = 0;
			this.frameComboBox.SelectedIndexChanged += new System.EventHandler(this.frameComboBox_SelectedIndexChanged);
			// 
			// modeComboBox
			// 
			this.modeComboBox.FormattingEnabled = true;
			this.modeComboBox.Items.AddRange(new object[] {
            "常规模式",
            "声控模式"});
			this.modeComboBox.Location = new System.Drawing.Point(445, 24);
			this.modeComboBox.Name = "modeComboBox";
			this.modeComboBox.Size = new System.Drawing.Size(104, 23);
			this.modeComboBox.TabIndex = 0;
			this.modeComboBox.SelectedIndexChanged += new System.EventHandler(this.modeComboBox_SelectedIndexChanged);
			// 
			// stepLabel
			// 
			this.stepLabel.AutoSize = true;
			this.stepLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.stepLabel.Location = new System.Drawing.Point(1227, 125);
			this.stepLabel.Name = "stepLabel";
			this.stepLabel.Size = new System.Drawing.Size(39, 20);
			this.stepLabel.TabIndex = 15;
			this.stepLabel.Text = "0/0";
			this.stepLabel.Click += new System.EventHandler(this.stepLabel_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1453, 961);
			this.Controls.Add(this.tongdaoGroupBox);
			this.Controls.Add(this.lightsListView);
			this.Controls.Add(this.exitButton);
			this.Controls.Add(this.oneKeyButton);
			this.Controls.Add(this.saveAsButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.globleSetButton);
			this.Controls.Add(this.openFileButton);
			this.Controls.Add(this.lightEditButton);
			this.Controls.Add(this.newFileButton);
			this.Controls.Add(this.openComButton);
			this.Controls.Add(this.comComboBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "智控配置";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tongdaoGroupBox.ResumeLayout(false);
			this.tongdaoGroupBox.PerformLayout();
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
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox comComboBox;
		private System.Windows.Forms.Button openComButton;
		private System.Windows.Forms.Button newFileButton;
		private System.Windows.Forms.Button openFileButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button saveAsButton;
		private System.Windows.Forms.Button exitButton;
		private System.Windows.Forms.Button lightEditButton;
		private System.Windows.Forms.Button globleSetButton;
		private System.Windows.Forms.Button oneKeyButton;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private Sunisoft.IrisSkin.SkinEngine skinEngine1;

		// 2019.5.23 : 添加listView 来存放加载的灯具列表
		private ListView lightsListView;
		public ListViewItem[] lightItems;
		private ColumnHeader lightType;
		private ImageList LargeImageList;
		private GroupBox tongdaoGroupBox;
		private ComboBox groupComboBox;
		private ComboBox frameComboBox;
		private ComboBox modeComboBox;
		private GroupBox tongdaoGroupBox1;

		// 通道区
		public Label label16;
		public Label label15;
		public Label label14;
		public Label label13;
		public Label label12;
		public Label label11;
		public Label label10;
		public Label label9;
		public Label label8;
		public Label label7;
		public Label label6;
		public Label label5;
		public Label label4;
		public Label label3;
		public Label label2;
		public Label label1;
		public VScrollBar vScrollBar16;
		public VScrollBar vScrollBar12;
		public VScrollBar vScrollBar8;
		public VScrollBar vScrollBar4;
		public VScrollBar vScrollBar15;
		public VScrollBar vScrollBar11;
		public VScrollBar vScrollBar14;
		public VScrollBar vScrollBar10;
		public VScrollBar vScrollBar7;
		public VScrollBar vScrollBar13;
		public VScrollBar vScrollBar6;
		public VScrollBar vScrollBar9;
		public VScrollBar vScrollBar3;
		public VScrollBar vScrollBar5;
		public VScrollBar vScrollBar2;
		public VScrollBar vScrollBar1;
		private GroupBox tongdaoGroupBox2;
		public Label label32;
		public VScrollBar vScrollBar17;
		public Label label31;
		public VScrollBar vScrollBar18;
		public VScrollBar vScrollBar19;
		public VScrollBar vScrollBar20;
		public Label label30;
		public VScrollBar vScrollBar21;
		public VScrollBar vScrollBar22;
		public Label label29;
		public VScrollBar vScrollBar23;
		public VScrollBar vScrollBar24;
		public VScrollBar vScrollBar25;
		public Label label28;
		public VScrollBar vScrollBar26;
		public VScrollBar vScrollBar27;
		public VScrollBar vScrollBar28;
		public Label label27;
		public VScrollBar vScrollBar29;
		public VScrollBar vScrollBar30;
		public VScrollBar vScrollBar31;
		public Label label26;
		public VScrollBar vScrollBar32;
		public Label label17;
		public Label label22;
		public Label label25;
		public Label label20;
		public Label label19;
		public Label label21;
		public Label label23;
		public Label label24;
		public Label label18;

		private NumericUpDown numericUpDown32;
		private NumericUpDown numericUpDown31;
		private NumericUpDown numericUpDown30;
		private NumericUpDown numericUpDown29;
		private NumericUpDown numericUpDown28;
		private NumericUpDown numericUpDown27;
		private NumericUpDown numericUpDown26;
		private NumericUpDown numericUpDown25;
		private NumericUpDown numericUpDown24;
		private NumericUpDown numericUpDown23;
		private NumericUpDown numericUpDown22;
		private NumericUpDown numericUpDown21;
		private NumericUpDown numericUpDown20;
		private NumericUpDown numericUpDown19;
		private NumericUpDown numericUpDown18;
		private NumericUpDown numericUpDown17;
		private NumericUpDown numericUpDown16;
		private NumericUpDown numericUpDown15;
		private NumericUpDown numericUpDown14;
		private NumericUpDown numericUpDown13;
		private NumericUpDown numericUpDown12;
		private NumericUpDown numericUpDown11;
		private NumericUpDown numericUpDown10;
		private NumericUpDown numericUpDown9;
		private NumericUpDown numericUpDown8;
		private NumericUpDown numericUpDown7;
		private NumericUpDown numericUpDown6;
		private NumericUpDown numericUpDown5;
		private NumericUpDown numericUpDown4;
		private NumericUpDown numericUpDown3;
		private NumericUpDown numericUpDown2;
		private NumericUpDown numericUpDown1;

		// 步数调节区
		private Label lightValueLabel;
		private Label lightLabel;
		private Button insertStepButton;
		private Button newStepButton;
		private Button deleteStepButton;
		private Button nextStepButton;
		private Button backStepButton;


		public Label[] labels = new Label[32];
		public VScrollBar[] vScrollBars = new VScrollBar[32];
		public NumericUpDown[] valueNumericUpDowns = new NumericUpDown[32];

		// 一个记录所有Step的数组
		public StepWrapper[,] stepList = new StepWrapper[24,2];
		private Label stepLabel;
	}
}

