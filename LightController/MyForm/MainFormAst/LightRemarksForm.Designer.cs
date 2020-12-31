namespace LightController.MyForm.LightList
{
	partial class LightRemarkForm
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
			this.cancelButton = new System.Windows.Forms.Button();
			this.enterButton = new System.Windows.Forms.Button();
			this.lightTypeLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lightRemarkTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lightAddrLabel = new System.Windows.Forms.Label();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(139, 131);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(72, 26);
			this.cancelButton.TabIndex = 15;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// enterButton
			// 
			this.enterButton.Location = new System.Drawing.Point(31, 132);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(72, 26);
			this.enterButton.TabIndex = 16;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = true;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// lightTypeLabel
			// 
			this.lightTypeLabel.AutoSize = true;
			this.lightTypeLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lightTypeLabel.Location = new System.Drawing.Point(96, 27);
			this.lightTypeLabel.Name = "lightTypeLabel";
			this.lightTypeLabel.Size = new System.Drawing.Size(31, 12);
			this.lightTypeLabel.TabIndex = 14;
			this.lightTypeLabel.Text = "型号";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(25, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 13;
			this.label1.Text = "灯具型号：";
			// 
			// lightRemarkTextBox
			// 
			this.lightRemarkTextBox.Location = new System.Drawing.Point(96, 90);
			this.lightRemarkTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.lightRemarkTextBox.MaxLength = 10;
			this.lightRemarkTextBox.Name = "lightRemarkTextBox";
			this.lightRemarkTextBox.Size = new System.Drawing.Size(125, 21);
			this.lightRemarkTextBox.TabIndex = 12;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(25, 94);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 12);
			this.label3.TabIndex = 11;
			this.label3.Text = "灯具备注：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(25, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 13;
			this.label2.Text = "灯具地址：";
			// 
			// lightAddrLabel
			// 
			this.lightAddrLabel.AutoSize = true;
			this.lightAddrLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lightAddrLabel.Location = new System.Drawing.Point(96, 60);
			this.lightAddrLabel.Name = "lightAddrLabel";
			this.lightAddrLabel.Size = new System.Drawing.Size(31, 12);
			this.lightAddrLabel.TabIndex = 14;
			this.lightAddrLabel.Text = "地址";
			// 
			// LightRemarkForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(240, 176);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.lightAddrLabel);
			this.Controls.Add(this.lightTypeLabel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lightRemarkTextBox);
			this.Controls.Add(this.label3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "LightRemarkForm";
			this.Text = "修改灯具备注";
			this.Load += new System.EventHandler(this.LightRemarkForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Label lightTypeLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox lightRemarkTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lightAddrLabel;
		private System.Windows.Forms.ToolTip myToolTip;
	}
}