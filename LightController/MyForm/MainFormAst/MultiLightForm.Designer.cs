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
			this.lightsSkinListView = new CCWin.SkinControl.SkinListView();
			this.lightName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LightAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.noticeLabel = new System.Windows.Forms.Label();
			this.copyAllCheckBox = new System.Windows.Forms.CheckBox();
			this.enterButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
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
			this.lightsSkinListView.HideSelection = false;
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
			// copyAllCheckBox
			// 
			this.copyAllCheckBox.Location = new System.Drawing.Point(18, 418);
			this.copyAllCheckBox.Name = "copyAllCheckBox";
			this.copyAllCheckBox.Size = new System.Drawing.Size(99, 43);
			this.copyAllCheckBox.TabIndex = 12;
			this.copyAllCheckBox.Text = "是否统一设为组长数据";
			this.copyAllCheckBox.UseVisualStyleBackColor = true;
			// 
			// enterButton
			// 
			this.enterButton.Enabled = false;
			this.enterButton.Location = new System.Drawing.Point(158, 423);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(75, 32);
			this.enterButton.TabIndex = 13;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterSkinButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(251, 423);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 32);
			this.cancelButton.TabIndex = 13;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelSkinButton_Click);
			// 
			// MultiLightForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(349, 469);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.copyAllCheckBox);
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
		private System.Windows.Forms.CheckBox copyAllCheckBox;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button cancelButton;
	}
}