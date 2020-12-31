namespace LightController.MyForm.Multiplex
{
	partial class MultiplexForm
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
			this.lightsListView = new System.Windows.Forms.ListView();
			this.checkBox = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lightAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lightRemark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.allStepButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.endNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.startNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.timesNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.cancelButton = new System.Windows.Forms.Button();
			this.enterButton = new System.Windows.Forms.Button();
			this.allCheckBox = new System.Windows.Forms.CheckBox();
			this.noticeLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.endNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.startNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.timesNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// lightsListView
			// 
			this.lightsListView.CheckBoxes = true;
			this.lightsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.checkBox,
            this.lightType,
            this.lightAddr,
            this.lightRemark});
			this.lightsListView.Dock = System.Windows.Forms.DockStyle.Left;
			this.lightsListView.FullRowSelect = true;
			this.lightsListView.GridLines = true;
			this.lightsListView.HideSelection = false;
			this.lightsListView.Location = new System.Drawing.Point(0, 0);
			this.lightsListView.MultiSelect = false;
			this.lightsListView.Name = "lightsListView";
			this.lightsListView.Size = new System.Drawing.Size(360, 381);
			this.lightsListView.TabIndex = 15;
			this.lightsListView.UseCompatibleStateImageBehavior = false;
			this.lightsListView.View = System.Windows.Forms.View.Details;
			// 
			// checkBox
			// 
			this.checkBox.Text = "";
			this.checkBox.Width = 30;
			// 
			// lightType
			// 
			this.lightType.Text = "型号";
			this.lightType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lightType.Width = 138;
			// 
			// lightAddr
			// 
			this.lightAddr.Text = "通道地址";
			this.lightAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lightAddr.Width = 76;
			// 
			// lightRemark
			// 
			this.lightRemark.Text = "备注";
			this.lightRemark.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lightRemark.Width = 102;
			// 
			// allStepButton
			// 
			this.allStepButton.Location = new System.Drawing.Point(508, 137);
			this.allStepButton.Name = "allStepButton";
			this.allStepButton.Size = new System.Drawing.Size(71, 22);
			this.allStepButton.TabIndex = 20;
			this.allStepButton.Text = "全选";
			this.allStepButton.UseVisualStyleBackColor = true;
			this.allStepButton.Click += new System.EventHandler(this.allStepButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(477, 183);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(11, 12);
			this.label2.TabIndex = 19;
			this.label2.Text = "-";
			// 
			// endNumericUpDown
			// 
			this.endNumericUpDown.Location = new System.Drawing.Point(508, 179);
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
			this.endNumericUpDown.Size = new System.Drawing.Size(71, 21);
			this.endNumericUpDown.TabIndex = 17;
			this.endNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.endNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.endNumericUpDown.ValueChanged += new System.EventHandler(this.startEndNumericUpDown_ValueChanged);
			// 
			// startNumericUpDown
			// 
			this.startNumericUpDown.Location = new System.Drawing.Point(390, 179);
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
			this.startNumericUpDown.TabIndex = 18;
			this.startNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.startNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.startNumericUpDown.ValueChanged += new System.EventHandler(this.startEndNumericUpDown_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(388, 142);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 12);
			this.label1.TabIndex = 16;
			this.label1.Text = "请选择被复用的步数";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(390, 239);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(113, 12);
			this.label3.TabIndex = 21;
			this.label3.Text = "请设置复用的次数：";
			// 
			// timesNumericUpDown
			// 
			this.timesNumericUpDown.Location = new System.Drawing.Point(508, 235);
			this.timesNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.timesNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.timesNumericUpDown.Name = "timesNumericUpDown";
			this.timesNumericUpDown.Size = new System.Drawing.Size(71, 21);
			this.timesNumericUpDown.TabIndex = 18;
			this.timesNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.timesNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.cancelButton.Location = new System.Drawing.Point(508, 332);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(78, 28);
			this.cancelButton.TabIndex = 22;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// enterButton
			// 
			this.enterButton.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.enterButton.Location = new System.Drawing.Point(390, 332);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(78, 28);
			this.enterButton.TabIndex = 23;
			this.enterButton.Text = "复用";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// allCheckBox
			// 
			this.allCheckBox.AutoSize = true;
			this.allCheckBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.allCheckBox.Location = new System.Drawing.Point(388, 28);
			this.allCheckBox.Name = "allCheckBox";
			this.allCheckBox.Size = new System.Drawing.Size(82, 18);
			this.allCheckBox.TabIndex = 24;
			this.allCheckBox.Text = "灯具全选";
			this.allCheckBox.UseVisualStyleBackColor = true;
			this.allCheckBox.CheckedChanged += new System.EventHandler(this.allCheckBox_CheckedChanged);
			// 
			// noticeLabel
			// 
			this.noticeLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.noticeLabel.Location = new System.Drawing.Point(388, 50);
			this.noticeLabel.Name = "noticeLabel";
			this.noticeLabel.Size = new System.Drawing.Size(191, 40);
			this.noticeLabel.TabIndex = 25;
			this.noticeLabel.Text = "提示：未被选中的灯具仍会添加相应步数，但并非复用步数，而是采用该灯具的最后一步进行填充。";
			this.noticeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MultiplexForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.HighlightText;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(604, 381);
			this.Controls.Add(this.noticeLabel);
			this.Controls.Add(this.allCheckBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.allStepButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.endNumericUpDown);
			this.Controls.Add(this.timesNumericUpDown);
			this.Controls.Add(this.startNumericUpDown);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lightsListView);
			this.ForeColor = System.Drawing.Color.Black;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(620, 420);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(620, 420);
			this.Name = "MultiplexForm";
			this.Text = "多步复用（同步）";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.MultiplexForm_HelpButtonClicked);
			this.Load += new System.EventHandler(this.MultiplexForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.endNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.startNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.timesNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lightsListView;
		private System.Windows.Forms.ColumnHeader checkBox;
		private System.Windows.Forms.ColumnHeader lightRemark;
		private System.Windows.Forms.ColumnHeader lightType;
		private System.Windows.Forms.ColumnHeader lightAddr;
		private System.Windows.Forms.Button allStepButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown endNumericUpDown;
		private System.Windows.Forms.NumericUpDown startNumericUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown timesNumericUpDown;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.CheckBox allCheckBox;
		private System.Windows.Forms.Label noticeLabel;
	}
}