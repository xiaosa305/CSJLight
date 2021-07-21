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
			this.lightsListView = new System.Windows.Forms.ListView();
			this.lightNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lightAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cancelButton = new System.Windows.Forms.Button();
			this.enterButton = new System.Windows.Forms.Button();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.copyAllCheckBox = new System.Windows.Forms.CheckBox();
			this.myStatusStrip = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.myStatusStrip.SuspendLayout();
			this.SuspendLayout();
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
			this.lightsListView.Size = new System.Drawing.Size(309, 285);
			this.lightsListView.TabIndex = 16;
			this.lightsListView.UseCompatibleStateImageBehavior = false;
			this.lightsListView.View = System.Windows.Forms.View.Details;
			this.lightsListView.SelectedIndexChanged += new System.EventHandler(this.lightsListView_SelectedIndexChanged);
			// 
			// lightNo
			// 
			this.lightNo.Text = "序号";
			this.lightNo.Width = 52;
			// 
			// lightType
			// 
			this.lightType.Text = "型号";
			this.lightType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lightType.Width = 121;
			// 
			// lightAddr
			// 
			this.lightAddr.Text = "通道地址";
			this.lightAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lightAddr.Width = 98;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(169, 391);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(89, 24);
			this.cancelButton.TabIndex = 19;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// enterButton
			// 
			this.enterButton.Enabled = false;
			this.enterButton.Location = new System.Drawing.Point(54, 390);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(89, 24);
			this.enterButton.TabIndex = 20;
			this.enterButton.Text = "进入编组";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(126, 347);
			this.nameTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.nameTextBox.MaxLength = 8;
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(132, 21);
			this.nameTextBox.TabIndex = 18;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(52, 351);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 17;
			this.label1.Text = "编组名：";
			// 
			// copyAllCheckBox
			// 
			this.copyAllCheckBox.AutoSize = true;
			this.copyAllCheckBox.Location = new System.Drawing.Point(54, 308);
			this.copyAllCheckBox.Name = "copyAllCheckBox";
			this.copyAllCheckBox.Size = new System.Drawing.Size(120, 16);
			this.copyAllCheckBox.TabIndex = 21;
			this.copyAllCheckBox.Text = "统一设为组长数据";
			this.copyAllCheckBox.UseVisualStyleBackColor = true;
			this.copyAllCheckBox.CheckedChanged += new System.EventHandler(this.copyAllCheckBox_CheckedChanged);
			// 
			// myStatusStrip
			// 
			this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.myStatusStrip.Location = new System.Drawing.Point(0, 431);
			this.myStatusStrip.Name = "myStatusStrip";
			this.myStatusStrip.Size = new System.Drawing.Size(309, 22);
			this.myStatusStrip.SizingGrip = false;
			this.myStatusStrip.TabIndex = 22;
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(264, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// NewGroupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(309, 453);
			this.Controls.Add(this.copyAllCheckBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.nameTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lightsListView);
			this.Controls.Add(this.myStatusStrip);
			this.Name = "NewGroupForm";
			this.Text = "灯具编组";
			this.Load += new System.EventHandler(this.NewGroupForm_Load);
			this.myStatusStrip.ResumeLayout(false);
			this.myStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lightsListView;
		private System.Windows.Forms.ColumnHeader lightNo;
		private System.Windows.Forms.ColumnHeader lightType;
		private System.Windows.Forms.ColumnHeader lightAddr;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox copyAllCheckBox;
		private System.Windows.Forms.StatusStrip myStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
	}
}