namespace RecordTools
{
	partial class ButtonForm
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
			this.tdButtonDemo = new System.Windows.Forms.Button();
			this.tdFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.myStatusStrip = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.frameComboBox = new System.Windows.Forms.ComboBox();
			this.minusButton = new System.Windows.Forms.Button();
			this.plusButton = new System.Windows.Forms.Button();
			this.mLKTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.jgtNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.stepTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.setFilePathButton = new System.Windows.Forms.Button();
			this.recordPathLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.binLabel = new System.Windows.Forms.Label();
			this.sceneNoTextBox = new System.Windows.Forms.TextBox();
			this.saveConfigButton = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.loadOldButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.pageFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.myStatusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.jgtNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.stepTimeNumericUpDown)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tdButtonDemo
			// 
			this.tdButtonDemo.BackColor = System.Drawing.Color.RosyBrown;
			this.tdButtonDemo.Location = new System.Drawing.Point(306, 56);
			this.tdButtonDemo.Name = "tdButtonDemo";
			this.tdButtonDemo.Size = new System.Drawing.Size(42, 42);
			this.tdButtonDemo.TabIndex = 0;
			this.tdButtonDemo.Text = "btn\r\nDemo";
			this.tdButtonDemo.UseVisualStyleBackColor = false;
			this.tdButtonDemo.Visible = false;
			// 
			// tdFLP
			// 
			this.tdFLP.BackColor = System.Drawing.Color.AliceBlue;
			this.tdFLP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tdFLP.Location = new System.Drawing.Point(10, 29);
			this.tdFLP.Name = "tdFLP";
			this.tdFLP.Size = new System.Drawing.Size(784, 54);
			this.tdFLP.TabIndex = 44;
			// 
			// myStatusStrip
			// 
			this.myStatusStrip.BackColor = System.Drawing.Color.LightGray;
			this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.myStatusStrip.Location = new System.Drawing.Point(0, 355);
			this.myStatusStrip.Name = "myStatusStrip";
			this.myStatusStrip.Size = new System.Drawing.Size(802, 22);
			this.myStatusStrip.SizingGrip = false;
			this.myStatusStrip.TabIndex = 48;
			this.myStatusStrip.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(0, 17);
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(627, 59);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 12);
			this.label3.TabIndex = 104;
			this.label3.Text = "开机场景：";
			// 
			// frameComboBox
			// 
			this.frameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.frameComboBox.FormattingEnabled = true;
			this.frameComboBox.Location = new System.Drawing.Point(627, 76);
			this.frameComboBox.Name = "frameComboBox";
			this.frameComboBox.Size = new System.Drawing.Size(82, 20);
			this.frameComboBox.TabIndex = 103;
			this.frameComboBox.SelectedIndexChanged += new System.EventHandler(this.frameComboBox_SelectedIndexChanged);
			// 
			// minusButton
			// 
			this.minusButton.Location = new System.Drawing.Point(515, 62);
			this.minusButton.Name = "minusButton";
			this.minusButton.Size = new System.Drawing.Size(25, 31);
			this.minusButton.TabIndex = 102;
			this.minusButton.Text = "-";
			this.minusButton.UseVisualStyleBackColor = true;
			this.minusButton.Click += new System.EventHandler(this.minusButton_Click);
			// 
			// plusButton
			// 
			this.plusButton.Location = new System.Drawing.Point(486, 62);
			this.plusButton.Name = "plusButton";
			this.plusButton.Size = new System.Drawing.Size(25, 31);
			this.plusButton.TabIndex = 101;
			this.plusButton.Text = "+";
			this.plusButton.UseVisualStyleBackColor = true;
			this.plusButton.Click += new System.EventHandler(this.plusButton_Click);
			// 
			// mLKTextBox
			// 
			this.mLKTextBox.BackColor = System.Drawing.Color.White;
			this.mLKTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mLKTextBox.Location = new System.Drawing.Point(555, 16);
			this.mLKTextBox.MaxLength = 20;
			this.mLKTextBox.Multiline = true;
			this.mLKTextBox.Name = "mLKTextBox";
			this.mLKTextBox.Size = new System.Drawing.Size(225, 22);
			this.mLKTextBox.TabIndex = 100;
			this.mLKTextBox.Text = "1";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(486, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 95;
			this.label2.Text = "音频链表：";
			// 
			// jgtNumericUpDown
			// 
			this.jgtNumericUpDown.Location = new System.Drawing.Point(403, 17);
			this.jgtNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.jgtNumericUpDown.Name = "jgtNumericUpDown";
			this.jgtNumericUpDown.Size = new System.Drawing.Size(55, 21);
			this.jgtNumericUpDown.TabIndex = 98;
			this.jgtNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// stepTimeNumericUpDown
			// 
			this.stepTimeNumericUpDown.DecimalPlaces = 2;
			this.stepTimeNumericUpDown.Location = new System.Drawing.Point(203, 17);
			this.stepTimeNumericUpDown.Name = "stepTimeNumericUpDown";
			this.stepTimeNumericUpDown.Size = new System.Drawing.Size(55, 21);
			this.stepTimeNumericUpDown.TabIndex = 99;
			this.stepTimeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// setFilePathButton
			// 
			this.setFilePathButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.setFilePathButton.Location = new System.Drawing.Point(21, 58);
			this.setFilePathButton.Name = "setFilePathButton";
			this.setFilePathButton.Size = new System.Drawing.Size(75, 39);
			this.setFilePathButton.TabIndex = 90;
			this.setFilePathButton.Text = "选择\r\n存放目录";
			this.setFilePathButton.UseVisualStyleBackColor = true;
			this.setFilePathButton.Click += new System.EventHandler(this.setFilePathButton_Click);
			// 
			// recordPathLabel
			// 
			this.recordPathLabel.Location = new System.Drawing.Point(106, 62);
			this.recordPathLabel.Name = "recordPathLabel";
			this.recordPathLabel.Size = new System.Drawing.Size(242, 30);
			this.recordPathLabel.TabIndex = 91;
			this.recordPathLabel.Text = "请选择存放目录。";
			this.recordPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(356, 71);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(65, 12);
			this.nameLabel.TabIndex = 93;
			this.nameLabel.Text = "文件名： M";
			// 
			// binLabel
			// 
			this.binLabel.AutoSize = true;
			this.binLabel.Location = new System.Drawing.Point(456, 71);
			this.binLabel.Name = "binLabel";
			this.binLabel.Size = new System.Drawing.Size(29, 12);
			this.binLabel.TabIndex = 92;
			this.binLabel.Text = ".bin";
			// 
			// sceneNoTextBox
			// 
			this.sceneNoTextBox.Location = new System.Drawing.Point(425, 67);
			this.sceneNoTextBox.MaxLength = 3;
			this.sceneNoTextBox.Name = "sceneNoTextBox";
			this.sceneNoTextBox.Size = new System.Drawing.Size(28, 21);
			this.sceneNoTextBox.TabIndex = 94;
			this.sceneNoTextBox.Text = "1";
			this.sceneNoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// saveConfigButton
			// 
			this.saveConfigButton.BackColor = System.Drawing.Color.MistyRose;
			this.saveConfigButton.Location = new System.Drawing.Point(718, 59);
			this.saveConfigButton.Name = "saveConfigButton";
			this.saveConfigButton.Size = new System.Drawing.Size(61, 37);
			this.saveConfigButton.TabIndex = 88;
			this.saveConfigButton.Text = "保存\r\n全局配置";
			this.saveConfigButton.UseVisualStyleBackColor = false;
			this.saveConfigButton.Click += new System.EventHandler(this.saveConfigButton_Click);
			// 
			// saveButton
			// 
			this.saveButton.BackColor = System.Drawing.Color.MistyRose;
			this.saveButton.Location = new System.Drawing.Point(548, 59);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(63, 37);
			this.saveButton.TabIndex = 87;
			this.saveButton.Text = "保存\r\n音频文件";
			this.saveButton.UseVisualStyleBackColor = false;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// loadOldButton
			// 
			this.loadOldButton.BackColor = System.Drawing.Color.Honeydew;
			this.loadOldButton.ForeColor = System.Drawing.Color.Black;
			this.loadOldButton.Location = new System.Drawing.Point(21, 9);
			this.loadOldButton.Name = "loadOldButton";
			this.loadOldButton.Size = new System.Drawing.Size(75, 37);
			this.loadOldButton.TabIndex = 89;
			this.loadOldButton.Text = "打开\r\n音频文件";
			this.loadOldButton.UseVisualStyleBackColor = false;
			this.loadOldButton.Click += new System.EventHandler(this.loadButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(285, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(125, 12);
			this.label1.TabIndex = 96;
			this.label1.Text = "叠加后间隔时间(ms)：";
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(112, 21);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(95, 12);
			this.label36.TabIndex = 97;
			this.label36.Text = "音频步时间(s)：";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tdFLP);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 252);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(802, 103);
			this.groupBox1.TabIndex = 105;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "选通道";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.pageFLP);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox2.Location = new System.Drawing.Point(0, 117);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(802, 135);
			this.groupBox2.TabIndex = 106;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "选灯具";
			// 
			// pageFLP
			// 
			this.pageFLP.BackColor = System.Drawing.Color.Azure;
			this.pageFLP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pageFLP.Location = new System.Drawing.Point(10, 20);
			this.pageFLP.Name = "pageFLP";
			this.pageFLP.Size = new System.Drawing.Size(784, 100);
			this.pageFLP.TabIndex = 44;
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "bin";
			this.openFileDialog.FileName = "M*.bin";
			this.openFileDialog.Filter = "(*.bin)|*.bin";
			this.openFileDialog.Title = "请打开格式为M*.bin的文件，否则程序可能出错！";
			// 
			// saveFolderBrowserDialog
			// 
			this.saveFolderBrowserDialog.Description = "请选择录制文件存放目录，本程序将会在点击《录制》按钮之后，将录制文件保存在该目录下。";
			this.saveFolderBrowserDialog.SelectedPath = "C:\\Temp\\CSJ";
			// 
			// ButtonForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.ClientSize = new System.Drawing.Size(802, 377);
			this.Controls.Add(this.tdButtonDemo);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.frameComboBox);
			this.Controls.Add(this.minusButton);
			this.Controls.Add(this.plusButton);
			this.Controls.Add(this.mLKTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.jgtNumericUpDown);
			this.Controls.Add(this.stepTimeNumericUpDown);
			this.Controls.Add(this.setFilePathButton);
			this.Controls.Add(this.recordPathLabel);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.binLabel);
			this.Controls.Add(this.sceneNoTextBox);
			this.Controls.Add(this.saveConfigButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.loadOldButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label36);
			this.Controls.Add(this.myStatusStrip);
			this.Name = "ButtonForm";
			this.Text = "ButtonForm";
			this.Load += new System.EventHandler(this.ButtonForm_Load);
			this.myStatusStrip.ResumeLayout(false);
			this.myStatusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.jgtNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.stepTimeNumericUpDown)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button tdButtonDemo;
		private System.Windows.Forms.FlowLayoutPanel tdFLP;
		private System.Windows.Forms.StatusStrip myStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox frameComboBox;
		private System.Windows.Forms.Button minusButton;
		private System.Windows.Forms.Button plusButton;
		private System.Windows.Forms.TextBox mLKTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown jgtNumericUpDown;
		private System.Windows.Forms.NumericUpDown stepTimeNumericUpDown;
		private System.Windows.Forms.Button setFilePathButton;
		private System.Windows.Forms.Label recordPathLabel;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label binLabel;
		private System.Windows.Forms.TextBox sceneNoTextBox;
		private System.Windows.Forms.Button saveConfigButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button loadOldButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.FlowLayoutPanel pageFLP;
		private System.Windows.Forms.ToolTip myToolTip;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.FolderBrowserDialog saveFolderBrowserDialog;
	}
}