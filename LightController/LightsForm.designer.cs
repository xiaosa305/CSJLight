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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LightsForm));
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.lightsListView = new System.Windows.Forms.ListView();
			this.LightAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LigntName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.largeImageList = new System.Windows.Forms.ImageList(this.components);
			this.smallImageList = new System.Windows.Forms.ImageList(this.components);
			this.LargeIconButton = new System.Windows.Forms.Button();
			this.smallIconButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.button2.Location = new System.Drawing.Point(386, 68);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(97, 40);
			this.button2.TabIndex = 2;
			this.button2.Text = "添加-->";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.BackColor = System.Drawing.Color.Beige;
			this.button3.Location = new System.Drawing.Point(386, 141);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(97, 40);
			this.button3.TabIndex = 2;
			this.button3.Text = "<--删除";
			this.button3.UseVisualStyleBackColor = false;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(386, 546);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(97, 49);
			this.button4.TabIndex = 2;
			this.button4.Text = "确定";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(0, 1);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(347, 634);
			this.treeView1.TabIndex = 3;
			// 
			// lightsListView
			// 
			this.lightsListView.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.lightsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LightAddr,
            this.LigntName,
            this.LightType});
			this.lightsListView.Dock = System.Windows.Forms.DockStyle.Right;
			this.lightsListView.GridLines = true;
			this.lightsListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
			this.lightsListView.LargeImageList = this.largeImageList;
			this.lightsListView.Location = new System.Drawing.Point(520, 0);
			this.lightsListView.Name = "lightsListView";
			this.lightsListView.Size = new System.Drawing.Size(381, 636);
			this.lightsListView.SmallImageList = this.smallImageList;
			this.lightsListView.TabIndex = 5;
			this.lightsListView.UseCompatibleStateImageBehavior = false;
			this.lightsListView.View = System.Windows.Forms.View.Details;
			this.lightsListView.SelectedIndexChanged += new System.EventHandler(this.lightsListView_SelectedIndexChanged);
			// 
			// LightAddr
			// 
			this.LightAddr.Text = "地址";
			this.LightAddr.Width = 107;
			// 
			// LigntName
			// 
			this.LigntName.Text = "厂商名";
			this.LigntName.Width = 118;
			// 
			// LightType
			// 
			this.LightType.Text = "型号";
			this.LightType.Width = 142;
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
			// 
			// smallImageList
			// 
			this.smallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImageList.ImageStream")));
			this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.smallImageList.Images.SetKeyName(0, "频闪.ico");
			this.smallImageList.Images.SetKeyName(1, "速度.ico");
			this.smallImageList.Images.SetKeyName(2, "调光.ico");
			this.smallImageList.Images.SetKeyName(3, "图案.ico");
			this.smallImageList.Images.SetKeyName(4, "图案自转.ico");
			this.smallImageList.Images.SetKeyName(5, "未知.ico");
			this.smallImageList.Images.SetKeyName(6, "颜色.ico");
			// 
			// LargeIconButton
			// 
			this.LargeIconButton.Location = new System.Drawing.Point(386, 276);
			this.LargeIconButton.Name = "LargeIconButton";
			this.LargeIconButton.Size = new System.Drawing.Size(97, 50);
			this.LargeIconButton.TabIndex = 6;
			this.LargeIconButton.Text = "大图标";
			this.LargeIconButton.UseVisualStyleBackColor = true;
			this.LargeIconButton.Click += new System.EventHandler(this.LargeIconButton_Click);
			// 
			// smallIconButton
			// 
			this.smallIconButton.Location = new System.Drawing.Point(386, 332);
			this.smallIconButton.Name = "smallIconButton";
			this.smallIconButton.Size = new System.Drawing.Size(97, 50);
			this.smallIconButton.TabIndex = 6;
			this.smallIconButton.Text = "小图标";
			this.smallIconButton.UseVisualStyleBackColor = true;
			this.smallIconButton.Click += new System.EventHandler(this.smallIconButton_Click);
			// 
			// LightsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(901, 636);
			this.Controls.Add(this.smallIconButton);
			this.Controls.Add(this.LargeIconButton);
			this.Controls.Add(this.lightsListView);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Name = "LightsForm";
			this.Text = "灯具编辑";
			this.Load += new System.EventHandler(this.LightsForm_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ListView lightsListView;
		private System.Windows.Forms.ColumnHeader LightAddr;
		private System.Windows.Forms.ColumnHeader LigntName;
		private System.Windows.Forms.ColumnHeader LightType;
		private ImageList largeImageList;
		private ImageList smallImageList;
		private Button LargeIconButton;
		private Button smallIconButton;
	}
}