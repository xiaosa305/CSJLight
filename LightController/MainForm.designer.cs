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
			this.tongdaoGroupBox2 = new System.Windows.Forms.GroupBox();
			this.valueLabel32 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.valueLabel31 = new System.Windows.Forms.Label();
			this.vScrollBar17 = new System.Windows.Forms.VScrollBar();
			this.valueLabel30 = new System.Windows.Forms.Label();
			this.valueLabel29 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.valueLabel28 = new System.Windows.Forms.Label();
			this.vScrollBar18 = new System.Windows.Forms.VScrollBar();
			this.valueLabel27 = new System.Windows.Forms.Label();
			this.vScrollBar19 = new System.Windows.Forms.VScrollBar();
			this.valueLabel26 = new System.Windows.Forms.Label();
			this.vScrollBar20 = new System.Windows.Forms.VScrollBar();
			this.valueLabel25 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.valueLabel24 = new System.Windows.Forms.Label();
			this.vScrollBar21 = new System.Windows.Forms.VScrollBar();
			this.valueLabel23 = new System.Windows.Forms.Label();
			this.vScrollBar22 = new System.Windows.Forms.VScrollBar();
			this.valueLabel22 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.valueLabel21 = new System.Windows.Forms.Label();
			this.vScrollBar23 = new System.Windows.Forms.VScrollBar();
			this.valueLabel20 = new System.Windows.Forms.Label();
			this.vScrollBar24 = new System.Windows.Forms.VScrollBar();
			this.valueLabel19 = new System.Windows.Forms.Label();
			this.vScrollBar25 = new System.Windows.Forms.VScrollBar();
			this.valueLabel18 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.valueLabel17 = new System.Windows.Forms.Label();
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
			this.valueLabel16 = new System.Windows.Forms.Label();
			this.valueLabel15 = new System.Windows.Forms.Label();
			this.valueLabel14 = new System.Windows.Forms.Label();
			this.valueLabel13 = new System.Windows.Forms.Label();
			this.valueLabel12 = new System.Windows.Forms.Label();
			this.valueLabel11 = new System.Windows.Forms.Label();
			this.valueLabel10 = new System.Windows.Forms.Label();
			this.valueLabel9 = new System.Windows.Forms.Label();
			this.valueLabel8 = new System.Windows.Forms.Label();
			this.valueLabel7 = new System.Windows.Forms.Label();
			this.valueLabel6 = new System.Windows.Forms.Label();
			this.valueLabel5 = new System.Windows.Forms.Label();
			this.valueLabel4 = new System.Windows.Forms.Label();
			this.valueLabel3 = new System.Windows.Forms.Label();
			this.valueLabel2 = new System.Windows.Forms.Label();
			this.valueLabel1 = new System.Windows.Forms.Label();
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
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.tongdaoGroupBox.SuspendLayout();
			this.tongdaoGroupBox2.SuspendLayout();
			this.tongdaoGroupBox1.SuspendLayout();
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
			this.tongdaoGroupBox.Controls.Add(this.tongdaoGroupBox2);
			this.tongdaoGroupBox.Controls.Add(this.tongdaoGroupBox1);
			this.tongdaoGroupBox.Controls.Add(this.comboBox3);
			this.tongdaoGroupBox.Controls.Add(this.comboBox2);
			this.tongdaoGroupBox.Controls.Add(this.comboBox1);
			this.tongdaoGroupBox.Location = new System.Drawing.Point(11, 221);
			this.tongdaoGroupBox.Name = "tongdaoGroupBox";
			this.tongdaoGroupBox.Size = new System.Drawing.Size(1430, 739);
			this.tongdaoGroupBox.TabIndex = 8;
			this.tongdaoGroupBox.TabStop = false;
			this.tongdaoGroupBox.Visible = false;
			// 
			// tongdaoGroupBox2
			// 
			this.tongdaoGroupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel32);
			this.tongdaoGroupBox2.Controls.Add(this.label32);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel31);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar17);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel30);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel29);
			this.tongdaoGroupBox2.Controls.Add(this.label31);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel28);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar18);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel27);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar19);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel26);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar20);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel25);
			this.tongdaoGroupBox2.Controls.Add(this.label30);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel24);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar21);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel23);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar22);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel22);
			this.tongdaoGroupBox2.Controls.Add(this.label29);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel21);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar23);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel20);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar24);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel19);
			this.tongdaoGroupBox2.Controls.Add(this.vScrollBar25);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel18);
			this.tongdaoGroupBox2.Controls.Add(this.label28);
			this.tongdaoGroupBox2.Controls.Add(this.valueLabel17);
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
			this.tongdaoGroupBox2.Location = new System.Drawing.Point(7, 399);
			this.tongdaoGroupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tongdaoGroupBox2.Name = "tongdaoGroupBox2";
			this.tongdaoGroupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tongdaoGroupBox2.Size = new System.Drawing.Size(1064, 330);
			this.tongdaoGroupBox2.TabIndex = 10;
			this.tongdaoGroupBox2.TabStop = false;
			// 
			// valueLabel32
			// 
			this.valueLabel32.Location = new System.Drawing.Point(998, 285);
			this.valueLabel32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel32.Name = "valueLabel32";
			this.valueLabel32.Size = new System.Drawing.Size(35, 23);
			this.valueLabel32.TabIndex = 11;
			this.valueLabel32.Text = "0";
			this.valueLabel32.Visible = false;
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
			// valueLabel31
			// 
			this.valueLabel31.Location = new System.Drawing.Point(934, 285);
			this.valueLabel31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel31.Name = "valueLabel31";
			this.valueLabel31.Size = new System.Drawing.Size(35, 23);
			this.valueLabel31.TabIndex = 11;
			this.valueLabel31.Text = "0";
			this.valueLabel31.Visible = false;
			// 
			// vScrollBar17
			// 
			this.vScrollBar17.Location = new System.Drawing.Point(42, 35);
			this.vScrollBar17.Maximum = 264;
			this.vScrollBar17.Name = "vScrollBar17";
			this.vScrollBar17.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar17.TabIndex = 0;
			this.vScrollBar17.Visible = false;
			// 
			// valueLabel30
			// 
			this.valueLabel30.Location = new System.Drawing.Point(870, 285);
			this.valueLabel30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel30.Name = "valueLabel30";
			this.valueLabel30.Size = new System.Drawing.Size(35, 23);
			this.valueLabel30.TabIndex = 11;
			this.valueLabel30.Text = "0";
			this.valueLabel30.Visible = false;
			// 
			// valueLabel29
			// 
			this.valueLabel29.Location = new System.Drawing.Point(806, 285);
			this.valueLabel29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel29.Name = "valueLabel29";
			this.valueLabel29.Size = new System.Drawing.Size(35, 23);
			this.valueLabel29.TabIndex = 11;
			this.valueLabel29.Text = "0";
			this.valueLabel29.Visible = false;
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
			// valueLabel28
			// 
			this.valueLabel28.Location = new System.Drawing.Point(742, 285);
			this.valueLabel28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel28.Name = "valueLabel28";
			this.valueLabel28.Size = new System.Drawing.Size(35, 23);
			this.valueLabel28.TabIndex = 11;
			this.valueLabel28.Text = "0";
			this.valueLabel28.Visible = false;
			// 
			// vScrollBar18
			// 
			this.vScrollBar18.Location = new System.Drawing.Point(106, 35);
			this.vScrollBar18.Maximum = 264;
			this.vScrollBar18.Name = "vScrollBar18";
			this.vScrollBar18.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar18.TabIndex = 0;
			this.vScrollBar18.Visible = false;
			// 
			// valueLabel27
			// 
			this.valueLabel27.Location = new System.Drawing.Point(678, 285);
			this.valueLabel27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel27.Name = "valueLabel27";
			this.valueLabel27.Size = new System.Drawing.Size(35, 23);
			this.valueLabel27.TabIndex = 11;
			this.valueLabel27.Text = "0";
			this.valueLabel27.Visible = false;
			// 
			// vScrollBar19
			// 
			this.vScrollBar19.Location = new System.Drawing.Point(170, 35);
			this.vScrollBar19.Maximum = 264;
			this.vScrollBar19.Name = "vScrollBar19";
			this.vScrollBar19.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar19.TabIndex = 0;
			this.vScrollBar19.Visible = false;
			// 
			// valueLabel26
			// 
			this.valueLabel26.Location = new System.Drawing.Point(614, 285);
			this.valueLabel26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel26.Name = "valueLabel26";
			this.valueLabel26.Size = new System.Drawing.Size(35, 23);
			this.valueLabel26.TabIndex = 11;
			this.valueLabel26.Text = "0";
			this.valueLabel26.Visible = false;
			// 
			// vScrollBar20
			// 
			this.vScrollBar20.Location = new System.Drawing.Point(234, 35);
			this.vScrollBar20.Maximum = 264;
			this.vScrollBar20.Name = "vScrollBar20";
			this.vScrollBar20.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar20.TabIndex = 0;
			this.vScrollBar20.Visible = false;
			// 
			// valueLabel25
			// 
			this.valueLabel25.Location = new System.Drawing.Point(550, 285);
			this.valueLabel25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel25.Name = "valueLabel25";
			this.valueLabel25.Size = new System.Drawing.Size(35, 23);
			this.valueLabel25.TabIndex = 11;
			this.valueLabel25.Text = "0";
			this.valueLabel25.Visible = false;
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
			// valueLabel24
			// 
			this.valueLabel24.Location = new System.Drawing.Point(486, 285);
			this.valueLabel24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel24.Name = "valueLabel24";
			this.valueLabel24.Size = new System.Drawing.Size(35, 23);
			this.valueLabel24.TabIndex = 11;
			this.valueLabel24.Text = "0";
			this.valueLabel24.Visible = false;
			// 
			// vScrollBar21
			// 
			this.vScrollBar21.Location = new System.Drawing.Point(298, 35);
			this.vScrollBar21.Maximum = 264;
			this.vScrollBar21.Name = "vScrollBar21";
			this.vScrollBar21.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar21.TabIndex = 0;
			this.vScrollBar21.Visible = false;
			// 
			// valueLabel23
			// 
			this.valueLabel23.Location = new System.Drawing.Point(422, 285);
			this.valueLabel23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel23.Name = "valueLabel23";
			this.valueLabel23.Size = new System.Drawing.Size(35, 23);
			this.valueLabel23.TabIndex = 11;
			this.valueLabel23.Text = "0";
			this.valueLabel23.Visible = false;
			// 
			// vScrollBar22
			// 
			this.vScrollBar22.Location = new System.Drawing.Point(362, 35);
			this.vScrollBar22.Maximum = 264;
			this.vScrollBar22.Name = "vScrollBar22";
			this.vScrollBar22.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar22.TabIndex = 0;
			this.vScrollBar22.Visible = false;
			// 
			// valueLabel22
			// 
			this.valueLabel22.Location = new System.Drawing.Point(358, 285);
			this.valueLabel22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel22.Name = "valueLabel22";
			this.valueLabel22.Size = new System.Drawing.Size(35, 23);
			this.valueLabel22.TabIndex = 11;
			this.valueLabel22.Text = "0";
			this.valueLabel22.Visible = false;
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
			// valueLabel21
			// 
			this.valueLabel21.Location = new System.Drawing.Point(294, 285);
			this.valueLabel21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel21.Name = "valueLabel21";
			this.valueLabel21.Size = new System.Drawing.Size(35, 23);
			this.valueLabel21.TabIndex = 11;
			this.valueLabel21.Text = "0";
			this.valueLabel21.Visible = false;
			// 
			// vScrollBar23
			// 
			this.vScrollBar23.Location = new System.Drawing.Point(426, 35);
			this.vScrollBar23.Maximum = 264;
			this.vScrollBar23.Name = "vScrollBar23";
			this.vScrollBar23.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar23.TabIndex = 0;
			this.vScrollBar23.Visible = false;
			// 
			// valueLabel20
			// 
			this.valueLabel20.Location = new System.Drawing.Point(230, 285);
			this.valueLabel20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel20.Name = "valueLabel20";
			this.valueLabel20.Size = new System.Drawing.Size(35, 23);
			this.valueLabel20.TabIndex = 11;
			this.valueLabel20.Text = "0";
			this.valueLabel20.Visible = false;
			// 
			// vScrollBar24
			// 
			this.vScrollBar24.Location = new System.Drawing.Point(490, 35);
			this.vScrollBar24.Maximum = 264;
			this.vScrollBar24.Name = "vScrollBar24";
			this.vScrollBar24.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar24.TabIndex = 0;
			this.vScrollBar24.Visible = false;
			// 
			// valueLabel19
			// 
			this.valueLabel19.Location = new System.Drawing.Point(166, 285);
			this.valueLabel19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel19.Name = "valueLabel19";
			this.valueLabel19.Size = new System.Drawing.Size(35, 23);
			this.valueLabel19.TabIndex = 11;
			this.valueLabel19.Text = "0";
			this.valueLabel19.Visible = false;
			// 
			// vScrollBar25
			// 
			this.vScrollBar25.Location = new System.Drawing.Point(554, 35);
			this.vScrollBar25.Maximum = 264;
			this.vScrollBar25.Name = "vScrollBar25";
			this.vScrollBar25.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar25.TabIndex = 0;
			this.vScrollBar25.Visible = false;
			// 
			// valueLabel18
			// 
			this.valueLabel18.Location = new System.Drawing.Point(102, 285);
			this.valueLabel18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel18.Name = "valueLabel18";
			this.valueLabel18.Size = new System.Drawing.Size(35, 23);
			this.valueLabel18.TabIndex = 11;
			this.valueLabel18.Text = "0";
			this.valueLabel18.Visible = false;
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
			// valueLabel17
			// 
			this.valueLabel17.Location = new System.Drawing.Point(38, 285);
			this.valueLabel17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel17.Name = "valueLabel17";
			this.valueLabel17.Size = new System.Drawing.Size(35, 23);
			this.valueLabel17.TabIndex = 11;
			this.valueLabel17.Text = "0";
			this.valueLabel17.Visible = false;
			// 
			// vScrollBar26
			// 
			this.vScrollBar26.Location = new System.Drawing.Point(618, 35);
			this.vScrollBar26.Maximum = 264;
			this.vScrollBar26.Name = "vScrollBar26";
			this.vScrollBar26.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar26.TabIndex = 0;
			this.vScrollBar26.Visible = false;
			// 
			// vScrollBar27
			// 
			this.vScrollBar27.Location = new System.Drawing.Point(682, 35);
			this.vScrollBar27.Maximum = 264;
			this.vScrollBar27.Name = "vScrollBar27";
			this.vScrollBar27.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar27.TabIndex = 0;
			this.vScrollBar27.Visible = false;
			// 
			// vScrollBar28
			// 
			this.vScrollBar28.Location = new System.Drawing.Point(746, 35);
			this.vScrollBar28.Maximum = 264;
			this.vScrollBar28.Name = "vScrollBar28";
			this.vScrollBar28.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar28.TabIndex = 0;
			this.vScrollBar28.Visible = false;
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
			this.vScrollBar29.Location = new System.Drawing.Point(810, 35);
			this.vScrollBar29.Maximum = 264;
			this.vScrollBar29.Name = "vScrollBar29";
			this.vScrollBar29.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar29.TabIndex = 0;
			this.vScrollBar29.Visible = false;
			// 
			// vScrollBar30
			// 
			this.vScrollBar30.Location = new System.Drawing.Point(874, 35);
			this.vScrollBar30.Maximum = 264;
			this.vScrollBar30.Name = "vScrollBar30";
			this.vScrollBar30.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar30.TabIndex = 0;
			this.vScrollBar30.Visible = false;
			// 
			// vScrollBar31
			// 
			this.vScrollBar31.Location = new System.Drawing.Point(938, 35);
			this.vScrollBar31.Maximum = 264;
			this.vScrollBar31.Name = "vScrollBar31";
			this.vScrollBar31.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar31.TabIndex = 0;
			this.vScrollBar31.Visible = false;
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
			this.vScrollBar32.Location = new System.Drawing.Point(1002, 35);
			this.vScrollBar32.Maximum = 264;
			this.vScrollBar32.Name = "vScrollBar32";
			this.vScrollBar32.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar32.TabIndex = 0;
			this.vScrollBar32.Visible = false;
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
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel16);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel15);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel14);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel13);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel12);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel11);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel10);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel9);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel8);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel7);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel6);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel5);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel4);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel3);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel2);
			this.tongdaoGroupBox1.Controls.Add(this.valueLabel1);
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
			// valueLabel16
			// 
			this.valueLabel16.Location = new System.Drawing.Point(999, 286);
			this.valueLabel16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel16.Name = "valueLabel16";
			this.valueLabel16.Size = new System.Drawing.Size(35, 23);
			this.valueLabel16.TabIndex = 11;
			this.valueLabel16.Text = "0";
			this.valueLabel16.Visible = false;
			// 
			// valueLabel15
			// 
			this.valueLabel15.Location = new System.Drawing.Point(935, 286);
			this.valueLabel15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel15.Name = "valueLabel15";
			this.valueLabel15.Size = new System.Drawing.Size(35, 23);
			this.valueLabel15.TabIndex = 11;
			this.valueLabel15.Text = "0";
			this.valueLabel15.Visible = false;
			// 
			// valueLabel14
			// 
			this.valueLabel14.Location = new System.Drawing.Point(871, 286);
			this.valueLabel14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel14.Name = "valueLabel14";
			this.valueLabel14.Size = new System.Drawing.Size(35, 23);
			this.valueLabel14.TabIndex = 11;
			this.valueLabel14.Text = "0";
			this.valueLabel14.Visible = false;
			// 
			// valueLabel13
			// 
			this.valueLabel13.Location = new System.Drawing.Point(807, 286);
			this.valueLabel13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel13.Name = "valueLabel13";
			this.valueLabel13.Size = new System.Drawing.Size(35, 23);
			this.valueLabel13.TabIndex = 11;
			this.valueLabel13.Text = "0";
			this.valueLabel13.Visible = false;
			// 
			// valueLabel12
			// 
			this.valueLabel12.Location = new System.Drawing.Point(743, 286);
			this.valueLabel12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel12.Name = "valueLabel12";
			this.valueLabel12.Size = new System.Drawing.Size(35, 23);
			this.valueLabel12.TabIndex = 11;
			this.valueLabel12.Text = "0";
			this.valueLabel12.Visible = false;
			// 
			// valueLabel11
			// 
			this.valueLabel11.Location = new System.Drawing.Point(679, 286);
			this.valueLabel11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel11.Name = "valueLabel11";
			this.valueLabel11.Size = new System.Drawing.Size(35, 23);
			this.valueLabel11.TabIndex = 11;
			this.valueLabel11.Text = "0";
			this.valueLabel11.Visible = false;
			// 
			// valueLabel10
			// 
			this.valueLabel10.Location = new System.Drawing.Point(615, 286);
			this.valueLabel10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel10.Name = "valueLabel10";
			this.valueLabel10.Size = new System.Drawing.Size(35, 23);
			this.valueLabel10.TabIndex = 11;
			this.valueLabel10.Text = "0";
			this.valueLabel10.Visible = false;
			// 
			// valueLabel9
			// 
			this.valueLabel9.Location = new System.Drawing.Point(551, 286);
			this.valueLabel9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel9.Name = "valueLabel9";
			this.valueLabel9.Size = new System.Drawing.Size(35, 23);
			this.valueLabel9.TabIndex = 11;
			this.valueLabel9.Text = "0";
			this.valueLabel9.Visible = false;
			// 
			// valueLabel8
			// 
			this.valueLabel8.Location = new System.Drawing.Point(487, 286);
			this.valueLabel8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel8.Name = "valueLabel8";
			this.valueLabel8.Size = new System.Drawing.Size(35, 23);
			this.valueLabel8.TabIndex = 11;
			this.valueLabel8.Text = "0";
			this.valueLabel8.Visible = false;
			// 
			// valueLabel7
			// 
			this.valueLabel7.Location = new System.Drawing.Point(423, 286);
			this.valueLabel7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel7.Name = "valueLabel7";
			this.valueLabel7.Size = new System.Drawing.Size(35, 23);
			this.valueLabel7.TabIndex = 11;
			this.valueLabel7.Text = "0";
			this.valueLabel7.Visible = false;
			// 
			// valueLabel6
			// 
			this.valueLabel6.Location = new System.Drawing.Point(359, 286);
			this.valueLabel6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel6.Name = "valueLabel6";
			this.valueLabel6.Size = new System.Drawing.Size(35, 23);
			this.valueLabel6.TabIndex = 11;
			this.valueLabel6.Text = "0";
			this.valueLabel6.Visible = false;
			// 
			// valueLabel5
			// 
			this.valueLabel5.Location = new System.Drawing.Point(295, 286);
			this.valueLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel5.Name = "valueLabel5";
			this.valueLabel5.Size = new System.Drawing.Size(35, 23);
			this.valueLabel5.TabIndex = 11;
			this.valueLabel5.Text = "0";
			this.valueLabel5.Visible = false;
			// 
			// valueLabel4
			// 
			this.valueLabel4.Location = new System.Drawing.Point(231, 286);
			this.valueLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel4.Name = "valueLabel4";
			this.valueLabel4.Size = new System.Drawing.Size(35, 23);
			this.valueLabel4.TabIndex = 11;
			this.valueLabel4.Text = "0";
			this.valueLabel4.Visible = false;
			// 
			// valueLabel3
			// 
			this.valueLabel3.Location = new System.Drawing.Point(167, 286);
			this.valueLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel3.Name = "valueLabel3";
			this.valueLabel3.Size = new System.Drawing.Size(35, 23);
			this.valueLabel3.TabIndex = 11;
			this.valueLabel3.Text = "0";
			this.valueLabel3.Visible = false;
			// 
			// valueLabel2
			// 
			this.valueLabel2.Location = new System.Drawing.Point(103, 286);
			this.valueLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel2.Name = "valueLabel2";
			this.valueLabel2.Size = new System.Drawing.Size(35, 23);
			this.valueLabel2.TabIndex = 11;
			this.valueLabel2.Text = "0";
			this.valueLabel2.Visible = false;
			// 
			// valueLabel1
			// 
			this.valueLabel1.Location = new System.Drawing.Point(39, 286);
			this.valueLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.valueLabel1.Name = "valueLabel1";
			this.valueLabel1.Size = new System.Drawing.Size(35, 23);
			this.valueLabel1.TabIndex = 11;
			this.valueLabel1.Text = "0";
			this.valueLabel1.Visible = false;
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
			// 
			// vScrollBar12
			// 
			this.vScrollBar12.Location = new System.Drawing.Point(745, 38);
			this.vScrollBar12.Maximum = 264;
			this.vScrollBar12.Name = "vScrollBar12";
			this.vScrollBar12.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar12.TabIndex = 0;
			// 
			// vScrollBar8
			// 
			this.vScrollBar8.Location = new System.Drawing.Point(489, 38);
			this.vScrollBar8.Maximum = 264;
			this.vScrollBar8.Name = "vScrollBar8";
			this.vScrollBar8.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar8.TabIndex = 0;
			// 
			// vScrollBar4
			// 
			this.vScrollBar4.Location = new System.Drawing.Point(233, 38);
			this.vScrollBar4.Maximum = 264;
			this.vScrollBar4.Name = "vScrollBar4";
			this.vScrollBar4.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar4.TabIndex = 0;
			// 
			// vScrollBar15
			// 
			this.vScrollBar15.Location = new System.Drawing.Point(937, 38);
			this.vScrollBar15.Maximum = 264;
			this.vScrollBar15.Name = "vScrollBar15";
			this.vScrollBar15.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar15.TabIndex = 0;
			// 
			// vScrollBar11
			// 
			this.vScrollBar11.Location = new System.Drawing.Point(681, 38);
			this.vScrollBar11.Maximum = 264;
			this.vScrollBar11.Name = "vScrollBar11";
			this.vScrollBar11.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar11.TabIndex = 0;
			// 
			// vScrollBar14
			// 
			this.vScrollBar14.Location = new System.Drawing.Point(873, 38);
			this.vScrollBar14.Maximum = 264;
			this.vScrollBar14.Name = "vScrollBar14";
			this.vScrollBar14.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar14.TabIndex = 0;
			// 
			// vScrollBar10
			// 
			this.vScrollBar10.Location = new System.Drawing.Point(617, 38);
			this.vScrollBar10.Maximum = 264;
			this.vScrollBar10.Name = "vScrollBar10";
			this.vScrollBar10.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar10.TabIndex = 0;
			// 
			// vScrollBar7
			// 
			this.vScrollBar7.Location = new System.Drawing.Point(425, 38);
			this.vScrollBar7.Maximum = 264;
			this.vScrollBar7.Name = "vScrollBar7";
			this.vScrollBar7.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar7.TabIndex = 0;
			// 
			// vScrollBar13
			// 
			this.vScrollBar13.Location = new System.Drawing.Point(809, 38);
			this.vScrollBar13.Maximum = 264;
			this.vScrollBar13.Name = "vScrollBar13";
			this.vScrollBar13.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar13.TabIndex = 0;
			// 
			// vScrollBar6
			// 
			this.vScrollBar6.Location = new System.Drawing.Point(361, 38);
			this.vScrollBar6.Maximum = 264;
			this.vScrollBar6.Name = "vScrollBar6";
			this.vScrollBar6.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar6.TabIndex = 0;
			// 
			// vScrollBar9
			// 
			this.vScrollBar9.Location = new System.Drawing.Point(553, 38);
			this.vScrollBar9.Maximum = 264;
			this.vScrollBar9.Name = "vScrollBar9";
			this.vScrollBar9.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar9.TabIndex = 0;
			// 
			// vScrollBar3
			// 
			this.vScrollBar3.Location = new System.Drawing.Point(169, 38);
			this.vScrollBar3.Maximum = 264;
			this.vScrollBar3.Name = "vScrollBar3";
			this.vScrollBar3.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar3.TabIndex = 0;
			// 
			// vScrollBar5
			// 
			this.vScrollBar5.Location = new System.Drawing.Point(297, 38);
			this.vScrollBar5.Maximum = 264;
			this.vScrollBar5.Name = "vScrollBar5";
			this.vScrollBar5.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar5.TabIndex = 0;
			// 
			// vScrollBar2
			// 
			this.vScrollBar2.Location = new System.Drawing.Point(105, 38);
			this.vScrollBar2.Maximum = 264;
			this.vScrollBar2.Name = "vScrollBar2";
			this.vScrollBar2.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar2.TabIndex = 0;
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Location = new System.Drawing.Point(41, 38);
			this.vScrollBar1.Maximum = 264;
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(24, 227);
			this.vScrollBar1.TabIndex = 0;
			// 
			// comboBox3
			// 
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Location = new System.Drawing.Point(261, 25);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(121, 23);
			this.comboBox3.TabIndex = 0;
			// 
			// comboBox2
			// 
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(134, 24);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(121, 23);
			this.comboBox2.TabIndex = 0;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "常规模式",
            "声控模式"});
			this.comboBox1.Location = new System.Drawing.Point(7, 25);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 23);
			this.comboBox1.TabIndex = 0;
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
			this.tongdaoGroupBox2.ResumeLayout(false);
			this.tongdaoGroupBox1.ResumeLayout(false);
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
		private ComboBox comboBox3;
		private ComboBox comboBox2;
		private ComboBox comboBox1;
		private GroupBox tongdaoGroupBox1;
		public Label valueLabel16;
		public Label valueLabel15;
		public Label valueLabel14;
		public Label valueLabel13;
		public Label valueLabel12;
		public Label valueLabel11;
		public Label valueLabel10;
		public Label valueLabel9;
		public Label valueLabel8;
		public Label valueLabel7;
		public Label valueLabel6;
		public Label valueLabel5;
		public Label valueLabel4;
		public Label valueLabel3;
		public Label valueLabel2;
		public Label valueLabel1;
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
		public Label valueLabel32;
		public Label label32;
		public Label valueLabel31;
		public VScrollBar vScrollBar17;
		public Label valueLabel30;
		public Label valueLabel29;
		public Label label31;
		public Label valueLabel28;
		public VScrollBar vScrollBar18;
		public Label valueLabel27;
		public VScrollBar vScrollBar19;
		public Label valueLabel26;
		public VScrollBar vScrollBar20;
		public Label valueLabel25;
		public Label label30;
		public Label valueLabel24;
		public VScrollBar vScrollBar21;
		public Label valueLabel23;
		public VScrollBar vScrollBar22;
		public Label valueLabel22;
		public Label label29;
		public Label valueLabel21;
		public VScrollBar vScrollBar23;
		public Label valueLabel20;
		public VScrollBar vScrollBar24;
		public Label valueLabel19;
		public VScrollBar vScrollBar25;
		public Label valueLabel18;
		public Label label28;
		public Label valueLabel17;
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

		public Label[] labels = new Label[32];
		public Label[] valueLabels = new Label[32];
		public VScrollBar[] vScrollBars = new VScrollBar[32];


	}
}

