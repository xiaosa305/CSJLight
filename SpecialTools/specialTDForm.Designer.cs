﻿namespace SpecialTools
{
	partial class specialTDForm
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
			this.tdTextBox = new System.Windows.Forms.TextBox();
			this.generateButton = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.tdPanelDemo = new System.Windows.Forms.Panel();
			this.tdNUDDemo = new System.Windows.Forms.NumericUpDown();
			this.tdCBDemo = new System.Windows.Forms.CheckBox();
			this.tdLabelDemo = new System.Windows.Forms.Label();
			this.tdFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.saveButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.tdPanelDemo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tdNUDDemo)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tdTextBox
			// 
			this.tdTextBox.Location = new System.Drawing.Point(9, 14);
			this.tdTextBox.Name = "tdTextBox";
			this.tdTextBox.Size = new System.Drawing.Size(259, 21);
			this.tdTextBox.TabIndex = 0;
			this.tdTextBox.Text = "503 504 505 506 507 508 509 510 511 512 ";
			// 
			// generateButton
			// 
			this.generateButton.BackColor = System.Drawing.Color.LightGray;
			this.generateButton.Location = new System.Drawing.Point(294, 14);
			this.generateButton.Name = "generateButton";
			this.generateButton.Size = new System.Drawing.Size(69, 21);
			this.generateButton.TabIndex = 1;
			this.generateButton.Text = "生成";
			this.generateButton.UseVisualStyleBackColor = false;
			this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(83, 48);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(69, 20);
			this.comboBox1.TabIndex = 3;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(215, 19);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(62, 21);
			this.numericUpDown1.TabIndex = 5;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(170, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 6;
			this.label1.Text = "步进值";
			// 
			// tdPanelDemo
			// 
			this.tdPanelDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tdPanelDemo.Controls.Add(this.tdNUDDemo);
			this.tdPanelDemo.Controls.Add(this.tdCBDemo);
			this.tdPanelDemo.Controls.Add(this.tdLabelDemo);
			this.tdPanelDemo.Location = new System.Drawing.Point(198, 46);
			this.tdPanelDemo.Name = "tdPanelDemo";
			this.tdPanelDemo.Size = new System.Drawing.Size(177, 26);
			this.tdPanelDemo.TabIndex = 4;
			this.tdPanelDemo.Visible = false;
			// 
			// tdNUDDemo
			// 
			this.tdNUDDemo.Location = new System.Drawing.Point(120, 1);
			this.tdNUDDemo.Name = "tdNUDDemo";
			this.tdNUDDemo.Size = new System.Drawing.Size(47, 21);
			this.tdNUDDemo.TabIndex = 2;
			this.tdNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tdCBDemo
			// 
			this.tdCBDemo.AutoSize = true;
			this.tdCBDemo.Location = new System.Drawing.Point(66, 3);
			this.tdCBDemo.Name = "tdCBDemo";
			this.tdCBDemo.Size = new System.Drawing.Size(48, 16);
			this.tdCBDemo.TabIndex = 1;
			this.tdCBDemo.Text = "启用";
			this.tdCBDemo.UseVisualStyleBackColor = true;
			// 
			// tdLabelDemo
			// 
			this.tdLabelDemo.AutoSize = true;
			this.tdLabelDemo.Location = new System.Drawing.Point(8, 5);
			this.tdLabelDemo.Name = "tdLabelDemo";
			this.tdLabelDemo.Size = new System.Drawing.Size(47, 12);
			this.tdLabelDemo.TabIndex = 0;
			this.tdLabelDemo.Text = "通道512";
			// 
			// tdFLP
			// 
			this.tdFLP.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tdFLP.Location = new System.Drawing.Point(0, 78);
			this.tdFLP.Name = "tdFLP";
			this.tdFLP.Size = new System.Drawing.Size(375, 164);
			this.tdFLP.TabIndex = 7;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 300);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(375, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 8;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(360, 17);
			this.myStatusLabel.Spring = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.checkBox1);
			this.panel1.Controls.Add(this.saveButton);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.numericUpDown1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 242);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(375, 58);
			this.panel1.TabIndex = 9;
			// 
			// saveButton
			// 
			this.saveButton.BackColor = System.Drawing.Color.Salmon;
			this.saveButton.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.saveButton.Location = new System.Drawing.Point(293, 12);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 35);
			this.saveButton.TabIndex = 7;
			this.saveButton.Text = "导出文件";
			this.saveButton.UseVisualStyleBackColor = false;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "请选择场景";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(12, 21);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(72, 16);
			this.checkBox1.TabIndex = 10;
			this.checkBox1.Text = "常亮模式";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// specialTDForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(375, 322);
			this.Controls.Add(this.tdPanelDemo);
			this.Controls.Add(this.tdFLP);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.generateButton);
			this.Controls.Add(this.tdTextBox);
			this.Name = "specialTDForm";
			this.Text = "特定DMX512灯光工具";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.tdPanelDemo.ResumeLayout(false);
			this.tdPanelDemo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tdNUDDemo)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tdTextBox;
		private System.Windows.Forms.Button generateButton;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel tdPanelDemo;
		private System.Windows.Forms.NumericUpDown tdNUDDemo;
		private System.Windows.Forms.CheckBox tdCBDemo;
		private System.Windows.Forms.Label tdLabelDemo;
		private System.Windows.Forms.FlowLayoutPanel tdFLP;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.CheckBox checkBox1;
	}
}

