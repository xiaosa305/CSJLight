namespace LightController.MyForm.Multiplex
{
	partial class GroupForm
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
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.enterButton = new System.Windows.Forms.Button();
			this.lightsListView = new System.Windows.Forms.ListView();
			this.lightNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lightAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.noticeLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(108, 361);
			this.nameTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.nameTextBox.MaxLength = 8;
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(132, 21);
			this.nameTextBox.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(37, 365);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "编组名：";
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(146, 400);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(89, 24);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(36, 400);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(89, 24);
			this.enterButton.TabIndex = 5;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// lightsListView
			// 
			this.lightsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lightNo,
            this.lightType,
            this.lightAddr});
			this.lightsListView.Dock = System.Windows.Forms.DockStyle.Top;
			this.lightsListView.FullRowSelect = true;
			this.lightsListView.GridLines = true;
			this.lightsListView.HideSelection = false;
			this.lightsListView.Location = new System.Drawing.Point(0, 0);
			this.lightsListView.Name = "lightsListView";
			this.lightsListView.Size = new System.Drawing.Size(274, 285);
			this.lightsListView.TabIndex = 15;
			this.lightsListView.UseCompatibleStateImageBehavior = false;
			this.lightsListView.View = System.Windows.Forms.View.Details;
			// 
			// lightNo
			// 
			this.lightNo.Text = "序号";
			this.lightNo.Width = 40;
			// 
			// lightType
			// 
			this.lightType.Text = "型号";
			this.lightType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lightType.Width = 111;
			// 
			// lightAddr
			// 
			this.lightAddr.Text = "通道地址";
			this.lightAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lightAddr.Width = 98;
			// 
			// noticeLabel
			// 
			this.noticeLabel.Location = new System.Drawing.Point(3, 289);
			this.noticeLabel.Margin = new System.Windows.Forms.Padding(2);
			this.noticeLabel.Name = "noticeLabel";
			this.noticeLabel.Padding = new System.Windows.Forms.Padding(3);
			this.noticeLabel.Size = new System.Drawing.Size(269, 51);
			this.noticeLabel.TabIndex = 16;
			this.noticeLabel.Text = "请选择其中的一个灯具做为编组的组长。在多灯模式界面中，将只展示组长的通道数据。若未选择，默认使用编组的第一个灯具作为组长。";
			this.noticeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GroupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.MenuBar;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(274, 442);
			this.Controls.Add(this.noticeLabel);
			this.Controls.Add(this.lightsListView);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GroupForm";
			this.Text = "灯具编组";
			this.Load += new System.EventHandler(this.GroupForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.ListView lightsListView;
		private System.Windows.Forms.ColumnHeader lightType;
		private System.Windows.Forms.ColumnHeader lightAddr;
		private System.Windows.Forms.Label noticeLabel;
		private System.Windows.Forms.ColumnHeader lightNo;
	}
}