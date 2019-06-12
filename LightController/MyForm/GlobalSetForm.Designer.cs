namespace LightController.MyForm
{
	partial class GlobalSetForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox10 = new System.Windows.Forms.GroupBox();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.button2 = new System.Windows.Forms.Button();
			this.zuheFrameComboBox = new System.Windows.Forms.ComboBox();
			this.zuheCheckBox = new System.Windows.Forms.CheckBox();
			this.zuheEnableGroupBox = new System.Windows.Forms.GroupBox();
			this.frame4numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.frame3numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.frame2numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.frame4ComboBox = new System.Windows.Forms.ComboBox();
			this.frame3ComboBox = new System.Windows.Forms.ComboBox();
			this.frame2ComboBox = new System.Windows.Forms.ComboBox();
			this.frame1ComboBox = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.frame1numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.circleTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.button12 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.startupComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tongdaoCountComboBox = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.checkBox8 = new System.Windows.Forms.CheckBox();
			this.checkBox7 = new System.Windows.Forms.CheckBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.jidianqiFrameComboBox = new System.Windows.Forms.ComboBox();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.button9 = new System.Windows.Forms.Button();
			this.entityCommand1 = new System.Data.Entity.Core.EntityClient.EntityCommand();
			this.groupBox1.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.zuheEnableGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.frame4numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.frame3numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.frame2numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.frame1numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.circleTimeNumericUpDown)).BeginInit();
			this.groupBox7.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.groupBox10);
			this.groupBox1.Controls.Add(this.groupBox8);
			this.groupBox1.Controls.Add(this.groupBox7);
			this.groupBox1.Location = new System.Drawing.Point(6, 79);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(928, 521);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "DMX512设置";
			this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// groupBox10
			// 
			this.groupBox10.Location = new System.Drawing.Point(7, 324);
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.Size = new System.Drawing.Size(910, 178);
			this.groupBox10.TabIndex = 2;
			this.groupBox10.TabStop = false;
			this.groupBox10.Text = "音频场景设置";
			// 
			// groupBox8
			// 
			this.groupBox8.Controls.Add(this.button2);
			this.groupBox8.Controls.Add(this.zuheFrameComboBox);
			this.groupBox8.Controls.Add(this.zuheCheckBox);
			this.groupBox8.Controls.Add(this.zuheEnableGroupBox);
			this.groupBox8.Location = new System.Drawing.Point(6, 99);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(911, 219);
			this.groupBox8.TabIndex = 1;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "多场景组合播放设置";
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.Linen;
			this.button2.Location = new System.Drawing.Point(798, 24);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(93, 28);
			this.button2.TabIndex = 3;
			this.button2.Text = "保存";
			this.button2.UseVisualStyleBackColor = false;
			// 
			// zuheFrameComboBox
			// 
			this.zuheFrameComboBox.FormattingEnabled = true;
			this.zuheFrameComboBox.Items.AddRange(new object[] {
            "标准",
            "动感",
            "商务",
            "抒情",
            "清洁",
            "柔和",
            "激情",
            "明亮",
            "浪漫"});
			this.zuheFrameComboBox.Location = new System.Drawing.Point(10, 34);
			this.zuheFrameComboBox.Name = "zuheFrameComboBox";
			this.zuheFrameComboBox.Size = new System.Drawing.Size(121, 23);
			this.zuheFrameComboBox.TabIndex = 2;
			this.zuheFrameComboBox.SelectedIndexChanged += new System.EventHandler(this.zuheFrameComboBox_SelectedIndexChanged);
			// 
			// zuheCheckBox
			// 
			this.zuheCheckBox.AutoSize = true;
			this.zuheCheckBox.Location = new System.Drawing.Point(172, 36);
			this.zuheCheckBox.Name = "zuheCheckBox";
			this.zuheCheckBox.Size = new System.Drawing.Size(134, 19);
			this.zuheCheckBox.TabIndex = 1;
			this.zuheCheckBox.Text = "是否开启此功能";
			this.zuheCheckBox.UseVisualStyleBackColor = true;
			this.zuheCheckBox.CheckedChanged += new System.EventHandler(this.zuheCheckBox_CheckedChanged);
			// 
			// zuheEnableGroupBox
			// 
			this.zuheEnableGroupBox.Controls.Add(this.frame4numericUpDown);
			this.zuheEnableGroupBox.Controls.Add(this.frame3numericUpDown);
			this.zuheEnableGroupBox.Controls.Add(this.frame2numericUpDown);
			this.zuheEnableGroupBox.Controls.Add(this.frame4ComboBox);
			this.zuheEnableGroupBox.Controls.Add(this.frame3ComboBox);
			this.zuheEnableGroupBox.Controls.Add(this.frame2ComboBox);
			this.zuheEnableGroupBox.Controls.Add(this.frame1ComboBox);
			this.zuheEnableGroupBox.Controls.Add(this.label9);
			this.zuheEnableGroupBox.Controls.Add(this.label7);
			this.zuheEnableGroupBox.Controls.Add(this.label6);
			this.zuheEnableGroupBox.Controls.Add(this.label5);
			this.zuheEnableGroupBox.Controls.Add(this.label8);
			this.zuheEnableGroupBox.Controls.Add(this.label4);
			this.zuheEnableGroupBox.Controls.Add(this.frame1numericUpDown);
			this.zuheEnableGroupBox.Controls.Add(this.label3);
			this.zuheEnableGroupBox.Controls.Add(this.circleTimeNumericUpDown);
			this.zuheEnableGroupBox.Enabled = false;
			this.zuheEnableGroupBox.Location = new System.Drawing.Point(10, 70);
			this.zuheEnableGroupBox.Name = "zuheEnableGroupBox";
			this.zuheEnableGroupBox.Size = new System.Drawing.Size(881, 130);
			this.zuheEnableGroupBox.TabIndex = 0;
			this.zuheEnableGroupBox.TabStop = false;
			this.zuheEnableGroupBox.Text = "播放设置";
			// 
			// frame4numericUpDown
			// 
			this.frame4numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.frame4numericUpDown.Location = new System.Drawing.Point(747, 88);
			this.frame4numericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.frame4numericUpDown.Name = "frame4numericUpDown";
			this.frame4numericUpDown.Size = new System.Drawing.Size(120, 25);
			this.frame4numericUpDown.TabIndex = 6;
			// 
			// frame3numericUpDown
			// 
			this.frame3numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.frame3numericUpDown.Location = new System.Drawing.Point(599, 88);
			this.frame3numericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.frame3numericUpDown.Name = "frame3numericUpDown";
			this.frame3numericUpDown.Size = new System.Drawing.Size(120, 25);
			this.frame3numericUpDown.TabIndex = 5;
			// 
			// frame2numericUpDown
			// 
			this.frame2numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.frame2numericUpDown.Location = new System.Drawing.Point(447, 88);
			this.frame2numericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.frame2numericUpDown.Name = "frame2numericUpDown";
			this.frame2numericUpDown.Size = new System.Drawing.Size(120, 25);
			this.frame2numericUpDown.TabIndex = 4;
			// 
			// frame4ComboBox
			// 
			this.frame4ComboBox.FormattingEnabled = true;
			this.frame4ComboBox.Items.AddRange(new object[] {
            "标准",
            "动感",
            "商务",
            "抒情",
            "清洁",
            "柔和",
            "激情",
            "明亮",
            "浪漫"});
			this.frame4ComboBox.Location = new System.Drawing.Point(747, 51);
			this.frame4ComboBox.Name = "frame4ComboBox";
			this.frame4ComboBox.Size = new System.Drawing.Size(87, 23);
			this.frame4ComboBox.TabIndex = 3;
			// 
			// frame3ComboBox
			// 
			this.frame3ComboBox.FormattingEnabled = true;
			this.frame3ComboBox.Items.AddRange(new object[] {
            "标准",
            "动感",
            "商务",
            "抒情",
            "清洁",
            "柔和",
            "激情",
            "明亮",
            "浪漫"});
			this.frame3ComboBox.Location = new System.Drawing.Point(599, 51);
			this.frame3ComboBox.Name = "frame3ComboBox";
			this.frame3ComboBox.Size = new System.Drawing.Size(87, 23);
			this.frame3ComboBox.TabIndex = 3;
			// 
			// frame2ComboBox
			// 
			this.frame2ComboBox.FormattingEnabled = true;
			this.frame2ComboBox.Items.AddRange(new object[] {
            "标准",
            "动感",
            "商务",
            "抒情",
            "清洁",
            "柔和",
            "激情",
            "明亮",
            "浪漫"});
			this.frame2ComboBox.Location = new System.Drawing.Point(447, 51);
			this.frame2ComboBox.Name = "frame2ComboBox";
			this.frame2ComboBox.Size = new System.Drawing.Size(87, 23);
			this.frame2ComboBox.TabIndex = 3;
			// 
			// frame1ComboBox
			// 
			this.frame1ComboBox.FormattingEnabled = true;
			this.frame1ComboBox.Items.AddRange(new object[] {
            "标准",
            "动感",
            "商务",
            "抒情",
            "清洁",
            "柔和",
            "激情",
            "明亮",
            "浪漫"});
			this.frame1ComboBox.Location = new System.Drawing.Point(295, 51);
			this.frame1ComboBox.Name = "frame1ComboBox";
			this.frame1ComboBox.Size = new System.Drawing.Size(87, 23);
			this.frame1ComboBox.TabIndex = 3;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(185, 55);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(67, 15);
			this.label9.TabIndex = 2;
			this.label9.Text = "场景类型";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(750, 21);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(75, 15);
			this.label7.TabIndex = 1;
			this.label7.Text = "组合场景4";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(598, 21);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(75, 15);
			this.label6.TabIndex = 1;
			this.label6.Text = "组合场景3";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(451, 21);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(75, 15);
			this.label5.TabIndex = 1;
			this.label5.Text = "组合场景2";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(185, 93);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(67, 15);
			this.label8.TabIndex = 1;
			this.label8.Text = "持续时间";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(294, 21);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(75, 15);
			this.label4.TabIndex = 1;
			this.label4.Text = "组合场景1";
			// 
			// frame1numericUpDown
			// 
			this.frame1numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.frame1numericUpDown.Location = new System.Drawing.Point(296, 88);
			this.frame1numericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.frame1numericUpDown.Name = "frame1numericUpDown";
			this.frame1numericUpDown.Size = new System.Drawing.Size(120, 25);
			this.frame1numericUpDown.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 37);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 15);
			this.label3.TabIndex = 1;
			this.label3.Text = "循环次数";
			// 
			// circleTimeNumericUpDown
			// 
			this.circleTimeNumericUpDown.Location = new System.Drawing.Point(8, 71);
			this.circleTimeNumericUpDown.Name = "circleTimeNumericUpDown";
			this.circleTimeNumericUpDown.Size = new System.Drawing.Size(120, 25);
			this.circleTimeNumericUpDown.TabIndex = 0;
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.button12);
			this.groupBox7.Controls.Add(this.label2);
			this.groupBox7.Controls.Add(this.startupComboBox);
			this.groupBox7.Controls.Add(this.label1);
			this.groupBox7.Controls.Add(this.tongdaoCountComboBox);
			this.groupBox7.Location = new System.Drawing.Point(7, 25);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(910, 68);
			this.groupBox7.TabIndex = 0;
			this.groupBox7.TabStop = false;
			// 
			// button12
			// 
			this.button12.BackColor = System.Drawing.Color.Wheat;
			this.button12.Location = new System.Drawing.Point(797, 19);
			this.button12.Name = "button12";
			this.button12.Size = new System.Drawing.Size(93, 28);
			this.button12.TabIndex = 3;
			this.button12.Text = "设置";
			this.button12.UseVisualStyleBackColor = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(314, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 15);
			this.label2.TabIndex = 3;
			this.label2.Text = "开机自动播放：";
			// 
			// startupComboBox
			// 
			this.startupComboBox.FormattingEnabled = true;
			this.startupComboBox.Items.AddRange(new object[] {
            "无",
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
			this.startupComboBox.Location = new System.Drawing.Point(438, 22);
			this.startupComboBox.Name = "startupComboBox";
			this.startupComboBox.Size = new System.Drawing.Size(138, 23);
			this.startupComboBox.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "通道总数设置：";
			// 
			// tongdaoCountComboBox
			// 
			this.tongdaoCountComboBox.FormattingEnabled = true;
			this.tongdaoCountComboBox.Items.AddRange(new object[] {
            "512",
            "384",
            "256",
            "128"});
			this.tongdaoCountComboBox.Location = new System.Drawing.Point(124, 22);
			this.tongdaoCountComboBox.Name = "tongdaoCountComboBox";
			this.tongdaoCountComboBox.Size = new System.Drawing.Size(138, 23);
			this.tongdaoCountComboBox.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.checkBox8);
			this.groupBox2.Controls.Add(this.checkBox7);
			this.groupBox2.Controls.Add(this.checkBox6);
			this.groupBox2.Controls.Add(this.checkBox5);
			this.groupBox2.Controls.Add(this.checkBox4);
			this.groupBox2.Controls.Add(this.checkBox3);
			this.groupBox2.Controls.Add(this.checkBox2);
			this.groupBox2.Controls.Add(this.checkBox1);
			this.groupBox2.Controls.Add(this.jidianqiFrameComboBox);
			this.groupBox2.Location = new System.Drawing.Point(6, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(928, 61);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "继电器开关设置";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Cornsilk;
			this.button1.Location = new System.Drawing.Point(804, 20);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(93, 28);
			this.button1.TabIndex = 2;
			this.button1.Text = "设置";
			this.button1.UseVisualStyleBackColor = false;
			// 
			// checkBox8
			// 
			this.checkBox8.AutoSize = true;
			this.checkBox8.Location = new System.Drawing.Point(709, 24);
			this.checkBox8.Name = "checkBox8";
			this.checkBox8.Size = new System.Drawing.Size(67, 19);
			this.checkBox8.TabIndex = 1;
			this.checkBox8.Text = "开关1";
			this.checkBox8.UseVisualStyleBackColor = true;
			// 
			// checkBox7
			// 
			this.checkBox7.AutoSize = true;
			this.checkBox7.Location = new System.Drawing.Point(630, 24);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new System.Drawing.Size(67, 19);
			this.checkBox7.TabIndex = 1;
			this.checkBox7.Text = "开关1";
			this.checkBox7.UseVisualStyleBackColor = true;
			// 
			// checkBox6
			// 
			this.checkBox6.AutoSize = true;
			this.checkBox6.Location = new System.Drawing.Point(551, 24);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(67, 19);
			this.checkBox6.TabIndex = 1;
			this.checkBox6.Text = "开关1";
			this.checkBox6.UseVisualStyleBackColor = true;
			// 
			// checkBox5
			// 
			this.checkBox5.AutoSize = true;
			this.checkBox5.Location = new System.Drawing.Point(472, 24);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(67, 19);
			this.checkBox5.TabIndex = 1;
			this.checkBox5.Text = "开关1";
			this.checkBox5.UseVisualStyleBackColor = true;
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.Location = new System.Drawing.Point(393, 24);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(67, 19);
			this.checkBox4.TabIndex = 1;
			this.checkBox4.Text = "开关1";
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new System.Drawing.Point(314, 24);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(67, 19);
			this.checkBox3.TabIndex = 1;
			this.checkBox3.Text = "开关1";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(235, 24);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(67, 19);
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "开关1";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(156, 24);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(67, 19);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "开关1";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// jidianqiFrameComboBox
			// 
			this.jidianqiFrameComboBox.FormattingEnabled = true;
			this.jidianqiFrameComboBox.Items.AddRange(new object[] {
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
			this.jidianqiFrameComboBox.Location = new System.Drawing.Point(6, 24);
			this.jidianqiFrameComboBox.Name = "jidianqiFrameComboBox";
			this.jidianqiFrameComboBox.Size = new System.Drawing.Size(99, 23);
			this.jidianqiFrameComboBox.TabIndex = 0;
			this.jidianqiFrameComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// button9
			// 
			this.button9.BackColor = System.Drawing.Color.CornflowerBlue;
			this.button9.Location = new System.Drawing.Point(844, 606);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(90, 58);
			this.button9.TabIndex = 3;
			this.button9.Text = "保存全部";
			this.button9.UseVisualStyleBackColor = false;
			// 
			// entityCommand1
			// 
			this.entityCommand1.CommandTimeout = 0;
			this.entityCommand1.CommandTree = null;
			this.entityCommand1.Connection = null;
			this.entityCommand1.EnablePlanCaching = true;
			this.entityCommand1.Transaction = null;
			// 
			// GlobalSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(941, 672);
			this.Controls.Add(this.button9);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GlobalSetForm";
			this.Text = "全局设置";
			this.Load += new System.EventHandler(this.GlobalSetForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox8.ResumeLayout(false);
			this.groupBox8.PerformLayout();
			this.zuheEnableGroupBox.ResumeLayout(false);
			this.zuheEnableGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.frame4numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.frame3numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.frame2numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.frame1numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.circleTimeNumericUpDown)).EndInit();
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox checkBox8;
		private System.Windows.Forms.CheckBox checkBox7;
		private System.Windows.Forms.CheckBox checkBox6;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.ComboBox jidianqiFrameComboBox;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox startupComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox tongdaoCountComboBox;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.CheckBox zuheCheckBox;
		private System.Windows.Forms.GroupBox zuheEnableGroupBox;
		private System.Data.Entity.Core.EntityClient.EntityCommand entityCommand1;
		private System.Windows.Forms.ComboBox zuheFrameComboBox;
		private System.Windows.Forms.NumericUpDown frame4numericUpDown;
		private System.Windows.Forms.NumericUpDown frame3numericUpDown;
		private System.Windows.Forms.NumericUpDown frame2numericUpDown;
		private System.Windows.Forms.ComboBox frame4ComboBox;
		private System.Windows.Forms.ComboBox frame2ComboBox;
		private System.Windows.Forms.ComboBox frame1ComboBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown frame1numericUpDown;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown circleTimeNumericUpDown;
		private System.Windows.Forms.ComboBox frame3ComboBox;
		private System.Windows.Forms.GroupBox groupBox10;
		private System.Windows.Forms.Button button2;
	}
}