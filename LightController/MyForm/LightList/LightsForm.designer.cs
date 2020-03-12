using System.Windows.Forms;

namespace LightController
{
	partial class LightsForm
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
			//System.Console.WriteLine("lightForm is dispose");
			this.Hide();

			//if (disposing && (components != null))
			//{
			//	components.Dispose();
			//}
			//base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LightsForm));
			this.largeImageList = new System.Windows.Forms.ImageList(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.skinTreeView1 = new CCWin.SkinControl.SkinTreeView();
			this.addButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.enterButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.lightsListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// largeImageList
			// 
			this.largeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeImageList.ImageStream")));
			this.largeImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.largeImageList.Images.SetKeyName(0, "RGB.ico");
			this.largeImageList.Images.SetKeyName(1, "XY轴.ico");
			this.largeImageList.Images.SetKeyName(2, "对焦.ico");
			this.largeImageList.Images.SetKeyName(3, "虹膜.ico");
			this.largeImageList.Images.SetKeyName(4, "混色.ico");
			this.largeImageList.Images.SetKeyName(5, "棱镜.ico");
			this.largeImageList.Images.SetKeyName(6, "频闪.ico");
			this.largeImageList.Images.SetKeyName(7, "速度.ico");
			this.largeImageList.Images.SetKeyName(8, "调光.ico");
			this.largeImageList.Images.SetKeyName(9, "图案.ico");
			this.largeImageList.Images.SetKeyName(10, "图案自转.ico");
			this.largeImageList.Images.SetKeyName(11, "未知.ico");
			this.largeImageList.Images.SetKeyName(12, "颜色.ico");
			this.largeImageList.Images.SetKeyName(13, "1.bmp");
			this.largeImageList.Images.SetKeyName(14, "2.bmp");
			this.largeImageList.Images.SetKeyName(15, "3.bmp");
			this.largeImageList.Images.SetKeyName(16, "4.bmp");
			this.largeImageList.Images.SetKeyName(17, "5.bmp");
			this.largeImageList.Images.SetKeyName(18, "6.bmp");
			this.largeImageList.Images.SetKeyName(19, "7.bmp");
			this.largeImageList.Images.SetKeyName(20, "8.bmp");
			this.largeImageList.Images.SetKeyName(21, "9.bmp");
			this.largeImageList.Images.SetKeyName(22, "10.bmp");
			this.largeImageList.Images.SetKeyName(23, "11.bmp");
			this.largeImageList.Images.SetKeyName(24, "12.bmp");
			this.largeImageList.Images.SetKeyName(25, "13.bmp");
			this.largeImageList.Images.SetKeyName(26, "14.bmp");
			this.largeImageList.Images.SetKeyName(27, "15.bmp");
			this.largeImageList.Images.SetKeyName(28, "16.bmp");
			this.largeImageList.Images.SetKeyName(29, "17.bmp");
			this.largeImageList.Images.SetKeyName(30, "18.bmp");
			this.largeImageList.Images.SetKeyName(31, "19.bmp");
			this.largeImageList.Images.SetKeyName(32, "20.bmp");
			this.largeImageList.Images.SetKeyName(33, "21.bmp");
			this.largeImageList.Images.SetKeyName(34, "22.bmp");
			this.largeImageList.Images.SetKeyName(35, "23.bmp");
			this.largeImageList.Images.SetKeyName(36, "24.bmp");
			this.largeImageList.Images.SetKeyName(37, "25.bmp");
			this.largeImageList.Images.SetKeyName(38, "27.bmp");
			this.largeImageList.Images.SetKeyName(39, "28.bmp");
			this.largeImageList.Images.SetKeyName(40, "29.gif");
			this.largeImageList.Images.SetKeyName(41, "30.bmp");
			this.largeImageList.Images.SetKeyName(42, "31.bmp");
			this.largeImageList.Images.SetKeyName(43, "ledpar.bmp");
			this.largeImageList.Images.SetKeyName(44, "灯带.bmp");
			this.largeImageList.Images.SetKeyName(45, "二合一.bmp");
			this.largeImageList.Images.SetKeyName(46, "二合一50.bmp");
			this.largeImageList.Images.SetKeyName(47, "魔球.bmp");
			this.largeImageList.Images.SetKeyName(48, "帕灯.bmp");
			this.largeImageList.Images.SetKeyName(49, "1.jpg");
			this.largeImageList.Images.SetKeyName(50, "灯光图.png");
			this.largeImageList.Images.SetKeyName(51, "3.jpg");
			this.largeImageList.Images.SetKeyName(52, "4.jpg");
			this.largeImageList.Images.SetKeyName(53, "5.jpg");
			this.largeImageList.Images.SetKeyName(54, "60w.jpg");
			this.largeImageList.Images.SetKeyName(55, "未知.ico");
			this.largeImageList.Images.SetKeyName(56, "j(1).png");
			this.largeImageList.Images.SetKeyName(57, "j(2).png");
			this.largeImageList.Images.SetKeyName(58, "j(3).png");
			this.largeImageList.Images.SetKeyName(59, "j(4).png");
			this.largeImageList.Images.SetKeyName(60, "j(5).png");
			this.largeImageList.Images.SetKeyName(61, "j(6).png");
			this.largeImageList.Images.SetKeyName(62, "j(7).png");
			this.largeImageList.Images.SetKeyName(63, "j(8).png");
			this.largeImageList.Images.SetKeyName(64, "j(9).png");
			this.largeImageList.Images.SetKeyName(65, "j(10).png");
			this.largeImageList.Images.SetKeyName(66, "j(11).png");
			this.largeImageList.Images.SetKeyName(67, "a (1).jpg");
			this.largeImageList.Images.SetKeyName(68, "a (1).png");
			this.largeImageList.Images.SetKeyName(69, "a (2).jpg");
			this.largeImageList.Images.SetKeyName(70, "a (2).png");
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label1.Location = new System.Drawing.Point(279, 203);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 51);
			this.label1.TabIndex = 6;
			this.label1.Text = "提示：双击右侧灯具，可修改初始通道地址。";
			// 
			// skinTreeView1
			// 
			this.skinTreeView1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.skinTreeView1.BorderColor = System.Drawing.Color.Black;
			this.skinTreeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.skinTreeView1.ForeColor = System.Drawing.Color.Black;
			this.skinTreeView1.Location = new System.Drawing.Point(0, 0);
			this.skinTreeView1.Name = "skinTreeView1";
			this.skinTreeView1.Size = new System.Drawing.Size(237, 509);
			this.skinTreeView1.TabIndex = 7;
			this.skinTreeView1.DoubleClick += new System.EventHandler(this.addLightButton_Click);
			// 
			// addButton
			// 
			this.addButton.Location = new System.Drawing.Point(290, 63);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(75, 28);
			this.addButton.TabIndex = 10;
			this.addButton.Text = "添加 ->";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addLightButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Location = new System.Drawing.Point(290, 118);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(75, 28);
			this.deleteButton.TabIndex = 10;
			this.deleteButton.Text = "<- 删除";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteLightButton_Click);
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(290, 380);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(75, 28);
			this.enterButton.TabIndex = 11;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(290, 435);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 28);
			this.cancelButton.TabIndex = 11;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// lightsListView
			// 
			this.lightsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.lightsListView.Dock = System.Windows.Forms.DockStyle.Right;
			this.lightsListView.FullRowSelect = true;
			this.lightsListView.GridLines = true;
			this.lightsListView.HideSelection = false;
			this.lightsListView.Location = new System.Drawing.Point(419, 0);
			this.lightsListView.Name = "lightsListView";
			this.lightsListView.Size = new System.Drawing.Size(328, 509);
			this.lightsListView.TabIndex = 12;
			this.lightsListView.UseCompatibleStateImageBehavior = false;
			this.lightsListView.View = System.Windows.Forms.View.Details;
			this.lightsListView.DoubleClick += new System.EventHandler(this.lightsListView_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "厂商名";
			this.columnHeader1.Width = 90;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "型号";
			this.columnHeader2.Width = 120;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "通道地址";
			this.columnHeader3.Width = 100;
			// 
			// LightsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LightSteelBlue;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(747, 509);
			this.Controls.Add(this.lightsListView);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.skinTreeView1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(763, 548);
			this.Name = "LightsForm";
			this.Text = "编辑工程灯具列表";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LightsForm_FormClosed);
			this.Load += new System.EventHandler(this.LightsForm_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private ImageList largeImageList;
		private Label label1;
		private CCWin.SkinControl.SkinTreeView skinTreeView1;
		private Button addButton;
		private Button deleteButton;
		private Button enterButton;
		private Button cancelButton;
		private ListView lightsListView;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
	}
}