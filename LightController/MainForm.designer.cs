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
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.openComButton = new System.Windows.Forms.Button();
			this.newFileButton = new System.Windows.Forms.Button();
			this.openFileButton = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.saveAsButton = new System.Windows.Forms.Button();
			this.exitButton = new System.Windows.Forms.Button();
			this.lightEditButton = new System.Windows.Forms.Button();
			this.globleSetButton = new System.Windows.Forms.Button();
			this.sceneSetButton = new System.Windows.Forms.Button();
			this.oneKeyButton = new System.Windows.Forms.Button();
			this.backupButton = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(11, 10);
			this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 28);
			this.comboBox1.TabIndex = 0;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
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
			this.openComButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// newFileButton
			// 
			this.newFileButton.Enabled = false;
			this.newFileButton.Location = new System.Drawing.Point(241, 8);
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
			this.openFileButton.Location = new System.Drawing.Point(300, 8);
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
			this.saveButton.Enabled = false;
			this.saveButton.Location = new System.Drawing.Point(360, 8);
			this.saveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(54, 32);
			this.saveButton.TabIndex = 4;
			this.saveButton.Text = "保存";
			this.saveButton.UseVisualStyleBackColor = true;
			// 
			// saveAsButton
			// 
			this.saveAsButton.Enabled = false;
			this.saveAsButton.Location = new System.Drawing.Point(420, 8);
			this.saveAsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.saveAsButton.Name = "saveAsButton";
			this.saveAsButton.Size = new System.Drawing.Size(54, 32);
			this.saveAsButton.TabIndex = 5;
			this.saveAsButton.Text = "另存";
			this.saveAsButton.UseVisualStyleBackColor = true;
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
			this.exitButton.Click += new System.EventHandler(this.button6_Click);
			// 
			// lightEditButton
			// 
			this.lightEditButton.Location = new System.Drawing.Point(479, 8);
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
			this.globleSetButton.Location = new System.Drawing.Point(567, 8);
			this.globleSetButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.globleSetButton.Name = "globleSetButton";
			this.globleSetButton.Size = new System.Drawing.Size(83, 32);
			this.globleSetButton.TabIndex = 2;
			this.globleSetButton.Text = "全局设置";
			this.globleSetButton.UseVisualStyleBackColor = true;
			// 
			// sceneSetButton
			// 
			this.sceneSetButton.Enabled = false;
			this.sceneSetButton.Location = new System.Drawing.Point(655, 8);
			this.sceneSetButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.sceneSetButton.Name = "sceneSetButton";
			this.sceneSetButton.Size = new System.Drawing.Size(82, 32);
			this.sceneSetButton.TabIndex = 3;
			this.sceneSetButton.Text = "场景设置";
			this.sceneSetButton.UseVisualStyleBackColor = true;
			// 
			// oneKeyButton
			// 
			this.oneKeyButton.Enabled = false;
			this.oneKeyButton.Location = new System.Drawing.Point(739, 8);
			this.oneKeyButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.oneKeyButton.Name = "oneKeyButton";
			this.oneKeyButton.Size = new System.Drawing.Size(82, 32);
			this.oneKeyButton.TabIndex = 4;
			this.oneKeyButton.Text = "一键配置";
			this.oneKeyButton.UseVisualStyleBackColor = true;
			// 
			// backupButton
			// 
			this.backupButton.Enabled = false;
			this.backupButton.Location = new System.Drawing.Point(826, 8);
			this.backupButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.backupButton.Name = "backupButton";
			this.backupButton.Size = new System.Drawing.Size(58, 32);
			this.backupButton.TabIndex = 5;
			this.backupButton.Text = "备份";
			this.backupButton.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Ivory;
			this.pictureBox1.Location = new System.Drawing.Point(11, 45);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(1126, 187);
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1163, 562);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.exitButton);
			this.Controls.Add(this.backupButton);
			this.Controls.Add(this.oneKeyButton);
			this.Controls.Add(this.saveAsButton);
			this.Controls.Add(this.sceneSetButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.globleSetButton);
			this.Controls.Add(this.openFileButton);
			this.Controls.Add(this.lightEditButton);
			this.Controls.Add(this.newFileButton);
			this.Controls.Add(this.openComButton);
			this.Controls.Add(this.comboBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "智控配置";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Button openComButton;
		private System.Windows.Forms.Button newFileButton;
		private System.Windows.Forms.Button openFileButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button saveAsButton;
		private System.Windows.Forms.Button exitButton;
		private System.Windows.Forms.Button lightEditButton;
		private System.Windows.Forms.Button globleSetButton;
		private System.Windows.Forms.Button sceneSetButton;
		private System.Windows.Forms.Button oneKeyButton;
		private System.Windows.Forms.Button backupButton;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private Sunisoft.IrisSkin.SkinEngine skinEngine1;
	}
}

