namespace LightController.MyForm.MainFormAst
{
	partial class SAUseForm
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
			this.saFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.demoButton = new System.Windows.Forms.Button();
			this.saToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.saFlowLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// saFlowLayoutPanel
			// 
			this.saFlowLayoutPanel.AutoScroll = true;
			this.saFlowLayoutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.saFlowLayoutPanel.Controls.Add(this.demoButton);
			this.saFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.saFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.saFlowLayoutPanel.Name = "saFlowLayoutPanel";
			this.saFlowLayoutPanel.Size = new System.Drawing.Size(320, 175);
			this.saFlowLayoutPanel.TabIndex = 0;
			// 
			// demoButton
			// 
			this.demoButton.BackColor = System.Drawing.Color.White;
			this.demoButton.Location = new System.Drawing.Point(3, 3);
			this.demoButton.Name = "demoButton";
			this.demoButton.Size = new System.Drawing.Size(94, 23);
			this.demoButton.TabIndex = 98;
			this.demoButton.Text = "测试达到八个汉字";
			this.demoButton.UseVisualStyleBackColor = false;
			this.demoButton.Visible = false;
			this.demoButton.Click += new System.EventHandler(this.saButton_Click);
			// 
			// SAUseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(320, 175);
			this.Controls.Add(this.saFlowLayoutPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SAUseForm";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SAUseForm_Load);
			this.saFlowLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel saFlowLayoutPanel;
		private System.Windows.Forms.ToolTip saToolTip;
		private System.Windows.Forms.Button demoButton;
	}
}