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
			this.ymButton = new System.Windows.Forms.Button();
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
			this.zuheSceneComboBox = new System.Windows.Forms.ComboBox();
			this.zuheCheckBox = new System.Windows.Forms.CheckBox();
			this.zuheEnableGroupBox = new System.Windows.Forms.GroupBox();
			this.scene0NumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label34 = new System.Windows.Forms.Label();
			this.scene4numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.scene3numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.scene2numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.scene4ComboBox = new System.Windows.Forms.ComboBox();
			this.scene3ComboBox = new System.Windows.Forms.ComboBox();
			this.scene2ComboBox = new System.Windows.Forms.ComboBox();
			this.scene1ComboBox = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label35 = new System.Windows.Forms.Label();
			this.scene1numericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.circleTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.label21 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label12 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.skNoticeButton = new System.Windows.Forms.Button();
			this.skSaveButton = new System.Windows.Forms.Button();
			this.skFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.skPanelDemo = new System.Windows.Forms.Panel();
			this.jgLabelDemo = new System.Windows.Forms.Label();
			this.stLabelDemo = new System.Windows.Forms.Label();
			this.lkTBDemo = new System.Windows.Forms.TextBox();
			this.jgNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.stNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.sceneLabelDemo = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label8 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.dmxGroupBox.SuspendLayout();
			this.globalGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.eachStepTimeNumericUpDown)).BeginInit();
			this.zuheGroupBox.SuspendLayout();
			this.zuheEnableGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scene0NumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.scene4numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.scene3numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.scene2numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.scene1numericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.circleTimeNumericUpDown)).BeginInit();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.skFLP.SuspendLayout();
			this.skPanelDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.jgNUDDemo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.stNUDDemo)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
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
			this.dmxGroupBox.Size = new System.Drawing.Size(1176, 267);
			this.dmxGroupBox.TabIndex = 0;
			this.dmxGroupBox.TabStop = false;
			this.dmxGroupBox.Text = "DMX512设置";
			// 
			// globalGroupBox
			// 
			this.globalGroupBox.BackColor = System.Drawing.Color.Transparent;
			this.globalGroupBox.Controls.Add(this.ymButton);
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
			// ymButton
			// 
			this.ymButton.Location = new System.Drawing.Point(23, 187);
			this.ymButton.Name = "ymButton";
			this.ymButton.Size = new System.Drawing.Size(84, 37);
			this.ymButton.TabIndex = 4;
			this.ymButton.Text = "摇麦设置";
			this.ymButton.UseVisualStyleBackColor = true;
			this.ymButton.Visible = false;
			this.ymButton.Click += new System.EventHandler(this.ymButton_Click);
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
			this.eachChangeModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
			this.startupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
			this.tongdaoCountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
			this.zuheGroupBox.Controls.Add(this.zuheSceneComboBox);
			this.zuheGroupBox.Controls.Add(this.zuheCheckBox);
			this.zuheGroupBox.Controls.Add(this.zuheEnableGroupBox);
			this.zuheGroupBox.Controls.Add(this.label3);
			this.zuheGroupBox.Controls.Add(this.circleTimeNumericUpDown);
			this.zuheGroupBox.Location = new System.Drawing.Point(289, 19);
			this.zuheGroupBox.Margin = new System.Windows.Forms.Padding(2);
			this.zuheGroupBox.Name = "zuheGroupBox";
			this.zuheGroupBox.Padding = new System.Windows.Forms.Padding(2);
			this.zuheGroupBox.Size = new System.Drawing.Size(884, 241);
			this.zuheGroupBox.TabIndex = 1;
			this.zuheGroupBox.TabStop = false;
			this.zuheGroupBox.Text = "多场景组合播放设置";
			// 
			// mFrameSaveButton
			// 
			this.mFrameSaveButton.BackColor = System.Drawing.Color.Gainsboro;
			this.mFrameSaveButton.Location = new System.Drawing.Point(806, 18);
			this.mFrameSaveButton.Name = "mFrameSaveButton";
			this.mFrameSaveButton.Size = new System.Drawing.Size(70, 37);
			this.mFrameSaveButton.TabIndex = 3;
			this.mFrameSaveButton.Text = "保存当前";
			this.mFrameSaveButton.UseVisualStyleBackColor = false;
			this.mFrameSaveButton.Click += new System.EventHandler(this.multipleFrameSaveButton_Click);
			// 
			// zuheSceneComboBox
			// 
			this.zuheSceneComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.zuheSceneComboBox.FormattingEnabled = true;
			this.zuheSceneComboBox.Location = new System.Drawing.Point(13, 26);
			this.zuheSceneComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.zuheSceneComboBox.Name = "zuheSceneComboBox";
			this.zuheSceneComboBox.Size = new System.Drawing.Size(158, 20);
			this.zuheSceneComboBox.TabIndex = 2;
			this.zuheSceneComboBox.SelectedIndexChanged += new System.EventHandler(this.zuheFrameComboBox_SelectedIndexChanged);
			// 
			// zuheCheckBox
			// 
			this.zuheCheckBox.AutoSize = true;
			this.zuheCheckBox.Location = new System.Drawing.Point(217, 28);
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
			this.zuheEnableGroupBox.Controls.Add(this.scene0NumericUpDown);
			this.zuheEnableGroupBox.Controls.Add(this.label34);
			this.zuheEnableGroupBox.Controls.Add(this.scene4numericUpDown);
			this.zuheEnableGroupBox.Controls.Add(this.scene3numericUpDown);
			this.zuheEnableGroupBox.Controls.Add(this.scene2numericUpDown);
			this.zuheEnableGroupBox.Controls.Add(this.scene4ComboBox);
			this.zuheEnableGroupBox.Controls.Add(this.scene3ComboBox);
			this.zuheEnableGroupBox.Controls.Add(this.scene2ComboBox);
			this.zuheEnableGroupBox.Controls.Add(this.scene1ComboBox);
			this.zuheEnableGroupBox.Controls.Add(this.label9);
			this.zuheEnableGroupBox.Controls.Add(this.label7);
			this.zuheEnableGroupBox.Controls.Add(this.label6);
			this.zuheEnableGroupBox.Controls.Add(this.label5);
			this.zuheEnableGroupBox.Controls.Add(this.label35);
			this.zuheEnableGroupBox.Controls.Add(this.scene1numericUpDown);
			this.zuheEnableGroupBox.Controls.Add(this.label4);
			this.zuheEnableGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.zuheEnableGroupBox.Enabled = false;
			this.zuheEnableGroupBox.Location = new System.Drawing.Point(2, 65);
			this.zuheEnableGroupBox.Margin = new System.Windows.Forms.Padding(2);
			this.zuheEnableGroupBox.Name = "zuheEnableGroupBox";
			this.zuheEnableGroupBox.Padding = new System.Windows.Forms.Padding(2);
			this.zuheEnableGroupBox.Size = new System.Drawing.Size(880, 174);
			this.zuheEnableGroupBox.TabIndex = 0;
			this.zuheEnableGroupBox.TabStop = false;
			this.zuheEnableGroupBox.Text = "播放设置";
			// 
			// scene0NumericUpDown
			// 
			this.scene0NumericUpDown.Location = new System.Drawing.Point(15, 102);
			this.scene0NumericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.scene0NumericUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.scene0NumericUpDown.Name = "scene0NumericUpDown";
			this.scene0NumericUpDown.Size = new System.Drawing.Size(87, 21);
			this.scene0NumericUpDown.TabIndex = 4;
			this.scene0NumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Location = new System.Drawing.Point(12, 69);
			this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(107, 12);
			this.label34.TabIndex = 5;
			this.label34.Text = "主场景播放时间(s)";
			// 
			// scene4numericUpDown
			// 
			this.scene4numericUpDown.Location = new System.Drawing.Point(714, 104);
			this.scene4numericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.scene4numericUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.scene4numericUpDown.Name = "scene4numericUpDown";
			this.scene4numericUpDown.Size = new System.Drawing.Size(74, 21);
			this.scene4numericUpDown.TabIndex = 6;
			this.scene4numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// scene3numericUpDown
			// 
			this.scene3numericUpDown.Location = new System.Drawing.Point(546, 104);
			this.scene3numericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.scene3numericUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.scene3numericUpDown.Name = "scene3numericUpDown";
			this.scene3numericUpDown.Size = new System.Drawing.Size(74, 21);
			this.scene3numericUpDown.TabIndex = 5;
			this.scene3numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// scene2numericUpDown
			// 
			this.scene2numericUpDown.Location = new System.Drawing.Point(378, 104);
			this.scene2numericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.scene2numericUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.scene2numericUpDown.Name = "scene2numericUpDown";
			this.scene2numericUpDown.Size = new System.Drawing.Size(74, 21);
			this.scene2numericUpDown.TabIndex = 4;
			this.scene2numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// scene4ComboBox
			// 
			this.scene4ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.scene4ComboBox.FormattingEnabled = true;
			this.scene4ComboBox.Location = new System.Drawing.Point(714, 66);
			this.scene4ComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.scene4ComboBox.Name = "scene4ComboBox";
			this.scene4ComboBox.Size = new System.Drawing.Size(158, 20);
			this.scene4ComboBox.TabIndex = 3;
			// 
			// scene3ComboBox
			// 
			this.scene3ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.scene3ComboBox.FormattingEnabled = true;
			this.scene3ComboBox.Location = new System.Drawing.Point(546, 66);
			this.scene3ComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.scene3ComboBox.Name = "scene3ComboBox";
			this.scene3ComboBox.Size = new System.Drawing.Size(158, 20);
			this.scene3ComboBox.TabIndex = 3;
			// 
			// scene2ComboBox
			// 
			this.scene2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.scene2ComboBox.FormattingEnabled = true;
			this.scene2ComboBox.Location = new System.Drawing.Point(378, 66);
			this.scene2ComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.scene2ComboBox.Name = "scene2ComboBox";
			this.scene2ComboBox.Size = new System.Drawing.Size(158, 20);
			this.scene2ComboBox.TabIndex = 3;
			// 
			// scene1ComboBox
			// 
			this.scene1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.scene1ComboBox.FormattingEnabled = true;
			this.scene1ComboBox.Location = new System.Drawing.Point(210, 66);
			this.scene1ComboBox.Margin = new System.Windows.Forms.Padding(2);
			this.scene1ComboBox.Name = "scene1ComboBox";
			this.scene1ComboBox.Size = new System.Drawing.Size(158, 20);
			this.scene1ComboBox.TabIndex = 3;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(145, 70);
			this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(53, 12);
			this.label9.TabIndex = 2;
			this.label9.Text = "场景类型";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(714, 37);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(59, 12);
			this.label7.TabIndex = 1;
			this.label7.Text = "组合场景4";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(546, 37);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(59, 12);
			this.label6.TabIndex = 1;
			this.label6.Text = "组合场景3";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(378, 37);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(59, 12);
			this.label5.TabIndex = 1;
			this.label5.Text = "组合场景2";
			// 
			// label35
			// 
			this.label35.AutoSize = true;
			this.label35.Location = new System.Drawing.Point(148, 108);
			this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(47, 12);
			this.label35.TabIndex = 1;
			this.label35.Text = "时间(s)";
			// 
			// scene1numericUpDown
			// 
			this.scene1numericUpDown.Location = new System.Drawing.Point(210, 104);
			this.scene1numericUpDown.Margin = new System.Windows.Forms.Padding(2);
			this.scene1numericUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.scene1numericUpDown.Name = "scene1numericUpDown";
			this.scene1numericUpDown.Size = new System.Drawing.Size(74, 21);
			this.scene1numericUpDown.TabIndex = 0;
			this.scene1numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(210, 37);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(59, 12);
			this.label4.TabIndex = 1;
			this.label4.Text = "组合场景1";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(384, 30);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 12);
			this.label3.TabIndex = 1;
			this.label3.Text = "总循环次数：";
			// 
			// circleTimeNumericUpDown
			// 
			this.circleTimeNumericUpDown.Enabled = false;
			this.circleTimeNumericUpDown.Location = new System.Drawing.Point(461, 26);
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
			this.label21.Location = new System.Drawing.Point(180, 20);
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
			this.label22.Location = new System.Drawing.Point(271, 20);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(53, 12);
			this.label22.TabIndex = 0;
			this.label22.Text = "间隔时间";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.myToolTip.SetToolTip(this.label22, "叠加后间隔时间");
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel3.Controls.Add(this.panel2);
			this.panel3.Controls.Add(this.skNoticeButton);
			this.panel3.Controls.Add(this.skSaveButton);
			this.panel3.Controls.Add(this.panel1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 17);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(1170, 58);
			this.panel3.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label12);
			this.panel2.Controls.Add(this.label21);
			this.panel2.Controls.Add(this.label22);
			this.panel2.Controls.Add(this.label23);
			this.panel2.Location = new System.Drawing.Point(1, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(454, 50);
			this.panel2.TabIndex = 13;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(11, 24);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(41, 12);
			this.label12.TabIndex = 0;
			this.label12.Text = "场景名";
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(377, 21);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(53, 12);
			this.label23.TabIndex = 0;
			this.label23.Text = "音频链表";
			// 
			// skNoticeButton
			// 
			this.skNoticeButton.Location = new System.Drawing.Point(1030, 9);
			this.skNoticeButton.Name = "skNoticeButton";
			this.skNoticeButton.Size = new System.Drawing.Size(52, 37);
			this.skNoticeButton.TabIndex = 6;
			this.skNoticeButton.Text = "提示";
			this.skNoticeButton.UseVisualStyleBackColor = true;
			this.skNoticeButton.Click += new System.EventHandler(this.skNoticeButton_Click);
			// 
			// skSaveButton
			// 
			this.skSaveButton.Location = new System.Drawing.Point(1088, 9);
			this.skSaveButton.Name = "skSaveButton";
			this.skSaveButton.Size = new System.Drawing.Size(75, 37);
			this.skSaveButton.TabIndex = 7;
			this.skSaveButton.Text = "保存设置";
			this.skSaveButton.UseVisualStyleBackColor = true;
			this.skSaveButton.Click += new System.EventHandler(this.skSaveButton_Click);
			// 
			// skFLP
			// 
			this.skFLP.AutoScroll = true;
			this.skFLP.BackColor = System.Drawing.Color.AliceBlue;
			this.skFLP.Controls.Add(this.skPanelDemo);
			this.skFLP.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skFLP.Location = new System.Drawing.Point(3, 75);
			this.skFLP.Name = "skFLP";
			this.skFLP.Size = new System.Drawing.Size(1170, 266);
			this.skFLP.TabIndex = 6;
			// 
			// skPanelDemo
			// 
			this.skPanelDemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.skPanelDemo.Controls.Add(this.jgLabelDemo);
			this.skPanelDemo.Controls.Add(this.stLabelDemo);
			this.skPanelDemo.Controls.Add(this.lkTBDemo);
			this.skPanelDemo.Controls.Add(this.jgNUDDemo);
			this.skPanelDemo.Controls.Add(this.stNUDDemo);
			this.skPanelDemo.Controls.Add(this.sceneLabelDemo);
			this.skPanelDemo.Location = new System.Drawing.Point(3, 3);
			this.skPanelDemo.Name = "skPanelDemo";
			this.skPanelDemo.Size = new System.Drawing.Size(569, 33);
			this.skPanelDemo.TabIndex = 12;
			this.skPanelDemo.Visible = false;
			// 
			// jgLabelDemo
			// 
			this.jgLabelDemo.AutoSize = true;
			this.jgLabelDemo.Location = new System.Drawing.Point(341, 7);
			this.jgLabelDemo.Name = "jgLabelDemo";
			this.jgLabelDemo.Size = new System.Drawing.Size(17, 12);
			this.jgLabelDemo.TabIndex = 12;
			this.jgLabelDemo.Text = "ms";
			// 
			// stLabelDemo
			// 
			this.stLabelDemo.AutoSize = true;
			this.stLabelDemo.Location = new System.Drawing.Point(244, 7);
			this.stLabelDemo.Name = "stLabelDemo";
			this.stLabelDemo.Size = new System.Drawing.Size(11, 12);
			this.stLabelDemo.TabIndex = 11;
			this.stLabelDemo.Text = "s";
			// 
			// lkTBDemo
			// 
			this.lkTBDemo.BackColor = System.Drawing.Color.White;
			this.lkTBDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lkTBDemo.Location = new System.Drawing.Point(374, 5);
			this.lkTBDemo.MaxLength = 20;
			this.lkTBDemo.Multiline = true;
			this.lkTBDemo.Name = "lkTBDemo";
			this.lkTBDemo.Size = new System.Drawing.Size(172, 22);
			this.lkTBDemo.TabIndex = 10;
			this.lkTBDemo.Text = "12345678901234567890";
			this.lkTBDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// jgNUDDemo
			// 
			this.jgNUDDemo.Font = new System.Drawing.Font("宋体", 10F);
			this.jgNUDDemo.Location = new System.Drawing.Point(269, 5);
			this.jgNUDDemo.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.jgNUDDemo.Name = "jgNUDDemo";
			this.jgNUDDemo.Size = new System.Drawing.Size(65, 23);
			this.jgNUDDemo.TabIndex = 1;
			this.jgNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.jgNUDDemo.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			// 
			// stNUDDemo
			// 
			this.stNUDDemo.DecimalPlaces = 2;
			this.stNUDDemo.Font = new System.Drawing.Font("宋体", 10F);
			this.stNUDDemo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.stNUDDemo.Location = new System.Drawing.Point(177, 4);
			this.stNUDDemo.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.stNUDDemo.Name = "stNUDDemo";
			this.stNUDDemo.Size = new System.Drawing.Size(60, 23);
			this.stNUDDemo.TabIndex = 1;
			this.stNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.stNUDDemo.Value = new decimal(new int[] {
            1250,
            0,
            0,
            131072});
			// 
			// sceneLabelDemo
			// 
			this.sceneLabelDemo.AutoSize = true;
			this.sceneLabelDemo.Location = new System.Drawing.Point(8, 8);
			this.sceneLabelDemo.Name = "sceneLabelDemo";
			this.sceneLabelDemo.Size = new System.Drawing.Size(77, 12);
			this.sceneLabelDemo.TabIndex = 0;
			this.sceneLabelDemo.Text = "辅助场景十二";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.skFLP);
			this.groupBox1.Controls.Add(this.panel3);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(4, 277);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1176, 344);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "音频场景设置";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.label13);
			this.panel1.Location = new System.Drawing.Point(576, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(454, 50);
			this.panel1.TabIndex = 13;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(9, 24);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(41, 12);
			this.label8.TabIndex = 0;
			this.label8.Text = "场景名";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(180, 20);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(41, 12);
			this.label10.TabIndex = 0;
			this.label10.Text = "步时间";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(271, 20);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(53, 12);
			this.label11.TabIndex = 0;
			this.label11.Text = "间隔时间";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(377, 21);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(53, 12);
			this.label13.TabIndex = 0;
			this.label13.Text = "音频链表";
			// 
			// GlobalSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(1184, 625);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.dmxGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GlobalSetForm";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Text = "工程全局设置";
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
			((System.ComponentModel.ISupportInitialize)(this.scene0NumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.scene4numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.scene3numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.scene2numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.scene1numericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.circleTimeNumericUpDown)).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.skFLP.ResumeLayout(false);
			this.skPanelDemo.ResumeLayout(false);
			this.skPanelDemo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.jgNUDDemo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.stNUDDemo)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox dmxGroupBox;
		private System.Windows.Forms.GroupBox zuheGroupBox;
		private System.Windows.Forms.CheckBox zuheCheckBox;
		private System.Windows.Forms.GroupBox zuheEnableGroupBox;
		private System.Windows.Forms.ComboBox zuheSceneComboBox;
	
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown circleTimeNumericUpDown;
		private System.Windows.Forms.ComboBox[] skComboBoxes = new System.Windows.Forms.ComboBox[24];
		private System.Windows.Forms.Label label34;

		private System.Windows.Forms.ComboBox[] sceneComboBoxes = new System.Windows.Forms.ComboBox[4];
		private System.Windows.Forms.ComboBox scene4ComboBox;
		private System.Windows.Forms.ComboBox scene2ComboBox;
		private System.Windows.Forms.ComboBox scene1ComboBox;
		private System.Windows.Forms.ComboBox scene3ComboBox;
		private System.Windows.Forms.NumericUpDown scene0NumericUpDown;
		private System.Windows.Forms.NumericUpDown[] sceneNumericUpDowns = new System.Windows.Forms.NumericUpDown[4];
		private System.Windows.Forms.NumericUpDown scene4numericUpDown;
		private System.Windows.Forms.NumericUpDown scene3numericUpDown;
		private System.Windows.Forms.NumericUpDown scene2numericUpDown;
		private System.Windows.Forms.NumericUpDown scene1numericUpDown;
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
		private FlowLayoutPanel skFLP;
		private Label label22;
		private Label label21;
		private Label label12;
		private Label label23;
		private GroupBox groupBox1;
		private Button skNoticeButton;
		private Button skSaveButton;
		private Panel skPanelDemo;
		private Label jgLabelDemo;
		private Label stLabelDemo;
		private TextBox lkTBDemo;
		private NumericUpDown jgNUDDemo;
		private NumericUpDown stNUDDemo;
		private Label sceneLabelDemo;
		private Button ymButton;
		private Panel panel2;
		private Panel panel1;
		private Label label8;
		private Label label10;
		private Label label11;
		private Label label13;
	}
}