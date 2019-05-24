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
			this.sceneSetButton = new System.Windows.Forms.Button();
			this.oneKeyButton = new System.Windows.Forms.Button();
			this.backupButton = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
			this.lightsListView = new System.Windows.Forms.ListView();
			this.lightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LargeImageList = new System.Windows.Forms.ImageList(this.components);
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
			this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
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
			this.lightsListView.Size = new System.Drawing.Size(1140, 162);
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1163, 562);
			this.Controls.Add(this.lightsListView);
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
			this.Controls.Add(this.comComboBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "智控配置";
			this.Load += new System.EventHandler(this.Form1_Load);
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
		private System.Windows.Forms.Button sceneSetButton;
		private System.Windows.Forms.Button oneKeyButton;
		private System.Windows.Forms.Button backupButton;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private Sunisoft.IrisSkin.SkinEngine skinEngine1;

		// 2019.5.23 : 添加listView 来存放加载的灯具列表
		private ListView lightsListView;
		public ListViewItem[] lightItems;
		private ColumnHeader lightType;
		private ImageList LargeImageList;
	}
}

