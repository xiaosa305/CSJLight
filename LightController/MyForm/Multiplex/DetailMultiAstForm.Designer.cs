namespace LightController.MyForm.Multiplex
{
	partial class DetailMultiAstForm
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
			this.bigFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.lightPanelDemo = new System.Windows.Forms.Panel();
			this.allCheckBoxDemo = new System.Windows.Forms.CheckBox();
			this.lightFLPDemo = new System.Windows.Forms.FlowLayoutPanel();
			this.tdCheckBoxDemo = new System.Windows.Forms.CheckBox();
			this.lightLabelDemo = new System.Windows.Forms.Label();
			this.enterButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.bigFlowLayoutPanel.SuspendLayout();
			this.lightPanelDemo.SuspendLayout();
			this.lightFLPDemo.SuspendLayout();
			this.SuspendLayout();
			// 
			// bigFlowLayoutPanel
			// 
			this.bigFlowLayoutPanel.AutoScroll = true;
			this.bigFlowLayoutPanel.BackColor = System.Drawing.SystemColors.Window;
			this.bigFlowLayoutPanel.Controls.Add(this.lightPanelDemo);
			this.bigFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.bigFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.bigFlowLayoutPanel.Name = "bigFlowLayoutPanel";
			this.bigFlowLayoutPanel.Size = new System.Drawing.Size(1037, 513);
			this.bigFlowLayoutPanel.TabIndex = 0;
			this.bigFlowLayoutPanel.WrapContents = false;
			// 
			// lightPanelDemo
			// 
			this.lightPanelDemo.BackColor = System.Drawing.Color.LightBlue;
			this.lightPanelDemo.Controls.Add(this.allCheckBoxDemo);
			this.lightPanelDemo.Controls.Add(this.lightFLPDemo);
			this.lightPanelDemo.Controls.Add(this.lightLabelDemo);
			this.lightPanelDemo.Location = new System.Drawing.Point(3, 3);
			this.lightPanelDemo.Name = "lightPanelDemo";
			this.lightPanelDemo.Size = new System.Drawing.Size(110, 489);
			this.lightPanelDemo.TabIndex = 1;
			this.lightPanelDemo.Visible = false;
			// 
			// allCheckBoxDemo
			// 
			this.allCheckBoxDemo.AutoSize = true;
			this.allCheckBoxDemo.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.allCheckBoxDemo.Location = new System.Drawing.Point(22, 57);
			this.allCheckBoxDemo.Name = "allCheckBoxDemo";
			this.allCheckBoxDemo.Size = new System.Drawing.Size(54, 17);
			this.allCheckBoxDemo.TabIndex = 3;
			this.allCheckBoxDemo.Text = "全选";
			this.allCheckBoxDemo.UseVisualStyleBackColor = true;
			this.allCheckBoxDemo.CheckedChanged += new System.EventHandler(this.allCheckBox_CheckedChanged);
			// 
			// lightFLPDemo
			// 
			this.lightFLPDemo.AutoScroll = true;
			this.lightFLPDemo.Controls.Add(this.tdCheckBoxDemo);
			this.lightFLPDemo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lightFLPDemo.Location = new System.Drawing.Point(0, 77);
			this.lightFLPDemo.Margin = new System.Windows.Forms.Padding(0);
			this.lightFLPDemo.Name = "lightFLPDemo";
			this.lightFLPDemo.Size = new System.Drawing.Size(110, 412);
			this.lightFLPDemo.TabIndex = 2;
			// 
			// tdCheckBoxDemo
			// 
			this.tdCheckBoxDemo.Location = new System.Drawing.Point(3, 3);
			this.tdCheckBoxDemo.Name = "tdCheckBoxDemo";
			this.tdCheckBoxDemo.Size = new System.Drawing.Size(87, 16);
			this.tdCheckBoxDemo.TabIndex = 0;
			this.tdCheckBoxDemo.Text = "X/Y轴转速";
			this.tdCheckBoxDemo.UseVisualStyleBackColor = true;
			// 
			// lightLabelDemo
			// 
			this.lightLabelDemo.Dock = System.Windows.Forms.DockStyle.Top;
			this.lightLabelDemo.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightLabelDemo.Location = new System.Drawing.Point(0, 0);
			this.lightLabelDemo.Name = "lightLabelDemo";
			this.lightLabelDemo.Size = new System.Drawing.Size(110, 50);
			this.lightLabelDemo.TabIndex = 0;
			this.lightLabelDemo.Text = "15激光染色摇头灯(100-130)";
			this.lightLabelDemo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// enterButton
			// 
			this.enterButton.BackColor = System.Drawing.Color.PowderBlue;
			this.enterButton.Location = new System.Drawing.Point(834, 533);
			this.enterButton.Name = "enterButton";
			this.enterButton.Size = new System.Drawing.Size(86, 39);
			this.enterButton.TabIndex = 1;
			this.enterButton.Text = "确定";
			this.enterButton.UseVisualStyleBackColor = false;
			this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(936, 533);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(86, 39);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "取消";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(23, 546);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(413, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "提示：请不要一次性选择过多通道，否则多步联调界面可能出现卡顿的情况。";
			// 
			// DetailMultiAstForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(1037, 591);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.enterButton);
			this.Controls.Add(this.bigFlowLayoutPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "DetailMultiAstForm";
			this.Text = "请选择多灯多步联调的通道";
			this.Load += new System.EventHandler(this.DetailMultiAstForm_Load);
			this.bigFlowLayoutPanel.ResumeLayout(false);
			this.lightPanelDemo.ResumeLayout(false);
			this.lightPanelDemo.PerformLayout();
			this.lightFLPDemo.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel bigFlowLayoutPanel;
		private System.Windows.Forms.Panel lightPanelDemo;
		private System.Windows.Forms.FlowLayoutPanel lightFLPDemo;
		private System.Windows.Forms.CheckBox tdCheckBoxDemo;
		private System.Windows.Forms.Label lightLabelDemo;
		private System.Windows.Forms.Button enterButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.CheckBox allCheckBoxDemo;
		private System.Windows.Forms.Label label1;
	}
}