namespace LightController.MyForm
{
	partial class MultiLightForm
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
			this.lightsSkinListView = new CCWin.SkinControl.SkinListView();
			this.lightName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LightAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.noticeLabel = new System.Windows.Forms.Label();
			this.enterSkinButton = new CCWin.SkinControl.SkinButton();
			this.cancelSkinButton = new CCWin.SkinControl.SkinButton();
			this.copyAllCheckBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lightsSkinListView
			// 
			this.lightsSkinListView.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lightsSkinListView.BorderColor = System.Drawing.Color.Black;
			this.lightsSkinListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lightName,
            this.LightType,
            this.LightAddr});
			this.lightsSkinListView.Dock = System.Windows.Forms.DockStyle.Top;
			this.lightsSkinListView.FullRowSelect = true;
			this.lightsSkinListView.GridLines = true;
			this.lightsSkinListView.HeadColor = System.Drawing.Color.White;
			this.lightsSkinListView.Location = new System.Drawing.Point(0, 0);
			this.lightsSkinListView.MultiSelect = false;
			this.lightsSkinListView.Name = "lightsSkinListView";
			this.lightsSkinListView.OwnerDraw = true;
			this.lightsSkinListView.RowBackColor1 = System.Drawing.Color.Transparent;
			this.lightsSkinListView.RowBackColor2 = System.Drawing.Color.Transparent;
			this.lightsSkinListView.Size = new System.Drawing.Size(349, 337);
			this.lightsSkinListView.TabIndex = 9;
			this.lightsSkinListView.UseCompatibleStateImageBehavior = false;
			this.lightsSkinListView.View = System.Windows.Forms.View.Details;
			this.lightsSkinListView.SelectedIndexChanged += new System.EventHandler(this.lightsSkinListView_SelectedIndexChanged);
			this.lightsSkinListView.DoubleClick += new System.EventHandler(this.enterSkinButton_Click);
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
			// noticeLabel
			// 
			this.noticeLabel.Location = new System.Drawing.Point(12, 351);
			this.noticeLabel.Name = "noticeLabel";
			this.noticeLabel.Size = new System.Drawing.Size(326, 62);
			this.noticeLabel.TabIndex = 10;
			this.noticeLabel.Text = "请选择其中的一个灯具做为编组的组长。在多灯模式界面中，将只展示组长的通道数据。若勾选了“是否统一设为组长数据”，则所有选中的灯具，会将其当前模式及场景下的所有步数" +
    "都设为与组长一样的数据。\r\n\r\n";
			// 
			// enterSkinButton
			// 
			this.enterSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.enterSkinButton.BaseColor = System.Drawing.Color.SkyBlue;
			this.enterSkinButton.BorderColor = System.Drawing.Color.Black;
			this.enterSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.enterSkinButton.DownBack = null;
			this.enterSkinButton.Enabled = false;
			this.enterSkinButton.Location = new System.Drawing.Point(154, 422);
			this.enterSkinButton.MouseBack = null;
			this.enterSkinButton.Name = "enterSkinButton";
			this.enterSkinButton.NormlBack = null;
			this.enterSkinButton.Size = new System.Drawing.Size(75, 32);
			this.enterSkinButton.TabIndex = 11;
			this.enterSkinButton.Text = "确定";
			this.enterSkinButton.UseVisualStyleBackColor = false;
			this.enterSkinButton.Click += new System.EventHandler(this.enterSkinButton_Click);
			// 
			// cancelSkinButton
			// 
			this.cancelSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.cancelSkinButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.cancelSkinButton.BorderColor = System.Drawing.Color.Black;
			this.cancelSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.cancelSkinButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelSkinButton.DownBack = null;
			this.cancelSkinButton.Location = new System.Drawing.Point(251, 422);
			this.cancelSkinButton.MouseBack = null;
			this.cancelSkinButton.Name = "cancelSkinButton";
			this.cancelSkinButton.NormlBack = null;
			this.cancelSkinButton.Size = new System.Drawing.Size(75, 32);
			this.cancelSkinButton.TabIndex = 11;
			this.cancelSkinButton.Text = "取消";
			this.cancelSkinButton.UseVisualStyleBackColor = false;
			this.cancelSkinButton.Click += new System.EventHandler(this.cancelSkinButton_Click);
			// 
			// copyAllCheckBox
			// 
			this.copyAllCheckBox.Location = new System.Drawing.Point(18, 418);
			this.copyAllCheckBox.Name = "copyAllCheckBox";
			this.copyAllCheckBox.Size = new System.Drawing.Size(99, 43);
			this.copyAllCheckBox.TabIndex = 12;
			this.copyAllCheckBox.Text = "是否统一设为组长数据";
			this.copyAllCheckBox.UseVisualStyleBackColor = true;
			// 
			// MultiLightForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelSkinButton;
			this.ClientSize = new System.Drawing.Size(349, 469);
			this.Controls.Add(this.copyAllCheckBox);
			this.Controls.Add(this.cancelSkinButton);
			this.Controls.Add(this.enterSkinButton);
			this.Controls.Add(this.noticeLabel);
			this.Controls.Add(this.lightsSkinListView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MultiLightForm";
			this.Text = "多灯模式";
			this.Load += new System.EventHandler(this.MultiLightForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private CCWin.SkinControl.SkinListView lightsSkinListView;
		private System.Windows.Forms.ColumnHeader lightName;
		private System.Windows.Forms.ColumnHeader LightType;
		private System.Windows.Forms.ColumnHeader LightAddr;
		private System.Windows.Forms.Label noticeLabel;
		private CCWin.SkinControl.SkinButton enterSkinButton;
		private CCWin.SkinControl.SkinButton cancelSkinButton;
		private System.Windows.Forms.CheckBox copyAllCheckBox;
	}
}