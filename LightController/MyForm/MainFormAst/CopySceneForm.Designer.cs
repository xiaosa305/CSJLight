namespace LightController.MyForm
{
	partial class CopySceneForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.sceneComboBox = new System.Windows.Forms.ComboBox();
            this.fromButton = new System.Windows.Forms.Button();
            this.toButton = new System.Windows.Forms.Button();
            this.normalCheckBox = new System.Windows.Forms.CheckBox();
            this.soundCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "指定场景：";
            // 
            // sceneComboBox
            // 
            this.sceneComboBox.FormattingEnabled = true;
            this.sceneComboBox.Location = new System.Drawing.Point(88, 27);
            this.sceneComboBox.Name = "sceneComboBox";
            this.sceneComboBox.Size = new System.Drawing.Size(145, 20);
            this.sceneComboBox.TabIndex = 10;
            // 
            // fromButton
            // 
            this.fromButton.Enabled = false;
            this.fromButton.Location = new System.Drawing.Point(31, 102);
            this.fromButton.Name = "fromButton";
            this.fromButton.Size = new System.Drawing.Size(202, 26);
            this.fromButton.TabIndex = 11;
            this.fromButton.Text = "从指定场景复制到当前场景";
            this.fromButton.UseVisualStyleBackColor = true;
            this.fromButton.Click += new System.EventHandler(this.fromButton_Click);
            // 
            // toButton
            // 
            this.toButton.Enabled = false;
            this.toButton.Location = new System.Drawing.Point(31, 141);
            this.toButton.Name = "toButton";
            this.toButton.Size = new System.Drawing.Size(202, 26);
            this.toButton.TabIndex = 11;
            this.toButton.Text = "从当前场景复制到指定场景";
            this.toButton.UseVisualStyleBackColor = true;
            this.toButton.Click += new System.EventHandler(this.toButton_Click);
            // 
            // normalCheckBox
            // 
            this.normalCheckBox.AutoSize = true;
            this.normalCheckBox.Location = new System.Drawing.Point(44, 63);
            this.normalCheckBox.Name = "normalCheckBox";
            this.normalCheckBox.Size = new System.Drawing.Size(72, 16);
            this.normalCheckBox.TabIndex = 12;
            this.normalCheckBox.Text = "常规模式";
            this.normalCheckBox.UseVisualStyleBackColor = true;
            this.normalCheckBox.CheckedChanged += new System.EventHandler(this.modeCheckBox_CheckedChanged);
            // 
            // soundCheckBox
            // 
            this.soundCheckBox.AutoSize = true;
            this.soundCheckBox.Location = new System.Drawing.Point(138, 63);
            this.soundCheckBox.Name = "soundCheckBox";
            this.soundCheckBox.Size = new System.Drawing.Size(72, 16);
            this.soundCheckBox.TabIndex = 12;
            this.soundCheckBox.Text = "音频模式";
            this.soundCheckBox.UseVisualStyleBackColor = true;
            this.soundCheckBox.CheckedChanged += new System.EventHandler(this.modeCheckBox_CheckedChanged);
            // 
            // CopySceneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(266, 186);
            this.Controls.Add(this.soundCheckBox);
            this.Controls.Add(this.normalCheckBox);
            this.Controls.Add(this.toButton);
            this.Controls.Add(this.fromButton);
            this.Controls.Add(this.sceneComboBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CopySceneForm";
            this.Text = "复用场景";
            this.Load += new System.EventHandler(this.CopySceneForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox sceneComboBox;
		private System.Windows.Forms.Button fromButton;
        private System.Windows.Forms.Button toButton;
        private System.Windows.Forms.CheckBox normalCheckBox;
        private System.Windows.Forms.CheckBox soundCheckBox;
    }
}