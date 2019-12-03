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
			this.lightsSkinListView = new CCWin.SkinControl.SkinListView();
			this.lightName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LightAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.enterSkinButton = new CCWin.SkinControl.SkinButton();
			this.deleteSkinButton = new CCWin.SkinControl.SkinButton();
			this.addSkinButton = new CCWin.SkinControl.SkinButton();
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
			// lightsSkinListView
			// 
			this.lightsSkinListView.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lightsSkinListView.BorderColor = System.Drawing.Color.Black;
			this.lightsSkinListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lightName,
            this.LightType,
            this.LightAddr});
			this.lightsSkinListView.Dock = System.Windows.Forms.DockStyle.Right;
			this.lightsSkinListView.FullRowSelect = true;
			this.lightsSkinListView.GridLines = true;
			this.lightsSkinListView.HeadColor = System.Drawing.Color.White;
			this.lightsSkinListView.Location = new System.Drawing.Point(419, 0);
			this.lightsSkinListView.Name = "lightsSkinListView";
			this.lightsSkinListView.OwnerDraw = true;
			this.lightsSkinListView.RowBackColor1 = System.Drawing.Color.Transparent;
			this.lightsSkinListView.RowBackColor2 = System.Drawing.Color.Transparent;
			this.lightsSkinListView.Size = new System.Drawing.Size(328, 509);
			this.lightsSkinListView.TabIndex = 8;
			this.lightsSkinListView.UseCompatibleStateImageBehavior = false;
			this.lightsSkinListView.View = System.Windows.Forms.View.Details;
			this.lightsSkinListView.DoubleClick += new System.EventHandler(this.lightsListView_DoubleClick);
			// 
			// lightName
			// 
			this.lightName.Text = "厂商名";
			this.lightName.Width = 97;
			// 
			// LightType
			// 
			this.LightType.Text = "型号";
			this.LightType.Width = 92;
			// 
			// LightAddr
			// 
			this.LightAddr.Text = "通道地址";
			this.LightAddr.Width = 86;
			// 
			// enterSkinButton
			// 
			this.enterSkinButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.enterSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.enterSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.enterSkinButton.BorderColor = System.Drawing.Color.Black;
			this.enterSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.enterSkinButton.DownBack = null;
			this.enterSkinButton.Location = new System.Drawing.Point(290, 448);
			this.enterSkinButton.MouseBack = null;
			this.enterSkinButton.Name = "enterSkinButton";
			this.enterSkinButton.NormlBack = null;
			this.enterSkinButton.Size = new System.Drawing.Size(73, 32);
			this.enterSkinButton.TabIndex = 9;
			this.enterSkinButton.Text = "确定";
			this.enterSkinButton.UseVisualStyleBackColor = false;
			this.enterSkinButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// deleteSkinButton
			// 
			this.deleteSkinButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.deleteSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.deleteSkinButton.BaseColor = System.Drawing.Color.MistyRose;
			this.deleteSkinButton.BorderColor = System.Drawing.Color.Black;
			this.deleteSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.deleteSkinButton.DownBack = null;
			this.deleteSkinButton.Location = new System.Drawing.Point(290, 109);
			this.deleteSkinButton.MouseBack = null;
			this.deleteSkinButton.Name = "deleteSkinButton";
			this.deleteSkinButton.NormlBack = null;
			this.deleteSkinButton.Size = new System.Drawing.Size(73, 32);
			this.deleteSkinButton.TabIndex = 9;
			this.deleteSkinButton.Text = "<- 删除";
			this.deleteSkinButton.UseVisualStyleBackColor = false;
			this.deleteSkinButton.Click += new System.EventHandler(this.deleteLightButton_Click);
			// 
			// addSkinButton
			// 
			this.addSkinButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.addSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.addSkinButton.BaseColor = System.Drawing.Color.LightSalmon;
			this.addSkinButton.BorderColor = System.Drawing.Color.Black;
			this.addSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.addSkinButton.DownBack = null;
			this.addSkinButton.Location = new System.Drawing.Point(290, 57);
			this.addSkinButton.MouseBack = null;
			this.addSkinButton.Name = "addSkinButton";
			this.addSkinButton.NormlBack = null;
			this.addSkinButton.Size = new System.Drawing.Size(73, 32);
			this.addSkinButton.TabIndex = 9;
			this.addSkinButton.Text = "添加 ->";
			this.addSkinButton.UseVisualStyleBackColor = false;
			this.addSkinButton.Click += new System.EventHandler(this.addLightButton_Click);
			// 
			// LightsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ClientSize = new System.Drawing.Size(747, 509);
			this.Controls.Add(this.addSkinButton);
			this.Controls.Add(this.deleteSkinButton);
			this.Controls.Add(this.enterSkinButton);
			this.Controls.Add(this.lightsSkinListView);
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
		private CCWin.SkinControl.SkinListView lightsSkinListView;
		private ColumnHeader lightName;
		private ColumnHeader LightType;
		private ColumnHeader LightAddr;
		private CCWin.SkinControl.SkinButton enterSkinButton;
		private CCWin.SkinControl.SkinButton deleteSkinButton;
		private CCWin.SkinControl.SkinButton addSkinButton;
	}
}