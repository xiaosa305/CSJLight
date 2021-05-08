namespace ImportProtocol
{
	partial class ImportProtocolForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.importButton = new System.Windows.Forms.Button();
			this.pbinOpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.protocolComboBox = new System.Windows.Forms.ComboBox();
			this.label18 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.allButton = new System.Windows.Forms.Button();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// importButton
			// 
			this.importButton.BackColor = System.Drawing.Color.PaleTurquoise;
			this.importButton.Location = new System.Drawing.Point(61, 80);
			this.importButton.Name = "importButton";
			this.importButton.Size = new System.Drawing.Size(107, 39);
			this.importButton.TabIndex = 0;
			this.importButton.Text = "导入协议";
			this.importButton.UseVisualStyleBackColor = false;
			this.importButton.Click += new System.EventHandler(this.importButton_Click);
			// 
			// pbinOpenDialog
			// 
			this.pbinOpenDialog.Filter = "pbin配置文件|*.pbin";
			// 
			// protocolComboBox
			// 
			this.protocolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.protocolComboBox.FormattingEnabled = true;
			this.protocolComboBox.Location = new System.Drawing.Point(121, 34);
			this.protocolComboBox.Name = "protocolComboBox";
			this.protocolComboBox.Size = new System.Drawing.Size(177, 20);
			this.protocolComboBox.TabIndex = 16;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(50, 37);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(53, 12);
			this.label18.TabIndex = 15;
			this.label18.Text = "协议选择";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 139);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(344, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 17;
			this.statusStrip1.Text = "statusStrip2";
			// 
			// myStatusLabel
			// 
			this.myStatusLabel.ForeColor = System.Drawing.SystemColors.MenuText;
			this.myStatusLabel.Name = "myStatusLabel";
			this.myStatusLabel.Size = new System.Drawing.Size(329, 17);
			this.myStatusLabel.Spring = true;
			this.myStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// allButton
			// 
			this.allButton.BackColor = System.Drawing.Color.Crimson;
			this.allButton.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.allButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.allButton.Location = new System.Drawing.Point(185, 80);
			this.allButton.Name = "allButton";
			this.allButton.Size = new System.Drawing.Size(107, 39);
			this.allButton.TabIndex = 0;
			this.allButton.Text = "全部导入";
			this.allButton.UseVisualStyleBackColor = false;
			this.allButton.Click += new System.EventHandler(this.importAllButton_Click);
			// 
			// ImportProtocolForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(344, 161);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.protocolComboBox);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.allButton);
			this.Controls.Add(this.importButton);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(360, 200);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(360, 200);
			this.Name = "ImportProtocolForm";
			this.Text = "导入协议到数据库";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button importButton;
		private System.Windows.Forms.OpenFileDialog pbinOpenDialog;
		private System.Windows.Forms.ComboBox protocolComboBox;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
		private System.Windows.Forms.Button allButton;
	}
}

