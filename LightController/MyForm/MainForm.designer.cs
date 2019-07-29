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
			this.oneKeyButton = new System.Windows.Forms.Button();
			this.projectSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
			this.lightsListView = new System.Windows.Forms.ListView();
			this.lightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LargeImageList = new System.Windows.Forms.ImageList(this.components);
			this.tongdaoGroupBox = new System.Windows.Forms.GroupBox();
			this.addStepCheckBox = new System.Windows.Forms.CheckBox();
			this.materialUseButton = new System.Windows.Forms.Button();
			this.pasteLightButton = new System.Windows.Forms.Button();
			this.materialSaveButton = new System.Windows.Forms.Button();
			this.copyLightButton = new System.Windows.Forms.Button();
			this.tongdaoPanel = new System.Windows.Forms.Panel();
			this.tongdaoGroupBox1 = new System.Windows.Forms.GroupBox();
			this.changeModeLabel = new System.Windows.Forms.Label();
			this.tongdaoValueLabel = new System.Windows.Forms.Label();
			this.stepTimeLabel = new System.Windows.Forms.Label();
			this.changeModeComboBox16 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox15 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox14 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox13 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox12 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox11 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox10 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox9 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox8 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox7 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox6 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox5 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox4 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox3 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox2 = new System.Windows.Forms.ComboBox();
			this.changeModeComboBox1 = new System.Windows.Forms.ComboBox();
			this.numericUpDown48 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown16 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown47 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown15 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown46 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown14 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown45 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown13 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown44 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown12 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown43 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown11 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown42 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown10 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown41 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown9 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown40 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown39 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown38 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown37 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown36 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown35 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown34 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown33 = new System.Windows.Forms.NumericUpDown();
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
			this.tongdaoGroupBox2 = new System.Windows.Forms.GroupBox();
			this.changeModeLabel2 = new System.Windows.Forms.Label();
			this.numericUpDown32 = new System.Windows.Forms.NumericUpDown();
			this.tongdaoValueLabel2 = new System.Windows.Forms.Label();
			this.changeModeComboBox32 = new System.Windows.Forms.ComboBox();
			this.stepTimeLabel2 = new System.Windows.Forms.Label();
			this.changeModeComboBox31 = new System.Windows.Forms.ComboBox();
			this.numericUpDown64 = new System.Windows.Forms.NumericUpDown();
			this.changeModeComboBox30 = new System.Windows.Forms.ComboBox();
			this.label32 = new System.Windows.Forms.Label();
			this.changeModeComboBox29 = new System.Windows.Forms.ComboBox();
			this.numericUpDown31 = new System.Windows.Forms.NumericUpDown();
			this.changeModeComboBox28 = new System.Windows.Forms.ComboBox();
			this.numericUpDown63 = new System.Windows.Forms.NumericUpDown();
			this.changeModeComboBox27 = new System.Windows.Forms.ComboBox();
			this.vScrollBar17 = new System.Windows.Forms.VScrollBar();
			this.changeModeComboBox26 = new System.Windows.Forms.ComboBox();
			this.numericUpDown30 = new System.Windows.Forms.NumericUpDown();
			this.changeModeComboBox25 = new System.Windows.Forms.ComboBox();
			this.numericUpDown62 = new System.Windows.Forms.NumericUpDown();
			this.changeModeComboBox24 = new System.Windows.Forms.ComboBox();
			this.label31 = new System.Windows.Forms.Label();
			this.changeModeComboBox23 = new System.Windows.Forms.ComboBox();
			this.numericUpDown29 = new System.Windows.Forms.NumericUpDown();
			this.changeModeComboBox22 = new System.Windows.Forms.ComboBox();
			this.numericUpDown61 = new System.Windows.Forms.NumericUpDown();
			this.changeModeComboBox21 = new System.Windows.Forms.ComboBox();
			this.vScrollBar18 = new System.Windows.Forms.VScrollBar();
			this.changeModeComboBox20 = new System.Windows.Forms.ComboBox();
			this.numericUpDown28 = new System.Windows.Forms.NumericUpDown();
			this.changeModeComboBox19 = new System.Windows.Forms.ComboBox();
			this.numericUpDown60 = new System.Windows.Forms.NumericUpDown();
			this.changeModeComboBox18 = new System.Windows.Forms.ComboBox();
			this.vScrollBar19 = new System.Windows.Forms.VScrollBar();
			this.changeModeComboBox17 = new System.Windows.Forms.ComboBox();
			this.numericUpDown27 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown59 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar20 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown26 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown58 = new System.Windows.Forms.NumericUpDown();
			this.label30 = new System.Windows.Forms.Label();
			this.numericUpDown25 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown57 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar21 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown24 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown56 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar22 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown23 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown55 = new System.Windows.Forms.NumericUpDown();
			this.label29 = new System.Windows.Forms.Label();
			this.numericUpDown22 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown54 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar23 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown21 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown53 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar24 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown20 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown52 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar25 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown19 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown51 = new System.Windows.Forms.NumericUpDown();
			this.label28 = new System.Windows.Forms.Label();
			this.numericUpDown18 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown50 = new System.Windows.Forms.NumericUpDown();
			this.vScrollBar26 = new System.Windows.Forms.VScrollBar();
			this.numericUpDown17 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown49 = new System.Windows.Forms.NumericUpDown();
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
			this.cmComboBox = new System.Windows.Forms.ComboBox();
			this.commonValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.stNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.changeModeButton = new System.Windows.Forms.Button();
			this.commonValueButton = new System.Windows.Forms.Button();
			this.steptimeSetButton = new System.Windows.Forms.Button();
			this.modeChooseLabel = new System.Windows.Forms.Label();
			this.frameChooseLabel = new System.Windows.Forms.Label();
			this.stepLabel = new System.Windows.Forms.Label();
			this.nextStepButton = new System.Windows.Forms.Button();
			this.deleteStepButton = new System.Windows.Forms.Button();
			this.backStepButton = new System.Windows.Forms.Button();
			this.insertBeforeStepButton = new System.Windows.Forms.Button();
			this.insertAfterStepButton = new System.Windows.Forms.Button();
			this.initButton = new System.Windows.Forms.Button();
			this.zeroButton = new System.Windows.Forms.Button();
			this.pasteStepButton = new System.Windows.Forms.Button();
			this.copyStepButton = new System.Windows.Forms.Button();
			this.addStepButton = new System.Windows.Forms.Button();
			this.lightValueLabel = new System.Windows.Forms.Label();
			this.lightLabel = new System.Windows.Forms.Label();
			this.frameComboBox = new System.Windows.Forms.ComboBox();
			this.modeComboBox = new System.Windows.Forms.ComboBox();
			this.test1Button = new System.Windows.Forms.Button();
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.lightLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hardwareSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hardwareSetNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hardwareSetOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lightsEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.globalSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ymSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.networkSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.previewButton = new System.Windows.Forms.Button();
			this.stopReviewButton = new System.Windows.Forms.Button();
			this.oneLightStepButton = new System.Windows.Forms.Button();
			this.realTimeCheckBox = new System.Windows.Forms.CheckBox();
			this.soundButton = new System.Windows.Forms.Button();
			this.connectButton = new System.Windows.Forms.Button();
			this.testGroupBox = new System.Windows.Forms.GroupBox();
			this.test4Button = new System.Windows.Forms.Button();
			this.test3Button = new System.Windows.Forms.Button();
			this.test2Button = new System.Windows.Forms.Button();
			this.tongdaoGroupBox.SuspendLayout();
			this.tongdaoPanel.SuspendLayout();
			this.tongdaoGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown48)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown16)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown47)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown15)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown46)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown14)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown45)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown13)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown44)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown43)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown42)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown41)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown40)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown39)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown38)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown37)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown36)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown35)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown34)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown33)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.tongdaoGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown32)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown64)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown31)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown63)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown30)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown62)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown29)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown61)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown28)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown60)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown27)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown59)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown26)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown58)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown25)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown57)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown24)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown56)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown23)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown55)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown22)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown54)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown21)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown53)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown20)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown52)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown19)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown51)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown18)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown50)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown17)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown49)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.commonValueNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.stNumericUpDown)).BeginInit();
			this.mainMenuStrip.SuspendLayout();
			this.testGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// comComboBox
			// 
			this.comComboBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.comComboBox.FormattingEnabled = true;
			this.comComboBox.Location = new System.Drawing.Point(18, 43);
			this.comComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.comComboBox.Name = "comComboBox";
			this.comComboBox.Size = new System.Drawing.Size(119, 28);
			this.comComboBox.TabIndex = 0;
			this.comComboBox.SelectedIndexChanged += new System.EventHandler(this.comComboBox_SelectedIndexChanged);
			// 
			// openComButton
			// 
			this.openComButton.Enabled = false;
			this.openComButton.Location = new System.Drawing.Point(18, 83);
			this.openComButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.openComButton.Name = "openComButton";
			this.openComButton.Size = new System.Drawing.Size(119, 32);
			this.openComButton.TabIndex = 1;
			this.openComButton.Text = "打开串口";
			this.openComButton.UseVisualStyleBackColor = true;
			this.openComButton.Click += new System.EventHandler(this.openCOMbutton_Click);
			// 
			// newFileButton
			// 
			this.newFileButton.Enabled = false;
			this.newFileButton.Location = new System.Drawing.Point(553, 57);
			this.newFileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.newFileButton.Name = "newFileButton";
			this.newFileButton.Size = new System.Drawing.Size(92, 65);
			this.newFileButton.TabIndex = 2;
			this.newFileButton.Text = "新建工程";
			this.newFileButton.UseVisualStyleBackColor = true;
			this.newFileButton.Click += new System.EventHandler(this.newButton_Click);
			// 
			// openFileButton
			// 
			this.openFileButton.Enabled = false;
			this.openFileButton.Location = new System.Drawing.Point(659, 57);
			this.openFileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.openFileButton.Name = "openFileButton";
			this.openFileButton.Size = new System.Drawing.Size(92, 65);
			this.openFileButton.TabIndex = 3;
			this.openFileButton.Text = "打开工程";
			this.openFileButton.UseVisualStyleBackColor = true;
			this.openFileButton.Click += new System.EventHandler(this.openButton_Click);
			// 
			// saveButton
			// 
			this.saveButton.Enabled = false;
			this.saveButton.Location = new System.Drawing.Point(765, 57);
			this.saveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(92, 65);
			this.saveButton.TabIndex = 4;
			this.saveButton.Text = "保存工程";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// saveAsButton
			// 
			this.saveAsButton.Enabled = false;
			this.saveAsButton.Location = new System.Drawing.Point(871, 57);
			this.saveAsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.saveAsButton.Name = "saveAsButton";
			this.saveAsButton.Size = new System.Drawing.Size(92, 65);
			this.saveAsButton.TabIndex = 5;
			this.saveAsButton.Text = "另存工程";
			this.saveAsButton.UseVisualStyleBackColor = true;
			this.saveAsButton.Visible = false;
			// 
			// oneKeyButton
			// 
			this.oneKeyButton.Enabled = false;
			this.oneKeyButton.Location = new System.Drawing.Point(1811, 57);
			this.oneKeyButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.oneKeyButton.Name = "oneKeyButton";
			this.oneKeyButton.Size = new System.Drawing.Size(92, 65);
			this.oneKeyButton.TabIndex = 4;
			this.oneKeyButton.Text = "一键配置";
			this.oneKeyButton.UseVisualStyleBackColor = true;
			this.oneKeyButton.Visible = false;
			this.oneKeyButton.Click += new System.EventHandler(this.oneKeyButton_Click);
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
			this.lightsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lightsListView.BackColor = System.Drawing.Color.Snow;
			this.lightsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lightType});
			this.lightsListView.LargeImageList = this.LargeImageList;
			this.lightsListView.Location = new System.Drawing.Point(0, 140);
			this.lightsListView.MultiSelect = false;
			this.lightsListView.Name = "lightsListView";
			this.lightsListView.Size = new System.Drawing.Size(1915, 214);
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
			this.LargeImageList.Images.SetKeyName(13, "1.bmp");
			this.LargeImageList.Images.SetKeyName(14, "2.bmp");
			this.LargeImageList.Images.SetKeyName(15, "3.bmp");
			this.LargeImageList.Images.SetKeyName(16, "4.bmp");
			this.LargeImageList.Images.SetKeyName(17, "5.bmp");
			this.LargeImageList.Images.SetKeyName(18, "6.bmp");
			this.LargeImageList.Images.SetKeyName(19, "7.bmp");
			this.LargeImageList.Images.SetKeyName(20, "8.bmp");
			this.LargeImageList.Images.SetKeyName(21, "9.bmp");
			this.LargeImageList.Images.SetKeyName(22, "10.bmp");
			this.LargeImageList.Images.SetKeyName(23, "11.bmp");
			this.LargeImageList.Images.SetKeyName(24, "12.bmp");
			this.LargeImageList.Images.SetKeyName(25, "13.bmp");
			this.LargeImageList.Images.SetKeyName(26, "14.bmp");
			this.LargeImageList.Images.SetKeyName(27, "15.bmp");
			this.LargeImageList.Images.SetKeyName(28, "16.bmp");
			this.LargeImageList.Images.SetKeyName(29, "17.bmp");
			this.LargeImageList.Images.SetKeyName(30, "18.bmp");
			this.LargeImageList.Images.SetKeyName(31, "19.bmp");
			this.LargeImageList.Images.SetKeyName(32, "20.bmp");
			this.LargeImageList.Images.SetKeyName(33, "21.bmp");
			this.LargeImageList.Images.SetKeyName(34, "22.bmp");
			this.LargeImageList.Images.SetKeyName(35, "23.bmp");
			this.LargeImageList.Images.SetKeyName(36, "24.bmp");
			this.LargeImageList.Images.SetKeyName(37, "25.bmp");
			this.LargeImageList.Images.SetKeyName(38, "27.bmp");
			this.LargeImageList.Images.SetKeyName(39, "28.bmp");
			this.LargeImageList.Images.SetKeyName(40, "29.gif");
			this.LargeImageList.Images.SetKeyName(41, "30.bmp");
			this.LargeImageList.Images.SetKeyName(42, "31.bmp");
			this.LargeImageList.Images.SetKeyName(43, "ledpar.bmp");
			this.LargeImageList.Images.SetKeyName(44, "灯带.bmp");
			this.LargeImageList.Images.SetKeyName(45, "二合一.bmp");
			this.LargeImageList.Images.SetKeyName(46, "二合一50.bmp");
			this.LargeImageList.Images.SetKeyName(47, "魔球.bmp");
			this.LargeImageList.Images.SetKeyName(48, "帕灯.bmp");
			// 
			// tongdaoGroupBox
			// 
			this.tongdaoGroupBox.BackColor = System.Drawing.Color.Transparent;
			this.tongdaoGroupBox.Controls.Add(this.addStepCheckBox);
			this.tongdaoGroupBox.Controls.Add(this.materialUseButton);
			this.tongdaoGroupBox.Controls.Add(this.pasteLightButton);
			this.tongdaoGroupBox.Controls.Add(this.materialSaveButton);
			this.tongdaoGroupBox.Controls.Add(this.copyLightButton);
			this.tongdaoGroupBox.Controls.Add(this.tongdaoPanel);
			this.tongdaoGroupBox.Controls.Add(this.cmComboBox);
			this.tongdaoGroupBox.Controls.Add(this.commonValueNumericUpDown);
			this.tongdaoGroupBox.Controls.Add(this.stNumericUpDown);
			this.tongdaoGroupBox.Controls.Add(this.changeModeButton);
			this.tongdaoGroupBox.Controls.Add(this.commonValueButton);
			this.tongdaoGroupBox.Controls.Add(this.steptimeSetButton);
			this.tongdaoGroupBox.Controls.Add(this.modeChooseLabel);
			this.tongdaoGroupBox.Controls.Add(this.frameChooseLabel);
			this.tongdaoGroupBox.Controls.Add(this.stepLabel);
			this.tongdaoGroupBox.Controls.Add(this.nextStepButton);
			this.tongdaoGroupBox.Controls.Add(this.deleteStepButton);
			this.tongdaoGroupBox.Controls.Add(this.backStepButton);
			this.tongdaoGroupBox.Controls.Add(this.insertBeforeStepButton);
			this.tongdaoGroupBox.Controls.Add(this.insertAfterStepButton);
			this.tongdaoGroupBox.Controls.Add(this.initButton);
			this.tongdaoGroupBox.Controls.Add(this.zeroButton);
			this.tongdaoGroupBox.Controls.Add(this.pasteStepButton);
			this.tongdaoGroupBox.Controls.Add(this.copyStepButton);
			this.tongdaoGroupBox.Controls.Add(this.addStepButton);
			this.tongdaoGroupBox.Controls.Add(this.lightValueLabel);
			this.tongdaoGroupBox.Controls.Add(this.lightLabel);
			this.tongdaoGroupBox.Controls.Add(this.frameComboBox);
			this.tongdaoGroupBox.Controls.Add(this.modeComboBox);
			this.tongdaoGroupBox.Location = new System.Drawing.Point(0, 360);
			this.tongdaoGroupBox.Name = "tongdaoGroupBox";
			this.tongdaoGroupBox.Size = new System.Drawing.Size(1915, 520);
			this.tongdaoGroupBox.TabIndex = 8;
			this.tongdaoGroupBox.TabStop = false;
			this.tongdaoGroupBox.Visible = false;
			// 
			// addStepCheckBox
			// 
			this.addStepCheckBox.AutoSize = true;
			this.addStepCheckBox.Location = new System.Drawing.Point(1209, 27);
			this.addStepCheckBox.Name = "addStepCheckBox";
			this.addStepCheckBox.Size = new System.Drawing.Size(179, 19);
			this.addStepCheckBox.TabIndex = 22;
			this.addStepCheckBox.Text = "添加步时使用模板数据";
			this.addStepCheckBox.UseVisualStyleBackColor = true;
			this.addStepCheckBox.CheckedChanged += new System.EventHandler(this.addStepCheckBox_CheckedChanged);
			// 
			// materialUseButton
			// 
			this.materialUseButton.Location = new System.Drawing.Point(184, 137);
			this.materialUseButton.Name = "materialUseButton";
			this.materialUseButton.Size = new System.Drawing.Size(98, 35);
			this.materialUseButton.TabIndex = 21;
			this.materialUseButton.Text = "使用素材";
			this.materialUseButton.UseVisualStyleBackColor = true;
			this.materialUseButton.Click += new System.EventHandler(this.materialUseButton_Click);
			// 
			// pasteLightButton
			// 
			this.pasteLightButton.Enabled = false;
			this.pasteLightButton.Location = new System.Drawing.Point(184, 82);
			this.pasteLightButton.Name = "pasteLightButton";
			this.pasteLightButton.Size = new System.Drawing.Size(98, 35);
			this.pasteLightButton.TabIndex = 21;
			this.pasteLightButton.Text = "粘贴灯";
			this.pasteLightButton.UseVisualStyleBackColor = true;
			this.pasteLightButton.Click += new System.EventHandler(this.pasteLightButton_Click);
			// 
			// materialSaveButton
			// 
			this.materialSaveButton.Location = new System.Drawing.Point(64, 137);
			this.materialSaveButton.Name = "materialSaveButton";
			this.materialSaveButton.Size = new System.Drawing.Size(98, 35);
			this.materialSaveButton.TabIndex = 21;
			this.materialSaveButton.Text = "保存素材";
			this.materialSaveButton.UseVisualStyleBackColor = true;
			this.materialSaveButton.Click += new System.EventHandler(this.materialSaveButton_Click);
			// 
			// copyLightButton
			// 
			this.copyLightButton.Location = new System.Drawing.Point(64, 82);
			this.copyLightButton.Name = "copyLightButton";
			this.copyLightButton.Size = new System.Drawing.Size(98, 35);
			this.copyLightButton.TabIndex = 21;
			this.copyLightButton.Text = "复制灯";
			this.copyLightButton.UseVisualStyleBackColor = true;
			this.copyLightButton.Click += new System.EventHandler(this.copyLightButton_Click);
			// 
			// tongdaoPanel
			// 
			this.tongdaoPanel.AutoScroll = true;
			this.tongdaoPanel.Controls.Add(this.tongdaoGroupBox1);
			this.tongdaoPanel.Controls.Add(this.tongdaoGroupBox2);
			this.tongdaoPanel.Location = new System.Drawing.Point(376, 135);
			this.tongdaoPanel.Name = "tongdaoPanel";
			this.tongdaoPanel.Size = new System.Drawing.Size(1539, 377);
			this.tongdaoPanel.TabIndex = 20;
			// 
			// tongdaoGroupBox1
			// 
			this.tongdaoGroupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.tongdaoGroupBox1.Controls.Add(this.changeModeLabel);
			this.tongdaoGroupBox1.Controls.Add(this.tongdaoValueLabel);
			this.tongdaoGroupBox1.Controls.Add(this.stepTimeLabel);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox16);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox15);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox14);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox13);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox12);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox11);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox10);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox9);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox8);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox7);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox6);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox5);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox4);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox3);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox2);
			this.tongdaoGroupBox1.Controls.Add(this.changeModeComboBox1);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown48);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown16);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown47);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown15);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown46);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown14);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown45);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown13);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown44);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown12);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown43);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown11);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown42);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown10);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown41);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown9);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown40);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown8);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown39);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown7);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown38);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown6);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown37);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown5);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown36);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown4);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown35);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown3);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown34);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown2);
			this.tongdaoGroupBox1.Controls.Add(this.numericUpDown33);
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
			this.tongdaoGroupBox1.Location = new System.Drawing.Point(3, 2);
			this.tongdaoGroupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tongdaoGroupBox1.Name = "tongdaoGroupBox1";
			this.tongdaoGroupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tongdaoGroupBox1.Size = new System.Drawing.Size(1508, 375);
			this.tongdaoGroupBox1.TabIndex = 9;
			this.tongdaoGroupBox1.TabStop = false;
			// 
			// changeModeLabel
			// 
			this.changeModeLabel.AutoSize = true;
			this.changeModeLabel.Location = new System.Drawing.Point(37, 291);
			this.changeModeLabel.Name = "changeModeLabel";
			this.changeModeLabel.Size = new System.Drawing.Size(67, 15);
			this.changeModeLabel.TabIndex = 14;
			this.changeModeLabel.Text = "变化方式";
			// 
			// tongdaoValueLabel
			// 
			this.tongdaoValueLabel.AutoSize = true;
			this.tongdaoValueLabel.Location = new System.Drawing.Point(38, 263);
			this.tongdaoValueLabel.Name = "tongdaoValueLabel";
			this.tongdaoValueLabel.Size = new System.Drawing.Size(68, 15);
			this.tongdaoValueLabel.TabIndex = 13;
			this.tongdaoValueLabel.Text = "通 道 值";
			// 
			// stepTimeLabel
			// 
			this.stepTimeLabel.AutoSize = true;
			this.stepTimeLabel.Location = new System.Drawing.Point(38, 320);
			this.stepTimeLabel.Name = "stepTimeLabel";
			this.stepTimeLabel.Size = new System.Drawing.Size(68, 15);
			this.stepTimeLabel.TabIndex = 13;
			this.stepTimeLabel.Text = "步 时 间";
			// 
			// changeModeComboBox16
			// 
			this.changeModeComboBox16.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox16.FormattingEnabled = true;
			this.changeModeComboBox16.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox16.Location = new System.Drawing.Point(1402, 287);
			this.changeModeComboBox16.Name = "changeModeComboBox16";
			this.changeModeComboBox16.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox16.TabIndex = 12;
			this.changeModeComboBox16.Visible = false;
			this.changeModeComboBox16.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox15
			// 
			this.changeModeComboBox15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox15.FormattingEnabled = true;
			this.changeModeComboBox15.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox15.Location = new System.Drawing.Point(1320, 287);
			this.changeModeComboBox15.Name = "changeModeComboBox15";
			this.changeModeComboBox15.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox15.TabIndex = 12;
			this.changeModeComboBox15.Visible = false;
			this.changeModeComboBox15.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox14
			// 
			this.changeModeComboBox14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox14.FormattingEnabled = true;
			this.changeModeComboBox14.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox14.Location = new System.Drawing.Point(1232, 287);
			this.changeModeComboBox14.Name = "changeModeComboBox14";
			this.changeModeComboBox14.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox14.TabIndex = 12;
			this.changeModeComboBox14.Visible = false;
			this.changeModeComboBox14.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox13
			// 
			this.changeModeComboBox13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox13.FormattingEnabled = true;
			this.changeModeComboBox13.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox13.Location = new System.Drawing.Point(1147, 287);
			this.changeModeComboBox13.Name = "changeModeComboBox13";
			this.changeModeComboBox13.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox13.TabIndex = 12;
			this.changeModeComboBox13.Visible = false;
			this.changeModeComboBox13.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox12
			// 
			this.changeModeComboBox12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox12.FormattingEnabled = true;
			this.changeModeComboBox12.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox12.Location = new System.Drawing.Point(1069, 287);
			this.changeModeComboBox12.Name = "changeModeComboBox12";
			this.changeModeComboBox12.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox12.TabIndex = 12;
			this.changeModeComboBox12.Visible = false;
			this.changeModeComboBox12.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox11
			// 
			this.changeModeComboBox11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox11.FormattingEnabled = true;
			this.changeModeComboBox11.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox11.Location = new System.Drawing.Point(984, 287);
			this.changeModeComboBox11.Name = "changeModeComboBox11";
			this.changeModeComboBox11.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox11.TabIndex = 12;
			this.changeModeComboBox11.Visible = false;
			this.changeModeComboBox11.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox10
			// 
			this.changeModeComboBox10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox10.FormattingEnabled = true;
			this.changeModeComboBox10.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox10.Location = new System.Drawing.Point(892, 287);
			this.changeModeComboBox10.Name = "changeModeComboBox10";
			this.changeModeComboBox10.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox10.TabIndex = 12;
			this.changeModeComboBox10.Visible = false;
			this.changeModeComboBox10.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox9
			// 
			this.changeModeComboBox9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox9.FormattingEnabled = true;
			this.changeModeComboBox9.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox9.Location = new System.Drawing.Point(809, 287);
			this.changeModeComboBox9.Name = "changeModeComboBox9";
			this.changeModeComboBox9.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox9.TabIndex = 12;
			this.changeModeComboBox9.Visible = false;
			this.changeModeComboBox9.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox8
			// 
			this.changeModeComboBox8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox8.FormattingEnabled = true;
			this.changeModeComboBox8.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox8.Location = new System.Drawing.Point(722, 287);
			this.changeModeComboBox8.Name = "changeModeComboBox8";
			this.changeModeComboBox8.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox8.TabIndex = 12;
			this.changeModeComboBox8.Visible = false;
			this.changeModeComboBox8.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox7
			// 
			this.changeModeComboBox7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox7.FormattingEnabled = true;
			this.changeModeComboBox7.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox7.Location = new System.Drawing.Point(637, 287);
			this.changeModeComboBox7.Name = "changeModeComboBox7";
			this.changeModeComboBox7.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox7.TabIndex = 12;
			this.changeModeComboBox7.Visible = false;
			this.changeModeComboBox7.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox6
			// 
			this.changeModeComboBox6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox6.FormattingEnabled = true;
			this.changeModeComboBox6.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox6.Location = new System.Drawing.Point(558, 287);
			this.changeModeComboBox6.Name = "changeModeComboBox6";
			this.changeModeComboBox6.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox6.TabIndex = 12;
			this.changeModeComboBox6.Visible = false;
			this.changeModeComboBox6.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox5
			// 
			this.changeModeComboBox5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox5.FormattingEnabled = true;
			this.changeModeComboBox5.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox5.Location = new System.Drawing.Point(474, 287);
			this.changeModeComboBox5.Name = "changeModeComboBox5";
			this.changeModeComboBox5.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox5.TabIndex = 12;
			this.changeModeComboBox5.Visible = false;
			this.changeModeComboBox5.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox4
			// 
			this.changeModeComboBox4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox4.FormattingEnabled = true;
			this.changeModeComboBox4.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox4.Location = new System.Drawing.Point(385, 287);
			this.changeModeComboBox4.Name = "changeModeComboBox4";
			this.changeModeComboBox4.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox4.TabIndex = 12;
			this.changeModeComboBox4.Visible = false;
			this.changeModeComboBox4.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox3
			// 
			this.changeModeComboBox3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox3.FormattingEnabled = true;
			this.changeModeComboBox3.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox3.Location = new System.Drawing.Point(304, 287);
			this.changeModeComboBox3.Name = "changeModeComboBox3";
			this.changeModeComboBox3.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox3.TabIndex = 12;
			this.changeModeComboBox3.Visible = false;
			this.changeModeComboBox3.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox2
			// 
			this.changeModeComboBox2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox2.FormattingEnabled = true;
			this.changeModeComboBox2.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox2.Location = new System.Drawing.Point(212, 287);
			this.changeModeComboBox2.Name = "changeModeComboBox2";
			this.changeModeComboBox2.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox2.TabIndex = 12;
			this.changeModeComboBox2.Visible = false;
			this.changeModeComboBox2.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// changeModeComboBox1
			// 
			this.changeModeComboBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox1.FormattingEnabled = true;
			this.changeModeComboBox1.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox1.Location = new System.Drawing.Point(127, 287);
			this.changeModeComboBox1.Name = "changeModeComboBox1";
			this.changeModeComboBox1.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox1.TabIndex = 12;
			this.changeModeComboBox1.Visible = false;
			this.changeModeComboBox1.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// numericUpDown48
			// 
			this.numericUpDown48.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown48.Location = new System.Drawing.Point(1407, 316);
			this.numericUpDown48.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown48.Name = "numericUpDown48";
			this.numericUpDown48.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown48.TabIndex = 11;
			this.numericUpDown48.Visible = false;
			this.numericUpDown48.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown16
			// 
			this.numericUpDown16.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown16.Location = new System.Drawing.Point(1407, 259);
			this.numericUpDown16.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown16.Name = "numericUpDown16";
			this.numericUpDown16.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown16.TabIndex = 11;
			this.numericUpDown16.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown47
			// 
			this.numericUpDown47.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown47.Location = new System.Drawing.Point(1325, 316);
			this.numericUpDown47.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown47.Name = "numericUpDown47";
			this.numericUpDown47.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown47.TabIndex = 11;
			this.numericUpDown47.Visible = false;
			this.numericUpDown47.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown15
			// 
			this.numericUpDown15.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown15.Location = new System.Drawing.Point(1325, 259);
			this.numericUpDown15.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown15.Name = "numericUpDown15";
			this.numericUpDown15.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown15.TabIndex = 11;
			this.numericUpDown15.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown46
			// 
			this.numericUpDown46.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown46.Location = new System.Drawing.Point(1237, 316);
			this.numericUpDown46.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown46.Name = "numericUpDown46";
			this.numericUpDown46.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown46.TabIndex = 11;
			this.numericUpDown46.Visible = false;
			this.numericUpDown46.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown14
			// 
			this.numericUpDown14.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown14.Location = new System.Drawing.Point(1237, 259);
			this.numericUpDown14.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown14.Name = "numericUpDown14";
			this.numericUpDown14.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown14.TabIndex = 11;
			this.numericUpDown14.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown45
			// 
			this.numericUpDown45.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown45.Location = new System.Drawing.Point(1152, 316);
			this.numericUpDown45.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown45.Name = "numericUpDown45";
			this.numericUpDown45.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown45.TabIndex = 11;
			this.numericUpDown45.Visible = false;
			this.numericUpDown45.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown13
			// 
			this.numericUpDown13.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown13.Location = new System.Drawing.Point(1152, 259);
			this.numericUpDown13.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown13.Name = "numericUpDown13";
			this.numericUpDown13.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown13.TabIndex = 11;
			this.numericUpDown13.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown44
			// 
			this.numericUpDown44.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown44.Location = new System.Drawing.Point(1074, 316);
			this.numericUpDown44.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown44.Name = "numericUpDown44";
			this.numericUpDown44.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown44.TabIndex = 11;
			this.numericUpDown44.Visible = false;
			this.numericUpDown44.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown12
			// 
			this.numericUpDown12.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown12.Location = new System.Drawing.Point(1074, 259);
			this.numericUpDown12.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown12.Name = "numericUpDown12";
			this.numericUpDown12.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown12.TabIndex = 11;
			this.numericUpDown12.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown43
			// 
			this.numericUpDown43.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown43.Location = new System.Drawing.Point(989, 316);
			this.numericUpDown43.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown43.Name = "numericUpDown43";
			this.numericUpDown43.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown43.TabIndex = 11;
			this.numericUpDown43.Visible = false;
			this.numericUpDown43.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown11
			// 
			this.numericUpDown11.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown11.Location = new System.Drawing.Point(989, 259);
			this.numericUpDown11.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown11.Name = "numericUpDown11";
			this.numericUpDown11.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown11.TabIndex = 11;
			this.numericUpDown11.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown42
			// 
			this.numericUpDown42.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown42.Location = new System.Drawing.Point(897, 316);
			this.numericUpDown42.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown42.Name = "numericUpDown42";
			this.numericUpDown42.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown42.TabIndex = 11;
			this.numericUpDown42.Visible = false;
			this.numericUpDown42.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown10
			// 
			this.numericUpDown10.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown10.Location = new System.Drawing.Point(897, 259);
			this.numericUpDown10.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown10.Name = "numericUpDown10";
			this.numericUpDown10.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown10.TabIndex = 11;
			this.numericUpDown10.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown41
			// 
			this.numericUpDown41.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown41.Location = new System.Drawing.Point(814, 316);
			this.numericUpDown41.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown41.Name = "numericUpDown41";
			this.numericUpDown41.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown41.TabIndex = 11;
			this.numericUpDown41.Visible = false;
			this.numericUpDown41.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown9
			// 
			this.numericUpDown9.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown9.Location = new System.Drawing.Point(814, 259);
			this.numericUpDown9.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown9.Name = "numericUpDown9";
			this.numericUpDown9.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown9.TabIndex = 11;
			this.numericUpDown9.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown40
			// 
			this.numericUpDown40.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown40.Location = new System.Drawing.Point(727, 316);
			this.numericUpDown40.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown40.Name = "numericUpDown40";
			this.numericUpDown40.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown40.TabIndex = 11;
			this.numericUpDown40.Visible = false;
			this.numericUpDown40.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown8
			// 
			this.numericUpDown8.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown8.Location = new System.Drawing.Point(727, 259);
			this.numericUpDown8.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown8.Name = "numericUpDown8";
			this.numericUpDown8.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown8.TabIndex = 11;
			this.numericUpDown8.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown39
			// 
			this.numericUpDown39.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown39.Location = new System.Drawing.Point(642, 316);
			this.numericUpDown39.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown39.Name = "numericUpDown39";
			this.numericUpDown39.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown39.TabIndex = 11;
			this.numericUpDown39.Visible = false;
			this.numericUpDown39.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown7
			// 
			this.numericUpDown7.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown7.Location = new System.Drawing.Point(642, 259);
			this.numericUpDown7.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown7.Name = "numericUpDown7";
			this.numericUpDown7.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown7.TabIndex = 11;
			this.numericUpDown7.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown38
			// 
			this.numericUpDown38.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown38.Location = new System.Drawing.Point(563, 316);
			this.numericUpDown38.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown38.Name = "numericUpDown38";
			this.numericUpDown38.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown38.TabIndex = 11;
			this.numericUpDown38.Visible = false;
			this.numericUpDown38.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown6
			// 
			this.numericUpDown6.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown6.Location = new System.Drawing.Point(563, 259);
			this.numericUpDown6.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown6.Name = "numericUpDown6";
			this.numericUpDown6.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown6.TabIndex = 11;
			this.numericUpDown6.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown37
			// 
			this.numericUpDown37.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown37.Location = new System.Drawing.Point(479, 316);
			this.numericUpDown37.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown37.Name = "numericUpDown37";
			this.numericUpDown37.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown37.TabIndex = 11;
			this.numericUpDown37.Visible = false;
			this.numericUpDown37.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown5
			// 
			this.numericUpDown5.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown5.Location = new System.Drawing.Point(479, 259);
			this.numericUpDown5.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown5.Name = "numericUpDown5";
			this.numericUpDown5.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown5.TabIndex = 11;
			this.numericUpDown5.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown36
			// 
			this.numericUpDown36.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown36.Location = new System.Drawing.Point(390, 316);
			this.numericUpDown36.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown36.Name = "numericUpDown36";
			this.numericUpDown36.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown36.TabIndex = 11;
			this.numericUpDown36.Visible = false;
			this.numericUpDown36.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown4
			// 
			this.numericUpDown4.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown4.Location = new System.Drawing.Point(390, 259);
			this.numericUpDown4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown4.TabIndex = 11;
			this.numericUpDown4.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown35
			// 
			this.numericUpDown35.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown35.Location = new System.Drawing.Point(309, 316);
			this.numericUpDown35.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown35.Name = "numericUpDown35";
			this.numericUpDown35.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown35.TabIndex = 11;
			this.numericUpDown35.Visible = false;
			this.numericUpDown35.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown3
			// 
			this.numericUpDown3.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown3.Location = new System.Drawing.Point(309, 259);
			this.numericUpDown3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown3.TabIndex = 11;
			this.numericUpDown3.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown34
			// 
			this.numericUpDown34.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown34.Location = new System.Drawing.Point(217, 316);
			this.numericUpDown34.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown34.Name = "numericUpDown34";
			this.numericUpDown34.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown34.TabIndex = 11;
			this.numericUpDown34.Visible = false;
			this.numericUpDown34.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown2.Location = new System.Drawing.Point(217, 259);
			this.numericUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown2.TabIndex = 11;
			this.numericUpDown2.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown33
			// 
			this.numericUpDown33.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown33.Location = new System.Drawing.Point(132, 316);
			this.numericUpDown33.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown33.Name = "numericUpDown33";
			this.numericUpDown33.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown33.TabIndex = 11;
			this.numericUpDown33.Visible = false;
			this.numericUpDown33.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown1.Location = new System.Drawing.Point(132, 259);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown1.TabIndex = 11;
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// label16
			// 
			this.label16.Font = new System.Drawing.Font("宋体", 8F);
			this.label16.Location = new System.Drawing.Point(1397, 45);
			this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(19, 203);
			this.label16.TabIndex = 10;
			this.label16.Text = "总调光1";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label16.Visible = false;
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("宋体", 8F);
			this.label15.Location = new System.Drawing.Point(1312, 45);
			this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(19, 203);
			this.label15.TabIndex = 10;
			this.label15.Text = "总调光1";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label15.Visible = false;
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("宋体", 8F);
			this.label14.Location = new System.Drawing.Point(1227, 45);
			this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(19, 203);
			this.label14.TabIndex = 10;
			this.label14.Text = "总调光1";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label14.Visible = false;
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("宋体", 8F);
			this.label13.Location = new System.Drawing.Point(1142, 45);
			this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(19, 203);
			this.label13.TabIndex = 10;
			this.label13.Text = "总调光1";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label13.Visible = false;
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("宋体", 8F);
			this.label12.Location = new System.Drawing.Point(1057, 45);
			this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(19, 203);
			this.label12.TabIndex = 10;
			this.label12.Text = "总调光1";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label12.Visible = false;
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("宋体", 8F);
			this.label11.Location = new System.Drawing.Point(972, 45);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(19, 203);
			this.label11.TabIndex = 10;
			this.label11.Text = "总调光1";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label11.Visible = false;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("宋体", 8F);
			this.label10.Location = new System.Drawing.Point(887, 45);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(19, 203);
			this.label10.TabIndex = 10;
			this.label10.Text = "总调光1";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label10.Visible = false;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("宋体", 8F);
			this.label9.Location = new System.Drawing.Point(802, 45);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(19, 203);
			this.label9.TabIndex = 10;
			this.label9.Text = "总调光1";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label9.Visible = false;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("宋体", 8F);
			this.label8.Location = new System.Drawing.Point(717, 45);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(19, 203);
			this.label8.TabIndex = 10;
			this.label8.Text = "总调光1";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label8.Visible = false;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("宋体", 8F);
			this.label7.Location = new System.Drawing.Point(632, 45);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(19, 203);
			this.label7.TabIndex = 10;
			this.label7.Text = "总调光1";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label7.Visible = false;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("宋体", 8F);
			this.label6.Location = new System.Drawing.Point(547, 45);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(19, 203);
			this.label6.TabIndex = 10;
			this.label6.Text = "总调光1";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label6.Visible = false;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("宋体", 8F);
			this.label5.Location = new System.Drawing.Point(462, 45);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(19, 203);
			this.label5.TabIndex = 10;
			this.label5.Text = "总调光1";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label5.Visible = false;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("宋体", 8F);
			this.label4.Location = new System.Drawing.Point(377, 45);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(19, 203);
			this.label4.TabIndex = 10;
			this.label4.Text = "总调光1";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label4.Visible = false;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 8F);
			this.label3.Location = new System.Drawing.Point(292, 45);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(19, 203);
			this.label3.TabIndex = 10;
			this.label3.Text = "总调光1";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label3.Visible = false;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 8F);
			this.label2.Location = new System.Drawing.Point(207, 45);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(19, 203);
			this.label2.TabIndex = 10;
			this.label2.Text = "总调光1";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label2.Visible = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 8F);
			this.label1.Location = new System.Drawing.Point(122, 45);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(19, 203);
			this.label1.TabIndex = 10;
			this.label1.Text = "总调光1";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label1.Visible = false;
			// 
			// vScrollBar16
			// 
			this.vScrollBar16.Location = new System.Drawing.Point(1420, 45);
			this.vScrollBar16.Maximum = 264;
			this.vScrollBar16.Name = "vScrollBar16";
			this.vScrollBar16.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar16.TabIndex = 0;
			// 
			// vScrollBar12
			// 
			this.vScrollBar12.Location = new System.Drawing.Point(1087, 45);
			this.vScrollBar12.Maximum = 264;
			this.vScrollBar12.Name = "vScrollBar12";
			this.vScrollBar12.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar12.TabIndex = 0;
			// 
			// vScrollBar8
			// 
			this.vScrollBar8.Location = new System.Drawing.Point(740, 45);
			this.vScrollBar8.Maximum = 264;
			this.vScrollBar8.Name = "vScrollBar8";
			this.vScrollBar8.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar8.TabIndex = 0;
			// 
			// vScrollBar4
			// 
			this.vScrollBar4.Location = new System.Drawing.Point(403, 45);
			this.vScrollBar4.Maximum = 264;
			this.vScrollBar4.Name = "vScrollBar4";
			this.vScrollBar4.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar4.TabIndex = 0;
			// 
			// vScrollBar15
			// 
			this.vScrollBar15.Location = new System.Drawing.Point(1338, 45);
			this.vScrollBar15.Maximum = 264;
			this.vScrollBar15.Name = "vScrollBar15";
			this.vScrollBar15.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar15.TabIndex = 0;
			// 
			// vScrollBar11
			// 
			this.vScrollBar11.Location = new System.Drawing.Point(1002, 45);
			this.vScrollBar11.Maximum = 264;
			this.vScrollBar11.Name = "vScrollBar11";
			this.vScrollBar11.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar11.TabIndex = 0;
			// 
			// vScrollBar14
			// 
			this.vScrollBar14.Location = new System.Drawing.Point(1250, 45);
			this.vScrollBar14.Maximum = 264;
			this.vScrollBar14.Name = "vScrollBar14";
			this.vScrollBar14.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar14.TabIndex = 0;
			// 
			// vScrollBar10
			// 
			this.vScrollBar10.Location = new System.Drawing.Point(910, 45);
			this.vScrollBar10.Maximum = 264;
			this.vScrollBar10.Name = "vScrollBar10";
			this.vScrollBar10.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar10.TabIndex = 0;
			// 
			// vScrollBar7
			// 
			this.vScrollBar7.Location = new System.Drawing.Point(655, 45);
			this.vScrollBar7.Maximum = 264;
			this.vScrollBar7.Name = "vScrollBar7";
			this.vScrollBar7.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar7.TabIndex = 0;
			// 
			// vScrollBar13
			// 
			this.vScrollBar13.Location = new System.Drawing.Point(1165, 45);
			this.vScrollBar13.Maximum = 264;
			this.vScrollBar13.Name = "vScrollBar13";
			this.vScrollBar13.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar13.TabIndex = 0;
			// 
			// vScrollBar6
			// 
			this.vScrollBar6.Location = new System.Drawing.Point(576, 45);
			this.vScrollBar6.Maximum = 264;
			this.vScrollBar6.Name = "vScrollBar6";
			this.vScrollBar6.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar6.TabIndex = 0;
			// 
			// vScrollBar9
			// 
			this.vScrollBar9.Location = new System.Drawing.Point(827, 45);
			this.vScrollBar9.Maximum = 264;
			this.vScrollBar9.Name = "vScrollBar9";
			this.vScrollBar9.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar9.TabIndex = 0;
			// 
			// vScrollBar3
			// 
			this.vScrollBar3.Location = new System.Drawing.Point(322, 45);
			this.vScrollBar3.Maximum = 264;
			this.vScrollBar3.Name = "vScrollBar3";
			this.vScrollBar3.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar3.TabIndex = 0;
			// 
			// vScrollBar5
			// 
			this.vScrollBar5.Location = new System.Drawing.Point(492, 45);
			this.vScrollBar5.Maximum = 264;
			this.vScrollBar5.Name = "vScrollBar5";
			this.vScrollBar5.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar5.TabIndex = 0;
			// 
			// vScrollBar2
			// 
			this.vScrollBar2.Location = new System.Drawing.Point(230, 45);
			this.vScrollBar2.Maximum = 264;
			this.vScrollBar2.Name = "vScrollBar2";
			this.vScrollBar2.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar2.TabIndex = 0;
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Location = new System.Drawing.Point(145, 45);
			this.vScrollBar1.Maximum = 264;
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar1.TabIndex = 0;
			// 
			// tongdaoGroupBox2
			// 
			this.tongdaoGroupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.tongdaoGroupBox2.Controls.Add(this.changeModeLabel2);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown32);
			this.tongdaoGroupBox2.Controls.Add(this.tongdaoValueLabel2);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox32);
			this.tongdaoGroupBox2.Controls.Add(this.stepTimeLabel2);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox31);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown64);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox30);
			this.tongdaoGroupBox2.Controls.Add(this.label32);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox29);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown31);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox28);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown63);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox27);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar17);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox26);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown30);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox25);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown62);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox24);
			this.tongdaoGroupBox2.Controls.Add(this.label31);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox23);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown29);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox22);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown61);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox21);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar18);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox20);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown28);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox19);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown60);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox18);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar19);
			this.tongdaoGroupBox2.Controls.Add(this.changeModeComboBox17);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown27);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown59);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar20);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown26);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown58);
			this.tongdaoGroupBox2.Controls.Add(this.label30);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown25);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown57);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar21);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown24);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown56);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar22);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown23);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown55);
			this.tongdaoGroupBox2.Controls.Add(this.label29);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown22);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown54);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar23);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown21);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown53);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar24);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown20);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown52);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar25);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown19);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown51);
			this.tongdaoGroupBox2.Controls.Add(this.label28);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown18);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown50);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar26);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown17);
			this.tongdaoGroupBox2.Controls.Add(this.numericUpDown49);
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
			this.tongdaoGroupBox2.Location = new System.Drawing.Point(3, 381);
			this.tongdaoGroupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tongdaoGroupBox2.Name = "tongdaoGroupBox2";
			this.tongdaoGroupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tongdaoGroupBox2.Size = new System.Drawing.Size(1508, 375);
			this.tongdaoGroupBox2.TabIndex = 10;
			this.tongdaoGroupBox2.TabStop = false;
			// 
			// changeModeLabel2
			// 
			this.changeModeLabel2.AutoSize = true;
			this.changeModeLabel2.Location = new System.Drawing.Point(37, 299);
			this.changeModeLabel2.Name = "changeModeLabel2";
			this.changeModeLabel2.Size = new System.Drawing.Size(67, 15);
			this.changeModeLabel2.TabIndex = 17;
			this.changeModeLabel2.Text = "变化方式";
			// 
			// numericUpDown32
			// 
			this.numericUpDown32.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown32.Location = new System.Drawing.Point(1407, 265);
			this.numericUpDown32.Name = "numericUpDown32";
			this.numericUpDown32.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown32.TabIndex = 11;
			this.numericUpDown32.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// tongdaoValueLabel2
			// 
			this.tongdaoValueLabel2.AutoSize = true;
			this.tongdaoValueLabel2.Location = new System.Drawing.Point(38, 269);
			this.tongdaoValueLabel2.Name = "tongdaoValueLabel2";
			this.tongdaoValueLabel2.Size = new System.Drawing.Size(68, 15);
			this.tongdaoValueLabel2.TabIndex = 15;
			this.tongdaoValueLabel2.Text = "通 道 值";
			// 
			// changeModeComboBox32
			// 
			this.changeModeComboBox32.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox32.FormattingEnabled = true;
			this.changeModeComboBox32.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox32.Location = new System.Drawing.Point(1402, 296);
			this.changeModeComboBox32.Name = "changeModeComboBox32";
			this.changeModeComboBox32.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox32.TabIndex = 12;
			this.changeModeComboBox32.Visible = false;
			this.changeModeComboBox32.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// stepTimeLabel2
			// 
			this.stepTimeLabel2.AutoSize = true;
			this.stepTimeLabel2.Location = new System.Drawing.Point(38, 330);
			this.stepTimeLabel2.Name = "stepTimeLabel2";
			this.stepTimeLabel2.Size = new System.Drawing.Size(68, 15);
			this.stepTimeLabel2.TabIndex = 16;
			this.stepTimeLabel2.Text = "步 时 间";
			// 
			// changeModeComboBox31
			// 
			this.changeModeComboBox31.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox31.FormattingEnabled = true;
			this.changeModeComboBox31.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox31.Location = new System.Drawing.Point(1320, 295);
			this.changeModeComboBox31.Name = "changeModeComboBox31";
			this.changeModeComboBox31.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox31.TabIndex = 12;
			this.changeModeComboBox31.Visible = false;
			this.changeModeComboBox31.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// numericUpDown64
			// 
			this.numericUpDown64.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown64.Location = new System.Drawing.Point(1409, 330);
			this.numericUpDown64.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown64.Name = "numericUpDown64";
			this.numericUpDown64.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown64.TabIndex = 11;
			this.numericUpDown64.Visible = false;
			this.numericUpDown64.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// changeModeComboBox30
			// 
			this.changeModeComboBox30.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox30.FormattingEnabled = true;
			this.changeModeComboBox30.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox30.Location = new System.Drawing.Point(1232, 295);
			this.changeModeComboBox30.Name = "changeModeComboBox30";
			this.changeModeComboBox30.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox30.TabIndex = 12;
			this.changeModeComboBox30.Visible = false;
			this.changeModeComboBox30.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// label32
			// 
			this.label32.Font = new System.Drawing.Font("宋体", 8F);
			this.label32.Location = new System.Drawing.Point(1396, 52);
			this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(19, 203);
			this.label32.TabIndex = 10;
			this.label32.Text = "总调光1";
			this.label32.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label32.Visible = false;
			// 
			// changeModeComboBox29
			// 
			this.changeModeComboBox29.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox29.FormattingEnabled = true;
			this.changeModeComboBox29.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox29.Location = new System.Drawing.Point(1147, 295);
			this.changeModeComboBox29.Name = "changeModeComboBox29";
			this.changeModeComboBox29.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox29.TabIndex = 12;
			this.changeModeComboBox29.Visible = false;
			this.changeModeComboBox29.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// numericUpDown31
			// 
			this.numericUpDown31.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown31.Location = new System.Drawing.Point(1325, 265);
			this.numericUpDown31.Name = "numericUpDown31";
			this.numericUpDown31.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown31.TabIndex = 11;
			this.numericUpDown31.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// changeModeComboBox28
			// 
			this.changeModeComboBox28.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox28.FormattingEnabled = true;
			this.changeModeComboBox28.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox28.Location = new System.Drawing.Point(1062, 295);
			this.changeModeComboBox28.Name = "changeModeComboBox28";
			this.changeModeComboBox28.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox28.TabIndex = 12;
			this.changeModeComboBox28.Visible = false;
			this.changeModeComboBox28.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// numericUpDown63
			// 
			this.numericUpDown63.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown63.Location = new System.Drawing.Point(1325, 330);
			this.numericUpDown63.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown63.Name = "numericUpDown63";
			this.numericUpDown63.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown63.TabIndex = 11;
			this.numericUpDown63.Visible = false;
			this.numericUpDown63.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// changeModeComboBox27
			// 
			this.changeModeComboBox27.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox27.FormattingEnabled = true;
			this.changeModeComboBox27.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox27.Location = new System.Drawing.Point(977, 295);
			this.changeModeComboBox27.Name = "changeModeComboBox27";
			this.changeModeComboBox27.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox27.TabIndex = 12;
			this.changeModeComboBox27.Visible = false;
			this.changeModeComboBox27.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// vScrollBar17
			// 
			this.vScrollBar17.Location = new System.Drawing.Point(145, 52);
			this.vScrollBar17.Maximum = 264;
			this.vScrollBar17.Name = "vScrollBar17";
			this.vScrollBar17.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar17.TabIndex = 0;
			this.vScrollBar17.Visible = false;
			// 
			// changeModeComboBox26
			// 
			this.changeModeComboBox26.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox26.FormattingEnabled = true;
			this.changeModeComboBox26.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox26.Location = new System.Drawing.Point(892, 295);
			this.changeModeComboBox26.Name = "changeModeComboBox26";
			this.changeModeComboBox26.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox26.TabIndex = 12;
			this.changeModeComboBox26.Visible = false;
			this.changeModeComboBox26.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// numericUpDown30
			// 
			this.numericUpDown30.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown30.Location = new System.Drawing.Point(1237, 265);
			this.numericUpDown30.Name = "numericUpDown30";
			this.numericUpDown30.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown30.TabIndex = 11;
			this.numericUpDown30.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// changeModeComboBox25
			// 
			this.changeModeComboBox25.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox25.FormattingEnabled = true;
			this.changeModeComboBox25.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox25.Location = new System.Drawing.Point(807, 295);
			this.changeModeComboBox25.Name = "changeModeComboBox25";
			this.changeModeComboBox25.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox25.TabIndex = 12;
			this.changeModeComboBox25.Visible = false;
			this.changeModeComboBox25.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// numericUpDown62
			// 
			this.numericUpDown62.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown62.Location = new System.Drawing.Point(1237, 326);
			this.numericUpDown62.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown62.Name = "numericUpDown62";
			this.numericUpDown62.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown62.TabIndex = 11;
			this.numericUpDown62.Visible = false;
			this.numericUpDown62.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// changeModeComboBox24
			// 
			this.changeModeComboBox24.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox24.FormattingEnabled = true;
			this.changeModeComboBox24.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox24.Location = new System.Drawing.Point(722, 295);
			this.changeModeComboBox24.Name = "changeModeComboBox24";
			this.changeModeComboBox24.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox24.TabIndex = 12;
			this.changeModeComboBox24.Visible = false;
			this.changeModeComboBox24.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// label31
			// 
			this.label31.Font = new System.Drawing.Font("宋体", 8F);
			this.label31.Location = new System.Drawing.Point(1312, 52);
			this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(19, 203);
			this.label31.TabIndex = 10;
			this.label31.Text = "总调光1";
			this.label31.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label31.Visible = false;
			// 
			// changeModeComboBox23
			// 
			this.changeModeComboBox23.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox23.FormattingEnabled = true;
			this.changeModeComboBox23.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox23.Location = new System.Drawing.Point(637, 295);
			this.changeModeComboBox23.Name = "changeModeComboBox23";
			this.changeModeComboBox23.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox23.TabIndex = 12;
			this.changeModeComboBox23.Visible = false;
			this.changeModeComboBox23.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// numericUpDown29
			// 
			this.numericUpDown29.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown29.Location = new System.Drawing.Point(1152, 265);
			this.numericUpDown29.Name = "numericUpDown29";
			this.numericUpDown29.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown29.TabIndex = 11;
			this.numericUpDown29.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// changeModeComboBox22
			// 
			this.changeModeComboBox22.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox22.FormattingEnabled = true;
			this.changeModeComboBox22.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox22.Location = new System.Drawing.Point(552, 295);
			this.changeModeComboBox22.Name = "changeModeComboBox22";
			this.changeModeComboBox22.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox22.TabIndex = 12;
			this.changeModeComboBox22.Visible = false;
			this.changeModeComboBox22.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// numericUpDown61
			// 
			this.numericUpDown61.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown61.Location = new System.Drawing.Point(1152, 326);
			this.numericUpDown61.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown61.Name = "numericUpDown61";
			this.numericUpDown61.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown61.TabIndex = 11;
			this.numericUpDown61.Visible = false;
			this.numericUpDown61.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// changeModeComboBox21
			// 
			this.changeModeComboBox21.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox21.FormattingEnabled = true;
			this.changeModeComboBox21.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox21.Location = new System.Drawing.Point(467, 295);
			this.changeModeComboBox21.Name = "changeModeComboBox21";
			this.changeModeComboBox21.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox21.TabIndex = 12;
			this.changeModeComboBox21.Visible = false;
			this.changeModeComboBox21.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// vScrollBar18
			// 
			this.vScrollBar18.Location = new System.Drawing.Point(230, 52);
			this.vScrollBar18.Maximum = 264;
			this.vScrollBar18.Name = "vScrollBar18";
			this.vScrollBar18.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar18.TabIndex = 0;
			this.vScrollBar18.Visible = false;
			// 
			// changeModeComboBox20
			// 
			this.changeModeComboBox20.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox20.FormattingEnabled = true;
			this.changeModeComboBox20.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox20.Location = new System.Drawing.Point(382, 295);
			this.changeModeComboBox20.Name = "changeModeComboBox20";
			this.changeModeComboBox20.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox20.TabIndex = 12;
			this.changeModeComboBox20.Visible = false;
			this.changeModeComboBox20.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// numericUpDown28
			// 
			this.numericUpDown28.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown28.Location = new System.Drawing.Point(1067, 265);
			this.numericUpDown28.Name = "numericUpDown28";
			this.numericUpDown28.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown28.TabIndex = 11;
			this.numericUpDown28.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// changeModeComboBox19
			// 
			this.changeModeComboBox19.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox19.FormattingEnabled = true;
			this.changeModeComboBox19.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox19.Location = new System.Drawing.Point(297, 295);
			this.changeModeComboBox19.Name = "changeModeComboBox19";
			this.changeModeComboBox19.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox19.TabIndex = 12;
			this.changeModeComboBox19.Visible = false;
			this.changeModeComboBox19.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// numericUpDown60
			// 
			this.numericUpDown60.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown60.Location = new System.Drawing.Point(1067, 326);
			this.numericUpDown60.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown60.Name = "numericUpDown60";
			this.numericUpDown60.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown60.TabIndex = 11;
			this.numericUpDown60.Visible = false;
			this.numericUpDown60.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// changeModeComboBox18
			// 
			this.changeModeComboBox18.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox18.FormattingEnabled = true;
			this.changeModeComboBox18.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox18.Location = new System.Drawing.Point(212, 295);
			this.changeModeComboBox18.Name = "changeModeComboBox18";
			this.changeModeComboBox18.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox18.TabIndex = 12;
			this.changeModeComboBox18.Visible = false;
			this.changeModeComboBox18.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// vScrollBar19
			// 
			this.vScrollBar19.Location = new System.Drawing.Point(315, 52);
			this.vScrollBar19.Maximum = 264;
			this.vScrollBar19.Name = "vScrollBar19";
			this.vScrollBar19.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar19.TabIndex = 0;
			this.vScrollBar19.Visible = false;
			// 
			// changeModeComboBox17
			// 
			this.changeModeComboBox17.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.changeModeComboBox17.FormattingEnabled = true;
			this.changeModeComboBox17.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.changeModeComboBox17.Location = new System.Drawing.Point(127, 295);
			this.changeModeComboBox17.Name = "changeModeComboBox17";
			this.changeModeComboBox17.Size = new System.Drawing.Size(60, 23);
			this.changeModeComboBox17.TabIndex = 12;
			this.changeModeComboBox17.Visible = false;
			this.changeModeComboBox17.SelectedIndexChanged += new System.EventHandler(this.changeModeComboBox_SelectedIndexChanged);
			// 
			// numericUpDown27
			// 
			this.numericUpDown27.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown27.Location = new System.Drawing.Point(979, 265);
			this.numericUpDown27.Name = "numericUpDown27";
			this.numericUpDown27.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown27.TabIndex = 11;
			this.numericUpDown27.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown59
			// 
			this.numericUpDown59.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown59.Location = new System.Drawing.Point(982, 326);
			this.numericUpDown59.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown59.Name = "numericUpDown59";
			this.numericUpDown59.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown59.TabIndex = 11;
			this.numericUpDown59.Visible = false;
			this.numericUpDown59.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// vScrollBar20
			// 
			this.vScrollBar20.Location = new System.Drawing.Point(400, 52);
			this.vScrollBar20.Maximum = 264;
			this.vScrollBar20.Name = "vScrollBar20";
			this.vScrollBar20.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar20.TabIndex = 0;
			this.vScrollBar20.Visible = false;
			// 
			// numericUpDown26
			// 
			this.numericUpDown26.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown26.Location = new System.Drawing.Point(897, 265);
			this.numericUpDown26.Name = "numericUpDown26";
			this.numericUpDown26.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown26.TabIndex = 11;
			this.numericUpDown26.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown58
			// 
			this.numericUpDown58.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown58.Location = new System.Drawing.Point(897, 326);
			this.numericUpDown58.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown58.Name = "numericUpDown58";
			this.numericUpDown58.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown58.TabIndex = 11;
			this.numericUpDown58.Visible = false;
			this.numericUpDown58.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// label30
			// 
			this.label30.Font = new System.Drawing.Font("宋体", 8F);
			this.label30.Location = new System.Drawing.Point(1226, 52);
			this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(19, 203);
			this.label30.TabIndex = 10;
			this.label30.Text = "总调光1";
			this.label30.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label30.Visible = false;
			// 
			// numericUpDown25
			// 
			this.numericUpDown25.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown25.Location = new System.Drawing.Point(812, 265);
			this.numericUpDown25.Name = "numericUpDown25";
			this.numericUpDown25.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown25.TabIndex = 11;
			this.numericUpDown25.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown57
			// 
			this.numericUpDown57.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown57.Location = new System.Drawing.Point(812, 326);
			this.numericUpDown57.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown57.Name = "numericUpDown57";
			this.numericUpDown57.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown57.TabIndex = 11;
			this.numericUpDown57.Visible = false;
			this.numericUpDown57.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// vScrollBar21
			// 
			this.vScrollBar21.Location = new System.Drawing.Point(485, 52);
			this.vScrollBar21.Maximum = 264;
			this.vScrollBar21.Name = "vScrollBar21";
			this.vScrollBar21.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar21.TabIndex = 0;
			this.vScrollBar21.Visible = false;
			// 
			// numericUpDown24
			// 
			this.numericUpDown24.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown24.Location = new System.Drawing.Point(727, 265);
			this.numericUpDown24.Name = "numericUpDown24";
			this.numericUpDown24.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown24.TabIndex = 11;
			this.numericUpDown24.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown56
			// 
			this.numericUpDown56.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown56.Location = new System.Drawing.Point(727, 326);
			this.numericUpDown56.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown56.Name = "numericUpDown56";
			this.numericUpDown56.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown56.TabIndex = 11;
			this.numericUpDown56.Visible = false;
			this.numericUpDown56.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// vScrollBar22
			// 
			this.vScrollBar22.Location = new System.Drawing.Point(570, 52);
			this.vScrollBar22.Maximum = 264;
			this.vScrollBar22.Name = "vScrollBar22";
			this.vScrollBar22.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar22.TabIndex = 0;
			this.vScrollBar22.Visible = false;
			// 
			// numericUpDown23
			// 
			this.numericUpDown23.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown23.Location = new System.Drawing.Point(642, 265);
			this.numericUpDown23.Name = "numericUpDown23";
			this.numericUpDown23.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown23.TabIndex = 11;
			this.numericUpDown23.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown55
			// 
			this.numericUpDown55.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown55.Location = new System.Drawing.Point(642, 326);
			this.numericUpDown55.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown55.Name = "numericUpDown55";
			this.numericUpDown55.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown55.TabIndex = 11;
			this.numericUpDown55.Visible = false;
			this.numericUpDown55.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// label29
			// 
			this.label29.Font = new System.Drawing.Font("宋体", 8F);
			this.label29.Location = new System.Drawing.Point(1140, 52);
			this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(19, 203);
			this.label29.TabIndex = 10;
			this.label29.Text = "总调光1";
			this.label29.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label29.Visible = false;
			// 
			// numericUpDown22
			// 
			this.numericUpDown22.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown22.Location = new System.Drawing.Point(557, 265);
			this.numericUpDown22.Name = "numericUpDown22";
			this.numericUpDown22.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown22.TabIndex = 11;
			this.numericUpDown22.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown54
			// 
			this.numericUpDown54.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown54.Location = new System.Drawing.Point(557, 326);
			this.numericUpDown54.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown54.Name = "numericUpDown54";
			this.numericUpDown54.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown54.TabIndex = 11;
			this.numericUpDown54.Visible = false;
			this.numericUpDown54.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// vScrollBar23
			// 
			this.vScrollBar23.Location = new System.Drawing.Point(655, 52);
			this.vScrollBar23.Maximum = 264;
			this.vScrollBar23.Name = "vScrollBar23";
			this.vScrollBar23.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar23.TabIndex = 0;
			this.vScrollBar23.Visible = false;
			// 
			// numericUpDown21
			// 
			this.numericUpDown21.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown21.Location = new System.Drawing.Point(472, 265);
			this.numericUpDown21.Name = "numericUpDown21";
			this.numericUpDown21.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown21.TabIndex = 11;
			this.numericUpDown21.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown53
			// 
			this.numericUpDown53.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown53.Location = new System.Drawing.Point(472, 326);
			this.numericUpDown53.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown53.Name = "numericUpDown53";
			this.numericUpDown53.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown53.TabIndex = 11;
			this.numericUpDown53.Visible = false;
			this.numericUpDown53.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// vScrollBar24
			// 
			this.vScrollBar24.Location = new System.Drawing.Point(740, 52);
			this.vScrollBar24.Maximum = 264;
			this.vScrollBar24.Name = "vScrollBar24";
			this.vScrollBar24.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar24.TabIndex = 0;
			this.vScrollBar24.Visible = false;
			// 
			// numericUpDown20
			// 
			this.numericUpDown20.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown20.Location = new System.Drawing.Point(387, 265);
			this.numericUpDown20.Name = "numericUpDown20";
			this.numericUpDown20.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown20.TabIndex = 11;
			this.numericUpDown20.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown52
			// 
			this.numericUpDown52.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown52.Location = new System.Drawing.Point(387, 326);
			this.numericUpDown52.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown52.Name = "numericUpDown52";
			this.numericUpDown52.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown52.TabIndex = 11;
			this.numericUpDown52.Visible = false;
			this.numericUpDown52.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// vScrollBar25
			// 
			this.vScrollBar25.Location = new System.Drawing.Point(825, 52);
			this.vScrollBar25.Maximum = 264;
			this.vScrollBar25.Name = "vScrollBar25";
			this.vScrollBar25.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar25.TabIndex = 0;
			this.vScrollBar25.Visible = false;
			// 
			// numericUpDown19
			// 
			this.numericUpDown19.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown19.Location = new System.Drawing.Point(302, 265);
			this.numericUpDown19.Name = "numericUpDown19";
			this.numericUpDown19.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown19.TabIndex = 11;
			this.numericUpDown19.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown51
			// 
			this.numericUpDown51.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown51.Location = new System.Drawing.Point(302, 326);
			this.numericUpDown51.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown51.Name = "numericUpDown51";
			this.numericUpDown51.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown51.TabIndex = 11;
			this.numericUpDown51.Visible = false;
			this.numericUpDown51.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// label28
			// 
			this.label28.Font = new System.Drawing.Font("宋体", 8F);
			this.label28.Location = new System.Drawing.Point(1054, 52);
			this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(24, 203);
			this.label28.TabIndex = 10;
			this.label28.Text = "总调光1";
			this.label28.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label28.Visible = false;
			// 
			// numericUpDown18
			// 
			this.numericUpDown18.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown18.Location = new System.Drawing.Point(217, 265);
			this.numericUpDown18.Name = "numericUpDown18";
			this.numericUpDown18.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown18.TabIndex = 11;
			this.numericUpDown18.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown50
			// 
			this.numericUpDown50.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown50.Location = new System.Drawing.Point(217, 326);
			this.numericUpDown50.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown50.Name = "numericUpDown50";
			this.numericUpDown50.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown50.TabIndex = 11;
			this.numericUpDown50.Visible = false;
			this.numericUpDown50.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// vScrollBar26
			// 
			this.vScrollBar26.Location = new System.Drawing.Point(910, 52);
			this.vScrollBar26.Maximum = 264;
			this.vScrollBar26.Name = "vScrollBar26";
			this.vScrollBar26.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar26.TabIndex = 0;
			this.vScrollBar26.Visible = false;
			// 
			// numericUpDown17
			// 
			this.numericUpDown17.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown17.Location = new System.Drawing.Point(132, 265);
			this.numericUpDown17.Name = "numericUpDown17";
			this.numericUpDown17.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown17.TabIndex = 11;
			this.numericUpDown17.ValueChanged += new System.EventHandler(this.valueNumericUpDown_ValueChanged);
			// 
			// numericUpDown49
			// 
			this.numericUpDown49.Font = new System.Drawing.Font("新宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.numericUpDown49.Location = new System.Drawing.Point(132, 326);
			this.numericUpDown49.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numericUpDown49.Name = "numericUpDown49";
			this.numericUpDown49.Size = new System.Drawing.Size(50, 22);
			this.numericUpDown49.TabIndex = 11;
			this.numericUpDown49.Visible = false;
			this.numericUpDown49.ValueChanged += new System.EventHandler(this.stepNumericUpDown_ValueChanged);
			// 
			// vScrollBar27
			// 
			this.vScrollBar27.Location = new System.Drawing.Point(995, 52);
			this.vScrollBar27.Maximum = 264;
			this.vScrollBar27.Name = "vScrollBar27";
			this.vScrollBar27.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar27.TabIndex = 0;
			this.vScrollBar27.Visible = false;
			// 
			// vScrollBar28
			// 
			this.vScrollBar28.Location = new System.Drawing.Point(1080, 52);
			this.vScrollBar28.Maximum = 264;
			this.vScrollBar28.Name = "vScrollBar28";
			this.vScrollBar28.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar28.TabIndex = 0;
			this.vScrollBar28.Visible = false;
			// 
			// label27
			// 
			this.label27.Font = new System.Drawing.Font("宋体", 8F);
			this.label27.Location = new System.Drawing.Point(970, 52);
			this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(24, 203);
			this.label27.TabIndex = 10;
			this.label27.Text = "总调光1";
			this.label27.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label27.Visible = false;
			// 
			// vScrollBar29
			// 
			this.vScrollBar29.Location = new System.Drawing.Point(1165, 52);
			this.vScrollBar29.Maximum = 264;
			this.vScrollBar29.Name = "vScrollBar29";
			this.vScrollBar29.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar29.TabIndex = 0;
			this.vScrollBar29.Visible = false;
			// 
			// vScrollBar30
			// 
			this.vScrollBar30.Location = new System.Drawing.Point(1250, 52);
			this.vScrollBar30.Maximum = 264;
			this.vScrollBar30.Name = "vScrollBar30";
			this.vScrollBar30.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar30.TabIndex = 0;
			this.vScrollBar30.Visible = false;
			// 
			// vScrollBar31
			// 
			this.vScrollBar31.Location = new System.Drawing.Point(1338, 52);
			this.vScrollBar31.Maximum = 264;
			this.vScrollBar31.Name = "vScrollBar31";
			this.vScrollBar31.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar31.TabIndex = 0;
			this.vScrollBar31.Visible = false;
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("宋体", 8F);
			this.label26.Location = new System.Drawing.Point(885, 52);
			this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(24, 203);
			this.label26.TabIndex = 10;
			this.label26.Text = "总调光1";
			this.label26.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label26.Visible = false;
			// 
			// vScrollBar32
			// 
			this.vScrollBar32.Location = new System.Drawing.Point(1420, 52);
			this.vScrollBar32.Maximum = 264;
			this.vScrollBar32.Name = "vScrollBar32";
			this.vScrollBar32.Size = new System.Drawing.Size(24, 203);
			this.vScrollBar32.TabIndex = 0;
			this.vScrollBar32.Visible = false;
			// 
			// label17
			// 
			this.label17.Font = new System.Drawing.Font("宋体", 8F);
			this.label17.Location = new System.Drawing.Point(126, 52);
			this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(19, 203);
			this.label17.TabIndex = 10;
			this.label17.Text = "总调光1";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label17.Visible = false;
			this.label17.MouseHover += new System.EventHandler(this.tdLabel_MouseEnter);
			// 
			// label22
			// 
			this.label22.Font = new System.Drawing.Font("宋体", 8F);
			this.label22.Location = new System.Drawing.Point(542, 52);
			this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(24, 203);
			this.label22.TabIndex = 10;
			this.label22.Text = "总调光1";
			this.label22.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label22.Visible = false;
			// 
			// label25
			// 
			this.label25.Font = new System.Drawing.Font("宋体", 8F);
			this.label25.Location = new System.Drawing.Point(800, 52);
			this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(24, 203);
			this.label25.TabIndex = 10;
			this.label25.Text = "总调光1";
			this.label25.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label25.Visible = false;
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("宋体", 8F);
			this.label20.Location = new System.Drawing.Point(376, 52);
			this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(24, 203);
			this.label20.TabIndex = 10;
			this.label20.Text = "总调光1";
			this.label20.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label20.Visible = false;
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("宋体", 8F);
			this.label19.Location = new System.Drawing.Point(291, 52);
			this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(24, 203);
			this.label19.TabIndex = 10;
			this.label19.Text = "总调光1";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label19.Visible = false;
			// 
			// label21
			// 
			this.label21.Font = new System.Drawing.Font("宋体", 8F);
			this.label21.Location = new System.Drawing.Point(457, 52);
			this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(24, 203);
			this.label21.TabIndex = 10;
			this.label21.Text = "总调光1";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label21.Visible = false;
			// 
			// label23
			// 
			this.label23.Font = new System.Drawing.Font("宋体", 8F);
			this.label23.Location = new System.Drawing.Point(632, 52);
			this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(24, 203);
			this.label23.TabIndex = 10;
			this.label23.Text = "总调光1";
			this.label23.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label23.Visible = false;
			// 
			// label24
			// 
			this.label24.Font = new System.Drawing.Font("宋体", 8F);
			this.label24.Location = new System.Drawing.Point(716, 52);
			this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(24, 203);
			this.label24.TabIndex = 10;
			this.label24.Text = "总调光1";
			this.label24.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label24.Visible = false;
			// 
			// label18
			// 
			this.label18.Font = new System.Drawing.Font("宋体", 8F);
			this.label18.Location = new System.Drawing.Point(206, 52);
			this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(24, 203);
			this.label18.TabIndex = 10;
			this.label18.Text = "总调光1";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label18.Visible = false;
			// 
			// cmComboBox
			// 
			this.cmComboBox.FormattingEnabled = true;
			this.cmComboBox.Items.AddRange(new object[] {
            "跳变",
            "渐变"});
			this.cmComboBox.Location = new System.Drawing.Point(64, 401);
			this.cmComboBox.Name = "cmComboBox";
			this.cmComboBox.Size = new System.Drawing.Size(76, 23);
			this.cmComboBox.TabIndex = 19;
			// 
			// commonValueNumericUpDown
			// 
			this.commonValueNumericUpDown.Location = new System.Drawing.Point(64, 346);
			this.commonValueNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.commonValueNumericUpDown.Name = "commonValueNumericUpDown";
			this.commonValueNumericUpDown.Size = new System.Drawing.Size(76, 25);
			this.commonValueNumericUpDown.TabIndex = 18;
			this.commonValueNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// stNumericUpDown
			// 
			this.stNumericUpDown.Location = new System.Drawing.Point(64, 449);
			this.stNumericUpDown.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.stNumericUpDown.Name = "stNumericUpDown";
			this.stNumericUpDown.Size = new System.Drawing.Size(76, 25);
			this.stNumericUpDown.TabIndex = 18;
			this.stNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// changeModeButton
			// 
			this.changeModeButton.Location = new System.Drawing.Point(165, 397);
			this.changeModeButton.Name = "changeModeButton";
			this.changeModeButton.Size = new System.Drawing.Size(136, 31);
			this.changeModeButton.TabIndex = 17;
			this.changeModeButton.Text = "统一跳渐变";
			this.changeModeButton.UseVisualStyleBackColor = true;
			this.changeModeButton.Click += new System.EventHandler(this.changeModeButton_Click);
			// 
			// commonValueButton
			// 
			this.commonValueButton.Location = new System.Drawing.Point(165, 343);
			this.commonValueButton.Name = "commonValueButton";
			this.commonValueButton.Size = new System.Drawing.Size(136, 31);
			this.commonValueButton.TabIndex = 17;
			this.commonValueButton.Text = "统一通道值";
			this.commonValueButton.UseVisualStyleBackColor = true;
			this.commonValueButton.Click += new System.EventHandler(this.commonValueButton_Click);
			// 
			// steptimeSetButton
			// 
			this.steptimeSetButton.Location = new System.Drawing.Point(165, 446);
			this.steptimeSetButton.Name = "steptimeSetButton";
			this.steptimeSetButton.Size = new System.Drawing.Size(136, 31);
			this.steptimeSetButton.TabIndex = 17;
			this.steptimeSetButton.Text = "统一步时间";
			this.steptimeSetButton.UseVisualStyleBackColor = true;
			this.steptimeSetButton.Click += new System.EventHandler(this.steptimeSetButton_Click);
			// 
			// modeChooseLabel
			// 
			this.modeChooseLabel.AutoSize = true;
			this.modeChooseLabel.Location = new System.Drawing.Point(614, 27);
			this.modeChooseLabel.Name = "modeChooseLabel";
			this.modeChooseLabel.Size = new System.Drawing.Size(82, 15);
			this.modeChooseLabel.TabIndex = 16;
			this.modeChooseLabel.Text = "选择模式：";
			// 
			// frameChooseLabel
			// 
			this.frameChooseLabel.AutoSize = true;
			this.frameChooseLabel.Location = new System.Drawing.Point(376, 27);
			this.frameChooseLabel.Name = "frameChooseLabel";
			this.frameChooseLabel.Size = new System.Drawing.Size(82, 15);
			this.frameChooseLabel.TabIndex = 16;
			this.frameChooseLabel.Text = "选择场景：";
			// 
			// stepLabel
			// 
			this.stepLabel.AutoSize = true;
			this.stepLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.stepLabel.Location = new System.Drawing.Point(624, 88);
			this.stepLabel.Name = "stepLabel";
			this.stepLabel.Size = new System.Drawing.Size(39, 20);
			this.stepLabel.TabIndex = 15;
			this.stepLabel.Text = "0/0";
			// 
			// nextStepButton
			// 
			this.nextStepButton.Location = new System.Drawing.Point(699, 82);
			this.nextStepButton.Name = "nextStepButton";
			this.nextStepButton.Size = new System.Drawing.Size(89, 32);
			this.nextStepButton.TabIndex = 14;
			this.nextStepButton.Text = "下一步";
			this.nextStepButton.UseVisualStyleBackColor = true;
			this.nextStepButton.Click += new System.EventHandler(this.nextStepButton_Click);
			// 
			// deleteStepButton
			// 
			this.deleteStepButton.Location = new System.Drawing.Point(1059, 82);
			this.deleteStepButton.Name = "deleteStepButton";
			this.deleteStepButton.Size = new System.Drawing.Size(89, 32);
			this.deleteStepButton.TabIndex = 14;
			this.deleteStepButton.Text = "删除步";
			this.deleteStepButton.UseVisualStyleBackColor = true;
			this.deleteStepButton.Click += new System.EventHandler(this.deleteStepButton_Click);
			// 
			// backStepButton
			// 
			this.backStepButton.Location = new System.Drawing.Point(496, 82);
			this.backStepButton.Name = "backStepButton";
			this.backStepButton.Size = new System.Drawing.Size(89, 32);
			this.backStepButton.TabIndex = 13;
			this.backStepButton.Text = "上一步";
			this.backStepButton.UseVisualStyleBackColor = true;
			this.backStepButton.Click += new System.EventHandler(this.backStepButton_Click);
			// 
			// insertBeforeStepButton
			// 
			this.insertBeforeStepButton.Location = new System.Drawing.Point(376, 82);
			this.insertBeforeStepButton.Name = "insertBeforeStepButton";
			this.insertBeforeStepButton.Size = new System.Drawing.Size(89, 32);
			this.insertBeforeStepButton.TabIndex = 14;
			this.insertBeforeStepButton.Text = "前插入步";
			this.insertBeforeStepButton.UseVisualStyleBackColor = true;
			this.insertBeforeStepButton.Click += new System.EventHandler(this.insertStepButton_Click);
			// 
			// insertAfterStepButton
			// 
			this.insertAfterStepButton.Location = new System.Drawing.Point(819, 82);
			this.insertAfterStepButton.Name = "insertAfterStepButton";
			this.insertAfterStepButton.Size = new System.Drawing.Size(89, 32);
			this.insertAfterStepButton.TabIndex = 14;
			this.insertAfterStepButton.Text = "后插入步";
			this.insertAfterStepButton.UseVisualStyleBackColor = true;
			this.insertAfterStepButton.Click += new System.EventHandler(this.insertStepButton_Click);
			// 
			// initButton
			// 
			this.initButton.Location = new System.Drawing.Point(184, 286);
			this.initButton.Name = "initButton";
			this.initButton.Size = new System.Drawing.Size(117, 31);
			this.initButton.TabIndex = 13;
			this.initButton.Text = "设为初始值";
			this.initButton.UseVisualStyleBackColor = true;
			this.initButton.Click += new System.EventHandler(this.initButton_Click);
			// 
			// zeroButton
			// 
			this.zeroButton.Location = new System.Drawing.Point(64, 286);
			this.zeroButton.Name = "zeroButton";
			this.zeroButton.Size = new System.Drawing.Size(114, 31);
			this.zeroButton.TabIndex = 13;
			this.zeroButton.Text = "全部归零";
			this.zeroButton.UseVisualStyleBackColor = true;
			this.zeroButton.Click += new System.EventHandler(this.zeroButton_Click);
			// 
			// pasteStepButton
			// 
			this.pasteStepButton.Location = new System.Drawing.Point(1299, 82);
			this.pasteStepButton.Name = "pasteStepButton";
			this.pasteStepButton.Size = new System.Drawing.Size(89, 32);
			this.pasteStepButton.TabIndex = 13;
			this.pasteStepButton.Text = "粘贴步";
			this.pasteStepButton.UseVisualStyleBackColor = true;
			this.pasteStepButton.Click += new System.EventHandler(this.pasteStepButton_Click);
			// 
			// copyStepButton
			// 
			this.copyStepButton.Location = new System.Drawing.Point(1179, 82);
			this.copyStepButton.Name = "copyStepButton";
			this.copyStepButton.Size = new System.Drawing.Size(89, 32);
			this.copyStepButton.TabIndex = 13;
			this.copyStepButton.Text = "复制步";
			this.copyStepButton.UseVisualStyleBackColor = true;
			this.copyStepButton.Click += new System.EventHandler(this.copyStepButton_Click);
			// 
			// addStepButton
			// 
			this.addStepButton.Location = new System.Drawing.Point(939, 82);
			this.addStepButton.Name = "addStepButton";
			this.addStepButton.Size = new System.Drawing.Size(89, 32);
			this.addStepButton.TabIndex = 13;
			this.addStepButton.Text = "追加步";
			this.addStepButton.UseVisualStyleBackColor = true;
			this.addStepButton.Click += new System.EventHandler(this.addStepButton_Click);
			// 
			// lightValueLabel
			// 
			this.lightValueLabel.AutoSize = true;
			this.lightValueLabel.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightValueLabel.Location = new System.Drawing.Point(109, 23);
			this.lightValueLabel.Name = "lightValueLabel";
			this.lightValueLabel.Size = new System.Drawing.Size(70, 15);
			this.lightValueLabel.TabIndex = 12;
			this.lightValueLabel.Text = "       ";
			// 
			// lightLabel
			// 
			this.lightLabel.AutoSize = true;
			this.lightLabel.Location = new System.Drawing.Point(34, 24);
			this.lightLabel.Name = "lightLabel";
			this.lightLabel.Size = new System.Drawing.Size(82, 15);
			this.lightLabel.TabIndex = 11;
			this.lightLabel.Text = "当前灯具：";
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
			this.frameComboBox.Location = new System.Drawing.Point(464, 23);
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
			this.modeComboBox.Location = new System.Drawing.Point(702, 23);
			this.modeComboBox.Name = "modeComboBox";
			this.modeComboBox.Size = new System.Drawing.Size(104, 23);
			this.modeComboBox.TabIndex = 0;
			this.modeComboBox.SelectedIndexChanged += new System.EventHandler(this.modeComboBox_SelectedIndexChanged);
			// 
			// test1Button
			// 
			this.test1Button.Location = new System.Drawing.Point(28, 19);
			this.test1Button.Name = "test1Button";
			this.test1Button.Size = new System.Drawing.Size(89, 22);
			this.test1Button.TabIndex = 13;
			this.test1Button.Text = "TEST1";
			this.test1Button.UseVisualStyleBackColor = true;
			this.test1Button.Click += new System.EventHandler(this.newTestButton_Click);
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightLibraryToolStripMenuItem,
            this.hardwareSetToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.lightsEditToolStripMenuItem,
            this.ExitToolStripMenuItem,
            this.globalSetToolStripMenuItem,
            this.ymSetToolStripMenuItem,
            this.networkSetToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(1915, 28);
			this.mainMenuStrip.TabIndex = 17;
			this.mainMenuStrip.Text = "menuStrip1";
			// 
			// lightLibraryToolStripMenuItem
			// 
			this.lightLibraryToolStripMenuItem.Name = "lightLibraryToolStripMenuItem";
			this.lightLibraryToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
			this.lightLibraryToolStripMenuItem.Text = "灯库编辑";
			this.lightLibraryToolStripMenuItem.Click += new System.EventHandler(this.lightLibraryToolStripMenuItem_Click);
			// 
			// hardwareSetToolStripMenuItem
			// 
			this.hardwareSetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hardwareSetNewToolStripMenuItem,
            this.hardwareSetOpenToolStripMenuItem});
			this.hardwareSetToolStripMenuItem.Name = "hardwareSetToolStripMenuItem";
			this.hardwareSetToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
			this.hardwareSetToolStripMenuItem.Text = "硬件设置";
			// 
			// hardwareSetNewToolStripMenuItem
			// 
			this.hardwareSetNewToolStripMenuItem.Name = "hardwareSetNewToolStripMenuItem";
			this.hardwareSetNewToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
			this.hardwareSetNewToolStripMenuItem.Text = "新建配置";
			this.hardwareSetNewToolStripMenuItem.Click += new System.EventHandler(this.hardwareSetNewToolStripMenuItem_Click);
			// 
			// hardwareSetOpenToolStripMenuItem
			// 
			this.hardwareSetOpenToolStripMenuItem.Name = "hardwareSetOpenToolStripMenuItem";
			this.hardwareSetOpenToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
			this.hardwareSetOpenToolStripMenuItem.Text = "打开配置";
			this.hardwareSetOpenToolStripMenuItem.Click += new System.EventHandler(this.hardwareSetOpenToolStripMenuItem_Click);
			// 
			// updateToolStripMenuItem
			// 
			this.updateToolStripMenuItem.Enabled = false;
			this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
			this.updateToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
			this.updateToolStripMenuItem.Text = "在线升级";
			this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click_1);
			// 
			// lightsEditToolStripMenuItem
			// 
			this.lightsEditToolStripMenuItem.Enabled = false;
			this.lightsEditToolStripMenuItem.Name = "lightsEditToolStripMenuItem";
			this.lightsEditToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
			this.lightsEditToolStripMenuItem.Text = "灯具编辑";
			this.lightsEditToolStripMenuItem.Click += new System.EventHandler(this.lightsEditToolStripMenuItem1_Click);
			// 
			// ExitToolStripMenuItem
			// 
			this.ExitToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			this.ExitToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.ExitToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
			this.ExitToolStripMenuItem.Text = "退出程序";
			this.ExitToolStripMenuItem.Click += new System.EventHandler(this.exitButton_Click);
			// 
			// globalSetToolStripMenuItem
			// 
			this.globalSetToolStripMenuItem.Enabled = false;
			this.globalSetToolStripMenuItem.Name = "globalSetToolStripMenuItem";
			this.globalSetToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
			this.globalSetToolStripMenuItem.Text = "全局配置";
			this.globalSetToolStripMenuItem.Click += new System.EventHandler(this.globleSetButton_Click);
			// 
			// ymSetToolStripMenuItem
			// 
			this.ymSetToolStripMenuItem.Enabled = false;
			this.ymSetToolStripMenuItem.Name = "ymSetToolStripMenuItem";
			this.ymSetToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
			this.ymSetToolStripMenuItem.Text = "摇麦设置";
			this.ymSetToolStripMenuItem.Click += new System.EventHandler(this.ymSetToolStripMenuItem_Click);
			// 
			// networkSetToolStripMenuItem
			// 
			this.networkSetToolStripMenuItem.Enabled = false;
			this.networkSetToolStripMenuItem.Name = "networkSetToolStripMenuItem";
			this.networkSetToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
			this.networkSetToolStripMenuItem.Text = "网络设置";
			this.networkSetToolStripMenuItem.Click += new System.EventHandler(this.NetworkSetToolStripMenuItem_Click);
			// 
			// previewButton
			// 
			this.previewButton.Location = new System.Drawing.Point(1338, 57);
			this.previewButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.previewButton.Name = "previewButton";
			this.previewButton.Size = new System.Drawing.Size(92, 65);
			this.previewButton.TabIndex = 5;
			this.previewButton.Text = "预览效果";
			this.previewButton.UseVisualStyleBackColor = true;
			this.previewButton.Visible = false;
			this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
			// 
			// stopReviewButton
			// 
			this.stopReviewButton.Location = new System.Drawing.Point(1534, 57);
			this.stopReviewButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.stopReviewButton.Name = "stopReviewButton";
			this.stopReviewButton.Size = new System.Drawing.Size(92, 65);
			this.stopReviewButton.TabIndex = 5;
			this.stopReviewButton.Text = "结束预览";
			this.stopReviewButton.UseVisualStyleBackColor = true;
			this.stopReviewButton.Visible = false;
			this.stopReviewButton.Click += new System.EventHandler(this.stopReviewButton_Click);
			// 
			// oneLightStepButton
			// 
			this.oneLightStepButton.Location = new System.Drawing.Point(1240, 83);
			this.oneLightStepButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.oneLightStepButton.Name = "oneLightStepButton";
			this.oneLightStepButton.Size = new System.Drawing.Size(92, 39);
			this.oneLightStepButton.TabIndex = 5;
			this.oneLightStepButton.Text = "单灯单步";
			this.oneLightStepButton.UseVisualStyleBackColor = true;
			this.oneLightStepButton.Visible = false;
			this.oneLightStepButton.Click += new System.EventHandler(this.oneLightStepButton_Click);
			// 
			// realTimeCheckBox
			// 
			this.realTimeCheckBox.AutoSize = true;
			this.realTimeCheckBox.Location = new System.Drawing.Point(1242, 61);
			this.realTimeCheckBox.Name = "realTimeCheckBox";
			this.realTimeCheckBox.Size = new System.Drawing.Size(89, 19);
			this.realTimeCheckBox.TabIndex = 18;
			this.realTimeCheckBox.Text = "实时调试";
			this.realTimeCheckBox.UseVisualStyleBackColor = true;
			this.realTimeCheckBox.Visible = false;
			this.realTimeCheckBox.CheckedChanged += new System.EventHandler(this.realTimeCheckBox_CheckedChanged);
			// 
			// soundButton
			// 
			this.soundButton.Location = new System.Drawing.Point(1436, 57);
			this.soundButton.Name = "soundButton";
			this.soundButton.Size = new System.Drawing.Size(92, 65);
			this.soundButton.TabIndex = 13;
			this.soundButton.Text = "触发音频";
			this.soundButton.UseVisualStyleBackColor = true;
			this.soundButton.Visible = false;
			this.soundButton.Click += new System.EventHandler(this.soundButton_Click);
			// 
			// connectButton
			// 
			this.connectButton.BackColor = System.Drawing.SystemColors.Control;
			this.connectButton.Enabled = false;
			this.connectButton.Location = new System.Drawing.Point(1137, 57);
			this.connectButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.connectButton.Name = "connectButton";
			this.connectButton.Size = new System.Drawing.Size(93, 65);
			this.connectButton.TabIndex = 5;
			this.connectButton.Text = "连接设备";
			this.connectButton.UseVisualStyleBackColor = false;
			this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// testGroupBox
			// 
			this.testGroupBox.BackColor = System.Drawing.SystemColors.Info;
			this.testGroupBox.Controls.Add(this.test4Button);
			this.testGroupBox.Controls.Add(this.test3Button);
			this.testGroupBox.Controls.Add(this.test2Button);
			this.testGroupBox.Controls.Add(this.test1Button);
			this.testGroupBox.Location = new System.Drawing.Point(212, 43);
			this.testGroupBox.Name = "testGroupBox";
			this.testGroupBox.Size = new System.Drawing.Size(253, 79);
			this.testGroupBox.TabIndex = 19;
			this.testGroupBox.TabStop = false;
			this.testGroupBox.Visible = false;
			// 
			// test4Button
			// 
			this.test4Button.Location = new System.Drawing.Point(139, 45);
			this.test4Button.Name = "test4Button";
			this.test4Button.Size = new System.Drawing.Size(89, 22);
			this.test4Button.TabIndex = 13;
			this.test4Button.Text = "TEST4";
			this.test4Button.UseVisualStyleBackColor = true;
			this.test4Button.Click += new System.EventHandler(this.newTestButton_Click);
			// 
			// test3Button
			// 
			this.test3Button.Location = new System.Drawing.Point(28, 45);
			this.test3Button.Name = "test3Button";
			this.test3Button.Size = new System.Drawing.Size(89, 22);
			this.test3Button.TabIndex = 13;
			this.test3Button.Text = "TEST3";
			this.test3Button.UseVisualStyleBackColor = true;
			this.test3Button.Click += new System.EventHandler(this.newTestButton_Click);
			// 
			// test2Button
			// 
			this.test2Button.Location = new System.Drawing.Point(139, 19);
			this.test2Button.Name = "test2Button";
			this.test2Button.Size = new System.Drawing.Size(89, 22);
			this.test2Button.TabIndex = 13;
			this.test2Button.Text = "TEST2";
			this.test2Button.UseVisualStyleBackColor = true;
			this.test2Button.Click += new System.EventHandler(this.newTestButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(1915, 881);
			this.Controls.Add(this.testGroupBox);
			this.Controls.Add(this.realTimeCheckBox);
			this.Controls.Add(this.tongdaoGroupBox);
			this.Controls.Add(this.lightsListView);
			this.Controls.Add(this.oneKeyButton);
			this.Controls.Add(this.openFileButton);
			this.Controls.Add(this.newFileButton);
			this.Controls.Add(this.openComButton);
			this.Controls.Add(this.comComboBox);
			this.Controls.Add(this.stopReviewButton);
			this.Controls.Add(this.oneLightStepButton);
			this.Controls.Add(this.connectButton);
			this.Controls.Add(this.previewButton);
			this.Controls.Add(this.saveAsButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.mainMenuStrip);
			this.Controls.Add(this.soundButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenuStrip;
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "智控配置";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tongdaoGroupBox.ResumeLayout(false);
			this.tongdaoGroupBox.PerformLayout();
			this.tongdaoPanel.ResumeLayout(false);
			this.tongdaoGroupBox1.ResumeLayout(false);
			this.tongdaoGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown48)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown16)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown47)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown15)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown46)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown14)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown45)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown13)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown44)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown43)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown42)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown10)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown41)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown40)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown39)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown38)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown37)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown36)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown35)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown34)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown33)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.tongdaoGroupBox2.ResumeLayout(false);
			this.tongdaoGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown32)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown64)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown31)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown63)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown30)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown62)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown29)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown61)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown28)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown60)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown27)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown59)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown26)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown58)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown25)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown57)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown24)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown56)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown23)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown55)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown22)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown54)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown21)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown53)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown20)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown52)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown19)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown51)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown18)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown50)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown17)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown49)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.commonValueNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.stNumericUpDown)).EndInit();
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.testGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comComboBox;
		private System.Windows.Forms.Button openComButton;
		private System.Windows.Forms.Button newFileButton;
		private System.Windows.Forms.Button openFileButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button saveAsButton;
		private System.Windows.Forms.Button oneKeyButton;
		private System.Windows.Forms.SaveFileDialog projectSaveFileDialog;
		private Sunisoft.IrisSkin.SkinEngine skinEngine1;

		// 2019.5.23 : 添加listView 来存放加载的灯具列表
		private ListView lightsListView;
		public ListViewItem[] lightItems;
		private ColumnHeader lightType;
		private ImageList LargeImageList;
		private GroupBox tongdaoGroupBox;
		private ComboBox frameComboBox;
		private ComboBox modeComboBox;
		private GroupBox tongdaoGroupBox1;

		// 通道区
		private Label changeModeLabel;
		private Label tongdaoValueLabel;
		private Label stepTimeLabel;
		private Label changeModeLabel2;
		private Label tongdaoValueLabel2;
		private Label stepTimeLabel2;

		public Label[] labels = new Label[32];
		public VScrollBar[] vScrollBars = new VScrollBar[32];
		public NumericUpDown[] valueNumericUpDowns = new NumericUpDown[32];
		public NumericUpDown[] steptimeNumericUpDowns = new NumericUpDown[32];
		public ComboBox[] changeModeComboBoxes = new ComboBox[32];

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

		private NumericUpDown numericUpDown33;
		private NumericUpDown numericUpDown34;
		private NumericUpDown numericUpDown35;
		private NumericUpDown numericUpDown36;
		private NumericUpDown numericUpDown37;
		private NumericUpDown numericUpDown38;
		private NumericUpDown numericUpDown39;
		private NumericUpDown numericUpDown40;
		private NumericUpDown numericUpDown41;
		private NumericUpDown numericUpDown42;
		private NumericUpDown numericUpDown43;
		private NumericUpDown numericUpDown44;
		private NumericUpDown numericUpDown45;
		private NumericUpDown numericUpDown46;
		private NumericUpDown numericUpDown47;
		private NumericUpDown numericUpDown48;
		private NumericUpDown numericUpDown49;
		private NumericUpDown numericUpDown50;
		private NumericUpDown numericUpDown51;
		private NumericUpDown numericUpDown52;
		private NumericUpDown numericUpDown53;
		private NumericUpDown numericUpDown54;
		private NumericUpDown numericUpDown55;
		private NumericUpDown numericUpDown56;
		private NumericUpDown numericUpDown57;
		private NumericUpDown numericUpDown58;
		private NumericUpDown numericUpDown59;
		private NumericUpDown numericUpDown60;
		private NumericUpDown numericUpDown61;
		private NumericUpDown numericUpDown62;
		private NumericUpDown numericUpDown63;
		private NumericUpDown numericUpDown64;
		private ComboBox changeModeComboBox1;
		private ComboBox changeModeComboBox2;
		private ComboBox changeModeComboBox3;
		private ComboBox changeModeComboBox4;
		private ComboBox changeModeComboBox5;
		private ComboBox changeModeComboBox6;
		private ComboBox changeModeComboBox7;
		private ComboBox changeModeComboBox8;
		private ComboBox changeModeComboBox9;
		private ComboBox changeModeComboBox10;
		private ComboBox changeModeComboBox11;
		private ComboBox changeModeComboBox12;
		private ComboBox changeModeComboBox13;
		private ComboBox changeModeComboBox14;
		private ComboBox changeModeComboBox15;
		private ComboBox changeModeComboBox16;
		private ComboBox changeModeComboBox17;
		private ComboBox changeModeComboBox18;
		private ComboBox changeModeComboBox19;
		private ComboBox changeModeComboBox20;
		private ComboBox changeModeComboBox21;
		private ComboBox changeModeComboBox22;
		private ComboBox changeModeComboBox23;
		private ComboBox changeModeComboBox24;
		private ComboBox changeModeComboBox25;
		private ComboBox changeModeComboBox26;
		private ComboBox changeModeComboBox27;
		private ComboBox changeModeComboBox28;
		private ComboBox changeModeComboBox29;
		private ComboBox changeModeComboBox30;
		private ComboBox changeModeComboBox31;
		private ComboBox changeModeComboBox32;

		// 步数调节区
		private Label lightValueLabel;
		private Label lightLabel;
		private Button insertAfterStepButton;
		private Button addStepButton;
		private Button deleteStepButton;
		private Button nextStepButton;
		private Button backStepButton;


		private MenuStrip mainMenuStrip;
		private ToolStripMenuItem lightLibraryToolStripMenuItem;
		private ToolStripMenuItem lightsEditToolStripMenuItem;
		private ToolStripMenuItem ExitToolStripMenuItem;
		private ToolStripMenuItem globalSetToolStripMenuItem;

		// 一个记录所有Step的数组

		public StepWrapper[,] stepList = new StepWrapper[24, 2];
		private Label stepLabel;

		
		private Label modeChooseLabel;
		private Label frameChooseLabel;
		private Button pasteStepButton;
		private Button copyStepButton;
		private Button previewButton;
		private Button stopReviewButton;
		private ComboBox cmComboBox;
		private NumericUpDown stNumericUpDown;
		private Button changeModeButton;
		private Button steptimeSetButton;
		private Button oneLightStepButton;
		private CheckBox realTimeCheckBox;
		private Button test1Button;
		private Panel tongdaoPanel;
		private ToolStripMenuItem ymSetToolStripMenuItem;
		private ToolStripMenuItem networkSetToolStripMenuItem;
		private Button materialUseButton;
		private Button pasteLightButton;
		private Button materialSaveButton;
		private Button copyLightButton;
		private Button insertBeforeStepButton;
		private Button soundButton;
		private Button initButton;
		private Button zeroButton;
		private NumericUpDown commonValueNumericUpDown;
		private Button commonValueButton;
		private CheckBox addStepCheckBox;
		private ToolStripMenuItem updateToolStripMenuItem;
		private Button connectButton;
		private ToolStripMenuItem hardwareSetToolStripMenuItem;
		private ToolStripMenuItem hardwareSetNewToolStripMenuItem;
		private ToolStripMenuItem hardwareSetOpenToolStripMenuItem;
		private GroupBox testGroupBox;
		private Button test4Button;
		private Button test3Button;
		private Button test2Button;
	}
}

