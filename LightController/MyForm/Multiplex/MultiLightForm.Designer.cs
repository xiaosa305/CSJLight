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
			this.noticeLabel = new System.Windows.Forms.Label();
			this.copyAllCheckBox = new System.Windows.Forms.CheckBox();
			this.enterButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.lightsListView = new System.Windows.Forms.ListView();
			this.lightName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lightAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// noticeLabel
			// 
			this.noticeLabel.Location = new System.Drawing.Point(12, 351);
			this.noticeLabel.Name = "noticeLabel";
			this.noticeLabel.Size = new System.Drawing.Size(326, 62);
			this.noticeLabel.TabIndex = 10;
			this.noticeLabel.Text = "请选择其中的一个灯具做为编组的组长。在多灯模式界面中，将只展示组长的通道数据。若勾选了“是否统一设为组长数据”，则所有选中的灯具，会将其当前模式及场景下的所有步数都设为与组长一样的数据。";
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
			// lightsListView
			// 
			this.lightsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lightName,
            this.lightType,
            this.lightAddr});
			this.lightsListView.Dock = System.Windows.Forms.DockStyle.Top;
			this.lightsListView.FullRowSelect = true;
			this.lightsListView.GridLines = true;
			this.lightsListView.HideSelection = false;
			this.lightsListView.Location = new System.Drawing.Point(0, 0);
			this.lightsListView.Name = "lightsListView";
			this.lightsListView.Size = new System.Drawing.Size(349, 337);
			this.lightsListView.TabIndex = 14;
			this.lightsListView.UseCompatibleStateImageBehavior = false;
			this.lightsListView.View = System.Windows.Forms.View.Details;
			this.lightsListView.SelectedIndexChanged += new System.EventHandler(this.lightsListView_SelectedIndexChanged);
			// 
			// lightName
			// 
			this.lightName.Text = "厂商名";
			this.lightName.Width = 102;
			// 
			// lightType
			// 
			this.lightType.Text = "型号";
			this.lightType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lightType.Width = 129;
			// 
			// lightAddr
			// 
			this.lightAddr.Text = "通道地址";
			this.lightAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lightAddr.Width = 92;
			// 
			// MultiLightForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(349, 474);
			this.Controls.Add(this.lightsListView);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.copyAllCheckBox);
			this.Controls.Add(this.noticeLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MultiLightForm";
			this.Text = "多灯模式";
			this.Load += new System.EventHandler(this.MultiLightForm_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label noticeLabel;
		private System.Windows.Forms.CheckBox copyAllCheckBox;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.ListView lightsListView;
		private System.Windows.Forms.ColumnHeader lightName;
		private System.Windows.Forms.ColumnHeader lightType;
		private System.Windows.Forms.ColumnHeader lightAddr;
	}
}