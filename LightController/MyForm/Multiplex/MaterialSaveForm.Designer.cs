namespace LightController.MyForm
{
	partial class MaterialSaveForm
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
			this.mNameTextBox = new System.Windows.Forms.TextBox();
			this.mNameLabel = new System.Windows.Forms.Label();
			this.selectAllCheckBox = new System.Windows.Forms.CheckBox();
			this.noticeLabel = new System.Windows.Forms.Label();
			this.lightLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tdFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.tdCBDemo = new System.Windows.Forms.CheckBox();
			this.startNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.endNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.allStepButton = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.lightNameLabel = new System.Windows.Forms.Label();
			this.genericRadioButton = new System.Windows.Forms.RadioButton();
			this.lightRadioButton = new System.Windows.Forms.RadioButton();
			this.tempRadioButton = new System.Windows.Forms.RadioButton();
			this.panel1.SuspendLayout();
			this.tdFLP.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.startNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.endNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// mNameTextBox
			// 
			this.mNameTextBox.Enabled = false;
			this.mNameTextBox.Location = new System.Drawing.Point(382, 299);
			this.mNameTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.mNameTextBox.Name = "mNameTextBox";
			this.mNameTextBox.Size = new System.Drawing.Size(139, 21);
			this.mNameTextBox.TabIndex = 1;
			this.mNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mNameTextBox_KeyPress);
			// 
			// mNameLabel
			// 
			this.mNameLabel.AutoSize = true;
			this.mNameLabel.Location = new System.Drawing.Point(317, 303);
			this.mNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.mNameLabel.Name = "mNameLabel";
			this.mNameLabel.Size = new System.Drawing.Size(65, 12);
			this.mNameLabel.TabIndex = 2;
			this.mNameLabel.Text = "素材名称：";
			// 
			// selectAllCheckBox
			// 
			this.selectAllCheckBox.AutoSize = true;
			this.selectAllCheckBox.Font = new System.Drawing.Font("宋体", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.selectAllCheckBox.Location = new System.Drawing.Point(167, 19);
			this.selectAllCheckBox.Margin = new System.Windows.Forms.Padding(2);
			this.selectAllCheckBox.Name = "selectAllCheckBox";
			this.selectAllCheckBox.Size = new System.Drawing.Size(54, 18);
			this.selectAllCheckBox.TabIndex = 2;
			this.selectAllCheckBox.Text = "全选";
			this.selectAllCheckBox.UseVisualStyleBackColor = true;
			this.selectAllCheckBox.CheckedChanged += new System.EventHandler(this.selectAllCheckBox_CheckedChanged);
			// 
			// noticeLabel
			// 
			this.noticeLabel.AutoSize = true;
			this.noticeLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.noticeLabel.Location = new System.Drawing.Point(11, 21);
			this.noticeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.noticeLabel.Name = "noticeLabel";
			this.noticeLabel.Size = new System.Drawing.Size(133, 14);
			this.noticeLabel.TabIndex = 1;
			this.noticeLabel.Text = "请勾选要保存的通道";
			// 
			// lightLabel
			// 
			this.lightLabel.AutoSize = true;
			this.lightLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightLabel.Location = new System.Drawing.Point(315, 31);
			this.lightLabel.Name = "lightLabel";
			this.lightLabel.Size = new System.Drawing.Size(77, 14);
			this.lightLabel.TabIndex = 7;
			this.lightLabel.Text = "灯具名称：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(317, 110);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 12);
			this.label1.TabIndex = 8;
			this.label1.Text = "请选择要保存的步数";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tdFLP);
			this.panel1.Controls.Add(this.noticeLabel);
			this.panel1.Controls.Add(this.selectAllCheckBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(288, 393);
			this.panel1.TabIndex = 9;
			// 
			// tdFLP
			// 
			this.tdFLP.AutoScroll = true;
			this.tdFLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tdFLP.Controls.Add(this.tdCBDemo);
			this.tdFLP.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tdFLP.Location = new System.Drawing.Point(0, 52);
			this.tdFLP.Name = "tdFLP";
			this.tdFLP.Size = new System.Drawing.Size(288, 341);
			this.tdFLP.TabIndex = 5;
			// 
			// tdCBDemo
			// 
			this.tdCBDemo.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tdCBDemo.Location = new System.Drawing.Point(10, 5);
			this.tdCBDemo.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.tdCBDemo.Name = "tdCBDemo";
			this.tdCBDemo.Size = new System.Drawing.Size(111, 20);
			this.tdCBDemo.TabIndex = 0;
			this.tdCBDemo.Text = "最长的通道名";
			this.tdCBDemo.UseVisualStyleBackColor = true;
			this.tdCBDemo.Visible = false;
			// 
			// startNumericUpDown
			// 
			this.startNumericUpDown.Location = new System.Drawing.Point(317, 141);
			this.startNumericUpDown.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
			this.startNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.startNumericUpDown.Name = "startNumericUpDown";
			this.startNumericUpDown.Size = new System.Drawing.Size(68, 21);
			this.startNumericUpDown.TabIndex = 10;
			this.startNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.startNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(414, 145);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 12);
			this.label2.TabIndex = 11;
			this.label2.Text = "- ";
			// 
			// endNumericUpDown
			// 
			this.endNumericUpDown.Location = new System.Drawing.Point(453, 141);
			this.endNumericUpDown.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
			this.endNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.endNumericUpDown.Name = "endNumericUpDown";
			this.endNumericUpDown.Size = new System.Drawing.Size(68, 21);
			this.endNumericUpDown.TabIndex = 10;
			this.endNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.endNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// allStepButton
			// 
			this.allStepButton.Location = new System.Drawing.Point(443, 105);
			this.allStepButton.Name = "allStepButton";
			this.allStepButton.Size = new System.Drawing.Size(78, 22);
			this.allStepButton.TabIndex = 13;
			this.allStepButton.Text = "全选";
			this.allStepButton.UseVisualStyleBackColor = true;
			this.allStepButton.Click += new System.EventHandler(this.allStepSkinButton_Click);
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(323, 349);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(78, 26);
			this.saveButton.TabIndex = 14;
			this.saveButton.Text = "保存";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(439, 349);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(78, 26);
			this.cancelButton.TabIndex = 14;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// lightNameLabel
			// 
			this.lightNameLabel.AutoSize = true;
			this.lightNameLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightNameLabel.Location = new System.Drawing.Point(332, 52);
			this.lightNameLabel.Name = "lightNameLabel";
			this.lightNameLabel.Size = new System.Drawing.Size(57, 12);
			this.lightNameLabel.TabIndex = 7;
			this.lightNameLabel.Text = "灯具名称";
			// 
			// genericRadioButton
			// 
			this.genericRadioButton.AutoSize = true;
			this.genericRadioButton.Location = new System.Drawing.Point(321, 263);
			this.genericRadioButton.Name = "genericRadioButton";
			this.genericRadioButton.Size = new System.Drawing.Size(71, 16);
			this.genericRadioButton.TabIndex = 15;
			this.genericRadioButton.Text = "通用素材";
			this.genericRadioButton.UseVisualStyleBackColor = true;
			// 
			// lightRadioButton
			// 
			this.lightRadioButton.AutoSize = true;
			this.lightRadioButton.Location = new System.Drawing.Point(425, 263);
			this.lightRadioButton.Name = "lightRadioButton";
			this.lightRadioButton.Size = new System.Drawing.Size(71, 16);
			this.lightRadioButton.TabIndex = 15;
			this.lightRadioButton.Text = "单灯素材";
			this.lightRadioButton.UseVisualStyleBackColor = true;
			// 
			// tempRadioButton
			// 
			this.tempRadioButton.AutoSize = true;
			this.tempRadioButton.Checked = true;
			this.tempRadioButton.Location = new System.Drawing.Point(320, 231);
			this.tempRadioButton.Name = "tempRadioButton";
			this.tempRadioButton.Size = new System.Drawing.Size(71, 16);
			this.tempRadioButton.TabIndex = 15;
			this.tempRadioButton.TabStop = true;
			this.tempRadioButton.Text = "临时素材";
			this.tempRadioButton.UseVisualStyleBackColor = true;
			this.tempRadioButton.CheckedChanged += new System.EventHandler(this.tempRadioButton_CheckedChanged);
			// 
			// MaterialSaveForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(542, 393);
			this.Controls.Add(this.lightRadioButton);
			this.Controls.Add(this.tempRadioButton);
			this.Controls.Add(this.genericRadioButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.allStepButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.endNumericUpDown);
			this.Controls.Add(this.startNumericUpDown);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lightNameLabel);
			this.Controls.Add(this.lightLabel);
			this.Controls.Add(this.mNameLabel);
			this.Controls.Add(this.mNameTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HelpButton = true;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MaterialSaveForm";
			this.Text = "保存素材 ";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.MaterialSaveForm_HelpButtonClicked);
			this.Load += new System.EventHandler(this.MaterialForm_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tdFLP.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.startNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.endNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox mNameTextBox;
		private System.Windows.Forms.Label mNameLabel;
		private System.Windows.Forms.Label noticeLabel;

		private System.Windows.Forms.CheckBox selectAllCheckBox;
		private System.Windows.Forms.Label lightLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.NumericUpDown startNumericUpDown;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown endNumericUpDown;
		private System.Windows.Forms.Button allStepButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label lightNameLabel;
        private System.Windows.Forms.RadioButton genericRadioButton;
        private System.Windows.Forms.RadioButton lightRadioButton;
		private System.Windows.Forms.RadioButton tempRadioButton;
		private System.Windows.Forms.FlowLayoutPanel tdFLP;
		private System.Windows.Forms.CheckBox tdCBDemo;
	}
}