namespace LightController.MyForm
{
	partial class ToolsForm
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
			this.toolsSkinFlowLayoutPanel = new CCWin.SkinControl.SkinFlowLayoutPanel();
			this.lightEditorSkinButton = new CCWin.SkinControl.SkinButton();
			this.DKToolSkinButton = new CCWin.SkinControl.SkinButton();
			this.ZKToolSkinButton = new CCWin.SkinControl.SkinButton();
			this.QBToolSkinButton = new CCWin.SkinControl.SkinButton();
			this.noticeLabel1 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.toolsSkinFlowLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolsSkinFlowLayoutPanel
			// 
			this.toolsSkinFlowLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			this.toolsSkinFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.toolsSkinFlowLayoutPanel.Controls.Add(this.lightEditorSkinButton);
			this.toolsSkinFlowLayoutPanel.Controls.Add(this.DKToolSkinButton);
			this.toolsSkinFlowLayoutPanel.Controls.Add(this.ZKToolSkinButton);
			this.toolsSkinFlowLayoutPanel.Controls.Add(this.QBToolSkinButton);
			this.toolsSkinFlowLayoutPanel.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.toolsSkinFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolsSkinFlowLayoutPanel.DownBack = null;
			this.toolsSkinFlowLayoutPanel.Location = new System.Drawing.Point(0, 63);
			this.toolsSkinFlowLayoutPanel.MouseBack = null;
			this.toolsSkinFlowLayoutPanel.Name = "toolsSkinFlowLayoutPanel";
			this.toolsSkinFlowLayoutPanel.NormlBack = null;
			this.toolsSkinFlowLayoutPanel.Size = new System.Drawing.Size(528, 136);
			this.toolsSkinFlowLayoutPanel.TabIndex = 6;
			// 
			// lightEditorSkinButton
			// 
			this.lightEditorSkinButton.AutoSize = true;
			this.lightEditorSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.lightEditorSkinButton.BaseColor = System.Drawing.Color.Transparent;
			this.lightEditorSkinButton.BorderColor = System.Drawing.Color.White;
			this.lightEditorSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.lightEditorSkinButton.DownBack = null;
			this.lightEditorSkinButton.DrawType = CCWin.SkinControl.DrawStyle.None;
			this.lightEditorSkinButton.Font = new System.Drawing.Font("华文细黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lightEditorSkinButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
			this.lightEditorSkinButton.ForeColorSuit = true;
			this.lightEditorSkinButton.Image = global::LightController.Properties.Resources.灯库编辑;
			this.lightEditorSkinButton.ImageSize = new System.Drawing.Size(50, 50);
			this.lightEditorSkinButton.InheritColor = true;
			this.lightEditorSkinButton.IsDrawBorder = false;
			this.lightEditorSkinButton.Location = new System.Drawing.Point(2, 2);
			this.lightEditorSkinButton.Margin = new System.Windows.Forms.Padding(2);
			this.lightEditorSkinButton.MouseBack = null;
			this.lightEditorSkinButton.Name = "lightEditorSkinButton";
			this.lightEditorSkinButton.NormlBack = null;
			this.lightEditorSkinButton.Size = new System.Drawing.Size(124, 106);
			this.lightEditorSkinButton.TabIndex = 5;
			this.lightEditorSkinButton.Text = "灯库编辑工具";
			this.lightEditorSkinButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.lightEditorSkinButton.UseVisualStyleBackColor = false;
			this.lightEditorSkinButton.Visible = false;
			this.lightEditorSkinButton.Click += new System.EventHandler(this.lightEditorSkinButton_Click);
			// 
			// DKToolSkinButton
			// 
			this.DKToolSkinButton.AutoSize = true;
			this.DKToolSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.DKToolSkinButton.BaseColor = System.Drawing.Color.Transparent;
			this.DKToolSkinButton.BorderColor = System.Drawing.Color.White;
			this.DKToolSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.DKToolSkinButton.DownBack = null;
			this.DKToolSkinButton.DrawType = CCWin.SkinControl.DrawStyle.None;
			this.DKToolSkinButton.Font = new System.Drawing.Font("华文细黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.DKToolSkinButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
			this.DKToolSkinButton.ForeColorSuit = true;
			this.DKToolSkinButton.Image = global::LightController.Properties.Resources.CSJ灯控工具;
			this.DKToolSkinButton.ImageSize = new System.Drawing.Size(50, 50);
			this.DKToolSkinButton.InheritColor = true;
			this.DKToolSkinButton.IsDrawBorder = false;
			this.DKToolSkinButton.Location = new System.Drawing.Point(130, 2);
			this.DKToolSkinButton.Margin = new System.Windows.Forms.Padding(2);
			this.DKToolSkinButton.MouseBack = null;
			this.DKToolSkinButton.Name = "DKToolSkinButton";
			this.DKToolSkinButton.NormlBack = null;
			this.DKToolSkinButton.Size = new System.Drawing.Size(124, 106);
			this.DKToolSkinButton.TabIndex = 5;
			this.DKToolSkinButton.Text = "灯控工具";
			this.DKToolSkinButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.DKToolSkinButton.UseVisualStyleBackColor = false;
			this.DKToolSkinButton.Click += new System.EventHandler(this.DKToolSkinButton_Click);
			// 
			// ZKToolSkinButton
			// 
			this.ZKToolSkinButton.AutoSize = true;
			this.ZKToolSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.ZKToolSkinButton.BaseColor = System.Drawing.Color.Transparent;
			this.ZKToolSkinButton.BorderColor = System.Drawing.Color.White;
			this.ZKToolSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.ZKToolSkinButton.DownBack = null;
			this.ZKToolSkinButton.DrawType = CCWin.SkinControl.DrawStyle.None;
			this.ZKToolSkinButton.Font = new System.Drawing.Font("华文细黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ZKToolSkinButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
			this.ZKToolSkinButton.ForeColorSuit = true;
			this.ZKToolSkinButton.Image = global::LightController.Properties.Resources.CSJ中控工具;
			this.ZKToolSkinButton.ImageSize = new System.Drawing.Size(50, 50);
			this.ZKToolSkinButton.InheritColor = true;
			this.ZKToolSkinButton.IsDrawBorder = false;
			this.ZKToolSkinButton.Location = new System.Drawing.Point(258, 2);
			this.ZKToolSkinButton.Margin = new System.Windows.Forms.Padding(2);
			this.ZKToolSkinButton.MouseBack = null;
			this.ZKToolSkinButton.Name = "ZKToolSkinButton";
			this.ZKToolSkinButton.NormlBack = null;
			this.ZKToolSkinButton.Size = new System.Drawing.Size(124, 106);
			this.ZKToolSkinButton.TabIndex = 5;
			this.ZKToolSkinButton.Text = "中控工具";
			this.ZKToolSkinButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.ZKToolSkinButton.UseVisualStyleBackColor = false;
			this.ZKToolSkinButton.Click += new System.EventHandler(this.ZKToolSkinButton_Click);
			// 
			// QBToolSkinButton
			// 
			this.QBToolSkinButton.AutoSize = true;
			this.QBToolSkinButton.BackColor = System.Drawing.Color.Transparent;
			this.QBToolSkinButton.BaseColor = System.Drawing.Color.Transparent;
			this.QBToolSkinButton.BorderColor = System.Drawing.Color.White;
			this.QBToolSkinButton.ControlState = CCWin.SkinClass.ControlState.Normal;
			this.QBToolSkinButton.DownBack = null;
			this.QBToolSkinButton.DrawType = CCWin.SkinControl.DrawStyle.None;
			this.QBToolSkinButton.Font = new System.Drawing.Font("华文细黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.QBToolSkinButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
			this.QBToolSkinButton.ForeColorSuit = true;
			this.QBToolSkinButton.Image = global::LightController.Properties.Resources.CSJ墙板工具;
			this.QBToolSkinButton.ImageSize = new System.Drawing.Size(50, 50);
			this.QBToolSkinButton.InheritColor = true;
			this.QBToolSkinButton.IsDrawBorder = false;
			this.QBToolSkinButton.Location = new System.Drawing.Point(386, 2);
			this.QBToolSkinButton.Margin = new System.Windows.Forms.Padding(2);
			this.QBToolSkinButton.MouseBack = null;
			this.QBToolSkinButton.Name = "QBToolSkinButton";
			this.QBToolSkinButton.NormlBack = null;
			this.QBToolSkinButton.Size = new System.Drawing.Size(124, 106);
			this.QBToolSkinButton.TabIndex = 5;
			this.QBToolSkinButton.Text = "墙板工具";
			this.QBToolSkinButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.QBToolSkinButton.UseVisualStyleBackColor = false;
			this.QBToolSkinButton.Click += new System.EventHandler(this.QBToolSkinButton_Click);
			// 
			// noticeLabel1
			// 
			this.noticeLabel1.AutoSize = true;
			this.noticeLabel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.noticeLabel1.Location = new System.Drawing.Point(12, 9);
			this.noticeLabel1.Name = "noticeLabel1";
			this.noticeLabel1.Size = new System.Drawing.Size(350, 14);
			this.noticeLabel1.TabIndex = 7;
			this.noticeLabel1.Text = "1.使用其它工具，需要安装相应驱动，并保持设备连接;\r\n";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(12, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(399, 14);
			this.label1.TabIndex = 7;
			this.label1.Text = "2.系统必须安装MicroSoft Office Excel，才能使用中控工具。";
			// 
			// ToolsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(528, 199);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.noticeLabel1);
			this.Controls.Add(this.toolsSkinFlowLayoutPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ToolsForm";
			this.Text = "其它工具";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ToolsForm_FormClosed);
			this.Load += new System.EventHandler(this.ToolsForm_Load);
			this.toolsSkinFlowLayoutPanel.ResumeLayout(false);
			this.toolsSkinFlowLayoutPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private CCWin.SkinControl.SkinButton DKToolSkinButton;
		private CCWin.SkinControl.SkinButton ZKToolSkinButton;
		private CCWin.SkinControl.SkinButton QBToolSkinButton;
		private CCWin.SkinControl.SkinFlowLayoutPanel toolsSkinFlowLayoutPanel;
		private System.Windows.Forms.Label noticeLabel1;
		private System.Windows.Forms.Label label1;
		private CCWin.SkinControl.SkinButton lightEditorSkinButton;
	}
}