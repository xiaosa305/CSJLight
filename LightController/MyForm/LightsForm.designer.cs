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
			this.addLightButton = new System.Windows.Forms.Button();
			this.deleteLightButton = new System.Windows.Forms.Button();
			this.enterButton = new System.Windows.Forms.Button();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.lightsListView = new System.Windows.Forms.ListView();
			this.LigntName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LightAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.largeImageList = new System.Windows.Forms.ImageList(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// addLightButton
			// 
			this.addLightButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.addLightButton.Location = new System.Drawing.Point(386, 105);
			this.addLightButton.Name = "addLightButton";
			this.addLightButton.Size = new System.Drawing.Size(97, 40);
			this.addLightButton.TabIndex = 2;
			this.addLightButton.Text = "添加-->";
			this.addLightButton.UseVisualStyleBackColor = false;
			this.addLightButton.Click += new System.EventHandler(this.addLightButton_Click);
			// 
			// deleteLightButton
			// 
			this.deleteLightButton.BackColor = System.Drawing.Color.Beige;
			this.deleteLightButton.Location = new System.Drawing.Point(386, 168);
			this.deleteLightButton.Name = "deleteLightButton";
			this.deleteLightButton.Size = new System.Drawing.Size(97, 40);
			this.deleteLightButton.TabIndex = 2;
			this.deleteLightButton.Text = "<--删除";
			this.deleteLightButton.UseVisualStyleBackColor = false;
			this.deleteLightButton.Click += new System.EventHandler(this.deleteLightButton_Click);
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(386, 546);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(97, 49);
			this.enterButton.TabIndex = 2;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// treeView1
			// 
			this.treeView1.BackColor = System.Drawing.SystemColors.WindowText;
			this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView1.ForeColor = System.Drawing.Color.OldLace;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(347, 636);
			this.treeView1.TabIndex = 3;
			this.treeView1.DoubleClick += new System.EventHandler(this.addLightButton_Click);
			// 
			// lightsListView
			// 
			this.lightsListView.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.lightsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LigntName,
            this.LightType,
            this.LightAddr});
			this.lightsListView.Dock = System.Windows.Forms.DockStyle.Right;
			this.lightsListView.GridLines = true;
			this.lightsListView.LargeImageList = this.largeImageList;
			this.lightsListView.Location = new System.Drawing.Point(526, 0);
			this.lightsListView.MultiSelect = false;
			this.lightsListView.Name = "lightsListView";
			this.lightsListView.Size = new System.Drawing.Size(470, 636);
			this.lightsListView.SmallImageList = this.largeImageList;
			this.lightsListView.TabIndex = 5;
			this.lightsListView.UseCompatibleStateImageBehavior = false;
			this.lightsListView.View = System.Windows.Forms.View.Details;
			this.lightsListView.DoubleClick += new System.EventHandler(this.lightsListView_DoubleClick);
			// 
			// LigntName
			// 
			this.LigntName.Text = "厂商名";
			this.LigntName.Width = 89;
			// 
			// LightType
			// 
			this.LightType.Text = "型号";
			this.LightType.Width = 79;
			// 
			// LightAddr
			// 
			this.LightAddr.Text = "地址";
			this.LightAddr.Width = 64;
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
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(372, 254);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(130, 64);
			this.label1.TabIndex = 6;
			this.label1.Text = "提示：双击右侧灯具，可修改初始通道地址。";
			// 
			// LightsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(996, 636);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lightsListView);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.deleteLightButton);
			this.Controls.Add(this.addLightButton);
			this.Name = "LightsForm";
			this.Text = "编辑工程灯具列表";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LightsForm_FormClosed);
			this.Load += new System.EventHandler(this.LightsForm_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button addLightButton;
		private System.Windows.Forms.Button deleteLightButton;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ListView lightsListView;
		private System.Windows.Forms.ColumnHeader LigntName;
		private System.Windows.Forms.ColumnHeader LightType;
		private ImageList largeImageList;
		private ColumnHeader LightAddr;
		private Label label1;
	}
}