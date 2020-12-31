using CCWin.SkinControl;
using System.Collections.Generic;
using System.Windows.Forms;

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
			this.components = new System.ComponentModel.Container();
			this.dmxGroupBox = new System.Windows.Forms.GroupBox();
			this.globalGroupBox = new System.Windows.Forms.GroupBox();
			this.globalSaveButton = new System.Windows.Forms.Button();
			this.eachChangeModeLabel = new System.Windows.Forms.Label();
			this.eachStepTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.eachStepTimeLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.eachChangeModeComboBox = new System.Windows.Forms.ComboBox();
			this.startupComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tongdaoCountComboBox = new System.Windows.Forms.ComboBox();
			this.zuheGroupBox = new System.Windows.Forms.GroupBox();
			this.mFrameSaveButton = new System.Windows.Forms.Button();
			this.zuheFrameComboBox = new System.Windows.Forms.ComboBox();
			this.zuheCheckBox = new System.Windows.Forms.CheckBox();
			this.zuheEnableGroupBox = new System.Windows.Forms.GroupBox();
			this.frame0NumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label34 = new System.Windows.Forms.Label();
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
			this.label35 = new System.Windows.Forms.Label();
			this.frame1numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.circleTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.label21 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label16 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.skNoticeButton = new System.Windows.Forms.Button();
			this.skSaveButton = new System.Windows.Forms.Button();
			this.label23 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.skFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.jgLabel = new System.Windows.Forms.Label();
			this.stLabel = new System.Windows.Forms.Label();
			this.lkTextBox = new System.Windows.Forms.TextBox();
			this.jgNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.stNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.frameLabel = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dmxGroupBox.SuspendLayout();
			this.globalGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.eachStepTimeNumericUpDown)).BeginInit();
			this.zuheGroupBox.SuspendLayout();
			this.zuheEnableGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.frame0NumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.frame4numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.frame3numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.frame2numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.frame1numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.circleTimeNumericUpDown)).BeginInit();
			this.panel3.SuspendLayout();
			this.skFlowLayoutPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.jgNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.stNumericUpDown)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dmxGroupBox
			// 
			this.dmxGroupBox.BackColor = System.Drawing.Color.Transparent;
			this.dmxGroupBox.Controls.Add(this.globalGroupBox);
			this.dmxGroupBox.Controls.Add(this.zuheGroupBox);
			this.dmxGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.dmxGroupBox.Location = new System.Drawing.Point(4, 4);
			this.dmxGroupBox.Margin = new System.Windows.Forms.Padding(2);
			this.dmxGroupBox.Name = "dmxGroupBox";
			this.dmxGroupBox.Padding = new System.Windows.Forms.Padding(8);
			this.dmxGroupBox.Size = new System.Drawing.Size(968, 267);
			this.dmxGroupBox.TabIndex = 0;
			this.dmxGroupBox.TabStop = false;
			this.dmxGroupBox.Text = "DMX512设置";
			// 
			// globalGroupBox
			// 
			this.globalGroupBox.BackColor = System.Drawing.Color.Transparent;
			this.globalGroupBox.Controls.Add(this.globalSaveButton);
			this.globalGroupBox.Controls.Add(this.eachChangeModeLabel);
			this.globalGroupBox.Controls.Add(this.eachStepTimeNumericUpDown);
			this.globalGroupBox.Controls.Add(this.eachStepTimeLabel);
			this.globalGroupBox.Controls.Add(this.label2);
			this.globalGroupBox.Controls.Add(this.eachChangeModeComboBox);
			this.globalGroupBox.Controls.Add(this.startupComboBox);
			this.globalGroupBox.Controls.Add(this.label1);
			this.globalGroupBox.Controls.Add(this.tongdaoCountComboBox);
			this.globalGroupBox.Location = new System.Drawing.Point(8, 19);
			this.globalGroupBox.Margin = new System.Windows.Forms.Padding(2);
			this.globalGroupBox.Name = "globalGroupBox";
			this.globalGroupBox.Padding = new System.Windows.Forms.Padding(2);
			this.globalGroupBox.Size = new System.Drawing.Size(266, 241);
			this.globalGroupBox.TabIndex = 0;
			this.globalGroupBox.TabStop = false;
			// 
			// globalSaveButton
			// 
			this.globalSaveButton.BackColor = System.Drawing.Color.Gainsboro;
			this.globalSaveButton.Location = new System.Drawing.Point(146, 187);
			this.globalSaveButton.Name = "globalSaveButton";
			this.globalSaveButton.Size = new System.Drawing.Size(84, 37);
			this.globalSaveButton.TabIndex = 7;
			this.globalSaveButton.Text = "保存设置";
			this.globalSaveButton.UseVisualStyleBackColor = false;
			this.globalSaveButton.Click += new System.EventHandler(this.globalSaveButton_Click);
			// 
			// eachChangeModeLabel
			// 
			this.eachChangeModeLabel.AutoSize = true;
			this.eachChangeModeLabel.Location = new System.Drawing.Point(21, 149);
			this.eachChangeModeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.eachChangeModeLabel.Name = "eachChangeModeLabel";
			this.eachChangeModeLabel.Size = new System.Drawing.Size(95, 12);
			this.eachChangeModeLabel.TabIndex = 6;
			this.eachChangeModeLabel.Text = "场景切换跳渐变:";
			// 
			// eachStepTimeNumericUpDown
			// 
			this.eachStepTimeNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.eachStepTimeNumericUpDown.Location = new System.Drawing.Point(146, 60);
			this.eachStepTimeNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.eachStepTimeNumericUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.eachStepTimeNumericUpDown.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
			this.eachStepTimeNumericUpDown.Name = "eachStepTimeNumericUpDown";
			this.eachStepTimeNumericUpDown.Size = new System.Drawing.Size(84, 21);
			this.eachStepTimeNumericUpDown.TabIndex = 5;
			this.eachStepTimeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.eachStepTimeNumericUpDown.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
			this.eachStepTimeNumericUpDown.ValueChanged += new System.EventHandler(this.eachStepTimeNumericUpDown_ValueChanged);
			// 
			// eachStepTimeLabel
			// 
			this.eachStepTimeLabel.AutoSize = true;
			this.eachStepTimeLabel.Location = new System.Drawing.Point(21, 64);
			this.eachStepTimeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.eachStepTimeLabel.Name = "eachStepTimeLabel";
			this.eachStepTimeLabel.Size = new System.Drawing.Size(89, 12);
			this.eachStepTimeLabel.TabIndex = 4;
			this.eachStepTimeLabel.Text = "时间因子(ms)：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 107);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(113, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "开机自动播放场景：";
			// 
			// eachChangeModeComboBox
			// 
			this.eachChangeModeComboBox.FormattingEnabled = true;
			this.eachChangeModeComboBox.Items.AddRange(new object[] {
            "跳变",
            "渐变慢",
            "渐变中",
            "渐变快"});
			this.eachChangeModeComboBox.Location = new System.Drawing.Point(146, 145);
			this.eachChangeModeComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.eachChangeModeComboBox.Name = "eachChangeModeComboBox";
			this.eachChangeModeComboBox.Size = new System.Drawing.Size(84, 20);
			this.eachChangeModeComboBox.TabIndex = 2;
			// 
			// startupComboBox
			// 
			this.startupComboBox.FormattingEnabled = true;
			this.startupComboBox.Items.AddRange(new object[] {
            "无"});
			this.startupComboBox.Location = new System.Drawing.Point(146, 103);
			this.startupComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.startupComboBox.Name = "startupComboBox";
			this.startupComboBox.Size = new System.Drawing.Size(84, 20);
			this.startupComboBox.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 22);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "通道总数设置：";
			// 
			// tongdaoCountComboBox
			// 
			this.tongdaoCountComboBox.Enabled = false;
			this.tongdaoCountComboBox.FormattingEnabled = true;
			this.tongdaoCountComboBox.Items.AddRange(new object[] {
            "512",
            "384",
            "256",
            "128"});
			this.tongdaoCountComboBox.Location = new System.Drawing.Point(146, 18);
			this.tongdaoCountComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.tongdaoCountComboBox.Name = "tongdaoCountComboBox";
			this.tongdaoCountComboBox.Size = new System.Drawing.Size(84, 20);
			this.tongdaoCountComboBox.TabIndex = 0;
			// 
			// zuheGroupBox
			// 
			this.zuheGroupBox.BackColor = System.Drawing.SystemColors.Window;
			this.zuheGroupBox.Controls.Add(this.mFrameSaveButton);
			this.zuheGroupBox.Controls.Add(this.zuheFrameComboBox);
			this.zuheGroupBox.Controls.Add(this.zuheCheckBox);
			this.zuheGroupBox.Controls.Add(this.zuheEnableGroupBox);
			this.zuheGroupBox.Controls.Add(this.label3);
			this.zuheGroupBox.Controls.Add(this.circleTimeNumericUpDown);
			this.zuheGroupBox.Location = new System.Drawing.Point(289, 19);
			this.zuheGroupBox.Margin = new System.Windows.Forms.Padding(2);
			this.zuheGroupBox.Name = "zuheGroupBox";
			this.zuheGroupBox.Padding = new System.Windows.Forms.Padding(2);
			this.zuheGroupBox.Size = new System.Drawing.Size(666, 241);
			this.zuheGroupBox.TabIndex = 1;
			this.zuheGroupBox.TabStop = false;
			this.zuheGroupBox.Text = "多场景组合播放设置";
			// 
			// mFrameSaveButton
			// 
			this.mFrameSaveButton.BackColor = System.Drawing.Color.Gainsboro;
			this.mFrameSaveButton.Location = new System.Drawing.Point(583, 20);
			this.mFrameSaveButton.Name = "mFrameSaveButton";
			this.mFrameSaveButton.Size = new System.Drawing.Size(70, 37);
			this.mFrameSaveButton.TabIndex = 3;
			this.mFrameSaveButton.Text = "保存当前";
			this.mFrameSaveButton.UseVisualStyleBackColor = false;
			this.mFrameSaveButton.Click += new System.EventHandler(this.multipleFrameSaveButton_Click);
			// 
			// zuheFrameComboBox
			// 
			this.zuheFrameComboBox.FormattingEnabled = true;
			this.zuheFrameComboBox.Location = new System.Drawing.Point(13, 26);
			this.zuheFrameComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.zuheFrameComboBox.Name = "zuheFrameComboBox";
			this.zuheFrameComboBox.Size = new System.Drawing.Size(92, 20);
			this.zuheFrameComboBox.TabIndex = 2;
			this.zuheFrameComboBox.SelectedIndexChanged += new System.EventHandler(this.zuheFrameComboBox_SelectedIndexChanged);
			// 
			// zuheCheckBox
			// 
			this.zuheCheckBox.AutoSize = true;
			this.zuheCheckBox.Location = new System.Drawing.Point(141, 29);
			this.zuheCheckBox.Margin = new System.Windows.Forms.Padding(2);
			this.zuheCheckBox.Name = "zuheCheckBox";
			this.zuheCheckBox.Size = new System.Drawing.Size(108, 16);
			this.zuheCheckBox.TabIndex = 1;
			this.zuheCheckBox.Text = "是否开启此功能";
			this.zuheCheckBox.UseVisualStyleBackColor = true;
			this.zuheCheckBox.CheckedChanged += new System.EventHandler(this.zuheCheckBox_CheckedChanged);
			// 
			// zuheEnableGroupBox
			// 
			this.zuheEnableGroupBox.Controls.Add(this.frame0NumericUpDown);
			this.zuheEnableGroupBox.Controls.Add(this.label34);
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
			this.zuheEnableGroupBox.Controls.Add(this.label35);
			this.zuheEnableGroupBox.Controls.Add(this.frame1numericUpDown);
			this.zuheEnableGroupBox.Controls.Add(this.label4);
			this.zuheEnableGroupBox.Enabled = false;
			this.zuheEnableGroupBox.Location = new System.Drawing.Point(4, 63);
			this.zuheEnableGroupBox.Margin = new System.Windows.Forms.Padding(2);
			this.zuheEnableGroupBox.Name = "zuheEnableGroupBox";
			this.zuheEnableGroupBox.Padding = new System.Windows.Forms.Padding(2);
			this.zuheEnableGroupBox.Size = new System.Drawing.Size(658, 174);
			this.zuheEnableGroupBox.TabIndex = 0;
			this.zuheEnableGroupBox.TabStop = false;
			this.zuheEnableGroupBox.Text = "播放设置";
			// 
			// frame0NumericUpDown
			// 
			this.frame0NumericUpDown.Location = new System.Drawing.Point(17, 102);
			this.frame0NumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.frame0NumericUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.frame0NumericUpDown.Name = "frame0NumericUpDown";
			this.frame0NumericUpDown.Size = new System.Drawing.Size(87, 21);
			this.frame0NumericUpDown.TabIndex = 4;
			this.frame0NumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Location = new System.Drawing.Point(14, 69);
			this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(107, 12);
			this.label34.TabIndex = 5;
			this.label34.Text = "主场景播放时间(s)";
			// 
			// frame4numericUpDown
			// 
			this.frame4numericUpDown.Location = new System.Drawing.Point(546, 104);
			this.frame4numericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.frame4numericUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.frame4numericUpDown.Name = "frame4numericUpDown";
			this.frame4numericUpDown.Size = new System.Drawing.Size(74, 21);
			this.frame4numericUpDown.TabIndex = 6;
			this.frame4numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// frame3numericUpDown
			// 
			this.frame3numericUpDown.Location = new System.Drawing.Point(436, 104);
			this.frame3numericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.frame3numericUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.frame3numericUpDown.Name = "frame3numericUpDown";
			this.frame3numericUpDown.Size = new System.Drawing.Size(74, 21);
			this.frame3numericUpDown.TabIndex = 5;
			this.frame3numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// frame2numericUpDown
			// 
			this.frame2numericUpDown.Location = new System.Drawing.Point(327, 104);
			this.frame2numericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.frame2numericUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.frame2numericUpDown.Name = "frame2numericUpDown";
			this.frame2numericUpDown.Size = new System.Drawing.Size(74, 21);
			this.frame2numericUpDown.TabIndex = 4;
			this.frame2numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// frame4ComboBox
			// 
			this.frame4ComboBox.FormattingEnabled = true;
			this.frame4ComboBox.Location = new System.Drawing.Point(545, 66);
			this.frame4ComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.frame4ComboBox.Name = "frame4ComboBox";
			this.frame4ComboBox.Size = new System.Drawing.Size(75, 20);
			this.frame4ComboBox.TabIndex = 3;
			// 
			// frame3ComboBox
			// 
			this.frame3ComboBox.FormattingEnabled = true;
			this.frame3ComboBox.Location = new System.Drawing.Point(435, 66);
			this.frame3ComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.frame3ComboBox.Name = "frame3ComboBox";
			this.frame3ComboBox.Size = new System.Drawing.Size(75, 20);
			this.frame3ComboBox.TabIndex = 3;
			// 
			// frame2ComboBox
			// 
			this.frame2ComboBox.FormattingEnabled = true;
			this.frame2ComboBox.Location = new System.Drawing.Point(325, 66);
			this.frame2ComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.frame2ComboBox.Name = "frame2ComboBox";
			this.frame2ComboBox.Size = new System.Drawing.Size(75, 20);
			this.frame2ComboBox.TabIndex = 3;
			// 
			// frame1ComboBox
			// 
			this.frame1ComboBox.FormattingEnabled = true;
			this.frame1ComboBox.Location = new System.Drawing.Point(215, 66);
			this.frame1ComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.frame1ComboBox.Name = "frame1ComboBox";
			this.frame1ComboBox.Size = new System.Drawing.Size(75, 20);
			this.frame1ComboBox.TabIndex = 3;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(149, 70);
			this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(53, 12);
			this.label9.TabIndex = 2;
			this.label9.Text = "场景类型";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(547, 37);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(59, 12);
			this.label7.TabIndex = 1;
			this.label7.Text = "组合场景4";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(436, 37);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(59, 12);
			this.label6.TabIndex = 1;
			this.label6.Text = "组合场景3";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(325, 37);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(59, 12);
			this.label5.TabIndex = 1;
			this.label5.Text = "组合场景2";
			// 
			// label35
			// 
			this.label35.AutoSize = true;
			this.label35.Location = new System.Drawing.Point(152, 108);
			this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(47, 12);
			this.label35.TabIndex = 1;
			this.label35.Text = "时间(s)";
			// 
			// frame1numericUpDown
			// 
			this.frame1numericUpDown.Location = new System.Drawing.Point(217, 104);
			this.frame1numericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.frame1numericUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.frame1numericUpDown.Name = "frame1numericUpDown";
			this.frame1numericUpDown.Size = new System.Drawing.Size(74, 21);
			this.frame1numericUpDown.TabIndex = 0;
			this.frame1numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(214, 37);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(59, 12);
			this.label4.TabIndex = 1;
			this.label4.Text = "组合场景1";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(308, 26);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 12);
			this.label3.TabIndex = 1;
			this.label3.Text = "总循环次数：";
			// 
			// circleTimeNumericUpDown
			// 
			this.circleTimeNumericUpDown.Enabled = false;
			this.circleTimeNumericUpDown.Location = new System.Drawing.Point(385, 23);
			this.circleTimeNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.circleTimeNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.circleTimeNumericUpDown.Name = "circleTimeNumericUpDown";
			this.circleTimeNumericUpDown.Size = new System.Drawing.Size(90, 21);
			this.circleTimeNumericUpDown.TabIndex = 0;
			this.circleTimeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.circleTimeNumericUpDown.Value = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(101, 28);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(41, 12);
			this.label21.TabIndex = 0;
			this.label21.Text = "步时间";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.myToolTip.SetToolTip(this.label21, "当前场景音频模式的统一步时间");
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(188, 28);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(53, 12);
			this.label22.TabIndex = 0;
			this.label22.Text = "间隔时间";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.myToolTip.SetToolTip(this.label22, "叠加后间隔时间");
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(656, 28);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(53, 12);
			this.label17.TabIndex = 9;
			this.label17.Text = "间隔时间";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.myToolTip.SetToolTip(this.label17, "叠加后间隔时间");
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(569, 28);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(41, 12);
			this.label18.TabIndex = 10;
			this.label18.Text = "步时间";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.myToolTip.SetToolTip(this.label18, "当前场景音频模式的统一步时间");
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel3.Controls.Add(this.label16);
			this.panel3.Controls.Add(this.label17);
			this.panel3.Controls.Add(this.label18);
			this.panel3.Controls.Add(this.label19);
			this.panel3.Controls.Add(this.skNoticeButton);
			this.panel3.Controls.Add(this.skSaveButton);
			this.panel3.Controls.Add(this.label23);
			this.panel3.Controls.Add(this.label22);
			this.panel3.Controls.Add(this.label21);
			this.panel3.Controls.Add(this.label12);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 17);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(962, 58);
			this.panel3.TabIndex = 0;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(751, 28);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(53, 12);
			this.label16.TabIndex = 8;
			this.label16.Text = "音频链表";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(476, 28);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(41, 12);
			this.label19.TabIndex = 11;
			this.label19.Text = "场景名";
			// 
			// skNoticeButton
			// 
			this.skNoticeButton.Location = new System.Drawing.Point(819, 6);
			this.skNoticeButton.Name = "skNoticeButton";
			this.skNoticeButton.Size = new System.Drawing.Size(52, 37);
			this.skNoticeButton.TabIndex = 6;
			this.skNoticeButton.Text = "提示";
			this.skNoticeButton.UseVisualStyleBackColor = true;
			this.skNoticeButton.Click += new System.EventHandler(this.skNoticeButton_Click);
			// 
			// skSaveButton
			// 
			this.skSaveButton.Location = new System.Drawing.Point(877, 6);
			this.skSaveButton.Name = "skSaveButton";
			this.skSaveButton.Size = new System.Drawing.Size(75, 37);
			this.skSaveButton.TabIndex = 7;
			this.skSaveButton.Text = "保存设置";
			this.skSaveButton.UseVisualStyleBackColor = true;
			this.skSaveButton.Click += new System.EventHandler(this.skSaveButton_Click);
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(282, 28);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(53, 12);
			this.label23.TabIndex = 0;
			this.label23.Text = "音频链表";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(8, 28);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(41, 12);
			this.label12.TabIndex = 0;
			this.label12.Text = "场景名";
			// 
			// skFlowLayoutPanel
			// 
			this.skFlowLayoutPanel.AutoScroll = true;
			this.skFlowLayoutPanel.BackColor = System.Drawing.Color.AliceBlue;
			this.skFlowLayoutPanel.Controls.Add(this.panel1);
			this.skFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skFlowLayoutPanel.Location = new System.Drawing.Point(3, 75);
			this.skFlowLayoutPanel.Name = "skFlowLayoutPanel";
			this.skFlowLayoutPanel.Size = new System.Drawing.Size(962, 266);
			this.skFlowLayoutPanel.TabIndex = 6;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.jgLabel);
			this.panel1.Controls.Add(this.stLabel);
			this.panel1.Controls.Add(this.lkTextBox);
			this.panel1.Controls.Add(this.jgNumericUpDown);
			this.panel1.Controls.Add(this.stNumericUpDown);
			this.panel1.Controls.Add(this.frameLabel);
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(460, 33);
			this.panel1.TabIndex = 12;
			this.panel1.Visible = false;
			// 
			// jgLabel
			// 
			this.jgLabel.AutoSize = true;
			this.jgLabel.Location = new System.Drawing.Point(250, 8);
			this.jgLabel.Name = "jgLabel";
			this.jgLabel.Size = new System.Drawing.Size(17, 12);
			this.jgLabel.TabIndex = 12;
			this.jgLabel.Text = "ms";
			// 
			// stLabel
			// 
			this.stLabel.AutoSize = true;
			this.stLabel.Location = new System.Drawing.Point(151, 8);
			this.stLabel.Name = "stLabel";
			this.stLabel.Size = new System.Drawing.Size(11, 12);
			this.stLabel.TabIndex = 11;
			this.stLabel.Text = "s";
			// 
			// lkTextBox
			// 
			this.lkTextBox.BackColor = System.Drawing.Color.White;
			this.lkTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lkTextBox.Location = new System.Drawing.Point(279, 3);
			this.lkTextBox.MaxLength = 20;
			this.lkTextBox.Multiline = true;
			this.lkTextBox.Name = "lkTextBox";
			this.lkTextBox.Size = new System.Drawing.Size(172, 22);
			this.lkTextBox.TabIndex = 10;
			this.lkTextBox.Text = "12345678901234567890";
			this.lkTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// jgNumericUpDown
			// 
			this.jgNumericUpDown.Font = new System.Drawing.Font("宋体", 10F);
			this.jgNumericUpDown.Location = new System.Drawing.Point(184, 4);
			this.jgNumericUpDown.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
			this.jgNumericUpDown.Name = "jgNumericUpDown";
			this.jgNumericUpDown.Size = new System.Drawing.Size(65, 23);
			this.jgNumericUpDown.TabIndex = 1;
			this.jgNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.jgNumericUpDown.Value = new decimal(new int[] {
            500000,
            0,
            0,
            0});
			// 
			// stNumericUpDown
			// 
			this.stNumericUpDown.Font = new System.Drawing.Font("宋体", 10F);
			this.stNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.stNumericUpDown.Location = new System.Drawing.Point(100, 4);
			this.stNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.stNumericUpDown.Name = "stNumericUpDown";
			this.stNumericUpDown.Size = new System.Drawing.Size(50, 23);
			this.stNumericUpDown.TabIndex = 1;
			this.stNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.stNumericUpDown.Value = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			// 
			// frameLabel
			// 
			this.frameLabel.AutoSize = true;
			this.frameLabel.Location = new System.Drawing.Point(8, 8);
			this.frameLabel.Name = "frameLabel";
			this.frameLabel.Size = new System.Drawing.Size(77, 12);
			this.frameLabel.TabIndex = 0;
			this.frameLabel.Text = "辅助场景十二";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.skFlowLayoutPanel);
			this.groupBox1.Controls.Add(this.panel3);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(4, 277);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(968, 344);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "音频场景设置";
			// 
			// GlobalSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(976, 625);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.dmxGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GlobalSetForm";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Text = "全局设置";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GlobalSetForm_FormClosed);
			this.Load += new System.EventHandler(this.GlobalSetForm_Load);
			this.dmxGroupBox.ResumeLayout(false);
			this.globalGroupBox.ResumeLayout(false);
			this.globalGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.eachStepTimeNumericUpDown)).EndInit();
			this.zuheGroupBox.ResumeLayout(false);
			this.zuheGroupBox.PerformLayout();
			this.zuheEnableGroupBox.ResumeLayout(false);
			this.zuheEnableGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.frame0NumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.frame4numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.frame3numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.frame2numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.frame1numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.circleTimeNumericUpDown)).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.skFlowLayoutPanel.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.jgNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.stNumericUpDown)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox dmxGroupBox;
		private System.Windows.Forms.GroupBox zuheGroupBox;
		private System.Windows.Forms.CheckBox zuheCheckBox;
		private System.Windows.Forms.GroupBox zuheEnableGroupBox;
		private System.Windows.Forms.ComboBox zuheFrameComboBox;
	
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown circleTimeNumericUpDown;
		private System.Windows.Forms.ComboBox[] skComboBoxes = new System.Windows.Forms.ComboBox[24];
		private System.Windows.Forms.Label label34;

		private System.Windows.Forms.ComboBox[] frameComboBoxes = new System.Windows.Forms.ComboBox[4];
		private System.Windows.Forms.ComboBox frame4ComboBox;
		private System.Windows.Forms.ComboBox frame2ComboBox;
		private System.Windows.Forms.ComboBox frame1ComboBox;
		private System.Windows.Forms.ComboBox frame3ComboBox;
		private System.Windows.Forms.NumericUpDown frame0NumericUpDown;
		private System.Windows.Forms.NumericUpDown[] frameNumericUpDowns = new System.Windows.Forms.NumericUpDown[4];
		private System.Windows.Forms.NumericUpDown frame4numericUpDown;
		private System.Windows.Forms.NumericUpDown frame3numericUpDown;
		private System.Windows.Forms.NumericUpDown frame2numericUpDown;
		private System.Windows.Forms.NumericUpDown frame1numericUpDown;
		//private System.Windows.Forms.ComboBox[] frameMethodComboBoxes = new System.Windows.Forms.ComboBox[4];
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.GroupBox globalGroupBox;
		private System.Windows.Forms.Label eachChangeModeLabel;
		private System.Windows.Forms.NumericUpDown eachStepTimeNumericUpDown;
		private System.Windows.Forms.Label eachStepTimeLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox eachChangeModeComboBox;
		private System.Windows.Forms.ComboBox startupComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox tongdaoCountComboBox;
				


		private Button globalSaveButton;
		private Button mFrameSaveButton;
		private ToolTip myToolTip;
		private Panel panel3;
		private FlowLayoutPanel skFlowLayoutPanel;
		private Label label22;
		private Label label21;
		private Label label12;
		private Label label23;
		private GroupBox groupBox1;
		private Button skNoticeButton;
		private Button skSaveButton;
		private Panel panel1;
		private Label jgLabel;
		private Label stLabel;
		private TextBox lkTextBox;
		private NumericUpDown jgNumericUpDown;
		private NumericUpDown stNumericUpDown;
		private Label frameLabel;
		private Label label16;
		private Label label17;
		private Label label18;
		private Label label19;
	}
}