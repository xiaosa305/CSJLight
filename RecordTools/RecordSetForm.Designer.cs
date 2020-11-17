namespace RecordTools
{
	partial class RecordSetForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordSetForm));
			this.nextButton = new System.Windows.Forms.Button();
			this.previousButton = new System.Windows.Forms.Button();
			this.bigFLP = new System.Windows.Forms.FlowLayoutPanel();
			this.saveButton = new System.Windows.Forms.Button();
			this.loadOldButton = new System.Windows.Forms.Button();
			this.pageLabel = new System.Windows.Forms.Label();
			this.myStatusStrip = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.setFilePathButton = new System.Windows.Forms.Button();
			this.recordPathLabel = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.binLabel = new System.Windows.Forms.Label();
			this.sceneNoTextBox = new System.Windows.Forms.TextBox();
			this.mLKTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.jgtNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.stepTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.minusButton = new System.Windows.Forms.Button();
			this.plusButton = new System.Windows.Forms.Button();
			this.saveFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.frameComboBox = new System.Windows.Forms.ComboBox();
			this.saveButton2 = new System.Windows.Forms.Button();
			this.myStatusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.jgtNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.stepTimeNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// nextButton
			// 
			this.nextButton.Location = new System.Drawing.Point(723, 420);
			this.nextButton.Name = "nextButton";
			this.nextButton.Size = new System.Drawing.Size(61, 23);
			this.nextButton.TabIndex = 44;
			this.nextButton.Text = "下一页";
			this.nextButton.UseVisualStyleBackColor = true;
			this.nextButton.Click += new System.EventHandler(this.pageButton_Click);
			// 
			// previousButton
			// 
			this.previousButton.Location = new System.Drawing.Point(605, 420);
			this.previousButton.Name = "previousButton";
			this.previousButton.Size = new System.Drawing.Size(61, 23);
			this.previousButton.TabIndex = 45;
			this.previousButton.Text = "上一页";
			this.previousButton.UseVisualStyleBackColor = true;
			this.previousButton.Click += new System.EventHandler(this.pageButton_Click);
			// 
			// bigFLP
			// 
			this.bigFLP.BackColor = System.Drawing.Color.AliceBlue;
			this.bigFLP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.bigFLP.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bigFLP.Location = new System.Drawing.Point(0, 105);
			this.bigFLP.Name = "bigFLP";
			this.bigFLP.Size = new System.Drawing.Size(784, 314);
			this.bigFLP.TabIndex = 43;
			// 
			// saveButton
			// 
			this.saveButton.BackColor = System.Drawing.Color.MistyRose;
			this.saveButton.Location = new System.Drawing.Point(547, 59);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(63, 37);
			this.saveButton.TabIndex = 41;
			this.saveButton.Text = "保存\r\n音频文件";
			this.saveButton.UseVisualStyleBackColor = false;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// loadOldButton
			// 
			this.loadOldButton.BackColor = System.Drawing.Color.Honeydew;
			this.loadOldButton.ForeColor = System.Drawing.Color.Black;
			this.loadOldButton.Location = new System.Drawing.Point(13, 9);
			this.loadOldButton.Name = "loadOldButton";
			this.loadOldButton.Size = new System.Drawing.Size(75, 37);
			this.loadOldButton.TabIndex = 42;
			this.loadOldButton.Text = "打开\r\n音频文件";
			this.loadOldButton.UseVisualStyleBackColor = false;
			this.loadOldButton.Click += new System.EventHandler(this.loadButton_Click);
			// 
			// pageLabel
			// 
			this.pageLabel.AutoSize = true;
			this.pageLabel.BackColor = System.Drawing.Color.LightGray;
			this.pageLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pageLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.pageLabel.Location = new System.Drawing.Point(683, 425);
			this.pageLabel.Name = "pageLabel";
			this.pageLabel.Size = new System.Drawing.Size(31, 14);
			this.pageLabel.TabIndex = 46;
			this.pageLabel.Text = "1/6";
			// 
			// myStatusStrip
			// 
			this.myStatusStrip.BackColor = System.Drawing.Color.LightGray;
			this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.myStatusStrip.Location = new System.Drawing.Point(0, 419);
			this.myStatusStrip.Name = "myStatusStrip";
			this.myStatusStrip.Size = new System.Drawing.Size(784, 22);
			this.myStatusStrip.SizingGrip = false;
			this.myStatusStrip.TabIndex = 47;
			this.myStatusStrip.Text = "statusStrip1";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(0, 17);
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// setFilePathButton
			// 
			this.setFilePathButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.setFilePathButton.Location = new System.Drawing.Point(13, 58);
			this.setFilePathButton.Name = "setFilePathButton";
			this.setFilePathButton.Size = new System.Drawing.Size(75, 39);
			this.setFilePathButton.TabIndex = 73;
			this.setFilePathButton.Text = "选择\r\n存放目录";
			this.setFilePathButton.UseVisualStyleBackColor = true;
			this.setFilePathButton.Click += new System.EventHandler(this.setFilePathButton_Click);
			// 
			// recordPathLabel
			// 
			this.recordPathLabel.Location = new System.Drawing.Point(98, 62);
			this.recordPathLabel.Name = "recordPathLabel";
			this.recordPathLabel.Size = new System.Drawing.Size(242, 30);
			this.recordPathLabel.TabIndex = 74;
			this.recordPathLabel.Text = "请选择存放目录。";
			this.recordPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(348, 71);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(65, 12);
			this.nameLabel.TabIndex = 76;
			this.nameLabel.Text = "文件名： M";
			// 
			// binLabel
			// 
			this.binLabel.AutoSize = true;
			this.binLabel.Location = new System.Drawing.Point(448, 71);
			this.binLabel.Name = "binLabel";
			this.binLabel.Size = new System.Drawing.Size(29, 12);
			this.binLabel.TabIndex = 75;
			this.binLabel.Text = ".bin";
			// 
			// sceneNoTextBox
			// 
			this.sceneNoTextBox.Location = new System.Drawing.Point(417, 67);
			this.sceneNoTextBox.MaxLength = 3;
			this.sceneNoTextBox.Name = "sceneNoTextBox";
			this.sceneNoTextBox.Size = new System.Drawing.Size(28, 21);
			this.sceneNoTextBox.TabIndex = 77;
			this.sceneNoTextBox.Text = "1";
			this.sceneNoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// mLKTextBox
			// 
			this.mLKTextBox.BackColor = System.Drawing.Color.White;
			this.mLKTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mLKTextBox.Location = new System.Drawing.Point(547, 16);
			this.mLKTextBox.MaxLength = 20;
			this.mLKTextBox.Multiline = true;
			this.mLKTextBox.Name = "mLKTextBox";
			this.mLKTextBox.Size = new System.Drawing.Size(225, 22);
			this.mLKTextBox.TabIndex = 82;
			this.mLKTextBox.Text = "12345678900987654321";
			this.mLKTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mLKTextBox_KeyPress);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(277, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(125, 12);
			this.label1.TabIndex = 78;
			this.label1.Text = "叠加后间隔时间(ms)：";
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(104, 21);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(95, 12);
			this.label36.TabIndex = 79;
			this.label36.Text = "音频步时间(s)：";
			// 
			// jgtNumericUpDown
			// 
			this.jgtNumericUpDown.Location = new System.Drawing.Point(395, 17);
			this.jgtNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.jgtNumericUpDown.Name = "jgtNumericUpDown";
			this.jgtNumericUpDown.Size = new System.Drawing.Size(55, 21);
			this.jgtNumericUpDown.TabIndex = 80;
			this.jgtNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// stepTimeNumericUpDown
			// 
			this.stepTimeNumericUpDown.DecimalPlaces = 2;
			this.stepTimeNumericUpDown.Location = new System.Drawing.Point(195, 17);
			this.stepTimeNumericUpDown.Name = "stepTimeNumericUpDown";
			this.stepTimeNumericUpDown.Size = new System.Drawing.Size(55, 21);
			this.stepTimeNumericUpDown.TabIndex = 81;
			this.stepTimeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.stepTimeNumericUpDown.ValueChanged += new System.EventHandler(this.stepTimeNumericUpDown_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(478, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 78;
			this.label2.Text = "音频链表：";
			this.myToolTip.SetToolTip(this.label2, "请在文本框内输入每一次执行的步数（范围为1-9），\r\n并将每步数字连在一起（如1234）；\r\n若设为\"0\"或空字符串，则表示该场景不执行声控模式；\r\n链表数量不可" +
        "超过20个。");
			// 
			// minusButton
			// 
			this.minusButton.Location = new System.Drawing.Point(508, 62);
			this.minusButton.Name = "minusButton";
			this.minusButton.Size = new System.Drawing.Size(25, 31);
			this.minusButton.TabIndex = 84;
			this.minusButton.Text = "-";
			this.minusButton.UseVisualStyleBackColor = true;
			this.minusButton.Click += new System.EventHandler(this.minusButton_Click);
			// 
			// plusButton
			// 
			this.plusButton.Location = new System.Drawing.Point(483, 62);
			this.plusButton.Name = "plusButton";
			this.plusButton.Size = new System.Drawing.Size(25, 31);
			this.plusButton.TabIndex = 83;
			this.plusButton.Text = "+";
			this.plusButton.UseVisualStyleBackColor = true;
			this.plusButton.Click += new System.EventHandler(this.plusButton_Click);
			// 
			// saveFolderBrowserDialog
			// 
			this.saveFolderBrowserDialog.Description = "请选择录制文件存放目录，本程序将会在点击《录制》按钮之后，将录制文件保存在该目录下。";
			this.saveFolderBrowserDialog.SelectedPath = "C:\\Temp\\CSJ";
			// 
			// frameComboBox
			// 
			this.frameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.frameComboBox.FormattingEnabled = true;
			this.frameComboBox.Location = new System.Drawing.Point(623, 67);
			this.frameComboBox.Name = "frameComboBox";
			this.frameComboBox.Size = new System.Drawing.Size(82, 20);
			this.frameComboBox.TabIndex = 85;
			this.frameComboBox.SelectedIndexChanged += new System.EventHandler(this.frameComboBox_SelectedIndexChanged);
			// 
			// saveButton2
			// 
			this.saveButton2.BackColor = System.Drawing.Color.MistyRose;
			this.saveButton2.Location = new System.Drawing.Point(711, 59);
			this.saveButton2.Name = "saveButton2";
			this.saveButton2.Size = new System.Drawing.Size(61, 37);
			this.saveButton2.TabIndex = 41;
			this.saveButton2.Text = "保存\r\n配置文件";
			this.saveButton2.UseVisualStyleBackColor = false;
			this.saveButton2.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// RecordSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.ClientSize = new System.Drawing.Size(784, 441);
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
			this.Controls.Add(this.nextButton);
			this.Controls.Add(this.previousButton);
			this.Controls.Add(this.bigFLP);
			this.Controls.Add(this.saveButton2);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.loadOldButton);
			this.Controls.Add(this.pageLabel);
			this.Controls.Add(this.myStatusStrip);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label36);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(800, 480);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(800, 480);
			this.Name = "RecordSetForm";
			this.Text = "录播文件·音频通道选择器";
			this.Load += new System.EventHandler(this.RecordSetForm_Load);
			this.myStatusStrip.ResumeLayout(false);
			this.myStatusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.jgtNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.stepTimeNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button nextButton;
		private System.Windows.Forms.Button previousButton;
		private System.Windows.Forms.FlowLayoutPanel bigFLP;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button loadOldButton;
		private System.Windows.Forms.Label pageLabel;
		private System.Windows.Forms.StatusStrip myStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button setFilePathButton;
		private System.Windows.Forms.Label recordPathLabel;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label binLabel;
		private System.Windows.Forms.TextBox sceneNoTextBox;
		private System.Windows.Forms.TextBox mLKTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.NumericUpDown jgtNumericUpDown;
		private System.Windows.Forms.NumericUpDown stepTimeNumericUpDown;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolTip myToolTip;
		private System.Windows.Forms.Button minusButton;
		private System.Windows.Forms.Button plusButton;
		private System.Windows.Forms.FolderBrowserDialog saveFolderBrowserDialog;
		private System.Windows.Forms.ComboBox frameComboBox;
		private System.Windows.Forms.Button saveButton2;
	}
}

