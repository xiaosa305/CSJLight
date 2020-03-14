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